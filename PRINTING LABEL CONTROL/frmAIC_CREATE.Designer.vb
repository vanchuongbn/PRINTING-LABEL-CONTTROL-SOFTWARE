<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAIC_CREATE
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.txtEnd_i = New System.Windows.Forms.TextBox()
        Me.txtStart_i = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tbnNew = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDatecode = New System.Windows.Forms.TextBox()
        Me.txtPO = New System.Windows.Forms.TextBox()
        Me.txtSN_end = New System.Windows.Forms.TextBox()
        Me.txtSN_Start = New System.Windows.Forms.TextBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.txtPN = New System.Windows.Forms.TextBox()
        Me.txtWO = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.dgwSN = New System.Windows.Forms.DataGridView()
        Me.txtPO_Item = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.dgwMAC_table = New System.Windows.Forms.DataGridView()
        Me.dgwMAC_List = New System.Windows.Forms.DataGridView()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtEnd_MAC = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtStart_MAC = New System.Windows.Forms.TextBox()
        Me.btnUploadMac = New System.Windows.Forms.Button()
        Me.txtMAC_Start_Dec = New System.Windows.Forms.TextBox()
        Me.txtMAC_End_Dec = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.btnSearch_MAC = New System.Windows.Forms.Button()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.dgwMAC_PN = New System.Windows.Forms.DataGridView()
        Me.Button4 = New System.Windows.Forms.Button()
        CType(Me.dgwSN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgwMAC_table, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgwMAC_List, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgwMAC_PN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.Fuchsia
        Me.Button7.Location = New System.Drawing.Point(1011, 203)
        Me.Button7.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(244, 55)
        Me.Button7.TabIndex = 65
        Me.Button7.Text = "LOAD SN LIST TO EXCEL"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(387, 145)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(193, 26)
        Me.TextBox4.TabIndex = 64
        '
        'txtEnd_i
        '
        Me.txtEnd_i.Location = New System.Drawing.Point(267, 145)
        Me.txtEnd_i.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtEnd_i.Name = "txtEnd_i"
        Me.txtEnd_i.Size = New System.Drawing.Size(79, 26)
        Me.txtEnd_i.TabIndex = 63
        '
        'txtStart_i
        '
        Me.txtStart_i.Location = New System.Drawing.Point(162, 145)
        Me.txtStart_i.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtStart_i.Name = "txtStart_i"
        Me.txtStart_i.Size = New System.Drawing.Size(94, 26)
        Me.txtStart_i.TabIndex = 62
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(598, 145)
        Me.txtDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(242, 26)
        Me.txtDate.TabIndex = 61
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(854, 102)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 20)
        Me.Label8.TabIndex = 60
        Me.Label8.Text = "End_SN"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(852, 60)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 20)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "Start_SN"
        '
        'tbnNew
        '
        Me.tbnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbnNew.Location = New System.Drawing.Point(267, 206)
        Me.tbnNew.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tbnNew.Name = "tbnNew"
        Me.tbnNew.Size = New System.Drawing.Size(135, 55)
        Me.tbnNew.TabIndex = 58
        Me.tbnNew.Text = "NEW"
        Me.tbnNew.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(642, 62)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 20)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "DATE CODE"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(376, 62)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 20)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "WO QTY"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(382, 103)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 20)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "AIC_PO"
        '
        'txtDatecode
        '
        Me.txtDatecode.Location = New System.Drawing.Point(754, 55)
        Me.txtDatecode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDatecode.Name = "txtDatecode"
        Me.txtDatecode.Size = New System.Drawing.Size(86, 26)
        Me.txtDatecode.TabIndex = 54
        '
        'txtPO
        '
        Me.txtPO.Location = New System.Drawing.Point(459, 97)
        Me.txtPO.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPO.Name = "txtPO"
        Me.txtPO.Size = New System.Drawing.Size(200, 26)
        Me.txtPO.TabIndex = 53
        '
        'txtSN_end
        '
        Me.txtSN_end.Location = New System.Drawing.Point(938, 97)
        Me.txtSN_end.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSN_end.Name = "txtSN_end"
        Me.txtSN_end.Size = New System.Drawing.Size(184, 26)
        Me.txtSN_end.TabIndex = 52
        '
        'txtSN_Start
        '
        Me.txtSN_Start.Location = New System.Drawing.Point(938, 55)
        Me.txtSN_Start.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSN_Start.Name = "txtSN_Start"
        Me.txtSN_Start.Size = New System.Drawing.Size(184, 26)
        Me.txtSN_Start.TabIndex = 51
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(459, 55)
        Me.txtQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(102, 26)
        Me.txtQty.TabIndex = 50
        '
        'txtPN
        '
        Me.txtPN.Location = New System.Drawing.Point(162, 97)
        Me.txtPN.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPN.Name = "txtPN"
        Me.txtPN.Size = New System.Drawing.Size(184, 26)
        Me.txtPN.TabIndex = 49
        '
        'txtWO
        '
        Me.txtWO.Location = New System.Drawing.Point(162, 55)
        Me.txtWO.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtWO.Name = "txtWO"
        Me.txtWO.Size = New System.Drawing.Size(184, 26)
        Me.txtWO.TabIndex = 48
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 103)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 20)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Part Number"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 62)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 20)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "WO"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(34, 271)
        Me.Button3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(1224, 35)
        Me.Button3.TabIndex = 67
        Me.Button3.Text = "SERIAL REQUEST LIST"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'dgwSN
        '
        Me.dgwSN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgwSN.Location = New System.Drawing.Point(32, 315)
        Me.dgwSN.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgwSN.Name = "dgwSN"
        Me.dgwSN.RowHeadersWidth = 62
        Me.dgwSN.Size = New System.Drawing.Size(1227, 974)
        Me.dgwSN.TabIndex = 66
        '
        'txtPO_Item
        '
        Me.txtPO_Item.Location = New System.Drawing.Point(754, 94)
        Me.txtPO_Item.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPO_Item.Name = "txtPO_Item"
        Me.txtPO_Item.Size = New System.Drawing.Size(86, 26)
        Me.txtPO_Item.TabIndex = 53
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(670, 102)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 20)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "PO_Item"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(18, 15)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(298, 26)
        Me.DateTimePicker1.TabIndex = 68
        '
        'dgwMAC_table
        '
        Me.dgwMAC_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgwMAC_table.Location = New System.Drawing.Point(1284, 315)
        Me.dgwMAC_table.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgwMAC_table.Name = "dgwMAC_table"
        Me.dgwMAC_table.RowHeadersWidth = 62
        Me.dgwMAC_table.Size = New System.Drawing.Size(762, 194)
        Me.dgwMAC_table.TabIndex = 69
        '
        'dgwMAC_List
        '
        Me.dgwMAC_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgwMAC_List.Location = New System.Drawing.Point(1284, 563)
        Me.dgwMAC_List.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgwMAC_List.Name = "dgwMAC_List"
        Me.dgwMAC_List.RowHeadersWidth = 62
        Me.dgwMAC_List.Size = New System.Drawing.Size(440, 726)
        Me.dgwMAC_List.TabIndex = 70
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(1176, 120)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 20)
        Me.Label9.TabIndex = 72
        Me.Label9.Text = "End_MAC"
        '
        'txtEnd_MAC
        '
        Me.txtEnd_MAC.Location = New System.Drawing.Point(1284, 109)
        Me.txtEnd_MAC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtEnd_MAC.Name = "txtEnd_MAC"
        Me.txtEnd_MAC.Size = New System.Drawing.Size(319, 26)
        Me.txtEnd_MAC.TabIndex = 73
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(1172, 68)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 20)
        Me.Label11.TabIndex = 75
        Me.Label11.Text = "Start_MAC"
        '
        'txtStart_MAC
        '
        Me.txtStart_MAC.Location = New System.Drawing.Point(1284, 63)
        Me.txtStart_MAC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtStart_MAC.Name = "txtStart_MAC"
        Me.txtStart_MAC.Size = New System.Drawing.Size(319, 26)
        Me.txtStart_MAC.TabIndex = 74
        '
        'btnUploadMac
        '
        Me.btnUploadMac.BackColor = System.Drawing.Color.Lime
        Me.btnUploadMac.Location = New System.Drawing.Point(1284, 202)
        Me.btnUploadMac.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnUploadMac.Name = "btnUploadMac"
        Me.btnUploadMac.Size = New System.Drawing.Size(204, 55)
        Me.btnUploadMac.TabIndex = 71
        Me.btnUploadMac.Text = "UPLOAD MAC#"
        Me.btnUploadMac.UseVisualStyleBackColor = False
        '
        'txtMAC_Start_Dec
        '
        Me.txtMAC_Start_Dec.Enabled = False
        Me.txtMAC_Start_Dec.Location = New System.Drawing.Point(1640, 63)
        Me.txtMAC_Start_Dec.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMAC_Start_Dec.Name = "txtMAC_Start_Dec"
        Me.txtMAC_Start_Dec.Size = New System.Drawing.Size(148, 26)
        Me.txtMAC_Start_Dec.TabIndex = 76
        '
        'txtMAC_End_Dec
        '
        Me.txtMAC_End_Dec.Enabled = False
        Me.txtMAC_End_Dec.Location = New System.Drawing.Point(1640, 109)
        Me.txtMAC_End_Dec.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMAC_End_Dec.Name = "txtMAC_End_Dec"
        Me.txtMAC_End_Dec.Size = New System.Drawing.Size(148, 26)
        Me.txtMAC_End_Dec.TabIndex = 76
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(1284, 271)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(762, 35)
        Me.Button1.TabIndex = 77
        Me.Button1.Text = "AIC MAC TABLE"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(1284, 518)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(440, 35)
        Me.Button2.TabIndex = 77
        Me.Button2.Text = "AIC MAC LIST"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'TextBox5
        '
        Me.TextBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox5.ForeColor = System.Drawing.Color.Blue
        Me.TextBox5.Location = New System.Drawing.Point(1852, 126)
        Me.TextBox5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox5.Multiline = True
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(192, 56)
        Me.TextBox5.TabIndex = 78
        Me.TextBox5.Text = "0"
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TextBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.TextBox6.Location = New System.Drawing.Point(1852, 57)
        Me.TextBox6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox6.Multiline = True
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(192, 58)
        Me.TextBox6.TabIndex = 79
        Me.TextBox6.Text = "AVAILABLE MAC QUANTITY"
        Me.TextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSearch_MAC
        '
        Me.btnSearch_MAC.Location = New System.Drawing.Point(1575, 200)
        Me.btnSearch_MAC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSearch_MAC.Name = "btnSearch_MAC"
        Me.btnSearch_MAC.Size = New System.Drawing.Size(214, 55)
        Me.btnSearch_MAC.TabIndex = 80
        Me.btnSearch_MAC.Text = "Search_MAC_byWO"
        Me.btnSearch_MAC.UseVisualStyleBackColor = True
        '
        'btnCreate
        '
        Me.btnCreate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnCreate.Location = New System.Drawing.Point(34, 206)
        Me.btnCreate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(135, 55)
        Me.btnCreate.TabIndex = 81
        Me.btnCreate.Text = "CreateSN"
        Me.btnCreate.UseVisualStyleBackColor = False
        '
        'dgwMAC_PN
        '
        Me.dgwMAC_PN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgwMAC_PN.Location = New System.Drawing.Point(1732, 563)
        Me.dgwMAC_PN.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgwMAC_PN.Name = "dgwMAC_PN"
        Me.dgwMAC_PN.RowHeadersWidth = 62
        Me.dgwMAC_PN.Size = New System.Drawing.Size(314, 726)
        Me.dgwMAC_PN.TabIndex = 84
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Button4.Location = New System.Drawing.Point(1732, 518)
        Me.Button4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(314, 35)
        Me.Button4.TabIndex = 85
        Me.Button4.Text = "MODEL NEED MAC"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'frmAIC_CREATE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2088, 1326)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.dgwMAC_PN)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.btnSearch_MAC)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtMAC_End_Dec)
        Me.Controls.Add(Me.txtMAC_Start_Dec)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtEnd_MAC)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtStart_MAC)
        Me.Controls.Add(Me.btnUploadMac)
        Me.Controls.Add(Me.dgwMAC_List)
        Me.Controls.Add(Me.dgwMAC_table)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.dgwSN)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.txtEnd_i)
        Me.Controls.Add(Me.txtStart_i)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.tbnNew)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtDatecode)
        Me.Controls.Add(Me.txtPO_Item)
        Me.Controls.Add(Me.txtPO)
        Me.Controls.Add(Me.txtSN_end)
        Me.Controls.Add(Me.txtSN_Start)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.txtPN)
        Me.Controls.Add(Me.txtWO)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmAIC_CREATE"
        Me.Text = "frmAIC_CREATE"
        CType(Me.dgwSN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgwMAC_table, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgwMAC_List, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgwMAC_PN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents txtEnd_i As System.Windows.Forms.TextBox
    Friend WithEvents txtStart_i As System.Windows.Forms.TextBox
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbnNew As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDatecode As System.Windows.Forms.TextBox
    Friend WithEvents txtPO As System.Windows.Forms.TextBox
    Friend WithEvents txtSN_end As System.Windows.Forms.TextBox
    Friend WithEvents txtSN_Start As System.Windows.Forms.TextBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents txtPN As System.Windows.Forms.TextBox
    Friend WithEvents txtWO As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents dgwSN As System.Windows.Forms.DataGridView
    Friend WithEvents txtPO_Item As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgwMAC_table As System.Windows.Forms.DataGridView
    Friend WithEvents dgwMAC_List As System.Windows.Forms.DataGridView
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtEnd_MAC As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtStart_MAC As System.Windows.Forms.TextBox
    Friend WithEvents btnUploadMac As System.Windows.Forms.Button
    Friend WithEvents txtMAC_Start_Dec As System.Windows.Forms.TextBox
    Friend WithEvents txtMAC_End_Dec As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch_MAC As System.Windows.Forms.Button
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents dgwMAC_PN As System.Windows.Forms.DataGridView
    Friend WithEvents Button4 As System.Windows.Forms.Button
End Class
