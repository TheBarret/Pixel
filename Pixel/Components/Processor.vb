Imports System.Threading
Namespace Components
    Public Class Processor
        Inherits Memory
        Implements IDisposable
        Private Property Cycle As Cycle
        Private Property Parent As Machine
        Private Property Display As Display
        Private Property Stack As Stack(Of UInt16)
        Private Property Address As Stack(Of UInt16)
        Private Property Instructions As List(Of Instruction)
        Sub New(Parent As Machine)
            Me.Parent = Parent
            Me.Cycle = New Cycle
            Me.Display = New Display
            Me.Pointer = Locations.Entrypoint
            Me.Stack = New Stack(Of UShort)
            Me.Address = New Stack(Of UShort)
            Me.Instructions = New List(Of Instruction)
            Me.Instructions.Add(New Instruction(Opcodes.OP_LD))
            Me.Instructions.Add(New Instruction(Opcodes.OP_ST))
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
            Me.Instructions.Add(New Instruction(Opcodes.OP_IFK))
            Me.Instructions.Add(New Instruction(Opcodes.OP_TIMER))
            Me.Instructions.Add(New Instruction(Opcodes.OP_PUSHDATA))
            Me.Instructions.Add(New Instruction(Opcodes.OP_PUSHMEMORY))
            Me.Instructions.Add(New Instruction(Opcodes.OP_CLS))
            Me.Instructions.Add(New Instruction(Opcodes.OP_END))
        End Sub
        Public Sub Clock()
            Me.Cycle.Begin()
            If (Me.Cycle.Throttle(1500)) Then
                Select Case Me.GetInstruction(Me.ReadByte(Me.Pointer)).Opcode
                    Case Opcodes.OP_LD
                        Me.Stack.Push(Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))))
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_ST
                        Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), Me.Stack.Pop)
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_PUSH
                        Me.Stack.Push(Me.ReadUInt(CUShort(Me.Pointer + 1)))
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_POP
                        Me.Stack.Pop()
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_JUMP
                        Me.Pointer = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))
                    Case Opcodes.OP_CALL
                        Me.Address.Push(CUShort(Me.Pointer + 3))
                        Me.Pointer = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))
                    Case Opcodes.OP_RET
                        If (Me.Address.Count >= 1) Then
                            Me.Pointer = Me.Address.Pop
                        End If
                    Case Opcodes.OP_ADD
                        If (Me.Stack.Count >= 2) Then
                            Me.Stack.Push(Me.Stack.Pop + Me.Stack.Pop)
                        End If
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_SUB
                        If (Me.Stack.Count >= 2) Then
                            Me.Stack.Push(Me.Stack.Pop - Me.Stack.Pop)
                        End If
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_MUL
                        If (Me.Stack.Count >= 2) Then
                            Me.Stack.Push(Me.Stack.Pop * Me.Stack.Pop)
                        End If
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_DIV
                        If (Me.Stack.Count >= 2) Then
                            Me.Stack.Push(Me.Stack.Pop \ Me.Stack.Pop)
                        End If
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_MOD
                        If (Me.Stack.Count >= 2) Then
                            Me.Stack.Push(Me.Stack.Pop Mod Me.Stack.Pop)
                        End If
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_AND
                        If (Me.Stack.Count >= 2) Then
                            Me.Stack.Push(Me.Stack.Pop And Me.Stack.Pop)
                        End If
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_OR
                        If (Me.Stack.Count >= 2) Then
                            Me.Stack.Push(Me.Stack.Pop Or Me.Stack.Pop)
                        End If
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_XOR
                        If (Me.Stack.Count >= 2) Then
                            Me.Stack.Push(Me.Stack.Pop Xor Me.Stack.Pop)
                        End If
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_IF
                        If (Me.Stack.Count >= 1) Then
                            If (Me.Stack.Pop = Me.ReadUInt(CUShort(Me.Pointer + 1))) Then
                                Me.Pointer = CUShort(Me.Pointer + 3)
                            Else
                                Me.Pointer = CUShort(Me.Pointer + 6)
                            End If
                        End If
                    Case Opcodes.OP_IFN
                        If (Me.Stack.Count >= 1) Then
                            If (Me.Stack.Pop <> Me.ReadUInt(CUShort(Me.Pointer + 1))) Then
                                Me.Pointer = CUShort(Me.Pointer + 6)
                            Else
                                Me.Pointer = CUShort(Me.Pointer + 3)
                            End If
                        End If
                    Case Opcodes.OP_IFG
                        If (Me.Stack.Count >= 1) Then
                            If (Me.Stack.Pop > Me.ReadUInt(CUShort(Me.Pointer + 1))) Then
                                Me.Pointer = CUShort(Me.Pointer + 3)
                            Else
                                Me.Pointer = CUShort(Me.Pointer + 6)
                            End If
                        End If
                    Case Opcodes.OP_IFL
                        If (Me.Stack.Count >= 1) Then
                            If (Me.Stack.Pop < Me.ReadUInt(CUShort(Me.Pointer + 1))) Then
                                Me.Pointer = CUShort(Me.Pointer + 3)
                            Else
                                Me.Pointer = CUShort(Me.Pointer + 6)
                            End If
                        End If
                    Case Opcodes.OP_SHR
                        If (Me.Stack.Count >= 1) Then
                            Me.Stack.Push(Me.Stack.Pop >> Me.ReadUInt(CUShort(Me.Pointer + 1)))
                            Me.Pointer = CUShort(Me.Pointer + 3)
                        End If
                    Case Opcodes.OP_SHL
                        If (Me.Stack.Count >= 1) Then
                            Me.Stack.Push(Me.Stack.Pop << Me.ReadUInt(CUShort(Me.Pointer + 1)))
                            Me.Pointer = CUShort(Me.Pointer + 3)
                        End If
                    Case Opcodes.OP_IFK
                        '// TODO
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_TIMER
                        '// TODO
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_PUSHDATA
                        If (Me.Stack.Count >= 2) Then
                            Me.Display.Allocate(Me.Stack.Pop, Me.Stack.Pop, Me.ReadBlock(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), 5))
                            Me.Display.Redraw = True
                        End If
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_PUSHMEMORY
                        If (Me.Stack.Count >= 2) Then
                            Me.Display.Allocate(Me.Stack.Pop, Me.Stack.Pop, Me.ReadBlock(Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))), 5))
                            Me.Display.Redraw = True
                        End If
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_CLS
                        Me.Display.Clear()
                        Me.Pointer = CUShort(Me.Pointer + 3)
                    Case Opcodes.OP_END
                        Me.Parent.Abort()
                End Select

                If (Me.Display.Redraw) Then
                    Me.Display.DrawFrame()
                    Me.Display.Redraw = False
                    Me.Parent.Viewport.BackgroundImage = Me.Display.Screen
                End If
            End If
        End Sub
        Private Function GetInstruction(Opcode As UInt16) As Instruction
            If (Me.Instructions.Where(Function(x) x.Opcode = Opcode).Any) Then
                Return Me.Instructions.Where(Function(x) x.Opcode = Opcode).FirstOrDefault
            End If
            Throw New Exception(String.Format("Unkown instruction at {0} '{1}'", Me.Pointer, Opcode.ToString))
        End Function
#Region "IDisposable Support"
        Private disposedValue As Boolean
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    Me.Reset()
                    Me.Stack = Nothing
                    Me.Instructions = Nothing
                End If
            End If
            Me.disposedValue = True
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
End Namespace