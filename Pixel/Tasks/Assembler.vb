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
                    Me.Normalize()
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
            Me.Table.Add(New Definition(TType.T_LD, "\bLD\b"))
            Me.Table.Add(New Definition(TType.T_ST, "\bST\b"))
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
            Me.Table.Add(New Definition(TType.T_IFN, "\bIFN\b"))
            Me.Table.Add(New Definition(TType.T_IFG, "\bIFG\b"))
            Me.Table.Add(New Definition(TType.T_IFG, "\bIFL\b"))
            Me.Table.Add(New Definition(TType.T_IF, "\bIF\b"))
            Me.Table.Add(New Definition(TType.T_SHR, "\bSHR\b"))
            Me.Table.Add(New Definition(TType.T_SHL, "\bSHL\b"))
            Me.Table.Add(New Definition(TType.T_IFK, "\bIFKEY\b"))
            Me.Table.Add(New Definition(TType.T_TIMER, "\bTIMER\b"))
            Me.Table.Add(New Definition(TType.T_PUSHDATA, "\bUPDD\b"))
            Me.Table.Add(New Definition(TType.T_PUSHMEMORY, "\bUPDM\b"))
            Me.Table.Add(New Definition(TType.T_CLS, "\bCLEAR\b"))
            Me.Table.Add(New Definition(TType.T_END, "\bEND\b"))
            Me.Table.Add(New Definition(TType.T_LABEL, "\:[a-z0-9_]+"))
            Me.Table.Add(New Definition(TType.T_NUMBER, "\#[a-z0-9]+"))
            Me.Table.Add(New Definition(TType.T_HEX, "0x[a-f0-9]+"))
            Me.Table.Add(New Definition(TType.T_LOCATION, "\[[a-z0-9_]+\]"))
            Me.Table.Add(New Definition(TType.T_VAR, "\:([0-9]+|\s+[0-9]+)"))
            Me.Table.Add(New Definition(TType.T_DAT, "\.((0|1)|\s+(0|1)){8}"))
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
                                    ElseIf (definition.Type = TType.T_DAT) Then
                                        location += 1
                                    ElseIf (definition.Type = TType.T_VAR) Then
                                        location += 2
                                    ElseIf (definition.Type <> TType.T_HEX And
                                            definition.Type <> TType.T_NUMBER And
                                            definition.Type <> TType.T_LOCATION) Then
                                        location += 3
                                    End If
                                Case Mode.Write
                                    Me.WriteInstruction(definition, m.Value)
                            End Select
                            found = True
                            line = line.Substring(m.Length).Trim
                        End If
                    Next
                    If (Not found) Then
                        Throw New Exception(String.Format("Could not match any definitions against '{0}...'", line.Substring(0, 15)))
                    End If
                Loop Until line.Length = 0
            Next
        End Sub
        ''' <summary>
        ''' Write bytecode instruction to stream
        ''' </summary>
        Private Sub WriteInstruction(Definition As Definition, Line As String)
            Select Case Definition.Type
                Case TType.T_PUSH
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_POP
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_LD
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_ST
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_JUMP
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_CALL
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_RET
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_ADD
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_SUB
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_MUL
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_DIV
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_MOD
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_AND
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_OR
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_XOR
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_IF
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_IFN
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_IFG
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_IFL
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_SHR
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_SHL
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_IFK
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_TIMER
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_PUSHDATA
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_PUSHMEMORY
                    Me.WriteByte(Definition.ToBytecode)
                Case TType.T_CLS
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_END
                    Me.WriteByte(Definition.ToBytecode)
                    Me.WriteUInt16(0)
                Case TType.T_DAT
                    Me.WriteByte(Me.String2Byte(Line))
                Case TType.T_VAR
                    Me.WriteUInt16(Me.String2UInt16(Line))
                Case TType.T_NUMBER
                    Me.WriteValue(Definition, Line)
                Case TType.T_HEX
                    Me.WriteValue(Definition, Line)
                Case TType.T_LOCATION
                    Me.WriteValue(Definition, Line)
            End Select
        End Sub
        ''' <summary>
        ''' Converts string number to UInt16
        ''' </summary>
        Private Function String2UInt16(Str As String) As UInt16
            Return UInt16.Parse(Str.Trim(New Char() {":"c, " "c}))
        End Function
        ''' <summary>
        ''' Converts binary array to byte
        ''' </summary>
        Private Function String2Byte(Str As String) As Byte
            Return Convert.ToByte(Str.Trim(New Char() {"."c, " "c}), 2)
        End Function
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
        ''' Cleans up user code string array
        ''' </summary>
        Private Sub Normalize()
            Dim changed As Boolean
            Do
                changed = False
                For i As Integer = 0 To Me.Usercode.Count - 1
                    Me.Usercode(i) = Me.Usercode(i).Trim
                    If (String.IsNullOrEmpty(Me.Usercode(i))) Then
                        Me.Usercode.RemoveAt(i)
                        changed = True
                        Exit For
                    End If
                Next
            Loop While changed
        End Sub
    End Class
End Namespace
