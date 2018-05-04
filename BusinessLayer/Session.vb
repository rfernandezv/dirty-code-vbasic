'''' <summary>
'''' Represents a single conference session
'''' </summary>
Public Class Session
    Public title As String
    Public Description As String
    Public Approved As Boolean

    Public Sub New(ByVal title As String,
                 ByVal Description As String)
        Me.title = title
        Me.Description = Description
    End Sub

    Public Property _Title() As String
        Get
            Return Me.title
        End Get
        Set(ByVal Value As String)
            Me.title = Value
        End Set
    End Property

    Public Property _Description() As String
        Get
            Return Me.Description
        End Get
        Set(ByVal Value As String)
            Me.Description = Value
        End Set
    End Property

    Public Property _Approved() As Boolean
        Get
            Return Me.Approved
        End Get
        Set(ByVal Value As Boolean)
            Me.Approved = Value
        End Set
    End Property
End Class
