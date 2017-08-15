Imports System.Threading
Imports System.Windows.Forms
Imports Pixel.Components
Imports Pixel.Assembler
Public Class Machine
    Public Event MachineActive()
    Public Event MachineInactive()
    Public Event Failure(Ex As Exception)
    Public Property Running As Boolean
    Private Property FrameRate As Long
    Private Property Check As DateTime
    Private Property Timer As Stopwatch
    Private Property Wait As ManualResetEvent
    Private Property ByteStream As Byte()
    Private Property Processor As Processor
    Protected Friend Property Viewport As Control
    Sub New(Viewport As Control)
        Me.Viewport = Viewport
    End Sub
    Public Sub Compile(Filename As String)
        Try
            Me.ByteStream = New ByteStream(New Lexer(Filename, New Grammars.Pixel).Parse).Compile()
        Catch ex As Exception
            RaiseEvent Failure(ex)
        End Try
    End Sub
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
    Public Function GetFps() As Int64
        Dim current As Int64 = CLng(Interlocked.Exchange(Me.FrameRate, 0) / (DateTime.Now - Me.Check).TotalSeconds)
        Me.Check = DateTime.Now
        Return current
    End Function
    Private Sub Run(Bytecode() As Byte, FrameRate As Integer)
        Try
            Me.Processor = New Processor(Me)
            Processor.WriteBlock(Locations.Entrypoint, Bytecode)
            If (Me.Running) Then
                Me.Running = False
                Me.Wait.WaitOne()
            End If
            Me.FrameRate = 0
            Me.Check = DateTime.Now
            Me.Running = True
            Me.Timer = New Stopwatch
            Me.Wait = New ManualResetEvent(False)
            RaiseEvent MachineActive()
            Do
                If (Not Me.Timer.IsRunning) Then
                    Me.Timer.Reset()
                    Me.Timer.Start()
                End If
                If (Me.Timer.ElapsedMilliseconds >= (1000 / FrameRate)) Then
                    Me.Timer.Restart()
                    Processor.Clock()
                    Interlocked.Increment(Me.FrameRate)
                End If
            Loop While Me.Running
            Memory.Dump(".\Dump.bin", Processor.ReadBlock(&H0, &HFFFF))
            Processor.Dispose()
        Catch ex As Exception
            RaiseEvent Failure(ex)
            Me.Running = False
        Finally
            Me.Wait.Set()
            RaiseEvent MachineInactive()
        End Try
    End Sub
End Class
