Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class CommonUtil

    Public Shared Sub FillComboBox(ByVal strSql As String, ByVal cmbVal As System.Windows.Forms.ComboBox)

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        cmbVal.DataSource = ds.Tables(0)
        cmbVal.DisplayMember = ds.Tables(0).Columns(1).ToString()
        cmbVal.ValueMember = ds.Tables(0).Columns(0).ToString()
        'MsgBox(cmbVal.ValueMember)
        'cmbVal.Text = ""
        cmbVal.SelectedIndex = -1
    End Sub

    Public Shared Sub FillComboBox2(ByVal strSql As String, ByVal cmbVal As System.Windows.Forms.ComboBox)

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        cmbVal.DataSource = ds.Tables(0)
        cmbVal.DisplayMember = ds.Tables(0).Columns(1).ToString()
        cmbVal.ValueMember = ds.Tables(0).Columns(0).ToString()
        'MsgBox(cmbVal.DisplayMember)
        'cmbVal.Text = ""
        cmbVal.SelectedIndex = -1
    End Sub

    Public Shared Sub LogonReport(ByVal rptReport As ReportDocument)
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
                    .IntegratedSecurity = True
                ElseIf strIsTrustedConn = "n" Then
                    .UserID = strUID
                    .Password = strPWD
                Else
                    Throw New Exception("Invalid value in Configuration File")
                End If

            End With

            crTableLogonInfo = crTable.LogOnInfo
            crTableLogonInfo.ConnectionInfo = crConnInfo
            crTable.ApplyLogOnInfo(crTableLogonInfo)


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


                        End With

                        crTableLogonInfo = crTable.LogOnInfo
                        crTableLogonInfo.ConnectionInfo = crConnInfo
                        crTable.ApplyLogOnInfo(crTableLogonInfo)

                    Next
                End If
            Next
        Next
    End Sub



End Class
