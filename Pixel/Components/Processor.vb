Imports System.Threading
Namespace Components
    Public Class Processor
        Inherits Memory
        Private Property Cycle As Cycle
        Private Property Parent As Machine
        Private Property Display As Display
        Private Property Keyboard As Keyboard
        Private Property Instructions As List(Of Instruction)
        Sub New(Parent As Machine)
            Me.Parent = Parent
            Me.Cycle = New Cycle
            Me.Display = New Display
            Me.Keyboard = New Keyboard
            Me.Pointer = Locations.Entrypoint
            Me.WriteBlock(Locations.Fonts, My.Resources.fonts)
            Me.Instructions = New List(Of Instruction)
            Me.Instructions.Add(New Instruction(Opcodes.OP_NOP))
            Me.Instructions.Add(New Instruction(Opcodes.OP_LD))
            Me.Instructions.Add(New Instruction(Opcodes.OP_ST))
            Me.Instructions.Add(New Instruction(Opcodes.OP_STV))
            Me.Instructions.Add(New Instruction(Opcodes.OP_PUSH))
            Me.Instructions.Add(New Instruction(Opcodes.OP_POP))
            Me.Instructions.Add(New Instruction(Opcodes.OP_JUMP))
            Me.Instructions.Add(New Instruction(Opcodes.OP_RET))
            Me.Instructions.Add(New Instruction(Opcodes.OP_CALL))
            Me.Instructions.Add(New Instruction(Opcodes.OP_ADD))
            Me.Instructions.Add(New Instruction(Opcodes.OP_SUB))
            Me.Instructions.Add(New Instruction(Opcodes.OP_MUL))
            Me.Instructions.Add(New Instruction(Opcodes.OP_DIV))
            Me.Instructions.Add(New Instruction(Opcodes.OP_MOD))
            Me.Instructions.Add(New Instruction(Opcodes.OP_IF))
            Me.Instructions.Add(New Instruction(Opcodes.OP_IFN))
            Me.Instructions.Add(New Instruction(Opcodes.OP_IFG))
            Me.Instructions.Add(New Instruction(Opcodes.OP_IFL))
            Me.Instructions.Add(New Instruction(Opcodes.OP_IFV))
            Me.Instructions.Add(New Instruction(Opcodes.OP_IFNV))
            Me.Instructions.Add(New Instruction(Opcodes.OP_IFGV))
            Me.Instructions.Add(New Instruction(Opcodes.OP_IFLV))
            Me.Instructions.Add(New Instruction(Opcodes.OP_SHR))
            Me.Instructions.Add(New Instruction(Opcodes.OP_SHL))
            Me.Instructions.Add(New Instruction(Opcodes.OP_OV))
            Me.Instructions.Add(New Instruction(Opcodes.OP_SCR))
            Me.Instructions.Add(New Instruction(Opcodes.OP_READ))
            Me.Instructions.Add(New Instruction(Opcodes.OP_WRITE))
            Me.Instructions.Add(New Instruction(Opcodes.OP_ADDR))
            Me.Instructions.Add(New Instruction(Opcodes.OP_RND))
            Me.Instructions.Add(New Instruction(Opcodes.OP_IFK))
            Me.Instructions.Add(New Instruction(Opcodes.OP_SPRITE))
            Me.Instructions.Add(New Instruction(Opcodes.OP_PRINT))
            Me.Instructions.Add(New Instruction(Opcodes.OP_CLS))
            Me.Instructions.Add(New Instruction(Opcodes.OP_END))
        End Sub
        Public Sub Clock()
            Me.Cycle.Begin()
            If (Me.Cycle.Throttle(1500)) Then
                Dim instruction As Instruction = Me.GetInstruction(Me.ReadByte(Me.Pointer))
                Select Case instruction.Opcode
                    Case Opcodes.OP_NOP
                        Me.Pointer = CUShort(Me.Pointer + 1)
                    Case Opcodes.OP_LD,
                         Opcodes.OP_ST,
                         Opcodes.OP_STV,
                         Opcodes.OP_PUSH,
                         Opcodes.OP_POP,
                         Opcodes.OP_JUMP,
                         Opcodes.OP_CALL,
                         Opcodes.OP_RET,
                         Opcodes.OP_END
                        Me.BasicOperations(instruction)
                    Case Opcodes.OP_ADD,
                         Opcodes.OP_SUB,
                         Opcodes.OP_MUL,
                         Opcodes.OP_DIV,
                         Opcodes.OP_MOD
                        Me.ArithmeticOperations(instruction)
                    Case Opcodes.OP_AND,
                         Opcodes.OP_OR,
                         Opcodes.OP_XOR,
                         Opcodes.OP_SHR,
                         Opcodes.OP_SHL
                        Me.BitwiseOperations(instruction)
                    Case Opcodes.OP_IF,
                         Opcodes.OP_IFN,
                         Opcodes.OP_IFG,
                         Opcodes.OP_IFL,
                         Opcodes.OP_IFV,
                         Opcodes.OP_IFNV,
                         Opcodes.OP_IFGV,
                         Opcodes.OP_IFLV
                        Me.CompareOperations(instruction)
                    Case Opcodes.OP_WRITE,
                         Opcodes.OP_READ,
                         Opcodes.OP_ADDR
                        Me.MemoryOperations(instruction)
                    Case Opcodes.OP_RND,
                         Opcodes.OP_IFK
                        Me.ExtendedOperations(instruction)
                    Case Opcodes.OP_SPRITE,
                         Opcodes.OP_PRINT,
                         Opcodes.OP_CLS,
                         Opcodes.OP_SCR
                        Me.VRamOperations(instruction)
                End Select
                If (Me.Display.Redraw) Then
                    Me.Display.Refresh()
                    Me.Parent.Viewport.BackgroundImage = Me.Display.Screen
                End If
            End If
        End Sub
        Private Sub BasicOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Opcodes.OP_LD
                    Me.Push(Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_ST
                    Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), Me.Pop)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_STV
                    Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Me.Pointer = CUShort(Me.Pointer + 5)
                Case Opcodes.OP_PUSH
                    Me.Push(Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_POP
                    Me.Pop()
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_JUMP
                    Me.Pointer = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))
                Case Opcodes.OP_CALL
                    Me.PushAddress(CUShort(Me.Pointer + 3))
                    Me.Pointer = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))
                Case Opcodes.OP_RET
                    Me.Pointer = Me.PopAddress
                Case Opcodes.OP_END
                    Me.Parent.Abort()
            End Select
        End Sub
        Private Sub CompareOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Opcodes.OP_IF
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) = Me.ReadUInt(CUShort(Me.Pointer + 3))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Opcodes.OP_IFN
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) <> Me.ReadUInt(CUShort(Me.Pointer + 3))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Opcodes.OP_IFG
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) > Me.ReadUInt(CUShort(Me.Pointer + 3))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Opcodes.OP_IFL
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) < Me.ReadUInt(CUShort(Me.Pointer + 3))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Opcodes.OP_IFV
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Opcodes.OP_IFNV
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) <> Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Opcodes.OP_IFGV
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) > Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
                Case Opcodes.OP_IFLV
                    If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) < Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))) Then
                        Me.Pointer = CUShort(Me.Pointer + 5)
                    Else
                        Me.Pointer = CUShort(Me.Pointer + 8)
                    End If
            End Select
        End Sub
        Private Sub MemoryOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Opcodes.OP_READ
                    Me.Push(Me.ReadUInt(Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_WRITE
                    Me.WriteUInt(Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))), Me.Pop)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_ADDR
                    Me.Push(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Me.Pointer = CUShort(Me.Pointer + 3)
            End Select
        End Sub
        Private Sub ArithmeticOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Opcodes.OP_ADD
                    Dim y As UInt16 = Me.Pop
                    Dim x As UInt16 = Me.Pop
                    Dim value As Int32 = x + y
                    Me.Push(CUShort(value))
                    Me.Overflow = If(value > &HFFFF, CUShort(1), CUShort(0))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_SUB
                    Dim y As UInt16 = Me.Pop
                    Dim x As UInt16 = Me.Pop
                    Dim value As Int32 = x - y
                    Me.Push(CUShort(value And &HFFFF))
                    Me.Overflow = If(value > &HFFFF, CUShort(&HFFFF), CUShort(0))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_MUL
                    Dim y As UInt16 = Me.Pop
                    Dim x As UInt16 = Me.Pop
                    Dim value As Int32 = x * y
                    Me.Push(CUShort(value And &HFFFF))
                    Me.Overflow = CUShort((value >> 16) And &HFFFF)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_DIV
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
                Case Opcodes.OP_MOD
                    Dim y As UInt16 = Me.Pop
                    Dim x As UInt16 = Me.Pop
                    If (y > 0) Then
                        Me.Push(0)
                    Else
                        Me.Push(x Mod y)
                    End If
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_OV
                    '//TODO
                    Me.Pointer = CUShort(Me.Pointer + 3)
            End Select
        End Sub
        Private Sub BitwiseOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Opcodes.OP_AND
                    Me.Push(Me.Pop And Me.Pop)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_OR
                    Me.Push(Me.Pop Or Me.Pop)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_XOR
                    Me.Push(Me.Pop Xor Me.Pop)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_SHR
                    Dim x As UInt16 = Me.Pop
                    Dim y As UInt16 = Me.ReadUInt(CUShort(Me.Pointer + 1))
                    Dim value As Int32 = x >> y
                    Me.Push(CUShort(value))
                    Me.Overflow = CUShort(((x << 16) >> y) And &HFFFF)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_SHL
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
                Case Opcodes.OP_CLS
                    Me.Display.Clear()
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_SPRITE
                    Dim x As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim y As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Me.Display.Allocate(x, y, Me.ReadBlock(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 5)), 5))
                    Me.Pointer = CUShort(Me.Pointer + 7)
                Case Opcodes.OP_PRINT
                    Dim x As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim y As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Dim addr As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 5)))
                    Me.Display.Allocate(x, y, Me.ReadBlock(Me.ReadUInt(CUShort(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 7)) + (addr * 2))), 5))
                    Me.Pointer = CUShort(Me.Pointer + 9)
                Case Opcodes.OP_SCR
                    Dim direction As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim value As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Me.Display.Scroll(direction, value)
                    Me.Pointer = CUShort(Me.Pointer + 5)
            End Select
        End Sub
        Private Sub ExtendedOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Opcodes.OP_RND
                    Me.Push(Me.ReadUInt(CUShort(Me.Pointer + 1)).Random)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Opcodes.OP_IFK
                    '// TODO
                    Me.Pointer = CUShort(Me.Pointer + 3)
            End Select
        End Sub
        Private Function GetInstruction(Opcode As UInt16) As Instruction
            If (Me.Instructions.Where(Function(x) x.Opcode = Opcode).Any) Then
                Return Me.Instructions.Where(Function(x) x.Opcode = Opcode).FirstOrDefault
            End If
            Throw New Exception(String.Format("Undefined instruction at 0x{0} '{1}'", Me.Pointer.ToString("X"), Opcode.ToString))
        End Function
        Protected Overrides Sub Dispose(disposing As Boolean)
            Me.Cycle = Nothing
            Me.Display = Nothing
            Me.Instructions = Nothing
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace