Imports System.Data.Common.CommandTrees.ExpressionBuilder

Public Class ventas
    Dim totalVentas As Integer
    Dim totalMonto As Integer
    Dim totalArticulos As Integer
    Dim dt As Date = Today
    Dim lstOfStrings As List(Of String)
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
            '  Dim row As DataGridViewRow
            If System.IO.File.Exists(RUTA) = True Then
                Dim OBJREADER As New System.IO.StreamReader(RUTA)
                Do While OBJREADER.Peek() <> -1

                    'row = New DataGridViewRow()
                    ' row.CreateCells(DataGridView1)
                    ' row.Cells(0).Value = "sss"
                    '  row.Cells(1).Value = "ttttt"

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

                    ' TABLA.AutoResizeColumn(0)       'Fecha
                    ' TABLA.AutoResizeColumn(1)       'Hora
                    ' TABLA.AutoResizeColumn(2)       'Secuencia
                    ' TABLA.AutoResizeColumn(3)       'Producto
                    ' TABLA.AutoResizeColumn(4)       'Cantidad
                    ' TABLA.AutoResizeColumn(5)       'Valor
                    ' TABLA.AutoResizeColumn(6)       'SubTotal
                    ' TABLA.AutoResizeColumn(7)       'Cantidad Total
                    ' TABLA.AutoResizeColumn(8)       'Monto Total
                    ' TABLA.AutoResizeColumn(9)      'Comunicacion POS
                    ' TABLA.AutoResizeColumn(10)      'Comprobante transbank
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

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i, j As Integer
        i = e.RowIndex
        j = e.ColumnIndex
        Dim spayload = DataGridView1.Item(j, i).Value
        If spayload.ToString.Length > 100 Then
            Dim resp = MsgBox("Imprimir Boucher TBK?", vbYesNo, "MiPosLite")
            If resp = vbYes Then

                lstOfStrings = formatBoucher(spayload)
                If lstOfStrings IsNot Nothing Then
                    If lstOfStrings.Count > 0 Then
                        PrintDocument1.Print()
                    End If
                End If
            End If
        End If

    End Sub

    Public Function formatBoucher(sdata As String)
        Logger.i(sdata)
        Dim lstOfStrings As New List(Of String)
        Dim slinea As String = ""
        Dim posIni = InStr(sdata, "Shares Type Gloss:")
        If posIni = 0 Then
            Return lstOfStrings
        Else
            Dim labelInicial As String = "Shares Type Gloss:"
            sdata = sdata.Substring(posIni + labelInicial.Length)
            sdata = "          " & sdata

            While sdata.Length > 39
                slinea = sdata.Substring(0, 40)
                sdata = sdata.Substring(40)
                lstOfStrings.Add(slinea)
                Logger.i(slinea)
            End While
            Return lstOfStrings
        End If
    End Function


    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            Logger.i("PrintDocument1_TransBank: ", New StackFrame(True))
            Dim ilinea As Integer = 2
            Dim ipaso As Integer = 13
            Dim prFont As New Font("Consolas", 6, FontStyle.Bold)

            For Each item In lstOfStrings
                ilinea = ilinea + ipaso
                e.Graphics.DrawString(item, prFont, Brushes.Black, 0, ilinea)
            Next

            e.HasMorePages = False
        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message, "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
End Class