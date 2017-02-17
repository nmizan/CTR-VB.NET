Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Public Class Logger

    Public Shared Sub system_log(ByVal log_message As String)

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Using conn As DbConnection = db.CreateConnection()

            Dim commProc As DbCommand = db.GetStoredProcCommand("CTR_SystemLog_Add")

            log_message = "[" + Environment.MachineName + "] " + log_message


            commProc.Parameters.Clear()
            db.AddInParameter(commProc, "@LOG_DESCRIPTION", DbType.String, log_message)
            conn.Open()
            db.ExecuteNonQuery(commProc)
        End Using
    End Sub
End Class
