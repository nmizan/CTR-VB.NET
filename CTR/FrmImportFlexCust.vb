Imports System.IO
Imports System.Globalization
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common
Imports CTR.Common



Public Class FrmImportFlexCust


    Dim _formName As String = "ToolsImportFlexCustomer"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""
    Dim dt As DataTable
    Dim dt2 As DataTable


    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        ofdImport.FileName = ""
        ofdImport.ShowDialog()
        If Not ofdImport.FileName.Trim() = "" Then
            txtFileName.Text = ofdImport.FileName
        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click



        Dim chrSep As Char() = {"~"}
        Dim strLine As String = ""
        Dim strArLine As String()
        Dim dtTrans As New DateTime
        Dim strSql As String = ""




        'Dim oFile As System.IO.File

        If txtFileName.Text.Trim = "" Then
            MessageBox.Show("No File")
            Exit Sub

        End If

        Dim oRead As System.IO.StreamReader

        Try
            oRead = File.OpenText(txtFileName.Text)


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                Dim trans As DbTransaction = conn.BeginTransaction()

                Dim commProc As DbCommand

                db.ExecuteNonQuery(trans, CommandType.Text, "delete FLEX_ACCOUNT_M")

                Dim SLNO As Integer = 0
                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()


                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        'dtTrans = DateTime.ParseExact(strArLine(4), "yyyyMMdd", New CultureInfo("en-US"))
                        If strArLine(0).Trim() = "HH" Then
                            dtTrans = DateTime.ParseExact(strArLine(4), "yyyyMMdd", New CultureInfo("en-US"))
                        End If


                        If strArLine(0).Trim() = "D" Then

                            SLNO = SLNO + 1

                            strSql = "insert into FLEX_ACCOUNT_M(ACCOUNT, ACCOUNT_CLASS, USER_NAME,SLNO) values(@P_ACCOUNT, @P_ACCOUNT_CLASS, @P_USER_NAME,@P_SLNO )"


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()


                            db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, strArLine(6).Trim())
                            db.AddInParameter(commProc, "@P_ACCOUNT_CLASS", DbType.String, strArLine(32).Trim())
                            db.AddInParameter(commProc, "@P_USER_NAME", DbType.String, CommonAppSet.User.ToString())
                            db.AddInParameter(commProc, "@P_SLNO", DbType.Int32, SLNO)

                            Dim result As Integer
                            result = db.ExecuteNonQuery(commProc, trans)
                            If result < 0 Then
                                MessageBox.Show("Import Error ", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                trans.Rollback()
                                Exit Sub
                            Else
                            End If


                        End If
                    End If
                    'MessageBox.Show(strLine)
                Loop

                
                trans.Commit()

                log_message = " Imported : Flex Customer File Date : " + dtTrans.ToString() + "." + " " + " By " + " " + CommonAppSet.User.ToString()
                Logger.system_log(log_message)

                MessageBox.Show("Import Successfull", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End Using




            oRead.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub


    Private Sub FrmImportFlexCust_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub

    Private Function Authorize() As TransState
        Dim tStatus As TransState
        tStatus = TransState.MakerCheckerSame

        Dim chrSep As Char() = {"~"}
        Dim strLine As String = ""
        Dim strArLine As String()
        Dim dtTrans As New DateTime
        Dim strSql As String = ""




        'Dim oFile As System.IO.File

        If txtFileName.Text.Trim = "" Then
            MessageBox.Show("No File")
            Exit Function

        End If

        Dim db2 As New SqlDatabase(CommonAppSet.ConnStr)

        Dim ds As New DataSet

        ds = db2.ExecuteDataSet(CommandType.Text, "Select distinct USER_NAME From FLEX_ACCOUNT_M")



        If ds.Tables(0).Rows.Count > 0 Then


            If ds.Tables(0).Rows(0)("USER_NAME").ToString() = CommonAppSet.User.Trim() Then
                Return TransState.MakerCheckerSame
                Exit Function

            End If
        End If

        Dim oRead As System.IO.StreamReader

        Try
            oRead = File.OpenText(txtFileName.Text)


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Using conn As DbConnection = db.CreateConnection()

                conn.Open()

                'Dim trans As DbTransaction = conn.BeginTransaction()
                Dim commProc As DbCommand
                'db.ExecuteNonQuery(CommandType.Text, "delete FLEX_ACCOUNT_C")
                Dim strSql4 As String = ""
                strSql4 = "select * From FLEX_ACCOUNT_M ORDER BY SLNO ASC"

                commProc = db.GetSqlStringCommand(strSql4)

                commProc.Parameters.Clear()

                dt2 = db.ExecuteDataSet(commProc).Tables(0)


                Dim j As Integer = 0
                'Dim SLNO As Integer = 0
                Do While oRead.Peek <> -1
                    strLine = oRead.ReadLine()


                    If Not strLine.Trim() = "" Then
                        strArLine = strLine.Split(chrSep)

                        If strArLine(0).Trim() = "HH" Then

                            dtTrans = DateTime.ParseExact(strArLine(4), "yyyyMMdd", New CultureInfo("en-US"))

                        End If

                        If strArLine(0).Trim() = "D" Then

                            'SLNO = SLNO + 1
                            'strSql = "insert into FLEX_ACCOUNT_C(ACCOUNT, ACCOUNT_CLASS, USER_NAME,SLNO) values(@P_ACCOUNT, @P_ACCOUNT_CLASS, @P_USER_NAME,@P_SLNO )"


                            'commProc = db.GetSqlStringCommand(strSql)

                            'commProc.Parameters.Clear()

                            'db.AddInParameter(commProc, "@P_ACCOUNT", DbType.String, strArLine(6).Trim())
                            'db.AddInParameter(commProc, "@P_ACCOUNT_CLASS", DbType.String, strArLine(32).Trim())
                            'db.AddInParameter(commProc, "@P_USER_NAME", DbType.String, CommonAppSet.User)
                            'db.AddInParameter(commProc, "@P_SLNO", DbType.Int32, SLNO)

                            'Dim result As Integer
                            'result = db.ExecuteNonQuery(commProc)
                            'If result < 0 Then
                            '    MessageBox.Show("Import Error ", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            '    'trans.Rollback()
                            '    Exit Function
                            'Else
                            'End If

                            If dt2.Rows(j)("ACCOUNT") <> strArLine(6).Trim() Then
                                Return TransState.UpdateNotPossible
                                Exit Function
                            End If
                            If dt2.Rows(j)("ACCOUNT_CLASS") <> strArLine(32).Trim() Then
                                Return TransState.UpdateNotPossible
                                Exit Function
                            End If

                            j = j + 1

                            'If oRead.Peek = j - 1 Then
                            '    Return TransState.UpdateNotPossible
                            '    Exit Function
                            'End If

                        End If
                    End If
                    'MessageBox.Show(strLine)
                Loop


                            db.ExecuteNonQuery(CommandType.Text, "delete FLEX_ACCOUNT")



                            strSql = "insert into FLEX_ACCOUNT(ACCOUNT, ACCOUNT_CLASS) select ACCOUNT, ACCOUNT_CLASS from FLEX_ACCOUNT_M"

                            commProc = db.GetSqlStringCommand(strSql)
                            db.ExecuteNonQuery(commProc)


                log_message = " Authorized : Flex Customer File Date : " + dtTrans.ToString() + "." + " " + " By " + " " + CommonAppSet.User.ToString()
                            Logger.system_log(log_message)

                            Return TransState.Update

                            MessageBox.Show("Authorized Successfull", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)


                'trans.Commit()

                'Dim strSql4 As String = ""


                'strSql = "select * From FLEX_ACCOUNT_C ORDER BY SLNO ASC"

                'commProc = db.GetSqlStringCommand(strSql)

                'commProc.Parameters.Clear()

                'dt = db.ExecuteDataSet(commProc).Tables(0)

                'strSql4 = "select * From FLEX_ACCOUNT_M ORDER BY SLNO ASC"

                'commProc = db.GetSqlStringCommand(strSql4)

                'commProc.Parameters.Clear()

                'dt2 = db.ExecuteDataSet(commProc).Tables(0)

                'If dt.Rows.Count <> dt2.Rows.Count Then
                '    Return TransState.UpdateNotPossible
                '    Exit Function

                'Else
                'For i = 0 To dt.Rows.Count - 1

                '    If dt.Rows(i)("ACCOUNT") <> dt2.Rows(i)("ACCOUNT") Then
                '        Return TransState.UpdateNotPossible
                '        Exit Function
                '    End If
                '    If dt.Rows(i)("ACCOUNT_CLASS") <> dt2.Rows(i)("ACCOUNT_CLASS") Then
                '        Return TransState.UpdateNotPossible
                '        Exit Function
                '    End If

                'Next


                'db.ExecuteNonQuery(CommandType.Text, "delete FLEX_ACCOUNT")



                'strSql = "insert into FLEX_ACCOUNT(ACCOUNT, ACCOUNT_CLASS) select ACCOUNT, ACCOUNT_CLASS from FLEX_ACCOUNT_C"

                'commProc = db.GetSqlStringCommand(strSql)
                'db.ExecuteNonQuery(commProc)


                'log_message = "Authorized Flex Customer File Date " + dtTrans.ToString() + " By " + CommonAppSet.User.ToString()
                'Logger.system_log(log_message)

                'Return TransState.Update


                'End If



                'log_message = " Import Flex Customer File Date " + dtTrans.ToString + " By " + CommonAppSet.User.ToString()
                'Logger.system_log(log_message)

                'MessageBox.Show("Authorized Successfull", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End Using




            oRead.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Private Sub btnAuthorizer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorizer.Click
        Dim tState As TransState


        Try
            If MessageBox.Show("Do you really want to Authorize?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then



                tState = Authorize()

                If tState = TransState.Update Then
                    'LoadAppData(_intSlno, _intModno)
                    lblToolStatus.Text = "!! Authorized Successfully !!"
                ElseIf tState = TransState.MakerCheckerSame Then
                    lblToolStatus.Text = "!! Failed! Maker Can't Verify Data !!"
                ElseIf tState = TransState.UpdateNotPossible Then
                    lblToolStatus.Text = "!! Failed! Record MisMatch !!"
                ElseIf tState = TransState.DBError Then
                    lblToolStatus.Text = "!! Database error occured. Please, Try Again !!"
                ElseIf tState = TransState.UnspecifiedError Then
                    lblToolStatus.Text = "!! Unpecified Error Occured !!"
                End If



            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
End Class