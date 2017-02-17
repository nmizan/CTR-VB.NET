Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common
Imports System.Threading

Public Class FrmProcessFileReadyGo
    Dim _formName As String = "ToolsProcessFileReadyGoAML"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click

        ProgressBar1.Style = ProgressBarStyle.Marquee

        BackgroundWorker1.RunWorkerAsync()



        'Exit Sub

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Me.Close()

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim strSql As String = ""

        Try

            'ProgressBar1.Style = ProgressBarStyle.Marquee
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()


                Dim commProc As DbCommand

                commProc = db.GetStoredProcCommand("GO_Process_Trans")

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(commProc, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                commProc.CommandTimeout = 1800

                Dim result As Integer
                result = db.ExecuteNonQuery(commProc)


                'If result < 0 Then
                '    MessageBox.Show("File Transfer Process Error ", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    trans.Rollback()
                '    Exit Sub
                'Else

                'End If

                'strSql = "insert into PROCESS_STATUS() values where PROC_MONTH=" & txtYear.Text & " and PROC_YEAR=" & txtYear.Text

                strSql = "UPDATE GO_BEARER_INFO  SET REFERENCE_NUMBER = t.AC_ENTRY_SR_NO  " & _
               "FROM " & _
               "(" & _
                  "Select a.AC_ENTRY_SR_NO ,a.REFERENCE, a.TXN_DATE" & _
                  " From  GO_FLEX_TRANS_BY_RULE a" & _
               ") t  " & _
               "WHERE GO_BEARER_INFO.REFERENCE_NUMBER = t.REFERENCE"

                Dim comm1 As DbCommand

                comm1 = db.GetSqlStringCommand(strSql)

                comm1.Parameters.Clear()

                db.AddInParameter(comm1, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(comm1, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                db.ExecuteNonQuery(comm1, trans)

                trans.Commit()

                log_message = " Ready For The File Generation process Done(goAML) "
                Logger.system_log(log_message)

                'ProgressBar1.Style = ProgressBarStyle.Continuous
                'Exit Sub
                'MessageBox.Show("! Process Completed !" & vbCrLf & "You can Export CTR Files for BBK", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End Using



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'ProgressBar1.Style = ProgressBarStyle.Continuous
            'BackgroundWorker1.CancelAsync()


        End Try

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        'BackgroundWorker1.CancelAsync()
        ProgressBar1.Style = ProgressBarStyle.Continuous
        MessageBox.Show("! Process Completed !" & vbCrLf & "You can Export goAML Files for BBK", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub FrmProcessFileReady_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If BackgroundWorker1.IsBusy = True Then
            MessageBox.Show("Process is running.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            e.Cancel = True
        End If

    End Sub

    Private Sub FrmProcessFileReady_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub
End Class