Imports Transbank.POSAutoservicio
Imports Transbank.Responses.CommonResponses
Imports Transbank.Responses.IntegradoResponse
Imports Transbank.Responses.AutoservicioResponse.LastSaleResponse


Public Class administraciontbk
    Private Sub cmdPolling_Click(sender As Object, e As EventArgs) Handles cmdPolling.Click
        Dim msgNOK As String
        Dim msgOK As String
        msgNOK = "Problema de conexión con POS"
        msgOK = "Polling OK"

        Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
        POSAutoservicio.Instance.OpenPort(portName)

        'Dim Task_resp = POSAutoservicio.Instance.Poll()

        'Task<bool> pollResult = Task.Run(async () => { return await POSIntegrado.Instance.Poll(); });
        TestPoll()

        Try
            Dim booleanPoll = True ' Task_resp.Result.ToString
            If booleanPoll Then
                MsgBox(msgOK, MsgBoxStyle.Information, "ESTADO POLLING")
            Else
                MsgBox(msgOK, MsgBoxStyle.Information, "ESTADO POLLING")
            End If
        Catch ex As Exception
            Debug.Print(ex.StackTrace.ToString)
            MsgBox(msgNOK)
        End Try
        POSAutoservicio.Instance.ClosePort()
    End Sub


    Async Function TestPoll() As Task(Of Boolean)
        Dim getEstados As Task(Of Boolean) = POSAutoservicio.Instance.Poll()
        Dim resp As Boolean = Await getEstados
        Return resp
    End Function

    Private Sub cmdManualTbk_Click(sender As Object, e As EventArgs) Handles cmdManualTbk.Click
        Dim msgNOK As String
        Dim msgOK As String
        msgNOK = "Cambio Modo Normal Error!"
        msgOK = "Cambio Modo Normal OK"

        Dim pos = POSAutoservicio.Instance

        Try
            Dim booleanPoll = pos.Poll

            ' If booleanPoll Then
            '   MsgBox(msgOK)
            '    Else
            '   MsgBox(msgNOK)
            '   End If
        Catch ex As Exception
            '
            Debug.Print(ex.StackTrace.ToString)
            MsgBox(msgNOK)
        End Try
    End Sub

    Private Sub cmdCargaLlaves_Click(sender As Object, e As EventArgs) Handles cmdCargaLlaves.Click




    End Sub

    Private Sub cmdUltmaVenta_Click(sender As Object, e As EventArgs) Handles cmdUltmaVenta.Click
        Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
        POSAutoservicio.Instance.OpenPort(portName)
        Dim lastStaleResponse = POSAutoservicio.Instance.LastSale()
        Debug.Print(lastStaleResponse.Result.ToString)
    End Sub


End Class