Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization


Public Class FrmTransAcOwnerMapping

#Region "Global Variables"

    Dim _formName As String = "MaintenanceAcNumberMapDetail"
    Dim opt As SecForm = New SecForm(_formName)

    Dim _formMode As FormTransMode
    'Dim _intSlno As Integer = 0
    Dim _strBank_Code As String = ""
    Dim _strBranch_Code As String = ""
    Dim _strAcNumber As String = ""
    Dim _intModno As Integer = 0
    Dim _intRowNum As Integer = 0
    Dim _intSelectedRow As Integer = 0
    Dim log_message As String
    Dim _AcWnName As String = ""
    Dim _OwnerCode As String = ""
    Dim _RowEditMode As Boolean = False
#End Region

#Region "User defined Codes"


    Private Sub EnableUnlock()
        If opt.IsUnlock = True Then
            btnUnlock.Enabled = True
        Else
            DisableUnlock()
        End If
    End Sub

    Private Sub DisableUnlock()
        btnUnlock.Enabled = False
    End Sub

    Private Sub EnableNew()
        If opt.IsNew = True Then
            btnNew.Enabled = True
        Else
            DisableNew()
        End If
    End Sub

    Private Sub DisableNew()
        btnNew.Enabled = False
    End Sub

    Private Sub EnableSave()
        If opt.IsSave = True Then
            btnSave.Enabled = True
        Else
            DisableSave()
        End If
    End Sub

    Private Sub DisableSave()
        btnSave.Enabled = False
    End Sub

    Private Sub EnableDelete()
        If opt.IsDelete = True Then
            btnDelete.Enabled = True
        Else
            DisableDelete()
        End If
    End Sub

    Private Sub DisableDelete()
        btnDelete.Enabled = False
    End Sub

    Private Sub EnableAuth()
        If opt.IsAuth = True Then
            btnAuthorize.Enabled = True
        Else
            DisableAuth()
        End If
    End Sub

    Private Sub DisableAuth()
        btnAuthorize.Enabled = False
    End Sub


    Private Sub EnableClear()
        btnClear.Enabled = True
    End Sub

    Private Sub DisableClear()
        btnClear.Enabled = False
    End Sub

    Private Sub EnableRefresh()
        btnRefresh.Enabled = True
    End Sub
    Private Sub DisableRefresh()
        btnRefresh.Enabled = False
    End Sub

    Private Sub DisableFields()
        'txtId.ReadOnly = True
        'txtName.ReadOnly = True
        txtAccNo.ReadOnly = True
        txtInsertedOn.ReadOnly = True
        txtModifiedOn.ReadOnly = True

        cmbOwner.Enabled = False
        cmbExecutive.Enabled = False
        cmbAccountPerson.Enabled = False
        cmbAuthority.Enabled = False

        btnAddOwner.Enabled = False
        btnRemove.Enabled = False
        chkPrimary.Enabled = False
        btnOwner.Enabled = False

       
    End Sub

    Private Sub EnableFields()
        'txtId.ReadOnly = False
        'txtName.ReadOnly = False

        'If _intModno = 0 Then
        '    cmbBank.Enabled = True
        '    cmbBranch.Enabled = True
        '    txtAccNo.ReadOnly = False
        'End If



        txtAccNo.ReadOnly = False
        txtInsertedOn.ReadOnly = False
        txtModifiedOn.ReadOnly = False

        cmbOwner.Enabled = True
        cmbExecutive.Enabled = True
        cmbAccountPerson.Enabled = True
        cmbAuthority.Enabled = True

        btnAddOwner.Enabled = True
        btnRemove.Enabled = True

        chkPrimary.Enabled = True
        btnOwner.Enabled = True

    End Sub

    Private Sub ClearFields()
        'txtId.Clear()
        'txtName.Clear()

        txtAccNo.Clear()
        txtInsertedOn.Clear()
        txtModifiedOn.Clear()


    End Sub

    Private Sub ClearFieldsAll()


        txtAccNo.Clear()
        lblAccTitle.Text = ""
        lblBankName.Text = ""
        lblBranchName.Text = ""

        dgView.Rows.Clear()
        _intRowNum = 0
        _intSelectedRow = 0

        txtInsertedOn.Clear()
        txtModifiedOn.Clear()
        cmbAccountPerson.SelectedValue = -1
        cmbOwner.SelectedValue = -1
        cmbExecutive.SelectedValue = -1
        cmbAuthority.SelectedIndex = -1

        _RowEditMode = False

        btnAddOwner.Text = "Add"

        _strBank_Code = ""
        _strBranch_Code = ""
        _strAcNumber = ""
        _intModno = 0

        lblVerNo.Text = ""
        lblVerTot.Text = ""

        lblInputBy.Text = ""
        lblInputDate.Text = ""
        lblAuthBy.Text = ""
        lblAuthDate.Text = ""

        lblModNo.Text = ""
        lblOwnerCode.Text = ""

    End Sub

    Private Function CheckValidData() As Boolean

        'If txtId.Text.Trim() = "" Then
        '    MessageBox.Show("Id required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    txtId.Focus()
        '    Return False
        'ElseIf txtName.Text.Trim() = "" Then
        '    MessageBox.Show("Name required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    txtName.Focus()
        '    Return False
        If txtAccNo.Text.Trim() = "" Then
            MessageBox.Show("Bank required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAccNo.Focus()
            Return False

        ElseIf txtInsertedOn.Text.Trim() = "" Then
            MessageBox.Show("Inserted ON required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtInsertedOn.Focus()
            Return False
       
        End If

        Return True

    End Function

    Private Function SaveData() As TransState

        Dim tStatus As TransState

        Dim strSql As String

        Dim intSlno As Integer = 0
        Dim intModno As Integer = 0

        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        If _formMode = FormTransMode.Add Then  ' OK


            Using conn As DbConnection = db.CreateConnection()


                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                Dim commProc As DbCommand

                For i = 0 To dgView.Rows.Count - 2

                    If dgView.Rows(0).Cells(0).Value Is Nothing Or dgView.Rows(0).Cells(0).Value Is DBNull.Value Then
                        Continue For
                    End If

                    If dgView.Rows(0).Cells(0).Value.ToString() = "" Then
                        Continue For
                    End If

                    strSql = "Insert Into FIU_TRANS_AC_OWNER(OWNER_CODE, BANK_CODE, BRANCH_CODE, ACNUMBER, EXE_DESIG_CODE, SIGN_AUTHORITY,  INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS, ROLE_TYPE,IS_PRIMARY,IS_PRIMARY)" & _
                     "Values(@P_OWNER_CODE, @P_BANK_CODE, @P_BRANCH_CODE, @P_ACNUMBER, @P_EXE_DESIG_CODE, @P_SIGN_AUTHORITY, @P_Inserted_On,@P_Modified_On,1,@P_Input_By,getdate(),0,'U',@P_ROLE_TYPE,@IS_PRIMARY)"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()

                    db.AddInParameter(commProc, "@P_OWNER_CODE", DbType.String, dgView.Rows(i).Cells(0).Value)
                    db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, dgView.Rows(i).Cells(2).Value)
                    db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dgView.Rows(i).Cells(3).Value)
                    db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dgView.Rows(i).Cells(1).Value)
                    db.AddInParameter(commProc, "@P_EXE_DESIG_CODE", DbType.String, dgView.Rows(i).Cells(4).Value)
                    db.AddInParameter(commProc, "@P_SIGN_AUTHORITY", DbType.String, dgView.Rows(i).Cells(5).Value)
                    db.AddInParameter(commProc, "@P_ROLE_TYPE", DbType.String, dgView.Rows(i).Cells(6).Value)

                    db.AddInParameter(commProc, "@IS_PRIMARY", DbType.Int32, dgView.Rows(i).Cells(7).Value)

                    If txtInsertedOn.Text <> "" Then
                        db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, DateTime.ParseExact(txtInsertedOn.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
                    Else
                        db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, DBNull.Value)
                    End If

                    If txtModifiedOn.Text <> "" Then
                        db.AddInParameter(commProc, "@P_Modified_On", DbType.DateTime, DateTime.ParseExact(txtModifiedOn.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
                    Else
                        db.AddInParameter(commProc, "@P_Modified_On", DbType.DateTime, DBNull.Value)
                    End If

                    db.AddInParameter(commProc, "@P_Input_By", DbType.String, CommonAppSet.User)


                    Dim result As Integer

                    result = db.ExecuteNonQuery(commProc, trans)

                    If result < 0 Then
                        tStatus = TransState.Exist

                        trans.Rollback()

                        Return tStatus

                    Else
                        tStatus = TransState.Add
                        '_intSlno = intSlno
                        _strBank_Code = dgView.Rows(0).Cells(2).Value
                        _strBranch_Code = dgView.Rows(0).Cells(3).Value
                        _strAcNumber = dgView.Rows(0).Cells(1).Value
                        _intModno = 1
                    End If


                Next


                

                trans.Commit()

                log_message = " Added : TransAccountOwner Mapping Account Number : " + txtAccNo.Text.ToString()
                Logger.system_log(log_message)

                conn.Close()


            End Using





        ElseIf _formMode = FormTransMode.Update Then


            Using conn As DbConnection = db.CreateConnection()


                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                db.ExecuteNonQuery(trans, CommandType.Text, "delete FIU_TRANS_AC_OWNER where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and IS_AUTHORIZED=0")
                Dim ds As New DataSet


                strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_TRANS_AC_OWNER where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "'"


                intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)


                Dim commProc As DbCommand
                For i = 0 To dgView.Rows.Count - 2

                    If dgView.Rows(i).Cells(0).Value Is Nothing Or dgView.Rows(0).Cells(0).Value Is DBNull.Value Then
                        Continue For
                    End If

                    If dgView.Rows(i).Cells(0).Value.ToString() = "" Then
                        Continue For
                    End If

                    strSql = "Insert Into FIU_TRANS_AC_OWNER(OWNER_CODE, BANK_CODE, BRANCH_CODE, ACNUMBER, EXE_DESIG_CODE, SIGN_AUTHORITY,  INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS,ROLE_TYPE,IS_PRIMARY)" & _
                     "Values(@P_OWNER_CODE, @P_BANK_CODE, @P_BRANCH_CODE, @P_ACNUMBER, @P_EXE_DESIG_CODE, @P_SIGN_AUTHORITY, " & _
                     "@P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'U',@P_ROLE_TYPE,@IS_PRIMARY)"


                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()

                    db.AddInParameter(commProc, "@P_OWNER_CODE", DbType.String, dgView.Rows(i).Cells(0).Value)
                    db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, dgView.Rows(i).Cells(2).Value)
                    db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dgView.Rows(i).Cells(3).Value)
                    db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dgView.Rows(i).Cells(1).Value)
                    db.AddInParameter(commProc, "@P_EXE_DESIG_CODE", DbType.String, dgView.Rows(i).Cells(4).Value)
                    db.AddInParameter(commProc, "@P_SIGN_AUTHORITY", DbType.String, dgView.Rows(i).Cells(5).Value)
                    db.AddInParameter(commProc, "@P_ROLE_TYPE", DbType.String, dgView.Rows(i).Cells(6).Value)
                    db.AddInParameter(commProc, "@IS_PRIMARY", DbType.Int32, dgView.Rows(i).Cells(7).Value)

                    If txtInsertedOn.Text <> "" Then
                        db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, DateTime.ParseExact(txtInsertedOn.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
                    Else
                        db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, DBNull.Value)
                    End If

                    If txtModifiedOn.Text <> "" Then
                        db.AddInParameter(commProc, "@P_Modified_On", DbType.DateTime, DateTime.ParseExact(txtModifiedOn.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
                    Else
                        db.AddInParameter(commProc, "@P_Modified_On", DbType.DateTime, DBNull.Value)
                    End If

                    db.AddInParameter(commProc, "@P_Input_By", DbType.String, CommonAppSet.User)


                    Dim result As Integer

                    result = db.ExecuteNonQuery(commProc, trans)

                    If result < 0 Then
                        tStatus = TransState.Exist

                        trans.Rollback()

                        Return tStatus

                    Else
                        tStatus = TransState.Update
                        '_intSlno = intSlno
                        _strBank_Code = dgView.Rows(0).Cells(2).Value
                        _strBranch_Code = dgView.Rows(0).Cells(3).Value
                        _strAcNumber = dgView.Rows(0).Cells(1).Value
                        _intModno = intModno
                    End If


                Next


                trans.Commit()

                log_message = " Updated : TransAccountOwner Mapping Account Code : " + txtAccNo.Text.ToString()
                Logger.system_log(log_message)

            End Using

        End If

        Return tStatus

    End Function

    Private Function AuthorizeData() As TransState

        Dim tStatus As TransState

        Dim strSql As String

        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Using conn As DbConnection = db.CreateConnection()

            conn.Open()

            Dim trans As DbTransaction = conn.BeginTransaction()

            strSql = "select IS_AUTHORIZED,STATUS from FIU_TRANS_AC_OWNER where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString()

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

            If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                    strSql = "update FIU_TRANS_AC_OWNER set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                    "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                    " where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString()

                ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                    strSql = "update FIU_TRANS_AC_OWNER set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                    "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                    " where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString()

                End If

                Dim result As Integer
                result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                If result <= 0 Then

                    tStatus = TransState.NoRecord

                ElseIf result > 0 Then

                    If _intModno > 1 Then

                        'if previous modification status is D(Deleted) then make it C(Closed)
                        strSql = "update FIU_TRANS_AC_OWNER set STATUS = 'C' " & _
                            " where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & (_intModno - 1).ToString() & _
                            " and STATUS ='D'"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                        'if previous modification status is L(Deleted) then make it O(Open)
                        strSql = "update FIU_TRANS_AC_OWNER set STATUS = 'O' " & _
                            " where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & (_intModno - 1).ToString() & _
                            " and STATUS ='L'"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                    End If
                    tStatus = TransState.Update
                End If
            Else
                tStatus = TransState.UpdateNotPossible
            End If

            trans.Commit()

            log_message = " Authorized : TransAccountOwner Mapping Account Code : " + txtAccNo.Text.ToString()
            Logger.system_log(log_message)

        End Using

        Return tStatus

    End Function

    Private Sub LoadAcOwnerData(ByVal strBankCd As String, ByVal strBranchCd As String, ByVal strAcNo As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_TRANS_AC_OWNER Where Bank_Code='" & strBankCd & "' and Branch_Code='" & strBranchCd & "' and AcNumber='" & strAcNo & "' and MODNO=" & intMod)

            If ds.Tables(0).Rows.Count > 0 Then

                _strBank_Code = ds.Tables(0).Rows(0)("Bank_Code").ToString()
                _strBranch_Code = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                _strAcNumber = ds.Tables(0).Rows(0)("AcNumber").ToString
                _intModno = intMod

                _formMode = FormTransMode.Update

                lblBankName.Text = ds.Tables(0).Rows(0)("Bank_Code").ToString()
                lblBranchName.Text = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                txtAccNo.Text = ds.Tables(0).Rows(0)("AcNumber").ToString


                dgView.Rows.Clear()
                _intRowNum = 0
                _intSelectedRow = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1

                    If dgView.Rows.Count = _intRowNum + 1 Then
                        dgView.Rows.Add()
                    End If
                    _OwnerCode = ds.Tables(0).Rows(i)("OWNER_CODE").ToString()
                    dgView.Rows(_intRowNum).Cells(0).Value = ds.Tables(0).Rows(i)("OWNER_CODE").ToString()
                    dgView.Rows(_intRowNum).Cells(1).Value = ds.Tables(0).Rows(i)("AcNumber").ToString()
                    dgView.Rows(_intRowNum).Cells(2).Value = ds.Tables(0).Rows(i)("Bank_Code").ToString()
                    dgView.Rows(_intRowNum).Cells(3).Value = ds.Tables(0).Rows(i)("Branch_Code").ToString()
                    dgView.Rows(_intRowNum).Cells(4).Value = ds.Tables(0).Rows(i)("EXE_DESIG_CODE").ToString()
                    dgView.Rows(_intRowNum).Cells(5).Value = ds.Tables(0).Rows(i)("SIGN_AUTHORITY").ToString()
                    dgView.Rows(_intRowNum).Cells(6).Value = ds.Tables(0).Rows(i)("ROLE_TYPE").ToString()
                    dgView.Rows(_intRowNum).Cells(7).Value = ds.Tables(0).Rows(i)("IS_PRIMARY")

                    _intRowNum = _intRowNum + 1

                Next

                txtInsertedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Inserted_On"))
                txtModifiedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Modified_On"))

                lblInputBy.Text = ds.Tables(0).Rows(0)("INPUT_BY").ToString()
                lblInputDate.Text = ds.Tables(0).Rows(0)("INPUT_DATETIME").ToString()
                lblAuthBy.Text = ds.Tables(0).Rows(0)("AUTH_BY").ToString()
                lblAuthDate.Text = ds.Tables(0).Rows(0)("AUTH_DATETIME").ToString()

                chkAuthorized.Checked = ds.Tables(0).Rows(0)("IS_AUTHORIZED")

                If ds.Tables(0).Rows(0)("STATUS") = "L" Or ds.Tables(0).Rows(0)("STATUS") = "U" Or ds.Tables(0).Rows(0)("STATUS") = "O" Then
                    chkOpen.Checked = True
                Else
                    chkOpen.Checked = False
                End If

                lblModNo.Text = ds.Tables(0).Rows(0)("ModNo").ToString()
                lblVerNo.Text = ds.Tables(0).Rows(0)("ModNo").ToString()
                lblVerTot.Text = db.ExecuteDataSet(CommandType.Text, "Select isnull(max(ModNo),0) From FIU_TRANS_AC_OWNER Where Bank_Code='" & strBankCd & "' and Branch_Code='" & strBranchCd & "' and AcNumber='" & strAcNo & "'").Tables(0).Rows(0)(0).ToString()

            Else
           
                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Function GetMaxMod(ByVal strBankCd As String, ByVal strBranchCd As String, ByVal strAcNo As String) As Integer


        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select isnull(max(MODNO),0) From FIU_TRANS_AC_OWNER Where Bank_Code='" & strBankCd & "' and Branch_Code='" & strBranchCd & "' and AcNumber='" & strAcNo & "'")

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)(0) > 0 Then
                    Return ds.Tables(0).Rows(0)(0)
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return 0

    End Function


    Private Sub LoadAccInfo(ByVal strAcNo As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_ACCOUNT_INFO Where AcNumber='" & strAcNo & "' and STATUS='L'")

            If ds.Tables(0).Rows.Count > 0 Then

                _strBank_Code = ds.Tables(0).Rows(0)("Bank_Code").ToString()
                _strBranch_Code = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                _strAcNumber = ds.Tables(0).Rows(0)("AcNumber").ToString
                '_intModno = intMod

                _formMode = FormTransMode.Update

                lblBankName.Text = ds.Tables(0).Rows(0)("Bank_Code").ToString()
                lblBranchName.Text = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                lblAccTitle.Text = ds.Tables(0).Rows(0)("AC_TITLE").ToString()
                txtAccNo.Text = ds.Tables(0).Rows(0)("AcNumber").ToString

            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Function DeleteData() As TransState

        Dim tStatus As TransState

        Dim intModno As Integer = 0

        tStatus = TransState.UnspecifiedError

        Dim strSql As String = ""

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Using conn As DbConnection = db.CreateConnection()

            conn.Open()

            Dim trans As DbTransaction = conn.BeginTransaction()


            strSql = "select IS_AUTHORIZED,STATUS from FIU_TRANS_AC_OWNER where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString()

            Dim ds As New DataSet
            ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0)(0) = False Then 'if not authorized

                    strSql = "delete FIU_TRANS_AC_OWNER where  Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString() & " and IS_AUTHORIZED=0"

                    db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    _intModno = _intModno - 1

                    tStatus = TransState.Delete


                ElseIf ds.Tables(0).Rows(0)(0) = True Then 'if authorized

                    If ds.Tables(0).Rows(0)("STATUS") = "L" Then 'if this is the last modified data

                        strSql = "delete FIU_TRANS_AC_OWNER where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and IS_AUTHORIZED=0"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                        strSql = "select * from FIU_TRANS_AC_OWNER where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString()

                        Dim dsKeeper As New DataSet
                        dsKeeper = db.ExecuteDataSet(trans, CommandType.Text, strSql)


                        strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_TRANS_AC_OWNER where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "'"


                        intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)



                        Dim commProc As DbCommand
                        For i = 0 To dsKeeper.Tables(0).Rows.Count - 1

                            strSql = "Insert Into FIU_TRANS_AC_OWNER(OWNER_CODE, BANK_CODE, BRANCH_CODE, ACNUMBER, EXE_DESIG_CODE, SIGN_AUTHORITY,  INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS, ROLE_TYPE,IS_PRIMARY)" & _
                             "Values(@P_OWNER_CODE, @P_BANK_CODE, @P_BRANCH_CODE, @P_ACNUMBER, @P_EXE_DESIG_CODE, @P_SIGN_AUTHORITY, " & _
                             "@P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'U',@P_ROLE_TYPE,@IS_PRIMARY)"


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            db.AddInParameter(commProc, "@P_OWNER_CODE", DbType.String, dsKeeper.Tables(0).Rows(i)("OWNER_CODE"))
                            db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, _strBank_Code)
                            db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, _strBranch_Code)
                            db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, _strAcNumber)
                            db.AddInParameter(commProc, "@P_EXE_DESIG_CODE", DbType.String, dsKeeper.Tables(0).Rows(i)("EXE_DESIG_CODE"))
                            db.AddInParameter(commProc, "@P_SIGN_AUTHORITY", DbType.String, dsKeeper.Tables(0).Rows(i)("SIGN_AUTHORITY"))
                            db.AddInParameter(commProc, "@P_ROLE_TYPE", DbType.String, dsKeeper.Tables(0).Rows(i)("ROLE_TYPE"))
                            db.AddInParameter(commProc, "@IS_PRIMARY", DbType.Int32, dsKeeper.Tables(0).Rows(i)("IS_PRIMARY"))
                            

                            db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, dsKeeper.Tables(0).Rows(i)("Inserted_On"))

                            db.AddInParameter(commProc, "@P_Modified_On", DbType.DateTime, dsKeeper.Tables(0).Rows(i)("Modified_On"))

                            db.AddInParameter(commProc, "@P_Input_By", DbType.String, CommonAppSet.UserId)


                            Dim result As Integer

                            result = db.ExecuteNonQuery(commProc, trans)

                            If result < 0 Then
                                tStatus = TransState.Exist

                                trans.Rollback()

                                Return tStatus

                            Else
                                tStatus = TransState.Delete
                                '_intSlno = intSlno
                                _strBank_Code = dgView.Rows(0).Cells(2).Value
                                _strBranch_Code = dgView.Rows(0).Cells(3).Value
                                _strAcNumber = dgView.Rows(0).Cells(1).Value
                                _intModno = intModno
                            End If

                        Next

                    Else

                        tStatus = TransState.UpdateNotPossible
                    End If

                End If

            Else
                tStatus = TransState.NoRecord
            End If

         

            trans.Commit()

            log_message = "Delete TransAccountOwner Mapping Account Code " + txtAccNo.Text.ToString() + " Owner Code " + _OwnerCode.ToString()
            Logger.system_log(log_message)

        End Using


        Return tStatus

    End Function

    Private Function CheckForDelete() As Boolean

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String = ""

        strSql = "select IS_AUTHORIZED,STATUS from FIU_TRANS_AC_OWNER where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString()

        Dim ds As New DataSet

        ds = db.ExecuteDataSet(CommandType.Text, strSql)

        If ds.Tables(0).Rows(0)("STATUS") = "O" Then
            MessageBox.Show("You can only delete last authorized and modified data", "!! STOP Delet !!", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return False

        ElseIf ds.Tables(0).Rows(0)("STATUS") = "C" Then
            MessageBox.Show("You can only delete last authorized and modified data", "!! STOP Delet !!", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return False

        End If

        Return True
    End Function

#End Region



    Private Sub FrmTransAcOwnerMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If


        CommonUtil.FillComboBox("select OWNER_CODE, RTRIM(LTRIM(OWNER_NAME)) + ' | ' + OWNER_CODE from FIU_OWNER_INFO where STATUS='L' order by OWNER_NAME", cmbOwner)
        CommonUtil.FillComboBox("select EXE_DESIG_CODE, EXE_DESIG_NAME  from dbo.FIU_EXECUTIVE_DESIG where STATUS='L'", cmbExecutive)
        CommonUtil.FillComboBox("GO_AccountPersonRoleType_GetList", cmbAccountPerson)




        If _intModno > 0 Then  ' Do not understand
            'LoadAppData(_intSlno, _intModno)

            LoadAcOwnerData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno)
        End If

        EnableUnlock()

        DisableNew()
        DisableSave()
        DisableDelete()
        DisableAuth()

        DisableClear()
        DisableRefresh()

        DisableFields()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlock.Click

        EnableNew()

        EnableClear()

        If _intModno > 0 Then

            EnableFields()

            EnableSave()

            EnableRefresh()

            EnableDelete()

            If chkAuthorized.Checked = False Then
                EnableAuth()
            End If
        Else

            DisableFields()
        End If

        DisableUnlock()

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        lblToolStatus.Text = ""
        _formMode = FormTransMode.Add

        EnableSave()

        ClearFieldsAll()
        EnableFields()

        DisableRefresh()
        DisableDelete()

        'If txtId.Enabled = True Then
        '    txtId.Focus()

        'End If

        If txtAccNo.ReadOnly = False Then
            txtAccNo.Focus()
        End If

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub



    Public Sub New(ByVal intModno As Integer, ByVal strAcNumber As String, ByVal strBankCode As String, ByVal strBranchCode As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()





        _strBank_Code = strBankCode
        _strBranch_Code = strBranchCode
        _strAcNumber = strAcNumber
        _intModno = intModno

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Not txtAccNo.Text.Trim() = "" Then
            LoadAccInfo(txtAccNo.Text.Trim())
        End If

        If _strAcNumber <> "" And _strBank_Code <> "" And _strBranch_Code <> "" Then
            Dim intMaxMod As Integer = GetMaxMod(_strBank_Code, _strBranch_Code, _strAcNumber)
            If intMaxMod > 0 Then
                LoadAcOwnerData(_strBank_Code, _strBranch_Code, _strAcNumber, intMaxMod)
            End If
        End If


    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub btnAddOwner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddOwner.Click
        If _strAcNumber = "" Or txtAccNo.Text.Trim() = "" Then
            MessageBox.Show("A/C required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAccNo.Focus()
            Exit Sub

        ElseIf _strAcNumber <> txtAccNo.Text.Trim() Then
            MessageBox.Show("A/C selection required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAccNo.Focus()
            Exit Sub
        ElseIf cmbOwner.Text.Trim() = "" Then
            MessageBox.Show("Owner required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbOwner.Focus()

            Exit Sub
        ElseIf cmbExecutive.Text.Trim() = "" Then
            MessageBox.Show("Designation required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbExecutive.Focus()

            Exit Sub
        ElseIf cmbAuthority.Text.Trim() = "" Then
            MessageBox.Show("Sign authority required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbAuthority.Focus()

            Exit Sub

        ElseIf cmbAccountPerson.Text.Trim() = "" Then
            MessageBox.Show("Account Person Role Type required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbAccountPerson.Focus()

            Exit Sub
        End If

        If _RowEditMode = False Then

            If dgView.Rows.Count = _intRowNum + 1 Then
                dgView.Rows.Add()

            End If


            dgView.Rows(_intRowNum).Cells(0).Value = cmbOwner.SelectedValue.ToString()
            dgView.Rows(_intRowNum).Cells(1).Value = _strAcNumber
            dgView.Rows(_intRowNum).Cells(2).Value = _strBank_Code
            dgView.Rows(_intRowNum).Cells(3).Value = _strBranch_Code
            dgView.Rows(_intRowNum).Cells(4).Value = cmbExecutive.SelectedValue.ToString()
            dgView.Rows(_intRowNum).Cells(5).Value = cmbAuthority.SelectedIndex
            dgView.Rows(_intRowNum).Cells(6).Value = cmbAccountPerson.SelectedValue.ToString()
            If chkPrimary.Checked = True Then
                dgView.Rows(_intRowNum).Cells(7).Value = 1
            Else
                dgView.Rows(_intRowNum).Cells(7).Value = 0
            End If



            _intRowNum = _intRowNum + 1
        End If

        If _RowEditMode = True Then

            Dim selRow As Integer = _intSelectedRow


            dgView.Item(0, selRow).Value = cmbOwner.SelectedValue.ToString()
            dgView.Item(1, selRow).Value = _strAcNumber
            dgView.Item(2, selRow).Value = _strBank_Code
            dgView.Item(3, selRow).Value = _strBranch_Code
            dgView.Item(4, selRow).Value = cmbExecutive.SelectedValue.ToString()
            dgView.Item(5, selRow).Value = cmbAuthority.SelectedIndex
            dgView.Item(6, selRow).Value = cmbAccountPerson.SelectedValue.ToString()

            If chkPrimary.Checked = True Then
                dgView.Item(7, selRow).Value = 1
            Else
                dgView.Item(7, selRow).Value = 0
            End If


            dgView.Rows(0).Selected = True
            dgView.Rows(0).Selected = False
            dgView.Rows(selRow).Selected = True

        End If



    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        'Try
        '    'MessageBox.Show(_intRowNum.ToString())
        '    If _intRowNum = 0 Then
        '        Exit Sub
        '    End If

        '    If Not (dgView.SelectedRows.Item(0).Cells(0).Value Is Nothing) Then

        '        For i = _intSelectedRow To _intRowNum - 1
        '            dgView.Rows(i).Cells(0).Value = dgView.Rows(i + 1).Cells(0).Value
        '            dgView.Rows(i).Cells(1).Value = dgView.Rows(i + 1).Cells(1).Value
        '            dgView.Rows(i).Cells(2).Value = dgView.Rows(i + 1).Cells(2).Value
        '            dgView.Rows(i).Cells(3).Value = dgView.Rows(i + 1).Cells(3).Value
        '            dgView.Rows(i).Cells(4).Value = dgView.Rows(i + 1).Cells(4).Value
        '            dgView.Rows(i).Cells(5).Value = dgView.Rows(i + 1).Cells(5).Value
        '            dgView.Rows(i).Cells(6).Value = dgView.Rows(i + 1).Cells(6).Value
        '            dgView.Rows(i).Cells(7).Value = dgView.Rows(i + 1).Cells(7).Value
        '        Next

        '        _intRowNum = _intRowNum - 1

        '        cmbOwner.SelectedIndex = -1
        '        cmbExecutive.SelectedIndex = -1
        '        cmbAuthority.SelectedIndex = -1
        '        cmbAccountPerson.SelectedIndex = -1
        '        chkPrimary.Checked = False

        '    End If

        'Catch ex As Exception

        'End Try

        If dgView.SelectedRows.Count = 0 Then Exit Sub

        For Each row As DataGridViewRow In dgView.SelectedRows
            dgView.Rows.Remove(row)
        Next

        cmbOwner.SelectedIndex = -1
        cmbExecutive.SelectedIndex = -1
        cmbAuthority.SelectedIndex = -1
        cmbAccountPerson.SelectedIndex = -1
        chkPrimary.Checked = False
        lblOwnerCode.Text = ""


    End Sub

    'Private Sub dgView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellClick
    '    ' _intSelectedRow = e.RowIndex
    '    If Not (dgView.SelectedRows.Item(0).Cells(0).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(0).Value Is DBNull.Value) Then


    '        _RowEditMode = True

    '        btnAddOwner.Text = "Update"



    '        _intSelectedRow = e.RowIndex

    '        cmbOwner.SelectedValue = dgView.Item(0, e.RowIndex).Value
    '        'txtAccNo.Text = dgView.Item(1, e.RowIndex).Value
    '        'lblBankName.Text = dgView.Item(2, e.RowIndex).Value
    '        'lblBranchName.Text = dgView.Item(3, e.RowIndex).Value
    '        cmbExecutive.SelectedValue = dgView.Item(4, e.RowIndex).Value
    '        cmbAuthority.SelectedIndex = dgView.Item(5, e.RowIndex).Value
    '        cmbAccountPerson.SelectedValue = dgView.Item(6, e.RowIndex).Value
    '        Dim strPrimary As String = NullHelper.NumToString(dgView.Item(7, e.RowIndex).Value)

    '        If strPrimary = 1 Then
    '            chkPrimary.Checked = True
    '        Else
    '            chkPrimary.Checked = False
    '        End If




    '    End If


    'End Sub

    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick

        If Not (dgView.SelectedRows.Item(0).Cells(0).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(0).Value Is DBNull.Value) Then


            _RowEditMode = True

            btnAddOwner.Text = "Update"



            _intSelectedRow = e.RowIndex

            cmbOwner.SelectedValue = dgView.Item(0, e.RowIndex).Value
            lblOwnerCode.Text = dgView.Item(0, e.RowIndex).Value
            'txtAccNo.Text = dgView.Item(1, e.RowIndex).Value
            'lblBankName.Text = dgView.Item(2, e.RowIndex).Value
            'lblBranchName.Text = dgView.Item(3, e.RowIndex).Value
            cmbExecutive.SelectedValue = dgView.Item(4, e.RowIndex).Value
            cmbAuthority.SelectedIndex = dgView.Item(5, e.RowIndex).Value
            cmbAccountPerson.SelectedValue = dgView.Item(6, e.RowIndex).Value
            Dim strPrimary As String = NullHelper.NumToString(dgView.Item(7, e.RowIndex).Value)

            If strPrimary = 1 Then
                chkPrimary.Checked = True
            Else
                chkPrimary.Checked = False
            End If

        End If

    End Sub



    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click

        Dim tState As TransState

        lblToolStatus.Text = ""

        If lblInputBy.Text.Trim() = CommonAppSet.User.Trim() Then
            MessageBox.Show("Maker can't verify data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub

        End If


        Try
            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then



                tState = AuthorizeData()

                If tState = TransState.Update Then
                    'LoadAppData(_intSlno, _intModno)
                    lblToolStatus.Text = "!! Authorized Successfully !!"
                    DisableAuth()

                ElseIf tState = TransState.UpdateNotPossible Then
                    lblToolStatus.Text = "!! Failed! Authorized info can't be authorized again !!"
                ElseIf tState = TransState.DBError Then
                    lblToolStatus.Text = "!! Database error occured. Please, Try Again !!"
                ElseIf tState = TransState.UnspecifiedError Then
                    lblToolStatus.Text = "!! Unpecified Error Occured !!"
                End If



            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try

            If CheckForDelete() = True Then

                If MessageBox.Show("Do you really want to delete?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    tState = DeleteData()

                    If tState = TransState.Delete Then


                        _formMode = FormTransMode.Add

                        LoadAcOwnerData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno)

                        DisableAuth()

                        If _intModno = 0 Then

                            DisableDelete()
                            DisableSave()
                            DisableRefresh()
                            DisableFields()



                        End If

                        lblToolStatus.Text = "!! Information Deleted Successfully !!"

                    ElseIf tState = TransState.UpdateNotPossible Then
                        lblToolStatus.Text = "!! Delete Not Possible !!"

                    ElseIf tState = TransState.Exist Then
                        lblToolStatus.Text = "!! New Delete status insertion failed !!"

                    ElseIf tState = TransState.NoRecord Then
                        lblToolStatus.Text = "!! Nothing to Delete !!"
                    Else
                        lblToolStatus.Text = "!! Unpecified Error Occured !!"
                    End If

                End If

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try
            If MessageBox.Show("Do you really want to Save?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                If CheckValidData() Then  ' Ok

                    tState = SaveData()

                    If tState = TransState.Add Then

                        'LoadAppData(_intSlno, _intModno) ' ---- Understanding

                        LoadAcOwnerData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno)

                        lblToolStatus.Text = "!! Information Added Successfully !!"

                        _formMode = FormTransMode.Update


                        EnableDelete()

                        EnableRefresh()

                    ElseIf tState = TransState.Update Then

                        'LoadAppData(_intSlno, _intModno)

                        LoadAcOwnerData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno)

                        lblToolStatus.Text = "!! Information Updated Successfully !!"



                    ElseIf tState = TransState.Exist Then
                        lblToolStatus.Text = "!! Already Exist !!"
                    ElseIf tState = TransState.NoRecord Then
                        lblToolStatus.Text = "!! Nothing to Update !!"
                    ElseIf tState = TransState.DBError Then
                        lblToolStatus.Text = "!! Database error occured. Please, Try Again !!"
                    ElseIf tState = TransState.UnspecifiedError Then
                        lblToolStatus.Text = "!! Unpecified Error Occured !!"
                    End If

                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadAcOwnerData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno)
    End Sub


    Private Sub btnPrevVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevVer.Click
        If _intModno - 1 > 0 Then
            LoadAcOwnerData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno - 1)

        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click
        Dim strBank_Code As String = _strBank_Code
        Dim strBranch_Code As String = _strBranch_Code
        Dim strAcNumber As String = _strAcNumber
        Dim intModno As Integer = _intModno


        LoadAcOwnerData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno + 1)

        If _intModno = 0 Then
            'LoadAppData(intSlno, intModno)
            LoadAcOwnerData(strBank_Code, strBranch_Code, strAcNumber, intModno)
        End If
    End Sub

    Private Sub cmbOwner_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOwner.Validated
        If cmbOwner.SelectedIndex = -1 Then
            cmbOwner.Text = ""
        End If
    End Sub

    

    Private Sub btnOwner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOwner.Click
        Dim strOwnerCode As String = lblOwnerCode.Text

        If strOwnerCode = "" Then
            MessageBox.Show("Please Select Owner Number!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub

        End If



        Dim frmOwnerInfoDet As New frmOwnerInfo(strOwnerCode)
        frmOwnerInfoDet.ShowDialog()

    End Sub
End Class

