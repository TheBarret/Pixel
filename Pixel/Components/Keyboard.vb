Namespace Components
    Public Class Keyboard
        Inherits Component
        Private Property Key As UInt16
        Private Property Mapping As Dictionary(Of Char, UInt16)
        Sub New(Parent As Processor)
            MyBase.New(Parent)
            Me.Mapping = New Dictionary(Of Char, UInt16)
            For i As UInt16 = Locations.Keys To Locations.KeysMax Step 4
                Me.Mapping.Add(ChrW(Me.Parent.ReadUInt(i)), Me.Parent.ReadUInt(CUShort(i + 2)))
            Next
        End Sub
        Public Sub PressKey(key As Char)
            If Me.Mapping.ContainsKey(Char.ToUpper(key)) Then
                Me.Key = Me.Mapping(Char.ToUpper(key))
            End If
        End Sub
        Public Sub ReleaseKey(key As Char)
            Me.Key = &H0
        End Sub
        Public Function GetKeyValue() As UInt16
            Return Me.Key
        End Function
    End Class
End Namespace