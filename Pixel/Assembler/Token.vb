Imports System.Text.RegularExpressions
Namespace Assembler
    Public Class Token
        Public Property Type As Types
        Public Property Match As Match
        Sub New(Type As Types)
            Me.Type = Type
        End Sub
        Sub New(Type As Types, Match As Match)
            Me.Type = Type
            Me.Match = Match
        End Sub
        Public Function ToByte() As Byte
            Return CType(Me.Type, Byte)
        End Function
        Public Overrides Function ToString() As String
            Return String.Format("{0}", Me.Type)
        End Function
    End Class
End Namespace