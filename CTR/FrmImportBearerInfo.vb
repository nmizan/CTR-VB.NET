'
'Author             : Fahad Khan
'Purpose            : Import Bearer/Depositor Info Information
'Creation date      : 2-Dec-2013
'Stored Procedure(s):

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql

'Imports Microsoft.Office.Interop

Imports System.IO.File
Imports System.IO.FileSystemInfo
Imports System.IO

Imports Microsoft.VisualBasic
Imports System.Xml

Imports Microsoft.Office.Interop

Public Class FrmImportBearerInfo

    Dim _formName As String = "MaintenanceGoBearerInfoImport"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""

    Dim flagRequireField As Boolean = False
    Dim flagWarning As Boolean = False
    Dim _debugRow As Integer = 0
    Dim _ToTRecordNo As Integer = 0
    Public Delegate Sub ChangeTextOfLabelDelegate( _
                            ByVal ToLabel As Label, _
                            ByVal AddText As String)

    Private Sub SetLabelText(ByVal lblName As System.Windows.Forms.Label, ByVal strMsg As String)

        'txtFileImpStatus.AppendText(strMsg)


        If lblName.InvokeRequired Then
            'lblName.Invoke(New Action(Of System.String)(AddressOf SetLabelText), strMsg)
            lblName.Invoke(New ChangeTextOfLabelDelegate(AddressOf SetLabelText), New Object() {lblName, strMsg})
        Else
            lblName.Text = strMsg
        End If

    End Sub

    Private Sub ReadExcel()
        Dim xlApp As New Excel.Application

        Try

            Dim wb As Excel.Workbook = xlApp.Workbooks.Open(opdRetFile.FileName)

            Dim sheet As Excel.Worksheet = wb.Sheets(1)

            Dim xlRange As Excel.Range = sheet.UsedRange

            Dim rowCount As Integer = xlRange.Rows.Count
            Dim colCount As Integer = xlRange.Columns.Count

            'Dim colCharset As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

            'xlRange.Cells(0, 0)
            dgView.Rows.Clear()
            dgView.AllowUserToAddRows = True

            Dim i As Integer
            _ToTRecordNo = 0
            For i = 2 To rowCount

                If sheet.Range("B" & i.ToString()).Value2 Is Nothing Then
                    Exit For
                End If

                If sheet.Range("B" & i.ToString()).Value2.ToString() = "" Then
                    Exit For
                End If


                If (i = dgView.Rows.Count + 1) Then
                    dgView.Rows.Add()
                End If

                dgView.Item(0, i - 2).Value = sheet.Range("A" & i.ToString()).Value2  ' Bearer/Depositor id 
                dgView.Item(1, i - 2).Value = sheet.Range("B" & i.ToString()).Value2  ' Reference Number
                dgView.Item(2, i - 2).Value = sheet.Range("C" & i.ToString()).Value2  ' Gender
                dgView.Item(3, i - 2).Value = sheet.Range("D" & i.ToString()).Value2  ' Title
                dgView.Item(4, i - 2).Value = sheet.Range("E" & i.ToString()).Value2  ' First Name
                dgView.Item(5, i - 2).Value = sheet.Range("F" & i.ToString()).Value2  ' Middle name
                dgView.Item(6, i - 2).Value = sheet.Range("G" & i.ToString()).Value2  ' Last Name
                dgView.Item(7, i - 2).Value = sheet.Range("H" & i.ToString()).Value2  ' Spouse
                dgView.Item(8, i - 2).Value = sheet.Range("I" & i.ToString()).Value   ' Birth date
                dgView.Item(9, i - 2).Value = sheet.Range("J" & i.ToString()).Value2  ' Birth Place
                dgView.Item(10, i - 2).Value = sheet.Range("K" & i.ToString()).Value2  ' Mothers Name
                dgView.Item(11, i - 2).Value = sheet.Range("L" & i.ToString()).Value2 ' Fathers Name
                dgView.Item(12, i - 2).Value = sheet.Range("M" & i.ToString()).Value2 ' National Id
                dgView.Item(13, i - 2).Value = sheet.Range("N" & i.ToString()).Value2 ' Passport Number
                dgView.Item(14, i - 2).Value = sheet.Range("O" & i.ToString()).Value2 ' Passport Country
                dgView.Item(15, i - 2).Value = sheet.Range("P" & i.ToString()).Value2 ' Birth regi No
                dgView.Item(16, i - 2).Value = sheet.Range("Q" & i.ToString()).Value2 ' Nationality1
                dgView.Item(17, i - 2).Value = sheet.Range("R" & i.ToString()).Value2 ' Nationality2
                dgView.Item(18, i - 2).Value = sheet.Range("S" & i.ToString()).Value2 ' Nationality3
                dgView.Item(19, i - 2).Value = sheet.Range("T" & i.ToString()).Value2 ' Residence
                dgView.Item(20, i - 2).Value = sheet.Range("U" & i.ToString()).Value2 ' Phone Contact Type
                dgView.Item(21, i - 2).Value = sheet.Range("V" & i.ToString()).Value2 ' Phone Communication Type
                dgView.Item(22, i - 2).Value = sheet.Range("W" & i.ToString()).Value2 ' Country Prefix
                dgView.Item(23, i - 2).Value = sheet.Range("X" & i.ToString()).Value2 ' Phone Number
                dgView.Item(24, i - 2).Value = sheet.Range("Y" & i.ToString()).Value2 ' Phone Extension
                dgView.Item(25, i - 2).Value = sheet.Range("Z" & i.ToString()).Value2 ' Address type
                dgView.Item(26, i - 2).Value = sheet.Range("AA" & i.ToString()).Value2 ' Address
                dgView.Item(27, i - 2).Value = sheet.Range("AB" & i.ToString()).Value2 ' Town/Thana
                dgView.Item(28, i - 2).Value = sheet.Range("AC" & i.ToString()).Value2 ' City/District
                dgView.Item(29, i - 2).Value = sheet.Range("AD" & i.ToString()).Value2 ' Zip 
                dgView.Item(30, i - 2).Value = sheet.Range("AE" & i.ToString()).Value2 ' Country Code
                dgView.Item(31, i - 2).Value = sheet.Range("AF" & i.ToString()).Value2 ' State/Division
                dgView.Item(32, i - 2).Value = sheet.Range("AG" & i.ToString()).Value2 ' Identification Type
                dgView.Item(33, i - 2).Value = sheet.Range("AH" & i.ToString()).Value2 ' Identification Number
                dgView.Item(34, i - 2).Value = sheet.Range("AI" & i.ToString()).Value  ' Issued Date
                dgView.Item(35, i - 2).Value = sheet.Range("AJ" & i.ToString()).Value  ' Expiry Date
                dgView.Item(36, i - 2).Value = sheet.Range("AK" & i.ToString()).Value2 ' Issued Country
                dgView.Item(37, i - 2).Value = sheet.Range("AL" & i.ToString()).Value2 ' Issued By
                dgView.Item(38, i - 2).Value = sheet.Range("AM" & i.ToString()).Value2 ' Email
                dgView.Item(39, i - 2).Value = sheet.Range("AN" & i.ToString()).Value2 ' Occupation
                dgView.Item(40, i - 2).Value = sheet.Range("AO" & i.ToString()).Value2 ' Employer Name
                dgView.Item(41, i - 2).Value = sheet.Range("AP" & i.ToString()).Value2 ' Contact Type
                dgView.Item(42, i - 2).Value = sheet.Range("AQ" & i.ToString()).Value2 ' Communication type
                dgView.Item(43, i - 2).Value = sheet.Range("AR" & i.ToString()).Value2 ' Country Prefix
                dgView.Item(44, i - 2).Value = sheet.Range("AS" & i.ToString()).Value2 ' Phone Number
                dgView.Item(45, i - 2).Value = sheet.Range("AT" & i.ToString()).Value2 ' Phone Extension
                dgView.Item(46, i - 2).Value = sheet.Range("AU" & i.ToString()).Value2 ' Address type
                dgView.Item(47, i - 2).Value = sheet.Range("AV" & i.ToString()).Value2 ' Address
                dgView.Item(48, i - 2).Value = sheet.Range("AW" & i.ToString()).Value2 ' Town
                dgView.Item(49, i - 2).Value = sheet.Range("AX" & i.ToString()).Value2 ' City
                dgView.Item(50, i - 2).Value = sheet.Range("AY" & i.ToString()).Value2 ' Country Type
                dgView.Item(51, i - 2).Value = sheet.Range("AZ" & i.ToString()).Value2 ' State/ division
                dgView.Item(52, i - 2).Value = sheet.Range("BA" & i.ToString()).Value2 ' Deseased
                dgView.Item(53, i - 2).Value = sheet.Range("BB" & i.ToString()).Value  ' Deseased date
                dgView.Item(54, i - 2).Value = sheet.Range("BC" & i.ToString()).Value2 ' Tax Number
                dgView.Item(55, i - 2).Value = sheet.Range("BD" & i.ToString()).Value2 ' Tax Regi Number
                dgView.Item(56, i - 2).Value = sheet.Range("BE" & i.ToString()).Value2 ' Source of wealth
                dgView.Item(57, i - 2).Value = sheet.Range("BF" & i.ToString()).Value2 ' Comments

                _ToTRecordNo = _ToTRecordNo + 1
                SetLabelText(lblTotRecNo, _ToTRecordNo.ToString())
            Next
            dgView.AllowUserToAddRows = False
           

            wb.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        Try
            xlApp.Quit()
        Catch ex As Exception

        End Try




    End Sub


    Private Sub CheckValidation()


        Dim tmpAccount As String = ""
        Dim strErrMsg As String = ""

        Dim RecGroup As String = ""

        Try



            For Each row As DataGridViewRow In dgView.Rows

                strErrMsg = ""
                flagRequireField = False

                If NullHelper.ObjectToString(row.Cells(1).Value).Trim() = "" Then ' First name

                    flagRequireField = True

                End If

                If NullHelper.ObjectToString(row.Cells(4).Value).Trim() = "" Then ' First name

                    flagRequireField = True

                End If

                If NullHelper.ObjectToString(row.Cells(6).Value).Trim() = "" Then ' Last NAme

                    flagRequireField = True

                End If

                If NullHelper.ObjectToString(row.Cells(12).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(13).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(15).Value).Trim() = "" Then
                    flagRequireField = True


                End If

                'If NullHelper.ObjectToString(row.Cells(12).Value).Trim() <> "" And Len(NullHelper.ObjectToString(row.Cells(12).Value).Trim()) <> 17 And Len(NullHelper.ObjectToString(row.Cells(12).Value).Trim()) <> 13 Then

                '    strErrMsg = strErrMsg + " | Invalid National ID Number " + """" + row.Cells(12).Value.ToString() + """" + " It might be 13 or 17 digits "
                '    flagRequireField = True


                'End If


                If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(14).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Passport Country Type Code " + """" + row.Cells(14).Value.ToString() + """"
                    flagRequireField = True
                End If

                If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(16).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Nationality1 Country Type Code " + """" + row.Cells(16).Value.ToString() + """"
                    flagRequireField = True
                End If

                If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(17).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Nationality2 Country Type Code " + """" + row.Cells(17).Value.ToString() + """"
                    flagRequireField = True
                End If

                If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(18).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Nationality3 Country Type Code " + """" + row.Cells(18).Value.ToString() + """"
                    flagRequireField = True
                End If

                If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(19).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Residence Country Type Code " + """" + row.Cells(19).Value.ToString() + """"
                    flagRequireField = True
                End If

                'phone

                If NullHelper.ObjectToString(row.Cells(20).Value) <> "" And NullHelper.ObjectToString(row.Cells(21).Value) <> "" And NullHelper.ObjectToString(row.Cells(23).Value) <> "" Then
                    'flagWarning = False

                ElseIf NullHelper.ObjectToString(row.Cells(20).Value) = "" And NullHelper.ObjectToString(row.Cells(21).Value) = "" And NullHelper.ObjectToString(row.Cells(23).Value) = "" Then
                    'flagWarning = False
                Else
                    flagWarning = True
                    strErrMsg = strErrMsg + " | Warning : Required Phone Field Missing, Phone Data might not be saved properly "
                End If


                If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(20).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Phone Contact Type Code " + """" + row.Cells(20).Value.ToString() + """"
                    flagWarning = True
                End If

                If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(21).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Phone Communication Type Code " + """" + row.Cells(21).Value.ToString() + """"
                    flagWarning = True
                End If

                ''address

                If NullHelper.ObjectToString(row.Cells(25).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(26).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(28).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(30).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(31).Value).Trim() <> "" Then ' phone contact type

                    ' flagWarning = False
                ElseIf NullHelper.ObjectToString(row.Cells(25).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(26).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(28).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(30).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(31).Value).Trim() = "" Then ' phone contact type

                    'flagWarning = False

                Else
                    flagWarning = True
                    strErrMsg = strErrMsg + " | Warning : Required Address Field Missing, Data might not be saved properly "
                End If

                If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(25).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Address Type Code " + """" + row.Cells(25).Value.ToString() + """"
                    flagWarning = True
                End If

                If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(30).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Country Code " + """" + row.Cells(30).Value.ToString() + """"
                    flagWarning = True
                End If

                ''identification

                If NullHelper.ObjectToString(row.Cells(32).Value) <> "" And NullHelper.ObjectToString(row.Cells(33).Value) <> "" And NullHelper.ObjectToString(row.Cells(36).Value) <> "" Then ' phone contact type

                    'flagWarning = False
                ElseIf NullHelper.ObjectToString(row.Cells(32).Value) = "" And NullHelper.ObjectToString(row.Cells(33).Value) = "" And NullHelper.ObjectToString(row.Cells(36).Value) = "" Then ' phone contact type

                    'flagWarning = False

                Else
                    flagWarning = True
                    strErrMsg = strErrMsg + " | Warning : Required Identification Field Missing, Data might not be saved properly "
                End If

                If TypeChecker.IsContained("identifier_type", NullHelper.ObjectToString(row.Cells(32).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Identification Type Code " + """" + row.Cells(32).Value.ToString() + """"
                    flagWarning = True
                End If

                If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(36).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Identification Country Type Code " + """" + row.Cells(36).Value.ToString() + """"
                    flagWarning = True
                End If

                ''emp Phone

                If NullHelper.ObjectToString(row.Cells(41).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(42).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(44).Value).Trim() <> "" Then ' phone contact type

                    'flagWarning = False
                ElseIf NullHelper.ObjectToString(row.Cells(41).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(42).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(44).Value).Trim() = "" Then ' phone contact type

                    'flagWarning = False

                Else
                    flagWarning = True
                    strErrMsg = strErrMsg + " | Warning : Required Emp Phone Field Missing, Data might not be saved properly "
                End If


                If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(41).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Employer Phone Contact Type Code " + """" + row.Cells(41).Value.ToString() + """"
                    flagWarning = True
                End If

                If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(42).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Employer Phone Communication Type Code " + """" + row.Cells(42).Value.ToString() + """"
                    flagWarning = True
                End If

                ''emp address


                If NullHelper.ObjectToString(row.Cells(46).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(47).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(49).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(50).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(51).Value).Trim() <> "" Then ' phone contact type

                    'flagWarning = False
                ElseIf NullHelper.ObjectToString(row.Cells(46).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(47).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(49).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(50).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(51).Value).Trim() = "" Then ' phone contact type

                    'flagWarning = False

                Else
                    flagWarning = True
                    strErrMsg = strErrMsg + " | Warning : Required Address Field Missing, Data might not be saved properly "
                End If

                If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(46).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid Employer Address Type Code " + """" + row.Cells(46).Value.ToString() + """"
                    flagWarning = True
                End If

                If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(50).Value), True) = False Then

                    strErrMsg = strErrMsg + " | Invalid employer Address Country Code " + """" + row.Cells(50).Value.ToString() + """"
                    flagWarning = True
                End If


                If flagRequireField = True Then
                    strErrMsg = strErrMsg & "| Require Field Missing |"
                End If

                If flagWarning = True Then
                    row.Cells(59).Value = strErrMsg
                    'row.DefaultCellStyle.BackColor = Color.Yellow
                End If

                If flagRequireField = True Then

                    row.Cells(58).Value = 1
                    row.DefaultCellStyle.BackColor = Color.Red
                    row.Cells(59).Value = strErrMsg
                    btnPrepareBearer.Enabled = False



                End If


            Next


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub



    Private Function CheckValidData() As Boolean
        Return True

    End Function



    Private Function SaveData() As TransState

        Dim tStatus As TransState

        Dim intModno As Integer = 0
        Dim _intModno As Integer = 0
        Dim intMod As Integer = 0
        Dim intEntityMod As Integer = 0
        Dim _intEntityMod As Integer = 0

        Dim AcNumber As String = ""

        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Using conn As DbConnection = db.CreateConnection()

            conn.Open()

            Dim trans As DbTransaction = conn.BeginTransaction()

            Dim strSql = ""
            Dim IntBearerID As Integer = 0


            Dim PhoneSlno As Integer = 1
            Dim AddressSlno As Integer = 1
            Dim EmpPhnSlno As Integer = 1
            Dim EmpAddressSlno As Integer = 1
            Dim IDentSlno As Integer = 1



            For i = 0 To dgView.Rows.Count - 1

                _debugRow = i + 1

                If NullHelper.ObjectToString(dgView.Rows(i).Cells(0).Value) = "" Then

                    'Add Bearer/Depositor
                    Dim dtBearerRef As DataTable
                    Dim commProc As DbCommand


                    strSql = "SELECT REFERENCE_NUMBER FROM GO_BEARER_INFO WHERE REFERENCE_NUMBER=@REFERENCE_NUMBER"


                    commProc = db.GetSqlStringCommand(strSql)

                    commProc.Parameters.Clear()

                    db.AddInParameter(commProc, "@REFERENCE_NUMBER", DbType.String, dgView.Rows(i).Cells(1).Value.ToString)

                    dtBearerRef = db.ExecuteDataSet(commProc, trans).Tables(0)


                    If dtBearerRef.Rows.Count > 0 Then

                        MessageBox.Show("Duplicate Bearer/Depositor Reference Number : " + dgView.Rows(i).Cells(1).Value.ToString() + " " + Environment.NewLine + "Error Row: " + _debugRow.ToString(), "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Function

                    Else

                        strSql = "SELECT REFERENCE_NUMBER FROM GO_BEARER_INFO_HIST WHERE REFERENCE_NUMBER=@REFERENCE_NUMBER"


                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()

                        db.AddInParameter(commProc, "@REFERENCE_NUMBER", DbType.String, dgView.Rows(i).Cells(1).Value.ToString)

                        dtBearerRef = db.ExecuteDataSet(commProc, trans).Tables(0)


                        If dtBearerRef.Rows.Count > 0 Then

                            MessageBox.Show("Duplicate Bearer/Depositor Reference Number : " + dgView.Rows(i).Cells(1).Value.ToString() + " " + Environment.NewLine + "Error Row: " + _debugRow.ToString(), "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Function

                        Else

                            Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_MAX_Bearer_ID")

                            commProc2.Parameters.Clear()

                            Dim maxid As Integer = db.ExecuteDataSet(commProc2, trans).Tables(0).Rows(0)(0)

                            IntBearerID = maxid + 1

                            commProc = db.GetStoredProcCommand("GO_Bearer_Add")

                            commProc.Parameters.Clear()


                            db.AddInParameter(commProc, "@PERSON_ID", DbType.String, IntBearerID.ToString())
                            db.AddInParameter(commProc, "@REFERENCE_NUMBER", DbType.String, dgView.Rows(i).Cells(1).Value)
                            db.AddInParameter(commProc, "@GENDER", DbType.String, dgView.Rows(i).Cells(2).Value)
                            db.AddInParameter(commProc, "@TITLE", DbType.String, dgView.Rows(i).Cells(3).Value)
                            db.AddInParameter(commProc, "@FIRST_NAME", DbType.String, dgView.Rows(i).Cells(4).Value)
                            db.AddInParameter(commProc, "@MIDDLE_NAME", DbType.String, dgView.Rows(i).Cells(5).Value)
                            db.AddInParameter(commProc, "@LAST_NAME", DbType.String, dgView.Rows(i).Cells(6).Value)
                            db.AddInParameter(commProc, "@PREFIX", DbType.String, dgView.Rows(i).Cells(7).Value)
                            db.AddInParameter(commProc, "@BIRTHDATE", DbType.DateTime, dgView.Rows(i).Cells(8).Value)
                            db.AddInParameter(commProc, "@BIRTH_PLACE", DbType.String, dgView.Rows(i).Cells(9).Value)
                            db.AddInParameter(commProc, "@MOTHERS_NAME", DbType.String, dgView.Rows(i).Cells(10).Value)
                            db.AddInParameter(commProc, "@ALIAS", DbType.String, dgView.Rows(i).Cells(11).Value)
                            db.AddInParameter(commProc, "@SSN", DbType.String, dgView.Rows(i).Cells(12).Value)
                            db.AddInParameter(commProc, "@PASSPORT_NUMBER", DbType.String, dgView.Rows(i).Cells(13).Value)
                            db.AddInParameter(commProc, "@PASSPORT_COUNTRY", DbType.String, dgView.Rows(i).Cells(14).Value)
                            db.AddInParameter(commProc, "@ID_NUMBER", DbType.String, dgView.Rows(i).Cells(15).Value)
                            db.AddInParameter(commProc, "@NATIONALITY1", DbType.String, dgView.Rows(i).Cells(16).Value)
                            db.AddInParameter(commProc, "@NATIONALITY2", DbType.String, dgView.Rows(i).Cells(17).Value)
                            db.AddInParameter(commProc, "@NATIONALITY3", DbType.String, dgView.Rows(i).Cells(18).Value)
                            db.AddInParameter(commProc, "@RESIDENCE", DbType.String, dgView.Rows(i).Cells(19).Value)
                            db.AddInParameter(commProc, "@EMAIL", DbType.String, dgView.Rows(i).Cells(38).Value)
                            db.AddInParameter(commProc, "@EMAIL2", DbType.String, "")
                            db.AddInParameter(commProc, "@EMAIL3", DbType.String, "")
                            db.AddInParameter(commProc, "@OCCUPATION", DbType.String, dgView.Rows(i).Cells(39).Value)
                            db.AddInParameter(commProc, "@EMPLOYER_NAME", DbType.String, dgView.Rows(i).Cells(40).Value)
                            db.AddInParameter(commProc, "@DECEASED", DbType.String, dgView.Rows(i).Cells(52).Value)

                            db.AddInParameter(commProc, "@DECEASED_DATE", DbType.DateTime, dgView.Rows(i).Cells(53).Value)

                            db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, dgView.Rows(i).Cells(54).Value)
                            db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, dgView.Rows(i).Cells(55).Value)
                            db.AddInParameter(commProc, "@SOURCE_OF_WEALTH", DbType.String, dgView.Rows(i).Cells(56).Value)
                            db.AddInParameter(commProc, "@COMMENTS", DbType.String, dgView.Rows(i).Cells(57).Value)



                            db.ExecuteNonQuery(commProc, trans)

                            ' Phone

                            If NullHelper.ObjectToString(dgView.Rows(i).Cells(20).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(21).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(23).Value) <> "" Then



                                Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_BearerPhone_Add")

                                commProcSche.Parameters.Clear()

                                db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, 1)
                                db.AddInParameter(commProcSche, "@PERSON_ID", DbType.String, IntBearerID)
                                db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                                db.AddInParameter(commProcSche, "@SLNO", DbType.String, 1)
                                db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(20).Value)
                                db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(21).Value)
                                db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(22).Value)
                                db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(23).Value)
                                db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(24).Value)

                                db.ExecuteNonQuery(commProcSche, trans)

                            End If

                            If NullHelper.ObjectToString(dgView.Rows(i).Cells(25).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(26).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(28).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(30).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(31).Value) <> "" Then

                                Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_BearerAddress_Add")

                                commProcAdd.Parameters.Clear()

                                db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, 1)
                                db.AddInParameter(commProcAdd, "@PERSON_ID", DbType.String, IntBearerID.ToString())
                                db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                                db.AddInParameter(commProcAdd, "@SLNO", DbType.String, 1)
                                db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(25).Value)
                                db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(26).Value)
                                db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(27).Value)
                                db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(28).Value)
                                db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(29).Value)
                                db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(30).Value)
                                db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(31).Value)

                                db.ExecuteNonQuery(commProcAdd, trans)

                            End If

                            'Add Identification
                            If NullHelper.ObjectToString(dgView.Rows(i).Cells(32).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(33).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(36).Value) <> "" Then

                                Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_BearerIdentification_Add")

                                commProcIdent.Parameters.Clear()

                                db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, 1)
                                db.AddInParameter(commProcIdent, "@PERSON_ID", DbType.String, IntBearerID.ToString())
                                db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                                db.AddInParameter(commProcIdent, "@SLNO", DbType.String, 1)
                                db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(32).Value)
                                db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(33).Value)
                                db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(34).Value)
                                db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(35).Value)
                                db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(37).Value)
                                db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(36).Value)

                                db.ExecuteNonQuery(commProcIdent, trans)

                            End If



                            ' Employer Phone

                            If (NullHelper.ObjectToString(dgView.Rows(i).Cells(41).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(42).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(44).Value) <> "") Then

                                Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_BearerPhone_Add")

                                commProcEMPhone.Parameters.Clear()

                                db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, 1)
                                db.AddInParameter(commProcEMPhone, "@PERSON_ID", DbType.String, IntBearerID.ToString())
                                db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                                db.AddInParameter(commProcEMPhone, "@SLNO", DbType.String, 1)
                                db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(41).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(42).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(43).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(44).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(45).Value)

                                db.ExecuteNonQuery(commProcEMPhone, trans)

                            End If


                            ' Add Employer Address 

                            If (NullHelper.ObjectToString(dgView.Rows(i).Cells(46).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(47).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(49).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(50).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(51).Value) <> "") Then

                                Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_BearerAddress_Add")

                                commProcEMAdd.Parameters.Clear()

                                db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, 1)
                                db.AddInParameter(commProcEMAdd, "@PERSON_ID", DbType.String, IntBearerID.ToString())
                                db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                                db.AddInParameter(commProcEMAdd, "@SLNO", DbType.String, 1)
                                db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(46).Value)
                                db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(47).Value)
                                db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(48).Value)
                                db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(49).Value)
                                db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, "")
                                db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(50).Value)
                                db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(51).Value)

                                db.ExecuteNonQuery(commProcEMAdd, trans)

                            End If ' history check end


                        End If




                    End If ' main table check end

                Else

                    ' update
                    IntBearerID = dgView.Rows(i).Cells(0).Value.ToString()

                    Dim commProc As DbCommand
                    Dim commProcMod As DbCommand = db.GetStoredProcCommand("GO_Bearer_GetMaxMod")

                    commProcMod.Parameters.Clear()

                    db.AddInParameter(commProcMod, "@PERSON_ID", DbType.String, IntBearerID.ToString)

                    intModno = db.ExecuteDataSet(commProcMod, trans).Tables(0).Rows(0)(0)


                    commProc = db.GetStoredProcCommand("GO_Bearer_Update")

                    commProc.Parameters.Clear()


                    db.AddInParameter(commProc, "@PERSON_ID", DbType.String, IntBearerID.ToString())
                    db.AddInParameter(commProc, "@REFERENCE_NUMBER", DbType.String, dgView.Rows(i).Cells(1).Value)
                    db.AddInParameter(commProc, "@GENDER", DbType.String, dgView.Rows(i).Cells(2).Value)
                    db.AddInParameter(commProc, "@TITLE", DbType.String, dgView.Rows(i).Cells(3).Value)
                    db.AddInParameter(commProc, "@FIRST_NAME", DbType.String, dgView.Rows(i).Cells(4).Value)
                    db.AddInParameter(commProc, "@MIDDLE_NAME", DbType.String, dgView.Rows(i).Cells(5).Value)
                    db.AddInParameter(commProc, "@LAST_NAME", DbType.String, dgView.Rows(i).Cells(6).Value)
                    db.AddInParameter(commProc, "@PREFIX", DbType.String, dgView.Rows(i).Cells(7).Value)
                    db.AddInParameter(commProc, "@BIRTHDATE", DbType.DateTime, dgView.Rows(i).Cells(8).Value)
                    db.AddInParameter(commProc, "@BIRTH_PLACE", DbType.String, dgView.Rows(i).Cells(9).Value)
                    db.AddInParameter(commProc, "@MOTHERS_NAME", DbType.String, dgView.Rows(i).Cells(10).Value)
                    db.AddInParameter(commProc, "@ALIAS", DbType.String, dgView.Rows(i).Cells(11).Value)
                    db.AddInParameter(commProc, "@SSN", DbType.String, dgView.Rows(i).Cells(12).Value)
                    db.AddInParameter(commProc, "@PASSPORT_NUMBER", DbType.String, dgView.Rows(i).Cells(13).Value)
                    db.AddInParameter(commProc, "@PASSPORT_COUNTRY", DbType.String, dgView.Rows(i).Cells(14).Value)
                    db.AddInParameter(commProc, "@ID_NUMBER", DbType.String, dgView.Rows(i).Cells(15).Value)
                    db.AddInParameter(commProc, "@NATIONALITY1", DbType.String, dgView.Rows(i).Cells(16).Value)
                    db.AddInParameter(commProc, "@NATIONALITY2", DbType.String, dgView.Rows(i).Cells(17).Value)
                    db.AddInParameter(commProc, "@NATIONALITY3", DbType.String, dgView.Rows(i).Cells(18).Value)
                    db.AddInParameter(commProc, "@RESIDENCE", DbType.String, dgView.Rows(i).Cells(19).Value)
                    db.AddInParameter(commProc, "@EMAIL", DbType.String, dgView.Rows(i).Cells(38).Value)
                    db.AddInParameter(commProc, "@EMAIL2", DbType.String, "")
                    db.AddInParameter(commProc, "@EMAIL3", DbType.String, "")
                    db.AddInParameter(commProc, "@OCCUPATION", DbType.String, dgView.Rows(i).Cells(39).Value)
                    db.AddInParameter(commProc, "@EMPLOYER_NAME", DbType.String, dgView.Rows(i).Cells(40).Value)
                    db.AddInParameter(commProc, "@DECEASED", DbType.String, dgView.Rows(i).Cells(52).Value)

                    db.AddInParameter(commProc, "@DECEASED_DATE", DbType.DateTime, dgView.Rows(i).Cells(53).Value)

                    db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, dgView.Rows(i).Cells(54).Value)
                    db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, dgView.Rows(i).Cells(55).Value)
                    db.AddInParameter(commProc, "@SOURCE_OF_WEALTH", DbType.String, dgView.Rows(i).Cells(56).Value)
                    db.AddInParameter(commProc, "@COMMENTS", DbType.String, dgView.Rows(i).Cells(57).Value)

                    db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intModno)
                    db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)


                    db.ExecuteNonQuery(commProc, trans)

                    _intModno = db.GetParameterValue(commProc, "@RET_MOD_NO")

                    ' Phone

                    If NullHelper.ObjectToString(dgView.Rows(i).Cells(20).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(21).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(23).Value) <> "" Then



                        Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_BearerPhone_Update")

                        commProcSche.Parameters.Clear()

                        db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, 1)
                        db.AddInParameter(commProcSche, "@PERSON_ID", DbType.String, IntBearerID)
                        db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                        db.AddInParameter(commProcSche, "@SLNO", DbType.String, 1)
                        db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(20).Value)
                        db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(21).Value)
                        db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(22).Value)
                        db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(23).Value)
                        db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(24).Value)

                        db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, _intModno)

                        db.ExecuteNonQuery(commProcSche, trans)

                    End If

                    If NullHelper.ObjectToString(dgView.Rows(i).Cells(25).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(26).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(28).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(30).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(31).Value) <> "" Then

                        Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_BearerAddress_Update")

                        commProcAdd.Parameters.Clear()

                        db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, 1)
                        db.AddInParameter(commProcAdd, "@PERSON_ID", DbType.String, IntBearerID.ToString())
                        db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                        db.AddInParameter(commProcAdd, "@SLNO", DbType.String, 1)
                        db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(25).Value)
                        db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(26).Value)
                        db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(27).Value)
                        db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(28).Value)
                        db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(29).Value)
                        db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(30).Value)
                        db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(31).Value)

                        db.AddInParameter(commProcAdd, "@MOD_NO", DbType.Int32, _intModno)

                        db.ExecuteNonQuery(commProcAdd, trans)

                    End If

                    'Add Identification
                    If NullHelper.ObjectToString(dgView.Rows(i).Cells(32).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(33).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(36).Value) <> "" Then

                        Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_BearerIdentification_Update")

                        commProcIdent.Parameters.Clear()

                        db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, 1)
                        db.AddInParameter(commProcIdent, "@PERSON_ID", DbType.String, IntBearerID.ToString())
                        db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                        db.AddInParameter(commProcIdent, "@SLNO", DbType.String, 1)
                        db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(32).Value)
                        db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(33).Value)
                        db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(34).Value)
                        db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(35).Value)
                        db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(37).Value)
                        db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(36).Value)

                        db.AddInParameter(commProcIdent, "@MOD_NO", DbType.Int32, _intModno)

                        db.ExecuteNonQuery(commProcIdent, trans)

                    End If


                    ' Employer Phone

                    If (NullHelper.ObjectToString(dgView.Rows(i).Cells(41).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(42).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(44).Value) <> "") Then

                        Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_BearerPhone_Update")

                        commProcEMPhone.Parameters.Clear()

                        db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, 1)
                        db.AddInParameter(commProcEMPhone, "@PERSON_ID", DbType.String, IntBearerID.ToString())
                        db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                        db.AddInParameter(commProcEMPhone, "@SLNO", DbType.String, 1)
                        db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(41).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(42).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(43).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(44).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(45).Value)

                        db.AddInParameter(commProcEMPhone, "@MOD_NO", DbType.Int32, _intModno)

                        db.ExecuteNonQuery(commProcEMPhone, trans)

                    End If


                    ' Add Employer Address 

                    If (NullHelper.ObjectToString(dgView.Rows(i).Cells(46).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(47).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(49).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(50).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(51).Value) <> "") Then

                        Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_BearerAddress_Update")

                        commProcEMAdd.Parameters.Clear()

                        db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, 1)
                        db.AddInParameter(commProcEMAdd, "@PERSON_ID", DbType.String, IntBearerID.ToString())
                        db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                        db.AddInParameter(commProcEMAdd, "@SLNO", DbType.String, 1)
                        db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(46).Value)
                        db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(47).Value)
                        db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(48).Value)
                        db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(49).Value)
                        db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, "")
                        db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(50).Value)
                        db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(51).Value)

                        db.AddInParameter(commProcEMAdd, "@MOD_NO", DbType.Int32, _intModno)

                        db.ExecuteNonQuery(commProcEMAdd, trans)

                    End If




                End If


            Next




            tStatus = TransState.Add




            trans.Commit()

            


        End Using

        log_message = " Uploaded : Bearer/depositor Person "
        Logger.system_log(log_message)



        Return tStatus

    End Function

    Private Sub FrmImportBearerInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub


    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        opdRetFile.Multiselect = False
        'opdRetFile.Filter() = "XML Files (*.XML)|(*.XML)"
        opdRetFile.FileName = txtFilename.Text

        opdRetFile.ShowDialog()
        txtFilename.Text = opdRetFile.FileName()
    End Sub

    Private Sub btnImportProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportProcess.Click
        If Not File.Exists(txtFilename.Text) Then
            MessageBox.Show("Select File", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnBrowse.Focus()
            Exit Sub
        End If

        ' btnPrepareEntity.Enabled = False

        ReadExcel()
        CheckValidation()
    End Sub

    Private Sub btnPrepareBearer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrepareBearer.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try
            If MessageBox.Show("Do you really want to Save?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                If CheckValidData() Then

                    tState = SaveData()

                    If tState = TransState.Add Then

                        lblToolStatus.Text = "!! Information Updated Successfully !!"

                        MessageBox.Show("Information Updated Successfull." & Environment.NewLine & _
                                        "** Authorization needed from Bearer Info Form", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ElseIf tState = TransState.Exist Then
                        lblToolStatus.Text = "!! Already Exist !!"
                    ElseIf tState = TransState.NoRecord Then
                        lblToolStatus.Text = "!! Nothing to Update !!"
                    ElseIf tState = TransState.DBError Then
                        lblToolStatus.Text = "!! Database error occured. Please, Try Again !!"
                    ElseIf tState = TransState.UnspecifiedError Then
                        lblToolStatus.Text = "!! Unpecified Error Occured !!"
                    End If

                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message + Environment.NewLine + "Error Row: " + _debugRow.ToString(), "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
End Class