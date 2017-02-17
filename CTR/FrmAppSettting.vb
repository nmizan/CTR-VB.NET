
'
'Author             : Md. Fahad Khan
'Purpose            : Maintain Database Connection String
'Creation date      : 23-07-2013  
'Last Modified      : 21-08-2013 [by Iftekharul Alam Khan Lodi]

Imports System.Collections.Specialized
Imports System.Configuration


Public Class FrmAppSettting
    Dim dbSetting As DBConnSection

    Private Sub FrmAppSettting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings

        'txtServer.Text = appSettings("SERVER")
        'txtDatabase.Text = appSettings("DATABASE")
        'txtConnString.Text = appSettings("ConnectionString")
        'txtDomain.Text = appSettings("DOMAIN")

        dbSetting = DBConnSection.Open()

        txtServer.Text = dbSetting.PRODConfig.Server
        txtDatabase.Text = dbSetting.PRODConfig.Database
        txtConnString.Text = dbSetting.PRODConfig.ConnectionString
        txtDomain.Text = dbSetting.PRODConfig.Domain


        txtUATServer.Text = dbSetting.UATConfig.Server
        txtUATDatabase.Text = dbSetting.UATConfig.Database
        txtUATConnString.Text = dbSetting.UATConfig.ConnectionString
        txtUATDomain.Text = dbSetting.UATConfig.Domain

        txtDEVServer.Text = dbSetting.DEVConfig.Server
        txtDEVDatabase.Text = dbSetting.DEVConfig.Database
        txtDEVConnString.Text = dbSetting.DEVConfig.ConnectionString
        txtDEVDomain.Text = dbSetting.DEVConfig.Domain

        If dbSetting.DefaultConfig.Name = "UAT" Then
            rbtnUAT.Checked = True
        ElseIf dbSetting.DefaultConfig.Name = "PROD" Then
            rbtnPROD.Checked = True
        ElseIf dbSetting.DefaultConfig.Name = "DEV" Then
            rbtnDEV.Checked = True
        End If
        

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings
        'Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

        'config.AppSettings.Settings("SERVER").Value = txtServer.Text.Trim()
        'config.AppSettings.Settings("DATABASE").Value = txtDatabase.Text.Trim()
        'config.AppSettings.Settings("ConnectionString").Value = txtConnString.Text.Trim()
        'config.AppSettings.Settings("DOMAIN").Value = txtDomain.Text.Trim()

        'config.Save(ConfigurationSaveMode.Modified)
        'ConfigurationManager.RefreshSection("appSettings")

        dbSetting.PRODConfig.Server = txtServer.Text.Trim()
        dbSetting.PRODConfig.Database = txtDatabase.Text.Trim()
        dbSetting.PRODConfig.ConnectionString = txtConnString.Text.Trim()
        dbSetting.PRODConfig.Domain = txtDomain.Text.Trim()


        dbSetting.UATConfig.Server = txtUATServer.Text.Trim()
        dbSetting.UATConfig.Database = txtUATDatabase.Text.Trim()
        dbSetting.UATConfig.ConnectionString = txtUATConnString.Text.Trim()
        dbSetting.UATConfig.Domain = txtUATDomain.Text.Trim()

        dbSetting.DEVConfig.Server = txtDEVServer.Text.Trim()
        dbSetting.DEVConfig.Database = txtDEVDatabase.Text.Trim()
        dbSetting.DEVConfig.ConnectionString = txtDEVConnString.Text.Trim()
        dbSetting.DEVConfig.Domain = txtDEVDomain.Text.Trim()

        If rbtnUAT.Checked = True Then
            dbSetting.DefaultConfig.Name = "UAT"
        ElseIf rbtnPROD.Checked = True Then
            dbSetting.DefaultConfig.Name = "PROD"
        ElseIf rbtnDEV.Checked = True Then
            dbSetting.DefaultConfig.Name = "DEV"
        End If

        dbSetting.Save()

        MessageBox.Show("Appliction settings saved", "Application Setting", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'defult port: 1433
    End Sub
End Class