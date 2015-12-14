Imports System.Drawing
Imports System.Windows.Forms


Public Class selectFitting

    Dim myGrid As DataGridView
    Dim myPipe As mPipe
    Public Sub New(ByRef g As DataGridView, ByRef p As mPipe)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        myGrid = g
        myPipe = p
    End Sub



    Private Sub fittingMaterial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles fittingMaterial.SelectedIndexChanged
        With fittingName
            .Enabled = True
            .ForeColor = Color.Black
            .Items.Clear()
        End With

        Dim sqlString As String = "select distinct cinvname from inventory where cinvname not like '%管' and cinvccode like '04%' and cinvdefine9= '" & Me.fittingMaterial.SelectedItem & "'"
        Dim db As New getDatabaseData
        db.retrieveDatabase(sqlString, fittingName)

    End Sub

    Private Sub fittingName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles fittingName.SelectedIndexChanged
        With fittingSpec
            .Items.Clear()
            .ForeColor = Color.Black
            .Enabled = True
        End With
        If Microsoft.VisualBasic.Right(fittingName.SelectedItem, 2) = "法兰" Then
            hasBolts.Visible = True
        End If

        Dim sqlString As String = "select cinvstd,cinvname,cinvdefine9,cinvdefine10,cinvcode from inventory where cinvccode like '04%'and cinvdefine9='" & Me.fittingMaterial.SelectedItem & "' and cinvname = '" & Me.fittingName.SelectedItem & "'"
        Dim db As New getDatabaseData
        db.retrieveDatabase(sqlString, fittingSpec)

    End Sub

    Private Sub fittingSpec_SelectedIndexChanged(sender As Object, e As EventArgs) Handles fittingSpec.SelectedIndexChanged
        PCS.Focus()
        PCS.SelectAll()
        BtnOK.Enabled = True
    End Sub
    Private Sub PCS_TextChanged(sender As Object, e As EventArgs) Handles PCS.TextChanged
        BtnOK.Enabled = False
        Dim length As String = PCS.Text
        For Each x As Char In length
            Dim n As Integer = Asc(x)
            If (n < 47 Or n > 57) And n <> 47 Then
                MsgBox("管道长度应为数字，不能为空值", MsgBoxStyle.Critical)
                BtnOK.Enabled = False
                PCS.SelectAll()
                Exit For
            Else
                BtnOK.Enabled = True
            End If
        Next
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Dim thisFitting As New mPipeFitting
        thisFitting.getValue(fittingSpec.SelectedItem)
        thisFitting.PCS = PCS.Text
        '把管件信息加入管道信息
        myPipe.Fittings.Add(thisFitting)
        '更新管件信息列表
        With thisFitting

            myGrid.Rows.Add(.PCS, .Name, .Specification, .Material, .ERPCode)
        End With


        If hasBolts.Visible And hasBolts.Checked Then
            Dim selectBolts As New flangeAtt(thisFitting, myGrid, myPipe)
            selectBolts.Show()
        End If

        Me.Dispose()
    End Sub



End Class
