Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared



Public Class FrmReportViewer
    Private _rd As New ReportDocument

    Public Sub SetReport(ByVal rd As ReportDocument)
        _rd = rd
    End Sub

    Private Sub FrmReportViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LogonReport(_rd)

        'Dim ConInfo As New TableLogOnInfo

        crViewer.ReportSource = Nothing



        crViewer.ReportSource = _rd

    End Sub


    Public Sub LogonReport(ByVal rptReport As CrystalDecisions.CrystalReports.Engine.ReportDocument)
        Dim strServer As String = ""
        Dim strDBase As String = ""
        Dim strUID As String = ""
        Dim strPWD As String = ""
        Dim strIsTrustedConn As String = ""

        Dim crReportDocument As New ReportDocument()
        Dim crTableLogonInfos As New TableLogOnInfos()
        Dim crTableLogonInfo As New TableLogOnInfo()
        Dim crTables As Tables
        Dim crTable As Table
        Dim crSections As Sections
        Dim crSection As Section
        Dim crReportObjects As ReportObjects
        Dim crReportObject As ReportObject
        Dim crSubreportObject As SubreportObject
        Dim crDataBase As Database
        Dim crConnInfo As New ConnectionInfo()
        Dim subRepDoc As New ReportDocument()





        strServer = CommonAppSet.Server
        strDBase = CommonAppSet.Database
        'strDBase = "BDS"
        strUID = CommonAppSet.UserId
        strPWD = CommonAppSet.UserPwd
        strIsTrustedConn = CommonAppSet.TrustedConn


        'Logs into the tables in the report
        crReportDocument = rptReport
        crDataBase = crReportDocument.Database
        crTables = crDataBase.Tables

        For Each crTable In crTables
            With crConnInfo
                .ServerName = strServer
                .DatabaseName = strDBase

                If strIsTrustedConn = "y" Then
                    crReportDocument.DataSourceConnections.Item(0).IntegratedSecurity = True
                ElseIf strIsTrustedConn = "n" Then
                    .UserID = strUID
                    .Password = strPWD
                Else
                    Throw New Exception("Invalid value in Configuration File")
                End If




                'crReportDocument.DataSourceConnections.Item(0).IntegratedSecurity = True
            End With
            'MsgBox("REPORT - TableName = " & crTable.Name & "", MsgBoxStyle.OKOnly)
            crTableLogonInfo = crTable.LogOnInfo
            crTableLogonInfo.ConnectionInfo = crConnInfo
            crTable.ApplyLogOnInfo(crTableLogonInfo)
            'crTable.Location = strDBase & ".dbo." & crTable.Name
            'crTable.ApplyLogOnInfo(crTableLogonInfo)
        Next

        'Logs into the tables in the Sub-reports
        crSections = crReportDocument.ReportDefinition.Sections
        For Each crSection In crSections
            crReportObjects = crSection.ReportObjects
            For Each crReportObject In crReportObjects
                If crReportObject.Kind = ReportObjectKind.SubreportObject Then
                    crSubreportObject = CType(crReportObject, SubreportObject)
                    subRepDoc = crSubreportObject.OpenSubreport(crSubreportObject.SubreportName)
                    crDataBase = subRepDoc.Database
                    crTables = crDataBase.Tables
                    For Each crTable In crTables
                        With crConnInfo
                            .ServerName = strServer
                            .DatabaseName = strDBase

                            If strIsTrustedConn = "y" Then
                                crReportDocument.DataSourceConnections.Item(0).IntegratedSecurity = True
                            ElseIf strIsTrustedConn = "n" Then
                                .UserID = strUID
                                .Password = strPWD
                            Else
                                Throw New Exception("Invalid value in Configuration File")
                            End If


                            '.UserID = strUID
                            '.Password = strPWD
                            'crReportDocument.DataSourceConnections.Item(0).IntegratedSecurity = True


                        End With
                        'MsgBox("SUBREPORT - TableName = " & crTable.Name & "", MsgBoxStyle.OKOnly)
                        crTableLogonInfo = crTable.LogOnInfo
                        crTableLogonInfo.ConnectionInfo = crConnInfo
                        crTable.ApplyLogOnInfo(crTableLogonInfo)
                        crTable.Location = strDBase & ".dbo." & crTable.Name
                    Next
                End If
            Next
        Next
    End Sub


End Class