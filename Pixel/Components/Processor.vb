Imports System.Threading
Imports System.Windows.Forms
Imports System.Drawing

Namespace Components
    Public Class Processor
        Inherits Memory
        Protected Friend Property Seed As UInt16
        Protected Friend Property Display As Display
        Protected Friend Property Keyboard As Keyboard
        Protected Friend Property Parent As Machine
        Private Property Instructions As List(Of Instruction)
        Public Event OnViewportUpdate(im As Image)
        Sub New(Parent As Machine)
            Me.Parent = Parent
            Me.Seed = &H0
            Me.Display = New Display(Me)
            Me.Keyboard = New Keyboard(Me)
            Me.Pointer = Locations.Entrypoint
            Me.Instructions = New List(Of Instruction)
            Me.Instructions.Add(New Instruction(Types.OP_NOP))
            Me.Instructions.Add(New Instruction(Types.OP_LD))
            Me.Instructions.Add(New Instruction(Types.OP_ST))
            Me.Instructions.Add(New Instruction(Types.OP_STV))
            Me.Instructions.Add(New Instruction(Types.OP_PUSH))
            Me.Instructions.Add(New Instruction(Types.OP_RSP))
            Me.Instructions.Add(New Instruction(Types.OP_JUMP))
            Me.Instructions.Add(New Instruction(Types.OP_RET))
            Me.Instructions.Add(New Instruction(Types.OP_CALL))
            Me.Instructions.Add(New Instruction(Types.OP_ADD))
            Me.Instructions.Add(New Instruction(Types.OP_SUB))
            Me.Instructions.Add(New Instruction(Types.OP_MUL))
            Me.Instructions.Add(New Instruction(Types.OP_DIV))
            Me.Instructions.Add(New Instruction(Types.OP_MOD))
            Me.Instructions.Add(New Instruction(Types.OP_INC))
            Me.Instructions.Add(New Instruction(Types.OP_DEC))
            Me.Instructions.Add(New Instruction(Types.OP_IF))
            Me.Instructions.Add(New Instruction(Types.OP_IFN))
            Me.Instructions.Add(New Instruction(Types.OP_IFG))
            Me.Instructions.Add(New Instruction(Types.OP_IFL))
            Me.Instructions.Add(New Instruction(Types.OP_IFV))
            Me.Instructions.Add(New Instruction(Types.OP_IFNV))
            Me.Instructions.Add(New Instruction(Types.OP_IFGV))
            Me.Instructions.Add(New Instruction(Types.OP_IFLV))
            Me.Instructions.Add(New Instruction(Types.OP_SHR))
            Me.Instructions.Add(New Instruction(Types.OP_SHL))
            Me.Instructions.Add(New Instruction(Types.OP_OV))
            Me.Instructions.Add(New Instruction(Types.OP_COL))
            Me.Instructions.Add(New Instruction(Types.OP_STKEY))
            Me.Instructions.Add(New Instruction(Types.OP_SCR))
            Me.Instructions.Add(New Instruction(Types.OP_READ))
            Me.Instructions.Add(New Instruction(Types.OP_WRITE))
            Me.Instructions.Add(New Instruction(Types.OP_ADDR))
            Me.Instructions.Add(New Instruction(Types.OP_SEED))
            Me.Instructions.Add(New Instruction(Types.OP_RND))
            Me.Instructions.Add(New Instruction(Types.OP_DRAW))
            Me.Instructions.Add(New Instruction(Types.OP_PRINT))
            Me.Instructions.Add(New Instruction(Types.OP_PRINTV))
            Me.Instructions.Add(New Instruction(Types.OP_CLS))
            Me.Instructions.Add(New Instruction(Types.OP_END))
            Me.Instructions.Add(New Instruction(Types.OP_SEED))
        End Sub
        Public Sub Clock()
            Dim instruction As Instruction = Me.GetInstruction(Me.ReadByte(Me.Pointer))
            Select Case instruction.Opcode
                Case Types.OP_NOP
                    Me.Pointer = CUShort(Me.Pointer + 1)
                Case Types.OP_LD,
                     Types.OP_ST,
                     Types.OP_STV,
                     Types.OP_PUSH,
                     Types.OP_RSP,
                     Types.OP_JUMP,
                     Types.OP_CALL,
                     Types.OP_RET,
                     Types.OP_END
                    Me.BasicOperations(instruction)
                Case Types.OP_ADD,
                     Types.OP_SUB,
                     Types.OP_MUL,
                     Types.OP_DIV,
                     Types.OP_MOD,
                     Types.OP_INC,
                     Types.OP_DEC
                    Me.ArithmeticOperations(instruction)
                Case Types.OP_AND,
                     Types.OP_OR,
                     Types.OP_XOR,
                     Types.OP_NOT,
                     Types.OP_SHR,
                     Types.OP_SHL
                    Me.BitwiseOperations(instruction)
                Case Types.OP_IF,
                     Types.OP_IFN,
                     Types.OP_IFG,
                     Types.OP_IFL,
                     Types.OP_IFV,
                     Types.OP_IFNV,
                     Types.OP_IFGV,
                     Types.OP_IFLV
                    Me.CompareOperations(instruction)
                Case Types.OP_WRITE,
                     Types.OP_READ,
                     Types.OP_ADDR
                    Me.MemoryOperations(instruction)
                Case Types.OP_DRAW,
                     Types.OP_PRINTV,
                     Types.OP_PRINT,
                     Types.OP_CLS,
                     Types.OP_SCR
                    Me.VRamOperations(instruction)
                Case Types.OP_STKEY
                    Me.KeyboardOperations(instruction)
                Case Types.OP_RND,
                     Types.OP_OV,
                     Types.OP_COL,
                     Types.OP_SEED
                    Me.ExtendedOperations(instruction)
            End Select
        End Sub
        Private Sub BasicOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.OP_LD
                    Me.Push(Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_ST
                    Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), Me.Pop)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_STV
                    Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Me.Pointer = CUShort(Me.Pointer + 5)
                Case Types.OP_PUSH
                    Me.Push(Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_RSP
                    Do Until Me.Stack = 0
                        Me.Pop()
                    Loop
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_JUMP
                    Me.Pointer = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))
                Case Types.OP_CALL
                    Me.PushAddress(CUShort(Me.Pointer + 3))
                    Me.Pointer = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))
                Case Types.OP_RET
                    Me.Pointer = Me.PopAddress
                Case Types.OP_END
                    Me.Parent.Abort()
            End Select
        End Sub
        Private Sub KeyboardOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.OP_STKEY
                    Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), Me.Keyboard.GetKeyValue())
                    Me.Pointer = CUShort(Me.Pointer + 3)
            End Select
        End Sub
        Private Sub CompareOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.OP_IF
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) = Me.ReadUInt(CUShort(Me.Pointer + 3))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Types.OP_IFN
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) <> Me.ReadUInt(CUShort(Me.Pointer + 3))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Types.OP_IFG
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) > Me.ReadUInt(CUShort(Me.Pointer + 3))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Types.OP_IFL
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) < Me.ReadUInt(CUShort(Me.Pointer + 3))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Types.OP_IFV
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Types.OP_IFNV
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) <> Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Types.OP_IFGV
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) > Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Types.OP_IFLV
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) < Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
            End Select
        End Sub
        Private Sub MemoryOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.OP_READ
                    Me.Push(Me.ReadUInt(Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_WRITE
                    Me.WriteUInt(Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))), Me.Pop)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_ADDR
                    Me.Push(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Me.Pointer = CUShort(Me.Pointer + 3)
            End Select
        End Sub
        Private Sub ArithmeticOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.OP_ADD
                    Dim y As UInt16 = Me.Pop
                    Dim x As UInt16 = Me.Pop
                    Dim value As Int32 = x + y
                    Me.Push(CUShort(value))
                    Me.Overflow = If(value > &HFFFF, CUShort(1), CUShort(0))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_SUB
                    Dim y As UInt16 = Me.Pop
                    Dim x As UInt16 = Me.Pop
                    Dim value As Int32 = x - y
                    Me.Push(CUShort(value And &HFFFF))
                    Me.Overflow = If(value > &HFFFF, CUShort(&HFFFF), CUShort(0))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_MUL
                    Dim y As UInt16 = Me.Pop
                    Dim x As UInt16 = Me.Pop
                    Dim value As Int32 = x * y
                    Me.Push(CUShort(value And &HFFFF))
                    Me.Overflow = CUShort((value >> 16) And &HFFFF)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_DIV
                    Dim y As UInt16 = Me.Pop
                    Dim x As UInt16 = Me.Pop
                    If (y > 0) Then
                        Dim value As Int32 = x \ y
                        Me.Push(CUShort(value And &HFFFF))
                        Me.Overflow = CUShort(((x << 16) \ y) And &HFFFF)
                    Else
                        Throw New Exception("Division by zero")
                    End If
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_MOD
                    Dim y As UInt16 = Me.Pop
                    Dim x As UInt16 = Me.Pop
                    If (y > 0) Then Me.Push(0) Else Me.Push(x Mod y)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_INC
                    Dim v As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim inc As UInt16 = Me.ReadUInt(CUShort(Me.Pointer + 3))
                    Dim sum As Int32 = v + inc
                    Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), CUShort(sum))
                    Me.Overflow = If(sum > &HFFFF, CUShort(1), CUShort(0))
                    Me.Pointer = CUShort(Me.Pointer + 5)
                Case Types.OP_DEC
                    Dim v As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim inc As UInt16 = Me.ReadUInt(CUShort(Me.Pointer + 3))
                    Dim sum As Int32 = v - inc
                    Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), CUShort(sum And &HFFFF))
                    Me.Overflow = If(sum > &HFFFF, CUShort(1), CUShort(0))
                    Me.Pointer = CUShort(Me.Pointer + 5)
            End Select
        End Sub
        Private Sub BitwiseOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.OP_AND
                    Me.Push(Me.Pop And Me.Pop)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_OR
                    Me.Push(Me.Pop Or Me.Pop)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_XOR
                    Me.Push(Me.Pop Xor Me.Pop)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_NOT
                    Me.Push(Not Me.Pop)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_SHR
                    Dim x As UInt16 = Me.Pop
                    Dim y As UInt16 = Me.ReadUInt(CUShort(Me.Pointer + 1))
                    Dim value As Int32 = x >> y
                    Me.Push(CUShort(value))
                    Me.Overflow = CUShort(((x << 16) >> y) And &HFFFF)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_SHL
                    Dim x As UInt16 = Me.Pop
                    Dim y As UInt16 = Me.ReadUInt(CUShort(Me.Pointer + 1))
                    Dim value As Int32 = (x << y)
                    Me.Push(CUShort(value))
                    Me.Overflow = CUShort((value >> 16) And &HFFFF)
                    Me.Pointer = CUShort(Me.Pointer + 3)
            End Select
        End Sub
        Private Sub VRamOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.OP_CLS
                    Me.Display.Clear()
                    Me.Display.Redraw = True
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_DRAW
                    Dim x As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim y As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Me.Display.Allocate(x, y, Me.ReadBlock(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 5)), 5))
                    Me.Pointer = CUShort(Me.Pointer + 7)
                Case Types.OP_PRINT
                    Dim x As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim y As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Dim i As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 5)))
                    Me.Display.Allocate(x, y, Me.ReadBlock(Me.ReadUInt(CUShort(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 7)) + (i * 2))), 5))
                    Me.Pointer = CUShort(Me.Pointer + 9)
                Case Types.OP_PRINTV
                    Dim x As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim y As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Dim num As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 5)))
                    For Each ch As Char In num.ToString.ToCharArray
                        Me.Display.Allocate(x, y, Me.NumberToSprite(ch))
                        x = CUShort(x + 5)
                    Next
                    Me.Pointer = CUShort(Me.Pointer + 7)
                Case Types.OP_SCR
                    Dim direction As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim value As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Me.Display.Scroll(direction, value)
                    Me.Pointer = CUShort(Me.Pointer + 5)
            End Select
        End Sub
        Private Sub ExtendedOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.OP_SEED
                    Me.Seed = Me.ReadUInt(CUShort(Me.Pointer + 1))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_RND
                    Me.Push(Me.ReadUInt(CUShort(Me.Pointer + 1)).Random(Me.Seed))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_COL
                    Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), Me.Collision)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_OV
                    Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), Me.Overflow)
                    Me.Pointer = CUShort(Me.Pointer + 3)
            End Select
        End Sub
        Private Function GetInstruction(Opcode As UInt16) As Instruction
            If (Me.Instructions.Where(Function(x) x.Opcode = Opcode).Any) Then
                Return Me.Instructions.Where(Function(x) x.Opcode = Opcode).FirstOrDefault
            End If
            Throw New Exception(String.Format("Undefined instruction at 0x{0} '{1}'", Me.Pointer.ToString("X"), Opcode.ToString))
        End Function
        Protected Friend Sub UpdateViewport(im As Image)
            RaiseEvent OnViewportUpdate(im)
        End Sub
        Protected Overrides Sub Dispose(disposing As Boolean)
            Me.Display = Nothing
            Me.Instructions = Nothing
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace