Imports System.IO

Namespace Components

    Public MustInherit Class Memory
        Implements IDisposable
        Public Property Memory As Byte()

        Sub New()
            Me.Reset()
            Me.WriteBlock(Locations.Fonts, My.Resources.fonts)
            Me.WriteBlock(Locations.Keys, My.Resources.characters)
        End Sub

        Public Sub Reset()
            Me.Memory = New Byte(UInt16.MaxValue) {}
            For i As UInt16 = 0 To UInt16.MaxValue - 1
                Me.WriteByte(i, &H0)
            Next
        End Sub

        Public Property VRam(x As Int32, y As Int32, offset As UInt32) As Byte
            Get
                Return Me.ReadByte(CUShort(Locations.VRam + ((y * offset) + x)))
            End Get
            Set(value As Byte)
                Me.WriteByte(CUShort(Locations.VRam + ((y * offset) + x)), value)
            End Set
        End Property

        Public Property Pointer(Optional Offset As UInt16 = 0) As UInt16
            Get
                Return Me.ReadUInt(CUShort(Locations.Pointer + Offset))
            End Get
            Set(value As UInt16)
                Me.WriteUInt(CUShort(Locations.Pointer + Offset), value)
            End Set
        End Property

        Public Property Collision() As UInt16
            Get
                Return Me.ReadUInt(Locations.Collision)
            End Get
            Set(value As UInt16)
                Me.WriteUInt(Locations.Collision, value)
            End Set
        End Property

        Public Property Overflow() As UInt16
            Get
                Return Me.ReadUInt(Locations.Overflow)
            End Get
            Set(value As UInt16)
                Me.WriteUInt(Locations.Overflow, value)
            End Set
        End Property

        Public Property Address() As UInt16
            Get
                Return Me.ReadUInt(Locations.AddressPtr)
            End Get
            Set(value As UInt16)
                Me.WriteUInt(Locations.AddressPtr, value)
            End Set
        End Property

        Public Property Stack() As UInt16
            Get
                Return Me.ReadUInt(Locations.StackPtr)
            End Get
            Set(value As UInt16)
                Me.WriteUInt(Locations.StackPtr, value)
            End Set
        End Property

        Public Function PeekAddress() As UShort
            If (Me.Address >= 2) Then Return Me.ReadUInt(CUShort(Locations.Address + Me.Address - 2))
            Throw New Exception("Address stack out of range")
        End Function

        Public Function PopAddress() As UShort
            If (Me.Address >= 0) Then
                Try
                    Return Me.PeekAddress
                Finally
                    Me.Address = CUShort(Me.Address - 2)
                End Try
            End If
            Throw New Exception("Address stack out of range")
        End Function

        Public Sub PushAddress(Value As UInt16)
            If (Me.Address <= Locations.AddressMax) Then
                Me.WriteUInt(CUShort(Locations.Address + Me.Address), Value)
                Me.Address = CUShort(Me.Address + 2)
                Return
            End If
            Throw New Exception("Address stack out of range")
        End Sub

        Public Function Peek() As UShort
            If (Me.Stack >= 2) Then Return Me.ReadUInt(CUShort(Locations.Stack + Me.Stack - 2))
            Throw New Exception("Stack out of range")
        End Function

        Public Function Pop() As UShort
            If (Me.Stack >= 0) Then
                Try
                    Return Me.Peek
                Finally
                    Me.Stack = CUShort(Me.Stack - 2)
                End Try
            End If
            Throw New Exception("Stack out of range")
        End Function

        Public Sub Push(Value As UInt16)
            If (Me.Stack <= Locations.StackMax) Then
                Me.WriteUInt(CUShort(Locations.Stack + Me.Stack), Value)
                Me.Stack = CUShort(Me.Stack + 2)
                Return
            End If
            Throw New Exception("Stack out of range")
        End Sub

        Public Function NumberToSprite(num As Char) As Byte()
            If (Char.IsNumber(num)) Then
                Select Case num
                    Case "1"c : Return Me.ReadBlock(160, 5)
                    Case "2"c : Return Me.ReadBlock(166, 5)
                    Case "3"c : Return Me.ReadBlock(172, 5)
                    Case "4"c : Return Me.ReadBlock(178, 5)
                    Case "5"c : Return Me.ReadBlock(184, 5)
                    Case "6"c : Return Me.ReadBlock(190, 5)
                    Case "7"c : Return Me.ReadBlock(196, 5)
                    Case "8"c : Return Me.ReadBlock(202, 5)
                    Case "9"c : Return Me.ReadBlock(208, 5)
                    Case "0"c : Return Me.ReadBlock(214, 5)
                End Select
            End If
            Return Me.ReadBlock(340, 5)
        End Function

        Public Shared Sub Dump(Filename As String, Buffer() As Byte)
            If (File.Exists(Filename)) Then File.Delete(Filename)
            Using fs As New FileStream(Filename, FileMode.Create, FileAccess.Write, FileShare.None)
                Using bw As New BinaryWriter(fs)
                    bw.Write(Buffer)
                    bw.Flush()
                End Using
            End Using
        End Sub

        Public Function ReadByte(Address As Byte) As Byte
            If (Address >= 0 AndAlso Address <= UInt16.MaxValue) Then
                Return Me.Memory(Address)
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Function

        Public Function ReadByte(Address As UInt16) As Byte
            If (Address >= 0 AndAlso Address <= UInt16.MaxValue) Then
                Return Me.Memory(Address)
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

        Public Sub WriteByte(Address As UInt16, Value As Byte)
            If (Address >= 0 AndAlso Address <= UInt16.MaxValue) Then
                Me.Memory(Address) = Value
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

        Public Sub WriteBlock(Address As UInt16, ParamArray Values() As Byte)
            If (Address >= 0 AndAlso Address + Values.Length <= UInt16.MaxValue) Then
                For i As Integer = 0 To Values.Length - 1
                    Me.Memory(Address + i) = Values(i)
                Next
                Return
            End If
            Throw New Exception(String.Format("Memory address out of range '0x{0}'", Address.ToString("X")))
        End Sub

#Region "IDisposable Support"

        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    Me.Memory = Nothing
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