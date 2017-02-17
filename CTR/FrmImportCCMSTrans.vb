Imports System.IO
Imports System.Globalization
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common
Imports CTR.Common
'Imports System.Windows.Forms




Public Class FrmImportCCMSTrans

    Dim _formName As String = "ToolsImportCCMSTransaction"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""
    Dim strSql As String = ""


    Dim _ProcessSuccess As Boolean = False

    Dim _ErrorOccur As Boolean = False

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


    Private Sub ImportMakerFiles()


        lblToolStatus.Text = ""

        'Dim intTotFile As Integer = 0
        'Dim intFailed As Integer = 0

        Try



            '_ToTRecordNo = 0
            'intFailed = 0

            'Dim fileNames = My.Computer.FileSystem.GetFiles(txtFileName.Text, _
            '                                                FileIO.SearchOption.SearchTopLevelOnly, "*.dat")
            'intTotFile = fileNames.Count()

            'SetLabelText(lblTotFile, intTotFile.ToString())
           
            If ReadDataFile(txtFileName.Text.Trim()) = False Then
                'txtFileImpStatus.AppendText(Environment.NewLine + "Failed: " + fileName)
                'SetFileImpStatus(Environment.NewLine + "Failed: " + fileName)
                'intFailed = intFailed + 1
                'SetLabelText(lblFaileFileNo, intFailed.ToString())
                _ErrorOccur = True

            End If


            _ProcessSuccess = True


        Catch ex As Exception


            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)


        End Try

        'lblFaileFileNo.Text = intFailed.ToString()
        'lblTotRecNo.Text = _ToTRecordNo.ToString()
        'lblTotFile.Text = intTotFile.ToString()

        'SetLabelText(lblFaileFileNo, intFailed.ToString())
        'SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())
        'SetLabelText(lblTotFile, intTotFile.ToString())


    End Sub



    Private Function ReadDataFile(ByVal strFilename As String) As Boolean

        Dim retVal As Boolean = False

        Dim chrSep As Char() = {","}
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

            'SetFileImpStatus(Environment.NewLine + "----------------")
            'SetFileImpStatus(Environment.NewLine + "Filename:" + strFilename)

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                Dim commProc As DbCommand

                ' Delete IMP_FLEX_TRANS_M data

                strSql = "delete  IMP_CCMS_TRANS_M"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                'db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                db.ExecuteNonQuery(commProc, trans)


                ' ----------------------------- ---

                Dim SLNO As Integer = 0
                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()


                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        'If strArLine(0).Trim() = "HH" Then

                        '    If strArLine(4).Trim() = "" Then
                        '        'MessageBox.Show("Date missing in Header." & Environment.NewLine & "Use valid file", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '        SetFileImpStatus(Environment.NewLine + "Date missing in Header. Invalid file")
                        '        Exit Function
                        '    End If

                        '    dtTrans = DateTime.ParseExact(strArLine(4), "yyyyMMdd", New CultureInfo("en-US"))

                        '    'MessageBox.Show("Transaction of " & dtTrans.ToShortDateString)
                        '    SetFileImpStatus(Environment.NewLine + "Transaction Date:" + dtTrans.ToShortDateString)

                        '    '-----------------------

                        '    strSql = "delete IMP_FLEX_TRANS_M where TXN_DATE=@P_TXN_DATE"

                        '    commProc = db.GetSqlStringCommand(strSql)

                        '    commProc.Parameters.Clear()

                        '    db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                        '    db.ExecuteNonQuery(commProc, trans)



                        'End If

                        If Not strArLine(0).Trim() = "" Then

                            SLNO = SLNO + 1

                            dtTrans = DateTime.ParseExact(strArLine(4), "yyyyMMdd", New CultureInfo("en-US"))

                            strSql = "insert into IMP_CCMS_TRANS_M(CTR_REF, D_CODE, ACC_NO, AMOUNT, TXN_DATE, INPUT_BY, SLNO) " & _
                                " values(@P_CTR_REF, @P_D_CODE, @P_ACC_NO, @P_AMOUNT, @P_TXN_DATE, @P_INPUT_BY, @P_SLNO)"


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()
                            db.AddInParameter(commProc, "@P_CTR_REF", DbType.String, strArLine(0).Trim())
                            db.AddInParameter(commProc, "@P_D_CODE", DbType.String, strArLine(1).Trim())
                            db.AddInParameter(commProc, "@P_ACC_NO", DbType.String, strArLine(2).Trim())
                            db.AddInParameter(commProc, "@P_AMOUNT", DbType.String, strArLine(3).Trim())
                            db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)
                            db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, CommonAppSet.User)
                            db.AddInParameter(commProc, "@P_SLNO", DbType.Int32, SLNO)

                            Dim result As Integer
                            result = db.ExecuteNonQuery(commProc, trans)
                            If result < 0 Then
                                MessageBox.Show("Import Error ", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'SetFileImpStatus(Environment.NewLine + "Import Error")
                                trans.Rollback()
                                Exit Function
                            Else
                            End If

                            '_ToTRecordNo = _ToTRecordNo + 1

                            'SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())

                        End If
                    End If
                    'MessageBox.Show(strLine)
                Loop


                trans.Commit()

                retVal = True
                'SetFileImpStatus(Environment.NewLine + "Success")
                'log_message = "Import Flex Transaction File Date " + dtTrans.ToString() + " By " + CommonAppSet.User.ToString()
                log_message = " Imported : CCMS Transaction [maker]"
                Logger.system_log(log_message)

                'MessageBox.Show("Import Successfull", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End Using




            oRead.Close()

        Catch ex As Exception
            'SetFileImpStatus(Environment.NewLine + ex.Message)
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not oRead Is Nothing Then
                oRead.Close()
            End If
        End Try

        Return retVal

    End Function



    Private Function AuthDataFile(ByVal strFilename As String) As TransState

        Dim tStatus As TransState
        tStatus = TransState.UnspecifiedError

        Dim chrSep As Char() = {","}
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

            'SetFileImpStatus(Environment.NewLine + "----------------")
            'SetFileImpStatus(Environment.NewLine + "Filename:" + strFilename)

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                'Dim trans As DbTransaction = conn.BeginTransaction()

                'Do While oRead.Peek <> -1
                '    strLine = oRead.ReadLine()

                '    If Not strLine.Trim() = "" Then
                '        strArLine = strLine.Split(chrSep)

                '        If Not strArLine(0).Trim() = "" Then

                '            'If strArLine(4).Trim() = "" Then
                '            '    'MessageBox.Show("Date missing in Header." & Environment.NewLine & "Use valid file", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '            '    SetFileImpStatus(Environment.NewLine + "Date missing in Header. Invalid file")
                '            '    Exit Function

                '            'End If

                '            'dtTrans = DateTime.ParseExact(strArLine(4), "yyyyMMdd", New CultureInfo("en-US"))

                '            'MessageBox.Show("Transaction of " & dtTrans.ToShortDateString)
                '            'SetFileImpStatus(Environment.NewLine + "Transaction Date:" + dtTrans.ToShortDateString)
                '            '--------

                '            Dim ds As New DataSet

                '            strSql = "Select distinct INPUT_BY From IMP_CCMS_TRANS_M"

                '            commProc = db.GetSqlStringCommand(strSql)

                '            commProc.Parameters.Clear()

                '            'db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                '            ds = db2.ExecuteDataSet(commProc)

                '            If ds.Tables(0).Rows.Count > 0 Then

                '                If ds.Tables(0).Rows(0)("INPUT_BY").ToString().ToUpper() = CommonAppSet.User.Trim().ToUpper() Then
                '                    Return TransState.MakerCheckerSame
                '                    'Exit Function

                '                End If

                '                flagHeaderFound = True

                '                Exit Do

                '                '------------------------------------
                '            Else
                '                Return TransState.NoRecord
                '            End If

                '        End If
                '    End If

                'Loop

                '----------------------
                '-------------------------

                'Dim commProc As DbCommand

                strSql = "select * From IMP_CCMS_TRANS_M ORDER BY SLNO ASC"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()

                'db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                Dim trans As DbTransaction = conn.BeginTransaction()

                dt2 = db.ExecuteDataSet(commProc, trans).Tables(0)

                Dim i As Integer = 0

                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()

                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        If Not strArLine(0).Trim() = "" Then
                            '---------------------
                            '---------------------
                            If flagHeaderFound = False Then
                                Dim ds As New DataSet

                                strSql = "Select distinct INPUT_BY From IMP_CCMS_TRANS_M"

                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()

                                'db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                                ds = db2.ExecuteDataSet(commProc, trans)

                                If ds.Tables(0).Rows.Count > 0 Then

                                    If ds.Tables(0).Rows(0)("INPUT_BY").ToString().ToUpper() = CommonAppSet.User.Trim().ToUpper() Then
                                        Return TransState.MakerCheckerSame
                                        'Exit Function

                                    End If

                                    flagHeaderFound = True

                                    'Exit Do

                                    '------------------------------------
                                Else
                                    Return TransState.NoRecord
                                End If

                            End If

                            '---------------------
                            '---------------------
                            If i >= dt2.Rows.Count Then
                                Return TransState.UpdateNotPossible
                            End If
                            ', , , , 
                            If dt2.Rows(i)("CTR_REF") <> strArLine(0).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("D_CODE") <> strArLine(1).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("ACC_NO") <> strArLine(2).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If
                            If dt2.Rows(i)("AMOUNT") <> strArLine(3).Trim() Then
                                Return TransState.UpdateNotPossible
                                'Exit Function
                            End If

                            dtTrans = DateTime.ParseExact(strArLine(4), "yyyyMMdd", New CultureInfo("en-US"))

                            If dt2.Rows(i)("TXN_DATE") <> dtTrans Then
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


                'Dim trans As DbTransaction = conn.BeginTransaction()

                'strSql = "select STATUS from STATUS_IMP_FLEX_TRANS where IMPORTED_DATE=@P_IMPORTED_DATE"

                'commProc = db.GetSqlStringCommand(strSql)

                'commProc.Parameters.Clear()
                'db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                'dt = db.ExecuteDataSet(commProc, trans).Tables(0)

                'If dt.Rows.Count > 0 Then

                '    strSql = "delete  IMP_FLEX_TRANS where TXN_DATE=@P_TXN_DATE"

                '    commProc = db.GetSqlStringCommand(strSql)

                '    commProc.Parameters.Clear()
                '    db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                '    db.ExecuteNonQuery(commProc, trans)

                '    strSql = "UPDATE STATUS_IMP_FLEX_TRANS SET STATUS='N',GOSTATUS='N' where IMPORTED_DATE=@P_IMPORTED_DATE"

                '    commProc = db.GetSqlStringCommand(strSql)

                '    commProc.Parameters.Clear()
                '    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                '    db.ExecuteNonQuery(commProc, trans)





                'Else '-------- insert new date into STATUS_IMP_FLEX_TRANS table



                '    strSql = "insert into STATUS_IMP_FLEX_TRANS(IMPORTED_DATE,STATUS,GOSTATUS) values(@P_IMPORTED_DATE,'N','N')"

                '    commProc = db.GetSqlStringCommand(strSql)

                '    commProc.Parameters.Clear()
                '    db.AddInParameter(commProc, "@P_IMPORTED_DATE", DbType.Date, dtTrans)

                '    db.ExecuteNonQuery(commProc, trans)



                'End If

                For i = 0 To dt2.Rows.Count - 1

                    ' Update On 05-01-2015

                    'strSql = "DELETE IMP_CCMS_TRANS " & _
                    '    " WHERE CTR_REF=@P_CTR_REF AND D_CODE=@P_D_CODE AND ACC_NO =@P_ACC_NO " & _
                    '    " AND AMOUNT=@P_AMOUNT AND TXN_DATE =@P_TXN_DATE"


                    strSql = "DELETE IMP_CCMS_TRANS " & _
                        " WHERE CTR_REF=@P_CTR_REF AND D_CODE=@P_D_CODE "


                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()

                    db.AddInParameter(commProc, "@P_CTR_REF", DbType.String, dt2.Rows(i)("CTR_REF"))
                    db.AddInParameter(commProc, "@P_D_CODE", DbType.String, dt2.Rows(i)("D_CODE"))

                    'db.AddInParameter(commProc, "@P_ACC_NO", DbType.String, dt2.Rows(i)("ACC_NO"))
                    'db.AddInParameter(commProc, "@P_AMOUNT", DbType.String, dt2.Rows(i)("AMOUNT"))
                    'db.AddInParameter(commProc, "@P_TXN_DATE", DbType.String, dt2.Rows(i)("TXN_DATE"))


                    db.ExecuteNonQuery(commProc, trans)




                    strSql = "insert into IMP_CCMS_TRANS(CTR_REF, D_CODE, ACC_NO, AMOUNT, TXN_DATE, INPUT_BY, AUTH_BY) " & _
                    " VALUES(@P_CTR_REF, @P_D_CODE, @P_ACC_NO, @P_AMOUNT, @P_TXN_DATE, @P_INPUT_BY, @P_AUTH_BY)"

                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()

                    db.AddInParameter(commProc, "@P_CTR_REF", DbType.String, dt2.Rows(i)("CTR_REF"))
                    db.AddInParameter(commProc, "@P_D_CODE", DbType.String, dt2.Rows(i)("D_CODE"))
                    db.AddInParameter(commProc, "@P_ACC_NO", DbType.String, dt2.Rows(i)("ACC_NO"))
                    db.AddInParameter(commProc, "@P_AMOUNT", DbType.String, dt2.Rows(i)("AMOUNT"))
                    db.AddInParameter(commProc, "@P_TXN_DATE", DbType.String, dt2.Rows(i)("TXN_DATE"))
                    db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, dt2.Rows(i)("INPUT_BY"))
                    db.AddInParameter(commProc, "@P_AUTH_BY", DbType.String, CommonAppSet.User)

                    db.ExecuteNonQuery(commProc, trans)


                Next

                
                '''''''''''''''

                strSql = "delete  IMP_CCMS_TRANS_M"

                commProc = db.GetSqlStringCommand(strSql)

                commProc.Parameters.Clear()
                'db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dtTrans)

                db.ExecuteNonQuery(commProc, trans)

                trans.Commit()

                'SetFileImpStatus(Environment.NewLine + "Success")

                log_message = " Authorized : CCMS Transaction File. "
                Logger.system_log(log_message)

                Return TransState.Update

            End Using

            oRead.Close()

        Catch ex As Exception
            'SetFileImpStatus(Environment.NewLine + ex.Message)
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        Return tStatus


    End Function



    Private Sub ImportCheckerFiles()


        lblToolStatus.Text = ""

        'Dim intTotFile As Integer = 0
        'Dim intFailed As Integer = 0

        Try



            '_ToTRecordNo = 0
            'intFailed = 0

            'Dim fileNames = My.Computer.FileSystem.GetFiles(txtFileName.Text, _
            '                                                FileIO.SearchOption.SearchTopLevelOnly, "*.dat")
            'intTotFile = fileNames.Count()

            'SetLabelText(lblTotFile, intTotFile.ToString())

            Dim procStatus As TransState = TransState.UnspecifiedError

            'For Each fileName As String In fileNames
            'filesListBox.Items.Add(fileName)
            procStatus = AuthDataFile(txtFileName.Text.Trim())

            If procStatus = TransState.Update Then

            ElseIf procStatus = TransState.NoRecord Then
                'SetFileImpStatus(Environment.NewLine + "No Record !! ")
                'SetFileImpStatus(Environment.NewLine + "Failed: " + fileName)
                'intFailed = intFailed + 1
                'SetLabelText(lblFaileFileNo, intFailed.ToString())
                _ErrorOccur = True
                MessageBox.Show("No Record", "Message!!", MessageBoxButtons.OK, MessageBoxIcon.Error)


            ElseIf procStatus = TransState.MakerCheckerSame Then
                'SetFileImpStatus(Environment.NewLine + "Maker cannot authorize record !! ")
                'SetFileImpStatus(Environment.NewLine + "Failed: " + fileName)
                'intFailed = intFailed + 1
                'SetLabelText(lblFaileFileNo, intFailed.ToString())
                _ErrorOccur = True
                MessageBox.Show("Maker cannot authorize record", "Message!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

            ElseIf procStatus = TransState.UpdateNotPossible Then
                'SetFileImpStatus(Environment.NewLine + "Record Mismatch !! ")
                'SetFileImpStatus(Environment.NewLine + "Failed: " + fileName)
                'intFailed = intFailed + 1
                'SetLabelText(lblFaileFileNo, intFailed.ToString())
                _ErrorOccur = True
                MessageBox.Show("Record mismatch", "Message!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else
                'SetFileImpStatus(Environment.NewLine + "Unspecified Error !!")
                'SetFileImpStatus(Environment.NewLine + "Failed: " + fileName)
                'intFailed = intFailed + 1

                'SetLabelText(lblFaileFileNo, intFailed.ToString())
                _ErrorOccur = True
                MessageBox.Show("Unspecified Error", "Message!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            'Next
            '-------

            _ProcessSuccess = True


        Catch ex As Exception


            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)


        End Try

        'lblFaileFileNo.Text = intFailed.ToString()
        'lblTotRecNo.Text = _ToTRecordNo.ToString()
        'lblTotFile.Text = intTotFile.ToString()

        'SetLabelText(lblFaileFileNo, intFailed.ToString())
        'SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())
        'SetLabelText(lblTotFile, intTotFile.ToString())


    End Sub



    Private Sub btnSetFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        OfdTransFile.ShowDialog()
        'fbdTransFile.ShowNewFolderButton = False
        'fbdTransFile.ShowDialog()

        txtFileName.Text = OfdTransFile.FileName
    End Sub

    Private Sub btnImportMaker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportMaker.Click

        If Not File.Exists(OfdTransFile.FileName) Or txtFileName.Text.Trim() = "" Then
            MessageBox.Show("Select File", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnBrowse.Focus()
            Exit Sub
        End If

        txtFileName.ReadOnly = True
        btnBrowse.Enabled = False
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


        txtFileName.ReadOnly = False
        btnBrowse.Enabled = True
        btnImportMaker.Enabled = True
        btnImportChecker.Enabled = True


        If _ProcessSuccess = True And _ErrorOccur = False Then
            MessageBox.Show("Completed!!", "File Upload Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If




    End Sub

    Private Sub btnImportChecker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportChecker.Click

        If Not File.Exists(OfdTransFile.FileName) Or txtFileName.Text.Trim() = "" Then
            MessageBox.Show("Select File", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnBrowse.Focus()
            Exit Sub
        End If

        'txtFileImpStatus.Clear()


        txtFileName.ReadOnly = True
        btnBrowse.Enabled = False
        btnImportMaker.Enabled = False
        btnImportChecker.Enabled = False



        ProgressBar1.Style = ProgressBarStyle.Marquee


        BackgroundWorker2.RunWorkerAsync()


    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork


        _ProcessSuccess = False
        _ErrorOccur = False


        ImportCheckerFiles()



    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted

        ProgressBar1.Style = ProgressBarStyle.Continuous



        txtFileName.ReadOnly = False
        btnBrowse.Enabled = True
        btnImportMaker.Enabled = True
        btnImportChecker.Enabled = True


        If _ProcessSuccess = True And _ErrorOccur = False Then
            MessageBox.Show("Completed!!", "File Upload Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub
End Class