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
Public Class LUCID_SN_CREATE
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Private Sub LUCID_SN_CREATE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPN.Enabled = False
        txtDatecode.Enabled = False
        txtQty.Enabled = False

        txtSN_end.Enabled = False
        txtSN_Start.Enabled = False
        txtDate.Hide()
        txtDate.Text = DateTimePicker1.Value.ToShortDateString
        txt_start_i.Visible = False
        txt_end_i.Visible = False

        txtBBPN1.Enabled = False

        txtBBPN2.Enabled = False
        txtPCBAPN.Enabled = False

        LOGIN.Size = MinimumSize

        GroupBox4.Visible = False


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
   
    Private Sub LoadWO_Information()
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("spGetWO_infor'" & Me.txtWO.Text.Trim & "'", conn)
        conn.Open()
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            Me.txtQty.Text = CInt(dr("OWO_QUANTITY").ToString.Trim)
            Me.txtPN.Text = dr("MODEL_CODE").ToString.Trim
            Dim issued_date As String
            issued_date = DateTimePicker1.Value.ToShortDateString

            Dim yyyy As String, yy As String

            yyyy = Microsoft.VisualBasic.Year(issued_date)

            yy = Microsoft.VisualBasic.Right(yyyy, 2)

            Dim ddd As String

            ddd = DateTime.Parse(Today()).DayOfYear()

            Dim datecode As String

            If Len(ddd) = 1 Then
                datecode = yy & "00" & ddd
            ElseIf Len(ddd) = 2 Then
                datecode = yy & "0" & ddd
            Else
                datecode = yy & ddd
            End If

            txtDatecode.Text = datecode

        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()


    End Sub
    Private Sub HISTORY_BYWO()

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblLUCID_SN_Log_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            If CInt(txtQty.Text.Trim) = CInt(dr1("").ToString.Trim) Then
                MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")
                PCBA_SN_Log_Load()
            Else
                txtNeedQty.Focus()

            End If

            txtCreatedQty.Text = CInt(dr1("").ToString.Trim)

        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()


    End Sub
    Private Sub PCBA_SN_Log_Load()

        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblLUCID_SN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblLUCID_SN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Log"))
            With DataGridView1
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try

    End Sub

    Private Sub tbnCreateSN_Click(sender As Object, e As EventArgs) Handles btnCreateSN.Click

        If Me.txtPN.Text.Trim = "" Then
            MessageBox.Show("Vui lòng nhập Wo và ấn enter để lấy thông tin wo trước khi ấn tạo!")
            Exit Sub
        End If

        If GroupBox1.Visible = True Then
            CreateSN_Start_End()
            createSN_List()
        End If



        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblLUCID_SN_Log_Load_1'" & Me.txtWO.Text.Trim & "','" & CInt(txt_start_i.Text.Trim) & "','" & CInt(txt_end_i.Text.Trim) & "','" & Format(Me.DateTimePicker1.Value, "yyyy-MM-dd") & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblLUCID_SN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Log"))
            With DataGridView1
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try

        Me.txtPN.Text = ""

    End Sub

   
    Private Sub CreateSN_Start_End()

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblLUCID_SN_Count'" & txtDate.Text & "','" & txtPN.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            Dim count As Integer = CInt(dr1("").ToString.Trim)
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

                Dim d As String = CStr(count + CInt(txtNeedQty.Text.Trim))
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

                Dim a As String = CStr(count + CInt(txtNeedQty.Text.Trim))
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

        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

    End Sub
    Private Sub createSN_List()

        txt_start_i.Text = CInt(txtSN_Start.Text)
        txt_end_i.Text = CInt(txtSN_end.Text)


        'create SN

        Dim fixdigit As String = "113866"

        Dim i As Integer
        For i = txt_start_i.Text To txt_end_i.Text

            conn = New SqlConnection(Strconn)
            Dim cmd As New SqlCommand

            cmd.Connection = conn
            cmd = New SqlCommand("sptblLUCID_SN_Log_add", conn)

            cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

            cmd.Parameters.Add(New SqlParameter("@Date", SqlDbType.DateTime)).Value = txtDate.Text.ToString.Trim
            cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 7)).Value = txtWO.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@PN", SqlDbType.NChar, 20)).Value = txtPN.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@Sequence", SqlDbType.Int)).Value = i
            cmd.Parameters.Add(New SqlParameter("@MarkingUsed", SqlDbType.NChar, 10)).Value = ""

            Dim j As String = CStr(i)

            If Len(j) = 1 Then
                cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 50)).Value = txtPN.Text.Trim & ":S" & txtDatecode.Text.Trim & fixdigit & "000" & i
            ElseIf Len(j) = 2 Then

                cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 50)).Value = txtPN.Text.Trim & ":S" & txtDatecode.Text.Trim & fixdigit & "00" & i
            ElseIf Len(j) = 3 Then

                cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 50)).Value = txtPN.Text.Trim & ":S" & txtDatecode.Text.Trim & fixdigit & "0" & i
            ElseIf Len(j) = 4 Then

                cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 50)).Value = txtPN.Text.Trim & ":S" & txtDatecode.Text.Trim & fixdigit & i


            Else
                MsgBox("The length Of SN number Is over!!!")
            End If

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()

            Catch ex As SqlException
                'MsgBox(ex)
                conn.Close()

            End Try
        Next
     
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        txtWO.Clear()
        txtPN.Clear()

        Cleartext()
        'Cleartext_1()
        'Cleartext_2()

    End Sub
    Private Sub Cleartext()
        ' txtWO.Clear()
        txtPN.Clear()
        txtQty.Clear()
        txtDatecode.Clear()
        txtSN_end.Clear()
        txtSN_Start.Clear()
        txt_start_i.Clear()
        txt_end_i.Clear()
        DataGridView1.DataSource = ""

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        SN_Export_1()

    End Sub
    Private Sub SN_Export_1()
        If (Me.DataGridView1.Columns.Count = 0) Then
            Exit Sub
        End If

        'Creating dataset to export
        Dim dset As New DataSet
        'add table to dataset
        dset.Tables.Add()
        'add column to that table
        If GroupBox1.Visible = True Then


            For i As Integer = 0 To Me.DataGridView1.ColumnCount - 1
                dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(i).HeaderText)
            Next


            Dim dr1 As DataRow
            For i As Integer = 0 To DataGridView1.RowCount - 1
                dr1 = dset.Tables(0).NewRow
                For j As Integer = 0 To DataGridView1.Columns.Count - 1
                    dr1(j) = Trim(DataGridView1.Rows(i).Cells(j).Value)
                Next

                dset.Tables(0).Rows.Add(dr1)
            Next

            Dim excel As New Excel.Application()
            Dim wBook1 As Excel.Workbook

            Dim wSheet1 As Excel.Worksheet


            wBook1 = excel.Workbooks.Add()
            wSheet1 = wBook1.Sheets("Sheet1")

            With wSheet1
                Dim rng As Range, rng1 As Range, rng2 As Range

                ' Format dang text cho mot vung tren excel

                rng = wSheet1.Columns("B:E")
                rng.Cells.NumberFormat = "@"

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

            wSheet1.Columns.AutoFit()


            Dim dialog As SaveFileDialog = New SaveFileDialog

            Dim strFileName As String = txtWO.Text.Trim & "_" & txtDatecode.Text.Trim & "_" & txtNeedQty.Text.Trim

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

            wBook1.SaveAs(strFileName)
            excel.Workbooks.Open(strFileName)
            excel.Visible = True


        End If
    End Sub
    Private Sub txtNeedQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNeedQty.KeyPress
        'Check quantity
        If e.KeyChar = Chr(Keys.Enter) Then

            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("sptblLUCID_SN_Log_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader()
            While dr1.Read
                If CInt(txtQty.Text.Trim) < CInt(dr1("").ToString.Trim) + CInt(txtNeedQty.Text.Trim) Then
                    MsgBox("THE REQUEST SN QUANTITY IS BIGGER THAN WO QUANTITY. PLEASE CHECK AGAIN")
                    PCBA_SN_Log_Load()
                Else
                    ' Create Start_End SN
                    MsgBox("Pleae click to the 'CREATE' button")
                End If
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()
        End If

    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click

        Check_link()

        Load_PN()
    End Sub
    Private Sub Check_link()
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblLUCID_SN_linked_model_Count'" & txtPCBAPN.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            If CInt(dr1("").ToString.Trim) = 1 Then
                '  MsgBox("PLEASE INPUT THE WO OF LINKED MODEL TO THE BELOW LINK_WO THEN PRESSS ENTER")
                txtBBPN1.Visible = False
                txtBBWO1.Visible = True
                txtPCBA_WO.Focus()

            ElseIf CInt(dr1("").ToString.Trim) = 2 Then
                '  MsgBox("PLEASE INPUT THE WO OF LINKED MODEL TO THE BELOW LINK_WO THEN PRESS ENNTER")
                txtBBPN1.Visible = False
                txtBBWO1.Visible = True
                txtBBPN2.Visible = False
                txtBBWO2.Visible = True
                txtPCBA_WO.Focus()

            End If
        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

    End Sub

    Private Sub Load_PN()
        conn = New SqlConnection(Strconn1)
        cmd = New SqlCommand("Select * from tblWOfollowUP where ORIGIN_WO='" & Me.txtPCBA_WO.Text.Trim & "'", conn)
        conn.Open()
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read

            Me.txtPCBAPN.Text = dr("MODEL_CODE").ToString.Trim

        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()

        conn = New SqlConnection(Strconn1)
        cmd = New SqlCommand("Select * from tblWOfollowUP where ORIGIN_WO='" & Me.txtBBWO1.Text.Trim & "'", conn)
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader
        While dr1.Read

            Me.txtBBPN1.Text = dr1("MODEL_CODE").ToString.Trim

        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

        conn = New SqlConnection(Strconn1)
        cmd = New SqlCommand("Select * from tblWOfollowUP where ORIGIN_WO='" & Me.txtBBWO2.Text.Trim & "'", conn)
        conn.Open()
        Dim dr2 As SqlDataReader
        dr2 = cmd.ExecuteReader
        While dr2.Read

            Me.txtBBPN2.Text = dr2("MODEL_CODE").ToString.Trim

        End While
        dr2.Close()
        cmd.Dispose()
        conn.Close()


        'Check sequence of PN assigned to Link

        ''Dim main_model As String, Linked_Model1 As String, Linked_Model2 As String

        ''conn = New SqlConnection(Strconn)
        ''cmd = New SqlCommand("Select * from tblLUCID_Linked_Model where Main_Model='" & Me.txtPCBA_WO.Text.Trim & "'", conn)
        ''conn.Open()
        ''Dim dr4 As SqlDataReader
        ''dr4 = cmd.ExecuteReader
        ''While dr4.Read

        ''    main_model = dr4("Main_Model").ToString.Trim


        ''End While
        ''dr4.Close()
        ''cmd.Dispose()
        ''conn.Close()



        'Load SN not linked

        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblLUCID_SN_Log_Load_notlinked'" & Me.txtPCBA_WO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblLUCID_SN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Log"))
            With DataGridView1
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try


        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblLUCID_SN_Log_Load_notlinked1'" & Me.txtBBWO1.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblLUCID_SN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Log"))
            With DataGridView2
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try


        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblLUCID_SN_Log_Load_notlinked1'" & Me.txtBBWO2.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblLUCID_SN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Log"))
            With DataGridView3
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            GroupBox4.Visible = True
        Else
            GroupBox4.Visible = False

        End If
    End Sub
    Private Sub btnLink_Click(sender As Object, e As EventArgs) Handles btnLink.Click
        Link_SN_1()
        Link_SN_2()
        Cleartext_2()

    End Sub
    Private Sub Link_SN_1()

        Dim i As Integer, a As Integer, b As Integer

        a = DataGridView2.Rows.Count - 2

        b = DataGridView1.Rows.Count - 2

        If a = 0 Then
            Exit Sub
        Else

            If a <= b Then

                For i = 0 To a
                    '            SNLINK_RECORD
                    conn = New SqlConnection(Strconn)
                    Dim cmd As New SqlCommand

                    cmd.Connection = conn
                    cmd = New SqlCommand("sptblLUCID_SN_Linked_add", conn)

                    cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

                    cmd.Parameters.Add(New SqlParameter("@SN_PCBA", SqlDbType.NVarChar, 50)).Value = DataGridView1.Rows(i).Cells("SN").Value
                    cmd.Parameters.Add(New SqlParameter("@WO_PCBA", SqlDbType.NChar, 7)).Value = txtPCBA_WO.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@PN_PCBA", SqlDbType.NVarChar, 50)).Value = txtPCBAPN.Text.Trim

                    cmd.Parameters.Add(New SqlParameter("@SN_BOXBUILD", SqlDbType.NVarChar, 50)).Value = DataGridView2.Rows(i).Cells("SN").Value
                    cmd.Parameters.Add(New SqlParameter("@WO_BOXBUILD", SqlDbType.NChar, 7)).Value = txtBBWO1.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@PN_BOXBUILD", SqlDbType.NVarChar, 50)).Value = txtBBPN1.Text.Trim

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        conn.Close()

                    Catch ex As SqlException
                        'MsgBox(ex)
                        conn.Close()

                    End Try


                Next i

            Else
                For i = 0 To b
                    '            SNLINK_RECORD
                    conn = New SqlConnection(Strconn)
                    Dim cmd As New SqlCommand

                    cmd.Connection = conn
                    cmd = New SqlCommand("sptblLUCID_SN_Linked_add", conn)

                    cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

                    cmd.Parameters.Add(New SqlParameter("@SN_PCBA", SqlDbType.NVarChar, 50)).Value = DataGridView1.Rows(i).Cells("SN").Value
                    cmd.Parameters.Add(New SqlParameter("@WO_PCBA", SqlDbType.NChar, 7)).Value = txtPCBA_WO.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@PN_PCBA", SqlDbType.NVarChar, 50)).Value = txtPCBAPN.Text.Trim

                    cmd.Parameters.Add(New SqlParameter("@SN_BOXBUILD", SqlDbType.NVarChar, 50)).Value = DataGridView2.Rows(i).Cells("SN").Value
                    cmd.Parameters.Add(New SqlParameter("@WO_BOXBUILD", SqlDbType.NChar, 7)).Value = txtBBWO1.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@PN_BOXBUILD", SqlDbType.NVarChar, 50)).Value = txtBBPN1.Text.Trim

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        conn.Close()

                    Catch ex As SqlException
                        'MsgBox(ex)
                        conn.Close()

                    End Try


                Next i
            End If
        End If

    End Sub
    Private Sub Link_SN_2()

        DataGridView1.DataSource = ""

        'load PCBA SN
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblLUCID_SN_Log_Load_notlinked'" & Me.txtPCBA_WO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblLUCID_SN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Log"))
            With DataGridView1
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try


        Dim i As Integer, a As Integer, b As Integer

        a = DataGridView3.Rows.Count - 2

        b = DataGridView1.Rows.Count - 2
        If a = 0 Then
            Exit Sub

        Else

            If a <= b Then
                For i = 0 To a
                    '            SNLINK_RECORD

                    conn = New SqlConnection(Strconn)
                    Dim cmd As New SqlCommand

                    cmd.Connection = conn
                    cmd = New SqlCommand("sptblLUCID_SN_Linked_add", conn)

                    cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

                    cmd.Parameters.Add(New SqlParameter("@SN_PCBA", SqlDbType.NVarChar, 50)).Value = DataGridView1.Rows(i).Cells("SN").Value
                    cmd.Parameters.Add(New SqlParameter("@WO_PCBA", SqlDbType.NChar, 7)).Value = txtPCBA_WO.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@PN_PCBA", SqlDbType.NVarChar, 50)).Value = txtPCBAPN.Text.Trim

                    cmd.Parameters.Add(New SqlParameter("@SN_BOXBUILD", SqlDbType.NVarChar, 50)).Value = DataGridView3.Rows(i).Cells("SN").Value
                    cmd.Parameters.Add(New SqlParameter("@WO_BOXBUILD", SqlDbType.NChar, 7)).Value = txtBBWO2.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@PN_BOXBUILD", SqlDbType.NVarChar, 50)).Value = txtBBPN2.Text.Trim

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        conn.Close()

                    Catch ex As SqlException
                        'MsgBox(ex)
                        conn.Close()

                    End Try


                Next i
            Else
                For i = 0 To b
                    '            SNLINK_RECORD

                    conn = New SqlConnection(Strconn)
                    Dim cmd As New SqlCommand

                    cmd.Connection = conn
                    cmd = New SqlCommand("sptblLUCID_SN_Linked_add", conn)

                    cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

                    cmd.Parameters.Add(New SqlParameter("@SN_PCBA", SqlDbType.NVarChar, 50)).Value = DataGridView1.Rows(i).Cells("SN").Value
                    cmd.Parameters.Add(New SqlParameter("@WO_PCBA", SqlDbType.NChar, 7)).Value = txtPCBA_WO.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@PN_PCBA", SqlDbType.NVarChar, 50)).Value = txtPCBAPN.Text.Trim

                    cmd.Parameters.Add(New SqlParameter("@SN_BOXBUILD", SqlDbType.NVarChar, 50)).Value = DataGridView3.Rows(i).Cells("SN").Value
                    cmd.Parameters.Add(New SqlParameter("@WO_BOXBUILD", SqlDbType.NChar, 7)).Value = txtBBWO2.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@PN_BOXBUILD", SqlDbType.NVarChar, 50)).Value = txtBBPN2.Text.Trim

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        conn.Close()

                    Catch ex As SqlException
                        'MsgBox(ex)
                        conn.Close()

                    End Try

                Next i
            End If
        End If

    End Sub
    Private Sub Cleartext_2()
        ' txtWO.Clear()
        txtPCBA_WO.Clear()
        txtPCBAPN.Clear()
        txtBBPN1.Clear()
        txtBBPN2.Clear()
        txtBBWO1.Clear()
        txtBBWO2.Clear()
        DataGridView1.DataSource = ""
        DataGridView2.DataSource = ""
        DataGridView3.DataSource = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtPCBA_WO.Text.Trim = "" Then
            MsgBox("Please input the PCBA WO to get the Linked SNs!!!")
        Else
            Try
                conn = New SqlConnection(Strconn)
                Me.StrSql = "sptblLUCID_SN_Log_Load_Linked'" & Me.txtPCBA_WO.Text.Trim & "'"
                Dim cmd As New SqlCommand(Me.StrSql, conn)
                da = New SqlDataAdapter(cmd)
                Dim cmb As New SqlCommandBuilder(da)
                ds = New DataSet
                da.Fill(ds, "tblLUCID_SN_Linked")
                conn.Close()
                Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Linked"))
                With DataGridView1
                    .DataSource = Dtv
                    .Rows(0).Selected = False
                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                End With
            Catch ex As SqlClient.SqlException
                MsgBox(ex.Message & StrSql)
            End Try

        End If

        Export_Linked_SN()

    End Sub

    Private Sub Export_Linked_SN()
        If (Me.DataGridView1.Columns.Count = 0) Then
            Exit Sub
        End If

        'Creating dataset to export
        Dim dset As New DataSet
        'add table to dataset
        dset.Tables.Add()
        'add column to that table



        For i As Integer = 0 To Me.DataGridView1.ColumnCount - 1
            dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(i).HeaderText)
        Next


        Dim dr1 As DataRow
        For i As Integer = 0 To DataGridView1.RowCount - 1
            dr1 = dset.Tables(0).NewRow
            For j As Integer = 0 To DataGridView1.Columns.Count - 1
                dr1(j) = Trim(DataGridView1.Rows(i).Cells(j).Value)
            Next

            dset.Tables(0).Rows.Add(dr1)
        Next

        Dim excel As New Excel.Application()
        Dim wBook1 As Excel.Workbook

        Dim wSheet1 As Excel.Worksheet


        wBook1 = excel.Workbooks.Add()
        wSheet1 = wBook1.Sheets("Sheet1")

        With wSheet1
            Dim rng As Range

            ' Format dang text cho mot vung tren excel

            rng = wSheet1.Columns("A:F")
            rng.Cells.NumberFormat = "@"

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

        wSheet1.Columns.AutoFit()


        Dim dialog As SaveFileDialog = New SaveFileDialog

        Dim strFileName As String = "LINKED_SN_" & txtPCBA_WO.Text.Trim

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

        wBook1.SaveAs(strFileName)
        excel.Workbooks.Open(strFileName)
        excel.Visible = True


    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        If ComboBox1.Text.Trim = "By WO and Date" Then

            Try
                conn = New SqlConnection(Strconn)
                Me.StrSql = "sptblLUCID_SN_Log_Load_2'" & Me.txtWO.Text.Trim & "','" & Format(Me.DateTimePicker1.Value, "yyyy-MM-dd") & "'"
                Dim cmd As New SqlCommand(Me.StrSql, conn)
                da = New SqlDataAdapter(cmd)
                Dim cmb As New SqlCommandBuilder(da)
                ds = New DataSet
                da.Fill(ds, "tblLUCID_SN_Log")
                conn.Close()
                Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Log"))
                With DataGridView1
                    .DataSource = Dtv
                    .Rows(0).Selected = False
                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                End With
            Catch ex As SqlClient.SqlException
                MsgBox(ex.Message & StrSql)
            End Try

        ElseIf ComboBox1.Text.Trim = "By WO" Then

            Try
                conn = New SqlConnection(Strconn)
                Me.StrSql = "sptblLUCID_SN_Log_Load'" & Me.txtWO.Text.Trim & "'"
                Dim cmd As New SqlCommand(Me.StrSql, conn)
                da = New SqlDataAdapter(cmd)
                Dim cmb As New SqlCommandBuilder(da)
                ds = New DataSet
                da.Fill(ds, "tblLUCID_SN_Log")
                conn.Close()
                Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Log"))
                With DataGridView1
                    .DataSource = Dtv
                    .Rows(0).Selected = False
                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                End With
            Catch ex As SqlClient.SqlException
                MsgBox(ex.Message & StrSql)
            End Try

        ElseIf ComboBox1.Text.Trim = "By Date" Then

            Try
                conn = New SqlConnection(Strconn)
                Me.StrSql = "sptblLUCID_SN_Log_Load_4'" & Format(Me.DateTimePicker1.Value, "yyyy-MM-dd") & "'"
                Dim cmd As New SqlCommand(Me.StrSql, conn)
                da = New SqlDataAdapter(cmd)
                Dim cmb As New SqlCommandBuilder(da)
                ds = New DataSet
                da.Fill(ds, "tblLUCID_SN_Log")
                conn.Close()
                Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Log"))
                With DataGridView1
                    .DataSource = Dtv
                    .Rows(0).Selected = False
                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                End With
            Catch ex As SqlClient.SqlException
                MsgBox(ex.Message & StrSql)
            End Try

        ElseIf ComboBox1.Text.Trim = "By WO and No used" Then

            Try
                conn = New SqlConnection(Strconn)
                Me.StrSql = "sptblLUCID_SN_Log_Load_5'" & Me.txtWO.Text.Trim & "','" & "" & "'"
                Dim cmd As New SqlCommand(Me.StrSql, conn)
                da = New SqlDataAdapter(cmd)
                Dim cmb As New SqlCommandBuilder(da)
                ds = New DataSet
                da.Fill(ds, "tblLUCID_SN_Log")
                conn.Close()
                Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Log"))
                With DataGridView1
                    .DataSource = Dtv
                    .Rows(0).Selected = False
                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                End With
            Catch ex As SqlClient.SqlException
                MsgBox(ex.Message & StrSql)
            End Try


        Else
            MsgBox("PLEASE CHOOSE THE CONDITION THAT YOU WANT TO PULL DATA")
        End If
       

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        ' Delete on Printed SN table [REPRINTLABEL].[dbo].[tblSNPrint]
        For i = 0 To DataGridView1.RowCount() - 2

            Dim SN As String

            SN = DataGridView1.Rows(i).Cells(4).Value.ToString.Trim

            conn = New SqlConnection(Strconn) ' connect to Database
            cmd = New SqlCommand("sptbltblSNPrint_DeleteSN", conn) ' Ket noi vao datatable cua Store Procedure
            cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

            cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NVarChar, 50)).Value = SN


            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
                'MsgBox("HOÀN THÀNH XÓA")
            Catch ex As SqlException
                conn.Close()

                MsgBox("KHÔNG THỂ XÓA")
                Exit Sub
            End Try
        Next
      


        ' Delete Created SN that not use

        conn = New SqlConnection(Strconn) ' connect to Database
        cmd = New SqlCommand("sptblLUCID_SN_NoUsed_Delete", conn) ' Ket noi vao datatable cua Store Procedure
        cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

        cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 7)).Value = Me.txtWO.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@MarkingUsed", SqlDbType.NChar, 10)).Value = ""

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




        'load by WO
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblLUCID_SN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblLUCID_SN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblLUCID_SN_Log"))
            With DataGridView1
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try
    End Sub
End Class