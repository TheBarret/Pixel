Imports System.Threading
Imports System.Windows.Forms
Imports Pixel.Assembler
Imports Pixel.Components

Public Class Machine
    Public Event MachineActive(Sender As Object)
    Public Event MachineInactive(Sender As Object)
    Public Event Failure(Sender As Object, Ex As Exception)
    Public Property Running As Boolean
    Public Property Processor As Processor
    Private Property Frames As Long
    Private Property Check As DateTime
    Private Property Timer As Stopwatch
    Private Property ByteStream As Byte()
    Private Property Viewport As Control
    Private Property Wait As ManualResetEvent
    Sub New(Viewport As Control)
        Me.Viewport = Viewport
    End Sub
    Public Function Compile(Filename As String) As Boolean
        Try
            Using stream As New ByteStream(New Lexer(Filename, New Grammars.Pixel).Parse)
                Me.ByteStream = stream.Compile()
            End Using
            Return True
        Catch ex As Exception
            RaiseEvent Failure(Me, ex)
            Return False
        End Try
    End Function
    Public Sub Start(Framerate As Integer)
        If (Me.ByteStream.Length > 0) Then
            Call New Thread(Sub() Me.Run(Me.ByteStream, Framerate)) With {.IsBackground = True}.Start()
        End If
    End Sub
    Public Sub Abort()
        Me.Running = False
    End Sub
    Public Sub KeyPressed(Params As KeyEventArgs)
        If (Me.Processor IsNot Nothing AndAlso Me.Running) Then
            Me.Processor.Keyboard.PressKey(ChrW(Params.KeyValue))
        End If
    End Sub
    Public Sub KeyReleased(Params As KeyEventArgs)
        If (Me.Processor IsNot Nothing AndAlso Me.Running) Then
            Me.Processor.Keyboard.ReleaseKey(ChrW(Params.KeyValue))
        End If
    End Sub
    Public Function Framerate() As Integer
        Dim current As Integer = CInt(Interlocked.Exchange(Me.Frames, 0) / (DateTime.Now - Me.Check).TotalSeconds)
        Me.Check = DateTime.Now
        Return current
    End Function
    Private Sub Run(Bytecode() As Byte, FrameRate As Integer)
        Try
            Me.Processor = New Processor(Me)
            AddHandler Me.Processor.OnViewportUpdate, AddressOf Me.ViewportUpdate
            Processor.WriteBlock(Locations.Entrypoint, Bytecode)
            If (Me.Running) Then
                Me.Running = False
                Me.Wait.WaitOne()
            End If
            Me.Frames = 0
            Me.Check = DateTime.Now
            Me.Running = True
            Me.Timer = New Stopwatch
            Me.Wait = New ManualResetEvent(False)
            RaiseEvent MachineActive(Me)
            Do
                If (Not Me.Timer.IsRunning) Then
                    Me.Timer.Reset()
                    Me.Timer.Start()
                End If
                If (Me.Timer.ElapsedMilliseconds >= (1000 / FrameRate)) Then
                    Me.Timer.Restart()
                    Processor.Clock()
                    Interlocked.Increment(Me.Frames)
                End If
                If (Me.Processor.Display.Redraw) Then
                    Me.Processor.Display.Refresh()
                End If
            Loop While Me.Running
            Memory.Dump(".\Memory.bin", Processor.ReadBlock(&H0, &HFFFF))
            Processor.Dispose()
            RemoveHandler Me.Processor.OnViewportUpdate, AddressOf Me.ViewportUpdate
        Catch ex As Exception
            RaiseEvent Failure(Me, ex)
            Me.Running = False
        Finally
            Me.Wait.Set()
            RaiseEvent MachineInactive(Me)
        End Try
    End Sub
    Private Sub ViewportUpdate(Sender As Object, im As Drawing.Image)
        SyncLock im
            Me.Viewport.BackgroundImage = im
        End SyncLock
    End Sub

End Class