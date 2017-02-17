<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAppSettting
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtDatabase = New System.Windows.Forms.TextBox
        Me.txtServer = New System.Windows.Forms.TextBox
        Me.txtDomain = New System.Windows.Forms.TextBox
        Me.txtConnString = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rbtnPROD = New System.Windows.Forms.RadioButton
        Me.rbtnUAT = New System.Windows.Forms.RadioButton
        Me.rbtnDEV = New System.Windows.Forms.RadioButton
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtUATDatabase = New System.Windows.Forms.TextBox
        Me.txtUATServer = New System.Windows.Forms.TextBox
        Me.txtUATDomain = New System.Windows.Forms.TextBox
        Me.txtUATConnString = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.txtDEVDatabase = New System.Windows.Forms.TextBox
        Me.txtDEVServer = New System.Windows.Forms.TextBox
        Me.txtDEVDomain = New System.Windows.Forms.TextBox
        Me.txtDEVConnString = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.txtDatabase)
        Me.GroupBox1.Controls.Add(Me.txtServer)
        Me.GroupBox1.Controls.Add(Me.txtDomain)
        Me.GroupBox1.Controls.Add(Me.txtConnString)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(559, 138)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "PROD"
        '
        'txtDatabase
        '
        Me.txtDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDatabase.Location = New System.Drawing.Point(133, 43)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(408, 20)
        Me.txtDatabase.TabIndex = 1
        '
        'txtServer
        '
        Me.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServer.Location = New System.Drawing.Point(133, 20)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(408, 20)
        Me.txtServer.TabIndex = 0
        '
        'txtDomain
        '
        Me.txtDomain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDomain.Location = New System.Drawing.Point(133, 66)
        Me.txtDomain.Name = "txtDomain"
        Me.txtDomain.Size = New System.Drawing.Size(408, 20)
        Me.txtDomain.TabIndex = 2
        '
        'txtConnString
        '
        Me.txtConnString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConnString.Location = New System.Drawing.Point(133, 89)
        Me.txtConnString.Multiline = True
        Me.txtConnString.Name = "txtConnString"
        Me.txtConnString.Size = New System.Drawing.Size(407, 33)
        Me.txtConnString.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 23)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Database:"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(123, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Server:"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(123, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Domain:"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(123, 23)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Connection String:"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.btnClose)
        Me.GroupBox2.Controls.Add(Me.btnSave)
        Me.GroupBox2.Location = New System.Drawing.Point(274, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(290, 47)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.Location = New System.Drawing.Point(150, 16)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(82, 23)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.Location = New System.Drawing.Point(48, 16)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(82, 23)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbtnDEV)
        Me.GroupBox3.Controls.Add(Me.rbtnUAT)
        Me.GroupBox3.Controls.Add(Me.rbtnPROD)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(263, 47)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Default"
        '
        'rbtnPROD
        '
        Me.rbtnPROD.AutoSize = True
        Me.rbtnPROD.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnPROD.Location = New System.Drawing.Point(20, 19)
        Me.rbtnPROD.Name = "rbtnPROD"
        Me.rbtnPROD.Size = New System.Drawing.Size(55, 17)
        Me.rbtnPROD.TabIndex = 0
        Me.rbtnPROD.TabStop = True
        Me.rbtnPROD.Text = "PROD"
        Me.rbtnPROD.UseVisualStyleBackColor = True
        '
        'rbtnUAT
        '
        Me.rbtnUAT.AutoSize = True
        Me.rbtnUAT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnUAT.Location = New System.Drawing.Point(105, 19)
        Me.rbtnUAT.Name = "rbtnUAT"
        Me.rbtnUAT.Size = New System.Drawing.Size(46, 17)
        Me.rbtnUAT.TabIndex = 1
        Me.rbtnUAT.TabStop = True
        Me.rbtnUAT.Text = "UAT"
        Me.rbtnUAT.UseVisualStyleBackColor = True
        '
        'rbtnDEV
        '
        Me.rbtnDEV.AutoSize = True
        Me.rbtnDEV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnDEV.Location = New System.Drawing.Point(176, 19)
        Me.rbtnDEV.Name = "rbtnDEV"
        Me.rbtnDEV.Size = New System.Drawing.Size(46, 17)
        Me.rbtnDEV.TabIndex = 2
        Me.rbtnDEV.TabStop = True
        Me.rbtnDEV.Text = "DEV"
        Me.rbtnDEV.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.txtUATDatabase)
        Me.GroupBox4.Controls.Add(Me.txtUATServer)
        Me.GroupBox4.Controls.Add(Me.txtUATDomain)
        Me.GroupBox4.Controls.Add(Me.txtUATConnString)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Location = New System.Drawing.Point(5, 198)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(559, 138)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "UAT"
        '
        'txtUATDatabase
        '
        Me.txtUATDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUATDatabase.Location = New System.Drawing.Point(133, 43)
        Me.txtUATDatabase.Name = "txtUATDatabase"
        Me.txtUATDatabase.Size = New System.Drawing.Size(408, 20)
        Me.txtUATDatabase.TabIndex = 1
        '
        'txtUATServer
        '
        Me.txtUATServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUATServer.Location = New System.Drawing.Point(133, 20)
        Me.txtUATServer.Name = "txtUATServer"
        Me.txtUATServer.Size = New System.Drawing.Size(408, 20)
        Me.txtUATServer.TabIndex = 0
        '
        'txtUATDomain
        '
        Me.txtUATDomain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUATDomain.Location = New System.Drawing.Point(133, 66)
        Me.txtUATDomain.Name = "txtUATDomain"
        Me.txtUATDomain.Size = New System.Drawing.Size(408, 20)
        Me.txtUATDomain.TabIndex = 2
        '
        'txtUATConnString
        '
        Me.txtUATConnString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUATConnString.Location = New System.Drawing.Point(133, 89)
        Me.txtUATConnString.Multiline = True
        Me.txtUATConnString.Name = "txtUATConnString"
        Me.txtUATConnString.Size = New System.Drawing.Size(407, 33)
        Me.txtUATConnString.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(7, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 23)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Database:"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(7, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 23)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Server:"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(7, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(123, 23)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Domain:"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(7, 91)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(123, 23)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Connection String:"
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.txtDEVDatabase)
        Me.GroupBox5.Controls.Add(Me.txtDEVServer)
        Me.GroupBox5.Controls.Add(Me.txtDEVDomain)
        Me.GroupBox5.Controls.Add(Me.txtDEVConnString)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Location = New System.Drawing.Point(5, 342)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(559, 138)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "DEV"
        '
        'txtDEVDatabase
        '
        Me.txtDEVDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDEVDatabase.Location = New System.Drawing.Point(133, 43)
        Me.txtDEVDatabase.Name = "txtDEVDatabase"
        Me.txtDEVDatabase.Size = New System.Drawing.Size(408, 20)
        Me.txtDEVDatabase.TabIndex = 1
        '
        'txtDEVServer
        '
        Me.txtDEVServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDEVServer.Location = New System.Drawing.Point(133, 20)
        Me.txtDEVServer.Name = "txtDEVServer"
        Me.txtDEVServer.Size = New System.Drawing.Size(408, 20)
        Me.txtDEVServer.TabIndex = 0
        '
        'txtDEVDomain
        '
        Me.txtDEVDomain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDEVDomain.Location = New System.Drawing.Point(133, 66)
        Me.txtDEVDomain.Name = "txtDEVDomain"
        Me.txtDEVDomain.Size = New System.Drawing.Size(408, 20)
        Me.txtDEVDomain.TabIndex = 2
        '
        'txtDEVConnString
        '
        Me.txtDEVConnString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDEVConnString.Location = New System.Drawing.Point(133, 89)
        Me.txtDEVConnString.Multiline = True
        Me.txtDEVConnString.Name = "txtDEVConnString"
        Me.txtDEVConnString.Size = New System.Drawing.Size(407, 33)
        Me.txtDEVConnString.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(7, 45)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(123, 23)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Database:"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(7, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(123, 23)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Server:"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(7, 68)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(123, 23)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Domain:"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(7, 91)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(123, 23)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Connection String:"
        '
        'FrmAppSettting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 496)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAppSettting"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Appliction Configuration"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtConnString As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtDomain As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnDEV As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnUAT As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnPROD As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtUATDatabase As System.Windows.Forms.TextBox
    Friend WithEvents txtUATServer As System.Windows.Forms.TextBox
    Friend WithEvents txtUATDomain As System.Windows.Forms.TextBox
    Friend WithEvents txtUATConnString As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDEVDatabase As System.Windows.Forms.TextBox
    Friend WithEvents txtDEVServer As System.Windows.Forms.TextBox
    Friend WithEvents txtDEVDomain As System.Windows.Forms.TextBox
    Friend WithEvents txtDEVConnString As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
End Class
