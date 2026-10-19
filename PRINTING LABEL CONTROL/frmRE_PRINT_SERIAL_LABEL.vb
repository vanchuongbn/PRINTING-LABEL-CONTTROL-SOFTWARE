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

Public Class frmPRINTLABEL
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
        ' LoadbyWO()
        txtMac.Enabled = False
        txtSerial.Enabled = False
        cbbtype.Text = ""
        'txtProcessID.Hide()
        'txtProcessName.Hide()
        'txtCustomer.Hide()
        'txtModelname.Hide()
        'txtProduct.Hide()
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
            Else
                txtWO.Focus()
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
            Loadinformation1()
            Loadinformation2()
            Datecode()
            LoadLabel()
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
        sqladap = New SqlDataAdapter("Select WO,SN from SN_WO where SN='" & Me.txtSerial.Text.Trim & "'", conn2) '"'and WO='" & txtWO.Text.Trim & 
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
            ' Exit Sub
        Else
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
        txtDate.Text = DateTimePicker1.Value.ToShortDateString

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
End Class