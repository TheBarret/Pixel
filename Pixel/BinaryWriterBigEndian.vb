Imports System.IO
Public Class BinaryWriterBigEndian
    Implements IDisposable
    Private Property Stream As Stream
    Sub New(stream As Stream)
        Me.Stream = stream
    End Sub
    Public Sub Write(data As Byte())
        Me.Stream.Write(data, 0, data.Length)
    End Sub
    Public Sub Write(data As Byte(), offset As Integer, count As Integer)
        Me.Stream.Write(data, offset, count)
    End Sub
    Public Sub Write(data As Int16)
        Me.Stream.WriteByte(CByte((data >> 8) And &HFF))
        Me.Stream.WriteByte(CByte(data And &HFF))
    End Sub
    Public Sub Write(data As UInt16)
        Me.Stream.WriteByte(CByte((data >> 8) And &HFF))
        Me.Stream.WriteByte(CByte(data And &HFF))
    End Sub
    Public Sub Write(data As Byte)
        Me.Stream.WriteByte(data)
    End Sub
    Public Sub Flush()
        Me.Stream.Flush()
    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Me.Stream.Close()
                Me.Stream.Dispose()
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
