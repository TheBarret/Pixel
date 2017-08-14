Public Class SyntaxError
    Inherits Exception
    Public Property Index As Integer
    Public Property Length As Integer
    Sub New(Index As Integer, Length As Integer, Message As String)
        MyBase.New(Message)
        Me.Index = Index
        Me.Length = Length
    End Sub
End Class
