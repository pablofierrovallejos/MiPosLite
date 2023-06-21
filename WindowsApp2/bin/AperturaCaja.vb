Public Class AperturaCaja

    Dim SSubTotales As Integer = 0
    Dim SNroVentas As Integer = 0
    Dim SNroProdVendidos As Integer = 0

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        vaciarArchivo("C:\ventasPOS\config.ini")

        agregarRegistro("ESTADO_CAJA=ABIERTA")

        Button1.Enabled = False
        Button2.Enabled = True

    End Sub
    Private Sub sumarventasdelDia()
        Dim dt As Date = Today
        Dim sdate As String = Replace(dt, "-", "")

        Dim nroCol As Integer = 1
        Dim nroFila As Integer = 1

        'Dim SSubTotales As Integer = 0
        'Dim SNroVentas As Integer = 0
        'Dim SNroProdVendidos As Integer = 0


        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("C:\ventasPOS\baseventas\ventasPOS" & sdate & ".csv")
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(";")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    nroCol = 1
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String
                    For Each currentField In currentRow
                        If (nroCol = 32) Then
                            sumarSubtotales(currentField)
                        ElseIf (nroCol = 31) Then
                            sumarProdVendidos(currentField)
                        End If
                        nroCol = nroCol + 1
                    Next
                    SNroVentas = SNroVentas + 1
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try

            End While
        End Using

        'Resumen productos vendidos en el día
        Label16.Text = SNroVentas
        Label13.Text = SSubTotales
        Label14.Text = SNroProdVendidos

    End Sub
    Public Sub sumarSubtotales(slin As String)
        'Dim sumaSubTotales As Integer = 0
        'Dim sumaNroVentas As Integer = 0
        'Dim sumaNroProductosVendidos As Integer = 0
        SSubTotales = SSubTotales + Integer.Parse(slin)
    End Sub

    Public Sub sumarProdVendidos(slin As String)
        'Dim sumaSubTotales As Integer = 0
        'Dim sumaNroVentas As Integer = 0
        'Dim sumaNroProductosVendidos As Integer = 0
        SNroProdVendidos = SNroProdVendidos + Integer.Parse(slin)

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MessageBox.Show("Realizar Cierre de Caja?", "Confirmar Cierre", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            SSubTotales = 0
            SNroVentas = 0
            SNroProdVendidos = 0

            vaciarArchivo("C:\ventasPOS\config.ini")

            agregarRegistro("ESTADO_CAJA=CERRADA")

            sumarventasdelDia()

            Button1.Enabled = True
            Button2.Enabled = False


        End If
    End Sub

    Private Sub vaciarArchivo(sfile As String)
        Using file As New IO.StreamWriter(sfile)
            file.Flush()
        End Using
    End Sub

    Private Function obtenerEstadoCaja() As Boolean
        Dim objReader As New IO.StreamReader("C:\ventasPOS\config.ini")
        Dim sLine As String = ""
        Dim cajaAbierta As Boolean = False
        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                If sLine.Trim = "ESTADO_CAJA=ABIERTA" Then
                    cajaAbierta = True
                ElseIf sLine.Trim = "ESTADO_CAJA=ABIERTA" Then
                    cajaAbierta = False
                Else
                    cajaAbierta = False
                End If
            End If
        Loop Until sLine Is Nothing
        objReader.Close()

        Return cajaAbierta
    End Function

    Private Sub agregarRegistro(sLin As String)
        Const fic As String = "C:\ventasPOS\config.ini"
        ' Dim texto As String = TextBox1.Text & ";" & TextBox2.Text & ";" & TextBox3.Text

        Dim sw As New System.IO.StreamWriter(fic, True)
        sw.WriteLine(sLin)
        sw.Close()
    End Sub



    Private Sub AperturaCaja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If obtenerEstadoCaja() Then  ' Apertura caja
            Button1.Enabled = False
            Button2.Enabled = True
        Else
            Button1.Enabled = True
            Button2.Enabled = False
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        frmMenu.Show()
    End Sub
End Class