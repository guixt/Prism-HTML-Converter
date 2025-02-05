<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lbFiles = New System.Windows.Forms.ListBox()
        Me.btnSelectFolder = New System.Windows.Forms.Button()
        Me.btnInsertScripts = New System.Windows.Forms.Button()
        Me.txtSelectedCode = New System.Windows.Forms.TextBox()
        Me.btnReplaceCode = New System.Windows.Forms.Button()
        Me.log = New System.Windows.Forms.RichTextBox()
        Me.WebView = New Microsoft.Web.WebView2.WinForms.WebView2()
        CType(Me.WebView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbFiles
        '
        Me.lbFiles.FormattingEnabled = True
        Me.lbFiles.Location = New System.Drawing.Point(12, 12)
        Me.lbFiles.Name = "lbFiles"
        Me.lbFiles.Size = New System.Drawing.Size(216, 407)
        Me.lbFiles.TabIndex = 0
        '
        'btnSelectFolder
        '
        Me.btnSelectFolder.Location = New System.Drawing.Point(12, 425)
        Me.btnSelectFolder.Name = "btnSelectFolder"
        Me.btnSelectFolder.Size = New System.Drawing.Size(216, 23)
        Me.btnSelectFolder.TabIndex = 1
        Me.btnSelectFolder.Text = "Verzeichnis wählen"
        Me.btnSelectFolder.UseVisualStyleBackColor = True
        '
        'btnInsertScripts
        '
        Me.btnInsertScripts.Location = New System.Drawing.Point(12, 454)
        Me.btnInsertScripts.Name = "btnInsertScripts"
        Me.btnInsertScripts.Size = New System.Drawing.Size(102, 23)
        Me.btnInsertScripts.TabIndex = 2
        Me.btnInsertScripts.Text = "Insert script tag"
        Me.btnInsertScripts.UseVisualStyleBackColor = True
        '
        'txtSelectedCode
        '
        Me.txtSelectedCode.Location = New System.Drawing.Point(12, 483)
        Me.txtSelectedCode.Multiline = True
        Me.txtSelectedCode.Name = "txtSelectedCode"
        Me.txtSelectedCode.Size = New System.Drawing.Size(216, 157)
        Me.txtSelectedCode.TabIndex = 4
        '
        'btnReplaceCode
        '
        Me.btnReplaceCode.Location = New System.Drawing.Point(120, 454)
        Me.btnReplaceCode.Name = "btnReplaceCode"
        Me.btnReplaceCode.Size = New System.Drawing.Size(102, 23)
        Me.btnReplaceCode.TabIndex = 5
        Me.btnReplaceCode.Text = "Replace code"
        Me.btnReplaceCode.UseVisualStyleBackColor = True
        '
        'log
        '
        Me.log.Location = New System.Drawing.Point(261, 521)
        Me.log.Name = "log"
        Me.log.Size = New System.Drawing.Size(899, 119)
        Me.log.TabIndex = 6
        Me.log.Text = ""
        '
        'WebView
        '
        Me.WebView.AllowExternalDrop = True
        Me.WebView.CreationProperties = Nothing
        Me.WebView.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WebView.Location = New System.Drawing.Point(249, 13)
        Me.WebView.Name = "WebView"
        Me.WebView.Size = New System.Drawing.Size(911, 502)
        Me.WebView.TabIndex = 7
        Me.WebView.ZoomFactor = 1.0R
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1172, 652)
        Me.Controls.Add(Me.WebView)
        Me.Controls.Add(Me.log)
        Me.Controls.Add(Me.btnReplaceCode)
        Me.Controls.Add(Me.txtSelectedCode)
        Me.Controls.Add(Me.btnInsertScripts)
        Me.Controls.Add(Me.btnSelectFolder)
        Me.Controls.Add(Me.lbFiles)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.WebView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbFiles As ListBox
    Friend WithEvents btnSelectFolder As Button
    Friend WithEvents btnInsertScripts As Button
    Friend WithEvents txtSelectedCode As TextBox
    Friend WithEvents btnReplaceCode As Button
    Friend WithEvents log As RichTextBox
    Friend WithEvents WebView As Microsoft.Web.WebView2.WinForms.WebView2
End Class
