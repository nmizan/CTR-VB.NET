﻿Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Public Class FrmOwnerTypeSumm

#Region "user defined codes"

    Dim _formName As String = "MaintenanceOwnerTypeSummary"
    Dim opt As SecForm = New SecForm(_formName)


    Private Sub LoadDataGrid()

        Try


            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String

            If chkShowAll.Checked = True Then
                strSql = "select OWTYPECODE, OWDEFINITION,IS_AUTHORIZED,MODNO, " + _
                    " 'S' = " + _
                    "	CASE  " + _
                    "	    WHEN IS_AUTHORIZED='1' and STATUS = 'D' THEN 'D' " + _
                    "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                    "       ELSE 'U' " + _
                    "	End " + _
                    " from FIU_OWNERSHIP_TYPES " + _
                    " where IS_AUTHORIZED=0 OR STATUS in ('L','D')  " + _
                    " order by IS_AUTHORIZED,OWTYPECODE"

            Else
                strSql = "select OWTYPECODE, OWDEFINITION,IS_AUTHORIZED,ModNo, " + _
                " 'S' = " + _
                "	CASE  " + _
                "	    WHEN IS_AUTHORIZED='1' THEN 'A' " + _
                "       ELSE 'U' " + _
                "	End " + _
                " from FIU_OWNERSHIP_TYPES " + _
                " where IS_AUTHORIZED=0 OR STATUS='L' " + _
                " order by IS_AUTHORIZED,OWTYPECODE"




            End If


            Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            dgView.AutoGenerateColumns = False
            dgView.DataSource = ds
            dgView.DataMember = ds.Tables(0).TableName

        Catch ex As Exception

            MessageBox.Show(ex.Message, "!! Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

#End Region


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Dim frmOwnerType As New FrmOwnerType
        frmOwnerType.ShowDialog()


    End Sub

    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try

            If Not (dgView.SelectedRows.Item(0).Cells(0).Value Is Nothing) Then
                'dgView.SelectedRows.Item(0).Cells(0).Value 
                Dim frmOwnerType As New FrmOwnerType(dgView.SelectedRows.Item(0).Cells(0).Value, dgView.SelectedRows.Item(0).Cells(2).Value)
                frmOwnerType.ShowDialog()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgView.CellDoubleClick
        If Not (dgView.SelectedRows.Item(0).Cells(0).Value Is Nothing Or dgView.SelectedRows.Item(0).Cells(0).Value Is DBNull.Value) Then
            Dim frmOwnerType As New FrmOwnerType(dgView.SelectedRows.Item(0).Cells(0).Value, dgView.SelectedRows.Item(0).Cells(2).Value)
            frmOwnerType.ShowDialog()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadDataGrid()

    End Sub




    Private Sub FrmOwnerTypeSumm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objExp As New ExportUtil(dgView)

        objExp.ExportXl()
    End Sub
End Class