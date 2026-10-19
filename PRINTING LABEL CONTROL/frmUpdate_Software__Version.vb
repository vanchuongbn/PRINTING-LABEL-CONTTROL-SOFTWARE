Imports System.Data.SqlClient
Imports System.IO
Public Class frmUpdate_Software__Version
    Dim filesize As Byte()
    Dim Filebyte As Byte()
    Dim filename As String
    Dim dt As New DataTable
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Private Sub frmUpdate_Software__Version_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtAssyName.Text = Application.ProductName.ToString()
        txtAssyVersion.Text = Application.ProductVersion.ToString()
        lblMessage.Visible = False
        Try
            conn = New SqlConnection(Strconn)
            Dim da As New SqlDataAdapter("select top 1 AssemblyVersion,AssemblyName,FileSize from TblSoftFiles where AssemblyName = '" + txtAssyName.Text.Trim + "' order by AssemblyVersion desc", conn)
            conn.Open()
            dt.Clear()
            da.Fill(dt)
            conn.Close()
            If dt.Rows.Count > 0 Then
                txtnewVersion.Text = dt.Rows(0).Item("AssemblyVersion").ToString()
                If txtAssyVersion.Text <> txtnewVersion.Text Then
                    lblMessage.Text = "Please click download button to download !"
                    lblMessage.ForeColor = Color.Blue
                    lblMessage.Visible = True
                Else
                    lblMessage.Visible = False
                    Button2.Enabled = False
                End If
            Else
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim ext As String = Path.GetExtension(dt.Rows(0).Item("AssemblyName"))
            Dim sFilePath As String
            sFilePath = Path.GetFileName(dt.Rows(0).Item("AssemblyName"))
            Filebyte = CType(dt.Rows(0).Item("FileSize"), Byte())
            SaveFileDialog1.FileName = sFilePath & " Version " & txtnewVersion.Text.ToString()
            SaveFileDialog1.Filter = "Exe Files (*.exe)|*.exe"
            SaveFileDialog1.ShowDialog()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        filename = SaveFileDialog1.FileName
        System.IO.File.WriteAllBytes(filename, Filebyte)
        Dim sFilePath As String = System.IO.Path.GetFullPath(filename)
        Me.Hide()
        If CheckBox1.Checked Then
            Using p As New System.Diagnostics.Process
                p.StartInfo = New System.Diagnostics.ProcessStartInfo(sFilePath)
                p.Start()
            End Using
        Else
        End If
    End Sub
End Class