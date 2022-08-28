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
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            POSIntegrado.Instance.OpenPort(portName)


            Dim details As Task(Of List(Of DetailResponse)) = Task.Run(Async Function() Await POSIntegrado.Instance.Details(False))
            Dim resp = details.Wait(posTimeout)
            Dim stmp As String = ""

            If resp Then
                For Each detail As DetailResponse In details.Result
                    'stmp = detail.CardType
                    stmp = "Tipo de Tarjeta : " & detail.CardType

                    stmp = stmp & "     Total : " & detail.Amount

                    stmp = stmp & "     Ultimos 4Digitos : " & detail.Last4Digits

                    stmp = stmp & "     Fecha : " & detail.RealDate & vbNewLine


                    TextBox1.Text = TextBox1.Text & stmp
                Next
            Else
                MsgBox("Error en Comunicación, no se pudo obtener detalle de Ventas", vbInformation, "miAutoPOS")
            End If
        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub cmdPrintVentasPos_Click(sender As Object, e As EventArgs) Handles cmdPrintVentasPos.Click
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            POSIntegrado.Instance.OpenPort(portName)

            TextBox1.Text = ""
            Dim details As Task(Of List(Of DetailResponse)) = Task.Run(Async Function() Await POSIntegrado.Instance.Details(True))
            Dim resp = details.Wait(posTimeout)
            Dim stmp As String = ""

            If resp Then
                For Each detail As DetailResponse In details.Result

                    stmp = "Tipo de Tarjeta : " & detail.CardType

                    stmp = stmp & "     Total : " & detail.Amount

                    stmp = stmp & "     Ultimos 4Digitos : " & detail.Last4Digits

                    stmp = stmp & "     Fecha : " & detail.RealDate & vbNewLine

                    TextBox1.Text = TextBox1.Text & stmp
                Next
            Else
                MsgBox("Error en Comunicación, no se pudo obtener detalle de Ventas", vbInformation, "miAutoPOS")
            End If
        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub cmdCierre_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCierre.Click
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            POSIntegrado.Instance.OpenPort(portName)

            Dim response As Task(Of CloseResponse) = Task.Run(Async Function() Await POSIntegrado.Instance.Close())
            Dim resp = response.Wait(posTimeout)

            If resp Then
                If response.Result.Success Then
                    MessageBox.Show(response.Result.ToString(), "Operación de Cierre.")
                End If
            Else
                MsgBox("Error en Comunicación, no se pudo realizar Cierre", vbInformation, "miAutoPOS")
            End If
            MessageBox.Show(response.Result.ToString(), "Operación de Cierre.")
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
            lblEstado.Text = "Solicitando llaves..."
            lblEstado.Text = response.Status.ToString

            Dim resp = response.Wait(posTimeout)


            If resp Then
                If response.Result.Success Then
                    MessageBox.Show(response.Result.ToString(), "Keys Cargada Satisfactoriamente.")
                End If
            Else
                MsgBox("Error en Comunicación, no se pudo cargar Keys", vbInformation, "miAutoPOS")
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
End Class