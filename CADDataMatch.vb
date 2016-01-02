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
            myPipe.pipeNo = ent.Handle.Value.ToString
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


        '输出至图形表格，并在图上标注
        <CommandMethod("display")> _
        Public Sub showInfo()
            Dim certaintype() As filterType = {filterType.Line}
            Dim LineCollection As DBObjectCollection = getValidCollection(getSelection(certaintype))
            Dim s(,) As String

            Try
                s = PFTo2DStr(LineCollection)
            Catch ex As ArgumentException
                Exit Sub
            End Try

            '插入BOM表
            If s.GetLength(0) > 0 Then
                Dim CN As String() = {"序号", "名称", "规格", "材质", "数量", "单位"}
                creatTable(s.GetLength(0), 5, CN, s)
            End If

            'TODO:插入注释
            createAnn(LineCollection, s)
        End Sub


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


        ''' <summary>
        ''' 把管道、管件属性分别集中后，合并重复项，再转换为二维数组
        ''' </summary>
        ''' <param name="c">直线集合</param>
        ''' <param name="count">没有属性的直线数量，作为判断是否所有直线均无属性的依据</param>
        ''' <returns>把所有管道和管件的属性合并成为二维数组，返回给调用程序</returns>
        ''' <remarks></remarks>
        Public Function PFTo2DStr(ByVal c As DBObjectCollection, Optional ByRef count As Integer = 0) As String(,)
            Dim Pipes As New List(Of mPipe)
            Dim Fittings As New List(Of mPipeFitting)
            For Each l As Line In c
                Dim myPipe As New mPipe
                myPipe = readAtt(l)
                If myPipe.pipeNo Is Nothing Then myPipe.setPipeNo(l.Handle.Value.ToString) '兼容旧版本
                '判断myPipe是否含有属性，如果有进行以下处理。如果没有退出循环。
                If myPipe.isNull And myPipe.Fittings.Count = 0 Then
                    count += 1
                Else
                    '添加管件信息
                    Fittings.AddRange(myPipe.Fittings)

                    '添加管道信息
                    myPipe.Fittings.Clear()
                    Pipes.Add(myPipe)
                End If
            Next
            '如果所有直线都没有管道管件信息，那么退出程序并报警。
            If count = c.Count Then
                MsgBox("选择的直线不含有管道管件信息，请选择有信息的直线！", MsgBoxStyle.Critical, "选择错误")
                Dim e As New ArgumentException
                Throw e
                Exit Function
            End If

            '管道信息合并
            combinePipeAndFittings(Pipes)
            '管件信息合并
            combinePipeAndFittings(Fittings)

            Dim totalAmount As Integer = Pipes.Count + Fittings.Count
            Dim pipeTable(totalAmount - 1, 7) As String
            Dim i As Integer = 0
            Do Until i = totalAmount
                Select Case i
                    Case Is < Pipes.Count
                        For Each p As mPipe In Pipes
                            Dim s() As String
                            s = DBOrderToExcelOrder(p.setString)
                            For j As Integer = 0 To 7
                                pipeTable(i, j) = s(j)
                            Next
                            i += 1
                        Next
                    Case Else
                        For Each p As mPipeFitting In Fittings
                            Dim s() As String
                            s = DBOrderToExcelOrder(p.setString)
                            For j As Integer = 0 To 7
                                pipeTable(i, j) = s(j)
                            Next
                            i += 1
                        Next
                End Select
            Loop
            Return pipeTable
        End Function

        Private Sub combinePipeAndFittings(ByRef s As List(Of mPipe))
            Dim i As Integer
            Dim j As Integer

            For i = 0 To s.Count - 2
                j = i + 1
                Do While j < s.Count
                    If s.Item(i).equals(s.Item(j)) Then
                        s.Item(i) = s.Item(i) + s.Item(j)
                        s.RemoveAt(j)
                        j -= 1
                    End If
                    j += 1
                Loop
            Next
        End Sub

        Private Sub combinePipeAndFittings(ByRef s As List(Of mPipeFitting))
            Dim i As Integer
            Dim j As Integer

            For i = 0 To s.Count - 2
                j = i + 1
                Do While j < s.Count
                    If s.Item(i).equals(s.Item(j)) Then
                        s.Item(i) = s.Item(i) + s.Item(j)
                        s.RemoveAt(j)
                        j -= 1
                    End If
                    j += 1
                Loop
            Next
        End Sub

        '输出信息至EXCEL表格
        Sub export2Excel(ByVal c As DBObjectCollection)

            Dim count As Integer = 0 '计算没有属性管道的数量
            Dim pipeTable As String(,)
            Try
                pipeTable = PFTo2DStr(c, count)
            Catch ex As ArgumentException
                Exit Sub
            End Try

            Dim row As Integer = pipeTable.GetLength(0)
            Dim noRow As Integer = 8 + row

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

        ''' <summary>
        ''' 把管道管件中定义的属性顺序转换成管道管件清单表格中的属性顺序
        ''' </summary>
        ''' <param name="pipeAtt">管道、管件的属性（对应数据库中属性中的顺序)构成的一维数组</param>
        ''' <returns>返回管道管件清单中的属性顺序构成的一维数组</returns>
        ''' <remarks>根据管道管件清单格式从货品名称列开始一一对应，PF(5)对应的品牌栏空白，pipeAtt(5)为ERP号，对应清单中第7栏</remarks>
        Private Function DBOrderToExcelOrder(ByRef pipeAtt As String()) As String()
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


        Public Sub creatTable(ByVal RowNum As Integer, ByVal ColNum As Integer, ByVal columnTitle() As String, ByVal content(,) As String)
            Dim db As Database = HostApplicationServices.WorkingDatabase
            Dim T As New Table
            If RowNum < 1 Or ColNum < 1 Then
                Dim e As New ArgumentException
                Throw e
                Exit Sub
            End If
            T.SetSize(RowNum + 2, ColNum + 1) '行数为内容行加表标题和栏标题
            T.TableStyle = db.Tablestyle

            'TODO:插入表标题及内容
            T.Cells(0, 0).TextString = "材料清单"
            For i As Integer = 0 To ColNum
                T.Cells(1, i).Value = columnTitle(i)
            Next
            Dim row As Integer = 0
            Dim col As Integer = 0
            For row = 2 To RowNum + 1
                For col = 0 To ColNum
                    If col = 0 Then
                        T.Cells(row, col).Value = row - 1
                    Else
                        T.Cells(row, col).Value = content(row - 2, col - 1)
                    End If

                Next
            Next
            T.GenerateLayout()

            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
            Dim pt As PromptPointResult = ed.GetPoint("请选择材料表的位置(默认原点)：")
            If pt.Status = PromptStatus.OK Then
                T.Position = pt.Value
            Else
                T.Position = Point3d.Origin
            End If

            Dim trans As Transaction = db.TransactionManager.StartTransaction
            Using trans
                Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
                Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
                btr.AppendEntity(T)
                trans.AddNewlyCreatedDBObject(T, True)
                trans.Commit()
            End Using
        End Sub

        '在有属性的直线旁边显示管道属性
        'TODO:修改显示在图形中的标记

        Private Sub createAnn(ByVal LineCollection As DBObjectCollection, ByVal pipeDataTable As String(,))
            Dim lNum As String
            Dim item As String = ""
            For Each l As Line In LineCollection
                lNum = l.Handle.Value.ToString
                For i As Integer = 0 To pipeDataTable.GetLength(0) - 1
                    If pipeDataTable(i, 6).Contains(lNum) Then
                        If item = "" Then
                            item = (i + 1).ToString
                        Else
                            item &= "," & (i + 1).ToString
                        End If
                    End If
                Next
                createLabel(l, item)
                item = ""
            Next
        End Sub

        Sub createLabel(ByVal l As Line, ByVal s As String)
            Dim db As Database = HostApplicationServices.WorkingDatabase
            '创建引线
            Dim firstVertex As Point3d = New Point3d((l.StartPoint.X + l.EndPoint.X) / 2, (l.StartPoint.Y + l.EndPoint.Y) / 2, 0)
            Dim offSetValue As Double
            offSetValue = l.Length / 5
            Dim secondVertex As New Point3d(l.StartPoint.X + offSetValue, l.StartPoint.Y + offSetValue, 0)
            Dim thirdVertex As New Point3d(secondVertex.X + offSetValue, secondVertex.Y, 0)

            'Dim myLeader As New Leader
            'myLeader.AppendVertex(firstVertex)
            'myLeader.AppendVertex(secondVertex)
            'myLeader.AppendVertex(thirdVertex)
            'myLeader.SetPlane(New Plane)

            Dim t As New MText
            With t
                .Width = s.Length * 5
                .Height = 5
                .Contents = s
                .Location = thirdVertex
            End With

            Dim myMLeader As New MLeader
            myMLeader.SetDatabaseDefaults()
            myMLeader.ContentType = ContentType.MTextContent

            With myMLeader
                Dim idx As Integer
                idx = .AddLeaderLine(secondVertex)
                .AddFirstVertex(idx, firstVertex)
                '.AddLastVertex(idx, thirdVertex)
                .MText = t
            End With

            ToModelSpace(myMLeader)

            '创建DBTEXT集合

            'Dim v As New Vector3d(4, 0, 0)
            'For i As Integer = 0 To s.Length - 1
            '    Dim ent As DBText = New DBText
            '    With ent
            '        .Position = thirdVertex
            '        .TextString = s(i)
            '        .Height = 3
            '    End With
            '    ToModelSpace(ent)
            '    thirdVertex += v * s(i).Length
            'Next

        End Sub

        Public Function ToModelSpace(ByVal ent As Entity) As ObjectId
            Dim db As Database = HostApplicationServices.WorkingDatabase
            Dim entId As ObjectId
            Dim trans As Transaction = db.TransactionManager.StartTransaction
            Using trans
                Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
                Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)

                entId = btr.AppendEntity(ent)
                trans.AddNewlyCreatedDBObject(ent, True)
                trans.Commit()
            End Using
        End Function

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