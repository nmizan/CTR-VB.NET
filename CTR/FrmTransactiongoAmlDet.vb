'
'Author             : Fahad Khan
'Purpose            : Maintain Transaction Information
'Creation date      : 02-nov-2013
'Stored Procedure(s):  

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmTransactiongoAmlDet

#Region "Global Variables"

    Dim _formName As String = "TransactionTransactiongoAmlDet"
    Dim opt As SecForm = New SecForm(_formName)

    Dim _formMode As FormTransMode
    Dim _intSlno As Integer = 0
    Dim _intModno As Integer = 0
    Dim _strTrans_Code As String = ""
    Dim _mod_datetime As Date
    Dim _status As String = ""
    Dim log_message As String = ""
    Dim IsFormChecked As String = ""
    Dim IsToChecked As String = ""

    Dim _interRef As String = ""
    Dim _transLoc As String = ""
    Dim _transDesc As String = ""
    Dim _transDate As String = ""
    Dim _teller As String = ""
    Dim _authorized As String = ""
    Dim _transType As String = ""
    Dim _valueDate As String = ""
    Dim _amount As String = ""
    Dim _balance As String = ""
    Dim _transComments As String = ""
    Dim _foreignAmount As String = ""
    Dim _currType As String = ""
    Dim _comments As String = ""

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
        txtAccountLocal.ReadOnly = True
        txtAuthorized.ReadOnly = True
        txtBalance.ReadOnly = True
        txtComments.ReadOnly = True
        txtInternalRef.ReadOnly = True
        txtLocation.ReadOnly = True
        txtTeller.ReadOnly = True
        txtTransComments.ReadOnly = True
        txtTransDate.ReadOnly = True
        txtValueDate.ReadOnly = True
        txtDescription.ReadOnly = True
        txtForeignAmount.ReadOnly = True
        cmbConduction.Enabled = False
        cmbCurrencyType.Enabled = False
        rndFromAccount.Enabled = False
        rndFromPerson.Enabled = False
        rndToAccount.Enabled = False
        rndToPerson.Enabled = False
        btnFromAccount.Enabled = False
        btnFromPerson.Enabled = False
        btnToAccount.Enabled = False
        btnToPerson.Enabled = False


    End Sub

    Private Sub EnableFields()
        If txtId.Text.Trim() = "" Then
            txtId.ReadOnly = False
        End If

        txtAccountLocal.ReadOnly = True
        txtAuthorized.ReadOnly = True
        txtBalance.ReadOnly = False
        txtComments.ReadOnly = False
        txtInternalRef.ReadOnly = False
        txtLocation.ReadOnly = False
        txtTeller.ReadOnly = True
        txtTransComments.ReadOnly = False
        txtTransDate.ReadOnly = True
        txtValueDate.ReadOnly = False
        txtDescription.ReadOnly = False
        txtForeignAmount.ReadOnly = False
        cmbConduction.Enabled = True
        cmbCurrencyType.Enabled = True



        If IsFormChecked = "A" Then

            rndFromAccount.Enabled = True

            btnFromAccount.Enabled = True
            'rndFromPerson.Enabled = True
            'btnFromPerson.Enabled = True


        Else

            rndFromPerson.Enabled = True

            btnFromPerson.Enabled = True

            btnFromAccount.Enabled = False
            rndFromAccount.Enabled = False

        End If



        If IsToChecked = "A" Then

            rndToAccount.Enabled = True


            btnToAccount.Enabled = True
            'rndToPerson.Enabled = True
            ' btnToPerson.Enabled = True


        Else

            rndToPerson.Enabled = True

            btnToPerson.Enabled = True
            btnToAccount.Enabled = False
            rndToAccount.Enabled = False


        End If




    End Sub


    Private Sub ClearFields()
        If txtId.ReadOnly = False Then
            txtId.Clear()
        End If

        txtAccountLocal.Clear()
        txtAuthorized.Clear()
        txtBalance.Clear()
        txtComments.Clear()
        txtInternalRef.Clear()
        txtLocation.Clear()
        txtTeller.Clear()
        txtTransComments.Clear()
        txtTransDate.Clear()
        txtValueDate.Clear()
        txtDescription.Clear()
        txtForeignAmount.Clear()

        'cmbConduction.SelectedValue = -1

    End Sub

    Private Sub ClearFieldsAll()
        txtId.Clear()
        txtAccountLocal.Clear()
        txtAuthorized.Clear()
        txtBalance.Clear()
        txtComments.Clear()
        txtInternalRef.Clear()
        txtLocation.Clear()
        txtTeller.Clear()
        txtTransComments.Clear()
        txtTransDate.Clear()
        txtValueDate.Clear()
        txtDescription.Clear()
        txtForeignAmount.Clear()
        'cmbConduction.SelectedValue = -1


        _strTrans_Code = ""
        _intModno = 0

        lblFrom.Text = ""
        lblTo.Text = ""


        lblVerNo.Text = ""
        lblVerTot.Text = ""

        lblInputBy.Text = ""
        lblInputDate.Text = ""
        lblAuthBy.Text = ""
        lblAuthDate.Text = ""
        lblModNo.Text = ""

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal strTransCode As String, ByVal intMod As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadTransData(strTransCode, intMod)
        _strTrans_Code = strTransCode
        _intModno = intMod


    End Sub



    Private Function CheckValidData() As Boolean

        If txtId.Text.Trim() = "" Then
            MessageBox.Show("Transaction Id required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtId.Focus()
            Return False
        ElseIf txtLocation.Text.Trim() = "" Then
            MessageBox.Show("Location required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtLocation.Focus()
            Return False
        ElseIf txtTransDate.Text.Trim() = "" Then
            MessageBox.Show("Transaction Date required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtTransDate.Focus()
            Return False
        ElseIf cmbConduction.Text.Trim() = "" Then
            MessageBox.Show("Transaction Type required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbConduction.Focus()
            Return False
        ElseIf txtAccountLocal.Text.Trim() = "" Then
            MessageBox.Show("Transaction Local Amount required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAccountLocal.Focus()
            Return False
      
        End If


        Return True

    End Function

   


    Private Sub LoadTransData(ByVal strTransCode As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Transaction_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@TRANSACTIONNUMBER", DbType.String, strTransCode)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intMod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then


                _strTrans_Code = strTransCode
                _intModno = intMod

                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("TRANSACTIONNUMBER").ToString()
                txtInternalRef.Text = ds.Tables(0).Rows(0)("INTERNAL_REF_NUMBER").ToString()
                _interRef = ds.Tables(0).Rows(0)("INTERNAL_REF_NUMBER").ToString()
                txtLocation.Text = ds.Tables(0).Rows(0)("TRANSACTION_LOCATION").ToString()
                _transLoc = ds.Tables(0).Rows(0)("TRANSACTION_LOCATION").ToString()
                txtComments.Text = ds.Tables(0).Rows(0)("COMMENTS").ToString()
                _comments = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                txtDescription.Text = ds.Tables(0).Rows(0)("TRANSACTION_DESCRIPTION").ToString()
                _transDesc = ds.Tables(0).Rows(0)("TRANSACTION_DESCRIPTION").ToString()
                txtTransDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("DATE_TRANSACTION"))
                _transDate = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("DATE_TRANSACTION")).ToString()
                txtValueDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("VALUE_DATE"))
                _valueDate = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("VALUE_DATE")).ToString()

                cmbConduction.SelectedValue = ds.Tables(0).Rows(0)("TRANSMODE_CODE")
                _transType = ds.Tables(0).Rows(0)("TRANSMODE_CODE").ToString()


                txtTransComments.Text = ds.Tables(0).Rows(0)("TRANSMODE_COMMENTS").ToString()
                _transComments = ds.Tables(0).Rows(0)("TRANSMODE_COMMENTS").ToString()

                txtAccountLocal.Text = ds.Tables(0).Rows(0)("AMOUNT_LOCAL").ToString()
                _amount = ds.Tables(0).Rows(0)("AMOUNT_LOCAL").ToString()
                txtBalance.Text = ds.Tables(0).Rows(0)("BALANCE").ToString()
                _balance = ds.Tables(0).Rows(0)("BALANCE").ToString()

                txtTeller.Text = ds.Tables(0).Rows(0)("TELLER").ToString()
                _teller = ds.Tables(0).Rows(0)("TELLER").ToString()
                txtAuthorized.Text = ds.Tables(0).Rows(0)("AUTHORIZED").ToString()
                _authorized = ds.Tables(0).Rows(0)("AUTHORIZED").ToString()

                IsFormChecked = ds.Tables(0).Rows(0)("FROM_FLAG").ToString()


                If IsFormChecked = "A" Then

                    rndFromAccount.Checked = True
                    lblFrom.Text = ds.Tables(0).Rows(0)("FROM_ACCOUNT").ToString()



                Else
                    rndFromPerson.Checked = True
                    lblFrom.Text = ds.Tables(0).Rows(0)("FROM_PERSON").ToString()


                End If

                IsToChecked = ds.Tables(0).Rows(0)("TO_FLAG").ToString()



                If IsToChecked = "A" Then

                    rndToAccount.Checked = True
                    lblTo.Text = ds.Tables(0).Rows(0)("TO_ACCOUNT").ToString()


                Else
                    rndToPerson.Checked = True
                    lblTo.Text = ds.Tables(0).Rows(0)("TO_PERSON").ToString()


                End If

                cmbCurrencyType.SelectedValue = ds.Tables(0).Rows(0)("ACCOUNT_CURRENCY")
                txtForeignAmount.Text = ds.Tables(0).Rows(0)("FOREIGN_AMOUNT").ToString()


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

                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_Transaction_GetMaxMod")

                commProc2.Parameters.Clear()

                db.AddInParameter(commProc2, "@TRANSACTIONNUMBER", DbType.String, strTransCode)

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


    Private Function SaveData() As TransState

        'Dim tStatus As TransState


        'tStatus = TransState.UnspecifiedError

        'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        'If _formMode = FormTransMode.Update Then

        '    Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Transaction_Update")

        '    commProc.Parameters.Clear()

        '    db.AddInParameter(commProc, "@TRANSACTIONNUMBER", DbType.String, txtId.Text.Trim())
        '    db.AddInParameter(commProc, "@INTERNAL_REF_NUMBER", DbType.String, txtInternalRef.Text.Trim())
        '    db.AddInParameter(commProc, "@TRANSACTION_LOCATION", DbType.String, txtLocation.Text.Trim())
        '    db.AddInParameter(commProc, "@TRANSACTION_DESCRIPTION", DbType.String, txtDescription.Text.Trim())
        '    db.AddInParameter(commProc, "@DATE_TRANSACTION", DbType.DateTime, NullHelper.StringToDate(txtTransDate.Text.Trim()))
        '    db.AddInParameter(commProc, "@TELLER", DbType.String, txtTeller.Text.Trim())
        '    db.AddInParameter(commProc, "@AUTHORIZED", DbType.String, txtAuthorized.Text.Trim())
        '    db.AddInParameter(commProc, "@VALUE_DATE", DbType.DateTime, NullHelper.StringToDate(txtValueDate.Text.Trim()))
        '    db.AddInParameter(commProc, "@TRANSMODE_CODE", DbType.String, cmbConduction.SelectedValue)
        '    db.AddInParameter(commProc, "@TRANSMODE_COMMENTS", DbType.String, txtTransComments.Text.Trim())
        '    db.AddInParameter(commProc, "@FROM_FLAG", DbType.String, IsFormChecked)
        '    db.AddInParameter(commProc, "@TO_FLAG", DbType.String, IsToChecked)

        '    db.AddInParameter(commProc, "@FROM_ACCOUNT", DbType.String, lblFrom.Text)
        '    db.AddInParameter(commProc, "@TO_ACCOUNT", DbType.String, lblTo.Text)

        '    db.AddInParameter(commProc, "@AMOUNT_LOCAL", DbType.String, txtAccountLocal.Text.Trim())
        '    db.AddInParameter(commProc, "@BALANCE", DbType.String, txtBalance.Text.Trim())

        '    db.AddInParameter(commProc, "@COMMENTS", DbType.String, txtComments.Text.Trim())

        '    db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
        '    db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)


        '    db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)


        '    Dim result As Integer

        '    db.ExecuteNonQuery(commProc)
        '    result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
        '    If result = 0 Then
        '        tStatus = TransState.Update
        '        _intModno = db.GetParameterValue(commProc, "@RET_MOD_NO")
        '    ElseIf result = 1 Then
        '        tStatus = TransState.UnspecifiedError
        '    ElseIf result = 4 Then
        '        tStatus = TransState.NoRecord

        '    End If


        'End If

        'Return tStatus

    End Function

    Private Function AuthorizeData() As TransState

        'Dim tStatus As TransState


        'tStatus = TransState.UnspecifiedError

        'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        'Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Transaction_Auth")

        'commProc.Parameters.Clear()

        'db.AddInParameter(commProc, "@TRANSACTIONNUMBER", DbType.String, _strTrans_Code)
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



        'Return tStatus

    End Function

    Private Function DeleteData() As TransState

        'Dim tStatus As TransState

        'Dim intModno As Integer = 0

        'tStatus = TransState.UnspecifiedError

        'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        'Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Transaction_Remove")

        'commProc.Parameters.Clear()

        'db.AddInParameter(commProc, "@TRANSACTIONNUMBER", DbType.String, _strTrans_Code)
        'db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
        'db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)

        'db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

        'Dim result As Integer

        'db.ExecuteNonQuery(commProc)
        'result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
        'If result = 0 Then

        '    tStatus = TransState.Delete
        '    _intModno = db.GetParameterValue(commProc, "@RET_MOD_NO")

        'ElseIf result = 1 Then

        '    tStatus = TransState.UpdateNotPossible

        'ElseIf result = 3 Then
        '    tStatus = TransState.UpdateNotPossible

        'ElseIf result = 4 Then
        '    tStatus = TransState.NoRecord

        'ElseIf result = 5 Then
        '    tStatus = TransState.UpdateNotPossible
        'ElseIf result = 6 Then
        '    tStatus = TransState.AlreadyDeleted

        'Else
        '    tStatus = TransState.UpdateNotPossible
        'End If


        'Return tStatus

    End Function



#End Region


    Private Sub FrmTransactiongoAmlDet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If


        CommonUtil.FillComboBox("GO_ConductionType_GetList", cmbConduction)

        CommonUtil.FillComboBox("Select CURRENCY_CODE,CURRENCY_NAME From FIU_CURRENCY_INFO where STATUS='L'", cmbCurrencyType)

        If _intModno > 0 Then
            LoadTransData(_strTrans_Code, _intModno)
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



        If Not (_strTrans_Code.Trim() = "") Then

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

                        LoadTransData(_strTrans_Code, _intModno)

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

                        LoadTransData(_strTrans_Code, _intModno)

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
        LoadTransData(_strTrans_Code, _intModno)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try


            If MessageBox.Show("Do you really want to delete?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                tState = DeleteData()

                If tState = TransState.Delete Then


                    _formMode = FormTransMode.Add

                    LoadTransData(_strTrans_Code, _intModno)

                    DisableAuth()

                    If _strTrans_Code = "" Then

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

            LoadTransData(_strTrans_Code, _intModno - 1)

        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click
        Dim strTransCode As String = _strTrans_Code
        Dim intModno As Integer = _intModno
        If intModno > 0 Then
            LoadTransData(_strTrans_Code, _intModno + 1)

            If _intModno = 0 Then
                LoadTransData(strTransCode, intModno)
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
                    LoadTransData(_strTrans_Code, _intModno)
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

   
    Private Sub btnFromAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFromAccount.Click

        Dim strAcNo As String = lblFrom.Text

        If strAcNo <> "" Then

            Dim frmAccountInfo As New FrmAccountInfo(strAcNo)
            frmAccountInfo.ShowDialog()
        End If
    End Sub

    Private Sub btnFromPerson_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFromPerson.Click
        Dim strRefNo As String = lblFrom.Text

        If strRefNo <> "" Then

            Dim FrmBearerDetail As New FrmBearerDetail(strRefNo)
            FrmBearerDetail.ShowDialog()
        End If
    End Sub


    Private Sub btnToAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToAccount.Click
        Dim strAcNo As String = lblTo.Text

        If strAcNo <> "" Then

            Dim frmAccountInfo As New FrmAccountInfo(strAcNo)
            frmAccountInfo.ShowDialog()
        End If
    End Sub

    Private Sub btnToPerson_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToPerson.Click

        Dim strRefNo As String = lblTo.Text

        If strRefNo <> "" Then

            Dim FrmBearerDetail As New FrmBearerDetail(strRefNo)
            FrmBearerDetail.ShowDialog()
        End If
    End Sub


End Class