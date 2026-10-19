<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PRINTING_LABEL_CONTROL
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(36, 36)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(97, 48)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "IN MỚI SỐ SERIAL"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(237, 36)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(97, 48)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "IN LẠI SỐ SERIAL"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(36, 118)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(97, 48)
        Me.Button3.TabIndex = 0
        Me.Button3.Text = "IN MỚI SỐ MAC"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(237, 118)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(97, 48)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "IN LẠI SỐ MAC"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'PRINTING_LABEL_CONTROL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(399, 229)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "PRINTING_LABEL_CONTROL"
        Me.Text = "PRINTING_LABEL_CONTROL VER 1.0.0.0"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
End Class
