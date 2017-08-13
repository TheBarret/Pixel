Imports System.Threading
Public Class Cycle
    Public Property Timer As Stopwatch
    Sub New()
        Me.Timer = New Stopwatch
    End Sub
    Public Sub Begin()
        If (Not Me.Timer.IsRunning) Then
            Me.Timer.Reset()
            Me.Timer.Start()
        End If
    End Sub
    Public Function Throttle(Framerate As Integer) As Boolean
        If (Me.Timer.ElapsedMilliseconds >= 1000 / Framerate) Then
            Me.Timer.Restart()
            Return True
        End If
        Return False
    End Function
    Public Overrides Function ToString() As String
        Return String.Format("Cycle: {0}ms", Me.Timer.ElapsedMilliseconds)
    End Function
End Class