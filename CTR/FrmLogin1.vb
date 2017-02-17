Imports System.Data.Common
Imports System.Collections.Specialized
Imports System.Collections
Imports System.Configuration

Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql


Public Class FrmLogin1

#Region "user defined procedures"

    ''' <summary>
    ''' Load Configuration Settings
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadConfSettings()

        ' Get the AppSettings collection.
        Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings

        CommonAppSet.Server = appSettings("Server")
        CommonAppSet.Database = appSettings("Database")
        CommonAppSet.TrustedConn = appSettings("isTrustedCon")
        CommonAppSet.UserId = appSettings("UID")
        CommonAppSet.UserPwd = appSettings("PWD")

        CommonAppSet.SecServer = appSettings("SMSSERVER")
        CommonAppSet.SecDatabase = appSettings("SECURITYDB")
        CommonAppSet.SecTrustedConn = appSettings("isSecTrustedCon")
        CommonAppSet.SecUserId = appSettings("SECUID")
        CommonAppSet.SecUserPwd = appSettings("SECPWD")

    End Sub

    ''' <summary>
    ''' Prepare Connection String
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PrepareConnString()

        '--- build connection string

        If CommonAppSet.SecTrustedConn = "y" Then

            CommonAppSet.SecConnStr = "server=" + CommonAppSet.SecServer + ";database=" + CommonAppSet.SecDatabase + ";trusted_connection=true;Connection Timeout=1800;"

        ElseIf CommonAppSet.SecTrustedConn = "n" Then

            CommonAppSet.SecConnStr = "server=" + CommonAppSet.SecServer + "; database=" + CommonAppSet.SecDatabase + "; uid=" + CommonAppSet.SecUserId + ";pwd=" + CommonAppSet.SecUserPwd + ";Connection Timeout=1800;"

        Else
            Throw New Exception("Invalid value in Configuration File")
        End If

        If CommonAppSet.TrustedConn = "y" Then

            CommonAppSet.ConnStr = "server=" + CommonAppSet.Server + "; database=" + CommonAppSet.Database + "; trusted_connection=true;Connection Timeout=1800;"

        ElseIf CommonAppSet.TrustedConn = "n" Then

            CommonAppSet.ConnStr = "server=" + CommonAppSet.Server + "; database=" + CommonAppSet.Database + "; uid=" + CommonAppSet.UserId + "; pwd=" + CommonAppSet.UserPwd + ";Connection Timeout=1800;"

        Else
            Throw New Exception("Invalid value in Configuration File")
        End If

    End Sub

    Private Function CheckLogin() As Boolean

        Try



            Dim x As System.Security.Principal.WindowsIdentity
            x = System.Security.Principal.WindowsIdentity.GetCurrent()

            If x.IsAuthenticated Then



                If x.Name.Trim() <> "" Then
                    Dim intSepPos As Integer
                    intSepPos = x.Name.IndexOf("\")
                    CommonAppSet.Domain = x.Name.Substring(0, intSepPos)
                    CommonAppSet.User = x.Name.Substring(intSepPos + 1, x.Name.Length - 1 - intSepPos)
                End If

                'MessageBox.Show("Domain: " + mStrDomain + " User: " + mStrUid)

                Dim db As New SqlDatabase(CommonAppSet.SecConnStr)

                Dim strSql As String = "select * from users where USERS_ID='" + CommonAppSet.User + "' and DOMAIN='" + CommonAppSet.Domain + "'"

                Dim ds As DataSet = db.ExecuteDataSet(CommandType.Text, strSql)

                If ds.Tables(0).Rows.Count() > 0 Then
                    If ds.Tables(0).Rows(0)("USER_STAT").ToString() = "D" Then
                        MessageBox.Show("User is disabled." + vbCrLf + "Please contact with your administrator.", "!!Login Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If

                    Return True
                End If

                MessageBox.Show("You are not authorized to use the application!", "!!Login Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False

            End If


        Catch ex As Exception

            'MessageBox.Show("Internal Error !" + vbCrLf + "Access is denied", "!!Login Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        MessageBox.Show("Internal Error !" + vbCrLf + "Access is denied", "!!Login Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Return False

    End Function


    Private Function CheckVersion() As Boolean
        Try



            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim strSql As String

            strSql = "SELECT * FROM VERSION"

            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item(0).ToString() = "1" Then

                    'If (Convert.ToInt32(Application.ProductVersion(0).ToString().Trim() + Application.ProductVersion(2).ToString().Trim()) <= _
                    '   Convert.ToInt32(ds.Tables(0).Rows(0).Item(3).ToString().Trim() + ds.Tables(0).Rows(0).Item(4).ToString().Trim())) And _
                    '   (Convert.ToInt32(Application.ProductVersion(0).ToString().Trim() + Application.ProductVersion(2).ToString().Trim()) >= _
                    '   Convert.ToInt32(ds.Tables(0).Rows(0).Item(1).ToString().Trim() + ds.Tables(0).Rows(0).Item(2).ToString().Trim())) _
                    '   Then
                    '    Return True
                    'End If


                    If (Convert.ToInt32(Application.ProductVersion(0).ToString().Trim() + Application.ProductVersion(2).ToString().Trim()) < _
                       Convert.ToInt32(ds.Tables(0).Rows(0).Item(1).ToString().Trim() + ds.Tables(0).Rows(0).Item(2).ToString().Trim())) Then
                        MessageBox.Show("!! Please Upgrade " & CommonAppSet.ModuleShortName & " Version. !! " & vbCrLf & "A newer version is available." & vbCrLf & "Please contact with you administrator", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Return False
                    ElseIf (Convert.ToInt32(Application.ProductVersion(0).ToString().Trim() + Application.ProductVersion(2).ToString().Trim()) > _
                       Convert.ToInt32(ds.Tables(0).Rows(0).Item(3).ToString().Trim() + ds.Tables(0).Rows(0).Item(4).ToString().Trim())) Then
                        MessageBox.Show("!! Server is not updated. !! " & vbCrLf & "You are using a newer version." & vbCrLf & "Please contact with you administrator", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Return False
                    Else
                        Return True

                    End If


                ElseIf ds.Tables(0).Rows(0).Item(0).ToString() = "0" Then
                    Return True
                End If

            End If

            MessageBox.Show("!! Server is not updated. !! " & vbCrLf & "Please contact with you administrator", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False

        Catch ex As Exception
            MessageBox.Show("!! Version Error !! " & vbCrLf & "Please contact with you administrator", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return False

    End Function

#End Region



    Private Sub FrmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Hide()

        LoadConfSettings() 'load configuration settings
        PrepareConnString()

        If CheckLogin() = False Then
            'MessageBox.Show("You are not authorized to use the application!", "!!Login Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()

        End If


        If CheckVersion() Then
            Dim frmMain As New FrmMain
            frmMain.ShowDialog()
        End If

        Me.Dispose()
    End Sub
End Class
