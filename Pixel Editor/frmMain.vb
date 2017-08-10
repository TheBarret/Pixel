Imports Pixel
Imports System.IO

Public Class frmMain
    Private Property Filename As String
    Private Property Machine As Machine
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Machine = New Machine(Me.Viewport)
        AddHandler Me.Machine.Failure, AddressOf Me.Failure
        AddHandler Me.Machine.MachineActive, AddressOf Me.MachineActive
        AddHandler Me.Machine.MachineInactive, AddressOf Me.MachineInactive
        Me.Filename = String.Format("{0}\{1}", Application.StartupPath, "usercode.txt")
        Me.LoadUsercode()
        Call New Threading.Thread(AddressOf Me.CreateButtonArray).Start()
    End Sub
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Machine.Abort()
    End Sub
    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.Control AndAlso e.KeyCode = Keys.S) Then Me.SaveUsercode()
    End Sub
    Private Sub cmdCompile_Click(sender As Object, e As EventArgs) Handles cmdCompile.Click
        Me.SaveUsercode()
        Me.AddOutputLog("Compiling...", True)
        Me.Machine.Compile(Me.Filename, True)
        Me.TabContainer.SelectedTab = Me.tabDisplay
    End Sub
    Private Sub cmdStop_Click(sender As Object, e As EventArgs) Handles cmdStop.Click
        Me.Machine.Abort()
    End Sub
    Private Sub Failure(ex As Exception)
        Me.AddOutputLog(String.Format("Error: {0}", ex.Message))
    End Sub
    Private Sub MachineActive()
        Me.AddOutputLog("Executing...")
        Me.SwitchGUI(True)
    End Sub
    Private Sub MachineInactive()
        Me.AddOutputLog("Stopped")
        Me.SwitchGUI(False)
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
        Me.Usercode.SaveFile(Me.Filename, RichTextBoxStreamType.PlainText)
    End Sub
    Private Sub LoadUsercode()
        If (File.Exists(Me.Filename)) Then Me.Usercode.LoadFile(Me.Filename, RichTextBoxStreamType.PlainText)
    End Sub
    Private Sub AddOutputLog(Message As String, Optional Clear As Boolean = False)
        If (Me.InvokeRequired) Then
            Me.Invoke(Sub() Me.AddOutputLog(Message, Clear))
        Else
            If (Clear) Then Me.tbOutput.Clear()
            Me.tbOutput.AppendText(String.Format("{0}{1}", Message, Environment.NewLine))
            Me.tbOutput.SelectionLength = Me.tbOutput.Text.Length
            Me.tbOutput.ScrollToCaret()
        End If
    End Sub
End Class
