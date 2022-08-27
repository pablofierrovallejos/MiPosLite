Imports System.IO

Public Class Logger
    Public Enum TipoMensaje
        DEBUG
        INFO
        WARNING
        LERROR
    End Enum

    Private Const TAM_FICHERO As Integer = 52428800
    Private Const EXTENSION As String = ".log"

    Private Shared eNivel As TipoMensaje = TipoMensaje.DEBUG

    Public Shared Property Nivel As TipoMensaje
        Get
            Return eNivel
        End Get
        Set(eActual As TipoMensaje)
            eNivel = eActual
        End Set
    End Property

    Private Shared Sub add(ByVal eTipo As TipoMensaje,
                           ByVal sMensaje As String,
                           Optional ByVal objTraza As StackFrame = Nothing)
        If eTipo >= eNivel Then
            Dim sDestino As String = getFileFecha()

            verificar(sDestino)

            Dim objFichero As New FileStream(sDestino, FileMode.Append,
                                             FileAccess.Write)

            If Not objFichero Is Nothing And objFichero.CanWrite Then
                Dim sDeb As String = String.Empty
                Dim objFecha As Date = Date.Now

                sDeb = objFecha.ToString() + " [" + eTipo.ToString + "] "
                sDeb &= getTraza(objTraza) & sMensaje

                Dim objStream As New StreamWriter(objFichero)
                objStream.WriteLine(sDeb)
                objStream.Close()
                objFichero.Close()
            End If
        End If
    End Sub

    Private Shared Function getFileFecha() As String
        Dim objFecha As Date = Date.Now
        Dim sCadena As String = String.Empty

        sCadena &= objFecha.Year
        If objFecha.Month < 10 Then
            sCadena &= "0"
        End If
        sCadena &= objFecha.Month
        If objFecha.Day < 10 Then
            sCadena &= "0"
        End If
        sCadena &= objFecha.Day
        sCadena &= EXTENSION

        Return sCadena
    End Function

    Private Shared Sub verificar(sFichero As String)
        Try
            If My.Computer.FileSystem.GetFileInfo(sFichero).Length >=
               TAM_FICHERO Then
                My.Computer.FileSystem.MoveFile(sFichero, sFichero & ".old")
            End If
        Catch ex As IOException
        End Try
    End Sub

    Public Shared Sub e(sMensaje As String, objExcepcion As Exception,
                        Optional ByVal objTraza As StackFrame = Nothing)
        add(TipoMensaje.LERROR, sMensaje & vbNewLine &
            vbTab & objExcepcion.Message & vbNewLine &
            vbTab & objExcepcion.ToString, objTraza)
    End Sub

    Public Shared Sub e(sMensaje As String,
                        Optional ByVal objTraza As StackFrame = Nothing)
        add(TipoMensaje.LERROR, sMensaje, objTraza)
    End Sub

    Public Shared Sub w(sMensaje As String,
                        Optional ByVal objTraza As StackFrame = Nothing)
        add(TipoMensaje.WARNING, sMensaje, objTraza)
    End Sub

    Public Shared Sub i(sMensaje As String,
                        Optional ByVal objTraza As StackFrame = Nothing)
        add(TipoMensaje.INFO, sMensaje, objTraza)
    End Sub

    Public Shared Sub d(sMensaje As String,
                        Optional ByVal objTraza As StackFrame = Nothing)
        add(TipoMensaje.DEBUG, sMensaje, objTraza)
    End Sub

    Public Shared Function getTraza(objTraza As StackFrame) As String
        Dim sTraza As String = String.Empty

        If Not objTraza Is Nothing Then
            Dim iPosicion As Integer = objTraza.GetFileName.LastIndexOf("\")

            If iPosicion < 0 Then
                sTraza = objTraza.GetFileName()
            Else
                sTraza = objTraza.GetFileName().Substring(iPosicion + 1)
            End If

            sTraza &= "::" & objTraza.GetFileLineNumber & " - "
        End If

        Return sTraza
    End Function
End Class