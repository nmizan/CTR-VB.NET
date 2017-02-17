Imports Microsoft.Office.Interop
Imports MigraDoc.DocumentObjectModel
Imports MigraDoc.DocumentObjectModel.Tables
Imports MigraDoc.Rendering
Imports MigraDoc.DocumentObjectModel.Shapes
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports PdfSharp.Pdf.AcroForms
Imports PdfSharp
Imports PdfSharp.Pdf.PdfDictionary
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports System.IO


Public Class FrmQualifiedTransgoAML

#Region "User defined code"
    Dim _formName As String = "ReportQualifiedTransgoAML"
    Dim opt As SecForm = New SecForm(_formName)

    Dim log_message As String

    '.......Md.Mizanur Rahman Work(28 Feb 2016)..............

    Dim _document As Document

    Dim _table As Table

    Shared ReadOnly TableBorder As Color = New Color(81, 125, 192)
    Shared ReadOnly TableBlue As Color = New Color(235, 240, 249)
    Shared ReadOnly TableGray As Color = New Color(242, 242, 242)

    Dim _fileName As String = ""


    Private Sub ExportToXl()


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String

        Dim strSearch As String = ""

        If NullHelper.ObjectToString(txtAcNumber.Text.Trim()) <> "" Then

            strSearch = " And ACCOUNT=@ACCNUMBER "

        End If

        If txtDateFrom.Text.Trim() <> "/  /" And txtDateFrom.Text.Trim() <> "/  /" Then

            strSearch += " And DATE_TRANSACTION >=@DATE_FROM AND DATE_TRANSACTION <=@DATE_TO "

        End If


        strSql = "select ACCOUNT,CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
                         " from GO_TRANSACTION WHERE [STATUS]='L' " & strSearch & _
                         " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND "


        'If NullHelper.ObjectToString(txtAcNumber.Text.Trim()) = "" And txtDateFrom.Text.Trim() = "/  /" And txtDateFrom.Text.Trim() = "/  /" Then

        '    strSql = "select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
        '                 " from GO_TRANSACTION " & _
        '                 " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND "


        'ElseIf NullHelper.ObjectToString(txtAcNumber.Text.Trim()) <> "" And txtDateFrom.Text.Trim() = "/  /" And txtDateFrom.Text.Trim() = "/  /" Then

        '    strSql = "select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
        '                 " from GO_TRANSACTION " & _
        '                 " where ACCOUNT ='" & txtAcNumber.Text & "'" & _
        '                 " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND "

        'ElseIf NullHelper.ObjectToString(txtAcNumber.Text.Trim()) = "" And txtDateFrom.Text.Trim() <> "/  /" And txtDateFrom.Text.Trim() <> "/  /" Then

        '    strSql = "select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
        '             " from GO_TRANSACTION " & _
        '             " where DATE_TRANSACTION  >='" & NullHelper.DateToString(NullHelper.StringToDate(txtDateFrom.Text.Trim())) & "'" & " AND DATE_TRANSACTION <='" & NullHelper.DateToString(NullHelper.StringToDate(txtDateTo.Text.Trim())) & "'" & _
        '             " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND "

        'Else

        '    strSql = "select ACCOUNT,CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
        '                 " from GO_TRANSACTION " & _
        '                 " where ACCOUNT ='" & txtAcNumber.Text & "'" & " AND DATE_TRANSACTION  >='" & NullHelper.DateToString(NullHelper.StringToDate(txtDateFrom.Text.Trim())) & "'" & " AND DATE_TRANSACTION <='" & NullHelper.DateToString(NullHelper.StringToDate(txtDateTo.Text.Trim())) & "'" & _
        '                 " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND "


        'End If


        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        dbCommand.Parameters.Clear()

        db.AddInParameter(dbCommand, "@ACCNUMBER", DbType.String, txtAcNumber.Text.Trim())

        db.AddInParameter(dbCommand, "@DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim()))
        db.AddInParameter(dbCommand, "@DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim()))


        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        '--------------Excell Report


        Dim xlApp As New Excel.Application

        Dim wb As Excel.Workbook = xlApp.Workbooks.Add

        Dim sheet As Excel.Worksheet = wb.Worksheets.Add

        sheet.Name = "Qualified Transaction Report"



        Dim i, j As Integer

        For j = 0 To ds.Tables(0).Columns.Count - 1
            sheet.Cells(1, j + 1) = ds.Tables(0).Columns(j).ColumnName
        Next j

        For i = 0 To ds.Tables(0).Rows.Count - 1
            For j = 0 To ds.Tables(0).Columns.Count - 1
                sheet.Cells(i + 2, j + 1) = ds.Tables(0).Rows(i)(j).ToString()
            Next j
        Next i
        CType(sheet.Rows(1, Type.Missing), Excel.Range).Font.Bold = True

        xlApp.Visible = True
        wb.Activate()
        log_message = " Showed : Qualified Transaction (goAML)"
        Logger.system_log(log_message)

    End Sub

    '...............Mizanur Rahman Work(28 Feb 2016)...........


    Private Sub CreategoAMLReportDocument()

        _document = New Document()
        _document.Info.Title = "Qualified Transaction Report(goAML)"
        _document.Info.Subject = "Transaction Report"
        _document.Info.Author = "CTR Application"

        DefineStyles()

        CreatePagegoAML()

        FillContentData_goAML()
    End Sub

    '...............Mizanur Rahman Work(29 Feb 2016)...........

    'Private Sub CreateCTRReportDocument()

    '    _document = New Document()
    '    _document.Info.Title = "Qualified Transaction Report(CTR vs goAML)"
    '    _document.Info.Subject = "Transaction Report"
    '    _document.Info.Author = "CTR Application"

    '    DefineStyles()

    '    Create_CTR_Page()

    '    FillContentdata_CTR()

    'End Sub

    '...............Mizanur Rahman Work(28 Feb 2016)...........

    Private Sub DefineStyles()

        ' Get the predefined style Normal.
        Dim style As Style = _document.Styles("Normal")
        ' Because all styles are derived from Normal, the next line changes the 
        ' font of the whole document. Or, more exactly, it changes the font of
        ' all styles and paragraphs that do not redefine the font.
        style.Font.Name = "Verdana"

        style = _document.Styles(StyleNames.Header)
        style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right)

        style = _document.Styles(StyleNames.Footer)
        style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center)

        ' Create a new style called Table based on style Normal
        style = _document.Styles.AddStyle("Table", "Normal")
        style.Font.Name = "Verdana"
        'style.Font.Name = "Times New Roman"
        style.Font.Size = 8

        ' Create a new style called Reference based on style Normal
        style = _document.Styles.AddStyle("Reference", "Normal")
        style.ParagraphFormat.SpaceBefore = "5mm"
        style.ParagraphFormat.SpaceAfter = "5mm"
        style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right)

    End Sub

    '...............Mizanur Rahman Work(28 Feb 2016)...........

    Private Sub CreatePagegoAML()

        Dim section As Section = _document.AddSection()

        ' Create Header
        Dim paragraph As Paragraph = section.Headers.Primary.AddParagraph()
        paragraph.AddText("Qualified Transaction Report(goAML)")
        paragraph.Format.Font.Size = 9
        paragraph.Format.Font.Bold = True
        paragraph.Format.Alignment = ParagraphAlignment.Center


        paragraph = section.Headers.Primary.AddParagraph()
        paragraph.AddText("For The Branch Of " + " " + cmboBranch.SelectedValue)
        paragraph.Format.Font.Size = 9
        paragraph.Format.Font.Bold = True
        paragraph.Format.Alignment = ParagraphAlignment.Center


        paragraph = section.Headers.Primary.AddParagraph()
        paragraph.AddText("Print Date: ")
        paragraph.Format.Font.Size = 8
        'paragraph.Format.Font.Bold = true
        paragraph.Format.Alignment = ParagraphAlignment.Right
        paragraph.AddDateField("dd-MMM-yyyy")

        ' Create footer
        paragraph = section.Footers.Primary.AddParagraph()
        paragraph.AddText("Page ")
        paragraph.AddPageField()
        paragraph.AddText(" of ")
        paragraph.AddNumPagesField()
        paragraph.Format.Font.Size = 7
        'paragraph.Format.Font.Bold = True
        paragraph.Format.Alignment = ParagraphAlignment.Center


        ' Create the item table
        _table = section.AddTable()
        _table.Style = "Table"
        _table.Borders.Color = TableBorder
        _table.Section.PageSetup.LeftMargin = 40
        _table.TopPadding = 5
        _table.BottomPadding = 5
        _table.Borders.Width = 0.25
        _table.Borders.Left.Width = 0.5
        _table.Borders.Right.Width = 0.5
        _table.Rows.LeftIndent = 0
        'total: 16
        ' Before you can add a row, you must define the columns
        Dim column As Column = _table.AddColumn("3.5cm")
        column.Format.Alignment = ParagraphAlignment.Center

        column = _table.AddColumn("3cm")
        column.Format.Alignment = ParagraphAlignment.Right

        column = _table.AddColumn("3cm")
        column.Format.Alignment = ParagraphAlignment.Right

        column = _table.AddColumn("3.2cm")
        column.Format.Alignment = ParagraphAlignment.Right

        column = _table.AddColumn("2cm")
        column.Format.Alignment = ParagraphAlignment.Right

        column = _table.AddColumn("3.5cm")
        column.Format.Alignment = ParagraphAlignment.Right



        ' Create the header of the table
        Dim row As Row = _table.AddRow()
        row.HeadingFormat = True
        row.Format.Alignment = ParagraphAlignment.Center
        row.Format.Font.Bold = True
        row.Shading.Color = TableBlue
        row.Cells(0).AddParagraph("Account")
        'row.Cells(0).Format.Font.Bold = false
        row.Cells(0).Format.Alignment = ParagraphAlignment.Left
        'row.Cells(0).VerticalAlignment = VerticalAlignment.Bottom
        'row.Cells(0).MergeDown = 1
        row.Cells(1).AddParagraph("Date_Transaction")
        row.Cells(1).Format.Alignment = ParagraphAlignment.Left
        'row.Cells(1).MergeRight = 3
        row.Cells(2).AddParagraph("Amount")
        row.Cells(2).Format.Alignment = ParagraphAlignment.Left
        'row.Cells(2).VerticalAlignment = VerticalAlignment.Bottom
        'row.Cells(5).MergeDown = 1
        row.Cells(3).AddParagraph("No_Of_Transaction")
        row.Cells(3).Format.Alignment = ParagraphAlignment.Left
        row.Cells(4).AddParagraph("DRCR_IND")
        row.Cells(4).Format.Alignment = ParagraphAlignment.Left
        row.Cells(5).AddParagraph("Branch_Name")
        row.Cells(5).Format.Alignment = ParagraphAlignment.Left

    End Sub

    '...............Mizanur Rahman Work(29 Feb 2016)...........

    'Private Sub Create_CTR_Page()



    '    Dim section As Section = _document.AddSection()


    '    ' Create Header
    '    Dim paragraph As Paragraph = section.Headers.Primary.AddParagraph()
    '    paragraph.AddText("Qualified Transaction Report(CTR vs goAML)")
    '    paragraph.Format.Font.Size = 9
    '    paragraph.Format.Font.Bold = True
    '    paragraph.Format.Alignment = ParagraphAlignment.Center

    '    paragraph = section.Headers.Primary.AddParagraph()
    '    paragraph.AddText("Print Date: ")
    '    paragraph.Format.Font.Size = 8
    '    'paragraph.Format.Font.Bold = true
    '    paragraph.Format.Alignment = ParagraphAlignment.Right
    '    paragraph.AddDateField("dd-MMM-yyyy")

    '    ' Create footer
    '    paragraph = section.Footers.Primary.AddParagraph()
    '    paragraph.AddText("Page ")
    '    paragraph.AddPageField()
    '    paragraph.AddText(" of ")
    '    paragraph.AddNumPagesField()
    '    paragraph.Format.Font.Size = 7
    '    'paragraph.Format.Font.Bold = True
    '    paragraph.Format.Alignment = ParagraphAlignment.Center

    '    ' Create the item table
    '    _table = section.AddTable()
    '    _table.Style = "Table"
    '    _table.Borders.Color = TableBorder
    '    _table.Section.PageSetup.LeftMargin = 15
    '    _table.TopPadding = 5
    '    _table.BottomPadding = 5
    '    _table.Borders.Width = 0.25
    '    _table.Borders.Left.Width = 0.5
    '    _table.Borders.Right.Width = 0.5
    '    _table.Rows.LeftIndent = 0

    '    'total: 16
    '    ' Before you can add a row, you must define the columns
    '    Dim column As Column = _table.AddColumn("3.3cm")
    '    column.Format.Alignment = ParagraphAlignment.Center

    '    column = _table.AddColumn("2cm")
    '    column.Format.Alignment = ParagraphAlignment.Right

    '    column = _table.AddColumn("1cm")
    '    column.Format.Alignment = ParagraphAlignment.Right

    '    column = _table.AddColumn("2cm")
    '    column.Format.Alignment = ParagraphAlignment.Right

    '    column = _table.AddColumn("1.7cm")
    '    column.Format.Alignment = ParagraphAlignment.Right

    '    column = _table.AddColumn("2cm")
    '    column.Format.Alignment = ParagraphAlignment.Right

    '    column = _table.AddColumn("2cm")
    '    column.Format.Alignment = ParagraphAlignment.Right

    '    column = _table.AddColumn("1.5cm")
    '    column.Format.Alignment = ParagraphAlignment.Right

    '    column = _table.AddColumn("1.7cm")
    '    column.Format.Alignment = ParagraphAlignment.Right

    '    column = _table.AddColumn("3cm")
    '    column.Format.Alignment = ParagraphAlignment.Right

    '    ' Create the header of the table
    '    Dim row As Row = _table.AddRow()
    '    row.HeadingFormat = True
    '    row.Format.Alignment = ParagraphAlignment.Center
    '    row.Format.Font.Bold = True
    '    row.Shading.Color = TableBlue
    '    row.Cells(0).AddParagraph("Acc_Number")
    '    'row.Cells(0).Format.Font.Bold = false
    '    row.Cells(0).Format.Alignment = ParagraphAlignment.Left
    '    'row.Cells(0).VerticalAlignment = VerticalAlignment.Bottom
    '    'row.Cells(0).MergeDown = 1
    '    row.Cells(1).AddParagraph("Trans_Date")
    '    row.Cells(1).Format.Alignment = ParagraphAlignment.Left
    '    'row.Cells(1).MergeRight = 3
    '    row.Cells(2).AddParagraph("DRCR_IND")
    '    row.Cells(2).Format.Alignment = ParagraphAlignment.Left
    '    'row.Cells(2).VerticalAlignment = VerticalAlignment.Bottom
    '    'row.Cells(5).MergeDown = 1
    '    row.Cells(3).AddParagraph("CTR_Amount")
    '    row.Cells(3).Format.Alignment = ParagraphAlignment.Left
    '    row.Cells(4).AddParagraph("CTR_Trans_Sum")
    '    row.Cells(4).Format.Alignment = ParagraphAlignment.Left
    '    row.Cells(5).AddParagraph("GOAML_Amount")
    '    row.Cells(5).Format.Alignment = ParagraphAlignment.Left
    '    row.Cells(6).AddParagraph("GO_Trans_Sum")
    '    row.Cells(6).Format.Alignment = ParagraphAlignment.Left
    '    row.Cells(7).AddParagraph("Amount_Diff")
    '    row.Cells(7).Format.Alignment = ParagraphAlignment.Left
    '    row.Cells(8).AddParagraph("Trans_Diff")
    '    row.Cells(8).Format.Alignment = ParagraphAlignment.Left
    '    row.Cells(9).AddParagraph("Branch_Name")
    '    row.Cells(9).Format.Alignment = ParagraphAlignment.Left
    'End Sub

    '...............Mizanur Rahman Work(28/29 Feb 2016)...........

    Private Sub FillContentData_goAML()

        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String

        Dim strSearch As String = ""

        If NullHelper.ObjectToString(txtAcNumber.Text.Trim()) <> "" Then

            strSearch = " And ACCOUNT=@ACCNUMBER "

        End If

        If txtDateFrom.Text.Trim() <> "/  /" And txtDateTo.Text.Trim() <> "/  /" Then

            strSearch += " And DATE_TRANSACTION >=@DATE_FROM AND DATE_TRANSACTION <=@DATE_TO "

        End If


        'strSql = "select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
        '                 " from GO_TRANSACTION WHERE [STATUS]='L' " & strSearch & _
        '                 " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND "

        strSql = "select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND ,TRANSACTION_LOCATION " & _
                         " from GO_TRANSACTION WHERE [STATUS]='L' AND TRANSACTION_LOCATION =@TRANSACTION_LOCATION" & strSearch & _
                         " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND,TRANSACTION_LOCATION "





        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        dbCommand.Parameters.Clear()

        db.AddInParameter(dbCommand, "@ACCNUMBER", DbType.String, txtAcNumber.Text.Trim())

        db.AddInParameter(dbCommand, "@DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom.Text.Trim()))
        db.AddInParameter(dbCommand, "@DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo.Text.Trim()))

        db.AddInParameter(dbCommand, "@TRANSACTION_LOCATION", DbType.String, cmboBranch.SelectedValue.Trim())


        Dim dt As DataTable = db.ExecuteDataSet(dbCommand).Tables(0)


        Dim paragraph As Paragraph


        For i = 0 To dt.Rows.Count - 1

            Dim row1 As Row = _table.AddRow()

            row1.TopPadding = 1.5

            row1.Cells(0).AddParagraph((dt.Rows(i)("Account")))
            row1.Cells(0).Format.Alignment = ParagraphAlignment.Center

            paragraph = row1.Cells(1).AddParagraph((dt.Rows(i)("Date_Transaction")))
            row1.Cells(1).Format.Alignment = ParagraphAlignment.Left

            row1.Cells(2).AddParagraph(dt.Rows(i)("Amount"))
            row1.Cells(2).Format.Alignment = ParagraphAlignment.Left

            row1.Cells(3).AddParagraph(dt.Rows(i)("Number_Of_Transaction"))
            row1.Cells(3).Format.Alignment = ParagraphAlignment.Left

            row1.Cells(4).AddParagraph(dt.Rows(i)("DRCR_IND"))
            row1.Cells(4).Format.Alignment = ParagraphAlignment.Left

            row1.Cells(5).AddParagraph(dt.Rows(i)("TRANSACTION_LOCATION"))
            row1.Cells(5).Format.Alignment = ParagraphAlignment.Left

        Next i



    End Sub


    '...............Mizanur Rahman Work(01 March 2016)...........

    'Private Sub FillContentdata_CTR()

    '    Dim db As New SqlDatabase(CommonAppSet.ConnStr)

    '    Dim strSql As String

    '    Dim strSearch As String = ""

    '    If NullHelper.ObjectToString(txtAcNumber2.Text.Trim()) <> "" Then

    '        strSearch = " And ab.ACNUMBER=@ACCNUMBER "

    '    End If

    '    If txtDateFrom2.Text.Trim() <> "/  /" And txtDateTo2.Text.Trim() <> "/  /" Then

    '        strSearch += " And ab.TRANS_DATE >=@DATE_FROM AND ab.TRANS_DATE <=@DATE_TO "

    '    End If


    '    'strSql = "select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
    '    '                 " from GO_TRANSACTION WHERE [STATUS]='L' " & strSearch & _
    '    '                 " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND "

    '    'strSql = "SELECT   a.ACNUMBER, CONVERT(VARCHAR(11), a.TRANSDATE,106) As TRANS_DATE, SUM ( CONVERT(NUMERIC(18,2), a.TRANSAMOUNT)) CTR_AMOUNT,  SUM(a.TRANSNUM) CTR_TRANS_SUM, " & _
    '    '         " b.AMOUNT GOAML_AMOUNT , b.NUMBER_OF_TRANSACTION GO_TRANS_SUM FROM " & _
    '    '         "FIU_TRANSACTION a INNER JOIN (select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, " & _
    '    '                       " sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
    '    '                       " from GO_TRANSACTION WHERE [STATUS]='L' " & _
    '    '                       " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND) " & _
    '    '         " b ON a.ACNUMBER = b.ACCOUNT AND a.TRANSDATE = b.DATE_TRANSACTION WHERE a.RPTYPECODE='CTR'  " & strSearch & _
    '    '         " GROUP BY a.ACNUMBER, a.TRANSDATE , b.AMOUNT , b.NUMBER_OF_TRANSACTION "

    '    




    '    strSql = "SELECT ab.ACNUMBER, ab.TRANS_DATE, ab.DRCR_IND, ab.CTR_AMOUNT,  ab.CTR_TRANS_SUM,ab.GOAML_AMOUNT, ab.GO_TRANS_SUM, ab.AMOUNT_DIFF, ab.TRANS_DIFF, c.CitiBranch_Name " & _
    '             "FROM " & _
    '             " ( " & _
    '              "SELECT a.ACNUMBER, a.TRANS_DATE, b.DRCR_IND, a.CTR_AMOUNT,  a.CTR_TRANS_SUM, " & _
    '              " b.AMOUNT GOAML_AMOUNT , b.NUMBER_OF_TRANSACTION GO_TRANS_SUM, (a.CTR_AMOUNT - b.AMOUNT) AMOUNT_DIFF ,(a.CTR_TRANS_SUM- b.NUMBER_OF_TRANSACTION) TRANS_DIFF, a.BRANCH_CODE " & _
    '              "FROM ( SELECT   ACNUMBER, CONVERT(VARCHAR(11), TRANSDATE,106) As TRANS_DATE, " & _
    '              " SUM ( CONVERT(NUMERIC(18,2), TRANSAMOUNT)) CTR_AMOUNT,  SUM(TRANSNUM) CTR_TRANS_SUM, BRANCH_CODE, " & _
    '              " CASE TRTYPECODE " & _
    '             " WHEN 01 THEN 'C' " & _
    '             " WHEN 02 THEN 'D'  " & _
    '             " WHEN 05 THEN 'C' " & _
    '             " WHEN 06 THEN 'D' " & _
    '             " WHEN 04 THEN 'C' " & _
    '             " WHEN 18 THEN 'D' " & _
    '             " WHEN 08 THEN 'C' " & _
    '             " WHEN 19 THEN 'D' " & _
    '             " WHEN 03 THEN 'C' " & _
    '             " END as DRCR_IND, RPTYPECODE " & _
    '                      " FROM " & _
    '                      " FIU_TRANSACTION GROUP BY ACNUMBER, TRANSDATE ,BRANCH_CODE , CASE TRTYPECODE " & _
    '             " WHEN 01 THEN 'C' " & _
    '             " WHEN 02 THEN 'D'  " & _
    '             " WHEN 05 THEN 'C' " & _
    '             " WHEN 06 THEN 'D' " & _
    '             " WHEN 04 THEN 'C' " & _
    '             " WHEN 18 THEN 'D' " & _
    '             " WHEN 08 THEN 'C' " & _
    '             " WHEN 19 THEN 'D' " & _
    '             " WHEN 03 THEN 'C' " & _
    '            " END, RPTYPECODE ) a " & _
    '                     " FULL OUTER JOIN (select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, " & _
    '                                       " sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
    '                                       " from GO_TRANSACTION WHERE [STATUS]='L'  " & _
    '                                       " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND)  b " & _
    '            " ON a.ACNUMBER = b.ACCOUNT AND a.TRANS_DATE = b.DATE_TRANSACTION AND a.DRCR_IND = b.DRCR_IND WHERE a.RPTYPECODE = 'CTR' " & _
    '            " ) ab " & _
    '            " INNER JOIN (SELECT CitiBranch_Name,BRANCH_CODE FROM CitiBank_Branch Where CitiBranch_Name = @Branch_Name  GROUP BY CitiBranch_Name,BRANCH_CODE) c " & _
    '            "  ON ab.BRANCH_CODE = c.BRANCH_CODE " & strSearch




    '    Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

    '    dbCommand.Parameters.Clear()

    '    db.AddInParameter(dbCommand, "@ACCNUMBER", DbType.String, txtAcNumber2.Text.Trim())

    '    db.AddInParameter(dbCommand, "@DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom2.Text.Trim()))
    '    db.AddInParameter(dbCommand, "@DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo2.Text.Trim()))
    '    db.AddInParameter(dbCommand, "@Branch_Name", DbType.String, comboCtrBranch.SelectedValue.Trim())


    '    Dim dt As DataTable = db.ExecuteDataSet(dbCommand).Tables(0)


    '    Dim paragraph As Paragraph


    '    For i = 0 To dt.Rows.Count - 1

    '        Dim row1 As Row = _table.AddRow()

    '        row1.TopPadding = 1.5

    '        row1.Cells(0).AddParagraph((dt.Rows(i)("ACNUMBER")))
    '        row1.Cells(0).Format.Alignment = ParagraphAlignment.Center

    '        paragraph = row1.Cells(1).AddParagraph((dt.Rows(i)("Trans_Date")))
    '        row1.Cells(1).Format.Alignment = ParagraphAlignment.Left

    '        row1.Cells(2).AddParagraph(dt.Rows(i)("DRCR_IND").ToString())
    '        row1.Cells(2).Format.Alignment = ParagraphAlignment.Left

    '        row1.Cells(3).AddParagraph(dt.Rows(i)("CTR_Amount"))
    '        row1.Cells(3).Format.Alignment = ParagraphAlignment.Left

    '        row1.Cells(4).AddParagraph(dt.Rows(i)("CTR_Trans_Sum"))
    '        row1.Cells(4).Format.Alignment = ParagraphAlignment.Left

    '        row1.Cells(5).AddParagraph(dt.Rows(i)("GOAML_Amount").ToString())
    '        row1.Cells(5).Format.Alignment = ParagraphAlignment.Left

    '        row1.Cells(6).AddParagraph(dt.Rows(i)("GO_Trans_Sum").ToString())
    '        row1.Cells(6).Format.Alignment = ParagraphAlignment.Left

    '        row1.Cells(7).AddParagraph(dt.Rows(i)("Amount_Diff").ToString())
    '        row1.Cells(7).Format.Alignment = ParagraphAlignment.Left

    '        row1.Cells(8).AddParagraph(dt.Rows(i)("Trans_Diff").ToString())
    '        row1.Cells(8).Format.Alignment = ParagraphAlignment.Left

    '        row1.Cells(9).AddParagraph(dt.Rows(i)("CitiBranch_Name"))
    '        row1.Cells(9).Format.Alignment = ParagraphAlignment.Left

    '    Next i
    'End Sub

    '...............Mizanur Rahman Work(28 Feb 2016)...........

    Private Sub GoAMLReportToPDF()

        Try

            CreategoAMLReportDocument()
            _document.UseCmykColor = True


            'Create a renderer for PDF that uses Unicode font encoding
            Dim pdfRenderer As PdfDocumentRenderer = New PdfDocumentRenderer(True)

            ' Set the MigraDoc document
            pdfRenderer.Document = _document

            ' Create the PDF document
            pdfRenderer.RenderDocument()

            ' Save the PDF document...
            'Dim filename As String = "Invoice.pdf"
            '#If DEBUG Then
            '        // I don't want to close the document constantly...
            'filename = "Invoice-" + Guid.NewGuid().ToString("N").ToUpper() + ".pdf"
            '#End If

            ' ...and start a viewer.
            Dim i As Integer = 0
            Dim fName As String = _fileName.Split(".")(0)
            While (File.Exists(_fileName))
                Try
                    File.Delete(_fileName)
                Catch ex As Exception
                    i = i + 1
                    _fileName = fName + i.ToString() + ".pdf"
                End Try
            End While
            pdfRenderer.Save(_fileName)

            'Dim document As PdfDocument = PdfReader.Open(_fileName, PdfDocumentOpenMode.Modify)
            'If document.AcroForm.Elements.ContainsKey("/NeedAppearances") = False Then
            '    document.AcroForm.Elements.Add("/NeedAppearances", New PdfSharp.Pdf.PdfBoolean(True))
            'End If
            'Dim fields As PdfAcroField.PdfAcroFieldCollection = document.AcroForm.Fields


            'Dim names() As String = fields.Names
            'For idx As Integer = 0 To names.Length - 1
            '    Dim fqName As String = names(idx)
            '    Dim field As PdfAcroField = fields(fqName)
            '    Dim txtField As PdfTextField
            '    txtField = CType(field, PdfTextField)
            '    If Not txtField Is Nothing Then
            '        txtField.ReadOnly = True
            '    End If
            'Next
            'document.Save(_fileName)


            Process.Start(_fileName)

        Catch ex As Exception
            MessageBox.Show("Proc: ReportToPDF" + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    '...............Mizanur Rahman Work(29 Feb 2016)...........

    'Private Sub CTR_ReportToPdf()
    '    Try

    '        CreateCTRReportDocument()
    '        _document.UseCmykColor = True


    '        ' Create a renderer for PDF that uses Unicode font encoding
    '        Dim pdfRenderer As PdfDocumentRenderer = New PdfDocumentRenderer(True)

    '        ' Set the MigraDoc document
    '        pdfRenderer.Document = _document

    '        ' Create the PDF document
    '        pdfRenderer.RenderDocument()

    '        ' Save the PDF document...
    '        'Dim filename As String = "Invoice.pdf"
    '        '#If DEBUG Then
    '        '        // I don't want to close the document constantly...
    '        'filename = "Invoice-" + Guid.NewGuid().ToString("N").ToUpper() + ".pdf"
    '        '#End If
    '        pdfRenderer.Save(_fileName)
    '        ' ...and start a viewer.
    '        Process.Start(_fileName)

    '    Catch ex As Exception
    '        MessageBox.Show("Proc: ReportToPDF" + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    Private Sub ExportToXlCTR()


        Dim db As New SqlDatabase(CommonAppSet.ConnStr)

        Dim strSql As String

        Dim strSearch As String = ""

        If NullHelper.ObjectToString(txtAcNumber2.Text.Trim()) <> "" Then

            strSearch = " And a.ACNUMBER=@ACCNUMBER "

        End If

        If txtDateFrom2.Text.Trim() <> "/  /" And txtDateTo2.Text.Trim() <> "/  /" Then

            strSearch += " And a.TRANS_DATE >=@DATE_FROM AND a.TRANS_DATE <=@DATE_TO "

        End If


        'strSql = "select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
        '                 " from GO_TRANSACTION WHERE [STATUS]='L' " & strSearch & _
        '                 " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND "

        'strSql = "SELECT   a.ACNUMBER, CONVERT(VARCHAR(11), a.TRANSDATE,106) As TRANS_DATE, SUM ( CONVERT(NUMERIC(18,2), a.TRANSAMOUNT)) CTR_AMOUNT,  SUM(a.TRANSNUM) CTR_TRANS_SUM, " & _
        '         " b.AMOUNT GOAML_AMOUNT , b.NUMBER_OF_TRANSACTION GO_TRANS_SUM FROM " & _
        '         "FIU_TRANSACTION a INNER JOIN (select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, " & _
        '                       " sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
        '                       " from GO_TRANSACTION WHERE [STATUS]='L' " & _
        '                       " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND) " & _
        '         " b ON a.ACNUMBER = b.ACCOUNT AND a.TRANSDATE = b.DATE_TRANSACTION WHERE a.RPTYPECODE='CTR'  " & strSearch & _
        '         " GROUP BY a.ACNUMBER, a.TRANSDATE , b.AMOUNT , b.NUMBER_OF_TRANSACTION "

        strSql = " SELECT a.ACNUMBER, a.TRANS_DATE, b.DRCR_IND, a.CTR_AMOUNT,  a.CTR_TRANS_SUM, " & _
              " b.AMOUNT GOAML_AMOUNT , b.NUMBER_OF_TRANSACTION GO_TRANS_SUM, (a.CTR_AMOUNT - b.AMOUNT) AMOUNT_DIFF ,(a.CTR_TRANS_SUM- b.NUMBER_OF_TRANSACTION) TRANS_DIFF " & _
              " FROM  ( SELECT   ACNUMBER, CONVERT(VARCHAR(11), TRANSDATE,106) As TRANS_DATE, " & _
              " SUM ( CONVERT(NUMERIC(18,2), TRANSAMOUNT)) CTR_AMOUNT,  SUM(TRANSNUM) CTR_TRANS_SUM,  " & _
              " CASE TRTYPECODE " & _
             " WHEN 01 THEN 'C' " & _
             " WHEN 02 THEN 'D'  " & _
             " WHEN 05 THEN 'C' " & _
             " WHEN 06 THEN 'D' " & _
             " WHEN 04 THEN 'C' " & _
             " WHEN 18 THEN 'D' " & _
             " WHEN 08 THEN 'C' " & _
             " WHEN 19 THEN 'D' " & _
             " WHEN 03 THEN 'C' " & _
             " END as DRCR_IND, RPTYPECODE " & _
                      " FROM " & _
                      " FIU_TRANSACTION GROUP BY ACNUMBER, TRANSDATE , CASE TRTYPECODE " & _
             " WHEN 01 THEN 'C' " & _
             " WHEN 02 THEN 'D'  " & _
             " WHEN 05 THEN 'C' " & _
             " WHEN 06 THEN 'D' " & _
             " WHEN 04 THEN 'C' " & _
             " WHEN 18 THEN 'D' " & _
             " WHEN 08 THEN 'C' " & _
             " WHEN 19 THEN 'D' " & _
             " WHEN 03 THEN 'C' " & _
            " END, RPTYPECODE ) a " & _
                     " FULL OUTER JOIN (select ACCOUNT, CONVERT(VARCHAR(11), DATE_TRANSACTION,106) As DATE_TRANSACTION, " & _
                                       " sum (AMOUNT_LOCAL)as AMOUNT, COUNT(DRCR_IND)As NUMBER_OF_TRANSACTION, DRCR_IND " & _
                                       " from GO_TRANSACTION WHERE [STATUS]='L'  " & _
                                       " GROUP BY ACCOUNT,DATE_TRANSACTION,DRCR_IND)  b " & _
            " ON a.ACNUMBER = b.ACCOUNT AND a.TRANS_DATE = b.DATE_TRANSACTION AND a.DRCR_IND = b.DRCR_IND WHERE a.RPTYPECODE ='CTR' " & strSearch




        Dim dbCommand As DbCommand = db.GetSqlStringCommand(strSql)

        dbCommand.Parameters.Clear()

        db.AddInParameter(dbCommand, "@ACCNUMBER", DbType.String, txtAcNumber2.Text.Trim())

        db.AddInParameter(dbCommand, "@DATE_FROM", DbType.Date, NullHelper.StringToDate(txtDateFrom2.Text.Trim()))
        db.AddInParameter(dbCommand, "@DATE_TO", DbType.Date, NullHelper.StringToDate(txtDateTo2.Text.Trim()))


        Dim ds As DataSet = db.ExecuteDataSet(dbCommand)

        '--------------


        Dim xlApp As New Excel.Application

        Dim wb As Excel.Workbook = xlApp.Workbooks.Add

        Dim sheet As Excel.Worksheet = wb.Worksheets.Add

        sheet.Name = "CTR vs goAML Transaction Report"



        Dim i, j As Integer

        For j = 0 To ds.Tables(0).Columns.Count - 1
            sheet.Cells(1, j + 1) = ds.Tables(0).Columns(j).ColumnName
        Next j

        For i = 0 To ds.Tables(0).Rows.Count - 1
            For j = 0 To ds.Tables(0).Columns.Count - 1
                sheet.Cells(i + 2, j + 1) = ds.Tables(0).Rows(i)(j).ToString()
            Next j
        Next i
        CType(sheet.Rows(1, Type.Missing), Excel.Range).Font.Bold = True

        xlApp.Visible = True
        wb.Activate()
        log_message = " Show CTR vs GoAML Report "
        Logger.system_log(log_message)

    End Sub


#End Region


    Private Sub FrmQualifiedTransgoAML_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If opt.IsShow = False Then
            MessageBox.Show("You are not authorized", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        CommonUtil.FillComboBox("SELECT CitiBranch_Name,CitiBranch_Name  FROM CitiBank_Branch", cmboBranch)
        'CommonUtil.FillComboBox("select distinct AC_BRANCH,AC_BRANCH from IMP_FLEX_TRANS", comboCtrBranch)
        'CommonUtil.FillComboBox("SELECT CitiBranch_Name,CitiBranch_Name  FROM CitiBank_Branch", comboCtrBranch)
    End Sub



    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click


        Try

            ExportToXl()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub brnReportCTR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brnReportCTR.Click
        Try

            ExportToXlCTR()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        Try
            'SaveFileDialog1.Filter = "Pdf files (*.pdf)|*.pdf"
            'SaveFileDialog1.ShowDialog()
            'SaveFileDialog1.CheckFileExists = True

            'If SaveFileDialog1.FileName.Trim() = "" Then
            '    MessageBox.Show("Report destination required !!")
            '    Me.Close()
            'End If

            '_fileName = SaveFileDialog1.FileName.Trim()

            'If _fileName.Trim() <> "" Then  

            _fileName = "goAML_Transaction.pdf"

            GoAMLReportToPDF()

            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub btnPdfReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdfReport.Click
    '    Try

    '        _fileName = "CTR_Transaction.pdf"

    '        CTR_ReportToPdf()

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub
End Class