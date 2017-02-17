Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Globalization

Public Class FrmAccountSumm

#Region "Global Variables"

    Dim _formName As String = "MaintenanceAccountInfoSummary"
    Dim opt As SecForm = New SecForm(_formName)
    Dim _formMode As String = ""
    Dim intSplit As Integer = 200
    Dim _intModno As Integer = 0
    Dim log_message As String = ""

    '' For Update
    Dim _AcTitle As String = ""
    Dim _branch As String = ""
    Dim _AcDepAmt As String = ""
    Dim _AcDepTrNo As String = ""
    Dim _DepMaxAmt As String = ""
    Dim _WithAmt As String = ""
    Dim _WithTrNo As String = ""
    Dim _WithMaxAmt As String = ""
    Dim _AcTin As String = ""
    Dim _AcBin As String = ""
    Dim _VatReg As String = ""
    Dim _PreAdd As String = ""
    Dim _ComRegi As String = ""
    Dim _PerAdd As String = ""
    Dim _AccNo As String = ""
    Dim _PhoneRes1 As String = ""
    Dim _PhoneRes2 As String = ""
    Dim _Mobile1 As String = ""
    Dim _Mobile2 As String = ""
    Dim _OwType As String = ""
    Dim _OwTypeName As String = ""
    Dim _AccType As String = ""
    Dim _AccTypeName As String = ""

    ''----For Authorize
    Dim _Title As String = ""
    Dim _Acbranch As String = ""
    Dim _DepAmt As String = ""
    Dim _DepTrNo As String = ""
    Dim _DeptMaxAmt As String = ""
    Dim _WithdrawAmt As String = ""
    Dim _WithdrawTrNo As String = ""
    Dim _WithdrawMaxAmt As String = ""
    Dim _Tin As String = ""
    Dim _Bin As String = ""
    Dim _VatRegi As String = ""
    Dim _PreAddrs As String = ""
    Dim _ComReg As String = ""
    Dim _PerAddrs As String = ""
    Dim _AccNumber As String = ""
    Dim _Phone1 As String = ""
    Dim _Phone2 As String = ""
    Dim _MobileOne As String = ""
    Dim _MobileTwo As String = ""
    Dim _OwnerType As String = ""
    Dim _OwnerTypeName As String = ""
    Dim _AccountType As String = ""
    Dim _AccountTypeName As String = ""

    Dim AccList As New List(Of String)
    Dim _accLog As String = ""
    Dim _log As String = ""

#End Region
#Region "user defined codes"

    Private Sub LoadDataGrid()

        If dgView.Columns.Count = 0 Then Exit Sub

        Try

            ' dgView.DataSource = Nothing


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            'If chkShowAll.Checked = True Then
            '    strSql = "select MODNO, BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2, " + _
            '        " 'S' = " + _
            '        "	CASE  " + _
            '        "	    WHEN IS_AUTHORIZED='1' and STATUS = 'D' THEN 'D' " + _
            '        "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
            '        "       ELSE 'U' " + _
            '        "	End " + _
            '        " from FIU_ACCOUNT_INFO " + _
            '        " where IS_AUTHORIZED=0 OR STATUS in ('L','D')  " + _
            '        " order by IS_AUTHORIZED,AcNumber"

            'ElseIf rdoAuthorized.Checked = True Then

            '    strSql = "select MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2, " + _
            '    " 'S' = " + _
            '    "	CASE  " + _
            '    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
            '    "	End " + _
            '    " from FIU_ACCOUNT_INFO " + _
            '    " where IS_AUTHORIZED=1 AND STATUS='L' " + _
            '    " order by IS_AUTHORIZED,AcNumber"


            'Else
            '    strSql = "select MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2, " + _
            '    " 'S' = " + _
            '    "	CASE  " + _
            '    "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
            '    "	End " + _
            '    " from FIU_ACCOUNT_INFO " + _
            '    " where IS_AUTHORIZED=0" + _
            '    " order by IS_AUTHORIZED,AcNumber"



            If chkShowAll.Checked = True Then
                strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
                 " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='1' and STATUS = 'D' THEN 'D' " + _
                    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                    "       ELSE 'U' " + _
                    "	End " + _
                    " from FIU_ACCOUNT_INFO " + _
                    " where IS_AUTHORIZED=0 OR STATUS in ('L','D')  " + _
                    " order by IS_AUTHORIZED,AcNumber"

            ElseIf rdoAuthorized.Checked = True Then

                strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
                 " 'S' = " + _
                 "	CASE  " + _
                 "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                 "	End " + _
                 " from FIU_ACCOUNT_INFO " + _
                 " where IS_AUTHORIZED=1 AND [STATUS]='L' " + _
                 " order by IS_AUTHORIZED,AcNumber"


            Else
                strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
                " 'S' = " + _
               "	CASE  " + _
               "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
               "	End " + _
               " from FIU_ACCOUNT_INFO " + _
               " where IS_AUTHORIZED=0" + _
               " order by IS_AUTHORIZED,AcNumber"


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

            dgView.AutoGenerateColumns = True
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

        Dim frmAccInfo As New FrmAccountInfo
        frmAccInfo.ShowDialog()

    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try

            If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing) Then
                'dgView.SelectedRows.Item(0).Cells(0).Value 
                Dim frmAccount As New FrmAccountInfo(dgView.SelectedRows.Item(0).Cells(1).Value, dgView.SelectedRows.Item(0).Cells("ACNUMBER").Value, dgView.SelectedRows.Item(0).Cells("BANK_CODE").Value, dgView.SelectedRows.Item(0).Cells("BRANCH_CODE").Value)
                frmAccount.ShowDialog()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick
        If Not (dgView.SelectedRows.Item(0).Cells(1).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(1).Value Is DBNull.Value) Then
            'dgView.SelectedRows.Item(0).Cells(0).Value 
            Dim frmAccount As New FrmAccountInfo(dgView.SelectedRows.Item(0).Cells(1).Value, dgView.SelectedRows.Item(0).Cells("ACNUMBER").Value, dgView.SelectedRows.Item(0).Cells("BANK_CODE").Value, dgView.SelectedRows.Item(0).Cells("BRANCH_CODE").Value)
            frmAccount.ShowDialog()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()

    End Sub

    
   
 

   
    Private Sub FrmAccountSumm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
        CommonUtil.FillComboBox("Select Branch_Code,Branch_Name From FIU_Bank_Branch where STATUS='L'", cmbBranch)

        Dim i As Integer
        For i = 1 To dgView.Columns.Count - 1
            dgView.Columns(i).ReadOnly = True

        Next

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            Dim strSearch As String = ""
            If cmbBranch.Text.Trim() <> "" Then
                strSearch = " And Branch_Code='" & cmbBranch.SelectedValue.ToString() & "' "
            End If

            If txtAccNo.Text.Trim() <> "" Then
                strSearch = " and AcNumber like '%" & txtAccNo.Text.Trim() & "%'"
            End If

            If txtAccTitle.Text.Trim() <> "" Then
                strSearch = " and AC_TITLE like '%" & txtAccTitle.Text.Trim() & "%'"
            End If


            If chkShowAll.Checked = True Then

                strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
                  " 'S' = " + _
                  "	CASE  " + _
                  "	    WHEN IS_AUTHORIZED='1' and STATUS = 'D' THEN 'D' " + _
                  "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                  "       ELSE 'U' " + _
                  "	End " + _
                  " from FIU_ACCOUNT_INFO " + _
                  " where (IS_AUTHORIZED=0 OR IS_AUTHORIZED=1 OR STATUS in ('L','D'))  " & strSearch + _
                  " order by IS_AUTHORIZED,AcNumber"


                'strSql = "select MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
                '    " 'S' = " + _
                '    "	CASE  " + _
                '    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                '    "       ELSE 'U' " + _
                '    "	End " + _
                '    " from FIU_ACCOUNT_INFO " + _
                '    " where (IS_AUTHORIZED=0 OR STATUS in ('L','D'))  " & strSearch + _
                '    " order by IS_AUTHORIZED,AcNumber"


            ElseIf rdoAuthorized.Checked = True Then

                strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
                    " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                    "	End " + _
                    " from FIU_ACCOUNT_INFO " + _
                    " where (IS_AUTHORIZED=1 AND STATUS='L')  " & strSearch + _
                    " order by IS_AUTHORIZED,AcNumber"

            Else
                strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
                  " 'S' = " + _
                  "	CASE  " + _
                  "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
                  "       ELSE 'U' " + _
                  "	End " + _
                  " from FIU_ACCOUNT_INFO " + _
                  " where (IS_AUTHORIZED=0)  " & strSearch + _
                  " order by IS_AUTHORIZED,AcNumber"

            End If

            'Else
            '    strSql = "select MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2, " + _
            '    " 'S' = " + _
            '    "	CASE  " + _
            '    "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
            '    "	End " + _
            '    " from FIU_ACCOUNT_INFO " + _
            '    " where (IS_AUTHORIZED=0 OR STATUS='L')  " & strSearch + _
            '    " order by IS_AUTHORIZED,AcNumber"


            'strSql = "select SLNO,APP_ID,APP_NAME,IS_AUTHORIZED,MODNO, " + _
            '" 'S' = " + _
            '"	CASE  " + _
            '"	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
            '"       ELSE 'U' " + _
            '"	End " + _
            '" from APPS " + _
            '" where IS_AUTHORIZED=0 OR STATUS='L' " + _
            '" order by IS_AUTHORIZED,APP_ID"


            'End If


            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            dgView.AutoGenerateColumns = True
            dgView.DataSource = ds
            dgView.DataMember = ds.Tables(0).TableName
            lblTotRecNo.Text = ds.Tables(0).Rows.Count


        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
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

    '            strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
    '               " 'S' = " + _
    '               "	CASE  " + _
    '               "	    WHEN IS_AUTHORIZED='1' and STATUS = 'D' THEN 'D' " + _
    '               "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '               "       ELSE 'U' " + _
    '               "	End " + _
    '               " from FIU_ACCOUNT_INFO " + _
    '               " where (IS_AUTHORIZED=0 OR IS_AUTHORIZED=1 OR STATUS in ('L','D')) " & strSearch + _
    '               " order by IS_AUTHORIZED,AcNumber"


    '            'strSql = "select MODNO, BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '            '    " 'S' = " + _
    '            '    "	CASE  " + _
    '            '    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '            '    "	End " + _
    '            '    " from FIU_ACCOUNT_INFO " + _
    '            '    " where (IS_AUTHORIZED=1 OR STATUS = 'L') and  ACNUMBER='" & txtSearch.Text.ToString() & "'  " + _
    '            '    " order by IS_AUTHORIZED,AcNumber"

    '        ElseIf rdoAuthorized.Checked = True Then

    '            strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
    '              " 'S' = " + _
    '              "	CASE  " + _
    '              "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '              "	End " + _
    '              " from FIU_ACCOUNT_INFO " + _
    '              " where (IS_AUTHORIZED=1 AND [STATUS]='L') " & strSearch + _
    '              " order by IS_AUTHORIZED,AcNumber"


    '        Else
    '            strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
    '               " 'S' = " + _
    '               "	CASE  " + _
    '               "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
    '               "	End " + _
    '               " from FIU_ACCOUNT_INFO " + _
    '               " where IS_AUTHORIZED=0 " & strSearch + _
    '               " order by IS_AUTHORIZED,AcNumber"


    '        End If


    '        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

    '        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

    '        dgView.AutoGenerateColumns = True
    '        dgView.DataSource = ds
    '        dgView.DataMember = ds.Tables(0).TableName
    '        lblTotRecNo.Text = ds.Tables(0).Rows.Count


    '    Catch ex As Exception

    '        MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    End Try
    'End Sub

    'Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
    '    Try


    '        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

    '        Dim strSql As String

    '        Dim strSearch As String = ""
    '        If txtSearch.Text.Trim() <> "" Then
    '            strSearch = " and ACNUMBER like '%" & txtSearch.Text.Trim() & "%'"
    '        End If
    '        If chkShowAll.Checked = True Then

    '            strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
    '              " 'S' = " + _
    '              "	CASE  " + _
    '              "	    WHEN IS_AUTHORIZED='1' and STATUS = 'D' THEN 'D' " + _
    '              "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '              "       ELSE 'U' " + _
    '              "	End " + _
    '              " from FIU_ACCOUNT_INFO " + _
    '              " where (IS_AUTHORIZED=0 OR IS_AUTHORIZED=1 OR STATUS in ('L','D')) " & strSearch + _
    '              " order by IS_AUTHORIZED,AcNumber"


    '            'strSql = "select MODNO, BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
    '            '    " 'S' = " + _
    '            '    "	CASE  " + _
    '            '    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '            '    "	End " + _
    '            '    " from FIU_ACCOUNT_INFO " + _
    '            '    " where (IS_AUTHORIZED=1 OR STATUS = 'L') and  ACNUMBER='" & txtSearch.Text.ToString() & "'  " + _
    '            '    " order by IS_AUTHORIZED,AcNumber"

    '        ElseIf rdoAuthorized.Checked = True Then

    '            strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
    '              " 'S' = " + _
    '              "	CASE  " + _
    '              "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
    '              "	End " + _
    '              " from FIU_ACCOUNT_INFO " + _
    '              " where (IS_AUTHORIZED=1 AND [STATUS]='L') " & strSearch + _
    '              " order by IS_AUTHORIZED,AcNumber"


    '        Else
    '            strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
    '               " 'S' = " + _
    '               "	CASE  " + _
    '               "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
    '               "	End " + _
    '               " from FIU_ACCOUNT_INFO " + _
    '               " where IS_AUTHORIZED=0 " & strSearch + _
    '               " order by IS_AUTHORIZED,AcNumber"


    '        End If


    '        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

    '        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

    '        dgView.AutoGenerateColumns = True
    '        dgView.DataSource = ds
    '        dgView.DataMember = ds.Tables(0).TableName
    '        lblTotRecNo.Text = ds.Tables(0).Rows.Count


    '    Catch ex As Exception

    '        MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    End Try
    'End Sub

    Private Sub rdoAuthorized_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAuthorized.CheckedChanged
        LoadDataGrid()

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

    ''-----------------------Mizan Work (21-04-16) ---------------------------

    Private Sub LoadDataForAuth(ByVal strBankCd As String, ByVal strBranchCd As String, ByVal strAcNo As String)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_ACCOUNT_INFO Where Bank_Code='" & strBankCd & "' and Branch_Code='" & strBranchCd & "' and AcNumber='" & strAcNo & "' and STATUS='L'")

            If ds.Tables(0).Rows.Count > 0 Then


                _formMode = FormTransMode.Update

                
                _Acbranch = ds.Tables(0).Rows(0)("Branch_Code").ToString()

                _AccNumber = ds.Tables(0).Rows(0)("AcNumber").ToString

                _Title = ds.Tables(0).Rows(0)("Ac_Title").ToString

                _AccountType = ds.Tables(0).Rows(0)("AcTypeCode").ToString()

                _OwnerType = ds.Tables(0).Rows(0)("OwTypeCode").ToString()
                _DepAmt = ds.Tables(0).Rows(0)("Declared_Deposit_Amount").ToString

                _DepTrNo = ds.Tables(0).Rows(0)("Declared_Deposit_TransNo").ToString

                _DeptMaxAmt = ds.Tables(0).Rows(0)("Declared_Deposit_MaxAmount").ToString

                _WithdrawAmt = ds.Tables(0).Rows(0)("Declared_Withdr_Amount").ToString
                _WithdrawTrNo = ds.Tables(0).Rows(0)("Declared_Withdr_TransNo").ToString
                _WithdrawMaxAmt = ds.Tables(0).Rows(0)("Declared_Withdr_MaxAmount").ToString
                _Tin = ds.Tables(0).Rows(0)("TIN").ToString

                _Bin = ds.Tables(0).Rows(0)("BIN").ToString
                _VatRegi = ds.Tables(0).Rows(0)("Vat_Reg_No").ToString
                _ComReg = ds.Tables(0).Rows(0)("Company_Reg_No").ToString
                _PreAddrs = ds.Tables(0).Rows(0)("Pres_Addr").ToString
                _PerAddrs = ds.Tables(0).Rows(0)("Perm_Addr").ToString
                _Phone1 = ds.Tables(0).Rows(0)("Phone_Res1").ToString
                _Phone2 = ds.Tables(0).Rows(0)("Phone_Res2").ToString

                _MobileOne = ds.Tables(0).Rows(0)("Mobile1").ToString
                _MobileTwo = ds.Tables(0).Rows(0)("Mobile2").ToString

                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_ACCOUNT_TYPES Where ACTYPECODE = '" & _AccountType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _AccountTypeName = ds2.Tables(0).Rows(0)("ACDEFINITION").ToString()
                    _AccountType = _AccountTypeName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_OWNERSHIP_TYPES Where OWTYPECODE = '" & _OwnerType & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _OwnerTypeName = ds3.Tables(0).Rows(0)("OWDEFINITION").ToString()
                    _OwnerType = _OwnerTypeName

                End If


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''-----------------------Mizan Work (21-04-16) ---------------------------

    Private Sub LoadAccData(ByVal strBankCd As String, ByVal strBranchCd As String, ByVal strAcNo As String, ByVal intMod As Integer)

        lblToolStatus.Text = ""

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            ds = db.ExecuteDataSet(CommandType.Text, "Select * From FIU_ACCOUNT_INFO Where Bank_Code='" & strBankCd & "' and Branch_Code='" & strBranchCd & "' and AcNumber='" & strAcNo & "' and MODNO=" & intMod)

            If ds.Tables(0).Rows.Count > 0 Then


                '_strBank_Code = ds.Tables(0).Rows(0)("Bank_Code").ToString()
                '_strBranch_Code = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                '_strAcNumber = ds.Tables(0).Rows(0)("AcNumber").ToString
                _intModno = intMod



                _formMode = FormTransMode.Update


                cmbBranch.SelectedValue = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                _branch = ds.Tables(0).Rows(0)("Branch_Code").ToString()
                txtAccNo.Text = ds.Tables(0).Rows(0)("AcNumber").ToString
                _AccNo = ds.Tables(0).Rows(0)("AcNumber").ToString
                txtAccTitle.Text = ds.Tables(0).Rows(0)("Ac_Title").ToString
                _AcTitle = ds.Tables(0).Rows(0)("Ac_Title").ToString
                'cmbAccType.SelectedValue = ds.Tables(0).Rows(0)("AcTypeCode").ToString
                _AccType = ds.Tables(0).Rows(0)("AcTypeCode").ToString()
                'cmbOwType.SelectedValue = ds.Tables(0).Rows(0)("OwTypeCode").ToString
                _OwType = ds.Tables(0).Rows(0)("OwTypeCode").ToString()

                'txtDepositAmt.Text = ds.Tables(0).Rows(0)("Declared_Deposit_Amount").ToString
                _AcDepAmt = ds.Tables(0).Rows(0)("Declared_Deposit_Amount").ToString
                'txtDepositTransNo.Text = ds.Tables(0).Rows(0)("Declared_Deposit_TransNo").ToString
                _AcDepTrNo = ds.Tables(0).Rows(0)("Declared_Deposit_TransNo").ToString
                'txtDepositMaxAmt.Text = ds.Tables(0).Rows(0)("Declared_Deposit_MaxAmount").ToString
                _DepMaxAmt = ds.Tables(0).Rows(0)("Declared_Deposit_MaxAmount").ToString
                'txtWithdrawAmt.Text = ds.Tables(0).Rows(0)("Declared_Withdr_Amount").ToString
                _WithAmt = ds.Tables(0).Rows(0)("Declared_Withdr_Amount").ToString

                'txtWithdrawTransNo.Text = ds.Tables(0).Rows(0)("Declared_Withdr_TransNo").ToString
                _WithTrNo = ds.Tables(0).Rows(0)("Declared_Withdr_TransNo").ToString

                'txtWithdrawMaxAmt.Text = ds.Tables(0).Rows(0)("Declared_Withdr_MaxAmount").ToString
                _WithMaxAmt = ds.Tables(0).Rows(0)("Declared_Withdr_MaxAmount").ToString

                'txtTIN.Text = ds.Tables(0).Rows(0)("TIN").ToString
                _AcTin = ds.Tables(0).Rows(0)("TIN").ToString

                'txtBIN.Text = ds.Tables(0).Rows(0)("BIN").ToString
                _AcBin = ds.Tables(0).Rows(0)("BIN").ToString

                'txtVatRegNo.Text = ds.Tables(0).Rows(0)("Vat_Reg_No").ToString
                _VatReg = ds.Tables(0).Rows(0)("Vat_Reg_No").ToString

                _ComRegi = ds.Tables(0).Rows(0)("Company_Reg_No").ToString
                _PreAdd = ds.Tables(0).Rows(0)("Pres_Addr").ToString
                _PerAdd = ds.Tables(0).Rows(0)("Perm_Addr").ToString

                _PhoneRes1 = ds.Tables(0).Rows(0)("Phone_Res1").ToString

                _PhoneRes2 = ds.Tables(0).Rows(0)("Phone_Res2").ToString
                _Mobile1 = ds.Tables(0).Rows(0)("Mobile1").ToString
                _Mobile2 = ds.Tables(0).Rows(0)("Mobile2").ToString

                Dim ds2 As New DataSet
                ds2 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_ACCOUNT_TYPES Where ACTYPECODE = '" & _AccType & "' ")
                If ds2.Tables(0).Rows.Count > 0 Then

                    _AccTypeName = ds2.Tables(0).Rows(0)("ACDEFINITION").ToString()
                    _AccType = _AccTypeName

                End If

                Dim ds3 As New DataSet
                ds3 = db.ExecuteDataSet(CommandType.Text, "Select *  From FIU_OWNERSHIP_TYPES Where OWTYPECODE = '" & _OwType & "' ")
                If ds3.Tables(0).Rows.Count > 0 Then

                    _OwTypeName = ds3.Tables(0).Rows(0)("OWDEFINITION").ToString()
                    _OwType = _OwTypeName

                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function AuthorizeData(ByVal i As Integer) As TransState

        ''-----------------------Mizan Work (21-04-16) ---------------------------

        LoadAccData(dgView.Rows(i).Cells("BANK_CODE").Value, dgView.Rows(i).Cells("Branch_Code").Value, dgView.Rows(i).Cells("ACNUMBER").Value, dgView.Rows(i).Cells(1).Value)

        If dgView.Rows(i).Cells(1).Value > 1 Then


            LoadDataForAuth(dgView.Rows(i).Cells("BANK_CODE").Value, dgView.Rows(i).Cells("Branch_Code").Value, dgView.Rows(i).Cells("ACNUMBER").Value)

            Dim tStatus As TransState

            Dim strSql As String
            tStatus = TransState.UnspecifiedError

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                strSql = "select IS_AUTHORIZED,STATUS from FIU_ACCOUNT_INFO where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("Branch_Code").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_ACCOUNT_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value

                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_ACCOUNT_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If dgView.Rows(i).Cells(1).Value > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_ACCOUNT_INFO set STATUS = 'C' " & _
                                " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_ACCOUNT_INFO set STATUS = 'O' " & _
                                " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

                        If _Title <> _AcTitle Then
                            If _Title = "" Then
                                log_message = " Title : " + _AcTitle + "." + " "
                            Else
                                log_message = " Title : " + _Title + " " + " To " + " " + _AcTitle + "." + " "
                            End If

                            AccList.Add(log_message)

                        End If


                        If _DepAmt <> _AcDepAmt Then

                            If _DepAmt = "" Then
                                log_message = " Deposit Amount : " + _AcDepAmt + "." + " "
                            Else
                                log_message = " Deposit Amount : " + _DepAmt + " " + " To " + " " + _AcDepAmt + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _DepTrNo <> _AcDepTrNo Then
                            If _DepTrNo = "" Then
                                log_message = " Deposit Trans No : " + _AcDepTrNo + "." + " "
                            Else
                                log_message = " Deposit Trans No : " + _DepTrNo + " " + " To " + " " + _AcDepTrNo + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _DeptMaxAmt <> _DepMaxAmt Then
                            If _DeptMaxAmt = "" Then
                                log_message = " Deposit Max Amount : " + _DepMaxAmt + "." + " "
                            Else
                                log_message = " Deposit Max Amount : " + _DeptMaxAmt + " " + " To " + " " + _DepMaxAmt + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _WithdrawAmt <> _WithAmt Then
                            If _WithdrawAmt = "" Then
                                log_message = " Withdrow Amount : " + _WithAmt + "." + " "
                            Else
                                log_message = " Withdrow Amount : " + _WithdrawAmt + " " + " To " + " " + _WithAmt + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _WithdrawTrNo <> _WithTrNo Then
                            If _WithdrawTrNo = "" Then
                                log_message = " Withdrow Trans No : " + _WithTrNo + "." + " "
                            Else
                                log_message = " Withdrow Trans No : " + _WithdrawTrNo + " " + " To " + " " + _WithTrNo + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _WithdrawMaxAmt <> _WithMaxAmt Then
                            If _WithdrawMaxAmt = "" Then
                                log_message = " Withdrow Max Amount : " + _WithMaxAmt + "." + " "
                            Else
                                log_message = " Withdrow Max Amount : " + _WithdrawMaxAmt + " " + " To " + " " + _WithMaxAmt + "." + " "

                            End If

                            AccList.Add(log_message)
                        End If

                        If _Tin <> _AcTin Then
                            If _Tin = "" Then
                                log_message = " TIN Number : " + _AcTin + "." + " "
                            Else
                                log_message = " TIN Number : " + _Tin + " " + " To " + " " + _AcTin + "." + " "
                            End If
                            AccList.Add(log_message)
                        End If

                        If _Bin <> _AcBin Then
                            If _Bin = "" Then
                                log_message = " BIN Number : " + _AcBin + "." + " "
                            Else
                                log_message = " BIN Number : " + _Bin + " " + " To " + " " + _AcBin + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _VatRegi <> _VatReg Then
                            If _VatRegi = "" Then
                                log_message = " Vat Regi : " + _VatReg + "." + " "
                            Else
                                log_message = " Vat Regi : " + _VatRegi + " " + " To " + " " + _VatReg + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _PreAddrs <> _PreAdd Then
                            If _PreAddrs = "" Then
                                log_message = " Present ADD : " + _PreAdd + "." + " "
                            Else
                                log_message = " Present ADD : " + _PreAddrs + " " + " To " + " " + _PreAdd + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _ComReg <> _ComRegi Then
                            If _ComReg = "" Then
                                log_message = " Company Regi : " + _ComRegi + "." + " "
                            Else
                                log_message = " Company Regi : " + _ComReg + " " + " To " + " " + _ComRegi + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _PerAddrs <> _PerAdd Then
                            If _PerAddrs = "" Then
                                log_message = " Permanent Address : " + _PerAdd + "." + " "
                            Else
                                log_message = " Permanent Address : " + _PerAddrs + " " + " To " + " " + _PerAdd + "." + " "
                            End If
                            AccList.Add(log_message)
                        End If


                        If _OwnerType <> _OwType Then
                            If _OwnerType = "" Then
                                log_message = " Ownership Type : " + _OwType + "." + " "
                            Else
                                log_message = " Ownership Type : " + _OwnerType + " " + " To " + " " + _OwType + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _AccountType <> _AccType Then
                            If _AccountType = "" Then
                                log_message = " Account Type : " + _AccType + "." + " "
                            Else
                                log_message = " Account Type : " + _AccountType + " " + " To " + " " + _AccType + "." + " "
                            End If

                            AccList.Add(log_message)
                        End If

                        If _MobileOne <> _Mobile1 Then
                            log_message = " Mobile 1 : " + _Mobile1 + "." + " "

                            AccList.Add(log_message)
                        End If

                        If _MobileTwo <> _Mobile2 Then
                            log_message = " Mobile 2 : " + _Mobile2 + "." + " "

                            AccList.Add(log_message)
                        End If

                        If _Phone1 <> _PhoneRes1 Then
                            log_message = " Phone Resident : " + _PhoneRes1 + "." + " "

                            AccList.Add(log_message)
                        End If

                        If _Phone2 <> _PhoneRes2 Then
                            log_message = " Phone Resident " + _PhoneRes2 + "." + " "

                            AccList.Add(log_message)
                        End If

                        For Each Accloglist As String In AccList
                            _accLog += Accloglist
                        Next

                        _log = " Authorized : Account No : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + _accLog

                        Logger.system_log(_log)
                        _accLog = ""
                        AccList.Clear()

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

                strSql = "select IS_AUTHORIZED,STATUS from FIU_ACCOUNT_INFO where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("Branch_Code").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

                Dim ds As New DataSet

                ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

                If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

                    If ds.Tables(0).Rows(0)("STATUS") = "U" Then
                        strSql = "update FIU_ACCOUNT_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
                        " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value

                    ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
                        strSql = "update FIU_ACCOUNT_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
                        "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
                        " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value

                    End If

                    Dim result As Integer
                    result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                    If result <= 0 Then

                        tStatus = TransState.NoRecord

                    ElseIf result > 0 Then

                        If dgView.Rows(i).Cells(1).Value > 1 Then

                            'if previous modification status is D(Deleted) then make it C(Closed)
                            strSql = "update FIU_ACCOUNT_INFO set STATUS = 'C' " & _
                                " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
                                " and STATUS ='D'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                            'if previous modification status is L(Deleted) then make it O(Open)
                            strSql = "update FIU_ACCOUNT_INFO set STATUS = 'O' " & _
                                " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
                                " and STATUS ='L'"

                            db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                        End If
                        tStatus = TransState.Update

                        If _Title <> _AcTitle Then
                            If _Title = "" Then
                                log_message = " Title : " + _AcTitle + "." + " "
                            Else
                                log_message = " Title : " + _Title + " " + " To " + " " + _AcTitle + "." + " "
                            End If

                            AccList.Add(log_message)

                        End If

                        If _DepAmt <> _AcDepAmt Then

                            If _DepAmt = "" Then
                                'log_message = " Deposit Amount : " + _AcDepAmt + "." + " "
                            Else
                                log_message = " Deposit Amount : " + _DepAmt + " " + " To " + " " + _AcDepAmt + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _DepTrNo <> _AcDepTrNo Then
                            If _DepTrNo = "" Then
                                'log_message = " Deposit Trans No : " + _AcDepTrNo + "." + " "
                            Else
                                log_message = " Deposit Trans No : " + _DepTrNo + " " + " To " + " " + _AcDepTrNo + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _DeptMaxAmt <> _DepMaxAmt Then
                            If _DeptMaxAmt = "" Then
                                'log_message = " Deposit Max Amount : " + _DepMaxAmt + "." + " "
                            Else
                                log_message = " Deposit Max Amount : " + _DeptMaxAmt + " " + " To " + " " + _DepMaxAmt + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _WithdrawAmt <> _WithAmt Then
                            If _WithdrawAmt = "" Then
                                'log_message = " Withdrow Amount : " + _WithAmt + "." + " "
                            Else
                                log_message = " Withdrow Amount : " + _WithdrawAmt + " " + " To " + " " + _WithAmt + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _WithdrawTrNo <> _WithTrNo Then
                            If _WithdrawTrNo = "" Then
                                'log_message = " Withdrow Trans No : " + _WithTrNo + "." + " "
                            Else
                                log_message = " Withdrow Trans No : " + _WithdrawTrNo + " " + " To " + " " + _WithTrNo + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _WithdrawMaxAmt <> _WithMaxAmt Then
                            If _WithdrawMaxAmt = "" Then
                                'log_message = " Withdrow Max Amount : " + _WithMaxAmt + "." + " "
                            Else
                                log_message = " Withdrow Max Amount : " + _WithdrawMaxAmt + " " + " To " + " " + _WithMaxAmt + "." + " "
                                AccList.Add(log_message)

                            End If
                        End If

                        If _Tin <> _AcTin Then
                            If _Tin = "" Then
                                'log_message = " TIN Number : " + _AcTin + "." + " "
                            Else
                                log_message = " TIN Number : " + _Tin + " " + " To " + " " + _AcTin + "." + " "
                                AccList.Add(log_message)
                            End If

                        End If

                        If _Bin <> _AcBin Then
                            If _Bin = "" Then
                                'log_message = " BIN Number : " + _AcBin + "." + " "
                            Else
                                log_message = " BIN Number : " + _Bin + " " + " To " + " " + _AcBin + "." + " "
                                AccList.Add(log_message)
                            End If
                        End If

                        If _VatRegi <> _VatReg Then
                            If _VatRegi = "" Then
                                ' log_message = " Vat Regi : " + _VatReg + "." + " "
                            Else
                                log_message = " Vat Regi : " + _VatRegi + " " + " To " + " " + _VatReg + "." + " "
                                AccList.Add(log_message)
                            End If
                        End If

                        If _PreAddrs <> _PreAdd Then
                            If _PreAddrs = "" Then
                                'log_message = " Present ADD : " + _PreAdd + "." + " "
                            Else
                                log_message = " Present ADD : " + _PreAddrs + " " + " To " + " " + _PreAdd + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _ComReg <> _ComRegi Then
                            If _ComReg = "" Then
                                ' log_message = " Company Regi : " + _ComRegi + "." + " "
                            Else
                                log_message = " Company Regi : " + _ComReg + " " + " To " + " " + _ComRegi + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _PerAddrs <> _PerAdd Then
                            If _PerAddrs = "" Then
                                'log_message = " Permanent Address : " + _PerAdd + "." + " "
                            Else
                                log_message = " Permanent Address : " + _PerAddrs + " " + " To " + " " + _PerAdd + "." + " "
                                AccList.Add(log_message)
                            End If

                        End If

                        If _OwnerType <> _OwType Then
                            If _OwnerType = "" Then
                            Else
                                log_message = " Ownership Type : " + _OwnerType + " " + " To " + " " + _OwType + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _AccountType <> _AccType Then
                            If _AccountType = "" Then
                            Else
                                log_message = " Account Type : " + _AccountType + " " + " To " + " " + _AccType + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _MobileOne <> _Mobile1 Then
                            If _MobileOne = "" Then
                            Else
                                log_message = " Mobile 1 : " + _Mobile1 + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        If _MobileTwo <> _Mobile2 Then
                            If _MobileTwo = "" Then
                            Else
                                log_message = " Mobile 2 : " + _Mobile2 + "." + " "
                                AccList.Add(log_message)
                            End If

                        End If

                        If _Phone1 <> _PhoneRes1 Then
                            If _Phone1 = "" Then
                            Else
                                log_message = " Phone Resident : " + _PhoneRes1 + "." + " "
                                AccList.Add(log_message)
                            End If
                        End If

                        If _Phone2 <> _PhoneRes2 Then
                            If _Phone2 = "" Then
                            Else
                                log_message = " Phone Resident " + _PhoneRes2 + "." + " "
                                AccList.Add(log_message)
                            End If


                        End If

                        For Each Accloglist As String In AccList
                            _accLog += Accloglist
                        Next

                        _log = " Authorized : Account No : " + dgView.Rows(i).Cells(4).Value.ToString() + "." + " " + " For Mandatory Field : " + "." + " " + _accLog

                        Logger.system_log(_log)
                        _accLog = ""
                        AccList.Clear()

                    End If
                Else
                    tStatus = TransState.UpdateNotPossible
                End If


                trans.Commit()

               

            End Using

            Return tStatus

        End If

       

        ''-----------------------Mizan Work (21-04-16) ---------------------------


        ''-------------------Commented By Mizan (21-04-16)------------------------

        'Dim tStatus As TransState

        'Dim strSql As String
        'tStatus = TransState.UnspecifiedError

        'Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        'Using conn As DbConnection = db.CreateConnection()

        '    conn.Open()

        '    Dim trans As DbTransaction = conn.BeginTransaction()

        '    strSql = "select IS_AUTHORIZED,STATUS from FIU_ACCOUNT_INFO where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("Branch_Code").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value.ToString()

        '    Dim ds As New DataSet

        '    ds = db.ExecuteDataSet(trans, CommandType.Text, strSql)

        '    If ds.Tables(0).Rows(0)("IS_AUTHORIZED") = 0 Then

        '        If ds.Tables(0).Rows(0)("STATUS") = "U" Then
        '            strSql = "update FIU_ACCOUNT_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
        '            "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1, STATUS = 'L' " & _
        '            " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value

        '        ElseIf ds.Tables(0).Rows(0)("STATUS") = "D" Then
        '            strSql = "update FIU_ACCOUNT_INFO set AUTH_BY='" & CommonAppSet.User.ToString().Trim() & _
        '            "', AUTH_DATETIME=getdate(), IS_AUTHORIZED=1 " & _
        '            " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & dgView.Rows(i).Cells(1).Value

        '        End If

        '        Dim result As Integer
        '        result = db.ExecuteNonQuery(trans, CommandType.Text, strSql)

        '        If result <= 0 Then

        '            tStatus = TransState.NoRecord

        '        ElseIf result > 0 Then

        '            If dgView.Rows(i).Cells(1).Value > 1 Then

        '                'if previous modification status is D(Deleted) then make it C(Closed)
        '                strSql = "update FIU_ACCOUNT_INFO set STATUS = 'C' " & _
        '                    " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
        '                    " and STATUS ='D'"

        '                db.ExecuteNonQuery(trans, CommandType.Text, strSql)

        '                'if previous modification status is L(Deleted) then make it O(Open)
        '                strSql = "update FIU_ACCOUNT_INFO set STATUS = 'O' " & _
        '                    " where Bank_Code='" & dgView.Rows(i).Cells("BANK_CODE").Value & "' and Branch_Code='" & dgView.Rows(i).Cells("BRANCH_CODE").Value & "' and AcNumber='" & dgView.Rows(i).Cells("ACNUMBER").Value & "' and MODNO=" & (dgView.Rows(i).Cells(1).Value - 1).ToString() & _
        '                    " and STATUS ='L'"

        '                db.ExecuteNonQuery(trans, CommandType.Text, strSql)



        '            End If
        '            tStatus = TransState.Update
        '        End If
        '    Else
        '        tStatus = TransState.UpdateNotPossible
        '    End If


        '    trans.Commit()

        '    'log_message = "Authorized Account Number " + txtAccNo.Text.Trim()
        '    log_message = "Authorized Account Number : " + dgView.Rows(i).Cells(4).Value.ToString()
        '    Logger.system_log(log_message)

        'End Using

        'Return tStatus

    End Function

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

                        If dgView.Rows(i).Cells(29).Value.ToString = CommonAppSet.User.Trim() Then
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

 
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objExp As New ExportUtil(dgView)

        objExp.ExportXl()
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

                    strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
                      " 'S' = " + _
                      "	CASE  " + _
                      "	    WHEN IS_AUTHORIZED='1' and STATUS = 'D' THEN 'D' " + _
                      "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                      "       ELSE 'U' " + _
                      "	End " + _
                      " from FIU_ACCOUNT_INFO " + _
                      " where (IS_AUTHORIZED=0 OR IS_AUTHORIZED=1 OR STATUS in ('L','D')) " & strSearch + _
                      " order by IS_AUTHORIZED,AcNumber"


                    'strSql = "select MODNO, BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,INPUT_DATETIME,AUTH_BY,AUTH_DATETIME, " + _
                    '    " 'S' = " + _
                    '    "	CASE  " + _
                    '    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                    '    "	End " + _
                    '    " from FIU_ACCOUNT_INFO " + _
                    '    " where (IS_AUTHORIZED=1 OR STATUS = 'L') and  ACNUMBER='" & txtSearch.Text.ToString() & "'  " + _
                    '    " order by IS_AUTHORIZED,AcNumber"

                ElseIf rdoAuthorized.Checked = True Then

                    strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
                      " 'S' = " + _
                      "	CASE  " + _
                      "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                      "	End " + _
                      " from FIU_ACCOUNT_INFO " + _
                      " where (IS_AUTHORIZED=1 AND [STATUS]='L') " & strSearch + _
                      " order by IS_AUTHORIZED,AcNumber"


                Else
                    strSql = "select  MODNO,BANK_CODE,BRANCH_CODE,ACNUMBER,AC_TITLE,ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO,CONVERT(VARCHAR(10), VAT_REG_DATE,  103) AS VAT_REG_DATE, COMPANY_REG_NO,CONVERT(VARCHAR(10), COMPANY_REG_DATE,  103) AS COMPANY_REG_DATE , REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2,INPUT_BY,CONVERT(VARCHAR(10), INPUT_DATETIME,  103) AS INPUT_DATETIME,AUTH_BY,CONVERT(VARCHAR(10), AUTH_DATETIME,  103) AS AUTH_DATETILE, " + _
                       " 'S' = " + _
                       "	CASE  " + _
                       "	    WHEN IS_AUTHORIZED='0' THEN 'U' " + _
                       "	End " + _
                       " from FIU_ACCOUNT_INFO " + _
                       " where IS_AUTHORIZED=0 " & strSearch + _
                       " order by IS_AUTHORIZED,AcNumber"


                End If


                Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                dgView.AutoGenerateColumns = True
                dgView.DataSource = ds
                dgView.DataMember = ds.Tables(0).TableName
                lblTotRecNo.Text = ds.Tables(0).Rows.Count


            Catch ex As Exception

                MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try
        End If
    End Sub
End Class