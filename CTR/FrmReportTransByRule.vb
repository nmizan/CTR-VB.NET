﻿'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared

Imports Microsoft.Office.Interop

Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Public Class FrmReportTransByRule



#Region "User defined code"

    Dim _formName As String = "ReportTransactionByRule"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String
    Private Sub ExportToXl()

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            strSql = "select AC_BRANCH, BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, USER_ID, AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, TXN_COUNT, SEG_RULE, BBK_TXN_TYPE " & _
            "        from FLEX_TRANS_BY_RULE " & _
            "        where Month(TXN_DATE) = " & txtMonth.Text & " And Year(TXN_DATE) = " & txtYear.Text


            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            '--------------


            Dim xlApp As New Excel.Application

            Dim wb As Excel.Workbook = xlApp.Workbooks.Add

            Dim sheet As Excel.Worksheet = wb.Worksheets.Add

            sheet.Name = "File Import Status"

            Dim i, j As Integer


            For j = 0 To ds.Tables(0).Columns.Count - 1
                sheet.Cells(1, j + 1) = ds.Tables(0).Columns(j).ColumnName



            Next j


            For i = 0 To ds.Tables(0).Rows.Count - 1
                For j = 0 To ds.Tables(0).Columns.Count - 1
                    sheet.Cells(i + 2, j + 1) = ds.Tables(0).Rows(i)(j).ToString()
                    If BackgroundWorker1.CancellationPending = True Then
                        GoTo 10
                    End If
                Next j
            Next i
10:

            xlApp.Visible = True
            wb.Activate()
            log_message = " Report Transaction By Rule "
            Logger.system_log(log_message)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ExportToXlFaster()

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            strSql = "select AC_BRANCH, BATCH_NO, ACCOUNT, ACCOUNT_CURRRENCY, LCY_AMOUNT, TRN_CODE, REFERENCE, DRCR_IND, MODULE, USER_ID, AUTH_ID, RELATED_CUSTOMER, HOST_REFERENCE, TRANSACTION_DESCRIPTION, APPLICATION_ID, TXN_BRANCH, TXN_DATE, TXN_COUNT, SEG_RULE, BBK_TXN_TYPE " & _
            "        from FLEX_TRANS_BY_RULE " & _
            "        where Month(TXN_DATE) = " & txtMonth.Text & " And Year(TXN_DATE) = " & txtYear.Text


            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

            Dim dt As DataTable = db.ExecuteDataSet(dbCommand).Tables(0)

            '--------------

            ' Copy the DataTable to an object array
            Dim rawData(dt.Rows.Count, dt.Columns.Count - 1) As Object

            ' Copy the column names to the first row of the object array
            For col = 0 To dt.Columns.Count - 1
                rawData(0, col) = dt.Columns(col).ColumnName
            Next

            ' Copy the values to the object array
            For col = 0 To dt.Columns.Count - 1
                For row = 0 To dt.Rows.Count - 1
                    rawData(row + 1, col) = dt.Rows(row).ItemArray(col)
                Next
            Next


            ' Calculate the final column letter
            Dim finalColLetter As String = String.Empty
            Dim colCharset As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            Dim colCharsetLen As Integer = colCharset.Length

            If dt.Columns.Count > colCharsetLen Then
                finalColLetter = colCharset.Substring( _
                 (dt.Columns.Count - 1) \ colCharsetLen - 1, 1)
            End If

            finalColLetter += colCharset.Substring( _
              (dt.Columns.Count - 1) Mod colCharsetLen, 1)

            Dim xlApp As New Excel.Application

            Dim wb As Excel.Workbook = xlApp.Workbooks.Add

            Dim sheet As Excel.Worksheet = wb.Worksheets.Add

            sheet.Name = "File Import Status"

            Dim excelRange As String = String.Format("A1:{0}{1}", finalColLetter, dt.Rows.Count + 1)

            sheet.Range(excelRange, Type.Missing).Value2 = rawData

            CType(sheet.Rows(1, Type.Missing), Excel.Range).Font.Bold = True

            xlApp.Visible = True
            wb.Activate()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region



    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub


    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        'Dim frmReportViewer As New FrmReportViewer

        'Dim rd As New crMismatchFlexAc

        'rd.SetParameterValue("paramMonth", txtMonth.Text)
        'rd.SetParameterValue("paramYear", txtYear.Text)


        'frmReportViewer.SetReport(rd)

        'frmReportViewer.ShowDialog()
        If btnShow.Text = "Show Transactions" Then
            btnShow.Text = "Cancel"
            ProgressBar1.Style = ProgressBarStyle.Marquee

            BackgroundWorker1.RunWorkerAsync()
        Else
            BackgroundWorker1.CancelAsync()

        End If






    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'ExportToXl()
        ExportToXlFaster()

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ProgressBar1.Style = ProgressBarStyle.Continuous
        btnShow.Text = "Show Transactions"
    End Sub

    Private Sub FrmReportTransByRule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub
End Class