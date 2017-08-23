Public Class Instruction
    Public Property Opcode As Types

    Sub New(Opcode As Types)
        Me.Opcode = Opcode
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}", Me.Opcode)
    End Function

End Class