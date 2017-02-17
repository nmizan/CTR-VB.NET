
'
'Author             : Fahad Khan
'Purpose            : Maintain Entity Information
'Creation date      : 21-Dec-2013
'Stored Procedure(s):  


Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmEntityPersonSummary

#Region "user defined codes"

    Dim _formName As String = "MaintenanceGoEntityPersonSummary"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""

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

    Private Sub LoadDataGrid()


        If dgView.Columns.Count = 0 Then Exit Sub

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_GetDetailList")

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
                dgView.Item(1, i).Value = dt.Rows(i).Item("MOD_NO")
                dgView.Item(2, i).Value = dt.Rows(i).Item("S").ToString()
                dgView.Item(3, i).Value = dt.Rows(i).Item("ENTITY_ID")
                dgView.Item(4, i).Value = dt.Rows(i).Item("NAME")
                dgView.Item(5, i).Value = dt.Rows(i).Item("COMMERTIAL_NAME")
                dgView.Item(6, i).Value = dt.Rows(i).Item("INCORPORATION_NUMBER")
                dgView.Item(7, i).Value = dt.Rows(i).Item("BUSINESS")
                dgView.Item(8, i).Value = dt.Rows(i).Item("INCORPORATION_STATE")
                dgView.Item(9, i).Value = dt.Rows(i).Item("INPUT_BY").ToString()
                dgView.Item(10, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INPUT_DATETIME"))
                dgView.Item(11, i).Value = dt.Rows(i).Item("INPUT_DATETIME")
                dgView.Item(12, i).Value = dt.Rows(i).Item("AUTH_BY").ToString()
                dgView.Item(13, i).Value = NullHelper.DateToString(dt.Rows(i).Item("AUTH_DATETIME"))

                dgView.Item(14, i).Value = dt.Rows(i).Item("EMAIL").ToString()
                dgView.Item(15, i).Value = dt.Rows(i).Item("URL").ToString()
                dgView.Item(16, i).Value = dt.Rows(i).Item("INCORPORATION_COUNTRY").ToString()
                dgView.Item(17, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INCORPORATION_DATE"))
                dgView.Item(18, i).Value = dt.Rows(i).Item("TAX_NUMBER").ToString()
                dgView.Item(19, i).Value = dt.Rows(i).Item("TAX_REG_NUMBER").ToString()
                dgView.Item(20, i).Value = dt.Rows(i).Item("COMMENTS").ToString()

                'dgView.Item(21, i).Value = dt.Rows(i).Item("ACNUMBER").ToString()



            Next

            lblTotRecNo.Text = dt.Rows.Count

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub


    Private Sub LoadDataGridAccount()


        If dgView.Columns.Count = 0 Then Exit Sub

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_GetDetailListWithAccount")

            commProc.Parameters.Clear()

            Dim dt As DataTable = db.ExecuteDataSet(commProc).Tables(0)

            Dim i As Integer

            dgView.Rows.Clear()

            For i = 0 To dt.Rows.Count - 1
                dgView.Rows.Add()
                dgView.Item(1, i).Value = dt.Rows(i).Item("MOD_NO")
                dgView.Item(2, i).Value = dt.Rows(i).Item("S").ToString()
                dgView.Item(3, i).Value = dt.Rows(i).Item("ENTITY_ID")
                dgView.Item(4, i).Value = dt.Rows(i).Item("NAME")
                dgView.Item(5, i).Value = dt.Rows(i).Item("COMMERTIAL_NAME")
                dgView.Item(6, i).Value = dt.Rows(i).Item("INCORPORATION_NUMBER")
                dgView.Item(7, i).Value = dt.Rows(i).Item("BUSINESS")
                dgView.Item(8, i).Value = dt.Rows(i).Item("INCORPORATION_STATE")
                dgView.Item(9, i).Value = dt.Rows(i).Item("INPUT_BY").ToString()
                dgView.Item(10, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INPUT_DATETIME"))
                dgView.Item(11, i).Value = dt.Rows(i).Item("INPUT_DATETIME")
                dgView.Item(12, i).Value = dt.Rows(i).Item("AUTH_BY").ToString()
                dgView.Item(13, i).Value = NullHelper.DateToString(dt.Rows(i).Item("AUTH_DATETIME"))

                dgView.Item(14, i).Value = dt.Rows(i).Item("EMAIL").ToString()
                dgView.Item(15, i).Value = dt.Rows(i).Item("URL").ToString()
                dgView.Item(16, i).Value = dt.Rows(i).Item("INCORPORATION_COUNTRY").ToString()
                dgView.Item(17, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INCORPORATION_DATE"))
                dgView.Item(18, i).Value = dt.Rows(i).Item("TAX_NUMBER").ToString()
                dgView.Item(19, i).Value = dt.Rows(i).Item("TAX_REG_NUMBER").ToString()
                dgView.Item(20, i).Value = dt.Rows(i).Item("COMMENTS").ToString()

                dgView.Item(21, i).Value = dt.Rows(i).Item("ACNUMBER").ToString()



            Next

            lblTotRecNo.Text = dt.Rows.Count

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub


#End Region

    Private Sub FrmEntityPersonSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If



        Dim i As Integer
        For i = 1 To dgView.Columns.Count - 1
            dgView.Columns(i).ReadOnly = True
        Next


    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim frmEntityDet As New FrmEntityPersonDet()
        frmEntityDet.ShowDialog()
    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try

            If chkAccount.Checked = False Then
                If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then

                    Dim frmEntityDet As New FrmEntityPersonDet(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
                    frmEntityDet.Show()
                End If
            End If
        Catch ex As Exception


        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click


        If chkAccount.Checked = True Then

            LoadDataGridAccount()

        Else
            LoadDataGrid()
        End If

    End Sub

    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick

        If chkAccount.Checked = False Then

            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then

                Dim frmEntityDet As New FrmEntityPersonDet(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
                frmEntityDet.Show()
            End If

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

    '----------------------Mizan Work (23-04-16)---------------------------

    Private Sub LoadMainDataForAuth(ByVal strId As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From GO_T_ENTITY Where ENTITY_ID ='" & strId & "' and STATUS ='L' ")

            If ds.Tables(0).Rows.Count > 0 Then

                _EntName = ds.Tables(0).Rows(0)("NAME")

                _IncNo = ds.Tables(0).Rows(0)("INCORPORATION_NUMBER").ToString()

                _IncState = ds.Tables(0).Rows(0)("INCORPORATION_STATE").ToString()
                _ComName = ds.Tables(0).Rows(0)("COMMERTIAL_NAME").ToString()
                _EnLegalType = ds.Tables(0).Rows(0)("INCORPORATION_LEGAL_FORM").ToString()

                _EntBussiness = ds.Tables(0).Rows(0)("BUSINESS").ToString()

                _Entemail = ds.Tables(0).Rows(0)("EMAIL").ToString()

                _Enturl = ds.Tables(0).Rows(0)("URL").ToString()


                _IncCountry = ds.Tables(0).Rows(0)("INCORPORATION_COUNTRY").ToString()


                _TNo = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()

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

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '----------------------Mizan Work (23-04-16)---------------------------

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

                
                _EName = ds.Tables(0).Rows(0)("NAME")

                _IncorpNo = ds.Tables(0).Rows(0)("INCORPORATION_NUMBER").ToString()

                _IncorpState = ds.Tables(0).Rows(0)("INCORPORATION_STATE").ToString()
               
                _CName = ds.Tables(0).Rows(0)("COMMERTIAL_NAME").ToString()
                _LegalType = ds.Tables(0).Rows(0)("INCORPORATION_LEGAL_FORM").ToString()

                _Bussiness = ds.Tables(0).Rows(0)("BUSINESS").ToString()

                _Email = ds.Tables(0).Rows(0)("EMAIL").ToString()

                _Url = ds.Tables(0).Rows(0)("URL").ToString()


                _IncorpCountry = ds.Tables(0).Rows(0)("INCORPORATION_COUNTRY").ToString()

                _TaxNo = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()

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

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Try

            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                '----------------------Mizan Work (23-04-16)---------------------------
                Dim i As Integer


                For i = 0 To dgView.Rows.Count - 1


                    If dgView.Rows(i).Cells(0).Value = True Then

                        If dgView.Rows(i).Cells(9).Value.ToString = CommonAppSet.User.Trim() Then

                            MessageBox.Show("Maker can't verify data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else

                            LoadMainData(dgView.Rows(i).Cells(3).Value.ToString(), dgView.Rows(i).Cells(1).Value)


                            If (dgView.Rows(i).Cells(1).Value) > 1 Then

                                LoadMainDataForAuth(dgView.Rows(i).Cells(3).Value.ToString())

                                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_Auth")


                                commProc.Parameters.Clear()
                                db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)
                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "!! Information Authorized Successfully !!"

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

                                    _Entlog = " Authorized : Entity ID : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + _EntityLog

                                    Logger.system_log(_Entlog)
                                    _EntityLog = ""
                                    EntityList.Clear()

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

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_Auth")


                                commProc.Parameters.Clear()
                                db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)
                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "!! Information Authorized Successfully !!"

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

                                    _Entlog = " Authorized : Entity ID : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _EntityLog

                                    Logger.system_log(_Entlog)
                                    _EntityLog = ""
                                    EntityList.Clear()

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


                '----------------------Mizan Work (23-04-16)---------------------------




                ''----------------------Commented By Mizan (23-04-16) -----------------

                'Dim i As Integer

                'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                'Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_Auth")

                'For i = 0 To dgView.Rows.Count - 1

                '    If dgView.Rows(i).Cells(0).Value = True Then

                'commProc.Parameters.Clear()
                'db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                'db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                'db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)
                'db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                'Dim result As Integer

                'db.ExecuteNonQuery(commProc)
                'result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                'If result = 0 Then
                '    lblToolStatus.Text = "!! Information Authorized Successfully !!"
                'ElseIf result = 1 Then

                '    MessageBox.Show("Update not possible", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'ElseIf result = 3 Then
                '    MessageBox.Show("Already authorized", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'ElseIf result = 4 Then
                '    MessageBox.Show("Record not found", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'ElseIf result = 5 Then
                '    MessageBox.Show("You cannot authorize the transaction", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'ElseIf result = 7 Then
                '    MessageBox.Show("Data mismatch! Reload records", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'Else
                '    MessageBox.Show("Auth Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'End If

                '    End If
                '    log_message = "Account Number " + dgView.Rows(i).Cells(3).Value.ToString() + " Authorized"
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

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Not txtEntityName.Text.Trim() = "" Then

            If dgView.Columns.Count = 0 Then Exit Sub

            Try


                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_GetDetailListbySearch")

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

                db.AddInParameter(commProc, "@NAME", DbType.String, txtEntityName.Text)


                Dim dt As DataTable = db.ExecuteDataSet(commProc).Tables(0)

                Dim i As Integer

                dgView.Rows.Clear()

                For i = 0 To dt.Rows.Count - 1
                    dgView.Rows.Add()
                    dgView.Item(1, i).Value = dt.Rows(i).Item("MOD_NO")
                    dgView.Item(2, i).Value = dt.Rows(i).Item("S").ToString()
                    dgView.Item(3, i).Value = dt.Rows(i).Item("ENTITY_ID")
                    dgView.Item(4, i).Value = dt.Rows(i).Item("NAME")
                    dgView.Item(5, i).Value = dt.Rows(i).Item("COMMERTIAL_NAME")
                    dgView.Item(6, i).Value = dt.Rows(i).Item("INCORPORATION_NUMBER")
                    dgView.Item(7, i).Value = dt.Rows(i).Item("BUSINESS")
                    dgView.Item(8, i).Value = dt.Rows(i).Item("INCORPORATION_STATE")
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



        End If
    End Sub

    Private Sub chkAccount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAccount.CheckedChanged

        If chkAccount.Checked = True Then

            rdoAuthorized.Enabled = False
            rdoUnauthorized.Enabled = False
            'btnAuthorize.Enabled = False
            chkShowAll.Enabled = False


            Dim i As Integer
            For i = 0 To dgView.Columns.Count - 1
                dgView.Columns(i).ReadOnly = True
            Next


        Else
            rdoAuthorized.Enabled = True
            rdoUnauthorized.Enabled = True
            'btnAuthorize.Enabled = True
            chkShowAll.Enabled = True


            Dim i As Integer
            For i = 1 To dgView.Columns.Count - 1
                dgView.Columns(i).ReadOnly = True
            Next


        End If


    End Sub
End Class