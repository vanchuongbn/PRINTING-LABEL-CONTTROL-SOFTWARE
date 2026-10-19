Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports System.Data
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports Excel = Microsoft.Office.Interop.Excel
Imports System
Imports System.Globalization
Imports System.Threading
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Diagnostics

Public Class frmNEW_SERIAL_PRINTING
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim obj As ListItem
    Dim dr As SqlDataReader
    Private Sub frmNEW_SERIAL_PRINTING_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPrintdate.Text = DateTimePicker1.Value.ToShortDateString
        txtPrintdate.Enabled = False
        txtQty_Printed.Enabled = False
        txtPN.Hide()
        txtProductName.Hide()
        txtCPN.Hide()
        txtDC.Hide()
        txtAgileREV.Hide()
        txtEngREV.Hide()
        txtCapacity.Hide()

        txtWO_qty.Enabled = False
        txtStart_SN.Enabled = False
        txtEnd_SN.Enabled = False
        txtStart_SN_Print.Enabled = False
        txtEnd_SN_Print.Enabled = False
        txtPrint_by.Enabled = False
        txtPrint_by.Text = LOGIN.txtUsername.Text.Trim
        TextBox1.Hide()
        TextBox2.Hide()
        TextBox3.Hide()
        TextBox4.Hide()
        TextBox10.Hide()
        TextBox11.Hide()
        TextBox9.Hide()
        If LOGIN.txtUsername.Text.Trim.StartsWith("mark.pham") = True Then
            Update.Visible = True
        Else
            Update.Visible = False
        End If

    End Sub
    Private Sub frmCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        conn = New SqlConnection(Strconn2)
        conn.Open()
        Dim sql = New SqlCommand("sptblCustomer_Load", conn)
        'Dim dataset As DataSet = New DataSet
        Dim da As New SqlDataAdapter
        Dim dt As System.Data.DataTable
        Dim dataset As DataSet = New DataSet
        da.SelectCommand = sql

        da.Fill(dataset, "Customername")
        dt = dataset.Tables("Customername")
        For x = 0 To dt.Rows.Count - 1
            Me.cbbCustomer.Items.Add(dt.Rows(x).Item("Customername"))
        Next
        conn.Close()

        'Dim dr As SqlDataReader

        'dr = Me.PopulateForm_11("sptblCustomer_Load", "")
        'Me.cbbCustomer.Items.Clear()
        'While dr.Read
        '    obj = New ListItem(CStr(dr.Item("Customername").ToString.Trim), dr.Item("Customername").ToString().Trim)
        '    Me.cbbCustomer.Items.Add(obj)
        'End While
        'dr.Close()


    End Sub
    Private Function PopulateForm_11(ByVal Ten_Thu_tuc As String, ByVal ID As String) As SqlDataReader
        Dim SqlDr As SqlDataReader
        Try

            Dim Sqlcmd As SqlCommand = New SqlCommand(" " & Ten_Thu_tuc & "   '" & ID & "'", Me.conn)
            SqlDr = Sqlcmd.ExecuteReader
        Catch ex As SqlException
            MsgBox(ex.Message)
        End Try
        Return SqlDr
        SqlDr.Close()
        conn.Close()
    End Function
    Private Sub LoadWO_Information()
        If cbbCustomer.Text.Trim = "" Then
            MsgBox("HÃY CHỌN TÊN KHÁCH HÀNG")
        Else

            conn = New SqlConnection(Strconn1)
            cmd = New SqlCommand("Select * from tblWOfollowUP where ORIGIN_WO='" & Me.txtWO.Text.Trim & "'", conn)
            conn.Open()
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            While dr.Read
                Me.txtWO_qty.Text = CInt(dr("OWO_QUANTITY").ToString.Trim)
                Me.txtPN.Text = dr("MODEL_CODE").ToString.Trim
                Dim YY As String
                YY = dr("ISSUED_DATE")
                Dim cvdate As String
                If Microsoft.VisualBasic.Right(YY, 11) = "12:00:00 AM" Then
                    cvdate = Microsoft.VisualBasic.Mid(YY, YY.IndexOf("12:00:00 AM") - 2, 2)

                Else
                    cvdate = Microsoft.VisualBasic.Right(YY, 2)

                End If
                Dim week As String

                week = Replace(Microsoft.VisualBasic.Right((dr("WORK_WEEK").ToString.Trim), 2), "_", 0)

                Dim ww As String
                ww = cvdate & week


                If cbbCustomer.Text.Trim = "EFI" Then
                    Dim mm As String, a As String

                    a = Microsoft.VisualBasic.Left(YY, YY.IndexOf("/"))

                    If Len(a) = 1 Then
                        mm = "0" & a
                    Else
                        mm = a
                    End If

                    Dim datecode As String
                    datecode = mm & "/" & cvdate
                    txtDC.Text = datecode
                Else
                    txtDC.Text = ww
                End If


                If cbbCustomer.Text.Trim = "FADU" Then


                    ww = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(YY, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)

                    Dim d As String
                    Dim dd As String

                    dd = DateTime.Parse(YY).DayOfWeek()
                    d = dd + 1

                    Dim datecode As String
                    datecode = cvdate & ww & d
                    txtDC.Text = datecode
                End If

                txtMicron_desc.Text = dr("MODEL_NAME").ToString.Trim

                Dim Rev As String = dr("REV_PICK_LIST").ToString.Trim

                If cbbCustomer.Text.Trim <> "PURE STORAGE" Then

                    If cbbCustomer.Text.Trim <> "OEM" Or cbbCustomer.Text.Trim <> "CISCO" Then
                        If cbbCustomer.Text.Trim = "EFI" Then

                            If Rev.IndexOf(".") = 0 Then

                                Me.txtAgileREV.Text = Microsoft.VisualBasic.Mid(Rev, 2, 1)
                            ElseIf Rev.IndexOf(".") = 1 Then

                                Me.txtAgileREV.Text = Microsoft.VisualBasic.Left(Rev, 1)
                            Else
                                Me.txtAgileREV.Text = Microsoft.VisualBasic.Left(Rev, 1)
                            End If
                        Else
                            If Rev.IndexOf(".") = 0 Then

                                Me.txtAgileREV.Text = Microsoft.VisualBasic.Mid(Rev, 2, 1)
                            ElseIf Rev.IndexOf(".") = 1 Then

                                Me.txtAgileREV.Text = Microsoft.VisualBasic.Left(Rev, 1)
                            Else
                                Me.txtAgileREV.Text = Microsoft.VisualBasic.Left(Rev, 2)
                            End If
                        End If




                    Else


                            If Rev.IndexOf(".") = 1 Then

                            Me.txtAgileREV.Text = Microsoft.VisualBasic.Left(Rev, 1)
                        Else
                            Me.txtAgileREV.Text = Microsoft.VisualBasic.Mid(Rev, 2, 1)
                        End If
                    End If

                Else

                    If Rev.IndexOf(".") = 0 Then

                        Me.txtAgileREV.Text = Microsoft.VisualBasic.Mid(Rev, 2, 2)

                    ElseIf Rev.IndexOf(".") = 1 Then

                        Me.txtAgileREV.Text = Microsoft.VisualBasic.Left(Rev, 1)

                    Else
                        Me.txtAgileREV.Text = Microsoft.VisualBasic.Left(Rev, 2)

                    End If


                End If

            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()

            If cbbCustomer.Text.Trim <> "MICRON" And cbbCustomer.Text.Trim <> "LONGSYS" Then

                'If cbbCustomer.Text.Trim = "SEAGATE" Then


                'Else
                conn = New SqlConnection(Strconn2)
                    cmd = New SqlCommand("Select * from tblLabelInformation where MODEL='" & Trim(Replace(Me.txtPN.Text.Trim, ".X.X", "")) & "'", conn)
                    conn.Open()
                    Dim dr3 As SqlDataReader
                    dr3 = cmd.ExecuteReader
                    While dr3.Read
                        Me.txtProductName.Text = dr3("ModelName").ToString.Trim
                        Me.txtEngREV.Text = dr3("ENGBOMRevision").ToString.Trim

                    End While
                    dr3.Close()
                    cmd.Dispose()
                    conn.Close()

                    conn = New SqlConnection(Strconn2)
                    cmd = New SqlCommand("Select * from tblModelname where UPN='" & Trim(Replace(Me.txtPN.Text.Trim, ".X.X", "")) & "'", conn)
                    conn.Open()
                    Dim dr4 As SqlDataReader
                    dr4 = cmd.ExecuteReader
                    While dr4.Read
                        Me.txtCPN.Text = dr4("CPN").ToString.Trim
                        Me.txtCapacity.Text = dr4("Capacity").ToString.Trim
                    End While
                    dr4.Close()
                    cmd.Dispose()
                    conn.Close()
                'End If




            End If


            If cbbCustomer.Text.Trim <> "MICRON" And cbbCustomer.Text.Trim <> "LONGSYS" Then
                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("spGetSN_Start'" & Me.txtWO.Text.Trim & "'", conn)
                conn.Open()
                Dim dr1 As SqlDataReader
                dr1 = cmd.ExecuteReader
                While dr1.Read
                    Me.txtStart_SN.Text = dr1("SN").ToString.Trim
                End While
                dr1.Close()
                cmd.Dispose()
                conn.Close()

                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("spGetSN_End'" & Me.txtWO.Text.Trim & "'", conn)
                conn.Open()
                Dim dr2 As SqlDataReader
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    Me.txtEnd_SN.Text = dr2("SN").ToString.Trim
                End While
                dr2.Close()
                cmd.Dispose()
                conn.Close()
            ElseIf cbbCustomer.Text.Trim = "MICRON" Or cbbCustomer.Text.Trim = "LONGSYS" Then
                txtStart_SN.Text = txtWO.Text.Trim & "-00001"

                If Len(txtWO_qty.Text.Trim) = 1 Then
                    txtEnd_SN.Text = txtWO.Text.Trim & "-0000" & txtWO_qty.Text.Trim
                ElseIf Len(txtWO_qty.Text.Trim) = 2 Then
                    txtEnd_SN.Text = txtWO.Text.Trim & "-000" & txtWO_qty.Text.Trim
                ElseIf Len(txtWO_qty.Text.Trim) = 3 Then
                    txtEnd_SN.Text = txtWO.Text.Trim & "-00" & txtWO_qty.Text.Trim
                ElseIf Len(txtWO_qty.Text.Trim) = 4 Then
                    txtEnd_SN.Text = txtWO.Text.Trim & "-0" & txtWO_qty.Text.Trim
                ElseIf Len(txtWO_qty.Text.Trim) >= 5 Then
                    txtEnd_SN.Text = txtWO.Text.Trim & "-" & txtWO_qty.Text.Trim
                End If

                Dim conn2 As SqlConnection = New SqlConnection(Strconn)
                sqladap = New SqlDataAdapter("Select SN from tblMicronSN where SN='" & Me.txtEnd_SN.Text.Trim & "'and WO='" & txtWO.Text.Trim & "'", conn2)
                Dim tb As New System.Data.DataTable()

                Try
                    conn2.Open()
                    sqladap.Fill(tb)
                Catch ex As Exception
                    ' MsgBox(ex)
                    conn2.Close()
                End Try
                If (tb.Rows.Count > 0) Then
                    conn2.Close()
                Else
                    Create_Micron_SN()
                End If

            Else

            End If
            LoadLabelPicture()
        End If

    End Sub

    Private Sub LoadLabelPicture()
        conn = New SqlConnection(Strconn2)
        Dim cmd = New SqlCommand("Select Picture from tblLabelInformation where Model='" & Replace(Me.txtPN.Text.Trim, ".X.X", "") & "' and Type = '" & "Serial" & "'", conn)
        conn.Open()
        Dim dr As SqlDataReader = cmd.ExecuteReader()
        If dr.HasRows Then
            dr.Read()

            Dim data As Byte() = DirectCast(dr("Picture"), Byte())
            Dim ms As New MemoryStream(data)
            PictureBox1.Image = Image.FromStream(ms)
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        End If

        conn.Close()

    End Sub
    Private Sub txtWO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWO.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            LoadWO_Information()
            LoadPrinted_SN()
            If cbbCustomer.Text = "TINTRI" Then
                loadTINTRI_WWN_SN()
                'ElseIf cbbCustomer.Text.Trim = "SEAGATE" Then

                '    LoadSeagateSN()


            End If
            txtPrint_Qty.Focus()
        End If
    End Sub
    Private Sub loadTINTRI_WWN_SN()
        'Try
        '    conn = New SqlConnection(Strconn2)
        '    Me.StrSql = "spCreateTINTRI_SN_WWN'" & Me.txtWO.Text.Trim & "'"
        '    Dim cmd As New SqlCommand(Me.StrSql, conn)
        '    da = New SqlDataAdapter(cmd)
        '    Dim cmb As New SqlCommandBuilder(da)
        '    conn.Close()
        'Catch ex As SqlClient.SqlException
        '    MsgBox(ex.Message & StrSql)
        'End Try

        Try
            conn = New SqlConnection(Strconn2)
            Me.StrSql = "spLoadTINTRI_SN_WWN'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet


            'Tao them cot tren datagridview -khong lay du lieu tu database
            ds = New DataSet
            da.Fill(ds, "[QUALITYDATASYSTEM].[dbo].[MyTempTable1]")
            'ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("MacAdd")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("Product#")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("ProductName")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("CPN")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("AgileREV")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("EngREV")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("DateCode")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("Capacity")
            If cbbCustomer.Text = "PURE STORAGE" Then
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("2Dbarcode")
            End If

            conn.Close()

            Dim Dtv As DataView = New DataView(ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]"))
            With DataGridView2
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            End With

            ' Do du lieu vao cac cot moi them tren datagridview voi data lay tu textbox
            Dim i As Integer
            For i = 0 To ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Rows.Count - 1
                '   DataGridView2.Item("MacAdd", i).Value = ""
                DataGridView2.Item("Product#", i).Value = Replace(Me.txtPN.Text.Trim, ".X.X", "")
                DataGridView2.Item("ProductName", i).Value = Me.txtProductName.Text.Trim
                DataGridView2.Item("CPN", i).Value = Me.txtCPN.Text.Trim
                DataGridView2.Item("AgileREV", i).Value = Me.txtAgileREV.Text.Trim
                DataGridView2.Item("EngREV", i).Value = Me.txtEngREV.Text.Trim
                DataGridView2.Item("DateCode", i).Value = Me.txtDC.Text.Trim
                DataGridView2.Item("Capacity", i).Value = Me.txtCapacity.Text.Trim
                If cbbCustomer.Text = "PURE STORAGE" Then
                    DataGridView2.Item("2Dbarcode", i).Value = DataGridView2.Item("SN", i).Value & ">" & txtPN.Text.Trim & txtAgileREV.Text.Trim & ">" & DataGridView2.Item("AliasSN", i).Value
                End If

            Next
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try

    End Sub
    Private Sub Create_Micron_SN()
        TextBox1.Text = CInt(Microsoft.VisualBasic.Right(txtStart_SN.Text.Trim, 5))
        TextBox2.Text = CInt(Microsoft.VisualBasic.Right(txtEnd_SN.Text.Trim, 5))

        For i As Integer = CInt(TextBox1.Text.Trim) To CInt(TextBox2.Text.Trim)
            TextBox3.Text = i

            conn = New SqlConnection(Strconn)
            Dim cmd As New SqlCommand

            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO tblMicronSN (WO,SN_id,SN,Model,Description) VALUES(@WO,@SN_id,@SN,@Model,@Description)"
            cmd.Parameters.AddWithValue("@WO", txtWO.Text.Trim)
            cmd.Parameters.AddWithValue("@Model", txtPN.Text.Trim)
            Dim micron_desc As String
            Dim a As String = txtMicron_desc.Text.Trim
            micron_desc = Microsoft.VisualBasic.Mid(a, a.IndexOf("PCBA") + 5, Len(a) - a.IndexOf("PCBA") + 5)
            cmd.Parameters.AddWithValue("@Description", micron_desc)

            If Len(TextBox3.Text.Trim) = 1 Then
                cmd.Parameters.AddWithValue("@SN_id", i)
                cmd.Parameters.AddWithValue("@SN", txtWO.Text.Trim & "-0000" & i)
            ElseIf Len(TextBox3.Text.Trim) = 2 Then
                cmd.Parameters.AddWithValue("@SN_id", i)
                cmd.Parameters.AddWithValue("@SN", txtWO.Text.Trim & "-000" & i)
            ElseIf Len(TextBox3.Text.Trim) = 3 Then
                cmd.Parameters.AddWithValue("@SN_id", i)
                cmd.Parameters.AddWithValue("@SN", txtWO.Text.Trim & "-00" & i)
            ElseIf Len(TextBox3.Text.Trim) = 4 Then
                cmd.Parameters.AddWithValue("@SN_id", i)
                cmd.Parameters.AddWithValue("@SN", txtWO.Text.Trim & "-0" & i)
            ElseIf Len(TextBox3.Text.Trim) >= 5 Then
                cmd.Parameters.AddWithValue("@SN_id", i)
                cmd.Parameters.AddWithValue("@SN", txtWO.Text.Trim & i)
            End If

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()

            Catch ex As SqlException
                conn.Close()

            End Try
        Next

    End Sub

    Private Sub LoadPrinted_SN()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "spGetPrinted_SN'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblSNPrint")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblSNPrint"))
            With DataGridView1
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblSNPrinted_Count'" & Me.txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            txtQty_Printed.Text = dr1("").ToString.Trim
        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

    End Sub
    Private Sub txtPrint_Qty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrint_Qty.KeyPress

        If e.KeyChar = Chr(Keys.Enter) Then

            txtEnd_SN_Print.Text = ""
            txtStart_SN_Print.Text = ""

            If CInt(txtPrint_Qty.Text.Trim) <= CInt(txtQty_Printed.Text.Trim) Then
                MsgBox("SỐ TEM CẦN IN ĐẾN KHÔNG HỢP LỆ. HÃY NHẬP LẠI")
            ElseIf CInt(txtPrint_Qty.Text.Trim) > CInt(txtWO_qty.Text.Trim) Then
                MsgBox("SỐ TEM CẦN IN ĐẾN KHÔNG ĐƯỢC LỚN HƠN SỐ LƯỢNG CỦA WO")
            Else
                If cbbCustomer.Text.Trim <> "MICRON" And cbbCustomer.Text.Trim <> "LONGSYS" Then
                    If cbbCustomer.Text.Trim = "TINTRI" Then
                        ListupSNrange_Tintri()
                    Else
                        conn = New SqlConnection(Strconn)
                        cmd = New SqlCommand("spGetSNRange_Print_Start'" & Me.txtWO.Text.Trim & "'", conn)
                        conn.Open()
                        Dim dr1 As SqlDataReader
                        dr1 = cmd.ExecuteReader
                        While dr1.Read
                            Me.txtStart_SN_Print.Text = dr1("SN").ToString.Trim
                        End While
                        dr1.Close()
                        cmd.Dispose()
                        conn.Close()

                        conn = New SqlConnection(Strconn)
                        cmd = New SqlCommand("spGetSNRange_Print_End'" & Me.txtWO.Text.Trim & "'" & "," & "'" & Me.txtPrint_Qty.Text.Trim & "'", conn)
                        conn.Open()
                        Dim dr2 As SqlDataReader
                        dr2 = cmd.ExecuteReader
                        While dr2.Read
                            Me.txtEnd_SN_Print.Text = dr2("SN").ToString.Trim
                        End While
                        dr2.Close()
                        cmd.Dispose()
                        conn.Close()


                        '-----Get SNrange to print------------

                        If cbbCustomer.Text = "PURE STORAGE" And (txtPN.Text.StartsWith("8") = True Or txtPN.Text.StartsWith("0") = True) Then


                            GetBaggerSN_print()

                        Else
                            GetSN_print()
                        End If




                    End If


                ElseIf cbbCustomer.Text.Trim = "MICRON" Or cbbCustomer.Text.Trim = "LONGSYS" Then
                    LoadMicronSN()

                End If
            End If

        End If
    End Sub
    Private Sub GetBaggerSN_print()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "spGetSNRange_Print_Bagger'" & Me.txtWO.Text.Trim & "'" & "," & "'" & Me.txtStart_SN_Print.Text.Trim & "'" & "," & "'" & Me.txtEnd_SN_Print.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)

            'Tao them cot tren datagridview -khong lay du lieu tu database
            ds = New DataSet
            da.Fill(ds, "[QUALITYDATASYSTEM].[dbo].[SN_WO]")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("Product#")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("ProductName")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("CPN")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("AgileREV")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("EngREV")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("DateCode")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("Capacity")
            If cbbCustomer.Text = "PURE STORAGE" Then
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("Mac_Convert")
            End If

            conn.Close()

            Dim Dtv As DataView = New DataView(ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]"))
            With DataGridView2
                .DataSource = Dtv
                DataGridView2.Columns(0).Selected = True
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            End With

            ' Do du lieu vao cac cot moi them tren datagridview voi data lay tu textbox
            Dim i As Integer
            For i = 0 To ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Rows.Count - 1
                DataGridView2.Item("Product#", i).Value = Replace(Me.txtPN.Text.Trim, ".X.X", "")
                DataGridView2.Item("ProductName", i).Value = Me.txtProductName.Text.Trim
                DataGridView2.Item("CPN", i).Value = Me.txtCPN.Text.Trim
                DataGridView2.Item("AgileREV", i).Value = Me.txtAgileREV.Text.Trim
                DataGridView2.Item("EngREV", i).Value = Me.txtEngREV.Text.Trim
                DataGridView2.Item("DateCode", i).Value = Me.txtDC.Text.Trim
                DataGridView2.Item("Capacity", i).Value = Me.txtCapacity.Text.Trim
                If cbbCustomer.Text = "PURE STORAGE" And (txtPN.Text.StartsWith("84") = True Or txtPN.Text.StartsWith("0") = True) Then

                    If Microsoft.VisualBasic.Left(txtStart_SN.Text.Trim, 5) = "PFMUH" Then
                        Dim strs As String
                        Dim insStr As String
                        Dim strRes As String
                        Dim strRes1 As String
                        Dim strRes2 As String
                        Dim strRes3 As String
                        Dim strRes4 As String
                        Dim strRes5 As String
                        Dim strRes6 As String
                        ' Dim strRes7 As String
                        Dim index As Integer = 0

                        strs = DataGridView2.Item("MacAdd", i).Value
                        insStr = ":"
                        strRes = strs.Insert(2, insStr)
                        strRes1 = strRes.Insert(5, insStr)
                        strRes2 = strRes1.Insert(8, insStr)
                        strRes3 = strRes2.Insert(11, insStr)
                        strRes4 = strRes3.Insert(14, insStr)
                        strRes5 = strRes4.Insert(17, insStr)
                        strRes6 = strRes5.Insert(20, insStr)
                        DataGridView2.Item("Mac_Convert", i).Value = strRes6
                    Else
                        DataGridView2.Item("Mac_Convert", i).Value = ""
                    End If


                End If
            Next

        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try
    End Sub
    Private Sub GetSN_print()

        'MsgBox(txtEnd_SN_Print.Text)
        If cbbCustomer.Text.Trim = "AGIGA" Then
            Dim EC As String, FW As String, SPD_BOM_ver As String



            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("sptblAGIGASN_Inf'" & Me.txtWO.Text.Trim & "'", conn)
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader
            While dr1.Read
                TextBox9.Text = dr1("EC").ToString.Trim
                TextBox10.Text = dr1("SPI_FW_ver").ToString.Trim
                TextBox11.Text = dr1("SPD_BOM_ver").ToString.Trim
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()


        ElseIf cbbCustomer.Text = "SEAGATE" Then

            '  Dim EC As String, FW As String, SPD_BOM_ver As String



            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("sptblSeagate_Inf'" & Me.txtWO.Text.Trim & "'", conn)
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader
            While dr1.Read

                txtPN.Text = dr1("UPN").ToString.Trim
                txtDC.Text = dr1("DOM").ToString.Trim
                txtProductName.Text = dr1("MODELNO_V08").ToString.Trim
                txtCPN.Text = dr1("CPN_V10").ToString.Trim
                txtCapacity.Text = dr1("CAPACITY_V01").ToString.Trim
                TextBox9.Text = dr1("VOLTAGE_V50").ToString.Trim
                TextBox10.Text = dr1("CURRENT_V50").ToString.Trim
                TextBox11.Text = dr1("FIRMWARE_V12").ToString.Trim
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()

        End If

        If cbbCustomer.Text = "SEAGATE" Then
            Try
                conn = New SqlConnection(Strconn)
                Me.StrSql = "spGetSNRange_Print'" & Me.txtWO.Text.Trim & "'" & "," & "'" & Me.txtStart_SN_Print.Text.Trim & "'" & "," & "'" & Me.txtEnd_SN_Print.Text.Trim & "'"
                Dim cmd As New SqlCommand(Me.StrSql, conn)
                da = New SqlDataAdapter(cmd)
                Dim cmb As New SqlCommandBuilder(da)

                'Tao them cot tren datagridview -khong lay du lieu tu database
                ds = New DataSet
                da.Fill(ds, "[QUALITYDATASYSTEM].[dbo].[SN_WO]")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("UPN")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("CPN_V10")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("MODELNO_V08")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("CAPACITY_V01")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("VOLTAGE_V50")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("CURRENT_V50")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("FIRMWARE_V12")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("DOM_V64")


                conn.Close()

                Dim Dtv As DataView = New DataView(ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]"))
                With DataGridView2
                    .DataSource = Dtv
                    DataGridView2.Columns(0).Selected = True
                    .Rows(0).Selected = False
                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                End With

                ' Do du lieu vao cac cot moi them tren datagridview voi data lay tu textbox
                Dim i As Integer
                For i = 0 To ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Rows.Count - 1


                    DataGridView2.Item("UPN", i).Value = Me.txtPN.Text.Trim
                    DataGridView2.Item("CPN_V10", i).Value = Me.txtCPN.Text.Trim
                    DataGridView2.Item("MODELNO_V08", i).Value = Me.txtProductName.Text.Trim
                    DataGridView2.Item("CAPACITY_V01", i).Value = Me.txtCapacity.Text.Trim
                    DataGridView2.Item("VOLTAGE_V50", i).Value = Me.TextBox9.Text.Trim
                    DataGridView2.Item("CURRENT_V50", i).Value = Me.TextBox10.Text.Trim
                    DataGridView2.Item("FIRMWARE_V12", i).Value = Me.TextBox11.Text.Trim
                    DataGridView2.Item("DOM_V64", i).Value = Me.txtDC.Text.Trim

                Next

            Catch ex As SqlClient.SqlException
                MsgBox(ex.Message & StrSql)
            End Try

        Else



            Try
                conn = New SqlConnection(Strconn)
                Me.StrSql = "spGetSNRange_Print'" & Me.txtWO.Text.Trim & "'" & "," & "'" & Me.txtStart_SN_Print.Text.Trim & "'" & "," & "'" & Me.txtEnd_SN_Print.Text.Trim & "'"
                Dim cmd As New SqlCommand(Me.StrSql, conn)
                da = New SqlDataAdapter(cmd)
                Dim cmb As New SqlCommandBuilder(da)

                'Tao them cot tren datagridview -khong lay du lieu tu database
                ds = New DataSet
                da.Fill(ds, "[QUALITYDATASYSTEM].[dbo].[SN_WO]")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("Product#")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("ProductName")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("CPN")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("AgileREV")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("EngREV")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("DateCode")
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("Capacity")
                If cbbCustomer.Text = "PURE STORAGE" Then
                    ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("Mac_Convert")
                ElseIf cbbCustomer.Text = "FADU" Then
                    ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("2Dbarcode")
                    ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("Current")
                ElseIf cbbCustomer.Text = "AGIGA" Then
                    ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("2Dbarcode")
                    ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("SNvisible")
                    ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("FW")
                    ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("EC")
                    ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Columns.Add("SPD_BOM_ver")
                End If

                conn.Close()

                Dim Dtv As DataView = New DataView(ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]"))
                With DataGridView2
                    .DataSource = Dtv
                    DataGridView2.Columns(0).Selected = True
                    .Rows(0).Selected = False
                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                End With

                ' Do du lieu vao cac cot moi them tren datagridview voi data lay tu textbox
                Dim i As Integer
                For i = 0 To ds.Tables("[QUALITYDATASYSTEM].[dbo].[SN_WO]").Rows.Count - 1
                    If cbbCustomer.Text.Trim = "FADU" Then

                        DataGridView2.Item("Product#", i).Value = Replace(Me.txtPN.Text.Trim, "DCSSD-", "DCSSD ")

                    Else
                        DataGridView2.Item("Product#", i).Value = Replace(Me.txtPN.Text.Trim, ".X.X", "")
                    End If

                    DataGridView2.Item("ProductName", i).Value = Me.txtProductName.Text.Trim
                    DataGridView2.Item("CPN", i).Value = Me.txtCPN.Text.Trim


                    If cbbCustomer.Text = "AGIGA" Then
                        DataGridView2.Item("AgileREV", i).Value = Microsoft.VisualBasic.Left(Me.txtAgileREV.Text.Trim, 2)

                    Else
                        If Me.txtAgileREV.Text.Trim.IndexOf(".") = 0 Then
                            DataGridView2.Item("AgileREV", i).Value = Microsoft.VisualBasic.Mid(Me.txtAgileREV.Text.Trim, 2, 1)
                        ElseIf Me.txtAgileREV.Text.Trim.IndexOf(".") > 0 Then
                            DataGridView2.Item("AgileREV", i).Value = Microsoft.VisualBasic.Left(Me.txtAgileREV.Text.Trim, 1)
                        Else
                            DataGridView2.Item("AgileREV", i).Value = Microsoft.VisualBasic.Left(Me.txtAgileREV.Text.Trim, 2)
                        End If

                    End If



                    DataGridView2.Item("EngREV", i).Value = Me.txtEngREV.Text.Trim
                    DataGridView2.Item("DateCode", i).Value = Me.txtDC.Text.Trim
                    DataGridView2.Item("Capacity", i).Value = Me.txtCapacity.Text.Trim

                    If cbbCustomer.Text = "PURE STORAGE" And (txtPN.Text.StartsWith("84") = True Or txtPN.Text.StartsWith("0") = True) Then
                        Dim strs As String
                        Dim insStr As String
                        Dim strRes As String
                        Dim strRes1 As String
                        Dim strRes2 As String
                        Dim strRes3 As String
                        Dim strRes4 As String
                        Dim strRes5 As String
                        Dim strRes6 As String
                        ' Dim strRes7 As String
                        Dim index As Integer = 0

                        strs = DataGridView2.Item("MacAdd", i).Value
                        insStr = ":"
                        strRes = strs.Insert(2, insStr)
                        strRes1 = strRes.Insert(5, insStr)
                        strRes2 = strRes1.Insert(8, insStr)
                        strRes3 = strRes2.Insert(11, insStr)
                        strRes4 = strRes3.Insert(14, insStr)
                        strRes5 = strRes4.Insert(17, insStr)
                        strRes6 = strRes5.Insert(20, insStr)
                        DataGridView2.Item("Mac_Convert", i).Value = strRes6

                    ElseIf cbbCustomer.Text = "FADU" Then

                        Dim SN2d As String
                        SN2d = "FADU " & (DataGridView2.Rows(i).Cells(4).Value) & ">" & (DataGridView2.Rows(i).Cells(1).Value)

                        DataGridView2.Item("2Dbarcode", i).Value = SN2d
                        If txtPN.Text = "DCSSD-FA1024A-M1M-000A" Then

                            DataGridView2.Item("Current", i).Value = "3.0A"
                        Else

                            DataGridView2.Item("Current", i).Value = "4.5A"
                        End If

                    ElseIf cbbCustomer.Text = "AGIGA" Then


                        DataGridView2.Item("FW", i).Value = TextBox10.Text.Trim
                        DataGridView2.Item("EC", i).Value = TextBox9.Text.Trim

                        DataGridView2.Item("SPD_BOM_ver", i).Value = TextBox11.Text.Trim

                        Dim SN2d As String, SNvisible As String, SN As String
                        If txtPN.Text.IndexOf("-032") > 0 Then

                            SN2d = "(L)32GB 1Rx4 NN4-3200AA-RZZZ-" & TextBox11.Text.Trim & (DataGridView2.Rows(i).Cells(1).Value)

                        ElseIf txtPN.Text.IndexOf("-016") > 0 Then
                            SN2d = "(L)16GB 1Rx4 NN4-3200AA-RZZZ-" & TextBox11.Text.Trim & (DataGridView2.Rows(i).Cells(1).Value)

                        End If



                        If txtPN.Text.IndexOf("AGIGA88") >= 0 Then
                            DataGridView2.Item("2Dbarcode", i).Value = SN2d
                            SNvisible = Microsoft.VisualBasic.Mid(SN2d, 35, 18)
                            DataGridView2.Item("SNvisible", i).Value = SNvisible

                        ElseIf txtPN.Text.IndexOf("AGIGA98") >= 0 Then
                            SN = DataGridView2.Item("SN", i).Value
                            SNvisible = Microsoft.VisualBasic.Mid(SN, 17, 14)
                            DataGridView2.Item("SNvisible", i).Value = SNvisible
                        End If



                    End If
                Next

            Catch ex As SqlClient.SqlException
                MsgBox(ex.Message & StrSql)
            End Try
        End If
    End Sub
    Private Sub LoadMicronSN()
        Dim a As Integer
        Dim b As Integer
        If CInt(txtQty_Printed.Text.Trim) = 0 Then
            a = 1
            txtStart_SN_Print.Text = txtWO.Text.Trim & "-0000" & a
            b = txtPrint_Qty.Text.Trim

            If Len(txtPrint_Qty.Text.Trim) = 1 Then
                txtEnd_SN_Print.Text = txtWO.Text.Trim & "-0000" & b
            ElseIf Len(txtPrint_Qty.Text.Trim) = 2 Then
                txtEnd_SN_Print.Text = txtWO.Text.Trim & "-000" & b
            ElseIf Len(txtPrint_Qty.Text.Trim) = 3 Then
                txtEnd_SN_Print.Text = txtWO.Text.Trim & "-00" & b
            ElseIf Len(txtPrint_Qty.Text.Trim) = 4 Then
                txtEnd_SN_Print.Text = txtWO.Text.Trim & "-0" & b
            ElseIf Len(txtPrint_Qty.Text.Trim) >= 5 Then
                txtEnd_SN_Print.Text = txtWO.Text.Trim & "-" & b
            End If

            ListupSNrange()

        Else

            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetSNRange_Print_Start_Micron'" & Me.txtWO.Text.Trim & "'", conn)
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader
            While dr1.Read

                Dim Micron_Max_SN As String

                Micron_Max_SN = dr1("SN").ToString.Trim

                a = CInt(Microsoft.VisualBasic.Right(Micron_Max_SN, 5)) + 1

                b = txtPrint_Qty.Text.Trim

                If a > CInt(txtPrint_Qty.Text.Trim) Then
                    MsgBox("SỐ TEM CẦN IN ĐẾN KHÔNG HỢP LỆ. HÃY NHẬP LẠI")
                ElseIf CInt(txtPrint_Qty.Text) > CInt(txtWO_qty.Text) Then
                    MsgBox("SỐ TEM CẦN IN ĐẾN KHÔNG ĐƯỢC LỚN HƠN SỐ LƯỢNG CỦA WO")
                Else
                    TextBox4.Text = a
                    If Len(TextBox4.Text) = 1 Then
                        txtStart_SN_Print.Text = txtWO.Text.Trim & "-0000" & a
                    ElseIf Len(TextBox4.Text) = 2 Then
                        txtStart_SN_Print.Text = txtWO.Text.Trim & "-000" & a
                    ElseIf Len(TextBox4.Text) = 3 Then
                        txtStart_SN_Print.Text = txtWO.Text.Trim & "-00" & a
                    ElseIf Len(TextBox4.Text) = 4 Then
                        txtStart_SN_Print.Text = txtWO.Text.Trim & "-0" & a
                    ElseIf Len(TextBox4.Text) >= 5 Then
                        txtStart_SN_Print.Text = txtWO.Text.Trim & "-" & a
                    End If


                    If Len(txtPrint_Qty.Text.Trim) = 1 Then
                        txtEnd_SN_Print.Text = txtWO.Text.Trim & "-0000" & b
                    ElseIf Len(txtPrint_Qty.Text.Trim) = 2 Then
                        txtEnd_SN_Print.Text = txtWO.Text.Trim & "-000" & b
                    ElseIf Len(txtPrint_Qty.Text.Trim) = 3 Then
                        txtEnd_SN_Print.Text = txtWO.Text.Trim & "-00" & b
                    ElseIf Len(txtPrint_Qty.Text.Trim) = 4 Then
                        txtEnd_SN_Print.Text = txtWO.Text.Trim & "-0" & b
                    ElseIf Len(txtPrint_Qty.Text.Trim) >= 5 Then
                        txtEnd_SN_Print.Text = txtWO.Text.Trim & "-" & b
                    End If

                End If

                ListupSNrange()
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()
        End If

    End Sub

    Private Sub ListupSNrange()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "spGetSNRange_Print_Micron'" & Me.txtWO.Text.Trim & "'" & "," & "'" & Me.txtStart_SN_Print.Text.Trim & "'" & "," & "'" & Me.txtEnd_SN_Print.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "[dbo].[tblMicronSN]")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("[dbo].[tblMicronSN]"))

            With DataGridView2
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            End With


        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try
    End Sub
    Private Sub ListupSNrange_Tintri()
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("spGetSNRange_Print_Start_Tintri'" & Me.txtWO.Text.Trim & "'", conn)
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader
        While dr1.Read
            Me.txtStart_SN_Print.Text = dr1("SN").ToString.Trim
        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("spGetSNRange_Print_End_Tintri'" & Me.txtWO.Text.Trim & "'" & "," & "'" & Me.txtPrint_Qty.Text.Trim & "'", conn)
        conn.Open()
        Dim dr2 As SqlDataReader
        dr2 = cmd.ExecuteReader
        While dr2.Read
            Me.txtEnd_SN_Print.Text = dr2("SN").ToString.Trim
        End While
        dr2.Close()
        cmd.Dispose()
        conn.Close()


        '-----Get SNrange to print------------
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "spGetSNRange_Print_Tintri'" & Me.txtWO.Text.Trim & "'" & "," & "'" & Me.txtStart_SN_Print.Text.Trim & "'" & "," & "'" & Me.txtEnd_SN_Print.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)

            'Tao them cot tren datagridview -khong lay du lieu tu database
            ds = New DataSet
            da.Fill(ds, "[QUALITYDATASYSTEM].[dbo].[MyTempTable1]")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("Product#")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("ProductName")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("CPN")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("AgileREV")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("EngREV")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("DateCode")
            ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("Capacity")
            If cbbCustomer.Text = "PURE STORAGE" Then
                ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Columns.Add("2Dbarcode")
            End If

            conn.Close()

            Dim Dtv As DataView = New DataView(ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]"))
            With DataGridView2
                .DataSource = Dtv
                DataGridView2.Columns(0).Selected = True
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            End With

            ' Do du lieu vao cac cot moi them tren datagridview voi data lay tu textbox
            Dim i As Integer
            For i = 0 To ds.Tables("[QUALITYDATASYSTEM].[dbo].[MyTempTable1]").Rows.Count - 1
                DataGridView2.Item("Product#", i).Value = Replace(Me.txtPN.Text.Trim, ".X.X", "")
                DataGridView2.Item("ProductName", i).Value = Me.txtProductName.Text.Trim
                DataGridView2.Item("CPN", i).Value = Me.txtCPN.Text.Trim
                DataGridView2.Item("AgileREV", i).Value = Me.txtAgileREV.Text.Trim
                DataGridView2.Item("EngREV", i).Value = Me.txtEngREV.Text.Trim
                DataGridView2.Item("DateCode", i).Value = Me.txtDC.Text.Trim
                DataGridView2.Item("Capacity", i).Value = Me.txtCapacity.Text.Trim
                If cbbCustomer.Text = "PURE STORAGE" Then
                    DataGridView2.Item("2Dbarcode", i).Value = DataGridView2.Item("SN", i).Value & ">" & txtPN.Text.Trim & txtAgileREV.Text.Trim & ">" & DataGridView2.Item("AliasSN", i).Value
                End If
            Next

        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try
    End Sub
    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click
        Excelfile_Export()
        SNRange_Print_Record()

    End Sub
    Private Sub Excelfile_Export()
        If ((Me.DataGridView2.Columns.Count = 0) Or (Me.DataGridView2.Rows.Count = 0)) Then
            Exit Sub
        End If

        'Creating dataset to export
        Dim dset As New DataSet
        'add table to dataset
        dset.Tables.Add()
        'add column to that table
        For i As Integer = 0 To Me.DataGridView2.ColumnCount - 1
            dset.Tables(0).Columns.Add(Me.DataGridView2.Columns(i).HeaderText)
        Next
        'add rows to the table
        Dim dr1 As DataRow
        For i As Integer = 0 To DataGridView2.RowCount - 1
            dr1 = dset.Tables(0).NewRow
            For j As Integer = 0 To DataGridView2.Columns.Count - 1
                dr1(j) = (DataGridView2.Rows(i).Cells(j).Value)

                If cbbCustomer.Text = "EFI" Then
                    Dim snprint As String = (DataGridView2.Rows(i).Cells(1).Value)

                    dr1(2) = Microsoft.VisualBasic.Right(snprint, 9)
                ElseIf cbbCustomer.Text = "PURE STORAGE" And txtPN.Text.Trim.IndexOf("31-") >= 0 Then
                    Dim pn As String = (DataGridView2.Rows(i).Cells(1).Value)
                    dr1(2) = Microsoft.VisualBasic.Left(pn, 13)
                    'ElseIf cbbCustomer.Text.ToUpper = "FADU" Then
                    '    Dim SN2d As String, vol As String
                    '    SN2d = "FADU " & (DataGridView2.Rows(i).Cells(1).Value) & ">" & (DataGridView2.Rows(i).Cells(1).Value)
                    '    dr1(2) = SN2d

                ElseIf cbbCustomer.Text = "PURE STORAGE" And txtPN.Text.Trim.IndexOf("02-0114") >= 0 Then
                    Dim pn As String = (DataGridView2.Rows(i).Cells(1).Value)
                    dr1(2) = Microsoft.VisualBasic.Left(pn, 13)


                End If


            Next
            dset.Tables(0).Rows.Add(dr1)
        Next


        Dim excel As New Excel.Application()
        Dim wBook As Excel.Workbook
        Dim wSheet As Excel.Worksheet


        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        With wSheet
            Dim rng As Range

            rng = wSheet.Columns("B:P")
            rng.Cells.NumberFormat = "@"  ' Format dang text cho mot vung tren excel
        End With

        'excel.Cells.NumberFormat = "@" ' Format dang text cho tat ca cac cell



        Dim dt As System.Data.DataTable = dset.Tables(0)
        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        For Each dc In dt.Columns
            colIndex = colIndex + 1
            excel.Cells(1, colIndex) = dc.ColumnName

        Next

        For Each dr In dt.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In dt.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
            Next
        Next

        wSheet.Columns.AutoFit()



        Dim dialog As SaveFileDialog = New SaveFileDialog

        Dim strFileName As String = "File.xls"
        dialog.FileName = strFileName
        dialog.ShowDialog()
        Dim blnFileOpen As Boolean = False
        Try
            Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
            fileTemp.Close()
        Catch ex As Exception
            blnFileOpen = False
        End Try

        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If

        wBook.SaveAs(strFileName)
        excel.Workbooks.Open(strFileName)
        excel.Visible = True
    End Sub

    Private Sub SNRange_Print_Record()
        If cbbCustomer.Text.Trim <> "MICRON" And cbbCustomer.Text.Trim <> "LONGSYS" Then
            If cbbCustomer.Text.Trim = "SEAGATE" Then

                Try

                    For Each row As DataGridViewRow In DataGridView2.Rows
                        Dim conn As SqlConnection = New SqlConnection(Strconn)

                        Using cmd As New SqlCommand("INSERT INTO tblSNPrint VALUES(@PrintDate, @Printby, @WO, @SN, @Mac_1, @Mac_2_WWN, @Product#, @ProductName, @CPN, @AgileREV, @EngREV, @DateCode, @Capacity)", conn)
                            cmd.Parameters.AddWithValue("@PrintDate", Me.txtPrintdate.Text.Trim)
                            cmd.Parameters.AddWithValue("@Printby", Me.txtPrint_by.Text.Trim)
                            cmd.Parameters.AddWithValue("@WO", row.Cells("WO").Value)
                            cmd.Parameters.AddWithValue("@SN", row.Cells("SN").Value)
                            cmd.Parameters.AddWithValue("@Mac_1", "")
                            cmd.Parameters.AddWithValue("@Mac_2_WWN", "")
                            cmd.Parameters.AddWithValue("@Product#", row.Cells("UPN").Value)
                            cmd.Parameters.AddWithValue("@ProductName", row.Cells("MODELNO_V08").Value)
                            cmd.Parameters.AddWithValue("@CPN", row.Cells("CPN_V10").Value)
                            cmd.Parameters.AddWithValue("@AgileREV", "")
                            cmd.Parameters.AddWithValue("@EngREV", "")
                            cmd.Parameters.AddWithValue("@DateCode", row.Cells("DOM_V64").Value)
                            cmd.Parameters.AddWithValue("@Capacity", row.Cells("CAPACITY_V01").Value)


                            conn.Open()
                            cmd.ExecuteNonQuery()
                            conn.Close()
                        End Using

                    Next
                    conn.Close()
                Catch ex As Exception
                    '   MessageBox.Show("Error :" & ex.ToString())
                Finally
                    conn.Close()
                End Try

                MessageBox.Show("Records inserted.")
            Else

                Try

                    For Each row As DataGridViewRow In DataGridView2.Rows
                        Dim conn As SqlConnection = New SqlConnection(Strconn)

                        Using cmd As New SqlCommand("INSERT INTO tblSNPrint VALUES(@PrintDate, @Printby, @WO, @SN, @Mac_1, @Mac_2_WWN, @Product#, @ProductName, @CPN, @AgileREV, @EngREV, @DateCode, @Capacity)", conn)
                            cmd.Parameters.AddWithValue("@PrintDate", Me.txtPrintdate.Text.Trim)
                            cmd.Parameters.AddWithValue("@Printby", Me.txtPrint_by.Text.Trim)
                            cmd.Parameters.AddWithValue("@WO", row.Cells("WO").Value)
                            cmd.Parameters.AddWithValue("@SN", row.Cells("SN").Value)
                            cmd.Parameters.AddWithValue("@Mac_1", row.Cells("MacAdd").Value)
                            cmd.Parameters.AddWithValue("@Mac_2_WWN", row.Cells("AliasSN").Value)
                            cmd.Parameters.AddWithValue("@Product#", row.Cells("Product#").Value)
                            cmd.Parameters.AddWithValue("@ProductName", row.Cells("ProductName").Value)
                            cmd.Parameters.AddWithValue("@CPN", row.Cells("CPN").Value)
                            cmd.Parameters.AddWithValue("@AgileREV", row.Cells("AgileREV").Value)
                            cmd.Parameters.AddWithValue("@EngREV", row.Cells("EngREV").Value)
                            cmd.Parameters.AddWithValue("@DateCode", row.Cells("DateCode").Value)
                            cmd.Parameters.AddWithValue("@Capacity", row.Cells("Capacity").Value)


                            conn.Open()
                            cmd.ExecuteNonQuery()
                            conn.Close()
                        End Using

                    Next
                    conn.Close()
                Catch ex As Exception
                    ' MessageBox.Show("Error :" & ex.ToString())
                Finally
                    conn.Close()
                End Try

                MessageBox.Show("Records inserted.")

            End If

        ElseIf cbbCustomer.Text.Trim = "MICRON" Or cbbCustomer.Text.Trim = "LONGSYS" Then
            Try

                For Each row As DataGridViewRow In DataGridView2.Rows
                    Dim conn As SqlConnection = New SqlConnection(Strconn)

                    Using cmd As New SqlCommand("INSERT INTO tblSNPrint VALUES(@PrintDate, @Printby, @WO, @SN, @Mac_1, @Mac_2_WWN, @Product#, @ProductName, @CPN, @AgileREV, @EngREV, @DateCode, @Capacity)", conn)
                        cmd.Parameters.AddWithValue("@PrintDate", Me.txtPrintdate.Text.Trim)
                        cmd.Parameters.AddWithValue("@Printby", Me.txtPrint_by.Text.Trim)
                        cmd.Parameters.AddWithValue("@WO", row.Cells("WO").Value)
                        cmd.Parameters.AddWithValue("@SN", row.Cells("SN").Value)
                        cmd.Parameters.AddWithValue("@Mac_1", "")
                        cmd.Parameters.AddWithValue("@Mac_2_WWN", "")
                        cmd.Parameters.AddWithValue("@Product#", row.Cells("Model").Value)
                        cmd.Parameters.AddWithValue("@ProductName", "")
                        cmd.Parameters.AddWithValue("@CPN", "")
                        cmd.Parameters.AddWithValue("@AgileREV", "")
                        cmd.Parameters.AddWithValue("@EngREV", "")
                        cmd.Parameters.AddWithValue("@DateCode", "")
                        cmd.Parameters.AddWithValue("@Capacity", "")

                        conn.Open()
                        cmd.ExecuteNonQuery()
                        conn.Close()

                    End Using

                Next
                conn.Close()
            Catch ex As Exception
                ' MessageBox.Show("Error :" & ex.ToString())
            Finally
                conn.Close()
            End Try

            MessageBox.Show("Records inserted.")
        End If
        LoadPrinted_SN()
    End Sub

    Private Sub EXITToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EXITToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox6.Text.Trim <> "" Or TextBox7.Text.Trim <> "" Then
            DeletebySNRange()
        Else
            deletebyWO()

        End If

    End Sub
    Private Sub deletebyWO()
        conn = New SqlConnection(Strconn) ' connect to Database
        cmd = New SqlCommand("sptbltblSNPrint_DeleteWO", conn) ' Ket noi vao datatable cua Store Procedure
        cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

        cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 7)).Value = Me.TextBox5.Text.Trim

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            MsgBox("HOÀN THÀNH XÓA")
        Catch ex As SqlException
            conn.Close()

            MsgBox("KHÔNG THỂ XÓA")
            Exit Sub
        End Try
        LoadPrinted_SN()
    End Sub
    Private Sub DeletebySNRange()
        conn = New SqlConnection(Strconn) ' connect to Database
        cmd = New SqlCommand("sptbltblSNPrint_DeleteSNRange'" & Me.TextBox5.Text.Trim & "'" & "," & "'" & Me.TextBox6.Text.Trim & "'" & "," & "'" & Me.TextBox7.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

        cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 6)).Value = Me.TextBox5.Text.Trim

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            MsgBox("HOÀN THÀNH XÓA")
        Catch ex As SqlException
            conn.Close()

            MsgBox(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblSN_RE_Print_Update_SKIP'" & Me.TextBox8.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

        'cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 6)).Value = Me.TextBox5.Text.Trim

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            MsgBox("CẬP NHẬT THÀNH CÔNG")
        Catch ex As SqlException
            conn.Close()
            MsgBox(ex.Message)
            'MsgBox("KHÔNG THỂ CẬP NHẬT")
            Exit Sub
        End Try
    End Sub
End Class