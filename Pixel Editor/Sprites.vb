Partial Class frmMain
    Private Property SpriteButtons As List(Of Button)
    Private Sub CreateButtonArray()
        Me.SpriteButtons = New List(Of Button)
        Dim x As Integer = 20, y As Integer = 40
        For i As Integer = 1 To 48
            If (x = 180) Then x = 20 : y += 20
            x += 20
            Dim button As New Button With {.Name = String.Format("Button{0}", i), .Enabled = True, .Location = New Point(x, y), .Size = New Size(20, 20), .Parent = Me.tabTools, .Tag = 0}
            AddHandler button.Click, AddressOf Me.SpriteButton
            Me.tabTools.Controls.Add(button)
            Me.SpriteButtons.Add(button)
        Next
    End Sub
    Private Sub SpriteButton(sender As Object, e As EventArgs)
        Dim button As Button = TryCast(sender, Button)
        If (button IsNot Nothing) Then
            If (CType(button.Tag, Integer) = 1) Then
                button.Tag = 0
                button.BackColor = Color.FromKnownColor(KnownColor.Control)
            Else
                button.Tag = 1
                button.BackColor = Color.Gray
            End If
        End If
        Me.SpriteArrayUpdate()
    End Sub
    Private Sub SpriteArrayUpdate()
        Dim cnt As Integer = 1, values As New List(Of String)
        Me.tbSpriteArray.Clear()
        Me.tbSpriteArray.AppendText(String.Format(":sprite{0}", Environment.NewLine))
        For i As Integer = 0 To Me.SpriteButtons.Count - 1
            values.Add(If(CType(Me.SpriteButtons(i).Tag, Integer) = 1, "1", "0"))
            If (cnt = 8) Then
                cnt = 1
                Me.tbSpriteArray.AppendText(String.Format(".{0}{1}", String.Concat(values.ToArray), Environment.NewLine))
                values.Clear()
            Else
                cnt += 1
            End If
        Next
    End Sub
    Private Sub cmdCopySprite_Click(sender As Object, e As EventArgs) Handles cmdCopySprite.Click
        Clipboard.Clear()
        Clipboard.SetText(Me.tbSpriteArray.Text)
        Me.tbSpriteArray.SelectAll()
        Me.tbSpriteArray.Focus()
    End Sub
    Private Sub cmdResetButtons_Click(sender As Object, e As EventArgs) Handles cmdResetButtons.Click
        For i As Integer = 3 To Me.tabTools.Controls.Count - 1
            Me.tabTools.Controls(i).Tag = 0
            Me.tabTools.Controls(i).BackColor = Color.FromKnownColor(KnownColor.Control)
        Next
        Me.SpriteArrayUpdate()
    End Sub
End Class
