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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.cmdCompile = New System.Windows.Forms.Button()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.TabContainer = New System.Windows.Forms.TabControl()
        Me.tabEditor = New System.Windows.Forms.TabPage()
        Me.lbSel1 = New System.Windows.Forms.Label()
        Me.Usercode = New System.Windows.Forms.RichTextBox()
        Me.tabDisplay = New System.Windows.Forms.TabPage()
        Me.tabTools = New System.Windows.Forms.TabPage()
        Me.cmdCopySprite = New System.Windows.Forms.Button()
        Me.cmdResetButtons = New System.Windows.Forms.Button()
        Me.tbSpriteArray = New System.Windows.Forms.TextBox()
        Me.tbOutput = New System.Windows.Forms.TextBox()
        Me.lbFps = New System.Windows.Forms.Label()
        Me.Viewport = New Pixel.Viewport()
        Me.TabContainer.SuspendLayout()
        Me.tabEditor.SuspendLayout()
        Me.tabDisplay.SuspendLayout()
        Me.tabTools.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCompile
        '
        Me.cmdCompile.Location = New System.Drawing.Point(441, 25)
        Me.cmdCompile.Name = "cmdCompile"
        Me.cmdCompile.Size = New System.Drawing.Size(91, 26)
        Me.cmdCompile.TabIndex = 1
        Me.cmdCompile.Text = "Compile"
        Me.cmdCompile.UseVisualStyleBackColor = True
        '
        'cmdStop
        '
        Me.cmdStop.Enabled = False
        Me.cmdStop.Location = New System.Drawing.Point(538, 25)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(91, 26)
        Me.cmdStop.TabIndex = 4
        Me.cmdStop.Text = "Stop"
        Me.cmdStop.UseVisualStyleBackColor = True
        '
        'TabContainer
        '
        Me.TabContainer.Controls.Add(Me.tabEditor)
        Me.TabContainer.Controls.Add(Me.tabDisplay)
        Me.TabContainer.Controls.Add(Me.tabTools)
        Me.TabContainer.Location = New System.Drawing.Point(4, 57)
        Me.TabContainer.Name = "TabContainer"
        Me.TabContainer.SelectedIndex = 0
        Me.TabContainer.Size = New System.Drawing.Size(629, 543)
        Me.TabContainer.TabIndex = 5
        '
        'tabEditor
        '
        Me.tabEditor.Controls.Add(Me.lbSel1)
        Me.tabEditor.Controls.Add(Me.Usercode)
        Me.tabEditor.Location = New System.Drawing.Point(4, 22)
        Me.tabEditor.Name = "tabEditor"
        Me.tabEditor.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEditor.Size = New System.Drawing.Size(621, 517)
        Me.tabEditor.TabIndex = 0
        Me.tabEditor.Text = "Editor"
        Me.tabEditor.UseVisualStyleBackColor = True
        '
        'lbSel1
        '
        Me.lbSel1.AutoSize = True
        Me.lbSel1.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSel1.Location = New System.Drawing.Point(1, 7)
        Me.lbSel1.Name = "lbSel1"
        Me.lbSel1.Size = New System.Drawing.Size(0, 13)
        Me.lbSel1.TabIndex = 4
        '
        'Usercode
        '
        Me.Usercode.AcceptsTab = True
        Me.Usercode.DetectUrls = False
        Me.Usercode.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Usercode.Location = New System.Drawing.Point(2, 23)
        Me.Usercode.Name = "Usercode"
        Me.Usercode.Size = New System.Drawing.Size(613, 490)
        Me.Usercode.TabIndex = 3
        Me.Usercode.Text = ""
        Me.Usercode.WordWrap = False
        '
        'tabDisplay
        '
        Me.tabDisplay.Controls.Add(Me.lbFps)
        Me.tabDisplay.Controls.Add(Me.Viewport)
        Me.tabDisplay.Location = New System.Drawing.Point(4, 22)
        Me.tabDisplay.Name = "tabDisplay"
        Me.tabDisplay.Size = New System.Drawing.Size(621, 517)
        Me.tabDisplay.TabIndex = 1
        Me.tabDisplay.Text = "Display"
        Me.tabDisplay.UseVisualStyleBackColor = True
        '
        'tabTools
        '
        Me.tabTools.Controls.Add(Me.cmdCopySprite)
        Me.tabTools.Controls.Add(Me.cmdResetButtons)
        Me.tabTools.Controls.Add(Me.tbSpriteArray)
        Me.tabTools.Location = New System.Drawing.Point(4, 22)
        Me.tabTools.Name = "tabTools"
        Me.tabTools.Size = New System.Drawing.Size(621, 517)
        Me.tabTools.TabIndex = 2
        Me.tabTools.Text = "Tools"
        Me.tabTools.UseVisualStyleBackColor = True
        '
        'cmdCopySprite
        '
        Me.cmdCopySprite.Location = New System.Drawing.Point(365, 109)
        Me.cmdCopySprite.Name = "cmdCopySprite"
        Me.cmdCopySprite.Size = New System.Drawing.Size(57, 51)
        Me.cmdCopySprite.TabIndex = 50
        Me.cmdCopySprite.Text = "Copy"
        Me.cmdCopySprite.UseVisualStyleBackColor = True
        '
        'cmdResetButtons
        '
        Me.cmdResetButtons.Location = New System.Drawing.Point(365, 40)
        Me.cmdResetButtons.Name = "cmdResetButtons"
        Me.cmdResetButtons.Size = New System.Drawing.Size(57, 51)
        Me.cmdResetButtons.TabIndex = 49
        Me.cmdResetButtons.Text = "Reset"
        Me.cmdResetButtons.UseVisualStyleBackColor = True
        '
        'tbSpriteArray
        '
        Me.tbSpriteArray.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.tbSpriteArray.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSpriteArray.Location = New System.Drawing.Point(224, 40)
        Me.tbSpriteArray.Multiline = True
        Me.tbSpriteArray.Name = "tbSpriteArray"
        Me.tbSpriteArray.ReadOnly = True
        Me.tbSpriteArray.Size = New System.Drawing.Size(135, 120)
        Me.tbSpriteArray.TabIndex = 48
        '
        'tbOutput
        '
        Me.tbOutput.BackColor = System.Drawing.Color.White
        Me.tbOutput.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbOutput.Location = New System.Drawing.Point(4, 606)
        Me.tbOutput.Multiline = True
        Me.tbOutput.Name = "tbOutput"
        Me.tbOutput.ReadOnly = True
        Me.tbOutput.Size = New System.Drawing.Size(629, 69)
        Me.tbOutput.TabIndex = 4
        '
        'lbFps
        '
        Me.lbFps.AutoSize = True
        Me.lbFps.Location = New System.Drawing.Point(43, 281)
        Me.lbFps.Name = "lbFps"
        Me.lbFps.Size = New System.Drawing.Size(69, 13)
        Me.lbFps.TabIndex = 7
        Me.lbFps.Text = "Framerate: ..."
        '
        'Viewport
        '
        Me.Viewport.BackColor = System.Drawing.Color.Black
        Me.Viewport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Viewport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Viewport.Location = New System.Drawing.Point(46, 20)
        Me.Viewport.Name = "Viewport"
        Me.Viewport.Size = New System.Drawing.Size(516, 258)
        Me.Viewport.TabIndex = 6
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 680)
        Me.Controls.Add(Me.tbOutput)
        Me.Controls.Add(Me.cmdCompile)
        Me.Controls.Add(Me.TabContainer)
        Me.Controls.Add(Me.cmdStop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pixel Assembly Editor"
        Me.TabContainer.ResumeLayout(False)
        Me.tabEditor.ResumeLayout(False)
        Me.tabEditor.PerformLayout()
        Me.tabDisplay.ResumeLayout(False)
        Me.tabDisplay.PerformLayout()
        Me.tabTools.ResumeLayout(False)
        Me.tabTools.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdCompile As System.Windows.Forms.Button
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents TabContainer As System.Windows.Forms.TabControl
    Friend WithEvents tabEditor As System.Windows.Forms.TabPage
    Friend WithEvents tbOutput As System.Windows.Forms.TextBox
    Friend WithEvents Usercode As System.Windows.Forms.RichTextBox
    Friend WithEvents Viewport As Pixel.Viewport
    Friend WithEvents tabDisplay As System.Windows.Forms.TabPage
    Friend WithEvents tabTools As System.Windows.Forms.TabPage
    Friend WithEvents tbSpriteArray As System.Windows.Forms.TextBox
    Friend WithEvents cmdResetButtons As System.Windows.Forms.Button
    Friend WithEvents cmdCopySprite As System.Windows.Forms.Button
    Friend WithEvents lbSel1 As System.Windows.Forms.Label
    Friend WithEvents lbFps As System.Windows.Forms.Label

End Class
