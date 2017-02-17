Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization

Public Class FrmTransType



#Region "Global Variables"

    Dim _formName As String = "MaintenanceTransacionTypeDet"
    Dim opt As SecForm = New SecForm(_formName)

    Dim _formMode As FormTransMode
    'Dim _intSlno As Integer = 0
    Dim _strTransTypeCode As String = ""
    Dim _intModno As Integer = 0


    Dim log_message As String = ""
    Dim _TTypeName As String = ""

    Dim _TransType As String = ""




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


        txtInsertedOn.ReadOnly = True
        txtModifiedOn.ReadOnly = True
    End Sub

    Private Sub EnableFields()



        If _intModno = 0 Then

            txtId.ReadOnly = False
        End If

        txtName.ReadOnly = False


        txtInsertedOn.ReadOnly = False
        txtModifiedOn.ReadOnly = False


    End Sub

    Private Sub ClearFields()
        txtId.Clear()
        txtName.Clear()
        txtInsertedOn.Clear()
        txtModifiedOn.Clear()




    End Sub

    Private Sub ClearFieldsAll()


        txtId.Clear()
        txtName.Clear()
        txtInsertedOn.Clear()
        txtModifiedOn.Clear()



        _strTransTypeCode = ""
        _intModno = 0

        lblVerNo.Text = ""
        lblVerTot.Text = ""

        lblInputBy.Text = ""
        lblInputDate.Text = ""
        lblAuthBy.Text = ""
        lblAuthDate.Text = ""

        lblModNo.Text = ""

    End Sub

    Private Function CheckValidData() As Boolean

        If txtId.Text.Trim() = "" Then
            MessageBox.Show("Code required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtId.Focus()
            Return False
        ElseIf txtName.Text.Trim() = "" Then
            MessageBox.Show("Name required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtName.Focus()
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



            strSql = "Insert Into FIU_TRANSACTION_TYPES(TRTYPECODE, TRDEFINITION, INSERTED_ON, MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED, STATUS) " & _
                     " Values(@P_TRTYPECODE, @P_TRDEFINITION, @P_INSERTED_ON, @P_MODIFIED_ON, 1, @P_INPUT_BY, getdate(), 0, 'U')"


            Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

            commProc.Parameters.Clear()
            db.AddInParameter(commProc, "@P_TRTYPECODE", DbType.String, txtId.Text.Trim())
            db.AddInParameter(commProc, "@P_TRDEFINITION", DbType.String, txtName.Text.Trim())


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

            result = db.ExecuteNonQuery(commProc)

            If result < 0 Then
                tStatus = TransState.Exist
            Else
                tStatus = TransState.Add

                _strTransTypeCode = txtId.Text.Trim()

                _intModno = 1

                log_message = " Added : Transaction Type Code : " + txtId.Text.Trim() + "." + " " + " Transaction Type Name : " + txtName.Text.Trim()
                Logger.system_log(log_message)


            End If

           



        ElseIf _formMode = FormTransMode.Update Then


            Using conn As DbConnection = db.CreateConnection()


                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                db.ExecuteNonQuery(trans, CommandType.Text, "delete FIU_TRANSACTION_TYPES where TRTYPECODE='" & _strTransTypeCode & "' and IS_AUTHORIZED=0")
                Dim ds As New DataSet


                strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_TRANSACTION_TYPES where TRTYPECODE='" & _strTransTypeCode & "'"


                intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)

                strSql = "Insert Into FIU_TRANSACTION_TYPES(TRTYPECODE, TRDEFINITION,  INSERTED_ON, MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED, STATUS) " & _
                     " Values(@P_TRTYPECODE, @P_TRDEFINITION," & _
                     " @P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'U')"


                Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_TRTYPECODE", DbType.String, txtId.Text.Trim())
                db.AddInParameter(commProc, "@P_TRDEFINITION", DbType.String, txtName.Text.Trim())

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

                Else
                    tStatus = TransState.Update
                    _intModno = intModno

                    '----------Mizan Work (10-04-16)------------

                    If _TTypeName <> txtName.Text.Trim() Then

                        log_message = " Updated : Transaction Type Code : " + txtId.Text.Trim() + "." + " " + " Transaction Type Name : " + _TTypeName + " " + " To " + " " + txtName.Text.ToString()
                        Logger.system_log(log_message)
                    Else
                        log_message = " Updated : Transaction Type Code : " + txtId.Text.Trim() + "." + " " + " Transaction Type Name : " + txtName.Text.ToString()
                        Logger.system_log(log_message)
                    End If
                    '----------Mizan Work (10-04-16)------------

                End If


                trans.Commit()

               
            End Using

        End If

        Return tStatus

    End Function

    Private Function AuthorizeData() As TransState

        '-----Mizan Work (13-04-2016)----------

        If _intModno > 1 Then

            LoadTransTypeDataForAuth(_strTransTypeCode)

            Dim tStatus As TransState

            Dim strSql As String

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                strSql = "select IS_AUTHORIZED,STATUS from FIU_TRANSACTION_TYPES where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_TRANSACTION_TYPES set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_TRANSACTION_TYPES set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If _intModno > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_TRANSACTION_TYPES set STATUS = 'C' " & _
                                " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_TRANSACTION_TYPES set STATUS = 'O' " & _
                                " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

                        If _TransType <> _TTypeName Then
                            If _TransType = "" Then
                                log_message = " Authorized : Transaction Type Code : " + txtId.Text.Trim() + "." + " " + " Transaction Type Name : " + _TTypeName
                            Else
                                log_message = " Authorized : Transaction Type Code : " + txtId.Text.Trim() + "." + " " + " Transaction Type Name : " + _TransType + " " + " To " + " " + _TTypeName
                            End If

                            Logger.system_log(log_message)
                        Else

                            log_message = "Authorized : Transaction Type Code : " + txtId.Text.Trim() + "." + " " + " Transaction Type Name : " + txtName.Text.Trim()
                            Logger.system_log(log_message)

                        End If

                    End If
                Else
                    tStatus = TransState.UpdateNotPossible
                End If

                trans.Commit()


               

            End Using

            Return tStatus

        Else

            Dim tStatus As TransState

            Dim strSql As String

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                strSql = "select IS_AUTHORIZED,STATUS from FIU_TRANSACTION_TYPES where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_TRANSACTION_TYPES set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_TRANSACTION_TYPES set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If _intModno > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_TRANSACTION_TYPES set STATUS = 'C' " & _
                                " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_TRANSACTION_TYPES set STATUS = 'O' " & _
                                " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

                        If _TransType <> _TTypeName Then
                            If _TransType = "" Then
                                log_message = " Authorized : Transaction Type Code : " + txtId.Text.Trim() + "." + " " + " Transaction Type Name : " + _TTypeName
                            Else
                                log_message = " Authorized : Transaction Type Code : " + txtId.Text.Trim() + "." + " " + " Transaction Type Name : " + _TransType + " " + " To " + " " + _TTypeName
                            End If

                            Logger.system_log(log_message)
                        Else

                            log_message = "Authorized : Transaction Type Code : " + txtId.Text.Trim() + "." + " " + " Transaction Type Name : " + txtName.Text.Trim()
                            Logger.system_log(log_message)

                        End If

                    End If
                Else
                    tStatus = TransState.UpdateNotPossible
                End If

                trans.Commit()


               

            End Using

            Return tStatus
        End If

        '------------Commented By Mizan (13-04-16)---------------

        'Dim tStatus As TransState

        'Dim strSql As String

        'tStatus = TransState.UnspecifiedError

        'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        'Using conn As DbConnection = db.CreateConnection()

        '    conn.Open()

        '    Dim trans As DbTransaction = conn.BeginTransaction()

        '    strSql = "select IS_AUTHORIZED,STATUS from FIU_TRANSACTION_TYPES where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

        '    Dim ds As New DataSet

        '    ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

        '    If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

        '        If ds.Tables(0).Rows(0)("STATUS") = "U" Then
        '            strSql = "update FIU_TRANSACTION_TYPES set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
        '            "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
        '            " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

        '        ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
        '            strSql = "update FIU_TRANSACTION_TYPES set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
        '            "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
        '            " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

        '        End If

        '        Dim result As Integer
        '        result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

        '        If result <= 0 Then

        '            tStatus = TransState.NoRecord

        '        ElseIf result > 0 Then

        '            If _intModno > 1 Then

        '                'if previous modification status is D(Deleted) then make it C(Closed)
        '                strSql = "update FIU_TRANSACTION_TYPES set STATUS = 'C' " & _
        '                    " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & (_intModno - 1).ToString() & _
        '                    " and STATUS ='D'"

        '                db.ExecuteNonQuery(trans, CommandType.Text, strSql)

        '                'if previous modification status is L(Deleted) then make it O(Open)
        '                strSql = "update FIU_TRANSACTION_TYPES set STATUS = 'O' " & _
        '                    " where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & (_intModno - 1).ToString() & _
        '                    " and STATUS ='L'"

        '                db.ExecuteNonQuery(trans, CommandType.Text, strSql)



        '            End If
        '            tStatus = TransState.Update
        '        End If
        '    Else
        '        tStatus = TransState.UpdateNotPossible
        '    End If

        '    trans.Commit()

        '    log_message = "Authorized Transaction Type Code " + txtId.Text.Trim() + " Transcation Name " + txtName.Text.Trim()
        '    Logger.system_log(log_message)

        'End Using

        'Return tStatus

    End Function

    '-----Mizan Work (13-04-2016)----------

    Private Sub LoadTransTypeDataForAuth(ByVal strTransTypeCd As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet



            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_TRANSACTION_TYPES Where TRTYPECODE='" & strTransTypeCd & "' and STATUS = 'L' ")



            If ds.Tables(0).Rows.Count > 0 Then


                _strTransTypeCode = ds.Tables(0).Rows(0)("TRTYPECODE").ToString()

                _formMode = FormTransMode.Update


                txtId.Text = _strTransTypeCode
                'txtName.Text = ds.Tables(0).Rows(0)("TRDEFINITION").ToString()
                _TransType = ds.Tables(0).Rows(0)("TRDEFINITION").ToString()

            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadTransTypeData(ByVal strTransTypeCd As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            'ds = db.ExecuteDataSet(CommandType.Text, "select * from APPS where SLNO=" & intslno & " and MODNO=" & intmod)

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_TRANSACTION_TYPES Where TRTYPECODE='" & strTransTypeCd & "' and MODNO=" & intMod.ToString())

            'MsgBox(ds.Tables(0).Rows.Count)

            If ds.Tables(0).Rows.Count > 0 Then


                _strTransTypeCode = ds.Tables(0).Rows(0)("TRTYPECODE").ToString()
                _intModno = intMod

                '_intSlno = intslno

                _formMode = FormTransMode.Update


                txtId.Text = _strTransTypeCode
                txtName.Text = ds.Tables(0).Rows(0)("TRDEFINITION").ToString()
                _TTypeName = ds.Tables(0).Rows(0)("TRDEFINITION").ToString()


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
                lblVerTot.Text = db.ExecuteDataSet(CommandType.Text, "Select Count(ModNo) From FIU_TRANSACTION_TYPES Where TRTYPECODE='" & strTransTypeCd & "'").Tables(0).Rows(0)(0).ToString()





            Else
                '_intModno = 0
                '_intSlno = 0

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


            strSql = "select IS_AUTHORIZED,STATUS from FIU_TRANSACTION_TYPES where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

            Dim ds As New DataSet
            ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0)(0) = False Then 'if not authorized

                    strSql = "delete FIU_TRANSACTION_TYPES where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString() & " and IS_AUTHORIZED=0"

                    db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    _intModno = _intModno - 1

                    tStatus = TransState.Delete


                ElseIf ds.Tables(0).Rows(0)(0) = True Then 'if authorized

                    If ds.Tables(0).Rows(0)("STATUS") = "L" Then 'if this is the last modified data

                        strSql = "delete FIU_TRANSACTION_TYPES where TRTYPECODE='" & _strTransTypeCode & "' and IS_AUTHORIZED=0"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                        strSql = "select * from FIU_TRANSACTION_TYPES where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

                        Dim dsKeeper As New DataSet
                        dsKeeper = db.ExecuteDataSet(trans, CommandType.Text, strSql)


                        strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_TRANSACTION_TYPES where TRTYPECODE='" & _strTransTypeCode & "'"


                        intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)



                        strSql = "Insert Into FIU_TRANSACTION_TYPES(TRTYPECODE, TRDEFINITION," & _
                     "Inserted_On,Modified_On,ModNo,Input_By,Input_Datetime,Is_Authorized,Status)" & _
                     "Values(@P_TRTYPECODE,@P_TRDEFINITION," & _
                     "@P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'D')"

                        Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        db.AddInParameter(commProc, "@P_TRTYPECODE", DbType.String, _strTransTypeCode)
                        db.AddInParameter(commProc, "@P_TRDEFINITION", DbType.String, dsKeeper.Tables(0).Rows(0)("TRDEFINITION"))


                        db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, dsKeeper.Tables(0).Rows(0)("Inserted_On"))

                        db.AddInParameter(commProc, "@P_Modified_On", DbType.DateTime, dsKeeper.Tables(0).Rows(0)("Modified_On"))

                        db.AddInParameter(commProc, "@P_Input_By", DbType.String, CommonAppSet.User)




                        Dim result As Integer
                        result = db.ExecuteNonQuery(commProc, trans)
                        If result < 0 Then
                            tStatus = TransState.Exist

                        Else
                            tStatus = TransState.Delete
                            _intModno = intModno
                        End If

                    Else

                        tStatus = TransState.UpdateNotPossible
                    End If

                End If

            Else
                tStatus = TransState.NoRecord
            End If

          

            trans.Commit()

            log_message = "Delete Transaction Type Code " + txtId.Text.Trim()
            Logger.system_log(log_message)

        End Using


        Return tStatus

    End Function

    Private Function CheckForDelete() As Boolean

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String = ""

        strSql = "select IS_AUTHORIZED,STATUS from FIU_TRANSACTION_TYPES where TRTYPECODE='" & _strTransTypeCode & "' and MODNO=" & _intModno.ToString()

        Dim ds As New DataSet

        ds = db.ExecuteDataSet(CommandType.Text, strSql)

        If ds.Tables(0).Rows(0)("STATUS") = "O" Then
            MessageBox.Show("You can only delete last authorized and modified data", "!! STOP Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return False

        ElseIf ds.Tables(0).Rows(0)("STATUS") = "C" Then
            MessageBox.Show("You can only delete last authorized and modified data", "!! STOP Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return False

        End If

        Return True
    End Function

#End Region


   

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal intModno As Integer, ByVal strAccTypeCode As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()


        _strTransTypeCode = strAccTypeCode
        _intModno = intModno

        ' Add any initialization after the InitializeComponent() call.

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

        If txtId.Enabled = True Then
            txtId.Focus()

        End If



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

                        LoadTransTypeData(_strTransTypeCode, _intModno)

                        lblToolStatus.Text = "!! Information Added Successfully !!"

                        _formMode = FormTransMode.Update


                        EnableDelete()

                        EnableRefresh()

                    ElseIf tState = TransState.Update Then


                        LoadTransTypeData(_strTransTypeCode, _intModno)

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

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadTransTypeData(_strTransTypeCode, _intModno)
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

                        LoadTransTypeData(_strTransTypeCode, _intModno)

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

    Private Sub btnPrevVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevVer.Click
        If _intModno - 1 > 0 Then
            LoadTransTypeData(_strTransTypeCode, _intModno - 1)

        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click

        Dim strAccTypeCode As String = _strTransTypeCode
        Dim intModno As Integer = _intModno


        LoadTransTypeData(_strTransTypeCode, _intModno + 1)

        If _intModno = 0 Then

            LoadTransTypeData(strAccTypeCode, intModno)
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub FrmTransType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If


        If _intModno > 0 Then


            LoadTransTypeData(_strTransTypeCode, _intModno)
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

   
End Class