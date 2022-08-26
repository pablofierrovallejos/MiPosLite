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
    Dim miResponseMsglocalLong As String
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

            Module1.escribeArchivoVentas(miResponseMsglocal, miResponseMsglocalLong, bsRespuestaTktTransb)
            If bsRespuestaTktTransb Then
                Timer1.Enabled = False
                Module1.sRespuestaTktTransb = miResponseMsglocal
                Module1.bsRespuestaTktTransb = False
                If miResponseMsglocal.Trim = "Aprobado" Then
                    If readConfig("PRINT_TKT_INTERNO") = "SI" Then
                        PrintDocument1.Print()
                    End If
                End If
                Me.Close()
            End If
        End If
        If contador <= 0 Then
            Me.Close()
        End If
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

            ' If bresp Then
            miResponseMsglocal = Task_resp.Result.ResponseMessage
            miResponseMsglocalLong = Task_resp.Result.ToString
            POSAutoservicio.Instance.ClosePort()
            'End If
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

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            Dim margenIzq As Integer
            Dim ilinea As Integer
            Dim ipaso As Integer
            Dim sCompra As String
            Dim mrelleno As Integer
            Dim mrellenoc As Integer

            Dim numAleatorio As New Random()
            Dim valorAleatorio As Integer = numAleatorio.Next(100, 999) ' Numero aleatorio para el ticket


            mrelleno = 17  'Relleno largo  del nombre del producto
            mrellenoc = 2  'Relleno largo  de cantidad de productos
            margenIzq = 0
            ilinea = 2
            ipaso = 20
            ' La fuente a usar
            Dim prFont As New Font("Consolas", 9, FontStyle.Regular)

            ' La fuente del titulo
            Dim prFontTit As New Font("Arial", 14, FontStyle.Regular)

            'imprimimos la fecha y hora
            e.Graphics.DrawString(Date.Now.ToShortDateString.ToString & " " & Date.Now.ToShortTimeString.ToString, prFont, Brushes.Black, margenIzq, ilinea)

            'imprimimos el nombre del Local
            ilinea = ilinea + ipaso
            e.Graphics.DrawString("  Heladería Serrano", prFontTit, Brushes.Black, margenIzq, ilinea)

            'Imprimir numero de ticket
            ilinea = ilinea + ipaso
            e.Graphics.DrawString("Ticket: " & Date.Now.ToShortDateString.ToString.Replace("-", "") & Date.Now.ToShortTimeString.ToString.Replace(":", "") & valorAleatorio, prFont, Brushes.Black, margenIzq, ilinea)

            'Salto de linea
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(" ", prFont, Brushes.Black, margenIzq, ilinea)


            'imprimimos Detalle de la compra
            sCompra = "Producto       Uni  Sub"
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)

            For i As Integer = 0 To Module1.indexCompras - 1   ' 
                ilinea = ilinea + ipaso
                sCompra = tbHProd(i).Text.PadRight(mrelleno, " ") & tbHUnid(i).Text.PadRight(mrellenoc, " ") & " $" & FormatNumber(Integer.Parse(tbHSubT(i).Text.Replace(".", "")), 0)
                e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            Next

            'Imprime el total
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(" ", prFont, Brushes.Black, margenIzq, ilinea) ' hace un salto de linea

            ilinea = ilinea + ipaso
            sCompra = "Total: " & Module1.smontoVenta
            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)



            ' ilinea = ilinea + ipaso
            ' e.Graphics.DrawString(". ", prFont, Brushes.Black, margenIzq, ilinea)
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(":) ", prFont, Brushes.Black, margenIzq, ilinea)

            e.HasMorePages = False

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message, "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



End Class