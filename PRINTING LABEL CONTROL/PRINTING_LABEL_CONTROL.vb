Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports System.Data
Imports System.Reflection
Public Class PRINTING_LABEL_CONTROL
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim answer As MsgBoxResult
        answer = MsgBox("Bạn có muốn thoát khỏi chương trình?", MsgBoxStyle.YesNo)
        If answer = MsgBoxResult.Yes Then
            MsgBox("Thoát khỏi chương trình")
            Me.Close()
            Application.Exit()
            End
        End If
    End Sub
    Private Sub TreeView1_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick

        If e.Node.Name = "USERLIST" Then
            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Then
                    Dim f As New frmUser With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    '  f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)
                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()

        ElseIf e.Node.Name = "PURE_STORAGE_New" Then

            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New CRUSHER_SN_CREATE_New With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)
                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()

        ElseIf e.Node.Name = "PURE_STORAGE" Then

            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New CRUSHER_SN_CREATE With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)
                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()

        ElseIf e.Node.Name = "EFI" Then
            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New EFI_SN_CREATE With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()
        ElseIf e.Node.Name = "SHOPVAC" Then
            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New SHOPVAC_SN_CREATE With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()

        ElseIf e.Node.Name = "FADU" Then
            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New FADU_SN_CREATE With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()

        ElseIf e.Node.Name = "AGIGA" Then
            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New AGIGA_SN_CREATE With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()

        ElseIf e.Node.Name = "SEAGATE" Then

            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New SEAGATE_SN_CREATE_NEW With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()

        ElseIf e.Node.Name = "LUCID" Then

            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New LUCID_SN_CREATE With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()

        ElseIf e.Node.Name = "AIC" Then

            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New frmAIC_CREATE With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()

        ElseIf e.Node.Name = "Node_AIC_New" Then

            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New frmAIC_CREATE_New With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()

        ElseIf e.Node.Name = "AiczelusskuBonly" Then
            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New frmAIC_ZelusSkuB_CREATE With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()
        ElseIf e.Node.Name = "Rototec" Then
            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New Rototec_Create With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox("Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()
        ElseIf e.Node.Name = "Ford_SN_Create" Then
            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New Ford_Artoo_CreateSN With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox("Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()
        ElseIf e.Node.Name = "IMEDRIX" Then

            Panel5.Controls.Clear()
            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select Permission from tblUser where Username='" & LoginUser & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Permission As String = dr("Permission").ToString()
                If Permission.StartsWith("Admin") Or Permission.StartsWith("ME") Then
                    Dim f As New IMEDRIX_SN_CREATE With {.TopLevel = False, .AutoSize = False}
                    f.Parent = Panel5
                    'f.Dock = DockStyle.Fill
                    f.Show()
                Else
                    MsgBox(" Bạn không được sử dụng chức năng này", vbCritical + vbOKOnly)

                    Panel5.Controls.Clear()
                End If
            End While
            dr.Close()
            cmd.Dispose()
            conn.Close()


        ElseIf e.Node.Name = "NEW_PRINT" Then
            Panel5.Controls.Clear()
            Dim f As New frmNEW_SERIAL_PRINTING With {.TopLevel = False, .AutoSize = False}
            f.Parent = Panel5
            f.Dock = DockStyle.Fill
            f.Show()
        ElseIf e.Node.Name = "RE_PRINT" Then
            Panel5.Controls.Clear()
            Dim f As New frmRE_PRINT_SERIAL_LABEL With {.TopLevel = False, .AutoSize = False}
            f.Parent = Panel5
            f.Dock = DockStyle.Fill
            f.Show()


        End If

    End Sub

    Private Sub PRINTING_LABEL_CONTROL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = "Version - " & Application.ProductVersion
    End Sub

End Class