Imports System.Drawing
Imports System.Windows.Forms

Public Class Visualizer
    Public Property Offset As Integer
    Private Property Lock As Object
    Private Property Width As Integer
    Private Property Height As Integer
    Private Property Viewport As Control
    Private Property Font As Font
    Sub New(Viewport As Control, Length As Integer, Offset As UInt16)
        Me.Width = Length \ 2
        Me.Height = Length \ 2
        Me.Offset = Offset
        Me.Viewport = Viewport
        Me.Lock = New Object
        Me.Font = New Font("Consolas", 7)
    End Sub

    Public Sub Clear()
        If (Me.Viewport.InvokeRequired) Then
            Me.Viewport.Invoke(Sub() Me.Clear())
        Else
            SyncLock Me.Lock
                Me.Viewport.BackgroundImage = Nothing
            End SyncLock
        End If
    End Sub

    Public Sub Update(Memory() As Byte)
        If (Memory IsNot Nothing) Then
            If (Me.Viewport.InvokeRequired) Then
                Me.Viewport.Invoke(Sub() Me.Update(Memory))
            Else
                SyncLock Me.Lock
                    Using bm As New Bitmap(Me.Viewport.Width, Me.Viewport.Height)
                        Using g As Graphics = Graphics.FromImage(bm)
                            Dim address As Integer = Offset, value As Integer = 0
                            If (address <= Memory.Length - 1) Then
                                For y As Integer = 0 To bm.Height - 1 Step (bm.Height \ Me.Height)
                                    For x As Integer = 0 To bm.Width - 1 Step (bm.Width \ Me.Width)
                                        If (Memory(address) <> 0) Then
                                            value = Memory(address) Xor 255
                                            g.FillRectangle(New SolidBrush(Color.FromArgb(0, value, value)), x, y, (bm.Width \ Me.Width), (bm.Height \ Me.Height))
                                            g.DrawString(Memory(address).ToString("X"), Me.Font, Brushes.Black, x, y - 1)
                                            g.DrawRectangle(Pens.Black, x, y, (bm.Width \ Me.Width), (bm.Height \ Me.Height))
                                        End If
                                        address += 1

                                    Next
                                Next
                            End If
                        End Using
                        Me.Viewport.BackgroundImage = CType(bm.Clone, Image)
                    End Using
                End SyncLock
            End If
        End If
    End Sub

End Class