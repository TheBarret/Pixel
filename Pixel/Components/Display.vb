Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace Components

    Public Class Display
        Inherits Component
        Public Property Redraw As Boolean
        Private Property Width As UInt16
        Private Property Height As UInt16
        Private Property Offset As UInt16
        Private Property Memory As Byte()
        Private Property Background As Brush
        Private Property Foreground As Brush
        Sub New(Parent As Processor, Width As UInt16, Height As UInt16)
            MyBase.New(Parent)
            Me.Mode(Width, Height, 4)
            Me.Background = New SolidBrush(Color.Black)
            Me.Foreground = New SolidBrush(Color.WhiteSmoke)
        End Sub
        Public Sub Mode(w As UInt16, h As UInt16, Optional Offset As UInt16 = 0)
            Me.Offset = Offset
            Me.Width = w
            Me.Height = h
            Me.Memory = New Byte(Width * Height) {}
            Me.Redraw = True
        End Sub
        Public Sub Allocate(x As Integer, y As Integer, buffer As Byte())
            SyncLock Me.Memory
                Dim px As Integer = x, py As Integer = y, before As Byte, after As Byte, update As Boolean = True
                Me.Parent.Collision = &H0
                For i As Integer = 0 To buffer.Length - 1
                    For j As Integer = 0 To 7
                        If Me.BitsToChar(buffer(i))(j) = "1"c Then
                            If (px + j >= 0 And px + j <= Me.Width) And (py + i >= 0 And py + i <= Me.Height) Then
                                If px + j < Me.Width AndAlso py + i < Me.Height Then
                                    before = Me.VRam(px + j, py + i)
                                    If Me.VRam(px + j, py + i) = &H1 Then update = False
                                    Me.VRam(px + j, py + i) = CByte(Me.VRam(px + j, py + i) Xor &H1)
                                    after = Me.VRam(px + j, py + i)
                                    If (before = 1 AndAlso after = 0) Then Me.Parent.Collision = &H1
                                End If
                            End If
                        End If
                    Next
                Next
                If (update) Then Me.Redraw = True
            End SyncLock
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
                            brush = If(Me.VRam(x, y) = &H0, Me.Background, Me.Foreground)
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
                SyncLock Me.Memory
                    For y As Integer = 0 To Me.Height - 1
                        Select Case Direction
                            Case &H0
                                For x As Integer = 0 To Me.Width - 1
                                    If (y + Value < Me.Height) Then
                                        vcopy(x, y + Value Mod Me.Height) = Me.VRam(x, y)
                                    End If
                                Next
                            Case &H1
                                For x As Integer = 0 To Me.Width - 1
                                    If (y - Value > 0) Then
                                        vcopy(x, y - Value Mod Me.Height) = Me.VRam(x, y)
                                    End If
                                Next
                            Case &H2
                                For x As Integer = 0 To Me.Width - 1
                                    If ((x + Value) < Me.Width) Then
                                        vcopy(x + Value Mod Me.Width, y) = Me.VRam(x, y)
                                    End If
                                Next
                            Case &H3
                                For x As Integer = 0 To Me.Width - 1
                                    If ((x + Value) < Me.Width) Then
                                        vcopy(x, y) = Me.VRam(x + Value Mod Me.Width, y)
                                    End If
                                Next
                        End Select
                    Next
                    For y As Integer = 0 To Me.Height - 1
                        For x As Integer = 0 To Me.Width - 1
                            Me.VRam(x, y) = vcopy(x, y)
                        Next
                    Next
                End SyncLock
            Finally
                Erase vcopy
            End Try
        End Sub
        Public Sub Clear()
            SyncLock Me.Memory
                For y As Integer = 0 To Me.Height - 1
                    For x As Integer = 0 To Me.Width - 1
                        Me.VRam(x, y) = &H0
                    Next
                Next
                Me.Redraw = True
            End SyncLock
        End Sub
        Private Property VRam(x As Int32, y As Int32) As Byte
            Get
                Return Me.Memory(CUShort((y * Me.Width) + x))
            End Get
            Set(value As Byte)
                Me.Memory(CUShort((y * Me.Width) + x)) = value
            End Set
        End Property
        Private Function BitsToChar(value As UInt16) As Char()
            Return Convert.ToString(value, 2).PadLeft(8, "0"c).ToCharArray()
        End Function
    End Class
End Namespace