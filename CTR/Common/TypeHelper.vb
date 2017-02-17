Imports System.Globalization

Public Class TypeHelper

    Public Shared Function AccTypeFlexToGo(ByVal code As String) As String
        
        Dim rtnCode As String = ""
        Select Case code
            Case "C/A LCY"
                rtnCode = "D"

            Case "C/A LCY_COLLECTION"
                rtnCode = "D"

            Case "C/A OD"
                rtnCode = "D"

            Case "C/A OD-FD"
                rtnCode = "D"

            Case "C/A_PAYROLL"
                rtnCode = "D"

            Case "NON_RES_BLOCK_TKA/C"
                rtnCode = "D"

            Case "A/C Payable"
                rtnCode = "D"

            Case "Internal"
                rtnCode = "D"

            Case "S/A - PF"
                rtnCode = "S"

            Case "S/A LCY"
                rtnCode = "S"

            Case "SND LCY"
                rtnCode = "P"

            Case "NRTA"
                rtnCode = "G"

            Case "NITA"
                rtnCode = "H"

            Case "NON-CONVTBLE_TK_A/C"
                rtnCode = "I"

            Case "CONVTBLE_TK_A/C"
                rtnCode = "J"

            Case "C/A ERQ"
                rtnCode = "N"


        End Select




        Return rtnCode

    End Function

    

End Class
