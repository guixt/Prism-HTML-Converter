Imports System.IO
Imports System.Reflection.Emit
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports HtmlAgilityPack
Imports Microsoft.Web.WebView2.WinForms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Web.WebView2.Core


Public Class Form1

    ' Beim Laden des Formulars ggf. initialisieren
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        ' Hier kannst du noch Initialisierungen vornehmen, falls nötig.
        basepath.Text = My.Settings.LastText

        LogMessage("Herzlich Willkommen!")

        cbCodeType.Items.AddRange(New String() {"ABAP", "HTML", "JavaScript", "GuiXT", "VB.NET"})
        If cbCodeType.Items.Count > 0 Then cbCodeType.SelectedIndex = 3



    End Sub


    ' Beim Schließen des Formulars
    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        My.Settings.LastText = basepath.Text
        My.Settings.Save()
    End Sub
    Private Sub btnSelectFolder_Click(sender As Object, e As EventArgs) Handles btnSelectFolder.Click

        Dim fbd As New FolderBrowserDialog()

        If fbd.ShowDialog() = DialogResult.OK Then
            basepath.Text = fbd.SelectedPath
        End If

    End Sub

    Private Async Sub LoadFilesFromDirectoryAsync(path As String)
        If Directory.Exists(path) Then
            lbFiles.Items.Clear()
            ProgressBar1.Style = ProgressBarStyle.Marquee
            ProgressBar1.Visible = True

            ' Dateisuche asynchron ausführen:
            Dim files As List(Of String) = Await Task.Run(Function()
                                                              Return Directory.EnumerateFiles(path, "*.html", SearchOption.AllDirectories).ToList()
                                                          End Function)

            ' Füge die Dateien in kleinen Chargen zur ListBox hinzu, um die UI nicht zu blockieren:
            Dim count As Integer = 0
            For Each file As String In files
                lbFiles.Items.Add(file)
                count += 1
                ' Kurze Pause alle 50 Dateien, um der UI Zeit zu geben, sich zu aktualisieren:
                If count Mod 50 = 0 Then
                    Application.DoEvents()

                End If
            Next

            ProgressBar1.Visible = False
        End If
    End Sub

    ' Wenn der Benutzer in der ListBox eine Datei auswählt, wird diese im WebView2 angezeigt.
    Private Sub lbFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbFiles.SelectedIndexChanged
        If lbFiles.SelectedItem IsNot Nothing Then
            Dim filePath As String = lbFiles.SelectedItem.ToString()
            WebView.Source = New Uri(filePath)
            WebView_2.Source = New Uri(filePath)


        End If
    End Sub

    ' Button zum Einfügen der Script Tags
    Private Sub btnInsertScripts_Click(sender As Object, e As EventArgs) Handles btnInsertScripts.Click
        If lbFiles.SelectedItem IsNot Nothing Then
            Dim filePath As String = lbFiles.SelectedItem.ToString()
            InsertScriptTags(filePath)

            WebView_2.Reload()
            WebView_2.Refresh()  ' Vorschau aktualisieren

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
                WebView_2.Reload()
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


        WebView_2.Reload()
        WebView_2.Refresh()
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

    Private Sub basepath_TextChanged(sender As Object, e As EventArgs) Handles basepath.TextChanged

        LoadFilesFromDirectoryAsync(basepath.Text)

    End Sub

    Private Sub WebView_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles WebView.NavigationCompleted



    End Sub

    Private Sub vbopen_Click(sender As Object, e As EventArgs) Handles vbopen.Click


        Try
            ' Pfad zur VS Code ausführbaren Datei (falls nicht in PATH-Umgebung)
            Dim vsCodePath As String = "C:\Users\Pascal\AppData\Local\Programs\Microsoft VS Code\Code.exe"

            ' Falls VS Code im System-PATH ist, reicht "code" als Befehl
            If Not System.IO.File.Exists(vsCodePath) Then
                vsCodePath = "code" ' Falls VS Code im PATH ist
            End If

            Dim filepath = lbFiles.SelectedItem.ToString()


            ' Prüfen, ob die Datei existiert
            If System.IO.File.Exists(filepath) Then
                ' Startet VS Code mit der Datei
                Process.Start(vsCodePath, """" & filepath & """")
            Else
                MessageBox.Show("Datei nicht gefunden: " & filepath, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Fehler beim Öffnen der Datei in VS Code: " & ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Async Sub repace_simple_Click(sender As Object, e As EventArgs) Handles repace_simple.Click


        Dim filepath = lbFiles.SelectedItem.ToString()
        Dim content As String = File.ReadAllText(filepath)

        LogMessage("Sicherung erstellen (.old)")

        Dim backupPath As String = filepath & ".old"
        File.WriteAllText(backupPath, content)



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

        ' Ersetzungsblock definieren
        Dim newBlock As String = "<div name=""prism_placeholder""></div>"

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
        File.WriteAllText(lbFiles.SelectedItem.ToString(), modifiedHtml)
        LogMessage("DOM-Knoten wurde direkt per JS ersetzt und die Datei aktualisiert.")


        WebView_2.Reload()
        WebView_2.Refresh()
    End Sub

    Private Sub replace_placeholder_Click(sender As Object, e As EventArgs) Handles replace_placeholder.Click

        Dim filepath = lbFiles.SelectedItem.ToString()

        ' 1. Lese den HTML-Inhalt der Datei ein
        Dim content As String = File.ReadAllText(filepath)

        LogMessage("Sicherung erstellen (.old)")

        Dim backupPath As String = filepath & ".old"
        File.WriteAllText(backupPath, content)

        ' 2. Prüfe, ob der Platzhalter vorhanden ist
        Dim placeholder As String = "<div name=""prism_placeholder""></div>"
        If Not content.Contains(placeholder) Then
            LogMessage("Kein Platzhalter gefunden.")
            Return
        End If

        ' 3. Benutzer-Code aus der Textbox abrufen
        Dim userCode As String = txt_code_to_insert.Text.Trim()
        If String.IsNullOrEmpty(userCode) Then
            LogMessage("Kein Code zum Einfügen eingegeben.")
            Return
        End If

        ' 4. PrismJS-Code generieren (basierend auf ausgewählter Sprache)
        Dim language As String = cbCodeType.SelectedItem.ToString()
        Dim prismBlock As String = GetPrismFormattedHtml(language, userCode, "Kopieren")

        ' 5. Ersetze den Platzhalter durch den PrismJS-Code
        content = content.Replace(placeholder, prismBlock)

        ' 6. Speichere die geänderte Datei
        File.WriteAllText(filePath, content)
        LogMessage("Platzhalter erfolgreich durch Prism-Code ersetzt.")

        ' 7. Vorschau aktualisieren
        WebView_2.Reload()
        WebView_2.Refresh()
    End Sub

End Class


