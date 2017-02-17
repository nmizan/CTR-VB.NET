'
'Author             : Fahad Khan
'Purpose            : Maintain Person Information
'Creation date      : 13-oct-2013
'Stored Procedure(s):  

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization


Public Class FrmPersonDetail



#Region "Global Variables"

    Dim _formName As String = "MaintenancePersonDetail"
    Dim opt As SecForm = New SecForm(_formName)


    Dim _formMode As FormTransMode
    Dim _strId As String = ""
    Dim _intModno As Integer = 0
    Dim _mod_datetime As Date
    Dim _status As String = ""
    Dim _RowEditMode As Boolean = False
    Dim _IsResetRow As Boolean = False
    Dim log_message As String = ""

    'For Update
    Dim _title As String = ""
    Dim _gender As String = ""
    Dim _fname As String = ""
    Dim _lname As String = ""
    Dim _mname As String = ""
    Dim _birthdate As String = ""
    Dim _nationalID As String = ""
    Dim _passNo As String = ""
    Dim _birthID As String = ""
    Dim _spouse As String = ""
    Dim _mother As String = ""
    Dim _father As String = ""
    Dim _birthplace As String = ""
    Dim _occpation As String = ""
    Dim _residence As String = ""
    Dim _nationality1 As String = ""
    Dim _passCountry As String = ""
    Dim _nationality1Name As String = ""
    Dim _residenceName As String = ""
    Dim _passCountryName As String = ""
    Dim _employerName As String = ""
    Dim _taxNo As String = ""
    Dim _taxReg As String = ""
    Dim _sourceWealth As String = ""
    Dim _comments As String = ""
    Dim _email As String = ""

    'For Auth
    Dim _btitle As String = ""
    Dim _bfname As String = ""
    Dim _blname As String = ""
    Dim _bmname As String = ""
    Dim _bbirthdate As String = ""
    Dim _bnationalID As String = ""
    Dim _bpassNo As String = ""
    Dim _bbirthID As String = ""
    Dim _bspouse As String = ""
    Dim _bmother As String = ""
    Dim _bfather As String = ""
    Dim _bbirthplace As String = ""
    Dim _boccpation As String = ""
    Dim _bresidence As String = ""
    Dim _bnationality1 As String = ""
    Dim _bpassCountry As String = ""
    Dim _bresidenceName As String = ""
    Dim _bnationality1Name As String = ""
    Dim _bpassCountryName As String = ""
    Dim _bemployerName As String = ""
    Dim _btaxNo As String = ""
    Dim _btaxReg As String = ""
    Dim _bsourceWealth As String = ""
    Dim _bcomments As String = ""
    Dim _bemail As String = ""

    Dim PersonList As New List(Of String)
    Dim _personLog As String = ""
    Dim _Plog As String = ""

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
        txtFirstName.ReadOnly = True
        txtTitle.ReadOnly = True
        txtLast.ReadOnly = True
        txtMiddle.ReadOnly = True
        txtPrefix.ReadOnly = True
        txtBirthDate.ReadOnly = True
        txtBirthplace.ReadOnly = True
        txtMother.ReadOnly = True
        txtAlias.ReadOnly = True
        txtSSN.ReadOnly = True
        txtIDNumber.ReadOnly = True
        TxtPassportNo.ReadOnly = True
        cbmPassportCountry.Enabled = False
        cmbNationality1.Enabled = False
        cmbNationality2.Enabled = False
        cmbNationality3.Enabled = False
        cmbResidence.Enabled = False
        txtOccupation.ReadOnly = True
        cmbSex.Enabled = False

        txtEmployerName.ReadOnly = True
        txtSourceWealth.ReadOnly = True

        cmbDecreased.Enabled = False

        txtDeceasedDate.ReadOnly = True
        txtEmail.ReadOnly = True
        txtEmail2.ReadOnly = True
        txtEmail3.ReadOnly = True
        txtComments.ReadOnly = True
        txtTaxRegNumber.ReadOnly = True
        txtTaxNumber.ReadOnly = True

        ' Personal Phone
        TxtPhone.ReadOnly = True

        cmbContact.Enabled = False
        cbmCommunication.Enabled = False
        txtCountryPrefix.ReadOnly = True
        txtExtension.ReadOnly = True

        'Personal Address

        cmbPerAddress.Enabled = False
        cmbPersonalCountry.Enabled = False
        cmbPThana.Enabled = False
        cmbPDistrict.Enabled = False
        cmbPDivision.Enabled = False
        txtAddress.ReadOnly = True
        txtZip.ReadOnly = True



        'Employer Phone

        cmbEmpCommunication.Enabled = False
        cmbEmpContact.Enabled = False
        txtEmpPhone.ReadOnly = True
        txtEmpPrefix.ReadOnly = True
        txtEmpExtension.ReadOnly = True


        'Employer Address

        txtEmployerAddress.ReadOnly = True
        cmbEmpThana.Enabled = False
        cmbEmpDistrict.Enabled = False
        cmbEmpDivision.Enabled = False
        txtEmployerZip.ReadOnly = True
        cmbEmployerContact.Enabled = False
        cmbEmployerCountry.Enabled = False

        'Identification 

        txtExpiryDate.ReadOnly = True

        txtIssuedBy.ReadOnly = True
        txtIssueDate.ReadOnly = True
        txtNumber.ReadOnly = True
      
        cmbIdentification.Enabled = False
        cmbIssueCountry.Enabled = False




        AddToGrid.Enabled = False
        btnRemoveGrid1.Enabled = False
        btnCancelGrid1.Visible = False

        btnAddPersonalAddressGrid.Enabled = False
        btnRemovePersonalAddtoGrid.Enabled = False
        btmCancelAddress.Visible = False

        btnEmpAddtoGrid.Enabled = False
        btnEmpRemoveToGrid.Enabled = False
        btnEmpCanceltoGrid.Visible = False

        btmEnployerAddressAddToGrid.Enabled = False
        btnEmployerRemoveGrid.Enabled = False
        btnCancelEmptoGrid.Visible = False

        btnIdentificationAddToGrid.Enabled = False
        btnRemoveIdentificationToGrid.Enabled = False
        btnCancelIdenticication.Visible = False




    End Sub

    Private Sub EnableFields()

        If _intModno = 0 Then
            txtId.ReadOnly = True
        End If

        txtFirstName.ReadOnly = False
        txtTitle.ReadOnly = False
        txtLast.ReadOnly = False
        txtMiddle.ReadOnly = False
        txtPrefix.ReadOnly = False
        txtBirthDate.ReadOnly = False
        txtBirthplace.ReadOnly = False
        txtMother.ReadOnly = False
        txtAlias.ReadOnly = False
        txtSSN.ReadOnly = False
        txtIDNumber.ReadOnly = False
        TxtPassportNo.ReadOnly = False
        cbmPassportCountry.Enabled = True
        cmbNationality1.Enabled = True
        cmbNationality2.Enabled = True
        cmbNationality3.Enabled = True
        cmbResidence.Enabled = True
        txtOccupation.ReadOnly = False

        txtEmployerName.ReadOnly = False
        txtSourceWealth.ReadOnly = False
        cmbDecreased.Enabled = True
        txtDeceasedDate.ReadOnly = False
        txtEmail.ReadOnly = False
        txtEmail2.ReadOnly = False
        txtEmail3.ReadOnly = False
        txtComments.ReadOnly = False
        txtTaxRegNumber.ReadOnly = False
        txtTaxNumber.ReadOnly = False
        cmbSex.Enabled = True
        ' Personal Phone
        TxtPhone.ReadOnly = False

        cmbContact.Enabled = True
        cbmCommunication.Enabled = True
        txtCountryPrefix.ReadOnly = False
        txtExtension.ReadOnly = False

        'Personal Address
        cmbPThana.Enabled = True
        cmbPDistrict.Enabled = True
        cmbPDivision.Enabled = True
        txtAddress.ReadOnly = False
        txtZip.ReadOnly = False

        cmbPerAddress.Enabled = True
        cmbPersonalCountry.Enabled = True


        'Employer Phone

        txtEmpPhone.ReadOnly = False
        txtEmpPrefix.ReadOnly = False
        txtEmpExtension.ReadOnly = False
        cmbEmpCommunication.Enabled = True
        cmbEmpContact.Enabled = True

        'Employer Address

        txtEmployerAddress.ReadOnly = False
        cmbEmpThana.Enabled = True
        cmbEmpDistrict.Enabled = True
        cmbEmpDivision.Enabled = True
        txtEmployerZip.ReadOnly = False
        cmbEmployerContact.Enabled = True
        cmbEmployerCountry.Enabled = True

        'Identification 

        txtExpiryDate.ReadOnly = False

        txtIssuedBy.ReadOnly = False
        txtIssueDate.ReadOnly = False
        txtNumber.ReadOnly = False
        cmbIdentification.Enabled = True
        cmbIssueCountry.Enabled = True
        

        AddToGrid.Enabled = True
        btnRemoveGrid1.Enabled = True
        btnCancelGrid1.Visible = False

        btnAddPersonalAddressGrid.Enabled = True
        btnRemovePersonalAddtoGrid.Enabled = True
        btmCancelAddress.Visible = False

        btnEmpAddtoGrid.Enabled = True
        btnEmpRemoveToGrid.Enabled = True
        btnEmpCanceltoGrid.Visible = False

        btmEnployerAddressAddToGrid.Enabled = True
        btnEmployerRemoveGrid.Enabled = True
        btnCancelEmptoGrid.Visible = False

        btnIdentificationAddToGrid.Enabled = True
        btnRemoveIdentificationToGrid.Enabled = True
        btnCancelIdenticication.Visible = False
      






    End Sub


    Private Sub ClearFields()

        txtId.Clear()
        txtFirstName.Clear()
        txtTitle.Clear()
        txtLast.Clear()
        txtMiddle.Clear()
        txtPrefix.Clear()
        txtBirthDate.Clear()
        txtBirthplace.Clear()
        txtMother.Clear()
        txtAlias.Clear()
        txtSSN.Clear()
        txtIDNumber.Clear()
        TxtPassportNo.Clear()

        txtOccupation.Clear()

        txtEmployerName.Clear()
        txtSourceWealth.Clear()

        txtDeceasedDate.Clear()
        txtEmail.Clear()
        txtEmail2.Clear()
        txtEmail3.Clear()
        txtComments.Clear()
        txtTaxRegNumber.Clear()
        txtTaxNumber.Clear()
        cmbDecreased.SelectedIndex = -1

        cmbSex.SelectedIndex = -1
        cbmPassportCountry.SelectedIndex = -1
        cmbNationality1.SelectedIndex = -1
        cmbNationality2.SelectedIndex = -1
        cmbNationality3.SelectedIndex = -1
        cmbResidence.SelectedIndex = -1


        ' Personal Phone
        TxtPhone.Clear()

        txtCountryPrefix.Clear()
        txtExtension.Clear()

        cmbContact.SelectedIndex = -1
        cbmCommunication.SelectedIndex = -1

        'Personal Address

        cmbPThana.SelectedIndex = -1
        cmbPDistrict.SelectedIndex = -1
        cmbPDivision.SelectedIndex = -1
        txtAddress.Clear()
        txtZip.Clear()
        cmbPerAddress.SelectedIndex = -1
        cmbPersonalCountry.SelectedIndex = -1

        'Employer Phone

        txtEmpPhone.Clear()
        txtEmpPrefix.Clear()
        txtEmpExtension.Clear()
        cmbEmpCommunication.SelectedIndex = -1
        cmbEmpContact.SelectedIndex = -1

        'Employer Address

        txtEmployerAddress.Clear()
        cmbEmpThana.SelectedIndex = -1
        cmbEmpDistrict.SelectedIndex = -1
        cmbEmpDivision.SelectedIndex = -1
        txtEmployerZip.Clear()
        cmbEmployerContact.SelectedIndex = -1
        cmbEmployerCountry.SelectedIndex = -1
        'Identification 

        txtExpiryDate.Clear()

        txtIssuedBy.Clear()
        txtIssueDate.Clear()
        txtNumber.Clear()
        cmbIdentification.SelectedIndex = -1
        cmbIssueCountry.SelectedIndex = -1


        DataGridView1.AllowUserToAddRows = False
        DataGridView3.AllowUserToAddRows = False
        DataGridView4.AllowUserToAddRows = False
        DataGridView5.AllowUserToAddRows = False
        DataGridView6.AllowUserToAddRows = False

        'dgView.DataSource = Nothing

    End Sub

    Private Sub ClearFieldsAll()

        txtId.Clear()
        txtFirstName.Clear()
        txtTitle.Clear()
        txtLast.Clear()
        txtMiddle.Clear()
        txtPrefix.Clear()
        txtBirthDate.Clear()
        txtBirthplace.Clear()
        txtMother.Clear()
        txtAlias.Clear()
        txtSSN.Clear()
        txtIDNumber.Clear()
        TxtPassportNo.Clear()

        txtOccupation.Clear()

        txtEmployerName.Clear()
        txtSourceWealth.Clear()

        txtDeceasedDate.Clear()
        txtEmail.Clear()
        txtEmail2.Clear()
        txtEmail3.Clear()
        txtComments.Clear()
        txtTaxRegNumber.Clear()
        txtTaxNumber.Clear()
        cmbSex.SelectedIndex = -1
        cbmPassportCountry.SelectedIndex = -1
        cmbNationality1.SelectedIndex = -1
        cmbNationality2.SelectedIndex = -1
        cmbNationality3.SelectedIndex = -1
        cmbResidence.SelectedIndex = -1
        cmbDecreased.SelectedIndex = -1

        ' Personal Phone
        TxtPhone.Clear()

        txtCountryPrefix.Clear()
        txtExtension.Clear()
        cmbContact.SelectedIndex = -1
        cbmCommunication.SelectedIndex = -1

        'Personal Address


        cmbPThana.SelectedIndex = -1
        cmbPDistrict.SelectedIndex = -1
        cmbPDivision.SelectedIndex = -1
        txtAddress.Clear()
        txtZip.Clear()
        cmbPerAddress.SelectedIndex = -1
        cmbPersonalCountry.SelectedIndex = -1

        'Employer Phone

        txtEmpPhone.Clear()
        txtEmpPrefix.Clear()
        txtEmpExtension.Clear()
        cmbEmpCommunication.SelectedIndex = -1
        cmbEmpContact.SelectedIndex = -1

        'Employer Address

        txtEmployerAddress.Clear()
        cmbEmpThana.SelectedIndex = -1
        cmbEmpDistrict.SelectedIndex = -1
        cmbEmpDivision.SelectedIndex = -1
        txtEmployerZip.Clear()
        cmbEmployerContact.SelectedIndex = -1
        cmbEmployerCountry.SelectedIndex = -1
        'Identification 

        txtExpiryDate.Clear()

        txtIssuedBy.Clear()
        txtIssueDate.Clear()
        txtNumber.Clear()
        cmbIdentification.SelectedIndex = -1
        cmbIssueCountry.SelectedIndex = -1

        DataGridView1.AllowUserToAddRows = False
        DataGridView3.AllowUserToAddRows = False
        DataGridView4.AllowUserToAddRows = False
        DataGridView5.AllowUserToAddRows = False
        DataGridView6.AllowUserToAddRows = False

        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()

        DataGridView3.DataSource = Nothing
        DataGridView3.Rows.Clear()

        DataGridView4.DataSource = Nothing
        DataGridView4.Rows.Clear()

        DataGridView5.DataSource = Nothing
        DataGridView5.Rows.Clear()

        DataGridView6.DataSource = Nothing
        DataGridView6.Rows.Clear()



        _strId = ""
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

    Private Function CheckValidData() As Boolean


        'If txtId.Text.Trim() = "" Then
        '    MessageBox.Show("Person ID required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    txtId.Focus()
        '    Return False



        'Else
        If txtFirstName.Text.Trim() = "" Then
            MessageBox.Show("Person First Name required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtFirstName.Focus()
            Return False
        ElseIf txtLast.Text.Trim() = "" Then
            MessageBox.Show("Person Last Name required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtLast.Focus()
            Return False
            'ElseIf txtIDNumber.Text.Trim() = "" Then
            '    MessageBox.Show("Person Identification Number required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    txtIDNumber.Focus()
            '    Return False




        End If


        Return True

    End Function

    Private Function SaveData() As TransState


        Dim tStatus As TransState

        Dim intModno As Integer = 0
        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        If _formMode = FormTransMode.Add Then

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()


                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_MAX_ID")

                commProc2.Parameters.Clear()
                Dim maxid As String = db.ExecuteDataSet(commProc2, trans).Tables(0).Rows(0)(0).ToString()

                txtId.Text = maxid + 1


                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_ReportingTPerson_Add")

                commProc.Parameters.Clear()


                db.AddInParameter(commProc, "@PERSON_ID", DbType.String, txtId.Text)

                Dim M As String = "M"

                If cmbSex.Text = "" Then
                    db.AddInParameter(commProc, "@GENDER", DbType.String, M)
                Else
                    db.AddInParameter(commProc, "@GENDER", DbType.String, cmbSex.Text)

                End If

                'If cmbSex.Text = "" Then
                '    db.AddInParameter(commProc, "@GENDER", DbType.String, 0)
                'Else
                '    db.AddInParameter(commProc, "@GENDER", DbType.String, cmbSex.SelectedIndex)

                'End If


                db.AddInParameter(commProc, "@TITLE", DbType.String, txtTitle.Text)
                db.AddInParameter(commProc, "@FIRST_NAME", DbType.String, txtFirstName.Text)
                db.AddInParameter(commProc, "@MIDDLE_NAME", DbType.String, txtMiddle.Text)
                db.AddInParameter(commProc, "@PREFIX", DbType.String, txtPrefix.Text)
                db.AddInParameter(commProc, "@LAST_NAME", DbType.String, txtLast.Text)
                
                db.AddInParameter(commProc, "@BIRTHDATE", DbType.DateTime, NullHelper.StringToDate(txtBirthDate.Text))

                db.AddInParameter(commProc, "@BIRTH_PLACE", DbType.String, txtBirthplace.Text)
                db.AddInParameter(commProc, "@MOTHERS_NAME", DbType.String, txtMother.Text)
                db.AddInParameter(commProc, "@ALIAS", DbType.String, txtAlias.Text)
                db.AddInParameter(commProc, "@SSN", DbType.String, txtSSN.Text)
                db.AddInParameter(commProc, "@PASSPORT_NUMBER", DbType.String, TxtPassportNo.Text)
                db.AddInParameter(commProc, "@PASSPORT_COUNTRY", DbType.String, cbmPassportCountry.SelectedValue)
                db.AddInParameter(commProc, "@ID_NUMBER", DbType.String, txtIDNumber.Text)
                db.AddInParameter(commProc, "@NATIONALITY1", DbType.String, cmbNationality1.SelectedValue)
                db.AddInParameter(commProc, "@NATIONALITY2", DbType.String, cmbNationality2.SelectedValue)
                db.AddInParameter(commProc, "@NATIONALITY3", DbType.String, cmbNationality3.SelectedValue)
                db.AddInParameter(commProc, "@RESIDENCE", DbType.String, cmbResidence.SelectedValue)
                db.AddInParameter(commProc, "@EMAIL", DbType.String, txtEmail.Text)
                db.AddInParameter(commProc, "@EMAIL2", DbType.String, txtEmail2.Text)
                db.AddInParameter(commProc, "@EMAIL3", DbType.String, txtEmail3.Text)
                db.AddInParameter(commProc, "@OCCUPATION", DbType.String, txtOccupation.Text)
                db.AddInParameter(commProc, "@EMPLOYER_NAME", DbType.String, txtEmployerName.Text)

                If cmbDecreased.Text.Trim() = "" Or cmbDecreased.SelectedIndex = -1 Or cmbDecreased.SelectedIndex = 1 Then

                    db.AddInParameter(commProc, "@DECEASED", DbType.String, 0)
                Else
                    db.AddInParameter(commProc, "@DECEASED", DbType.String, 1)

                End If


                'If cmbDecreased.Text = "" Then
                '    db.AddInParameter(commProc, "@DECEASED", DbType.String, 1)
                'Else

                '    db.AddInParameter(commProc, "@DECEASED", DbType.String, cmbDecreased.SelectedIndex)
                'End If

                db.AddInParameter(commProc, "@DECEASED_DATE", DbType.DateTime, NullHelper.StringToDate(txtDeceasedDate.Text))



                db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, txtTaxNumber.Text)
                db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, txtTaxRegNumber.Text)
                db.AddInParameter(commProc, "@SOURCE_OF_WEALTH", DbType.String, txtSourceWealth.Text)
                db.AddInParameter(commProc, "@COMMENTS", DbType.String, txtComments.Text)


                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                Dim result As Integer


                db.ExecuteNonQuery(commProc, trans)
                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                'If result = 0 Then
                '    tStatus = TransState.Add

                '    _strId = txtId.Text.Trim()

                '    _intModno = 1

                'Else
                '    tStatus = TransState.Exist
                '    trans.Rollback()
                'End If

                If result <> 0 Then
                    tStatus = TransState.Exist
                    trans.Rollback()
                    Return tStatus

                Else

                    ' Add Personal Phone

                    Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_RPersonPhone_Add")

                    For i = 0 To DataGridView1.Rows.Count - 1

                        commProcSche.Parameters.Clear()


                        db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, DataGridView1.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcSche, "@PERSON_ID", DbType.String, txtId.Text.Trim())
                        db.AddInParameter(commProcSche, "@SID", DbType.String, DataGridView1.Rows(i).Cells(7).Value)
                        db.AddInParameter(commProcSche, "@SLNO", DbType.String, DataGridView1.Rows(i).Cells(6).Value)
                        db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, DataGridView1.Rows(i).Cells(1).Value)
                        db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, DataGridView1.Rows(i).Cells(2).Value)
                        db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, DataGridView1.Rows(i).Cells(3).Value)
                        db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, DataGridView1.Rows(i).Cells(4).Value)
                        db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, DataGridView1.Rows(i).Cells(5).Value)

                        db.AddParameter(commProcSche, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcSche, trans)

                        If db.GetParameterValue(commProcSche, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If


                    Next

                    ' Add Person Address 

                    Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_RPersonAddress_Add")

                    For i = 0 To DataGridView4.Rows.Count - 1

                        commProcAdd.Parameters.Clear()


                        db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, DataGridView4.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcAdd, "@PERSON_ID", DbType.String, txtId.Text.Trim())
                        db.AddInParameter(commProcAdd, "@SID", DbType.String, DataGridView4.Rows(i).Cells(9).Value)
                        db.AddInParameter(commProcAdd, "@SLNO", DbType.String, DataGridView4.Rows(i).Cells(8).Value)
                        db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, DataGridView4.Rows(i).Cells(1).Value)
                        db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, DataGridView4.Rows(i).Cells(2).Value)
                        db.AddInParameter(commProcAdd, "@TOWN", DbType.String, DataGridView4.Rows(i).Cells(3).Value)
                        db.AddInParameter(commProcAdd, "@CITY", DbType.String, DataGridView4.Rows(i).Cells(4).Value)
                        db.AddInParameter(commProcAdd, "@ZIP", DbType.String, DataGridView4.Rows(i).Cells(5).Value)
                        db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, DataGridView4.Rows(i).Cells(6).Value)
                        db.AddInParameter(commProcAdd, "@STATE", DbType.String, DataGridView4.Rows(i).Cells(7).Value)

                        db.AddParameter(commProcAdd, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcAdd, trans)

                        If db.GetParameterValue(commProcAdd, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If


                    Next

                    ' Add Employer Phone

                    Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_RPersonPhone_Add")

                    For i = 0 To DataGridView3.Rows.Count - 1

                        commProcEMPhone.Parameters.Clear()


                        db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, DataGridView3.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcEMPhone, "@PERSON_ID", DbType.String, txtId.Text.Trim())
                        db.AddInParameter(commProcEMPhone, "@SID", DbType.String, DataGridView3.Rows(i).Cells(7).Value)
                        db.AddInParameter(commProcEMPhone, "@SLNO", DbType.String, DataGridView3.Rows(i).Cells(6).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, DataGridView3.Rows(i).Cells(1).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, DataGridView3.Rows(i).Cells(2).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, DataGridView3.Rows(i).Cells(3).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, DataGridView3.Rows(i).Cells(4).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, DataGridView3.Rows(i).Cells(5).Value)

                        db.AddParameter(commProcEMPhone, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcEMPhone, trans)

                        If db.GetParameterValue(commProcEMPhone, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If


                    Next

                    'Add Employer Address
                    Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_RPersonAddress_Add")

                    For i = 0 To DataGridView5.Rows.Count - 1

                        commProcEMAdd.Parameters.Clear()


                        db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, DataGridView5.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcEMAdd, "@PERSON_ID", DbType.String, txtId.Text.Trim())
                        db.AddInParameter(commProcEMAdd, "@SID", DbType.String, DataGridView5.Rows(i).Cells(9).Value)
                        db.AddInParameter(commProcEMAdd, "@SLNO", DbType.String, DataGridView5.Rows(i).Cells(8).Value)
                        db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, DataGridView5.Rows(i).Cells(1).Value)
                        db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, DataGridView5.Rows(i).Cells(2).Value)
                        db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, DataGridView5.Rows(i).Cells(3).Value)
                        db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, DataGridView5.Rows(i).Cells(4).Value)
                        db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, DataGridView5.Rows(i).Cells(5).Value)
                        db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, DataGridView5.Rows(i).Cells(6).Value)
                        db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, DataGridView5.Rows(i).Cells(7).Value)

                        db.AddParameter(commProcEMAdd, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcEMAdd, trans)

                        If db.GetParameterValue(commProcEMAdd, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If


                    Next


                    'Add Identification

                    Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_RPersonIdentification_Add")

                    For i = 0 To DataGridView6.Rows.Count - 1

                        commProcIdent.Parameters.Clear()


                        db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, DataGridView6.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcIdent, "@PERSON_ID", DbType.String, txtId.Text.Trim())
                        db.AddInParameter(commProcIdent, "@SID", DbType.String, DataGridView6.Rows(i).Cells(8).Value)
                        db.AddInParameter(commProcIdent, "@SLNO", DbType.String, DataGridView6.Rows(i).Cells(7).Value)
                        db.AddInParameter(commProcIdent, "@TYPE", DbType.String, DataGridView6.Rows(i).Cells(1).Value)
                        db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, DataGridView6.Rows(i).Cells(2).Value)
                        db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, DataGridView6.Rows(i).Cells(3).Value)
                        db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, DataGridView6.Rows(i).Cells(4).Value)
                        db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, DataGridView6.Rows(i).Cells(5).Value)
                        db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, DataGridView6.Rows(i).Cells(6).Value)


                        db.AddParameter(commProcIdent, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcIdent, trans)

                        If db.GetParameterValue(commProcIdent, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If


                    Next



                    tStatus = TransState.Add
                    _strId = txtId.Text.Trim()
                    _intModno = 1

                    log_message = "Added : Reporting Person : " + txtId.Text.Trim() + "." + " " + " Name : " + txtTitle.Text.ToString() + " " + txtFirstName.Text.ToString() + " " + txtMiddle.Text.ToString() + " " + txtLast.Text.ToString()
                    Logger.system_log(log_message)

                End If


                trans.Commit()

                
            End Using


        ElseIf _formMode = FormTransMode.Update Then



            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_RPerson_Update")

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@PERSON_ID", DbType.String, _strId)

                Dim M As String = "M"

                If cmbSex.Text = "" Then
                    db.AddInParameter(commProc, "@GENDER", DbType.String, M)
                Else
                    db.AddInParameter(commProc, "@GENDER", DbType.String, cmbSex.Text)

                End If


                db.AddInParameter(commProc, "@TITLE", DbType.String, txtTitle.Text)
                db.AddInParameter(commProc, "@FIRST_NAME", DbType.String, txtFirstName.Text)
                db.AddInParameter(commProc, "@MIDDLE_NAME", DbType.String, txtMiddle.Text)
                db.AddInParameter(commProc, "@PREFIX", DbType.String, txtPrefix.Text)
                db.AddInParameter(commProc, "@LAST_NAME", DbType.String, txtLast.Text)

                db.AddInParameter(commProc, "@BIRTHDATE", DbType.DateTime, NullHelper.StringToDate(txtBirthDate.Text))

                db.AddInParameter(commProc, "@BIRTH_PLACE", DbType.String, txtBirthplace.Text)
                db.AddInParameter(commProc, "@MOTHERS_NAME", DbType.String, txtMother.Text)
                db.AddInParameter(commProc, "@ALIAS", DbType.String, txtAlias.Text)
                db.AddInParameter(commProc, "@SSN", DbType.String, txtSSN.Text)
                db.AddInParameter(commProc, "@PASSPORT_NUMBER", DbType.String, TxtPassportNo.Text)
                db.AddInParameter(commProc, "@PASSPORT_COUNTRY", DbType.String, cbmPassportCountry.SelectedValue)
                db.AddInParameter(commProc, "@ID_NUMBER", DbType.String, txtIDNumber.Text)
                db.AddInParameter(commProc, "@NATIONALITY1", DbType.String, cmbNationality1.SelectedValue)
                db.AddInParameter(commProc, "@NATIONALITY2", DbType.String, cmbNationality2.SelectedValue)
                db.AddInParameter(commProc, "@NATIONALITY3", DbType.String, cmbNationality3.SelectedValue)
                db.AddInParameter(commProc, "@RESIDENCE", DbType.String, cmbResidence.SelectedValue)
                db.AddInParameter(commProc, "@EMAIL", DbType.String, txtEmail.Text)
                db.AddInParameter(commProc, "@EMAIL2", DbType.String, txtEmail2.Text)
                db.AddInParameter(commProc, "@EMAIL3", DbType.String, txtEmail3.Text)
                db.AddInParameter(commProc, "@OCCUPATION", DbType.String, txtOccupation.Text)
                db.AddInParameter(commProc, "@EMPLOYER_NAME", DbType.String, txtEmployerName.Text)

                If cmbDecreased.Text.Trim() = "" Or cmbDecreased.SelectedIndex = -1 Or cmbDecreased.SelectedIndex = 1 Then

                    db.AddInParameter(commProc, "@DECEASED", DbType.String, 0)
                Else
                    db.AddInParameter(commProc, "@DECEASED", DbType.String, 1)

                End If

                'If cmbDecreased.Text = "" Then
                '    db.AddInParameter(commProc, "@DECEASED", DbType.String, 1)
                'Else

                '    db.AddInParameter(commProc, "@DECEASED", DbType.String, cmbDecreased.SelectedIndex)
                'End If

                db.AddInParameter(commProc, "@DECEASED_DATE", DbType.DateTime, NullHelper.StringToDate(txtDeceasedDate.Text))



                db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, txtTaxNumber.Text)
                db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, txtTaxRegNumber.Text)
                db.AddInParameter(commProc, "@SOURCE_OF_WEALTH", DbType.String, txtSourceWealth.Text)
                db.AddInParameter(commProc, "@COMMENTS", DbType.String, txtComments.Text)


                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
                db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)

                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                Dim result As Integer

                db.ExecuteNonQuery(commProc, trans)
                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")

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
                End If

                ' Update Person Phone
                Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_RPersonalPhone_Update")


                For i = 0 To DataGridView1.Rows.Count - 1

                    commProcSche.Parameters.Clear()


                    db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, DataGridView1.Rows(i).Cells(0).Value)
                    db.AddInParameter(commProcSche, "@PERSON_ID", DbType.String, _strId)
                    db.AddInParameter(commProcSche, "@SID", DbType.String, DataGridView1.Rows(i).Cells(7).Value)
                    db.AddInParameter(commProcSche, "@SLNO", DbType.String, DataGridView1.Rows(i).Cells(6).Value)
                    db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, DataGridView1.Rows(i).Cells(1).Value)
                    db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, DataGridView1.Rows(i).Cells(2).Value)
                    db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, DataGridView1.Rows(i).Cells(3).Value)
                    db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, DataGridView1.Rows(i).Cells(4).Value)
                    db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, DataGridView1.Rows(i).Cells(5).Value)
                    db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, intModno)




                    db.AddParameter(commProcSche, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                    db.ExecuteNonQuery(commProcSche, trans)

                    If db.GetParameterValue(commProcSche, "@PROC_RET_VAL") <> 0 Then

                        trans.Rollback()
                        Return TransState.UnspecifiedError

                    End If


                Next

                ' Update Person Address 

                Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_RPersonAddress_Update")

                For i = 0 To DataGridView4.Rows.Count - 1

                    commProcAdd.Parameters.Clear()


                    db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, DataGridView4.Rows(i).Cells(0).Value)
                    db.AddInParameter(commProcAdd, "@PERSON_ID", DbType.String, _strId)
                    db.AddInParameter(commProcAdd, "@SID", DbType.String, DataGridView4.Rows(i).Cells(9).Value)
                    db.AddInParameter(commProcAdd, "@SLNO", DbType.String, DataGridView4.Rows(i).Cells(8).Value)
                    db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, DataGridView4.Rows(i).Cells(1).Value)
                    db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, DataGridView4.Rows(i).Cells(2).Value)
                    db.AddInParameter(commProcAdd, "@TOWN", DbType.String, DataGridView4.Rows(i).Cells(3).Value)
                    db.AddInParameter(commProcAdd, "@CITY", DbType.String, DataGridView4.Rows(i).Cells(4).Value)
                    db.AddInParameter(commProcAdd, "@ZIP", DbType.String, DataGridView4.Rows(i).Cells(5).Value)
                    db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, DataGridView4.Rows(i).Cells(6).Value)
                    db.AddInParameter(commProcAdd, "@STATE", DbType.String, DataGridView4.Rows(i).Cells(7).Value)
                    db.AddInParameter(commProcAdd, "@MOD_NO", DbType.Int32, intModno)

                    db.AddParameter(commProcAdd, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                    db.ExecuteNonQuery(commProcAdd, trans)

                    If db.GetParameterValue(commProcAdd, "@PROC_RET_VAL") <> 0 Then

                        trans.Rollback()
                        Return TransState.UnspecifiedError

                    End If


                Next

                '' Update Employer Phone

                Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_RPersonalPhone_Update")

                For i = 0 To DataGridView3.Rows.Count - 1

                    commProcEMPhone.Parameters.Clear()

                    If (Not (String.IsNullOrEmpty(DataGridView3.Rows(i).Cells(0).Value.ToString()))) Then  '-----Mizan Work (11-04-2016)

                        db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, DataGridView3.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcEMPhone, "@PERSON_ID", DbType.String, _strId)
                        db.AddInParameter(commProcEMPhone, "@SID", DbType.String, DataGridView3.Rows(i).Cells(7).Value)
                        db.AddInParameter(commProcEMPhone, "@SLNO", DbType.String, DataGridView3.Rows(i).Cells(6).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, DataGridView3.Rows(i).Cells(1).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, DataGridView3.Rows(i).Cells(2).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, DataGridView3.Rows(i).Cells(3).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, DataGridView3.Rows(i).Cells(4).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, DataGridView3.Rows(i).Cells(5).Value)
                        db.AddInParameter(commProcEMPhone, "@MOD_NO", DbType.Int32, intModno)

                        db.AddParameter(commProcEMPhone, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcEMPhone, trans)

                        If db.GetParameterValue(commProcEMPhone, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If
                    End If


                Next

                ''UPDATE Employer Address
                Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_RPersonAddress_Update")

                For i = 0 To DataGridView5.Rows.Count - 1

                    commProcEMAdd.Parameters.Clear()

                    If (Not (String.IsNullOrEmpty(DataGridView5.Rows(i).Cells(0).Value.ToString()))) Then  '-----Mizan Work (11-04-2016)

                        db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, DataGridView5.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcEMAdd, "@PERSON_ID", DbType.String, _strId)
                        db.AddInParameter(commProcEMAdd, "@SID", DbType.String, DataGridView5.Rows(i).Cells(9).Value)
                        db.AddInParameter(commProcEMAdd, "@SLNO", DbType.String, DataGridView5.Rows(i).Cells(8).Value)
                        db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, DataGridView5.Rows(i).Cells(1).Value)
                        db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, DataGridView5.Rows(i).Cells(2).Value)
                        db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, DataGridView5.Rows(i).Cells(3).Value)
                        db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, DataGridView5.Rows(i).Cells(4).Value)
                        db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, DataGridView5.Rows(i).Cells(5).Value)
                        db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, DataGridView5.Rows(i).Cells(6).Value)
                        db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, DataGridView5.Rows(i).Cells(7).Value)
                        db.AddInParameter(commProcEMAdd, "@MOD_NO", DbType.Int32, intModno)

                        db.AddParameter(commProcEMAdd, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcEMAdd, trans)

                        If db.GetParameterValue(commProcEMAdd, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If
                    End If


                Next


                ''Update(Identification)

                Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_RPersonIdentification_Update")

                For i = 0 To DataGridView6.Rows.Count - 1

                    commProcIdent.Parameters.Clear()


                    db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, DataGridView6.Rows(i).Cells(0).Value)
                    db.AddInParameter(commProcIdent, "@PERSON_ID", DbType.String, _strId)
                    db.AddInParameter(commProcIdent, "@SID", DbType.String, DataGridView6.Rows(i).Cells(8).Value)
                    db.AddInParameter(commProcIdent, "@SLNO", DbType.String, DataGridView6.Rows(i).Cells(7).Value)
                    db.AddInParameter(commProcIdent, "@TYPE", DbType.String, DataGridView6.Rows(i).Cells(1).Value)
                    db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, DataGridView6.Rows(i).Cells(2).Value)
                    db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, DataGridView6.Rows(i).Cells(3).Value)
                    db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, DataGridView6.Rows(i).Cells(4).Value)
                    db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, DataGridView6.Rows(i).Cells(5).Value)
                    db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, DataGridView6.Rows(i).Cells(6).Value)
                    db.AddInParameter(commProcIdent, "@MOD_NO", DbType.Int32, intModno)

                    db.AddParameter(commProcIdent, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                    db.ExecuteNonQuery(commProcIdent, trans)

                    If db.GetParameterValue(commProcIdent, "@PROC_RET_VAL") <> 0 Then

                        trans.Rollback()
                        Return TransState.UnspecifiedError

                    End If


                Next


                tStatus = TransState.Update
                _intModno = intModno

                trans.Commit()

                '--------------Mizan Work (20-04-16)---------------------

                If _title <> txtTitle.Text.Trim() Then
                    If _title = "" Then
                        log_message = " Title : " + txtTitle.Text.ToString() + "." + " "

                    Else
                        log_message = " Title : " + _title + " " + " To " + " " + txtTitle.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
               
                If _fname <> txtFirstName.Text.Trim() Then
                    log_message = " First Name : " + _fname + " " + " To " + " " + txtFirstName.Text.ToString() + "." + " "
                    PersonList.Add(log_message)
                End If
                If _mname <> txtMiddle.Text.Trim() Then
                    If _mname = "" Then
                        log_message = " Middle Name : " + txtMiddle.Text.ToString() + "." + " "

                    Else
                        log_message = " Middle Name : " + _mname + " " + " To " + " " + txtMiddle.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _lname <> txtLast.Text.Trim() Then
                    log_message = " Last Name " + _lname + " " + " To " + " " + txtLast.Text.ToString() + "." + " "
                    PersonList.Add(log_message)
                End If
                If _spouse <> txtPrefix.Text.Trim() Then
                    If _spouse = "" Then
                        log_message = " Spouse : " + txtPrefix.Text.ToString() + "." + " "

                    Else
                        log_message = " Spouse  : " + _spouse + " " + " To " + " " + txtPrefix.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _birthdate <> txtBirthDate.Text.ToString() Then
                    If _birthdate = "" Then
                        log_message = " Birth date:  " + txtBirthDate.Text.ToString() + "." + " "
                    Else
                        log_message = " Birth date:  " + _birthdate + " " + " To " + " " + txtBirthDate.Text.ToString() + "." + " "
                    End If

                    PersonList.Add(log_message)
                End If
                If _birthplace <> txtBirthplace.Text.Trim() Then
                    If _birthplace = "" Then
                        log_message = " Birth Place : " + txtBirthplace.Text.ToString() + "." + " "

                    Else
                        log_message = " Birth Place : " + _birthplace + " " + " To " + " " + txtBirthplace.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _mother <> txtMother.Text.Trim() Then
                    If _mother = "" Then
                        log_message = " Mother : " + txtMother.Text.ToString() + "." + " "

                    Else
                        log_message = " Mother : " + _mother + " " + " To " + " " + txtMother.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _father <> txtAlias.Text.Trim() Then
                    If _father = "" Then
                        log_message = " Father : " + txtAlias.Text.ToString() + "." + " "

                    Else
                        log_message = " Father : " + _father + " " + " To " + " " + txtAlias.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _nationalID <> txtSSN.Text.Trim() Then
                    If _nationalID = "" Then
                        log_message = " National ID : " + txtSSN.Text.ToString() + "." + " "

                    Else
                        log_message = " National ID : " + _nationalID + " " + " To " + " " + txtSSN.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _passNo <> TxtPassportNo.Text.Trim() Then
                    If _passNo = "" Then
                        log_message = " Passport No : " + TxtPassportNo.Text.ToString() + "." + " "

                    Else
                        log_message = " Passport No  : " + _passNo + " " + " To " + " " + TxtPassportNo.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _passCountry <> cbmPassportCountry.Text Then
                    If _passCountry = "" Then
                        log_message = " Passport Country  : " + cbmPassportCountry.Text + "." + " "
                    Else
                        log_message = " Passport Country  : " + _passCountry + " " + " To " + " " + cbmPassportCountry.Text + "." + " "
                    End If
                    PersonList.Add(log_message)
                End If

                If _birthID <> txtIDNumber.Text.Trim() Then
                    If _birthID = "" Then
                        log_message = " Birth Reg ID : " + txtIDNumber.Text.ToString() + "." + " "

                    Else
                        log_message = "  Birth Reg ID : " + _birthID + " " + " To " + " " + txtIDNumber.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _nationality1 <> cmbNationality1.Text Then
                    If _nationality1 = "" Then
                        log_message = " Nationality 1 : " + cmbNationality1.Text + "." + " "
                    Else
                        log_message = " Nationality 1 : " + _nationality1 + " " + " To " + " " + cmbNationality1.Text + "." + " "
                    End If

                    PersonList.Add(log_message)
                End If
                If _residence <> cmbResidence.Text Then
                    If _residence = "" Then
                        log_message = "  Residence : " + cmbResidence.Text + "." + " "
                    Else
                        log_message = "  Residence : " + _residence + " " + " To " + " " + cmbResidence.Text + "." + " "
                    End If

                    PersonList.Add(log_message)
                End If

                If _occpation <> txtOccupation.Text.Trim() Then
                    If _occpation = "" Then
                        log_message = " Occupation : " + txtOccupation.Text.ToString() + "." + " "

                    Else
                        log_message = "   Occupation : " + _occpation + " " + " To " + " " + txtOccupation.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _employerName <> txtEmployerName.Text.Trim() Then
                    If _employerName = "" Then
                        log_message = " Employer Name : " + txtEmployerName.Text.ToString() + "." + " "

                    Else
                        log_message = "  Employer Name : " + _employerName + " " + " To " + " " + txtEmployerName.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _email <> txtEmail.Text.Trim() Then
                    If _email = "" Then
                        log_message = " Email : " + txtEmail.Text.ToString() + "." + " "

                    Else
                        log_message = "  Email : " + _email + " " + " To " + " " + txtEmail.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _taxNo <> txtTaxNumber.Text.Trim() Then
                    If _taxNo = "" Then
                        log_message = " Tax No : " + txtTaxNumber.Text.ToString() + "." + " "

                    Else
                        log_message = " Tax No : " + _taxNo + " " + " To " + " " + txtTaxNumber.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _taxReg <> txtTaxRegNumber.Text.Trim() Then
                    If _taxReg = "" Then
                        log_message = " Tax Reg No : " + txtTaxRegNumber.Text.ToString() + "." + " "

                    Else
                        log_message = " Tax Reg No : " + _taxReg + " " + " To " + " " + txtTaxRegNumber.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _sourceWealth <> txtSourceWealth.Text.Trim() Then
                    If _sourceWealth = "" Then
                        log_message = " Source wealth : " + txtSourceWealth.Text.ToString() + "." + " "

                    Else
                        log_message = " Source wealth : " + _sourceWealth + " " + " To " + " " + txtSourceWealth.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _comments <> txtComments.Text.Trim() Then
                    If _comments = "" Then
                        log_message = " Comments : " + txtComments.Text.ToString() + "." + " "

                    Else
                        log_message = " Comments : " + _comments + " " + " To " + " " + txtComments.Text.ToString() + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If

                For Each perlist As String In PersonList
                    _personLog += perlist
                Next

                _Plog = " Updated : Reporting Person : " + txtId.Text.ToString() + "." + " " + _personLog

                Logger.system_log(_Plog)
                _personLog = ""
                PersonList.Clear()

                '--------------Mizan Work (20-04-16)---------------------


                'log_message = "Updated Reporting Person " + txtId.Text.Trim() + " Name " + txtFirstName.Text.ToString() + " " + txtLast.Text.ToString()
                'Logger.system_log(log_message)

                Return tStatus

                trans.Rollback()

            End Using

        End If

        Return tStatus


    End Function

    '--------------Mizan Work (20-04-16)---------------------

    Private Sub LoadMainDataForAuth(ByVal strId As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From  GO_REPORT_PERSON Where PERSON_ID ='" & strId & "' and STATUS ='L' ")
           
            If ds.Tables(0).Rows.Count > 0 Then

                _strId = strId


                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("PERSON_ID").ToString()
                txtTitle.Text = ds.Tables(0).Rows(0)("TITLE").ToString()
                _btitle = ds.Tables(0).Rows(0)("TITLE").ToString()
                txtFirstName.Text = ds.Tables(0).Rows(0)("FIRST_NAME").ToString()
                _bfname = ds.Tables(0).Rows(0)("FIRST_NAME").ToString()
                txtMiddle.Text = ds.Tables(0).Rows(0)("MIDDLE_NAME").ToString()
                _bmname = ds.Tables(0).Rows(0)("MIDDLE_NAME").ToString()
                txtLast.Text = ds.Tables(0).Rows(0)("LAST_NAME").ToString()
                _blname = ds.Tables(0).Rows(0)("LAST_NAME").ToString()
                txtPrefix.Text = ds.Tables(0).Rows(0)("PREFIX").ToString()
                _bspouse = ds.Tables(0).Rows(0)("PREFIX").ToString()


                txtBirthDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("BIRTHDATE"))
                _bbirthdate = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("BIRTHDATE"))
                txtBirthplace.Text = ds.Tables(0).Rows(0)("BIRTH_PLACE").ToString()
                _bbirthplace = ds.Tables(0).Rows(0)("BIRTH_PLACE").ToString()
                txtMother.Text = ds.Tables(0).Rows(0)("MOTHERS_NAME").ToString()
                _bmother = ds.Tables(0).Rows(0)("MOTHERS_NAME").ToString()
                txtAlias.Text = ds.Tables(0).Rows(0)("ALIAS").ToString()
                _bfather = ds.Tables(0).Rows(0)("ALIAS").ToString()
                txtSSN.Text = ds.Tables(0).Rows(0)("SSN").ToString()
                _bnationalID = ds.Tables(0).Rows(0)("SSN").ToString()
                TxtPassportNo.Text = ds.Tables(0).Rows(0)("PASSPORT_NUMBER").ToString()
                _bpassNo = ds.Tables(0).Rows(0)("PASSPORT_NUMBER").ToString()

                txtIDNumber.Text = ds.Tables(0).Rows(0)("ID_NUMBER").ToString()
                _bbirthID = ds.Tables(0).Rows(0)("ID_NUMBER").ToString()

                ''cbmPassportCountry.SelectedValue = ds.Tables(0).Rows(0)("PASSPORT_COUNTRY")
                _bpassCountry = ds.Tables(0).Rows(0)("PASSPORT_COUNTRY").ToString()

                ''cmbNationality1.SelectedValue = ds.Tables(0).Rows(0)("NATIONALITY1")
                _bnationality1 = ds.Tables(0).Rows(0)("NATIONALITY1").ToString()

                ''cmbResidence.SelectedValue = ds.Tables(0).Rows(0)("RESIDENCE")
                _bresidence = ds.Tables(0).Rows(0)("RESIDENCE").ToString()

                txtOccupation.Text = ds.Tables(0).Rows(0)("OCCUPATION").ToString()
                _boccpation = ds.Tables(0).Rows(0)("OCCUPATION").ToString()

                txtEmployerName.Text = ds.Tables(0).Rows(0)("EMPLOYER_NAME").ToString()
                _bemployerName = ds.Tables(0).Rows(0)("EMPLOYER_NAME").ToString()


                txtEmail.Text = ds.Tables(0).Rows(0)("EMAIL").ToString()
                _bemail = ds.Tables(0).Rows(0)("EMAIL").ToString()

                txtTaxNumber.Text = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()
                _btaxNo = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()

                txtTaxRegNumber.Text = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString()
                _btaxReg = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString()
                txtSourceWealth.Text = ds.Tables(0).Rows(0)("SOURCE_OF_WEALTH").ToString()
                _bsourceWealth = ds.Tables(0).Rows(0)("SOURCE_OF_WEALTH").ToString()
                txtComments.Text = ds.Tables(0).Rows(0)("COMMENTS").ToString()
                _bcomments = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                '--------------Mizan Work (26-04-2016-----------
                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE = '" & _bnationality1 & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _bnationality1Name = ds3.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _bnationality1 = _bnationality1Name

                End If

                Dim ds4 As New DataSet
                ds4 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE ='" & _bresidence & "' ")
                If ds4.Tables(0).Rows.Count > 0 Then

                    _bresidenceName = ds4.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _bresidence = _bresidenceName

                End If

                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE ='" & _bpassCountry & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _bpassCountryName = ds2.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _bpassCountry = _bpassCountryName

                End If
                '--------------Mizan Work (26-04-2016---------
               
                
            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub LoadMainData(ByVal strId As String, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_RPerson_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@PERSON_ID", DbType.String, strId)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intmod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then

                _strId = strId
                _intModno = intmod

                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("PERSON_ID").ToString()

                If ds.Tables(0).Rows(0)("GENDER").ToString() = "M" Then
                    cmbSex.Text = "MALE"
                Else
                    cmbSex.Text = "FEMALE"
                End If
                txtTitle.Text = ds.Tables(0).Rows(0)("TITLE").ToString()
                _title = ds.Tables(0).Rows(0)("TITLE").ToString()
                txtFirstName.Text = ds.Tables(0).Rows(0)("FIRST_NAME").ToString()
                _fname = ds.Tables(0).Rows(0)("FIRST_NAME").ToString()
                txtMiddle.Text = ds.Tables(0).Rows(0)("MIDDLE_NAME").ToString()
                _mname = ds.Tables(0).Rows(0)("MIDDLE_NAME").ToString()
                txtLast.Text = ds.Tables(0).Rows(0)("LAST_NAME").ToString()
                _lname = ds.Tables(0).Rows(0)("LAST_NAME").ToString()
                txtPrefix.Text = ds.Tables(0).Rows(0)("PREFIX").ToString()
                _spouse = ds.Tables(0).Rows(0)("PREFIX").ToString()


                txtBirthDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("BIRTHDATE"))
                _birthdate = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("BIRTHDATE"))
                txtBirthplace.Text = ds.Tables(0).Rows(0)("BIRTH_PLACE").ToString()
                _birthplace = ds.Tables(0).Rows(0)("BIRTH_PLACE").ToString()
                txtMother.Text = ds.Tables(0).Rows(0)("MOTHERS_NAME").ToString()
                _mother = ds.Tables(0).Rows(0)("MOTHERS_NAME").ToString()
                txtAlias.Text = ds.Tables(0).Rows(0)("ALIAS").ToString()
                _father = ds.Tables(0).Rows(0)("ALIAS").ToString()
                txtSSN.Text = ds.Tables(0).Rows(0)("SSN").ToString()
                _nationalID = ds.Tables(0).Rows(0)("SSN").ToString()
                TxtPassportNo.Text = ds.Tables(0).Rows(0)("PASSPORT_NUMBER").ToString()
                _passNo = ds.Tables(0).Rows(0)("PASSPORT_NUMBER").ToString()

                txtIDNumber.Text = ds.Tables(0).Rows(0)("ID_NUMBER").ToString()
                _birthID = ds.Tables(0).Rows(0)("ID_NUMBER").ToString()



                cbmPassportCountry.SelectedValue = ds.Tables(0).Rows(0)("PASSPORT_COUNTRY")
                _passCountry = ds.Tables(0).Rows(0)("PASSPORT_COUNTRY").ToString()

                cmbNationality1.SelectedValue = ds.Tables(0).Rows(0)("NATIONALITY1")
                _nationality1 = ds.Tables(0).Rows(0)("NATIONALITY1").ToString()
                cmbNationality2.SelectedValue = ds.Tables(0).Rows(0)("NATIONALITY2")
                cmbNationality3.SelectedValue = ds.Tables(0).Rows(0)("NATIONALITY3")
                cmbResidence.SelectedValue = ds.Tables(0).Rows(0)("RESIDENCE")
                _residence = ds.Tables(0).Rows(0)("RESIDENCE").ToString()

                txtOccupation.Text = ds.Tables(0).Rows(0)("OCCUPATION").ToString()
                _occpation = ds.Tables(0).Rows(0)("OCCUPATION").ToString()

                txtEmployerName.Text = ds.Tables(0).Rows(0)("EMPLOYER_NAME").ToString()
                _employerName = ds.Tables(0).Rows(0)("EMPLOYER_NAME").ToString()

                If ds.Tables(0).Rows(0)("DECEASED").ToString() = 1 Then
                    cmbDecreased.Text = "Yes"
                Else
                    cmbDecreased.Text = "No"
                End If
                txtDeceasedDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("DECEASED_DATE"))
                txtEmail.Text = ds.Tables(0).Rows(0)("EMAIL").ToString()
                _email = ds.Tables(0).Rows(0)("EMAIL").ToString()
                txtEmail2.Text = ds.Tables(0).Rows(0)("EMAIL2").ToString()
                txtEmail3.Text = ds.Tables(0).Rows(0)("EMAIL3").ToString()
                txtTaxNumber.Text = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()
                _taxNo = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()

                txtTaxRegNumber.Text = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString()
                _taxReg = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString()
                txtSourceWealth.Text = ds.Tables(0).Rows(0)("SOURCE_OF_WEALTH").ToString()
                _sourceWealth = ds.Tables(0).Rows(0)("SOURCE_OF_WEALTH").ToString()
                txtComments.Text = ds.Tables(0).Rows(0)("COMMENTS").ToString()
                _comments = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                '--------------Mizan Work (26-04-2016-----------
                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE = '" & _nationality1 & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _nationality1Name = ds3.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _nationality1 = _nationality1Name

                End If

                Dim ds4 As New DataSet
                ds4 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE ='" & _residence & "' ")
                If ds4.Tables(0).Rows.Count > 0 Then

                    _residenceName = ds4.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _residence = _residenceName

                End If
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE ='" & _passCountry & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _passCountryName = ds2.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _passCountry = _passCountryName

                End If
                '--------------Mizan Work (26-04-2016-----------

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

                LoadPersonalPhoneData(strId, intmod)
                LoadEmployerPhoneData(strId, intmod)
                LoadPersonAddressData(strId, intmod)
                LoadEmployerAddressData(strId, intmod)
                LoadIdentificationData(strId, intmod)


                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_RPerson_GetMaxMod")

                commProc2.Parameters.Clear()

                db.AddInParameter(commProc2, "@PERSON_ID", DbType.String, strId)

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

    Private Sub LoadPersonalPhoneData(ByVal strId As String, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim dt As New DataTable
            Dim Status As String = "P"
            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_RPersonalPhone_GetDetails")

            commProcSche.Parameters.Clear()

            db.AddInParameter(commProcSche, "@PERSON_ID", DbType.String, strId)
            'db.AddInParameter(commProcSche, "@SID", DbType.String, Status)
            db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, intmod)

            dt = db.ExecuteDataSet(commProcSche).Tables(0)


            'If dt.Rows.Count > 0 Then

            '    txtPhoneID.Text = dt.Rows(0)("TPH_ID")
            '    cmbContact.SelectedValue = dt.Rows(0)("TPH_CONTACT_TYPE")
            '    cbmCommunication.SelectedValue = dt.Rows(0)("TPH_COMMUNICATION_TYPE")
            '    txtCountryPrefix.Text = dt.Rows(0)("TPH_COUNTRY_PREFIX")
            '    TxtPhone.Text = dt.Rows(0)("TPH_NUMBER")
            '    txtExtension.Text = dt.Rows(0)("TPH_EXTENSION")
            '    Label80.Text = dt.Rows(0)("SLNO")
            'End If

            DataGridView1.Rows.Clear()

            If dt.Rows.Count > 0 Then

                DataGridView1.AllowUserToAddRows = True
                For i = 0 To dt.Rows.Count - 1
                    If (i = DataGridView1.Rows.Count - 1) Then
                        DataGridView1.Rows.Add()
                    End If
                    DataGridView1.Item(0, i).Value = dt.Rows(i)("TPH_ID")
                    DataGridView1.Item(1, i).Value = dt.Rows(i)("TPH_CONTACT_TYPE")
                    DataGridView1.Item(2, i).Value = dt.Rows(i)("TPH_COMMUNICATION_TYPE")
                    DataGridView1.Item(3, i).Value = dt.Rows(i)("TPH_COUNTRY_PREFIX")
                    DataGridView1.Item(4, i).Value = dt.Rows(i)("TPH_NUMBER")
                    DataGridView1.Item(5, i).Value = dt.Rows(i)("TPH_EXTENSION")
                    DataGridView1.Item(6, i).Value = dt.Rows(i)("SLNO")
                    DataGridView1.Item(7, i).Value = dt.Rows(i)("SID")
                    

                Next
                DataGridView1.AllowUserToAddRows = False

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadEmployerPhoneData(ByVal strId As String, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim dt As New DataTable
            Dim Status As String = "E"
            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_REmployerPhone_GetDetails")

            commProcSche.Parameters.Clear()

            db.AddInParameter(commProcSche, "@PERSON_ID", DbType.String, strId)
            'db.AddInParameter(commProcSche, "@SID", DbType.String, Status)
            db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, intmod)

            dt = db.ExecuteDataSet(commProcSche).Tables(0)


            'If dt.Rows.Count > 0 Then

            '    txtEmployerId.Text = dt.Rows(0)("TPH_ID")
            '    cmbEmpContact.SelectedValue = dt.Rows(0)("TPH_CONTACT_TYPE")
            '    cmbEmpCommunication.SelectedValue = dt.Rows(0)("TPH_COMMUNICATION_TYPE")
            '    txtEmpPrefix.Text = dt.Rows(0)("TPH_COUNTRY_PREFIX")
            '    txtEmpPhone.Text = dt.Rows(0)("TPH_NUMBER")
            '    txtEmpExtension.Text = dt.Rows(0)("TPH_EXTENSION")
            '    Label80.Text = dt.Rows(0)("SLNO")
            'End If

            DataGridView3.Rows.Clear()

            If dt.Rows.Count > 0 Then

                DataGridView3.AllowUserToAddRows = True
                For i = 0 To dt.Rows.Count - 1
                    If (i = DataGridView3.Rows.Count - 1) Then
                        DataGridView3.Rows.Add()
                    End If
                    DataGridView3.Item(0, i).Value = dt.Rows(i)("TPH_ID")
                    DataGridView3.Item(1, i).Value = dt.Rows(i)("TPH_CONTACT_TYPE")
                    DataGridView3.Item(2, i).Value = dt.Rows(i)("TPH_COMMUNICATION_TYPE")
                    DataGridView3.Item(3, i).Value = dt.Rows(i)("TPH_COUNTRY_PREFIX")
                    DataGridView3.Item(4, i).Value = dt.Rows(i)("TPH_NUMBER")
                    DataGridView3.Item(5, i).Value = dt.Rows(i)("TPH_EXTENSION")
                    DataGridView3.Item(6, i).Value = dt.Rows(i)("SLNO")
                    DataGridView3.Item(7, i).Value = dt.Rows(i)("SID")

                Next
                DataGridView3.AllowUserToAddRows = False

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadPersonAddressData(ByVal strId As String, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim dt As New DataTable
            Dim Status As String = "P"
            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_RPersonAddress_GetDetails")

            commProcSche.Parameters.Clear()

            db.AddInParameter(commProcSche, "@PERSON_ID", DbType.String, strId)
            'db.AddInParameter(commProcSche, "@SID", DbType.String, Status)
            db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, intmod)

            dt = db.ExecuteDataSet(commProcSche).Tables(0)


            'If dt.Rows.Count > 0 Then

            '    txtPersonalAdID.Text = dt.Rows(0)("ADDRESS_ID")
            '    cmbPerAddress.SelectedValue = dt.Rows(0)("ADDRESS_TYPE")
            '    txtAddress.Text = dt.Rows(0)("ADDRESS")
            '    txtTown.Text = dt.Rows(0)("TOWN")
            '    txtCity.Text = dt.Rows(0)("CITY")
            '    txtZip .Text = dt.Rows(0)("ZIP")
            '    cmbPersonalCountry.SelectedValue = dt.Rows(0)("COUNTRY_CODE")
            '    txtState.Text = dt.Rows(0)("STATE")
            '    Label80.Text = dt.Rows(0)("SLNO")
            'End If

            DataGridView4.Rows.Clear()

            If dt.Rows.Count > 0 Then

                DataGridView4.AllowUserToAddRows = True
                For i = 0 To dt.Rows.Count - 1
                    If (i = DataGridView4.Rows.Count - 1) Then
                        DataGridView4.Rows.Add()
                    End If
                    DataGridView4.Item(0, i).Value = dt.Rows(i)("ADDRESS_ID")
                    DataGridView4.Item(1, i).Value = dt.Rows(i)("ADDRESS_TYPE")
                    DataGridView4.Item(2, i).Value = dt.Rows(i)("ADDRESS")
                    DataGridView4.Item(3, i).Value = dt.Rows(i)("TOWN")
                    DataGridView4.Item(4, i).Value = dt.Rows(i)("CITY")
                    DataGridView4.Item(5, i).Value = dt.Rows(i)("ZIP")
                    DataGridView4.Item(6, i).Value = dt.Rows(i)("COUNTRY_CODE")
                    DataGridView4.Item(7, i).Value = dt.Rows(i)("STATE")
                    DataGridView4.Item(8, i).Value = dt.Rows(i)("SLNO")
                    DataGridView4.Item(9, i).Value = dt.Rows(i)("SID")

                Next
                DataGridView4.AllowUserToAddRows = False

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadEmployerAddressData(ByVal strId As String, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim dt As New DataTable
            Dim Status As String = "E"
            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_REmployerAddress_GetDetails")

            commProcSche.Parameters.Clear()

            db.AddInParameter(commProcSche, "@PERSON_ID", DbType.String, strId)
            'db.AddInParameter(commProcSche, "@SID", DbType.String, Status)
            db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, intmod)

            dt = db.ExecuteDataSet(commProcSche).Tables(0)


            'If dt.Rows.Count > 0 Then

            '    txtEmployerAdId.Text = dt.Rows(0)("ADDRESS_ID")
            '    cmbEmployerContact.SelectedValue = dt.Rows(0)("ADDRESS_TYPE")
            '    txtEmployerAddress.Text = dt.Rows(0)("ADDRESS")
            '    txtEmployerTown.Text = dt.Rows(0)("TOWN")
            '    txtCity.Text = dt.Rows(0)("CITY")
            '    txtEmployerZip.Text = dt.Rows(0)("ZIP")
            '    cmbEmployerCountry.SelectedValue = dt.Rows(0)("COUNTRY_CODE")
            '    txtEmployerState.Text = dt.Rows(0)("STATE")
            '    Label80.Text = dt.Rows(0)("SLNO")
            'End If

            DataGridView5.Rows.Clear()

            If dt.Rows.Count > 0 Then

                DataGridView5.AllowUserToAddRows = True
                For i = 0 To dt.Rows.Count - 1
                    If (i = DataGridView5.Rows.Count - 1) Then
                        DataGridView5.Rows.Add()
                    End If
                    DataGridView5.Item(0, i).Value = dt.Rows(i)("ADDRESS_ID")
                    DataGridView5.Item(1, i).Value = dt.Rows(i)("ADDRESS_TYPE")
                    DataGridView5.Item(2, i).Value = dt.Rows(i)("ADDRESS")
                    DataGridView5.Item(3, i).Value = dt.Rows(i)("TOWN")
                    DataGridView5.Item(4, i).Value = dt.Rows(i)("CITY")
                    DataGridView5.Item(5, i).Value = dt.Rows(i)("ZIP")
                    DataGridView5.Item(6, i).Value = dt.Rows(i)("COUNTRY_CODE")
                    DataGridView5.Item(7, i).Value = dt.Rows(i)("STATE")
                    DataGridView5.Item(8, i).Value = dt.Rows(i)("SLNO")
                    DataGridView5.Item(9, i).Value = dt.Rows(i)("SID")

                Next
                DataGridView5.AllowUserToAddRows = False

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadIdentificationData(ByVal strId As String, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim dt As New DataTable
            Dim Status As String = "E"
            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_RPersonIdentificarion_GetDetails")

            commProcSche.Parameters.Clear()

            db.AddInParameter(commProcSche, "@PERSON_ID", DbType.String, strId)
            'db.AddInParameter(commProcSche, "@SID", DbType.String, Status)
            db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, intmod)

            dt = db.ExecuteDataSet(commProcSche).Tables(0)


            'If dt.Rows.Count > 0 Then

            '    txtIentID.Text = dt.Rows(0)("IDENTIFICATION_ID")
            '    cmbIdentification.SelectedValue = dt.Rows(0)("TYPE")
            '    txtNumber.Text = dt.Rows(0)("NUMBER")
            '    txtIssueDate.Text = NullHelper.DateToString(dt.Rows(0)("ISSUE_DATE"))
            '    txtExpiryDate.Text = NullHelper.DateToString(dt.Rows(0)("EXPIRY_DATE"))
            '    txtIssuedBy.Text = dt.Rows(0)("ISSUED_BY")
            '    cmbIssueCountry.SelectedValue = dt.Rows(0)("ISSUE_COUNTRY")
            '    Label80.Text = dt.Rows(0)("SLNO")
            'End If

            DataGridView6.Rows.Clear()

            If dt.Rows.Count > 0 Then

                DataGridView6.AllowUserToAddRows = True
                For i = 0 To dt.Rows.Count - 1
                    If (i = DataGridView6.Rows.Count - 1) Then
                        DataGridView6.Rows.Add()
                    End If
                    DataGridView6.Item(0, i).Value = dt.Rows(i)("IDENTIFICATION_ID")
                    DataGridView6.Item(1, i).Value = dt.Rows(i)("TYPE")
                    DataGridView6.Item(2, i).Value = dt.Rows(i)("NUMBER")
                    DataGridView6.Item(3, i).Value = dt.Rows(i)("ISSUE_DATE")
                    DataGridView6.Item(4, i).Value = dt.Rows(i)("EXPIRY_DATE")
                    DataGridView6.Item(5, i).Value = dt.Rows(i)("ISSUED_BY")
                    DataGridView6.Item(6, i).Value = dt.Rows(i)("ISSUE_COUNTRY")
                    DataGridView6.Item(7, i).Value = dt.Rows(i)("SLNO")
                    DataGridView6.Item(8, i).Value = dt.Rows(i)("SID")

                Next
                DataGridView6.AllowUserToAddRows = False

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

    Public Sub New(ByVal strId As String, ByVal intmod As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadMainData(strId, intmod)
        _strId = strId

        _intModno = intmod

    End Sub

    '--------------Mizan Work (20-04-16)---------------------

    Private Function AuthorizeData() As TransState

        If _intModno > 1 Then

            LoadMainDataForAuth(_strId)

            Dim tStatus As TransState

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_RPerson_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@PERSON_ID", DbType.String, _strId)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                '--------------Mizan Work (20-04-16)---------------------

                If _btitle <> _title Then
                    If _btitle = "" Then
                        log_message = " Title : " + _title + "." + " "

                    Else
                        log_message = " Title : " + _btitle + " " + " To " + " " + _title + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bfname <> _fname Then
                    If _bfname = "" Then
                        log_message = " First Name : " + _fname + "." + " "
                    Else
                        log_message = " First Name : " + _bfname + " " + " To " + " " + _fname + "." + " "
                    End If

                    PersonList.Add(log_message)
                End If
                If _bmname <> _mname Then
                    If _bmname = "" Then
                        log_message = " Middle Name : " + _mname + "." + " "

                    Else
                        log_message = " Middle Name : " + _bmname + " " + " To " + " " + _mname + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _blname <> _lname Then
                    If _blname = "" Then
                        log_message = " Last Name : " + _lname + "." + " "
                    Else
                        log_message = " Last Name : " + _blname + " " + " To " + " " + _lname + "." + " "
                    End If

                    PersonList.Add(log_message)
                End If
                If _bspouse <> _spouse Then
                    If _bspouse = "" Then
                        log_message = " Spouse : " + _spouse + "." + " "

                    Else
                        log_message = " Spouse  : " + _bspouse + " " + " To " + " " + _spouse + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bbirthdate <> _birthdate Then
                    If _bbirthdate = "" Then
                        log_message = " Birth date:  " + _birthdate + "." + " "
                    Else
                        log_message = " Birth date:  " + _bbirthdate + " " + " To " + " " + _birthdate + "." + " "
                    End If

                    PersonList.Add(log_message)
                End If
                If _bbirthplace <> _birthplace Then
                    If _bbirthplace = "" Then
                        log_message = " Birth Place : " + _birthplace + "." + " "

                    Else
                        log_message = " Birth Place : " + _bbirthplace + " " + " To " + " " + _birthplace + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bmother <> _mother Then
                    If _bmother = "" Then
                        log_message = " Mother : " + _mother + "." + " "

                    Else
                        log_message = " Mother : " + _bmother + " " + " To " + " " + _mother + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bfather <> _father Then
                    If _bfather = "" Then
                        log_message = " Father : " + _father + "." + " "

                    Else
                        log_message = " Father : " + _bfather + " " + " To " + " " + _father + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bnationalID <> _nationalID Then
                    If _bnationalID = "" Then
                        log_message = " National ID : " + _nationalID + "." + " "

                    Else
                        log_message = " National ID : " + _bnationalID + " " + " To " + " " + _nationalID + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bpassNo <> _passNo Then
                    If _bpassNo = "" Then
                        log_message = " Passport No : " + _passNo + "." + " "

                    Else
                        log_message = " Passport No  : " + _bpassNo + " " + " To " + " " + _passNo + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bpassCountry <> _passCountry Then
                    If _bpassCountry = "" Then
                        log_message = " Passport Country  : " + _passCountry + "." + " "
                    Else
                        log_message = " Passport Country  : " + _bpassCountry + " " + " To " + " " + _passCountry + "." + " "
                    End If
                    PersonList.Add(log_message)
                End If

                If _bbirthID <> _birthID Then
                    If _bbirthID = "" Then
                        log_message = " Birth Reg ID : " + _birthID + "." + " "

                    Else
                        log_message = "  Birth Reg ID : " + _bbirthID + " " + " To " + " " + _birthID + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bnationality1 <> _nationality1 Then
                    log_message = " Nationality 1 : " + _bnationality1 + " " + " To " + " " + _nationality1 + "." + " "
                    PersonList.Add(log_message)
                End If
                If _bresidence <> _residence Then

                    log_message = "  Residence : " + _bresidence + " " + " To " + " " + _residence + "." + " "
                    PersonList.Add(log_message)
                End If

                If _boccpation <> _occpation Then
                    If _boccpation = "" Then
                        log_message = " Occupation : " + _occpation + "." + " "

                    Else
                        log_message = "   Occupation : " + _boccpation + " " + " To " + " " + _occpation + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bemployerName <> _employerName Then
                    If _bemployerName = "" Then
                        log_message = " Employer Name : " + _employerName + "." + " "

                    Else
                        log_message = "  Employer Name : " + _bemployerName + " " + " To " + " " + _employerName + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bemail <> _email Then
                    If _bemail = "" Then
                        log_message = " Email : " + _email + "." + " "

                    Else
                        log_message = "  Email : " + _bemail + " " + " To " + " " + _email + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _btaxNo <> _taxNo Then
                    If _btaxNo = "" Then
                        log_message = " Tax No : " + _taxNo + "." + " "

                    Else
                        log_message = " Tax No : " + _btaxNo + " " + " To " + " " + _taxNo + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _btaxReg <> _taxReg Then
                    If _btaxReg = "" Then
                        log_message = " Tax Reg No : " + _taxReg + "." + " "

                    Else
                        log_message = " Tax Reg No : " + _btaxReg + " " + " To " + " " + _taxReg + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bsourceWealth <> _sourceWealth Then
                    If _bsourceWealth = "" Then
                        log_message = " Source wealth : " + _sourceWealth + "." + " "

                    Else
                        log_message = " Source wealth : " + _bsourceWealth + " " + " To " + " " + _sourceWealth + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _bcomments <> _comments Then
                    If _bcomments = "" Then
                        log_message = " Comments : " + _comments + "." + " "

                    Else
                        log_message = " Comments : " + _bcomments + " " + " To " + " " + _comments + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If

                For Each perlist As String In PersonList
                    _personLog += perlist
                Next

                _Plog = " Authorized : Reporting Person : " + txtId.Text.ToString() + "." + " " + _personLog

                Logger.system_log(_Plog)
                _personLog = ""
                PersonList.Clear()

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

           

            'log_message = "Authorized Reporting Person " + _strId + " Name " + txtTitle.Text.ToString() + " " + txtFirstName.Text.ToString() + " " + txtMiddle.Text.ToString() + " " + txtLast.Text.ToString()
            'Logger.system_log(log_message)

            Return tStatus


        Else

            Dim tStatus As TransState

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_RPerson_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@PERSON_ID", DbType.String, _strId)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                '--------------Mizan Work (20-04-16)---------------------

                If _btitle <> _title Then
                    If _btitle = "" Then
                        ' log_message = " Title : " + _title + "." + " "

                    Else
                        log_message = " Title : " + _btitle + " " + " To " + " " + _title + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bfname <> _fname Then
                    If _bfname = "" Then
                        log_message = " First Name : " + _fname + "." + " "
                    Else
                        log_message = " First Name : " + _bfname + " " + " To " + " " + _fname + "." + " "
                    End If

                    PersonList.Add(log_message)
                End If
                If _bmname <> _mname Then
                    If _bmname = "" Then
                        log_message = " Middle Name : " + _mname + "." + " "

                    Else
                        log_message = " Middle Name : " + _bmname + " " + " To " + " " + _mname + "." + " "

                    End If
                    PersonList.Add(log_message)
                End If
                If _blname <> _lname Then
                    If _blname = "" Then
                        log_message = " Last Name " + _lname + "." + " "
                    Else
                        log_message = " Last Name " + _blname + " " + " To " + " " + _lname + "." + " "
                    End If

                    PersonList.Add(log_message)
                End If
                If _bspouse <> _spouse Then
                    If _bspouse = "" Then
                        'log_message = " Spouse : " + _spouse + "." + " "

                    Else
                        log_message = " Spouse  : " + _bspouse + " " + " To " + " " + _spouse + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bbirthdate <> _birthdate Then
                    If _bbirthdate = "" Then
                        'log_message = " Birth date:  " + _birthdate + "." + " "
                    Else
                        log_message = " Birth date:  " + _bbirthdate + " " + " To " + " " + _birthdate + "." + " "
                        PersonList.Add(log_message)
                    End If


                End If
                If _bbirthplace <> _birthplace Then
                    If _bbirthplace = "" Then
                        'log_message = " Birth Place : " + _birthplace + "." + " "

                    Else
                        log_message = " Birth Place : " + _bbirthplace + " " + " To " + " " + _birthplace + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bmother <> _mother Then
                    If _bmother = "" Then
                        'log_message = " Mother : " + _mother + "." + " "

                    Else
                        log_message = " Mother : " + _bmother + " " + " To " + " " + _mother + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bfather <> _father Then
                    If _bfather = "" Then
                        'log_message = " Father : " + _father + "." + " "

                    Else
                        log_message = " Father : " + _bfather + " " + " To " + " " + _father + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bnationalID <> _nationalID Then
                    If _bnationalID = "" Then
                        'log_message = " National ID : " + _nationalID + "." + " "

                    Else
                        log_message = " National ID : " + _bnationalID + " " + " To " + " " + _nationalID + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bpassNo <> _passNo Then
                    If _bpassNo = "" Then
                        'log_message = " Passport No : " + _passNo + "." + " "

                    Else
                        log_message = " Passport No  : " + _bpassNo + " " + " To " + " " + _passNo + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bpassCountry <> _passCountry Then
                    If _bpassCountry = "" Then
                    Else
                        log_message = " Passport Country  : " + _bpassCountry + " " + " To " + " " + _passCountry + "." + " "
                        PersonList.Add(log_message)
                    End If
                End If

                If _bbirthID <> _birthID Then
                    If _bbirthID = "" Then
                        'log_message = " Birth Reg ID : " + _birthID + "." + " "

                    Else
                        log_message = "  Birth Reg ID : " + _bbirthID + " " + " To " + " " + _birthID + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bnationality1 <> _nationality1 Then
                    If _bnationality1 = "" Then
                    Else
                        log_message = " Nationality 1 : " + _bnationality1 + " " + " To " + " " + _nationality1 + "." + " "
                        PersonList.Add(log_message)
                    End If


                End If
                If _bresidence <> _residence Then
                    If _bresidence = "" Then
                    Else
                        log_message = "  Residence : " + _bresidence + " " + " To " + " " + _residence + "." + " "
                        PersonList.Add(log_message)
                    End If


                End If

                If _boccpation <> _occpation Then
                    If _boccpation = "" Then
                        'log_message = " Occupation : " + _occpation + "." + " "

                    Else
                        log_message = "   Occupation : " + _boccpation + " " + " To " + " " + _occpation + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bemployerName <> _employerName Then
                    If _bemployerName = "" Then
                        ' log_message = " Employer Name : " + _employerName + "." + " "

                    Else
                        log_message = "  Employer Name : " + _bemployerName + " " + " To " + " " + _employerName + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bemail <> _email Then
                    If _bemail = "" Then
                        ' log_message = " Email : " + _email + "." + " "

                    Else
                        log_message = "  Email : " + _bemail + " " + " To " + " " + _email + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _btaxNo <> _taxNo Then
                    If _btaxNo = "" Then
                        'log_message = " Tax No : " + _taxNo + "." + " "

                    Else
                        log_message = " Tax No : " + _btaxNo + " " + " To " + " " + _taxNo + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _btaxReg <> _taxReg Then
                    If _btaxReg = "" Then
                        ' log_message = " Tax Reg No : " + _taxReg + "." + " "

                    Else
                        log_message = " Tax Reg No : " + _btaxReg + " " + " To " + " " + _taxReg + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bsourceWealth <> _sourceWealth Then
                    If _bsourceWealth = "" Then
                        'log_message = " Source wealth : " + _sourceWealth + "." + " "

                    Else
                        log_message = " Source wealth : " + _bsourceWealth + " " + " To " + " " + _sourceWealth + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If
                If _bcomments <> _comments Then
                    If _bcomments = "" Then
                        'log_message = " Comments : " + _comments + "." + " "

                    Else
                        log_message = " Comments : " + _bcomments + " " + " To " + " " + _comments + "." + " "
                        PersonList.Add(log_message)
                    End If

                End If

                For Each perlist As String In PersonList
                    _personLog += perlist
                Next

                _Plog = " Authorized : Reporting Person : " + txtId.Text.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _personLog

                Logger.system_log(_Plog)

                _personLog = ""
                PersonList.Clear()

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

            

            'log_message = "Authorized Reporting Person " + _strId + " Name " + txtTitle.Text.ToString() + " " + txtFirstName.Text.ToString() + " " + txtMiddle.Text.ToString() + " " + txtLast.Text.ToString()
            'Logger.system_log(log_message)

            Return tStatus

        End If

    End Function

    Private Function DeleteData() As TransState

        Dim tStatus As TransState

        Dim intModno As Integer = 0

        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim commProc As DbCommand = db.GetStoredProcCommand("GO_RPerson_Remove")

        commProc.Parameters.Clear()

        db.AddInParameter(commProc, "@PERSON_ID", DbType.String, _strId)
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

        log_message = "Authorized Reporting Person " + _strId + " Name " + txtFirstName.Text.ToString() + " " + txtLast.Text.ToString()
        Logger.system_log(log_message)

        Return tStatus

    End Function


#End Region

    Private Sub FrmPersonDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        CommonUtil.FillComboBox("GO_ContactType_GetList", cmbContact)
        CommonUtil.FillComboBox("GO_CommunicationType_GetList", cbmCommunication)

        CommonUtil.FillComboBox("GO_CountryType_GetList", cbmPassportCountry)
        CommonUtil.FillComboBox("GO_CountryType_GetList", cmbNationality1)
        CommonUtil.FillComboBox("GO_CountryType_GetList", cmbNationality2)
        CommonUtil.FillComboBox("GO_CountryType_GetList", cmbNationality3)
        CommonUtil.FillComboBox("GO_CountryType_GetList", cmbResidence)

        CommonUtil.FillComboBox("GO_ContactType_GetList", cmbPerAddress)
        CommonUtil.FillComboBox("GO_CountryType_GetList", cmbPersonalCountry)
        CommonUtil.FillComboBox("GO_ThanaType_GetList", cmbPThana)
        CommonUtil.FillComboBox("GO_DistrictType_GetList", cmbPDistrict)
        CommonUtil.FillComboBox("GO_DivisionType_GetList", cmbPDivision)




        CommonUtil.FillComboBox("GO_ContactType_GetList", cmbEmpContact)
        CommonUtil.FillComboBox("GO_CommunicationType_GetList", cmbEmpCommunication)

        CommonUtil.FillComboBox("GO_ContactType_GetList", cmbEmployerContact)
        CommonUtil.FillComboBox("GO_CountryType_GetList", cmbEmployerCountry)
        CommonUtil.FillComboBox("GO_ThanaType_GetList", cmbEmpThana)
        CommonUtil.FillComboBox("GO_DistrictType_GetList", cmbEmpDistrict)
        CommonUtil.FillComboBox("GO_DivisionType_GetList", cmbEmpDivision)


        CommonUtil.FillComboBox("GO_IdentifierType_GetList", cmbIdentification)
        CommonUtil.FillComboBox("GO_CountryType_GetList", cmbIssueCountry)



        If _intModno > 0 Then
            LoadMainData(_strId, _intModno)
        End If



        EnableUnlock()

        DisableNew()
        DisableSave()
        DisableDelete()
        DisableAuth()

        DisableClear()
        DisableRefresh()

        DisableFields()

        DataGridView1.Enabled = False
        DataGridView3.Enabled = False
        DataGridView4.Enabled = False
        DataGridView5.Enabled = False
        DataGridView6.Enabled = False

      


    End Sub

    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlock.Click

        lblToolStatus.Text = ""

        EnableNew()
        If Not (_strId = "") Then

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

        DataGridView1.Enabled = True
        DataGridView3.Enabled = True
        DataGridView4.Enabled = True
        DataGridView5.Enabled = True
        DataGridView6.Enabled = True
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
                        LoadMainData(_strId, _intModno)

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

                        LoadMainData(_strId, _intModno)

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
        LoadMainData(_strId, _intModno)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try


            If MessageBox.Show("Do you really want to delete?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                tState = DeleteData()

                If tState = TransState.Delete Then


                    _formMode = FormTransMode.Add

                    LoadMainData(_strId, _intModno)

                    DisableAuth()

                    If _strId = "" Then

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

            LoadMainData(_strId, _intModno - 1)

        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click
        Dim strId As String = _strId
        Dim intModno As Integer = _intModno
        If intModno > 0 Then
            LoadMainData(_strId, _intModno + 1)

            If _intModno = 0 Then
                LoadMainData(strId, intModno)
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
                    LoadMainData(_strId, _intModno)
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

    Private Sub AddToGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToGrid.Click

       If cmbContact.Text.Trim() = "" Then
            MessageBox.Show("Contact Type required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbContact.Focus()
            Exit Sub
        ElseIf cbmCommunication.Text = "" Then
            MessageBox.Show("Communication Code required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cbmCommunication.Focus()
            Exit Sub
        ElseIf TxtPhone.Text.Trim() = "" Then
            MessageBox.Show("Phone Number required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtPhone.Focus()
            Exit Sub

        End If


        'For Each row As DataGridViewRow In DataGridView1.Rows
        '    If row.Index.ToString() <> lblRowNo.Text Then
        '        If row.Cells(0).Value.ToString().Trim() = txtPhoneID.Text.Trim() Then
        '            MessageBox.Show("Phone Code Error", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Exit Sub
        '        End If
        '    End If

        'Next



        If _RowEditMode = True Then

            Dim selRow As Integer = lblRowNo.Text.Trim()

            DataGridView1.Item(0, selRow).Value = Label80.Text
            DataGridView1.Item(1, selRow).Value = cmbContact.SelectedValue
            DataGridView1.Item(2, selRow).Value = cbmCommunication.SelectedValue
            DataGridView1.Item(3, selRow).Value = txtCountryPrefix.Text.Trim()
            DataGridView1.Item(4, selRow).Value = TxtPhone.Text.Trim()
            DataGridView1.Item(5, selRow).Value = txtExtension.Text.Trim()






            DataGridView1.Rows(0).Selected = True
            DataGridView1.Rows(0).Selected = False
            DataGridView1.Rows(selRow).Selected = True


        End If

        If _RowEditMode = False Then

            Dim MaxSlNo As Integer = 1
            Dim PhoneStatus As String = "P"   ' P For Personal Phone Information in database '

            For Each row As DataGridViewRow In DataGridView1.Rows
                If MaxSlNo <= NullHelper.ToIntNum(row.Cells(6).Value) Then
                    MaxSlNo = NullHelper.ToIntNum(row.Cells(6).Value) + 1
                End If


            Next

            DataGridView1.Rows.Add()

            Dim maxRow As Integer = DataGridView1.Rows.Count - 1

            DataGridView1.Item(0, maxRow).Value = MaxSlNo
            DataGridView1.Item(1, maxRow).Value = cmbContact.SelectedValue
            DataGridView1.Item(2, maxRow).Value = cbmCommunication.SelectedValue
            DataGridView1.Item(3, maxRow).Value = txtCountryPrefix.Text.Trim()
            DataGridView1.Item(4, maxRow).Value = TxtPhone.Text.Trim()
            DataGridView1.Item(5, maxRow).Value = txtExtension.Text.Trim()
            DataGridView1.Item(6, maxRow).Value = MaxSlNo
            DataGridView1.Item(7, maxRow).Value = PhoneStatus

            DataGridView1.Rows(0).Selected = True
            DataGridView1.Rows(0).Selected = False
            DataGridView1.Rows(maxRow).Selected = True

        End If
        btnCancelGrid1.Visible = False
        btnRemoveGrid1.Enabled = True
        Label80.Text = ""
        cmbContact.SelectedValue = -1
        cbmCommunication.SelectedValue = -1
        txtCountryPrefix.Clear()
        TxtPhone.Clear()
        txtExtension.Clear()


    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        If Not (DataGridView1.SelectedRows.Item(0).Cells(0).Value Is Nothing Or DataGridView1.SelectedRows.Item(0).Cells(0).Value Is DBNull.Value) Then


            _RowEditMode = True

            AddToGrid.Text = "Update"
            btnCancelGrid1.Visible = True
            btnRemoveGrid1.Enabled = False


            lblRowNo.Text = e.RowIndex.ToString()

            Label80.Text = DataGridView1.Item(0, e.RowIndex).Value
            cmbContact.SelectedValue = DataGridView1.Item(1, e.RowIndex).Value
            cbmCommunication.SelectedValue = DataGridView1.Item(2, e.RowIndex).Value
            txtCountryPrefix.Text = DataGridView1.Item(3, e.RowIndex).Value
            TxtPhone.Text = DataGridView1.Item(4, e.RowIndex).Value
            txtExtension.Text = DataGridView1.Item(5, e.RowIndex).Value




        End If

    End Sub


    Private Sub btnRemoveGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveGrid1.Click
        If DataGridView1.SelectedRows.Count = 0 Then Exit Sub

        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)
        Next
    End Sub

    Private Sub btnCancelGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelGrid1.Click
        _RowEditMode = False

        btnCancelGrid1.Visible = False
        btnRemoveGrid1.Enabled = True

        Label80.Text = ""
        cmbContact.SelectedValue = -1
        cbmCommunication.SelectedValue = -1
        txtCountryPrefix.Clear()
        TxtPhone.Clear()
        txtExtension.Clear()
    End Sub

    Private Sub btnAddPersonalAddressGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPersonalAddressGrid.Click

        If cmbPerAddress.Text.Trim() = "" Then
            MessageBox.Show("Address Type required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbPerAddress.Focus()
            Exit Sub
        ElseIf cmbPersonalCountry.Text = "" Then
            MessageBox.Show("Country Type required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbPersonalCountry.Focus()
            Exit Sub
        ElseIf txtAddress.Text.Trim() = "" Then
            MessageBox.Show("Address required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAddress.Focus()
            Exit Sub
        
        ElseIf cmbPThana.Text.Trim() = "" Then
            MessageBox.Show("City required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbPThana.Focus()
            Exit Sub

        ElseIf cmbPDivision.Text.Trim() = "" Then
            MessageBox.Show("State required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbPDivision.Focus()
            Exit Sub

        End If


        'For Each row As DataGridViewRow In DataGridView4.Rows
        '    If row.Index.ToString() <> lblRowNo.Text Then
        '        If row.Cells(0).Value.ToString().Trim() = txtPersonalAdID.Text.Trim() Then
        '            MessageBox.Show("Address Code Error", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Exit Sub
        '        End If
        '    End If

        'Next



        If _RowEditMode = True Then

            Dim selRow As Integer = lblRowNo.Text.Trim()

           
            DataGridView4.Item(0, selRow).Value = Label80.Text
            DataGridView4.Item(1, selRow).Value = cmbPerAddress.SelectedValue
            DataGridView4.Item(2, selRow).Value = txtAddress.Text.Trim()
            DataGridView4.Item(3, selRow).Value = cmbPThana.Text.Trim()
            DataGridView4.Item(4, selRow).Value = cmbPDistrict.Text.Trim()
            DataGridView4.Item(5, selRow).Value = txtZip.Text.Trim()
            DataGridView4.Item(6, selRow).Value = cmbPersonalCountry.SelectedValue
            DataGridView4.Item(7, selRow).Value = cmbPDivision.Text.Trim()
           




            DataGridView4.Rows(0).Selected = True
            DataGridView4.Rows(0).Selected = False
            DataGridView4.Rows(selRow).Selected = True


        End If



        If _RowEditMode = False Then

            Dim MaxSlNo2 As Integer = 1
            Dim AddressStatus As String = "P"   ' P For Personal Address Information in database '

            For Each row As DataGridViewRow In DataGridView4.Rows
                If MaxSlNo2 <= NullHelper.ToIntNum(row.Cells(8).Value) Then
                    MaxSlNo2 = NullHelper.ToIntNum(row.Cells(8).Value) + 1
                End If


            Next

            DataGridView4.Rows.Add()

            Dim maxRow As Integer = DataGridView4.Rows.Count - 1

            DataGridView4.Item(0, maxRow).Value = MaxSlNo2
            DataGridView4.Item(1, maxRow).Value = cmbPerAddress.SelectedValue
            DataGridView4.Item(2, maxRow).Value = txtAddress.Text.Trim()
            DataGridView4.Item(3, maxRow).Value = cmbPThana.Text.Trim()
            DataGridView4.Item(4, maxRow).Value = cmbPDistrict.Text.Trim()
            DataGridView4.Item(5, maxRow).Value = txtZip.Text.Trim()
            DataGridView4.Item(6, maxRow).Value = cmbPersonalCountry.SelectedValue
            DataGridView4.Item(7, maxRow).Value = cmbPDivision.Text.Trim()
            DataGridView4.Item(8, maxRow).Value = MaxSlNo2
            DataGridView4.Item(9, maxRow).Value = AddressStatus

            DataGridView4.Rows(0).Selected = True
            DataGridView4.Rows(0).Selected = False
            DataGridView4.Rows(maxRow).Selected = True

        End If
        btmCancelAddress.Visible = False
        btnRemovePersonalAddtoGrid.Enabled = True

        Label80.Text = ""
        cmbPerAddress.SelectedValue = -1
        cmbPersonalCountry.SelectedValue = -1
        cmbPDistrict.SelectedIndex = -1
        cmbPDivision.SelectedIndex = -1
        cmbPThana.SelectedIndex = -1

        txtAddress.Clear()


        txtZip.Clear()

    End Sub

    Private Sub DataGridView4_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView4.CellDoubleClick

        If Not (DataGridView4.SelectedRows.Item(0).Cells(0).Value Is Nothing Or DataGridView4.SelectedRows.Item(0).Cells(0).Value Is DBNull.Value) Then


            _RowEditMode = True

            btnAddPersonalAddressGrid.Text = "Update"
            btmCancelAddress.Visible = True
            btnRemovePersonalAddtoGrid.Enabled = False


            lblRowNo.Text = e.RowIndex.ToString()

            Label80.Text = DataGridView4.Item(0, e.RowIndex).Value
            cmbPerAddress.SelectedValue = DataGridView4.Item(1, e.RowIndex).Value
            txtAddress.Text = DataGridView4.Item(2, e.RowIndex).Value

            cmbPThana.Text = DataGridView4.Item(3, e.RowIndex).Value
            cmbPDistrict.Text = DataGridView4.Item(4, e.RowIndex).Value
            txtZip.Text = DataGridView4.Item(5, e.RowIndex).Value

            cmbPersonalCountry.SelectedValue = DataGridView4.Item(6, e.RowIndex).Value
            cmbPDivision.Text = DataGridView4.Item(7, e.RowIndex).Value



        End If

    End Sub

    Private Sub btnRemovePersonalAddtoGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePersonalAddtoGrid.Click
        If DataGridView4.SelectedRows.Count = 0 Then Exit Sub

        For Each row As DataGridViewRow In DataGridView4.SelectedRows
            DataGridView4.Rows.Remove(row)
        Next
    End Sub

    Private Sub btmCancelAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmCancelAddress.Click
        _RowEditMode = False

        btmCancelAddress.Visible = False
        btnRemovePersonalAddtoGrid.Enabled = True

        Label80.Text = ""
        cmbPerAddress.SelectedValue = -1
        cmbPersonalCountry.SelectedValue = -1
        cmbPThana.SelectedIndex = -1
        cmbPDistrict.SelectedIndex = -1
        cmbPDivision.SelectedIndex = -1
        txtAddress.Clear()
        txtZip.Clear()
    End Sub

    Private Sub btnEmpAddtoGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpAddtoGrid.Click

        If cmbEmpContact.Text.Trim() = "" Then
            MessageBox.Show("Contact Type required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbEmpContact.Focus()
            Exit Sub
        ElseIf cmbEmpCommunication.Text = "" Then
            MessageBox.Show("Communication Code required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbEmpCommunication.Focus()
            Exit Sub
        ElseIf txtEmpPhone.Text.Trim() = "" Then
            MessageBox.Show("Phone Number required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmpPhone.Focus()
            Exit Sub

        End If

        'For Each row As DataGridViewRow In DataGridView3.Rows
        '    If row.Index.ToString() <> lblRowNo.Text Then
        '        If row.Cells(0).Value.ToString().Trim() = txtEmployerId.Text.Trim() Then
        '            MessageBox.Show("Phone Code Error", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Exit Sub
        '        End If
        '    End If

        'Next



        If _RowEditMode = True Then

            Dim selRow As Integer = lblRowNo.Text.Trim()

            DataGridView3.Item(0, selRow).Value = Label80.Text
            DataGridView3.Item(1, selRow).Value = cmbEmpContact.SelectedValue
            DataGridView3.Item(2, selRow).Value = cmbEmpCommunication.SelectedValue
            DataGridView3.Item(3, selRow).Value = txtEmpPrefix.Text.Trim()
            DataGridView3.Item(4, selRow).Value = txtEmpPhone.Text.Trim()
            DataGridView3.Item(5, selRow).Value = txtEmpExtension.Text.Trim()




            DataGridView3.Rows(0).Selected = True
            DataGridView3.Rows(0).Selected = False
            DataGridView3.Rows(selRow).Selected = True


        End If



        If _RowEditMode = False Then

            Dim MaxSlNo3 As Integer = 1
            Dim EmpPhoneStatus As String = "E"   ' E For Employer Phone Information in database '

            For Each row As DataGridViewRow In DataGridView3.Rows
                If MaxSlNo3 <= NullHelper.ToIntNum(row.Cells(6).Value) Then
                    MaxSlNo3 = NullHelper.ToIntNum(row.Cells(6).Value) + 1
                End If


            Next

            DataGridView3.Rows.Add()

            Dim maxRow As Integer = DataGridView3.Rows.Count - 1

            DataGridView3.Item(0, maxRow).Value = MaxSlNo3
            DataGridView3.Item(1, maxRow).Value = cmbEmpContact.SelectedValue
            DataGridView3.Item(2, maxRow).Value = cmbEmpCommunication.SelectedValue
            DataGridView3.Item(3, maxRow).Value = txtEmpPrefix.Text.Trim()
            DataGridView3.Item(4, maxRow).Value = txtEmpPhone.Text.Trim()
            DataGridView3.Item(5, maxRow).Value = txtEmpExtension.Text.Trim()
            DataGridView3.Item(6, maxRow).Value = MaxSlNo3
            DataGridView3.Item(7, maxRow).Value = EmpPhoneStatus

            DataGridView3.Rows(0).Selected = True
            DataGridView3.Rows(0).Selected = False
            DataGridView3.Rows(maxRow).Selected = True

        End If
        btnEmpCanceltoGrid.Visible = False
        btnEmpRemoveToGrid.Enabled = True

        Label80.Text = ""
        cmbEmpContact.SelectedValue = -1
        cmbEmpCommunication.SelectedValue = -1
        txtEmpPrefix.Clear()
        txtEmpPhone.Clear()
        txtEmpExtension.Clear()

    End Sub

    Private Sub DataGridView3_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellDoubleClick

        If Not (DataGridView3.SelectedRows.Item(0).Cells(0).Value Is Nothing Or DataGridView3.SelectedRows.Item(0).Cells(0).Value Is DBNull.Value) Then


            _RowEditMode = True

            btnEmpAddtoGrid.Text = "Update"
            btnEmpCanceltoGrid.Visible = True
            btnEmpRemoveToGrid.Enabled = False


            lblRowNo.Text = e.RowIndex.ToString()

            Label80.Text = DataGridView3.Item(0, e.RowIndex).Value
            cmbEmpContact.SelectedValue = DataGridView3.Item(1, e.RowIndex).Value
            cmbEmpCommunication.SelectedValue = DataGridView3.Item(2, e.RowIndex).Value
            txtEmpPrefix.Text = DataGridView3.Item(3, e.RowIndex).Value
            txtEmpPhone.Text = DataGridView3.Item(4, e.RowIndex).Value
            txtEmpExtension.Text = DataGridView3.Item(5, e.RowIndex).Value




        End If

    End Sub

    Private Sub btnEmpRemoveToGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpRemoveToGrid.Click
        If DataGridView3.SelectedRows.Count = 0 Then Exit Sub

        For Each row As DataGridViewRow In DataGridView3.SelectedRows
            DataGridView3.Rows.Remove(row)
        Next
    End Sub

    Private Sub btnEmpCanceltoGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpCanceltoGrid.Click
        _RowEditMode = False

        btnEmpCanceltoGrid.Visible = False
        btnEmpRemoveToGrid.Enabled = True

        Label80.Text = ""
        cmbEmpContact.SelectedValue = -1
        cmbEmpCommunication.SelectedValue = -1
        txtEmpPrefix.Clear()
        txtEmpPhone.Clear()
        txtEmpExtension.Clear()

    End Sub

    Private Sub btmEnployerAddressAddToGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmEnployerAddressAddToGrid.Click

        If cmbEmployerContact.Text.Trim() = "" Then
            MessageBox.Show("Address Type required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbEmployerContact.Focus()
            Exit Sub
        ElseIf cmbEmployerCountry.Text = "" Then
            MessageBox.Show("Country Type required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbEmployerCountry.Focus()
            Exit Sub
        ElseIf txtEmployerAddress.Text.Trim() = "" Then
            MessageBox.Show("Employer Address required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmployerAddress.Focus()
            Exit Sub
        ElseIf cmbEmpDivision.Text.Trim() = "" Then
            MessageBox.Show("Employer State required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbEmpDivision.Focus()
            Exit Sub
        ElseIf cmbEmpThana.Text.Trim() = "" Then
            MessageBox.Show("Employer City required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbEmpThana.Focus()
            Exit Sub

        End If

        'For Each row As DataGridViewRow In DataGridView5.Rows
        '    If row.Index.ToString() <> lblRowNo.Text Then
        '        If row.Cells(0).Value.ToString().Trim() = txtEmployerAdId.Text.Trim() Then
        '            MessageBox.Show("Address Code Error", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Exit Sub
        '        End If
        '    End If

        'Next



        If _RowEditMode = True Then

            Dim selRow As Integer = lblRowNo.Text.Trim()

            DataGridView5.Item(0, selRow).Value = Label80.Text
            DataGridView5.Item(1, selRow).Value = cmbEmployerContact.SelectedValue
            DataGridView5.Item(2, selRow).Value = txtEmployerAddress.Text.Trim()
            DataGridView5.Item(3, selRow).Value = cmbEmpThana.Text.Trim()
            DataGridView5.Item(4, selRow).Value = cmbEmpDistrict.Text.Trim()
            DataGridView5.Item(5, selRow).Value = txtEmployerZip.Text.Trim()
            DataGridView5.Item(6, selRow).Value = cmbEmployerCountry.SelectedValue
            DataGridView5.Item(7, selRow).Value = cmbEmpDivision.Text.Trim()





            DataGridView5.Rows(0).Selected = True
            DataGridView5.Rows(0).Selected = False
            DataGridView5.Rows(selRow).Selected = True


        End If


        If _RowEditMode = False Then

            Dim MaxSlNo4 As Integer = 1
            Dim AddressEmpStatus As String = "E"   ' E For Employer Address Information in database '

            For Each row As DataGridViewRow In DataGridView5.Rows
                If MaxSlNo4 <= NullHelper.ToIntNum(row.Cells(8).Value) Then
                    MaxSlNo4 = NullHelper.ToIntNum(row.Cells(8).Value) + 1
                End If


            Next

            DataGridView5.Rows.Add()

            Dim maxRow As Integer = DataGridView5.Rows.Count - 1

            DataGridView5.Item(0, maxRow).Value = MaxSlNo4
            DataGridView5.Item(1, maxRow).Value = cmbEmployerContact.SelectedValue
            DataGridView5.Item(2, maxRow).Value = txtEmployerAddress.Text.Trim()
            DataGridView5.Item(3, maxRow).Value = cmbEmpThana.Text.Trim()
            DataGridView5.Item(4, maxRow).Value = cmbEmpDistrict.Text.Trim()
            DataGridView5.Item(5, maxRow).Value = txtEmployerZip.Text.Trim()
            DataGridView5.Item(6, maxRow).Value = cmbEmployerCountry.SelectedValue
            DataGridView5.Item(7, maxRow).Value = cmbEmpDivision.Text.Trim()
            DataGridView5.Item(8, maxRow).Value = MaxSlNo4
            DataGridView5.Item(9, maxRow).Value = AddressEmpStatus

            DataGridView5.Rows(0).Selected = True
            DataGridView5.Rows(0).Selected = False
            DataGridView5.Rows(maxRow).Selected = True

        End If
        btnCancelEmptoGrid.Visible = False
        btnEmployerRemoveGrid.Enabled = True

        Label80.Text = ""
        cmbEmployerContact.SelectedValue = -1
        cmbEmployerCountry.SelectedValue = -1
        cmbEmpThana.SelectedIndex = -1
        txtEmployerAddress.Clear()
        cmbEmpDistrict.SelectedIndex = -1
        cmbEmpDivision.SelectedIndex = -1
        txtEmployerZip.Clear()
    End Sub

    Private Sub DataGridView5_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView5.CellDoubleClick

        If Not (DataGridView5.SelectedRows.Item(0).Cells(0).Value Is Nothing Or DataGridView5.SelectedRows.Item(0).Cells(0).Value Is DBNull.Value) Then


            _RowEditMode = True

            btmEnployerAddressAddToGrid.Text = "Update"
            btnCancelEmptoGrid.Visible = True
            btnEmployerRemoveGrid.Enabled = False


            lblRowNo.Text = e.RowIndex.ToString()

            Label80.Text = DataGridView5.Item(0, e.RowIndex).Value
            cmbEmployerContact.SelectedValue = DataGridView5.Item(1, e.RowIndex).Value
            txtEmployerAddress.Text = DataGridView5.Item(2, e.RowIndex).Value

            cmbEmpThana.Text = DataGridView5.Item(3, e.RowIndex).Value
            cmbEmpDistrict.Text = DataGridView5.Item(4, e.RowIndex).Value
            txtEmployerZip.Text = DataGridView5.Item(5, e.RowIndex).Value

            cmbEmployerCountry.SelectedValue = DataGridView5.Item(6, e.RowIndex).Value
            cmbEmpDivision.Text = DataGridView5.Item(7, e.RowIndex).Value



        End If

    End Sub

    Private Sub btnEmployerRemoveGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmployerRemoveGrid.Click

        If DataGridView5.SelectedRows.Count = 0 Then Exit Sub

        For Each row As DataGridViewRow In DataGridView5.SelectedRows
            DataGridView5.Rows.Remove(row)
        Next

    End Sub

    Private Sub btnCancelEmptoGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelEmptoGrid.Click
        _RowEditMode = False

        btnEmpCanceltoGrid.Visible = False
        btnEmpRemoveToGrid.Enabled = True

        Label80.Text = ""
        cmbEmployerContact.SelectedValue = -1
        cmbEmployerCountry.SelectedValue = -1
        cmbEmpThana.SelectedIndex = -1
        txtEmployerAddress.Clear()
        cmbEmpDistrict.SelectedIndex = -1
        cmbEmpDivision.SelectedIndex = -1
        txtEmployerZip.Clear()
    End Sub

    Private Sub btnIdentificationAddToGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIdentificationAddToGrid.Click
       If cmbIdentification.Text.Trim() = "" Then
            MessageBox.Show("Identification Type required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbIdentification.Focus()
            Exit Sub
        ElseIf cmbIssueCountry.Text = "" Then
            MessageBox.Show("Issue Country Type required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbIssueCountry.Focus()
            Exit Sub

        ElseIf txtIssueDate.Text = "" Then
            MessageBox.Show("Issue Date required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtIssueDate.Focus()
            Exit Sub
        ElseIf txtExpiryDate.Text = "" Then
            MessageBox.Show("Expiry Date required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtExpiryDate.Focus()
            Exit Sub

        ElseIf txtNumber.Text.Trim() = "" Then
            MessageBox.Show("Identification Number required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtNumber.Focus()
            Exit Sub

        End If

        'For Each row As DataGridViewRow In DataGridView6.Rows
        '    If row.Index.ToString() <> lblRowNo.Text Then
        '        If row.Cells(0).Value.ToString().Trim() = txtIentID.Text.Trim() Then
        '            MessageBox.Show("Address Code Error", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Exit Sub
        '        End If
        '    End If

        'Next



        If _RowEditMode = True Then

            Dim selRow As Integer = lblRowNo.Text.Trim()

            DataGridView6.Item(0, selRow).Value = Label80.Text
            DataGridView6.Item(1, selRow).Value = cmbIdentification.SelectedValue
            DataGridView6.Item(2, selRow).Value = txtNumber.Text.Trim()
            DataGridView6.Item(3, selRow).Value = NullHelper.StringToDate(txtIssueDate.Text)
            DataGridView6.Item(4, selRow).Value = NullHelper.StringToDate(txtExpiryDate.Text)
            DataGridView6.Item(5, selRow).Value = txtIssuedBy.Text.Trim()
            DataGridView6.Item(6, selRow).Value = cmbIssueCountry.SelectedValue




            DataGridView6.Rows(0).Selected = True
            DataGridView6.Rows(0).Selected = False
            DataGridView6.Rows(selRow).Selected = True


        End If





        If _RowEditMode = False Then

            Dim MaxSlNo5 As Integer = 1
            Dim IdentificationStatus As String = "E"   ' E For Personal Identification Information in database '

            For Each row As DataGridViewRow In DataGridView6.Rows
                If MaxSlNo5 <= NullHelper.ToIntNum(row.Cells(7).Value) Then
                    MaxSlNo5 = NullHelper.ToIntNum(row.Cells(7).Value) + 1
                End If


            Next

            DataGridView6.Rows.Add()

            Dim maxRow As Integer = DataGridView6.Rows.Count - 1

            DataGridView6.Item(0, maxRow).Value = MaxSlNo5
            DataGridView6.Item(1, maxRow).Value = cmbIdentification.SelectedValue
            DataGridView6.Item(2, maxRow).Value = txtNumber.Text.Trim()
            DataGridView6.Item(3, maxRow).Value = NullHelper.StringToDate(txtIssueDate.Text)
            DataGridView6.Item(4, maxRow).Value = NullHelper.StringToDate(txtExpiryDate.Text)
            DataGridView6.Item(5, maxRow).Value = txtIssuedBy.Text.Trim()
            DataGridView6.Item(6, maxRow).Value = cmbIssueCountry.SelectedValue
            DataGridView6.Item(7, maxRow).Value = MaxSlNo5
            DataGridView6.Item(8, maxRow).Value = IdentificationStatus

            DataGridView6.Rows(0).Selected = True
            DataGridView6.Rows(0).Selected = False
            DataGridView6.Rows(maxRow).Selected = True

        End If
        btnCancelIdenticication.Visible = False
        btnRemoveIdentificationToGrid.Enabled = True

        Label80.Text = ""
        cmbIdentification.SelectedValue = -1
        cmbIssueCountry.SelectedValue = -1
        txtNumber.Clear()
        txtIssueDate.Clear()
        txtExpiryDate.Clear()
        txtIssuedBy.Clear()

    End Sub

    Private Sub DataGridView6_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView6.CellDoubleClick

        If Not (DataGridView6.SelectedRows.Item(0).Cells(0).Value Is Nothing Or DataGridView6.SelectedRows.Item(0).Cells(0).Value Is DBNull.Value) Then


            _RowEditMode = True

            btnIdentificationAddToGrid.Text = "Update"
            btnCancelIdenticication.Visible = True
            btnRemoveIdentificationToGrid.Enabled = False


            lblRowNo.Text = e.RowIndex.ToString()

            Label80.Text = DataGridView6.Item(0, e.RowIndex).Value
            cmbIdentification.SelectedValue = DataGridView6.Item(1, e.RowIndex).Value
            txtNumber.Text = DataGridView6.Item(2, e.RowIndex).Value

            txtIssueDate.Text = NullHelper.DateToStringNew(DataGridView6.Item(3, e.RowIndex).Value)
            txtExpiryDate.Text = NullHelper.DateToStringNew(DataGridView6.Item(4, e.RowIndex).Value)
            txtIssuedBy.Text = NullHelper.ObjectToString(DataGridView6.Item(5, e.RowIndex).Value)

            cmbIssueCountry.SelectedValue = DataGridView6.Item(6, e.RowIndex).Value



        End If

    End Sub


    Private Sub btnRemoveIdentificationToGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveIdentificationToGrid.Click

        If DataGridView6.SelectedRows.Count = 0 Then Exit Sub

        For Each row As DataGridViewRow In DataGridView6.SelectedRows
            DataGridView6.Rows.Remove(row)
        Next

    End Sub

    Private Sub btnCancelIdenticication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelIdenticication.Click
        _RowEditMode = False

        btnCancelIdenticication.Visible = False
        btnRemoveIdentificationToGrid.Enabled = True

        Label80.Text = ""
        cmbIdentification.SelectedValue = -1
        cmbIssueCountry.SelectedValue = -1
        txtNumber.Clear()
        txtIssueDate.Clear()
        txtExpiryDate.Clear()
        txtIssuedBy.Clear()
    End Sub


End Class