'
'Author             : Fahad Khan
'Purpose            : Maintain Entity Information
'Creation date      : 19-Dec-2013
'Stored Procedure(s):  

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization


Public Class FrmEntityPersonDet

#Region "Global Variables"

    Dim _formName As String = "MaintenanceGoEntityPersonDetail"
    Dim opt As SecForm = New SecForm(_formName)


    Dim _formMode As FormTransMode
    Dim _strId As String = ""
    Dim _intModno As Integer = 0
    Dim _mod_datetime As Date
    Dim _status As String = ""
    Dim _RowEditMode As Boolean = False
    Dim _IsResetRow As Boolean = False
    Dim log_message As String = ""
    Dim intmod As Integer = 0

    'For Update
    Dim _EName As String = ""
    Dim _CName As String = ""
    Dim _LegalType As String = ""
    Dim _IncorpNo As String = ""
    Dim _Bussiness As String = ""
    Dim _IncorpState As String = ""
    Dim _IncorpCountry As String = ""
    Dim _TaxNo As String = ""
    Dim _TaxRegNo As String = ""
    Dim _Email As String = ""
    Dim _Url As String = ""
    Dim _LegalTypeName As String = ""
    Dim _IncorpCountryName As String = ""

    'For Auth
    Dim _EntName As String = ""
    Dim _ComName As String = ""
    Dim _EnLegalType As String = ""
    Dim _IncNo As String = ""
    Dim _EntBussiness As String = ""
    Dim _IncState As String = ""
    Dim _IncCountry As String = ""
    Dim _TNo As String = ""
    Dim _TRegNo As String = ""
    Dim _Entemail As String = ""
    Dim _Enturl As String = ""
    Dim _EnLegalTypeName As String = ""
    Dim _IncCountryName As String = ""

    Dim EntityList As New List(Of String)
    Dim _EntityLog As String = ""
    Dim _Entlog As String = ""

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
        txtCommercialName.ReadOnly = True
        txtIncorporateNumber.ReadOnly = True
        txtBusiness.ReadOnly = True

        txtEmail.ReadOnly = True
        txtUrl.ReadOnly = True
        txtIncorporateState.ReadOnly = True
        txtIncorporateDate.ReadOnly = True
        txtBusinessClsDate.ReadOnly = True
        txttaxNumber.ReadOnly = True
        txtRegNo.ReadOnly = True

        cmbLegalType.Enabled = False
        cbmIncorporateCountry.Enabled = False
        cmbBusiness.Enabled = False

        ' Personal Phone
        TxtPhone.ReadOnly = True
        cmbPContact.Enabled = False
        cmbPCommunication.Enabled = False
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

        txtDirectorId.ReadOnly = True
        cmbEntityPersonRole.Enabled = False




        btnAddtoPersonPhone.Enabled = False
        btnRemovePhone.Enabled = False
        btnCancelPersonPhone.Visible = False

        btnAddPersonalAddressGrid.Enabled = False
        btnRemovePersonalAddtoGrid.Enabled = False
        btmCancelAddress.Visible = False

        btnAddtodirMap.Enabled = False
        btnRemoveDirMap.Enabled = False
        btnCancelDirMap.Visible = False




    End Sub

    Private Sub EnableFields()

        If _intModno = 0 Then
            txtId.ReadOnly = True
        End If

        txtName.ReadOnly = False
        txtCommercialName.ReadOnly = False
        txtIncorporateNumber.ReadOnly = False
        txtBusiness.ReadOnly = False

        txtEmail.ReadOnly = False
        txtUrl.ReadOnly = False
        txtIncorporateState.ReadOnly = False
        txtIncorporateDate.ReadOnly = False
        txtBusinessClsDate.ReadOnly = False
        txttaxNumber.ReadOnly = False
        txtRegNo.ReadOnly = False

        cmbLegalType.Enabled = True
        cbmIncorporateCountry.Enabled = True
        cmbBusiness.Enabled = True

        ' Personal Phone
        txtPhone.ReadOnly = False
        cmbPContact.Enabled = True
        cmbPCommunication.Enabled = True
        txtCountryPrefix.ReadOnly = False
        txtExtension.ReadOnly = False

        'Personal Address

        cmbPerAddress.Enabled = True
        cmbPersonalCountry.Enabled = True
        cmbPThana.Enabled = True
        cmbPDistrict.Enabled = True
        cmbPDivision.Enabled = True
        txtAddress.ReadOnly = False
        txtZip.ReadOnly = False

        txtDirectorId.ReadOnly = False
        cmbEntityPersonRole.Enabled = True


        btnAddtoPersonPhone.Enabled = True
        btnRemovePhone.Enabled = True
        btnCancelPersonPhone.Visible = True

        btnAddPersonalAddressGrid.Enabled = True
        btnRemovePersonalAddtoGrid.Enabled = True
        btmCancelAddress.Visible = True

        btnAddtodirMap.Enabled = True
        btnRemoveDirMap.Enabled = True
        btnCancelDirMap.Visible = True




    End Sub


    Private Sub ClearFields()

        txtId.Clear()
        txtName.Clear()
        txtCommercialName.Clear()
        txtIncorporateNumber.Clear()
        txtBusiness.Clear()

        txtEmail.Clear()
        txtUrl.Clear()
        txtIncorporateState.Clear()
        txtIncorporateDate.Clear()
        txtBusinessClsDate.Clear()
        txttaxNumber.Clear()
        txtRegNo.Clear()

        cmbLegalType.SelectedIndex = -1
        cbmIncorporateCountry.SelectedIndex = -1
        cmbBusiness.SelectedIndex = -1

        ' Personal Phone
        txtPhone.Clear()
        cmbPContact.SelectedIndex = -1
        cmbPCommunication.SelectedIndex = -1
        txtCountryPrefix.Clear()
        txtExtension.Clear()

        'Personal Address

        cmbPerAddress.SelectedIndex = -1
        cmbPersonalCountry.SelectedIndex = -1
        cmbPThana.SelectedIndex = -1
        cmbPDistrict.SelectedIndex = -1
        cmbPDivision.SelectedIndex = -1
        txtAddress.Clear()
        txtZip.Clear()

        txtDirectorId.Clear()
        cmbEntityPersonRole.SelectedIndex = -1


        DataGridView2.AllowUserToAddRows = False
        DataGridView4.AllowUserToAddRows = False
        DataGridView7.AllowUserToAddRows = False

        'dgView.DataSource = Nothing

    End Sub

    Private Sub ClearFieldsAll()

        txtId.Clear()
        txtName.Clear()
        txtCommercialName.Clear()
        txtIncorporateNumber.Clear()
        txtBusiness.Clear()

        txtEmail.Clear()
        txtUrl.Clear()
        txtIncorporateState.Clear()
        txtIncorporateDate.Clear()
        txtBusinessClsDate.Clear()
        txttaxNumber.Clear()
        txtRegNo.Clear()

        cmbLegalType.SelectedIndex = -1
        cbmIncorporateCountry.SelectedIndex = -1
        cmbBusiness.SelectedIndex = -1

        ' Personal Phone
        txtPhone.Clear()
        cmbPContact.SelectedIndex = -1
        cmbPCommunication.SelectedIndex = -1
        txtCountryPrefix.Clear()
        txtExtension.Clear()

        'Personal Address

        cmbPerAddress.SelectedIndex = -1
        cmbPersonalCountry.SelectedIndex = -1
        cmbPThana.SelectedIndex = -1
        cmbPDistrict.SelectedIndex = -1
        cmbPDivision.SelectedIndex = -1
        txtAddress.Clear()
        txtZip.Clear()

        txtDirectorId.Clear()
        cmbEntityPersonRole.SelectedIndex = -1


        DataGridView2.AllowUserToAddRows = False
        DataGridView4.AllowUserToAddRows = False
        DataGridView7.AllowUserToAddRows = False

        DataGridView2.DataSource = Nothing
        DataGridView2.Rows.Clear()

        DataGridView4.DataSource = Nothing
        DataGridView4.Rows.Clear()

        DataGridView7.DataSource = Nothing
        DataGridView7.Rows.Clear()




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
        If txtName.Text.Trim() = "" Then
            MessageBox.Show("Entity Name required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtName.Focus()
            Return False
        ElseIf cmbLegalType.Text.Trim() = "" Then
            MessageBox.Show("Entity Legal Type required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbLegalType.Focus()
            Return False

        ElseIf txtIncorporateNumber.Text.Trim() = "" Then
            MessageBox.Show("Incorporate  Number required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtIncorporateNumber.Focus()
            Return False

        ElseIf txtBusiness.Text.Trim() = "" Then
            MessageBox.Show("Business required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtBusiness.Focus()
            Return False

            'ElseIf txtBusiness.Text.Trim() = "" Then
            '    MessageBox.Show("Business required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    txtBusiness.Focus()
            '    Return False

        ElseIf cbmIncorporateCountry.Text.Trim() = "" Then
            MessageBox.Show("Incorporate Country required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cbmIncorporateCountry.Focus()
            Return False

        ElseIf txtIncorporateState.Text.Trim() = "" Then
            MessageBox.Show("Incorporate State required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtIncorporateState.Focus()
            Return False

        ElseIf DataGridView2.Rows.Count() = 0 Then

            MessageBox.Show(" Atleast Personal One Phone Number Required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return False

        ElseIf DataGridView4.Rows.Count() = 0 Then

            MessageBox.Show(" Atleast Personal One Address Required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return False



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


                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_MAX_Entity_ID")

                commProc2.Parameters.Clear()
                Dim maxid As String = db.ExecuteDataSet(commProc2, trans).Tables(0).Rows(0)(0).ToString()

                txtId.Text = maxid + 1


                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_Add")

                commProc.Parameters.Clear()


                db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, txtId.Text)
                db.AddInParameter(commProc, "@NAME", DbType.String, txtName.Text)
                db.AddInParameter(commProc, "@COMMERTIAL_NAME", DbType.String, txtCommercialName.Text())
                db.AddInParameter(commProc, "@INCORPORATION_LEGAL_FORM", DbType.String, cmbLegalType.SelectedValue)
                db.AddInParameter(commProc, "@INCORPORATION_NUMBER", DbType.String, txtIncorporateNumber.Text())
               
                db.AddInParameter(commProc, "@BUSINESS", DbType.String, txtBusiness.Text())
                db.AddInParameter(commProc, "@EMAIL", DbType.String, txtEmail.Text)
                db.AddInParameter(commProc, "@URL", DbType.String, txtUrl.Text)
                db.AddInParameter(commProc, "@INCORPORATION_STATE", DbType.String, txtIncorporateState.Text)
                db.AddInParameter(commProc, "@INCORPORATION_COUNTRY", DbType.String, cbmIncorporateCountry.SelectedValue)
                db.AddInParameter(commProc, "@INCORPORATION_DATE", DbType.DateTime, NullHelper.StringToDate(txtIncorporateDate.Text))

                If cmbBusiness.Text.Trim() = "" Then
                    db.AddInParameter(commProc, "@BUSINESS__CLOSE", DbType.String, 1)
                Else
                    db.AddInParameter(commProc, "@BUSINESS__CLOSE", DbType.String, cmbBusiness.SelectedIndex)
                End If



                db.AddInParameter(commProc, "@DATE_BUSINESS_CLOSE", DbType.DateTime, NullHelper.StringToDate(txtBusinessClsDate.Text))
                db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, txttaxNumber.Text)
                db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, txtRegNo.Text)



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

                    Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_EntityPhone_Add")

                    For i = 0 To DataGridView2.Rows.Count - 1

                        commProcSche.Parameters.Clear()


                        db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, DataGridView2.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcSche, "@ENTITY_ID", DbType.String, txtId.Text.Trim())
                        db.AddInParameter(commProcSche, "@SID", DbType.String, DataGridView2.Rows(i).Cells(7).Value)
                        db.AddInParameter(commProcSche, "@SLNO", DbType.String, DataGridView2.Rows(i).Cells(6).Value)
                        db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, DataGridView2.Rows(i).Cells(1).Value)
                        db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, DataGridView2.Rows(i).Cells(2).Value)
                        db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, DataGridView2.Rows(i).Cells(3).Value)
                        db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, DataGridView2.Rows(i).Cells(4).Value)
                        db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, DataGridView2.Rows(i).Cells(5).Value)

                        db.AddParameter(commProcSche, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                        db.ExecuteNonQuery(commProcSche, trans)

                        If db.GetParameterValue(commProcSche, "@PROC_RET_VAL") <> 0 Then

                            trans.Rollback()
                            Return TransState.UnspecifiedError

                        End If


                    Next

                    ' Add Person Address 

                    Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_EntityAddress_Add")

                    For i = 0 To DataGridView4.Rows.Count - 1

                        commProcAdd.Parameters.Clear()


                        db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, DataGridView4.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcAdd, "@ENTITY_ID", DbType.String, txtId.Text.Trim())
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

                    


                    'Add Mapping

                    Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_EntityDirectorMapping_Add")

                    For i = 0 To DataGridView7.Rows.Count - 1

                        commProcIdent.Parameters.Clear()


                        db.AddInParameter(commProcIdent, "@DIRECTOR_ID", DbType.String, DataGridView7.Rows(i).Cells(0).Value)
                        db.AddInParameter(commProcIdent, "@ENTITY_ID", DbType.String, txtId.Text.Trim())
                        db.AddInParameter(commProcIdent, "@ROLE", DbType.String, DataGridView7.Rows(i).Cells(1).Value)
                      

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

                    log_message = " Added : Entity ID : " + txtId.Text.Trim() + "." + " " + " Entity Name : " + txtName.Text.ToString()
                    Logger.system_log(log_message)

                End If


                trans.Commit()

                


            End Using


        ElseIf _formMode = FormTransMode.Update Then



            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_Update")

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, _strId)
                db.AddInParameter(commProc, "@NAME", DbType.String, txtName.Text)
                db.AddInParameter(commProc, "@COMMERTIAL_NAME", DbType.String, txtCommercialName.Text())
                db.AddInParameter(commProc, "@INCORPORATION_LEGAL_FORM", DbType.String, cmbLegalType.SelectedValue)
                db.AddInParameter(commProc, "@INCORPORATION_NUMBER", DbType.String, txtIncorporateNumber.Text())

                db.AddInParameter(commProc, "@BUSINESS", DbType.String, txtBusiness.Text())
                db.AddInParameter(commProc, "@EMAIL", DbType.String, txtEmail.Text)
                db.AddInParameter(commProc, "@URL", DbType.String, txtUrl.Text)
                db.AddInParameter(commProc, "@INCORPORATION_STATE", DbType.String, txtIncorporateState.Text)
                db.AddInParameter(commProc, "@INCORPORATION_COUNTRY", DbType.String, cbmIncorporateCountry.SelectedValue)
                db.AddInParameter(commProc, "@INCORPORATION_DATE", DbType.DateTime, NullHelper.StringToDate(txtIncorporateDate.Text))

                If cmbBusiness.Text.Trim() = "" Then
                    db.AddInParameter(commProc, "@BUSINESS__CLOSE", DbType.String, 1)
                Else
                    db.AddInParameter(commProc, "@BUSINESS__CLOSE", DbType.String, cmbBusiness.SelectedIndex)
                End If



                db.AddInParameter(commProc, "@DATE_BUSINESS_CLOSE", DbType.DateTime, NullHelper.StringToDate(txtBusinessClsDate.Text))
                db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, txttaxNumber.Text)
                db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, txtRegNo.Text)



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

                Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_EntityPhone_Update")


                For i = 0 To DataGridView2.Rows.Count - 1

                    commProcSche.Parameters.Clear()


                    db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, DataGridView2.Rows(i).Cells(0).Value)
                    db.AddInParameter(commProcSche, "@ENTITY_ID", DbType.String, _strId)
                    db.AddInParameter(commProcSche, "@SID", DbType.String, DataGridView2.Rows(i).Cells(7).Value)
                    db.AddInParameter(commProcSche, "@SLNO", DbType.String, DataGridView2.Rows(i).Cells(6).Value)
                    db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, DataGridView2.Rows(i).Cells(1).Value)
                    db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, DataGridView2.Rows(i).Cells(2).Value)
                    db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, DataGridView2.Rows(i).Cells(3).Value)
                    db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, DataGridView2.Rows(i).Cells(4).Value)
                    db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, DataGridView2.Rows(i).Cells(5).Value)
                    db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, intModno)




                    db.AddParameter(commProcSche, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                    db.ExecuteNonQuery(commProcSche, trans)

                    If db.GetParameterValue(commProcSche, "@PROC_RET_VAL") <> 0 Then

                        trans.Rollback()
                        Return TransState.UnspecifiedError

                    End If


                Next

                ' Update Person Address 

                Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_EntityAddress_Update")

                For i = 0 To DataGridView4.Rows.Count - 1

                    commProcAdd.Parameters.Clear()


                    db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, DataGridView4.Rows(i).Cells(0).Value)
                    db.AddInParameter(commProcAdd, "@ENTITY_ID", DbType.String, _strId)
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

               

                ''Update(Mapping)

                Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_DirectorEntityMapping_Update")

                For i = 0 To DataGridView7.Rows.Count - 1

                    commProcIdent.Parameters.Clear()


                    db.AddInParameter(commProcIdent, "@DIRECTOR_ID", DbType.String, DataGridView7.Rows(i).Cells(0).Value)
                    db.AddInParameter(commProcIdent, "@ENTITY_ID", DbType.String, _strId)
                    db.AddInParameter(commProcIdent, "@ROLE", DbType.String, DataGridView7.Rows(i).Cells(1).Value)
                   
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

                '----------------------Mizan Work (20-04-16)---------------------------

                If _EName <> txtName.Text.Trim() Then
                    log_message = " Entity Name : " + _EName + " " + " To " + " " + txtName.Text.ToString() + "." + " "
                    EntityList.Add(log_message)
                End If
                If _CName <> txtCommercialName.Text.Trim() Then
                    log_message = " Commercial Name : " + _CName + " " + " To " + " " + txtCommercialName.Text.Trim() + "." + " "
                    EntityList.Add(log_message)
                End If


                If _LegalType <> cmbLegalType.Text Then
                    log_message = " Legal Type : " + _LegalType + " " + " To " + " " + cmbLegalType.Text + "." + " "
                    EntityList.Add(log_message)
                End If
                If _IncorpNo <> txtIncorporateNumber.Text.Trim() Then
                    log_message = " Incorporate Number : " + _IncorpNo + " " + " To " + " " + txtIncorporateNumber.Text.ToString() + "." + " "
                    EntityList.Add(log_message)
                End If
                If _Bussiness <> txtBusiness.Text.Trim() Then
                    log_message = " Business : " + _Bussiness + " " + " To " + " " + txtBusiness.Text.ToString() + "." + " "
                    EntityList.Add(log_message)
                End If


                If _IncorpState <> txtIncorporateState.Text.Trim() Then
                    log_message = " Incorporate State : " + _IncorpState + " " + " To " + " " + txtIncorporateState.Text.ToString() + "." + " "
                    EntityList.Add(log_message)
                End If
                If _IncorpCountry <> cbmIncorporateCountry.Text Then
                    log_message = " Incorporate Country : " + _IncorpCountry + " " + " To " + " " + cbmIncorporateCountry.Text + "." + " "
                    EntityList.Add(log_message)
                End If


                If _TaxNo <> txttaxNumber.Text.Trim() Then
                    If _TaxNo = "" Then
                        log_message = " Tax No : " + txttaxNumber.Text.ToString() + "." + " "
                    Else
                        log_message = " Tax No : " + _TaxNo + " " + " To " + " " + txttaxNumber.Text.ToString() + "." + " "
                    End If
                    EntityList.Add(log_message)
                End If
                If _TaxRegNo <> txtRegNo.Text.Trim() Then
                    If _TaxRegNo = "" Then
                        log_message = " Tax Reg No : " + txtRegNo.Text.Trim() + "." + " "
                    Else
                        log_message = " Tax Reg No : " + _TaxRegNo + " " + " To " + " " + txtRegNo.Text.ToString() + "." + " "
                    End If
                    EntityList.Add(log_message)
                End If
                If _Email <> txtEmail.Text.Trim() Then
                    If _Email = "" Then
                        log_message = " Email :" + txtEmail.Text.Trim() + "." + " "
                    Else
                        log_message = " Email : " + _Email + " " + " To " + " " + txtEmail.Text.ToString() + "." + " "
                    End If
                    EntityList.Add(log_message)
                End If
                If _Url <> txtUrl.Text.Trim() Then
                    If _Url = "" Then
                        log_message = " URL : " + txtUrl.Text.ToString() + "." + " "
                    Else
                        log_message = " URL : " + _Url + " " + " To " + " " + txtUrl.Text.ToString() + "." + " "
                    End If
                    EntityList.Add(log_message)
                End If


                For Each Entloglist As String In EntityList
                    _EntityLog += Entloglist
                Next

                _Entlog = " Updated : Entity ID : " + txtId.Text.Trim() + "." + " " + _EntityLog

                Logger.system_log(_Entlog)
                _EntityLog = ""
                EntityList.Clear()

                '----------------------Mizan Work (20-04-16)---------------------------

                Return tStatus

                trans.Rollback()

            End Using

        End If

        Return tStatus


    End Function

    '----------------------Mizan Work (20-04-16)---------------------------

    Private Sub LoadMainDataForAuth(ByVal strId As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From GO_T_ENTITY Where ENTITY_ID ='" & strId & "' and STATUS ='L' ")

            If ds.Tables(0).Rows.Count > 0 Then

                _strId = strId


                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("ENTITY_ID").ToString()

                txtName.Text = ds.Tables(0).Rows(0)("NAME")
                _EntName = ds.Tables(0).Rows(0)("NAME")
                txtIncorporateNumber.Text = ds.Tables(0).Rows(0)("INCORPORATION_NUMBER")
                _IncNo = ds.Tables(0).Rows(0)("INCORPORATION_NUMBER").ToString()
                txtIncorporateState.Text = ds.Tables(0).Rows(0)("INCORPORATION_STATE")
                _IncState = ds.Tables(0).Rows(0)("INCORPORATION_STATE").ToString()

                txtCommercialName.Text = ds.Tables(0).Rows(0)("COMMERTIAL_NAME")
                _ComName = ds.Tables(0).Rows(0)("COMMERTIAL_NAME").ToString()

                ''cmbLegalType.SelectedValue = ds.Tables(0).Rows(0)("INCORPORATION_LEGAL_FORM")
                _EnLegalType = ds.Tables(0).Rows(0)("INCORPORATION_LEGAL_FORM").ToString()
                txtBusiness.Text = ds.Tables(0).Rows(0)("BUSINESS")
                _EntBussiness = ds.Tables(0).Rows(0)("BUSINESS").ToString()


                txtEmail.Text = ds.Tables(0).Rows(0)("EMAIL").ToString()
                _Entemail = ds.Tables(0).Rows(0)("EMAIL").ToString()
                txtUrl.Text = ds.Tables(0).Rows(0)("URL").ToString()
                _Enturl = ds.Tables(0).Rows(0)("URL").ToString()

                ''cbmIncorporateCountry.SelectedValue = ds.Tables(0).Rows(0)("INCORPORATION_COUNTRY").ToString()
                _IncCountry = ds.Tables(0).Rows(0)("INCORPORATION_COUNTRY").ToString()

                txttaxNumber.Text = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()
                _TNo = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()

                txtRegNo.Text = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString()
                _TRegNo = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString()

                ''------------------Mizan Work (26-04-16) ---------------------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_ENTITY_TYPE Where ENTYPE_CODE = '" & _EnLegalType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _EnLegalTypeName = ds2.Tables(0).Rows(0)("ENTYPE_NAME").ToString()
                    _EnLegalType = _EnLegalTypeName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE = '" & _IncCountry & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _IncCountryName = ds3.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _IncCountry = _IncCountryName

                End If



                ''------------------Mizan Work (26-04-16) ----------------------

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

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, strId)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intmod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then

                _strId = strId
                _intModno = intmod

                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("ENTITY_ID").ToString()

                txtName.Text = ds.Tables(0).Rows(0)("NAME")
                _EName = ds.Tables(0).Rows(0)("NAME")
                txtIncorporateNumber.Text = ds.Tables(0).Rows(0)("INCORPORATION_NUMBER")
                _IncorpNo = ds.Tables(0).Rows(0)("INCORPORATION_NUMBER").ToString()
                txtIncorporateState.Text = ds.Tables(0).Rows(0)("INCORPORATION_STATE")
                _IncorpState = ds.Tables(0).Rows(0)("INCORPORATION_STATE").ToString()
                txtIncorporateDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("INCORPORATION_DATE"))
                txtCommercialName.Text = ds.Tables(0).Rows(0)("COMMERTIAL_NAME")
                _CName = ds.Tables(0).Rows(0)("COMMERTIAL_NAME").ToString()

                cmbLegalType.SelectedValue = ds.Tables(0).Rows(0)("INCORPORATION_LEGAL_FORM")
                _LegalType = ds.Tables(0).Rows(0)("INCORPORATION_LEGAL_FORM").ToString()
                txtBusiness.Text = ds.Tables(0).Rows(0)("BUSINESS")
                _Bussiness = ds.Tables(0).Rows(0)("BUSINESS").ToString()


                txtEmail.Text = ds.Tables(0).Rows(0)("EMAIL").ToString()
                _Email = ds.Tables(0).Rows(0)("EMAIL").ToString()
                txtUrl.Text = ds.Tables(0).Rows(0)("URL").ToString()
                _Url = ds.Tables(0).Rows(0)("URL").ToString()

                cbmIncorporateCountry.SelectedValue = ds.Tables(0).Rows(0)("INCORPORATION_COUNTRY").ToString()
                _IncorpCountry = ds.Tables(0).Rows(0)("INCORPORATION_COUNTRY").ToString()


                If ds.Tables(0).Rows(0)("BUSINESS__CLOSE") = True Then

                    cmbBusiness.Text = "No"
                Else

                    cmbBusiness.Text = "Yes"
                End If


                txtBusinessClsDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("DATE_BUSINESS_CLOSE"))


                txttaxNumber.Text = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()
                _TaxNo = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()

                txtRegNo.Text = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString()
                _TaxRegNo = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString()


                ''------------------Mizan Work (26-04-16) ---------------------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_ENTITY_TYPE Where ENTYPE_CODE = '" & _LegalType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _LegalTypeName = ds2.Tables(0).Rows(0)("ENTYPE_NAME").ToString()
                    _LegalType = _LegalTypeName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE = '" & _IncorpCountry & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _IncorpCountryName = ds3.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _IncorpCountry = _IncorpCountryName

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

                LoadPersonalPhoneData(strId, intmod)
                LoadPersonAddressData(strId, intmod)
                LoadDirectorEntityMappingData(strId, intmod)


                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_ENTITY_GetMaxMod")

                commProc2.Parameters.Clear()

                db.AddInParameter(commProc2, "@ENTITY_ID", DbType.String, strId)

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
            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_EntityPhone_GetDetails")

            commProcSche.Parameters.Clear()

            db.AddInParameter(commProcSche, "@ENTITY_ID", DbType.String, strId)
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

            DataGridView2.Rows.Clear()

            If dt.Rows.Count > 0 Then

                DataGridView2.AllowUserToAddRows = True
                For i = 0 To dt.Rows.Count - 1
                    If (i = DataGridView2.Rows.Count - 1) Then
                        DataGridView2.Rows.Add()
                    End If
                    DataGridView2.Item(0, i).Value = dt.Rows(i)("TPH_ID")
                    DataGridView2.Item(1, i).Value = dt.Rows(i)("TPH_CONTACT_TYPE")
                    DataGridView2.Item(2, i).Value = dt.Rows(i)("TPH_COMMUNICATION_TYPE")
                    DataGridView2.Item(3, i).Value = dt.Rows(i)("TPH_COUNTRY_PREFIX")
                    DataGridView2.Item(4, i).Value = dt.Rows(i)("TPH_NUMBER")
                    DataGridView2.Item(5, i).Value = dt.Rows(i)("TPH_EXTENSION")
                    DataGridView2.Item(6, i).Value = dt.Rows(i)("SLNO")
                    DataGridView2.Item(7, i).Value = dt.Rows(i)("SID")


                Next
                DataGridView2.AllowUserToAddRows = False

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
            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_EntityAddress_GetDetails")

            commProcSche.Parameters.Clear()

            db.AddInParameter(commProcSche, "@ENTITY_ID", DbType.String, strId)
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

   
    Private Sub LoadDirectorEntityMappingData(ByVal strId As String, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim dt As New DataTable
            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_DirectorEntityMapping_GetDetails")

            commProcSche.Parameters.Clear()

            db.AddInParameter(commProcSche, "@ENTITY_ID", DbType.String, strId)
            'db.AddInParameter(commProcSche, "@SID", DbType.String, Status)
            db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, intmod)

            dt = db.ExecuteDataSet(commProcSche).Tables(0)



            DataGridView7.Rows.Clear()

            If dt.Rows.Count > 0 Then

                DataGridView7.AllowUserToAddRows = True
                For i = 0 To dt.Rows.Count - 1
                    If (i = DataGridView7.Rows.Count - 1) Then
                        DataGridView7.Rows.Add()
                    End If
                    DataGridView7.Item(0, i).Value = dt.Rows(i)("DIRECTOR_ID")
                    DataGridView7.Item(1, i).Value = dt.Rows(i)("ROLE")
                   

                Next
                DataGridView7.AllowUserToAddRows = False

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

    '----------------------Mizan Work (20-04-16)---------------------------

    Private Function AuthorizeData() As TransState

        If _intModno > 1 Then

            LoadMainDataForAuth(_strId)

            Dim tStatus As TransState

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, _strId)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                '----------------------Mizan Work (20-04-16)---------------------------

                If _EntName <> _EName Then
                    If _EntName = "" Then
                        log_message = " Entity Name : " + _EName + "." + " "
                    Else
                        log_message = " Entity Name : " + _EntName + " " + " To " + " " + _EName + "." + " "
                    End If

                    EntityList.Add(log_message)
                End If
                If _ComName <> _CName Then
                    If _ComName = "" Then
                        log_message = " Commercial Name : " + _CName + "." + " "
                    Else
                        log_message = " Commercial Name : " + _ComName + " " + " To " + " " + _CName + "." + " "
                    End If

                    EntityList.Add(log_message)
                End If


                If _EnLegalType <> _LegalType Then
                    log_message = " Legal Type : " + _EnLegalType + " " + " To " + " " + _LegalType + "." + " "
                    EntityList.Add(log_message)
                End If
                If _IncNo <> _IncorpNo Then
                    If _IncNo = "" Then
                        log_message = " Incorporate Number : " + _IncorpNo + "." + " "
                    Else
                        log_message = " Incorporate Number : " + _IncNo + " " + " To " + " " + _IncorpNo + "." + " "
                    End If

                    EntityList.Add(log_message)
                End If
                If _EntBussiness <> _Bussiness Then
                    If _EntBussiness = "" Then
                        log_message = " Business : " + _Bussiness + "." + " "
                    Else
                        log_message = " Business : " + _EntBussiness + " " + " To " + " " + _Bussiness + "." + " "
                    End If

                    EntityList.Add(log_message)
                End If


                If _IncState <> _IncorpState Then
                    If _IncState = "" Then
                        log_message = " Incorporate State : " + _IncorpState + "." + " "
                    Else
                        log_message = " Incorporate State : " + _IncState + " " + " To " + " " + _IncorpState + "." + " "
                    End If

                    EntityList.Add(log_message)
                End If
                If _IncCountry <> _IncorpCountry Then
                    log_message = " Incorporate Country : " + _IncCountry + " " + " To " + " " + _IncorpCountry + "." + " "
                    EntityList.Add(log_message)
                End If


                If _TNo <> _TaxNo Then
                    If _TNo = "" Then
                        log_message = " Tax No : " + _TaxNo + "." + " "
                    Else
                        log_message = " Tax No : " + _TNo + " " + " To " + " " + _TaxNo + "." + " "
                    End If
                    EntityList.Add(log_message)
                End If
                If _TRegNo <> _TaxRegNo Then
                    If _TRegNo = "" Then
                        log_message = " Tax Reg No : " + _TaxRegNo + "." + " "
                    Else
                        log_message = " Tax Reg No : " + _TRegNo + " " + " To " + " " + _TaxRegNo + "." + " "
                    End If
                    EntityList.Add(log_message)
                End If
                If _Entemail <> _Email Then
                    If _Entemail = "" Then
                        log_message = " Email :" + _Email + "." + " "
                    Else
                        log_message = " Email : " + _Entemail + " " + " To " + " " + _Email + "." + " "
                    End If
                    EntityList.Add(log_message)
                End If
                If _Enturl <> _Url Then
                    If _Enturl = "" Then
                        log_message = " URL : " + _Url + "." + " "
                    Else
                        log_message = " URL : " + _Enturl + " " + " To " + " " + _Url + "." + " "
                    End If
                    EntityList.Add(log_message)
                End If


                For Each Entloglist As String In EntityList
                    _EntityLog += Entloglist
                Next

                _Entlog = " Authorized : Entity ID : " + txtId.Text.Trim() + "." + " " + _EntityLog

                Logger.system_log(_Entlog)
                _EntityLog = ""
                EntityList.Clear()

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

           

            'log_message = "Authorized Entity " + _strId + " Name " + txtName.Text.ToString()
            'Logger.system_log(log_message)

            Return tStatus

        Else

            Dim tStatus As TransState

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, _strId)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                '----------------------Mizan Work (20-04-16)---------------------------

                If _EntName <> _EName Then
                    If _EntName = "" Then
                        log_message = " Entity Name : " + _EName + "." + " "
                    Else
                        log_message = " Entity Name : " + _EntName + " " + " To " + " " + _EName + "." + " "
                    End If

                    EntityList.Add(log_message)
                End If
                If _ComName <> _CName Then
                    If _ComName = "" Then
                        'log_message = " Commercial Name : " + _CName + "." + " "
                    Else
                        log_message = " Commercial Name : " + _ComName + " " + " To " + " " + _CName + "." + " "
                        EntityList.Add(log_message)
                    End If

                End If


                If _EnLegalType <> _LegalType Then
                    log_message = " Legal Type : " + _EnLegalType + " " + " To " + " " + _LegalType + "." + " "
                    EntityList.Add(log_message)
                End If
                If _IncNo <> _IncorpNo Then
                    If _IncNo = "" Then
                        log_message = " Incorporate Number : " + _IncorpNo + "." + " "
                    Else
                        log_message = " Incorporate Number : " + _IncNo + " " + " To " + " " + _IncorpNo + "." + " "
                    End If

                    EntityList.Add(log_message)
                End If
                If _EntBussiness <> _Bussiness Then
                    If _EntBussiness = "" Then
                        log_message = " Business : " + _Bussiness + "." + " "
                    Else
                        log_message = " Business : " + _EntBussiness + " " + " To " + " " + _Bussiness + "." + " "
                    End If

                    EntityList.Add(log_message)
                End If


                If _IncState <> _IncorpState Then
                    If _IncState = "" Then
                        'log_message = " Incorporate State : " + _IncorpState + "." + " "
                    Else
                        log_message = " Incorporate State : " + _IncState + " " + " To " + " " + _IncorpState + "." + " "
                        EntityList.Add(log_message)
                    End If


                End If
                If _IncCountry <> _IncorpCountry Then
                    If _IncCountry = "" Then
                    Else
                        log_message = " Incorporate Country : " + _IncCountry + " " + " To " + " " + _IncorpCountry + "." + " "
                        EntityList.Add(log_message)
                    End If


                End If


                If _TNo <> _TaxNo Then
                    If _TNo = "" Then
                        ' log_message = " Tax No : " + _TaxNo + "." + " "
                    Else
                        log_message = " Tax No : " + _TNo + " " + " To " + " " + _TaxNo + "." + " "
                        EntityList.Add(log_message)
                    End If

                End If
                If _TRegNo <> _TaxRegNo Then
                    If _TRegNo = "" Then
                        'log_message = " Tax Reg No : " + _TaxRegNo + "." + " "
                    Else
                        log_message = " Tax Reg No : " + _TRegNo + " " + " To " + " " + _TaxRegNo + "." + " "
                        EntityList.Add(log_message)
                    End If

                End If
                If _Entemail <> _Email Then
                    If _Entemail = "" Then
                        'log_message = " Email :" + _Email + "." + " "
                    Else
                        log_message = " Email : " + _Entemail + " " + " To " + " " + _Email + "." + " "
                        EntityList.Add(log_message)
                    End If

                End If
                If _Enturl <> _Url Then
                    If _Enturl = "" Then
                        'log_message = " URL : " + _Url + "." + " "
                    Else
                        log_message = " URL : " + _Enturl + " " + " To " + " " + _Url + "." + " "
                        EntityList.Add(log_message)
                    End If

                End If


                For Each Entloglist As String In EntityList
                    _EntityLog += Entloglist
                Next

                _Entlog = " Authorized : Entity ID : " + txtId.Text.Trim() + "." + " " + " For Mandatory Field : " + "." + " " + _EntityLog

                Logger.system_log(_Entlog)
                _EntityLog = ""
                EntityList.Clear()

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

            

            'log_message = "Authorized Entity " + _strId + " Name " + txtName.Text.ToString()
            'Logger.system_log(log_message)

            Return tStatus

        End If

    End Function

    Private Function DeleteData() As TransState

        Dim tStatus As TransState

        Dim intModno As Integer = 0

        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_Remove")

        commProc.Parameters.Clear()

        db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, _strId)
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

        log_message = "Authorized Entity " + _strId + " Name " + txtName.Text.ToString()
        Logger.system_log(log_message)

        Return tStatus

    End Function


#End Region




    Private Sub FrmEntityPersonDet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If



        CommonUtil.FillComboBox("GO_EntityType_GetList", cmbLegalType)
        CommonUtil.FillComboBox("GO_CountryType_GetList", cbmIncorporateCountry)

        'CommonUtil.FillComboBox("GO_Director_GetList", cbmIncorporateCountry)

        CommonUtil.FillComboBox("GO_ContactType_GetList", cmbPContact)
        CommonUtil.FillComboBox("GO_CommunicationType_GetList", cmbPCommunication)

        CommonUtil.FillComboBox("GO_ContactType_GetList", cmbPerAddress)
        CommonUtil.FillComboBox("GO_CountryType_GetList", cmbPersonalCountry)
        CommonUtil.FillComboBox("GO_ThanaType_GetList", cmbPThana)
        CommonUtil.FillComboBox("GO_DistrictType_GetList", cmbPDistrict)
        CommonUtil.FillComboBox("GO_DivisionType_GetList", cmbPDivision)

        CommonUtil.FillComboBox("GO_EntityPersonRoleType_GetList", cmbEntityPersonRole)

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


        DataGridView2.Enabled = False
        DataGridView4.Enabled = False
        DataGridView7.Enabled = False




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


        DataGridView2.Enabled = True
        DataGridView4.Enabled = True
        DataGridView7.Enabled = True

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
            txtName.Focus()
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

    Private Sub btnAddtoPersonPhone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddtoPersonPhone.Click

        If cmbPContact.Text.Trim() = "" Then
            MessageBox.Show("Contact Type required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbPContact.Focus()
            Exit Sub
        ElseIf cmbPCommunication.Text = "" Then
            MessageBox.Show("Communication Code required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbPCommunication.Focus()
            Exit Sub
        ElseIf txtPhone.Text.Trim() = "" Then
            MessageBox.Show("Phone Number required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPhone.Focus()
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

            DataGridView2.Item(0, selRow).Value = Label18.Text
            DataGridView2.Item(1, selRow).Value = cmbPContact.SelectedValue
            DataGridView2.Item(2, selRow).Value = cmbPCommunication.SelectedValue
            DataGridView2.Item(3, selRow).Value = txtCountryPrefix.Text.Trim()
            DataGridView2.Item(4, selRow).Value = txtPhone.Text.Trim()
            DataGridView2.Item(5, selRow).Value = txtExtension.Text.Trim()






            DataGridView2.Rows(0).Selected = True
            DataGridView2.Rows(0).Selected = False
            DataGridView2.Rows(selRow).Selected = True


        End If

        If _RowEditMode = False Then

    Dim MaxSlNo As Integer = 1
    Dim PhoneStatus As String = "P"   ' P For Personal Phone Information in database '

            For Each row As DataGridViewRow In DataGridView2.Rows
                If MaxSlNo <= NullHelper.ToIntNum(row.Cells(6).Value) Then
                    MaxSlNo = NullHelper.ToIntNum(row.Cells(6).Value) + 1
                End If


            Next

            DataGridView2.Rows.Add()

    Dim maxRow As Integer = DataGridView2.Rows.Count - 1

            DataGridView2.Item(0, maxRow).Value = MaxSlNo
            DataGridView2.Item(1, maxRow).Value = cmbPContact.SelectedValue
            DataGridView2.Item(2, maxRow).Value = cmbPCommunication.SelectedValue
            DataGridView2.Item(3, maxRow).Value = txtCountryPrefix.Text.Trim()
            DataGridView2.Item(4, maxRow).Value = txtPhone.Text.Trim()
            DataGridView2.Item(5, maxRow).Value = txtExtension.Text.Trim()
            DataGridView2.Item(6, maxRow).Value = MaxSlNo
            DataGridView2.Item(7, maxRow).Value = PhoneStatus

            DataGridView2.Rows(0).Selected = True
            DataGridView2.Rows(0).Selected = False
            DataGridView2.Rows(maxRow).Selected = True

        End If
        btnCancelPersonPhone.Visible = False
        btnRemovePhone.Enabled = True
        Label18.Text = ""
        cmbPContact.SelectedValue = -1
        cmbPCommunication.SelectedValue = -1
        txtCountryPrefix.Clear()
        txtPhone.Clear()
        txtExtension.Clear()


    End Sub


    Private Sub DataGridView2_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick

        If Not (DataGridView2.SelectedRows.Item(0).Cells(0).Value Is Nothing Or DataGridView2.SelectedRows.Item(0).Cells(0).Value Is DBNull.Value) Then


            _RowEditMode = True

            btnAddtoPersonPhone.Text = "Update"
            btnCancelPersonPhone.Visible = True
            btnRemovePhone.Enabled = False


            lblRowNo.Text = e.RowIndex.ToString()

            Label18.Text = DataGridView2.Item(0, e.RowIndex).Value.ToString()
            cmbPContact.SelectedValue = DataGridView2.Item(1, e.RowIndex).Value
            cmbPCommunication.SelectedValue = DataGridView2.Item(2, e.RowIndex).Value
            txtCountryPrefix.Text = DataGridView2.Item(3, e.RowIndex).Value.ToString()
            txtPhone.Text = DataGridView2.Item(4, e.RowIndex).Value.ToString()
            txtExtension.Text = DataGridView2.Item(5, e.RowIndex).Value.ToString()




        End If
    End Sub

    Private Sub btnRemovePhone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePhone.Click
        If DataGridView2.SelectedRows.Count = 0 Then Exit Sub

        For Each row As DataGridViewRow In DataGridView2.SelectedRows
            DataGridView2.Rows.Remove(row)
        Next
    End Sub

    Private Sub btnCancelPersonPhone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelPersonPhone.Click
        _RowEditMode = False

        btnCancelPersonPhone.Visible = False
        btnRemovePhone.Enabled = True

        Label18.Text = ""
        cmbPContact.SelectedValue = -1
        cmbPCommunication.SelectedValue = -1
        txtCountryPrefix.Clear()
        txtPhone.Clear()
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
            MessageBox.Show("Town required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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


            DataGridView4.Item(0, selRow).Value = Label81.Text
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

        Label81.Text = ""
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

            Label81.Text = DataGridView4.Item(0, e.RowIndex).Value.ToString
            cmbPerAddress.SelectedValue = DataGridView4.Item(1, e.RowIndex).Value
            txtAddress.Text = DataGridView4.Item(2, e.RowIndex).Value.ToString

            cmbPThana.Text = DataGridView4.Item(3, e.RowIndex).Value.ToString
            cmbPDistrict.Text = DataGridView4.Item(4, e.RowIndex).Value.ToString
            txtZip.Text = DataGridView4.Item(5, e.RowIndex).Value.ToString

            cmbPersonalCountry.SelectedValue = DataGridView4.Item(6, e.RowIndex).Value
            cmbPDivision.Text = DataGridView4.Item(7, e.RowIndex).Value.ToString



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

        Label81.Text = ""
        cmbPerAddress.SelectedValue = -1
        cmbPersonalCountry.SelectedValue = -1
        cmbPThana.SelectedIndex = -1
        cmbPDistrict.SelectedIndex = -1
        cmbPDivision.SelectedIndex = -1
        txtAddress.Clear()
        txtZip.Clear()
    End Sub



    Private Sub btnSearchDirector_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDirector.Click
        Dim frmList As New FrmList()
        frmList.Text = "Director List"
        frmList.ProcName = "GO_Director_GetList"
        frmList.filter = New String(,) {{"DIRECTOR_ID", "Director Code"}, {"DIR_NAME", "Director Name"}}
        frmList.colwidth = New Integer(,) {{1, 300}}
        frmList.colrename = New String(,) {{"0", "Director ID"}, {"1", "Director Name"}}
        frmList.ShowDialog()

        If (frmList.RowResult.Cells.Count > 0) Then

            txtDirectorId.Text = frmList.RowResult.Cells(0).Value.ToString()
            'lblDirectorName.Text = frmList.RowResult.Cells(1).Value.ToString()

            'txtDirectorId.Text = ""
            'lblDirectorName.Text = ""


            SendKeys.Send("{tab}")
        End If

        frmList.Dispose()

    End Sub

   

    Private Sub txtDirectorId_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDirectorId.KeyDown
        

        If e.KeyCode = Keys.Enter Then
            If txtDirectorId.Text.Trim() = "" Then

                Dim frmList As New FrmList()
                frmList.Text = "Director List"
                frmList.ProcName = "GO_Director_GetList"
                frmList.filter = New String(,) {{"DIRECTOR_ID", "Director Code"}, {"DIR_NAME", "Director Name"}}
                frmList.colwidth = New Integer(,) {{1, 300}}
                frmList.colrename = New String(,) {{"0", "Director ID"}, {"1", "Director Name"}}
                frmList.ShowDialog()

                If (frmList.RowResult.Cells.Count > 0) Then

                    txtDirectorId.Text = frmList.RowResult.Cells(0).Value.ToString()
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

                    Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_GetListByCode")

                    commProc.Parameters.Clear()

                    db.AddInParameter(commProc, "@DIRECTOR_ID", DbType.String, txtDirectorId.Text.Trim())

                    dt = db.ExecuteDataSet(commProc).Tables(0)

                    If dt.Rows.Count > 0 Then
                        lblDirectorName.Text = dt.Rows(0)("DIR_NAME").ToString()
                    Else
                        txtDirectorId.Clear()
                        lblDirectorName.Text = ""
                    End If


                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try


            End If

            If txtDirectorId.Text.Trim() <> "" Then
                SendKeys.Send("{tab}")
                SendKeys.Send("{tab}")
            End If

        End If



    End Sub


    Private Sub btnAddtodirMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddtodirMap.Click

        If txtDirectorId.Text.Trim() = "" Then
            MessageBox.Show("Director ID required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtDirectorId.Focus()
            Exit Sub
        ElseIf cmbEntityPersonRole.Text = "" Then
            MessageBox.Show("Role required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbEntityPersonRole.Focus()
            Exit Sub
       

        End If


        If _RowEditMode = True Then

            Dim selRow As Integer = lblRowNo3.Text.Trim()

            DataGridView7.Item(0, selRow).Value = txtDirectorId.Text.Trim()
            DataGridView7.Item(1, selRow).Value = cmbEntityPersonRole.SelectedValue
          




            DataGridView7.Rows(0).Selected = True
            DataGridView7.Rows(0).Selected = False
            DataGridView7.Rows(selRow).Selected = True


        End If





        If _RowEditMode = False Then

            'Dim MaxSlNo5 As Integer = 1

            'For Each row As DataGridViewRow In DataGridView7.Rows
            '    If MaxSlNo5 <= NullHelper.ToIntNum(row.Cells(2).Value) Then
            '        MaxSlNo5 = NullHelper.ToIntNum(row.Cells(2).Value) + 1
            '    End If


            'Next

            DataGridView7.Rows.Add()

            Dim maxRow As Integer = DataGridView7.Rows.Count - 1


            DataGridView7.Item(0, maxRow).Value = txtDirectorId.Text
            DataGridView7.Item(1, maxRow).Value = cmbEntityPersonRole.SelectedValue
            ' DataGridView7.Item(2, maxRow).Value = MaxSlNo5
            
            DataGridView7.Rows(0).Selected = True
            DataGridView7.Rows(0).Selected = False
            DataGridView7.Rows(maxRow).Selected = True

        End If

        btnCancelDirMap.Visible = False
        btnRemoveDirMap.Enabled = True


        cmbEntityPersonRole.SelectedValue = -1
        txtDirectorId.Clear()




    End Sub

    Private Sub DataGridView7_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView7.CellDoubleClick
        If Not (DataGridView7.SelectedRows.Item(0).Cells(0).Value Is Nothing Or DataGridView7.SelectedRows.Item(0).Cells(0).Value Is DBNull.Value) Then


            _RowEditMode = True

            btnAddtodirMap.Text = "Update"
            btnCancelDirMap.Visible = True
            btnRemoveDirMap.Enabled = False


            lblRowNo3.Text = e.RowIndex.ToString()

            txtDirectorId.Text = DataGridView7.Item(0, e.RowIndex).Value.ToString
            cmbEntityPersonRole.SelectedValue = DataGridView7.Item(1, e.RowIndex).Value


        End If
    End Sub




    Private Sub btnCancelDirMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelDirMap.Click
        _RowEditMode = False

        btnCancelDirMap.Visible = False
        btnRemoveDirMap.Enabled = True
        cmbEntityPersonRole.SelectedValue = -1

        txtDirectorId.Clear()
        
    End Sub

    Private Sub btnRemoveDirMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveDirMap.Click
        If DataGridView7.SelectedRows.Count = 0 Then Exit Sub

        For Each row As DataGridViewRow In DataGridView7.SelectedRows
            DataGridView7.Rows.Remove(row)
        Next
    End Sub

    Private Sub btnGetDirector_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDirector.Click

        Dim strId As String = txtDirectorId.Text.Trim()

        If strId = "" Then
            MessageBox.Show("Please Select ID Number!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub

        End If



        Dim frmDirectorInfoDet As New FrmDirectorDetail(strId)
        frmDirectorInfoDet.ShowDialog()

    End Sub

    Private Sub txtDirectorId_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDirectorId.Leave
        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim dt As New DataTable

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_GetListByCode")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@DIRECTOR_ID", DbType.String, txtDirectorId.Text.Trim())

            dt = db.ExecuteDataSet(commProc).Tables(0)

            If dt.Rows.Count > 0 Then
                lblDirectorName.Text = dt.Rows(0)("DIR_NAME").ToString()
            Else
                txtDirectorId.Clear()
                lblDirectorName.Text = ""
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class