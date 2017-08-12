Namespace Tasks
    Public Class Token
        Public Property Type As Opcodes
        Public Property Value As String
        Public Property Index As Integer
        Sub New(Type As Opcodes, Value As String, Index As Integer)
            Me.Type = Type
            Me.Value = Value
            Me.Index = Index
        End Sub
    End Class
End Namespace