Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports System.Data
Public Class frmUser
    Dim conn As SqlConnection = New SqlConnection(Strconn)
    Dim cmd As SqlCommand
    Dim sqladap = New SqlDataAdapter
    Dim ConnSql1 As SqlConnection
    Dim StrSql As String = ""
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Private Sub frmUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        frmUser_Load()
    End Sub
    Private Sub frmUser_Load()
        Try
            conn = New SqlConnection(Strconn)
            Me.StrSql = "sptblUser_Load"

            Dim cmd As New SqlCommand(Me.StrSql, conn)
            da = New SqlDataAdapter(cmd)
            Dim cmb As New SqlCommandBuilder(da)
            ds = New DataSet
            da.Fill(ds, "tblUser")
            conn.Close()
            Dim Dtv As DataView = New DataView(ds.Tables("tblUser"))
            With DGVUser
                .DataSource = Dtv
                .Rows(0).Selected = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            End With
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message & StrSql)
        End Try
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        If txtUsername.Text = "" Or txtPassword.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Bạn chưa nhập Username hoặc Password")
        Else
            tblUser_add() ' call store function by shortcut with Private sub name
            conn.Close()

        End If
    End Sub
    Private Sub tblUser_add()
        conn = New SqlConnection(Strconn) ' connect to Database
        cmd = New SqlCommand("sptblUser_add ", conn) ' Ket noi vao datatable cua Store Procedure
        cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

        cmd.Parameters.Add(New SqlParameter("@Username", SqlDbType.NChar, 20)).Value = Me.txtUsername.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@Password", SqlDbType.NChar, 50)).Value = Me.txtPassword.Text.Trim
        cmd.Parameters.Add(New SqlParameter("@Permission", SqlDbType.NChar, 20)).Value = Me.ComboBox1.Text.Trim

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            MsgBox("Hoàn thành nhập")
        Catch ex As SqlException
            conn.Close()
            MsgBox("Không thể nhập")
            Exit Sub
        End Try
        frmUser_Load()
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If Me.txtUsername.Text = "" Or Me.txtPassword.Text = "" Then
            MsgBox("Bạn chưa chon hang muc de xoa ")
            Me.txtUsername.Focus()
        Else

            Dim sqladap = New SqlDataAdapter("Select Username from tblUser where Username='" & Me.txtUsername.Text & "'", conn)
            Dim tb As New DataTable()
            Try
                conn.Open()
                sqladap.Fill(tb)
                conn.Close()
            Catch ex As Exception
                conn.Close()
            End Try
            If (tb.Rows.Count = 0) Then
                MsgBox("User này ko tồn tại")
                Me.txtUsername.Text = ""
                Me.txtUsername.Focus()
            Else
                tblUser_delete()
            End If

        End If


        Me.txtUsername.Text = ""
        Me.txtPassword.Text = ""
        Me.ComboBox1.Text = ""
    End Sub
    Private Sub tblUser_delete()
        conn = New SqlConnection(Strconn) ' connect to Database
        cmd = New SqlCommand("sptblUser_Delete", conn) ' Ket noi vao datatable cua Store Procedure
        cmd.CommandType = CommandType.StoredProcedure ' Goi lenh store procedure

        cmd.Parameters.Add(New SqlParameter("@Username", SqlDbType.NChar, 20)).Value = Me.txtUsername.Text.Trim

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
        frmUser_Load()
    End Sub
    Private Sub DGVUser_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUser.CellContentClick
        Dim i As Integer
        i = DGVUser.CurrentRow.Index
        Me.txtUsername.Text = DGVUser.Item(0, i).Value.ToString()
        Me.txtPassword.Text = DGVUser.Item(1, i).Value.ToString()
        Me.ComboBox1.Text = DGVUser.Item(2, i).Value.ToString()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()

    End Sub
End Class