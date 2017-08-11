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
            Me.Table.Add(New Definition(TType.T_PUSH, "\bPUSH\b"))
            Me.Table.Add(New Definition(TType.T_POP, "\bPOP\b"))
            Me.Table.Add(New Definition(TType.T_LD, "\bLOAD\b"))
            Me.Table.Add(New Definition(TType.T_ST, "\bSTORE\b"))
            Me.Table.Add(New Definition(TType.T_STV, "\bSTOREV\b"))
            Me.Table.Add(New Definition(TType.T_JUMP, "\bJMP\b"))
            Me.Table.Add(New Definition(TType.T_CALL, "\bCALL\b"))
            Me.Table.Add(New Definition(TType.T_RET, "\bRETURN\b"))
            Me.Table.Add(New Definition(TType.T_ADD, "\bADD\b"))
            Me.Table.Add(New Definition(TType.T_SUB, "\bSUB\b"))
            Me.Table.Add(New Definition(TType.T_MUL, "\bPMUL\b"))
            Me.Table.Add(New Definition(TType.T_DIV, "\bDIV\b"))
            Me.Table.Add(New Definition(TType.T_MOD, "\bMOD\b"))
            Me.Table.Add(New Definition(TType.T_AND, "\bAND\b"))
            Me.Table.Add(New Definition(TType.T_OR, "\bOR\b"))
            Me.Table.Add(New Definition(TType.T_XOR, "\bXOR\b"))
            Me.Table.Add(New Definition(TType.T_IF, "\bIF\b"))
            Me.Table.Add(New Definition(TType.T_IFN, "\bIFN\b"))
            Me.Table.Add(New Definition(TType.T_IFG, "\bIFG\b"))
            Me.Table.Add(New Definition(TType.T_IFG, "\bIFL\b"))
            Me.Table.Add(New Definition(TType.T_SHR, "\bSHR\b"))
            Me.Table.Add(New Definition(TType.T_SHL, "\bSHL\b"))
            Me.Table.Add(New Definition(TType.T_SHL, "\bOVERFLOW\b"))
            Me.Table.Add(New Definition(TType.T_WRITE, "\bWRITEAT\b"))
            Me.Table.Add(New Definition(TType.T_ADDR, "\bADDRESSOF\b"))
            Me.Table.Add(New Definition(TType.T_RND, "\bRANDOM\b"))
            Me.Table.Add(New Definition(TType.T_IFK, "\bIFKEY\b"))
            Me.Table.Add(New Definition(TType.T_CLS, "\bCLEAR\b"))
            Me.Table.Add(New Definition(TType.T_END, "\bEND\b"))
            Me.Table.Add(New Definition(TType.T_LABEL, "\:[a-z0-9_]+"))
            Me.Table.Add(New Definition(TType.T_NUMBER, "\#[a-z0-9]+"))
            Me.Table.Add(New Definition(TType.T_HEX, "0x[a-f0-9]+"))
            Me.Table.Add(New Definition(TType.T_STR, "\@("".*?"")"))
            Me.Table.Add(New Definition(TType.T_LOCATION, "\[[a-z0-9_]+\]"))
            Me.Table.Add(New Definition(TType.T_VAR, "\:([0-9]+|\s+[0-9]+)"))
            Me.Table.Add(New Definition(TType.T_DAT, "\.((0|1)|\s+(0|1)){8}"))
            Me.Table.Add(New Definition(TType.T_PUSHVRAMDATA, "\bDRAW\b"))
            Me.Table.Add(New Definition(TType.T_PUSHVRAMMEMORY, "\bDRAWM\b"))
            Me.Table.Add(New Definition(TType.T_PUSHVRAMSTRING, "\bPRINT\b"))
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
                                    If (definition.Type = TType.T_LABEL) Then
                                        Me.Labels.Add(m.Value.Substring(1).ToUpper, CUShort(location))
                                    Else
                                        location += Me.GetOpcodeLength(definition, m)
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
        Private Function GetOpcodeLength(Definition As Definition, Match As Match) As Integer
            Select Case Definition.Type
                Case TType.T_HEX,
                     TType.T_NUMBER,
                     TType.T_LOCATION
                    Return 0
                Case TType.T_DAT
                    Return 1
                Case TType.T_VAR
                    Return 2
                Case TType.T_IF,
                     TType.T_IFN,
                     TType.T_IFG,
                     TType.T_IFL,
                     TType.T_STV,
                     TType.T_STV
                    Return 5
                Case TType.T_PUSHVRAMDATA,
                     TType.T_PUSHVRAMMEMORY
                    Return 7
                Case TType.T_PUSHVRAMSTRING
                    Return 9
                Case TType.T_STR
                    Return (Match.Groups(1).Length - 1) * 2
                Case Else
                    Return 3
            End Select
        End Function
        ''' <summary>
        ''' Write bytecode instruction to stream
        ''' </summary>
        Private Sub WriteInstruction(Definition As Definition, Line As String)
            Select Case Definition.Type
                Case TType.T_PUSH,
                     TType.T_LD,
                     TType.T_ST,
                     TType.T_STV,
                     TType.T_JUMP,
                     TType.T_CALL,
                     TType.T_IF,
                     TType.T_IFN,
                     TType.T_IFG,
                     TType.T_IFL,
                     TType.T_SHR,
                     TType.T_SHL,
                     TType.T_OV,
                     TType.T_WRITE,
                     TType.T_ADDR,
                     TType.T_RND,
                     TType.T_IFK,
                     TType.T_PUSHVRAMDATA,
                     TType.T_PUSHVRAMMEMORY,
                     TType.T_PUSHVRAMSTRING
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_POP,
                     TType.T_RET,
                     TType.T_ADD,
                     TType.T_SUB,
                     TType.T_MUL,
                     TType.T_DIV,
                     TType.T_MOD,
                     TType.T_AND,
                     TType.T_OR,
                     TType.T_XOR,
                     TType.T_CLS,
                     TType.T_END
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_DAT
                    Me.WriteByte(Line.String2Byte)
                Case TType.T_VAR
                    Me.WriteUInt16(Line.String2UInt16)
                Case TType.T_STR
                    Me.WriteString(Line)
                Case TType.T_NUMBER,
                     TType.T_HEX,
                     TType.T_LOCATION
                    Me.WriteValue(Definition, Line)
            End Select
        End Sub
        ''' <summary>
        ''' Writes instruction parameter (number,hexadecimal or label)
        ''' </summary>
        Private Sub WriteValue(Definition As Definition, Line As String)
            Select Case Definition.Type
                Case TType.T_NUMBER
                    Me.WriteUInt16(UInt16.Parse(Line.Substring(1)))
                Case TType.T_HEX
                    Me.WriteUInt16(UInt16.Parse(Line.Substring(2), NumberStyles.HexNumber))
                Case TType.T_LOCATION
                    Me.WriteUInt16(Me.GetLabel(Line))
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
        ''' Writes string data to the bytecode stream
        ''' </summary>
        Private Sub WriteString(Line As String)
            Dim str As String = Line.Substring(2, Line.Length - 3)
            For Each ch As Char In str.ToCharArray
                Me.WriteUInt16(ch.ToSpriteAddress)
            Next
            Me.WriteUInt16(0)
        End Sub
    End Class
End Namespace