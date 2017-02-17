<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmExportGoAML
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmExportGoAML))
        Me.Label9 = New System.Windows.Forms.Label
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.txtSubmissionDate = New System.Windows.Forms.MaskedTextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtAction = New System.Windows.Forms.TextBox
        Me.txtIndicator = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtCurrency = New System.Windows.Forms.TextBox
        Me.txtReason = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtFiuRef = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtBranch = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtReporttype = New System.Windows.Forms.TextBox
        Me.txtSubmission = New System.Windows.Forms.TextBox
        Me.txtEntityRef = New System.Windows.Forms.TextBox
        Me.txtId = New System.Windows.Forms.TextBox
        Me.lblDept = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.btnExport = New System.Windows.Forms.Button
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnCheck = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtFolderPath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.fbdTransFile = New System.Windows.Forms.FolderBrowserDialog
        Me.StatusStrip2 = New System.Windows.Forms.StatusStrip
        Me.lblToolStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.txtProcessStatus = New System.Windows.Forms.RichTextBox
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtTransacrionNumber = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtXmlFileName = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.StatusStrip2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(35, 42)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 20)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Action :"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtSubmissionDate)
        Me.GroupBox6.Controls.Add(Me.Label7)
        Me.GroupBox6.Controls.Add(Me.Label14)
        Me.GroupBox6.Controls.Add(Me.txtAction)
        Me.GroupBox6.Controls.Add(Me.Label9)
        Me.GroupBox6.Controls.Add(Me.txtIndicator)
        Me.GroupBox6.Controls.Add(Me.Label16)
        Me.GroupBox6.Controls.Add(Me.txtCurrency)
        Me.GroupBox6.Controls.Add(Me.txtReason)
        Me.GroupBox6.Controls.Add(Me.Label15)
        Me.GroupBox6.Controls.Add(Me.Label22)
        Me.GroupBox6.Location = New System.Drawing.Point(4, 66)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(715, 65)
        Me.GroupBox6.TabIndex = 49
        Me.GroupBox6.TabStop = False
        '
        'txtSubmissionDate
        '
        Me.txtSubmissionDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubmissionDate.Location = New System.Drawing.Point(331, 38)
        Me.txtSubmissionDate.Mask = "00/00/0000"
        Me.txtSubmissionDate.Name = "txtSubmissionDate"
        Me.txtSubmissionDate.Size = New System.Drawing.Size(77, 20)
        Me.txtSubmissionDate.TabIndex = 32
        Me.txtSubmissionDate.ValidatingType = GetType(Date)
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(240, 40)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 20)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "Submission Date :"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(412, 42)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(78, 15)
        Me.Label14.TabIndex = 33
        Me.Label14.Text = "(dd/mm/yyyy)"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAction
        '
        Me.txtAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAction.Location = New System.Drawing.Point(88, 39)
        Me.txtAction.Name = "txtAction"
        Me.txtAction.Size = New System.Drawing.Size(141, 20)
        Me.txtAction.TabIndex = 18
        '
        'txtIndicator
        '
        Me.txtIndicator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIndicator.Location = New System.Drawing.Point(561, 11)
        Me.txtIndicator.Name = "txtIndicator"
        Me.txtIndicator.Size = New System.Drawing.Size(140, 20)
        Me.txtIndicator.TabIndex = 29
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(475, 13)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(94, 20)
        Me.Label16.TabIndex = 27
        Me.Label16.Text = "Report Indicator:"
        '
        'txtCurrency
        '
        Me.txtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrency.Location = New System.Drawing.Point(88, 13)
        Me.txtCurrency.Name = "txtCurrency"
        Me.txtCurrency.Size = New System.Drawing.Size(141, 20)
        Me.txtCurrency.TabIndex = 31
        '
        'txtReason
        '
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Location = New System.Drawing.Point(311, 12)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(147, 20)
        Me.txtReason.TabIndex = 29
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(249, 14)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 20)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Reason :"
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(2, 17)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(94, 20)
        Me.Label22.TabIndex = 23
        Me.Label22.Text = "Currency Type:"
        '
        'txtFiuRef
        '
        Me.txtFiuRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFiuRef.Location = New System.Drawing.Point(561, 38)
        Me.txtFiuRef.Name = "txtFiuRef"
        Me.txtFiuRef.Size = New System.Drawing.Size(141, 20)
        Me.txtFiuRef.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(479, 42)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(64, 20)
        Me.Label13.TabIndex = 15
        Me.Label13.Text = "FIU Ref :"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(6, 39)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(94, 20)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Report Type:"
        '
        'txtBranch
        '
        Me.txtBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBranch.Location = New System.Drawing.Point(314, 12)
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.Size = New System.Drawing.Size(145, 20)
        Me.txtBranch.TabIndex = 10
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(232, 15)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 20)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Rentity Branch:"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(464, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 20)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Submission Type:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtReporttype)
        Me.GroupBox2.Controls.Add(Me.txtSubmission)
        Me.GroupBox2.Controls.Add(Me.txtFiuRef)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtBranch)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtEntityRef)
        Me.GroupBox2.Controls.Add(Me.txtId)
        Me.GroupBox2.Controls.Add(Me.lblDept)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(716, 66)
        Me.GroupBox2.TabIndex = 47
        Me.GroupBox2.TabStop = False
        '
        'txtReporttype
        '
        Me.txtReporttype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReporttype.Location = New System.Drawing.Point(89, 38)
        Me.txtReporttype.Name = "txtReporttype"
        Me.txtReporttype.Size = New System.Drawing.Size(140, 20)
        Me.txtReporttype.TabIndex = 17
        '
        'txtSubmission
        '
        Me.txtSubmission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubmission.Location = New System.Drawing.Point(561, 12)
        Me.txtSubmission.Name = "txtSubmission"
        Me.txtSubmission.Size = New System.Drawing.Size(141, 20)
        Me.txtSubmission.TabIndex = 16
        '
        'txtEntityRef
        '
        Me.txtEntityRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEntityRef.Location = New System.Drawing.Point(314, 38)
        Me.txtEntityRef.Name = "txtEntityRef"
        Me.txtEntityRef.Size = New System.Drawing.Size(145, 20)
        Me.txtEntityRef.TabIndex = 1
        '
        'txtId
        '
        Me.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtId.Location = New System.Drawing.Point(89, 13)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(140, 20)
        Me.txtId.TabIndex = 0
        '
        'lblDept
        '
        Me.lblDept.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.Location = New System.Drawing.Point(237, 40)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(68, 20)
        Me.lblDept.TabIndex = 5
        Me.lblDept.Text = "Entity Ref :"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(79, 20)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Rentity ID:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtDateTo)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtDateFrom)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label40)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 191)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(714, 45)
        Me.GroupBox1.TabIndex = 50
        Me.GroupBox1.TabStop = False
        '
        'txtDateTo
        '
        Me.txtDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateTo.Location = New System.Drawing.Point(397, 14)
        Me.txtDateTo.Mask = "00/00/0000"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(77, 20)
        Me.txtDateTo.TabIndex = 26
        Me.txtDateTo.ValidatingType = GetType(Date)
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(331, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 20)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "Date To :"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(475, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 15)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "(dd/mm/yyyy)"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDateFrom
        '
        Me.txtDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateFrom.Location = New System.Drawing.Point(151, 14)
        Me.txtDateFrom.Mask = "00/00/0000"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(77, 20)
        Me.txtDateFrom.TabIndex = 23
        Me.txtDateFrom.ValidatingType = GetType(Date)
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(85, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 20)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Date From :"
        '
        'Label34
        '
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(228, 18)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(78, 15)
        Me.Label34.TabIndex = 24
        Me.Label34.Text = "(dd/mm/yyyy)"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(553, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 19)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "*"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(304, 13)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(15, 19)
        Me.Label40.TabIndex = 12
        Me.Label40.Text = "*"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExport
        '
        Me.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnExport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExport.Location = New System.Drawing.Point(376, 44)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(68, 27)
        Me.btnExport.TabIndex = 2
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBrowse.Location = New System.Drawing.Point(77, 43)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(89, 27)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "Select Folder"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnCheck)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.btnExport)
        Me.GroupBox3.Controls.Add(Me.btnBrowse)
        Me.GroupBox3.Controls.Add(Me.txtFolderPath)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(78, 237)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(473, 76)
        Me.GroupBox3.TabIndex = 51
        Me.GroupBox3.TabStop = False
        '
        'btnCheck
        '
        Me.btnCheck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnCheck.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCheck.Location = New System.Drawing.Point(234, 43)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(96, 27)
        Me.btnCheck.TabIndex = 15
        Me.btnCheck.Text = "Missing Field"
        Me.btnCheck.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(445, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(15, 19)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "*"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtFolderPath
        '
        Me.txtFolderPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFolderPath.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolderPath.Location = New System.Drawing.Point(77, 17)
        Me.txtFolderPath.Name = "txtFolderPath"
        Me.txtFolderPath.Size = New System.Drawing.Size(367, 21)
        Me.txtFolderPath.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Export Loc:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnExit
        '
        Me.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(614, 262)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(50, 27)
        Me.btnExit.TabIndex = 52
        Me.btnExit.Text = "Exit"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'StatusStrip2
        '
        Me.StatusStrip2.BackColor = System.Drawing.Color.Transparent
        Me.StatusStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblToolStatus, Me.ProgressBar1})
        Me.StatusStrip2.Location = New System.Drawing.Point(0, 410)
        Me.StatusStrip2.Name = "StatusStrip2"
        Me.StatusStrip2.Size = New System.Drawing.Size(722, 22)
        Me.StatusStrip2.SizingGrip = False
        Me.StatusStrip2.TabIndex = 53
        Me.StatusStrip2.Text = "StatusStrip2"
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
        Me.lblToolStatus.Size = New System.Drawing.Size(605, 17)
        Me.lblToolStatus.Spring = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'BackgroundWorker1
        '
        '
        'txtProcessStatus
        '
        Me.txtProcessStatus.Location = New System.Drawing.Point(4, 319)
        Me.txtProcessStatus.Name = "txtProcessStatus"
        Me.txtProcessStatus.ReadOnly = True
        Me.txtProcessStatus.Size = New System.Drawing.Size(715, 86)
        Me.txtProcessStatus.TabIndex = 54
        Me.txtProcessStatus.Text = ""
        '
        'BackgroundWorker2
        '
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.txtXmlFileName)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.txtTransacrionNumber)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Location = New System.Drawing.Point(5, 132)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(714, 59)
        Me.GroupBox4.TabIndex = 55
        Me.GroupBox4.TabStop = False
        '
        'txtTransacrionNumber
        '
        Me.txtTransacrionNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransacrionNumber.Location = New System.Drawing.Point(137, 19)
        Me.txtTransacrionNumber.Name = "txtTransacrionNumber"
        Me.txtTransacrionNumber.Size = New System.Drawing.Size(141, 20)
        Me.txtTransacrionNumber.TabIndex = 20
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(15, 22)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(142, 20)
        Me.Label17.TabIndex = 21
        Me.Label17.Text = "Number Of Transaction :"
        '
        'txtXmlFileName
        '
        Me.txtXmlFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtXmlFileName.Location = New System.Drawing.Point(362, 19)
        Me.txtXmlFileName.Name = "txtXmlFileName"
        Me.txtXmlFileName.Size = New System.Drawing.Size(141, 20)
        Me.txtXmlFileName.TabIndex = 22
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(280, 22)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(142, 20)
        Me.Label18.TabIndex = 23
        Me.Label18.Text = "Xml File Name :"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(518, 21)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(142, 20)
        Me.Label19.TabIndex = 24
        Me.Label19.Text = "N.B : Citi Bank"
        '
        'FrmExportGoAML
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 432)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.txtProcessStatus)
        Me.Controls.Add(Me.StatusStrip2)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "FrmExportGoAML"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Export File ( goAML )"
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.StatusStrip2.ResumeLayout(False)
        Me.StatusStrip2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtFiuRef As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtIndicator As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtAction As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtBranch As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEntityRef As System.Windows.Forms.TextBox
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents lblDept As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtReporttype As System.Windows.Forms.TextBox
    Friend WithEvents txtSubmission As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrency As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFolderPath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents txtSubmissionDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents fbdTransFile As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents StatusStrip2 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblToolStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents txtProcessStatus As System.Windows.Forms.RichTextBox
    Friend WithEvents btnCheck As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTransacrionNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtXmlFileName As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
End Class
