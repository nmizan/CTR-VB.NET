'
'Author             : Iftekharul Alam Khan Lodi
'Purpose            : Process Imported Flex File by Rule
'Creation date      : 
'
' -- Modification Histroy
'Modification date  : 25-Sep-2013
'Modified By        : Iftekharul Alam Khan Lodi
'
'

Imports System.IO
Imports System.Globalization
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common


Public Class FrmProcessImport
    Dim log_message As String
    Dim _formName As String = "ToolsProcessImport"
    Dim opt As SecForm = New SecForm(_formName)
    Dim _errLevel As String = "Level 0"

    Private Function CheckProcess() As Boolean

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dt As New DataTable
            Dim strSql As String = ""

            ' ------------------- update 2012-07-26 ---------------------

            'strSql = "select count(STATUS) cnt from STATUS_IMP_FLEX_TRANS " & _
            '    " where STATUS = 'N' and month(IMPORTED_DATE)=" & txtMonth.Text & " and year(IMPORTED_DATE)=" & txtYear.Text

            _errLevel = "Level 1"

            strSql = "select count(STATUS) cnt from STATUS_IMP_FLEX_TRANS " & _
                " where month(IMPORTED_DATE)= '" & txtMonth.Text & "' and year(IMPORTED_DATE)= '" & txtYear.Text & "' "
            ' -----------------------------------------------------------

            dt = db.ExecuteDataSet(CommandType.Text, strSql).Tables(0)

            If dt.Rows.Count > 0 Then
                If dt.Rows(0)(0) = 0 Then
                    MessageBox.Show("No unprocessed transaction available for inputed month." & vbCrLf & " ! Proces failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Else
                MessageBox.Show("File import status record error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If


            ' Insert Delete From Hist

            ' ------------------- update 2015-01-03 ---------------------

            _errLevel = "Level 2"

            Dim commProc1 As DbCommand = db.GetStoredProcCommand("CTR_IMP_FLEX_TRANS_HIST")

            commProc1.Parameters.Clear()

            db.AddInParameter(commProc1, "@P_Month", DbType.Int32, txtMonth.Text.Trim())
            db.AddInParameter(commProc1, "@P_Year", DbType.Int32, txtYear.Text.Trim())

            db.AddParameter(commProc1, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

            commProc1.CommandTimeout = 3600



            Dim result As Integer

            db.ExecuteNonQuery(commProc1)
            result = db.GetParameterValue(commProc1, "@PROC_RET_VAL")

            If result = 0 Then

            ElseIf result = 1 Then

                MessageBox.Show("transaction Error" & vbCrLf & " Can't Insert Transaction ", "History Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False

            End If


            _errLevel = "Level 3"



            ' ------------------- update 2012-07-30 ---------------------

            'strSql = "select count(distinct i.ACCOUNT) cntAcc from (select ACCOUNT,TXN_DATE from IMP_FLEX_TRANS " & _
            '    " where len(account)=16 and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)= " & txtYear.Text & " ) i " & _
            '    " left outer join FLEX_ACCOUNT f " & _
            '    " on i.ACCOUNT=f.ACCOUNT where f.ACCOUNT is null"

            ' ------------------- update 25-Sep-2013 ---------------------

            'strSql = "select count(distinct i.ACCOUNT) cntAcc from (select ACCOUNT,TXN_DATE from IMP_FLEX_TRANS " & _
            '    " where month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)= " & txtYear.Text & " ) i " & _
            '    " left outer join FLEX_ACCOUNT f " & _
            '    " on i.ACCOUNT=f.ACCOUNT where f.ACCOUNT is null"

            strSql = "select count(distinct i.ACCOUNT) cntAcc from (select ACCOUNT,TXN_DATE from IMP_FLEX_TRANS " & _
                " where len(account)=16 and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)= " & txtYear.Text & " ) i " & _
                " left outer join FLEX_ACCOUNT f " & _
                " on i.ACCOUNT=f.ACCOUNT where f.ACCOUNT is null"

            ' -----------------------------------------------------------

            ' ------------------- update 2012-07-26 ---------------------
            Dim commProc As DbCommand

            commProc = db.GetSqlStringCommand(strSql)

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

            MessageBox.Show(_errLevel + Environment.NewLine + _
                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

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

            strSql = "delete dbo.FLEX_TRANS_BY_RULE where month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

            Dim comm1 As DbCommand

            comm1 = db.GetSqlStringCommand(strSql)

            comm1.CommandTimeout = 1800

            db.ExecuteNonQuery(comm1)

            ' -----------------------------------------------------------

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                '------------ Rule: Flexcube Teller ----------------

                '-- edited 17-Nov-13

                strSql = "insert into dbo.FLEX_TRANS_BY_RULE(AC_BRANCH, BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, USER_ID, AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, TXN_COUNT, SEG_RULE, BBK_TXN_TYPE) " & _
                    " select i.AC_BRANCH, i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, i.USER_ID, i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE,1 'TXN_COUNT','Flexcube Teller' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                    " from IMP_FLEX_TRANS i " & _
                    " left outer join FLEX_ACCOUNT a on " & _
                    " i.ACCOUNT=a.ACCOUNT " & _
                    " left outer join FLEX_AC_CLASS c on " & _
                    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                    " where i.ACCOUNT<>'195100501' " & _
                    " and i.ACCOUNT<>'100200101' " & _
                    " and i.TRN_CODE in ('002','003','004','005') " & _
                    " and i.APPLICATION_ID ='CTELLR' " & _
                    " and i.MODULE='DE' " & _
                    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                    " and c.ACC_TYPE='Customer' "

                '--- modification for BRD version 0.3 (2012-05-08) -----

                'strSql = "insert into dbo.FLEX_TRANS_BY_RULE " & _
                '    " select i.*,1 'TXN_COUNT','Flexcube Teller' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                '    " from IMP_FLEX_TRANS i " & _
                '    " left outer join FLEX_ACCOUNT a on " & _
                '    " i.ACCOUNT=a.ACCOUNT " & _
                '    " left outer join FLEX_AC_CLASS c on " & _
                '    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    " where i.ACCOUNT<>'195100501' " & _
                '    " and i.ACCOUNT<>'100200101' " & _
                '    " and i.TRN_CODE in ('002','003','004','005') " & _
                '    " and i.APPLICATION_ID ='CTELLR' " & _
                '    " and i.MODULE='DE' " & _
                '    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                '    " and c.ACC_TYPE='Customer' "




                'strSql = "insert into dbo.FLEX_TRANS_BY_RULE " & _
                '    " select i.*,1 'TXN_COUNT','Flexcube Teller' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                '    " from IMP_FLEX_TRANS i " & _
                '    " left outer join FLEX_ACCOUNT a on " & _
                '    " i.ACCOUNT=a.ACCOUNT " & _
                '    " left outer join FLEX_AC_CLASS c on " & _
                '    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    " where i.ACCOUNT_CURRRENCY='BDT' " & _
                '    " and i.ACCOUNT<>'195100501' " & _
                '    " and i.ACCOUNT<>'100200101' " & _
                '    " and i.TRN_CODE in ('002','003','004','005') " & _
                '    " and i.APPLICATION_ID ='CTELLR' " & _
                '    " and i.MODULE='DE' " & _
                '    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                '    " and c.ACC_TYPE='Customer' "

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                '------------ Rule: ZRAC  ---------------
                '-- edited 17-Nov-13

                strSql = "insert into dbo.FLEX_TRANS_BY_RULE (AC_BRANCH, BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, USER_ID, AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, TXN_COUNT, SEG_RULE, BBK_TXN_TYPE) " & _
                    " select i.AC_BRANCH, i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, i.USER_ID, i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE,1 'TXN_COUNT','ZRAC' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                    " from IMP_FLEX_TRANS i " & _
                    " left outer join FLEX_ACCOUNT a on " & _
                    " i.ACCOUNT=a.ACCOUNT " & _
                    " left outer join FLEX_AC_CLASS c on " & _
                    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                    " where i.ACCOUNT<>'195100501' " & _
                    " and i.ACCOUNT<>'100200101' " & _
                    " and i.TRN_CODE = '002' " & _
                    " and i.DRCR_IND ='C' " & _
                    " and i.REFERENCE LIKE 'G0%ZRAC%'" & _
                    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                    " and c.ACC_TYPE='Customer' "


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


                'strSql = "insert into dbo.FLEX_TRANS_BY_RULE " & _
                '    " select i.*,1 'TXN_COUNT','ZRAC' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                '    " from IMP_FLEX_TRANS i " & _
                '    " left outer join FLEX_ACCOUNT a on " & _
                '    " i.ACCOUNT=a.ACCOUNT " & _
                '    " left outer join FLEX_AC_CLASS c on " & _
                '    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    " where i.ACCOUNT_CURRRENCY='BDT' " & _
                '    " and i.ACCOUNT<>'195100501' " & _
                '    " and i.ACCOUNT<>'100200101' " & _
                '    " and i.TRN_CODE = '002' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    " and i.REFERENCE LIKE 'G0%ZRAC%'" & _
                '    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                '    " and c.ACC_TYPE='Customer' "

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                '------------ Rule: non-ALICO (Non Consolidated Format) ---------------

                '-- edited 17-Nov-13

                strSql = "insert into dbo.FLEX_TRANS_BY_RULE (AC_BRANCH, BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, USER_ID, AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, TXN_COUNT, SEG_RULE, BBK_TXN_TYPE) " & _
                    " select i.AC_BRANCH, i.BATCH_NO, i.ACCOUNT, i.ACCOUNT_CURRRENCY, i.LCY_AMOUNT, i.TRN_CODE, i.REFERENCE, i.DRCR_IND, i.MODULE, i.USER_ID, i.AUTH_ID, i.RELATED_CUSTOMER, i.HOST_REFERENCE, i.TRANSACTION_DESCRIPTION, i.APPLICATION_ID, i.TXN_BRANCH, i.TXN_DATE,1 'TXN_COUNT','non-ALICO' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                    " from IMP_FLEX_TRANS i " & _
                    " left outer join FLEX_ACCOUNT a on " & _
                    " i.ACCOUNT=a.ACCOUNT " & _
                    " left outer join FLEX_AC_CLASS c on " & _
                    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                    " where i.ACCOUNT<>'195100501' " & _
                    " and i.ACCOUNT<>'G010000200038002' " & _
                    " and i.TRN_CODE = '355' " & _
                    " and i.DRCR_IND ='C' " & _
                    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                    " and c.ACC_TYPE='Customer' "


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
                '    " and i.BATCH_NO = '5602'" & _
                '    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                '    " and c.ACC_TYPE='Customer' "

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                '------------ Rule: ALICO  (Consolidated Format)---------------


                strSql = " select i.*,1 'TXN_COUNT','ALICO' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                    " from IMP_FLEX_TRANS i " & _
                    " left outer join FLEX_ACCOUNT a on " & _
                    " i.ACCOUNT=a.ACCOUNT " & _
                    " left outer join FLEX_AC_CLASS c on " & _
                    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                    " where i.ACCOUNT='G010000200038002' " & _
                    " and i.TRN_CODE = '355' " & _
                    " and i.DRCR_IND ='C' " & _
                    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                    " and c.ACC_TYPE='Customer' "


                'strSql = " select i.*,1 'TXN_COUNT','ALICO' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                '    " from IMP_FLEX_TRANS i " & _
                '    " left outer join FLEX_ACCOUNT a on " & _
                '    " i.ACCOUNT=a.ACCOUNT " & _
                '    " left outer join FLEX_AC_CLASS c on " & _
                '    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    " where i.ACCOUNT='G010000200038002' " & _
                '    " and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    " and i.BATCH_NO = '5602'" & _
                '    " and month(i.TXN_DATE)=" & txtMonth.Text & " and year(i.TXN_DATE)=" & txtYear.Text & _
                '    " and c.ACC_TYPE='Customer' "

                dt = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0)

                Dim intPos As Integer = -1
                Dim strTemp As String = ""
                Dim strCount As String = ""
                Dim TxnCount As Integer = 1
                Dim inc As Integer = 0
                Dim commProc As DbCommand

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

                        strSql = "insert into dbo.FLEX_TRANS_BY_RULE(AC_BRANCH, BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, USER_ID, AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, TXN_COUNT, SEG_RULE) values(@P_AC_BRANCH, @P_BATCH_NO, @P_ACCOUNT, @P_ACCOUNT_CURRRENCY, @P_LCY_AMOUNT, @P_TRN_CODE, @P_REFERENCE, @P_DRCR_IND, @P_MODULE, @P_USER_ID, @P_AUTH_ID, @P_RELATED_CUSTOMER, @P_HOST_REFERENCE, @P_TRANSACTION_DESCRIPTION, @P_APPLICATION_ID, @P_TXN_BRANCH, @P_TXN_DATE, @P_TXN_COUNT, @P_SEG_RULE)"



                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        db.AddInParameter(commProc, "@P_AC_BRANCH", DbType.String, dt.Rows(i)("AC_BRANCH"))
                        db.AddInParameter(commProc, "@P_BATCH_NO", DbType.String, dt.Rows(i)("BATCH_NO"))
                        db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, dt.Rows(i)("ACCOUNT"))
                        db.AddInParameter(commProc, "@P_ACCOUNT_CURRRENCY", DbType.String, dt.Rows(i)("ACCOUNT_CURRRENCY"))
                        db.AddInParameter(commProc, "@P_LCY_AMOUNT", DbType.String, dt.Rows(i)("LCY_AMOUNT"))
                        db.AddInParameter(commProc, "@P_TRN_CODE", DbType.String, dt.Rows(i)("TRN_CODE"))
                        db.AddInParameter(commProc, "@P_REFERENCE", DbType.String, dt.Rows(i)("REFERENCE"))
                        db.AddInParameter(commProc, "@P_DRCR_IND", DbType.String, dt.Rows(i)("DRCR_IND"))
                        db.AddInParameter(commProc, "@P_MODULE", DbType.String, dt.Rows(i)("MODULE"))
                        db.AddInParameter(commProc, "@P_USER_ID", DbType.String, dt.Rows(i)("USER_ID"))
                        db.AddInParameter(commProc, "@P_AUTH_ID", DbType.String, dt.Rows(i)("AUTH_ID"))
                        db.AddInParameter(commProc, "@P_RELATED_CUSTOMER", DbType.String, dt.Rows(i)("RELATED_CUSTOMER"))
                        db.AddInParameter(commProc, "@P_HOST_REFERENCE", DbType.String, dt.Rows(i)("HOST_REFERENCE"))
                        db.AddInParameter(commProc, "@P_TRANSACTION_DESCRIPTION", DbType.String, dt.Rows(i)("TRANSACTION_DESCRIPTION"))
                        db.AddInParameter(commProc, "@P_APPLICATION_ID", DbType.String, dt.Rows(i)("APPLICATION_ID"))
                        db.AddInParameter(commProc, "@P_TXN_BRANCH", DbType.String, dt.Rows(i)("REFERENCE").ToString().Trim().Substring(0, 3))
                        db.AddInParameter(commProc, "@P_TXN_DATE", DbType.Date, dt.Rows(i)("TXN_DATE"))
                        db.AddInParameter(commProc, "@P_TXN_COUNT", DbType.Int64, TxnCount)
                        db.AddInParameter(commProc, "@P_SEG_RULE", DbType.String, "ALICO")


                        Dim result As Integer
                        result = db.ExecuteNonQuery(commProc, trans)
                        If result < 0 Then
                            MessageBox.Show("Process for ALICO error ", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            trans.Rollback()
                            Exit Sub
                        Else
                        End If

                        '---------------

                    Next

                End If



                'strSql = "insert into dbo.FLEX_TRANS_BY_RULE " & _
                '    " select i.*,1 'TXN_COUNT','ALICO' 'SEG_RULE' ,null 'BBK_TXN_TYPE' " & _
                '    " from IMP_FLEX_TRANS i " & _
                '    " left outer join FLEX_ACCOUNT a on " & _
                '    " i.ACCOUNT=a.ACCOUNT " & _
                '    " left outer join FLEX_AC_CLASS c on " & _
                '    " a.ACCOUNT_CLASS=c.AC_CLASS " & _
                '    " where i.ACCOUNT='G010000200038002' " & _
                '    " and i.TRN_CODE = '355' " & _
                '    " and i.DRCR_IND ='C' " & _
                '    " and i.BATCH_NO = '5602'" '& _
                ''" and c.ACC_TYPE='Customer' "

                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)



                '------ modification for BRD version 0.3 (2012-07-31) -------

                strSql = "delete dbo.FLEX_TRANS_BY_RULE where month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text & _
                    "and RELATED_CUSTOMER in (" & _
                    "'BD0200141','BD0200110','BD0200004','BD0200013','BD0200502','BD0200200'," & _
                    "'BD0100053','BD0200528','BD0200809','BD0200714','BD0200593','BD0200587'," & _
                    "'BD0200676','BD0200767','BD0200205','BD0200873','BD0200723','BD0200062'," & _
                    "'BD0200738','BD0200838','BD0100058','BD0200691','BD0200702','BD0200735'," & _
                    "'BD0200742','BD0200772')" & _
                    "and DRCR_IND='C'"

                Dim comm2 As DbCommand

                comm2 = db.GetSqlStringCommand(strSql)

                comm2.CommandTimeout = 1800

                db.ExecuteNonQuery(comm2, trans)

                '-------------------------------------------------------------



                ' Transaction code convertion for BBK 

                '------------ Rule: 1  ---------------
                '--- modification for BRD version 0.3 (2012-05-08) -----

                strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='01' " & _
                    " where TRN_CODE='002' and DRCR_IND='C' " & _
                    " and AC_BRANCH=TXN_BRANCH " & _
                    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='01' " & _
                '    " where TRN_CODE='002' and DRCR_IND='C' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and AC_BRANCH=TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                '------------ Rule: 2  ---------------
                '--- modification for BRD version 0.3 (2012-05-08) -----

                strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='02' " & _
                    " where TRN_CODE='003' and DRCR_IND='D' " & _
                    " and AC_BRANCH=TXN_BRANCH " & _
                    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='02' " & _
                '    " where TRN_CODE='003' and DRCR_IND='D' and ACCOUNT_CURRRENCY='BDT'" & _
                '    " and AC_BRANCH=TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                '------------ Rule: 3  ---------------
                '--- modification for BRD version 0.3 (2012-05-08) -----

                strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='05' " & _
                    " where TRN_CODE='004' and DRCR_IND='C' " & _
                    " and AC_BRANCH=TXN_BRANCH " & _
                    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='05' " & _
                '    " where TRN_CODE='004' and DRCR_IND='C' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and AC_BRANCH=TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                '------------ Rule: 4  ---------------
                '--- modification for BRD version 0.3 (2012-05-08) -----

                strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='06' " & _
                    " where TRN_CODE='005' and DRCR_IND='D' " & _
                    " and AC_BRANCH=TXN_BRANCH " & _
                    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='06' " & _
                '    " where TRN_CODE='005' and DRCR_IND='D' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and AC_BRANCH=TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                '------------ Rule: 5  ---------------
                '--- modification for BRD version 0.3 (2012-05-08) -----

                strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='04' " & _
                    " where TRN_CODE='002' and DRCR_IND='C' " & _
                    " and AC_BRANCH<>TXN_BRANCH " & _
                    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='04' " & _
                '    " where TRN_CODE='002' and DRCR_IND='C' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and AC_BRANCH<>TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                '------------ Rule: 6  ---------------
                '--- modification for BRD version 0.3 (2012-05-08) -----

                strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='18' " & _
                    " where TRN_CODE='003' and DRCR_IND='D' " & _
                    " and AC_BRANCH<>TXN_BRANCH " & _
                    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text


                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='18' " & _
                '    " where TRN_CODE='003' and DRCR_IND='D' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and AC_BRANCH<>TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                '------------ Rule: 7  ---------------
                '--- modification for BRD version 0.3 (2012-05-08) -----

                strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='08' " & _
                    " where TRN_CODE='004' and DRCR_IND='C' " & _
                    " and AC_BRANCH<>TXN_BRANCH " & _
                    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text


                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='08' " & _
                '    " where TRN_CODE='004' and DRCR_IND='C' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and AC_BRANCH<>TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                '------------ Rule: 8  ---------------
                '--- modification for BRD version 0.3 (2012-05-08) -----

                strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='19' " & _
                    " where TRN_CODE='005' and DRCR_IND='D' " & _
                    " and AC_BRANCH<>TXN_BRANCH " & _
                    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='19' " & _
                '    " where TRN_CODE='005' and DRCR_IND='D' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and AC_BRANCH<>TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                '------------ Rule: for alico and non alico of trncode=355  ---------------
                '--- modification for BRD version 0.3 (2012-05-08) -----

                strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='03' " & _
                    " where TRN_CODE='355' " & _
                    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='01' " & _
                '    " where TRN_CODE='355' " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                '------------ update   ---------------
                strSql = "update STATUS_IMP_FLEX_TRANS set STATUS = 'P' " & _
                    " where month(IMPORTED_DATE)=" & txtMonth.Text & " and year(IMPORTED_DATE)=" & txtYear.Text

                db.ExecuteNonQuery(trans, CommandType.Text, strSql)






                ''------------ Rule: 1  ---------------
                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='01' " & _
                '    " where TRN_CODE='002' and DRCR_IND='C' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and substring (REFERENCE,4,4)='MLAL' " & _
                '    " and AC_BRANCH=TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                ''------------ Rule: 2  ---------------
                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='02' " & _
                '    " where TRN_CODE='003' and DRCR_IND='D' and ACCOUNT_CURRRENCY='BDT'" & _
                '    " and substring (REFERENCE,4,4)='ALML' " & _
                '    " and AC_BRANCH=TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                ''------------ Rule: 3  ---------------
                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='05' " & _
                '    " where TRN_CODE='004' and DRCR_IND='C' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and substring (REFERENCE,4,4)='MFAL' " & _
                '    " and AC_BRANCH=TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                ''------------ Rule: 4  ---------------
                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='06' " & _
                '    " where TRN_CODE='005' and DRCR_IND='D' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and substring (REFERENCE,4,4)='ALMF' " & _
                '    " and AC_BRANCH=TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)

                ''------------ Rule: 5  ---------------
                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='04' " & _
                '    " where TRN_CODE='002' and DRCR_IND='C' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and substring (REFERENCE,4,4)='MLAL' " & _
                '    " and AC_BRANCH<>TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                ''------------ Rule: 6  ---------------
                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='18' " & _
                '    " where TRN_CODE='003' and DRCR_IND='D' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and substring (REFERENCE,4,4)='ALML' " & _
                '    " and AC_BRANCH<>TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                ''------------ Rule: 7  ---------------
                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='08' " & _
                '    " where TRN_CODE='004' and DRCR_IND='C' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and substring (REFERENCE,4,4)='MFAL' " & _
                '    " and AC_BRANCH<>TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                'db.ExecuteNonQuery(trans, CommandType.Text, strSql)


                ''------------ Rule: 8  ---------------
                'strSql = "update FLEX_TRANS_BY_RULE set BBK_TXN_TYPE='19' " & _
                '    " where TRN_CODE='005' and DRCR_IND='D' and ACCOUNT_CURRRENCY='BDT' " & _
                '    " and substring (REFERENCE,4,4)='ALMF' " & _
                '    " and AC_BRANCH<>TXN_BRANCH " & _
                '    " and month(TXN_DATE)=" & txtMonth.Text & " and year(TXN_DATE)=" & txtYear.Text

                

                trans.Commit()
                log_message = "Imported Flex File By Rule"
                Logger.system_log(log_message)

                MessageBox.Show("Process Completed Successfully", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End Using






        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub


    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click

        If CheckProcess() = True Then
            ProcessTransaction()

        End If


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

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        Dim ProcessByRuleHist As New FrmProcessByRuleHistory()
        ProcessByRuleHist.ShowDialog()
    End Sub
End Class