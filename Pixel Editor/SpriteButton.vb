Public Class SpriteButton
    Inherits Button
    Public Property Value As Integer
    Public Property Index As Integer

    Sub New(Parent As Control, Index As Integer, Location As Point)
        Me.Value = 0
        Me.Parent = Parent
        Me.Index = Index
        Me.Enabled = True
        Me.Size = New Size(20, 20)
        Me.Location = Location
        Me.Name = String.Format("SpriteButton_{0}", Index)
    End Sub

End Class