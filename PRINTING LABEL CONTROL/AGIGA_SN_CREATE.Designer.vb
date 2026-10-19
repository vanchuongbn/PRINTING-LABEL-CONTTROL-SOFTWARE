<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AGIGA_SN_CREATE
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
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tbnNew = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.txtDatecode = New System.Windows.Forms.TextBox()
        Me.txtRev = New System.Windows.Forms.TextBox()
        Me.txtSN_end = New System.Windows.Forms.TextBox()
        Me.txtSN_Start = New System.Windows.Forms.TextBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.txtPN = New System.Windows.Forms.TextBox()
        Me.txtWO = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.txtSPD_BOM = New System.Windows.Forms.TextBox()
        Me.txtSPI_FW = New System.Windows.Forms.TextBox()
        Me.txtHW_BOM = New System.Windows.Forms.TextBox()
        Me.txtSPI_BOM = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.Fuchsia
        Me.Button7.Location = New System.Drawing.Point(613, 195)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(131, 31)
        Me.Button7.TabIndex = 68
        Me.Button7.Text = "LOAD to EXCEL"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(16, 232)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(728, 23)
        Me.Button3.TabIndex = 67
        Me.Button3.Text = "SERIAL REQUEST LIST"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(253, 110)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(130, 20)
        Me.TextBox4.TabIndex = 66
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(173, 110)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(54, 20)
        Me.TextBox3.TabIndex = 65
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(103, 110)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(64, 20)
        Me.TextBox2.TabIndex = 64
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(394, 111)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(163, 20)
        Me.txtDate.TabIndex = 63
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(564, 76)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 13)
        Me.Label8.TabIndex = 62
        Me.Label8.Text = "End_SN"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(563, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 13)
        Me.Label7.TabIndex = 61
        Me.Label7.Text = "Start_SN"
        '
        'tbnNew
        '
        Me.tbnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbnNew.Location = New System.Drawing.Point(209, 8)
        Me.tbnNew.Name = "tbnNew"
        Me.tbnNew.Size = New System.Drawing.Size(57, 20)
        Me.tbnNew.TabIndex = 60
        Me.tbnNew.Text = "NEW"
        Me.tbnNew.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(391, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 59
        Me.Label6.Text = "DATE CODE"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(250, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 58
        Me.Label5.Text = "WO QTY"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(250, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Product REV"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(2, 8)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 56
        '
        'txtDatecode
        '
        Me.txtDatecode.Location = New System.Drawing.Point(476, 46)
        Me.txtDatecode.Name = "txtDatecode"
        Me.txtDatecode.Size = New System.Drawing.Size(81, 20)
        Me.txtDatecode.TabIndex = 55
        '
        'txtRev
        '
        Me.txtRev.Location = New System.Drawing.Point(322, 73)
        Me.txtRev.Name = "txtRev"
        Me.txtRev.Size = New System.Drawing.Size(61, 20)
        Me.txtRev.TabIndex = 54
        '
        'txtSN_end
        '
        Me.txtSN_end.Location = New System.Drawing.Point(620, 73)
        Me.txtSN_end.Name = "txtSN_end"
        Me.txtSN_end.Size = New System.Drawing.Size(124, 20)
        Me.txtSN_end.TabIndex = 53
        '
        'txtSN_Start
        '
        Me.txtSN_Start.Location = New System.Drawing.Point(620, 46)
        Me.txtSN_Start.Name = "txtSN_Start"
        Me.txtSN_Start.Size = New System.Drawing.Size(124, 20)
        Me.txtSN_Start.TabIndex = 52
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(322, 46)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(61, 20)
        Me.txtQty.TabIndex = 51
        '
        'txtPN
        '
        Me.txtPN.Location = New System.Drawing.Point(103, 73)
        Me.txtPN.Name = "txtPN"
        Me.txtPN.Size = New System.Drawing.Size(124, 20)
        Me.txtPN.TabIndex = 50
        '
        'txtWO
        '
        Me.txtWO.Location = New System.Drawing.Point(103, 46)
        Me.txtWO.Name = "txtWO"
        Me.txtWO.Size = New System.Drawing.Size(124, 20)
        Me.txtWO.TabIndex = 49
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Part Number"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "WO"
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(14, 261)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(730, 491)
        Me.DataGridView2.TabIndex = 46
        '
        'txtSPD_BOM
        '
        Me.txtSPD_BOM.Location = New System.Drawing.Point(114, 19)
        Me.txtSPD_BOM.Name = "txtSPD_BOM"
        Me.txtSPD_BOM.Size = New System.Drawing.Size(64, 20)
        Me.txtSPD_BOM.TabIndex = 69
        '
        'txtSPI_FW
        '
        Me.txtSPI_FW.Location = New System.Drawing.Point(264, 19)
        Me.txtSPI_FW.Name = "txtSPI_FW"
        Me.txtSPI_FW.Size = New System.Drawing.Size(66, 20)
        Me.txtSPI_FW.TabIndex = 69
        '
        'txtHW_BOM
        '
        Me.txtHW_BOM.Location = New System.Drawing.Point(434, 19)
        Me.txtHW_BOM.Name = "txtHW_BOM"
        Me.txtHW_BOM.Size = New System.Drawing.Size(59, 20)
        Me.txtHW_BOM.TabIndex = 69
        '
        'txtSPI_BOM
        '
        Me.txtSPI_BOM.Location = New System.Drawing.Point(631, 19)
        Me.txtSPI_BOM.Name = "txtSPI_BOM"
        Me.txtSPI_BOM.Size = New System.Drawing.Size(69, 20)
        Me.txtSPI_BOM.TabIndex = 69
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "SPD_BOM_ver"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(190, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(68, 13)
        Me.Label9.TabIndex = 70
        Me.Label9.Text = "SPI_FW_ver"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(351, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 13)
        Me.Label10.TabIndex = 70
        Me.Label10.Text = "HW_BOM_ver"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(532, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 13)
        Me.Label11.TabIndex = 70
        Me.Label11.Text = "SPI_BOM_ver"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSPI_FW)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtSPD_BOM)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtHW_BOM)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtSPI_BOM)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 136)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(728, 51)
        Me.GroupBox1.TabIndex = 71
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Yellow
        Me.Button1.Location = New System.Drawing.Point(16, 195)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(114, 31)
        Me.Button1.TabIndex = 72
        Me.Button1.Text = "CREATE SN"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'AGIGA_SN_CREATE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(746, 761)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.tbnNew)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.txtDatecode)
        Me.Controls.Add(Me.txtRev)
        Me.Controls.Add(Me.txtSN_end)
        Me.Controls.Add(Me.txtSN_Start)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.txtPN)
        Me.Controls.Add(Me.txtWO)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridView2)
        Me.Name = "AGIGA_SN_CREATE"
        Me.Text = "AGIGA_SN_CREATE"
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button7 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents txtDate As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents tbnNew As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents txtDatecode As TextBox
    Friend WithEvents txtRev As TextBox
    Friend WithEvents txtSN_end As TextBox
    Friend WithEvents txtSN_Start As TextBox
    Friend WithEvents txtQty As TextBox
    Friend WithEvents txtPN As TextBox
    Friend WithEvents txtWO As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents txtSPD_BOM As TextBox
    Friend WithEvents txtSPI_FW As TextBox
    Friend WithEvents txtHW_BOM As TextBox
    Friend WithEvents txtSPI_BOM As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
End Class
