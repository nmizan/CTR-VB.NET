

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Public Class FrmPermFGDet

    Dim _formName As String = "SystemFGPermissionDetail"
    Dim opt As SecForm = New SecForm(_formName, CommonAppSet.IsAdmin)

#Region "Global Variables"
    Dim _formMode As FormTransMode
    Dim _intSlno As Integer = 0
    Dim _intModno As Integer = 0
    Dim _mod_datetime As Date
    Dim _status As String = ""
    Dim log_message As String = ""
    Dim MenuName As String = ""
    Dim FormName As String = ""

    Dim _funcName As String = ""
    Dim _funcDept As String = ""
    Dim _udeptName As String = ""

    Dim _functionName As String = ""
    Dim _functionDept As String = ""
    Dim _adepartName As String = ""

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
        txtId.ReadOnly = True
        txtName.ReadOnly = True

        cmbDept.Enabled = False


        dgViewForms.ReadOnly = True
        dgViewMenus.ReadOnly = True


    End Sub

    Private Sub EnableFields()


        txtId.ReadOnly = False
        txtName.ReadOnly = False

        If _intModno = 0 Then
            cmbDept.Enabled = True

        End If


        dgViewForms.ReadOnly = False
        dgViewMenus.ReadOnly = False

        dgViewForms.Columns(1).ReadOnly = True
        dgViewMenus.Columns(1).ReadOnly = True


    End Sub


    Private Sub ClearFields()
        txtId.Clear()
        txtName.Clear()

        dgViewForms.DataSource = Nothing
        dgViewMenus.DataSource = Nothing



    End Sub

    Private Sub ClearFieldsAll()
        txtId.Clear()
        txtName.Clear()


        _intSlno = 0
        _intModno = 0

        lblVerNo.Text = ""
        lblVerTot.Text = ""

        lblInputBy.Text = ""
        lblInputDate.Text = ""
        lblAuthBy.Text = ""
        lblAuthDate.Text = ""

        lblModNo.Text = ""

        dgViewForms.DataSource = Nothing
        dgViewMenus.DataSource = Nothing


    End Sub


    Private Function CheckValidData() As Boolean

        If txtId.Text.Trim() = "" Then
            MessageBox.Show("Id required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtId.Focus()
            Return False
        ElseIf txtName.Text.Trim() = "" Then
            MessageBox.Show("Name required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtName.Focus()
            Return False
        ElseIf cmbDept.Text.Trim() = "" Then
            MessageBox.Show("Department required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbDept.Focus()
            Return False
        End If


        Return True

    End Function



    Private Function SaveData() As TransState

        Dim tStatus As TransState

        Dim intSlno As Integer = 0

        Dim intModno As Integer = 0

        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)


        If _formMode = FormTransMode.Add Then

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()


                Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_FGroup_Add")

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@FG_ID", DbType.String, txtId.Text.Trim())
                db.AddInParameter(commProc, "@FG_NAME", DbType.String, txtName.Text.Trim())
                db.AddInParameter(commProc, "@DEPT_SLNO", DbType.Int32, cmbDept.SelectedValue)
                db.AddOutParameter(commProc, "@RET_SLNO", DbType.Int32, 5)

                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                Dim result As Integer


                db.ExecuteNonQuery(commProc, trans)
                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")

                If result <> 0 Then
                    tStatus = TransState.Exist
                    trans.Rollback()
                    Return tStatus

                   


                Else

                    intSlno = db.GetParameterValue(commProc, "@RET_SLNO")

                    intModno = 1

                    '----------- for FORMS Permission -------------------------------

                    Dim commProcForm As DbCommand = db.GetStoredProcCommand("CTR_PermForms_Add")

                    For i = 0 To dgViewForms.Rows.Count - 1

                        commProcForm.Parameters.Clear()

                        db.AddInParameter(commProcForm, "@FG_SLNO", DbType.Int64, intSlno)
                        db.AddInParameter(commProcForm, "@FORM_SLNO", DbType.Int64, dgViewForms.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcForm, "@IS_SHOW", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(2).Value))
                        db.AddInParameter(commProcForm, "@IS_NEW", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(4).Value))
                        db.AddInParameter(commProcForm, "@IS_UNLOCK", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(3).Value))
                        db.AddInParameter(commProcForm, "@IS_AUTHORIZER", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(7).Value))
                        db.AddInParameter(commProcForm, "@IS_DELETE", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(6).Value))
                        db.AddInParameter(commProcForm, "@IS_SAVE", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(5).Value))

                        db.AddParameter(commProcForm, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcForm, trans)

                        If db.GetParameterValue(commProcForm, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If

                        FormName += dgViewForms.Rows(i).Cells(1).Value.ToString() + " , "

                    Next

                   


                    '--------- for Menu Permission ---------------------
                    Dim commProcMenu As DbCommand = db.GetStoredProcCommand("CTR_PermMenus_Add")

                    For i = 0 To dgViewMenus.Rows.Count - 1

                        commProcMenu.Parameters.Clear()

                        db.AddInParameter(commProcMenu, "@FG_SLNO", DbType.Int64, intSlno)
                        db.AddInParameter(commProcMenu, "@MENU_SLNO", DbType.Int64, dgViewMenus.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcMenu, "@IS_VISIBLE", DbType.Boolean, NullHelper.ToBool(dgViewMenus.Rows(i).Cells(2).Value))
                        db.AddInParameter(commProcMenu, "@IS_ENABLE", DbType.Boolean, NullHelper.ToBool(dgViewMenus.Rows(i).Cells(3).Value))

                        db.AddParameter(commProcMenu, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcMenu, trans)

                        If db.GetParameterValue(commProcMenu, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If

                        MenuName += dgViewMenus.Rows(i).Cells(1).Value.ToString() + " , "

                    Next


                    tStatus = TransState.Add
                    _intSlno = intSlno
                    _intModno = 1

                    log_message = " Added : Functional Group : " + txtId.Text.Trim() + "." + " " + " Name : " + txtName.Text.Trim() + "." + " " + " Department : " + cmbDept.Text
                    Logger.system_log(log_message)

                    log_message = " Added : Functional Group : " + txtId.Text.Trim() + "." + " " + " Name : " + txtName.Text.Trim() + " " + " For Form Permission "
                    Logger.system_log(log_message)

                    log_message = " Added : Functional Group : " + txtId.Text.Trim() + "." + " " + " Name : " + txtName.Text.Trim() + " " + " For Menu Permission "
                    Logger.system_log(log_message)

                End If

                trans.Commit()

              


            End Using

        ElseIf _formMode = FormTransMode.Update Then


            Using conn As DbConnection = db.CreateConnection()


                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()


                Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_FGroup_Update")

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@SLNO", DbType.Int64, _intSlno)
                db.AddInParameter(commProc, "@FG_ID", DbType.String, txtId.Text.Trim())
                db.AddInParameter(commProc, "@FG_NAME", DbType.String, txtName.Text.Trim())
                db.AddInParameter(commProc, "@DEPT_SLNO", DbType.Int32, cmbDept.SelectedValue)
                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
                db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)

                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                Dim result As Integer

                db.ExecuteNonQuery(commProc, trans)
                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")

                '----------Mizan Work (12-04-2016)---------------

                If _funcName <> txtName.Text.Trim() And _funcDept <> cmbDept.Text Then
                    log_message = " Updated : Functional Group : " + txtId.Text.Trim() + "." + " " + " Name : " + _funcName + " " + " To " + " " + txtName.Text.Trim() + "." + " " + " Department : " + _funcDept + " " + " To " + " " + cmbDept.Text

                ElseIf _funcName <> txtName.Text.Trim() Then
                    log_message = " Updated : Functional Group :  " + txtId.Text.Trim() + "." + " " + " Name : " + _funcName + " " + " To " + " " + txtName.Text.Trim()

                ElseIf _funcDept <> cmbDept.Text Then
                    log_message = " Updated : Functional Group :  " + txtId.Text.Trim() + "." + " " + " Department : " + _funcDept + " " + " To " + " " + cmbDept.Text

                Else
                    log_message = " Updated : Functional Group :  " + txtId.Text.Trim()
                End If

                Logger.system_log(log_message)

                '----------Mizan Work (12-04-2016)---------------


                ''log_message = "Functional Group " + txtId.Text.Trim() + ", Name " + txtName.Text.Trim() + ", Department " + cmbDept.Text + " UPdated"


                If result = 1 Then
                    tStatus = TransState.UnspecifiedError
                    trans.Rollback()
                    Return tStatus
                ElseIf result = 4 Then
                    tStatus = TransState.NoRecord
                    trans.Rollback()
                    Return tStatus
                ElseIf result = 0 Then

                    intModno = db.GetParameterValue(commProc, "@RET_MOD_NO")

                    '----------- for FORMS Permission -------------------------------


                    Dim commProcForm As DbCommand = db.GetStoredProcCommand("CTR_PermForms_Update")

                    For i = 0 To dgViewForms.Rows.Count - 1

                        commProcForm.Parameters.Clear()

                        db.AddInParameter(commProcForm, "@FG_SLNO", DbType.Int64, _intSlno)
                        db.AddInParameter(commProcForm, "@FORM_SLNO", DbType.Int64, dgViewForms.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcForm, "@IS_SHOW", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(2).Value))
                        db.AddInParameter(commProcForm, "@IS_NEW", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(4).Value))
                        db.AddInParameter(commProcForm, "@IS_UNLOCK", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(3).Value))
                        db.AddInParameter(commProcForm, "@IS_AUTHORIZER", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(7).Value))
                        db.AddInParameter(commProcForm, "@IS_DELETE", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(6).Value))
                        db.AddInParameter(commProcForm, "@IS_SAVE", DbType.Boolean, NullHelper.ToBool(dgViewForms.Rows(i).Cells(5).Value))
                        db.AddInParameter(commProcForm, "@MOD_NO", DbType.Int32, intModno)

                        db.AddParameter(commProcForm, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcForm, trans)

                        If db.GetParameterValue(commProcForm, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If

                        FormName += dgViewForms.Rows(i).Cells(1).Value.ToString() + " , "

                    Next



                    log_message = " Updated : Functional Group " + txtId.Text.Trim() + "." + " " + " Name : " + txtName.Text.Trim() + " " + " For Form Permission "
                    Logger.system_log(log_message)

                    '--------- for Menu Permission ---------------------

                    Dim commProcMenu As DbCommand = db.GetStoredProcCommand("CTR_PermMenus_Update")

                    For i = 0 To dgViewMenus.Rows.Count - 1

                        commProcMenu.Parameters.Clear()

                        db.AddInParameter(commProcMenu, "@FG_SLNO", DbType.Int64, _intSlno)
                        db.AddInParameter(commProcMenu, "@MENU_SLNO", DbType.Int64, dgViewMenus.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcMenu, "@IS_VISIBLE", DbType.Boolean, NullHelper.ToBool(dgViewMenus.Rows(i).Cells(2).Value))
                        db.AddInParameter(commProcMenu, "@IS_ENABLE", DbType.Boolean, NullHelper.ToBool(dgViewMenus.Rows(i).Cells(3).Value))
                        db.AddInParameter(commProcMenu, "@MOD_NO", DbType.Int32, intModno)

                        db.AddParameter(commProcMenu, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcMenu, trans)

                        If db.GetParameterValue(commProcMenu, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If

                        MenuName += dgViewMenus.Rows(i).Cells(1).Value.ToString() + " , "
                    Next

                    tStatus = TransState.Update
                    _intModno = intModno

                    trans.Commit()

                    log_message = " Updated : Functional Group " + txtId.Text.Trim() + "." + " " + " Name : " + txtName.Text.Trim() + " " + " For Menu Permission "
                    Logger.system_log(log_message)

                    Return tStatus

                End If

                trans.Rollback()


            End Using


        End If


        Return tStatus

    End Function

    '----------Mizan Work (19-04-2016)---------------

    Private Function AuthorizeData() As TransState

        If _intModno > 1 Then

            LoadGroupDataForAuth(_intSlno)

            Dim tStatus As TransState

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_FGroup_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@SLNO", DbType.Int64, _intSlno)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                If _functionName <> _funcName And _functionDept <> _funcDept Then
                    If _functionName = "" And _functionDept = "" Then
                        log_message = " Authorized : Functional Group : " + txtId.Text.Trim() + "." + " " + " Name : " + _funcName + "." + " " + " Department : " + _funcDept
                    Else
                        log_message = " Authorized : Functional Group : " + txtId.Text.Trim() + "." + " " + " Name : " + _functionName + " " + " To " + " " + _funcName + "." + " " + " Department : " + _functionDept + " " + " To " + " " + _funcDept
                    End If


                ElseIf _functionName <> _funcName Then
                    If _functionName = "" Then
                        log_message = " Authorized : Functional Group :  " + txtId.Text.Trim() + "." + " " + " Name : " + _funcName
                    Else
                        log_message = " Authorized : Functional Group :  " + txtId.Text.Trim() + "." + " " + " Name : " + _functionName + " " + " To " + " " + _funcName
                    End If


                ElseIf _functionDept <> _funcDept Then
                    log_message = " Authorized : Functional Group :  " + txtId.Text.Trim() + "." + " " + " Department : " + _functionDept + " " + " To " + " " + _funcDept

                Else
                    log_message = " Authorized : Functional Group :  " + txtId.Text.Trim()
                End If

                Logger.system_log(log_message)


            ElseIf result = 1 Then

                tStatus = TransState.UpdateNotPossible

            ElseIf result = 3 Then
                tStatus = TransState.AlreadyAuthorized

            ElseIf result = 4 Then
                tStatus = TransState.NoRecord

            ElseIf result = 5 Then
                tStatus = TransState.MakerCheckerSame
            ElseIf result = 7 Then
                tStatus = TransState.ModifiedOutside

            Else
                tStatus = TransState.UpdateNotPossible
            End If

           

            Return tStatus

        Else

            Dim tStatus As TransState

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_FGroup_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@SLNO", DbType.Int64, _intSlno)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                If _functionName <> _funcName And _functionDept <> _funcDept Then
                    If _functionName = "" And _functionDept = "" Then
                        log_message = " Authorized : Functional Group : " + txtId.Text.Trim() + "." + " " + " Name : " + _funcName + "." + " " + " Department : " + _funcDept
                    Else
                        log_message = " Authorized : Functional Group : " + txtId.Text.Trim() + "." + " " + " Name : " + _functionName + " " + " To " + " " + _funcName + "." + " " + " Department : " + _functionDept + " " + " To " + " " + _funcDept
                    End If


                ElseIf _functionName <> _funcName Then
                    If _functionName = "" Then
                        log_message = " Authorized : Functional Group :  " + txtId.Text.Trim() + "." + " " + " Name : " + _funcName
                    Else
                        log_message = " Authorized : Functional Group :  " + txtId.Text.Trim() + "." + " " + " Name : " + _functionName + " " + " To " + " " + _funcName
                    End If


                ElseIf _functionDept <> _funcDept Then
                    log_message = " Authorized : Functional Group :  " + txtId.Text.Trim() + "." + " " + " Department : " + _functionDept + " " + " To " + " " + _funcDept

                Else
                    log_message = " Authorized : Functional Group :  " + txtId.Text.Trim()
                End If

                Logger.system_log(log_message)



            ElseIf result = 1 Then

                tStatus = TransState.UpdateNotPossible

            ElseIf result = 3 Then
                tStatus = TransState.AlreadyAuthorized

            ElseIf result = 4 Then
                tStatus = TransState.NoRecord

            ElseIf result = 5 Then
                tStatus = TransState.MakerCheckerSame
            ElseIf result = 7 Then
                tStatus = TransState.ModifiedOutside

            Else
                tStatus = TransState.UpdateNotPossible
            End If

           
            Return tStatus

        End If


        '--------------------Commented By Mizan(19-04-16)-------------------

        'Dim tStatus As TransState

        'tStatus = TransState.UnspecifiedError

        'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        'Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_FGroup_Auth")

        'commProc.Parameters.Clear()

        'db.AddInParameter(commProc, "@SLNO", DbType.Int64, _intSlno)
        'db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
        'db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

        'db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

        'Dim result As Integer

        'db.ExecuteNonQuery(commProc)
        'result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
        'If result = 0 Then

        '    tStatus = TransState.Update

        'ElseIf result = 1 Then

        '    tStatus = TransState.UpdateNotPossible

        'ElseIf result = 3 Then
        '    tStatus = TransState.AlreadyAuthorized

        'ElseIf result = 4 Then
        '    tStatus = TransState.NoRecord

        'ElseIf result = 5 Then
        '    tStatus = TransState.MakerCheckerSame
        'ElseIf result = 7 Then
        '    tStatus = TransState.ModifiedOutside

        'Else
        '    tStatus = TransState.UpdateNotPossible
        'End If

        'log_message = "Authorized Functional Group Persission SLNO " + _intSlno.ToString() + " Name " + txtName.Text()
        'Logger.system_log(log_message)


        'Return tStatus


    End Function

    '----------Mizan Work (19-04-2016)---------------

    Private Sub LoadGroupDataForAuth(ByVal intSlno As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From F_GROUP Where SLNO ='" & intSlno & "' and STATUS= 'L' ")

            If ds.Tables(0).Rows.Count > 0 Then

                _intSlno = intSlno

                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("FG_ID").ToString()
                txtName.Text = ds.Tables(0).Rows(0)("FG_NAME").ToString()
                _functionName = ds.Tables(0).Rows(0)("FG_NAME").ToString()
                cmbDept.SelectedValue = ds.Tables(0).Rows(0)("DEPT_SLNO")
                _functionDept = ds.Tables(0).Rows(0)("DEPT_SLNO").ToString()

                '--------------Mizan Work (26-04-2016---------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From DEPARTMENT Where SLNO ='" & _functionDept & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _adepartName = ds2.Tables(0).Rows(0)("DEPT_NAME").ToString()
                    _functionDept = _adepartName

                End If
                '--------------Mizan Work (26-04-2016---------


            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadGroupData(ByVal intSlno As Integer, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_FGroup_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@SLNO", DbType.Int64, intSlno)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intmod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then

                _intSlno = intSlno
                _intModno = intmod

                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("FG_ID").ToString()
                txtName.Text = ds.Tables(0).Rows(0)("FG_NAME").ToString()
                _funcName = ds.Tables(0).Rows(0)("FG_NAME").ToString()
                cmbDept.SelectedValue = ds.Tables(0).Rows(0)("DEPT_SLNO")
                _funcDept = ds.Tables(0).Rows(0)("DEPT_SLNO").ToString()

                '--------------Mizan Work (26-04-2016---------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From DEPARTMENT Where SLNO ='" & _funcDept & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _udeptName = ds2.Tables(0).Rows(0)("DEPT_NAME").ToString()
                    _funcDept = _udeptName

                End If
                '--------------Mizan Work (26-04-2016---------


                lblInputBy.Text = ds.Tables(0).Rows(0)("INPUT_BY").ToString()
                lblInputDate.Text = ds.Tables(0).Rows(0)("INPUT_DATETIME").ToString()

                _mod_datetime = ds.Tables(0).Rows(0)("INPUT_DATETIME")

                lblAuthBy.Text = ds.Tables(0).Rows(0)("AUTH_BY").ToString()
                lblAuthDate.Text = ds.Tables(0).Rows(0)("AUTH_DATETIME").ToString()

                chkAuthorized.Checked = ds.Tables(0).Rows(0)("IS_AUTH")

                If ds.Tables(0).Rows(0)("STATUS") = "L" Or ds.Tables(0).Rows(0)("STATUS") = "U" Or ds.Tables(0).Rows(0)("STATUS") = "O" Then
                    chkOpen.Checked = True
                Else
                    chkOpen.Checked = False
                End If

                _status = ds.Tables(0).Rows(0)("STATUS")

                lblModNo.Text = ds.Tables(0).Rows(0)("MOD_NO").ToString()
                lblVerNo.Text = ds.Tables(0).Rows(0)("MOD_NO").ToString()

                Dim commProc2 As DbCommand = db.GetStoredProcCommand("CTR_FGroup_GetMaxMod")

                commProc2.Parameters.Clear()

                db.AddInParameter(commProc2, "@SLNO", DbType.Int64, intSlno)

                lblVerTot.Text = db.ExecuteDataSet(commProc2).Tables(0).Rows(0)(0).ToString()


                If _status = "L" Or _status = "U" _
                    Or (_status = "D" And chkAuthorized.Checked = False) Then


                    If btnUnlock.Enabled = False Then
                        EnableFields()
                        EnableClear()
                        EnableDelete()
                        EnableNew()
                        EnableRefresh()
                        EnableSave()

                    End If
                Else
                    DisableAuth()
                    DisableClear()
                    DisableDelete()
                    DisableRefresh()
                    DisableSave()

                    DisableFields()
                End If

                
                If chkAuthorized.Checked = False And (Not lblInputBy.Text.Trim.ToUpper() = CommonAppSet.User.ToUpper()) Then
                    EnableAuth()
                Else
                    DisableAuth()
                End If


            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub LoadFormMenuData(ByVal intSlno As Integer, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProcForm As DbCommand = db.GetStoredProcCommand("CTR_PermForms_GetDetails")

            commProcForm.Parameters.Clear()

            db.AddInParameter(commProcForm, "@FG_SLNO", DbType.Int64, intSlno)
            db.AddInParameter(commProcForm, "@MOD_NO", DbType.Int32, intmod)

            ds = db.ExecuteDataSet(commProcForm)

            dgViewForms.AutoGenerateColumns = False
            dgViewForms.DataSource = ds
            dgViewForms.DataMember = ds.Tables(0).TableName



            Dim commProcMenu As DbCommand = db.GetStoredProcCommand("CTR_PermMenus_GetDetails")

            commProcMenu.Parameters.Clear()

            db.AddInParameter(commProcMenu, "@FG_SLNO", DbType.Int64, intSlno)
            db.AddInParameter(commProcMenu, "@MOD_NO", DbType.Int32, intmod)

            ds = New DataSet

            ds = db.ExecuteDataSet(commProcMenu)

            dgViewMenus.AutoGenerateColumns = False
            dgViewMenus.DataSource = ds
            dgViewMenus.DataMember = ds.Tables(0).TableName





        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Function DeleteData() As TransState

        Dim tStatus As TransState

        Dim intModno As Integer = 0

        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_FGroup_Remove")

        commProc.Parameters.Clear()

        db.AddInParameter(commProc, "@SLNO", DbType.Int64, _intSlno)
        db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
        db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)

        db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

        Dim result As Integer

        db.ExecuteNonQuery(commProc)
        result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
        If result = 0 Then

            tStatus = TransState.Delete
            _intModno = db.GetParameterValue(commProc, "@RET_MOD_NO")

        ElseIf result = 1 Then

            tStatus = TransState.UpdateNotPossible

        ElseIf result = 3 Then
            tStatus = TransState.UpdateNotPossible

        ElseIf result = 4 Then
            tStatus = TransState.NoRecord

        ElseIf result = 5 Then
            tStatus = TransState.UpdateNotPossible
        ElseIf result = 6 Then
            tStatus = TransState.AlreadyDeleted

        Else
            tStatus = TransState.UpdateNotPossible
        End If
        log_message = "Delete Functional Group Persission SLNO " + _intSlno.ToString() + " Name " + txtName.Text()
        Logger.system_log(log_message)

        Return tStatus

    End Function

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal intSlno As Integer, ByVal intmod As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'LoadGroupData(intSlno, intmod)
        'LoadFormMenuData(intSlno, intmod)

        _intSlno = intSlno

        _intModno = intmod
    End Sub

    
#End Region

    Private Sub FrmPermFGDet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
        CommonUtil.FillComboBox("CTR_Department_GetList", cmbDept)

        If _intModno > 0 Then
            LoadGroupData(_intSlno, _intModno)
            LoadFormMenuData(_intSlno, _intModno)
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

   


  

    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlock.Click
        EnableNew()



        If Not (_intSlno = 0) Then

            If _status = "L" Or _status = "U" _
                    Or (_status = "D" And chkAuthorized.Checked = False) Then
                EnableFields()


                EnableClear()
                EnableDelete()
                EnableNew()
                EnableRefresh()
                EnableSave()


            Else
                DisableAuth()
                DisableClear()
                DisableDelete()
                DisableRefresh()
                DisableSave()

                DisableFields()
            End If


            If chkAuthorized.Checked = False And (Not lblInputBy.Text.Trim.ToUpper() = CommonAppSet.User.ToUpper()) Then
                EnableAuth()
            Else
                DisableAuth()
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

        If txtId.Enabled = True Then
            txtId.Focus()
        End If

        LoadFormMenuData(_intSlno, _intModno)

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try
            If MessageBox.Show("Do you really want to Save?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                If CheckValidData() Then

                    tState = SaveData()

                    If tState = TransState.Add Then

                        LoadGroupData(_intSlno, _intModno)

                        lblToolStatus.Text = "!! Information Added Successfully !!"

                        _formMode = FormTransMode.Update


                        EnableUnlock()
                        DisableNew()
                        DisableSave()
                        DisableDelete()
                        DisableAuth()
                        DisableClear()
                        DisableRefresh()
                        DisableFields()



                    ElseIf tState = TransState.Update Then

                        LoadGroupData(_intSlno, _intModno)

                        lblToolStatus.Text = "!! Information Updated Successfully !!"

                        EnableUnlock()
                        DisableNew()
                        DisableSave()
                        DisableDelete()
                        DisableAuth()
                        DisableClear()
                        DisableRefresh()
                        DisableFields()




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

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadGroupData(_intSlno, _intModno)
        LoadFormMenuData(_intSlno, _intModno)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try


            If MessageBox.Show("Do you really want to delete?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                tState = DeleteData()

                If tState = TransState.Delete Then


                    _formMode = FormTransMode.Add

                    LoadGroupData(_intSlno, _intModno)
                    LoadFormMenuData(_intSlno, _intModno)

                    DisableAuth()

                    If _intSlno = 0 Then

                        DisableDelete()
                        DisableSave()
                        DisableRefresh()
                        DisableFields()



                    End If

                    lblToolStatus.Text = "!! Information Deleted Successfully !!"

                ElseIf tState = TransState.AlreadyDeleted Then
                    lblToolStatus.Text = "!! Failed. Data is already deleted !!"
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




        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnPrevVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevVer.Click
        If _intModno - 1 > 0 Then
            LoadGroupData(_intSlno, _intModno - 1)
            LoadFormMenuData(_intSlno, _intModno)

        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click
        Dim intSlno As Integer = _intSlno
        Dim intModno As Integer = _intModno

        If intModno > 0 Then
            LoadGroupData(_intSlno, _intModno + 1)

            If _intModno = 0 Then
                LoadGroupData(intSlno, intModno)
            End If

            LoadFormMenuData(_intSlno, _intModno)
        End If

    End Sub

    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try
            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then



                tState = AuthorizeData()

                If tState = TransState.Update Then
                    LoadGroupData(_intSlno, _intModno)
                    lblToolStatus.Text = "!! Authorized Successfully !!"
                    EnableUnlock()

                    DisableNew()
                    DisableSave()
                    DisableDelete()
                    DisableAuth()

                    DisableClear()
                    DisableRefresh()

                    DisableFields()
                ElseIf tState = TransState.AlreadyAuthorized Then
                    lblToolStatus.Text = "!! Authorized Data cannot be authorized again !!"
                ElseIf tState = TransState.MakerCheckerSame Then
                    lblToolStatus.Text = "!! You cannot authorize the transaction !!"
                ElseIf tState = TransState.UpdateNotPossible Then
                    lblToolStatus.Text = "!! Failed! Authorization Failed !!"
                ElseIf tState = TransState.ModifiedOutside Then
                    lblToolStatus.Text = "!! Failed! Data Mismatch. Reload, Check and Authorise again !!"
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

    

    

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


    
    'Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
    '    Dim objExp As New ExportUtil(dgViewForms)

    '    objExp.ExportXl()
    'End Sub
End Class