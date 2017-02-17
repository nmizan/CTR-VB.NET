'
'Author             : Fahad Khan
'Purpose            : Maintain Bearer Information
'Creation date      : 22-Dec-2013
'Stored Procedure(s):  


Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmBearerSummary

#Region "user defined codes"

    Dim _formName As String = "MaintenanceGoBearerSummary"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""

    Private Sub LoadDataGrid()


        If dgView.Columns.Count = 0 Then Exit Sub

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Bearer_GetDetailList")

            commProc.Parameters.Clear()

            If chkShowAll.Checked = True Then
                db.AddInParameter(commProc, "@DEL_FLAG", DbType.Int32, 1)
            Else
                db.AddInParameter(commProc, "@DEL_FLAG", DbType.Int32, 0)

            End If

            If rdoAuthorized.Checked = True Then
                db.AddInParameter(commProc, "@AUTH_FLAG", DbType.Int32, 1)

            Else
                db.AddInParameter(commProc, "@AUTH_FLAG", DbType.Int32, 0)
            End If

            db.AddInParameter(commProc, "@ENTRY_DATE", DbType.DateTime, dtpEntryDate.Value)
            db.AddInParameter(commProc, "@to_DATE", DbType.DateTime, dtpToDate.Value)

            Dim dt As DataTable = db.ExecuteDataSet(commProc).Tables(0)

            Dim i As Integer

            dgView.Rows.Clear()

            For i = 0 To dt.Rows.Count - 1
                dgView.Rows.Add()
                dgView.Item(1, i).Value = dt.Rows(i).Item("MOD_NO").ToString()
                dgView.Item(2, i).Value = dt.Rows(i).Item("S").ToString()
                dgView.Item(3, i).Value = dt.Rows(i).Item("PERSON_ID").ToString()

                If dt.Rows(i).Item("GENDER").ToString() = "M" Then
                    dgView.Item(4, i).Value = "MALE"

                Else
                    dgView.Item(4, i).Value = "FEMALE"
                End If

                dgView.Item(5, i).Value = dt.Rows(i).Item("FIRST_NAME").ToString()
                dgView.Item(6, i).Value = dt.Rows(i).Item("LAST_NAME").ToString()
                dgView.Item(7, i).Value = dt.Rows(i).Item("MOTHERS_NAME").ToString()
                dgView.Item(8, i).Value = dt.Rows(i).Item("REFERENCE_NUMBER").ToString()
                dgView.Item(9, i).Value = dt.Rows(i).Item("INPUT_BY").ToString()
                dgView.Item(10, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INPUT_DATETIME"))
                dgView.Item(11, i).Value = dt.Rows(i).Item("INPUT_DATETIME")
                dgView.Item(12, i).Value = dt.Rows(i).Item("AUTH_BY").ToString()
                dgView.Item(13, i).Value = NullHelper.DateToString(dt.Rows(i).Item("AUTH_DATETIME"))


                dgView.Item(14, i).Value = NullHelper.DateToString(dt.Rows(i).Item("BIRTHDATE"))
                dgView.Item(15, i).Value = dt.Rows(i).Item("BIRTH_PLACE").ToString()
                dgView.Item(16, i).Value = dt.Rows(i).Item("ALIAS").ToString()
                dgView.Item(17, i).Value = dt.Rows(i).Item("SSN").ToString()
                dgView.Item(18, i).Value = dt.Rows(i).Item("PASSPORT_NUMBER").ToString()
                dgView.Item(19, i).Value = dt.Rows(i).Item("PASSPORT_COUNTRY").ToString()
                dgView.Item(20, i).Value = dt.Rows(i).Item("NATIONALITY1").ToString()

                dgView.Item(21, i).Value = dt.Rows(i).Item("NATIONALITY2").ToString()
                dgView.Item(22, i).Value = dt.Rows(i).Item("NATIONALITY3").ToString()
                dgView.Item(23, i).Value = dt.Rows(i).Item("RESIDENCE").ToString()
                dgView.Item(24, i).Value = dt.Rows(i).Item("EMAIL").ToString()
                dgView.Item(25, i).Value = dt.Rows(i).Item("OCCUPATION").ToString()

                dgView.Item(26, i).Value = dt.Rows(i).Item("EMPLOYER_NAME").ToString()

                dgView.Item(27, i).Value = dt.Rows(i).Item("TAX_NUMBER").ToString()

                dgView.Item(28, i).Value = dt.Rows(i).Item("TAX_REG_NUMBER").ToString()

                dgView.Item(29, i).Value = dt.Rows(i).Item("SOURCE_OF_WEALTH").ToString()
                dgView.Item(30, i).Value = dt.Rows(i).Item("COMMENTS").ToString()



            Next

            lblTotRecNo.Text = dt.Rows.Count

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

#End Region

    Private Sub FrmBearerSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
        dtpEntryDate.Value = DateTime.Today

        Dim i As Integer
        For i = 1 To dgView.Columns.Count - 1
            dgView.Columns(i).ReadOnly = True
        Next
        If rdoAuthorized.Checked = True Then
            dtpEntryDate.Enabled = True
            dtpToDate.Enabled = True
        End If
    End Sub


    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim frmPersonDetail As New FrmBearerDetail()
        frmPersonDetail.ShowDialog()
    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try


            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then

                Dim frmPersonDetail As New FrmBearerDetail(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
                frmPersonDetail.Show()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()
    End Sub
    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick
        If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then

            Dim frmPersonDetail As New FrmBearerDetail(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
            frmPersonDetail.Show()
        End If
    End Sub





    Private Sub dgView_RowPrePaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles dgView.RowPrePaint
        If (e.RowIndex < dgView.Rows.Count - 1) Then
            If dgView.Rows(e.RowIndex).Cells(2).Value.ToString() = "D" Then
                dgView.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Tomato

            ElseIf dgView.Rows(e.RowIndex).Cells(2).Value.ToString() = "U" Then
                dgView.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Pink
            End If
        End If
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged

        Dim rowsCount, i As Integer
        rowsCount = dgView.Rows.Count

        If chkAll.Checked = True Then
            For i = 0 To rowsCount - 1
                dgView(0, i).Value = True
            Next i
        ElseIf chkAll.Checked = False Then
            For i = 0 To rowsCount - 1
                dgView(0, i).Value = False
            Next i
        End If

    End Sub

    Private Sub rdoAuthorized_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAuthorized.CheckedChanged

        If rdoAuthorized.Checked = True Then
            btnAuthorize.Enabled = False
            chkAll.Visible = False
            If dgView.Columns.Count > 0 Then
                dgView.Columns(0).Visible = False
            End If
            chkShowAll.Visible = True
            dtpEntryDate.Enabled = True


        ElseIf rdoUnauthorized.Checked = True Then
            btnAuthorize.Enabled = True
            chkAll.Visible = True

            If dgView.Columns.Count > 0 Then
                dgView.Columns(0).Visible = True
            End If
            chkShowAll.Visible = False
            dtpEntryDate.Enabled = False

        End If

        LoadDataGrid()

    End Sub
    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Try

            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                Dim i As Integer

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Bearer_Auth")

                For i = 0 To dgView.Rows.Count - 1

                    If dgView.Rows(i).Cells(0).Value = True Then

                        commProc.Parameters.Clear()

                        db.AddInParameter(commProc, "@PERSON_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                        db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                        db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                        db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        Dim result As Integer
                        '-2146232060
                        Try
                            db.ExecuteNonQuery(commProc)
                            'Catch ex As DuplicateNameException
                            '    MessageBox.Show("Reference number exists !!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            '    Continue For
                        Catch ex As SqlClient.SqlException
                            If ex.ErrorCode = -2146232060 Then
                                MessageBox.Show("Reference number: " + NullHelper.ObjectToString(dgView.Rows(i).Cells(8).Value) + " exists !!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Continue For
                            End If
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Continue For

                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Continue For

                            'i = i + 1

                        End Try

                        'db.ExecuteNonQuery(commProc)
                        result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                        If result = 0 Then
                            lblToolStatus.Text = "!! Information Authorized Successfully !!"
                        ElseIf result = 1 Then

                            MessageBox.Show("Update not possible", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        ElseIf result = 3 Then
                            MessageBox.Show("Already authorized", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        ElseIf result = 4 Then
                            MessageBox.Show("Record not found", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        ElseIf result = 5 Then
                            MessageBox.Show("You cannot authorize the transaction", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ElseIf result = 7 Then
                            MessageBox.Show("Data mismatch! Reload records", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Else
                            MessageBox.Show("Auth Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    End If

                    log_message = "Authorized Bearer/Depositor Person " + dgView.Rows(i).Cells(3).Value.ToString() + " Name " + dgView.Rows(i).Cells(5).Value.ToString() + " " + dgView.Rows(i).Cells(6).Value.ToString()
                    Logger.system_log(log_message)
                Next i

            End If

        Catch ex As Exception

           
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        LoadDataGrid()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objExp As New ExportUtil(dgView)

        objExp.ExportXl()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub dtpEntryDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpEntryDate.ValueChanged
        LoadDataGrid()
    End Sub

    Private Sub chkShowAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowAll.CheckedChanged

    End Sub

    Private Sub dtpToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpToDate.ValueChanged
        LoadDataGrid()
    End Sub
End Class