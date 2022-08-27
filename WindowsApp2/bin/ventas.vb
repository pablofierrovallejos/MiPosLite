Public Class ventas
    Dim totalVentas As Integer
    Dim totalMonto As Integer
    Dim totalArticulos As Integer
    Dim dt As Date = Today

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        IMPORTARCSV(DataGridView1, ";")
    End Sub

    Sub IMPORTARCSV(ByVal TABLA As DataGridView, ByVal DELIMITADOR As String)
        Dim numeroLinea As Integer = 0
        Dim secuenciaActual As Integer = 0
        Dim secuenciaAnterior As Integer
        totalVentas = 0
        totalMonto = 0
        totalArticulos = 0
        Label5.Text = dt
        Dim sdate As String = Replace(dt, "-", "")

        Try
            Dim RUTA As String = "C:\ventasPOS\baseventas\ventasPOS" & sdate & ".csv"
            Dim TEXTLINE As String = ""
            Dim SPLITLINE() As String

            If System.IO.File.Exists(RUTA) = True Then
                Dim OBJREADER As New System.IO.StreamReader(RUTA)
                Do While OBJREADER.Peek() <> -1
                    TEXTLINE = OBJREADER.ReadLine()
                    SPLITLINE = Split(TEXTLINE, DELIMITADOR)
                    If numeroLinea > 0 Then

                        If SPLITLINE.GetValue(10) = "Aprobado" Then   ' Valida si transaccion es EXITOSA o FALLIDA por transbank
                            totalMonto = totalMonto + Integer.Parse(SPLITLINE.GetValue(5).ToString.Replace(".", "")) * Integer.Parse(SPLITLINE.GetValue(4).ToString.Replace(".", ""))       ' Sumatoria de los montos
                            totalArticulos = totalArticulos + Integer.Parse(SPLITLINE.GetValue(4))  ' Sumatoria de artículos
                        End If

                        secuenciaActual = Integer.Parse(SPLITLINE.GetValue(2))
                        If (secuenciaActual <> secuenciaAnterior) Then
                            secuenciaAnterior = secuenciaActual

                            If SPLITLINE.GetValue(10) = "Aprobado" Then
                                totalVentas = totalVentas + 1
                            End If
                        Else
                            secuenciaAnterior = secuenciaActual
                        End If
                    End If
                    TABLA.ColumnCount = SPLITLINE.Length - 1
                    TABLA.Rows.Add(SPLITLINE)
                    TABLA.AutoResizeColumn(0)       'Fecha
                    TABLA.AutoResizeColumn(1)       'Hora
                    TABLA.AutoResizeColumn(2)       'Secuencia
                    TABLA.AutoResizeColumn(3)       'Producto
                    TABLA.AutoResizeColumn(4)       'Cantidad
                    TABLA.AutoResizeColumn(5)       'Valor
                    TABLA.AutoResizeColumn(6)       'SubTotal
                    TABLA.AutoResizeColumn(7)       'Cantidad Total
                    TABLA.AutoResizeColumn(8)       'Monto Total
                    TABLA.AutoResizeColumn(9)      'Comunicacion POS
                    TABLA.AutoResizeColumn(10)      'Comprobante transbank
                    TABLA.BackgroundColor = Color.White
                    numeroLinea = numeroLinea + 1

                    ' TABLA.CurrentCell = TABLA.Rows(TABLA.Rows.Count - 1).Cells(TABLA.CurrentCell.ColumnIndex)
                    DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.Rows.Count - 1
                Loop
                OBJREADER.Close()
            Else
                MsgBox("ARCHIVO INEXISTENTE", MsgBoxStyle.Information, "CSV INEXISTENTE " & RUTA)
            End If
        Catch EX As Exception
            MsgBox("ERROR DE IMPORTACION: " + EX.ToString + " numeroLinea: " + numeroLinea.ToString)
        End Try
        TextBox1.Text = totalMonto
        TextBox2.Text = totalArticulos
        TextBox3.Text = totalVentas
    End Sub

    Private Sub ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IMPORTARCSV(DataGridView1, ";")
    End Sub

    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()

        dt = MonthCalendar1.SelectionStart.Date
        IMPORTARCSV(DataGridView1, ";")
    End Sub

End Class