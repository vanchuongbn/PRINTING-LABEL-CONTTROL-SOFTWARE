Imports System.Data.SqlClient

Public Class DAL_Ford_CreateSN

    Public Function CreateSN_List_By_WO(ByVal WO As String) As DataTable
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

    Public Function GetLast_50_SN_By_WO(ByVal Type As Integer, ByVal Y_and_D_Number As Integer, ByVal PN As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim Str_Querry As String = "select top 50 Sequence, SN, PN, WO, Year_And_DateNum, Log_time, Maked_Date, 
            from tblFord_SN_Log
            where [Type] = @Type and PN = @PN and Year_And_DateNum = @Year_And_DateNum
            order by [Sequence] desc"
        Dim cn As SqlConnection = New SqlConnection(Strconn)
        cn.Open()
        Try
            Dim cmd As SqlCommand = New SqlCommand(Str_Querry, cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = Type
            cmd.Parameters.Add("@PN", SqlDbType.NChar).Value = PN
            cmd.Parameters.Add("@Year_And_DateNum", SqlDbType.Int).Value = Y_and_D_Number
            Dim adap As SqlDataAdapter = New SqlDataAdapter(cmd)
            adap.Fill(dt)
        Catch ex As Exception
        Finally
            cn.Dispose()
        End Try

        Return dt

    End Function

    Public Function Ford_CreateSN(ByVal Type As Integer, ByVal WO As String, ByVal Y_and_D_Number As String, ByVal PN As String,
                                  ByVal Maked_Date As String, ByVal Maked_By As String, ByVal Qty As Integer, ByVal Supplier_Code As String,
                                  ByVal Customer_Assembly_Code As String, ByVal Type_Str As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim Str_Querry As String = "Ford_CreateSN"
        Dim cn As SqlConnection = New SqlConnection(Strconn)
        cn.Open()
        Try
            Dim cmd As SqlCommand = New SqlCommand(Str_Querry, cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = Type
            cmd.Parameters.Add("@WO", SqlDbType.NChar).Value = WO
            cmd.Parameters.Add("@PN", SqlDbType.NVarChar).Value = PN
            cmd.Parameters.Add("@Year_And_DateNum", SqlDbType.NChar).Value = Y_and_D_Number
            cmd.Parameters.Add("@Maked_Date", SqlDbType.Date).Value = Maked_Date
            cmd.Parameters.Add("@Maked_By", SqlDbType.VarChar).Value = Maked_By
            cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = Qty
            cmd.Parameters.Add("@Supplier_Code", SqlDbType.NVarChar).Value = Supplier_Code
            cmd.Parameters.Add("@Customer_Assembly_Code", SqlDbType.NVarChar).Value = Customer_Assembly_Code
            cmd.Parameters.Add("@Type_Str", SqlDbType.NVarChar).Value = Type_Str
            Dim adap As SqlDataAdapter = New SqlDataAdapter(cmd)
            adap.Fill(dt)
        Catch ex As Exception
        Finally
            cn.Dispose()
        End Try

        Return dt

    End Function

    Public Function Ford_GetHistory_SN(ByVal Type As Integer, ByVal WO As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim Str_Querry As String = "select ID, Type, Log_time, WO, PN, Year_And_DateNum, SN, [Sequence]
        from tblFord_SN_Log where WO = @WO and Type = @Type
        order by Sequence"
        Dim cn As SqlConnection = New SqlConnection(Strconn)
        cn.Open()
        Try
            Dim cmd As SqlCommand = New SqlCommand(Str_Querry, cn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = Type
            cmd.Parameters.Add("@WO", SqlDbType.NChar).Value = WO
            Dim adap As SqlDataAdapter = New SqlDataAdapter(cmd)
            adap.Fill(dt)
        Catch ex As Exception
        Finally
            cn.Dispose()
        End Try

        Return dt

    End Function

End Class
