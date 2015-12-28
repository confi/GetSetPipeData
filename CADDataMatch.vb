' (C) Copyright 2011 by  
'
Imports System
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.EditorInput
Imports Microsoft.Office.Interop
Imports Autodesk.AutoCAD.Windows


' This line is not mandatory, but improves loading performances
<Assembly: CommandClass(GetType(GetSetPipeData.ISOmetric))> 

Namespace GetSetPipeData

    ' This class is instantiated by AutoCAD for each document when
    ' a command is called by the user the first time in the context
    ' of a given document. In other words, non static data in this class
    ' is implicitly per-document!
    Public Class ISOmetric

        '添加右键菜单
        '=================================================================

        <CommandMethod("AWSISO")> _
        Public Sub AddContextMenu()

            Dim ce As New ContextMenuExtension
            ce.Title = "AWSISO"
            Dim input As New MenuItem("输入数据(input)")
            AddHandler input.Click, New EventHandler(AddressOf input_click)
            Dim output As New MenuItem("输出到EXCEL（output)")
            AddHandler output.Click, New EventHandler(AddressOf output_click)
            Dim display As New MenuItem("插入管线说明(display)")
            AddHandler display.Click, New EventHandler(AddressOf display_click)
            ce.MenuItems.Add(input)
            ce.MenuItems.Add(output)
            ce.MenuItems.Add(display)
            Autodesk.AutoCAD.ApplicationServices.Application.AddDefaultContextMenuExtension(ce)
        End Sub

        Sub input_click(ByVal sender As Object, ByVal e As EventArgs)
            Dim doc As Document = Application.DocumentManager.MdiActiveDocument
            doc.SendStringToExecute("input ", True, False, True)
        End Sub

        Sub output_click(ByVal sender As Object, ByVal e As EventArgs)
            Dim doc As Document = Application.DocumentManager.MdiActiveDocument
            doc.SendStringToExecute("output ", True, False, True)
        End Sub

        Sub display_click(ByVal sender As Object, ByVal e As EventArgs)
            Dim doc As Document = Application.DocumentManager.MdiActiveDocument
            doc.SendStringToExecute("display ", True, False, True)
        End Sub

        '=================================================================

        <CommandMethod("input")> _
        Public Sub inputInformation()
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
            Dim i As Boolean = True

            Do While i
                Dim lineOption As PromptEntityOptions = New PromptEntityOptions(ControlChars.Lf & "请选择需要添加信息的直线")
                Dim lineResult As PromptEntityResult = ed.GetEntity(lineOption)

                '如果选择成功，则进行添加信息处理
                If (lineResult.Status = PromptStatus.OK) Then
                    Dim db As Database = Application.DocumentManager.MdiActiveDocument.Database
                    Using trans As Transaction = db.TransactionManager.StartTransaction
                        Dim ent As Object = trans.GetObject(lineResult.ObjectId, OpenMode.ForRead)
                        '判断选择的图形是否为直线
                        If (ent.GetType.Name = "Line") Then
                            Dim line As Line = CType(ent, Line)

                            '如果直线没有字典则创建扩展数据字典。
                            If line.ExtensionDictionary.IsNull Then
                                Dim inputUI As Pipe = New Pipe
                                '显示输入界面
                                inputUI.ShowDialog()
                                '创建字典
                                writeAtt(line, inputUI.pipeData)

                            Else
                                readModifyAtt(line)
                            End If
                        Else

                        End If
                        trans.Commit()
                    End Using
                Else
                    i = False
                End If
            Loop

        End Sub



        '读取直线字典并作修改
        Sub readModifyAtt(ByVal ent As Line)
            Dim db As Database = Application.DocumentManager.MdiActiveDocument.Database
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

            Using trans As Transaction = db.TransactionManager.StartTransaction

                Dim dic As DBDictionary = trans.GetObject(ent.ExtensionDictionary, OpenMode.ForRead)
                Dim inputUI As Pipe = New Pipe
                
                If dic.Contains("pipeAtt") Then
                    '读取管道属性
                    Dim entryId As ObjectId = dic.GetAt("pipeAtt")

                    ed.WriteMessage(ControlChars.Lf & "该直线已经设置属性，请核对或修改！" & vbCrLf)
                    Dim pipeTable As DataTable = trans.GetObject(entryId, OpenMode.ForRead)
                    dataTableToPipe(pipeTable, inputUI.pipeData)

                    inputUI.display(inputUI.pipeData)
                    writeAtt(ent, inputUI.pipeData)
                Else

                    '显示输入界面
                    inputUI.ShowDialog()
                    '设置管道属性
                    writeAtt(ent, inputUI.pipeData)

                End If

                trans.Commit()
            End Using

        End Sub

        '写入数据到直线字典
        Sub writeAtt(ByVal ent As Line, ByVal myPipe As mPipe)

            '创建pipeAtt纪录
            Dim pipeTable As New DataTable
            myPipe.pipeNo = ent.Handle.Value
            pipeTable = fillPipeDataTable(myPipe)

            Dim db As Database = HostApplicationServices.WorkingDatabase
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
            Using trans As Transaction = db.TransactionManager.StartTransaction
                '如果直线没有字典，则创建字典
                If ent.ExtensionDictionary.IsNull Then
                    ent.UpgradeOpen()
                    ent.CreateExtensionDictionary()
                End If

                Dim dic As DBDictionary = trans.GetObject(ent.ExtensionDictionary, OpenMode.ForWrite)

                dic.SetAt("pipeAtt", pipeTable)
                trans.AddNewlyCreatedDBObject(pipeTable, True)
                trans.Commit()

                ed.WriteMessage(vbCrLf, "已经写入管道属性。")
                'For Each value As TypedValue In myXrecord.Data
                '    If value.Value.ToString <> "" Then
                '        ed.WriteMessage(value.Value.ToString() & ";")
                '    End If
                'Next
                ed.WriteMessage(vbCrLf)
            End Using
        End Sub

        Private Function fillPipeDataTable(ByVal myPipe As mPipe) As DataTable
            '建立数据表
            Dim attTable As New DataTable
            attTable.TableName = "PipeAttTable"
            Dim columName() As String = {"Name", "Spec", "Material", "PCS", "Unit", "ERPNo", "PipeNo"}
            Dim i As Integer
            Dim j As Integer
            Dim k As Integer
            '设置标题栏
            For i = 0 To columName.Length - 1
                attTable.AppendColumn(CellType.CharPtr, columName(i))
            Next
            '加入内容
            For j = 0 To myPipe.Fittings.Count
                Dim attstring As String()
                If j = myPipe.Fittings.Count Then
                    attstring = myPipe.setString()
                Else
                    myPipe.Fittings.Item(j).pipeNo = myPipe.pipeNo
                    attstring = myPipe.Fittings.Item(j).setString()
                End If

                Dim ROW As New DataCellCollection
                For k = 0 To columName.Length - 1
                    Dim cell As New DataCell
                    cell.SetString(attstring(k))
                    ROW.Add(cell)
                Next
                attTable.AppendRow(ROW, True)
            Next

            Return attTable
        End Function

        Private Sub dataTableToPipe(ByVal dt As DataTable, ByVal myPipe As mPipe)

            For i As Integer = 0 To dt.NumRows - 1
                Dim att As String = ""
                Dim mid As String = ""
                For j As Integer = 0 To dt.NumColumns - 1

                    Dim TC As DataCell = dt.GetCellAt(i, j)

                    Select Case j
                        Case 0
                            mid = TC.Value.ToString
                        Case 1
                            att = TC.Value.ToString & ";" & mid & ";"
                        Case 3, 6
                        Case 4
                            att &= ";"
                        Case Else
                            att &= TC.Value.ToString & ";"
                    End Select
                Next

                '最后一行是管道信息，其它行都是管件信息
                If i = dt.NumRows - 1 Then
                    myPipe.getValue(att)
                    myPipe.Length = dt.GetCellAt(i, 3).Value.ToString
                    If dt.NumColumns = 7 Then myPipe.pipeNo = dt.GetCellAt(i, 6).Value.ToString '兼容旧版本。旧版本中没有管道编号一栏
                Else
                    Dim f As New mPipeFitting
                    f.getValue(att)
                    f.PCS = dt.GetCellAt(i, 3).Value.ToString
                    If dt.NumColumns = 7 Then f.pipeNo = dt.GetCellAt(i, 6).Value.ToString '同上
                    myPipe.Fittings.Add(f)
                End If
            Next
        End Sub

        '定义过滤类型
        Public Enum filterType
            Line = 0
            Text = 1
            Circle = 2
        End Enum


        <CommandMethod("output")> _
        Public Sub extractInfo()
            Dim certainType() As filterType = {filterType.Line}
            Dim LineCollection As DBObjectCollection = getValidCollection(getSelection(certainType))
            export2Excel(LineCollection)
        End Sub

        'TODO:标示所有管道属性
        '<CommandMethod("display")> _
        'Public Sub showInfo()
        '    Dim certaintype() As filterType = {filterType.Line}
        '    Dim LineCollection As DBObjectCollection = getValidCollection(getSelection(certaintype))
        '    For Each l As Line In LineCollection
        '        createLabel(l)
        '    Next
        'End Sub


        Function getSelection(ByVal tps As filterType()) As DBObjectCollection
            Dim doc As Document = Application.DocumentManager.MdiActiveDocument
            Dim db As Database = HostApplicationServices.WorkingDatabase
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
            Dim entity As Entity = Nothing
            Dim entityCollection As New DBObjectCollection
            Dim selops As New PromptSelectionOptions()
            selops.MessageForAdding = "请选择统计范围"

            '建立选择的过滤器内容
            Dim filList(tps.Length + 1) As TypedValue
            filList(0) = New TypedValue(DxfCode.Operator, "<or")
            filList(tps.Length + 1) = New TypedValue(DxfCode.Operator, "or>")
            For i As Integer = 0 To tps.Length - 1
                filList(i + 1) = New TypedValue(DxfCode.Start, tps(i).ToString)
            Next

            '建立过滤器
            Dim filter As New SelectionFilter(filList)

            '按照过滤器进行选择
            Dim ents As PromptSelectionResult = ed.GetSelection(selops, filter)
            If ents.Status = PromptStatus.OK Then
                Using tr As Transaction = db.TransactionManager.StartTransaction
                    Dim SS As SelectionSet = ents.Value
                    For Each id As ObjectId In SS.GetObjectIds
                        entity = tr.GetObject(id, OpenMode.ForRead, False)
                        If entity <> Nothing Then
                            entityCollection.Add(entity)
                        End If
                    Next
                End Using
            End If
            Return entityCollection
        End Function

        '返回含有pipeAtt纪录的实体
        Function getValidCollection(ByVal col As DBObjectCollection) As DBObjectCollection
            Dim db As Database = HostApplicationServices.WorkingDatabase
            Dim validCollections As New DBObjectCollection
            Using tr As Transaction = db.TransactionManager.StartTransaction
                Dim dic As DBDictionary
                For Each e As Entity In col
                    If Not (e.ExtensionDictionary.IsNull) Then
                        dic = tr.GetObject(e.ExtensionDictionary, OpenMode.ForRead)
                        If dic.Contains("pipeAtt") Then
                            validCollections.Add(e)
                        End If
                    End If
                Next
            End Using
            Return validCollections
        End Function

        '输出信息至EXCEL表格
        Sub export2Excel(ByVal c As DBObjectCollection)
            'TODO:直接把PFS改为mPipe的泛型集合，单独写一个函数输出至EXCEL，另一个函数输出至图形表格，并在图上标注
            '将管道信息读取到泛型PFS中
            Dim PFS As New List(Of String())
            Dim count As Integer = 0 '计算没有属性管道的数量
            Dim myPipe As New mPipe
            For Each e As Line In c
                myPipe = readAtt(e)
                '判断myPipe是否含有属性，如果有进行以下处理。如果没有退出循环。
                If myPipe.isNull And myPipe.Fittings.Count = 0 Then
                    count += 1
                Else
                    '添加管道信息
                    Dim pipeAtt() As String
                    pipeAtt = myPipe.setString
                    PFS.Add(pipeAttToArray(pipeAtt))
                    '添加管件信息
                    For Each f As mPipeFitting In myPipe.Fittings
                        pipeAtt = f.setString
                        PFS.Add(pipeAttToArray(pipeAtt))
                    Next
                End If
            Next
            Dim pipeTable(PFS.Count - 1, 7) As String
            Dim row As Integer
            Dim col As Integer
            For row = 0 To PFS.Count - 1
                For col = 0 To 7
                    pipeTable(row, col) = PFS(row)(col)
                Next
            Next
            Dim noRow As Integer = 8 + PFS.Count

            '尝试建立清单，找不到模板时，提交错误信息。
            Try
                Dim myExcel As New Excel.Application
                Dim myWorkbook As Excel.Workbook = myExcel.Workbooks.Open("D:\ISO\970 3D02 材料清单.xltx", , False)
                Dim myWorksheet As Excel.Worksheet = myWorkbook.Sheets("管道管件清单")

                Dim cell1 As Excel.Range  '目前操作行或单元格
                Const STARTROW As Integer = 8 '开始行


                '检验表格格式，如有不符抛出异常
                Dim title As String() = {"序号", "货品名称", "型号", "材质", "数量", "单位", "品牌", "备注", "ERP编码", "单价", "总价"}
                Dim i As Integer
                For i = 1 To 11
                    cell1 = myWorksheet.Cells(STARTROW, i)
                    'If cell1.Value Is Nothing Then cell1(i).value = ""
                    If cell1.Value.ToString <> title(i - 1) Then
                        Dim e As New ArgumentException
                        Throw e
                    End If
                Next

                '如果所有直线均不含管道属性则销毁EXCEL文件。否则使表格可见并提醒存储。
                If count = c.Count Then
                    MsgBox("所有直线均不含管道属性！", MsgBoxStyle.Critical, "信息")
                    myWorkbook.Close(Excel.XlSaveAction.xlDoNotSaveChanges)
                    myExcel.Quit()
                Else
                    With myWorksheet
                        .Range("B9:I" & noRow.ToString).Value = pipeTable
                        '.Range("A9:I9").Copy()
                        '.Range("B9:I" & noRow.ToString).PasteSpecial(Excel.XlPasteType.xlPasteFormats)
                        '.Range("A" & noRow.ToString & "I" & noRow.ToString).Borders(4).LineStyle = Excel.XlLineStyle.xlContinuous
                    End With


                    '将打印区域设为最后一行止，并将最后一行的下框线设为实线
                    myWorksheet.PageSetup.PrintArea() = "A1:H" & noRow.ToString
                    myExcel.Visible = True
                    '合并重复项
                    cell1 = myWorksheet.Range("A9:I" & noRow.ToString)
                    cell1.Sort(cell1.Cells(1, 9), Excel.XlSortOrder.xlAscending, , , , , , , , , Excel.XlSortOrientation.xlSortColumns)
                    Dim rowInOp As Integer = STARTROW
                    Do While rowInOp <= noRow
                        Dim ERPNo1 As String = myWorksheet.Range("I" & rowInOp.ToString).Value
                        Dim nextRow As Integer = rowInOp + 1
                        Dim ERPNo2 As String = myWorksheet.Range("I" & nextRow.ToString).Value
                        If ERPNo1 = ERPNo2 Then
                            With myWorksheet
                                .Range("E" & rowInOp).Value = CType(.Range("E" & rowInOp).Value, Double) + CType(.Range("E" & nextRow).Value, Double)
                                .Range("H" & rowInOp).Value &= "," & .Range("H" & nextRow).Value
                                .Rows(nextRow).delete()
                                noRow -= 1
                            End With
                        Else
                            rowInOp += 1
                        End If

                    Loop

                    '设定序号
                    With myWorksheet.Range("A9:A" & noRow.ToString)
                        .Formula = "= ROW() - 8"
                        .Copy()
                        .PasteSpecial(Excel.XlPasteType.xlPasteValues)
                    End With

                    Dim saveAs As New System.Windows.Forms.SaveFileDialog
                    saveAs.Filter = "Excel 工作薄 (*.xlsx)|*.xlsx|All files (*.*)|*.* "
                    If saveAs.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        myWorkbook.SaveAs(saveAs.FileName)
                    End If
                End If

            Catch ex As System.IO.FileNotFoundException
                MsgBox("没有找到清单模板,请将970 3D02 材料清单.xltx复制到D:\ISO目录下，并重新启动输出命令。", MsgBoxStyle.Critical, "找不到模板文件")

            Catch ex As ArgumentException
                MsgBox("请检查清单模板是否已经被修改", MsgBoxStyle.Critical, "参数错误")

            End Try
        End Sub

        Private Function pipeAttToArray(ByRef pipeAtt As String()) As String()
            '从表格第一行数据开始填写管道属性
            Dim PF(7) As String
            Dim i As Integer
            For i = 0 To 4
                PF(i) = pipeAtt(i)
            Next
            PF(6) = pipeAtt(6)
            PF(7) = pipeAtt(5)
            Return PF
        End Function

        

        '在有属性的直线旁边显示管道属性
        'TODO:修改显示在图形中的标记
        'Sub createLabel(ByVal l As Line)
        '    Dim db As Database = HostApplicationServices.WorkingDatabase

        '    If l.ExtensionDictionary.IsNull Then
        '        MsgBox("选择的直线无管道属性！", MsgBoxStyle.Exclamation)
        '        Exit Sub
        '    End If
        '    Using tr As Transaction = db.TransactionManager.StartTransaction
        '        Dim dic As DBDictionary = tr.GetObject(l.ExtensionDictionary, OpenMode.ForRead)
        '        Dim myPipe As New pipeAtt
        '        If dic.Contains("pipeAtt") Then
        '            myPipe = readAtt(l)

        '        Else
        '            MsgBox("选择的直线无管道属性！", MsgBoxStyle.Exclamation)
        '            Exit Sub
        '        End If
        '        '生成管道标签文字
        '        Dim str1 As String = "#"
        '        For i As Integer = 0 To 5
        '            str1 = str1 + myPipe.pipe(i)
        '            If i < 4 Then str1 = str1 + ","
        '        Next
        '        '生成管件标签文字
        '        Dim str2 As String = ""
        '        For i = 0 To 1
        '            If myPipe.endPoint(i, 1) <> "" Then
        '                str2 = str2 & vbCrLf & "#"
        '                For j = 0 To 5
        '                    str2 = str2 + myPipe.endPoint(i, j)
        '                    If j < 4 Then str2 = str2 + ","
        '                Next
        '            End If
        '        Next

        '        '创建MTEXT对象并添加
        '        Dim lLabel As New MText
        '        Const angleV1 As Double = Math.PI / 2
        '        Const angleV2 As Double = 3 * Math.PI / 2
        '        lLabel.TextHeight = 350
        '        lLabel.Width = 8000
        '        'lLabel.Annotative = AnnotativeStates.True

        '        '判断文字方向
        '        If l.Angle > angleV1 And l.Angle < angleV2 Then
        '            lLabel.Location = l.EndPoint
        '            lLabel.Rotation = l.Angle - Math.PI
        '        Else
        '            lLabel.Location = l.StartPoint
        '            lLabel.Rotation = l.Angle
        '        End If

        '        lLabel.Contents = str1 & str2

        '        Dim entId As ObjectId
        '        Dim bt As BlockTable = tr.GetObject(db.BlockTableId, OpenMode.ForRead)
        '        Dim btr As BlockTableRecord = tr.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
        '        entId = btr.AppendEntity(lLabel)
        '        tr.AddNewlyCreatedDBObject(lLabel, True)
        '        tr.Commit()
        '    End Using
        'End Sub

        '读取图形中的纪录写入pipeAtt
        Function readAtt(ByVal ent As Line) As mPipe
            Dim db As Database = HostApplicationServices.WorkingDatabase
            Dim myPipe As New mPipe
            '如果直线不含有扩展信息，则返回空管道对象，由调用程序通过myPipe.isNull判断。
            If ent.ExtensionDictionary.IsNull Then
                Return myPipe
                Exit Function
            End If
            Using tr As Transaction = db.TransactionManager.StartTransaction
                Dim dic As DBDictionary = tr.GetObject(ent.ExtensionDictionary, OpenMode.ForRead)
                '如果直线不含有指定属性的字典，则返回空管道对象，由调用程序通过myPipe.isNull判断。
                If Not (dic.Contains("pipeAtt")) Then
                    Return myPipe
                Else
                    Dim entryID As ObjectId = dic.GetAt("pipeAtt")
                    Dim pipeTable As DataTable = tr.GetObject(entryID, OpenMode.ForRead)
                    dataTableToPipe(pipeTable, myPipe)
                    Return myPipe
                End If
            End Using
        End Function
    End Class

End Namespace