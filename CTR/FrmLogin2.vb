Imports System.Data.Common
Imports System.Collections.Specialized
Imports System.Collections
Imports System.Configuration

Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.DirectoryServices.AccountManagement
Imports System.Reflection

Imports System.DirectoryServices

Imports System.Security.Principal


Public Class FrmLogin2

#Region "Global Variables"

    Dim log_mesage As String = ""

    Private _ExitMdi As Boolean = False

#End Region

#Region "User Defined Codes"

    ''' <summary>
    ''' Load Configuration Settings
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadConfSettings()

        Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings

        CommonAppSet.Server = appSettings("SERVER")
        CommonAppSet.Database = appSettings("DATABASE")
        CommonAppSet.Domain = appSettings("DOMAIN")
        CommonAppSet.ConnStr = appSettings("ConnectionString")

        'Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        'Dim gui As DBConnSection = CType(config.Sections("DBConnSection"), DBConnSection)
        'MessageBox.Show(gui.DEVConfig.Server)

        Dim dbSetting As DBConnSection

        dbSetting = DBConnSection.Open()

        MessageBox.Show(dbSetting.DEVConfig.Server)
        MessageBox.Show(dbSetting.UATConfig.Server)


        'dbSetting.UATConfig.Server = "matha nosto"

        'dbSetting.Save()


        '        Configuration config = 
        '    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        'MySection gui = (MySection)config.Sections["MySection"];
        'float fSize = gui.SectorConfig.TextSize;
        'SomeSettings set1 = gui.SectorConfig;


    End Sub


    WriteOnly Property Status()
        Set(ByVal value)
            lblStatus.Text = value
        End Set
    End Property

    Property IsMdiExit() As Boolean
        Get
            Return _ExitMdi
        End Get
        Set(ByVal value As Boolean)
            _ExitMdi = value
        End Set
    End Property

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


                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

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


    Private Sub LoginWithCredential()
        LoadConfSettings()
        'PrepareConnString()

        lblVersion.Text = "Ver: " & Application.ProductVersion
        lblAppName.Text = CommonAppSet.ModuleName & " (" & CommonAppSet.ModuleShortName & ")"

    End Sub


    Private Function IsUserAuthenticated() As Boolean
        ' disable account after 6 failed attempt
        ' disable account for inactivity of 100 days excepts admin accounts
        ' session sign out for inactivity of 30 miniutes:

        Dim flagLoginSuccess As Boolean = False

        CommonAppSet.User = txtUser.Text.Trim()



        '-----------
        Dim winIdent As WindowsIdentity = WindowsIdentity.GetCurrent()

        Dim ident As WindowsIdentity = WindowsIdentity.GetCurrent()


        Dim entry As New DirectoryEntry

        'Dim str As String = "/" + txtUser.Text.Trim()

        entry.Username = ident.Name


        entry.Password = txtPass.Text.Trim()


        Try

            'Bind to the native AdsObject to force authentication. 



            If CommonAppSet.IsByPass = False Then

                Dim obj As Object = entry.NativeObject

                flagLoginSuccess = True

            End If

        Catch ex As Exception

            'MsgBox(ex.Message)

            ' If Exception is Raised, Authentication is Failed, If Failure Details are Required , Un-Comment the line below

            'Throw New Exception("Authentication failed with following Error : " & ex.Message)


        End Try
        '----------

        'flagLoginSuccess = True

        If CommonAppSet.IsByPass = True Then
            flagLoginSuccess = True
        End If


        If flagLoginSuccess = True Then

            If CheckVersion() = False Then
                Application.Exit()
            End If


            'Return True
            Try

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim dtUserTable As DataTable

                'dtUserTable = db.ExecuteDataSet(CommandType.Text, "SELECT USERS_ID, DATEDIFF( day ,LAST_LOGIN_DATE,GETDATE()) AS DayDiff,USER_STAT, IS_ADMIN FROM USERS WHERE [STATUS]='L' AND USER_STAT='E' ").Tables(0)

                'dtUserTable = db.ExecuteDataSet(CommandType.Text, "SELECT USERS_ID, DATEDIFF( day ,LAST_LOGIN_DATE,GETDATE()) AS DayDiff,USER_STAT, IS_ADMIN FROM USERS WHERE [STATUS]='L' AND USER_STAT='E' AND LAST_LOGIN_DATE <> '' ").Tables(0)

                'dtUserTable = db.ExecuteDataSet(CommandType.Text, "SELECT USERS_ID, LAST_LOGIN_DATE, USER_STAT, IS_ADMIN FROM USERS WHERE [STATUS]='L' AND USER_STAT='E' ").Tables(0)

                dtUserTable = db.ExecuteDataSet(CommandType.Text, "SELECT USERS_ID, CONVERT(date, LAST_LOGIN_DATE) as LAST_LOGIN_DATE,USER_STAT, IS_ADMIN FROM USERS WHERE [STATUS]='L' AND USER_STAT='E' ").Tables(0)


                For k = 0 To dtUserTable.Rows.Count - 1

                    'Dim daydiff As String = dtUserTable.Rows(k)("DayDiff").ToString()
                    'Dim admin_user As String = dtUserTable.Rows(k)("IS_ADMIN").ToString()


                    Dim User_ID As String = dtUserTable.Rows(k)("USERS_ID").ToString()

                    Dim DayDif As Long = 0

                    If Not dtUserTable.Rows(k)("LAST_LOGIN_DATE") Is DBNull.Value Then

                        Dim last_date As Date = dtUserTable.Rows(k)("LAST_LOGIN_DATE")
                        Dim today_date As Date = DateTime.Today


                        DayDif = DateDiff(DateInterval.Day, last_date, today_date)

                    End If

                    'If ((dtUserTable.Rows(k)("DayDiff") >= 100) And dtUserTable.Rows(k)("IS_ADMIN") = 0) Then

                    If ((DayDif >= 100) And dtUserTable.Rows(k)("IS_ADMIN") = 0) Then

                        Dim strSql2 As String = "update USERS set USER_STAT='D'" & _
                                                " where USERS_ID='" & User_ID & "'"

                        db.ExecuteNonQuery(CommandType.Text, strSql2)

                        log_mesage = "User " + dtUserTable.Rows(k)("USERS_ID").ToString() + " is disabled due to inactivity [" + DayDif.ToString() + "] of over 100 Days "

                        Logger.system_log(log_mesage)


                    End If


                Next


                Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_Users_GetDetailByCode")

                commProc.Parameters.Clear()

                db.AddInParameter(commProc, "@USERS_ID", DbType.String, CommonAppSet.User)

                Dim dt As New DataTable

                dt = db.ExecuteDataSet(commProc).Tables(0)

                If dt.Rows.Count > 0 Then

                    If (dt.Rows(0)("USER_STAT").ToString() = "E") Then

                        CommonAppSet.IsAdmin = NullHelper.ToBool(dt.Rows(0)("IS_ADMIN"))

                        Dim strSql As String = "update USERS set INV_ATTEMPT=0" & _
                                               ", LAST_LOGIN_DATE=GETDATE()" & _
                                               "   where USERS_ID='" & CommonAppSet.User & "'"
                        db.ExecuteNonQuery(CommandType.Text, strSql)

                        log_mesage = "User " + CommonAppSet.User + " Successfully Logged In"
                        Logger.system_log(log_mesage)

                        Return True


                    ElseIf (dt.Rows(0)("USER_STAT").ToString() = "D") Then

                        log_mesage = "User " + CommonAppSet.User + " Trying To Login But Account has been Disabled"
                        Logger.system_log(log_mesage)

                        MessageBox.Show("You ID is disable!!", "Login Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Return False
                    Else

                        MessageBox.Show("You ID is corrupted!!", "Login Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Return False
                    End If

                Else

                    MessageBox.Show("You are not authorized!!", "Login Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    Return False

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try


        Else

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand

            Dim strSql As String = ""

            Dim dt As New DataTable

            strSql = "select USERS_ID,USER_STAT,INV_ATTEMPT From USERS where USERS_ID=@USERS_ID"

            commProc = db.GetSqlStringCommand(strSql)
            commProc.Parameters.Clear()
            db.AddInParameter(commProc, "@USERS_ID", DbType.String, CommonAppSet.User)

            dt = db.ExecuteDataSet(commProc).Tables(0)


            If dt.Rows.Count > 0 Then
                'If (dt.Rows(0)("INV_ATTEMPT").ToString <> "6") Then
                If (NullHelper.ToIntNum(dt.Rows(0)("INV_ATTEMPT")) < 6) Then

                    'strSql = "update USERS set INV_ATTEMPT ='" & dt.Rows(0)("INV_ATTEMPT") + "1" & "' where USERS_ID='" & CommonAppSet.User.ToString() & "'"

                    strSql = "update USERS set INV_ATTEMPT =ISNULL(INV_ATTEMPT,0)+1 where USERS_ID='" & CommonAppSet.User.ToString() & "'"

                    db.ExecuteNonQuery(CommandType.Text, strSql)

                    log_mesage = "User " + CommonAppSet.User + " Login Failed"
                    Logger.system_log(log_mesage)

                ElseIf (dt.Rows(0)("INV_ATTEMPT") >= 5) Then

                    'strSql = "update USERS set INV_ATTEMPT='" & dt.Rows(0)("INV_ATTEMPT") + "1" & _
                    '         "', USER_STAT='D'" & _
                    '         " where USERS_ID='" & CommonAppSet.User.ToString() & "'"

                    strSql = "update USERS set INV_ATTEMPT=INV_ATTEMPT+1" & _
                             ", USER_STAT='D'" & _
                             " where USERS_ID='" & CommonAppSet.User.ToString() & "'"


                    db.ExecuteNonQuery(CommandType.Text, strSql)

                    log_mesage = "User " + CommonAppSet.User + " Disabled For Maximum Try"
                    Logger.system_log(log_mesage)

                End If
            End If

            MessageBox.Show("Invalid User/Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)

            txtPass.SelectAll()
            txtPass.Focus()
        End If

        Return False

    End Function



    Private Function CheckVersion() As Boolean


        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_Version_GetDetail")

            Dim dt As New DataTable

            dt = db.ExecuteDataSet(commProc).Tables(0)

            If dt.Rows.Count > 0 Then

                If (dt.Rows(0)("CHK").ToString() = "1") Then


                    Dim dbMinMajor As Integer = dt.Rows(0)("MIN_MAJOR")
                    Dim dbMinMinor As Integer = dt.Rows(0)("MIN_MINOR")
                    Dim dbMinBuild As Integer = dt.Rows(0)("MIN_BUILD")
                    Dim dbMaxMajor As Integer = dt.Rows(0)("MAX_MAJOR")
                    Dim dbMaxMinor As Integer = dt.Rows(0)("MAX_MINOR")
                    Dim dbMaxBuild As Integer = dt.Rows(0)("MAX_BUILD")

                    Dim verApp As Version = Assembly.GetExecutingAssembly().GetName().Version

                    Dim verMin As New Version(dbMinMajor, dbMinMinor, dbMinBuild, 0)
                    Dim verMax As New Version(dbMaxMajor, dbMaxMinor, dbMaxBuild, 0)

                    If (verMin <= verApp And verMax >= verApp) Then

                        Return True
                    End If

                    MessageBox.Show("Application version incompatible." + Environment.NewLine + _
                        "Your app ver: " + Application.ProductVersion + Environment.NewLine + _
                        "Please contact with your administrator to continue.", "Version Incompatible!!", MessageBoxButtons.OK, MessageBoxIcon.Error)


                Else 'no version check needed

                    ' so the check is ok.
                    Return True
                End If

            Else

                MessageBox.Show("No version information found", "Version Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)


            End If

        Catch ex As SqlClient.SqlException

            If ex.ErrorCode = -2146232060 Then
                MessageBox.Show("You are not authorized!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            Exit Function

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try



        Return False



    End Function




#End Region



    Private Sub FrmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        MessageBox.Show("You are authorized to use this system for approved business purposes only." + Environment.NewLine + _
                        "Use for any other purpose is prohibited. All transactional records, reports, " + Environment.NewLine + _
                        "e-mail, software and other data generated by or residing upon this system are " + Environment.NewLine + _
                        "the property of the company and may be used by the company for any purpose." + Environment.NewLine + _
                        "Authorized and unauthorized activities may be monitored", CommonAppSet.ModuleName, MessageBoxButtons.OK, MessageBoxIcon.None)


        'LoginWithoutCredential()
        LoginWithCredential()



        txtUser.Text = Environment.UserName
        lblDomain.Text = Environment.UserDomainName

        If lblDomain.Text.Trim().ToUpper() <> CommonAppSet.Domain.Trim().ToUpper() Then
            lblStatus.Text = "Mismatch domain name"
        End If



    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()

    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click


        lblStatus.Text = ""

        If lblDomain.Text.Trim().ToUpper() <> CommonAppSet.Domain.Trim().ToUpper() Then
            MessageBox.Show("Update your domain name", "Domain Name Mismatch!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblStatus.Text = "Mismatch domain name"
            Exit Sub
        End If

        If txtUser.Text.Trim().ToUpper() <> Environment.UserName.Trim().ToUpper() Then
            MessageBox.Show("No USER other than the USER currently logged on the WINDOWS OS are allowed", "Invalid Login!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblStatus.Text = "Invalid login"
            Exit Sub
        End If

        If txtPass.Text.Trim() = "" Then
            MessageBox.Show("Empty password not allowed", "Invalid Login!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblStatus.Text = "Empty password not allowed"
            txtPass.Focus()
            Exit Sub
        End If

        Try
            If IsUserAuthenticated() = True Then
                txtPass.Clear()
                Me.Hide()

                IsMdiExit = True
                Dim frmMain As New FrmMain(Me)
                frmMain.ShowDialog()

                Logger.system_log("User: " + CommonAppSet.User + " Logged Out.")

                If IsMdiExit = True Then
                    Me.Dispose()
                Else
                    Me.Show()

                End If


                'Me.Dispose()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try




    End Sub


    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

        lblStatus.Text = ""

        Dim frmSetting As New FrmAppSettting()
        frmSetting.ShowDialog()

        LoadConfSettings()
        'PrepareConnString()

    End Sub

    Private Sub txtUser_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUser.KeyDown
        If e.KeyCode = Keys.Enter And txtUser.Text.Trim() <> "" Then
            SendKeys.Send("{tab}")
        End If
    End Sub

    Private Sub txtPass_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPass.KeyDown
        If e.KeyCode = Keys.Enter And txtPass.Text.Trim() <> "" Then
            btnLogin_Click(sender, e)
        End If
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

   
End Class
