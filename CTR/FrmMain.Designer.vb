<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblServer = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblUser = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblLoginDt = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblLoginTime = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblAppName = New System.Windows.Forms.Label
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuFileLogout = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuFileExit = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMaintenance = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainAccInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainAccInfoDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainAccInfoSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainOwnerInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainOwnerInfoDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainOwnerInfoDetSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAcOwnerMapping = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAcOwnerMappingDetail = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAcOwnerMappingSum = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuMainAccType = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainAccTypeDetail = New System.Windows.Forms.ToolStripMenuItem
        Me.mnumainAccTypeSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainOwnerType = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainOwnerTypeDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainOwnerTypeSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainTransType = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainTransTypeDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainTransTypeSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainBank = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainBankDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainBankSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainBranch = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainBranchDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainBranchSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainRegAuth = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainRegAuthDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainRegAuthSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainExecDesig = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainExecDesigDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainExecDesigSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainOccuType = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainOccuTypeDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainOccuTypeDetSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainDivision = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainDivisionDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainDivisionSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainDistrict = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainDistrictDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainDistrictSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainThana = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainThanaDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainThanaSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainCountry = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainCountryDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainCountrySum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainCurrency = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainCurrencyDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainCurrencySum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainAssDuration = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainAssDurationDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainAssDurationSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainReportingType = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainReportingTypeDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMainReportingTypeSum = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlMaintenance = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlOwnerDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlOwnerSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuImportOwnerInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.nmugoAmlAccountInfoDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlAccountInfoSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuImportgoAMLAccountInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAMLEntityPersonDetail = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAMLEntityPersonSummary = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAMLImportEntityInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAMLDirectorDetail = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAMLDirectorSummary = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAMLBearerDetail = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAMLBearerSummary = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlBearerInfoImport = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlReportDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlReportSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlPersonDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlPersonSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlAddressDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlAddressSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlAccountdet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlAccountSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.AccountStatusTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlAcStatusDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlAcStatusSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.AccountPersonRoleTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlAccountPersonRoleDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlAccountPersonRoleSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.CommunicationTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlCommuDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlCommuSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.ConductionTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlConductionTypeDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlConductionTypeSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.ContactTypeToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlContactTypeDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlContactTypeSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.CurrenciesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlCurrenciesDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlCurrenciesSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.CountryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlCountryDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlCountrySumm = New System.Windows.Forms.ToolStripMenuItem
        Me.EntityLegalFormTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlEntityDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlEntitySumm = New System.Windows.Forms.ToolStripMenuItem
        Me.EntityPersonRoleTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlEntityPersonRoleDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlEntityPersonRoleSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.FundsTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlFundDetail = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlFundSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOccupationDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOccupationSummary = New System.Windows.Forms.ToolStripMenuItem
        Me.IdentifierTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlIdentTypeDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlIdentTypeSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.SubmissionTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlSubTypeDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlSubTypeSummary = New System.Windows.Forms.ToolStripMenuItem
        Me.DivisiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlDivisionDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlDivisionSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.DistrictToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlDistrictDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlDistrictSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.ThanaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlThanaDetail = New System.Windows.Forms.ToolStripMenuItem
        Me.mnugoAmlThanaSummary = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuTransaction = New System.Windows.Forms.ToolStripMenuItem
        Me.GoAMLTransactionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuTransgoAmlTransDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuTransGoAmlTransSumm = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuTransCTRTransaction = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuTransactionFlexTransaction = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuTools = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuToolsImport = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuToolsImportFlexTrans = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuToolsImportFlexCust = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuToolsImportFlexCustGo = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuToolsImportCCMSTrans = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuToolsProcess = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuToolsProcessRule = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuToolsProcessFileReady = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuToolsProcessRuleGo = New System.Windows.Forms.ToolStripMenuItem
        Me.FileReadyGo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuToolsExport = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuToolsExportgoAml = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReport = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportMismatch = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportMismatchFlexAc = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportMismatchFlexAcGo = New System.Windows.Forms.ToolStripMenuItem
        Me.ProcessTransactionWithACInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportMismatchMissingOwner = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportMismatchMissingOwnerGo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportMissAccountField = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportStatus = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportStatusFileImport = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportStatusCustomergoAML = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportStatusunAuthTrans = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportStatusUnAuthCust = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportTransaction = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportTransactionGo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportOwnerInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuQualifiedTransgoAML = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportMissingEntity = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportMissingDirectorInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuQualifiedAccWithSignEntity = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportCTRvsGoAML = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportFlexvsCCMS = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSystem = New System.Windows.Forms.ToolStripMenuItem
        Me.DepartmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysDeptDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysDeptSum = New System.Windows.Forms.ToolStripMenuItem
        Me.UsersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysUserDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysUserSum = New System.Windows.Forms.ToolStripMenuItem
        Me.FunctionalGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysFGDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysFGSum = New System.Windows.Forms.ToolStripMenuItem
        Me.AssignFGToUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysFGtoUserDet = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysFGtoUserSum = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportSystLog = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysReportUserRole = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysRolePermission = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysRoleMenuPermmission = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysRoleFromPermission = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSystemUserInactivity = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSysEERSFeedExport = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuHelpAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.applicationIdle = New Winforms.Components.ApplicationIdle
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblServer, Me.lblUser, Me.lblLoginDt, Me.lblLoginTime})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 376)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(759, 24)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblServer
        '
        Me.lblServer.AutoSize = False
        Me.lblServer.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblServer.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(70, 19)
        Me.lblServer.Text = "Server"
        '
        'lblUser
        '
        Me.lblUser.AutoSize = False
        Me.lblUser.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblUser.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(200, 19)
        Me.lblUser.Text = "User"
        '
        'lblLoginDt
        '
        Me.lblLoginDt.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblLoginDt.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.lblLoginDt.Name = "lblLoginDt"
        Me.lblLoginDt.Size = New System.Drawing.Size(274, 19)
        Me.lblLoginDt.Spring = True
        Me.lblLoginDt.Text = "Date:"
        '
        'lblLoginTime
        '
        Me.lblLoginTime.AutoSize = False
        Me.lblLoginTime.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblLoginTime.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.lblLoginTime.Name = "lblLoginTime"
        Me.lblLoginTime.Size = New System.Drawing.Size(200, 19)
        Me.lblLoginTime.Text = "Logged at "
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lblVersion)
        Me.Panel1.Controls.Add(Me.lblAppName)
        Me.Panel1.Location = New System.Drawing.Point(1, 67)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(514, 91)
        Me.Panel1.TabIndex = 7
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Black
        Me.Panel2.Location = New System.Drawing.Point(353, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(139, 29)
        Me.Panel2.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(342, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(169, 11)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Developed By Convince Computer Ltd."
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(11, 29)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(38, 13)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "Label1"
        '
        'lblAppName
        '
        Me.lblAppName.AutoSize = True
        Me.lblAppName.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppName.Location = New System.Drawing.Point(10, 13)
        Me.lblAppName.Name = "lblAppName"
        Me.lblAppName.Size = New System.Drawing.Size(50, 16)
        Me.lblAppName.TabIndex = 0
        Me.lblAppName.Text = "Label1"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuMaintenance, Me.mnugoAmlMaintenance, Me.mnuTransaction, Me.mnuTools, Me.mnuReport, Me.mnuSystem, Me.mnuHelp})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(759, 24)
        Me.MenuStrip1.TabIndex = 8
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.mnuFileLogout, Me.mnuFileExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "&File"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(114, 6)
        '
        'mnuFileLogout
        '
        Me.mnuFileLogout.Name = "mnuFileLogout"
        Me.mnuFileLogout.Size = New System.Drawing.Size(117, 22)
        Me.mnuFileLogout.Text = "Log Out"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Image = CType(resources.GetObject("mnuFileExit.Image"), System.Drawing.Image)
        Me.mnuFileExit.Name = "mnuFileExit"
        Me.mnuFileExit.Size = New System.Drawing.Size(117, 22)
        Me.mnuFileExit.Text = "E&xit"
        '
        'mnuMaintenance
        '
        Me.mnuMaintenance.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainAccInfo, Me.mnuMainOwnerInfo, Me.mnuAcOwnerMapping, Me.ToolStripSeparator2, Me.mnuMainAccType, Me.mnuMainOwnerType, Me.mnuMainTransType, Me.mnuMainBank, Me.mnuMainBranch, Me.mnuMainRegAuth, Me.mnuMainExecDesig, Me.mnuMainOccuType, Me.mnuMainDivision, Me.mnuMainDistrict, Me.mnuMainThana, Me.mnuMainCountry, Me.mnuMainCurrency, Me.mnuMainAssDuration, Me.mnuMainReportingType})
        Me.mnuMaintenance.Name = "mnuMaintenance"
        Me.mnuMaintenance.Size = New System.Drawing.Size(88, 20)
        Me.mnuMaintenance.Text = "&Maintenance"
        '
        'mnuMainAccInfo
        '
        Me.mnuMainAccInfo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainAccInfoDet, Me.mnuMainAccInfoSum})
        Me.mnuMainAccInfo.Name = "mnuMainAccInfo"
        Me.mnuMainAccInfo.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainAccInfo.Text = "Account Info"
        '
        'mnuMainAccInfoDet
        '
        Me.mnuMainAccInfoDet.Name = "mnuMainAccInfoDet"
        Me.mnuMainAccInfoDet.Size = New System.Drawing.Size(152, 22)
        Me.mnuMainAccInfoDet.Text = "Detail"
        '
        'mnuMainAccInfoSum
        '
        Me.mnuMainAccInfoSum.Name = "mnuMainAccInfoSum"
        Me.mnuMainAccInfoSum.Size = New System.Drawing.Size(152, 22)
        Me.mnuMainAccInfoSum.Text = "Summary"
        '
        'mnuMainOwnerInfo
        '
        Me.mnuMainOwnerInfo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainOwnerInfoDet, Me.mnuMainOwnerInfoDetSum})
        Me.mnuMainOwnerInfo.Name = "mnuMainOwnerInfo"
        Me.mnuMainOwnerInfo.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainOwnerInfo.Text = "Owner Info"
        '
        'mnuMainOwnerInfoDet
        '
        Me.mnuMainOwnerInfoDet.Name = "mnuMainOwnerInfoDet"
        Me.mnuMainOwnerInfoDet.Size = New System.Drawing.Size(152, 22)
        Me.mnuMainOwnerInfoDet.Text = "Detail"
        '
        'mnuMainOwnerInfoDetSum
        '
        Me.mnuMainOwnerInfoDetSum.Name = "mnuMainOwnerInfoDetSum"
        Me.mnuMainOwnerInfoDetSum.Size = New System.Drawing.Size(152, 22)
        Me.mnuMainOwnerInfoDetSum.Text = "Summary"
        '
        'mnuAcOwnerMapping
        '
        Me.mnuAcOwnerMapping.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAcOwnerMappingDetail, Me.mnuAcOwnerMappingSum})
        Me.mnuAcOwnerMapping.Name = "mnuAcOwnerMapping"
        Me.mnuAcOwnerMapping.Size = New System.Drawing.Size(189, 22)
        Me.mnuAcOwnerMapping.Text = "A/C Owner Mapping"
        '
        'mnuAcOwnerMappingDetail
        '
        Me.mnuAcOwnerMappingDetail.Name = "mnuAcOwnerMappingDetail"
        Me.mnuAcOwnerMappingDetail.Size = New System.Drawing.Size(152, 22)
        Me.mnuAcOwnerMappingDetail.Text = "Detail"
        '
        'mnuAcOwnerMappingSum
        '
        Me.mnuAcOwnerMappingSum.Name = "mnuAcOwnerMappingSum"
        Me.mnuAcOwnerMappingSum.Size = New System.Drawing.Size(152, 22)
        Me.mnuAcOwnerMappingSum.Text = "Summary"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(186, 6)
        '
        'mnuMainAccType
        '
        Me.mnuMainAccType.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainAccTypeDetail, Me.mnumainAccTypeSum})
        Me.mnuMainAccType.Name = "mnuMainAccType"
        Me.mnuMainAccType.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainAccType.Text = "Account Type"
        '
        'mnuMainAccTypeDetail
        '
        Me.mnuMainAccTypeDetail.Name = "mnuMainAccTypeDetail"
        Me.mnuMainAccTypeDetail.Size = New System.Drawing.Size(152, 22)
        Me.mnuMainAccTypeDetail.Text = "Detail"
        '
        'mnumainAccTypeSum
        '
        Me.mnumainAccTypeSum.Name = "mnumainAccTypeSum"
        Me.mnumainAccTypeSum.Size = New System.Drawing.Size(152, 22)
        Me.mnumainAccTypeSum.Text = "Summary"
        '
        'mnuMainOwnerType
        '
        Me.mnuMainOwnerType.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainOwnerTypeDet, Me.mnuMainOwnerTypeSum})
        Me.mnuMainOwnerType.Name = "mnuMainOwnerType"
        Me.mnuMainOwnerType.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainOwnerType.Text = "Ownership Type"
        '
        'mnuMainOwnerTypeDet
        '
        Me.mnuMainOwnerTypeDet.Name = "mnuMainOwnerTypeDet"
        Me.mnuMainOwnerTypeDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainOwnerTypeDet.Text = "Detail"
        '
        'mnuMainOwnerTypeSum
        '
        Me.mnuMainOwnerTypeSum.Name = "mnuMainOwnerTypeSum"
        Me.mnuMainOwnerTypeSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainOwnerTypeSum.Text = "Summary"
        '
        'mnuMainTransType
        '
        Me.mnuMainTransType.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainTransTypeDet, Me.mnuMainTransTypeSum})
        Me.mnuMainTransType.Name = "mnuMainTransType"
        Me.mnuMainTransType.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainTransType.Text = "Transaction Type"
        '
        'mnuMainTransTypeDet
        '
        Me.mnuMainTransTypeDet.Name = "mnuMainTransTypeDet"
        Me.mnuMainTransTypeDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainTransTypeDet.Text = "Detail"
        '
        'mnuMainTransTypeSum
        '
        Me.mnuMainTransTypeSum.Name = "mnuMainTransTypeSum"
        Me.mnuMainTransTypeSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainTransTypeSum.Text = "Summary"
        '
        'mnuMainBank
        '
        Me.mnuMainBank.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainBankDet, Me.mnuMainBankSum})
        Me.mnuMainBank.Name = "mnuMainBank"
        Me.mnuMainBank.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainBank.Text = "Bank"
        '
        'mnuMainBankDet
        '
        Me.mnuMainBankDet.Name = "mnuMainBankDet"
        Me.mnuMainBankDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainBankDet.Text = "Detail"
        '
        'mnuMainBankSum
        '
        Me.mnuMainBankSum.Name = "mnuMainBankSum"
        Me.mnuMainBankSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainBankSum.Text = "Summary"
        '
        'mnuMainBranch
        '
        Me.mnuMainBranch.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainBranchDet, Me.mnuMainBranchSum})
        Me.mnuMainBranch.Name = "mnuMainBranch"
        Me.mnuMainBranch.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainBranch.Text = "Branch"
        '
        'mnuMainBranchDet
        '
        Me.mnuMainBranchDet.Name = "mnuMainBranchDet"
        Me.mnuMainBranchDet.Size = New System.Drawing.Size(152, 22)
        Me.mnuMainBranchDet.Text = "Detail"
        '
        'mnuMainBranchSum
        '
        Me.mnuMainBranchSum.Name = "mnuMainBranchSum"
        Me.mnuMainBranchSum.Size = New System.Drawing.Size(152, 22)
        Me.mnuMainBranchSum.Text = "Summary"
        '
        'mnuMainRegAuth
        '
        Me.mnuMainRegAuth.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainRegAuthDet, Me.mnuMainRegAuthSum})
        Me.mnuMainRegAuth.Name = "mnuMainRegAuth"
        Me.mnuMainRegAuth.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainRegAuth.Text = "Regulatory Authority"
        '
        'mnuMainRegAuthDet
        '
        Me.mnuMainRegAuthDet.Name = "mnuMainRegAuthDet"
        Me.mnuMainRegAuthDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainRegAuthDet.Text = "Detail"
        '
        'mnuMainRegAuthSum
        '
        Me.mnuMainRegAuthSum.Name = "mnuMainRegAuthSum"
        Me.mnuMainRegAuthSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainRegAuthSum.Text = "Summary"
        '
        'mnuMainExecDesig
        '
        Me.mnuMainExecDesig.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainExecDesigDet, Me.mnuMainExecDesigSum})
        Me.mnuMainExecDesig.Name = "mnuMainExecDesig"
        Me.mnuMainExecDesig.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainExecDesig.Text = "Executive Designation"
        '
        'mnuMainExecDesigDet
        '
        Me.mnuMainExecDesigDet.Name = "mnuMainExecDesigDet"
        Me.mnuMainExecDesigDet.Size = New System.Drawing.Size(152, 22)
        Me.mnuMainExecDesigDet.Text = "Detail"
        '
        'mnuMainExecDesigSum
        '
        Me.mnuMainExecDesigSum.Name = "mnuMainExecDesigSum"
        Me.mnuMainExecDesigSum.Size = New System.Drawing.Size(152, 22)
        Me.mnuMainExecDesigSum.Text = "Summary"
        '
        'mnuMainOccuType
        '
        Me.mnuMainOccuType.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainOccuTypeDet, Me.mnuMainOccuTypeDetSum})
        Me.mnuMainOccuType.Name = "mnuMainOccuType"
        Me.mnuMainOccuType.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainOccuType.Text = "Occupation Type"
        '
        'mnuMainOccuTypeDet
        '
        Me.mnuMainOccuTypeDet.Name = "mnuMainOccuTypeDet"
        Me.mnuMainOccuTypeDet.Size = New System.Drawing.Size(152, 22)
        Me.mnuMainOccuTypeDet.Text = "Detail"
        '
        'mnuMainOccuTypeDetSum
        '
        Me.mnuMainOccuTypeDetSum.Name = "mnuMainOccuTypeDetSum"
        Me.mnuMainOccuTypeDetSum.Size = New System.Drawing.Size(152, 22)
        Me.mnuMainOccuTypeDetSum.Text = "Summary"
        '
        'mnuMainDivision
        '
        Me.mnuMainDivision.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainDivisionDet, Me.mnuMainDivisionSum})
        Me.mnuMainDivision.Name = "mnuMainDivision"
        Me.mnuMainDivision.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainDivision.Text = "Division"
        '
        'mnuMainDivisionDet
        '
        Me.mnuMainDivisionDet.Name = "mnuMainDivisionDet"
        Me.mnuMainDivisionDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainDivisionDet.Text = "Detail"
        '
        'mnuMainDivisionSum
        '
        Me.mnuMainDivisionSum.Name = "mnuMainDivisionSum"
        Me.mnuMainDivisionSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainDivisionSum.Text = "Summary"
        '
        'mnuMainDistrict
        '
        Me.mnuMainDistrict.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainDistrictDet, Me.mnuMainDistrictSum})
        Me.mnuMainDistrict.Name = "mnuMainDistrict"
        Me.mnuMainDistrict.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainDistrict.Text = "District"
        '
        'mnuMainDistrictDet
        '
        Me.mnuMainDistrictDet.Name = "mnuMainDistrictDet"
        Me.mnuMainDistrictDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainDistrictDet.Text = "Detail"
        '
        'mnuMainDistrictSum
        '
        Me.mnuMainDistrictSum.Name = "mnuMainDistrictSum"
        Me.mnuMainDistrictSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainDistrictSum.Text = "Summary"
        '
        'mnuMainThana
        '
        Me.mnuMainThana.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainThanaDet, Me.mnuMainThanaSum})
        Me.mnuMainThana.Name = "mnuMainThana"
        Me.mnuMainThana.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainThana.Text = "Thana"
        '
        'mnuMainThanaDet
        '
        Me.mnuMainThanaDet.Name = "mnuMainThanaDet"
        Me.mnuMainThanaDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainThanaDet.Text = "Detail"
        '
        'mnuMainThanaSum
        '
        Me.mnuMainThanaSum.Name = "mnuMainThanaSum"
        Me.mnuMainThanaSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainThanaSum.Text = "Summary"
        '
        'mnuMainCountry
        '
        Me.mnuMainCountry.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainCountryDet, Me.mnuMainCountrySum})
        Me.mnuMainCountry.Name = "mnuMainCountry"
        Me.mnuMainCountry.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainCountry.Text = "Country"
        '
        'mnuMainCountryDet
        '
        Me.mnuMainCountryDet.Name = "mnuMainCountryDet"
        Me.mnuMainCountryDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainCountryDet.Text = "Detail"
        '
        'mnuMainCountrySum
        '
        Me.mnuMainCountrySum.Name = "mnuMainCountrySum"
        Me.mnuMainCountrySum.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainCountrySum.Text = "Summary"
        '
        'mnuMainCurrency
        '
        Me.mnuMainCurrency.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainCurrencyDet, Me.mnuMainCurrencySum})
        Me.mnuMainCurrency.Name = "mnuMainCurrency"
        Me.mnuMainCurrency.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainCurrency.Text = "Currency"
        '
        'mnuMainCurrencyDet
        '
        Me.mnuMainCurrencyDet.Name = "mnuMainCurrencyDet"
        Me.mnuMainCurrencyDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainCurrencyDet.Text = "Detail"
        '
        'mnuMainCurrencySum
        '
        Me.mnuMainCurrencySum.Name = "mnuMainCurrencySum"
        Me.mnuMainCurrencySum.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainCurrencySum.Text = "Summary"
        '
        'mnuMainAssDuration
        '
        Me.mnuMainAssDuration.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainAssDurationDet, Me.mnuMainAssDurationSum})
        Me.mnuMainAssDuration.Name = "mnuMainAssDuration"
        Me.mnuMainAssDuration.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainAssDuration.Text = "Assesment Duration"
        '
        'mnuMainAssDurationDet
        '
        Me.mnuMainAssDurationDet.Name = "mnuMainAssDurationDet"
        Me.mnuMainAssDurationDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainAssDurationDet.Text = "Detail"
        '
        'mnuMainAssDurationSum
        '
        Me.mnuMainAssDurationSum.Name = "mnuMainAssDurationSum"
        Me.mnuMainAssDurationSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainAssDurationSum.Text = "Summary"
        '
        'mnuMainReportingType
        '
        Me.mnuMainReportingType.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainReportingTypeDet, Me.mnuMainReportingTypeSum})
        Me.mnuMainReportingType.Name = "mnuMainReportingType"
        Me.mnuMainReportingType.Size = New System.Drawing.Size(189, 22)
        Me.mnuMainReportingType.Text = "Reporting Type"
        '
        'mnuMainReportingTypeDet
        '
        Me.mnuMainReportingTypeDet.Name = "mnuMainReportingTypeDet"
        Me.mnuMainReportingTypeDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainReportingTypeDet.Text = "Detail"
        '
        'mnuMainReportingTypeSum
        '
        Me.mnuMainReportingTypeSum.Name = "mnuMainReportingTypeSum"
        Me.mnuMainReportingTypeSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainReportingTypeSum.Text = "Summary"
        '
        'mnugoAmlMaintenance
        '
        Me.mnugoAmlMaintenance.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem5, Me.ToolStripMenuItem6, Me.ToolStripSeparator6, Me.ToolStripMenuItem9, Me.ToolStripMenuItem8, Me.ToolStripSeparator7, Me.ToolStripMenuItem10, Me.ToolStripSeparator4, Me.ToolStripMenuItem2, Me.ToolStripMenuItem4, Me.ToolStripMenuItem3, Me.ToolStripSeparator5, Me.ToolStripMenuItem1, Me.AccountStatusTypeToolStripMenuItem, Me.AccountPersonRoleTypeToolStripMenuItem, Me.CommunicationTypeToolStripMenuItem, Me.ConductionTypeToolStripMenuItem, Me.ContactTypeToolStripMenuItem1, Me.CurrenciesToolStripMenuItem, Me.CountryToolStripMenuItem, Me.EntityLegalFormTypeToolStripMenuItem, Me.EntityPersonRoleTypeToolStripMenuItem, Me.FundsTypeToolStripMenuItem, Me.ToolStripMenuItem7, Me.IdentifierTypeToolStripMenuItem, Me.SubmissionTypeToolStripMenuItem, Me.DivisiToolStripMenuItem, Me.DistrictToolStripMenuItem, Me.ThanaToolStripMenuItem})
        Me.mnugoAmlMaintenance.Name = "mnugoAmlMaintenance"
        Me.mnugoAmlMaintenance.Size = New System.Drawing.Size(130, 20)
        Me.mnugoAmlMaintenance.Text = "goAML Maintenance"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlOwnerDet, Me.mnugoAmlOwnerSumm, Me.mnuImportOwnerInfo})
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem5.Text = "Owner Info( goAML )"
        '
        'mnugoAmlOwnerDet
        '
        Me.mnugoAmlOwnerDet.Name = "mnugoAmlOwnerDet"
        Me.mnugoAmlOwnerDet.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAmlOwnerDet.Text = "Detail"
        '
        'mnugoAmlOwnerSumm
        '
        Me.mnugoAmlOwnerSumm.Name = "mnugoAmlOwnerSumm"
        Me.mnugoAmlOwnerSumm.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAmlOwnerSumm.Text = "Summary"
        '
        'mnuImportOwnerInfo
        '
        Me.mnuImportOwnerInfo.Name = "mnuImportOwnerInfo"
        Me.mnuImportOwnerInfo.Size = New System.Drawing.Size(152, 22)
        Me.mnuImportOwnerInfo.Text = "Import File"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.nmugoAmlAccountInfoDet, Me.mnugoAmlAccountInfoSumm, Me.mnuImportgoAMLAccountInfo})
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem6.Text = "Account Info( goAML )"
        '
        'nmugoAmlAccountInfoDet
        '
        Me.nmugoAmlAccountInfoDet.Name = "nmugoAmlAccountInfoDet"
        Me.nmugoAmlAccountInfoDet.Size = New System.Drawing.Size(152, 22)
        Me.nmugoAmlAccountInfoDet.Text = "Detail"
        '
        'mnugoAmlAccountInfoSumm
        '
        Me.mnugoAmlAccountInfoSumm.Name = "mnugoAmlAccountInfoSumm"
        Me.mnugoAmlAccountInfoSumm.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAmlAccountInfoSumm.Text = "Summary"
        '
        'mnuImportgoAMLAccountInfo
        '
        Me.mnuImportgoAMLAccountInfo.Name = "mnuImportgoAMLAccountInfo"
        Me.mnuImportgoAMLAccountInfo.Size = New System.Drawing.Size(152, 22)
        Me.mnuImportgoAMLAccountInfo.Text = "Import File"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(210, 6)
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAMLEntityPersonDetail, Me.mnugoAMLEntityPersonSummary, Me.mnugoAMLImportEntityInfo})
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem9.Text = "Entity Information"
        '
        'mnugoAMLEntityPersonDetail
        '
        Me.mnugoAMLEntityPersonDetail.Name = "mnugoAMLEntityPersonDetail"
        Me.mnugoAMLEntityPersonDetail.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAMLEntityPersonDetail.Text = "Detail"
        '
        'mnugoAMLEntityPersonSummary
        '
        Me.mnugoAMLEntityPersonSummary.Name = "mnugoAMLEntityPersonSummary"
        Me.mnugoAMLEntityPersonSummary.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAMLEntityPersonSummary.Text = "Summary"
        '
        'mnugoAMLImportEntityInfo
        '
        Me.mnugoAMLImportEntityInfo.Name = "mnugoAMLImportEntityInfo"
        Me.mnugoAMLImportEntityInfo.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAMLImportEntityInfo.Text = "Import Flie"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAMLDirectorDetail, Me.mnugoAMLDirectorSummary})
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem8.Text = "Director Info"
        '
        'mnugoAMLDirectorDetail
        '
        Me.mnugoAMLDirectorDetail.Name = "mnugoAMLDirectorDetail"
        Me.mnugoAMLDirectorDetail.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAMLDirectorDetail.Text = "Detail"
        '
        'mnugoAMLDirectorSummary
        '
        Me.mnugoAMLDirectorSummary.Name = "mnugoAMLDirectorSummary"
        Me.mnugoAMLDirectorSummary.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAMLDirectorSummary.Text = "Summary"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(210, 6)
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAMLBearerDetail, Me.mnugoAMLBearerSummary, Me.mnugoAmlBearerInfoImport})
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem10.Text = "Bearer/Dipositor"
        '
        'mnugoAMLBearerDetail
        '
        Me.mnugoAMLBearerDetail.Name = "mnugoAMLBearerDetail"
        Me.mnugoAMLBearerDetail.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAMLBearerDetail.Text = "Detail"
        '
        'mnugoAMLBearerSummary
        '
        Me.mnugoAMLBearerSummary.Name = "mnugoAMLBearerSummary"
        Me.mnugoAMLBearerSummary.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAMLBearerSummary.Text = "Summary"
        '
        'mnugoAmlBearerInfoImport
        '
        Me.mnugoAmlBearerInfoImport.Name = "mnugoAmlBearerInfoImport"
        Me.mnugoAmlBearerInfoImport.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAmlBearerInfoImport.Text = "Import File"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(210, 6)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlReportDet, Me.mnugoAmlReportSumm})
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem2.Text = "Report"
        '
        'mnugoAmlReportDet
        '
        Me.mnugoAmlReportDet.Name = "mnugoAmlReportDet"
        Me.mnugoAmlReportDet.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAmlReportDet.Text = "Detail"
        '
        'mnugoAmlReportSumm
        '
        Me.mnugoAmlReportSumm.Name = "mnugoAmlReportSumm"
        Me.mnugoAmlReportSumm.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAmlReportSumm.Text = "Summary"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlPersonDet, Me.mnugoAmlPersonSumm})
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem4.Text = "Reporting Person"
        '
        'mnugoAmlPersonDet
        '
        Me.mnugoAmlPersonDet.Name = "mnugoAmlPersonDet"
        Me.mnugoAmlPersonDet.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAmlPersonDet.Text = "Detail"
        '
        'mnugoAmlPersonSumm
        '
        Me.mnugoAmlPersonSumm.Name = "mnugoAmlPersonSumm"
        Me.mnugoAmlPersonSumm.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAmlPersonSumm.Text = "Summary"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlAddressDet, Me.mnugoAmlAddressSumm})
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem3.Text = "Address"
        '
        'mnugoAmlAddressDet
        '
        Me.mnugoAmlAddressDet.Name = "mnugoAmlAddressDet"
        Me.mnugoAmlAddressDet.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAmlAddressDet.Text = "Detail"
        '
        'mnugoAmlAddressSumm
        '
        Me.mnugoAmlAddressSumm.Name = "mnugoAmlAddressSumm"
        Me.mnugoAmlAddressSumm.Size = New System.Drawing.Size(152, 22)
        Me.mnugoAmlAddressSumm.Text = "Summary"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(210, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlAccountdet, Me.mnugoAmlAccountSumm})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem1.Text = "Account Type"
        '
        'mnugoAmlAccountdet
        '
        Me.mnugoAmlAccountdet.Name = "mnugoAmlAccountdet"
        Me.mnugoAmlAccountdet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlAccountdet.Text = "Detail"
        '
        'mnugoAmlAccountSumm
        '
        Me.mnugoAmlAccountSumm.Name = "mnugoAmlAccountSumm"
        Me.mnugoAmlAccountSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlAccountSumm.Text = "Summary"
        '
        'AccountStatusTypeToolStripMenuItem
        '
        Me.AccountStatusTypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlAcStatusDet, Me.mnugoAmlAcStatusSumm})
        Me.AccountStatusTypeToolStripMenuItem.Name = "AccountStatusTypeToolStripMenuItem"
        Me.AccountStatusTypeToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.AccountStatusTypeToolStripMenuItem.Text = "Account Status Type"
        '
        'mnugoAmlAcStatusDet
        '
        Me.mnugoAmlAcStatusDet.Name = "mnugoAmlAcStatusDet"
        Me.mnugoAmlAcStatusDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlAcStatusDet.Text = "Detail"
        '
        'mnugoAmlAcStatusSumm
        '
        Me.mnugoAmlAcStatusSumm.Name = "mnugoAmlAcStatusSumm"
        Me.mnugoAmlAcStatusSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlAcStatusSumm.Text = "Summary"
        '
        'AccountPersonRoleTypeToolStripMenuItem
        '
        Me.AccountPersonRoleTypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlAccountPersonRoleDet, Me.mnugoAmlAccountPersonRoleSumm})
        Me.AccountPersonRoleTypeToolStripMenuItem.Name = "AccountPersonRoleTypeToolStripMenuItem"
        Me.AccountPersonRoleTypeToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.AccountPersonRoleTypeToolStripMenuItem.Text = "Account Person Role Type"
        '
        'mnugoAmlAccountPersonRoleDet
        '
        Me.mnugoAmlAccountPersonRoleDet.Name = "mnugoAmlAccountPersonRoleDet"
        Me.mnugoAmlAccountPersonRoleDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlAccountPersonRoleDet.Text = "Detail"
        '
        'mnugoAmlAccountPersonRoleSumm
        '
        Me.mnugoAmlAccountPersonRoleSumm.Name = "mnugoAmlAccountPersonRoleSumm"
        Me.mnugoAmlAccountPersonRoleSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlAccountPersonRoleSumm.Text = "Summary"
        '
        'CommunicationTypeToolStripMenuItem
        '
        Me.CommunicationTypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlCommuDet, Me.mnugoAmlCommuSumm})
        Me.CommunicationTypeToolStripMenuItem.Name = "CommunicationTypeToolStripMenuItem"
        Me.CommunicationTypeToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.CommunicationTypeToolStripMenuItem.Text = "Communication Type"
        '
        'mnugoAmlCommuDet
        '
        Me.mnugoAmlCommuDet.Name = "mnugoAmlCommuDet"
        Me.mnugoAmlCommuDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlCommuDet.Text = "Detail"
        '
        'mnugoAmlCommuSumm
        '
        Me.mnugoAmlCommuSumm.Name = "mnugoAmlCommuSumm"
        Me.mnugoAmlCommuSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlCommuSumm.Text = "Summary"
        '
        'ConductionTypeToolStripMenuItem
        '
        Me.ConductionTypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlConductionTypeDet, Me.mnugoAmlConductionTypeSumm})
        Me.ConductionTypeToolStripMenuItem.Name = "ConductionTypeToolStripMenuItem"
        Me.ConductionTypeToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.ConductionTypeToolStripMenuItem.Text = "Conduction Type"
        '
        'mnugoAmlConductionTypeDet
        '
        Me.mnugoAmlConductionTypeDet.Name = "mnugoAmlConductionTypeDet"
        Me.mnugoAmlConductionTypeDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlConductionTypeDet.Text = "Detail"
        '
        'mnugoAmlConductionTypeSumm
        '
        Me.mnugoAmlConductionTypeSumm.Name = "mnugoAmlConductionTypeSumm"
        Me.mnugoAmlConductionTypeSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlConductionTypeSumm.Text = "Summary"
        '
        'ContactTypeToolStripMenuItem1
        '
        Me.ContactTypeToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlContactTypeDet, Me.mnugoAmlContactTypeSumm})
        Me.ContactTypeToolStripMenuItem1.Name = "ContactTypeToolStripMenuItem1"
        Me.ContactTypeToolStripMenuItem1.Size = New System.Drawing.Size(213, 22)
        Me.ContactTypeToolStripMenuItem1.Text = "Contact Type"
        '
        'mnugoAmlContactTypeDet
        '
        Me.mnugoAmlContactTypeDet.Name = "mnugoAmlContactTypeDet"
        Me.mnugoAmlContactTypeDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlContactTypeDet.Text = "Detail"
        '
        'mnugoAmlContactTypeSumm
        '
        Me.mnugoAmlContactTypeSumm.Name = "mnugoAmlContactTypeSumm"
        Me.mnugoAmlContactTypeSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlContactTypeSumm.Text = "Summary"
        '
        'CurrenciesToolStripMenuItem
        '
        Me.CurrenciesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlCurrenciesDet, Me.mnugoAmlCurrenciesSumm})
        Me.CurrenciesToolStripMenuItem.Name = "CurrenciesToolStripMenuItem"
        Me.CurrenciesToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.CurrenciesToolStripMenuItem.Text = "Currencies"
        '
        'mnugoAmlCurrenciesDet
        '
        Me.mnugoAmlCurrenciesDet.Name = "mnugoAmlCurrenciesDet"
        Me.mnugoAmlCurrenciesDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlCurrenciesDet.Text = "Detail"
        '
        'mnugoAmlCurrenciesSumm
        '
        Me.mnugoAmlCurrenciesSumm.Name = "mnugoAmlCurrenciesSumm"
        Me.mnugoAmlCurrenciesSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlCurrenciesSumm.Text = "Summary"
        '
        'CountryToolStripMenuItem
        '
        Me.CountryToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlCountryDet, Me.mnugoAmlCountrySumm})
        Me.CountryToolStripMenuItem.Name = "CountryToolStripMenuItem"
        Me.CountryToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.CountryToolStripMenuItem.Text = "Country"
        '
        'mnugoAmlCountryDet
        '
        Me.mnugoAmlCountryDet.Name = "mnugoAmlCountryDet"
        Me.mnugoAmlCountryDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlCountryDet.Text = "Detail"
        '
        'mnugoAmlCountrySumm
        '
        Me.mnugoAmlCountrySumm.Name = "mnugoAmlCountrySumm"
        Me.mnugoAmlCountrySumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlCountrySumm.Text = "Summary"
        '
        'EntityLegalFormTypeToolStripMenuItem
        '
        Me.EntityLegalFormTypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlEntityDet, Me.mnugoAmlEntitySumm})
        Me.EntityLegalFormTypeToolStripMenuItem.Name = "EntityLegalFormTypeToolStripMenuItem"
        Me.EntityLegalFormTypeToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.EntityLegalFormTypeToolStripMenuItem.Text = "Entity Legal Form Type"
        '
        'mnugoAmlEntityDet
        '
        Me.mnugoAmlEntityDet.Name = "mnugoAmlEntityDet"
        Me.mnugoAmlEntityDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlEntityDet.Text = "Detail"
        '
        'mnugoAmlEntitySumm
        '
        Me.mnugoAmlEntitySumm.Name = "mnugoAmlEntitySumm"
        Me.mnugoAmlEntitySumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlEntitySumm.Text = "Summary"
        '
        'EntityPersonRoleTypeToolStripMenuItem
        '
        Me.EntityPersonRoleTypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlEntityPersonRoleDet, Me.mnugoAmlEntityPersonRoleSumm})
        Me.EntityPersonRoleTypeToolStripMenuItem.Name = "EntityPersonRoleTypeToolStripMenuItem"
        Me.EntityPersonRoleTypeToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.EntityPersonRoleTypeToolStripMenuItem.Text = "Entity Person Role Type"
        '
        'mnugoAmlEntityPersonRoleDet
        '
        Me.mnugoAmlEntityPersonRoleDet.Name = "mnugoAmlEntityPersonRoleDet"
        Me.mnugoAmlEntityPersonRoleDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlEntityPersonRoleDet.Text = "Detail"
        '
        'mnugoAmlEntityPersonRoleSumm
        '
        Me.mnugoAmlEntityPersonRoleSumm.Name = "mnugoAmlEntityPersonRoleSumm"
        Me.mnugoAmlEntityPersonRoleSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlEntityPersonRoleSumm.Text = "Summary"
        '
        'FundsTypeToolStripMenuItem
        '
        Me.FundsTypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlFundDetail, Me.mnugoAmlFundSumm})
        Me.FundsTypeToolStripMenuItem.Name = "FundsTypeToolStripMenuItem"
        Me.FundsTypeToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.FundsTypeToolStripMenuItem.Text = "Funds Type"
        '
        'mnugoAmlFundDetail
        '
        Me.mnugoAmlFundDetail.Name = "mnugoAmlFundDetail"
        Me.mnugoAmlFundDetail.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlFundDetail.Text = "Detail"
        '
        'mnugoAmlFundSumm
        '
        Me.mnugoAmlFundSumm.Name = "mnugoAmlFundSumm"
        Me.mnugoAmlFundSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlFundSumm.Text = "Summary"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOccupationDet, Me.mnuOccupationSummary})
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem7.Text = "Occupation Type"
        '
        'mnuOccupationDet
        '
        Me.mnuOccupationDet.Name = "mnuOccupationDet"
        Me.mnuOccupationDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuOccupationDet.Text = "Detail"
        '
        'mnuOccupationSummary
        '
        Me.mnuOccupationSummary.Name = "mnuOccupationSummary"
        Me.mnuOccupationSummary.Size = New System.Drawing.Size(125, 22)
        Me.mnuOccupationSummary.Text = "Summary"
        '
        'IdentifierTypeToolStripMenuItem
        '
        Me.IdentifierTypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlIdentTypeDet, Me.mnugoAmlIdentTypeSumm})
        Me.IdentifierTypeToolStripMenuItem.Name = "IdentifierTypeToolStripMenuItem"
        Me.IdentifierTypeToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.IdentifierTypeToolStripMenuItem.Text = "Identifier Type"
        '
        'mnugoAmlIdentTypeDet
        '
        Me.mnugoAmlIdentTypeDet.Name = "mnugoAmlIdentTypeDet"
        Me.mnugoAmlIdentTypeDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlIdentTypeDet.Text = "Detail"
        '
        'mnugoAmlIdentTypeSumm
        '
        Me.mnugoAmlIdentTypeSumm.Name = "mnugoAmlIdentTypeSumm"
        Me.mnugoAmlIdentTypeSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlIdentTypeSumm.Text = "Summary"
        '
        'SubmissionTypeToolStripMenuItem
        '
        Me.SubmissionTypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlSubTypeDet, Me.mnugoAmlSubTypeSummary})
        Me.SubmissionTypeToolStripMenuItem.Name = "SubmissionTypeToolStripMenuItem"
        Me.SubmissionTypeToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.SubmissionTypeToolStripMenuItem.Text = "Submission Type"
        '
        'mnugoAmlSubTypeDet
        '
        Me.mnugoAmlSubTypeDet.Name = "mnugoAmlSubTypeDet"
        Me.mnugoAmlSubTypeDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlSubTypeDet.Text = "Detail"
        '
        'mnugoAmlSubTypeSummary
        '
        Me.mnugoAmlSubTypeSummary.Name = "mnugoAmlSubTypeSummary"
        Me.mnugoAmlSubTypeSummary.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlSubTypeSummary.Text = "Summary"
        '
        'DivisiToolStripMenuItem
        '
        Me.DivisiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlDivisionDet, Me.mnugoAmlDivisionSumm})
        Me.DivisiToolStripMenuItem.Name = "DivisiToolStripMenuItem"
        Me.DivisiToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.DivisiToolStripMenuItem.Text = "Division"
        '
        'mnugoAmlDivisionDet
        '
        Me.mnugoAmlDivisionDet.Name = "mnugoAmlDivisionDet"
        Me.mnugoAmlDivisionDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlDivisionDet.Text = "Detail"
        '
        'mnugoAmlDivisionSumm
        '
        Me.mnugoAmlDivisionSumm.Name = "mnugoAmlDivisionSumm"
        Me.mnugoAmlDivisionSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlDivisionSumm.Text = "Summary"
        '
        'DistrictToolStripMenuItem
        '
        Me.DistrictToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlDistrictDet, Me.mnugoAmlDistrictSumm})
        Me.DistrictToolStripMenuItem.Name = "DistrictToolStripMenuItem"
        Me.DistrictToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.DistrictToolStripMenuItem.Text = "District"
        '
        'mnugoAmlDistrictDet
        '
        Me.mnugoAmlDistrictDet.Name = "mnugoAmlDistrictDet"
        Me.mnugoAmlDistrictDet.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlDistrictDet.Text = "Detail"
        '
        'mnugoAmlDistrictSumm
        '
        Me.mnugoAmlDistrictSumm.Name = "mnugoAmlDistrictSumm"
        Me.mnugoAmlDistrictSumm.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlDistrictSumm.Text = "Summary"
        '
        'ThanaToolStripMenuItem
        '
        Me.ThanaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnugoAmlThanaDetail, Me.mnugoAmlThanaSummary})
        Me.ThanaToolStripMenuItem.Name = "ThanaToolStripMenuItem"
        Me.ThanaToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.ThanaToolStripMenuItem.Text = "Thana"
        '
        'mnugoAmlThanaDetail
        '
        Me.mnugoAmlThanaDetail.Name = "mnugoAmlThanaDetail"
        Me.mnugoAmlThanaDetail.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlThanaDetail.Text = "Detail"
        '
        'mnugoAmlThanaSummary
        '
        Me.mnugoAmlThanaSummary.Name = "mnugoAmlThanaSummary"
        Me.mnugoAmlThanaSummary.Size = New System.Drawing.Size(125, 22)
        Me.mnugoAmlThanaSummary.Text = "Summary"
        '
        'mnuTransaction
        '
        Me.mnuTransaction.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GoAMLTransactionToolStripMenuItem, Me.mnuTransCTRTransaction, Me.mnuTransactionFlexTransaction})
        Me.mnuTransaction.Name = "mnuTransaction"
        Me.mnuTransaction.Size = New System.Drawing.Size(81, 20)
        Me.mnuTransaction.Text = "Transaction"
        '
        'GoAMLTransactionToolStripMenuItem
        '
        Me.GoAMLTransactionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuTransgoAmlTransDet, Me.mnuTransGoAmlTransSumm})
        Me.GoAMLTransactionToolStripMenuItem.Name = "GoAMLTransactionToolStripMenuItem"
        Me.GoAMLTransactionToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.GoAMLTransactionToolStripMenuItem.Text = "goAML Transaction"
        '
        'mnuTransgoAmlTransDet
        '
        Me.mnuTransgoAmlTransDet.Name = "mnuTransgoAmlTransDet"
        Me.mnuTransgoAmlTransDet.Size = New System.Drawing.Size(152, 22)
        Me.mnuTransgoAmlTransDet.Text = "Detail"
        '
        'mnuTransGoAmlTransSumm
        '
        Me.mnuTransGoAmlTransSumm.Name = "mnuTransGoAmlTransSumm"
        Me.mnuTransGoAmlTransSumm.Size = New System.Drawing.Size(152, 22)
        Me.mnuTransGoAmlTransSumm.Text = "Summary"
        '
        'mnuTransCTRTransaction
        '
        Me.mnuTransCTRTransaction.Name = "mnuTransCTRTransaction"
        Me.mnuTransCTRTransaction.Size = New System.Drawing.Size(178, 22)
        Me.mnuTransCTRTransaction.Text = "CTR Transaction"
        '
        'mnuTransactionFlexTransaction
        '
        Me.mnuTransactionFlexTransaction.Name = "mnuTransactionFlexTransaction"
        Me.mnuTransactionFlexTransaction.Size = New System.Drawing.Size(178, 22)
        Me.mnuTransactionFlexTransaction.Text = "Flex Transaction"
        '
        'mnuTools
        '
        Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuToolsImport, Me.mnuToolsProcess, Me.mnuToolsExport, Me.mnuToolsExportgoAml})
        Me.mnuTools.Name = "mnuTools"
        Me.mnuTools.Size = New System.Drawing.Size(48, 20)
        Me.mnuTools.Text = "&Tools"
        '
        'mnuToolsImport
        '
        Me.mnuToolsImport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuToolsImportFlexTrans, Me.mnuToolsImportFlexCust, Me.mnuToolsImportFlexCustGo, Me.ToolStripSeparator8, Me.mnuToolsImportCCMSTrans})
        Me.mnuToolsImport.Name = "mnuToolsImport"
        Me.mnuToolsImport.Size = New System.Drawing.Size(152, 22)
        Me.mnuToolsImport.Text = "&Import"
        '
        'mnuToolsImportFlexTrans
        '
        Me.mnuToolsImportFlexTrans.Name = "mnuToolsImportFlexTrans"
        Me.mnuToolsImportFlexTrans.Size = New System.Drawing.Size(227, 22)
        Me.mnuToolsImportFlexTrans.Text = "Flex Transaction"
        '
        'mnuToolsImportFlexCust
        '
        Me.mnuToolsImportFlexCust.Name = "mnuToolsImportFlexCust"
        Me.mnuToolsImportFlexCust.Size = New System.Drawing.Size(227, 22)
        Me.mnuToolsImportFlexCust.Text = "Flex Customer"
        '
        'mnuToolsImportFlexCustGo
        '
        Me.mnuToolsImportFlexCustGo.Name = "mnuToolsImportFlexCustGo"
        Me.mnuToolsImportFlexCustGo.Size = New System.Drawing.Size(227, 22)
        Me.mnuToolsImportFlexCustGo.Text = "Flex Customer (goAML)"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(224, 6)
        '
        'mnuToolsImportCCMSTrans
        '
        Me.mnuToolsImportCCMSTrans.Name = "mnuToolsImportCCMSTrans"
        Me.mnuToolsImportCCMSTrans.Size = New System.Drawing.Size(227, 22)
        Me.mnuToolsImportCCMSTrans.Text = "CCMS Transactions (goAML)"
        '
        'mnuToolsProcess
        '
        Me.mnuToolsProcess.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuToolsProcessRule, Me.mnuToolsProcessFileReady, Me.mnuToolsProcessRuleGo, Me.FileReadyGo})
        Me.mnuToolsProcess.Name = "mnuToolsProcess"
        Me.mnuToolsProcess.Size = New System.Drawing.Size(152, 22)
        Me.mnuToolsProcess.Text = "&Process"
        '
        'mnuToolsProcessRule
        '
        Me.mnuToolsProcessRule.Name = "mnuToolsProcessRule"
        Me.mnuToolsProcessRule.Size = New System.Drawing.Size(256, 22)
        Me.mnuToolsProcessRule.Text = "Process by Rule"
        '
        'mnuToolsProcessFileReady
        '
        Me.mnuToolsProcessFileReady.Name = "mnuToolsProcessFileReady"
        Me.mnuToolsProcessFileReady.Size = New System.Drawing.Size(256, 22)
        Me.mnuToolsProcessFileReady.Text = "Ready for File Generation"
        '
        'mnuToolsProcessRuleGo
        '
        Me.mnuToolsProcessRuleGo.Name = "mnuToolsProcessRuleGo"
        Me.mnuToolsProcessRuleGo.Size = New System.Drawing.Size(256, 22)
        Me.mnuToolsProcessRuleGo.Text = "Process by Rule (goAML)"
        '
        'FileReadyGo
        '
        Me.FileReadyGo.Name = "FileReadyGo"
        Me.FileReadyGo.Size = New System.Drawing.Size(256, 22)
        Me.FileReadyGo.Text = "Ready for File Generation (goAML)"
        '
        'mnuToolsExport
        '
        Me.mnuToolsExport.Name = "mnuToolsExport"
        Me.mnuToolsExport.Size = New System.Drawing.Size(152, 22)
        Me.mnuToolsExport.Text = "&Export"
        '
        'mnuToolsExportgoAml
        '
        Me.mnuToolsExportgoAml.Name = "mnuToolsExportgoAml"
        Me.mnuToolsExportgoAml.Size = New System.Drawing.Size(152, 22)
        Me.mnuToolsExportgoAml.Text = "Export goAML"
        '
        'mnuReport
        '
        Me.mnuReport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportMismatch, Me.mnuReportStatus, Me.mnuReportTransaction, Me.mnuReportTransactionGo, Me.mnuReportOwnerInfo, Me.mnuQualifiedTransgoAML, Me.mnuReportMissingEntity, Me.mnuReportMissingDirectorInfo, Me.mnuQualifiedAccWithSignEntity, Me.mnuReportCTRvsGoAML, Me.mnuReportFlexvsCCMS})
        Me.mnuReport.Name = "mnuReport"
        Me.mnuReport.Size = New System.Drawing.Size(54, 20)
        Me.mnuReport.Text = "&Report"
        '
        'mnuReportMismatch
        '
        Me.mnuReportMismatch.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportMismatchFlexAc, Me.mnuReportMismatchFlexAcGo, Me.ProcessTransactionWithACInfoToolStripMenuItem, Me.mnuReportMismatchMissingOwner, Me.mnuReportMismatchMissingOwnerGo, Me.mnuReportMissAccountField})
        Me.mnuReportMismatch.Name = "mnuReportMismatch"
        Me.mnuReportMismatch.Size = New System.Drawing.Size(331, 22)
        Me.mnuReportMismatch.Text = "Mismatch"
        '
        'mnuReportMismatchFlexAc
        '
        Me.mnuReportMismatchFlexAc.Name = "mnuReportMismatchFlexAc"
        Me.mnuReportMismatchFlexAc.Size = New System.Drawing.Size(307, 22)
        Me.mnuReportMismatchFlexAc.Text = "Flex Transaction with Flex A/C"
        '
        'mnuReportMismatchFlexAcGo
        '
        Me.mnuReportMismatchFlexAcGo.Name = "mnuReportMismatchFlexAcGo"
        Me.mnuReportMismatchFlexAcGo.Size = New System.Drawing.Size(307, 22)
        Me.mnuReportMismatchFlexAcGo.Text = "Flex Transaction with Flex A/C (goAML)"
        '
        'ProcessTransactionWithACInfoToolStripMenuItem
        '
        Me.ProcessTransactionWithACInfoToolStripMenuItem.Name = "ProcessTransactionWithACInfoToolStripMenuItem"
        Me.ProcessTransactionWithACInfoToolStripMenuItem.Size = New System.Drawing.Size(307, 22)
        Me.ProcessTransactionWithACInfoToolStripMenuItem.Text = "Process Transaction with A/C Info"
        '
        'mnuReportMismatchMissingOwner
        '
        Me.mnuReportMismatchMissingOwner.Name = "mnuReportMismatchMissingOwner"
        Me.mnuReportMismatchMissingOwner.Size = New System.Drawing.Size(307, 22)
        Me.mnuReportMismatchMissingOwner.Text = "Missing Owner Information"
        '
        'mnuReportMismatchMissingOwnerGo
        '
        Me.mnuReportMismatchMissingOwnerGo.Name = "mnuReportMismatchMissingOwnerGo"
        Me.mnuReportMismatchMissingOwnerGo.Size = New System.Drawing.Size(307, 22)
        Me.mnuReportMismatchMissingOwnerGo.Text = "Missing Owner Information (goAML)"
        '
        'mnuReportMissAccountField
        '
        Me.mnuReportMissAccountField.Name = "mnuReportMissAccountField"
        Me.mnuReportMissAccountField.Size = New System.Drawing.Size(307, 22)
        Me.mnuReportMissAccountField.Text = "Missing Account Field Information (goAML)"
        '
        'mnuReportStatus
        '
        Me.mnuReportStatus.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportStatusFileImport, Me.mnuReportStatusCustomergoAML, Me.mnuReportStatusunAuthTrans, Me.mnuReportStatusUnAuthCust})
        Me.mnuReportStatus.Name = "mnuReportStatus"
        Me.mnuReportStatus.Size = New System.Drawing.Size(331, 22)
        Me.mnuReportStatus.Text = "Status"
        '
        'mnuReportStatusFileImport
        '
        Me.mnuReportStatusFileImport.Name = "mnuReportStatusFileImport"
        Me.mnuReportStatusFileImport.Size = New System.Drawing.Size(215, 22)
        Me.mnuReportStatusFileImport.Text = "Transaction File Import"
        '
        'mnuReportStatusCustomergoAML
        '
        Me.mnuReportStatusCustomergoAML.Name = "mnuReportStatusCustomergoAML"
        Me.mnuReportStatusCustomergoAML.Size = New System.Drawing.Size(215, 22)
        Me.mnuReportStatusCustomergoAML.Text = "Import Customer (goAML)"
        '
        'mnuReportStatusunAuthTrans
        '
        Me.mnuReportStatusunAuthTrans.Name = "mnuReportStatusunAuthTrans"
        Me.mnuReportStatusunAuthTrans.Size = New System.Drawing.Size(215, 22)
        Me.mnuReportStatusunAuthTrans.Text = "Unauthorized Transaction "
        '
        'mnuReportStatusUnAuthCust
        '
        Me.mnuReportStatusUnAuthCust.Name = "mnuReportStatusUnAuthCust"
        Me.mnuReportStatusUnAuthCust.Size = New System.Drawing.Size(215, 22)
        Me.mnuReportStatusUnAuthCust.Text = "Unauthorized Customer"
        '
        'mnuReportTransaction
        '
        Me.mnuReportTransaction.Name = "mnuReportTransaction"
        Me.mnuReportTransaction.Size = New System.Drawing.Size(331, 22)
        Me.mnuReportTransaction.Text = "Transaction"
        '
        'mnuReportTransactionGo
        '
        Me.mnuReportTransactionGo.Name = "mnuReportTransactionGo"
        Me.mnuReportTransactionGo.Size = New System.Drawing.Size(331, 22)
        Me.mnuReportTransactionGo.Text = "Transaction (goAML)"
        '
        'mnuReportOwnerInfo
        '
        Me.mnuReportOwnerInfo.Name = "mnuReportOwnerInfo"
        Me.mnuReportOwnerInfo.Size = New System.Drawing.Size(331, 22)
        Me.mnuReportOwnerInfo.Text = "Owner Info"
        '
        'mnuQualifiedTransgoAML
        '
        Me.mnuQualifiedTransgoAML.Name = "mnuQualifiedTransgoAML"
        Me.mnuQualifiedTransgoAML.Size = New System.Drawing.Size(331, 22)
        Me.mnuQualifiedTransgoAML.Text = "Qualified Transaction (goAML)"
        '
        'mnuReportMissingEntity
        '
        Me.mnuReportMissingEntity.Name = "mnuReportMissingEntity"
        Me.mnuReportMissingEntity.Size = New System.Drawing.Size(331, 22)
        Me.mnuReportMissingEntity.Text = "Missing Entity Info(goAML)"
        '
        'mnuReportMissingDirectorInfo
        '
        Me.mnuReportMissingDirectorInfo.Name = "mnuReportMissingDirectorInfo"
        Me.mnuReportMissingDirectorInfo.Size = New System.Drawing.Size(331, 22)
        Me.mnuReportMissingDirectorInfo.Text = "Missing Director Info(goAML)"
        '
        'mnuQualifiedAccWithSignEntity
        '
        Me.mnuQualifiedAccWithSignEntity.Name = "mnuQualifiedAccWithSignEntity"
        Me.mnuQualifiedAccWithSignEntity.Size = New System.Drawing.Size(331, 22)
        Me.mnuQualifiedAccWithSignEntity.Text = "Qualified Account With Signatory and Entity Info"
        '
        'mnuReportCTRvsGoAML
        '
        Me.mnuReportCTRvsGoAML.Name = "mnuReportCTRvsGoAML"
        Me.mnuReportCTRvsGoAML.Size = New System.Drawing.Size(331, 22)
        Me.mnuReportCTRvsGoAML.Text = "CTR vs GoAML Qualified Transaction"
        Me.mnuReportCTRvsGoAML.Visible = False
        '
        'mnuReportFlexvsCCMS
        '
        Me.mnuReportFlexvsCCMS.Name = "mnuReportFlexvsCCMS"
        Me.mnuReportFlexvsCCMS.Size = New System.Drawing.Size(331, 22)
        Me.mnuReportFlexvsCCMS.Text = "FLEX vs CCMS Mismatch"
        Me.mnuReportFlexvsCCMS.Visible = False
        '
        'mnuSystem
        '
        Me.mnuSystem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DepartmentToolStripMenuItem, Me.UsersToolStripMenuItem, Me.FunctionalGroupToolStripMenuItem, Me.AssignFGToUserToolStripMenuItem, Me.ToolStripSeparator3, Me.ReportToolStripMenuItem, Me.mnuSysEERSFeedExport})
        Me.mnuSystem.Name = "mnuSystem"
        Me.mnuSystem.Size = New System.Drawing.Size(57, 20)
        Me.mnuSystem.Text = "System"
        '
        'DepartmentToolStripMenuItem
        '
        Me.DepartmentToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSysDeptDet, Me.mnuSysDeptSum})
        Me.DepartmentToolStripMenuItem.Name = "DepartmentToolStripMenuItem"
        Me.DepartmentToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.DepartmentToolStripMenuItem.Text = "Department"
        '
        'mnuSysDeptDet
        '
        Me.mnuSysDeptDet.Name = "mnuSysDeptDet"
        Me.mnuSysDeptDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuSysDeptDet.Text = "Detail"
        '
        'mnuSysDeptSum
        '
        Me.mnuSysDeptSum.Name = "mnuSysDeptSum"
        Me.mnuSysDeptSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuSysDeptSum.Text = "Summary"
        '
        'UsersToolStripMenuItem
        '
        Me.UsersToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSysUserDet, Me.mnuSysUserSum})
        Me.UsersToolStripMenuItem.Name = "UsersToolStripMenuItem"
        Me.UsersToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.UsersToolStripMenuItem.Text = "Users"
        '
        'mnuSysUserDet
        '
        Me.mnuSysUserDet.Name = "mnuSysUserDet"
        Me.mnuSysUserDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuSysUserDet.Text = "Detail"
        '
        'mnuSysUserSum
        '
        Me.mnuSysUserSum.Name = "mnuSysUserSum"
        Me.mnuSysUserSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuSysUserSum.Text = "Summary"
        '
        'FunctionalGroupToolStripMenuItem
        '
        Me.FunctionalGroupToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSysFGDet, Me.mnuSysFGSum})
        Me.FunctionalGroupToolStripMenuItem.Name = "FunctionalGroupToolStripMenuItem"
        Me.FunctionalGroupToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.FunctionalGroupToolStripMenuItem.Text = "Functional Group"
        '
        'mnuSysFGDet
        '
        Me.mnuSysFGDet.Name = "mnuSysFGDet"
        Me.mnuSysFGDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuSysFGDet.Text = "Detail"
        '
        'mnuSysFGSum
        '
        Me.mnuSysFGSum.Name = "mnuSysFGSum"
        Me.mnuSysFGSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuSysFGSum.Text = "Summary"
        '
        'AssignFGToUserToolStripMenuItem
        '
        Me.AssignFGToUserToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSysFGtoUserDet, Me.mnuSysFGtoUserSum})
        Me.AssignFGToUserToolStripMenuItem.Name = "AssignFGToUserToolStripMenuItem"
        Me.AssignFGToUserToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.AssignFGToUserToolStripMenuItem.Text = "Assign FG to User"
        '
        'mnuSysFGtoUserDet
        '
        Me.mnuSysFGtoUserDet.Name = "mnuSysFGtoUserDet"
        Me.mnuSysFGtoUserDet.Size = New System.Drawing.Size(125, 22)
        Me.mnuSysFGtoUserDet.Text = "Detail"
        '
        'mnuSysFGtoUserSum
        '
        Me.mnuSysFGtoUserSum.Name = "mnuSysFGtoUserSum"
        Me.mnuSysFGtoUserSum.Size = New System.Drawing.Size(125, 22)
        Me.mnuSysFGtoUserSum.Text = "Summary"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(163, 6)
        '
        'ReportToolStripMenuItem
        '
        Me.ReportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportSystLog, Me.mnuSysReportUserRole, Me.mnuSysRolePermission, Me.mnuSystemUserInactivity})
        Me.ReportToolStripMenuItem.Name = "ReportToolStripMenuItem"
        Me.ReportToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ReportToolStripMenuItem.Text = "Report"
        '
        'mnuReportSystLog
        '
        Me.mnuReportSystLog.Name = "mnuReportSystLog"
        Me.mnuReportSystLog.Size = New System.Drawing.Size(186, 22)
        Me.mnuReportSystLog.Text = "System Log"
        '
        'mnuSysReportUserRole
        '
        Me.mnuSysReportUserRole.Name = "mnuSysReportUserRole"
        Me.mnuSysReportUserRole.Size = New System.Drawing.Size(186, 22)
        Me.mnuSysReportUserRole.Text = "User Role"
        '
        'mnuSysRolePermission
        '
        Me.mnuSysRolePermission.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSysRoleMenuPermmission, Me.mnuSysRoleFromPermission})
        Me.mnuSysRolePermission.Name = "mnuSysRolePermission"
        Me.mnuSysRolePermission.Size = New System.Drawing.Size(186, 22)
        Me.mnuSysRolePermission.Text = "Role Permission"
        '
        'mnuSysRoleMenuPermmission
        '
        Me.mnuSysRoleMenuPermmission.Name = "mnuSysRoleMenuPermmission"
        Me.mnuSysRoleMenuPermmission.Size = New System.Drawing.Size(166, 22)
        Me.mnuSysRoleMenuPermmission.Text = "Menu Permission"
        '
        'mnuSysRoleFromPermission
        '
        Me.mnuSysRoleFromPermission.Name = "mnuSysRoleFromPermission"
        Me.mnuSysRoleFromPermission.Size = New System.Drawing.Size(166, 22)
        Me.mnuSysRoleFromPermission.Text = "From Permission"
        '
        'mnuSystemUserInactivity
        '
        Me.mnuSystemUserInactivity.Name = "mnuSystemUserInactivity"
        Me.mnuSystemUserInactivity.Size = New System.Drawing.Size(186, 22)
        Me.mnuSystemUserInactivity.Text = "User Inactivity Report"
        '
        'mnuSysEERSFeedExport
        '
        Me.mnuSysEERSFeedExport.Name = "mnuSysEERSFeedExport"
        Me.mnuSysEERSFeedExport.Size = New System.Drawing.Size(166, 22)
        Me.mnuSysEERSFeedExport.Text = "EERS Feed Export"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHelpAbout})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(44, 20)
        Me.mnuHelp.Text = "&Help"
        '
        'mnuHelpAbout
        '
        Me.mnuHelpAbout.Name = "mnuHelpAbout"
        Me.mnuHelpAbout.Size = New System.Drawing.Size(107, 22)
        Me.mnuHelpAbout.Text = "About"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(349, 48)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(142, 46)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Black
        Me.Panel3.Location = New System.Drawing.Point(2, 72)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(515, 88)
        Me.Panel3.TabIndex = 11
        '
        'applicationIdle
        '
        Me.applicationIdle.IdleTime = System.TimeSpan.Parse("00:30:00")
        Me.applicationIdle.WarnTime = System.TimeSpan.Parse("00:00:20")
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 400)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Panel3)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FrmMain"
        Me.Text = "CTR"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblUser As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblLoginDt As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblLoginTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblAppName As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsImport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaintenance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainAccInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainOwnerInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsProcess As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsImportFlexTrans As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsImportFlexCust As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportMismatch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportMismatchFlexAc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsProcessRule As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsProcessFileReady As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProcessTransactionWithACInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainAccInfoDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainAccInfoSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportMismatchMissingOwner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainOwnerInfoDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainOwnerInfoDetSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsExport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAcOwnerMapping As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAcOwnerMappingDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAcOwnerMappingSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainAccType As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainAccTypeDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnumainAccTypeSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainOwnerType As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainOwnerTypeDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainOwnerTypeSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainReportingType As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainReportingTypeDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainReportingTypeSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainTransType As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainTransTypeDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainTransTypeSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainAssDuration As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainAssDurationDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainAssDurationSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainBank As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainBankDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainBankSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainBranch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainBranchDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainBranchSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainRegAuth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainRegAuthDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainRegAuthSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainCountry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainCountryDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainCountrySum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainCurrency As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainCurrencyDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainCurrencySum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainDistrict As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainDistrictDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainDistrictSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainDivision As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainDivisionDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainDivisionSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainExecDesig As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainExecDesigDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainExecDesigSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainThana As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainThanaDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainThanaSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainOccuType As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainOccuTypeDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainOccuTypeDetSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuReportStatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportStatusFileImport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportTransaction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSystem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHelpAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DepartmentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysUserDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FunctionalGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysUserSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysDeptDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysDeptSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysFGDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysFGSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AssignFGToUserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysFGtoUserDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysFGtoUserSum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileLogout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysEERSFeedExport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportSystLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSysReportUserRole As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents applicationIdle As Winforms.Components.ApplicationIdle
    Friend WithEvents mnuSysRolePermission As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysRoleMenuPermmission As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysRoleFromPermission As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlMaintenance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FundsTypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlFundDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlFundSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AccountStatusTypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlAcStatusDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlAcStatusSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SubmissionTypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlSubTypeDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlSubTypeSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IdentifierTypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlIdentTypeDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlIdentTypeSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CommunicationTypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlCommuDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlCommuSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EntityLegalFormTypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlEntityDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlEntitySumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlAccountdet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlAccountSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConductionTypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlConductionTypeDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlConductionTypeSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContactTypeToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlContactTypeDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlContactTypeSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DivisiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlDivisionDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlDivisionSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DistrictToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlDistrictDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlDistrictSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ThanaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlThanaDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlThanaSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AccountPersonRoleTypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlAccountPersonRoleDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlAccountPersonRoleSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EntityPersonRoleTypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlEntityPersonRoleDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlEntityPersonRoleSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CurrenciesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlCurrenciesDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlCurrenciesSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CountryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlCountryDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlCountrySumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlReportDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlReportSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlAddressDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlAddressSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlPersonDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlPersonSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlOwnerDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlOwnerSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents nmugoAmlAccountInfoDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlAccountInfoSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransaction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoAMLTransactionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransgoAmlTransDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransGoAmlTransSumm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuToolsExportgoAml As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOccupationDet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOccupationSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsImportFlexCustGo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuToolsProcessRuleGo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileReadyGo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportMismatchFlexAcGo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportMismatchMissingOwnerGo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportTransactionGo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportStatusCustomergoAML As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportStatusunAuthTrans As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportStatusUnAuthCust As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAMLDirectorDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAMLDirectorSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAMLEntityPersonDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAMLEntityPersonSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAMLBearerDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAMLBearerSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnugoAMLImportEntityInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuImportOwnerInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImportgoAMLAccountInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnugoAmlBearerInfoImport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportOwnerInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransCTRTransaction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportMissAccountField As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuQualifiedTransgoAML As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSystemUserInactivity As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportMissingEntity As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportMissingDirectorInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuToolsImportCCMSTrans As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblServer As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuTransactionFlexTransaction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuQualifiedAccWithSignEntity As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportCTRvsGoAML As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportFlexvsCCMS As System.Windows.Forms.ToolStripMenuItem
End Class
