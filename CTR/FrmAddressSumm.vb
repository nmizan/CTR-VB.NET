'
'Author             : Fahad Khan
'Purpose            : Address Summary
'Creation date      : 10-10-2013
'Stored Procedure(s):  
'

Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmAddressSumm

#Region "user defined codes"

    Dim _formName As String = "MaintenanceAddressSumm"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""

    'For Update
    Dim _address As String = ""
    Dim _addressType As String = ""
    Dim _town As String = ""
    Dim _city As String = ""
    Dim _zipCode As String = ""
    Dim _country As String = ""
    Dim _state As String = ""
    Dim _comments As String = ""
    Dim _addressTypeName As String = ""
    Dim _countryName As String = ""

    'For Auth
    Dim _aaddress As String = ""
    Dim _aaddressType As String = ""
    Dim _atown As String = ""
    Dim _acity As String = ""
    Dim _azipCode As String = ""
    Dim _acountry As String = ""
    Dim _astate As String = ""
    Dim _acomments As String = ""
    Dim _aaddressTypeName As String = ""
    Dim _acountryName As String = ""

    Dim AddressList As New List(Of String)
    Dim _addressLog As String = ""
    Dim _Alog As String = ""

    Private Sub LoadDataGrid()


        If dgView.Columns.Count = 0 Then Exit Sub

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Address_GetDetailList")

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
                dgView.Item(3, i).Value = dt.Rows(i).Item("ADDRESS_ID").ToString()
                dgView.Item(4, i).Value = dt.Rows(i).Item("ADDRESS").ToString()
                dgView.Item(5, i).Value = dt.Rows(i).Item("CTYPE_NAME").ToString()
                dgView.Item(6, i).Value = dt.Rows(i).Item("TOWN").ToString()
                dgView.Item(7, i).Value = dt.Rows(i).Item("CITY").ToString()
                dgView.Item(8, i).Value = dt.Rows(i).Item("COUNTRY_NAME").ToString()
                dgView.Item(9, i).Value = dt.Rows(i).Item("INPUT_BY").ToString()
                dgView.Item(10, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INPUT_DATETIME"))
                dgView.Item(11, i).Value = dt.Rows(i).Item("INPUT_DATETIME")
                dgView.Item(12, i).Value = dt.Rows(i).Item("AUTH_BY").ToString()
                dgView.Item(13, i).Value = NullHelper.DateToString(dt.Rows(i).Item("AUTH_DATETIME"))
            Next

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

#End Region

    Private Sub FrmAddressSumm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


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
        Dim frmAddressDet As New FrmAddressDet()
        frmAddressDet.ShowDialog()
    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try


            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then

                Dim frmAddressDet As New FrmAddressDet(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
                frmAddressDet.Show()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()
    End Sub
    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick
        If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then

            Dim frmAddressDet As New FrmAddressDet(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
            frmAddressDet.Show()
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

    Private Sub LoadAddTypeDataForAuth(ByVal strAddTypeCode As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From  GO_T_ADDRESS Where ADDRESS_ID ='" & strAddTypeCode & "' and STATUS ='L' ")

            If ds.Tables(0).Rows.Count > 0 Then


                _aaddress = ds.Tables(0).Rows(0)("ADDRESS").ToString()

                _acity = ds.Tables(0).Rows(0)("CITY").ToString()

                _acomments = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                _astate = ds.Tables(0).Rows(0)("STATE").ToString()

                _atown = ds.Tables(0).Rows(0)("TOWN").ToString()

                _azipCode = ds.Tables(0).Rows(0)("ZIP").ToString()

                _aaddressType = ds.Tables(0).Rows(0)("ADDRESS_TYPE").ToString()

                _acountry = ds.Tables(0).Rows(0)("COUNTRY_CODE").ToString()

                '--------------Mizan Work (26-04-2016---------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE ='" & _acountry & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _acountryName = ds2.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _acountry = _acountryName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_CONTACT_TYPE Where CTYPE_CODE = '" & _aaddressType & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _aaddressTypeName = ds3.Tables(0).Rows(0)("CTYPE_NAME").ToString()
                    _aaddressType = _aaddressTypeName

                End If

                '--------------Mizan Work (26-04-2016---------


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--------------Mizan Work (24-04-16)---------------------

    Private Sub LoadAddTypeData(ByVal strAddTypeCode As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Address_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@ADDRESS_ID", DbType.String, strAddTypeCode)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intMod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then


                _address = ds.Tables(0).Rows(0)("ADDRESS").ToString()

                _city = ds.Tables(0).Rows(0)("CITY").ToString()

                _comments = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                _state = ds.Tables(0).Rows(0)("STATE").ToString()

                _town = ds.Tables(0).Rows(0)("TOWN").ToString()

                _zipCode = ds.Tables(0).Rows(0)("ZIP").ToString()


                _addressType = ds.Tables(0).Rows(0)("ADDRESS_TYPE").ToString()

                _country = ds.Tables(0).Rows(0)("COUNTRY_CODE").ToString()


                '--------------Mizan Work (26-04-2016---------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE ='" & _country & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _countryName = ds2.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _country = _countryName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_CONTACT_TYPE Where CTYPE_CODE = '" & _addressType & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _addressTypeName = ds3.Tables(0).Rows(0)("CTYPE_NAME").ToString()
                    _addressType = _addressTypeName

                End If

                '--------------Mizan Work (26-04-2016---------


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

                            LoadAddTypeData(dgView.Rows(i).Cells(3).Value.ToString(), dgView.Rows(i).Cells(1).Value)


                            If (dgView.Rows(i).Cells(1).Value) > 1 Then

                                LoadAddTypeDataForAuth(dgView.Rows(i).Cells(3).Value.ToString())

                                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Address_Auth")



                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@ADDRESS_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "Information Authorized Successfully !! "

                                    If _aaddress <> _address Then
                                        If _aaddress = "" Then
                                            log_message = " Address : " + _address + "." + " "
                                        Else
                                            log_message = " Address : " + _aaddress + " " + " To " + " " + _address + "." + " "
                                        End If

                                        AddressList.Add(log_message)
                                    End If
                                    If _aaddressType <> _addressType Then

                                        log_message = " Address Type : " + _aaddressType + " " + " To " + " " + _addressType + "." + " "


                                        AddressList.Add(log_message)
                                    End If
                                    If _acountry <> _country Then
                                        log_message = " Country : " + _acountry + " " + " To " + " " + _country + "." + " "
                                        AddressList.Add(log_message)
                                    End If
                                    If _atown <> _town Then
                                        If _atown = "" Then
                                            log_message = " Town : " + _town + "." + " "
                                        Else
                                            log_message = " Town : " + _atown + " " + " To " + " " + _town + "." + " "
                                        End If

                                        AddressList.Add(log_message)
                                    End If
                                    If _acity <> _city Then
                                        If _acity = "" Then
                                            log_message = " City : " + _city + "." + " "
                                        Else
                                            log_message = " City : " + _acity + " " + " To " + " " + _city + "." + " "
                                        End If

                                        AddressList.Add(log_message)
                                    End If
                                    If _azipCode <> _zipCode Then
                                        If _azipCode = "" Then
                                            log_message = " Zip Code : " + _zipCode + "." + " "

                                        Else
                                            log_message = " Zip Code : " + _azipCode + " " + " To " + " " + _zipCode + "." + " "

                                        End If
                                        AddressList.Add(log_message)
                                    End If

                                    If _astate <> _state Then
                                        If _astate = "" Then
                                            log_message = " State : " + _state + "." + " "

                                        Else
                                            log_message = " State : " + _astate + " " + " To " + " " + _state + "." + " "

                                        End If
                                        AddressList.Add(log_message)
                                    End If
                                    If _acomments <> _comments Then
                                        If _acomments = "" Then
                                            log_message = " Comments : " + _comments + "." + " "

                                        Else
                                            log_message = " Comments : " + _acomments + " " + " To " + " " + _comments + "." + " "

                                        End If
                                        AddressList.Add(log_message)
                                    End If

                                    For Each addrslist As String In AddressList
                                        _addressLog += addrslist
                                    Next

                                    _Alog = " Authorized : Address Code : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + _addressLog

                                    Logger.system_log(_Alog)
                                    _addressLog = ""
                                    AddressList.Clear()

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

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Address_Auth")


                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@ADDRESS_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "Information Authorized Successfully !! "

                                    If _aaddress <> _address Then
                                        If _aaddress = "" Then
                                            log_message = " Address : " + _address + "." + " "
                                        Else
                                            log_message = " Address : " + _aaddress + " " + " To " + " " + _address + "." + " "
                                        End If

                                        AddressList.Add(log_message)
                                    End If
                                    If _aaddressType <> _addressType Then
                                        If _aaddressType = "" Then
                                        Else
                                            log_message = " Address Type : " + _aaddressType + " " + " To " + " " + _addressType + "." + " "
                                            AddressList.Add(log_message)
                                        End If


                                    End If
                                    If _acountry <> _country Then
                                        If _acountry = "" Then
                                        Else
                                            log_message = " Country : " + _acountry + " " + " To " + " " + _country + "." + " "
                                            AddressList.Add(log_message)
                                        End If
                                    End If

                                    If _atown <> _town Then
                                        If _atown = "" Then
                                            log_message = " Town : " + _town + "." + " "
                                        Else
                                            log_message = " Town : " + _atown + " " + " To " + " " + _town + "." + " "
                                        End If

                                        AddressList.Add(log_message)
                                    End If
                                    If _acity <> _city Then
                                        If _acity = "" Then
                                            log_message = " City : " + _city + "." + " "
                                        Else
                                            log_message = " City : " + _acity + " " + " To " + " " + _city + "." + " "
                                        End If

                                        AddressList.Add(log_message)
                                    End If
                                    If _azipCode <> _zipCode Then
                                        If _azipCode = "" Then
                                            'log_message = " Zip Code : " + _zipCode + "." + " "

                                        Else
                                            log_message = " Zip Code : " + _azipCode + " " + " To " + " " + _zipCode + "." + " "
                                            AddressList.Add(log_message)
                                        End If

                                    End If

                                    If _astate <> _state Then
                                        If _astate = "" Then
                                            ' log_message = " State : " + _state + "." + " "

                                        Else
                                            log_message = " State : " + _astate + " " + " To " + " " + _state + "." + " "
                                            AddressList.Add(log_message)
                                        End If

                                    End If
                                    If _acomments <> _comments Then
                                        If _acomments = "" Then
                                            ' log_message = " Comments : " + _comments + "." + " "

                                        Else
                                            log_message = " Comments : " + _acomments + " " + " To " + " " + _comments + "." + " "
                                            AddressList.Add(log_message)
                                        End If

                                    End If

                                    For Each addrslist As String In AddressList
                                        _addressLog += addrslist
                                    Next

                                    _Alog = " Authorized : Address Code : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _addressLog

                                    Logger.system_log(_Alog)
                                    _addressLog = ""
                                    AddressList.Clear()

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

                'Dim i As Integer

                'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                'Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Address_Auth")

                'For i = 0 To dgView.Rows.Count - 1

                '    If dgView.Rows(i).Cells(0).Value = True Then

                '        commProc.Parameters.Clear()

                '        db.AddInParameter(commProc, "@ADDRESS_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
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

                '    log_message = "Authorized Address Code " + dgView.Rows(i).Cells(3).Value.ToString() + " Address " + dgView.Rows(i).Cells(4).Value.ToString()
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