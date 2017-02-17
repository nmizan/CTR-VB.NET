'
'Author             : Fahad Khan
'Purpose            : Import Owner Information
'Creation date      : 29-Dec-2013
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



Public Class FrmImportOwnerInfo

    Dim _formName As String = "MaintenanceGoOwnerInfoImport"
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

       
        If lblName.InvokeRequired Then
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

                If sheet.Range("A" & i.ToString()).Value2 Is Nothing Then
                    Exit For
                End If

                If sheet.Range("A" & i.ToString()).Value2.ToString() = "" Then
                    Exit For
                End If

                If sheet.Range("E" & i.ToString()).Value2.ToString() = "" Then
                    Exit For
                End If


                If (i = dgView.Rows.Count + 1) Then
                    dgView.Rows.Add()
                End If

                dgView.Item(0, i - 2).Value = sheet.Range("A" & i.ToString()).Value2  ' Rec Group
                dgView.Item(1, i - 2).Value = sheet.Range("B" & i.ToString()).Value2  ' Mapping Flag
                dgView.Item(2, i - 2).Value = sheet.Range("C" & i.ToString()).Value2  ' Branch Code
                dgView.Item(3, i - 2).Value = sheet.Range("D" & i.ToString()).Value2  ' Owner Code
                dgView.Item(4, i - 2).Value = sheet.Range("E" & i.ToString()).Value2  ' Account No
                dgView.Item(5, i - 2).Value = sheet.Range("F" & i.ToString()).Value2  ' Executive Designation
                dgView.Item(6, i - 2).Value = sheet.Range("G" & i.ToString()).Value2  ' Sign Auth
                dgView.Item(7, i - 2).Value = sheet.Range("H" & i.ToString()).Value2  ' Role
                dgView.Item(8, i - 2).Value = sheet.Range("I" & i.ToString()).Value2  ' Is Primary
                dgView.Item(9, i - 2).Value = sheet.Range("J" & i.ToString()).Value2  ' Gender
                dgView.Item(10, i - 2).Value = sheet.Range("K" & i.ToString()).Value2   ' Title
                dgView.Item(11, i - 2).Value = sheet.Range("L" & i.ToString()).Value2  ' First Name
                dgView.Item(12, i - 2).Value = sheet.Range("M" & i.ToString()).Value2 ' Middle Name
                dgView.Item(13, i - 2).Value = sheet.Range("N" & i.ToString()).Value2 ' Last Name
                dgView.Item(14, i - 2).Value = sheet.Range("O" & i.ToString()).Value2 ' Father Name
                dgView.Item(15, i - 2).Value = sheet.Range("P" & i.ToString()).Value2 ' Mother Name
                dgView.Item(16, i - 2).Value = sheet.Range("Q" & i.ToString()).Value2 ' Spouse name
                dgView.Item(17, i - 2).Value = sheet.Range("R" & i.ToString()).Value  ' Date of Birth
                dgView.Item(18, i - 2).Value = sheet.Range("S" & i.ToString()).Value2 ' Nationality1
                dgView.Item(19, i - 2).Value = sheet.Range("T" & i.ToString()).Value2 ' Nationality2
                dgView.Item(20, i - 2).Value = sheet.Range("U" & i.ToString()).Value2 ' Nationality3
                dgView.Item(21, i - 2).Value = sheet.Range("V" & i.ToString()).Value2 ' Residence
                dgView.Item(22, i - 2).Value = sheet.Range("W" & i.ToString()).Value2 ' Occupation
                dgView.Item(23, i - 2).Value = sheet.Range("X" & i.ToString()).Value2 ' Phone Contact Type
                dgView.Item(24, i - 2).Value = sheet.Range("Y" & i.ToString()).Value2 ' phone Communication Type
                dgView.Item(25, i - 2).Value = sheet.Range("Z" & i.ToString()).Value2 ' Country Prefix
                dgView.Item(26, i - 2).Value = sheet.Range("AA" & i.ToString()).Value2 ' Phone Number
                dgView.Item(27, i - 2).Value = sheet.Range("AB" & i.ToString()).Value2 ' Phone Extension
                dgView.Item(28, i - 2).Value = sheet.Range("AC" & i.ToString()).Value2 ' Address type 
                dgView.Item(29, i - 2).Value = sheet.Range("AD" & i.ToString()).Value2 ' Address
                dgView.Item(30, i - 2).Value = sheet.Range("AE" & i.ToString()).Value2 ' Town
                dgView.Item(31, i - 2).Value = sheet.Range("AF" & i.ToString()).Value2 ' City
                dgView.Item(32, i - 2).Value = sheet.Range("AG" & i.ToString()).Value2 ' Country
                dgView.Item(33, i - 2).Value = sheet.Range("AH" & i.ToString()).Value2 ' Zip
                dgView.Item(34, i - 2).Value = sheet.Range("AI" & i.ToString()).Value2 ' State/division
                dgView.Item(35, i - 2).Value = sheet.Range("AJ" & i.ToString()).Value2 ' Identification type
                dgView.Item(36, i - 2).Value = sheet.Range("AK" & i.ToString()).Value2 ' Identification Number
                dgView.Item(37, i - 2).Value = sheet.Range("AL" & i.ToString()).Value '  issue date
                dgView.Item(38, i - 2).Value = sheet.Range("AM" & i.ToString()).Value '  expiry date
                dgView.Item(39, i - 2).Value = sheet.Range("AN" & i.ToString()).Value2 ' isse country
                dgView.Item(40, i - 2).Value = sheet.Range("AO" & i.ToString()).Value2 ' issued by 
                dgView.Item(41, i - 2).Value = sheet.Range("AP" & i.ToString()).Value2 ' national id
                dgView.Item(42, i - 2).Value = sheet.Range("AQ" & i.ToString()).Value2 ' passport number
                dgView.Item(43, i - 2).Value = sheet.Range("AR" & i.ToString()).Value2 ' birth regi no
                dgView.Item(44, i - 2).Value = sheet.Range("AS" & i.ToString()).Value2 ' birth place 
                dgView.Item(45, i - 2).Value = sheet.Range("AT" & i.ToString()).Value2 ' Employer name
                dgView.Item(46, i - 2).Value = sheet.Range("AU" & i.ToString()).Value2 ' emp contact type
                dgView.Item(47, i - 2).Value = sheet.Range("AV" & i.ToString()).Value2 ' emp communication type
                dgView.Item(48, i - 2).Value = sheet.Range("AW" & i.ToString()).Value2 ' prefix
                dgView.Item(49, i - 2).Value = sheet.Range("AX" & i.ToString()).Value2 ' phone number
                dgView.Item(50, i - 2).Value = sheet.Range("AY" & i.ToString()).Value2 ' extension
                dgView.Item(51, i - 2).Value = sheet.Range("AZ" & i.ToString()).Value2 ' address type
                dgView.Item(52, i - 2).Value = sheet.Range("BA" & i.ToString()).Value2 ' address
                dgView.Item(53, i - 2).Value = sheet.Range("BB" & i.ToString()).Value2 ' town
                dgView.Item(54, i - 2).Value = sheet.Range("BC" & i.ToString()).Value2 ' city
                dgView.Item(55, i - 2).Value = sheet.Range("BD" & i.ToString()).Value2 ' country
                dgView.Item(56, i - 2).Value = sheet.Range("BE" & i.ToString()).Value2 ' zip
                dgView.Item(57, i - 2).Value = sheet.Range("BF" & i.ToString()).Value2 ' state
                dgView.Item(58, i - 2).Value = sheet.Range("BG" & i.ToString()).Value2 ' TIN
                dgView.Item(59, i - 2).Value = sheet.Range("BH" & i.ToString()).Value2 ' BIN
                dgView.Item(60, i - 2).Value = sheet.Range("BI" & i.ToString()).Value2 ' Occupation
                dgView.Item(61, i - 2).Value = sheet.Range("BJ" & i.ToString()).Value2 ' phone no
                dgView.Item(62, i - 2).Value = sheet.Range("BK" & i.ToString()).Value2 ' phone city
                dgView.Item(63, i - 2).Value = sheet.Range("BL" & i.ToString()).Value2 ' phone country
                dgView.Item(64, i - 2).Value = sheet.Range("BM" & i.ToString()).Value2 ' mobile no
                dgView.Item(65, i - 2).Value = sheet.Range("BN" & i.ToString()).Value2 ' mobile city
                dgView.Item(66, i - 2).Value = sheet.Range("BO" & i.ToString()).Value2 ' mobile country
                dgView.Item(67, i - 2).Value = sheet.Range("BP" & i.ToString()).Value2 ' office phone
                dgView.Item(68, i - 2).Value = sheet.Range("BQ" & i.ToString()).Value2 ' office city
                dgView.Item(69, i - 2).Value = sheet.Range("BR" & i.ToString()).Value2 ' office phn country
                dgView.Item(70, i - 2).Value = sheet.Range("BS" & i.ToString()).Value2 ' PREsent Address
                dgView.Item(71, i - 2).Value = sheet.Range("BT" & i.ToString()).Value2 ' present thana
                dgView.Item(72, i - 2).Value = sheet.Range("BU" & i.ToString()).Value2 ' permanent address
                dgView.Item(73, i - 2).Value = sheet.Range("BV" & i.ToString()).Value2 ' permanent thana
                dgView.Item(74, i - 2).Value = sheet.Range("BW" & i.ToString()).Value2 ' Driving license

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


                If RecGroup <> row.Cells(0).Value.ToString() Then


                    If NullHelper.ObjectToString(row.Cells(1).Value).Trim() = "" Then ' mapping

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(2).Value).Trim() = "" Then ' branch

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(4).Value).Trim() = "" Then ' account

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(5).Value).Trim() = "" Then ' executive designation

                        flagRequireField = True

                    End If
                    If NullHelper.ObjectToString(row.Cells(7).Value).Trim() = "" Then ' role

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(8).Value).Trim() = "" Then ' is primary

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(9).Value).Trim() = "" Then ' gender

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(11).Value).Trim() = "" Then ' First Name

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(13).Value).Trim() = "" Then ' Last name

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(14).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(15).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(16).Value).Trim() = "" Then  ' Father

                        flagRequireField = True

                    End If

                    If row.Cells(17).Value.ToString = "" Then ' birth date

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(18).Value).Trim() = "" Then ' nationality

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(21).Value).Trim() = "" Then ' Resident

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(22).Value).Trim() = "" Then ' occupation

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(23).Value).Trim() = "" Then ' phone contact type

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(24).Value).Trim() = "" Then ' phone communication type

                        flagRequireField = True

                    End If
                    If NullHelper.ObjectToString(row.Cells(26).Value).Trim() = "" Then ' phone number

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(28).Value).Trim() = "" Then ' address type

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(29).Value).Trim() = "" Then ' address

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(31).Value).Trim() = "" Then ' city

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(32).Value).Trim() = "" Then ' country

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(34).Value).Trim() = "" Then ' state

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(35).Value).Trim() = "" Then ' identification type

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(36).Value).Trim() = "" Then ' idenyification number

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(39).Value).Trim() = "" Then ' country

                        flagRequireField = True

                    End If



                    'Employer phone

                    If NullHelper.ObjectToString(row.Cells(46).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(47).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(49).Value).Trim() <> "" Then  ' phone contact type

                        'flagWarning = False
                    ElseIf NullHelper.ObjectToString(row.Cells(46).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(47).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(49).Value).Trim() = "" Then
                        'flagWarning = False
                    Else
                        strErrMsg = strErrMsg + " | Warning : Required Employer Phone Field Missing, Phone Data might not be saved properly "
                        flagWarning = True

                    End If



                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(46).Value), True) = False Then
                        flagWarning = True
                        strErrMsg = strErrMsg + " | Invalid Phone Contact Type Code " + """" + row.Cells(46).Value.ToString() + """"

                    End If

                    If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(47).Value), True) = False Then

                        flagWarning = True
                        strErrMsg = strErrMsg + " | Invalid Phone Communication Type Code " + """" + row.Cells(47).Value.ToString() + """"

                    End If



                    'Employer address

                    If NullHelper.ObjectToString(row.Cells(51).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(52).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(54).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(55).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(57).Value).Trim() <> "" Then

                        'flagWarning = False

                    ElseIf NullHelper.ObjectToString(row.Cells(51).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(52).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(54).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(55).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(57).Value).Trim() = "" Then

                        'flagWarning = False
                    Else

                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Employer Address Field Missing, Address might not be saved properly "
                    End If


                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(51).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Emp Address Type Code " + """" + row.Cells(51).Value.ToString() + """"
                        flagWarning = True
                    End If


                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(55).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Emp Address Country Type Code " + """" + row.Cells(55).Value.ToString() + """"
                        flagWarning = True
                    End If



                    If NullHelper.ObjectToString(row.Cells(41).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(42).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(43).Value).Trim() = "" Then
                        flagRequireField = True


                    End If

                    'If NullHelper.ObjectToString(row.Cells(41).Value).Trim() <> "" And Len(NullHelper.ObjectToString(row.Cells(41).Value).Trim()) <> 17 And Len(NullHelper.ObjectToString(row.Cells(41).Value).Trim()) <> 13 Then

                    '    strErrMsg = strErrMsg + " | Invalid National ID Number " + """" + row.Cells(41).Value.ToString() + """" + " It might be 13 or 17 digits "
                    '    flagRequireField = True


                    'End If


                    If NullHelper.ObjectToString(row.Cells(60).Value).Trim() = "" Then ' occupation

                        flagRequireField = True

                    End If


                    If TypeChecker.IsContained("account_person_role_type", NullHelper.ObjectToString(row.Cells(7).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Role Type " + """" + row.Cells(7).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(18).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Nationality1 Country Code " + """" + row.Cells(18).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(19).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Nationality2 Country Code " + """" + row.Cells(19).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(20).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Nationality3 Country Code " + """" + row.Cells(20).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(21).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Residence Country Code " + """" + row.Cells(21).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(23).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Phone Contact Type Code " + """" + row.Cells(23).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(24).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Phone Communication Type Code " + """" + row.Cells(24).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(28).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Address Type Code " + """" + row.Cells(28).Value.ToString() + """"
                        flagRequireField = True
                    End If


                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(32).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Address Country Type Code " + """" + row.Cells(32).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("identifier_type", NullHelper.ObjectToString(row.Cells(35).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Identification Type Code " + """" + row.Cells(35).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(39).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Identification Country Type Code " + """" + row.Cells(39).Value.ToString() + """"
                        flagRequireField = True
                    End If



                    'If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(46).Value), True) = False Then

                    '    strErrMsg = strErrMsg + " | Invalid Employer Phone Contact Type Code " + """" + row.Cells(46).Value.ToString() + """"
                    '    flagRequireField = True
                    'End If

                    'If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(47).Value), True) = False Then

                    '    strErrMsg = strErrMsg + " | Invalid employer Phone Communication Type Code " + """" + row.Cells(47).Value.ToString() + """"
                    '    flagRequireField = True
                    'End If

                    'If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(51).Value), True) = False Then

                    '    strErrMsg = strErrMsg + " | Invalid Employer Address Type Code " + """" + row.Cells(51).Value.ToString() + """"
                    '    flagRequireField = True
                    'End If


                    'If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(55).Value), True) = False Then

                    '    strErrMsg = strErrMsg + " | Invalid Employer Address Country Type Code " + """" + row.Cells(55).Value.ToString() + """"
                    '    flagRequireField = True
                    'End If


                Else

                    'phone

                    If NullHelper.ObjectToString(row.Cells(23).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(24).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(26).Value).Trim() <> "" Then  ' phone contact type

                        'flagWarning = False
                    ElseIf NullHelper.ObjectToString(row.Cells(23).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(24).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(26).Value).Trim() = "" Then
                        'flagWarning = False
                    Else

                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Phone Field Missing, Phone Data might not be saved properly "
                    End If

                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(23).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Phone Contact Type Code " + """" + row.Cells(23).Value.ToString() + """"
                        flagWarning = True
                    End If

                    If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(24).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Phone Communication Type Code " + """" + row.Cells(24).Value.ToString() + """"
                        flagWarning = True
                    End If

                    'address

                    If NullHelper.ObjectToString(row.Cells(28).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(29).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(31).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(32).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(34).Value).Trim() <> "" Then

                        'flagWarning = False

                    ElseIf NullHelper.ObjectToString(row.Cells(28).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(29).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(31).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(32).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(34).Value).Trim() = "" Then
                        'flagWarning = False

                    Else

                        strErrMsg = strErrMsg + " | Warning : Required Address Field Missing, Address might not be saved properly "
                        flagWarning = True

                    End If

                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(28).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Address Type Code " + """" + row.Cells(28).Value.ToString() + """"
                        flagRequireField = True
                    End If


                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(32).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Address Country Type Code " + """" + row.Cells(32).Value.ToString() + """"
                        flagRequireField = True
                    End If



                    'Identification 

                    If NullHelper.ObjectToString(row.Cells(35).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(36).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(39).Value).Trim() <> "" Then

                        'flagWarning = False

                    ElseIf NullHelper.ObjectToString(row.Cells(35).Value) = "" And NullHelper.ObjectToString(row.Cells(36).Value) = "" And NullHelper.ObjectToString(row.Cells(39).Value) = "" Then

                        'flagWarning = False
                    Else
                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Identification Field Missing, Data might not be saved properly "


                    End If

                    If TypeChecker.IsContained("identifier_type", NullHelper.ObjectToString(row.Cells(35).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Identification Type Code " + """" + row.Cells(35).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(39).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Identification Country Type Code " + """" + row.Cells(39).Value.ToString() + """"
                        flagRequireField = True
                    End If


                    'Employer phone

                    If NullHelper.ObjectToString(row.Cells(46).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(47).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(49).Value).Trim() <> "" Then  ' phone contact type

                        'flagWarning = False
                    ElseIf NullHelper.ObjectToString(row.Cells(46).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(47).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(49).Value).Trim() = "" Then
                        'flagWarning = False
                    Else

                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Employer Phone Data Missing, Phone Data might not be saved properly "
                    End If


                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(46).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Employer Phone Contact Type Code " + """" + row.Cells(46).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(47).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid employer Phone Communication Type Code " + """" + row.Cells(47).Value.ToString() + """"
                        flagRequireField = True
                    End If



                    'Employer address

                    If NullHelper.ObjectToString(row.Cells(51).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(52).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(54).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(55).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(57).Value).Trim() <> "" Then

                        'flagWarning = False

                    ElseIf NullHelper.ObjectToString(row.Cells(51).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(52).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(54).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(55).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(57).Value).Trim() = "" Then

                        'flagWarning = False

                    Else

                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Employer Address Field Missing, Address might not be saved properly "
                    End If


                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(51).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Employer Address Type Code " + """" + row.Cells(51).Value.ToString() + """"
                        flagRequireField = True
                    End If


                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(55).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Employer Address Country Type Code " + """" + row.Cells(55).Value.ToString() + """"
                        flagRequireField = True
                    End If



                End If

                RecGroup = row.Cells(0).Value.ToString()



                If flagRequireField = True Then
                    strErrMsg = strErrMsg & "| Require Field Missing |"
                End If

                If flagWarning = True Then

                    row.Cells(76).Value = strErrMsg
                    'row.DefaultCellStyle.BackColor = Color.Yellow
                End If


                If flagRequireField = True Then

                    row.Cells(75).Value = 1
                    row.DefaultCellStyle.BackColor = Color.Red
                    row.Cells(76).Value = strErrMsg
                    btnPrepareOwner.Enabled = False


                End If


            Next

            ' lblTotalCheckAmount.Text = CheckTotal.ToString()
            ' lblTotalCheckNo.Text = CheckNoTotal.ToString()

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
        Dim intMod As Integer = 0
        Dim _intModno As Integer = 0

        tStatus = TransState.UnspecifiedError

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Using conn As DbConnection = db.CreateConnection()

            conn.Open()

            Dim trans As DbTransaction = conn.BeginTransaction()

            Dim strSql = ""

            Dim OwnerCode As String = ""
            Dim IntEntityID As Integer = 0

            Dim RecGroup As String = ""
           
            Dim PhoneSlno As Integer = 1
            Dim AddressSlno As Integer = 1
            Dim EmpPhnSlno As Integer = 1
            Dim EmpAddressSlno As Integer = 1
            Dim IDentSlno As Integer = 1

            Dim dtOwnerInfoMap As DataTable


            For i = 0 To dgView.Rows.Count - 1

                _debugRow = i + 1

                If RecGroup <> dgView.Rows(i).Cells(0).Value.ToString() Then 'different group start


                    'reset variables
                    PhoneSlno = 1
                    AddressSlno = 1
                    EmpPhnSlno = 1
                    EmpAddressSlno = 1
                    IDentSlno = 1



                    'owner code saving(add,update)


                    If dgView.Rows(i).Cells(3).Value = "" Then 'check owner info




                        If Not dgView.Rows(i).Cells(2).Value = "" Then


                            strSql = "select right('00000' + convert(varchar,isnull(max(convert(bigint,substring(OWNER_CODE,8,5))),0)+1),5) from dbo.FIU_OWNER_INFO where substring(OWNER_CODE,1,7)='026" & dgView.Rows(i).Cells(2).Value & "'"    '"0201'"



                            Dim td As New DataTable
                            td = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0)

                            If td.Rows.Count > 0 Then
                                OwnerCode = "026" + dgView.Rows(i).Cells(2).Value + td.Rows(0)(0).ToString().Trim()
                            End If




                        End If

                        'FIU_Owner Info Add

                        Dim commProc As DbCommand = db.GetStoredProcCommand("GO_FIUOwnerInfo_Add")

                        commProc.Parameters.Clear()

                        db.AddInParameter(commProc, "@P_OWNER_CODE", DbType.String, OwnerCode)
                        db.AddInParameter(commProc, "@P_OWNER_NAME", DbType.String, dgView.Rows(i).Cells(11).Value + " " + dgView.Rows(i).Cells(13).Value)

                        db.AddInParameter(commProc, "@P_OCTYPECODE", DbType.String, dgView.Rows(i).Cells(60).Value)
                        db.AddInParameter(commProc, "@P_GENDER", DbType.String, dgView.Rows(i).Cells(9).Value)

                        db.AddInParameter(commProc, "@P_OWNER_FATHER", DbType.String, dgView.Rows(i).Cells(14).Value)
                        db.AddInParameter(commProc, "@P_OWNER_MOTHER", DbType.String, dgView.Rows(i).Cells(15).Value)
                        db.AddInParameter(commProc, "@P_OWNER_SPOUSE", DbType.String, dgView.Rows(i).Cells(16).Value)

                        db.AddInParameter(commProc, "@P_DOB", DbType.DateTime, Convert.ToDateTime(dgView.Rows(i).Cells(17).Value).Date)

                        db.AddInParameter(commProc, "@P_PHONE_RES1", DbType.String, dgView.Rows(i).Cells(61).Value)
                        db.AddInParameter(commProc, "@P_PHONE_CITY_RES1", DbType.String, dgView.Rows(i).Cells(62).Value)
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_RES1", DbType.String, dgView.Rows(i).Cells(63).Value)
                        db.AddInParameter(commProc, "@P_PHONE_RES2", DbType.String, "")
                        db.AddInParameter(commProc, "@P_PHONE_CITY_RES2", DbType.String, "")
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_RES2", DbType.String, "")
                        db.AddInParameter(commProc, "@P_MOBILE1", DbType.String, dgView.Rows(i).Cells(64).Value)
                        db.AddInParameter(commProc, "@P_MOBILE1_CITY", DbType.String, dgView.Rows(i).Cells(65).Value)
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_MOB1", DbType.String, dgView.Rows(i).Cells(66).Value)
                        db.AddInParameter(commProc, "@P_MOBILE2", DbType.String, "")
                        db.AddInParameter(commProc, "@P_MOBILE2_CITY", DbType.String, "")
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_MOB2", DbType.String, "")
                        db.AddInParameter(commProc, "@P_PHONE_OFF1", DbType.String, dgView.Rows(i).Cells(67).Value)
                        db.AddInParameter(commProc, "@P_PHONE_CITY_OFF1", DbType.String, dgView.Rows(i).Cells(68).Value)
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_OFF1", DbType.String, dgView.Rows(i).Cells(69).Value)
                        db.AddInParameter(commProc, "@P_PHONE_OFF2", DbType.String, "")
                        db.AddInParameter(commProc, "@P_PHONE_CITY_OFF2", DbType.String, "")
                        db.AddInParameter(commProc, "@P_COUNTRY_CODE_OFF2", DbType.String, "")
                        db.AddInParameter(commProc, "@P_PPNO", DbType.String, dgView.Rows(i).Cells(42).Value)
                        db.AddInParameter(commProc, "@P_DRIVINGLNO", DbType.String, dgView.Rows(i).Cells(74).Value)
                        db.AddInParameter(commProc, "@P_TIN", DbType.String, dgView.Rows(i).Cells(58).Value)
                        db.AddInParameter(commProc, "@P_BIN", DbType.String, dgView.Rows(i).Cells(59).Value)
                        db.AddInParameter(commProc, "@P_PRES_ADDR", DbType.String, dgView.Rows(i).Cells(70).Value)
                        db.AddInParameter(commProc, "@P_PRES_THANA_CODE", DbType.String, dgView.Rows(i).Cells(71).Value)
                        db.AddInParameter(commProc, "@P_PERM_ADDR", DbType.String, dgView.Rows(i).Cells(72).Value)
                        db.AddInParameter(commProc, "@P_PERM_THANA_CODE", DbType.String, dgView.Rows(i).Cells(73).Value)
                        db.AddInParameter(commProc, "@P_BB_OWNER_CODE", DbType.String, "")


                        db.ExecuteNonQuery(commProc, trans)

                        'GO_Owner_info Add

                        Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_OwnerInfo_Add")

                        commProc2.Parameters.Clear()


                        db.AddInParameter(commProc2, "@OWNER_CODE", DbType.String, OwnerCode)


                        db.AddInParameter(commProc2, "@TITLE", DbType.String, dgView.Rows(i).Cells(10).Value)
                        db.AddInParameter(commProc2, "@FIRST_NAME", DbType.String, dgView.Rows(i).Cells(11).Value)
                        db.AddInParameter(commProc2, "@MIDDLE_NAME", DbType.String, dgView.Rows(i).Cells(12).Value)

                        db.AddInParameter(commProc2, "@LAST_NAME", DbType.String, dgView.Rows(i).Cells(13).Value)

                        db.AddInParameter(commProc2, "@BIRTH_PLACE", DbType.String, dgView.Rows(i).Cells(44).Value)

                        db.AddInParameter(commProc2, "@SSN", DbType.String, dgView.Rows(i).Cells(41).Value)

                        db.AddInParameter(commProc2, "@ID_NUMBER", DbType.String, dgView.Rows(i).Cells(43).Value)
                        db.AddInParameter(commProc2, "@NATIONALITY1", DbType.String, dgView.Rows(i).Cells(18).Value)
                        db.AddInParameter(commProc2, "@NATIONALITY2", DbType.String, dgView.Rows(i).Cells(19).Value)
                        db.AddInParameter(commProc2, "@NATIONALITY3", DbType.String, dgView.Rows(i).Cells(20).Value)
                        db.AddInParameter(commProc2, "@RESIDENCE", DbType.String, dgView.Rows(i).Cells(21).Value)
                        db.AddInParameter(commProc2, "@EMPLOYER_NAME", DbType.String, dgView.Rows(i).Cells(45).Value)
                        db.AddInParameter(commProc2, "@OCP_CODE", DbType.String, dgView.Rows(i).Cells(22).Value)

                        db.AddInParameter(commProc2, "@EMAIL", DbType.String, "")
                        db.AddInParameter(commProc2, "@EMAIL2", DbType.String, "")
                        db.AddInParameter(commProc2, "@EMAIL3", DbType.String, "")
                        db.AddInParameter(commProc2, "@DECEASED", DbType.String, 0)

                        db.AddInParameter(commProc2, "@DECEASED_DATE", DbType.DateTime, NullHelper.StringToDate(""))


                        db.AddInParameter(commProc2, "@TAX_REG_NUMBER", DbType.String, "")
                        db.AddInParameter(commProc2, "@SOURCE_OF_WEALTH", DbType.String, "")
                        db.AddInParameter(commProc2, "@COMMENTS", DbType.String, "")


                        db.ExecuteNonQuery(commProc2, trans)


                        'Personal Phone


                        Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_OwnerInfoPhone_Add")

                        commProcSche.Parameters.Clear()

                        db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, PhoneSlno)
                        db.AddInParameter(commProcSche, "@OWNER_CODE", DbType.String, OwnerCode)
                        db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                        db.AddInParameter(commProcSche, "@SLNO", DbType.Int32, PhoneSlno)
                        db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(23).Value)
                        db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(24).Value)
                        db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(25).Value)
                        db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(26).Value)
                        db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(27).Value)

                        db.ExecuteNonQuery(commProcSche, trans)


                        'Add Person Address 

                        Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_OwnerInfoAddress_Add")

                        commProcAdd.Parameters.Clear()

                        db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, AddressSlno)
                        db.AddInParameter(commProcAdd, "@OWNER_CODE", DbType.String, OwnerCode)
                        db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                        db.AddInParameter(commProcAdd, "@SLNO", DbType.Int32, AddressSlno)
                        db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(28).Value)
                        db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(29).Value)
                        db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(30).Value)
                        db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(31).Value)
                        db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(33).Value)
                        db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(32).Value)
                        db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(34).Value)

                        db.ExecuteNonQuery(commProcAdd, trans)


                        'Add Identification

                        Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_OwnerInfoIdentification_Add")

                        commProcIdent.Parameters.Clear()

                        db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, IDentSlno)
                        db.AddInParameter(commProcIdent, "@OWNER_CODE", DbType.String, OwnerCode)
                        db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                        db.AddInParameter(commProcIdent, "@SLNO", DbType.Int32, IDentSlno)
                        db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(35).Value)
                        db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(36).Value)
                        db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(37).Value)
                        db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(38).Value)
                        db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(40).Value)
                        db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(39).Value)

                        db.ExecuteNonQuery(commProcIdent, trans)

                        ' Employer Phone

                        If (NullHelper.ObjectToString(dgView.Rows(i).Cells(46).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(47).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(49).Value) <> "") Then

                            Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_OwnerInfoPhone_Add")

                            commProcEMPhone.Parameters.Clear()

                            db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, EmpPhnSlno)
                            db.AddInParameter(commProcEMPhone, "@OWNER_CODE", DbType.String, OwnerCode)
                            db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                            db.AddInParameter(commProcEMPhone, "@SLNO", DbType.Int32, EmpPhnSlno)
                            db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(46).Value)
                            db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(47).Value)
                            db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(48).Value)
                            db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(49).Value)
                            db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(50).Value)

                            db.ExecuteNonQuery(commProcEMPhone, trans)

                        End If


                        ' Add Employer Address 

                        If (NullHelper.ObjectToString(dgView.Rows(i).Cells(51).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(52).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(54).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(55).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(57).Value) <> "") Then

                            Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_OwnerInfoAddress_Add")

                            commProcEMAdd.Parameters.Clear()

                            db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, EmpAddressSlno)
                            db.AddInParameter(commProcEMAdd, "@OWNER_CODE", DbType.String, OwnerCode)
                            db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                            db.AddInParameter(commProcEMAdd, "@SLNO", DbType.Int32, EmpAddressSlno)
                            db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(51).Value)
                            db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(52).Value)
                            db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(53).Value)
                            db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(54).Value)
                            db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(56).Value)
                            db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(55).Value)
                            db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(57).Value)

                            db.ExecuteNonQuery(commProcEMAdd, trans)



                        End If

                        _intModno = 1


                    Else

                        ' Update Statement

                        OwnerCode = dgView.Rows(i).Cells(3).Value

                        If dgView.Rows(i).Cells(1).Value.ToString <> "R" Then

                            'FIU OWNER INFO

                            db.ExecuteNonQuery(trans, CommandType.Text, "delete FIU_OWNER_INFO where OWNER_CODE='" & OwnerCode & "' and IS_AUTHORIZED=0")
                            Dim ds As New DataSet


                            strSql = "select isnull(max(MODNO),0)+1 maxmodno from FIU_OWNER_INFO where OWNER_CODE='" & OwnerCode & "'"


                            intModno = db.ExecuteDataSet(trans, CommandType.Text, strSql).Tables(0).Rows(0)(0)

                            'If intModno > 0 Then



                            strSql = "Insert Into FIU_OWNER_INFO(OWNER_CODE, OWNER_NAME, OCTYPECODE, GENDER, OWNER_FATHER, OWNER_MOTHER, OWNER_SPOUSE, DOB, PHONE_RES1, PHONE_CITY_RES1, COUNTRY_CODE_RES1,MOBILE1, MOBILE1_CITY, COUNTRY_CODE_MOB1,PHONE_OFF1, PHONE_CITY_OFF1, COUNTRY_CODE_OFF1,PPNO, DRIVINGLNO, TIN, BIN, PRES_ADDR, PRES_THANA_CODE, PERM_ADDR, PERM_THANA_CODE, BB_OWNER_CODE, MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS)" & _
                                 "Values(@P_OWNER_CODE, @P_OWNER_NAME, @P_OCTYPECODE, @P_GENDER, @P_OWNER_FATHER, @P_OWNER_MOTHER, @P_OWNER_SPOUSE, @P_DOB, @P_PHONE_RES1, @P_PHONE_CITY_RES1, @P_COUNTRY_CODE_RES1, @P_MOBILE1, @P_MOBILE1_CITY, @P_COUNTRY_CODE_MOB1, @P_PHONE_OFF1, @P_PHONE_CITY_OFF1, @P_COUNTRY_CODE_OFF1,@P_PPNO, @P_DRIVINGLNO, @P_TIN, @P_BIN, @P_PRES_ADDR, @P_PRES_THANA_CODE, @P_PERM_ADDR, @P_PERM_THANA_CODE, @P_BB_OWNER_CODE, " & intModno.ToString() & ",@P_Input_By,getdate(),0,'U')"


                            Dim commProc As DbCommand = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()


                            db.AddInParameter(commProc, "@P_OWNER_CODE", DbType.String, OwnerCode)
                            db.AddInParameter(commProc, "@P_OWNER_NAME", DbType.String, dgView.Rows(i).Cells(11).Value + " " + dgView.Rows(i).Cells(13).Value)
                            db.AddInParameter(commProc, "@P_OCTYPECODE", DbType.String, dgView.Rows(i).Cells(60).Value)
                            db.AddInParameter(commProc, "@P_GENDER", DbType.String, dgView.Rows(i).Cells(9).Value)
                            db.AddInParameter(commProc, "@P_OWNER_FATHER", DbType.String, dgView.Rows(i).Cells(14).Value)
                            db.AddInParameter(commProc, "@P_OWNER_MOTHER", DbType.String, dgView.Rows(i).Cells(15).Value)
                            db.AddInParameter(commProc, "@P_OWNER_SPOUSE", DbType.String, dgView.Rows(i).Cells(16).Value)

                            db.AddInParameter(commProc, "@P_DOB", DbType.DateTime, dgView.Rows(i).Cells(17).Value)

                            db.AddInParameter(commProc, "@P_PHONE_RES1", DbType.String, dgView.Rows(i).Cells(61).Value)
                            db.AddInParameter(commProc, "@P_PHONE_CITY_RES1", DbType.String, dgView.Rows(i).Cells(62).Value)
                            db.AddInParameter(commProc, "@P_COUNTRY_CODE_RES1", DbType.String, dgView.Rows(i).Cells(63).Value)
                            'db.AddInParameter(commProc, "@P_PHONE_RES2", DbType.String, "")
                            'db.AddInParameter(commProc, "@P_PHONE_CITY_RES2", DbType.String, "")
                            'db.AddInParameter(commProc, "@P_COUNTRY_CODE_RES2", DbType.String, "")
                            db.AddInParameter(commProc, "@P_MOBILE1", DbType.String, dgView.Rows(i).Cells(64).Value)
                            db.AddInParameter(commProc, "@P_MOBILE1_CITY", DbType.String, dgView.Rows(i).Cells(65).Value)
                            db.AddInParameter(commProc, "@P_COUNTRY_CODE_MOB1", DbType.String, dgView.Rows(i).Cells(66).Value)
                            'db.AddInParameter(commProc, "@P_MOBILE2", DbType.String, "")
                            'db.AddInParameter(commProc, "@P_MOBILE2_CITY", DbType.String, "")
                            'db.AddInParameter(commProc, "@P_COUNTRY_CODE_MOB2", DbType.String, "")
                            db.AddInParameter(commProc, "@P_PHONE_OFF1", DbType.String, dgView.Rows(i).Cells(67).Value)
                            db.AddInParameter(commProc, "@P_PHONE_CITY_OFF1", DbType.String, dgView.Rows(i).Cells(68).Value)
                            db.AddInParameter(commProc, "@P_COUNTRY_CODE_OFF1", DbType.String, dgView.Rows(i).Cells(69).Value)
                            'db.AddInParameter(commProc, "@P_PHONE_OFF2", DbType.String, "")
                            'db.AddInParameter(commProc, "@P_PHONE_CITY_OFF2", DbType.String, "")
                            'db.AddInParameter(commProc, "@P_COUNTRY_CODE_OFF2", DbType.String, "")
                            db.AddInParameter(commProc, "@P_PPNO", DbType.String, dgView.Rows(i).Cells(42).Value)
                            db.AddInParameter(commProc, "@P_DRIVINGLNO", DbType.String, dgView.Rows(i).Cells(74).Value)
                            db.AddInParameter(commProc, "@P_TIN", DbType.String, dgView.Rows(i).Cells(58).Value)
                            db.AddInParameter(commProc, "@P_BIN", DbType.String, dgView.Rows(i).Cells(59).Value)
                            db.AddInParameter(commProc, "@P_PRES_ADDR", DbType.String, dgView.Rows(i).Cells(70).Value)
                            db.AddInParameter(commProc, "@P_PRES_THANA_CODE", DbType.String, dgView.Rows(i).Cells(71).Value)
                            db.AddInParameter(commProc, "@P_PERM_ADDR", DbType.String, dgView.Rows(i).Cells(72).Value)
                            db.AddInParameter(commProc, "@P_PERM_THANA_CODE", DbType.String, dgView.Rows(i).Cells(73).Value)
                            db.AddInParameter(commProc, "@P_BB_OWNER_CODE", DbType.String, "")
                            db.AddInParameter(commProc, "@P_Input_By", DbType.String, CommonAppSet.User)


                            db.ExecuteNonQuery(commProc, trans)

                            'End If


                            'GO_Owner_info Update



                            Dim commProcMod As DbCommand = db.GetStoredProcCommand("GO_OwnerInfo_GetMaxMod")

                            commProcMod.Parameters.Clear()

                            db.AddInParameter(commProcMod, "@OWNER_CODE", DbType.String, OwnerCode)

                            intModno = db.ExecuteDataSet(commProcMod, trans).Tables(0).Rows(0)(0)

                            If intModno > 0 Then   'check owner code exist or not

                                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_OwnerInfo_Update")

                                commProc2.Parameters.Clear()


                                db.AddInParameter(commProc2, "@OWNER_CODE", DbType.String, OwnerCode)


                                db.AddInParameter(commProc2, "@TITLE", DbType.String, dgView.Rows(i).Cells(10).Value)
                                db.AddInParameter(commProc2, "@FIRST_NAME", DbType.String, dgView.Rows(i).Cells(11).Value)
                                db.AddInParameter(commProc2, "@MIDDLE_NAME", DbType.String, dgView.Rows(i).Cells(12).Value)

                                db.AddInParameter(commProc2, "@LAST_NAME", DbType.String, dgView.Rows(i).Cells(13).Value)

                                db.AddInParameter(commProc2, "@BIRTH_PLACE", DbType.String, dgView.Rows(i).Cells(44).Value)

                                db.AddInParameter(commProc2, "@SSN", DbType.String, dgView.Rows(i).Cells(41).Value)

                                db.AddInParameter(commProc2, "@ID_NUMBER", DbType.String, dgView.Rows(i).Cells(43).Value)
                                db.AddInParameter(commProc2, "@NATIONALITY1", DbType.String, dgView.Rows(i).Cells(18).Value)
                                db.AddInParameter(commProc2, "@NATIONALITY2", DbType.String, dgView.Rows(i).Cells(19).Value)
                                db.AddInParameter(commProc2, "@NATIONALITY3", DbType.String, dgView.Rows(i).Cells(20).Value)
                                db.AddInParameter(commProc2, "@RESIDENCE", DbType.String, dgView.Rows(i).Cells(21).Value)
                                db.AddInParameter(commProc2, "@EMPLOYER_NAME", DbType.String, dgView.Rows(i).Cells(45).Value)
                                db.AddInParameter(commProc2, "@OCP_CODE", DbType.String, dgView.Rows(i).Cells(22).Value)

                                db.AddInParameter(commProc2, "@EMAIL", DbType.String, "")
                                db.AddInParameter(commProc2, "@EMAIL2", DbType.String, "")
                                db.AddInParameter(commProc2, "@EMAIL3", DbType.String, "")
                                db.AddInParameter(commProc2, "@DECEASED", DbType.String, 0)

                                db.AddInParameter(commProc2, "@DECEASED_DATE", DbType.DateTime, NullHelper.StringToDate(""))


                                db.AddInParameter(commProc2, "@TAX_REG_NUMBER", DbType.String, "")
                                db.AddInParameter(commProc2, "@SOURCE_OF_WEALTH", DbType.String, "")
                                db.AddInParameter(commProc2, "@COMMENTS", DbType.String, "")

                                db.AddInParameter(commProc2, "@MOD_NO", DbType.Int32, intModno)
                                db.AddOutParameter(commProc2, "@RET_MOD_NO", DbType.Int32, 5)

                                'db.AddParameter(commProc, "@PROC_RET_VAL", DbType.Int32, ParameterDirection.ReturnValue, DBNull.Value.ToString(), DataRowVersion.Default, DBNull.Value)

                                db.ExecuteNonQuery(commProc2, trans)

                                _intModno = db.GetParameterValue(commProc2, "@RET_MOD_NO")
                                'result = db.GetParameterValue(commProc, "@PROC_RET_VAL")


                                'Personal Phone


                                Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_OwnerPersonalPhone_Update")

                                commProcSche.Parameters.Clear()

                                db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, PhoneSlno)
                                db.AddInParameter(commProcSche, "@OWNER_CODE", DbType.String, OwnerCode)
                                db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                                db.AddInParameter(commProcSche, "@SLNO", DbType.Int32, PhoneSlno)
                                db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(23).Value)
                                db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(24).Value)
                                db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(25).Value)
                                db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(26).Value)
                                db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(27).Value)
                                db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, _intModno)

                                db.ExecuteNonQuery(commProcSche, trans)


                                'Add Person Address 

                                Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_OwnerPersonalAddress_Update")

                                commProcAdd.Parameters.Clear()

                                db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, AddressSlno)
                                db.AddInParameter(commProcAdd, "@OWNER_CODE", DbType.String, OwnerCode)
                                db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                                db.AddInParameter(commProcAdd, "@SLNO", DbType.Int32, AddressSlno)
                                db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(28).Value)
                                db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(29).Value)
                                db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(30).Value)
                                db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(31).Value)
                                db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(33).Value)
                                db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(32).Value)
                                db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(34).Value)
                                db.AddInParameter(commProcAdd, "@MOD_NO", DbType.Int32, _intModno)

                                db.ExecuteNonQuery(commProcAdd, trans)


                                'Add Identification

                                Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_OwnerIdentification_Update")

                                commProcIdent.Parameters.Clear()

                                db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, IDentSlno)
                                db.AddInParameter(commProcIdent, "@OWNER_CODE", DbType.String, OwnerCode)
                                db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                                db.AddInParameter(commProcIdent, "@SLNO", DbType.Int32, IDentSlno)
                                db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(35).Value)
                                db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(36).Value)
                                db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(37).Value)
                                db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(38).Value)
                                db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(40).Value)
                                db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(39).Value)
                                db.AddInParameter(commProcIdent, "@MOD_NO", DbType.Int32, _intModno)

                                db.ExecuteNonQuery(commProcIdent, trans)

                                ' Employer Phone

                                If (NullHelper.ObjectToString(dgView.Rows(i).Cells(46).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(47).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(49).Value) <> "") Then

                                    Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_OwnerPersonalPhone_Update")

                                    commProcEMPhone.Parameters.Clear()

                                    db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, EmpPhnSlno)
                                    db.AddInParameter(commProcEMPhone, "@OWNER_CODE", DbType.String, OwnerCode)
                                    db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                                    db.AddInParameter(commProcEMPhone, "@SLNO", DbType.Int32, EmpPhnSlno)
                                    db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(46).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(47).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(48).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(49).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(50).Value)
                                    db.AddInParameter(commProcEMPhone, "@MOD_NO", DbType.Int32, _intModno)

                                    db.ExecuteNonQuery(commProcEMPhone, trans)

                                End If


                                ' Add Employer Address 



                                If (NullHelper.ObjectToString(dgView.Rows(i).Cells(51).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(52).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(54).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(55).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(57).Value) <> "") Then

                                    Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_OwnerPersonalAddress_Update")

                                    commProcEMAdd.Parameters.Clear()

                                    db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, EmpAddressSlno)
                                    db.AddInParameter(commProcEMAdd, "@OWNER_CODE", DbType.String, OwnerCode)
                                    db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                                    db.AddInParameter(commProcEMAdd, "@SLNO", DbType.Int32, EmpAddressSlno)
                                    db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(51).Value)
                                    db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(52).Value)
                                    db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(53).Value)
                                    db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(54).Value)
                                    db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(56).Value)
                                    db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(55).Value)
                                    db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(57).Value)
                                    db.AddInParameter(commProcEMAdd, "@MOD_NO", DbType.Int32, _intModno)

                                    db.ExecuteNonQuery(commProcEMAdd, trans)

                                End If


                            Else  'insert owner if not exists

                                Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_OwnerInfo_Add")

                                commProc2.Parameters.Clear()


                                db.AddInParameter(commProc2, "@OWNER_CODE", DbType.String, OwnerCode)


                                db.AddInParameter(commProc2, "@TITLE", DbType.String, dgView.Rows(i).Cells(10).Value)
                                db.AddInParameter(commProc2, "@FIRST_NAME", DbType.String, dgView.Rows(i).Cells(11).Value)
                                db.AddInParameter(commProc2, "@MIDDLE_NAME", DbType.String, dgView.Rows(i).Cells(12).Value)

                                db.AddInParameter(commProc2, "@LAST_NAME", DbType.String, dgView.Rows(i).Cells(13).Value)

                                db.AddInParameter(commProc2, "@BIRTH_PLACE", DbType.String, dgView.Rows(i).Cells(44).Value)

                                db.AddInParameter(commProc2, "@SSN", DbType.String, dgView.Rows(i).Cells(41).Value)

                                db.AddInParameter(commProc2, "@ID_NUMBER", DbType.String, dgView.Rows(i).Cells(43).Value)
                                db.AddInParameter(commProc2, "@NATIONALITY1", DbType.String, dgView.Rows(i).Cells(18).Value)
                                db.AddInParameter(commProc2, "@NATIONALITY2", DbType.String, dgView.Rows(i).Cells(19).Value)
                                db.AddInParameter(commProc2, "@NATIONALITY3", DbType.String, dgView.Rows(i).Cells(20).Value)
                                db.AddInParameter(commProc2, "@RESIDENCE", DbType.String, dgView.Rows(i).Cells(21).Value)
                                db.AddInParameter(commProc2, "@EMPLOYER_NAME", DbType.String, dgView.Rows(i).Cells(45).Value)
                                db.AddInParameter(commProc2, "@OCP_CODE", DbType.String, dgView.Rows(i).Cells(22).Value)

                                db.AddInParameter(commProc2, "@EMAIL", DbType.String, "")
                                db.AddInParameter(commProc2, "@EMAIL2", DbType.String, "")
                                db.AddInParameter(commProc2, "@EMAIL3", DbType.String, "")
                                db.AddInParameter(commProc2, "@DECEASED", DbType.String, 0)

                                db.AddInParameter(commProc2, "@DECEASED_DATE", DbType.DateTime, NullHelper.StringToDate(""))


                                db.AddInParameter(commProc2, "@TAX_REG_NUMBER", DbType.String, "")
                                db.AddInParameter(commProc2, "@SOURCE_OF_WEALTH", DbType.String, "")
                                db.AddInParameter(commProc2, "@COMMENTS", DbType.String, "")


                                db.ExecuteNonQuery(commProc2, trans)


                                'Personal Phone


                                Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_OwnerInfoPhone_Add")

                                commProcSche.Parameters.Clear()

                                db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, PhoneSlno)
                                db.AddInParameter(commProcSche, "@OWNER_CODE", DbType.String, OwnerCode)
                                db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                                db.AddInParameter(commProcSche, "@SLNO", DbType.Int32, PhoneSlno)
                                db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(23).Value)
                                db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(24).Value)
                                db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(25).Value)
                                db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(26).Value)
                                db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(27).Value)

                                db.ExecuteNonQuery(commProcSche, trans)


                                'Add Person Address 

                                Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_OwnerInfoAddress_Add")

                                commProcAdd.Parameters.Clear()

                                db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, AddressSlno)
                                db.AddInParameter(commProcAdd, "@OWNER_CODE", DbType.String, OwnerCode)
                                db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                                db.AddInParameter(commProcAdd, "@SLNO", DbType.Int32, AddressSlno)
                                db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(28).Value)
                                db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(29).Value)
                                db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(30).Value)
                                db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(31).Value)
                                db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(33).Value)
                                db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(32).Value)
                                db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(34).Value)

                                db.ExecuteNonQuery(commProcAdd, trans)


                                'Add Identification

                                Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_OwnerInfoIdentification_Add")

                                commProcIdent.Parameters.Clear()

                                db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, IDentSlno)
                                db.AddInParameter(commProcIdent, "@OWNER_CODE", DbType.String, OwnerCode)
                                db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                                db.AddInParameter(commProcIdent, "@SLNO", DbType.Int32, IDentSlno)
                                db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(35).Value)
                                db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(36).Value)
                                db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(37).Value)
                                db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(38).Value)
                                db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(40).Value)
                                db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(39).Value)

                                db.ExecuteNonQuery(commProcIdent, trans)

                                ' Employer Phone

                                If (NullHelper.ObjectToString(dgView.Rows(i).Cells(46).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(47).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(49).Value) <> "") Then

                                    Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_OwnerInfoPhone_Add")

                                    commProcEMPhone.Parameters.Clear()

                                    db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, EmpPhnSlno)
                                    db.AddInParameter(commProcEMPhone, "@OWNER_CODE", DbType.String, OwnerCode)
                                    db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                                    db.AddInParameter(commProcEMPhone, "@SLNO", DbType.Int32, EmpPhnSlno)
                                    db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(46).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(47).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(48).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(49).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(50).Value)

                                    db.ExecuteNonQuery(commProcEMPhone, trans)

                                End If


                                ' Add Employer Address 

                                If (NullHelper.ObjectToString(dgView.Rows(i).Cells(51).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(52).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(54).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(55).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(57).Value) <> "") Then

                                    Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_OwnerInfoAddress_Add")

                                    commProcEMAdd.Parameters.Clear()

                                    db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, EmpAddressSlno)
                                    db.AddInParameter(commProcEMAdd, "@OWNER_CODE", DbType.String, OwnerCode)
                                    db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                                    db.AddInParameter(commProcEMAdd, "@SLNO", DbType.Int32, EmpAddressSlno)
                                    db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(51).Value)
                                    db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(52).Value)
                                    db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(53).Value)
                                    db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(54).Value)
                                    db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(56).Value)
                                    db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(55).Value)
                                    db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(57).Value)

                                    db.ExecuteNonQuery(commProcEMAdd, trans)



                                End If

                                _intModno = 1


                            End If



                    End If

                    End If

                    'mapping code
                    Dim BankCode As String = "026"
                    Dim dtAcOwnerMap As DataTable

                    If dgView.Rows(i).Cells(1).Value.ToString = "A" Then

                        Dim commProc As DbCommand

                        strSql = "SELECT OWNER_CODE,ACNUMBER,MODNO FROM FIU_TRANS_AC_OWNER WHERE Bank_Code='" & BankCode & "' and Branch_Code='" & dgView.Rows(i).Cells(2).Value & "' and AcNumber='" & dgView.Rows(i).Cells(4).Value & "' AND IS_AUTHORIZED = 0"


                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()

                        dtOwnerInfoMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                        If dtOwnerInfoMap.Rows.Count > 0 Then

                            strSql = "INSERT INTO FIU_TRANS_AC_OWNER(OWNER_CODE, BANK_CODE, BRANCH_CODE, ACNUMBER, EXE_DESIG_CODE, SIGN_AUTHORITY,MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS, ROLE_TYPE,IS_PRIMARY) " & _
                                     "VALUES(@P_OWNER_CODE,@P_BANK_CODE,@P_BRANCH_CODE,@P_ACNUMBER,@P_EXE_DESIG_CODE,@P_SIGN_AUTHORITY,@P_MODNO,@P_INPUT_BY, GETDATE(),0,'U',@P_ROLE_TYPE,@IS_PRIMARY)"


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            db.AddInParameter(commProc, "@P_OWNER_CODE", DbType.String, OwnerCode)
                            db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, BankCode)
                            db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dgView.Rows(i).Cells(2).Value)
                            db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dgView.Rows(i).Cells(4).Value)
                            db.AddInParameter(commProc, "@P_EXE_DESIG_CODE", DbType.String, dgView.Rows(i).Cells(5).Value)
                            db.AddInParameter(commProc, "@P_SIGN_AUTHORITY", DbType.String, dgView.Rows(i).Cells(6).Value)
                            db.AddInParameter(commProc, "@P_ROLE_TYPE", DbType.String, dgView.Rows(i).Cells(7).Value)
                            db.AddInParameter(commProc, "@IS_PRIMARY", DbType.Int32, dgView.Rows(i).Cells(8).Value)

                            db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, CommonAppSet.User)
                            db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, dtOwnerInfoMap.Rows(0)("MODNO"))

                            db.ExecuteNonQuery(commProc, trans)


                        Else

                            strSql = "SELECT OWNER_CODE,ACNUMBER,MODNO FROM FIU_TRANS_AC_OWNER WHERE Bank_Code='" & BankCode & "' and Branch_Code='" & dgView.Rows(i).Cells(2).Value & "' and AcNumber='" & dgView.Rows(i).Cells(4).Value & "' AND STATUS = 'L' "


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            dtAcOwnerMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                            If dtAcOwnerMap.Rows.Count > 0 Then

                                intMod = NullHelper.ToIntNum(dtAcOwnerMap.Rows(0)("MODNO"))


                                strSql = "INSERT INTO FIU_TRANS_AC_OWNER(OWNER_CODE, BANK_CODE, BRANCH_CODE, ACNUMBER, EXE_DESIG_CODE, SIGN_AUTHORITY,MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS, ROLE_TYPE,IS_PRIMARY) " & _
                                         "SELECT OWNER_CODE, BANK_CODE, BRANCH_CODE, ACNUMBER, EXE_DESIG_CODE, SIGN_AUTHORITY, @P_MODNO, @P_INPUT_BY, GETDATE(),0,'U', ROLE_TYPE, IS_PRIMARY " & _
                                         "FROM FIU_TRANS_AC_OWNER " & _
                                         " WHERE Bank_Code='" & BankCode & "' and Branch_Code='" & dgView.Rows(i).Cells(2).Value & "' and AcNumber='" & dgView.Rows(i).Cells(4).Value & "' AND STATUS = 'L' "


                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, CommonAppSet.User)
                                db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)

                                db.ExecuteNonQuery(commProc, trans)


                                strSql = "INSERT INTO FIU_TRANS_AC_OWNER(OWNER_CODE, BANK_CODE, BRANCH_CODE, ACNUMBER, EXE_DESIG_CODE, SIGN_AUTHORITY,MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS, ROLE_TYPE,IS_PRIMARY) " & _
                                     "VALUES(@P_OWNER_CODE,@P_BANK_CODE,@P_BRANCH_CODE,@P_ACNUMBER,@P_EXE_DESIG_CODE,@P_SIGN_AUTHORITY,@P_MODNO,@P_INPUT_BY, GETDATE(),0,'U',@P_ROLE_TYPE,@IS_PRIMARY)"


                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@P_OWNER_CODE", DbType.String, OwnerCode)
                                db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, BankCode)
                                db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dgView.Rows(i).Cells(2).Value)
                                db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dgView.Rows(i).Cells(4).Value)
                                db.AddInParameter(commProc, "@P_EXE_DESIG_CODE", DbType.String, dgView.Rows(i).Cells(5).Value)
                                db.AddInParameter(commProc, "@P_SIGN_AUTHORITY", DbType.String, dgView.Rows(i).Cells(6).Value)
                                db.AddInParameter(commProc, "@P_ROLE_TYPE", DbType.String, dgView.Rows(i).Cells(7).Value)
                                db.AddInParameter(commProc, "@IS_PRIMARY", DbType.Int32, dgView.Rows(i).Cells(8).Value)

                                db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, CommonAppSet.User)
                                db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)

                                db.ExecuteNonQuery(commProc, trans)

                            Else

                                strSql = "INSERT INTO FIU_TRANS_AC_OWNER(OWNER_CODE, BANK_CODE, BRANCH_CODE, ACNUMBER, EXE_DESIG_CODE, SIGN_AUTHORITY,MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS, ROLE_TYPE,IS_PRIMARY) " & _
                                    "VALUES(@P_OWNER_CODE,@P_BANK_CODE,@P_BRANCH_CODE,@P_ACNUMBER,@P_EXE_DESIG_CODE,@P_SIGN_AUTHORITY,@P_MODNO,@P_INPUT_BY, GETDATE(),0,'U',@P_ROLE_TYPE,@IS_PRIMARY)"


                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@P_OWNER_CODE", DbType.String, OwnerCode)
                                db.AddInParameter(commProc, "@P_BANK_CODE", DbType.String, BankCode)
                                db.AddInParameter(commProc, "@P_BRANCH_CODE", DbType.String, dgView.Rows(i).Cells(2).Value)
                                db.AddInParameter(commProc, "@P_ACNUMBER", DbType.String, dgView.Rows(i).Cells(4).Value)
                                db.AddInParameter(commProc, "@P_EXE_DESIG_CODE", DbType.String, dgView.Rows(i).Cells(5).Value)
                                db.AddInParameter(commProc, "@P_SIGN_AUTHORITY", DbType.String, dgView.Rows(i).Cells(6).Value)
                                db.AddInParameter(commProc, "@P_ROLE_TYPE", DbType.String, dgView.Rows(i).Cells(7).Value)
                                db.AddInParameter(commProc, "@IS_PRIMARY", DbType.Int32, dgView.Rows(i).Cells(8).Value)

                                db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, CommonAppSet.User)
                                db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, 1)

                                db.ExecuteNonQuery(commProc, trans)


                            End If


                        End If

                    ElseIf (dgView.Rows(i).Cells(1).Value.ToString = "R") Then

                        Dim commProc As DbCommand

                        strSql = "SELECT OWNER_CODE,ACNUMBER,MODNO FROM FIU_TRANS_AC_OWNER WHERE OWNER_CODE='" & OwnerCode & "' And Bank_Code='" & BankCode & "' and Branch_Code='" & dgView.Rows(i).Cells(2).Value & "' and AcNumber='" & dgView.Rows(i).Cells(4).Value & "' AND IS_AUTHORIZED = 0"


                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()

                        dtOwnerInfoMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                        If dtOwnerInfoMap.Rows.Count > 0 Then

                            intMod = NullHelper.ToIntNum(dtOwnerInfoMap.Rows(0)("MODNO"))

                            strSql = "DELETE FROM FIU_TRANS_AC_OWNER WHERE OWNER_CODE='" & OwnerCode & "' AND  Bank_Code='" & BankCode & "' and Branch_Code='" & dgView.Rows(i).Cells(2).Value & "' and AcNumber='" & dgView.Rows(i).Cells(4).Value & "' And MODNO='" & intMod & "' AND IS_AUTHORIZED = 0"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            db.ExecuteNonQuery(commProc, trans)

                        Else

                            strSql = "SELECT OWNER_CODE,ACNUMBER,MODNO FROM FIU_TRANS_AC_OWNER WHERE OWNER_CODE='" & OwnerCode & "' AND Bank_Code='" & BankCode & "' and Branch_Code='" & dgView.Rows(i).Cells(2).Value & "' and AcNumber='" & dgView.Rows(i).Cells(4).Value & "' AND STATUS = 'L' "


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            dtAcOwnerMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                            If dtAcOwnerMap.Rows.Count > 0 Then

                                intMod = NullHelper.ToIntNum(dtAcOwnerMap.Rows(0)("MODNO"))


                                strSql = "INSERT INTO FIU_TRANS_AC_OWNER(OWNER_CODE, BANK_CODE, BRANCH_CODE, ACNUMBER, EXE_DESIG_CODE, SIGN_AUTHORITY,MODNO, INPUT_BY, INPUT_DATETIME, IS_AUTHORIZED,  STATUS, ROLE_TYPE,IS_PRIMARY) " & _
                                         "SELECT OWNER_CODE, BANK_CODE, BRANCH_CODE, ACNUMBER, EXE_DESIG_CODE, SIGN_AUTHORITY, @P_MODNO, @P_INPUT_BY, GETDATE(),0,'U', ROLE_TYPE, IS_PRIMARY " & _
                                         "FROM FIU_TRANS_AC_OWNER " & _
                                         " WHERE Bank_Code='" & BankCode & "' and Branch_Code='" & dgView.Rows(i).Cells(2).Value & "' and AcNumber='" & dgView.Rows(i).Cells(4).Value & "' AND STATUS = 'L' "


                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, CommonAppSet.User)
                                db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)

                                db.ExecuteNonQuery(commProc, trans)


                                strSql = "DELETE FROM FIU_TRANS_AC_OWNER WHERE OWNER_CODE='" & OwnerCode & "' AND Bank_Code='" & BankCode & "' and Branch_Code='" & dgView.Rows(i).Cells(2).Value & "' and AcNumber='" & dgView.Rows(i).Cells(4).Value & "' And MODNO='" & intMod + 1 & "' AND IS_AUTHORIZED = 0"

                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()

                                db.ExecuteNonQuery(commProc, trans)


                            End If




                        End If



                    Else

                    End If  'mapping end


                Else 'same group 

                    PhoneSlno = PhoneSlno + 1
                    AddressSlno = AddressSlno + 1
                    EmpPhnSlno = EmpPhnSlno + 1
                    EmpAddressSlno = EmpAddressSlno + 1
                    IDentSlno = IDentSlno + 1


                    'Personal Phone

                    If (NullHelper.ObjectToString(dgView.Rows(i).Cells(23).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(24).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(26).Value) <> "") Then

                        Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_OwnerPersonalPhone_Update")

                        commProcSche.Parameters.Clear()

                        db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, PhoneSlno)
                        db.AddInParameter(commProcSche, "@OWNER_CODE", DbType.String, OwnerCode)
                        db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                        db.AddInParameter(commProcSche, "@SLNO", DbType.Int32, PhoneSlno)
                        db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(23).Value)
                        db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(24).Value)
                        db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(25).Value)
                        db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(26).Value)
                        db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(27).Value)
                        db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, _intModno)

                        db.ExecuteNonQuery(commProcSche, trans)

                    End If

                    'Add Person Address 

                    If (NullHelper.ObjectToString(dgView.Rows(i).Cells(28).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(29).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(31).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(32).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(34).Value) <> "") Then

                        Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_OwnerPersonalAddress_Update")

                        commProcAdd.Parameters.Clear()

                        db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, AddressSlno)
                        db.AddInParameter(commProcAdd, "@OWNER_CODE", DbType.String, OwnerCode)
                        db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                        db.AddInParameter(commProcAdd, "@SLNO", DbType.Int32, AddressSlno)
                        db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(28).Value)
                        db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(29).Value)
                        db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(30).Value)
                        db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(31).Value)
                        db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(33).Value)
                        db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(32).Value)
                        db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(34).Value)
                        db.AddInParameter(commProcAdd, "@MOD_NO", DbType.Int32, _intModno)

                        db.ExecuteNonQuery(commProcAdd, trans)

                    End If

                    'Add Identification

                    If (NullHelper.ObjectToString(dgView.Rows(i).Cells(35).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(36).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(39).Value) <> "") Then

                        Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_OwnerIdentification_Update")

                        commProcIdent.Parameters.Clear()

                        db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, IDentSlno)
                        db.AddInParameter(commProcIdent, "@OWNER_CODE", DbType.String, OwnerCode)
                        db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                        db.AddInParameter(commProcIdent, "@SLNO", DbType.Int32, IDentSlno)
                        db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(35).Value)
                        db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(36).Value)
                        db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(37).Value)
                        db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(38).Value)
                        db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(40).Value)
                        db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(39).Value)
                        db.AddInParameter(commProcIdent, "@MOD_NO", DbType.Int32, _intModno)

                        db.ExecuteNonQuery(commProcIdent, trans)

                    End If

                    ' Employer Phone

                    If (NullHelper.ObjectToString(dgView.Rows(i).Cells(46).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(47).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(49).Value) <> "") Then

                        Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_OwnerPersonalPhone_Update")

                        commProcEMPhone.Parameters.Clear()

                        db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, EmpPhnSlno)
                        db.AddInParameter(commProcEMPhone, "@OWNER_CODE", DbType.String, OwnerCode)
                        db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                        db.AddInParameter(commProcEMPhone, "@SLNO", DbType.Int32, EmpPhnSlno)
                        db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(46).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(47).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(48).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(49).Value)
                        db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(50).Value)
                        db.AddInParameter(commProcEMPhone, "@MOD_NO", DbType.Int32, _intModno)

                        db.ExecuteNonQuery(commProcEMPhone, trans)

                    End If


                    ' Add Employer Address 

                    If (NullHelper.ObjectToString(dgView.Rows(i).Cells(51).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(52).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(54).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(55).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(57).Value) <> "") Then

                        Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_OwnerPersonalAddress_Update")

                        commProcEMAdd.Parameters.Clear()

                        db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, EmpAddressSlno)
                        db.AddInParameter(commProcEMAdd, "@OWNER_CODE", DbType.String, OwnerCode)
                        db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                        db.AddInParameter(commProcEMAdd, "@SLNO", DbType.Int32, EmpAddressSlno)
                        db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(51).Value)
                        db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(52).Value)
                        db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(53).Value)
                        db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(54).Value)
                        db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(56).Value)
                        db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(55).Value)
                        db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(57).Value)
                        db.AddInParameter(commProcEMAdd, "@MOD_NO", DbType.Int32, _intModno)

                        db.ExecuteNonQuery(commProcEMAdd, trans)

                    End If



                End If

                RecGroup = dgView.Rows(i).Cells(0).Value

                '----- end of i loop

            Next


            tStatus = TransState.Add




            trans.Commit()

           
        End Using


        log_message = " Uploaded : Owner Information "
        Logger.system_log(log_message)



        Return tStatus

    End Function



    Private Sub FrmImportOwnerInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

        
        ' btnPrepareOwner.Enabled = False

        ReadExcel()
        CheckValidation()

        'If flagRequireField = True Then
        '    btnPrepareOwner.Enabled = False
        'Else
        '    btnPrepareOwner.Enabled = True
        'End If


    End Sub

    Private Sub btnPrepareOwner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrepareOwner.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try
            If MessageBox.Show("Do you really want to Save?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                If CheckValidData() Then

                    tState = SaveData()

                    If tState = TransState.Add Then

                        lblToolStatus.Text = "!! Information Updated Successfully !!"

                        MessageBox.Show("Information Updated Successfull." & Environment.NewLine & _
                                        "** Separate authorization needed from Owner Info,  GoAml Owner info Form", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

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