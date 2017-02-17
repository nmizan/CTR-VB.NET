Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmReportView
    Public _rpt As New ReportDocument

    Public _reportFormula As String = ""

    Public Sub SetReport(ByRef rptReport As ReportDocument)

        _rpt = rptReport

    End Sub

    Public Sub SetFormula(ByVal strFormula As String)

        _reportFormula = strFormula

    End Sub
    Private Sub frmReportView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CommonUtil.LogonReport(_rpt)


        crReportView.ReportSource = _rpt

        If _reportFormula.Trim() <> "" Then
            crReportView.SelectionFormula = _reportFormula
        End If




    End Sub

    
End Class