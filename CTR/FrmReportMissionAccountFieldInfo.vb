Imports Microsoft.Office.Interop

Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmReportMissionAccountFieldInfo

#Region "User defined code"
    Dim _formName As String = "ReportMissingAccountRequiredFieldGo"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String
    Private Sub ExportToXl()


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String


        strSql = "select t.ACCOUNT,o.BANK_CODE,o.BRANCH_CODE,o.AC_TITLE,o.ACTYPECODE,o.OWTYPECODE,o.DECLARED_DEPOSIT_AMOUNT,o.DECLARED_DEPOSIT_TRANSNO,o.DECLARED_DEPOSIT_MAXAMOUNT,o.DECLARED_WITHDR_AMOUNT,o.DECLARED_WITHDR_TRANSNO,o.DECLARED_WITHDR_MAXAMOUNT,o.TIN,o.BIN,o.VAT_REG_NO,o.VAT_REG_DATE,o.COMPANY_REG_NO,o.COMPANY_REG_DATE,o.REG_AUTHORITY_CODE,o.PRES_ADDR, o.PERM_ADDR, o.PHONE_RES1, o.PHONE_RES2, o.PHONE_OFFICE1, o.PHONE_OFFICE2,o.MOBILE1, o.MOBILE2,g.CURRENCY_CODE AS GO_CURRENCY_CODE,g.OPENED AS GO_OPEN_DATE,g.ACCOUNT_TYPE AS GO_ACCOUNT_TYPE,g.STATUS_CODE AS GO_STATUS_CODE,g.IBAN, g.CLIENT_NUMBER,g.CLOSED AS GO_ACCOUNT_CLOSE_DATE, g.BENEFICIARY, g.BENEFICIARY_COMMENTS, g.ENTITY_ID  from  " & _
            " (select distinct ACCOUNT from  GO_TRANSACTION " & _
            " where DATE_TRANSACTION>=@P_TXN_DATE_FROM AND DATE_TRANSACTION<=@P_TXN_DATE_TO " & _
            " ) t  " & _
            " left outer join (select BANK_CODE,BRANCH_CODE,AC_TITLE,ACNUMBER,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2 from FIU_ACCOUNT_INFO where STATUS='L') o " & _
            " on t.ACCOUNT=o.ACNUMBER " & _
            " left outer join (SELECT CURRENCY_CODE,OPENED,ACCOUNT_TYPE,STATUS_CODE,ACNUMBER,IBAN, CLIENT_NUMBER,CLOSED, BENEFICIARY, BENEFICIARY_COMMENTS, ENTITY_ID FROM GO_ACCOUNT_INFO WHERE STATUS='L') g " & _
            " on o.ACNUMBER=g.ACNUMBER " & _
            "order by t.ACCOUNT"


        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        dbCommand.Parameters.Clear()

        db.AddInParameter(dbCommand, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
        db.AddInParameter(dbCommand, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))


        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        '--------------


        Dim xlApp As New Excel.Application

        Dim wb As Excel.Workbook = xlApp.Workbooks.Add

        Dim sheet As Excel.Worksheet = wb.Worksheets.Add

        sheet.Name = "Missing Account Info"



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

        log_message = " Showed : Missing Required Account Information Field Report "
        Logger.system_log(log_message)

    End Sub

#End Region



    Private Sub FrmReportMissionAccountFieldInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click

        Try

            ExportToXl()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class