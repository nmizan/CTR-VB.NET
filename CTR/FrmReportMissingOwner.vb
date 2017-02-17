'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
Imports Microsoft.Office.Interop

Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Public Class FrmReportMissingOwner

#Region "User defined code"
    Dim _formName As String = "ReportMissingOwner"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String
    Private Sub ExportToXl()


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String

        'strSql = "select t.ACCOUNT from " & _
        '" (select distinct ACCOUNT from FLEX_TRANS_BY_RULE " & _
        '" where year(TXN_DATE)=" & txtYear.Text & " and month(TXN_DATE)=" & txtMonth.Text & ") t " & _
        '" where t.ACCOUNT not in (select distinct ACNUMBER from FIU_TRANS_AC_OWNER where STATUS='L') "

        strSql = "select t.ACNUMBER,f.OWNER_CODE,o.OWNER_NAME from  " & _
                " (select distinct ACNUMBER from FIU_TRANSACTION  " & _
                " where year(TRANSDATE)=" & txtYear.Text & " and month(TRANSDATE)=" & txtMonth.Text & ") t  " & _
                " left outer join (select ACNUMBER,OWNER_CODE from FIU_TRANS_AC_OWNER where STATUS='L') f " & _
                " on t.ACNUMBER=f.ACNUMBER " & _
                " left outer join (select OWNER_CODE,OWNER_NAME from FIU_OWNER_INFO where STATUS='L') o " & _
                " on f.OWNER_CODE=o.OWNER_CODE " & _
                " order by t.ACNUMBER,f.OWNER_CODE"






        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        '--------------


        Dim xlApp As New Excel.Application

        Dim wb As Excel.Workbook = xlApp.Workbooks.Add

        Dim sheet As Excel.Worksheet = wb.Worksheets.Add

        sheet.Name = "Missig Owner Info"



        Dim i, j As Integer

        For j = 0 To ds.Tables(0).Columns.Count - 1
            sheet.Cells(1, j + 1) = ds.Tables(0).Columns(j).ColumnName
        Next j

        For i = 0 To ds.Tables(0).Rows.Count - 1
            For j = 0 To ds.Tables(0).Columns.Count - 1
                sheet.Cells(i + 2, j + 1) = ds.Tables(0).Rows(i)(j).ToString()
            Next j
        Next i

        xlApp.Visible = True
        wb.Activate()

        log_message = " Showed : Missing Owner Report  "
        Logger.system_log(log_message)

    End Sub

#End Region


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub


    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        'Dim frmReportViewer As New FrmReportViewer

        'Dim rd As New crMissingOwnerinfo

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

    Private Sub FrmReportMissingOwner_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub
End Class