'
'Author             : Fahad Khan
'Purpose            : Maintain Address Information
'Creation date      : 10-oct-2013
'Stored Procedure(s):  

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmAddressDet

#Region "Global Variables"
    Dim _formName As String = "MaintenanceAddressDet"
    Dim opt As SecForm = New SecForm(_formName)

    Dim _formMode As FormTransMode
    Dim _intSlno As Integer = 0
    Dim _intModno As Integer = 0
    Dim _strAddType_Code As String = ""
    Dim _mod_datetime As Date
    Dim _status As String = ""
    Dim log_message As String = ""

    'For Update
    Dim _address As String = ""
    Dim _addressType As String = ""
    Dim _town As String = ""
    Dim _city As String = ""
    Dim _zipCode As String = ""
    Dim _country As String = ""
    Dim _state As String = ""
    Dim _comments As String = ""
    Dim _addressTypeName As String = ""
    Dim _countryName As String = ""

    'For Auth
    Dim _aaddress As String = ""
    Dim _aaddressType As String = ""
    Dim _atown As String = ""
    Dim _acity As String = ""
    Dim _azipCode As String = ""
    Dim _acountry As String = ""
    Dim _astate As String = ""
    Dim _acomments As String = ""
    Dim _aaddressTypeName As String = ""
    Dim _acountryName As String = ""

    Dim AddressList As New List(Of String)
    Dim _addressLog As String = ""
    Dim _Alog As String = ""

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
        txtAddress.ReadOnly = True
        txtCity.ReadOnly = True
        txtComments.ReadOnly = True
        txtState.ReadOnly = True
        txtTown.ReadOnly = True
        txtZip.ReadOnly = True
        cmbAddress.Enabled = False
        cmbCountry.Enabled = False

    End Sub

    Private Sub EnableFields()
        If txtId.Text.Trim() = "" Then
            txtId.ReadOnly = False
        End If

        txtAddress.ReadOnly = False
        txtCity.ReadOnly = False
        txtComments.ReadOnly = False
        txtState.ReadOnly = False
        txtTown.ReadOnly = False
        txtZip.ReadOnly = False
        cmbAddress.Enabled = True
        cmbCountry.Enabled = True


    End Sub


    Private Sub ClearFields()
        If txtId.ReadOnly = False Then
            txtId.Clear()
        End If

        txtAddress.Clear()
        txtCity.Clear()
        txtComments.Clear()
        txtState.Clear()
        txtTown.Clear()
        txtZip.Clear()
        cmbAddress.SelectedValue = -1
        cmbCountry.SelectedValue = -1

    End Sub

    Private Sub ClearFieldsAll()
        txtId.Clear()
        txtAddress.Clear()
        txtCity.Clear()
        txtComments.Clear()
        txtState.Clear()
        txtTown.Clear()
        txtZip.Clear()
        cmbAddress.SelectedValue = -1
        cmbCountry.SelectedValue = -1


        _strAddType_Code = ""
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
            MessageBox.Show("Id required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtId.Focus()
            Return False
        ElseIf txtAddress.Text.Trim() = "" Then
            MessageBox.Show("Address required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAddress.Focus()
            Return False
        ElseIf cmbAddress.Text.Trim() = "" Then
            MessageBox.Show("Address Type required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbAddress.Focus()
            Return False
        ElseIf cmbCountry.Text.Trim() = "" Then
            MessageBox.Show("Country required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbCountry.Focus()
            Return False
        ElseIf txtTown.Text.Trim() = "" Then
            MessageBox.Show("Town required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtTown.Focus()
            Return False
        ElseIf txtCity.Text.Trim() = "" Then
            MessageBox.Show("City required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtCity.Focus()
            Return False

        End If


        Return True

    End Function

    Private Function SaveData() As TransState

        Dim tStatus As TransState


        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        If _formMode = FormTransMode.Add Then

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Address_Add")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ADDRESS_ID", DbType.String, txtId.Text.Trim())
            db.AddInParameter(commProc, "@ADDRESS_TYPE", DbType.String, cmbAddress.SelectedValue)
            db.AddInParameter(commProc, "@ADDRESS", DbType.String, txtAddress.Text.Trim())
            db.AddInParameter(commProc, "@TOWN", DbType.String, txtTown.Text.Trim())
            db.AddInParameter(commProc, "@CITY", DbType.String, txtCity.Text.Trim())
            db.AddInParameter(commProc, "@ZIP", DbType.String, txtZip.Text.Trim())
            db.AddInParameter(commProc, "@COUNTRY_CODE", DbType.String, cmbCountry.SelectedValue)
            db.AddInParameter(commProc, "@STATE", DbType.String, txtState.Text.Trim())
            db.AddInParameter(commProc, "@COMMENTS", DbType.String, txtComments.Text.Trim())

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer


            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then
                tStatus = TransState.Add

                _strAddType_Code = txtId.Text.Trim()

                _intModno = 1

                log_message = " Added  : Address Code : " + txtId.Text.Trim() + "." + " " + " Address : " + txtAddress.Text.Trim()
                Logger.system_log(log_message)

            Else
                tStatus = TransState.Exist
            End If

            



        ElseIf _formMode = FormTransMode.Update Then



            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Address_Update")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ADDRESS_ID", DbType.String, txtId.Text.Trim())
            db.AddInParameter(commProc, "@ADDRESS_TYPE", DbType.String, cmbAddress.SelectedValue)
            db.AddInParameter(commProc, "@ADDRESS", DbType.String, txtAddress.Text.Trim())
            db.AddInParameter(commProc, "@TOWN", DbType.String, txtTown.Text.Trim())
            db.AddInParameter(commProc, "@CITY", DbType.String, txtCity.Text.Trim())
            db.AddInParameter(commProc, "@ZIP", DbType.String, txtZip.Text.Trim())
            db.AddInParameter(commProc, "@COUNTRY_CODE", DbType.String, cmbCountry.SelectedValue)
            db.AddInParameter(commProc, "@STATE", DbType.String, txtState.Text.Trim())
            db.AddInParameter(commProc, "@COMMENTS", DbType.String, txtComments.Text.Trim())
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)


            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)


            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then
                tStatus = TransState.Update
                _intModno = db.GetParameterValue(commProc, "@RET_MOD_NO")

                '--------------Mizan Work (20-04-16)---------------------

                If _address <> txtAddress.Text.Trim() Then
                    log_message = " Address : " + _address + " " + " To " + " " + txtAddress.Text.Trim() + "." + " "
                    AddressList.Add(log_message)
                End If
                If _addressType <> cmbAddress.Text Then
                    log_message = " Address Type : " + _addressType + " " + " To " + " " + cmbAddress.Text + "." + " "
                    AddressList.Add(log_message)
                End If
                If _country <> cmbCountry.Text Then
                    log_message = " Country : " + _country + " " + " To " + " " + cmbCountry.Text + "." + " "
                    AddressList.Add(log_message)
                End If
                If _town <> txtTown.Text.Trim() Then
                    log_message = " Town : " + _town + " " + " To " + " " + txtTown.Text.Trim() + "." + " "
                    AddressList.Add(log_message)
                End If
                If _city <> txtCity.Text.Trim() Then
                    log_message = " City : " + _city + " " + " To " + " " + txtCity.Text.Trim() + "." + " "
                    AddressList.Add(log_message)
                End If
                If _zipCode <> txtZip.Text.Trim() Then
                    If _zipCode = "" Then
                        log_message = " Zip Code : " + txtZip.Text.Trim() + "." + " "

                    Else
                        log_message = " Zip Code : " + _zipCode + " " + " To " + " " + txtZip.Text.Trim() + "." + " "

                    End If
                    AddressList.Add(log_message)
                End If

                If _state <> txtState.Text.Trim() Then
                    If _state = "" Then
                        log_message = " State : " + txtState.Text.Trim() + "." + " "

                    Else
                        log_message = " State : " + _state + " " + " To " + " " + txtState.Text.Trim() + "." + " "

                    End If
                    AddressList.Add(log_message)
                End If
                If _comments <> txtComments.Text.Trim() Then
                    If _comments = "" Then
                        log_message = " Comments : " + txtComments.Text.Trim() + "." + " "

                    Else
                        log_message = " Comments : " + _comments + " " + " To " + " " + txtComments.Text.Trim() + "." + " "

                    End If
                    AddressList.Add(log_message)
                End If


                For Each addrslist As String In AddressList
                    _addressLog += addrslist
                Next

                _Alog = " Updated : Address Code : " + txtId.Text.ToString() + "." + " " + _addressLog

                Logger.system_log(_Alog)
                _addressLog = ""
                AddressList.Clear()

                '--------------Mizan Work (20-04-16)---------------------

            ElseIf result = 1 Then
                tStatus = TransState.UnspecifiedError
            ElseIf result = 4 Then
                tStatus = TransState.NoRecord

            End If
           


            'log_message = "Updated Address Code " + txtId.Text.Trim() + " Address " + txtAddress.Text.Trim()
            'Logger.system_log(log_message)
        End If

        Return tStatus

    End Function

    '--------------Mizan Work (20-04-16)---------------------

    Private Function AuthorizeData() As TransState

        If _intModno > 1 Then

            LoadAddTypeDataForAuth(_strAddType_Code)

            Dim tStatus As TransState

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Address_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ADDRESS_ID", DbType.String, _strAddType_Code)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                '--------------Mizan Work (20-04-16)---------------------

                If _aaddress <> _address Then
                    If _aaddress = "" Then
                        log_message = " Address : " + _address + "." + " "
                    Else
                        log_message = " Address : " + _aaddress + " " + " To " + " " + _address + "." + " "
                    End If

                    AddressList.Add(log_message)
                End If
                If _aaddressType <> _addressType Then

                    log_message = " Address Type : " + _aaddressType + " " + " To " + " " + _addressType + "." + " "

                    AddressList.Add(log_message)
                End If
                If _acountry <> _country Then
                    log_message = " Country : " + _acountry + " " + " To " + " " + _country + "." + " "
                    AddressList.Add(log_message)
                End If
                If _atown <> _town Then
                    If _atown = "" Then
                        log_message = " Town : " + _town + "." + " "
                    Else
                        log_message = " Town : " + _atown + " " + " To " + " " + _town + "." + " "
                    End If

                    AddressList.Add(log_message)
                End If
                If _acity <> _city Then
                    If _acity = "" Then
                        log_message = " City : " + _city + "." + " "
                    Else
                        log_message = " City : " + _acity + " " + " To " + " " + _city + "." + " "
                    End If

                    AddressList.Add(log_message)
                End If
                If _azipCode <> _zipCode Then
                    If _azipCode = "" Then
                        log_message = " Zip Code : " + _zipCode + "." + " "

                    Else
                        log_message = " Zip Code : " + _azipCode + " " + " To " + " " + _zipCode + "." + " "

                    End If
                    AddressList.Add(log_message)
                End If

                If _astate <> _state Then
                    If _astate = "" Then
                        log_message = " State : " + _state + "." + " "

                    Else
                        log_message = " State : " + _astate + " " + " To " + " " + _state + "." + " "

                    End If
                    AddressList.Add(log_message)
                End If
                If _acomments <> _comments Then
                    If _acomments = "" Then
                        log_message = " Comments : " + _comments + "." + " "

                    Else
                        log_message = " Comments : " + _acomments + " " + " To " + " " + _comments + "." + " "

                    End If
                    AddressList.Add(log_message)
                End If

                For Each addrslist As String In AddressList
                    _addressLog += addrslist
                Next

                _Alog = " Authorized : Address Code : " + txtId.Text.ToString() + "." + " " + _addressLog

                Logger.system_log(_Alog)
                _addressLog = ""
                AddressList.Clear()

                '--------------Mizan Work (20-04-16)---------------------

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

           

            'log_message = "Authorized Address Code " + _strAddType_Code + " Address " + txtAddress.Text.Trim()
            'Logger.system_log(log_message)

            Return tStatus

        Else

            Dim tStatus As TransState


            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Address_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ADDRESS_ID", DbType.String, _strAddType_Code)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                '--------------Mizan Work (20-04-16)---------------------

                If _aaddress <> _address Then
                    If _aaddress = "" Then
                        log_message = " Address : " + _address + "." + " "
                    Else
                        log_message = " Address : " + _aaddress + " " + " To " + " " + _address + "." + " "
                    End If

                    AddressList.Add(log_message)
                End If
                If _aaddressType <> _addressType Then
                    If _aaddressType = "" Then
                    Else
                        log_message = " Address Type : " + _aaddressType + " " + " To " + " " + _addressType + "." + " "
                        AddressList.Add(log_message)
                    End If


                End If
                If _acountry <> _country Then
                    If _acountry = "" Then
                    Else
                        log_message = " Country : " + _acountry + " " + " To " + " " + _country + "." + " "
                        AddressList.Add(log_message)
                    End If


                End If
                If _atown <> _town Then
                    If _atown = "" Then
                        log_message = " Town : " + _town + "." + " "
                    Else
                        log_message = " Town : " + _atown + " " + " To " + " " + _town + "." + " "
                    End If

                    AddressList.Add(log_message)
                End If
                If _acity <> _city Then
                    If _acity = "" Then
                        log_message = " City : " + _city + "." + " "
                    Else
                        log_message = " City : " + _acity + " " + " To " + " " + _city + "." + " "
                    End If

                    AddressList.Add(log_message)
                End If
                If _azipCode <> _zipCode Then
                    If _azipCode = "" Then
                        'log_message = " Zip Code : " + _zipCode + "." + " "

                    Else
                        log_message = " Zip Code : " + _azipCode + " " + " To " + " " + _zipCode + "." + " "
                        AddressList.Add(log_message)
                    End If

                End If

                If _astate <> _state Then
                    If _astate = "" Then
                        ' log_message = " State : " + _state + "." + " "

                    Else
                        log_message = " State : " + _astate + " " + " To " + " " + _state + "." + " "
                        AddressList.Add(log_message)
                    End If

                End If
                If _acomments <> _comments Then
                    If _acomments = "" Then
                        ' log_message = " Comments : " + _comments + "." + " "

                    Else
                        log_message = " Comments : " + _acomments + " " + " To " + " " + _comments + "." + " "
                        AddressList.Add(log_message)
                    End If

                End If

                For Each addrslist As String In AddressList
                    _addressLog += addrslist
                Next

                _Alog = " Authorized : Address Code : " + txtId.Text.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _addressLog

                Logger.system_log(_Alog)
                _addressLog = ""
                AddressList.Clear()

                '--------------Mizan Work (20-04-16)---------------------

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

           

            'log_message = "Authorized Address Code " + _strAddType_Code + " Address " + txtAddress.Text.Trim()
            'Logger.system_log(log_message)

            Return tStatus

        End If

    End Function



    '--------------Mizan Work (20-04-16)---------------------

    Private Sub LoadAddTypeDataForAuth(ByVal strAddTypeCode As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From  GO_T_ADDRESS Where ADDRESS_ID ='" & strAddTypeCode & "' and STATUS ='L' ")

            If ds.Tables(0).Rows.Count > 0 Then


                _strAddType_Code = strAddTypeCode


                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("ADDRESS_ID").ToString()

                txtAddress.Text = ds.Tables(0).Rows(0)("ADDRESS").ToString()
                _aaddress = ds.Tables(0).Rows(0)("ADDRESS").ToString()
                txtCity.Text = ds.Tables(0).Rows(0)("CITY").ToString()
                _acity = ds.Tables(0).Rows(0)("CITY").ToString()
                txtComments.Text = ds.Tables(0).Rows(0)("COMMENTS").ToString()
                _acomments = ds.Tables(0).Rows(0)("COMMENTS").ToString()
                txtState.Text = ds.Tables(0).Rows(0)("STATE").ToString()
                _astate = ds.Tables(0).Rows(0)("STATE").ToString()
                txtTown.Text = ds.Tables(0).Rows(0)("TOWN").ToString()
                _atown = ds.Tables(0).Rows(0)("TOWN").ToString()
                txtZip.Text = ds.Tables(0).Rows(0)("ZIP").ToString()
                _azipCode = ds.Tables(0).Rows(0)("ZIP").ToString()

                ''cmbAddress.SelectedValue = ds.Tables(0).Rows(0)("ADDRESS_TYPE")
                _aaddressType = ds.Tables(0).Rows(0)("ADDRESS_TYPE").ToString()
                ''cmbCountry.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE")
                _acountry = ds.Tables(0).Rows(0)("COUNTRY_CODE").ToString()

                '--------------Mizan Work (26-04-2016---------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE ='" & _acountry & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _acountryName = ds2.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _acountry = _acountryName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_CONTACT_TYPE Where CTYPE_CODE = '" & _aaddressType & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _aaddressTypeName = ds3.Tables(0).Rows(0)("CTYPE_NAME").ToString()
                    _aaddressType = _aaddressTypeName

                End If

                '--------------Mizan Work (26-04-2016---------


            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadAddTypeData(ByVal strAddTypeCode As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Address_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ADDRESS_ID", DbType.String, strAddTypeCode)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intMod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then


                _strAddType_Code = strAddTypeCode
                _intModno = intMod

                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("ADDRESS_ID").ToString()
                txtAddress.Text = ds.Tables(0).Rows(0)("ADDRESS").ToString()
                _address = ds.Tables(0).Rows(0)("ADDRESS").ToString()
                txtCity.Text = ds.Tables(0).Rows(0)("CITY").ToString()
                _city = ds.Tables(0).Rows(0)("CITY").ToString()
                txtComments.Text = ds.Tables(0).Rows(0)("COMMENTS").ToString()
                _comments = ds.Tables(0).Rows(0)("COMMENTS").ToString()
                txtState.Text = ds.Tables(0).Rows(0)("STATE").ToString()
                _state = ds.Tables(0).Rows(0)("STATE").ToString()
                txtTown.Text = ds.Tables(0).Rows(0)("TOWN").ToString()
                _town = ds.Tables(0).Rows(0)("TOWN").ToString()
                txtZip.Text = ds.Tables(0).Rows(0)("ZIP").ToString()
                _zipCode = ds.Tables(0).Rows(0)("ZIP").ToString()

                cmbAddress.SelectedValue = ds.Tables(0).Rows(0)("ADDRESS_TYPE")
                _addressType = ds.Tables(0).Rows(0)("ADDRESS_TYPE").ToString()
                cmbCountry.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE")
                _country = ds.Tables(0).Rows(0)("COUNTRY_CODE").ToString()

                '--------------Mizan Work (26-04-2016---------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE ='" & _country & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _countryName = ds2.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _country = _countryName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_CONTACT_TYPE Where CTYPE_CODE = '" & _addressType & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _addressTypeName = ds3.Tables(0).Rows(0)("CTYPE_NAME").ToString()
                    _addressType = _addressTypeName

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

                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_Address_GetMaxMod")

                commProc2.Parameters.Clear()

                db.AddInParameter(commProc2, "@ADDRESS_ID", DbType.String, strAddTypeCode)

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



    Private Function DeleteData() As TransState

        Dim tStatus As TransState

        Dim intModno As Integer = 0

        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Address_Remove")

        commProc.Parameters.Clear()

        db.AddInParameter(commProc, "@ADDRESS_ID", DbType.String, _strAddType_Code)
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

        log_message = "Deleted Address Code " + _strAddType_Code + " Address " + txtAddress.Text.Trim()
        Logger.system_log(log_message)
        Return tStatus

    End Function


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal strAddTypeCode As String, ByVal intMod As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadAddTypeData(strAddTypeCode, intMod)
        _strAddType_Code = strAddTypeCode
        _intModno = intMod


    End Sub


#End Region


    Private Sub FrmAddressDet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
        CommonUtil.FillComboBox("GO_ContactType_GetList", cmbAddress)
        CommonUtil.FillComboBox("GO_CountryType_GetList", cmbCountry)

        If _intModno > 0 Then
            LoadAddTypeData(_strAddType_Code, _intModno)
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



        If Not (_strAddType_Code.Trim() = "") Then

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

                        LoadAddTypeData(_strAddType_Code, _intModno)

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

                        LoadAddTypeData(_strAddType_Code, _intModno)

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
        LoadAddTypeData(_strAddType_Code, _intModno)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try


            If MessageBox.Show("Do you really want to delete?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                tState = DeleteData()

                If tState = TransState.Delete Then


                    _formMode = FormTransMode.Add

                    LoadAddTypeData(_strAddType_Code, _intModno)

                    DisableAuth()

                    If _strAddType_Code = "" Then

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
            LoadAddTypeData(_strAddType_Code, _intModno - 1)
        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click
        Dim strAddTypeCode As String = _strAddType_Code
        Dim intModno As Integer = _intModno
        If intModno > 0 Then
            LoadAddTypeData(_strAddType_Code, _intModno + 1)

            If _intModno = 0 Then
                LoadAddTypeData(strAddTypeCode, intModno)
            End If
        End If
    End Sub

    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try
            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then



                tState = AuthorizeData()

                If tState = TransState.Update Then
                    LoadAddTypeData(_strAddType_Code, _intModno)
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
End Class