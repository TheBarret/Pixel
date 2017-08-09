﻿Imports System.IO
Namespace Components
    Public MustInherit Class Memory
        Private Property Data As Byte()
        Sub New()
            Me.Reset()
        End Sub
        Public Sub LoadFonts()
            Me.WriteBlock(&H4, My.Resources.fonts)
        End Sub
        Public Sub Reset()
            Me.Data = New Byte(UInt16.MaxValue) {}
            For i As UInt16 = 0 To UInt16.MaxValue - 1
                Me.WriteByte(i, &H0)
            Next
        End Sub
        Public Property Pointer(Optional Offset As UInt16 = 0) As UInt16
            Get
                Return Me.ReadUInt(CUShort(Locations.Pointer + Offset))
            End Get
            Set(value As UInt16)
                Me.WriteUInt(CUShort(Locations.Pointer + Offset), value)
            End Set
        End Property
        Public Sub Dump(Filename As String, Buffer() As Byte)
            Using fs As New FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None)
                Using bw As New BinaryWriter(fs)
                    bw.Write(Buffer)
                End Using
            End Using
        End Sub
        Public Function ReadByte(Address As Byte) As Byte
            If (Address >= 0 AndAlso Address <= UInt16.MaxValue) Then
                Return Me.Data(Address)
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Function
        Public Function ReadByte(Address As UInt16) As Byte
            If (Address >= 0 AndAlso Address <= UInt16.MaxValue) Then
                Return Me.Data(Address)
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Function
        Public Function ReadUInt(Address As Byte) As UInt16
            If (Address >= 0 AndAlso Address <= UInt16.MaxValue) Then
                Return BitConverter.ToUInt16(New Byte() {Me.ReadByte(CByte(Address + 1)), Me.ReadByte(Address)}, 0)
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Function
        Public Function ReadUInt(Address As UInt16) As UInt16
            If (Address >= 0 AndAlso Address <= UInt16.MaxValue) Then
                Return BitConverter.ToUInt16(New Byte() {Me.ReadByte(CUShort(Address + 1)), Me.ReadByte(Address)}, 0)
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Function
        Public Sub WriteByte(Address As Byte, Value As Byte)
            If (Address >= 0 AndAlso Address <= UInt16.MaxValue) Then
                Me.Data(Address) = Value
                Return
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Sub
        Public Sub WriteByte(Address As UInt16, Value As Byte)
            If (Address >= 0 AndAlso Address <= UInt16.MaxValue) Then
                Me.Data(Address) = Value
                Return
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Sub
        Public Sub WriteUInt(Address As Byte, Value As UInt16)
            If (Address >= 0 AndAlso Address <= UInt16.MaxValue) Then
                Dim data() As Byte = BitConverter.GetBytes(Value)
                Me.WriteByte(Address, data(1))
                Me.WriteByte(CByte(Address + 1), data(0))
                Return
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Sub
        Public Sub WriteUInt(Address As UInt16, Value As UInt16)
            If (Address >= 0 AndAlso Address <= UInt16.MaxValue) Then
                Dim data() As Byte = BitConverter.GetBytes(Value)
                Me.WriteByte(Address, data(1))
                Me.WriteByte(CUShort(Address + 1), data(0))
                Return
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Sub
        Public Function ReadBlock(Address As Byte, Length As Byte) As Byte()
            If (Address >= 0 AndAlso Address + Length <= UInt16.MaxValue) Then
                Dim data() As Byte = New Byte(Length) {}
                For i As Integer = 0 To Length
                    data(i) = Me.ReadByte(CByte(Address + i))
                Next
                Return data
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Function
        Public Function ReadBlock(Address As UInt16, Length As UInt16) As Byte()
            If (Address >= 0 AndAlso Address + Length <= UInt16.MaxValue) Then
                Dim data() As Byte = New Byte(Length) {}
                For i As Integer = 0 To Length
                    data(i) = Me.ReadByte(CUShort(Address + i))
                Next
                Return data
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Function
        Public Sub WriteBlock(Address As Byte, ParamArray Values() As Byte)
            If (Address >= 0 AndAlso Address + Values.Length <= UInt16.MaxValue) Then
                For i As Integer = 0 To Values.Length - 1
                    Me.Data(Address + i) = Values(i)
                Next
                Return
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Sub
        Public Sub WriteBlock(Address As UInt16, ParamArray Values() As Byte)
            If (Address >= 0 AndAlso Address + Values.Length <= UInt16.MaxValue) Then
                For i As Integer = 0 To Values.Length - 1
                    Me.Data(Address + i) = Values(i)
                Next
                Return
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Sub
    End Class
End Namespace