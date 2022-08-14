Imports Transbank.POSIntegrado
Imports Transbank.Responses.CommonResponses
Imports Transbank.Responses.IntegradoResponse

Public Class administraciontbk
    Private Sub cmdPolling_Click(sender As Object, e As EventArgs) Handles cmdPolling.Click
        Dim msgNOK As String
        Dim msgOK As String
        msgNOK = "Problema de conexión con POS"
        msgOK = "Polling OK"

        Dim pos = POSIntegrado.Instance

        Try
            Dim booleanPoll = pos.Poll.Result
            If booleanPoll Then
                MsgBox(msgOK)
            Else
                MsgBox(msgNOK)
            End If
        Catch ex As Exception
            '
            Debug.Print(ex.StackTrace.ToString)
            MsgBox(msgNOK)
        End Try

    End Sub

    Private Sub cmdManualTbk_Click(sender As Object, e As EventArgs) Handles cmdManualTbk.Click
        Dim msgNOK As String
        Dim msgOK As String
        msgNOK = "Cambio Modo Normal Error!"
        msgOK = "Cambio Modo Normal OK"

        Dim pos = POSIntegrado.Instance

        Try
            Dim booleanPoll = pos.SetNormalMode

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
End Class