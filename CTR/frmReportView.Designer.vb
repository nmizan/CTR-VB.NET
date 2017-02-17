<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.crReportView = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'crReportView
        '
        Me.crReportView.ActiveViewIndex = -1
        Me.crReportView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crReportView.DisplayGroupTree = False
        Me.crReportView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crReportView.Location = New System.Drawing.Point(0, 0)
        Me.crReportView.Name = "crReportView"
        Me.crReportView.SelectionFormula = ""
        Me.crReportView.Size = New System.Drawing.Size(779, 413)
        Me.crReportView.TabIndex = 0
        Me.crReportView.ViewTimeSelectionFormula = ""
        '
        'frmReportView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(779, 413)
        Me.Controls.Add(Me.crReportView)
        Me.Name = "frmReportView"
        Me.Text = "Report Viewer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crReportView As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
