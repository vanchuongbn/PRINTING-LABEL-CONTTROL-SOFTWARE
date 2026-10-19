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

Public Class FADU_SN_CREATE
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Private Sub FADU_SN_CREATE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            Dim yy As String
            If Microsoft.VisualBasic.Right(issued_date, 11) = "12:00:00 AM" Then
                yy = Microsoft.VisualBasic.Mid(issued_date, issued_date.IndexOf("12:00:00 AM") - 2, 2)

            Else
                yy = Microsoft.VisualBasic.Right(issued_date, 2)

            End If
            Dim ww As String

            issued_date.IndexOf("/")

            ' mm = Microsoft.VisualBasic.Left(issued_date, issued_date.IndexOf("/"))
            ww = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(issued_date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)

            Dim d As String
            Dim dd As String

            dd = DateTime.Parse(issued_date).DayOfWeek()
            d = dd + 1

            Dim datecode As String
            datecode = yy & ww & d
            txtDatecode.Text = datecode

            Dim ver As String
            ver = Microsoft.VisualBasic.Mid(dr("REV_PICK_LIST").ToString.Trim, 2, 1)

            txtRev.Text = ver


        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()

        'Load name of product

        conn = New SqlConnection(Strconn2)
        cmd = New SqlCommand("Select ModelName from tblLabelinformation where Model='" & Me.txtPN.Text.Trim & "'", conn)
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader
        While dr1.Read
            Me.TextBox4.Text = dr1("ModelName").ToString.Trim

        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

    End Sub
    Private Sub HISTORY_BYWO()

        If TextBox4.Text.Trim = "" Then
            MsgBox("Missing some information. Please confirm again.")

        Else
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("sptblFADU_SN_Log_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader()
            While dr1.Read
                If CInt(txtQty.Text.Trim) = CInt(dr1("").ToString.Trim) Then
                    MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")
                    PCBA_SN_Log_Load()
                Else
                    CreateSN_PCBA_Start_End()

                    'Tao danh sach so SN cho WO
                    createSN_List()

                End If
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()



        End If

    End Sub
    Private Sub createSN_List()

        Dim fixdigit As String = "FV1"
        Dim cccc As String

        If txtPN.Text.Trim.IndexOf("T1M") >= 0 Then
            If txtPN.Text.IndexOf("1024") >= 0 Then
                cccc = "B000"
            ElseIf txtPN.Text.IndexOf("2048") >= 0 Then
                cccc = "B001"

            ElseIf txtPN.Text.IndexOf("4096") >= 0 Then
                cccc = "B002"
            End If
        ElseIf txtPN.Text.Trim.IndexOf("M1M") >= 0 Then

            If txtPN.Text.IndexOf("1024") >= 0 Then
                cccc = "B003"
            ElseIf txtPN.Text.IndexOf("2048") >= 0 Then
                cccc = "B004"

            ElseIf txtPN.Text.IndexOf("4096") >= 0 Then
                cccc = "B005"
            End If

        End If


        Dim i As Integer
        For i = TextBox2.Text To TextBox3.Text
            txti.text = i
            DectoBase34()
            txtSN_Pre.Text = fixdigit & txtDatecode.Text.Trim & txtSN.Text.Trim & cccc & "A"
            Checksum()

            conn = New SqlConnection(Strconn)
            Dim cmd As New SqlCommand

            cmd.Connection = conn
            cmd = New SqlCommand("sptblFaduSN_log_add", conn)

            cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

            cmd.Parameters.Add(New SqlParameter("@Date", SqlDbType.DateTime)).Value = txtDate.Text.ToString.Trim
            cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 7)).Value = txtWO.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@Datecode", SqlDbType.NChar, 5)).Value = txtDatecode.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@PN", SqlDbType.NChar, 22)).Value = txtPN.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@Sequence", SqlDbType.Int)).Value = i
            cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 18)).Value = txtSN.Text.Trim


            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()

            Catch ex As SqlException
                MsgBox(ex)
                conn.Close()

            End Try
        Next

        LoadFaduSN_log()

    End Sub
    Private Sub DectoBase34()
        Dim a As Double
        a = CDbl(txti.Text.Trim)

        If a < 1336336 Then
            Dim modulo As Integer, remain As Integer
            Dim result As String

            modulo = a \ 39304

            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & modulo & "'", conn)
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader
            While dr1.Read
                result = dr1("Base34_convert").ToString.Trim
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()


            remain = a - modulo * 39304

            modulo = remain \ (34 * 34)

            Dim result1 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & modulo & "'", conn)
            conn.Open()
            Dim dr2 As SqlDataReader
            dr2 = cmd.ExecuteReader
            While dr2.Read
                result1 = dr2("Base34_convert").ToString.Trim
            End While
            dr2.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result1

            remain = remain - modulo * 34 * 34

            modulo = remain \ 34

            Dim result2 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & modulo & "'", conn)
            conn.Open()
            Dim dr3 As SqlDataReader
            dr3 = cmd.ExecuteReader
            While dr3.Read
                result2 = dr3("Base34_convert").ToString.Trim
            End While
            dr3.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result2


            remain = remain - modulo * 34

            Dim result3 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & remain & "'", conn)
            conn.Open()
            Dim dr4 As SqlDataReader
            dr4 = cmd.ExecuteReader
            While dr4.Read
                result3 = dr4("Base34_convert").ToString.Trim
            End While
            dr4.Close()
            cmd.Dispose()
            conn.Close()
            result = result + result3

            txtSN.Text = result

        Else

            MsgBox("Number is too large")        ' Number is too large

        End If

    End Sub
    Private Sub Checksum()
        Dim temp As Integer
        Dim result As String
        Dim i, j As Integer
        Dim step2, step3 As Integer
        step2 = 0
        step3 = 0
        Dim a As String = txtSN_Pre.Text.ToUpper

        If Len(a) <> 17 Then
            MsgBox("Length of Pre_SN is wrong__" & Len(a))
        Else

            For i = 1 To 17
                result = StrConv(Mid(a, i, 1), vbUpperCase)
                ' For j = 0 To 33
                conn = New SqlConnection(Strconn)
                    cmd = New SqlCommand("spGetBASE_1'" & result & "'", conn)
                    conn.Open()
                    Dim dr1 As SqlDataReader
                    dr1 = cmd.ExecuteReader
                    While dr1.Read
                        temp = dr1("Decimalnumber").ToString.Trim
                    End While
                    dr1.Close()
                    cmd.Dispose()
                    conn.Close()

                    'If result = base34table(j) Then
                    '    temp = j
                    'Exit For
                    'End If

                    ' Next j

                    If (i Mod 2 = 1) Then
                    step2 = step2 + temp
                Else
                    step3 = step3 + temp
                End If

            Next i

            temp = step2 * 3 + step3
            temp = temp Mod 34
            If temp <> 0 Then
                temp = 34 - temp
            End If

            'result = base34table(temp)

            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & temp & "'", conn)
            conn.Open()
            Dim dr2 As SqlDataReader
            dr2 = cmd.ExecuteReader
            While dr2.Read
                result = dr2("Base34_convert").ToString.Trim
            End While
            dr2.Close()
            cmd.Dispose()
            conn.Close()

            txtSN.Text = txtSN_Pre.Text & result

        End If
    End Sub
    Private Sub PCBA_SN_Log_Load()

        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblFaduSN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblFaduSN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblFaduSN_Log"))
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
        cmd = New SqlCommand("sptblFadu_SN_Count", conn) ' Ket noi vao datatable cua Store Procedure
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
            DectoBase34_Start()
            '   Checksum_Start()

            DectoBase34_End()
            ' Checksum_End()

        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

    End Sub
    Private Sub DectoBase34_Start()
        Dim a As Double
        a = CDbl(TextBox2.Text.Trim)

        If a < 1336336 Then
            Dim modulo As Integer, remain As Integer
            Dim result As String

            modulo = a \ 39304

            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & modulo & "'", conn)
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader
            While dr1.Read
                result = dr1("Base34_convert").ToString.Trim
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()


            remain = a - modulo * 39304

            modulo = remain \ (34 * 34)

            Dim result1 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & modulo & "'", conn)
            conn.Open()
            Dim dr2 As SqlDataReader
            dr2 = cmd.ExecuteReader
            While dr2.Read
                result1 = dr2("Base34_convert").ToString.Trim
            End While
            dr2.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result1

            remain = remain - modulo * 34 * 34

            modulo = remain \ 34

            Dim result2 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & modulo & "'", conn)
            conn.Open()
            Dim dr3 As SqlDataReader
            dr3 = cmd.ExecuteReader
            While dr3.Read
                result2 = dr3("Base34_convert").ToString.Trim
            End While
            dr3.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result2


            remain = remain - modulo * 34

            Dim result3 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & remain & "'", conn)
            conn.Open()
            Dim dr4 As SqlDataReader
            dr4 = cmd.ExecuteReader
            While dr4.Read
                result3 = dr4("Base34_convert").ToString.Trim
            End While
            dr4.Close()
            cmd.Dispose()
            conn.Close()
            result = result + result3



            Dim fixdigit As String = "FV1"
            Dim cccc As String
            If txtPN.Text.IndexOf("1024") >= 0 Then
                cccc = "B000"
            ElseIf txtPN.Text.IndexOf("2048") >= 0 Then
                cccc = "B001"

            ElseIf txtPN.Text.IndexOf("4096") >= 0 Then
                cccc = "B002"
            End If

            txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & result & cccc & "A"

        Else

            MsgBox("Number is too large")  ' Number is too large

        End If

    End Sub
    Private Sub DectoBase34_End()
        Dim a As Double
        a = CDbl(TextBox3.Text.Trim)

        If a < 1336336 Then
            Dim modulo As Integer, remain As Integer
            Dim result As String

            modulo = a \ 39304

            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & modulo & "'", conn)
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader
            While dr1.Read
                result = dr1("Base34_convert").ToString.Trim
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()


            remain = a - modulo * 39304

            modulo = remain \ (34 * 34)

            Dim result1 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & modulo & "'", conn)
            conn.Open()
            Dim dr2 As SqlDataReader
            dr2 = cmd.ExecuteReader
            While dr2.Read
                result1 = dr2("Base34_convert").ToString.Trim
            End While
            dr2.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result1

            remain = remain - modulo * 34 * 34

            modulo = remain \ 34

            Dim result2 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & modulo & "'", conn)
            conn.Open()
            Dim dr3 As SqlDataReader
            dr3 = cmd.ExecuteReader
            While dr3.Read
                result2 = dr3("Base34_convert").ToString.Trim
            End While
            dr3.Close()
            cmd.Dispose()
            conn.Close()

            result = result + result2


            remain = remain - modulo * 34

            Dim result3 As String
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetBASE'" & remain & "'", conn)
            conn.Open()
            Dim dr4 As SqlDataReader
            dr4 = cmd.ExecuteReader
            While dr4.Read
                result3 = dr4("Base34_convert").ToString.Trim
            End While
            dr4.Close()
            cmd.Dispose()
            conn.Close()
            result = result + result3


            Dim fixdigit As String = "FV1"
            Dim cccc As String
            If txtPN.Text.IndexOf("1024") >= 0 Then
                cccc = "B000"
            ElseIf txtPN.Text.IndexOf("2048") >= 0 Then
                cccc = "B001"

            ElseIf txtPN.Text.IndexOf("4096") >= 0 Then
                cccc = "B002"
            End If

            txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & result & cccc & "A"
        Else

            MsgBox("Number is too large")  ' Number is too large

        End If

    End Sub
    Private Sub LoadFaduSN_log()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblFaduSN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblFaduSN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblFaduSN_Log"))
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

            rng = wSheet.Columns("B:C")
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