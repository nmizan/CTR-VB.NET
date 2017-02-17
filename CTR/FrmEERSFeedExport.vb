'
'Author             : Fahad Khan
'Purpose            : EERS Feed Export
'Creation date      : 05-Aug-2013  
'

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.IO.File
Imports System.IO.FileSystemInfo
Imports System.IO
Imports Microsoft.VisualBasic


Public Class FrmEERSFeedExport

    Dim _formName As String = "SystemEERSFeedExport"
    Dim opt As SecForm = New SecForm(_formName, CommonAppSet.IsAdmin)

    Private _Seq_No As Integer
    Private _Batch_No As String = ""
    Private _Ref_No As String = ""
    Dim _FileNm As String = ""
    Dim _ProcessSuccess As Boolean = False
    Dim _FileSuccess As Boolean = False
    Dim _PathEERS = Environment.CurrentDirectory + "\EERSUpFeed" '
    Dim _strFileName As String = ""

    Dim log_message As String = ""



#Region "User defined Procedures"



    Private Sub FlexFileGen()

        Dim owrite As System.IO.StreamWriter

        Dim exptext As String

        _FileNm = "EERS" & CommonAppSet.AppId & ".txt" '


        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr) '

            Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_Users_GetEERSDetail") '

            Dim dt2 As DataTable = db.ExecuteDataSet(commProc).Tables(0) '

            If dt2.Rows.Count = 0 Then '

                MessageBox.Show("No record found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub

            End If


            If Not Directory.Exists(_PathEERS) Then
                Directory.CreateDirectory(_PathEERS)
            End If

            owrite = New System.IO.StreamWriter(_PathEERS + "\" + _FileNm, False)


            For row = 0 To dt2.Rows.Count - 1

                'dt2.Rows(row)(5)

                exptext = CommonAppSet.AppId + vbTab + vbTab + vbTab + dt2.Rows(row)("USERS_ID").ToString()
                exptext = exptext + vbTab + vbTab + vbTab + dt2.Rows(row)("FG_ID").ToString()
                exptext = exptext + vbTab + dt2.Rows(row)("FG_NAME").ToString()
                exptext = exptext + vbTab + dt2.Rows(row)("USERS_ID").ToString()
                exptext = exptext + vbTab + vbTab + vbTab


                owrite.WriteLine(exptext)


            Next

            log_message = "Export EERS Feed File Successfully"
            Logger.system_log(log_message)

            owrite.Close()

            _FileSuccess = True

            


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            If Not (owrite Is Nothing) Then
                owrite.Close()
            End If

        End Try


        '''''''''''''''''''''''''''




    End Sub



#End Region




    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Me.Close()
    End Sub










    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        '_ProcessSuccess = False
        _FileSuccess = False

        'btnViewReversalData.Enabled = False

        'PrepareReversalData()

        FlexFileGen()


    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        ProgressBar1.Style = ProgressBarStyle.Continuous
        btnProcess.Enabled = True
        btnViewReport.Enabled = True

        If _FileSuccess = True Then
            MessageBox.Show("! Process Completed !" + _
                            Environment.NewLine + "EERS Feed ready!!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Process.Start("explorer.exe", "/select," & _PathEERS & "\" & _FileNm)

        End If



    End Sub








    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click

        btnProcess.Enabled = False
        btnViewReport.Enabled = False

        ProgressBar1.Style = ProgressBarStyle.Marquee

        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub btnViewDDBreakupReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewReport.Click

        Dim rd As New crUsersRole()

        Dim frmRptViewer As New frmReportView()

        frmRptViewer.SetReport(rd)

        frmRptViewer.Show()


    End Sub



    Private Sub FrmEERSFeedExport_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If BackgroundWorker1.IsBusy = True Then
            MessageBox.Show("Process is running.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            e.Cancel = True
        End If
    End Sub

    Private Sub FrmEERSFeedExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized !!", "Unauthorize Access", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub
End Class