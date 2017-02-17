
Imports Microsoft.Office.Interop

Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmQualifiedAccWithEntityOwner

#Region "User defined code"
    Dim _formName As String = "ReportQualifiedAccWithOwnerEntity"
    Dim opt As SecForm = New SecForm(_formName)

    Dim log_message As String

    Private Sub ExportToXl()


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String

        Dim strSearch As String = ""

        If NullHelper.ObjectToString(txtAcNumber.Text.Trim()) <> "" Then

            strSearch = " And ACCOUNT=@ACCNUMBER "

        End If

        If txtDateFrom.Text.Trim() <> "/  /" And txtDateFrom.Text.Trim() <> "/  /" Then

            strSearch += " And DATE_TRANSACTION >=@DATE_FROM AND DATE_TRANSACTION <=@DATE_TO "

        End If



        strSql = "SELECT DISTINCT a.ACCOUNT, b.AC_TITLE, c.ENTITY_ID, e.DIRECTOR_ID , f.FIRST_NAME + ' ' + f.LAST_NAME as DIRECTOR_NAME , '' SignatoryID, '' SignatoryName , " & _
        " g.ADDRESS + ', Town : ' + g.TOWN + ', City : ' + g.CITY + ', Country : ' + g.COUNTRY_CODE as ADDRESS  ," & _
        " CONVERT(VARCHAR(11),a.DATE_TRANSACTION,106) QUALIFIED_DATE  " & _
        " FROM GO_TRANSACTION a " & _
        " INNER JOIN FIU_ACCOUNT_INFO b ON a.ACCOUNT = b.ACNUMBER AND b.STATUS = 'L' " & _
        " INNER JOIN GO_ACCOUNT_INFO c ON c.ACNUMBER = a.ACCOUNT AND c.STATUS ='L' " & _
        " LEFT JOIN GO_T_ENTITY d ON d.ENTITY_ID = c.ENTITY_ID AND d.STATUS = 'L' " & _
        " LEFT JOIN GO_DIRECTOR_ENTITY_MAP e ON e.ENTITY_ID =d.ENTITY_ID " & _
        " LEFT JOIN GO_DIRECTOR_INFO f ON f.DIRECTOR_ID = e.DIRECTOR_ID AND f.STATUS = 'L' " & _
        " LEFT JOIN GO_T_ENTITY_ADDRESS g ON d.ENTITY_ID = g.ENTITY_ID WHERE a.[STATUS]='L' " & strSearch & _
        " UNION ALL " & _
        " SELECT DISTINCT a.ACCOUNT, b.AC_TITLE, '', '', '',  ac.OWNER_CODE , o.OWNER_NAME, '', ''   FROM GO_TRANSACTION a " & _
        " INNER JOIN FIU_ACCOUNT_INFO b ON a.ACCOUNT = b.ACNUMBER AND b.STATUS = 'L' " & _
        " LEFT JOIN FIU_TRANS_AC_OWNER  ac ON ac.ACNUMBER = a.ACCOUNT AND ac.STATUS ='L' " & _
        " INNER JOIN FIU_OWNER_INFO o ON o.OWNER_CODE = ac.OWNER_CODE AND o.STATUS = 'L' WHERE a.[STATUS]='L' " & strSearch & _
        " Order By b.AC_TITLE, a.ACCOUNT "


        


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

        sheet.Name = "Qualified Account"



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
        log_message = " Showed : Qualified Account with Entity Owner (goAML) "
        Logger.system_log(log_message)

    End Sub

#End Region

    Private Sub FrmQualifiedAccWithEntityOwner_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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