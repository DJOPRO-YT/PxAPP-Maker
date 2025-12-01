Imports System.IO

Public Class EmulatorLoader
    Public Property path_ As String
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()
        For Each emulator In Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Emulator"))
            ComboBox1.Items.Add(Path.GetFileName(emulator))
        Next
        ProgressBar1.Value = 0
        Label1.Text = "To run your Paxo app, Please choose an emulator to go with:"
        ComboBox1.Text = ""
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Label1.Text = "Please wait ...."
        ProgressBar1.Value = 5
        For Each emulator In Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Emulator"))
            If Path.GetFileName(emulator) = ComboBox1.Text Then
                Directory.CreateDirectory(Path.Combine(emulator, "storage", "apps", Path.GetFileName(path_)))
                Dim folderapp = Path.Combine(emulator, "storage", "apps", Path.GetFileName(path_))
                Label1.Text = "Installing the app ..."
                ProgressBar1.Value = 20
                For Each fileapp In Directory.GetFiles(Path.Combine(path_, "Bin"))
                    If Path.GetFileName(fileapp) = "Code.lua" Then
                        File.Copy(fileapp, Path.Combine(folderapp, "app.lua"), True)
                    Else
                        File.Copy(fileapp, Path.Combine(folderapp, Path.GetFileName(fileapp)), True)
                    End If

                Next
                Label1.Text = "App Installed, Launching the Emulator ...."
                ProgressBar1.Value = 90

                Dim psi As New ProcessStartInfo()
                psi.FileName = Path.Combine(emulator, "program.exe")
                psi.WorkingDirectory = emulator
                Process.Start(psi)

                Me.Close()
            End If
        Next
    End Sub

End Class