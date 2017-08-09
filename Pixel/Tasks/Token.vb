Namespace Tasks
    Public Class Token
        Public Property Type As TType
        Public Property Value As String
        Public Property Index As Integer
        Sub New(Type As TType, Value As String, Index As Integer)
            Me.Type = Type
            Me.Value = Value
            Me.Index = Index
        End Sub
    End Class
End Namespace