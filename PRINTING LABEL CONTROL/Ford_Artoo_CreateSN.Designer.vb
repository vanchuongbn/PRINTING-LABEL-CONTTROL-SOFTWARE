<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Ford_Artoo_CreateSN
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
        Me.txtWO = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPN = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.txt_Qty = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_y_d_Number = New System.Windows.Forms.TextBox()
        Me.btnGetSNList = New System.Windows.Forms.Button()
        Me.tbnNew = New System.Windows.Forms.Button()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.Rad_Artoo = New System.Windows.Forms.RadioButton()
        Me.Rad_EiOne = New System.Windows.Forms.RadioButton()
        Me.Rad_Eitwo = New System.Windows.Forms.RadioButton()
        Me.Rad_Eithree = New System.Windows.Forms.RadioButton()
        Me.Rad_Kaytoo_M_AB = New System.Windows.Forms.RadioButton()
        Me.Rad_Kaytoo_M_SB = New System.Windows.Forms.RadioButton()
        Me.Rad_Kaytoo_M_ES = New System.Windows.Forms.RadioButton()
        Me.Rad_Kaytoo_N_SB = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbl_Supplier_Code = New System.Windows.Forms.Label()
        Me.lbl_customerAssembly_Code = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btn_export2 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(79, 11)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 5
        '
        'txtWO
        '
        Me.txtWO.Location = New System.Drawing.Point(79, 58)
        Me.txtWO.Name = "txtWO"
        Me.txtWO.Size = New System.Drawing.Size(200, 20)
        Me.txtWO.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "WO"
        '
        'txtPN
        '
        Me.txtPN.Location = New System.Drawing.Point(79, 81)
        Me.txtPN.Name = "txtPN"
        Me.txtPN.ReadOnly = True
        Me.txtPN.Size = New System.Drawing.Size(200, 20)
        Me.txtPN.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Part Number"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(13, 169)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 62
        Me.DataGridView1.Size = New System.Drawing.Size(731, 537)
        Me.DataGridView1.TabIndex = 10
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(749, 169)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersWidth = 62
        Me.DataGridView2.Size = New System.Drawing.Size(504, 537)
        Me.DataGridView2.TabIndex = 11
        '
        'txt_Qty
        '
        Me.txt_Qty.Location = New System.Drawing.Point(448, 58)
        Me.txt_Qty.Name = "txt_Qty"
        Me.txt_Qty.ReadOnly = True
        Me.txt_Qty.Size = New System.Drawing.Size(109, 20)
        Me.txt_Qty.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(326, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Q'ty"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(13, 143)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(731, 23)
        Me.Button3.TabIndex = 14
        Me.Button3.Text = "SERIAL REQUEST LIST"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(749, 143)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(504, 23)
        Me.Button4.TabIndex = 15
        Me.Button4.Text = "50 Last SN created"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(326, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(113, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Year and DateNumber"
        '
        'txt_y_d_Number
        '
        Me.txt_y_d_Number.Enabled = False
        Me.txt_y_d_Number.Location = New System.Drawing.Point(448, 81)
        Me.txt_y_d_Number.Name = "txt_y_d_Number"
        Me.txt_y_d_Number.ReadOnly = True
        Me.txt_y_d_Number.Size = New System.Drawing.Size(109, 20)
        Me.txt_y_d_Number.TabIndex = 17
        '
        'btnGetSNList
        '
        Me.btnGetSNList.BackColor = System.Drawing.Color.Fuchsia
        Me.btnGetSNList.Location = New System.Drawing.Point(235, 110)
        Me.btnGetSNList.Name = "btnGetSNList"
        Me.btnGetSNList.Size = New System.Drawing.Size(185, 31)
        Me.btnGetSNList.TabIndex = 19
        Me.btnGetSNList.Text = "GET SN LIST"
        Me.btnGetSNList.UseVisualStyleBackColor = False
        '
        'tbnNew
        '
        Me.tbnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbnNew.Location = New System.Drawing.Point(15, 109)
        Me.tbnNew.Name = "tbnNew"
        Me.tbnNew.Size = New System.Drawing.Size(95, 32)
        Me.tbnNew.TabIndex = 18
        Me.tbnNew.Text = "NEW"
        Me.tbnNew.UseVisualStyleBackColor = False
        '
        'btn_Refresh
        '
        Me.btn_Refresh.BackColor = System.Drawing.Color.MediumSpringGreen
        Me.btn_Refresh.Location = New System.Drawing.Point(127, 109)
        Me.btn_Refresh.Name = "btn_Refresh"
        Me.btn_Refresh.Size = New System.Drawing.Size(95, 32)
        Me.btn_Refresh.TabIndex = 20
        Me.btn_Refresh.Text = "Refresh"
        Me.btn_Refresh.UseVisualStyleBackColor = False
        '
        'Rad_Artoo
        '
        Me.Rad_Artoo.AutoSize = True
        Me.Rad_Artoo.Location = New System.Drawing.Point(448, 9)
        Me.Rad_Artoo.Margin = New System.Windows.Forms.Padding(2)
        Me.Rad_Artoo.Name = "Rad_Artoo"
        Me.Rad_Artoo.Size = New System.Drawing.Size(73, 17)
        Me.Rad_Artoo.TabIndex = 21
        Me.Rad_Artoo.TabStop = True
        Me.Rad_Artoo.Text = "Artoo (ES)"
        Me.Rad_Artoo.UseVisualStyleBackColor = True
        '
        'Rad_EiOne
        '
        Me.Rad_EiOne.AutoSize = True
        Me.Rad_EiOne.Location = New System.Drawing.Point(563, 9)
        Me.Rad_EiOne.Margin = New System.Windows.Forms.Padding(2)
        Me.Rad_EiOne.Name = "Rad_EiOne"
        Me.Rad_EiOne.Size = New System.Drawing.Size(54, 17)
        Me.Rad_EiOne.TabIndex = 22
        Me.Rad_EiOne.TabStop = True
        Me.Rad_EiOne.Text = "ElOne"
        Me.Rad_EiOne.UseVisualStyleBackColor = True
        '
        'Rad_Eitwo
        '
        Me.Rad_Eitwo.AutoSize = True
        Me.Rad_Eitwo.Location = New System.Drawing.Point(679, 8)
        Me.Rad_Eitwo.Margin = New System.Windows.Forms.Padding(2)
        Me.Rad_Eitwo.Name = "Rad_Eitwo"
        Me.Rad_Eitwo.Size = New System.Drawing.Size(55, 17)
        Me.Rad_Eitwo.TabIndex = 23
        Me.Rad_Eitwo.TabStop = True
        Me.Rad_Eitwo.Text = "ElTwo"
        Me.Rad_Eitwo.UseVisualStyleBackColor = True
        '
        'Rad_Eithree
        '
        Me.Rad_Eithree.AutoSize = True
        Me.Rad_Eithree.Location = New System.Drawing.Point(793, 9)
        Me.Rad_Eithree.Margin = New System.Windows.Forms.Padding(2)
        Me.Rad_Eithree.Name = "Rad_Eithree"
        Me.Rad_Eithree.Size = New System.Drawing.Size(62, 17)
        Me.Rad_Eithree.TabIndex = 24
        Me.Rad_Eithree.TabStop = True
        Me.Rad_Eithree.Text = "ElThree"
        Me.Rad_Eithree.UseVisualStyleBackColor = True
        '
        'Rad_Kaytoo_M_AB
        '
        Me.Rad_Kaytoo_M_AB.AutoSize = True
        Me.Rad_Kaytoo_M_AB.Location = New System.Drawing.Point(793, 34)
        Me.Rad_Kaytoo_M_AB.Margin = New System.Windows.Forms.Padding(2)
        Me.Rad_Kaytoo_M_AB.Name = "Rad_Kaytoo_M_AB"
        Me.Rad_Kaytoo_M_AB.Size = New System.Drawing.Size(153, 17)
        Me.Rad_Kaytoo_M_AB.TabIndex = 28
        Me.Rad_Kaytoo_M_AB.TabStop = True
        Me.Rad_Kaytoo_M_AB.Text = "Kaytoo Mdium - Single (AB)"
        Me.Rad_Kaytoo_M_AB.UseVisualStyleBackColor = True
        '
        'Rad_Kaytoo_M_SB
        '
        Me.Rad_Kaytoo_M_SB.AutoSize = True
        Me.Rad_Kaytoo_M_SB.Location = New System.Drawing.Point(679, 32)
        Me.Rad_Kaytoo_M_SB.Margin = New System.Windows.Forms.Padding(2)
        Me.Rad_Kaytoo_M_SB.Name = "Rad_Kaytoo_M_SB"
        Me.Rad_Kaytoo_M_SB.Size = New System.Drawing.Size(115, 17)
        Me.Rad_Kaytoo_M_SB.TabIndex = 27
        Me.Rad_Kaytoo_M_SB.TabStop = True
        Me.Rad_Kaytoo_M_SB.Text = "Kaytoo Mdium (SB)"
        Me.Rad_Kaytoo_M_SB.UseVisualStyleBackColor = True
        '
        'Rad_Kaytoo_M_ES
        '
        Me.Rad_Kaytoo_M_ES.AutoSize = True
        Me.Rad_Kaytoo_M_ES.Location = New System.Drawing.Point(563, 34)
        Me.Rad_Kaytoo_M_ES.Margin = New System.Windows.Forms.Padding(2)
        Me.Rad_Kaytoo_M_ES.Name = "Rad_Kaytoo_M_ES"
        Me.Rad_Kaytoo_M_ES.Size = New System.Drawing.Size(115, 17)
        Me.Rad_Kaytoo_M_ES.TabIndex = 26
        Me.Rad_Kaytoo_M_ES.TabStop = True
        Me.Rad_Kaytoo_M_ES.Text = "Kaytoo Mdium (ES)"
        Me.Rad_Kaytoo_M_ES.UseVisualStyleBackColor = True
        '
        'Rad_Kaytoo_N_SB
        '
        Me.Rad_Kaytoo_N_SB.AutoSize = True
        Me.Rad_Kaytoo_N_SB.Location = New System.Drawing.Point(448, 34)
        Me.Rad_Kaytoo_N_SB.Margin = New System.Windows.Forms.Padding(2)
        Me.Rad_Kaytoo_N_SB.Name = "Rad_Kaytoo_N_SB"
        Me.Rad_Kaytoo_N_SB.Size = New System.Drawing.Size(115, 17)
        Me.Rad_Kaytoo_N_SB.TabIndex = 25
        Me.Rad_Kaytoo_N_SB.TabStop = True
        Me.Rad_Kaytoo_N_SB.Text = "Kaytoo Heavy (SB)"
        Me.Rad_Kaytoo_N_SB.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(580, 60)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 13)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "Supplier Code:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(580, 83)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(141, 13)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Customer Assembly Number:"
        '
        'lbl_Supplier_Code
        '
        Me.lbl_Supplier_Code.AutoSize = True
        Me.lbl_Supplier_Code.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Supplier_Code.ForeColor = System.Drawing.Color.DeepPink
        Me.lbl_Supplier_Code.Location = New System.Drawing.Point(739, 61)
        Me.lbl_Supplier_Code.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_Supplier_Code.Name = "lbl_Supplier_Code"
        Me.lbl_Supplier_Code.Size = New System.Drawing.Size(45, 13)
        Me.lbl_Supplier_Code.TabIndex = 31
        Me.lbl_Supplier_Code.Text = "JBYCB"
        '
        'lbl_customerAssembly_Code
        '
        Me.lbl_customerAssembly_Code.AutoSize = True
        Me.lbl_customerAssembly_Code.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_customerAssembly_Code.ForeColor = System.Drawing.Color.Magenta
        Me.lbl_customerAssembly_Code.Location = New System.Drawing.Point(739, 85)
        Me.lbl_customerAssembly_Code.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_customerAssembly_Code.Name = "lbl_customerAssembly_Code"
        Me.lbl_customerAssembly_Code.Size = New System.Drawing.Size(107, 13)
        Me.lbl_customerAssembly_Code.TabIndex = 32
        Me.lbl_customerAssembly_Code.Text = "V4GB-19K392-NA"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 13)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Date"
        '
        'btn_export2
        '
        Me.btn_export2.BackColor = System.Drawing.Color.DarkViolet
        Me.btn_export2.Location = New System.Drawing.Point(426, 110)
        Me.btn_export2.Name = "btn_export2"
        Me.btn_export2.Size = New System.Drawing.Size(185, 31)
        Me.btn_export2.TabIndex = 34
        Me.btn_export2.Text = "Export SNs for Print"
        Me.btn_export2.UseVisualStyleBackColor = False
        '
        'Ford_Artoo_CreateSN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1279, 690)
        Me.Controls.Add(Me.btn_export2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lbl_customerAssembly_Code)
        Me.Controls.Add(Me.lbl_Supplier_Code)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Rad_Kaytoo_M_AB)
        Me.Controls.Add(Me.Rad_Kaytoo_M_SB)
        Me.Controls.Add(Me.Rad_Kaytoo_M_ES)
        Me.Controls.Add(Me.Rad_Kaytoo_N_SB)
        Me.Controls.Add(Me.Rad_Eithree)
        Me.Controls.Add(Me.Rad_Eitwo)
        Me.Controls.Add(Me.Rad_EiOne)
        Me.Controls.Add(Me.Rad_Artoo)
        Me.Controls.Add(Me.btn_Refresh)
        Me.Controls.Add(Me.btnGetSNList)
        Me.Controls.Add(Me.tbnNew)
        Me.Controls.Add(Me.txt_y_d_Number)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.txt_Qty)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtPN)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtWO)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Ford_Artoo_CreateSN"
        Me.Text = "Ford_Artoo_CreateSN"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents txtWO As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPN As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents txt_Qty As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_y_d_Number As TextBox
    Friend WithEvents btnGetSNList As Button
    Friend WithEvents tbnNew As Button
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents Rad_Artoo As RadioButton
    Friend WithEvents Rad_EiOne As RadioButton
    Friend WithEvents Rad_Eitwo As RadioButton
    Friend WithEvents Rad_Eithree As RadioButton
    Friend WithEvents Rad_Kaytoo_M_AB As RadioButton
    Friend WithEvents Rad_Kaytoo_M_SB As RadioButton
    Friend WithEvents Rad_Kaytoo_M_ES As RadioButton
    Friend WithEvents Rad_Kaytoo_N_SB As RadioButton
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lbl_Supplier_Code As Label
    Friend WithEvents lbl_customerAssembly_Code As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents btn_export2 As Button
End Class
