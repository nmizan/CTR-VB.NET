'
'Author             : Fahad Khan
'Purpose            : Maintain Director Information
'Creation date      : 19-Dec-2013
'Stored Procedure(s):  


Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmDirectorSummary

#Region "user defined codes"

    Dim _formName As String = "MaintenanceGoDirectorSummary"
    Dim opt As SecForm = New SecForm(_formName)
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
    Dim _residenceName As String = ""
    Dim _nationality1Name As String = ""
    Dim _passCountryName As String = ""
    Dim _employerName As String = ""
    Dim _taxNo As String = ""
    Dim _taxReg As String = ""
    Dim _sourceWealth As String = ""
    Dim _comments As String = ""
    Dim _email As String = ""

    'For Auth
    Dim _dtitle As String = ""
    Dim _dfname As String = ""
    Dim _dlname As String = ""
    Dim _dmname As String = ""
    Dim _dbirthdate As String = ""
    Dim _dnationalID As String = ""
    Dim _dpassNo As String = ""
    Dim _dbirthID As String = ""
    Dim _dspouse As String = ""
    Dim _dmother As String = ""
    Dim _dfather As String = ""
    Dim _dbirthplace As String = ""
    Dim _doccpation As String = ""
    Dim _dresidence As String = ""
    Dim _dnationality1 As String = ""
    Dim _dpassCountry As String = ""
    Dim _dresidenceName As String = ""
    Dim _dnationality1Name As String = ""
    Dim _dpassCountryName As String = ""
    Dim _demployerName As String = ""
    Dim _dtaxNo As String = ""
    Dim _dtaxReg As String = ""
    Dim _dsourceWealth As String = ""
    Dim _dcomments As String = ""
    Dim _demail As String = ""

    Dim DirectorList As New List(Of String)
    Dim _DirectorLog As String = ""
    Dim _Dlog As String = ""

    Private Sub LoadDataGrid()


        If dgView.Columns.Count = 0 Then Exit Sub

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_GetDetailList")

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
                dgView.Item(3, i).Value = dt.Rows(i).Item("DIRECTOR_ID").ToString()

                If dt.Rows(i).Item("GENDER").ToString() = "M" Then
                    dgView.Item(4, i).Value = "MALE"

                ElseIf dt.Rows(i).Item("GENDER").ToString() = "F" Then
                    dgView.Item(4, i).Value = "FEMALE"
                End If

                dgView.Item(5, i).Value = dt.Rows(i).Item("FIRST_NAME").ToString()
                dgView.Item(6, i).Value = dt.Rows(i).Item("LAST_NAME").ToString()
                dgView.Item(7, i).Value = dt.Rows(i).Item("MOTHERS_NAME").ToString()
                dgView.Item(8, i).Value = dt.Rows(i).Item("ID_NUMBER")
                dgView.Item(9, i).Value = dt.Rows(i).Item("INPUT_BY").ToString()
                dgView.Item(10, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INPUT_DATETIME"))
                dgView.Item(11, i).Value = dt.Rows(i).Item("INPUT_DATETIME")
                dgView.Item(12, i).Value = dt.Rows(i).Item("AUTH_BY").ToString()
                dgView.Item(13, i).Value = NullHelper.DateToString(dt.Rows(i).Item("AUTH_DATETIME"))

                dgView.Item(14, i).Value = NullHelper.DateToString(dt.Rows(i).Item("BIRTHDATE"))
                dgView.Item(15, i).Value = dt.Rows(i).Item("BIRTH_PLACE").ToString()
                dgView.Item(16, i).Value = dt.Rows(i).Item("ALIAS").ToString()
                dgView.Item(17, i).Value = dt.Rows(i).Item("SSN").ToString()
                dgView.Item(18, i).Value = dt.Rows(i).Item("PASSPORT_NUMBER").ToString()
                dgView.Item(19, i).Value = dt.Rows(i).Item("PASSPORT_COUNTRY").ToString()
                dgView.Item(20, i).Value = dt.Rows(i).Item("NATIONALITY1").ToString()

                dgView.Item(21, i).Value = dt.Rows(i).Item("NATIONALITY2").ToString()
                dgView.Item(22, i).Value = dt.Rows(i).Item("NATIONALITY3").ToString()
                dgView.Item(23, i).Value = dt.Rows(i).Item("RESIDENCE").ToString()
                dgView.Item(24, i).Value = dt.Rows(i).Item("EMAIL").ToString()
                dgView.Item(25, i).Value = dt.Rows(i).Item("OCCUPATION").ToString()

                dgView.Item(26, i).Value = dt.Rows(i).Item("EMPLOYER_NAME").ToString()

                dgView.Item(27, i).Value = dt.Rows(i).Item("TAX_NUMBER").ToString()

                dgView.Item(28, i).Value = dt.Rows(i).Item("TAX_REG_NUMBER").ToString()

                dgView.Item(29, i).Value = dt.Rows(i).Item("SOURCE_OF_WEALTH").ToString()
                dgView.Item(30, i).Value = dt.Rows(i).Item("COMMENTS").ToString()






            Next

            lblTotRecNo.Text = dt.Rows.Count

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub


    Private Sub LoadDataGridWithEntityDirector()


        If dgView.Columns.Count = 0 Then Exit Sub

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_GetDetailListWithAccount")

            commProc.Parameters.Clear()

            Dim dt As DataTable = db.ExecuteDataSet(commProc).Tables(0)

            Dim i As Integer

            dgView.Rows.Clear()

            For i = 0 To dt.Rows.Count - 1
                dgView.Rows.Add()
                dgView.Item(1, i).Value = dt.Rows(i).Item("MOD_NO").ToString()
                dgView.Item(2, i).Value = dt.Rows(i).Item("S").ToString()
                dgView.Item(3, i).Value = dt.Rows(i).Item("DIRECTOR_ID").ToString()

                If dt.Rows(i).Item("GENDER").ToString() = "M" Then
                    dgView.Item(4, i).Value = "MALE"

                ElseIf dt.Rows(i).Item("GENDER").ToString() = "F" Then
                    dgView.Item(4, i).Value = "FEMALE"
                End If

                dgView.Item(5, i).Value = dt.Rows(i).Item("FIRST_NAME").ToString()
                dgView.Item(6, i).Value = dt.Rows(i).Item("LAST_NAME").ToString()
                dgView.Item(7, i).Value = dt.Rows(i).Item("MOTHERS_NAME").ToString()
                dgView.Item(8, i).Value = dt.Rows(i).Item("ID_NUMBER")
                dgView.Item(9, i).Value = dt.Rows(i).Item("INPUT_BY").ToString()
                dgView.Item(10, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INPUT_DATETIME"))
                dgView.Item(11, i).Value = dt.Rows(i).Item("INPUT_DATETIME")
                dgView.Item(12, i).Value = dt.Rows(i).Item("AUTH_BY").ToString()
                dgView.Item(13, i).Value = NullHelper.DateToString(dt.Rows(i).Item("AUTH_DATETIME"))

                dgView.Item(14, i).Value = NullHelper.DateToString(dt.Rows(i).Item("BIRTHDATE"))
                dgView.Item(15, i).Value = dt.Rows(i).Item("BIRTH_PLACE").ToString()
                dgView.Item(16, i).Value = dt.Rows(i).Item("ALIAS").ToString()
                dgView.Item(17, i).Value = dt.Rows(i).Item("SSN").ToString()
                dgView.Item(18, i).Value = dt.Rows(i).Item("PASSPORT_NUMBER").ToString()
                dgView.Item(19, i).Value = dt.Rows(i).Item("PASSPORT_COUNTRY").ToString()
                dgView.Item(20, i).Value = dt.Rows(i).Item("NATIONALITY1").ToString()

                dgView.Item(21, i).Value = dt.Rows(i).Item("NATIONALITY2").ToString()
                dgView.Item(22, i).Value = dt.Rows(i).Item("NATIONALITY3").ToString()
                dgView.Item(23, i).Value = dt.Rows(i).Item("RESIDENCE").ToString()
                dgView.Item(24, i).Value = dt.Rows(i).Item("EMAIL").ToString()
                dgView.Item(25, i).Value = dt.Rows(i).Item("OCCUPATION").ToString()

                dgView.Item(26, i).Value = dt.Rows(i).Item("EMPLOYER_NAME").ToString()

                dgView.Item(27, i).Value = dt.Rows(i).Item("TAX_NUMBER").ToString()

                dgView.Item(28, i).Value = dt.Rows(i).Item("TAX_REG_NUMBER").ToString()

                dgView.Item(29, i).Value = dt.Rows(i).Item("SOURCE_OF_WEALTH").ToString()
                dgView.Item(30, i).Value = dt.Rows(i).Item("COMMENTS").ToString()

                dgView.Item(31, i).Value = dt.Rows(i).Item("ENTITY_ID").ToString()

                dgView.Item(32, i).Value = dt.Rows(i).Item("ACNUMBER").ToString()






            Next

            lblTotRecNo.Text = dt.Rows.Count

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

#End Region

    Private Sub FrmDirectorSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
        Dim frmDirectorDetail As New FrmDirectorDetail()
        frmDirectorDetail.ShowDialog()
    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try
            If chkAccount.Checked = False Then

                If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then

                    Dim frmDirectorDetail As New FrmDirectorDetail(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
                    frmDirectorDetail.Show()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If chkAccount.Checked = True Then

            LoadDataGridWithEntityDirector()

        Else
            LoadDataGrid()
        End If



    End Sub

    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick

        If chkAccount.Checked = False Then

            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then

                Dim frmDirectorDetail As New FrmDirectorDetail(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
                frmDirectorDetail.Show()
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


    '--------------Mizan Work (23-04-16)---------------------

    Private Sub LoadMainDataForAuth(ByVal strId As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From  GO_DIRECTOR_INFO Where DIRECTOR_ID='" & strId & "' and STATUS ='L' ")

            If ds.Tables(0).Rows.Count > 0 Then


                'txtTitle.Text = ds.Tables(0).Rows(0)("TITLE").ToString()
                _dtitle = ds.Tables(0).Rows(0)("TITLE").ToString()
                ' txtFirstName.Text = ds.Tables(0).Rows(0)("FIRST_NAME").ToString()
                _dfname = ds.Tables(0).Rows(0)("FIRST_NAME").ToString()
                ' txtMiddle.Text = ds.Tables(0).Rows(0)("MIDDLE_NAME").ToString()
                _dmname = ds.Tables(0).Rows(0)("MIDDLE_NAME").ToString()
                'txtLast.Text = ds.Tables(0).Rows(0)("LAST_NAME").ToString()
                _dlname = ds.Tables(0).Rows(0)("LAST_NAME").ToString()
                ' txtPrefix.Text = ds.Tables(0).Rows(0)("PREFIX").ToString()
                _dspouse = ds.Tables(0).Rows(0)("PREFIX").ToString()


                'txtBirthDate.Text = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("BIRTHDATE"))
                _dbirthdate = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("BIRTHDATE"))
                'txtBirthplace.Text = ds.Tables(0).Rows(0)("BIRTH_PLACE").ToString()
                _dbirthplace = ds.Tables(0).Rows(0)("BIRTH_PLACE").ToString()
                'txtMother.Text = ds.Tables(0).Rows(0)("MOTHERS_NAME").ToString()
                _dmother = ds.Tables(0).Rows(0)("MOTHERS_NAME").ToString()
                ' txtAlias.Text = ds.Tables(0).Rows(0)("ALIAS").ToString()
                _dfather = ds.Tables(0).Rows(0)("ALIAS").ToString()
                ' txtSSN.Text = ds.Tables(0).Rows(0)("SSN").ToString()
                _dnationalID = ds.Tables(0).Rows(0)("SSN").ToString()
                'TxtPassportNo.Text = ds.Tables(0).Rows(0)("PASSPORT_NUMBER").ToString()
                _dpassNo = ds.Tables(0).Rows(0)("PASSPORT_NUMBER").ToString()

                _dbirthID = ds.Tables(0).Rows(0)("ID_NUMBER").ToString()

                'cbmPassportCountry.SelectedValue = ds.Tables(0).Rows(0)("PASSPORT_COUNTRY")
                _dpassCountry = ds.Tables(0).Rows(0)("PASSPORT_COUNTRY").ToString()

                'cmbNationality1.SelectedValue = ds.Tables(0).Rows(0)("NATIONALITY1")
                _dnationality1 = ds.Tables(0).Rows(0)("NATIONALITY1").ToString()

                'cmbResidence.SelectedValue = ds.Tables(0).Rows(0)("RESIDENCE")
                _dresidence = ds.Tables(0).Rows(0)("RESIDENCE").ToString()
                _doccpation = ds.Tables(0).Rows(0)("OCCUPATION").ToString()
                _demployerName = ds.Tables(0).Rows(0)("EMPLOYER_NAME").ToString()

                _demail = ds.Tables(0).Rows(0)("EMAIL").ToString()


                _dtaxNo = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()

                _dtaxReg = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString()

                _dsourceWealth = ds.Tables(0).Rows(0)("SOURCE_OF_WEALTH").ToString()

                _dcomments = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                '--------------Mizan Work (26-04-2016-----------
                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE = '" & _dnationality1 & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _dnationality1Name = ds3.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _dnationality1 = _dnationality1Name

                End If

                Dim ds4 As New DataSet
                ds4 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE ='" & _dresidence & "' ")
                If ds4.Tables(0).Rows.Count > 0 Then

                    _dresidenceName = ds4.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _dresidence = _dresidenceName

                End If

                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE ='" & _dpassCountry & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _dpassCountryName = ds2.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _dpassCountry = _dpassCountryName

                End If
                '--------------Mizan Work (26-04-2016-----------

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--------------Mizan Work (23-04-16)---------------------

    Private Sub LoadMainData(ByVal strId As String, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@DIRECTOR_ID", DbType.String, strId)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intmod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then

                
                _title = ds.Tables(0).Rows(0)("TITLE").ToString()

                _fname = ds.Tables(0).Rows(0)("FIRST_NAME").ToString()

                _mname = ds.Tables(0).Rows(0)("MIDDLE_NAME").ToString()

                _lname = ds.Tables(0).Rows(0)("LAST_NAME").ToString()

                _spouse = ds.Tables(0).Rows(0)("PREFIX").ToString()

                _birthdate = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("BIRTHDATE"))

                _birthplace = ds.Tables(0).Rows(0)("BIRTH_PLACE").ToString()

                _mother = ds.Tables(0).Rows(0)("MOTHERS_NAME").ToString()

                _father = ds.Tables(0).Rows(0)("ALIAS").ToString()

                _nationalID = ds.Tables(0).Rows(0)("SSN").ToString()

                _passNo = ds.Tables(0).Rows(0)("PASSPORT_NUMBER").ToString()

                _birthID = ds.Tables(0).Rows(0)("ID_NUMBER").ToString()
                _passCountry = ds.Tables(0).Rows(0)("PASSPORT_COUNTRY").ToString()


                _nationality1 = ds.Tables(0).Rows(0)("NATIONALITY1").ToString()
               
                _residence = ds.Tables(0).Rows(0)("RESIDENCE").ToString()

                _occpation = ds.Tables(0).Rows(0)("OCCUPATION").ToString()

                _employerName = ds.Tables(0).Rows(0)("EMPLOYER_NAME").ToString()

               
                _email = ds.Tables(0).Rows(0)("EMAIL").ToString()
                
                _taxNo = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()
                _taxReg = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString()

                _sourceWealth = ds.Tables(0).Rows(0)("SOURCE_OF_WEALTH").ToString()

                _comments = ds.Tables(0).Rows(0)("COMMENTS").ToString()

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

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Try

            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                '--------------Mizan Work (23-04-16)---------------------


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

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_Auth")


                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@DIRECTOR_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "!! Information Authorized Successfully !!"

                                    If _dtitle <> _title Then
                                        If _dtitle = "" Then
                                            log_message = " Title : " + _title + "." + " "

                                        Else
                                            log_message = " Title : " + _dtitle + " " + " To " + " " + _title + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dfname <> _fname Then
                                        If _dfname = "" Then
                                            log_message = " First Name : " + _fname + "." + " "
                                        Else
                                            log_message = " First Name : " + _dfname + " " + " To " + " " + _fname + "." + " "
                                        End If

                                        DirectorList.Add(log_message)
                                    End If
                                    If _dmname <> _mname Then
                                        If _dmname = "" Then
                                            log_message = " Middle Name : " + _mname + "." + " "

                                        Else
                                            log_message = " Middle Name : " + _dmname + " " + " To " + " " + _mname + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dlname <> _lname Then
                                        If _dlname = "" Then
                                            log_message = " Last Name : " + _lname + "." + " "
                                        Else
                                            log_message = " Last Name : " + _dlname + " " + " To " + " " + _lname + "." + " "
                                        End If

                                        DirectorList.Add(log_message)
                                    End If
                                    If _dspouse <> _spouse Then
                                        If _dspouse = "" Then
                                            log_message = " Spouse : " + _spouse + "." + " "

                                        Else
                                            log_message = " Spouse  : " + _dspouse + " " + " To " + " " + _spouse + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dbirthdate <> _birthdate Then
                                        If _dbirthdate = "" Then
                                            log_message = " Birth date:  " + _birthdate + "." + " "
                                        Else
                                            log_message = " Birth date:  " + _dbirthdate + " " + " To " + " " + _birthdate + "." + " "
                                        End If

                                        DirectorList.Add(log_message)
                                    End If
                                    If _dbirthplace <> _birthplace Then
                                        If _dbirthplace = "" Then
                                            log_message = " Birth Place : " + _birthplace + "." + " "

                                        Else
                                            log_message = " Birth Place : " + _dbirthplace + " " + " To " + " " + _birthplace + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dmother <> _mother Then
                                        If _dmother = "" Then
                                            log_message = " Mother : " + _mother + "." + " "

                                        Else
                                            log_message = " Mother : " + _dmother + " " + " To " + " " + _mother + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dfather <> _father Then
                                        If _dfather = "" Then
                                            log_message = " Father : " + _father + "." + " "

                                        Else
                                            log_message = " Father : " + _dfather + " " + " To " + " " + _father + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dnationalID <> _nationalID Then
                                        If _dnationalID = "" Then
                                            log_message = " National ID : " + _nationalID + "." + " "

                                        Else
                                            log_message = " National ID : " + _dnationalID + " " + " To " + " " + _nationalID + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dpassNo <> _passNo Then
                                        If _dpassNo = "" Then
                                            log_message = " Passport No : " + _passNo + "." + " "

                                        Else
                                            log_message = " Passport No  : " + _dpassNo + " " + " To " + " " + _passNo + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dpassCountry <> _passCountry Then
                                        If _dpassCountry = "" Then
                                            log_message = " Passport Country  : " + _passCountry + "." + " "
                                        Else
                                            log_message = " Passport Country  : " + _dpassCountry + " " + " To " + " " + _passCountry + "." + " "
                                        End If


                                        DirectorList.Add(log_message)
                                    End If

                                    If _dbirthID <> _birthID Then
                                        If _dbirthID = "" Then
                                            log_message = " Birth Reg ID : " + _birthID + "." + " "

                                        Else
                                            log_message = "  Birth Reg ID : " + _dbirthID + " " + " To " + " " + _birthID + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dnationality1 <> _nationality1 Then
                                        If _dnationality1 = "" Then
                                            log_message = " Nationality 1 : " + _nationality1 + "." + " "
                                        Else
                                            log_message = " Nationality 1 : " + _dnationality1 + " " + " To " + " " + _nationality1 + "." + " "
                                        End If

                                        DirectorList.Add(log_message)
                                    End If
                                    If _dresidence <> _residence Then
                                        If _dresidence = "" Then
                                            log_message = "  Residence : " + _residence + "." + " "
                                        Else
                                            log_message = "  Residence : " + _dresidence + " " + " To " + " " + _residence + "." + " "
                                        End If

                                        DirectorList.Add(log_message)
                                    End If

                                    If _doccpation <> _occpation Then
                                        If _doccpation = "" Then
                                            log_message = " Occupation : " + _occpation + "." + " "

                                        Else
                                            log_message = "   Occupation : " + _doccpation + " " + " To " + " " + _occpation + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _demployerName <> _employerName Then
                                        If _demployerName = "" Then
                                            log_message = " Employer Name : " + _employerName + "." + " "

                                        Else
                                            log_message = "  Employer Name : " + _demployerName + " " + " To " + " " + _employerName + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _demail <> _email Then
                                        If _demail = "" Then
                                            log_message = " Email : " + _email + "." + " "

                                        Else
                                            log_message = "  Email : " + _demail + " " + " To " + " " + _email + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dtaxNo <> _taxNo Then
                                        If _dtaxNo = "" Then
                                            log_message = " Tax No : " + _taxNo + "." + " "

                                        Else
                                            log_message = " Tax No : " + _dtaxNo + " " + " To " + " " + _taxNo + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dtaxReg <> _taxReg Then
                                        If _dtaxReg = "" Then
                                            log_message = " Tax Reg No : " + _taxReg + "." + " "

                                        Else
                                            log_message = " Tax Reg No : " + _dtaxReg + " " + " To " + " " + _taxReg + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dsourceWealth <> _sourceWealth Then
                                        If _dsourceWealth = "" Then
                                            log_message = " Source wealth : " + _sourceWealth + "." + " "

                                        Else
                                            log_message = " Source wealth : " + _dsourceWealth + " " + " To " + " " + _sourceWealth + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dcomments <> _comments Then
                                        If _dcomments = "" Then
                                            log_message = " Comments : " + _comments + "." + " "

                                        Else
                                            log_message = " Comments : " + _dcomments + " " + " To " + " " + _comments + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If

                                    For Each direclist As String In DirectorList
                                        _DirectorLog += direclist
                                    Next

                                    _Dlog = " Authorized : Director Person ID : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + _DirectorLog

                                    Logger.system_log(_Dlog)
                                    _DirectorLog = ""
                                    DirectorList.Clear()

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

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_Auth")


                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@DIRECTOR_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "!! Information Authorized Successfully !!"

                                    If _dtitle <> _title Then
                                        If _dtitle = "" Then
                                            'log_message = " Title : " + _title + "." + " "

                                        Else
                                            log_message = " Title : " + _dtitle + " " + " To " + " " + _title + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _dfname <> _fname Then
                                        If _dfname = "" Then
                                            log_message = " First Name : " + _fname + "." + " "
                                        Else
                                            log_message = " First Name : " + _dfname + " " + " To " + " " + _fname + "." + " "
                                        End If

                                        DirectorList.Add(log_message)
                                    End If
                                    If _dmname <> _mname Then
                                        If _dmname = "" Then
                                            log_message = " Middle Name : " + _mname + "." + " "

                                        Else
                                            log_message = " Middle Name : " + _dmname + " " + " To " + " " + _mname + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dlname <> _lname Then
                                        If _dlname = "" Then
                                            log_message = " Last Name : " + _lname + "." + " "
                                        Else
                                            log_message = " Last Name : " + _dlname + " " + " To " + " " + _lname + "." + " "
                                        End If

                                        DirectorList.Add(log_message)
                                    End If
                                    If _dspouse <> _spouse Then
                                        If _dspouse = "" Then
                                            'log_message = " Spouse : " + _spouse + "." + " "

                                        Else
                                            log_message = " Spouse  : " + _dspouse + " " + " To " + " " + _spouse + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _dbirthdate <> _birthdate Then
                                        If _dbirthdate = "" Then
                                            log_message = " Birth date:  " + _birthdate + "." + " "
                                        Else
                                            log_message = " Birth date:  " + _dbirthdate + " " + " To " + " " + _birthdate + "." + " "
                                        End If

                                        DirectorList.Add(log_message)
                                    End If
                                    If _dbirthplace <> _birthplace Then
                                        If _dbirthplace = "" Then
                                            ' log_message = " Birth Place : " + _birthplace + "." + " "

                                        Else
                                            log_message = " Birth Place : " + _dbirthplace + " " + " To " + " " + _birthplace + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _dmother <> _mother Then
                                        If _dmother = "" Then
                                            '  log_message = " Mother : " + _mother + "." + " "

                                        Else
                                            log_message = " Mother : " + _dmother + " " + " To " + " " + _mother + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _dfather <> _father Then
                                        If _dfather = "" Then
                                            ' log_message = " Father : " + _father + "." + " "

                                        Else
                                            log_message = " Father : " + _dfather + " " + " To " + " " + _father + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _dnationalID <> _nationalID Then
                                        If _dnationalID = "" Then
                                            log_message = " National ID : " + _nationalID + "." + " "

                                        Else
                                            log_message = " National ID : " + _dnationalID + " " + " To " + " " + _nationalID + "." + " "

                                        End If
                                        DirectorList.Add(log_message)
                                    End If
                                    If _dpassNo <> _passNo Then
                                        If _dpassNo = "" Then
                                            ' log_message = " Passport No : " + _passNo + "." + " "

                                        Else
                                            log_message = " Passport No  : " + _dpassNo + " " + " To " + " " + _passNo + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _dpassCountry <> _passCountry Then
                                        If _dpassCountry = "" Then
                                            'log_message = " Passport Country  : " + _passCountry + "." + " "
                                        Else
                                            log_message = " Passport Country  : " + _dpassCountry + " " + " To " + " " + _passCountry + "." + " "
                                            DirectorList.Add(log_message)
                                        End If


                                    End If

                                    If _dbirthID <> _birthID Then
                                        If _dbirthID = "" Then
                                            'log_message = " Birth Reg ID : " + _birthID + "." + " "

                                        Else
                                            log_message = "  Birth Reg ID : " + _dbirthID + " " + " To " + " " + _birthID + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _dnationality1 <> _nationality1 Then
                                        If _dnationality1 = "" Then
                                            log_message = " Nationality 1 : " + _nationality1 + "." + " "
                                        Else
                                            log_message = " Nationality 1 : " + _dnationality1 + " " + " To " + " " + _nationality1 + "." + " "

                                        End If
                                        DirectorList.Add(log_message)

                                    End If
                                    If _dresidence <> _residence Then
                                        If _dresidence = "" Then
                                            'log_message = "  Residence : " + _residence + "." + " "
                                        Else
                                            log_message = "  Residence : " + _dresidence + " " + " To " + " " + _residence + "." + " "
                                            DirectorList.Add(log_message)
                                        End If


                                    End If

                                    If _doccpation <> _occpation Then
                                        If _doccpation = "" Then
                                            'log_message = " Occupation : " + _occpation + "." + " "

                                        Else
                                            log_message = "   Occupation : " + _doccpation + " " + " To " + " " + _occpation + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _demployerName <> _employerName Then
                                        If _demployerName = "" Then
                                            ' log_message = " Employer Name : " + _employerName + "." + " "

                                        Else
                                            log_message = "  Employer Name : " + _demployerName + " " + " To " + " " + _employerName + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _demail <> _email Then
                                        If _demail = "" Then
                                            ' log_message = " Email : " + _email + "." + " "

                                        Else
                                            log_message = "  Email : " + _demail + " " + " To " + " " + _email + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _dtaxNo <> _taxNo Then
                                        If _dtaxNo = "" Then
                                            'log_message = " Tax No : " + _taxNo + "." + " "

                                        Else
                                            log_message = " Tax No : " + _dtaxNo + " " + " To " + " " + _taxNo + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _dtaxReg <> _taxReg Then
                                        If _dtaxReg = "" Then
                                            'log_message = " Tax Reg No : " + _taxReg + "." + " "

                                        Else
                                            log_message = " Tax Reg No : " + _dtaxReg + " " + " To " + " " + _taxReg + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _dsourceWealth <> _sourceWealth Then
                                        If _dsourceWealth = "" Then
                                            'log_message = " Source wealth : " + _sourceWealth + "." + " "

                                        Else
                                            log_message = " Source wealth : " + _dsourceWealth + " " + " To " + " " + _sourceWealth + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If
                                    If _dcomments <> _comments Then
                                        If _dcomments = "" Then
                                            ' log_message = " Comments : " + _comments + "." + " "

                                        Else
                                            log_message = " Comments : " + _dcomments + " " + " To " + " " + _comments + "." + " "
                                            DirectorList.Add(log_message)
                                        End If

                                    End If

                                    For Each direclist As String In DirectorList
                                        _DirectorLog += direclist
                                    Next

                                    _Dlog = " Authorized : Director Person ID : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _DirectorLog

                                    Logger.system_log(_Dlog)
                                    _DirectorLog = ""
                                    DirectorList.Clear()

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


                '--------------Mizan Work (23-04-16)---------------------



                '--------------Commented By Mizan (23-04-16)---------------------


                'Dim i As Integer

                'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                'Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_Auth")

                'For i = 0 To dgView.Rows.Count - 1

                '    If dgView.Rows(i).Cells(0).Value = True Then

                '        commProc.Parameters.Clear()

                '        db.AddInParameter(commProc, "@DIRECTOR_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                '        db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                '        db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                '        db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                '        Dim result As Integer

                '        db.ExecuteNonQuery(commProc)
                '        result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                '        If result = 0 Then
                '            lblToolStatus.Text = "!! Information Authorized Successfully !!"
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

                '    log_message = "Authorized Reporting Person " + dgView.Rows(i).Cells(3).Value.ToString() + " Name " + dgView.Rows(i).Cells(5).Value.ToString() + " " + dgView.Rows(i).Cells(6).Value.ToString()
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

    Private Sub chkAccount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAccount.CheckedChanged

        If chkAccount.Checked = True Then

            chkAll.Enabled = False
            chkShowAll.Enabled = False
            rdoAuthorized.Enabled = False
            rdoUnauthorized.Enabled = False

            Dim i As Integer
            For i = 0 To dgView.Columns.Count - 1
                dgView.Columns(i).ReadOnly = True
            Next


        Else


            chkAll.Enabled = True
            chkShowAll.Enabled = True
            rdoAuthorized.Enabled = True
            rdoUnauthorized.Enabled = True

            Dim i As Integer
            For i = 1 To dgView.Columns.Count - 1
                dgView.Columns(i).ReadOnly = True
            Next

        End If



    End Sub
End Class