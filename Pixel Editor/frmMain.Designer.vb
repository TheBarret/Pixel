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
        Me.TabContainer = New System.Windows.Forms.TabControl()
        Me.tabEditor = New System.Windows.Forms.TabPage()
        Me.cbFiles = New System.Windows.Forms.ComboBox()
        Me.Usercode = New System.Windows.Forms.RichTextBox()
        Me.tabDisplay = New System.Windows.Forms.TabPage()
        Me.tabMemory = New System.Windows.Forms.TabPage()
        Me.tbOffset = New System.Windows.Forms.TrackBar()
        Me.tabTools = New System.Windows.Forms.TabPage()
        Me.cmdCopySprite = New System.Windows.Forms.Button()
        Me.cmdResetButtons = New System.Windows.Forms.Button()
        Me.tbSpriteArray = New System.Windows.Forms.TextBox()
        Me.tbOutput = New System.Windows.Forms.TextBox()
        Me.Strip = New System.Windows.Forms.StatusStrip()
        Me.lbStripSelected = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lbStripFramerate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.stripBtnStop = New System.Windows.Forms.ToolStripButton()
        Me.stripBtnStart = New System.Windows.Forms.ToolStripButton()
        Me.Viewport = New Pixel.Viewport()
        Me.vpVmem = New Pixel.Viewport()
        Me.TabContainer.SuspendLayout()
        Me.tabEditor.SuspendLayout()
        Me.tabDisplay.SuspendLayout()
        Me.tabMemory.SuspendLayout()
        CType(Me.tbOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabTools.SuspendLayout()
        Me.Strip.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabContainer
        '
        Me.TabContainer.Controls.Add(Me.tabEditor)
        Me.TabContainer.Controls.Add(Me.tabDisplay)
        Me.TabContainer.Controls.Add(Me.tabMemory)
        Me.TabContainer.Controls.Add(Me.tabTools)
        Me.TabContainer.Location = New System.Drawing.Point(4, 42)
        Me.TabContainer.Name = "TabContainer"
        Me.TabContainer.SelectedIndex = 0
        Me.TabContainer.Size = New System.Drawing.Size(629, 543)
        Me.TabContainer.TabIndex = 5
        '
        'tabEditor
        '
        Me.tabEditor.Controls.Add(Me.cbFiles)
        Me.tabEditor.Controls.Add(Me.Usercode)
        Me.tabEditor.Location = New System.Drawing.Point(4, 22)
        Me.tabEditor.Name = "tabEditor"
        Me.tabEditor.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEditor.Size = New System.Drawing.Size(621, 517)
        Me.tabEditor.TabIndex = 0
        Me.tabEditor.Text = "Editor"
        Me.tabEditor.UseVisualStyleBackColor = True
        '
        'cbFiles
        '
        Me.cbFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFiles.FormattingEnabled = True
        Me.cbFiles.Location = New System.Drawing.Point(401, 10)
        Me.cbFiles.Name = "cbFiles"
        Me.cbFiles.Size = New System.Drawing.Size(214, 21)
        Me.cbFiles.TabIndex = 5
        '
        'Usercode
        '
        Me.Usercode.AcceptsTab = True
        Me.Usercode.AutoWordSelection = True
        Me.Usercode.DetectUrls = False
        Me.Usercode.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Usercode.Location = New System.Drawing.Point(2, 37)
        Me.Usercode.Name = "Usercode"
        Me.Usercode.Size = New System.Drawing.Size(613, 476)
        Me.Usercode.TabIndex = 3
        Me.Usercode.Text = ""
        Me.Usercode.WordWrap = False
        '
        'tabDisplay
        '
        Me.tabDisplay.Controls.Add(Me.Viewport)
        Me.tabDisplay.Location = New System.Drawing.Point(4, 22)
        Me.tabDisplay.Name = "tabDisplay"
        Me.tabDisplay.Size = New System.Drawing.Size(621, 517)
        Me.tabDisplay.TabIndex = 1
        Me.tabDisplay.Text = "Display"
        Me.tabDisplay.UseVisualStyleBackColor = True
        '
        'tabMemory
        '
        Me.tabMemory.Controls.Add(Me.tbOffset)
        Me.tabMemory.Controls.Add(Me.vpVmem)
        Me.tabMemory.Location = New System.Drawing.Point(4, 22)
        Me.tabMemory.Name = "tabMemory"
        Me.tabMemory.Size = New System.Drawing.Size(621, 517)
        Me.tabMemory.TabIndex = 3
        Me.tabMemory.Text = "Visualization"
        Me.tabMemory.UseVisualStyleBackColor = True
        '
        'tbOffset
        '
        Me.tbOffset.Enabled = False
        Me.tbOffset.LargeChange = 64
        Me.tbOffset.Location = New System.Drawing.Point(6, 318)
        Me.tbOffset.Maximum = 4096
        Me.tbOffset.Name = "tbOffset"
        Me.tbOffset.Size = New System.Drawing.Size(609, 42)
        Me.tbOffset.SmallChange = 64
        Me.tbOffset.TabIndex = 11
        Me.tbOffset.TickFrequency = 512
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
        Me.tbOutput.Location = New System.Drawing.Point(4, 591)
        Me.tbOutput.Multiline = True
        Me.tbOutput.Name = "tbOutput"
        Me.tbOutput.ReadOnly = True
        Me.tbOutput.Size = New System.Drawing.Size(629, 69)
        Me.tbOutput.TabIndex = 4
        '
        'Strip
        '
        Me.Strip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbStripSelected, Me.lbStripFramerate})
        Me.Strip.Location = New System.Drawing.Point(0, 664)
        Me.Strip.Name = "Strip"
        Me.Strip.Size = New System.Drawing.Size(638, 22)
        Me.Strip.SizingGrip = False
        Me.Strip.TabIndex = 6
        '
        'lbStripSelected
        '
        Me.lbStripSelected.Name = "lbStripSelected"
        Me.lbStripSelected.Size = New System.Drawing.Size(61, 17)
        Me.lbStripSelected.Text = "Selected: 0"
        Me.lbStripSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbStripFramerate
        '
        Me.lbStripFramerate.Name = "lbStripFramerate"
        Me.lbStripFramerate.Size = New System.Drawing.Size(70, 17)
        Me.lbStripFramerate.Text = "Framerate: 0"
        '
        'ToolStrip
        '
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.stripBtnStop, Me.stripBtnStart})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(638, 39)
        Me.ToolStrip.TabIndex = 7
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'stripBtnStop
        '
        Me.stripBtnStop.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.stripBtnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.stripBtnStop.Enabled = False
        Me.stripBtnStop.Image = CType(resources.GetObject("stripBtnStop.Image"), System.Drawing.Image)
        Me.stripBtnStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.stripBtnStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.stripBtnStop.Name = "stripBtnStop"
        Me.stripBtnStop.Size = New System.Drawing.Size(36, 36)
        Me.stripBtnStop.Text = "Stop"
        '
        'stripBtnStart
        '
        Me.stripBtnStart.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.stripBtnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.stripBtnStart.Image = CType(resources.GetObject("stripBtnStart.Image"), System.Drawing.Image)
        Me.stripBtnStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.stripBtnStart.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.stripBtnStart.Name = "stripBtnStart"
        Me.stripBtnStart.Size = New System.Drawing.Size(36, 36)
        Me.stripBtnStart.Text = "Start"
        '
        'Viewport
        '
        Me.Viewport.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.Viewport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Viewport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Viewport.Location = New System.Drawing.Point(52, 26)
        Me.Viewport.Name = "Viewport"
        Me.Viewport.Size = New System.Drawing.Size(512, 256)
        Me.Viewport.TabIndex = 8
        '
        'vpVmem
        '
        Me.vpVmem.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.vpVmem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.vpVmem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.vpVmem.Location = New System.Drawing.Point(6, 5)
        Me.vpVmem.Name = "vpVmem"
        Me.vpVmem.Size = New System.Drawing.Size(609, 307)
        Me.vpVmem.TabIndex = 10
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(638, 686)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.Strip)
        Me.Controls.Add(Me.tbOutput)
        Me.Controls.Add(Me.TabContainer)
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
        Me.tabDisplay.ResumeLayout(False)
        Me.tabMemory.ResumeLayout(False)
        Me.tabMemory.PerformLayout()
        CType(Me.tbOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabTools.ResumeLayout(False)
        Me.tabTools.PerformLayout()
        Me.Strip.ResumeLayout(False)
        Me.Strip.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabContainer As System.Windows.Forms.TabControl
    Friend WithEvents tabEditor As System.Windows.Forms.TabPage
    Friend WithEvents tbOutput As System.Windows.Forms.TextBox
    Friend WithEvents Usercode As System.Windows.Forms.RichTextBox
    Friend WithEvents tabDisplay As System.Windows.Forms.TabPage
    Friend WithEvents tabTools As System.Windows.Forms.TabPage
    Friend WithEvents tbSpriteArray As System.Windows.Forms.TextBox
    Friend WithEvents cmdResetButtons As System.Windows.Forms.Button
    Friend WithEvents cmdCopySprite As System.Windows.Forms.Button
    Friend WithEvents Viewport As Pixel.Viewport
    Friend WithEvents cbFiles As System.Windows.Forms.ComboBox
    Friend WithEvents Strip As System.Windows.Forms.StatusStrip
    Friend WithEvents lbStripSelected As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents stripBtnStop As System.Windows.Forms.ToolStripButton
    Friend WithEvents stripBtnStart As System.Windows.Forms.ToolStripButton
    Friend WithEvents lbStripFramerate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tabMemory As System.Windows.Forms.TabPage
    Friend WithEvents vpVmem As Pixel.Viewport
    Friend WithEvents tbOffset As System.Windows.Forms.TrackBar

End Class
