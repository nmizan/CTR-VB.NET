Imports Microsoft.Office.Interop

Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmRptFLEXvsCCMS

#Region "User defined code"
    Dim _formName As String = "ReportFlexvsCCMS"
    Dim opt As SecForm = New SecForm(_formName)

    Dim log_message As String

    Private Sub ExportToXl()


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String

        Dim strSearch As String = ""

        If txtDateFrom.Text.Trim() <> "/  /" And txtDateFrom.Text.Trim() <> "/  /" Then

            strSearch = " TXN_DATE >=@DATE_FROM AND TXN_DATE <=@DATE_TO "

            strSql = " SELECT ACCOUNT, CONVERT(VARCHAR(11), TXN_DATE,106) As TXN_DATE , CTR_REF,  FLEX_AMOUNT, SLIPCOUNT, CCMS_AMOUNT, TRANS_SUM, (FLEX_AMOUNT- CCMS_AMOUNT) AMOUNT_DIFF, (SLIPCOUNT-TRANS_SUM) TRANS_DIFF  FROM GO_TMP_FLEX_VS_CCMS WHERE " & strSearch

        Else
            strSql = " SELECT ACCOUNT, CONVERT(VARCHAR(11), TXN_DATE,106) As TXN_DATE , CTR_REF,  FLEX_AMOUNT, SLIPCOUNT, CCMS_AMOUNT, TRANS_SUM, (FLEX_AMOUNT- CCMS_AMOUNT) AMOUNT_DIFF, (SLIPCOUNT-TRANS_SUM) TRANS_DIFF  FROM GO_TMP_FLEX_VS_CCMS "

        End If


        ' strSql = " SELECT ACCOUNT, TXN_DATE, CTR_REF,  FLEX_AMOUNT, SLIPCOUNT, CCMS_AMOUNT, TRANS_SUM  FROM GO_TMP_FLEX_VS_CCMS "




        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        dbCommand.Parameters.Clear()

        db.AddInParameter(dbCommand, "@DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim()))
        db.AddInParameter(dbCommand, "@DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim()))


        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        '--------------


        Dim xlApp As New Excel.Application

        Dim wb As Excel.Workbook = xlApp.Workbooks.Add

        Dim sheet As Excel.Worksheet = wb.Worksheets.Add

        sheet.Name = "CTR vs goAML Transaction Report"



        Dim i, j As Integer

        For j = 0 To ds.Tables(0).Columns.Count - 1
            sheet.Cells(1, j + 1) = ds.Tables(0).Columns(j).ColumnName
        Next j

        For i = 0 To ds.Tables(0).Rows.Count - 1
            For j = 0 To ds.Tables(0).Columns.Count - 1
                sheet.Cells(i + 2, j + 1) = ds.Tables(0).Rows(i)(j).ToString()
            Next j
        Next i
        CType(sheet.Rows(1, Type.Missing), Excel.Range).Font.Bold = True

        xlApp.Visible = True
        wb.Activate()
        log_message = " Showed : Flex vs CCMS Report "
        Logger.system_log(log_message)

    End Sub

#End Region

    Private Sub FrmRptFLEXvsCCMS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Try

            ExportToXl()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class