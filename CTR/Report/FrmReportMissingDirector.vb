
'/**************************/
' From name: frmReportMissingDirector
' Author   : Md. Fahad khan
' Create date : 27-04-2014
'/**************************/


Imports Microsoft.Office.Interop
Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql




Public Class FrmReportMissingDirector


#Region "User defined code"
    Dim _formName As String = "ReportMissingDirectorInfo"
    Dim opt As SecForm = New SecForm(_formName)

    Dim log_message As String


    Private Sub ExportToXl()


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String

        Dim strSearch As String = ""


        If txtDateFrom.Text.Trim() <> "/  /" And txtDateFrom.Text.Trim() <> "/  /" Then

            strSearch = " And a.DATE_TRANSACTION >=@DATE_FROM AND a.DATE_TRANSACTION <=@DATE_TO "

        End If

        If strSearch <> "" Then

            strSql = "select distinct a.ACCOUNT,b.ENTITY_ID,c.DIRECTOR_ID  FROM GO_TRANSACTION a left join " & _
                     " GO_ACCOUNT_INFO b ON b.ACNUMBER =a.ACCOUNT left join GO_DIRECTOR_ENTITY_MAP c " & _
                     " on c.ENTITY_ID = b.ENTITY_ID where a.[STATUS]='L' " & strSearch & _
                     " and b.ENTITY_ID is not null and c.DIRECTOR_ID is null "

        Else

            strSql = "select distinct a.ACCOUNT,b.ENTITY_ID,c.DIRECTOR_ID  FROM GO_TRANSACTION a left join " & _
                     " GO_ACCOUNT_INFO b ON b.ACNUMBER =a.ACCOUNT left join GO_DIRECTOR_ENTITY_MAP c " & _
                     " on c.ENTITY_ID = b.ENTITY_ID where a.[STATUS]='L' and b.ENTITY_ID is not null and c.DIRECTOR_ID is null "

        End If


        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        dbCommand.Parameters.Clear()


        db.AddInParameter(dbCommand, "@DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim()))
        db.AddInParameter(dbCommand, "@DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim()))


        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        '--------------


        Dim xlApp As New Excel.Application

        Dim wb As Excel.Workbook = xlApp.Workbooks.Add

        Dim sheet As Excel.Worksheet = wb.Worksheets.Add

        sheet.Name = "Missing Director Info Report"



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
        log_message = " Showed : Missing Director Information (goAML) "
        Logger.system_log(log_message)

    End Sub


#End Region

    Private Sub FrmReportMissingDirector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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