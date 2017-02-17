'
'Author             : Fahad Khan
'Purpose            : Maintain Reporting Information
'Creation date      : 24-oct-2013
'Stored Procedure(s):  

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization


Public Class FrmReportDet

#Region "Global Variables"

    Dim _formName As String = "MaintenanceReportDet"
    Dim opt As SecForm = New SecForm(_formName)

    Dim _formMode As FormTransMode
    Dim _intSlno As Integer = 0
    Dim _intModno As Integer = 0
    Dim _strRptType_Code As String = ""
    Dim _mod_datetime As Date
    Dim _status As String = ""
    Dim log_message As String = ""

    'For Update
    Dim _branch As String = ""
    Dim _submissionType As String = ""
    Dim _reportType As String = ""
    Dim _entityRef As String = ""
    Dim _fiuRef As String = ""
    Dim _currtype As String = ""
    Dim _reportPerson As String = ""
    Dim _location As String = ""
    Dim _action As String = ""
    Dim _reason As String = ""
    Dim _reportIndicator As String = ""
    Dim _submissionTypeName As String = ""
    Dim _reportTypeName As String = ""
    Dim _currtypeName As String = ""
    Dim _reportPersonName As String = ""
    Dim _locationName As String = ""

    'For Auth
    Dim _rbranch As String = ""
    Dim _rsubmissionType As String = ""
    Dim _rreportType As String = ""
    Dim _rentityRef As String = ""
    Dim _rfiuRef As String = ""
    Dim _rcurrtype As String = ""
    Dim _rreportPerson As String = ""
    Dim _rlocation As String = ""
    Dim _raction As String = ""
    Dim _rreason As String = ""
    Dim _rreportIndicator As String = ""
    Dim _rsubmissionTypeName As String = ""
    Dim _rreportTypeName As String = ""
    Dim _rcurrtypeName As String = ""
    Dim _rreportPersonName As String = ""
    Dim _rlocationName As String = ""

    Dim ReportList As New List(Of String)
    Dim _reportLog As String = ""
    Dim _Rlog As String = ""

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
        txtBranch.ReadOnly = True
        txtEntityRef.ReadOnly = True
        txtFiuRef.ReadOnly = True
        txtReason.ReadOnly = True
        txtIndicator.ReadOnly = True
        txtAction.ReadOnly = True
        chkActive.Enabled = False
        cmbLocation.Enabled = False
        cmbReportPerson.Enabled = False
        cmbCurrency.Enabled = False
        cmbSubmission.Enabled = False
        cmbReport.Enabled = False

    End Sub

    Private Sub EnableFields()
        If txtId.Text.Trim() = "" Then
            txtId.ReadOnly = False
        End If

        txtBranch.ReadOnly = False
        txtEntityRef.ReadOnly = False
        txtFiuRef.ReadOnly = False
        txtReason.ReadOnly = False
        txtIndicator.ReadOnly = False
        txtAction.ReadOnly = False
        chkActive.Enabled = True
        cmbLocation.Enabled = True
        cmbReportPerson.Enabled = True
        cmbCurrency.Enabled = True
        cmbSubmission.Enabled = True
        cmbReport.Enabled = True


    End Sub


    Private Sub ClearFields()
        If txtId.ReadOnly = False Then
            txtId.Clear()
        End If

        txtBranch.ReadOnly = False
        txtEntityRef.ReadOnly = False
        txtFiuRef.ReadOnly = False
        txtReason.ReadOnly = False
        txtIndicator.ReadOnly = False
        txtAction.ReadOnly = False
        chkActive.Checked = False
        cmbLocation.SelectedValue = -1
        cmbReportPerson.SelectedValue = -1

        cmbSubmission.SelectedValue = "E"
        cmbReport.SelectedValue = "CTR"
        cmbCurrency.SelectedValue = "BDT"

    End Sub

    Private Sub ClearFieldsAll()
        txtId.Clear()
        txtBranch.Clear()
        txtEntityRef.Clear()
        txtFiuRef.Clear()
        txtReason.Clear()
        txtIndicator.Clear()
        txtAction.Clear()
        chkActive.Checked = False
        cmbLocation.SelectedValue = -1
        cmbReportPerson.SelectedValue = -1

        cmbSubmission.SelectedValue = "E"
        cmbReport.SelectedValue = "CTR"
        cmbCurrency.SelectedValue = "BDT"

        _strRptType_Code = ""
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
        ElseIf txtBranch.Text.Trim() = "" Then
            MessageBox.Show("Branch required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtBranch.Focus()
            Return False
        ElseIf cmbSubmission.Text.Trim() = "" Then
            MessageBox.Show("Submission Type required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbSubmission.Focus()
            Return False
        ElseIf cmbReport.Text.Trim() = "" Then
            MessageBox.Show("Report Code required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbReport.Focus()
            Return False
        ElseIf txtEntityRef.Text.Trim() = "" Then
            MessageBox.Show("Entity Reference required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEntityRef.Focus()
            Return False
        ElseIf cmbCurrency.Text.Trim() = "" Then
            MessageBox.Show("Currency Code required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbCurrency.Focus()
            Return False


        ElseIf cmbLocation.Text.Trim() = "" Then
            MessageBox.Show("Location Code required!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbLocation.Focus()
            Return False

        End If


        Return True

    End Function

    Private Function SaveData() As TransState

        Dim tStatus As TransState


        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        If _formMode = FormTransMode.Add Then

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Report_Add")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@RENTITY_ID", DbType.String, txtId.Text.Trim())
            db.AddInParameter(commProc, "@RENTITY_BRANCH", DbType.String, txtBranch.Text)
            db.AddInParameter(commProc, "@SUBMISSION_CODE", DbType.String, cmbSubmission.SelectedValue)
            db.AddInParameter(commProc, "@REPORT_CODE", DbType.String, cmbReport.SelectedValue)
            db.AddInParameter(commProc, "@ENTITY_REFERENCE", DbType.String, txtEntityRef.Text.Trim())
            db.AddInParameter(commProc, "@FIU_REF_NUMBER", DbType.String, txtFiuRef.Text.Trim())
            db.AddInParameter(commProc, "@CURRENCY_CODE_LOCAL", DbType.String, cmbCurrency.SelectedValue)
            db.AddInParameter(commProc, "@REPORTING_PERSON", DbType.String, cmbReportPerson.SelectedValue)
            db.AddInParameter(commProc, "@LOCATION", DbType.String, cmbLocation.SelectedValue)

            Dim NA As String = "n/a"

            If txtReason.Text = "" Then
                db.AddInParameter(commProc, "@REASON", DbType.String, NA)

            Else
                db.AddInParameter(commProc, "@REASON", DbType.String, txtReason.Text.Trim())
            End If

            If txtAction.Text = "" Then
                db.AddInParameter(commProc, "@ACTION", DbType.String, NA)

            Else
                db.AddInParameter(commProc, "@ACTION", DbType.String, txtAction.Text.Trim())
            End If




            db.AddInParameter(commProc, "@REPORT_INDICATOR", DbType.String, txtIndicator.Text.Trim())

            If chkActive.Checked Then
                db.AddInParameter(commProc, "@ACTIVE", DbType.Int32, 1)
            Else
                db.AddInParameter(commProc, "@ACTIVE", DbType.Int32, 0)
            End If


            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer


            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then
                tStatus = TransState.Add

                _strRptType_Code = txtId.Text.Trim()

                _intModno = 1

                log_message = " Added : Report : " + txtId.Text.Trim() + "." + " " + " Branch : " + txtBranch.Text.ToString()
                Logger.system_log(log_message)

            Else
                tStatus = TransState.Exist
            End If

            

        ElseIf _formMode = FormTransMode.Update Then



            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Report_Update")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@RENTITY_ID", DbType.String, txtId.Text.Trim())
            db.AddInParameter(commProc, "@RENTITY_BRANCH", DbType.String, txtBranch.Text)
            db.AddInParameter(commProc, "@SUBMISSION_CODE", DbType.String, cmbSubmission.SelectedValue)
            db.AddInParameter(commProc, "@REPORT_CODE", DbType.String, cmbReport.SelectedValue)
            db.AddInParameter(commProc, "@ENTITY_REFERENCE", DbType.String, txtEntityRef.Text.Trim())
            db.AddInParameter(commProc, "@FIU_REF_NUMBER", DbType.String, txtFiuRef.Text.Trim())
            db.AddInParameter(commProc, "@CURRENCY_CODE_LOCAL", DbType.String, cmbCurrency.SelectedValue)
            db.AddInParameter(commProc, "@REPORTING_PERSON", DbType.String, cmbReportPerson.SelectedValue)
            db.AddInParameter(commProc, "@LOCATION", DbType.String, cmbLocation.SelectedValue)

            Dim NA As String = "n/a"

            If txtReason.Text = "" Then
                db.AddInParameter(commProc, "@REASON", DbType.String, NA)

            Else
                db.AddInParameter(commProc, "@REASON", DbType.String, txtReason.Text.Trim())
            End If

            If txtAction.Text = "" Then
                db.AddInParameter(commProc, "@ACTION", DbType.String, NA)

            Else
                db.AddInParameter(commProc, "@ACTION", DbType.String, txtAction.Text.Trim())
            End If




            db.AddInParameter(commProc, "@REPORT_INDICATOR", DbType.String, txtIndicator.Text.Trim())

            If chkActive.Checked Then
                db.AddInParameter(commProc, "@ACTIVE", DbType.Int32, 1)
            Else
                db.AddInParameter(commProc, "@ACTIVE", DbType.Int32, 0)
            End If

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

                If _branch <> txtBranch.Text.Trim() Then
                    log_message = " Branch : " + _branch + " " + " To " + " " + txtBranch.Text.ToString() + "." + " "
                    ReportList.Add(log_message)
                End If
                If _submissionType <> cmbSubmission.Text Then
                    log_message = " Submission Type : " + _submissionType + " " + " To " + " " + cmbSubmission.Text + "." + " "
                    ReportList.Add(log_message)
                End If
                If _reportType <> cmbReport.Text Then
                    log_message = " Report Type : " + _reportType + " " + " To " + " " + cmbReport.Text + "." + " "
                    ReportList.Add(log_message)
                End If
                If _entityRef <> txtEntityRef.Text.Trim() Then
                    log_message = " Entity Reference : " + _entityRef + " " + " To " + " " + txtEntityRef.Text.ToString() + "." + " "
                    ReportList.Add(log_message)
                End If
                If _fiuRef <> txtFiuRef.Text.Trim() Then
                    If _fiuRef = "" Then
                        log_message = " FIU Reference : " + txtFiuRef.Text.ToString() + "." + " "

                    Else
                        log_message = " FIU Reference : " + _fiuRef + " " + " To " + " " + txtFiuRef.Text.ToString() + "." + " "

                    End If
                    ReportList.Add(log_message)
                End If
                If _currtype <> cmbCurrency.Text Then
                    log_message = " Currency Type : " + _currtype + " " + " To " + " " + cmbCurrency.Text + "." + " "
                    ReportList.Add(log_message)
                End If
                If _reportPerson <> cmbReportPerson.Text Then

                    log_message = " Report person : " + _reportPerson + " " + " To " + " " + cmbReportPerson.Text + "." + " "
                    ReportList.Add(log_message)
                End If

                If _location <> cmbLocation.Text Then
                    log_message = " Location : " + _location + " " + " To " + " " + cmbLocation.Text + "." + " "
                    ReportList.Add(log_message)
                End If

                If _action <> txtAction.Text.Trim() Then
                    If _action = "" Then
                        log_message = " Action : " + txtAction.Text.ToString() + "." + " "

                    Else
                        log_message = " Action : " + _action + " " + " To " + " " + txtAction.Text.ToString() + "." + " "

                    End If
                    ReportList.Add(log_message)
                End If
                If _reason <> txtReason.Text.Trim() Then
                    If _reason = "" Then
                        log_message = " Reason : " + txtReason.Text.ToString() + "." + " "

                    Else
                        log_message = " Reason : " + _reason + " " + " To " + " " + txtReason.Text.ToString() + "." + " "

                    End If
                    ReportList.Add(log_message)
                End If
                If _reportIndicator <> txtIndicator.Text.Trim() Then
                    If _reportIndicator = "" Then
                        log_message = " Report Indicator : " + txtIndicator.Text.ToString() + "." + " "

                    Else
                        log_message = " Report Indicator : " + _reportIndicator + " " + " To " + " " + txtIndicator.Text.ToString() + "." + " "

                    End If
                    ReportList.Add(log_message)
                End If

                For Each reptlist As String In ReportList
                    _reportLog += reptlist
                Next

                _Rlog = " Updated : Report : " + txtId.Text.ToString() + "." + " " + _reportLog

                Logger.system_log(_Rlog)
                _reportLog = ""
                ReportList.Clear()

                '--------------Mizan Work (20-04-16)---------------------

            ElseIf result = 1 Then
                tStatus = TransState.UnspecifiedError
            ElseIf result = 4 Then
                tStatus = TransState.NoRecord

            End If

           

            'log_message = "Updated Report " + txtId.Text.Trim() + " Branch " + txtBranch.Text.ToString()
            'Logger.system_log(log_message)

        End If

        Return tStatus

    End Function

    '--------------Mizan Work (20-04-16)---------------------

    Private Function AuthorizeData() As TransState

        If _intModno > 1 Then

            LoadRptTypeDataForAuth(_strRptType_Code)

            Dim tStatus As TransState

            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Report_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@RENTITY_ID", DbType.String, _strRptType_Code)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                '--------------Mizan Work (20-04-16)---------------------

                If _rbranch <> _branch Then
                    If _rbranch = "" Then
                        log_message = " Branch : " + _branch + "." + " "
                    Else
                        log_message = " Branch : " + _rbranch + " " + " To " + " " + _branch + "." + " "
                    End If

                    ReportList.Add(log_message)
                End If
                If _rsubmissionType <> _submissionType Then
                    log_message = " Submission Type : " + _rsubmissionType + " " + " To " + " " + _submissionType + "." + " "
                    ReportList.Add(log_message)
                End If
                If _rreportType <> _reportType Then
                    log_message = " Report Type : " + _rreportType + " " + " To " + " " + _reportType + "." + " "
                    ReportList.Add(log_message)
                End If
                If _rentityRef <> _entityRef Then
                    If _rentityRef = "" Then
                        log_message = " Entity Reference : " + _entityRef + "." + " "
                    Else
                        log_message = " Entity Reference : " + _rentityRef + " " + " To " + " " + _entityRef + "." + " "
                    End If

                    ReportList.Add(log_message)
                End If
                If _rfiuRef <> _fiuRef Then
                    If _rfiuRef = "" Then
                        log_message = " FIU Reference : " + _fiuRef + "." + " "

                    Else
                        log_message = " FIU Reference : " + _rfiuRef + " " + " To " + " " + _fiuRef + "." + " "

                    End If
                    ReportList.Add(log_message)
                End If
                If _rcurrtype <> _currtype Then
                    log_message = " Currency Type : " + _rcurrtype + " " + " To " + " " + _currtype + "." + " "
                    ReportList.Add(log_message)
                End If
                If _rreportPerson <> _reportPerson Then

                    log_message = " Report person : " + _rreportPerson + " " + " To " + " " + _reportPerson + "." + " "
                    ReportList.Add(log_message)
                End If

                If _rlocation <> _location Then
                    log_message = " Location : " + _rlocation + " " + " To " + " " + _location + "." + " "
                    ReportList.Add(log_message)
                End If

                If _raction <> _action Then
                    If _raction = "" Then
                        log_message = " Action : " + _action + "." + " "

                    Else
                        log_message = " Action : " + _raction + " " + " To " + " " + _action + "." + " "

                    End If
                    ReportList.Add(log_message)
                End If
                If _rreason <> _reason Then
                    If _rreason = "" Then
                        log_message = " Reason : " + _reason + "." + " "

                    Else
                        log_message = " Reason : " + _rreason + " " + " To " + " " + _reason + "." + " "

                    End If
                    ReportList.Add(log_message)
                End If
                If _rreportIndicator <> _reportIndicator Then
                    If _rreportIndicator = "" Then
                        log_message = " Report Indicator : " + _reportIndicator + "." + " "

                    Else
                        log_message = " Report Indicator : " + _rreportIndicator + " " + " To " + " " + _reportIndicator + "." + " "

                    End If
                    ReportList.Add(log_message)
                End If

                For Each reptlist As String In ReportList
                    _reportLog += reptlist
                Next

                _Rlog = " Authorized : Report : " + txtId.Text.ToString() + "." + " " + _reportLog

                Logger.system_log(_Rlog)
                _reportLog = ""
                ReportList.Clear()

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

            
            'log_message = "Authorized Report " + _strRptType_Code + " Branch " + txtBranch.Text.ToString()
            'Logger.system_log(log_message)

            Return tStatus

        Else

            Dim tStatus As TransState


            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Report_Auth")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@RENTITY_ID", DbType.String, _strRptType_Code)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, _intModno)
            db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, _mod_datetime)

            db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            Dim result As Integer

            db.ExecuteNonQuery(commProc)
            result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
            If result = 0 Then

                tStatus = TransState.Update

                '--------------Mizan Work (20-04-16)---------------------

                If _rbranch <> _branch Then
                    If _rbranch = "" Then
                        log_message = " Branch : " + _branch + "." + " "
                    Else
                        log_message = " Branch : " + _rbranch + " " + " To " + " " + _branch + "." + " "
                    End If

                    ReportList.Add(log_message)
                End If
                If _rsubmissionType <> _submissionType Then
                    If _rsubmissionType = "" Then
                        log_message = " Submission Type : " + _submissionType + "." + " "
                    Else
                        log_message = " Submission Type : " + _rsubmissionType + " " + " To " + " " + _submissionType + "." + " "
                    End If

                    ReportList.Add(log_message)
                End If
                If _rreportType <> _reportType Then
                    If _rreportType = "" Then
                    Else
                        log_message = " Report Type : " + _rreportType + " " + " To " + " " + _reportType + "." + " "
                        ReportList.Add(log_message)
                    End If


                End If
                If _rentityRef <> _entityRef Then
                    If _rentityRef = "" Then
                        'log_message = " Entity Reference : " + _entityRef + "." + " "
                    Else
                        log_message = " Entity Reference : " + _rentityRef + " " + " To " + " " + _entityRef + "." + " "
                        ReportList.Add(log_message)
                    End If


                End If
                If _rfiuRef <> _fiuRef Then
                    If _rfiuRef = "" Then
                        'log_message = " FIU Reference : " + _fiuRef + "." + " "

                    Else
                        log_message = " FIU Reference : " + _rfiuRef + " " + " To " + " " + _fiuRef + "." + " "
                        ReportList.Add(log_message)
                    End If

                End If
                If _rcurrtype <> _currtype Then
                    If _rcurrtype = "" Then
                    Else
                        log_message = " Currency Type : " + _rcurrtype + " " + " To " + " " + _currtype + "." + " "
                        ReportList.Add(log_message)
                    End If


                End If
                If _rreportPerson <> _reportPerson Then
                    If _rreportPerson = "" Then
                    Else
                        log_message = " Report person : " + _rreportPerson + " " + " To " + " " + _reportPerson + "." + " "
                        ReportList.Add(log_message)
                    End If


                End If

                If _rlocation <> _location Then
                    If _rlocation = "" Then
                    Else
                        log_message = " Location : " + _rlocation + " " + " To " + " " + _location + "." + " "
                        ReportList.Add(log_message)
                    End If


                End If

                If _raction <> _action Then
                    If _raction = "" Then
                        'log_message = " Action : " + _action + "." + " "

                    Else
                        log_message = " Action : " + _raction + " " + " To " + " " + _action + "." + " "
                        ReportList.Add(log_message)
                    End If

                End If
                If _rreason <> _reason Then
                    If _rreason = "" Then
                        'log_message = " Reason : " + _reason + "." + " "

                    Else
                        log_message = " Reason : " + _rreason + " " + " To " + " " + _reason + "." + " "
                        ReportList.Add(log_message)
                    End If

                End If
                If _rreportIndicator <> _reportIndicator Then
                    If _rreportIndicator = "" Then
                        'log_message = " Report Indicator : " + _reportIndicator + "." + " "

                    Else
                        log_message = " Report Indicator : " + _rreportIndicator + " " + " To " + " " + _reportIndicator + "." + " "
                        ReportList.Add(log_message)
                    End If

                End If

                For Each reptlist As String In ReportList
                    _reportLog += reptlist
                Next

                _Rlog = " Authorized : Report : " + txtId.Text.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _reportLog

                Logger.system_log(_Rlog)
                _reportLog = ""
                ReportList.Clear()

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

           

            'log_message = "Authorized Report " + _strRptType_Code + " Branch " + txtBranch.Text.ToString()
            'Logger.system_log(log_message)

            Return tStatus

        End If

    End Function

    '--------------Mizan Work (20-04-16)---------------------

    Private Sub LoadRptTypeDataForAuth(ByVal strRptTypeCode As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From  GO_REPORT Where RENTITY_ID ='" & strRptTypeCode & "' and STATUS ='L' ")
            If ds.Tables(0).Rows.Count > 0 Then


                _strRptType_Code = strRptTypeCode


                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("RENTITY_ID").ToString()
                txtBranch.Text = ds.Tables(0).Rows(0)("RENTITY_BRANCH").ToString()
                _rbranch = ds.Tables(0).Rows(0)("RENTITY_BRANCH").ToString()
                cmbSubmission.SelectedValue = ds.Tables(0).Rows(0)("SUBMISSION_CODE")
                _rsubmissionType = ds.Tables(0).Rows(0)("SUBMISSION_CODE").ToString()
                cmbReport.SelectedValue = ds.Tables(0).Rows(0)("REPORT_CODE")
                _rreportType = ds.Tables(0).Rows(0)("REPORT_CODE").ToString()
                txtEntityRef.Text = ds.Tables(0).Rows(0)("ENTITY_REFERENCE").ToString()
                _rentityRef = ds.Tables(0).Rows(0)("ENTITY_REFERENCE").ToString()
                txtFiuRef.Text = ds.Tables(0).Rows(0)("FIU_REF_NUMBER").ToString()
                _rfiuRef = ds.Tables(0).Rows(0)("FIU_REF_NUMBER").ToString()
                txtReason.Text = ds.Tables(0).Rows(0)("REASON").ToString()
                _rreason = ds.Tables(0).Rows(0)("REASON").ToString()
                txtAction.Text = ds.Tables(0).Rows(0)("ACTION").ToString()
                _raction = ds.Tables(0).Rows(0)("ACTION").ToString()



                txtIndicator.Text = ds.Tables(0).Rows(0)("REPORT_INDICATOR").ToString()
                _rreportIndicator = ds.Tables(0).Rows(0)("REPORT_INDICATOR").ToString()
                'cmbCurrency.SelectedValue = ds.Tables(0).Rows(0)("CURRENCY_CODE_LOCAL")
                _rcurrtype = ds.Tables(0).Rows(0)("CURRENCY_CODE_LOCAL").ToString()
                'cmbReportPerson.SelectedValue = ds.Tables(0).Rows(0)("REPORTING_PERSON")
                _rreportPerson = ds.Tables(0).Rows(0)("REPORTING_PERSON").ToString()
                'cmbLocation.SelectedValue = ds.Tables(0).Rows(0)("LOCATION")
                _rlocation = ds.Tables(0).Rows(0)("LOCATION").ToString()

                '--------------Mizan Work (26-04-2016-----------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_REPORTING_TYPES Where RPTYPECODE ='" & _rreportType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _rreportTypeName = ds2.Tables(0).Rows(0)("RPDEFINITION").ToString()
                    _rreportType = _rreportTypeName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_T_ADDRESS Where ADDRESS_ID = '" & _rlocation & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _rlocationName = ds3.Tables(0).Rows(0)("ADDRESS").ToString()
                    _rlocation = _rlocationName

                End If

                Dim ds4 As New DataSet
                ds4 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_CURRENCY_TYPE Where CURRENCY_CODE ='" & _rcurrtype & "' ")
                If ds4.Tables(0).Rows.Count > 0 Then

                    _rcurrtypeName = ds4.Tables(0).Rows(0)("CURRENCY_NAME").ToString()
                    _rcurrtype = _rcurrtypeName

                End If
                Dim ds5 As New DataSet
                ds5 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_SUBMISSIONTYPE Where SUBTYPE_CODE ='" & _rsubmissionType & "' ")
                If ds5.Tables(0).Rows.Count > 0 Then

                    _rsubmissionTypeName = ds5.Tables(0).Rows(0)("SUBTYPE_NAME").ToString()
                    _rsubmissionType = _rsubmissionTypeName

                End If
                Dim ds6 As New DataSet
                ds6 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_REPORT_PERSON Where PERSON_ID ='" & _rreportPerson & "' ")
                If ds6.Tables(0).Rows.Count > 0 Then

                    _rreportPersonName = ds6.Tables(0).Rows(0)("FIRST_NAME").ToString() + ds6.Tables(0).Rows(0)("LAST_NAME").ToString()
                    _rreportPerson = _rreportPersonName

                End If

                '--------------Mizan Work (26-04-2016-----------

            Else

                ClearFieldsAll()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub LoadRptTypeData(ByVal strRptTypeCode As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Report_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@RENTITY_ID", DbType.String, strRptTypeCode)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intMod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then


                _strRptType_Code = strRptTypeCode
                _intModno = intMod

                _formMode = FormTransMode.Update


                txtId.Text = ds.Tables(0).Rows(0)("RENTITY_ID").ToString()
                txtBranch.Text = ds.Tables(0).Rows(0)("RENTITY_BRANCH").ToString()
                _branch = ds.Tables(0).Rows(0)("RENTITY_BRANCH").ToString()
                cmbSubmission.SelectedValue = ds.Tables(0).Rows(0)("SUBMISSION_CODE")
                _submissionType = ds.Tables(0).Rows(0)("SUBMISSION_CODE").ToString()
                cmbReport.SelectedValue = ds.Tables(0).Rows(0)("REPORT_CODE")
                _reportType = ds.Tables(0).Rows(0)("REPORT_CODE").ToString()
                txtEntityRef.Text = ds.Tables(0).Rows(0)("ENTITY_REFERENCE").ToString()
                _entityRef = ds.Tables(0).Rows(0)("ENTITY_REFERENCE").ToString()
                txtFiuRef.Text = ds.Tables(0).Rows(0)("FIU_REF_NUMBER").ToString()
                _fiuRef = ds.Tables(0).Rows(0)("FIU_REF_NUMBER").ToString()
                txtReason.Text = ds.Tables(0).Rows(0)("REASON").ToString()
                _reason = ds.Tables(0).Rows(0)("REASON").ToString()
                txtAction.Text = ds.Tables(0).Rows(0)("ACTION").ToString()
                _action = ds.Tables(0).Rows(0)("ACTION").ToString()

                If ds.Tables(0).Rows(0)("ACTIVE") = 1 Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False

                End If

                txtIndicator.Text = ds.Tables(0).Rows(0)("REPORT_INDICATOR").ToString()
                _reportIndicator = ds.Tables(0).Rows(0)("REPORT_INDICATOR").ToString()
                cmbCurrency.SelectedValue = ds.Tables(0).Rows(0)("CURRENCY_CODE_LOCAL")
                _currtype = ds.Tables(0).Rows(0)("CURRENCY_CODE_LOCAL").ToString()
                cmbReportPerson.SelectedValue = ds.Tables(0).Rows(0)("REPORTING_PERSON")
                _reportPerson = ds.Tables(0).Rows(0)("REPORTING_PERSON").ToString()
                cmbLocation.SelectedValue = ds.Tables(0).Rows(0)("LOCATION")
                _location = ds.Tables(0).Rows(0)("LOCATION").ToString()

                '--------------Mizan Work (26-04-2016-----------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_REPORTING_TYPES Where RPTYPECODE ='" & _reportType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _reportTypeName = ds2.Tables(0).Rows(0)("RPDEFINITION").ToString()
                    _reportType = _reportTypeName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_T_ADDRESS Where ADDRESS_ID = '" & _location & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _locationName = ds3.Tables(0).Rows(0)("ADDRESS").ToString()
                    _location = _locationName

                End If

                Dim ds4 As New DataSet
                ds4 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_CURRENCY_TYPE Where CURRENCY_CODE ='" & _currtype & "' ")
                If ds4.Tables(0).Rows.Count > 0 Then

                    _currtypeName = ds4.Tables(0).Rows(0)("CURRENCY_NAME").ToString()
                    _currtype = _currtypeName

                End If
                Dim ds5 As New DataSet
                ds5 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_SUBMISSIONTYPE Where SUBTYPE_CODE ='" & _submissionType & "' ")
                If ds5.Tables(0).Rows.Count > 0 Then

                    _submissionTypeName = ds5.Tables(0).Rows(0)("SUBTYPE_NAME").ToString()
                    _submissionType = _submissionTypeName

                End If
                Dim ds6 As New DataSet
                ds6 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_REPORT_PERSON Where PERSON_ID ='" & _reportPerson & "' ")
                If ds6.Tables(0).Rows.Count > 0 Then

                    _reportPersonName = ds6.Tables(0).Rows(0)("FIRST_NAME").ToString() + ds6.Tables(0).Rows(0)("LAST_NAME").ToString()
                    _reportPerson = _reportPersonName

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

                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_Report_GetMaxMod")

                commProc2.Parameters.Clear()

                db.AddInParameter(commProc2, "@RENTITY_ID", DbType.String, strRptTypeCode)

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

        Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Report_Remove")

        commProc.Parameters.Clear()

        db.AddInParameter(commProc, "@RENTITY_ID", DbType.String, _strRptType_Code)
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

        log_message = "Deleted Report " + _strRptType_Code + " Branch " + txtBranch.Text.ToString()
        Logger.system_log(log_message)
        Return tStatus

    End Function


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal strRptTypeCode As String, ByVal intMod As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadRptTypeData(strRptTypeCode, intMod)
        _strRptType_Code = strRptTypeCode
        _intModno = intMod


    End Sub


#End Region
    Private Sub FrmReportDet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        CommonUtil.FillComboBox("GO_SubmissionType_GetList", cmbSubmission)
        CommonUtil.FillComboBox("GO_ReportingType_GetList", cmbReport)

        CommonUtil.FillComboBox("GO_CurrencyType_GetList", cmbCurrency)
        CommonUtil.FillComboBox("GO_ReportingPerson_GetList", cmbReportPerson)

        CommonUtil.FillComboBox("GO_AddressType_GetList", cmbLocation)

        'cmbSubmission.SelectedValue = "E"
        'cmbReport.SelectedValue = "CTR"
        'cmbCurrency.SelectedValue = "BDT"

        If _intModno > 0 Then
            LoadRptTypeData(_strRptType_Code, _intModno)
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

        If Not (_strRptType_Code.Trim() = "") Then

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

                        LoadRptTypeData(_strRptType_Code, _intModno)

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

                        LoadRptTypeData(_strRptType_Code, _intModno)

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
        LoadRptTypeData(_strRptType_Code, _intModno)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try


            If MessageBox.Show("Do you really want to delete?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                tState = DeleteData()

                If tState = TransState.Delete Then


                    _formMode = FormTransMode.Add

                    LoadRptTypeData(_strRptType_Code, _intModno)

                    DisableAuth()

                    If _strRptType_Code = "" Then

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
            LoadRptTypeData(_strRptType_Code, _intModno - 1)
        End If
    End Sub

    Private Sub btnNextVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextVer.Click
        Dim strRptTypeCode As String = _strRptType_Code
        Dim intModno As Integer = _intModno
        If intModno > 0 Then
            LoadRptTypeData(_strRptType_Code, _intModno + 1)

            If _intModno = 0 Then
                LoadRptTypeData(strRptTypeCode, intModno)
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
                    LoadRptTypeData(_strRptType_Code, _intModno)
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