
Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization


Public Class FrmFlexTransaction

#Region "Global Variables"

    Dim _formName As String = "TransactionFlexTransaction"
    Dim opt As SecForm = New SecForm(_formName)


    Dim _intModno As Integer = 0
    Dim log_message As String = ""
#End Region

#Region "user defined codes"

    Private Sub LoadDataGrid()

        If dgView.Columns.Count = 0 Then Exit Sub

        Try

            ' dgView.DataSource = Nothing


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            'strSql = " SELECT * FROM IMP_FLEX_TRANS WHERE TXN_DATE>=@P_TXN_DATE_FROM AND TXN_DATE<=@P_TXN_DATE_TO"
            strSql = " SELECT * FROM IMP_FLEX_TRANS "

            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

            dbCommand.Parameters.Clear()



            db.AddInParameter(dbCommand, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
            db.AddInParameter(dbCommand, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))


            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            dgView.AutoGenerateColumns = True
            dgView.DataSource = ds
            dgView.DataMember = ds.Tables(0).TableName
            lblTotRecNo.Text = ds.Tables(0).Rows.Count


        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

#End Region

    Private Sub FrmFlexTransaction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        Dim i As Integer
        For i = 1 To dgView.Columns.Count - 1
            dgView.Columns(i).ReadOnly = True
        Next


        'txtDateFrom.Text = DateTime.Today.ToString("dd/MM/yyyy")

        'txtDateTo.Text = DateTime.Today.ToString("dd/MM/yyyy")

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objExp As New ExportUtil(dgView)

        objExp.ExportXl()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            Dim strSearch As String = ""

            If txtAccountNumber.Text.Trim() <> "" Then



                strSearch = "AND ACCOUNT like '%" & txtAccountNumber.Text.Trim() & "%'"

            End If

            If strSearch <> "" And txtDateFrom.Text.Trim() = "/  /" And txtDateTo.Text.Trim() = "/  /" Then


                strSql = " SELECT * FROM IMP_FLEX_TRANS WHERE TXN_DATE>=@P_TXN_DATE_FROM AND TXN_DATE<=@P_TXN_DATE_TO " & strSearch


            ElseIf txtDateFrom.Text.Trim() <> "/  /" And txtDateTo.Text.Trim() <> "/  /" And strSearch = "" Then


                strSql = " SELECT * FROM IMP_FLEX_TRANS WHERE TXN_DATE>=@P_TXN_DATE_FROM AND TXN_DATE<=@P_TXN_DATE_TO"

            Else

                strSql = " SELECT * FROM IMP_FLEX_TRANS WHERE TXN_DATE>=@P_TXN_DATE_FROM AND TXN_DATE<=@P_TXN_DATE_TO " & strSearch

            End If


            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

            dbCommand.Parameters.Clear()



            db.AddInParameter(dbCommand, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
            db.AddInParameter(dbCommand, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))


            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            dgView.AutoGenerateColumns = True
            dgView.DataSource = ds
            dgView.DataMember = ds.Tables(0).TableName

            lblTotRecNo.Text = ds.Tables(0).Rows.Count

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
End Class