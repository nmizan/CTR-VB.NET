'
'Author             : Iftekharul Alam Khan Lodi
'Purpose            : About CCMS
'Creation date      : 
'Stored Procedure(s):  
'

Public Class FrmAbout

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Me.Close()

    End Sub

    Private Sub FrmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblAppName.Text = CommonAppSet.ModuleName + " (" + CommonAppSet.ModuleShortName + ")"
        lblVersion.Text = "Ver: " + Application.ProductVersion



    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

    '    Dim strTemp As String = "jld kdfj dj SLIPCOUN 34"

    '    Dim intPos As Integer = -1
    '    Dim TxnCount As Integer = 1
    '    Dim inc As Integer = 0
    '    Dim strCount As String = ""

    '    intPos = strTemp.IndexOf("SLIPCOUNT ")

    '    If intPos > 0 Then
    '        While (Char.IsDigit(strTemp(intPos + 10 + inc)))
    '            strCount = strCount + strTemp(intPos + 10 + inc)
    '            inc = inc + 1
    '            If intPos + 10 + inc = strTemp.Length Then
    '                Exit While
    '            End If
    '        End While

    '        Integer.TryParse(strCount, TxnCount)
    '        If TxnCount = 0 Then TxnCount = 1
    '    End If

    'End Sub
End Class