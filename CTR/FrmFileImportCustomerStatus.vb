'
'Author             : Fahad Khan
'Purpose            : Report File Import Status(goAML)
'Creation date      : 17-Nov-13
'


'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared

Imports Microsoft.Office.Interop
Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmFileImportCustomerStatus

#Region "User defined code"

    Dim _formName As String = "ReportFileImportCustomerStatusGOAML"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String

    Private Sub ExportToXl()


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)
        Dim dtTransation As DataTable


        'Dim strSql As String


        'strSql = "select convert(varchar,IMPORTED_DATE,107),'Stat'=" & _
        '" case " & _
        '"	when GOSTATUS='P' then 'Processed' " & _
        '"	when GOSTATUS='N' then 'Not processed' " & _
        '"        End " & _
        '"        from STATUS_IMP_FLEX_CUST " & _
        '"   WHERE IMPORTED_DATE>=" & NullHelper.StringToDate(txtDateFrom.Text) & " AND IMPORTED_DATE<= " & NullHelper.StringToDate(txtDateTo.Text)


        Dim commTrans As DbCommand

        commTrans = db.GetSqlStringCommand("select convert(varchar,IMPORTED_DATE,107),'Stat'=" & _
                                            " case " & _
                                            "	when STATUS='P' then 'Processed' " & _
                                            "	when STATUS='N' then 'Not processed' " & _
                                            "        End " & _
                                            "        from STATUS_IMP_FLEX_CUST " & _
                                            "   WHERE IMPORTED_DATE>=@P_DATE_FROM  AND IMPORTED_DATE<= @P_DATE_TO ")

        commTrans.Parameters.Clear()

        db.AddInParameter(commTrans, "@P_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
        db.AddInParameter(commTrans, "@P_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

        dtTransation = db.ExecuteDataSet(commTrans).Tables(0)





        'Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        'Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        '--------------


        Dim xlApp As New Excel.Application

        Dim wb As Excel.Workbook = xlApp.Workbooks.Add

        Dim sheet As Excel.Worksheet = wb.Worksheets.Add

        sheet.Name = "File Import Status (goAML)"

        Dim i, j As Integer


        For i = 0 To dtTransation.Rows.Count - 1
            For j = 0 To dtTransation.Columns.Count - 1
                sheet.Cells(i + 1, j + 1) = dtTransation.Rows(i)(j).ToString()

            Next j
        Next i

        xlApp.Visible = True
        wb.Activate()
        log_message = " Showed : File Import Status Report For goAML "
        Logger.system_log(log_message)

    End Sub

    Private Function CheckData() As Boolean

        If txtDateFrom.Text.Trim() = "/  /" Then
            MessageBox.Show("Date From required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtDateFrom.Focus()
            Return False
        End If

        If txtDateTo.Text.Trim() = "/  /" Then
            MessageBox.Show("Date To required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtDateTo.Focus()
            Return False
        End If

        Return True

    End Function

#End Region





    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            If CheckData() Then
                ExportToXl()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FrmFileImportCustomerStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class