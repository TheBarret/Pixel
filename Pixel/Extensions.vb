Imports System.Text.RegularExpressions

Public Module Extensions
    <System.Runtime.CompilerServices.Extension> _
    Public Function String2UInt16(Str As String) As UInt16
        Dim value As UInt16 = 0
        If (UInt16.TryParse(Str.Trim(New Char() {":"c, " "c}), value)) Then
            Return value
        End If
        Throw New Exception(String.Format("Unable to parse number '{0}'", Str))
    End Function
    <System.Runtime.CompilerServices.Extension> _
    Public Function String2Byte(Str As String) As Byte
        Return Convert.ToByte(Str.Trim.Remove(0, 1), 2)
    End Function
    <System.Runtime.CompilerServices.Extension> _
    Public Function Random(value As UInt16) As UInt16
        Static rnd As New Random(Environment.TickCount)
        Return CUShort(rnd.Next(0, value))
    End Function
    <System.Runtime.CompilerServices.Extension> _
    Public Function Truncate(value As String, max As Integer) As String
        Return If(value.Length <= max, value, value.Substring(0, max) + "...")
    End Function
    <System.Runtime.CompilerServices.Extension> _
    Public Function Normalize(Lines As List(Of String)) As List(Of String)
        Dim changed As Boolean
        Do
            changed = False
            For i As Integer = 0 To Lines.Count - 1
                Lines(i) = Regex.Replace(Lines(i), "\;(.*?)(\r?\n|$)", String.Empty)
                Lines(i) = Lines(i).Trim
                If (String.IsNullOrEmpty(Lines(i))) Then
                    Lines.RemoveAt(i)
                    changed = True
                    Exit For
                End If
            Next
        Loop While changed
        Return Lines
    End Function
    <System.Runtime.CompilerServices.Extension> _
    Public Function ToKeyIndex(ch As Char) As UInt16
        Select Case Char.ToUpper(ch)
            Case "A"c : Return Keys.A
            Case "B"c : Return Keys.B
            Case "C"c : Return Keys.C
            Case "D"c : Return Keys.D
            Case "E"c : Return Keys.E
            Case "F"c : Return Keys.F
            Case "G"c : Return Keys.G
            Case "H"c : Return Keys.H
            Case "I"c : Return Keys.I
            Case "J"c : Return Keys.J
            Case "K"c : Return Keys.K
            Case "L"c : Return Keys.L
            Case "M"c : Return Keys.M
            Case "N"c : Return Keys.N
            Case "O"c : Return Keys.O
            Case "P"c : Return Keys.P
            Case "Q"c : Return Keys.Q
            Case "R"c : Return Keys.R
            Case "S"c : Return Keys.S
            Case "T"c : Return Keys.T
            Case "U"c : Return Keys.U
            Case "V"c : Return Keys.V
            Case "W"c : Return Keys.W
            Case "X"c : Return Keys.X
            Case "Y"c : Return Keys.Y
            Case "Z"c : Return Keys.Z
            Case "1"c : Return Keys.D1
            Case "2"c : Return Keys.D2
            Case "3"c : Return Keys.D3
            Case "4"c : Return Keys.D4
            Case "5"c : Return Keys.D5
            Case "6"c : Return Keys.D6
            Case "7"c : Return Keys.D7
            Case "8"c : Return Keys.D8
            Case "9"c : Return Keys.D9
            Case "0"c : Return Keys.D0
            Case "!"c : Return Keys.EM
            Case "?"c : Return Keys.QM
            Case "+"c : Return Keys.Plus
            Case "-"c : Return Keys.Minus
            Case "*"c : Return Keys.Mult
            Case "/"c : Return Keys.Div
            Case "."c : Return Keys.Dot
            Case ":"c : Return Keys.Colon
            Case "→"c : Return Keys.Right
            Case "←"c : Return Keys.Left
            Case "↑"c : Return Keys.Up
            Case "↓"c : Return Keys.Down
            Case "="c : Return Keys.Equal
            Case ">"c : Return Keys.Greater
            Case "<"c : Return Keys.Lesser
            Case "'"c : Return Keys.Quote
            Case "["c : Return Keys.BOpen
            Case "|"c : Return Keys.Pipe
            Case "]"c : Return Keys.BClose
            Case "\"c : Return Keys.Slash
            Case " "c : Return Keys.Space
            Case ","c : Return Keys.Comma
            Case Else : Return Keys.Space
        End Select
    End Function
End Module