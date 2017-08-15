Imports System.Text.RegularExpressions
Namespace Assembler
    Public Interface IGrammar
        ReadOnly Property Name As String
        ReadOnly Property Options As RegexOptions
        Property Rules As List(Of Rule)
    End Interface
End Namespace