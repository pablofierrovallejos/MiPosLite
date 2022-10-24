'Imports Transbank.POSAutoservicio
Imports Transbank.Responses.CommonResponses


Imports Transbank.POSIntegrado
Imports Transbank.Exceptions.CommonExceptions
Imports Transbank.Exceptions.IntegradoExceptions
Imports Transbank.Responses.IntegradoResponses

'Imports Transbank.Responses.AutoservicioResponse.LastSaleResponse
'Imports Transbank.Responses.AutoservicioResponse



Public Class tarjeta
    Dim contador As Integer = 30
    Dim posTimeout As String = Integer.Parse(readConfig("POS_TIMEOUT_VENTA_MS"))
    Dim miResponseMsglocal As String
    Dim miResponseMsglocalLong As String
    Dim enespera As Boolean = False
    Private Sub tarjeta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Logger.i("tarjeta_Load: ", New StackFrame(True))
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

        If contador = 30 Then
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
            cleanScreen()
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
            Dim boucherTbk As String = readConfig("PRINT_TKT_TRANSBK")
            Dim imprimeBoucher As Boolean = False
            If boucherTbk = "SI" Then imprimeBoucher = True
            Dim imonto As Integer = Integer.Parse(smonto)
            Dim Task_resp As Task(Of SaleResponse)

            POSIntegrado.Instance.OpenPort(portName)

            If Not enespera Then
                Task_resp = Task.Run(Async Function() Await POSIntegrado.Instance.Sale(imonto, "Ticket", True))
                'Task_resp = Task.Run(Async Function() Await POSIntegrado.Instance.Sale(imonto, "Ticket", imprimeBoucher))
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
            Logger.i("generaVentaTransbk(): miResponseMsglocalLong " & miResponseMsglocalLong.Replace(vbLf, "|"), New StackFrame(True))
            POSIntegrado.Instance.ClosePort()
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

            Dim lstOfStrings As List(Of String)
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
            ipaso = 18
            Dim prFont As New Font("Consolas", 9, FontStyle.Regular)   ' La fuente a usar en cuerpo
            Dim prFontTit As New Font("Arial", 12, FontStyle.Regular) ' La fuente del titulo

            e.Graphics.DrawString(Date.Now.ToShortDateString.ToString & " " & Date.Now.ToShortTimeString.ToString, prFont, Brushes.Black, margenIzq, ilinea)   'imprimimos la fecha y hora
            ilinea = ilinea + ipaso
            e.Graphics.DrawString("    Heladería Serrano", prFontTit, Brushes.Black, margenIzq, ilinea)  'imprimimos el nombre del Local
            ilinea = ilinea + ipaso
            e.Graphics.DrawString("Ticket: " & Date.Now.ToShortDateString.ToString.Replace("-", "") & Date.Now.ToShortTimeString.ToString.Replace(":", "") & "-" & valorAleatorio, prFont, Brushes.Black, margenIzq, ilinea)  'Imprimir numero de ticket
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(" ", prFont, Brushes.Black, margenIzq, ilinea)
            sCompra = "Producto       Uni  Sub"  'imprimimos Detalle de la compra
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)

            For i As Integer = 0 To Module1.indexCompras - 1   ' 
                ilinea = ilinea + ipaso
                sCompra = tbHProd(i).Text.PadRight(mrelleno, " ") & tbHUnid(i).Text.PadRight(mrellenoc, " ") & " $" & FormatNumber(Integer.Parse(tbHSubT(i).Text.Replace(".", "")), 0)
                e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            Next

            ilinea = ilinea + ipaso
            e.Graphics.DrawString(" ", prFont, Brushes.Black, margenIzq, ilinea) ' hace un salto de linea

            ilinea = ilinea + ipaso
            sCompra = "Total: " & Module1.smontoVenta
            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)

            ilinea = ilinea + ipaso
            e.Graphics.DrawString("Gracias!", prFont, Brushes.Black, margenIzq, ilinea)



            'Imprime boucher ultima venta
            ipaso = 13
            Dim prFontTerm As New Font("Consolas", 6, FontStyle.Bold)
            lstOfStrings = getLastSale()
            If lstOfStrings IsNot Nothing Then
                If lstOfStrings.Count > 0 Then
                    For Each item In lstOfStrings
                        ilinea = ilinea + ipaso
                        e.Graphics.DrawString(item, prFontTerm, Brushes.Black, 0, ilinea)
                    Next
                End If
            End If


            e.HasMorePages = False
        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message, "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Function getLastSale()
        Dim lstOfStrings As List(Of String)
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            POSIntegrado.Instance.OpenPort(portName)

            Dim response As Task(Of MultiCodeLastSaleResponse) = Task.Run(Async Function() Await POSIntegrado.Instance.MultiCodeLastSale(True))
            Dim sout = response.Wait(posTimeout)

            If sout Then
                If response.Result.Success Then
                    Logger.i("cmdUltimaVentaMulticode_Click(): " + response.Result.ToString(), New StackFrame(True))

                    lstOfStrings = formatBoucher(response.Result.ToString())

                End If
            Else
                MessageBox.Show("Falló la conexión con POS.")
            End If
        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
        Return lstOfStrings
    End Function


    Public Function formatBoucher(sdata As String)
        Logger.i(sdata)
        Dim lstOfStrings As New List(Of String)
        Dim slinea As String = ""
        Dim posIni = InStr(sdata, "Voucher: TRANSBANK")
        If posIni = 0 Then
            Return lstOfStrings
        Else
            Dim labelInicial As String = "Voucher: TRANSBANK"
            sdata = sdata.Substring(posIni + labelInicial.Length)
            sdata = "                       " & sdata

            While sdata.Length > 39
                slinea = sdata.Substring(0, 40)
                sdata = sdata.Substring(40)
                lstOfStrings.Add(slinea)


                'Para cortar el mensaje e imprimir solo boucher cliente
                Dim posCorte = InStr(slinea, "TRANSBANK")
                If posCorte > 0 Then
                    sdata = ""
                Else
                    Logger.i("CLIENTE>>" + slinea)
                End If
                'Logger.i(">>" + slinea)

            End While
            Return lstOfStrings
        End If
    End Function
    Public Sub cleanScreen()
        For i As Integer = 0 To 11   ' Inicializa Vector columnas productos, unidades, valor y subtotal donde se muestran las compras seleccionadas por cliente
            frmMenu.indexCompras = 0
            frmMenu.lblnumTotal.Text = ""
            frmMenu.lblUnidades.Text = ""
            frmMenu.tbHProd(i).Text = ""
            frmMenu.tbHUnid(i).Text = ""
            frmMenu.tbHValor(i).Text = ""
            frmMenu.tbHSubT(i).Text = ""
        Next
    End Sub

End Class