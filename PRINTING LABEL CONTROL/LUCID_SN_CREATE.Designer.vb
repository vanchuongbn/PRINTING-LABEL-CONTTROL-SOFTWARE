<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LUCID_SN_CREATE
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LUCID_SN_CREATE))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnLink = New System.Windows.Forms.Button()
        Me.txtPCBAPN = New System.Windows.Forms.TextBox()
        Me.txtBBPN1 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtBBWO2 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtBBPN2 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtBBWO1 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtPCBA_WO = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.btnCreateSN = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtCreatedQty = New System.Windows.Forms.TextBox()
        Me.txtNeedQty = New System.Windows.Forms.TextBox()
        Me.txtWO = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPN = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_end_i = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtDatecode = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_start_i = New System.Windows.Forms.TextBox()
        Me.txtSN_end = New System.Windows.Forms.TextBox()
        Me.txtSN_Start = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Panel2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.NavajoWhite
        Me.Panel2.Controls.Add(Me.ComboBox1)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnSearch)
        Me.Panel2.Controls.Add(Me.CheckBox1)
        Me.Panel2.Controls.Add(Me.GroupBox4)
        Me.Panel2.Controls.Add(Me.btnNew)
        Me.Panel2.Controls.Add(Me.DataGridView3)
        Me.Panel2.Controls.Add(Me.DataGridView2)
        Me.Panel2.Controls.Add(Me.Button7)
        Me.Panel2.Controls.Add(Me.btnCreateSN)
        Me.Panel2.Controls.Add(Me.DataGridView1)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.DateTimePicker1)
        Me.Panel2.Location = New System.Drawing.Point(18, 18)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1846, 1143)
        Me.Panel2.TabIndex = 8
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"By Date", "By WO", "By WO and Date", "By WO and No used"})
        Me.ComboBox1.Location = New System.Drawing.Point(177, 480)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(152, 28)
        Me.ComboBox1.TabIndex = 80
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.Red
        Me.btnDelete.Location = New System.Drawing.Point(363, 481)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(214, 54)
        Me.btnDelete.TabIndex = 79
        Me.btnDelete.Text = "Delete_NO USED SN"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(28, 480)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(140, 57)
        Me.btnSearch.TabIndex = 76
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.CheckBox1.ForeColor = System.Drawing.Color.Red
        Me.CheckBox1.Location = New System.Drawing.Point(22, 763)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(423, 29)
        Me.CheckBox1.TabIndex = 75
        Me.CheckBox1.Text = "Click mouse to this check box to link SN"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox4.Controls.Add(Me.Button1)
        Me.GroupBox4.Controls.Add(Me.btnLoad)
        Me.GroupBox4.Controls.Add(Me.btnLink)
        Me.GroupBox4.Controls.Add(Me.txtPCBAPN)
        Me.GroupBox4.Controls.Add(Me.txtBBPN1)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.txtBBWO2)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.Label22)
        Me.GroupBox4.Controls.Add(Me.txtBBPN2)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.txtBBWO1)
        Me.GroupBox4.Controls.Add(Me.Label21)
        Me.GroupBox4.Controls.Add(Me.txtPCBA_WO)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Location = New System.Drawing.Point(22, 828)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox4.Size = New System.Drawing.Size(585, 308)
        Me.GroupBox4.TabIndex = 74
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "LINKSN"
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(370, 232)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(206, 45)
        Me.Button1.TabIndex = 76
        Me.Button1.Text = "Load Linked SN"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(14, 232)
        Me.btnLoad.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(126, 45)
        Me.btnLoad.TabIndex = 75
        Me.btnLoad.Text = "LOAD"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnLink
        '
        Me.btnLink.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnLink.Location = New System.Drawing.Point(184, 232)
        Me.btnLink.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnLink.Name = "btnLink"
        Me.btnLink.Size = New System.Drawing.Size(124, 45)
        Me.btnLink.TabIndex = 4
        Me.btnLink.Text = "LINK"
        Me.btnLink.UseVisualStyleBackColor = False
        '
        'txtPCBAPN
        '
        Me.txtPCBAPN.Location = New System.Drawing.Point(369, 38)
        Me.txtPCBAPN.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPCBAPN.Name = "txtPCBAPN"
        Me.txtPCBAPN.Size = New System.Drawing.Size(184, 26)
        Me.txtPCBAPN.TabIndex = 3
        '
        'txtBBPN1
        '
        Me.txtBBPN1.Location = New System.Drawing.Point(370, 98)
        Me.txtBBPN1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBBPN1.Name = "txtBBPN1"
        Me.txtBBPN1.Size = New System.Drawing.Size(184, 26)
        Me.txtBBPN1.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(256, 45)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 20)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "PCBA_PN"
        '
        'txtBBWO2
        '
        Me.txtBBWO2.Location = New System.Drawing.Point(136, 157)
        Me.txtBBWO2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBBWO2.Name = "txtBBWO2"
        Me.txtBBWO2.Size = New System.Drawing.Size(88, 26)
        Me.txtBBWO2.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(258, 105)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(108, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Boxbuild1_PN"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(9, 162)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(114, 20)
        Me.Label22.TabIndex = 2
        Me.Label22.Text = "WO_Boxbuild2"
        '
        'txtBBPN2
        '
        Me.txtBBPN2.Location = New System.Drawing.Point(369, 157)
        Me.txtBBPN2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBBPN2.Name = "txtBBPN2"
        Me.txtBBPN2.Size = New System.Drawing.Size(184, 26)
        Me.txtBBPN2.TabIndex = 3
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(256, 163)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(108, 20)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Boxbuild2_PN"
        '
        'txtBBWO1
        '
        Me.txtBBWO1.Location = New System.Drawing.Point(136, 100)
        Me.txtBBWO1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBBWO1.Name = "txtBBWO1"
        Me.txtBBWO1.Size = New System.Drawing.Size(88, 26)
        Me.txtBBWO1.TabIndex = 3
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(9, 105)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(114, 20)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "WO_Boxbuild1"
        '
        'txtPCBA_WO
        '
        Me.txtPCBA_WO.Location = New System.Drawing.Point(136, 38)
        Me.txtPCBA_WO.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPCBA_WO.Name = "txtPCBA_WO"
        Me.txtPCBA_WO.Size = New System.Drawing.Size(88, 26)
        Me.txtPCBA_WO.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 43)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "WO_PCBA"
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Location = New System.Drawing.Point(174, 413)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(158, 48)
        Me.btnNew.TabIndex = 72
        Me.btnNew.Text = "NEW"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'DataGridView3
        '
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Location = New System.Drawing.Point(1272, 598)
        Me.DataGridView3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.RowHeadersWidth = 62
        Me.DataGridView3.Size = New System.Drawing.Size(570, 542)
        Me.DataGridView3.TabIndex = 71
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(1272, 55)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersWidth = 62
        Me.DataGridView2.Size = New System.Drawing.Size(570, 534)
        Me.DataGridView2.TabIndex = 71
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.Fuchsia
        Me.Button7.Image = CType(resources.GetObject("Button7.Image"), System.Drawing.Image)
        Me.Button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button7.Location = New System.Drawing.Point(362, 415)
        Me.Button7.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(216, 48)
        Me.Button7.TabIndex = 70
        Me.Button7.Text = "LOAD SN TO EXCEL"
        Me.Button7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button7.UseVisualStyleBackColor = False
        '
        'btnCreateSN
        '
        Me.btnCreateSN.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCreateSN.Location = New System.Drawing.Point(28, 412)
        Me.btnCreateSN.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCreateSN.Name = "btnCreateSN"
        Me.btnCreateSN.Size = New System.Drawing.Size(140, 49)
        Me.btnCreateSN.TabIndex = 69
        Me.btnCreateSN.Text = "CREATE"
        Me.btnCreateSN.UseVisualStyleBackColor = False
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(616, 55)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 62
        Me.DataGridView1.Size = New System.Drawing.Size(646, 1085)
        Me.DataGridView1.TabIndex = 24
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.txtCreatedQty)
        Me.GroupBox1.Controls.Add(Me.txtNeedQty)
        Me.GroupBox1.Controls.Add(Me.txtWO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtPN)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtQty)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txt_end_i)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtDatecode)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txt_start_i)
        Me.GroupBox1.Controls.Add(Me.txtSN_end)
        Me.GroupBox1.Controls.Add(Me.txtSN_Start)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtDate)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 55)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(585, 347)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "MAIN WO"
        '
        'txtCreatedQty
        '
        Me.txtCreatedQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.txtCreatedQty.ForeColor = System.Drawing.Color.Red
        Me.txtCreatedQty.Location = New System.Drawing.Point(411, 192)
        Me.txtCreatedQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCreatedQty.Multiline = True
        Me.txtCreatedQty.Name = "txtCreatedQty"
        Me.txtCreatedQty.Size = New System.Drawing.Size(144, 52)
        Me.txtCreatedQty.TabIndex = 11
        '
        'txtNeedQty
        '
        Me.txtNeedQty.Location = New System.Drawing.Point(136, 194)
        Me.txtNeedQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNeedQty.Name = "txtNeedQty"
        Me.txtNeedQty.Size = New System.Drawing.Size(88, 26)
        Me.txtNeedQty.TabIndex = 11
        '
        'txtWO
        '
        Me.txtWO.Location = New System.Drawing.Point(68, 29)
        Me.txtWO.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtWO.Name = "txtWO"
        Me.txtWO.Size = New System.Drawing.Size(157, 26)
        Me.txtWO.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 38)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "WO"
        '
        'txtPN
        '
        Me.txtPN.Location = New System.Drawing.Point(370, 28)
        Me.txtPN.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPN.Name = "txtPN"
        Me.txtPN.Size = New System.Drawing.Size(184, 26)
        Me.txtPN.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(262, 34)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Part Number"
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(102, 82)
        Me.txtQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(122, 26)
        Me.txtQty.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 92)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 20)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "WO QTY"
        '
        'txt_end_i
        '
        Me.txt_end_i.Location = New System.Drawing.Point(120, 255)
        Me.txt_end_i.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txt_end_i.Name = "txt_end_i"
        Me.txt_end_i.Size = New System.Drawing.Size(102, 26)
        Me.txt_end_i.TabIndex = 10
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Red
        Me.Label20.Location = New System.Drawing.Point(262, 205)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(138, 20)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "QTY CREATED"
        '
        'txtDatecode
        '
        Me.txtDatecode.Location = New System.Drawing.Point(136, 140)
        Me.txtDatecode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDatecode.Name = "txtDatecode"
        Me.txtDatecode.Size = New System.Drawing.Size(88, 26)
        Me.txtDatecode.TabIndex = 3
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Blue
        Me.Label19.Location = New System.Drawing.Point(9, 198)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(128, 20)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "QTY NEEDED"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 145)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 20)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "DATE CODE"
        '
        'txt_start_i
        '
        Me.txt_start_i.Location = New System.Drawing.Point(6, 255)
        Me.txt_start_i.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txt_start_i.Name = "txt_start_i"
        Me.txt_start_i.Size = New System.Drawing.Size(103, 26)
        Me.txt_start_i.TabIndex = 10
        '
        'txtSN_end
        '
        Me.txtSN_end.Location = New System.Drawing.Point(370, 140)
        Me.txtSN_end.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSN_end.Name = "txtSN_end"
        Me.txtSN_end.Size = New System.Drawing.Size(184, 26)
        Me.txtSN_end.TabIndex = 3
        '
        'txtSN_Start
        '
        Me.txtSN_Start.Location = New System.Drawing.Point(370, 82)
        Me.txtSN_Start.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSN_Start.Name = "txtSN_Start"
        Me.txtSN_Start.Size = New System.Drawing.Size(184, 26)
        Me.txtSN_Start.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(280, 86)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 20)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Start_SN"
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(344, 255)
        Me.txtDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(211, 26)
        Me.txtDate.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(286, 145)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 20)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "End_SN"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(22, 5)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(298, 26)
        Me.DateTimePicker1.TabIndex = 4
        '
        'LUCID_SN_CREATE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1882, 1172)
        Me.Controls.Add(Me.Panel2)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "LUCID_SN_CREATE"
        Me.Text = "LUCID_SN_CREATE"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txt_end_i As System.Windows.Forms.TextBox
    Friend WithEvents txt_start_i As System.Windows.Forms.TextBox
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtDatecode As System.Windows.Forms.TextBox
    Friend WithEvents txtSN_end As System.Windows.Forms.TextBox
    Friend WithEvents txtSN_Start As System.Windows.Forms.TextBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents txtPN As System.Windows.Forms.TextBox
    Friend WithEvents txtWO As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txtBBPN2 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtPCBA_WO As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBBPN1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents btnCreateSN As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents txtCreatedQty As System.Windows.Forms.TextBox
    Friend WithEvents txtNeedQty As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPCBAPN As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtBBWO2 As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtBBWO1 As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnLink As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
End Class
