'
'Author             : Fahad Khan
'Purpose            : Report Summary
'Creation date      : 24-oct-2013
'Stored Procedure(s):  
'

Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmReportSummary

    Dim _formName As String = "MaintenanceReportSummary"
    Dim opt As SecForm = New SecForm(_formName)

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

#Region "user defined codes"

    Private Sub LoadDataGrid()


        If dgView.Columns.Count = 0 Then Exit Sub

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Report_GetDetailList")

            commProc.Parameters.Clear()

            If chkShowAll.Checked = True Then
                db.AddInParameter(commProc, "@DEL_FLAG", DbType.Int32, 1)
            Else
                db.AddInParameter(commProc, "@DEL_FLAG", DbType.Int32, 0)

            End If

            If rdoAuthorized.Checked = True Then
                db.AddInParameter(commProc, "@AUTH_FLAG", DbType.Int32, 1)
            Else
                db.AddInParameter(commProc, "@AUTH_FLAG", DbType.Int32, 0)
            End If

            Dim dt As DataTable = db.ExecuteDataSet(commProc).Tables(0)

            Dim i As Integer

            dgView.Rows.Clear()

            For i = 0 To dt.Rows.Count - 1
                dgView.Rows.Add()
                dgView.Item(1, i).Value = dt.Rows(i).Item("MOD_NO").ToString()
                dgView.Item(2, i).Value = dt.Rows(i).Item("S").ToString()
                dgView.Item(3, i).Value = dt.Rows(i).Item("RENTITY_ID").ToString()
                dgView.Item(4, i).Value = dt.Rows(i).Item("RENTITY_BRANCH").ToString()
                dgView.Item(5, i).Value = dt.Rows(i).Item("SUBMISSION_CODE").ToString()
                dgView.Item(6, i).Value = dt.Rows(i).Item("REPORT_CODE").ToString()
                dgView.Item(7, i).Value = dt.Rows(i).Item("ENTITY_REFERENCE").ToString()
                dgView.Item(8, i).Value = dt.Rows(i).Item("FIU_REF_NUMBER").ToString()
                dgView.Item(9, i).Value = dt.Rows(i).Item("INPUT_BY").ToString()
                dgView.Item(10, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INPUT_DATETIME"))
                dgView.Item(11, i).Value = dt.Rows(i).Item("INPUT_DATETIME")
                dgView.Item(12, i).Value = dt.Rows(i).Item("AUTH_BY").ToString()
                dgView.Item(13, i).Value = NullHelper.DateToString(dt.Rows(i).Item("AUTH_DATETIME"))
            Next

            lblTotRecNo.Text = dt.Rows.Count

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

#End Region

    Private Sub FrmReportSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        For i = 1 To dgView.Columns.Count - 1
            dgView.Columns(i).ReadOnly = True
        Next
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim frmReportDet As New FrmReportDet()
        frmReportDet.ShowDialog()

    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try


            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then

                Dim frmReportDet As New FrmReportDet(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
                frmReportDet.Show()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()
    End Sub
    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick
        If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then

            Dim frmReportDet As New FrmReportDet(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
            frmReportDet.Show()
        End If
    End Sub

    Private Sub dgView_RowPrePaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles dgView.RowPrePaint
        If (e.RowIndex < dgView.Rows.Count - 1) Then
            If dgView.Rows(e.RowIndex).Cells(2).Value.ToString() = "D" Then
                dgView.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Tomato

            ElseIf dgView.Rows(e.RowIndex).Cells(2).Value.ToString() = "U" Then
                dgView.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Pink
            End If
        End If
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged

        Dim rowsCount, i As Integer
        rowsCount = dgView.Rows.Count

        If chkAll.Checked = True Then
            For i = 0 To rowsCount - 1
                dgView(0, i).Value = True
            Next i
        ElseIf chkAll.Checked = False Then
            For i = 0 To rowsCount - 1
                dgView(0, i).Value = False
            Next i
        End If

    End Sub

    Private Sub rdoAuthorized_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAuthorized.CheckedChanged

        If rdoAuthorized.Checked = True Then
            btnAuthorize.Enabled = False
            chkAll.Visible = False
            If dgView.Columns.Count > 0 Then
                dgView.Columns(0).Visible = False
            End If
            chkShowAll.Visible = True



        ElseIf rdoUnauthorized.Checked = True Then
            btnAuthorize.Enabled = True
            chkAll.Visible = True

            If dgView.Columns.Count > 0 Then
                dgView.Columns(0).Visible = True
            End If
            chkShowAll.Visible = False

        End If

        LoadDataGrid()

    End Sub


    '--------------Mizan Work (24-04-16)---------------------

    Private Sub LoadRptTypeDataForAuth(ByVal strRptTypeCode As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From  GO_REPORT Where RENTITY_ID ='" & strRptTypeCode & "' and STATUS ='L' ")

            If ds.Tables(0).Rows.Count > 0 Then

                _rbranch = ds.Tables(0).Rows(0)("RENTITY_BRANCH").ToString()

                _rsubmissionType = ds.Tables(0).Rows(0)("SUBMISSION_CODE").ToString()

                _rreportType = ds.Tables(0).Rows(0)("REPORT_CODE").ToString()

                _rentityRef = ds.Tables(0).Rows(0)("ENTITY_REFERENCE").ToString()

                _rfiuRef = ds.Tables(0).Rows(0)("FIU_REF_NUMBER").ToString()

                _rreason = ds.Tables(0).Rows(0)("REASON").ToString()

                _raction = ds.Tables(0).Rows(0)("ACTION").ToString()

                _rreportIndicator = ds.Tables(0).Rows(0)("REPORT_INDICATOR").ToString()

                _rcurrtype = ds.Tables(0).Rows(0)("CURRENCY_CODE_LOCAL").ToString()

                _rreportPerson = ds.Tables(0).Rows(0)("REPORTING_PERSON").ToString()

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



            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--------------Mizan Work (24-04-16)---------------------

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

                _branch = ds.Tables(0).Rows(0)("RENTITY_BRANCH").ToString()

                _submissionType = ds.Tables(0).Rows(0)("SUBMISSION_CODE").ToString()

                _reportType = ds.Tables(0).Rows(0)("REPORT_CODE").ToString()

                _entityRef = ds.Tables(0).Rows(0)("ENTITY_REFERENCE").ToString()

                _fiuRef = ds.Tables(0).Rows(0)("FIU_REF_NUMBER").ToString()

                _reason = ds.Tables(0).Rows(0)("REASON").ToString()

                _action = ds.Tables(0).Rows(0)("ACTION").ToString()

               

                _reportIndicator = ds.Tables(0).Rows(0)("REPORT_INDICATOR").ToString()

                _currtype = ds.Tables(0).Rows(0)("CURRENCY_CODE_LOCAL").ToString()

                _reportPerson = ds.Tables(0).Rows(0)("REPORTING_PERSON").ToString()

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

                
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Try

            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                '--------------Mizan Work (24-04-16)---------------------

                Dim i As Integer

                For i = 0 To dgView.Rows.Count - 1


                    If dgView.Rows(i).Cells(0).Value = True Then

                        If dgView.Rows(i).Cells(9).Value.ToString = CommonAppSet.User.Trim() Then

                            MessageBox.Show("Maker can't verify data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else

                            LoadRptTypeData(dgView.Rows(i).Cells(3).Value.ToString(), dgView.Rows(i).Cells(1).Value)


                            If (dgView.Rows(i).Cells(1).Value) > 1 Then

                                LoadRptTypeDataForAuth(dgView.Rows(i).Cells(3).Value.ToString())

                                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Report_Auth")



                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@RENTITY_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "!! Information Authorized Successfully !!"

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

                                    _Rlog = " Authorized : Report : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + _reportLog

                                    Logger.system_log(_Rlog)
                                    _reportLog = ""
                                    ReportList.Clear()

                                ElseIf result = 1 Then

                                    MessageBox.Show("Update not possible", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                ElseIf result = 3 Then
                                    MessageBox.Show("Already authorized", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                ElseIf result = 4 Then
                                    MessageBox.Show("Record not found", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                ElseIf result = 5 Then
                                    MessageBox.Show("You cannot authorize the transaction", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                ElseIf result = 7 Then
                                    MessageBox.Show("Data mismatch! Reload records", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                Else
                                    MessageBox.Show("Auth Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If


                            Else

                                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Report_Auth")


                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@RENTITY_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "!! Information Authorized Successfully !!"

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

                                    _Rlog = " Authorized : Report : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _reportLog

                                    Logger.system_log(_Rlog)
                                    _reportLog = ""
                                    ReportList.Clear()

                                ElseIf result = 1 Then

                                    MessageBox.Show("Update not possible", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                ElseIf result = 3 Then
                                    MessageBox.Show("Already authorized", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                ElseIf result = 4 Then
                                    MessageBox.Show("Record not found", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                ElseIf result = 5 Then
                                    MessageBox.Show("You cannot authorize the transaction", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                ElseIf result = 7 Then
                                    MessageBox.Show("Data mismatch! Reload records", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                Else
                                    MessageBox.Show("Auth Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If

                            End If
                        End If
                    End If

                Next i





                '--------------Mizan Work (24-04-16)---------------------


                '--------------Commented By Mizan (24-04-16)---------------


                'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                'Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Report_Auth")

                'For i = 0 To dgView.Rows.Count - 1

                '    If dgView.Rows(i).Cells(0).Value = True Then

                '        commProc.Parameters.Clear()

                '        db.AddInParameter(commProc, "@RENTITY_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                '        db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                '        db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                '        db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                '        Dim result As Integer

                '        db.ExecuteNonQuery(commProc)
                '        result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                '        If result = 0 Then

                '        ElseIf result = 1 Then

                '            MessageBox.Show("Update not possible", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                '        ElseIf result = 3 Then
                '            MessageBox.Show("Already authorized", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                '        ElseIf result = 4 Then
                '            MessageBox.Show("Record not found", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                '        ElseIf result = 5 Then
                '            MessageBox.Show("You cannot authorize the transaction", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        ElseIf result = 7 Then
                '            MessageBox.Show("Data mismatch! Reload records", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                '        Else
                '            MessageBox.Show("Auth Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        End If

                '    End If

                '    log_message = "Authorized Report " + dgView.Rows(i).Cells(3).Value.ToString() + " Branch " + dgView.Rows(i).Cells(4).Value.ToString()
                '    Logger.system_log(log_message)
                'Next i

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        LoadDataGrid()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objExp As New ExportUtil(dgView)

        objExp.ExportXl()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class