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
Imports Microsoft.Office.Interop.Excel

Public Class frmRE_PRINT_SERIAL_LABEL
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim conn1 As SqlConnection = New SqlConnection(Strconn1)
    Dim conn2 As SqlConnection = New SqlConnection(Strconn2)
    Dim cmd As SqlCommand
    Dim cmd1 As SqlCommand
    Dim cmd2 As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim h As String
    Dim w As String
    Private Sub frmPRINTLABEL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDate.Text = DateTimePicker1.Value
        txtDate.Enabled = False
        LBLSHORTDATE.Text = DateTimePicker1.Value.ToShortDateString
        LBLSHORTDATE.Hide()
        CreateID()

        txtMac.Enabled = False
        txtSerial.Enabled = False
        cbbtype.Text = ""
        txtMac2.Enabled = False

        txtPrintID.Enabled = False
        txtPrintby.Text = LOGIN.txtUsername.Text.Trim
        txtPrintby.Enabled = False
        txtCombinationSN.Hide()

        Panel1.Hide()
        Panel2.Hide()
        Panel3.Hide()
        Panel4.Hide()
        Panel5.Hide()
        Panel6.Hide()
        Panel7.Hide()
        Panel8.Hide()
        Panel9.Hide()
        Panel10.Hide()



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
                LoadReprintlabel_List_byWO()
                Cleartext()
                Panel1.Hide()
                Panel2.Hide()
                Panel3.Hide()
                Panel4.Hide()
                Panel5.Hide()
                Panel6.Hide()
                Panel7.Hide()
                Panel8.Hide()
                Panel9.Hide()
                Panel10.Hide()
                txtWO.Enabled = False

                Loadinformation1()
                If cbbtype.Text = "Serial" Then
                    txtSerial.Focus()
                ElseIf cbbtype.Text = "Mac" Then
                    txtMac.Focus()
                ElseIf cbbtype.Text = "Product" Then

                    '  LoadLabel()
                End If
            End If
        Else

        End If

    End Sub
    Private Sub txtSerial_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSerial.KeyPress
        If e.KeyChar = Chr(13) Then
            If txtSerial.Text.Trim = "" Then
                MsgBox("Hãy nhập SN")
                txtSerial.Focus()
            Else

                If txtWO.Enabled = True Then
                    MsgBox("HÃY QUAY LẠI Ô NHẬP WO VÀ ẤN ENTER")
                Else

                    LoadRe_print_history()
                End If
            End If
        End If
    End Sub

    Private Sub LoadRe_print_history()
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblSN_RE_Printed_Count'" & Me.txtSerial.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            txtRe_print_times.Text = CInt(dr1("").ToString.Trim)
        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()

        Dim dr As SqlDataReader

        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
        conn.Open()
        dr = cmd.ExecuteReader
        While dr.Read
            Dim Permission As String = dr("Permission").ToString()


            If Permission.StartsWith("User") And txtRe_print_times.Text.Trim > 0 Then
                MsgBox("BẠN KHÔNG ĐƯỢC PHÉP IN LẠI TEM LẦN 2. HÃY LIÊN LẠC VỚI QUẢN LÝ CỦA BẠN ĐỂ XỬ LÝ", vbCritical + vbOKOnly)

            Else

                Loadinformation2()

            End If
        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()


    End Sub
    Private Sub Loadinformation1()

        cmd = New SqlCommand("Select * from tblWOfollowUP where ORIGIN_WO='" & Me.txtWO.Text.Trim & "'", conn1)
        conn1.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader
        While dr1.Read
            Me.txtProduct.Text = dr1("MODEL_CODE").ToString.Trim

        End While

        conn1.Close()
        dr1.Close()
        cmd.Dispose()

        If Len(Me.txtProduct.Text.Trim) = 6 Then
            If Microsoft.VisualBasic.Left(Me.txtProduct.Text.Trim, 1) = 2 Or Microsoft.VisualBasic.Left(Me.txtProduct.Text.Trim, 1) = 3 Then
                Me.txtCustomer.Text = "MICRON"
            Else

            End If
        Else

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

            conn2.Close()
            dr2.Close()
            cmd.Dispose()

            cmd = New SqlCommand("Select * from tblWOfollowUP where ORIGIN_WO='" & Me.txtWO.Text.Trim & "'", conn1)
            conn1.Open()
            Dim dr3 As SqlDataReader
            dr3 = cmd.ExecuteReader
            While dr3.Read
                Dim Rev As String = dr3("REV_PICK_LIST").ToString.Trim
                If Len(Rev) < 3 Then
                    txtAgileRev.Text = Microsoft.VisualBasic.Left(Rev, 1)
                ElseIf Len(Rev) = 3 Then
                    If Rev.IndexOf(".") = 0 Then
                        Me.txtAgileRev.Text = Microsoft.VisualBasic.Mid(Rev, 2, 1)
                    ElseIf Rev.IndexOf(".") = 1 Then
                        txtAgileRev.Text = Microsoft.VisualBasic.Left(Rev, 1)
                    ElseIf Rev.IndexOf(".") = 2 Then
                        txtAgileRev.Text = Microsoft.VisualBasic.Left(Rev, 2)
                    Else
                        txtAgileRev.Text = Microsoft.VisualBasic.Left(Rev, 2)
                    End If
                End If

            End While

            conn1.Close()
            dr3.Close()
            cmd.Dispose()

            conn = New SqlConnection(Strconn2)
            cmd = New SqlCommand("Select * from tblLabelInformation where MODEL='" & Trim(Replace(Me.txtProduct.Text.Trim, ".X.X", "")) & "'", conn)
            conn.Open()
            Dim dr4 As SqlDataReader
            dr4 = cmd.ExecuteReader
            While dr4.Read
                Me.txtModelname.Text = dr4("ModelName").ToString.Trim
                'Me.txtAgileRev.Text = dr3("AgileBOMRevision").ToString.Trim
                'Me.txtEngRev.Text = dr3("ENGBOMRevision").ToString.Trim

            End While
            dr4.Close()
            cmd.Dispose()
            conn.Close()

        End If


    End Sub
    Private Sub Loadinformation2()
        If txtCustomer.Text.Trim <> "MICRON" Then

            Dim conn2 As SqlConnection = New SqlConnection(Strconn4)
            sqladap = New SqlDataAdapter("P_GET_SN_WO_1'" & Me.txtSerial.Text.Trim & "','" & txtWO.Text.Trim & "'", conn2)
            Dim tb As New System.Data.DataTable()

            Try
                conn2.Open()
                sqladap.Fill(tb)
            Catch ex As Exception
                ' MsgBox(ex)
                conn2.Close()
            End Try
            If (tb.Rows.Count <= 0) Then
                MsgBox("Số SN không đúng theo WO")

                conn2.Close()

            Else


                conn2.Close()
                cmd = New SqlCommand("P_GET_SN_WO_2'" & Me.txtSerial.Text.Trim & "'", conn2)
                conn2.Open()
                Dim dr As SqlDataReader
                dr = cmd.ExecuteReader
                While dr.Read
                    txtMac2.Text = dr("AliasSN").ToString.ToUpper.Trim
                    txtSaber_Mac2.Text = "MAC2: " & dr("AliasSN").ToString.ToUpper.Trim
                    'If txtCustomer.Text = "PURE STORAGE" Then
                    txtMac.Text = dr("AliasSN").ToString.ToUpper.Trim
                    'Else
                    Me.txtMac.Text = dr("MacAdd").ToString.ToUpper.Trim
                    'End If

                    If Not dr.IsDBNull(dr.GetOrdinal("CurrentProcessID")) Then
                        txtProcessID.Text = dr("CurrentProcessID")
                    Else
                        txtProcessID.Text = 0
                    End If
                    '  Me.txtModelname.Text = dr("Name").ToString.Trim
                    Dim a = txtProcessID.Text
                    If a = 66 Or a = 67 Then
                        txtProcessName.Text = "SMT"
                    ElseIf a = 44 Then
                        txtProcessName.Text = "WAVE"
                    ElseIf a = 13 Or a = 38 Or a = 50 Or a = 85 Or a = 36 Then
                        txtProcessName.Text = "Function T"
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


                conn2.Close()
                dr.Close()
                cmd.Dispose()
                Datecode()
                '  LoadLabel()
                AdditionAction()
                Reprintlabel_save()
            End If
        Else
            cmd = New SqlCommand("Select MODEL_CODE,WORK_WEEK, OWO_QUANTITY from tblWOfollowUP where ORIGIN_WO='" & Me.txtWO.Text.Trim & "'", conn1)
            conn1.Open()
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader
            Dim WOqty As String
            While dr1.Read
                WOqty = dr1("OWO_QUANTITY").ToString.Trim
                Dim sn = txtSerial.Text.Trim
                If CInt(Microsoft.VisualBasic.Left(sn, 6)) <> txtWO.Text.Trim Then
                    MsgBox("SỐ SERIAL KHÔNG ĐÚNG THEO WO")
                Else

                    If CInt(Microsoft.VisualBasic.Right(sn, 5)) > CInt(WOqty) Then
                        MsgBox("SỐ SERIAL CẦN IN VƯỢT QUÁ GIỚI HẠN CỦA WO")
                    Else
                        AdditionAction()
                        Reprintlabel_save()
                    End If

                End If
            End While

            conn1.Close()
            dr1.Close()
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
    'Private Sub LoadLabel()
    '    If txtCustomer.Text = "CISCO" Then
    '        LoadLabelCisco()
    '    ElseIf txtCustomer.Text = "PURE STORAGE" Then
    '        LoadLabelPURESTORAGE()
    '    ElseIf txtCustomer.Text = "SEL" And txtModelname.Text.Trim = "SABERTOOTH" Then
    '        LoadSabertoothLabel()
    '    ElseIf txtCustomer.Text = "SEL" And txtModelname.Text.Trim = "STEGOSAURUS" Then
    '        LoadStegosaurusLabel()
    '    ElseIf txtCustomer.Text = "SEL" And txtModelname.Text.Trim = "Spike" Then
    '        LoadSpikeLabel()
    '    ElseIf txtCustomer.Text = "SEL" And txtModelname.Text.Trim = "Stegodon PCIE" Then
    '        LoadStegodonLabel()
    '    ElseIf txtCustomer.Text = "SEL" And (txtModelname.Text.Trim <> "STEGOSAURUS" Or txtModelname.Text.Trim <> "Spike" Or txtModelname.Text.Trim = "Stegodon PCIE") Then
    '        LoadLabelSEL()
    '    End If
    'End Sub
    'Private Sub LoadLabelCisco()
    '    If txtProducttype.Text.Trim = "eUSB" And cbbtype.Text.Trim = "Serial" Then
    '        Panel1.Show()
    '        Panel1.Location = New Point(300, 15)
    '        txtE_SN.Text = txtSerial.Text
    '        txtE_DC.Text = txtDatecode.Text
    '        txtE_WO.Text = txtWO.Text
    '        txtE_Bar.Text = txtSerial.Text
    '        txtE_Bar.Font = New Font("Code 128", 26.0, FontStyle.Regular)
    '        txtE_CAP1.Text = txtCapacity.Text
    '    ElseIf txtProducttype.Text.Trim = "eUSB" And cbbtype.Text.Trim = "Product" Then
    '        Panel2.Show()
    '        Panel2.Location = New Point(300, 15)
    '        txtE_CPN.Text = txtCPN.Text
    '        txtE_Bar2.Text = txtE_CPN.Text
    '        txtE_Bar2.Font = New Font("Code 128", 36.0, FontStyle.Regular)
    '        txtE_Product.Text = Replace(txtProduct.Text, ".X.X", "")
    '        txtE_Cap2.Text = txtCapacity.Text
    '    ElseIf txtProducttype.Text.Trim = "CF Card" And cbbtype.Text.Trim = "Serial" Then
    '        Panel3.Show()
    '        Panel3.Location = New Point(300, 15)
    '        txtCF_Bar1.Text = txtSerial.Text
    '        txtCF_Bar1.Font = New Font("Code 128", 20.0, FontStyle.Regular)
    '        txtCF_Product.Text = Replace(txtProduct.Text, ".X.X", "")
    '        txtCF_SN.Text = txtSerial.Text
    '        txtCF_DC.Text = txtDatecode.Text
    '        txtCF_WO.Text = txtWO.Text
    '    ElseIf txtProducttype.Text.Trim = "CF Card" And cbbtype.Text.Trim = "Product" Then
    '        Panel4.Show()
    '        Panel4.Location = New Point(300, 15)
    '        txtCF_Bar2.Text = txtCPN.Text
    '        txtCF_Bar2.Font = New Font("Code 128", 40.0, FontStyle.Regular)
    '        txtCF_Capacity.Text = txtCapacity.Text
    '        txtCF_CPN.Text = txtCPN.Text

    '    End If
    'End Sub
    'Private Sub LoadLabelPURESTORAGE()
    '    Panel5.Show()
    '    Panel5.Location = New Point(300, 15)
    '    txtPur_Bar1.Text = txtSerial.Text
    '    txtPur_SN.Text = txtSerial.Text
    '    txtPur_Bar1.Font = New Font("Code 128", 14.0, FontStyle.Regular)

    '    txtPur_Product.Text = txtProduct.Text
    '    txtPur_Bar2.Text = txtProduct.Text
    '    txtPur_Bar2.Font = New Font("Code 128", 14.0, FontStyle.Regular)

    '    txtWWN.Text = txtMac.Text
    '    txtPur_Bar3.Text = txtMac.Text
    '    txtPur_Bar3.Font = New Font("Code 128", 14.0, FontStyle.Regular)

    '    txtPur_WO.Text = txtWO.Text & " - " & txtDatecode.Text

    '    cmd = New SqlCommand("Select AgileBOMRevision from tblLabelInformation where Model='" & Me.txtProduct.Text.Trim & "'", conn2) 'and Type= " & cbbtype.Text.Trim & "
    '    conn2.Open()
    '    Dim dr2 As SqlDataReader
    '    dr2 = cmd.ExecuteReader
    '    While dr2.Read
    '        Me.txtPur_AgileRev.Text = dr2("AgileBOMRevision").ToString.Trim

    '    End While
    '    '   Savedata()
    '    conn2.Close()
    '    dr2.Close()
    '    cmd.Dispose()

    'End Sub
    'Private Sub LoadLabelSEL()
    '    Panel6.Show()
    '    Panel6.Location = New Point(300, 15)

    '    txtSEL_bar1.Text = Replace(txtProduct.Text.Trim, ".X.X", "")
    '    txtSEL_PN.Text = Replace(txtProduct.Text.Trim, ".X.X", "")
    '    txtSEL_bar1.Font = New Font("Code 128", 14.0, FontStyle.Regular)
    '    txtSEL_bar2.Text = txtSerial.Text
    '    txtSEL_SN.Text = txtSerial.Text
    '    txtSEL_bar2.Font = New Font("Code 128", 14.0, FontStyle.Regular)

    '    cmd = New SqlCommand("Select AgileBOMRevision,ENGBOMRevision from tblLabelInformation where Model='" & Replace(Me.txtProduct.Text.Trim, ".X.X", "") & "'", conn2) 'and Type= " & cbbtype.Text.Trim & "
    '    conn2.Open()
    '    Dim dr2 As SqlDataReader
    '    dr2 = cmd.ExecuteReader
    '    While dr2.Read
    '        Me.txtSEL_Agile.Text = dr2("AgileBOMRevision").ToString.Trim
    '        Me.txtSEL_ENG.Text = dr2("ENGBOMRevision").ToString.Trim
    '    End While
    '    '   Savedata()
    '    conn2.Close()
    '    dr2.Close()
    '    cmd.Dispose()
    'End Sub
    'Private Sub LoadSabertoothLabel()
    '    Panel7.Show()
    '    Panel7.Location = New Point(300, 15)
    '    txtSaber_bar1.Text = txtSerial.Text.Trim
    '    txtSaber_SN.Text = txtProduct.Text.Trim
    '    txtSaber_bar1.Font = New Font("Code 128", 14.0, FontStyle.Regular)

    '    txtSaber_Bar2.Text = txtMac.Text
    '    txtSaber_Mac1.Text = "MAC1: " & txtMac.Text
    '    txtSaber_Bar2.Font = New Font("Code 128", 14.0, FontStyle.Regular)

    'End Sub
    'Private Sub LoadStegodonLabel()
    '    Panel8.Show()
    '    Panel8.Location = New Point(300, 15)
    '    txtStegodon_bar.Text = txtSerial.Text.Trim
    '    txtStegodon_SN.Text = txtSerial.Text.Trim
    '    txtStegodon_bar.Font = New Font("Code 128", 32, FontStyle.Regular)
    '    cmd = New SqlCommand("Select AgileBOMRevision,ENGBOMRevision from tblLabelInformation where Model='" & Replace(Me.txtProduct.Text.Trim, ".X.X", "") & "'", conn2) 'and Type= " & cbbtype.Text.Trim & "
    '    conn2.Open()
    '    Dim dr2 As SqlDataReader
    '    dr2 = cmd.ExecuteReader
    '    While dr2.Read

    '        Me.txtEngRev.Text = dr2("ENGBOMRevision").ToString.Trim
    '    End While
    '    '   Savedata()
    '    conn2.Close()
    '    dr2.Close()
    '    cmd.Dispose()
    '    txtStegodon_PN.Text = "STEGODON 2048GB eMLC," & txtEngRev.Text

    'End Sub
    'Private Sub LoadStegosaurusLabel()
    '    Panel9.Show()
    '    Panel9.Location = New Point(300, 15)
    '    txtStegosaurus_SN_bar.Text = txtSerial.Text.Trim
    '    txtStegosaurusSN.Text = txtSerial.Text.Trim
    '    txtStegosaurus_SN_bar.Font = New Font("Code 128", 32, FontStyle.Regular)
    '    cmd = New SqlCommand("Select AgileBOMRevision,ENGBOMRevision from tblLabelInformation where Model='" & Replace(Me.txtProduct.Text.Trim, ".X.X", "") & "'", conn2) 'and Type= " & cbbtype.Text.Trim & "
    '    conn2.Open()
    '    Dim dr2 As SqlDataReader
    '    dr2 = cmd.ExecuteReader
    '    While dr2.Read
    '        Me.txtAgileRev.Text = dr2("AgileBOMRevision").ToString.Trim
    '        Me.txtEngRev.Text = dr2("ENGBOMRevision").ToString.Trim
    '    End While

    '    conn2.Close()
    '    dr2.Close()
    '    cmd.Dispose()
    '    txtStegosaurus_PN.Text = txtEngRev.Text.Trim & " WO:" & txtWO.Text.Trim & " Stegosaurus " & txtAgileRev.Text
    'End Sub
    'Private Sub LoadSpikeLabel()
    '    Panel10.Show()
    '    Panel10.Location = New Point(300, 15)
    '    txtSpikeSN.Text = txtSerial.Text.Trim
    '    ' txtStegosaurusSN.Text = txtSerial.Text.Trim
    '    'txtStegosaurus_SN_bar.Font = New Font("Code 128", 32, FontStyle.Regular)
    '    cmd = New SqlCommand("Select AgileBOMRevision,ENGBOMRevision from tblLabelInformation where Model='" & Replace(Me.txtProduct.Text.Trim, ".X.X", "") & "'", conn2) 'and Type= " & cbbtype.Text.Trim & "
    '    conn2.Open()
    '    Dim dr2 As SqlDataReader
    '    dr2 = cmd.ExecuteReader
    '    While dr2.Read
    '        Me.txtAgileRev.Text = dr2("AgileBOMRevision").ToString.Trim
    '        Me.txtEngRev.Text = dr2("ENGBOMRevision").ToString.Trim
    '    End While

    '    conn2.Close()
    '    dr2.Close()
    '    cmd.Dispose()
    '    txtSpikePN.Text = "Spike     " & txtAgileRev.Text.Trim
    '    txtSpikeWO.Text = txtEngRev.Text.Trim & "   WO:" & txtWO.Text.Trim
    'End Sub

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
        Panel8.Hide()
        Panel9.Hide()
        Panel10.Hide()
        txtWO.Enabled = True
        txtPrintby.Text = LOGIN.txtUsername.Text.Trim
        txtDate.Text = DateTimePicker1.Value
        CreateID()

    End Sub

    Private Sub CreateID()
        conn = New SqlConnection(Strconn)
        Dim Sql As SqlCommand = New SqlCommand("SELECT MAX(PrintID) AS [PrintID] FROM tblSN_RE_PRINT where Shortdate= '" & Me.LBLSHORTDATE.Text & "'", conn)
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
        AdditionAction()
        Reprintlabel_save()
    End Sub
    Private Sub AdditionAction()
        If txtCustomer.Text.Trim = "Micron" Then
            TextBox1.Text = ""
        ElseIf txtCustomer.Text.Trim = "PURE STORAGE" Or txtCustomer.Text.Trim = "CISCO" Or txtCustomer.Text.Trim = "UTPE" Then
            If txtProcessName.Text.Trim = "FQA" Or txtProcessName.Text.Trim = "FQC" Or txtProcessName.Text = "Function T" Or
                                    txtProcessName.Text = "VPD TEST" Or txtProcessName.Text = "FWDL" Then
                TextBox1.Text = "Retest"
            Else
                TextBox1.Text = ""
            End If
        ElseIf txtCustomer.Text.Trim = "SEL" Then

            If txtModelname.Text.Trim = "Stegodon PCIE" Then

                If txtProcessName.Text.Trim = "FQA" Or txtProcessName.Text.Trim = "FQC" Or txtProcessName.Text = "Function Test" Then
                    TextBox1.Text = "Combine Check"
                    txtCombinationSN.Text = txtSerial.Text.Trim
                Else
                    TextBox1.Text = ""
                End If
            ElseIf txtModelname.Text.Trim = "STEGOSAURUS" Then 'Or txtModelname.Text.Trim = "Spike" Then

                Dim conn As New SqlConnection(Strconn)
                Dim sqladap3 = New SqlDataAdapter("Select * from MammothLabel where TrunkSN='" & txtSerial.Text & "'", conn)
                Dim tb3 As New System.Data.DataTable()
                Try
                    conn.Open()
                    sqladap3.Fill(tb3)
                    conn.Close()
                Catch ex As Exception
                    conn.Close()
                End Try
                If (tb3.Rows.Count <= 0) Then
                    If txtProcessName.Text.Trim = "FQA" Or txtProcessName.Text.Trim = "FQC" Or txtProcessName.Text = "Function Test" Or txtProcessName.Text = "ICT" Then

                        TextBox1.Text = "Retest"
                    Else
                        TextBox1.Text = ""
                    End If
                Else
                    cmd1 = New SqlCommand("Select MamoothSN from MammothLabel where TrunkSN='" & txtSerial.Text & "'", conn) 'and Type= " & cbbtype.Text.Trim & "
                    conn.Open()
                    Dim dr1 As SqlDataReader
                    dr1 = cmd1.ExecuteReader
                    While dr1.Read
                        Me.txtCombinationSN.Text = dr1("MamoothSN").ToString.Trim
                    End While
                    conn.Close()
                    dr1.Close()
                    cmd1.Dispose()

                    TextBox1.Text = "Combine Check"
                End If

            ElseIf txtModelname.Text.Trim = "Spike" Then

                Dim conn As New SqlConnection(Strconn)
                Dim sqladap3 = New SqlDataAdapter("Select * from MammothLabel where Ear0SN='" & txtSerial.Text & "'", conn)
                Dim tb3 As New System.Data.DataTable()
                Try
                    conn.Open()
                    sqladap3.Fill(tb3)
                    conn.Close()
                Catch ex As Exception
                    conn.Close()
                End Try
                If (tb3.Rows.Count <= 0) Then
                    If txtProcessName.Text.Trim = "FQA" Or txtProcessName.Text.Trim = "FQC" Or txtProcessName.Text = "Function Test" Or txtProcessName.Text = "ICT" Then

                        TextBox1.Text = "Retest"
                    Else
                        TextBox1.Text = ""
                    End If

                Else
                    cmd2 = New SqlCommand("Select MamoothSN from MammothLabel where Ear0SN='" & txtSerial.Text & "'", conn) 'and Type= " & cbbtype.Text.Trim & "
                    conn.Open()
                    Dim dr2 As SqlDataReader
                    dr2 = cmd2.ExecuteReader
                    While dr2.Read
                        Me.txtCombinationSN.Text = dr2("MamoothSN").ToString.Trim
                    End While
                    conn.Close()
                    dr2.Close()
                    cmd2.Dispose()

                    TextBox1.Text = "Combine Check"
                End If

            Else

                If txtProcessName.Text.Trim = "FQA" Or txtProcessName.Text.Trim = "FQC" Or txtProcessName.Text = "Function T" Or
                                    txtProcessName.Text = "HIPOT" Or txtProcessName.Text = "ICT" Then
                    TextBox1.Text = "Retest"
                Else
                    TextBox1.Text = ""
                End If
            End If
        Else
            TextBox1.Text = ""
        End If

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
        cmd.Parameters.Add(New SqlParameter("@PrintControlNo", SqlDbType.NChar, 10)).Value = txtPrintControlNo.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@Re_print_times", SqlDbType.Int)).Value = CInt(Me.txtRe_print_times.Text)
        cmd.Parameters.Add(New SqlParameter("@AdditionAction", SqlDbType.NChar, 20)).Value = TextBox1.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@Shortdate", SqlDbType.Date)).Value = LBLSHORTDATE.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@CombinationSN", SqlDbType.NVarChar, 50)).Value = txtCombinationSN.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@ProductName", SqlDbType.NVarChar, 50)).Value = txtModelname.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@CPN", SqlDbType.NVarChar, 50)).Value = txtCPN.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@AgileREV", SqlDbType.NChar, 10)).Value = txtAgileRev.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@EngREV", SqlDbType.NChar, 10)).Value = txtEngRev.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@DateCode", SqlDbType.NChar, 10)).Value = txtDatecode.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@Capacity", SqlDbType.NChar, 10)).Value = txtCapacity.Text.Trim
        If txtCustomer.Text = "PURE STORAGE" And txtProduct.Text.StartsWith("84") = True Then
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

            strs = txtMac.Text.Trim
            insStr = ":"
            strRes = strs.Insert(2, insStr)
            strRes1 = strRes.Insert(5, insStr)
            strRes2 = strRes1.Insert(8, insStr)
            strRes3 = strRes2.Insert(11, insStr)
            strRes4 = strRes3.Insert(14, insStr)
            strRes5 = strRes4.Insert(17, insStr)
            strRes6 = strRes5.Insert(20, insStr)

            cmd.Parameters.Add(New SqlParameter("@Mac_Convert", SqlDbType.NVarChar, 50)).Value = strRes6
        Else
            cmd.Parameters.Add(New SqlParameter("@Mac_Convert", SqlDbType.NVarChar, 50)).Value = ""
        End If

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
        LoadReprintlabel_List_byControlNo()
        txtSerial.Text = ""
        txtMac.Text = ""
        txtMac2.Text = ""
        txtProcessID.Text = ""
        txtProcessName.Text = ""
        txtCombinationSN.Text = ""

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
    Private Sub LoadReprintlabel_List_byWO()
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
    Private Sub LoadReprintlabel_List_byControlNo()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "spGetSN_Re_Printed_by_ControlNo'" & Me.txtPrintControlNo.Text.Trim & "'"
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
        Dim dr As SqlDataReader
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
        conn.Open()
        dr = cmd.ExecuteReader
        While dr.Read
            Dim Permission As String = dr("Permission").ToString()
            If Permission.StartsWith("Admin") Then
                DeleteSN()
            Else
                MsgBox("BẠN KHÔNG ĐƯỢC SỬ DỤNG CHỨC NĂNG NÀY. HÃY LIÊN LẠC VỚI QUẢN LÝ CỦA BẠN ĐỂ XỬ LÝ", vbCritical + vbOKOnly)
                ' Me.Close()
            End If
        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()
        LoadReprintlabel_List_byWO()
    End Sub

    Private Sub DeleteSN()
        conn = New SqlConnection(Strconn) ' connect to Database
        cmd = New SqlCommand("sptblSN_RE_Print_Delete", conn) ' Ket noi vao datatable cua Store Procedure
        cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

        cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NVarChar, 50)).Value = Me.txtSerial.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@PrintControlNo", SqlDbType.NChar, 10)).Value = Me.txtPrintControlNo.Text.Trim

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

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

        'Load re_reprint_times
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("sptblSN_RE_Printed_Count_byControlNo'" & Me.txtPrintControlNo.Text.Trim & "'", conn) ' Ket noi vao datatable cua Store Procedure
        conn.Open()
        Dim dr1 As SqlDataReader
        dr1 = cmd.ExecuteReader()
        While dr1.Read
            txtRe_print_times.Text = CInt(dr1("Re_print_times").ToString.Trim + 1)
        End While
        dr1.Close()
        cmd.Dispose()
        conn.Close()
        LoadReprintlabel_List_byControlNo()
        'Excelfile_Export()
        'Load Permision
        Dim dr As SqlDataReader
        conn = New SqlConnection(Strconn)
        cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
        conn.Open()
        dr = cmd.ExecuteReader
        While dr.Read
            Dim Permission As String = dr("Permission").ToString()
            If Permission.StartsWith("User") And txtRe_print_times.Text > 1 Then
                MsgBox("BẠN KHÔNG ĐƯỢC PHÉP IN LẠI LẦN 2. HÃY XÁC NHẬN VỚI QUẢN LÝ ĐỂ IN LẠI", vbCritical + vbOKOnly)
            Else

                Excelfile_Export()
                SNRe_Print_Record()
            End If
        End While
        dr.Close()
        cmd.Dispose()
        conn.Close()
        LoadReprintlabel_List_byControlNo()

    End Sub
    Private Sub Excelfile_Export()
        If ((Me.DataGridView1.Columns.Count = 0) Or (Me.DataGridView1.Rows.Count = 0)) Then
            Exit Sub
        End If

        'Creating dataset to export
        Dim dset As New DataSet
        'add table to dataset
        dset.Tables.Add()
        'add all column to that table
        'For i As Integer = 0 To Me.DataGridView1.ColumnCount - 1
        '    dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(i).HeaderText)

        'Next
        'Add specific columns to excel
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(0).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(1).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(2).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(3).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(4).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(10).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(12).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(15).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(16).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(17).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(18).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(19).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(20).HeaderText)
        dset.Tables(0).Columns.Add(Me.DataGridView1.Columns(21).HeaderText)
        'add rows to the table
        Dim dr1 As DataRow
        For i As Integer = 0 To DataGridView1.RowCount - 1
            dr1 = dset.Tables(0).NewRow

            'export all selected columns to excel
            'For j As Integer = 0 To DataGridView1.Columns.Count - 1
            '    dr1(j) = DataGridView1.Rows(i).Cells(j).Value

            'Next
            Try
                'export specific columns to excel
                dr1(0) = DataGridView1.Rows(i).Cells("WO").Value
                dr1(1) = DataGridView1.Rows(i).Cells("SN").Value
                dr1(2) = DataGridView1.Rows(i).Cells("Mac_1").Value
                dr1(3) = DataGridView1.Rows(i).Cells("Mac_2_WWN").Value
                dr1(4) = DataGridView1.Rows(i).Cells("PN").Value
                dr1(5) = DataGridView1.Rows(i).Cells("PrintControlNo").Value
                dr1(6) = DataGridView1.Rows(i).Cells("AdditionAction").Value
                dr1(7) = DataGridView1.Rows(i).Cells("ProductName").Value
                dr1(8) = DataGridView1.Rows(i).Cells("CPN").Value
                dr1(9) = DataGridView1.Rows(i).Cells("AgileREV").Value
                dr1(10) = DataGridView1.Rows(i).Cells("EngREV").Value
                dr1(11) = DataGridView1.Rows(i).Cells("DateCode").Value
                dr1(12) = DataGridView1.Rows(i).Cells("Capacity").Value
                dr1(13) = DataGridView1.Rows(i).Cells("Mac_Convert").Value

            Catch ex As IndexOutOfRangeException
                Console.WriteLine("IndexOutOfRangeException caught")

            End Try

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

    Private Sub EXITToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EXITToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub SNRe_Print_Record()

        'update re_print_times
        conn = New SqlConnection(Strconn) ' connect to Database
        cmd = New SqlCommand("sptblSN_RE_Print_Update", conn) ' Ket noi vao datatable cua Store Procedure
        cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

        cmd.Parameters.AddWithValue("Re_print_times", Me.txtRe_print_times.Text.Trim)
        cmd.Parameters.AddWithValue("PrintControlNo", Me.txtPrintControlNo.Text.Trim)

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            MsgBox("ĐÃ XUẤT DANH SÁCH SỐ SERIAL CẦN IN LẠI")
        Catch ex As SqlException
            conn.Close()
            MsgBox(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            conn = New SqlConnection(Strconn)

            Me.StrSql = "spGetSNRePrint_bydate'" & Format(Me.DateTimePicker1.Value, "MM/dd/yyyy") & "'" & "," & "'" & Format(Me.DateTimePicker2.Value, "MM/dd/yyyy") & "'"

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
End Class