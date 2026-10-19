<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IMEDRIX_SN_CREATE
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
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.Fuchsia
        Me.Button7.Location = New System.Drawing.Point(394, 137)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(163, 31)
        Me.Button7.TabIndex = 91
        Me.Button7.Text = "LOAD SN LIST TO EXCEL"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(16, 175)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(728, 23)
        Me.Button3.TabIndex = 90
        Me.Button3.Text = "SERIAL REQUEST LIST"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(253, 110)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(130, 20)
        Me.TextBox4.TabIndex = 89
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(173, 110)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(54, 20)
        Me.TextBox3.TabIndex = 88
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(103, 110)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(64, 20)
        Me.TextBox2.TabIndex = 87
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(394, 111)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(163, 20)
        Me.txtDate.TabIndex = 86
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(564, 76)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 13)
        Me.Label8.TabIndex = 85
        Me.Label8.Text = "End_SN"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(563, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 13)
        Me.Label7.TabIndex = 84
        Me.Label7.Text = "Start_SN"
        '
        'tbnNew
        '
        Me.tbnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbnNew.Location = New System.Drawing.Point(103, 136)
        Me.tbnNew.Name = "tbnNew"
        Me.tbnNew.Size = New System.Drawing.Size(124, 32)
        Me.tbnNew.TabIndex = 83
        Me.tbnNew.Text = "NEW"
        Me.tbnNew.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(391, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 82
        Me.Label6.Text = "DATE CODE"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(250, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 81
        Me.Label5.Text = "WO QTY"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(250, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 80
        Me.Label4.Text = "Product REV"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(2, 8)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 79
        '
        'txtDatecode
        '
        Me.txtDatecode.Location = New System.Drawing.Point(476, 46)
        Me.txtDatecode.Name = "txtDatecode"
        Me.txtDatecode.Size = New System.Drawing.Size(81, 20)
        Me.txtDatecode.TabIndex = 78
        '
        'txtRev
        '
        Me.txtRev.Location = New System.Drawing.Point(322, 73)
        Me.txtRev.Name = "txtRev"
        Me.txtRev.Size = New System.Drawing.Size(61, 20)
        Me.txtRev.TabIndex = 77
        '
        'txtSN_end
        '
        Me.txtSN_end.Location = New System.Drawing.Point(620, 73)
        Me.txtSN_end.Name = "txtSN_end"
        Me.txtSN_end.Size = New System.Drawing.Size(124, 20)
        Me.txtSN_end.TabIndex = 76
        '
        'txtSN_Start
        '
        Me.txtSN_Start.Location = New System.Drawing.Point(620, 46)
        Me.txtSN_Start.Name = "txtSN_Start"
        Me.txtSN_Start.Size = New System.Drawing.Size(124, 20)
        Me.txtSN_Start.TabIndex = 75
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(322, 46)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(61, 20)
        Me.txtQty.TabIndex = 74
        '
        'txtPN
        '
        Me.txtPN.Location = New System.Drawing.Point(103, 73)
        Me.txtPN.Name = "txtPN"
        Me.txtPN.Size = New System.Drawing.Size(124, 20)
        Me.txtPN.TabIndex = 73
        '
        'txtWO
        '
        Me.txtWO.Location = New System.Drawing.Point(103, 46)
        Me.txtWO.Name = "txtWO"
        Me.txtWO.Size = New System.Drawing.Size(124, 20)
        Me.txtWO.TabIndex = 72
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 71
        Me.Label3.Text = "Part Number"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 70
        Me.Label2.Text = "WO"
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(14, 204)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(730, 548)
        Me.DataGridView2.TabIndex = 69
        '
        'IMEDRIX_SN_CREATE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(746, 761)
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
        Me.Name = "IMEDRIX_SN_CREATE"
        Me.Text = "IMEDRIX_SN_CREATE"
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbnNew As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtDatecode As System.Windows.Forms.TextBox
    Friend WithEvents txtRev As System.Windows.Forms.TextBox
    Friend WithEvents txtSN_end As System.Windows.Forms.TextBox
    Friend WithEvents txtSN_Start As System.Windows.Forms.TextBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents txtPN As System.Windows.Forms.TextBox
    Friend WithEvents txtWO As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
End Class
