Imports System.IO
Imports System.Diagnostics
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json

Public Class PaxoStudioWorkspace
    Public Property path_ As String
    Public Property version As String
    Dim drawing As Boolean = False
    Dim drawing_started As Boolean = False
    Dim XB As Integer = 0
    Dim YB As Integer = 0
    Dim ItemsUI As New List(Of Object)
    Dim item_selected
    Dim random As New Random()
    Dim config_project

    Public Function GenerateRandomWord(length As Integer) As String
        Dim rand As New Random()
        Dim chars As String = "abcdefghijklmnopqrstuvwxyz"
        Dim result As Char() = New Char(length - 1) {}

        For i As Integer = 0 To length - 1
            result(i) = chars(rand.Next(chars.Length))
        Next

        Return New String(result)
    End Function

    Sub Unselect_Items_From_ListView(listview)
        For Each item As ListViewItem In listview.SelectedItems
            item.Selected = False
        Next
    End Sub

    Function Visual_GUI(name_obj, x, y, w, h, Optional DisableAppend = False, Optional Randomword = "untilted")
        Dim parent = Panel1
        Dim new_ = New Label()
        new_.Text = name_obj
        new_.TextAlign = ContentAlignment.MiddleCenter
        new_.AutoSize = False
        new_.BorderStyle = BorderStyle.FixedSingle
        new_.Width = w
        new_.BackColor = Color.White
        new_.Height = h
        new_.Location = New Point(x, y)
        new_.Tag = name_obj
        parent.Controls.Add(new_)

        If False Then ' Not DisableAppend Then
            File.AppendAllText(Path.Combine(path_, "Preview.pxstudio"), name_obj & " " & Randomword & " [] " & x & " " & y & " " & w & " " & h & Environment.NewLine)
        End If

        Return new_
    End Function

    Private Sub GaxoWorkspace_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            config_project = JObject.Parse(File.ReadAllText(Path.Combine(path_, "config.json")))
            ImageList1.Images.Clear()
            For Each img In Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Path.Combine("SDKs", Path.Combine(config_project("sdk"), "Assets"))))
                ImageList1.Images.Add(Path.GetFileName(img), Image.FromFile(img))
            Next
        Catch ex As Exception
            MessageBox.Show("Error: " & Environment.NewLine & "The config file config.json is incompatible or corrumpt, Please contact the developer for more.", PaxoStudio.Tag, MessageBoxButtons.OK, MessageBoxIcon.Error)
            PaxoStudio.Show()
            Exit Sub
        End Try

        Me.KeyPreview = True
        ToolStripLabel1.Text = PaxoStudio.Tag
        Me.Text = PaxoStudio.Tag & " | " & Path.GetFileName(path_)
        TreeView1.Nodes.Clear()
        Dim rootNode = TreeView1.Nodes.Add(Path.GetFileName(path_))
        rootNode.ImageKey = "folder.png"
        rootNode.SelectedImageKey = "folder.png"
        rootNode.Name = Path.GetFileName(path_)
        rootNode.Tag = path_
        rootNode.ContextMenuStrip = FolderList
        LoadDirectory(path_, rootNode)
        TreeView1.ExpandAll()
        ListView1.Clear()

        ' ToolKit Loading
        For Each tool In config_project("toolkit")
            Dim type = New ListViewGroup(tool)

            ListView1.Groups.Add(type)
            For Each file_ In Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SDKs", config_project("sdk"), "Toolkit", tool))

                Dim jsonText As String = File.ReadAllText(file_)
                Dim obj As JObject = JObject.Parse(jsonText)

                Dim item = New ListViewItem(Path.GetFileNameWithoutExtension(file_))
                item.Group = type
                item.ImageKey = obj("image").ToString()
                ListView1.Items.Add(item)
            Next
        Next
        Dim temptemp
        Try
            temptemp = JArray.Parse(File.ReadAllText(Path.Combine(path_, "Preview.pxstudio"))).ToObject(Of List(Of Object))()
        Catch ex As Exception
            MessageBox.Show("Error: " & Environment.NewLine & "The project file Preview.pxstudio is incompatible or corrumpt, Please contact the developer for more.", PaxoStudio.Tag, MessageBoxButtons.OK, MessageBoxIcon.Error)
            PaxoStudio.Show()
            Exit Sub
        End Try

        For Each item__ In temptemp
            Dim item = item__.ToObject(Of Dictionary(Of String, Object))
            Dim Name = item("name")
            Dim X = item("x")
            Dim Y = item("y")
            Dim W = item("w")
            Dim H = item("h")
            Dim random_ = item("name_oncode")
            Dim item_ = Visual_GUI(Name, X, Y, W, H)
            item("reference") = item_
            ItemsUI.Add(item)
            AddHandler CType(item_, Label).Click, AddressOf Item_Click

        Next
        'For Each line As String In File.ReadLines(Path.Combine(path_, "Preview.pxstudio")).ToArray()
        '    Dim Parts As String() = line.Split(" "c)
        '    Dim Name = Parts(0)
        '    Dim X = CInt(Parts(3))
        '    Dim Y = CInt(Parts(4))
        '    Dim W = CInt(Parts(5))
        '    Dim H = CInt(Parts(6))

        '    For Each folder In Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "item"))
        '        For Each file_ In Directory.GetFiles(folder)
        '            Dim jsonText As String = File.ReadAllText(file_)
        '            Dim obj As JObject = JObject.Parse(jsonText)
        '            If obj("name") = Name Then
        '                item_selected = obj.ToObject(Of Dictionary(Of String, Object))
        '                Exit For
        '            End If
        '        Next
        '    Next
        '    Dim random_ = GenerateRandomWord(10)
        '    Dim item = Visual_GUI(Name, X, Y, W, H, DisableAppend:=True, Randomword:=random_)
        '    item_selected("reference") = item
        '    item_selected("name_oncode") = random_
        '    ItemsUI.Add(item_selected)
        '    AddHandler CType(item, Label).Click, AddressOf Item_Click
        'Next
    End Sub

    Private Sub LoadDirectory(dirPath As String, parentNode As TreeNode)
        Try
            ' Add subfolders
            For Each folder In Directory.GetDirectories(dirPath)
                Dim folderNode = parentNode.Nodes.Add(Path.GetFileName(folder))
                folderNode.Name = Path.GetFileName(folder)
                folderNode.ImageKey = "folder.png"
                folderNode.SelectedImageKey = "folder.png"
                folderNode.ContextMenuStrip = Me.FolderList
                folderNode.Tag = folder
                ' Recursive call to load this folder's contents
                LoadDirectory(folder, folderNode)
            Next

            ' Add files
            For Each file In Directory.GetFiles(dirPath)
                Dim fileNode = parentNode.Nodes.Add(Path.GetFileName(file))
                fileNode.Name = Path.GetFileName(file)
                fileNode.ImageKey = "file.png"
                fileNode.SelectedImageKey = "file.png"
                fileNode.ContextMenuStrip = FileList
                fileNode.Tag = file
            Next
        Catch ex As UnauthorizedAccessException
            ' Skip folders where you don't have access permissions
        End Try
    End Sub

    Private Sub GaxoWorkspace_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        PaxoStudio.Close()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count > 0 Then
            For Each tool In config_project("toolkit")
                Dim type = New ListViewGroup(tool)

                ListView1.Groups.Add(type)
                For Each file_ In Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SDKs", config_project("sdk"), "Toolkit", tool))
                    If ListView1.SelectedItems(0).Text = Path.GetFileNameWithoutExtension(file_) Then
                        Dim jsonText As String = File.ReadAllText(file_)
                        Dim obj As JObject = JObject.Parse(jsonText)
                        item_selected = obj.ToObject(Of Dictionary(Of String, Object))
                        Panel1.Cursor = Cursors.Cross
                        drawing = True
                        drawing_started = False
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        If Not drawing_started And drawing Then
            If item_selected("need_size") Then
                drawing_started = True
                XB = e.X
                YB = e.Y
            Else
                drawing_started = True
                XB = e.X
                YB = e.Y

                TempLabel = New Label()
                TempLabel.BorderStyle = BorderStyle.FixedSingle
                TempLabel.AutoSize = False
                TempLabel.Location = New Point(XB, YB)
                TempLabel.Width = item_selected("w")
                TempLabel.Height = item_selected("h")
                Panel1.Controls.Add(TempLabel)
            End If
        End If
    End Sub

    Dim TempLabel As Label
    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        If drawing_started And drawing Then
            If Not IsNothing(TempLabel) Then
                Panel1.Controls.Remove(TempLabel)
            End If
            drawing_started = False
            drawing = False
            Panel1.Cursor = Cursors.Default

            Dim random_ = GenerateRandomWord(10)
            If item_selected("need_size") Then
                Dim item As Label = Visual_GUI(item_selected("name"), Math.Min(XB, e.X), Math.Min(YB, e.Y), Math.Abs(e.X - XB), Math.Abs(e.Y - YB), Randomword:=random_)
                Unselect_Items_From_ListView(ListView1)
                item_selected("reference") = item
                item_selected("name_oncode") = random_
                item_selected("x") = item.Location.X
                item_selected("y") = item.Location.Y
                item_selected("w") = item.Width
                item_selected("h") = item.Height
                ItemsUI.Add(item_selected)
                AddHandler CType(item, Label).Click, AddressOf Item_Click
            Else
                Dim item = Visual_GUI(item_selected("name"), Math.Min(XB, e.X), Math.Min(YB, e.Y), item_selected("w"), item_selected("h"), Randomword:=random_)
                Unselect_Items_From_ListView(ListView1)
                item_selected("reference") = item
                item_selected("name_oncode") = random_
                item_selected("x") = item.Location.X
                item_selected("y") = item.Location.Y
                ItemsUI.Add(item_selected)
                AddHandler CType(item, Label).Click, AddressOf Item_Click
            End If
        End If
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If drawing_started Then
            If Not IsNothing(TempLabel) Then
                Panel1.Controls.Remove(TempLabel)
            End If
            If item_selected("need_size") Then
                TempLabel = New Label()
                TempLabel.BorderStyle = BorderStyle.FixedSingle
                TempLabel.AutoSize = False
                TempLabel.Location = New Point(XB, YB)
                TempLabel.Width = e.X - XB
                TempLabel.Height = e.Y - YB
                Panel1.Controls.Add(TempLabel)
            Else
                XB = e.X
                YB = e.Y

                TempLabel = New Label()
                TempLabel.BorderStyle = BorderStyle.FixedSingle
                TempLabel.AutoSize = False
                TempLabel.Location = New Point(XB, YB)
                TempLabel.Width = item_selected("w")
                TempLabel.Height = item_selected("h")
                Panel1.Controls.Add(TempLabel)
            End If
        End If
    End Sub

    Sub AddPropertyTitleRow(table As TableLayoutPanel, propName As String, color As Color)
        table.RowCount += 1
        table.RowStyles.Insert(0, New RowStyle(SizeType.Percent, 5))

        For Each ctrl As Control In table.Controls
            Dim rowIndex As Integer = table.GetRow(ctrl)
            table.SetRow(ctrl, rowIndex + 1)
        Next

        Dim lblprop = New Label()
        Dim lblprop2 = New Label()
        lblprop.AutoSize = False
        lblprop2.AutoSize = False
        lblprop2.Dock = DockStyle.Fill
        lblprop.Dock = DockStyle.Fill
        lblprop.TextAlign = ContentAlignment.MiddleLeft
        lblprop.Text = propName
        lblprop.BackColor = color
        lblprop2.BackColor = color
        lblprop2.Text = ""
        table.Controls.Add(lblprop, 0, 0)
        table.Controls.Add(lblprop2, 1, 0)
        table.RowStyles(table.RowCount - 1).SizeType = SizeType.Percent
        table.RowStyles(table.RowCount - 1).Height = 100 - (table.RowCount - 1) * 5
    End Sub

    Sub AddPropertyRow(table As TableLayoutPanel, propName As String, obj As Object)
        table.RowCount += 1
        table.RowStyles.Insert(0, New RowStyle(SizeType.Percent, 5))

        For Each ctrl As Control In table.Controls
            Dim rowIndex As Integer = table.GetRow(ctrl)
            table.SetRow(ctrl, rowIndex + 1)
        Next

        Dim lblprop = New Label()
        lblprop.AutoSize = False
        'lblprop.BorderStyle = BorderStyle.FixedSingle
        lblprop.Dock = DockStyle.Fill
        lblprop.TextAlign = ContentAlignment.MiddleLeft
        lblprop.Text = propName
        table.Controls.Add(lblprop, 0, 0)
        table.Controls.Add(obj, 1, 0)
        table.RowStyles(table.RowCount - 1).SizeType = SizeType.Percent
        table.RowStyles(table.RowCount - 1).Height = 100 - (table.RowCount - 1) * 5
    End Sub

    Sub EmptyPropertyTable(table As TableLayoutPanel)
        table.Controls.Clear()
        table.RowStyles.Clear()
        table.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        table.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
    End Sub

    Private Sub Item_Click(sender As Object, e As EventArgs)
        Dim clickedItem As Label = CType(sender, Label)
        EmptyPropertyTable(Me.PropertiesTableLayout)
        Me.PropertiesTableLayout.RowCount = 1
        Me.PropertiesTableLayout.RowStyles.Insert(0, New RowStyle(SizeType.Percent, 100))

        For Each item In ItemsUI
            If item("reference") Is clickedItem Then
                For Each prop In item("properties")
                    Dim checkbox
                    If prop("value_type") = "bool" Then
                        checkbox = New CheckBox()
                        checkbox.Dock = Dock.Fill
                        checkbox.CheckAlign = ContentAlignment.MiddleCenter
                        checkbox.Text = ""
                        If prop("value") = "true" Then
                            checkbox.Checked = True
                        Else
                            checkbox.Checked = False
                        End If
                        AddHandler CType(checkbox, CheckBox).CheckedChanged, AddressOf PropertiesItems
                    ElseIf prop("value_type") = "int" Then
                        checkbox = New NumericUpDown()
                        checkbox.Dock = Dock.Fill
                        checkbox.Value = prop("value")

                        AddHandler CType(checkbox, NumericUpDown).ValueChanged, AddressOf PropertiesItems
                    Else
                        checkbox = New TextBox()
                        checkbox.Dock = Dock.Fill
                        checkbox.Text = prop("value")

                        AddHandler CType(checkbox, TextBox).TextChanged, AddressOf PropertiesItems
                    End If
                    checkbox.Tag = prop
                    AddPropertyRow(Me.PropertiesTableLayout, prop("name"), checkbox)
                Next
                AddPropertyTitleRow(Me.PropertiesTableLayout, "Properties", Color.LightGray)
                Dim deletebtn As New Button()
                deletebtn.Text = "Delete"
                deletebtn.Dock = DockStyle.Fill
                deletebtn.TextAlign = ContentAlignment.MiddleCenter
                deletebtn.BackColor = Color.Red()
                deletebtn.Tag = item
                AddPropertyRow(Me.PropertiesTableLayout, "Delete The " & item("name"), deletebtn)
                AddHandler CType(deletebtn, Button).Click, AddressOf DeleteBtn_Click

                If item("need_size") Then
                    Dim w As New NumericUpDown()
                    w.Dock = DockStyle.Fill
                    w.Minimum = 0
                    w.Maximum = 999
                    w.Value = clickedItem.Width
                    w.Tag = item
                    w.Name = random.Next(1, 10001).ToString() & "w"
                    AddPropertyRow(Me.PropertiesTableLayout, "Width", w)

                    Dim h As New NumericUpDown()
                    h.Dock = DockStyle.Fill
                    h.Minimum = 0
                    h.Maximum = 999
                    h.Value = clickedItem.Height
                    h.Tag = item
                    h.Name = random.Next(1, 10001).ToString() & "h"
                    AddPropertyRow(Me.PropertiesTableLayout, "Height", h)

                    AddHandler CType(h, NumericUpDown).ValueChanged, AddressOf NumericUpDownProperties
                    AddHandler CType(w, NumericUpDown).ValueChanged, AddressOf NumericUpDownProperties
                End If

                Dim x As New NumericUpDown()
                x.Dock = DockStyle.Fill
                x.Minimum = 0
                x.Maximum = 999
                x.Value = clickedItem.Location.X
                x.Tag = item
                x.Name = random.Next(1, 10001).ToString() & "x"
                AddPropertyRow(Me.PropertiesTableLayout, "X", x)

                Dim y As New NumericUpDown()
                y.Dock = DockStyle.Fill
                y.Minimum = 0
                y.Maximum = 999
                y.Value = clickedItem.Location.Y
                y.Name = random.Next(1, 10001).ToString() & "y"
                y.Tag = item
                AddPropertyRow(Me.PropertiesTableLayout, "Y", y)

                Dim name As New TextBox()
                name.Text = item("name_oncode")
                name.Dock = Dock.Fill
                name.Tag = item
                AddHandler CType(name, TextBox).TextChanged, AddressOf NameItems
                AddPropertyRow(Me.PropertiesTableLayout, "Name", name)

                AddHandler CType(x, NumericUpDown).ValueChanged, AddressOf NumericUpDownProperties
                AddHandler CType(y, NumericUpDown).ValueChanged, AddressOf NumericUpDownProperties

                AddPropertyTitleRow(Me.PropertiesTableLayout, "Basic", Color.LightGray)
            End If
        Next

        Me.PropertiesTitle.Text = "Properties [" & clickedItem.Text & "]"

    End Sub

    Private Sub NameItems(sender As Object, a As EventArgs)
        Dim main_item As TextBox = CType(sender, TextBox)
        Dim item = main_item.Tag
        For Each iteem_ In ItemsUI
            If iteem_ IsNot item Then
                If iteem_("name_oncode") = main_item.Text Then
                    MessageBox.Show("Error: " & Environment.NewLine & "The name is already used by another Object [" & iteem_("name") & "].", PaxoStudio.Tag, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    main_item.Text = item("name_oncode")
                    Exit Sub
                End If
            End If
        Next
        For Each character As Char In main_item.Text
            If Not (Char.IsLetterOrDigit(character) Or character = "_"c) Then
                MessageBox.Show("Error: " & Environment.NewLine & "The name has illegal characters", PaxoStudio.Tag, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                main_item.Text = item("name_oncode")
                Exit Sub
            End If
        Next
        item("name_oncode") = main_item.Text
    End Sub

    Private Sub PropertiesItems(sender As Object, a As EventArgs)
        Dim main_item As Object = CType(sender, Object)
        Dim item = main_item.Tag

        If item("value_type") = "bool" Then
            If main_item.Checked Then
                item("value") = "true"
            Else
                item("value") = "false"
            End If
        ElseIf item("value_type") = "int" Then
            item("value") = CInt(main_item.Value)
        Else
            item("value") = main_item.Text
        End If

    End Sub

    Private Sub NumericUpDownProperties(sender As Object, e As EventArgs)
        Dim NumberBox As NumericUpDown = CType(sender, NumericUpDown)
        Dim item = NumberBox.Tag

        If NumberBox.Name.Contains("x") Then
            item("reference").Location = New Point(NumberBox.Value, item("reference").Location.Y)
            item("x") = CInt(NumberBox.Value)
        ElseIf NumberBox.Name.Contains("y") Then
            item("reference").Location = New Point(item("reference").Location.X, NumberBox.Value)
            item("y") = CInt(NumberBox.Value)
        ElseIf NumberBox.Name.Contains("h") Then
            item("reference").Height = NumberBox.Value
            item("h") = CInt(NumberBox.Value)
        ElseIf NumberBox.Name.Contains("w") Then
            item("reference").Width = NumberBox.Value
            item("w") = CInt(NumberBox.Value)
        End If

    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs)
        Dim item As Object = CType(sender, Button).Tag

        For Each item_ In ItemsUI
            If item("reference") Is item_("reference") Then
                Panel1.Controls.Remove(item("reference"))
                EmptyPropertyTable(Me.PropertiesTableLayout)
                ItemsUI.Remove(item_)
                Me.PropertiesTitle.Text = "Properties"
                Exit For
            End If
        Next

        'Dim index = 0
        'Dim AllLines = File.ReadAllLines(Path.Combine(path_, "Preview.pxstudio"))
        'For Each line As String In AllLines
        '    Dim Parts As String() = line.Split(" "c)
        '    Dim Name = Parts(0)
        '    Dim X = CInt(Parts(3))
        '    Dim Y = CInt(Parts(4))
        '    Dim W = CInt(Parts(5))
        '    Dim H = CInt(Parts(6))

        '    If item("name") = Name Then
        '        ItemsUI.Remove(item)
        '        Panel1.Controls.Remove(item("reference"))
        '        EmptyPropertyTable(Me.PropertiesTableLayout)

        '        Dim tolist = AllLines.ToList()
        '        tolist.RemoveAt(index)
        '        File.WriteAllLines(Path.Combine(path_, "Preview.pxstudio"), tolist)
        '        Me.PropertiesTitle.Text = "Properties"
        '        Exit For
        '    End If
        '    index += 1
        'Next

    End Sub

    Dim NodeTreeView As TreeNode
    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        Clipboard.SetText(NodeTreeView.Tag)
    End Sub

    Private Sub OpenTheFolderOnExplorerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenTheFolderOnExplorerToolStripMenuItem.Click
        Process.Start("explorer.exe", NodeTreeView.Tag)
    End Sub

    Private Sub FolderList_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles FolderList.Opening
        Dim menu As ContextMenuStrip = DirectCast(sender, ContextMenuStrip)
        Dim tv As TreeView = TryCast(menu.SourceControl, TreeView)
        If tv IsNot Nothing Then
            NodeTreeView = tv.GetNodeAt(tv.PointToClient(Cursor.Position))
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Clipboard.SetText(NodeTreeView.Tag)
    End Sub

    Private Sub ToolStripMenuItem1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles FileList.Opening
        Dim menu As ContextMenuStrip = DirectCast(sender, ContextMenuStrip)
        Dim tv As TreeView = TryCast(menu.SourceControl, TreeView)
        If tv IsNot Nothing Then
            NodeTreeView = tv.GetNodeAt(tv.PointToClient(Cursor.Position))
        End If
    End Sub

    Private Sub OpenFileWithNotepadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileWithNotepadToolStripMenuItem.Click
        Process.Start("notepad.exe", NodeTreeView.Tag)
    End Sub

    Private Sub PaxoStudioWorkspace_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Not drawing_started And drawing Then
            If e.KeyCode = Keys.Escape Then
                Me.ListView1.SelectedItems.Clear()
                Panel1.Cursor = Cursors.Default
                item_selected = Nothing
                drawing = False
            End If
        ElseIf drawing_started And drawing Then
            If e.KeyCode = Keys.Escape Then
                Me.ListView1.SelectedItems.Clear()
                Panel1.Cursor = Cursors.Default
                item_selected = Nothing
                If TempLabel IsNot Nothing Then
                    Panel1.Controls.Remove(TempLabel)
                End If
                drawing_started = False
                drawing = False
            End If
        End If
    End Sub

    Sub SaveDesignToJSON(path)

        For Each tt In ItemsUI
            If tt("reference") IsNot Nothing Then
                Panel1.Controls.Remove(tt("reference"))
                tt.Remove("reference")
            End If

            tt("x") = CInt(tt("x"))
            tt("y") = CInt(tt("y"))
            tt("w") = CInt(tt("w"))
            tt("h") = CInt(tt("h"))
        Next

        Dim final_code = JsonConvert.SerializeObject(ItemsUI, Formatting.Indented)
        File.WriteAllText(path, final_code)

        For Each tt In ItemsUI
            Dim tttt = Visual_GUI(tt("name"), CInt(tt("x")), CInt(tt("y")), CInt(tt("w")), CInt(tt("h")))
            tt("reference") = tttt
            AddHandler CType(tttt, Label).Click, AddressOf Item_Click
        Next
    End Sub

    Private Sub ExportDesignToluaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportDesignToluaToolStripMenuItem.Click
        Try
            SaveDesignToJSON(Path.Combine(path_, "Preview.pxstudio"))
            Dim maincode As New List(Of String)
            maincode.Add("win = gui:window()")

            For Each item In ItemsUI
                If item("name_oncode") = "" Then
                    Throw New Exception("The name of each item must not be empty.")
                End If
                Dim lbl As Label = item("reference")
                Dim line As String = item("code")
                line = line.Replace("[[name_oncode]]", item("name_oncode"))
                line = line.Replace("[[x]]", lbl.Location.X.ToString())
                line = line.Replace("[[y]]", lbl.Location.Y.ToString())
                If item("need_size") Then
                    line = line.Replace("[[w]]", lbl.Width.ToString())
                    line = line.Replace("[[h]]", lbl.Height.ToString())
                End If

                Dim proplinestemp As New List(Of String)

                For Each propertie In item("properties")
                    Dim name_prop As String = propertie("name").ToString()
                    Dim value_prop_type As String = propertie("value_type").ToString()
                    Dim value_prop As String = propertie("value").ToString()

                    If value_prop_type = "str" Then
                        value_prop = """" & value_prop & """"
                    ElseIf value_prop_type = "bool" Then
                        value_prop = value_prop.ToLower()
                        If value_prop IsNot "true" Or value_prop IsNot "false" Then
                            value_prop = "false"
                        End If
                    ElseIf value_prop_type = "int" Then
                        If Not Char.IsDigit(value_prop) Then
                            value_prop = "0"
                        End If
                    End If

                    If propertie("code") IsNot "" Then
                        Dim lineprop As String = propertie("code")
                        lineprop = lineprop.Replace("[[current]]", value_prop)
                        lineprop = lineprop.Replace("[[name_oncode]]", item("name_oncode"))
                        lineprop = lineprop.Replace("[[" & name_prop & "]]", value_prop)
                        proplinestemp.Add(lineprop)
                    End If

                    line = line.Replace("[[" & name_prop & "]]", value_prop)
                Next
                maincode.Add(line)
                For Each lll In proplinestemp
                    maincode.Add(lll)
                Next
            Next

            maincode.Add("gui:setWindow(win)")
            File.WriteAllLines(Path.Combine(path_, Path.Combine("Bin", "Design.lua")), maincode)
            MsgBox("The Design was exported successfully!" & Environment.NewLine & "Location: Bin/Design.lua")
        Catch ex As Exception
            MessageBox.Show("Error While Exporting the Project: " & Environment.NewLine & ex.Message, PaxoStudio.Tag, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveTheProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveTheProjectToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        SaveDesignToJSON(Path.Combine(path_, "Preview.pxstudio"))
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        EmulatorLoader.path_ = path_
        EmulatorLoader.ShowDialog()
    End Sub

End Class
