Imports System.IO
Imports System.Globalization
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common
Imports CTR.Common
'Imports System.Windows.Forms




Public Class FrmImportFlexCustGo

    Dim _formName As String = "ToolsImportFlexCustomerGoAML"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""
    Dim strSql As String = ""


    Dim _ProcessSuccess As Boolean = False
    Dim _ToTRecordNo As Integer = 0

    Public Delegate Sub ChangeTextOfLabelDelegate( _
                            ByVal ToLabel As Label, _
                            ByVal AddText As String)


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()


    End Sub

    Private Sub FrmImportFlexTrans_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If BackgroundWorker1.IsBusy = True Or BackgroundWorker2.IsBusy = True Then
            MessageBox.Show("Process is running.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            e.Cancel = True
        End If

    End Sub

    Private Sub FrmImportFlexTrans_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub

    Private Sub SetFileImpStatus(ByVal strMsg As String)

        'txtFileImpStatus.AppendText(strMsg)


        If txtFileImpStatus.InvokeRequired Then
            txtFileImpStatus.Invoke(New Action(Of System.String)(AddressOf SetFileImpStatus), strMsg)
        Else
            txtFileImpStatus.AppendText(strMsg)
        End If

    End Sub

    Private Sub SetLabelText(ByVal lblName As System.Windows.Forms.Label, ByVal strMsg As String)

        'txtFileImpStatus.AppendText(strMsg)


        If lblName.InvokeRequired Then
            'lblName.Invoke(New Action(Of System.String)(AddressOf SetLabelText), strMsg)
            lblName.Invoke(New ChangeTextOfLabelDelegate(AddressOf SetLabelText), New Object() {lblName, strMsg})
        Else
            lblName.Text = strMsg
        End If

    End Sub



    Private Sub ImportMakerFiles()


        lblToolStatus.Text = ""

        Dim intTotFile As Integer = 0
        Dim intFailed As Integer = 0

        Try



            _ToTRecordNo = 0
            intFailed = 0

            Dim fileNames = My.Computer.FileSystem.GetFiles(txtFolderPath.Text, _
                                                            FileIO.SearchOption.SearchTopLevelOnly, "*.dat")
            intTotFile = fileNames.Count()

            SetLabelText(lblTotFile, intTotFile.ToString())

            For Each fileName As String In fileNames
                'filesListBox.Items.Add(fileName)
                If ReadDataFile(fileName) = False Then
                    'txtFileImpStatus.AppendText(Environment.NewLine + "Failed: " + fileName)
                    SetFileImpStatus(Environment.NewLine + "Failed: " + fileName)
                    intFailed = intFailed + 1
                    SetLabelText(lblFaileFileNo, intFailed.ToString())
                End If
            Next
            '-------

            _ProcessSuccess = True


        Catch ex As Exception


            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)


        End Try

        'lblFaileFileNo.Text = intFailed.ToString()
        'lblTotRecNo.Text = _ToTRecordNo.ToString()
        'lblTotFile.Text = intTotFile.ToString()

        SetLabelText(lblFaileFileNo, intFailed.ToString())
        SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())
        'SetLabelText(lblTotFile, intTotFile.ToString())


    End Sub



    Private Function ReadDataFile(ByVal strFilename As String) As Boolean

        Dim retVal As Boolean = False

        Dim chrSep As Char() = {"~"}
        Dim strLine As String = ""
        Dim strArLine As String()
        Dim dtTrans As New DateTime
        Dim strSql As String = ""
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable


        'Dim oFile As System.IO.File

        If strFilename.Trim = "" Then
            Exit Function
        End If

        Dim oRead As System.IO.StreamReader

        Try
            oRead = File.OpenText(strFilename)

            SetFileImpStatus(Environment.NewLine + "----------------")
            SetFileImpStatus(Environment.NewLine + "Filename:" + strFilename)

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                Dim commProc As DbCommand

                ' Delete IMP_FLEX_TRANS_M data



                ' --------------------------------

                Dim SLNO As Integer = 0
                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()


                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        If strArLine(0).Trim() = "HH" Then

                            If strArLine(4).Trim() = "" Then
                                'MessageBox.Show("Date missing in Header." & Environment.NewLine & "Use valid file", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                SetFileImpStatus(Environment.NewLine + "Date missing in Header. Invalid file")
                                Exit Function
                            End If

                            dtTrans = DateTime.ParseExact(strArLine(4), "yyyyMMdd", New CultureInfo("en-US"))

                            'MessageBox.Show("Transaction of " & dtTrans.ToShortDateString)
                            SetFileImpStatus(Environment.NewLine + "Transaction Date:" + dtTrans.ToShortDateString)

                            '-----------------------

                            strSql = "delete IMP_FLEX_CUST_M where IMPORTED_DATE=@P_IMPORTED_DATE"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                            db.ExecuteNonQuery(commProc, trans)



                        End If

                        If strArLine(0).Trim() = "D" Then

                            SLNO = SLNO + 1

                            strSql = "insert into IMP_FLEX_CUST_M(IMPORTED_DATE, ACC_BRANCH, ACCOUNT, " & _
                                "DESCRIPTION, ACCOUNT_OPENING_DATE, CCY, CURRENT_BALANCE_ACC_LCY, " & _
                                "CUSTOMER_ID, ACCOUNT_CLASS, BBK_DEFINITION,SLNO,[USER_NAME]) " & _
                                "values(@P_IMPORTED_DATE, @P_ACC_BRANCH, @P_ACCOUNT, " & _
                                "@P_DESCRIPTION, @P_ACCOUNT_OPENING_DATE, @P_CCY, @P_CURRENT_BALANCE_ACC_LCY, " & _
                                "@P_CUSTOMER_ID, @P_ACCOUNT_CLASS, @P_BBK_DEFINITION,@P_SLNO,@P_USER_NAME)"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()
                            db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)
                            db.AddInParameter(commProc, "@P_ACC_BRANCH", DbType.String, strArLine(5).Trim())
                            db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, strArLine(6).Trim())
                            db.AddInParameter(commProc, "@P_DESCRIPTION", DbType.String, strArLine(7).Trim())
                            db.AddInParameter(commProc, "@P_ACCOUNT_OPENING_DATE", DbType.Date, DateTime.ParseExact(strArLine(8), "yyyyMMdd", New CultureInfo("en-US")))
                            db.AddInParameter(commProc, "@P_CCY", DbType.String, strArLine(9).Trim())
                            db.AddInParameter(commProc, "@P_CURRENT_BALANCE_ACC_LCY", DbType.Decimal, NullHelper.ToDecNum(strArLine(16).Trim()))
                            db.AddInParameter(commProc, "@P_CUSTOMER_ID", DbType.String, strArLine(22).Trim())
                            db.AddInParameter(commProc, "@P_ACCOUNT_CLASS", DbType.String, strArLine(32).Trim())
                            db.AddInParameter(commProc, "@P_BBK_DEFINITION", DbType.String, strArLine(61).Trim())
                            db.AddInParameter(commProc, "@P_SLNO", DbType.Int32, SLNO)
                            db.AddInParameter(commProc, "@P_USER_NAME", DbType.String, CommonAppSet.User)

                            Dim result As Integer
                            result = db.ExecuteNonQuery(commProc, trans)
                            If result < 0 Then
                                'MessageBox.Show("Import Error ", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                SetFileImpStatus(Environment.NewLine + "Import Error")
                                trans.Rollback()
                                Exit Function
                            Else
                            End If

                            _ToTRecordNo = _ToTRecordNo + 1
                            SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())
                        End If
                    End If
                    'MessageBox.Show(strLine)
                Loop


                trans.Commit()

                retVal = True
                SetFileImpStatus(Environment.NewLine + "Record(s): " + SLNO.ToString())
                SetFileImpStatus(Environment.NewLine + "Success")
                'log_message = "Import Flex Transaction File Date " + dtTrans.ToString() + " By " + CommonAppSet.User.ToString()
                log_message = " Imported : Flex Customer File Date : " + dtTrans.ToString() + "." + " " + " [maker]"
                Logger.system_log(log_message)

                'MessageBox.Show("Import Successfull", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End Using




            oRead.Close()

        Catch ex As Exception
            SetFileImpStatus(Environment.NewLine + ex.Message)
            'MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        Return retVal

    End Function



    Private Function AuthDataFileOld(ByVal strFilename As String) As TransState

        Dim tStatus As TransState
        tStatus = TransState.UnspecifiedError

        Dim chrSep As Char() = {"~"}
        Dim strLine As String = ""
        Dim strArLine As String()
        Dim dtTrans As New DateTime
        Dim strSql As String = ""
        Dim dt As New DataTable

        Dim dt2 As New DataTable

        Dim flagHeaderFound As Boolean = False

        'Dim oFile As System.IO.File

        If strFilename.Trim = "" Then
            Exit Function
        End If

        Dim db2 As New SqlDatabase(CommonAppSet.ConnStr)

        Dim oRead As System.IO.StreamReader

        Try
            oRead = File.OpenText(strFilename)

            SetFileImpStatus(Environment.NewLine + "----------------")
            SetFileImpStatus(Environment.NewLine + "Filename:" + strFilename)

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                'Dim trans As DbTransaction = conn.BeginTransaction()

                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()

                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        If strArLine(0).Trim() = "HH" Then

                            If strArLine(4).Trim() = "" Then
                                'MessageBox.Show("Date missing in Header." & Environment.NewLine & "Use valid file", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                SetFileImpStatus(Environment.NewLine + "Date missing in Header. Invalid file")
                                Exit Function

                            End If

                            dtTrans = DateTime.ParseExact(strArLine(4), "yyyyMMdd", New CultureInfo("en-US"))

                            'MessageBox.Show("Transaction of " & dtTrans.ToShortDateString)
                            SetFileImpStatus(Environment.NewLine + "Transaction Date:" + dtTrans.ToShortDateString)
                            '--------

                            Dim ds As New DataSet

                            strSql = "Select distinct USER_NAME From IMP_FLEX_CUST_M WHERE IMPORTED_DATE=@P_IMPORTED_DATE"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()


                            db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                            ds = db2.ExecuteDataSet(commProc)

                            If ds.Tables(0).Rows.Count > 0 Then

                                If ds.Tables(0).Rows(0)("USER_NAME").ToString().ToUpper() = CommonAppSet.User.Trim().ToUpper() Then
                                    Return TransState.MakerCheckerSame
                                    'Exit Function

                                End If

                                flagHeaderFound = True

                                Exit Do

                                '------------------------------------
                            Else
                                Return TransState.NoRecord
                            End If

                        End If
                    End If

                Loop

                '----------------------
                '-------------------------

                'Dim commProc As DbCommand

                strSql = "select * From IMP_FLEX_CUST_M WHERE IMPORTED_DATE=@P_IMPORTED_DATE ORDER BY SLNO ASC"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                dt2 = db.ExecuteDataSet(commProc).Tables(0)

                Dim i As Integer = 0

                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()

                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        If strArLine(0).Trim() = "D" Then

                            If i >= dt2.Rows.Count Then
                                Return TransState.UpdateNotPossible
                            End If

                            If dt2.Rows(i)("ACC_BRANCH") <> strArLine(5).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACCOUNT") <> strArLine(6).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("DESCRIPTION") <> strArLine(7).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACCOUNT_OPENING_DATE") <> DateTime.ParseExact(strArLine(8), "yyyyMMdd", New CultureInfo("en-US")) Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("CCY") <> strArLine(9).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("CURRENT_BALANCE_ACC_LCY") <> NullHelper.ToDecNum(strArLine(16).Trim()) Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("CUSTOMER_ID") <> strArLine(22).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACCOUNT_CLASS") <> strArLine(32).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("BBK_DEFINITION") <> strArLine(61).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If


                            i = i + 1

                            '_ToTRecordNo = _ToTRecordNo + 1

                            'SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())

                        End If
                    End If
                    'MessageBox.Show(strLine)
                Loop

                If i <> dt2.Rows.Count Then
                    Return TransState.UpdateNotPossible
                End If

                '---- checking complete
                '---- now have to update tables

                Dim trans As DbTransaction = conn.BeginTransaction()

                '-------
                '-------

                strSql = "select STATUS from STATUS_IMP_FLEX_CUST where IMPORTED_DATE=@P_IMPORTED_DATE"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                dt = db.ExecuteDataSet(commProc, trans).Tables(0)

                If dt.Rows.Count > 0 Then

                    strSql = "delete CUST_BALANCE where IMPORTED_DATE =@P_IMPORTED_DATE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)

                    strSql = "UPDATE STATUS_IMP_FLEX_CUST SET STATUS='N' where IMPORTED_DATE=@P_IMPORTED_DATE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)





                Else '-------- insert new date into STATUS_IMP_FLEX_TRANS table



                    strSql = "insert into STATUS_IMP_FLEX_CUST(IMPORTED_DATE,STATUS) values(@P_IMPORTED_DATE,'N')"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)



                End If



                '-------
                '---------

                'strSql = "delete CUST_BALANCE where IMPORTED_DATE =@P_IMPORTED_DATE"

                'commProc = db.GetSqlStringCommand(strSql)

                'commProc.Parameters.Clear()
                'db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                'db.ExecuteNonQuery(commProc, trans)


                strSql = "delete FIU_ACCOUNT_INFO where IS_AUTHORIZED=0"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.ExecuteNonQuery(commProc, trans)

                strSql = "delete GO_ACCOUNT_INFO_HIST where IS_AUTH=0"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.ExecuteNonQuery(commProc, trans)

                Dim dtBankBranch As New DataTable

                For i = 0 To dt2.Rows.Count - 1



                    strSql = "INSERT INTO  CUST_BALANCE(IMPORTED_DATE, ACCOUNT, CURRENT_BALANCE_ACC_LCY, " & _
                        "ACCOUNT_CLASS, BBK_DEFINITION) " & _
                        "VALUES(@P_IMPORTED_DATE, @P_ACCOUNT, @P_CURRENT_BALANCE_ACC_LCY, " & _
                        "@P_ACCOUNT_CLASS, @P_BBK_DEFINITION)"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)
                    db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dt2.Rows(i)("ACCOUNT"))
                    db.AddInParameter(commProc, "@P_CURRENT_BALANCE_ACC_LCY", DbType.Decimal, dt2.Rows(i)("CURRENT_BALANCE_ACC_LCY"))
                    db.AddInParameter(commProc, "@P_ACCOUNT_CLASS", DbType.String, dt2.Rows(i)("ACCOUNT_CLASS"))
                    db.AddInParameter(commProc, "@P_BBK_DEFINITION", DbType.String, dt2.Rows(i)("BBK_DEFINITION"))

                    db.ExecuteNonQuery(commProc, trans)


                    strSql = "SELECT * FROM CitiBank_Branch WHERE CITIBRANCH_CODE=@P_CITIBRANCH_CODE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_CITIBRANCH_CODE", DbType.String, dt2.Rows(i)("ACCOUNT").ToString().Trim().Substring(0, 3))

                    dtBankBranch = db.ExecuteDataSet(commProc, trans).Tables(0)

                    If dtBankBranch.Rows.Count > 0 Then

                    Else
                        _ToTRecordNo = _ToTRecordNo + 1
                        SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())

                        Continue For
                    End If

                    strSql = "select ACNUMBER,INPUT_BY from FIU_ACCOUNT_INFO where ACNUMBER=@P_ACNUMBER AND [STATUS]='L'"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))

                    dt = db.ExecuteDataSet(commProc, trans).Tables(0)

                    If dt.Rows.Count > 0 Then

                        'If dt.Rows(0)("").ToString().ToUpper() = "SYSTEM" Then

                        strSql = "UPDATE FIU_ACCOUNT_INFO " & _
                        "SET AC_TITLE = @P_AC_TITLE, INPUT_DATETIME=GETDATE(), AUTH_DATETIME=GETDATE() " & _
                        "where ACNUMBER=@P_ACNUMBER AND [STATUS]='L'"

                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                        db.AddInParameter(commProc, "@P_AC_TITLE", DbType.String, dt2.Rows(i)("DESCRIPTION"))

                        db.ExecuteNonQuery(commProc, trans)

                        'Else



                        'End If

                    Else

                        '---------------------
                        strSql = "select ACNUMBER from FIU_ACCOUNT_INFO where ACNUMBER=@P_ACNUMBER AND [STATUS]='D'"

                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))

                        If db.ExecuteDataSet(commProc, trans).Tables(0).Rows.Count = 0 Then

                            strSql = "INSERT INTO  FIU_ACCOUNT_INFO(BANK_CODE, BRANCH_CODE, ACNUMBER, AC_TITLE, MODNO, INSERTED_ON, INPUT_BY, INPUT_DATETIME,IS_AUTHORIZED, AUTH_BY, AUTH_DATETIME, [STATUS]) " & _
                                "VALUES(@P_BANK_CODE, @P_BRANCH_CODE, @P_ACNUMBER, @P_AC_TITLE, 1, @INSERTED_ON, 'SYSTEM', GETDATE(),1, 'SYSTEM', GETDATE(), 'L')"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()
                            db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, dtBankBranch.Rows(0)("Bank_Code"))
                            db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dtBankBranch.Rows(0)("Branch_Code"))
                            db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                            db.AddInParameter(commProc, "@P_AC_TITLE", DbType.String, dt2.Rows(i)("DESCRIPTION"))
                            db.AddInParameter(commProc, "@INSERTED_ON", DbType.DateTime, dt2.Rows(i)("ACCOUNT_OPENING_DATE"))


                            db.ExecuteNonQuery(commProc, trans)

                        End If


                        '---------------------



                    End If

                    ''----go

                    strSql = "select ACNUMBER,INPUT_BY from GO_ACCOUNT_INFO where ACNUMBER=@P_ACNUMBER AND [STATUS] IN ('L','D')"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))

                    dt = db.ExecuteDataSet(commProc, trans).Tables(0)

                    If dt.Rows.Count > 0 Then

                        'If dt.Rows(0)("").ToString().ToUpper() = "SYSTEM" Then

                        strSql = "UPDATE GO_ACCOUNT_INFO " & _
                        "SET CURRENCY_CODE = @P_CURRENCY_CODE, CLIENT_NUMBER=@P_CLIENT_NUMBER, ACCOUNT_TYPE=@P_ACCOUNT_TYPE,OPENED=@P_OPENED,INPUT_DATETIME=GETDATE(), AUTH_DATETIME=GETDATE() " & _
                        "where ACNUMBER=@P_ACNUMBER AND [STATUS]='L'"

                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()

                        db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                        db.AddInParameter(commProc, "@P_CURRENCY_CODE", DbType.String, dt2.Rows(i)("CCY"))
                        db.AddInParameter(commProc, "@P_CLIENT_NUMBER", DbType.String, dt2.Rows(i)("CUSTOMER_ID"))
                        db.AddInParameter(commProc, "@P_ACCOUNT_TYPE", DbType.String, TypeHelper.AccTypeFlexToGo(dt2.Rows(i)("BBK_DEFINITION")))
                        db.AddInParameter(commProc, "@P_OPENED", DbType.DateTime, dt2.Rows(i)("ACCOUNT_OPENING_DATE"))


                        db.ExecuteNonQuery(commProc, trans)

                        'Else



                        'End If

                    Else

                        strSql = "INSERT INTO  GO_ACCOUNT_INFO(ACNUMBER, CURRENCY_CODE, CLIENT_NUMBER, ACCOUNT_TYPE, OPENED, STATUS_CODE, INPUT_BY, INPUT_DATETIME, AUTH_BY, AUTH_DATETIME,  MOD_NO, [STATUS]) " & _
                        "VALUES(@P_ACNUMBER, @P_CURRENCY_CODE, @P_CLIENT_NUMBER, @P_ACCOUNT_TYPE, @P_OPENED, 'A', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE(),  1, 'L')"

                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                        db.AddInParameter(commProc, "@P_CURRENCY_CODE", DbType.String, dt2.Rows(i)("CCY"))
                        db.AddInParameter(commProc, "@P_CLIENT_NUMBER", DbType.String, dt2.Rows(i)("CUSTOMER_ID"))
                        db.AddInParameter(commProc, "@P_ACCOUNT_TYPE", DbType.String, TypeHelper.AccTypeFlexToGo(dt2.Rows(i)("BBK_DEFINITION")))
                        db.AddInParameter(commProc, "@P_OPENED", DbType.DateTime, dt2.Rows(i)("ACCOUNT_OPENING_DATE"))

                        db.ExecuteNonQuery(commProc, trans)

                    End If

                    ''----end go

                    _ToTRecordNo = _ToTRecordNo + 1

                    SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())


                Next i

                strSql = "delete  IMP_FLEX_CUST_M WHERE IMPORTED_DATE=@P_IMPORTED_DATE"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                db.ExecuteNonQuery(commProc, trans)


                '-----------------------


                trans.Commit()

                SetFileImpStatus(Environment.NewLine + "Success")

                log_message = " Authorized : Flex Customer File Date : " + dtTrans.ToString()
                Logger.system_log(log_message)

                Return TransState.Update

            End Using

            oRead.Close()

        Catch ex As Exception
            SetFileImpStatus(Environment.NewLine + ex.Message)

        End Try

        Return tStatus


    End Function


    Private Function AuthDataFileOld2(ByVal strFilename As String) As TransState

        Dim tStatus As TransState
        tStatus = TransState.UnspecifiedError

        Dim chrSep As Char() = {"~"}
        Dim strLine As String = ""
        Dim strArLine As String()
        Dim dtTrans As New DateTime
        Dim strSql As String = ""
        Dim dt As New DataTable

        Dim dt2 As New DataTable

        Dim flagHeaderFound As Boolean = False

        'Dim oFile As System.IO.File

        If strFilename.Trim = "" Then
            Exit Function
        End If

        Dim db2 As New SqlDatabase(CommonAppSet.ConnStr)

        Dim oRead As System.IO.StreamReader

        Try
            oRead = File.OpenText(strFilename)

            SetFileImpStatus(Environment.NewLine + "----------------")
            SetFileImpStatus(Environment.NewLine + "Filename:" + strFilename)

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                'Dim trans As DbTransaction = conn.BeginTransaction()

                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()

                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        If strArLine(0).Trim() = "HH" Then

                            If strArLine(4).Trim() = "" Then
                                'MessageBox.Show("Date missing in Header." & Environment.NewLine & "Use valid file", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                SetFileImpStatus(Environment.NewLine + "Date missing in Header. Invalid file")
                                Exit Function

                            End If

                            dtTrans = DateTime.ParseExact(strArLine(4), "yyyyMMdd", New CultureInfo("en-US"))

                            'MessageBox.Show("Transaction of " & dtTrans.ToShortDateString)
                            SetFileImpStatus(Environment.NewLine + "Transaction Date:" + dtTrans.ToShortDateString)
                            '--------

                            Dim ds As New DataSet

                            strSql = "Select distinct USER_NAME From IMP_FLEX_CUST_M WHERE IMPORTED_DATE=@P_IMPORTED_DATE"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()


                            db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                            ds = db2.ExecuteDataSet(commProc)

                            If ds.Tables(0).Rows.Count > 0 Then

                                If ds.Tables(0).Rows(0)("USER_NAME").ToString().ToUpper() = CommonAppSet.User.Trim().ToUpper() Then
                                    Return TransState.MakerCheckerSame
                                    'Exit Function

                                End If

                                flagHeaderFound = True

                                Exit Do

                                '------------------------------------
                            Else
                                Return TransState.NoRecord
                            End If

                        End If
                    End If

                Loop

                '----------------------
                '-------------------------

                'Dim commProc As DbCommand

                strSql = "select * From IMP_FLEX_CUST_M WHERE IMPORTED_DATE=@P_IMPORTED_DATE ORDER BY SLNO ASC"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                dt2 = db.ExecuteDataSet(commProc).Tables(0)

                Dim i As Integer = 0

                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()

                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        If strArLine(0).Trim() = "D" Then

                            If i >= dt2.Rows.Count Then
                                Return TransState.UpdateNotPossible
                            End If

                            If dt2.Rows(i)("ACC_BRANCH") <> strArLine(5).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACCOUNT") <> strArLine(6).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("DESCRIPTION") <> strArLine(7).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACCOUNT_OPENING_DATE") <> DateTime.ParseExact(strArLine(8), "yyyyMMdd", New CultureInfo("en-US")) Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("CCY") <> strArLine(9).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("CURRENT_BALANCE_ACC_LCY") <> NullHelper.ToDecNum(strArLine(16).Trim()) Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("CUSTOMER_ID") <> strArLine(22).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACCOUNT_CLASS") <> strArLine(32).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("BBK_DEFINITION") <> strArLine(61).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If


                            i = i + 1

                            '_ToTRecordNo = _ToTRecordNo + 1

                            'SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())

                        End If
                    End If
                    'MessageBox.Show(strLine)
                Loop

                If i <> dt2.Rows.Count Then
                    Return TransState.UpdateNotPossible
                End If

                '---- checking complete
                '---- now have to update tables

                Dim trans As DbTransaction = conn.BeginTransaction()

                '-------
                '-------

                strSql = "select STATUS from STATUS_IMP_FLEX_CUST where IMPORTED_DATE=@P_IMPORTED_DATE"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                dt = db.ExecuteDataSet(commProc, trans).Tables(0)

                If dt.Rows.Count > 0 Then

                    strSql = "delete CUST_BALANCE where IMPORTED_DATE =@P_IMPORTED_DATE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)

                    strSql = "UPDATE STATUS_IMP_FLEX_CUST SET STATUS='N' where IMPORTED_DATE=@P_IMPORTED_DATE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)





                Else '-------- insert new date into STATUS_IMP_FLEX_TRANS table



                    strSql = "insert into STATUS_IMP_FLEX_CUST(IMPORTED_DATE,STATUS) values(@P_IMPORTED_DATE,'N')"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)



                End If



                '-------
                '---------

                'strSql = "delete CUST_BALANCE where IMPORTED_DATE =@P_IMPORTED_DATE"

                'commProc = db.GetSqlStringCommand(strSql)

                'commProc.Parameters.Clear()
                'db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                'db.ExecuteNonQuery(commProc, trans)


                strSql = "delete FIU_ACCOUNT_INFO where IS_AUTHORIZED=0"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.ExecuteNonQuery(commProc, trans)

                strSql = "delete GO_ACCOUNT_INFO_HIST where IS_AUTH=0"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.ExecuteNonQuery(commProc, trans)

                Dim dtBankBranch As New DataTable

                For i = 0 To dt2.Rows.Count - 1



                    strSql = "INSERT INTO  CUST_BALANCE(IMPORTED_DATE, ACCOUNT, CURRENT_BALANCE_ACC_LCY, " & _
                        "ACCOUNT_CLASS, BBK_DEFINITION) " & _
                        "VALUES(@P_IMPORTED_DATE, @P_ACCOUNT, @P_CURRENT_BALANCE_ACC_LCY, " & _
                        "@P_ACCOUNT_CLASS, @P_BBK_DEFINITION)"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)
                    db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dt2.Rows(i)("ACCOUNT"))
                    db.AddInParameter(commProc, "@P_CURRENT_BALANCE_ACC_LCY", DbType.Decimal, dt2.Rows(i)("CURRENT_BALANCE_ACC_LCY"))
                    db.AddInParameter(commProc, "@P_ACCOUNT_CLASS", DbType.String, dt2.Rows(i)("ACCOUNT_CLASS"))
                    db.AddInParameter(commProc, "@P_BBK_DEFINITION", DbType.String, dt2.Rows(i)("BBK_DEFINITION"))

                    db.ExecuteNonQuery(commProc, trans)


                    strSql = "SELECT * FROM CitiBank_Branch WHERE CITIBRANCH_CODE=@P_CITIBRANCH_CODE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_CITIBRANCH_CODE", DbType.String, dt2.Rows(i)("ACCOUNT").ToString().Trim().Substring(0, 3))

                    dtBankBranch = db.ExecuteDataSet(commProc, trans).Tables(0)

                    If dtBankBranch.Rows.Count > 0 Then

                    Else
                        _ToTRecordNo = _ToTRecordNo + 1
                        SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())

                        Continue For
                    End If

                    strSql = "select ACNUMBER,INPUT_BY,MODNO,STATUS from FIU_ACCOUNT_INFO where ACNUMBER=@P_ACNUMBER AND [STATUS] in ('L','D')"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))

                    dt = db.ExecuteDataSet(commProc, trans).Tables(0)

                    If dt.Rows.Count > 0 Then

                        If dt.Rows(0)("STATUS").ToString() = "L" Then

                            strSql = "INSERT INTO FIU_ACCOUNT_INFO(BANK_CODE, BRANCH_CODE, ACNUMBER, AC_TITLE, ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2, OLD_ACNUMBER, OLD_CODE_UPDATED_ON, OLD_CODE_UPDATED_BY, INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED, AUTH_BY, AUTH_DATETIME, STATUS) " & _
                                        "SELECT BANK_CODE, BRANCH_CODE, ACNUMBER, @P_AC_TITLE, ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2, OLD_ACNUMBER, OLD_CODE_UPDATED_ON, OLD_CODE_UPDATED_BY, INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON, MODNO + 1, @P_INPUT_BY, GETDATE(), IS_AUTHORIZED, @P_AUTH_BY, GETDATE(), STATUS " & _
                                        "FROM FIU_ACCOUNT_INFO " & _
                                        "where ACNUMBER=@P_ACNUMBER AND [STATUS]='L' AND MODNO=@P_MODNO"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()
                            db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                            db.AddInParameter(commProc, "@P_AC_TITLE", DbType.String, dt2.Rows(i)("DESCRIPTION"))
                            db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, dt2.Rows(i)("USER_NAME"))
                            db.AddInParameter(commProc, "@P_AUTH_BY", DbType.String, CommonAppSet.User.Trim().ToUpper())
                            db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, dt.Rows(0)("MODNO"))

                            db.ExecuteNonQuery(commProc, trans)

                            strSql = "UPDATE FIU_ACCOUNT_INFO " & _
                               "SET [STATUS]='O' " & _
                               "where ACNUMBER=@P_ACNUMBER AND [STATUS]='L' AND MODNO=@P_MODNO"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()
                            db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                            db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, dt.Rows(0)("MODNO"))

                            db.ExecuteNonQuery(commProc, trans)



                        End If

                    Else

                        '---------------------
                        'strSql = "select ACNUMBER from FIU_ACCOUNT_INFO where ACNUMBER=@P_ACNUMBER AND [STATUS]='D'"

                        'commProc = db.GetSqlStringCommand(strSql)

                        'commProc.Parameters.Clear()
                        'db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))

                        'If db.ExecuteDataSet(commProc, trans).Tables(0).Rows.Count = 0 Then

                        strSql = "INSERT INTO  FIU_ACCOUNT_INFO(BANK_CODE, BRANCH_CODE, ACNUMBER, AC_TITLE, MODNO, INPUT_BY, INPUT_DATETIME,IS_AUTHORIZED, AUTH_BY, AUTH_DATETIME, [STATUS]) " & _
                            "VALUES(@P_BANK_CODE, @P_BRANCH_CODE, @P_ACNUMBER, @P_AC_TITLE, 1, @P_INPUT_BY, GETDATE(),1, @P_AUTH_BY, GETDATE(), 'L')"

                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, dtBankBranch.Rows(0)("Bank_Code"))
                        db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dtBankBranch.Rows(0)("Branch_Code"))
                        db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                        db.AddInParameter(commProc, "@P_AC_TITLE", DbType.String, dt2.Rows(i)("DESCRIPTION"))
                        db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, dt2.Rows(i)("USER_NAME"))
                        db.AddInParameter(commProc, "@P_AUTH_BY", DbType.String, CommonAppSet.User.Trim().ToUpper())

                        db.ExecuteNonQuery(commProc, trans)

                        'End If


                        '---------------------



                    End If

                    ''----go

                    strSql = "select ACNUMBER,INPUT_BY,MOD_NO,STATUS from GO_ACCOUNT_INFO where ACNUMBER=@P_ACNUMBER AND [STATUS] IN ('L','D')"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))

                    dt = db.ExecuteDataSet(commProc, trans).Tables(0)

                    If dt.Rows.Count > 0 Then

                        If dt.Rows(0)("STATUS").ToString() = "L" Then

                            strSql = "INSERT INTO GO_ACCOUNT_INFO_HIST(ACNUMBER, CURRENCY_CODE, IBAN, CLIENT_NUMBER, ACCOUNT_TYPE, OPENED, CLOSED, STATUS_CODE, BENEFICIARY, BENEFICIARY_COMMENTS, COMMENTS, INPUT_BY, INPUT_DATETIME, INPUT_FROM, AUTH_BY, AUTH_DATETIME, AUTH_FROM, MOD_NO, STATUS, IS_AUTH, ENTITY_ID) " & _
                                            "SELECT ACNUMBER, CURRENCY_CODE, IBAN, CLIENT_NUMBER, ACCOUNT_TYPE, OPENED, CLOSED, STATUS_CODE, BENEFICIARY, BENEFICIARY_COMMENTS, COMMENTS, INPUT_BY, INPUT_DATETIME, INPUT_FROM, AUTH_BY, AUTH_DATETIME, AUTH_FROM, MOD_NO, 'O',1, ENTITY_ID " & _
                                            "FROM GO_ACCOUNT_INFO " & _
                                            "where ACNUMBER=@P_ACNUMBER AND [STATUS]='L' AND MOD_NO=@P_MOD_NO"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()
                            db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                            db.AddInParameter(commProc, "@P_MOD_NO", DbType.Int32, dt.Rows(0)("MOD_NO"))

                            db.ExecuteNonQuery(commProc, trans)

                            strSql = "UPDATE GO_ACCOUNT_INFO " & _
                               "SET CURRENCY_CODE = @P_CURRENCY_CODE, CLIENT_NUMBER=@P_CLIENT_NUMBER, ACCOUNT_TYPE=@P_ACCOUNT_TYPE,OPENED=@P_OPENED, INPUT_BY=@P_INPUT_BY,INPUT_DATETIME=GETDATE(),AUTH_BY=@P_AUTH_BY, AUTH_DATETIME=GETDATE(), MOD_NO=MOD_NO+1 " & _
                               "where ACNUMBER=@P_ACNUMBER AND [STATUS]='L' AND MOD_NO=@P_MOD_NO"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()
                            db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                            db.AddInParameter(commProc, "@P_CURRENCY_CODE", DbType.String, dt2.Rows(i)("CCY"))
                            db.AddInParameter(commProc, "@P_CLIENT_NUMBER", DbType.String, dt2.Rows(i)("CUSTOMER_ID"))
                            db.AddInParameter(commProc, "@P_ACCOUNT_TYPE", DbType.String, TypeHelper.AccTypeFlexToGo(dt2.Rows(i)("BBK_DEFINITION")))
                            db.AddInParameter(commProc, "@P_OPENED", DbType.DateTime, dt2.Rows(i)("ACCOUNT_OPENING_DATE"))
                            db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, dt2.Rows(i)("USER_NAME"))
                            db.AddInParameter(commProc, "@P_AUTH_BY", DbType.String, CommonAppSet.User.Trim().ToUpper())
                            db.AddInParameter(commProc, "@P_MOD_NO", DbType.Int32, dt.Rows(0)("MOD_NO"))

                            db.ExecuteNonQuery(commProc, trans)



                        End If

                    Else

                        strSql = "INSERT INTO  GO_ACCOUNT_INFO(ACNUMBER, CURRENCY_CODE, CLIENT_NUMBER, ACCOUNT_TYPE, OPENED, STATUS_CODE, INPUT_BY, INPUT_DATETIME, AUTH_BY, AUTH_DATETIME,  MOD_NO, [STATUS]) " & _
                        "VALUES(@P_ACNUMBER, @P_CURRENCY_CODE, @P_CLIENT_NUMBER, @P_ACCOUNT_TYPE, @P_OPENED, 'A', @P_INPUT_BY, GETDATE(), @P_AUTH_BY, GETDATE(),  1, 'L')"

                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                        db.AddInParameter(commProc, "@P_CURRENCY_CODE", DbType.String, dt2.Rows(i)("CCY"))
                        db.AddInParameter(commProc, "@P_CLIENT_NUMBER", DbType.String, dt2.Rows(i)("CUSTOMER_ID"))
                        db.AddInParameter(commProc, "@P_ACCOUNT_TYPE", DbType.String, TypeHelper.AccTypeFlexToGo(dt2.Rows(i)("BBK_DEFINITION")))
                        db.AddInParameter(commProc, "@P_OPENED", DbType.DateTime, dt2.Rows(i)("ACCOUNT_OPENING_DATE"))
                        db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, dt2.Rows(i)("USER_NAME"))
                        db.AddInParameter(commProc, "@P_AUTH_BY", DbType.String, CommonAppSet.User.Trim().ToUpper())

                        db.ExecuteNonQuery(commProc, trans)

                    End If

                    ''----end go

                    _ToTRecordNo = _ToTRecordNo + 1

                    SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())


                Next i

                strSql = "delete  IMP_FLEX_CUST_M WHERE IMPORTED_DATE=@P_IMPORTED_DATE"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                db.ExecuteNonQuery(commProc, trans)


                '-----------------------


                trans.Commit()

                SetFileImpStatus(Environment.NewLine + "Success")

                log_message = " Authorized : Flex Customer File Date : " + dtTrans.ToString()
                Logger.system_log(log_message)

                Return TransState.Update

            End Using

            oRead.Close()

        Catch ex As Exception
            SetFileImpStatus(Environment.NewLine + ex.Message)

        End Try

        Return tStatus


    End Function


    Private Function AuthDataFile(ByVal strFilename As String) As TransState

        Dim tStatus As TransState
        tStatus = TransState.UnspecifiedError

        Dim chrSep As Char() = {"~"}
        Dim strLine As String = ""
        Dim strArLine As String()
        Dim dtTrans As New DateTime
        Dim strSql As String = ""
        Dim dt As New DataTable

        Dim dt2 As New DataTable

        Dim flagHeaderFound As Boolean = False

        'Dim oFile As System.IO.File

        If strFilename.Trim = "" Then
            Exit Function
        End If

        Dim db2 As New SqlDatabase(CommonAppSet.ConnStr)

        Dim oRead As System.IO.StreamReader

        Try
            oRead = File.OpenText(strFilename)

            SetFileImpStatus(Environment.NewLine + "----------------")
            SetFileImpStatus(Environment.NewLine + "Filename:" + strFilename)

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                'Dim trans As DbTransaction = conn.BeginTransaction()

                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()

                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        If strArLine(0).Trim() = "HH" Then

                            If strArLine(4).Trim() = "" Then
                                'MessageBox.Show("Date missing in Header." & Environment.NewLine & "Use valid file", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                SetFileImpStatus(Environment.NewLine + "Date missing in Header. Invalid file")
                                Exit Function

                            End If

                            dtTrans = DateTime.ParseExact(strArLine(4), "yyyyMMdd", New CultureInfo("en-US"))

                            'MessageBox.Show("Transaction of " & dtTrans.ToShortDateString)
                            SetFileImpStatus(Environment.NewLine + "Transaction Date:" + dtTrans.ToShortDateString)
                            '--------

                            Dim ds As New DataSet

                            strSql = "Select distinct USER_NAME From IMP_FLEX_CUST_M WHERE IMPORTED_DATE=@P_IMPORTED_DATE"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()


                            db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                            ds = db2.ExecuteDataSet(commProc)

                            If ds.Tables(0).Rows.Count > 0 Then

                                If ds.Tables(0).Rows(0)("USER_NAME").ToString().ToUpper() = CommonAppSet.User.Trim().ToUpper() Then
                                    Return TransState.MakerCheckerSame
                                    'Exit Function

                                End If

                                flagHeaderFound = True

                                Exit Do

                                '------------------------------------
                            Else
                                Return TransState.NoRecord
                            End If

                        End If
                    End If

                Loop

                '----------------------
                '-------------------------

                'Dim commProc As DbCommand

                strSql = "select * From IMP_FLEX_CUST_M WHERE IMPORTED_DATE=@P_IMPORTED_DATE ORDER BY SLNO ASC"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                dt2 = db.ExecuteDataSet(commProc).Tables(0)

                Dim i As Integer = 0

                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()

                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        If strArLine(0).Trim() = "D" Then

                            If i >= dt2.Rows.Count Then
                                Return TransState.UpdateNotPossible
                            End If

                            If dt2.Rows(i)("ACC_BRANCH") <> strArLine(5).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACCOUNT") <> strArLine(6).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("DESCRIPTION") <> strArLine(7).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACCOUNT_OPENING_DATE") <> DateTime.ParseExact(strArLine(8), "yyyyMMdd", New CultureInfo("en-US")) Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("CCY") <> strArLine(9).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("CURRENT_BALANCE_ACC_LCY") <> NullHelper.ToDecNum(strArLine(16).Trim()) Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("CUSTOMER_ID") <> strArLine(22).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACCOUNT_CLASS") <> strArLine(32).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("BBK_DEFINITION") <> strArLine(61).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If


                            i = i + 1

                            '_ToTRecordNo = _ToTRecordNo + 1

                            'SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())

                        End If
                    End If
                    'MessageBox.Show(strLine)
                Loop

                If i <> dt2.Rows.Count Then
                    Return TransState.UpdateNotPossible
                End If

                '---- checking complete
                '---- now have to update tables

                Dim trans As DbTransaction = conn.BeginTransaction()

                '-------
                '-------

                strSql = "select STATUS from STATUS_IMP_FLEX_CUST where IMPORTED_DATE=@P_IMPORTED_DATE"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                dt = db.ExecuteDataSet(commProc, trans).Tables(0)

                If dt.Rows.Count > 0 Then

                    strSql = "delete CUST_BALANCE where IMPORTED_DATE =@P_IMPORTED_DATE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)

                    strSql = "UPDATE STATUS_IMP_FLEX_CUST SET STATUS='N' where IMPORTED_DATE=@P_IMPORTED_DATE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)





                Else '-------- insert new date into STATUS_IMP_FLEX_TRANS table



                    strSql = "insert into STATUS_IMP_FLEX_CUST(IMPORTED_DATE,STATUS) values(@P_IMPORTED_DATE,'N')"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)



                End If



                '-------
                '---------

                'strSql = "delete CUST_BALANCE where IMPORTED_DATE =@P_IMPORTED_DATE"

                'commProc = db.GetSqlStringCommand(strSql)

                'commProc.Parameters.Clear()
                'db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                'db.ExecuteNonQuery(commProc, trans)


                strSql = "delete FIU_ACCOUNT_INFO where IS_AUTHORIZED=0"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.ExecuteNonQuery(commProc, trans)

                strSql = "delete GO_ACCOUNT_INFO_HIST where IS_AUTH=0"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.ExecuteNonQuery(commProc, trans)

                Dim dtBankBranch As New DataTable

                For i = 0 To dt2.Rows.Count - 1



                    strSql = "INSERT INTO  CUST_BALANCE(IMPORTED_DATE, ACCOUNT, CURRENT_BALANCE_ACC_LCY, " & _
                        "ACCOUNT_CLASS, BBK_DEFINITION) " & _
                        "VALUES(@P_IMPORTED_DATE, @P_ACCOUNT, @P_CURRENT_BALANCE_ACC_LCY, " & _
                        "@P_ACCOUNT_CLASS, @P_BBK_DEFINITION)"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)
                    db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dt2.Rows(i)("ACCOUNT"))
                    db.AddInParameter(commProc, "@P_CURRENT_BALANCE_ACC_LCY", DbType.Decimal, dt2.Rows(i)("CURRENT_BALANCE_ACC_LCY"))
                    db.AddInParameter(commProc, "@P_ACCOUNT_CLASS", DbType.String, dt2.Rows(i)("ACCOUNT_CLASS"))
                    db.AddInParameter(commProc, "@P_BBK_DEFINITION", DbType.String, dt2.Rows(i)("BBK_DEFINITION"))

                    db.ExecuteNonQuery(commProc, trans)


                    strSql = "SELECT * FROM CitiBank_Branch WHERE CITIBRANCH_CODE=@P_CITIBRANCH_CODE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_CITIBRANCH_CODE", DbType.String, dt2.Rows(i)("ACCOUNT").ToString().Trim().Substring(0, 3))

                    dtBankBranch = db.ExecuteDataSet(commProc, trans).Tables(0)

                    If dtBankBranch.Rows.Count > 0 Then

                    Else
                        _ToTRecordNo = _ToTRecordNo + 1
                        SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())

                        Continue For
                    End If

                    strSql = "select ACNUMBER,INPUT_BY,MODNO,STATUS from FIU_ACCOUNT_INFO where ACNUMBER=@P_ACNUMBER AND [STATUS] in ('L','D')"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))

                    dt = db.ExecuteDataSet(commProc, trans).Tables(0)

                    If dt.Rows.Count > 0 Then

                        If dt.Rows(0)("STATUS").ToString() = "L" Then

                            'strSql = "INSERT INTO FIU_ACCOUNT_INFO(BANK_CODE, BRANCH_CODE, ACNUMBER, AC_TITLE, ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2, OLD_ACNUMBER, OLD_CODE_UPDATED_ON, OLD_CODE_UPDATED_BY, INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED, AUTH_BY, AUTH_DATETIME, STATUS) " & _
                            '            "SELECT BANK_CODE, BRANCH_CODE, ACNUMBER, @P_AC_TITLE, ACTYPECODE, OWTYPECODE, DECLARED_DEPOSIT_AMOUNT, DECLARED_DEPOSIT_TRANSNO, DECLARED_DEPOSIT_MAXAMOUNT, DECLARED_WITHDR_AMOUNT, DECLARED_WITHDR_TRANSNO, DECLARED_WITHDR_MAXAMOUNT, TIN, BIN, VAT_REG_NO, VAT_REG_DATE, COMPANY_REG_NO, COMPANY_REG_DATE, REG_AUTHORITY_CODE, PRES_ADDR, PERM_ADDR, PHONE_RES1, PHONE_RES2, PHONE_OFFICE1, PHONE_OFFICE2, MOBILE1, MOBILE2, OLD_ACNUMBER, OLD_CODE_UPDATED_ON, OLD_CODE_UPDATED_BY, INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON, MODNO + 1, @P_INPUT_BY, GETDATE(), IS_AUTHORIZED, @P_AUTH_BY, GETDATE(), STATUS " & _
                            '            "FROM FIU_ACCOUNT_INFO " & _
                            '            "where ACNUMBER=@P_ACNUMBER AND [STATUS]='L' AND MODNO=@P_MODNO"

                            'strSql = "UPDATE FIU_ACCOUNT_INFO " & _
                            '   "SET AC_TITLE=@P_AC_TITLE, INPUT_BY=@P_INPUT_BY, INPUT_DATETIME=GETDATE(), AUTH_BY=@P_AUTH_BY, AUTH_DATETIME=GETDATE() " & _
                            '   "where ACNUMBER=@P_ACNUMBER AND [STATUS]='L' AND MODNO=@P_MODNO"

                            strSql = "UPDATE FIU_ACCOUNT_INFO " & _
                               "SET AC_TITLE=@P_AC_TITLE, INPUT_BY=@P_INPUT_BY, INPUT_DATETIME=GETDATE(), AUTH_BY=@P_AUTH_BY, AUTH_DATETIME=GETDATE() " & _
                               "where ACNUMBER=@P_ACNUMBER AND MODNO=@P_MODNO"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()
                            db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                            db.AddInParameter(commProc, "@P_AC_TITLE", DbType.String, dt2.Rows(i)("DESCRIPTION"))
                            db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, dt2.Rows(i)("USER_NAME"))
                            db.AddInParameter(commProc, "@P_AUTH_BY", DbType.String, CommonAppSet.User.Trim().ToUpper())
                            db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, dt.Rows(0)("MODNO"))

                            db.ExecuteNonQuery(commProc, trans)

                            'strSql = "UPDATE FIU_ACCOUNT_INFO " & _
                            '   "SET [STATUS]='O' " & _
                            '   "where ACNUMBER=@P_ACNUMBER AND [STATUS]='L' AND MODNO=@P_MODNO"

                            'commProc = db.GetSqlStringCommand(strSql)

                            'commProc.Parameters.Clear()
                            'db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                            'db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, dt.Rows(0)("MODNO"))

                            'db.ExecuteNonQuery(commProc, trans)



                        End If

                    Else

                        '---------------------
                        'strSql = "select ACNUMBER from FIU_ACCOUNT_INFO where ACNUMBER=@P_ACNUMBER AND [STATUS]='D'"

                        'commProc = db.GetSqlStringCommand(strSql)

                        'commProc.Parameters.Clear()
                        'db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))

                        'If db.ExecuteDataSet(commProc, trans).Tables(0).Rows.Count = 0 Then

                        strSql = "INSERT INTO  FIU_ACCOUNT_INFO(BANK_CODE, BRANCH_CODE, ACNUMBER, AC_TITLE, INSERTED_ON, MODNO, INPUT_BY, INPUT_DATETIME,IS_AUTHORIZED, AUTH_BY, AUTH_DATETIME, [STATUS]) " & _
                            "VALUES(@P_BANK_CODE, @P_BRANCH_CODE, @P_ACNUMBER, @P_AC_TITLE, @INSERTED_ON , 1, @P_INPUT_BY, GETDATE(),1, @P_AUTH_BY, GETDATE(), 'L')"

                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, dtBankBranch.Rows(0)("Bank_Code"))
                        db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dtBankBranch.Rows(0)("Branch_Code"))
                        db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                        db.AddInParameter(commProc, "@P_AC_TITLE", DbType.String, dt2.Rows(i)("DESCRIPTION"))
                        db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, dt2.Rows(i)("USER_NAME"))
                        db.AddInParameter(commProc, "@P_AUTH_BY", DbType.String, CommonAppSet.User.Trim().ToUpper())

                        db.AddInParameter(commProc, "@INSERTED_ON", DbType.DateTime, dt2.Rows(i)("ACCOUNT_OPENING_DATE"))

                        db.ExecuteNonQuery(commProc, trans)

                        'End If


                        '---------------------



                    End If

                    ''----go

                    strSql = "select ACNUMBER,INPUT_BY,MOD_NO,STATUS from GO_ACCOUNT_INFO where ACNUMBER=@P_ACNUMBER AND [STATUS] IN ('L','D')"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))

                    dt = db.ExecuteDataSet(commProc, trans).Tables(0)

                    If dt.Rows.Count > 0 Then

                        If dt.Rows(0)("STATUS").ToString() = "L" Then

                            'strSql = "INSERT INTO GO_ACCOUNT_INFO_HIST(ACNUMBER, CURRENCY_CODE, IBAN, CLIENT_NUMBER, ACCOUNT_TYPE, OPENED, CLOSED, STATUS_CODE, BENEFICIARY, BENEFICIARY_COMMENTS, COMMENTS, INPUT_BY, INPUT_DATETIME, INPUT_FROM, AUTH_BY, AUTH_DATETIME, AUTH_FROM, MOD_NO, STATUS, IS_AUTH, ENTITY_ID) " & _
                            '                "SELECT ACNUMBER, CURRENCY_CODE, IBAN, CLIENT_NUMBER, ACCOUNT_TYPE, OPENED, CLOSED, STATUS_CODE, BENEFICIARY, BENEFICIARY_COMMENTS, COMMENTS, INPUT_BY, INPUT_DATETIME, INPUT_FROM, AUTH_BY, AUTH_DATETIME, AUTH_FROM, MOD_NO, 'O',1, ENTITY_ID " & _
                            '                "FROM GO_ACCOUNT_INFO " & _
                            '                "where ACNUMBER=@P_ACNUMBER AND [STATUS]='L' AND MOD_NO=@P_MOD_NO"

                            'commProc = db.GetSqlStringCommand(strSql)

                            'commProc.Parameters.Clear()
                            'db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                            'db.AddInParameter(commProc, "@P_MOD_NO", DbType.Int32, dt.Rows(0)("MOD_NO"))

                            'db.ExecuteNonQuery(commProc, trans)

                            'strSql = "UPDATE GO_ACCOUNT_INFO " & _
                            '   "SET CURRENCY_CODE = @P_CURRENCY_CODE, CLIENT_NUMBER=@P_CLIENT_NUMBER, ACCOUNT_TYPE=@P_ACCOUNT_TYPE,OPENED=@P_OPENED, INPUT_BY=@P_INPUT_BY,INPUT_DATETIME=GETDATE(),AUTH_BY=@P_AUTH_BY, AUTH_DATETIME=GETDATE() " & _
                            '   "where ACNUMBER=@P_ACNUMBER AND [STATUS]='L' AND MOD_NO=@P_MOD_NO"

                            strSql = "UPDATE GO_ACCOUNT_INFO " & _
                               "SET CURRENCY_CODE = @P_CURRENCY_CODE, CLIENT_NUMBER=@P_CLIENT_NUMBER, ACCOUNT_TYPE=@P_ACCOUNT_TYPE,OPENED=@P_OPENED, INPUT_BY=@P_INPUT_BY,INPUT_DATETIME=GETDATE(),AUTH_BY=@P_AUTH_BY, AUTH_DATETIME=GETDATE() " & _
                               "where ACNUMBER=@P_ACNUMBER  AND MOD_NO=@P_MOD_NO"


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()
                            db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                            db.AddInParameter(commProc, "@P_CURRENCY_CODE", DbType.String, dt2.Rows(i)("CCY"))
                            db.AddInParameter(commProc, "@P_CLIENT_NUMBER", DbType.String, dt2.Rows(i)("CUSTOMER_ID"))
                            db.AddInParameter(commProc, "@P_ACCOUNT_TYPE", DbType.String, TypeHelper.AccTypeFlexToGo(dt2.Rows(i)("BBK_DEFINITION")))
                            db.AddInParameter(commProc, "@P_OPENED", DbType.DateTime, dt2.Rows(i)("ACCOUNT_OPENING_DATE"))
                            db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, dt2.Rows(i)("USER_NAME"))
                            db.AddInParameter(commProc, "@P_AUTH_BY", DbType.String, CommonAppSet.User.Trim().ToUpper())
                            db.AddInParameter(commProc, "@P_MOD_NO", DbType.Int32, dt.Rows(0)("MOD_NO"))

                            db.ExecuteNonQuery(commProc, trans)



                        End If

                    Else

                        strSql = "INSERT INTO  GO_ACCOUNT_INFO(ACNUMBER, CURRENCY_CODE, CLIENT_NUMBER, ACCOUNT_TYPE, OPENED, STATUS_CODE, INPUT_BY, INPUT_DATETIME, AUTH_BY, AUTH_DATETIME,  MOD_NO, [STATUS]) " & _
                        "VALUES(@P_ACNUMBER, @P_CURRENCY_CODE, @P_CLIENT_NUMBER, @P_ACCOUNT_TYPE, @P_OPENED, 'A', @P_INPUT_BY, GETDATE(), @P_AUTH_BY, GETDATE(),  1, 'L')"

                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dt2.Rows(i)("ACCOUNT"))
                        db.AddInParameter(commProc, "@P_CURRENCY_CODE", DbType.String, dt2.Rows(i)("CCY"))
                        db.AddInParameter(commProc, "@P_CLIENT_NUMBER", DbType.String, dt2.Rows(i)("CUSTOMER_ID"))
                        db.AddInParameter(commProc, "@P_ACCOUNT_TYPE", DbType.String, TypeHelper.AccTypeFlexToGo(dt2.Rows(i)("BBK_DEFINITION")))
                        db.AddInParameter(commProc, "@P_OPENED", DbType.DateTime, dt2.Rows(i)("ACCOUNT_OPENING_DATE"))
                        db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, dt2.Rows(i)("USER_NAME"))
                        db.AddInParameter(commProc, "@P_AUTH_BY", DbType.String, CommonAppSet.User.Trim().ToUpper())

                        db.ExecuteNonQuery(commProc, trans)

                    End If

                    ''----end go

                    _ToTRecordNo = _ToTRecordNo + 1

                    SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())


                Next i

                strSql = "delete  IMP_FLEX_CUST_M WHERE IMPORTED_DATE=@P_IMPORTED_DATE"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                db.ExecuteNonQuery(commProc, trans)


                '-----------------------


                trans.Commit()

                SetFileImpStatus(Environment.NewLine + "Success")

                log_message = " Authorized : Flex Customer File Date : " + dtTrans.ToString()
                Logger.system_log(log_message)

                Return TransState.Update

            End Using

            oRead.Close()

        Catch ex As Exception
            SetFileImpStatus(Environment.NewLine + ex.Message)

        End Try

        Return tStatus


    End Function



    Private Sub ImportCheckerFiles()


        lblToolStatus.Text = ""

        Dim intTotFile As Integer = 0
        Dim intFailed As Integer = 0

        Try



            _ToTRecordNo = 0
            intFailed = 0

            Dim fileNames = My.Computer.FileSystem.GetFiles(txtFolderPath.Text, _
                                                            FileIO.SearchOption.SearchTopLevelOnly, "*.dat")
            intTotFile = fileNames.Count()

            SetLabelText(lblTotFile, intTotFile.ToString())

            Dim procStatus As TransState = TransState.UnspecifiedError

            For Each fileName As String In fileNames
                'filesListBox.Items.Add(fileName)
                procStatus = AuthDataFile(fileName)

                If procStatus = TransState.Update Then

                ElseIf procStatus = TransState.NoRecord Then
                    SetFileImpStatus(Environment.NewLine + "No Record !! ")
                    SetFileImpStatus(Environment.NewLine + "Failed: " + fileName)
                    intFailed = intFailed + 1
                    SetLabelText(lblFaileFileNo, intFailed.ToString())

                ElseIf procStatus = TransState.MakerCheckerSame Then
                    SetFileImpStatus(Environment.NewLine + "Maker cannot authorize record !! ")
                    SetFileImpStatus(Environment.NewLine + "Failed: " + fileName)
                    intFailed = intFailed + 1
                    SetLabelText(lblFaileFileNo, intFailed.ToString())

                ElseIf procStatus = TransState.UpdateNotPossible Then
                    SetFileImpStatus(Environment.NewLine + "Record Mismatch !! ")
                    SetFileImpStatus(Environment.NewLine + "Failed: " + fileName)
                    intFailed = intFailed + 1
                    SetLabelText(lblFaileFileNo, intFailed.ToString())

                Else
                    'SetFileImpStatus(Environment.NewLine + "Unspecified Error !!")
                    SetFileImpStatus(Environment.NewLine + "Failed: " + fileName)
                    intFailed = intFailed + 1
                    SetLabelText(lblFaileFileNo, intFailed.ToString())
                End If
            Next
            '-------

            _ProcessSuccess = True


        Catch ex As Exception


            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)


        End Try

        'lblFaileFileNo.Text = intFailed.ToString()
        'lblTotRecNo.Text = _ToTRecordNo.ToString()
        'lblTotFile.Text = intTotFile.ToString()

        SetLabelText(lblFaileFileNo, intFailed.ToString())
        SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())
        SetLabelText(lblTotFile, intTotFile.ToString())


    End Sub



    Private Sub btnSetFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFolder.Click
        fbdTransFile.ShowNewFolderButton = False
        fbdTransFile.ShowDialog()
        txtFolderPath.Text = fbdTransFile.SelectedPath
    End Sub

    Private Sub btnImportMaker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportMaker.Click

        If Not Directory.Exists(fbdTransFile.SelectedPath) Or txtFolderPath.Text.Trim() = "" Then
            MessageBox.Show("Select Path", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnSetFolder.Focus()
            Exit Sub
        End If

        txtFileImpStatus.Clear()


        txtFolderPath.ReadOnly = True
        btnSetFolder.Enabled = False
        btnImportMaker.Enabled = False
        btnImportChecker.Enabled = False

        ProgressBar1.Style = ProgressBarStyle.Marquee

        BackgroundWorker1.RunWorkerAsync()

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        _ProcessSuccess = False

        ImportMakerFiles()


    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        ProgressBar1.Style = ProgressBarStyle.Continuous


        txtFolderPath.ReadOnly = False
        btnSetFolder.Enabled = True
        btnImportMaker.Enabled = True
        btnImportChecker.Enabled = True


        If _ProcessSuccess = True Then
            MessageBox.Show("Completed!!", "File Upload Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If




    End Sub

    Private Sub btnImportChecker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportChecker.Click

        If Not Directory.Exists(fbdTransFile.SelectedPath) Or txtFolderPath.Text.Trim() = "" Then
            MessageBox.Show("Select Path", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnSetFolder.Focus()
            Exit Sub
        End If

        txtFileImpStatus.Clear()


        txtFolderPath.ReadOnly = True
        btnSetFolder.Enabled = False
        btnImportMaker.Enabled = False
        btnImportChecker.Enabled = False



        ProgressBar1.Style = ProgressBarStyle.Marquee


        BackgroundWorker2.RunWorkerAsync()


    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork


        _ProcessSuccess = False

        ImportCheckerFiles()



    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted

        ProgressBar1.Style = ProgressBarStyle.Continuous



        txtFolderPath.ReadOnly = False
        btnSetFolder.Enabled = True
        btnImportMaker.Enabled = True
        btnImportChecker.Enabled = True


        If _ProcessSuccess = True Then
            MessageBox.Show("Completed!!", "File Upload Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub
End Class