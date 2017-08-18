Namespace Components
    Public Interface IComponent
        Property Parent As Processor
    End Interface
    Public Class Component
        Implements IComponent
        Public Property Parent As Processor Implements IComponent.Parent
        Sub New(Parent As Processor)
            Me.Parent = Parent
        End Sub
    End Class
End Namespace