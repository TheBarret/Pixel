﻿Imports System.Drawing

Namespace Components
    Public Class Processor
        Inherits Memory
        Protected Friend Property Display As Display
        Protected Friend Property Keyboard As Keyboard
        Protected Friend Property Machine As Machine
        Private Property Instructions As List(Of Instruction)
        Public Event OnViewportUpdate(Sender As Object, im As Image)
        Sub New(Machine As Machine)
            Me.Machine = Machine
            Me.RndSeed = &H0
            Me.FontOffset = &H6
            Me.Keyboard = New Keyboard(Me)
            Me.Display = New Display(Me, 128, 64)
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
            Me.Instructions.Add(New Instruction(Types.OP_AND))
            Me.Instructions.Add(New Instruction(Types.OP_XOR))
            Me.Instructions.Add(New Instruction(Types.OP_OR))
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
            Me.Instructions.Add(New Instruction(Types.OP_INPUT))
            Me.Instructions.Add(New Instruction(Types.OP_SCR))
            Me.Instructions.Add(New Instruction(Types.OP_READ))
            Me.Instructions.Add(New Instruction(Types.OP_WRITE))
            Me.Instructions.Add(New Instruction(Types.OP_ADDR))
            Me.Instructions.Add(New Instruction(Types.OP_SEED))
            Me.Instructions.Add(New Instruction(Types.OP_RND))
            Me.Instructions.Add(New Instruction(Types.OP_DRAW))
            Me.Instructions.Add(New Instruction(Types.OP_PRINT))
            Me.Instructions.Add(New Instruction(Types.OP_PRINTV))
            Me.Instructions.Add(New Instruction(Types.OP_STRLEN))
            Me.Instructions.Add(New Instruction(Types.OP_STRCMP))
            Me.Instructions.Add(New Instruction(Types.OP_MODE))
            Me.Instructions.Add(New Instruction(Types.OP_CLS))
            Me.Instructions.Add(New Instruction(Types.OP_END))
            Me.Instructions.Add(New Instruction(Types.OP_SEED))
            Me.Instructions.Add(New Instruction(Types.SPECIAL_DRAWA))
            Me.Instructions.Add(New Instruction(Types.SPECIAL_PRINT))
            Me.Instructions.Add(New Instruction(Types.SPECIAL_PRINTV))
            Me.Instructions.Add(New Instruction(Types.SPECIAL_STRLA))
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
                     Types.OP_SCR,
                     Types.OP_MODE
                    Me.VRamOperations(instruction)
                Case Types.OP_STRLEN,
                     Types.OP_STRCMP
                    Me.StringOperations(instruction)
                Case Types.OP_INPUT
                    Me.KeyboardOperations(instruction)
                Case Types.OP_RND,
                     Types.OP_OV,
                     Types.OP_COL,
                     Types.OP_SEED
                    Me.ExtendedOperations(instruction)
                Case Types.SPECIAL_PRINT,
                     Types.SPECIAL_PRINTV,
                     Types.SPECIAL_DRAWA,
                     Types.SPECIAL_STRLA
                    Me.LibraryOperations(instruction)
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
                    Me.Machine.Abort()
            End Select
        End Sub
        Private Sub KeyboardOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.OP_INPUT
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
        Private Sub StringOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.OP_STRLEN
                    Dim len As UInt16 = 0
                    Dim src As UInt16 = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))
                    Dim dst As UInt16 = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3))
                    For i As Integer = src To src + Byte.MaxValue Step 2
                        If (Me.ReadUInt(CUShort(i)) = 0) Then Exit For
                        len = CUShort(len + 1)
                    Next
                    Me.WriteUInt(dst, len)
                    Me.Pointer = CUShort(Me.Pointer + 5)
                Case Types.OP_STRCMP
                    Dim isequal As Boolean = True
                    Dim src As UInt16 = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))
                    Dim dst As UInt16 = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3))
                    For i As Integer = 0 To Byte.MaxValue Step 2
                        Dim a As UInt16 = Me.ReadUInt(CUShort(i + src))
                        Dim b As UInt16 = Me.ReadUInt(CUShort(i + dst))
                        If (a = 0 AndAlso b = 0) Then Exit For
                        If (a <> b) Then
                            isequal = False
                            Exit For
                        End If
                    Next
                    If (isequal) Then
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
                    Dim v = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1))
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
                        x = CUShort(x + 6)
                    Next
                    Me.Pointer = CUShort(Me.Pointer + 7)
                Case Types.OP_SCR
                    Dim direction As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim value As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Me.Display.Shift(direction, value)
                    Me.Pointer = CUShort(Me.Pointer + 5)
                Case Types.OP_MODE
                    Select Case Me.ReadUInt(CUShort(Me.Pointer + 1))
                        Case 0 : Me.Display.Mode(64, 32, 8)
                        Case 1 : Me.Display.Mode(128, 64, 4)
                    End Select
                    Me.Pointer = CUShort(Me.Pointer + 3)
            End Select
        End Sub
        Private Sub ExtendedOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.OP_SEED
                    Me.RndSeed = Me.ReadUInt(CUShort(Me.Pointer + 1))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_RND
                    Me.Push(Me.ReadUInt(CUShort(Me.Pointer + 1)).Random(Me.RndSeed))
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_COL
                    Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), Me.Collision)
                    Me.Pointer = CUShort(Me.Pointer + 3)
                Case Types.OP_OV
                    Me.WriteUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)), Me.Overflow)
                    Me.Pointer = CUShort(Me.Pointer + 3)
            End Select
        End Sub
        Private Sub LibraryOperations(instruction As Instruction)
            Select Case instruction.Opcode
                Case Types.SPECIAL_STRLA
                    Dim len As UInt16 = 0
                    Dim src As UInt16 = Locations.Entrypoint + Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim dst As UInt16 = Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3))
                    For i As Integer = src To src + Byte.MaxValue Step 2
                        If (Me.ReadUInt(CUShort(i)) = 0) Then Exit For
                        len = CUShort(len + 1)
                    Next
                    Me.WriteUInt(dst, len)
                    Me.Pointer = CUShort(Me.Pointer + 5)
                Case Types.SPECIAL_DRAWA
                    Dim x As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim y As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Dim addr As UInt16 = Me.ReadUInt(CUShort(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 5))))
                    Me.Display.Allocate(x, y, Me.ReadBlock(addr, 5))
                    Me.Pointer = CUShort(Me.Pointer + 7)
                Case Types.SPECIAL_PRINT
                    Dim x As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim y As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Dim i As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 5)))
                    Dim addr As UInt16 = Locations.Entrypoint + Me.ReadUInt(CUShort(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 7))))
                    Me.Display.Allocate(x, y, Me.ReadBlock(Me.ReadUInt(CUShort(addr + (i * 2))), 5))
                    Me.Pointer = CUShort(Me.Pointer + 9)
                Case Types.SPECIAL_PRINTV
                    Dim x As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 1)))
                    Dim y As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 3)))
                    Dim num As UInt16 = Me.ReadUInt(Locations.Entrypoint + Me.ReadUInt(CUShort(Locations.Entrypoint + Me.ReadUInt(CUShort(Me.Pointer + 5)))))
                    For Each ch As Char In num.ToString.ToCharArray
                        Me.Display.Allocate(x, y, Me.NumberToSprite(ch))
                        x = CUShort(x + 6)
                    Next
                    Me.Pointer = CUShort(Me.Pointer + 7)
            End Select
        End Sub
        Private Function GetInstruction(Opcode As UInt16) As Instruction
            If (Me.Instructions.Where(Function(x) x.Opcode = Opcode).Any) Then
                Return Me.Instructions.Where(Function(x) x.Opcode = Opcode).FirstOrDefault
            End If
            Throw New Exception(String.Format("Undefined instruction at 0x{0} '{1}'", Me.Pointer.ToString("X"), Opcode.ToString))
        End Function
        Protected Friend Sub UpdateViewport(im As Image)
            RaiseEvent OnViewportUpdate(Me, im)
        End Sub
        Protected Overrides Sub Dispose(disposing As Boolean)
            Me.Display = Nothing
            Me.Instructions = Nothing
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace