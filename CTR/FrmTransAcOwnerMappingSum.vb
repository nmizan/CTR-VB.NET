Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization

Public Class FrmTransAcOwnerMappingSum

    Dim _formName As String = "MaintenanceAcNumberMapSummary"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""



#Region "user defined codes"

    Private Sub LoadDataGrid()

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            If chkShowAll.Checked = True Then
                strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED, STATUS,INPUT_BY, " + _
                    " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='1' and STATUS = 'D' THEN 'D' " + _
                    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                    "	End " + _
                    " from FIU_TRANS_AC_OWNER  " + _
                    " where IS_AUTHORIZED=0 OR STATUS in ('L','D')  " + _
                    " order by IS_AUTHORIZED,AcNumber"


            ElseIf rdoAuthorized.Checked = True Then

                strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED, STATUS,INPUT_BY, " + _
                    " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                    "	End " + _
                    " from FIU_TRANS_AC_OWNER  " + _
                    " where IS_AUTHORIZED=1 AND STATUS ='L'  " + _
                    " order by IS_AUTHORIZED,AcNumber"


            Else
                strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED,STATUS,INPUT_BY, " + _
                " 'S' = " + _
                "	CASE  " + _
                "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
                "	End " + _
                " from FIU_TRANS_AC_OWNER  " + _
                " where IS_AUTHORIZED=0 " + _
                " order by IS_AUTHORIZED,AcNumber"


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

    Private Sub LoadDataGrid2()

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String


            strSql = "SELECT taw.MODNO,taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,taw.BANK_CODE,taw.BRANCH_CODE,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                     " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                     " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                     " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                     " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                     " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                     " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                     " ow.TIN, ow.BIN " + _
                     " FROM FIU_TRANS_AC_OWNER taw " + _
                     " inner join FIU_OWNER_INFO ow ON (ow.OWNER_CODE = taw.OWNER_CODE and ow .STATUS = 'L') " + _
                     " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                     " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                     " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                     " WHERE taw.STATUS ='L' ORDER BY taw.ACNUMBER "




            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            dgView1.AutoGenerateColumns = True
            dgView1.DataSource = ds
            dgView1.DataMember = ds.Tables(0).TableName
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

        Dim frmAccOwnerMap As New FrmTransAcOwnerMapping
        frmAccOwnerMap.ShowDialog()

    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try

            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then
                'dgView.SelectedRows.Item(0).Cells(0).Value 
                Dim frmAccOwnerMap As New FrmTransAcOwnerMapping(dgView.SelectedRows.Item(0).Cells(1).Value, dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(4).Value, dgView.SelectedRows.Item(0).Cells(5).Value)
                frmAccOwnerMap.ShowDialog()
            End If

        Catch ex As Exception

        End Try
    End Sub

   

    'Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then
    '        'dgView.SelectedRows.Item(0).Cells(0).Value 
    '        Dim frmAccOwnerMap As New FrmTransAcOwnerMapping(dgView.SelectedRows.Item(0).Cells(1).Value, dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(4).Value, dgView.SelectedRows.Item(0).Cells(5).Value)
    '        frmAccOwnerMap.ShowDialog()
    '    End If
    'End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()
        LoadDataGrid2()
    End Sub

    Private Sub FrmTransAcOwnerMappingSum_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        Dim i As Integer
        For i = 1 To dgView.Columns.Count - 1
            dgView.Columns(i).ReadOnly = True
        Next

        Dim j As Integer
        For j = 1 To dgView1.Columns.Count - 1
            dgView1.Columns(j).ReadOnly = True
        Next

    End Sub

    'Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub

    
    
    Private Function AuthorizeData(ByVal i As Integer) As TransState

        Dim tStatus As TransState

        Dim strSql As String
        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Using conn As DbConnection = db.CreateConnection()

            conn.Open()

            Dim trans As DbTransaction = conn.BeginTransaction()

            strSql = "select IS_AUTHORIZED,STATUS from FIU_TRANS_AC_OWNER where Bank_Code='" & dgView.Rows(i).Cells(4).Value & "' and Branch_Code='" & dgView.Rows(i).Cells(5).Value & "' and AcNumber='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

            If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                    strSql = "update FIU_TRANS_AC_OWNER set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                    "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                    " where Bank_Code='" & dgView.Rows(i).Cells(4).Value & "' and Branch_Code='" & dgView.Rows(i).Cells(5).Value & "' and AcNumber='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

                ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                    strSql = "update FIU_TRANS_AC_OWNER set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                    "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                    " where Bank_Code='" & dgView.Rows(i).Cells(4).Value & "' and Branch_Code='" & dgView.Rows(i).Cells(5).Value & "' and AcNumber='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

                End If

                Dim result As Integer
                result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                If result <= 0 Then

                    tStatus = TransState.NoRecord

                ElseIf result > 0 Then

                    If dgView.Rows(i).Cells(1).Value > 1 Then

                        'if previous modification status is D(Deleted) then make it C(Closed)
                        strSql = "update FIU_TRANS_AC_OWNER set STATUS = 'C' " & _
                            " where Bank_Code='" & dgView.Rows(i).Cells(4).Value & "' and Branch_Code='" & dgView.Rows(i).Cells(5).Value & "' and AcNumber='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
                            " and STATUS ='D'"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                        'if previous modification status is L(Deleted) then make it O(Open)
                        strSql = "update FIU_TRANS_AC_OWNER set STATUS = 'O' " & _
                            " where Bank_Code='" & dgView.Rows(i).Cells(4).Value & "' and Branch_Code='" & dgView.Rows(i).Cells(5).Value & "' and AcNumber='" & dgView.Rows(i).Cells(3).Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
                            " and STATUS ='L'"

                        db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                    End If
                    tStatus = TransState.Update
                End If
            Else
                tStatus = TransState.UpdateNotPossible
            End If

            trans.Commit()

            log_message = " Authorized : TransAccountOwner Mapping Account Code : " + dgView.Rows(i).Cells(3).Value.ToString()
            Logger.system_log(log_message)

        End Using

        Return tStatus

    End Function

    
    Private Sub dgView_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellContentDoubleClick
        If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then
            '        'dgView.SelectedRows.Item(0).Cells(0).Value 
            Dim frmAccOwnerMap As New FrmTransAcOwnerMapping(dgView.SelectedRows.Item(0).Cells(1).Value, dgView.SelectedRows.Item(0).Cells(3).Value, dgView.SelectedRows.Item(0).Cells(4).Value, dgView.SelectedRows.Item(0).Cells(5).Value)
            frmAccOwnerMap.ShowDialog()
        End If
    End Sub

    Private Sub txtAccountNumber_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccountNumber.KeyDown

        If e.KeyCode = Keys.Enter Then

            Try


                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim strSql As String

                Dim strSearch As String = ""
                If txtAccountNumber.Text.Trim() <> "" Then
                    strSearch = " and taw.ACNUMBER like '%" & txtAccountNumber.Text.Trim() & "%'"
                End If
                If txtAccountNumber.Text.Trim() <> "" Then

                    strSql = "SELECT taw.MODNO,taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,taw.BANK_CODE,taw.BRANCH_CODE,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                        " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                        " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                        " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                        " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                        " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                        " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                        " ow.TIN, ow.BIN " + _
                        " FROM FIU_TRANS_AC_OWNER taw " + _
                        " inner join FIU_OWNER_INFO ow ON (ow.OWNER_CODE = taw.OWNER_CODE and ow .STATUS = 'L') " + _
                        " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                        " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                        " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                        " WHERE taw.STATUS ='L' " & strSearch + _
                        " ORDER BY taw.ACNUMBER "


                Else

                    strSql = "SELECT taw.MODNO,taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,taw.BANK_CODE,taw.BRANCH_CODE,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                            " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                            " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                            " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                            " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                            " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                            " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                            " ow.TIN, ow.BIN " + _
                            " FROM FIU_TRANS_AC_OWNER taw " + _
                            " inner join FIU_OWNER_INFO ow ON (ow.OWNER_CODE = taw.OWNER_CODE and ow .STATUS = 'L') " + _
                            " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                            " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                            " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                            " WHERE taw.STATUS ='L' ORDER BY taw.ACNUMBER "


                End If


                Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                dgView1.AutoGenerateColumns = True
                dgView1.DataSource = ds
                dgView1.DataMember = ds.Tables(0).TableName
                lblTotRecNo.Text = ds.Tables(0).Rows.Count
            Catch ex As Exception

                MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        End If
    End Sub


    Private Sub txtOwnerCode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOwnerCode.KeyDown
        If e.KeyCode = Keys.Enter Then

            Try


                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim strSql As String

                Dim strSearch As String = ""
                If txtOwnerCode.Text.Trim() <> "" Then
                    strSearch = " and ow.OWNER_CODE like '%" & txtOwnerCode.Text.Trim() & "%'"
                End If
                If txtOwnerCode.Text.Trim() <> "" Then

                    strSql = "SELECT taw.MODNO,taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,taw.BANK_CODE,taw.BRANCH_CODE,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                       " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                       " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                       " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                       " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                       " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                       " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                       " ow.TIN, ow.BIN " + _
                       " FROM FIU_TRANS_AC_OWNER taw " + _
                       " inner join FIU_OWNER_INFO ow ON (ow.OWNER_CODE = taw.OWNER_CODE and ow .STATUS = 'L') " + _
                       " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                       " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                       " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                       " WHERE taw.STATUS ='L' " & strSearch + _
                       " ORDER BY taw.ACNUMBER "


                Else

                    strSql = "SELECT taw.MODNO,taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,taw.BANK_CODE,taw.BRANCH_CODE,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                           " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                           " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                           " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                           " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                           " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                           " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                           " ow.TIN, ow.BIN " + _
                           " FROM FIU_TRANS_AC_OWNER taw " + _
                           " inner join FIU_OWNER_INFO ow ON (ow.OWNER_CODE = taw.OWNER_CODE and ow .STATUS = 'L') " + _
                           " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                           " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                           " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                           " WHERE taw.STATUS ='L' ORDER BY taw.ACNUMBER "


                End If


                Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                dgView1.AutoGenerateColumns = True
                dgView1.DataSource = ds
                dgView1.DataMember = ds.Tables(0).TableName
                lblTotRecNo.Text = ds.Tables(0).Rows.Count
            Catch ex As Exception

                MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        End If

    End Sub

    Private Sub txtOwnerName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOwnerName.TextChanged
        'Try


        '    Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        '    Dim strSql As String

        '    Dim strSearch As String = ""
        '    If txtOwnerName.Text.Trim() <> "" Then
        '        strSearch = " and ow.OWNER_NAME like '%" & txtOwnerName.Text.Trim() & "%'"
        '    End If
        '    If txtOwnerName.Text.Trim() <> "" Then

        '        strSql = "SELECT taw.MODNO,taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,taw.BANK_CODE,taw.BRANCH_CODE,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
        '                " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
        '                " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
        '                " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
        '                " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
        '                " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
        '                " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
        '                " ow.TIN, ow.BIN " + _
        '                " FROM FIU_TRANS_AC_OWNER taw " + _
        '                " inner join FIU_OWNER_INFO ow ON (ow.OWNER_CODE = taw.OWNER_CODE and ow .STATUS = 'L') " + _
        '                " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
        '                " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
        '                " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
        '                " WHERE taw.STATUS ='L' " & strSearch + _
        '                " ORDER BY taw.ACNUMBER "


        '    Else

        '        strSql = "SELECT taw.MODNO,taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,taw.BANK_CODE,taw.BRANCH_CODE,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
        '                    " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
        '                    " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
        '                    " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
        '                    " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
        '                    " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
        '                    " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
        '                    " ow.TIN, ow.BIN " + _
        '                    " FROM FIU_TRANS_AC_OWNER taw " + _
        '                    " inner join FIU_OWNER_INFO ow ON (ow.OWNER_CODE = taw.OWNER_CODE and ow .STATUS = 'L') " + _
        '                    " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
        '                    " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
        '                    " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
        '                    " WHERE taw.STATUS ='L' ORDER BY taw.ACNUMBER "

        '    End If


        '    Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        '    Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        '    dgView1.AutoGenerateColumns = True
        '    dgView1.DataSource = ds
        '    dgView1.DataMember = ds.Tables(0).TableName
        '    lblTotRecNo.Text = ds.Tables(0).Rows.Count
        'Catch ex As Exception

        '    MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        'End Try

    End Sub

    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        'If lblInputBy.Text.Trim() = CommonAppSet.User.Trim() Then
        '    MessageBox.Show("Maker can't verify data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub

        'End If


        Try
            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim i As Integer
                For i = 0 To dgView.Rows.Count - 1

                    If dgView.Rows(i).Cells(0).Value = True Then

                        If dgView.Rows(i).Cells(6).Value.ToString() = CommonAppSet.User.Trim() Then
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

    
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged

        'Try

        '    Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        '    Dim strSql As String

        '    Dim strSearch As String = ""

        '    If txtSearch.Text.Trim() <> "" Then
        '        strSearch = " and ACNUMBER like '%" & txtSearch.Text.Trim() & "%'"
        '    End If

        '    If chkShowAll.Checked = True Then
        '        strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED, STATUS,INPUT_BY, " + _
        '            " 'S' = " + _
        '            "	CASE  " + _
        '            "	    WHEN IS_AUTHORIZED='1' and STATUS = 'D' THEN 'D' " + _
        '            "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
        '            "       ELSE 'U' " + _
        '            "	End " + _
        '            " from FIU_TRANS_AC_OWNER " + _
        '            " where IS_AUTHORIZED=0 OR STATUS in ('L','D') " & strSearch + _
        '            " order by IS_AUTHORIZED,AcNumber"



        '    ElseIf rdoAuthorized.Checked = True Then


        '        strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED, STATUS,INPUT_BY, " + _
        '           " 'S' = " + _
        '           "	CASE  " + _
        '           "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
        '           "	End " + _
        '           " from FIU_TRANS_AC_OWNER  " + _
        '           " where IS_AUTHORIZED=1 AND STATUS ='L' " & strSearch + _
        '           " order by IS_AUTHORIZED,AcNumber"


        '    Else

        '        strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED, STATUS,INPUT_BY, " + _
        '        " 'S' = " + _
        '        "	CASE  " + _
        '        "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
        '        "	End " + _
        '        " from FIU_TRANS_AC_OWNER  " + _
        '        " where IS_AUTHORIZED=0 " & strSearch + _
        '        " order by IS_AUTHORIZED,AcNumber"


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
    End Sub

    'Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
    '    Try

    '        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

    '        Dim strSql As String
    '        Dim strSearch As String = ""

    '        If txtSearch.Text.Trim() <> "" Then
    '            strSearch = " and ACNUMBER like '%" & txtSearch.Text.Trim() & "%'"
    '        End If

    '        If chkShowAll.Checked = True Then
    '            strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED, STATUS,INPUT_BY, " + _
    '                " 'S' = " + _
    '                "	CASE  " + _
    '                "	    WHEN IS_AUTHORIZED='1' and STATUS = 'D' THEN 'D' " + _
    '                "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '                "       ELSE 'U' " + _
    '                "	End " + _
    '                " from FIU_TRANS_AC_OWNER  " + _
    '                " where IS_AUTHORIZED=0 OR STATUS in ('L','D') " & strSearch + _
    '                " order by IS_AUTHORIZED,AcNumber"

    '        ElseIf rdoAuthorized.Checked = True Then


    '            strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED, STATUS,INPUT_BY, " + _
    '               " 'S' = " + _
    '               "	CASE  " + _
    '               "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '               "	End " + _
    '               " from FIU_TRANS_AC_OWNER  " + _
    '               " where IS_AUTHORIZED=1 AND STATUS ='L' " & strSearch + _
    '               " order by IS_AUTHORIZED,AcNumber"


    '        Else

    '            strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED, STATUS,INPUT_BY, " + _
    '            " 'S' = " + _
    '            "	CASE  " + _
    '            "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
    '            "	End " + _
    '            " from FIU_TRANS_AC_OWNER  " + _
    '            " where IS_AUTHORIZED=0 " & strSearch + _
    '            " order by IS_AUTHORIZED,AcNumber"

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

    Private Sub dgView1_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView1.CellContentDoubleClick
        If Not (dgView1.SelectedRows.Item(0).Cells(0).Value Is Nothing Or dgView1.SelectedRows.Item(0).Cells(0).Value Is DBNull.Value) Then
            '        'dgView.SelectedRows.Item(0).Cells(0).Value 
            Dim frmAccOwnerMap As New FrmTransAcOwnerMapping(dgView1.SelectedRows.Item(0).Cells(0).Value, dgView1.SelectedRows.Item(0).Cells(1).Value, dgView1.SelectedRows.Item(0).Cells(4).Value, dgView1.SelectedRows.Item(0).Cells(5).Value)
            frmAccOwnerMap.ShowDialog()
        End If
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then

            Try

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim strSql As String

                Dim strSearch As String = ""

                If txtSearch.Text.Trim() <> "" Then
                    strSearch = " and ACNUMBER like '%" & txtSearch.Text.Trim() & "%'"
                End If

                If chkShowAll.Checked = True Then
                    strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED, STATUS,INPUT_BY, " + _
                        " 'S' = " + _
                        "	CASE  " + _
                        "	    WHEN IS_AUTHORIZED='1' and STATUS = 'D' THEN 'D' " + _
                        "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                        "       ELSE 'U' " + _
                        "	End " + _
                        " from FIU_TRANS_AC_OWNER " + _
                        " where IS_AUTHORIZED=0 OR STATUS in ('L','D') " & strSearch + _
                        " order by IS_AUTHORIZED,AcNumber"



                ElseIf rdoAuthorized.Checked = True Then


                    strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED, STATUS,INPUT_BY, " + _
                       " 'S' = " + _
                       "	CASE  " + _
                       "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                       "	End " + _
                       " from FIU_TRANS_AC_OWNER  " + _
                       " where IS_AUTHORIZED=1 AND STATUS ='L' " & strSearch + _
                       " order by IS_AUTHORIZED,AcNumber"


                Else

                    strSql = "select Distinct BANK_CODE, BRANCH_CODE, ACNUMBER,MODNO,IS_AUTHORIZED, STATUS,INPUT_BY, " + _
                    " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
                    "	End " + _
                    " from FIU_TRANS_AC_OWNER  " + _
                    " where IS_AUTHORIZED=0 " & strSearch + _
                    " order by IS_AUTHORIZED,AcNumber"


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

    Private Sub txtOwnerName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOwnerName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try


                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim strSql As String

                Dim strSearch As String = ""
                If txtOwnerName.Text.Trim() <> "" Then
                    strSearch = " and ow.OWNER_NAME like '%" & txtOwnerName.Text.Trim() & "%'"
                End If
                If txtOwnerName.Text.Trim() <> "" Then

                    strSql = "SELECT taw.MODNO,taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,taw.BANK_CODE,taw.BRANCH_CODE,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                            " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                            " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                            " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                            " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                            " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                            " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                            " ow.TIN, ow.BIN " + _
                            " FROM FIU_TRANS_AC_OWNER taw " + _
                            " inner join FIU_OWNER_INFO ow ON (ow.OWNER_CODE = taw.OWNER_CODE and ow .STATUS = 'L') " + _
                            " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                            " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                            " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                            " WHERE taw.STATUS ='L' " & strSearch + _
                            " ORDER BY taw.ACNUMBER "


                Else

                    strSql = "SELECT taw.MODNO,taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,taw.BANK_CODE,taw.BRANCH_CODE,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                                " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                                " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                                " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                                " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                                " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                                " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                                " ow.TIN, ow.BIN " + _
                                " FROM FIU_TRANS_AC_OWNER taw " + _
                                " inner join FIU_OWNER_INFO ow ON (ow.OWNER_CODE = taw.OWNER_CODE and ow .STATUS = 'L') " + _
                                " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                                " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                                " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                                " WHERE taw.STATUS ='L' ORDER BY taw.ACNUMBER "

                End If


                Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                dgView1.AutoGenerateColumns = True
                dgView1.DataSource = ds
                dgView1.DataMember = ds.Tables(0).TableName
                lblTotRecNo.Text = ds.Tables(0).Rows.Count
            Catch ex As Exception

                MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        End If
    End Sub
End Class