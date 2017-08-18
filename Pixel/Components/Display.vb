Imports System.Drawing
Imports System.Drawing.Drawing2D
Namespace Components
    Public Class Display
        Inherits Component
        Public Property Redraw As Boolean
        Private Property Width As UInt16
        Private Property Height As UInt16
        Private Property Offset As UInt16
        Private Property Background As Brush
        Private Property Foreground As Brush
        Sub New(Parent As Processor, Width As UInt16, Height As UInt16)
            MyBase.New(Parent)
            Me.Width = Width
            Me.Height = Height
            Me.Offset = 4
            Me.Redraw = False
            Me.Background = New SolidBrush(Color.White)
            Me.Foreground = New SolidBrush(Color.Red)
        End Sub
        Public Sub Allocate(x As Integer, y As Integer, buffer As Byte())
            Dim px As Integer = x, py As Integer = y, before As Byte, after As Byte, update As Boolean = True
            Me.Parent.Collision = &H0
            For i As Integer = 0 To buffer.Length - 1
                For j As Integer = 0 To 7
                    If Me.BitsToChar(buffer(i))(j) = "1"c Then
                        If (px + j >= 0 And px + j <= Me.Width) And (py + i >= 0 And py + i <= Me.Height) Then
                            If px + j < Me.Width AndAlso py + i < Me.Height Then
                                before = Me.Memory(px + j, py + i)
                                If Me.Memory(px + j, py + i) = &H1 Then update = False
                                Me.Memory(px + j, py + i) = CByte(Me.Memory(px + j, py + i) Xor &H1)
                                after = Me.Memory(px + j, py + i)
                                If (before = 1 AndAlso after = 0) Then Me.Parent.Collision = &H1
                            End If
                        End If
                    End If
                Next
            Next
            If (update) Then Me.Redraw = True
        End Sub
        Public Sub Refresh()
            Using bm As New Bitmap(Me.Width * Me.Offset, Me.Height * Me.Offset)
                Using g As Graphics = Graphics.FromImage(bm)
                    g.SmoothingMode = SmoothingMode.None
                    g.CompositingMode = CompositingMode.SourceCopy
                    g.PixelOffsetMode = PixelOffsetMode.Half
                    g.InterpolationMode = InterpolationMode.NearestNeighbor
                    Dim pixel As Integer = 0, brush As Brush
                    For y As Integer = 0 To Me.Height - 1
                        For x As Integer = 0 To Me.Width - 1
                            brush = If(Me.Memory(x, y) = &H0, Me.Background, Me.Foreground)
                            g.FillRectangle(brush, x * Me.Offset, y * Me.Offset, Me.Offset, Me.Offset)
                        Next
                    Next
                End Using
                Me.Parent.UpdateViewport(CType(bm.Clone, Image))
            End Using
            Me.Redraw = False
        End Sub
        Public Sub Shift(Direction As UInt16, Value As UInt16)
            Dim vcopy(,) As Byte = New Byte(Me.Width, Me.Height) {}
            Try
                For y As Integer = 0 To Me.Height - 1
                    Select Case Direction
                        Case &H0
                            For x As Integer = 0 To Me.Width - 1
                                If (y + Value < Me.Height) Then
                                    vcopy(x, y + Value Mod Me.Height) = Me.Memory(x, y)
                                End If
                            Next
                        Case &H1
                            For x As Integer = 0 To Me.Width - 1
                                If (y - Value > 0) Then
                                    vcopy(x, y - Value Mod Me.Height) = Me.Memory(x, y)
                                End If
                            Next
                        Case &H2
                            For x As Integer = 0 To Me.Width - 1
                                If ((x + Value) < Me.Width) Then
                                    vcopy(x + Value Mod Me.Width, y) = Me.Memory(x, y)
                                End If
                            Next
                        Case &H3
                            For x As Integer = 0 To Me.Width - 1
                                If ((x + Value) < Me.Width) Then
                                    vcopy(x, y) = Me.Memory(x + Value Mod Me.Width, y)
                                End If
                            Next
                    End Select
                Next
                For y As Integer = 0 To Me.Height - 1
                    For x As Integer = 0 To Me.Width - 1
                        Me.Memory(x, y) = vcopy(x, y)
                    Next
                Next
            Finally
                Erase vcopy
            End Try
        End Sub
        Public Sub Clear()
            For y As Integer = 0 To Me.Height - 1
                For x As Integer = 0 To Me.Width - 1
                    Me.Memory(x, y) = &H0
                Next
            Next
            Me.Redraw = True
        End Sub
        Private Property Memory(x As Integer, y As Integer) As Byte
            Get
                Return Me.Parent.VRam(x, y, Me.Width)
            End Get
            Set(value As Byte)
                Me.Parent.VRam(x, y, Me.Width) = value
            End Set
        End Property
        Private Function BitsToChar(value As UInt16) As Char()
            Return Convert.ToString(value, 2).PadLeft(8, "0"c).ToCharArray()
        End Function
    End Class
End Namespace