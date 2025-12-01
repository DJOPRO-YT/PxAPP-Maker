Imports System.IO

Public Class PaxoStudioNewProject
    Public Property full_path As String
    Public Property version As String
    Dim path_ As String
    Dim iconpath As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not ((ComboBox1.Text IsNot "") And (CheckedListBox2.CheckedItems.Count > 0) And (TextBox1.Text IsNot "") And (TextBox2.Text IsNot "") And (TextBox3.Text IsNot "")) Then
            MessageBox.Show("All the fields must not be blank (except the Icon), Please fill them and try again.", "Paxo Studio", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        path_ = Path.Combine(full_path, TextBox1.Text)
        Directory.CreateDirectory(path_)
        Dim listtoolkits = ""
        Dim temptimes = 1
        For Each tttt In CheckedListBox2.CheckedItems
            If CheckedListBox2.CheckedItems.Count < temptimes Then
                listtoolkits = listtoolkits & """" & tttt & """" & ","
            Else
                listtoolkits = listtoolkits & """" & tttt & """"
                Exit For
            End If
            temptimes += 1
        Next
        Dim TextToStore As String = $"{{""toolkit"":[{listtoolkits}],""os_version"":""{TextBox3.Text}"",""sdk"":""{ComboBox1.Text}""}}"
        File.WriteAllText(Path.Combine(path_, "config.json"), TextToStore)

        Directory.CreateDirectory(Path.Combine(path_, "Bin"))
        TextToStore = "{""access"": ["
        For Each item In CheckedListBox1.CheckedItems
            TextToStore = TextToStore + """" + item.ToString() + ""","
        Next

        If TextToStore.Substring(TextToStore.Length - 1, 1) = "," Then
            TextToStore = TextToStore.Substring(0, TextToStore.Length - 1)
        End If

        TextToStore = TextToStore + "],""name"":""" + TextBox2.Text + """,""os_version"":""" + TextBox3.Text + """}"
        File.WriteAllText(Path.Combine(path_, "Bin/manifest.json"), TextToStore)

        File.WriteAllText(Path.Combine(path_, "Bin/Design.lua"), "")
        File.WriteAllText(Path.Combine(path_, "Bin/Code.lua"), "require(""Design.lua"")" & Environment.NewLine & "function run(args)" & Environment.NewLine & "end")
        File.WriteAllText(Path.Combine(path_, "Preview.pxstudio"), "[]")
        File.Copy(iconpath, Path.Combine(path_, "Bin/icon.png"))

        PaxoStudioWorkspace.path_ = path_
        PaxoStudioWorkspace.Show()
        PaxoStudio.Hide()
        Me.Close()
    End Sub

    Private Sub PaxoStudioNewProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckedListBox2.Items.Clear() ' Toolkit
        ComboBox1.Items.Clear() ' SDK Version
        CheckedListBox1.Items.Clear() 'Perms
        Label1.Text = PaxoStudio.Tag
        For Each folder In Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SDKs"))
            ComboBox1.Items.Add(Path.GetFileName(folder))
        Next
        Button1.Enabled = False
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        CheckedListBox1.Items.Clear() 'Perms
        CheckedListBox2.Items.Clear() ' Toolkit
        Button1.Enabled = False
        For Each folder In Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SDKs"))
            If Path.GetFileName(folder) = ComboBox1.Text Then
                For Each folder2 In Directory.GetDirectories(Path.Combine(folder, "Toolkit"))
                    CheckedListBox2.Items.Add(Path.GetFileName(folder2))
                Next

                For Each perm In File.ReadAllLines(Path.Combine(folder, "perms"))
                    CheckedListBox1.Items.Add(perm)
                Next
                Button1.Enabled = True
                iconpath = Path.Combine(folder, "Assets", "default_icon_app.png")
                Exit Sub
            End If
        Next
    End Sub

    Private Sub iconfile_chooser_Click(sender As Object, e As EventArgs) Handles iconfile_chooser.Click
        MsgBox("NOTE: The recommended resolution for the icon of a Paxo application is 40x40 pixels")
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            iconpath = OpenFileDialog1.FileName
            iconfile_chooser.Text = iconpath
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        For i As Integer = 0 To CheckedListBox2.Items.Count - 1
            CheckedListBox2.SetItemChecked(i, CheckBox2.Checked)
        Next
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SetItemChecked(i, CheckBox1.Checked)
        Next
    End Sub
End Class