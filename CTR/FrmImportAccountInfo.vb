'
'Author             : Fahad Khan
'Purpose            : Import Account Info Information
'Creation date      : 2-Dec-2013
'Stored Procedure(s):

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

'Imports Microsoft.Office.Interop

Imports System.IO.File
Imports System.IO.FileSystemInfo
Imports System.IO

Imports Microsoft.VisualBasic
Imports System.Xml

Imports Microsoft.Office.Interop



Public Class FrmImportAccountInfo

    Dim _formName As String = "MaintenanceGoAccountInfoImport"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""

    Dim flagRequireField As Boolean = False

    Dim _debugRow As Integer = 0
    Dim _ToTRecordNo As Integer = 0
    Public Delegate Sub ChangeTextOfLabelDelegate( _
                            ByVal ToLabel As Label, _
                            ByVal AddText As String)

    Private Sub SetLabelText(ByVal lblName As System.Windows.Forms.Label, ByVal strMsg As String)

        'txtFileImpStatus.AppendText(strMsg)


        If lblName.InvokeRequired Then
            'lblName.Invoke(New Action(Of System.String)(AddressOf SetLabelText), strMsg)
            lblName.Invoke(New ChangeTextOfLabelDelegate(AddressOf SetLabelText), New Object() {lblName, strMsg})
        Else
            lblName.Text = strMsg
        End If

    End Sub

    Private Sub ReadExcel()
        Dim xlApp As New Excel.Application

        Try

            Dim wb As Excel.Workbook = xlApp.Workbooks.Open(opdRetFile.FileName)

            Dim sheet As Excel.Worksheet = wb.Sheets(1)

            Dim xlRange As Excel.Range = sheet.UsedRange

            Dim rowCount As Integer = xlRange.Rows.Count
            Dim colCount As Integer = xlRange.Columns.Count

            'Dim colCharset As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

            'xlRange.Cells(0, 0)
            dgView.Rows.Clear()
            dgView.AllowUserToAddRows = True

            Dim i As Integer
            _ToTRecordNo = 0
            For i = 2 To rowCount

                If sheet.Range("A" & i.ToString()).Value2 Is Nothing Then
                    Exit For
                End If

                If sheet.Range("A" & i.ToString()).Value2.ToString() = "" Then
                    Exit For
                End If


                If (i = dgView.Rows.Count + 1) Then
                    dgView.Rows.Add()
                End If

                dgView.Item(0, i - 2).Value = sheet.Range("A" & i.ToString()).Value2  ' Account 
                dgView.Item(1, i - 2).Value = sheet.Range("B" & i.ToString()).Value2  ' Branch
                dgView.Item(2, i - 2).Value = sheet.Range("C" & i.ToString()).Value2  ' Account Tittle
                dgView.Item(3, i - 2).Value = sheet.Range("D" & i.ToString()).Value2  ' Account Type
                dgView.Item(4, i - 2).Value = sheet.Range("E" & i.ToString()).Value2  ' Ownership type
                dgView.Item(5, i - 2).Value = sheet.Range("F" & i.ToString()).Value2  ' Deposit
                dgView.Item(6, i - 2).Value = sheet.Range("G" & i.ToString()).Value2  ' Deposit trans
                dgView.Item(7, i - 2).Value = sheet.Range("H" & i.ToString()).Value2  ' Deposit Max Amt
                dgView.Item(8, i - 2).Value = sheet.Range("I" & i.ToString()).Value2  ' withdraw
                dgView.Item(9, i - 2).Value = sheet.Range("J" & i.ToString()).Value2  ' withdraw trans
                dgView.Item(10, i - 2).Value = sheet.Range("K" & i.ToString()).Value2 ' withdraw  Max Amount
                dgView.Item(11, i - 2).Value = sheet.Range("L" & i.ToString()).Value2 ' TIN
                dgView.Item(12, i - 2).Value = sheet.Range("M" & i.ToString()).Value2 ' BIN
                dgView.Item(13, i - 2).Value = sheet.Range("N" & i.ToString()).Value2 ' VAT REGi No
                dgView.Item(14, i - 2).Value = sheet.Range("O" & i.ToString()).Value  ' VAT regi date
                dgView.Item(15, i - 2).Value = sheet.Range("P" & i.ToString()).Value2 ' Company Regi no
                dgView.Item(16, i - 2).Value = sheet.Range("Q" & i.ToString()).Value  ' Company Regi date
                dgView.Item(17, i - 2).Value = sheet.Range("R" & i.ToString()).Value2 ' Registration Authority
                dgView.Item(18, i - 2).Value = sheet.Range("S" & i.ToString()).Value2 ' Present Address
                dgView.Item(19, i - 2).Value = sheet.Range("T" & i.ToString()).Value2 ' Permanent Address
                dgView.Item(20, i - 2).Value = sheet.Range("U" & i.ToString()).Value2 ' Phone Resident
                dgView.Item(21, i - 2).Value = sheet.Range("V" & i.ToString()).Value2 ' Phone Office
                dgView.Item(22, i - 2).Value = sheet.Range("W" & i.ToString()).Value2 ' Mobile No
                dgView.Item(23, i - 2).Value = sheet.Range("X" & i.ToString()).Value2 ' Currency Type
                dgView.Item(24, i - 2).Value = sheet.Range("Y" & i.ToString()).Value2 ' Client Number
                dgView.Item(25, i - 2).Value = sheet.Range("Z" & i.ToString()).Value2 ' Account Type
                dgView.Item(26, i - 2).Value = sheet.Range("AA" & i.ToString()).Value ' Open date
                dgView.Item(27, i - 2).Value = sheet.Range("AB" & i.ToString()).Value ' close Date
                dgView.Item(28, i - 2).Value = sheet.Range("AC" & i.ToString()).Value2 ' Status 
                dgView.Item(29, i - 2).Value = sheet.Range("AD" & i.ToString()).Value2 ' Beneficiary
                dgView.Item(30, i - 2).Value = sheet.Range("AE" & i.ToString()).Value2 ' beneficiary Comments
                dgView.Item(31, i - 2).Value = sheet.Range("AF" & i.ToString()).Value2 ' Comments

                _ToTRecordNo = _ToTRecordNo + 1
                SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())

            Next
            dgView.AllowUserToAddRows = False









            wb.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        Try
            xlApp.Quit()
        Catch ex As Exception

        End Try




    End Sub
    Private Sub CheckValidation()


        Dim tmpAccount As String = ""
        Dim strErrMsg As String = ""

        Dim RecGroup As String = ""

        Try



            For Each row As DataGridViewRow In dgView.Rows

                strErrMsg = ""
                flagRequireField = False


                If NullHelper.ObjectToString(row.Cells(0).Value) <> "" Then


                    If NullHelper.ObjectToString(row.Cells(1).Value).Trim() = "" Then ' Branch

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(2).Value).Trim() = "" Then ' Ac Title

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(3).Value).Trim() = "" Then ' account Type

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(4).Value).Trim() = "" Then ' Ownership Type

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(23).Value).Trim() = "" Then ' Currency Type

                        flagRequireField = True

                    End If

                    'If TypeChecker.IsContained("regularity_authority", NullHelper.ObjectToString(row.Cells(17).Value), True) = False Then

                    '    strErrMsg = strErrMsg + " | Invalid Registration Authority Code " + """" + row.Cells(17).Value.ToString() + """"
                    '    flagRequireField = True
                    'End If


                    If TypeChecker.IsContained("currency_type", NullHelper.ObjectToString(row.Cells(23).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Account Currency Type Code " + """" + row.Cells(23).Value.ToString() + """"
                        flagRequireField = True
                    End If


                    If TypeChecker.IsContained("account_type", NullHelper.ObjectToString(row.Cells(25).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Account Type Code(goAML) " + """" + row.Cells(25).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If NullHelper.ObjectToString(row.Cells(25).Value).Trim() = "" Then ' Account Type

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(26).Value).Trim() = "" Then ' Open date

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(28).Value).Trim() = "" Then ' Status

                        flagRequireField = True

                    End If


                    If TypeChecker.IsContained("account_status_type", NullHelper.ObjectToString(row.Cells(28).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Account Status Type Code(goAML) " + """" + row.Cells(28).Value.ToString() + """"
                        flagRequireField = True
                    End If



                End If


                If flagRequireField = True Then
                    strErrMsg = "| Require Field Missing |" & strErrMsg
                End If


                If flagRequireField = True Then

                    row.Cells(32).Value = 1
                    row.DefaultCellStyle.BackColor = Color.Red
                    row.Cells(33).Value = strErrMsg
                    btnPrepareAccount.Enabled = False



                End If


            Next

            '        ' lblTotalCheckAmount.Text = CheckTotal.ToString()
            '        ' lblTotalCheckNo.Text = CheckNoTotal.ToString()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub



    Private Function CheckValidData() As Boolean


        Return True

    End Function

    Private Function SaveData() As TransState

        Dim tStatus As TransState

        Dim intModno As Integer = 0
        Dim _intModno As Integer = 0
        Dim intMod As Integer = 0
        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Using conn As DbConnection = db.CreateConnection()

            conn.Open()

            Dim trans As DbTransaction = conn.BeginTransaction()

            Dim strSql = ""

            Dim commProc As DbCommand
            Dim dtAccountInfo As DataTable
            Dim BankCode As String = "026"

            For i = 0 To dgView.Rows.Count - 1
                _debugRow = i + 1
                'FIU_ACCOUNT_INFO

                strSql = "DELETE FIU_ACCOUNT_INFO " & _
                         "WHERE ACNUMBER=@P_ACCOUNT AND BANK_CODE=@P_BANK_CODE AND BRANCH_CODE = @P_BRANCH_CODE AND IS_AUTHORIZED=0"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dgView.Rows(i).Cells(0).Value.ToString())
                db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, BankCode.ToString())
                db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dgView.Rows(i).Cells(1).Value.ToString())

                db.ExecuteNonQuery(commProc, trans)


                strSql = "SELECT ACNUMBER,BANK_CODE,BRANCH_CODE, MODNO,STATUS FROM FIU_ACCOUNT_INFO " & _
                         "WHERE ACNUMBER=@P_ACCOUNT AND BANK_CODE=@P_BANK_CODE AND BRANCH_CODE = @P_BRANCH_CODE AND [STATUS]='L'"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dgView.Rows(i).Cells(0).Value.ToString())
                db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, BankCode.ToString())
                db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dgView.Rows(i).Cells(1).Value.ToString())

                dtAccountInfo = db.ExecuteDataSet(commProc, trans).Tables(0)

                If dtAccountInfo.Rows.Count > 0 Then

                    intMod = NullHelper.ToIntNum(dtAccountInfo.Rows(0)("MODNO"))

                    strSql = "INSERT INTO FIU_ACCOUNT_INFO(BANK_CODE, BRANCH_CODE, ACNUMBER, AC_TITLE, ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_OFFICE1, MOBILE1, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,STATUS) " & _
                             "VALUES(@P_BANK_CODE, @P_BRANCH_CODE, @P_ACCOUNT, @P_AC_TITLE, @P_ACCOUNT_TYPE, @P_OWNER_TYPE, @P_DEPOSIT, @P_DEPOSIT_TRANS, @P_DEPOSIT_MAX, @P_WITHWRAW, @P_WITHDRAW_TRANS, @P_WITHDRAW_MAX, @P_TIN, @P_BIN, @P_VAT_REGI, @P_VAT_REGI_DATE, @P_COMPANY_REGI,@P_COMPANY_REGI_DATE,@P_COMPANY_REGI_AUTH, @P_PRESENT_ADDRESS, @P_PERMANENT_ADDRESS, @P_PHONE_RESIDENT,@P_PHONE_OFFICE,@P_MOBILE,  @P_MODNO, @P_INPUT_BY, GETDATE(), 0,'U')"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()

                    db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dgView.Rows(i).Cells(0).Value.ToString())
                    db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, BankCode.ToString())
                    db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dgView.Rows(i).Cells(1).Value.ToString())
                    db.AddInParameter(commProc, "@P_AC_TITLE", DbType.String, dgView.Rows(i).Cells(2).Value)
                    db.AddInParameter(commProc, "@P_ACCOUNT_TYPE", DbType.String, dgView.Rows(i).Cells(3).Value)
                    db.AddInParameter(commProc, "@P_OWNER_TYPE", DbType.String, dgView.Rows(i).Cells(4).Value)

                    db.AddInParameter(commProc, "@P_DEPOSIT", DbType.Decimal, dgView.Rows(i).Cells(5).Value)
                    db.AddInParameter(commProc, "@P_DEPOSIT_TRANS", DbType.Int32, dgView.Rows(i).Cells(6).Value)
                    db.AddInParameter(commProc, "@P_DEPOSIT_MAX", DbType.Decimal, dgView.Rows(i).Cells(7).Value)

                    db.AddInParameter(commProc, "@P_WITHWRAW", DbType.Decimal, dgView.Rows(i).Cells(8).Value)
                    db.AddInParameter(commProc, "@P_WITHDRAW_TRANS", DbType.Int32, dgView.Rows(i).Cells(9).Value)
                    db.AddInParameter(commProc, "@P_WITHDRAW_MAX", DbType.Decimal, dgView.Rows(i).Cells(10).Value)

                    db.AddInParameter(commProc, "@P_TIN", DbType.String, dgView.Rows(i).Cells(11).Value)
                    db.AddInParameter(commProc, "@P_BIN", DbType.String, dgView.Rows(i).Cells(12).Value)
                    db.AddInParameter(commProc, "@P_VAT_REGI", DbType.String, dgView.Rows(i).Cells(13).Value)
                    db.AddInParameter(commProc, "@P_VAT_REGI_DATE", DbType.DateTime, dgView.Rows(i).Cells(14).Value)

                    db.AddInParameter(commProc, "@P_COMPANY_REGI", DbType.String, dgView.Rows(i).Cells(15).Value)
                    db.AddInParameter(commProc, "@P_COMPANY_REGI_DATE", DbType.DateTime, dgView.Rows(i).Cells(16).Value)
                    db.AddInParameter(commProc, "@P_COMPANY_REGI_AUTH", DbType.String, dgView.Rows(i).Cells(17).Value)


                    db.AddInParameter(commProc, "@P_PRESENT_ADDRESS", DbType.String, dgView.Rows(i).Cells(18).Value)
                    db.AddInParameter(commProc, "@P_PERMANENT_ADDRESS", DbType.String, dgView.Rows(i).Cells(19).Value)
                    db.AddInParameter(commProc, "@P_PHONE_RESIDENT", DbType.String, dgView.Rows(i).Cells(20).Value)
                    db.AddInParameter(commProc, "@P_PHONE_OFFICE", DbType.String, dgView.Rows(i).Cells(21).Value)
                    db.AddInParameter(commProc, "@P_MOBILE", DbType.String, dgView.Rows(i).Cells(22).Value)
                    
                    db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, CommonAppSet.User)

                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)

                    db.ExecuteNonQuery(commProc, trans)

                Else

                    strSql = "INSERT INTO FIU_ACCOUNT_INFO(BANK_CODE, BRANCH_CODE, ACNUMBER, AC_TITLE, ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_OFFICE1, MOBILE1, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,STATUS) " & _
                            "VALUES(@P_BANK_CODE, @P_BRANCH_CODE, @P_ACCOUNT, @P_AC_TITLE, @P_ACCOUNT_TYPE, @P_OWNER_TYPE, @P_DEPOSIT, @P_DEPOSIT_TRANS, @P_DEPOSIT_MAX, @P_WITHWRAW, @P_WITHDRAW_TRANS, @P_WITHDRAW_MAX, @P_TIN, @P_BIN, @P_VAT_REGI, @P_VAT_REGI_DATE, @P_COMPANY_REGI,@P_COMPANY_REGI_DATE,@P_COMPANY_REGI_AUTH, @P_PRESENT_ADDRESS, @P_PERMANENT_ADDRESS, @P_PHONE_RESIDENT,@P_PHONE_OFFICE,@P_MOBILE,  @P_MODNO, @P_INPUT_BY, GETDATE(), 0,'U')"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()

                    db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dgView.Rows(i).Cells(0).Value.ToString())
                    db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, BankCode.ToString())
                    db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dgView.Rows(i).Cells(1).Value.ToString())
                    db.AddInParameter(commProc, "@P_AC_TITLE", DbType.String, dgView.Rows(i).Cells(2).Value)
                    db.AddInParameter(commProc, "@P_ACCOUNT_TYPE", DbType.String, dgView.Rows(i).Cells(3).Value)
                    db.AddInParameter(commProc, "@P_OWNER_TYPE", DbType.String, dgView.Rows(i).Cells(4).Value)

                    db.AddInParameter(commProc, "@P_DEPOSIT", DbType.Decimal, dgView.Rows(i).Cells(5).Value)
                    db.AddInParameter(commProc, "@P_DEPOSIT_TRANS", DbType.Int32, dgView.Rows(i).Cells(6).Value)
                    db.AddInParameter(commProc, "@P_DEPOSIT_MAX", DbType.Decimal, dgView.Rows(i).Cells(7).Value)

                    db.AddInParameter(commProc, "@P_WITHWRAW", DbType.Decimal, dgView.Rows(i).Cells(8).Value)
                    db.AddInParameter(commProc, "@P_WITHDRAW_TRANS", DbType.Int32, dgView.Rows(i).Cells(9).Value)
                    db.AddInParameter(commProc, "@P_WITHDRAW_MAX", DbType.Decimal, dgView.Rows(i).Cells(10).Value)

                    db.AddInParameter(commProc, "@P_TIN", DbType.String, dgView.Rows(i).Cells(11).Value)
                    db.AddInParameter(commProc, "@P_BIN", DbType.String, dgView.Rows(i).Cells(12).Value)
                    db.AddInParameter(commProc, "@P_VAT_REGI", DbType.String, dgView.Rows(i).Cells(13).Value)
                    db.AddInParameter(commProc, "@P_VAT_REGI_DATE", DbType.DateTime, dgView.Rows(i).Cells(14).Value)

                    db.AddInParameter(commProc, "@P_COMPANY_REGI", DbType.String, dgView.Rows(i).Cells(15).Value)
                    db.AddInParameter(commProc, "@P_COMPANY_REGI_DATE", DbType.DateTime, dgView.Rows(i).Cells(16).Value)
                    db.AddInParameter(commProc, "@P_COMPANY_REGI_AUTH", DbType.String, dgView.Rows(i).Cells(17).Value)


                    db.AddInParameter(commProc, "@P_PRESENT_ADDRESS", DbType.String, dgView.Rows(i).Cells(18).Value)
                    db.AddInParameter(commProc, "@P_PERMANENT_ADDRESS", DbType.String, dgView.Rows(i).Cells(19).Value)
                    db.AddInParameter(commProc, "@P_PHONE_RESIDENT", DbType.String, dgView.Rows(i).Cells(20).Value)
                    db.AddInParameter(commProc, "@P_PHONE_OFFICE", DbType.String, dgView.Rows(i).Cells(21).Value)
                    db.AddInParameter(commProc, "@P_MOBILE", DbType.String, dgView.Rows(i).Cells(22).Value)

                    db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, CommonAppSet.User)

                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, 1)

                    db.ExecuteNonQuery(commProc, trans)




                End If

                'GO_ACCOUNT_INFO


                strSql = "DELETE GO_ACCOUNT_INFO_HIST " & _
                         "WHERE ACNUMBER=@P_ACCOUNT AND IS_AUTH=0"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dgView.Rows(i).Cells(0).Value.ToString())
                
                db.ExecuteNonQuery(commProc, trans)


                strSql = "SELECT ACNUMBER,MOD_NO,ENTITY_ID FROM GO_ACCOUNT_INFO " & _
                         "WHERE ACNUMBER=@P_ACCOUNT AND [STATUS]='L'"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dgView.Rows(i).Cells(0).Value.ToString())
                
                dtAccountInfo = db.ExecuteDataSet(commProc, trans).Tables(0)


                If dtAccountInfo.Rows.Count > 0 Then

                    intMod = NullHelper.ToIntNum(dtAccountInfo.Rows(0)("MOD_NO"))


                    strSql = "INSERT INTO GO_ACCOUNT_INFO_HIST(ACNUMBER, CURRENCY_CODE,CLIENT_NUMBER, ACCOUNT_TYPE, OPENED, CLOSED, STATUS_CODE, BENEFICIARY, BENEFICIARY_COMMENTS, COMMENTS, INPUT_BY, INPUT_DATETIME, MOD_NO, STATUS, IS_AUTH, ENTITY_ID) " & _
                             "VALUES(@P_ACCOUNT, @CURRENCY_CODE, @CLIENT_NUMBER, @ACCOUNT_TYPE, @OPENED, @CLOSED, @STATUS_CODE, @BENEFICIARY, @BENEFICIARY_COMMENTS, @COMMENTS, @P_INPUT_BY, GETDATE(), @P_MODNO, 'U', 0, @ENTITY_ID)"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()

                    db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dgView.Rows(i).Cells(0).Value.ToString())
                    db.AddInParameter(commProc, "@CURRENCY_CODE", DbType.String, dgView.Rows(i).Cells(23).Value)
                    db.AddInParameter(commProc, "@CLIENT_NUMBER", DbType.String, dgView.Rows(i).Cells(24).Value)
                    db.AddInParameter(commProc, "@ACCOUNT_TYPE", DbType.String, dgView.Rows(i).Cells(25).Value)
                    db.AddInParameter(commProc, "@OPENED", DbType.DateTime, dgView.Rows(i).Cells(26).Value)
                    db.AddInParameter(commProc, "@CLOSED", DbType.DateTime, dgView.Rows(i).Cells(27).Value)
                    db.AddInParameter(commProc, "@STATUS_CODE", DbType.String, dgView.Rows(i).Cells(28).Value)

                    db.AddInParameter(commProc, "@BENEFICIARY", DbType.String, dgView.Rows(i).Cells(29).Value)
                    db.AddInParameter(commProc, "@BENEFICIARY_COMMENTS", DbType.String, dgView.Rows(i).Cells(30).Value)
                    db.AddInParameter(commProc, "@COMMENTS", DbType.String, dgView.Rows(i).Cells(31).Value)


                    db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, CommonAppSet.User)
                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)
                    db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, dtAccountInfo.Rows(0)("ENTITY_ID"))


                    db.ExecuteNonQuery(commProc, trans)

                Else

                    strSql = "INSERT INTO GO_ACCOUNT_INFO_HIST(ACNUMBER, CURRENCY_CODE,CLIENT_NUMBER, ACCOUNT_TYPE, OPENED, CLOSED, STATUS_CODE, BENEFICIARY, BENEFICIARY_COMMENTS, COMMENTS, INPUT_BY, INPUT_DATETIME, MOD_NO, STATUS, IS_AUTH) " & _
                             "VALUES(@P_ACCOUNT, @CURRENCY_CODE, @CLIENT_NUMBER, @ACCOUNT_TYPE, @OPENED, @CLOSED, @STATUS_CODE, @BENEFICIARY, @BENEFICIARY_COMMENTS, @COMMENTS, @P_INPUT_BY, GETDATE(), @P_MODNO, 'U', 0)"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()

                    db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dgView.Rows(i).Cells(0).Value.ToString())
                    db.AddInParameter(commProc, "@CURRENCY_CODE", DbType.String, dgView.Rows(i).Cells(23).Value)
                    db.AddInParameter(commProc, "@CLIENT_NUMBER", DbType.String, dgView.Rows(i).Cells(24).Value)
                    db.AddInParameter(commProc, "@ACCOUNT_TYPE", DbType.String, dgView.Rows(i).Cells(25).Value)
                    db.AddInParameter(commProc, "@OPENED", DbType.DateTime, dgView.Rows(i).Cells(26).Value)
                    db.AddInParameter(commProc, "@CLOSED", DbType.DateTime, dgView.Rows(i).Cells(27).Value)
                    db.AddInParameter(commProc, "@STATUS_CODE", DbType.String, dgView.Rows(i).Cells(28).Value)

                    db.AddInParameter(commProc, "@BENEFICIARY", DbType.String, dgView.Rows(i).Cells(29).Value)
                    db.AddInParameter(commProc, "@BENEFICIARY_COMMENTS", DbType.String, dgView.Rows(i).Cells(30).Value)
                    db.AddInParameter(commProc, "@COMMENTS", DbType.String, dgView.Rows(i).Cells(31).Value)


                    db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, CommonAppSet.User)
                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, 1)
                    

                    db.ExecuteNonQuery(commProc, trans)



                End If



            Next


            tStatus = TransState.Add




            trans.Commit()

            

        End Using

        log_message = " Uploaded : Account Information "
        Logger.system_log(log_message)



        Return tStatus

    End Function


    Private Sub FrmImportAccountInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click

        opdRetFile.Multiselect = False
        'opdRetFile.Filter() = "XML Files (*.XML)|(*.XML)"
        opdRetFile.FileName = txtFilename.Text

        opdRetFile.ShowDialog()
        txtFilename.Text = opdRetFile.FileName()
    End Sub

    Private Sub btnImportProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportProcess.Click
        If Not File.Exists(txtFilename.Text) Then
            MessageBox.Show("Select File", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnBrowse.Focus()
            Exit Sub
        End If


        ' btnPrepareAccount.Enabled = False

        ReadExcel()
        CheckValidation()

        'If flagRequireField = True Then
        '    btnPrepareAccount.Enabled = False
        'Else
        '    btnPrepareAccount.Enabled = True
        'End If
    End Sub

    Private Sub btnPrepareAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrepareAccount.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try
            If MessageBox.Show("Do you really want to Save?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                If CheckValidData() Then

                    tState = SaveData()

                    If tState = TransState.Add Then

                        lblToolStatus.Text = "!! Information Updated Successfully !!"

                        MessageBox.Show("Information Updated Successfull." & Environment.NewLine & _
                                        "** Separate authorization needed from Account Info,  GoAml Account info Form", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ElseIf tState = TransState.Exist Then
                        lblToolStatus.Text = "!! Already Exist !!"
                    ElseIf tState = TransState.NoRecord Then
                        lblToolStatus.Text = "!! Nothing to Update !!"
                    ElseIf tState = TransState.DBError Then
                        lblToolStatus.Text = "!! Database error occured. Please, Try Again !!"
                    ElseIf tState = TransState.UnspecifiedError Then
                        lblToolStatus.Text = "!! Unpecified Error Occured !!"
                    End If

                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message + Environment.NewLine + "Error Row: " + _debugRow.ToString(), "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
End Class