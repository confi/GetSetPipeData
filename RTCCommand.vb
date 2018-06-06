
Imports System
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.EditorInput
Imports Microsoft.Office.Interop
Imports Autodesk.AutoCAD.Windows

<Assembly: CommandClass(GetType(AWS.RTC))> 

Namespace AWS


    Public Class RTC
        
        <CommandMethod("readTable")> _
        Public Sub ReadTables()
            Dim textType() As filterType = {0, 1, 3}
            Dim textEntities As DBObjectCollection = getSelection(textType)
            exportToExcel(textEntities)
        End Sub

        '定义过滤类型
        Public Enum filterType
            Line = 0
            TEXT = 1
            Circle = 2
            LWPolyline = 3
        End Enum

        Function getSelection(ByVal tps As filterType()) As DBObjectCollection
            Dim doc As Document = Application.DocumentManager.MdiActiveDocument
            Dim db As Database = HostApplicationServices.WorkingDatabase
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
            Dim entity As Entity = Nothing
            Dim entityCollection As New DBObjectCollection
            Dim selops As New PromptSelectionOptions()
            selops.MessageForAdding = "请选择读取范围"

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
            'Dim ents As PromptSelectionResult = ed.GetSelection(selops)
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

        Private Sub exportToExcel(tableObjects As DBObjectCollection)
            Dim textContent As New List(Of DBText)
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
            Dim entName As String
            Dim xPoints As New List(Of Double)
            Dim yPoints As New List(Of Double)
            '把表格线和文字内容分类存储于上面两个集合中
            
            For Each ent In tableObjects
                entName = ent.GetType().Name.ToLower
                If entName = "line" Or entName = "polyline" Then
                    Try
                        If (ent.StartPoint.X - ent.EndPoint.X) = 0 Then
                            xPoints.Add(ent.startpoint.x)
                        Else
                            yPoints.Add(ent.startpoint.y)
                        End If
                    Catch ex As System.Exception
                        ed.WriteMessage(ex.ToString)
                        Return
                    End Try

                ElseIf entName = "dbtext" Then
                    textContent.Add(ent)
                End If
            Next

            ed.WriteMessage("共计横，竖线：" & yPoints.Count & "，" & xPoints.Count & "共计文字:" & textContent.Count & " ")
            
            ed.WriteMessage(" ")

            If yPoints.Count = 0 Then
                ed.WriteMessage("没有选中表格横线，请从右下角向左上角选取表格内容！")
            Else
                Dim tableContent(,) As String

                tableContent = ReorganizedTable(xPoints, yPoints, textContent)
                
                Dim myExcel As New Excel.Application
                Dim myWorkbook As Excel.Workbook = myExcel.Workbooks.Add
                Dim myWorksheet As Excel.Worksheet = myWorkbook.Sheets(1)
                Dim cellRB As Excel.Range
                Dim rows As Integer
                Dim cols As Integer

                rows = tableContent.GetLength(0)
                cols = tableContent.GetLength(1)

                cellRB = myWorksheet.Cells(rows, cols)

                myWorksheet.Range("A1", cellRB).Value = tableContent
                myExcel.Visible = True

            End If





        End Sub

        Private Function ReorganizedTable(ByVal x As List(Of Double), ByVal y As List(Of Double), ByVal text As List(Of DBText)) As String(,)
            Dim tableGrid(y.Count + 2, x.Count + 2) As String
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
            x.Sort()
            y.Sort()
            ed.WriteMessage(" ")
            'For Each p In y
            '    ed.WriteMessage(p & ",")
            'Next
            Dim x1 As Double
            Dim y1 As Double
            Dim c As Integer '列号
            Dim r As Integer '行号
            
            For Each t In text
                x1 = t.Position.X
                y1 = t.Position.Y

                '确定列号
                If x1 < x(0) Then
                    c = 0
                    'ed.WriteMessage("最左列" & t.TextString)
                ElseIf x1 > x(x.Count - 1) Then
                    c = x.Count + 1
                    'ed.WriteMessage("最右列" & t.TextString)
                Else
                    For i = 0 To x.Count - 2
                        If (x1 > x(i)) And (x1 < x(i + 1)) Then
                            c = i + 1
                        End If
                    Next
                End If

                '确定行号
                If y1 < y(0) Then
                    r = y.Count + 1
                ElseIf y1 > y(y.Count - 1) Then
                    r = 0
                Else
                    For j = y.Count - 1 To 1 Step -1
                        If (y1 < y(j)) And (y1 > y(j - 1)) Then
                            r = y.Count - j
                        End If
                    Next
                End If
                If (c <> -1) And (r <> -1) Then
                    tableGrid(r, c) &= t.TextString
                Else
                    'ed.WriteMessage(" Y1=" & y1 & "," & "X1=" & x1)
                End If
                'ed.WriteMessage(" " & c & "," & r & " ")
                c = -1
                r = -1
                
            Next
            Return tableGrid
        End Function

        
    End Class
End Namespace