'
'Author             : Fahad Khan
'Purpose            : Maintain Contact Type Information
'Creation date      : 26-Sept-2013
'Stored Procedure(s):  
'

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmContactTypeDet

#Region "Global Variables"

    Dim _formName As String = "MaintenanceContactTypeDet"
    Dim opt As SecForm = New SecForm(_formName)


    Dim _formMode As FormTransMode
    Dim _intModno As Integer = 0
    Dim _strContType_Code As String = ""
    Dim _mod_datetime As Date
    Dim _status As String = ""
    Dim log_message As String = ""

    Dim _ContactType As String = ""
    Dim _ConType As String = ""
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

    End Sub

    Private Sub EnableFields()
        If txtId.Text.Trim() = "" Then
            txtId.ReadOnly = False
        End If

        txtName.ReadOnly = False


    End Sub


    Private Sub ClearFields()
        If txtId.ReadOnly = False Then
            txtId.Clear()
        End If

        txtName.Clear()

    End Sub

    Private Sub ClearFieldsAll()
        txtId.Clear()
        txtName.Clear()



        _strContType_Code = ""
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


        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        If _formMode = FormTransMode.Add Then

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_ContactType_Add")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@CTYPE_CODE", DbType.String, txtId.Text.Trim())
            db.AddInParameter(commProc, "@CTYPE_NAME", DbType.String, txtName.Text.Trim())


            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer


            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then
                tStatus = TransState.Add

                _strContType_Code = txtId.Text.Trim()

                _intModno = 1

                log_message = " Added : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + txtName.Text.ToString()
                Logger.system_log(log_message)

            Else
                tStatus = TransState.Exist
            End If

            

        ElseIf _formMode = FormTransMode.Update Then



            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_ContactType_Update")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@CTYPE_CODE", DbType.String, txtId.Text.Trim())
            db.AddInParameter(commProc, "@CTYPE_NAME", DbType.String, txtName.Text.Trim())

            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)


            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)


            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then
                tStatus = TransState.Update
                _intModno = db.GetParameterValue(commProc, "@RET_MOD_NO")

                '----------Mizan Work (11-04-2016)---------------

                If _ContactType <> txtName.Text.Trim() Then

                    log_message = " Updated : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + _ContactType + " " + " To " + " " + txtName.Text.ToString()
                    Logger.system_log(log_message)
                Else
                    log_message = " Updated : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + txtName.Text.Trim()
                    Logger.system_log(log_message)
                End If

                '----------Mizan Work (11-04-2016)---------------


            ElseIf result = 1 Then
                tStatus = TransState.UnspecifiedError
            ElseIf result = 4 Then
                tStatus = TransState.NoRecord

            End If
           
            'log_message = "Updated Contact Type " + txtId.Text.Trim() + " Name " + _ContactType + " To " + txtName.Text.ToString()
            'Logger.system_log(log_message)


        End If

        Return tStatus

    End Function

    '----------Mizan Work (18-04-2016)---------------
    Private Sub LoadContTypeDataForAuth(ByVal strContTypeCode As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From GO_CONTACT_TYPE Where CTYPE_CODE ='" & strContTypeCode & "' and STATUS= 'L' ")

            If ds.Tables(0).Rows.Count > 0 Then


                _strContType_Code = strContTypeCode


                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("CTYPE_CODE").ToString()
                txtName.Text = ds.Tables(0).Rows(0)("CTYPE_NAME").ToString()
                _ConType = ds.Tables(0).Rows(0)("CTYPE_NAME").ToString()

            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadContTypeData(ByVal strContTypeCode As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_ContactType_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@CTYPE_CODE", DbType.String, strContTypeCode)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intMod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then


                _strContType_Code = strContTypeCode
                _intModno = intMod

                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("CTYPE_CODE").ToString()
                txtName.Text = ds.Tables(0).Rows(0)("CTYPE_NAME").ToString()
                _ContactType = ds.Tables(0).Rows(0)("CTYPE_NAME").ToString()

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

                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_ContactType_GetMaxMod")

                commProc2.Parameters.Clear()

                db.AddInParameter(commProc2, "@CTYPE_CODE", DbType.String, strContTypeCode)

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

                If chkAuthorized.Checked = False And (Not lblInputBy.Text.Trim = CommonAppSet.User) Then
                    EnableAuth()
                End If


            Else

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

    Public Sub New(ByVal strContTypeCode As String, ByVal intMod As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadContTypeData(strContTypeCode, intMod)
        _strContType_Code = strContTypeCode
        _intModno = intMod


    End Sub

    Private Function DeleteData() As TransState

        Dim tStatus As TransState

        Dim intModno As Integer = 0

        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim commProc As DbCommand = db.GetStoredProcCommand("GO_ContactType_Remove")

        commProc.Parameters.Clear()

        db.AddInParameter(commProc, "@CTYPE_CODE", DbType.String, _strContType_Code)
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

        log_message = "Deleted Contact Type " + _strContType_Code + " Name " + txtName.Text.ToString()
        Logger.system_log(log_message)

        Return tStatus

    End Function

    '----------Mizan Work (18-04-2016)---------------

    Private Function AuthorizeData() As TransState

        If _intModno > 1 Then

            LoadContTypeDataForAuth(_strContType_Code)

            Dim tStatus As TransState

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_ContactType_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@CTYPE_CODE", DbType.String, _strContType_Code)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                If _ConType <> _ContactType Then
                    If _ConType = "" Then
                        log_message = " Authorized : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + _ContactType
                    Else
                        log_message = " Authorized : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + _ConType + " " + " To " + " " + _ContactType
                    End If

                    Logger.system_log(log_message)
                Else
                    log_message = " Authorized : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + txtName.Text.Trim()
                    Logger.system_log(log_message)
                End If

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

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_ContactType_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@CTYPE_CODE", DbType.String, _strContType_Code)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                If _ConType <> _ContactType Then
                    If _ConType = "" Then
                        log_message = " Authorized : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + _ContactType
                    Else
                        log_message = " Authorized : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + _ConType + " " + " To " + " " + _ContactType
                    End If

                    Logger.system_log(log_message)
                Else
                    log_message = " Authorized : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + txtName.Text.Trim()
                    Logger.system_log(log_message)
                End If


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


        '--------------------Commented By Mizan(18-04-16)-------------------

        'Dim tStatus As TransState

        'tStatus = TransState.UnspecifiedError

        'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        'Dim commProc As DbCommand = db.GetStoredProcCommand("GO_ContactType_Auth")

        'commProc.Parameters.Clear()

        'db.AddInParameter(commProc, "@CTYPE_CODE", DbType.String, _strContType_Code)
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

        'If _ConType <> _ContactType Then
        '    If _ConType = "" Then
        '        log_message = " Authorized : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + _ContactType
        '    Else
        '        log_message = " Authorized : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + _ConType + " " + " To " + " " + _ContactType
        '    End If

        '    Logger.system_log(log_message)
        'Else
        '    log_message = " Authorized : Contact Type Code : " + txtId.Text.Trim() + "." + " " + " Contact Type Name : " + txtName.Text.Trim()
        '    Logger.system_log(log_message)
        'End If
        ''log_message = "Authorized Contact Type " + _strContType_Code + " Name " + txtName.Text.ToString()
        ''Logger.system_log(log_message)

        'Return tStatus

    End Function

#End Region

    Private Sub FrmContactTypeDet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If


        If _intModno > 0 Then
            LoadContTypeData(_strContType_Code, _intModno)
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



        If Not (_strContType_Code.Trim() = "") Then

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


            If chkAuthorized.Checked = False And (Not lblInputBy.Text.Trim = CommonAppSet.User) Then
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

                If CheckValidData() Then

                    tState = SaveData()

                    If tState = TransState.Add Then

                        LoadContTypeData(_strContType_Code, _intModno)

                        lblToolStatus.Text = "!! Information Added Successfully !!"

                        _formMode = FormTransMode.Update


                        EnableDelete()

                        EnableRefresh()


                    ElseIf tState = TransState.Update Then

                        LoadContTypeData(_strContType_Code, _intModno)

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
        LoadContTypeData(_strContType_Code, _intModno)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try


            If MessageBox.Show("Do you really want to delete?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                tState = DeleteData()

                If tState = TransState.Delete Then


                    _formMode = FormTransMode.Add

                    LoadContTypeData(_strContType_Code, _intModno)

                    DisableAuth()

                    If _strContType_Code = "" Then

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

    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try
            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then



                tState = AuthorizeData()

                If tState = TransState.Update Then
                    LoadContTypeData(_strContType_Code, _intModno)
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

    Private Sub btnPrevVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevVer.Click
        If _intModno - 1 > 0 Then
            LoadContTypeData(_strContType_Code, _intModno - 1)
        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click
        Dim strContTypeCode As String = _strContType_Code
        Dim intModno As Integer = _intModno
        If intModno > 0 Then
            LoadContTypeData(_strContType_Code, _intModno + 1)

            If _intModno = 0 Then
                LoadContTypeData(strContTypeCode, intModno)
            End If
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class