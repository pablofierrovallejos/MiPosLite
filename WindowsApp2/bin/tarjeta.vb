Imports Transbank.POSAutoservicio
Imports Transbank.Responses.CommonResponses
Imports Transbank.Responses.IntegradoResponse

Imports Transbank.POSIntegrado
Imports Transbank.Exceptions.CommonExceptions
Imports Transbank.Exceptions.IntegradoExceptions
Imports Transbank.Responses
Imports Transbank.Responses.AutoservicioResponse.LastSaleResponse
Imports Transbank.Responses.AutoservicioResponse


Public Class tarjeta
    Dim contador As Integer = 60
    Dim posTimeout As String = Integer.Parse(readConfig("POS_TIMEOUT_VENTA_MS"))
    Dim miResponseMsglocal As String
    Dim enespera As Boolean = False
    Private Sub tarjeta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Timer1.Interval = 1000
        Timer1.Enabled = True
    End Sub

    Async Function decrementa() As Task(Of Boolean)
        While contador >= 0
            Await Task.Delay(1000)
            Label1.Text = contador
            contador -= 1
        End While
        Return True
    End Function
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = contador

        If contador = 60 Then
            bsRespuestaTktTransb = generaVentaTransbk(Module1.smontoVenta)

            Module1.escribeArchivoVentas(miResponseMsglocal, bsRespuestaTktTransb)
            If bsRespuestaTktTransb Then
                Timer1.Enabled = False
                Module1.sRespuestaTktTransb = miResponseMsglocal
                Module1.bsRespuestaTktTransb = False
                Me.Close()
            End If
        End If
        If contador <= 0 Then
            Me.Close()
        End If
        'decrementa()
    End Sub
    Private Sub wait(ByVal interval As Integer)
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            Application.DoEvents()  ' Allows UI to remain responsive
            enespera = True
        Loop
        sw.Stop()
    End Sub
    Public Function generaVentaTransbk(smonto As String) As Boolean
        smonto = smonto.Replace(".", "")
        Dim bresp = False
        Dim miResponseMsg As String = ""
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            Dim imonto As Integer = Integer.Parse(smonto)

            POSAutoservicio.Instance.OpenPort(portName)
            Dim Task_resp As Task(Of SaleResponse)

            If Not enespera Then
                Task_resp = Task.Run(Async Function() Await POSAutoservicio.Instance.Sale(imonto, "Ticket", False, True))
            End If

            Do While (Not Task_resp.IsCompleted)
                contador -= 1
                Label1.Text = contador
                Me.Refresh()
                wait(1000)

            Loop
            enespera = False
            bresp = Task_resp.Wait(posTimeout)

            If bresp Then
                miResponseMsg = Task_resp.Result.ResponseMessage
                POSAutoservicio.Instance.ClosePort()
                miResponseMsglocal = miResponseMsg.ToString
            End If
        Catch ex As Exception
            Debug.Print(ex.StackTrace.ToString)
        End Try

        Return bresp
    End Function


    Private Function obtenerEstadoCaja() As Boolean
        Dim sEstadoCaja As String = Integer.Parse(readConfig("ESTADO_CAJA"))
        If sEstadoCaja.Trim = "ABIERTA" Then
            Return True
        Else
            Return False
        End If
    End Function
End Class