Namespace Interfaces
    Public Interface ITask
        Property Parent As Machine
        Sub Initialize()
        Sub Execute()
    End Interface
    Public MustInherit Class Task
        Implements ITask
        Sub New(Parent As Machine)
            Me.Parent = Parent
        End Sub
        Public MustOverride Sub Initialize() Implements ITask.Initialize
        Public MustOverride Sub Execute() Implements ITask.Execute
        Public Property Parent As Machine Implements ITask.Parent
    End Class
End Namespace