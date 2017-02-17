'
'Author             : Fahad Khan
'Purpose            : Export goAML Information
'Creation date      : 11-nov-2013
'Stored Procedure(s):  

Imports CTR.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Data.SqlClient



Public Class FrmExportGoAML

#Region "Global Variables"
    Dim _formName As String = "ToolsExportGoAML"
    Dim opt As SecForm = New SecForm(_formName)
    Dim log_message As String = ""
    Dim _ReportPersonId As Integer = 0
    Dim _ReportLoc As Integer = 0

    Dim _ProcessSuccess As Boolean = False
    Dim ErrorRow As Integer = 0
#End Region

#Region "User Define Function "

    Private Sub DisableFields()
        txtId.ReadOnly = True
        txtBranch.ReadOnly = True
        txtSubmission.ReadOnly = True
        txtReporttype.ReadOnly = True
        txtCurrency.ReadOnly = True
        txtReason.ReadOnly = True
        txtAction.ReadOnly = True
        txtIndicator.ReadOnly = True
        txtSubmissionDate.ReadOnly = True
    End Sub

    Private Sub LoadReportData()


        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim ds As New DataSet

            Dim commProc As DbCommand = db.GetStoredProcCommand("GO_Report_GetReport")

            commProc.Parameters.Clear()

            ds = db.ExecuteDataSet(commProc)

            If ds.Tables(0).Rows.Count = 1 Then



                txtId.Text = ds.Tables(0).Rows(0)("RENTITY_ID").ToString()
                txtBranch.Text = ds.Tables(0).Rows(0)("RENTITY_BRANCH").ToString()
                txtSubmission.Text = ds.Tables(0).Rows(0)("SUBMISSION_CODE").ToString()
                txtReporttype.Text = ds.Tables(0).Rows(0)("REPORT_CODE").ToString()
                txtEntityRef.Text = ds.Tables(0).Rows(0)("ENTITY_REFERENCE").ToString()
                txtFiuRef.Text = ds.Tables(0).Rows(0)("FIU_REF_NUMBER").ToString()
                txtCurrency.Text = ds.Tables(0).Rows(0)("CURRENCY_CODE_LOCAL").ToString()
                txtReason.Text = ds.Tables(0).Rows(0)("REASON").ToString()
                txtAction.Text = ds.Tables(0).Rows(0)("ACTION").ToString()
                txtIndicator.Text = ds.Tables(0).Rows(0)("REPORT_INDICATOR").ToString()

                _ReportPersonId = ds.Tables(0).Rows(0)("REPORTING_PERSON")
                _ReportLoc = ds.Tables(0).Rows(0)("LOCATION")



            Else

                MessageBox.Show("Please Active atleast one Report!!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Function CheckData() As Boolean

        If txtDateFrom.Text.Trim() = "/  /" Then
            MessageBox.Show("Date From required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtDateFrom.Focus()
            Return False
        End If

        If txtDateTo.Text.Trim() = "/  /" Then
            MessageBox.Show("Date To required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtDateTo.Focus()
            Return False
        End If

        'If txtTransacrionNumber.Text.Trim() = "" Then

        '    MessageBox.Show("Number of Transaction required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    txtTransacrionNumber.Focus()
        '    Return False

        'End If

        'If txtXmlFileName.Text.Trim() = "" Then

        '    MessageBox.Show("Xml File Name required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    txtXmlFileName.Focus()
        '    Return False

        'End If

        Return True

    End Function

    'Private Sub WriteX(ByVal xwriter As XmlTextWriter, ByVal id As String, ByVal val As String, ByVal fNill As Boolean)
    '    If fNill = True Or val.Trim() <> "" Then
    '        xwriter.WriteStartElement(id)
    '        xwriter.WriteString(val.Trim())
    '        xwriter.WriteEndElement()
    '    End If


    'End Sub

    Private Sub WriteX(ByVal xwriter As XmlTextWriter, ByVal id As String, ByVal val As Object, ByVal fNill As Boolean)

        Dim strVal As String = ""

        If (val Is Nothing) Or (val Is DBNull.Value) Then
            strVal = ""
        Else
            strVal = val.ToString().Trim()
        End If


        If fNill = True Or strVal <> "" Then
            xwriter.WriteStartElement(id)
            xwriter.WriteString(strVal)
            xwriter.WriteEndElement()
        End If


    End Sub

    Private Sub ExportXML()

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)
        Dim dtReportPerson As DataTable


        Dim xmlwrt As New XmlTextWriter(txtFolderPath.Text.Trim() & "\export.xml", System.Text.Encoding.UTF8)

        Try

        

            xmlwrt.Formatting = Formatting.Indented
            xmlwrt.Indentation = 3

            xmlwrt.WriteStartElement("report")

            xmlwrt.WriteStartElement("rentity_id")
            xmlwrt.WriteString(txtId.Text.Trim())
            xmlwrt.WriteEndElement() 'rentity_id

            xmlwrt.WriteStartElement("rentity_branch")
            xmlwrt.WriteString(txtBranch.Text.Trim())
            xmlwrt.WriteEndElement() 'rentity_branch

            xmlwrt.WriteStartElement("submission_code")
            xmlwrt.WriteString(txtSubmission.Text.Trim())
            xmlwrt.WriteEndElement() 'submission_code

            xmlwrt.WriteStartElement("report_code")
            xmlwrt.WriteString(txtReporttype.Text.Trim())
            xmlwrt.WriteEndElement() 'report_code

            xmlwrt.WriteStartElement("entity_reference")
            xmlwrt.WriteString(txtEntityRef.Text.Trim())
            xmlwrt.WriteEndElement() 'entity_reference

            xmlwrt.WriteStartElement("submission_date")
            xmlwrt.WriteString(NullHelper.DateToXML(NullHelper.StringToDate(txtSubmissionDate.Text)))
            xmlwrt.WriteEndElement() 'submission_date

            xmlwrt.WriteStartElement("currency_code_local")
            xmlwrt.WriteString("BDT")
            xmlwrt.WriteEndElement() 'currency_code_local

            '--- start reporting_person

            dtReportPerson = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_REPORT_PERSON WHERE [STATUS]='L' AND PERSON_ID=" & _ReportPersonId.ToString()).Tables(0)

            If dtReportPerson.Rows.Count = 0 Then
                xmlwrt.Close()
                MessageBox.Show("", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub

            End If

            xmlwrt.WriteStartElement("reporting_person")

            WriteX(xmlwrt, "title", dtReportPerson.Rows(0)("TITLE"), True)
            WriteX(xmlwrt, "first_name", dtReportPerson.Rows(0)("FIRST_NAME"), True)
            WriteX(xmlwrt, "middle_name", dtReportPerson.Rows(0)("MIDDLE_NAME"), False)
            WriteX(xmlwrt, "last_name", dtReportPerson.Rows(0)("LAST_NAME"), True)

            xmlwrt.WriteStartElement("phones")

            Dim dtReportPersonPhone As DataTable
            '-- SID=P for Personal Phone
            dtReportPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM dbo.GO_PERSON_PHONE WHERE [STATUS]='L' AND [SID]='P' AND PERSON_ID=" & _ReportPersonId.ToString()).Tables(0)

            For i = 0 To dtReportPersonPhone.Rows.Count - 1
                xmlwrt.WriteStartElement("phone")
                WriteX(xmlwrt, "tph_contact_type", dtReportPersonPhone.Rows(i)("TPH_CONTACT_TYPE"), True)
                WriteX(xmlwrt, "tph_communication_type", dtReportPersonPhone.Rows(i)("TPH_COMMUNICATION_TYPE"), True)
                WriteX(xmlwrt, "tph_number", dtReportPersonPhone.Rows(i)("TPH_NUMBER"), True)
                xmlwrt.WriteEndElement() 'phone

            Next

            xmlwrt.WriteEndElement() 'phones

            xmlwrt.WriteStartElement("addresses")

            Dim dtReportPersonAdd As DataTable
            '-- SID=P for Personal Address
            dtReportPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_PERSON_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND PERSON_ID=" & _ReportPersonId.ToString()).Tables(0)

            For i = 0 To dtReportPersonAdd.Rows.Count - 1
                xmlwrt.WriteStartElement("address")
                WriteX(xmlwrt, "address_type", dtReportPersonAdd.Rows(i)("ADDRESS_TYPE"), True)
                WriteX(xmlwrt, "address", dtReportPersonAdd.Rows(i)("ADDRESS"), True)
                WriteX(xmlwrt, "city", dtReportPersonAdd.Rows(i)("CITY"), True)
                WriteX(xmlwrt, "country_code", dtReportPersonAdd.Rows(i)("COUNTRY_CODE"), True)
                WriteX(xmlwrt, "state", dtReportPersonAdd.Rows(i)("STATE"), True)
                xmlwrt.WriteEndElement() 'address

            Next

            xmlwrt.WriteEndElement() 'addresses

            WriteX(xmlwrt, "email", dtReportPerson.Rows(0)("EMAIL"), True)

            xmlwrt.WriteEndElement() 'reporting_person

            '--- end reporting_person

            '--- start location
            xmlwrt.WriteStartElement("location")
            Dim dtReportLoc As DataTable
            dtReportLoc = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM dbo.GO_T_ADDRESS WHERE [STATUS]='L' AND ADDRESS_ID=" & _ReportLoc.ToString()).Tables(0)

            WriteX(xmlwrt, "address_type", dtReportLoc.Rows(0)("ADDRESS_TYPE"), True)
            WriteX(xmlwrt, "address", dtReportLoc.Rows(0)("ADDRESS"), True)
            WriteX(xmlwrt, "city", dtReportLoc.Rows(0)("CITY"), True)
            WriteX(xmlwrt, "zip", dtReportLoc.Rows(0)("ZIP"), True)
            WriteX(xmlwrt, "country_code", dtReportLoc.Rows(0)("COUNTRY_CODE"), True)
            WriteX(xmlwrt, "state", dtReportLoc.Rows(0)("STATE"), True)


            xmlwrt.WriteEndElement() 'location
            '--- end location


            Dim dtTransation As DataTable
            Dim dtTranAcc As DataTable
            Dim dtTranAccGo As DataTable
            Dim dtTranBranch As DataTable
            Dim dtAccSig As DataTable
            Dim dtAccSigDetGo As DataTable
            Dim dtAccSigDet As DataTable
            Dim dtSigPersonPhone As DataTable
            Dim dtSigPersonAdd As DataTable
            Dim dtSigPersonIdent As DataTable


            Dim commTrans As DbCommand

            commTrans = db.GetSqlStringCommand("SELECT * FROM GO_TRANSACTION WHERE [STATUS]='L' AND " & _
                                               "DATE_TRANSACTION>=@P_TXN_DATE_FROM AND DATE_TRANSACTION<=@P_TXN_DATE_FROM")

            commTrans.Parameters.Clear()

            db.AddInParameter(commTrans, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
            db.AddInParameter(commTrans, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

            dtTransation = db.ExecuteDataSet(commTrans).Tables(0)

            For i = 0 To dtTransation.Rows.Count - 1
                '--- start transaction
                xmlwrt.WriteStartElement("transaction")

                WriteX(xmlwrt, "transactionnumber", dtTransation.Rows(i)("TRANSACTIONNUMBER"), True)
                WriteX(xmlwrt, "internal_ref_number", dtTransation.Rows(i)("INTERNAL_REF_NUMBER"), True)
                WriteX(xmlwrt, "transaction_location", dtTransation.Rows(i)("TRANSACTION_LOCATION"), True)
                WriteX(xmlwrt, "date_transaction", NullHelper.DateToXML(dtTransation.Rows(i)("DATE_TRANSACTION")), True)
                WriteX(xmlwrt, "transmode_code", dtTransation.Rows(i)("TRANSMODE_CODE"), True)
                WriteX(xmlwrt, "amount_local", dtTransation.Rows(i)("AMOUNT_LOCAL"), True)

                '----------------------
                '----------------------
                '----------------------

                xmlwrt.WriteStartElement("t_from_my_client")

                WriteX(xmlwrt, "from_funds_code", IIf(dtTransation.Rows(i)("DRCR_IND") = "C", "A", "W"), True)


                dtTranAcc = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)


                dtTranAccGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)

                If dtTranAccGo.Rows.Count = 0 Then
                    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                    SetProcessStatus(Environment.NewLine & "Account goAML info missing")
                    Exit Sub
                End If


                dtTranBranch = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_BANK_BRANCH WHERE [STATUS]='L' AND BRANCH_CODE='" & dtTranAcc.Rows(0)("BRANCH_CODE").ToString() & "' ").Tables(0)

                If dtTranBranch.Rows.Count = 0 Then
                    SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                    SetProcessStatus(Environment.NewLine & "Branch Information not found")
                    Exit Sub
                End If

                If NullHelper.ObjectToString(dtTranAcc.Rows(0)("AC_TITLE")) = "" Then
                    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                    SetProcessStatus(Environment.NewLine & "Account Title Missing")
                    Exit Sub
                End If

                If NullHelper.ObjectToString(dtTranBranch.Rows(0)("SWIFT_CODE")) = "" Then
                    SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                    SetProcessStatus(Environment.NewLine & "Swift code Missing")
                    Exit Sub
                End If

                If NullHelper.ObjectToString(dtTranBranch.Rows(0)("BRANCH_NAME")) = "" Then
                    SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                    SetProcessStatus(Environment.NewLine & "Branch name missing")
                    Exit Sub
                End If

                If NullHelper.ObjectToString(dtTranAccGo.Rows(0)("ACCOUNT_TYPE")) = "" Then
                    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                    SetProcessStatus(Environment.NewLine & "Account Type Missing")
                    Exit Sub
                End If



                xmlwrt.WriteStartElement("from_account")


                WriteX(xmlwrt, "institution_name", "CITIBANK N. A.", True)
                WriteX(xmlwrt, "swift", dtTranBranch.Rows(0)("SWIFT_CODE"), True)
                WriteX(xmlwrt, "non_bank_institution", "0", True)
                WriteX(xmlwrt, "branch", dtTranBranch.Rows(0)("BRANCH_NAME").ToString() & "-" & dtTranBranch.Rows(0)("BRANCH_CODE").ToString(), True)
                WriteX(xmlwrt, "account", dtTransation.Rows(i)("ACCOUNT"), True)
                WriteX(xmlwrt, "currency_code", dtTransation.Rows(i)("ACCOUNT_CURRENCY"), True)
                WriteX(xmlwrt, "account_name", dtTranAcc.Rows(0)("AC_TITLE"), True)
                WriteX(xmlwrt, "personal_account_type", dtTranAccGo.Rows(0)("ACCOUNT_TYPE"), True)



                dtAccSig = db.ExecuteDataSet(CommandType.Text, "SELECT OWNER_CODE FROM FIU_TRANS_AC_OWNER WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "'").Tables(0)

                If dtAccSig.Rows.Count = 0 Then
                    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                    SetProcessStatus(Environment.NewLine & "Owner Info Missing")
                    Exit Sub
                End If



                For j = 0 To dtAccSig.Rows.Count - 1

                    'role check

                    If NullHelper.ObjectToString(dtAccSig.Rows(j)("ROLE_TYPE")) = "" Then
                        SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Role Missing(Account Owner Mapping)")
                    End If

                    dtAccSigDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                    dtAccSigDetGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                    If dtAccSigDetGo.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Info Missing (goAML)")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("FIRST_NAME")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner First Name Missing")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("LAST_NAME")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Last Name Missing")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("DOB")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner DOB Missing")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("NATIONALITY1")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Nationality1 missing")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("RESIDENCE")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Residence missing (goAML)")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("OCP_CODE")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner occupation missing (goAML)")
                        Exit Sub
                    End If

                    xmlwrt.WriteStartElement("signatory")

                    WriteX(xmlwrt, "is_primary", "1", True)

                    xmlwrt.WriteStartElement("t_person")

                    WriteX(xmlwrt, "gender", dtAccSigDet.Rows(0)("GENDER"), True)
                    WriteX(xmlwrt, "first_name", dtAccSigDetGo.Rows(0)("FIRST_NAME"), True)
                    WriteX(xmlwrt, "last_name", dtAccSigDetGo.Rows(0)("LAST_NAME"), True)
                    WriteX(xmlwrt, "birthdate", NullHelper.DateToXML(dtAccSigDet.Rows(0)("DOB")), True)
                    WriteX(xmlwrt, "mothers_name", dtAccSigDet.Rows(0)("OWNER_MOTHER"), True)
                    WriteX(xmlwrt, "nationality1", dtAccSigDetGo.Rows(0)("NATIONALITY1"), True)
                    WriteX(xmlwrt, "residence", dtAccSigDetGo.Rows(0)("RESIDENCE"), True)
                    '----------------------

                    xmlwrt.WriteStartElement("phones")


                    '-- SID=P For Personal Phone 
                    dtSigPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                    If dtSigPersonPhone.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Phone Missing")
                        Exit Sub
                    End If

                    For k = 0 To dtReportPersonPhone.Rows.Count - 1
                        xmlwrt.WriteStartElement("phone")
                        WriteX(xmlwrt, "tph_contact_type", dtSigPersonPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                        WriteX(xmlwrt, "tph_communication_type", dtSigPersonPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                        WriteX(xmlwrt, "tph_number", dtSigPersonPhone.Rows(k)("TPH_NUMBER"), True)
                        xmlwrt.WriteEndElement() 'phone

                    Next

                    xmlwrt.WriteEndElement() 'phones

                    xmlwrt.WriteStartElement("addresses")


                    '-- SID=P for Personal Address
                    dtSigPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                    If dtSigPersonAdd.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Address Missing")
                        Exit Sub
                    End If

                    For k = 0 To dtSigPersonAdd.Rows.Count - 1
                        xmlwrt.WriteStartElement("address")
                        WriteX(xmlwrt, "address_type", dtSigPersonAdd.Rows(k)("ADDRESS_TYPE"), True)
                        WriteX(xmlwrt, "address", dtSigPersonAdd.Rows(k)("ADDRESS"), True)
                        WriteX(xmlwrt, "city", dtSigPersonAdd.Rows(k)("CITY"), True)
                        WriteX(xmlwrt, "country_code", dtSigPersonAdd.Rows(k)("COUNTRY_CODE"), True)
                        WriteX(xmlwrt, "state", dtSigPersonAdd.Rows(k)("STATE"), True)
                        xmlwrt.WriteEndElement() 'address

                    Next

                    xmlwrt.WriteEndElement() 'addresses

                    '---------------------

                    WriteX(xmlwrt, "email", dtAccSigDetGo.Rows(0)("EMAIL"), True)
                    WriteX(xmlwrt, "occupation", dtAccSigDetGo.Rows(0)("OCP_CODE"), True)


                    '--SID=E used for personal indentification
                    dtSigPersonIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                    If dtSigPersonIdent.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Indentification Missing")
                        Exit Sub
                    End If

                    For k = 0 To dtSigPersonIdent.Rows.Count - 1
                        xmlwrt.WriteStartElement("identification")
                        WriteX(xmlwrt, "type", dtSigPersonIdent.Rows(k)("TYPE"), True)
                        WriteX(xmlwrt, "number", dtSigPersonIdent.Rows(k)("NUMBER"), True)
                        WriteX(xmlwrt, "issue_country", dtSigPersonIdent.Rows(k)("ISSUE_COUNTRY"), True)

                        xmlwrt.WriteEndElement() 'identification

                    Next

                    xmlwrt.WriteEndElement() 't_person

                    xmlwrt.WriteEndElement() 'signatory


                Next j




                'WriteX(xmlwrt, "status_code", dtTranAccGo.Rows(0)("STATUS_CODE"), True)

                WriteX(xmlwrt, "status_code", "A", True)

                xmlwrt.WriteEndElement() 'from_account

                WriteX(xmlwrt, "from_country", "BD", True)

                xmlwrt.WriteEndElement() 't_from_my_client

                '------------------------
                '------------------------
                '-------------------------







                xmlwrt.WriteStartElement("t_to_my_client")

                WriteX(xmlwrt, "to_funds_code", IIf(dtTransation.Rows(i)("DRCR_IND") = "C", "A", "W"), True)


                dtTranAcc = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)


                dtTranAccGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)

                If dtTranAccGo.Rows.Count = 0 Then
                    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                    SetProcessStatus(Environment.NewLine & "Account goAML info missing")
                    Exit Sub
                End If


                dtTranBranch = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_BANK_BRANCH WHERE [STATUS]='L' AND BRANCH_CODE='" & dtTranAcc.Rows(0)("BRANCH_CODE").ToString() & "' ").Tables(0)

                If dtTranBranch.Rows.Count = 0 Then
                    SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                    SetProcessStatus(Environment.NewLine & "Branch Information not found")
                    Exit Sub
                End If

                If NullHelper.ObjectToString(dtTranAcc.Rows(0)("AC_TITLE")) = "" Then
                    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                    SetProcessStatus(Environment.NewLine & "Account Title Missing")
                    Exit Sub
                End If

                If NullHelper.ObjectToString(dtTranBranch.Rows(0)("SWIFT_CODE")) = "" Then
                    SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                    SetProcessStatus(Environment.NewLine & "Swift code Missing")
                    Exit Sub
                End If

                If NullHelper.ObjectToString(dtTranBranch.Rows(0)("BRANCH_NAME")) = "" Then
                    SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                    SetProcessStatus(Environment.NewLine & "Branch name missing")
                    Exit Sub
                End If

                If NullHelper.ObjectToString(dtTranAccGo.Rows(0)("ACCOUNT_TYPE")) = "" Then
                    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                    SetProcessStatus(Environment.NewLine & "Account Type Missing")
                    Exit Sub
                End If



                xmlwrt.WriteStartElement("to_account")


                WriteX(xmlwrt, "institution_name", "CITIBANK N. A.", True)
                WriteX(xmlwrt, "swift", dtTranBranch.Rows(0)("SWIFT_CODE"), True)
                WriteX(xmlwrt, "non_bank_institution", "0", True)
                WriteX(xmlwrt, "branch", dtTranBranch.Rows(0)("BRANCH_NAME").ToString() & "-" & dtTranBranch.Rows(0)("BRANCH_CODE").ToString(), True)
                WriteX(xmlwrt, "account", dtTransation.Rows(i)("ACCOUNT"), True)
                WriteX(xmlwrt, "currency_code", dtTransation.Rows(i)("ACCOUNT_CURRENCY"), True)
                WriteX(xmlwrt, "account_name", dtTranAcc.Rows(0)("AC_TITLE"), True)
                WriteX(xmlwrt, "personal_account_type", dtTranAccGo.Rows(0)("ACCOUNT_TYPE"), True)



                dtAccSig = db.ExecuteDataSet(CommandType.Text, "SELECT OWNER_CODE FROM FIU_TRANS_AC_OWNER WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "'").Tables(0)

                If dtAccSig.Rows.Count = 0 Then
                    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                    SetProcessStatus(Environment.NewLine & "Owner Info Missing")
                    Exit Sub
                End If



                For j = 0 To dtAccSig.Rows.Count - 1


                    dtAccSigDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                    dtAccSigDetGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                    If dtAccSigDetGo.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Info Missing (goAML)")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("FIRST_NAME")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner First Name Missing")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("LAST_NAME")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Last Name Missing")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("DOB")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner DOB Missing")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("NATIONALITY1")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Nationality1 missing")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("RESIDENCE")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Residence missing (goAML)")
                        Exit Sub
                    End If

                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("OCP_CODE")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner occupation missing (goAML)")
                        Exit Sub
                    End If

                    xmlwrt.WriteStartElement("signatory")

                    WriteX(xmlwrt, "is_primary", "1", True)

                    xmlwrt.WriteStartElement("t_person")

                    WriteX(xmlwrt, "gender", dtAccSigDet.Rows(0)("GENDER"), True)
                    WriteX(xmlwrt, "first_name", dtAccSigDetGo.Rows(0)("FIRST_NAME"), True)
                    WriteX(xmlwrt, "last_name", dtAccSigDetGo.Rows(0)("LAST_NAME"), True)
                    WriteX(xmlwrt, "birthdate", NullHelper.DateToXML(dtAccSigDet.Rows(0)("DOB")), True)
                    WriteX(xmlwrt, "mothers_name", dtAccSigDet.Rows(0)("OWNER_MOTHER"), True)
                    WriteX(xmlwrt, "nationality1", dtAccSigDetGo.Rows(0)("NATIONALITY1"), True)
                    WriteX(xmlwrt, "residence", dtAccSigDetGo.Rows(0)("RESIDENCE"), True)
                    '----------------------

                    xmlwrt.WriteStartElement("phones")


                    '-- SID=P For Personal Phone 
                    dtSigPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                    If dtSigPersonPhone.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Phone Missing")
                        Exit Sub
                    End If

                    For k = 0 To dtReportPersonPhone.Rows.Count - 1
                        xmlwrt.WriteStartElement("phone")
                        WriteX(xmlwrt, "tph_contact_type", dtSigPersonPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                        WriteX(xmlwrt, "tph_communication_type", dtSigPersonPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                        WriteX(xmlwrt, "tph_number", dtSigPersonPhone.Rows(k)("TPH_NUMBER"), True)
                        xmlwrt.WriteEndElement() 'phone

                    Next

                    xmlwrt.WriteEndElement() 'phones

                    xmlwrt.WriteStartElement("addresses")


                    '-- SID=P for Personal Address
                    dtSigPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                    If dtSigPersonAdd.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Address Missing")
                        Exit Sub
                    End If

                    For k = 0 To dtSigPersonAdd.Rows.Count - 1
                        xmlwrt.WriteStartElement("address")
                        WriteX(xmlwrt, "address_type", dtSigPersonAdd.Rows(k)("ADDRESS_TYPE"), True)
                        WriteX(xmlwrt, "address", dtSigPersonAdd.Rows(k)("ADDRESS"), True)
                        WriteX(xmlwrt, "city", dtSigPersonAdd.Rows(k)("CITY"), True)
                        WriteX(xmlwrt, "country_code", dtSigPersonAdd.Rows(k)("COUNTRY_CODE"), True)
                        WriteX(xmlwrt, "state", dtSigPersonAdd.Rows(k)("STATE"), True)
                        xmlwrt.WriteEndElement() 'address

                    Next

                    xmlwrt.WriteEndElement() 'addresses

                    '---------------------

                    WriteX(xmlwrt, "email", dtAccSigDetGo.Rows(0)("EMAIL"), True)
                    WriteX(xmlwrt, "occupation", dtAccSigDetGo.Rows(0)("OCP_CODE"), True)


                    '--SID=E used for personal indentification
                    dtSigPersonIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                    If dtSigPersonIdent.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        SetProcessStatus(Environment.NewLine & "Owner Indentification Missing")
                        Exit Sub
                    End If

                    For k = 0 To dtSigPersonIdent.Rows.Count - 1
                        xmlwrt.WriteStartElement("identification")
                        WriteX(xmlwrt, "type", dtSigPersonIdent.Rows(k)("TYPE"), True)
                        WriteX(xmlwrt, "number", dtSigPersonIdent.Rows(k)("NUMBER"), True)
                        WriteX(xmlwrt, "issue_country", dtSigPersonIdent.Rows(k)("ISSUE_COUNTRY"), True)

                        xmlwrt.WriteEndElement() 'identification

                    Next

                    xmlwrt.WriteEndElement() 't_person

                    xmlwrt.WriteEndElement() 'signatory


                Next j




                'WriteX(xmlwrt, "status_code", dtTranAccGo.Rows(0)("STATUS_CODE"), True)
                WriteX(xmlwrt, "status_code", "A", True)

                xmlwrt.WriteEndElement() 'to_account

                WriteX(xmlwrt, "to_country", "BD", True)

                xmlwrt.WriteEndElement() 't_to_my_client


                '----------------------
                '----------------------
                '----------------------

                xmlwrt.WriteEndElement() 'transaction
                '--- end transaction
            Next i



            xmlwrt.WriteStartElement("report_indicators")
            WriteX(xmlwrt, "indicator", txtIndicator.Text.Trim(), True)
            xmlwrt.WriteEndElement() 'report_indicators

            xmlwrt.WriteEndElement() 'report

            _ProcessSuccess = True


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        xmlwrt.Close()


    End Sub


    Private Function ExportXML(ByVal xFileName As String, ByVal db As SqlDatabase, ByVal dtTransation As DataTable, ByVal rowStart As Integer, ByVal rowEnd As Integer) As Boolean

        Dim errLevel As String = "0"

        Dim flagProcessSuccess = False
        'Dim db As New SqlDatabase(CommonAppSet.ConnStr)
        Dim dtReportPerson As DataTable


        If xFileName.Trim() = "" Then
            SetProcessStatus(Environment.NewLine & "XML filename missing")
            Return False
        End If

        Dim xmlwrt As New XmlTextWriter(txtFolderPath.Text.Trim() & "\" & xFileName.Trim() & ".xml", System.Text.Encoding.UTF8)

        Try



            xmlwrt.Formatting = Formatting.Indented
            xmlwrt.Indentation = 3

            xmlwrt.WriteStartElement("report")

            xmlwrt.WriteStartElement("rentity_id")
            xmlwrt.WriteString(txtId.Text.Trim())
            xmlwrt.WriteEndElement() 'rentity_id

            xmlwrt.WriteStartElement("rentity_branch")
            xmlwrt.WriteString(txtBranch.Text.Trim())
            xmlwrt.WriteEndElement() 'rentity_branch

            xmlwrt.WriteStartElement("submission_code")
            xmlwrt.WriteString(txtSubmission.Text.Trim())
            xmlwrt.WriteEndElement() 'submission_code

            xmlwrt.WriteStartElement("report_code")
            xmlwrt.WriteString(txtReporttype.Text.Trim())
            xmlwrt.WriteEndElement() 'report_code

            xmlwrt.WriteStartElement("entity_reference")
            xmlwrt.WriteString(txtEntityRef.Text.Trim())
            xmlwrt.WriteEndElement() 'entity_reference

            xmlwrt.WriteStartElement("fiu_ref_number")
            xmlwrt.WriteString(txtFiuRef.Text.Trim())
            xmlwrt.WriteEndElement() 'fiu_reference

            xmlwrt.WriteStartElement("submission_date")
            xmlwrt.WriteString(NullHelper.DateToXML(NullHelper.StringToDate(txtSubmissionDate.Text)))
            xmlwrt.WriteEndElement() 'submission_date

            xmlwrt.WriteStartElement("currency_code_local")
            xmlwrt.WriteString("BDT")
            xmlwrt.WriteEndElement() 'currency_code_local

            '--- start reporting_person

            dtReportPerson = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_REPORT_PERSON WHERE [STATUS]='L' AND PERSON_ID=" & _ReportPersonId.ToString()).Tables(0)

            If dtReportPerson.Rows.Count = 0 Then
                xmlwrt.Close()
                MessageBox.Show("Reporting person information required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False

            End If

            If NullHelper.ObjectToString(dtReportPerson.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtReportPerson.Rows(0)("PASSPORT_NUMBER")) = "" And NullHelper.ObjectToString(dtReportPerson.Rows(0)("ID_NUMBER")) = "" Then
                SetProcessStatus(Environment.NewLine & "Reporting Person ID: " & dtReportPerson.Rows(0)("PERSON_ID"))
                SetProcessStatus(Environment.NewLine & "Reporting Person National ID, Passport, Birth Regi no Missing ")
                Return False

            End If


            '------National ID----
            'If NullHelper.ObjectToString(dtReportPerson.Rows(0)("SSN")) = "" And Len(NullHelper.ObjectToString(dtReportPerson.Rows(0)("SSN"))) <> 13 And Len(NullHelper.ObjectToString(dtReportPerson.Rows(0)("SSN"))) <> 17 Then
            '    SetProcessStatus(Environment.NewLine & "Reporting Person ID: " & dtReportPerson.Rows(0)("PERSON_ID"))
            '    SetProcessStatus(Environment.NewLine & "Reporting Person National ID Must be 13 or 17 digits ")
            '    Return False

            'End If

            '------National ID end----
            errLevel = "1"
            xmlwrt.WriteStartElement("reporting_person")

            WriteX(xmlwrt, "gender", dtReportPerson.Rows(0)("GENDER"), False)
            WriteX(xmlwrt, "title", dtReportPerson.Rows(0)("TITLE"), True)
            WriteX(xmlwrt, "first_name", dtReportPerson.Rows(0)("FIRST_NAME"), True)
            WriteX(xmlwrt, "middle_name", dtReportPerson.Rows(0)("MIDDLE_NAME"), True)
            WriteX(xmlwrt, "prefix", dtReportPerson.Rows(0)("PREFIX"), False)
            WriteX(xmlwrt, "last_name", dtReportPerson.Rows(0)("LAST_NAME"), True)
            WriteX(xmlwrt, "birthdate", NullHelper.DateToXML(dtReportPerson.Rows(0)("BIRTHDATE")), False)
            WriteX(xmlwrt, "birth_place", dtReportPerson.Rows(0)("BIRTH_PLACE"), False)
            WriteX(xmlwrt, "mothers_name", dtReportPerson.Rows(0)("MOTHERS_NAME"), False)
            WriteX(xmlwrt, "alias", dtReportPerson.Rows(0)("ALIAS"), False)
            WriteX(xmlwrt, "ssn", dtReportPerson.Rows(0)("SSN"), False)

            If Not NullHelper.ObjectToString(dtReportPerson.Rows(0)("PASSPORT_NUMBER")) = "" Then
                WriteX(xmlwrt, "passport_number", dtReportPerson.Rows(0)("PASSPORT_NUMBER"), False)
                WriteX(xmlwrt, "passport_country", dtReportPerson.Rows(0)("PASSPORT_COUNTRY"), False)
            End If


            WriteX(xmlwrt, "id_number", dtReportPerson.Rows(0)("ID_NUMBER"), False)
            WriteX(xmlwrt, "nationality1", dtReportPerson.Rows(0)("NATIONALITY1"), False)
            WriteX(xmlwrt, "nationality2", dtReportPerson.Rows(0)("NATIONALITY2"), False)
            WriteX(xmlwrt, "nationality3", dtReportPerson.Rows(0)("NATIONALITY3"), False)
            WriteX(xmlwrt, "residence", dtReportPerson.Rows(0)("RESIDENCE"), False)

            errLevel = "2"
            xmlwrt.WriteStartElement("phones")

            Dim dtReportPersonPhone As DataTable
            '-- SID=P for Personal Phone
            dtReportPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM dbo.GO_PERSON_PHONE WHERE [STATUS]='L' AND [SID]='P' AND PERSON_ID=" & _ReportPersonId.ToString()).Tables(0)

            For i = 0 To dtReportPersonPhone.Rows.Count - 1

                xmlwrt.WriteStartElement("phone")

                WriteX(xmlwrt, "tph_contact_type", dtReportPersonPhone.Rows(i)("TPH_CONTACT_TYPE"), True)
                WriteX(xmlwrt, "tph_communication_type", dtReportPersonPhone.Rows(i)("TPH_COMMUNICATION_TYPE"), True)
                WriteX(xmlwrt, "tph_country_prefix", dtReportPersonPhone.Rows(i)("TPH_COUNTRY_PREFIX"), False)
                WriteX(xmlwrt, "tph_number", dtReportPersonPhone.Rows(i)("TPH_NUMBER"), True)
                WriteX(xmlwrt, "tph_extension", dtReportPersonPhone.Rows(i)("TPH_EXTENSION"), False)


                xmlwrt.WriteEndElement() 'phone

            Next

            xmlwrt.WriteEndElement() 'phones
            errLevel = "3"
            xmlwrt.WriteStartElement("addresses")

            Dim dtReportPersonAdd As DataTable
            '-- SID=P for Personal Address
            dtReportPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_PERSON_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND PERSON_ID=" & _ReportPersonId.ToString()).Tables(0)

            For i = 0 To dtReportPersonAdd.Rows.Count - 1
                xmlwrt.WriteStartElement("address")

                WriteX(xmlwrt, "address_type", dtReportPersonAdd.Rows(i)("ADDRESS_TYPE"), True)
                WriteX(xmlwrt, "address", dtReportPersonAdd.Rows(i)("ADDRESS"), True)
                WriteX(xmlwrt, "town", dtReportPersonAdd.Rows(i)("TOWN"), False)
                WriteX(xmlwrt, "city", dtReportPersonAdd.Rows(i)("CITY"), True)
                WriteX(xmlwrt, "zip", dtReportPersonAdd.Rows(i)("ZIP"), False)
                WriteX(xmlwrt, "country_code", dtReportPersonAdd.Rows(i)("COUNTRY_CODE"), True)
                WriteX(xmlwrt, "state", dtReportPersonAdd.Rows(i)("STATE"), True)

                xmlwrt.WriteEndElement() 'address

            Next

            xmlwrt.WriteEndElement() 'addresses
            errLevel = "4"
            WriteX(xmlwrt, "email", dtReportPerson.Rows(0)("EMAIL"), True)

            WriteX(xmlwrt, "occupation", dtReportPerson.Rows(0)("OCCUPATION"), False)
            WriteX(xmlwrt, "employer_name", dtReportPerson.Rows(0)("EMPLOYER_NAME"), False)
            WriteX(xmlwrt, "tax_number", dtReportPerson.Rows(0)("TAX_NUMBER"), False)
            WriteX(xmlwrt, "tax_reg_number", dtReportPerson.Rows(0)("TAX_REG_NUMBER"), False)
            WriteX(xmlwrt, "source_of_wealth", dtReportPerson.Rows(0)("SOURCE_OF_WEALTH"), False)

            xmlwrt.WriteEndElement() 'reporting_person

            '--- end reporting_person
            errLevel = "5"
            '--- start location
            xmlwrt.WriteStartElement("location")
            Dim dtReportLoc As DataTable
            dtReportLoc = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM dbo.GO_T_ADDRESS WHERE [STATUS]='L' AND ADDRESS_ID=" & _ReportLoc.ToString()).Tables(0)

            WriteX(xmlwrt, "address_type", dtReportLoc.Rows(0)("ADDRESS_TYPE"), True)
            WriteX(xmlwrt, "address", dtReportLoc.Rows(0)("ADDRESS"), True)
            WriteX(xmlwrt, "town", dtReportLoc.Rows(0)("TOWN"), False)
            WriteX(xmlwrt, "city", dtReportLoc.Rows(0)("CITY"), True)
            WriteX(xmlwrt, "zip", dtReportLoc.Rows(0)("ZIP"), True)
            WriteX(xmlwrt, "country_code", dtReportLoc.Rows(0)("COUNTRY_CODE"), True)
            WriteX(xmlwrt, "state", dtReportLoc.Rows(0)("STATE"), True)


            xmlwrt.WriteEndElement() 'location

            '--- end location

            'xmlwrt.WriteStartElement("reason")
            'xmlwrt.WriteString(txtReason.Text)
            'xmlwrt.WriteEndElement() 'reason

            'xmlwrt.WriteStartElement("action")
            'xmlwrt.WriteString(txtAction.Text)
            'xmlwrt.WriteEndElement() 'Action

            'Dim dtTransation As DataTable
            Dim dtTranAcc As DataTable
            Dim dtTranAccGo As DataTable
            Dim dtTranBranch As DataTable
            Dim dtAccEntity As DataTable
            Dim dtAccEntityPhone As DataTable
            Dim dtAccEntityAdd As DataTable
            Dim dtAccEntityDir As DataTable
            Dim dtAccEntityDirDet As DataTable
            Dim dtAccEntityDirPhone As DataTable
            Dim dtAccEntityDirAdd As DataTable

            Dim dtAccEntityDirEmployerPhone As DataTable
            Dim dtAccEntityDirEmployerAdd As DataTable

            Dim dtAccEntityDirIdent As DataTable

            Dim dtBearer As DataTable

            Dim dtBearerPhone As DataTable
            Dim dtBearerAdd As DataTable

            Dim dtBearerEmployerPhone As DataTable
            Dim dtBearerEmployerAdd As DataTable

            Dim dtBearerIdent As DataTable


            Dim dtAccSig As DataTable
            Dim dtAccSigDetGo As DataTable
            Dim dtAccSigDet As DataTable
            Dim dtSigPersonPhone As DataTable
            Dim dtSigPersonAdd As DataTable

            Dim dtSigEmployerPhone As DataTable
            Dim dtSigEmployerAdd As DataTable

            Dim dtSigPersonIdent As DataTable


            'Dim commTrans As DbCommand

            'commTrans = db.GetSqlStringCommand("SELECT * FROM GO_TRANSACTION WHERE [STATUS]='L' AND " & _
            '                                   "DATE_TRANSACTION>=@P_TXN_DATE_FROM AND DATE_TRANSACTION<=@P_TXN_DATE_FROM")

            'commTrans.Parameters.Clear()

            'db.AddInParameter(commTrans, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
            'db.AddInParameter(commTrans, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

            'dtTransation = db.ExecuteDataSet(commTrans).Tables(0)
            errLevel = "6"
            For i = rowStart To rowEnd 'dtTransation.Rows.Count - 1
                '--- start transaction
                xmlwrt.WriteStartElement("transaction")

                WriteX(xmlwrt, "transactionnumber", dtTransation.Rows(i)("TRANSACTIONNUMBER"), True)
                WriteX(xmlwrt, "internal_ref_number", dtTransation.Rows(i)("INTERNAL_REF_NUMBER"), True)
                WriteX(xmlwrt, "transaction_location", dtTransation.Rows(i)("TRANSACTION_LOCATION"), True)
                WriteX(xmlwrt, "transaction_description", dtTransation.Rows(i)("TRANSACTION_DESCRIPTION"), False)

                WriteX(xmlwrt, "date_transaction", NullHelper.DateToXML(dtTransation.Rows(i)("DATE_TRANSACTION")), True)
                WriteX(xmlwrt, "teller", dtTransation.Rows(i)("TELLER"), False)
                WriteX(xmlwrt, "authorized", dtTransation.Rows(i)("AUTHORIZED"), False)

                If Not dtTransation.Rows(i)("VALUE_DATE") Is DBNull.Value Then

                    If dtTransation.Rows(i)("VALUE_DATE") < dtTransation.Rows(i)("DATE_TRANSACTION") Then
                        WriteX(xmlwrt, "late_deposit", "True", True)
                        WriteX(xmlwrt, "date_posting", NullHelper.DateToXML(dtTransation.Rows(i)("VALUE_DATE")), True)
                        WriteX(xmlwrt, "value_date", NullHelper.DateToXML(dtTransation.Rows(i)("VALUE_DATE")), True)
                    End If

                End If

                'WriteX(xmlwrt, "value_date", NullHelper.DateToXML(dtTransation.Rows(i)("DATE_TRANSACTION")), True)

                WriteX(xmlwrt, "transmode_code", dtTransation.Rows(i)("TRANSMODE_CODE"), True)
                WriteX(xmlwrt, "transmode_comment", dtTransation.Rows(i)("TRANSMODE_COMMENTS"), False)

                WriteX(xmlwrt, "amount_local", dtTransation.Rows(i)("AMOUNT_LOCAL"), True)

                '----------------------
                '----------------------
                '----------------------

                If dtTransation.Rows(i)("FROM_TYPE") = "1" Then 'my client
                    xmlwrt.WriteStartElement("t_from_my_client")
                Else 'not my client
                    xmlwrt.WriteStartElement("t_from")
                End If


                WriteX(xmlwrt, "from_funds_code", IIf(dtTransation.Rows(i)("DRCR_IND") = "C", "A", "W"), True)

                If (dtTransation.Rows(i)("ACCOUNT_CURRENCY") <> "BDT" And dtTransation.Rows(i)("FOREIGN_AMOUNT") > 0) Then

                    xmlwrt.WriteStartElement("from_foreign_currency")

                    WriteX(xmlwrt, "foreign_currency_code", dtTransation.Rows(i)("ACCOUNT_CURRENCY"), False)
                    WriteX(xmlwrt, "foreign_amount", dtTransation.Rows(i)("FOREIGN_AMOUNT"), False)
                    WriteX(xmlwrt, "foreign_exchange_rate", NullHelper.ToDecNum(dtTransation.Rows(i)("FOREIGN_AMOUNT") / dtTransation.Rows(i)("AMOUNT_LOCAL")), False)


                    xmlwrt.WriteEndElement()

                End If
                errLevel = "7"

                If dtTransation.Rows(i)("FROM_FLAG") = "A" Then 'From Account


                    dtTranAcc = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)
                    errLevel = "8"

                    dtTranAccGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)

                    If dtTranAccGo.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        SetProcessStatus(Environment.NewLine & "Account goAML info missing")
                        Return False
                    End If
                    errLevel = "9"

                    dtTranBranch = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_BANK_BRANCH WHERE [STATUS]='L' AND BRANCH_CODE='" & dtTranAcc.Rows(0)("BRANCH_CODE").ToString() & "' ").Tables(0)

                    If dtTranBranch.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                        SetProcessStatus(Environment.NewLine & "Branch Information not found")
                        Return False
                    End If

                    If NullHelper.ObjectToString(dtTranAcc.Rows(0)("AC_TITLE")) = "" Then
                        SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        SetProcessStatus(Environment.NewLine & "Account Title Missing")
                        Return False
                    End If

                    If NullHelper.ObjectToString(dtTranBranch.Rows(0)("SWIFT_CODE")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                        SetProcessStatus(Environment.NewLine & "Swift code Missing")
                        Return False
                    End If

                    If NullHelper.ObjectToString(dtTranBranch.Rows(0)("BRANCH_NAME")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                        SetProcessStatus(Environment.NewLine & "Branch name missing")
                        Return False
                    End If

                    'If NullHelper.ObjectToString(dtTransation.Rows(i)("ACC_TYPE")) = "" Then
                    '    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                    '    SetProcessStatus(Environment.NewLine & "Account Type Missing")
                    '    Return False
                    'End If


                    errLevel = "10"
                    xmlwrt.WriteStartElement("from_account")


                    WriteX(xmlwrt, "institution_name", "CITIBANK N. A.", True)
                    WriteX(xmlwrt, "swift", dtTranBranch.Rows(0)("SWIFT_CODE"), True)
                    WriteX(xmlwrt, "non_bank_institution", "0", True)
                    WriteX(xmlwrt, "branch", dtTranBranch.Rows(0)("BRANCH_NAME").ToString() & "-" & dtTranBranch.Rows(0)("BRANCH_CODE").ToString(), True)
                    WriteX(xmlwrt, "account", dtTransation.Rows(i)("ACCOUNT"), True)
                    WriteX(xmlwrt, "currency_code", dtTransation.Rows(i)("ACCOUNT_CURRENCY"), True)
                    WriteX(xmlwrt, "account_name", dtTranAcc.Rows(0)("AC_TITLE"), True)
                    WriteX(xmlwrt, "iban", dtTranAccGo.Rows(0)("IBAN"), False)
                    WriteX(xmlwrt, "client_number", dtTranAccGo.Rows(0)("CLIENT_NUMBER"), False)

                    If NullHelper.ObjectToString(dtTransation.Rows(i)("ACC_TYPE")) = "" Then
                        WriteX(xmlwrt, "personal_account_type", "D", True)
                    Else
                        WriteX(xmlwrt, "personal_account_type", dtTransation.Rows(i)("ACC_TYPE"), True)
                    End If


                    '---- entity information
                    errLevel = "11"
                    dtAccEntity = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY WHERE [STATUS]='L' AND ENTITY_ID='" & NullHelper.ObjectToString(dtTranAccGo.Rows(0)("ENTITY_ID")) & "' ").Tables(0)


                    If dtAccEntity.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                        SetProcessStatus(Environment.NewLine & "Entity info Missing")
                        Return False
                    End If

                    If dtAccEntity.Rows.Count > 0 Then

                        xmlwrt.WriteStartElement("t_entity")

                        WriteX(xmlwrt, "name", dtAccEntity.Rows(0)("NAME"), True)
                        WriteX(xmlwrt, "commercial_name", dtAccEntity.Rows(0)("COMMERTIAL_NAME"), False)
                        WriteX(xmlwrt, "incorporation_legal_form", dtAccEntity.Rows(0)("INCORPORATION_LEGAL_FORM"), False)
                        WriteX(xmlwrt, "incorporation_number", dtAccEntity.Rows(0)("INCORPORATION_NUMBER"), True)
                        WriteX(xmlwrt, "business", dtAccEntity.Rows(0)("BUSINESS"), True)
                        '-- entity phone
                        errLevel = "12"
                        xmlwrt.WriteStartElement("phones")

                        '-- SID=P For Personal Phone 
                        dtAccEntityPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_PHONE WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityPhone.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                            SetProcessStatus(Environment.NewLine & "Entity Phone Missing")
                            Return False
                        End If

                        For k = 0 To dtAccEntityPhone.Rows.Count - 1
                            xmlwrt.WriteStartElement("phone")
                            WriteX(xmlwrt, "tph_contact_type", dtAccEntityPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                            WriteX(xmlwrt, "tph_communication_type", dtAccEntityPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                            WriteX(xmlwrt, "tph_country_prefix", dtAccEntityPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                            WriteX(xmlwrt, "tph_number", dtAccEntityPhone.Rows(k)("TPH_NUMBER"), True)
                            WriteX(xmlwrt, "tph_extension", dtAccEntityPhone.Rows(k)("TPH_EXTENSION"), False)


                            xmlwrt.WriteEndElement() 'phone

                        Next

                        xmlwrt.WriteEndElement() 'phones


                        '-- end entity phone
                        '-- entity address

                        errLevel = "13"
                        xmlwrt.WriteStartElement("addresses")


                        '-- SID=P for Personal Address
                        dtAccEntityAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityAdd.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Account Code: " & dtTransation.Rows(i)("ACCOUNT"))
                            SetProcessStatus(Environment.NewLine & "Entity Address Missing")
                            Return False
                        End If

                        For k = 0 To dtAccEntityAdd.Rows.Count - 1
                            xmlwrt.WriteStartElement("address")
                            WriteX(xmlwrt, "address_type", dtAccEntityAdd.Rows(k)("ADDRESS_TYPE"), True)
                            WriteX(xmlwrt, "address", dtAccEntityAdd.Rows(k)("ADDRESS"), True)
                            WriteX(xmlwrt, "town", dtAccEntityAdd.Rows(k)("TOWN"), False)
                            WriteX(xmlwrt, "city", dtAccEntityAdd.Rows(k)("CITY"), True)
                            WriteX(xmlwrt, "zip", dtAccEntityAdd.Rows(k)("ZIP"), False)
                            WriteX(xmlwrt, "country_code", dtAccEntityAdd.Rows(k)("COUNTRY_CODE"), True)
                            WriteX(xmlwrt, "state", dtAccEntityAdd.Rows(k)("STATE"), True)
                            xmlwrt.WriteEndElement() 'address

                        Next

                        xmlwrt.WriteEndElement() 'addresses

                        '-- end entity address
                        errLevel = "14"

                        WriteX(xmlwrt, "email", dtAccEntity.Rows(0)("EMAIL"), False)
                        WriteX(xmlwrt, "url", dtAccEntity.Rows(0)("URL"), False)
                        WriteX(xmlwrt, "incorporation_state", dtAccEntity.Rows(0)("INCORPORATION_STATE"), True)
                        WriteX(xmlwrt, "incorporation_country_code", dtAccEntity.Rows(0)("INCORPORATION_COUNTRY"), True)


                        '-- entity director

                        errLevel = "15"
                        dtAccEntityDir = db.ExecuteDataSet(CommandType.Text, "SELECT DIRECTOR_ID,ROLE FROM GO_DIRECTOR_ENTITY_MAP WHERE [STATUS]='L' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityDir.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            SetProcessStatus(Environment.NewLine & "Director Info Missing")
                            Return False
                        End If


                        For j = 0 To dtAccEntityDir.Rows.Count - 1


                            dtAccEntityDirDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_INFO WHERE [STATUS]='L' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "' ").Tables(0)

                            errLevel = "16"
                            If dtAccEntityDirDet.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Info Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("FIRST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director First Name Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("LAST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Last Name Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("BIRTHDATE")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director DOB Missing")
                                Return False
                            End If

                            'PREFIX=spouse name,ALIAS=fathers name
                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("PREFIX")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("ALIAS")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("MOTHERS_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Father,Mother,Spouse Name Missing")
                            End If


                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("NATIONALITY1")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Nationality1 missing")
                                Return False
                            End If

                            'If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("RESIDENCE")) = "" Then
                            '    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            '    SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                            '    SetProcessStatus(Environment.NewLine & "Director Residence missing (goAML)")
                            '    Return False
                            'End If

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("PASSPORT_NUMBER")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("ID_NUMBER")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director National ID,Birth Regi No, Passport Number Missing  ")
                                Return False

                            End If


                            '--National ID start---

                            'If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("SSN")) <> "" And Len(NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("SSN"))) <> 13 And Len(NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("SSN"))) <> 17 Then
                            '    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            '    SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                            '    SetProcessStatus(Environment.NewLine & "Director National ID Must be 13 or 17 digits ")
                            '    Return False

                            'End If

                            '---National ID end----

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("OCCUPATION")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director occupation missing")
                                Return False
                            End If
                            errLevel = "17"
                            xmlwrt.WriteStartElement("director_id")

                            'xmlwrt.WriteStartElement("t_person")

                            WriteX(xmlwrt, "gender", dtAccEntityDirDet.Rows(0)("GENDER"), True)
                            WriteX(xmlwrt, "title", dtAccEntityDirDet.Rows(0)("TITLE"), False)
                            WriteX(xmlwrt, "first_name", dtAccEntityDirDet.Rows(0)("FIRST_NAME"), True)
                            WriteX(xmlwrt, "middle_name", dtAccEntityDirDet.Rows(0)("MIDDLE_NAME"), False)
                            WriteX(xmlwrt, "prefix", dtAccEntityDirDet.Rows(0)("PREFIX"), False)
                            WriteX(xmlwrt, "last_name", dtAccEntityDirDet.Rows(0)("LAST_NAME"), True)
                            WriteX(xmlwrt, "birthdate", NullHelper.DateToXML(dtAccEntityDirDet.Rows(0)("BIRTHDATE")), True)
                            WriteX(xmlwrt, "birth_place", dtAccEntityDirDet.Rows(0)("BIRTH_PLACE"), False)

                            WriteX(xmlwrt, "mothers_name", dtAccEntityDirDet.Rows(0)("MOTHERS_NAME"), True)
                            WriteX(xmlwrt, "alias", dtAccEntityDirDet.Rows(0)("ALIAS"), False)
                            WriteX(xmlwrt, "ssn", dtAccEntityDirDet.Rows(0)("SSN"), False)

                            If Not NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("PASSPORT_NUMBER")) = "" Then
                                WriteX(xmlwrt, "passport_number", dtAccEntityDirDet.Rows(0)("PASSPORT_NUMBER"), False)
                                WriteX(xmlwrt, "passport_country", dtAccEntityDirDet.Rows(0)("PASSPORT_COUNTRY"), False)
                            End If


                            WriteX(xmlwrt, "id_number", dtAccEntityDirDet.Rows(0)("ID_NUMBER"), False)
                            WriteX(xmlwrt, "nationality1", dtAccEntityDirDet.Rows(0)("NATIONALITY1"), True)
                            WriteX(xmlwrt, "nationality2", dtAccEntityDirDet.Rows(0)("NATIONALITY2"), False)
                            WriteX(xmlwrt, "nationality3", dtAccEntityDirDet.Rows(0)("NATIONALITY3"), False)

                            WriteX(xmlwrt, "residence", dtAccEntityDirDet.Rows(0)("RESIDENCE"), False)
                            '----------------------
                            errLevel = "18"
                            xmlwrt.WriteStartElement("phones")


                            '-- SID=P For Personal Phone 
                            dtAccEntityDirPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_PHONE WHERE [STATUS]='L' AND [SID]='P' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                            If dtAccEntityDirPhone.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Phone Missing")
                                Return False
                            End If

                            For k = 0 To dtAccEntityDirPhone.Rows.Count - 1
                                xmlwrt.WriteStartElement("phone")
                                WriteX(xmlwrt, "tph_contact_type", dtAccEntityDirPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                WriteX(xmlwrt, "tph_communication_type", dtAccEntityDirPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                WriteX(xmlwrt, "tph_country_prefix", dtAccEntityDirPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                WriteX(xmlwrt, "tph_number", dtAccEntityDirPhone.Rows(k)("TPH_NUMBER"), True)

                                WriteX(xmlwrt, "tph_extension", dtAccEntityDirPhone.Rows(k)("TPH_EXTENSION"), False)


                                xmlwrt.WriteEndElement() 'phone

                            Next

                            xmlwrt.WriteEndElement() 'phones
                            errLevel = "19"
                            xmlwrt.WriteStartElement("addresses")


                            '-- SID=P for Personal Address
                            dtAccEntityDirAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                            If dtAccEntityDirAdd.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Address Missing")
                                Return False
                            End If

                            For k = 0 To dtAccEntityDirAdd.Rows.Count - 1
                                xmlwrt.WriteStartElement("address")
                                WriteX(xmlwrt, "address_type", dtAccEntityDirAdd.Rows(k)("ADDRESS_TYPE"), True)
                                WriteX(xmlwrt, "address", dtAccEntityDirAdd.Rows(k)("ADDRESS"), True)
                                WriteX(xmlwrt, "town", dtAccEntityDirAdd.Rows(k)("TOWN"), False)
                                WriteX(xmlwrt, "city", dtAccEntityDirAdd.Rows(k)("CITY"), True)
                                WriteX(xmlwrt, "zip", dtAccEntityDirAdd.Rows(k)("ZIP"), False)
                                WriteX(xmlwrt, "country_code", dtAccEntityDirAdd.Rows(k)("COUNTRY_CODE"), True)
                                WriteX(xmlwrt, "state", dtAccEntityDirAdd.Rows(k)("STATE"), True)
                                xmlwrt.WriteEndElement() 'address

                            Next

                            xmlwrt.WriteEndElement() 'addresses
                            errLevel = "20"
                            '---------------------

                            WriteX(xmlwrt, "email", dtAccEntityDirDet.Rows(0)("EMAIL"), True)
                            WriteX(xmlwrt, "occupation", dtAccEntityDirDet.Rows(0)("OCCUPATION"), True)
                            WriteX(xmlwrt, "employer_name", dtAccEntityDirDet.Rows(0)("EMPLOYER_NAME"), False)


                            'xmlwrt.WriteStartElement("employer_address_id")


                            '-- SID=E for Employer Address
                            'dtAccEntityDirEmployerAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_ADDRESS WHERE [STATUS]='L' AND [SID]='E' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)


                            'For k = 0 To dtAccEntityDirEmployerAdd.Rows.Count - 1
                            '    xmlwrt.WriteStartElement("employer_address_id")
                            '    WriteX(xmlwrt, "address_type", dtAccEntityDirEmployerAdd.Rows(k)("ADDRESS_TYPE"), False)
                            '    WriteX(xmlwrt, "address", dtAccEntityDirEmployerAdd.Rows(k)("ADDRESS"), False)
                            '    WriteX(xmlwrt, "town", dtAccEntityDirEmployerAdd.Rows(k)("TOWN"), False)
                            '    WriteX(xmlwrt, "city", dtAccEntityDirEmployerAdd.Rows(k)("CITY"), False)
                            '    WriteX(xmlwrt, "zip", dtAccEntityDirEmployerAdd.Rows(k)("ZIP"), False)
                            '    WriteX(xmlwrt, "country_code", dtAccEntityDirEmployerAdd.Rows(k)("COUNTRY_CODE"), False)
                            '    WriteX(xmlwrt, "state", dtAccEntityDirEmployerAdd.Rows(k)("STATE"), False)
                            '    xmlwrt.WriteEndElement() 'address

                            'Next

                            'xmlwrt.WriteEndElement() 'addresses

                            '---------------------


                            'xmlwrt.WriteStartElement("phones")


                            ''-- SID=E For Employer Phone 
                            'dtAccEntityDirEmployerPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_PHONE WHERE [STATUS]='L' AND [SID]='E' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)


                            'For k = 0 To dtAccEntityDirEmployerPhone.Rows.Count - 1
                            '    xmlwrt.WriteStartElement("employer_phone_id")
                            '    WriteX(xmlwrt, "tph_contact_type", dtAccEntityDirEmployerPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                            '    WriteX(xmlwrt, "tph_communication_type", dtAccEntityDirEmployerPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                            '    WriteX(xmlwrt, "tph_country_prefix", dtAccEntityDirEmployerPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                            '    WriteX(xmlwrt, "tph_number", dtAccEntityDirEmployerPhone.Rows(k)("TPH_NUMBER"), True)

                            '    WriteX(xmlwrt, "tph_extension", dtAccEntityDirEmployerPhone.Rows(k)("TPH_EXTENSION"), False)


                            '    xmlwrt.WriteEndElement() 'phone

                            'Next

                            ' xmlwrt.WriteEndElement() 'phones

                            errLevel = "21"
                            '--SID=E used for personal indentification
                            dtAccEntityDirIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                            If dtAccEntityDirIdent.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Identification Missing")
                                Return False
                            End If

                            For k = 0 To dtAccEntityDirIdent.Rows.Count - 1
                                xmlwrt.WriteStartElement("identification")
                                WriteX(xmlwrt, "type", dtAccEntityDirIdent.Rows(k)("TYPE"), True)
                                WriteX(xmlwrt, "number", dtAccEntityDirIdent.Rows(k)("NUMBER"), True)
                                WriteX(xmlwrt, "issue_date", NullHelper.DateToXML(dtAccEntityDirIdent.Rows(k)("ISSUE_DATE")), False)
                                WriteX(xmlwrt, "expiry_date", NullHelper.DateToXML(dtAccEntityDirIdent.Rows(k)("EXPIRY_DATE")), False)
                                WriteX(xmlwrt, "issued_by", dtAccEntityDirIdent.Rows(k)("ISSUED_BY"), False)
                                WriteX(xmlwrt, "issue_country", dtAccEntityDirIdent.Rows(k)("ISSUE_COUNTRY"), True)

                                xmlwrt.WriteEndElement() 'identification

                            Next
                            errLevel = "22"
                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("DECEASED")) = "1" Then
                                WriteX(xmlwrt, "deceased", "1", False)
                                WriteX(xmlwrt, "date_deceased", NullHelper.DateToXML(dtAccEntityDirDet.Rows(0)("DECEASED_DATE")), False)
                            End If


                            WriteX(xmlwrt, "tax_number", dtAccEntityDirDet.Rows(0)("TAX_NUMBER"), False)
                            WriteX(xmlwrt, "tax_reg_number", dtAccEntityDirDet.Rows(0)("TAX_REG_NUMBER"), False)
                            WriteX(xmlwrt, "source_of_wealth", dtAccEntityDirDet.Rows(0)("SOURCE_OF_WEALTH"), False)
                            WriteX(xmlwrt, "comments", dtAccEntityDirDet.Rows(0)("COMMENTS"), False)

                            'xmlwrt.WriteEndElement() 't_person

                            WriteX(xmlwrt, "role", dtAccEntityDir.Rows(j)("ROLE"), False)

                            xmlwrt.WriteEndElement() 'director_id


                        Next j

                        errLevel = "23"


                        '-- end entity director

                        WriteX(xmlwrt, "incorporation_date", NullHelper.DateToXML(dtAccEntity.Rows(0)("INCORPORATION_DATE")), True)

                        If NullHelper.ToBool(dtAccEntity.Rows(0)("BUSINESS__CLOSE")) = True Then
                            WriteX(xmlwrt, "business_closed", "1", True)
                            WriteX(xmlwrt, "date_business_closed", NullHelper.DateToXML(dtAccEntity.Rows(0)("DATE_BUSINESS_CLOSE")), False)
                        End If
                        WriteX(xmlwrt, "tax_number", dtAccEntity.Rows(0)("TAX_NUMBER"), False)
                        WriteX(xmlwrt, "tax_reg_number", dtAccEntity.Rows(0)("TAX_REG_NUMBER"), False)
                        WriteX(xmlwrt, "comments", dtAccEntity.Rows(0)("COMMENTS"), False)

                        xmlwrt.WriteEndElement()

                    End If
                    errLevel = "24"
                    '----- end entity information

                    dtAccSig = db.ExecuteDataSet(CommandType.Text, "SELECT OWNER_CODE,ROLE_TYPE FROM FIU_TRANS_AC_OWNER WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "'").Tables(0)

                    If dtAccSig.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        SetProcessStatus(Environment.NewLine & "Owner Info Missing")
                        Return False
                    End If




                    For j = 0 To dtAccSig.Rows.Count - 1

                        If NullHelper.ObjectToString(dtAccSig.Rows(j)("ROLE_TYPE")) = "" Then
                            SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Role Missing(Account Owner Mapping)")
                        End If


                        dtAccSigDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                        dtAccSigDetGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                        If dtAccSigDetGo.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Info Missing (goAML)")
                            Return False
                        End If

                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("FIRST_NAME")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner First Name Missing")
                            Return False
                        End If

                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("LAST_NAME")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Last Name Missing")
                            Return False
                        End If

                        If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("DOB")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner DOB Missing")
                            Return False
                        End If


                        If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_SPOUSE")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_FATHER")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_MOTHER")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Father,Mother,Spouse Name Missing")
                        End If




                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("NATIONALITY1")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Nationality1 missing")
                            Return False
                        End If

                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("RESIDENCE")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Residence missing (goAML)")
                            Return False
                        End If

                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("OCP_CODE")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner occupation missing (goAML)")
                            Return False
                        End If


                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("PPNO")) = "" And NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("ID_NUMBER")) = "" Then
                            SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner National ID, Birth Regi No(goAML) and Passport No (CTR) Missing")
                            Return False

                        End If

                        '--National ID start---

                        'If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) <> "" And Len(NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN"))) <> 13 And Len(NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN"))) <> 17 Then
                        '    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        '    SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        '    SetProcessStatus(Environment.NewLine & "Owner National ID Must be 13 or 17 digits(goAML) ")
                        '    Return False

                        'End If

                        '---National ID end----
                        errLevel = "25"
                        xmlwrt.WriteStartElement("signatory")

                        WriteX(xmlwrt, "is_primary", "1", True)

                        xmlwrt.WriteStartElement("t_person")

                        WriteX(xmlwrt, "gender", dtAccSigDet.Rows(0)("GENDER"), True)
                        WriteX(xmlwrt, "title", dtAccSigDetGo.Rows(0)("TITLE"), False)
                        WriteX(xmlwrt, "first_name", dtAccSigDetGo.Rows(0)("FIRST_NAME"), True)
                        WriteX(xmlwrt, "middle_name", dtAccSigDetGo.Rows(0)("MIDDLE_NAME"), False)
                        WriteX(xmlwrt, "prefix", dtAccSigDet.Rows(0)("OWNER_SPOUSE"), False)
                        WriteX(xmlwrt, "last_name", dtAccSigDetGo.Rows(0)("LAST_NAME"), True)
                        WriteX(xmlwrt, "birthdate", NullHelper.DateToXML(dtAccSigDet.Rows(0)("DOB")), True)
                        WriteX(xmlwrt, "birth_place", dtAccSigDetGo.Rows(0)("BIRTH_PLACE"), False)

                        WriteX(xmlwrt, "mothers_name", dtAccSigDet.Rows(0)("OWNER_MOTHER"), True)
                        WriteX(xmlwrt, "alias", dtAccSigDet.Rows(0)("OWNER_FATHER"), False)
                        WriteX(xmlwrt, "ssn", dtAccSigDetGo.Rows(0)("SSN"), False)
                        WriteX(xmlwrt, "passport_number", dtAccSigDet.Rows(0)("PPNO"), False)

                        WriteX(xmlwrt, "id_number", dtAccSigDetGo.Rows(0)("ID_NUMBER"), False)
                        WriteX(xmlwrt, "nationality1", dtAccSigDetGo.Rows(0)("NATIONALITY1"), True)
                        WriteX(xmlwrt, "nationality2", dtAccSigDetGo.Rows(0)("NATIONALITY2"), False)
                        WriteX(xmlwrt, "nationality3", dtAccSigDetGo.Rows(0)("NATIONALITY3"), False)

                        WriteX(xmlwrt, "residence", dtAccSigDetGo.Rows(0)("RESIDENCE"), True)
                        '----------------------
                        errLevel = "26"
                        xmlwrt.WriteStartElement("phones")


                        '-- SID=P For Personal Phone 
                        dtSigPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                        If dtSigPersonPhone.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Phone Missing")
                            Return False
                        End If

                        For k = 0 To dtSigPersonPhone.Rows.Count - 1
                            xmlwrt.WriteStartElement("phone")
                            WriteX(xmlwrt, "tph_contact_type", dtSigPersonPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                            WriteX(xmlwrt, "tph_communication_type", dtSigPersonPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                            WriteX(xmlwrt, "tph_country_prefix", dtSigPersonPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                            WriteX(xmlwrt, "tph_number", dtSigPersonPhone.Rows(k)("TPH_NUMBER"), True)

                            WriteX(xmlwrt, "tph_extension", dtSigPersonPhone.Rows(k)("TPH_EXTENSION"), False)


                            xmlwrt.WriteEndElement() 'phone

                        Next

                        xmlwrt.WriteEndElement() 'phones
                        errLevel = "27"
                        xmlwrt.WriteStartElement("addresses")


                        '-- SID=P for Personal Address
                        dtSigPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                        If dtSigPersonAdd.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Address Missing")
                            Return False
                        End If

                        For k = 0 To dtSigPersonAdd.Rows.Count - 1
                            xmlwrt.WriteStartElement("address")
                            WriteX(xmlwrt, "address_type", dtSigPersonAdd.Rows(k)("ADDRESS_TYPE"), True)
                            WriteX(xmlwrt, "address", dtSigPersonAdd.Rows(k)("ADDRESS"), True)
                            WriteX(xmlwrt, "town", dtSigPersonAdd.Rows(k)("TOWN"), False)
                            WriteX(xmlwrt, "city", dtSigPersonAdd.Rows(k)("CITY"), True)
                            WriteX(xmlwrt, "zip", dtSigPersonAdd.Rows(k)("ZIP"), False)
                            WriteX(xmlwrt, "country_code", dtSigPersonAdd.Rows(k)("COUNTRY_CODE"), True)
                            WriteX(xmlwrt, "state", dtSigPersonAdd.Rows(k)("STATE"), True)
                            xmlwrt.WriteEndElement() 'address

                        Next

                        xmlwrt.WriteEndElement() 'addresses

                        '---------------------
                        errLevel = "28"
                        WriteX(xmlwrt, "email", dtAccSigDetGo.Rows(0)("EMAIL"), True)
                        WriteX(xmlwrt, "occupation", dtAccSigDetGo.Rows(0)("OCP_CODE"), True)
                        WriteX(xmlwrt, "employer_name", dtAccSigDetGo.Rows(0)("EMPLOYER_NAME"), False)


                        ''xmlwrt.WriteStartElement("employer_address_id")

                        errLevel = "29"
                        ''-- SID=E for Employer Address
                        dtSigEmployerAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                        If dtSigEmployerAdd.Rows.Count > 0 Then
                            ' For k = 0 To dtSigEmployerAdd.Rows.Count - 1

                            For k = 0 To 0

                                xmlwrt.WriteStartElement("employer_address_id")
                                WriteX(xmlwrt, "address_type", dtSigEmployerAdd.Rows(k)("ADDRESS_TYPE"), False)
                                WriteX(xmlwrt, "address", dtSigEmployerAdd.Rows(k)("ADDRESS"), False)
                                WriteX(xmlwrt, "town", dtSigEmployerAdd.Rows(k)("TOWN"), False)
                                WriteX(xmlwrt, "city", dtSigEmployerAdd.Rows(k)("CITY"), False)
                                WriteX(xmlwrt, "zip", dtSigEmployerAdd.Rows(k)("ZIP"), False)
                                WriteX(xmlwrt, "country_code", dtSigEmployerAdd.Rows(k)("COUNTRY_CODE"), False)
                                WriteX(xmlwrt, "state", dtSigEmployerAdd.Rows(k)("STATE"), False)
                                xmlwrt.WriteEndElement() 'address

                            Next

                        End If

                        ''xmlwrt.WriteEndElement() 'addresses

                        ''---------------------


                        'xmlwrt.WriteStartElement("phones")
                        errLevel = "30"

                        ''-- SID=E For Employer Phone 
                        dtSigEmployerPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                        If dtSigEmployerPhone.Rows.Count > 0 Then

                            'For k = 0 To dtSigEmployerPhone.Rows.Count - 1
                            For k = 0 To 0

                                xmlwrt.WriteStartElement("employer_phone_id")
                                WriteX(xmlwrt, "tph_contact_type", dtSigEmployerPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                WriteX(xmlwrt, "tph_communication_type", dtSigEmployerPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                WriteX(xmlwrt, "tph_country_prefix", dtSigEmployerPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                WriteX(xmlwrt, "tph_number", dtSigEmployerPhone.Rows(k)("TPH_NUMBER"), True)

                                WriteX(xmlwrt, "tph_extension", dtSigEmployerPhone.Rows(k)("TPH_EXTENSION"), False)


                                xmlwrt.WriteEndElement() 'phone

                            Next

                        End If

                        ' xmlwrt.WriteEndElement() 'phones



                        'xmlwrt.WriteStartElement("employer_address_id")


                        ''-- SID=E for Employer Address
                        'dtSigEmployerAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)


                        'For k = 0 To dtSigEmployerAdd.Rows.Count - 1
                        '    xmlwrt.WriteStartElement("employer_address_id")
                        '    WriteX(xmlwrt, "address_type", dtSigEmployerAdd.Rows(k)("ADDRESS_TYPE"), False)
                        '    WriteX(xmlwrt, "address", dtSigEmployerAdd.Rows(k)("ADDRESS"), False)
                        '    WriteX(xmlwrt, "town", dtSigEmployerAdd.Rows(k)("TOWN"), False)
                        '    WriteX(xmlwrt, "city", dtSigEmployerAdd.Rows(k)("CITY"), False)
                        '    WriteX(xmlwrt, "zip", dtSigEmployerAdd.Rows(k)("ZIP"), False)
                        '    WriteX(xmlwrt, "country_code", dtSigEmployerAdd.Rows(k)("COUNTRY_CODE"), False)
                        '    WriteX(xmlwrt, "state", dtSigEmployerAdd.Rows(k)("STATE"), False)
                        '    xmlwrt.WriteEndElement() 'address

                        'Next

                        'xmlwrt.WriteEndElement() 'addresses

                        '---------------------


                        errLevel = "31"

                        '--SID=E used for personal indentification
                        dtSigPersonIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                        If dtSigPersonIdent.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Indentification Missing")
                            Return False
                        End If

                        For k = 0 To dtSigPersonIdent.Rows.Count - 1
                            xmlwrt.WriteStartElement("identification")
                            WriteX(xmlwrt, "type", dtSigPersonIdent.Rows(k)("TYPE"), True)
                            WriteX(xmlwrt, "number", dtSigPersonIdent.Rows(k)("NUMBER"), True)
                            WriteX(xmlwrt, "issue_date", NullHelper.DateToXML(dtSigPersonIdent.Rows(k)("ISSUE_DATE")), False)
                            WriteX(xmlwrt, "expiry_date", NullHelper.DateToXML(dtSigPersonIdent.Rows(k)("EXPIRY_DATE")), False)
                            WriteX(xmlwrt, "issued_by", dtSigPersonIdent.Rows(k)("ISSUED_BY"), False)
                            WriteX(xmlwrt, "issue_country", dtSigPersonIdent.Rows(k)("ISSUE_COUNTRY"), True)

                            xmlwrt.WriteEndElement() 'identification

                        Next
                        errLevel = "32"

                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("DECEASED")) = "1" Then
                            WriteX(xmlwrt, "deceased", "1", False)
                            WriteX(xmlwrt, "date_deceased", NullHelper.DateToXML(dtAccSigDetGo.Rows(0)("DECEASED_DATE")), False)
                        End If

                        WriteX(xmlwrt, "tax_number", dtAccSigDet.Rows(0)("TIN"), False)
                        WriteX(xmlwrt, "tax_reg_number", dtAccSigDetGo.Rows(0)("TAX_REG_NUMBER"), False)
                        WriteX(xmlwrt, "source_of_wealth", dtAccSigDetGo.Rows(0)("SOURCE_OF_WEALTH"), False)
                        WriteX(xmlwrt, "comments", dtAccSigDetGo.Rows(0)("COMMENTS"), False)

                        xmlwrt.WriteEndElement() 't_person

                        WriteX(xmlwrt, "role", dtAccSig.Rows(j)("ROLE_TYPE"), True)

                        xmlwrt.WriteEndElement() 'signatory


                    Next j

                    errLevel = "33"

                    WriteX(xmlwrt, "opened", NullHelper.DateToXML(dtTranAccGo.Rows(0)("OPENED")), False)
                    WriteX(xmlwrt, "closed", NullHelper.DateToXML(dtTranAccGo.Rows(0)("CLOSED")), False)
                    WriteX(xmlwrt, "balance", dtTransation.Rows(i)("BALANCE"), False)
                    WriteX(xmlwrt, "date_balance", NullHelper.DateToXML(dtTransation.Rows(i)("DATE_TRANSACTION")), False)

                    'WriteX(xmlwrt, "status_code", dtTranAccGo.Rows(0)("STATUS_CODE"), True)
                    WriteX(xmlwrt, "status_code", "A", True)

                    WriteX(xmlwrt, "beneficiary", dtTranAccGo.Rows(0)("BENEFICIARY"), False)
                    WriteX(xmlwrt, "beneficiary_comment", dtTranAccGo.Rows(0)("BENEFICIARY_COMMENTS"), False)
                    WriteX(xmlwrt, "comments", dtTranAccGo.Rows(0)("COMMENTS"), False)


                    xmlwrt.WriteEndElement() 'from_account

                    WriteX(xmlwrt, "from_country", "BD", True)


                    errLevel = "34"


                Else 'From Person

                    errLevel = "35"
                    '-- start from person

                    dtBearer = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_INFO WHERE [STATUS]='L' AND REFERENCE_NUMBER='" & NullHelper.ObjectToString(dtTransation.Rows(i)("FROM_PERSON")) & "'").Tables(0)

                    xmlwrt.WriteStartElement("from_person")

                    If dtBearer.Rows.Count > 0 Then
                        'original bearer information processing start

                        For j = 0 To 0

                            If NullHelper.ObjectToString(dtBearer.Rows(0)("FIRST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                SetProcessStatus(Environment.NewLine & "Depositor First Name Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtBearer.Rows(0)("LAST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                SetProcessStatus(Environment.NewLine & "Depositor Last Name Missing")
                                Return False
                            End If



                            'PREFIX=spouse name,ALIAS=fathers name
                            'If NullHelper.ObjectToString(dtBearer.Rows(0)("PREFIX")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("ALIAS")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("MOTHERS_NAME")) = "" Then
                            '    SetProcessStatus(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                            '    SetProcessStatus(Environment.NewLine & "Depositor Father,Mother,Spouse Name Missing")
                            'End If

                            'SSN=National ID, ID_NUMBER= Birth Registration Number
                            If NullHelper.ObjectToString(dtBearer.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("PASSPORT_NUMBER")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("ID_NUMBER")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                SetProcessStatus(Environment.NewLine & "Depositor National ID/Passport No/Birth Registration No Missing")
                            End If

                            '--National ID start---

                            'If NullHelper.ObjectToString(dtBearer.Rows(0)("SSN")) <> "" And Len(NullHelper.ObjectToString(dtBearer.Rows(0)("SSN"))) <> 13 And Len(NullHelper.ObjectToString(dtBearer.Rows(0)("SSN"))) <> 17 Then
                            '    SetProcessStatus(Environment.NewLine & "Ref Code" & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                            '    SetProcessStatus(Environment.NewLine & "Depositor National ID Must be 13 or 17 digits(goAML)")
                            '    Return False

                            'End If

                            '---National ID end----



                            WriteX(xmlwrt, "gender", dtBearer.Rows(0)("GENDER"), False)
                            WriteX(xmlwrt, "title", dtBearer.Rows(0)("TITLE"), False)
                            WriteX(xmlwrt, "first_name", dtBearer.Rows(0)("FIRST_NAME"), True)
                            WriteX(xmlwrt, "middle_name", dtBearer.Rows(0)("MIDDLE_NAME"), False)
                            WriteX(xmlwrt, "prefix", dtBearer.Rows(0)("PREFIX"), False)
                            WriteX(xmlwrt, "last_name", dtBearer.Rows(0)("LAST_NAME"), True)
                            WriteX(xmlwrt, "birthdate", NullHelper.DateToXML(dtBearer.Rows(0)("BIRTHDATE")), False)
                            WriteX(xmlwrt, "birth_place", dtBearer.Rows(0)("BIRTH_PLACE"), False)

                            WriteX(xmlwrt, "mothers_name", dtBearer.Rows(0)("MOTHERS_NAME"), False)
                            WriteX(xmlwrt, "alias", dtBearer.Rows(0)("ALIAS"), False)
                            WriteX(xmlwrt, "ssn", dtBearer.Rows(0)("SSN"), False)

                            If Not NullHelper.ObjectToString(dtBearer.Rows(0)("PASSPORT_NUMBER")) = "" Then
                                WriteX(xmlwrt, "passport_number", dtBearer.Rows(0)("PASSPORT_NUMBER"), False)
                                WriteX(xmlwrt, "passport_country", dtBearer.Rows(0)("PASSPORT_COUNTRY"), False)
                            End If

                            If Not NullHelper.ObjectToString(dtBearer.Rows(0)("ID_NUMBER")) = "" Then
                                WriteX(xmlwrt, "id_number", dtBearer.Rows(0)("ID_NUMBER"), False)
                            ElseIf Not NullHelper.ObjectToString(dtBearer.Rows(0)("PASSPORT_NUMBER")) = "" Then
                                WriteX(xmlwrt, "id_number", dtBearer.Rows(0)("PASSPORT_NUMBER"), False)
                            Else
                                WriteX(xmlwrt, "id_number", dtBearer.Rows(0)("SSN"), False)
                            End If

                            'WriteX(xmlwrt, "id_number", dtBearer.Rows(0)("ID_NUMBER"), False)

                            WriteX(xmlwrt, "nationality1", dtBearer.Rows(0)("NATIONALITY1"), False)
                            WriteX(xmlwrt, "nationality2", dtBearer.Rows(0)("NATIONALITY2"), False)
                            WriteX(xmlwrt, "nationality3", dtBearer.Rows(0)("NATIONALITY3"), False)

                            WriteX(xmlwrt, "residence", dtBearer.Rows(0)("RESIDENCE"), False)
                            '----------------------
                            errLevel = "36"
                            xmlwrt.WriteStartElement("phones")


                            '-- SID=P For Personal Phone 
                            dtBearerPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND PERSON_ID='" & dtBearer.Rows(0)("PERSON_ID").ToString() & "'").Tables(0)
                            '81271
                            For k = 0 To dtBearerPhone.Rows.Count - 1
                                xmlwrt.WriteStartElement("phone")
                                WriteX(xmlwrt, "tph_contact_type", dtBearerPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                WriteX(xmlwrt, "tph_communication_type", dtBearerPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                WriteX(xmlwrt, "tph_country_prefix", dtBearerPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                WriteX(xmlwrt, "tph_number", dtBearerPhone.Rows(k)("TPH_NUMBER"), True)

                                WriteX(xmlwrt, "tph_extension", dtBearerPhone.Rows(k)("TPH_EXTENSION"), False)


                                xmlwrt.WriteEndElement() 'phone

                            Next

                            xmlwrt.WriteEndElement() 'phones
                            errLevel = "37"
                            xmlwrt.WriteStartElement("addresses")


                            '-- SID=P for Personal Address
                            dtBearerAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND PERSON_ID='" & dtBearer.Rows(0)("PERSON_ID").ToString() & "'").Tables(0)

                            For k = 0 To dtBearerAdd.Rows.Count - 1
                                xmlwrt.WriteStartElement("address")
                                WriteX(xmlwrt, "address_type", dtBearerAdd.Rows(k)("ADDRESS_TYPE"), True)
                                WriteX(xmlwrt, "address", dtBearerAdd.Rows(k)("ADDRESS"), True)
                                WriteX(xmlwrt, "town", dtBearerAdd.Rows(k)("TOWN"), False)
                                WriteX(xmlwrt, "city", dtBearerAdd.Rows(k)("CITY"), True)
                                WriteX(xmlwrt, "zip", dtBearerAdd.Rows(k)("ZIP"), False)
                                WriteX(xmlwrt, "country_code", dtBearerAdd.Rows(k)("COUNTRY_CODE"), True)
                                WriteX(xmlwrt, "state", dtBearerAdd.Rows(k)("STATE"), True)
                                xmlwrt.WriteEndElement() 'address

                            Next

                            xmlwrt.WriteEndElement() 'addresses
                            errLevel = "38"
                            '---------------------

                            WriteX(xmlwrt, "email", dtBearer.Rows(0)("EMAIL"), False)
                            WriteX(xmlwrt, "occupation", dtBearer.Rows(0)("OCCUPATION"), False)
                            WriteX(xmlwrt, "employer_name", dtBearer.Rows(0)("EMPLOYER_NAME"), False)


                            'xmlwrt.WriteStartElement("employer_address_id")


                            '-- SID=E for Employer Address
                            dtBearerEmployerAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_ADDRESS WHERE [STATUS]='L' AND [SID]='E' AND PERSON_ID='" & dtBearer.Rows(0)("PERSON_ID").ToString() & "'").Tables(0)

                            If dtBearerEmployerAdd.Rows.Count > 0 Then

                                'For k = 0 To dtBearerEmployerAdd.Rows.Count - 1
                                For k = 0 To 0
                                    xmlwrt.WriteStartElement("employer_address_id")
                                    WriteX(xmlwrt, "address_type", dtBearerEmployerAdd.Rows(k)("ADDRESS_TYPE"), False)
                                    WriteX(xmlwrt, "address", dtBearerEmployerAdd.Rows(k)("ADDRESS"), False)
                                    WriteX(xmlwrt, "town", dtBearerEmployerAdd.Rows(k)("TOWN"), False)
                                    WriteX(xmlwrt, "city", dtBearerEmployerAdd.Rows(k)("CITY"), False)
                                    WriteX(xmlwrt, "zip", dtBearerEmployerAdd.Rows(k)("ZIP"), False)
                                    WriteX(xmlwrt, "country_code", dtBearerEmployerAdd.Rows(k)("COUNTRY_CODE"), False)
                                    WriteX(xmlwrt, "state", dtBearerEmployerAdd.Rows(k)("STATE"), False)
                                    xmlwrt.WriteEndElement() 'address

                                Next

                            End If

                            errLevel = "39"
                            'xmlwrt.WriteEndElement() 'addresses

                            '---------------------


                            'xmlwrt.WriteStartElement("phones")


                            ''-- SID=E For Employer Phone 
                            dtBearerEmployerPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_PHONE WHERE [STATUS]='L' AND [SID]='E' AND PERSON_ID='" & dtBearer.Rows(0)("PERSON_ID").ToString() & "'").Tables(0)


                            If dtBearerEmployerPhone.Rows.Count > 0 Then
                                'SetProcessStatus(Environment.NewLine & "Bearer Code: " & dtBearer.Rows(0)("PERSON_ID"))
                                'SetProcessStatus(Environment.NewLine & "Bearer Phone Missing")
                                'Return False


                                'For k = 0 To dtBearerEmployerPhone.Rows.Count - 1
                                For k = 0 To 0
                                    xmlwrt.WriteStartElement("employer_phone_id")
                                    WriteX(xmlwrt, "tph_contact_type", dtBearerEmployerPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                    WriteX(xmlwrt, "tph_communication_type", dtBearerEmployerPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                    WriteX(xmlwrt, "tph_country_prefix", dtBearerEmployerPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                    WriteX(xmlwrt, "tph_number", dtBearerEmployerPhone.Rows(k)("TPH_NUMBER"), True)

                                    WriteX(xmlwrt, "tph_extension", dtBearerEmployerPhone.Rows(k)("TPH_EXTENSION"), False)


                                    xmlwrt.WriteEndElement() 'phone

                                Next

                            End If

                            ' xmlwrt.WriteEndElement() 'phones
                            errLevel = "40"

                            '--SID=E used for personal indentification
                            dtBearerIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND PERSON_ID='" & dtBearer.Rows(0)("PERSON_ID").ToString() & "'").Tables(0)


                            If dtBearerIdent.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Bearer Code: " & dtBearer.Rows(0)("PERSON_ID"))
                                SetProcessStatus(Environment.NewLine & "Bearer Identification Missing")
                                Return False
                            End If

                            'For k = 0 To dtBearerIdent.Rows.Count - 1
                            For k = 0 To 0
                                xmlwrt.WriteStartElement("identification")
                                WriteX(xmlwrt, "type", dtBearerIdent.Rows(k)("TYPE"), True)
                                WriteX(xmlwrt, "number", dtBearerIdent.Rows(k)("NUMBER"), True)
                                WriteX(xmlwrt, "issue_date", NullHelper.DateToXML(dtBearerIdent.Rows(k)("ISSUE_DATE")), False)
                                WriteX(xmlwrt, "expiry_date", NullHelper.DateToXML(dtBearerIdent.Rows(k)("EXPIRY_DATE")), False)
                                WriteX(xmlwrt, "issued_by", dtBearerIdent.Rows(k)("ISSUED_BY"), False)
                                WriteX(xmlwrt, "issue_country", dtBearerIdent.Rows(k)("ISSUE_COUNTRY"), True)

                                xmlwrt.WriteEndElement() 'identification

                            Next

                            errLevel = "41"
                            If NullHelper.ObjectToString(dtBearer.Rows(0)("DECEASED")) = "1" Then
                                WriteX(xmlwrt, "deceased", "1", False)
                                WriteX(xmlwrt, "date_deceased", NullHelper.DateToXML(dtBearer.Rows(0)("DECEASED_DATE")), False)
                            End If





                            WriteX(xmlwrt, "tax_number", dtBearer.Rows(0)("TAX_NUMBER"), False)
                            WriteX(xmlwrt, "tax_reg_number", dtBearer.Rows(0)("TAX_REG_NUMBER"), False)
                            WriteX(xmlwrt, "source_of_wealth", dtBearer.Rows(0)("SOURCE_OF_WEALTH"), False)
                            WriteX(xmlwrt, "comments", dtBearer.Rows(0)("COMMENTS"), False)





                        Next j
                        errLevel = "42"

                        'original bearer information processing end
                    Else
                        errLevel = "43"
                        dtAccSig = db.ExecuteDataSet(CommandType.Text, "SELECT OWNER_CODE,ROLE_TYPE FROM FIU_TRANS_AC_OWNER WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "'").Tables(0)

                        If dtAccSig.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            SetProcessStatus(Environment.NewLine & "Owner Info Missing")
                            Return False
                        End If




                        For j = 0 To 0


                            dtAccSigDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                            dtAccSigDetGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                            If dtAccSigDetGo.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Info Missing (goAML)")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("FIRST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner First Name Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("LAST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Last Name Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("DOB")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner DOB Missing")
                                Return False
                            End If


                            If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_SPOUSE")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_FATHER")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_MOTHER")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Father,Mother,Spause Name Missing")
                            End If

                            'SSN=National ID, ID_NUMBER= Birth Registration Number
                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("PPNO")) = "" And NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("ID_NUMBER")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner National ID/Passport No/Birth Registration No Missing")
                            End If

                            '--National ID start---

                            'If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) <> "" And Len(NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN"))) <> 13 And Len(NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN"))) <> 17 Then
                            '    SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            '    SetProcessStatus(Environment.NewLine & "Owner National ID Must be 13 or 17 digits(goAML)")
                            '    Return False

                            'End If
                            '--National ID End---

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("NATIONALITY1")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Nationality1 missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("RESIDENCE")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Residence missing (goAML)")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("OCP_CODE")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner occupation missing (goAML)")
                                Return False
                            End If

                            WriteX(xmlwrt, "gender", dtAccSigDet.Rows(0)("GENDER"), True)
                            WriteX(xmlwrt, "title", dtAccSigDetGo.Rows(0)("TITLE"), False)
                            WriteX(xmlwrt, "first_name", dtAccSigDetGo.Rows(0)("FIRST_NAME"), True)
                            WriteX(xmlwrt, "middle_name", dtAccSigDetGo.Rows(0)("MIDDLE_NAME"), False)
                            WriteX(xmlwrt, "prefix", dtAccSigDet.Rows(0)("OWNER_SPOUSE"), False)
                            WriteX(xmlwrt, "last_name", dtAccSigDetGo.Rows(0)("LAST_NAME"), True)
                            WriteX(xmlwrt, "birthdate", NullHelper.DateToXML(dtAccSigDet.Rows(0)("DOB")), True)
                            WriteX(xmlwrt, "birth_place", dtAccSigDetGo.Rows(0)("BIRTH_PLACE"), False)

                            WriteX(xmlwrt, "mothers_name", dtAccSigDet.Rows(0)("OWNER_MOTHER"), True)
                            WriteX(xmlwrt, "alias", dtAccSigDet.Rows(0)("OWNER_FATHER"), False)
                            WriteX(xmlwrt, "ssn", dtAccSigDetGo.Rows(0)("SSN"), False)
                            WriteX(xmlwrt, "passport_number", dtAccSigDet.Rows(0)("PPNO"), False)

                            WriteX(xmlwrt, "id_number", dtAccSigDetGo.Rows(0)("ID_NUMBER"), False)
                            WriteX(xmlwrt, "nationality1", dtAccSigDetGo.Rows(0)("NATIONALITY1"), True)
                            WriteX(xmlwrt, "nationality2", dtAccSigDetGo.Rows(0)("NATIONALITY2"), False)
                            WriteX(xmlwrt, "nationality3", dtAccSigDetGo.Rows(0)("NATIONALITY3"), False)

                            WriteX(xmlwrt, "residence", dtAccSigDetGo.Rows(0)("RESIDENCE"), True)
                            '----------------------
                            errLevel = "44"
                            xmlwrt.WriteStartElement("phones")


                            '-- SID=P For Personal Phone 
                            dtSigPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                            If dtSigPersonPhone.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Phone Missing")
                                Return False
                            End If

                            For k = 0 To dtSigPersonPhone.Rows.Count - 1
                                xmlwrt.WriteStartElement("phone")
                                WriteX(xmlwrt, "tph_contact_type", dtSigPersonPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                WriteX(xmlwrt, "tph_communication_type", dtSigPersonPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                WriteX(xmlwrt, "tph_country_prefix", dtSigPersonPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                WriteX(xmlwrt, "tph_number", dtSigPersonPhone.Rows(k)("TPH_NUMBER"), True)

                                WriteX(xmlwrt, "tph_extension", dtSigPersonPhone.Rows(k)("TPH_EXTENSION"), False)


                                xmlwrt.WriteEndElement() 'phone

                            Next

                            xmlwrt.WriteEndElement() 'phones
                            errLevel = "45"
                            xmlwrt.WriteStartElement("addresses")


                            '-- SID=P for Personal Address
                            dtSigPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                            If dtSigPersonAdd.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Address Missing")
                                Return False
                            End If

                            For k = 0 To dtSigPersonAdd.Rows.Count - 1
                                xmlwrt.WriteStartElement("address")
                                WriteX(xmlwrt, "address_type", dtSigPersonAdd.Rows(k)("ADDRESS_TYPE"), True)
                                WriteX(xmlwrt, "address", dtSigPersonAdd.Rows(k)("ADDRESS"), True)
                                WriteX(xmlwrt, "town", dtSigPersonAdd.Rows(k)("TOWN"), False)
                                WriteX(xmlwrt, "city", dtSigPersonAdd.Rows(k)("CITY"), True)
                                WriteX(xmlwrt, "zip", dtSigPersonAdd.Rows(k)("ZIP"), False)
                                WriteX(xmlwrt, "country_code", dtSigPersonAdd.Rows(k)("COUNTRY_CODE"), True)
                                WriteX(xmlwrt, "state", dtSigPersonAdd.Rows(k)("STATE"), True)
                                xmlwrt.WriteEndElement() 'address

                            Next

                            xmlwrt.WriteEndElement() 'addresses

                            '---------------------
                            errLevel = "46"
                            WriteX(xmlwrt, "email", dtAccSigDetGo.Rows(0)("EMAIL"), True)
                            WriteX(xmlwrt, "occupation", dtAccSigDetGo.Rows(0)("OCP_CODE"), True)
                            WriteX(xmlwrt, "employer_name", dtAccSigDetGo.Rows(0)("EMPLOYER_NAME"), False)


                            'xmlwrt.WriteStartElement("employer_address_id")


                            '-- SID=E for Employer Address
                            dtSigEmployerAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                            If dtSigEmployerAdd.Rows.Count > 0 Then
                                'SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                'SetProcessStatus(Environment.NewLine & "Owner Employer Address Missing")
                                'Return False



                                'For k = 0 To dtSigEmployerAdd.Rows.Count - 1
                                For k = 0 To 0
                                    xmlwrt.WriteStartElement("employer_address_id")
                                    WriteX(xmlwrt, "address_type", dtSigEmployerAdd.Rows(k)("ADDRESS_TYPE"), False)
                                    WriteX(xmlwrt, "address", dtSigEmployerAdd.Rows(k)("ADDRESS"), False)
                                    WriteX(xmlwrt, "town", dtSigEmployerAdd.Rows(k)("TOWN"), False)
                                    WriteX(xmlwrt, "city", dtSigEmployerAdd.Rows(k)("CITY"), False)
                                    WriteX(xmlwrt, "zip", dtSigEmployerAdd.Rows(k)("ZIP"), False)
                                    WriteX(xmlwrt, "country_code", dtSigEmployerAdd.Rows(k)("COUNTRY_CODE"), False)
                                    WriteX(xmlwrt, "state", dtSigEmployerAdd.Rows(k)("STATE"), False)
                                    xmlwrt.WriteEndElement() 'address

                                Next

                            End If

                            'xmlwrt.WriteEndElement() 'addresses

                            '---------------------
                            errLevel = "47"

                            'xmlwrt.WriteStartElement("phones")


                            ''-- SID=E For Employer Phone 
                            dtSigEmployerPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)


                            If dtSigEmployerPhone.Rows.Count > 0 Then

                                'SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                'SetProcessStatus(Environment.NewLine & "Owner Employer Phone Missing")
                                'Return False


                                'For k = 0 To dtSigEmployerPhone.Rows.Count - 1
                                For k = 0 To 0
                                    xmlwrt.WriteStartElement("employer_phone_id")
                                    WriteX(xmlwrt, "tph_contact_type", dtSigEmployerPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                    WriteX(xmlwrt, "tph_communication_type", dtSigEmployerPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                    WriteX(xmlwrt, "tph_country_prefix", dtSigEmployerPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                    WriteX(xmlwrt, "tph_number", dtSigEmployerPhone.Rows(k)("TPH_NUMBER"), True)

                                    WriteX(xmlwrt, "tph_extension", dtSigEmployerPhone.Rows(k)("TPH_EXTENSION"), False)


                                    xmlwrt.WriteEndElement() 'phone

                                Next

                            End If

                            ' xmlwrt.WriteEndElement() 'phones

                            errLevel = "48"


                            '--SID=E used for personal indentification
                            dtSigPersonIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                            If dtSigPersonIdent.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Indentification Missing")
                                Return False
                            End If

                            'For k = 0 To dtSigPersonIdent.Rows.Count - 1
                            For k = 0 To 0
                                xmlwrt.WriteStartElement("identification")
                                WriteX(xmlwrt, "type", dtSigPersonIdent.Rows(k)("TYPE"), True)
                                WriteX(xmlwrt, "number", dtSigPersonIdent.Rows(k)("NUMBER"), True)
                                WriteX(xmlwrt, "issue_date", NullHelper.DateToXML(dtSigPersonIdent.Rows(k)("ISSUE_DATE")), False)
                                WriteX(xmlwrt, "expiry_date", NullHelper.DateToXML(dtSigPersonIdent.Rows(k)("EXPIRY_DATE")), False)
                                WriteX(xmlwrt, "issued_by", dtSigPersonIdent.Rows(k)("ISSUED_BY"), False)
                                WriteX(xmlwrt, "issue_country", dtSigPersonIdent.Rows(k)("ISSUE_COUNTRY"), True)

                                xmlwrt.WriteEndElement() 'identification

                            Next
                            errLevel = "49"

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("DECEASED")) = "1" Then
                                WriteX(xmlwrt, "deceased", "1", False)
                                WriteX(xmlwrt, "date_deceased", NullHelper.DateToXML(dtAccSigDetGo.Rows(0)("DECEASED_DATE")), False)
                            End If

                            WriteX(xmlwrt, "tax_number", dtAccSigDet.Rows(0)("TIN"), False)
                            WriteX(xmlwrt, "tax_reg_number", dtAccSigDetGo.Rows(0)("TAX_REG_NUMBER"), False)
                            WriteX(xmlwrt, "source_of_wealth", dtAccSigDetGo.Rows(0)("SOURCE_OF_WEALTH"), False)
                            WriteX(xmlwrt, "comments", dtAccSigDetGo.Rows(0)("COMMENTS"), False)


                        Next j
            errLevel = "50"
            '------------------Entity Information--------
            'Dim dtEntityInfo As DataTable

            'dtAccEntity = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & NullHelper.ObjectToString(dtTransation.Rows(i)("ACCOUNT")) & "'").Tables(0)

            'If dtAccEntity.Rows.Count = 0 Then
            '    SetProcessStatus(Environment.NewLine & "Account : " & dtTransation.Rows(i)("ACCOUNT"))
            '    SetProcessStatus(Environment.NewLine & "Entity ID Missing(goAML)")
            '    Return False
            'End If

            'If NullHelper.ObjectToString(dtAccEntity.Rows(0)("ENTITY_ID")) = "" Then
            '    SetProcessStatus(Environment.NewLine & "Account : " & dtTransation.Rows(i)("ACCOUNT"))
            '    SetProcessStatus(Environment.NewLine & "Entity ID Missing(goAML)")
            '    Return False
            'End If
            ''ENTITY_ID

            'dtEntityInfo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY WHERE [STATUS]='L' AND ENTITY_ID='" & NullHelper.ObjectToString(dtAccEntity.Rows(0)("ENTITY_ID")) & "' ").Tables(0)

            'If dtEntityInfo.Rows.Count = 0 Then
            '    SetProcessStatus(Environment.NewLine & "Account : " & dtTransation.Rows(i)("ACCOUNT"))
            '    SetProcessStatus(Environment.NewLine & "Entity ID: " & dtAccEntity.Rows(0)("ENTITY_ID"))
            '    SetProcessStatus(Environment.NewLine & "Entity Missing(goAML)")
            '    Return False
            'End If

            'If dtEntityInfo.Rows.Count > 0 Then


            '    WriteX(xmlwrt, "first_name", dtEntityInfo.Rows(0)("NAME").Substring(0, dtEntityInfo.Rows(0)("NAME").IndexOf(" ")), True)
            '    WriteX(xmlwrt, "last_name", dtEntityInfo.Rows(0)("NAME").Substring(dtEntityInfo.Rows(0)("NAME").IndexOf(" ") + 1), True)
            '    WriteX(xmlwrt, "passport_number", dtEntityInfo.Rows(0)("INCORPORATION_NUMBER"), True)

            '    '-- entity phone

            '    xmlwrt.WriteStartElement("phones")

            '    '-- SID=P For Personal Phone 
            '    dtAccEntityPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_PHONE WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtAccEntity.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

            '    If dtAccEntityPhone.Rows.Count = 0 Then
            '        SetProcessStatus(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
            '        SetProcessStatus(Environment.NewLine & "Entity ID: " & dtAccEntity.Rows(0)("ENTITY_ID"))
            '        SetProcessStatus(Environment.NewLine & "Entity Phone Missing")
            '        Return False
            '    End If

            '    For k = 0 To dtAccEntityPhone.Rows.Count - 1
            '        xmlwrt.WriteStartElement("phone")
            '        WriteX(xmlwrt, "tph_contact_type", dtAccEntityPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
            '        WriteX(xmlwrt, "tph_communication_type", dtAccEntityPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
            '        WriteX(xmlwrt, "tph_country_prefix", dtAccEntityPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
            '        WriteX(xmlwrt, "tph_number", dtAccEntityPhone.Rows(k)("TPH_NUMBER"), True)
            '        WriteX(xmlwrt, "tph_extension", dtAccEntityPhone.Rows(k)("TPH_EXTENSION"), False)


            '        xmlwrt.WriteEndElement() 'phone

            '    Next

            '    xmlwrt.WriteEndElement() 'phones


            '    '-- end entity phone
            '    '-- entity address


            '    xmlwrt.WriteStartElement("addresses")


            '    '-- SID=P for Personal Address
            '    dtAccEntityAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtAccEntity.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

            '    If dtAccEntityAdd.Rows.Count = 0 Then
            '        SetProcessStatus(Environment.NewLine & "Account Code: " & dtTransation.Rows(i)("ACCOUNT"))
            '        SetProcessStatus(Environment.NewLine & "Entity ID: " & dtAccEntity.Rows(0)("ENTITY_ID"))
            '        SetProcessStatus(Environment.NewLine & "Entity Address Missing")
            '        Return False
            '    End If

            '    For k = 0 To dtAccEntityAdd.Rows.Count - 1
            '        xmlwrt.WriteStartElement("address")
            '        WriteX(xmlwrt, "address_type", dtAccEntityAdd.Rows(k)("ADDRESS_TYPE"), True)
            '        WriteX(xmlwrt, "address", dtAccEntityAdd.Rows(k)("ADDRESS"), True)
            '        WriteX(xmlwrt, "town", dtAccEntityAdd.Rows(k)("TOWN"), False)
            '        WriteX(xmlwrt, "city", dtAccEntityAdd.Rows(k)("CITY"), True)
            '        WriteX(xmlwrt, "zip", dtAccEntityAdd.Rows(k)("ZIP"), False)
            '        WriteX(xmlwrt, "country_code", dtAccEntityAdd.Rows(k)("COUNTRY_CODE"), True)
            '        WriteX(xmlwrt, "state", dtAccEntityAdd.Rows(k)("STATE"), True)
            '        xmlwrt.WriteEndElement() 'address

            '    Next

            '    xmlwrt.WriteEndElement() 'addresses

            '    '-- end entity address


            'End If

            '------------------Entity Information End-----




                    End If '-- end of bearer condition

                    xmlwrt.WriteEndElement() 'from_person

                    WriteX(xmlwrt, "from_country", "BD", True)

                    '-- end from person
                    errLevel = "51"

                End If

                xmlwrt.WriteEndElement() 't_from_my_client / t_from

                '------------------------
                '------------------------
                '-------------------------



                If dtTransation.Rows(i)("TO_TYPE") = "1" Then ' my client
                    xmlwrt.WriteStartElement("t_to_my_client")
                Else 'not my client
                    xmlwrt.WriteStartElement("t_to")
                End If



                WriteX(xmlwrt, "to_funds_code", IIf(dtTransation.Rows(i)("DRCR_IND") = "C", "A", "W"), True)


                If (dtTransation.Rows(i)("ACCOUNT_CURRENCY") <> "BDT" And dtTransation.Rows(i)("FOREIGN_AMOUNT") > 0) Then

                    xmlwrt.WriteStartElement("to_foreign_currency")

                    WriteX(xmlwrt, "foreign_currency_code", dtTransation.Rows(i)("ACCOUNT_CURRENCY"), False)
                    WriteX(xmlwrt, "foreign_amount", dtTransation.Rows(i)("FOREIGN_AMOUNT"), False)
                    WriteX(xmlwrt, "foreign_exchange_rate", NullHelper.ToDecNum(dtTransation.Rows(i)("FOREIGN_AMOUNT") / dtTransation.Rows(i)("AMOUNT_LOCAL")), False)


                    xmlwrt.WriteEndElement()

                End If

                errLevel = "52"

                If dtTransation.Rows(i)("TO_FLAG") = "A" Then 'To Account

                    dtTranAcc = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)


                    dtTranAccGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)

                    If dtTranAccGo.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        SetProcessStatus(Environment.NewLine & "Account goAML info missing")
                        Return False
                    End If


                    dtTranBranch = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_BANK_BRANCH WHERE [STATUS]='L' AND BRANCH_CODE='" & dtTranAcc.Rows(0)("BRANCH_CODE").ToString() & "' ").Tables(0)

                    If dtTranBranch.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                        SetProcessStatus(Environment.NewLine & "Branch Information not found")
                        Return False
                    End If

                    If NullHelper.ObjectToString(dtTranAcc.Rows(0)("AC_TITLE")) = "" Then
                        SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        SetProcessStatus(Environment.NewLine & "Account Title Missing")
                        Return False
                    End If

                    If NullHelper.ObjectToString(dtTranBranch.Rows(0)("SWIFT_CODE")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                        SetProcessStatus(Environment.NewLine & "Swift code Missing")
                        Return False
                    End If

                    If NullHelper.ObjectToString(dtTranBranch.Rows(0)("BRANCH_NAME")) = "" Then
                        SetProcessStatus(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                        SetProcessStatus(Environment.NewLine & "Branch name missing")
                        Return False
                    End If

                    'If NullHelper.ObjectToString(dtTransation.Rows(i)("ACC_TYPE")) = "" Then
                    '    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                    '    SetProcessStatus(Environment.NewLine & "Account Type Missing")
                    '    Return False
                    'End If



                    xmlwrt.WriteStartElement("to_account")


                    WriteX(xmlwrt, "institution_name", "CITIBANK N. A.", True)
                    WriteX(xmlwrt, "swift", dtTranBranch.Rows(0)("SWIFT_CODE"), True)
                    WriteX(xmlwrt, "non_bank_institution", "0", True)
                    WriteX(xmlwrt, "branch", dtTranBranch.Rows(0)("BRANCH_NAME").ToString() & "-" & dtTranBranch.Rows(0)("BRANCH_CODE").ToString(), True)
                    WriteX(xmlwrt, "account", dtTransation.Rows(i)("ACCOUNT"), True)
                    WriteX(xmlwrt, "currency_code", dtTransation.Rows(i)("ACCOUNT_CURRENCY"), True)
                    WriteX(xmlwrt, "account_name", dtTranAcc.Rows(0)("AC_TITLE"), True)
                    WriteX(xmlwrt, "iban", dtTranAccGo.Rows(0)("IBAN"), False)
                    WriteX(xmlwrt, "client_number", dtTranAccGo.Rows(0)("CLIENT_NUMBER"), False)

                    If NullHelper.ObjectToString(dtTransation.Rows(i)("ACC_TYPE")) = "" Then
                        WriteX(xmlwrt, "personal_account_type", "D", True)
                    Else
                        WriteX(xmlwrt, "personal_account_type", dtTransation.Rows(i)("ACC_TYPE"), True)
                    End If
                    'WriteX(xmlwrt, "personal_account_type", dtTransation.Rows(i)("ACC_TYPE"), True)

                    errLevel = "53"
                    '---- entity information

                    dtAccEntity = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY WHERE [STATUS]='L' AND ENTITY_ID='" & NullHelper.ObjectToString(dtTranAccGo.Rows(0)("ENTITY_ID")) & "' ").Tables(0)

                    If dtAccEntity.Rows.Count > 0 Then

                        xmlwrt.WriteStartElement("t_entity")

                        WriteX(xmlwrt, "name", dtAccEntity.Rows(0)("NAME"), True)
                        WriteX(xmlwrt, "commercial_name", dtAccEntity.Rows(0)("COMMERTIAL_NAME"), False)
                        WriteX(xmlwrt, "incorporation_legal_form", dtAccEntity.Rows(0)("INCORPORATION_LEGAL_FORM"), False)
                        WriteX(xmlwrt, "incorporation_number", dtAccEntity.Rows(0)("INCORPORATION_NUMBER"), True)
                        WriteX(xmlwrt, "business", dtAccEntity.Rows(0)("BUSINESS"), True)
                        '-- entity phone
                        errLevel = "54"
                        xmlwrt.WriteStartElement("phones")

                        '-- SID=P For Personal Phone 
                        dtAccEntityPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_PHONE WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityPhone.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                            SetProcessStatus(Environment.NewLine & "Entity Phone Missing")
                            Return False
                        End If

                        For k = 0 To dtAccEntityPhone.Rows.Count - 1
                            xmlwrt.WriteStartElement("phone")
                            WriteX(xmlwrt, "tph_contact_type", dtAccEntityPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                            WriteX(xmlwrt, "tph_communication_type", dtAccEntityPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                            WriteX(xmlwrt, "tph_country_prefix", dtAccEntityPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                            WriteX(xmlwrt, "tph_number", dtAccEntityPhone.Rows(k)("TPH_NUMBER"), True)
                            WriteX(xmlwrt, "tph_extension", dtAccEntityPhone.Rows(k)("TPH_EXTENSION"), False)


                            xmlwrt.WriteEndElement() 'phone

                        Next

                        xmlwrt.WriteEndElement() 'phones

                        errLevel = "55"
                        '-- end entity phone
                        '-- entity address


                        xmlwrt.WriteStartElement("addresses")


                        '-- SID=P for Personal Address
                        dtAccEntityAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityAdd.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Account Code: " & dtTransation.Rows(i)("ACCOUNT"))
                            SetProcessStatus(Environment.NewLine & "Entity Address Missing")
                            Return False
                        End If

                        For k = 0 To dtAccEntityAdd.Rows.Count - 1
                            xmlwrt.WriteStartElement("address")
                            WriteX(xmlwrt, "address_type", dtAccEntityAdd.Rows(k)("ADDRESS_TYPE"), True)
                            WriteX(xmlwrt, "address", dtAccEntityAdd.Rows(k)("ADDRESS"), True)
                            WriteX(xmlwrt, "town", dtAccEntityAdd.Rows(k)("TOWN"), False)
                            WriteX(xmlwrt, "city", dtAccEntityAdd.Rows(k)("CITY"), True)
                            WriteX(xmlwrt, "zip", dtAccEntityAdd.Rows(k)("ZIP"), False)
                            WriteX(xmlwrt, "country_code", dtAccEntityAdd.Rows(k)("COUNTRY_CODE"), True)
                            WriteX(xmlwrt, "state", dtAccEntityAdd.Rows(k)("STATE"), True)
                            xmlwrt.WriteEndElement() 'address

                        Next

                        xmlwrt.WriteEndElement() 'addresses
                        errLevel = "56"
                        '-- end entity address


                        WriteX(xmlwrt, "email", dtAccEntity.Rows(0)("EMAIL"), False)
                        WriteX(xmlwrt, "url", dtAccEntity.Rows(0)("URL"), False)
                        WriteX(xmlwrt, "incorporation_state", dtAccEntity.Rows(0)("INCORPORATION_STATE"), True)
                        WriteX(xmlwrt, "incorporation_country_code", dtAccEntity.Rows(0)("INCORPORATION_COUNTRY"), True)


                        '-- entity director
                        errLevel = "57"

                        dtAccEntityDir = db.ExecuteDataSet(CommandType.Text, "SELECT DIRECTOR_ID,ROLE FROM GO_DIRECTOR_ENTITY_MAP WHERE [STATUS]='L' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityDir.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            SetProcessStatus(Environment.NewLine & "Director Info Missing")
                            Return False
                        End If


                        For j = 0 To dtAccEntityDir.Rows.Count - 1


                            dtAccEntityDirDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_INFO WHERE [STATUS]='L' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "' ").Tables(0)


                            If dtAccEntityDirDet.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Info Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("FIRST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director First Name Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("LAST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Last Name Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("BIRTHDATE")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director DOB Missing")
                                Return False
                            End If

                            'PREFIX=spouse name,ALIAS=fathers name
                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("PREFIX")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("ALIAS")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("MOTHERS_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Father,Mother,Spouse Name Missing")
                            End If

                            'SSN=National ID, ID_NUMBER= Birth Registration Number
                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("PASSPORT_NUMBER")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("ID_NUMBER")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director National ID/Passport No/Birth Registration No Missing")
                            End If

                            '--National ID start---

                            'If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("SSN")) <> "" And Len(NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("SSN"))) <> 13 And Len(NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("SSN"))) <> 17 Then
                            '    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            '    SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                            '    SetProcessStatus(Environment.NewLine & "Director National ID Must be 13 or 17 digits(goAML)")
                            '    Return False

                            'End If
                            '--National ID End---


                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("NATIONALITY1")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Nationality1 missing")
                                Return False
                            End If

                            'If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("RESIDENCE")) = "" Then
                            '    SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            '    SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                            '    SetProcessStatus(Environment.NewLine & "Director Residence missing (goAML)")
                            '    Return False
                            'End If

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("OCCUPATION")) = "" Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director occupation missing")
                                Return False
                            End If

                            xmlwrt.WriteStartElement("director_id")

                            'xmlwrt.WriteStartElement("t_person")

                            WriteX(xmlwrt, "gender", dtAccEntityDirDet.Rows(0)("GENDER"), True)
                            WriteX(xmlwrt, "title", dtAccEntityDirDet.Rows(0)("TITLE"), False)
                            WriteX(xmlwrt, "first_name", dtAccEntityDirDet.Rows(0)("FIRST_NAME"), True)
                            WriteX(xmlwrt, "middle_name", dtAccEntityDirDet.Rows(0)("MIDDLE_NAME"), False)
                            WriteX(xmlwrt, "prefix", dtAccEntityDirDet.Rows(0)("PREFIX"), False)
                            WriteX(xmlwrt, "last_name", dtAccEntityDirDet.Rows(0)("LAST_NAME"), True)
                            WriteX(xmlwrt, "birthdate", NullHelper.DateToXML(dtAccEntityDirDet.Rows(0)("BIRTHDATE")), True)
                            WriteX(xmlwrt, "birth_place", dtAccEntityDirDet.Rows(0)("BIRTH_PLACE"), False)

                            WriteX(xmlwrt, "mothers_name", dtAccEntityDirDet.Rows(0)("MOTHERS_NAME"), True)
                            WriteX(xmlwrt, "alias", dtAccEntityDirDet.Rows(0)("ALIAS"), False)
                            WriteX(xmlwrt, "ssn", dtAccEntityDirDet.Rows(0)("SSN"), False)

                            If Not NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("PASSPORT_NUMBER")) = "" Then
                                WriteX(xmlwrt, "passport_number", dtAccEntityDirDet.Rows(0)("PASSPORT_NUMBER"), False)
                                WriteX(xmlwrt, "passport_country", dtAccEntityDirDet.Rows(0)("PASSPORT_COUNTRY"), False)
                            End If


                            WriteX(xmlwrt, "id_number", dtAccEntityDirDet.Rows(0)("ID_NUMBER"), False)
                            WriteX(xmlwrt, "nationality1", dtAccEntityDirDet.Rows(0)("NATIONALITY1"), True)
                            WriteX(xmlwrt, "nationality2", dtAccEntityDirDet.Rows(0)("NATIONALITY2"), False)
                            WriteX(xmlwrt, "nationality3", dtAccEntityDirDet.Rows(0)("NATIONALITY3"), False)

                            WriteX(xmlwrt, "residence", dtAccEntityDirDet.Rows(0)("RESIDENCE"), False)
                            '----------------------
                            errLevel = "58"
                            xmlwrt.WriteStartElement("phones")


                            '-- SID=P For Personal Phone 
                            dtAccEntityDirPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_PHONE WHERE [STATUS]='L' AND [SID]='P' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                            If dtAccEntityDirPhone.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Phone Missing")
                                Return False
                            End If

                            For k = 0 To dtAccEntityDirPhone.Rows.Count - 1
                                xmlwrt.WriteStartElement("phone")
                                WriteX(xmlwrt, "tph_contact_type", dtAccEntityDirPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                WriteX(xmlwrt, "tph_communication_type", dtAccEntityDirPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                WriteX(xmlwrt, "tph_country_prefix", dtAccEntityDirPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                WriteX(xmlwrt, "tph_number", dtAccEntityDirPhone.Rows(k)("TPH_NUMBER"), True)

                                WriteX(xmlwrt, "tph_extension", dtAccEntityDirPhone.Rows(k)("TPH_EXTENSION"), False)


                                xmlwrt.WriteEndElement() 'phone

                            Next

                            xmlwrt.WriteEndElement() 'phones
                            errLevel = "59"
                            xmlwrt.WriteStartElement("addresses")


                            '-- SID=P for Personal Address
                            dtAccEntityDirAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                            If dtAccEntityDirAdd.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Address Missing")
                                Return False
                            End If

                            For k = 0 To dtAccEntityDirAdd.Rows.Count - 1
                                xmlwrt.WriteStartElement("address")
                                WriteX(xmlwrt, "address_type", dtAccEntityDirAdd.Rows(k)("ADDRESS_TYPE"), True)
                                WriteX(xmlwrt, "address", dtAccEntityDirAdd.Rows(k)("ADDRESS"), True)
                                WriteX(xmlwrt, "town", dtAccEntityDirAdd.Rows(k)("TOWN"), False)
                                WriteX(xmlwrt, "city", dtAccEntityDirAdd.Rows(k)("CITY"), True)
                                WriteX(xmlwrt, "zip", dtAccEntityDirAdd.Rows(k)("ZIP"), False)
                                WriteX(xmlwrt, "country_code", dtAccEntityDirAdd.Rows(k)("COUNTRY_CODE"), True)
                                WriteX(xmlwrt, "state", dtAccEntityDirAdd.Rows(k)("STATE"), True)
                                xmlwrt.WriteEndElement() 'address

                            Next

                            xmlwrt.WriteEndElement() 'addresses
                            errLevel = "60"
                            '---------------------

                            WriteX(xmlwrt, "email", dtAccEntityDirDet.Rows(0)("EMAIL"), True)
                            WriteX(xmlwrt, "occupation", dtAccEntityDirDet.Rows(0)("OCCUPATION"), True)
                            WriteX(xmlwrt, "employer_name", dtAccEntityDirDet.Rows(0)("EMPLOYER_NAME"), False)


                            'xmlwrt.WriteStartElement("employer_address_id")


                            '-- SID=E for Employer Address
                            'dtAccEntityDirEmployerAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_ADDRESS WHERE [STATUS]='L' AND [SID]='E' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)


                            'For k = 0 To dtAccEntityDirEmployerAdd.Rows.Count - 1
                            '    xmlwrt.WriteStartElement("employer_address_id")
                            '    WriteX(xmlwrt, "address_type", dtAccEntityDirEmployerAdd.Rows(k)("ADDRESS_TYPE"), False)
                            '    WriteX(xmlwrt, "address", dtAccEntityDirEmployerAdd.Rows(k)("ADDRESS"), False)
                            '    WriteX(xmlwrt, "town", dtAccEntityDirEmployerAdd.Rows(k)("TOWN"), False)
                            '    WriteX(xmlwrt, "city", dtAccEntityDirEmployerAdd.Rows(k)("CITY"), False)
                            '    WriteX(xmlwrt, "zip", dtAccEntityDirEmployerAdd.Rows(k)("ZIP"), False)
                            '    WriteX(xmlwrt, "country_code", dtAccEntityDirEmployerAdd.Rows(k)("COUNTRY_CODE"), False)
                            '    WriteX(xmlwrt, "state", dtAccEntityDirEmployerAdd.Rows(k)("STATE"), False)
                            '    xmlwrt.WriteEndElement() 'address

                            'Next

                            'xmlwrt.WriteEndElement() 'addresses

                            '---------------------


                            'xmlwrt.WriteStartElement("phones")


                            ''-- SID=E For Employer Phone 
                            'dtAccEntityDirEmployerPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_PHONE WHERE [STATUS]='L' AND [SID]='E' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)


                            'For k = 0 To dtAccEntityDirEmployerPhone.Rows.Count - 1
                            '    xmlwrt.WriteStartElement("employer_phone_id")
                            '    WriteX(xmlwrt, "tph_contact_type", dtAccEntityDirEmployerPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                            '    WriteX(xmlwrt, "tph_communication_type", dtAccEntityDirEmployerPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                            '    WriteX(xmlwrt, "tph_country_prefix", dtAccEntityDirEmployerPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                            '    WriteX(xmlwrt, "tph_number", dtAccEntityDirEmployerPhone.Rows(k)("TPH_NUMBER"), True)

                            '    WriteX(xmlwrt, "tph_extension", dtAccEntityDirEmployerPhone.Rows(k)("TPH_EXTENSION"), False)


                            '    xmlwrt.WriteEndElement() 'phone

                            'Next

                            ' xmlwrt.WriteEndElement() 'phones

                            errLevel = "61"
                            '--SID=E used for personal indentification
                            dtAccEntityDirIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                            If dtAccEntityDirIdent.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                SetProcessStatus(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                SetProcessStatus(Environment.NewLine & "Director Identification Missing")
                                Return False
                            End If

                            For k = 0 To dtAccEntityDirIdent.Rows.Count - 1
                                xmlwrt.WriteStartElement("identification")
                                WriteX(xmlwrt, "type", dtAccEntityDirIdent.Rows(k)("TYPE"), True)
                                WriteX(xmlwrt, "number", dtAccEntityDirIdent.Rows(k)("NUMBER"), True)
                                WriteX(xmlwrt, "issue_date", NullHelper.DateToXML(dtAccEntityDirIdent.Rows(k)("ISSUE_DATE")), False)
                                WriteX(xmlwrt, "expiry_date", NullHelper.DateToXML(dtAccEntityDirIdent.Rows(k)("EXPIRY_DATE")), False)
                                WriteX(xmlwrt, "issued_by", dtAccEntityDirIdent.Rows(k)("ISSUED_BY"), False)
                                WriteX(xmlwrt, "issue_country", dtAccEntityDirIdent.Rows(k)("ISSUE_COUNTRY"), True)

                                xmlwrt.WriteEndElement() 'identification

                            Next

                            errLevel = "62"
                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("DECEASED")) = "1" Then
                                WriteX(xmlwrt, "deceased", "1", False)
                                WriteX(xmlwrt, "date_deceased", NullHelper.DateToXML(dtAccEntityDirDet.Rows(0)("DECEASED_DATE")), False)
                            End If


                            WriteX(xmlwrt, "tax_number", dtAccEntityDirDet.Rows(0)("TAX_NUMBER"), False)
                            WriteX(xmlwrt, "tax_reg_number", dtAccEntityDirDet.Rows(0)("TAX_REG_NUMBER"), False)
                            WriteX(xmlwrt, "source_of_wealth", dtAccEntityDirDet.Rows(0)("SOURCE_OF_WEALTH"), False)
                            WriteX(xmlwrt, "comments", dtAccEntityDirDet.Rows(0)("COMMENTS"), False)

                            'xmlwrt.WriteEndElement() 't_person

                            WriteX(xmlwrt, "role", dtAccEntityDir.Rows(j)("ROLE"), False)

                            xmlwrt.WriteEndElement() 'director_id


                        Next j

                        errLevel = "63"


                        '-- end entity director

                        WriteX(xmlwrt, "incorporation_date", NullHelper.DateToXML(dtAccEntity.Rows(0)("INCORPORATION_DATE")), True)
                        If NullHelper.ToBool(dtAccEntity.Rows(0)("BUSINESS__CLOSE")) = True Then
                            WriteX(xmlwrt, "business_closed", "1", True)
                            WriteX(xmlwrt, "date_business_closed", NullHelper.DateToXML(dtAccEntity.Rows(0)("DATE_BUSINESS_CLOSE")), False)
                        End If
                        WriteX(xmlwrt, "tax_number", dtAccEntity.Rows(0)("TAX_NUMBER"), False)
                        WriteX(xmlwrt, "tax_reg_number", dtAccEntity.Rows(0)("TAX_REG_NUMBER"), False)
                        WriteX(xmlwrt, "comments", dtAccEntity.Rows(0)("COMMENTS"), False)

                        xmlwrt.WriteEndElement()

                    End If

                    '----- end entity information
                    errLevel = "64"


                    dtAccSig = db.ExecuteDataSet(CommandType.Text, "SELECT OWNER_CODE,ROLE_TYPE FROM FIU_TRANS_AC_OWNER WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "'").Tables(0)

                    If dtAccSig.Rows.Count = 0 Then
                        SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        SetProcessStatus(Environment.NewLine & "Owner Info Missing")
                        Return False
                    End If



                    For j = 0 To dtAccSig.Rows.Count - 1

                        ' role check

                        If NullHelper.ObjectToString(dtAccSig.Rows(j)("ROLE_TYPE")) = "" Then
                            SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Role Missing(Account Owner Mapping)")
                        End If

                        dtAccSigDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                        dtAccSigDetGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                        If dtAccSigDetGo.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Info Missing (goAML)")
                            Return False
                        End If

                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("FIRST_NAME")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner First Name Missing")
                            Return False
                        End If

                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("LAST_NAME")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Last Name Missing")
                            Return False
                        End If

                        If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("DOB")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner DOB Missing")
                            Return False
                        End If

                        If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_SPOUSE")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_FATHER")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_MOTHER")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Father,Mother,Spause Name Missing")
                        End If

                        'SSN=National ID, ID_NUMBER= Birth Registration Number
                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("PPNO")) = "" And NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("ID_NUMBER")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Director National ID/Passport No/Birth Registration No Missing")
                        End If

                        '--National ID start---

                        'If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) <> "" And Len(NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN"))) <> 13 And Len(NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN"))) <> 17 Then
                        '    SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                        '    SetProcessStatus(Environment.NewLine & "Owner National ID Must be 13 or 17 digits(goAML)")
                        '    Return False

                        'End If
                        '--National ID End---


                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("NATIONALITY1")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Nationality1 missing")
                            Return False
                        End If

                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("RESIDENCE")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Residence missing (goAML)")
                            Return False
                        End If

                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("OCP_CODE")) = "" Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner occupation missing (goAML)")
                            Return False
                        End If

                        xmlwrt.WriteStartElement("signatory")

                        WriteX(xmlwrt, "is_primary", "1", True)

                        xmlwrt.WriteStartElement("t_person")

                        WriteX(xmlwrt, "gender", dtAccSigDet.Rows(0)("GENDER"), True)
                        WriteX(xmlwrt, "title", dtAccSigDetGo.Rows(0)("TITLE"), False)
                        WriteX(xmlwrt, "first_name", dtAccSigDetGo.Rows(0)("FIRST_NAME"), True)
                        WriteX(xmlwrt, "middle_name", dtAccSigDetGo.Rows(0)("MIDDLE_NAME"), False)
                        WriteX(xmlwrt, "prefix", dtAccSigDet.Rows(0)("OWNER_SPOUSE"), False)
                        WriteX(xmlwrt, "last_name", dtAccSigDetGo.Rows(0)("LAST_NAME"), True)
                        WriteX(xmlwrt, "birthdate", NullHelper.DateToXML(dtAccSigDet.Rows(0)("DOB")), True)
                        WriteX(xmlwrt, "birth_place", dtAccSigDetGo.Rows(0)("BIRTH_PLACE"), False)
                        WriteX(xmlwrt, "mothers_name", dtAccSigDet.Rows(0)("OWNER_MOTHER"), True)
                        WriteX(xmlwrt, "alias", dtAccSigDet.Rows(0)("OWNER_FATHER"), False)
                        WriteX(xmlwrt, "ssn", dtAccSigDetGo.Rows(0)("SSN"), False)
                        WriteX(xmlwrt, "passport_number", dtAccSigDet.Rows(0)("PPNO"), False)
                        WriteX(xmlwrt, "id_number", dtAccSigDetGo.Rows(0)("ID_NUMBER"), False)
                        WriteX(xmlwrt, "nationality1", dtAccSigDetGo.Rows(0)("NATIONALITY1"), True)
                        WriteX(xmlwrt, "nationality2", dtAccSigDetGo.Rows(0)("NATIONALITY2"), False)
                        WriteX(xmlwrt, "nationality3", dtAccSigDetGo.Rows(0)("NATIONALITY3"), False)
                        WriteX(xmlwrt, "residence", dtAccSigDetGo.Rows(0)("RESIDENCE"), True)


                        '----------------------
                        errLevel = "65"
                        xmlwrt.WriteStartElement("phones")


                        '-- SID=P For Personal Phone 
                        dtSigPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                        If dtSigPersonPhone.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Phone Missing")
                            Return False
                        End If

                        For k = 0 To dtSigPersonPhone.Rows.Count - 1
                            xmlwrt.WriteStartElement("phone")
                            WriteX(xmlwrt, "tph_contact_type", dtSigPersonPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                            WriteX(xmlwrt, "tph_communication_type", dtSigPersonPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                            WriteX(xmlwrt, "tph_country_prefix", dtSigPersonPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                            WriteX(xmlwrt, "tph_number", dtSigPersonPhone.Rows(k)("TPH_NUMBER"), True)
                            WriteX(xmlwrt, "tph_extension", dtSigPersonPhone.Rows(k)("TPH_EXTENSION"), False)
                            xmlwrt.WriteEndElement() 'phone

                        Next


                        xmlwrt.WriteEndElement() 'phones
                        errLevel = "66"
                        xmlwrt.WriteStartElement("addresses")


                        '-- SID=P for Personal Address
                        dtSigPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                        If dtSigPersonAdd.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Address Missing")
                            Return False
                        End If

                        For k = 0 To dtSigPersonAdd.Rows.Count - 1
                            xmlwrt.WriteStartElement("address")
                            WriteX(xmlwrt, "address_type", dtSigPersonAdd.Rows(k)("ADDRESS_TYPE"), True)
                            WriteX(xmlwrt, "address", dtSigPersonAdd.Rows(k)("ADDRESS"), True)
                            WriteX(xmlwrt, "town", dtSigPersonAdd.Rows(k)("TOWN"), False)
                            WriteX(xmlwrt, "city", dtSigPersonAdd.Rows(k)("CITY"), True)
                            WriteX(xmlwrt, "zip", dtSigPersonAdd.Rows(k)("ZIP"), False)
                            WriteX(xmlwrt, "country_code", dtSigPersonAdd.Rows(k)("COUNTRY_CODE"), True)
                            WriteX(xmlwrt, "state", dtSigPersonAdd.Rows(k)("STATE"), True)
                            xmlwrt.WriteEndElement() 'address

                        Next



                        xmlwrt.WriteEndElement() 'addresses
                        errLevel = "67"
                        '---------------------

                        WriteX(xmlwrt, "email", dtAccSigDetGo.Rows(0)("EMAIL"), True)
                        WriteX(xmlwrt, "occupation", dtAccSigDetGo.Rows(0)("OCP_CODE"), True)
                        WriteX(xmlwrt, "employer_name", dtAccSigDetGo.Rows(0)("EMPLOYER_NAME"), False)


                        'xmlwrt.WriteStartElement("employer_address_id")


                        '-- SID=E for Employer Address
                        dtSigEmployerAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)


                        If dtSigEmployerAdd.Rows.Count > 0 Then

                            'SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            'SetProcessStatus(Environment.NewLine & "Owner Employer Address Missing")
                            'Return False


                            'For l = 0 To dtSigEmployerAdd.Rows.Count - 1

                            For l = 0 To 0

                                xmlwrt.WriteStartElement("employer_address_id")
                                WriteX(xmlwrt, "address_type", dtSigEmployerAdd.Rows(l)("ADDRESS_TYPE"), False)
                                WriteX(xmlwrt, "address", dtSigEmployerAdd.Rows(l)("ADDRESS"), False)
                                WriteX(xmlwrt, "town", dtSigEmployerAdd.Rows(l)("TOWN"), False)
                                WriteX(xmlwrt, "city", dtSigEmployerAdd.Rows(l)("CITY"), False)
                                WriteX(xmlwrt, "zip", dtSigEmployerAdd.Rows(l)("ZIP"), False)
                                WriteX(xmlwrt, "country_code", dtSigEmployerAdd.Rows(l)("COUNTRY_CODE"), False)
                                WriteX(xmlwrt, "state", dtSigEmployerAdd.Rows(l)("STATE"), False)
                                xmlwrt.WriteEndElement() 'address

                            Next

                        End If

                        'xmlwrt.WriteEndElement() 'addresses

                        '---------------------
                        errLevel = "68"

                        'xmlwrt.WriteStartElement("phones")


                        '-- SID=E For Employer Phone 
                        dtSigEmployerPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)


                        If dtSigEmployerPhone.Rows.Count > 0 Then
                            'SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            'SetProcessStatus(Environment.NewLine & "Owner Employer Phone Missing")
                            'Return False

                            'For k = 0 To dtSigEmployerPhone.Rows.Count - 1

                            For k = 0 To 0

                                xmlwrt.WriteStartElement("employer_phone_id")
                                WriteX(xmlwrt, "tph_contact_type", dtSigEmployerPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                WriteX(xmlwrt, "tph_communication_type", dtSigEmployerPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                WriteX(xmlwrt, "tph_country_prefix", dtSigEmployerPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                WriteX(xmlwrt, "tph_number", dtSigEmployerPhone.Rows(k)("TPH_NUMBER"), True)

                                WriteX(xmlwrt, "tph_extension", dtSigEmployerPhone.Rows(k)("TPH_EXTENSION"), False)


                                xmlwrt.WriteEndElement() 'phone

                            Next

                        End If


                        errLevel = "69"
                        'xmlwrt.WriteEndElement() 'phones


                        '--SID=E used for personal indentification
                        dtSigPersonIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                        If dtSigPersonIdent.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            SetProcessStatus(Environment.NewLine & "Owner Indentification Missing")
                            Return False
                        End If

                        For k = 0 To dtSigPersonIdent.Rows.Count - 1
                            xmlwrt.WriteStartElement("identification")
                            WriteX(xmlwrt, "type", dtSigPersonIdent.Rows(k)("TYPE"), True)
                            WriteX(xmlwrt, "number", dtSigPersonIdent.Rows(k)("NUMBER"), True)
                            WriteX(xmlwrt, "issue_date", NullHelper.DateToXML(dtSigPersonIdent.Rows(k)("ISSUE_DATE")), False)
                            WriteX(xmlwrt, "expiry_date", NullHelper.DateToXML(dtSigPersonIdent.Rows(k)("EXPIRY_DATE")), False)
                            WriteX(xmlwrt, "issued_by", dtSigPersonIdent.Rows(k)("ISSUED_BY"), False)
                            WriteX(xmlwrt, "issue_country", dtSigPersonIdent.Rows(k)("ISSUE_COUNTRY"), True)

                            xmlwrt.WriteEndElement() 'identification

                        Next
                        errLevel = "70"

                        If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("DECEASED")) = "1" Then
                            WriteX(xmlwrt, "deceased", "1", False)
                            WriteX(xmlwrt, "date_deceased", NullHelper.DateToXML(dtAccSigDetGo.Rows(0)("DECEASED_DATE")), False)
                        End If

                        WriteX(xmlwrt, "tax_number", dtAccSigDet.Rows(0)("TIN"), False)
                        WriteX(xmlwrt, "tax_reg_number", dtAccSigDetGo.Rows(0)("TAX_REG_NUMBER"), False)
                        WriteX(xmlwrt, "source_of_wealth", dtAccSigDetGo.Rows(0)("SOURCE_OF_WEALTH"), False)
                        WriteX(xmlwrt, "comments", dtAccSigDetGo.Rows(0)("COMMENTS"), False)


                        xmlwrt.WriteEndElement() 't_person

                        WriteX(xmlwrt, "role", dtAccSig.Rows(j)("ROLE_TYPE"), True)

                        xmlwrt.WriteEndElement() 'signatory


                    Next j
                    errLevel = "71"
                    WriteX(xmlwrt, "opened", NullHelper.DateToXML(dtTranAccGo.Rows(0)("OPENED")), False)
                    WriteX(xmlwrt, "closed", NullHelper.DateToXML(dtTranAccGo.Rows(0)("CLOSED")), False)
                    WriteX(xmlwrt, "balance", dtTransation.Rows(i)("BALANCE"), False)
                    WriteX(xmlwrt, "date_balance", NullHelper.DateToXML(dtTransation.Rows(i)("DATE_TRANSACTION")), False)

                    'WriteX(xmlwrt, "status_code", dtTranAccGo.Rows(0)("STATUS_CODE"), True)
                    WriteX(xmlwrt, "status_code", "A", True)

                    WriteX(xmlwrt, "beneficiary", dtTranAccGo.Rows(0)("BENEFICIARY"), False)
                    WriteX(xmlwrt, "beneficiary_comment", dtTranAccGo.Rows(0)("BENEFICIARY_COMMENTS"), False)
                    WriteX(xmlwrt, "comments", dtTranAccGo.Rows(0)("COMMENTS"), False)

                    xmlwrt.WriteEndElement() 'to_account

                    WriteX(xmlwrt, "to_country", "BD", True)

                    'xmlwrt.WriteEndElement() 'to_account

                    errLevel = "72"

                Else
                    errLevel = "73"
                    '-- start to_person

                    dtBearer = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_INFO WHERE [STATUS]='L' AND REFERENCE_NUMBER='" & NullHelper.ObjectToString(dtTransation.Rows(i)("TO_PERSON")) & "'").Tables(0)

                    xmlwrt.WriteStartElement("to_person")

                    If dtBearer.Rows.Count > 0 Then
                        'original bearer information processing start

                        For j = 0 To 0

                            If NullHelper.ObjectToString(dtBearer.Rows(0)("FIRST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                SetProcessStatus(Environment.NewLine & "Depositor First Name Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtBearer.Rows(0)("LAST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                SetProcessStatus(Environment.NewLine & "Depositor Last Name Missing")
                                Return False
                            End If



                            'PREFIX=spouse name,ALIAS=fathers name
                            'If NullHelper.ObjectToString(dtBearer.Rows(0)("PREFIX")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("ALIAS")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("MOTHERS_NAME")) = "" Then
                            '    SetProcessStatus(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                            '    SetProcessStatus(Environment.NewLine & "Depositor Father,Mother,Spouse Name Missing")
                            'End If

                            'SSN=National ID, ID_NUMBER= Birth Registration Number
                            If NullHelper.ObjectToString(dtBearer.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("PASSPORT_NUMBER")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("ID_NUMBER")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                SetProcessStatus(Environment.NewLine & "Depositor National ID/Passport No/Birth Registration No Missing")
                            End If

                            '--National ID start---

                            'If NullHelper.ObjectToString(dtBearer.Rows(0)("SSN")) <> "" And Len(NullHelper.ObjectToString(dtBearer.Rows(0)("SSN"))) <> 13 And Len(NullHelper.ObjectToString(dtBearer.Rows(0)("SSN"))) <> 17 Then
                            '    SetProcessStatus(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                            '    SetProcessStatus(Environment.NewLine & "Depositor National ID Must be 13 or 17 digits(goAML)")
                            '    Return False

                            'End If
                            '--National ID End---


                            WriteX(xmlwrt, "gender", dtBearer.Rows(0)("GENDER"), False)
                            WriteX(xmlwrt, "title", dtBearer.Rows(0)("TITLE"), False)
                            WriteX(xmlwrt, "first_name", dtBearer.Rows(0)("FIRST_NAME"), True)
                            WriteX(xmlwrt, "middle_name", dtBearer.Rows(0)("MIDDLE_NAME"), False)
                            WriteX(xmlwrt, "prefix", dtBearer.Rows(0)("PREFIX"), False)
                            WriteX(xmlwrt, "last_name", dtBearer.Rows(0)("LAST_NAME"), True)
                            WriteX(xmlwrt, "birthdate", NullHelper.DateToXML(dtBearer.Rows(0)("BIRTHDATE")), False)
                            WriteX(xmlwrt, "birth_place", dtBearer.Rows(0)("BIRTH_PLACE"), False)

                            WriteX(xmlwrt, "mothers_name", dtBearer.Rows(0)("MOTHERS_NAME"), False)
                            WriteX(xmlwrt, "alias", dtBearer.Rows(0)("ALIAS"), False)
                            WriteX(xmlwrt, "ssn", dtBearer.Rows(0)("SSN"), False)

                            If Not NullHelper.ObjectToString(dtBearer.Rows(0)("PASSPORT_NUMBER")) = "" Then
                                WriteX(xmlwrt, "passport_number", dtBearer.Rows(0)("PASSPORT_NUMBER"), False)
                                WriteX(xmlwrt, "passport_country", dtBearer.Rows(0)("PASSPORT_COUNTRY"), False)
                            End If


                            If Not NullHelper.ObjectToString(dtBearer.Rows(0)("ID_NUMBER")) = "" Then
                                WriteX(xmlwrt, "id_number", dtBearer.Rows(0)("ID_NUMBER"), False)
                            ElseIf Not NullHelper.ObjectToString(dtBearer.Rows(0)("PASSPORT_NUMBER")) = "" Then
                                WriteX(xmlwrt, "id_number", dtBearer.Rows(0)("PASSPORT_NUMBER"), False)
                            Else
                                WriteX(xmlwrt, "id_number", dtBearer.Rows(0)("SSN"), False)
                            End If

                            'WriteX(xmlwrt, "id_number", dtBearer.Rows(0)("ID_NUMBER"), False)

                            WriteX(xmlwrt, "nationality1", dtBearer.Rows(0)("NATIONALITY1"), False)
                            WriteX(xmlwrt, "nationality2", dtBearer.Rows(0)("NATIONALITY2"), False)
                            WriteX(xmlwrt, "nationality3", dtBearer.Rows(0)("NATIONALITY3"), False)

                            WriteX(xmlwrt, "residence", dtBearer.Rows(0)("RESIDENCE"), False)
                            '----------------------
                            errLevel = "74"
                            xmlwrt.WriteStartElement("phones")


                            '-- SID=P For Personal Phone 
                            dtBearerPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND PERSON_ID='" & dtBearer.Rows(0)("PERSON_ID").ToString() & "'").Tables(0)


                            If dtBearerPhone.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Bearer Code: " & dtBearer.Rows(0)("PERSON_ID"))
                                SetProcessStatus(Environment.NewLine & "Bearer Personal Phone Missing")
                                Return False
                            End If

                            For k = 0 To dtBearerPhone.Rows.Count - 1
                                xmlwrt.WriteStartElement("phone")
                                WriteX(xmlwrt, "tph_contact_type", dtBearerPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                WriteX(xmlwrt, "tph_communication_type", dtBearerPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                WriteX(xmlwrt, "tph_country_prefix", dtBearerPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                WriteX(xmlwrt, "tph_number", dtBearerPhone.Rows(k)("TPH_NUMBER"), True)

                                WriteX(xmlwrt, "tph_extension", dtBearerPhone.Rows(k)("TPH_EXTENSION"), False)


                                xmlwrt.WriteEndElement() 'phone

                            Next

                            xmlwrt.WriteEndElement() 'phones
                            errLevel = "75"
                            xmlwrt.WriteStartElement("addresses")


                            '-- SID=P for Personal Address
                            dtBearerAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND PERSON_ID='" & dtBearer.Rows(0)("PERSON_ID").ToString() & "'").Tables(0)

                            If dtBearerAdd.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Bearer Code: " & dtBearer.Rows(0)("PERSON_ID"))
                                SetProcessStatus(Environment.NewLine & "Bearer Personal Address Missing")
                                Return False
                            End If


                            For k = 0 To dtBearerAdd.Rows.Count - 1
                                xmlwrt.WriteStartElement("address")
                                WriteX(xmlwrt, "address_type", dtBearerAdd.Rows(k)("ADDRESS_TYPE"), True)
                                WriteX(xmlwrt, "address", dtBearerAdd.Rows(k)("ADDRESS"), True)
                                WriteX(xmlwrt, "town", dtBearerAdd.Rows(k)("TOWN"), False)
                                WriteX(xmlwrt, "city", dtBearerAdd.Rows(k)("CITY"), True)
                                WriteX(xmlwrt, "zip", dtBearerAdd.Rows(k)("ZIP"), False)
                                WriteX(xmlwrt, "country_code", dtBearerAdd.Rows(k)("COUNTRY_CODE"), True)
                                WriteX(xmlwrt, "state", dtBearerAdd.Rows(k)("STATE"), True)
                                xmlwrt.WriteEndElement() 'address

                            Next

                            xmlwrt.WriteEndElement() 'addresses

                            '---------------------
                            errLevel = "76"
                            WriteX(xmlwrt, "email", dtBearer.Rows(0)("EMAIL"), False)
                            WriteX(xmlwrt, "occupation", dtBearer.Rows(0)("OCCUPATION"), False)
                            WriteX(xmlwrt, "employer_name", dtBearer.Rows(0)("EMPLOYER_NAME"), False)


                            'xmlwrt.WriteStartElement("employer_address_id")


                            '-- SID=E for Employer Address
                            dtBearerEmployerAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_ADDRESS WHERE [STATUS]='L' AND [SID]='E' AND PERSON_ID='" & dtBearer.Rows(0)("PERSON_ID").ToString() & "'").Tables(0)

                            If dtBearerEmployerAdd.Rows.Count > 0 Then
                                'SetProcessStatus(Environment.NewLine & "Bearer Code: " & dtBearer.Rows(0)("PERSON_ID"))
                                'SetProcessStatus(Environment.NewLine & "Bearer Employer Address Missing")
                                'Return False



                                'For k = 0 To dtBearerEmployerAdd.Rows.Count - 1
                                For k = 0 To 0
                                    xmlwrt.WriteStartElement("employer_address_id")
                                    WriteX(xmlwrt, "address_type", dtBearerEmployerAdd.Rows(k)("ADDRESS_TYPE"), False)
                                    WriteX(xmlwrt, "address", dtBearerEmployerAdd.Rows(k)("ADDRESS"), False)
                                    WriteX(xmlwrt, "town", dtBearerEmployerAdd.Rows(k)("TOWN"), False)
                                    WriteX(xmlwrt, "city", dtBearerEmployerAdd.Rows(k)("CITY"), False)
                                    WriteX(xmlwrt, "zip", dtBearerEmployerAdd.Rows(k)("ZIP"), False)
                                    WriteX(xmlwrt, "country_code", dtBearerEmployerAdd.Rows(k)("COUNTRY_CODE"), False)
                                    WriteX(xmlwrt, "state", dtBearerEmployerAdd.Rows(k)("STATE"), False)
                                    xmlwrt.WriteEndElement() 'address

                                Next

                            End If

                            'xmlwrt.WriteEndElement() 'addresses

                            '---------------------
                            errLevel = "77"

                            'xmlwrt.WriteStartElement("phones")


                            ''-- SID=E For Employer Phone 
                            dtBearerEmployerPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_PHONE WHERE [STATUS]='L' AND [SID]='E' AND PERSON_ID='" & dtBearer.Rows(0)("PERSON_ID").ToString() & "'").Tables(0)


                            If dtBearerEmployerPhone.Rows.Count > 0 Then
                                'SetProcessStatus(Environment.NewLine & "Bearer Code: " & dtBearer.Rows(0)("PERSON_ID"))
                                'SetProcessStatus(Environment.NewLine & "Bearer Employer Phone Missing")
                                'Return False


                                'For k = 0 To dtBearerEmployerPhone.Rows.Count - 1
                                For k = 0 To 0
                                    xmlwrt.WriteStartElement("employer_phone_id")
                                    WriteX(xmlwrt, "tph_contact_type", dtBearerEmployerPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                    WriteX(xmlwrt, "tph_communication_type", dtBearerEmployerPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                    WriteX(xmlwrt, "tph_country_prefix", dtBearerEmployerPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                    WriteX(xmlwrt, "tph_number", dtBearerEmployerPhone.Rows(k)("TPH_NUMBER"), True)

                                    WriteX(xmlwrt, "tph_extension", dtBearerEmployerPhone.Rows(k)("TPH_EXTENSION"), False)


                                    xmlwrt.WriteEndElement() 'phone

                                Next

                            End If

                            ' xmlwrt.WriteEndElement() 'phones
                            errLevel = "78"

                            '--SID=E used for personal indentification
                            dtBearerIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND PERSON_ID='" & dtBearer.Rows(0)("PERSON_ID").ToString() & "'").Tables(0)


                            If dtBearerIdent.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Bearer Code: " & dtBearer.Rows(0)("PERSON_ID"))
                                SetProcessStatus(Environment.NewLine & "Bearer Identification Missing")
                                Return False
                            End If

                            'For k = 0 To dtBearerIdent.Rows.Count - 1

                            For k = 0 To 0
                                xmlwrt.WriteStartElement("identification")
                                WriteX(xmlwrt, "type", dtBearerIdent.Rows(k)("TYPE"), True)
                                WriteX(xmlwrt, "number", dtBearerIdent.Rows(k)("NUMBER"), True)
                                WriteX(xmlwrt, "issue_date", NullHelper.DateToXML(dtBearerIdent.Rows(k)("ISSUE_DATE")), False)
                                WriteX(xmlwrt, "expiry_date", NullHelper.DateToXML(dtBearerIdent.Rows(k)("EXPIRY_DATE")), False)
                                WriteX(xmlwrt, "issued_by", dtBearerIdent.Rows(k)("ISSUED_BY"), False)
                                WriteX(xmlwrt, "issue_country", dtBearerIdent.Rows(k)("ISSUE_COUNTRY"), True)

                                xmlwrt.WriteEndElement() 'identification

                            Next
                            errLevel = "79"

                            If NullHelper.ObjectToString(dtBearer.Rows(0)("DECEASED")) = "1" Then
                                WriteX(xmlwrt, "deceased", "1", False)
                                WriteX(xmlwrt, "date_deceased", NullHelper.DateToXML(dtBearer.Rows(0)("DECEASED_DATE")), False)
                            End If



                            WriteX(xmlwrt, "tax_number", dtBearer.Rows(0)("TAX_NUMBER"), False)
                            WriteX(xmlwrt, "tax_reg_number", dtBearer.Rows(0)("TAX_REG_NUMBER"), False)
                            WriteX(xmlwrt, "source_of_wealth", dtBearer.Rows(0)("SOURCE_OF_WEALTH"), False)
                            WriteX(xmlwrt, "comments", dtBearer.Rows(0)("COMMENTS"), False)





                        Next j

                        errLevel = "80"
                        'original bearer information processing end
                    Else
                        errLevel = "81"
                        dtAccSig = db.ExecuteDataSet(CommandType.Text, "SELECT OWNER_CODE,ROLE_TYPE FROM FIU_TRANS_AC_OWNER WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "'").Tables(0)

                        If dtAccSig.Rows.Count = 0 Then
                            SetProcessStatus(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            SetProcessStatus(Environment.NewLine & "Owner Info Missing")
                            Return False
                        End If




                        For j = 0 To 0


                            dtAccSigDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                            dtAccSigDetGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                            If dtAccSigDetGo.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Info Missing (goAML)")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("FIRST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner First Name Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("LAST_NAME")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Last Name Missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("DOB")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner DOB Missing")
                                Return False
                            End If


                            If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_SPOUSE")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_FATHER")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_MOTHER")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Father,Mother,Spause Name Missing")
                            End If

                            'SSN=National ID, ID_NUMBER= Birth Registration Number
                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("PPNO")) = "" And NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("ID_NUMBER")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner National ID/Passport No/Birth Registration No Missing")
                            End If

                            '--National ID start---

                            'If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) <> "" And Len(NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN"))) <> 13 And Len(NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN"))) <> 17 Then
                            '    SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                            '    SetProcessStatus(Environment.NewLine & "Owner National ID Must be 13 or 17 digits(goAML)")
                            '    Return False

                            'End If
                            '--National ID End---


                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("NATIONALITY1")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Nationality1 missing")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("RESIDENCE")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Residence missing (goAML)")
                                Return False
                            End If

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("OCP_CODE")) = "" Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner occupation missing (goAML)")
                                Return False
                            End If

                            WriteX(xmlwrt, "gender", dtAccSigDet.Rows(0)("GENDER"), True)
                            WriteX(xmlwrt, "title", dtAccSigDetGo.Rows(0)("TITLE"), False)
                            WriteX(xmlwrt, "first_name", dtAccSigDetGo.Rows(0)("FIRST_NAME"), True)
                            WriteX(xmlwrt, "middle_name", dtAccSigDetGo.Rows(0)("MIDDLE_NAME"), False)
                            WriteX(xmlwrt, "prefix", dtAccSigDet.Rows(0)("OWNER_SPOUSE"), False)
                            WriteX(xmlwrt, "last_name", dtAccSigDetGo.Rows(0)("LAST_NAME"), True)
                            WriteX(xmlwrt, "birthdate", NullHelper.DateToXML(dtAccSigDet.Rows(0)("DOB")), True)
                            WriteX(xmlwrt, "birth_place", dtAccSigDetGo.Rows(0)("BIRTH_PLACE"), False)

                            WriteX(xmlwrt, "mothers_name", dtAccSigDet.Rows(0)("OWNER_MOTHER"), True)
                            WriteX(xmlwrt, "alias", dtAccSigDet.Rows(0)("OWNER_FATHER"), False)
                            WriteX(xmlwrt, "ssn", dtAccSigDetGo.Rows(0)("SSN"), False)
                            WriteX(xmlwrt, "passport_number", dtAccSigDet.Rows(0)("PPNO"), False)

                            WriteX(xmlwrt, "id_number", dtAccSigDetGo.Rows(0)("ID_NUMBER"), False)
                            WriteX(xmlwrt, "nationality1", dtAccSigDetGo.Rows(0)("NATIONALITY1"), True)
                            WriteX(xmlwrt, "nationality2", dtAccSigDetGo.Rows(0)("NATIONALITY2"), False)
                            WriteX(xmlwrt, "nationality3", dtAccSigDetGo.Rows(0)("NATIONALITY3"), False)

                            WriteX(xmlwrt, "residence", dtAccSigDetGo.Rows(0)("RESIDENCE"), True)
                            '----------------------
                            errLevel = "82"
                            xmlwrt.WriteStartElement("phones")


                            '-- SID=P For Personal Phone 
                            dtSigPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                            If dtSigPersonPhone.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Phone Missing")
                                Return False
                            End If

                            For k = 0 To dtSigPersonPhone.Rows.Count - 1
                                xmlwrt.WriteStartElement("phone")
                                WriteX(xmlwrt, "tph_contact_type", dtSigPersonPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                WriteX(xmlwrt, "tph_communication_type", dtSigPersonPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                WriteX(xmlwrt, "tph_country_prefix", dtSigPersonPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                WriteX(xmlwrt, "tph_number", dtSigPersonPhone.Rows(k)("TPH_NUMBER"), True)

                                WriteX(xmlwrt, "tph_extension", dtSigPersonPhone.Rows(k)("TPH_EXTENSION"), False)


                                xmlwrt.WriteEndElement() 'phone

                            Next

                            xmlwrt.WriteEndElement() 'phones
                            errLevel = "83"
                            xmlwrt.WriteStartElement("addresses")


                            '-- SID=P for Personal Address
                            dtSigPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                            If dtSigPersonAdd.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Address Missing")
                                Return False
                            End If

                            For k = 0 To dtSigPersonAdd.Rows.Count - 1
                                xmlwrt.WriteStartElement("address")
                                WriteX(xmlwrt, "address_type", dtSigPersonAdd.Rows(k)("ADDRESS_TYPE"), True)
                                WriteX(xmlwrt, "address", dtSigPersonAdd.Rows(k)("ADDRESS"), True)
                                WriteX(xmlwrt, "town", dtSigPersonAdd.Rows(k)("TOWN"), False)
                                WriteX(xmlwrt, "city", dtSigPersonAdd.Rows(k)("CITY"), True)
                                WriteX(xmlwrt, "zip", dtSigPersonAdd.Rows(k)("ZIP"), False)
                                WriteX(xmlwrt, "country_code", dtSigPersonAdd.Rows(k)("COUNTRY_CODE"), True)
                                WriteX(xmlwrt, "state", dtSigPersonAdd.Rows(k)("STATE"), True)
                                xmlwrt.WriteEndElement() 'address

                            Next

                            xmlwrt.WriteEndElement() 'addresses
                            errLevel = "84"
                            '---------------------

                            WriteX(xmlwrt, "email", dtAccSigDetGo.Rows(0)("EMAIL"), True)
                            WriteX(xmlwrt, "occupation", dtAccSigDetGo.Rows(0)("OCP_CODE"), True)
                            WriteX(xmlwrt, "employer_name", dtAccSigDetGo.Rows(0)("EMPLOYER_NAME"), False)


                            ''xmlwrt.WriteStartElement("employer_address_id")


                            ''-- SID=E for Employer Address
                            dtSigEmployerAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                            If dtSigEmployerAdd.Rows.Count > 0 Then

                                'For k = 0 To dtSigEmployerAdd.Rows.Count - 1
                                For k = 0 To 0
                                    xmlwrt.WriteStartElement("employer_address_id")
                                    WriteX(xmlwrt, "address_type", dtSigEmployerAdd.Rows(k)("ADDRESS_TYPE"), False)
                                    WriteX(xmlwrt, "address", dtSigEmployerAdd.Rows(k)("ADDRESS"), False)
                                    WriteX(xmlwrt, "town", dtSigEmployerAdd.Rows(k)("TOWN"), False)
                                    WriteX(xmlwrt, "city", dtSigEmployerAdd.Rows(k)("CITY"), False)
                                    WriteX(xmlwrt, "zip", dtSigEmployerAdd.Rows(k)("ZIP"), False)
                                    WriteX(xmlwrt, "country_code", dtSigEmployerAdd.Rows(k)("COUNTRY_CODE"), False)
                                    WriteX(xmlwrt, "state", dtSigEmployerAdd.Rows(k)("STATE"), False)
                                    xmlwrt.WriteEndElement() 'address

                                Next

                            End If

                            ''xmlwrt.WriteEndElement() 'addresses

                            ''---------------------


                            'xmlwrt.WriteStartElement("phones")

                            errLevel = "85"
                            ''-- SID=E For Employer Phone 
                            dtSigEmployerPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)


                            If dtSigEmployerPhone.Rows.Count > 0 Then

                                'SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                'SetProcessStatus(Environment.NewLine & "Owner Employer Phone Missing")
                                'Return False


                                'For k = 0 To dtSigEmployerPhone.Rows.Count - 1
                                For k = 0 To 0
                                    xmlwrt.WriteStartElement("employer_phone_id")
                                    WriteX(xmlwrt, "tph_contact_type", dtSigEmployerPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                                    WriteX(xmlwrt, "tph_communication_type", dtSigEmployerPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                                    WriteX(xmlwrt, "tph_country_prefix", dtSigEmployerPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                                    WriteX(xmlwrt, "tph_number", dtSigEmployerPhone.Rows(k)("TPH_NUMBER"), True)

                                    WriteX(xmlwrt, "tph_extension", dtSigEmployerPhone.Rows(k)("TPH_EXTENSION"), False)


                                    xmlwrt.WriteEndElement() 'phone

                                Next

                            End If

                            ' xmlwrt.WriteEndElement() 'phones



                            errLevel = "86"



                            '--SID=E used for personal indentification
                            dtSigPersonIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                            If dtSigPersonIdent.Rows.Count = 0 Then
                                SetProcessStatus(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                SetProcessStatus(Environment.NewLine & "Owner Indentification Missing")
                                Return False
                            End If

                            'For k = 0 To dtSigPersonIdent.Rows.Count - 1
                            For k = 0 To 0
                                xmlwrt.WriteStartElement("identification")
                                WriteX(xmlwrt, "type", dtSigPersonIdent.Rows(k)("TYPE"), True)
                                WriteX(xmlwrt, "number", dtSigPersonIdent.Rows(k)("NUMBER"), True)
                                WriteX(xmlwrt, "issue_date", NullHelper.DateToXML(dtSigPersonIdent.Rows(k)("ISSUE_DATE")), False)
                                WriteX(xmlwrt, "expiry_date", NullHelper.DateToXML(dtSigPersonIdent.Rows(k)("EXPIRY_DATE")), False)
                                WriteX(xmlwrt, "issued_by", dtSigPersonIdent.Rows(k)("ISSUED_BY"), False)
                                WriteX(xmlwrt, "issue_country", dtSigPersonIdent.Rows(k)("ISSUE_COUNTRY"), True)

                                xmlwrt.WriteEndElement() 'identification

                            Next

                            errLevel = "87"
                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("DECEASED")) = "1" Then
                                WriteX(xmlwrt, "deceased", "1", False)
                                WriteX(xmlwrt, "date_deceased", NullHelper.DateToXML(dtAccSigDetGo.Rows(0)("DECEASED_DATE")), False)
                            End If

                            WriteX(xmlwrt, "tax_number", dtAccSigDet.Rows(0)("TIN"), False)
                            WriteX(xmlwrt, "tax_reg_number", dtAccSigDetGo.Rows(0)("TAX_REG_NUMBER"), False)
                            WriteX(xmlwrt, "source_of_wealth", dtAccSigDetGo.Rows(0)("SOURCE_OF_WEALTH"), False)
                            WriteX(xmlwrt, "comments", dtAccSigDetGo.Rows(0)("COMMENTS"), False)

                        Next j
                        errLevel = "88"
                        '------------------Entity Information--------


                        'Dim dtEntityInfo As DataTable

                        ' ''
                        'dtAccEntity = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & NullHelper.ObjectToString(dtTransation.Rows(i)("ACCOUNT")) & "'").Tables(0)

                        'If dtAccEntity.Rows.Count = 0 Then
                        '    SetProcessStatus(Environment.NewLine & "Account : " & dtTransation.Rows(i)("ACCOUNT"))
                        '    SetProcessStatus(Environment.NewLine & "Entity ID Missing(goAML)")
                        '    Return False
                        'End If

                        'If NullHelper.ObjectToString(dtAccEntity.Rows(0)("ENTITY_ID")) = "" Then
                        '    SetProcessStatus(Environment.NewLine & "Account : " & dtTransation.Rows(i)("ACCOUNT"))
                        '    SetProcessStatus(Environment.NewLine & "Entity ID Missing(goAML)")
                        '    Return False
                        'End If
                        ''ENTITY_ID

                        'dtEntityInfo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY WHERE [STATUS]='L' AND ENTITY_ID='" & NullHelper.ObjectToString(dtAccEntity.Rows(0)("ENTITY_ID")) & "' ").Tables(0)

                        'If dtEntityInfo.Rows.Count = 0 Then
                        '    SetProcessStatus(Environment.NewLine & "Account : " & dtTransation.Rows(i)("ACCOUNT"))
                        '    SetProcessStatus(Environment.NewLine & "Entity ID: " & dtAccEntity.Rows(0)("ENTITY_ID"))
                        '    SetProcessStatus(Environment.NewLine & "Entity Missing(goAML)")
                        '    Return False
                        'End If

                        ''
                        'dtAccEntity = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & NullHelper.ObjectToString(dtTransation.Rows(i)("ACCOUNT")) & "'").Tables(0)

                        'If dtAccEntity.Rows.Count = 0 Then
                        '    SetProcessStatus(Environment.NewLine & "Account : " & dtTransation.Rows(i)("ACCOUNT"))
                        '    SetProcessStatus(Environment.NewLine & "Entity ID Missing(goAML)")
                        '    Return False
                        'End If

                        'dtEntityInfo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY WHERE [STATUS]='L' AND ENTITY_ID='" & NullHelper.ObjectToString(dtAccEntity.Rows(0)("ENTITY_ID")) & "' ").Tables(0)

                        'If dtEntityInfo.Rows.Count > 0 Then


                        '    WriteX(xmlwrt, "first_name", dtEntityInfo.Rows(0)("NAME").Substring(0, dtEntityInfo.Rows(0)("NAME").IndexOf(" ")), True)
                        '    WriteX(xmlwrt, "last_name", dtEntityInfo.Rows(0)("NAME").Substring(dtEntityInfo.Rows(0)("NAME").IndexOf(" ") + 1), True)
                        '    WriteX(xmlwrt, "passport_number", dtEntityInfo.Rows(0)("INCORPORATION_NUMBER"), True)

                        '    '-- entity phone

                        '    xmlwrt.WriteStartElement("phones")

                        '    '-- SID=P For Personal Phone 
                        '    dtAccEntityPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_PHONE WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtAccEntity.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        '    If dtAccEntityPhone.Rows.Count = 0 Then
                        '        SetProcessStatus(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                        '        SetProcessStatus(Environment.NewLine & "Entity ID: " & dtAccEntity.Rows(0)("ENTITY_ID"))
                        '        SetProcessStatus(Environment.NewLine & "Entity Phone Missing")
                        '        Return False
                        '    End If

                        '    For k = 0 To dtAccEntityPhone.Rows.Count - 1
                        '        xmlwrt.WriteStartElement("phone")
                        '        WriteX(xmlwrt, "tph_contact_type", dtAccEntityPhone.Rows(k)("TPH_CONTACT_TYPE"), True)
                        '        WriteX(xmlwrt, "tph_communication_type", dtAccEntityPhone.Rows(k)("TPH_COMMUNICATION_TYPE"), True)
                        '        WriteX(xmlwrt, "tph_country_prefix", dtAccEntityPhone.Rows(k)("TPH_COUNTRY_PREFIX"), False)
                        '        WriteX(xmlwrt, "tph_number", dtAccEntityPhone.Rows(k)("TPH_NUMBER"), True)
                        '        WriteX(xmlwrt, "tph_extension", dtAccEntityPhone.Rows(k)("TPH_EXTENSION"), False)


                        '        xmlwrt.WriteEndElement() 'phone

                        '    Next

                        '    xmlwrt.WriteEndElement() 'phones


                        '    '-- end entity phone
                        '    '-- entity address


                        '    xmlwrt.WriteStartElement("addresses")


                        '    '-- SID=P for Personal Address
                        '    dtAccEntityAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtAccEntity.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        '    If dtAccEntityAdd.Rows.Count = 0 Then
                        '        SetProcessStatus(Environment.NewLine & "Account Code: " & dtTransation.Rows(i)("ACCOUNT"))
                        '        SetProcessStatus(Environment.NewLine & "Entity ID: " & dtAccEntity.Rows(0)("ENTITY_ID"))
                        '        SetProcessStatus(Environment.NewLine & "Entity Address Missing")
                        '        Return False
                        '    End If

                        '    For k = 0 To dtAccEntityAdd.Rows.Count - 1
                        '        xmlwrt.WriteStartElement("address")
                        '        WriteX(xmlwrt, "address_type", dtAccEntityAdd.Rows(k)("ADDRESS_TYPE"), True)
                        '        WriteX(xmlwrt, "address", dtAccEntityAdd.Rows(k)("ADDRESS"), True)
                        '        WriteX(xmlwrt, "town", dtAccEntityAdd.Rows(k)("TOWN"), False)
                        '        WriteX(xmlwrt, "city", dtAccEntityAdd.Rows(k)("CITY"), True)
                        '        WriteX(xmlwrt, "zip", dtAccEntityAdd.Rows(k)("ZIP"), False)
                        '        WriteX(xmlwrt, "country_code", dtAccEntityAdd.Rows(k)("COUNTRY_CODE"), True)
                        '        WriteX(xmlwrt, "state", dtAccEntityAdd.Rows(k)("STATE"), True)
                        '        xmlwrt.WriteEndElement() 'address

                        '    Next

                        '    xmlwrt.WriteEndElement() 'addresses

                        '    '-- end entity address


                        'End If

                        '------------------Entity Information End-----





                    End If '-- end of bearer condition

                    xmlwrt.WriteEndElement() 'to_person

                    WriteX(xmlwrt, "to_country", "BD", True)


                    '-- end to_person
                    errLevel = "89"

                End If

                xmlwrt.WriteEndElement() 't_to_my_client/t_to



                '----------------------
                '----------------------
                '----------------------

                xmlwrt.WriteEndElement() 'transaction
                '--- end transaction
            Next i
            errLevel = "90"


            'xmlwrt.WriteStartElement("report_indicators")
            'WriteX(xmlwrt, "indicator", txtIndicator.Text.Trim(), True)
            'xmlwrt.WriteEndElement() 'report_indicators

            xmlwrt.WriteEndElement() 'report
            errLevel = "91"

            '_ProcessSuccess = True

            flagProcessSuccess = True

        Catch ex As Exception
            MessageBox.Show("Proc: ExportXML" + Environment.NewLine + _
                            "Level: " + errLevel + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            flagProcessSuccess = False
        End Try

        xmlwrt.Close()

        Return flagProcessSuccess  'Success file created with no error


    End Function

    Private Function ExportFILE(ByVal xFileName As String, ByVal db As SqlDatabase, ByVal dtTransation As DataTable, ByVal rowStart As Integer, ByVal rowEnd As Integer) As Boolean
        ErrorRow = 0
        Dim flagProcessSuccess = False

        'Dim db As New SqlDatabase(CommonAppSet.ConnStr)
        Dim dtReportPerson As DataTable


        If xFileName.Trim() = "" Then
            SetProcessStatus(Environment.NewLine & "Filename missing")
            Return False
        End If


        Dim objWriter As New System.IO.StreamWriter(txtFolderPath.Text.Trim() & "\" & xFileName.Trim() & ".txt")

       
        'Dim xmlwrt As New XmlTextWriter(txtFolderPath.Text.Trim() & "\" & xFileName.Trim() & ".txt", System.Text.Encoding.UTF8)

        Try

            'objWriter.Write(TextBox1.Text)
            'objWriter.Close()
            'MsgBox("Text written to file")

           

            '--- start reporting_person

            dtReportPerson = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_REPORT_PERSON WHERE [STATUS]='L' AND PERSON_ID=" & _ReportPersonId.ToString()).Tables(0)

            If dtReportPerson.Rows.Count = 0 Then

                objWriter.Write("Reporting person information required")

            End If

            If NullHelper.ObjectToString(dtReportPerson.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtReportPerson.Rows(0)("PASSPORT_NUMBER")) = "" And NullHelper.ObjectToString(dtReportPerson.Rows(0)("ID_NUMBER")) = "" Then

                objWriter.WriteLine(Environment.NewLine & "Reporting Person ID: " & dtReportPerson.Rows(0)("PERSON_ID"))
                objWriter.WriteLine(Environment.NewLine & "Reporting Person National ID, Passport, Birth Regi no Missing ")

            End If
           

            'Dim dtTransation As DataTable
            Dim dtTranAcc As DataTable
            Dim dtTranAccGo As DataTable
            Dim dtTranBranch As DataTable
            Dim dtAccEntity As DataTable
            Dim dtAccEntityPhone As DataTable
            Dim dtAccEntityAdd As DataTable
            Dim dtAccEntityDir As DataTable
            Dim dtAccEntityDirDet As DataTable
            Dim dtAccEntityDirPhone As DataTable
            Dim dtAccEntityDirAdd As DataTable

            Dim dtAccEntityDirEmployerPhone As DataTable
            Dim dtAccEntityDirEmployerAdd As DataTable

            Dim dtAccEntityDirIdent As DataTable

            Dim dtBearer As DataTable

            Dim dtBearerPhone As DataTable
            Dim dtBearerAdd As DataTable

            Dim dtBearerEmployerPhone As DataTable
            Dim dtBearerEmployerAdd As DataTable

            Dim dtBearerIdent As DataTable


            Dim dtAccSig As DataTable
            Dim dtAccSigDetGo As DataTable
            Dim dtAccSigDet As DataTable
            Dim dtSigPersonPhone As DataTable
            Dim dtSigPersonAdd As DataTable

            Dim dtSigEmployerPhone As DataTable
            Dim dtSigEmployerAdd As DataTable

            Dim dtSigPersonIdent As DataTable


           

            For i = rowStart To rowEnd 'dtTransation.Rows.Count - 1
                ErrorRow = ErrorRow + 1
                If dtTransation.Rows(i)("FROM_FLAG") = "A" Then 'From Account


                    dtTranAcc = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)


                    dtTranAccGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)

                    If dtTranAccGo.Rows.Count = 0 Then

                        objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        objWriter.WriteLine(Environment.NewLine & "Account goAML info missing")

                    End If


                    dtTranBranch = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_BANK_BRANCH WHERE [STATUS]='L' AND BRANCH_CODE='" & dtTranAcc.Rows(0)("BRANCH_CODE").ToString() & "' ").Tables(0)

                    If dtTranBranch.Rows.Count = 0 Then

                        objWriter.WriteLine(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                        objWriter.WriteLine(Environment.NewLine & "Branch Information not found")

                    End If

                    If NullHelper.ObjectToString(dtTranAcc.Rows(0)("AC_TITLE")) = "" Then

                        objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        objWriter.WriteLine(Environment.NewLine & "Account Title Missing")
                    End If

                    If NullHelper.ObjectToString(dtTranBranch.Rows(0)("SWIFT_CODE")) = "" Then

                        objWriter.WriteLine(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                        objWriter.WriteLine(Environment.NewLine & "Swift code Missing")

                    End If

                    If NullHelper.ObjectToString(dtTranBranch.Rows(0)("BRANCH_NAME")) = "" Then

                        objWriter.WriteLine(Environment.NewLine & "Branch Code: " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                        objWriter.WriteLine(Environment.NewLine & "Swift name Missing")

                        
                    End If

                    
                   

                    '---- entity information

                    dtAccEntity = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY WHERE [STATUS]='L' AND ENTITY_ID='" & NullHelper.ObjectToString(dtTranAccGo.Rows(0)("ENTITY_ID")) & "' ").Tables(0)


                    If dtAccEntity.Rows.Count = 0 Then


                        objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        objWriter.WriteLine(Environment.NewLine & "Entity Info Missing")

                    End If


                    If dtAccEntity.Rows.Count > 0 Then


                        '-- SID=P For Personal Phone 
                        dtAccEntityPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_PHONE WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityPhone.Rows.Count = 0 Then

                            objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            objWriter.WriteLine(Environment.NewLine & "Entity Phone Missing")

                        End If


                        '-- SID=P for Personal Address
                        dtAccEntityAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityAdd.Rows.Count = 0 Then

                            objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            objWriter.WriteLine(Environment.NewLine & "Entity Address Missing")

                        End If


                        '-- entity director


                        dtAccEntityDir = db.ExecuteDataSet(CommandType.Text, "SELECT DIRECTOR_ID,ROLE FROM GO_DIRECTOR_ENTITY_MAP WHERE [STATUS]='L' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityDir.Rows.Count = 0 Then


                            objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            objWriter.WriteLine(Environment.NewLine & "Director Info Missing")

                        End If


                        For j = 0 To dtAccEntityDir.Rows.Count - 1


                            dtAccEntityDirDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_INFO WHERE [STATUS]='L' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "' ").Tables(0)


                            If dtAccEntityDirDet.Rows.Count = 0 Then


                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                                objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                objWriter.WriteLine(Environment.NewLine & "Director Info Missing")


                            End If

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("FIRST_NAME")) = "" Then


                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                                objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                objWriter.WriteLine(Environment.NewLine & "Director First Name Missing")

                            End If

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("LAST_NAME")) = "" Then


                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                                objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                objWriter.WriteLine(Environment.NewLine & "Director Last Name Missing")


                            End If

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("BIRTHDATE")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                                objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                objWriter.WriteLine(Environment.NewLine & "Director DOB Missing")



                            End If

                            'PREFIX=spouse name,ALIAS=fathers name
                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("PREFIX")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("ALIAS")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("MOTHERS_NAME")) = "" Then


                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                                objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                objWriter.WriteLine(Environment.NewLine & "Director Father,Mother,Spouse Name Missing")

                            End If


                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("NATIONALITY1")) = "" Then


                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                                objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                objWriter.WriteLine(Environment.NewLine & "Director Nationality1 Missing")


                            End If



                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("PASSPORT_NUMBER")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("ID_NUMBER")) = "" Then


                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                                objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                objWriter.WriteLine(Environment.NewLine & "National ID,Birth Regi No, Passport Number missing")


                            End If




                            '---National ID end----

                            If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("OCCUPATION")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                                objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                objWriter.WriteLine(Environment.NewLine & "Director occupation missing")


                            End If



                            '-- SID=P For Personal Phone 
                            dtAccEntityDirPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_PHONE WHERE [STATUS]='L' AND [SID]='P' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                            If dtAccEntityDirPhone.Rows.Count = 0 Then

                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                                objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                objWriter.WriteLine(Environment.NewLine & "Director Phone missing")


                            End If




                            '-- SID=P for Personal Address
                            dtAccEntityDirAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                            If dtAccEntityDirAdd.Rows.Count = 0 Then


                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                                objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                objWriter.WriteLine(Environment.NewLine & "Director Address missing")


                            End If



                            '--SID=E used for personal indentification
                            dtAccEntityDirIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                            If dtAccEntityDirIdent.Rows.Count = 0 Then

                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                                objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))
                                objWriter.WriteLine(Environment.NewLine & "Director Identification missing")


                            End If




                        Next j

                    End If

                    '----- end entity information

                    dtAccSig = db.ExecuteDataSet(CommandType.Text, "SELECT OWNER_CODE,ROLE_TYPE FROM FIU_TRANS_AC_OWNER WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "'").Tables(0)

                    If dtAccSig.Rows.Count = 0 Then

                        objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))

                        objWriter.WriteLine(Environment.NewLine & "Owner Info Missing")


                    End If




                    For j = 0 To dtAccSig.Rows.Count - 1

                        If NullHelper.ObjectToString(dtAccSig.Rows(j)("ROLE_TYPE")) = "" Then


                            objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))

                            objWriter.WriteLine(Environment.NewLine & "Owner Role Missing(Account Owner Mapping)")

                        End If


                        dtAccSigDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                        dtAccSigDetGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                        If dtAccSigDet.Rows.Count = 0 Then


                            objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))

                            objWriter.WriteLine(Environment.NewLine & "Owner Info Missing (CTR)")

                        End If

                        If dtAccSigDetGo.Rows.Count = 0 Then


                            objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))

                            objWriter.WriteLine(Environment.NewLine & "Owner Info Missing (goAML)")

                        End If

                        If dtAccSigDetGo.Rows.Count > 0 Then

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("FIRST_NAME")) = "" Then


                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))

                                objWriter.WriteLine(Environment.NewLine & "Owner First Name Missing")


                            End If

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("LAST_NAME")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))

                                objWriter.WriteLine(Environment.NewLine & "Owner Last Name Missing")

                            End If
                        End If

                        If dtAccSigDet.Rows.Count > 0 Then
                            If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("DOB")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))

                                objWriter.WriteLine(Environment.NewLine & "Owner DOB Missing")


                            End If


                            If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_SPOUSE")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_FATHER")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_MOTHER")) = "" Then


                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                objWriter.WriteLine(Environment.NewLine & "Owner Father,Mother,Spouse Name Missing")

                            End If
                        End If


                        If dtAccSigDetGo.Rows.Count > 0 Then

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("NATIONALITY1")) = "" Then


                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                objWriter.WriteLine(Environment.NewLine & "Owner Nationality1 Missing")

                            End If

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("RESIDENCE")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                objWriter.WriteLine(Environment.NewLine & "Owner Residence missing (goAML)")


                            End If

                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("OCP_CODE")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                objWriter.WriteLine(Environment.NewLine & "Owner occupation missing (goAML)")


                            End If


                            If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("PPNO")) = "" And NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("ID_NUMBER")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                objWriter.WriteLine(Environment.NewLine & "Owner National ID, Birth Regi No(goAML) and Passport No (CTR) Missing")


                            End If





                            '-- SID=P For Personal Phone 
                            dtSigPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                            If dtSigPersonPhone.Rows.Count = 0 Then

                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                objWriter.WriteLine(Environment.NewLine & "Owner Phone Missing ")


                            End If


                            '-- SID=P for Personal Address
                            dtSigPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                            If dtSigPersonAdd.Rows.Count = 0 Then

                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                objWriter.WriteLine(Environment.NewLine & "Owner Address Missing ")


                            End If




                            '--SID=E used for personal indentification
                            dtSigPersonIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                            If dtSigPersonIdent.Rows.Count = 0 Then


                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                objWriter.WriteLine(Environment.NewLine & "Owner Indentification Missing ")


                            End If

                        End If


                    Next j



                Else 'From Person


                    '-- start from person

                    dtBearer = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_INFO WHERE [STATUS]='L' AND REFERENCE_NUMBER='" & NullHelper.ObjectToString(dtTransation.Rows(i)("FROM_PERSON")) & "'").Tables(0)


                    If dtBearer.Rows.Count = 0 Then


                        objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                        objWriter.WriteLine(Environment.NewLine & "Bearer: " & dtTransation.Rows(i)("FROM_PERSON"))
                        objWriter.WriteLine(Environment.NewLine & "Bearer Info Missing ")


                    End If

                    If dtBearer.Rows.Count > 0 Then
                        'original bearer information processing start

                        For j = 0 To 0

                            If NullHelper.ObjectToString(dtBearer.Rows(0)("FIRST_NAME")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                objWriter.WriteLine(Environment.NewLine & "Depositor First Name Missing ")

                              
                            End If

                            If NullHelper.ObjectToString(dtBearer.Rows(0)("LAST_NAME")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                objWriter.WriteLine(Environment.NewLine & "Depositor Last Name Missing ")


                            End If



                            'SSN=National ID, ID_NUMBER= Birth Registration Number
                            If NullHelper.ObjectToString(dtBearer.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("PASSPORT_NUMBER")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("ID_NUMBER")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                objWriter.WriteLine(Environment.NewLine & "Depositor National ID/Passport No/Birth Registration No Missing ")

                            End If

                        Next j


                        'original bearer information processing end
                    Else

                        dtAccSig = db.ExecuteDataSet(CommandType.Text, "SELECT OWNER_CODE,ROLE_TYPE FROM FIU_TRANS_AC_OWNER WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "'").Tables(0)

                        If dtAccSig.Rows.Count = 0 Then


                            objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            objWriter.WriteLine(Environment.NewLine & "Owner Info Missing ")


                        End If

                        If dtAccSig.Rows.Count > 0 Then


                            For j = 0 To 0


                                dtAccSigDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                                dtAccSigDetGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)


                                If dtAccSigDet.Rows.Count = 0 Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Info Missing (CTR)")


                                End If


                                If dtAccSigDetGo.Rows.Count = 0 Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Info Missing (goAML)")


                                End If

                                If dtAccSigDetGo.Rows.Count > 0 Then

                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("FIRST_NAME")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner First Name Missing ")



                                    End If

                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("LAST_NAME")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Last Name Missing ")

                                    End If
                                End If


                                If dtAccSigDet.Rows.Count > 0 Then

                                    If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("DOB")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner DOB Missing ")

                                    End If


                                    If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_SPOUSE")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_FATHER")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_MOTHER")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Father,Mother,Spause Name Missing ")

                                    End If
                                End If

                                If dtAccSigDetGo.Rows.Count > 0 Then

                                    'SSN=National ID, ID_NUMBER= Birth Registration Number
                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("PPNO")) = "" And NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("ID_NUMBER")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner National ID/Passport No/Birth Registration No Missing ")

                                    End If



                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("NATIONALITY1")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Nationality1 Missing ")


                                    End If

                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("RESIDENCE")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Residence missing (goAML) ")

                                    End If

                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("OCP_CODE")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner occupation missing (goAML) ")


                                    End If




                                    '-- SID=P For Personal Phone 
                                    dtSigPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                                    If dtSigPersonPhone.Rows.Count = 0 Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Phone Missing ")


                                    End If


                                    '-- SID=P for Personal Address
                                    dtSigPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                                    If dtSigPersonAdd.Rows.Count = 0 Then


                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Address Missing ")


                                    End If



                                    '--SID=E used for personal indentification
                                    dtSigPersonIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                                    If dtSigPersonIdent.Rows.Count = 0 Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Indentification Missing ")


                                    End If

                                End If



                            Next j
                        End If

                        '-- end from person


                        End If


                End If




                If dtTransation.Rows(i)("TO_FLAG") = "A" Then 'To Account

                    dtTranAcc = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)


                    If dtTranAcc.Rows.Count = 0 Then

                        objWriter.WriteLine(Environment.NewLine & "A/C:  " & dtTransation.Rows(i)("ACCOUNT"))
                        objWriter.WriteLine(Environment.NewLine & "Account CTR info missing ")


                    End If


                    dtTranAccGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_ACCOUNT_INFO WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "' ").Tables(0)

                    If dtTranAccGo.Rows.Count = 0 Then

                        objWriter.WriteLine(Environment.NewLine & "A/C:  " & dtTransation.Rows(i)("ACCOUNT"))
                        objWriter.WriteLine(Environment.NewLine & "Account goAML info missing ")


                    End If





                    dtTranBranch = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_BANK_BRANCH WHERE [STATUS]='L' AND BRANCH_CODE='" & dtTranAcc.Rows(0)("BRANCH_CODE").ToString() & "' ").Tables(0)

                    If dtTranBranch.Rows.Count = 0 Then

                        objWriter.WriteLine(Environment.NewLine & "Branch Code:  " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                        objWriter.WriteLine(Environment.NewLine & "Branch Information not found")


                    End If

                    If dtTranAcc.Rows.Count > 0 Then

                        If NullHelper.ObjectToString(dtTranAcc.Rows(0)("AC_TITLE")) = "" Then

                            objWriter.WriteLine(Environment.NewLine & "A/C:  " & dtTransation.Rows(i)("ACCOUNT"))
                            'objWriter.WriteLine(Environment.NewLine & "Branch Code:  " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                            objWriter.WriteLine(Environment.NewLine & "Account Title not found")


                        End If
                    End If

                    If dtTranBranch.Rows.Count > 0 Then

                        If NullHelper.ObjectToString(dtTranBranch.Rows(0)("SWIFT_CODE")) = "" Then


                            objWriter.WriteLine(Environment.NewLine & "Branch Code:  " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                            objWriter.WriteLine(Environment.NewLine & "Swift  not found")


                        End If

                        If NullHelper.ObjectToString(dtTranBranch.Rows(0)("BRANCH_NAME")) = "" Then


                            objWriter.WriteLine(Environment.NewLine & "Branch Code:  " & dtTranAcc.Rows(0)("BRANCH_CODE"))
                            objWriter.WriteLine(Environment.NewLine & "Branch  name not found")



                        End If
                    End If



                    '---- entity information

                    dtAccEntity = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY WHERE [STATUS]='L' AND ENTITY_ID='" & NullHelper.ObjectToString(dtTranAccGo.Rows(0)("ENTITY_ID")) & "' ").Tables(0)


                    If dtAccEntity.Rows.Count = 0 Then

                        objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                        objWriter.WriteLine(Environment.NewLine & "Entity info missing")


                    End If

                    If dtAccEntity.Rows.Count > 0 Then


                        '-- SID=P For Personal Phone 
                        dtAccEntityPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_PHONE WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityPhone.Rows.Count = 0 Then

                            objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                            objWriter.WriteLine(Environment.NewLine & "Entity Phone missing")


                        End If




                        '-- SID=P for Personal Address
                        dtAccEntityAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_T_ENTITY_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityAdd.Rows.Count = 0 Then


                            objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                            objWriter.WriteLine(Environment.NewLine & "Entity Address missing")


                        End If



                        '-- entity director


                        dtAccEntityDir = db.ExecuteDataSet(CommandType.Text, "SELECT DIRECTOR_ID,ROLE FROM GO_DIRECTOR_ENTITY_MAP WHERE [STATUS]='L' AND ENTITY_ID='" & dtTranAccGo.Rows(0)("ENTITY_ID").ToString() & "'").Tables(0)

                        If dtAccEntityDir.Rows.Count = 0 Then

                            objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                            objWriter.WriteLine(Environment.NewLine & "Director info missing")

                        End If

                        If dtAccEntityDir.Rows.Count > 0 Then


                            For j = 0 To dtAccEntityDir.Rows.Count - 1


                                dtAccEntityDirDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_INFO WHERE [STATUS]='L' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "' ").Tables(0)


                                If dtAccEntityDirDet.Rows.Count = 0 Then

                                    objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))

                                    objWriter.WriteLine(Environment.NewLine & "Director info missing")


                                End If

                                If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("FIRST_NAME")) = "" Then

                                    objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))

                                    objWriter.WriteLine(Environment.NewLine & "Director First Name missing")





                                End If

                                If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("LAST_NAME")) = "" Then

                                    objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))

                                    objWriter.WriteLine(Environment.NewLine & "Director Last Name missing")


                                End If

                                If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("BIRTHDATE")) = "" Then


                                    objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))

                                    objWriter.WriteLine(Environment.NewLine & "Director DOB missing")


                                End If

                                'PREFIX=spouse name,ALIAS=fathers name
                                If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("PREFIX")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("ALIAS")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("MOTHERS_NAME")) = "" Then

                                    objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))

                                    objWriter.WriteLine(Environment.NewLine & "Director Father,Mother,Spouse Name missing")


                                End If

                                'SSN=National ID, ID_NUMBER= Birth Registration Number
                                If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("PASSPORT_NUMBER")) = "" And NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("ID_NUMBER")) = "" Then

                                    objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))

                                    objWriter.WriteLine(Environment.NewLine & "Director National ID/Passport No/Birth Registration No missing")

                                End If




                                If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("NATIONALITY1")) = "" Then


                                    objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))

                                    objWriter.WriteLine(Environment.NewLine & "Director Nationality1 missing")


                                End If



                                If NullHelper.ObjectToString(dtAccEntityDirDet.Rows(0)("OCCUPATION")) = "" Then


                                    objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))

                                    objWriter.WriteLine(Environment.NewLine & "Director occupation missing")

                                End If

                                '-- SID=P For Personal Phone 
                                dtAccEntityDirPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_PHONE WHERE [STATUS]='L' AND [SID]='P' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                                If dtAccEntityDirPhone.Rows.Count = 0 Then

                                    objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))

                                    objWriter.WriteLine(Environment.NewLine & "Director Phone missing")

                                End If



                                '-- SID=P for Personal Address
                                dtAccEntityDirAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                                If dtAccEntityDirAdd.Rows.Count = 0 Then


                                    objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))

                                    objWriter.WriteLine(Environment.NewLine & "Director Address missing")


                                End If


                                '--SID=E used for personal indentification
                                dtAccEntityDirIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_DIRECTOR_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND DIRECTOR_ID='" & dtAccEntityDir.Rows(j)("DIRECTOR_ID").ToString() & "'").Tables(0)

                                If dtAccEntityDirIdent.Rows.Count = 0 Then

                                    objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Director Code: " & dtAccEntityDir.Rows(j)("DIRECTOR_ID"))

                                    objWriter.WriteLine(Environment.NewLine & "Director Identification missing")


                                End If


                            Next j

                        End If

                    End If

                    '----- end entity information



                    dtAccSig = db.ExecuteDataSet(CommandType.Text, "SELECT OWNER_CODE,ROLE_TYPE FROM FIU_TRANS_AC_OWNER WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "'").Tables(0)

                    If dtAccSig.Rows.Count = 0 Then

                        objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))

                        objWriter.WriteLine(Environment.NewLine & "Owner Info missing")
                    End If

                    If dtAccSig.Rows.Count > 0 Then

                        For j = 0 To dtAccSig.Rows.Count - 1

                            ' role check

                            If NullHelper.ObjectToString(dtAccSig.Rows(j)("ROLE_TYPE")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "Account: " & dtTransation.Rows(i)("ACCOUNT"))
                                objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                objWriter.WriteLine(Environment.NewLine & "Owner Role Missing(Account Owner Mapping)")

                            End If

                            dtAccSigDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                            dtAccSigDetGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                            If dtAccSigDet.Rows.Count = 0 Then

                                objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                objWriter.WriteLine(Environment.NewLine & "Owner Info Missing (CTR)")


                            End If


                            If dtAccSigDetGo.Rows.Count = 0 Then

                                objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                objWriter.WriteLine(Environment.NewLine & "Owner Info Missing (goAML)")


                            End If

                            If dtAccSigDetGo.Rows.Count > 0 Then

                                If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("FIRST_NAME")) = "" Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner First Name Missing")


                                End If

                                If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("LAST_NAME")) = "" Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Last Name Missing")

                                End If

                            End If

                            If dtAccSigDet.Rows.Count > 0 Then

                                If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("DOB")) = "" Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner DOB Missing")


                                End If

                                If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_SPOUSE")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_FATHER")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_MOTHER")) = "" Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Father,Mother,Spause Name Missing")

                                End If

                            End If

                            If dtAccSigDetGo.Rows.Count > 0 Then

                                'SSN=National ID, ID_NUMBER= Birth Registration Number
                                If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("PPNO")) = "" And NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("ID_NUMBER")) = "" Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Director National ID/Passport No/Birth Registration No Missing")

                                End If



                                If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("NATIONALITY1")) = "" Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Nationality1 missing")

                                End If

                                If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("RESIDENCE")) = "" Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Residence missing(goAML)")


                                End If

                                If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("OCP_CODE")) = "" Then


                                    objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner occupation missing(goAML)")


                                End If



                                '-- SID=P For Personal Phone 
                                dtSigPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                                If dtSigPersonPhone.Rows.Count = 0 Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Phone missing")


                                End If



                                '-- SID=P for Personal Address
                                dtSigPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                                If dtSigPersonAdd.Rows.Count = 0 Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Address missing")


                                End If


                                '--SID=E used for personal indentification
                                dtSigPersonIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                                If dtSigPersonIdent.Rows.Count = 0 Then

                                    objWriter.WriteLine(Environment.NewLine & "Owner: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Indentification missing")


                                End If

                            End If



                        Next j
                    End If

                Else

                    '-- start to_person

                    dtBearer = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_BEARER_INFO WHERE [STATUS]='L' AND REFERENCE_NUMBER='" & NullHelper.ObjectToString(dtTransation.Rows(i)("TO_PERSON")) & "'").Tables(0)

                    If dtBearer.Rows.Count = 0 Then

                        objWriter.WriteLine(Environment.NewLine & "Bearer: " & dtTransation.Rows(i)("TO_PERSON"))
                        objWriter.WriteLine(Environment.NewLine & "Bearer info missing")


                    End If

                    If dtBearer.Rows.Count > 0 Then
                        'original bearer information processing start

                        For j = 0 To 0

                            If NullHelper.ObjectToString(dtBearer.Rows(0)("FIRST_NAME")) = "" Then


                                objWriter.WriteLine(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                objWriter.WriteLine(Environment.NewLine & "Depositor First Name Missing")


                            End If

                            If NullHelper.ObjectToString(dtBearer.Rows(0)("LAST_NAME")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                objWriter.WriteLine(Environment.NewLine & "Depositor Last Name Missing")

                            End If

                            'SSN=National ID, ID_NUMBER= Birth Registration Number
                            If NullHelper.ObjectToString(dtBearer.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("PASSPORT_NUMBER")) = "" And NullHelper.ObjectToString(dtBearer.Rows(0)("ID_NUMBER")) = "" Then

                                objWriter.WriteLine(Environment.NewLine & "Ref Code: " & dtTransation.Rows(i)("TRANSACTIONNUMBER"))
                                objWriter.WriteLine(Environment.NewLine & "Depositor National ID/Passport No/Birth Registration No Missing")


                            End If


                        Next j


                        'original bearer information processing end
                    Else

                        dtAccSig = db.ExecuteDataSet(CommandType.Text, "SELECT OWNER_CODE,ROLE_TYPE FROM FIU_TRANS_AC_OWNER WHERE [STATUS]='L' AND ACNUMBER='" & dtTransation.Rows(i)("ACCOUNT").ToString() & "'").Tables(0)

                        If dtAccSig.Rows.Count = 0 Then

                            objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                            objWriter.WriteLine(Environment.NewLine & "Owner Info Missing")


                        End If


                        If dtAccSig.Rows.Count > 0 Then

                            For j = 0 To 0


                                dtAccSigDet = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM FIU_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)

                                dtAccSigDetGo = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_INFO WHERE [STATUS]='L' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "' ").Tables(0)


                                If dtAccSigDet.Rows.Count = 0 Then

                                    objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))

                                    objWriter.WriteLine(Environment.NewLine & "Owner Info Missing (CTR)")


                                End If

                                If dtAccSigDetGo.Rows.Count = 0 Then

                                    objWriter.WriteLine(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                    objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))

                                    objWriter.WriteLine(Environment.NewLine & "Owner Info Missing (Goaml)")


                                End If

                                If dtAccSigDetGo.Rows.Count > 0 Then

                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("FIRST_NAME")) = "" Then

                                        'objWriter.Write(Environment.NewLine & "A/C: " & dtTransation.Rows(i)("ACCOUNT"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))

                                        objWriter.WriteLine(Environment.NewLine & "Owner First Name Missing")

                                    End If

                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("LAST_NAME")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))

                                        objWriter.WriteLine(Environment.NewLine & "Owner Last Name Missing")


                                    End If

                                End If

                                If dtAccSigDet.Rows.Count > 0 Then

                                    If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("DOB")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner DOB Missing")


                                    End If


                                    If NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_SPOUSE")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_FATHER")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("OWNER_MOTHER")) = "" Then


                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Father,Mother,Spause Name Missing")



                                    End If

                                End If

                                If dtAccSigDetGo.Rows.Count > 0 Then

                                    'SSN=National ID, ID_NUMBER= Birth Registration Number
                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("SSN")) = "" And NullHelper.ObjectToString(dtAccSigDet.Rows(0)("PPNO")) = "" And NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("ID_NUMBER")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner National ID/Passport No/Birth Registration No Missing")

                                    End If




                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("NATIONALITY1")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Nationality1 Missing")

                                    End If

                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("RESIDENCE")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Residence Missing (goAML)")


                                    End If

                                    If NullHelper.ObjectToString(dtAccSigDetGo.Rows(0)("OCP_CODE")) = "" Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner occupation Missing (goAML)")

                                    End If




                                    '-- SID=P For Personal Phone 
                                    dtSigPersonPhone = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_PHONE WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                                    If dtSigPersonPhone.Rows.Count = 0 Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Phone missing")


                                    End If


                                    '-- SID=P for Personal Address
                                    dtSigPersonAdd = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_ADDRESS WHERE [STATUS]='L' AND [SID]='P' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                                    If dtSigPersonAdd.Rows.Count = 0 Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Address missing")

                                    End If


                                    '--SID=E used for personal indentification
                                    dtSigPersonIdent = db.ExecuteDataSet(CommandType.Text, "SELECT * FROM GO_OWNER_IDENTIFICATION WHERE [STATUS]='L' AND [SID]='E' AND OWNER_CODE='" & dtAccSig.Rows(j)("OWNER_CODE").ToString() & "'").Tables(0)

                                    If dtSigPersonIdent.Rows.Count = 0 Then

                                        objWriter.WriteLine(Environment.NewLine & "Owner Code: " & dtAccSig.Rows(j)("OWNER_CODE"))
                                        objWriter.WriteLine(Environment.NewLine & "Owner Indentification missing")

                                    End If

                                End If


                            Next j

                        End If



                        End If '-- end of bearer condition



                End If

                '--- end transaction
            Next i


            '_ProcessSuccess = True

            flagProcessSuccess = True

        Catch ex As Exception
            MessageBox.Show(ex.Message + "Line: " + ErrorRow.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            flagProcessSuccess = False
        End Try

        objWriter.Close()

        Return flagProcessSuccess  'Success file created with no error


    End Function

    Private Sub FileGenProcess()

        Try



            If CheckData() Then

                ExportXML()


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub


    Private Sub XFileGenProcess()

        Dim errLevel As String = "0"

        Dim flagExpProcSucc As Boolean = False

        If txtTransacrionNumber.Text.Trim() = "" Then
            MessageBox.Show("Number of Transaction required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)


            Exit Sub
        End If

        If txtXmlFileName.Text.Trim() = "" Then
            MessageBox.Show("Xml File Name required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)


            Exit Sub
        End If

        Try

            If CheckData() Then

              
                errLevel = "1"

                'ExportXML()

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim dtTransation As DataTable

                Dim commTrans As DbCommand

                commTrans = db.GetSqlStringCommand("SELECT * FROM GO_TRANSACTION WHERE [STATUS]='L' AND " & _
                                                   "DATE_TRANSACTION>=@P_TXN_DATE_FROM AND DATE_TRANSACTION<=@P_TXN_DATE_TO")

                commTrans.Parameters.Clear()

                db.AddInParameter(commTrans, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(commTrans, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                dtTransation = db.ExecuteDataSet(commTrans).Tables(0)

                If dtTransation.Rows.Count = 0 Then
                    MessageBox.Show("No transaction found for the selected date range", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim TrnsNumber As Integer = txtTransacrionNumber.Text.Trim()

                Dim noFile As Integer = Math.Ceiling(dtTransation.Rows.Count / TrnsNumber)
                'noFile = 3
                Dim strFileName As String = ""

                Dim rowStart As Integer = 0
                Dim rowEnd As Integer = IIf(dtTransation.Rows.Count >= TrnsNumber, TrnsNumber - 1, dtTransation.Rows.Count - 1)

                errLevel = "2"

                For cntFile = 1 To noFile

                    'rowStart = 0
                    'rowEnd = 0

                    flagExpProcSucc = False

                    ' strFileName = txtXmlFileName.Text.Trim() & "-" & Today.ToString("yyyyMMdd") & "_" & cntFile.ToString()


                    Dim SDate As DateTime
                    Dim MonthName As String
                    Dim year As Integer

                    SDate = NullHelper.StringToDate(txtDateFrom.Text.Trim())

                    MonthName = SDate.ToString("MMMM")
                    year = SDate.ToString("yyyy")


                    strFileName = txtXmlFileName.Text.Trim() & "-" & MonthName.ToString() & "-" & year.ToString() & "-" & cntFile.ToString()



                    'strFileName = txtXmlFileName.Text.Trim() & "-" & Today.ToString("MMMM") & "-" & Today.ToString("yyyy") & "-" & cntFile.ToString()

                    flagExpProcSucc = ExportXML(strFileName, db, dtTransation, rowStart, rowEnd)

                    If flagExpProcSucc = False Then
                        SetProcessStatus(Environment.NewLine + "File generation failed!!")
                        Exit For
                    End If

                    rowStart = rowStart + TrnsNumber
                    'rowStart = rowStart + 400
                    'rowEnd = rowStart + 500
                    rowEnd = IIf(dtTransation.Rows.Count >= rowStart + TrnsNumber, rowStart + TrnsNumber - 1, dtTransation.Rows.Count - 1)


                Next

                _ProcessSuccess = True



            End If

        Catch ex As Exception
            MessageBox.Show("Err Proc: XFileGenProcess" + vbNewLine + _
                            "Level: " + errLevel + vbNewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub


    Private Sub ReportFileGenProcess()

        Dim flagExpProcSucc2 As Boolean = False

        If txtTransacrionNumber.Text.Trim() = "" Then
            MessageBox.Show("Number of Transaction required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)


            Exit Sub
        End If

        If txtXmlFileName.Text.Trim() = "" Then
            MessageBox.Show("Xml File Name required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)


            Exit Sub
        End If



        Try

            If CheckData() Then

               

                'ExportXML()

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)

                Dim dtTransation As DataTable

                Dim commTrans As DbCommand

                commTrans = db.GetSqlStringCommand("SELECT * FROM GO_TRANSACTION WHERE [STATUS]='L' AND " & _
                                                   "DATE_TRANSACTION>=@P_TXN_DATE_FROM AND DATE_TRANSACTION<=@P_TXN_DATE_TO")

                commTrans.Parameters.Clear()

                db.AddInParameter(commTrans, "@P_TXN_DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim))
                db.AddInParameter(commTrans, "@P_TXN_DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim))

                dtTransation = db.ExecuteDataSet(commTrans).Tables(0)

                If dtTransation.Rows.Count = 0 Then
                    MessageBox.Show("No transaction found for the selected date range", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim TrnsNumber As Integer = txtTransacrionNumber.Text.Trim()

                Dim noFile As Integer = Math.Ceiling(dtTransation.Rows.Count / TrnsNumber)
                'noFile = 3
                Dim strFileName As String = ""

                Dim rowStart As Integer = 0
                Dim rowEnd As Integer = IIf(dtTransation.Rows.Count >= TrnsNumber, TrnsNumber - 1, dtTransation.Rows.Count - 1)



                For cntFile = 1 To noFile

                    'rowStart = 0
                    'rowEnd = 0

                    flagExpProcSucc2 = False

                    ' strFileName = "ExpGO_" & Today.ToString("yyyyMMdd") & "_" & cntFile.ToString()


                    Dim SDate As DateTime
                    Dim MonthName As String
                    Dim year As Integer

                    SDate = NullHelper.StringToDate(txtDateFrom.Text.Trim())

                    MonthName = SDate.ToString("MMMM")
                    year = SDate.ToString("yyyy")


                    strFileName = txtXmlFileName.Text.Trim() & "-" & MonthName.ToString() & "-" & year.ToString() & "-" & cntFile.ToString()




                    flagExpProcSucc2 = ExportFILE(strFileName, db, dtTransation, rowStart, rowEnd)

                    'If flagExpProcSucc2 = False Then
                    '    SetProcessStatus(Environment.NewLine + "File generation failed!!")
                    '    Exit For
                    'End If

                    rowStart = rowStart + TrnsNumber
                    'rowStart = rowStart + 400
                    'rowEnd = rowStart + 500
                    rowEnd = IIf(dtTransation.Rows.Count >= rowStart + TrnsNumber, rowStart + TrnsNumber - 1, dtTransation.Rows.Count - 1)


                Next

                _ProcessSuccess = True



            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub


    Private Sub SetProcessStatus(ByVal strMsg As String)

        'txtFileImpStatus.AppendText(strMsg)


        If txtProcessStatus.InvokeRequired Then
            txtProcessStatus.Invoke(New Action(Of System.String)(AddressOf SetProcessStatus), strMsg)
        Else
            txtProcessStatus.AppendText(strMsg)
        End If

    End Sub

#End Region

    Private Sub FrmExportGoAML_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If BackgroundWorker1.IsBusy = True Then
            MessageBox.Show("Process is running.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            e.Cancel = True
        End If

    End Sub


    Private Sub FrmExportGoAML_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtSubmissionDate.Text = DateTime.Now.ToString("dd/MM/yyyy")


        'If opt.IsShow = False Then
        '    MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Me.Close()
        'End If
        LoadReportData()
        DisableFields()

    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        fbdTransFile.ShowNewFolderButton = False
        fbdTransFile.ShowDialog()
        txtFolderPath.Text = fbdTransFile.SelectedPath

    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click

        txtProcessStatus.Text = ""

        ProgressBar1.Style = ProgressBarStyle.Marquee
        btnExport.Enabled = False
        btnBrowse.Enabled = False
        btnCheck.Enabled = True

        BackgroundWorker1.RunWorkerAsync()

     


        

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        _ProcessSuccess = False

        'FileGenProcess()
        XFileGenProcess()

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted


        ProgressBar1.Style = ProgressBarStyle.Continuous

        btnExport.Enabled = True
        btnBrowse.Enabled = True
        btnCheck.Enabled = True
        
        If _ProcessSuccess = True Then
            MessageBox.Show("Completed!!", "File Upload Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If


    End Sub

    Private Sub btnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheck.Click
        txtProcessStatus.Text = ""

        ProgressBar1.Style = ProgressBarStyle.Marquee
        btnExport.Enabled = False
        btnBrowse.Enabled = False
        btnCheck.Enabled = False

        BackgroundWorker2.RunWorkerAsync()

    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        _ProcessSuccess = False

        'FileGenProcess()
        ReportFileGenProcess()
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        ProgressBar1.Style = ProgressBarStyle.Continuous

        btnExport.Enabled = True
        btnBrowse.Enabled = True
        btnCheck.Enabled = True

        If _ProcessSuccess = True Then
            MessageBox.Show("Completed!!", "Missing Report Status", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub
End Class