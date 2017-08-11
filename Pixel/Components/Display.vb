Imports System.Drawing
Imports System.Drawing.Drawing2D
Namespace Components
    Public Class Display
        Public Property Redraw As Boolean
        Public Property Screen As Bitmap
        Private Property Width As UInt16
        Private Property Height As UInt16
        Private Property Offset As UInt16
        Private Property buffer As UInt16(,)
        Private Property Background As Brush
        Private Property Foreground As Brush
        Sub New()
            Me.Width = 256 '128
            Me.Height = 128
            Me.Offset = 2
            Me.Redraw = False
            Me.Background = New SolidBrush(Color.CornflowerBlue)
            Me.Foreground = New SolidBrush(Color.Black)
            Me.buffer = New UInt16(Me.Width - 1, Me.Height - 1) {}
        End Sub
        Public Sub Allocate(x As Integer, y As Integer, buffer As Byte())
            Dim px As Integer = x, py As Integer = y
            For i As Integer = 0 To buffer.Length - 1
                For j As Integer = 0 To 7
                    If Me.BitToString(buffer(i))(j) = Char.Parse("1") Then
                        If px + j < Width AndAlso py + i < Height Then
                            Me.buffer(px + j, py + i) = CByte(Me.buffer(px + j, py + i) Xor &H1)
                            Me.Redraw = True
                        End If
                    End If
                Next
            Next
        End Sub
        Public Sub Refresh()
            Using bm As New Bitmap(Me.Width * Me.Offset, Me.Height * Me.Offset)
                Using g As Graphics = Graphics.FromImage(bm)
                    g.CompositingMode = CompositingMode.SourceCopy
                    g.PixelOffsetMode = PixelOffsetMode.Half
                    g.InterpolationMode = InterpolationMode.NearestNeighbor
                    Dim pixel As Integer = 0, brush As Brush
                    For y As Integer = 0 To Me.Height - 1
                        For x As Integer = 0 To Me.Width - 1
                            brush = If(Me.buffer(x, y) = &H0, Me.Background, Me.Foreground)
                            g.FillRectangle(brush, x * Me.Offset, y * Me.Offset, Me.Offset, Me.Offset)
                        Next
                    Next
                End Using
                Me.Screen = CType(bm.Clone, Bitmap)
            End Using
            Me.Redraw = False
        End Sub
        Public Sub Clear()
            For y As Integer = 0 To Me.Height - 1
                For x As Integer = 0 To Me.Width - 1
                    Me.buffer(x, y) = &H0
                Next
            Next
            Me.Redraw = True
        End Sub
        Private Function BitToString(value As Integer) As Char()
            Return Convert.ToString(value, 2).PadLeft(8, "0"c).ToCharArray()
        End Function
    End Class
End Namespace