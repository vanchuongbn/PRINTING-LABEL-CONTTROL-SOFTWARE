Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports System.Data
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports Excel = Microsoft.Office.Interop.Excel
Public Class SHOPVAC_SN_CREATE
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Private Sub SHOPVAC_SN_CREATE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            'Dim mm As String

            'issued_date.IndexOf("/")

            'mm = Microsoft.VisualBasic.Left(issued_date, issued_date.IndexOf("/"))

            Dim datecode As String
            datecode = yy
            txtDatecode.Text = datecode
            Dim ver As String
            ver = Microsoft.VisualBasic.Left(dr("REV_PICK_LIST").ToString.Trim, 2)

            If ver.IndexOf(".") = 0 Then
                txtRev.Text = Microsoft.VisualBasic.Mid(ver, 1, 1)
            ElseIf ver.IndexOf(".") = 1 Then
                txtRev.Text = Microsoft.VisualBasic.Left(ver, 1)
            Else
                txtRev.Text = Microsoft.VisualBasic.Left(ver, 1)
            End If

        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()

        'Load name of product

        'conn = New SqlConnection(Strconn2)
        'cmd = New SqlCommand("Select ModelName from tblLabelinformation where Model='" & Me.txtPN.Text.Trim & "'", conn)
        'conn.Open()
        'Dim dr1 As SqlDataReader
        'dr1 = cmd.ExecuteReader
        'While dr1.Read
        '    Me.TextBox4.Text = dr1("ModelName").ToString.Trim

        'End While
        'dr1.Close()
        'cmd.Dispose()
        'conn.Close()

    End Sub
    Private Sub HISTORY_BYWO()

        'If TextBox4.Text.Trim = "" Then
        '    MsgBox("Missing some information. Please confirm again.")

        'Else
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblShopvac_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader()
            While dr1.Read
                If CInt(txtQty.Text.Trim) = CInt(dr1("").ToString.Trim) Then
                    MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")
                    LoadShopvacSN()
                Else
                    CreateSN_Start_End()

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

        TextBox2.Text = CInt(txtSN_Start.Text)
        TextBox3.Text = CInt(txtSN_end.Text)

        Dim fixdigit As String = "U"

        Dim i As Integer
        For i = TextBox2.Text To TextBox3.Text

            conn = New SqlConnection(Strconn)
            Dim cmd As New SqlCommand

            cmd.Connection = conn
            cmd = New SqlCommand("sptblShopvacSN_add", conn)

            cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

            cmd.Parameters.Add(New SqlParameter("@Date", SqlDbType.DateTime)).Value = txtDate.Text.ToString.Trim
            cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 6)).Value = txtWO.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@Datecode", SqlDbType.NChar, 2)).Value = txtDatecode.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@Partnumber", SqlDbType.NChar, 10)).Value = txtPN.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@Sequence", SqlDbType.Int)).Value = i
            cmd.Parameters.Add(New SqlParameter("@Revision", SqlDbType.NChar, 2)).Value = txtRev.Text.Trim

            Dim j As String = CStr(i)

            If Len(j) = 1 Then

                cmd.Parameters.Add(New SqlParameter("@Serial", SqlDbType.NChar, 18)).Value = fixdigit & txtDatecode.Text & "000000" & i
            ElseIf Len(j) = 2 Then

                cmd.Parameters.Add(New SqlParameter("@Serial", SqlDbType.NChar, 18)).Value = fixdigit & txtDatecode.Text & "00000" & i
            ElseIf Len(j) = 3 Then

                cmd.Parameters.Add(New SqlParameter("@Serial", SqlDbType.NChar, 18)).Value = fixdigit & txtDatecode.Text & "0000" & i
            ElseIf Len(j) = 4 Then

                cmd.Parameters.Add(New SqlParameter("@Serial", SqlDbType.NChar, 18)).Value = fixdigit & txtDatecode.Text & "000" & i
            ElseIf Len(j) = 5 Then

                cmd.Parameters.Add(New SqlParameter("@Serial", SqlDbType.NChar, 18)).Value = fixdigit & txtDatecode.Text & "00" & i
            ElseIf Len(j) = 6 Then

                cmd.Parameters.Add(New SqlParameter("@Serial", SqlDbType.NChar, 18)).Value = fixdigit & txtDatecode.Text & "0" & i
            ElseIf Len(j) = 7 Then

                cmd.Parameters.Add(New SqlParameter("@Serial", SqlDbType.NChar, 18)).Value = fixdigit & txtDatecode.Text & i

            Else
                MsgBox("The length of SN number is over!!!")
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

        LoadShopvacSN()

    End Sub

    Private Sub CreateSN_Start_End()



        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblShopvac_SN_Count", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            Dim count As Integer = CInt(dr1("").ToString.Trim)
            If count > 0 Then
                Dim c As String = CStr(count + 1)

                If Len(c) = 1 Then
                    txtSN_Start.Text = "000000" & c
                ElseIf Len(c) = 2 Then
                    txtSN_Start.Text = "00000" & c
                ElseIf Len(c) = 3 Then
                    txtSN_Start.Text = "0000" & c
                ElseIf Len(c) = 4 Then
                    txtSN_Start.Text = "000" & c
                ElseIf Len(c) = 5 Then
                    txtSN_Start.Text = "00" & c
                ElseIf Len(c) = 6 Then
                    txtSN_Start.Text = "0" & c
                ElseIf Len(c) = 7 Then
                    txtSN_Start.Text = c

                End If

                Dim d As String = CStr(count + CInt(txtQty.Text.Trim))
                If Len(d) = 1 Then
                    txtSN_end.Text = "000000" & d
                ElseIf Len(d) = 2 Then
                    txtSN_end.Text = "00000" & d
                ElseIf Len(d) = 3 Then
                    txtSN_end.Text = "0000" & d
                ElseIf Len(d) = 4 Then
                    txtSN_end.Text = "000" & d
                ElseIf Len(d) = 5 Then
                    txtSN_end.Text = "00" & d
                ElseIf Len(d) = 6 Then
                    txtSN_end.Text = "0" & d
                ElseIf Len(d) = 7 Then
                    txtSN_end.Text = d

                End If

            Else

                txtSN_Start.Text = "0000001"

                Dim a As String = CStr(count + CInt(txtQty.Text.Trim))
                ' MsgBox(Len(a))

                If Len(a) = 1 Then
                    txtSN_end.Text = "000000" & a
                ElseIf Len(a) = 2 Then
                    txtSN_end.Text = "00000" & a
                ElseIf Len(a) = 3 Then
                    txtSN_end.Text = "0000" & a
                ElseIf Len(a) = 4 Then
                    txtSN_end.Text = "000" & a
                ElseIf Len(a) = 5 Then
                    txtSN_end.Text = "00" & a
                ElseIf Len(a) = 6 Then
                    txtSN_end.Text = "0" & a
                ElseIf Len(a) = 7 Then
                    txtSN_end.Text = a

                End If

            End If

        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

    End Sub


    Private Sub LoadShopvacSN()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblShopvacSN_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblShopvacSN")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblShopvacSN"))
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
        SN_Export()

    End Sub
    Private Sub SN_Export()
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