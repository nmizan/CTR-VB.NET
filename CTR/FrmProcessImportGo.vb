Imports System.IO
Imports System.Globalization
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common


Public Class FrmProcessImportGo
    Dim log_message As String
    Dim _formName As String = "ToolsProcessRuleGoAML"
    Dim opt As SecForm = New SecForm(_formName)

    Private Function CheckProcess() As Boolean



        If txtDateFrom.Text.Trim() = "/  /" Then
            MessageBox.Show("Date From required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return False
        End If

        If txtDateTo.Text.Trim() = "/  /" Then
            MessageBox.Show("Date To required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return False
        End If

        ' From Date

        Dim thisDateFrom As Date
        Dim thisMonthFrom As Integer
        Dim thisYearFrom As Integer
        thisDateFrom = NullHelper.StringToDate(txtDateFrom.Text.Trim)
        thisMonthFrom = Month(thisDateFrom)
        thisYearFrom = Year(thisDateFrom)

        ' To Date

        Dim thisDateTo As Date
        Dim thisMonthTo As Integer
        Dim thisYearTo As Integer
        thisDateTo = NullHelper.StringToDate(txtDateTo.Text.Trim)
        thisMonthTo = Month(thisDateTo)
        thisYearTo = Year(thisDateTo)


        If Not ((thisMonthFrom = thisMonthTo) And (thisYearFrom = thisYearTo)) Then

            MessageBox.Show("Please Give Same Month And Year in Date From And Date To", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return False

        End If




        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dt As New DataTable
            Dim strSql As String = ""

            ' ------------------- update 2012-07-26 ---------------------

            'strSql = "select count(STATUS) cnt from STATUS_IMP_FLEX_TRANS " & _
            '    " where STATUS = 'N' and month(IMPORTED_DATE)=" & txtMonth.Text & " and year(IMPORTED_DATE)=" & txtYear.Text

            strSql = "select count(STATUS) cnt from STATUS_IMP_FLEX_TRANS " & _
                " where IMPORTED_DATE>=@P_IMPORTED_DATE_FROM AND IMPORTED_DATE<=@P_IMPORTED_DATE_TO"
            ' -----------------------------------------------------------
            Dim commProc As DbCommand
            commProc = db.GetSqlStringCommand(strSql)

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@P_IMPORTED_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
            db.AddInParameter(commProc, "@P_IMPORTED_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

            dt = db.ExecuteDataSet(commProc).Tables(0)

            If dt.Rows.Count > 0 Then
                If dt.Rows(0)(0) = 0 Then
                    MessageBox.Show("No unprocessed transaction available for inputed date range." & vbCrLf & " ! Proces failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Else
                MessageBox.Show("Error occured counting Status", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            ' Insert, Delete From Hist

            ' ------------------- update 2015-01-03 ---------------------

            Dim commProc1 As DbCommand = db.GetStoredProcCommand("GO_IMP_FLEX_TRANS_HIST")

            commProc1.Parameters.Clear()

            db.AddInParameter(commProc1, "@TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
            db.AddInParameter(commProc1, "@TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))


            db.AddParameter(commProc1, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            commProc1.CommandTimeout = 3600


            Dim result As Integer

            db.ExecuteNonQuery(commProc1)
            result = db.GetParameterValue(commProc1, "@PROC_RET_VAL")

            If result = 0 Then

            ElseIf result = 1 Then

                MessageBox.Show("transaction Error" & vbCrLf & " Can't Insert Transacion ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False

            End If




            strSql = "select count(distinct i.ACCOUNT) cntAcc from (select ACCOUNT,TXN_DATE from IMP_FLEX_TRANS " & _
                " where len(account)=16 and TXN_DATE>=@P_TXN_DATE_FROM AND TXN_DATE<=@P_TXN_DATE_TO ) i " & _
                " left outer join CUST_BALANCE f " & _
                " on i.ACCOUNT=f.ACCOUNT AND i.TXN_DATE=f.IMPORTED_DATE where f.ACCOUNT is null"




            commProc = db.GetSqlStringCommand(strSql)

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
            db.AddInParameter(commProc, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))


            commProc.CommandTimeout = 1800

            dt = db.ExecuteDataSet(commProc).Tables(0)

            'dt = db.ExecuteDataSet(CommandType.Text, strSql).Tables(0)
            '----------------------------------------


            If dt.Rows.Count > 0 Then
                If dt.Rows(0)(0) > 0 Then
                    MessageBox.Show("Mismatch found of Flex transaction with Flex A/C" & vbCrLf & "Pls update the system by imporing latest Flex Customer A/C", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False

                End If

            End If




        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try




        Return True
    End Function

    Private Sub ProcessTransaction()
        Dim strSql As String = ""
        Dim dt As New DataTable
        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            ' ------------------- update 2012-07-26 ---------------------

            strSql = "delete dbo.GO_FLEX_TRANS_BY_RULE where TXN_DATE>=@P_TXN_DATE_FROM AND TXN_DATE<=@P_TXN_DATE_TO"

            Dim comm1 As DbCommand

            comm1 = db.GetSqlStringCommand(strSql)

            comm1.Parameters.Clear()

            db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
            db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))


            comm1.CommandTimeout = 1800

exe1:
            Try
                db.ExecuteNonQuery(comm1)
            Catch ex As TimeoutException
                comm1.CommandTimeout += comm1.CommandTimeout
                GoTo exe1
            End Try



            strSql = "delete FROM dbo.GO_TMP_FLEX_VS_CCMS"

            Dim commdc As DbCommand

            commdc = db.GetSqlStringCommand(strSql)

            commdc.Parameters.Clear()
            commdc.CommandTimeout = 1800
exe2:
            Try
                db.ExecuteNonQuery(commdc)
            Catch ex As TimeoutException
                commdc.CommandTimeout += commdc.CommandTimeout
                GoTo exe2
            End Try

            ' -----------------------------------------------------------

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                '------------ Rule: Flexcube Teller ----------------

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '     "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '     "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '     "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '     "SEG_RULE) " & _
                '     "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '     "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '     "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '     "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '     "'Flexcube Teller' 'SEG_RULE'  " & _
                '     "from IMP_FLEX_TRANS i " & _
                '     "left outer join CUST_BALANCE a on " & _
                '     "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '     "left outer join FLEX_AC_CLASS c on " & _
                '     "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '     "where i.ACCOUNT<>'195100501' " & _
                '     "and i.ACCOUNT<>'100200101' " & _
                '     "and i.TRN_CODE in ('002','003','004','005') " & _
                '     "and i.APPLICATION_ID ='CTELLR' " & _
                '     "and i.MODULE='DE' " & _
                '     "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '     "and c.ACC_TYPE='Customer' "

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '     "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '     "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '     "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '     "SEG_RULE,ACC_TYPE) " & _
                '     "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '     "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '     "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '     "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '     "'Flexcube Teller' 'SEG_RULE', at.GO_ACC_TYPE_CODE  " & _
                '     "from IMP_FLEX_TRANS i " & _
                '     "left outer join CUST_BALANCE a on " & _
                '     "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '     "left outer join FLEX_AC_CLASS c on " & _
                '     "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '     "left outer join GO_ACC_TYPE at on " & _
                '     "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                '     "where i.ACCOUNT<>'195100501' " & _
                '     "and i.ACCOUNT<>'100200101' " & _
                '     "and i.TRN_CODE in ('002','003','004','005') " & _
                '     "and i.APPLICATION_ID ='CTELLR' " & _
                '     "and i.MODULE='DE' " & _
                '     "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '     "and c.ACC_TYPE='Customer' "

                '' Update On 22-04-2014

                strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                     "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                     "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                     "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                     "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                     "SEG_RULE,ACC_TYPE,VALUE_DATE) " & _
                     "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                     "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                     "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                     "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                     "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                     "'Flexcube Teller' 'SEG_RULE', at.GO_ACC_TYPE_CODE, i.VALUE_DATE  " & _
                     "from IMP_FLEX_TRANS i " & _
                     "left outer join CUST_BALANCE a on " & _
                     "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                     "left outer join FLEX_AC_CLASS c on " & _
                     "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                     "left outer join GO_ACC_TYPE at on " & _
                     "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                     "where i.ACCOUNT<>'195100501' " & _
                     "and i.ACCOUNT<>'100200101' " & _
                     "and i.TRN_CODE in ('002','003','004','005') " & _
                     "and i.APPLICATION_ID ='CTELLR' " & _
                     "and i.MODULE='DE' " & _
                     "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                     "and c.ACC_TYPE='Customer' "


                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

exe3:
                Try
                    db.ExecuteNonQuery(comm1, trans)
                Catch ex As TimeoutException
                    comm1.CommandTimeout += comm1.CommandTimeout
                    GoTo exe3
                End Try



                '------------ Rule: ZRAC  ---------------
                '--- modification for BRD version 0.3 (2012-05-08) -----

                'strSql = "insert into dbo.FLEX_TRANS_BY_RULE " & _
                '    " select i.*,1 'TXN_COUNT','ZRAC' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                '    " from IMP_FLEX_TRANS i " & _
                '    " left outer join FLEX_ACCOUNT a on " & _
                '    " i.ACCOUNT=a.ACCOUNT " & _
                '    " left outer join FLEX_AC_CLASS c on " & _
                '    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    " where i.ACCOUNT<>'195100501' " & _
                '    " and i.ACCOUNT<>'100200101' " & _
                '    " and i.TRN_CODE = '002' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    " and i.REFERENCE LIKE 'G0%ZRAC%'" & _
                '    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                '    " and c.ACC_TYPE='Customer' "

                ' ------------------------

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '     "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '     "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '     "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '     "SEG_RULE) " & _
                '     "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '     "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '     "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '     "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '     "'ZRAC' 'SEG_RULE'  " & _
                '     "from IMP_FLEX_TRANS i " & _
                '     "left outer join CUST_BALANCE a on " & _
                '     "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '     "left outer join FLEX_AC_CLASS c on " & _
                '     "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '     "where i.ACCOUNT<>'195100501' " & _
                '     "and i.ACCOUNT<>'100200101' " & _
                '     "and i.TRN_CODE = '002' " & _
                '     " and i.DRCR_IND ='C' " & _
                '     " and i.REFERENCE LIKE 'G0%ZRAC%'" & _
                '     "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '     "and c.ACC_TYPE='Customer' "


                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '     "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '     "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '     "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '     "SEG_RULE,ACC_TYPE) " & _
                '     "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '     "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '     "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '     "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '     "'ZRAC' 'SEG_RULE' , at.GO_ACC_TYPE_CODE  " & _
                '     "from IMP_FLEX_TRANS i " & _
                '     "left outer join CUST_BALANCE a on " & _
                '     "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '     "left outer join FLEX_AC_CLASS c on " & _
                '     "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '     "left outer join GO_ACC_TYPE at on " & _
                '     "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                '     "where i.ACCOUNT<>'195100501' " & _
                '     "and i.ACCOUNT<>'100200101' " & _
                '     "and i.TRN_CODE = '002' " & _
                '     " and i.DRCR_IND ='C' " & _
                '     " and i.REFERENCE LIKE 'G0%ZRAC%'" & _
                '     "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '     "and c.ACC_TYPE='Customer' "


                '' Update On 22/04/2014

                strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                    "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                    "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                    "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                    "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                    "SEG_RULE,ACC_TYPE,VALUE_DATE) " & _
                    "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                    "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                    "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                    "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                    "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                    "'ZRAC' 'SEG_RULE' , at.GO_ACC_TYPE_CODE, i.VALUE_DATE  " & _
                    "from IMP_FLEX_TRANS i " & _
                    "left outer join CUST_BALANCE a on " & _
                    "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                    "left outer join FLEX_AC_CLASS c on " & _
                    "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                    "left outer join GO_ACC_TYPE at on " & _
                    "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                    "where i.ACCOUNT<>'195100501' " & _
                    "and i.ACCOUNT<>'100200101' " & _
                    "and i.TRN_CODE = '002' " & _
                    " and i.DRCR_IND ='C' " & _
                    " and i.REFERENCE LIKE 'G0%ZRAC%'" & _
                    "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                    "and c.ACC_TYPE='Customer' "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))
exe4:
                Try
                    db.ExecuteNonQuery(comm1, trans)
                Catch ex As TimeoutException
                    comm1.CommandTimeout += comm1.CommandTimeout
                    GoTo exe4
                End Try



                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                '------------ Rule: non-ALICO (Non Consolidated Format) ---------------
                'strSql = "insert into dbo.FLEX_TRANS_BY_RULE " & _
                '    " select i.*,1 'TXN_COUNT','non-ALICO' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                '    " from IMP_FLEX_TRANS i " & _
                '    " left outer join FLEX_ACCOUNT a on " & _
                '    " i.ACCOUNT=a.ACCOUNT " & _
                '    " left outer join FLEX_AC_CLASS c on " & _
                '    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    " where i.ACCOUNT<>'195100501' " & _
                '    " and i.ACCOUNT<>'G010000200038002' " & _
                '    " and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                '    " and c.ACC_TYPE='Customer' "

                '----------------

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '    "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '    "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '    "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '    "SEG_RULE) " & _
                '    "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '    "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '    "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '    "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '    "'non-ALICO' 'SEG_RULE'  " & _
                '    "from IMP_FLEX_TRANS i " & _
                '    "left outer join CUST_BALANCE a on " & _
                '    "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '    "left outer join FLEX_AC_CLASS c on " & _
                '    "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    "where i.ACCOUNT<>'195100501' " & _
                '    "and i.ACCOUNT<>'G010000200038002' " & _
                '    "and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '    "and c.ACC_TYPE='Customer' "

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '    "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '    "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '    "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '    "SEG_RULE,ACC_TYPE) " & _
                '    "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '    "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '    "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '    "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '    "'non-ALICO' 'SEG_RULE' , at.GO_ACC_TYPE_CODE  " & _
                '    "from IMP_FLEX_TRANS i " & _
                '    "left outer join CUST_BALANCE a on " & _
                '    "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '    "left outer join FLEX_AC_CLASS c on " & _
                '    "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    "left outer join GO_ACC_TYPE at on " & _
                '    "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                '    "where i.ACCOUNT<>'195100501' " & _
                '    "and i.ACCOUNT<>'G010000200038002' " & _
                '    "and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '    "and c.ACC_TYPE='Customer' "

                '' Update on 22/04/2014

                strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                   "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                   "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                   "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                   "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                   "SEG_RULE,ACC_TYPE,VALUE_DATE) " & _
                   "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                   "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                   "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                   "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                   "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                   "'non-ALICO' 'SEG_RULE' , at.GO_ACC_TYPE_CODE, i.VALUE_DATE  " & _
                   "from IMP_FLEX_TRANS i " & _
                   "left outer join CUST_BALANCE a on " & _
                   "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                   "left outer join FLEX_AC_CLASS c on " & _
                   "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                   "left outer join GO_ACC_TYPE at on " & _
                   "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                   "where i.ACCOUNT<>'195100501' " & _
                   "and i.ACCOUNT<>'G010000200038002' " & _
                   "and i.TRN_CODE = '355' " & _
                   " and i.DRCR_IND ='C' " & _
                   "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                   "and c.ACC_TYPE='Customer' "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))
exe5:
                Try
                    db.ExecuteNonQuery(comm1, trans)
                Catch ex As TimeoutException
                    comm1.CommandTimeout += comm1.CommandTimeout
                    GoTo exe5
                End Try


                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                '------------ Rule: ALICO  (Consolidated Format)---------------


                'strSql = " select i.*,1 'TXN_COUNT','ALICO' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                '    " from IMP_FLEX_TRANS i " & _
                '    " left outer join FLEX_ACCOUNT a on " & _
                '    " i.ACCOUNT=a.ACCOUNT " & _
                '    " left outer join FLEX_AC_CLASS c on " & _
                '    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    " where i.ACCOUNT='G010000200038002' " & _
                '    " and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                '    " and c.ACC_TYPE='Customer' "

                '----------------

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '    "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '    "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '    "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '    "SEG_RULE) " & _
                '    "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '    "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '    "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '    "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '    "'ALICO' 'SEG_RULE'  " & _
                '    "from IMP_FLEX_TRANS i " & _
                '    "left outer join CUST_BALANCE a on " & _
                '    "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '    "left outer join FLEX_AC_CLASS c on " & _
                '    "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    "where i.ACCOUNT='G010000200038002' " & _
                '    "and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '    "and c.ACC_TYPE='Customer' "

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '    "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '    "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '    "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '    "SEG_RULE,ACC_TYPE) " & _
                '    "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '    "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '    "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '    "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '    "'ALICO' 'SEG_RULE' , at.GO_ACC_TYPE_CODE  " & _
                '    "from IMP_FLEX_TRANS i " & _
                '    "left outer join CUST_BALANCE a on " & _
                '    "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '    "left outer join FLEX_AC_CLASS c on " & _
                '    "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    "left outer join GO_ACC_TYPE at on " & _
                '    "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                '    "where i.ACCOUNT='G010000200038002' " & _
                '    "and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '    "and c.ACC_TYPE='Customer' "

                '' UPDATE ON 22/04/2014

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '  "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '  "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '  "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '  "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '  "SEG_RULE,ACC_TYPE,VALUE_DATE) " & _
                '  "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '  "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '  "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '  "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '  "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '  "'ALICO' 'SEG_RULE' , at.GO_ACC_TYPE_CODE, i.VALUE_DATE  " & _
                '  "from IMP_FLEX_TRANS i " & _
                '  "left outer join CUST_BALANCE a on " & _
                '  "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '  "left outer join FLEX_AC_CLASS c on " & _
                '  "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '  "left outer join GO_ACC_TYPE at on " & _
                '  "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                '  "where i.ACCOUNT='G010000200038002' " & _
                '  "and i.TRN_CODE = '355' " & _
                '  " and i.DRCR_IND ='C' " & _
                '  "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '  "and c.ACC_TYPE='Customer' "

                'comm1 = db.GetSqlStringCommand(strSql)

                'comm1.Parameters.Clear()

                'db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                'db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                'db.ExecuteNonQuery(comm1, trans)

                '' UPDATE ON 24/07/2014

                strSql = "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                  "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                  "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                  "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                  "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                  "'ALICO' 'SEG_RULE' , at.GO_ACC_TYPE_CODE, i.VALUE_DATE  " & _
                  "from IMP_FLEX_TRANS i " & _
                  "left outer join CUST_BALANCE a on " & _
                  "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                  "left outer join FLEX_AC_CLASS c on " & _
                  "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                  "left outer join GO_ACC_TYPE at on " & _
                  "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                  "where i.ACCOUNT='G010000200038002' " & _
                  "and i.TRN_CODE = '355' " & _
                  " and i.DRCR_IND ='C' " & _
                  "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                  "and c.ACC_TYPE='Customer' "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                'db.ExecuteNonQuery(comm1, trans)
exe6:
                Try
                    dt = db.ExecuteDataSet(comm1, trans).Tables(0)
                Catch ex As TimeoutException
                    comm1.CommandTimeout += comm1.CommandTimeout
                    GoTo exe6
                End Try


                Dim intPos As Integer = -1
                Dim strTemp As String = ""
                Dim strCount As String = ""
                Dim TxnCount As Integer = 1
                Dim inc As Integer = 0
                Dim commProc As DbCommand
                Dim dtCMS As DataTable
                Dim cmsRow As Integer = 0

                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1


                        intPos = -1
                        TxnCount = 1
                        inc = 0
                        strCount = ""
                        strTemp = dt.Rows(i)("TRANSACTION_DESCRIPTION").ToString().Trim()
                        intPos = strTemp.IndexOf("SLIPCOUNT ")
                        If intPos >= 0 Then
                            While (Char.IsDigit(strTemp(intPos + 10 + inc)))
                                strCount = strCount + strTemp(intPos + 10 + inc)
                                inc = inc + 1
                                If intPos + 10 + inc = strTemp.Length Then
                                    Exit While
                                End If
                            End While

                            Integer.TryParse(strCount, TxnCount)
                            If TxnCount = 0 Then TxnCount = 1
                        End If



                        '---------------------



                        'strSql = "SELECT * FROM IMP_CCMS_TRANS " & _
                        '   "WHERE ACC_NO='G010000200038002' AND CTR_REF=@P_CTR_REF AND TXN_DATE=@P_TXN_DATE "

                        ' update date 05-01-2015

                        strSql = "SELECT * FROM IMP_CCMS_TRANS " & _
                                 "WHERE ACC_NO='G010000200038002' AND CTR_REF=@P_CTR_REF "

                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        db.AddInParameter(commProc, "@P_CTR_REF", DbType.String, dt.Rows(i)("HOST_REFERENCE"))

                        'update date 05-01-2015

                        'db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dt.Rows(i)("TXN_DATE"))

                        Dim CCMSAMT As Decimal = 0
exe7:
                        Try
                            dtCMS = db.ExecuteDataSet(commProc, trans).Tables(0)
                        Catch ex As TimeoutException
                            commProc.CommandTimeout += commProc.CommandTimeout
                            GoTo exe7
                        End Try



                        For cmsRow = 0 To dtCMS.Rows.Count - 1

                            strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                            "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                            "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                            "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                            "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                            "SEG_RULE,ACC_TYPE,VALUE_DATE) " & _
                            " values(@P_AC_ENTRY_SR_NO, @P_AC_BRANCH, " & _
                            "@P_BATCH_NO, @P_ACCOUNT, @P_ACCOUNT_CURRRENCY, @P_LCY_AMOUNT, @P_FCY_AMOUNT, " & _
                            "@P_BALANCE_AMOUNT, @P_TRN_CODE, @P_REFERENCE, @P_DRCR_IND, @P_MODULE, " & _
                            "@P_USER_ID, @P_AUTH_ID, @P_RELATED_CUSTOMER, @P_HOST_REFERENCE, " & _
                            "@P_TRANSACTION_DESCRIPTION, @P_APPLICATION_ID, @P_TXN_BRANCH, @P_TXN_DATE, " & _
                            "@P_SEG_RULE,@P_ACC_TYPE,@P_VALUE_DATE)"



                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            '' UPDATE ON 15/10/2014
                            'db.AddInParameter(commProc, "@P_AC_ENTRY_SR_NO", DbType.String, "CMS-" + dtCMS.Rows(cmsRow)("D_CODE")) '
                            db.AddInParameter(commProc, "@P_AC_ENTRY_SR_NO", DbType.String, "CMS-" + dtCMS.Rows(cmsRow)("CTR_REF") + "-" + dtCMS.Rows(cmsRow)("D_CODE")) '

                            db.AddInParameter(commProc, "@P_AC_BRANCH", DbType.String, dt.Rows(i)("AC_BRANCH")) '

                            db.AddInParameter(commProc, "@P_BATCH_NO", DbType.String, dt.Rows(i)("BATCH_NO")) '
                            db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dt.Rows(i)("ACCOUNT")) '
                            db.AddInParameter(commProc, "@P_ACCOUNT_CURRRENCY", DbType.String, dt.Rows(i)("ACCOUNT_CURRRENCY")) '
                            db.AddInParameter(commProc, "@P_LCY_AMOUNT", DbType.String, dtCMS.Rows(cmsRow)("AMOUNT")) '
                            db.AddInParameter(commProc, "@P_FCY_AMOUNT", DbType.String, 0) '

                            db.AddInParameter(commProc, "@P_BALANCE_AMOUNT", DbType.String, dt.Rows(i)("CURRENT_BALANCE_ACC_LCY")) '
                            db.AddInParameter(commProc, "@P_TRN_CODE", DbType.String, dt.Rows(i)("TRN_CODE")) '
                            db.AddInParameter(commProc, "@P_REFERENCE", DbType.String, dt.Rows(i)("REFERENCE")) '
                            db.AddInParameter(commProc, "@P_DRCR_IND", DbType.String, dt.Rows(i)("DRCR_IND")) '
                            db.AddInParameter(commProc, "@P_MODULE", DbType.String, dt.Rows(i)("MODULE")) '

                            db.AddInParameter(commProc, "@P_USER_ID", DbType.String, dt.Rows(i)("USER_ID")) '
                            db.AddInParameter(commProc, "@P_AUTH_ID", DbType.String, dt.Rows(i)("AUTH_ID")) '
                            db.AddInParameter(commProc, "@P_RELATED_CUSTOMER", DbType.String, dt.Rows(i)("RELATED_CUSTOMER")) '
                            db.AddInParameter(commProc, "@P_HOST_REFERENCE", DbType.String, dt.Rows(i)("HOST_REFERENCE")) '

                            db.AddInParameter(commProc, "@P_TRANSACTION_DESCRIPTION", DbType.String, dt.Rows(i)("TRANSACTION_DESCRIPTION")) '
                            db.AddInParameter(commProc, "@P_APPLICATION_ID", DbType.String, dt.Rows(i)("APPLICATION_ID")) '
                            db.AddInParameter(commProc, "@P_TXN_BRANCH", DbType.String, dt.Rows(i)("REFERENCE").ToString().Trim().Substring(0, 3)) '
                            db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dt.Rows(i)("TXN_DATE")) '
                            db.AddInParameter(commProc, "@P_ACC_TYPE", DbType.String, dt.Rows(i)("GO_ACC_TYPE_CODE")) '
                            db.AddInParameter(commProc, "@P_VALUE_DATE", DbType.Date, dt.Rows(i)("VALUE_DATE")) '


                            db.AddInParameter(commProc, "@P_SEG_RULE", DbType.String, "ALICO")


                            Dim result As Integer = -121
exe8:
                            Try
                                result = db.ExecuteNonQuery(commProc, trans)
                            Catch ex As TimeoutException
                                commProc.CommandTimeout += commProc.CommandTimeout
                                GoTo exe8
                            End Try

                            If result < 0 Then
                                MessageBox.Show("Process for ALICO error ", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                trans.Rollback()
                                Exit Sub
                            Else
                            End If


                            CCMSAMT += NullHelper.ToDecNum(dtCMS.Rows(cmsRow)("AMOUNT"))


                        Next


                        ' Insert CCMS vs FLEX table MISMatch

                        If Not ((dtCMS.Rows.Count = TxnCount) Or (CCMSAMT = NullHelper.ToDecNum(dt.Rows(i)("LCY_AMOUNT")))) Then

                            strSql = "insert into  GO_TMP_FLEX_VS_CCMS(ACCOUNT, TXN_DATE, FLEX_AMOUNT, SLIPCOUNT, CCMS_AMOUNT, TRANS_SUM, CTR_REF) " & _
                                " values(@ACCOUNT, @TXN_DATE, @FLEX_AMOUNT, @SLIPCOUNT, @CCMS_AMOUNT, @TRANS_SUM, @CTR_REF)"



                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            db.AddInParameter(commProc, "@ACCOUNT", DbType.String, dt.Rows(i)("ACCOUNT")) '
                            db.AddInParameter(commProc, "@TXN_DATE", DbType.Date, dt.Rows(i)("TXN_DATE"))
                            db.AddInParameter(commProc, "@FLEX_AMOUNT", DbType.String, dt.Rows(i)("LCY_AMOUNT")) '

                            db.AddInParameter(commProc, "@SLIPCOUNT", DbType.Int32, TxnCount) '
                            db.AddInParameter(commProc, "@CCMS_AMOUNT", DbType.String, CCMSAMT) '
                            db.AddInParameter(commProc, "@TRANS_SUM", DbType.Int32, dtCMS.Rows.Count)

                            db.AddInParameter(commProc, "@CTR_REF", DbType.String, dt.Rows(i)("HOST_REFERENCE"))


exe9:
                            Try
                                db.ExecuteNonQuery(commProc, trans)
                            Catch ex As TimeoutException
                                commProc.CommandTimeout += commProc.CommandTimeout
                                GoTo exe9
                            End Try


                        End If



                        'if CCMS data not uploaded or no matching data found
                        If dtCMS.Rows.Count = 0 Then

                            

                            strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                            "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                            "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                            "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                            "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                            "SEG_RULE,ACC_TYPE,VALUE_DATE) " & _
                            " values(@P_AC_ENTRY_SR_NO, @P_AC_BRANCH, " & _
                            "@P_BATCH_NO, @P_ACCOUNT, @P_ACCOUNT_CURRRENCY, @P_LCY_AMOUNT, @P_FCY_AMOUNT, " & _
                            "@P_BALANCE_AMOUNT, @P_TRN_CODE, @P_REFERENCE, @P_DRCR_IND, @P_MODULE, " & _
                            "@P_USER_ID, @P_AUTH_ID, @P_RELATED_CUSTOMER, @P_HOST_REFERENCE, " & _
                            "@P_TRANSACTION_DESCRIPTION, @P_APPLICATION_ID, @P_TXN_BRANCH, @P_TXN_DATE, " & _
                            "@P_SEG_RULE,@P_ACC_TYPE,@P_VALUE_DATE)"



                            commProc = db.GetSqlStringCommand(strSql)
                            'LCY_AMOUNT, FCY_AMOUNT
                            commProc.Parameters.Clear()
                            db.AddInParameter(commProc, "@P_AC_ENTRY_SR_NO", DbType.String, dt.Rows(i)("AC_ENTRY_SR_NO")) '
                            db.AddInParameter(commProc, "@P_AC_BRANCH", DbType.String, dt.Rows(i)("AC_BRANCH")) '

                            db.AddInParameter(commProc, "@P_BATCH_NO", DbType.String, dt.Rows(i)("BATCH_NO")) '
                            db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dt.Rows(i)("ACCOUNT")) '
                            db.AddInParameter(commProc, "@P_ACCOUNT_CURRRENCY", DbType.String, dt.Rows(i)("ACCOUNT_CURRRENCY")) '
                            db.AddInParameter(commProc, "@P_LCY_AMOUNT", DbType.String, dt.Rows(i)("LCY_AMOUNT")) '
                            db.AddInParameter(commProc, "@P_FCY_AMOUNT", DbType.String, dt.Rows(i)("FCY_AMOUNT")) '

                            db.AddInParameter(commProc, "@P_BALANCE_AMOUNT", DbType.String, dt.Rows(i)("CURRENT_BALANCE_ACC_LCY")) '
                            db.AddInParameter(commProc, "@P_TRN_CODE", DbType.String, dt.Rows(i)("TRN_CODE")) '
                            db.AddInParameter(commProc, "@P_REFERENCE", DbType.String, dt.Rows(i)("REFERENCE")) '
                            db.AddInParameter(commProc, "@P_DRCR_IND", DbType.String, dt.Rows(i)("DRCR_IND")) '
                            db.AddInParameter(commProc, "@P_MODULE", DbType.String, dt.Rows(i)("MODULE")) '

                            db.AddInParameter(commProc, "@P_USER_ID", DbType.String, dt.Rows(i)("USER_ID")) '
                            db.AddInParameter(commProc, "@P_AUTH_ID", DbType.String, dt.Rows(i)("AUTH_ID")) '
                            db.AddInParameter(commProc, "@P_RELATED_CUSTOMER", DbType.String, dt.Rows(i)("RELATED_CUSTOMER")) '
                            db.AddInParameter(commProc, "@P_HOST_REFERENCE", DbType.String, dt.Rows(i)("HOST_REFERENCE")) '

                            db.AddInParameter(commProc, "@P_TRANSACTION_DESCRIPTION", DbType.String, dt.Rows(i)("TRANSACTION_DESCRIPTION")) '
                            db.AddInParameter(commProc, "@P_APPLICATION_ID", DbType.String, dt.Rows(i)("APPLICATION_ID")) '
                            db.AddInParameter(commProc, "@P_TXN_BRANCH", DbType.String, dt.Rows(i)("REFERENCE").ToString().Trim().Substring(0, 3)) '
                            db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dt.Rows(i)("TXN_DATE")) '
                            db.AddInParameter(commProc, "@P_ACC_TYPE", DbType.String, dt.Rows(i)("GO_ACC_TYPE_CODE")) '
                            db.AddInParameter(commProc, "@P_VALUE_DATE", DbType.Date, dt.Rows(i)("VALUE_DATE")) '


                            db.AddInParameter(commProc, "@P_SEG_RULE", DbType.String, "ALICO")


                            Dim result As Integer = -121
exe10:
                            Try
                                result = db.ExecuteNonQuery(commProc, trans)
                            Catch ex As TimeoutException
                                commProc.CommandTimeout += commProc.CommandTimeout
                                GoTo exe10
                            End Try


                            If result < 0 Then
                                MessageBox.Show("Process for ALICO error ", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                trans.Rollback()
                                Exit Sub
                            Else
                            End If


                        End If

                        '---------------

                    Next

                End If


                '------ Delete Specific Govt Account -------

                strSql = "delete dbo.GO_FLEX_TRANS_BY_RULE where TXN_DATE >=@P_TXN_DATE_FROM and TXN_DATE <=@P_TXN_DATE_TO " & _
                    " and RELATED_CUSTOMER in (" & _
                    "'BD0200141','BD0200110','BD0200004','BD0200013','BD0200502','BD0200200'," & _
                    "'BD0100053','BD0200528','BD0200809','BD0200714','BD0200593','BD0200587'," & _
                    "'BD0200676','BD0200767','BD0200205','BD0200873','BD0200723','BD0200062'," & _
                    "'BD0200738','BD0200838','BD0100058','BD0200691','BD0200702','BD0200735'," & _
                    "'BD0200742','BD0200772')" & _
                    "and DRCR_IND='C'"

                Dim comm2 As DbCommand
                comm2 = db.GetSqlStringCommand(strSql)

                comm2.Parameters.Clear()

                db.AddInParameter(comm2, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm2, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                comm2.CommandTimeout = 1800

exe11:
                Try
                    db.ExecuteNonQuery(comm2, trans)
                Catch ex As TimeoutException
                    comm2.CommandTimeout += comm2.CommandTimeout
                    GoTo exe11
                End Try



                'Dim comm2 As DbCommand
                'comm2 = db.GetSqlStringCommand(strSql)

                'comm2.Parameters.Clear()

                'comm2.CommandTimeout = 1800

                'db.ExecuteNonQuery(comm2, trans)

                '-------------------------------------------------------------



             
                ' Transaction code convertion for BBK 

                strSql = "update GO_FLEX_TRANS_BY_RULE set BBK_FUNDS_TYPE='A' " & _
                   " where DRCR_IND='C' " & _
                   "and TXN_DATE>=@P_TXN_DATE_FROM AND TXN_DATE<=@P_TXN_DATE_TO "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

exe12:
                Try
                    db.ExecuteNonQuery(comm1, trans)
                Catch ex As TimeoutException
                    comm1.CommandTimeout += comm1.CommandTimeout
                    GoTo exe12
                End Try




                strSql = "update GO_FLEX_TRANS_BY_RULE set BBK_FUNDS_TYPE='W' " & _
                   " where DRCR_IND='D' " & _
                   "and TXN_DATE>=@P_TXN_DATE_FROM AND TXN_DATE<=@P_TXN_DATE_TO "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

exe13:
                Try
                    db.ExecuteNonQuery(comm1, trans)
                Catch ex As TimeoutException
                    comm1.CommandTimeout += comm1.CommandTimeout
                    GoTo exe13
                End Try


                '------------ update   ---------------

                'strSql = "update STATUS_IMP_FLEX_TRANS set STATUS = 'P' " & _
                '    " where month(IMPORTED_DATE)=" & txtMonth.Text & " and year(IMPORTED_DATE)=" & txtYear.Text

                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                strSql = "update STATUS_IMP_FLEX_TRANS set GOSTATUS='P' " & _
                   "WHERE IMPORTED_DATE>=@P_IMP_DATE_FROM AND IMPORTED_DATE<=@P_IMP_DATE_TO "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_IMP_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_IMP_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

exe14:
                Try
                    db.ExecuteNonQuery(comm1, trans)
                Catch ex As TimeoutException
                    comm1.CommandTimeout += comm1.CommandTimeout
                    GoTo exe14
                End Try




                strSql = "update STATUS_IMP_FLEX_CUST set STATUS='P' " & _
                  "WHERE IMPORTED_DATE>=@P_IMP_DATE_FROM AND IMPORTED_DATE<=@P_IMP_DATE_TO "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_IMP_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_IMP_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

exe15:
                Try
                    db.ExecuteNonQuery(comm1, trans)
                Catch ex As TimeoutException
                    comm1.CommandTimeout += comm1.CommandTimeout
                    GoTo exe15
                End Try



                trans.Commit()
                log_message = " Imported : Flex File By Rule (goAML) "
                Logger.system_log(log_message)

                MessageBox.Show("Process Completed Successfully", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End Using






        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub



    Private Sub ProcessTransactionOld()
        Dim strSql As String = ""
        Dim dt As New DataTable
        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            ' ------------------- update 2012-07-26 ---------------------

            strSql = "delete dbo.GO_FLEX_TRANS_BY_RULE where TXN_DATE>=@P_TXN_DATE_FROM AND TXN_DATE<=@P_TXN_DATE_TO"

            Dim comm1 As DbCommand

            comm1 = db.GetSqlStringCommand(strSql)

            comm1.Parameters.Clear()

            db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
            db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))


            comm1.CommandTimeout = 1800

            db.ExecuteNonQuery(comm1)

            ' -----------------------------------------------------------

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                '------------ Rule: Flexcube Teller ----------------

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '     "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '     "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '     "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '     "SEG_RULE) " & _
                '     "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '     "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '     "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '     "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '     "'Flexcube Teller' 'SEG_RULE'  " & _
                '     "from IMP_FLEX_TRANS i " & _
                '     "left outer join CUST_BALANCE a on " & _
                '     "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '     "left outer join FLEX_AC_CLASS c on " & _
                '     "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '     "where i.ACCOUNT<>'195100501' " & _
                '     "and i.ACCOUNT<>'100200101' " & _
                '     "and i.TRN_CODE in ('002','003','004','005') " & _
                '     "and i.APPLICATION_ID ='CTELLR' " & _
                '     "and i.MODULE='DE' " & _
                '     "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '     "and c.ACC_TYPE='Customer' "

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '     "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '     "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '     "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '     "SEG_RULE,ACC_TYPE) " & _
                '     "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '     "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '     "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '     "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '     "'Flexcube Teller' 'SEG_RULE', at.GO_ACC_TYPE_CODE  " & _
                '     "from IMP_FLEX_TRANS i " & _
                '     "left outer join CUST_BALANCE a on " & _
                '     "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '     "left outer join FLEX_AC_CLASS c on " & _
                '     "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '     "left outer join GO_ACC_TYPE at on " & _
                '     "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                '     "where i.ACCOUNT<>'195100501' " & _
                '     "and i.ACCOUNT<>'100200101' " & _
                '     "and i.TRN_CODE in ('002','003','004','005') " & _
                '     "and i.APPLICATION_ID ='CTELLR' " & _
                '     "and i.MODULE='DE' " & _
                '     "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '     "and c.ACC_TYPE='Customer' "

                '' Update On 22-04-2014

                strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                     "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                     "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                     "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                     "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                     "SEG_RULE,ACC_TYPE,VALUE_DATE) " & _
                     "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                     "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                     "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                     "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                     "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                     "'Flexcube Teller' 'SEG_RULE', at.GO_ACC_TYPE_CODE, i.VALUE_DATE  " & _
                     "from IMP_FLEX_TRANS i " & _
                     "left outer join CUST_BALANCE a on " & _
                     "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                     "left outer join FLEX_AC_CLASS c on " & _
                     "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                     "left outer join GO_ACC_TYPE at on " & _
                     "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                     "where i.ACCOUNT<>'195100501' " & _
                     "and i.ACCOUNT<>'100200101' " & _
                     "and i.TRN_CODE in ('002','003','004','005') " & _
                     "and i.APPLICATION_ID ='CTELLR' " & _
                     "and i.MODULE='DE' " & _
                     "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                     "and c.ACC_TYPE='Customer' "


                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(comm1, trans)


                '------------ Rule: ZRAC  ---------------
                '--- modification for BRD version 0.3 (2012-05-08) -----

                'strSql = "insert into dbo.FLEX_TRANS_BY_RULE " & _
                '    " select i.*,1 'TXN_COUNT','ZRAC' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                '    " from IMP_FLEX_TRANS i " & _
                '    " left outer join FLEX_ACCOUNT a on " & _
                '    " i.ACCOUNT=a.ACCOUNT " & _
                '    " left outer join FLEX_AC_CLASS c on " & _
                '    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    " where i.ACCOUNT<>'195100501' " & _
                '    " and i.ACCOUNT<>'100200101' " & _
                '    " and i.TRN_CODE = '002' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    " and i.REFERENCE LIKE 'G0%ZRAC%'" & _
                '    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                '    " and c.ACC_TYPE='Customer' "

                ' ------------------------

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '     "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '     "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '     "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '     "SEG_RULE) " & _
                '     "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '     "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '     "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '     "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '     "'ZRAC' 'SEG_RULE'  " & _
                '     "from IMP_FLEX_TRANS i " & _
                '     "left outer join CUST_BALANCE a on " & _
                '     "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '     "left outer join FLEX_AC_CLASS c on " & _
                '     "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '     "where i.ACCOUNT<>'195100501' " & _
                '     "and i.ACCOUNT<>'100200101' " & _
                '     "and i.TRN_CODE = '002' " & _
                '     " and i.DRCR_IND ='C' " & _
                '     " and i.REFERENCE LIKE 'G0%ZRAC%'" & _
                '     "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '     "and c.ACC_TYPE='Customer' "


                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '     "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '     "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '     "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '     "SEG_RULE,ACC_TYPE) " & _
                '     "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '     "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '     "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '     "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '     "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '     "'ZRAC' 'SEG_RULE' , at.GO_ACC_TYPE_CODE  " & _
                '     "from IMP_FLEX_TRANS i " & _
                '     "left outer join CUST_BALANCE a on " & _
                '     "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '     "left outer join FLEX_AC_CLASS c on " & _
                '     "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '     "left outer join GO_ACC_TYPE at on " & _
                '     "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                '     "where i.ACCOUNT<>'195100501' " & _
                '     "and i.ACCOUNT<>'100200101' " & _
                '     "and i.TRN_CODE = '002' " & _
                '     " and i.DRCR_IND ='C' " & _
                '     " and i.REFERENCE LIKE 'G0%ZRAC%'" & _
                '     "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '     "and c.ACC_TYPE='Customer' "


                '' Update On 22/04/2014

                strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                    "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                    "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                    "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                    "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                    "SEG_RULE,ACC_TYPE,VALUE_DATE) " & _
                    "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                    "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                    "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                    "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                    "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                    "'ZRAC' 'SEG_RULE' , at.GO_ACC_TYPE_CODE, i.VALUE_DATE  " & _
                    "from IMP_FLEX_TRANS i " & _
                    "left outer join CUST_BALANCE a on " & _
                    "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                    "left outer join FLEX_AC_CLASS c on " & _
                    "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                    "left outer join GO_ACC_TYPE at on " & _
                    "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                    "where i.ACCOUNT<>'195100501' " & _
                    "and i.ACCOUNT<>'100200101' " & _
                    "and i.TRN_CODE = '002' " & _
                    " and i.DRCR_IND ='C' " & _
                    " and i.REFERENCE LIKE 'G0%ZRAC%'" & _
                    "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                    "and c.ACC_TYPE='Customer' "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(comm1, trans)


                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                '------------ Rule: non-ALICO (Non Consolidated Format) ---------------
                'strSql = "insert into dbo.FLEX_TRANS_BY_RULE " & _
                '    " select i.*,1 'TXN_COUNT','non-ALICO' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                '    " from IMP_FLEX_TRANS i " & _
                '    " left outer join FLEX_ACCOUNT a on " & _
                '    " i.ACCOUNT=a.ACCOUNT " & _
                '    " left outer join FLEX_AC_CLASS c on " & _
                '    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    " where i.ACCOUNT<>'195100501' " & _
                '    " and i.ACCOUNT<>'G010000200038002' " & _
                '    " and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                '    " and c.ACC_TYPE='Customer' "

                '----------------

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '    "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '    "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '    "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '    "SEG_RULE) " & _
                '    "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '    "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '    "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '    "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '    "'non-ALICO' 'SEG_RULE'  " & _
                '    "from IMP_FLEX_TRANS i " & _
                '    "left outer join CUST_BALANCE a on " & _
                '    "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '    "left outer join FLEX_AC_CLASS c on " & _
                '    "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    "where i.ACCOUNT<>'195100501' " & _
                '    "and i.ACCOUNT<>'G010000200038002' " & _
                '    "and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '    "and c.ACC_TYPE='Customer' "

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '    "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '    "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '    "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '    "SEG_RULE,ACC_TYPE) " & _
                '    "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '    "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '    "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '    "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '    "'non-ALICO' 'SEG_RULE' , at.GO_ACC_TYPE_CODE  " & _
                '    "from IMP_FLEX_TRANS i " & _
                '    "left outer join CUST_BALANCE a on " & _
                '    "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '    "left outer join FLEX_AC_CLASS c on " & _
                '    "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    "left outer join GO_ACC_TYPE at on " & _
                '    "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                '    "where i.ACCOUNT<>'195100501' " & _
                '    "and i.ACCOUNT<>'G010000200038002' " & _
                '    "and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '    "and c.ACC_TYPE='Customer' "

                '' Update on 22/04/2014

                strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                   "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                   "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                   "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                   "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                   "SEG_RULE,ACC_TYPE,VALUE_DATE) " & _
                   "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                   "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                   "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                   "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                   "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                   "'non-ALICO' 'SEG_RULE' , at.GO_ACC_TYPE_CODE, i.VALUE_DATE  " & _
                   "from IMP_FLEX_TRANS i " & _
                   "left outer join CUST_BALANCE a on " & _
                   "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                   "left outer join FLEX_AC_CLASS c on " & _
                   "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                   "left outer join GO_ACC_TYPE at on " & _
                   "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                   "where i.ACCOUNT<>'195100501' " & _
                   "and i.ACCOUNT<>'G010000200038002' " & _
                   "and i.TRN_CODE = '355' " & _
                   " and i.DRCR_IND ='C' " & _
                   "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                   "and c.ACC_TYPE='Customer' "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(comm1, trans)

                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                '------------ Rule: ALICO  (Consolidated Format)---------------


                'strSql = " select i.*,1 'TXN_COUNT','ALICO' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                '    " from IMP_FLEX_TRANS i " & _
                '    " left outer join FLEX_ACCOUNT a on " & _
                '    " i.ACCOUNT=a.ACCOUNT " & _
                '    " left outer join FLEX_AC_CLASS c on " & _
                '    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    " where i.ACCOUNT='G010000200038002' " & _
                '    " and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                '    " and c.ACC_TYPE='Customer' "

                '----------------

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '    "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '    "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '    "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '    "SEG_RULE) " & _
                '    "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '    "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '    "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '    "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '    "'ALICO' 'SEG_RULE'  " & _
                '    "from IMP_FLEX_TRANS i " & _
                '    "left outer join CUST_BALANCE a on " & _
                '    "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '    "left outer join FLEX_AC_CLASS c on " & _
                '    "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    "where i.ACCOUNT='G010000200038002' " & _
                '    "and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '    "and c.ACC_TYPE='Customer' "

                'strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                '    "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                '    "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                '    "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                '    "SEG_RULE,ACC_TYPE) " & _
                '    "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                '    "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                '    "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                '    "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                '    "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                '    "'ALICO' 'SEG_RULE' , at.GO_ACC_TYPE_CODE  " & _
                '    "from IMP_FLEX_TRANS i " & _
                '    "left outer join CUST_BALANCE a on " & _
                '    "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                '    "left outer join FLEX_AC_CLASS c on " & _
                '    "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    "left outer join GO_ACC_TYPE at on " & _
                '    "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                '    "where i.ACCOUNT='G010000200038002' " & _
                '    "and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                '    "and c.ACC_TYPE='Customer' "

                '' UPDATE ON 22/04/2014

                strSql = "insert into  GO_FLEX_TRANS_BY_RULE(AC_ENTRY_SR_NO, AC_BRANCH, " & _
                  "BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, FCY_AMOUNT, " & _
                  "BALANCE_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, " & _
                  "[USER_ID], AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, " & _
                  "TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, " & _
                  "SEG_RULE,ACC_TYPE,VALUE_DATE) " & _
                  "select i.AC_ENTRY_SR_NO,i.AC_BRANCH, " & _
                  "i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, FCY_AMOUNT, " & _
                  "a.CURRENT_BALANCE_ACC_LCY,i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, " & _
                  "i.[USER_ID], i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, " & _
                  "i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE, " & _
                  "'ALICO' 'SEG_RULE' , at.GO_ACC_TYPE_CODE, i.VALUE_DATE  " & _
                  "from IMP_FLEX_TRANS i " & _
                  "left outer join CUST_BALANCE a on " & _
                  "i.ACCOUNT=a.ACCOUNT AND i.TXN_DATE=a.IMPORTED_DATE " & _
                  "left outer join FLEX_AC_CLASS c on " & _
                  "a.ACCOUNT_CLASS=c.AC_CLASS " & _
                  "left outer join GO_ACC_TYPE at on " & _
                  "a.BBK_DEFINITION=at.BBK_DEFINITION " & _
                  "where i.ACCOUNT='G010000200038002' " & _
                  "and i.TRN_CODE = '355' " & _
                  " and i.DRCR_IND ='C' " & _
                  "and i.TXN_DATE>=@P_TXN_DATE_FROM AND i.TXN_DATE<=@P_TXN_DATE_TO " & _
                  "and c.ACC_TYPE='Customer' "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(comm1, trans)





                '------ Delete Specific Govt Account -------

                strSql = "delete dbo.GO_FLEX_TRANS_BY_RULE where TXN_DATE >=@P_TXN_DATE_FROM and TXN_DATE <=@P_TXN_DATE_TO " & _
                    " and RELATED_CUSTOMER in (" & _
                    "'BD0200141','BD0200110','BD0200004','BD0200013','BD0200502','BD0200200'," & _
                    "'BD0100053','BD0200528','BD0200809','BD0200714','BD0200593','BD0200587'," & _
                    "'BD0200676','BD0200767','BD0200205','BD0200873','BD0200723','BD0200062'," & _
                    "'BD0200738','BD0200838','BD0100058','BD0200691','BD0200702','BD0200735'," & _
                    "'BD0200742','BD0200772')" & _
                    "and DRCR_IND='C'"

                Dim comm2 As DbCommand
                comm2 = db.GetSqlStringCommand(strSql)

                comm2.Parameters.Clear()

                db.AddInParameter(comm2, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm2, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                comm2.CommandTimeout = 1800

                db.ExecuteNonQuery(comm2, trans)


                'Dim comm2 As DbCommand
                'comm2 = db.GetSqlStringCommand(strSql)

                'comm2.Parameters.Clear()

                'comm2.CommandTimeout = 1800

                'db.ExecuteNonQuery(comm2, trans)

                '-------------------------------------------------------------




                ' Transaction code convertion for BBK 

                strSql = "update GO_FLEX_TRANS_BY_RULE set BBK_FUNDS_TYPE='A' " & _
                   " where DRCR_IND='C' " & _
                   "and TXN_DATE>=@P_TXN_DATE_FROM AND TXN_DATE<=@P_TXN_DATE_TO "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(comm1, trans)

                strSql = "update GO_FLEX_TRANS_BY_RULE set BBK_FUNDS_TYPE='W' " & _
                   " where DRCR_IND='D' " & _
                   "and TXN_DATE>=@P_TXN_DATE_FROM AND TXN_DATE<=@P_TXN_DATE_TO "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(comm1, trans)

                '------------ update   ---------------

                'strSql = "update STATUS_IMP_FLEX_TRANS set STATUS = 'P' " & _
                '    " where month(IMPORTED_DATE)=" & txtMonth.Text & " and year(IMPORTED_DATE)=" & txtYear.Text

                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                strSql = "update STATUS_IMP_FLEX_TRANS set GOSTATUS='P' " & _
                   "WHERE IMPORTED_DATE>=@P_IMP_DATE_FROM AND IMPORTED_DATE<=@P_IMP_DATE_TO "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_IMP_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_IMP_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(comm1, trans)


                strSql = "update STATUS_IMP_FLEX_CUST set STATUS='P' " & _
                  "WHERE IMPORTED_DATE>=@P_IMP_DATE_FROM AND IMPORTED_DATE<=@P_IMP_DATE_TO "

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_IMP_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_IMP_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(comm1, trans)





                trans.Commit()
                log_message = " Imported : Flex File By Rule (goAML) "
                Logger.system_log(log_message)

                MessageBox.Show("Process Completed Successfully", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End Using






        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub



    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click

        ProgressBar1.Style = ProgressBarStyle.Marquee

        btnProcess.Enabled = False

        BackgroundWorker1.RunWorkerAsync()


        'If CheckProcess() = True Then
        '    ProcessTransaction()

        'End If


    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub FrmProcessImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        If CheckProcess() = True Then
            ProcessTransaction()

        End If

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted


        ProgressBar1.Style = ProgressBarStyle.Continuous

        btnProcess.Enabled = True


        

    End Sub
End Class