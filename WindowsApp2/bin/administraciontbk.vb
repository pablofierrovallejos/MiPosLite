Imports Transbank.POSAutoservicio
Imports Transbank.POSIntegrado
Imports Transbank.Exceptions.CommonExceptions
Imports Transbank.Exceptions.IntegradoExceptions
Imports Transbank.Responses.CommonResponses
Imports Transbank.Responses
Imports Transbank.Responses.AutoservicioResponse.LastSaleResponse
'Imports Transbank.Responses.AutoservicioResponse
Imports Transbank.Responses.IntegradoResponses
Imports System.Threading

Public Class administraciontbk
    Dim posTimeout As String = Integer.Parse(readConfig("POS_TIMEOUT_MS"))
    Private Sub cmdPolling_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdPolling.Click
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            Dim miPos = POSIntegrado.Instance

            If Not iscomopen Then
                miPos.OpenPort(portName)
                iscomopen = True
            End If

            Dim pollResult As Task(Of Boolean) = Task.Run(Async Function() Await POSIntegrado.Instance.Poll())
            Dim sout = pollResult.Wait(posTimeout)

            If sout Then  'If pollResult.Result Then
                MsgBox("Polling OK", vbInformation, "miAutoPOS")
            Else
                MsgBox("Error en Comunicación con POS,no se pudo realizar Poll ", vbInformation, "miAutoPOS")
            End If
        Catch a As InvalidOperationException
            MessageBox.Show(a.Message & " Problema de conexión con POS", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Catch a As TransbankException
            MessageBox.Show(a.Message & " Problema de conexión con POS", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Catch a As Exception
            Console.WriteLine([String].Concat(a.StackTrace, a.Message))
            If a.InnerException Is Nothing Then
                Console.WriteLine("Inner Exception")
                Console.WriteLine([String].Concat(a.InnerException.StackTrace, a.InnerException.Message))
                MessageBox.Show(a.Message & " Problema de conexión con POS", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Else
                MessageBox.Show(a.InnerException.Message & " Problema de conexión con POS", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            End If
        End Try
    End Sub

    Private Sub cmdManualTbk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdManualTbk.Click
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            POSIntegrado.Instance.OpenPort(portName)

            Dim dialogResult As DialogResult = MessageBox.Show("La configuración en Modo Normal desconectará el POS" & vbLf & " Está Seguro?", "Set Modo Normal", MessageBoxButtons.YesNo)

            If dialogResult = DialogResult.Yes Then
                Dim result As Task(Of Boolean) = Task.Run(Async Function() Await POSIntegrado.Instance.SetNormalMode())
                Dim sout = result.Wait(posTimeout)

                If sout Then  'If result.Result Then
                    MsgBox("Cambio Modo Normal OK", vbInformation, "miAutoPOS")
                Else
                    MsgBox("Error en Comunicación, no se pudo cambiar POS a modo Normal", vbInformation, "miAutoPOS")
                End If
            End If
        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub


    Private Sub cmdUltmaVenta_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUltmaVenta.Click
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            POSIntegrado.Instance.OpenPort(portName)

            Dim response As Task(Of LastSaleResponse) = Task.Run(Async Function() Await POSIntegrado.Instance.LastSale())
            Dim sout = response.Wait(posTimeout)

            If sout Then
                If response.Result.Success Then
                    MessageBox.Show(response.Result.ToString(), "Impresión de última venta OK")
                End If
            Else
                MessageBox.Show("Falló la conexión con POS.")
            End If
        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub



    Private Sub cmdDetalleVentas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDetalleVentas.Click
        Dim countListSales = getListDetailSales()
        If countListSales = 0 Then
            MsgBox("No Existen Ventas", vbInformation, "miAutoPOS")
        Else
            MsgBox("Detalle de Ventas OK", vbInformation, "miAutoPOS")
        End If
    End Sub

    Public Function getListDetailSales() As Integer
        Dim totalDimList As Integer = 0
        Try
            TextBox1.Text = ""
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            POSIntegrado.Instance.OpenPort(portName)
            Dim details As Task(Of List(Of DetailResponse)) = Task.Run(Async Function() Await POSIntegrado.Instance.Details(False))
            lblEstado.Text = If(details.Status = 1, "WaitingForActivation", "")

            Dim resp = details.Wait(posTimeout)
            Dim stmp As String = ""
            TextBox1.Text = "Detalle de Ventas" & vbNewLine

            totalDimList = details.Result.Count

            If resp Then
                For Each detail As DetailResponse In details.Result
                    'stmp = detail.CardType
                    TextBox1.Text = TextBox1.Text & "Tipo Tarjeta : " & detail.CardType
                    TextBox1.Text = TextBox1.Text & "     Total : " & detail.Amount
                    TextBox1.Text = TextBox1.Text & "     Ultimos 4Digitos : " & detail.Last4Digits
                    TextBox1.Text = TextBox1.Text & "     Fecha : " & detail.RealDate & vbNewLine
                Next
                ' If details.resResponseCode = 3 Then
                ' MsgBox("Reintente -Conexión falló", vbInformation, "miAutoPOS")
                ' End If
                'If details.Result.Count = 0 Then
                'MsgBox("No Existen Ventas", vbInformation, "miAutoPOS")
                ' Else
                'MsgBox("Detalle de Ventas OK", vbInformation, "miAutoPOS")
                'End If
            Else
                lblEstado.Text = ""
                MsgBox("Error en Comunicación, no se pudo obtener detalle de Ventas o no hay Ventas", vbInformation, "miAutoPOS")
                totalDimList = -1
            End If
            lblEstado.Text = ""
            POSIntegrado.Instance.ClosePort()

        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            totalDimList = -1
        Catch a As Exception
            Console.WriteLine([String].Concat(a.StackTrace, a.Message))
            If a.InnerException Is Nothing Then
                Console.WriteLine("Inner Exception")
                Console.WriteLine([String].Concat(a.InnerException.StackTrace, a.InnerException.Message))
                MessageBox.Show(a.Message & " Problema de conexión con POS", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Else
                MessageBox.Show(a.InnerException.Message & " Problema de conexión con POS", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            End If
            totalDimList = -1
        End Try
        Return totalDimList
    End Function

    Private Sub cmdPrintVentasPos_Click(sender As Object, e As EventArgs) Handles cmdPrintVentasPos.Click
        Try
            Dim countListSales = getListDetailSales()
            If countListSales = 0 Then
                MsgBox("No Existen Ventas", vbInformation, "miAutoPOS")
            ElseIf countListSales > 0 Then


                Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
                Thread.Sleep(3000)

                POSIntegrado.Instance.OpenPort(portName)

                Dim details As Task(Of List(Of DetailResponse)) = Task.Run(Async Function() Await POSIntegrado.Instance.Details(True))

                lblEstado.Text = If(details.Status = 1, "WaitingForActivation", "")
                Dim resp = details.Wait(posTimeout)
                Dim stmp As String = ""
                lblEstado.Text = ""
                If resp Then
                    MsgBox("Detalle de Ventas OK", vbInformation, "miAutoPOS")
                Else
                    MsgBox("Error en Comunicación, no se pudo obtener detalle de Ventas", vbInformation, "miAutoPOS")
                End If

                POSIntegrado.Instance.ClosePort()
            End If
        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Catch a As Exception
            Console.WriteLine([String].Concat(a.StackTrace, a.Message))
            If a.InnerException Is Nothing Then
                Console.WriteLine("Inner Exception")
                Console.WriteLine([String].Concat(a.InnerException.StackTrace, a.InnerException.Message))
                MessageBox.Show(a.Message & " Problema de conexión con POS", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Else
                MessageBox.Show(a.InnerException.Message & " Problema de conexión con POS", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            End If
        End Try

    End Sub

    Private Sub cmdCierre_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCierre.Click
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            POSIntegrado.Instance.OpenPort(portName)

            Dim response As Task(Of CloseResponse) = Task.Run(Async Function() Await POSIntegrado.Instance.Close())
            lblEstado.Text = response.Status.ToString
            Dim resp = response.Wait(posTimeout)

            If resp Then
                If response.Result.Success Then
                    TextBox1.Text = response.Result.ToString().Replace(vbLf, vbNewLine)
                    MsgBox("Cierre OK", vbInformation, "miAutoPOS")
                End If
                If response.Result.ResponseCode = 3 Then
                    MsgBox("Reintente -Conexión falló", vbInformation, "miAutoPOS")
                End If
            Else
                MsgBox("Error en Comunicación, no se pudo realizar Cierre", vbInformation, "miAutoPOS")
            End If
            lblEstado.Text = ""
        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Public Function mitarea() As Boolean
        Thread.Sleep(100)
        Return True
    End Function


    Private Sub cmdCargaLlaves_Click(sender As Object, e As EventArgs) Handles cmdCargaLlaves.Click
        Try

            Dim tokenSource = New CancellationTokenSource()
            Dim token = tokenSource.Token



            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            Dim miPos = POSIntegrado.Instance '.OpenPort(portName) ' POSIntegrado.Instance.OpenPort(portName)
            miPos.OpenPort(portName)



            Dim response As Task(Of LoadKeysResponse) = Task.Run(Async Function()
                                                                     If token.IsCancellationRequested Then
                                                                         token.ThrowIfCancellationRequested()
                                                                     End If
                                                                     Return Await miPos.LoadKeys()
                                                                 End Function)
            'lblEstado.Text = "Solicitando llaves..."
            lblEstado.Text = response.Status.ToString

            Dim resp = response.Wait(posTimeout)


            If resp Then
                If response.Result.Success Then
                    MessageBox.Show(response.Result.ToString(), "Carga de llaves OK")
                End If
                If response.Result.ResponseCode = 3 Then
                    MsgBox("Reintente -Conexión falló", vbInformation, "miAutoPOS")
                End If
            Else
                MsgBox("Error en Comunicación Intente de Nuevo", vbInformation, "miAutoPOS")
            End If
            lblEstado.Text = ""

            tokenSource.Cancel()


            If miPos.IsPortOpen Then
                'POSIntegrado.Instance.ClosePort()
                'POSIntegrado.Instance.Close()
                ' miPos.ClosePort()
                Me.Close()
                Dim formtbk As New administraciontbk()
                formtbk.Show()
            End If

        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try

    End Sub

    Private Sub administraciontbk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iscomopen = False
    End Sub

    Private Sub cmdTotalVentas_Click(sender As Object, e As EventArgs) Handles cmdTotalVentas.Click
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            POSIntegrado.Instance.OpenPort(portName)

            Dim response As Task(Of TotalsResponse) = Task.Run(Async Function() Await POSIntegrado.Instance.Totals())
            Dim sout = response.Wait(posTimeout)

            If sout Then
                If response.Result.Success Then
                    TextBox1.Text = response.Result.ToString().Replace(vbLf, vbNewLine)
                    MsgBox("Total de Ventas OK", vbInformation, "miAutoPOS")

                End If
            Else
                MessageBox.Show("Falló la conexión con POS.")
            End If
        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub cmdAnular_Click(sender As Object, e As EventArgs) Handles cmdAnular.Click
        Dim frmAnular As New AnularVenta()
        frmAnular.Show()

    End Sub

    Private Sub cmdUltimaVentaMulticode_Click(sender As Object, e As EventArgs) Handles cmdUltimaVentaMulticode.Click
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            POSIntegrado.Instance.OpenPort(portName)

            Dim response As Task(Of MultiCodeLastSaleResponse) = Task.Run(Async Function() Await POSIntegrado.Instance.MultiCodeLastSale(True))
            Dim sout = response.Wait(posTimeout)

            If sout Then
                If response.Result.Success Then
                    Logger.i("cmdUltimaVentaMulticode_Click(): " + response.Result.ToString(), New StackFrame(True))

                    Dim lstOfStrings As List(Of String)

                    lstOfStrings = formatBoucher(response.Result.ToString())


                    MessageBox.Show(response.Result.ToString(), "Impresión de última venta OK")

                End If
            Else
                MessageBox.Show("Falló la conexión con POS.")
            End If
        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub


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

End Class