<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDirectorSummary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDirectorSummary))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.btnExport = New System.Windows.Forms.Button
        Me.btnAuthorize = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.rdoUnauthorized = New System.Windows.Forms.RadioButton
        Me.chkShowAll = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnDetail = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.rdoAuthorized = New System.Windows.Forms.RadioButton
        Me.lblToolStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.chkAll = New System.Windows.Forms.CheckBox
        Me.dgView = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblTotRecNo = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column19 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column20 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column21 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column22 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column23 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column31 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column24 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column25 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column26 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column27 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column28 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column29 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column30 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column32 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column33 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.chkAccount = New System.Windows.Forms.CheckBox
        Me.GroupBox2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExport
        '
        Me.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExport.Location = New System.Drawing.Point(766, 12)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(77, 27)
        Me.btnExport.TabIndex = 24
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnAuthorize
        '
        Me.btnAuthorize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnAuthorize.Image = CType(resources.GetObject("btnAuthorize.Image"), System.Drawing.Image)
        Me.btnAuthorize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAuthorize.Location = New System.Drawing.Point(487, 12)
        Me.btnAuthorize.Name = "btnAuthorize"
        Me.btnAuthorize.Size = New System.Drawing.Size(77, 27)
        Me.btnAuthorize.TabIndex = 21
        Me.btnAuthorize.Text = "Authorize"
        Me.btnAuthorize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAuthorize.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(811, 13)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(50, 27)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'rdoUnauthorized
        '
        Me.rdoUnauthorized.AutoSize = True
        Me.rdoUnauthorized.Location = New System.Drawing.Point(184, 17)
        Me.rdoUnauthorized.Name = "rdoUnauthorized"
        Me.rdoUnauthorized.Size = New System.Drawing.Size(88, 17)
        Me.rdoUnauthorized.TabIndex = 22
        Me.rdoUnauthorized.Text = "Unauthorized"
        Me.rdoUnauthorized.UseVisualStyleBackColor = True
        '
        'chkShowAll
        '
        Me.chkShowAll.AutoSize = True
        Me.chkShowAll.Location = New System.Drawing.Point(245, 20)
        Me.chkShowAll.Name = "chkShowAll"
        Me.chkShowAll.Size = New System.Drawing.Size(67, 17)
        Me.chkShowAll.TabIndex = 3
        Me.chkShowAll.Text = "Show All"
        Me.chkShowAll.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkShowAll)
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnNew)
        Me.GroupBox2.Controls.Add(Me.btnDetail)
        Me.GroupBox2.Controls.Add(Me.btnRefresh)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(867, 48)
        Me.GroupBox2.TabIndex = 25
        Me.GroupBox2.TabStop = False
        '
        'btnNew
        '
        Me.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNew.Location = New System.Drawing.Point(6, 13)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(68, 27)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnDetail
        '
        Me.btnDetail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnDetail.Image = CType(resources.GetObject("btnDetail.Image"), System.Drawing.Image)
        Me.btnDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDetail.Location = New System.Drawing.Point(80, 13)
        Me.btnDetail.Name = "btnDetail"
        Me.btnDetail.Size = New System.Drawing.Size(68, 27)
        Me.btnDetail.TabIndex = 1
        Me.btnDetail.Text = "Detail"
        Me.btnDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDetail.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh.Location = New System.Drawing.Point(154, 13)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 27)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'rdoAuthorized
        '
        Me.rdoAuthorized.AutoSize = True
        Me.rdoAuthorized.Checked = True
        Me.rdoAuthorized.Location = New System.Drawing.Point(80, 17)
        Me.rdoAuthorized.Name = "rdoAuthorized"
        Me.rdoAuthorized.Size = New System.Drawing.Size(75, 17)
        Me.rdoAuthorized.TabIndex = 23
        Me.rdoAuthorized.TabStop = True
        Me.rdoAuthorized.Text = "Authorized"
        Me.rdoAuthorized.UseVisualStyleBackColor = True
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
        Me.lblToolStatus.Size = New System.Drawing.Size(852, 17)
        Me.lblToolStatus.Spring = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.Transparent
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblToolStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 504)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(867, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 24
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(20, 18)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(37, 17)
        Me.chkAll.TabIndex = 20
        Me.chkAll.Text = "All"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'dgView
        '
        Me.dgView.AllowUserToAddRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro
        Me.dgView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column5, Me.Column4, Me.Column2, Me.Column3, Me.Column11, Me.Column12, Me.Column13, Me.Column14, Me.Column6, Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.Column15, Me.Column16, Me.Column17, Me.Column18, Me.Column19, Me.Column20, Me.Column21, Me.Column22, Me.Column23, Me.Column31, Me.Column24, Me.Column25, Me.Column26, Me.Column27, Me.Column28, Me.Column29, Me.Column30, Me.Column32, Me.Column33})
        Me.dgView.Location = New System.Drawing.Point(20, 54)
        Me.dgView.MultiSelect = False
        Me.dgView.Name = "dgView"
        Me.dgView.RowHeadersVisible = False
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DarkSeaGreen
        Me.dgView.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgView.Size = New System.Drawing.Size(823, 380)
        Me.dgView.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkAccount)
        Me.GroupBox1.Controls.Add(Me.lblTotRecNo)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnExport)
        Me.GroupBox1.Controls.Add(Me.btnAuthorize)
        Me.GroupBox1.Controls.Add(Me.rdoUnauthorized)
        Me.GroupBox1.Controls.Add(Me.rdoAuthorized)
        Me.GroupBox1.Controls.Add(Me.chkAll)
        Me.GroupBox1.Controls.Add(Me.dgView)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 49)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(858, 449)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        '
        'lblTotRecNo
        '
        Me.lblTotRecNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotRecNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRecNo.Location = New System.Drawing.Point(647, 17)
        Me.lblTotRecNo.Name = "lblTotRecNo"
        Me.lblTotRecNo.Size = New System.Drawing.Size(82, 20)
        Me.lblTotRecNo.TabIndex = 33
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(588, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 20)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Total Rec:"
        '
        'Column1
        '
        Me.Column1.HeaderText = ""
        Me.Column1.Name = "Column1"
        Me.Column1.Visible = False
        Me.Column1.Width = 50
        '
        'Column5
        '
        Me.Column5.DataPropertyName = "MOD_NO"
        Me.Column5.HeaderText = "Mod No"
        Me.Column5.Name = "Column5"
        Me.Column5.Visible = False
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "S"
        Me.Column4.HeaderText = "S"
        Me.Column4.Name = "Column4"
        Me.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column4.Width = 25
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "DIRECTOR_ID"
        Me.Column2.HeaderText = "Director ID"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Gender"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 50
        '
        'Column11
        '
        Me.Column11.HeaderText = "First Name"
        Me.Column11.Name = "Column11"
        '
        'Column12
        '
        Me.Column12.HeaderText = "Last Name"
        Me.Column12.Name = "Column12"
        '
        'Column13
        '
        Me.Column13.HeaderText = "Mother Name"
        Me.Column13.Name = "Column13"
        '
        'Column14
        '
        Me.Column14.HeaderText = "Birth Regi No"
        Me.Column14.Name = "Column14"
        '
        'Column6
        '
        Me.Column6.HeaderText = "Input By"
        Me.Column6.Name = "Column6"
        '
        'Column7
        '
        Me.Column7.HeaderText = "Input Date"
        Me.Column7.Name = "Column7"
        '
        'Column8
        '
        Me.Column8.HeaderText = "input datetime"
        Me.Column8.Name = "Column8"
        Me.Column8.Visible = False
        '
        'Column9
        '
        Me.Column9.HeaderText = "Auth By"
        Me.Column9.Name = "Column9"
        '
        'Column10
        '
        Me.Column10.HeaderText = "Auth Date"
        Me.Column10.Name = "Column10"
        '
        'Column15
        '
        Me.Column15.HeaderText = "DOB"
        Me.Column15.Name = "Column15"
        '
        'Column16
        '
        Me.Column16.HeaderText = "Birth Place"
        Me.Column16.Name = "Column16"
        '
        'Column17
        '
        Me.Column17.HeaderText = "Spouse"
        Me.Column17.Name = "Column17"
        '
        'Column18
        '
        Me.Column18.HeaderText = "SSN"
        Me.Column18.Name = "Column18"
        '
        'Column19
        '
        Me.Column19.HeaderText = "Passport No"
        Me.Column19.Name = "Column19"
        '
        'Column20
        '
        Me.Column20.HeaderText = "Passport Country"
        Me.Column20.Name = "Column20"
        Me.Column20.Width = 140
        '
        'Column21
        '
        Me.Column21.HeaderText = "Nationality1"
        Me.Column21.Name = "Column21"
        '
        'Column22
        '
        Me.Column22.HeaderText = "Nationality2"
        Me.Column22.Name = "Column22"
        '
        'Column23
        '
        Me.Column23.HeaderText = "Nationality3"
        Me.Column23.Name = "Column23"
        '
        'Column31
        '
        Me.Column31.HeaderText = "Residence"
        Me.Column31.Name = "Column31"
        '
        'Column24
        '
        Me.Column24.HeaderText = "Email"
        Me.Column24.Name = "Column24"
        '
        'Column25
        '
        Me.Column25.HeaderText = "Occupation"
        Me.Column25.Name = "Column25"
        '
        'Column26
        '
        Me.Column26.HeaderText = "Employer Name"
        Me.Column26.Name = "Column26"
        Me.Column26.Width = 130
        '
        'Column27
        '
        Me.Column27.HeaderText = "Tax Number"
        Me.Column27.Name = "Column27"
        '
        'Column28
        '
        Me.Column28.HeaderText = "Tax regi Number"
        Me.Column28.Name = "Column28"
        Me.Column28.Width = 140
        '
        'Column29
        '
        Me.Column29.HeaderText = "source of Wealth"
        Me.Column29.Name = "Column29"
        Me.Column29.Width = 140
        '
        'Column30
        '
        Me.Column30.HeaderText = "Comments"
        Me.Column30.Name = "Column30"
        '
        'Column32
        '
        Me.Column32.HeaderText = "Entity ID"
        Me.Column32.Name = "Column32"
        '
        'Column33
        '
        Me.Column33.HeaderText = "Account"
        Me.Column33.Name = "Column33"
        '
        'chkAccount
        '
        Me.chkAccount.AutoSize = True
        Me.chkAccount.Location = New System.Drawing.Point(294, 19)
        Me.chkAccount.Name = "chkAccount"
        Me.chkAccount.Size = New System.Drawing.Size(163, 17)
        Me.chkAccount.TabIndex = 34
        Me.chkAccount.Text = "With Entity, Account Number"
        Me.chkAccount.UseVisualStyleBackColor = True
        '
        'FrmDirectorSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(867, 526)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "FrmDirectorSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Director Information Detail"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnAuthorize As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents rdoUnauthorized As System.Windows.Forms.RadioButton
    Friend WithEvents chkShowAll As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnDetail As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents rdoAuthorized As System.Windows.Forms.RadioButton
    Friend WithEvents lblToolStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents dgView As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotRecNo As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkAccount As System.Windows.Forms.CheckBox
End Class
