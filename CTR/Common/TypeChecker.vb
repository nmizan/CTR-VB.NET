Imports System.Globalization
Imports CTR
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.XPath


Public Class TypeChecker

    Public Shared Function IsContained(ByVal TypeName As String, ByVal TypeValue As String) As Boolean

        Dim xMLDoc As XmlDocument = New XmlDocument()
        xMLDoc.Load("goAMLSchema.xsd")

        Dim xMan As XmlNamespaceManager = New XmlNamespaceManager(xMLDoc.NameTable)
        xMan.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema")

        Dim xNodeList As XmlNodeList = xMLDoc.SelectNodes("//xs:schema/xs:simpleType[@name='" & TypeName & "']/xs:restriction[@base='xs:string']/xs:enumeration[@value='" & TypeValue & "']", xMan)
        
        'MessageBox.Show(xNodeList.Count)

        If xNodeList.Count > 0 Then

            Return True
        Else
            Return False

        End If

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="TypeName"></param>
    ''' <param name="TypeValue"></param>
    ''' <param name="IgnoreBlank">To ignore the blank type value</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsContained(ByVal TypeName As String, ByVal TypeValue As String, ByVal IgnoreBlank As Boolean) As Boolean

        If IgnoreBlank = True And TypeValue.Trim() = "" Then
            Return True
        End If


        Dim xMLDoc As XmlDocument = New XmlDocument()
        xMLDoc.Load("goAMLSchema.xsd")

        Dim xMan As XmlNamespaceManager = New XmlNamespaceManager(xMLDoc.NameTable)
        xMan.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema")

        Dim xNodeList As XmlNodeList = xMLDoc.SelectNodes("//xs:schema/xs:simpleType[@name='" & TypeName & "']/xs:restriction[@base='xs:string']/xs:enumeration[@value='" & TypeValue & "']", xMan)

        'MessageBox.Show(xNodeList.Count)

        If xNodeList.Count > 0 Then

            Return True
        Else
            Return False

        End If

    End Function


End Class
