Public Class WebBrowser
    Public Name As BrowserName
    Public MajorVersion As Integer

    Public Sub New(ByVal title As String, _
                 ByVal MajorVersion As Integer)
        Me.Name = TranslateStringToBrowserName(title)
        Me.MajorVersion = MajorVersion
    End Sub

    Public Function TranslateStringToBrowserName(ByVal name) As BrowserName
        'TODO: Add more logic for properly sniffing for other browsers.
        If name.Equals("IE") Then
            Return BrowserName.InternetExplorer
        End If
        Return BrowserName.Unknown
    End Function

    Public Enum BrowserName
        Unknown
        InternetExplorer
        Firefox
        Chrome
        Opera
        Safari
        Dolphin
        Konqueror
        Linx
    End Enum
End Class
