'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared

Imports Microsoft.Office.Interop

Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Public Class FrmReportMismatchFlexAc



#Region "User defined code"

    Dim _formName As String = "ReportMissmatchFlexAccount"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String
    Private Sub ExportToXl()


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String

        strSql = "select distinct i.ACCOUNT from (select ACCOUNT,TXN_DATE from IMP_FLEX_TRANS " & _
            " where len(account)=16 and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text & ") i " & _
            " left outer join FLEX_ACCOUNT f " & _
            " on i.ACCOUNT=f.ACCOUNT where f.ACCOUNT is null"

        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        '--------------


        Dim xlApp As New Excel.Application

        Dim wb As Excel.Workbook = xlApp.Workbooks.Add

        Dim sheet As Excel.Worksheet = wb.Worksheets.Add

        sheet.Name = "Mismatch Customer Acc"

        Dim i, j As Integer


        For i = 0 To ds.Tables(0).Rows.Count - 1
            For j = 0 To ds.Tables(0).Columns.Count - 1
                sheet.Cells(i + 1, j + 1) = ds.Tables(0).Rows(i)(j).ToString()
            Next j
        Next i

        xlApp.Visible = True
        wb.Activate()
        log_message = " Showed : Mismatch Flex Account Report "
        Logger.system_log(log_message)

    End Sub

#End Region



    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        'Dim frmReportViewer As New FrmReportViewer

        'Dim rd As New crMismatchFlexAc

        'rd.SetParameterValue("paramMonth", txtMonth.Text)
        'rd.SetParameterValue("paramYear", txtYear.Text)


        'frmReportViewer.SetReport(rd)

        'frmReportViewer.ShowDialog()

        Try

            ExportToXl()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FrmReportMismatchFlexAc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub
End Class