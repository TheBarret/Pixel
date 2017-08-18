Imports System.Text.RegularExpressions
Namespace Assembler
    Public Class Rule
        Public Property Regex As Regex
        Public Property Type As Types
        Sub New(Regex As Regex, Type As Types)
            Me.Regex = Regex
            Me.Type = Type
        End Sub
        Public Overrides Function ToString() As String
            Return String.Format("{0} [{1}]", Me.Type, Me.Regex.ToString)
        End Function
    End Class
End Namespace