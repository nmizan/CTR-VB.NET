Imports Microsoft.Office.Interop

Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql



Public Class FrmRptCTRvsGoAML

#Region "User defined code"
    Dim _formName As String = "ReportCTRvsgoAML"
    Dim opt As SecForm = New SecForm(_formName)

    Dim log_message As String

    Private Sub ExportToXl()


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String

        Dim strSearch As String = ""

        If NullHelper.ObjectToString(txtAcNumber.Text.Trim()) <> "" Then

            strSearch = " And a.ACNUMBER=@ACCNUMBER "

        End If

        If txtDateFrom.Text.Trim() <> "/  /" And txtDateFrom.Text.Trim() <> "/  /" Then

            strSearch += " And a.TRANS_DATE >=@DATE_FROM AND a.TRANS_DATE <=@DATE_TO "

        End If


        'strSql = "select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
        '                 " from GO_TRANSACTION WHERE [STATUS]='L' " & strSearch & _
        '                 " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND "

        'strSql = "SELECT   a.ACNUMBER, CONVERT(VARCHAR(11), a.TRANSDATE,106) As TRANS_DATE, SUM ( CONVERT(NUMERIC(18,2), a.TRANSAMOUNT)) CTR_AMOUNT,  SUM(a.TRANSNUM) CTR_TRANS_SUM, " & _
        '         " b.AMOUNT GOAML_AMOUNT , b.NUMBER_OF_TRANSACTION GO_TRANS_SUM FROM " & _
        '         "FIU_TRANSACTION a INNER JOIN (select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, " & _
        '                       " sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
        '                       " from GO_TRANSACTION WHERE [STATUS]='L' " & _
        '                       " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND) " & _
        '         " b ON a.ACNUMBER = b.ACCOUNT AND a.TRANSDATE = b.DATE_TRANSACTION WHERE a.RPTYPECODE='CTR'  " & strSearch & _
        '         " GROUP BY a.ACNUMBER, a.TRANSDATE , b.AMOUNT , b.NUMBER_OF_TRANSACTION "

        strSql = " SELECT a.ACNUMBER, a.TRANS_DATE, b.DRCR_IND, a.CTR_AMOUNT,  a.CTR_TRANS_SUM, " & _
              " b.AMOUNT GOAML_AMOUNT , b.NUMBER_OF_TRANSACTION GO_TRANS_SUM, (a.CTR_AMOUNT - b.AMOUNT) AMOUNT_DIFF ,(a.CTR_TRANS_SUM- b.NUMBER_OF_TRANSACTION) TRANS_DIFF " & _
              " FROM  ( SELECT   ACNUMBER, CONVERT(VARCHAR(11), TRANSDATE,106) As TRANS_DATE, " & _
              " SUM ( CONVERT(NUMERIC(18,2), TRANSAMOUNT)) CTR_AMOUNT,  SUM(TRANSNUM) CTR_TRANS_SUM,  " & _
              " CASE TRTYPECODE " & _
             " WHEN 01 THEN 'C' " & _
             " WHEN 02 THEN 'D'  " & _
             " WHEN 05 THEN 'C' " & _
             " WHEN 06 THEN 'D' " & _
             " WHEN 04 THEN 'C' " & _
             " WHEN 18 THEN 'D' " & _
             " WHEN 08 THEN 'C' " & _
             " WHEN 19 THEN 'D' " & _
             " WHEN 03 THEN 'C' " & _
             " END as DRCR_IND, RPTYPECODE " & _
                      " FROM " & _
                      " FIU_TRANSACTION GROUP BY ACNUMBER, TRANSDATE , CASE TRTYPECODE " & _
             " WHEN 01 THEN 'C' " & _
             " WHEN 02 THEN 'D'  " & _
             " WHEN 05 THEN 'C' " & _
             " WHEN 06 THEN 'D' " & _
             " WHEN 04 THEN 'C' " & _
             " WHEN 18 THEN 'D' " & _
             " WHEN 08 THEN 'C' " & _
             " WHEN 19 THEN 'D' " & _
             " WHEN 03 THEN 'C' " & _
            " END, RPTYPECODE ) a " & _
                     " FULL OUTER JOIN (select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, " & _
                                       " sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
                                       " from GO_TRANSACTION WHERE [STATUS]='L'  " & _
                                       " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND)  b " & _
            " ON a.ACNUMBER = b.ACCOUNT AND a.TRANS_DATE = b.DATE_TRANSACTION AND a.DRCR_IND = b.DRCR_IND WHERE a.RPTYPECODE ='CTR' " & strSearch




        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        dbCommand.Parameters.Clear()

        db.AddInParameter(dbCommand, "@ACCNUMBER", DbType.String, txtAcNumber.Text.Trim())

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
        log_message = " Showed : CTR vs GoAML Report "
        Logger.system_log(log_message)

    End Sub

#End Region


    Private Sub FrmRptCTRvsGoAML_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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