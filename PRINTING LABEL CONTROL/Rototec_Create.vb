Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel
Imports DocumentFormat.OpenXml.Drawing

Public Class Rototec_Create
    Dim Roto_Digi_Fixed As String = "103"

    Private Sub txtWO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWO.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            Cleartext()

            LoadWO_Information()
            HISTORY_BYWO()
        End If
    End Sub

    Private Sub Cleartext()
        txtPN.Clear()
        txtQty.Clear()
        txtDatecode.Clear()

    End Sub
    Private Sub LoadWO_Information()
        Try
            Dim Model_Code As String
            conn = New SqlConnection(Strconn)
            Dim cmd = New SqlCommand("spGetWO_infor'" & Me.txtWO.Text.Trim & "'", conn)
            conn.Open()
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtQty.Text = CInt(dr("OWO_QUANTITY").ToString.Trim)
                Me.txtPN.Text = dr("MODEL_CODE").ToString.Trim
                Model_Code = dr("MODEL_CODE").ToString.Trim


                Dim issued_date As String
                issued_date = dr("ISSUED_DATE")
                Dim yy As String
                If Microsoft.VisualBasic.Right(issued_date, 11) = "12:00:00 AM" Then
                    yy = Microsoft.VisualBasic.Mid(issued_date, issued_date.IndexOf("12:00:00 AM") - 2, 2)

                Else
                    yy = Microsoft.VisualBasic.Right(issued_date, 2)

                End If

                Dim week As String

                week = Replace(Microsoft.VisualBasic.Right((dr("WORK_WEEK").ToString.Trim), 2), "_", 0)

                Dim ww As String
                ww = week


                txtDatecode.Text = yy & ww

            End While
            dr.Close()
            cmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try



    End Sub
    Private Sub HISTORY_BYWO()

        Try
            Dim Count_history As Integer = GetCount_SN_ByWO(txtWO.Text.Trim)
            If (Count_history > 0) Then
                MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")
                PCBA_SN_Log_History_Load()
            Else
                MsgBox("Please click 'Create' Button to create new SN list")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try



    End Sub
    Private Sub PCBA_SN_Log_History_Load()
        Dim cn As New SqlConnection(Strconn)
        Try

            cn.Open()
            Dim Str_Querry = "select ID,[Date], Wo, PN, Datecode, SN, [Sequence] from Rototec_SN_log where WO = @Wo order by [Sequence]"
            Dim cmd As New SqlCommand(Str_Querry, cn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@Wo", SqlDbType.VarChar).Value = Me.txtWO.Text.ToString.Trim
            Dim da = New SqlDataAdapter(cmd)
            Dim ds = New DataSet
            da.Fill(ds, 0)
            Dim Dtv As DataView = New DataView(ds.Tables(0))
            With dgwSN
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message)
        Finally
            cn.Close()
        End Try

    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Dim PN As String
        If (txtPN.Text.ToString.Trim = "") Then
            MessageBox.Show("Please enter the WO!")
            Return
        End If
        If Me.txtQty.Text = CInt(GetCount_SN_ByWO(txtWO.Text.Trim)) Then
            MessageBox.Show("The WO is enought SNs!")
            Return
        End If
        PN = txtPN.ToString.Trim

        CreateSN_start_End()
        createSN_List()
        PCBA_SN_Log_History_Load()
    End Sub

    Private Sub CreateSN_start_End()
        If txtWO.Text.ToString.Trim = "" Then
            MessageBox.Show("Please Enter the wo!")
            Return
        End If

        Dim count As Integer = 0
        count = GetCount_SN(txtDatecode.Text.Trim, txtPN.Text.Trim)
        If count > 0 Then
            Dim c As String = CStr(count + 1)

            If Len(c) = 1 Then
                txtSN_Start.Text = "000" & c
            ElseIf Len(c) = 2 Then
                txtSN_Start.Text = "00" & c
            ElseIf Len(c) = 3 Then
                txtSN_Start.Text = "0" & c
            ElseIf Len(c) = 4 Then
                txtSN_Start.Text = c
            Else
                MsgBox("OUT OF SN RANGE")
            End If

            Dim d As String = CStr(count + CInt(txtQty.Text.Trim))
            If Len(d) = 1 Then
                txtSN_end.Text = "000" & d
            ElseIf Len(d) = 2 Then
                txtSN_end.Text = "00" & d
            ElseIf Len(d) = 3 Then
                txtSN_end.Text = "0" & d
            ElseIf Len(d) = 4 Then

                txtSN_end.Text = d
            Else
                MsgBox("OUT OF SN RANGE")

            End If

        Else

            txtSN_Start.Text = "0001"

            Dim a As String = CStr(count + CInt(txtQty.Text.Trim))
            ' MsgBox(Len(a))

            If Len(a) = 1 Then
                txtSN_end.Text = "000" & a
            ElseIf Len(a) = 2 Then
                txtSN_end.Text = "00" & a
            ElseIf Len(a) = 3 Then
                txtSN_end.Text = "0" & a
            ElseIf Len(a) = 4 Then
                txtSN_end.Text = a
            Else
                MsgBox("OUT OF SN RANGE")
            End If

        End If

    End Sub

    Private Sub createSN_List()

        txtStart_i.Text = CInt(txtSN_Start.Text)
        txtEnd_i.Text = CInt(txtSN_end.Text)

        Dim Start_SN As Integer = CInt(txtSN_Start.Text)
        Dim End_SN As Integer = CInt(txtSN_Start.Text)
        If Len(End_SN) > 6 Then
            MsgBox("The length Of max SN number Is over!!!")
            Return
        End If

        Dim i As Integer
        conn = New SqlConnection(Strconn)
        Try
            conn.Open()
            Dim cmd As New SqlCommand
            For i = txtStart_i.Text To txtEnd_i.Text
                cmd = New SqlCommand("sp_tbl_Rototec_SN_log_add", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New SqlParameter("@Date", SqlDbType.DateTime)).Value = DateTime.Now.ToString("yyyy-MM-dd 12:00:00")
                cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 7)).Value = txtWO.Text.Trim
                cmd.Parameters.Add(New SqlParameter("@PN", SqlDbType.NVarChar, 50)).Value = txtPN.Text.Trim
                cmd.Parameters.Add(New SqlParameter("@Datecode", SqlDbType.NChar, 4)).Value = txtDatecode.Text.Trim
                cmd.Parameters.Add(New SqlParameter("@Sequence", SqlDbType.Int)).Value = i
                Dim j As String = CStr(i)
                cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 23)).Value = Roto_Digi_Fixed & txtDatecode.Text.Trim & i.ToString("D6")
                cmd.ExecuteNonQuery()

            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            conn.Close()
        End Try

    End Sub

    Public Function GetCount_SN(ByVal DateCode As String, ByVal PN As String) As Integer
        Dim Retval As Integer = 0

        Dim Str_Querry As String = "Select Count(*) as count_SN " &
                                   "from Rototec_SN_log " &
                                   "where Datecode = @DateCode and PN = @PN"

        Dim dt As New System.Data.DataTable()
        Dim cn As New SqlConnection(Strconn)

        Try
            cn.Open()

            Dim cmd As New SqlCommand(Str_Querry, cn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@DateCode", SqlDbType.NChar).Value = DateCode
            cmd.Parameters.Add("@PN", SqlDbType.NVarChar).Value = PN

            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(dt)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Retval = Convert.ToInt32(dt.Rows(0)("count_SN"))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            cn.Close()
        End Try

        Return Retval
    End Function
    Public Function GetCount_SN_ByWO(ByVal WO As String) As Integer
        Dim Retval As Integer = 0

        Dim Str_Querry As String = "Select Count(*) as count_SN " &
                                   "from Rototec_SN_log " &
                                   "where WO = @WO"

        Dim dt As New System.Data.DataTable()
        Dim cn As New SqlConnection(Strconn)

        Try
            cn.Open()

            Dim cmd As New SqlCommand(Str_Querry, cn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@WO", SqlDbType.NVarChar).Value = WO

            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(dt)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Retval = Convert.ToInt32(dt.Rows(0)("count_SN"))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            cn.Close()
        End Try

        Return Retval
    End Function

    Private Sub btn_ExportExcell_Click(sender As Object, e As EventArgs) Handles btn_ExportExcell.Click
        Export_DGV_To_Excel()
    End Sub

    Public Sub Export_DGV_To_Excel()
        Using sfd As New SaveFileDialog()
            sfd.Filter = "Excel File (*.xlsx)|*.xlsx"
            sfd.FileName = "SN_Export.xlsx"

            If sfd.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim wb As New XLWorkbook()
            Dim ws = wb.Worksheets.Add("SN_Data")

            ' ===== HEADER =====
            For col As Integer = 0 To dgwSN.Columns.Count - 1
                ws.Cell(1, col + 1).Value = dgwSN.Columns(col).HeaderText
            Next

            ' ===== DATA =====
            For row As Integer = 0 To dgwSN.Rows.Count - 1
                If dgwSN.Rows(row).IsNewRow Then Continue For

                For col As Integer = 0 To dgwSN.Columns.Count - 1
                    Dim dgvCell = dgwSN.Rows(row).Cells(col)
                    Dim xlCell = ws.Cell(row + 2, col + 1)

                    If dgvCell.Value Is Nothing OrElse dgvCell.Value Is DBNull.Value Then
                        xlCell.Value = ""
                        Continue For
                    End If

                    If dgwSN.Columns(col).Name = "SN" Then
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

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        txtWO.Text = ""
        txtPN.Text = ""
        txtQty.Text = ""
        txtDatecode.Text = ""
        txtSN_Start.Text = ""
        txtSN_end.Text = ""
        txtStart_i.Text = ""
        txtEnd_i.Text = ""
        dgwSN.DataSource = Nothing
    End Sub

    Private Sub Rototec_Create_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbl_fixedDigital.Text = Roto_Digi_Fixed
        cbb_RotoModel.Items.Add("Rototec(Main)")
        cbb_RotoModel.Items.Add("Rototec")
        cbb_RotoModel.SelectedText = "Rototec(Main)"
    End Sub

    Private Sub cbb_RotoModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbb_RotoModel.SelectedIndexChanged
        If (cbb_RotoModel.SelectedIndex = 0) Then
            Roto_Digi_Fixed = "103"

        Else
            Roto_Digi_Fixed = "1QCS6490V11"
        End If
        lbl_fixedDigital.Text = Roto_Digi_Fixed

    End Sub
End Class