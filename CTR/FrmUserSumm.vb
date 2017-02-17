
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Public Class FrmUserSumm

    Dim _formName As String = "SystemUsersSummary"
    Dim opt As SecForm = New SecForm(_formName, CommonAppSet.IsAdmin)
    Dim log_message As String = ""

    Dim _userName As String = ""
    Dim _deptName As String = ""
    Dim _domain As String = ""
    Dim _udeptName As String = ""

    Dim _uName As String = ""
    Dim _departName As String = ""
    Dim _domainName As String = ""
    Dim _adepartName As String = ""


#Region "user defined codes"

    Private Sub LoadDataGrid()

        Try

            If dgView.Columns.Count = 0 Then Exit Sub


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_Users_GetDetailList")

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

            Dim dt As DataTable = db.ExecuteDataSet(commProc).Tables(0)

            Dim i As Integer

            dgView.Rows.Clear()

            For i = 0 To dt.Rows.Count - 1
                dgView.Rows.Add()
                dgView.Item(1, i).Value = dt.Rows(i).Item("SLNO").ToString()
                dgView.Item(2, i).Value = dt.Rows(i).Item("MOD_NO").ToString()
                dgView.Item(3, i).Value = dt.Rows(i).Item("S").ToString()
                dgView.Item(4, i).Value = dt.Rows(i).Item("USERS_ID").ToString()
                dgView.Item(5, i).Value = dt.Rows(i).Item("USERS_NAME").ToString()
                dgView.Item(6, i).Value = dt.Rows(i).Item("DEPT_NAME").ToString()
                dgView.Item(7, i).Value = dt.Rows(i).Item("USER_STAT").ToString()
                dgView.Item(8, i).Value = dt.Rows(i).Item("INPUT_BY").ToString()
                dgView.Item(9, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INPUT_DATETIME"))
                dgView.Item(10, i).Value = dt.Rows(i).Item("INPUT_DATETIME")
                dgView.Item(11, i).Value = dt.Rows(i).Item("AUTH_BY").ToString()
                dgView.Item(12, i).Value = NullHelper.DateToString(dt.Rows(i).Item("AUTH_DATETIME"))
            Next


        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

#End Region


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub FrmUserSumm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        'LoadDataGrid()


        Dim i As Integer
        For i = 1 To dgView.Columns.Count - 1
            dgView.Columns(i).ReadOnly = True
        Next

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()

    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try


            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then
                Dim frmUserDet As New FrmUsersDet(dgView.SelectedRows.Item(0).Cells(4).Value, dgView.SelectedRows.Item(0).Cells(2).Value)
                frmUserDet.ShowDialog()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim frmUserDet As New FrmUsersDet
        frmUserDet.ShowDialog()

    End Sub


    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick

        If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then
            Dim frmUserDet As New FrmUsersDet(dgView.SelectedRows.Item(0).Cells(4).Value, dgView.SelectedRows.Item(0).Cells(2).Value)
            frmUserDet.ShowDialog()
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



        ElseIf rdoUnauthorized.Checked = True Then
            If opt.IsAuth = True Then
                btnAuthorize.Enabled = True
            Else
                btnAuthorize.Enabled = False
            End If
            chkAll.Visible = True

            If dgView.Columns.Count > 0 Then
                dgView.Columns(0).Visible = True
            End If
            chkShowAll.Visible = False

            End If

            LoadDataGrid()
    End Sub

    '----------Mizan Work (25-04-2016)---------------

    Private Sub LoadUsersDataForAuth(ByVal strUsersCode As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From USERS Where USERS_ID ='" & strUsersCode & "' and STATUS= 'L' ")

            If ds.Tables(0).Rows.Count > 0 Then



                ''txtName.Text = ds.Tables(0).Rows(0)("USERS_NAME").ToString()
                _uName = ds.Tables(0).Rows(0)("USERS_NAME").ToString()
                '' cmbDept.SelectedValue = ds.Tables(0).Rows(0)("DEPT_SLNO")
                _departName = ds.Tables(0).Rows(0)("DEPT_SLNO").ToString()

                'txtDomain.Text = ds.Tables(0).Rows(0)("DOMAIN").ToString()
                _domainName = ds.Tables(0).Rows(0)("DOMAIN").ToString()

                '--------------Mizan Work (26-04-2016---------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From DEPARTMENT Where SLNO ='" & _departName & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _adepartName = ds2.Tables(0).Rows(0)("DEPT_NAME").ToString()
                    _departName = _adepartName

                End If
                '--------------Mizan Work (26-04-2016---------


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '----------Mizan Work (25-04-2016)---------------

    Private Sub LoadUsersData(ByVal strUsersCode As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_Users_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@USERS_ID", DbType.String, strUsersCode)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intMod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then

                'txtName.Text = ds.Tables(0).Rows(0)("USERS_NAME").ToString()
                _userName = ds.Tables(0).Rows(0)("USERS_NAME").ToString()
                ' cmbDept.SelectedValue = ds.Tables(0).Rows(0)("DEPT_SLNO")
                _deptName = ds.Tables(0).Rows(0)("DEPT_SLNO").ToString()

                'txtDomain.Text = ds.Tables(0).Rows(0)("DOMAIN").ToString()
                _domain = ds.Tables(0).Rows(0)("DOMAIN").ToString()

                '--------------Mizan Work (26-04-2016---------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From DEPARTMENT Where SLNO ='" & _deptName & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _udeptName = ds2.Tables(0).Rows(0)("DEPT_NAME").ToString()
                    _deptName = _udeptName

                End If
                '--------------Mizan Work (26-04-2016---------

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Try

            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then



                '--------------Mizan Work (25-04-16)---------------------

                Dim i As Integer

                For i = 0 To dgView.Rows.Count - 1


                    If dgView.Rows(i).Cells(0).Value = True Then

                        If dgView.Rows(i).Cells(8).Value.ToString = CommonAppSet.User.Trim() Then

                            MessageBox.Show("Maker can't verify data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else

                            LoadUsersData(dgView.Rows(i).Cells(4).Value.ToString(), dgView.Rows(i).Cells(2).Value)


                            If (dgView.Rows(i).Cells(2).Value) > 1 Then

                                LoadUsersDataForAuth(dgView.Rows(i).Cells(4).Value.ToString())

                                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                                Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_Users_Auth")

                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@USERS_ID", DbType.String, dgView.Rows(i).Cells(4).Value)
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(2).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(10).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "Information Authorized Successfully !! "

                                    If _uName <> _userName And _domainName <> _domain And _departName <> _deptName Then
                                        If _uName = "" And _domainName = "" And _departName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _userName + "." + " " + " Department : " + _deptName + "." + " " + " Domain : " + _domain
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _uName + " " + " To " + " " + _userName + "." + " " + " Department : " + _departName + " " + " To " + " " + _deptName + "." + " " + " Domain : " + _domainName + " " + " To " + " " + _domain
                                        End If


                                    ElseIf _uName <> _userName And _domainName <> _domain Then
                                        If _uName = "" And _domainName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _userName + "." + " " + " Domain " + _domain
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _uName + " " + " To " + " " + _userName + "." + " " + " Domain " + _domainName + " " + " To " + " " + _domain
                                        End If


                                    ElseIf _uName <> _userName And _departName <> _deptName Then
                                        If _uName = "" And _departName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _userName + "." + " " + " Department : " + _deptName
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _uName + " " + " To " + " " + _userName + "." + " " + " Department : " + _departName + " " + " To " + " " + _deptName
                                        End If


                                    ElseIf _domainName <> _domain And _departName <> _deptName Then
                                        If _domainName = "" And _departName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Domain : " + _domain + "." + " " + " Department : " + _deptName
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Domain : " + _domainName + " " + " To " + " " + _domain + "." + " " + " Department : " + _departName + " " + " To " + " " + _deptName
                                        End If


                                    ElseIf _uName <> _userName Then
                                        If _uName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _userName
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _uName + " " + " To " + " " + _userName
                                        End If


                                    ElseIf _departName <> _deptName Then
                                        If _departName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Department : " + _deptName
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Department : " + _departName + " " + " To " + " " + _deptName
                                        End If


                                    ElseIf _domainName <> _domain Then
                                        If _domainName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Domain : " + _domain
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Domain : " + _domainName + " " + " To " + " " + _domain
                                        End If
                                    Else
                                        log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString()

                                    End If

                                    Logger.system_log(log_message)

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

                               


                            Else

                                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                                Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_Users_Auth")


                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@USERS_ID", DbType.String, dgView.Rows(i).Cells(4).Value)
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(2).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(10).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "Information Authorized Successfully !! "

                                    If _uName <> _userName And _domainName <> _domain And _departName <> _deptName Then
                                        If _uName = "" And _domainName = "" And _departName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _userName + "." + " " + " Department : " + _deptName + "." + " " + " Domain : " + _domain
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _uName + " " + " To " + " " + _userName + "." + " " + " Department : " + _departName + " " + " To " + " " + _deptName + "." + " " + " Domain : " + _domainName + " " + " To " + " " + _domain
                                        End If


                                    ElseIf _uName <> _userName And _domainName <> _domain Then
                                        If _uName = "" And _domainName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _userName + "." + " " + " Domain " + _domain
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _uName + " " + " To " + " " + _userName + "." + " " + " Domain " + _domainName + " " + " To " + " " + _domain
                                        End If


                                    ElseIf _uName <> _userName And _departName <> _deptName Then
                                        If _uName = "" And _departName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _userName + "." + " " + " Department : " + _deptName
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _uName + " " + " To " + " " + _userName + "." + " " + " Department : " + _departName + " " + " To " + " " + _deptName
                                        End If


                                    ElseIf _domainName <> _domain And _departName <> _deptName Then
                                        If _domainName = "" And _departName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Domain : " + _domain + "." + " " + " Department : " + _deptName
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Domain : " + _domainName + " " + " To " + " " + _domain + "." + " " + " Department : " + _departName + " " + " To " + " " + _deptName
                                        End If


                                    ElseIf _uName <> _userName Then
                                        If _uName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _userName
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " User Name : " + _uName + " " + " To " + " " + _userName
                                        End If


                                    ElseIf _departName <> _deptName Then
                                        If _departName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Department : " + _deptName
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Department : " + _departName + " " + " To " + " " + _deptName
                                        End If


                                    ElseIf _domainName <> _domain Then
                                        If _domainName = "" Then
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Domain : " + _domain
                                        Else
                                            log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " Domain : " + _domainName + " " + " To " + " " + _domain
                                        End If
                                    Else
                                        log_message = " Authorized : User Id : " + dgView.Rows(i).Cells(4).Value.ToString()

                                    End If

                                    Logger.system_log(log_message)

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
                        End If
                    End If

                Next i

                '--------------Mizan Work (25-04-16)---------------------


                '--------------Commented By Mizan (25-04-16)---------------

                'Dim i As Integer

                'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                'Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_Users_Auth")

                'For i = 0 To dgView.Rows.Count - 1

                '    If dgView.Rows(i).Cells(0).Value = True Then

                '        commProc.Parameters.Clear()

                '        db.AddInParameter(commProc, "@USERS_ID", DbType.String, dgView.Rows(i).Cells(4).Value)
                '        db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(2).Value)
                '        db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(10).Value)

                '        db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                '        Dim result As Integer

                '        db.ExecuteNonQuery(commProc)
                '        result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                '        If result = 0 Then
                '            
                '        ElseIf result = 1 Then

                '            MessageBox.Show("Update not possible", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                '        ElseIf result = 3 Then
                '            MessageBox.Show("Already authorized", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                '        ElseIf result = 4 Then
                '            MessageBox.Show("Record not found", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                '        ElseIf result = 5 Then
                '            MessageBox.Show("You cannot authorize the transaction", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        ElseIf result = 7 Then
                '            MessageBox.Show("Data mismatch! Reload records", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                '        Else
                '            MessageBox.Show("Auth Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        End If

                '    End If

                '    log_message = "Authorized User ID " + dgView.Rows(i).Cells(4).Value.ToString()
                '    Logger.system_log(log_message)

                'Next i

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
End Class