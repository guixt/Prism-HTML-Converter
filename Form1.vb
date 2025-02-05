Imports System.IO
Imports System.Reflection.Emit
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports HtmlAgilityPack
Imports Microsoft.Web.WebView2.WinForms


Public Class Form1

    ' Beim Laden des Formulars ggf. initialisieren
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        ' Hier kannst du noch Initialisierungen vornehmen, falls nötig.
        LoadFilesFromDirectory("D:\Synactive\Work in Progress\Code Highlighting mit Prism\tutor_d")

        LogMessage("Herzlich Willkommen!")

        cbCodeType.Items.AddRange(New String() {"ABAP", "HTML", "JavaScript", "GuiXT", "VB.NET"})
        If cbCodeType.Items.Count > 0 Then cbCodeType.SelectedIndex = 3

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

            WebView.Reload()
            WebView.Refresh()  ' Vorschau aktualisieren
        End If
    End Sub


    Private Sub InsertScriptTags(filePath As String)

        Dim content As String = File.ReadAllText(filePath)

        LogMessage("Sicherung erstellen (.old)")

        Dim backupPath As String = filePath & ".old"
        File.WriteAllText(backupPath, content)


        LogMessage("Suche nach dem <body>-Tag und füge direkt dahinter die Script- und Link-Tags ein")

        Dim pattern As String = "(<body[^>]*>)"
        Dim replacement As String = "$1" & vbCrLf &
            "<script type='text/javascript' src=""../js/prism.js""></script>" & vbCrLf &
            "<script type='text/javascript' src=""../js/prism_guixt.js""></script>" & vbCrLf &
            "<link rel=""stylesheet"" href=""../css/prism.css"">" & vbCrLf &
            "<link rel=""stylesheet"" href=""../css/prism_guixt.css"">" & vbCrLf
        Dim newContent As String = Regex.Replace(content, pattern, replacement, RegexOptions.IgnoreCase)
        File.WriteAllText(filePath, newContent)
    End Sub

    Private Sub LogMessage(message As String)
        Dim timeStamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        rtbLog.AppendText($"[{timeStamp}] {message}" & Environment.NewLine)
        rtbLog.SelectionStart = rtbLog.TextLength
        rtbLog.ScrollToCaret()
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        If lbFiles.SelectedItem IsNot Nothing Then
            Dim filePath As String = lbFiles.SelectedItem.ToString()
            Dim backupPath As String = filePath & ".old"
            If File.Exists(backupPath) Then
                File.Copy(backupPath, filePath, True)
                LogMessage("Originaldatei aus Backup wiederhergestellt.")
                WebView.Reload()
            Else
                LogMessage("Backup-Datei nicht gefunden: " & backupPath)
            End If
        End If
    End Sub

    Private Function GetPrismFormattedHtml(language As String, code As String, Optional copyText As String = "Kopieren") As String
        Dim prefix As String = ""
        Dim suffix As String = ""
        Select Case language.ToLower()
            Case "guixt"
                prefix = "<span class=""language-title"">GuiXT</span>" &
                         $"<button class=""copy-button"" onclick=""copyToClipboardPrism(this)"">{copyText}</button>" & vbCrLf &
                         "<pre class=""language-guixt line-numbers""><code>"
                suffix = "</code></pre>"
            Case "abap"
                prefix = "<span class=""language-title"">ABAP</span>" &
                         $"<button class=""copy-button"" onclick=""copyToClipboardPrism(this)"">{copyText}</button>" & vbCrLf &
                         "<pre class=""language-abap line-numbers""><code>"
                suffix = "</code></pre>"
            Case "html"
                prefix = "<span class=""language-title"">HTML</span>" &
                         $"<button class=""copy-button"" onclick=""copyToClipboardPrism(this)"">{copyText}</button>" & vbCrLf &
                         "<script type=""text/plain"" class=""language-html line-numbers"">"
                suffix = "</script>"
            Case "javascript"
                prefix = "<span class=""language-title"">JavaScript</span>" &
                         $"<button class=""copy-button"" onclick=""copyToClipboardPrism(this)"">{copyText}</button>" & vbCrLf &
                         "<script type=""text/plain"" class=""language-javascript line-numbers"">"
                suffix = "</script>"
            Case "vb.net"
                prefix = "<span class=""language-title"">VB.NET</span>" &
                         $"<button class=""copy-button"" onclick=""copyToClipboardPrism(this)"">{copyText}</button>" & vbCrLf &
                         "<pre class=""language-vbnet line-numbers""><code>"
                suffix = "</code></pre>"
            Case Else
                prefix = "<pre><code>"
                suffix = "</code></pre>"
        End Select
        ' Den Code HTML-sicher kodieren
        Dim encodedCode As String = System.Net.WebUtility.HtmlEncode(code)
        Return prefix & encodedCode & suffix
    End Function


    Private Async Sub ReplaceSelectedCodeUnifiedAsync(filePath As String)
        ' 1. Ermittele den selektierten DOM-Knoten (outerHTML und Text) per JavaScript:
        Dim script As String = "
      (function() {
          var sel = window.getSelection();
          if (sel.rangeCount > 0) {
              var container = sel.getRangeAt(0).commonAncestorContainer;
              if (container.nodeType === 3) { 
                  container = container.parentNode; 
              }
              return JSON.stringify({ outer: container.outerHTML, text: sel.toString() });
          }
          return JSON.stringify({ outer: '', text: '' });
      })();"
        Dim resultJson As String = Await WebView.CoreWebView2.ExecuteScriptAsync(script)
        ' Zuerst den inneren JSON-String extrahieren, da WebView2-Ergebnis doppelt kodiert sein kann:
        Dim innerJson As String = JsonConvert.DeserializeObject(Of String)(resultJson)
        Dim selectionInfo As SelectionInfo = JsonConvert.DeserializeObject(Of SelectionInfo)(innerJson)

        If String.IsNullOrEmpty(selectionInfo.outer) OrElse String.IsNullOrEmpty(selectionInfo.text) Then
            LogMessage("Keine gültige Selektion gefunden.")
            Return
        End If

        ' 2. Erzeuge den neuen PrismJS-Block basierend auf der Selektion:
        Dim language As String = cbCodeType.SelectedItem.ToString()
        Dim newBlock As String = GetPrismFormattedHtml(language, selectionInfo.text, "Kopieren")

        ' 3. Serialisiere den neuen Block zur Übergabe in JavaScript:
        Dim newBlockJson As String = JsonConvert.SerializeObject(newBlock)

        ' 4. Führe JavaScript aus, das den selektierten DOM-Knoten durch den neuen Block ersetzt:
        Dim jsCode As String = "
      (function(newHtml) {
          var sel = window.getSelection();
          if (sel.rangeCount > 0) {
              var container = sel.getRangeAt(0).commonAncestorContainer;
              if (container.nodeType === 3) { 
                  container = container.parentNode; 
              }
              var fragment = document.createRange().createContextualFragment(newHtml);
              container.parentNode.replaceChild(fragment, container);
              return document.documentElement.outerHTML;
          }
          return '';
      })(" & newBlockJson & ");"

        Dim modifiedHtmlJson As String = Await WebView.CoreWebView2.ExecuteScriptAsync(jsCode)
        Dim modifiedHtml As String = JsonConvert.DeserializeObject(Of String)(modifiedHtmlJson)

        ' 5. Speichere das aktualisierte HTML in der Datei:
        File.WriteAllText(filePath, modifiedHtml)
        LogMessage("DOM-Knoten wurde direkt per JS ersetzt und die Datei aktualisiert.")


        WebView.Reload()
        WebView.Refresh()

    End Sub


    Private Function NormalizeText(text As String) As String
        ' Entfernt überflüssige Leerzeichen, Zeilenumbrüche, Tabs usw.
        Dim normalized As String = Regex.Replace(text, "\s+", " ")
        Return normalized.Trim().ToLower()
    End Function

    ' Hilfsklasse zum Deserialisieren des JSON-Ergebnisses aus dem JavaScript
    Private Class SelectionInfo
        Public Property outer As String
        Public Property text As String
    End Class

    Private Sub btnReplaceCode_Click(sender As Object, e As EventArgs) Handles btnReplaceCode.Click


        If lbFiles.SelectedItem IsNot Nothing Then
            Dim filePath As String = lbFiles.SelectedItem.ToString()

            Dim content As String = File.ReadAllText(filePath)

            LogMessage("Sicherung erstellen (.old)")

            Dim backupPath As String = filePath & ".old"
            File.WriteAllText(backupPath, content)

            LogMessage("Ersetze den selektierten Codeabschnitt")
            ReplaceSelectedCodeUnifiedAsync(filePath)

        End If

    End Sub
End Class


