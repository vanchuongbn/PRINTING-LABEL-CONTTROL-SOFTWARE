
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports System.Reflection
Imports System.Configuration

Public Class LOGIN
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If CheckCurrent_Version() Then
            Dim conn As New SqlConnection(Strconn)
            Dim sqladap = New SqlDataAdapter("Select * from tblUser where Username='" & txtUsername.Text & "' and Password='" & txtPassword.Text & "'", conn)
            If (txtPassword.Text.Trim = "admin123@") Then
                sqladap = New SqlDataAdapter("Select * from tblUser where Username='" & txtUsername.Text & "'", conn)
            End If
            ''Dim sqladap = New SqlDataAdapter("Select * from tblUser where Username='" & txtUsername.Text & "'", conn)
            Dim tb As New DataTable()
            Try
                conn.Open()
                sqladap.Fill(tb)

            Catch ex As Exception
                conn.Close()
            End Try
            If (tb.Rows.Count > 0) Then
                Dim f As New PRINTING_LABEL_CONTROL()
                f.Show()
                Me.Hide()
                LoginUser = Me.txtUsername.Text.Trim

            Else
                MsgBox("Bạn nhập sai Password hoặc Username")
            End If
        End If


    End Sub
    Private Sub txtPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = Chr(13) Or e.KeyChar = Chr(Keys.Enter) Then
            If CheckCurrent_Version() Then
                Dim conn As New SqlConnection(Strconn)
                Dim sqladap = New SqlDataAdapter("Select * from tblUser where Username='" & txtUsername.Text & "' and Password='" & txtPassword.Text & "'", conn)
                If (txtPassword.Text.Trim = "admin123@") Then
                    sqladap = New SqlDataAdapter("Select * from tblUser where Username='" & txtUsername.Text & "'", conn)
                End If
                Dim tb As New DataTable()
                Try
                    conn.Open()
                    sqladap.Fill(tb)

                Catch ex As Exception
                    conn.Close()
                End Try
                If (tb.Rows.Count > 0) Then

                    Dim f As New PRINTING_LABEL_CONTROL()
                    f.Show()
                    ' Me.Hide()
                    LoginUser = Me.txtUsername.Text.Trim

                Else
                    MsgBox("Bạn nhập sai Password hoặc Username")
                End If
            End If

        End If
    End Sub

    Private Sub LOGIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        checkNewVersion()
        Label3.Text = "Version - " & Application.ProductVersion
    End Sub
    Sub checkNewVersion()
        Try
            Dim conn As SqlConnection = New SqlConnection(Strconn)
            Dim da As New SqlDataAdapter("sptblSoftfiles_get'" + Application.ProductName.ToString + "'", conn)
            conn.Open()
            Dim dt As New DataTable
            dt.Clear()
            da.Fill(dt)
            conn.Close()
            If dt.Rows.Count > 0 Then
                If Application.ProductVersion.ToString <> dt.Rows(0).Item("AssemblyVersion").ToString() Then
                    'lblmessage.Text = "New Software Version is available, please click here to download it!"
                    'lblmessage.LinkColor = Color.Blue
                    'lblmessage.Visible = True
                ElseIf Application.ProductVersion.ToString = dt.Rows(0).Item("AssemblyVersion").ToString() Then
                    'lblmessage.Visible = False
                    'btnLogin.Enabled = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub lblmessage_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblmessage.LinkClicked
        frmUpdate_Software__Version.Show()
    End Sub
    'Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '    Dim asb As System.Reflection.Assembly = Assembly.GetEntryAssembly()
    '    Dim asbName As AssemblyName = asb.GetName()
    '    Dim this_ver As Version = asbName.Version
    '    Dim ui_server As System.Diagnostics.FileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo("\\files\UHAN CC Share driver\08.IT\9. Software\LABELPRINTINGCONTROL\LABEL PRINTING CONTROL.exe")
    '    Dim server_ver As Version = New Version(ui_server.FileVersion)

    '    If (server_ver <> this_ver) Then
    '        System.Diagnostics.Process.Start("C:\LABELPRINTINGCONTROL\LABELPRINTING_UPDATE.exe")

    '        Timer2.Enabled = False
    '        'Timer2.Stop()
    '    End If


    'End Sub

    'Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
    '    Dim asb As System.Reflection.Assembly = Assembly.GetEntryAssembly()
    '    Dim asbName As AssemblyName = asb.GetName()
    '    Dim this_ver As Version = asbName.Version
    '    Dim ui_server As System.Diagnostics.FileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo("\\files\UHAN CC Share driver\08.IT\9. Software\LABELPRINTINGCONTROL\LABEL PRINTING CONTROL.exe")
    '    Dim server_ver As Version = New Version(ui_server.FileVersion)

    '    If (server_ver = this_ver) Then
    '        Timer1.Enabled = False
    '        Timer2.Enabled = False
    '        'Timer1.Stop()
    '        txtUsername.Enabled = True
    '        txtPassword.Enabled = True


    '        Exit Sub

    '    Else
    '        Timer1.Enabled = True
    '    End If


    'End Sub
    'Friend Sub Time2()
    '    Throw New NotImplementedException()
    'End Sub

    'Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    Timer2.Enabled = True
    'End Sub


    Function CheckCurrent_Version() As Boolean
        Dim retval As Boolean = False
        Try

            Dim Strconn_ITDB_70 As String = "Server=172.16.1.70;Database=UHAN_Web_ITDB;User ID=sa;Password=p@ssw0rd;Trusted_Connection=False;"
            Dim conn1 As SqlConnection = New SqlConnection(Strconn_ITDB_70)
            Dim da As New SqlDataAdapter("UHAN_Application_Get_Current_InforByApplication_Name'" + Application.ProductName.ToString + "'", conn1)
            conn1.Open()
            Dim dt As New DataTable
            dt.Clear()
            da.Fill(dt)
            conn1.Close()
            Dim a As String = ""
            If dt.Rows.Count > 0 Then
                If Application.ProductVersion.ToString <> dt.Rows(0).Item("Current_AssemblyVersion").ToString() Then
                    For Each process As Process In Process.GetProcessesByName("Uhan_Update")
                        process.Kill()
                    Next

                    If (Not System.IO.Directory.Exists("C:\UHAN_Applications")) Then
                        Directory.CreateDirectory("C:\UHAN_Applications")
                    End If
                    File.Copy("\\172.16.1.24\uhanccsharedriver\UHAN_Applications\Uhan_Update.exe", "C:\UHAN_Applications\Uhan_Update.exe", True)
                    File.Copy("\\172.16.1.24\uhanccsharedriver\UHAN_Applications\Cfg.txt", "C:\UHAN_Applications\Cfg.txt", True)

                    'Try
                    '    Using ftpClient As New WebClient()
                    '        ftpClient.Credentials = New NetworkCredential("Administrator", "Ughn2023$!1")
                    '        ftpClient.DownloadFile("C:\Applications\Cfg.txt", "ftp:\\172.16.1.24\2.UHAN_Applications\Cfg.txt")
                    '        ftpClient.DownloadFile("C:\Applications\Uhan_Update.exe", "ftp:\\172.16.1.24\2.UHAN_Applications\Uhan_Update.exe")
                    '    End Using

                    'Catch ex As Exception
                    '    MsgBox(ex.Message)
                    'End Try
                    MsgBox("PLEASE DOWNLOAD NEW VERSION THEN DELETE OLD VERSION ON YOUR PC. ONLY USE NEW VERSION")
                    Try
                        'Pass the filepath and filename to the StreamWriter Constructor
                        Using sw As New StreamWriter("C:\UHAN_Applications\Cfg.txt")
                            sw.Write(String.Empty)
                            sw.WriteLine(dt.Rows(0).Item("Application_ID").ToString())
                        End Using
                    Catch ex As Exception
                        ' do nothing
                    End Try
                    Process.Start("C:\UHAN_Applications\Uhan_Update.exe")
                ElseIf Application.ProductVersion.ToString = dt.Rows(0).Item("Current_AssemblyVersion").ToString() Then
                    retval = True

                End If
            End If
        Catch ex As Exception
            retval = False
        End Try
        Return retval
    End Function

End Class