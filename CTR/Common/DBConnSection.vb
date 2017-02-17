Imports System.Configuration



Public Class DBConnSection
    Inherits ConfigurationSection

#Region "Public Method"

    Public Shared Function Open() As DBConnSection

        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Return CType(config.Sections("DBConnSection"), DBConnSection)

    End Function

    Public Sub Save()
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

        Dim connSection As DBConnSection = CType(config.Sections("DBConnSection"), DBConnSection)

        connSection.UATConfig = Me.UATConfig
        connSection.PRODConfig = Me.PRODConfig
        connSection.DEVConfig = Me.DEVConfig
        connSection.DefaultConfig = Me.DefaultConfig

        config.Save(ConfigurationSaveMode.Modified)


    End Sub

#End Region


#Region "Properties"

    <ConfigurationProperty("UAT")> Public Property UATConfig() As DBConnSettings
        Get
            Return CType(Me("UAT"), DBConnSettings)
        End Get
        Set(ByVal value As DBConnSettings)
            Me("UAT") = value
        End Set
    End Property

    <ConfigurationProperty("PROD")> Public Property PRODConfig() As DBConnSettings
        Get
            Return CType(Me("PROD"), DBConnSettings)
        End Get
        Set(ByVal value As DBConnSettings)
            Me("PROD") = value
        End Set
    End Property

    <ConfigurationProperty("DEV")> Public Property DEVConfig() As DBConnSettings
        Get
            Return CType(Me("DEV"), DBConnSettings)
        End Get
        Set(ByVal value As DBConnSettings)
            Me("DEV") = value
        End Set
    End Property

    <ConfigurationProperty("DefautltConn")> Public Property DefaultConfig() As DBConnDefaultSettings
        Get
            Return CType(Me("DefautltConn"), DBConnDefaultSettings)
        End Get
        Set(ByVal value As DBConnDefaultSettings)
            Me("DefautltConn") = value
        End Set
    End Property


#End Region

End Class





