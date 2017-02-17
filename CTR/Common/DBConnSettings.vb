Imports System.Configuration


Public Class DBConnSettings
    Inherits ConfigurationElement


    <ConfigurationProperty("Server")> Public Property Server() As String
        Get
            Return Me("Server")
        End Get
        Set(ByVal value As String)
            Me("Server") = value
        End Set
    End Property

    <ConfigurationProperty("Database")> Public Property Database() As String
        Get
            Return Me("Database")
        End Get
        Set(ByVal value As String)
            Me("Database") = value
        End Set
    End Property

    <ConfigurationProperty("Domain")> Public Property Domain() As String
        Get
            Return Me("Domain")
        End Get
        Set(ByVal value As String)
            Me("Domain") = value
        End Set
    End Property

    <ConfigurationProperty("ConnectionString")> Public Property ConnectionString() As String
        Get
            Return Me("ConnectionString")
        End Get
        Set(ByVal value As String)
            Me("ConnectionString") = value
        End Set
    End Property

End Class
