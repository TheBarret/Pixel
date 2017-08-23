Imports System.Text.RegularExpressions
Imports Pixel.Assembler

Namespace Grammars

    Public Class Pixel
        Implements IGrammar

        Sub New()
            Me.Rules = New List(Of Rule)
            Me.Rules.Add(New Rule(New Regex("^(\r\n|\r|\n|\|)", Me.Options), Types.T_END))
            Me.Rules.Add(New Rule(New Regex("^\s+", Me.Options), Types.T_SPACE))
            Me.Rules.Add(New Rule(New Regex("^(;)(.*?)(\r?\n|$)", Me.Options), Types.T_COMMENT))
            Me.Rules.Add(New Rule(New Regex("^\bPUSH\b", Me.Options), Types.OP_PUSH))
            Me.Rules.Add(New Rule(New Regex("^\bRSP\b", Me.Options), Types.OP_RSP))
            Me.Rules.Add(New Rule(New Regex("^\bLOAD\b", Me.Options), Types.OP_LD))
            Me.Rules.Add(New Rule(New Regex("^\bSTORE\b", Me.Options), Types.OP_ST))
            Me.Rules.Add(New Rule(New Regex("^\bSTOREV\b", Me.Options), Types.OP_STV))
            Me.Rules.Add(New Rule(New Regex("^\bJMP\b", Me.Options), Types.OP_JUMP))
            Me.Rules.Add(New Rule(New Regex("^\bCALL\b", Me.Options), Types.OP_CALL))
            Me.Rules.Add(New Rule(New Regex("^\bRETURN\b", Me.Options), Types.OP_RET))
            Me.Rules.Add(New Rule(New Regex("^\bADD\b", Me.Options), Types.OP_ADD))
            Me.Rules.Add(New Rule(New Regex("^\bSUB\b", Me.Options), Types.OP_SUB))
            Me.Rules.Add(New Rule(New Regex("^\bMUL\b", Me.Options), Types.OP_MUL))
            Me.Rules.Add(New Rule(New Regex("^\bDIV\b", Me.Options), Types.OP_DIV))
            Me.Rules.Add(New Rule(New Regex("^\bMOD\b", Me.Options), Types.OP_MOD))
            Me.Rules.Add(New Rule(New Regex("^\bAND\b", Me.Options), Types.OP_AND))
            Me.Rules.Add(New Rule(New Regex("^\bOR\b", Me.Options), Types.OP_OR))
            Me.Rules.Add(New Rule(New Regex("^\bXOR\b", Me.Options), Types.OP_XOR))
            Me.Rules.Add(New Rule(New Regex("^\bNOT\b", Me.Options), Types.OP_NOT))
            Me.Rules.Add(New Rule(New Regex("^\bINC\b", Me.Options), Types.OP_INC))
            Me.Rules.Add(New Rule(New Regex("^\bDEC\b", Me.Options), Types.OP_DEC))
            Me.Rules.Add(New Rule(New Regex("^\bIFN\b", Me.Options), Types.OP_IFN))
            Me.Rules.Add(New Rule(New Regex("^\bIFG\b", Me.Options), Types.OP_IFG))
            Me.Rules.Add(New Rule(New Regex("^\bIFL\b", Me.Options), Types.OP_IFL))
            Me.Rules.Add(New Rule(New Regex("^\bIFV\b", Me.Options), Types.OP_IFV))
            Me.Rules.Add(New Rule(New Regex("^\bIFNV\b", Me.Options), Types.OP_IFNV))
            Me.Rules.Add(New Rule(New Regex("^\bIFGV\b", Me.Options), Types.OP_IFGV))
            Me.Rules.Add(New Rule(New Regex("^\bIFLV\b", Me.Options), Types.OP_IFLV))
            Me.Rules.Add(New Rule(New Regex("^\bIF\b", Me.Options), Types.OP_IF))
            Me.Rules.Add(New Rule(New Regex("^\bINPUT\b", Me.Options), Types.OP_INPUT))
            Me.Rules.Add(New Rule(New Regex("^\bSHR\b", Me.Options), Types.OP_SHR))
            Me.Rules.Add(New Rule(New Regex("^\bSHL\b", Me.Options), Types.OP_SHL))
            Me.Rules.Add(New Rule(New Regex("^\bSTOV\b", Me.Options), Types.OP_OV))
            Me.Rules.Add(New Rule(New Regex("^\bSTCOL\b", Me.Options), Types.OP_COL))
            Me.Rules.Add(New Rule(New Regex("^\bSCROLL\b", Me.Options), Types.OP_SCR))
            Me.Rules.Add(New Rule(New Regex("^\bREADAT\b", Me.Options), Types.OP_READ))
            Me.Rules.Add(New Rule(New Regex("^\bWRITEAT\b", Me.Options), Types.OP_WRITE))
            Me.Rules.Add(New Rule(New Regex("^\bADDRESSOF\b", Me.Options), Types.OP_ADDR))
            Me.Rules.Add(New Rule(New Regex("^\bSEED\b", Me.Options), Types.OP_SEED))
            Me.Rules.Add(New Rule(New Regex("^\bRANDOM\b", Me.Options), Types.OP_RND))
            Me.Rules.Add(New Rule(New Regex("^\bCLEAR\b", Me.Options), Types.OP_CLS))
            Me.Rules.Add(New Rule(New Regex("^\bEND\b", Me.Options), Types.OP_END))
            Me.Rules.Add(New Rule(New Regex("^\bDRAW\b", Me.Options), Types.OP_DRAW))
            Me.Rules.Add(New Rule(New Regex("^\bPRINTV\b", Me.Options), Types.OP_PRINTV))
            Me.Rules.Add(New Rule(New Regex("^\bPRINT\b", Me.Options), Types.OP_PRINT))
            Me.Rules.Add(New Rule(New Regex("^\bSTRLEN\b", Me.Options), Types.OP_STRLEN))
            Me.Rules.Add(New Rule(New Regex("^\bSTRCMP\b", Me.Options), Types.OP_STRCMP))
            Me.Rules.Add(New Rule(New Regex("^0x[a-f0-9]+", Me.Options), Types.T_CONST_HEXADECIMAL))
            Me.Rules.Add(New Rule(New Regex("^[0-9]+", Me.Options), Types.T_VARIABLE))
            Me.Rules.Add(New Rule(New Regex("^(:)(?:[a-z][a-z0-9_]*)", Me.Options), Types.T_LABEL))
            Me.Rules.Add(New Rule(New Regex("^\[(?:[a-z][a-z0-9_]*)\]", Me.Options), Types.T_LOCATION))
            Me.Rules.Add(New Rule(New Regex("^\.((0|1)|\s+(0|1)){8}", Me.Options), Types.T_SPRITEDATA))
            Me.Rules.Add(New Rule(New Regex("^\#[a-z0-9]+", Me.Options), Types.T_CONST_NUMBER))
            Me.Rules.Add(New Rule(New Regex("^("".*?"")", Me.Options), Types.T_CONST_STRING))
            Me.Rules.Add(New Rule(New Regex("^\{(.*?)\}", Me.Options), Types.T_CONST_KEY))
        End Sub

        Public Property Rules As List(Of Rule) Implements IGrammar.Rules

        Public ReadOnly Property Name As String Implements IGrammar.Name
            Get
                Return "Pixel Assembly"
            End Get
        End Property

        Public ReadOnly Property Options As RegexOptions Implements IGrammar.Options
            Get
                Return RegexOptions.Singleline Or RegexOptions.IgnoreCase
            End Get
        End Property

    End Class

End Namespace