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
        Me.txt_code_to_insert = New System.Windows.Forms.TextBox()
        Me.btnReplaceCode = New System.Windows.Forms.Button()
        Me.rtbLog = New System.Windows.Forms.RichTextBox()
        Me.WebView = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.btnRestore = New System.Windows.Forms.Button()
        Me.cbCodeType = New System.Windows.Forms.ComboBox()
        Me.basepath = New System.Windows.Forms.TextBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.WebView_2 = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.vbopen = New System.Windows.Forms.Button()
        Me.repace_simple = New System.Windows.Forms.Button()
        Me.replace_placeholder = New System.Windows.Forms.Button()
        CType(Me.WebView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WebView_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbFiles
        '
        Me.lbFiles.FormattingEnabled = True
        Me.lbFiles.Location = New System.Drawing.Point(12, 12)
        Me.lbFiles.Name = "lbFiles"
        Me.lbFiles.Size = New System.Drawing.Size(486, 407)
        Me.lbFiles.TabIndex = 0
        '
        'btnSelectFolder
        '
        Me.btnSelectFolder.Location = New System.Drawing.Point(12, 425)
        Me.btnSelectFolder.Name = "btnSelectFolder"
        Me.btnSelectFolder.Size = New System.Drawing.Size(129, 23)
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
        'txt_code_to_insert
        '
        Me.txt_code_to_insert.Location = New System.Drawing.Point(12, 483)
        Me.txt_code_to_insert.Multiline = True
        Me.txt_code_to_insert.Name = "txt_code_to_insert"
        Me.txt_code_to_insert.Size = New System.Drawing.Size(475, 252)
        Me.txt_code_to_insert.TabIndex = 4
        '
        'btnReplaceCode
        '
        Me.btnReplaceCode.Location = New System.Drawing.Point(385, 452)
        Me.btnReplaceCode.Name = "btnReplaceCode"
        Me.btnReplaceCode.Size = New System.Drawing.Size(102, 23)
        Me.btnReplaceCode.TabIndex = 5
        Me.btnReplaceCode.Text = "Replace code"
        Me.btnReplaceCode.UseVisualStyleBackColor = True
        '
        'rtbLog
        '
        Me.rtbLog.Font = New System.Drawing.Font("Rockwell", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbLog.Location = New System.Drawing.Point(499, 481)
        Me.rtbLog.Name = "rtbLog"
        Me.rtbLog.Size = New System.Drawing.Size(635, 157)
        Me.rtbLog.TabIndex = 6
        Me.rtbLog.Text = ""
        '
        'WebView
        '
        Me.WebView.AllowExternalDrop = True
        Me.WebView.CreationProperties = Nothing
        Me.WebView.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WebView.Location = New System.Drawing.Point(504, 12)
        Me.WebView.Name = "WebView"
        Me.WebView.Size = New System.Drawing.Size(630, 407)
        Me.WebView.TabIndex = 7
        Me.WebView.ZoomFactor = 1.0R
        '
        'btnRestore
        '
        Me.btnRestore.Location = New System.Drawing.Point(737, 428)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(132, 23)
        Me.btnRestore.TabIndex = 8
        Me.btnRestore.Text = "Backup wiederherstellen"
        Me.btnRestore.UseVisualStyleBackColor = True
        '
        'cbCodeType
        '
        Me.cbCodeType.FormattingEnabled = True
        Me.cbCodeType.Location = New System.Drawing.Point(493, 454)
        Me.cbCodeType.Name = "cbCodeType"
        Me.cbCodeType.Size = New System.Drawing.Size(185, 21)
        Me.cbCodeType.TabIndex = 9
        '
        'basepath
        '
        Me.basepath.Location = New System.Drawing.Point(148, 428)
        Me.basepath.Name = "basepath"
        Me.basepath.Size = New System.Drawing.Size(350, 20)
        Me.basepath.TabIndex = 10
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(504, 425)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(174, 23)
        Me.ProgressBar1.TabIndex = 11
        '
        'WebView_2
        '
        Me.WebView_2.AllowExternalDrop = True
        Me.WebView_2.CreationProperties = Nothing
        Me.WebView_2.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WebView_2.Location = New System.Drawing.Point(1140, 12)
        Me.WebView_2.Name = "WebView_2"
        Me.WebView_2.Size = New System.Drawing.Size(630, 407)
        Me.WebView_2.TabIndex = 12
        Me.WebView_2.ZoomFactor = 1.0R
        '
        'vbopen
        '
        Me.vbopen.Location = New System.Drawing.Point(120, 454)
        Me.vbopen.Name = "vbopen"
        Me.vbopen.Size = New System.Drawing.Size(108, 23)
        Me.vbopen.TabIndex = 13
        Me.vbopen.Text = "in VS Code öffnen"
        Me.vbopen.UseVisualStyleBackColor = True
        '
        'repace_simple
        '
        Me.repace_simple.Location = New System.Drawing.Point(504, 674)
        Me.repace_simple.Name = "repace_simple"
        Me.repace_simple.Size = New System.Drawing.Size(108, 23)
        Me.repace_simple.TabIndex = 14
        Me.repace_simple.Text = "Platzhalter einfügen"
        Me.repace_simple.UseVisualStyleBackColor = True
        '
        'replace_placeholder
        '
        Me.replace_placeholder.Location = New System.Drawing.Point(504, 703)
        Me.replace_placeholder.Name = "replace_placeholder"
        Me.replace_placeholder.Size = New System.Drawing.Size(108, 23)
        Me.replace_placeholder.TabIndex = 15
        Me.replace_placeholder.Text = "Platzhalter ersetzen"
        Me.replace_placeholder.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1784, 828)
        Me.Controls.Add(Me.replace_placeholder)
        Me.Controls.Add(Me.repace_simple)
        Me.Controls.Add(Me.vbopen)
        Me.Controls.Add(Me.WebView_2)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.basepath)
        Me.Controls.Add(Me.cbCodeType)
        Me.Controls.Add(Me.btnRestore)
        Me.Controls.Add(Me.WebView)
        Me.Controls.Add(Me.rtbLog)
        Me.Controls.Add(Me.btnReplaceCode)
        Me.Controls.Add(Me.txt_code_to_insert)
        Me.Controls.Add(Me.btnInsertScripts)
        Me.Controls.Add(Me.btnSelectFolder)
        Me.Controls.Add(Me.lbFiles)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.WebView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WebView_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbFiles As ListBox
    Friend WithEvents btnSelectFolder As Button
    Friend WithEvents btnInsertScripts As Button
    Friend WithEvents txt_code_to_insert As TextBox
    Friend WithEvents btnReplaceCode As Button
    Friend WithEvents rtbLog As RichTextBox
    Friend WithEvents WebView As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents btnRestore As Button
    Friend WithEvents cbCodeType As ComboBox
    Friend WithEvents basepath As TextBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents WebView_2 As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents vbopen As Button
    Friend WithEvents repace_simple As Button
    Friend WithEvents replace_placeholder As Button
End Class
