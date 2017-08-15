Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Namespace Assembler
    Public Class Lexer
        Inherits List(Of Token)
        Private Property Grammar As IGrammar
        Private Property Usercode As StringBuilder
        Sub New(Filename As String, Grammar As IGrammar)
            If (File.Exists(Filename)) Then
                Me.Grammar = Grammar
                Me.Usercode = New StringBuilder
                Using fs As New FileStream(Filename, FileMode.Open, FileAccess.Read)
                    Using reader As New StreamReader(fs)
                        Me.Usercode.Append(reader.ReadToEnd)
                    End Using
                End Using
            End If
        End Sub
        Public Function Parse() As List(Of Token)
            If (Me.Usercode.Length > 0 And Me.Grammar.Rules.Count > 0) Then
                Dim i As Integer = 0, match As Match, ltype As Types
                Do While (i < Me.Usercode.Length)
                    For Each rule As Rule In Me.Grammar.Rules
                        match = rule.Regex.Match(Me.Usercode.ToString)
                        If (match.Success) Then
                            If (rule.Type = Types.T_END AndAlso ltype = rule.Type) Then
                                i = match.Length
                                Me.Usercode.Remove(0, i)
                                Exit For
                            ElseIf (rule.Type = Types.T_SPACE) Then
                                i = match.Length
                                Me.Usercode.Remove(0, i)
                                Exit For
                            ElseIf (rule.Type = Types.T_COMMENT) Then
                                i = match.Length
                                Me.Usercode.Remove(0, i)
                                Exit For
                            End If
                            i = match.Length
                            Me.Add(New Token(rule.Type, match))
                            Me.Usercode.Remove(0, i)
                            ltype = rule.Type
                            Exit For
                        End If
                    Next
                Loop
                Me.Add(New Token(Types.T_EOF))
            End If
            Return Me
        End Function
    End Class
End Namespace

