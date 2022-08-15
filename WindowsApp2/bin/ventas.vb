Public Class ventas
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        IMPORTARCSV(DataGridView1, ";")
    End Sub

    ' Sub IMPORTARCSV(ByVal OFD As OpenFileDialog, ByVal TABLA As DataGridView, ByVal DELIMITADOR As String)
    Sub IMPORTARCSV(ByVal TABLA As DataGridView, ByVal DELIMITADOR As String)
        Try
            Dim RUTA As String = "C:\ventasPOS\baseventas\ventasPOS14082022.csv"

            ' OFD.InitialDirectory = "C:\TEMP\"
            'OFD.Filter = "CSV FILES (*.CSV)|*.CSV"
            'OFD.FilterIndex = 2
            'OFD.RestoreDirectory = True

            'If (OFD.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            'RUTA = OFD.FileName
            'End If

            Dim TEXTLINE As String = ""
            Dim SPLITLINE() As String

            If System.IO.File.Exists(RUTA) = True Then
                Dim OBJREADER As New System.IO.StreamReader(RUTA)
                Do While OBJREADER.Peek() <> -1
                    TEXTLINE = OBJREADER.ReadLine()
                    SPLITLINE = Split(TEXTLINE, DELIMITADOR)
                    TABLA.ColumnCount = SPLITLINE.Length - 1
                    TABLA.Rows.Add(SPLITLINE)
                Loop
            Else
                MsgBox("ARCHIVO INEXISTENTE", MsgBoxStyle.Information, "CSV INEXISTENTE " & RUTA)
            End If
        Catch EX As Exception
            MsgBox("ERROR DE IMPORTACION: " + EX.ToString)
        End Try
    End Sub
End Class