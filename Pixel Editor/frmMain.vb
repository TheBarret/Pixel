Imports Pixel
Imports System.IO
Imports System.Text
Imports System.Threading

Public Class frmMain
    Private Property ExitLock As Object
    Private Property ExitFlag As Boolean
    Private Property Filename As String
    Private Property Timer As Timers.Timer
    Private Property Machine As Machine
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ExitFlag = False
        Me.ExitLock = New Object
        Me.Machine = New Machine(Me.Viewport)
        AddHandler Me.Machine.Failure, AddressOf Me.Failure
        AddHandler Me.Machine.MachineActive, AddressOf Me.MachineActive
        AddHandler Me.Machine.MachineInactive, AddressOf Me.MachineInactive
        Me.Filename = String.Format("{0}\{1}", Application.StartupPath, "usercode.txt")
        Me.LoadUsercode()
        Me.InitializeSpriteEditor()
    End Sub
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.FlagForShutdown()
        Me.Machine.Abort()
    End Sub
    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (Me.Machine.Running) Then Me.Machine.KeyPressed(e)
        If (e.Control AndAlso e.KeyCode = Windows.Forms.Keys.S) Then Me.SaveUsercode()
    End Sub
    Private Sub frmMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If (Me.Machine.Running) Then Me.Machine.KeyReleased(e)
    End Sub
    Private Sub cmdCompile_Click(sender As Object, e As EventArgs) Handles cmdCompile.Click
        Me.SaveUsercode()

        Me.AddOutputLog("Compiling...", True)
        Me.Machine.Compile(Me.Filename)

        Me.AddOutputLog("Executing...")
        Me.Machine.Start(700)

        Me.TabContainer.SelectedTab = Me.tabDisplay
    End Sub
    Private Sub cmdStop_Click(sender As Object, e As EventArgs) Handles cmdStop.Click
        Me.Machine.Abort()
    End Sub
    Private Sub FpsTick(sender As Object, e As Timers.ElapsedEventArgs)
        Me.UpdateFps(Me.Machine.GetFps)
    End Sub
    Private Sub Failure(ex As Exception)
        Me.AddOutputLog(String.Format("Error: {0}", ex.Message))
    End Sub
    Private Sub MachineActive()
        Me.Timer = New Timers.Timer(1000) With {.Enabled = True}
        AddHandler Me.Timer.Elapsed, AddressOf Me.FpsTick
        Me.Timer.Start()
        Me.SwitchGUI(True)
    End Sub
    Private Sub MachineInactive()
        Me.Timer.Stop()
        RemoveHandler Me.Timer.Elapsed, AddressOf Me.FpsTick
        Me.AddOutputLog("Program has stopped")
        Me.SwitchGUI(False)
    End Sub
    Private Sub FlagForShutdown()
        SyncLock Me.ExitLock
            Me.ExitFlag = True
        End SyncLock
    End Sub
    Private Sub UpdateFps(fps As Int64)
        If (Me.InvokeRequired) Then
            Me.Invoke(Sub() Me.UpdateFps(fps))
        Else
            Me.lbFps.Text = String.Format("Framerate: {0}", fps)
        End If
    End Sub
    Private Sub SwitchGUI(state As Boolean)
        If (Me.InvokeRequired) Then
            Me.Invoke(Sub() Me.SwitchGUI(state))
        Else
            Me.cmdStop.Enabled = state
            Me.cmdCompile.Enabled = Not state
        End If
    End Sub
    Private Sub SaveUsercode()
        File.WriteAllText(Me.Filename, Me.Usercode.Text, Encoding.UTF8)
    End Sub
    Private Sub LoadUsercode()
        If (File.Exists(Me.Filename)) Then
            Me.Usercode.Text = File.ReadAllText(Me.Filename, Encoding.UTF8)
        End If
    End Sub
    Private Sub AddOutputLog(Message As String, Optional Clear As Boolean = False)
        If (Not Me.ExitFlag) Then
            If (Me.InvokeRequired) Then
                Me.Invoke(Sub() Me.AddOutputLog(Message, Clear))
            Else
                If (Clear) Then Me.tbOutput.Clear()
                Me.tbOutput.AppendText(String.Format("{0}{1}", Message, Environment.NewLine))
                Me.tbOutput.SelectionLength = Me.tbOutput.Text.Length
                Me.tbOutput.ScrollToCaret()
            End If
        End If
    End Sub
    Private Sub Usercode_SelectionChanged(sender As Object, e As EventArgs) Handles Usercode.SelectionChanged
        If (Me.Usercode.SelectedText.Length > 0) Then
            Me.lbSel1.Text = String.Format("{0}", Me.Usercode.SelectionLength)
        Else
            Me.lbSel1.Text = String.Empty
        End If
    End Sub
End Class
