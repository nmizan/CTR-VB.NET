Imports System.Globalization
Public Class NullHelper

    Public Shared Function ToBool(ByVal objVal As Object) As Boolean

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return False
        End If


        Return Convert.ToBoolean(objVal)

    End Function

    Public Shared Function ToIntNum(ByVal objVal As Object) As Integer

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return 0
        End If

        If (objVal.ToString().Trim() = "") Then
            Return 0
        End If


        Return Convert.ToInt64(objVal)

    End Function


    Public Shared Function ToDecNum(ByVal objVal As Object) As Decimal

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return 0
        End If

        If (objVal.ToString().Trim() = "") Then
            Return 0
        End If

        Return Math.Round(Convert.ToDecimal(objVal), 2)

    End Function


    Public Shared Function DateToString(ByVal objVal As Object) As String

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return ""
        End If


        Return Convert.ToDateTime(objVal).ToString("MM/dd/yyyy")

    End Function

    Public Shared Function DateToStringNew(ByVal objVal As Object) As String

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return ""
        End If


        Return Convert.ToDateTime(objVal).ToString("dd/MM/yyyy")

    End Function


    Public Shared Function StringToString(ByVal objVal As Object) As Object

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return DBNull.Value
        End If

        If objVal.ToString().Trim() = "" Then
            Return DBNull.Value
        End If

        Return objVal

    End Function

    Public Shared Function StringToCSV(ByVal objVal As Object) As Object

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return ""
        End If

        If objVal.ToString().Trim() = "" Then
            Return ""
        End If

        Return """" & objVal.ToString().Replace(vbCrLf, "") & """"
        'If objVal.ToString().Trim() = "" Then
        '    Return DBNull.Value
        'End If

        'Return objVal

    End Function

    Public Shared Function DateToXML(ByVal objVal As Object) As String

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return ""
        End If

        Return Convert.ToDateTime(objVal).ToString("yyyy-MM-ddT00:00:00")

    End Function

    Public Shared Function DateToCSV(ByVal objVal As Object) As String

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return ""
        End If

        Return Convert.ToDateTime(objVal).ToString("dd-MMM-yyyy hh:mm:ss tt")

        '15-NOV-2006 12:00:00 AM

    End Function

    Public Shared Function NumToString(ByVal objVal As Object) As String

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return "0"
        End If


        Return Convert.ToDecimal(objVal).ToString("F")

    End Function

    Public Shared Function NumToXML(ByVal objVal As Object) As Object

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return "0"
        End If

        If objVal.ToString().Trim() = "" Then
            Return "0"
        End If


        Return objVal

    End Function

    Public Shared Function ObjectToNull(ByVal objVal As Object) As Object

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Or (objVal.ToString().Trim() = "") Then
            Return DBNull.Value
        End If

        Return objVal


    End Function

    Public Shared Function ObjectToString(ByVal objVal As Object) As String

        If (objVal Is Nothing) Or (objVal Is DBNull.Value) Then
            Return ""
        End If

        If (objVal.ToString().Trim() = "") Then
            Return ""
        End If

        Return objVal.ToString().Trim()


    End Function


    Public Shared Function StringToDate(ByVal str As String) As Object

        If str.Trim() = "" Or str.Trim() = "/  /" Then
            Return DBNull.Value
        Else
            Return DateTime.ParseExact(str.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
        End If

    End Function


    Public Shared Function StringToDatereport(ByVal str As String) As Object

        If str.Trim() = "" Or str.Trim() = "/  /" Then
            Return DBNull.Value
        Else
            Return DateTime.ParseExact(str.Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture)
        End If

    End Function


    Public Shared Function StringToDate2(ByVal str As String) As Object

        If str.Trim() = "" Or str.Trim() = "/  /" Then
            Return DBNull.Value
        Else
            Return DateTime.ParseExact(str.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
        End If

    End Function

    Public Shared Function StringToCrDateString(ByVal str As String) As String
        Try




            If str.Trim() = "" Or str Is Nothing Then
                Return ""

            Else
                Dim dt As New DateTime

                dt = DateTime.ParseExact(str.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)

                Return dt.ToString("MM/dd/yyyy")


            End If

        Catch ex As Exception
            Return str
        End Try

    End Function

    Public Shared Function StringToObject(ByVal str As String) As Object
        Try




            If str.Trim() = "" Or str Is Nothing Then
                Return DBNull.Value

            Else
                Dim dt As New DateTime

                dt = DateTime.ParseExact(str.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)

                Return dt.ToString("dd-MMM-yyyy")


            End If

        Catch ex As Exception
            Return str
        End Try

    End Function

    Public Shared Function ObjectToXL(ByVal str As Object) As Object
        Try


            'If str.Trim() = "" Or str Is Nothing Then
            'Return DBNull.Value

            If str Is Nothing Then
                Return DBNull.Value

            ElseIf str.ToString().Trim() = "" Then
                Return DBNull.Value

            ElseIf IsNumeric(str) Then
                str = "'" + str.ToString()


            Else
                Dim dt As New DateTime

                dt = DateTime.ParseExact(str.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)

                Return dt.ToString("dd-MMM-yyyy")


            End If



        Catch ex As Exception

        End Try

        Return str



    End Function

End Class
