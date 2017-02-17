Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Globalization.Calendar
Imports CrystalDecisions.CrystalReports.Engine



Public Class FrmRolePermission

    Private Sub FrmRolePermission_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CommonUtil.FillComboBox("CTR_FG_GetList", cmbRolePerm)



    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        'Dim report As New frmReportView()
        'Dim rpt As New crRolePermission()

        'rpt.SetParameterValue("@FG_SLNO", cmbRolePerm)




        'report.SetReport(rpt)
        'report.ShowDialog()
    End Sub
End Class