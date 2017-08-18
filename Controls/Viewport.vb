Imports System.Windows.Forms
Public Class Viewport
    Inherits Panel
    Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
    End Sub
End Class
