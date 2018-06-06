Imports System.Drawing
Imports System.Windows.Forms

Public Class flangeAtt

    Public flangeProp As mPipeFitting
    Private myGrid As DataGridView
    Private myPipe As mPipe

    Public Sub New(ByVal flange As mPipeFitting, ByRef g As DataGridView, ByRef p As mPipe)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        flangeProp = flange
        myGrid = g
        myPipe = p

    End Sub

    Private Sub flangepropAtt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '填充螺栓材质并打开对话窗口
        Dim sqlstring As String
        sqlstring = "select distinct cinvdefine9 from inventory where cinvname = '外六角螺栓'"
        Dim db As New getDatabaseData
        db.retrieveDatabase(sqlstring, boltMaterial)
        Dim material As String = flangeProp.Material
        If material.Contains("316") Then
            boltMaterial.SelectedItem = "AISI316"
        Else
            boltMaterial.SelectedItem = "AISI304"
        End If
        '设定法兰垫片选项
        sqlstring = "select cinvstd,cinvname,cinvdefine9,cinvdefine10,cinvcode from inventory where cinvname like '%法兰垫片' and cinvdefine10 = '" & flangeProp.DN & "'"

        db.retrieveDatabase(sqlstring, gasketSpec)
        With gasketSpec
            .Enabled = True
            .ForeColor = Color.Black
        End With

        gasketSpec.SelectedIndex = 0
    End Sub

    Private Sub boltsSpec_SelectedIndexChanged(sender As Object, e As EventArgs) Handles boltsSpec.SelectedIndexChanged
        boltsPCS.Focus()
        boltsPCS.SelectAll()

        Dim m As String = boltsSpec.SelectedItem
        Dim specs() As String = m.Split("x")

        '选择平垫弹垫和螺母
        Dim sqlString As String = "select cinvstd,cinvname,cinvdefine9,cinvdefine10,cinvcode from inventory where cinvdefine9='" & boltMaterial.SelectedItem & "' and " & "cinvname = '平垫' and cinvstd like '" & specs(0) & "'"
        Dim db As New getDatabaseData
        db.retrieveDatabase(sqlString, washer)
        With washer
            .ForeColor = Color.Black
            .SelectedIndex = 0
            .Enabled = True
        End With


        sqlString = "select cinvstd,cinvname,cinvdefine9,cinvdefine10,cinvcode from inventory where cinvdefine9='" & boltMaterial.SelectedItem & "' and " & "cinvname = '弹垫' and cinvstd like '" & specs(0) & "'"

        db.retrieveDatabase(sqlString, springWasher)
        With springWasher
            .ForeColor = Color.Black
            .SelectedIndex = 0
            .Enabled = True
        End With


        sqlString = "select cinvstd,cinvname,cinvdefine9,cinvdefine10,cinvcode from inventory where cinvdefine9='" & boltMaterial.SelectedItem & "' and " & "cinvname = '螺母' and cinvstd like '" & specs(0) & "'"
        db.retrieveDatabase(sqlString, nut)
        With nut
            .ForeColor = Color.Black
            .SelectedIndex = 0
            .Enabled = True
        End With



    End Sub
    Private Sub boltMaterial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles boltMaterial.SelectedIndexChanged

        Dim boltAtt As String() = selectBoltSpec(flangeProp)
        Me.Cursor = Cursors.WaitCursor
        Dim sqlString As String = "select cinvstd,cinvname,cinvdefine9,cinvdefine10,cinvcode from inventory where cinvdefine9='" & boltMaterial.SelectedItem & "' and " & "cinvname = '外六角螺栓'"
        With boltsSpec
            .Enabled = True
            .ForeColor = Color.Black
        End With
        Me.Cursor = Cursors.Default
        Dim db As New getDatabaseData
        db.retrieveDatabase(sqlString, boltsSpec)

        '选定匹配的螺栓型号
        If boltAtt(0) <> "" Then
            For Each item As String In boltsSpec.Items
                If item.Contains(boltAtt(0)) Then
                    boltsSpec.SelectedItem = item
                    boltsPCS.Text = boltAtt(1)
                    Exit For
                End If
            Next
        End If

    End Sub

    Private Sub PCS_TextChanged(sender As Object, e As EventArgs) Handles boltsPCS.TextChanged, gasketPCS.TextChanged

    End Sub


    '根据法兰规格确定螺栓规格及数量
    Private Function selectBoltSpec(ByVal flange As mPipeFitting) As String()
        Dim DN As String
        Dim boltSpec(1) As String
        If flange.DN = "" Then
            boltSpec(0) = ""
            boltSpec(1) = "0"
            Return boltSpec
        Else
            DN = flange.DN
        End If


        '确定螺栓直径
        Select Case DN
            Case "DN10", "DN15", "DN20", "DN25"
                boltSpec(0) = "M12x60"
                boltSpec(1) = "4"
                Return boltSpec
                Exit Function
            Case "DN32", "DN40", "DN50", "DN65", "DN80", "DN100", "DN125"
                boltSpec(0) = "M16x"
            Case "DN150", "DN200", "DN250", "DN300", "DN350"
                boltSpec(0) = "M20x"
            Case "DN400", "DN450", "DN500"
                boltSpec(0) = "M24x"
            Case "DN600"
                boltSpec(0) = "M27x"
        End Select

        '确定螺栓长度
        Select Case DN
            Case "DN32", "DN40"
                boltSpec(0) &= "70"
            Case "DN50", "DN65", "DN80"
                boltSpec(0) &= "75"
            Case "DN100", "DN125"
                boltSpec(0) &= "80"
            Case "DN150", "DN200", "DN250", "DN300"
                boltSpec(0) &= "90"
            Case "DN350"
                boltSpec(0) &= "95"
            Case "DN400"
                boltSpec(0) &= "110"
            Case "DN450"
                boltSpec(0) &= "115"
            Case "DN500"
                boltSpec(0) &= "120"
            Case "DN600"
                boltSpec(0) &= "130"
        End Select

        '确定螺栓数量
        Select Case DN
            Case "DN32", "DN40", "DN50"
                boltSpec(1) &= "4"
            Case "DN65", "DN80", "DN100", "DN125", "DN150", "DN200"
                boltSpec(1) &= "8"
            Case "DN250", "DN300"
                boltSpec(1) &= "12"
            Case "DN350", "DN400"
                boltSpec(1) &= "16"
            Case "DN450", "DN500", "DN600"
                boltSpec(1) &= "20"
        End Select

        Return boltSpec

    End Function


    Private Sub addBotlsGasket_Click(sender As Object, e As EventArgs) Handles addBotlsGasket.Click
        If textIsNumber(boltsPCS.Text) And textIsNumber(gasketPCS.Text) Then
            '添加螺栓
            addFittingtoPipe(boltsSpec, boltsPCS.Text)
            If CheckBox1.Checked Then
                '添加平垫、弹垫、螺母
                addFittingtoPipe(washer, boltsPCS.Text)
                addFittingtoPipe(springWasher, boltsPCS.Text)
                addFittingtoPipe(nut, boltsPCS.Text)
            End If
            '添加法兰垫片
            addFittingtoPipe(gasketSpec, gasketPCS.Text)
            Me.Dispose()
        End If
    End Sub

    Private Sub addFittingtoPipe(ByVal fullSpecification As ComboBox, ByVal PCS As String)
        If PCS = "0" Then Exit Sub
        Dim thisFitting As New mPipeFitting
        thisFitting.PCS = PCS
        thisFitting.getValue(fullSpecification.SelectedItem)
        myPipe.Fittings.Add(thisFitting)
        With thisFitting
            myGrid.Rows.Add(.PCS, .Name, .Specification, .Material, .ERPCode)
        End With
    End Sub

    Private Function textIsNumber(ByVal text As String) As Boolean
        Dim num As Integer
        Try
            num = CInt(text)
            If num < 0 Then
                MsgBox("法兰附件数量不可以为负值！", MsgBoxStyle.Critical, "数值为负")
                Return False
            Else
                Return True
            End If
        Catch ex As System.OverflowException
            Return False
            MsgBox("法兰附件数量框内只允许输入数字！", MsgBoxStyle.Exclamation, "数值类型错误")
        End Try
    End Function

End Class