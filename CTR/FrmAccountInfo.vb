Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization

Public Class FrmAccountInfo

#Region "Global Variables"

    Dim _formName As String = "MaintenanceAccountInfoDetail"
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
    Dim _status As String = ""
    Dim _strTransCode As String = ""

    '' For Update
    Dim _AcTitle As String = ""
    Dim _branch As String = ""
    Dim _AcDepAmt As String = ""
    Dim _AcDepTrNo As String = ""
    Dim _DepMaxAmt As String = ""
    Dim _WithAmt As String = ""
    Dim _WithTrNo As String = ""
    Dim _WithMaxAmt As String = ""
    Dim _AcTin As String = ""
    Dim _AcBin As String = ""
    Dim _VatReg As String = ""
    Dim _PreAdd As String = ""
    Dim _ComRegi As String = ""
    Dim _PerAdd As String = ""
    Dim _AccNo As String = ""
    Dim _PhoneRes1 As String = ""
    Dim _PhoneRes2 As String = ""
    Dim _Mobile1 As String = ""
    Dim _Mobile2 As String = ""
    Dim _OwType As String = ""
    Dim _OwTypeName As String = ""
    Dim _AccType As String = ""
    Dim _AccTypeName As String = ""

    ''----For Authorize
    Dim _Title As String = ""
    Dim _Acbranch As String = ""
    Dim _DepAmt As String = ""
    Dim _DepTrNo As String = ""
    Dim _DeptMaxAmt As String = ""
    Dim _WithdrawAmt As String = ""
    Dim _WithdrawTrNo As String = ""
    Dim _WithdrawMaxAmt As String = ""
    Dim _Tin As String = ""
    Dim _Bin As String = ""
    Dim _VatRegi As String = ""
    Dim _PreAddrs As String = ""
    Dim _ComReg As String = ""
    Dim _PerAddrs As String = ""
    Dim _AccNumber As String = ""
    Dim _Phone1 As String = ""
    Dim _Phone2 As String = ""
    Dim _MobileOne As String = ""
    Dim _MobileTwo As String = ""
    Dim _OwnerType As String = ""
    Dim _OwnerTypeName As String = ""
    Dim _AccountType As String = ""
    Dim _AccountTypeName As String = ""

    Dim AccList As New List(Of String)
    Dim _accLog As String = ""
    Dim _log As String = ""

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

        cmbBank.Enabled = False
        cmbBranch.Enabled = False
        txtAccNo.ReadOnly = True
        txtAccNoOld.ReadOnly = True
        txtAccTitle.ReadOnly = True
        cmbAccType.Enabled = False
        cmbOwType.Enabled = False
        txtDepositAmt.ReadOnly = True
        txtDepositTransNo.ReadOnly = True
        txtDepositMaxAmt.ReadOnly = True
        txtWithdrawAmt.ReadOnly = True
        txtWithdrawTransNo.ReadOnly = True
        txtWithdrawMaxAmt.ReadOnly = True
        txtTIN.ReadOnly = True
        txtBIN.ReadOnly = True
        txtVatRegNo.ReadOnly = True
        txtVatRegDt.ReadOnly = True
        txtCompanyRegNo.ReadOnly = True
        txtCompanyRegDt.ReadOnly = True
        cmbRegAuthority.Enabled = False
        txtPrsAddrs.ReadOnly = True
        txtPerAddrs.ReadOnly = True
        txtPhoneRes1.ReadOnly = True
        txtPhoneRes2.ReadOnly = True
        txtPhoneOff1.ReadOnly = True
        txtPhoneOff2.ReadOnly = True
        txtMobile1.ReadOnly = True
        txtMobile2.ReadOnly = True
        txtInsertedOn.ReadOnly = True
        txtModifiedOn.ReadOnly = True
    End Sub

    Private Sub EnableFields()
        'txtId.ReadOnly = False
        'txtName.ReadOnly = False

        If _intModno = 0 Then
            cmbBank.Enabled = True
            cmbBranch.Enabled = True
            txtAccNo.ReadOnly = False
        End If



        txtAccNoOld.ReadOnly = False
        txtAccTitle.ReadOnly = False
        cmbAccType.Enabled = True
        cmbOwType.Enabled = True

        txtDepositAmt.ReadOnly = False
        txtDepositTransNo.ReadOnly = False
        txtDepositMaxAmt.ReadOnly = False
        txtWithdrawAmt.ReadOnly = False
        txtWithdrawTransNo.ReadOnly = False
        txtWithdrawMaxAmt.ReadOnly = False
        txtTIN.ReadOnly = False
        txtBIN.ReadOnly = False
        txtVatRegNo.ReadOnly = False
        txtVatRegDt.ReadOnly = False
        txtCompanyRegNo.ReadOnly = False
        txtCompanyRegDt.ReadOnly = False
        cmbRegAuthority.Enabled = True
        txtPrsAddrs.ReadOnly = False
        txtPerAddrs.ReadOnly = False
        txtPhoneRes1.ReadOnly = False
        txtPhoneRes2.ReadOnly = False
        txtPhoneOff1.ReadOnly = False
        txtPhoneOff2.ReadOnly = False
        txtMobile1.ReadOnly = False
        txtMobile2.ReadOnly = False
        txtInsertedOn.ReadOnly = False
        txtModifiedOn.ReadOnly = False


    End Sub

    Private Sub ClearFields()
        'txtId.Clear()
        'txtName.Clear()

        txtAccNo.Clear()
        txtAccNoOld.Clear()
        txtAccTitle.Clear()
        txtDepositAmt.Clear()
        txtDepositTransNo.Clear()
        txtDepositMaxAmt.Clear()
        txtWithdrawAmt.Clear()
        txtWithdrawTransNo.Clear()
        txtDepositMaxAmt.Clear()
        txtTIN.Clear()
        txtBIN.Clear()
        txtVatRegNo.Clear()
        txtVatRegDt.Clear()
        txtCompanyRegNo.Clear()
        txtCompanyRegDt.Clear()
        txtPrsAddrs.Clear()
        txtPerAddrs.Clear()
        txtPhoneRes1.Clear()
        txtPhoneRes2.Clear()
        txtPhoneOff1.Clear()
        txtPhoneOff2.Clear()
        txtMobile1.Clear()
        txtMobile2.Clear()

    End Sub

    Private Sub ClearFieldsAll()

        txtAccNo.Clear()
        txtAccNoOld.Clear()
        txtAccTitle.Clear()
        txtDepositAmt.Clear()
        txtDepositTransNo.Clear()
        txtDepositMaxAmt.Clear()
        txtWithdrawAmt.Clear()
        txtWithdrawTransNo.Clear()
        txtDepositMaxAmt.Clear()
        txtTIN.Clear()
        txtBIN.Clear()
        txtVatRegNo.Clear()
        txtVatRegDt.Clear()
        txtCompanyRegNo.Clear()
        txtCompanyRegDt.Clear()
        txtPrsAddrs.Clear()
        txtPerAddrs.Clear()
        txtPhoneRes1.Clear()
        txtPhoneRes2.Clear()
        txtPhoneOff1.Clear()
        txtPhoneOff2.Clear()
        txtMobile1.Clear()
        txtMobile2.Clear()
        txtWithdrawMaxAmt.Clear()
        txtInsertedOn.Clear()


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
        If cmbBank.Text = "" Then
            MessageBox.Show("Bank required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbBank.Focus()
            Return False
        ElseIf cmbBranch.Text = "" Then
            MessageBox.Show("Branch required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbBranch.Focus()
            Return False
        ElseIf txtAccNo.Text.Trim() = "" Then
            MessageBox.Show("A/C No required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAccNo.Focus()
            Return False
        ElseIf txtAccTitle.Text.Trim() = "" Then
            MessageBox.Show("A/C Title required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAccTitle.Focus()
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

            'intSlno = db.ExecuteDataSet(CommandType.Text, "select ISNULL(max(SLNO),0)+1 maxslno from APPS").Tables(0).Rows(0)(0)

            'strSql = "insert into APPS(SLNO, APP_ID, APP_NAME, MODNO, " + _
            '    " INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  " + _
            '    " STATUS) values(@P_Slno, @P_App_Id, @P_App_Name, " + _
            '    " 1, @P_Input_By, getdate(), 0, 'U')"

            strSql = "Insert Into FIU_ACCOUNT_INFO(Bank_Code,Branch_Code," & _
                     "AcNumber,Ac_Title,AcTypeCode,OwTypeCode," & _
                     "Declared_Deposit_Amount,Declared_Deposit_TransNo," & _
                     "Declared_Deposit_MaxAmount,Declared_Withdr_Amount," & _
                     "Declared_Withdr_TransNo,Declared_Withdr_MaxAmount," & _
                     "TIN,BIN,Vat_Reg_No,Vat_Reg_Date,Company_Reg_No,Company_Reg_Date," & _
                     "Reg_Authority_Code,Pres_Addr,Perm_Addr,Phone_Res1,Phone_Res2," & _
                     "Phone_Office1,Phone_Office2,Mobile1,Mobile2,Old_AcNumber," & _
                     "Inserted_On,Modified_On,ModNo,Input_By,Input_Datetime,Is_Authorized,Status)" & _
                     "Values(@P_Bank_Code,@P_Branch_Code,@P_AcNumber,@P_Ac_Title," & _
                     "@P_AcTypeCode,@P_OwTypeCode,@P_DipositAmt,@P_DipositTransNo," & _
                     "@P_DipositMaxAmt,@P_WithdrawAmt,@P_WithdrawTransNo,@P_WithdrawMaxAmt," & _
                     "@P_TIN,@P_BIN,@P_VatRegNo,@P_VatRegDt,@P_CompanyRegNo,@P_CompanyRegDt," & _
                     "@P_RegAuthority,@P_PresAddr,@P_PermAddr,@P_PhoneRes1,@P_PhoneRes2," & _
                     "@P_PhoneOff1,@P_PhoneOff2,@P_Mobile1,@P_Mobile2,@P_AccNoOld," & _
                     "@P_Inserted_On,@P_Modified_On,1,@P_Input_By,getdate(),0,'U')"

            Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

            commProc.Parameters.Clear()
            db.AddInParameter(commProc, "@P_Bank_Code", DbType.String, cmbBank.SelectedValue)
            db.AddInParameter(commProc, "@P_Branch_Code", DbType.String, cmbBranch.SelectedValue)
            db.AddInParameter(commProc, "@P_AcNumber", DbType.String, txtAccNo.Text.Trim())
            db.AddInParameter(commProc, "@P_Ac_Title", DbType.String, txtAccTitle.Text.Trim())
            db.AddInParameter(commProc, "@P_AcTypeCode", DbType.String, cmbAccType.SelectedValue)
            db.AddInParameter(commProc, "@P_OwTypeCode", DbType.String, cmbOwType.SelectedValue)

            If txtDepositAmt.Text <> "" Then
                db.AddInParameter(commProc, "@P_DipositAmt", DbType.Decimal, txtDepositAmt.Text.Trim())
            Else
                db.AddInParameter(commProc, "@P_DipositAmt", DbType.Decimal, DBNull.Value)
            End If

            If txtDepositTransNo.Text <> "" Then
                db.AddInParameter(commProc, "@P_DipositTransNo", DbType.Int32, txtDepositTransNo.Text.Trim())
            Else
                db.AddInParameter(commProc, "@P_DipositTransNo", DbType.Int32, DBNull.Value)
            End If

            If txtDepositMaxAmt.Text <> "" Then
                db.AddInParameter(commProc, "@P_DipositMaxAmt", DbType.Decimal, txtDepositMaxAmt.Text.Trim())
            Else
                db.AddInParameter(commProc, "@P_DipositMaxAmt", DbType.Decimal, DBNull.Value)
            End If

            If txtWithdrawAmt.Text <> "" Then
                db.AddInParameter(commProc, "@P_WithdrawAmt", DbType.Decimal, txtWithdrawAmt.Text.Trim())
            Else
                db.AddInParameter(commProc, "@P_WithdrawAmt", DbType.Decimal, DBNull.Value)
            End If

            If txtWithdrawTransNo.Text <> "" Then
                db.AddInParameter(commProc, "@P_WithdrawTransNo", DbType.Int32, txtWithdrawTransNo.Text.Trim())
            Else
                db.AddInParameter(commProc, "@P_WithdrawTransNo", DbType.Int32, DBNull.Value)
            End If

            If txtWithdrawMaxAmt.Text <> "" Then
                db.AddInParameter(commProc, "@P_WithdrawMaxAmt", DbType.Decimal, txtWithdrawMaxAmt.Text.Trim())
            Else
                db.AddInParameter(commProc, "@P_WithdrawMaxAmt", DbType.Decimal, DBNull.Value)
            End If

            db.AddInParameter(commProc, "@P_TIN", DbType.String, txtTIN.Text.Trim())
            db.AddInParameter(commProc, "@P_BIN", DbType.String, txtBIN.Text.Trim())
            db.AddInParameter(commProc, "@P_VatRegNo", DbType.String, txtVatRegNo.Text.Trim())

            If txtVatRegDt.Text <> "" Then
                db.AddInParameter(commProc, "@P_VatRegDt", DbType.DateTime, DateTime.ParseExact(txtVatRegDt.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
            Else
                db.AddInParameter(commProc, "@P_VatRegDt", DbType.DateTime, DBNull.Value)
            End If
            db.AddInParameter(commProc, "@P_CompanyRegNo", DbType.String, txtCompanyRegNo.Text.Trim())

            If txtCompanyRegDt.Text <> "" Then
                db.AddInParameter(commProc, "@P_CompanyRegDt", DbType.DateTime, DateTime.ParseExact(txtCompanyRegDt.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
            Else
                db.AddInParameter(commProc, "@P_CompanyRegDt", DbType.DateTime, DBNull.Value)
            End If

            db.AddInParameter(commProc, "@P_RegAuthority", DbType.String, cmbRegAuthority.SelectedValue)
            db.AddInParameter(commProc, "@P_PresAddr", DbType.String, txtPrsAddrs.Text.Trim())
            db.AddInParameter(commProc, "@P_PermAddr", DbType.String, txtPerAddrs.Text.Trim())
            db.AddInParameter(commProc, "@P_PhoneRes1", DbType.String, txtPhoneRes1.Text.Trim())
            db.AddInParameter(commProc, "@P_PhoneRes2", DbType.String, txtPhoneRes2.Text.Trim())
            db.AddInParameter(commProc, "@P_PhoneOff1", DbType.String, txtPhoneOff1.Text.Trim())
            db.AddInParameter(commProc, "@P_PhoneOff2", DbType.String, txtPhoneOff2.Text.Trim())
            db.AddInParameter(commProc, "@P_Mobile1", DbType.String, txtMobile1.Text.Trim())
            db.AddInParameter(commProc, "@P_Mobile2", DbType.String, txtMobile2.Text.Trim())
            db.AddInParameter(commProc, "@P_AccNoOld", DbType.String, txtAccNoOld.Text.Trim())





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

            'db.AddInParameter(commProc, "@P_Slno", DbType.Int16, intSlno)
            'db.AddInParameter(commProc, "@P_App_Id", DbType.String, txtId.Text.Trim())
            'db.AddInParameter(commProc, "@P_App_Name", DbType.String, txtName.Text.Trim())


            Dim result As Integer

            result = db.ExecuteNonQuery(commProc)

            If result < 0 Then  '  Don't Understand
                tStatus = TransState.Exist
            Else
                tStatus = TransState.Add
                '_intSlno = intSlno
                _strBank_Code = cmbBank.SelectedValue
                _strBranch_Code = cmbBranch.SelectedValue
                _strAcNumber = txtAccNo.Text.Trim()
                _intModno = 1

                log_message = " Added : New Account Number : " + txtAccNo.Text.Trim() + "." + " " + " Account Title : " + txtAccTitle.Text.ToString()
                Logger.system_log(log_message)

            End If


        ElseIf _formMode = FormTransMode.Update Then


            Using conn As DbConnection = db.CreateConnection()


                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                db.ExecuteNonQuery(trans, CommandType.Text, "delete FIU_ACCOUNT_INFO where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and IS_AUTHORIZED=0")
                Dim ds As New DataSet


                strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_ACCOUNT_INFO where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "'"


                intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)

                strSql = "Insert Into FIU_ACCOUNT_INFO(Bank_Code,Branch_Code," & _
                     "AcNumber,Ac_Title,AcTypeCode,OwTypeCode," & _
                     "Declared_Deposit_Amount,Declared_Deposit_TransNo," & _
                     "Declared_Deposit_MaxAmount,Declared_Withdr_Amount," & _
                     "Declared_Withdr_TransNo,Declared_Withdr_MaxAmount," & _
                     "TIN,BIN,Vat_Reg_No,Vat_Reg_Date,Company_Reg_No,Company_Reg_Date," & _
                     "Reg_Authority_Code,Pres_Addr,Perm_Addr,Phone_Res1,Phone_Res2," & _
                     "Phone_Office1,Phone_Office2,Mobile1,Mobile2,Old_AcNumber," & _
                     "Inserted_On,Modified_On,ModNo,Input_By,Input_Datetime,Is_Authorized,Status)" & _
                     "Values(@P_Bank_Code,@P_Branch_Code,@P_AcNumber,@P_Ac_Title," & _
                     "@P_AcTypeCode,@P_OwTypeCode,@P_DipositAmt,@P_DipositTransNo," & _
                     "@P_DipositMaxAmt,@P_WithdrawAmt,@P_WithdrawTransNo,@P_WithdrawMaxAmt," & _
                     "@P_TIN,@P_BIN,@P_VatRegNo,@P_VatRegDt,@P_CompanyRegNo,@P_CompanyRegDt," & _
                     "@P_RegAuthority,@P_PresAddr,@P_PermAddr,@P_PhoneRes1,@P_PhoneRes2," & _
                     "@P_PhoneOff1,@P_PhoneOff2,@P_Mobile1,@P_Mobile2,@P_AccNoOld," & _
                     "@P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'U')"


                Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_Bank_Code", DbType.String, _strBank_Code)
                db.AddInParameter(commProc, "@P_Branch_Code", DbType.String, _strBranch_Code)
                db.AddInParameter(commProc, "@P_AcNumber", DbType.String, _strAcNumber)
                db.AddInParameter(commProc, "@P_Ac_Title", DbType.String, txtAccTitle.Text.Trim())
                db.AddInParameter(commProc, "@P_AcTypeCode", DbType.String, cmbAccType.SelectedValue)
                db.AddInParameter(commProc, "@P_OwTypeCode", DbType.String, cmbOwType.SelectedValue)

                If txtDepositAmt.Text <> "" Then
                    db.AddInParameter(commProc, "@P_DipositAmt", DbType.Decimal, txtDepositAmt.Text.Trim())
                Else
                    db.AddInParameter(commProc, "@P_DipositAmt", DbType.Decimal, DBNull.Value)
                End If

                If txtDepositTransNo.Text <> "" Then
                    db.AddInParameter(commProc, "@P_DipositTransNo", DbType.Int32, txtDepositTransNo.Text.Trim())
                Else
                    db.AddInParameter(commProc, "@P_DipositTransNo", DbType.Int32, DBNull.Value)
                End If

                If txtDepositMaxAmt.Text <> "" Then
                    db.AddInParameter(commProc, "@P_DipositMaxAmt", DbType.Decimal, txtDepositMaxAmt.Text.Trim())
                Else
                    db.AddInParameter(commProc, "@P_DipositMaxAmt", DbType.Decimal, DBNull.Value)
                End If

                If txtWithdrawAmt.Text <> "" Then
                    db.AddInParameter(commProc, "@P_WithdrawAmt", DbType.Decimal, txtWithdrawAmt.Text.Trim())
                Else
                    db.AddInParameter(commProc, "@P_WithdrawAmt", DbType.Decimal, DBNull.Value)
                End If

                If txtWithdrawTransNo.Text <> "" Then
                    db.AddInParameter(commProc, "@P_WithdrawTransNo", DbType.Int32, txtWithdrawTransNo.Text.Trim())
                Else
                    db.AddInParameter(commProc, "@P_WithdrawTransNo", DbType.Int32, DBNull.Value)
                End If

                If txtWithdrawMaxAmt.Text <> "" Then
                    db.AddInParameter(commProc, "@P_WithdrawMaxAmt", DbType.Decimal, txtWithdrawMaxAmt.Text.Trim())
                Else
                    db.AddInParameter(commProc, "@P_WithdrawMaxAmt", DbType.Decimal, DBNull.Value)
                End If

                db.AddInParameter(commProc, "@P_TIN", DbType.String, txtTIN.Text.Trim())
                db.AddInParameter(commProc, "@P_BIN", DbType.String, txtBIN.Text.Trim())
                db.AddInParameter(commProc, "@P_VatRegNo", DbType.String, txtVatRegNo.Text.Trim())

                If txtVatRegDt.Text <> "" Then
                    db.AddInParameter(commProc, "@P_VatRegDt", DbType.DateTime, DateTime.ParseExact(txtVatRegDt.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
                Else
                    db.AddInParameter(commProc, "@P_VatRegDt", DbType.DateTime, DBNull.Value)
                End If
                db.AddInParameter(commProc, "@P_CompanyRegNo", DbType.String, txtCompanyRegNo.Text.Trim())

                If txtCompanyRegDt.Text <> "" Then
                    db.AddInParameter(commProc, "@P_CompanyRegDt", DbType.DateTime, DateTime.ParseExact(txtCompanyRegDt.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
                Else
                    db.AddInParameter(commProc, "@P_CompanyRegDt", DbType.DateTime, DBNull.Value)
                End If

                db.AddInParameter(commProc, "@P_RegAuthority", DbType.String, cmbRegAuthority.SelectedValue)
                db.AddInParameter(commProc, "@P_PresAddr", DbType.String, txtPrsAddrs.Text.Trim())
                db.AddInParameter(commProc, "@P_PermAddr", DbType.String, txtPerAddrs.Text.Trim())
                db.AddInParameter(commProc, "@P_PhoneRes1", DbType.String, txtPhoneRes1.Text.Trim())
                db.AddInParameter(commProc, "@P_PhoneRes2", DbType.String, txtPhoneRes2.Text.Trim())
                db.AddInParameter(commProc, "@P_PhoneOff1", DbType.String, txtPhoneOff1.Text.Trim())
                db.AddInParameter(commProc, "@P_PhoneOff2", DbType.String, txtPhoneOff2.Text.Trim())
                db.AddInParameter(commProc, "@P_Mobile1", DbType.String, txtMobile1.Text.Trim())
                db.AddInParameter(commProc, "@P_Mobile2", DbType.String, txtMobile2.Text.Trim())
                db.AddInParameter(commProc, "@P_AccNoOld", DbType.String, txtAccNoOld.Text.Trim())
                'db.AddInParameter(commProc, "@P_Inserted_By", DbType.String, CommonAppSet.User)


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


                'db.AddInParameter(commProc, "@P_Slno", DbType.Int16, intSlno)
                'db.AddInParameter(commProc, "@P_App_Id", DbType.String, txtId.Text.Trim())
                'db.AddInParameter(commProc, "@P_App_Name", DbType.String, txtName.Text.Trim())
                'db.AddInParameter(commProc, "@P_Input_By", DbType.String, CommonAppSet.UserId)


                Dim result As Integer
                result = db.ExecuteNonQuery(commProc, trans)
                If result < 0 Then
                    tStatus = TransState.Exist

                Else
                    tStatus = TransState.Update
                    _intModno = intModno

                    '--------------Mizan Work (19-04-16)---------------------

                    If _AcTitle <> txtAccTitle.Text.Trim() Then
                        If _AcTitle = "" Then
                            log_message = " Title : " + txtAccTitle.Text.Trim() + "." + " "
                        Else
                            log_message = " Title : " + _AcTitle + " " + " To " + " " + txtAccTitle.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If
                    If _branch <> cmbBranch.SelectedValue Then
                        log_message = " Branch : " + cmbBranch.Text + "." + " "
                        AccList.Add(log_message)
                    End If

                    If _AcDepAmt <> txtDepositAmt.Text.Trim() Then
                        If _AcDepAmt = "" Then
                            log_message = " Deposit Amount : " + txtDepositAmt.Text.Trim() + "." + " "
                        Else
                            log_message = " Deposit Amount : " + _AcDepAmt + " " + " To " + " " + txtDepositAmt.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If

                    If _AcDepTrNo <> txtDepositTransNo.Text.Trim() Then
                        If _AcDepTrNo = "" Then
                            log_message = " Deposit Trans No : " + txtDepositTransNo.Text.Trim() + "." + " "
                        Else
                            log_message = " Deposit Trans No : " + _AcDepTrNo + " " + " To " + " " + txtDepositTransNo.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If

                    If _DepMaxAmt <> txtDepositMaxAmt.Text.Trim() Then
                        If _DepMaxAmt = "" Then
                            log_message = " Deposit Max Amount : " + txtDepositMaxAmt.Text.Trim() + "." + " "
                        Else
                            log_message = " Deposit Max Amount : " + _DepMaxAmt + " " + " To " + " " + txtDepositMaxAmt.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If

                    If _WithAmt <> txtWithdrawAmt.Text.Trim() Then
                        If _WithAmt = "" Then
                            log_message = " Withdrow Amount : " + txtWithdrawAmt.Text.Trim() + "." + " "
                        Else
                            log_message = " Withdrow Amount : " + _WithAmt + " " + " To " + " " + txtWithdrawAmt.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If

                    If _WithTrNo <> txtWithdrawTransNo.Text.Trim() Then
                        If _WithTrNo = "" Then
                            log_message = " Withdrow Trans No : " + txtWithdrawTransNo.Text.Trim() + "." + " "
                        Else
                            log_message = " Withdrow Trans No : " + _WithTrNo + " " + " To " + " " + txtWithdrawTransNo.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If

                    If _WithMaxAmt <> txtWithdrawMaxAmt.Text.Trim() Then
                        If _WithMaxAmt = "" Then
                            log_message = " Withdrow Max Amount : " + txtWithdrawMaxAmt.Text.Trim() + "." + " "
                        Else
                            log_message = " Withdrow Max Amount : " + _WithMaxAmt + " " + " To " + " " + txtWithdrawMaxAmt.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If

                    If _AcTin <> txtTIN.Text.Trim() Then
                        If _AcTin = "" Then
                            log_message = " TIN Number : " + txtTIN.Text.Trim() + "." + " "
                        Else
                            log_message = " TIN Number : " + _AcTin + " " + " To " + " " + txtTIN.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If

                    If _AcBin <> txtBIN.Text.Trim() Then
                        If _AcBin = "" Then
                            log_message = " BIN Number : " + txtBIN.Text.Trim() + "." + " "
                        Else
                            log_message = " BIN Number : " + _AcBin + " " + " To " + " " + txtBIN.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If

                    If _VatReg <> txtVatRegNo.Text.Trim() Then
                        If _VatReg = "" Then
                            log_message = " Vat Regi : " + txtVatRegNo.Text.Trim() + "." + " "
                        Else
                            log_message = " Vat Regi : " + _VatReg + " " + " To " + " " + txtVatRegNo.Text.Trim() + "." + " "
                        End If

                        AccList.Add(log_message)
                    End If

                    If _PreAdd <> txtPrsAddrs.Text.Trim() Then
                        If _PreAdd = "" Then
                            log_message = " Present ADD : " + txtPrsAddrs.Text.Trim() + "." + " "
                        Else
                            log_message = " Present ADD : " + _PreAdd + " " + " To " + " " + txtPrsAddrs.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If

                    If _ComRegi <> txtCompanyRegNo.Text.Trim() Then
                        If _ComRegi = "" Then
                            log_message = " Company Regi : " + txtCompanyRegNo.Text.Trim() + "." + " "
                        Else
                            log_message = " Company Regi : " + _ComRegi + " " + " To " + " " + txtCompanyRegNo.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If

                    If _PerAdd <> txtPerAddrs.Text.Trim() Then
                        If _PerAdd = "" Then
                            log_message = " Permanent Address : " + txtPerAddrs.Text.Trim() + "." + " "
                        Else
                            log_message = " Permanent Address : " + _PerAdd + " " + " To " + " " + txtPerAddrs.Text.Trim() + "." + " "
                        End If
                        AccList.Add(log_message)
                    End If

                    If _OwType <> cmbOwType.Text Then
                        log_message = " Ownership Type : " + _OwType + " " + " To " + " " + cmbOwType.Text + "." + " "
                        AccList.Add(log_message)
                    End If

                    If _AccType <> cmbAccType.Text Then
                        log_message = " Account Type : " + _AccType + " " + " To " + " " + cmbAccType.Text + "." + " "
                        AccList.Add(log_message)
                    End If

                    If _Mobile1 <> txtMobile1.Text.Trim() Then
                        If _Mobile1 = "" Then
                            log_message = " Mobile 1 : " + txtMobile1.Text.Trim() + "." + " "
                        Else
                            log_message = " Mobile 1 : " + _Mobile1 + " " + " To " + " " + txtMobile1.Text.Trim() + "." + " "
                        End If

                        AccList.Add(log_message)
                    End If

                    If _Mobile2 <> txtMobile2.Text.Trim() Then
                        log_message = " Mobile 2 : " + txtMobile2.Text.Trim() + "." + " "
                        AccList.Add(log_message)
                    End If

                    If _PhoneRes1 <> txtPhoneRes1.Text.Trim() Then
                        If _PhoneRes1 = "" Then
                            log_message = " Phone Resident : " + txtPhoneRes1.Text.Trim() + "." + " "
                        Else
                            log_message = " Phone Resident : " + _PhoneRes1 + " " + " To " + " " + txtPhoneRes1.Text.Trim() + "." + " "
                        End If

                        AccList.Add(log_message)
                    End If

                    If _PhoneRes2 <> txtPhoneRes2.Text.Trim() Then
                        log_message = " Phone Resident " + txtPhoneRes2.Text.Trim() + "." + " "
                        AccList.Add(log_message)
                    End If

                    For Each Accloglist As String In AccList
                        _accLog += Accloglist
                    Next

                    _log = " Updated : Account No : " + txtAccNo.Text.ToString() + "." + " " + _accLog

                    Logger.system_log(_log)
                    _accLog = ""
                    AccList.Clear()

                    '--------------Mizan Work (19-04-16)---------------------

                End If

                'intModno = db.ExecuteDataSet(trans, CommandType.Text, "select max(ISNULL(MODNO,0))+1 maxmodno from DEPARTMENT where SLNO=" & _intSlno.ToString()).Tables(0).Rows(0)(0)


                trans.Commit()

               
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

            strSql = "select IS_AUTHORIZED,STATUS from FIU_ACCOUNT_INFO where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString()

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

            If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                    strSql = "update FIU_ACCOUNT_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                    "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                    " where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString()

                ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                    strSql = "update FIU_ACCOUNT_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
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
                        strSql = "update FIU_ACCOUNT_INFO set STATUS = 'C' " & _
                            " where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & (_intModno - 1).ToString() & _
                            " and STATUS ='D'"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                        'if previous modification status is L(Deleted) then make it O(Open)
                        strSql = "update FIU_ACCOUNT_INFO set STATUS = 'O' " & _
                            " where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & (_intModno - 1).ToString() & _
                            " and STATUS ='L'"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                    End If
                    tStatus = TransState.Update

                    '----Mizan Work (19-04-16)-------------

                    If _intModno > 1 Then

                        If _Title <> _AcTitle Then
                            If _Title = "" Then
                                log_message = " Title : " + _AcTitle + "." + " "
                            Else
                                log_message = " Title : " + _Title + " To " + _AcTitle + "." + " "
                            End If

                            AccList.Add(log_message)

                        End If
                        If _DepAmt <> _AcDepAmt Then

                            If _DepAmt = "" Then
                                log_message = " Deposit Amount : " + _AcDepAmt + "." + " "
                            Else
                                log_message = " Deposit Amount : " + _DepAmt + " To " + _AcDepAmt + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _DepTrNo <> _AcDepTrNo Then
                            If _DepTrNo = "" Then
                                log_message = " Deposit Trans No : " + _AcDepTrNo + "." + " "
                            Else
                                log_message = " Deposit Trans No : " + _DepTrNo + " To " + _AcDepTrNo + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _DeptMaxAmt <> _DepMaxAmt Then
                            If _DeptMaxAmt = "" Then
                                log_message = " Deposit Max Amount : " + _DepMaxAmt + "." + " "
                            Else
                                log_message = " Deposit Max Amount : " + _DeptMaxAmt + " To " + _DepMaxAmt + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _WithdrawAmt <> _WithAmt Then
                            If _WithdrawAmt = "" Then
                                log_message = " Withdrow Amount : " + _WithAmt + "." + " "
                            Else
                                log_message = " Withdrow Amount : " + _WithdrawAmt + " To " + _WithAmt + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _WithdrawTrNo <> _WithTrNo Then
                            If _WithdrawTrNo = "" Then
                                log_message = " Withdrow Trans No : " + _WithTrNo + "." + " "
                            Else
                                log_message = " Withdrow Trans No : " + _WithdrawTrNo + " To " + _WithTrNo + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _WithdrawMaxAmt <> _WithMaxAmt Then
                            If _WithdrawMaxAmt = "" Then
                                log_message = " Withdrow Max Amount : " + _WithMaxAmt + "." + " "
                            Else
                                log_message = " Withdrow Max Amount : " + _WithdrawMaxAmt + " To " + _WithMaxAmt + "." + " "

                            End If

                            AccList.Add(log_message)
                        End If

                        If _Tin <> _AcTin Then
                            If _Tin = "" Then
                                log_message = " TIN Number : " + _AcTin + "." + " "
                            Else
                                log_message = " TIN Number : " + _Tin + " To " + _AcTin + "." + " "
                            End If
                            AccList.Add(log_message)
                        End If

                        If _Bin <> _AcBin Then
                            If _Bin = "" Then
                                log_message = " BIN Number : " + _AcBin + "." + " "
                            Else
                                log_message = " BIN Number : " + _Bin + " To " + _AcBin + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _VatRegi <> _VatReg Then
                            If _VatRegi = "" Then
                                log_message = " Vat Regi : " + _VatReg + "." + " "
                            Else
                                log_message = " Vat Regi : " + _VatRegi + " To " + _VatReg + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _PreAddrs <> _PreAdd Then
                            If _PreAddrs = "" Then
                                log_message = " Present ADD : " + _PreAdd + "." + " "
                            Else
                                log_message = " Present ADD : " + _PreAddrs + " To " + _PreAdd + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _ComReg <> _ComRegi Then
                            If _ComReg = "" Then
                                log_message = " Company Regi : " + _ComRegi + "." + " "
                            Else
                                log_message = " Company Regi : " + _ComReg + " To " + _ComRegi + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _PerAddrs <> _PerAdd Then
                            If _PerAddrs = "" Then
                                log_message = " Permanent Address : " + _PerAdd + "." + " "
                            Else
                                log_message = " Permanent Address : " + _PerAddrs + " To " + _PerAdd + "." + " "
                            End If
                            AccList.Add(log_message)
                        End If
                        If _Acbranch <> _branch Then
                            log_message = " Branch : " + cmbBranch.Text + "." + " "
                            AccList.Add(log_message)
                        End If

                        If _OwnerType <> _OwType Then
                            log_message = " Ownership Type : " + _OwnerType + " To " + _OwType + "." + " "
                            AccList.Add(log_message)
                        End If

                        If _AccountType <> _AccType Then
                            log_message = " Account Type : " + _AccountType + " To " + _AccType + "." + " "
                            AccList.Add(log_message)
                        End If

                        If _MobileOne <> _Mobile1 Then
                            log_message = " Mobile 1 : " + txtMobile1.Text.Trim() + "." + " "

                            AccList.Add(log_message)
                        End If

                        If _MobileTwo <> _Mobile2 Then
                            log_message = " Mobile 2 : " + txtMobile2.Text.Trim() + "." + " "

                            AccList.Add(log_message)
                        End If

                        If _Phone1 <> _PhoneRes1 Then
                            log_message = " Phone Resident : " + txtPhoneRes1.Text.Trim() + "." + " "

                            AccList.Add(log_message)
                        End If

                        If _Phone2 <> _PhoneRes2 Then
                            log_message = " Phone Resident " + txtPhoneRes2.Text.Trim() + "." + " "

                            AccList.Add(log_message)
                        End If

                        For Each Accloglist As String In AccList
                            _accLog += Accloglist
                        Next

                        _log = " Authorized : Account No : " + txtAccNo.Text.ToString() + "." + " " + _accLog

                        Logger.system_log(_log)
                        _accLog = ""
                        AccList.Clear()


                    Else


                        If _Title <> _AcTitle Then
                            If _Title = "" Then
                                log_message = " Title : " + _AcTitle + "." + " "
                            Else
                                log_message = " Title : " + _Title + " To " + _AcTitle + "." + " "
                            End If

                            AccList.Add(log_message)

                        End If

                        If _Acbranch <> _branch Then
                            log_message = " Branch : " + cmbBranch.Text + "." + " "
                            AccList.Add(log_message)
                        End If

                        If _DepAmt <> _AcDepAmt Then

                            If _DepAmt = "" Then
                                'log_message = " Deposit Amount : " + _AcDepAmt + "." + " "
                            Else
                                log_message = " Deposit Amount : " + _DepAmt + " To " + _AcDepAmt + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _DepTrNo <> _AcDepTrNo Then
                            If _DepTrNo = "" Then
                                'log_message = " Deposit Trans No : " + _AcDepTrNo + "." + " "
                            Else
                                log_message = " Deposit Trans No : " + _DepTrNo + " To " + _AcDepTrNo + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _DeptMaxAmt <> _DepMaxAmt Then
                            If _DeptMaxAmt = "" Then
                                'log_message = " Deposit Max Amount : " + _DepMaxAmt + "." + " "
                            Else
                                log_message = " Deposit Max Amount : " + _DeptMaxAmt + " To " + _DepMaxAmt + "." + " "
                                AccList.Add(log_message)

                            End If

                        End If

                        If _WithdrawAmt <> _WithAmt Then
                            If _WithdrawAmt = "" Then
                                'log_message = " Withdrow Amount : " + _WithAmt + "." + " "
                            Else
                                log_message = " Withdrow Amount : " + _WithdrawAmt + " To " + _WithAmt + "." + " "
                                AccList.Add(log_message)

                            End If


                        End If

                        If _WithdrawTrNo <> _WithTrNo Then
                            If _WithdrawTrNo = "" Then
                                'log_message = " Withdrow Trans No : " + _WithTrNo + "." + " "
                            Else
                                log_message = " Withdrow Trans No : " + _WithdrawTrNo + " To " + _WithTrNo + "." + " "
                                AccList.Add(log_message)

                            End If


                        End If

                        If _WithdrawMaxAmt <> _WithMaxAmt Then
                            If _WithdrawMaxAmt = "" Then
                                'log_message = " Withdrow Max Amount : " + _WithMaxAmt + "." + " "
                            Else
                                log_message = " Withdrow Max Amount : " + _WithdrawMaxAmt + " To " + _WithMaxAmt + "." + " "
                                AccList.Add(log_message)
                            End If
                        End If

                        If _Tin <> _AcTin Then
                            If _Tin = "" Then
                                'log_message = " TIN Number : " + _AcTin + "." + " "
                            Else
                                log_message = " TIN Number : " + _Tin + " To " + _AcTin + "." + " "
                                AccList.Add(log_message)
                            End If

                        End If

                        If _Bin <> _AcBin Then
                            If _Bin = "" Then
                                'log_message = " BIN Number : " + _AcBin + "." + " "
                            Else
                                log_message = " BIN Number : " + _Bin + " To " + _AcBin + "." + " "
                                AccList.Add(log_message)
                            End If
                        End If

                        If _VatRegi <> _VatReg Then
                            If _VatRegi = "" Then
                                ' log_message = " Vat Regi : " + _VatReg + "." + " "
                            Else
                                log_message = " Vat Regi : " + _VatRegi + " To " + _VatReg + "." + " "
                                AccList.Add(log_message)
                            End If
                        End If

                        If _PreAddrs <> _PreAdd Then
                            If _PreAddrs = "" Then
                                'log_message = " Present ADD : " + _PreAdd + "." + " "
                            Else
                                log_message = " Present ADD : " + _PreAddrs + " To " + _PreAdd + "." + " "
                                AccList.Add(log_message)
                            End If

                        End If

                        If _ComReg <> _ComRegi Then
                            If _ComReg = "" Then
                                ' log_message = " Company Regi : " + _ComRegi + "." + " "
                            Else
                                log_message = " Company Regi : " + _ComReg + " To " + _ComRegi + "." + " "
                                AccList.Add(log_message)
                            End If
                        End If

                        If _PerAddrs <> _PerAdd Then
                            If _PerAddrs = "" Then
                                'log_message = " Permanent Address : " + _PerAdd + "." + " "
                            Else
                                log_message = " Permanent Address : " + _PerAddrs + " To " + _PerAdd + "." + " "
                                AccList.Add(log_message)
                            End If

                        End If

                        If _OwnerType <> _OwType Then
                            If _OwnerType = "" Then
                            Else
                                log_message = " Ownership Type : " + _OwnerType + " To " + _OwType + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _AccountType <> _AccType Then
                            If _AccountType = "" Then
                            Else
                                log_message = " Account Type : " + _AccountType + " To " + _AccType + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _MobileOne <> _Mobile1 Then
                            If _MobileOne = "" Then
                            Else
                                log_message = " Mobile 1 : " + txtMobile1.Text.Trim() + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _MobileTwo <> _Mobile2 Then
                            If _MobileTwo = "" Then
                            Else
                                log_message = " Mobile 2 : " + txtMobile2.Text.Trim() + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _Phone1 <> _PhoneRes1 Then
                            If _Phone1 = "" Then
                            Else
                                log_message = " Phone Resident : " + txtPhoneRes1.Text.Trim() + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _Phone2 <> _PhoneRes2 Then
                            If _Phone2 = "" Then
                            Else
                                log_message = " Phone Resident " + txtPhoneRes2.Text.Trim() + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        For Each Accloglist As String In AccList
                            _accLog += Accloglist
                        Next

                        _log = " Authorized : Account No : " + txtAccNo.Text.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _accLog

                        Logger.system_log(_log)
                        _accLog = ""
                        AccList.Clear()

                    End If

                    '----------------------Mizan Work (19-04-16)-------------

                End If
            Else
                tStatus = TransState.UpdateNotPossible
            End If


            trans.Commit()



        End Using

        Return tStatus



    End Function

    '--------------Mizan Work (12-04-16)---------------------

    Private Sub LoadDataForAuth(ByVal strBankCd As String, ByVal strBranchCd As String, ByVal strAcNo As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_ACCOUNT_INFO Where Bank_Code='" & strBankCd & "' and Branch_Code='" & strBranchCd & "' and AcNumber='" & strAcNo & "' and STATUS='L'")

            If ds.Tables(0).Rows.Count > 0 Then


                _strBank_Code = ds.Tables(0).Rows(0)("Bank_Code").ToString()
                _strBranch_Code = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                _strAcNumber = ds.Tables(0).Rows(0)("AcNumber").ToString



                _formMode = FormTransMode.Update

                'cmbBank.SelectedValue = ds.Tables(0).Rows(0)("Bank_Code").ToString()
                'cmbBranch.SelectedValue = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                _Acbranch = ds.Tables(0).Rows(0)("Branch_Code").ToString()

                _AccNumber = ds.Tables(0).Rows(0)("AcNumber").ToString

                _Title = ds.Tables(0).Rows(0)("Ac_Title").ToString

                _AccountType = ds.Tables(0).Rows(0)("AcTypeCode").ToString()

                _OwnerType = ds.Tables(0).Rows(0)("OwTypeCode").ToString()


                _DepAmt = ds.Tables(0).Rows(0)("Declared_Deposit_Amount").ToString


                _DepTrNo = ds.Tables(0).Rows(0)("Declared_Deposit_TransNo").ToString


                _DeptMaxAmt = ds.Tables(0).Rows(0)("Declared_Deposit_MaxAmount").ToString



                _WithdrawAmt = ds.Tables(0).Rows(0)("Declared_Withdr_Amount").ToString


                _WithdrawTrNo = ds.Tables(0).Rows(0)("Declared_Withdr_TransNo").ToString


                _WithdrawMaxAmt = ds.Tables(0).Rows(0)("Declared_Withdr_MaxAmount").ToString


                _Tin = ds.Tables(0).Rows(0)("TIN").ToString


                _Bin = ds.Tables(0).Rows(0)("BIN").ToString


                _VatRegi = ds.Tables(0).Rows(0)("Vat_Reg_No").ToString

                
                _ComReg = ds.Tables(0).Rows(0)("Company_Reg_No").ToString

                _PreAddrs = ds.Tables(0).Rows(0)("Pres_Addr").ToString


                _PerAddrs = ds.Tables(0).Rows(0)("Perm_Addr").ToString


                _Phone1 = ds.Tables(0).Rows(0)("Phone_Res1").ToString

                _Phone2 = ds.Tables(0).Rows(0)("Phone_Res2").ToString
               

                _MobileOne = ds.Tables(0).Rows(0)("Mobile1").ToString

                _MobileTwo = ds.Tables(0).Rows(0)("Mobile2").ToString

                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_ACCOUNT_TYPES Where ACTYPECODE = '" & _AccountType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _AccountTypeName = ds2.Tables(0).Rows(0)("ACDEFINITION").ToString()
                    _AccountType = _AccountTypeName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_OWNERSHIP_TYPES Where OWTYPECODE = '" & _OwnerType & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _OwnerTypeName = ds3.Tables(0).Rows(0)("OWDEFINITION").ToString()
                    _OwnerType = _OwnerTypeName

                End If



            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadAccData(ByVal strBankCd As String, ByVal strBranchCd As String, ByVal strAcNo As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            'ds = db.ExecuteDataSet(CommandType.Text, "select * from APPS where SLNO=" & intslno & " and MODNO=" & intmod)

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_ACCOUNT_INFO Where Bank_Code='" & strBankCd & "' and Branch_Code='" & strBranchCd & "' and AcNumber='" & strAcNo & "' and MODNO=" & intMod)

            'MsgBox(ds.Tables(0).Rows.Count)

            If ds.Tables(0).Rows.Count > 0 Then


                _strBank_Code = ds.Tables(0).Rows(0)("Bank_Code").ToString()
                _strBranch_Code = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                _strAcNumber = ds.Tables(0).Rows(0)("AcNumber").ToString
                _intModno = intMod

                '_intSlno = intslno

                _formMode = FormTransMode.Update




                cmbBank.SelectedValue = ds.Tables(0).Rows(0)("Bank_Code").ToString()
                cmbBranch.SelectedValue = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                _branch = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                txtAccNo.Text = ds.Tables(0).Rows(0)("AcNumber").ToString
                _AccNo = ds.Tables(0).Rows(0)("AcNumber").ToString
                txtAccTitle.Text = ds.Tables(0).Rows(0)("Ac_Title").ToString
                _AcTitle = ds.Tables(0).Rows(0)("Ac_Title").ToString
                cmbAccType.SelectedValue = ds.Tables(0).Rows(0)("AcTypeCode").ToString
                _AccType = ds.Tables(0).Rows(0)("AcTypeCode").ToString()
                cmbOwType.SelectedValue = ds.Tables(0).Rows(0)("OwTypeCode").ToString
                _OwType = ds.Tables(0).Rows(0)("OwTypeCode").ToString()

                txtDepositAmt.Text = ds.Tables(0).Rows(0)("Declared_Deposit_Amount").ToString
                _AcDepAmt = ds.Tables(0).Rows(0)("Declared_Deposit_Amount").ToString

                txtDepositTransNo.Text = ds.Tables(0).Rows(0)("Declared_Deposit_TransNo").ToString
                _AcDepTrNo = ds.Tables(0).Rows(0)("Declared_Deposit_TransNo").ToString

                txtDepositMaxAmt.Text = ds.Tables(0).Rows(0)("Declared_Deposit_MaxAmount").ToString
                _DepMaxAmt = ds.Tables(0).Rows(0)("Declared_Deposit_MaxAmount").ToString


                txtWithdrawAmt.Text = ds.Tables(0).Rows(0)("Declared_Withdr_Amount").ToString
                _WithAmt = ds.Tables(0).Rows(0)("Declared_Withdr_Amount").ToString

                txtWithdrawTransNo.Text = ds.Tables(0).Rows(0)("Declared_Withdr_TransNo").ToString
                _WithTrNo = ds.Tables(0).Rows(0)("Declared_Withdr_TransNo").ToString

                txtWithdrawMaxAmt.Text = ds.Tables(0).Rows(0)("Declared_Withdr_MaxAmount").ToString
                _WithMaxAmt = ds.Tables(0).Rows(0)("Declared_Withdr_MaxAmount").ToString

                txtTIN.Text = ds.Tables(0).Rows(0)("TIN").ToString
                _AcTin = ds.Tables(0).Rows(0)("TIN").ToString

                txtBIN.Text = ds.Tables(0).Rows(0)("BIN").ToString
                _AcBin = ds.Tables(0).Rows(0)("BIN").ToString

                txtVatRegNo.Text = ds.Tables(0).Rows(0)("Vat_Reg_No").ToString
                _VatReg = ds.Tables(0).Rows(0)("Vat_Reg_No").ToString

                txtVatRegDt.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Vat_Reg_Date"))
                txtCompanyRegNo.Text = ds.Tables(0).Rows(0)("Company_Reg_No").ToString
                _ComRegi = ds.Tables(0).Rows(0)("Company_Reg_No").ToString

                txtCompanyRegDt.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Company_Reg_Date"))
                cmbRegAuthority.SelectedValue = ds.Tables(0).Rows(0)("Reg_Authority_Code").ToString
                txtPrsAddrs.Text = ds.Tables(0).Rows(0)("Pres_Addr").ToString

                _PreAdd = ds.Tables(0).Rows(0)("Pres_Addr").ToString

                txtPerAddrs.Text = ds.Tables(0).Rows(0)("Perm_Addr").ToString
                _PerAdd = ds.Tables(0).Rows(0)("Perm_Addr").ToString

                txtPhoneRes1.Text = ds.Tables(0).Rows(0)("Phone_Res1").ToString
                _PhoneRes1 = ds.Tables(0).Rows(0)("Phone_Res1").ToString
                txtPhoneRes2.Text = ds.Tables(0).Rows(0)("Phone_Res2").ToString
                _PhoneRes2 = ds.Tables(0).Rows(0)("Phone_Res2").ToString
                txtPhoneOff1.Text = ds.Tables(0).Rows(0)("Phone_Office1").ToString
                txtPhoneOff2.Text = ds.Tables(0).Rows(0)("Phone_Office2").ToString
                txtMobile1.Text = ds.Tables(0).Rows(0)("Mobile1").ToString
                _Mobile1 = ds.Tables(0).Rows(0)("Mobile1").ToString
                txtMobile2.Text = ds.Tables(0).Rows(0)("Mobile2").ToString
                _Mobile2 = ds.Tables(0).Rows(0)("Mobile2").ToString
                txtAccNoOld.Text = ds.Tables(0).Rows(0)("Old_AcNumber").ToString

                ''------------------Mizan Work (25-04-16) ----------------------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_ACCOUNT_TYPES Where ACTYPECODE = '" & _AccType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _AccTypeName = ds2.Tables(0).Rows(0)("ACDEFINITION").ToString()
                    _AccType = _AccTypeName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_OWNERSHIP_TYPES Where OWTYPECODE = '" & _OwType & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _OwTypeName = ds3.Tables(0).Rows(0)("OWDEFINITION").ToString()
                    _OwType = _OwTypeName

                End If
                ''------------------Mizan Work (25-04-16) ----------------------

                txtInsertedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Inserted_On"))
                txtModifiedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Modified_On"))

                'lblInputBy.Text = ds.Tables(0).Rows(0)("Inserted_By").ToString()
                'lblInputDate.Text = ds.Tables(0).Rows(0)("Inserted_On").ToString()
                'lblAuthDate.Text = ds.Tables(0).Rows(0)("Inserted_On").ToString()

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
                lblVerTot.Text = db.ExecuteDataSet(CommandType.Text, "Select Count(ModNo) From FIU_ACCOUNT_INFO Where Bank_Code='" & strBankCd & "' and Branch_Code='" & strBranchCd & "' and AcNumber='" & strAcNo & "'").Tables(0).Rows(0)(0).ToString()



                'txtId.Text = ds.Tables(0).Rows(0)("APP_ID").ToString()
                'txtName.Text = ds.Tables(0).Rows(0)("APP_NAME").ToString()

                

                'chkAuthorized.Checked = ds.Tables(0).Rows(0)("IS_AUTHORIZED")

                'If ds.Tables(0).Rows(0)("STATUS") = "L" Or ds.Tables(0).Rows(0)("STATUS") = "U" Or ds.Tables(0).Rows(0)("STATUS") = "O" Then
                '    chkOpen.Checked = True
                'Else
                '    chkOpen.Checked = False
                'End If
                'lblModNo.Text = ds.Tables(0).Rows(0)("MODNO").ToString()

                'lblVerNo.Text = ds.Tables(0).Rows(0)("MODNO").ToString()

                'lblVerTot.Text = db.ExecuteDataSet(CommandType.Text, "select count(MODNO) 'totver' from APPS where SLNO=" & intslno).Tables(0).Rows(0)(0).ToString()

            Else
                '_intModno = 0
                '_intSlno = 0

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadAccountData(ByVal strAcNo As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_AccountInfoOld_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ACNUMBER", DbType.String, strAcNo)

            ds = db.ExecuteDataSet(commProc)


            If ds.Tables(0).Rows.Count > 0 Then

                _strTransCode = strAcNo
                _strBank_Code = ds.Tables(0).Rows(0)("Bank_Code").ToString()
                _strBranch_Code = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                _strAcNumber = ds.Tables(0).Rows(0)("AcNumber").ToString

                _formMode = FormTransMode.Update


                txtAccNo.Text = ds.Tables(0).Rows(0)("ACNUMBER").ToString()

                cmbBank.SelectedValue = ds.Tables(0).Rows(0)("Bank_Code").ToString()
                cmbBranch.SelectedValue = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                txtAccNo.Text = ds.Tables(0).Rows(0)("AcNumber").ToString
                _AccNo = ds.Tables(0).Rows(0)("AcNumber").ToString
                txtAccTitle.Text = ds.Tables(0).Rows(0)("Ac_Title").ToString
                _AcTitle = ds.Tables(0).Rows(0)("Ac_Title").ToString
                cmbAccType.SelectedValue = ds.Tables(0).Rows(0)("AcTypeCode").ToString
                cmbOwType.SelectedValue = ds.Tables(0).Rows(0)("OwTypeCode").ToString

                txtDepositAmt.Text = ds.Tables(0).Rows(0)("Declared_Deposit_Amount").ToString
                _AcDepAmt = ds.Tables(0).Rows(0)("Declared_Deposit_Amount").ToString


                txtDepositTransNo.Text = ds.Tables(0).Rows(0)("Declared_Deposit_TransNo").ToString
                _AcDepTrNo = ds.Tables(0).Rows(0)("Declared_Deposit_TransNo").ToString

                txtDepositMaxAmt.Text = ds.Tables(0).Rows(0)("Declared_Deposit_MaxAmount").ToString
                _DepMaxAmt = ds.Tables(0).Rows(0)("Declared_Deposit_MaxAmount").ToString


                txtWithdrawAmt.Text = ds.Tables(0).Rows(0)("Declared_Withdr_Amount").ToString
                _WithAmt = ds.Tables(0).Rows(0)("Declared_Withdr_Amount").ToString

                txtWithdrawTransNo.Text = ds.Tables(0).Rows(0)("Declared_Withdr_TransNo").ToString
                _WithTrNo = ds.Tables(0).Rows(0)("Declared_Withdr_TransNo").ToString

                txtWithdrawMaxAmt.Text = ds.Tables(0).Rows(0)("Declared_Withdr_MaxAmount").ToString
                _WithMaxAmt = ds.Tables(0).Rows(0)("Declared_Withdr_MaxAmount").ToString

                txtTIN.Text = ds.Tables(0).Rows(0)("TIN").ToString
                _AcTin = ds.Tables(0).Rows(0)("TIN").ToString

                txtBIN.Text = ds.Tables(0).Rows(0)("BIN").ToString
                _AcBin = ds.Tables(0).Rows(0)("BIN").ToString

                txtVatRegNo.Text = ds.Tables(0).Rows(0)("Vat_Reg_No").ToString
                _VatReg = ds.Tables(0).Rows(0)("Vat_Reg_No").ToString

                txtVatRegDt.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Vat_Reg_Date"))
                txtCompanyRegNo.Text = ds.Tables(0).Rows(0)("Company_Reg_No").ToString
                _ComRegi = ds.Tables(0).Rows(0)("Company_Reg_No").ToString

                txtCompanyRegDt.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Company_Reg_Date"))
                cmbRegAuthority.SelectedValue = ds.Tables(0).Rows(0)("Reg_Authority_Code").ToString
                txtPrsAddrs.Text = ds.Tables(0).Rows(0)("Pres_Addr").ToString

                _PreAdd = ds.Tables(0).Rows(0)("Pres_Addr").ToString

                txtPerAddrs.Text = ds.Tables(0).Rows(0)("Perm_Addr").ToString
                _PerAdd = ds.Tables(0).Rows(0)("Perm_Addr").ToString

                txtPhoneRes1.Text = ds.Tables(0).Rows(0)("Phone_Res1").ToString
                _PhoneRes1 = ds.Tables(0).Rows(0)("Phone_Res1").ToString
                txtPhoneRes2.Text = ds.Tables(0).Rows(0)("Phone_Res2").ToString
                _PhoneRes2 = ds.Tables(0).Rows(0)("Phone_Res2").ToString
                txtPhoneOff1.Text = ds.Tables(0).Rows(0)("Phone_Office1").ToString
                txtPhoneOff2.Text = ds.Tables(0).Rows(0)("Phone_Office2").ToString
                txtMobile1.Text = ds.Tables(0).Rows(0)("Mobile1").ToString
                _Mobile1 = ds.Tables(0).Rows(0)("Mobile1").ToString
                txtMobile2.Text = ds.Tables(0).Rows(0)("Mobile2").ToString
                _Mobile2 = ds.Tables(0).Rows(0)("Mobile2").ToString
                txtAccNoOld.Text = ds.Tables(0).Rows(0)("Old_AcNumber").ToString

                txtInsertedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Inserted_On"))
                txtModifiedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Modified_On"))

                'lblInputBy.Text = ds.Tables(0).Rows(0)("Inserted_By").ToString()
                'lblInputDate.Text = ds.Tables(0).Rows(0)("Inserted_On").ToString()
                'lblAuthDate.Text = ds.Tables(0).Rows(0)("Inserted_On").ToString()

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


                _intModno = ds.Tables(0).Rows(0)("MODNO")
                Dim intmod As Integer = ds.Tables(0).Rows(0)("MODNO")





                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_AccountInfoOld_GetMaxMod")

                commProc2.Parameters.Clear()

                db.AddInParameter(commProc2, "@ACNUMBER", DbType.String, strAcNo)

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
            MessageBox.Show("Account Number not Exists!! Please Maintenance Account Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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


            strSql = "select IS_AUTHORIZED,STATUS from FIU_ACCOUNT_INFO where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString()

            Dim ds As New DataSet
            ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0)(0) = False Then 'if not authorized

                    strSql = "delete FIU_ACCOUNT_INFO where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString() & " and IS_AUTHORIZED=0"

                    db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    _intModno = _intModno - 1

                    tStatus = TransState.Delete


                ElseIf ds.Tables(0).Rows(0)(0) = True Then 'if authorized

                    If ds.Tables(0).Rows(0)("STATUS") = "L" Then 'if this is the last modified data

                        strSql = "delete FIU_ACCOUNT_INFO where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and IS_AUTHORIZED=0"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                        strSql = "select * from FIU_ACCOUNT_INFO where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString()

                        Dim dsKeeper As New DataSet
                        dsKeeper = db.ExecuteDataSet(trans, CommandType.Text, strSql)


                        strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_ACCOUNT_INFO where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "'"


                        intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)



                        strSql = "Insert Into FIU_ACCOUNT_INFO(Bank_Code,Branch_Code," & _
                     "AcNumber,Ac_Title,AcTypeCode,OwTypeCode," & _
                     "Declared_Deposit_Amount,Declared_Deposit_TransNo," & _
                     "Declared_Deposit_MaxAmount,Declared_Withdr_Amount," & _
                     "Declared_Withdr_TransNo,Declared_Withdr_MaxAmount," & _
                     "TIN,BIN,Vat_Reg_No,Vat_Reg_Date,Company_Reg_No,Company_Reg_Date," & _
                     "Reg_Authority_Code,Pres_Addr,Perm_Addr,Phone_Res1,Phone_Res2," & _
                     "Phone_Office1,Phone_Office2,Mobile1,Mobile2,Old_AcNumber," & _
                     "Inserted_On,Modified_On,ModNo,Input_By,Input_Datetime,Is_Authorized,Status)" & _
                     "Values(@P_Bank_Code,@P_Branch_Code,@P_AcNumber,@P_Ac_Title," & _
                     "@P_AcTypeCode,@P_OwTypeCode,@P_DipositAmt,@P_DipositTransNo," & _
                     "@P_DipositMaxAmt,@P_WithdrawAmt,@P_WithdrawTransNo,@P_WithdrawMaxAmt," & _
                     "@P_TIN,@P_BIN,@P_VatRegNo,@P_VatRegDt,@P_CompanyRegNo,@P_CompanyRegDt," & _
                     "@P_RegAuthority,@P_PresAddr,@P_PermAddr,@P_PhoneRes1,@P_PhoneRes2," & _
                     "@P_PhoneOff1,@P_PhoneOff2,@P_Mobile1,@P_Mobile2,@P_AccNoOld," & _
                     "@P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'D')"

                        Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        db.AddInParameter(commProc, "@P_Bank_Code", DbType.String, _strBank_Code)
                        db.AddInParameter(commProc, "@P_Branch_Code", DbType.String, _strBranch_Code)
                        db.AddInParameter(commProc, "@P_AcNumber", DbType.String, _strAcNumber)
                        db.AddInParameter(commProc, "@P_Ac_Title", DbType.String, dsKeeper.Tables(0).Rows(0)("Ac_Title"))
                        db.AddInParameter(commProc, "@P_AcTypeCode", DbType.String, dsKeeper.Tables(0).Rows(0)("AcTypeCode"))
                        db.AddInParameter(commProc, "@P_OwTypeCode", DbType.String, dsKeeper.Tables(0).Rows(0)("OwTypeCode"))


                        db.AddInParameter(commProc, "@P_DipositAmt", DbType.Decimal, dsKeeper.Tables(0).Rows(0)("Declared_Deposit_Amount"))

                        db.AddInParameter(commProc, "@P_DipositTransNo", DbType.Int32, dsKeeper.Tables(0).Rows(0)("Declared_Deposit_TransNo"))

                        db.AddInParameter(commProc, "@P_DipositMaxAmt", DbType.Decimal, dsKeeper.Tables(0).Rows(0)("Declared_Deposit_MaxAmount"))

                        db.AddInParameter(commProc, "@P_WithdrawAmt", DbType.Decimal, dsKeeper.Tables(0).Rows(0)("Declared_Withdr_Amount"))

                        db.AddInParameter(commProc, "@P_WithdrawTransNo", DbType.Int32, dsKeeper.Tables(0).Rows(0)("Declared_Withdr_TransNo"))

                        db.AddInParameter(commProc, "@P_WithdrawMaxAmt", DbType.Decimal, dsKeeper.Tables(0).Rows(0)("Declared_Withdr_MaxAmount"))


                        db.AddInParameter(commProc, "@P_TIN", DbType.String, dsKeeper.Tables(0).Rows(0)("TIN"))
                        db.AddInParameter(commProc, "@P_BIN", DbType.String, dsKeeper.Tables(0).Rows(0)("BIN"))
                        db.AddInParameter(commProc, "@P_VatRegNo", DbType.String, dsKeeper.Tables(0).Rows(0)("Vat_Reg_No"))

                        
                        db.AddInParameter(commProc, "@P_VatRegDt", DbType.DateTime, dsKeeper.Tables(0).Rows(0)("Vat_Reg_Date"))

                        db.AddInParameter(commProc, "@P_CompanyRegNo", DbType.String, dsKeeper.Tables(0).Rows(0)("Company_Reg_No"))

                        
                        db.AddInParameter(commProc, "@P_CompanyRegDt", DbType.DateTime, dsKeeper.Tables(0).Rows(0)("Company_Reg_Date"))


                        db.AddInParameter(commProc, "@P_RegAuthority", DbType.String, dsKeeper.Tables(0).Rows(0)("Reg_Authority_Code"))
                        db.AddInParameter(commProc, "@P_PresAddr", DbType.String, dsKeeper.Tables(0).Rows(0)("Pres_Addr"))
                        db.AddInParameter(commProc, "@P_PermAddr", DbType.String, dsKeeper.Tables(0).Rows(0)("Perm_Addr"))
                        db.AddInParameter(commProc, "@P_PhoneRes1", DbType.String, dsKeeper.Tables(0).Rows(0)("Phone_Res1"))
                        db.AddInParameter(commProc, "@P_PhoneRes2", DbType.String, dsKeeper.Tables(0).Rows(0)("Phone_Res2"))
                        db.AddInParameter(commProc, "@P_PhoneOff1", DbType.String, dsKeeper.Tables(0).Rows(0)("Phone_Office1"))
                        db.AddInParameter(commProc, "@P_PhoneOff2", DbType.String, dsKeeper.Tables(0).Rows(0)("Phone_Office2"))
                        db.AddInParameter(commProc, "@P_Mobile1", DbType.String, dsKeeper.Tables(0).Rows(0)("Mobile1"))
                        db.AddInParameter(commProc, "@P_Mobile2", DbType.String, dsKeeper.Tables(0).Rows(0)("Mobile2"))
                        db.AddInParameter(commProc, "@P_AccNoOld", DbType.String, dsKeeper.Tables(0).Rows(0)("Old_AcNumber"))

                        'db.AddInParameter(commProc, "@P_Inserted_By", DbType.String, dsKeeper.Tables(0).Rows(0)("Inserted_By"))


                        db.AddInParameter(commProc, "@P_Inserted_On", DbType.DateTime, dsKeeper.Tables(0).Rows(0)("Inserted_On"))

                        db.AddInParameter(commProc, "@P_Modified_On", DbType.DateTime, dsKeeper.Tables(0).Rows(0)("Modified_On"))

                        db.AddInParameter(commProc, "@P_Input_By", DbType.String, CommonAppSet.User)


                        'db.AddInParameter(commProc, "@P_Slno", DbType.Int16, intSlno)
                        'db.AddInParameter(commProc, "@P_App_Id", DbType.String, txtId.Text.Trim())
                        'db.AddInParameter(commProc, "@P_App_Name", DbType.String, txtName.Text.Trim())
                        'db.AddInParameter(commProc, "@P_Input_By", DbType.String, CommonAppSet.UserId)


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


            log_message = "Delete Account Number " + txtAccNo.Text.Trim()
            Logger.system_log(log_message)


        End Using


        Return tStatus

    End Function

    Private Function CheckForDelete() As Boolean

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String = ""

        strSql = "select IS_AUTHORIZED,STATUS from FIU_ACCOUNT_INFO where Bank_Code='" & _strBank_Code & "' and Branch_Code='" & _strBranch_Code & "' and AcNumber='" & _strAcNumber & "' and MODNO=" & _intModno.ToString()

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

    Private Sub GroupBox3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub FrmDept_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'txtAuthBy.ForeColor = Color.Maroon
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FrmAccountInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If


        CommonUtil.FillComboBox("Select Bank_Code,Bank_Name From FIU_Bank where STATUS='L'", cmbBank)

        CommonUtil.FillComboBox("Select Branch_Code,Branch_Name From FIU_Bank_Branch where STATUS='L'", cmbBranch)

        CommonUtil.FillComboBox("Select AcTypeCode,AcDefinition From FIU_Account_Types where STATUS='L'", cmbAccType)

        CommonUtil.FillComboBox("Select OwTypeCode,OwDefinition From FIU_OwnerShip_Types where STATUS='L'", cmbOwType)

        CommonUtil.FillComboBox("Select Reg_Authority_Code,Reg_Authority_Name From FIU_Company_Reg_Authority where STATUS='L'", cmbRegAuthority)



        If _intModno > 0 Then  ' Do not understand
    'LoadAppData(_intSlno, _intModno)

            LoadAccData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno)


       
        End If


        If _strTransCode <> "" Then

            LoadAccountData(_strTransCode)

        End If

        EnableUnlock()

        DisableNew()
        DisableSave()
        DisableDelete()
        DisableAuth()
        btnMoreDetail.Enabled = False
        DisableClear()
        DisableRefresh()

        DisableFields()

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Public Sub New(ByVal strAcNo As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()





        _strTransCode = strAcNo
        'LoadAccountData(strAcNo)

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


    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlock.Click

        EnableNew()

        EnableClear()

        If _intModno > 0 Then

            EnableFields()

            EnableSave()

            EnableRefresh()

            EnableDelete()
            btnMoreDetail.Enabled = True

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
        btnMoreDetail.Enabled = True

        'If txtId.Enabled = True Then
        '    txtId.Focus()

        'End If

        If cmbBank.Enabled = True Then
            cmbBank.Focus()
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

                        LoadAccData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno)

                        lblToolStatus.Text = "!! Information Added Successfully !!"

                        _formMode = FormTransMode.Update


                        EnableDelete()

                        EnableRefresh()

                    ElseIf tState = TransState.Update Then

                        'LoadAppData(_intSlno, _intModno)

                        LoadAccData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno)

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
        LoadAccData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno)
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

                        LoadAccData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno)

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
            LoadAccData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno - 1)

        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click

        Dim strBank_Code As String = _strBank_Code
        Dim strBranch_Code As String = _strBranch_Code
        Dim strAcNumber As String = _strAcNumber
        Dim intModno As Integer = _intModno


        LoadAccData(_strBank_Code, _strBranch_Code, _strAcNumber, _intModno + 1)

        If _intModno = 0 Then
            'LoadAppData(intSlno, intModno)
            LoadAccData(strBank_Code, strBranch_Code, strAcNumber, intModno)
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

                '------------Mizan Work (19-04-16)-------------
                If _intModno > 1 Then
                    LoadDataForAuth(_strBank_Code, _strBranch_Code, _strAcNumber)
                End If
                '------------Mizan Work (19-04-16)-------------

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

    Private Sub cmbBank_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBank.LostFocus

        If cmbBank.Text <> "" Then
            CommonUtil.FillComboBox("Select Branch_Code,Branch_Name From FIU_Bank_Branch Where Bank_Code=" & cmbBank.SelectedValue, cmbBranch)
        Else
            cmbBranch.Text = ""
        End If

    End Sub




    Private Sub dgView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        _intSelectedRow = e.RowIndex

    End Sub

    Private Sub btnMoreDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoreDetail.Click
        Dim strAccNO As String = txtAccNo.Text

        If strAccNO = "" Then
            MessageBox.Show("Please Enter Account Number!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub

        End If



        Dim frmAccountInfoDet As New FrmAccountInfoGoAMLDet(strAccNO)
        frmAccountInfoDet.ShowDialog()

    End Sub

    
End Class