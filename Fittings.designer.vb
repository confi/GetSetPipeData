<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class selectFitting
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.fittingName = New System.Windows.Forms.ComboBox()
        Me.fittingMaterial = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PCS = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.fittingSpec = New System.Windows.Forms.ComboBox()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.hasBolts = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'fittingName
        '
        Me.fittingName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.fittingName.Enabled = False
        Me.fittingName.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.fittingName.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.fittingName.FormattingEnabled = True
        Me.fittingName.ItemHeight = 19
        Me.fittingName.Location = New System.Drawing.Point(66, 22)
        Me.fittingName.Name = "fittingName"
        Me.fittingName.Size = New System.Drawing.Size(181, 27)
        Me.fittingName.TabIndex = 2
        Me.fittingName.Tag = ""
        '
        'fittingMaterial
        '
        Me.fittingMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.fittingMaterial.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.fittingMaterial.ForeColor = System.Drawing.SystemColors.WindowText
        Me.fittingMaterial.FormattingEnabled = True
        Me.fittingMaterial.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.fittingMaterial.Location = New System.Drawing.Point(346, 22)
        Me.fittingMaterial.Name = "fittingMaterial"
        Me.fittingMaterial.Size = New System.Drawing.Size(80, 27)
        Me.fittingMaterial.TabIndex = 1
        Me.fittingMaterial.Tag = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(2, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 20)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "管件名称"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.Location = New System.Drawing.Point(273, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 20)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "管件材质"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 20)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "管件数量(个)"
        '
        'PCS
        '
        Me.PCS.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.PCS.HideSelection = False
        Me.PCS.Location = New System.Drawing.Point(99, 111)
        Me.PCS.MaxLength = 2
        Me.PCS.Name = "PCS"
        Me.PCS.Size = New System.Drawing.Size(25, 25)
        Me.PCS.TabIndex = 4
        Me.PCS.Text = "1"
        Me.PCS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(2, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "管件规格"
        '
        'fittingSpec
        '
        Me.fittingSpec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.fittingSpec.Enabled = False
        Me.fittingSpec.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.fittingSpec.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.fittingSpec.FormattingEnabled = True
        Me.fittingSpec.Location = New System.Drawing.Point(66, 65)
        Me.fittingSpec.Name = "fittingSpec"
        Me.fittingSpec.Size = New System.Drawing.Size(360, 27)
        Me.fittingSpec.TabIndex = 3
        Me.fittingSpec.Tag = ""
        '
        'BtnOK
        '
        Me.BtnOK.Enabled = False
        Me.BtnOK.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(321, 108)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(80, 30)
        Me.BtnOK.TabIndex = 5
        Me.BtnOK.Text = "添加"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'hasBolts
        '
        Me.hasBolts.AutoSize = True
        Me.hasBolts.Checked = True
        Me.hasBolts.CheckState = System.Windows.Forms.CheckState.Checked
        Me.hasBolts.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.hasBolts.Location = New System.Drawing.Point(154, 112)
        Me.hasBolts.Name = "hasBolts"
        Me.hasBolts.Size = New System.Drawing.Size(126, 24)
        Me.hasBolts.TabIndex = 10
        Me.hasBolts.Text = "配套螺栓和垫片"
        Me.hasBolts.UseVisualStyleBackColor = True
        Me.hasBolts.Visible = False
        '
        'selectFitting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 159)
        Me.Controls.Add(Me.hasBolts)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.fittingSpec)
        Me.Controls.Add(Me.fittingName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.fittingMaterial)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.PCS)
        Me.Controls.Add(Me.Label2)
        Me.Location = New System.Drawing.Point(2000, 1500)
        Me.MaximizeBox = False
        Me.Name = "selectFitting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "选择管件"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents fittingName As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PCS As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents fittingSpec As System.Windows.Forms.ComboBox
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents fittingMaterial As System.Windows.Forms.ComboBox
    Friend WithEvents hasBolts As System.Windows.Forms.CheckBox

End Class
