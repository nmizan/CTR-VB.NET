<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmImportOwnerInfo
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnImportProcess = New System.Windows.Forms.Button
        Me.txtFilename = New System.Windows.Forms.TextBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.btnPrepareOwner = New System.Windows.Forms.Button
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.lblToolStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.opdRetFile = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.dgView = New System.Windows.Forms.DataGridView
        Me.Column26 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column27 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column28 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column29 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column30 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column31 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column32 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column33 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column34 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column35 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column36 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column37 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column38 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column19 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column39 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column20 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column21 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column40 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column22 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column23 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column24 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column41 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column42 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column25 = New System.Windows.Forms.DataGridViewTextBoxColumn
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
        Me.Column53 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column54 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column55 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column56 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column57 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column58 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column59 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column60 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column61 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column62 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column63 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column64 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column65 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column66 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column67 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column68 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column69 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column70 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column71 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column72 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column73 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column74 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column75 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column76 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column77 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblTotRecNo = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnImportProcess)
        Me.GroupBox2.Controls.Add(Me.txtFilename)
        Me.GroupBox2.Controls.Add(Me.btnBrowse)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(497, 77)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Import File"
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
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(165, 43)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(145, 27)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'btnPrepareOwner
        '
        Me.btnPrepareOwner.Location = New System.Drawing.Point(659, 22)
        Me.btnPrepareOwner.Name = "btnPrepareOwner"
        Me.btnPrepareOwner.Size = New System.Drawing.Size(145, 27)
        Me.btnPrepareOwner.TabIndex = 17
        Me.btnPrepareOwner.Text = "Prepare Owner Info"
        Me.btnPrepareOwner.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(100, 16)
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
        'opdRetFile
        '
        Me.opdRetFile.FileName = "OpenFileDialog1"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.dgView)
        Me.GroupBox8.Location = New System.Drawing.Point(12, 92)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(977, 371)
        Me.GroupBox8.TabIndex = 16
        Me.GroupBox8.TabStop = False
        '
        'dgView
        '
        Me.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column26, Me.Column27, Me.Column28, Me.Column1, Me.Column29, Me.Column30, Me.Column31, Me.Column32, Me.Column33, Me.Column2, Me.Column34, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column8, Me.Column10, Me.Column11, Me.Column12, Me.Column35, Me.Column36, Me.Column13, Me.Column14, Me.Column15, Me.Column16, Me.Column37, Me.Column17, Me.Column38, Me.Column18, Me.Column19, Me.Column39, Me.Column20, Me.Column21, Me.Column40, Me.Column22, Me.Column23, Me.Column24, Me.Column41, Me.Column42, Me.Column25, Me.Column43, Me.Column44, Me.Column45, Me.Column46, Me.Column47, Me.Column48, Me.Column49, Me.Column50, Me.Column51, Me.Column52, Me.Column53, Me.Column54, Me.Column55, Me.Column56, Me.Column57, Me.Column58, Me.Column59, Me.Column60, Me.Column61, Me.Column62, Me.Column63, Me.Column64, Me.Column65, Me.Column66, Me.Column67, Me.Column68, Me.Column69, Me.Column70, Me.Column71, Me.Column72, Me.Column73, Me.Column74, Me.Column75, Me.Column76, Me.Column77, Me.Column7, Me.Column9})
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
        'Column26
        '
        Me.Column26.HeaderText = "Record Group"
        Me.Column26.Name = "Column26"
        Me.Column26.ReadOnly = True
        '
        'Column27
        '
        Me.Column27.HeaderText = "Mapping Flag"
        Me.Column27.Name = "Column27"
        Me.Column27.ReadOnly = True
        '
        'Column28
        '
        Me.Column28.HeaderText = "Branch Code"
        Me.Column28.Name = "Column28"
        Me.Column28.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Owner Code"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column29
        '
        Me.Column29.HeaderText = "Account No"
        Me.Column29.Name = "Column29"
        Me.Column29.ReadOnly = True
        '
        'Column30
        '
        Me.Column30.HeaderText = "Executive Designation"
        Me.Column30.Name = "Column30"
        Me.Column30.ReadOnly = True
        Me.Column30.Width = 150
        '
        'Column31
        '
        Me.Column31.HeaderText = "Sign Auth"
        Me.Column31.Name = "Column31"
        Me.Column31.ReadOnly = True
        '
        'Column32
        '
        Me.Column32.HeaderText = "Role"
        Me.Column32.Name = "Column32"
        Me.Column32.ReadOnly = True
        '
        'Column33
        '
        Me.Column33.HeaderText = "Is Primary"
        Me.Column33.Name = "Column33"
        Me.Column33.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Gender"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column34
        '
        Me.Column34.HeaderText = "Title"
        Me.Column34.Name = "Column34"
        Me.Column34.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "First Name"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Middle Name"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Last Name"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "Fathers Name"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "Mothers name"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column10
        '
        Me.Column10.HeaderText = "Spouse Name"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'Column11
        '
        Me.Column11.HeaderText = "DOB"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        '
        'Column12
        '
        Me.Column12.HeaderText = "Nationality1"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        '
        'Column35
        '
        Me.Column35.HeaderText = "Nationality2"
        Me.Column35.Name = "Column35"
        Me.Column35.ReadOnly = True
        '
        'Column36
        '
        Me.Column36.HeaderText = "Nationality3"
        Me.Column36.Name = "Column36"
        Me.Column36.ReadOnly = True
        '
        'Column13
        '
        Me.Column13.HeaderText = "Residence"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        '
        'Column14
        '
        Me.Column14.HeaderText = "Occupation "
        Me.Column14.Name = "Column14"
        Me.Column14.ReadOnly = True
        '
        'Column15
        '
        Me.Column15.HeaderText = "Phone Contact Type"
        Me.Column15.Name = "Column15"
        Me.Column15.ReadOnly = True
        '
        'Column16
        '
        Me.Column16.HeaderText = "Phone Comm. Type"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        '
        'Column37
        '
        Me.Column37.HeaderText = "Country Prefix"
        Me.Column37.Name = "Column37"
        Me.Column37.ReadOnly = True
        '
        'Column17
        '
        Me.Column17.HeaderText = "Phone Number"
        Me.Column17.Name = "Column17"
        Me.Column17.ReadOnly = True
        '
        'Column38
        '
        Me.Column38.HeaderText = "Extension"
        Me.Column38.Name = "Column38"
        Me.Column38.ReadOnly = True
        '
        'Column18
        '
        Me.Column18.HeaderText = "Address Type Code"
        Me.Column18.Name = "Column18"
        Me.Column18.ReadOnly = True
        '
        'Column19
        '
        Me.Column19.HeaderText = "Address"
        Me.Column19.Name = "Column19"
        Me.Column19.ReadOnly = True
        '
        'Column39
        '
        Me.Column39.HeaderText = "Town/Thana"
        Me.Column39.Name = "Column39"
        Me.Column39.ReadOnly = True
        '
        'Column20
        '
        Me.Column20.HeaderText = "City/District"
        Me.Column20.Name = "Column20"
        Me.Column20.ReadOnly = True
        '
        'Column21
        '
        Me.Column21.HeaderText = "Country Code"
        Me.Column21.Name = "Column21"
        Me.Column21.ReadOnly = True
        '
        'Column40
        '
        Me.Column40.HeaderText = "Zip"
        Me.Column40.Name = "Column40"
        Me.Column40.ReadOnly = True
        '
        'Column22
        '
        Me.Column22.HeaderText = "State/Division"
        Me.Column22.Name = "Column22"
        Me.Column22.ReadOnly = True
        '
        'Column23
        '
        Me.Column23.HeaderText = "Indentification Type"
        Me.Column23.Name = "Column23"
        Me.Column23.ReadOnly = True
        '
        'Column24
        '
        Me.Column24.HeaderText = "Identification Number"
        Me.Column24.Name = "Column24"
        Me.Column24.ReadOnly = True
        '
        'Column41
        '
        Me.Column41.HeaderText = "Issue Date"
        Me.Column41.Name = "Column41"
        Me.Column41.ReadOnly = True
        '
        'Column42
        '
        Me.Column42.HeaderText = "Expiry Date"
        Me.Column42.Name = "Column42"
        Me.Column42.ReadOnly = True
        '
        'Column25
        '
        Me.Column25.HeaderText = "Issue Country Code"
        Me.Column25.Name = "Column25"
        Me.Column25.ReadOnly = True
        '
        'Column43
        '
        Me.Column43.HeaderText = "Issued By"
        Me.Column43.Name = "Column43"
        Me.Column43.ReadOnly = True
        '
        'Column44
        '
        Me.Column44.HeaderText = "National ID"
        Me.Column44.Name = "Column44"
        Me.Column44.ReadOnly = True
        '
        'Column45
        '
        Me.Column45.HeaderText = "Passport No"
        Me.Column45.Name = "Column45"
        Me.Column45.ReadOnly = True
        '
        'Column46
        '
        Me.Column46.HeaderText = "Birth Reg No"
        Me.Column46.Name = "Column46"
        Me.Column46.ReadOnly = True
        '
        'Column47
        '
        Me.Column47.HeaderText = "Birth Place"
        Me.Column47.Name = "Column47"
        Me.Column47.ReadOnly = True
        '
        'Column48
        '
        Me.Column48.HeaderText = "Employer Name"
        Me.Column48.Name = "Column48"
        Me.Column48.ReadOnly = True
        '
        'Column49
        '
        Me.Column49.HeaderText = "Employer Contact Type"
        Me.Column49.Name = "Column49"
        Me.Column49.ReadOnly = True
        '
        'Column50
        '
        Me.Column50.HeaderText = "Emp Comm Type"
        Me.Column50.Name = "Column50"
        Me.Column50.ReadOnly = True
        '
        'Column51
        '
        Me.Column51.HeaderText = "Country Prefix"
        Me.Column51.Name = "Column51"
        Me.Column51.ReadOnly = True
        '
        'Column52
        '
        Me.Column52.HeaderText = "Emp Phn Number"
        Me.Column52.Name = "Column52"
        Me.Column52.ReadOnly = True
        '
        'Column53
        '
        Me.Column53.HeaderText = "Emp Extension"
        Me.Column53.Name = "Column53"
        Me.Column53.ReadOnly = True
        '
        'Column54
        '
        Me.Column54.HeaderText = "Address Type"
        Me.Column54.Name = "Column54"
        Me.Column54.ReadOnly = True
        '
        'Column55
        '
        Me.Column55.HeaderText = "Address"
        Me.Column55.Name = "Column55"
        Me.Column55.ReadOnly = True
        '
        'Column56
        '
        Me.Column56.HeaderText = "Town/Thana"
        Me.Column56.Name = "Column56"
        Me.Column56.ReadOnly = True
        '
        'Column57
        '
        Me.Column57.HeaderText = "City/District"
        Me.Column57.Name = "Column57"
        Me.Column57.ReadOnly = True
        '
        'Column58
        '
        Me.Column58.HeaderText = "Country Code"
        Me.Column58.Name = "Column58"
        Me.Column58.ReadOnly = True
        '
        'Column59
        '
        Me.Column59.HeaderText = "Zip"
        Me.Column59.Name = "Column59"
        Me.Column59.ReadOnly = True
        '
        'Column60
        '
        Me.Column60.HeaderText = "State/Division"
        Me.Column60.Name = "Column60"
        Me.Column60.ReadOnly = True
        '
        'Column61
        '
        Me.Column61.HeaderText = "Tin"
        Me.Column61.Name = "Column61"
        Me.Column61.ReadOnly = True
        '
        'Column62
        '
        Me.Column62.HeaderText = "Bin"
        Me.Column62.Name = "Column62"
        Me.Column62.ReadOnly = True
        '
        'Column63
        '
        Me.Column63.HeaderText = "Occupation"
        Me.Column63.Name = "Column63"
        Me.Column63.ReadOnly = True
        '
        'Column64
        '
        Me.Column64.HeaderText = "Phone no"
        Me.Column64.Name = "Column64"
        Me.Column64.ReadOnly = True
        '
        'Column65
        '
        Me.Column65.HeaderText = "Phone City"
        Me.Column65.Name = "Column65"
        Me.Column65.ReadOnly = True
        '
        'Column66
        '
        Me.Column66.HeaderText = "Phone Country"
        Me.Column66.Name = "Column66"
        Me.Column66.ReadOnly = True
        '
        'Column67
        '
        Me.Column67.HeaderText = "Mobile no"
        Me.Column67.Name = "Column67"
        Me.Column67.ReadOnly = True
        '
        'Column68
        '
        Me.Column68.HeaderText = "Mobile City"
        Me.Column68.Name = "Column68"
        Me.Column68.ReadOnly = True
        '
        'Column69
        '
        Me.Column69.HeaderText = "Mobile Country"
        Me.Column69.Name = "Column69"
        Me.Column69.ReadOnly = True
        '
        'Column70
        '
        Me.Column70.HeaderText = "Office Phone No"
        Me.Column70.Name = "Column70"
        Me.Column70.ReadOnly = True
        '
        'Column71
        '
        Me.Column71.HeaderText = "Office Phone City"
        Me.Column71.Name = "Column71"
        Me.Column71.ReadOnly = True
        Me.Column71.Width = 120
        '
        'Column72
        '
        Me.Column72.HeaderText = "Office Phone Country"
        Me.Column72.Name = "Column72"
        Me.Column72.ReadOnly = True
        Me.Column72.Width = 150
        '
        'Column73
        '
        Me.Column73.HeaderText = "Present Address"
        Me.Column73.Name = "Column73"
        Me.Column73.ReadOnly = True
        '
        'Column74
        '
        Me.Column74.HeaderText = "Present Thana"
        Me.Column74.Name = "Column74"
        Me.Column74.ReadOnly = True
        '
        'Column75
        '
        Me.Column75.HeaderText = "Permanent Address"
        Me.Column75.Name = "Column75"
        Me.Column75.ReadOnly = True
        '
        'Column76
        '
        Me.Column76.HeaderText = "Permanent Thana"
        Me.Column76.Name = "Column76"
        Me.Column76.ReadOnly = True
        '
        'Column77
        '
        Me.Column77.HeaderText = "Driving Lisence"
        Me.Column77.Name = "Column77"
        Me.Column77.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "Is Error"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "Error Message"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 200
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.Transparent
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblToolStatus, Me.ProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 474)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(998, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 15
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblTotRecNo
        '
        Me.lblTotRecNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotRecNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRecNo.Location = New System.Drawing.Point(907, 69)
        Me.lblTotRecNo.Name = "lblTotRecNo"
        Me.lblTotRecNo.Size = New System.Drawing.Size(82, 20)
        Me.lblTotRecNo.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(819, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 20)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Total Rec No:"
        '
        'FrmImportOwnerInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(998, 496)
        Me.Controls.Add(Me.lblTotRecNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnPrepareOwner)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "FrmImportOwnerInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import Owner Information "
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnImportProcess As System.Windows.Forms.Button
    Friend WithEvents txtFilename As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents btnPrepareOwner As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblToolStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents opdRetFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents dgView As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Column26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column36 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column37 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column38 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column39 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column40 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column41 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column42 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column25 As System.Windows.Forms.DataGridViewTextBoxColumn
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
    Friend WithEvents Column53 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column54 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column55 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column56 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column57 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column58 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column59 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column60 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column61 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column62 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column63 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column64 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column65 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column66 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column67 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column68 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column69 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column70 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column71 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column72 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column73 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column74 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column75 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column76 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column77 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblTotRecNo As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
