<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmImportAccountInfo
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.dgView = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column26 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column88 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column27 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column28 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column29 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column30 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column31 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column32 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column33 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column34 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column35 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column36 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column37 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column38 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column39 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column40 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column41 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column42 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column43 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column44 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column45 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column46 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column47 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column48 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column49 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column50 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column51 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column52 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column90 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column91 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column53 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.btnImportProcess = New System.Windows.Forms.Button
        Me.txtFilename = New System.Windows.Forms.TextBox
        Me.lblToolStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.opdRetFile = New System.Windows.Forms.OpenFileDialog
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnPrepareAccount = New System.Windows.Forms.Button
        Me.lblTotRecNo = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgView
        '
        Me.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column26, Me.Column88, Me.Column27, Me.Column28, Me.Column29, Me.Column30, Me.Column31, Me.Column32, Me.Column33, Me.Column34, Me.Column35, Me.Column36, Me.Column37, Me.Column38, Me.Column39, Me.Column40, Me.Column41, Me.Column42, Me.Column43, Me.Column44, Me.Column45, Me.Column46, Me.Column47, Me.Column48, Me.Column49, Me.Column50, Me.Column51, Me.Column52, Me.Column90, Me.Column91, Me.Column53, Me.Column2, Me.Column9})
        Me.dgView.Location = New System.Drawing.Point(6, 15)
        Me.dgView.MultiSelect = False
        Me.dgView.Name = "dgView"
        Me.dgView.ReadOnly = True
        Me.dgView.RowHeadersVisible = False
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkSeaGreen
        Me.dgView.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgView.Size = New System.Drawing.Size(965, 350)
        Me.dgView.TabIndex = 17
        '
        'Column1
        '
        Me.Column1.HeaderText = "Account No"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column26
        '
        Me.Column26.HeaderText = "Branch Code"
        Me.Column26.Name = "Column26"
        Me.Column26.ReadOnly = True
        Me.Column26.Width = 120
        '
        'Column88
        '
        Me.Column88.HeaderText = "Account Title"
        Me.Column88.Name = "Column88"
        Me.Column88.ReadOnly = True
        Me.Column88.Visible = False
        '
        'Column27
        '
        Me.Column27.HeaderText = "Account Type"
        Me.Column27.Name = "Column27"
        Me.Column27.ReadOnly = True
        '
        'Column28
        '
        Me.Column28.HeaderText = "Ownership Type"
        Me.Column28.Name = "Column28"
        Me.Column28.ReadOnly = True
        Me.Column28.Width = 120
        '
        'Column29
        '
        Me.Column29.HeaderText = "Deposit"
        Me.Column29.Name = "Column29"
        Me.Column29.ReadOnly = True
        '
        'Column30
        '
        Me.Column30.HeaderText = "Deposit Trans"
        Me.Column30.Name = "Column30"
        Me.Column30.ReadOnly = True
        '
        'Column31
        '
        Me.Column31.HeaderText = "Deposit Max Amount"
        Me.Column31.Name = "Column31"
        Me.Column31.ReadOnly = True
        Me.Column31.Width = 150
        '
        'Column32
        '
        Me.Column32.HeaderText = "Withdraw"
        Me.Column32.Name = "Column32"
        Me.Column32.ReadOnly = True
        Me.Column32.Width = 150
        '
        'Column33
        '
        Me.Column33.HeaderText = "Withdraw Trans"
        Me.Column33.Name = "Column33"
        Me.Column33.ReadOnly = True
        Me.Column33.Width = 190
        '
        'Column34
        '
        Me.Column34.HeaderText = "Withdraw Max Amount"
        Me.Column34.Name = "Column34"
        Me.Column34.ReadOnly = True
        Me.Column34.Width = 150
        '
        'Column35
        '
        Me.Column35.HeaderText = "TIN"
        Me.Column35.Name = "Column35"
        Me.Column35.ReadOnly = True
        '
        'Column36
        '
        Me.Column36.HeaderText = "BIN"
        Me.Column36.Name = "Column36"
        Me.Column36.ReadOnly = True
        '
        'Column37
        '
        Me.Column37.HeaderText = "Vat Regi No"
        Me.Column37.Name = "Column37"
        Me.Column37.ReadOnly = True
        Me.Column37.Width = 130
        '
        'Column38
        '
        Me.Column38.HeaderText = "Vat Regi Date"
        Me.Column38.Name = "Column38"
        Me.Column38.ReadOnly = True
        Me.Column38.Width = 120
        '
        'Column39
        '
        Me.Column39.HeaderText = "Company Regi No"
        Me.Column39.Name = "Column39"
        Me.Column39.ReadOnly = True
        Me.Column39.Width = 130
        '
        'Column40
        '
        Me.Column40.HeaderText = "Company Regi Date"
        Me.Column40.Name = "Column40"
        Me.Column40.ReadOnly = True
        Me.Column40.Width = 140
        '
        'Column41
        '
        Me.Column41.HeaderText = "Regi Authority"
        Me.Column41.Name = "Column41"
        Me.Column41.ReadOnly = True
        '
        'Column42
        '
        Me.Column42.HeaderText = "Present Address"
        Me.Column42.Name = "Column42"
        Me.Column42.ReadOnly = True
        Me.Column42.Width = 150
        '
        'Column43
        '
        Me.Column43.HeaderText = "Permanent Address"
        Me.Column43.Name = "Column43"
        Me.Column43.ReadOnly = True
        Me.Column43.Width = 150
        '
        'Column44
        '
        Me.Column44.HeaderText = "Phone Residence"
        Me.Column44.Name = "Column44"
        Me.Column44.ReadOnly = True
        Me.Column44.Width = 130
        '
        'Column45
        '
        Me.Column45.HeaderText = "Phone Office"
        Me.Column45.Name = "Column45"
        Me.Column45.ReadOnly = True
        '
        'Column46
        '
        Me.Column46.HeaderText = "Mobile No"
        Me.Column46.Name = "Column46"
        Me.Column46.ReadOnly = True
        Me.Column46.Width = 130
        '
        'Column47
        '
        Me.Column47.HeaderText = "Currency Type"
        Me.Column47.Name = "Column47"
        Me.Column47.ReadOnly = True
        Me.Column47.Width = 120
        '
        'Column48
        '
        Me.Column48.HeaderText = "Client Number"
        Me.Column48.Name = "Column48"
        Me.Column48.ReadOnly = True
        '
        'Column49
        '
        Me.Column49.HeaderText = "Account Type"
        Me.Column49.Name = "Column49"
        Me.Column49.ReadOnly = True
        Me.Column49.Width = 120
        '
        'Column50
        '
        Me.Column50.HeaderText = "Open Date"
        Me.Column50.Name = "Column50"
        Me.Column50.ReadOnly = True
        Me.Column50.Width = 150
        '
        'Column51
        '
        Me.Column51.HeaderText = "Close Date"
        Me.Column51.Name = "Column51"
        Me.Column51.ReadOnly = True
        '
        'Column52
        '
        Me.Column52.HeaderText = "Status Type"
        Me.Column52.Name = "Column52"
        Me.Column52.ReadOnly = True
        Me.Column52.Width = 140
        '
        'Column90
        '
        Me.Column90.HeaderText = "Beneficiary"
        Me.Column90.Name = "Column90"
        Me.Column90.ReadOnly = True
        '
        'Column91
        '
        Me.Column91.HeaderText = "Beneficiary Comments"
        Me.Column91.Name = "Column91"
        Me.Column91.ReadOnly = True
        Me.Column91.Width = 150
        '
        'Column53
        '
        Me.Column53.HeaderText = "Comments"
        Me.Column53.Name = "Column53"
        Me.Column53.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Is Error"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "Error Message"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 200
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'btnImportProcess
        '
        Me.btnImportProcess.Location = New System.Drawing.Point(337, 43)
        Me.btnImportProcess.Name = "btnImportProcess"
        Me.btnImportProcess.Size = New System.Drawing.Size(145, 27)
        Me.btnImportProcess.TabIndex = 1
        Me.btnImportProcess.Text = "Import Process"
        Me.btnImportProcess.UseVisualStyleBackColor = True
        '
        'txtFilename
        '
        Me.txtFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilename.Location = New System.Drawing.Point(9, 19)
        Me.txtFilename.Name = "txtFilename"
        Me.txtFilename.Size = New System.Drawing.Size(473, 20)
        Me.txtFilename.TabIndex = 1
        '
        'lblToolStatus
        '
        Me.lblToolStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblToolStatus.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblToolStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.lblToolStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToolStatus.Name = "lblToolStatus"
        Me.lblToolStatus.Size = New System.Drawing.Size(881, 17)
        Me.lblToolStatus.Spring = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.Transparent
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblToolStatus, Me.ProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 471)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(998, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 19
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(165, 43)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(145, 27)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.dgView)
        Me.GroupBox8.Location = New System.Drawing.Point(12, 90)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(977, 371)
        Me.GroupBox8.TabIndex = 20
        Me.GroupBox8.TabStop = False
        '
        'opdRetFile
        '
        Me.opdRetFile.FileName = "OpenFileDialog1"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnImportProcess)
        Me.GroupBox2.Controls.Add(Me.txtFilename)
        Me.GroupBox2.Controls.Add(Me.btnBrowse)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(497, 77)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Import File"
        '
        'btnPrepareAccount
        '
        Me.btnPrepareAccount.Location = New System.Drawing.Point(659, 27)
        Me.btnPrepareAccount.Name = "btnPrepareAccount"
        Me.btnPrepareAccount.Size = New System.Drawing.Size(145, 27)
        Me.btnPrepareAccount.TabIndex = 21
        Me.btnPrepareAccount.Text = "Prepare Account Info"
        Me.btnPrepareAccount.UseVisualStyleBackColor = True
        '
        'lblTotRecNo
        '
        Me.lblTotRecNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotRecNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRecNo.Location = New System.Drawing.Point(907, 62)
        Me.lblTotRecNo.Name = "lblTotRecNo"
        Me.lblTotRecNo.Size = New System.Drawing.Size(82, 20)
        Me.lblTotRecNo.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(819, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 20)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Total Rec No:"
        '
        'FrmImportAccountInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(998, 493)
        Me.Controls.Add(Me.lblTotRecNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnPrepareAccount)
        Me.Name = "FrmImportAccountInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import Account Information"
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgView As System.Windows.Forms.DataGridView
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents btnImportProcess As System.Windows.Forms.Button
    Friend WithEvents txtFilename As System.Windows.Forms.TextBox
    Friend WithEvents lblToolStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents opdRetFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrepareAccount As System.Windows.Forms.Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column88 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column36 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column37 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column38 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column39 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column40 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column41 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column42 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column43 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column44 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column45 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column46 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column47 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column48 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column49 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column50 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column51 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column52 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column90 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column91 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column53 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblTotRecNo As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
