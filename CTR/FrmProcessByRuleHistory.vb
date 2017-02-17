
'
'Author             : fahad Khan
'Purpose            : File Transfer Mian to History table
'Creation date      : 15-01-2015
'
Imports System.IO
Imports System.Globalization
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common

Public Class FrmProcessByRuleHistory

    Private Sub FrmProcessByRuleHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String = ""

        Dim dt As New DataTable



        If txtDateFrom.Text.Trim() = "/  /" Then
            MessageBox.Show("Date From required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return
        End If

        If txtDateTo.Text.Trim() = "/  /" Then
            MessageBox.Show("Date To required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return
        End If



        Try

            strSql = "  SELECT COUNT( ACCOUNT) CNTACC " & _
                     " FROM IMP_FLEX_TRANS WHERE TXN_DATE >= @TXN_DATE_FROM AND TXN_DATE <= @TXN_DATE_TO "

            Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

            commProc.Parameters.Clear()

            db.AddInParameter(commProc, "@TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
            db.AddInParameter(commProc, "@TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

            dt = db.ExecuteDataSet(commProc).Tables(0)

            If dt.Rows.Count > 0 Then

                lblTotRecNo.Text = dt.Rows(0)("CNTACC").ToString()

            End If




            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                strSql = " INSERT INTO IMP_FLEX_TRANS_HIST (AC_BRANCH, BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT," & _
                         " TRN_CODE, REFERENCE, DRCR_IND, MODULE, USER_ID, AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE,  " & _
                         " TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, AC_ENTRY_SR_NO, FCY_AMOUNT, VALUE_DATE)" & _
                         " SELECT AC_BRANCH, BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, " & _
                         " MODULE, USER_ID, AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, TRANSACTION_DESCRIPTION, APPLICATION_ID, " & _
                         " TXN_BRANCH, TXN_DATE, AC_ENTRY_SR_NO, FCY_AMOUNT, VALUE_DATE " & _
                         " FROM IMP_FLEX_TRANS WHERE TXN_DATE >= @TXN_DATE_FROM AND TXN_DATE <= @TXN_DATE_TO "

                Dim commProc1 As DbCommand = db.GetSqlStringCommand(strSql)

                commProc1.Parameters.Clear()

                db.AddInParameter(commProc1, "@TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(commProc1, "@TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(commProc1, trans)


                strSql = " DELETE FROM IMP_FLEX_TRANS WHERE TXN_DATE >= @TXN_DATE_FROM AND TXN_DATE <= @TXN_DATE_TO "



                commProc1 = db.GetSqlStringCommand(strSql)


                commProc1.Parameters.Clear()

                db.AddInParameter(commProc1, "@TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(commProc1, "@TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(commProc1, trans)


                strSql = " INSERT INTO CUST_BALANCE_HIST (IMPORTED_DATE, ACCOUNT, CURRENT_BALANCE_ACC_LCY," & _
                         " ACCOUNT_CLASS, BBK_DEFINITION)" & _
                         " SELECT  IMPORTED_DATE, ACCOUNT, CURRENT_BALANCE_ACC_LCY, ACCOUNT_CLASS, BBK_DEFINITION " & _
                         " FROM CUST_BALANCE WHERE IMPORTED_DATE >= @TXN_DATE_FROM AND IMPORTED_DATE <= @TXN_DATE_TO "


                commProc1 = db.GetSqlStringCommand(strSql)

                commProc1.Parameters.Clear()

                db.AddInParameter(commProc1, "@TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(commProc1, "@TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(commProc1, trans)



                strSql = " DELETE FROM CUST_BALANCE WHERE IMPORTED_DATE >= @TXN_DATE_FROM AND IMPORTED_DATE <= @TXN_DATE_TO "



                commProc1 = db.GetSqlStringCommand(strSql)

                commProc1.Parameters.Clear()

                db.AddInParameter(commProc1, "@TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(commProc1, "@TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(commProc1, trans)





                trans.Commit()

                MessageBox.Show("Data Transfered Successfully", "SuccessFull", MessageBoxButtons.OK, MessageBoxIcon.Information)




            End Using



        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try




    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class