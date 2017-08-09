Namespace Tasks
    Public Class Definition
        Public Property Type As TType
        Public Property Pattern As String
        Sub New(Type As TType, Pattern As String)
            Me.Type = Type
            Me.Pattern = Pattern
        End Sub
        Public Function ToBytecode() As Byte
            For Each opcode As Opcodes In [Enum].GetValues(GetType(Opcodes))
                If (opcode.ToString.Replace("OP_", "T_").Equals(Me.Type.ToString)) Then
                    Return CType(opcode, Byte)
                End If
            Next
            Throw New Exception(String.Format("Could not convert defintion '{0}' to opcode value", Me.Type))
        End Function
        Public Function IsInstruction() As Boolean
            Return Me.Type <> TType.T_LABEL And
                   Me.Type <> TType.T_LOCATION And
                   Me.Type <> TType.T_HEX And
                   Me.Type <> TType.T_VAR And
                   Me.Type <> TType.T_NUMBER
        End Function
        Public Overrides Function ToString() As String
            Return String.Format("{0}", Me.Type.ToString)
        End Function
    End Class
End Namespace