Imports System.IO
Imports System.Text.RegularExpressions


Public Class Form1

    ' Beim Laden des Formulars ggf. initialisieren
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        ' Hier kannst du noch Initialisierungen vornehmen, falls nötig.
        LoadFilesFromDirectory("D:\Synactive\Work in Progress\Code Highlighting mit Prism\tutor_d")




    End Sub

    Private Sub btnSelectFolder_Click(sender As Object, e As EventArgs) Handles btnSelectFolder.Click

        Dim fbd As New FolderBrowserDialog
        If fbd.ShowDialog() = DialogResult.OK Then
            LoadFilesFromDirectory(fbd.SelectedPath)
        End If

    End Sub

    Private Sub LoadFilesFromDirectory(path As String)
        lbFiles.Items.Clear()
        Dim files = Directory.GetFiles(path, "*.html", SearchOption.AllDirectories)
        For Each file In files
            lbFiles.Items.Add(file)
        Next
    End Sub

    ' Wenn der Benutzer in der ListBox eine Datei auswählt, wird diese im WebView2 angezeigt.
    Private Sub lbFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbFiles.SelectedIndexChanged
        If lbFiles.SelectedItem IsNot Nothing Then
            Dim filePath As String = lbFiles.SelectedItem.ToString()
            WebView.Source = New Uri(filePath)

        End If
    End Sub

    ' Button zum Einfügen der Script Tags
    Private Sub btnInsertScripts_Click(sender As Object, e As EventArgs) Handles btnInsertScripts.Click
        If lbFiles.SelectedItem IsNot Nothing Then
            Dim filePath As String = lbFiles.SelectedItem.ToString()
            InsertScriptTags(filePath)
            webView.Refresh()  ' Vorschau aktualisieren
        End If
    End Sub

    Private Sub InsertScriptTags(filePath As String)

        Dim content As String = File.ReadAllText(filePath)
        ' Sicherung erstellen (.old)
        Dim backupPath As String = filePath & ".old"
        File.WriteAllText(backupPath, content)

        ' Suche nach dem <body>-Tag und füge direkt dahinter die Script- und Link-Tags ein.
        Dim pattern As String = "(<body[^>]*>)"
        Dim replacement As String = "$1" & vbCrLf &
            "<script type='text/javascript' src=""../js/prism.js""></script>" & vbCrLf &
            "<script type='text/javascript' src=""../js/prism_guixt.js""></script>" & vbCrLf &
            "<link rel=""stylesheet"" href=""../css/prism.css"">" & vbCrLf &
            "<link rel=""stylesheet"" href=""../css/prism_guixt.css"">" & vbCrLf
        Dim newContent As String = Regex.Replace(content, pattern, replacement, RegexOptions.IgnoreCase)
        File.WriteAllText(filePath, newContent)
    End Sub



End Class
