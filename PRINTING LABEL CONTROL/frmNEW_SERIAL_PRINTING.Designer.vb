<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmNEW_SERIAL_PRINTING
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtQty_Printed = New System.Windows.Forms.TextBox()
        Me.txtPN = New System.Windows.Forms.TextBox()
        Me.txtPrintdate = New System.Windows.Forms.TextBox()
        Me.txtEnd_SN = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtStart_SN = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtWO_qty = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.EXITToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtPrint_by = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtEnd_SN_Print = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtStart_SN_Print = New System.Windows.Forms.TextBox()
        Me.txtWOQty = New System.Windows.Forms.Label()
        Me.txtPrint_Qty = New System.Windows.Forms.TextBox()
        Me.txtWO = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbbCustomer = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.txtDC = New System.Windows.Forms.TextBox()
        Me.txtProductName = New System.Windows.Forms.TextBox()
        Me.txtAgileREV = New System.Windows.Forms.TextBox()
        Me.txtEngREV = New System.Windows.Forms.TextBox()
        Me.txtCPN = New System.Windows.Forms.TextBox()
        Me.txtCapacity = New System.Windows.Forms.TextBox()
        Me.txtMicron_desc = New System.Windows.Forms.TextBox()
        Me.Update = New System.Windows.Forms.GroupBox()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Update.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnExport
        '
        Me.BtnExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExport.ForeColor = System.Drawing.Color.Blue
        Me.BtnExport.Location = New System.Drawing.Point(682, 127)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(122, 74)
        Me.BtnExport.TabIndex = 39
        Me.BtnExport.Text = "XUẤT DỮ LIỆU IN"
        Me.BtnExport.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(677, 217)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(327, 25)
        Me.Label11.TabIndex = 38
        Me.Label11.Text = "DANH SÁCH SỐ SERIAL CẦN IN"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(123, 217)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(312, 25)
        Me.Label10.TabIndex = 37
        Me.Label10.Text = "DANH SÁCH SỐ SERIAL ĐÃ IN"
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(682, 245)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(668, 422)
        Me.DataGridView2.TabIndex = 36
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(15, 245)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(635, 422)
        Me.DataGridView1.TabIndex = 35
        '
        'txtQty_Printed
        '
        Me.txtQty_Printed.Location = New System.Drawing.Point(471, 63)
        Me.txtQty_Printed.Name = "txtQty_Printed"
        Me.txtQty_Printed.Size = New System.Drawing.Size(179, 20)
        Me.txtQty_Printed.TabIndex = 28
        '
        'txtPN
        '
        Me.txtPN.Location = New System.Drawing.Point(471, 29)
        Me.txtPN.Name = "txtPN"
        Me.txtPN.Size = New System.Drawing.Size(98, 20)
        Me.txtPN.TabIndex = 27
        '
        'txtPrintdate
        '
        Me.txtPrintdate.Location = New System.Drawing.Point(128, 29)
        Me.txtPrintdate.Name = "txtPrintdate"
        Me.txtPrintdate.Size = New System.Drawing.Size(179, 20)
        Me.txtPrintdate.TabIndex = 29
        '
        'txtEnd_SN
        '
        Me.txtEnd_SN.Location = New System.Drawing.Point(128, 177)
        Me.txtEnd_SN.Name = "txtEnd_SN"
        Me.txtEnd_SN.Size = New System.Drawing.Size(179, 20)
        Me.txtEnd_SN.TabIndex = 31
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 181)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 13)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Số Serial kết thúc"
        '
        'txtStart_SN
        '
        Me.txtStart_SN.Location = New System.Drawing.Point(128, 151)
        Me.txtStart_SN.Name = "txtStart_SN"
        Me.txtStart_SN.Size = New System.Drawing.Size(179, 20)
        Me.txtStart_SN.TabIndex = 25
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 155)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Số Serial bắt đầu"
        '
        'txtWO_qty
        '
        Me.txtWO_qty.Location = New System.Drawing.Point(128, 122)
        Me.txtWO_qty.Name = "txtWO_qty"
        Me.txtWO_qty.Size = New System.Drawing.Size(179, 20)
        Me.txtWO_qty.TabIndex = 32
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(334, 131)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(133, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Số bắt đầu của dãy cần in"
        '
        'EXITToolStripMenuItem
        '
        Me.EXITToolStripMenuItem.BackColor = System.Drawing.Color.Red
        Me.EXITToolStripMenuItem.Name = "EXITToolStripMenuItem"
        Me.EXITToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.EXITToolStripMenuItem.Text = "EXIT"
        '
        'txtPrint_by
        '
        Me.txtPrint_by.Location = New System.Drawing.Point(471, 182)
        Me.txtPrint_by.Name = "txtPrint_by"
        Me.txtPrint_by.Size = New System.Drawing.Size(179, 20)
        Me.txtPrint_by.TabIndex = 30
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(334, 186)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 13)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Người in"
        '
        'txtEnd_SN_Print
        '
        Me.txtEnd_SN_Print.Location = New System.Drawing.Point(471, 156)
        Me.txtEnd_SN_Print.Name = "txtEnd_SN_Print"
        Me.txtEnd_SN_Print.Size = New System.Drawing.Size(179, 20)
        Me.txtEnd_SN_Print.TabIndex = 33
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(334, 160)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(116, 13)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Số cuối của dãy cần in"
        '
        'txtStart_SN_Print
        '
        Me.txtStart_SN_Print.Location = New System.Drawing.Point(471, 127)
        Me.txtStart_SN_Print.Name = "txtStart_SN_Print"
        Me.txtStart_SN_Print.Size = New System.Drawing.Size(179, 20)
        Me.txtStart_SN_Print.TabIndex = 26
        '
        'txtWOQty
        '
        Me.txtWOQty.AutoSize = True
        Me.txtWOQty.Location = New System.Drawing.Point(12, 126)
        Me.txtWOQty.Name = "txtWOQty"
        Me.txtWOQty.Size = New System.Drawing.Size(71, 13)
        Me.txtWOQty.TabIndex = 17
        Me.txtWOQty.Text = "Số lượng WO"
        '
        'txtPrint_Qty
        '
        Me.txtPrint_Qty.Location = New System.Drawing.Point(471, 97)
        Me.txtPrint_Qty.Name = "txtPrint_Qty"
        Me.txtPrint_Qty.Size = New System.Drawing.Size(179, 20)
        Me.txtPrint_Qty.TabIndex = 24
        '
        'txtWO
        '
        Me.txtWO.Location = New System.Drawing.Point(128, 92)
        Me.txtWO.Name = "txtWO"
        Me.txtWO.Size = New System.Drawing.Size(179, 20)
        Me.txtWO.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(334, 101)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Số tem cần in đến"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "WO:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(334, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Số lượng đã in"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "KHÁCH HÀNG"
        '
        'cbbCustomer
        '
        Me.cbbCustomer.FormattingEnabled = True
        Me.cbbCustomer.Location = New System.Drawing.Point(128, 62)
        Me.cbbCustomer.Name = "cbbCustomer"
        Me.cbbCustomer.Size = New System.Drawing.Size(179, 21)
        Me.cbbCustomer.TabIndex = 12
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(128, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(179, 20)
        Me.DateTimePicker1.TabIndex = 11
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EXITToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1362, 24)
        Me.MenuStrip1.TabIndex = 10
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(820, 62)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(224, 140)
        Me.PictureBox1.TabIndex = 34
        Me.PictureBox1.TabStop = False
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(682, 96)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(122, 20)
        Me.TextBox4.TabIndex = 40
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(335, 4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 40
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(335, 27)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 40
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(335, 49)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(100, 20)
        Me.TextBox3.TabIndex = 40
        '
        'txtDC
        '
        Me.txtDC.Location = New System.Drawing.Point(575, 29)
        Me.txtDC.Name = "txtDC"
        Me.txtDC.Size = New System.Drawing.Size(98, 20)
        Me.txtDC.TabIndex = 27
        '
        'txtProductName
        '
        Me.txtProductName.Location = New System.Drawing.Point(682, 29)
        Me.txtProductName.Name = "txtProductName"
        Me.txtProductName.Size = New System.Drawing.Size(98, 20)
        Me.txtProductName.TabIndex = 27
        '
        'txtAgileREV
        '
        Me.txtAgileREV.Location = New System.Drawing.Point(786, 29)
        Me.txtAgileREV.Name = "txtAgileREV"
        Me.txtAgileREV.Size = New System.Drawing.Size(59, 20)
        Me.txtAgileREV.TabIndex = 27
        '
        'txtEngREV
        '
        Me.txtEngREV.Location = New System.Drawing.Point(851, 29)
        Me.txtEngREV.Name = "txtEngREV"
        Me.txtEngREV.Size = New System.Drawing.Size(55, 20)
        Me.txtEngREV.TabIndex = 27
        '
        'txtCPN
        '
        Me.txtCPN.Location = New System.Drawing.Point(913, 29)
        Me.txtCPN.Name = "txtCPN"
        Me.txtCPN.Size = New System.Drawing.Size(65, 20)
        Me.txtCPN.TabIndex = 27
        '
        'txtCapacity
        '
        Me.txtCapacity.Location = New System.Drawing.Point(984, 29)
        Me.txtCapacity.Name = "txtCapacity"
        Me.txtCapacity.Size = New System.Drawing.Size(60, 20)
        Me.txtCapacity.TabIndex = 27
        '
        'txtMicron_desc
        '
        Me.txtMicron_desc.Location = New System.Drawing.Point(682, 65)
        Me.txtMicron_desc.Name = "txtMicron_desc"
        Me.txtMicron_desc.Size = New System.Drawing.Size(98, 20)
        Me.txtMicron_desc.TabIndex = 41
        '
        'Update
        '
        Me.Update.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Update.Controls.Add(Me.TextBox8)
        Me.Update.Controls.Add(Me.Label14)
        Me.Update.Controls.Add(Me.Label13)
        Me.Update.Controls.Add(Me.Label12)
        Me.Update.Controls.Add(Me.Button2)
        Me.Update.Controls.Add(Me.Button1)
        Me.Update.Controls.Add(Me.TextBox7)
        Me.Update.Controls.Add(Me.TextBox6)
        Me.Update.Controls.Add(Me.TextBox5)
        Me.Update.Location = New System.Drawing.Point(1050, 27)
        Me.Update.Name = "Update"
        Me.Update.Size = New System.Drawing.Size(300, 212)
        Me.Update.TabIndex = 42
        Me.Update.TabStop = False
        Me.Update.Text = "UPDATE"
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(181, 14)
        Me.TextBox8.Multiline = True
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(102, 156)
        Me.TextBox8.TabIndex = 3
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 94)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 13)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "EndSN"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 58)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(44, 13)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "StartSN"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 17)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(26, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "WO"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(181, 172)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(102, 34)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Change AdditionAction"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Red
        Me.Button1.Location = New System.Drawing.Point(56, 170)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(110, 36)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Xóa Tem đã in"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(56, 91)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(110, 20)
        Me.TextBox7.TabIndex = 0
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(56, 52)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(110, 20)
        Me.TextBox6.TabIndex = 0
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(56, 14)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(110, 20)
        Me.TextBox5.TabIndex = 0
        '
        'TextBox9
        '
        Me.TextBox9.Location = New System.Drawing.Point(470, 213)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(51, 20)
        Me.TextBox9.TabIndex = 43
        '
        'TextBox10
        '
        Me.TextBox10.Location = New System.Drawing.Point(527, 213)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(56, 20)
        Me.TextBox10.TabIndex = 43
        '
        'TextBox11
        '
        Me.TextBox11.Location = New System.Drawing.Point(594, 213)
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(56, 20)
        Me.TextBox11.TabIndex = 43
        '
        'frmNEW_SERIAL_PRINTING
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 741)
        Me.Controls.Add(Me.TextBox11)
        Me.Controls.Add(Me.TextBox10)
        Me.Controls.Add(Me.TextBox9)
        Me.Controls.Add(Me.Update)
        Me.Controls.Add(Me.txtMicron_desc)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.BtnExport)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtQty_Printed)
        Me.Controls.Add(Me.txtCapacity)
        Me.Controls.Add(Me.txtCPN)
        Me.Controls.Add(Me.txtEngREV)
        Me.Controls.Add(Me.txtAgileREV)
        Me.Controls.Add(Me.txtProductName)
        Me.Controls.Add(Me.txtDC)
        Me.Controls.Add(Me.txtPN)
        Me.Controls.Add(Me.txtPrintdate)
        Me.Controls.Add(Me.txtEnd_SN)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtStart_SN)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtWO_qty)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtPrint_by)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtEnd_SN_Print)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtStart_SN_Print)
        Me.Controls.Add(Me.txtWOQty)
        Me.Controls.Add(Me.txtPrint_Qty)
        Me.Controls.Add(Me.txtWO)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbbCustomer)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "frmNEW_SERIAL_PRINTING"
        Me.Text = "frmNEW_SERIAL_PRINTING"
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Update.ResumeLayout(False)
        Me.Update.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnExport As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txtQty_Printed As TextBox
    Friend WithEvents txtPN As TextBox
    Friend WithEvents txtPrintdate As TextBox
    Friend WithEvents txtEnd_SN As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtStart_SN As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtWO_qty As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents EXITToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents txtPrint_by As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtEnd_SN_Print As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtStart_SN_Print As TextBox
    Friend WithEvents txtWOQty As Label
    Friend WithEvents txtPrint_Qty As TextBox
    Friend WithEvents txtWO As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cbbCustomer As ComboBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents txtDC As TextBox
    Friend WithEvents txtProductName As TextBox
    Friend WithEvents txtAgileREV As TextBox
    Friend WithEvents txtEngREV As TextBox
    Friend WithEvents txtCPN As TextBox
    Friend WithEvents txtCapacity As TextBox
    Friend WithEvents txtMicron_desc As TextBox
    Friend WithEvents Update As GroupBox
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TextBox9 As TextBox
    Friend WithEvents TextBox10 As TextBox
    Friend WithEvents TextBox11 As TextBox
End Class
