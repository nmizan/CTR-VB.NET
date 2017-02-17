'
'Author             : Fahad Khan
'Purpose            : Maintain Owner Information
'Creation date      : 29-oct-2013
'Stored Procedure(s):  


Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmOwnergoAmlSumm


#Region "user defined codes"


    Dim _formName As String = "MaintenanceOwnergoAmlSumm"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""

    'For Update
    Dim _Title As String = ""
    Dim _FName As String = ""
    Dim _MName As String = ""
    Dim _LName As String = ""
    Dim _NID As String = ""
    Dim _BirthID As String = ""
    Dim _OccType As String = ""
    Dim _OccTypeName As String = ""
    Dim _EmpName As String = ""
    Dim _TaxReg As String = ""
    Dim _nationality1 As String = ""
    Dim _nationality1Name As String = ""
    Dim _residence As String = ""
    Dim _residenceName As String = ""
    Dim _email As String = ""
    Dim _comments As String = ""
    Dim _sourceWealth As String = ""
    Dim _birthPlace As String = ""

    'For Auth
    Dim _owTitle As String = ""
    Dim _owFName As String = ""
    Dim _owMName As String = ""
    Dim _owLName As String = ""
    Dim _owNID As String = ""
    Dim _owBirthID As String = ""
    Dim _owOccType As String = ""
    Dim _owOccTypeName As String = ""
    Dim _owEmpName As String = ""
    Dim _owTaxReg As String = ""
    Dim _ownationality1 As String = ""
    Dim _ownationality1Name As String = ""
    Dim _owresidence As String = ""
    Dim _owresidenceName As String = ""
    Dim _owemail As String = ""
    Dim _owcomments As String = ""
    Dim _owsourceWealth As String = ""
    Dim _owbirthPlace As String = ""

    Dim GoOwnerList As New List(Of String)
    Dim _goownerLog As String = ""
    Dim _golog As String = ""


  

    Private Sub LoadDataGrid()


        If dgView.Columns.Count = 0 Then Exit Sub

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_OwnerInfo_GetDetailList")

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
                dgView.Item(3, i).Value = dt.Rows(i).Item("OWNER_CODE").ToString()
                dgView.Item(4, i).Value = dt.Rows(i).Item("TITLE").ToString()
                dgView.Item(5, i).Value = dt.Rows(i).Item("FIRST_NAME").ToString()
                dgView.Item(6, i).Value = dt.Rows(i).Item("MIDDLE_NAME").ToString()
                dgView.Item(7, i).Value = dt.Rows(i).Item("LAST_NAME").ToString()
                dgView.Item(8, i).Value = dt.Rows(i).Item("BIRTH_PLACE")
                dgView.Item(9, i).Value = dt.Rows(i).Item("SSN")
                dgView.Item(10, i).Value = dt.Rows(i).Item("ID_NUMBER")
                dgView.Item(11, i).Value = dt.Rows(i).Item("NATIONALITY1")
                dgView.Item(12, i).Value = dt.Rows(i).Item("NATIONALITY2")
                dgView.Item(13, i).Value = dt.Rows(i).Item("NATIONALITY3")
                dgView.Item(14, i).Value = dt.Rows(i).Item("RESIDENCE")
                dgView.Item(15, i).Value = dt.Rows(i).Item("EMAIL")

                dgView.Item(16, i).Value = dt.Rows(i).Item("EMPLOYER_NAME")
                dgView.Item(17, i).Value = dt.Rows(i).Item("TAX_REG_NUMBER")




                dgView.Item(18, i).Value = dt.Rows(i).Item("INPUT_BY")
                dgView.Item(19, i).Value = NullHelper.DateToString(dt.Rows(i).Item("INPUT_DATETIME"))
                dgView.Item(20, i).Value = dt.Rows(i).Item("INPUT_DATETIME")
                dgView.Item(21, i).Value = dt.Rows(i).Item("AUTH_BY").ToString()
                dgView.Item(22, i).Value = NullHelper.DateToString(dt.Rows(i).Item("AUTH_DATETIME"))
            Next

            lblTotRecNo.Text = dt.Rows.Count


        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

#End Region


    Private Sub FrmOwnergoAmlSumm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



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
        Dim frmOwnerInfo As New FrmOwnergoAmlDet()
        frmOwnerInfo.ShowDialog()
    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try


            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then

                Dim frmOwnerInfo As New FrmOwnergoAmlDet(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
                frmOwnerInfo.Show()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()

    End Sub

    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick
        If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then

            Dim frmOwnerInfo As New FrmOwnergoAmlDet(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
            frmOwnerInfo.Show()
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

    ''-----------------------Mizan Work (23-04-16) ---------------------------

    Private Sub LoadMainData(ByVal strId As String, ByVal intmod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_OwnerInfoMain_GetDetail")

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@OWNER_CODE", DbType.String, strId)
            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intmod)

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count > 0 Then

               
                _Title = ds.Tables(0).Rows(0)("TITLE").ToString

                _FName = ds.Tables(0).Rows(0)("FIRST_NAME").ToString

                _MName = ds.Tables(0).Rows(0)("MIDDLE_NAME").ToString

                _LName = ds.Tables(0).Rows(0)("LAST_NAME").ToString
                _birthPlace = ds.Tables(0).Rows(0)("BIRTH_PLACE").ToString()
                _NID = ds.Tables(0).Rows(0)("SSN").ToString

                _OccType = ds.Tables(0).Rows(0)("OCP_CODE").ToString

                _BirthID = ds.Tables(0).Rows(0)("ID_NUMBER").ToString
                _nationality1 = ds.Tables(0).Rows(0)("NATIONALITY1").ToString()
               
                _residence = ds.Tables(0).Rows(0)("RESIDENCE").ToString()

                _EmpName = ds.Tables(0).Rows(0)("EMPLOYER_NAME").ToString

                _email = ds.Tables(0).Rows(0)("EMAIL").ToString()
               
                _TaxReg = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString

                _sourceWealth = ds.Tables(0).Rows(0)("SOURCE_OF_WEALTH").ToString()

                _comments = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                ''------------------Mizan Work (26-04-16) ---------------------
                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_OCCUPATION Where OCP_CODE = '" & _OccType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _OccTypeName = ds2.Tables(0).Rows(0)("OCP_NAME").ToString()
                    _OccType = _OccTypeName

                End If

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

                ''------------------Mizan Work (26-04-16) ----------------------


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--------------Mizan Work (23-04-16)---------------------

    Private Sub LoadOwnerDataForAuth(ByVal strId As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From GO_OWNER_INFO Where OWNER_CODE='" & strId & "' and STATUS ='L' ")


            If ds.Tables(0).Rows.Count > 0 Then

                _owTitle = ds.Tables(0).Rows(0)("TITLE").ToString

                _owFName = ds.Tables(0).Rows(0)("FIRST_NAME").ToString

                _owMName = ds.Tables(0).Rows(0)("MIDDLE_NAME").ToString

                _owLName = ds.Tables(0).Rows(0)("LAST_NAME").ToString

                _owbirthPlace = ds.Tables(0).Rows(0)("BIRTH_PLACE").ToString()

                _owNID = ds.Tables(0).Rows(0)("SSN").ToString
                _owOccType = ds.Tables(0).Rows(0)("OCP_CODE").ToString

                _owBirthID = ds.Tables(0).Rows(0)("ID_NUMBER").ToString
                _ownationality1 = ds.Tables(0).Rows(0)("NATIONALITY1").ToString()

                _owresidence = ds.Tables(0).Rows(0)("RESIDENCE").ToString()

                _owEmpName = ds.Tables(0).Rows(0)("EMPLOYER_NAME").ToString

                _owemail = ds.Tables(0).Rows(0)("EMAIL").ToString()

                _owTaxReg = ds.Tables(0).Rows(0)("TAX_REG_NUMBER").ToString

                _owsourceWealth = ds.Tables(0).Rows(0)("SOURCE_OF_WEALTH").ToString()

                _owcomments = ds.Tables(0).Rows(0)("COMMENTS").ToString()

                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_OCCUPATION Where OCP_CODE = '" & _owOccType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _owOccTypeName = ds2.Tables(0).Rows(0)("OCP_NAME").ToString()
                    _owOccType = _owOccTypeName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE = '" & _ownationality1 & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _ownationality1Name = ds3.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _ownationality1 = _ownationality1Name

                End If

                Dim ds4 As New DataSet
                ds4 = db.ExecuteDataSet(CommandType.Text, "Select *  From GO_COUNTRY_TYPE Where COUNTRY_CODE ='" & _owresidence & "' ")
                If ds4.Tables(0).Rows.Count > 0 Then

                    _owresidenceName = ds4.Tables(0).Rows(0)("COUNTRY_NAME").ToString()
                    _owresidence = _owresidenceName

                End If


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
                        If dgView.Rows(i).Cells(18).Value.ToString = CommonAppSet.User.Trim() Then

                            MessageBox.Show("Maker can't verify data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else

                            LoadMainData(dgView.Rows(i).Cells(3).Value.ToString(), dgView.Rows(i).Cells(1).Value)


                            If (dgView.Rows(i).Cells(1).Value) > 1 Then

                                LoadOwnerDataForAuth(dgView.Rows(i).Cells(3).Value.ToString())

                                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_OwnerInfo_Auth")

                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@OWNER_CODE", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(20).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "!! Information Authorized Successfully !!"

                                    If _owTitle <> _Title Then
                                        If _owTitle = "" Then
                                            log_message = " Title : " + _Title + "." + " "
                                        Else
                                            log_message = " Title : " + _owTitle + " " + " To " + " " + _Title + "." + " "
                                        End If
                                        GoOwnerList.Add(log_message)
                                    End If

                                    If _owFName <> _FName Then
                                        If _owFName = "" Then
                                            log_message = " First Name : " + _FName + "." + " "
                                        Else
                                            log_message = " First Name : " + _owFName + " " + " To " + " " + _FName + "." + " "
                                        End If

                                        GoOwnerList.Add(log_message)
                                    End If
                                    If _owLName <> _LName Then
                                        If _owLName = "" Then
                                            log_message = " Last Name : " + _LName + "." + " "
                                        Else
                                            log_message = " Last Name : " + _owLName + " " + " To " + " " + _LName + "." + " "
                                        End If

                                        GoOwnerList.Add(log_message)
                                    End If
                                    If _owMName <> _MName Then
                                        If _owMName = "" Then
                                            log_message = " Middle Name : " + _MName + "." + " "

                                        Else
                                            log_message = " Middle Name : " + _owMName + " " + " To " + " " + _MName + "." + " "

                                        End If
                                        GoOwnerList.Add(log_message)
                                    End If

                                    If _owNID <> _NID Then
                                        If _owNID = "" Then
                                            log_message = " National ID : " + _NID + "." + " "

                                        Else
                                            log_message = " National ID : " + _owNID + " " + " To " + " " + _NID + "." + " "

                                        End If
                                        GoOwnerList.Add(log_message)
                                    End If
                                    If _owBirthID <> _BirthID Then
                                        If _owBirthID = "" Then
                                            log_message = " Birth Reg ID : " + _BirthID + "." + " "

                                        Else
                                            log_message = " Birth Reg ID : " + _owBirthID + " " + " To " + " " + _BirthID + "." + " "

                                        End If
                                        GoOwnerList.Add(log_message)
                                    End If

                                    If _owbirthPlace <> _birthPlace Then
                                        If _owbirthPlace = "" Then
                                            log_message = " Birth place : " + _birthPlace + "." + " "

                                        Else
                                            log_message = " Birth place : " + _owbirthPlace + " " + " To " + " " + _birthPlace + "." + " "

                                        End If
                                        GoOwnerList.Add(log_message)
                                    End If


                                    If _owTaxReg <> _TaxReg Then
                                        If _owTaxReg = "" Then
                                            log_message = " Tax Reg No. : " + _TaxReg + "." + " "

                                        Else
                                            log_message = " Tax Reg No. : " + _owTaxReg + " " + " To " + " " + _TaxReg + "." + " "

                                        End If
                                        GoOwnerList.Add(log_message)
                                    End If

                                    If _owresidence <> _residence Then

                                        log_message = " Residence : " + _owresidence + " " + " To " + " " + _residence + "." + " "

                                        GoOwnerList.Add(log_message)

                                    End If
                                    If _owcomments <> _comments Then
                                        If _owcomments = "" Then
                                            log_message = " Comments : " + _comments + "." + " "

                                        Else
                                            log_message = " Comments : " + _owcomments + " " + " To " + " " + _comments + "." + " "

                                        End If
                                        GoOwnerList.Add(log_message)
                                    End If

                                    If _owsourceWealth <> _sourceWealth Then
                                        If _owsourceWealth = "" Then
                                            log_message = " Source wealth : " + _sourceWealth + "." + " "

                                        Else
                                            log_message = " Source wealth : " + _owsourceWealth + " " + " To " + " " + _sourceWealth + "." + " "

                                        End If
                                        GoOwnerList.Add(log_message)
                                    End If
                                    If _owEmpName <> _EmpName Then
                                        If _owEmpName = "" Then
                                            log_message = " Employer Name : " + _EmpName + "." + " "

                                        Else
                                            log_message = " Employer Name : " + _owEmpName + " " + " To " + " " + _EmpName + "." + " "

                                        End If
                                        GoOwnerList.Add(log_message)
                                    End If
                                    If _owemail <> _email Then
                                        If _owemail = "" Then
                                            log_message = " Email : " + _email + "." + " "

                                        Else
                                            log_message = " Email : " + _owemail + " " + " To " + " " + _email + "." + " "

                                        End If
                                        GoOwnerList.Add(log_message)
                                    End If

                                    If _ownationality1 <> _nationality1 Then

                                        log_message = " Nationality 1 : " + _ownationality1 + " " + " To " + " " + _nationality1 + "." + " "
                                        GoOwnerList.Add(log_message)

                                    End If
                                    If _owOccType <> _OccType Then

                                        log_message = " Occupation Type : " + _owOccType + " " + " To " + " " + _OccType + "." + " "
                                        GoOwnerList.Add(log_message)

                                    End If

                                    For Each Goownerloglist As String In GoOwnerList
                                        _goownerLog += Goownerloglist
                                    Next

                                    _golog = " Authorized : Owner Code : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + _goownerLog

                                    Logger.system_log(_golog)
                                    _goownerLog = ""
                                    GoOwnerList.Clear()


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

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_OwnerInfo_Auth")

                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@OWNER_CODE", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                                db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(20).Value)

                                db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                Dim result As Integer

                                db.ExecuteNonQuery(commProc)
                                result = db.GetParameterValue(commProc, "@PROC_RET_VAL")
                                If result = 0 Then
                                    lblToolStatus.Text = "!! Information Authorized Successfully !!"

                                    If _owTitle <> _Title Then
                                        If _owTitle = "" Then
                                            'log_message = " Title : " + _Title + "." + " "
                                        Else
                                            log_message = " Title : " + _owTitle + " " + " To " + " " + _Title + "." + " "
                                            GoOwnerList.Add(log_message)
                                        End If

                                    End If

                                    If _owFName <> _FName Then
                                        If _owFName = "" Then
                                            log_message = " First Name : " + _FName + "." + " "
                                        Else
                                            log_message = " First Name : " + _owFName + " " + " To " + " " + _FName + "." + " "
                                        End If

                                        GoOwnerList.Add(log_message)
                                    End If
                                    If _owLName <> _LName Then
                                        If _owLName = "" Then
                                            log_message = " Last Name : " + _LName + "." + " "
                                        Else
                                            log_message = " Last Name : " + _owLName + " " + " To " + " " + _LName + "." + " "
                                        End If

                                        GoOwnerList.Add(log_message)
                                    End If
                                    If _owMName <> _MName Then
                                        If _owMName = "" Then
                                            log_message = " Middle Name : " + _MName + "." + " "

                                        Else
                                            log_message = " Middle Name : " + _owMName + " " + " To " + " " + _MName + "." + " "

                                        End If
                                        GoOwnerList.Add(log_message)
                                    End If

                                    If _owNID <> _NID Then
                                        If _owNID = "" Then
                                            'log_message = " National ID : " + _NID + "." + " "

                                        Else
                                            log_message = " National ID : " + _owNID + " " + " To " + " " + _NID + "." + " "
                                            GoOwnerList.Add(log_message)
                                        End If

                                    End If
                                    If _owBirthID <> _BirthID Then
                                        If _owBirthID = "" Then
                                            'log_message = " Birth Reg ID : " + _BirthID + "." + " "

                                        Else
                                            log_message = " Birth Reg ID : " + _owBirthID + " " + " To " + " " + _BirthID + "." + " "
                                            GoOwnerList.Add(log_message)
                                        End If

                                    End If

                                    If _owbirthPlace <> _birthPlace Then
                                        If _owbirthPlace = "" Then
                                            ' log_message = " Birth place : " + _birthPlace + "." + " "

                                        Else
                                            log_message = " Birth place : " + _owbirthPlace + " " + " To " + " " + _birthPlace + "." + " "
                                            GoOwnerList.Add(log_message)
                                        End If

                                    End If


                                    If _owTaxReg <> _TaxReg Then
                                        If _owTaxReg = "" Then
                                            'log_message = " Tax Reg No. : " + _TaxReg + "." + " "

                                        Else
                                            log_message = " Tax Reg No. : " + _owTaxReg + " " + " To " + " " + _TaxReg + "." + " "
                                            GoOwnerList.Add(log_message)
                                        End If

                                    End If

                                    If _owresidence <> _residence Then
                                        If _owresidence = "" Then
                                        Else
                                            log_message = " Residence : " + _owresidence + " " + " To " + " " + _residence + "." + " "
                                            GoOwnerList.Add(log_message)
                                        End If



                                    End If
                                    If _owcomments <> _comments Then
                                        If _owcomments = "" Then
                                            'log_message = " Comments : " + _comments + "." + " "

                                        Else
                                            log_message = " Comments : " + _owcomments + " " + " To " + " " + _comments + "." + " "
                                            GoOwnerList.Add(log_message)
                                        End If

                                    End If

                                    If _owsourceWealth <> _sourceWealth Then
                                        If _owsourceWealth = "" Then
                                            ' log_message = " Source wealth : " + _sourceWealth + "." + " "

                                        Else
                                            log_message = " Source wealth : " + _owsourceWealth + " " + " To " + " " + _sourceWealth + "." + " "
                                            GoOwnerList.Add(log_message)
                                        End If

                                    End If
                                    If _owEmpName <> _EmpName Then
                                        If _owEmpName = "" Then
                                            'log_message = " Employer Name : " + _EmpName + "." + " "

                                        Else
                                            log_message = " Employer Name : " + _owEmpName + " " + " To " + " " + _EmpName + "." + " "
                                            GoOwnerList.Add(log_message)
                                        End If

                                    End If
                                    If _owemail <> _email Then
                                        If _owemail = "" Then
                                            ' log_message = " Email : " + _email + "." + " "

                                        Else
                                            log_message = " Email : " + _owemail + " " + " To " + " " + _email + "." + " "
                                            GoOwnerList.Add(log_message)
                                        End If

                                    End If

                                    If _ownationality1 <> _nationality1 Then
                                        If _ownationality1 = "" Then
                                            log_message = " Nationality 1 : " + _nationality1 + "." + " "
                                        Else
                                            log_message = " Nationality 1 : " + _ownationality1 + " " + " To " + " " + _nationality1 + "." + " "
                                        End If

                                        GoOwnerList.Add(log_message)

                                    End If
                                    If _owOccType <> _OccType Then
                                        If _owOccType = "" Then
                                            log_message = " Occupation Type : " + _OccType + "." + ""
                                        Else
                                            log_message = " Occupation Type : " + _owOccType + " " + " To " + " " + _OccType + "." + ""
                                        End If

                                        GoOwnerList.Add(log_message)

                                    End If

                                    For Each Goownerloglist As String In GoOwnerList
                                        _goownerLog += Goownerloglist
                                    Next

                                    _golog = " Authorized : Owner Code : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _goownerLog

                                    Logger.system_log(_golog)
                                    _goownerLog = ""
                                    GoOwnerList.Clear()

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

                '----------------------------Mizan Work (23-04-16)---------------------


                ''---------------------Commented By Mizan (23-04-16) ------------------------

                'Dim i As Integer
                'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                'Dim commProc As DbCommand = db.GetStoredProcCommand("GO_OwnerInfo_Auth")

                'For i = 0 To dgView.Rows.Count - 1

                '    If dgView.Rows(i).Cells(0).Value = True Then

                '        commProc.Parameters.Clear()

                '        db.AddInParameter(commProc, "@OWNER_CODE", DbType.String, dgView.Rows(i).Cells(3).Value.ToString())
                '        db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, dgView.Rows(i).Cells(1).Value)
                '        db.AddInParameter(commProc, "@MOD_DATETIME", DbType.DateTime, dgView.Rows(i).Cells(20).Value)

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

                '    log_message = "Authorized Owner " + dgView.Rows(i).Cells(3).Value.ToString() + " Name " + dgView.Rows(i).Cells(4).Value.ToString() + " " + dgView.Rows(i).Cells(5).Value.ToString()
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