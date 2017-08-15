Imports System.Text.RegularExpressions
Namespace Assembler
    Public Class Rule
        Public Property Regex As Regex
        Public Property Type As Types
        Sub New(Regex As Regex, Type As Types)
            Me.Regex = Regex
            Me.Type = Type
        End Sub
    End Class
End Namespace