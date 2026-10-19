<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rototec_Create
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.txtDatecode = New System.Windows.Forms.TextBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.txtPN = New System.Windows.Forms.TextBox()
        Me.txtWO = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.btn_ExportExcell = New System.Windows.Forms.Button()
        Me.dgwSN = New System.Windows.Forms.DataGridView()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSN_end = New System.Windows.Forms.TextBox()
        Me.txtSN_Start = New System.Windows.Forms.TextBox()
        Me.txtEnd_i = New System.Windows.Forms.TextBox()
        Me.txtStart_i = New System.Windows.Forms.TextBox()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbb_RotoModel = New System.Windows.Forms.ComboBox()
        Me.lbl_fixedDigital = New System.Windows.Forms.Label()
        CType(Me.dgwSN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(169, 14)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(292, 26)
        Me.DateTimePicker1.TabIndex = 111
        '
        'txtDatecode
        '
        Me.txtDatecode.Enabled = False
        Me.txtDatecode.Location = New System.Drawing.Point(621, 110)
        Me.txtDatecode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDatecode.Name = "txtDatecode"
        Me.txtDatecode.Size = New System.Drawing.Size(142, 26)
        Me.txtDatecode.TabIndex = 116
        '
        'txtQty
        '
        Me.txtQty.Enabled = False
        Me.txtQty.Location = New System.Drawing.Point(621, 72)
        Me.txtQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(142, 26)
        Me.txtQty.TabIndex = 115
        '
        'txtPN
        '
        Me.txtPN.Location = New System.Drawing.Point(169, 107)
        Me.txtPN.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPN.Name = "txtPN"
        Me.txtPN.Size = New System.Drawing.Size(292, 26)
        Me.txtPN.TabIndex = 114
        '
        'txtWO
        '
        Me.txtWO.Location = New System.Drawing.Point(169, 65)
        Me.txtWO.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtWO.Name = "txtWO"
        Me.txtWO.Size = New System.Drawing.Size(292, 26)
        Me.txtWO.TabIndex = 113
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 72)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 20)
        Me.Label2.TabIndex = 112
        Me.Label2.Text = "WO"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(494, 116)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 20)
        Me.Label6.TabIndex = 118
        Me.Label6.Text = "DATE CODE"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(494, 75)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 20)
        Me.Label5.TabIndex = 117
        Me.Label5.Text = "WO QTY"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(34, 110)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 20)
        Me.Label3.TabIndex = 119
        Me.Label3.Text = "Part Number"
        '
        'btnCreate
        '
        Me.btnCreate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnCreate.Location = New System.Drawing.Point(38, 181)
        Me.btnCreate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(135, 55)
        Me.btnCreate.TabIndex = 127
        Me.btnCreate.Text = "CreateSN"
        Me.btnCreate.UseVisualStyleBackColor = False
        '
        'btn_ExportExcell
        '
        Me.btn_ExportExcell.BackColor = System.Drawing.Color.Fuchsia
        Me.btn_ExportExcell.Location = New System.Drawing.Point(217, 181)
        Me.btn_ExportExcell.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn_ExportExcell.Name = "btn_ExportExcell"
        Me.btn_ExportExcell.Size = New System.Drawing.Size(244, 55)
        Me.btn_ExportExcell.TabIndex = 126
        Me.btn_ExportExcell.Text = "LOAD SN LIST TO EXCEL"
        Me.btn_ExportExcell.UseVisualStyleBackColor = False
        '
        'dgwSN
        '
        Me.dgwSN.AllowUserToAddRows = False
        Me.dgwSN.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgwSN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgwSN.Location = New System.Drawing.Point(38, 255)
        Me.dgwSN.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgwSN.Name = "dgwSN"
        Me.dgwSN.RowHeadersWidth = 62
        Me.dgwSN.Size = New System.Drawing.Size(1746, 722)
        Me.dgwSN.TabIndex = 128
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(798, 113)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 20)
        Me.Label8.TabIndex = 132
        Me.Label8.Text = "End_SN"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(796, 71)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 20)
        Me.Label7.TabIndex = 131
        Me.Label7.Text = "Start_SN"
        '
        'txtSN_end
        '
        Me.txtSN_end.Location = New System.Drawing.Point(882, 108)
        Me.txtSN_end.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSN_end.Name = "txtSN_end"
        Me.txtSN_end.Size = New System.Drawing.Size(184, 26)
        Me.txtSN_end.TabIndex = 130
        '
        'txtSN_Start
        '
        Me.txtSN_Start.Location = New System.Drawing.Point(882, 66)
        Me.txtSN_Start.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSN_Start.Name = "txtSN_Start"
        Me.txtSN_Start.Size = New System.Drawing.Size(184, 26)
        Me.txtSN_Start.TabIndex = 129
        '
        'txtEnd_i
        '
        Me.txtEnd_i.Location = New System.Drawing.Point(1224, 110)
        Me.txtEnd_i.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtEnd_i.Name = "txtEnd_i"
        Me.txtEnd_i.Size = New System.Drawing.Size(79, 26)
        Me.txtEnd_i.TabIndex = 134
        Me.txtEnd_i.Visible = False
        '
        'txtStart_i
        '
        Me.txtStart_i.Location = New System.Drawing.Point(1119, 110)
        Me.txtStart_i.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtStart_i.Name = "txtStart_i"
        Me.txtStart_i.Size = New System.Drawing.Size(94, 26)
        Me.txtStart_i.TabIndex = 133
        Me.txtStart_i.Visible = False
        '
        'btn_Refresh
        '
        Me.btn_Refresh.BackColor = System.Drawing.Color.PaleGreen
        Me.btn_Refresh.Location = New System.Drawing.Point(507, 181)
        Me.btn_Refresh.Name = "btn_Refresh"
        Me.btn_Refresh.Size = New System.Drawing.Size(182, 55)
        Me.btn_Refresh.TabIndex = 135
        Me.btn_Refresh.Text = "ClearForm"
        Me.btn_Refresh.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(494, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 20)
        Me.Label1.TabIndex = 136
        Me.Label1.Text = "RotoModel"
        '
        'cbb_RotoModel
        '
        Me.cbb_RotoModel.FormattingEnabled = True
        Me.cbb_RotoModel.Location = New System.Drawing.Point(621, 14)
        Me.cbb_RotoModel.Name = "cbb_RotoModel"
        Me.cbb_RotoModel.Size = New System.Drawing.Size(445, 28)
        Me.cbb_RotoModel.TabIndex = 137
        '
        'lbl_fixedDigital
        '
        Me.lbl_fixedDigital.AutoSize = True
        Me.lbl_fixedDigital.Location = New System.Drawing.Point(1119, 13)
        Me.lbl_fixedDigital.Name = "lbl_fixedDigital"
        Me.lbl_fixedDigital.Size = New System.Drawing.Size(0, 20)
        Me.lbl_fixedDigital.TabIndex = 138
        '
        'Rototec_Create
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1850, 1009)
        Me.Controls.Add(Me.lbl_fixedDigital)
        Me.Controls.Add(Me.cbb_RotoModel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Refresh)
        Me.Controls.Add(Me.txtEnd_i)
        Me.Controls.Add(Me.txtStart_i)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtSN_end)
        Me.Controls.Add(Me.txtSN_Start)
        Me.Controls.Add(Me.dgwSN)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.btn_ExportExcell)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtDatecode)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.txtPN)
        Me.Controls.Add(Me.txtWO)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Name = "Rototec_Create"
        Me.Text = "Rototec_Create"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgwSN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents txtDatecode As TextBox
    Friend WithEvents txtQty As TextBox
    Friend WithEvents txtPN As TextBox
    Friend WithEvents txtWO As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnCreate As Button
    Friend WithEvents btn_ExportExcell As Button
    Friend WithEvents dgwSN As DataGridView
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtSN_end As TextBox
    Friend WithEvents txtSN_Start As TextBox
    Friend WithEvents txtEnd_i As TextBox
    Friend WithEvents txtStart_i As TextBox
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cbb_RotoModel As ComboBox
    Friend WithEvents lbl_fixedDigital As Label
End Class
