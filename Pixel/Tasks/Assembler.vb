Imports System.IO
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports Pixel.Interfaces
Namespace Tasks
    Public Class Assembler
        Inherits Task
        Private Property Usercode As List(Of String)
        Private Property Table As List(Of Definition)
        Private Property Labels As Dictionary(Of String, UInt16)
        Sub New(Parent As Machine, Filename As String)
            MyBase.New(Parent)
            If (File.Exists(Filename)) Then
                Me.Usercode = New List(Of String)
                Me.Table = New List(Of Definition)
                Me.Labels = New Dictionary(Of String, UShort)
                Using stream As New StreamReader(Filename, True)
                    Me.Usercode.AddRange(stream.ReadToEnd.Split(CChar(Environment.NewLine)))
                    Me.Usercode = Me.Usercode.Normalize
                End Using
            Else
                Throw New Exception(String.Format("File does not exist '{0}'", Filename))
            End If
        End Sub
        ''' <summary>
        ''' Initializes instance
        ''' </summary>
        Public Overrides Sub Initialize()
            Me.Table.Add(New Definition(Opcodes.OP_PUSH, "\s+"))
            Me.Table.Add(New Definition(Opcodes.OP_PUSH, "\bPUSH\b"))
            Me.Table.Add(New Definition(Opcodes.OP_POP, "\bPOP\b"))
            Me.Table.Add(New Definition(Opcodes.OP_LD, "\bLOAD\b"))
            Me.Table.Add(New Definition(Opcodes.OP_ST, "\bSTORE\b"))
            Me.Table.Add(New Definition(Opcodes.OP_STV, "\bSTOREV\b"))
            Me.Table.Add(New Definition(Opcodes.OP_JUMP, "\bJMP\b"))
            Me.Table.Add(New Definition(Opcodes.OP_CALL, "\bCALL\b"))
            Me.Table.Add(New Definition(Opcodes.OP_RET, "\bRETURN\b"))
            Me.Table.Add(New Definition(Opcodes.OP_ADD, "\bADD\b"))
            Me.Table.Add(New Definition(Opcodes.OP_SUB, "\bSUB\b"))
            Me.Table.Add(New Definition(Opcodes.OP_MUL, "\bPMUL\b"))
            Me.Table.Add(New Definition(Opcodes.OP_DIV, "\bDIV\b"))
            Me.Table.Add(New Definition(Opcodes.OP_MOD, "\bMOD\b"))
            Me.Table.Add(New Definition(Opcodes.OP_AND, "\bAND\b"))
            Me.Table.Add(New Definition(Opcodes.OP_OR, "\bOR\b"))
            Me.Table.Add(New Definition(Opcodes.OP_XOR, "\bXOR\b"))
            Me.Table.Add(New Definition(Opcodes.OP_IFN, "\bIFN\b"))
            Me.Table.Add(New Definition(Opcodes.OP_IFG, "\bIFG\b"))
            Me.Table.Add(New Definition(Opcodes.OP_IFL, "\bIFL\b"))
            Me.Table.Add(New Definition(Opcodes.OP_IFV, "\bIFV\b"))
            Me.Table.Add(New Definition(Opcodes.OP_IFNV, "\bIFNV\b"))
            Me.Table.Add(New Definition(Opcodes.OP_IFGV, "\bIFGV\b"))
            Me.Table.Add(New Definition(Opcodes.OP_IFLV, "\bIFLV\b"))
            Me.Table.Add(New Definition(Opcodes.OP_STKEY, "\bKEY\b"))
            Me.Table.Add(New Definition(Opcodes.OP_IF, "\bIF\b"))
            Me.Table.Add(New Definition(Opcodes.OP_SHR, "\bSHR\b"))
            Me.Table.Add(New Definition(Opcodes.OP_SHL, "\bSHL\b"))
            Me.Table.Add(New Definition(Opcodes.OP_OV, "\bSTOV\b"))
            Me.Table.Add(New Definition(Opcodes.OP_COL, "\bSTCOL\b"))
            Me.Table.Add(New Definition(Opcodes.OP_SCR, "\bSCROLL\b"))
            Me.Table.Add(New Definition(Opcodes.OP_READ, "\bREADAT\b"))
            Me.Table.Add(New Definition(Opcodes.OP_WRITE, "\bWRITEAT\b"))
            Me.Table.Add(New Definition(Opcodes.OP_ADDR, "\bADDRESSOF\b"))
            Me.Table.Add(New Definition(Opcodes.OP_RND, "\bRANDOM\b"))
            Me.Table.Add(New Definition(Opcodes.OP_CLS, "\bCLEAR\b"))
            Me.Table.Add(New Definition(Opcodes.OP_END, "\bEND\b"))
            Me.Table.Add(New Definition(Opcodes.OP_SPRITE, "\bDRAW\b"))
            Me.Table.Add(New Definition(Opcodes.OP_PRINT, "\bPRINT\b"))
            Me.Table.Add(New Definition(Opcodes.T_LABEL, "\:(?:[a-z][a-z0-9_]*)"))
            Me.Table.Add(New Definition(Opcodes.T_NUMBER, "\#[a-z0-9]+"))
            Me.Table.Add(New Definition(Opcodes.T_HEXADECIMAL, "0x[a-f0-9]+"))
            Me.Table.Add(New Definition(Opcodes.T_STRING, "("".*?"")"))
            Me.Table.Add(New Definition(Opcodes.T_LOCATION, "\[(?:[a-z][a-z0-9_]*)\]"))
            Me.Table.Add(New Definition(Opcodes.T_VARIABLE, "[0-9]+"))
            Me.Table.Add(New Definition(Opcodes.T_SPRITEDATA, "\.((0|1)|\s+(0|1)){8}"))
            Me.Table.Add(New Definition(Opcodes.T_KEY, "\{([a-z0-9\s\!\?\+\-\*\/\.\,\:\→\←\↑\↓\=\>\<\[\]\|\'\\]{1})\}"))
        End Sub
        ''' <summary>
        ''' Begins parsing of user code into bytecode
        ''' </summary>
        Public Overrides Sub Execute()
            Dim location As Integer, found As Boolean, rx As Regex, m As Match, line As String
            For Each state As Mode In [Enum].GetValues(GetType(Mode))
                location = 0
                line = String.Join(" ", Me.Usercode)
                Do
                    found = False
                    For Each definition As Definition In Me.Table
                        rx = New Regex(String.Format("^{0}", definition.Pattern), RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                        m = rx.Match(line)
                        If (m.Success) Then
                            Select Case state
                                Case Mode.Scan
                                    If (definition.Type = Opcodes.T_LABEL) Then
                                        Me.Labels.Add(m.Value.Substring(1).ToUpper, CUShort(location))
                                    Else
                                        location += Me.GetLength(definition, m)
                                    End If
                                Case Mode.Write
                                    Me.WriteInstruction(definition, m.Value)
                            End Select
                            found = True
                            line = line.Substring(m.Length).Trim
                        End If
                    Next
                    If (Not found) Then
                        Throw New Exception(String.Format("Syntax error, unable to parse '{0}...'", line.Truncate(10)))
                    End If
                Loop Until line.Length = 0
            Next
            Components.Processor.Dump(".\Program.bin", Me.Parent.Bytecode.ToArray)
        End Sub
        ''' <summary>
        ''' Returns instruction byte length
        ''' </summary>
        Private Function GetLength(Definition As Definition, Match As Match) As Integer
            If (Definition.Type = Opcodes.T_STRING) Then
                Return (Match.Groups(1).Length - 1) * 2
            Else
                Return Definition.Length
            End If
        End Function
        ''' <summary>
        ''' Write bytecode instruction to stream
        ''' </summary>
        Private Sub WriteInstruction(Definition As Definition, Line As String)
            If (Definition.GetActualLength = 1) Then
                Me.WriteByte(Definition.ToByte)
                Me.WriteUInt16(0)
            ElseIf (Definition.GetActualLength = 3) Then
                Me.WriteByte(Definition.ToByte)
            ElseIf (Definition.GetActualLength > 3) Then
                Me.WriteValue(Definition, Line)
            End If
        End Sub
        ''' <summary>
        ''' Writes instruction parameter (number,hexadecimal or label)
        ''' </summary>
        Private Sub WriteValue(Definition As Definition, Line As String)
            Select Case Definition.Type
                Case Opcodes.T_NUMBER
                    Me.WriteUInt16(UInt16.Parse(Line.Substring(1)))
                Case Opcodes.T_HEXADECIMAL
                    Me.WriteUInt16(UInt16.Parse(Line.Substring(2), NumberStyles.HexNumber))
                Case Opcodes.T_LOCATION
                    Me.WriteUInt16(Me.GetLabel(Line))
                Case Opcodes.T_STRING
                    Me.WriteString(Line)
                Case Opcodes.T_KEY
                    Me.WriteKey(Line.Substring(1, Line.Length - 2).ToUpper)
                Case Opcodes.T_VARIABLE
                    Me.WriteUInt16(Line.String2UInt16)
                Case Opcodes.T_SPRITEDATA
                    Me.WriteByte(Line.String2Byte)
                Case Else
                    Throw New Exception(String.Format("Could not convert definition '{0}' to value", Definition.Type))
            End Select
        End Sub
        ''' <summary>
        ''' Returns label address, throws exception if not found
        ''' </summary>
        Private Function GetLabel(Line As String) As UInt16
            Dim reference As String = Line.Substring(1, Line.Length - 2).ToUpper
            If (Me.Labels.ContainsKey(reference)) Then Return Me.Labels(reference)
            Throw New Exception(String.Format("No labels defined as '{0}'", Line))
        End Function
        ''' <summary>
        ''' Writes a byte to the bytecode stream
        ''' </summary>
        Private Sub WriteByte(Value As Byte)
            Me.Parent.Bytecode.Add(Value)
        End Sub
        ''' <summary>
        ''' Writes an unsigned int16 to the bytecode stream
        ''' </summary>
        Private Sub WriteUInt16(Value As UInt16)
            Dim bytes() As Byte = BitConverter.GetBytes(Value)
            Me.Parent.Bytecode.AddRange({bytes(1), bytes(0)})
        End Sub
        ''' <summary>
        ''' Writes key index to the bytestream
        ''' </summary>
        Private Sub WriteKey(ch As String)
            Dim chrindex As Integer = AscW(ch)
            For i As Integer = 0 To My.Resources.characters.Length - 1 Step 4
                Dim chrint As UInt16 = BitConverter.ToUInt16(New Byte(1) {My.Resources.characters(i + 1), My.Resources.characters(i)}, 0)
                Dim chraddr As UInt16 = BitConverter.ToUInt16(New Byte(1) {My.Resources.characters(i + 3), My.Resources.characters(i + 2)}, 0)
                If (chrint = chrindex) Then
                    Me.WriteUInt16(chraddr)
                    Exit For
                End If
            Next
        End Sub
        ''' <summary>
        ''' Writes string data to the bytecode stream
        ''' </summary>
        Private Sub WriteString(Line As String)
            For Each ch As Char In Line.Substring(1, Line.Length - 2).ToCharArray
                Me.WriteUInt16(ch.ToKeyIndex)
            Next
            Me.WriteUInt16(0)
        End Sub
    End Class
End Namespace