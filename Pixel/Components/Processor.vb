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
            Me.Instructions.Add(New Instruction(Opcodes.OP_SHR))
            Me.Instructions.Add(New Instruction(Opcodes.OP_SHL))
            Me.Instructions.Add(New Instruction(Opcodes.OP_WRITE))
            Me.Instructions.Add(New Instruction(Opcodes.OP_ADDR))
            Me.Instructions.Add(New Instruction(Opcodes.OP_RND))
            Me.Instructions.Add(New Instruction(Opcodes.OP_IFK))
            Me.Instructions.Add(New Instruction(Opcodes.OP_PUSHVRAMDATA))
            Me.Instructions.Add(New Instruction(Opcodes.OP_PUSHVRAMMEMORY))
            Me.Instructions.Add(New Instruction(Opcodes.OP_PUSHVRAMSTRING))
            Me.Instructions.Add(New Instruction(Opcodes.OP_CLS))
            Me.Instructions.Add(New Instruction(Opcodes.OP_END))
        End Sub
        Public Sub Clock()
            Me.Cycle.Begin()
            If (Me.Cycle.Throttle(1500)) Then
                Dim v = Me.GetInstruction(Me.ReadByte(Me.Pointer)).Opcode
                Select Case v
                    Case Opcodes.OP_NOP
                        Me.Pointer = CUShort(Me.Pointer + 1)
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
                        Me.StoreReturn(CUShort(Me.Pointer + 3))
                        Me.Pointer = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))
                    Case Opcodes.OP_RET
                        Me.Pointer = Me.FetchReturn
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
                    Case Opcodes.OP_IF
                        If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) = Me.ReadUInt(CUShort(Me.Pointer + 3))) Then
                            Me.Pointer = CUShort(Me.Pointer + 5)
                        Else
                            Me.Pointer = CUShort(Me.Pointer + 8)
                        End If
                    Case Opcodes.OP_IFN
                        If (Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))) <> Me.ReadUInt(CUShort(Me.Pointer + 3))) Then
                            Me.Pointer = CUShort(Me.Pointer + 8)
                        Else
                            Me.Pointer = CUShort(Me.Pointer + 5)
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
                    Case Opcodes.OP_WRITE
                        Me.WriteUInt(Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))), Me.Pop)
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_ADDR
                        Me.Push(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_RND
                        Static rnd As New Random(Environment.TickCount)
                        Me.Push(CUShort(rnd.Next(0, Me.ReadUInt(CUShort(Me.Pointer + 1)))))
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_IFK
                        '// TODO
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_PUSHVRAMDATA
                        Dim x As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                        Dim y As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                        Me.Display.Allocate(x, y, Me.ReadBlock(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 5)), 5))
                        Me.Pointer = CUShort(Me.Pointer + 7)
                    Case Opcodes.OP_PUSHVRAMMEMORY
                        Dim x As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                        Dim y As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                        Me.Display.Allocate(x, y, Me.ReadBlock(Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 5))), 5))
                        Me.Display.Redraw = True
                        Me.Pointer = CUShort(Me.Pointer + 7)
                    Case Opcodes.OP_PUSHVRAMSTRING
                        Dim x As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                        Dim y As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                        Dim addr As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 5)))
                        Me.Display.Allocate(x, y, Me.ReadBlock(Me.ReadUInt(CUShort(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 7)) + (addr * 2))), 5))
                        Me.Pointer = CUShort(Me.Pointer + 9)
                    Case Opcodes.OP_CLS
                        Me.Display.Clear()
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_END
                        Me.Parent.Abort()
                End Select

                If (Me.Display.Redraw) Then
                    Me.Display.Refresh()
                    Me.Parent.Viewport.BackgroundImage = Me.Display.Screen
                End If
            End If
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