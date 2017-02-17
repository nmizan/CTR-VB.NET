Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Globalization.Calendar
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmLogReport
    Dim _formName As String = "SystemLogReport"
    Dim opt As SecForm = New SecForm(_formName, CommonAppSet.IsAdmin)

    Private Sub frmLogReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtEntryDateFrom.Focus()
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click




        Dim report As New frmReportView()
        Dim rpt As New crSysLogReport()

        rpt.SetParameterValue("@DATE_FROM", NullHelper.StringToDate(txtEntryDateFrom.Text))
        rpt.SetParameterValue("@DATE_TO", NullHelper.StringToDate(txtEntryDateTo.Text))
        rpt.SetParameterValue("@USER_NAME", NullHelper.ObjectToNull(txtUser.Text.Trim()))
        rpt.SetParameterValue("@TEXT", NullHelper.ObjectToNull(txtText.Text.Trim()))

        report.SetReport(rpt)
        report.ShowDialog()



    End Sub


    Private Sub txtUser_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUser.KeyDown
        If e.KeyCode = Keys.Enter And txtUser.Text.Trim() <> "" Then
            SendKeys.Send("{tab}")
        End If
    End Sub

    Private Sub txtText_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtText.KeyDown
        If e.KeyCode = Keys.Enter And txtText.Text.Trim() <> "" Then
            SendKeys.Send("{tab}")
        End If
    End Sub

    Private Sub txtEntryDateFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEntryDateFrom.KeyDown
        If e.KeyCode = Keys.Enter And txtEntryDateFrom.Text.Trim() <> "" Then
            SendKeys.Send("{tab}")
        End If
    End Sub

    Private Sub txtEntryDateTo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEntryDateTo.KeyDown
        If e.KeyCode = Keys.Enter And txtEntryDateTo.Text.Trim() <> "" Then
            SendKeys.Send("{tab}")
        End If
    End Sub

    
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class