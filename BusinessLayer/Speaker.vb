'''' <summary>
'''' Represents a single speaker
'''' </summary>
Public Class Speaker
    Public FirstName As String
    Public LastName As String
    Public Email As String
    Public Exp As Integer
    Public HasBlog As Boolean
    Public BlogURL As String
    Public Browser As WebBrowser
    Public Certifications As List(Of String)
    Public Employer As String
    Public RegistrationFee As Integer
    Public Sessions As List(Of Session)


    '/// <summary>
    '/// Register a speaker
    '/// </summary>
    '/// <returns>speakerID</returns>
    Function Register(repository As IRepository) As Integer
        'lets init some vars
        Dim speakerId As Integer
        Dim good As Boolean = False
        Dim appr As Boolean = False
        'Dim nt() As String = {"MVC4", "Node.js", "CouchDB", "KendoUI", "Dapper", "Angular"}
        Dim ot() As String = {"Cobol", "Punch Cards", "Commodore", "VBScript"}

        'DEFECT #5274 DA 12/10/2012
        'We weren't filtering out the prodigy domain so I added it.
        Dim domains() As String = {"aol.com", "hotmail.com", "prodigy.com", "CompuServe.com"}

        If Not String.IsNullOrEmpty(FirstName) Then
            If Not String.IsNullOrEmpty(LastName) Then
                If Not String.IsNullOrEmpty(Email) Then
                    Dim emps() As String = {"Microsoft", "Google", "Fog Creek Software", "37Signals"}

                    'DFCT #838 Jimmy 
                    'We're now requiring 3 certifications so I changed the hard coded number. Boy, programming is hard.
                    good = ((Exp > 10 Or HasBlog Or Certifications.Count() > 3 Or emps.Contains(Employer)))
                    If Not good Then
                        'need to get just the domain from the email
                        Dim splitArray() = Strings.Split(Email, "@")
                        Dim emailDomain = splitArray(1)

                        If (Not domains.Contains(emailDomain) And (Not (Browser.Name = WebBrowser.BrowserName.InternetExplorer And Browser.MajorVersion < 9))) Then
                            good = True
                        End If
                    End If

                    If good Then
                        'DEFECT #5013 CO 1/12/2012
                        'We weren't requiring at least one session
                        If Sessions.Count <> 0 Then
                            For Each session As Session In Sessions
                                'For Each tech as Integer in nt
                                '    if session.Title.Contains(tech) then
                                '        session.Approved = true
                                '        Exit For
                                '    End If
                                'Next

                                For Each tech As String In ot
                                    If session.title.Contains(tech) Or session.Description.Contains(tech) Then
                                        session.Approved = False
                                        Exit For
                                    Else
                                        session.Approved = True
                                        appr = True
                                    End If
                                Next
                            Next
                        Else
                            Throw New Exception("Can't register speaker with no sessions to present.")
                        End If

                        If appr Then
                            'if we got this far, the speaker Is approved
                            'let's go ahead and register him/her now.
                            'First, let's calculate the registration fee. 
                            'More experienced speakers pay a lower fee.

                            If (Exp <= 1) Then
                                RegistrationFee = 500
                            ElseIf (Exp >= 2 And Exp <= 3) Then
                                RegistrationFee = 250
                            ElseIf (Exp >= 4 And Exp <= 5) Then
                                RegistrationFee = 100
                            ElseIf (Exp >= 6 And Exp <= 9) Then
                                RegistrationFee = 50
                            Else
                                RegistrationFee = 0
                            End If

                            'Now, save the speaker and sessions to the db.
                            Try
                                speakerId = repository.SaveSpeaker(Me)
                            Catch ex As Exception
                                'in case the db call fails 
                            End Try
                        Else
                            Throw New NoSessionsApprovedException("No sessions approved.")
                        End If
                    Else
                        Throw New SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our abitrary and capricious standards.")
                    End If

                Else
                    Throw New Exception("Email is required")
                End If
            Else
                Throw New Exception("Last Name is required")
            End If
        Else
            Throw New Exception("First Name is required")
        End If

        Return speakerId
    End Function

    Public Class SpeakerDoesntMeetRequirementsException
        Inherits Exception
        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal format As String, ByVal param As List(Of String))
            MyBase.New(String.Format(format, param))
        End Sub
    End Class

    Public Class NoSessionsApprovedException
        Inherits Exception
        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub
    End Class

    Public Sub New(ByVal FirstName As String)
        Me.FirstName = FirstName
    End Sub

    Public Sub New(ByVal FirstName As String,
                 ByVal LastName As String,
                 ByVal Email As String,
                 ByVal Employer As String,
                 ByVal HasBlog As Boolean,
                 ByVal Browser As WebBrowser,
                 ByVal Exp As Integer,
                 ByVal Certifications As List(Of String),
                 ByVal BlogURL As String,
                 ByVal Sessions As List(Of Session))
        Me.FirstName = FirstName
        Me.LastName = LastName
        Me.Email = Email
        Me.Exp = Exp
        Me.HasBlog = HasBlog
        Me.BlogURL = BlogURL
        Me.Browser = Browser
        Me.Certifications = Certifications
        Me.Employer = Employer
        Me.Sessions = Sessions
    End Sub

    Public Property _FirstName() As String
        Get
            Return Me.FirstName
        End Get
        Set(ByVal Value As String)
            Me.FirstName = Value
        End Set
    End Property

    Public Property _LastName() As String
        Get
            Return Me.LastName
        End Get
        Set(ByVal Value As String)
            Me.LastName = Value
        End Set
    End Property

    Public Property _Email() As String
        Get
            Return Me.Email
        End Get
        Set(ByVal Value As String)
            Me.Email = Value
        End Set
    End Property

    Public Property _Exp() As Integer
        Get
            Return Me.Exp
        End Get
        Set(ByVal Value As Integer)
            Me.Exp = Value
        End Set
    End Property

    Public Property _HasBlog() As Boolean
        Get
            Return Me.HasBlog
        End Get
        Set(ByVal Value As Boolean)
            Me.HasBlog = Value
        End Set
    End Property

    Public Property _BlogURL() As String
        Get
            Return Me.BlogURL
        End Get
        Set(ByVal Value As String)
            Me.BlogURL = Value
        End Set
    End Property

    Public Property _Browser() As WebBrowser
        Get
            Return Me.Browser
        End Get
        Set(ByVal Value As WebBrowser)
            Me.Browser = Value
        End Set
    End Property

    Public Property _Certifications() As List(Of String)
        Get
            Return Me.Certifications
        End Get
        Set(ByVal Value As List(Of String))
            Me.Certifications = Value
        End Set
    End Property

    Public Property _Employer() As String
        Get
            Return Me.Employer
        End Get
        Set(ByVal Value As String)
            Me.Employer = Value
        End Set
    End Property

    Public Property _RegistrationFee() As Integer
        Get
            Return Me.RegistrationFee
        End Get
        Set(ByVal Value As Integer)
            Me.RegistrationFee = Value
        End Set
    End Property

    Public Property _Sessions() As List(Of Session)
        Get
            Return Me.Sessions
        End Get
        Set(ByVal Value As List(Of Session))
            Me.Sessions = Value
        End Set
    End Property



End Class
