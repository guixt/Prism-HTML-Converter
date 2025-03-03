Imports System.IO
Imports System.Text

Public Module EncodingDetector

    Public Function DetectFileEncoding(filePath As String) As Encoding
        Dim defaultEncoding As Encoding = Encoding.Default ' Falls nichts erkannt wird → ANSI
        Dim buffer(3) As Byte

        Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
            If fs.Length >= 2 Then fs.Read(buffer, 0, 2)
            If fs.Length >= 3 Then fs.Read(buffer, 2, 1)
            If fs.Length >= 4 Then fs.Read(buffer, 3, 1)
        End Using

        ' Prüfen auf BOM (Byte Order Mark)
        If buffer(0) = &HFF AndAlso buffer(1) = &HFE Then
            Return Encoding.Unicode ' UTF-16 LE
        ElseIf buffer(0) = &HFE AndAlso buffer(1) = &HFF Then
            Return Encoding.BigEndianUnicode ' UTF-16 BE
        ElseIf buffer(0) = &HEF AndAlso buffer(1) = &HBB AndAlso buffer(2) = &HBF Then
            Return Encoding.UTF8 ' UTF-8 mit BOM
        ElseIf buffer(0) = 0 AndAlso buffer(1) = 0 AndAlso buffer(2) = &HFE AndAlso buffer(3) = &HFF Then
            Return Encoding.UTF32 ' UTF-32 BE
        ElseIf buffer(0) = &HFF AndAlso buffer(1) = &HFE AndAlso buffer(2) = 0 AndAlso buffer(3) = 0 Then
            Return Encoding.UTF32 ' UTF-32 LE
        End If

        ' Prüfen, ob Datei UTF-8 ist (ohne BOM), indem bestimmte Byte-Muster geprüft werden
        Dim utf8 As Boolean = True
        Dim bytes() As Byte = File.ReadAllBytes(filePath)

        Dim i As Integer = 0
        While i < bytes.Length
            If bytes(i) <= &H7F Then
                ' ASCII-Zeichen (0-127), alles gut
            ElseIf bytes(i) >= &HC2 AndAlso bytes(i) <= &HF4 Then
                ' Start eines UTF-8-Multibyte-Zeichens
                Dim remainingBytes As Integer = 0
                If bytes(i) >= &HC2 AndAlso bytes(i) <= &HDF Then
                    remainingBytes = 1
                ElseIf bytes(i) >= &HE0 AndAlso bytes(i) <= &HEF Then
                    remainingBytes = 2
                ElseIf bytes(i) >= &HF0 AndAlso bytes(i) <= &HF4 Then
                    remainingBytes = 3
                Else
                    utf8 = False
                    Exit While
                End If

                ' Prüfen, ob die nächsten Bytes dem UTF-8-Format entsprechen
                If i + remainingBytes >= bytes.Length Then
                    utf8 = False
                    Exit While
                End If

                For j As Integer = 1 To remainingBytes
                    If bytes(i + j) < &H80 OrElse bytes(i + j) > &HBF Then
                        utf8 = False
                        Exit While
                    End If
                Next

                i += remainingBytes
            Else
                utf8 = False
                Exit While
            End If
            i += 1
        End While

        If utf8 Then
            Return Encoding.UTF8 ' UTF-8 ohne BOM erkannt
        End If

        ' Standard-Fallback: ANSI (Windows-1252 oder Default-Encoding)
        Return defaultEncoding
    End Function

End Module
