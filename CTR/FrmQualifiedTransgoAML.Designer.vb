<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmQualifiedTransgoAML
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmQualifiedTransgoAML))
        Me.btnClose = New System.Windows.Forms.Button
        Me.txtAcNumber = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.grpClient = New System.Windows.Forms.GroupBox
        Me.cmboBranch = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.btnReport = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox
        Me.grpEntry = New System.Windows.Forms.GroupBox
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtDateTo2 = New System.Windows.Forms.MaskedTextBox
        Me.txtDateFrom2 = New System.Windows.Forms.MaskedTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtAcNumber2 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.brnReportCTR = New System.Windows.Forms.Button
        Me.btnPdf = New System.Windows.Forms.Button
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.grpClient.SuspendLayout()
        Me.grpEntry.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(12, 302)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(50, 27)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Exit"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtAcNumber
        '
        Me.txtAcNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcNumber.Location = New System.Drawing.Point(116, 14)
        Me.txtAcNumber.Name = "txtAcNumber"
        Me.txtAcNumber.Size = New System.Drawing.Size(174, 20)
        Me.txtAcNumber.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(54, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 20)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "A/C :"
        '
        'grpClient
        '
        Me.grpClient.Controls.Add(Me.cmboBranch)
        Me.grpClient.Controls.Add(Me.Label11)
        Me.grpClient.Controls.Add(Me.txtAcNumber)
        Me.grpClient.Controls.Add(Me.Label4)
        Me.grpClient.Location = New System.Drawing.Point(12, 90)
        Me.grpClient.Name = "grpClient"
        Me.grpClient.Size = New System.Drawing.Size(607, 42)
        Me.grpClient.TabIndex = 8
        Me.grpClient.TabStop = False
        '
        'cmboBranch
        '
        Me.cmboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmboBranch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmboBranch.FormattingEnabled = True
        Me.cmboBranch.Location = New System.Drawing.Point(362, 11)
        Me.cmboBranch.Name = "cmboBranch"
        Me.cmboBranch.Size = New System.Drawing.Size(225, 21)
        Me.cmboBranch.TabIndex = 13
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(296, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 20)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Branch:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnReport
        '
        Me.btnReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReport.Location = New System.Drawing.Point(511, 147)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(108, 27)
        Me.btnReport.TabIndex = 10
        Me.btnReport.Text = "Excel Report"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(467, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 20)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "dd/mm/yyyy"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(218, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "dd/mm/yyyy"
        '
        'txtDateTo
        '
        Me.txtDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateTo.Location = New System.Drawing.Point(375, 29)
        Me.txtDateTo.Mask = "00/00/0000"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(87, 20)
        Me.txtDateTo.TabIndex = 4
        Me.txtDateTo.ValidatingType = GetType(Date)
        '
        'grpEntry
        '
        Me.grpEntry.Controls.Add(Me.Label6)
        Me.grpEntry.Controls.Add(Me.Label2)
        Me.grpEntry.Controls.Add(Me.txtDateTo)
        Me.grpEntry.Controls.Add(Me.txtDateFrom)
        Me.grpEntry.Controls.Add(Me.Label1)
        Me.grpEntry.Controls.Add(Me.Label5)
        Me.grpEntry.Location = New System.Drawing.Point(11, 18)
        Me.grpEntry.Name = "grpEntry"
        Me.grpEntry.Size = New System.Drawing.Size(607, 66)
        Me.grpEntry.TabIndex = 7
        Me.grpEntry.TabStop = False
        Me.grpEntry.Text = "Qualified Transaction goAML"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateFrom.Location = New System.Drawing.Point(125, 29)
        Me.txtDateFrom.Mask = "00/00/0000"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(87, 20)
        Me.txtDateFrom.TabIndex = 1
        Me.txtDateFrom.ValidatingType = GetType(Date)
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(271, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Range To:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(52, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 20)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Range From:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtDateTo2)
        Me.GroupBox1.Controls.Add(Me.txtDateFrom2)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 181)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(607, 59)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "CTR vs goAML Qualified Transaction"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(467, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "dd/mm/yyyy"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(218, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 20)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "dd/mm/yyyy"
        '
        'txtDateTo2
        '
        Me.txtDateTo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateTo2.Location = New System.Drawing.Point(375, 24)
        Me.txtDateTo2.Mask = "00/00/0000"
        Me.txtDateTo2.Name = "txtDateTo2"
        Me.txtDateTo2.Size = New System.Drawing.Size(87, 20)
        Me.txtDateTo2.TabIndex = 4
        Me.txtDateTo2.ValidatingType = GetType(Date)
        '
        'txtDateFrom2
        '
        Me.txtDateFrom2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateFrom2.Location = New System.Drawing.Point(125, 24)
        Me.txtDateFrom2.Mask = "00/00/0000"
        Me.txtDateFrom2.Name = "txtDateFrom2"
        Me.txtDateFrom2.Size = New System.Drawing.Size(87, 20)
        Me.txtDateFrom2.TabIndex = 1
        Me.txtDateFrom2.ValidatingType = GetType(Date)
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(271, 27)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 20)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "Range To:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(52, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 20)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Range From:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtAcNumber2)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 246)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(607, 42)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        '
        'txtAcNumber2
        '
        Me.txtAcNumber2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcNumber2.Location = New System.Drawing.Point(209, 16)
        Me.txtAcNumber2.Name = "txtAcNumber2"
        Me.txtAcNumber2.Size = New System.Drawing.Size(208, 20)
        Me.txtAcNumber2.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(138, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(99, 20)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "A/C :"
        '
        'brnReportCTR
        '
        Me.brnReportCTR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.brnReportCTR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.brnReportCTR.Location = New System.Drawing.Point(510, 302)
        Me.brnReportCTR.Name = "brnReportCTR"
        Me.brnReportCTR.Size = New System.Drawing.Size(108, 27)
        Me.brnReportCTR.TabIndex = 17
        Me.brnReportCTR.Text = "Generate Report"
        Me.brnReportCTR.UseVisualStyleBackColor = True
        '
        'btnPdf
        '
        Me.btnPdf.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnPdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPdf.Location = New System.Drawing.Point(342, 147)
        Me.btnPdf.Name = "btnPdf"
        Me.btnPdf.Size = New System.Drawing.Size(152, 27)
        Me.btnPdf.TabIndex = 18
        Me.btnPdf.Text = "Branch Wise(PDF) Report"
        Me.btnPdf.UseVisualStyleBackColor = True
        '
        'FrmQualifiedTransgoAML
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(630, 364)
        Me.Controls.Add(Me.btnPdf)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.brnReportCTR)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grpClient)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.grpEntry)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "FrmQualifiedTransgoAML"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Qualified Transaction (goAML)"
        Me.grpClient.ResumeLayout(False)
        Me.grpClient.PerformLayout()
        Me.grpEntry.ResumeLayout(False)
        Me.grpEntry.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtAcNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grpClient As System.Windows.Forms.GroupBox
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents grpEntry As System.Windows.Forms.GroupBox
    Friend WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDateTo2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtDateFrom2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtAcNumber2 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents brnReportCTR As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmboBranch As System.Windows.Forms.ComboBox
    Friend WithEvents btnPdf As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
End Class
