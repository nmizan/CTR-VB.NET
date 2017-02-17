'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared

Imports Microsoft.Office.Interop

Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Public Class FrmReportFileImportStatus



#Region "User defined code"

    Dim _formName As String = "ReportFileImportStatus"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String
    Private Sub ExportToXl()


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String

        strSql = "select convert(varchar,IMPORTED_DATE,107) Date,'CTR_Status'=" & _
        " case " & _
        "	when STATUS='P' then 'Processed' " & _
        "	when STATUS='N' then 'Not processed' " & _
        "        End, " & _
        "'Go_Status'=" & _
        " case " & _
        "	when GOSTATUS='P' then 'Processed' " & _
        "	ELSE 'Not processed' " & _
        "        End " & _
        "  , INPUT_BY, AUTH_BY      from STATUS_IMP_FLEX_TRANS " & _
        "        where Month(IMPORTED_DATE) = " & txtMonth.Text & " And Year(IMPORTED_DATE) = " & txtYear.Text

        '"	when GOSTATUS='N' then 'Not processed' " & _
        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        '--------------


        Dim xlApp As New Excel.Application

        Dim wb As Excel.Workbook = xlApp.Workbooks.Add

        Dim sheet As Excel.Worksheet = wb.Worksheets.Add

        sheet.Name = "File Import Status"

        Dim i, j, k As Integer

        
        For k = 0 To ds.Tables(0).Columns.Count - 1
            sheet.Cells(1, k + 1) = ds.Tables(0).Columns(k).ColumnName



        Next k

        For i = 0 To ds.Tables(0).Rows.Count - 1

            

            For j = 0 To ds.Tables(0).Columns.Count - 1
                sheet.Cells(i + 2, j + 1) = ds.Tables(0).Rows(i)(j).ToString()

            Next j
        Next i

        xlApp.Visible = True
        wb.Activate()
        log_message = " Showed : File Import Status Report "
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

    Private Sub FrmReportFileImportStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub
End Class