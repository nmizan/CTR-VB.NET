Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization

Public Class FrmBankBranch

#Region "Global Variables"

    Dim _formName As String = "MaintenanceBankBranchDetail"
    Dim opt As SecForm = New SecForm(_formName)

    Dim _formMode As FormTransMode
    'Dim _intSlno As Integer = 0
    Dim _intModno As Integer = 0
    Dim _strBankBranch_Code As String = ""
    Dim log_message As String

    Dim _BName As String = ""
    Dim _BLoc As String = ""
    Dim _cmboThana As String = ""
    Dim _cmboDistrict As String = ""
    Dim _cmboThanaName As String = ""
    Dim _cmboDistrictName As String = ""
    Dim _Po As String = ""
    Dim _WNo As String = ""
    Dim _InstCode As String = ""
    Dim _SCode As String = ""
    Dim _Uni As String = ""
    Dim _BNo As String = ""

    Dim _BrName As String = ""
    Dim _BrNo As String = ""
    Dim _BrLoc As String = ""
    Dim _cmboboxThana As String = ""
    Dim _cmboboxDistrict As String = ""
    Dim _ThanaName As String = ""
    Dim _DistrictName As String = ""
    Dim _Post As String = ""
    Dim _WardNo As String = ""
    Dim _InstituteCode As String = ""
    Dim _SwiftCode As String = ""
    Dim _UniMun As String = ""

    Dim BranchList As New List(Of String)
    Dim _BranchLog As String = ""
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
        txtBranchCode.ReadOnly = True
        txtBranchName.ReadOnly = True
        txtInserted_On.ReadOnly = True
        txtModified_On.ReadOnly = True
        txtBranch_No.ReadOnly() = True
        txtBranchLoc.ReadOnly() = True
        txtInstitutionCode.ReadOnly() = True
        txtSwiftCode.ReadOnly() = True
        txtPO.ReadOnly() = True
        txtUNI_MUN.ReadOnly() = True
        txtWard.ReadOnly() = True
        cmbBankCode.Enabled = False
        cmbDistrict.Enabled = False
        cmbThana.Enabled = False
        'txtPrevDivCode.ReadOnly = True
    End Sub

    Private Sub EnableFields()
        txtBranchCode.ReadOnly = False
        txtBranchName.ReadOnly = False
        txtInserted_On.ReadOnly = False
        txtModified_On.ReadOnly = False
        txtBranch_No.ReadOnly() = False
        txtBranchLoc.ReadOnly() = False
        txtInstitutionCode.ReadOnly() = False
        txtSwiftCode.ReadOnly() = False
        txtPO.ReadOnly() = False
        txtUNI_MUN.ReadOnly() = False
        txtWard.ReadOnly() = False
        cmbBankCode.Enabled = True
        cmbDistrict.Enabled = True
        cmbThana.Enabled = True
        'txtPrevDivCode.ReadOnly = False

    End Sub


    Private Sub ClearFields()
        txtBranchCode.Clear()
        txtBranchName.Clear()
        txtInserted_On.Clear()
        txtModified_On.Clear()
        txtBranch_No.Clear()
        txtBranchLoc.Clear()
        txtPO.Clear()
        txtUNI_MUN.Clear()
        txtWard.Clear()
        txtInstitutionCode.Clear()
        txtSwiftCode.Clear()
        'txtPrevDivCode.Clear()
    End Sub

    Private Sub ClearFieldsAll()
        txtBranchCode.Clear()
        txtBranchName.Clear()
        txtInserted_On.Clear()
        txtModified_On.Clear()
        txtBranch_No.Clear()
        txtBranchLoc.Clear()
        txtPO.Clear()
        txtUNI_MUN.Clear()
        txtWard.Clear()
        txtInstitutionCode.Clear()
        txtSwiftCode.Clear()
        ' txtPrevDivCode.Clear()

        '_intSlno = 0
        _strBankBranch_Code = ""
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

        If txtBranchCode.Text.Trim() = "" Then
            MessageBox.Show("Code required !!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtBranchCode.Focus()
            Return False
        ElseIf txtBranchName.Text.Trim() = "" Then
            MessageBox.Show("Name required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtBranchName.Focus()
            Return False
        ElseIf txtInstitutionCode.Text.Trim() = "" And txtSwiftCode.Text.Trim() = "" Then
            MessageBox.Show("Institution Or Swift Code required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSwiftCode.Focus()
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

            strSql = "Insert Into FIU_BANK_BRANCH(BRANCH_CODE, BANK_CODE, BRANCH_NAME, BRANCH_LOC, THANA_CODE, DIST_CODE, PO, UNI_MUN, WARD, BRANCH_NO, INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS,INSTITUTION_CODE,SWIFT_CODE)" & _
                     "Values(@P_BRANCH_CODE, @P_BANK_CODE, @P_BRANCH_NAME, @P_BRANCH_LOC, @P_THANA_CODE, @P_DIST_CODE, @P_PO, @P_UNI_MUN, @P_WARD, @P_BRANCH_NO, @P_Inserted_On,@P_Modified_On,1,@P_Input_By,getdate(),0,'U',@INSTITUTION_CODE,@SWIFT_CODE)"

            Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

            commProc.Parameters.Clear()
            db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, txtBranchCode.Text.Trim())
            db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, cmbBankCode.SelectedValue)
            db.AddInParameter(commProc, "@P_BRANCH_NAME", DbType.String, txtBranchName.Text.Trim())
            db.AddInParameter(commProc, "@P_BRANCH_LOC", DbType.String, txtBranchLoc.Text.Trim())
            db.AddInParameter(commProc, "@P_THANA_CODE", DbType.String, cmbThana.SelectedValue)
            db.AddInParameter(commProc, "@P_DIST_CODE", DbType.String, cmbDistrict.SelectedValue)
            db.AddInParameter(commProc, "@P_PO", DbType.String, txtPO.Text.Trim())
            db.AddInParameter(commProc, "@P_UNI_MUN", DbType.String, txtUNI_MUN.Text.Trim())
            db.AddInParameter(commProc, "@P_WARD", DbType.String, txtWard.Text.Trim())
            db.AddInParameter(commProc, "@P_BRANCH_NO", DbType.String, txtBranch_No.Text.Trim())

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

            db.AddInParameter(commProc, "@INSTITUTION_CODE", DbType.String, txtInstitutionCode.Text.Trim())

            db.AddInParameter(commProc, "@SWIFT_CODE", DbType.String, txtSwiftCode.Text.Trim())

            Dim result As Integer

            result = db.ExecuteNonQuery(commProc)

            If result < 0 Then  '  Don't Understand
                tStatus = TransState.Exist
            Else
                tStatus = TransState.Add
                '_intSlno = intSlno
                _strBankBranch_Code = txtBranchCode.Text.Trim()

                _intModno = 1

                log_message = " Added : Branch Code : " + txtBranchCode.Text.Trim() + "." + " " + " Branch Name : " + txtBranchName.Text.Trim()
                Logger.system_log(log_message)

            End If


           

        ElseIf _formMode = FormTransMode.Update Then


            Using conn As DbConnection = db.CreateConnection()


                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                db.ExecuteNonQuery(trans, CommandType.Text, "delete FIU_BANK_BRANCH where BRANCH_CODE='" & _strBankBranch_Code & "' and IS_AUTHORIZED=0")
                Dim ds As New DataSet


                strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_BANK_BRANCH where BRANCH_CODE='" & _strBankBranch_Code & "'"


                intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)

                strSql = "Insert Into FIU_BANK_BRANCH(BRANCH_CODE, BANK_CODE, BRANCH_NAME, BRANCH_LOC, THANA_CODE, DIST_CODE, PO, UNI_MUN, WARD, BRANCH_NO, INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED, STATUS,INSTITUTION_CODE,SWIFT_CODE)" & _
                     "Values(@P_BRANCH_CODE, @P_BANK_CODE, @P_BRANCH_NAME, @P_BRANCH_LOC, @P_THANA_CODE, @P_DIST_CODE, @P_PO, @P_UNI_MUN, @P_WARD, @P_BRANCH_NO,@P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'U',@INSTITUTION_CODE,@SWIFT_CODE)"


                Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, txtBranchCode.Text.Trim())
                db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, cmbBankCode.SelectedValue)
                db.AddInParameter(commProc, "@P_BRANCH_NAME", DbType.String, txtBranchName.Text.Trim())
                db.AddInParameter(commProc, "@P_BRANCH_LOC", DbType.String, txtBranchLoc.Text.Trim())
                db.AddInParameter(commProc, "@P_THANA_CODE", DbType.String, cmbThana.SelectedValue)
                db.AddInParameter(commProc, "@P_DIST_CODE", DbType.String, cmbDistrict.SelectedValue)
                db.AddInParameter(commProc, "@P_PO", DbType.String, txtPO.Text.Trim())
                db.AddInParameter(commProc, "@P_UNI_MUN", DbType.String, txtUNI_MUN.Text.Trim())
                db.AddInParameter(commProc, "@P_WARD", DbType.String, txtWard.Text.Trim())
                db.AddInParameter(commProc, "@P_BRANCH_NO", DbType.String, txtBranch_No.Text.Trim())


                db.AddInParameter(commProc, "@INSTITUTION_CODE", DbType.String, txtInstitutionCode.Text.Trim())
                db.AddInParameter(commProc, "@SWIFT_CODE", DbType.String, txtSwiftCode.Text.Trim())

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

                    '--------------Mizan Work (19-04-16)---------------------

                    If _BName <> txtBranchName.Text.Trim() Then
                        log_message = " Branch Name : " + _BName + " " + " To " + " " + txtBranchName.Text.Trim() + "." + " "
                        BranchList.Add(log_message)
                    End If

                    If _BLoc <> txtBranchLoc.Text.Trim() Then
                        If _BLoc = "" Then
                            log_message = " Branch Location : " + txtBranchLoc.Text.Trim() + "." + " "
                        Else
                            log_message = " Branch Location : " + _BLoc + " " + " To " + " " + txtBranchLoc.Text.Trim() + "." + " "
                        End If
                        BranchList.Add(log_message)
                    End If

                    If _cmboThana <> cmbThana.Text Then
                        If _cmboThana = "" Then
                            log_message = " Branch Thana : " + cmbThana.Text + "." + " "
                        Else
                            log_message = " Branch Thana : " + _cmboThana + " " + " To " + " " + cmbThana.Text + "." + " "
                        End If

                        BranchList.Add(log_message)
                    End If

                    If _cmboDistrict <> cmbDistrict.Text Then
                        If _cmboDistrict = "" Then
                            log_message = " Branch District : " + cmbDistrict.Text + "." + " "
                        Else
                            log_message = " Branch District : " + _cmboDistrict + " " + " To " + " " + cmbDistrict.Text + "." + " "
                        End If

                        BranchList.Add(log_message)
                    End If

                    If _BNo <> txtBranch_No.Text.Trim() Then
                        If _BNo = "" Then
                            log_message = " Branch No : " + txtBranch_No.Text.Trim() + "." + " "
                        Else
                            log_message = " Branch No : " + _BNo + " " + " To " + " " + txtBranch_No.Text.Trim() + "." + " "
                        End If
                        BranchList.Add(log_message)
                    End If
                    If _Po <> txtPO.Text.Trim() Then
                        If _Po = "" Then
                            log_message = " PO : " + txtPO.Text.Trim()
                        Else
                            log_message = " PO : " + _Po + " " + " To " + " " + txtPO.Text.Trim() + "." + " "
                        End If
                        BranchList.Add(log_message)
                    End If
                    If _InstCode <> txtInstitutionCode.Text.Trim() Then
                        If _InstCode = "" Then
                            log_message = " Institute Code : " + txtInstitutionCode.Text.Trim() + "." + " "
                        Else
                            log_message = " Institute Code : " + _InstCode + " " + " To " + " " + txtInstitutionCode.Text.Trim() + "." + " "
                        End If
                        BranchList.Add(log_message)
                    End If
                    If _WNo <> txtWard.Text.Trim() Then
                        If _WNo = "" Then
                            log_message = " Ward No : " + txtWard.Text.Trim() + "." + " "
                        Else
                            log_message = " Ward No : " + _WNo + " " + " To " + " " + txtWard.Text.Trim() + "." + " "
                        End If
                        BranchList.Add(log_message)
                    End If
                    If _SCode <> txtSwiftCode.Text.Trim() Then
                        If _SCode = "" Then
                            log_message = " Swift Code : " + txtSwiftCode.Text.Trim() + "." + " "
                        Else
                            log_message = " swift Code : " + _SCode + " " + " To " + " " + txtSwiftCode.Text.Trim() + "." + " "
                        End If
                        BranchList.Add(log_message)
                    End If
                    If _Uni <> txtUNI_MUN.Text.Trim() Then
                        If _Uni = "" Then
                            log_message = " UNI MUN : " + txtUNI_MUN.Text.Trim() + "." + " "
                        Else
                            log_message = "  UNI MUN : " + _Uni + " " + " To " + " " + txtUNI_MUN.Text.Trim() + "." + " "
                        End If
                        BranchList.Add(log_message)
                    End If

                    For Each branchloglist As String In BranchList
                        _BranchLog += branchloglist
                    Next

                    _log = " Updated : Branch Code : " + txtBranchCode.Text.ToString() + "." + " " + _BranchLog

                    Logger.system_log(_log)

                    _BranchLog = ""
                    BranchList.Clear()


                    '--------------Mizan Work (19-04-16)---------------------

                End If

                'intModno = db.ExecuteDataSet(trans, CommandType.Text, "select max(ISNULL(MODNO,0))+1 maxmodno from DEPARTMENT where SLNO=" & _intSlno.ToString()).Tables(0).Rows(0)(0)

                trans.Commit()

            End Using

        End If

        Return tStatus

    End Function

    '--------------Mizan Work (19-04-16)---------------------

    Private Sub LoadBankBranchDataForAuth(ByVal strBankBranchCode As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet


            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_BANK_BRANCH Where BRANCH_CODE='" & strBankBranchCode & "' and STATUS = 'L' ")



            If ds.Tables(0).Rows.Count > 0 Then


                _strBankBranch_Code = strBankBranchCode

                _formMode = FormTransMode.Update

                txtBranchCode.Text = ds.Tables(0).Rows(0)("BRANCH_CODE").ToString()

                cmbBankCode.SelectedValue = ds.Tables(0).Rows(0)("BANK_CODE")

                txtBranchName.Text = ds.Tables(0).Rows(0)("BRANCH_NAME").ToString()
                _BrName = ds.Tables(0).Rows(0)("BRANCH_NAME").ToString()
                txtBranchLoc.Text = ds.Tables(0).Rows(0)("BRANCH_LOC").ToString()
                _BrLoc = ds.Tables(0).Rows(0)("BRANCH_LOC").ToString()

                'cmbDistrict.SelectedValue = ds.Tables(0).Rows(0)("DIST_CODE")
                _cmboboxDistrict = ds.Tables(0).Rows(0)("DIST_CODE").ToString()
                'cmbThana.SelectedValue = ds.Tables(0).Rows(0)("THANA_CODE")
                _cmboboxThana = ds.Tables(0).Rows(0)("THANA_CODE")

                txtPO.Text = ds.Tables(0).Rows(0)("PO").ToString()
                _Post = ds.Tables(0).Rows(0)("PO").ToString()
                txtUNI_MUN.Text = ds.Tables(0).Rows(0)("UNI_MUN").ToString()
                _UniMun = ds.Tables(0).Rows(0)("UNI_MUN").ToString()
                txtWard.Text = ds.Tables(0).Rows(0)("WARD").ToString()
                _WardNo = ds.Tables(0).Rows(0)("WARD").ToString()
                txtBranch_No.Text = ds.Tables(0).Rows(0)("BRANCH_NO").ToString()
                _BrNo = ds.Tables(0).Rows(0)("BRANCH_NO").ToString()

                txtInstitutionCode.Text = ds.Tables(0).Rows(0)("INSTITUTION_CODE").ToString()
                _InstituteCode = ds.Tables(0).Rows(0)("INSTITUTION_CODE").ToString()
                txtSwiftCode.Text = ds.Tables(0).Rows(0)("SWIFT_CODE").ToString()
                _SwiftCode = ds.Tables(0).Rows(0)("SWIFT_CODE").ToString()

                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_DISTRICT Where DIST_CODE = '" & _cmboboxDistrict & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _DistrictName = ds2.Tables(0).Rows(0)("DIST_NAME").ToString()
                    _cmboboxDistrict = _DistrictName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_THANA Where THANA_CODE = '" & _cmboboxThana & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _ThanaName = ds3.Tables(0).Rows(0)("THANA_NAME").ToString()
                    _cmboboxThana = _ThanaName

                End If

            Else


                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadBankBranchData(ByVal strBankBranchCode As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet


            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_BANK_BRANCH Where BRANCH_CODE='" & strBankBranchCode & "' and MODNO=" & intMod.ToString())



            If ds.Tables(0).Rows.Count > 0 Then


                _strBankBranch_Code = strBankBranchCode
                _intModno = intMod

                '_intSlno = intslno

                _formMode = FormTransMode.Update




                txtBranchCode.Text = ds.Tables(0).Rows(0)("BRANCH_CODE").ToString()

                cmbBankCode.SelectedValue = ds.Tables(0).Rows(0)("BANK_CODE")

                txtBranchName.Text = ds.Tables(0).Rows(0)("BRANCH_NAME").ToString()
                _BName = ds.Tables(0).Rows(0)("BRANCH_NAME").ToString()
                txtBranchLoc.Text = ds.Tables(0).Rows(0)("BRANCH_LOC").ToString()
                _BLoc = ds.Tables(0).Rows(0)("BRANCH_LOC").ToString()

                cmbDistrict.SelectedValue = ds.Tables(0).Rows(0)("DIST_CODE")
                _cmboDistrict = ds.Tables(0).Rows(0)("DIST_CODE").ToString()
                cmbThana.SelectedValue = ds.Tables(0).Rows(0)("THANA_CODE")
                _cmboThana = ds.Tables(0).Rows(0)("THANA_CODE")
                
                txtPO.Text = ds.Tables(0).Rows(0)("PO").ToString()
                _Po = ds.Tables(0).Rows(0)("PO").ToString()
                txtUNI_MUN.Text = ds.Tables(0).Rows(0)("UNI_MUN").ToString()
                _Uni = ds.Tables(0).Rows(0)("UNI_MUN").ToString()
                txtWard.Text = ds.Tables(0).Rows(0)("WARD").ToString()
                _WNo = ds.Tables(0).Rows(0)("WARD").ToString()
                txtBranch_No.Text = ds.Tables(0).Rows(0)("BRANCH_NO").ToString()
                _BNo = ds.Tables(0).Rows(0)("BRANCH_NO").ToString()

                txtInstitutionCode.Text = ds.Tables(0).Rows(0)("INSTITUTION_CODE").ToString()
                _InstCode = ds.Tables(0).Rows(0)("INSTITUTION_CODE").ToString()
                txtSwiftCode.Text = ds.Tables(0).Rows(0)("SWIFT_CODE").ToString()
                _SCode = ds.Tables(0).Rows(0)("SWIFT_CODE").ToString()

                ''------------------Mizan Work (25-04-16) ---------------------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_DISTRICT Where DIST_CODE = '" & _cmboDistrict & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _cmboDistrictName = ds2.Tables(0).Rows(0)("DIST_NAME").ToString()
                    _cmboDistrict = _cmboDistrictName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_THANA Where THANA_CODE = '" & _cmboThana & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _cmboThanaName = ds3.Tables(0).Rows(0)("THANA_NAME").ToString()
                    _cmboThana = _cmboThanaName

                End If
                ''------------------Mizan Work (25-04-16) ---------------------
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
                lblVerTot.Text = db.ExecuteDataSet(CommandType.Text, "Select Count(MODNO) From FIU_BANK_BRANCH Where BRANCH_CODE='" & strBankBranchCode & "'").Tables(0).Rows(0)(0).ToString()



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

    Public Sub New(ByVal strBankBranchCode As String, ByVal intMod As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LoadBankBranchData(strBankBranchCode, intMod)

        ' Add any initialization after the InitializeComponent() call.
        _strBankBranch_Code = strBankBranchCode
        _intModno = intMod


    End Sub

    '--------------Mizan Work (19-04-16)---------------------

    Private Function AuthorizeData() As TransState

        If _intModno > 1 Then

            LoadBankBranchDataForAuth(_strBankBranch_Code)

            Dim tStatus As TransState

            Dim strSql As String

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                strSql = "select IS_AUTHORIZED,STATUS from FIU_BANK_BRANCH where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & _intModno.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_BANK_BRANCH set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & _intModno.ToString()


                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_BANK_BRANCH set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & _intModno.ToString()

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If _intModno > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_BANK_BRANCH set STATUS = 'C' " & _
                                " where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_BANK_BRANCH set STATUS = 'O' " & _
                                " where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

                        If _BrName <> _BName Then
                            If _BrName = "" Then
                                log_message = " Branch Name : " + _BName + "." + " "
                            Else
                                log_message = " Branch Name : " + _BrName + " " + " To " + " " + _BName + "." + " "
                            End If

                            BranchList.Add(log_message)
                        End If

                        If _BrLoc <> _BLoc Then
                            If _BrLoc = "" Then
                                log_message = " Branch Location : " + _BLoc + "." + " "
                            Else
                                log_message = " Branch Location : " + _BrLoc + " " + " To " + " " + _BLoc + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If

                        If _cmboboxThana <> _cmboThana Then
                            If _cmboboxThana = "" Then
                                log_message = " Branch Thana : " + _cmboThana + "." + " "
                            Else
                                log_message = " Branch Thana : " + _cmboboxThana + " " + " To " + " " + _cmboThana + "." + " "
                            End If

                            BranchList.Add(log_message)
                        End If

                        If _cmboboxDistrict <> _cmboDistrict Then
                            If _cmboboxDistrict = "" Then
                                log_message = " Branch District : " + _cmboDistrict + "." + " "
                            Else
                                log_message = " Branch District : " + _cmboboxDistrict + " " + " To " + " " + _cmboDistrict + "." + " "
                            End If

                            BranchList.Add(log_message)
                        End If

                        If _BrNo <> _BNo Then
                            If _BrNo = "" Then
                                log_message = " Branch No : " + _BNo + "." + " "
                            Else
                                log_message = " Branch No : " + _BrNo + " " + " To " + " " + _BNo + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If
                        If _Post <> _Po Then
                            If _Post = "" Then
                                log_message = " PO : " + _Po + "." + " "
                            Else
                                log_message = " PO : " + _Post + " " + " To " + " " + _Po + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If
                        If _InstituteCode <> _InstCode Then
                            If _InstituteCode = "" Then
                                log_message = " Institute Code : " + _InstCode + "." + " "
                            Else
                                log_message = " Institute Code : " + _InstituteCode + " " + " To " + " " + _InstCode + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If
                        If _WardNo <> _WNo Then
                            If _WardNo = "" Then
                                log_message = " Ward No : " + _WNo + "." + " "
                            Else
                                log_message = " Ward No : " + _WardNo + " " + " To " + " " + _WNo + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If
                        If _SwiftCode <> _SCode Then
                            If _SwiftCode = "" Then
                                log_message = " Swift Code : " + _SCode + "." + " "
                            Else
                                log_message = " swift Code : " + _SwiftCode + " " + " To " + " " + _SCode + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If
                        If _UniMun <> _Uni Then
                            If _UniMun = "" Then
                                log_message = " UNI MUN : " + _Uni + "." + " "
                            Else
                                log_message = "  UNI MUN : " + _UniMun + " " + " To " + " " + _Uni + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If

                        For Each branchloglist As String In BranchList
                            _BranchLog += branchloglist
                        Next

                        _log = " Authorized : Branch Code : " + txtBranchCode.Text.ToString() + "." + " " + _BranchLog

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

                strSql = "select IS_AUTHORIZED,STATUS from FIU_BANK_BRANCH where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & _intModno.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_BANK_BRANCH set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & _intModno.ToString()


                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_BANK_BRANCH set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & _intModno.ToString()

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If _intModno > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_BANK_BRANCH set STATUS = 'C' " & _
                                " where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_BANK_BRANCH set STATUS = 'O' " & _
                                " where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & (_intModno - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

                        If _BrName <> _BName Then
                            If _BrName = "" Then
                                log_message = " Branch Name : " + _BName + "." + " "
                            Else
                                log_message = " Branch Name : " + _BrName + " " + " To " + " " + _BName + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If

                        If _BrLoc <> _BLoc Then
                            If _BrLoc = "" Then
                                'log_message = " Branch Location : " + _BLoc + "." + " "
                            Else
                                log_message = " Branch Location : " + _BrLoc + " " + " To " + " " + _BLoc + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If

                        If _cmboboxThana <> _cmboThana Then
                            If _cmboboxThana = "" Then
                            Else
                                log_message = " Branch Thana : " + _cmboboxThana + " " + " To " + " " + _cmboThana + "." + " "
                            End If

                            BranchList.Add(log_message)
                        End If

                        If _cmboboxDistrict <> _cmboDistrict Then
                            If _cmboboxDistrict = "" Then
                            Else
                                log_message = " Branch District : " + _cmboboxDistrict + " " + " To " + " " + _cmboDistrict + "." + " "
                            End If

                            BranchList.Add(log_message)
                        End If

                        If _BrNo <> _BNo Then
                            If _BrNo = "" Then
                                'log_message = " Branch No : " + _BNo + "." + " "
                            Else
                                log_message = " Branch No : " + _BrNo + " " + " To " + " " + _BNo + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If
                        If _Post <> _Po Then
                            If _Post = "" Then
                                ' log_message = " PO : " + _Po + "." + " "
                            Else
                                log_message = " PO : " + _Post + " " + " To " + " " + _Po + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If
                        If _InstituteCode <> _InstCode Then
                            If _InstituteCode = "" Then
                                log_message = " Institute Code : " + _InstCode + "." + " "
                            Else
                                log_message = " Institute Code : " + _InstituteCode + " " + " To " + " " + _InstCode + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If
                        If _WardNo <> _WNo Then
                            If _WardNo = "" Then
                                'log_message = " Ward No : " + _WNo + "." + " "
                            Else
                                log_message = " Ward No : " + _WardNo + " " + " To " + " " + _WNo + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If
                        If _SwiftCode <> _SCode Then
                            If _SwiftCode = "" Then
                                ' log_message = " Swift Code : " + _SCode + "." + " "
                            Else
                                log_message = " swift Code : " + _SwiftCode + " " + " To " + " " + _SCode + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If
                        If _UniMun <> _Uni Then
                            If _UniMun = "" Then
                                ' log_message = " UNI MUN : " + _Uni + "." + " "
                            Else
                                log_message = "  UNI MUN : " + _UniMun + " " + " To " + " " + _Uni + "." + " "
                            End If
                            BranchList.Add(log_message)
                        End If

                        For Each branchloglist As String In BranchList
                            _BranchLog += branchloglist
                        Next

                        _log = " Authorized : Branch Code : " + txtBranchCode.Text.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _BranchLog

                        Logger.system_log(_log)

                    End If
                Else
                    tStatus = TransState.UpdateNotPossible
                End If



                trans.Commit()

                

                    'log_message = "Authorized Branch Code " + txtBranchCode.Text.Trim() + " Branch Name " + txtBranchName.Text.Trim()
                    'Logger.system_log(log_message)



            End Using

            Return tStatus

        End If

    End Function
    Private Function CheckForDelete() As Boolean

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String = ""

        strSql = "select IS_AUTHORIZED,STATUS from FIU_BANK_BRANCH where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & _intModno.ToString()

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


            strSql = "select IS_AUTHORIZED,STATUS from FIU_BANK_BRANCH where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & _intModno.ToString()

            Dim ds As New DataSet
            ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0)(0) = False Then 'if not authorized

                    strSql = "delete FIU_BANK_BRANCH where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & _intModno.ToString() & " and IS_AUTHORIZED=0"

                    db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    _intModno = _intModno - 1

                    tStatus = TransState.Delete


                ElseIf ds.Tables(0).Rows(0)(0) = True Then 'if authorized

                    If ds.Tables(0).Rows(0)("STATUS") = "L" Then 'if this is the last modified data

                        strSql = "delete FIU_BANK_BRANCH where BRANCH_CODE='" & _strBankBranch_Code & "'  and IS_AUTHORIZED=0"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                        strSql = "select * from FIU_BANK_BRANCH where BRANCH_CODE='" & _strBankBranch_Code & "' and MODNO=" & _intModno.ToString()

                        Dim dsKeeper As New DataSet
                        dsKeeper = db.ExecuteDataSet(trans, CommandType.Text, strSql)


                        strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_BANK_BRANCH where BRANCH_CODE='" & _strBankBranch_Code & "'"


                        intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)

                        strSql = "Insert Into FIU_BANK_BRANCH(BRANCH_CODE, BANK_CODE, BRANCH_NAME, BRANCH_LOC, THANA_CODE, DIST_CODE, PO, UNI_MUN, WARD, BRANCH_NO, INSERTED_ON,  MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS,INSTITUTION_CODE,SWIFT_CODE)" & _
                     "Values(@P_BRANCH_CODE, @P_BANK_CODE, @P_BRANCH_NAME, @P_BRANCH_LOC, @P_THANA_CODE, @P_DIST_CODE, @P_PO, @P_UNI_MUN, @P_WARD, @P_BRANCH_NO, @P_Inserted_On,@P_Modified_On," & intModno.ToString() & ",@P_Input_By,getdate(),0,'D',@INSTITUTION_CODE,@SWIFT_CODE)"


                        Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()

                        db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dsKeeper.Tables(0).Rows(0)("BRANCH_CODE"))
                        db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, dsKeeper.Tables(0).Rows(0)("BANK_CODE"))
                        db.AddInParameter(commProc, "@P_BRANCH_NAME", DbType.String, dsKeeper.Tables(0).Rows(0)("BRANCH_NAME"))
                        db.AddInParameter(commProc, "@P_BRANCH_LOC", DbType.String, dsKeeper.Tables(0).Rows(0)("BRANCH_LOC"))
                        db.AddInParameter(commProc, "@P_THANA_CODE", DbType.String, dsKeeper.Tables(0).Rows(0)("THANA_CODE"))
                        db.AddInParameter(commProc, "@P_DIST_CODE", DbType.String, dsKeeper.Tables(0).Rows(0)("DIST_CODE"))
                        db.AddInParameter(commProc, "@P_PO", DbType.String, dsKeeper.Tables(0).Rows(0)("PO"))
                        db.AddInParameter(commProc, "@P_UNI_MUN", DbType.String, dsKeeper.Tables(0).Rows(0)("UNI_MUN"))
                        db.AddInParameter(commProc, "@P_WARD", DbType.String, dsKeeper.Tables(0).Rows(0)("WARD"))
                        db.AddInParameter(commProc, "@P_BRANCH_NO", DbType.String, dsKeeper.Tables(0).Rows(0)("BRANCH_NO"))
                        db.AddInParameter(commProc, "@INSTITUTION_CODE", DbType.String, dsKeeper.Tables(0).Rows(0)("INSTITUTION_CODE"))
                        db.AddInParameter(commProc, "@SWIFT_CODE", DbType.String, dsKeeper.Tables(0).Rows(0)("SWIFT_CODE"))


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

            log_message = "Delete Branch Code " + txtBranchCode.Text.Trim()
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

    Private Sub FrmBankBranch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If



        CommonUtil.FillComboBox("select BANK_CODE, BANK_NAME  from FIU_BANK where STATUS = 'L'", cmbBankCode)
        CommonUtil.FillComboBox("select DIST_CODE, DIST_NAME  from FIU_DISTRICT where STATUS = 'L'", cmbDistrict)
        CommonUtil.FillComboBox("select THANA_CODE, THANA_NAME  from FIU_THANA where STATUS = 'L'", cmbThana)


        If _intModno > 0 Then  ' Do not understand
            'LoadAppData(_intSlno, _intModno)

            LoadBankBranchData(_strBankBranch_Code, _intModno)
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

        If txtBranchCode.Enabled = True Then
            txtBranchCode.Focus()
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

                        LoadBankBranchData(_strBankBranch_Code, _intModno)

                        lblToolStatus.Text = "!! Information Added Successfully !!"

                        _formMode = FormTransMode.Update


                        EnableDelete()

                        EnableRefresh()

                    ElseIf tState = TransState.Update Then

                        'LoadAppData(_intSlno, _intModno)

                        LoadBankBranchData(_strBankBranch_Code, _intModno)

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
        LoadBankBranchData(_strBankBranch_Code, _intModno)
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

                        LoadBankBranchData(_strBankBranch_Code, _intModno)

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
            LoadBankBranchData(_strBankBranch_Code, _intModno - 1)
        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click
        Dim strBankBranch_Code As String = _strBankBranch_Code
        Dim intModno As Integer = _intModno


        LoadBankBranchData(_strBankBranch_Code, _intModno + 1)

        If _intModno = 0 Then
            'LoadAppData(intSlno, intModno)
            LoadBankBranchData(strBankBranch_Code, intModno)
        End If
    End Sub

   
    Private Sub lblVerTot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblVerTot.Click

    End Sub
End Class