Imports Pixel
Imports System.IO
Imports System.Text
Imports System.Threading
Imports Timer = System.Timers.Timer

Public Class frmMain
    Private Property Timer As Timer
    Private Property Vism As Visualizer
    Private Property Machine As Machine
    Private Property ExitLock As Object
    Private Property ExitFlag As Boolean
    Private Property Filename As String

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ExitFlag = False
        Me.ExitLock = New Object
        Me.Vism = New Visualizer(Me.vpVmem, 64, 0)
        Me.Machine = New Machine(Me.Viewport)
        AddHandler Me.Machine.Failure, AddressOf Me.Failure
        AddHandler Me.Machine.MachineActive, AddressOf Me.MachineActive
        AddHandler Me.Machine.MachineInactive, AddressOf Me.MachineInactive
        Me.Filename = String.Format("{0}\{1}", Application.StartupPath, "usercode.tmp")
        Me.LoadUsercode()
        Me.InitializeSpriteEditor()
        Me.PopulateDropdownList()
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.FlagForShutdown()
        Me.Machine.Abort()
    End Sub

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (Me.Machine.Running) Then Me.Machine.KeyPressed(e)
        If (e.Control AndAlso e.KeyCode = Windows.Forms.Keys.S) Then
            Me.SaveUsercode()
        End If
    End Sub

    Private Sub frmMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If (Me.Machine.Running) Then Me.Machine.KeyReleased(e)
    End Sub

    Private Sub stripBtnStart_Click(sender As Object, e As EventArgs) Handles stripBtnStart.Click
        Me.SaveUsercode()
        Me.AddOutputLog("Compiling...", True)
        If (Me.Machine.Compile(Me.Filename)) Then
            Me.TabContainer.SelectedTab = Me.tabDisplay
            Me.AddOutputLog("Executing...")
            Me.Machine.Start(100000)
        End If
    End Sub

    Private Sub stripBtnStop_Click(sender As Object, e As EventArgs) Handles stripBtnStop.Click
        Me.Machine.Abort()
        Me.lbStripFramerate.Text = "Framerate: 0"
    End Sub

    Private Sub cbFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFiles.SelectedIndexChanged
        Dim filename As String = Me.cbFiles.Items(Me.cbFiles.SelectedIndex).ToString
        If (File.Exists(String.Format(".\{0}", filename))) Then
            Me.Usercode.Text = File.ReadAllText(String.Format(".\{0}", filename), Encoding.UTF8)
            Me.Usercode.Focus()
        End If
    End Sub

    Private Sub Usercode_SelectionChanged(sender As Object, e As EventArgs) Handles Usercode.SelectionChanged
        If (Me.Usercode.SelectedText.Length > 0) Then
            Me.lbStripSelected.Text = String.Format("Selected: {0}", Me.Usercode.SelectionLength)
        Else
            Me.lbStripSelected.Text = "Selected: 0"
        End If
    End Sub

    Private Sub tbOffset_Scroll(sender As Object, e As EventArgs) Handles tbOffset.Scroll
        Me.Vism.Offset = Me.tbOffset.Value
    End Sub

    Private Sub FpsTick(sender As Object, e As Timers.ElapsedEventArgs)
        Me.UpdateFps(Me.Machine.Framerate)
        Call New Thread(Sub() Me.Vism.Update(Me.Machine.Processor.Memory)) With {.IsBackground = True}.Start()
    End Sub

    Private Sub Failure(Sender As Object, ex As Exception)
        Me.AddOutputLog(String.Format("Error: {0}", ex.Message))
    End Sub

    Private Sub MachineActive(Sender As Object)
        Me.Timer = New Timers.Timer(125) With {.Enabled = True}
        AddHandler Me.Timer.Elapsed, AddressOf Me.FpsTick
        Me.Timer.Start()
        Me.SwitchGUI(True)
    End Sub

    Private Sub MachineInactive(Sender As Object)
        Me.Timer.Stop()
        RemoveHandler Me.Timer.Elapsed, AddressOf Me.FpsTick
        Me.AddOutputLog("Program has stopped")
        Me.SwitchGUI(False)
        Me.Vism.Clear()
    End Sub

    Private Sub FlagForShutdown()
        SyncLock Me.ExitLock
            Me.ExitFlag = True
        End SyncLock
    End Sub

    Private Sub UpdateFps(fps As Int64)
        If (Me.ExitFlag) Then Return
        If (Me.InvokeRequired) Then
            Me.Invoke(Sub() Me.UpdateFps(fps))
        Else
            Me.lbStripFramerate.Text = String.Format("Framerate: {0}", fps)
        End If
    End Sub

    Private Sub SwitchGUI(state As Boolean)
        If (Me.ExitFlag) Then Return
        If (Me.InvokeRequired) Then
            Me.Invoke(Sub() Me.SwitchGUI(state))
        Else
            Me.tbOffset.Enabled = state
            Me.stripBtnStop.Enabled = state
            Me.stripBtnStart.Enabled = Not state
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
        If (Me.ExitFlag) Then Return
        If (Me.InvokeRequired) Then
            Me.Invoke(Sub() Me.AddOutputLog(Message, Clear))
        Else
            If (Clear) Then Me.tbOutput.Clear()
            Me.tbOutput.AppendText(String.Format("{0}{1}", Message, Environment.NewLine))
            Me.tbOutput.SelectionLength = Me.tbOutput.Text.Length
            Me.tbOutput.ScrollToCaret()
        End If
    End Sub

    Private Sub PopulateDropdownList()
        Me.cbFiles.Items.Clear()
        For Each File As String In Directory.GetFiles(".\", "*.asm")
            Me.cbFiles.Items.Add(Path.GetFileName(File))
        Next
    End Sub

End Class