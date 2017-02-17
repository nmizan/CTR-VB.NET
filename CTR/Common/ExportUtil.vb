Imports Microsoft.Office.Interop

Public Class ExportUtil

    Private _dgView As DataGridView
    Private _sheetName As String = "Export Data"
    Dim log_message As String = ""

    Public Sub New(ByVal dgView As DataGridView)
        _dgView = dgView

    End Sub

    Public Sub New(ByVal dgView As DataGridView, ByVal sheetName As String)
        _dgView = dgView

        If sheetName.Trim() <> "" Then
            _sheetName = sheetName

        End If
    End Sub
    Public Sub ExportXl()

        Try


            Dim totCol As Integer = _dgView.Columns.Count
            Dim totRow As Integer = _dgView.Rows.Count

            '--------------

            ' Copy the DataTable to an object array
            'Dim rawData(dgView.Rows.Count, dgView.Columns.Count - 1) As Object
            Dim rawData(totRow, totCol - 1) As Object

            ' Copy the column names to the first row of the object array
            Dim relCol As Integer = 0

            For col = 0 To totCol - 1

                If Not (_dgView.Columns(col).HeaderText.Trim() = "" Or _dgView.Columns(col).Visible = False) Then
                    rawData(0, relCol) = _dgView.Columns(col).HeaderText
                    relCol = relCol + 1

                End If

            Next


            For row = 0 To totRow - 1
                relCol = 0
                For col = 0 To totCol - 1
                    If Not (_dgView.Columns(col).HeaderText.Trim() = "" Or _dgView.Columns(col).Visible = False) Then
                        'rawData(row + 1, relCol) = NullHelper.StringToObject(_dgView.Rows(row).Cells(col).Value) 'dt.Rows(row).ItemArray(col)
                        rawData(row + 1, relCol) = NullHelper.ObjectToXL(_dgView.Rows(row).Cells(col).Value) 'dt.Rows(row).ItemArray(col)
                        relCol = relCol + 1

                    End If

                Next

            Next


            ' Calculate the final column letter
            Dim finalColLetter As String = String.Empty
            Dim colCharset As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            Dim colCharsetLen As Integer = colCharset.Length

            If totCol > colCharsetLen Then
                finalColLetter = colCharset.Substring( _
                (totCol - 1) \ colCharsetLen - 1, 1)
            End If

            finalColLetter += colCharset.Substring( _
              (totCol - 1) Mod colCharsetLen, 1)

            Dim xlApp As New Excel.Application

            Dim wb As Excel.Workbook = xlApp.Workbooks.Add

            Dim sheet As Excel.Worksheet = wb.Worksheets.Add

            sheet.Name = _sheetName

            Dim excelRange As String = String.Format("A1:{0}{1}", finalColLetter, _dgView.Rows.Count + 1)

            sheet.Range(excelRange, Type.Missing).Value2 = rawData
            sheet.Range(excelRange, Type.Missing).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop
            'CType(sheet.Rows(1, Type.Missing), Excel.Range).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop

            CType(sheet.Rows(1, Type.Missing), Excel.Range).Font.Bold = True


            xlApp.Visible = True
            wb.Activate()



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
End Class

