Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Data.SqlClient

Public Class FrmExportAll
#Region "Global Variable"
    Public Shared strpath As String
    Private flagTransExist As Boolean
    Dim log_message As String

    Dim _formName As String = "ToolsExportAll"
    Dim opt As SecForm = New SecForm(_formName)

#End Region

#Region "UserdefineFunction"

    Private Sub ExpThana()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_THANA.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "THANA_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "fiu_Thana")
        xmlwrt.WriteAttributeString("rs:basecolumn", "THANA_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "6")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "THANA_NAME")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "fiu_Thana")
        xmlwrt.WriteAttributeString("rs:basecolumn", "THANA_NAME")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DIST_CODE")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "fiu_Thana")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DIST_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteEndElement() 'end s:datatype dist code
        xmlwrt.WriteEndElement() 'end s:attributetype dist code



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PREV_THANA_CODE")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "fiu_Thana")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PREV_THANA_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "6")
        xmlwrt.WriteEndElement() 'end s:datatype prev_thana code
        xmlwrt.WriteEndElement() 'end s:attributetype prev_thana code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "fiu_Thana")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "fiu_Thana")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "fiu_Thana")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "fiu_Thana")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "9")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "fiu_Thana")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "10")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "fiu_Thana")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select THANA_CODE,THANA_NAME,DIST_CODE,PREV_THANA_CODE,INSERTED_ON,MODIFIED_ON from FIU_THANA where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")


            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("THANA_CODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("THANA_NAME", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("DIST_CODE", ds.Tables(0).Rows(i)(2).ToString())
                    xmlwrt.WriteAttributeString("PREV_THANA_CODE", ds.Tables(0).Rows(i)(3).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(4)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(5).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(5)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub


    Private Sub ExpAccountInfo()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_ACCOUNT_INFO.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BANK_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BANK_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BRANCH_CODE")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BRANCH_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "ACNUMBER")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "ACNUMBER")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "AC_TITLE")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "AC_TITLE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "200")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "ACTYPECODE")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "ACTYPECODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OWTYPECODE")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OWTYPECODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DECLARED_DEPOSIT_AMOUNT")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DECLARED_DEPOSIT_AMOUNT")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "float")
        xmlwrt.WriteAttributeString("dt:maxLenth", "8")
        xmlwrt.WriteAttributeString("rs:precision", "15")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DECLARED_DEPOSIT_TRANSNO")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DECLARED_DEPOSIT_TRANSNO")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "int")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:precision", "10")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DECLARED_DEPOSIT_MAXAMOUNT")
        xmlwrt.WriteAttributeString("rs:number", "9")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DECLARED_DEPOSIT_MAXAMOUNT")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "float")
        xmlwrt.WriteAttributeString("dt:maxLenth", "8")
        xmlwrt.WriteAttributeString("rs:precision", "15")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DECLARED_WITHDR_AMOUNT")
        xmlwrt.WriteAttributeString("rs:number", "10")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DECLARED_WITHDR_AMOUNT")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "float")
        xmlwrt.WriteAttributeString("dt:maxLenth", "8")
        xmlwrt.WriteAttributeString("rs:precision", "15")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DECLARED_WITHDR_TRANSNO")
        xmlwrt.WriteAttributeString("rs:number", "11")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DECLARED_WITHDR_TRANSNO")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "int")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:precision", "10")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DECLARED_WITHDR_MAXAMOUNT")
        xmlwrt.WriteAttributeString("rs:number", "12")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DECLARED_WITHDR_MAXAMOUNT")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "float")
        xmlwrt.WriteAttributeString("dt:maxLenth", "8")
        xmlwrt.WriteAttributeString("rs:precision", "15")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "TIN")
        xmlwrt.WriteAttributeString("rs:number", "13")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "TIN")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BIN")
        xmlwrt.WriteAttributeString("rs:number", "14")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BIN")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "VAT_REG_NO")
        xmlwrt.WriteAttributeString("rs:number", "15")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "VAT_REG_NO")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "VAT_REG_DATE")
        xmlwrt.WriteAttributeString("rs:number", "16")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "VAT_REG_DATE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "COMPANY_REG_NO")
        xmlwrt.WriteAttributeString("rs:number", "17")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "COMPANY_REG_NO")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "COMPANY_REG_DATE")
        xmlwrt.WriteAttributeString("rs:number", "18")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "COMPANY_REG_DATE")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "REG_AUTHORITY_CODE")
        xmlwrt.WriteAttributeString("rs:number", "19")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "REG_AUTHORITY_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PRES_ADDR")
        xmlwrt.WriteAttributeString("rs:number", "20")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PRES_ADDR")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "200")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PERM_ADDR")
        xmlwrt.WriteAttributeString("rs:number", "21")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PERM_ADDR")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "200")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_RES1")
        xmlwrt.WriteAttributeString("rs:number", "22")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_RES1")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_RES2")
        xmlwrt.WriteAttributeString("rs:number", "23")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_RES2")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_OFFICE1")
        xmlwrt.WriteAttributeString("rs:number", "24")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_OFFICE1")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_OFFICE2")
        xmlwrt.WriteAttributeString("rs:number", "25")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_OFFICE2")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MOBILE1")
        xmlwrt.WriteAttributeString("rs:number", "26")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MOBILE1")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MOBILE2")
        xmlwrt.WriteAttributeString("rs:number", "27")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MOBILE2")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OLD_ACNUMBER")
        xmlwrt.WriteAttributeString("rs:number", "28")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OLD_ACNUMBER")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OLD_CODE_UPDATED_ON")
        xmlwrt.WriteAttributeString("rs:number", "29")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OLD_CODE_UPDATED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OLD_CODE_UPDATED_BY")
        xmlwrt.WriteAttributeString("rs:number", "30")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OLD_CODE_UPDATED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype




        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "31")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "32")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "33")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "34")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "35")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "36")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        If flagTransExist = True Then

            Try

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)
                'Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from TEMP_ACCOUNT_INFO where (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

                Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from TEMP_ACCOUNT_INFO")

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


                If ds.Tables(0).Rows.Count > 0 Then
                    Dim i As Integer = 0

                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        xmlwrt.WriteStartElement("z:row", Nothing)
                        xmlwrt.WriteAttributeString("BANK_CODE", ds.Tables(0).Rows(i)(0).ToString())
                        xmlwrt.WriteAttributeString("BRANCH_CODE", ds.Tables(0).Rows(i)(1).ToString())
                        xmlwrt.WriteAttributeString("ACNUMBER", ds.Tables(0).Rows(i)(2).ToString())
                        xmlwrt.WriteAttributeString("AC_TITLE", ds.Tables(0).Rows(i)(3).ToString())

                        If Not (ds.Tables(0).Rows(i)(4) Is DBNull.Value Or (ds.Tables(0).Rows(i)(4) Is Nothing) Or ds.Tables(0).Rows(i)(4).ToString() = "") Then
                            xmlwrt.WriteAttributeString("ACTYPECODE", ds.Tables(0).Rows(i)(4).ToString())
                        End If
                        'xmlwrt.WriteAttributeString("ACTYPECODE", ds.Tables(0).Rows(i)(4).ToString())

                        If Not (ds.Tables(0).Rows(i)(5) Is DBNull.Value Or (ds.Tables(0).Rows(i)(5) Is Nothing) Or ds.Tables(0).Rows(i)(5).ToString() = "") Then
                            xmlwrt.WriteAttributeString("OWTYPECODE", ds.Tables(0).Rows(i)(5).ToString())
                        End If
                        'xmlwrt.WriteAttributeString("OWTYPECODE", ds.Tables(0).Rows(i)(5).ToString())

                        xmlwrt.WriteAttributeString("DECLARED_DEPOSIT_AMOUNT", NullHelper.NumToXML(ds.Tables(0).Rows(i)(6)).ToString())
                        xmlwrt.WriteAttributeString("DECLARED_DEPOSIT_TRANSNO", NullHelper.NumToXML(ds.Tables(0).Rows(i)(7)).ToString())
                        xmlwrt.WriteAttributeString("DECLARED_DEPOSIT_MAXAMOUNT", NullHelper.NumToXML(ds.Tables(0).Rows(i)(8)).ToString())
                        xmlwrt.WriteAttributeString("DECLARED_WITHDR_AMOUNT", NullHelper.NumToXML(ds.Tables(0).Rows(i)(9)).ToString())
                        xmlwrt.WriteAttributeString("DECLARED_WITHDR_TRANSNO", NullHelper.NumToXML(ds.Tables(0).Rows(i)(10)).ToString())
                        xmlwrt.WriteAttributeString("DECLARED_WITHDR_MAXAMOUNT", NullHelper.NumToXML(ds.Tables(0).Rows(i)(11)).ToString())
                        xmlwrt.WriteAttributeString("TIN", ds.Tables(0).Rows(i)(12).ToString())
                        xmlwrt.WriteAttributeString("BIN", ds.Tables(0).Rows(i)(13).ToString())
                        xmlwrt.WriteAttributeString("VAT_REG_NO", ds.Tables(0).Rows(i)(14).ToString())
                        If Not (ds.Tables(0).Rows(i)(15) Is DBNull.Value Or (ds.Tables(0).Rows(i)(15) Is Nothing) Or ds.Tables(0).Rows(i)(15).ToString() = "") Then
                            xmlwrt.WriteAttributeString("VAT_REG_DATE", Convert.ToDateTime(ds.Tables(0).Rows(i)(15)).ToString("yyyy-MM-ddT00:00:00"))
                        End If

                        xmlwrt.WriteAttributeString("COMPANY_REG_NO", ds.Tables(0).Rows(i)(16).ToString())

                        If Not (ds.Tables(0).Rows(i)(17) Is DBNull.Value Or (ds.Tables(0).Rows(i)(17) Is Nothing) Or ds.Tables(0).Rows(i)(17).ToString() = "") Then
                            xmlwrt.WriteAttributeString("COMPANY_REG_DATE", Convert.ToDateTime(ds.Tables(0).Rows(i)(17)).ToString("yyyy-MM-ddT00:00:00"))
                        End If



                        If Not (ds.Tables(0).Rows(i)(18) Is DBNull.Value Or (ds.Tables(0).Rows(i)(18) Is Nothing) Or ds.Tables(0).Rows(i)(18).ToString() = "") Then
                            xmlwrt.WriteAttributeString("REG_AUTHORITY_CODE", ds.Tables(0).Rows(i)(18).ToString())
                        End If



                        xmlwrt.WriteAttributeString("PRES_ADDR", ds.Tables(0).Rows(i)(19).ToString())
                        xmlwrt.WriteAttributeString("PERM_ADDR", ds.Tables(0).Rows(i)(20).ToString())
                        xmlwrt.WriteAttributeString("PHONE_RES1", ds.Tables(0).Rows(i)(21).ToString())
                        xmlwrt.WriteAttributeString("PHONE_RES2", ds.Tables(0).Rows(i)(22).ToString())
                        xmlwrt.WriteAttributeString("PHONE_OFFICE1", ds.Tables(0).Rows(i)(23).ToString())
                        xmlwrt.WriteAttributeString("PHONE_OFFICE2", ds.Tables(0).Rows(i)(24).ToString())
                        xmlwrt.WriteAttributeString("MOBILE1", ds.Tables(0).Rows(i)(25).ToString())
                        xmlwrt.WriteAttributeString("MOBILE2", ds.Tables(0).Rows(i)(26).ToString())
                        xmlwrt.WriteAttributeString("OLD_ACNUMBER", ds.Tables(0).Rows(i)(27).ToString())
                        If Not (ds.Tables(0).Rows(i)(28) Is DBNull.Value Or (ds.Tables(0).Rows(i)(28) Is Nothing) Or ds.Tables(0).Rows(i)(28).ToString() = "") Then
                            xmlwrt.WriteAttributeString("OLD_CODE_UPDATED_ON", NullHelper.DateToXML(ds.Tables(0).Rows(i)(28)))
                        End If

                        xmlwrt.WriteAttributeString("OLD_CODE_UPDATED_BY", ds.Tables(0).Rows(i)(29).ToString())
                        'xmlwrt.WriteAttributeString("INSERTED_ON", ds.Tables(0).Rows(i)(32).ToString())

                        If Not (ds.Tables(0).Rows(i)(32) Is DBNull.Value Or (ds.Tables(0).Rows(i)(32) Is Nothing) Or ds.Tables(0).Rows(i)(32).ToString() = "") Then
                            xmlwrt.WriteAttributeString("INSERTED_ON", NullHelper.DateToXML(ds.Tables(0).Rows(i)(32)))
                        End If



                        If ds.Tables(0).Rows(i)(35).ToString() <> "" Then
                            xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(35)).ToString("yyyy-MM-ddT00:00:00"))
                        End If
                        xmlwrt.WriteEndElement() 'end z:row 
                    Next i
                End If
            Catch ex As SqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

            End Try

        End If


        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub


    Private Sub ExpAccTypes()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_ACCOUNT_TYPES.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "ACTYPECODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "ACTYPECODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "ACDEFINITION")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "ACDEFINITION")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ACCOUNT_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select ACTYPECODE,ACDEFINITION,INSERTED_ON,MODIFIED_ON from FIU_ACCOUNT_TYPES where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("ACTYPECODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("ACDEFINITION", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(2)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(3).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub


    Private Sub ExpAssessment_Duration()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_ASSESSMENT_DURATION.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DUR_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ASSESSMENT_DURATION")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DUR_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DURDEFINITION")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ASSESSMENT_DURATION")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DURDEFINITION")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ASSESSMENT_DURATION")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ASSESSMENT_DURATION")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ASSESSMENT_DURATION")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ASSESSMENT_DURATION")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ASSESSMENT_DURATION")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_ASSESSMENT_DURATION")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select DUR_CODE,DURDEFINITION,INSERTED_ON,MODIFIED_ON from FIU_ASSESSMENT_DURATION where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("DUR_CODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("DURDEFINITION", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(2)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(3).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub



    Private Sub ExpBank()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_BANK.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BANK_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BANK_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BANK_NAME")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BANK_NAME")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "100")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "GCODE")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "GCODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "10")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OLD_BANK_CODE")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OLD_BANK_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OLD_CODE_UPDATED_ON")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OLD_CODE_UPDATED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OLD_CODE_UPDATED_BY")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OLD_CODE_UPDATED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "9")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "10")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "11")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "12")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select BANK_CODE,BANK_NAME,GCODE,OLD_BANK_CODE,OLD_CODE_UPDATED_ON,OLD_CODE_UPDATED_BY,INSERTED_ON,MODIFIED_ON from FIU_BANK where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("BANK_CODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("BANK_NAME", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("GCODE", ds.Tables(0).Rows(i)(2).ToString())
                    xmlwrt.WriteAttributeString("OLD_BANK_CODE", ds.Tables(0).Rows(i)(3).ToString())
                    xmlwrt.WriteAttributeString("OLD_CODE_UPDATED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(4)).ToString("yyyy-MM-ddT00:00:00"))
                    xmlwrt.WriteAttributeString("OLD_CODE_UPDATED_BY", ds.Tables(0).Rows(i)(5).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(6)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(7).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(7)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub


    Private Sub ExpBankBranch()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_BANK_BRANCH.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BRANCH_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BRANCH_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BANK_CODE")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BANK_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BRANCH_NAME")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BRANCH_NAME")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "100")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BRANCH_LOC")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BRANCH_LOC")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "200")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "THANA_CODE")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "THANA_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "6")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DIST_CODE")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DIST_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PO")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PO")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "100")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "UNI_MUN")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "UNI_MUN")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "3")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "WARD")
        xmlwrt.WriteAttributeString("rs:number", "9")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "WARD")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BRANCH_NO")
        xmlwrt.WriteAttributeString("rs:number", "10")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BRANCH_NO")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OLD_BRANCH_CODE")
        xmlwrt.WriteAttributeString("rs:number", "11")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OLD_BRANCH_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OLD_CODE_UPDATED_ON")
        xmlwrt.WriteAttributeString("rs:number", "12")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OLD_CODE_UPDATED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OLD_CODE_UPDATED_BY")
        xmlwrt.WriteAttributeString("rs:number", "13")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OLD_CODE_UPDATED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "14")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "15")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "16")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "17")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "18")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "19")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_BANK_BRANCH")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select BRANCH_CODE,BANK_CODE,BRANCH_NAME,BRANCH_LOC,THANA_CODE,DIST_CODE,PO,UNI_MUN,WARD,BRANCH_NO,OLD_BRANCH_CODE,OLD_CODE_UPDATED_ON,OLD_CODE_UPDATED_BY,INSERTED_ON,MODIFIED_ON from FIU_BANK_BRANCH where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("BRANCH_CODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("BANK_CODE", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("BRANCH_NAME", ds.Tables(0).Rows(i)(2).ToString())
                    xmlwrt.WriteAttributeString("BRANCH_LOC", ds.Tables(0).Rows(i)(3).ToString())
                    xmlwrt.WriteAttributeString("THANA_CODE", ds.Tables(0).Rows(i)(4).ToString())
                    xmlwrt.WriteAttributeString("DIST_CODE", ds.Tables(0).Rows(i)(5).ToString())
                    xmlwrt.WriteAttributeString("PO", ds.Tables(0).Rows(i)(6).ToString())
                    xmlwrt.WriteAttributeString("UNI_MUN", ds.Tables(0).Rows(i)(7).ToString())
                    xmlwrt.WriteAttributeString("WARD", ds.Tables(0).Rows(i)(8).ToString())
                    xmlwrt.WriteAttributeString("BRANCH_NO", ds.Tables(0).Rows(i)(9).ToString())
                    xmlwrt.WriteAttributeString("OLD_BRANCH_CODE", ds.Tables(0).Rows(i)(10).ToString())
                    xmlwrt.WriteAttributeString("OLD_CODE_UPDATED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(11)).ToString("yyyy-MM-ddT00:00:00"))
                    xmlwrt.WriteAttributeString("OLD_CODE_UPDATED_BY", ds.Tables(0).Rows(i)(12).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(13)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(14).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(14)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub


    Private Sub ExpCmp_Reg_Auth()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_COMPANY_REG_AUTHORITY.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "REG_AUTHORITY_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COMPANY_REG_AUTHORITY")
        xmlwrt.WriteAttributeString("rs:basecolumn", "REG_AUTHORITY_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "REG_AUTHORITY_NAME")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COMPANY_REG_AUTHORITY")
        xmlwrt.WriteAttributeString("rs:basecolumn", "REG_AUTHORITY_NAME")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "100")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COMPANY_REG_AUTHORITY")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COMPANY_REG_AUTHORITY")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COMPANY_REG_AUTHORITY")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COMPANY_REG_AUTHORITY")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COMPANY_REG_AUTHORITY")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COMPANY_REG_AUTHORITY")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select REG_AUTHORITY_CODE,REG_AUTHORITY_NAME,INSERTED_ON,MODIFIED_ON from FIU_COMPANY_REG_AUTHORITY where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("REG_AUTHORITY_CODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("REG_AUTHORITY_NAME", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(2)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(3).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub


    Private Sub ExpCountry_Info()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_COUNTRY_INFO.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "COUNTRY_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COUNTRY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "COUNTRY_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "COUNTRY_NAME")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COUNTRY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "COUNTRY_NAME")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "100")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COUNTRY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COUNTRY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COUNTRY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COUNTRY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COUNTRY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_COUNTRY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select COUNTRY_CODE,COUNTRY_NAME,INSERTED_ON,MODIFIED_ON from FIU_COUNTRY_INFO where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("COUNTRY_CODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("COUNTRY_NAME", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(2)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(3).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub


    Private Sub ExpCurrency_Info()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_CURRENCY_INFO.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "CURRENCY_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_CURRENCY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "CURRENCY_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "3")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "CURRENCY_NAME")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_CURRENCY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "CURRENCY_NAME")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "100")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_CURRENCY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_CURRENCY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_CURRENCY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_CURRENCY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_CURRENCY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_CURRENCY_INFO")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select CURRENCY_CODE,CURRENCY_NAME,INSERTED_ON,MODIFIED_ON from FIU_CURRENCY_INFO where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("CURRENCY_CODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("CURRENCY_NAME", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(2)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(3).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub


    Private Sub ExpDistrict()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_DISTRICT.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DIST_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DISTRICT")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DIST_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DIV_CODE")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DISTRICT")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DIV_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DIST_NAME")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DISTRICT")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DIST_NAME")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PREV_DIST_CODE")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DISTRICT")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PREV_DIST_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "6")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DISTRICT")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DISTRICT")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DISTRICT")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DISTRICT")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "9")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DISTRICT")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "10")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DISTRICT")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select DIST_CODE,DIV_CODE,DIST_NAME,PREV_DIST_CODE,INSERTED_ON,MODIFIED_ON from FIU_DISTRICT where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("DIST_CODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("DIV_CODE", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("DIST_NAME", ds.Tables(0).Rows(i)(2).ToString())
                    xmlwrt.WriteAttributeString("PREV_DIST_CODE", ds.Tables(0).Rows(i)(3).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(4)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(5).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(5)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub


    Private Sub ExpDivision()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_DIVISION.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DIV_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DIVISION")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DIV_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DIV_NAME")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DIVISION")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DIV_NAME")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PREV_DIV_CODE")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_DIVISION")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PREV_DIV_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "6")
        xmlwrt.WriteEndElement() 'end s:datatype
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select DIV_CODE,DIV_NAME,PREV_DIV_CODE from FIU_DIVISION where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("DIV_CODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("DIV_NAME", ds.Tables(0).Rows(i)(1).ToString())

                    If ds.Tables(0).Rows(i)(2).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("PREV_DIV_CODE", ds.Tables(0).Rows(i)(2).ToString())
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub

    Private Sub ExpExecutive_Desig()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_EXECUTIVE_DESIG.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "EXE_DESIG_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_EXECUTIVE_DESIG")
        xmlwrt.WriteAttributeString("rs:basecolumn", "EXE_DESIG_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "3")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "EXE_DESIG_NAME")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_EXECUTIVE_DESIG")
        xmlwrt.WriteAttributeString("rs:basecolumn", "EXE_DESIG_NAME")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_EXECUTIVE_DESIG")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_EXECUTIVE_DESIG")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_EXECUTIVE_DESIG")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_EXECUTIVE_DESIG")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_EXECUTIVE_DESIG")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_EXECUTIVE_DESIG")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select EXE_DESIG_CODE,EXE_DESIG_NAME,INSERTED_ON,MODIFIED_ON from FIU_EXECUTIVE_DESIG where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("EXE_DESIG_CODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("EXE_DESIG_NAME", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(2)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(3).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub


    Private Sub ExpHoliday()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_HOLIDAYS.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "HOLIDAY")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_HOLIDAYS")
        xmlwrt.WriteAttributeString("rs:basecolumn", "HOLIDAY")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "TYPE")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_HOLIDAYS")
        xmlwrt.WriteAttributeString("rs:basecolumn", "TYPE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "1")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "REASON")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_HOLIDAYS")
        xmlwrt.WriteAttributeString("rs:basecolumn", "REASON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "150")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_HOLIDAYS")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_HOLIDAYS")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_HOLIDAYS")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_HOLIDAYS")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_HOLIDAYS")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "9")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_HOLIDAYS")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select HOLIDAY,TYPE,REASON,INSERTED_ON,MODIFIED_ON from FIU_HOLIDAYS where (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("HOLIDAY", Convert.ToDateTime(ds.Tables(0).Rows(i)(0)).ToString("yyyy-MM-ddT00:00:00"))
                    xmlwrt.WriteAttributeString("TYPE", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("REASON", ds.Tables(0).Rows(i)(2).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(4).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(4)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()


    End Sub


    Private Sub ExpOccupation_Types()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_OCCUPATION_TYPES.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OCTYPECODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OCCUPATION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OCTYPECODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OCDEFINITION")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OCCUPATION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OCDEFINITION")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OCCUPATION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OCCUPATION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OCCUPATION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OCCUPATION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OCCUPATION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OCCUPATION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select OCTYPECODE,OCDEFINITION,INSERTED_ON,MODIFIED_ON from FIU_OCCUPATION_TYPES where STATUS='L' and  (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("OCTYPECODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("OCDEFINITION", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(2)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(3).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()

    End Sub



    Private Sub ExpOwner_Info()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_OWNER_INFO.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OWNER_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OWNER_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "12")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OWNER_NAME")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OWNER_NAME")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OCTYPECODE")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OCTYPECODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "GENDER")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "GENDER")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "1")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OWNER_FATHER")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OWNER_FATHER")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OWNER_MOTHER")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OWNER_MOTHER")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OWNER_SPOUSE")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OWNER_SPOUSE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DOB")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DOB")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_RES1")
        xmlwrt.WriteAttributeString("rs:number", "9")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_RES1")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_CITY_RES1")
        xmlwrt.WriteAttributeString("rs:number", "10")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_CITY_RES1")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "COUNTRY_CODE_RES1")
        xmlwrt.WriteAttributeString("rs:number", "11")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "COUNTRY_CODE_RES1")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_RES2")
        xmlwrt.WriteAttributeString("rs:number", "12")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_RES2")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 



        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_CITY_RES2")
        xmlwrt.WriteAttributeString("rs:number", "13")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_CITY_RES2")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "COUNTRY_CODE_RES2")
        xmlwrt.WriteAttributeString("rs:number", "14")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "COUNTRY_CODE_RES2")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MOBILE1")
        xmlwrt.WriteAttributeString("rs:number", "15")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MOBILE1")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MOBILE1_CITY")
        xmlwrt.WriteAttributeString("rs:number", "16")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MOBILE1_CITY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "COUNTRY_CODE_MOB1")
        xmlwrt.WriteAttributeString("rs:number", "17")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "COUNTRY_CODE_MOB1")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MOBILE2")
        xmlwrt.WriteAttributeString("rs:number", "18")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MOBILE2")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MOBILE2_CITY")
        xmlwrt.WriteAttributeString("rs:number", "19")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MOBILE2_CITY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "COUNTRY_CODE_MOB2")
        xmlwrt.WriteAttributeString("rs:number", "20")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "COUNTRY_CODE_MOB2")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_OFF1")
        xmlwrt.WriteAttributeString("rs:number", "21")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_OFF1")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_CITY_OFF1")
        xmlwrt.WriteAttributeString("rs:number", "22")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_CITY_OFF1")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "COUNTRY_CODE_OFF1")
        xmlwrt.WriteAttributeString("rs:number", "23")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "COUNTRY_CODE_OFF1")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_OFF2")
        xmlwrt.WriteAttributeString("rs:number", "24")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_OFF2")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PHONE_CITY_OFF2")
        xmlwrt.WriteAttributeString("rs:number", "25")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PHONE_CITY_OFF2")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "COUNTRY_CODE_OFF2")
        xmlwrt.WriteAttributeString("rs:number", "26")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "COUNTRY_CODE_OFF2")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PPNO")
        xmlwrt.WriteAttributeString("rs:number", "27")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PPNO")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DRIVINGLNO")
        xmlwrt.WriteAttributeString("rs:number", "28")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DRIVINGLNO")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "TIN")
        xmlwrt.WriteAttributeString("rs:number", "29")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "TIN")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BIN")
        xmlwrt.WriteAttributeString("rs:number", "30")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BIN")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PRES_ADDR")
        xmlwrt.WriteAttributeString("rs:number", "31")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PRES_ADDR")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "200")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PRES_THANA_CODE")
        xmlwrt.WriteAttributeString("rs:number", "32")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PRES_THANA_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "6")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PERM_ADDR")
        xmlwrt.WriteAttributeString("rs:number", "33")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PERM_ADDR")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "200")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PERM_THANA_CODE")
        xmlwrt.WriteAttributeString("rs:number", "34")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PERM_THANA_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "6")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BB_OWNER_CODE")
        xmlwrt.WriteAttributeString("rs:number", "35")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BB_OWNER_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "12")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BB_CODE_UPDATED_ON")
        xmlwrt.WriteAttributeString("rs:number", "36")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BB_CODE_UPDATED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BB_CODE_UPDATED_BY")
        xmlwrt.WriteAttributeString("rs:number", "37")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BB_CODE_UPDATED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "38")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "39")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "40")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "41")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "42")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "43")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNER_INFO_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)
        If flagTransExist = True Then



            Try

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)
                'Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from FIU_OWNER_INFO where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

                Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from TEMP_OWNER_INFO")

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


                If ds.Tables(0).Rows.Count > 0 Then
                    Dim i As Integer = 0

                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        xmlwrt.WriteStartElement("z:row", Nothing)
                        xmlwrt.WriteAttributeString("OWNER_CODE", ds.Tables(0).Rows(i)(0).ToString())
                        xmlwrt.WriteAttributeString("OWNER_NAME", ds.Tables(0).Rows(i)(1).ToString())

                        If Not (ds.Tables(0).Rows(i)(2) Is DBNull.Value Or (ds.Tables(0).Rows(i)(2) Is Nothing) Or ds.Tables(0).Rows(i)(2).ToString() = "") Then
                            xmlwrt.WriteAttributeString("OCTYPECODE", ds.Tables(0).Rows(i)(2).ToString())
                        End If


                        If Not (ds.Tables(0).Rows(i)(3) Is DBNull.Value Or (ds.Tables(0).Rows(i)(3) Is Nothing) Or ds.Tables(0).Rows(i)(3).ToString() = "") Then
                            xmlwrt.WriteAttributeString("GENDER", ds.Tables(0).Rows(i)(3).ToString())
                        End If
                        xmlwrt.WriteAttributeString("OWNER_FATHER", ds.Tables(0).Rows(i)(4).ToString())
                        xmlwrt.WriteAttributeString("OWNER_MOTHER", ds.Tables(0).Rows(i)(5).ToString())
                        xmlwrt.WriteAttributeString("OWNER_SPOUSE", ds.Tables(0).Rows(i)(6).ToString())

                        If Not (ds.Tables(0).Rows(i)(7) Is DBNull.Value Or (ds.Tables(0).Rows(i)(7) Is Nothing) Or ds.Tables(0).Rows(i)(7).ToString() = "") Then
                            xmlwrt.WriteAttributeString("DOB", NullHelper.DateToXML(ds.Tables(0).Rows(i)(7)))
                        End If


                        xmlwrt.WriteAttributeString("PHONE_RES1", ds.Tables(0).Rows(i)(8).ToString())
                        xmlwrt.WriteAttributeString("PHONE_CITY_RES1", ds.Tables(0).Rows(i)(9).ToString())

                        If Not (ds.Tables(0).Rows(i)(10) Is DBNull.Value Or (ds.Tables(0).Rows(i)(10) Is Nothing) Or ds.Tables(0).Rows(i)(10).ToString() = "") Then
                            xmlwrt.WriteAttributeString("COUNTRY_CODE_RES1", ds.Tables(0).Rows(i)(10).ToString())
                        End If


                        xmlwrt.WriteAttributeString("PHONE_RES2", ds.Tables(0).Rows(i)(11).ToString())
                        xmlwrt.WriteAttributeString("PHONE_CITY_RES2", ds.Tables(0).Rows(i)(12).ToString())

                        If Not (ds.Tables(0).Rows(i)(13) Is DBNull.Value Or (ds.Tables(0).Rows(i)(13) Is Nothing) Or ds.Tables(0).Rows(i)(13).ToString() = "") Then
                            xmlwrt.WriteAttributeString("COUNTRY_CODE_RES2", ds.Tables(0).Rows(i)(13).ToString())
                        End If


                        xmlwrt.WriteAttributeString("MOBILE1", ds.Tables(0).Rows(i)(14).ToString())
                        xmlwrt.WriteAttributeString("MOBILE1_CITY", ds.Tables(0).Rows(i)(15).ToString())

                        If Not (ds.Tables(0).Rows(i)(16) Is DBNull.Value Or (ds.Tables(0).Rows(i)(16) Is Nothing) Or ds.Tables(0).Rows(i)(16).ToString() = "") Then
                            xmlwrt.WriteAttributeString("COUNTRY_CODE_MOB1", ds.Tables(0).Rows(i)(16).ToString())
                        End If


                        xmlwrt.WriteAttributeString("MOBILE2", ds.Tables(0).Rows(i)(17).ToString())
                        xmlwrt.WriteAttributeString("MOBILE2_CITY", ds.Tables(0).Rows(i)(18).ToString())

                        If Not (ds.Tables(0).Rows(i)(19) Is DBNull.Value Or (ds.Tables(0).Rows(i)(19) Is Nothing) Or ds.Tables(0).Rows(i)(19).ToString() = "") Then
                            xmlwrt.WriteAttributeString("COUNTRY_CODE_MOB2", ds.Tables(0).Rows(i)(19).ToString())
                        End If

                        xmlwrt.WriteAttributeString("PHONE_OFF1", ds.Tables(0).Rows(i)(20).ToString())
                        xmlwrt.WriteAttributeString("PHONE_CITY_OFF1", ds.Tables(0).Rows(i)(21).ToString())

                        If Not (ds.Tables(0).Rows(i)(22) Is DBNull.Value Or (ds.Tables(0).Rows(i)(22) Is Nothing) Or ds.Tables(0).Rows(i)(22).ToString() = "") Then
                            xmlwrt.WriteAttributeString("COUNTRY_CODE_OFF1", ds.Tables(0).Rows(i)(22).ToString())
                        End If

                        xmlwrt.WriteAttributeString("PHONE_OFF2", ds.Tables(0).Rows(i)(23).ToString())
                        xmlwrt.WriteAttributeString("PHONE_CITY_OFF2", ds.Tables(0).Rows(i)(24).ToString())

                        If Not (ds.Tables(0).Rows(i)(25) Is DBNull.Value Or (ds.Tables(0).Rows(i)(25) Is Nothing) Or ds.Tables(0).Rows(i)(25).ToString() = "") Then
                            xmlwrt.WriteAttributeString("COUNTRY_CODE_OFF2", ds.Tables(0).Rows(i)(25).ToString())
                        End If


                        xmlwrt.WriteAttributeString("PPNO", ds.Tables(0).Rows(i)(26).ToString())
                        xmlwrt.WriteAttributeString("DRIVINGLNO", ds.Tables(0).Rows(i)(27).ToString())
                        xmlwrt.WriteAttributeString("TIN", ds.Tables(0).Rows(i)(28).ToString())
                        xmlwrt.WriteAttributeString("BIN", ds.Tables(0).Rows(i)(29).ToString())
                        xmlwrt.WriteAttributeString("PRES_ADDR", ds.Tables(0).Rows(i)(30).ToString())

                        If Not (ds.Tables(0).Rows(i)(31) Is DBNull.Value Or (ds.Tables(0).Rows(i)(31) Is Nothing) Or ds.Tables(0).Rows(i)(31).ToString() = "") Then
                            xmlwrt.WriteAttributeString("PRES_THANA_CODE", ds.Tables(0).Rows(i)(31).ToString())
                        End If

                        xmlwrt.WriteAttributeString("PERM_ADDR", ds.Tables(0).Rows(i)(32).ToString())

                        If Not (ds.Tables(0).Rows(i)(33) Is DBNull.Value Or (ds.Tables(0).Rows(i)(33) Is Nothing) Or ds.Tables(0).Rows(i)(33).ToString() = "") Then
                            xmlwrt.WriteAttributeString("PERM_THANA_CODE", ds.Tables(0).Rows(i)(33).ToString())
                        End If

                        xmlwrt.WriteAttributeString("BB_OWNER_CODE", ds.Tables(0).Rows(i)(34).ToString())

                        If Not (ds.Tables(0).Rows(i)(35) Is DBNull.Value Or (ds.Tables(0).Rows(i)(35) Is Nothing) Or ds.Tables(0).Rows(i)(35).ToString() = "") Then
                            xmlwrt.WriteAttributeString("BB_CODE_UPDATED_ON", NullHelper.DateToXML(ds.Tables(0).Rows(i)(35)))
                        End If

                        xmlwrt.WriteAttributeString("BB_CODE_UPDATED_BY", ds.Tables(0).Rows(i)(36).ToString())

                        If Not (ds.Tables(0).Rows(i)(39) Is DBNull.Value Or (ds.Tables(0).Rows(i)(39) Is Nothing) Or ds.Tables(0).Rows(i)(39).ToString() = "") Then
                            xmlwrt.WriteAttributeString("INSERTED_ON", NullHelper.DateToXML(ds.Tables(0).Rows(i)(39)))
                        End If


                        If ds.Tables(0).Rows(i)(42).ToString() <> "" Then
                            xmlwrt.WriteAttributeString("MODIFIED_ON", NullHelper.DateToXML(ds.Tables(0).Rows(i)(42)))
                        End If
                        xmlwrt.WriteEndElement() 'end z:row 
                    Next i
                End If
            Catch ex As SqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

            End Try

        End If

        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()

    End Sub



    Private Sub ExpOwnership_Types()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_OWNERSHIP_TYPES.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OWTYPECODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNERSHIP_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OWTYPECODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OWDEFINITION")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNERSHIP_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OWDEFINITION")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNERSHIP_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNERSHIP_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNERSHIP_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNERSHIP_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNERSHIP_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_OWNERSHIP_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select OWTYPECODE,OWDEFINITION,INSERTED_ON,MODIFIED_ON from FIU_OWNERSHIP_TYPES where STATUS='L' and  (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("OWTYPECODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("OWDEFINITION", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(2)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(3).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()

    End Sub


    Private Sub ExpReporting_Types()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_REPORTING_TYPES.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "RPTYPECODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_REPORTING_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "RPTYPECODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "3")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "RPDEFINITION")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_REPORTING_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "RPDEFINITION")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_REPORTING_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_REPORTING_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_REPORTING_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_REPORTING_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_REPORTING_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_REPORTING_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select RPTYPECODE,RPDEFINITION,INSERTED_ON,MODIFIED_ON from FIU_REPORTING_TYPES where STATUS='L' and  (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("RPTYPECODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("RPDEFINITION", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(2)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(3).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()

    End Sub


    Private Sub ExpTrans_Ac_Owner()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_TRANS_AC_OWNER.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "OWNER_CODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "OWNER_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "12")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BANK_CODE")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BANK_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BRANCH_CODE")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BRANCH_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "ACNUMBER")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "ACNUMBER")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "EXE_DESIG_CODE")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "EXE_DESIG_CODE")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "3")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "SIGN_AUTHORITY")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "SIGN_AUTHORITY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "1")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "9")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "10")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "11")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "12")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANS_AC_OWNER_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        If flagTransExist = True Then

            Try

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)
                'Dim dbCommand As DbCommand = db.GetSqlStringCommand("select OWNER_CODE,BANK_CODE,BRANCH_CODE,ACNUMBER,EXE_DESIG_CODE,SIGN_AUTHORITY,INSERTED_ON,MODIFIED_ON from FIU_TRANS_AC_OWNER where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
                Dim dbCommand As DbCommand = db.GetSqlStringCommand("select OWNER_CODE,BANK_CODE,BRANCH_CODE,ACNUMBER,EXE_DESIG_CODE,SIGN_AUTHORITY,INSERTED_ON,MODIFIED_ON  from  TEMP_TRANS_AC_OWNER")

                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


                If ds.Tables(0).Rows.Count > 0 Then
                    Dim i As Integer = 0

                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        xmlwrt.WriteStartElement("z:row", Nothing)
                        xmlwrt.WriteAttributeString("OWNER_CODE", ds.Tables(0).Rows(i)(0).ToString())
                        xmlwrt.WriteAttributeString("BANK_CODE", ds.Tables(0).Rows(i)(1).ToString())
                        xmlwrt.WriteAttributeString("BRANCH_CODE", ds.Tables(0).Rows(i)(2).ToString())
                        xmlwrt.WriteAttributeString("ACNUMBER", ds.Tables(0).Rows(i)(3).ToString())

                        If Not (ds.Tables(0).Rows(i)(4) Is DBNull.Value Or (ds.Tables(0).Rows(i)(4) Is Nothing) Or ds.Tables(0).Rows(i)(4).ToString() = "") Then
                            xmlwrt.WriteAttributeString("EXE_DESIG_CODE", ds.Tables(0).Rows(i)(4).ToString())
                        End If

                        xmlwrt.WriteAttributeString("SIGN_AUTHORITY", ds.Tables(0).Rows(i)(5).ToString())
                        xmlwrt.WriteAttributeString("INSERTED_ON", NullHelper.DateToXML(ds.Tables(0).Rows(i)(6)))
                        If ds.Tables(0).Rows(i)(7).ToString() <> "" Then
                            xmlwrt.WriteAttributeString("MODIFIED_ON", NullHelper.DateToXML(ds.Tables(0).Rows(i)(7)))
                        End If
                        xmlwrt.WriteEndElement() 'end z:row 
                    Next i
                End If
            Catch ex As SqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

            End Try

        End If

        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()

    End Sub


    Private Sub ExpTransaction()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_TRANSACTION.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "RPTYPECODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "RPTYPECODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "3")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "DUR_CODE")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "DUR_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BANK_CODE")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BANK_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BRANCH_CODE")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BRANCH_CODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "ACNUMBER")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "ACNUMBER")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "SLNO")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "SLNO")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "int")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteAttributeString("rs:precision", "10")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "TRANSDATE")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "TRANSDATE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "TRTYPECODE")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "TRTYPECODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "REPORTING_MONTH")
        xmlwrt.WriteAttributeString("rs:number", "9")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "REPORTING_MONTH")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "TRANSAMOUNT")
        xmlwrt.WriteAttributeString("rs:number", "10")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "TRANSAMOUNT")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "float")
        xmlwrt.WriteAttributeString("dt:maxLenth", "8")
        xmlwrt.WriteAttributeString("rs:precision", "15")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "TRANSNUM")
        xmlwrt.WriteAttributeString("rs:number", "11")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "TRANSNUM")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "int")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteAttributeString("rs:precision", "10")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BANK_CODE_REMIT")
        xmlwrt.WriteAttributeString("rs:number", "12")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BANK_CODE_REMIT")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BRANCH_CODE_REMIT")
        xmlwrt.WriteAttributeString("rs:number", "13")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BRANCH_CODE_REMIT")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "4")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "ACNUMBER_REMIT")
        xmlwrt.WriteAttributeString("rs:number", "14")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "ACNUMBER_REMIT")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "CURRENCY_CODE")
        xmlwrt.WriteAttributeString("rs:number", "15")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "CURRENCY_CODE")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "3")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "BRIEF_DESC")
        xmlwrt.WriteAttributeString("rs:number", "16")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "BRIEF_DESC")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "500")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "STR_REF_BR")
        xmlwrt.WriteAttributeString("rs:number", "17")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "STR_REF_BR")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "STR_REF_HO")
        xmlwrt.WriteAttributeString("rs:number", "18")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "STR_REF_HO")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "PRIOR_STR_REPORTED")
        xmlwrt.WriteAttributeString("rs:number", "19")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "PRIOR_STR_REPORTED")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "1")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "RECENT_STR_REF_HO")
        xmlwrt.WriteAttributeString("rs:number", "20")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "RECENT_STR_REF_HO")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "20")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "ACCEPTED_BY_HO")
        xmlwrt.WriteAttributeString("rs:number", "21")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "ACCEPTED_BY_HO")


        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "1")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "22")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "23")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "24")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "25")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "26")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "27")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TEMP")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from FIU_TRANSACTION where (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("RPTYPECODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("DUR_CODE", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("BANK_CODE", ds.Tables(0).Rows(i)(2).ToString())
                    xmlwrt.WriteAttributeString("BRANCH_CODE", ds.Tables(0).Rows(i)(3).ToString())
                    xmlwrt.WriteAttributeString("ACNUMBER", ds.Tables(0).Rows(i)(4).ToString())
                    xmlwrt.WriteAttributeString("SLNO", ds.Tables(0).Rows(i)(5).ToString())
                    xmlwrt.WriteAttributeString("TRANSDATE", Convert.ToDateTime(ds.Tables(0).Rows(i)(6)).ToString("yyyy-MM-ddT00:00:00"))
                    xmlwrt.WriteAttributeString("TRTYPECODE", ds.Tables(0).Rows(i)(7).ToString())
                    xmlwrt.WriteAttributeString("REPORTING_MONTH", Convert.ToDateTime(ds.Tables(0).Rows(i)(8)).ToString("yyyy-MM-ddT00:00:00"))
                    xmlwrt.WriteAttributeString("TRANSAMOUNT", ds.Tables(0).Rows(i)(9).ToString())
                    xmlwrt.WriteAttributeString("TRANSNUM", ds.Tables(0).Rows(i)(10).ToString())
                    xmlwrt.WriteAttributeString("BANK_CODE_REMIT", ds.Tables(0).Rows(i)(11).ToString())
                    xmlwrt.WriteAttributeString("BRANCH_CODE_REMIT", ds.Tables(0).Rows(i)(12).ToString())
                    xmlwrt.WriteAttributeString("ACNUMBER_REMIT", ds.Tables(0).Rows(i)(13).ToString())
                    xmlwrt.WriteAttributeString("CURRENCY_CODE", ds.Tables(0).Rows(i)(14).ToString())
                    xmlwrt.WriteAttributeString("BRIEF_DESC", ds.Tables(0).Rows(i)(15).ToString())
                    xmlwrt.WriteAttributeString("STR_REF_BR", ds.Tables(0).Rows(i)(16).ToString())
                    xmlwrt.WriteAttributeString("STR_REF_HO", ds.Tables(0).Rows(i)(17).ToString())
                    xmlwrt.WriteAttributeString("PRIOR_STR_REPORTED", ds.Tables(0).Rows(i)(18).ToString())
                    xmlwrt.WriteAttributeString("RECENT_STR_REF_HO", ds.Tables(0).Rows(i)(19).ToString())
                    xmlwrt.WriteAttributeString("ACCEPTED_BY_HO", ds.Tables(0).Rows(i)(20).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(23)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(26).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(26)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()

    End Sub

    Private Sub ExpTransaction_Types()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_TRANSACTION_TYPES.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "TRTYPECODE")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "TRTYPECODE")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "2")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Thana_Code
        xmlwrt.WriteEndElement() 'end s:attributetype Thana_Code


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "TRDEFINITION")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "TRDEFINITION")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "50")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_TRANSACTION_TYPES")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select TRTYPECODE,TRDEFINITION,INSERTED_ON,MODIFIED_ON from FIU_TRANSACTION_TYPES where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("TRTYPECODE", ds.Tables(0).Rows(i)(0).ToString())
                    xmlwrt.WriteAttributeString("TRDEFINITION", ds.Tables(0).Rows(i)(1).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(2)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(3).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()

    End Sub


    Private Sub ExpWeekend()

        Dim xmlwrt As New XmlTextWriter(strpath & "\FIU_WEEKEND.xml", System.Text.Encoding.UTF8)

        xmlwrt.Formatting = Formatting.Indented
        xmlwrt.Indentation = 3
        'xmlwrt.WriteStartDocument(True)
        xmlwrt.WriteStartElement("xml")

        xmlwrt.WriteAttributeString("xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882")
        xmlwrt.WriteAttributeString("xmlns:rs", "urn:schemas-microsoft-com:rowset")
        xmlwrt.WriteAttributeString("xmlns:z", "#RowsetSchema")

        xmlwrt.WriteStartElement("s:schema", Nothing)
        xmlwrt.WriteAttributeString("id", "RowsetSchema")

        xmlwrt.WriteStartElement("s:ElementType", Nothing)
        xmlwrt.WriteAttributeString("name", "row")
        xmlwrt.WriteAttributeString("content", "eltOnly")
        xmlwrt.WriteAttributeString("rs:updatable", "true")

        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "EFFEC_FROM")
        xmlwrt.WriteAttributeString("rs:number", "1")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_WEEKEND")
        xmlwrt.WriteAttributeString("rs:basecolumn", "EFFEC_FROM")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "EFFEC_TO")
        xmlwrt.WriteAttributeString("rs:number", "2")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_WEEKEND")
        xmlwrt.WriteAttributeString("rs:basecolumn", "EFFEC_TO")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype 
        xmlwrt.WriteEndElement() 'end s:attributetype 


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "EFFEC_DAY")
        xmlwrt.WriteAttributeString("rs:number", "3")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_WEEKEND")
        xmlwrt.WriteAttributeString("rs:basecolumn", "EFFEC_DAY")
        xmlwrt.WriteAttributeString("rs:keycolumn", "true")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "9")
        xmlwrt.WriteAttributeString("rs:maybenull", "false")
        xmlwrt.WriteEndElement() 'end s:datatype Name
        xmlwrt.WriteEndElement() 'end s:attributetype Name


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "4")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_WEEKEND")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted from
        xmlwrt.WriteEndElement() 'end s:attributetype inserted from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_BY")
        xmlwrt.WriteAttributeString("rs:number", "5")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_WEEKEND")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype inserted by
        xmlwrt.WriteEndElement() 'end s:attributetype inserted by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "INSERTED_ON")
        xmlwrt.WriteAttributeString("rs:number", "6")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_WEEKEND")
        xmlwrt.WriteAttributeString("rs:basecolumn", "INSERTED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype inserted on
        xmlwrt.WriteEndElement() 'end s:attributetype inserted on


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_FROM")
        xmlwrt.WriteAttributeString("rs:number", "7")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_WEEKEND")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_FROM")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified from
        xmlwrt.WriteEndElement() 'end s:attributetype modified from


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_BY")
        xmlwrt.WriteAttributeString("rs:number", "8")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_WEEKEND")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_BY")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "string")
        xmlwrt.WriteAttributeString("rs:dbtype", "str")
        xmlwrt.WriteAttributeString("dt:maxLenth", "30")
        xmlwrt.WriteEndElement() 'end s:datatype modified by
        xmlwrt.WriteEndElement() 'end s:attributetype modified by


        xmlwrt.WriteStartElement("s:AttributeType", Nothing)
        xmlwrt.WriteAttributeString("name", "MODIFIED_ON")
        xmlwrt.WriteAttributeString("rs:number", "9")
        xmlwrt.WriteAttributeString("rs:nullable", "true")
        xmlwrt.WriteAttributeString("rs:writeunknown", "true")
        xmlwrt.WriteAttributeString("rs:basecatalog", "FIU")
        xmlwrt.WriteAttributeString("rs:basetable", "FIU_WEEKEND")
        xmlwrt.WriteAttributeString("rs:basecolumn", "MODIFIED_ON")

        xmlwrt.WriteStartElement("s:dataType", Nothing)
        xmlwrt.WriteAttributeString("dt:type", "dateTime")
        xmlwrt.WriteAttributeString("rs:dbtype", "timestamp")
        xmlwrt.WriteAttributeString("dt:maxLenth", "16")
        xmlwrt.WriteAttributeString("rs:scale", "3")
        xmlwrt.WriteAttributeString("rs:precision", "23")
        xmlwrt.WriteAttributeString("rs:fixedlength", "true")
        xmlwrt.WriteEndElement() 'end s:datatype modified on
        xmlwrt.WriteEndElement() 'end s:attributetype modified on


        xmlwrt.WriteStartElement("s:extends", Nothing)
        xmlwrt.WriteAttributeString("type", "rs:rowbase")
        xmlwrt.WriteEndElement() ' end s:extends

        xmlwrt.WriteEndElement() 'end s:element type

        xmlwrt.WriteEndElement() 'end s:schema

        xmlwrt.WriteStartElement("rs:data", Nothing)

        Try

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select EFFEC_FROM,EFFEC_TO,EFFEC_DAY,INSERTED_ON,MODIFIED_ON from FIU_WEEKEND where (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")

            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    xmlwrt.WriteStartElement("z:row", Nothing)
                    xmlwrt.WriteAttributeString("EFFEC_FROM", Convert.ToDateTime(ds.Tables(0).Rows(i)(0)).ToString("yyyy-MM-ddT00:00:00"))
                    xmlwrt.WriteAttributeString("EFFEC_TO", Convert.ToDateTime(ds.Tables(0).Rows(i)(1)).ToString("yyyy-MM-ddT00:00:00"))
                    xmlwrt.WriteAttributeString("EFFEC_DAY", ds.Tables(0).Rows(i)(2).ToString())
                    xmlwrt.WriteAttributeString("INSERTED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(3)).ToString("yyyy-MM-ddT00:00:00"))
                    If ds.Tables(0).Rows(i)(4).ToString() <> "" Then
                        xmlwrt.WriteAttributeString("MODIFIED_ON", Convert.ToDateTime(ds.Tables(0).Rows(i)(4)).ToString("yyyy-MM-ddT00:00:00"))
                    End If
                    xmlwrt.WriteEndElement() 'end z:row 
                Next i
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")

        End Try
        xmlwrt.WriteEndElement() 'end rs:data
        xmlwrt.WriteEndElement() 'end xml

        xmlwrt.Close()

    End Sub

    'csv file

    Private Sub Csv_ACCOUNT_INFO()


        Try

            Dim fs As New FileStream(strpath & "\FIU_ACCOUNT_INFO.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)

            If flagTransExist = True Then

                Dim db As New SqlDatabase(CommonAppSet.ConnStr)
                'Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from FIU_ACCOUNT_INFO where STATUS='L' and  (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
                Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from TEMP_ACCOUNT_INFO")
                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim i As Integer = 0

                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(4).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(7).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(8).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(9).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(10).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(11).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(12).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(13).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(14).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(15)))
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(16).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(17)))
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(18).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.StringToCSV(ds.Tables(0).Rows(i)(19)))
                        sw.Write(",")
                        sw.Write(NullHelper.StringToCSV(ds.Tables(0).Rows(i)(20)))
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(21).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(22).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(23).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(24).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(25).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(26).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(27).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(28)))
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(29).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(30).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(31).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(32)))
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(33).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(34).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(35)))

                        sw.WriteLine()
                    Next i
                End If

            End If



            sw.Close()
            fs.Close()

        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try


    End Sub

    Private Sub Csv_ACCOUNT_TYPES()


        Try
            Dim fs As New FileStream(strpath & "\FIU_ACCOUNT_TYPES.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select ACTYPECODE,ACDEFINITION, INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY,MODIFIED_ON from FIU_ACCOUNT_TYPES where STATUS='L' and  (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(4)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(7)))

                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try


    End Sub


    Private Sub Csv_ASSESSMENT_DURATION()


        Try
            Dim fs As New FileStream(strpath & "\FIU_ASSESSMENT_DURATION.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select DUR_CODE,DURDEFINITION,INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON from FIU_ASSESSMENT_DURATION where STATUS='L' and  (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(4)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(7)))

                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try


    End Sub


    Private Sub Csv_BANK()


        Try
            Dim fs As New FileStream(strpath & "\FIU_BANK.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select BANK_CODE,BANK_NAME,GCODE,OLD_BANK_CODE,OLD_CODE_UPDATED_ON,OLD_CODE_UPDATED_BY, INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON from FIU_BANK where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(4).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(7).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(8)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(9).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(10).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(11)))

                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try


    End Sub


    Private Sub Csv_BANK_BRANCH()


        Try
            Dim fs As New FileStream(strpath & "\FIU_BANK_BRANCH.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)

            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from FIU_BANK_BRANCH where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(4).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(7).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(8).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(9).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(10).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(11).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(12).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(13).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(14).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(15)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(16).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(17).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(18)))


                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try


    End Sub


    Private Sub Csv_COMPANY_REG_AUTHORITY()
        Try
            Dim fs As New FileStream(strpath & "\FIU_COMPANY_REG_AUTHORITY.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select REG_AUTHORITY_CODE,REG_AUTHORITY_NAME, INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON from FIU_COMPANY_REG_AUTHORITY where STATUS='L' and  (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(4)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(7)))
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub

    Private Sub Csv_COUNTRY_INFO()
        Try
            Dim fs As New FileStream(strpath & "\FIU_COUNTRY_INFO.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select COUNTRY_CODE,COUNTRY_NAME, INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON from FIU_COUNTRY_INFO where STATUS='L' and  (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(4)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(7)))
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub

    Private Sub Csv_CURRENCY_INFO()
        Try
            Dim fs As New FileStream(strpath & "\FIU_CURRENCY_INFO.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select CURRENCY_CODE,CURRENCY_NAME, INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON from FIU_CURRENCY_INFO where STATUS='L' and  (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(4)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(7)))
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub


    Private Sub Csv_DISTRICT()
        Try
            Dim fs As New FileStream(strpath & "\FIU_DISTRICT.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select DIST_CODE,DIV_CODE,DIST_NAME,PREV_DIST_CODE, INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON from FIU_DISTRICT where STATUS='L' and  (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(4).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(6)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(7).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(8).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(9)))
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub

    Private Sub Csv_DIVISION()
        Try
            Dim fs As New FileStream(strpath & "\FIU_DIVISION.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select DIV_CODE,DIV_NAME,PREV_DIV_CODE from FIU_DIVISION where STATUS='L' and  (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub


    Private Sub Csv_EXECUTIVE_DESIG()
        Try
            Dim fs As New FileStream(strpath & "\FIU_EXECUTIVE_DESIG.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select EXE_DESIG_CODE,EXE_DESIG_NAME,INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON from FIU_EXECUTIVE_DESIG where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(4)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(7)))
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub

    Private Sub Csv_HOLIDAYS()
        Try
            Dim fs As New FileStream(strpath & "\FIU_HOLIDAYS.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select HOLIDAY,TYPE,REASON from FIU_HOLIDAYS where (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub


    Private Sub Csv_OCCUPATION_TYPES()
        Try
            Dim fs As New FileStream(strpath & "\FIU_OCCUPATION_TYPES.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select OCTYPECODE,OCDEFINITION,INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON from FIU_OCCUPATION_TYPES where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(4)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(7)))
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub

    Private Sub Csv_OWNER_INFO()
        Try
            Dim fs As New FileStream(strpath & "\FIU_OWNER_INFO.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)

            If flagTransExist = True Then
                Dim db As New SqlDatabase(CommonAppSet.ConnStr)
                'Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from FIU_OWNER_INFO where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
                Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from TEMP_OWNER_INFO")
                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim i As Integer = 0

                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(4).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(7)))
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(8).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(9).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(10).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(11).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(12).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(13).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(14).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(15).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(16).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(17).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(18).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(19).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(20).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(21).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(22).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(23).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(24).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(25).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(26).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(27).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(28).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(29).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.StringToCSV(ds.Tables(0).Rows(i)(30)))
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(31).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.StringToCSV(ds.Tables(0).Rows(i)(32)))
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(33).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(34).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(35)))
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(36).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(37).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(38).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(39)))
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(40).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(41).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(42)))

                        sw.WriteLine()
                    Next i
                End If
            End If

            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub


    Private Sub Csv_OWNERSHIP_TYPES()
        Try
            Dim fs As New FileStream(strpath & "\FIU_OWNERSHIP_TYPES.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select OWTYPECODE,OWDEFINITION from FIU_OWNERSHIP_TYPES where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub


    Private Sub Csv_REPORTING_TYPES()
        Try
            Dim fs As New FileStream(strpath & "\FIU_REPORTING_TYPES.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select RPTYPECODE,RPDEFINITION from FIU_REPORTING_TYPES where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub


    Private Sub Csv_THANA()
        Try
            Dim fs As New FileStream(strpath & "\FIU_THANA.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select THANA_CODE,THANA_NAME,DIST_CODE,PREV_THANA_CODE from FIU_THANA where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub


    Private Sub Csv_TRANS_AC_OWNER()
        Try
            Dim fs As New FileStream(strpath & "\FIU_TRANS_AC_OWNER.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)

            If flagTransExist = True Then
                Dim db As New SqlDatabase(CommonAppSet.ConnStr)
                'Dim dbCommand As DbCommand = db.GetSqlStringCommand("select OWNER_CODE,BANK_CODE,BRANCH_CODE,ACNUMBER,EXE_DESIG_CODE,SIGN_AUTHORITY,INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON from FIU_TRANS_AC_OWNER where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
                Dim dbCommand As DbCommand = db.GetSqlStringCommand("select OWNER_CODE,BANK_CODE,BRANCH_CODE,ACNUMBER,EXE_DESIG_CODE,SIGN_AUTHORITY,INSERTED_FROM, INSERTED_BY, INSERTED_ON, MODIFIED_FROM, MODIFIED_BY, MODIFIED_ON from TEMP_TRANS_AC_OWNER")
                Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim i As Integer = 0

                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(4).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(6).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(7).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(8)))
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(9).ToString())
                        sw.Write(",")
                        sw.Write(ds.Tables(0).Rows(i)(10).ToString())
                        sw.Write(",")
                        sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(11)))
                        sw.WriteLine()
                    Next i
                End If
            End If

            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub

    Private Sub Csv_TRANSACTION()
        Try
            Dim fs As New FileStream(strpath & "\FIU_TRANSACTION.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from FIU_TRANSACTION where (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(3).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(4).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(5).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(6)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(7).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(8)))
                    sw.Write(",")
                    sw.Write(NullHelper.NumToString(ds.Tables(0).Rows(i)(9)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(10).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(11).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(12).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(13).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(14).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(15).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(16).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(17).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(18).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(19).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(20).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(21).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(22).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(23)))
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(24).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(25).ToString())
                    sw.Write(",")
                    sw.Write(NullHelper.DateToCSV(ds.Tables(0).Rows(i)(26)))
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub


    Private Sub Csv_TRANSACTION_TYPES()
        Try
            Dim fs As New FileStream(strpath & "\FIU_TRANSACTION_TYPES.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select TRTYPECODE,TRDEFINITION from FIU_TRANSACTION_TYPES where STATUS='L' and (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub


    Private Sub Csv_WEEKEND()
        Try
            Dim fs As New FileStream(strpath & "\FIU_WEEKEND.csv", FileMode.Create, FileAccess.Write)
            Dim sw As New StreamWriter(fs)
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)
            Dim dbCommand As DbCommand = db.GetSqlStringCommand("select EFFEC_FROM,EFFEC_TO,EFFEC_DAY from FIU_WEEKEND where (year(inserted_on) = " & txtYear.Text & " and month(inserted_on) = " & txtMonth.Text & ") or (year(modified_on) = " & txtYear.Text & " and month(modified_on) = " & txtMonth.Text & " )")
            Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    sw.Write(ds.Tables(0).Rows(i)(0).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(1).ToString())
                    sw.Write(",")
                    sw.Write(ds.Tables(0).Rows(i)(2).ToString())
                    sw.WriteLine()
                Next i
            End If
            sw.Close()
            fs.Close()
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub


    Private Function CheckExport() As Boolean

        Try
            
            Dim db As New SqlDatabase(CommonAppSet.ConnStr)

            Dim strSql As String = ""

            strSql = "select isnull(count(t.ACNUMBER),0) cnt " & _
            " from (select distinct(ACNUMBER) from  dbo.FIU_TRANSACTION " & _
            "      where year(TRANSDATE)='" & txtYear.Text & "'  and month(TRANSDATE)='" & txtMonth.Text & "')t  " & _
            "     left outer join (select * from FIU_ACCOUNT_INFO where STATUS='L') a " & _
            "    on t.ACNUMBER=a.ACNUMBER " & _
            "            where(a.ACNUMBER Is null)"

            If db.ExecuteDataSet(CommandType.Text, strSql).Tables(0).Rows(0)(0) > 0 Then
                MessageBox.Show("Account Maintenance needed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If


            strSql = "select isnull(count(t.ACNUMBER),0) cnt from  " & _
            " (select distinct ACNUMBER from FIU_TRANSACTION " & _
            " where year(TRANSDATE)='" & txtYear.Text & "' and month(TRANSDATE)='" & txtMonth.Text & "') t " & _
            " where t.ACNUMBER not in (select distinct ACNUMBER from FIU_TRANS_AC_OWNER where STATUS='L')"

            If db.ExecuteDataSet(CommandType.Text, strSql).Tables(0).Rows(0)(0) > 0 Then
                MessageBox.Show("Owner Maintenance needed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

        
        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try


        Return True

    End Function

#End Region

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click

        FolderBrowserDialog1.ShowDialog()
        txtFileName.Text = FolderBrowserDialog1.SelectedPath

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click

        If Not CheckExport() Then
            Exit Sub
        End If


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)
        Dim dbCommand As DbCommand = db.GetSqlStringCommand("select * from FIU_TRANSACTION where year(inserted_on) = '" & txtYear.Text & "' and month(inserted_on) = '" & txtMonth.Text & "' ")
        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        flagTransExist = False


        If ds.Tables(0).Rows.Count > 0 Then
            flagTransExist = True
        End If



        If txtFileName.Text.Trim() = "" Then
            MessageBox.Show("Select Export Folder", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf txtYear.Text.Trim() = "" Then
            MessageBox.Show("Select Export Year", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf txtMonth.Text.Trim() = "" Then
            MessageBox.Show("Select Export Month", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            'IO.Directory.CreateDirectory(txtFileName.Text & "\FIUDataBBank_ReportingMonth")
            'strpath = txtFileName.Text & "\FIUDataBBank_ReportingMonth"

            IO.Directory.CreateDirectory(txtFileName.Text & "\ReportingMonthBBank")
            strpath = txtFileName.Text & "\ReportingMonthBBank"

            ExpAccountInfo() ' '
            ExpAccTypes()
            ExpAssessment_Duration()
            ExpBank()
            ExpBankBranch()
            ExpCmp_Reg_Auth()
            ExpCountry_Info()
            ExpCurrency_Info()
            ExpDistrict()
            ExpDivision()
            ExpExecutive_Desig()
            ExpHoliday()
            ExpOccupation_Types()
            ExpOwner_Info() ' '
            ExpOwnership_Types()
            ExpReporting_Types()
            ExpThana()
            ExpTrans_Ac_Owner() ' '
            ExpTransaction()
            ExpTransaction_Types()
            ExpWeekend()


            Csv_ACCOUNT_INFO() '
            Csv_ACCOUNT_TYPES()
            Csv_ASSESSMENT_DURATION()
            Csv_BANK()
            Csv_BANK_BRANCH()
            Csv_COMPANY_REG_AUTHORITY()
            Csv_COUNTRY_INFO()
            Csv_CURRENCY_INFO()
            Csv_DISTRICT()
            Csv_DIVISION()
            Csv_EXECUTIVE_DESIG()
            Csv_HOLIDAYS()
            Csv_OCCUPATION_TYPES()
            Csv_OWNER_INFO() '
            Csv_OWNERSHIP_TYPES()
            Csv_REPORTING_TYPES()
            Csv_THANA()
            Csv_TRANS_AC_OWNER() '
            Csv_TRANSACTION()
            Csv_TRANSACTION_TYPES()
            Csv_WEEKEND()


            'IO.Directory.CreateDirectory(txtFileName.Text & "\FIUDataCSV_DateRange_CSV")
            'strpath = txtFileName.Text & "\FIUDataCSV_DateRange_CSV"
            IO.Directory.CreateDirectory(txtFileName.Text & "\DateRangeCSV")
            strpath = txtFileName.Text & "\DateRangeCSV"

            Csv_ACCOUNT_INFO()
            Csv_ACCOUNT_TYPES()
            Csv_ASSESSMENT_DURATION()
            Csv_BANK()
            Csv_BANK_BRANCH()
            Csv_COMPANY_REG_AUTHORITY()
            Csv_COUNTRY_INFO()
            Csv_CURRENCY_INFO()
            Csv_DISTRICT()
            Csv_DIVISION()
            Csv_EXECUTIVE_DESIG()
            Csv_HOLIDAYS()
            Csv_OCCUPATION_TYPES()
            Csv_OWNER_INFO()
            Csv_OWNERSHIP_TYPES()
            Csv_REPORTING_TYPES()
            Csv_THANA()
            Csv_TRANS_AC_OWNER()
            Csv_TRANSACTION()
            Csv_TRANSACTION_TYPES()
            Csv_WEEKEND()

            'IO.Directory.CreateDirectory(txtFileName.Text & "\FIUDataHO_DateRange_XML")
            'strpath = txtFileName.Text & "\FIUDataHO_DateRange_XML"
            IO.Directory.CreateDirectory(txtFileName.Text & "\DateRangeHO")
            strpath = txtFileName.Text & "\DateRangeHO"

            ExpAccountInfo()
            ExpAccTypes()
            ExpAssessment_Duration()
            ExpBank()
            ExpBankBranch()
            ExpCmp_Reg_Auth()
            ExpCountry_Info()
            ExpCurrency_Info()
            ExpDistrict()
            ExpDivision()
            ExpExecutive_Desig()
            ExpHoliday()
            ExpOccupation_Types()
            ExpOwner_Info()
            ExpOwnership_Types()
            ExpReporting_Types()
            ExpThana()
            ExpTrans_Ac_Owner()
            ExpTransaction()
            ExpTransaction_Types()
            ExpWeekend()

            'IO.Directory.CreateDirectory(txtFileName.Text & "\FIUDataHO_ReportingMonth")
            'strpath = txtFileName.Text & "\FIUDataHO_ReportingMonth"
            IO.Directory.CreateDirectory(txtFileName.Text & "\ReportingMonthHO")
            strpath = txtFileName.Text & "\ReportingMonthHO"

            ExpAccountInfo()
            ExpAccTypes()
            ExpAssessment_Duration()
            ExpBank()
            ExpBankBranch()
            ExpCmp_Reg_Auth()
            ExpCountry_Info()
            ExpCurrency_Info()
            ExpDistrict()
            ExpDivision()
            ExpExecutive_Desig()
            ExpHoliday()
            ExpOccupation_Types()
            ExpOwner_Info()
            ExpOwnership_Types()
            ExpReporting_Types()
            ExpThana()
            ExpTrans_Ac_Owner()
            ExpTransaction()
            ExpTransaction_Types()
            ExpWeekend()


            Csv_ACCOUNT_INFO()
            Csv_ACCOUNT_TYPES()
            Csv_ASSESSMENT_DURATION()
            Csv_BANK()
            Csv_BANK_BRANCH()
            Csv_COMPANY_REG_AUTHORITY()
            Csv_COUNTRY_INFO()
            Csv_CURRENCY_INFO()
            Csv_DISTRICT()
            Csv_DIVISION()
            Csv_EXECUTIVE_DESIG()
            Csv_HOLIDAYS()
            Csv_OCCUPATION_TYPES()
            Csv_OWNER_INFO()
            Csv_OWNERSHIP_TYPES()
            Csv_REPORTING_TYPES()
            Csv_THANA()
            Csv_TRANS_AC_OWNER()
            Csv_TRANSACTION()
            Csv_TRANSACTION_TYPES()
            Csv_WEEKEND()

            log_message = " Exported : All File "
            Logger.system_log(log_message)

            MessageBox.Show(" Data exported successfully ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub FrmExportAll_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
        flagTransExist = False

    End Sub
End Class