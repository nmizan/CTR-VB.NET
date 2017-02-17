Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization
Imports System.Data.SqlClient

Public Class frmOwnerInfo

    Dim _formName As String = "MaintenanceOwnerInfoDetail"
    Dim opt As SecForm = New SecForm(_formName)

#Region "Global Variables"
    Dim _formMode As FormTransMode
    'Dim _intSlno As Integer = 0
    Dim _strOwner_Code As String = ""
    Dim _intModno As Integer = 0
    Dim log_message As String

    'For Update
    Dim _OwnerName As String = ""
    Dim _owFather As String = ""
    Dim _owMother As String = ""
    Dim _owSpouse As String = ""
    Dim _owDob As String = ""
    Dim _owAdd As String = ""
    Dim _owPPNO As String = ""
    Dim _owDLN As String = ""
    Dim _owTIN As String = ""
    Dim _owBIN As String = ""
    Dim _owMobile As String = ""
    Dim _owPerm As String = ""
    Dim _cmboPermThana As String = ""
    Dim _cmboPresThana As String = ""
    Dim _cmbOccp As String = ""
    Dim _PermThanaName As String = ""
    Dim _PresThanaName As String = ""
    Dim _OccpName As String = ""

    Dim _strAccNO As String = ""

    Dim OwnerList As New List(Of String)
    Dim _ownerLog As String = ""
    Dim _log As String = ""

    'For Authorize
    Dim _OwName As String = ""
    Dim _ownerFather As String = ""
    Dim _ownerMother As String = ""
    Dim _ownerSpouse As String = ""
    Dim _ownerDob As String = ""
    Dim _ownerAdd As String = ""
    Dim _ownerPPNO As String = ""
    Dim _ownerDLN As String = ""
    Dim _ownerTIN As String = ""
    Dim _ownerBIN As String = ""
    Dim _ownerMobile As String = ""
    Dim _ownerPerm As String = ""
    Dim _cmbPermeThana As String = ""
    Dim _cmbPreseThana As String = ""
    Dim _cmboOccp As String = ""
    Dim _PermeThana As String = ""
    Dim _PreseThana As String = ""
    Dim _Occp As String = ""
#End Region

#Region "User defined Codes"


    Private Sub EnableUnlock()
        btnUnlock.Enabled = True
    End Sub

    Private Sub DisableUnlock()
        btnUnlock.Enabled = False
    End Sub

    Private Sub EnableNew()
        btnNew.Enabled = True
    End Sub

    Private Sub DisableNew()
        btnNew.Enabled = False
    End Sub

    Private Sub EnableSave()
        btnSave.Enabled = True
    End Sub

    Private Sub DisableSave()
        btnSave.Enabled = False
    End Sub

    Private Sub EnableDelete()
        btnDelete.Enabled = True
    End Sub

    Private Sub DisableDelete()
        btnDelete.Enabled = False
    End Sub

    Private Sub EnableAuth()
        btnAuthorize.Enabled = True
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

        cmbBranch.Enabled = False
        'txtOwnerCode.ReadOnly = True
        cmbOccType.Enabled = False
        cmbSex.Enabled = False
        txtOwnerName.ReadOnly = True
        txtOwnerSpouse.ReadOnly = True
        txtDob.ReadOnly = True
        txtOwnerFather.ReadOnly = True
        txtOwnerMother.ReadOnly = True

        txtPresAdd.ReadOnly = True
        cmbPresThana.Enabled = False


        txtPermAdd.ReadOnly = True

        cmbPermThana.Enabled = False


        txtPhnRes1.ReadOnly = True
        txtPhnCityRes1.ReadOnly = True

        cmbCountryRes1.Enabled = False


        txtPhnRes2.ReadOnly = True
        txtPhnCityRes2.ReadOnly = True
        cmbCountryRes2.Enabled = False

        txtMob1.ReadOnly = True
        txtMobCity1.ReadOnly = True
        cmbCountryMob1.Enabled = False

        txtMob2.ReadOnly = True
        txtMobCity2.ReadOnly = True
        cmbCountryMob2.Enabled = False

        txtPhnOff1.ReadOnly = True
        txtPhnCityOff1.ReadOnly = True
        cmbCountryOff1.Enabled = False

        txtPhnOff2.ReadOnly = True
        txtPhnCityOff2.ReadOnly = True
        cmbCountryOff2.Enabled = False

        txtOwnerPPNO.ReadOnly = True
        txtOwnerDLN.ReadOnly = True
        txtOwnerTIN.ReadOnly = True

        txtOwnerBIN.ReadOnly = True
        txtBBOwnerCode.ReadOnly = True
        txtBBCodeUpdatedOn.ReadOnly = True
        txtBBCodeUpdatedBy.ReadOnly = True


        txtInsertedOn.ReadOnly = True
        txtModifiedOn.ReadOnly = True

    End Sub

    Private Sub EnableFields()
        'txtId.ReadOnly = False
        'txtName.ReadOnly = False

        If _intModno = 0 Then
            cmbBranch.Enabled = True
            'txtOwnerCode.ReadOnly = False
        End If




        cmbOccType.Enabled = True
        cmbSex.Enabled = True
        txtOwnerName.ReadOnly = False
        txtOwnerSpouse.ReadOnly = False
        txtDob.ReadOnly = False
        txtOwnerFather.ReadOnly = False
        txtOwnerMother.ReadOnly = False

        txtPresAdd.ReadOnly = False
        cmbPresThana.Enabled = True


        txtPermAdd.ReadOnly = False

        cmbPermThana.Enabled = True

        txtPhnRes1.ReadOnly = False
        txtPhnCityRes1.ReadOnly = False
        cmbCountryRes1.Enabled = True

        txtPhnRes2.ReadOnly = False
        txtPhnCityRes2.ReadOnly = False
        cmbCountryRes2.Enabled = True

        txtMob1.ReadOnly = False
        txtMobCity1.ReadOnly = False
        cmbCountryMob1.Enabled = True

        txtMob2.ReadOnly = False
        txtMobCity2.ReadOnly = False
        cmbCountryMob2.Enabled = True

        txtPhnOff1.ReadOnly = False
        txtPhnCityOff1.ReadOnly = False
        cmbCountryOff1.Enabled = True

        txtPhnOff2.ReadOnly = False
        txtPhnCityOff2.ReadOnly = False
        cmbCountryOff2.Enabled = True

        txtOwnerPPNO.ReadOnly = False
        txtOwnerDLN.ReadOnly = False
        txtOwnerTIN.ReadOnly = False

        txtOwnerBIN.ReadOnly = False
        txtBBOwnerCode.ReadOnly = False
        txtBBCodeUpdatedOn.ReadOnly = False
        txtBBCodeUpdatedBy.ReadOnly = False


        txtInsertedOn.ReadOnly = False
        txtModifiedOn.ReadOnly = False


    End Sub

    Private Sub ClearFields()
        'txtId.Clear()
        'txtName.Clear()

        txtOwnerCode.Clear()
        txtOwnerName.Clear()
        txtOwnerSpouse.Clear()
        txtDob.Clear()
        txtOwnerFather.Clear()
        txtOwnerMother.Clear()

        txtPresAdd.Clear()
        cmbPresThana.SelectedIndex = -1


        txtPermAdd.Clear()
        cmbPermThana.SelectedIndex = -1

        txtPhnRes1.Clear()
        txtPhnCityRes1.Clear()

        cmbCountryRes1.SelectedIndex = -1

        txtPhnRes2.Clear()
        txtPhnCityRes2.Clear()
        cmbCountryRes2.SelectedIndex = -1

        txtMob1.Clear()
        txtMobCity1.Clear()
        cmbCountryMob1.SelectedIndex = -1

        txtMob2.Clear()
        txtMobCity2.Clear()
        cmbCountryMob2.SelectedIndex = -1

        txtPhnOff1.Clear()
        txtPhnCityOff1.Clear()
        cmbCountryOff1.SelectedIndex = -1

        txtPhnOff2.Clear()
        txtPhnCityOff2.Clear()
        cmbCountryOff2.SelectedIndex = -1

        txtOwnerPPNO.Clear()
        txtOwnerDLN.Clear()
        txtOwnerTIN.Clear()

        txtOwnerBIN.Clear()
        txtBBOwnerCode.Clear()
        txtBBCodeUpdatedOn.Clear()
        txtBBCodeUpdatedBy.Clear()


        txtInsertedOn.Clear()
        txtModifiedOn.Clear()

    End Sub

    Private Sub ClearFieldsAll()

        txtOwnerCode.Clear()
        txtOwnerName.Clear()
        txtOwnerSpouse.Clear()
        txtDob.Clear()
        txtOwnerFather.Clear()
        txtOwnerMother.Clear()

        txtPresAdd.Clear()


        txtPermAdd.Clear()



        txtPhnRes1.Clear()
        txtPhnCityRes1.Clear()
        cmbCountryRes1.SelectedIndex = -1

        txtPhnRes2.Clear()
        txtPhnCityRes2.Clear()
        cmbCountryRes2.SelectedIndex = -1

        txtMob1.Clear()
        txtMobCity1.Clear()
        cmbCountryMob1.SelectedIndex = -1

        txtMob2.Clear()
        txtMobCity2.Clear()
        cmbCountryMob2.SelectedIndex = -1

        txtPhnOff1.Clear()
        txtPhnCityOff1.Clear()
        cmbCountryOff1.SelectedIndex = -1

        txtPhnOff2.Clear()
        txtPhnCityOff2.Clear()
        cmbCountryOff2.SelectedIndex = -1

        txtOwnerPPNO.Clear()
        txtOwnerDLN.Clear()
        txtOwnerTIN.Clear()

        txtOwnerBIN.Clear()
        txtBBOwnerCode.Clear()
        txtBBCodeUpdatedOn.Clear()
        txtBBCodeUpdatedBy.Clear()


        txtInsertedOn.Clear()
        txtModifiedOn.Clear()



        _strOwner_Code = ""
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

        If txtOwnerCode.Text.Trim() = "" Then
            MessageBox.Show("Owner Code required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbBranch.Focus()
            Return False
        ElseIf txtOwnerName.Text.Trim() = "" Then
            MessageBox.Show("Owner Names required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtOwnerName.Focus()
            Return False

        ElseIf txtDob.Text.Trim() = "" Then
            MessageBox.Show("Owner Birth Date required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtDob.Focus()
            Return False

        ElseIf txtOwnerSpouse.Text.Trim() = "" And txtOwnerFather.Text.Trim() = "" And txtOwnerMother.Text.Trim() = "" Then
            MessageBox.Show("Owner Spause/Father/Mother Name Missing, atleast one of Three is required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

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

            strSql = "Insert Into FIU_OWNER_INFO(OWNER_CODE, OWNER_NAME, OCTYPECODE, GENDER, OWNER_FATHER, OWNER_MOTHER, OWNER_SPOUSE, DOB, PHONE_RES1, PHONE_CITY_RES1, COUNTRY_CODE_RES1, PHONE_RES2, PHONE_CITY_RES2, COUNTRY_CODE_RES2, MOBILE1, MOBILE1_CITY, COUNTRY_CODE_MOB1, MOBILE2, MOBILE2_CITY, COUNTRY_CODE_MOB2, PHONE_OFF1, PHONE_CITY_OFF1, COUNTRY_CODE_OFF1, PHONE_OFF2, PHONE_CITY_OFF2, COUNTRY_CODE_OFF2, PPNO, DRIVINGLNO, TIN, BIN, PRES_ADDR, PRES_THANA_CODE, PERM_ADDR, PERM_THANA_CODE, BB_OWNER_CODE, BB_CODE_UPDATED_ON, BB_CODE_UPDATED_BY, INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS )" & _
                     "Values(@P_OWNER_CODE, @P_OWNER_NAME, @P_OCTYPECODE, @P_GENDER, @P_OWNER_FATHER, @P_OWNER_MOTHER, @P_OWNER_SPOUSE, @P_DOB, @P_PHONE_RES1, @P_PHONE_CITY_RES1, @P_COUNTRY_CODE_RES1, @P_PHONE_RES2, @P_PHONE_CITY_RES2, @P_COUNTRY_CODE_RES2, @P_MOBILE1, @P_MOBILE1_CITY, @P_COUNTRY_CODE_MOB1, @P_MOBILE2, @P_MOBILE2_CITY, @P_COUNTRY_CODE_MOB2, @P_PHONE_OFF1, @P_PHONE_CITY_OFF1, @P_COUNTRY_CODE_OFF1, @P_PHONE_OFF2, @P_PHONE_CITY_OFF2, @P_COUNTRY_CODE_OFF2, @P_PPNO, @P_DRIVINGLNO, @P_TIN, @P_BIN, @P_PRES_ADDR, @P_PRES_THANA_CODE, @P_PERM_ADDR, @P_PERM_THANA_CODE, @P_BB_OWNER_CODE, @P_BB_CODE_UPDATED_ON, @P_BB_CODE_UPDATED_BY,@P_Inserted_On,@P_Modified_On,1,@P_Input_By,getdate(),0,'U' )"

            Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

            commProc.Parameters.Clear()
            'db.AddInParameter(commProc, "@P_OWNER_BRANCH", DbType.String, cmbBranch.SelectedValue)

            db.AddInParameter(commProc, "@P_OWNER_CODE", DbType.String, txtOwnerCode.Text.Trim())
            db.AddInParameter(commProc, "@P_OWNER_NAME", DbType.String, txtOwnerName.Text.Trim())
            db.AddInParameter(commProc, "@P_OCTYPECODE", DbType.String, cmbOccType.SelectedValue)
            db.AddInParameter(commProc, "@P_GENDER", DbType.String, cmbSex.Text.First)
            db.AddInParameter(commProc, "@P_OWNER_FATHER", DbType.String, txtOwnerFather.Text.Trim())
            db.AddInParameter(commProc, "@P_OWNER_MOTHER", DbType.String, txtOwnerMother.Text.Trim())
            db.AddInParameter(commProc, "@P_OWNER_SPOUSE", DbType.String, txtOwnerSpouse.Text.Trim())


            If txtDob.Text.Trim <> "" Then
                db.AddInParameter(commProc, "@P_DOB", DbType.DateTime, DateTime.ParseExact(txtDob.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
            Else
                db.AddInParameter(commProc, "@P_DOB", DbType.DateTime, DBNull.Value)
            End If

            db.AddInParameter(commProc, "@P_PHONE_RES1", DbType.String, txtPhnRes1.Text.Trim())
            db.AddInParameter(commProc, "@P_PHONE_CITY_RES1", DbType.String, txtPhnCityRes1.Text.Trim())
            db.AddInParameter(commProc, "@P_COUNTRY_CODE_RES1", DbType.String, cmbCountryRes1.SelectedValue)
            db.AddInParameter(commProc, "@P_PHONE_RES2", DbType.String, txtPhnRes2.Text.Trim())
            db.AddInParameter(commProc, "@P_PHONE_CITY_RES2", DbType.String, txtPhnCityRes2.Text.Trim())
            db.AddInParameter(commProc, "@P_COUNTRY_CODE_RES2", DbType.String, cmbCountryRes2.SelectedValue)
            db.AddInParameter(commProc, "@P_MOBILE1", DbType.String, txtMob1.Text.Trim())
            db.AddInParameter(commProc, "@P_MOBILE1_CITY", DbType.String, txtMobCity1.Text.Trim())
            db.AddInParameter(commProc, "@P_COUNTRY_CODE_MOB1", DbType.String, cmbCountryMob1.SelectedValue)
            db.AddInParameter(commProc, "@P_MOBILE2", DbType.String, txtMob2.Text.Trim())
            db.AddInParameter(commProc, "@P_MOBILE2_CITY", DbType.String, txtMobCity2.Text.Trim())
            db.AddInParameter(commProc, "@P_COUNTRY_CODE_MOB2", DbType.String, cmbCountryMob2.SelectedValue)
            db.AddInParameter(commProc, "@P_PHONE_OFF1", DbType.String, txtPhnOff1.Text.Trim())
            db.AddInParameter(commProc, "@P_PHONE_CITY_OFF1", DbType.String, txtPhnCityOff1.Text.Trim())
            db.AddInParameter(commProc, "@P_COUNTRY_CODE_OFF1", DbType.String, cmbCountryOff1.SelectedValue)
            db.AddInParameter(commProc, "@P_PHONE_OFF2", DbType.String, txtPhnOff2.Text.Trim())
            db.AddInParameter(commProc, "@P_PHONE_CITY_OFF2", DbType.String, txtPhnCityOff2.Text.Trim())
            db.AddInParameter(commProc, "@P_COUNTRY_CODE_OFF2", DbType.String, cmbCountryOff2.SelectedValue)
            db.AddInParameter(commProc, "@P_PPNO", DbType.String, txtOwnerPPNO.Text.Trim())
            db.AddInParameter(commProc, "@P_DRIVINGLNO", DbType.String, txtOwnerDLN.Text.Trim())
            db.AddInParameter(commProc, "@P_TIN", DbType.String, txtOwnerTIN.Text.Trim())
            db.AddInParameter(commProc, "@P_BIN", DbType.String, txtOwnerBIN.Text.Trim())
            db.AddInParameter(commProc, "@P_PRES_ADDR", DbType.String, txtPresAdd.Text.Trim())
            db.AddInParameter(commProc, "@P_PRES_THANA_CODE", DbType.String, cmbPresThana.SelectedValue)
            db.AddInParameter(commProc, "@P_PERM_ADDR", DbType.String, txtPermAdd.Text.Trim())
            db.AddInParameter(commProc, "@P_PERM_THANA_CODE", DbType.String, cmbPermThana.SelectedValue)
            db.AddInParameter(commProc, "@P_BB_OWNER_CODE", DbType.String, txtBBOwnerCode.Text.Trim())

            If txtBBCodeUpdatedOn.Text.Trim <> "" Then
                db.AddInParameter(commProc, "@P_BB_CODE_UPDATED_ON", DbType.DateTime, DateTime.ParseExact(txtBBCodeUpdatedOn.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
            Else
                db.AddInParameter(commProc, "@P_BB_CODE_UPDATED_ON", DbType.DateTime, DBNull.Value)
            End If



            db.AddInParameter(commProc, "@P_BB_CODE_UPDATED_BY", DbType.String, txtBBCodeUpdatedBy.Text.Trim())


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

            If result < 0 Then  '  Don't Understand
                tStatus = TransState.Exist
            Else
                tStatus = TransState.Add
                '_intSlno = intSlno
                _strOwner_Code = txtOwnerCode.Text.Trim()

                _intModno = 1

                log_message = " Added : Owner Code : " + txtOwnerCode.Text.Trim() + "." + " " + " Owner Name : " + txtOwnerName.Text.Trim()
                Logger.system_log(log_message)

            End If


            


        ElseIf _formMode = FormTransMode.Update Then


            Using conn As DbConnection = db.CreateConnection()


                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                db.ExecuteNonQuery(trans, CommandType.Text, "delete FIU_OWNER_INFO where OWNER_CODE='" & _strOwner_Code & "' and IS_AUTHORIZED=0")
                Dim ds As New DataSet


                strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_OWNER_INFO where OWNER_CODE='" & _strOwner_Code & "'"


                intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)

                strSql = "Insert Into FIU_OWNER_INFO(OWNER_CODE, OWNER_NAME, OCTYPECODE, GENDER, OWNER_FATHER, OWNER_MOTHER, OWNER_SPOUSE, DOB, PHONE_RES1, PHONE_CITY_RES1, COUNTRY_CODE_RES1, PHONE_RES2, PHONE_CITY_RES2, COUNTRY_CODE_RES2, MOBILE1, MOBILE1_CITY, COUNTRY_CODE_MOB1, MOBILE2, MOBILE2_CITY, COUNTRY_CODE_MOB2, PHONE_OFF1, PHONE_CITY_OFF1, COUNTRY_CODE_OFF1, PHONE_OFF2, PHONE_CITY_OFF2, COUNTRY_CODE_OFF2, PPNO, DRIVINGLNO, TIN, BIN, PRES_ADDR, PRES_THANA_CODE, PERM_ADDR, PERM_THANA_CODE, BB_OWNER_CODE, BB_CODE_UPDATED_ON, BB_CODE_UPDATED_BY, INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS)" & _
                     "Values(@P_OWNER_CODE, @P_OWNER_NAME, @P_OCTYPECODE, @P_GENDER, @P_OWNER_FATHER, @P_OWNER_MOTHER, @P_OWNER_SPOUSE, @P_DOB, @P_PHONE_RES1, @P_PHONE_CITY_RES1, @P_COUNTRY_CODE_RES1, @P_PHONE_RES2, @P_PHONE_CITY_RES2, @P_COUNTRY_CODE_RES2, @P_MOBILE1, @P_MOBILE1_CITY, @P_COUNTRY_CODE_MOB1, @P_MOBILE2, @P_MOBILE2_CITY, @P_COUNTRY_CODE_MOB2, @P_PHONE_OFF1, @P_PHONE_CITY_OFF1, @P_COUNTRY_CODE_OFF1, @P_PHONE_OFF2, @P_PHONE_CITY_OFF2, @P_COUNTRY_CODE_OFF2, @P_PPNO, @P_DRIVINGLNO, @P_TIN, @P_BIN, @P_PRES_ADDR, @P_PRES_THANA_CODE, @P_PERM_ADDR, @P_PERM_THANA_CODE, @P_BB_OWNER_CODE, @P_BB_CODE_UPDATED_ON, @P_BB_CODE_UPDATED_BY,@P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'U')"


                Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                'db.AddInParameter(commProc, "@P_OWNER_BRANCH", DbType.String, cmbBranch.SelectedValue)

                db.AddInParameter(commProc, "@P_OWNER_CODE", DbType.String, txtOwnerCode.Text.Trim())
                db.AddInParameter(commProc, "@P_OWNER_NAME", DbType.String, txtOwnerName.Text.Trim())
                db.AddInParameter(commProc, "@P_OCTYPECODE", DbType.String, cmbOccType.SelectedValue)
                db.AddInParameter(commProc, "@P_GENDER", DbType.String, cmbSex.Text.First)
                db.AddInParameter(commProc, "@P_OWNER_FATHER", DbType.String, txtOwnerFather.Text.Trim())
                db.AddInParameter(commProc, "@P_OWNER_MOTHER", DbType.String, txtOwnerMother.Text.Trim())
                db.AddInParameter(commProc, "@P_OWNER_SPOUSE", DbType.String, txtOwnerSpouse.Text.Trim())


                If txtDob.Text.Trim <> "" Then
                    db.AddInParameter(commProc, "@P_DOB", DbType.DateTime, DateTime.ParseExact(txtDob.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
                Else
                    db.AddInParameter(commProc, "@P_DOB", DbType.DateTime, DBNull.Value)
                End If

                db.AddInParameter(commProc, "@P_PHONE_RES1", DbType.String, txtPhnRes1.Text.Trim())
                db.AddInParameter(commProc, "@P_PHONE_CITY_RES1", DbType.String, txtPhnCityRes1.Text.Trim())
                db.AddInParameter(commProc, "@P_COUNTRY_CODE_RES1", DbType.String, cmbCountryRes1.SelectedValue)
                db.AddInParameter(commProc, "@P_PHONE_RES2", DbType.String, txtPhnRes2.Text.Trim())
                db.AddInParameter(commProc, "@P_PHONE_CITY_RES2", DbType.String, txtPhnCityRes2.Text.Trim())
                db.AddInParameter(commProc, "@P_COUNTRY_CODE_RES2", DbType.String, cmbCountryRes2.SelectedValue)
                db.AddInParameter(commProc, "@P_MOBILE1", DbType.String, txtMob1.Text.Trim())
                db.AddInParameter(commProc, "@P_MOBILE1_CITY", DbType.String, txtMobCity1.Text.Trim())
                db.AddInParameter(commProc, "@P_COUNTRY_CODE_MOB1", DbType.String, cmbCountryMob1.SelectedValue)
                db.AddInParameter(commProc, "@P_MOBILE2", DbType.String, txtMob2.Text.Trim())
                db.AddInParameter(commProc, "@P_MOBILE2_CITY", DbType.String, txtMobCity2.Text.Trim())
                db.AddInParameter(commProc, "@P_COUNTRY_CODE_MOB2", DbType.String, cmbCountryMob2.SelectedValue)
                db.AddInParameter(commProc, "@P_PHONE_OFF1", DbType.String, txtPhnOff1.Text.Trim())
                db.AddInParameter(commProc, "@P_PHONE_CITY_OFF1", DbType.String, txtPhnCityOff1.Text.Trim())
                db.AddInParameter(commProc, "@P_COUNTRY_CODE_OFF1", DbType.String, cmbCountryOff1.SelectedValue)
                db.AddInParameter(commProc, "@P_PHONE_OFF2", DbType.String, txtPhnOff2.Text.Trim())
                db.AddInParameter(commProc, "@P_PHONE_CITY_OFF2", DbType.String, txtPhnCityOff2.Text.Trim())
                db.AddInParameter(commProc, "@P_COUNTRY_CODE_OFF2", DbType.String, cmbCountryOff2.SelectedValue)
                db.AddInParameter(commProc, "@P_PPNO", DbType.String, txtOwnerPPNO.Text.Trim())
                db.AddInParameter(commProc, "@P_DRIVINGLNO", DbType.String, txtOwnerDLN.Text.Trim())
                db.AddInParameter(commProc, "@P_TIN", DbType.String, txtOwnerTIN.Text.Trim())
                db.AddInParameter(commProc, "@P_BIN", DbType.String, txtOwnerBIN.Text.Trim())
                db.AddInParameter(commProc, "@P_PRES_ADDR", DbType.String, txtPresAdd.Text.Trim())
                db.AddInParameter(commProc, "@P_PRES_THANA_CODE", DbType.String, cmbPresThana.SelectedValue)
                db.AddInParameter(commProc, "@P_PERM_ADDR", DbType.String, txtPermAdd.Text.Trim())
                db.AddInParameter(commProc, "@P_PERM_THANA_CODE", DbType.String, cmbPermThana.SelectedValue)
                db.AddInParameter(commProc, "@P_BB_OWNER_CODE", DbType.String, txtBBOwnerCode.Text.Trim())

                If txtBBCodeUpdatedOn.Text.Trim <> "" Then
                    db.AddInParameter(commProc, "@P_BB_CODE_UPDATED_ON", DbType.DateTime, DateTime.ParseExact(txtBBCodeUpdatedOn.Text.Trim(), "MM/dd/yyyy", New CultureInfo("en-us")))
                Else
                    db.AddInParameter(commProc, "@P_BB_CODE_UPDATED_ON", DbType.DateTime, DBNull.Value)
                End If



                db.AddInParameter(commProc, "@P_BB_CODE_UPDATED_BY", DbType.String, txtBBCodeUpdatedBy.Text.Trim())


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

                    '--------------Mizan Work (19-04-16)---------------------

                    If _OwnerName <> txtOwnerName.Text.Trim() Then
                        log_message = " Owner Name : " + _OwnerName + " " + " To " + " " + txtOwnerName.Text.Trim() + "." + " "
                        OwnerList.Add(log_message)
                    End If

                    If _owSpouse <> txtOwnerSpouse.Text.Trim() Then
                        If _owSpouse = "" Then
                            log_message = " Owner Spouse Name : " + txtOwnerSpouse.Text.Trim() + "." + " "
                        Else
                            log_message = " Owner Spouse Name : " + _owSpouse + " " + " To " + " " + txtOwnerSpouse.Text.Trim() + "." + " "
                        End If
                        OwnerList.Add(log_message)
                    End If

                    If _owFather <> txtOwnerFather.Text.Trim() Then
                        If _owFather = "" Then
                            log_message = " Owner Father Name : " + txtOwnerFather.Text.Trim() + "." + " "

                        Else
                            log_message = " Owner Father Name : " + _owFather + " " + " To " + " " + txtOwnerFather.Text.Trim() + "." + " "

                        End If
                        OwnerList.Add(log_message)
                    End If

                    If _owMother <> txtOwnerMother.Text.Trim() Then
                        If _owMother = "" Then
                            log_message = " Owner Mother Name : " + txtOwnerMother.Text.Trim() + "." + " "

                        Else
                            log_message = " Owner Mother Name : " + _owMother + " " + " To " + " " + txtOwnerMother.Text.Trim() + "." + " "

                        End If
                        OwnerList.Add(log_message)
                    End If

                    If _owDob <> txtDob.Text.Trim() Then
                        If _owDob = "" Then
                            log_message = " Date Of Birth : " + txtDob.Text.Trim() + "." + " "
                        Else
                            log_message = " Date Of Birth : " + _owDob + " " + " To " + " " + txtDob.Text.Trim() + "." + " "
                        End If
                        OwnerList.Add(log_message)
                    End If

                    If _owAdd <> txtPresAdd.Text.Trim() Then
                        If _owAdd = "" Then
                            log_message = " Present Address : " + txtPresAdd.Text.Trim() + "." + " "

                        Else
                            log_message = " Present Address : " + _owAdd + " " + " To " + " " + txtPresAdd.Text.Trim() + "." + " "

                        End If
                        OwnerList.Add(log_message)
                    End If

                    If _cmboPresThana <> cmbPresThana.Text Then
                        If _cmboPresThana = "" Then
                            log_message = " Present Thana : " + cmbPresThana.Text + "." + " "
                        Else
                            log_message = " Present Thana : " + _cmboPresThana + " " + " To " + " " + cmbPresThana.Text + "." + " "
                        End If
                        OwnerList.Add(log_message)
                    End If

                    If _owPerm <> txtPermAdd.Text.Trim() Then
                        If _owPerm = "" Then
                            log_message = " Permanent Address : " + txtPermAdd.Text.Trim() + "." + " "

                        Else
                            log_message = " Permanent Address : " + _owPerm + " " + " To " + " " + txtPermAdd.Text.Trim() + "." + " "

                            OwnerList.Add(log_message)
                        End If
                    End If


                    If _cmboPermThana <> cmbPermThana.Text Then
                        If _cmboPermThana = "" Then
                            log_message = " Permanent Thana : " + cmbPermThana.Text + "." + " "
                        Else
                            log_message = " Permanent Thana : " + _cmboPermThana + " " + " To " + " " + cmbPermThana.Text + "." + " "
                        End If

                        OwnerList.Add(log_message)

                    End If

                    If _owMobile <> txtMob1.Text.Trim() Then
                        If _owMobile = "" Then
                            log_message = " Mobile : " + txtMob1.Text.Trim() + "." + " "

                        Else
                            log_message = " Mobile : " + _owMobile + " " + " To " + " " + txtMob1.Text.Trim() + "." + " "

                        End If
                        OwnerList.Add(log_message)
                    End If
                    If _owPPNO <> txtOwnerPPNO.Text.Trim() Then
                        If _owPPNO = "" Then
                            log_message = " Passport Number : " + txtOwnerPPNO.Text.Trim() + "." + " "

                        Else
                            log_message = " Passport Number : " + _owPPNO + " " + " To " + " " + txtOwnerPPNO.Text.Trim() + "." + " "

                        End If
                        OwnerList.Add(log_message)
                    End If
                    If _owDLN <> txtOwnerDLN.Text.Trim() Then
                        If _owDLN = "" Then
                            log_message = " Driving License : " + txtOwnerDLN.Text.Trim() + "." + " "

                        Else
                            log_message = " Driving License : " + _owDLN + " " + " To " + " " + txtOwnerDLN.Text.Trim() + "." + " "

                        End If
                        OwnerList.Add(log_message)
                    End If
                    If _owBIN <> txtOwnerBIN.Text.Trim() Then
                        If _owBIN = "" Then
                            log_message = " BIN : " + txtOwnerBIN.Text.Trim() + "." + " "

                        Else
                            log_message = " BIN : " + _owBIN + " " + " To " + " " + txtOwnerBIN.Text.Trim() + "." + " "

                        End If
                        OwnerList.Add(log_message)
                    End If
                    If _owTIN <> txtOwnerTIN.Text.Trim() Then
                        If _owTIN = "" Then
                            log_message = " TIN : " + txtOwnerTIN.Text.Trim() + "." + " "

                        Else
                            log_message = " TIN : " + _owTIN + " " + " To " + " " + txtOwnerTIN.Text.Trim() + "." + " "

                        End If
                        OwnerList.Add(log_message)
                    End If
                    If _cmbOccp <> cmbOccType.Text Then
                        If _cmbOccp = "" Then
                            log_message = " Occupation Type : " + cmbOccType.Text + "." + " "
                        Else
                            log_message = " Occupation Type : " + _cmbOccp + " " + " To " + " " + cmbOccType.Text + "." + " "
                        End If

                        OwnerList.Add(log_message)
                    End If

                    For Each ownerloglist As String In OwnerList
                        _ownerLog += ownerloglist
                    Next

                    _log = " Updated : Owner Code : " + txtOwnerCode.Text.ToString() + "." + " " + _ownerLog

                    Logger.system_log(_log)
                    _ownerLog = ""
                    OwnerList.Clear()

                    '--------------Mizan Work (19-04-16)---------------------

                End If

                'intModno = db.ExecuteDataSet(trans, CommandType.Text, "select max(ISNULL(MODNO,0))+1 maxmodno from DEPARTMENT where SLNO=" & _intSlno.ToString()).Tables(0).Rows(0)(0)


                trans.Commit()

               

                'log_message = " Updated : Owner Code : " + txtOwnerCode.Text.Trim() + "." + " " + " Owner Name : " + txtOwnerName.Text.Trim()
                'Logger.system_log(log_message)

            End Using

        End If

        Return tStatus

    End Function

    '--------------Mizan Work (19-04-16)---------------------

    Private Function AuthorizeData() As TransState

        If _intModno > 1 Then

            LoadOwnerDataForAuth(_strOwner_Code)

            Dim tStatus As TransState

            Dim strSql As String

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                strSql = "select IS_AUTHORIZED,STATUS from FIU_OWNER_INFO where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where  OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()


                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If _intModno > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_OWNER_INFO set STATUS = 'C' " & _
                                " where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_OWNER_INFO set STATUS = 'O' " & _
                                " where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

                        '--------------Mizan Work (19-04-16)---------------------

                        If _OwName <> _OwnerName Then
                            If _OwName = "" Then
                                log_message = " Owner Name : " + _OwnerName + "." + " "
                            Else
                                log_message = " Owner Name : " + _OwName + " " + " To " + " " + _OwnerName + "." + " "
                            End If

                            OwnerList.Add(log_message)
                        End If

                        If _ownerSpouse <> _owSpouse Then
                            If _ownerSpouse = "" Then
                                log_message = " Owner Spouse Name : " + _owSpouse + "." + " "

                            Else
                                log_message = " Owner Spouse Name : " + _ownerSpouse + " " + " To " + " " + _owSpouse + "." + " "

                            End If
                            OwnerList.Add(log_message)
                        End If

                        If _ownerFather <> _owFather Then
                            If _ownerFather = "" Then
                                log_message = " Owner Father Name : " + _owFather + "." + " "

                            Else
                                log_message = " Owner Father Name : " + _ownerFather + " " + " To " + " " + _owFather + "." + " "

                            End If
                            OwnerList.Add(log_message)
                        End If

                        If _ownerMother <> _owMother Then
                            If _ownerMother = "" Then
                                log_message = " Owner Mother Name : " + _owMother + "." + " "

                            Else
                                log_message = " Owner Mother Name : " + _ownerMother + " " + " To " + " " + _owMother + "." + " "

                            End If
                            OwnerList.Add(log_message)
                        End If

                        If _ownerDob <> _owDob Then
                            If _ownerDob = "" Then
                                log_message = " Date Of Birth : " + _owDob + "." + " "
                            Else
                                log_message = " Date Of Birth : " + _ownerDob + " " + " To " + " " + _owDob + "." + " "
                            End If
                            OwnerList.Add(log_message)
                        End If

                        If _ownerAdd <> _owAdd Then
                            If _ownerAdd = "" Then
                                log_message = " Present Address : " + _owAdd + "." + " "

                            Else
                                log_message = " Present Address : " + _ownerAdd + " " + " To " + " " + _owAdd + "." + " "

                            End If
                            OwnerList.Add(log_message)
                        End If

                        If _cmbPreseThana <> _cmboPresThana Then
                            If _cmbPreseThana = "" Then
                                log_message = " Present Thana : " + _cmboPresThana + "." + " "
                            Else
                                log_message = " Present Thana : " + _cmbPreseThana + " " + " To " + " " + _cmboPresThana + "." + " "
                            End If


                            OwnerList.Add(log_message)
                        End If

                        If _ownerPerm <> _owPerm Then
                            If _ownerPerm = "" Then
                                log_message = " Permanent Address : " + _owPerm + "." + " "

                            Else
                                log_message = " Permanent Address : " + _ownerPerm + " " + " To " + " " + _owPerm + "." + " "

                                OwnerList.Add(log_message)
                            End If
                        End If


                        If _cmbPermeThana <> _cmboPermThana Then
                            If _cmbPermeThana = "" Then
                                log_message = " Permanent Thana : " + _cmboPermThana + "." + " "
                            Else
                                log_message = " Permanent Thana : " + _cmbPermeThana + " " + " To " + " " + _cmboPermThana + "." + " "
                            End If


                            OwnerList.Add(log_message)

                        End If

                        If _ownerMobile <> _owMobile Then
                            If _ownerMobile = "" Then
                                log_message = " Mobile : " + _owMobile + "." + " "

                            Else
                                log_message = " Mobile : " + _ownerMobile + " " + " To " + " " + _owMobile + "." + " "

                            End If
                            OwnerList.Add(log_message)
                        End If
                        If _ownerPPNO <> _owPPNO Then
                            If _ownerPPNO = "" Then
                                log_message = " Passport Number : " + _owPPNO + "." + " "

                            Else
                                log_message = " Passport Number : " + _ownerPPNO + " " + " To " + " " + _owPPNO + "." + " "

                            End If
                            OwnerList.Add(log_message)
                        End If
                        If _ownerDLN <> _owDLN Then
                            If _ownerDLN = "" Then
                                log_message = " Driving License : " + _owDLN + "." + " "

                            Else
                                log_message = " Driving License : " + _ownerDLN + " " + " To " + " " + _owDLN + "." + " "

                            End If
                            OwnerList.Add(log_message)
                        End If
                        If _ownerBIN <> _owBIN Then
                            If _ownerBIN = "" Then
                                log_message = " BIN : " + _owBIN + "." + " "

                            Else
                                log_message = " BIN : " + _ownerBIN + " " + " To " + " " + _owBIN + "." + " "

                            End If
                            OwnerList.Add(log_message)
                        End If
                        If _ownerTIN <> _owTIN Then
                            If _ownerTIN = "" Then
                                log_message = " TIN : " + _owTIN + "." + " "

                            Else
                                log_message = " TIN : " + _ownerTIN + " " + " To " + " " + _owTIN + "." + " "

                            End If
                            OwnerList.Add(log_message)
                        End If
                        If _cmboOccp <> _cmbOccp Then
                            If _cmboOccp = "" Then
                                log_message = " Occupation Type : " + _cmbOccp + "." + " "
                            Else
                                log_message = " Occupation Type : " + _cmboOccp + " " + " To " + " " + _cmbOccp + "." + " "
                            End If

                            OwnerList.Add(log_message)
                        End If

                        For Each ownerloglist As String In OwnerList
                            _ownerLog += ownerloglist
                        Next

                        _log = " Authorized : Owner Code : " + txtOwnerCode.Text.ToString() + "." + " " + _ownerLog

                        Logger.system_log(_log)

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

                strSql = "select IS_AUTHORIZED,STATUS from FIU_OWNER_INFO where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where  OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()


                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If _intModno > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_OWNER_INFO set STATUS = 'C' " & _
                                " where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_OWNER_INFO set STATUS = 'O' " & _
                                " where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

                        '--------------Mizan Work (19-04-16)---------------------

                        If _OwName <> _OwnerName Then
                            If _OwName = "" Then
                                log_message = " Owner Name : " + _OwnerName + "." + " "
                            Else
                                log_message = " Owner Name : " + _OwName + " " + " To " + " " + _OwnerName + "." + " "
                            End If

                            OwnerList.Add(log_message)
                        End If

                        If _ownerSpouse <> _owSpouse Then
                            If _ownerSpouse = "" Then
                                ' log_message = " Owner Spouse Name : " + _owSpouse + "." + " "

                            Else
                                log_message = " Owner Spouse Name : " + _ownerSpouse + " " + " To " + " " + _owSpouse + "." + " "
                                OwnerList.Add(log_message)
                            End If

                        End If

                        If _ownerFather <> _owFather Then
                            If _ownerFather = "" Then
                                log_message = " Owner Father Name : " + _owFather + "." + " "

                            Else
                                log_message = " Owner Father Name : " + _ownerFather + " " + " To " + " " + _owFather + "." + " "

                            End If
                            OwnerList.Add(log_message)
                        End If

                        If _ownerMother <> _owMother Then
                            If _ownerMother = "" Then
                                log_message = " Owner Mother Name : " + _owMother + "." + " "

                            Else
                                log_message = " Owner Mother Name : " + _ownerMother + " " + " To " + " " + _owMother + "." + " "

                            End If
                            OwnerList.Add(log_message)
                        End If

                        If _ownerDob <> _owDob Then
                            If _ownerDob = "" Then
                                log_message = " Date Of Birth : " + _owDob + "." + " "
                            Else
                                log_message = " Date Of Birth : " + _ownerDob + " " + " To " + " " + _owDob + "." + " "
                            End If
                            OwnerList.Add(log_message)
                        End If

                        If _ownerAdd <> _owAdd Then
                            If _ownerAdd = "" Then
                                ' log_message = " Present Address : " + _owAdd + "." + " "

                            Else
                                log_message = " Present Address : " + _ownerAdd + " " + " To " + " " + _owAdd + "." + " "
                                OwnerList.Add(log_message)
                            End If

                        End If

                        If _cmbPreseThana <> _cmboPresThana Then
                            If _cmbPreseThana = "" Then
                            Else
                                log_message = " Present Thana : " + _cmbPreseThana + " " + " To " + " " + _cmboPresThana + "." + " "
                                OwnerList.Add(log_message)
                            End If


                        End If

                        If _ownerPerm <> _owPerm Then
                            If _ownerPerm = "" Then
                                'log_message = " Permanent Address : " + _owPerm + "." + " "

                            Else
                                log_message = " Permanent Address : " + _ownerPerm + " " + " To " + " " + _owPerm + "." + " "
                                OwnerList.Add(log_message)
                            End If
                        End If


                        If _cmbPermeThana <> _cmboPermThana Then
                            If _cmbPermeThana = "" Then
                            Else
                                log_message = " Permanent Thana : " + _cmbPermeThana + " " + " To " + " " + _cmboPermThana + "." + " "
                                OwnerList.Add(log_message)
                            End If

                        End If

                        If _ownerMobile <> _owMobile Then
                            If _ownerMobile = "" Then
                                'log_message = " Mobile : " + _owMobile + "." + " "

                            Else
                                log_message = " Mobile : " + _ownerMobile + " " + " To " + " " + _owMobile + "." + " "
                                OwnerList.Add(log_message)
                            End If

                        End If
                        If _ownerPPNO <> _owPPNO Then
                            If _ownerPPNO = "" Then
                                'log_message = " Passport Number : " + _owPPNO + "." + " "

                            Else
                                log_message = " Passport Number : " + _ownerPPNO + " " + " To " + " " + _owPPNO + "." + " "
                                OwnerList.Add(log_message)
                            End If

                        End If
                        If _ownerDLN <> _owDLN Then
                            If _ownerDLN = "" Then
                                ' log_message = " Driving License : " + _owDLN + "." + " "

                            Else
                                log_message = " Driving License : " + _ownerDLN + " " + " To " + " " + _owDLN + "." + " "
                                OwnerList.Add(log_message)
                            End If

                        End If
                        If _ownerBIN <> _owBIN Then
                            If _ownerBIN = "" Then
                                'log_message = " BIN : " + _owBIN + "." + " "

                            Else
                                log_message = " BIN : " + _ownerBIN + " " + " To " + " " + _owBIN + "." + " "
                                OwnerList.Add(log_message)
                            End If

                        End If
                        If _ownerTIN <> _owTIN Then
                            If _ownerTIN = "" Then
                                ' log_message = " TIN : " + _owTIN + "." + " "

                            Else
                                log_message = " TIN : " + _ownerTIN + " " + " To " + " " + _owTIN + "." + " "
                                OwnerList.Add(log_message)
                            End If

                        End If
                        If _cmboOccp <> _cmbOccp Then
                            If _cmboOccp = "" Then
                                log_message = " Occupation Type : " + _cmbOccp + "." + " "
                            Else
                                log_message = " Occupation Type : " + _cmboOccp + " " + " To " + " " + _cmbOccp + "." + " "
                            End If

                            OwnerList.Add(log_message)
                        End If

                        For Each ownerloglist As String In OwnerList
                            _ownerLog += ownerloglist
                        Next

                        _log = " Authorized : Owner Code : " + txtOwnerCode.Text.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _ownerLog

                        Logger.system_log(_log)

                        '--------------Mizan Work (19-04-16)---------------------

                    End If
                Else
                    tStatus = TransState.UpdateNotPossible
                End If



                trans.Commit()

               

                '-----------------Commented By Mizan (19-04-2016) -----------

                'Dim tStatus As TransState

                'Dim strSql As String

                'tStatus = TransState.UnspecifiedError

                'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                'Using conn As DbConnection = db.CreateConnection()

                '    conn.Open()

                '    Dim trans As DbTransaction = conn.BeginTransaction()

                '    strSql = "select IS_AUTHORIZED,STATUS from FIU_OWNER_INFO where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()

                '    Dim ds As New DataSet

                '    ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                '    If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                '        If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                '            strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                '            "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                '            " where  OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()


                '        ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                '            strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                '            "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                '            " where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()

                '        End If

                '        Dim result As Integer
                '        result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                '        If result <= 0 Then

                '            tStatus = TransState.NoRecord

                '        ElseIf result > 0 Then

                '            If _intModno > 1 Then

                '                'if previous modification status is D(Deleted) then make it C(Closed)
                '                strSql = "update FIU_OWNER_INFO set STATUS = 'C' " & _
                '                    " where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                '                    " and STATUS ='D'"

                '                db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                '                'if previous modification status is L(Deleted) then make it O(Open)
                '                strSql = "update FIU_OWNER_INFO set STATUS = 'O' " & _
                '                    " where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                '                    " and STATUS ='L'"

                '                db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                '            End If
                '            tStatus = TransState.Update
                '        End If
                '    Else
                '        tStatus = TransState.UpdateNotPossible
                '    End If



                '    trans.Commit()

                'log_message = "Authorized Owner Code " + txtOwnerCode.Text.Trim() + " Owner Name " + txtOwnerName.Text.Trim()
                'Logger.system_log(log_message)

            End Using

            Return tStatus

        End If

    End Function

    '--------------Mizan Work (19-04-16)---------------------

    Private Sub LoadOwnerDataForAuth(ByVal strOwnerCode As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet


            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_OWNER_INFO Where OWNER_CODE='" & strOwnerCode & "' and STATUS ='L' ")

            If ds.Tables(0).Rows.Count > 0 Then


                _strOwner_Code = strOwnerCode

                _formMode = FormTransMode.Update


                'cmbBranch.SelectedValue = ds.Tables(0).Rows(0)("OWNER_BRANCH")

                txtOwnerCode.Text = ds.Tables(0).Rows(0)("OWNER_CODE").ToString

                ' cmbOccType.SelectedValue = ds.Tables(0).Rows(0)("OCTYPECODE")
                _cmboOccp = ds.Tables(0).Rows(0)("OCTYPECODE").ToString()

                txtOwnerName.Text = ds.Tables(0).Rows(0)("OWNER_NAME").ToString
                _OwName = ds.Tables(0).Rows(0)("OWNER_NAME").ToString
                txtOwnerSpouse.Text = ds.Tables(0).Rows(0)("OWNER_SPOUSE").ToString
                _ownerSpouse = ds.Tables(0).Rows(0)("OWNER_SPOUSE").ToString()
                txtDob.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("DOB"))
                _ownerDob = NullHelper.DateToString(ds.Tables(0).Rows(0)("DOB"))
                txtOwnerFather.Text = ds.Tables(0).Rows(0)("OWNER_FATHER").ToString
                _ownerFather = ds.Tables(0).Rows(0)("OWNER_FATHER").ToString
                txtOwnerMother.Text = ds.Tables(0).Rows(0)("OWNER_MOTHER").ToString
                _ownerMother = ds.Tables(0).Rows(0)("OWNER_MOTHER").ToString


                txtPresAdd.Text = ds.Tables(0).Rows(0)("PRES_ADDR").ToString
                _ownerAdd = ds.Tables(0).Rows(0)("PRES_ADDR").ToString
                'cmbPresThana.SelectedValue = ds.Tables(0).Rows(0)("PRES_THANA_CODE")
                _cmbPreseThana = ds.Tables(0).Rows(0)("PRES_THANA_CODE").ToString()


                txtPermAdd.Text = ds.Tables(0).Rows(0)("PERM_ADDR").ToString
                _ownerPerm = ds.Tables(0).Rows(0)("PERM_ADDR").ToString
                'cmbPermThana.SelectedValue = ds.Tables(0).Rows(0)("PERM_THANA_CODE")
                _cmbPermeThana = ds.Tables(0).Rows(0)("PERM_THANA_CODE").ToString()

                txtMob1.Text = ds.Tables(0).Rows(0)("MOBILE1").ToString
                _ownerMobile = ds.Tables(0).Rows(0)("MOBILE1").ToString

                txtOwnerPPNO.Text = ds.Tables(0).Rows(0)("PPNO").ToString
                _ownerPPNO = ds.Tables(0).Rows(0)("PPNO").ToString
                txtOwnerDLN.Text = ds.Tables(0).Rows(0)("DRIVINGLNO").ToString
                _ownerDLN = ds.Tables(0).Rows(0)("DRIVINGLNO").ToString
                txtOwnerTIN.Text = ds.Tables(0).Rows(0)("TIN").ToString
                _ownerTIN = ds.Tables(0).Rows(0)("TIN").ToString
                txtOwnerBIN.Text = ds.Tables(0).Rows(0)("BIN").ToString
                _ownerBIN = ds.Tables(0).Rows(0)("BIN").ToString

                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_OCCUPATION_TYPES Where OCTYPECODE = '" & _cmboOccp & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _Occp = ds2.Tables(0).Rows(0)("OCDEFINITION").ToString()
                    _cmboOccp = _Occp

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_THANA Where THANA_CODE = '" & _cmbPreseThana & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _PreseThana = ds3.Tables(0).Rows(0)("THANA_NAME").ToString()
                    _cmbPreseThana = _PreseThana

                End If

                Dim ds4 As New DataSet
                ds4 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_THANA Where THANA_CODE = '" & _cmbPermeThana & "' ")
                If ds4.Tables(0).Rows.Count > 0 Then

                    _PermeThana = ds4.Tables(0).Rows(0)("THANA_NAME").ToString()
                    _cmbPermeThana = _PermeThana

                End If


            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub LoadOwnerData(ByVal strOwnerCode As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet


            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_OWNER_INFO Where OWNER_CODE='" & strOwnerCode & "' and MODNO=" & intMod.ToString())



            If ds.Tables(0).Rows.Count > 0 Then


                _strOwner_Code = strOwnerCode
                _intModno = intMod

                '_intSlno = intslno

                _formMode = FormTransMode.Update


                'cmbBranch.SelectedValue = ds.Tables(0).Rows(0)("OWNER_BRANCH")

                txtOwnerCode.Text = ds.Tables(0).Rows(0)("OWNER_CODE").ToString

                cmbOccType.SelectedValue = ds.Tables(0).Rows(0)("OCTYPECODE")
                _cmbOccp = ds.Tables(0).Rows(0)("OCTYPECODE").ToString()

                If ds.Tables(0).Rows(0)("GENDER").ToString = "M" Then
                    cmbSex.SelectedIndex = 0
                ElseIf ds.Tables(0).Rows(0)("GENDER").ToString = "F" Then
                    cmbSex.SelectedIndex = 1
                Else
                    cmbSex.SelectedIndex = -1
                End If


                txtOwnerName.Text = ds.Tables(0).Rows(0)("OWNER_NAME").ToString
                _OwnerName = ds.Tables(0).Rows(0)("OWNER_NAME").ToString
                txtOwnerSpouse.Text = ds.Tables(0).Rows(0)("OWNER_SPOUSE").ToString
                _owSpouse = ds.Tables(0).Rows(0)("OWNER_SPOUSE").ToString()
                txtDob.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("DOB"))
                _owDob = NullHelper.DateToString(ds.Tables(0).Rows(0)("DOB"))
                txtOwnerFather.Text = ds.Tables(0).Rows(0)("OWNER_FATHER").ToString
                _owFather = ds.Tables(0).Rows(0)("OWNER_FATHER").ToString
                txtOwnerMother.Text = ds.Tables(0).Rows(0)("OWNER_MOTHER").ToString
                _owMother = ds.Tables(0).Rows(0)("OWNER_MOTHER").ToString


                txtPresAdd.Text = ds.Tables(0).Rows(0)("PRES_ADDR").ToString
                _owAdd = ds.Tables(0).Rows(0)("PRES_ADDR").ToString
                cmbPresThana.SelectedValue = ds.Tables(0).Rows(0)("PRES_THANA_CODE")
                _cmboPresThana = ds.Tables(0).Rows(0)("PRES_THANA_CODE").ToString()


                txtPermAdd.Text = ds.Tables(0).Rows(0)("PERM_ADDR").ToString
                _owPerm = ds.Tables(0).Rows(0)("PERM_ADDR").ToString
                cmbPermThana.SelectedValue = ds.Tables(0).Rows(0)("PERM_THANA_CODE")
                _cmboPermThana = ds.Tables(0).Rows(0)("PERM_THANA_CODE").ToString()


                txtPhnRes1.Text = ds.Tables(0).Rows(0)("PHONE_RES1").ToString
                txtPhnCityRes1.Text = ds.Tables(0).Rows(0)("PHONE_CITY_RES1").ToString
                cmbCountryRes1.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_RES1")

                txtPhnRes2.Text = ds.Tables(0).Rows(0)("PHONE_RES2").ToString
                txtPhnCityRes2.Text = ds.Tables(0).Rows(0)("PHONE_CITY_RES2").ToString
                cmbCountryRes2.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_RES2").ToString

                txtMob1.Text = ds.Tables(0).Rows(0)("MOBILE1").ToString
                _owMobile = ds.Tables(0).Rows(0)("MOBILE1").ToString
                txtMobCity1.Text = ds.Tables(0).Rows(0)("MOBILE1_CITY").ToString
                cmbCountryMob1.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_MOB1")

                txtMob2.Text = ds.Tables(0).Rows(0)("MOBILE2").ToString
                txtMobCity2.Text = ds.Tables(0).Rows(0)("MOBILE2_CITY").ToString
                cmbCountryMob2.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_MOB2")

                txtPhnOff1.Text = ds.Tables(0).Rows(0)("PHONE_OFF1").ToString
                txtPhnCityOff1.Text = ds.Tables(0).Rows(0)("PHONE_CITY_OFF1").ToString
                cmbCountryOff1.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_OFF1")

                txtPhnOff2.Text = ds.Tables(0).Rows(0)("PHONE_OFF2").ToString
                txtPhnCityOff2.Text = ds.Tables(0).Rows(0)("PHONE_CITY_OFF2").ToString
                cmbCountryOff2.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_OFF2")

                txtOwnerPPNO.Text = ds.Tables(0).Rows(0)("PPNO").ToString
                _owPPNO = ds.Tables(0).Rows(0)("PPNO").ToString
                txtOwnerDLN.Text = ds.Tables(0).Rows(0)("DRIVINGLNO").ToString
                _owDLN = ds.Tables(0).Rows(0)("DRIVINGLNO").ToString
                txtOwnerTIN.Text = ds.Tables(0).Rows(0)("TIN").ToString
                _owTIN = ds.Tables(0).Rows(0)("TIN").ToString

                txtOwnerBIN.Text = ds.Tables(0).Rows(0)("BIN").ToString
                _owBIN = ds.Tables(0).Rows(0)("BIN").ToString
                txtBBOwnerCode.Text = ds.Tables(0).Rows(0)("BB_OWNER_CODE").ToString
                txtBBCodeUpdatedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("BB_CODE_UPDATED_ON"))
                txtBBCodeUpdatedBy.Text = ds.Tables(0).Rows(0)("BB_CODE_UPDATED_BY").ToString

                txtInsertedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Inserted_On"))
                txtModifiedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Modified_On"))

                ''------------------Mizan Work (25-04-16) ---------------------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_OCCUPATION_TYPES Where OCTYPECODE = '" & _cmbOccp & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _OccpName = ds2.Tables(0).Rows(0)("OCDEFINITION").ToString()
                    _cmbOccp = _OccpName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_THANA Where THANA_CODE = '" & _cmboPresThana & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _PresThanaName = ds3.Tables(0).Rows(0)("THANA_NAME").ToString()
                    _cmboPresThana = _PresThanaName

                End If

                Dim ds4 As New DataSet
                ds4 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_THANA Where THANA_CODE ='" & _cmboPermThana & "' ")
                If ds4.Tables(0).Rows.Count > 0 Then

                    _PermThanaName = ds4.Tables(0).Rows(0)("THANA_NAME").ToString()
                    _cmboPermThana = _PermThanaName

                End If

                ''------------------Mizan Work (25-04-16) ----------------------

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

                lblModNo.Text = ds.Tables(0).Rows(0)("ModNo").ToString()
                lblVerNo.Text = ds.Tables(0).Rows(0)("ModNo").ToString()
                lblVerTot.Text = db.ExecuteDataSet(CommandType.Text, "Select Count(ModNo) From FIU_OWNER_INFO Where OWNER_CODE='" & strOwnerCode & "'").Tables(0).Rows(0)(0).ToString()





            Else
                '_intModno = 0
                '_intSlno = 0

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub LoadOwnerInfoData(ByVal strOwnerCode As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet


            'ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_OWNER_INFO Where OWNER_CODE='" & strOwnerCode & "'")
            ds = db.ExecuteDataSet(CommandType.Text, "Select MAX(MODNO) as MODNO From FIU_OWNER_INFO Where OWNER_CODE=" & strOwnerCode.ToString())



            If ds.Tables(0).Rows.Count > 0 Then


                _strOwner_Code = strOwnerCode

                _intModno = ds.Tables(0).Rows(0)("MODNO").ToString()
                '_intModno = intMod

                '_intSlno = intslno

                '_formMode = FormTransMode.Update

                LoadOwnerData(_strOwner_Code, _intModno)


                ''cmbBranch.SelectedValue = ds.Tables(0).Rows(0)("OWNER_BRANCH")

                'txtOwnerCode.Text = ds.Tables(0).Rows(0)("OWNER_CODE").ToString

                'cmbOccType.SelectedValue = ds.Tables(0).Rows(0)("OCTYPECODE")

                'If ds.Tables(0).Rows(0)("GENDER").ToString = "M" Then
                '    cmbSex.SelectedIndex = 0
                'ElseIf ds.Tables(0).Rows(0)("GENDER").ToString = "F" Then
                '    cmbSex.SelectedIndex = 1
                'Else
                '    cmbSex.SelectedIndex = -1
                'End If


                'txtOwnerName.Text = ds.Tables(0).Rows(0)("OWNER_NAME").ToString
                '_OwnerName = ds.Tables(0).Rows(0)("OWNER_NAME").ToString
                'txtOwnerSpouse.Text = ds.Tables(0).Rows(0)("OWNER_SPOUSE").ToString
                'txtDob.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("DOB"))
                'txtOwnerFather.Text = ds.Tables(0).Rows(0)("OWNER_FATHER").ToString
                '_owFather = ds.Tables(0).Rows(0)("OWNER_FATHER").ToString
                'txtOwnerMother.Text = ds.Tables(0).Rows(0)("OWNER_MOTHER").ToString
                '_owMother = ds.Tables(0).Rows(0)("OWNER_MOTHER").ToString


                'txtPresAdd.Text = ds.Tables(0).Rows(0)("PRES_ADDR").ToString
                '_owAdd = ds.Tables(0).Rows(0)("PRES_ADDR").ToString
                'cmbPresThana.SelectedValue = ds.Tables(0).Rows(0)("PRES_THANA_CODE")

                'txtPermAdd.Text = ds.Tables(0).Rows(0)("PERM_ADDR").ToString
                '_owPerm = ds.Tables(0).Rows(0)("PERM_ADDR").ToString
                'cmbPermThana.SelectedValue = ds.Tables(0).Rows(0)("PERM_THANA_CODE")

                'txtPhnRes1.Text = ds.Tables(0).Rows(0)("PHONE_RES1").ToString
                'txtPhnCityRes1.Text = ds.Tables(0).Rows(0)("PHONE_CITY_RES1").ToString
                'cmbCountryRes1.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_RES1")

                'txtPhnRes2.Text = ds.Tables(0).Rows(0)("PHONE_RES2").ToString
                'txtPhnCityRes2.Text = ds.Tables(0).Rows(0)("PHONE_CITY_RES2").ToString
                'cmbCountryRes2.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_RES2").ToString

                'txtMob1.Text = ds.Tables(0).Rows(0)("MOBILE1").ToString
                '_owMobile = ds.Tables(0).Rows(0)("MOBILE1").ToString
                'txtMobCity1.Text = ds.Tables(0).Rows(0)("MOBILE1_CITY").ToString
                'cmbCountryMob1.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_MOB1")

                'txtMob2.Text = ds.Tables(0).Rows(0)("MOBILE2").ToString
                'txtMobCity2.Text = ds.Tables(0).Rows(0)("MOBILE2_CITY").ToString
                'cmbCountryMob2.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_MOB2")

                'txtPhnOff1.Text = ds.Tables(0).Rows(0)("PHONE_OFF1").ToString
                'txtPhnCityOff1.Text = ds.Tables(0).Rows(0)("PHONE_CITY_OFF1").ToString
                'cmbCountryOff1.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_OFF1")

                'txtPhnOff2.Text = ds.Tables(0).Rows(0)("PHONE_OFF2").ToString
                'txtPhnCityOff2.Text = ds.Tables(0).Rows(0)("PHONE_CITY_OFF2").ToString
                'cmbCountryOff2.SelectedValue = ds.Tables(0).Rows(0)("COUNTRY_CODE_OFF2")

                'txtOwnerPPNO.Text = ds.Tables(0).Rows(0)("PPNO").ToString
                'txtOwnerDLN.Text = ds.Tables(0).Rows(0)("DRIVINGLNO").ToString
                'txtOwnerTIN.Text = ds.Tables(0).Rows(0)("TIN").ToString

                'txtOwnerBIN.Text = ds.Tables(0).Rows(0)("BIN").ToString
                'txtBBOwnerCode.Text = ds.Tables(0).Rows(0)("BB_OWNER_CODE").ToString
                'txtBBCodeUpdatedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("BB_CODE_UPDATED_ON"))
                'txtBBCodeUpdatedBy.Text = ds.Tables(0).Rows(0)("BB_CODE_UPDATED_BY").ToString





                'txtInsertedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Inserted_On"))
                'txtModifiedOn.Text = NullHelper.DateToString(ds.Tables(0).Rows(0)("Modified_On"))

                'lblInputBy.Text = ds.Tables(0).Rows(0)("INPUT_BY").ToString()
                'lblInputdt.Text = ds.Tables(0).Rows(0)("INPUT_DATETIME").ToString()
                'lblAuthBy.Text = ds.Tables(0).Rows(0)("AUTH_BY").ToString()
                'lblAuthDate.Text = ds.Tables(0).Rows(0)("AUTH_DATETIME").ToString()

                'chkAuthorized.Checked = ds.Tables(0).Rows(0)("IS_AUTHORIZED")

                'If ds.Tables(0).Rows(0)("STATUS") = "L" Or ds.Tables(0).Rows(0)("STATUS") = "U" Or ds.Tables(0).Rows(0)("STATUS") = "O" Then
                '    chkOpen.Checked = True
                'Else
                '    chkOpen.Checked = False
                'End If

                '_intModno = ds.Tables(0).Rows(0)("ModNo").ToString()

                'lblModNo.Text = ds.Tables(0).Rows(0)("ModNo").ToString()
                'lblVerNo.Text = ds.Tables(0).Rows(0)("ModNo").ToString()
                'lblVerTot.Text = db.ExecuteDataSet(CommandType.Text, "Select Count(ModNo) From FIU_OWNER_INFO Where OWNER_CODE='" & strOwnerCode & "'").Tables(0).Rows(0)(0).ToString()





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


            strSql = "select IS_AUTHORIZED,STATUS from FIU_OWNER_INFO where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()

            Dim ds As New DataSet
            ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0)(0) = False Then 'if not authorized

                    strSql = "delete FIU_OWNER_INFO where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString() & " and IS_AUTHORIZED=0"

                    db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    _intModno = _intModno - 1

                    tStatus = TransState.Delete


                ElseIf ds.Tables(0).Rows(0)(0) = True Then 'if authorized

                    If ds.Tables(0).Rows(0)("STATUS") = "L" Then 'if this is the last modified data

                        strSql = "delete FIU_OWNER_INFO where OWNER_CODE='" & _strOwner_Code & "'  and IS_AUTHORIZED=0"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                        strSql = "select * from FIU_OWNER_INFO where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()

                        Dim dsKeeper As New DataSet
                        dsKeeper = db.ExecuteDataSet(trans, CommandType.Text, strSql)


                        strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_OWNER_INFO where OWNER_CODE='" & _strOwner_Code & "'"


                        intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)

                        strSql = "Insert Into FIU_OWNER_INFO(OWNER_CODE, OWNER_NAME, OCTYPECODE, GENDER, OWNER_FATHER, OWNER_MOTHER, OWNER_SPOUSE, DOB, PHONE_RES1, PHONE_CITY_RES1, COUNTRY_CODE_RES1, PHONE_RES2, PHONE_CITY_RES2, COUNTRY_CODE_RES2, MOBILE1, MOBILE1_CITY, COUNTRY_CODE_MOB1, MOBILE2, MOBILE2_CITY, COUNTRY_CODE_MOB2, PHONE_OFF1, PHONE_CITY_OFF1, COUNTRY_CODE_OFF1, PHONE_OFF2, PHONE_CITY_OFF2, COUNTRY_CODE_OFF2, PPNO, DRIVINGLNO, TIN, BIN, PRES_ADDR, PRES_THANA_CODE, PERM_ADDR, PERM_THANA_CODE, BB_OWNER_CODE, BB_CODE_UPDATED_ON, BB_CODE_UPDATED_BY, INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS)" & _
                     "Values(@P_OWNER_CODE, @P_OWNER_NAME, @P_OCTYPECODE, @P_GENDER, @P_OWNER_FATHER, @P_OWNER_MOTHER, @P_OWNER_SPOUSE, @P_DOB, @P_PHONE_RES1, @P_PHONE_CITY_RES1, @P_COUNTRY_CODE_RES1, @P_PHONE_RES2, @P_PHONE_CITY_RES2, @P_COUNTRY_CODE_RES2, @P_MOBILE1, @P_MOBILE1_CITY, @P_COUNTRY_CODE_MOB1, @P_MOBILE2, @P_MOBILE2_CITY, @P_COUNTRY_CODE_MOB2, @P_PHONE_OFF1, @P_PHONE_CITY_OFF1, @P_COUNTRY_CODE_OFF1, @P_PHONE_OFF2, @P_PHONE_CITY_OFF2, @P_COUNTRY_CODE_OFF2, @P_PPNO, @P_DRIVINGLNO, @P_TIN, @P_BIN, @P_PRES_ADDR, @P_PRES_THANA_CODE, @P_PERM_ADDR, @P_PERM_THANA_CODE, @P_BB_OWNER_CODE, @P_BB_CODE_UPDATED_ON, @P_BB_CODE_UPDATED_BY,@P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'D')"


                        Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()

                        'db.AddInParameter(commProc, "@OWNER_BRANCH", DbType.String, dsKeeper.Tables(0).Rows(0)("OWNER_BRANCH"))
                        db.AddInParameter(commProc, "@P_OWNER_CODE", DbType.String, dsKeeper.Tables(0).Rows(0)("OWNER_CODE"))
                        db.AddInParameter(commProc, "@P_OWNER_NAME", DbType.String, dsKeeper.Tables(0).Rows(0)("OWNER_NAME"))
                        db.AddInParameter(commProc, "@P_OCTYPECODE", DbType.String, dsKeeper.Tables(0).Rows(0)("OCTYPECODE"))
                        db.AddInParameter(commProc, "@P_GENDER", DbType.String, dsKeeper.Tables(0).Rows(0)("GENDER"))
                        db.AddInParameter(commProc, "@P_OWNER_FATHER", DbType.String, dsKeeper.Tables(0).Rows(0)("OWNER_FATHER"))
                        db.AddInParameter(commProc, "@P_OWNER_MOTHER", DbType.String, dsKeeper.Tables(0).Rows(0)("OWNER_MOTHER"))
                        db.AddInParameter(commProc, "@P_OWNER_SPOUSE", DbType.String, dsKeeper.Tables(0).Rows(0)("OWNER_SPOUSE"))



                        db.AddInParameter(commProc, "@P_DOB", DbType.DateTime, dsKeeper.Tables(0).Rows(0)("DOB"))


                        db.AddInParameter(commProc, "@P_PHONE_RES1", DbType.String, dsKeeper.Tables(0).Rows(0)("PHONE_RES1"))
                        db.AddInParameter(commProc, "@P_PHONE_CITY_RES1", DbType.String, dsKeeper.Tables(0).Rows(0)("PHONE_CITY_RES1"))
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_RES1", DbType.String, dsKeeper.Tables(0).Rows(0)("COUNTRY_CODE_RES1"))
                        db.AddInParameter(commProc, "@P_PHONE_RES2", DbType.String, dsKeeper.Tables(0).Rows(0)("PHONE_RES2"))
                        db.AddInParameter(commProc, "@P_PHONE_CITY_RES2", DbType.String, dsKeeper.Tables(0).Rows(0)("PHONE_CITY_RES2"))
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_RES2", DbType.String, dsKeeper.Tables(0).Rows(0)("COUNTRY_CODE_RES2"))
                        db.AddInParameter(commProc, "@P_MOBILE1", DbType.String, dsKeeper.Tables(0).Rows(0)("MOBILE1"))
                        db.AddInParameter(commProc, "@P_MOBILE1_CITY", DbType.String, dsKeeper.Tables(0).Rows(0)("MOBILE1_CITY"))
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_MOB1", DbType.String, dsKeeper.Tables(0).Rows(0)("COUNTRY_CODE_MOB1"))
                        db.AddInParameter(commProc, "@P_MOBILE2", DbType.String, dsKeeper.Tables(0).Rows(0)("MOBILE2"))
                        db.AddInParameter(commProc, "@P_MOBILE2_CITY", DbType.String, dsKeeper.Tables(0).Rows(0)("MOBILE2_CITY"))
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_MOB2", DbType.String, dsKeeper.Tables(0).Rows(0)("COUNTRY_CODE_MOB2"))
                        db.AddInParameter(commProc, "@P_PHONE_OFF1", DbType.String, dsKeeper.Tables(0).Rows(0)("PHONE_OFF1"))
                        db.AddInParameter(commProc, "@P_PHONE_CITY_OFF1", DbType.String, dsKeeper.Tables(0).Rows(0)("PHONE_CITY_OFF1"))
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_OFF1", DbType.String, dsKeeper.Tables(0).Rows(0)("COUNTRY_CODE_OFF1"))
                        db.AddInParameter(commProc, "@P_PHONE_OFF2", DbType.String, dsKeeper.Tables(0).Rows(0)("PHONE_OFF2"))
                        db.AddInParameter(commProc, "@P_PHONE_CITY_OFF2", DbType.String, dsKeeper.Tables(0).Rows(0)("PHONE_CITY_OFF2"))
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_OFF2", DbType.String, dsKeeper.Tables(0).Rows(0)("COUNTRY_CODE_OFF2"))
                        db.AddInParameter(commProc, "@P_PPNO", DbType.String, dsKeeper.Tables(0).Rows(0)("PPNO"))
                        db.AddInParameter(commProc, "@P_DRIVINGLNO", DbType.String, dsKeeper.Tables(0).Rows(0)("DRIVINGLNO"))
                        db.AddInParameter(commProc, "@P_TIN", DbType.String, dsKeeper.Tables(0).Rows(0)("TIN"))
                        db.AddInParameter(commProc, "@P_BIN", DbType.String, dsKeeper.Tables(0).Rows(0)("BIN"))
                        db.AddInParameter(commProc, "@P_PRES_ADDR", DbType.String, dsKeeper.Tables(0).Rows(0)("PRES_ADDR"))
                        db.AddInParameter(commProc, "@P_PRES_THANA_CODE", DbType.String, dsKeeper.Tables(0).Rows(0)("PRES_THANA_CODE"))
                        db.AddInParameter(commProc, "@P_PERM_ADDR", DbType.String, dsKeeper.Tables(0).Rows(0)("PERM_ADDR"))
                        db.AddInParameter(commProc, "@P_PERM_THANA_CODE", DbType.String, dsKeeper.Tables(0).Rows(0)("PERM_THANA_CODE"))
                        db.AddInParameter(commProc, "@P_BB_OWNER_CODE", DbType.String, dsKeeper.Tables(0).Rows(0)("BB_OWNER_CODE"))


                        db.AddInParameter(commProc, "@P_BB_CODE_UPDATED_ON", DbType.DateTime, dsKeeper.Tables(0).Rows(0)("BB_CODE_UPDATED_ON"))

                        db.AddInParameter(commProc, "@P_BB_CODE_UPDATED_BY", DbType.String, dsKeeper.Tables(0).Rows(0)("BB_CODE_UPDATED_BY"))






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

            log_message = "Delete Owner Code " + txtOwnerCode.Text.Trim()
            Logger.system_log(log_message)
        End Using


        Return tStatus

    End Function

    Private Function CheckForDelete() As Boolean

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String = ""

        strSql = "select IS_AUTHORIZED,STATUS from FIU_OWNER_INFO where OWNER_CODE='" & _strOwner_Code & "' and MODNO=" & _intModno.ToString()

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




    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmOwnerInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        CommonUtil.FillComboBox("select OCTYPECODE, OCDEFINITION  from FIU_OCCUPATION_TYPES where STATUS='L'", cmbOccType)

        CommonUtil.FillComboBox("select BRANCH_CODE ,BRANCH_NAME  from FIU_BANK_BRANCH where BANK_CODE='026' and STATUS='L' order by BRANCH_NAME ", cmbBranch)
        CommonUtil.FillComboBox("select  THANA_CODE,THANA_NAME from FIU_THANA where STATUS='L' order by THANA_NAME", cmbPresThana)
        CommonUtil.FillComboBox("select  THANA_CODE,THANA_NAME from FIU_THANA where STATUS='L' order by THANA_NAME", cmbPermThana)
        CommonUtil.FillComboBox("select COUNTRY_CODE,COUNTRY_NAME from FIU_COUNTRY_INFO where STATUS='L' order by COUNTRY_NAME", cmbCountryRes1)
        CommonUtil.FillComboBox("select COUNTRY_CODE,COUNTRY_NAME from FIU_COUNTRY_INFO where STATUS='L' order by COUNTRY_NAME", cmbCountryRes2)
        CommonUtil.FillComboBox("select COUNTRY_CODE,COUNTRY_NAME from FIU_COUNTRY_INFO where STATUS='L' order by COUNTRY_NAME", cmbCountryMob1)
        CommonUtil.FillComboBox("select COUNTRY_CODE,COUNTRY_NAME from FIU_COUNTRY_INFO where STATUS='L' order by COUNTRY_NAME", cmbCountryMob2)
        CommonUtil.FillComboBox("select COUNTRY_CODE,COUNTRY_NAME from FIU_COUNTRY_INFO where STATUS='L' order by COUNTRY_NAME", cmbCountryOff1)
        CommonUtil.FillComboBox("select COUNTRY_CODE,COUNTRY_NAME from FIU_COUNTRY_INFO where STATUS='L' order by COUNTRY_NAME", cmbCountryOff2)

        If _intModno > 0 Then  ' Do not understand
            'LoadAppData(_intSlno, _intModno)

            LoadOwnerData(_strOwner_Code, _intModno)

        ElseIf _strOwner_Code <> "" Then

            LoadOwnerInfoData(_strOwner_Code)

        End If

        EnableUnlock()

        DisableNew()
        DisableSave()
        DisableDelete()
        DisableAuth()

        DisableClear()
        DisableRefresh()

        DisableFields()
        btnMoreDetail.Enabled = False

    End Sub


    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

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

        If txtOwnerCode.ReadOnly = False Then
            txtOwnerCode.Focus()
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

                        LoadOwnerData(_strOwner_Code, _intModno)

                        lblToolStatus.Text = "!! Information Added Successfully !!"

                        _formMode = FormTransMode.Update


                        EnableDelete()

                        EnableRefresh()

                    ElseIf tState = TransState.Update Then

                        'LoadAppData(_intSlno, _intModno)

                        LoadOwnerData(_strOwner_Code, _intModno)

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
        LoadOwnerData(_strOwner_Code, _intModno)
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

                        LoadOwnerData(_strOwner_Code, _intModno)

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
            LoadOwnerData(_strOwner_Code, _intModno - 1)

        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click

        Dim strOwner_Code As String = _strOwner_Code
        Dim intModno As Integer = _intModno


        LoadOwnerData(_strOwner_Code, _intModno + 1)

        If _intModno = 0 Then
            'LoadAppData(intSlno, intModno)
            LoadOwnerData(strOwner_Code, intModno)
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

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal strOwnerCode As String, ByVal intMod As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _strOwner_Code = strOwnerCode
        _intModno = intMod


    End Sub

    Public Sub New(ByVal strOwnerCode As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        txtOwnerCode.Text = strOwnerCode
        _strAccNO = strOwnerCode
        _strOwner_Code = strOwnerCode

    End Sub






    Private Sub cmbBranch_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBranch.SelectionChangeCommitted

        Try
            If Not cmbBranch.Text.Trim() = "" Then
                Dim strSql As String = ""

                strSql = "select right('00000' + convert(varchar,isnull(max(convert(bigint,substring(OWNER_CODE,8,5))),0)+1),5) from dbo.FIU_OWNER_INFO where substring(OWNER_CODE,1,7)='026" & cmbBranch.SelectedValue.ToString() & "'"    '"0201'"

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim td As New DataTable
                td = db.ExecuteDataSet(CommandType.Text, strSql).Tables(0)

                If td.Rows.Count > 0 Then
                    txtOwnerCode.Text = "026" + cmbBranch.SelectedValue.ToString().Trim() + td.Rows(0)(0).ToString().Trim()
                End If


            End If

            '"select right('00000' + convert(varchar,isnull(max(convert(bigint,substring(OWNER_CODE,8,5))),0)+1),5) from dbo.FIU_OWNER_INFO where substring(OWNER_CODE,1,7)='0260201'"


        Catch ex As Exception

        End Try
    End Sub
   



    Private Sub btnMoreDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoreDetail.Click

        Dim strId As String = txtOwnerCode.Text


        If strId = "" Then
            MessageBox.Show("Please Enter Owner Code!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub

        End If



        Dim frmOwnergoAmlDet As New FrmOwnergoAmlDet(strId)
        frmOwnergoAmlDet.ShowDialog()

    End Sub
End Class
