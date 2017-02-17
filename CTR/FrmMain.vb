Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Imports Winforms.Components.ApplicationIdleData

Public Class FrmMain

#Region "Global Variables"

    Private _hostForm As FrmLogin
    Dim MenuName As String
    Dim IsVisible As Boolean
    Dim IsEnable As Boolean
    Dim log_mesage As String = ""

#End Region


#Region "User defined code"


    Private Sub LoadMenuSec()
        Dim user = CommonAppSet.User
        Dim db As New SqlDatabase(CommonAppSet.ConnStr)
        Dim i As Integer
        Dim ds As New DataSet
        Dim comProc As DbCommand = db.GetStoredProcCommand("CTR_Users_FunMenuPermission")
        db.AddInParameter(comProc, "@USERS_ID", DbType.String, user)

        ds = db.ExecuteDataSet(comProc)
        For i = 0 To ds.Tables(0).Rows.Count - 1
            MenuName = ds.Tables(0).Rows(i)("MENU_NAME").ToString()
            IsVisible = ds.Tables(0).Rows(i)("IS_VISIBLE").ToString()
            IsEnable = ds.Tables(0).Rows(i)("IS_ENABLE").ToString
            MenuPerm()

        Next
        LoadAdminMenu()

    End Sub

    Private Sub LoadAdminMenu()
        If CommonAppSet.IsAdmin = True Then

            Me.mnuSystem.Visible = True
            Me.mnuSystem.Enabled = True
            Me.mnuSysDeptDet.Visible = True
            Me.mnuSysDeptDet.Enabled = True
            Me.mnuSysDeptSum.Visible = True
            Me.mnuSysDeptSum.Enabled = True
            Me.mnuSysUserDet.Visible = True
            Me.mnuSysUserDet.Enabled = True
            Me.mnuSysUserSum.Visible = True
            Me.mnuSysUserSum.Enabled = True
            Me.mnuSysFGDet.Visible = True
            Me.mnuSysFGDet.Enabled = True
            Me.mnuSysFGSum.Visible = True
            Me.mnuSysFGSum.Enabled = True
            Me.mnuSysFGtoUserDet.Visible = True
            Me.mnuSysFGtoUserDet.Enabled = True
            'Me.mnuSysFGtoUserDet.Visible = False
            Me.mnuSysFGtoUserSum.Visible = True
            Me.mnuSysFGtoUserSum.Enabled = True
            Me.mnuSysEERSFeedExport.Visible = True
            Me.mnuSysEERSFeedExport.Enabled = True
            Me.mnuSysReportUserRole.Visible = True
            Me.mnuSysReportUserRole.Enabled = True
            Me.mnuReportSystLog.Visible = True
            Me.mnuReportSystLog.Enabled = True
            Me.mnuSysRoleMenuPermmission.Enabled = True
            Me.mnuSysRoleMenuPermmission.Visible = True
            Me.mnuSysRoleFromPermission.Enabled = True
            Me.mnuSysRoleFromPermission.Visible = True
            Me.mnuSystemUserInactivity.Visible = True
            Me.mnuSystemUserInactivity.Enabled = True
        End If

    End Sub

    Private Sub MenuPerm()

        Select Case MenuName

            ' -------- Maintenance Menu Start -------

            Case "Maintenance"

                If IsVisible = True Then
                    Me.mnuMaintenance.Visible = True
                    If IsEnable = True Then
                        Me.mnuMaintenance.Enabled = True
                    Else
                        Me.mnuMaintenance.Enabled = False
                    End If
                Else
                    Me.mnuMaintenance.Visible = False
                End If

            Case "MaintenanceAccountDetail"

                If IsVisible = True Then
                    Me.mnuMainAccInfoDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainAccInfoDet.Enabled = True
                    Else
                        Me.mnuMainAccInfoDet.Enabled = False
                    End If
                Else
                    Me.mnuMainAccInfoDet.Visible = False
                End If

            Case "MaintenanceAccountSummary"

                If IsVisible = True Then
                    Me.mnuMainAccInfoSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainAccInfoSum.Enabled = True
                    Else
                        Me.mnuMainAccInfoSum.Enabled = False
                    End If
                Else
                    Me.mnuMainAccInfoSum.Visible = False
                End If

            Case "MaintenanceOwnerInfoDetail"

                If IsVisible = True Then
                    Me.mnuMainOwnerInfo.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainOwnerInfo.Enabled = True
                    Else
                        Me.mnuMainOwnerInfo.Enabled = False
                    End If
                Else
                    Me.mnuMainOwnerInfo.Visible = False

                End If

            Case "MaintenanceOwnerInfoSummary"

                If IsVisible = True Then
                    Me.mnuMainOwnerInfoDetSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainOwnerInfoDetSum.Enabled = True
                    Else
                        Me.mnuMainOwnerInfoDetSum.Enabled = False
                    End If
                Else
                    Me.mnuMainOwnerInfoDetSum.Visible = False

                End If

            Case "MaintenanceAcOwnerMappingDetail"

                If IsVisible = True Then
                    Me.mnuAcOwnerMappingDetail.Visible = True
                    If IsEnable = True Then
                        Me.mnuAcOwnerMappingDetail.Enabled = True
                    Else
                        Me.mnuAcOwnerMappingDetail.Enabled = False
                    End If
                Else
                    Me.mnuAcOwnerMappingDetail.Visible = False

                End If

            Case "MaintenanceAcOwnerMappingSummary"

                If IsVisible = True Then
                    Me.mnuAcOwnerMappingSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuAcOwnerMappingSum.Enabled = True
                    Else
                        Me.mnuAcOwnerMappingSum.Enabled = False
                    End If
                Else
                    Me.mnuAcOwnerMappingSum.Visible = False

                End If

            Case "MaintenanceAcTypeDetail"

                If IsVisible = True Then
                    Me.mnuMainAccTypeDetail.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainAccTypeDetail.Enabled = True
                    Else
                        Me.mnuMainAccTypeDetail.Enabled = False
                    End If
                Else
                    Me.mnuMainAccTypeDetail.Visible = False

                End If

            Case "MaintenanceAcTypeSummary"

                If IsVisible = True Then
                    Me.mnumainAccTypeSum.Visible = True
                    If IsEnable = True Then
                        Me.mnumainAccTypeSum.Enabled = True
                    Else
                        Me.mnumainAccTypeSum.Enabled = False
                    End If
                Else
                    Me.mnumainAccTypeSum.Visible = False

                End If


            Case "MaintenanceOwnerTypeDetail"

                If IsVisible = True Then
                    Me.mnuMainOwnerTypeDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainOwnerTypeDet.Enabled = True
                    Else
                        Me.mnuMainOwnerTypeDet.Enabled = False
                    End If
                Else
                    Me.mnuMainOwnerTypeDet.Visible = False

                End If

            Case "MaintenanceOwnerTypeSummary"

                If IsVisible = True Then
                    Me.mnuMainOwnerTypeSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainOwnerTypeSum.Enabled = True
                    Else
                        Me.mnuMainOwnerTypeSum.Enabled = False
                    End If
                Else
                    Me.mnuMainOwnerTypeSum.Visible = False

                End If

            Case "MaintenanceTransTypeDetail"

                If IsVisible = True Then
                    Me.mnuMainTransTypeDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainTransTypeDet.Enabled = True
                    Else
                        Me.mnuMainTransTypeDet.Enabled = False
                    End If
                Else
                    Me.mnuMainTransTypeDet.Visible = False

                End If

            Case "MaintenanceTransTypeSummary"

                If IsVisible = True Then
                    Me.mnuMainTransTypeSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainTransTypeSum.Enabled = True
                    Else
                        Me.mnuMainTransTypeSum.Enabled = False
                    End If
                Else
                    Me.mnuMainTransTypeSum.Visible = False

                End If

            Case "MaintenanceBankDetail"

                If IsVisible = True Then
                    Me.mnuMainBankDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainBankDet.Enabled = True
                    Else
                        Me.mnuMainBankDet.Enabled = False
                    End If
                Else
                    Me.mnuMainBankDet.Visible = False

                End If

            Case "MaintenanceBankSummary"

                If IsVisible = True Then
                    Me.mnuMainBankSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainBankSum.Enabled = True
                    Else
                        Me.mnuMainBankSum.Enabled = False
                    End If
                Else
                    Me.mnuMainBankSum.Visible = False

                End If

            Case "MaintenanceBranchDetail"

                If IsVisible = True Then
                    Me.mnuMainBranchDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainBranchDet.Enabled = True
                    Else
                        Me.mnuMainBranchDet.Enabled = False
                    End If
                Else
                    Me.mnuMainBranchDet.Visible = False

                End If

            Case "MaintenanceBranchSummary"

                If IsVisible = True Then
                    Me.mnuMainBranchSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainBranchSum.Enabled = True
                    Else
                        Me.mnuMainBranchSum.Enabled = False
                    End If
                Else
                    Me.mnuMainBranchSum.Visible = False

                End If

            Case "MaintenanceRegAuthorityDetail"

                If IsVisible = True Then
                    Me.mnuMainRegAuthDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainRegAuthDet.Enabled = True
                    Else
                        Me.mnuMainRegAuthDet.Enabled = False
                    End If
                Else
                    Me.mnuMainRegAuthDet.Visible = False

                End If

            Case "MaintenanceRegAuthoritySummary"

                If IsVisible = True Then
                    Me.mnuMainRegAuthSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainRegAuthSum.Enabled = True
                    Else
                        Me.mnuMainRegAuthSum.Enabled = False
                    End If
                Else
                    Me.mnuMainRegAuthSum.Visible = False

                End If

            Case "MaintenanceExecDesignitionDetail"

                If IsVisible = True Then
                    Me.mnuMainExecDesigDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainExecDesigDet.Enabled = True
                    Else
                        Me.mnuMainExecDesigDet.Enabled = False
                    End If
                Else
                    Me.mnuMainExecDesigDet.Visible = False

                End If

            Case "MaintenanceExecDesignitionSummary"

                If IsVisible = True Then
                    Me.mnuMainExecDesigSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainExecDesigSum.Enabled = True
                    Else
                        Me.mnuMainExecDesigSum.Enabled = False
                    End If
                Else
                    Me.mnuMainExecDesigSum.Visible = False

                End If

            Case "MaintenanceOccupationTypeDetail"

                If IsVisible = True Then
                    Me.mnuMainOccuTypeDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainOccuTypeDet.Enabled = True
                    Else
                        Me.mnuMainOccuTypeDet.Enabled = False
                    End If
                Else
                    Me.mnuMainOccuTypeDet.Visible = False

                End If

            Case "MaintenanceOccupationTypeSummary"

                If IsVisible = True Then
                    Me.mnuMainOccuTypeDetSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainOccuTypeDetSum.Enabled = True
                    Else
                        Me.mnuMainOccuTypeDetSum.Enabled = False
                    End If
                Else
                    Me.mnuMainOccuTypeDetSum.Visible = False

                End If

            Case "MaintenanceDivisionDetail"

                If IsVisible = True Then
                    Me.mnuMainDivisionDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainDivisionDet.Enabled = True
                    Else
                        Me.mnuMainDivisionDet.Enabled = False
                    End If
                Else
                    Me.mnuMainDivisionDet.Visible = False

                End If

            Case "MaintenanceDivisionSummary"

                If IsVisible = True Then
                    Me.mnuMainDivisionSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainDivisionSum.Enabled = True
                    Else
                        Me.mnuMainDivisionSum.Enabled = False
                    End If
                Else
                    Me.mnuMainDivisionSum.Visible = False

                End If


            Case "MaintenanceDistrictDetail"

                If IsVisible = True Then
                    Me.mnuMainDistrictDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainDistrictDet.Enabled = True
                    Else
                        Me.mnuMainDistrictDet.Enabled = False
                    End If
                Else
                    Me.mnuMainDistrictDet.Visible = False

                End If

            Case "MaintenanceDistrictSummary"

                If IsVisible = True Then
                    Me.mnuMainDistrictSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainDistrictSum.Enabled = True
                    Else
                        Me.mnuMainDistrictSum.Enabled = False
                    End If
                Else
                    Me.mnuMainDistrictSum.Visible = False

                End If

            Case "MaintenanceThanaDetail"

                If IsVisible = True Then
                    Me.mnuMainThanaDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainThanaDet.Enabled = True
                    Else
                        Me.mnuMainThanaDet.Enabled = False
                    End If
                Else
                    Me.mnuMainThanaDet.Visible = False

                End If

            Case "MaintenanceThanaSummary"

                If IsVisible = True Then
                    Me.mnuMainThanaSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainThanaSum.Enabled = True
                    Else
                        Me.mnuMainThanaSum.Enabled = False
                    End If
                Else
                    Me.mnuMainThanaSum.Visible = False

                End If

            Case "MaintenanceCountryDetail"

                If IsVisible = True Then
                    Me.mnuMainCountryDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainCountryDet.Enabled = True
                    Else
                        Me.mnuMainCountryDet.Enabled = False
                    End If
                Else
                    Me.mnuMainCountryDet.Visible = False

                End If

            Case "MaintenanceCountrySummary"

                If IsVisible = True Then
                    Me.mnuMainCountrySum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainCountrySum.Enabled = True
                    Else
                        Me.mnuMainCountrySum.Enabled = False
                    End If
                Else
                    Me.mnuMainCountrySum.Visible = False

                End If

            Case "MaintenanceCurrencyDetail"

                If IsVisible = True Then
                    Me.mnuMainCurrencyDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainCurrencyDet.Enabled = True
                    Else
                        Me.mnuMainCurrencyDet.Enabled = False
                    End If
                Else
                    Me.mnuMainCurrencyDet.Visible = False

                End If

            Case "MaintenanceCurrencySummary"

                If IsVisible = True Then
                    Me.mnuMainCurrencySum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainCurrencySum.Enabled = True
                    Else
                        Me.mnuMainCurrencySum.Enabled = False
                    End If
                Else
                    Me.mnuMainCurrencySum.Visible = False

                End If

            Case "MaintenanceAssesmentDurationDetail"

                If IsVisible = True Then
                    Me.mnuMainAssDurationDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainAssDurationDet.Enabled = True
                    Else
                        Me.mnuMainAssDurationDet.Enabled = False
                    End If
                Else
                    Me.mnuMainAssDurationDet.Visible = False

                End If

            Case "MaintenanceAssesmentDurationSummary"

                If IsVisible = True Then
                    Me.mnuMainAssDurationSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainAssDurationSum.Enabled = True
                    Else
                        Me.mnuMainAssDurationSum.Enabled = False
                    End If
                Else
                    Me.mnuMainAssDurationSum.Visible = False

                End If

            Case "MaintenanceReportingTypeDetail"

                If IsVisible = True Then
                    Me.mnuMainReportingTypeDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainReportingTypeDet.Enabled = True
                    Else
                        Me.mnuMainReportingTypeDet.Enabled = False
                    End If
                Else
                    Me.mnuMainReportingTypeDet.Visible = False

                End If

            Case "MaintenanceReportingTypeSummary"

                If IsVisible = True Then
                    Me.mnuMainReportingTypeSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuMainReportingTypeSum.Enabled = True
                    Else
                        Me.mnuMainReportingTypeSum.Enabled = False
                    End If
                Else
                    Me.mnuMainReportingTypeSum.Visible = False

                End If

                ' -------- Maintenance Menu End -------

                ' -------- Tools Menu Start -------

            Case "Tools"

                If IsVisible = True Then
                    Me.mnuTools.Visible = True
                    If IsEnable = True Then
                        Me.mnuTools.Enabled = True
                    Else
                        Me.mnuTools.Enabled = False
                    End If
                Else
                    Me.mnuTools.Visible = False

                End If

            Case "ToolsImportFlexTransaction"

                If IsVisible = True Then
                    Me.mnuToolsImportFlexTrans.Visible = True
                    If IsEnable = True Then
                        Me.mnuToolsImportFlexTrans.Enabled = True
                    Else
                        Me.mnuToolsImportFlexTrans.Enabled = False
                    End If
                Else
                    Me.mnuToolsImportFlexTrans.Visible = False

                End If

            Case "ToolsImportFlexCustomer"

                If IsVisible = True Then
                    Me.mnuToolsImportFlexCust.Visible = True
                    If IsEnable = True Then
                        Me.mnuToolsImportFlexCust.Enabled = True
                    Else
                        Me.mnuToolsImportFlexCust.Enabled = False
                    End If
                Else
                    Me.mnuToolsImportFlexCust.Visible = False

                End If

            Case "ToolsImportCCMSTransaction"

                If IsVisible = True Then
                    Me.mnuToolsImportCCMSTrans.Visible = True
                    If IsEnable = True Then
                        Me.mnuToolsImportCCMSTrans.Enabled = True
                    Else
                        Me.mnuToolsImportCCMSTrans.Enabled = False
                    End If
                Else
                    Me.mnuToolsImportCCMSTrans.Visible = False

                End If


            Case "ToolsProcessByRule"

                If IsVisible = True Then
                    Me.mnuToolsProcessRule.Visible = True
                    If IsEnable = True Then
                        Me.mnuToolsProcessRule.Enabled = True
                    Else
                        Me.mnuToolsProcessRule.Enabled = False
                    End If
                Else
                    Me.mnuToolsProcessRule.Visible = False

                End If

            Case "ToolsProcessReadyFileGeneration"

                If IsVisible = True Then
                    Me.mnuToolsProcessFileReady.Visible = True
                    If IsEnable = True Then
                        Me.mnuToolsProcessFileReady.Enabled = True
                    Else
                        Me.mnuToolsProcessFileReady.Enabled = False
                    End If
                Else
                    Me.mnuToolsProcessFileReady.Visible = False

                End If


            Case "ToolsExport"

                If IsVisible = True Then
                    Me.mnuToolsExport.Visible = True
                    If IsEnable = True Then
                        Me.mnuToolsExport.Enabled = True
                    Else
                        Me.mnuToolsExport.Enabled = False
                    End If
                Else
                    Me.mnuToolsExport.Visible = False

                End If



                ' -------- Tools Menu End -------
                ' -------- Report Menu Start -------

            Case "Report"

                If IsVisible = True Then
                    Me.mnuReport.Visible = True
                    If IsEnable = True Then
                        Me.mnuReport.Enabled = True
                    Else
                        Me.mnuReport.Enabled = False
                    End If
                Else
                    Me.mnuReport.Visible = False

                End If

            Case "ReportMismatchFlexAccount"

                If IsVisible = True Then
                    Me.mnuReportMismatchFlexAc.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportMismatchFlexAc.Enabled = True
                    Else
                        Me.mnuReportMismatchFlexAc.Enabled = False
                    End If
                Else
                    Me.mnuReportMismatchFlexAc.Visible = False

                End If

            Case "ReportMismatchProcessTransactionAc"

                If IsVisible = True Then
                    Me.ProcessTransactionWithACInfoToolStripMenuItem.Visible = True
                    If IsEnable = True Then
                        Me.ProcessTransactionWithACInfoToolStripMenuItem.Enabled = True
                    Else
                        Me.ProcessTransactionWithACInfoToolStripMenuItem.Enabled = False
                    End If
                Else
                    Me.ProcessTransactionWithACInfoToolStripMenuItem.Visible = False

                End If


            Case "ReportMismatchMissingOwner"

                If IsVisible = True Then
                    Me.mnuReportMismatchMissingOwner.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportMismatchMissingOwner.Enabled = True
                    Else
                        Me.mnuReportMismatchMissingOwner.Enabled = False
                    End If
                Else
                    Me.mnuReportMismatchMissingOwner.Visible = False

                End If

            Case "ReportStatusFileImport"

                If IsVisible = True Then
                    Me.mnuReportStatusFileImport.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportStatusFileImport.Enabled = True
                    Else
                        Me.mnuReportStatusFileImport.Enabled = False
                    End If
                Else
                    Me.mnuReportStatusFileImport.Visible = False

                End If

            Case "ReportFileImportCustomerStatusGOAML"

                If IsVisible = True Then
                    Me.mnuReportStatusCustomergoAML.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportStatusCustomergoAML.Enabled = True
                    Else
                        Me.mnuReportStatusCustomergoAML.Enabled = False
                    End If
                Else
                    Me.mnuReportStatusCustomergoAML.Visible = False

                End If

            Case "ReportTransaction"

                If IsVisible = True Then
                    Me.mnuReportTransaction.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportTransaction.Enabled = True
                    Else
                        Me.mnuReportTransaction.Enabled = False
                    End If
                Else
                    Me.mnuReportTransaction.Visible = False

                End If

                ' -------- Report Menu End -------
                ' -------- System Menu Start -------

            Case "System"

                If IsVisible = True Then
                    Me.mnuSystem.Visible = True
                    If IsEnable = True Then
                        Me.mnuSystem.Enabled = True
                    Else
                        Me.mnuSystem.Enabled = False
                    End If
                Else
                    Me.mnuSystem.Visible = False

                End If

            Case "SystemReportSystemLog"

                If IsVisible = True Then
                    Me.mnuReportSystLog.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportSystLog.Enabled = True
                    Else
                        Me.mnuReportSystLog.Enabled = False
                    End If
                Else
                    Me.mnuReportSystLog.Visible = False

                End If


            Case "SystemDepartDetail"

                If IsVisible = True Then
                    Me.mnuSysDeptDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysDeptDet.Enabled = True
                    Else
                        Me.mnuSysDeptDet.Enabled = False
                    End If
                Else
                    Me.mnuSysDeptDet.Visible = False

                End If

            Case "SystemDepartSummary"

                If IsVisible = True Then
                    Me.mnuSysDeptSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysDeptSum.Enabled = True
                    Else
                        Me.mnuSysDeptSum.Enabled = False
                    End If
                Else
                    Me.mnuSysDeptSum.Visible = False

                End If

            Case "SystemUsersDetail"

                If IsVisible = True Then
                    Me.mnuSysUserDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysUserDet.Enabled = True
                    Else
                        Me.mnuSysUserDet.Enabled = False
                    End If
                Else
                    Me.mnuSysUserDet.Visible = False

                End If

            Case "SystemUsersSummary"

                If IsVisible = True Then
                    Me.mnuSysUserSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysUserSum.Enabled = True
                    Else
                        Me.mnuSysUserSum.Enabled = False
                    End If
                Else
                    Me.mnuSysUserSum.Visible = False

                End If

            Case "SystemFGDetail"

                If IsVisible = True Then
                    Me.mnuSysFGDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysFGDet.Enabled = True
                    Else
                        Me.mnuSysFGDet.Enabled = False
                    End If
                Else
                    Me.mnuSysFGDet.Visible = False

                End If


            Case "SystemFGSummary"

                If IsVisible = True Then
                    Me.mnuSysFGSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysFGSum.Enabled = True
                    Else
                        Me.mnuSysFGSum.Enabled = False
                    End If
                Else
                    Me.mnuSysFGSum.Visible = False

                End If

            Case "SystemFGtoUserDetail"

                If IsVisible = True Then
                    Me.mnuSysFGtoUserDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysFGtoUserDet.Enabled = True
                    Else
                        Me.mnuSysFGtoUserDet.Enabled = False
                    End If
                Else
                    Me.mnuSysFGtoUserDet.Visible = False

                End If


            Case "SystemFGtoUserSummary"

                If IsVisible = True Then
                    Me.mnuSysFGtoUserSum.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysFGtoUserSum.Enabled = True
                    Else
                        Me.mnuSysFGtoUserSum.Enabled = False
                    End If
                Else
                    Me.mnuSysFGtoUserSum.Visible = False

                End If

            Case "SystemEERSFeedExport"

                If IsVisible = True Then
                    Me.mnuSysEERSFeedExport.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysEERSFeedExport.Enabled = True
                    Else
                        Me.mnuSysEERSFeedExport.Enabled = False
                    End If
                Else
                    Me.mnuSysEERSFeedExport.Visible = False

                End If

            Case "SystemReportUserRole"

                If IsVisible = True Then
                    Me.mnuSysReportUserRole.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysReportUserRole.Enabled = True
                    Else
                        Me.mnuSysReportUserRole.Enabled = False
                    End If
                Else
                    Me.mnuSysReportUserRole.Visible = False

                End If

            Case "SystemReportRolePermMenu"

                If IsVisible = True Then
                    Me.mnuSysRoleMenuPermmission.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysRoleMenuPermmission.Enabled = True
                    Else
                        Me.mnuSysRoleMenuPermmission.Enabled = False
                    End If
                Else
                    Me.mnuSysRoleMenuPermmission.Visible = False

                End If


            Case "SystemReportRolePermForm"

                If IsVisible = True Then
                    Me.mnuSysRoleFromPermission.Visible = True
                    If IsEnable = True Then
                        Me.mnuSysRoleFromPermission.Enabled = True
                    Else
                        Me.mnuSysRoleFromPermission.Enabled = False
                    End If
                Else
                    Me.mnuSysRoleFromPermission.Visible = False

                End If

                ' -------- System Menu End -------
                '---------- GoAML menu Start ------


            Case "MaintenanceGoAml"

                If IsVisible = True Then
                    Me.mnugoAmlMaintenance.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlMaintenance.Enabled = True
                    Else
                        Me.mnugoAmlMaintenance.Enabled = False
                    End If
                Else
                    Me.mnugoAmlMaintenance.Visible = False

                End If

            Case "MaintenanceGoOwnerDet"

                If IsVisible = True Then
                    Me.mnugoAmlOwnerDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlOwnerDet.Enabled = True
                    Else
                        Me.mnugoAmlOwnerDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlOwnerDet.Visible = False

                End If

            Case "MaintenanceGoOwnerSumm"

                If IsVisible = True Then
                    Me.mnugoAmlOwnerSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlOwnerSumm.Enabled = True
                    Else
                        Me.mnugoAmlOwnerSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlOwnerSumm.Visible = False

                End If

            Case "MaintenanceGoAccountInfoDet"

                If IsVisible = True Then
                    Me.nmugoAmlAccountInfoDet.Visible = True
                    If IsEnable = True Then
                        Me.nmugoAmlAccountInfoDet.Enabled = True
                    Else
                        Me.nmugoAmlAccountInfoDet.Enabled = False
                    End If
                Else
                    Me.nmugoAmlAccountInfoDet.Visible = False

                End If

            Case "MaintenanceGoAccountInfoSumm"

                If IsVisible = True Then
                    Me.mnugoAmlAccountInfoSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlAccountInfoSumm.Enabled = True
                    Else
                        Me.mnugoAmlAccountInfoSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlAccountInfoSumm.Visible = False

                End If

            Case "MaintenanceGoReportDet"

                If IsVisible = True Then
                    Me.mnugoAmlReportDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlReportDet.Enabled = True
                    Else
                        Me.mnugoAmlReportDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlReportDet.Visible = False

                End If

            Case "MaintenanceGoReportSumm"

                If IsVisible = True Then
                    Me.mnugoAmlReportSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlReportSumm.Enabled = True
                    Else
                        Me.mnugoAmlReportSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlReportSumm.Visible = False

                End If

            Case "MaintenanceGoReportPersonDet"

                If IsVisible = True Then
                    Me.mnugoAmlPersonDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlPersonDet.Enabled = True
                    Else
                        Me.mnugoAmlPersonDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlPersonDet.Visible = False

                End If

            Case "MaintenanceGoReportPersonSumm"

                If IsVisible = True Then
                    Me.mnugoAmlPersonSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlPersonSumm.Enabled = True
                    Else
                        Me.mnugoAmlPersonSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlPersonSumm.Visible = False

                End If

            Case "MaintenanceGoAddressDet"

                If IsVisible = True Then
                    Me.mnugoAmlAddressDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlAddressDet.Enabled = True
                    Else
                        Me.mnugoAmlAddressDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlAddressDet.Visible = False

                End If

            Case "MaintenanceGoAddressSumm"

                If IsVisible = True Then
                    Me.mnugoAmlAddressSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlAddressSumm.Enabled = True
                    Else
                        Me.mnugoAmlAddressSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlAddressSumm.Visible = False

                End If

            Case "MaintenanceGoAccountTypeDet"

                If IsVisible = True Then
                    Me.mnugoAmlAccountdet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlAccountdet.Enabled = True
                    Else
                        Me.mnugoAmlAccountdet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlAccountdet.Visible = False

                End If

            Case "MaintenanceGoACCountTypeSumm"

                If IsVisible = True Then
                    Me.mnugoAmlAccountSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlAccountSumm.Enabled = True
                    Else
                        Me.mnugoAmlAccountSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlAccountSumm.Visible = False

                End If

            Case "MaintenanceGoAccountStatusDet"

                If IsVisible = True Then
                    Me.mnugoAmlAcStatusDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlAcStatusDet.Enabled = True
                    Else
                        Me.mnugoAmlAcStatusDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlAcStatusDet.Visible = False

                End If

            Case "MaintenanceGoAccountStatusSumm"

                If IsVisible = True Then
                    Me.mnugoAmlAcStatusSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlAcStatusSumm.Enabled = True
                    Else
                        Me.mnugoAmlAcStatusSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlAcStatusSumm.Visible = False

                End If

            Case "MaintenanceGoAccountPersonRoleDet"

                If IsVisible = True Then
                    Me.mnugoAmlAccountPersonRoleDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlAccountPersonRoleDet.Enabled = True
                    Else
                        Me.mnugoAmlAccountPersonRoleDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlAccountPersonRoleDet.Visible = False

                End If

            Case "MaintenanceGoAccountPersonRoleSumm"

                If IsVisible = True Then
                    Me.mnugoAmlAccountPersonRoleSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlAccountPersonRoleSumm.Enabled = True
                    Else
                        Me.mnugoAmlAccountPersonRoleSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlAccountPersonRoleSumm.Visible = False

                End If

            Case "MaintenanceGoCommunicationDet"

                If IsVisible = True Then
                    Me.mnugoAmlCommuDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlCommuDet.Enabled = True
                    Else
                        Me.mnugoAmlCommuDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlCommuDet.Visible = False

                End If

            Case "MaintenanceGoCommunicationSumm"

                If IsVisible = True Then
                    Me.mnugoAmlCommuSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlCommuSumm.Enabled = True
                    Else
                        Me.mnugoAmlCommuSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlCommuSumm.Visible = False

                End If

            Case "MaintenanceGoConductionDet"

                If IsVisible = True Then
                    Me.mnugoAmlConductionTypeDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlConductionTypeDet.Enabled = True
                    Else
                        Me.mnugoAmlConductionTypeDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlConductionTypeDet.Visible = False

                End If

            Case "MaintenanceGoConductionSumm"

                If IsVisible = True Then
                    Me.mnugoAmlConductionTypeSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlConductionTypeSumm.Enabled = True
                    Else
                        Me.mnugoAmlConductionTypeSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlConductionTypeSumm.Visible = False

                End If

            Case "MaintenanceGoContactTypeDet"

                If IsVisible = True Then
                    Me.mnugoAmlContactTypeDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlContactTypeDet.Enabled = True
                    Else
                        Me.mnugoAmlContactTypeDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlContactTypeDet.Visible = False

                End If

            Case "MaintenanceGoContactTypeSumm"

                If IsVisible = True Then
                    Me.mnugoAmlContactTypeSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlContactTypeSumm.Enabled = True
                    Else
                        Me.mnugoAmlContactTypeSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlContactTypeSumm.Visible = False

                End If

            Case "MaintenanceGoCountryTypeDet"

                If IsVisible = True Then
                    Me.mnugoAmlCountryDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlCountryDet.Enabled = True
                    Else
                        Me.mnugoAmlCountryDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlCountryDet.Visible = False

                End If

            Case "MaintenanceGoCountryTypeSumm"

                If IsVisible = True Then
                    Me.mnugoAmlCountrySumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlCountrySumm.Enabled = True
                    Else
                        Me.mnugoAmlCountrySumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlCountrySumm.Visible = False

                End If

            Case "MaintenanceGoEntityLegalDet"

                If IsVisible = True Then
                    Me.mnugoAmlEntityDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlEntityDet.Enabled = True
                    Else
                        Me.mnugoAmlEntityDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlEntityDet.Visible = False

                End If

            Case "MaintenanceGoEntityLegalSumm"

                If IsVisible = True Then
                    Me.mnugoAmlEntitySumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlEntitySumm.Enabled = True
                    Else
                        Me.mnugoAmlEntitySumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlEntitySumm.Visible = False

                End If

            Case "MaintenanceGoEntityPersonRoleDet"

                If IsVisible = True Then
                    Me.mnugoAmlEntityPersonRoleDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlEntityPersonRoleDet.Enabled = True
                    Else
                        Me.mnugoAmlEntityPersonRoleDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlEntityPersonRoleDet.Visible = False

                End If

            Case "MaintenanceGoEntityPersonRoleSumm"

                If IsVisible = True Then
                    Me.mnugoAmlEntityPersonRoleSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlEntityPersonRoleSumm.Enabled = True
                    Else
                        Me.mnugoAmlEntityPersonRoleSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlEntityPersonRoleSumm.Visible = False

                End If

            Case "MaintenanceGoFundTypeDet"

                If IsVisible = True Then
                    Me.mnugoAmlFundDetail.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlFundDetail.Enabled = True
                    Else
                        Me.mnugoAmlFundDetail.Enabled = False
                    End If
                Else
                    Me.mnugoAmlFundDetail.Visible = False

                End If

            Case "MaintenanceGoFundTypeSumm"

                If IsVisible = True Then
                    Me.mnugoAmlFundSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlFundSumm.Enabled = True
                    Else
                        Me.mnugoAmlFundSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlFundSumm.Visible = False

                End If

            Case "MaintenanceGoIdentifierTypeDet"

                If IsVisible = True Then
                    Me.mnugoAmlIdentTypeDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlIdentTypeDet.Enabled = True
                    Else
                        Me.mnugoAmlIdentTypeDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlIdentTypeDet.Visible = False

                End If

            Case "MaintenanceGoIdentifierTypeSumm"

                If IsVisible = True Then
                    Me.mnugoAmlIdentTypeSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlIdentTypeSumm.Enabled = True
                    Else
                        Me.mnugoAmlIdentTypeSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlIdentTypeSumm.Visible = False

                End If

            Case "MaintenanceGoSubmissionTypeDet"

                If IsVisible = True Then
                    Me.mnugoAmlSubTypeDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlSubTypeDet.Enabled = True
                    Else
                        Me.mnugoAmlSubTypeDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlSubTypeDet.Visible = False

                End If

            Case "MaintenanceGoSubmissionTypeSumm"

                If IsVisible = True Then
                    Me.mnugoAmlSubTypeSummary.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlSubTypeSummary.Enabled = True
                    Else
                        Me.mnugoAmlSubTypeSummary.Enabled = False
                    End If
                Else
                    Me.mnugoAmlSubTypeSummary.Visible = False

                End If


            Case "MaintenanceGoThanaDet"

                If IsVisible = True Then
                    Me.mnugoAmlThanaDetail.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlThanaDetail.Enabled = True
                    Else
                        Me.mnugoAmlThanaDetail.Enabled = False
                    End If
                Else
                    Me.mnugoAmlThanaDetail.Visible = False

                End If

            Case "MaintenanceGoThanaSummary"

                If IsVisible = True Then
                    Me.mnugoAmlThanaSummary.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlThanaSummary.Enabled = True
                    Else
                        Me.mnugoAmlThanaSummary.Enabled = False
                    End If
                Else
                    Me.mnugoAmlThanaSummary.Visible = False

                End If

            Case "MaintenanceGoDivisionDet"

                If IsVisible = True Then
                    Me.mnugoAmlDivisionDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlDivisionDet.Enabled = True
                    Else
                        Me.mnugoAmlDivisionDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlDivisionDet.Visible = False

                End If


            Case "MaintenanceGoDivisionSumm"

                If IsVisible = True Then
                    Me.mnugoAmlDivisionSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlDivisionSumm.Enabled = True
                    Else
                        Me.mnugoAmlDivisionSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlDivisionSumm.Visible = False

                End If

            Case "MaintenanceGoDistrictDet"

                If IsVisible = True Then
                    Me.mnugoAmlDistrictDet.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlDistrictDet.Enabled = True
                    Else
                        Me.mnugoAmlDistrictDet.Enabled = False
                    End If
                Else
                    Me.mnugoAmlDistrictDet.Visible = False

                End If

            Case "MaintenanceGoDistrictSumm"

                If IsVisible = True Then
                    Me.mnugoAmlDistrictSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlDistrictSumm.Enabled = True
                    Else
                        Me.mnugoAmlDistrictSumm.Enabled = False
                    End If
                Else
                    Me.mnugoAmlDistrictSumm.Visible = False

                End If

            Case "MaintenanceGoBearerDetail"

                If IsVisible = True Then
                    Me.mnugoAMLBearerDetail.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAMLBearerDetail.Enabled = True
                    Else
                        Me.mnugoAMLBearerDetail.Enabled = False
                    End If
                Else
                    Me.mnugoAMLBearerDetail.Visible = False

                End If

            Case "MaintenanceGoBearerSummary"

                If IsVisible = True Then
                    Me.mnugoAMLBearerSummary.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAMLBearerSummary.Enabled = True
                    Else
                        Me.mnugoAMLBearerSummary.Enabled = False
                    End If
                Else
                    Me.mnugoAMLBearerSummary.Visible = False

                End If

            Case "MaintenanceGoEntityPersonDetail"

                If IsVisible = True Then
                    Me.mnugoAMLEntityPersonDetail.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAMLEntityPersonDetail.Enabled = True
                    Else
                        Me.mnugoAMLEntityPersonDetail.Enabled = False
                    End If
                Else
                    Me.mnugoAMLEntityPersonDetail.Visible = False

                End If


            Case "MaintenanceGoEntityPersonSummary"

                If IsVisible = True Then
                    Me.mnugoAMLEntityPersonSummary.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAMLEntityPersonSummary.Enabled = True
                    Else
                        Me.mnugoAMLEntityPersonSummary.Enabled = False
                    End If
                Else
                    Me.mnugoAMLEntityPersonSummary.Visible = False

                End If

            Case "MaintenanceGoDirectorDetail"

                If IsVisible = True Then
                    Me.mnugoAMLDirectorDetail.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAMLDirectorDetail.Enabled = True
                    Else
                        Me.mnugoAMLDirectorDetail.Enabled = False
                    End If
                Else
                    Me.mnugoAMLDirectorDetail.Visible = False

                End If

            Case "MaintenanceGoDirectorSummary"

                If IsVisible = True Then
                    Me.mnugoAMLDirectorSummary.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAMLDirectorSummary.Enabled = True
                    Else
                        Me.mnugoAMLDirectorSummary.Enabled = False
                    End If
                Else
                    Me.mnugoAMLDirectorSummary.Visible = False

                End If




            Case "MenuTransaction"

                If IsVisible = True Then
                    Me.mnuTransaction.Visible = True
                    If IsEnable = True Then
                        Me.mnuTransaction.Enabled = True
                    Else
                        Me.mnuTransaction.Enabled = False
                    End If
                Else
                    Me.mnuTransaction.Visible = False

                End If


            Case "TransactionGoTransDet"

                If IsVisible = True Then
                    Me.mnuTransgoAmlTransDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuTransgoAmlTransDet.Enabled = True
                    Else
                        Me.mnuTransgoAmlTransDet.Enabled = False
                    End If
                Else
                    Me.mnuTransgoAmlTransDet.Visible = False

                End If

            Case "TransactionGoTransSumm"

                If IsVisible = True Then
                    Me.mnuTransGoAmlTransSumm.Visible = True
                    If IsEnable = True Then
                        Me.mnuTransGoAmlTransSumm.Enabled = True
                    Else
                        Me.mnuTransGoAmlTransSumm.Enabled = False
                    End If
                Else
                    Me.mnuTransGoAmlTransSumm.Visible = False

                End If

            Case "ToolsExportgoAML"

                If IsVisible = True Then
                    Me.mnuToolsExportgoAml.Visible = True
                    If IsEnable = True Then
                        Me.mnuToolsExportgoAml.Enabled = True
                    Else
                        Me.mnuToolsExportgoAml.Enabled = False
                    End If
                Else
                    Me.mnuToolsExportgoAml.Visible = False

                End If

            Case "MaintenanceOccupationTypeDet"

                If IsVisible = True Then
                    Me.mnuOccupationDet.Visible = True
                    If IsEnable = True Then
                        Me.mnuOccupationDet.Enabled = True
                    Else
                        Me.mnuOccupationDet.Enabled = False
                    End If
                Else
                    Me.mnuOccupationDet.Visible = False

                End If



            Case "MaintenanceOccupationTypeSummary"

                If IsVisible = True Then
                    Me.mnuOccupationSummary.Visible = True
                    If IsEnable = True Then
                        Me.mnuOccupationSummary.Enabled = True
                    Else
                        Me.mnuOccupationSummary.Enabled = False
                    End If
                Else
                    Me.mnuOccupationSummary.Visible = False

                End If

                '-----
            Case "MaintenanceGoOwnerInfoImport"

                If IsVisible = True Then
                    Me.mnuImportOwnerInfo.Visible = True
                    If IsEnable = True Then
                        Me.mnuImportOwnerInfo.Enabled = True
                    Else
                        Me.mnuImportOwnerInfo.Enabled = False
                    End If
                Else
                    Me.mnuImportOwnerInfo.Visible = False

                End If



            Case "MaintenanceGoEntityInfoImport"

                If IsVisible = True Then
                    Me.mnugoAMLImportEntityInfo.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAMLImportEntityInfo.Enabled = True
                    Else
                        Me.mnugoAMLImportEntityInfo.Enabled = False
                    End If
                Else
                    Me.mnugoAMLImportEntityInfo.Visible = False

                End If

                '-----

            Case "ToolsImportFlexCustomerGoAML"

                If IsVisible = True Then
                    Me.mnuToolsImportFlexCustGo.Visible = True
                    If IsEnable = True Then
                        Me.mnuToolsImportFlexCustGo.Enabled = True
                    Else
                        Me.mnuToolsImportFlexCustGo.Enabled = False
                    End If
                Else
                    Me.mnuToolsImportFlexCustGo.Visible = False

                End If

            Case "ToolsProcessRuleGoAML"

                If IsVisible = True Then
                    Me.mnuToolsProcessRuleGo.Visible = True
                    If IsEnable = True Then
                        Me.mnuToolsProcessRuleGo.Enabled = True
                    Else
                        Me.mnuToolsProcessRuleGo.Enabled = False
                    End If
                Else
                    Me.mnuToolsProcessRuleGo.Visible = False

                End If

            Case "ToolsProcessFileReadyGoAML"

                If IsVisible = True Then
                    Me.FileReadyGo.Visible = True
                    If IsEnable = True Then
                        Me.FileReadyGo.Enabled = True
                    Else
                        Me.FileReadyGo.Enabled = False
                    End If
                Else
                    Me.FileReadyGo.Visible = False

                End If

            Case "ReportMissmatchFlexAccountGOAML"

                If IsVisible = True Then
                    Me.mnuReportMismatchFlexAcGo.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportMismatchFlexAcGo.Enabled = True
                    Else
                        Me.mnuReportMismatchFlexAcGo.Enabled = False
                    End If
                Else
                    Me.mnuReportMismatchFlexAcGo.Visible = False

                End If

            Case "ReportMissingOwnerGOAML"

                If IsVisible = True Then
                    Me.mnuReportMismatchMissingOwnerGo.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportMismatchMissingOwnerGo.Enabled = True
                    Else
                        Me.mnuReportMismatchMissingOwnerGo.Enabled = False
                    End If
                Else
                    Me.mnuReportMismatchMissingOwnerGo.Visible = False

                End If


            Case "ReportTransactionByRuleGOAML"

                If IsVisible = True Then
                    Me.mnuReportTransactionGo.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportTransactionGo.Enabled = True
                    Else
                        Me.mnuReportTransactionGo.Enabled = False
                    End If
                Else
                    Me.mnuReportTransactionGo.Visible = False

                End If

            Case "ReportStatusUnAuthorizedTransaction"

                If IsVisible = True Then
                    Me.mnuReportStatusunAuthTrans.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportStatusunAuthTrans.Enabled = True
                    Else
                        Me.mnuReportStatusunAuthTrans.Enabled = False
                    End If
                Else
                    Me.mnuReportStatusunAuthTrans.Visible = False

                End If

            Case "ReportStatusUnAuthorizedCustomer"

                If IsVisible = True Then
                    Me.mnuReportStatusUnAuthCust.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportStatusUnAuthCust.Enabled = True
                    Else
                        Me.mnuReportStatusUnAuthCust.Enabled = False
                    End If
                Else
                    Me.mnuReportStatusUnAuthCust.Visible = False

                End If

            Case "MaintenanceGoAccountInfoImport"

                If IsVisible = True Then
                    Me.mnuImportgoAMLAccountInfo.Visible = True
                    If IsEnable = True Then
                        Me.mnuImportgoAMLAccountInfo.Enabled = True
                    Else
                        Me.mnuImportgoAMLAccountInfo.Enabled = False
                    End If
                Else
                    Me.mnuImportgoAMLAccountInfo.Visible = False

                End If

            Case "MaintenanceGoBearerInfoImport"

                If IsVisible = True Then
                    Me.mnugoAmlBearerInfoImport.Visible = True
                    If IsEnable = True Then
                        Me.mnugoAmlBearerInfoImport.Enabled = True
                    Else
                        Me.mnugoAmlBearerInfoImport.Enabled = False
                    End If
                Else
                    Me.mnugoAmlBearerInfoImport.Visible = False

                End If

            Case "MaintenanceReportCTRTransaction"

                If IsVisible = True Then
                    Me.mnuTransCTRTransaction.Visible = True
                    If IsEnable = True Then
                        Me.mnuTransCTRTransaction.Enabled = True
                    Else
                        Me.mnuTransCTRTransaction.Enabled = False
                    End If
                Else
                    Me.mnuTransCTRTransaction.Visible = False

                End If

            Case "MaintenanceReportOwnerInfo"

                If IsVisible = True Then
                    Me.mnuReportOwnerInfo.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportOwnerInfo.Enabled = True
                    Else
                        Me.mnuReportOwnerInfo.Enabled = False
                    End If
                Else
                    Me.mnuReportOwnerInfo.Visible = False

                End If

            Case "ReportMissingAccountRequiredFieldGo"

                If IsVisible = True Then
                    Me.mnuReportMissAccountField.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportMissAccountField.Enabled = True
                    Else
                        Me.mnuReportMissAccountField.Enabled = False
                    End If
                Else
                    Me.mnuReportMissAccountField.Visible = False

                End If

            Case "ReportQualifiedTransgoAML"

                If IsVisible = True Then
                    Me.mnuQualifiedTransgoAML.Visible = True
                    If IsEnable = True Then
                        Me.mnuQualifiedTransgoAML.Enabled = True
                    Else
                        Me.mnuQualifiedTransgoAML.Enabled = False
                    End If
                Else
                    Me.mnuQualifiedTransgoAML.Visible = False

                End If

            Case "SystemReportUserInactivity"

                If IsVisible = True Then
                    Me.mnuSystemUserInactivity.Visible = True
                    If IsEnable = True Then
                        Me.mnuSystemUserInactivity.Enabled = True
                    Else
                        Me.mnuSystemUserInactivity.Enabled = False
                    End If
                Else
                    Me.mnuSystemUserInactivity.Visible = False

                End If


            Case "ReportMissingEntityInfo"

                If IsVisible = True Then
                    Me.mnuReportMissingEntity.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportMissingEntity.Enabled = True
                    Else
                        Me.mnuReportMissingEntity.Enabled = False
                    End If
                Else
                    Me.mnuReportMissingEntity.Visible = False

                End If

            Case "ReportMissingDirectorInfo"

                If IsVisible = True Then
                    Me.mnuReportMissingDirectorInfo.Visible = True
                    If IsEnable = True Then
                        Me.mnuReportMissingDirectorInfo.Enabled = True
                    Else
                        Me.mnuReportMissingDirectorInfo.Enabled = False
                    End If
                Else
                    Me.mnuReportMissingDirectorInfo.Visible = False

                End If


            Case "TransactionFlexTransaction"

                If IsVisible = True Then
                    Me.mnuTransactionFlexTransaction.Visible = True
                    If IsEnable = True Then
                        Me.mnuTransactionFlexTransaction.Enabled = True
                    Else
                        Me.mnuTransactionFlexTransaction.Enabled = False
                    End If
                Else
                    Me.mnuTransactionFlexTransaction.Visible = False

                End If

            Case "ReportQualifiedAccWithOwnerEntity"

                If IsVisible = True Then
                    Me.mnuQualifiedAccWithSignEntity.Visible = True
                    If IsEnable = True Then
                        Me.mnuQualifiedAccWithSignEntity.Enabled = True
                    Else
                        Me.mnuQualifiedAccWithSignEntity.Enabled = False
                    End If
                Else
                    Me.mnuQualifiedAccWithSignEntity.Visible = False

                End If


                'Case "ReportCTRvsgoAML"

                '    If IsVisible = True Then
                '        Me.mnuReportCTRvsGoAML.Visible = True
                '        If IsEnable = True Then
                '            Me.mnuReportCTRvsGoAML.Enabled = True
                '        Else
                '            Me.mnuReportCTRvsGoAML.Enabled = False
                '        End If
                '    Else
                '        Me.mnuReportCTRvsGoAML.Visible = False

                '    End If

                'Case "ReportFlexvsCCMS"

                '    If IsVisible = True Then
                '        Me.mnuReportFlexvsCCMS.Visible = True
                '        If IsEnable = True Then
                '            Me.mnuReportFlexvsCCMS.Enabled = True
                '        Else
                '            Me.mnuReportFlexvsCCMS.Enabled = False
                '        End If
                '    Else
                '        Me.mnuReportFlexvsCCMS.Visible = False

                '    End If


        End Select



    End Sub


    Private Sub LogIn()

        'mnuFileLogout.Text = "&Log Out"

    End Sub

    Private Sub LogOut()

        'mnuFileLogout.Text = "&Log In"

StartLoop:

        For Each f As Form In My.Application.OpenForms

            If Not (f.Name = "FrmLogin" Or f.Name = "FrmMain") Then
                f.Close()
                GoTo StartLoop
            End If
        Next
        log_mesage = "User " + CommonAppSet.User + " Logged Out"
        Logger.system_log(log_mesage)

        Me.Dispose()

    End Sub

    Private Sub applicationIdle_Idle(ByVal sender As System.Object, ByVal e As System.EventArgs)
        _hostForm.IsMdiExit = False
        '_hostForm.Status = "Your You have been automatically logged out."
        _hostForm.Status = "Your session has expired. Please Login again."
        log_mesage = CommonAppSet.User + " Logout For InActivity"
        Logger.system_log(log_mesage)
        LogOut()

    End Sub
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByRef hostForm As Windows.Forms.Form)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _hostForm = hostForm

    End Sub

    Delegate Sub IdleDelegate(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private Sub applicationIdle_IdleAsync(ByVal sender As System.Object, ByVal e As System.EventArgs)

        BeginInvoke(New IdleDelegate(AddressOf applicationIdle_Idle), sender, e)

    End Sub

    Private Sub applicationIdle_WarnAsync(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Console.Beep()

    End Sub



#End Region



    Private Sub FrmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = CommonAppSet.ModuleShortName + " - " + CommonAppSet.ModuleName

        lblAppName.Text = CommonAppSet.ModuleName + " (" + CommonAppSet.ModuleShortName + ")"
        'lblVersion.Text = "Ver: " + Application.ProductVersion(0).ToString() + "." + Application.ProductVersion(2).ToString
        lblVersion.Text = "Ver: " + Application.ProductVersion()


        lblServer.Text = CommonAppSet.ServerConfigName
        lblUser.Text = CommonAppSet.Domain + "\" + CommonAppSet.User


        lblLoginDt.Text = DateTime.Now.ToString("dd-MMM-yyyy")
        lblLoginTime.Text = DateTime.Now.ToShortTimeString()

        LoadMenuSec()

        AddHandler applicationIdle.WarnAsync, AddressOf applicationIdle_WarnAsync
        AddHandler applicationIdle.IdleAsync, AddressOf applicationIdle_IdleAsync

        If applicationIdle.IsRunning = False Then

            applicationIdle.Start()
            LogIn()

        End If


    End Sub

    Private Sub mnuFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click
        _hostForm.IsMdiExit = True
        Me.Close()
    End Sub





    Private Sub mnuToolsImportFlexTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsImportFlexTrans.Click
        Dim frmImpFlex As New FrmImportFlexTrans
        frmImpFlex.ShowDialog()
    End Sub

    Private Sub mnuToolsImportFlexCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsImportFlexCust.Click
        Dim frmImpFlexCust As New FrmImportFlexCust
        frmImpFlexCust.ShowDialog()
    End Sub



    Private Sub mnuReportMismatchFlexAc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportMismatchFlexAc.Click
        Dim frmRptMismatch As New FrmReportMismatchFlexAc
        frmRptMismatch.ShowDialog()


    End Sub

    Private Sub mnuToolsProcessRule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsProcessRule.Click
        Dim frmProcesImp As New FrmProcessImport
        frmProcesImp.ShowDialog()
    End Sub

    Private Sub mnuToolsProcessFileReady_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsProcessFileReady.Click
        Dim frmProcesFileReady As New FrmProcessFileReady
        frmProcesFileReady.ShowDialog()

    End Sub

    Private Sub ProcessTransactionWithACInfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProcessTransactionWithACInfoToolStripMenuItem.Click
        Dim frmReport As New FrmReportMismatchFiuCustAc
        frmReport.ShowDialog()


    End Sub

    Private Sub mnuMainAccInfoDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainAccInfoDet.Click
        Dim frmAccountInfo As New FrmAccountInfo
        frmAccountInfo.ShowDialog()

    End Sub

    Private Sub mnuMainAccInfoSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainAccInfoSum.Click
        Dim frmAccountSum As New FrmAccountSumm
        frmAccountSum.ShowDialog()

    End Sub

    Private Sub mnuReportMismatchMissingOwner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportMismatchMissingOwner.Click
        Dim frmReportMissing As New FrmReportMissingOwner
        frmReportMissing.ShowDialog()


        'Dim frmReportViewer As New FrmReportViewer

        'Dim rd As New crMissingOwnerinfo

        'frmReportViewer.SetReport(rd)

        'frmReportViewer.ShowDialog()

    End Sub


    Private Sub mnuToolsExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsExport.Click
        Dim frmExportAll As New FrmExportAll
        frmExportAll.ShowDialog()

    End Sub

    Private Sub mnuMainOwnerInfoDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainOwnerInfoDet.Click
        Dim frmOwner As New frmOwnerInfo
        frmOwner.ShowDialog()

    End Sub

    Private Sub mnuMainOwnerInfoDetSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainOwnerInfoDetSum.Click
        Dim frmOwnerSumm As New frmOwnerInfoSumm
        frmOwnerSumm.ShowDialog()


    End Sub

    Private Sub mnuAcOwnerMappingDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAcOwnerMappingDetail.Click
        Dim frmAcOwnerMap As New FrmTransAcOwnerMapping
        frmAcOwnerMap.ShowDialog()

    End Sub

    Private Sub mnuAcOwnerMappingSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAcOwnerMappingSum.Click
        Dim frmAcOwnerMapSum As New FrmTransAcOwnerMappingSum
        frmAcOwnerMapSum.ShowDialog()

    End Sub




    Private Sub mnumainAccTypeSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnumainAccTypeSum.Click
        Dim frmAccTypeSum As New FrmAccountTypeSumm
        frmAccTypeSum.ShowDialog()

    End Sub

    Private Sub mnuMainAccTypeDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainAccTypeDetail.Click
        Dim frmAccTypeDet As New FrmAccountType
        frmAccTypeDet.ShowDialog()

    End Sub

    Private Sub mnuMainOwnerTypeDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainOwnerTypeDet.Click
        Dim FrmOwnerType As New FrmOwnerType
        FrmOwnerType.ShowDialog()

    End Sub

    Private Sub mnuMainOwnerTypeSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainOwnerTypeSum.Click
        Dim frmOwnerTypeSum As New FrmOwnerTypeSumm
        frmOwnerTypeSum.ShowDialog()

    End Sub


    Private Sub mnuMainReportingTypeSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainReportingTypeSum.Click
        Dim frmRptTypeSum As New FrmReportingTypeSumm
        frmRptTypeSum.ShowDialog()
    End Sub

    Private Sub mnuMainReportingTypeDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainReportingTypeDet.Click

        Dim frmRptType As New FrmReportingType
        frmRptType.ShowDialog()

    End Sub


    Private Sub mnuMainTransTypeSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainTransTypeSum.Click
        Dim frmTransTypeSum As New FrmTransTypeSumm
        frmTransTypeSum.ShowDialog()

    End Sub

    Private Sub mnuMainTransTypeDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainTransTypeDet.Click
        Dim frmTransType As New FrmTransType
        frmTransType.ShowDialog()

    End Sub


    Private Sub mnuMainAssDurationDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainAssDurationDet.Click
        Dim frmAssDurDet As New FrmAssessment_Duration
        frmAssDurDet.ShowDialog()
    End Sub

    Private Sub mnuMainAssDurationSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainAssDurationSum.Click
        Dim frmAssDurSum As New frmAssessmentDurSumm
        frmAssDurSum.ShowDialog()
    End Sub

    Private Sub mnuMainBankDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainBankDet.Click
        Dim frmBnkdet As New FrmBank
        frmBnkdet.ShowDialog()
    End Sub


    Private Sub mnuMainBankSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainBankSum.Click
        Dim frmBnkSum As New frmBankSumm
        frmBnkSum.ShowDialog()
    End Sub

    Private Sub mnuMainBranchDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainBranchDet.Click
        Dim frmBrnchDet As New FrmBankBranch
        frmBrnchDet.ShowDialog()
    End Sub

    Private Sub mnuMainBranchSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainBranchSum.Click
        Dim frmBrnchSum As New FrmBankBranchSumm
        frmBrnchSum.ShowDialog()
    End Sub

    Private Sub mnuMainRegAuthDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainRegAuthDet.Click
        Dim frmRgAuthDet As New FrmComRegAuth
        frmRgAuthDet.ShowDialog()
    End Sub

    Private Sub mnuMainRegAuthSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainRegAuthSum.Click
        Dim frmRgAuthSum As New frmComRegAuthSumm
        frmRgAuthSum.ShowDialog()
    End Sub


    Private Sub mnuMainCountryDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainCountryDet.Click
        Dim frmCounDet As New FrmCountry
        frmCounDet.ShowDialog()
    End Sub

    Private Sub mnuMainCountrySum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainCountrySum.Click
        Dim frmCounSum As New frmCountrySumm
        frmCounSum.ShowDialog()
    End Sub

    Private Sub mnuMainCurrencyDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainCurrencyDet.Click
        Dim frmCurDet As New FrmCurrency
        frmCurDet.ShowDialog()
    End Sub

    Private Sub mnuMainCurrencySum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainCurrencySum.Click
        Dim frmCurSum As New frmCurrencySumm
        frmCurSum.ShowDialog()
    End Sub

    Private Sub mnuMainDistrictDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainDistrictDet.Click
        Dim frmDistDet As New FrmDistrict
        frmDistDet.ShowDialog()
    End Sub

    Private Sub mnuMainDistrictSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainDistrictSum.Click
        Dim frmDistSum As New FrmDistSumm
        frmDistSum.ShowDialog()
    End Sub

    Private Sub mnuMainDivisionDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainDivisionDet.Click
        Dim frmDivDet As New FrmDivision
        frmDivDet.ShowDialog()
    End Sub

    Private Sub mnuMainDivisionSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainDivisionSum.Click
        Dim frmDivSum As New frmDivInfoSumm
        frmDivSum.ShowDialog()
    End Sub

    Private Sub mnuMainExecDesigDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainExecDesigDet.Click
        Dim frmExeDesigDet As New FrmExDesig
        frmExeDesigDet.ShowDialog()
    End Sub

    Private Sub mnuMainExecDesigSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainExecDesigSum.Click
        Dim frmExeDesigSum As New frmExDesigSumm
        frmExeDesigSum.ShowDialog()
    End Sub

    Private Sub mnuMainThanaDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainThanaDet.Click
        Dim frmThnDet As New FrmThana
        frmThnDet.ShowDialog()
    End Sub

    Private Sub mnuMainThanaSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainThanaSum.Click
        Dim frmThnSum As New FrmThanaSummary
        frmThnSum.ShowDialog()
    End Sub

    Private Sub mnuMainOccuTypeDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainOccuTypeDet.Click
        Dim frmOccTypeDet As New FrmOccTypes
        frmOccTypeDet.ShowDialog()
    End Sub

    Private Sub mnuMainOccuTypeDetSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainOccuTypeDetSum.Click
        Dim frmOccTypeSum As New FrmOccTypesSumm
        frmOccTypeSum.ShowDialog()
    End Sub


    Private Sub mnuReportStatusFileImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportStatusFileImport.Click
        Dim frmRptFileImport As New FrmReportFileImportStatus
        frmRptFileImport.ShowDialog()

    End Sub


    Private Sub mnuReportTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportTransaction.Click
        Dim frmRptTransByRule As New FrmReportTransByRule
        frmRptTransByRule.ShowDialog()

    End Sub





    'Private Sub mnuReportSystLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim frmlog As New frmLogReport
    '    frmlog.ShowDialog()
    'End Sub

    Private Sub mnuSysDeptDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysDeptDet.Click
        Dim frmsysDept As New FrmDeptDet
        frmsysDept.ShowDialog()
    End Sub

    Private Sub mnuSysDeptSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysDeptSum.Click
        Dim frmDeptSum As New FrmDeptSumm
        frmDeptSum.ShowDialog()
    End Sub

    Private Sub mnuSysUserDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysUserDet.Click
        Dim frmUser As New FrmUsersDet
        frmUser.ShowDialog()
    End Sub

    Private Sub mnuSysUserSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysUserSum.Click
        Dim frmUserSum As New FrmUserSumm
        frmUserSum.ShowDialog()
    End Sub

    Private Sub mnuSysFGDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysFGDet.Click
        Dim frmPermFgDet As New FrmPermFGDet
        frmPermFgDet.ShowDialog()
    End Sub

    Private Sub mnuSysFGSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysFGSum.Click
        Dim frmFgPermSum As New FrmFGPermSumm
        frmFgPermSum.ShowDialog()
    End Sub

    Private Sub mnuSysFGtoUserDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysFGtoUserDet.Click
        Dim frmUserRoleDet As New FrmUsersFGDet
        frmUserRoleDet.ShowDialog()
    End Sub

    Private Sub mnuSysFGtoUserSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysFGtoUserSum.Click
        Dim frmUsersFGSum As New FrmUsersFGSumm
        frmUsersFGSum.ShowDialog()
    End Sub

    Private Sub mnuReportSystLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportSystLog.Click

        Dim frmlog As New frmLogReport
        frmlog.ShowDialog()
    End Sub

    Private Sub mnuSysReportUserRole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysReportUserRole.Click
        Dim rd As New crUsersRole()

        Dim frmRptViewer As New frmReportView()

        frmRptViewer.SetReport(rd)

        frmRptViewer.Show()
    End Sub

    Private Sub mnuSysEERSFeedExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysEERSFeedExport.Click


        Dim frmEERS As New FrmEERSFeedExport()
        frmEERS.Show()

    End Sub

    Private Sub mnuFileLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileLogout.Click
        If applicationIdle.IsRunning = True Then
            applicationIdle.Stop()
            _hostForm.Status = "You have been logged out."
            _hostForm.IsMdiExit = False
            LogOut()
        End If
    End Sub


    Private Sub mnuHelpAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelpAbout.Click

        Dim frmAbout As New FrmAbout
        frmAbout.ShowDialog()

    End Sub

    Private Sub mnuSysRolePermission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysRolePermission.Click
        'Dim FrmRole As New FrmRolePermission
        'FrmRole.ShowDialog()

        'Dim rd As New crRolePermission()

        'Dim frmRptViewer As New frmReportView()

        'frmRptViewer.SetReport(rd)

        'frmRptViewer.Show()
    End Sub

    Private Sub mnuSysRoleMenuPermmission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysRoleMenuPermmission.Click
        Dim rd As New crMenuPermission()

        Dim frmRptViewer As New frmReportView()

        frmRptViewer.SetReport(rd)

        frmRptViewer.Show()
    End Sub

    Private Sub mnuSysRoleFromPermission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSysRoleFromPermission.Click
        Dim rd As New crFormPermission()

        Dim frmRptViewer As New frmReportView()

        frmRptViewer.SetReport(rd)

        frmRptViewer.Show()
    End Sub


    Private Sub mnugoAmlFundDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlFundDetail.Click
        Dim frmFundType As New frmFundTypeDetail
        frmFundType.ShowDialog()
    End Sub

    Private Sub mnugoAmlFundSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlFundSumm.Click
        Dim frmFundTypeSumm As New FrmFundTypeSummary
        frmFundTypeSumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlSubTypeDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlSubTypeDet.Click
        Dim frmSubTypeDet As New FrmSubmissionTypeDet()
        frmSubTypeDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlSubTypeSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlSubTypeSummary.Click
        Dim frmSubTypeSumm As New FrmSubmissionTypeSumm()
        frmSubTypeSumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlAcStatusDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlAcStatusDet.Click
        Dim frmAccountStatusDet As New FrmAccountStatusDet()
        frmAccountStatusDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlAcStatusSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlAcStatusSumm.Click
        Dim frmAccountStatusSumm As New FrmAccountStatusSumm()
        frmAccountStatusSumm.ShowDialog()
    End Sub


    Private Sub mnugoAmlIdentTypeDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlIdentTypeDet.Click
        Dim frmidentdet As New FrmIdentifierTypeDet()
        frmidentdet.ShowDialog()
    End Sub

    Private Sub mnugoAmlIdentTypeSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlIdentTypeSumm.Click
        Dim frmidentsumm As New FrmIdentifierTypeSumm()
        frmidentsumm.ShowDialog()
    End Sub

    'Private Sub mnugoAmlTransactionStatusDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlTransactionStatusDet.Click
    '    Dim frmTransactionStatusDet As New FrmTransactionItemStatusDet()
    '    frmTransactionStatusDet.ShowDialog()
    'End Sub

    'Private Sub mnugoAmlTrasactionStatusSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlTrasactionStatusSum.Click
    '    Dim frmTransactionStatusSum As New FrmTransactionItemStatusSumm()
    '    frmTransactionStatusSum.ShowDialog()
    'End Sub

    Private Sub mnugoAmlAccountdet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlAccountdet.Click
        Dim frmAccountTypeDet As New FrmAccountTypeDetail()
        frmAccountTypeDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlAccountSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlAccountSumm.Click
        Dim frmAccountTypeSummary As New FrmAccountTypeSummary()
        frmAccountTypeSummary.ShowDialog()
    End Sub

    Private Sub mnugoAmlConductionTypeDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlConductionTypeDet.Click
        Dim frmConductionTypeDet As New FrmConductionTypeDet()
        frmConductionTypeDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlConductionTypeSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlConductionTypeSumm.Click
        Dim frmConductionTypeSumm As New FrmConductionTypeSumm()
        frmConductionTypeSumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlContactTypeDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlContactTypeDet.Click
        Dim frmContactTypedet As New FrmContactTypeDet()
        frmContactTypedet.ShowDialog()
    End Sub

    Private Sub mnugoAmlContactTypeSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlContactTypeSumm.Click
        Dim frmContactTypeSumm As New FrmContactTypeSumm()
        frmContactTypeSumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlCommuDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlCommuDet.Click
        Dim frmCommunicationType As New FrmCommunicationTypeDet()
        frmCommunicationType.ShowDialog()
    End Sub


    Private Sub mnugoAmlCommuSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlCommuSumm.Click
        Dim frmcommunicationsumm As New FrmCommunicationTypeSumm()
        frmcommunicationsumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlEntityDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlEntityDet.Click
        Dim frmentitydet As New FrmEntityLegalDet()
        frmentitydet.ShowDialog()

    End Sub

    Private Sub mnugoAmlEntitySumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlEntitySumm.Click
        Dim frmentitysumm As New FrmEntityLegalSumm()
        frmentitysumm.ShowDialog()

    End Sub

    'Private Sub mnugoAmlTransactionItemDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlTransactionItemDet.Click
    '    Dim frmTransactionItemdet As New FrmTransactionItemDet()
    '    frmTransactionItemdet.ShowDialog()
    'End Sub

    'Private Sub mnugoAmlTransactionItenSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlTransactionItenSumm.Click
    '    Dim frmTransactionItemSumm As New FrmTransactionItemSumm()
    '    frmTransactionItemSumm.ShowDialog()
    'End Sub

    Private Sub mnugoAmlDivisionDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlDivisionDet.Click
        Dim frmDivisionDet As New FrmDivisionDet()
        frmDivisionDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlDivisionSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlDivisionSumm.Click
        Dim frmDivisionSumm As New FrmDivisionSumm()
        frmDivisionSumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlDistrictDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlDistrictDet.Click
        Dim frmDistrictDet As New FrmDistrictDet()
        frmDistrictDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlDistrictSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlDistrictSumm.Click
        Dim frmDistrictSumm As New FrmDistrictSumm()
        frmDistrictSumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlThanaDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlThanaDetail.Click
        Dim frmThanaDet As New FrmThanaDet()
        frmThanaDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlThanaSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlThanaSummary.Click
        Dim frmThanaSumm As New FrmThanaSummary2()
        frmThanaSumm.ShowDialog()

    End Sub

    Private Sub mnugoAmlAccountPersonRoleDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlAccountPersonRoleDet.Click
        Dim frmAccountPersonDet As New FrmAccountPersonRoleDet()
        frmAccountPersonDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlAccountPersonRoleSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlAccountPersonRoleSumm.Click
        Dim frmAccountPersonSumm As New FrmAccountPersonRoleSumm()
        frmAccountPersonSumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlEntityPersonRoleDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlEntityPersonRoleDet.Click
        Dim frmentitypersonroledet As New FrmEntityPersonRoleDet()
        frmentitypersonroledet.ShowDialog()
    End Sub

    Private Sub mnugoAmlEntityPersonRoleSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlEntityPersonRoleSumm.Click
        Dim frmentitypersonrolesumm As New FrmEntityPersonRoleSumm()
        frmentitypersonrolesumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlCurrenciesDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlCurrenciesDet.Click
        Dim frmCurrencyDet As New FrmCurrencyDetail()
        frmCurrencyDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlCurrenciesSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlCurrenciesSumm.Click
        Dim frmCurrencySumm As New FrmCurrencySummary()
        frmCurrencySumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlCountryDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlCountryDet.Click
        Dim frmCountryDet As New FrmCountryDetail()
        frmCountryDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlCountrySumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlCountrySumm.Click
        Dim frmCountrySumm As New FrmCountrySummary()
        frmCountrySumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlReportDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlReportDet.Click
        Dim frmReportDetail As New FrmReportDet()
        frmReportDetail.ShowDialog()
    End Sub

    Private Sub mnugoAmlReportSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlReportSumm.Click
        Dim frmReportSumm As New FrmReportSummary()
        frmReportSumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlAddressDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlAddressDet.Click
        Dim frmAddressDet As New FrmAddressDet()
        frmAddressDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlAddressSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlAddressSumm.Click
        Dim frmaddressSumm As New FrmAddressSumm()
        frmaddressSumm.ShowDialog()
    End Sub

    Private Sub mnugoAmlPersonDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlPersonDet.Click
        Dim frmPersonDet As New FrmPersonDetail()
        frmPersonDet.ShowDialog()

    End Sub

    Private Sub mnugoAmlPersonSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlPersonSumm.Click
        Dim frmPersonSumm As New FrmPersonSummary()
        frmPersonSumm.ShowDialog()
    End Sub


    Private Sub mnugoAmlOwnerDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlOwnerDet.Click
        Dim frmOwnerINfoDet As New FrmOwnergoAmlDet()
        frmOwnerINfoDet.ShowDialog()
    End Sub

    Private Sub mnugoAmlOwnerSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlOwnerSumm.Click
        Dim frmOwnerInfoSumm As New FrmOwnergoAmlSumm()
        frmOwnerInfoSumm.ShowDialog()
    End Sub

    Private Sub nmugoAmlAccountInfoDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nmugoAmlAccountInfoDet.Click
        Dim frmAccountInfodet As New FrmAccountInfoGoAMLDet()
        frmAccountInfodet.ShowDialog()
    End Sub

    Private Sub mnugoAmlAccountInfoSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlAccountInfoSumm.Click
        Dim frmAccountInfoSumm As New FrmAccountInfoGoAmlSumm()
        frmAccountInfoSumm.ShowDialog()
    End Sub

    Private Sub mnuTransgoAmlTransDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTransgoAmlTransDet.Click
        Dim frmTransactionDet As New FrmTransactiongoAmlDet()
        frmTransactionDet.ShowDialog()

    End Sub

    Private Sub mnuTransGoAmlTransSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTransGoAmlTransSumm.Click
        Dim frmTransactionSumm As New FrmTransactiongoAmlSumm()
        frmTransactionSumm.ShowDialog()
    End Sub

    'Private Sub mnuAcOwnerMapping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAcOwnerMapping.Click

    'End Sub

    Private Sub mnuToolsExportgoAml_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsExportgoAml.Click
        Dim frmGOamlExport As New FrmExportGoAML()
        frmGOamlExport.ShowDialog()
    End Sub

    Private Sub mnuOccupationDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOccupationDet.Click
        Dim frmOccupationDet As New FrmOccupationTypeDet()
        frmOccupationDet.ShowDialog()
    End Sub

    Private Sub mnuOccupationSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOccupationSummary.Click
        Dim FrmOccupationSumm As New FrmOccupationTypeSummary()
        FrmOccupationSumm.ShowDialog()
    End Sub

    Private Sub mnuToolsImportFlexCustGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsImportFlexCustGo.Click
        Dim frmImpGoCust As New FrmImportFlexCustGo()
        frmImpGoCust.ShowDialog()
    End Sub

    Private Sub mnuToolsProcessRuleGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsProcessRuleGo.Click
        Dim frmProcImp As New FrmProcessImportGo
        frmProcImp.ShowDialog()
    End Sub

    Private Sub mnuToolsProcessFileReadyGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileReadyGo.Click
        Dim frmProcFileGo As New FrmProcessFileReadyGo()
        frmProcFileGo.ShowDialog()
    End Sub

    Private Sub mnuReportMismatchFlexAcGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportMismatchFlexAcGo.Click
        Dim frmRptMismatchCustAc As New FrmReportMismatchFlexAcGo()
        frmRptMismatchCustAc.ShowDialog()
    End Sub

    Private Sub mnuReportMismatchMissingOwnerGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportMismatchMissingOwnerGo.Click
        Dim frmMissingOwner As New FrmReportMissingOwnerGo
        frmMissingOwner.ShowDialog()
    End Sub

    Private Sub mnuReportTransactionGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportTransactionGo.Click
        Dim frmRptTransGo As New FrmReportTransByRuleGo
        frmRptTransGo.ShowDialog()
    End Sub

  
    Private Sub mnuReportStatusCustomergoAML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportStatusCustomergoAML.Click
        Dim frmImportFileCustomer As New FrmFileImportCustomerStatus()
        frmImportFileCustomer.ShowDialog()
    End Sub

   
   
    Private Sub mnuReportStatusunAuthTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportStatusunAuthTrans.Click
        Dim frmUnauthTransaction As New FrmReportStatusUnAuthTrans()
        frmUnauthTransaction.ShowDialog()
    End Sub

    Private Sub mnuReportStatusUnAuthCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportStatusUnAuthCust.Click
        Dim frmUnauthCust As New FrmReportStatusUnAuthCust()
        frmUnauthCust.ShowDialog()
    End Sub

    Private Sub mnugoAMLDirectorDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAMLDirectorDetail.Click
        Dim frmDirectorDet As New FrmDirectorDetail()
        frmDirectorDet.ShowDialog()
    End Sub

    Private Sub mnugoAMLDirectorSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAMLDirectorSummary.Click
        Dim frmDirectorsumm As New FrmDirectorSummary()
        frmDirectorsumm.ShowDialog()
    End Sub

    Private Sub mnugoAMLEntityPersonDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAMLEntityPersonDetail.Click
        Dim frmEntityDet As New FrmEntityPersonDet
        frmEntityDet.ShowDialog()
    End Sub

    Private Sub mnugoAMLEntityPersonSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAMLEntityPersonSummary.Click
        Dim FrmEntitySumm As New FrmEntityPersonSummary()
        FrmEntitySumm.ShowDialog()
    End Sub

    Private Sub mnugoAMLBearerDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAMLBearerDetail.Click
        Dim frmbearerdet As New FrmBearerDetail()
        frmbearerdet.ShowDialog()
    End Sub

    Private Sub mnugoAMLBearerSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAMLBearerSummary.Click
        Dim frmbearersummar As New FrmBearerSummary()
        frmbearersummar.ShowDialog()
    End Sub

    Private Sub mnugoAMLImportEntityInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAMLImportEntityInfo.Click
        Dim frmImportEntity As New FrmImportEntityInformation()
        frmImportEntity.ShowDialog()
    End Sub

    Private Sub mnuImportOwnerInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportOwnerInfo.Click
        Dim FrmImportOwner As New FrmImportOwnerInfo
        FrmImportOwner.ShowDialog()
    End Sub

    Private Sub mnuImportgoAMLAccountInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportgoAMLAccountInfo.Click
        Dim FrmImportAccount As New FrmImportAccountInfo
        FrmImportAccount.ShowDialog()
    End Sub

    Private Sub mnugoAmlBearerInfoImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugoAmlBearerInfoImport.Click
        Dim FrmImportBearer As New FrmImportBearerInfo
        FrmImportBearer.ShowDialog()
    End Sub

    Private Sub mnuReportOwnerInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportOwnerInfo.Click
        Dim FrmReportOwner As New FrmReportOwnerInfo()
        FrmReportOwner.ShowDialog()
    End Sub

    Private Sub mnuTransCTRTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTransCTRTransaction.Click
        Dim FrmCTRTrsanReprt As New FrmCTRTransaction()
        FrmCTRTrsanReprt.ShowDialog()
    End Sub

    Private Sub mnuReportMissAccountField_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportMissAccountField.Click
        Dim FrmReportAccount As New FrmReportMissionAccountFieldInfo()
        FrmReportAccount.ShowDialog()
    End Sub

    'Private Sub mnuTransactionCTRsum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTransactionCTRsum.Click
    '    Dim rd As New crUsersRole()

    '    Dim frmRptViewer As New frmReportView()

    '    frmRptViewer.SetReport(rd)

    '    frmRptViewer.Show()
    'End Sub

    Private Sub mnuQualifiedTransgoAML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQualifiedTransgoAML.Click
        Dim frmQualify As New FrmQualifiedTransgoAML()
        frmQualify.ShowDialog()
    End Sub

    Private Sub mnuSystemUserInactivity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSystemUserInactivity.Click
        Dim rd As New CrUserInactivity()

        Dim frmRptViewer As New frmReportView()

        frmRptViewer.SetReport(rd)

        frmRptViewer.Show()

    End Sub

   
    Private Sub mnuReportMissingEntity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportMissingEntity.Click
        Dim frmEntityReport As New FrmReportMissingEntityInfo()
        frmEntityReport.ShowDialog()
    End Sub

    Private Sub mnuReportMissingDirectorInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportMissingDirectorInfo.Click
        Dim frmDirectorReport As New FrmReportMissingDirector()
        frmDirectorReport.ShowDialog()
    End Sub

    Private Sub mnuToolsImportCCMSTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsImportCCMSTrans.Click
        Dim frmImportCCMSTrans As New FrmImportCCMSTrans()
        frmImportCCMSTrans.ShowDialog()
    End Sub

    Private Sub mnuTransactionFlexTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTransactionFlexTransaction.Click
        Dim FrmFlex As New FrmFlexTransaction()
        FrmFlex.ShowDialog()
    End Sub

    Private Sub mnuQualifiedAccWithSignEntity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQualifiedAccWithSignEntity.Click
        Dim Qualified As New FrmQualifiedAccWithEntityOwner()
        Qualified.ShowDialog()
    End Sub

    Private Sub mnuReportCTRvsGoAML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportCTRvsGoAML.Click
        Dim RptCTR As New FrmRptCTRvsGoAML()
        RptCTR.ShowDialog()
    End Sub

    Private Sub mnuReportFlexvsCCMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportFlexvsCCMS.Click
        Dim RptCCMS As New FrmRptFLEXvsCCMS()
        RptCCMS.ShowDialog()
    End Sub


End Class