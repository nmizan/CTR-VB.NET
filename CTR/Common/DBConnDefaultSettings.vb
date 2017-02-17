Imports System.Configuration


Public Class DBConnDefaultSettings
    Inherits ConfigurationElement


    <ConfigurationProperty("Name")> Public Property Name() As String
        Get
            Return Me("Name")
        End Get
        Set(ByVal value As String)
            Me("Name") = value
        End Set
    End Property

    

End Class
