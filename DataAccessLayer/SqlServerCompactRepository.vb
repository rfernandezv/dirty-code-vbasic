Imports BusinessLayer

Public Class SqlServerCompactRepository
    Implements IRepository

    Function SaveSpeaker(ByVal speaker As Speaker) As Integer Implements IRepository.SaveSpeaker
        'TODO: Save speaker to DB for now. For demo, just assume success and return 1.
        Return 1
    End Function

End Class
