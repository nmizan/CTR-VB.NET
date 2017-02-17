Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization

Public Class FrmComRegAuth

#Region "Global Variables"

    Dim _formName As String = "MaintenanceRegualrityAuthDetail"
    Dim opt As SecForm = New SecForm(_formName)


    Dim _formMode As FormTransMode
    'Dim _intSlno As Integer = 0
    Dim _intModno As Integer = 0
    Dim _strRegAuth_Code As String = ""
    Dim log_message As String
    Dim _AName As String = ""

    Dim _AuthName As String = ""
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
        txtRegAuthCode.ReadOnly = True
        txtRegAuthName.ReadOnly = True
        txtInserted_On.ReadOnly = True
        txtModified_On.ReadOnly = True
        'txtPrevDivCode.ReadOnly = True
    End Sub

    Private Sub EnableFields()
        txtRegAuthCode.ReadOnly = False
        txtRegAuthName.ReadOnly = False
        txtInserted_On.ReadOnly = False
        txtModified_On.ReadOnly = False
        'txtPrevDivCode.ReadOnly = False

    End Sub


    Private Sub ClearFields()
        txtRegAuthCode.Clear()
        txtRegAuthName.Clear()
        txtInserted_On.Clear()
        txtModified_On.Clear()
        'txtPrevDivCode.Clear()
    End Sub

    Private Sub ClearFieldsAll()
        txtRegAuthCode.Clear()
        txtRegAuthName.Clear()
        txtInserted_On.Clear()
        txtModified_On.Clear()
        ' txtPrevDivCode.Clear()

        '_intSlno = 0
        _strRegAuth_Code = ""
        _intModno = 0

        lblVerNo.Text = ""
        lblVerTot.Text = ""

        lblInputBy.Text = ""
        lblInputdt.Text = ""
        lblAuthBy.Text = ""
        lblAuthDate.Text = ""
        lblModNo.Text = ""

    End Sub

    Private Function CheckValidData() As Boolean

        If txtRegAuthCode.Text.Trim() = "" Then
            MessageBox.Show("Code required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtRegAuthCode.Focus()
            Return False
        ElseIf txtRegAuthName.Text.Trim() = "" Then
            MessageBox.Show("Name required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtRegAuthName.Focus()
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

            'intSlno = db.ExecuteDataSet(CommandType.Text, "select ISNULL(max(SLNO),0)+1 maxslno from APPS").Tables(0).Rows(0)(0)

            'strSql = "insert into APPS(SLNO, APP_ID, APP_NAME, MODNO, " + _
            '    " INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  " + _
            '    " STATUS) values(@P_Slno, @P_App_Id, @P_App_Name, " + _
            '    " 1, @P_Input_By, getdate(), 0, 'U')"

            strSql = "Insert Into FIU_COMPANY_REG_AUTHORITY(REG_AUTHORITY_CODE, REG_AUTHORITY_NAME, INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS)" & _
                     "Values(@P_REG_AUTHORITY_CODE, @P_REG_AUTHORITY_NAME,@P_Inserted_On,@P_Modified_On,1,@P_Input_By,getdate(),0,'U')"

            Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

            commProc.Parameters.Clear()
            db.AddInParameter(commProc, "@P_REG_AUTHORITY_CODE", DbType.String, txtRegAuthCode.Text.Trim())
            db.AddInParameter(commProc, "@P_REG_AUTHORITY_NAME", DbType.String, txtRegAuthName.Text.Trim())

            If txtInserted_On.Text <> "" Then
                db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, DateTime.ParseExact(txtInserted_On.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
            Else
                db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, DBNull.Value)
            End If

            If txtModified_On.Text <> "" Then
                db.AddInParameter(commProc, "@P_Modified_On", DbType.DateTime, DateTime.ParseExact(txtModified_On.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
            Else
                db.AddInParameter(commProc, "@P_Modified_On", DbType.DateTime, DBNull.Value)
            End If

            db.AddInParameter(commProc, "@P_Input_By", DbType.String, CommonAppSet.User)

            Dim result As Integer

            result = db.ExecuteNonQuery(commProc)

            If result < 0 Then  '  Don't Understand
                tStatus = TransState.Exist
            Else
                tStatus = TransState.Add
                '_intSlno = intSlno
                _strRegAuth_Code = txtRegAuthCode.Text.Trim()

                _intModno = 1

                log_message = " Added : Regularity Authority Code : " + txtRegAuthCode.Text.Trim() + " " + " Authority Name : " + txtRegAuthName.Text.Trim()
                Logger.system_log(log_message)

            End If

           

        ElseIf _formMode = FormTransMode.Update Then


            Using conn As DbConnection = db.CreateConnection()


                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                db.ExecuteNonQuery(trans, CommandType.Text, "delete FIU_COMPANY_REG_AUTHORITY where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and IS_AUTHORIZED=0")
                Dim ds As New DataSet


                strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_COMPANY_REG_AUTHORITY where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "'"


                intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)

                strSql = "Insert Into FIU_COMPANY_REG_AUTHORITY(REG_AUTHORITY_CODE, REG_AUTHORITY_NAME, INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED, STATUS)" & _
                     "Values(@P_REG_AUTHORITY_CODE, @P_REG_AUTHORITY_NAME,@P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'U')"


                Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@P_REG_AUTHORITY_CODE", DbType.String, txtRegAuthCode.Text.Trim())
                db.AddInParameter(commProc, "@P_REG_AUTHORITY_NAME", DbType.String, txtRegAuthName.Text.Trim())

                If txtInserted_On.Text <> "" Then
                    db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, DateTime.ParseExact(txtInserted_On.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
                Else
                    db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, DBNull.Value)
                End If

                If txtModified_On.Text <> "" Then
                    db.AddInParameter(commProc, "@P_Modified_On", DbType.DateTime, DateTime.ParseExact(txtModified_On.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
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

                    '------------Mizan Work (10-04-16)---------
                    If _AName <> txtRegAuthName.Text.Trim() Then

                        log_message = " Updated : Regularity Authority Code : " + txtRegAuthCode.Text.Trim() + "." + " " + " Authority Name : " + _AName + " " + " To " + " " + txtRegAuthName.Text.Trim()
                        Logger.system_log(log_message)
                    Else
                        log_message = " Updated : Regularity Authority Code : " + txtRegAuthCode.Text.Trim()
                        Logger.system_log(log_message)
                    End If
                    '------------Mizan Work (10-04-16)---------

                End If

                'intModno = db.ExecuteDataSet(trans, CommandType.Text, "select max(ISNULL(MODNO,0))+1 maxmodno from DEPARTMENT where SLNO=" & _intSlno.ToString()).Tables(0).Rows(0)(0)

                trans.Commit()
                
            End Using

        End If

        Return tStatus

    End Function

    '------------Mizan Work (13-04-16)---------
    Private Sub LoadComRegAuthDataForAuthorize(ByVal strRegAuthCode As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet


            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_COMPANY_REG_AUTHORITY Where REG_AUTHORITY_CODE='" & strRegAuthCode & "' and STATUS= 'L' ")

            If ds.Tables(0).Rows.Count > 0 Then

                _strRegAuth_Code = strRegAuthCode


                _formMode = FormTransMode.Update




                txtRegAuthCode.Text = ds.Tables(0).Rows(0)("REG_AUTHORITY_CODE").ToString()

                'txtRegAuthName.Text = ds.Tables(0).Rows(0)("REG_AUTHORITY_NAME").ToString()
                _AuthName = ds.Tables(0).Rows(0)("REG_AUTHORITY_NAME").ToString()


            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadComRegAuthData(ByVal strRegAuthCode As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet


            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_COMPANY_REG_AUTHORITY Where REG_AUTHORITY_CODE='" & strRegAuthCode & "' and MODNO=" & intMod.ToString())

            If ds.Tables(0).Rows.Count > 0 Then

                _strRegAuth_Code = strRegAuthCode
                _intModno = intMod

                '_intSlno = intslno

                _formMode = FormTransMode.Update




                txtRegAuthCode.Text = ds.Tables(0).Rows(0)("REG_AUTHORITY_CODE").ToString()



                'If ds.Tables(0).Rows(0)("GENDER").ToString = "M" Then
                'cmbSex.SelectedIndex = 0
                'ElseIf ds.Tables(0).Rows(0)("GENDER").ToString = "F" Then
                'cmbSex.SelectedIndex = 1
                'Else
                'cmbSex.SelectedIndex = -1
                'End If


                txtRegAuthName.Text = ds.Tables(0).Rows(0)("REG_AUTHORITY_NAME").ToString()
                _AName = ds.Tables(0).Rows(0)("REG_AUTHORITY_NAME").ToString()
                txtInserted_On.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("INSERTED_ON"))
                txtModified_On.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("MODIFIED_ON"))

                lblInputBy.Text = ds.Tables(0).Rows(0)("INPUT_BY").ToString()
                lblInputdt.Text = ds.Tables(0).Rows(0)("INPUT_DATETIME").ToString()
                lblAuthBy.Text = ds.Tables(0).Rows(0)("AUTH_BY").ToString()
                lblAuthDate.Text = ds.Tables(0).Rows(0)("AUTH_DATETIME").ToString()

                chkAuthorized.Checked = ds.Tables(0).Rows(0)("IS_AUTHORIZED")

                If ds.Tables(0).Rows(0)("STATUS") = "L" Or ds.Tables(0).Rows(0)("STATUS") = "U" Or ds.Tables(0).Rows(0)("STATUS") = "O" Then
                    chkOpen.Checked = True
                Else
                    chkOpen.Checked = False
                End If

                lblModNo.Text = ds.Tables(0).Rows(0)("MODNO").ToString()
                lblVerNo.Text = ds.Tables(0).Rows(0)("MODNO").ToString()
                lblVerTot.Text = db.ExecuteDataSet(CommandType.Text, "Select Count(MODNO) From FIU_COMPANY_REG_AUTHORITY Where REG_AUTHORITY_CODE='" & strRegAuthCode & "'").Tables(0).Rows(0)(0).ToString()





            Else
                '_intModno = 0
                '_intSlno = 0

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal strRegAuthCode As String, ByVal intMod As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LoadComRegAuthData(strRegAuthCode, intMod)

        ' Add any initialization after the InitializeComponent() call.
        _strRegAuth_Code = strRegAuthCode
        _intModno = intMod


    End Sub

    Private Function AuthorizeData() As TransState

        '------------Mizan Work (13-04-16)---------

        If _intModno > 1 Then

            LoadComRegAuthDataForAuthorize(_strRegAuth_Code)

            Dim tStatus As TransState

            Dim strSql As String

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                strSql = "select IS_AUTHORIZED,STATUS from FIU_COMPANY_REG_AUTHORITY where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_COMPANY_REG_AUTHORITY set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where  REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()


                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_COMPANY_REG_AUTHORITY set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If _intModno > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_COMPANY_REG_AUTHORITY set STATUS = 'C' " & _
                                " where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_COMPANY_REG_AUTHORITY set STATUS = 'O' " & _
                                " where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

                        If _AuthName <> _AName Then
                            If _AuthName = "" Then
                                log_message = " Authorized : Regularity Authority Code : " + txtRegAuthCode.Text.Trim() + "." + " " + " Authority Name : " + _AName
                            Else
                                log_message = " Authorized : Regularity Authority Code : " + txtRegAuthCode.Text.Trim() + "." + " " + " Authority Name : " + _AuthName + " " + " To " + " " + _AName
                            End If

                            Logger.system_log(log_message)
                        Else
                            log_message = " Authorized : Regularity Authority Code : " + txtRegAuthCode.Text.Trim()
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

                strSql = "select IS_AUTHORIZED,STATUS from FIU_COMPANY_REG_AUTHORITY where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_COMPANY_REG_AUTHORITY set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where  REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()


                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_COMPANY_REG_AUTHORITY set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If _intModno > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_COMPANY_REG_AUTHORITY set STATUS = 'C' " & _
                                " where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_COMPANY_REG_AUTHORITY set STATUS = 'O' " & _
                                " where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

                        If _AuthName <> _AName Then
                            If _AuthName = "" Then
                                log_message = " Authorized : Regularity Authority Code : " + txtRegAuthCode.Text.Trim() + "." + " " + " Authority Name : " + _AName
                            Else
                                log_message = " Authorized : Regularity Authority Code : " + txtRegAuthCode.Text.Trim() + "." + " " + " Authority Name : " + _AuthName + " " + " To " + " " + _AName
                            End If

                            Logger.system_log(log_message)
                        Else
                            log_message = " Authorized : Regularity Authority Code : " + txtRegAuthCode.Text.Trim()
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

        '    strSql = "select IS_AUTHORIZED,STATUS from FIU_COMPANY_REG_AUTHORITY where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()

        '    Dim ds As New DataSet

        '    ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

        '    If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

        '        If ds.Tables(0).Rows(0)("STATUS") = "U" Then
        '            strSql = "update FIU_COMPANY_REG_AUTHORITY set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
        '            "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
        '            " where  REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()


        '        ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
        '            strSql = "update FIU_COMPANY_REG_AUTHORITY set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
        '            "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
        '            " where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()

        '        End If

        '        Dim result As Integer
        '        result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)



        '        If result <= 0 Then

        '            tStatus = TransState.NoRecord

        '        ElseIf result > 0 Then

        '            If _intModno > 1 Then

        '                'if previous modification status is D(Deleted) then make it C(Closed)
        '                strSql = "update FIU_COMPANY_REG_AUTHORITY set STATUS = 'C' " & _
        '                    " where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
        '                    " and STATUS ='D'"

        '                db.ExecuteNonQuery(trans, CommandType.Text, strSql)

        '                'if previous modification status is L(Deleted) then make it O(Open)
        '                strSql = "update FIU_COMPANY_REG_AUTHORITY set STATUS = 'O' " & _
        '                    " where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
        '                    " and STATUS ='L'"

        '                db.ExecuteNonQuery(trans, CommandType.Text, strSql)



        '            End If
        '            tStatus = TransState.Update

        '        End If
        '    Else
        '        tStatus = TransState.UpdateNotPossible
        '    End If

        '    trans.Commit()
        '   

        '    log_message = "Authorized Regularity Authority Code " + txtRegAuthCode.Text.Trim() + " Authority Name " + txtRegAuthName.Text.Trim()
        '    Logger.system_log(log_message)

        'End Using

        'Return tStatus

    End Function
    Private Function CheckForDelete() As Boolean

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String = ""

        strSql = "select IS_AUTHORIZED,STATUS from FIU_COMPANY_REG_AUTHORITY where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()

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

    Private Function DeleteData() As TransState

        Dim tStatus As TransState

        Dim intModno As Integer = 0

        tStatus = TransState.UnspecifiedError

        Dim strSql As String = ""

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Using conn As DbConnection = db.CreateConnection()

            conn.Open()

            Dim trans As DbTransaction = conn.BeginTransaction()


            strSql = "select IS_AUTHORIZED,STATUS from FIU_COMPANY_REG_AUTHORITY where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()

            Dim ds As New DataSet
            ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0)(0) = False Then 'if not authorized

                    strSql = "delete FIU_COMPANY_REG_AUTHORITY where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString() & " and IS_AUTHORIZED=0"

                    db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    _intModno = _intModno - 1

                    tStatus = TransState.Delete


                ElseIf ds.Tables(0).Rows(0)(0) = True Then 'if authorized

                    If ds.Tables(0).Rows(0)("STATUS") = "L" Then 'if this is the last modified data

                        strSql = "delete FIU_COMPANY_REG_AUTHORITY where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "'  and IS_AUTHORIZED=0"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                        strSql = "select * from FIU_COMPANY_REG_AUTHORITY where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "' and MODNO=" & _intModno.ToString()

                        Dim dsKeeper As New DataSet
                        dsKeeper = db.ExecuteDataSet(trans, CommandType.Text, strSql)


                        strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_COMPANY_REG_AUTHORITY where REG_AUTHORITY_CODE='" & _strRegAuth_Code & "'"


                        intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)

                        strSql = "Insert Into FIU_COMPANY_REG_AUTHORITY(REG_AUTHORITY_CODE, REG_AUTHORITY_NAME, INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS)" & _
                     "Values(@P_REG_AUTHORITY_CODE, @P_REG_AUTHORITY_NAME, @P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'D')"


                        Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()

                        db.AddInParameter(commProc, "@P_REG_AUTHORITY_CODE", DbType.String, dsKeeper.Tables(0).Rows(0)("REG_AUTHORITY_CODE"))
                        db.AddInParameter(commProc, "@P_REG_AUTHORITY_NAME", DbType.String, dsKeeper.Tables(0).Rows(0)("REG_AUTHORITY_NAME"))



                        db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, dsKeeper.Tables(0).Rows(0)("INSERTED_ON"))

                        db.AddInParameter(commProc, "@P_Modified_On", DbType.DateTime, dsKeeper.Tables(0).Rows(0)("MODIFIED_ON"))

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

            log_message = "Delete Regularity Authority Code " + txtRegAuthCode.Text.Trim()
            Logger.system_log(log_message)

        End Using


        Return tStatus

    End Function

#End Region


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

    Private Sub FrmComRegAuth_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
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

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        lblToolStatus.Text = ""
        _formMode = FormTransMode.Add

        EnableSave()

        ClearFieldsAll()
        EnableFields()

        DisableRefresh()
        DisableDelete()

        If txtRegAuthCode.Enabled = True Then
            txtRegAuthCode.Focus()
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

                        LoadComRegAuthData(_strRegAuth_Code, _intModno)

                        lblToolStatus.Text = "!! Information Added Successfully !!"

                        _formMode = FormTransMode.Update


                        EnableDelete()

                        EnableRefresh()

                    ElseIf tState = TransState.Update Then

                        'LoadAppData(_intSlno, _intModno)

                        LoadComRegAuthData(_strRegAuth_Code, _intModno)

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
        LoadComRegAuthData(_strRegAuth_Code, _intModno)
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

                        LoadComRegAuthData(_strRegAuth_Code, _intModno)

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
            LoadComRegAuthData(_strRegAuth_Code, _intModno - 1)
        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click
        Dim strRegAuth_Code As String = _strRegAuth_Code
        Dim intModno As Integer = _intModno


        LoadComRegAuthData(_strRegAuth_Code, _intModno + 1)

        If _intModno = 0 Then
            'LoadAppData(intSlno, intModno)
            LoadComRegAuthData(strRegAuth_Code, intModno)
        End If
    End Sub
End Class