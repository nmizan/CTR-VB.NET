
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

Public Class SecForm
    Private _IsAuth As Boolean = False
    Private _IsShow As Boolean = False
    Private _IsNew As Boolean = False
    Private _IsSave As Boolean = False
    Private _IsDelete As Boolean = False
    Private _IsUnlock As Boolean = False

    Public Sub New(ByVal FormName As String)
        Dim user = CommonAppSet.User
        Dim db As New SqlDatabase(CommonAppSet.ConnStr)
        Dim ds As New DataSet
        Dim comProc As DbCommand = db.GetStoredProcCommand("CTR_Users_FunFormPermission")
        db.AddInParameter(comProc, "@USERS_ID", DbType.String, user)
        db.AddInParameter(comProc, "@FORMS_NAME", DbType.String, FormName)

        ds = db.ExecuteDataSet(comProc)
        If ds.Tables(0).Rows.Count > 0 Then
            'FormName = ds.Tables(0).Rows(0)("FORMS_NAME").ToString()
            _IsShow = ds.Tables(0).Rows(0)("IS_SHOW").ToString()
            _IsNew = ds.Tables(0).Rows(0)("IS_NEW").ToString()
            _IsUnlock = ds.Tables(0).Rows(0)("IS_UNLOCK").ToString()
            _IsAuth = ds.Tables(0).Rows(0)("IS_AUTHORIZER").ToString()
            _IsDelete = ds.Tables(0).Rows(0)("IS_DELETE").ToString()
            _IsSave = ds.Tables(0).Rows(0)("IS_SAVE").ToString()
        End If


        If CommonAppSet.IsByPass = True Then
            _IsShow = True
            _IsNew = True
            _IsUnlock = True
            _IsAuth = True
            _IsDelete = True
            _IsSave = True
        End If

        '_IsShow = True
        '_IsNew = True
        '_IsUnlock = True
        '_IsAuth = True
        '_IsDelete = True
        '_IsSave = True


    End Sub
    Public Sub New(ByVal FormName As String, ByVal IsAdmin As Boolean)

        If IsAdmin = True Then

            _IsShow = True
            _IsNew = True
            _IsUnlock = True
            _IsAuth = True
            _IsDelete = True
            _IsSave = True

        Else

            Dim user = CommonAppSet.User
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim ds As New DataSet
            Dim comProc As DbCommand = db.GetStoredProcCommand("CTR_Users_FunFormPermission")
            db.AddInParameter(comProc, "@USERS_ID", DbType.String, user)
            db.AddInParameter(comProc, "@FORMS_NAME", DbType.String, FormName)

            ds = db.ExecuteDataSet(comProc)
            If ds.Tables(0).Rows.Count > 0 Then
                'FormName = ds.Tables(0).Rows(0)("FORMS_NAME").ToString()
                _IsShow = ds.Tables(0).Rows(0)("IS_SHOW").ToString()
                _IsNew = ds.Tables(0).Rows(0)("IS_NEW").ToString()
                _IsUnlock = ds.Tables(0).Rows(0)("IS_UNLOCK").ToString()
                _IsAuth = ds.Tables(0).Rows(0)("IS_AUTHORIZER").ToString()
                _IsDelete = ds.Tables(0).Rows(0)("IS_DELETE").ToString()
                _IsSave = ds.Tables(0).Rows(0)("IS_SAVE").ToString()
            End If


        End If


        If CommonAppSet.IsByPass = True Then
            _IsShow = True
            _IsNew = True
            _IsUnlock = True
            _IsAuth = True
            _IsDelete = True
            _IsSave = True
        End If

       


    End Sub

    Public ReadOnly Property IsAuth() As Boolean
        Get
            Return _IsAuth
        End Get

    End Property

    Public ReadOnly Property IsShow() As Boolean
        Get
            Return _IsShow
        End Get

    End Property

    Public ReadOnly Property IsNew() As Boolean
        Get
            Return _IsNew
        End Get

    End Property

    Public ReadOnly Property IsUnlock() As Boolean
        Get
            Return _IsUnlock
        End Get

    End Property

    Public ReadOnly Property IsSave() As Boolean
        Get
            Return _IsSave
        End Get

    End Property

    Public ReadOnly Property IsDelete() As Boolean
        Get
            Return _IsDelete
        End Get

    End Property


End Class

