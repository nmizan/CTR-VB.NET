<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmImportFlexCustGo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmImportFlexCustGo))
        Me.btnExit = New System.Windows.Forms.Button
        Me.StatusStrip2 = New System.Windows.Forms.StatusStrip
        Me.lblToolStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtFolderPath = New System.Windows.Forms.TextBox
        Me.btnImportChecker = New System.Windows.Forms.Button
        Me.btnImportMaker = New System.Windows.Forms.Button
        Me.btnSetFolder = New System.Windows.Forms.Button
        Me.fbdTransFile = New System.Windows.Forms.FolderBrowserDialog
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtFileImpStatus = New System.Windows.Forms.RichTextBox
        Me.lblTotRecNo = New System.Windows.Forms.Label
        Me.lblFaileFileNo = New System.Windows.Forms.Label
        Me.lblTotFile = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker
        Me.StatusStrip2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(631, 236)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(50, 27)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'StatusStrip2
        '
        Me.StatusStrip2.BackColor = System.Drawing.Color.Transparent
        Me.StatusStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblToolStatus, Me.ProgressBar1})
        Me.StatusStrip2.Location = New System.Drawing.Point(0, 356)
        Me.StatusStrip2.Name = "StatusStrip2"
        Me.StatusStrip2.Size = New System.Drawing.Size(705, 22)
        Me.StatusStrip2.SizingGrip = False
        Me.StatusStrip2.TabIndex = 11
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
        Me.lblToolStatus.Size = New System.Drawing.Size(557, 17)
        Me.lblToolStatus.Spring = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtFolderPath)
        Me.GroupBox2.Controls.Add(Me.btnImportChecker)
        Me.GroupBox2.Controls.Add(Me.btnImportMaker)
        Me.GroupBox2.Controls.Add(Me.btnSetFolder)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 1)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(695, 83)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Flex Customer File Location (Multiple File)"
        '
        'txtFolderPath
        '
        Me.txtFolderPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFolderPath.Location = New System.Drawing.Point(10, 19)
        Me.txtFolderPath.Name = "txtFolderPath"
        Me.txtFolderPath.Size = New System.Drawing.Size(671, 20)
        Me.txtFolderPath.TabIndex = 2
        '
        'btnImportChecker
        '
        Me.btnImportChecker.Location = New System.Drawing.Point(526, 45)
        Me.btnImportChecker.Name = "btnImportChecker"
        Me.btnImportChecker.Size = New System.Drawing.Size(145, 27)
        Me.btnImportChecker.TabIndex = 0
        Me.btnImportChecker.Text = "Authorizer Import"
        Me.btnImportChecker.UseVisualStyleBackColor = True
        '
        'btnImportMaker
        '
        Me.btnImportMaker.Location = New System.Drawing.Point(352, 45)
        Me.btnImportMaker.Name = "btnImportMaker"
        Me.btnImportMaker.Size = New System.Drawing.Size(145, 27)
        Me.btnImportMaker.TabIndex = 0
        Me.btnImportMaker.Text = "Maker Import"
        Me.btnImportMaker.UseVisualStyleBackColor = True
        '
        'btnSetFolder
        '
        Me.btnSetFolder.Location = New System.Drawing.Point(182, 45)
        Me.btnSetFolder.Name = "btnSetFolder"
        Me.btnSetFolder.Size = New System.Drawing.Size(145, 27)
        Me.btnSetFolder.TabIndex = 0
        Me.btnSetFolder.Text = "Set Folder"
        Me.btnSetFolder.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtFileImpStatus)
        Me.GroupBox4.Controls.Add(Me.lblTotRecNo)
        Me.GroupBox4.Controls.Add(Me.lblFaileFileNo)
        Me.GroupBox4.Controls.Add(Me.btnExit)
        Me.GroupBox4.Controls.Add(Me.lblTotFile)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Location = New System.Drawing.Point(4, 84)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(695, 269)
        Me.GroupBox4.TabIndex = 13
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "File Import Status"
        '
        'txtFileImpStatus
        '
        Me.txtFileImpStatus.Location = New System.Drawing.Point(6, 43)
        Me.txtFileImpStatus.Name = "txtFileImpStatus"
        Me.txtFileImpStatus.ReadOnly = True
        Me.txtFileImpStatus.Size = New System.Drawing.Size(683, 187)
        Me.txtFileImpStatus.TabIndex = 20
        Me.txtFileImpStatus.Text = ""
        '
        'lblTotRecNo
        '
        Me.lblTotRecNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotRecNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRecNo.Location = New System.Drawing.Point(415, 20)
        Me.lblTotRecNo.Name = "lblTotRecNo"
        Me.lblTotRecNo.Size = New System.Drawing.Size(82, 20)
        Me.lblTotRecNo.TabIndex = 19
        '
        'lblFaileFileNo
        '
        Me.lblFaileFileNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFaileFileNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFaileFileNo.Location = New System.Drawing.Point(231, 20)
        Me.lblFaileFileNo.Name = "lblFaileFileNo"
        Me.lblFaileFileNo.Size = New System.Drawing.Size(82, 20)
        Me.lblFaileFileNo.TabIndex = 19
        '
        'lblTotFile
        '
        Me.lblTotFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotFile.Location = New System.Drawing.Point(65, 20)
        Me.lblTotFile.Name = "lblTotFile"
        Me.lblTotFile.Size = New System.Drawing.Size(82, 20)
        Me.lblTotFile.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(327, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 20)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Total Rec No:"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(163, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 20)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Err File No:"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Total File:"
        '
        'BackgroundWorker1
        '
        '
        'BackgroundWorker2
        '
        '
        'FrmImportFlexCustGo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 378)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.StatusStrip2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmImportFlexCustGo"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Flex Customer"
        Me.StatusStrip2.ResumeLayout(False)
        Me.StatusStrip2.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents StatusStrip2 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblToolStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFolderPath As System.Windows.Forms.TextBox
    Friend WithEvents btnImportMaker As System.Windows.Forms.Button
    Friend WithEvents btnSetFolder As System.Windows.Forms.Button
    Friend WithEvents fbdTransFile As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotRecNo As System.Windows.Forms.Label
    Friend WithEvents lblFaileFileNo As System.Windows.Forms.Label
    Friend WithEvents lblTotFile As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents txtFileImpStatus As System.Windows.Forms.RichTextBox
    Friend WithEvents btnImportChecker As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
End Class
