﻿Imports System.Threading
Imports System.Windows.Forms
Imports Pixel.Tasks
Imports Pixel.Interfaces
Imports Pixel.Components
Public Class Machine
    Public Property Running As Boolean
    Public Property Wait As ManualResetEvent
    Protected Friend Property Viewport As Control
    Protected Friend Property Processor As Processor
    Protected Friend Property Tasks As List(Of Task)
    Protected Friend Property Bytecode As List(Of Byte)
    Public Event MachineActive()
    Public Event MachineInactive()
    Public Event Failure(Ex As Exception)
    Sub New(Viewport As Control)
        Me.Viewport = Viewport
    End Sub
    Public Sub Compile(Usercode As String, Optional Run As Boolean = False)
        Try
            Me.Bytecode = New List(Of Byte)
            Me.Tasks = New List(Of Task)
            Me.Tasks.Add(New Assembler(Me, Usercode))
            Me.Tasks.Add(New Optimizer(Me))
            For Each Task As Task In Me.Tasks
                Task.Initialize()
                Task.Execute()
            Next
            If (Run) Then
                Call New Thread(Sub() Me.Run(Me.Bytecode.ToArray)) With {.IsBackground = True}.Start()
            End If
        Catch ex As Exception
            RaiseEvent Failure(ex)
        End Try
    End Sub
    Public Sub Run(Bytecode() As Byte)
        Try
            Me.Processor = New Processor(Me)
            Processor.WriteBlock(Locations.Entrypoint, Bytecode)
            If (Me.Running) Then
                Me.Running = False
                Me.Wait.WaitOne()
            End If
            Me.Running = True
            Me.Wait = New ManualResetEvent(False)
            RaiseEvent MachineActive()
            Do
                Processor.Clock()
            Loop While Me.Running
            Memory.Dump(".\Dump.bin", Processor.ReadBlock(&H0, &HFFFF))
            Processor.Dispose()
        Catch ex As Exception
            RaiseEvent Failure(ex)
            Me.Running = False
        Finally
            Me.Wait.Set()
            RaiseEvent MachineInactive()
        End Try
    End Sub
    Public Sub KeyPressed(Params As KeyEventArgs)
        If (Me.Processor IsNot Nothing AndAlso Me.Running) Then
            Me.Processor.Keyboard.PressKey(ChrW(Params.KeyValue))
        End If
    End Sub
    Public Sub KeyReleased(Params As KeyEventArgs)
        If (Me.Processor IsNot Nothing AndAlso Me.Running) Then
            Me.Processor.Keyboard.ReleaseKey(ChrW(Params.KeyValue))
        End If
    End Sub
    Public Sub Abort()
        Me.Running = False
    End Sub

End Class
