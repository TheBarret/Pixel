Namespace Tasks
    Public Class Definition
        Public Property Type As Opcodes
        Public Property Pattern As String
        Sub New(Type As Opcodes, Pattern As String)
            Me.Type = Type
            Me.Pattern = Pattern
        End Sub
        Public Function IsOpcode() As Boolean
            Return Me.Type <> Opcodes.T_LABEL And
                   Me.Type <> Opcodes.T_LOCATION And
                   Me.Type <> Opcodes.T_HEX And
                   Me.Type <> Opcodes.T_VAR And
                   Me.Type <> Opcodes.T_NUMBER
        End Function
        Public Overrides Function ToString() As String
            Return String.Format("{0}", Me.Type.ToString)
        End Function
    End Class
End Namespace