Imports System.Data.SqlClient
Imports System.Globalization

Public Class DAL_Common

    Public Function createSN_List_ACIWithNewPN(ByVal Start_SN As Integer, ByVal End_SN As Integer, ByVal txtDate As String, ByVal txtWO As String,
            ByVal txtPN As String, ByVal txtDatecode As String, ByVal txtPO As String, ByVal txtPO_Item As String, ByVal Fixed_Characts As String) As Integer

        Dim Retval As Integer = 1
        Dim i As Integer
        conn = New SqlConnection(Strconn)
        conn.Open()
        Dim cmd As New SqlCommand
        Dim cmd1 As New SqlCommand
        Dim MAC_B As String = ""
        Dim SN_B As String = ""
        Dim SN As String = ""
        Dim baseCode As String = ""
        Dim yearSuffix As String = Now.Year.ToString().Substring(2, 2)
        Dim lastCode As String = ""
        Dim lastSeqStr As String = ""
        Dim Str_sql As String = ""
        Try
            For i = Start_SN To End_SN

                cmd = New SqlCommand("sptblAIC_SN_Log_add_WithNewPN", conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add(New SqlParameter("@Date", SqlDbType.DateTime)).Value = txtDate
                cmd.Parameters.Add(New SqlParameter("@WO", SqlDbType.NChar, 7)).Value = txtWO
                cmd.Parameters.Add(New SqlParameter("@PN", SqlDbType.NVarChar, 50)).Value = txtPN
                cmd.Parameters.Add(New SqlParameter("@Datecode", SqlDbType.NChar, 4)).Value = txtDatecode
                cmd.Parameters.Add(New SqlParameter("@AIC_PO", SqlDbType.NChar, 13)).Value = txtPO
                cmd.Parameters.Add(New SqlParameter("@PO_Item", SqlDbType.NChar, 1)).Value = txtPO_Item

                cmd.Parameters.Add(New SqlParameter("@Sequence", SqlDbType.Int)).Value = i

                Dim j As String = CStr(i)

                If Len(j) = 1 Then
                    SN = txtPO & txtPO_Item & "000" & i '& "U" & txtDatecode.Text.Trim
                ElseIf Len(j) = 2 Then

                    SN = txtPO & txtPO_Item & "00" & i '& "U" & txtDatecode.Text.Trim
                ElseIf Len(j) = 3 Then

                    SN = txtPO & txtPO_Item & "0" & i '& "U" & txtDatecode.Text.Trim
                ElseIf Len(j) = 4 Then

                    SN = txtPO & txtPO_Item & i ' & "U" & txtDatecode.Text.Trim
                Else
                    Retval = 0
                    Exit For
                End If

                Dim prefix As String = New String("A"c, 16)
                ''Lấy năm hiện tại và tuần trong năm
                Dim now As DateTime = DateTime.Now
                Dim cul As CultureInfo = CultureInfo.CurrentCulture
                Dim cal As Calendar = cul.Calendar

                ' Tuần trong năm (dùng chuẩn ISO như bên C#)
                Dim week As Integer = cal.GetWeekOfYear(now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)

                Dim weekStr As String = week.ToString("D2")

                Dim nextSequence As Integer = 1

                baseCode = Fixed_Characts & yearSuffix & weekStr
                Str_sql = "SELECT MAX(SN_B) FROM tblAIC_SN_log WHERE SN_B LIKE @BaseCode + '%'"
                cmd1 = New SqlCommand(Str_sql, conn)
                cmd1.Parameters.AddWithValue("@BaseCode", baseCode)
                Dim result As Object = cmd.ExecuteScalar()

                If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                    lastCode = result.ToString()
                    lastSeqStr = lastCode.Substring(20, 4)
                    If Integer.TryParse(lastSeqStr, nextSequence) Then
                        nextSequence += 1
                    End If
                End If

                SN_B = baseCode & nextSequence.ToString("D4")
                MAC_B = SN
                cmd.Parameters.Add(New SqlParameter("@SN", SqlDbType.NChar, 23)).Value = SN
                cmd.Parameters.Add(New SqlParameter("@MAC_B", SqlDbType.NVarChar)).Value = MAC_B
                cmd.Parameters.Add(New SqlParameter("@SN_B", SqlDbType.NVarChar)).Value = SN_B

                cmd.ExecuteNonQuery()

            Next


        Catch ex As SqlException
            'MsgBox(ex)
            conn.Close()

        End Try
        Return Retval
    End Function



    Public Function Training_GetListUsersSelectedTodo_OfAllEmpsToDo(ByVal DocNo As String, ByVal Docrev As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim Str_Querry As String = "select ID,EmployeeID,EmployeeName, DocNo, DocRev,NeedToRetrain
                from tblEmployeeList_NeedToTrain_ForNewDoc
				where NeedToRetrain = 1 and DocNo = @DocNo and DocRev = @DocRev"
        Dim cn As SqlConnection = New SqlConnection(Strconn)
        cn.Open()
        Try
            Dim cmd As SqlCommand = New SqlCommand(Str_Querry, cn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@DocNo", SqlDbType.VarChar).Value = DocNo
            cmd.Parameters.Add("@DocRev", SqlDbType.NChar).Value = Docrev
            Dim adap As SqlDataAdapter = New SqlDataAdapter(cmd)
            adap.Fill(dt)
        Catch ex As Exception
        Finally
            cn.Close()
        End Try

        Return dt

    End Function

    Public Function AIC_Zelus_GetMacAvairible(ByVal DocNo As String, ByVal Docrev As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim Str_Querry As String = "select ID,EmployeeID,EmployeeName, DocNo, DocRev,NeedToRetrain
                from tblEmployeeList_NeedToTrain_ForNewDoc
				where NeedToRetrain = 1 and DocNo = @DocNo and DocRev = @DocRev"
        Dim cn As SqlConnection = New SqlConnection(Strconn)
        cn.Open()
        Try
            Dim cmd As SqlCommand = New SqlCommand(Str_Querry, cn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@DocNo", SqlDbType.VarChar).Value = DocNo
            cmd.Parameters.Add("@DocRev", SqlDbType.NChar).Value = Docrev
            Dim adap As SqlDataAdapter = New SqlDataAdapter(cmd)
            adap.Fill(dt)
        Catch ex As Exception
        Finally
            cn.Close()
        End Try

        Return dt

    End Function


    Public Function Get_WO_Information(ByVal WO As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim Str_Querry As String = "spGetWO_infor"
        Dim cn As SqlConnection = New SqlConnection(Strconn)
        cn.Open()
        Try
            Dim cmd As SqlCommand = New SqlCommand(Str_Querry, cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Origin_WO", SqlDbType.NChar).Value = WO
            Dim adap As SqlDataAdapter = New SqlDataAdapter(cmd)
            adap.Fill(dt)
        Catch ex As Exception
        Finally
            cn.Dispose()
        End Try

        Return dt

    End Function



End Class
