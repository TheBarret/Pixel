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
        Dim value As Byte = 0
        If (Byte.TryParse(Str.Trim(New Char() {"."c, " "c}), value)) Then
            Return value
        End If
        Throw New Exception(String.Format("Unable to parse number '{0}'", Str))
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
                Lines(i) = Lines(i).Trim
                Lines(i) = Regex.Replace(Lines(i), "\;(.*?)(\r?\n|$)", String.Empty)
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
    Public Function ToSpriteAddress(ch As Char) As UInt16
        Select Case Char.ToUpper(ch)
            Case "A"c : Return 4
            Case "B"c : Return 10
            Case "C"c : Return 16
            Case "D"c : Return 22
            Case "E"c : Return 28
            Case "F"c : Return 34
            Case "G"c : Return 40
            Case "H"c : Return 46
            Case "I"c : Return 52
            Case "J"c : Return 58
            Case "K"c : Return 64
            Case "L"c : Return 70
            Case "M"c : Return 76
            Case "N"c : Return 82
            Case "O"c : Return 88
            Case "P"c : Return 94
            Case "Q"c : Return 100
            Case "R"c : Return 106
            Case "S"c : Return 112
            Case "T"c : Return 118
            Case "U"c : Return 124
            Case "V"c : Return 130
            Case "W"c : Return 136
            Case "X"c : Return 142
            Case "Y"c : Return 148
            Case "Z"c : Return 154
            Case "1"c : Return 160
            Case "2"c : Return 166
            Case "3"c : Return 172
            Case "4"c : Return 178
            Case "5"c : Return 184
            Case "6"c : Return 190
            Case "7"c : Return 196
            Case "8"c : Return 202
            Case "9"c : Return 208
            Case "0"c : Return 214
            Case "!"c : Return 220
            Case "?"c : Return 226
            Case "+"c : Return 232
            Case "-"c : Return 238
            Case "*"c : Return 244
            Case "/"c : Return 250
            Case "."c : Return 256
            Case ":"c : Return 262
            Case "→"c : Return 268
            Case "←"c : Return 274
            Case "↑"c : Return 280
            Case "↓"c : Return 286
            Case "="c : Return 292
            Case ">"c : Return 298
            Case "<"c : Return 304
            Case "'"c : Return 310
            Case "["c : Return 316
            Case "|"c : Return 322
            Case "]"c : Return 328
            Case "\"c : Return 334
            Case " "c : Return 340
            Case ","c : Return 346
            Case Else : Return 340
        End Select
    End Function
End Module