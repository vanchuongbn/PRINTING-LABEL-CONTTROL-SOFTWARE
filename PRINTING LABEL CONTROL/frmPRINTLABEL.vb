Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports System.Data
Imports Microsoft.Office.Interop
Imports Microsoft.Reporting.WinForms
Imports Microsoft.VisualBasic
'Imports System.Globalization.DateTimeFormatInfo.InvariantInfo
Imports System.Drawing.Drawing2D

Public Class frmRE_PRINT_SERIAL_LABEL
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim conn1 As SqlConnection = New SqlConnection(Strconn1)
    Dim conn2 As SqlConnection = New SqlConnection(Strconn2)
    Dim cmd As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim h As String
    Dim w As String
    Private Sub frmPRINTLABEL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDate.Text = DateTimePicker1.Value.ToShortDateString
        txtDate.Enabled = False
        CreateID()


        txtMac.Enabled = False
        txtSerial.Enabled = False
        cbbtype.Text = ""
        txtMac2.Enabled = False
        txtPrintControlNo.Enabled = False
        txtPrintID.Enabled = False

        Panel1.Hide()
        Panel2.Hide()
        Panel3.Hide()
        Panel4.Hide()
        Panel5.Hide()
        Panel6.Hide()
        Panel7.Hide()


    End Sub
    Private Sub cbbtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbtype.SelectedIndexChanged

        If cbbtype.Text = "Serial" Then
            txtSerial.Enabled = True
            txtMac.Enabled = False
        ElseIf cbbtype.Text = "Mac" Then
            txtSerial.Enabled = False
            txtMac.Enabled = True
        End If
    End Sub
    Private Sub txtWO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWO.KeyPress
        If e.KeyChar = Chr(13) Then
            If txtWO.Text.Trim = "" Then
                MsgBox("Hãy nhập WO")
                txtWO.Focus()
            Else
                CreateID()
                LoadReprintlabel_List()
                Cleartext()
                Panel1.Hide()
                Panel2.Hide()
                Panel3.Hide()
                Panel4.Hide()
                Panel5.Hide()
                Panel6.Hide()
                Panel7.Hide()

                If cbbtype.Text = "Serial" Then
                    txtSerial.Focus()
                ElseIf cbbtype.Text = "Mac" Then
                    txtMac.Focus()
                ElseIf cbbtype.Text = "Product" Then
                    Loadinformation1()
                    LoadLabel()
                End If
            End If
        Else

        End If

    End Sub
    Private Sub txtSerial_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSerial.KeyPress
        If e.KeyChar = Chr(13) Then

            Loadinformation2()

        End If

    End Sub
    Private Sub Loadinformation1()
        cmd = New SqlCommand("Select MODEL_CODE,WORK_WEEK from tblWOfollowUP where ORIGIN_WO='" & Me.txtWO.Text.Trim & "'", conn1)
        conn1.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader
        While dr1.Read
            Me.txtProduct.Text = dr1("MODEL_CODE").ToString.Trim
        End While
        'Savedata()
        conn1.Close()
        dr1.Close()
        cmd.Dispose()

        cmd = New SqlCommand("Select * from tblModelname where UPN='" & Replace(Me.txtProduct.Text.Trim, ".X.X", "") & "'", conn2)
        conn2.Open()
        Dim dr2 As SqlDataReader
        dr2 = cmd.ExecuteReader
        While dr2.Read
            Me.txtCustomer.Text = dr2("Customer").ToString.Trim
            Me.txtProducttype.Text = dr2("Producttype").ToString.Trim
            Me.txtCPN.Text = dr2("CPN").ToString.Trim
            txtCapacity.Text = dr2("Capacity").ToString.Trim

        End While
        '   Savedata()
        conn2.Close()
        dr2.Close()
        cmd.Dispose()


    End Sub
    Private Sub Loadinformation2()
        Dim conn2 As SqlConnection = New SqlConnection(Strconn2)
        sqladap = New SqlDataAdapter("Select WO,SN from SN_WO where SN='" & Me.txtSerial.Text.Trim & "'and WO='" & txtWO.Text.Trim & "'", conn2)
        Dim tb As New DataTable()

        Try
            conn2.Open()
            sqladap.Fill(tb)
        Catch ex As Exception
            MsgBox(ex)
            conn2.Close()
        End Try
        If (tb.Rows.Count <= 0) Then
            MsgBox("Số SN không đúng theo WO")

            conn2.Close()

        Else
            Loadinformation1()
            Datecode()
            LoadLabel()

            conn2.Close()
            cmd = New SqlCommand("Select MacAdd,CurrentProcessID,Name, AliasSN from SN_WO where SN='" & Me.txtSerial.Text.Trim & "'", conn2)
            conn2.Open()
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            While dr.Read
                txtSaber_Mac2.Text = "MAC2: " & dr("AliasSN").ToString.Trim
                If txtCustomer.Text = "PURE STORAGE" Then
                    txtMac.Text = dr("AliasSN").ToString.Trim
                Else
                    Me.txtMac.Text = dr("MacAdd").ToString.Trim
                End If

                If Not dr.IsDBNull(dr.GetOrdinal("CurrentProcessID")) Then
                    txtProcessID.Text = dr("CurrentProcessID")
                Else
                    txtProcessID.Text = 0
                End If
                Me.txtModelname.Text = dr("Name").ToString.Trim
                Dim a = txtProcessID.Text
                If a = 66 Or a = 67 Then
                    txtProcessName.Text = "SMT"
                ElseIf a = 44 Then
                    txtProcessName.Text = "WAVE"
                ElseIf a = 13 Or a = 38 Or a = 50 Or a = 85 Or a = 36 Then
                    txtProcessName.Text = "Function Test"
                ElseIf a = 49 Then
                    txtProcessName.Text = "ICT"
                ElseIf a = 2 Then
                    txtProcessName.Text = "XRAY"
                ElseIf a = 12 Or a = 28 Or a = 88 Then
                    txtProcessName.Text = "FQC"
                ElseIf a = 11 Then
                    txtProcessName.Text = "FQA"
                ElseIf a = 80 Then
                    txtProcessName.Text = "HIPOT"
                ElseIf a = 162 Then
                    txtProcessName.Text = "VPD TEST"
                ElseIf a = 99 Then
                    txtProcessName.Text = "FWDL"
                Else
                    txtProcessName.Text = "KITTING"
                End If
            End While

            '  Savedata()
            conn2.Close()
            dr.Close()
            cmd.Dispose()
        End If

    End Sub
    Private Sub Datecode()
        cmd = New SqlCommand("Select * from tblWOfollowUP where ORIGIN_WO='" & Me.txtWO.Text.Trim & "'", conn1)
        conn1.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader
        While dr1.Read
            Me.txtProduct.Text = dr1("MODEL_CODE").ToString.Trim
            Dim YY As String
            YY = dr1("ISSUED_DATE")
            Dim cvdate As String
            If Microsoft.VisualBasic.Right(YY, 11) = "12:00:00 AM" Then
                cvdate = Microsoft.VisualBasic.Mid(YY, YY.IndexOf("12:00:00 AM") - 2, 2)
            Else
                cvdate = Microsoft.VisualBasic.Right(YY, 2)
            End If
            Dim week As String
            week = Replace(Microsoft.VisualBasic.Right((dr1("WORK_WEEK").ToString.Trim), 2), "_", 0)
            txtDatecode.Text = cvdate & week

        End While
        'Savedata()
        conn1.Close()
        dr1.Close()
        cmd.Dispose()
    End Sub
    Private Sub LoadLabel()
        If txtCustomer.Text = "CISCO" Then
            LoadLabelCisco()
        ElseIf txtCustomer.Text = "PURE STORAGE" Then
            LoadLabelPURESTORAGE()
        ElseIf txtCustomer.Text = "SEL" And txtModelname.Text.Trim = "SABERTOOTH" Then
            LoadSabertoothLabel()
        ElseIf txtCustomer.Text = "SEL" Then
            LoadLabelSEL()
        End If
    End Sub
    Private Sub LoadLabelCisco()
        If txtProducttype.Text.Trim = "eUSB" And cbbtype.Text.Trim = "Serial" Then
            Panel1.Show()
            Panel1.Location = New Point(300, 15)
            txtE_SN.Text = txtSerial.Text
            txtE_DC.Text = txtDatecode.Text
            txtE_WO.Text = txtWO.Text
            txtE_Bar.Text = txtSerial.Text
            txtE_Bar.Font = New Font("Code 128", 26.0, FontStyle.Regular)
            txtE_CAP1.Text = txtCapacity.Text
        ElseIf txtProducttype.Text.Trim = "eUSB" And cbbtype.Text.Trim = "Product" Then
            Panel2.Show()
            Panel2.Location = New Point(300, 15)
            txtE_CPN.Text = txtCPN.Text
            txtE_Bar2.Text = txtE_CPN.Text
            txtE_Bar2.Font = New Font("Code 128", 36.0, FontStyle.Regular)
            txtE_Product.Text = Replace(txtProduct.Text, ".X.X", "")
            txtE_Cap2.Text = txtCapacity.Text
        ElseIf txtProducttype.Text.Trim = "CF Card" And cbbtype.Text.Trim = "Serial" Then
            Panel3.Show()
            Panel3.Location = New Point(300, 15)
            txtCF_Bar1.Text = txtSerial.Text
            txtCF_Bar1.Font = New Font("Code 128", 20.0, FontStyle.Regular)
            txtCF_Product.Text = Replace(txtProduct.Text, ".X.X", "")
            txtCF_SN.Text = txtSerial.Text
            txtCF_DC.Text = txtDatecode.Text
            txtCF_WO.Text = txtWO.Text
        ElseIf txtProducttype.Text.Trim = "CF Card" And cbbtype.Text.Trim = "Product" Then
            Panel4.Show()
            Panel4.Location = New Point(300, 15)
            txtCF_Bar2.Text = txtCPN.Text
            txtCF_Bar2.Font = New Font("Code 128", 40.0, FontStyle.Regular)
            txtCF_Capacity.Text = txtCapacity.Text
            txtCF_CPN.Text = txtCPN.Text

        End If
    End Sub
    Private Sub LoadLabelPURESTORAGE()
        Panel5.Show()
        Panel5.Location = New Point(300, 15)
        txtPur_Bar1.Text = txtSerial.Text
        txtPur_SN.Text = txtSerial.Text
        txtPur_Bar1.Font = New Font("Code 128", 14.0, FontStyle.Regular)

        txtPur_Product.Text = txtProduct.Text
        txtPur_Bar2.Text = txtProduct.Text
        txtPur_Bar2.Font = New Font("Code 128", 14.0, FontStyle.Regular)

        txtWWN.Text = txtMac.Text
        txtPur_Bar3.Text = txtMac.Text
        txtPur_Bar3.Font = New Font("Code 128", 14.0, FontStyle.Regular)

        txtPur_WO.Text = txtWO.Text & " - " & txtDatecode.Text

        cmd = New SqlCommand("Select AgileBOMRevision from tblLabelInformation where Model='" & Me.txtProduct.Text.Trim & "'", conn2) 'and Type= " & cbbtype.Text.Trim & "
        conn2.Open()
        Dim dr2 As SqlDataReader
        dr2 = cmd.ExecuteReader
        While dr2.Read
            Me.txtPur_AgileRev.Text = dr2("AgileBOMRevision").ToString.Trim

        End While
        '   Savedata()
        conn2.Close()
        dr2.Close()
        cmd.Dispose()

    End Sub
    Private Sub LoadLabelSEL()
        Panel6.Show()
        Panel6.Location = New Point(300, 15)

        txtSEL_bar1.Text = Replace(txtProduct.Text.Trim, ".X.X", "")
        txtSEL_PN.Text = Replace(txtProduct.Text.Trim, ".X.X", "")
        txtSEL_bar1.Font = New Font("Code 128", 14.0, FontStyle.Regular)
        txtSEL_bar2.Text = txtSerial.Text
        txtSEL_SN.Text = txtSerial.Text
        txtSEL_bar2.Font = New Font("Code 128", 14.0, FontStyle.Regular)

        cmd = New SqlCommand("Select AgileBOMRevision,ENGBOMRevision from tblLabelInformation where Model='" & Replace(Me.txtProduct.Text.Trim, ".X.X", "") & "'", conn2) 'and Type= " & cbbtype.Text.Trim & "
        conn2.Open()
        Dim dr2 As SqlDataReader
        dr2 = cmd.ExecuteReader
        While dr2.Read
            Me.txtSEL_Agile.Text = dr2("AgileBOMRevision").ToString.Trim
            Me.txtSEL_ENG.Text = dr2("ENGBOMRevision").ToString.Trim
        End While
        '   Savedata()
        conn2.Close()
        dr2.Close()
        cmd.Dispose()
    End Sub
    Private Sub LoadSabertoothLabel()
        Panel7.Show()
        Panel7.Location = New Point(300, 15)
        txtSaber_bar1.Text = txtSerial.Text.Trim
        txtSaber_SN.Text = txtProduct.Text.Trim
        txtSaber_bar1.Font = New Font("Code 128", 14.0, FontStyle.Regular)

        txtSaber_Bar2.Text = txtMac.Text
        txtSaber_Mac1.Text = "MAC1: " & txtMac.Text
        txtSaber_Bar2.Font = New Font("Code 128", 14.0, FontStyle.Regular)

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        EmptyTxt(Me)
        cbbtype.Text = ""
        Panel1.Hide()
        Panel2.Hide()
        Panel3.Hide()
        Panel4.Hide()
        Panel5.Hide()
        Panel6.Hide()
        Panel7.Hide()
        txtDate.Text = DateTimePicker1.Value.ToShortDateString
        CreateID()

    End Sub

    Private Sub CreateID()
        conn = New SqlConnection(Strconn)
        Dim Sql As SqlCommand = New SqlCommand("SELECT MAX(PrintID) AS [PrintID] FROM tblSN_RE_PRINT where Printdate= '" & Me.txtDate.Text & "'", conn)
        conn.Open()
        Dim formattedDate As String = DateTimePicker1.Value.ToString("yyMMdd")

        Dim myreader = Sql.ExecuteReader
        If myreader.Read() Then
            If myreader.IsDBNull(0) Then

                Dim number As Integer = 1
                Dim IDNo As String = number.ToString()
                Me.txtPrintID.Text = IDNo
                If Len(txtPrintID.Text) = 1 Then
                    Me.txtPrintControlNo.Text = formattedDate + "000" + IDNo
                ElseIf Len(txtPrintID.Text) = 2 Then
                    Me.txtPrintControlNo.Text = formattedDate + "00" + IDNo
                ElseIf Len(txtPrintID.Text) = 3 Then
                    Me.txtPrintControlNo.Text = formattedDate + "0" + IDNo
                Else
                    Me.txtPrintControlNo.Text = formattedDate + IDNo
                End If
            Else

                Dim rd As String = myreader(0).ToString
                Dim id As Integer = myreader(0).ToString + 1
                Dim NoID As String = id.ToString
                Me.txtPrintID.Text = NoID
                If Len(txtPrintID.Text) = 1 Then
                    Me.txtPrintControlNo.Text = formattedDate + "000" + NoID
                ElseIf Len(txtPrintID.Text) = 2 Then
                    Me.txtPrintControlNo.Text = formattedDate + "00" + NoID
                ElseIf Len(txtPrintID.Text) = 3 Then
                    Me.txtPrintControlNo.Text = formattedDate + "0" + NoID
                Else
                    Me.txtPrintControlNo.Text = formattedDate + NoID
                End If

            End If
            conn.Close()
        End If


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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Reprintlabel_save()
        LoadReprintlabel_List()
    End Sub

    Private Sub Reprintlabel_save()
        conn = New SqlConnection(Strconn) ' connect to Database
        cmd = New SqlCommand("sptblSN_RE_Print_add", conn) ' Ket noi vao datatable cua Store Procedure
        cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

        cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 10)).Value = Me.txtWO.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NVarChar, 50)).Value = Me.txtSerial.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@Mac_1", SqlDbType.NVarChar, 50)).Value = Me.txtMac.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@Mac_2_WWN", SqlDbType.NVarChar, 50)).Value = Me.txtMac2.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@PN", SqlDbType.NVarChar, 50)).Value = Me.txtProduct.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@Customer", SqlDbType.NChar, 20)).Value = Me.txtCustomer.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@Process", SqlDbType.NChar, 10)).Value = Me.txtProcessName.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@Printdate", SqlDbType.DateTime)).Value = Me.txtDate.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@Printby", SqlDbType.NVarChar, 20)).Value = Me.txtPrintby.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@PrintID", SqlDbType.Int)).Value = CInt(Me.txtPrintID.Text.Trim)
        cmd.Parameters.Add(New SqlParameter("@PrintControlNo", SqlDbType.NChar, 10)).Value = txtPrintControlNo.text.trim

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            MsgBox("ĐÃ LƯU DỮ LIỆU IN LẠI")
        Catch ex As SqlException
            conn.Close()
            MsgBox("KHÔNG THỂ LƯU")
            Exit Sub
        End Try
        Cleartext()

    End Sub
    Private Sub Cleartext()
        txtSerial.Text = ""
        txtMac.Text = ""
        txtMac2.Text = ""
        txtProcessID.Text = ""
        txtProcessName.Text = ""
        txtProduct.Text = ""
        txtCustomer.Text = ""
        txtModelname.Text = ""
        txtDatecode.Text = ""
        txtCapacity.Text = ""
        txtCPN.Text = ""
        txtAgileRev.Text = ""
        txtEngRev.Text = ""

    End Sub
    Private Sub LoadReprintlabel_List()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "spGetSN_Re_Printed_by_WO'" & Me.txtWO.Text.Trim & "'"
            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblSN_RE_PRINT")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblSN_RE_PRINT"))
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

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If txtSerial.Text.Trim <> "" Then
            conn = New SqlConnection(Strconn) ' connect to Database
            cmd = New SqlCommand("sptblSN_RE_Print_Delete'" & Me.txtSerial.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
            cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

            cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NVarChar, 50)).Value = Me.txtSerial.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@PrintControlNo", SqlDbType.NChar, 10)).Value = Me.txtPrintControlNo.Text.Trim

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
                MsgBox("Hoàn thành xoa")
            Catch ex As SqlException
                conn.Close()
                MsgBox("Không thể xoa")
                Exit Sub
            End Try
        ElseIf txtPrintControlNo.Text.Trim <> "" Then
            conn = New SqlConnection(Strconn) ' connect to Database
            cmd = New SqlCommand("sptblSN_RE_Print_Delete'" & Me.txtPrintControlNo.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
            cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

            cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NVarChar, 50)).Value = Me.txtSerial.Text.Trim
            cmd.Parameters.Add(New SqlParameter("@PrintControlNo", SqlDbType.NChar, 10)).Value = Me.txtPrintControlNo.Text.Trim

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
                MsgBox("Hoàn thành xoa")
            Catch ex As SqlException
                conn.Close()
                MsgBox("Không thể xoa")
                Exit Sub
            End Try
        End If

        LoadReprintlabel_List()
    End Sub
End Class