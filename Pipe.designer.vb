<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pipe
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

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pipe))
        Me.pipeSpec = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pipeLength = New System.Windows.Forms.TextBox()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pipeMaterial = New System.Windows.Forms.ComboBox()
        Me.AddFitting = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.PCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fittingName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SPEC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Material = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnRemove = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pipeSpec
        '
        Me.pipeSpec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.pipeSpec.Enabled = False
        Me.pipeSpec.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.pipeSpec.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.pipeSpec.FormattingEnabled = True
        Me.pipeSpec.Location = New System.Drawing.Point(135, 42)
        Me.pipeSpec.Name = "pipeSpec"
        Me.pipeSpec.Size = New System.Drawing.Size(330, 27)
        Me.pipeSpec.TabIndex = 2
        Me.pipeSpec.Tag = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(131, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 20)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "管道规格"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 20)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "管道长度(米)"
        '
        'pipeLength
        '
        Me.pipeLength.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.pipeLength.Location = New System.Drawing.Point(135, 87)
        Me.pipeLength.Name = "pipeLength"
        Me.pipeLength.Size = New System.Drawing.Size(100, 25)
        Me.pipeLength.TabIndex = 3
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(386, 309)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 23)
        Me.BtnOK.TabIndex = 9
        Me.BtnOK.Text = "完成"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 20)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "管道材质"
        '
        'pipeMaterial
        '
        Me.pipeMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.pipeMaterial.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.pipeMaterial.ForeColor = System.Drawing.SystemColors.WindowText
        Me.pipeMaterial.FormattingEnabled = True
        Me.pipeMaterial.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.pipeMaterial.Items.AddRange(New Object() {"SS304", "SS316L", "20#", "PVC", "CPVC", "PTFE", "PP"})
        Me.pipeMaterial.Location = New System.Drawing.Point(8, 42)
        Me.pipeMaterial.Name = "pipeMaterial"
        Me.pipeMaterial.Size = New System.Drawing.Size(80, 27)
        Me.pipeMaterial.TabIndex = 1
        Me.pipeMaterial.Tag = ""
        '
        'AddFitting
        '
        Me.AddFitting.Location = New System.Drawing.Point(8, 309)
        Me.AddFitting.Name = "AddFitting"
        Me.AddFitting.Size = New System.Drawing.Size(75, 23)
        Me.AddFitting.TabIndex = 11
        Me.AddFitting.Text = "增加管件"
        Me.AddFitting.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PCS, Me.fittingName, Me.SPEC, Me.Material, Me.CODE})
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView1.Location = New System.Drawing.Point(8, 129)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(460, 170)
        Me.DataGridView1.TabIndex = 10
        '
        'PCS
        '
        Me.PCS.HeaderText = "数量"
        Me.PCS.Name = "PCS"
        Me.PCS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PCS.Width = 30
        '
        'fittingName
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.fittingName.DefaultCellStyle = DataGridViewCellStyle1
        Me.fittingName.HeaderText = "名称"
        Me.fittingName.Name = "fittingName"
        Me.fittingName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'SPEC
        '
        Me.SPEC.HeaderText = "规格"
        Me.SPEC.Name = "SPEC"
        Me.SPEC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SPEC.Width = 150
        '
        'Material
        '
        Me.Material.HeaderText = "材质"
        Me.Material.Name = "Material"
        Me.Material.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Material.Width = 60
        '
        'CODE
        '
        Me.CODE.HeaderText = "ERP号"
        Me.CODE.Name = "CODE"
        Me.CODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CODE.Width = 80
        '
        'BtnRemove
        '
        Me.BtnRemove.Location = New System.Drawing.Point(189, 309)
        Me.BtnRemove.Name = "BtnRemove"
        Me.BtnRemove.Size = New System.Drawing.Size(91, 23)
        Me.BtnRemove.TabIndex = 12
        Me.BtnRemove.Text = "删除所选管件"
        Me.BtnRemove.UseVisualStyleBackColor = True
        '
        'Pipe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 342)
        Me.Controls.Add(Me.BtnRemove)
        Me.Controls.Add(Me.AddFitting)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.pipeLength)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pipeMaterial)
        Me.Controls.Add(Me.pipeSpec)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(2000, 1000)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(490, 380)
        Me.MinimizeBox = False
        Me.Name = "Pipe"
        Me.Tag = ""
        Me.Text = "输入管道信息"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pipeSpec As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pipeLength As System.Windows.Forms.TextBox
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pipeMaterial As System.Windows.Forms.ComboBox
    'Friend WithEvents Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AddFitting As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents BtnRemove As System.Windows.Forms.Button
    Friend WithEvents PCS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fittingName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SPEC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Material As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CODE As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
