'
'Author             : Fahad Khan
'Purpose            : Maintain Person Information
'Creation date      : 21-oct-2013
'Stored Procedure(s):  


Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmPersonSummary

#Region "user defined codes"

    Dim _formName As String = "MaintenancePersonSummary"
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
    Dim _employerName As String = ""
    Dim _taxNo As String = ""
    Dim _taxReg As String = ""
    Dim _sourceWealth As String = ""
    Dim _comments As String = ""
    Dim _email As String = ""
    Dim _nationality1Name As String = ""
    Dim _residenceName As String = ""
    Dim _passCountryName As String = ""

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
    Dim _bemployerName As String = ""
    Dim _btaxNo As String = ""
    Dim _btaxReg As String = ""
    Dim _bsourceWealth As String = ""
    Dim _bcomments As String = ""
    Dim _bemail As String = ""
    Dim _bresidenceName As String = ""
    Dim _bnationality1Name As String = ""
    Dim _bpassCountryName As String = ""

    Dim PersonList As New List(Of String)
    Dim _personLog As String = ""
    Dim _Plog As String = ""

    Private Sub LoadDataGrid()


        If dgView.Columns.Count = 0 Then Exit Sub

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_RPerson_GetDetailList")

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
                dgView.Item(3, i).Value = dt.Rows(i).Item("PERSON_ID").ToString()

                If dt.Rows(i).Item("GENDER").ToString() = "M" Then
                    dgView.Item(4, i).Value = "MALE"

                Else
                    dgView.Item(4, i).Value = "FEMALE"
                End If

                dgView.Item(5, i).Value = dt.Rows(i).Item("FIRST_NAME").ToString()
                dgView.Item(6, i).Value = dt.Rows(i).Item("LAST_NAME").ToString()
                dgView.Item(7, i).Value = dt.Rows(i).Item("MOTHERS_NAME").ToString()
                dgView.Item(8, i).Value = dt.Rows(i).Item("ID_NUMBER").ToString()
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

    Private Sub FrmPersonSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


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
        Dim frmPersonDetail As New FrmPersonDetail()
        frmPersonDetail.ShowDialog()
    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try


            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then

                Dim frmPersonDetail As New FrmPersonDetail(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
                frmPersonDetail.Show()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()
    End Sub
    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick
        If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then

            Dim frmPersonDetail As New FrmPersonDetail(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
            frmPersonDetail.Show()
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

    Private Sub LoadMainDataForAuth(ByVal strId As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From  GO_REPORT_PERSON Where PERSON_ID ='" & strId & "' and STATUS ='L' ")

            If ds.Tables(0).Rows.Count > 0 Then

                
                _btitle = ds.Tables(0).Rows(0)("TITLE").ToString()

                _bfname = ds.Tables(0).Rows(0)("FIRST_NAME").ToString()

                _bmname = ds.Tables(0).Rows(0)("MIDDLE_NAME").ToString()

                _blname = ds.Tables(0).Rows(0)("LAST_NAME").ToString()

                _bspouse = ds.Tables(0).Rows(0)("PREFIX").ToString()

                _bbirthdate = NullHelper.DateToStringNew(ds.Tables(0).Rows(0)("BIRTHDATE"))

                _bbirthplace = ds.Tables(0).Rows(0)("BIRTH_PLACE").ToString()

                _bmother = ds.Tables(0).Rows(0)("MOTHERS_NAME").ToString()

                _bfather = ds.Tables(0).Rows(0)("ALIAS").ToString()

                _bnationalID = ds.Tables(0).Rows(0)("SSN").ToString()

                _bpassNo = ds.Tables(0).Rows(0)("PASSPORT_NUMBER").ToString()

                _bbirthID = ds.Tables(0).Rows(0)("ID_NUMBER").ToString()

                _bpassCountry = ds.Tables(0).Rows(0)("PASSPORT_COUNTRY").ToString()

                _bnationality1 = ds.Tables(0).Rows(0)("NATIONALITY1").ToString()

                _bresidence = ds.Tables(0).Rows(0)("RESIDENCE").ToString()

                _boccpation = ds.Tables(0).Rows(0)("OCCUPATION").ToString()

                _bemployerName = ds.Tables(0).Rows(0)("EMPLOYER_NAME").ToString()

                _bemail = ds.Tables(0).Rows(0)("EMAIL").ToString()

                _btaxNo = ds.Tables(0).Rows(0)("TAX_NUMBER").ToString()
                _btaxReg = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString()

                _bsourceWealth = ds.Tables(0).Rows(0)("SOURCE_OF_WEALTH").ToString()

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


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--------------Mizan Work (24-04-16)---------------------

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

                            LoadMainData(dgView.Rows(i).Cells(3).Value.ToString(), dgView.Rows(i).Cells(1).Value)


                            If (dgView.Rows(i).Cells(1).Value) > 1 Then

                                LoadMainDataForAuth(dgView.Rows(i).Cells(3).Value.ToString())

                                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_RPerson_Auth")



                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@PERSON_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "Information Authorized Successfully !! "

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
                                            log_message = " Last Name " + _lname + "." + " "
                                        Else
                                            log_message = " Last Name " + _blname + " " + " To " + " " + _lname + "." + " "
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

                                    _Plog = " Authorized : Reporting Person : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + _personLog

                                    Logger.system_log(_Plog)
                                    _personLog = ""
                                    PersonList.Clear()

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

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_RPerson_Auth")


                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@PERSON_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(11).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "Information Authorized Successfully !! "

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

                                    _Plog = " Authorized : Reporting Person : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _personLog

                                    Logger.system_log(_Plog)

                                    _personLog = ""
                                    PersonList.Clear()

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

                'Dim commProc As DbCommand = db.GetStoredProcCommand("GO_RPerson_Auth")

                'For i = 0 To dgView.Rows.Count - 1

                '    If dgView.Rows(i).Cells(0).Value = True Then

                '        commProc.Parameters.Clear()

                '        db.AddInParameter(commProc, "@PERSON_ID", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
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
End Class