<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class flangeAtt
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.boltMaterial = New System.Windows.Forms.ComboBox()
        Me.boltsSpec = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.boltsPCS = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.gasketPCS = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.gasketSpec = New System.Windows.Forms.ComboBox()
        Me.addBotlsGasket = New System.Windows.Forms.Button()
        Me.washer = New System.Windows.Forms.ComboBox()
        Me.springWasher = New System.Windows.Forms.ComboBox()
        Me.nut = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 20)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "螺栓材质"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 66)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 20)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "螺栓规格"
        '
        'boltMaterial
        '
        Me.boltMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.boltMaterial.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.boltMaterial.ForeColor = System.Drawing.SystemColors.WindowText
        Me.boltMaterial.FormattingEnabled = True
        Me.boltMaterial.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.boltMaterial.Location = New System.Drawing.Point(85, 19)
        Me.boltMaterial.Name = "boltMaterial"
        Me.boltMaterial.Size = New System.Drawing.Size(80, 27)
        Me.boltMaterial.TabIndex = 1
        Me.boltMaterial.Tag = ""
        '
        'boltsSpec
        '
        Me.boltsSpec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.boltsSpec.Enabled = False
        Me.boltsSpec.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.boltsSpec.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.boltsSpec.FormattingEnabled = True
        Me.boltsSpec.Location = New System.Drawing.Point(85, 63)
        Me.boltsSpec.Name = "boltsSpec"
        Me.boltsSpec.Size = New System.Drawing.Size(340, 27)
        Me.boltsSpec.TabIndex = 2
        Me.boltsSpec.Tag = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(210, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 20)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "螺 栓 数 量 ( 个 )"
        '
        'boltsPCS
        '
        Me.boltsPCS.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.boltsPCS.HideSelection = False
        Me.boltsPCS.Location = New System.Drawing.Point(339, 17)
        Me.boltsPCS.MaxLength = 2
        Me.boltsPCS.Name = "boltsPCS"
        Me.boltsPCS.Size = New System.Drawing.Size(25, 25)
        Me.boltsPCS.TabIndex = 4
        Me.boltsPCS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(16, 107)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(140, 24)
        Me.CheckBox1.TabIndex = 9
        Me.CheckBox1.Text = "配套一平一弹一母"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'gasketPCS
        '
        Me.gasketPCS.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.gasketPCS.HideSelection = False
        Me.gasketPCS.Location = New System.Drawing.Point(141, 291)
        Me.gasketPCS.MaxLength = 2
        Me.gasketPCS.Name = "gasketPCS"
        Me.gasketPCS.Size = New System.Drawing.Size(25, 25)
        Me.gasketPCS.TabIndex = 4
        Me.gasketPCS.Text = "1"
        Me.gasketPCS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 293)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(117, 20)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "法兰垫片数量(片)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 247)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "垫片规格"
        '
        'gasketSpec
        '
        Me.gasketSpec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.gasketSpec.Enabled = False
        Me.gasketSpec.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.gasketSpec.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.gasketSpec.FormattingEnabled = True
        Me.gasketSpec.Location = New System.Drawing.Point(85, 244)
        Me.gasketSpec.Name = "gasketSpec"
        Me.gasketSpec.Size = New System.Drawing.Size(340, 27)
        Me.gasketSpec.TabIndex = 2
        Me.gasketSpec.Tag = ""
        '
        'addBotlsGasket
        '
        Me.addBotlsGasket.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.addBotlsGasket.Location = New System.Drawing.Point(347, 289)
        Me.addBotlsGasket.Name = "addBotlsGasket"
        Me.addBotlsGasket.Size = New System.Drawing.Size(80, 29)
        Me.addBotlsGasket.TabIndex = 10
        Me.addBotlsGasket.Text = "添加"
        Me.addBotlsGasket.UseVisualStyleBackColor = True
        '
        'washer
        '
        Me.washer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.washer.Enabled = False
        Me.washer.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.washer.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.washer.FormattingEnabled = True
        Me.washer.Location = New System.Drawing.Point(85, 137)
        Me.washer.Name = "washer"
        Me.washer.Size = New System.Drawing.Size(340, 27)
        Me.washer.TabIndex = 2
        Me.washer.Tag = ""
        '
        'springWasher
        '
        Me.springWasher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.springWasher.Enabled = False
        Me.springWasher.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.springWasher.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.springWasher.FormattingEnabled = True
        Me.springWasher.Location = New System.Drawing.Point(85, 170)
        Me.springWasher.Name = "springWasher"
        Me.springWasher.Size = New System.Drawing.Size(340, 27)
        Me.springWasher.TabIndex = 2
        Me.springWasher.Tag = ""
        '
        'nut
        '
        Me.nut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.nut.Enabled = False
        Me.nut.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.nut.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.nut.FormattingEnabled = True
        Me.nut.Location = New System.Drawing.Point(85, 203)
        Me.nut.Name = "nut"
        Me.nut.Size = New System.Drawing.Size(340, 27)
        Me.nut.TabIndex = 2
        Me.nut.Tag = ""
        '
        'flangeAtt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 360)
        Me.Controls.Add(Me.addBotlsGasket)
        Me.Controls.Add(Me.gasketSpec)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.nut)
        Me.Controls.Add(Me.springWasher)
        Me.Controls.Add(Me.washer)
        Me.Controls.Add(Me.boltsSpec)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.boltMaterial)
        Me.Controls.Add(Me.gasketPCS)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.boltsPCS)
        Me.Name = "flangeAtt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "选择法兰附件"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents boltMaterial As System.Windows.Forms.ComboBox
    Friend WithEvents boltsSpec As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents boltsPCS As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents gasketPCS As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents gasketSpec As System.Windows.Forms.ComboBox
    Friend WithEvents addBotlsGasket As System.Windows.Forms.Button
    Friend WithEvents washer As System.Windows.Forms.ComboBox
    Friend WithEvents springWasher As System.Windows.Forms.ComboBox
    Friend WithEvents nut As System.Windows.Forms.ComboBox
End Class
