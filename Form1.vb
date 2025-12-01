Imports System.IO

Public Class PaxoStudio
    Dim full_path
    Dim version As String = "beta-0.1"
    Dim name_app = "PxApp Maker"

    Private Sub quit_btn_Click(sender As Object, e As EventArgs) Handles quit_btn.Click
        Application.Exit()
    End Sub

    Private Sub New_project_Click(sender As Object, e As EventArgs) Handles New_project.Click
        PaxoStudioNewProject.full_path = full_path
        PaxoStudioNewProject.version = version
        PaxoStudioNewProject.ShowDialog()
    End Sub

    Private Sub Gaxo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Tag = name_app
        Label1.Text = name_app
        Dim documentsPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        full_path = Path.Combine(documentsPath, name_app & " Projects")

        If Not Directory.Exists(full_path) Then
            Directory.CreateDirectory(full_path)
        End If
        Label3.Text = "Version " & version
    End Sub

    Private Sub load_project_Click(sender As Object, e As EventArgs) Handles load_project.Click
        Using folderDialog As New FolderBrowserDialog()
            folderDialog.Description = "Select a project folder"
            folderDialog.SelectedPath = full_path
            folderDialog.ShowNewFolderButton = False

            If folderDialog.ShowDialog() = DialogResult.OK Then
                Dim selectedPath As String = folderDialog.SelectedPath

                PaxoStudioWorkspace.path_ = selectedPath
                PaxoStudioWorkspace.version = version
                PaxoStudioWorkspace.Show()
                Me.Hide()

            End If
        End Using
    End Sub

    Private Sub about_paxo_studio_Click(sender As Object, e As EventArgs) Handles about_paxo_studio.Click
        Process.Start("https://github.com/DJOPRO-YT/PxAPP-Maker")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox("This feature is not available now but you can check the latest version of " & name_app & " on the Github")
        Process.Start("https://github.com/DJOPRO-YT/PxAPP-Maker/releases")
    End Sub

    Private Sub changelog_Click(sender As Object, e As EventArgs) Handles changelog.Click
        MsgBox("This feature is not available now but you can check the changelog of " & name_app & " on the Github")
        Process.Start("https://github.com/DJOPRO-YT/PxAPP-Maker")
    End Sub
End Class
