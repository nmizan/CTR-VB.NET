
'
'Author             : Fahad Khan
'Purpose            : Maintain Account Information
'Creation date      : 30-oct-2013
'Stored Procedure(s):  


Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql



Public Class FrmAccountInfoGoAmlSumm

#Region "user defined codes"

    Dim _formName As String = "MaintenanceAccountInfoGoAmlSumm"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""

    'For Update

    Dim _cmbCurrType As String = ""
    Dim _cmbAccType As String = ""
    Dim _cmbStatusType As String = ""
    Dim _cmbCurrTypeName As String = ""
    Dim _cmbAccTypeName As String = ""
    Dim _cmbStatusTypeName As String = ""
    Dim _Iban As String = ""
    Dim _beneficiary As String = ""
    Dim _beneficiaryCommnts As String = ""
    Dim _comments As String = ""
    Dim _clientNo As String = ""

    'For Auth
    Dim _cmboCurrType As String = ""
    Dim _cmboAccType As String = ""
    Dim _cmboStatusType As String = ""
    Dim _cmboCurrTypeName As String = ""
    Dim _cmboAccTypeName As String = ""
    Dim _cmboStatusTypeName As String = ""
    Dim _IbanAcc As String = ""
    Dim _beneficiaryAcc As String = ""
    Dim _beneficiaryCommntsAcc As String = ""
    Dim _commentsAcc As String = ""
    Dim _clientNoAcc As String = ""

    Dim GoAccList As New List(Of String)
    Dim _goAccLog As String = ""
    Dim _goAccInfolog As String = ""

    Private Sub LoadDataGrid()


        If dgView.Columns.Count = 0 Then Exit Sub

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_GetDetailList")

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
                dgView.Item(1, i).Value = dt.Rows(i).Item("MOD_NO").ToString()
                dgView.Item(2, i).Value = dt.Rows(i).Item("S").ToString()
                dgView.Item(3, i).Value = dt.Rows(i).Item("ACNUMBER").ToString()
                dgView.Item(4, i).Value = dt.Rows(i).Item("CURRENCY_CODE").ToString()
                dgView.Item(5, i).Value = dt.Rows(i).Item("STATUS_CODE").ToString()
                dgView.Item(6, i).Value = dt.Rows(i).Item("CLIENT_NUMBER").ToString()
                dgView.Item(7, i).Value = NullHelper.DateToString(dt.Rows(i).Item("OPENED"))
                dgView.Item(8, i).Value = NullHelper.DateToString(dt.Rows(i).Item("CLOSED"))
                dgView.Item(9, i).Value = dt.Rows(i).Item("INPUT_BY").ToString()
                dgView.Item(10, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INPUT_DATETIME"))
                dgView.Item(11, i).Value = dt.Rows(i).Item("INPUT_DATETIME")
                dgView.Item(12, i).Value = dt.Rows(i).Item("AUTH_BY").ToString()
                dgView.Item(13, i).Value = NullHelper.DateToString(dt.Rows(i).Item("AUTH_DATETIME"))
                dgView.Item(14, i).Value = dt.Rows(i).Item("ENTITY_ID").ToString()
            Next

            lblTotRecNo.Text = dt.Rows.Count

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

#End Region

    Private Sub FrmAccountInfoGoAmlSumm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If



        Dim i As Integer
        For i = 1 To dgView.Columns.Count - 1
            dgView.Columns(i).ReadOnly = True
        Next
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim frmAccountInfo As New FrmAccountInfoGoAMLDet()
        frmAccountInfo.ShowDialog()
    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try


            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then

                Dim frmAccountInfo As New FrmAccountInfoGoAMLDet(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
                frmAccountInfo.Show()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()
    End Sub

    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick
        If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then

            Dim frmAccountInfo As New FrmAccountInfoGoAMLDet(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
            frmAccountInfo.Show()
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
            btnAuthorize.Enabled = True
            chkAll.Visible = True

            If dgView.Columns.Count > 0 Then
                dgView.Columns(0).Visible = True
            End If
            chkShowAll.Visible = False

        End If

        LoadDataGrid()

    End Sub

    '----------------------Mizan Work (23-04-16)---------------------------

    Private Sub LoadMainDataForAuth(ByVal strAccNo As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From GO_ACCOUNT_INFO Where ACNUMBER='" & strAccNo & "' and STATUS ='L' ")

            If ds.Tables(0).Rows.Count > 0 Then

                _IbanAcc = ds.Tables(0).Rows(0)("IBAN").ToString()

                _clientNoAcc = ds.Tables(0).Rows(0)("CLIENT_NUMBER").ToString()

                _cmboCurrType = ds.Tables(0).Rows(0)("CURRENCY_CODE").ToString()

                _cmboAccType = ds.Tables(0).Rows(0)("ACCOUNT_TYPE").ToString()

                _cmboStatusType = ds.Tables(0).Rows(0)("STATUS_CODE").ToString()

                _beneficiaryAcc = ds.Tables(0).Rows(0)("BENEFICIARY").ToString()

                _beneficiaryCommntsAcc = ds.Tables(0).Rows(0)("BENEFICIARY_COMMENTS").ToString()

                _commentsAcc = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                ''------------------Mizan Work (26-04-16) ---------------------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_CURRENCY_TYPE Where CURRENCY_CODE = '" & _cmboCurrType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _cmboCurrTypeName = ds2.Tables(0).Rows(0)("CURRENCY_NAME").ToString()
                    _cmboCurrType = _cmboCurrTypeName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_ACCOUNT_TYPE Where ACTYPECODE = '" & _cmboAccType & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _cmboAccTypeName = ds3.Tables(0).Rows(0)("ACTYPENAME").ToString()
                    _cmboAccType = _cmboAccTypeName

                End If

                Dim ds4 As New DataSet
                ds4 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_ACSTATUSTYPE Where ACTYPE_CODE ='" & _cmboStatusType & "' ")
                If ds4.Tables(0).Rows.Count > 0 Then

                    _cmboStatusTypeName = ds4.Tables(0).Rows(0)("ACTYPE_NAME").ToString()
                    _cmboStatusType = _cmboStatusTypeName

                End If

                ''------------------Mizan Work (26-04-16) ----------------------

               
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '----------------------Mizan Work (23-04-16)---------------------------

    Private Sub LoadMainData(ByVal strAccNo As String, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfoMain_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ACNUMBER", DbType.String, strAccNo)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intmod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then

              
                _Iban = ds.Tables(0).Rows(0)("IBAN").ToString()

                _clientNo = ds.Tables(0).Rows(0)("CLIENT_NUMBER").ToString()

                _cmbCurrType = ds.Tables(0).Rows(0)("CURRENCY_CODE").ToString()

                _cmbAccType = ds.Tables(0).Rows(0)("ACCOUNT_TYPE").ToString()

                _cmbStatusType = ds.Tables(0).Rows(0)("STATUS_CODE").ToString()
                _beneficiary = ds.Tables(0).Rows(0)("BENEFICIARY").ToString()
                _beneficiaryCommnts = ds.Tables(0).Rows(0)("BENEFICIARY_COMMENTS").ToString()

                _comments = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                ''------------------Mizan Work (26-04-16) ---------------------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_CURRENCY_TYPE Where CURRENCY_CODE = '" & _cmbCurrType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _cmbCurrTypeName = ds2.Tables(0).Rows(0)("CURRENCY_NAME").ToString()
                    _cmbCurrType = _cmbCurrTypeName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_ACCOUNT_TYPE Where ACTYPECODE = '" & _cmbAccType & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _cmbAccTypeName = ds3.Tables(0).Rows(0)("ACTYPENAME").ToString()
                    _cmbAccType = _cmbAccTypeName

                End If

                Dim ds4 As New DataSet
                ds4 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_ACSTATUSTYPE Where ACTYPE_CODE ='" & _cmbStatusType & "' ")
                If ds4.Tables(0).Rows.Count > 0 Then

                    _cmbStatusTypeName = ds4.Tables(0).Rows(0)("ACTYPE_NAME").ToString()
                    _cmbStatusType = _cmbStatusTypeName

                End If

                ''------------------Mizan Work (26-04-16) ----------------------

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Try

            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                '----------------------Mizan Work (23-04-16)---------------------------

                Dim i As Integer


                For i = 0 To dgView.Rows.Count - 1


                    If dgView.Rows(i).Cells(0).Value = True Then

                        If dgView.Rows(i).Cells(9).Value.ToString = CommonAppSet.User.Trim() Then

                            MessageBox.Show("Maker can't verify data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else

                            LoadMainData(dgView.Rows(i).Cells(3).Value.ToString(), dgView.Rows(i).Cells(1).Value)


                            If (dgView.Rows(i).Cells(1).Value) > 1 Then

                                LoadMainDataForAuth(dgView.Rows(i).Cells(3).Value.ToString())

                                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_Auth")


                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@ACNUMBER", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "!! Information Authorized Successfully !!"

                                    If _IbanAcc <> _Iban Then
                                        If _IbanAcc = "" Then
                                            log_message = " Iban : " + _Iban + "." + " "

                                        Else
                                            log_message = " Iban : " + _IbanAcc + " " + " To " + " " + _Iban + "." + " "

                                        End If
                                        GoAccList.Add(log_message)
                                    End If

                                    If _cmboCurrType <> _cmbCurrType Then
                                        log_message = " Currency : " + _cmboCurrType + " " + " To " + " " + _cmbCurrType + "." + " "
                                        GoAccList.Add(log_message)
                                    End If

                                    If _cmboAccType <> _cmbAccType Then
                                        log_message = " Account Type : " + _cmboAccType + " " + " To " + " " + _cmbAccType + "." + " "
                                        GoAccList.Add(log_message)
                                    End If

                                    If _cmboStatusType <> _cmbStatusType Then
                                        log_message = " Status : " + _cmboStatusType + " " + " To " + " " + _cmbStatusType + "." + " "
                                        GoAccList.Add(log_message)
                                    End If
                                    If _beneficiaryAcc <> _beneficiary Then
                                        If _beneficiaryAcc = "" Then
                                            log_message = " Beneficiary : " + _beneficiary + "." + " "

                                        Else
                                            log_message = " Beneficiary : " + _beneficiaryAcc + " " + " To " + " " + _beneficiary + "." + " "

                                        End If
                                        GoAccList.Add(log_message)
                                    End If
                                    If _beneficiaryCommntsAcc <> _beneficiaryCommnts Then
                                        If _beneficiaryCommntsAcc = "" Then
                                            log_message = " Beneficiary Comments : " + _beneficiaryCommnts + "." + " "

                                        Else
                                            log_message = " Beneficiary Comments : " + _beneficiaryCommntsAcc + " " + " To " + " " + _beneficiaryCommnts + "." + " "

                                        End If
                                        GoAccList.Add(log_message)
                                    End If
                                    If _commentsAcc <> _comments Then
                                        If _commentsAcc = "" Then
                                            log_message = " Comments : " + _comments + "." + " "

                                        Else
                                            log_message = " Comments : " + _commentsAcc + " " + " To " + " " + _comments + "." + " "

                                        End If
                                        GoAccList.Add(log_message)
                                    End If
                                    If _clientNoAcc <> _clientNo Then
                                        If _clientNoAcc = "" Then
                                            log_message = " Client Number : " + _clientNo + "." + " "

                                        Else
                                            log_message = " Client Number : " + _clientNoAcc + " " + " To " + " " + _clientNo + "." + " "

                                        End If
                                        GoAccList.Add(log_message)
                                    End If

                                    For Each Accloglist As String In GoAccList
                                        _goAccLog += Accloglist
                                    Next

                                    _goAccInfolog = " Authorized : Account Number  : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + _goAccLog

                                    Logger.system_log(_goAccInfolog)
                                    _goAccLog = ""
                                    GoAccList.Clear()

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

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_Auth")


                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@ACNUMBER", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "!! Information Authorized Successfully !!"

                                    If _IbanAcc <> _Iban Then
                                        If _IbanAcc = "" Then
                                            'log_message = " Iban : " + _Iban + "." + " "

                                        Else
                                            log_message = " Iban : " + _IbanAcc + " " + " To " + " " + _Iban + "." + " "
                                            GoAccList.Add(log_message)
                                        End If

                                    End If

                                    If _cmboCurrType <> _cmbCurrType Then
                                        If _cmboCurrType = "" Then
                                            ' log_message = " Currency : " + _cmbCurrType + "." + " "
                                        Else
                                            log_message = " Currency : " + _cmboCurrType + " " + " To " + " " + _cmbCurrType + "." + " "
                                            GoAccList.Add(log_message)
                                        End If


                                    End If

                                    If _cmboAccType <> _cmbAccType Then
                                        If _cmboAccType = "" Then
                                            log_message = " Account Type : " + _cmbAccType + "." + " "
                                        Else
                                            log_message = " Account Type : " + _cmboAccType + " " + " To " + " " + _cmbAccType + "." + " "
                                        End If

                                        GoAccList.Add(log_message)
                                    End If

                                    If _cmboStatusType <> _cmbStatusType Then
                                        If _cmboStatusType = "" Then
                                            log_message = " Status : " + _cmbStatusType + "." + " "
                                        Else
                                            log_message = " Status : " + _cmboStatusType + " " + " To " + " " + _cmbStatusType + "." + " "
                                        End If

                                        GoAccList.Add(log_message)
                                    End If
                                    If _beneficiaryAcc <> _beneficiary Then
                                        If _beneficiaryAcc = "" Then
                                            '  log_message = " Beneficiary : " + _beneficiary + "." + " "

                                        Else
                                            log_message = " Beneficiary : " + _beneficiaryAcc + " " + " To " + " " + _beneficiary + "." + " "
                                            GoAccList.Add(log_message)
                                        End If

                                    End If
                                    If _beneficiaryCommntsAcc <> _beneficiaryCommnts Then
                                        If _beneficiaryCommntsAcc = "" Then
                                            ' log_message = " Beneficiary Comments : " + _beneficiaryCommnts + "." + " "

                                        Else
                                            log_message = " Beneficiary Comments : " + _beneficiaryCommntsAcc + " " + " To " + " " + _beneficiaryCommnts + "." + " "
                                            GoAccList.Add(log_message)
                                        End If

                                    End If
                                    If _commentsAcc <> _comments Then
                                        If _commentsAcc = "" Then
                                            '  log_message = " Comments : " + _comments + "." + " "

                                        Else
                                            log_message = " Comments : " + _commentsAcc + " " + " To " + " " + _comments + "." + " "
                                            GoAccList.Add(log_message)
                                        End If

                                    End If
                                    If _clientNoAcc <> _clientNo Then
                                        If _clientNoAcc = "" Then
                                            ' log_message = " Client Number : " + _clientNo + "." + " "

                                        Else
                                            log_message = " Client Number : " + _clientNoAcc + " " + " To " + " " + _clientNo + "." + " "
                                            GoAccList.Add(log_message)
                                        End If

                                    End If

                                    For Each Accloglist As String In GoAccList
                                        _goAccLog += Accloglist
                                    Next

                                    _goAccInfolog = " Authorized : Account Number  : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _goAccLog

                                    Logger.system_log(_goAccInfolog)
                                    _goAccLog = ""
                                    GoAccList.Clear()

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


                '----------------------Mizan Work (23-04-16)---------------------------

                ''----------------------Commented By Mizan (23-04-16) ----------------------------------

                'Dim i As Integer

                'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                'Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_Auth")

                'For i = 0 To dgView.Rows.Count - 1

                '    If dgView.Rows(i).Cells(0).Value = True Then

                '        commProc.Parameters.Clear()

                '        db.AddInParameter(commProc, "@ACNUMBER", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                '        db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                '        db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                '        db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                '        Dim result As Integer

                '        db.ExecuteNonQuery(commProc)
                '        result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                '        If result = 0 Then
                '            lblToolStatus.Text = "!! Information Authorized Successfully !!"
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
                '    log_message = "Account Number " + dgView.Rows(i).Cells(3).Value.ToString() + " Authorized"
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class