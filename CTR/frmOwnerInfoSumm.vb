Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization

Public Class frmOwnerInfoSumm

#Region "user defined codes"

    Dim _formName As String = "MaintenanceOwnerInfoSummary"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""
    Dim _formMode As FormTransMode

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


    Private Sub LoadDataGrid()

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            If chkShowAll.Checked = True Then
                strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
                    " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='1' and [STATUS] = 'D' THEN 'D' " + _
                    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                    "       ELSE 'U' " + _
                    "	End " + _
                    " from FIU_OWNER_INFO " + _
                    " where IS_AUTHORIZED=0 OR [STATUS] in ('L','D')  " + _
                    " order by IS_AUTHORIZED,OWNER_CODE"

            ElseIf rdoAuthorized.Checked = True Then
                strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
                    " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                    "	End " + _
                    " from FIU_OWNER_INFO " + _
                    " where IS_AUTHORIZED=1 AND [STATUS] ='L'  " + _
                    " order by IS_AUTHORIZED,OWNER_CODE"


            Else
                strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,ModNo,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME,  " + _
                " 'S' = " + _
                "	CASE  " + _
                "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
                "	End " + _
                " from FIU_OWNER_INFO " + _
                " where IS_AUTHORIZED=0  " + _
                " order by IS_AUTHORIZED,OWNER_CODE"


                'strSql = "select SLNO,APP_ID,APP_NAME,IS_AUTHORIZED,MODNO, " + _
                '" 'S' = " + _
                '"	CASE  " + _
                '"	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                '"       ELSE 'U' " + _
                '"	End " + _
                '" from APPS " + _
                '" where IS_AUTHORIZED=0 OR STATUS='L' " + _
                '" order by IS_AUTHORIZED,APP_ID"


            End If


            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            dgView.AutoGenerateColumns = False
            dgView.DataSource = ds
            dgView.DataMember = ds.Tables(0).TableName
            lblTotRecNo.Text = ds.Tables(0).Rows.Count

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

#End Region


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Dim frmOwnerInfo As New frmOwnerInfo
        frmOwnerInfo.ShowDialog()

    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try

            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then
                'dgView.SelectedRows.Item(0).Cells(0).Value 
                Dim frmOwnerInfo As New frmOwnerInfo(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
                frmOwnerInfo.ShowDialog()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick
        If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then
            'dgView.SelectedRows.Item(0).Cells(0).Value 
            Dim frmOwnerInfo As New frmOwnerInfo(dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(1).Value)
            frmOwnerInfo.ShowDialog()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()

    End Sub

    Private Sub frmOwnerInfoSumm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        Dim i As Integer
        For i = 1 To dgView.Columns.Count - 1
            dgView.Columns(i).ReadOnly = True
        Next
    End Sub

    'Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress

    '    Try

    '        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

    '        Dim strSql As String

    '        Dim strSearch As String = ""
    '        If txtSearch.Text.Trim() <> "" Then
    '            strSearch = " and OWNER_CODE like '%" & txtSearch.Text.Trim() & "%'"
    '        End If

    '        If chkShowAll.Checked = True Then
    '            strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '                " 'S' = " + _
    '                "	CASE  " + _
    '                "	    WHEN IS_AUTHORIZED='1' and [STATUS] = 'D' THEN 'D' " + _
    '                "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '                "       ELSE 'U' " + _
    '                "	End " + _
    '                " from FIU_OWNER_INFO " + _
    '                " where IS_AUTHORIZED=0 OR [STATUS] in ('L','D') " & strSearch + _
    '                " order by IS_AUTHORIZED,OWNER_CODE"

    '        ElseIf rdoAuthorized.Checked = True Then

    '            strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '            " 'S' = " + _
    '            "	CASE  " + _
    '            "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '            "	End " + _
    '            " from FIU_OWNER_INFO " + _
    '            " where (IS_AUTHORIZED=1 AND [STATUS]='L') " & strSearch + _
    '            " order by IS_AUTHORIZED,OWNER_CODE"

    '        Else
    '            strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '            " 'S' = " + _
    '            "	CASE  " + _
    '            "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
    '            "	End " + _
    '            " from FIU_OWNER_INFO " + _
    '            " where (IS_AUTHORIZED=0 ) " & strSearch + _
    '            " order by IS_AUTHORIZED,OWNER_CODE"


    '        End If


    '        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

    '        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

    '        dgView.AutoGenerateColumns = False
    '        dgView.DataSource = ds
    '        dgView.DataMember = ds.Tables(0).TableName
    '        lblTotRecNo.Text = ds.Tables(0).Rows.Count

    '    Catch ex As Exception

    '        MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    End Try
    'End Sub

    'Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged

    'Try

    '    Dim db As New SqlDatabase(CommonAppSet.ConnStr)

    '    Dim strSql As String
    '    Dim strSearch As String = ""
    '    If txtSearch.Text.Trim() <> "" Then
    '        strSearch = " and OWNER_CODE like '%" & txtSearch.Text.Trim() & "%'"
    '    End If


    '    If chkShowAll.Checked = True Then
    '        strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '            " 'S' = " + _
    '            "	CASE  " + _
    '            "	    WHEN IS_AUTHORIZED='1' and [STATUS] = 'D' THEN 'D' " + _
    '            "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '            "       ELSE 'U' " + _
    '            "	End " + _
    '            " from FIU_OWNER_INFO " + _
    '            " where IS_AUTHORIZED=0 OR [STATUS] in ('L','D') " & strSearch + _
    '            " order by IS_AUTHORIZED,OWNER_CODE"


    '    ElseIf rdoAuthorized.Checked = True Then

    '        strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,ModNo,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '        " 'S' = " + _
    '        "	CASE  " + _
    '        "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '        "	End " + _
    '        " from FIU_OWNER_INFO " + _
    '        " where (IS_AUTHORIZED=1 AND [STATUS]='L') " & strSearch + _
    '        " order by IS_AUTHORIZED,OWNER_CODE"

    '    Else
    '        strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,ModNo,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '        " 'S' = " + _
    '        "	CASE  " + _
    '        "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
    '        "	End " + _
    '        " from FIU_OWNER_INFO " + _
    '        " where IS_AUTHORIZED=0 " & strSearch + _
    '        " order by IS_AUTHORIZED,OWNER_CODE"


    '    End If


    '    Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

    '    Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

    '    dgView.AutoGenerateColumns = False
    '    dgView.DataSource = ds
    '    dgView.DataMember = ds.Tables(0).TableName
    '    lblTotRecNo.Text = ds.Tables(0).Rows.Count

    'Catch ex As Exception

    '    MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

    'End Try

    'End Sub


    'Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
    '    Try

    '        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

    '        Dim strSql As String

    '        Dim strSearch As String = ""
    '        If txtName.Text.Trim() <> "" Then
    '            strSearch = " and OWNER_NAME like '%" & txtName.Text.Trim() & "%'"
    '        End If

    '        If chkShowAll.Checked = True Then
    '            strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '                " 'S' = " + _
    '                "	CASE  " + _
    '                "	    WHEN IS_AUTHORIZED='1' and [STATUS] = 'D' THEN 'D' " + _
    '                "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '                "       ELSE 'U' " + _
    '                "	End " + _
    '                " from FIU_OWNER_INFO " + _
    '                " where IS_AUTHORIZED=0 OR [STATUS] in ('L','D') " & strSearch + _
    '                " order by IS_AUTHORIZED,OWNER_CODE"

    '        ElseIf rdoAuthorized.Checked = True Then

    '            strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,ModNo,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '            " 'S' = " + _
    '            "	CASE  " + _
    '            "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '            "	End " + _
    '            " from FIU_OWNER_INFO " + _
    '            " where (IS_AUTHORIZED=1 AND [STATUS]='L') " & strSearch + _
    '            " order by IS_AUTHORIZED,OWNER_CODE"

    '        Else
    '            strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,ModNo,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '            " 'S' = " + _
    '            "	CASE  " + _
    '            "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
    '            "	End " + _
    '            " from FIU_OWNER_INFO " + _
    '            " where IS_AUTHORIZED=0  " & strSearch + _
    '            " order by IS_AUTHORIZED,OWNER_CODE"


    '        End If


    '        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

    '        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

    '        dgView.AutoGenerateColumns = False
    '        dgView.DataSource = ds
    '        dgView.DataMember = ds.Tables(0).TableName
    '        lblTotRecNo.Text = ds.Tables(0).Rows.Count

    '    Catch ex As Exception

    '        MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    End Try
    'End Sub

    'Private Sub txtName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
    'Try

    '    Dim db As New SqlDatabase(CommonAppSet.ConnStr)

    '    Dim strSql As String
    '    Dim strSearch As String = ""
    '    If txtName.Text.Trim() <> "" Then
    '        strSearch = " and OWNER_NAME like '%" & txtName.Text.Trim() & "%'"
    '    End If
    '    If chkShowAll.Checked = True Then
    '        strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '            " 'S' = " + _
    '            "	CASE  " + _
    '            "	    WHEN IS_AUTHORIZED='1' and [STATUS] = 'D' THEN 'D' " + _
    '            "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '            "       ELSE 'U' " + _
    '            "	End " + _
    '            " from FIU_OWNER_INFO " + _
    '            " where IS_AUTHORIZED=0 OR STATUS in ('L','D') " & strSearch + _
    '            " order by IS_AUTHORIZED,OWNER_CODE"

    '    ElseIf rdoAuthorized.Checked = True Then

    '        strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '        " 'S' = " + _
    '        "	CASE  " + _
    '        "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '        "	End " + _
    '        " from FIU_OWNER_INFO " + _
    '        " where (IS_AUTHORIZED=1 AND [STATUS]='L') " & strSearch + _
    '        " order by IS_AUTHORIZED,OWNER_CODE"


    '    Else
    '        strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '        " 'S' = " + _
    '        "	CASE  " + _
    '        "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
    '        "	End " + _
    '        " from FIU_OWNER_INFO " + _
    '        " where IS_AUTHORIZED=0  " & strSearch + _
    '        " order by IS_AUTHORIZED,OWNER_CODE"


    '    End If


    '    Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

    '    Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

    '    dgView.AutoGenerateColumns = False
    '    dgView.DataSource = ds
    '    dgView.DataMember = ds.Tables(0).TableName
    '    lblTotRecNo.Text = ds.Tables(0).Rows.Count

    'Catch ex As Exception

    '    MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

    'End Try
    'End Sub

    Private Sub rdoAuthorized_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAuthorized.CheckedChanged

        If rdoAuthorized.Checked = True Then
            btnAuthorize.Enabled = False
            chkAll.Visible = False
            If dgView.Columns.Count > 0 Then
                dgView.Columns(0).Visible = False
            End If
            chkShowAll.Visible = True



        ElseIf rdoUnauthorized.Checked = True Then
            If opt.IsAuth = True Then
                btnAuthorize.Enabled = True
            Else
                btnAuthorize.Enabled = False
            End If
            chkAll.Visible = True

            If dgView.Columns.Count > 0 Then
                dgView.Columns(0).Visible = True
            End If
            chkShowAll.Visible = False

        End If

        LoadDataGrid()
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

    '--------------Mizan Work (23-04-16)---------------------

    Private Sub LoadOwnerDataForAuth(ByVal strOwnerCode As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet


            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_OWNER_INFO Where OWNER_CODE='" & strOwnerCode & "' and STATUS ='L' ")

            If ds.Tables(0).Rows.Count > 0 Then


                _formMode = FormTransMode.Update


                _cmboOccp = ds.Tables(0).Rows(0)("OCTYPECODE").ToString()


                _OwName = ds.Tables(0).Rows(0)("OWNER_NAME").ToString

                _ownerSpouse = ds.Tables(0).Rows(0)("OWNER_SPOUSE").ToString

                _ownerDob = NullHelper.DateToString(ds.Tables(0).Rows(0)("DOB"))

                _ownerFather = ds.Tables(0).Rows(0)("OWNER_FATHER").ToString

                _ownerMother = ds.Tables(0).Rows(0)("OWNER_MOTHER").ToString

                _ownerAdd = ds.Tables(0).Rows(0)("PRES_ADDR").ToString
                'cmbPresThana.SelectedValue = ds.Tables(0).Rows(0)("PRES_THANA_CODE")
                _cmbPreseThana = ds.Tables(0).Rows(0)("PRES_THANA_CODE").ToString()

                _ownerPerm = ds.Tables(0).Rows(0)("PERM_ADDR").ToString
                'cmbPermThana.SelectedValue = ds.Tables(0).Rows(0)("PERM_THANA_CODE")
                _cmbPermeThana = ds.Tables(0).Rows(0)("PERM_THANA_CODE").ToString()

                _ownerMobile = ds.Tables(0).Rows(0)("MOBILE1").ToString

                _ownerPPNO = ds.Tables(0).Rows(0)("PPNO").ToString

                _ownerDLN = ds.Tables(0).Rows(0)("DRIVINGLNO").ToString

                _ownerTIN = ds.Tables(0).Rows(0)("TIN").ToString

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

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--------------Mizan Work (23-04-16)---------------------

    Private Sub LoadOwnerData(ByVal strOwnerCode As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet


            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_OWNER_INFO Where OWNER_CODE='" & strOwnerCode & "' and MODNO=" & intMod.ToString())



            If ds.Tables(0).Rows.Count > 0 Then

                _formMode = FormTransMode.Update


                _cmbOccp = ds.Tables(0).Rows(0)("OCTYPECODE").ToString()
                _OwnerName = ds.Tables(0).Rows(0)("OWNER_NAME").ToString

                _owSpouse = ds.Tables(0).Rows(0)("OWNER_SPOUSE").ToString

                _owDob = NullHelper.DateToString(ds.Tables(0).Rows(0)("DOB"))

                _owFather = ds.Tables(0).Rows(0)("OWNER_FATHER").ToString

                _owMother = ds.Tables(0).Rows(0)("OWNER_MOTHER").ToString

                _owAdd = ds.Tables(0).Rows(0)("PRES_ADDR").ToString

                _cmboPresThana = ds.Tables(0).Rows(0)("PRES_THANA_CODE").ToString()

                _owPerm = ds.Tables(0).Rows(0)("PERM_ADDR").ToString

                _cmboPermThana = ds.Tables(0).Rows(0)("PERM_THANA_CODE").ToString()

                _owMobile = ds.Tables(0).Rows(0)("MOBILE1").ToString

                _owPPNO = ds.Tables(0).Rows(0)("PPNO").ToString

                _owDLN = ds.Tables(0).Rows(0)("DRIVINGLNO").ToString

                _owTIN = ds.Tables(0).Rows(0)("TIN").ToString
                _owBIN = ds.Tables(0).Rows(0)("BIN").ToString

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
               

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function AuthorizeData(ByVal i As Integer) As TransState

        ''-----------------------Mizan Work (23-04-16) ---------------------------

        LoadOwnerData(dgView.Rows(i).Cells(3).Value.ToString(), dgView.Rows(i).Cells(1).Value.ToString())

        If dgView.Rows(i).Cells(1).Value > 1 Then

            LoadOwnerDataForAuth(dgView.Rows(i).Cells(3).Value.ToString())

            Dim tStatus As TransState

            Dim strSql As String
            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                strSql = "select IS_AUTHORIZED,STATUS from FIU_OWNER_INFO where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where  OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()


                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If dgView.Rows(i).Cells(1).Value > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_OWNER_INFO set STATUS = 'C' " & _
                                " where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_OWNER_INFO set STATUS = 'O' " & _
                                " where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

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
                            log_message = " Occupation Type : " + _cmboOccp + " " + " To " + " " + _cmbOccp + "." + " "
                            OwnerList.Add(log_message)
                        End If

                        For Each ownerloglist As String In OwnerList
                            _ownerLog += ownerloglist
                        Next

                        _log = " Authorized : Owner Code : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + _ownerLog

                        Logger.system_log(_log)
                        _ownerLog = ""
                        OwnerList.Clear()

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

                strSql = "select IS_AUTHORIZED,STATUS from FIU_OWNER_INFO where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where  OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()


                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If dgView.Rows(i).Cells(1).Value > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_OWNER_INFO set STATUS = 'C' " & _
                                " where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_OWNER_INFO set STATUS = 'O' " & _
                                " where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

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
                            Else
                                log_message = " Occupation Type : " + _cmboOccp + " " + " To " + " " + _cmbOccp + "." + " "
                                OwnerList.Add(log_message)
                            End If


                        End If

                        For Each ownerloglist As String In OwnerList
                            _ownerLog += ownerloglist
                        Next

                        _log = " Authorized : Owner Code : " + dgView.Rows(i).Cells(3).Value.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _ownerLog

                        Logger.system_log(_log)
                        _ownerLog = ""
                        OwnerList.Clear()

                    End If
                Else
                    tStatus = TransState.UpdateNotPossible
                End If

                trans.Commit()

                


            End Using

            Return tStatus

        End If

        ''-----------------------Mizan Work (23-04-16) ---------------------------


        ''-------------------Commented By Mizan (23-04-16)------------------------


        'Dim tStatus As TransState

        'Dim strSql As String
        'tStatus = TransState.UnspecifiedError

        'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        'Using conn As DbConnection = db.CreateConnection()

        '    conn.Open()

        '    Dim trans As DbTransaction = conn.BeginTransaction()

        '    strSql = "select IS_AUTHORIZED,STATUS from FIU_OWNER_INFO where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

        '    Dim ds As New DataSet

        '    ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

        '    If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

        '        If ds.Tables(0).Rows(0)("STATUS") = "U" Then
        '            strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
        '            "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
        '            " where  OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()


        '        ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
        '            strSql = "update FIU_OWNER_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
        '            "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
        '            " where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

        '        End If

        '        Dim result As Integer
        '        result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

        '        If result <= 0 Then

        '            tStatus = TransState.NoRecord

        '        ElseIf result > 0 Then

        '            If dgView.Rows(i).Cells(1).Value > 1 Then

        '                'if previous modification status is D(Deleted) then make it C(Closed)
        '                strSql = "update FIU_OWNER_INFO set STATUS = 'C' " & _
        '                    " where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
        '                    " and STATUS ='D'"

        '                db.ExecuteNonQuery(trans, CommandType.Text, strSql)

        '                'if previous modification status is L(Deleted) then make it O(Open)
        '                strSql = "update FIU_OWNER_INFO set STATUS = 'O' " & _
        '                    " where OWNER_CODE='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
        '                    " and STATUS ='L'"

        '                db.ExecuteNonQuery(trans, CommandType.Text, strSql)



        '            End If
        '            tStatus = TransState.Update
        '        End If
        '    Else
        '        tStatus = TransState.UpdateNotPossible
        '    End If



        '    trans.Commit()
        '    log_message = "Authorized Owner Code " + dgView.Rows(i).Cells(3).Value.ToString()
        '    Logger.system_log(log_message)

        'End Using

        'Return tStatus

    End Function


    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        'If dgView.Rows(i).Cells(5).Value = CommonAppSet.User.Trim() Then
        '    MessageBox.Show("Maker can't verify data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub

        'End If


        Try
            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim i As Integer
                For i = 0 To dgView.Rows.Count - 1

                    If dgView.Rows(i).Cells(0).Value = True Then
                        If dgView.Rows(i).Cells(5).Value.ToString = CommonAppSet.User.Trim() Then


                            MessageBox.Show("Maker can't verify data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            tState = AuthorizeData(i)
                        End If


                    End If
                Next i


                If tState = TransState.Update Then
                    'LoadAppData(_intSlno, _intModno)
                    lblToolStatus.Text = "!! Authorized Successfully !!"
                    btnAuthorize.Enabled = False

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
        LoadDataGrid()
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim strSql As String
                Dim strSearch As String = ""
                If txtSearch.Text.Trim() <> "" Then
                    strSearch = " and OWNER_CODE like '%" & txtSearch.Text.Trim() & "%'"
                End If


                If chkShowAll.Checked = True Then
                    strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
                        " 'S' = " + _
                        "	CASE  " + _
                        "	    WHEN IS_AUTHORIZED='1' and [STATUS] = 'D' THEN 'D' " + _
                        "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                        "       ELSE 'U' " + _
                        "	End " + _
                        " from FIU_OWNER_INFO " + _
                        " where IS_AUTHORIZED=0 OR [STATUS] in ('L','D') " & strSearch + _
                        " order by IS_AUTHORIZED,OWNER_CODE"


                ElseIf rdoAuthorized.Checked = True Then

                    strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,ModNo,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
                    " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                    "	End " + _
                    " from FIU_OWNER_INFO " + _
                    " where (IS_AUTHORIZED=1 AND [STATUS]='L') " & strSearch + _
                    " order by IS_AUTHORIZED,OWNER_CODE"

                Else
                    strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,ModNo,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
                    " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
                    "	End " + _
                    " from FIU_OWNER_INFO " + _
                    " where IS_AUTHORIZED=0 " & strSearch + _
                    " order by IS_AUTHORIZED,OWNER_CODE"


                End If


                Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                dgView.AutoGenerateColumns = False
                dgView.DataSource = ds
                dgView.DataMember = ds.Tables(0).TableName
                lblTotRecNo.Text = ds.Tables(0).Rows.Count

            Catch ex As Exception

                MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        End If


    End Sub

    Private Sub txtName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim strSql As String
                Dim strSearch As String = ""
                If txtName.Text.Trim() <> "" Then
                    strSearch = " and OWNER_NAME like '%" & txtName.Text.Trim() & "%'"
                End If
                If chkShowAll.Checked = True Then
                    strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
                        " 'S' = " + _
                        "	CASE  " + _
                        "	    WHEN IS_AUTHORIZED='1' and [STATUS] = 'D' THEN 'D' " + _
                        "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                        "       ELSE 'U' " + _
                        "	End " + _
                        " from FIU_OWNER_INFO " + _
                        " where IS_AUTHORIZED=0 OR STATUS in ('L','D') " & strSearch + _
                        " order by IS_AUTHORIZED,OWNER_CODE"

                ElseIf rdoAuthorized.Checked = True Then

                    strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
                    " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                    "	End " + _
                    " from FIU_OWNER_INFO " + _
                    " where (IS_AUTHORIZED=1 AND [STATUS]='L') " & strSearch + _
                    " order by IS_AUTHORIZED,OWNER_CODE"


                Else
                    strSql = "select OWNER_CODE, OWNER_NAME,IS_AUTHORIZED,MODNO,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
                    " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
                    "	End " + _
                    " from FIU_OWNER_INFO " + _
                    " where IS_AUTHORIZED=0  " & strSearch + _
                    " order by IS_AUTHORIZED,OWNER_CODE"


                End If


                Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                dgView.AutoGenerateColumns = False
                dgView.DataSource = ds
                dgView.DataMember = ds.Tables(0).TableName
                lblTotRecNo.Text = ds.Tables(0).Rows.Count

            Catch ex As Exception

                MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try
        End If

    End Sub
End Class