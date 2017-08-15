Imports System.IO
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports Pixel.Assembler
Public Class ByteStream
    Inherits MemoryStream
    Private Property Index As Integer
    Private Property Location As Integer
    Private Property Token As Token
    Private Property TokenStream As List(Of Token)
    Private Property Labels As Dictionary(Of String, UInt16)
    Sub New(Tokens As List(Of Token))
        Me.TokenStream = Tokens
        Me.Labels = New Dictionary(Of String, UInt16)
    End Sub
    Public Function Compile() As Byte()
        Me.Parse(Mode.Scan)
        Me.Parse(Mode.Write)
        Components.Memory.Dump(".\Program.bin", Me.ToArray)
        Return Me.ToArray
    End Function
    Private Sub Parse(Mode As Mode)
        If (Me.TokenStream.Count = 0) Then Return
        Me.Index = 0
        Me.Location = 0
        Me.Token = Me.TokenStream.First
        Do
            Select Case Me.Token.Type
                Case Types.OP_PUSH
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_LD
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_POP
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_ST
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_STV
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_JUMP
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_CALL
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_RET
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_ADD
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_SUB
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_MUL
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_DIV
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_MOD
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_AND
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_OR
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_XOR
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_IF
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_IFN
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_IFL
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_IFG
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_IFV
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_IFNV
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_IFLV
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_IFGV
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_SHR
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_SHL
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_SCR
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_READ
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_WRITE
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_ADDR
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_RND
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_STKEY
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_END
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_CLS
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                        Me.Fill(1)
                    End If
                    Me.Location += 3
                    Me.NextToken()
                Case Types.OP_DRAW
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_PRINT
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.OP_PRINTV
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteByte(Me.Token.ToByte)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.T_LABEL
                    If (Mode = Pixel.Mode.Scan) Then
                        Me.SetLabel(Me.Token.Match.Value, CUShort(Me.Location))
                    End If
                    Me.NextToken()
                Case Types.T_VARIABLE
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteValue(Me.Token)
                    End If
                    Me.Location += 2
                    Me.NextToken()
                Case Types.T_LOCATION
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteValue(Me.Token)
                    End If
                    Me.Location += 2
                    Me.NextToken()
                Case Types.T_SPRITEDATA
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteValue(Me.Token)
                    End If
                    Me.Location += 1
                    Me.NextToken()
                Case Types.T_CONST_KEY
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteValue(Me.Token)
                    End If
                    Me.Location += 2
                    Me.NextToken()
                Case Types.T_CONST_NUMBER
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteValue(Me.Token)
                    End If
                    Me.Location += 2
                    Me.NextToken()
                Case Types.T_CONST_STRING
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteValue(Me.Token)
                    End If
                    Me.Location += (Me.Token.Match.Groups(1).Length - 1) * 2
                    Me.NextToken()
                Case Types.T_CONST_HEXADECIMAL
                    If (Mode = Pixel.Mode.Write) Then
                        Me.WriteValue(Me.Token)
                    End If
                    Me.Location += 2
                    Me.NextToken()
            End Select
            If (Me.Token.Type = Types.T_END) Then Me.NextToken()
        Loop Until Me.Token.Type = Types.T_EOF
    End Sub
    Private Sub NextToken()
        Me.Index += 1
        Me.Token = Me.TokenStream(Me.Index)
    End Sub
    Private Sub Fill(length As Integer, Optional Value As UInt16 = 0)
        For i As Integer = 1 To length
            Me.WriteUInt16(Value)
            Me.Location += 2
        Next
    End Sub
    Private Sub WriteUInt16(Value As UInt16)
        Dim bytes() As Byte = BitConverter.GetBytes(Value)
        Me.WriteByte(bytes(1))
        Me.WriteByte(bytes(0))
    End Sub
    Private Sub WriteValue(Token As Token)
        Select Case Token.Type
            Case Types.T_CONST_NUMBER
                Me.WriteUInt16(UInt16.Parse(Token.Match.Value.Substring(1)))
            Case Types.T_CONST_HEXADECIMAL
                Me.WriteUInt16(UInt16.Parse(Token.Match.Value.Substring(2), NumberStyles.HexNumber))
            Case Types.T_LOCATION
                Me.WriteUInt16(Me.GetLabel(Token.Match.Value))
            Case Types.T_CONST_STRING
                Me.WriteString(Token.Match.Value)
            Case Types.T_CONST_KEY
                Me.WriteKey(Token.Match.Value.Substring(1, Token.Match.Value.Length - 2).ToUpper)
            Case Types.T_VARIABLE
                Me.WriteUInt16(Token.Match.Value.StringToUInt16)
            Case Types.T_SPRITEDATA
                Me.WriteByte(Token.Match.Value.StringToByte)
            Case Else
                Throw New Exception(String.Format("Undefined value type '{0}'", Token.Type))
        End Select
    End Sub
    Private Sub SetLabel(Line As String, Location As UInt16)
        Dim reference As String = Line.Substring(1).ToUpper
        If (Not Me.Labels.ContainsKey(reference)) Then
            Me.Labels.Add(reference, Location)
            Return
        End If
        Throw New Exception(String.Format("Label '{0}' already exist", reference))
    End Sub
    Private Function GetLabel(Line As String) As UInt16
        Dim reference As String = Line.Substring(1, Line.Length - 2).ToUpper
        If (Me.Labels.ContainsKey(reference)) Then Return Me.Labels(reference)
        Throw New Exception(String.Format("Label '{0}' does not exist", Line))
    End Function
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
    Private Sub WriteString(Line As String)
        For Each ch As Char In Line.Substring(1, Line.Length - 2).ToCharArray
            Me.WriteUInt16(ch.ToKeyIndex)
        Next
        Me.WriteUInt16(0)
    End Sub
    Public Overrides Function ToString() As String
        Return String.Format("Length: {0} Tokens: {1} Labels: {2}", Me.Length, Me.TokenStream.Count, Me.Labels.Count)
    End Function
    Protected Overrides Sub Dispose(disposing As Boolean)
        Me.Labels = Nothing
        Me.TokenStream = Nothing
        MyBase.Dispose(disposing)
    End Sub
End Class
