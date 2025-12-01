<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PaxoStudio
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.New_project = New System.Windows.Forms.Button()
        Me.load_project = New System.Windows.Forms.Button()
        Me.quit_btn = New System.Windows.Forms.Button()
        Me.about_paxo_studio = New System.Windows.Forms.Button()
        Me.changelog = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 45.0!)
        Me.Label1.Location = New System.Drawing.Point(3, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(567, 95)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Paxo Studio"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(68, 158)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(437, 32)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Create An Application With easy !"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 394)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Version 1.0"
        '
        'New_project
        '
        Me.New_project.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.New_project.Location = New System.Drawing.Point(80, 222)
        Me.New_project.Name = "New_project"
        Me.New_project.Size = New System.Drawing.Size(206, 45)
        Me.New_project.TabIndex = 3
        Me.New_project.Text = "New Project"
        Me.New_project.UseVisualStyleBackColor = True
        '
        'load_project
        '
        Me.load_project.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.load_project.Location = New System.Drawing.Point(292, 222)
        Me.load_project.Name = "load_project"
        Me.load_project.Size = New System.Drawing.Size(213, 45)
        Me.load_project.TabIndex = 4
        Me.load_project.Text = "Load Project"
        Me.load_project.UseVisualStyleBackColor = True
        '
        'quit_btn
        '
        Me.quit_btn.BackColor = System.Drawing.Color.Red
        Me.quit_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.quit_btn.ForeColor = System.Drawing.Color.White
        Me.quit_btn.Location = New System.Drawing.Point(234, 325)
        Me.quit_btn.Name = "quit_btn"
        Me.quit_btn.Size = New System.Drawing.Size(270, 45)
        Me.quit_btn.TabIndex = 6
        Me.quit_btn.Text = "Quit"
        Me.quit_btn.UseVisualStyleBackColor = False
        '
        'about_paxo_studio
        '
        Me.about_paxo_studio.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.about_paxo_studio.Location = New System.Drawing.Point(234, 274)
        Me.about_paxo_studio.Name = "about_paxo_studio"
        Me.about_paxo_studio.Size = New System.Drawing.Size(126, 45)
        Me.about_paxo_studio.TabIndex = 7
        Me.about_paxo_studio.Text = "Github"
        Me.about_paxo_studio.UseVisualStyleBackColor = True
        '
        'changelog
        '
        Me.changelog.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.changelog.Location = New System.Drawing.Point(366, 274)
        Me.changelog.Name = "changelog"
        Me.changelog.Size = New System.Drawing.Size(139, 45)
        Me.changelog.TabIndex = 8
        Me.changelog.Text = "Changelog"
        Me.changelog.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(80, 275)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(148, 95)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "Check For Updates"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PaxoStudio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(582, 450)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.changelog)
        Me.Controls.Add(Me.about_paxo_studio)
        Me.Controls.Add(Me.quit_btn)
        Me.Controls.Add(Me.load_project)
        Me.Controls.Add(Me.New_project)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "PaxoStudio"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Welcome"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents New_project As Button
    Friend WithEvents load_project As Button
    Friend WithEvents quit_btn As Button
    Friend WithEvents about_paxo_studio As Button
    Friend WithEvents changelog As Button
    Friend WithEvents Button1 As Button
End Class
