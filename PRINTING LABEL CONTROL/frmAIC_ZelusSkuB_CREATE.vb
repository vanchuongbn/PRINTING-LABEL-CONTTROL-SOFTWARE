Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Office.Interop.Excel
Imports Excel = Microsoft.Office.Interop.Excel
Public Class frmAIC_ZelusSkuB_CREATE
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim Model_Code As String = ""
    Dim dal_common As DAL_Common = New DAL_Common()

    Private Sub frmAIC_ZelusSkuB_CREATE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPN.Enabled = False
        txtDatecode.Enabled = False
        txtQty.Enabled = False
        txtPO.Enabled = True
        txtPO_Item.Enabled = True
        txtSN_end.Enabled = False
        txtSN_Start.Enabled = False
        txtDate.Hide()
        txtDate.Text = DateTimePicker1.Value.ToShortDateString
        txtStart_i.Enabled = False
        txtEnd_i.Enabled = False
        TextBox4.Enabled = False
        LOGIN.Size = MinimumSize

        Load_Mac_Range()
        Load_Mac_available()
        Count_MAC_available()
        Load_MAC_PN()

    End Sub

    Private Sub Load_MAC_PN()
        Try
            conn.Open()
            conn = New SqlConnection(Strconn)
            Me.StrSql = "spAIC_Zelus_SkuB_Mac_Required_PN_Load" ' & "A" & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblAIC_Zelus_SkuB_MAC_Required_PN")
            Dim Dtv As DataView = New DataView(ds.Tables("tblAIC_Zelus_SkuB_MAC_Required_PN"))
            With dgwMAC_PN
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        Finally
            conn.Close()
        End Try
    End Sub
    Private Sub Count_MAC_available()
        Try
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("sptblAIC_Zelus_SkuB_MAC_Count", conn) ' Ket noi vao datatable cua Store Procedure
            conn.Open()
            Dim dr1 As SqlDataReader

            dr1 = cmd.ExecuteReader()
            While dr1.Read
                TextBox5.Text = dr1("").ToString.Trim
            End While
            dr1.Close()
            cmd.Dispose()

            If CInt(TextBox5.Text) < 1000 Then
                MsgBox("AVAILABLE MAC_ADDRESS QUANTITY IS LESS THAN 1K, PLEASE WORK WITH CUSTOMER TO PROVIDE A NEW RANGE")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub Load_Mac_available()
        Try
            conn = New SqlConnection(Strconn)
            conn.Open()
            Me.StrSql = "spGetAIC_Zelus_SkuB_Mac_Available" ' & "A" & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblAIC_Zelus_SkuB_MAC_List")
            Dim Dtv As DataView = New DataView(ds.Tables("tblAIC_Zelus_SkuB_MAC_List"))
            dgwMAC_List.DataSource = Dtv
            'With dgwMAC_List
            '    .DataSource = Dtv
            '    .Rows(0).Selected = False
            '    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            '    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            'End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Load_Mac_Range()
        Try
            conn = New SqlConnection(Strconn)
            conn.Open()
            Me.StrSql = "spGetAIC_Zelus_SkuB_Mac_Range" ' & "A" & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblAIC_Zelus_SkuB_MAC_range")
            Dim Dtv As DataView = New DataView(ds.Tables("tblAIC_Zelus_SkuB_MAC_range"))
            dgwMAC_table.DataSource = Dtv
            'With dgwMAC_table
            '    .DataSource = Dtv
            '    .Rows(0).Selected = False
            '    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            '    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            'End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub txtWO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWO.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            Cleartext()

            Load_Mac_available()
            Count_MAC_available()

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
        txtPO.Clear()
        txtPO_Item.Clear()
        txtStart_i.Clear()
        txtEnd_i.Clear()
        TextBox4.Clear()

    End Sub
    Private Sub LoadWO_Information()
        Try
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("spGetWO_infor'" & Me.txtWO.Text.Trim & "'", conn)
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
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("sptblAIC_Zelus_SkuB_SN_Log_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader()
            While dr1.Read
                If CInt(txtQty.Text.Trim) = CInt(dr1("").ToString.Trim) Then
                    MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")
                    PCBA_SN_Log_History_Load()
                Else

                    MsgBox("Please input PO information the click 'Create' Button")

                End If
            End While
            dr1.Close()
            cmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try



    End Sub
    Private Sub PCBA_SN_Log_Load()

        Try
            conn = New SqlConnection(Strconn)
            conn.Open()
            Dim Str_Querry = "select ID,[Date], Wo, PN, Datecode,AIC_PO, PO_Item, SN, [Sequence] from tblAIC_Zelus_SkuB_SN_log where WO = @Wo order by [Sequence]"
            Dim cmd As New SqlCommand(Str_Querry, conn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@Wo", SqlDbType.VarChar).Value = Me.txtWO.Text.ToString.Trim
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            da.Fill(ds, "tblAIC_Zelus_SkuB_SN_log")
            Dim Dtv As DataView = New DataView(ds.Tables("tblAIC_Zelus_SkuB_SN_log"))
            With dgwSN
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        Finally
            conn.Close()
        End Try

    End Sub
    Private Sub PCBA_SN_Log_History_Load()
        Dim cn As New SqlConnection(Strconn)
        Try

            cn.Open()
            Dim Str_Querry = "PCBA_AICZelus_SkuB_GetListSN_History_By_WO"
            Dim cmd As New SqlCommand(Str_Querry, cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Wo", SqlDbType.VarChar).Value = Me.txtWO.Text.ToString.Trim
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            da.Fill(ds, 0)
            Dim Dtv As DataView = New DataView(ds.Tables(0))
            With dgwSN
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        Finally
            cn.Close()
        End Try

    End Sub

    Private Sub CreateSN_start_End()
        Try
            conn = New SqlConnection(Strconn)
            conn.Open()
            cmd = New SqlCommand("sptblAIC_Zelus_SkuB_SN_Count'" & txtPO.Text.Trim & "' ,'" & txtPN.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
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

            End While
            dr1.Close()
            cmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub
    Private Sub createSN_List()

        txtStart_i.Text = CInt(txtSN_Start.Text)
        txtEnd_i.Text = CInt(txtSN_end.Text)

        Dim Start_SN As Integer = CInt(txtSN_Start.Text)
        Dim End_SN As Integer = CInt(txtSN_Start.Text)

        'create SN
        If (Model_Code = "BMB1UPS0001BC011_test") Then
            Dim Fixed_Charact As String = ""
            Dim Retval As Integer = 1
            Retval = dal_common.createSN_List_ACIWithNewPN(Start_SN, End_SN, txtDate.Text.ToString.Trim, txtWO.Text.ToString.Trim, txtPN.Text.ToString.Trim, txtDatecode.Text.ToString.Trim, txtPO.Text.ToString.Trim, txtPO_Item.Text.ToString.Trim, Fixed_Charact)
            If (Retval = 0) Then
                MsgBox("The length Of SN number Is over!!!")
            End If
        Else
            Dim i As Integer
            Try
                Dim Fixed_Charact As String = "AICAMDA0227-0001"
                Dim Po_i = txtPO.Text.Trim
                Fixed_Charact += txtDatecode.Text.Trim
                conn = New SqlConnection(Strconn)
                conn.Open()
                For i = txtStart_i.Text To txtEnd_i.Text
                    cmd = New SqlCommand("sptblAIC_Zelus_SkuB_SN_Log_add", conn)
                    cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure
                    cmd.Parameters.Add(New SqlParameter("@Date", SqlDbType.DateTime)).Value = txtDate.Text.ToString.Trim
                    cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 7)).Value = txtWO.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@PN", SqlDbType.NVarChar, 50)).Value = txtPN.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@Datecode", SqlDbType.NChar, 4)).Value = txtDatecode.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@AIC_PO", SqlDbType.NChar, 13)).Value = txtPO.Text.Trim
                    cmd.Parameters.Add(New SqlParameter("@PO_Item", SqlDbType.NChar, 1)).Value = txtPO_Item.Text.Trim

                    cmd.Parameters.Add(New SqlParameter("@Sequence", SqlDbType.Int)).Value = i


                    Dim j As String = CStr(i)

                    If Len(j) = 1 Then
                        cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 23)).Value = txtPO.Text.Trim & txtPO_Item.Text.Trim & "000" & i '& "U" & txtDatecode.Text.Trim
                        cmd.Parameters.Add(New SqlParameter("@SN_1", SqlDbType.NChar, 24)).Value = Fixed_Charact & "000" & i '& "U" & txtDatecode.Text.Trim
                    ElseIf Len(j) = 2 Then

                        cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 23)).Value = txtPO.Text.Trim & txtPO_Item.Text.Trim & "00" & i '& "U" & txtDatecode.Text.Trim
                        cmd.Parameters.Add(New SqlParameter("@SN_1", SqlDbType.NChar, 24)).Value = Fixed_Charact & "00" & i '& "U" & txtDatecode.Text.Trim
                    ElseIf Len(j) = 3 Then

                        cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 23)).Value = txtPO.Text.Trim & txtPO_Item.Text.Trim & "0" & i '& "U" & txtDatecode.Text.Trim
                        cmd.Parameters.Add(New SqlParameter("@SN_1", SqlDbType.NChar, 24)).Value = Fixed_Charact & "0" & i '& "U" & txtDatecode.Text.Trim
                    ElseIf Len(j) = 4 Then

                        cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 23)).Value = txtPO.Text.Trim & txtPO_Item.Text.Trim & i ' & "U" & txtDatecode.Text.Trim
                        cmd.Parameters.Add(New SqlParameter("@SN_1", SqlDbType.NChar, 24)).Value = Fixed_Charact & i ' & "U" & txtDatecode.Text.Trim


                    Else
                        MsgBox("The length Of SN number Is over!!!")
                    End If
                    cmd.ExecuteNonQuery()

                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                conn.Close()
            End Try


        End If

        PCBA_SN_Log_Load()
    End Sub

    Private Sub tbnNew_Click(sender As Object, e As EventArgs) Handles tbnNew.Click
        dgwSN.DataSource = ""
        dgwSN.Columns.Clear()
        Cleartext()
        txtWO.Clear()

        Load_Mac_Range()
        Load_Mac_available()
        Count_MAC_available()

    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        PCBA_SN_Export()

    End Sub
    Private Sub PCBA_SN_Export()
        If ((Me.dgwSN.Columns.Count = 0) Or (Me.dgwSN.Rows.Count = 0)) Then
            Exit Sub
        End If

        'Creating dataset to export
        Dim dset As New DataSet
        'add table to dataset
        dset.Tables.Add()
        'add column to that table

        For i As Integer = 0 To Me.dgwSN.ColumnCount - 1
            dset.Tables(0).Columns.Add(Me.dgwSN.Columns(i).HeaderText)
        Next

        Dim dr1 As DataRow

        For i As Integer = 0 To dgwSN.RowCount - 1
            dr1 = dset.Tables(0).NewRow

            For j As Integer = 0 To Me.dgwSN.ColumnCount - 1
                dr1(j) = Trim(dgwSN.Rows(i).Cells(j).Value)

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

    Private Sub btnUploadMac_Click(sender As Object, e As EventArgs) Handles btnUploadMac.Click
        'Check_Duplicate

        Dim StartMAC_Dec_i As String, EndMAC_Dec_i As String
        Dim StartMAC_I As String, EndMAC_I As String

        Dim StartMAC_Dec As Long, EndMAC_Dec As Long

        StartMAC_I = txtStart_MAC.Text.Trim
        EndMAC_I = txtEnd_MAC.Text.Trim
        StartMAC_I = Replace(Replace(StartMAC_I, ":", ""), "-", "")
        EndMAC_I = Replace(Replace(EndMAC_I, ":", ""), "-", "")

        StartMAC_Dec = CLng("&H" & Microsoft.VisualBasic.Right(StartMAC_I, 6))
        EndMAC_Dec = CLng("&H" & Microsoft.VisualBasic.Right(EndMAC_I, 6))

        txtMAC_Start_Dec.Text = StartMAC_Dec
        txtMAC_End_Dec.Text = EndMAC_Dec

        Dim i As Integer

        If dgwMAC_table.RowCount() > 0 Then


            For i = 0 To dgwMAC_table.RowCount() - 1

                StartMAC_Dec_i = dgwMAC_table.Rows(i).Cells(4).Value
                EndMAC_Dec_i = dgwMAC_table.Rows(i).Cells(5).Value


                If StartMAC_Dec >= StartMAC_Dec_i And StartMAC_Dec <= EndMAC_Dec_i Then
                    MsgBox("New MAC range is duplicated with previous assignment")

                    Exit For

                ElseIf EndMAC_Dec >= StartMAC_Dec_i And EndMAC_Dec <= EndMAC_Dec_i Then

                    MsgBox("New MAC range is duplicated with previous assignment")

                    Exit For

                ElseIf StartMAC_Dec <= StartMAC_Dec_i And EndMAC_Dec >= EndMAC_Dec_i Then
                    MsgBox("New MAC range is duplicated with previous assignment")
                    Exit For


                Else

                    Save_MAc_range()

                    Save_Mac_List()

                End If


            Next

        Else

            Save_MAc_range()

            Save_Mac_List()


        End If

    End Sub

    Private Sub Save_MAc_range()
        Try
            conn = New SqlConnection(Strconn)
            conn.Open()
            cmd = New SqlCommand("sptblAIC_Zelus_SkuB_MAC_RANGE_add", conn) ' Ket noi vao datatable cua Store Procedure
            cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure
            'cmd.Parameters.Add(New SqlParameter("@ID", SqlDbType.Int)).Value = TextBox10.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@Inputdate", SqlDbType.Date)).Value = txtDate.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@MAC_Start", SqlDbType.NChar, 12)).Value = txtStart_MAC.Text.Trim.ToUpper
            cmd.Parameters.Add(New SqlParameter("@MAC_End", SqlDbType.NChar, 12)).Value = txtEnd_MAC.Text.Trim.ToUpper
            cmd.Parameters.Add(New SqlParameter("@Mac_Start_Dec", SqlDbType.Float)).Value = CInt(txtMAC_Start_Dec.Text)
            cmd.Parameters.Add(New SqlParameter("@Mac_End_Dec", SqlDbType.Float)).Value = CInt(txtMAC_End_Dec.Text)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Can not insert Mac_Range. \n" + ex.Message)
        Finally
            conn.Close()
        End Try


    End Sub

    Private Sub Save_Mac_List()


        Dim StartMAC_Dec As Long, EndMAC_Dec As Long, fixdigit As String
        Dim StartMAC_I As String, EndMAC_I As String

        StartMAC_I = txtStart_MAC.Text.Trim
        EndMAC_I = txtEnd_MAC.Text.Trim
        StartMAC_I = Replace(Replace(StartMAC_I, ":", ""), "-", "")
        EndMAC_I = Replace(Replace(EndMAC_I, ":", ""), "-", "")

        fixdigit = Microsoft.VisualBasic.Left(txtStart_MAC.Text.Trim, 6)


        StartMAC_Dec = CLng("&H" & Microsoft.VisualBasic.Right(StartMAC_I, 6))
        EndMAC_Dec = CLng("&H" & Microsoft.VisualBasic.Right(EndMAC_I, 6))

        txtMAC_Start_Dec.Text = StartMAC_Dec
        txtMAC_End_Dec.Text = EndMAC_Dec

        Dim j As Integer
        Try
            conn = New SqlConnection(Strconn)
            conn.Open()
            For j = StartMAC_Dec To EndMAC_Dec


                ' For Each row As DataGridViewRow In DataGridView1.Rows
                cmd = New SqlCommand("tblAIC_Zelus_SkuB_MAC_List_Add", conn) ' Ket noi vao datatable cua Store Procedure
                cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

                cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 23)).Value = ""
                cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 7)).Value = ""

                Dim DectoHex As String = Hex(j)

                If Len(DectoHex) = 1 Then

                    cmd.Parameters.Add(New SqlParameter("@Mac#", SqlDbType.NChar, 17)).Value = fixdigit & "00000" & DectoHex

                ElseIf Len(DectoHex) = 2 Then
                    cmd.Parameters.Add(New SqlParameter("@Mac#", SqlDbType.NChar, 17)).Value = fixdigit & "0000" & DectoHex

                ElseIf Len(DectoHex) = 3 Then
                    cmd.Parameters.Add(New SqlParameter("@Mac#", SqlDbType.NChar, 17)).Value = fixdigit & "000" & DectoHex

                ElseIf Len(DectoHex) = 4 Then
                    cmd.Parameters.Add(New SqlParameter("@Mac#", SqlDbType.NChar, 17)).Value = fixdigit & "00" & DectoHex

                ElseIf Len(DectoHex) = 5 Then
                    cmd.Parameters.Add(New SqlParameter("@Mac#", SqlDbType.NChar, 17)).Value = fixdigit & "0" & DectoHex

                ElseIf Len(DectoHex) = 6 Then
                    cmd.Parameters.Add(New SqlParameter("@Mac#", SqlDbType.NChar, 17)).Value = fixdigit & DectoHex

                Else
                    MsgBox("The length of MAC# is out of range")

                End If

                cmd.ExecuteNonQuery()

            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try


        Load_Mac_Range()
        Load_Mac_available()
        Count_MAC_available()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnSearch_MAC.Click
        LoadMAC_byWO()


    End Sub

    Sub LoadMAC_byWO()
        Try
            conn = New SqlConnection(Strconn)
            conn.Open()
            Me.StrSql = "sptblAIC_Zelus_SkuB_MAC_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblAIC_Zelus_SkuB_MAC_List")
            Dim Dtv As DataView = New DataView(ds.Tables("tblAIC_Zelus_SkuB_MAC_List"))
            With dgwMAC_List
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Dim PO_i = txtPO.Text.Trim()
        Dim idx_ofPO As Integer = PO_i.IndexOf("-"c)
        If txtPO.Text.Trim = "" Or txtPO_Item.Text.Trim = "" Then
            MsgBox("Please input PO Information")
        ElseIf (idx_ofPO = -1) Or (PO_i.Length < idx_ofPO + 1 + 4) Then
            MsgBox("PO is not corect if must be contant: '-YYMM'")
        Else
            'Check MAC# qty and WOqty to ensure enough the MAC#

            Dim PN As String

            PN = txtPN.ToString.Trim

            Dim MACreq As Integer, i As Integer

            For i = 0 To dgwMAC_PN.RowCount() - 1

                If dgwMAC_PN.Rows(i).Cells("PN").Value = PN Then

                    MACreq = dgwMAC_PN.Rows(i).Cells("MAC_required").Value

                Else
                    MACreq = 0

                End If

            Next


            If CInt(txtQty.Text.Trim) * MACreq > CInt(TextBox5.Text.Trim) Then
                MsgBox("REMAIN MAC# NUMBER IS NOT ENOUGH TO ASSIGN FOR THIS WO. PLEASE WORK WITH CUSTOMER TO PROVIDE NEW MAC#")
            Else

                CreateSN_start_End()
                createSN_List()


                'Assign MAC to WO and SN

                Update_MAC_WO()
                dgwSN.DataSource = Nothing
                dgwSN.Columns.Clear()
                PCBA_SN_Log_History_Load()
            End If

        End If


    End Sub

    Private Sub Update_MAC_WO()
        'Load MAC_Range to assigned for WO

        Load_Mac_available()

        Dim PN As String, PN1 As String

        PN = txtPN.Text.Trim

        Dim MACreq As Integer, i As Integer

        For i = 0 To dgwMAC_PN.RowCount() - 2

            PN1 = dgwMAC_PN.Rows(i).Cells("PN").Value.ToString.Trim

            If PN1 = PN Then

                MACreq = dgwMAC_PN.Rows(i).Cells("MAC_required").Value
                Exit For
            Else
                MACreq = 0

            End If

        Next

        If MACreq > 0 Then
            'add MACcol to SNtable

            For c = 1 To MACreq
                Dim Cname As String
                Cname = "MAC-" & c
                dgwSN.Columns.Add(Cname, Cname)


            Next


            Dim k As Integer
            Try
                Dim conn As SqlConnection = New SqlConnection(Strconn)
                conn.Open()
                For k = 0 To dgwSN.RowCount() - 1

                    Dim x As Integer, colNo As Integer

                    colNo = dgwSN.ColumnCount() - 1

                    '  MsgBox(colNo)

                    For x = colNo - MACreq + 1 To colNo


                        dgwSN.Rows(k).Cells(x).Value = dgwMAC_List.Rows(0).Cells("MAC#").Value.ToString

                        cmd = New SqlCommand("sptblAIC_MAC_Zelus_SkuB_Update", conn) ' Ket noi vao datatable cua Store Procedure
                        cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

                        cmd.Parameters.AddWithValue("Mac#", dgwMAC_List.Rows(0).Cells("Mac#").Value)
                        cmd.Parameters.AddWithValue("SN", dgwSN.Rows(k).Cells("SN").Value)
                        cmd.Parameters.AddWithValue("WO", Me.txtWO.Text.Trim)

                        Try

                            cmd.ExecuteNonQuery()

                        Catch ex As SqlException

                            'MessageBox.Show("Can not insert Mac#.")
                        End Try

                        Load_Mac_available()

                    Next

                Next

            Catch ex As Exception
                'donothing
            Finally
                conn.Close()
            End Try



        End If

        LoadMAC_byWO()

        Count_MAC_available()

    End Sub


End Class