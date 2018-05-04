Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports BusinessLayer



'''<summary>
'''Se trata de una clase de prueba para SpeakerTest y se pretende que
'''contenga todas las pruebas unitarias SpeakerTest.
'''</summary>
<TestClass()> _
Public Class SpeakerTest


    Private testContextInstance As TestContext

    '''<summary>
    '''Obtiene o establece el contexto de la prueba que proporciona
    '''la información y funcionalidad para la ejecución de pruebas actual.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Atributos de prueba adicionales"
    '
    'Puede utilizar los siguientes atributos adicionales mientras escribe sus pruebas:
    '
    'Use ClassInitialize para ejecutar código antes de ejecutar la primera prueba en la clase 
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup para ejecutar código después de haber ejecutado todas las pruebas en una clase
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize para ejecutar código antes de ejecutar cada prueba
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup para ejecutar código después de que se hayan ejecutado todas las pruebas
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    '''<summary>
    '''Una prueba de Register
    '''</summary>
    <TestMethod()> _
    Public Sub Register_EmptyFirstName_ThrowsArgumentNullException()
        Dim target As Speaker = New Speaker() ' TODO: Inicializar en un valor adecuado
        Dim expected As Integer = 0 ' TODO: Inicializar en un valor adecuado
        Dim actual As Integer
        actual = target.Register
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Test method 1.")
    End Sub
End Class
