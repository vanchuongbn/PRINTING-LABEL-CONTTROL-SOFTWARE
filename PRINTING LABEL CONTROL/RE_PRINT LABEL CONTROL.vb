Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports System.Data
Public Class frmLogin
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Hide()
        Label2.Hide()
        txtPass.Hide()
        txtUser.Hide()
        SettingToolStripMenuItem.Enabled = False
        PrintToolStripMenuItem.Enabled = False

    End Sub
    Private Sub LogInToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogInToolStripMenuItem.Click
        Label1.Show()
        Label2.Show()
        txtPass.Show()
        txtUser.Show()
    End Sub

    Private Sub UserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UserToolStripMenuItem.Click
        frmUser.Show()
    End Sub
    Private Sub txtPass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPass.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then

            Dim dr As SqlDataReader
            conn = New SqlConnection(Strconn)
            cmd = New SqlCommand("Select * from tblUser where Username='" & CStr(txtUser.Text.Trim) & "' and Password='" & CStr(txtPass.Text.Trim) & "'", conn)
            Dim tb As New DataTable()
            conn.Open()
            dr = cmd.ExecuteReader
            Try
                'conn.Open()
                sqladap.Fill(tb)

            Catch ex As Exception
                MsgBox(ex)
                conn.Close()
            End Try
            MsgBox(tb.Rows.Count)
            If (tb.Rows.Count > 0) Then

                While dr.Read
                    Dim Permission As String = dr("Permission").ToString().Trim

                    If Permission = "Admin" Then
                        SettingToolStripMenuItem.Enabled = True
                        PrintToolStripMenuItem.Enabled = True
                        SettingToolStripMenuItem.BackColor = Color.Blue
                        PrintToolStripMenuItem.BackColor = Color.Coral
                        Label1.Hide()
                        Label2.Hide()
                        txtPass.Hide()
                        txtUser.Hide()
                    Else
                        SettingToolStripMenuItem.Enabled = False
                        PrintToolStripMenuItem.Enabled = True
                        Label1.Hide()
                        Label2.Hide()
                        txtPass.Hide()
                        txtUser.Hide()
                    End If
                End While
                dr.Close()
                cmd.Dispose()
                conn.Close()
            Else
                MsgBox("Bạn nhập sai Password hoặc Username")
            End If


        End If

    End Sub
End Class
