<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdCompile = New System.Windows.Forms.Button()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.TabContainer = New System.Windows.Forms.TabControl()
        Me.tabEditor = New System.Windows.Forms.TabPage()
        Me.tbOutput = New System.Windows.Forms.TextBox()
        Me.Usercode = New System.Windows.Forms.RichTextBox()
        Me.Viewport = New Pixel.Viewport()
        Me.TabContainer.SuspendLayout()
        Me.tabEditor.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCompile
        '
        Me.cmdCompile.Location = New System.Drawing.Point(378, 165)
        Me.cmdCompile.Name = "cmdCompile"
        Me.cmdCompile.Size = New System.Drawing.Size(91, 26)
        Me.cmdCompile.TabIndex = 1
        Me.cmdCompile.Text = "Compile"
        Me.cmdCompile.UseVisualStyleBackColor = True
        '
        'cmdStop
        '
        Me.cmdStop.Enabled = False
        Me.cmdStop.Location = New System.Drawing.Point(378, 197)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(91, 26)
        Me.cmdStop.TabIndex = 4
        Me.cmdStop.Text = "Stop"
        Me.cmdStop.UseVisualStyleBackColor = True
        '
        'TabContainer
        '
        Me.TabContainer.Controls.Add(Me.tabEditor)
        Me.TabContainer.Location = New System.Drawing.Point(12, 229)
        Me.TabContainer.Name = "TabContainer"
        Me.TabContainer.SelectedIndex = 0
        Me.TabContainer.Size = New System.Drawing.Size(457, 446)
        Me.TabContainer.TabIndex = 5
        '
        'tabEditor
        '
        Me.tabEditor.Controls.Add(Me.tbOutput)
        Me.tabEditor.Controls.Add(Me.Usercode)
        Me.tabEditor.Location = New System.Drawing.Point(4, 22)
        Me.tabEditor.Name = "tabEditor"
        Me.tabEditor.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEditor.Size = New System.Drawing.Size(449, 420)
        Me.tabEditor.TabIndex = 0
        Me.tabEditor.Text = "Editor"
        Me.tabEditor.UseVisualStyleBackColor = True
        '
        'tbOutput
        '
        Me.tbOutput.BackColor = System.Drawing.Color.White
        Me.tbOutput.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbOutput.Location = New System.Drawing.Point(6, 356)
        Me.tbOutput.Multiline = True
        Me.tbOutput.Name = "tbOutput"
        Me.tbOutput.ReadOnly = True
        Me.tbOutput.Size = New System.Drawing.Size(437, 58)
        Me.tbOutput.TabIndex = 4
        '
        'Usercode
        '
        Me.Usercode.AcceptsTab = True
        Me.Usercode.DetectUrls = False
        Me.Usercode.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Usercode.Location = New System.Drawing.Point(6, 3)
        Me.Usercode.Name = "Usercode"
        Me.Usercode.Size = New System.Drawing.Size(437, 347)
        Me.Usercode.TabIndex = 3
        Me.Usercode.Text = ""
        Me.Usercode.WordWrap = False
        '
        'Viewport
        '
        Me.Viewport.BackColor = System.Drawing.Color.Black
        Me.Viewport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Viewport.Location = New System.Drawing.Point(12, 12)
        Me.Viewport.Name = "Viewport"
        Me.Viewport.Size = New System.Drawing.Size(360, 211)
        Me.Viewport.TabIndex = 6
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 680)
        Me.Controls.Add(Me.Viewport)
        Me.Controls.Add(Me.TabContainer)
        Me.Controls.Add(Me.cmdStop)
        Me.Controls.Add(Me.cmdCompile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pixel Assembly Editor"
        Me.TabContainer.ResumeLayout(False)
        Me.tabEditor.ResumeLayout(False)
        Me.tabEditor.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdCompile As System.Windows.Forms.Button
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents TabContainer As System.Windows.Forms.TabControl
    Friend WithEvents tabEditor As System.Windows.Forms.TabPage
    Friend WithEvents tbOutput As System.Windows.Forms.TextBox
    Friend WithEvents Usercode As System.Windows.Forms.RichTextBox
    Friend WithEvents Viewport As Pixel.Viewport

End Class
