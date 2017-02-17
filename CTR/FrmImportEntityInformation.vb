'
'Author             : Fahad Khan
'Purpose            : Import Entity Information
'Creation date      : 02-nov-2013
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

Public Class FrmImportEntityInformation

    Dim _formName As String = "MaintenanceGoEntityInfoImport"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""
    Dim _debugRow As Integer = 0
    Dim flagRequireField As Boolean = False
    Dim flagInvalidField As Boolean = False
    Dim flagWarning As Boolean = False
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

                If (i = dgView.Rows.Count + 1) Then
                    dgView.Rows.Add()
                End If

                dgView.Item(0, i - 2).Value = sheet.Range("A" & i.ToString()).Value2  ' Entity Group
                dgView.Item(1, i - 2).Value = sheet.Range("B" & i.ToString()).Value2  ' Mapping Flag
                dgView.Item(2, i - 2).Value = sheet.Range("C" & i.ToString()).Value2  ' account number
                dgView.Item(3, i - 2).Value = sheet.Range("D" & i.ToString()).Value2  ' entity person role
                dgView.Item(4, i - 2).Value = sheet.Range("E" & i.ToString()).Value2  ' entity id
                dgView.Item(5, i - 2).Value = sheet.Range("F" & i.ToString()).Value2  ' name
                dgView.Item(6, i - 2).Value = sheet.Range("G" & i.ToString()).Value2  ' commercial name
                dgView.Item(7, i - 2).Value = sheet.Range("H" & i.ToString()).Value2  ' inc legal form type
                dgView.Item(8, i - 2).Value = sheet.Range("I" & i.ToString()).Value2  ' inc number
                dgView.Item(9, i - 2).Value = sheet.Range("J" & i.ToString()).Value2  ' business
                dgView.Item(10, i - 2).Value = sheet.Range("K" & i.ToString()).Value2   ' phone contact type
                dgView.Item(11, i - 2).Value = sheet.Range("L" & i.ToString()).Value2  ' phone communication type
                dgView.Item(12, i - 2).Value = sheet.Range("M" & i.ToString()).Value2 ' phone prefix
                dgView.Item(13, i - 2).Value = sheet.Range("N" & i.ToString()).Value2 ' phone number
                dgView.Item(14, i - 2).Value = sheet.Range("O" & i.ToString()).Value2 ' phone Extension
                dgView.Item(15, i - 2).Value = sheet.Range("P" & i.ToString()).Value2 ' address type code
                dgView.Item(16, i - 2).Value = sheet.Range("Q" & i.ToString()).Value2 ' address
                dgView.Item(17, i - 2).Value = sheet.Range("R" & i.ToString()).Value2 ' thana/town
                dgView.Item(18, i - 2).Value = sheet.Range("S" & i.ToString()).Value2 ' city/district
                dgView.Item(19, i - 2).Value = sheet.Range("T" & i.ToString()).Value2 ' zip
                dgView.Item(20, i - 2).Value = sheet.Range("U" & i.ToString()).Value2 ' country code
                dgView.Item(21, i - 2).Value = sheet.Range("V" & i.ToString()).Value2 ' state/division
                dgView.Item(22, i - 2).Value = sheet.Range("W" & i.ToString()).Value2 ' Email
                dgView.Item(23, i - 2).Value = sheet.Range("X" & i.ToString()).Value2 ' url
                dgView.Item(24, i - 2).Value = sheet.Range("Y" & i.ToString()).Value2 ' inc state/district
                dgView.Item(25, i - 2).Value = sheet.Range("Z" & i.ToString()).Value2 ' inc Country
                dgView.Item(26, i - 2).Value = sheet.Range("AA" & i.ToString()).Value ' inc date
                dgView.Item(27, i - 2).Value = sheet.Range("AB" & i.ToString()).Value2 ' business close
                dgView.Item(28, i - 2).Value = sheet.Range("AC" & i.ToString()).Value ' business close date
                dgView.Item(29, i - 2).Value = sheet.Range("AD" & i.ToString()).Value2 ' Tax Number
                dgView.Item(30, i - 2).Value = sheet.Range("AE" & i.ToString()).Value2 ' tax regi number
                dgView.Item(31, i - 2).Value = sheet.Range("AF" & i.ToString()).Value2 ' comments
                dgView.Item(32, i - 2).Value = sheet.Range("AG" & i.ToString()).Value2 ' Director Group
                dgView.Item(33, i - 2).Value = sheet.Range("AH" & i.ToString()).Value2 ' director id
                dgView.Item(34, i - 2).Value = sheet.Range("AI" & i.ToString()).Value2 ' gender
                dgView.Item(35, i - 2).Value = sheet.Range("AJ" & i.ToString()).Value2 ' title
                dgView.Item(36, i - 2).Value = sheet.Range("AK" & i.ToString()).Value2 ' first name
                dgView.Item(37, i - 2).Value = sheet.Range("AL" & i.ToString()).Value2 ' middle name
                dgView.Item(38, i - 2).Value = sheet.Range("AM" & i.ToString()).Value2 ' last name
                dgView.Item(39, i - 2).Value = sheet.Range("AN" & i.ToString()).Value2 ' spouse
                dgView.Item(40, i - 2).Value = sheet.Range("AO" & i.ToString()).Value ' birthdate
                dgView.Item(41, i - 2).Value = sheet.Range("AP" & i.ToString()).Value2 ' birthplace
                dgView.Item(42, i - 2).Value = sheet.Range("AQ" & i.ToString()).Value2 ' mothers name
                dgView.Item(43, i - 2).Value = sheet.Range("AR" & i.ToString()).Value2 ' fathers name
                dgView.Item(44, i - 2).Value = sheet.Range("AS" & i.ToString()).Value2 ' national id 
                dgView.Item(45, i - 2).Value = sheet.Range("AT" & i.ToString()).Value2 ' passport number
                dgView.Item(46, i - 2).Value = sheet.Range("AU" & i.ToString()).Value2 ' passport country
                dgView.Item(47, i - 2).Value = sheet.Range("AV" & i.ToString()).Value2 ' Birth Regi no
                dgView.Item(48, i - 2).Value = sheet.Range("AW" & i.ToString()).Value2 ' Nationality1
                dgView.Item(49, i - 2).Value = sheet.Range("AX" & i.ToString()).Value2 ' Nationality2
                dgView.Item(50, i - 2).Value = sheet.Range("AY" & i.ToString()).Value2 ' Nationality3
                dgView.Item(51, i - 2).Value = sheet.Range("AZ" & i.ToString()).Value2 ' Residence
                dgView.Item(52, i - 2).Value = sheet.Range("BA" & i.ToString()).Value2 ' phone contact type
                dgView.Item(53, i - 2).Value = sheet.Range("BB" & i.ToString()).Value2 ' phone communication type
                dgView.Item(54, i - 2).Value = sheet.Range("BC" & i.ToString()).Value2 ' country prefix
                dgView.Item(55, i - 2).Value = sheet.Range("BD" & i.ToString()).Value2 ' phone number
                dgView.Item(56, i - 2).Value = sheet.Range("BE" & i.ToString()).Value2 ' phone Extension
                dgView.Item(57, i - 2).Value = sheet.Range("BF" & i.ToString()).Value2 ' Address type
                dgView.Item(58, i - 2).Value = sheet.Range("BG" & i.ToString()).Value2 ' Address
                dgView.Item(59, i - 2).Value = sheet.Range("BH" & i.ToString()).Value2 ' Town/ Thana
                dgView.Item(60, i - 2).Value = sheet.Range("BI" & i.ToString()).Value2 ' City/District
                dgView.Item(61, i - 2).Value = sheet.Range("BJ" & i.ToString()).Value2 ' Zip
                dgView.Item(62, i - 2).Value = sheet.Range("BK" & i.ToString()).Value2 ' country
                dgView.Item(63, i - 2).Value = sheet.Range("BL" & i.ToString()).Value2 ' State/Division
                dgView.Item(64, i - 2).Value = sheet.Range("BM" & i.ToString()).Value2 ' identification Type
                dgView.Item(65, i - 2).Value = sheet.Range("BN" & i.ToString()).Value2 ' identification Number
                dgView.Item(66, i - 2).Value = sheet.Range("BO" & i.ToString()).Value ' issue date
                dgView.Item(67, i - 2).Value = sheet.Range("BP" & i.ToString()).Value ' expiry date
                dgView.Item(68, i - 2).Value = sheet.Range("BQ" & i.ToString()).Value2 ' issue country
                dgView.Item(69, i - 2).Value = sheet.Range("BR" & i.ToString()).Value2 ' issued by
                dgView.Item(70, i - 2).Value = sheet.Range("BS" & i.ToString()).Value2 ' Email
                dgView.Item(71, i - 2).Value = sheet.Range("BT" & i.ToString()).Value2 ' occupation
                dgView.Item(72, i - 2).Value = sheet.Range("BU" & i.ToString()).Value2 ' Employer name
                dgView.Item(73, i - 2).Value = sheet.Range("BV" & i.ToString()).Value2 ' phone contact type
                dgView.Item(74, i - 2).Value = sheet.Range("BW" & i.ToString()).Value2 ' phone communication type
                dgView.Item(75, i - 2).Value = sheet.Range("BX" & i.ToString()).Value2 ' prefix
                dgView.Item(76, i - 2).Value = sheet.Range("BY" & i.ToString()).Value2 ' phone number
                dgView.Item(77, i - 2).Value = sheet.Range("BZ" & i.ToString()).Value2 ' extension
                dgView.Item(78, i - 2).Value = sheet.Range("CA" & i.ToString()).Value2 ' Address type
                dgView.Item(79, i - 2).Value = sheet.Range("CB" & i.ToString()).Value2 ' address
                dgView.Item(80, i - 2).Value = sheet.Range("CC" & i.ToString()).Value2 ' town/thana
                dgView.Item(81, i - 2).Value = sheet.Range("CD" & i.ToString()).Value2 ' city/district
                dgView.Item(82, i - 2).Value = sheet.Range("CE" & i.ToString()).Value2 ' country code
                dgView.Item(83, i - 2).Value = sheet.Range("CF" & i.ToString()).Value2 ' state/division
                dgView.Item(84, i - 2).Value = sheet.Range("CG" & i.ToString()).Value2 ' Deceased 
                dgView.Item(85, i - 2).Value = sheet.Range("CH" & i.ToString()).Value ' Deceased date
                dgView.Item(86, i - 2).Value = sheet.Range("CI" & i.ToString()).Value2 ' Tax number
                dgView.Item(87, i - 2).Value = sheet.Range("CJ" & i.ToString()).Value2 ' Tax regi no
                dgView.Item(88, i - 2).Value = sheet.Range("CK" & i.ToString()).Value2 ' source of wealth
                dgView.Item(89, i - 2).Value = sheet.Range("CL" & i.ToString()).Value2 ' Comments

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

                    If NullHelper.ObjectToString(row.Cells(2).Value).Trim() = "" Then

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(3).Value).Trim() = "" Then

                        flagRequireField = True

                    End If


                    If NullHelper.ObjectToString(row.Cells(5).Value).Trim() = "" Then 
                        flagRequireField = True

                    End If

                    If TypeChecker.IsContained("entity_person_role_type", NullHelper.ObjectToString(row.Cells(3).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Person Role Type " + """" + row.Cells(3).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("legal_form_type", NullHelper.ObjectToString(row.Cells(7).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Incorporation Legal Form Type " + """" + row.Cells(7).Value.ToString() + """"
                        flagRequireField = True

                    End If

                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(10).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Entity Phone Contact Type " + """" + row.Cells(10).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(11).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Entity Phone Communication Type " + """" + row.Cells(11).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(15).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Entity Address Type " + """" + row.Cells(15).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(20).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Entity Country Type Code " + """" + row.Cells(20).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(25).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Incorporation Country Code " + """" + row.Cells(25).Value.ToString() + """"
                        flagRequireField = True
                    End If


                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(52).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Phone Contact Type " + """" + row.Cells(52).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(53).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Phone Communication Type " + """" + row.Cells(53).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(57).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Address Type " + """" + row.Cells(57).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(62).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Address Country Type " + """" + row.Cells(62).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("identifier_type", NullHelper.ObjectToString(row.Cells(64).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Identification Type " + """" + row.Cells(64).Value.ToString() + """"
                        flagRequireField = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(68).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Identification Country Type " + """" + row.Cells(68).Value.ToString() + """"
                        flagRequireField = True

                    End If

                    'employer phone

                    If NullHelper.ObjectToString(row.Cells(73).Value) <> "" And NullHelper.ObjectToString(row.Cells(74).Value) <> "" And NullHelper.ObjectToString(row.Cells(76).Value) <> "" Then ' phone contact type

                        'flagWarning = False

                    ElseIf NullHelper.ObjectToString(row.Cells(73).Value) = "" And NullHelper.ObjectToString(row.Cells(74).Value) = "" And NullHelper.ObjectToString(row.Cells(76).Value) = "" Then ' phone contact type
                        'flagWarning = False

                    Else
                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Director Phone Field Missing, Phone Data might not be saved properly "
                    End If

                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(73).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Employer Phone Contact Type " + """" + row.Cells(73).Value.ToString() + """"
                        flagWarning = True
                    End If

                    If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(74).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Employer Phone Communication Type " + """" + row.Cells(74).Value.ToString() + """"
                        flagWarning = True
                    End If

                    ' employer address

                    If NullHelper.ObjectToString(row.Cells(78).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(79).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(81).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(82).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(83).Value).Trim() <> "" Then ' phone contact type

                        'flagWarning = False
                    ElseIf NullHelper.ObjectToString(row.Cells(78).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(79).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(81).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(82).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(83).Value).Trim() = "" Then ' phone contact type
                        'flagWarning = False
                    Else
                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Director Employer Address Field Missing, Data might not be saved properly "
                    End If


                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(78).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid  Director Employer Address Type " + """" + row.Cells(78).Value.ToString() + """"
                        flagWarning = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(82).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Employer Address Country Type " + """" + row.Cells(82).Value.ToString() + """"
                        flagWarning = True
                    End If




                    If NullHelper.ObjectToString(row.Cells(8).Value).Trim() = "" Then

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(9).Value).Trim() = "" Then

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(10).Value).Trim() = "" Then

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(11).Value).Trim() = "" Then

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(13).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If


                    If NullHelper.ObjectToString(row.Cells(15).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(16).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(18).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(20).Value).Trim() = "" Then

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(21).Value).Trim() = "" Then

                        flagRequireField = True

                    End If
                    If NullHelper.ObjectToString(row.Cells(24).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(25).Value).Trim() = "" Then '  

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(32).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(36).Value).Trim() = "" Then ' city

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(38).Value).Trim() = "" Then ' country

                        flagRequireField = True

                    End If



                    If NullHelper.ObjectToString(row.Cells(39).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(42).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(43).Value).Trim() = "" Then
                        flagRequireField = True


                    End If

                    If NullHelper.ObjectToString(row.Cells(40).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(44).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(45).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(47).Value).Trim() = "" Then  ' id

                        flagRequireField = True

                    End If

                    'If NullHelper.ObjectToString(row.Cells(44).Value).Trim() <> "" And Len(NullHelper.ObjectToString(row.Cells(44).Value).Trim()) <> 17 And Len(NullHelper.ObjectToString(row.Cells(44).Value).Trim()) <> 13 Then

                    '    strErrMsg = strErrMsg + " | Invalid National ID Number " + """" + row.Cells(44).Value.ToString() + """" + " It might be 13 or 17 digits "
                    '    flagRequireField = True


                    'End If


                    If NullHelper.ObjectToString(row.Cells(52).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(53).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(55).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If
                    If NullHelper.ObjectToString(row.Cells(57).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(58).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(60).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(62).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(63).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(64).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(65).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    If NullHelper.ObjectToString(row.Cells(68).Value).Trim() = "" Then ' 

                        flagRequireField = True

                    End If

                    'If NullHelper.ObjectToString(row.Cells(73).Value).Trim() = "" Then ' 

                    '    flagRequireField = True

                    'End If

                Else

                    'entity Phone

                    If NullHelper.ObjectToString(row.Cells(10).Value) <> "" And NullHelper.ObjectToString(row.Cells(11).Value) <> "" And NullHelper.ObjectToString(row.Cells(13).Value) <> "" Then
                        ''flagWarning = False

                    ElseIf NullHelper.ObjectToString(row.Cells(10).Value) = "" And NullHelper.ObjectToString(row.Cells(11).Value) = "" And NullHelper.ObjectToString(row.Cells(13).Value) = "" Then
                        'flagWarning = False
                    Else
                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Entity Phone Field Missing, Phone Data might not be saved properly "
                    End If

                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(10).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Entity Phone Contact Type " + """" + row.Cells(10).Value.ToString() + """"
                        flagWarning = True
                    End If

                    If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(11).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Entity Phone Communication Type " + """" + row.Cells(11).Value.ToString() + """"
                        flagWarning = True
                    End If

                    'enriry Address

                    If NullHelper.ObjectToString(row.Cells(15).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(16).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(18).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(20).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(21).Value).Trim() <> "" Then ' phone contact type

                        'flagWarning = False
                    ElseIf NullHelper.ObjectToString(row.Cells(15).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(16).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(18).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(20).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(21).Value).Trim() = "" Then ' phone contact type

                        ' flagWarning = False

                    Else
                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Entity Address Field Missing, Address Data might not be saved properly "
                    End If

                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(15).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Entity Address Type " + """" + row.Cells(15).Value.ToString() + """"
                        flagWarning = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(20).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Entity Country Type Code " + """" + row.Cells(20).Value.ToString() + """"
                        flagWarning = True
                    End If


                    'If NullHelper.ObjectToString(row.Cells(23).Value).Trim() = "" Then ' phone contact type

                    '    flagRequireField = True

                    'End If


                    'Director phone

                    If NullHelper.ObjectToString(row.Cells(52).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(53).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(55).Value).Trim() <> "" Then ' phone contact type

                        'flagWarning = False
                    ElseIf NullHelper.ObjectToString(row.Cells(52).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(53).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(55).Value).Trim() = "" Then ' phone contact type
                        'flagWarning = False
                    Else
                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Director Phone Field Missing, Phone Data might not be saved properly "
                    End If



                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(52).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Phone Contact Type " + """" + row.Cells(52).Value.ToString() + """"
                        flagWarning = True
                    End If

                    If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(53).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Phone Communication Type " + """" + row.Cells(53).Value.ToString() + """"
                        flagWarning = True
                    End If

                    'Director Address

                    If NullHelper.ObjectToString(row.Cells(57).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(58).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(60).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(62).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(63).Value).Trim() <> "" Then ' phone contact type

                        'flagWarning = False
                    ElseIf NullHelper.ObjectToString(row.Cells(57).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(58).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(60).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(62).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(63).Value).Trim() = "" Then ' phone contact type
                        'flagWarning = False
                    Else
                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Director Address Field Missing, Address Data might not be saved properly "
                    End If


                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(57).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Address Type " + """" + row.Cells(57).Value.ToString() + """"
                        flagWarning = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(62).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Address Country Type " + """" + row.Cells(62).Value.ToString() + """"
                        flagWarning = True
                    End If


                    'Director identification

                    If NullHelper.ObjectToString(row.Cells(64).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(65).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(68).Value).Trim() <> "" Then ' phone contact type

                        'flagWarning = False
                    ElseIf NullHelper.ObjectToString(row.Cells(64).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(65).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(68).Value).Trim() = "" Then ' phone contact type
                        'flagWarning = False
                    Else
                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Director Identification Field Missing, Data might not be saved properly "
                    End If


                    If TypeChecker.IsContained("identifier_type", NullHelper.ObjectToString(row.Cells(64).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Identification Type " + """" + row.Cells(64).Value.ToString() + """"
                        flagWarning = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(68).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Identification Country Type " + """" + row.Cells(68).Value.ToString() + """"
                        flagWarning = True

                    End If


                    'director Employer phone

                    If NullHelper.ObjectToString(row.Cells(73).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(74).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(76).Value).Trim() <> "" Then ' phone contact type

                        'flagWarning = False
                    ElseIf NullHelper.ObjectToString(row.Cells(73).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(74).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(76).Value).Trim() = "" Then ' phone contact type
                        'flagWarning = False
                    Else
                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Director Phone Field Missing, Phone Data might not be saved properly "
                    End If



                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(73).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Employer Phone Contact Type " + """" + row.Cells(73).Value.ToString() + """"
                        flagWarning = True
                    End If

                    If TypeChecker.IsContained("communication_type", NullHelper.ObjectToString(row.Cells(74).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Employer Phone Communication Type " + """" + row.Cells(74).Value.ToString() + """"
                        flagWarning = True
                    End If

                   


                    'director Employer address

                    If NullHelper.ObjectToString(row.Cells(78).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(79).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(81).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(82).Value).Trim() <> "" And NullHelper.ObjectToString(row.Cells(83).Value).Trim() <> "" Then ' phone contact type

                        'flagWarning = False
                    ElseIf NullHelper.ObjectToString(row.Cells(78).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(79).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(81).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(82).Value).Trim() = "" And NullHelper.ObjectToString(row.Cells(83).Value).Trim() = "" Then ' phone contact type
                        'flagWarning = False
                    Else
                        flagWarning = True
                        strErrMsg = strErrMsg + " | Warning : Required Director Employer Address Field Missing, Data might not be saved properly "
                    End If

                    If TypeChecker.IsContained("contact_type", NullHelper.ObjectToString(row.Cells(78).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid  Director Employer Address Type " + """" + row.Cells(78).Value.ToString() + """"
                        flagWarning = True
                    End If

                    If TypeChecker.IsContained("country_type", NullHelper.ObjectToString(row.Cells(82).Value), True) = False Then

                        strErrMsg = strErrMsg + " | Invalid Director Employer Address Country Type " + """" + row.Cells(82).Value.ToString() + """"
                        flagWarning = True
                    End If






                End If

                    RecGroup = row.Cells(0).Value



                If flagRequireField = True Then
                    strErrMsg = "| Require Field Missing |" & strErrMsg
                End If

                If flagWarning = True Then
                    row.Cells(91).Value = strErrMsg
                    'row.DefaultCellStyle.BackColor = Color.Yellow
                End If

                If flagRequireField = True Then

                    row.Cells(90).Value = 1
                    row.DefaultCellStyle.BackColor = Color.Red
                    row.Cells(91).Value = strErrMsg
                    btnPrepareEntity.Enabled = False



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
            Dim IntDirectorID As Integer = 0
            Dim IntEntityID As Integer = 0

            Dim EntityGroup As String = ""
            Dim DirectorGroup As String = ""

            Dim EntityPhoneSlno As Integer = 1
            Dim EntityAddressSlno As Integer = 1

            Dim PhoneSlno As Integer = 1
            Dim AddressSlno As Integer = 1
            Dim EmpPhnSlno As Integer = 1
            Dim EmpAddressSlno As Integer = 1
            Dim IDentSlno As Integer = 1



            For i = 0 To dgView.Rows.Count - 1

                _debugRow = i + 1

                If EntityGroup <> dgView.Rows(i).Cells(0).Value.ToString() Then 'different Entity group start


                    If NullHelper.ObjectToString(dgView.Rows(i).Cells(4).Value) = "" Then 'check entity id



                        'reset variables
                        EntityPhoneSlno = 1
                        EntityAddressSlno = 1

                        'add entity

                        Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_MAX_Entity_ID")

                        commProc2.Parameters.Clear()
                        Dim maxid As String = db.ExecuteDataSet(commProc2, trans).Tables(0).Rows(0)(0).ToString()

                        IntEntityID = maxid + 1

                        'If TypeChecker.IsContained("legal_form_type", dgView.Rows(i).Cells(7).Value) = False Then


                        '    MessageBox.Show("Please Correct Incorporation Legal Form Type" + Environment.NewLine + "Error Row: " + _debugRow.ToString(), "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        '    Exit Function

                        'End If

                        Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_Add")

                        commProc.Parameters.Clear()


                        db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, IntEntityID.ToString)
                        db.AddInParameter(commProc, "@NAME", DbType.String, dgView.Rows(i).Cells(5).Value)
                        db.AddInParameter(commProc, "@COMMERTIAL_NAME", DbType.String, dgView.Rows(i).Cells(6).Value)
                        db.AddInParameter(commProc, "@INCORPORATION_LEGAL_FORM", DbType.String, dgView.Rows(i).Cells(7).Value)
                        db.AddInParameter(commProc, "@INCORPORATION_NUMBER", DbType.String, dgView.Rows(i).Cells(8).Value)

                        db.AddInParameter(commProc, "@BUSINESS", DbType.String, dgView.Rows(i).Cells(9).Value)
                        db.AddInParameter(commProc, "@EMAIL", DbType.String, dgView.Rows(i).Cells(22).Value)
                        db.AddInParameter(commProc, "@URL", DbType.String, dgView.Rows(i).Cells(23).Value)
                        db.AddInParameter(commProc, "@INCORPORATION_STATE", DbType.String, dgView.Rows(i).Cells(24).Value)
                        db.AddInParameter(commProc, "@INCORPORATION_COUNTRY", DbType.String, dgView.Rows(i).Cells(25).Value)
                        db.AddInParameter(commProc, "@INCORPORATION_DATE", DbType.DateTime, dgView.Rows(i).Cells(26).Value)

                        db.AddInParameter(commProc, "@BUSINESS__CLOSE", DbType.String, dgView.Rows(i).Cells(27).Value)

                        db.AddInParameter(commProc, "@DATE_BUSINESS_CLOSE", DbType.DateTime, dgView.Rows(i).Cells(28).Value)
                        db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, dgView.Rows(i).Cells(29).Value)
                        db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, dgView.Rows(i).Cells(30).Value)


                        db.ExecuteNonQuery(commProc, trans)

                        ' Add Personal Phone

                        Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_EntityPhone_Add")
                        commProcSche.Parameters.Clear()

                        db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, EntityPhoneSlno)
                        db.AddInParameter(commProcSche, "@ENTITY_ID", DbType.String, IntEntityID)
                        db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                        db.AddInParameter(commProcSche, "@SLNO", DbType.String, EntityPhoneSlno)
                        db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(10).Value)
                        db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(11).Value)
                        db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(12).Value)
                        db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(13).Value)
                        db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(14).Value)


                        db.ExecuteNonQuery(commProcSche, trans)


                        ' Add Person Address 

                        Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_EntityAddress_Add")


                        commProcAdd.Parameters.Clear()
                        db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, EntityAddressSlno)
                        db.AddInParameter(commProcAdd, "@ENTITY_ID", DbType.String, IntEntityID.ToString)
                        db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                        db.AddInParameter(commProcAdd, "@SLNO", DbType.String, EntityAddressSlno)
                        db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(15).Value)
                        db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(16).Value)

                        If NullHelper.ObjectToString(dgView.Rows(i).Cells(17).Value) = "" Then

                            db.AddInParameter(commProcAdd, "@TOWN", DbType.String, "N/A")
                        Else
                            db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(17).Value)
                        End If

                        db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(18).Value)
                        db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(19).Value)
                        db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(20).Value)
                        db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(21).Value)

                        db.ExecuteNonQuery(commProcAdd, trans)

                        _intEntityMod = 1



                    Else

                        IntEntityID = dgView.Rows(i).Cells(4).Value

                        If dgView.Rows(i).Cells(1).Value.ToString <> "R" Then


                            'update entity

                            Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_ENTITY_GetMaxMod")

                            commProc2.Parameters.Clear()

                            db.AddInParameter(commProc2, "@ENTITY_ID", DbType.String, IntEntityID.ToString)

                            intEntityMod = db.ExecuteDataSet(commProc2, trans).Tables(0).Rows(0)(0).ToString()



                            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Entity_Update")

                            commProc.Parameters.Clear()


                            db.AddInParameter(commProc, "@ENTITY_ID", DbType.String, IntEntityID.ToString)
                            db.AddInParameter(commProc, "@NAME", DbType.String, dgView.Rows(i).Cells(5).Value)
                            db.AddInParameter(commProc, "@COMMERTIAL_NAME", DbType.String, dgView.Rows(i).Cells(6).Value)
                            db.AddInParameter(commProc, "@INCORPORATION_LEGAL_FORM", DbType.String, dgView.Rows(i).Cells(7).Value)
                            db.AddInParameter(commProc, "@INCORPORATION_NUMBER", DbType.String, dgView.Rows(i).Cells(8).Value)

                            db.AddInParameter(commProc, "@BUSINESS", DbType.String, dgView.Rows(i).Cells(9).Value)
                            db.AddInParameter(commProc, "@EMAIL", DbType.String, dgView.Rows(i).Cells(22).Value)
                            db.AddInParameter(commProc, "@URL", DbType.String, dgView.Rows(i).Cells(23).Value)
                            db.AddInParameter(commProc, "@INCORPORATION_STATE", DbType.String, dgView.Rows(i).Cells(24).Value)
                            db.AddInParameter(commProc, "@INCORPORATION_COUNTRY", DbType.String, dgView.Rows(i).Cells(25).Value)
                            db.AddInParameter(commProc, "@INCORPORATION_DATE", DbType.DateTime, dgView.Rows(i).Cells(26).Value)

                            db.AddInParameter(commProc, "@BUSINESS__CLOSE", DbType.String, dgView.Rows(i).Cells(27).Value)

                            db.AddInParameter(commProc, "@DATE_BUSINESS_CLOSE", DbType.DateTime, dgView.Rows(i).Cells(28).Value)
                            db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, dgView.Rows(i).Cells(29).Value)
                            db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, dgView.Rows(i).Cells(30).Value)

                            db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intEntityMod)
                            db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)


                            db.ExecuteNonQuery(commProc, trans)

                            _intEntityMod = db.GetParameterValue(commProc, "@RET_MOD_NO")

                            ' Add Personal Phone

                            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_EntityPhone_Update")
                            commProcSche.Parameters.Clear()

                            db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, EntityPhoneSlno)
                            db.AddInParameter(commProcSche, "@ENTITY_ID", DbType.String, IntEntityID.ToString)
                            db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                            db.AddInParameter(commProcSche, "@SLNO", DbType.String, EntityPhoneSlno)
                            db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(10).Value)
                            db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(11).Value)
                            db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(12).Value)
                            db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(13).Value)
                            db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(14).Value)

                            db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, _intEntityMod)


                            db.ExecuteNonQuery(commProcSche, trans)


                            ' Add Person Address 

                            Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_EntityAddress_Update")


                            commProcAdd.Parameters.Clear()
                            db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, EntityAddressSlno)
                            db.AddInParameter(commProcAdd, "@ENTITY_ID", DbType.String, IntEntityID.ToString)
                            db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                            db.AddInParameter(commProcAdd, "@SLNO", DbType.String, EntityAddressSlno)
                            db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(15).Value)
                            db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(16).Value)

                            If NullHelper.ObjectToString(dgView.Rows(i).Cells(17).Value) = "" Then

                                db.AddInParameter(commProcAdd, "@TOWN", DbType.String, "N/A")
                            Else
                                db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(17).Value)
                            End If

                            db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(18).Value)
                            db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(19).Value)
                            db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(20).Value)
                            db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(21).Value)

                            db.AddInParameter(commProcAdd, "@MOD_NO", DbType.Int32, _intEntityMod)



                            db.ExecuteNonQuery(commProcAdd, trans)


                        End If


                    End If



                    If DirectorGroup <> dgView.Rows(i).Cells(32).Value.ToString() Then 'different director start

                        'reset variables
                        PhoneSlno = 1
                        AddressSlno = 1
                        EmpPhnSlno = 1
                        EmpAddressSlno = 1
                        IDentSlno = 1


                        If NullHelper.ObjectToString(dgView.Rows(i).Cells(33).Value) = "" Then ' check director

                            'Add director

                            Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_MAX_Director_ID")

                            commProc2.Parameters.Clear()

                            Dim maxid As Integer = db.ExecuteDataSet(commProc2, trans).Tables(0).Rows(0)(0)

                            IntDirectorID = maxid + 1

                            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_Add")

                            commProc.Parameters.Clear()


                            db.AddInParameter(commProc, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProc, "@GENDER", DbType.String, dgView.Rows(i).Cells(34).Value)
                            db.AddInParameter(commProc, "@TITLE", DbType.String, dgView.Rows(i).Cells(35).Value)
                            db.AddInParameter(commProc, "@FIRST_NAME", DbType.String, dgView.Rows(i).Cells(36).Value)
                            db.AddInParameter(commProc, "@MIDDLE_NAME", DbType.String, dgView.Rows(i).Cells(37).Value)
                            db.AddInParameter(commProc, "@LAST_NAME", DbType.String, dgView.Rows(i).Cells(38).Value)
                            db.AddInParameter(commProc, "@PREFIX", DbType.String, dgView.Rows(i).Cells(39).Value)
                            db.AddInParameter(commProc, "@BIRTHDATE", DbType.DateTime, dgView.Rows(i).Cells(40).Value)
                            db.AddInParameter(commProc, "@BIRTH_PLACE", DbType.String, dgView.Rows(i).Cells(41).Value)
                            db.AddInParameter(commProc, "@MOTHERS_NAME", DbType.String, dgView.Rows(i).Cells(42).Value)
                            db.AddInParameter(commProc, "@ALIAS", DbType.String, dgView.Rows(i).Cells(43).Value)
                            db.AddInParameter(commProc, "@SSN", DbType.String, dgView.Rows(i).Cells(44).Value)
                            db.AddInParameter(commProc, "@PASSPORT_NUMBER", DbType.String, dgView.Rows(i).Cells(45).Value)
                            db.AddInParameter(commProc, "@PASSPORT_COUNTRY", DbType.String, dgView.Rows(i).Cells(46).Value)
                            db.AddInParameter(commProc, "@ID_NUMBER", DbType.String, dgView.Rows(i).Cells(47).Value)
                            db.AddInParameter(commProc, "@NATIONALITY1", DbType.String, dgView.Rows(i).Cells(48).Value)
                            db.AddInParameter(commProc, "@NATIONALITY2", DbType.String, dgView.Rows(i).Cells(49).Value)
                            db.AddInParameter(commProc, "@NATIONALITY3", DbType.String, dgView.Rows(i).Cells(50).Value)
                            db.AddInParameter(commProc, "@RESIDENCE", DbType.String, dgView.Rows(i).Cells(51).Value)
                            db.AddInParameter(commProc, "@EMAIL", DbType.String, dgView.Rows(i).Cells(70).Value)
                            db.AddInParameter(commProc, "@EMAIL2", DbType.String, "")
                            db.AddInParameter(commProc, "@EMAIL3", DbType.String, "")
                            db.AddInParameter(commProc, "@OCCUPATION", DbType.String, dgView.Rows(i).Cells(71).Value)
                            db.AddInParameter(commProc, "@EMPLOYER_NAME", DbType.String, dgView.Rows(i).Cells(72).Value)
                            db.AddInParameter(commProc, "@DECEASED", DbType.String, dgView.Rows(i).Cells(84).Value)

                            db.AddInParameter(commProc, "@DECEASED_DATE", DbType.DateTime, dgView.Rows(i).Cells(85).Value)

                            db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, dgView.Rows(i).Cells(86).Value)
                            db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, dgView.Rows(i).Cells(87).Value)
                            db.AddInParameter(commProc, "@SOURCE_OF_WEALTH", DbType.String, dgView.Rows(i).Cells(88).Value)
                            db.AddInParameter(commProc, "@COMMENTS", DbType.String, dgView.Rows(i).Cells(89).Value)



                            db.ExecuteNonQuery(commProc, trans)


                            ' Phone

                            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Add")

                            commProcSche.Parameters.Clear()

                            db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, PhoneSlno)
                            db.AddInParameter(commProcSche, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                            db.AddInParameter(commProcSche, "@SLNO", DbType.String, PhoneSlno)
                            db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(52).Value)
                            db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(53).Value)
                            db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(54).Value)
                            db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(55).Value)
                            db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(56).Value)

                            db.ExecuteNonQuery(commProcSche, trans)

                            ' Add Person Address 

                            Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Add")

                            commProcAdd.Parameters.Clear()

                            db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, AddressSlno)
                            db.AddInParameter(commProcAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                            db.AddInParameter(commProcAdd, "@SLNO", DbType.String, AddressSlno)
                            db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(57).Value)
                            db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(58).Value)
                            db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(59).Value)
                            db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(60).Value)
                            db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(61).Value)
                            db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(62).Value)
                            db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(63).Value)

                            db.ExecuteNonQuery(commProcAdd, trans)

                            'Add Identification

                            Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_DirectorIdentification_Add")

                            commProcIdent.Parameters.Clear()

                            db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, IDentSlno)
                            db.AddInParameter(commProcIdent, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                            db.AddInParameter(commProcIdent, "@SLNO", DbType.String, IDentSlno)
                            db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(64).Value)
                            db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(65).Value)
                            db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(66).Value)
                            db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(67).Value)
                            db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(69).Value)
                            db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(68).Value)

                            db.ExecuteNonQuery(commProcIdent, trans)

                            ' Employer Phone

                            If (NullHelper.ObjectToString(dgView.Rows(i).Cells(73).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(74).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(76).Value) <> "") Then

                                Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Add")

                                commProcEMPhone.Parameters.Clear()

                                db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, EmpPhnSlno)
                                db.AddInParameter(commProcEMPhone, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                                db.AddInParameter(commProcEMPhone, "@SLNO", DbType.String, EmpPhnSlno)
                                db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(73).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(74).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(75).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(76).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(77).Value)

                                db.ExecuteNonQuery(commProcEMPhone, trans)

                            End If


                            ' Add Employer Address 

                            If (NullHelper.ObjectToString(dgView.Rows(i).Cells(78).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(79).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(81).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(82).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(83).Value) <> "") Then

                                Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Add")

                                commProcEMAdd.Parameters.Clear()

                                db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, EmpAddressSlno)
                                db.AddInParameter(commProcEMAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                                db.AddInParameter(commProcEMAdd, "@SLNO", DbType.String, EmpAddressSlno)
                                db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(78).Value)
                                db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(79).Value)
                                db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(80).Value)
                                db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(81).Value)
                                db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, "")
                                db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(82).Value)
                                db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(83).Value)

                                db.ExecuteNonQuery(commProcEMAdd, trans)

                            End If

                            _intModno = 1


                        Else

                            IntDirectorID = dgView.Rows(i).Cells(33).Value

                            If dgView.Rows(i).Cells(1).Value.ToString <> "R" Then

                                'update director

                                Dim commProcMod As DbCommand = db.GetStoredProcCommand("GO_Director_GetMaxMod")

                                commProcMod.Parameters.Clear()

                                db.AddInParameter(commProcMod, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString)

                                intModno = db.ExecuteDataSet(commProcMod, trans).Tables(0).Rows(0)(0)

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_Update")

                                commProc.Parameters.Clear()


                                db.AddInParameter(commProc, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProc, "@GENDER", DbType.String, dgView.Rows(i).Cells(34).Value)
                                db.AddInParameter(commProc, "@TITLE", DbType.String, dgView.Rows(i).Cells(35).Value)
                                db.AddInParameter(commProc, "@FIRST_NAME", DbType.String, dgView.Rows(i).Cells(36).Value)
                                db.AddInParameter(commProc, "@MIDDLE_NAME", DbType.String, dgView.Rows(i).Cells(37).Value)
                                db.AddInParameter(commProc, "@LAST_NAME", DbType.String, dgView.Rows(i).Cells(38).Value)
                                db.AddInParameter(commProc, "@PREFIX", DbType.String, dgView.Rows(i).Cells(39).Value)
                                db.AddInParameter(commProc, "@BIRTHDATE", DbType.DateTime, dgView.Rows(i).Cells(40).Value)
                                db.AddInParameter(commProc, "@BIRTH_PLACE", DbType.String, dgView.Rows(i).Cells(41).Value)
                                db.AddInParameter(commProc, "@MOTHERS_NAME", DbType.String, dgView.Rows(i).Cells(42).Value)
                                db.AddInParameter(commProc, "@ALIAS", DbType.String, dgView.Rows(i).Cells(43).Value)
                                db.AddInParameter(commProc, "@SSN", DbType.String, dgView.Rows(i).Cells(44).Value)
                                db.AddInParameter(commProc, "@PASSPORT_NUMBER", DbType.String, dgView.Rows(i).Cells(45).Value)
                                db.AddInParameter(commProc, "@PASSPORT_COUNTRY", DbType.String, dgView.Rows(i).Cells(46).Value)
                                db.AddInParameter(commProc, "@ID_NUMBER", DbType.String, dgView.Rows(i).Cells(47).Value)
                                db.AddInParameter(commProc, "@NATIONALITY1", DbType.String, dgView.Rows(i).Cells(48).Value)
                                db.AddInParameter(commProc, "@NATIONALITY2", DbType.String, dgView.Rows(i).Cells(49).Value)
                                db.AddInParameter(commProc, "@NATIONALITY3", DbType.String, dgView.Rows(i).Cells(50).Value)
                                db.AddInParameter(commProc, "@RESIDENCE", DbType.String, dgView.Rows(i).Cells(51).Value)
                                db.AddInParameter(commProc, "@EMAIL", DbType.String, dgView.Rows(i).Cells(70).Value)
                                db.AddInParameter(commProc, "@EMAIL2", DbType.String, "")
                                db.AddInParameter(commProc, "@EMAIL3", DbType.String, "")
                                db.AddInParameter(commProc, "@OCCUPATION", DbType.String, dgView.Rows(i).Cells(71).Value)
                                db.AddInParameter(commProc, "@EMPLOYER_NAME", DbType.String, dgView.Rows(i).Cells(72).Value)
                                db.AddInParameter(commProc, "@DECEASED", DbType.String, dgView.Rows(i).Cells(84).Value)

                                db.AddInParameter(commProc, "@DECEASED_DATE", DbType.DateTime, dgView.Rows(i).Cells(85).Value)

                                db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, dgView.Rows(i).Cells(86).Value)
                                db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, dgView.Rows(i).Cells(87).Value)
                                db.AddInParameter(commProc, "@SOURCE_OF_WEALTH", DbType.String, dgView.Rows(i).Cells(88).Value)
                                db.AddInParameter(commProc, "@COMMENTS", DbType.String, dgView.Rows(i).Cells(89).Value)

                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intModno)
                                db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)

                                db.ExecuteNonQuery(commProc, trans)

                                _intModno = db.GetParameterValue(commProc, "@RET_MOD_NO")


                                ' Phone

                                Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Update")

                                commProcSche.Parameters.Clear()

                                db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, PhoneSlno)
                                db.AddInParameter(commProcSche, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                                db.AddInParameter(commProcSche, "@SLNO", DbType.String, PhoneSlno)
                                db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(52).Value)
                                db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(53).Value)
                                db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(54).Value)
                                db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(55).Value)
                                db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(56).Value)

                                db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, _intModno)


                                db.ExecuteNonQuery(commProcSche, trans)

                                ' Add Person Address 

                                Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Update")

                                commProcAdd.Parameters.Clear()

                                db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, AddressSlno)
                                db.AddInParameter(commProcAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                                db.AddInParameter(commProcAdd, "@SLNO", DbType.String, AddressSlno)
                                db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(57).Value)
                                db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(58).Value)
                                db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(59).Value)
                                db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(60).Value)
                                db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(61).Value)
                                db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(62).Value)
                                db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(63).Value)

                                db.AddInParameter(commProcAdd, "@MOD_NO", DbType.Int32, _intModno)

                                db.ExecuteNonQuery(commProcAdd, trans)

                                'Add Identification

                                Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_DirectorIdentification_Update")

                                commProcIdent.Parameters.Clear()

                                db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, IDentSlno)
                                db.AddInParameter(commProcIdent, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                                db.AddInParameter(commProcIdent, "@SLNO", DbType.String, IDentSlno)
                                db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(64).Value)
                                db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(65).Value)
                                db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(66).Value)
                                db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(67).Value)
                                db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(69).Value)
                                db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(68).Value)

                                db.AddInParameter(commProcIdent, "@MOD_NO", DbType.Int32, _intModno)

                                db.ExecuteNonQuery(commProcIdent, trans)

                                ' Employer Phone

                                If (NullHelper.ObjectToString(dgView.Rows(i).Cells(73).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(74).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(76).Value) <> "") Then

                                    Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Update")

                                    commProcEMPhone.Parameters.Clear()

                                    db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, EmpPhnSlno)
                                    db.AddInParameter(commProcEMPhone, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                    db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                                    db.AddInParameter(commProcEMPhone, "@SLNO", DbType.String, EmpPhnSlno)
                                    db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(73).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(74).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(75).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(76).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(77).Value)

                                    db.AddInParameter(commProcEMPhone, "@MOD_NO", DbType.Int32, _intModno)

                                    db.ExecuteNonQuery(commProcEMPhone, trans)

                                End If


                                ' Add Employer Address 

                                If (NullHelper.ObjectToString(dgView.Rows(i).Cells(78).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(79).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(81).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(82).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(83).Value) <> "") Then

                                    Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Update")

                                    commProcEMAdd.Parameters.Clear()

                                    db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, EmpAddressSlno)
                                    db.AddInParameter(commProcEMAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                    db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                                    db.AddInParameter(commProcEMAdd, "@SLNO", DbType.String, EmpAddressSlno)
                                    db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(78).Value)
                                    db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(79).Value)
                                    db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(80).Value)
                                    db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(81).Value)
                                    db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, "")
                                    db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(82).Value)
                                    db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(83).Value)

                                    db.AddInParameter(commProcEMAdd, "@MOD_NO", DbType.Int32, _intModno)

                                    db.ExecuteNonQuery(commProcEMAdd, trans)

                                End If





                            End If

                        End If

                        'mapping director entity

                        Dim dtEntityDirectorMap As DataTable

                        If dgView.Rows(i).Cells(1).Value.ToString = "A" Then

                            Dim commProc As DbCommand


                            strSql = "SELECT ENTITY_ID,DIRECTOR_ID,MOD_NO FROM GO_DIRECTOR_ENTITY_MAP_HIST WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND IS_AUTH = 0"


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            dtEntityDirectorMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                            If dtEntityDirectorMap.Rows.Count > 0 Then ' if history

                                strSql = "INSERT INTO GO_DIRECTOR_ENTITY_MAP_HIST(ENTITY_ID, DIRECTOR_ID, ROLE, MOD_NO, STATUS, IS_AUTH) " & _
                                         "VALUES(@P_ENTITY_ID,@P_DIRECTOR_ID,@P_ROLE,@P_MODNO,'U',0)"


                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@P_ENTITY_ID", DbType.String, IntEntityID)
                                db.AddInParameter(commProc, "@P_DIRECTOR_ID", DbType.String, IntDirectorID)
                                db.AddInParameter(commProc, "@P_ROLE", DbType.String, dgView.Rows(i).Cells(3).Value)

                                db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, dtEntityDirectorMap.Rows(0)("MOD_NO"))

                                db.ExecuteNonQuery(commProc, trans)

                            Else ' 

                                strSql = "SELECT ENTITY_ID,DIRECTOR_ID,MOD_NO FROM GO_DIRECTOR_ENTITY_MAP WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND STATUS = 'L' "


                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()
                                Dim dtAcOwnerMap As DataTable

                                dtAcOwnerMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                                If dtAcOwnerMap.Rows.Count > 0 Then ' if last data

                                    intMod = NullHelper.ToIntNum(dtAcOwnerMap.Rows(0)("MOD_NO"))


                                    strSql = "INSERT INTO GO_DIRECTOR_ENTITY_MAP_HIST(ENTITY_ID, DIRECTOR_ID, ROLE, MOD_NO, STATUS, IS_AUTH) " & _
                                             "SELECT ENTITY_ID, DIRECTOR_ID, ROLE,@P_MODNO,'U',0 " & _
                                             "FROM GO_DIRECTOR_ENTITY_MAP " & _
                                             " WHERE ENTITY_ID ='" & IntEntityID & "' AND STATUS = 'L' "


                                    commProc = db.GetSqlStringCommand(strSql)

                                    commProc.Parameters.Clear()

                                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)

                                    db.ExecuteNonQuery(commProc, trans)


                                    strSql = "INSERT INTO GO_DIRECTOR_ENTITY_MAP_HIST(ENTITY_ID, DIRECTOR_ID, ROLE, MOD_NO, STATUS, IS_AUTH) " & _
                                          "VALUES(@P_ENTITY_ID,@P_DIRECTOR_ID,@P_ROLE,@P_MODNO,'U',0)"


                                    commProc = db.GetSqlStringCommand(strSql)

                                    commProc.Parameters.Clear()

                                    db.AddInParameter(commProc, "@P_ENTITY_ID", DbType.String, IntEntityID)
                                    db.AddInParameter(commProc, "@P_DIRECTOR_ID", DbType.String, IntDirectorID)
                                    db.AddInParameter(commProc, "@P_ROLE", DbType.String, dgView.Rows(i).Cells(3).Value)

                                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)

                                    db.ExecuteNonQuery(commProc, trans)

                                Else ' if no data


                                    strSql = "INSERT INTO GO_DIRECTOR_ENTITY_MAP_HIST(ENTITY_ID, DIRECTOR_ID, ROLE, MOD_NO, STATUS, IS_AUTH) " & _
                                          "VALUES(@P_ENTITY_ID,@P_DIRECTOR_ID,@P_ROLE,@P_MODNO,'U',0)"


                                    commProc = db.GetSqlStringCommand(strSql)

                                    commProc.Parameters.Clear()

                                    db.AddInParameter(commProc, "@P_ENTITY_ID", DbType.String, IntEntityID)
                                    db.AddInParameter(commProc, "@P_DIRECTOR_ID", DbType.String, IntDirectorID)
                                    db.AddInParameter(commProc, "@P_ROLE", DbType.String, dgView.Rows(i).Cells(3).Value)

                                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, 1)

                                    db.ExecuteNonQuery(commProc, trans)



                                End If



                            End If



                        ElseIf dgView.Rows(i).Cells(1).Value.ToString = "R" Then

                            Dim commProc As DbCommand


                            strSql = "SELECT ENTITY_ID,DIRECTOR_ID,MOD_NO FROM GO_DIRECTOR_ENTITY_MAP_HIST WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND DIRECTOR_ID='" & IntDirectorID.ToString & "' AND IS_AUTH = 0"


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            dtEntityDirectorMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                            If dtEntityDirectorMap.Rows.Count > 0 Then ' if history

                                intMod = NullHelper.ToIntNum(dtEntityDirectorMap.Rows(0)("MOD_NO"))

                                strSql = "DELETE FROM GO_DIRECTOR_ENTITY_MAP_HIST WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND  DIRECTOR_ID='" & IntDirectorID.ToString & "' And MOD_NO='" & intMod & "' AND IS_AUTH = 0"

                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()

                                db.ExecuteNonQuery(commProc, trans)

                            Else

                                strSql = "SELECT ENTITY_ID,DIRECTOR_ID,MOD_NO FROM GO_DIRECTOR_ENTITY_MAP_HIST WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND DIRECTOR_ID='" & IntDirectorID.ToString & "' AND STATUS = 'L' "


                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()
                                Dim dtAcOwnerMap As DataTable

                                dtAcOwnerMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                                If dtAcOwnerMap.Rows.Count > 0 Then

                                    intMod = NullHelper.ToIntNum(dtAcOwnerMap.Rows(0)("MOD_NO"))


                                    strSql = "INSERT INTO GO_DIRECTOR_ENTITY_MAP_HIST(ENTITY_ID, DIRECTOR_ID, ROLE, MOD_NO, STATUS, IS_AUTH) " & _
                                             "SELECT ENTITY_ID, DIRECTOR_ID, ROLE,@P_MODNO,'U',0 " & _
                                             "FROM GO_DIRECTOR_ENTITY_MAP " & _
                                             " WHERE ENTITY_ID ='" & IntEntityID & "' AND STATUS = 'L' "


                                    commProc = db.GetSqlStringCommand(strSql)

                                    commProc.Parameters.Clear()

                                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)

                                    db.ExecuteNonQuery(commProc, trans)


                                    strSql = "DELETE FROM GO_DIRECTOR_ENTITY_MAP_HIST WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND  DIRECTOR_ID='" & IntDirectorID.ToString & "' And MOD_NO='" & intMod & "' AND IS_AUTH = 0"

                                    commProc = db.GetSqlStringCommand(strSql)

                                    commProc.Parameters.Clear()

                                    db.ExecuteNonQuery(commProc, trans)


                                End If



                            End If



                        End If


                    Else 'same director group


                        'PhoneSlno = PhoneSlno + 1
                        'AddressSlno = AddressSlno + 1
                        'EmpPhnSlno = EmpPhnSlno + 1
                        'EmpAddressSlno = EmpAddressSlno + 1
                        'IDentSlno = IDentSlno + 1



                        'Personal Phone

                        'If (NullHelper.ObjectToString(dgView.Rows(i).Cells(52).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(53).Value) = "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(55).Value) = "") Then

                        '    Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Update")

                        '    commProcSche.Parameters.Clear()

                        '    db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, PhoneSlno)
                        '    db.AddInParameter(commProcSche, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                        '    db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                        '    db.AddInParameter(commProcSche, "@SLNO", DbType.String, PhoneSlno)
                        '    db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(52).Value)
                        '    db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(53).Value)
                        '    db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(54).Value)
                        '    db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(55).Value)
                        '    db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(56).Value)

                        '    db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, _intModno)

                        '    db.ExecuteNonQuery(commProcSche, trans)

                        'End If

                        'Add Person Address 

                        'If (NullHelper.ObjectToString(dgView.Rows(i).Cells(57).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(58).Value) = "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(62).Value) = "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(63).Value) = "") Then

                        '    Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Update")

                        '    commProcAdd.Parameters.Clear()

                        '    db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, AddressSlno)
                        '    db.AddInParameter(commProcAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                        '    db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                        '    db.AddInParameter(commProcAdd, "@SLNO", DbType.String, AddressSlno)
                        '    db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(57).Value)
                        '    db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(58).Value)
                        '    db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(59).Value)
                        '    db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(60).Value)
                        '    db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(61).Value)
                        '    db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(62).Value)
                        '    db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(63).Value)
                        '    db.AddInParameter(commProcAdd, "@MOD_NO", DbType.Int32, _intModno)

                        '    db.ExecuteNonQuery(commProcAdd, trans)

                        'End If

                        'Add Identification

                        'If (NullHelper.ObjectToString(dgView.Rows(i).Cells(64).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(65).Value) = "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(68).Value) = "") Then

                        '    Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_DirectorIdentification_Update")

                        '    commProcIdent.Parameters.Clear()

                        '    db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, IDentSlno)
                        '    db.AddInParameter(commProcIdent, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                        '    db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                        '    db.AddInParameter(commProcIdent, "@SLNO", DbType.String, IDentSlno)
                        '    db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(64).Value)
                        '    db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(65).Value)
                        '    db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(66).Value)
                        '    db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(67).Value)
                        '    db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(69).Value)
                        '    db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(68).Value)
                        '    db.AddInParameter(commProcIdent, "@MOD_NO", DbType.Int32, _intModno)

                        '    db.ExecuteNonQuery(commProcIdent, trans)

                        'End If

                        ' Employer Phone

                        'If (NullHelper.ObjectToString(dgView.Rows(i).Cells(73).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(74).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(76).Value) <> "") Then

                        '    Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Update")

                        '    commProcEMPhone.Parameters.Clear()

                        '    db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, EmpPhnSlno)
                        '    db.AddInParameter(commProcEMPhone, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                        '    db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                        '    db.AddInParameter(commProcEMPhone, "@SLNO", DbType.String, EmpPhnSlno)
                        '    db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(73).Value)
                        '    db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(74).Value)
                        '    db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(75).Value)
                        '    db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(76).Value)
                        '    db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(77).Value)
                        '    db.AddInParameter(commProcEMPhone, "@MOD_NO", DbType.Int32, _intModno)

                        '    db.ExecuteNonQuery(commProcEMPhone, trans)

                        'End If


                        ' Add Employer Address 

                        'If (NullHelper.ObjectToString(dgView.Rows(i).Cells(78).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(79).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(83).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(82).Value) <> "") Then

                        '    Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Update")

                        '    commProcEMAdd.Parameters.Clear()

                        '    db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, EmpAddressSlno)
                        '    db.AddInParameter(commProcEMAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                        '    db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                        '    db.AddInParameter(commProcEMAdd, "@SLNO", DbType.String, EmpAddressSlno)
                        '    db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(78).Value)
                        '    db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(79).Value)
                        '    db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(80).Value)
                        '    db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(81).Value)
                        '    db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, "")
                        '    db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(82).Value)
                        '    db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(83).Value)
                        '    db.AddInParameter(commProcEMAdd, "@MOD_NO", DbType.Int32, _intModno)

                        '    db.ExecuteNonQuery(commProcEMAdd, trans)

                        'End If




                    End If 'director group

                    DirectorGroup = dgView.Rows(i).Cells(32).Value 'director group end


                    ' update account info goAML
                    AcNumber = dgView.Rows(i).Cells(2).Value.ToString()

                    If AcNumber <> "" Then


                        strSql = "SELECT ACNUMBER,MOD_NO FROM GO_ACCOUNT_INFO WHERE ACNUMBER='" & AcNumber.ToString & "' AND STATUS = 'L'"

                        Dim commProc As DbCommand

                        commProc = db.GetSqlStringCommand(strSql)

                        commProc.Parameters.Clear()
                        Dim dtAcOwnerMap As DataTable

                        dtAcOwnerMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                        If dtAcOwnerMap.Rows.Count > 0 Then


                            intMod = NullHelper.ToIntNum(dtAcOwnerMap.Rows(0)("MOD_NO"))

                            strSql = "DELETE FROM GO_ACCOUNT_INFO_HIST WHERE ACNUMBER='" & AcNumber.ToString & "' AND IS_AUTH = 0"

                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            db.ExecuteNonQuery(commProc, trans)


                            strSql = "INSERT INTO GO_ACCOUNT_INFO_HIST(ACNUMBER, CURRENCY_CODE, IBAN, CLIENT_NUMBER, ACCOUNT_TYPE, OPENED, CLOSED, STATUS_CODE, BENEFICIARY, BENEFICIARY_COMMENTS, COMMENTS, INPUT_BY,INPUT_DATETIME, MOD_NO, STATUS, IS_AUTH, ENTITY_ID) " & _
                                            "SELECT ACNUMBER, CURRENCY_CODE, IBAN, CLIENT_NUMBER, ACCOUNT_TYPE, OPENED, CLOSED, STATUS_CODE, BENEFICIARY, BENEFICIARY_COMMENTS, COMMENTS, @P_INPUT_BY,@INPUT_DATETIME,@P_MODNO,'U',0, @P_ENTITY_ID " & _
                                            "FROM GO_ACCOUNT_INFO " & _
                                            " WHERE ACNUMBER ='" & AcNumber & "' AND STATUS = 'L' "


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            db.AddInParameter(commProc, "@INPUT_DATETIME", DbType.DateTime, DateTime.Now)

                            db.AddInParameter(commProc, "@P_ENTITY_ID", DbType.String, IntEntityID.ToString)
                            db.AddInParameter(commProc, "@P_INPUT_BY", DbType.String, CommonAppSet.User)
                            db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)

                            db.ExecuteNonQuery(commProc, trans)





                        Else

                            MessageBox.Show(AcNumber + " Account Number Not Found", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        End If





                    End If

                    ' end update account info


                Else ' Same entity group

                    EntityPhoneSlno = EntityPhoneSlno + 1
                    EntityAddressSlno = EntityAddressSlno + 1

                    ' Add Personal Phone
                    If (NullHelper.ObjectToString(dgView.Rows(i).Cells(10).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(11).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(10).Value) <> "") Then



                        Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_EntityPhone_Update")
                        commProcSche.Parameters.Clear()

                        db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, EntityPhoneSlno)
                        db.AddInParameter(commProcSche, "@ENTITY_ID", DbType.String, IntEntityID.ToString)
                        db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                        db.AddInParameter(commProcSche, "@SLNO", DbType.String, EntityPhoneSlno)
                        db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(10).Value)
                        db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(11).Value)
                        db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(12).Value)
                        db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(13).Value)
                        db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(14).Value)

                        db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, _intEntityMod)


                        db.ExecuteNonQuery(commProcSche, trans)

                    End If

                    ' Add Person Address 
                    If (NullHelper.ObjectToString(dgView.Rows(i).Cells(15).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(16).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(18).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(20).Value) <> "") Then

                        Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_EntityAddress_Update")


                        commProcAdd.Parameters.Clear()
                        db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, EntityAddressSlno)
                        db.AddInParameter(commProcAdd, "@ENTITY_ID", DbType.String, IntEntityID.ToString)
                        db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                        db.AddInParameter(commProcAdd, "@SLNO", DbType.String, EntityAddressSlno)
                        db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(15).Value)
                        db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(16).Value)

                        If NullHelper.ObjectToString(dgView.Rows(i).Cells(17).Value) = "" Then

                            db.AddInParameter(commProcAdd, "@TOWN", DbType.String, "N/A")
                        Else
                            db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(17).Value)
                        End If

                        db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(18).Value)
                        db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(19).Value)
                        db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(20).Value)
                        db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(21).Value)

                        db.AddInParameter(commProcAdd, "@MOD_NO", DbType.Int32, _intEntityMod)



                        db.ExecuteNonQuery(commProcAdd, trans)

                    End If

                    If DirectorGroup = dgView.Rows(i).Cells(32).Value.ToString Then 'same director



                        PhoneSlno = PhoneSlno + 1
                        AddressSlno = AddressSlno + 1
                        EmpPhnSlno = EmpPhnSlno + 1
                        EmpAddressSlno = EmpAddressSlno + 1
                        IDentSlno = IDentSlno + 1



                        'Personal Phone

                        If (NullHelper.ObjectToString(dgView.Rows(i).Cells(52).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(53).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(55).Value) <> "") Then

                            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Update")

                            commProcSche.Parameters.Clear()

                            db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, PhoneSlno)
                            db.AddInParameter(commProcSche, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                            db.AddInParameter(commProcSche, "@SLNO", DbType.String, PhoneSlno)
                            db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(52).Value)
                            db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(53).Value)
                            db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(54).Value)
                            db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(55).Value)
                            db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(56).Value)

                            db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, _intModno)

                            db.ExecuteNonQuery(commProcSche, trans)

                        End If

                        'Add Person Address 

                        If (NullHelper.ObjectToString(dgView.Rows(i).Cells(57).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(58).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(62).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(63).Value) <> "") Then

                            Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Update")

                            commProcAdd.Parameters.Clear()

                            db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, AddressSlno)
                            db.AddInParameter(commProcAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                            db.AddInParameter(commProcAdd, "@SLNO", DbType.String, AddressSlno)
                            db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(57).Value)
                            db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(58).Value)
                            db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(59).Value)
                            db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(60).Value)
                            db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(61).Value)
                            db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(62).Value)
                            db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(63).Value)
                            db.AddInParameter(commProcAdd, "@MOD_NO", DbType.Int32, _intModno)

                            db.ExecuteNonQuery(commProcAdd, trans)

                        End If

                        'Add Identification

                        If (NullHelper.ObjectToString(dgView.Rows(i).Cells(64).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(65).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(68).Value) <> "") Then

                            Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_DirectorIdentification_Update")

                            commProcIdent.Parameters.Clear()

                            db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, IDentSlno)
                            db.AddInParameter(commProcIdent, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                            db.AddInParameter(commProcIdent, "@SLNO", DbType.String, IDentSlno)
                            db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(64).Value)
                            db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(65).Value)
                            db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(66).Value)
                            db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(67).Value)
                            db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(69).Value)
                            db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(68).Value)
                            db.AddInParameter(commProcIdent, "@MOD_NO", DbType.Int32, _intModno)

                            db.ExecuteNonQuery(commProcIdent, trans)

                        End If

                        ' Employer Phone

                        If (NullHelper.ObjectToString(dgView.Rows(i).Cells(73).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(74).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(76).Value) <> "") Then

                            Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Update")

                            commProcEMPhone.Parameters.Clear()

                            db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, EmpPhnSlno)
                            db.AddInParameter(commProcEMPhone, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                            db.AddInParameter(commProcEMPhone, "@SLNO", DbType.String, EmpPhnSlno)
                            db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(73).Value)
                            db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(74).Value)
                            db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(75).Value)
                            db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(76).Value)
                            db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(77).Value)
                            db.AddInParameter(commProcEMPhone, "@MOD_NO", DbType.Int32, _intModno)

                            db.ExecuteNonQuery(commProcEMPhone, trans)

                        End If


                        ' Add Employer Address 

                        If (NullHelper.ObjectToString(dgView.Rows(i).Cells(78).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(79).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(81).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(83).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(82).Value) <> "") Then

                            Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Update")

                            commProcEMAdd.Parameters.Clear()

                            db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, EmpAddressSlno)
                            db.AddInParameter(commProcEMAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                            db.AddInParameter(commProcEMAdd, "@SLNO", DbType.String, EmpAddressSlno)
                            db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(78).Value)
                            db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(79).Value)
                            db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(80).Value)
                            db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(81).Value)
                            db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, "")
                            db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(82).Value)
                            db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(83).Value)
                            db.AddInParameter(commProcEMAdd, "@MOD_NO", DbType.Int32, _intModno)

                            db.ExecuteNonQuery(commProcEMAdd, trans)

                        End If


                    Else


                        'reset variables
                        PhoneSlno = 1
                        AddressSlno = 1
                        EmpPhnSlno = 1
                        EmpAddressSlno = 1
                        IDentSlno = 1


                        If NullHelper.ObjectToString(dgView.Rows(i).Cells(33).Value) = "" Then ' check director

                            'Add director

                            Dim commProc2 As DbCommand = db.GetStoredProcCommand("GO_MAX_Director_ID")

                            commProc2.Parameters.Clear()

                            Dim maxid As Integer = db.ExecuteDataSet(commProc2, trans).Tables(0).Rows(0)(0)

                            IntDirectorID = maxid + 1

                            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_Add")

                            commProc.Parameters.Clear()


                            db.AddInParameter(commProc, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProc, "@GENDER", DbType.String, dgView.Rows(i).Cells(34).Value)
                            db.AddInParameter(commProc, "@TITLE", DbType.String, dgView.Rows(i).Cells(35).Value)
                            db.AddInParameter(commProc, "@FIRST_NAME", DbType.String, dgView.Rows(i).Cells(36).Value)
                            db.AddInParameter(commProc, "@MIDDLE_NAME", DbType.String, dgView.Rows(i).Cells(37).Value)
                            db.AddInParameter(commProc, "@LAST_NAME", DbType.String, dgView.Rows(i).Cells(38).Value)
                            db.AddInParameter(commProc, "@PREFIX", DbType.String, dgView.Rows(i).Cells(39).Value)
                            db.AddInParameter(commProc, "@BIRTHDATE", DbType.DateTime, dgView.Rows(i).Cells(40).Value)
                            db.AddInParameter(commProc, "@BIRTH_PLACE", DbType.String, dgView.Rows(i).Cells(41).Value)
                            db.AddInParameter(commProc, "@MOTHERS_NAME", DbType.String, dgView.Rows(i).Cells(42).Value)
                            db.AddInParameter(commProc, "@ALIAS", DbType.String, dgView.Rows(i).Cells(43).Value)
                            db.AddInParameter(commProc, "@SSN", DbType.String, dgView.Rows(i).Cells(44).Value)
                            db.AddInParameter(commProc, "@PASSPORT_NUMBER", DbType.String, dgView.Rows(i).Cells(45).Value)
                            db.AddInParameter(commProc, "@PASSPORT_COUNTRY", DbType.String, dgView.Rows(i).Cells(46).Value)
                            db.AddInParameter(commProc, "@ID_NUMBER", DbType.String, dgView.Rows(i).Cells(47).Value)
                            db.AddInParameter(commProc, "@NATIONALITY1", DbType.String, dgView.Rows(i).Cells(48).Value)
                            db.AddInParameter(commProc, "@NATIONALITY2", DbType.String, dgView.Rows(i).Cells(49).Value)
                            db.AddInParameter(commProc, "@NATIONALITY3", DbType.String, dgView.Rows(i).Cells(50).Value)
                            db.AddInParameter(commProc, "@RESIDENCE", DbType.String, dgView.Rows(i).Cells(51).Value)
                            db.AddInParameter(commProc, "@EMAIL", DbType.String, dgView.Rows(i).Cells(70).Value)
                            db.AddInParameter(commProc, "@EMAIL2", DbType.String, "")
                            db.AddInParameter(commProc, "@EMAIL3", DbType.String, "")
                            db.AddInParameter(commProc, "@OCCUPATION", DbType.String, dgView.Rows(i).Cells(71).Value)
                            db.AddInParameter(commProc, "@EMPLOYER_NAME", DbType.String, dgView.Rows(i).Cells(72).Value)
                            db.AddInParameter(commProc, "@DECEASED", DbType.String, dgView.Rows(i).Cells(84).Value)

                            db.AddInParameter(commProc, "@DECEASED_DATE", DbType.DateTime, dgView.Rows(i).Cells(85).Value)

                            db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, dgView.Rows(i).Cells(86).Value)
                            db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, dgView.Rows(i).Cells(87).Value)
                            db.AddInParameter(commProc, "@SOURCE_OF_WEALTH", DbType.String, dgView.Rows(i).Cells(88).Value)
                            db.AddInParameter(commProc, "@COMMENTS", DbType.String, dgView.Rows(i).Cells(89).Value)



                            db.ExecuteNonQuery(commProc, trans)


                            ' Phone

                            Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Add")

                            commProcSche.Parameters.Clear()

                            db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, PhoneSlno)
                            db.AddInParameter(commProcSche, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                            db.AddInParameter(commProcSche, "@SLNO", DbType.String, PhoneSlno)
                            db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(52).Value)
                            db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(53).Value)
                            db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(54).Value)
                            db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(55).Value)
                            db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(56).Value)

                            db.ExecuteNonQuery(commProcSche, trans)

                            ' Add Person Address 


                            Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Add")

                            commProcAdd.Parameters.Clear()

                            db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, AddressSlno)
                            db.AddInParameter(commProcAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                            db.AddInParameter(commProcAdd, "@SLNO", DbType.String, AddressSlno)
                            db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(57).Value)
                            db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(58).Value)
                            db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(59).Value)
                            db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(60).Value)
                            db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(61).Value)
                            db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(62).Value)
                            db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(63).Value)

                            db.ExecuteNonQuery(commProcAdd, trans)

                            'Add Identification

                            Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_DirectorIdentification_Add")

                            commProcIdent.Parameters.Clear()

                            db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, IDentSlno)
                            db.AddInParameter(commProcIdent, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                            db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                            db.AddInParameter(commProcIdent, "@SLNO", DbType.String, IDentSlno)
                            db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(64).Value)
                            db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(65).Value)
                            db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(66).Value)
                            db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(67).Value)
                            db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(69).Value)
                            db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(68).Value)

                            db.ExecuteNonQuery(commProcIdent, trans)

                            ' Employer Phone

                            If (NullHelper.ObjectToString(dgView.Rows(i).Cells(73).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(74).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(76).Value) <> "") Then

                                Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Add")

                                commProcEMPhone.Parameters.Clear()

                                db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, EmpPhnSlno)
                                db.AddInParameter(commProcEMPhone, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                                db.AddInParameter(commProcEMPhone, "@SLNO", DbType.String, EmpPhnSlno)
                                db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(73).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(74).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(75).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(76).Value)
                                db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(77).Value)

                                db.ExecuteNonQuery(commProcEMPhone, trans)

                            End If


                            '' Add Employer Address 

                            If (NullHelper.ObjectToString(dgView.Rows(i).Cells(78).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(79).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(81).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(82).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(83).Value) <> "") Then

                                Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Add")

                                commProcEMAdd.Parameters.Clear()

                                db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, EmpAddressSlno)
                                db.AddInParameter(commProcEMAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                                db.AddInParameter(commProcEMAdd, "@SLNO", DbType.String, EmpAddressSlno)
                                db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(78).Value)
                                db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(79).Value)
                                db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(80).Value)
                                db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(81).Value)
                                db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, "")
                                db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(82).Value)
                                db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(83).Value)

                                db.ExecuteNonQuery(commProcEMAdd, trans)

                            End If

                            _intModno = 1


                        Else

                            IntDirectorID = dgView.Rows(i).Cells(33).Value

                            If dgView.Rows(i).Cells(1).Value.ToString <> "R" Then

                                '    'update director

                                Dim commProcMod As DbCommand = db.GetStoredProcCommand("GO_Director_GetMaxMod")

                                commProcMod.Parameters.Clear()

                                db.AddInParameter(commProcMod, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString)

                                intModno = db.ExecuteDataSet(commProcMod, trans).Tables(0).Rows(0)(0)

                                Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Director_Update")

                                commProc.Parameters.Clear()


                                db.AddInParameter(commProc, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProc, "@GENDER", DbType.String, dgView.Rows(i).Cells(34).Value)
                                db.AddInParameter(commProc, "@TITLE", DbType.String, dgView.Rows(i).Cells(35).Value)
                                db.AddInParameter(commProc, "@FIRST_NAME", DbType.String, dgView.Rows(i).Cells(36).Value)
                                db.AddInParameter(commProc, "@MIDDLE_NAME", DbType.String, dgView.Rows(i).Cells(37).Value)
                                db.AddInParameter(commProc, "@LAST_NAME", DbType.String, dgView.Rows(i).Cells(38).Value)
                                db.AddInParameter(commProc, "@PREFIX", DbType.String, dgView.Rows(i).Cells(39).Value)
                                db.AddInParameter(commProc, "@BIRTHDATE", DbType.DateTime, dgView.Rows(i).Cells(40).Value)
                                db.AddInParameter(commProc, "@BIRTH_PLACE", DbType.String, dgView.Rows(i).Cells(41).Value)
                                db.AddInParameter(commProc, "@MOTHERS_NAME", DbType.String, dgView.Rows(i).Cells(42).Value)
                                db.AddInParameter(commProc, "@ALIAS", DbType.String, dgView.Rows(i).Cells(43).Value)
                                db.AddInParameter(commProc, "@SSN", DbType.String, dgView.Rows(i).Cells(44).Value)
                                db.AddInParameter(commProc, "@PASSPORT_NUMBER", DbType.String, dgView.Rows(i).Cells(45).Value)
                                db.AddInParameter(commProc, "@PASSPORT_COUNTRY", DbType.String, dgView.Rows(i).Cells(46).Value)
                                db.AddInParameter(commProc, "@ID_NUMBER", DbType.String, dgView.Rows(i).Cells(47).Value)
                                db.AddInParameter(commProc, "@NATIONALITY1", DbType.String, dgView.Rows(i).Cells(48).Value)
                                db.AddInParameter(commProc, "@NATIONALITY2", DbType.String, dgView.Rows(i).Cells(49).Value)
                                db.AddInParameter(commProc, "@NATIONALITY3", DbType.String, dgView.Rows(i).Cells(50).Value)
                                db.AddInParameter(commProc, "@RESIDENCE", DbType.String, dgView.Rows(i).Cells(51).Value)
                                db.AddInParameter(commProc, "@EMAIL", DbType.String, dgView.Rows(i).Cells(70).Value)
                                db.AddInParameter(commProc, "@EMAIL2", DbType.String, "")
                                db.AddInParameter(commProc, "@EMAIL3", DbType.String, "")
                                db.AddInParameter(commProc, "@OCCUPATION", DbType.String, dgView.Rows(i).Cells(71).Value)
                                db.AddInParameter(commProc, "@EMPLOYER_NAME", DbType.String, dgView.Rows(i).Cells(72).Value)
                                db.AddInParameter(commProc, "@DECEASED", DbType.String, dgView.Rows(i).Cells(84).Value)

                                db.AddInParameter(commProc, "@DECEASED_DATE", DbType.DateTime, dgView.Rows(i).Cells(85).Value)

                                db.AddInParameter(commProc, "@TAX_NUMBER", DbType.String, dgView.Rows(i).Cells(86).Value)
                                db.AddInParameter(commProc, "@TAX_REG_NUMBER", DbType.String, dgView.Rows(i).Cells(87).Value)
                                db.AddInParameter(commProc, "@SOURCE_OF_WEALTH", DbType.String, dgView.Rows(i).Cells(88).Value)
                                db.AddInParameter(commProc, "@COMMENTS", DbType.String, dgView.Rows(i).Cells(89).Value)

                                db.AddInParameter(commProc, "@MOD_NO", DbType.Int32, intModno)
                                db.AddOutParameter(commProc, "@RET_MOD_NO", DbType.Int32, 5)

                                db.ExecuteNonQuery(commProc, trans)

                                _intModno = db.GetParameterValue(commProc, "@RET_MOD_NO")


                                ' Phone

                                Dim commProcSche As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Update")

                                commProcSche.Parameters.Clear()

                                db.AddInParameter(commProcSche, "@TPH_ID", DbType.String, PhoneSlno)
                                db.AddInParameter(commProcSche, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProcSche, "@SID", DbType.String, "P")
                                db.AddInParameter(commProcSche, "@SLNO", DbType.String, PhoneSlno)
                                db.AddInParameter(commProcSche, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(52).Value)
                                db.AddInParameter(commProcSche, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(53).Value)
                                db.AddInParameter(commProcSche, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(54).Value)
                                db.AddInParameter(commProcSche, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(55).Value)
                                db.AddInParameter(commProcSche, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(56).Value)

                                db.AddInParameter(commProcSche, "@MOD_NO", DbType.Int32, _intModno)


                                db.ExecuteNonQuery(commProcSche, trans)

                                ' Add Person Address 

                                Dim commProcAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Update")

                                commProcAdd.Parameters.Clear()

                                db.AddInParameter(commProcAdd, "@ADDRESS_ID", DbType.String, AddressSlno)
                                db.AddInParameter(commProcAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProcAdd, "@SID", DbType.String, "P")
                                db.AddInParameter(commProcAdd, "@SLNO", DbType.String, AddressSlno)
                                db.AddInParameter(commProcAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(57).Value)
                                db.AddInParameter(commProcAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(58).Value)
                                db.AddInParameter(commProcAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(59).Value)
                                db.AddInParameter(commProcAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(60).Value)
                                db.AddInParameter(commProcAdd, "@ZIP", DbType.String, dgView.Rows(i).Cells(61).Value)
                                db.AddInParameter(commProcAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(62).Value)
                                db.AddInParameter(commProcAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(63).Value)

                                db.AddInParameter(commProcAdd, "@MOD_NO", DbType.Int32, _intModno)

                                db.ExecuteNonQuery(commProcAdd, trans)

                                'Add Identification

                                Dim commProcIdent As DbCommand = db.GetStoredProcCommand("GO_DirectorIdentification_Update")

                                commProcIdent.Parameters.Clear()

                                db.AddInParameter(commProcIdent, "@IDENTIFICATION_ID", DbType.String, IDentSlno)
                                db.AddInParameter(commProcIdent, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                db.AddInParameter(commProcIdent, "@SID", DbType.String, "E")
                                db.AddInParameter(commProcIdent, "@SLNO", DbType.String, IDentSlno)
                                db.AddInParameter(commProcIdent, "@TYPE", DbType.String, dgView.Rows(i).Cells(64).Value)
                                db.AddInParameter(commProcIdent, "@NUMBER", DbType.String, dgView.Rows(i).Cells(65).Value)
                                db.AddInParameter(commProcIdent, "@ISSUE_DATE", DbType.DateTime, dgView.Rows(i).Cells(66).Value)
                                db.AddInParameter(commProcIdent, "@EXPIRY_DATE", DbType.DateTime, dgView.Rows(i).Cells(67).Value)
                                db.AddInParameter(commProcIdent, "@ISSUED_BY", DbType.String, dgView.Rows(i).Cells(69).Value)
                                db.AddInParameter(commProcIdent, "@ISSUE_COUNTRY", DbType.String, dgView.Rows(i).Cells(68).Value)

                                db.AddInParameter(commProcIdent, "@MOD_NO", DbType.Int32, _intModno)

                                db.ExecuteNonQuery(commProcIdent, trans)

                                ' Employer Phone

                                If (NullHelper.ObjectToString(dgView.Rows(i).Cells(73).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(74).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(76).Value) <> "") Then

                                    Dim commProcEMPhone As DbCommand = db.GetStoredProcCommand("GO_DirectorPhone_Update")

                                    commProcEMPhone.Parameters.Clear()

                                    db.AddInParameter(commProcEMPhone, "@TPH_ID", DbType.String, EmpPhnSlno)
                                    db.AddInParameter(commProcEMPhone, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                    db.AddInParameter(commProcEMPhone, "@SID", DbType.String, "E")
                                    db.AddInParameter(commProcEMPhone, "@SLNO", DbType.String, EmpPhnSlno)
                                    db.AddInParameter(commProcEMPhone, "@TPH_CONTACT_TYPE", DbType.String, dgView.Rows(i).Cells(73).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_COMMUNICATION_TYPE", DbType.String, dgView.Rows(i).Cells(74).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_COUNTRY_PREFIX", DbType.String, dgView.Rows(i).Cells(75).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_NUMBER", DbType.String, dgView.Rows(i).Cells(76).Value)
                                    db.AddInParameter(commProcEMPhone, "@TPH_EXTENSION", DbType.String, dgView.Rows(i).Cells(77).Value)

                                    db.AddInParameter(commProcEMPhone, "@MOD_NO", DbType.Int32, _intModno)

                                    db.ExecuteNonQuery(commProcEMPhone, trans)

                                End If


                                ' Add Employer Address 

                                If (NullHelper.ObjectToString(dgView.Rows(i).Cells(78).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(79).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(81).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(82).Value) <> "" And NullHelper.ObjectToString(dgView.Rows(i).Cells(83).Value) <> "") Then

                                    Dim commProcEMAdd As DbCommand = db.GetStoredProcCommand("GO_DirectorAddress_Update")

                                    commProcEMAdd.Parameters.Clear()

                                    db.AddInParameter(commProcEMAdd, "@ADDRESS_ID", DbType.String, EmpAddressSlno)
                                    db.AddInParameter(commProcEMAdd, "@DIRECTOR_ID", DbType.String, IntDirectorID.ToString())
                                    db.AddInParameter(commProcEMAdd, "@SID", DbType.String, "E")
                                    db.AddInParameter(commProcEMAdd, "@SLNO", DbType.String, EmpAddressSlno)
                                    db.AddInParameter(commProcEMAdd, "@ADDRESS_TYPE", DbType.String, dgView.Rows(i).Cells(78).Value)
                                    db.AddInParameter(commProcEMAdd, "@ADDRESS", DbType.String, dgView.Rows(i).Cells(79).Value)
                                    db.AddInParameter(commProcEMAdd, "@TOWN", DbType.String, dgView.Rows(i).Cells(80).Value)
                                    db.AddInParameter(commProcEMAdd, "@CITY", DbType.String, dgView.Rows(i).Cells(81).Value)
                                    db.AddInParameter(commProcEMAdd, "@ZIP", DbType.String, "")
                                    db.AddInParameter(commProcEMAdd, "@COUNTRY_CODE", DbType.String, dgView.Rows(i).Cells(82).Value)
                                    db.AddInParameter(commProcEMAdd, "@STATE", DbType.String, dgView.Rows(i).Cells(83).Value)

                                    db.AddInParameter(commProcEMAdd, "@MOD_NO", DbType.Int32, _intModno)

                                    db.ExecuteNonQuery(commProcEMAdd, trans)

                                End If





                            End If

                        End If

                        'mapping director entity

                        Dim dtEntityDirectorMap As DataTable

                        If dgView.Rows(i).Cells(1).Value.ToString = "A" Then

                            Dim commProc As DbCommand


                            strSql = "SELECT ENTITY_ID,DIRECTOR_ID,MOD_NO FROM GO_DIRECTOR_ENTITY_MAP_HIST WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND IS_AUTH = 0"


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            dtEntityDirectorMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                            If dtEntityDirectorMap.Rows.Count > 0 Then ' if history

                                strSql = "INSERT INTO GO_DIRECTOR_ENTITY_MAP_HIST(ENTITY_ID, DIRECTOR_ID, ROLE, MOD_NO, STATUS, IS_AUTH) " & _
                                         "VALUES(@P_ENTITY_ID,@P_DIRECTOR_ID,@P_ROLE,@P_MODNO,'U',0)"


                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()

                                db.AddInParameter(commProc, "@P_ENTITY_ID", DbType.String, IntEntityID)
                                db.AddInParameter(commProc, "@P_DIRECTOR_ID", DbType.String, IntDirectorID)
                                db.AddInParameter(commProc, "@P_ROLE", DbType.String, dgView.Rows(i).Cells(3).Value)

                                db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, dtEntityDirectorMap.Rows(0)("MOD_NO"))

                                db.ExecuteNonQuery(commProc, trans)

                            Else ' 

                                strSql = "SELECT ENTITY_ID,DIRECTOR_ID,MOD_NO FROM GO_DIRECTOR_ENTITY_MAP WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND STATUS = 'L' "


                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()
                                Dim dtAcOwnerMap As DataTable

                                dtAcOwnerMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                                If dtAcOwnerMap.Rows.Count > 0 Then ' if last data

                                    intMod = NullHelper.ToIntNum(dtAcOwnerMap.Rows(0)("MOD_NO"))


                                    strSql = "INSERT INTO GO_DIRECTOR_ENTITY_MAP_HIST(ENTITY_ID, DIRECTOR_ID, ROLE, MOD_NO, STATUS, IS_AUTH) " & _
                                             "SELECT ENTITY_ID, DIRECTOR_ID, ROLE,@P_MODNO,'U',0 " & _
                                             "FROM GO_DIRECTOR_ENTITY_MAP " & _
                                             " WHERE ENTITY_ID ='" & IntEntityID & "' AND STATUS = 'L' "


                                    commProc = db.GetSqlStringCommand(strSql)

                                    commProc.Parameters.Clear()

                                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)

                                    db.ExecuteNonQuery(commProc, trans)


                                    strSql = "INSERT INTO GO_DIRECTOR_ENTITY_MAP_HIST(ENTITY_ID, DIRECTOR_ID, ROLE, MOD_NO, STATUS, IS_AUTH) " & _
                                          "VALUES(@P_ENTITY_ID,@P_DIRECTOR_ID,@P_ROLE,@P_MODNO,'U',0)"


                                    commProc = db.GetSqlStringCommand(strSql)

                                    commProc.Parameters.Clear()

                                    db.AddInParameter(commProc, "@P_ENTITY_ID", DbType.String, IntEntityID)
                                    db.AddInParameter(commProc, "@P_DIRECTOR_ID", DbType.String, IntDirectorID)
                                    db.AddInParameter(commProc, "@P_ROLE", DbType.String, dgView.Rows(i).Cells(3).Value)

                                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)

                                    db.ExecuteNonQuery(commProc, trans)

                                Else ' if no data


                                    strSql = "INSERT INTO GO_DIRECTOR_ENTITY_MAP_HIST(ENTITY_ID, DIRECTOR_ID, ROLE, MOD_NO, STATUS, IS_AUTH) " & _
                                          "VALUES(@P_ENTITY_ID,@P_DIRECTOR_ID,@P_ROLE,@P_MODNO,'U',0)"


                                    commProc = db.GetSqlStringCommand(strSql)

                                    commProc.Parameters.Clear()

                                    db.AddInParameter(commProc, "@P_ENTITY_ID", DbType.String, IntEntityID)
                                    db.AddInParameter(commProc, "@P_DIRECTOR_ID", DbType.String, IntDirectorID)
                                    db.AddInParameter(commProc, "@P_ROLE", DbType.String, dgView.Rows(i).Cells(3).Value)

                                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, 1)

                                    db.ExecuteNonQuery(commProc, trans)



                                End If



                            End If



                        ElseIf dgView.Rows(i).Cells(1).Value.ToString = "R" Then

                            Dim commProc As DbCommand


                            strSql = "SELECT ENTITY_ID,DIRECTOR_ID,MOD_NO FROM GO_DIRECTOR_ENTITY_MAP_HIST WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND DIRECTOR_ID='" & IntDirectorID.ToString & "' AND IS_AUTH = 0"


                            commProc = db.GetSqlStringCommand(strSql)

                            commProc.Parameters.Clear()

                            dtEntityDirectorMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                            If dtEntityDirectorMap.Rows.Count > 0 Then ' if history

                                intMod = NullHelper.ToIntNum(dtEntityDirectorMap.Rows(0)("MOD_NO"))

                                strSql = "DELETE FROM GO_DIRECTOR_ENTITY_MAP_HIST WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND  DIRECTOR_ID='" & IntDirectorID.ToString & "' And MOD_NO='" & intMod & "' AND IS_AUTH = 0"

                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()

                                db.ExecuteNonQuery(commProc, trans)

                            Else

                                strSql = "SELECT ENTITY_ID,DIRECTOR_ID,MOD_NO FROM GO_DIRECTOR_ENTITY_MAP_HIST WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND DIRECTOR_ID='" & IntDirectorID.ToString & "' AND STATUS = 'L' "


                                commProc = db.GetSqlStringCommand(strSql)

                                commProc.Parameters.Clear()
                                Dim dtAcOwnerMap As DataTable

                                dtAcOwnerMap = db.ExecuteDataSet(commProc, trans).Tables(0)

                                If dtAcOwnerMap.Rows.Count > 0 Then

                                    intMod = NullHelper.ToIntNum(dtAcOwnerMap.Rows(0)("MOD_NO"))


                                    strSql = "INSERT INTO GO_DIRECTOR_ENTITY_MAP_HIST(ENTITY_ID, DIRECTOR_ID, ROLE, MOD_NO, STATUS, IS_AUTH) " & _
                                             "SELECT ENTITY_ID, DIRECTOR_ID, ROLE,@P_MODNO,'U',0 " & _
                                             "FROM GO_DIRECTOR_ENTITY_MAP " & _
                                             " WHERE ENTITY_ID ='" & IntEntityID & "' AND STATUS = 'L' "


                                    commProc = db.GetSqlStringCommand(strSql)

                                    commProc.Parameters.Clear()

                                    db.AddInParameter(commProc, "@P_MODNO", DbType.Int32, intMod + 1)

                                    db.ExecuteNonQuery(commProc, trans)


                                    strSql = "DELETE FROM GO_DIRECTOR_ENTITY_MAP_HIST WHERE ENTITY_ID='" & IntEntityID.ToString & "' AND  DIRECTOR_ID='" & IntDirectorID.ToString & "' And MOD_NO='" & intMod & "' AND IS_AUTH = 0"

                                    commProc = db.GetSqlStringCommand(strSql)

                                    commProc.Parameters.Clear()

                                    db.ExecuteNonQuery(commProc, trans)


                                End If



                            End If



                        End If




                    End If ' director

                    DirectorGroup = dgView.Rows(i).Cells(32).Value ' Director group end


                End If 'Entity group
                EntityGroup = dgView.Rows(i).Cells(0).Value ' entity group end


            Next




            tStatus = TransState.Add




            trans.Commit()

            


        End Using

        log_message = " Uploaded : Entity & Director Information "
        Logger.system_log(log_message)



        Return tStatus

    End Function


    Private Sub FrmImportEntityInformation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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



        'If flagRequireField = True Then

        'btnPrepareEntity.Enabled = False
        ' Else

        'btnPrepareEntity.Enabled = True
        ' End If


    End Sub

    Private Sub btnPrepareEntity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrepareEntity.Click
        Dim tState As TransState

        lblToolStatus.Text = ""

        Try
            If MessageBox.Show("Do you really want to Save?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                If CheckValidData() Then

                    tState = SaveData()

                    If tState = TransState.Add Then

                        lblToolStatus.Text = "!! Information Updated Successfully !!"

                        MessageBox.Show("Information Updated Successfull." & Environment.NewLine & _
                                        "** Separate authorization needed from Account Info, Entity Info, Director info Form", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

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