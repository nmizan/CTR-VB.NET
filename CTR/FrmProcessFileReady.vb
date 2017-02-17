Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common
Imports System.Threading

Public Class FrmProcessFileReady
    Dim _formName As String = "ToolsProcessFileReady"
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

                commProc = db.GetStoredProcCommand("Process_FIU_Month_Trans")

                commProc.Parameters.Clear()
                db.AddInParameter(commProc, "@P_Month", DbType.Int16, txtMonth.Text)
                db.AddInParameter(commProc, "@P_Year", DbType.Int16, txtYear.Text)
                commProc.CommandTimeout = 1200

                Dim result As Integer
                result = db.ExecuteNonQuery(commProc)


                'If result < 0 Then
                '    MessageBox.Show("File Transfer Process Error ", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    trans.Rollback()
                '    Exit Sub
                'Else

                'End If

                'strSql = "insert into PROCESS_STATUS() values where PROC_MONTH=" & txtYear.Text & " and PROC_YEAR=" & txtYear.Text
                

                trans.Commit()

                log_message = " Ready For The File Generation process Done "
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
        MessageBox.Show("! Process Completed !" & vbCrLf & "You can Export CTR Files for BBK", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

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