Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization




Public Class FrmReportOwnerInfo

#Region "Global Variables"

    Dim _formName As String = "MaintenanceReportOwnerInfo"
    Dim opt As SecForm = New SecForm(_formName)


    Dim _intModno As Integer = 0
    Dim log_message As String = ""
#End Region
#Region "user defined codes"

    Private Sub LoadDataGrid()

        If dgView.Columns.Count = 0 Then Exit Sub

        Try

            ' dgView.DataSource = Nothing


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            strSql = "SELECT taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                     " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                     " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                     " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                     " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                     " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                     " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                     " ow.TIN, ow.BIN " + _
                     " FROM FIU_OWNER_INFO ow " + _
                     " Inner join FIU_TRANS_AC_OWNER taw ON (taw.OWNER_CODE = ow.OWNER_CODE and taw .STATUS = 'L') " + _
                     " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                     " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                     " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                     " WHERE ow.STATUS ='L' ORDER BY taw.ACNUMBER "



            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            dgView.AutoGenerateColumns = True
            dgView.DataSource = ds
            dgView.DataMember = ds.Tables(0).TableName


        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

#End Region

    Private Sub FrmReportOwnerInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        Dim i As Integer
        For i = 1 To dgView.Columns.Count - 1
            dgView.Columns(i).ReadOnly = True
        Next
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objExp As New ExportUtil(dgView)

        objExp.ExportXl()
    End Sub

    'Private Sub txtAccountNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAccountNumber.TextChanged
    '    Try


    '        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

    '        Dim strSql As String

    '        Dim strSearch As String = ""
    '        If txtAccountNumber.Text.Trim() <> "" Then
    '            strSearch = " and ACNUMBER like '%" & txtAccountNumber.Text.Trim() & "%'"
    '        End If
    '        If txtAccountNumber.Text.Trim() <> "" Then

    '            strSql = "SELECT taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
    '                " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
    '                " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
    '                " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
    '                " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
    '                " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
    '                " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
    '                " ow.TIN, ow.BIN " + _
    '                " FROM FIU_OWNER_INFO ow " + _
    '                " Inner join FIU_TRANS_AC_OWNER taw ON (taw.OWNER_CODE = ow.OWNER_CODE and taw .STATUS = 'L') " + _
    '                " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
    '                " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
    '                " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
    '                " WHERE ow.STATUS ='L' " & strSearch + _
    '                " ORDER BY taw.ACNUMBER "


    '        Else

    '            strSql = "SELECT taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
    '                 " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
    '                 " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
    '                 " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
    '                 " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
    '                 " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
    '                 " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
    '                 " ow.TIN, ow.BIN " + _
    '                 " FROM FIU_OWNER_INFO ow " + _
    '                 " Inner join FIU_TRANS_AC_OWNER taw ON (taw.OWNER_CODE = ow.OWNER_CODE and taw .STATUS = 'L') " + _
    '                 " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
    '                 " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
    '                 " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
    '                 " WHERE ow.STATUS ='L' ORDER BY taw.ACNUMBER "


    '        End If


    '        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

    '        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

    '        dgView.AutoGenerateColumns = True
    '        dgView.DataSource = ds
    '        dgView.DataMember = ds.Tables(0).TableName

    '    Catch ex As Exception

    '        MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    End Try
    'End Sub

   

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

                    strSql = "SELECT taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                        " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                        " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                        " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                        " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                        " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                        " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                        " ow.TIN, ow.BIN " + _
                        " FROM FIU_OWNER_INFO ow " + _
                        " Inner join FIU_TRANS_AC_OWNER taw ON (taw.OWNER_CODE = ow.OWNER_CODE and taw .STATUS = 'L') " + _
                        " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                        " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                        " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                        " WHERE ow.STATUS ='L' " & strSearch + _
                        " ORDER BY taw.ACNUMBER "


                Else

                    strSql = "SELECT taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                         " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                         " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                         " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                         " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                         " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                         " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                         " ow.TIN, ow.BIN " + _
                         " FROM FIU_OWNER_INFO ow " + _
                         " Inner join FIU_TRANS_AC_OWNER taw ON (taw.OWNER_CODE = ow.OWNER_CODE and taw .STATUS = 'L') " + _
                         " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                         " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                         " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                         " WHERE ow.STATUS ='L' ORDER BY taw.ACNUMBER "


                End If


                Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                dgView.AutoGenerateColumns = True
                dgView.DataSource = ds
                dgView.DataMember = ds.Tables(0).TableName

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

                    strSql = "SELECT taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                        " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                        " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                        " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                        " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                        " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                        " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                        " ow.TIN, ow.BIN " + _
                        " FROM FIU_OWNER_INFO ow " + _
                        " Inner join FIU_TRANS_AC_OWNER taw ON (taw.OWNER_CODE = ow.OWNER_CODE and taw .STATUS = 'L') " + _
                        " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                        " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                        " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                        " WHERE ow.STATUS ='L' " & strSearch + _
                        " ORDER BY taw.ACNUMBER "


                Else

                    strSql = "SELECT taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                         " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                         " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                         " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                         " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                         " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                         " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                         " ow.TIN, ow.BIN " + _
                         " FROM FIU_OWNER_INFO ow " + _
                         " Inner join FIU_TRANS_AC_OWNER taw ON (taw.OWNER_CODE = ow.OWNER_CODE and taw .STATUS = 'L') " + _
                         " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                         " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                         " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                         " WHERE ow.STATUS ='L' ORDER BY taw.ACNUMBER "


                End If


                Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                dgView.AutoGenerateColumns = True
                dgView.DataSource = ds
                dgView.DataMember = ds.Tables(0).TableName

            Catch ex As Exception

                MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        End If

    End Sub

    Private Sub txtOwnerName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOwnerName.TextChanged
        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            Dim strSearch As String = ""
            If txtOwnerName.Text.Trim() <> "" Then
                strSearch = " and ow.OWNER_NAME like '%" & txtOwnerName.Text.Trim() & "%'"
            End If
            If txtOwnerName.Text.Trim() <> "" Then

                strSql = "SELECT taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                    " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                    " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                    " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                    " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                    " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                    " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                    " ow.TIN, ow.BIN " + _
                    " FROM FIU_OWNER_INFO ow " + _
                    " Inner join FIU_TRANS_AC_OWNER taw ON (taw.OWNER_CODE = ow.OWNER_CODE and taw .STATUS = 'L') " + _
                    " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                    " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                    " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                    " WHERE ow.STATUS ='L' " & strSearch + _
                    " ORDER BY taw.ACNUMBER "


            Else

                strSql = "SELECT taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                     " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                     " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                     " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                     " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                     " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                     " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                     " ow.TIN, ow.BIN " + _
                     " FROM FIU_OWNER_INFO ow " + _
                     " Inner join FIU_TRANS_AC_OWNER taw ON (taw.OWNER_CODE = ow.OWNER_CODE and taw .STATUS = 'L') " + _
                     " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                     " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                     " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                     " WHERE ow.STATUS ='L' ORDER BY taw.ACNUMBER "


            End If


            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            dgView.AutoGenerateColumns = True
            dgView.DataSource = ds
            dgView.DataMember = ds.Tables(0).TableName

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub txtPassportNumber_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassportNumber.KeyDown
        If e.KeyCode = Keys.Enter Then



            Try


                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim strSql As String

                Dim strSearch As String = ""
                If txtPassportNumber.Text.Trim() <> "" Then
                    strSearch = " and ow.PPNO like '%" & txtPassportNumber.Text.Trim() & "%'"
                End If
                If txtPassportNumber.Text.Trim() <> "" Then

                    strSql = "SELECT taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                        " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                        " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                        " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                        " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                        " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                        " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                        " ow.TIN, ow.BIN " + _
                        " FROM FIU_OWNER_INFO ow " + _
                        " Inner join FIU_TRANS_AC_OWNER taw ON (taw.OWNER_CODE = ow.OWNER_CODE and taw .STATUS = 'L') " + _
                        " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                        " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                        " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                        " WHERE ow.STATUS ='L' " & strSearch + _
                        " ORDER BY taw.ACNUMBER "


                Else

                    strSql = "SELECT taw.ACNUMBER,ow.OWNER_CODE,ow.OWNER_NAME ,ow.OWNER_FATHER ,ow.OWNER_MOTHER ,ow.OWNER_SPOUSE , " + _
                         " CONVERT(VARCHAR(10), ow.DOB,  103) AS DATE_OF_BIRTH ,ow.PRES_ADDR ,th.THANA_NAME AS PRESENT_THANA,ow.PERM_ADDR , " + _
                         " tn.THANA_NAME AS PERMANENT_THANA,ow.PHONE_CITY_OFF1, ow.PHONE_OFF1 , " + _
                         " ow.PHONE_CITY_OFF2, ow.PHONE_OFF2, " + _
                         " ow.PHONE_CITY_RES1, ow.PHONE_RES1, ow.PHONE_CITY_RES2, " + _
                         " ow.PHONE_RES2, ow.MOBILE1, ow.MOBILE2, ow.PPNO, ow.DRIVINGLNO, " + _
                         " ed.EXE_DESIG_NAME, taw.SIGN_AUTHORITY, " + _
                         " ow.TIN, ow.BIN " + _
                         " FROM FIU_OWNER_INFO ow " + _
                         " Inner join FIU_TRANS_AC_OWNER taw ON (taw.OWNER_CODE = ow.OWNER_CODE and taw .STATUS = 'L') " + _
                         " left join FIU_THANA th ON(th.THANA_CODE = ow.PRES_THANA_CODE AND th.STATUS ='L') " + _
                         " left join FIU_THANA tn ON(tn.THANA_CODE = ow.PERM_THANA_CODE AND tn.STATUS ='L' ) " + _
                         " left join FIU_EXECUTIVE_DESIG  ed ON(ed.EXE_DESIG_CODE = taw .EXE_DESIG_CODE And ed.STATUS = 'L' ) " + _
                         " WHERE ow.STATUS ='L' ORDER BY taw.ACNUMBER "


                End If


                Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                dgView.AutoGenerateColumns = True
                dgView.DataSource = ds
                dgView.DataMember = ds.Tables(0).TableName

            Catch ex As Exception

                MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        End If
    End Sub
End Class