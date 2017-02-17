Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization


Public Class FrmCTRTransaction

#Region "Global Variables"

    Dim _formName As String = "MaintenanceReportCTRTransaction"
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

            strSql = " SELECT ft.ACNUMBER ,tr.TXN_BRANCH,CONVERT(varchar(10),ft.TRANSDATE,103) AS TRANSDATE,ft.TRTYPECODE ,ft.TRANSAMOUNT ,ft.SLNO ,ft.TRANSNUM ,ft.CURRENCY_CODE ,tr.TRN_CODE ,CONVERT(varchar(10),ft.REPORTING_MONTH, 103) AS REPORTING_MONTH  " + _
                     " FROM FIU_TRANSACTION ft " + _
                     " left join FIU_TRANSACTION_REPORT tr ON (tr.ACNUMBER = ft.ACNUMBER AND tr.TRANSDATE = ft.TRANSDATE AND tr.TRTYPECODE = ft.TRTYPECODE AND tr.REPORTING_MONTH = ft.REPORTING_MONTH ) " + _
                     " Order by ft.ACNUMBER "



            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

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


    Private Sub FrmCTRTransaction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        Dim i As Integer
        For i = 1 To dgView.Columns.Count - 1
            dgView.Columns(i).ReadOnly = True
        Next
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

    'Private Sub dgView_RowPrePaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles dgView.RowPrePaint
    '    If (e.RowIndex < dgView.Rows.Count - 1) Then
    '        If dgView.Rows(e.RowIndex).Cells(3).Value.ToString() = "03" Then
    '            dgView.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Pink

    '        ElseIf dgView.Rows(e.RowIndex).Cells(3).Value.ToString() = "04" Then
    '            dgView.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Tomato
    '        End If
    '    End If
    'End Sub

    

    Private Sub txtAccountNumber_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccountNumber.KeyDown


        If e.KeyCode = Keys.Enter Then


            Try


                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim strSql As String

                Dim strSearch As String = ""
                If txtAccountNumber.Text.Trim() <> "" Then
                    strSearch = " AND ft.ACNUMBER like '%" & txtAccountNumber.Text.Trim() & "%'"
                End If



                If strSearch <> "" Then



                    strSql = " SELECT ft.ACNUMBER ,tr.TXN_BRANCH,CONVERT(varchar(10),ft.TRANSDATE,103) AS TRANSDATE,ft.TRTYPECODE ,ft.TRANSAMOUNT ,ft.SLNO ,ft.TRANSNUM ,ft.CURRENCY_CODE ,tr.TRN_CODE ,CONVERT(varchar(10),ft.REPORTING_MONTH, 103) AS REPORTING_MONTH  " + _
                           " FROM FIU_TRANSACTION ft " + _
                           " left join FIU_TRANSACTION_REPORT tr ON (tr.ACNUMBER = ft.ACNUMBER AND tr.TRANSDATE = ft.TRANSDATE AND tr.TRTYPECODE = ft.TRTYPECODE AND tr.REPORTING_MONTH = ft.REPORTING_MONTH ) " + _
                           " Where ft.RPTYPECODE='CTR' " & strSearch + _
                           " Order by tr.ACNUMBER  "

                Else



                    strSql = " SELECT ft.ACNUMBER ,tr.TXN_BRANCH,CONVERT(varchar(10),ft.TRANSDATE,103) AS TRANSDATE,ft.TRTYPECODE ,ft.TRANSAMOUNT ,ft.SLNO ,ft.TRANSNUM ,ft.CURRENCY_CODE ,tr.TRN_CODE ,CONVERT(varchar(10),ft.REPORTING_MONTH, 103) AS REPORTING_MONTH  " + _
                         " FROM FIU_TRANSACTION ft " + _
                         " left join FIU_TRANSACTION_REPORT tr ON (tr.ACNUMBER = ft.ACNUMBER AND tr.TRANSDATE = ft.TRANSDATE AND tr.TRTYPECODE = ft.TRTYPECODE AND tr.REPORTING_MONTH = ft.REPORTING_MONTH ) " + _
                         " Order by tr.ACNUMBER "

                End If



                Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                dgView.AutoGenerateColumns = True
                dgView.DataSource = ds
                dgView.DataMember = ds.Tables(0).TableName

                lblTotRecNo.Text = ds.Tables(0).Rows.Count

            Catch ex As Exception

                MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            Dim strSearch As String = ""
            
            If txtAccountNumber.Text.Trim() <> "" Then
                strSearch = " AND ft.ACNUMBER like '%" & txtAccountNumber.Text.Trim() & "%'"
            End If

            If txtAmount.Text.Trim() <> "" Then
                strSearch += " AND ft.TRANSAMOUNT like '%" & txtAmount.Text.Trim() & "%'"
            End If

            If txtMonth.Text.Trim() <> "" Then
                strSearch += " AND month(ft.REPORTING_MONTH) =  '" & txtMonth.Text.Trim() & "'"
            End If


            'If txtDateFrom.Text.Trim() <> "" Then
            '    DateFrom = " AND ft.TRANSDATE >=  '" & NullHelper.StringToDate(txtDateFrom.Text.Trim()) & "'"
            'End If

            'If txtDateTo.Text.Trim() <> "" Then
            '    DateTo = " AND ft.TRANSDATE <=  '" & NullHelper.StringToDate(txtDateTo.Text.Trim()) & "'"
            'End If
            'MessageBox.Show(DateTo)

            If strSearch <> "" And txtDateFrom.Text.Trim() = "/  /" And txtDateTo.Text.Trim() = "/  /" Then



                strSql = " SELECT ft.ACNUMBER ,tr.TXN_BRANCH,CONVERT(varchar(10),ft.TRANSDATE,103) AS TRANSDATE,ft.TRTYPECODE ,ft.TRANSAMOUNT ,ft.SLNO ,ft.TRANSNUM ,ft.CURRENCY_CODE ,tr.TRN_CODE ,CONVERT(varchar(10),ft.REPORTING_MONTH, 103) AS REPORTING_MONTH  " + _
                       " FROM FIU_TRANSACTION ft " + _
                       " left join FIU_TRANSACTION_REPORT tr ON (tr.ACNUMBER = ft.ACNUMBER AND tr.TRANSDATE = ft.TRANSDATE AND tr.TRTYPECODE = ft.TRTYPECODE AND tr.REPORTING_MONTH = ft.REPORTING_MONTH ) " + _
                       " Where ft.RPTYPECODE='CTR' " & strSearch + _
                       " Order by tr.ACNUMBER  "




            ElseIf txtDateFrom.Text.Trim() <> "/  /" And txtDateTo.Text.Trim() <> "/  /" Then


                strSql = " SELECT ft.ACNUMBER ,tr.TXN_BRANCH,CONVERT(varchar(10),ft.TRANSDATE,103) AS TRANSDATE,ft.TRTYPECODE ,ft.TRANSAMOUNT ,ft.SLNO ,ft.TRANSNUM ,ft.CURRENCY_CODE ,tr.TRN_CODE ,CONVERT(varchar(10),ft.REPORTING_MONTH, 103) AS REPORTING_MONTH  " + _
                      " FROM FIU_TRANSACTION ft " + _
                      " left join FIU_TRANSACTION_REPORT tr ON (tr.ACNUMBER = ft.ACNUMBER AND tr.TRANSDATE = ft.TRANSDATE AND tr.TRTYPECODE = ft.TRTYPECODE AND tr.REPORTING_MONTH = ft.REPORTING_MONTH ) " + _
                      " Where ft.TRANSDATE >='" & NullHelper.StringToDate(txtDateFrom.Text) & "' AND ft.TRANSDATE <='" & NullHelper.StringToDate(txtDateTo.Text) & "'"

            Else
                strSql = " SELECT ft.ACNUMBER ,tr.TXN_BRANCH,CONVERT(varchar(10),ft.TRANSDATE,103) AS TRANSDATE,ft.TRTYPECODE ,ft.TRANSAMOUNT ,ft.SLNO ,ft.TRANSNUM ,ft.CURRENCY_CODE ,tr.TRN_CODE ,CONVERT(varchar(10),ft.REPORTING_MONTH, 103) AS REPORTING_MONTH  " + _
                     " FROM FIU_TRANSACTION ft " + _
                     " left join FIU_TRANSACTION_REPORT tr ON (tr.ACNUMBER = ft.ACNUMBER AND tr.TRANSDATE = ft.TRANSDATE AND tr.TRTYPECODE = ft.TRTYPECODE AND tr.REPORTING_MONTH = ft.REPORTING_MONTH ) " + _
                     " Order by tr.ACNUMBER "

            End If


            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            dgView.AutoGenerateColumns = True
            dgView.DataSource = ds
            dgView.DataMember = ds.Tables(0).TableName

            lblTotRecNo.Text = ds.Tables(0).Rows.Count

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub
End Class