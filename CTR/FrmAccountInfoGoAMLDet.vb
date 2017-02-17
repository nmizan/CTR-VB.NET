'
'Author             : Fahad Khan
'Purpose            : Maintain Account Information
'Creation date      : 30-oct-2013
'Stored Procedure(s):  

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization



Public Class FrmAccountInfoGoAMLDet

#Region "Global Variables"

    Dim _formName As String = "MaintenanceAccountInfoGoAMLDet"
    Dim opt As SecForm = New SecForm(_formName)

    Dim _formMode As FormTransMode
    Dim _strAccNO As String = ""
    Dim _intModno As Integer = 0
    Dim _mod_datetime As Date
    Dim _status As String = ""
    Dim _RowEditMode As Boolean = False
    Dim _IsResetRow As Boolean = False
    Dim _strOwner_Code As String = ""

    'For Update
    Dim log_message As String = ""
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

        txtAccNo.ReadOnly = True
        txtIban.ReadOnly = True
        txtClientNumber.ReadOnly = True
        txtOpenDate.ReadOnly = True
        txtCloseDate.ReadOnly = True
        txtBeneficiary.ReadOnly = True
        txtBenefiComments.ReadOnly = True
        txtComments.ReadOnly = True
        cbmStatus.Enabled = False
        cmbAccountType.Enabled = False
        cmbCurrency.Enabled = False
        txtEntityId.ReadOnly = True




    End Sub

    Private Sub EnableFields()

        If _intModno = 0 Then

            txtAccNo.ReadOnly = False
        End If

        'txtAccNo.ReadOnly = False
        txtIban.ReadOnly = False
        txtClientNumber.ReadOnly = False
        txtOpenDate.ReadOnly = False
        txtCloseDate.ReadOnly = False
        txtBeneficiary.ReadOnly = False
        txtBenefiComments.ReadOnly = False
        txtComments.ReadOnly = False
        cbmStatus.Enabled = True
        cmbAccountType.Enabled = True
        cmbCurrency.Enabled = True
        txtEntityId.ReadOnly = False


    End Sub


    Private Sub ClearFields()

        'txtAccNo.Clear()

        If txtAccNo.ReadOnly = False Then
            txtAccNo.Clear()
        End If
        txtIban.Clear()
        txtClientNumber.Clear()
        txtOpenDate.Clear()
        txtCloseDate.Clear()
        txtBeneficiary.Clear()
        txtBenefiComments.Clear()
        txtComments.Clear()
        'cbmStatus.SelectedIndex = -1
        'cmbAccountType.SelectedIndex = -1
        'cmbCurrency.SelectedIndex = -1
        cbmStatus.SelectedValue = "A"
        cmbAccountType.SelectedValue = "S"
        cmbCurrency.SelectedValue = "BDT"
        txtEntityId.Clear()

       

    End Sub

    Private Sub ClearFieldsAll()


      
        'txtAccNo.Clear()
        txtIban.Clear()
        txtClientNumber.Clear()
        txtOpenDate.Clear()
        txtCloseDate.Clear()
        txtBeneficiary.Clear()
        txtBenefiComments.Clear()
        txtComments.Clear()
        cbmStatus.SelectedValue = "A"
        cmbAccountType.SelectedValue = "S"
        cmbCurrency.SelectedValue = "BDT"
       
        txtEntityId.Clear()


        _strAccNO = ""
        _intModno = 0

        lblVerNo.Text = ""
        lblVerTot.Text = ""

        lblInputBy.Text = ""
        lblInputDate.Text = ""
        lblAuthBy.Text = ""
        lblAuthDate.Text = ""

        lblModNo.Text = ""

        _IsResetRow = False


    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal strAccNO As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        txtAccNo.Text = strAccNO
        _strAccNO = strAccNO
        _strOwner_Code = strAccNO

    End Sub

    Public Sub New(ByVal strAccNO As String, ByVal intmod As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' LoadMainData(strId, intmod)
        _strAccNO = strAccNO

        _intModno = intmod

    End Sub

    Private Sub LoadAccountData(ByVal strAccNO As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ACNUMBER", DbType.String, strAccNO)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then

                _strAccNO = strAccNO


                _formMode = FormTransMode.Update


                txtAccNo.Text = ds.Tables(0).Rows(0)("ACNUMBER").ToString()


                txtIban.Text = ds.Tables(0).Rows(0)("IBAN").ToString()
                txtClientNumber.Text = ds.Tables(0).Rows(0)("CLIENT_NUMBER").ToString()


                cmbCurrency.SelectedValue = ds.Tables(0).Rows(0)("CURRENCY_CODE")
                cmbAccountType.SelectedValue = ds.Tables(0).Rows(0)("ACCOUNT_TYPE")
                cbmStatus.SelectedValue = ds.Tables(0).Rows(0)("STATUS_CODE")


                txtEntityId.Text = ds.Tables(0).Rows(0)("ENTITY_ID").ToString()


                txtOpenDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("OPENED"))
                txtCloseDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("CLOSED"))

                txtBeneficiary.Text = ds.Tables(0).Rows(0)("BENEFICIARY").ToString()

                txtBenefiComments.Text = ds.Tables(0).Rows(0)("BENEFICIARY_COMMENTS").ToString()

                txtComments.Text = ds.Tables(0).Rows(0)("COMMENTS").ToString()



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

                _intModno = ds.Tables(0).Rows(0)("MOD_NO")
                ' Dim intmod As Integer = ds.Tables(0).Rows(0)("MOD_NO")





                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_GetMaxMod")

                commProc2.Parameters.Clear()

                db.AddInParameter(commProc2, "@ACNUMBER", DbType.String, strAccNO)

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




    Private Function CheckValidData() As Boolean

        If txtAccNo.Text.Trim() = "" Then
            MessageBox.Show("Account Number required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAccNo.Focus()
            Return False

        ElseIf txtOpenDate.Text.Trim() = "/  /" Then
            MessageBox.Show("Open Date required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtOpenDate.Focus()
            Return False


        ElseIf cmbCurrency.Text.Trim() = "" Then
            MessageBox.Show("Currency Code required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbCurrency.Focus()
            Return False
        ElseIf cmbAccountType.Text.Trim() = "" Then
            MessageBox.Show("Person Account Type required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbAccountType.Focus()
            Return False
       

        ElseIf cbmStatus.Text.Trim() = "" Then
            MessageBox.Show("Person Account status required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cbmStatus.Focus()
            Return False

            'ElseIf txtBeneficiary.Text.Trim() = "" Then
            '    MessageBox.Show("Beneficiary required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    txtBeneficiary.Focus()
            '    Return False


        End If


        Return True

    End Function

    Private Function SaveData() As TransState

        Dim tStatus As TransState


        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        If _formMode = FormTransMode.Add Then

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_Add")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ACNUMBER", DbType.String, txtAccNo.Text.Trim())
            db.AddInParameter(commProc, "@CURRENCY_CODE", DbType.String, cmbCurrency.SelectedValue)
            db.AddInParameter(commProc, "@IBAN", DbType.String, txtIban.Text.Trim())
            db.AddInParameter(commProc, "@CLIENT_NUMBER", DbType.String, txtClientNumber.Text.Trim())
            db.AddInParameter(commProc, "@ACCOUNT_TYPE", DbType.String, cmbAccountType.SelectedValue)
            db.AddInParameter(commProc, "@OPENED", DbType.DateTime, NullHelper.StringToDate(txtOpenDate.Text))
            db.AddInParameter(commProc, "@CLOSED", DbType.DateTime, NullHelper.StringToDate(txtCloseDate.Text))
            db.AddInParameter(commProc, "@STATUS_CODE", DbType.String, cbmStatus.SelectedValue)
            db.AddInParameter(commProc, "@BENEFICIARY", DbType.String, txtBeneficiary.Text.Trim())
            db.AddInParameter(commProc, "@BENEFICIARY_COMMENTS", DbType.String, txtBenefiComments.Text.Trim())
            db.AddInParameter(commProc, "@COMMENTS", DbType.String, txtComments.Text.Trim())
            db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, txtEntityId.Text.Trim())

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer


            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then
                tStatus = TransState.Add

                _strAccNO = txtAccNo.Text.Trim()

                _intModno = 1

                log_message = " Added : Account Number : " + txtAccNo.Text.ToString() + " " + "  For goAML Reporting "
                Logger.system_log(log_message)


            Else
                tStatus = TransState.Exist
            End If


            


        ElseIf _formMode = FormTransMode.Update Then



            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_Update")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ACNUMBER", DbType.String, txtAccNo.Text.Trim())
            db.AddInParameter(commProc, "@CURRENCY_CODE", DbType.String, cmbCurrency.SelectedValue)
            db.AddInParameter(commProc, "@IBAN", DbType.String, txtIban.Text.Trim())
            db.AddInParameter(commProc, "@CLIENT_NUMBER", DbType.String, txtClientNumber.Text.Trim())
            db.AddInParameter(commProc, "@ACCOUNT_TYPE", DbType.String, cmbAccountType.SelectedValue)
            db.AddInParameter(commProc, "@OPENED", DbType.DateTime, NullHelper.StringToDate(txtOpenDate.Text))
            db.AddInParameter(commProc, "@CLOSED", DbType.DateTime, NullHelper.StringToDate(txtCloseDate.Text))
            db.AddInParameter(commProc, "@STATUS_CODE", DbType.String, cbmStatus.SelectedValue)
            db.AddInParameter(commProc, "@BENEFICIARY", DbType.String, txtBeneficiary.Text.Trim())
            db.AddInParameter(commProc, "@BENEFICIARY_COMMENTS", DbType.String, txtBenefiComments.Text.Trim())
            db.AddInParameter(commProc, "@COMMENTS", DbType.String, txtComments.Text.Trim())
            db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, txtEntityId.Text.Trim())



            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)


            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)


            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then
                tStatus = TransState.Update
                _intModno = db.GetParameterValue(commProc, "@RET_MOD_NO")

                '----------------------Mizan Work (20-04-16)---------------------------

                If _Iban <> txtIban.Text.Trim() Then
                    If _Iban = "" Then
                        log_message = " Iban : " + txtIban.Text.ToString() + "." + " "

                    Else
                        log_message = " Iban : " + _Iban + " " + " To " + " " + txtIban.Text.ToString() + "." + " "

                    End If
                    GoAccList.Add(log_message)
                End If

                If _cmbCurrType <> cmbCurrency.Text Then
                    If _cmbCurrType = "" Then
                        log_message = " Currency : " + cmbCurrency.Text + "." + " "
                    Else
                        log_message = " Currency : " + _cmbCurrType + " " + " To " + " " + cmbCurrency.Text + "." + " "
                    End If

                    GoAccList.Add(log_message)
                End If

                If _cmbAccType <> cmbAccountType.Text Then
                    If _cmbAccType = "" Then
                        log_message = " Account Type : " + cmbAccountType.Text + "." + " "
                    Else
                        log_message = " Account Type : " + _cmbAccType + " " + " To " + " " + cmbAccountType.Text + "." + " "
                    End If

                    GoAccList.Add(log_message)
                End If

                If _cmbStatusType <> cbmStatus.Text Then
                    If _cmbStatusType = "" Then
                        log_message = " Status : " + cbmStatus.Text + "." + " "
                    Else
                        log_message = " Status : " + _cmbStatusType + " " + " To " + " " + cbmStatus.Text + "." + " "
                    End If

                    GoAccList.Add(log_message)
                End If
                If _beneficiary <> txtBeneficiary.Text.Trim() Then
                    If _beneficiary = "" Then
                        log_message = " Beneficiary : " + txtBeneficiary.Text.ToString() + "." + " "

                    Else
                        log_message = " Beneficiary : " + _beneficiary + " " + " To " + " " + txtBeneficiary.Text.ToString() + "." + " "

                    End If
                    GoAccList.Add(log_message)
                End If
                If _beneficiaryCommnts <> txtBenefiComments.Text.Trim() Then
                    If _beneficiaryCommnts = "" Then
                        log_message = " Beneficiary Comments : " + txtBenefiComments.Text.ToString() + "." + " "

                    Else
                        log_message = " Beneficiary Comments : " + _beneficiaryCommnts + " " + " To " + " " + txtBenefiComments.Text.ToString() + "." + " "

                    End If
                    GoAccList.Add(log_message)
                End If
                If _comments <> txtComments.Text.Trim() Then
                    If _comments = "" Then
                        log_message = " Comments : " + txtComments.Text.ToString() + "." + " "

                    Else
                        log_message = " Comments : " + _comments + " " + " To " + " " + txtComments.Text.ToString() + "." + " "

                    End If
                    GoAccList.Add(log_message)
                End If
                If _clientNo <> txtClientNumber.Text.Trim() Then
                    If _clientNo = "" Then
                        log_message = " Client Number : " + txtClientNumber.Text.ToString() + "." + " "

                    Else
                        log_message = " Client Number : " + _clientNo + " " + " To " + " " + txtClientNumber.Text.ToString() + "." + " "

                    End If
                    GoAccList.Add(log_message)
                End If

                For Each Accloglist As String In GoAccList
                    _goAccLog += Accloglist
                Next

                _goAccInfolog = " Updated : Account Number  : " + txtAccNo.Text.ToString() + "." + " " + _goAccLog

                Logger.system_log(_goAccInfolog)
                _goAccLog = ""
                GoAccList.Clear()

                '----------------------Mizan Work (20-04-16)---------------------------

            ElseIf result = 1 Then
                tStatus = TransState.UnspecifiedError
            ElseIf result = 4 Then
                tStatus = TransState.NoRecord

            End If
           

            'log_message = " Updated : Account Number  : " + txtAccNo.Text.ToString()
            'Logger.system_log(log_message)

        End If

        Return tStatus

    End Function

    '----------------------Mizan Work (20-04-16)---------------------------

    Private Function AuthorizeData() As TransState

        If _intModno > 1 Then

            LoadMainDataForAuth(_strAccNO)

            Dim tStatus As TransState

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ACNUMBER", DbType.String, _strAccNO)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                '----------------------Mizan Work (20-04-16)---------------------------

                If _IbanAcc <> _Iban Then
                    If _IbanAcc = "" Then
                        log_message = " Iban : " + _Iban + "." + " "

                    Else
                        log_message = " Iban : " + _IbanAcc + " " + " To " + " " + _Iban + "." + " "

                    End If
                    GoAccList.Add(log_message)
                End If

                If _cmboCurrType <> _cmbCurrType Then
                    If _cmboCurrType = "" Then
                        log_message = " Currency : " + _cmbCurrType + "." + " "
                    Else
                        log_message = " Currency : " + _cmboCurrType + " " + " To " + " " + _cmbCurrType + "." + " "
                    End If

                    GoAccList.Add(log_message)
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

                _goAccInfolog = " Authorized : Account Number  : " + txtAccNo.Text.ToString() + "." + " " + _goAccLog

                Logger.system_log(_goAccInfolog)
                _goAccLog = ""
                GoAccList.Clear()

                '----------------------Mizan Work (20-04-16)---------------------------

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

           

            'log_message = "Account Number " + _strAccNO + " Authorized"
            'Logger.system_log(log_message)

            Return tStatus

        Else

            Dim tStatus As TransState

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ACNUMBER", DbType.String, _strAccNO)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                '----------------------Mizan Work (20-04-16)---------------------------

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

                _goAccInfolog = " Authorized : Account Number  : " + txtAccNo.Text.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _goAccLog

                Logger.system_log(_goAccInfolog)
                _goAccLog = ""
                GoAccList.Clear()

                '----------------------Mizan Work (20-04-16)---------------------------

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

           

            'log_message = "Account Number " + _strAccNO + " Authorized"
            'Logger.system_log(log_message)

            Return tStatus

        End If

    End Function

    '----------------------Mizan Work (20-04-16)---------------------------

    Private Sub LoadMainDataForAuth(ByVal strAccNo As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From GO_ACCOUNT_INFO Where ACNUMBER='" & strAccNo & "' and STATUS ='L' ")

            If ds.Tables(0).Rows.Count > 0 Then

                _strAccNO = strAccNo


                _formMode = FormTransMode.Update

                txtAccNo.Text = ds.Tables(0).Rows(0)("ACNUMBER").ToString()

                txtIban.Text = ds.Tables(0).Rows(0)("IBAN").ToString()
                _IbanAcc = ds.Tables(0).Rows(0)("IBAN").ToString()
                txtClientNumber.Text = ds.Tables(0).Rows(0)("CLIENT_NUMBER").ToString()
                _clientNoAcc = ds.Tables(0).Rows(0)("CLIENT_NUMBER").ToString()


                ''cmbCurrency.SelectedValue = ds.Tables(0).Rows(0)("CURRENCY_CODE")
                _cmboCurrType = ds.Tables(0).Rows(0)("CURRENCY_CODE").ToString()
                ''cmbAccountType.SelectedValue = ds.Tables(0).Rows(0)("ACCOUNT_TYPE")
                _cmboAccType = ds.Tables(0).Rows(0)("ACCOUNT_TYPE").ToString()
                ''cbmStatus.SelectedValue = ds.Tables(0).Rows(0)("STATUS_CODE")
                _cmboStatusType = ds.Tables(0).Rows(0)("STATUS_CODE").ToString()

                txtBeneficiary.Text = ds.Tables(0).Rows(0)("BENEFICIARY").ToString()
                _beneficiaryAcc = ds.Tables(0).Rows(0)("BENEFICIARY").ToString()

                txtBenefiComments.Text = ds.Tables(0).Rows(0)("BENEFICIARY_COMMENTS").ToString()
                _beneficiaryCommntsAcc = ds.Tables(0).Rows(0)("BENEFICIARY_COMMENTS").ToString()

                txtComments.Text = ds.Tables(0).Rows(0)("COMMENTS").ToString()
                _commentsAcc = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                txtEntityId.Text = ds.Tables(0).Rows(0)("ENTITY_ID").ToString()

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


            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


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

                _strAccNO = strAccNo
                _intModno = intmod

                _formMode = FormTransMode.Update

                txtAccNo.Text = ds.Tables(0).Rows(0)("ACNUMBER").ToString()

                txtIban.Text = ds.Tables(0).Rows(0)("IBAN").ToString()
                _Iban = ds.Tables(0).Rows(0)("IBAN").ToString()
                txtClientNumber.Text = ds.Tables(0).Rows(0)("CLIENT_NUMBER").ToString()
                _clientNo = ds.Tables(0).Rows(0)("CLIENT_NUMBER").ToString()


                cmbCurrency.SelectedValue = ds.Tables(0).Rows(0)("CURRENCY_CODE")
                _cmbCurrType = ds.Tables(0).Rows(0)("CURRENCY_CODE").ToString()
                cmbAccountType.SelectedValue = ds.Tables(0).Rows(0)("ACCOUNT_TYPE")
                _cmbAccType = ds.Tables(0).Rows(0)("ACCOUNT_TYPE").ToString()
                cbmStatus.SelectedValue = ds.Tables(0).Rows(0)("STATUS_CODE")
                _cmbStatusType = ds.Tables(0).Rows(0)("STATUS_CODE").ToString()

                txtOpenDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("OPENED"))
                txtCloseDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("CLOSED"))

                txtBeneficiary.Text = ds.Tables(0).Rows(0)("BENEFICIARY").ToString()
                _beneficiary = ds.Tables(0).Rows(0)("BENEFICIARY").ToString()

                txtBenefiComments.Text = ds.Tables(0).Rows(0)("BENEFICIARY_COMMENTS").ToString()
                _beneficiaryCommnts = ds.Tables(0).Rows(0)("BENEFICIARY_COMMENTS").ToString()

                txtComments.Text = ds.Tables(0).Rows(0)("COMMENTS").ToString()
                _comments = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                txtEntityId.Text = ds.Tables(0).Rows(0)("ENTITY_ID").ToString()

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

                '_intModno = ds.Tables(0).Rows(0)("MOD_NO")


                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_GetMaxMod")

                commProc2.Parameters.Clear()

                db.AddInParameter(commProc2, "@ACNUMBER", DbType.String, strAccNo)

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


    Private Function DeleteData() As TransState

        Dim tStatus As TransState

        Dim intModno As Integer = 0

        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfo_Remove")

        commProc.Parameters.Clear()

        db.AddInParameter(commProc, "@ACNUMBER", DbType.String, _strAccNO)
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
        ElseIf result = 10 Then
            tStatus = TransState.ChildExist

        Else
            tStatus = TransState.UpdateNotPossible
        End If


        log_message = "Account Number " + _strAccNO + " Deleted"
        Logger.system_log(log_message)


        Return tStatus

    End Function



#End Region

    Private Sub FrmAccountInfoGoAMLDet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If



        'CommonUtil.FillComboBox("Select CURRENCY_CODE,CURRENCY_NAME From FIU_CURRENCY_INFO where STATUS='L'", cmbCurrency)
        CommonUtil.FillComboBox("GO_CurrencyType_GetList", cmbCurrency)

        CommonUtil.FillComboBox("GO_AccountType_GetList", cmbAccountType)
        CommonUtil.FillComboBox("GO_AccountStatusType_GetList", cbmStatus)





        If _intModno > 0 Then
            LoadMainData(_strAccNO, _intModno)
        End If

        If _strAccNO <> "" Then

            LoadAccountData(_strAccNO)

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
        lblToolStatus.Text = ""

        EnableNew()
        If Not (_strAccNO = "") Then

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
            Else
                DisableAuth()
            End If

        Else

            DisableFields()





        End If



        DisableUnlock()

        If btnNew.Enabled = True Then
            btnNew.Focus()
        End If

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        lblToolStatus.Text = ""
        _formMode = FormTransMode.Add

        EnableSave()

        ClearFieldsAll()
        EnableFields()


        DisableRefresh()
        DisableDelete()


        If txtAccNo.Enabled = True Then
            txtAccNo.Focus()
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
                        LoadMainData(_strAccNO, _intModno)

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

                        LoadMainData(_strAccNO, _intModno)

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
        LoadMainData(_strAccNO, _intModno)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try


            If MessageBox.Show("Do you really want to delete?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                tState = DeleteData()

                If tState = TransState.Delete Then


                    _formMode = FormTransMode.Add

                    LoadMainData(_strAccNO, _intModno)

                    DisableAuth()

                    If _strAccNO = "" Then

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

            LoadMainData(_strAccNO, _intModno - 1)

        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click
        Dim strAccNo As String = _strAccNO
        Dim intModno As Integer = _intModno
        If intModno > 0 Then
            LoadMainData(_strAccNO, _intModno + 1)

            If _intModno = 0 Then
                LoadMainData(strAccNo, intModno)
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
                    LoadMainData(_strAccNO, _intModno)
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

    Private Sub btnSearchEntity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEntity.Click
        Dim frmList As New FrmList()
        frmList.Text = "Entity List"
        frmList.ProcName = "GO_Entity_GetList"
        frmList.filter = New String(,) {{"NAME", "Entity Name"}, {"ENTITY_ID", "Entity Code"}}
        frmList.colwidth = New Integer(,) {{1, 300}}
        frmList.colrename = New String(,) {{"0", "Entity ID"}, {"1", "Entity Name"}}
        frmList.ShowDialog()

        If (frmList.RowResult.Cells.Count > 0) Then

            txtEntityId.Text = frmList.RowResult.Cells(0).Value.ToString()
            'lblDirectorName.Text = frmList.RowResult.Cells(1).Value.ToString()

            'txtDirectorId.Text = ""
            'lblDirectorName.Text = ""


            SendKeys.Send("{tab}")
        End If

        frmList.Dispose()
    End Sub


    Private Sub txtEntityId_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEntityId.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtEntityId.Text.Trim() = "" Then

                Dim frmList As New FrmList()
                frmList.Text = "Entity List"
                frmList.ProcName = "GO_Entity_GetList"
                frmList.filter = New String(,) {{"NAME", "Entity Name"}, {"ENTITY_ID", "Entity Code"}}
                frmList.colwidth = New Integer(,) {{1, 300}}
                frmList.colrename = New String(,) {{"0", "Entity ID"}, {"1", "Entity Name"}}
                frmList.ShowDialog()

                If (frmList.RowResult.Cells.Count > 0) Then

                    txtEntityId.Text = frmList.RowResult.Cells(0).Value.ToString()
                    'lblDirectorName.Text = frmList.RowResult.Cells(1).Value.ToString()

                    'txtDirectorId.Text = ""
                    'lblDirectorName.Text = ""


                    SendKeys.Send("{tab}")
                End If

                frmList.Dispose()

            Else


                Try

                    Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                    Dim dt As New DataTable

                    Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_GetListByCode")

                    commProc.Parameters.Clear()

                    db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, txtEntityId.Text.Trim())

                    dt = db.ExecuteDataSet(commProc).Tables(0)

                    If dt.Rows.Count > 0 Then
                        lblEntityName.Text = dt.Rows(0)("NAME").ToString()
                    Else
                        txtEntityId.Clear()
                        lblEntityName.Text = ""
                    End If


                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try


            End If

            If txtEntityId.Text.Trim() <> "" Then
                SendKeys.Send("{tab}")
                SendKeys.Send("{tab}")
            End If

        End If



    End Sub

    Private Sub txtEntityId_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntityId.Leave
        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim dt As New DataTable

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_GetListByCode")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, txtEntityId.Text.Trim())

            dt = db.ExecuteDataSet(commProc).Tables(0)

            If dt.Rows.Count > 0 Then
                lblEntityName.Text = dt.Rows(0)("NAME").ToString()
            Else
                txtEntityId.Clear()
                lblEntityName.Text = ""
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class