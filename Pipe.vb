Imports System.Windows.Forms
Imports System.Data.SqlClient



Partial Class Pipe
    Public pipeData As New mPipe
    Private dataBaseAvailable As Boolean = False
    '显示时为防止管道规格combobox自动更新引起的不确定性，设立显示界面指示器并将更新管道规格的SQL语句传至公共变量，由display()函数更新combobox
    Private isDisplay As Boolean = False
    Private displaySql As String = ""
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '窗体加载即检索数据库加载管道材质。如无法连接数据库则出报警信息
        If isDisplay = True Then Exit Sub
        Dim sqlString As String = "select distinct cinvdefine9 from inventory where cinvccode like '04%' and cinvdefine9 <> 'null' and cinvname like'%管'"
        Dim db As New getDatabaseData
        db.retrieveDatabase(sqlString, pipeMaterial)

    End Sub



    '将界面中输入的数值传递给结构数据，供Class1调用。
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click

        pipeData.Length = pipeLength.Text
        pipeData.getValue(pipeSpec.SelectedItem)
        Me.Close()

    End Sub

    '判断管道长度值是否有效
    Private Sub pipeLength_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pipeLength.TextChanged
        Dim length As String = pipeLength.Text

        For Each x As Char In length
            Dim n As Integer = Asc(x)
            If (n < 46 Or n > 57) And n <> 47 Then
                MsgBox("管道长度应为数字，不能为空值", MsgBoxStyle.Critical)
                BtnOK.Enabled = False
                pipeLength.SelectAll()
                Exit For
            Else
                BtnOK.Enabled = True
            End If
        Next
    End Sub

    '确定管道材质时填充规格combobox
    Private Sub pipeMaterial_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pipeMaterial.SelectedIndexChanged

        If pipeMaterial.SelectedIndex >= 0 Then

            pipeSpec.Items.Clear()
            pipeSpec.Enabled = True
            pipeSpec.ForeColor = System.Drawing.Color.Black

            Dim sqlstring = "select cinvstd,cinvname,cinvdefine9,cinvdefine10,cinvcode from inventory where cinvname like '%管' and cinvdefine9 = '" & pipeMaterial.SelectedItem & "'"
            Dim db As New getDatabaseData
            db.retrieveDatabase(sqlstring, pipeSpec)

        Else
            MsgBox("请选择管道材质!", MsgBoxStyle.Exclamation)
            pipeMaterial.Focus()
        End If
    End Sub


    '管道规格变化时，如已选择端头种类则更新端头规格
    Private Sub pipeSpec_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pipeSpec.SelectedIndexChanged

        pipeLength.Text = "3"
        pipeLength.Focus()
        pipeLength.SelectAll()

    End Sub



    '公共方法显示图形中的数据

    Public Sub display(ByVal myPipe As mPipe)
        '显示材质
        isDisplay = True
        Dim sqlString As String = "select distinct cinvdefine9 from inventory where cinvccode like '04%' and cinvdefine9 <> 'null' and cinvname like'%管'"
        Dim db As New getDatabaseData
        db.retrieveDatabase(sqlString, pipeMaterial)
        pipeMaterial.SelectedItem = myPipe.Material
        '显示管道规格
        displaySql = "select cinvstd,cinvname,cinvdefine9,cinvdefine10,cinvcode from inventory where cinvname like '%管' and cinvdefine9 = '" & pipeMaterial.SelectedItem & "'"
        db.retrieveDatabase(displaySql, pipeSpec)
        For Each item As String In pipeSpec.Items
            If item.Contains(myPipe.ERPCode) Then
                pipeSpec.SelectedItem = item
                Exit For
            End If
        Next

        '显示管道长度
        pipeLength.Text = myPipe.Length
        '显示端头种类和规格
        DataGridView1.Rows.Clear()
        For Each f As mPipeFitting In myPipe.Fittings
            With f
                DataGridView1.Rows.Add(.PCS, .Name, .Specification, .Material, .ERPCode)
            End With
        Next
        Me.ShowDialog()
    End Sub

    Sub refillColumns(ByVal x As String, ByVal c As ComboBox)
        For Each m As String In c.Items
            If m = x Then
                c.SelectedItem = m
            End If
        Next
    End Sub

    '从数据库选择管件
    Private Sub AddFitting_Click(sender As Object, e As EventArgs) Handles AddFitting.Click
        Dim frmSelectFitting As New selectFitting(DataGridView1, pipeData)
        With frmSelectFitting.fittingMaterial
            .Items.Clear()
            Dim sqlString As String = "select distinct cinvdefine9 from inventory where cinvccode like '04%' and cinvdefine9 <> 'null'"
            Dim db As New getDatabaseData
            db.retrieveDatabase(sqlString, frmSelectFitting.fittingMaterial)
            frmSelectFitting.StartPosition = FormStartPosition.CenterScreen
            frmSelectFitting.Show()
            .SelectedItem = Me.pipeMaterial.SelectedItem
        End With
    End Sub
    Private Sub BtnRemove_Click(sender As Object, e As EventArgs) Handles BtnRemove.Click
        '删除DATAGRIDVIEW中的数据
        With DataGridView1
            For Each row As DataGridViewRow In .SelectedRows
                Dim i As Integer = row.Index
                .Rows.RemoveAt(i)
                pipeData.Fittings.RemoveAt(i)
            Next

        End With

    End Sub
End Class

