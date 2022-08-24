Module Module1
    Private mINI As New cIniArray

    Public miComm As String
    Public iscomopen As Boolean
    Public sRespuestaTktTransb As String = ""   ' La respuesta de transbank
    Public bsRespuestaTktTransb As Boolean = False
    Public smontoVenta As String = ""

    Public Function writeConfig(scampoClave As String, sValor As String) As Boolean
        Dim sFicINI As String = Application.StartupPath & "\configparams.ini"
        Dim sSeccion As String = "PARAMETROS"
        mINI.IniWrite(sFicINI, sSeccion, scampoClave, sValor)
        Return True
    End Function

    Public Function readConfig(scampoClave As String) As String
        Dim sFicINI As String = Application.StartupPath & "\configparams.ini"
        Dim sSeccion As String = "PARAMETROS"
        Dim sValor As String = ""
        Return mINI.IniGet(sFicINI, sSeccion, scampoClave, sValor)
    End Function

End Module
