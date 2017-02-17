Imports System.IO
Imports System.Globalization
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common
Imports CTR.Common
'Imports System.Windows.Forms




Public Class FrmImportFlexTrans

    Dim _formName As String = "ToolsImportFlexTransaction"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""
    Dim strSql As String = ""

    Dim errLevel As String = "0"


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
        SetLabelText(lblTotFile, intTotFile.ToString())


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

                            strSql = "delete IMP_FLEX_TRANS_M where TXN_DATE=@P_TXN_DATE"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                            db.ExecuteNonQuery(commProc, trans)



                        End If

                        If strArLine(0).Trim() = "D" Then

                            SLNO = SLNO + 1

                            strSql = "insert into IMP_FLEX_TRANS_M(AC_BRANCH, BATCH_NO,ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, " & _
                                "  TRN_CODE, REFERENCE, DRCR_IND, MODULE, USER_ID, AUTH_ID, " & _
                                " RELATED_CUSTOMER, HOST_REFERENCE, TRANSACTION_DESCRIPTION, " & _
                                " APPLICATION_ID,TXN_BRANCH, TXN_DATE,USER_NAME,SLNO,AC_ENTRY_SR_NO,FCY_AMOUNT,VALUE_DATE) values(@P_AC_BRANCH, @P_BATCH_NO, @P_ACCOUNT, @P_ACCOUNT_CURRRENCY, @P_LCY_AMOUNT,  @P_TRN_CODE, @P_REFERENCE, @P_DRCR_IND, @P_MODULE, @P_USER_ID, @P_AUTH_ID, @P_RELATED_CUSTOMER,  @P_HOST_REFERENCE, @P_TRANSACTION_DESCRIPTION, @P_APPLICATION_ID,@P_TXN_BRANCH, @P_TXN_DATE, @P_USER_NAME, @P_SLNO,@P_AC_ENTRY_SR_NO,@P_FCY_AMOUNT,@P_VALUE_DATE)"


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()
                            db.AddInParameter(commProc, "@P_AC_BRANCH", DbType.String, strArLine(2).Trim())
                            db.AddInParameter(commProc, "@P_BATCH_NO", DbType.String, strArLine(3).Trim())
                            db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, strArLine(5).Trim())
                            db.AddInParameter(commProc, "@P_ACCOUNT_CURRRENCY", DbType.String, strArLine(6).Trim())
                            db.AddInParameter(commProc, "@P_LCY_AMOUNT", DbType.Decimal, NullHelper.ToDecNum(strArLine(8).Trim()))
                            db.AddInParameter(commProc, "@P_TRN_CODE", DbType.String, strArLine(10).Trim())
                            db.AddInParameter(commProc, "@P_REFERENCE", DbType.String, strArLine(11).Trim())
                            db.AddInParameter(commProc, "@P_DRCR_IND", DbType.String, strArLine(13).Trim())
                            db.AddInParameter(commProc, "@P_MODULE", DbType.String, strArLine(16).Trim())
                            db.AddInParameter(commProc, "@P_USER_ID", DbType.String, strArLine(23).Trim())
                            db.AddInParameter(commProc, "@P_AUTH_ID", DbType.String, strArLine(24).Trim())
                            db.AddInParameter(commProc, "@P_RELATED_CUSTOMER", DbType.String, strArLine(26).Trim())
                            db.AddInParameter(commProc, "@P_HOST_REFERENCE", DbType.String, strArLine(29).Trim())
                            db.AddInParameter(commProc, "@P_TRANSACTION_DESCRIPTION", DbType.String, strArLine(62).Trim())
                            db.AddInParameter(commProc, "@P_APPLICATION_ID", DbType.String, strArLine(63).Trim())
                            db.AddInParameter(commProc, "@P_TXN_BRANCH", DbType.String, strArLine(11).Trim().Substring(0, 3))
                            db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)
                            db.AddInParameter(commProc, "@P_USER_NAME", DbType.String, CommonAppSet.User)
                            db.AddInParameter(commProc, "@P_SLNO", DbType.Int32, SLNO)

                            db.AddInParameter(commProc, "@P_AC_ENTRY_SR_NO", DbType.String, strArLine(19).Trim())
                            db.AddInParameter(commProc, "@P_FCY_AMOUNT", DbType.Decimal, NullHelper.ToDecNum(strArLine(7).Trim()))
                            db.AddInParameter(commProc, "@P_VALUE_DATE", DbType.Date, DateTime.ParseExact(strArLine(9), "yyyyMMdd", New CultureInfo("en-US")))


                            'AC_ENTRY_SR_NO,FCY_AMOUNT


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
                SetFileImpStatus(Environment.NewLine + "Success")
                'log_message = "Import Flex Transaction File Date " + dtTrans.ToString() + " By " + CommonAppSet.User.ToString()
                log_message = " Imported : Flex Transaction File Date : " + dtTrans.ToString() + "." + " " + " [maker]"
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

                            strSql = "Select distinct USER_NAME From IMP_FLEX_TRANS_M WHERE TXN_DATE=@P_TXN_DATE"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()


                            db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

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

                strSql = "select * From IMP_FLEX_TRANS_M WHERE TXN_DATE=@P_TXN_DATE ORDER BY SLNO ASC"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                dt2 = db.ExecuteDataSet(commProc).Tables(0)

                Dim i As Integer = 0

                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()

                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        If strArLine(0).Trim() = "D" Then

                            If i >= dt2.Rows.Count Then

                                errLevel = "level 1 " + " Checker Record " + i + " Maker Record " + dt2.Rows.Count
                                Return TransState.UpdateNotPossible
                            End If

                            If dt2.Rows(i)("AC_BRANCH") <> strArLine(2).Trim() Then
                                errLevel = "level 2 " + " Checker Branch : " + strArLine(2).Trim() + " Maker Branch " + dt2.Rows(i)("AC_BRANCH").ToString()
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("BATCH_NO") <> strArLine(3).Trim() Then

                                errLevel = "level 3 " + " Checker BATCH_NO : " + strArLine(3).Trim() + " Maker BATCH_NO " + dt2.Rows(i)("BATCH_NO").ToString()

                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACCOUNT") <> strArLine(5).Trim() Then

                                errLevel = "level 4 " + " Checker ACCOUNT : " + strArLine(5).Trim() + " Maker ACCOUNT " + dt2.Rows(i)("ACCOUNT").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACCOUNT_CURRRENCY") <> strArLine(6).Trim() Then

                                errLevel = "level 5 " + " Checker ACCOUNT_CURRRENCY : " + strArLine(6).Trim() + " Maker ACCOUNT_CURRRENCY " + dt2.Rows(i)("ACCOUNT_CURRRENCY").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If

                            If dt2.Rows(i)("LCY_AMOUNT") <> NullHelper.ToDecNum(strArLine(8).Trim()) Then

                                errLevel = "level 6 " + " Checker LCY_AMOUNT : " + NullHelper.ToDecNum(strArLine(8).Trim()) + " Maker LCY_AMOUNT " + dt2.Rows(i)("LCY_AMOUNT").ToString()

                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("TRN_CODE") <> strArLine(10).Trim() Then

                                errLevel = "level 7 " + " Checker TRN_CODE : " + strArLine(10).Trim() + " Maker TRN_CODE " + dt2.Rows(i)("TRN_CODE").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("REFERENCE") <> strArLine(11).Trim() Then

                                errLevel = "level 8 " + " Checker REFERENCE : " + strArLine(11).Trim() + " Maker REFERENCE " + dt2.Rows(i)("REFERENCE").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("DRCR_IND") <> strArLine(13).Trim() Then

                                errLevel = "level 9 " + " Checker DRCR_IND : " + strArLine(13).Trim() + " Maker DRCR_IND " + dt2.Rows(i)("DRCR_IND").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("MODULE") <> strArLine(16).Trim() Then

                                errLevel = "level 10 " + " Checker MODULE : " + strArLine(16).Trim() + " Maker MODULE " + dt2.Rows(i)("MODULE").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("USER_ID") <> strArLine(23).Trim() Then
                                errLevel = "level 11 " + " Checker USER_ID : " + strArLine(23).Trim() + " Maker USER_ID " + dt2.Rows(i)("USER_ID").ToString()

                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("AUTH_ID") <> strArLine(24).Trim() Then

                                errLevel = "level 12 " + " Checker AUTH_ID : " + strArLine(24).Trim() + " Maker AUTH_ID " + dt2.Rows(i)("AUTH_ID").ToString()

                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("RELATED_CUSTOMER") <> strArLine(26).Trim() Then

                                errLevel = "level 13 " + " Checker RELATED_CUSTOMER : " + strArLine(26).Trim() + " Maker RELATED_CUSTOMER " + dt2.Rows(i)("RELATED_CUSTOMER").ToString()

                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("HOST_REFERENCE") <> strArLine(29).Trim() Then
                                errLevel = "level 14 " + " Checker HOST_REFERENCE : " + strArLine(29).Trim() + " Maker HOST_REFERENCE " + dt2.Rows(i)("HOST_REFERENCE").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("TRANSACTION_DESCRIPTION") <> strArLine(62).Trim() Then


                                errLevel = "level 15 " + " Checker TRANSACTION_DESCRIPTION : " + strArLine(62).Trim() + " Maker TRANSACTION_DESCRIPTION " + dt2.Rows(i)("TRANSACTION_DESCRIPTION").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("APPLICATION_ID") <> strArLine(63).Trim() Then

                                errLevel = "level 16 " + " Checker APPLICATION_ID : " + strArLine(63).Trim() + " Maker APPLICATION_ID " + dt2.Rows(i)("APPLICATION_ID").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("TXN_BRANCH") <> strArLine(11).Trim().Substring(0, 3) Then

                                errLevel = "level 17 " + " Checker TXN_BRANCH : " + strArLine(11).Trim() + " Maker TXN_BRANCH " + dt2.Rows(i)("TXN_BRANCH").ToString()

                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("TXN_DATE") <> dtTrans Then

                                errLevel = "level 18 " + " Checker TXN_DATE : " + dtTrans + " Maker TXN_DATE " + dt2.Rows(i)("TXN_DATE").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If

                            If dt2.Rows(i)("AC_ENTRY_SR_NO") <> strArLine(19).Trim() Then

                                errLevel = "level 19 " + " Checker AC_ENTRY_SR_NO : " + strArLine(19).Trim() + " Maker AC_ENTRY_SR_NO " + dt2.Rows(i)("AC_ENTRY_SR_NO").ToString()

                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If

                            If dt2.Rows(i)("FCY_AMOUNT") <> NullHelper.ToDecNum(strArLine(7).Trim()) Then

                                errLevel = "level 20 " + " Checker FCY_AMOUNT : " + NullHelper.ToDecNum(strArLine(7).Trim()) + " Maker FCY_AMOUNT " + dt2.Rows(i)("FCY_AMOUNT").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If

                            If dt2.Rows(i)("VALUE_DATE") <> DateTime.ParseExact(strArLine(9), "yyyyMMdd", New CultureInfo("en-US")) Then

                                errLevel = "level 21 " + " Checker VALUE_DATE : " + DateTime.ParseExact(strArLine(9), "yyyyMMdd", New CultureInfo("en-US")) + " Maker VALUE_DATE " + dt2.Rows(i)("VALUE_DATE").ToString()


                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If

                            i = i + 1

                            _ToTRecordNo = _ToTRecordNo + 1

                            SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())



                        End If
                    End If
                    'MessageBox.Show(strLine)
                Loop

                If i <> dt2.Rows.Count Then

                    errLevel = "level 22 " + " Checker record : " + i + " Maker record " + dt2.Rows.Count


                    Return TransState.UpdateNotPossible
                End If


                Dim trans As DbTransaction = conn.BeginTransaction()

                strSql = "select STATUS from STATUS_IMP_FLEX_TRANS where IMPORTED_DATE=@P_IMPORTED_DATE"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                dt = db.ExecuteDataSet(commProc, trans).Tables(0)

                If dt.Rows.Count > 0 Then

                    Dim ds As New DataSet

                    strSql = "Select distinct USER_NAME From IMP_FLEX_TRANS_M WHERE TXN_DATE=@P_TXN_DATE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()


                    db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                    ds = db2.ExecuteDataSet(commProc, trans)



                    Dim Maker As String = ds.Tables(0).Rows(0)("USER_NAME").ToString()


                    strSql = "delete  IMP_FLEX_TRANS where TXN_DATE=@P_TXN_DATE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)

                    strSql = "UPDATE STATUS_IMP_FLEX_TRANS SET STATUS='N',GOSTATUS='N', INPUT_BY='" + Maker.ToString() + "', AUTH_BY ='" + CommonAppSet.User.ToString() + "'  where IMPORTED_DATE=@P_IMPORTED_DATE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)





                Else '-------- insert new date into STATUS_IMP_FLEX_TRANS table

                    Dim ds As New DataSet

                    strSql = "Select distinct USER_NAME From IMP_FLEX_TRANS_M WHERE TXN_DATE=@P_TXN_DATE"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()


                    db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                    ds = db2.ExecuteDataSet(commProc, trans)


                    Dim Maker As String = ds.Tables(0).Rows(0)("USER_NAME").ToString()


                    strSql = "insert into STATUS_IMP_FLEX_TRANS(IMPORTED_DATE,STATUS,GOSTATUS, INPUT_BY, AUTH_BY) values(@P_IMPORTED_DATE,'N','N', '" + Maker.ToString() + "','" + CommonAppSet.User.ToString() + "' )"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()
                    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                    db.ExecuteNonQuery(commProc, trans)



                End If

                strSql = "insert into IMP_FLEX_TRANS(AC_BRANCH, BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, " & _
                    "LCY_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, USER_ID, AUTH_ID, RELATED_CUSTOMER, " & _
                    "HOST_REFERENCE, TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE,AC_ENTRY_SR_NO,FCY_AMOUNT,VALUE_DATE) " & _
                    "select AC_BRANCH, BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, " & _
                    "LCY_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, USER_ID, AUTH_ID, RELATED_CUSTOMER, " & _
                    "HOST_REFERENCE, TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE,AC_ENTRY_SR_NO,FCY_AMOUNT,VALUE_DATE " & _
                    " from IMP_FLEX_TRANS_M where TXN_DATE=@P_TXN_DATE"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                db.ExecuteNonQuery(commProc, trans)

                '''''''''''''''

                strSql = "delete  IMP_FLEX_TRANS_M where TXN_DATE=@P_TXN_DATE"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                db.ExecuteNonQuery(commProc, trans)

                trans.Commit()

                SetFileImpStatus(Environment.NewLine + "Success")

                log_message = " Authorized : Flex Transaction File Date : " + dtTrans.ToString()
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

                    SetFileImpStatus(Environment.NewLine + errLevel)

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