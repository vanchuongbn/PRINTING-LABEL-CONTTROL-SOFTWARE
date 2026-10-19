Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports System.Data
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports Excel = Microsoft.Office.Interop.Excel
Imports System
Imports System.Globalization
Imports System.Threading
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Diagnostics
Public Class frmLABEL_TEMPLATE
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim obj As ListItem
    Dim dr As SqlDataReader
    Private Sub frmLABEL_TEMPLATE_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        RECORD()

    End Sub
    Private Sub RECORD()
        ModVar.NX = "N"

        Try
            conn = New SqlConnection(ModVar.Strconn_Mark)
            cmd = New SqlCommand("tblLABEL_TEMPLATE_Add", conn)
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            conn.Open()

            For i = 0 To DataGridView1.RowCount() - 2

                cmd.Parameters.Add(New SqlParameter("@PRODUCTNO", Me.DataGridView1.Item(0, i).Value.ToString.Trim))
                cmd.Parameters.Add(New SqlParameter("@LABEL_NAME", txtLabelName.Text.ToString().Trim()))
                cmd.Parameters.Add(New SqlParameter("@LABEL_VERSION", txtVersion.Text.ToString().Trim()))
                cmd.Parameters.Add(New SqlParameter("@STATUS", cbbstatus.Text.ToString().Trim()))
                cmd.Parameters.Add(New SqlParameter("@PATH_LINE", txtAttached_file.Text.ToString().Trim()))

                cmd.Parameters.Add(New SqlParameter("@USER_NAME", LOGIN.txtUsername.Text.ToString().Trim()))
                cmd.Parameters.Add(New SqlParameter("@UPDATE_DATE", DateTimePicker1.Value))
                cmd.Parameters.Add(New SqlParameter("@REMARK", txtRemark.Text.ToString().Trim()))

                cmd.Parameters.Add(New SqlParameter("@nx", ModVar.NX.ToString().Trim()))
                cmd.ExecuteNonQuery()
                conn.Dispose()
                conn.Close()
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim fd As New OpenFileDialog()
        fd.Filter = "All files (*.*)|*.*" '"pdf file|*.pdf; excel file|*.xls; excel files(*.xlsx)|*.xlsx; word files(*.doc)|*.doc; word files(*.docx)|*.docx;"
        If fd.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Dim filebyte As Byte() = Nothing
            Dim cmd As SqlCommand = Nothing
            filebyte = System.IO.File.ReadAllBytes(fd.FileName)
            Me.txtAttached_file.Text = fd.FileName
        End If
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click

        'conn = New SqlConnection(Strconn)
        'Dim da As New SqlDataAdapter()
        'Dim ds As New DataSet()
        'StrSql = "Get_Attached_file_RMA " & "'" & Me.txtRMA.Text.Trim & "','" & Me.cbbSeq.Text.Trim & "'"
        'da = New SqlDataAdapter(StrSql, conn)
        'da.Fill(ds, "tbl")

        'Dim type As String = txtAttached_file.Text.Trim

        'If type.IndexOf("xlsx") >= 0 Then

        '    Dim strExtenstion As String = ".xlsx"
        '    Try
        '        Dim bytFile As Byte() = CType(ds.Tables(0).Rows(0).Item("Attach_1"), Byte())
        '        Dim sFilePath As String
        '        sFilePath = System.IO.Path.GetTempFileName()
        '        System.IO.File.Move(sFilePath, System.IO.Path.ChangeExtension(sFilePath, ".xlsx"))
        '        sFilePath = System.IO.Path.ChangeExtension(sFilePath, ".xlsx")
        '        System.IO.File.WriteAllBytes(sFilePath, bytFile)
        '        Using p As New System.Diagnostics.Process
        '            p.StartInfo = New System.Diagnostics.ProcessStartInfo(sFilePath)
        '            p.Start()
        '            p.WaitForExit()
        '            Try
        '                System.IO.File.Delete(sFilePath)
        '            Catch
        '            End Try
        '        End Using
        '    Catch ex As Exception
        '        MsgBox("Không có file đính kèm")
        '    End Try
        'End If

    End Sub
End Class