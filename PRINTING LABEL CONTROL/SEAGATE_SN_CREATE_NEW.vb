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

Public Class SEAGATE_SN_CREATE_NEW
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Private Sub SEAGATE_SN_CREATE_NEW_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPN.Enabled = False
        txtDatecode.Enabled = False
        txtQty.Enabled = False
        txtRev.Enabled = False
        txtSN_end.Enabled = False
        txtSN_Start.Enabled = False
        txtDate.Hide()
        txtDate.Text = DateTimePicker1.Value.ToShortDateString
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        LOGIN.Size = MinimumSize
        txti.Hide()
        txtSN.Hide()
        txtSN_Pre.Hide()
    End Sub
    Private Sub txtWO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWO.KeyPress

        If e.KeyChar = Chr(Keys.Enter) Then
            Cleartext()

            'Load thong tin WO
            LoadWO_Information()
            ' Looking history of WO 

            HISTORY_BYWO()
            ' End If
        End If
        'End If
    End Sub
    Private Sub Cleartext()
        txtPN.Clear()
        txtQty.Clear()
        txtDatecode.Clear()
        txtSN_end.Clear()
        txtSN_Start.Clear()
        txtRev.Clear()
        'txtWO.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        txtProductcode.Clear()

    End Sub
    Private Sub LoadWO_Information()
        conn = New SqlConnection(Strconn1)
        cmd = New SqlCommand("Select * from tblWOfollowUP where ORIGIN_WO='" & Me.txtWO.Text.Trim & "'", conn)
        conn.Open()
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            Me.txtQty.Text = CInt(dr("OWO_QUANTITY").ToString.Trim)
            Me.txtPN.Text = dr("MODEL_CODE").ToString.Trim
            Dim issued_date As String
            issued_date = dr("ISSUED_DATE")

            Dim yyyy As String, mmm As String, dd As Integer, month As Integer

            yyyy = Microsoft.VisualBasic.Year(issued_date) + (Microsoft.VisualBasic.Month(issued_date) >= 7)

            month = Microsoft.VisualBasic.Month(issued_date)

            mmm = Microsoft.VisualBasic.Left(MonthName(month), 3).ToUpper



            dd = Microsoft.VisualBasic.Day(issued_date)



            Dim DOM As String

            If Len(dd) <= 1 Then
                DOM = "0" & dd & mmm & yyyy
            Else
                DOM = dd & mmm & yyyy

            End If

            txtDatecode.Text = DOM

            Dim ver As String
            ver = Microsoft.VisualBasic.Mid(dr("REV_PICK_LIST").ToString.Trim, 2, 1)

            txtRev.Text = ver


        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()



    End Sub
    Private Sub HISTORY_BYWO()

        'If TextBox4.Text.Trim = "" Then
        '    MsgBox("Missing some information. Please confirm again.")

        'Else
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblSeagate_SN_Log_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            If CInt(txtQty.Text.Trim) = CInt(dr1("").ToString.Trim) Then
                MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")
                PCBA_SN_Log_Load()
            Else

                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("sptblSeagate_SN_Inf'" & Me.txtPN.Text.Trim & "'", conn)
                conn.Open()
                Dim dr As SqlDataReader
                dr = cmd.ExecuteReader
                While dr.Read
                    Dim Productcode As String

                    Productcode = CStr(dr("PRODUCTCODE").ToString.Trim)
                    txtProductcode.Text = Productcode

                End While
                dr.Close()
                cmd.Dispose()
                conn.Close()


                CreateSN_PCBA_Start_End()

                'Tao danh sach so SN cho WO
                createSN_List()

            End If
        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()



        'End If

    End Sub
    Private Sub createSN_List()

        'Load product information

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblSeagate_SN_Inf'" & Me.txtPN.Text.Trim & "'", conn)
        conn.Open()
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            Dim CPN As String, ModelNo As String, Capacity As String, Voltage As String, Current As String, Firmware As String, Productcode As String

            CPN = CStr(dr("CPN_V10").ToString.Trim)
            ModelNo = CStr(dr("MODELNO_V08").ToString.Trim)
            Capacity = CStr(dr("CAPACITY_V01").ToString.Trim)
            Voltage = CStr(dr("VOLTAGE_V50").ToString.Trim)
            Current = CStr(dr("CURRENT_V50").ToString.Trim)
            Firmware = CStr(dr("FIRMWARE_V12").ToString.Trim)

            Dim i As Integer
            For i = TextBox2.Text To TextBox3.Text
                txti.Text = i
                DectoBase()
                txtSN.Text = txtSN_Pre.Text.Trim

                '  conn = New SqlConnection(Strconn)
                Dim cmd1 As New SqlCommand

                cmd1.Connection = conn
                cmd1 = New SqlCommand("sptblSeagateSN_log_add", conn)

                cmd1.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

                cmd1.Parameters.Add(New SqlParameter("@Date", SqlDbType.DateTime)).Value = txtDate.Text.ToString.Trim
                cmd1.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 10)).Value = txtWO.Text.Trim
                cmd1.Parameters.Add(New SqlParameter("@DOM", SqlDbType.NChar, 9)).Value = txtDatecode.Text.Trim
                cmd1.Parameters.Add(New SqlParameter("@UPN", SqlDbType.NVarChar, 50)).Value = txtPN.Text.Trim
                cmd1.Parameters.Add(New SqlParameter("@Sequence", SqlDbType.Int)).Value = i
                cmd1.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 10)).Value = txtSN.Text.Trim
                cmd1.Parameters.Add(New SqlParameter("@CPN_V10", SqlDbType.NChar, 10)).Value = CPN
                cmd1.Parameters.Add(New SqlParameter("@MODELNO_V08", SqlDbType.NChar, 10)).Value = ModelNo
                cmd1.Parameters.Add(New SqlParameter("@CAPACITY_V01", SqlDbType.NChar, 10)).Value = Capacity
                cmd1.Parameters.Add(New SqlParameter("@VOLTAGE_V50", SqlDbType.NChar, 10)).Value = Voltage
                cmd1.Parameters.Add(New SqlParameter("@CURRENT_V50", SqlDbType.NChar, 10)).Value = Current
                cmd1.Parameters.Add(New SqlParameter("@FIRMWARE_V12", SqlDbType.NChar, 10)).Value = Firmware


                Try
                    conn.Open()
                    cmd1.ExecuteNonQuery()
                    conn.Close()

                Catch ex As SqlException
                    MsgBox(ex)
                    conn.Close()

                End Try
            Next

            LoadSeagateSN_log()


        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()

    End Sub
    Private Sub DectoBase()
        Dim a As Double
        a = CDbl(txti.Text.Trim)

        If a < 1148860 Then
            Dim modulo As Integer, remain As Integer
            Dim result As String

            modulo = a \ 35937

            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & modulo & "'", conn)
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader
            While dr1.Read
                result = dr1("Base_convert").ToString.Trim
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()


            remain = a - modulo * 35937

            modulo = remain \ (33 * 33)

            Dim result1 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & modulo & "'", conn)
            conn.Open()
            Dim dr2 As SqlDataReader
            dr2 = cmd.ExecuteReader
            While dr2.Read
                result1 = dr2("Base_convert").ToString.Trim
            End While
            dr2.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result1

            remain = remain - modulo * 33 * 33

            modulo = remain \ 33

            Dim result2 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & modulo & "'", conn)
            conn.Open()
            Dim dr3 As SqlDataReader
            dr3 = cmd.ExecuteReader
            While dr3.Read
                result2 = dr3("Base_convert").ToString.Trim
            End While
            dr3.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result2


            remain = remain - modulo * 33

            Dim result3 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & remain & "'", conn)
            conn.Open()
            Dim dr4 As SqlDataReader
            dr4 = cmd.ExecuteReader
            While dr4.Read
                result3 = dr4("Base_convert").ToString.Trim
            End While
            dr4.Close()
            cmd.Dispose()
            conn.Close()
            result = result + result3

            Dim fixdigit As String = "7"
            Dim cccc As String
            'If txtPN.Text.IndexOf("UBPKI04500HCNT1-IHV-STX") >= 0 Then
            '    cccc = "Y7"
            'ElseIf txtPN.Text.IndexOf("UBPKI0401T0CNT1-JHV-STX") >= 0 Then
            '    cccc = "Y8"

            'ElseIf txtPN.Text.IndexOf("UBPKI0402T0CNT1-KHV-STX") >= 0 Then
            '    cccc = "Y9"
            'End If
            cccc = txtProductcode.Text.Trim

            If Len(result) = 1 Then
                txtSN_Pre.Text = fixdigit & cccc & "0000" & result
            ElseIf Len(result) = 2 Then
                txtSN_Pre.Text = fixdigit & cccc & "000" & result
            ElseIf Len(result) = 3 Then
                txtSN_Start.Text = fixdigit & cccc & "00" & result

            ElseIf Len(result) = 4 Then
                txtSN_Pre.Text = fixdigit & cccc & "0" & result
            ElseIf Len(result) = 5 Then
                txtSN_Pre.Text = fixdigit & cccc & result
            End If

            txtSN.Text = txtSN_Pre.Text.Trim



        Else

            MsgBox("Number is too large")        ' Number is too large

        End If

    End Sub
    Private Sub PCBA_SN_Log_Load()

        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblSeagateSN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblSeagateSN_log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblSeagateSN_log"))
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
    Private Sub CreateSN_PCBA_Start_End()

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblSeagate_SN_Count'" & Me.txtPN.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()

        While dr1.Read
            Dim sequence As String, count As Integer
            sequence = (dr1("").ToString.Trim)

            If sequence = "" Then
                count = 0
            Else
                count = CInt(sequence)
            End If

            If count > 0 Then
                TextBox2.Text = (count + 1)
                TextBox3.Text = count + CInt(txtQty.Text.Trim)

            Else
                TextBox2.Text = 1
                TextBox3.Text = CInt(txtQty.Text.Trim)

            End If
            DectoBase_Start()
            '   Checksum_Start()

            DectoBase_End()
            ' Checksum_End()

        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

    End Sub
    Private Sub DectoBase_Start()
        Dim a As Double
        a = CDbl(TextBox2.Text.Trim)

        If a < 1148860 Then
            Dim modulo As Integer, remain As Integer
            Dim result As String

            modulo = a \ 35937

            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & modulo & "'", conn)
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader
            While dr1.Read
                result = dr1("Base_convert").ToString.Trim
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()


            remain = a - modulo * 35937

            modulo = remain \ (33 * 33)

            Dim result1 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & modulo & "'", conn)
            conn.Open()
            Dim dr2 As SqlDataReader
            dr2 = cmd.ExecuteReader
            While dr2.Read
                result1 = dr2("Base_convert").ToString.Trim
            End While
            dr2.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result1

            remain = remain - modulo * 33 * 33

            modulo = remain \ 33

            Dim result2 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & modulo & "'", conn)
            conn.Open()
            Dim dr3 As SqlDataReader
            dr3 = cmd.ExecuteReader
            While dr3.Read
                result2 = dr3("Base_convert").ToString.Trim
            End While
            dr3.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result2


            remain = remain - modulo * 33

            Dim result3 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & remain & "'", conn)
            conn.Open()
            Dim dr4 As SqlDataReader
            dr4 = cmd.ExecuteReader
            While dr4.Read
                result3 = dr4("Base_convert").ToString.Trim
            End While
            dr4.Close()
            cmd.Dispose()
            conn.Close()
            result = result + result3



            Dim fixdigit As String = "7"
            Dim cccc As String
            'If txtPN.Text.IndexOf("UBPKI04500HCNT1-IHV-STX") >= 0 Then
            '    cccc = "Y7"
            'ElseIf txtPN.Text.IndexOf("UBPKI0401T0CNT1-JHV-STX") >= 0 Then
            '    cccc = "Y8"

            'ElseIf txtPN.Text.IndexOf("UBPKI0402T0CNT1-KHV-STX") >= 0 Then
            '    cccc = "Y9"
            'End If
            cccc = txtProductcode.Text.Trim
            If Len(result) = 1 Then
                txtSN_Start.Text = fixdigit & cccc & "0000" & result
            ElseIf Len(result) = 2 Then
                txtSN_Start.Text = fixdigit & cccc & "000" & result
            ElseIf Len(result) = 3 Then
                txtSN_Start.Text = fixdigit & cccc & "00" & result

            ElseIf Len(result) = 4 Then
                txtSN_Start.Text = fixdigit & cccc & "0" & result
            ElseIf Len(result) = 5 Then
                txtSN_Start.Text = fixdigit & cccc & result
            End If



        Else

            MsgBox("Number is too large")  ' Number is too large

        End If

    End Sub
    Private Sub DectoBase_End()
        Dim a As Double
        a = CDbl(TextBox3.Text.Trim)

        If a < 1148860 Then
            Dim modulo As Integer, remain As Integer
            Dim result As String

            modulo = a \ 35937

            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & modulo & "'", conn)
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader
            While dr1.Read
                result = dr1("Base_convert").ToString.Trim
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()


            remain = a - modulo * 35937

            modulo = remain \ (33 * 33)

            Dim result1 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & modulo & "'", conn)
            conn.Open()
            Dim dr2 As SqlDataReader
            dr2 = cmd.ExecuteReader
            While dr2.Read
                result1 = dr2("Base_convert").ToString.Trim
            End While
            dr2.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result1

            remain = remain - modulo * 33 * 33

            modulo = remain \ 33

            Dim result2 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & modulo & "'", conn)
            conn.Open()
            Dim dr3 As SqlDataReader
            dr3 = cmd.ExecuteReader
            While dr3.Read
                result2 = dr3("Base_convert").ToString.Trim
            End While
            dr3.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result2


            remain = remain - modulo * 33

            Dim result3 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE_Seagate'" & remain & "'", conn)
            conn.Open()
            Dim dr4 As SqlDataReader
            dr4 = cmd.ExecuteReader
            While dr4.Read
                result3 = dr4("Base_convert").ToString.Trim
            End While
            dr4.Close()
            cmd.Dispose()
            conn.Close()
            result = result + result3

            Dim fixdigit As String = "7"
            Dim cccc As String
            'If txtPN.Text.IndexOf("UBPKI04500HCNT1-IHV-STX") >= 0 Then
            '    cccc = "Y7"
            'ElseIf txtPN.Text.IndexOf("UBPKI0401T0CNT1-JHV-STX") >= 0 Then
            '    cccc = "Y8"

            'ElseIf txtPN.Text.IndexOf("UBPKI0402T0CNT1-KHV-STX") >= 0 Then
            '    cccc = "Y9"
            'End If
            cccc = txtProductcode.Text.Trim
            If Len(result) = 1 Then
                txtSN_end.Text = fixdigit & cccc & "0000" & result
            ElseIf Len(result) = 2 Then
                txtSN_end.Text = fixdigit & cccc & "000" & result
            ElseIf Len(result) = 3 Then
                txtSN_end.Text = fixdigit & cccc & "00" & result

            ElseIf Len(result) = 4 Then
                txtSN_end.Text = fixdigit & cccc & "0" & result
            ElseIf Len(result) = 5 Then
                txtSN_end.Text = fixdigit & cccc & result
            End If

        Else

            MsgBox("Number is too large")  ' Number is too large

        End If

    End Sub
    Private Sub LoadSeagateSN_log()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblSeagateSN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblSeagateSN_log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblSeagateSN_log"))
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

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        PCBA_SN_Export()

    End Sub
    Private Sub PCBA_SN_Export()
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
                dr1(j) = Trim(DataGridView2.Rows(i).Cells(j).Value)
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

            rng = wSheet.Columns("B:F")
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

    Private Sub tbnNew_Click(sender As Object, e As EventArgs) Handles tbnNew.Click
        DataGridView2.DataSource = ""
        DataGridView2.Columns.Clear()
        Cleartext()
        txtWO.Clear()
    End Sub
End Class