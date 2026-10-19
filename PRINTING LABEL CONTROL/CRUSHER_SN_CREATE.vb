Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports System.Data
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports Excel = Microsoft.Office.Interop.Excel

Imports System.Reflection
Imports Microsoft.Win32
Imports System.Net.Mail
Imports System.Net.Mime
Imports Newtonsoft.Json
Imports System.Configuration
Imports ClosedXML.Excel
Imports DataTable = System.Data.DataTable

Public Class CRUSHER_SN_CREATE
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand
    Dim cmd1 As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Private Sub CRUSHER_SN_CREATE_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Load()

    End Sub
    Private Sub Count_MAC_available()
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblCrusher_MAC_Count'" & "A" & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            TextBox5.Text = dr1("").ToString.Trim
        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

        If CInt(TextBox5.Text) < 20000 Then
            MsgBox("AVAILABLE MAC_ADDRESS QUANTITY IS LESS THAN 20K, PLEASE WORK WITH CUSTOMER TO PROVIDE A NEW RANGE")
        End If
    End Sub

    Private Sub Load_Mac_available()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "spGetMac_Available" ' & "A" & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblCrusher_MAC")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblCrusher_MAC"))
            With DataGridView4
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try
    End Sub

    Private Sub txtWO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWO.KeyPress
        If RadioButton1.Checked = False And RadioButton2.Checked = False And RadioButton5.Checked = False Then
            MsgBox("PLEASE CHOOSE PRODUCT !!! ")
        Else
            If e.KeyChar = Chr(Keys.Enter) Then
                Cleartext()

                'Load thong tin WO
                LoadWO_Information()
                ' Looking history of WO 
                If RadioButton1.Checked = True And CInt(txtQty.Text) > CInt(TextBox5.Text) And txtWO.Text.Trim.IndexOf("02-") = 0 Then
                    MsgBox("MAC# IS NOT ENOUGH FOR WO")

                Else
                    HISTORY_BYWO()
                End If

                ' End If
            End If
        End If

    End Sub
    Private Sub Cleartext()
        txtPN.Clear()
        txtQty.Clear()
        txtDatecode.Clear()
        txtSN_end.Clear()
        txtSN_Start.Clear()
        txtPCBA_WO.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        txtdesc.Clear()
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
            Me.txtdesc.Text = dr("MODEL_NAME").ToString.Trim
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
            txtDatecode.Text = ww
            Dim ver As String
            ver = dr("REV_PICK_LIST").ToString.Trim
            If RadioButton5.Checked = True Then
                txtRev.Text = Microsoft.VisualBasic.Left(ver, 2)
            Else
                If ver.IndexOf(".") = 1 Then
                    MsgBox("Please confirm REV again")
                ElseIf ver.IndexOf(".") = 0 Then
                    txtRev.Text = Microsoft.VisualBasic.Right(ver, 2)
                Else
                    txtRev.Text = Microsoft.VisualBasic.Left(ver, 2)
                End If

            End If

        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()
    End Sub
    Private Sub HISTORY_BYWO()
        If RadioButton2.Checked = True Then
            ' If txtPN.Text.Trim.IndexOf("02-0114") = 0 Or txtPN.Text.Trim.IndexOf("02-0136") = 0 Then

            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("sptblEimezSN_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
            conn.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader()
            While dr1.Read
                If CInt(dr1("").ToString.Trim) > 1 Then
                    MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")

                    Eimez_SN_Log_Load()
                Else
                    'Tao SN format_End _Start cua WO
                    CreateSN_Eimez()
                    ' Tao danh sach so SN cho WO
                    createSN_List()

                End If
            End While
            dr1.Close()
            cmd.Dispose()
            conn.Close()
        ElseIf RadioButton1.Checked = True Then

            If txtPN.Text.Trim.IndexOf("02-") >= 0 Then
                txtPCBA_WO.Clear()

                txtPCBA_WO.Visible = False

                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("sptblCrusher_SN_PCBA_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
                conn.Open()
                Dim dr1 As SqlDataReader
                dr1 = cmd.ExecuteReader()
                While dr1.Read
                    If CInt(txtQty.Text.Trim) = CInt(dr1("").ToString.Trim) Then
                        MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")
                        PCBA_SN_Log_Load()
                    Else
                        Dim conn1 As SqlConnection = New SqlConnection(Strconn)
                        cmd = New SqlCommand("sptblCrusher_SN_Count_byWO'" & txtWO.Text.Trim & "'", conn1) ' Ket noi vao datatable cua Store Procedure
                        conn1.Open()
                        Dim dr2 As SqlDataReader
                        dr2 = cmd.ExecuteReader()
                        While dr2.Read
                            If CInt(txtQty.Text.Trim) < CInt(dr2("").ToString.Trim) Then
                                'Kiem tra so luong da tao truoc do'
                                'MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")
                                PCBA_SN_Log_Load1()
                            Else
                                'Tao SN format_End _Start cua WO
                                CreateSN_PCBA_Start_End()
                                'Tao danh sach so SN cho WO
                                createSN_List()
                                'Lay so MAC_Start_End
                                Get_Start_End_Mac()
                                'Load danh sach MAC can dung
                                Load_Mac_Range()
                            End If

                        End While
                        dr2.Close()
                        cmd.Dispose()
                        conn1.Close()
                    End If
                End While
                dr1.Close()
                cmd.Dispose()
                conn.Close()
            ElseIf txtPN.Text.Trim.IndexOf("83-") >= 0 Then

                MsgBox("PLEASE INPUT PCBA_WO TO BOX!!!")
                txtPCBA_WO.Visible = True
                txtPCBA_WO.Clear()
                txtPCBA_WO.Focus()

            ElseIf txtPN.Text.Trim.IndexOf("80-") >= 0 Then
                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("sptblCrusher_SN_FG_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
                conn.Open()
                Dim dr1 As SqlDataReader
                dr1 = cmd.ExecuteReader()
                While dr1.Read
                    If CInt(dr1("").ToString.Trim) > 1 Then
                        MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")

                        FG_SN_Log_Load()
                    Else
                        'Tao SN format_End _Start cua WO
                        CreateSN_FG()
                        ' Tao danh sach so SN cho WO
                        createSN_List()

                    End If
                End While
                dr1.Close()
                cmd.Dispose()
                conn.Close()
            End If


        ElseIf RadioButton5.Checked = True Then
            If txtPN.Text.Trim.IndexOf("31-") >= 0 Then

                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("sptblCarrierSN_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
                conn.Open()
                Dim dr1 As SqlDataReader
                dr1 = cmd.ExecuteReader()
                While dr1.Read
                    If CInt(dr1("").ToString.Trim) > 1 Then
                        MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")

                        CARRIER_SN_Log_Load()
                    Else
                        'Tao SN format_End _Start cua WO
                        CreateSN_Carrier()
                        ' Tao danh sach so SN cho WO
                        createSN_List()

                    End If
                End While
                dr1.Close()
                cmd.Dispose()
                conn.Close()
            End If
        End If


    End Sub
    Private Sub FG_SN_Log_Load()

        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblCrusherSN_FG_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblCrusherSN_Log_FG")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblCrusherSN_Log_FG"))
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


    Private Sub CARRIER_SN_Log_Load()

        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblCarrierSN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblCarrierSN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblCarrierSN_Log"))
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
    Private Sub Eimez_SN_Log_Load()

        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblEimezSN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblEimezSN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblEimezSN_Log"))
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


        Dim fixdigit As String = "PFMUH"
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblCrusher_SN_Count_byDatecode1'" & txtDatecode.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()


        Dim count As Integer
        While dr1.Read
            If IsDBNull(dr1("")) Then
                ' If IsDBNull(dr1("")) Then
                count = 0
            Else
                count = CInt(dr1("").ToString.Trim)
            End If

            If count > 0 Then
                'Dim count1 As Integer = count + 1
                Dim countplus As Integer = count + CInt(txtQty.Text.Trim) - 1
                'MsgBox(Len(count1.ToString.Trim))
                Dim c As String = Hex(count)
                If Len(c) = 1 Then
                    txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "000" & c
                ElseIf Len(c) = 2 Then
                    txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "00" & c
                ElseIf Len(c) = 3 Then
                    txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "0" & c
                ElseIf Len(c) = 4 Then
                    txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & c
                End If

                Dim d As String = Hex(countplus)
                If Len(d) = 1 Then
                    txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "000" & d
                ElseIf Len(d) = 2 Then
                    txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "00" & d
                ElseIf Len(d) = 3 Then
                    txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "0" & d
                ElseIf Len(d) = 4 Then
                    txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & d
                End If

            Else

                txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "0000"

                Dim a As String = Hex(CInt(txtQty.Text.Trim) - 1)
                If Len(a) = 1 Then
                    txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "000" & a
                ElseIf Len(a) = 2 Then
                    txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "00" & a
                ElseIf Len(a) = 3 Then
                    txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "0" & a
                ElseIf Len(a) = 4 Then
                    txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & a

                End If
            End If
        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()
        ' Tao danh sach so SN cho WO
        'createSN_List()

    End Sub
    Private Sub CreateSN_BOXBUILD_Start_End()

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("spGetCrusherSN_Start_byWO'" & txtPCBA_WO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            txtSN_Start.Text = dr1("SN")
        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("spGetCrusherSN_End_byWO'" & txtPCBA_WO.Text.Trim & "','" & txtQty.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr2 As SqlDataReader
        dr2 = cmd.ExecuteReader()
        While dr2.Read
            txtSN_end.Text = dr2("SN")
        End While
        dr2.Close()
        cmd.Dispose()
        conn.Close()

    End Sub
    'Private Sub CreateSN_BOXBUILD_Start_End()
    '    Dim fixdigit As String = "PFMUH"
    '    conn = New SqlConnection(Strconn)
    '    cmd = New SqlCommand("sptblCrusher_SN_Count_byDatecode2'" & txtDatecode.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
    '    conn.Open()
    '    Dim dr1 As SqlDataReader
    '    dr1 = cmd.ExecuteReader()
    '    While dr1.Read
    '        Dim count As Integer = CInt(dr1("").ToString.Trim)
    '        If count > 0 Then
    '            Dim c As String = Hex(count)
    '            '  Dim count1 As Integer = count + 1
    '            Dim countplus As Integer = count + CInt(txtQty.Text.Trim) - 1
    '            'MsgBox(Len(count1.ToString.Trim))
    '            If Len(c) = 1 Then
    '                txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "000" & c
    '            ElseIf Len(c) = 2 Then
    '                txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "00" & c
    '            ElseIf Len(c) = 3 Then
    '                txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "0" & c
    '            ElseIf Len(c) = 4 Then
    '                txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & c
    '            End If

    '            Dim d As String = Hex(countplus)
    '            If Len(d) = 1 Then
    '                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "000" & d
    '            ElseIf Len(d) = 2 Then
    '                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "00" & d
    '            ElseIf Len(d) = 3 Then
    '                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "0" & d
    '            ElseIf Len(d) = 4 Then
    '                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & d
    '            End If

    '        Else
    '            txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "0000"
    '            Dim a As String = Hex(CInt(txtQty.Text.Trim) - 1)
    '            If Len(a) = 1 Then
    '                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "000" & a
    '            ElseIf Len(a) = 2 Then
    '                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "00" & a
    '            ElseIf Len(a) = 3 Then
    '                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "0" & a
    '            ElseIf Len(a) = 4 Then
    '                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & a

    '            End If
    '        End If
    '    End While
    '    dr1.Close()
    '    cmd.Dispose()
    '    conn.Close()
    '    Tao danh sach so SN cho WO
    '    createSN_List()
    '    EndSub

    Private Sub CreateSN_FG()

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblCrusher_SN_GF_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader()
        While dr.Read
            If CInt(dr("").ToString.Trim) > 0 Then
                'If CInt(dr("").ToString.Trim) >= CInt(txtQty.Text.Trim) Then
                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("spGetCrusherSN_Start_byWO_FG'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
                conn.Open()
                Dim dr2 As SqlDataReader
                dr2 = cmd.ExecuteReader()
                While dr2.Read
                    txtSN_Start.Text = dr2("SN").ToString.Trim
                End While
                dr2.Close()
                cmd.Dispose()
                conn.Close()
                '--------------------
                If txtQty.Text = 1 Then

                    txtSN_end.Text = txtSN_Start.Text
                Else

                    conn = New SqlConnection(Strconn)
                    cmd = New SqlCommand("spGetCrusherSN_FG_End_byWO'" & Me.txtWO.Text.Trim & "," & CInt(Me.txtQty.Text.Trim) & "'", conn)
                    conn.Open()
                    Dim dr3 As SqlDataReader
                    dr3 = cmd.ExecuteReader
                    While dr3.Read
                        Me.txtSN_end.Text = dr3("SN").ToString.Trim
                    End While
                    dr3.Close()
                    cmd.Dispose()
                    conn.Close()
                End If

            End If
        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()


        Dim fixdigit As String = "PSPUH"
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblCrusher_SN_GF_Count_byDatecode1'" & txtDatecode.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            Dim count As Integer = CInt(dr1("").ToString.Trim)
            Dim a As String = Hex(count)
            ' Dim count1 As Integer = count + 1

            'MsgBox(Len(count1.ToString.Trim))
            If Len(a) = 1 Then
                txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "000" & a
            ElseIf Len(a) = 2 Then
                txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "00" & a
            ElseIf Len(a) = 3 Then
                txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "0" & a
            ElseIf Len(a) = 4 Then
                txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & a
            End If

            Dim countplus As Integer = count + CInt(txtQty.Text.Trim) - 1
            Dim b As String = Hex(countplus)

            If Len(b) = 1 Then
                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "000" & b
            ElseIf Len(b) = 2 Then
                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "00" & b
            ElseIf Len(b) = 3 Then
                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "0" & b
            ElseIf Len(b) = 4 Then
                txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & b
            End If


        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()
    End Sub
    Private Sub CreateSN_Carrier()

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblCarrierSN_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader()
        While dr.Read
            If CInt(dr("").ToString.Trim) > 0 Then
                'If CInt(dr("").ToString.Trim) >= CInt(txtQty.Text.Trim) Then
                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("spGetCarrierSN_Start_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
                conn.Open()
                Dim dr2 As SqlDataReader
                dr2 = cmd.ExecuteReader()
                While dr2.Read
                    txtSN_Start.Text = dr2("SN").ToString.Trim
                End While
                dr2.Close()
                cmd.Dispose()
                conn.Close()
                '--------------------
                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("spGetCarrierSN_End_byWO'" & Me.txtWO.Text.Trim & "," & CInt(Me.txtQty.Text.Trim) & "'", conn)
                conn.Open()
                Dim dr3 As SqlDataReader
                dr3 = cmd.ExecuteReader
                While dr3.Read
                    Me.txtSN_end.Text = dr3("SN").ToString.Trim
                End While
                dr3.Close()
                cmd.Dispose()
                conn.Close()
            Else


                Dim conn1 As SqlConnection
                Dim fixdigit As String = "PDCUH"
                conn1 = New SqlConnection(Strconn)
                cmd = New SqlCommand("sptblCarrierSN_Count_byDatecode1'" & txtDatecode.Text.Trim & "'", conn1) ' Ket noi vao datatable cua Store Procedure
                conn1.Open()
                Dim dr1 As SqlDataReader
                dr1 = cmd.ExecuteReader()
                While dr1.Read
                    Dim count As Integer = CInt(dr1("").ToString.Trim)
                    Dim a As String = Hex(count)
                    ' Dim count1 As Integer = count + 1

                    'MsgBox(Len(count1.ToString.Trim))
                    If Len(a) = 1 Then
                        txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "000" & a
                    ElseIf Len(a) = 2 Then
                        txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "00" & a
                    ElseIf Len(a) = 3 Then
                        txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "0" & a
                    ElseIf Len(a) = 4 Then
                        txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & a
                    End If

                    Dim countplus As Integer = count + CInt(txtQty.Text.Trim) - 1
                    Dim b As String = Hex(countplus)

                    If Len(b) = 1 Then
                        txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "000" & b
                    ElseIf Len(b) = 2 Then
                        txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "00" & b
                    ElseIf Len(b) = 3 Then
                        txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "0" & b
                    ElseIf Len(b) = 4 Then
                        txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & b
                    End If


                End While
                dr1.Close()
                cmd.Dispose()
                conn1.Close()


            End If
        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()




    End Sub
    Private Sub CreateSN_Eimez()

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblEimezSN_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader()
        While dr.Read
            If CInt(dr("").ToString.Trim) > 0 Then
                'If CInt(dr("").ToString.Trim) >= CInt(txtQty.Text.Trim) Then
                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("spGetEimezSN_Start_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
                conn.Open()
                Dim dr2 As SqlDataReader
                dr2 = cmd.ExecuteReader()
                While dr2.Read
                    txtSN_Start.Text = dr2("SN").ToString.Trim
                End While
                dr2.Close()
                cmd.Dispose()
                conn.Close()
                '--------------------
                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("spGetEimezSN_End_byWO'" & Me.txtWO.Text.Trim & "," & CInt(Me.txtQty.Text.Trim) & "'", conn)
                conn.Open()
                Dim dr3 As SqlDataReader
                dr3 = cmd.ExecuteReader
                While dr3.Read
                    Me.txtSN_end.Text = dr3("SN").ToString.Trim
                End While
                dr3.Close()
                cmd.Dispose()
                conn.Close()
            Else

                Dim conn1 As SqlConnection
                Dim fixdigit As String = "PPCUH"
                conn1 = New SqlConnection(Strconn)
                cmd = New SqlCommand("sptblEimezSN_Count_byDatecode1'" & txtDatecode.Text.Trim & "'", conn1) ' Ket noi vao datatable cua Store Procedure
                '' lay count cua tuan hien tai
                conn1.Open()
                Dim dr1 As SqlDataReader
                dr1 = cmd.ExecuteReader()
                While dr1.Read
                    Dim count As Integer = CInt(dr1("").ToString.Trim)
                    Dim a As String = Hex(count)
                    ' Dim count1 As Integer = count + 1

                    'MsgBox(Len(count1.ToString.Trim))
                    If Len(a) = 1 Then
                        txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "000" & a
                    ElseIf Len(a) = 2 Then
                        txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "00" & a
                    ElseIf Len(a) = 3 Then
                        txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & "0" & a
                    ElseIf Len(a) = 4 Then
                        txtSN_Start.Text = fixdigit & txtDatecode.Text.Trim & a
                    End If

                    Dim countplus As Integer = count + CInt(txtQty.Text.Trim) - 1
                    Dim b As String = Hex(countplus)

                    If Len(b) = 1 Then
                        txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "000" & b
                    ElseIf Len(b) = 2 Then
                        txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "00" & b
                    ElseIf Len(b) = 3 Then
                        txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & "0" & b
                    ElseIf Len(b) = 4 Then
                        txtSN_end.Text = fixdigit & txtDatecode.Text.Trim & b
                    End If


                End While
                dr1.Close()
                cmd.Dispose()
                conn1.Close()


            End If
        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()




    End Sub
    Private Sub Get_Start_End_Mac()

        If RadioButton1.Checked = True Then
            If txtPN.Text.Trim.IndexOf("02-") >= 0 Then
                ' Get start_Mac#

                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("spGetPCBA_Mac_Start'" & "A" & "'", conn)
                conn.Open()
                Dim dr1 As SqlDataReader
                dr1 = cmd.ExecuteReader
                While dr1.Read
                    Me.TextBox7.Text = dr1("Mac#").ToString.Trim
                End While
                dr1.Close()
                cmd.Dispose()
                conn.Close()

                ' Get End_Mac#
                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("spGetPCBA_Mac_End'" & Me.txtQty.Text.Trim & "'", conn)
                conn.Open()
                Dim dr2 As SqlDataReader
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    Me.TextBox8.Text = dr2("Mac#").ToString.Trim
                End While
                dr2.Close()
                cmd.Dispose()
                conn.Close()
            ElseIf txtPN.Text.Trim.IndexOf("83-") >= 0 Then
                ' Get start_Mac#
                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("spGetBOXBUILD_Mac_Start'" & "B" & "','" & txtPCBA_WO.Text.Trim & "'", conn)
                conn.Open()
                Dim dr1 As SqlDataReader
                dr1 = cmd.ExecuteReader
                While dr1.Read
                    Me.TextBox7.Text = dr1("Mac#").ToString.Trim
                End While
                dr1.Close()
                cmd.Dispose()
                conn.Close()

                ' Get End_Mac#
                conn = New SqlConnection(Strconn)
                cmd = New SqlCommand("spGetBOXBUILD_Mac_End'" & Me.txtQty.Text.Trim & "','" & txtPCBA_WO.Text.Trim & "'", conn)
                conn.Open()
                Dim dr2 As SqlDataReader
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    Me.TextBox8.Text = dr2("Mac#").ToString.Trim
                End While
                dr2.Close()
                cmd.Dispose()
                conn.Close()
            ElseIf txtPN.Text.Trim.IndexOf("80-") >= 0 Then

            Else

            End If

        End If


    End Sub
    Private Sub Load_Mac_Range()
        If RadioButton1.Checked = True Then
            If txtPN.Text.Trim.IndexOf("02-") >= 0 Then

                Try
                    conn = New SqlConnection(Strconn)
                    Me.StrSql = "sptblCrusher_MAC_Available_Load_PCBA'" & Me.TextBox7.Text.Trim & "'" & "," & "'" & Me.TextBox8.Text.Trim & "'"
                    Dim cmd As New SqlCommand(Me.StrSql, conn)
                    da = New SqlDataAdapter(cmd)
                    Dim cmb As New SqlCommandBuilder(da)
                    ds = New DataSet
                    da.Fill(ds, "tblCrusher_MAC")
                    conn.Close()
                    Dim Dtv As DataView = New DataView(ds.Tables("tblCrusher_MAC"))
                    With DataGridView3
                        .DataSource = Dtv
                        .Rows(0).Selected = False
                        .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                        .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                    End With
                Catch ex As SqlClient.SqlException
                    MsgBox(ex.Message & StrSql)
                End Try

            ElseIf txtPN.Text.Trim.IndexOf("83-") >= 0 Then
                Try
                    conn = New SqlConnection(Strconn)
                    Me.StrSql = "sptblCrusher_MAC_Available_Load_BOXBUILD'" & Me.TextBox7.Text.Trim & "'" & "," & "'" & Me.TextBox8.Text.Trim & "'"
                    Dim cmd As New SqlCommand(Me.StrSql, conn)
                    da = New SqlDataAdapter(cmd)
                    Dim cmb As New SqlCommandBuilder(da)
                    ds = New DataSet
                    da.Fill(ds, "tblCrusher_MAC")
                    conn.Close()
                    Dim Dtv As DataView = New DataView(ds.Tables("tblCrusher_MAC"))
                    With DataGridView3
                        .DataSource = Dtv
                        .Rows(0).Selected = False
                        .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                        .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                    End With
                Catch ex As SqlClient.SqlException
                    MsgBox(ex.Message & StrSql)
                End Try
            Else

            End If



            MsgBox("PLEASE CLICK TO BUTTON 'CREATE SN TABLE' TO MATCH WITH MAC# ")


        End If

    End Sub

    ' ham này là hàm sẽ lưu lại những mac ở list bên cạnh với các sn ở bảng đầu tiên
    Private Sub tblCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click

        ' Check the BOM revision is DVT of MP

        Dim Rev As String = Me.txtRev.Text.Trim

        If IsNumeric(Microsoft.VisualBasic.Left(Rev, 1)) = True Then

            Select Case MsgBox("THE BOM REVISION OF THIS WO IS NOT APPLIED FOR MASS PRODUCTION. DO YOU WANT TO DO CONTINUE TO CREATE THE SN? OR YOU SHOULD TO STOP TO CONFIRM WITH YOUR SUPERVISOR", MsgBoxStyle.YesNo, "IF YES. THE SYSTEM WILL CONTINUE. IF NO, THE SYSTEM WULL STOP. PLEASE CONFIRM")
                Case MsgBoxResult.Yes
                    If txtPN.Text.Trim.IndexOf("80-") = 1 Or txtPN.Text.Trim.IndexOf("80-") = 0 Or RadioButton5.Checked = True Then
                        Select Case MsgBox("PLEASE CONFIRM IF DEVIATION IS ASSIGNED FOR THIS WO?", MsgBoxStyle.YesNo, "PLEASE CONFIRM")
                            Case MsgBoxResult.Yes
                                txtDEV.Visible = True
                                If Me.txtDEV.Text = "" Then
                                    MsgBox("PLEASE INPUT DEV NUMBER TO BOX")
                                    txtDEV.Focus()
                                Else

                                End If
                            Case MsgBoxResult.No
                                txtDEV.Clear()
                                txtDEV.Visible = False

                        End Select
                        ''Hunter add new for carrier
                    ElseIf RadioButton5.Checked = True Then
                        Select Case MsgBox("PLEASE CONFIRM IF DEVIATION IS ASSIGNED FOR THIS WO?", MsgBoxStyle.YesNo, "PLEASE CONFIRM")
                            Case MsgBoxResult.Yes
                                txtDEV.Visible = True
                                If Me.txtDEV.Text = "" Then
                                    MsgBox("PLEASE INPUT DEV NUMBER TO BOX")
                                    txtDEV.Focus()
                                Else

                                End If
                            Case MsgBoxResult.No
                                txtDEV.Clear()
                                txtDEV.Visible = False

                                If txtQty.Text.Trim <> "" And txtSN_Start.Text.Trim <> "" Then
                                    CreateSN_List_Eimez()
                                Else
                                    MsgBox("MISSING SOME INFORMATION. NEED TO CONFIRM AGAIN")
                                End If

                        End Select
                    ElseIf RadioButton2.Checked = True Then


                        Select Case MsgBox("PLEASE CONFIRM IF DEVIATION IS ASSIGNED FOR THIS WO?", MsgBoxStyle.YesNo, "PLEASE CONFIRM")
                            Case MsgBoxResult.Yes
                                txtDEV.Visible = True
                                If Me.txtDEV.Text = "" Then
                                    MsgBox("PLEASE INPUT DEV NUMBER TO BOX")
                                    txtDEV.Focus()
                                Else
                                    CreateSN_List_Eimez()
                                End If
                            Case MsgBoxResult.No
                                txtDEV.Clear()
                                txtDEV.Visible = False

                                If txtQty.Text.Trim <> "" And txtSN_Start.Text.Trim <> "" Then
                                    CreateSN_List_Eimez()
                                Else
                                    MsgBox("MISSING SOME INFORMATION. NEED TO CONFIRM AGAIN")
                                End If

                        End Select
                    Else

                        Select Case MsgBox("PLEASE CONFIRM IF DEVIATION IS ASSIGNED FOR THIS WO?", MsgBoxStyle.YesNo, "PLEASE CONFIRM")
                            Case MsgBoxResult.Yes
                                txtDEV.Visible = True
                                If Me.txtDEV.Text = "" Then
                                    MsgBox("PLEASE INPUT DEV NUMBER TO BOX")
                                    txtDEV.Focus()
                                Else
                                    Combine_Mac()
                                End If
                            Case MsgBoxResult.No
                                txtDEV.Clear()
                                txtDEV.Visible = False

                                If txtQty.Text.Trim <> "" And txtSN_Start.Text.Trim <> "" Then
                                    Combine_Mac()
                                Else
                                    MsgBox("MISSING SOME INFORMATION. NEED TO CONFIRM AGAIN")
                                End If

                        End Select


                    End If

                Case MsgBoxResult.No

            End Select

        Else

            If txtPN.Text.Trim.IndexOf("80-") = 0 Or RadioButton5.Checked = True Then
                Select Case MsgBox("PLEASE CONFIRM IF DEVIATION IS ASSIGNED FOR THIS WO?", MsgBoxStyle.YesNo, "PLEASE CONFIRM")
                    Case MsgBoxResult.Yes
                        txtDEV.Visible = True
                        If Me.txtDEV.Text = "" Then
                            MsgBox("PLEASE INPUT DEV NUMBER TO BOX")
                            txtDEV.Focus()
                        Else

                        End If
                    Case MsgBoxResult.No
                        txtDEV.Clear()
                        txtDEV.Visible = False

                End Select
                ''Hunter add new for carrier
            ElseIf RadioButton2.Checked = True Then ''sinh SN  của eimezz bat dau tu doan nay


                Select Case MsgBox("PLEASE CONFIRM IF DEVIATION IS ASSIGNED FOR THIS WO?", MsgBoxStyle.YesNo, "PLEASE CONFIRM")
                    Case MsgBoxResult.Yes
                        txtDEV.Visible = True
                        If Me.txtDEV.Text = "" Then
                            MsgBox("PLEASE INPUT DEV NUMBER TO BOX")
                            txtDEV.Focus()
                        Else
                            CreateSN_List_Eimez()
                        End If
                    Case MsgBoxResult.No
                        txtDEV.Clear()
                        txtDEV.Visible = False

                        If txtQty.Text.Trim <> "" And txtSN_Start.Text.Trim <> "" Then
                            CreateSN_List_Eimez()
                        Else
                            MsgBox("MISSING SOME INFORMATION. NEED TO CONFIRM AGAIN")
                        End If

                End Select
            Else

                Select Case MsgBox("PLEASE CONFIRM IF DEVIATION IS ASSIGNED FOR THIS WO?", MsgBoxStyle.YesNo, "PLEASE CONFIRM")
                    Case MsgBoxResult.Yes
                        txtDEV.Visible = True
                        If Me.txtDEV.Text = "" Then
                            MsgBox("PLEASE INPUT DEV NUMBER TO BOX")
                            txtDEV.Focus()
                        Else
                            Combine_Mac()
                        End If
                    Case MsgBoxResult.No
                        txtDEV.Clear()
                        txtDEV.Visible = False

                        If txtQty.Text.Trim <> "" And txtSN_Start.Text.Trim <> "" Then
                            Combine_Mac()
                        Else
                            MsgBox("MISSING SOME INFORMATION. NEED TO CONFIRM AGAIN")
                        End If

                End Select


            End If

        End If





    End Sub
    Private Sub createSN_List()
        If RadioButton2.Checked = True Then
            '' neu la hang eimez

            'End If
            'If txtPN.Text.IndexOf("02-0114") = 0 Then


            ' CreateSN_List_Eimez()

        ElseIf RadioButton1.Checked = True Then
            If txtPN.Text.Trim.IndexOf("02-") >= 0 Or txtPN.Text.Trim.IndexOf("83-") >= 0 Then
                CreateSN_List_PCBA_BOXBUILD()
                'Lay so MAC_Start_End
                Get_Start_End_Mac()
                'Load danh sach MAC can dung
                Load_Mac_Range()
            ElseIf txtPN.Text.Trim.IndexOf("80-") >= 0 Then
                CreateSN_List_FG()
            End If

        ElseIf RadioButton5.Checked = True Then
            CreateSN_List_Carrier()
        End If
    End Sub
    Private Sub CreateSN_List_PCBA_BOXBUILD()
        TextBox2.Text = Convert.ToInt32((Microsoft.VisualBasic.Right(txtSN_Start.Text.Trim, 4)), 16)
        TextBox3.Text = Convert.ToInt32((Microsoft.VisualBasic.Right(txtSN_end.Text.Trim, 4)), 16)

        For i As Integer = CInt(TextBox2.Text.Trim) To CInt(TextBox3.Text.Trim)
            TextBox4.Text = i
            Dim b As String = Hex(i)
            Dim fixdigit As String

            fixdigit = "PFMUH"
            conn = New SqlConnection(Strconn)
            Dim cmd As New SqlCommand

            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO tblCrusherSN_Log (Date,WO,Week,SN,Sequence,PN) VALUES(@Date,@WO,@Week,@SN,@Sequence,@PN)"
            cmd.Parameters.AddWithValue("@WO", txtWO.Text.Trim)
            cmd.Parameters.AddWithValue("@PN", Microsoft.VisualBasic.Right(txtPN.Text.Trim, 10))
            cmd.Parameters.AddWithValue("@Date", txtDate.Text.ToString.Trim)
            cmd.Parameters.AddWithValue("@Week", txtDatecode.Text.Trim)

            If Len(b) = 1 Then
                cmd.Parameters.AddWithValue("@sequence", i)
                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "000" & b)
            ElseIf Len(b) = 2 Then
                cmd.Parameters.AddWithValue("@sequence", i)
                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "00" & b)
            ElseIf Len(b) = 3 Then
                cmd.Parameters.AddWithValue("@sequence", i)
                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "0" & b)
            ElseIf Len(b) = 4 Then
                cmd.Parameters.AddWithValue("@sequence", i)
                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & b)

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
        LoadCrusherSN_log()

    End Sub
    Private Sub CreateSN_List_FG()
        TextBox2.Text = Convert.ToInt32((Microsoft.VisualBasic.Right(txtSN_Start.Text.Trim, 4)), 16)
        TextBox3.Text = Convert.ToInt32((Microsoft.VisualBasic.Right(txtSN_end.Text.Trim, 4)), 16)
        'TextBox2.Text = CInt(Microsoft.VisualBasic.Right(txtSN_Start.Text.Trim, 4))
        'TextBox3.Text = CInt(Microsoft.VisualBasic.Right(txtSN_end.Text.Trim, 4))

        For i As Integer = CInt(TextBox2.Text.Trim) To CInt(TextBox3.Text.Trim)
            Dim b As String = Hex(i)
            Dim fixdigit As String

            fixdigit = "PSPUH"
            conn = New SqlConnection(Strconn)
            Dim cmd As New SqlCommand

            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO tblCrusherSN_Log_FG (Date,WO,Week,SN,Sequence,PN) VALUES(@Date,@WO,@Week,@SN,@Sequence,@PN)"
            cmd.Parameters.AddWithValue("@WO", txtWO.Text.Trim)
            cmd.Parameters.AddWithValue("@PN", txtPN.Text.Trim)
            cmd.Parameters.AddWithValue("@Date", txtDate.Text.ToString.Trim)
            cmd.Parameters.AddWithValue("@Week", txtDatecode.Text.Trim)

            If Len(b) = 1 Then
                cmd.Parameters.AddWithValue("@sequence", i)
                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "000" & b)
            ElseIf Len(b) = 2 Then
                cmd.Parameters.AddWithValue("@sequence", i)
                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "00" & b)
            ElseIf Len(b) = 3 Then
                cmd.Parameters.AddWithValue("@sequence", i)
                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "0" & b)
            ElseIf Len(b) = 4 Then
                cmd.Parameters.AddWithValue("@sequence", i)
                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & b)

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

        LoadCrusherSN_FG_log()
    End Sub
    Private Sub CreateSN_List_Carrier()

        ''
        TextBox2.Text = Convert.ToInt32((Microsoft.VisualBasic.Right(txtSN_Start.Text.Trim, 4)), 16)
        TextBox3.Text = Convert.ToInt32((Microsoft.VisualBasic.Right(txtSN_end.Text.Trim, 4)), 16)
        'TextBox2.Text = CInt(Microsoft.VisualBasic.Right(txtSN_Start.Text.Trim, 4))
        'TextBox3.Text = CInt(Microsoft.VisualBasic.Right(txtSN_end.Text.Trim, 4))

        For i As Integer = CInt(TextBox2.Text.Trim) To CInt(TextBox3.Text.Trim)
            Dim b As String = Hex(i)
            Dim fixdigit As String

            fixdigit = "PDCUH"
            conn = New SqlConnection(Strconn)
            Dim cmd As New SqlCommand

            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO tblCarrierSN_Log (Date,WO,Week,SN,Sequence,PN) VALUES(@Date,@WO,@Week,@SN,@Sequence,@PN)"
            cmd.Parameters.AddWithValue("@WO", txtWO.Text.Trim)
            cmd.Parameters.AddWithValue("@PN", txtPN.Text.Trim)
            cmd.Parameters.AddWithValue("@Date", txtDate.Text.ToString.Trim)
            cmd.Parameters.AddWithValue("@Week", txtDatecode.Text.Trim)

            Dim rev As String
            rev = Trim(Microsoft.VisualBasic.Replace(txtRev.Text.Trim, ".", ""))


            If Len(b) = 1 Then
                cmd.Parameters.AddWithValue("@sequence", i)

                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "000" & b & ">" & txtPN.Text.Trim)
            ElseIf Len(b) = 2 Then
                cmd.Parameters.AddWithValue("@sequence", i)
                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "00" & b & ">" & txtPN.Text.Trim)
            ElseIf Len(b) = 3 Then
                cmd.Parameters.AddWithValue("@sequence", i)
                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "0" & b & ">" & txtPN.Text.Trim)
            ElseIf Len(b) = 4 Then
                cmd.Parameters.AddWithValue("@sequence", i)
                cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & b & ">" & txtPN.Text.Trim)

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


        LoadCarrierSN_log()
    End Sub
    Private Sub CreateSN_List_Eimez()


        TextBox2.Text = Convert.ToInt32((Microsoft.VisualBasic.Right(txtSN_Start.Text.Trim, 4)), 16)
        TextBox3.Text = Convert.ToInt32((Microsoft.VisualBasic.Right(txtSN_end.Text.Trim, 4)), 16)
        'TextBox2.Text = CInt(Microsoft.VisualBasic.Right(txtSN_Start.Text.Trim, 4))
        'TextBox3.Text = CInt(Microsoft.VisualBasic.Right(txtSN_end.Text.Trim, 4))
        Dim SN As String
        For i As Integer = CInt(TextBox2.Text.Trim) To CInt(TextBox3.Text.Trim)
            Dim b As String = Hex(i)
            Dim fixdigit As String

            fixdigit = "PPCUH"
            Dim PN As String
            PN = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Replace(txtPN.Text.Trim, "-", ""), 8)
            If txtPN.Text.Trim.Contains("HLOB") Then
                PN = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Replace(txtPN.Text.Trim, "-", "").Replace("HLOB", ""), 8)
            End If


            conn = New SqlConnection(Strconn)
            Dim cmd As New SqlCommand

            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO tblEimezSN_Log (Date,WO,Week,SN,Sequence,PN) VALUES(@Date,@WO,@Week,@SN,@Sequence,@PN)"
            cmd.Parameters.AddWithValue("@WO", txtWO.Text.Trim)
            ''cmd.Parameters.AddWithValue("@PN", txtPN.Text.Trim) '' Old code
            cmd.Parameters.AddWithValue("@PN", PN)
            cmd.Parameters.AddWithValue("@Date", txtDate.Text.ToString.Trim)
            cmd.Parameters.AddWithValue("@Week", txtDatecode.Text.Trim)

            Dim rev As String
            rev = Trim(txtRev.Text.Trim)


            If txtDEV.Text.Trim = "" Then
                If Len(b) = 1 Then
                    cmd.Parameters.AddWithValue("@sequence", i)

                    cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "000" & b & ">" & PN & rev)
                ElseIf Len(b) = 2 Then
                    cmd.Parameters.AddWithValue("@sequence", i)
                    cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "00" & b & ">" & PN & rev)
                ElseIf Len(b) = 3 Then
                    cmd.Parameters.AddWithValue("@sequence", i)
                    cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "0" & b & ">" & PN & rev)
                ElseIf Len(b) = 4 Then
                    If PN.Contains("HLOB") Then
                        PN = Microsoft.VisualBasic.Replace(txtPN.Text.Trim, "-", "").Replace("HLOB", "")
                        cmd.Parameters.AddWithValue("@sequence", i)
                        cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & b & ">" & PN & rev)
                    Else
                        cmd.Parameters.AddWithValue("@sequence", i)
                        cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & b & ">" & PN & rev)
                    End If

                End If
                ElseIf Len(txtDEV.Text.Trim) = 7 Then
                Dim DEV As String
                DEV = txtDEV.Text.Trim

                If Len(b) = 1 Then
                    cmd.Parameters.AddWithValue("@sequence", i)

                    cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "000" & b & ">" & PN & rev & ">" & DEV)
                    SN = fixdigit & txtDatecode.Text.Trim & "000" & b & ">" & PN & rev & ">" & DEV
                ElseIf Len(b) = 2 Then
                    cmd.Parameters.AddWithValue("@sequence", i)
                    cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "00" & b & ">" & PN & rev & ">" & DEV)
                    SN = fixdigit & txtDatecode.Text.Trim & "00" & b & ">" & PN & rev & ">" & DEV
                ElseIf Len(b) = 3 Then
                    cmd.Parameters.AddWithValue("@sequence", i)
                    cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & "0" & b & ">" & PN & rev & ">" & DEV)
                    SN = fixdigit & txtDatecode.Text.Trim & "0" & b & ">" & PN & rev & ">" & DEV
                ElseIf Len(b) = 4 Then
                    cmd.Parameters.AddWithValue("@sequence", i)
                    cmd.Parameters.AddWithValue("@SN", fixdigit & txtDatecode.Text.Trim & b & ">" & PN & rev & ">" & DEV)
                    SN = fixdigit & txtDatecode.Text.Trim & b & ">" & PN & rev & ">" & DEV

                End If
                If (SN = "") Then
                    '' do no thing
                End If

            End If



            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()

            Catch ex As SqlException
                MsgBox(ex.ToString())
                conn.Close()

            End Try
        Next





        LoadEimezSN_log()
    End Sub
    Private Sub LoadCrusherSN_log()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblCrusherSN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblCrusherSN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblCrusherSN_Log"))
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
    Private Sub LoadCrusherSN_FG_log()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblCrusherSN_FG_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblCrusherSN_Log_FG")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblCrusherSN_Log_FG"))
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
    Private Sub LoadCarrierSN_log()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblCarrierSN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblCarrierSN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblCarrierSN_Log"))
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
    Private Sub LoadEimezSN_log()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblEimezSN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblEimezSN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblEimezSN_Log"))
            With DataGridView2
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                .Columns(0).Width = 50
            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try


    End Sub
    Private Sub Combine_Mac()


        Dim col As New DataGridViewTextBoxColumn
        Dim col1 As New DataGridViewTextBoxColumn
        Dim col2 As New DataGridViewTextBoxColumn
        col.DataPropertyName = "Combine"
        col.HeaderText = "Combine"
        col.Name = "Combine"
        col1.DataPropertyName = "Name"
        col1.HeaderText = "Name"
        col1.Name = "Name"
        col2.DataPropertyName = "Mac_add"
        col2.HeaderText = "Mac_add"
        col2.Name = "Mac_add"
        DataGridView2.Columns.Add(col)
        DataGridView2.Columns.Add(col1)
        DataGridView2.Columns.Add(col2)

        Dim i As Integer

        For i = 0 To DataGridView2.Rows.Count - 2

            Dim SN As String = Trim(DataGridView2.Rows(i).Cells("SN").Value)
            Dim PN As String = Microsoft.VisualBasic.Right(Trim(Replace(DataGridView2.Rows(i).Cells("PN").Value, "-", "")), 8)
            Dim Rev As String = txtRev.Text.Trim

            If txtPN.Text.Trim.IndexOf("02-") >= 0 Or txtPN.Text.Trim.IndexOf("83-") >= 0 Then
                Dim Mac As String = Trim(DataGridView3.Rows(i).Cells("Mac#").Value)
                DataGridView2.Rows(i).Cells("Combine").Value = Trim(SN & ">" & PN & Rev & ">" & Mac)

            Else
                DataGridView2.Rows(i).Cells("Combine").Value = Trim(SN)

            End If


            Dim Modelname As String = txtdesc.Text.Trim.ToUpper

            If Modelname.IndexOf("CRUSHER") >= 0 Then

                DataGridView2.Rows(i).Cells("Name").Value = "CRUSHER"

            ElseIf Modelname.IndexOf("DOVER") >= 0 Then
                DataGridView2.Rows(i).Cells("Name").Value = "DOVER"
            ElseIf Modelname.IndexOf("SLOVER") >= 0 Then
                DataGridView2.Rows(i).Cells("Name").Value = "SLOVER"
            ElseIf Modelname.IndexOf("EIGER") >= 0 Then
                DataGridView2.Rows(i).Cells("Name").Value = "EIGER"
            ElseIf Modelname.IndexOf("FRESHER") >= 0 Then
                DataGridView2.Rows(i).Cells("Name").Value = "FRESHER"
            ElseIf Modelname.IndexOf("MEZZ") >= 0 Then
                DataGridView2.Rows(i).Cells("Name").Value = "MEZZ"
            ElseIf Modelname.IndexOf("GROVER") >= 0 Then
                DataGridView2.Rows(i).Cells("Name").Value = "GROVER"
            ElseIf Modelname.IndexOf("HOPPER") >= 0 Then
                DataGridView2.Rows(i).Cells("Name").Value = "HOPPER"
            ElseIf Modelname.IndexOf("ZEPTO") >= 0 Then

                DataGridView2.Rows(i).Cells("Name").Value = "ZEPTO"

            ElseIf Modelname.IndexOf("ICER") >= 0 Then

                DataGridView2.Rows(i).Cells("Name").Value = "ICER"
            Else

                DataGridView2.Rows(i).Cells("Name").Value = "Pure Storage SN"
            End If

            DataGridView2.Rows(i).Cells("Mac_add").Value = Microsoft.VisualBasic.Right(DataGridView2.Rows(i).Cells("Combine").Value, 16).Trim

        Next

        If txtPN.Text.Trim.IndexOf("02-") = 0 Or txtPN.Text.Trim.IndexOf("02-") = 1 Or txtPN.Text.Trim.Contains("HLOB-02") Then
            UpdateMAC_PCBA_Status()
            PCBA_SN_Log() 'luu log insert 02 

        ElseIf txtPN.Text.Trim.IndexOf("83-") = 0 Or txtPN.Text.Trim.IndexOf("83-") = 1 Or txtPN.Text.Trim.IndexOf("HLOB-83") = 0 Then
            UpdateMAC_BOXBUILD_Status()
            BOXBUILD_SN_Log()
        End If
        DataGridView3.DataSource = ""
        DataGridView3.Columns.Clear()
        MsgBox("PLEASE CLICK TO 'GET SN LIST' BUTTON TO PRINT THE LIST OF SN")
    End Sub
    Private Sub UpdateMAC_PCBA_Status()
        '  If RadioButton1.Checked = True Then
        Dim conn As SqlConnection = New SqlConnection(Strconn)
        Dim j As Integer
        For j = 0 To DataGridView3.RowCount - 1
            ' For Each row As DataGridViewRow In DataGridView1.Rows
            cmd = New SqlCommand("sptblCrusher_MAC_Update", conn) ' Ket noi vao datatable cua Store Procedure
            cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

            cmd.Parameters.AddWithValue("Mac#", Trim(DataGridView3.Rows(j).Cells("Mac#").Value))
            cmd.Parameters.AddWithValue("PCBA_WO", Me.txtWO.Text.Trim)

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()

            Catch ex As SqlException
                conn.Close()
                'MessageBox.Show("Can not insert Mac#.")
            End Try

        Next

        conn.Close()

        ALERT_NEW_MAC_RANGE_USED()
        Count_MAC_available()
        Load_Mac_available()

    End Sub
    Private Sub ALERT_NEW_MAC_RANGE_USED()


        '''ALERT THE FAIR FOR NEW MAC_RANGE USED AND SEND EMAIL

        Dim k As Integer, StartMAC_range As String, PCBA_WO As String


        For k = 0 To DataGridView6.RowCount() - 2

            StartMAC_range = DataGridView6.Rows(k).Cells(5).Value.ToString.Trim
            PCBA_WO = txtWO.Text.ToString.Trim

            Dim conn As New SqlConnection(Strconn)
            Dim sqladap = New SqlDataAdapter("spCheck_New_MAC_range_Used" & "'" & StartMAC_range & "','" & PCBA_WO & "'", conn)
            Dim tb As New Data.DataTable()
            Try
                conn.Open()
                sqladap.Fill(tb)

            Catch ex As Exception
                conn.Close()
            End Try

            If CInt(tb.Rows.Count) >= 1 Then

                Dim Email As String
                Email = ""
                conn = New SqlConnection(Strconn)
                conn.Open()
                Dim da = New SqlDataAdapter("spGetEmail", conn)
                Dim tb1 As Data.DataTable = New Data.DataTable
                da.Fill(tb1)
                conn.Close()
                Email = tb1.Rows(0).Item("Email").ToString()
                conn.Close()

                Dim send As String = ConfigurationManager.AppSettings("App_Email").ToString()
                Dim pw As String = ConfigurationManager.AppSettings("App_Email_Keys").ToString()
                Dim Smtp_Server As New SmtpClient
                Dim e_mail As New MailMessage()

                Try
                    Smtp_Server.UseDefaultCredentials = False
                    Smtp_Server.Credentials = New Net.NetworkCredential(send, pw)
                    Smtp_Server.Port = 587
                    Smtp_Server.EnableSsl = True
                    Smtp_Server.Host = "smtp.gmail.com"

                    e_mail = New MailMessage()
                    e_mail.From = New MailAddress(send)
                    e_mail.To.Add(Email) 'dang xem o day

                    e_mail.Subject = "PLEASE MAKE FAIR REPORT FOR NEW MAC_RANGE USED" & DateString
                    e_mail.IsBodyHtml = False
                    e_mail.Body = "WO: " & txtWO.Text.Trim & "  |  " &
                  "ALLOCATION_ID" & DataGridView6.Rows(k).Cells(1).Value & "  |  " &
                  "MIN_MAC#:" & DataGridView6.Rows(k).Cells(5).Value & "  |  " &
                  "MAX_MAC#:" & DataGridView6.Rows(k).Cells(6).Value & "  |  " & vbCrLf & vbCrLf & "--------------------------------------------------" & vbCrLf & "This email notification was generated automatically, please do not reply"
                    Smtp_Server.Send(e_mail)

                Catch error_t As Exception
                    Smtp_Server.UseDefaultCredentials = False
                    Smtp_Server.Credentials = New Net.NetworkCredential(send, pw)
                    Smtp_Server.Port = 587
                    Smtp_Server.EnableSsl = True
                    Smtp_Server.Host = "smtp.gmail.com"
                    e_mail = New MailMessage()
                    e_mail.From = New MailAddress(send)
                    e_mail.To.Add(Email) 'dang xem o day
                    e_mail.Subject = "Alert : Error Sending"
                    e_mail.IsBodyHtml = False
                    e_mail.Body = error_t.ToString
                    Smtp_Server.Send(e_mail)
                End Try


                Exit For

                Me.Close()
            Else

            End If



        Next


    End Sub

    Private Sub UpdateMAC_BOXBUILD_Status()

        Dim conn As SqlConnection = New SqlConnection(Strconn)
        Dim j As Integer
        For j = 0 To DataGridView3.RowCount - 2
            ' For Each row As DataGridViewRow In DataGridView1.Rows
            cmd = New SqlCommand("sptblCrusher_MAC_Update1", conn) ' Ket noi vao datatable cua Store Procedure
            cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

            cmd.Parameters.AddWithValue("Mac#", DataGridView3.Rows(j).Cells("Mac#").Value)
            cmd.Parameters.AddWithValue("BOXBUILD_WO", Me.txtWO.Text.Trim)

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()

            Catch ex As SqlException
                conn.Close()
                'MessageBox.Show("Can not insert Mac#.")
            End Try

        Next

        conn.Close()
        Count_MAC_available()
        Load_Mac_available()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnUploadMac.Click

        If lblID.Text.Trim = "" Then

            MsgBox("PLEASE FILL ALLOCATION_ID ")
        Else

            Dim conn As SqlConnection = New SqlConnection(Strconn)
            'Get range and insert to SQL table

            Dim StartMAC As String, EndMAC As String

            Dim max As Integer

            max = dgw.RowCount - 2

            StartMAC = dgw.Rows(0).Cells(0).Value
            EndMAC = dgw.Rows(max).Cells(0).Value

            txtStart_MAC.Text = StartMAC
            txtEnd_MAC.Text = EndMAC

            Dim StartMAC_Dec As Long, EndMAC_Dec As Long

            StartMAC_Dec = CLng("&H" & Microsoft.VisualBasic.Right(StartMAC, 10))
            EndMAC_Dec = CLng("&H" & Microsoft.VisualBasic.Right(EndMAC, 10))


            cmd = New SqlCommand("sptblMAC_RANGE_add", conn) ' Ket noi vao datatable cua Store Procedure
            cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure
            'cmd.Parameters.Add(New SqlParameter("@ID", SqlDbType.Int)).Value = TextBox10.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@ProductGroupID", SqlDbType.SmallInt)).Value = lblID.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@ProductGroup", SqlDbType.NVarChar, 40)).Value = "PFM"
            cmd.Parameters.Add(New SqlParameter("@MinWWNAddressDec", SqlDbType.BigInt)).Value = StartMAC_Dec
            cmd.Parameters.Add(New SqlParameter("@MaxWWNAddressDec", SqlDbType.BigInt)).Value = EndMAC_Dec
            cmd.Parameters.Add(New SqlParameter("@MinWWNAddress", SqlDbType.NVarChar, 50)).Value = StartMAC
            cmd.Parameters.Add(New SqlParameter("@MaxWWNAddress", SqlDbType.NVarChar, 50)).Value = EndMAC
            cmd.Parameters.Add(New SqlParameter("@Product_type", SqlDbType.NVarChar, 40)).Value = "PFM"
            cmd.Parameters.Add(New SqlParameter("@Active", SqlDbType.Bit)).Value = 1
            cmd.Parameters.Add(New SqlParameter("@OUI", SqlDbType.NVarChar, 20)).Value = Microsoft.VisualBasic.Left(StartMAC, 6)
            cmd.Parameters.Add(New SqlParameter("@SITE", SqlDbType.NVarChar, 20)).Value = "UHAN"
            cmd.Parameters.Add(New SqlParameter("@USER", SqlDbType.NVarChar, 50)).Value = LOGIN.txtUsername.Text.Trim

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
                'DataGridView1.DataSource = ""
            Catch ex As SqlException
                conn.Close()
                MessageBox.Show("Can not insert Mac_Range.")
            End Try




            Dim j As Integer
            For j = 0 To dgw.RowCount - 1


                ' For Each row As DataGridViewRow In DataGridView1.Rows
                cmd = New SqlCommand("sptblCrusher_MAC_add", conn) ' Ket noi vao datatable cua Store Procedure
                cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

                cmd.Parameters.Add(New SqlParameter("@Mac#", SqlDbType.NChar, 16)).Value = dgw.Rows(j).Cells("MacAddress").Value
                cmd.Parameters.Add(New SqlParameter("@PCBA_WO", SqlDbType.NChar, 7)).Value = "A"
                cmd.Parameters.Add(New SqlParameter("@BOXBUILD_WO", SqlDbType.NChar, 7)).Value = "B"

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()

                Catch ex As SqlException
                    conn.Close()
                    'MessageBox.Show("Can not insert Mac#.")
                End Try

            Next
            'conn.Close()
            Count_MAC_available()
            Load_Mac_available()

        End If

        GET_MAC_RANGE()

        dgw.DataSource = ""

    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs)
        'Use to paste data from excel to Gridview cell directly

        If e.Control AndAlso e.KeyCode = Keys.V Then

            Try
                For Each line As String In Clipboard.GetText.Split(vbNewLine)
                    Dim item() As String = line.Trim.Split(vbTab)
                    If item.Length = Me.dgw.ColumnCount Then
                        Me.dgw.Rows.Add(item)
                    End If
                Next

            Catch ex As Exception
                MessageBox.Show(ex.Message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        dgw.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    End Sub

    Private Sub tbnNew_Click(sender As Object, e As EventArgs) Handles tbnNew.Click
        DataGridView2.DataSource = ""
        DataGridView2.Columns.Clear()
        'DataGridView2.Columns.Remove("Combine")
        'DataGridView2.Columns.Remove("Name")
        'DataGridView2.Columns.Remove("Mac_add")
        dgw.DataSource = ""
        Cleartext()
        Load_Mac_available()
        Count_MAC_available()
    End Sub
    Public Sub EmptyTxt(ByVal Frm As Form)
        Dim Ctl As Control
        For Each Ctl In Frm.Controls
            If TypeOf Ctl Is TextBox Then Ctl.Text = ""
            If TypeOf Ctl Is GroupBox Then
                Dim Ctl1 As Control
                For Each Ctl1 In Ctl.Controls
                    If TypeOf Ctl1 Is TextBox Then
                        Ctl1.Text = ""
                    End If
                Next
            End If
        Next
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnGetSNList.Click
        ' If txtPN.Text.Trim.IndexOf("02-0114") = 0 Then
        If RadioButton2.Checked = True Then

            Eimez_SN_Export()
        ElseIf RadioButton1.Checked = True Then

            If txtPN.Text.Trim.IndexOf("02-") >= 0 Then

                PCBA_SN_Export()
                ''PCBA_SN_Export_Hunter()
            ElseIf txtPN.Text.Trim.IndexOf("83-") >= 0 Then

                BOXBUILD_SN_Export()

            ElseIf txtPN.Text.Trim.IndexOf("80-") >= 0 Then
                FG_SN_Export()
            End If

        ElseIf RadioButton5.Checked = True Then

            Carrier_SN_Export()
        End If
    End Sub
    Private Sub PCBA_SN_Log()
        Dim conn As SqlConnection = New SqlConnection(Strconn)
        Dim j As Integer
        For j = 0 To DataGridView2.RowCount - 2
            ' For Each row As DataGridViewRow In DataGridView1.Rows
            cmd = New SqlCommand("sptblCrusherSN_Log_PCBA_add", conn) ' Ket noi vao datatable cua Store Procedure
            cmd.CommandType = CommandType.StoredProcedure ' oi lenh store procedure

            cmd.Parameters.Add(New SqlParameter("@Date", SqlDbType.DateTime)).Value = DataGridView2.Rows(j).Cells(0).Value.ToString.Trim
            cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 7)).Value = DataGridView2.Rows(j).Cells(1).Value.ToString.Trim
            cmd.Parameters.Add(New SqlParameter("@Week", SqlDbType.NChar, 4)).Value = DataGridView2.Rows(j).Cells(2).Value.ToString.Trim
            cmd.Parameters.Add(New SqlParameter("@PN", SqlDbType.NChar, 12)).Value = Microsoft.VisualBasic.Right(DataGridView2.Rows(j).Cells(5).Value.ToString.Trim, 10)
            cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NVarChar, 200)).Value = DataGridView2.Rows(j).Cells(6).Value.ToString.Trim
            cmd.Parameters.Add(New SqlParameter("@Name", SqlDbType.NChar, 14)).Value = DataGridView2.Rows(j).Cells(7).Value.ToString.Trim
            cmd.Parameters.Add(New SqlParameter("@Mac_add", SqlDbType.NChar, 16)).Value = DataGridView2.Rows(j).Cells(8).Value.ToString.Trim

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()

            Catch ex As SqlException
                conn.Close()
                'MessageBox.Show("Can not insert Mac#.")
            End Try

        Next
        DataGridView2.DataSource = ""
        DataGridView2.Columns.Clear()
        'DataGridView2.Columns.Remove("Combine")
        'DataGridView2.Columns.Remove("Name")
        'DataGridView2.Columns.Remove("Mac_add")
        PCBA_SN_Log_Load()

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
        For i As Integer = 0 To DataGridView2.RowCount - 2
            dr1 = dset.Tables(0).NewRow
            If txtDEV.Text.Trim <> "" Then
                dr1(0) = Trim(DataGridView2.Rows(i).Cells(0).Value.ToString.Trim & ">" & txtDEV.Text.Trim)
                For j As Integer = 1 To DataGridView2.Columns.Count - 1
                    dr1(j) = Trim(DataGridView2.Rows(i).Cells(j).Value.ToString.Trim)
                Next
            Else
                For j As Integer = 0 To DataGridView2.Columns.Count - 1
                    dr1(j) = Trim(DataGridView2.Rows(i).Cells(j).Value.ToString.Trim)
                Next
            End If


            dset.Tables(0).Rows.Add(dr1)
        Next

        Dim excel As New Excel.Application()
        Dim wBook As Excel.Workbook
        Dim wSheet As Excel.Worksheet


        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        With wSheet
            Dim rng As Range

            rng = wSheet.Columns("B:B")
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

        Dim strFileName As String = "File.xlsx"
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

    Private Sub PCBA_SN_Export_Hunter()
        Try

            Dim dt As DataTable = CType(DataGridView2.DataSource, DataTable)
            Dim wb As New XLWorkbook()
            wb.Worksheets.Add(dt, "Sheet1")
            ''wb.SaveAs("Exported.xlsx")
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "Excel Workbook|*.xlsx"
            saveFileDialog.Title = "Save Excel File"
            saveFileDialog.FileName = "Exported.xlsx"

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    wb.SaveAs(saveFileDialog.FileName)
                    MessageBox.Show("Xuất file Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Lỗi khi lưu file: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub PCBA_SN_Log_Load()

        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblCrusherSN_Log_PCBA_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblCrusherSN_Log_PCBA")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblCrusherSN_Log_PCBA"))
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
    Private Sub PCBA_SN_Log_Load1()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblCrusherSN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblCrusherSN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblCrusherSN_Log"))
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
    Private Sub BOXBUILD_SN_Log()


        Dim conn As SqlConnection = New SqlConnection(Strconn)
        Dim j As Integer
        For j = 0 To DataGridView2.RowCount - 2
            ' For Each row As DataGridViewRow In DataGridView1.Rows
            cmd = New SqlCommand("sptblCrusherSN_Log_BOXBUILD_add", conn) ' Ket noi vao datatable cua Store Procedure
            cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

            cmd.Parameters.Add(New SqlParameter("@Date", SqlDbType.DateTime)).Value = DataGridView2.Rows(j).Cells(0).Value
            cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 7)).Value = DataGridView2.Rows(j).Cells(1).Value
            cmd.Parameters.Add(New SqlParameter("@Week", SqlDbType.NChar, 4)).Value = DataGridView2.Rows(j).Cells(2).Value
            cmd.Parameters.Add(New SqlParameter("@PN", SqlDbType.NChar, 12)).Value = Microsoft.VisualBasic.Right(DataGridView2.Rows(j).Cells(5).Value.ToString.Trim, 10)
            cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NVarChar, 200)).Value = DataGridView2.Rows(j).Cells(6).Value
            cmd.Parameters.Add(New SqlParameter("@Name", SqlDbType.NChar, 14)).Value = DataGridView2.Rows(j).Cells(7).Value
            cmd.Parameters.Add(New SqlParameter("@Mac_add", SqlDbType.NChar, 16)).Value = DataGridView2.Rows(j).Cells(8).Value

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()

            Catch ex As SqlException
                conn.Close()
                'MessageBox.Show("Can not insert Mac#.")
            End Try

        Next

        DataGridView2.DataSource = ""
        DataGridView2.Columns.Remove("Combine")
        DataGridView2.Columns.Remove("Name")
        DataGridView2.Columns.Remove("Mac_add")
        BOXBUILD_SN_Log_Load()
    End Sub
    Private Sub BOXBUILD_SN_Log_Load()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblCrusherSN_Log_BOXBUILD_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblCrusherSN_Log_BOXBUILD")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblCrusherSN_Log_BOXBUILD"))
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
    Private Sub BOXBUILD_SN_Log_Load1()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblCrusherSN_Log_Load'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblCrusherSN_Log")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblCrusherSN_Log"))
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

    Private Sub BOXBUILD_SN_Export()
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
        'For i As Integer = 0 To DataGridView2.RowCount - 1
        '    dr1 = dset.Tables(0).NewRow
        '    For j As Integer = 0 To DataGridView2.Columns.Count - 1
        '        dr1(j) = Trim(DataGridView2.Rows(i).Cells(j).Value)
        '    Next
        '    dset.Tables(0).Rows.Add(dr1)
        'Next
        For i As Integer = 0 To DataGridView2.RowCount - 2
            dr1 = dset.Tables(0).NewRow
            If txtDEV.Text.Trim <> "" Then
                dr1(0) = Trim(DataGridView2.Rows(i).Cells(0).Value.ToString.Trim & ">" & txtDEV.Text.Trim)
                For j As Integer = 1 To DataGridView2.Columns.Count - 1
                    dr1(j) = Trim(DataGridView2.Rows(i).Cells(j).Value.ToString.Trim)
                Next
            Else
                For j As Integer = 0 To DataGridView2.Columns.Count - 1
                    dr1(j) = Trim(DataGridView2.Rows(i).Cells(j).Value.ToString.Trim)
                Next
            End If



            dset.Tables(0).Rows.Add(dr1)
        Next


        Dim excel As New Excel.Application()
        Dim wBook As Excel.Workbook
        Dim wSheet As Excel.Worksheet


        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        With wSheet
            Dim rng As Range

            rng = wSheet.Columns("B:B")
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

        Dim strFileName As String = "File.xlsx"
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
    Private Sub FG_SN_Export()
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
        'For i As Integer = 0 To DataGridView2.RowCount - 1
        '    dr1 = dset.Tables(0).NewRow
        '    For j As Integer = 0 To DataGridView2.Columns.Count - 1
        '        dr1(j) = Trim(DataGridView2.Rows(i).Cells(j).Value)
        '    Next
        '    dset.Tables(0).Rows.Add(dr1)
        'Next

        For i As Integer = 0 To DataGridView2.RowCount - 1
            dr1 = dset.Tables(0).NewRow
            If txtDEV.Text.Trim <> "" Then
                dr1(0) = Trim(DataGridView2.Rows(i).Cells(0).Value.ToString.Trim & ">" & txtDEV.Text.Trim)
                For j As Integer = 1 To DataGridView2.Columns.Count - 1
                    dr1(j) = Trim(DataGridView2.Rows(i).Cells(j).Value.ToString.Trim)
                Next
            Else
                For j As Integer = 0 To DataGridView2.Columns.Count - 1
                    dr1(j) = Trim(DataGridView2.Rows(i).Cells(j).Value.ToString.Trim)
                Next
            End If



            dset.Tables(0).Rows.Add(dr1)
        Next


        Dim excel As New Excel.Application()
        Dim wBook As Excel.Workbook
        Dim wSheet As Excel.Worksheet


        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        With wSheet
            Dim rng As Range

            rng = wSheet.Columns("B:B")
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

        Dim strFileName As String = "File.xlsx"
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
    Private Sub Carrier_SN_Export()
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
                Try
                    dr1(j) = Trim(DataGridView2.Rows(i).Cells(j).Value.ToString.Trim)
                Catch ex As Exception
                    dr1(j) = ""
                End Try

            Next
            If dr1(0) <> "" Then
                dset.Tables(0).Rows.Add(dr1)
            End If

        Next

        Dim excel As New Excel.Application()
        Dim wBook As Excel.Workbook
        Dim wSheet As Excel.Worksheet


        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        With wSheet
            Dim rng As Range

            rng = wSheet.Columns("B:B")
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

        Dim strFileName As String = "File.xlsx"
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
    Private Sub Eimez_SN_Export()
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
                If (DataGridView2.Rows(i).Cells(j).Value IsNot Nothing) Then
                    dr1(j) = Trim(DataGridView2.Rows(i).Cells(j).Value.ToString.Trim)
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

            rng = wSheet.Columns("B:B")
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

        Dim strFileName As String = "File.xlsx"
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

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            RadioButton2.Checked = False
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            RadioButton1.Checked = False
        End If
    End Sub

    Private Sub txtPCBA_WO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPCBA_WO.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            If txtPCBA_WO.Text.Trim = "" Then
                MsgBox("YOU HAVE NOT INPUT PCBA_WO INTO BOX. PLEASE INPUT!!!")
            ElseIf Len(txtPCBA_WO.Text.Trim) <> 6 And Len(txtPCBA_WO.Text.Trim) <> 7 Then
                MsgBox("WO ARE NOT CORRECT, PLEASE CONFIRM AGAIN!!!")
            Else
                LoadBoxbuild_SN_Mac()
            End If
        End If

    End Sub
    Private Sub LoadBoxbuild_SN_Mac()
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblCrusher_SN_BOXBUILD_Count_byWO'" & txtWO.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            If CInt(txtQty.Text.Trim) = CInt(dr1("").ToString.Trim) Then
                MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")
                BOXBUILD_SN_Log_Load()

            Else
                Dim conn1 As SqlConnection = New SqlConnection(Strconn)
                cmd = New SqlCommand("sptblCrusher_SN_Count_byWO'" & txtWO.Text.Trim & "'", conn1) ' Ket noi vao datatable cua Store Procedure
                conn1.Open()
                Dim dr2 As SqlDataReader
                dr2 = cmd.ExecuteReader()
                While dr2.Read
                    If CInt(txtQty.Text.Trim) < CInt(dr2("").ToString.Trim) Then
                        'MsgBox("SN LIST OF THIS WO HAVE RELEASED ALREADY. PLEASE CONFIRM AGAIN!!!")
                        BOXBUILD_SN_Log_Load1()
                    Else
                        'Tao SN format_End _Start cua WO
                        CreateSN_BOXBUILD_Start_End()
                        ' Tao danh sach so SN cho WO
                        createSN_List()
                        'Lay so MAC_Start_End
                        Get_Start_End_Mac()
                        'Load danh sach MAC can dung
                        Load_Mac_Range()
                    End If

                End While
                dr2.Close()
                cmd.Dispose()
                conn1.Close()

            End If
        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        Dim conn As SqlConnection = New SqlConnection(Strconn)
        'Load Mac range from UHQ

        GET_MAC_RANGE()

        ' Delete Mac duplicate table


        cmd = New SqlCommand("sptblMAC_Duplicated_Del", conn) ' Ket noi vao datatable cua Store Procedure
        cmd.CommandType = CommandType.StoredProcedure

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()

        Catch ex As SqlException
            conn.Close()
            'MessageBox.Show("Can not insert Mac#.")
        End Try


        Dim unit As Integer, site As String, location As String

        unit = CInt(lblUnit.Text.Trim)
        site = lblSite.Text.Trim
        location = lblLocation.Text.Trim

        If unit <> 4 Then
            MsgBox("Wrong count_per_unit")

            lblUnit.ForeColor = Color.Red
        End If

        If site.ToString.Trim <> "uhn-mfg" Then
            MsgBox("Wrong allocated_site")
            lblSite.ForeColor = Color.Red

        End If

        If location.ToString.Trim.ToUpper <> "UNIGEN" Then

            MsgBox("Wrong allocated_organization")

            lblLocation.ForeColor = Color.Red

        End If

        Dim StartMAC As String, EndMAC As String


        Dim max As Double

        max = dgw.RowCount - 2

        If max + 1 <> CDbl(lblCount.Text.Trim) Then
            MsgBox("Unit_count is not: " & lblCount.Text & " _Actual is: " & max + 1)
            lblCount.ForeColor = Color.Red

        End If


        StartMAC = dgw.Rows(0).Cells(0).Value
        EndMAC = dgw.Rows(max).Cells(0).Value

        If StartMAC <> txtStart_MAC.Text.Trim Then
            MsgBox("Wrong low_address")
            txtStart_MAC.ForeColor = Color.Red
        End If
        'If EndMAC <> txtEnd_MAC.Text.Trim Then
        '    MsgBox("Wrong high_address")
        '    txtEnd_MAC.ForeColor = Color.Red
        'End If

        Dim k As Integer, AllocationID As Long

        For k = 0 To DataGridView6.RowCount - 2
            AllocationID = DataGridView6.Rows(k).Cells(0).Value
            If CInt(AllocationID) = CInt(lblID.Text.Trim) Then

                lblID.ForeColor = Color.Red
                MsgBox("Wrong group_allocation_id")

                Exit For
            End If

        Next

        Dim StartMAC_Dec As Long, EndMAC_Dec As Long

        StartMAC_Dec = CLng("&H" & Microsoft.VisualBasic.Right(StartMAC, 10))
        EndMAC_Dec = CLng("&H" & Microsoft.VisualBasic.Right(EndMAC, 10))

        Dim MinMAC As Long, MaxMAC As Long, Reason As String = ""


        '5. CHECK DUPLICATE WITH PREVIOUS ASSIGNMENT TO UHQ

        For k = 0 To DataGridView6.RowCount - 2

            MinMAC = DataGridView6.Rows(k).Cells(2).Value
            MaxMAC = DataGridView6.Rows(k).Cells(3).Value
            site = DataGridView6.Rows(k).Cells(9).Value


            If StartMAC_Dec >= MinMAC And StartMAC_Dec <= MaxMAC Then
                If site = "UHQ" Then
                    Reason = "DUPLICATE-UHQ"
                ElseIf site = "UHAN" Then
                    Reason = "DUPLICATE-UHAN"

                End If

                Exit For
            ElseIf EndMAC_Dec >= MinMAC And EndMAC_Dec <= MaxMAC Then
                If site = "UHQ" Then
                    Reason = "DUPLICATE-UHQ"
                ElseIf site = "UHAN" Then
                    Reason = "DUPLICATE-UHAN"

                End If

                Exit For
            ElseIf StartMAC_Dec <= MinMAC And EndMAC_Dec >= MaxMAC Then
                If site = "UHQ" Then
                    Reason = "DUPLICATE-UHQ"
                ElseIf site = "UHAN" Then
                    Reason = "DUPLICATE-UHAN"

                End If

                Exit For

            Else
                Reason = ""
            End If



        Next

        If Reason <> "" Then

            Dim y As Integer

            Dim MAC_dec As Long, MAC_1 As String


            For i = 0 To dgw.RowCount - 2


                Dim Mac As String, prefix As String

                Mac = CStr(dgw.Rows(i).Cells("MacAddress").Value)

                prefix = Microsoft.VisualBasic.Left(Mac, 6)



                '1. CHECK OUI
                If prefix <> "24A937" Then
                    Reason = "WRONG OUI"

                Else

                    '2. CHECK DUPLICATE IN THE NEW RANGE
                    Dim PendingTotal As Integer = 0
                    Dim counter As Integer = 0
                    For y = 0 To dgw.RowCount - 2

                        If dgw.Rows(y).Cells("MacAddress").Value = Mac Then
                            Dim temp As Integer
                            Int32.TryParse(dgw.Rows(y).Cells("MacAddress").Value.ToString(), temp)
                            PendingTotal += temp
                            counter += 1
                            If counter > 1 Then
                                Exit For

                            End If

                        End If

                    Next

                    If counter > 1 Then
                        Reason = "DUPLICATE IN FILE"
                    Else

                        '3. CHECK THE SEQUENCE OF MAC
                        MAC_1 = Microsoft.VisualBasic.Right(dgw.Rows(i).Cells("MacAddress").Value, 10)

                        MAC_dec = CLng("&H" & MAC_1)

                        Dim last_digit As String

                        last_digit = Microsoft.VisualBasic.Right(Mac, 1).ToString.Trim


                        'Check sequence in 4 loops

                        Dim last_1stMAC_digit As String

                        last_1stMAC_digit = Microsoft.VisualBasic.Right(StartMAC, 1)

                        'If last_1stMAC_digit = "0" Or last_1stMAC_digit = "4" Or last_1stMAC_digit = "8" Or last_1stMAC_digit = "C" Then

                        If last_digit <> "0" And last_digit <> "4" And last_digit <> "8" And last_digit <> "C" Then

                            Reason = "WRONG SEQUENCE"

                        Else

                            '5. CHECK DUPLICATE WITH PREVIOUS ASSIGNMENT TO UHAN


                            For k = 0 To DataGridView6.RowCount - 2

                                MinMAC = DataGridView6.Rows(k).Cells(2).Value
                                MaxMAC = DataGridView6.Rows(k).Cells(3).Value
                                site = DataGridView6.Rows(k).Cells(9).Value

                                AllocationID = DataGridView6.Rows(k).Cells(0).Value



                                If MAC_dec >= MinMAC And MAC_dec <= MaxMAC Then
                                    If site = "UHQ" Then
                                        Reason = "DUPLICATE-UHQ"
                                    ElseIf site = "UHAN" Then
                                        Reason = "DUPLICATE-UHAN"
                                    Else
                                        Reason = ""
                                    End If

                                    Exit For
                                Else
                                    Reason = ""
                                End If



                            Next
                        End If


                    End If

                End If




                'SAVE DATA TO TABLE DUPLICATION

                If Reason <> "" Then

                    Dim conn1 As SqlConnection = New SqlConnection(Strconn)
                    Dim cmd1 As New SqlCommand


                    cmd1 = New SqlCommand("sptblMAC_Duplicated_add", conn1) ' Ket noi vao datatable cua Store Procedure
                    cmd1.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

                    cmd1.Parameters.Add(New SqlParameter("@MAC_ADDRESS", SqlDbType.NChar, 16)).Value = Mac

                    cmd1.Parameters.Add(New SqlParameter("@DUPLICATED_SITE", SqlDbType.NChar, 30)).Value = Reason

                    Try
                        conn1.Open()
                        cmd1.ExecuteNonQuery()
                        conn1.Close()

                    Catch ex As SqlException
                        conn1.Close()
                        MsgBox(ex)
                    End Try

                End If


            Next

        Else
            Dim i As Integer, y As Integer

            Dim MAC_dec As Long, MAC_1 As String


            For i = 0 To dgw.RowCount - 2


                Dim Mac As String, prefix As String

                Mac = CStr(dgw.Rows(i).Cells("MacAddress").Value)

                prefix = Microsoft.VisualBasic.Left(Mac, 6)



                '1. CHECK OUI
                If prefix <> "24A937" Then
                    Reason = "WRONG OUI"

                Else

                    '2. CHECK DUPLICATE IN THE NEW RANGE
                    Dim PendingTotal As Integer = 0
                    Dim counter As Integer = 0
                    For y = 0 To dgw.RowCount - 2

                        If dgw.Rows(y).Cells("MacAddress").Value = Mac Then
                            Dim temp As Integer
                            Int32.TryParse(dgw.Rows(y).Cells("MacAddress").Value.ToString(), temp)
                            PendingTotal += temp
                            counter += 1
                            If counter > 1 Then
                                Exit For

                            End If

                        End If

                    Next

                    If counter > 1 Then
                        Reason = "DUPLICATE IN FILE"
                    Else

                        '3. CHECK THE SEQUENCE OF MAC
                        MAC_1 = Microsoft.VisualBasic.Right(dgw.Rows(i).Cells("MacAddress").Value, 10)

                        MAC_dec = CLng("&H" & MAC_1)
                        Dim last_digit As String

                        last_digit = Microsoft.VisualBasic.Right(Mac, 1).ToString.Trim

                        'Check sequence in 4 loops

                        Dim last_1stMAC_digit As String

                        last_1stMAC_digit = Microsoft.VisualBasic.Right(StartMAC, 1)

                        '  If last_1stMAC_digit = "0" Or last_1stMAC_digit = "4" Or last_1stMAC_digit = "8" Or last_1stMAC_digit = "C" Then

                        If last_digit <> "0" And last_digit <> "4" And last_digit <> "8" And last_digit <> "C" Then

                            Reason = "WRONG SEQUENCE"

                        Else

                            '5. CHECK DUPLICATE WITH PREVIOUS ASSIGNMENT TO UHQ


                            For k = 0 To DataGridView6.RowCount - 2

                                MinMAC = DataGridView6.Rows(k).Cells(2).Value
                                MaxMAC = DataGridView6.Rows(k).Cells(3).Value
                                site = DataGridView6.Rows(k).Cells(9).Value


                                If MAC_dec >= MinMAC And MAC_dec <= MaxMAC Then
                                    If site = "UHQ" Then
                                        Reason = "DUPLICATE-UHQ"
                                    ElseIf site = "UHAN" Then
                                        Reason = "DUPLICATE-UHAN"
                                    Else
                                        Reason = ""
                                    End If

                                    Exit For
                                Else
                                    Reason = ""
                                End If


                            Next
                        End If


                    End If



                End If




                'SAVE DATA TO TABLE DUPLICATION

                If Reason <> "" Then

                    Dim conn1 As SqlConnection = New SqlConnection(Strconn)
                    Dim cmd1 As New SqlCommand


                    cmd1 = New SqlCommand("sptblMAC_Duplicated_add", conn1) ' Ket noi vao datatable cua Store Procedure
                    cmd1.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

                    cmd1.Parameters.Add(New SqlParameter("@MAC_ADDRESS", SqlDbType.NChar, 16)).Value = Mac

                    cmd1.Parameters.Add(New SqlParameter("@DUPLICATED_SITE", SqlDbType.NChar, 30)).Value = Reason

                    Try
                        conn1.Open()
                        cmd1.ExecuteNonQuery()
                        conn1.Close()

                    Catch ex As SqlException
                        conn1.Close()
                        MsgBox(ex)
                    End Try

                End If


            Next


        End If

        ' Load duplicate

        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "spGetMac_Duplicated"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblMAC_Duplicated")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblMAC_Duplicated"))
            With DataGridView5
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try



        Dim Count As Integer

        Count = Me.DataGridView5.RowCount - 1

        If Count >= 1 Or lblID.ForeColor = Color.Red Or lblUnit.ForeColor = Color.Red Or lblSite.ForeColor = Color.Red Or lblLocation.ForeColor = Color.Red Or txtStart_MAC.ForeColor = Color.Red Or txtEnd_MAC.ForeColor = Color.Red Or lblCount.ForeColor = Color.Red Then
            '  MsgBox("THERE ARE SOME DUPLICATE MAC# WITH THE PREVIOUS ASSIGNMENT or MISTAKES WITH NEW MAC# INFORMATION WHERE IS HIGHLIGHT IN RED. PLEASE FEEDBACK TO PURE STORAGE!!!")

            MsgBox("THERE ARE SOME MISTAKES WHERE IN RED TEXT AND/OR IN THE LIST OF ERROR MAC#")

            btnUploadMac.Visible = False

        Else

            MsgBox("NEW MAC# RANGE IS GOOD TO USE!!!")

            btnUploadMac.Visible = True
            conn.Close()
            Count_MAC_available()
            Load_Mac_available()

        End If


    End Sub

    Private Sub GET_MAC_RANGE()

        ' Dim conn As SqlConnection = New SqlConnection(Strconn)
        Dim conn7 As SqlConnection = New SqlConnection(Strconn7)

        Try

            'Me.StrSql = "spGetMac_Range"
            Me.StrSql = "P_SELECT_UINGEN_PureStorage_MacRange"
            Dim cmd2 As New SqlCommand(Me.StrSql, conn7)
            da = New SqlDataAdapter(cmd2)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "UINGEN_PureStorage_MaxWWNAddress")
            conn7.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("UINGEN_PureStorage_MaxWWNAddress"))
            With DataGridView6
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try





    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs)
        Load()
    End Sub
    Private Sub Load()
        txtPN.Enabled = False
        txtDatecode.Enabled = False
        txtQty.Enabled = False
        txtRev.Enabled = False
        txtSN_end.Enabled = False
        txtSN_Start.Enabled = False
        txtDate.Hide()
        txtDate.Text = DateTimePicker1.Value.ToShortDateString
        Load_Mac_available()
        ' Load_Dover_Mac_available()
        Count_MAC_available()
        '  Count_Dover_MAC_available()
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        LOGIN.Size = MinimumSize
        txtPCBA_WO.Visible = False
        txtDEV.Visible = False
        GET_MAC_RANGE()
        btnUploadMac.Visible = False

    End Sub

    Private Sub btnGetfile_Click(sender As Object, e As EventArgs) Handles btnGetfile.Click
        Dim openFileDialog As New OpenFileDialog With {
          .Filter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
      }

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Dim jsonContent As String = File.ReadAllText(openFileDialog.FileName)
            'ParseJson(jsonContent)


            Dim Obj_Info As Class1 = JsonConvert.DeserializeObject(Of Class1)(jsonContent)

            lblID.Text = Obj_Info.group_allocation_id
            lblSite.Text = Obj_Info.allocated_site
            lblUnit.Text = Obj_Info.count_per_unit
            lblLocation.Text = Obj_Info.allocated_organization
            txtStart_MAC.Text = Obj_Info.low_address.Replace(":", "")
            txtEnd_MAC.Text = Obj_Info.high_address.Replace(":", "")
            lblCount.Text = Obj_Info.unit_count

            Dim gridData As New Data.DataTable()
            gridData.Columns.Add("MacAddress")

            For Each listItem As String In Obj_Info.guid_range_base_addresses

                listItem = listItem.Replace(":", "")
                gridData.Rows.Add(listItem)

            Next

            dgw.DataSource = gridData

            dgw.Columns(0).Width = 150

        End If


    End Sub
End Class