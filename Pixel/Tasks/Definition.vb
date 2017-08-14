Namespace Tasks
    Public Class Definition
        Public Property Type As Opcodes
        Public Property Pattern As String
        Sub New(Type As Opcodes, Pattern As String)
            Me.Type = Type
            Me.Pattern = Pattern
        End Sub
        Public Function GetActualLength() As Integer
            Select Case Me.Type
                Case Opcodes.OP_POP,
                     Opcodes.OP_RET,
                     Opcodes.OP_ADD,
                     Opcodes.OP_SUB,
                     Opcodes.OP_MUL,
                     Opcodes.OP_DIV,
                     Opcodes.OP_MOD,
                     Opcodes.OP_AND,
                     Opcodes.OP_OR,
                     Opcodes.OP_XOR,
                     Opcodes.OP_CLS,
                     Opcodes.OP_END
                    Return 1
                Case Opcodes.OP_PUSH,
                     Opcodes.OP_LD,
                     Opcodes.OP_ST,
                     Opcodes.OP_STV,
                     Opcodes.OP_JUMP,
                     Opcodes.OP_CALL,
                     Opcodes.OP_IF,
                     Opcodes.OP_IFN,
                     Opcodes.OP_IFG,
                     Opcodes.OP_IFL,
                     Opcodes.OP_IFV,
                     Opcodes.OP_IFNV,
                     Opcodes.OP_IFGV,
                     Opcodes.OP_IFLV,
                     Opcodes.OP_SHR,
                     Opcodes.OP_SHL,
                     Opcodes.OP_OV,
                     Opcodes.OP_READ,
                     Opcodes.OP_WRITE,
                     Opcodes.OP_ADDR,
                     Opcodes.OP_RND,
                     Opcodes.OP_SCR,
                     Opcodes.OP_SPRITE,
                     Opcodes.OP_PRINT,
                     Opcodes.OP_STKEY,
                     Opcodes.OP_COL
                    Return 3
                Case Opcodes.T_NUMBER,
                     Opcodes.T_HEXADECIMAL,
                     Opcodes.T_LOCATION,
                     Opcodes.T_KEY,
                     Opcodes.T_STRING,
                     Opcodes.T_SPRITEDATA,
                     Opcodes.T_VARIABLE
                    Return 4
                Case Else
                    Return -1
            End Select
        End Function
        Public ReadOnly Property ToByte As Byte
            Get
                Return CType(Me.Type, Byte)
            End Get
        End Property
        Public ReadOnly Property Length As Integer
            Get
                Select Case Me.Type
                    Case Opcodes.T_HEXADECIMAL,
                         Opcodes.T_NUMBER,
                         Opcodes.T_LOCATION,
                         Opcodes.T_SPACE
                        Return 0
                    Case Opcodes.T_SPRITEDATA
                        Return 1
                    Case Opcodes.T_VARIABLE,
                         Opcodes.T_KEY
                        Return 2
                    Case Opcodes.OP_IF,
                         Opcodes.OP_IFN,
                         Opcodes.OP_IFG,
                         Opcodes.OP_IFL,
                         Opcodes.OP_IFV,
                         Opcodes.OP_IFNV,
                         Opcodes.OP_IFGV,
                         Opcodes.OP_IFLV,
                         Opcodes.OP_STV,
                         Opcodes.OP_STV,
                         Opcodes.OP_SCR
                        Return 5
                    Case Opcodes.OP_SPRITE
                        Return 7
                    Case Opcodes.OP_PRINT
                        Return 9
                    Case Else
                        Return 3
                End Select
            End Get
        End Property
        Public Overrides Function ToString() As String
            Return String.Format("{0}", Me.Type.ToString)
        End Function
    End Class
End Namespace