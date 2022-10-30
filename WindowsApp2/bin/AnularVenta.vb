Imports Transbank.Exceptions.CommonExceptions
Imports Transbank.POSIntegrado

Public Class AnularVenta
    Dim posTimeout As String = Integer.Parse(readConfig("POS_TIMEOUT_MS"))
    Private Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        Dim op As Integer = Convert.ToInt32(txbNroOpe.Text)

        Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass


        Try
            POSIntegrado.Instance.OpenPort(portName)

            Dim response As Task(Of Transbank.Responses.CommonResponses.RefundResponse) = Task.Run(Async Function() Await POSIntegrado.Instance.Refund(op))

            lblEstado.Text = If(response.Status = 1, "WaitingForActivation", "")

            Dim resp = response.Wait(posTimeout)

            If resp Then
                ' MessageBox.Show(response.ToString(), "Refund Success.")
                MessageBox.Show("Respuesta OK", "Anulación")
                Me.Close()
            Else
                If response.Status = TaskStatus.WaitingToRun Then
                    MsgBox("Tiempo superado de espera.", vbInformation, "miAutoPOS")
                ElseIf response.Status = TaskStatus.WaitingForActivation Then
                    MsgBox("Aún no computado.", vbInformation, "miAutoPOS")
                Else
                    MsgBox("Error en Comunicación o Venta no Existe, no se pudo realizar Anulación", vbInformation, "miAutoPOS")
                End If
            End If
            POSIntegrado.Instance.ClosePort()
            lblEstado.Text = ""
        Catch a As TransbankException
            MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub txbNroOpe_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txbNroOpe.KeyPress

        '97 - 122 = Ascii codes for simple letters
        '65 - 90  = Ascii codes for capital letters
        '48 - 57  = Ascii codes for numbers

        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub
End Class


