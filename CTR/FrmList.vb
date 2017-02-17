'
' List of Values
' Author: Iftekharul Alam Khan Lodi
' Since: 14-Nov-12
'

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Public Class FrmList

    Dim db As SqlDatabase = New SqlDatabase(CommonAppSet.ConnStr)

    Dim commProc As DbCommand

    Dim dt As DataTable = New DataTable()
    Public ProcName As String = ""
    Private _filter As String = ""
    Public filter(,) As String
    Private _row As DataGridViewRow = New DataGridViewRow()
    Public colHide() As Integer
    Public colwidth(,) As Integer
    Public colrename(,) As String

    Public Sub AddParamToDB(ByVal name As String, ByVal dbType As System.Data.DbType, ByVal value As Object)
        If commProc Is Nothing Then
            commProc = db.GetStoredProcCommand(ProcName)
            commProc.Parameters.Clear()
        End If

        db.AddInParameter(commProc, name, dbType, value)
    End Sub


    Public ReadOnly Property RowResult() As DataGridViewRow
        Get
            Return _row
        End Get

    End Property

    Public Sub ClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)

        _filter = filter(CType(sender, RadioButton).Tag, 0)
        dgView.Focus()

    End Sub

    Private Sub FrmList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim rdoFilter As System.Windows.Forms.RadioButton

        If Not filter Is Nothing Then

            Dim i As Integer
            For i = 0 To filter.GetUpperBound(0)

                rdoFilter = New System.Windows.Forms.RadioButton()
                rdoFilter.Text = filter(i, 1)
                rdoFilter.Tag = i

                AddHandler rdoFilter.Click, AddressOf ClickHandler

                flpFilter.Controls.Add(rdoFilter)

                If i = 0 Then
                    rdoFilter.Checked = True
                End If


            Next

        End If

        If ProcName.Trim() = "" Or filter(0, 0) = "" Then
            MessageBox.Show("Search list is not properly configured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        _filter = filter(0, 0)

        'Dim db As SqlDatabase = New SqlDatabase(CommonAppSet.ConnStr)

        Try

            If commProc Is Nothing Then
                dt = db.ExecuteDataSet(CommandType.StoredProcedure, ProcName).Tables(0)
            Else
                dt = db.ExecuteDataSet(commProc).Tables(0)

            End If

            dgView.DataSource = dt

            If Not colHide Is Nothing Then
                For Each i As Integer In colHide
                    dgView.Columns(i).Visible = False
                Next

            End If

            If Not colwidth Is Nothing Then
                Dim i As Integer
                For i = 0 To colwidth.GetUpperBound(0)
                    dgView.Columns(colwidth(i, 0)).Width = colwidth(i, 1)
                Next

            End If

            If Not colrename Is Nothing Then
                Dim i As Integer
                For i = 0 To colrename.GetUpperBound(0)
                    dgView.Columns(Integer.Parse(colrename(i, 0))).HeaderText = colrename(i, 1)

                Next

            End If

            lblTotRec.Text = dt.Rows.Count.ToString()


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub dgView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgView.KeyDown
        If (e.KeyCode = Keys.Enter) Then

            e.Handled = True
            If Not dgView.CurrentRow Is Nothing Then
                _row = dgView.Rows(dgView.CurrentRow.Index)
            End If

            Me.Close()

        End If

        If (e.KeyCode = Keys.Escape) Then

            Me.Close()

        End If

    End Sub

    Private Sub dgView_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgView.KeyPress

        If (e.KeyChar = Convert.ToChar(8) And txtFind.Text.Trim().Length <> 0) Then

            txtFind.Text = txtFind.Text.Substring(0, txtFind.Text.Length - 1)

        ElseIf (e.KeyChar <> Convert.ToChar(9) And e.KeyChar <> Convert.ToChar(13) And e.KeyChar <> Convert.ToChar(8)) Then

            txtFind.AppendText(e.KeyChar.ToString())
        End If
    End Sub

    Private Sub txtFind_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFind.TextChanged
        Dim strFilter As String = ""

        If (txtFind.Text.Trim() = "") Then


            dt.DefaultView.RowFilter = ""

        Else

            strFilter = _filter + " like '%" + txtFind.Text.Trim() + "%'"
            dt.DefaultView.RowFilter = strFilter
        End If
    End Sub

    Private Sub dgView_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgView.DoubleClick
        _row = dgView.Rows(dgView.CurrentRow.Index)
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'commProc = db.GetStoredProcCommand(ProcName)

        'commProc.Parameters.Clear()

    End Sub
End Class