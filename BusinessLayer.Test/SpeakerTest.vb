Imports System.Text
Imports BusinessLayer
Imports BusinessLayer.Speaker
Imports DataAccessLayer
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class SpeakerTest
    Private repository As SqlServerCompactRepository = New SqlServerCompactRepository()


    <TestMethod()>
    <ExpectedException(GetType(System.Exception), "_")>
    Public Sub Register_EmptyFirstName_ThrowsArgumentNullException()

        'arrange
        Dim Speaker As Speaker = GetSpeakerThatWouldBeApproved()
        Speaker.FirstName = ""

        'act
        Speaker.Register(repository)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(System.Exception), "_")>
    Public Sub Register_EmptyLastName_ThrowsArgumentNullException()

        'arrange
        Dim Speaker As Speaker = GetSpeakerThatWouldBeApproved()
        Speaker.LastName = ""

        'act
        Speaker.Register(repository)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(System.Exception), "_")>
    Public Sub Register_EmptyEmail_ThrowsArgumentNullException()

        'arrange
        Dim Speaker As Speaker = GetSpeakerThatWouldBeApproved()
        Speaker.Email = ""

        'act
        Speaker.Register(repository)
    End Sub

    <TestMethod()>
    Public Sub Register_WorksForPrestigiousEmployerButHasRedFlags_ReturnsSpeakerId()
        'arrange
        Dim Speaker As Speaker = GetSpeakerWithRedFlags()
        Speaker.Employer = "Microsoft"

        'act
        Dim speakerId = Speaker.Register(repository)

        'assert
        Assert.IsNotNull(speakerId)
    End Sub

    <TestMethod()>
    Public Sub Register_HasBlogButHasRedFlags_ReturnsSpeakerId()

        'arrange
        Dim Speaker As Speaker = GetSpeakerWithRedFlags()

        'act
        Dim speakerId = Speaker.Register(repository)

        'assert
        Assert.IsFalse(String.IsNullOrEmpty(speakerId))
    End Sub

    <TestMethod()>
    Public Sub Register_HasCertificationsButHasRedFlags_ReturnsSpeakerId()

        'arrange
        Dim Speaker As Speaker = GetSpeakerWithRedFlags()
        Speaker.Certifications = New List(Of String) From {"cert1", "cert2", "cert3", "cert4"}


        'act
        Dim speakerId = Speaker.Register(New SqlServerCompactRepository())

        'assert
        Assert.IsFalse(String.IsNullOrEmpty(speakerId))
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(NoSessionsApprovedException), "_")>
    Public Sub Register_SingleSessionThatsOnOldTech_ThrowsNoSessionsApprovedException()

        'arrange
        Dim Speaker As Speaker = GetSpeakerThatWouldBeApproved()
        Speaker.Sessions = New List(Of Session) From {
                                                        New Session("Cobol for dummies", "Intro to Cobol")
                                                     }
        'act
        Speaker.Register(repository)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(System.Exception), "_")>
    Public Sub Register_NoSessionsPassed_ThrowsArgumentException()

        'arrange
        Dim Speaker As Speaker = GetSpeakerThatWouldBeApproved()
        Speaker.Sessions = New List(Of Session)

        'act
        Speaker.Register(repository)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(SpeakerDoesntMeetRequirementsException), "_")>
    Public Sub Register_DoesntAppearExceptionalAndUsingOldBrowser_ThrowsNoSessionsApprovedException()

        'arrange
        Dim speakerThatDoesntAppearExceptional As Speaker = GetSpeakerThatWouldBeApproved()
        speakerThatDoesntAppearExceptional.HasBlog = False
        speakerThatDoesntAppearExceptional.Browser = New WebBrowser("IE", 6)

        'act
        speakerThatDoesntAppearExceptional.Register(repository)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(SpeakerDoesntMeetRequirementsException), "_")>
    Public Sub Register_DoesntAppearExceptionalAndHasAncientEmail_ThrowsNoSessionsApprovedException()

        'arrange
        Dim speakerThatDoesntAppearExceptional As Speaker = GetSpeakerThatWouldBeApproved()
        speakerThatDoesntAppearExceptional.HasBlog = False
        speakerThatDoesntAppearExceptional.Email = "name@aol.com"

        'act
        speakerThatDoesntAppearExceptional.Register(repository)
    End Sub

    Function GetSpeakerThatWouldBeApproved() As Speaker
        Return New Speaker("First",
                           "Last",
                           "example@domain.com",
                           "Example Employer",
                           True,
                           New WebBrowser("test", 1),
                           1,
                           New List(Of String),
                           "",
                           New List(Of Session) From {
                                                        New Session("test title", "test description")
                                                     }
                           )
    End Function


    Function GetSpeakerWithRedFlags() As Speaker
        Dim speaker As Speaker = GetSpeakerThatWouldBeApproved()
        speaker.Email = "tom@aol.com"
        speaker.Browser = New WebBrowser("IE", 6)
        Return speaker
    End Function

End Class