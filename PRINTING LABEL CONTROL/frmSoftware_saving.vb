Imports System.Data.SqlClient

Public Class frmSoftware_saving
    Dim filesize As Byte()
    Dim filename As String
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Private Sub frmSoftware_saving_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtAssyName.Text = Application.ProductName.ToString()
        txtAssyVersion.Text = Application.ProductVersion.ToString()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtAssyName.Text = "" Then
            MsgBox("Missing Assembly Name")
            Exit Sub
        End If
        If txtAssyVersion.Text = "" Then
            MsgBox("Missing Assembly Version")
            Exit Sub
        End If
        If txtFilepath.Text = "" Then
            MsgBox("Missing File Path can not blank")
            Exit Sub
        End If
        If ComboBox1.Text = "" Then
            MsgBox("Missing Critical Update")
            Exit Sub
        End If
        If filesize Is Nothing Then
            MsgBox(".exe Files is not attached yet")
            Exit Sub
        End If

        Try
            conn = New SqlConnection(Strconn)
            Dim cmd = New SqlCommand("spTblSoftFiles", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@AssemblyName", txtAssyName.Text.Trim)
            cmd.Parameters.AddWithValue("@AssemblyVersion", txtAssyVersion.Text.Trim)
            cmd.Parameters.AddWithValue("@FileSizes", filesize)
            cmd.Parameters.AddWithValue("@Critical", ComboBox1.Text.Trim)
            cmd.Connection = conn
            conn.Open()
            cmd.ExecuteReader()
            conn.Close()
            MsgBox("Saved successfully!")
            Me.Hide()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fd As New OpenFileDialog()
        fd.Filter = "Exe Files (*.exe)|*.exe"
        If fd.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Dim filebyte As Byte() = Nothing
            Dim cmd As SqlCommand = Nothing
            filesize = System.IO.File.ReadAllBytes(fd.FileName)
            txtFilepath.Text = System.IO.Path.GetFullPath(fd.FileName)
            filename = System.IO.Path.GetFileName(fd.FileName)
        Else
            MsgBox("Can not attach file")
        End If
    End Sub
End Class