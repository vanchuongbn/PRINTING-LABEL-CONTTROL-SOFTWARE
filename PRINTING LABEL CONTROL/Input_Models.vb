Imports System.IO
Imports Newtonsoft.Json


Public Class Input_Models

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Dim jsonData As String = File.ReadAllText("D:\id_729_DFM_Dover_NVME EUI-64_20000_units.json")

        ' Chuyển đổi JSON thành model
        Dim Obj_Info As Class1 = JsonConvert.DeserializeObject(Of Class1)(jsonData)

        lbl_organization_info.Text = Obj_Info.allocated_organization
        Dim gridData As New DataTable()
        gridData.Columns.Add("address")

        For Each listItem As String In Obj_Info.guid_range_base_addresses

            listItem = listItem.Replace(":", "")
            gridData.Rows.Add(listItem)

        Next

        DataGridView1.DataSource = gridData

        TextBox1.Text = Obj_Info.group_allocation_id

        TextBox2.Text = Obj_Info.allocated_site

    End Sub
End Class