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

    ' Sub IMPORTARCSV(ByVal OFD As OpenFileDialog, ByVal TABLA As DataGridView, ByVal DELIMITADOR As String)
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
                    If numeroLinea > 0 Then
                        totalMonto = totalMonto + Integer.Parse(SPLITLINE.GetValue(5))
                        totalArticulos = totalArticulos + Integer.Parse(SPLITLINE.GetValue(4))
                        secuenciaActual = Integer.Parse(SPLITLINE.GetValue(2))
                        If (secuenciaActual <> secuenciaAnterior) Then
                            secuenciaAnterior = secuenciaActual
                            totalVentas = totalVentas + 1
                        Else
                            secuenciaAnterior = secuenciaActual
                        End If
                    End If
                    TABLA.ColumnCount = SPLITLINE.Length - 1
                    TABLA.Rows.Add(SPLITLINE)
                    numeroLinea = numeroLinea + 1
                Loop
                OBJREADER.Close()
            Else
                MsgBox("ARCHIVO INEXISTENTE", MsgBoxStyle.Information, "CSV INEXISTENTE " & RUTA)
            End If


        Catch EX As Exception
            MsgBox("ERROR DE IMPORTACION: " + EX.ToString)
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

    Private Sub MonthCalendar1_Click(sender As Object, e As EventArgs) Handles MonthCalendar1.Click


    End Sub

    Private Sub MonthCalendar1_DoubleClick(sender As Object, e As EventArgs) Handles MonthCalendar1.DoubleClick

    End Sub

    Private Sub MonthCalendar1_MouseClick(sender As Object, e As MouseEventArgs) Handles MonthCalendar1.MouseClick

    End Sub
End Class