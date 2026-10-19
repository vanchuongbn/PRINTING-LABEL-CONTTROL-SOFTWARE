Imports System.IO
Imports ClosedXML.Excel


Public Class Ford_Artoo_CreateSN

    Dim dal_common As DAL_Common = New DAL_Common()
    Dim dal_CreateSN As DAL_Ford_CreateSN = New DAL_Ford_CreateSN()

    Private Sub Ford_Artoo_CreateSN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Rad_Artoo.Checked = True
        txt_y_d_Number.Text = Convert.ToDateTime(DateTime.Now).ToString("yy") + Convert.ToDateTime(DateTime.Now).DayOfYear.ToString().PadLeft(3, "0")
    End Sub

    Private Sub txtWO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWO.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            Dim dt As System.Data.DataTable = New System.Data.DataTable()
            dt = dal_common.Get_WO_Information(txtWO.Text.Trim().ToString())
            If dt Is Nothing Or dt.Rows.Count = 0 Then
                MessageBox.Show("Không tìm thấy thông tin WO! vui lòng liên hệ bộ phận plan để kiểm tra và tạo thông tin!")
                Exit Sub
            Else
                txt_Qty.Text = dt.Rows(0)("OWO_QUANTITY").ToString().Trim()
                Dim PN As String = dt.Rows(0)("MODEL_CODE").ToString()
                txtPN.Text = PN
                dt = New System.Data.DataTable()
                Dim Type As Integer = 1
                If Rad_Artoo.Checked Then
                    Type = 1
                ElseIf Rad_EiOne.Checked Then
                    Type = 2
                ElseIf Rad_Eitwo.Checked Then
                    Type = 3
                ElseIf Rad_Eithree.Checked Then
                    Type = 4
                ElseIf Rad_Kaytoo_N_SB.Checked Then
                    Type = 5
                ElseIf Rad_Kaytoo_M_ES.Checked Then
                    Type = 6
                ElseIf Rad_Kaytoo_M_SB.Checked Then
                    Type = 7
                ElseIf Rad_Kaytoo_M_AB.Checked Then
                    Type = 8
                Else
                    Type = 1
                End If
                dt = dal_CreateSN.GetLast_50_SN_By_WO(Type, Convert.ToInt32(txt_y_d_Number.Text.Trim), PN)
                DataGridView2.DataSource = dt
                dt = New System.Data.DataTable()
                dt = dal_CreateSN.Ford_GetHistory_SN(Type, txtWO.Text.Trim.ToString())
                DataGridView1.DataSource = dt
            End If
        End If

    End Sub

    Private Sub tbnNew_Click(sender As Object, e As EventArgs) Handles tbnNew.Click
        If txtPN.Text.Trim = "" Then
            MessageBox.Show("Vui lòng nhập Wo!")
            Exit Sub
        ElseIf txtPN.Text.Trim = "" Then
            MessageBox.Show("Vui lòng nhập PN!")
            Exit Sub
        Else
            Dim Type As Integer
            Dim Wo As String = txtWO.Text.ToString.Trim
            Dim Y_and_D_Number As String = txt_y_d_Number.Text.Trim
            Dim PN As String = txtPN.Text.Trim
            Dim Qty As Integer = Convert.ToInt32(txt_Qty.Text.Trim)
            Dim Supplier_Code As String = lbl_Supplier_Code.Text.Trim
            Dim Customer_Assembly_Code As String = lbl_customerAssembly_Code.Text.Trim
            Dim Type_Str As String = ""
            If Rad_Artoo.Checked Then
                Type = 1
                Type_Str = "Rad_Artoo"
            ElseIf Rad_EiOne.Checked Then
                Type = 2
                Type_Str = "Rad_EiOne"
            ElseIf Rad_Eitwo.Checked Then
                Type = 3
                Type_Str = "Rad_Eitwo"
            ElseIf Rad_Eithree.Checked Then
                Type = 4
                Type_Str = "ElThree"
            ElseIf Rad_Kaytoo_N_SB.Checked Then
                Type = 5
                Type_Str = "Kaytoo Heavy (SB)"
            ElseIf Rad_Kaytoo_M_ES.Checked Then
                Type = 6
                Type_Str = "Kaytoo Mdium (ES)"
            ElseIf Rad_Kaytoo_M_SB.Checked Then
                Type = 7
                Type_Str = "Kaytoo Mdium (SB)"
            ElseIf Rad_Kaytoo_M_AB.Checked Then
                Type = 8
                Type_Str = "Kaytoo Mdium - Single (AB)"
            End If
            Dim dt As System.Data.DataTable = New System.Data.DataTable()
            dt = dal_CreateSN.Ford_CreateSN(Type, Wo, txt_y_d_Number.Text.Trim, txtPN.Text.ToString.Trim, Convert.ToDateTime(DateTimePicker1.Text).ToString("yyyy-MM-dd"), "", Qty, Supplier_Code, Customer_Assembly_Code, Type_Str)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                If Convert.ToInt32(dt.Rows(0)("Status")) = 1 Then
                    DataGridView1.DataSource = dt
                ElseIf Convert.ToInt32(dt.Rows(0)("Status")) = 2 Then
                    MessageBox.Show("Hiện tại bạn đã  tạo đủ số lượng SN của wo này vui lòng kiểm tra lại!")
                Else
                    MessageBox.Show("Không thể tạo được SN vui lòng tạo lại wo này!")
                End If
            Else
                MessageBox.Show("Không thể tạo được SN vui lòng liên hệ với bộ phận IT để kiểm tra lại!")

            End If
        End If
    End Sub

    Private Sub btnGetSNList_Click(sender As Object, e As EventArgs) Handles btnGetSNList.Click
        SN_Export_file()
    End Sub

    Private Sub SN_Export_file()
        Using sfd As New SaveFileDialog()
            sfd.Filter = "Excel File (*.xlsx)|*.xlsx"
            sfd.FileName = "SN_Export.xlsx"

            If sfd.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim wb As New XLWorkbook()
            Dim ws = wb.Worksheets.Add("SN_Data")

            ' ===== HEADER =====
            For col As Integer = 0 To DataGridView1.Columns.Count - 1
                ws.Cell(1, col + 1).Value = DataGridView1.Columns(col).HeaderText
            Next

            ' ===== DATA =====
            For row As Integer = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(row).IsNewRow Then Continue For

                For col As Integer = 0 To DataGridView1.Columns.Count - 1
                    Dim dgvCell = DataGridView1.Rows(row).Cells(col)
                    Dim xlCell = ws.Cell(row + 2, col + 1)

                    If dgvCell.Value Is Nothing OrElse dgvCell.Value Is DBNull.Value Then
                        xlCell.Value = ""
                        Continue For
                    End If

                    If DataGridView1.Columns(col).Name = "SN" Then
                        xlCell.Style.NumberFormat.Format = "@"
                        xlCell.Value = dgvCell.Value.ToString().Trim()
                    Else
                        xlCell.Value = dgvCell.Value.ToString().Trim()
                    End If
                Next
            Next

            ws.Columns().AdjustToContents()

            wb.SaveAs(sfd.FileName)
            MessageBox.Show("Export Excel thành công!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If File.Exists(sfd.FileName) Then
                Process.Start(sfd.FileName)
            End If
        End Using
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        MessageBox.Show("you have changed to: " + DateTimePicker1.Text.ToString())

        txt_y_d_Number.Text = Convert.ToDateTime(DateTimePicker1.Value).ToString("yy") + Convert.ToDateTime(DateTimePicker1.Value).DayOfYear.ToString().PadLeft(3, "0")
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        txtWO.Text = ""
        txtPN.Text = ""
        txt_Qty.Text = ""
        DataGridView1.DataSource = Nothing
        DataGridView2.DataSource = Nothing
        Rad_Artoo.Checked = True
    End Sub

    Private Sub Rad_Artoo_CheckedChanged(sender As Object, e As EventArgs) Handles Rad_Artoo.CheckedChanged
        If (Rad_Artoo.Checked) Then

            lbl_customerAssembly_Code.Text = "V4GB-19K392-NA"
            Rad_EiOne.Checked = False
            Rad_Eitwo.Checked = False
            Rad_Eithree.Checked = False
            Rad_Kaytoo_N_SB.Checked = False
            Rad_Kaytoo_M_ES.Checked = False
            Rad_Kaytoo_M_SB.Checked = False
            Rad_Kaytoo_M_AB.Checked = False
            DataGridView2.DataSource = Nothing
        End If
    End Sub
    Private Sub Rad_EiOne_CheckedChanged(sender As Object, e As EventArgs) Handles Rad_EiOne.CheckedChanged
        If Rad_EiOne.Checked Then
            lbl_customerAssembly_Code.Text = "V4GB-19K392-AA"
            Rad_Artoo.Checked = False
            Rad_Eitwo.Checked = False
            Rad_Eithree.Checked = False
            Rad_Kaytoo_N_SB.Checked = False
            Rad_Kaytoo_M_ES.Checked = False
            Rad_Kaytoo_M_SB.Checked = False
            Rad_Kaytoo_M_AB.Checked = False
            DataGridView2.DataSource = Nothing
        End If

    End Sub
    Private Sub Rad_Eitwo_CheckedChanged(sender As Object, e As EventArgs) Handles Rad_Eitwo.CheckedChanged
        If Rad_Eitwo.Checked Then
            lbl_customerAssembly_Code.Text = "V4GB-19K392-BA"
            Rad_Artoo.Checked = False
            Rad_EiOne.Checked = False
            Rad_Eithree.Checked = False
            Rad_Kaytoo_N_SB.Checked = False
            Rad_Kaytoo_M_ES.Checked = False
            Rad_Kaytoo_M_SB.Checked = False
            Rad_Kaytoo_M_AB.Checked = False
            DataGridView2.DataSource = Nothing
        End If
    End Sub

    Private Sub Rad_Eithree_CheckedChanged(sender As Object, e As EventArgs) Handles Rad_Eithree.CheckedChanged
        If Rad_Eithree.Checked Then
            lbl_customerAssembly_Code.Text = "V4GB-19K392-CA"
            Rad_Artoo.Checked = False
            Rad_EiOne.Checked = False
            Rad_Eitwo.Checked = False
            Rad_Kaytoo_N_SB.Checked = False
            Rad_Kaytoo_M_ES.Checked = False
            Rad_Kaytoo_M_SB.Checked = False
            Rad_Kaytoo_M_AB.Checked = False
            DataGridView2.DataSource = Nothing
        End If
    End Sub

    Private Sub Rad_Kaytoo_N_SB_CheckedChanged(sender As Object, e As EventArgs) Handles Rad_Kaytoo_N_SB.CheckedChanged
        If Rad_Kaytoo_N_SB.Checked Then
            lbl_customerAssembly_Code.Text = "V4GB-19K392-GA"
            Rad_Artoo.Checked = False
            Rad_EiOne.Checked = False
            Rad_Eitwo.Checked = False
            Rad_Eithree.Checked = False
            Rad_Kaytoo_M_ES.Checked = False
            Rad_Kaytoo_M_SB.Checked = False
            Rad_Kaytoo_M_AB.Checked = False
            DataGridView2.DataSource = Nothing
        End If
    End Sub

    Private Sub Rad_Kaytoo_M_ES_CheckedChanged(sender As Object, e As EventArgs) Handles Rad_Kaytoo_M_ES.CheckedChanged
        If Rad_Kaytoo_M_ES.Checked Then
            lbl_customerAssembly_Code.Text = "V4GB-19K392-DA"
            Rad_Artoo.Checked = False
            Rad_EiOne.Checked = False
            Rad_Eitwo.Checked = False
            Rad_Eithree.Checked = False
            Rad_Kaytoo_N_SB.Checked = False
            Rad_Kaytoo_M_SB.Checked = False
            Rad_Kaytoo_M_AB.Checked = False
            DataGridView2.DataSource = Nothing
        End If
    End Sub

    Private Sub Rad_Kaytoo_M_SB_CheckedChanged(sender As Object, e As EventArgs) Handles Rad_Kaytoo_M_SB.CheckedChanged
        If Rad_Kaytoo_M_SB.Checked Then
            lbl_customerAssembly_Code.Text = "V4GB-19K392-FA"
            Rad_Artoo.Checked = False
            Rad_EiOne.Checked = False
            Rad_Eitwo.Checked = False
            Rad_Eithree.Checked = False
            Rad_Kaytoo_N_SB.Checked = False
            Rad_Kaytoo_M_ES.Checked = False
            Rad_Kaytoo_M_AB.Checked = False
            DataGridView2.DataSource = Nothing
        End If
    End Sub

    Private Sub Rad_Kaytoo_M_AB_CheckedChanged(sender As Object, e As EventArgs) Handles Rad_Kaytoo_M_AB.CheckedChanged
        If Rad_Kaytoo_M_AB.Checked Then
            lbl_customerAssembly_Code.Text = "V4GB-19K392-EA"
            Rad_Artoo.Checked = False
            Rad_EiOne.Checked = False
            Rad_Eitwo.Checked = False
            Rad_Eithree.Checked = False
            Rad_Kaytoo_N_SB.Checked = False
            Rad_Kaytoo_M_ES.Checked = False
            Rad_Kaytoo_M_SB.Checked = False
            DataGridView2.DataSource = Nothing
        End If
    End Sub

    Private Sub btn_export2_Click(sender As Object, e As EventArgs) Handles btn_export2.Click
        Using sfd As New SaveFileDialog()
            sfd.Filter = "Excel File (*.xlsx)|*.xlsx"
            sfd.FileName = "SN_Export.xlsx"

            If sfd.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim wb As New XLWorkbook()
            Dim ws = wb.Worksheets.Add("SN_Data")

            For col As Integer = 0 To DataGridView1.Columns.Count - 1
                ws.Cell(1, col + 1).Value = DataGridView1.Columns(col).HeaderText
            Next

            ' Thêm cột SN_Se ở cuối
            Dim snSeCol As Integer = DataGridView1.Columns.Count + 1
            ws.Cell(1, snSeCol).Value = "SN_Se"

            For row As Integer = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(row).IsNewRow Then Continue For

                For col As Integer = 0 To DataGridView1.Columns.Count - 1
                    Dim dgvCell = DataGridView1.Rows(row).Cells(col)
                    Dim xlCell = ws.Cell(row + 2, col + 1)

                    If dgvCell.Value Is Nothing OrElse dgvCell.Value Is DBNull.Value Then
                        xlCell.Value = ""
                        Continue For
                    End If

                    If DataGridView1.Columns(col).Name = "SN" Then
                        xlCell.Style.NumberFormat.Format = "@"
                        xlCell.Value = dgvCell.Value.ToString().Trim()
                    Else
                        xlCell.Value = dgvCell.Value.ToString().Trim()
                    End If
                Next

                ' ===== Gán giá trị cho cột SN_Se =====
                Dim col8Value As String = ""

                If DataGridView1.Rows(row).Cells(7).Value IsNot Nothing Then
                    col8Value = DataGridView1.Rows(row).Cells(7).Value.ToString().Trim()
                End If

                ws.Cell(row + 2, snSeCol).Value = "A" & col8Value.PadLeft(4, "0"c)

            Next

            ws.Columns().AdjustToContents()

            wb.SaveAs(sfd.FileName)
            MessageBox.Show("Export Excel thành công!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If File.Exists(sfd.FileName) Then
                Process.Start(sfd.FileName)
            End If
        End Using

        ''---------------------------

        'If ((Me.DataGridView1.Columns.Count = 0) Or (Me.DataGridView1.Rows.Count = 0)) Then
        '    Exit Sub
        'End If

        ''Creating dataset to export
        'Dim dset As New DataSet
        ''add table to dataset
        'dset.Tables.Add()
        ''add column to that table
        'For i As Integer = 0 To Me.DataGridView1.ColumnCount - 1
        '    dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(i).HeaderText)
        'Next

        'dset.Tables(0).Columns.Add("SN_Se")
        'Dim col8Value As String = ""
        ''add rows to the table
        'Dim dr1 As DataRow
        'For i As Integer = 0 To DataGridView1.RowCount - 2
        '    dr1 = dset.Tables(0).NewRow
        '    For j As Integer = 0 To DataGridView1.Columns.Count - 1
        '        dr1(j) = Trim(DataGridView1.Rows(i).Cells(j).Value.ToString.Trim)
        '    Next
        '    'Lay du lieu cot thu 8 (index = 7)
        '    col8Value = Trim(DataGridView1.Rows(i).Cells(7).Value.ToString.Trim)

        '    'Them gia tri cho cot SN_Se
        '    dr1("SN_Se") = "A" & col8Value.PadLeft(4, "0"c)

        '    dset.Tables(0).Rows.Add(dr1)
        'Next

        'Dim excel As New Excel.Application()
        'Dim wBook As Excel.Workbook
        'Dim wSheet As Excel.Worksheet


        'wBook = excel.Workbooks.Add()
        'wSheet = wBook.ActiveSheet()

        'With wSheet
        '    Dim rng As Range

        '    rng = wSheet.Columns("B:B")
        '    rng.Cells.NumberFormat = "@"  ' Format dang text cho mot vung tren excel
        'End With

        ''excel.Cells.NumberFormat = "@" ' Format dang text cho tat ca cac cell



        'Dim dt As System.Data.DataTable = dset.Tables(0)
        'Dim dc As System.Data.DataColumn
        'Dim dr As System.Data.DataRow
        'Dim colIndex As Integer = 0
        'Dim rowIndex As Integer = 0

        'For Each dc In dt.Columns
        '    colIndex = colIndex + 1
        '    excel.Cells(1, colIndex) = dc.ColumnName

        'Next

        'For Each dr In dt.Rows
        '    rowIndex = rowIndex + 1
        '    colIndex = 0
        '    For Each dc In dt.Columns
        '        colIndex = colIndex + 1
        '        excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
        '    Next
        'Next

        'wSheet.Columns.AutoFit()



        'Dim dialog As SaveFileDialog = New SaveFileDialog

        'Dim strFileName As String = "File.xlsx"
        'dialog.FileName = strFileName
        'dialog.ShowDialog()
        'Dim blnFileOpen As Boolean = False
        'Try
        '    Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
        '    fileTemp.Close()
        'Catch ex As Exception
        '    blnFileOpen = False
        'End Try

        'If System.IO.File.Exists(strFileName) Then
        '    System.IO.File.Delete(strFileName)
        'End If

        'wBook.SaveAs(strFileName)
        'excel.Workbooks.Open(strFileName)
        'excel.Visible = True

    End Sub
End Class