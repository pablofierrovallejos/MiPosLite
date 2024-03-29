﻿Imports Transbank.POSIntegrado
Imports Transbank.Responses.CommonResponses
Imports Transbank.Responses.IntegradoResponse

Public Class Form1
    Dim mfocus As Integer

    Public Sub cargarMarizProductosyPrecios()
        Dim nroCol As Integer = 1
        Dim nroFila As Integer = 1
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("C:\ventasPOS\productos.txt")
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(";")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    nroCol = 1
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String
                    For Each currentField In currentRow
                        setearLabel(nroFila, nroCol, currentField)
                        'MsgBox(currentField)
                        nroCol = nroCol + 1
                    Next
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
                nroFila = nroFila + 1
            End While
        End Using
    End Sub




    Private Sub setearLabel(nroFila As Integer, nroCol As Integer, currentField As String)
        If nroCol = 1 Then   ' Seteamos los nombres de productos
            Select Case nroFila
                Case 1
                    Label6.Text = currentField
                Case 2
                    Label9.Text = currentField
                Case 3
                    Label11.Text = currentField
                Case 4
                    Label17.Text = currentField
                Case 5
                    Label15.Text = currentField
                Case 6
                    Label13.Text = currentField
                Case 7
                    Label30.Text = currentField
                Case 8
                    Label28.Text = currentField
                Case 9
                    Label26.Text = currentField
                Case Else
                    'nada
            End Select
        ElseIf nroCol = 2 Then 'Seteamos los precios
            Select Case nroFila
                Case 1
                    Label7.Text = currentField
                Case 2
                    Label8.Text = currentField
                Case 3
                    Label10.Text = currentField
                Case 4
                    Label16.Text = currentField
                Case 5
                    Label14.Text = currentField
                Case 6
                    Label12.Text = currentField
                Case 7
                    Label29.Text = currentField
                Case 8
                    Label27.Text = currentField
                Case 9
                    Label25.Text = currentField
                Case Else
                    'nada
            End Select
        End If

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

    Private Sub actualizaTotaPagar()
        Label19.Text = 0
        If TextBox9.Text.Trim <> "" Then
            Label19.Text = Integer.Parse(Label19.Text) + Integer.Parse(TextBox22.Text.Trim)
        End If
        If TextBox10.Text.Trim <> "" Then
            Label19.Text = Integer.Parse(Label19.Text) + Integer.Parse(TextBox21.Text.Trim)
        End If
        If TextBox11.Text.Trim <> "" Then
            Label19.Text = Integer.Parse(Label19.Text) + Integer.Parse(TextBox20.Text.Trim)
        End If
        If TextBox12.Text.Trim <> "" Then
            Label19.Text = Integer.Parse(Label19.Text) + Integer.Parse(TextBox19.Text.Trim)
        End If
        If TextBox13.Text.Trim <> "" Then
            Label19.Text = Integer.Parse(Label19.Text) + Integer.Parse(TextBox18.Text.Trim)
        End If
        If TextBox14.Text.Trim <> "" Then
            Label19.Text = Integer.Parse(Label19.Text) + Integer.Parse(TextBox17.Text.Trim)
        End If
        If TextBox15.Text.Trim <> "" Then
            Label19.Text = Integer.Parse(Label19.Text) + Integer.Parse(TextBox16.Text.Trim)
        End If

        If TextBox31.Text.Trim <> "" Then
            TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)
        End If

    End Sub
    Private Sub actualizaTotalCantidades()
        Label18.Text = 0
        If TextBox9.Text.Trim <> "" Then
            Label18.Text = Integer.Parse(Label18.Text) + Integer.Parse(TextBox9.Text.Trim)
        End If
        If TextBox10.Text.Trim <> "" Then
            Label18.Text = Integer.Parse(Label18.Text) + Integer.Parse(TextBox10.Text.Trim)
        End If
        If TextBox11.Text.Trim <> "" Then
            Label18.Text = Integer.Parse(Label18.Text) + Integer.Parse(TextBox11.Text.Trim)
        End If
        If TextBox12.Text.Trim <> "" Then
            Label18.Text = Integer.Parse(Label18.Text) + Integer.Parse(TextBox12.Text.Trim)
        End If
        If TextBox13.Text.Trim <> "" Then
            Label18.Text = Integer.Parse(Label18.Text) + Integer.Parse(TextBox13.Text.Trim)
        End If
        If TextBox14.Text.Trim <> "" Then
            Label18.Text = Integer.Parse(Label18.Text) + Integer.Parse(TextBox14.Text.Trim)
        End If
        If TextBox15.Text.Trim <> "" Then
            Label18.Text = Integer.Parse(Label18.Text) + Integer.Parse(TextBox15.Text.Trim)
        End If

    End Sub
    Private Sub insertaCompra(Producto As String, Precio As String)
        Dim existeProdYaInsertado As Boolean = False
        If TextBox2.Text.Trim = Producto Then
            existeProdYaInsertado = True
            TextBox9.Text = Integer.Parse(TextBox9.Text) + 1  'Cantidad
            TextBox22.Text = Integer.Parse(TextBox9.Text) * Integer.Parse(TextBox29.Text) ' Precio
        ElseIf TextBox3.Text.Trim = Producto Then
            existeProdYaInsertado = True
            TextBox10.Text = Integer.Parse(TextBox10.Text) + 1  'Cantidad
            TextBox21.Text = Integer.Parse(TextBox10.Text) * Integer.Parse(TextBox28.Text) ' Precio
        ElseIf TextBox4.Text.Trim = Producto Then
            existeProdYaInsertado = True
            TextBox11.Text = Integer.Parse(TextBox11.Text) + 1  'Cantidad
            TextBox20.Text = Integer.Parse(TextBox11.Text) * Integer.Parse(TextBox27.Text) ' Precio
        ElseIf TextBox5.Text.Trim = Producto Then
            existeProdYaInsertado = True
            TextBox12.Text = Integer.Parse(TextBox12.Text) + 1  'Cantidad
            TextBox19.Text = Integer.Parse(TextBox12.Text) * Integer.Parse(TextBox26.Text) ' Precio
        ElseIf TextBox6.Text.Trim = Producto Then
            existeProdYaInsertado = True
            TextBox13.Text = Integer.Parse(TextBox13.Text) + 1  'Cantidad
            TextBox18.Text = Integer.Parse(TextBox13.Text) * Integer.Parse(TextBox25.Text) ' Precio
        ElseIf TextBox7.Text.Trim = Producto Then
            existeProdYaInsertado = True
            TextBox14.Text = Integer.Parse(TextBox14.Text) + 1  'Cantidad
            TextBox17.Text = Integer.Parse(TextBox14.Text) * Integer.Parse(TextBox24.Text) ' Precio
        ElseIf TextBox8.Text.Trim = Producto Then
            existeProdYaInsertado = True
            TextBox15.Text = Integer.Parse(TextBox15.Text) + 1  'Cantidad
            TextBox16.Text = Integer.Parse(TextBox15.Text) * Integer.Parse(TextBox23.Text) ' Precio
        End If


        If Not existeProdYaInsertado Then
            If TextBox2.Text.Trim = "" Then
                TextBox2.Text = Producto
                TextBox9.Text = 1
                TextBox29.Text = Precio
                TextBox22.Text = Precio
            ElseIf TextBox3.Text.Trim = "" Then
                TextBox3.Text = Producto
                TextBox10.Text = 1
                TextBox28.Text = Precio
                TextBox21.Text = Precio
            ElseIf TextBox4.Text.Trim = "" Then
                TextBox4.Text = Producto
                TextBox11.Text = 1
                TextBox27.Text = Precio
                TextBox20.Text = Precio
            ElseIf TextBox5.Text.Trim = "" Then
                TextBox5.Text = Producto
                TextBox12.Text = 1
                TextBox26.Text = Precio
                TextBox19.Text = Precio
            ElseIf TextBox6.Text.Trim = "" Then
                TextBox6.Text = Producto
                TextBox13.Text = 1
                TextBox25.Text = Precio
                TextBox18.Text = Precio
            ElseIf TextBox7.Text.Trim = "" Then
                TextBox7.Text = Producto
                TextBox14.Text = 1
                TextBox24.Text = Precio
                TextBox17.Text = Precio
            ElseIf TextBox8.Text.Trim = "" Then
                TextBox8.Text = Producto
                TextBox15.Text = 1
                TextBox23.Text = Precio
                TextBox16.Text = Precio
            End If
        End If
    End Sub
    Private Sub actualizaTotalesManuales()
        Select Case mfocus
            Case 1
                If TextBox29.Text.Trim = "" Then
                    TextBox9.Text = ""
                    TextBox22.Text = ""
                    TextBox2.Text = ""
                Else
                    TextBox22.Text = TextBox29.Text
                    TextBox9.Text = 1
                    TextBox2.Text = "Otro"
                End If
            Case 2
                If TextBox28.Text.Trim = "" Then
                    TextBox10.Text = ""
                    TextBox21.Text = ""
                    TextBox3.Text = ""
                Else
                    TextBox21.Text = TextBox28.Text
                    TextBox10.Text = 1
                    TextBox3.Text = "Otro"
                End If
            Case 3
                If TextBox27.Text.Trim = "" Then
                    TextBox11.Text = ""
                    TextBox20.Text = ""
                    TextBox4.Text = ""
                Else
                    TextBox20.Text = TextBox27.Text
                    TextBox11.Text = 1
                    TextBox4.Text = "Otro"
                End If
            Case 4
                If TextBox26.Text.Trim = "" Then
                    TextBox12.Text = ""
                    TextBox19.Text = ""
                    TextBox5.Text = ""
                Else
                    TextBox19.Text = TextBox26.Text
                    TextBox12.Text = 1
                    TextBox5.Text = "Otro"
                End If
            Case 5
                If TextBox25.Text.Trim = "" Then
                    TextBox13.Text = ""
                    TextBox18.Text = ""
                    TextBox6.Text = ""
                Else
                    TextBox18.Text = TextBox25.Text
                    TextBox13.Text = 1
                    TextBox6.Text = "Otro"
                End If
            Case 6
                If TextBox24.Text.Trim = "" Then
                    TextBox17.Text = ""
                    TextBox14.Text = ""
                    TextBox7.Text = ""
                Else
                    TextBox17.Text = TextBox24.Text
                    TextBox14.Text = 1
                    TextBox7.Text = "Otro"
                End If
            Case 7
                If TextBox23.Text.Trim = "" Then
                    TextBox9.Text = ""
                    TextBox22.Text = ""
                    TextBox2.Text = ""
                Else
                    TextBox16.Text = TextBox23.Text
                    TextBox15.Text = 1
                    TextBox8.Text = "Otro"
                End If
            Case Else
                'nada
        End Select
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            Dim margenIzq As Integer
            Dim ilinea As Integer
            Dim ipaso As Integer
            Dim sCompra As String
            margenIzq = 2
            ilinea = 2
            ipaso = 20
            ' La fuente a usar
            Dim prFont As New Font("Arial", 10, FontStyle.Regular)
            ' la posición superior

            'imprimimos la fecha y hora
            e.Graphics.DrawString(Date.Now.ToShortDateString.ToString & " " & Date.Now.ToShortTimeString.ToString, prFont, Brushes.Black, margenIzq, ilinea)

            'imprimimos el nombre del Local
            ilinea = ilinea + ipaso
            e.Graphics.DrawString("Heladeria Serrano", prFont, Brushes.Black, margenIzq, ilinea)
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(" ", prFont, Brushes.Black, margenIzq, ilinea)


            'imprimimos Detalle de la compra
            sCompra = "Producto  Unidad  Sub."
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)

            If TextBox2.Text.Trim <> "" Then
                ilinea = ilinea + ipaso
                sCompra = TextBox2.Text.PadRight(14, " ") & "   " & TextBox9.Text & " " & TextBox22.Text
                e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            End If
            If TextBox3.Text.Trim <> "" Then
                ilinea = ilinea + ipaso
                sCompra = TextBox3.Text.PadRight(14, " ") & "   " & TextBox10.Text & " " & TextBox21.Text
                e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            End If
            If TextBox4.Text.Trim <> "" Then
                ilinea = ilinea + ipaso
                sCompra = TextBox4.Text.PadRight(14, " ") & "   " & TextBox11.Text & " " & TextBox20.Text
                sCompra = sCompra.PadRight(13)
                e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            End If
            If TextBox5.Text.Trim <> "" Then
                ilinea = ilinea + ipaso
                'sCompra = TextBox5.Text & "   " & TextBox12.Text & "   " & TextBox26.Text & "   " & TextBox19.Text
                sCompra = TextBox5.Text.PadRight(14, " ") & "   " & TextBox12.Text & " " & TextBox19.Text
                sCompra = sCompra.PadRight(13)
                e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            End If

            If TextBox6.Text.Trim <> "" Then
                ilinea = ilinea + ipaso
                'sCompra = TextBox6.Text & "   " & TextBox13.Text & "   " & TextBox25.Text & "   " & TextBox18.Text
                sCompra = TextBox6.Text.PadRight(14, " ") & "   " & TextBox13.Text & " " & TextBox18.Text
                sCompra = sCompra.PadRight(13)
                e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            End If

            If TextBox7.Text.Trim <> "" Then
                ilinea = ilinea + ipaso
                'sCompra = TextBox7.Text & "   " & TextBox14.Text & "   " & TextBox24.Text & "   " & TextBox17.Text
                sCompra = TextBox7.Text.PadRight(14, " ") & "   " & TextBox14.Text & " " & TextBox17.Text
                sCompra = sCompra.PadRight(13)
                e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            End If

            If TextBox8.Text.Trim <> "" Then
                ilinea = ilinea + ipaso
                'sCompra = TextBox8.Text & "   " & TextBox15.Text & "   " & TextBox23.Text & "   " & TextBox16.Text
                sCompra = TextBox8.Text.PadRight(14, " ") & "   " & TextBox15.Text & " " & TextBox16.Text
                sCompra = sCompra.PadRight(13)
                e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            End If

            'Imprime el total
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(" ", prFont, Brushes.Black, margenIzq, ilinea)

            ilinea = ilinea + ipaso
            sCompra = Label2.Text & "   " & Label18.Text & "   " & "$" & Label19.Text
            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)

            'Imprime vuelto
            ilinea = ilinea + ipaso
            sCompra = Label22.Text & "   " & TextBox30.Text
            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)

            ilinea = ilinea + ipaso
            sCompra = Label24.Text & "   " & TextBox31.Text
            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)

            ilinea = ilinea + ipaso
            e.Graphics.DrawString(". ", prFont, Brushes.Black, margenIzq, ilinea)
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(". ", prFont, Brushes.Black, margenIzq, ilinea)
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(". ", prFont, Brushes.Black, margenIzq, ilinea)

            'indicamos que hemos llegado al final de la pagina
            e.HasMorePages = False

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message, "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Function rellenaDer(scad As String)
        scad = scad.Trim
        Dim iLength As Integer = scad.Length
        Dim iLimit As Integer = 16

        Dim s As String
        s = "."
        If iLength < iLimit Then
            For i As Integer = 1 To iLimit - iLength
                s += "."
            Next
        End If

        Return s
    End Function

    Public Function validarPago() As Boolean
        If TextBox30.Text <> "" Then

            If TextBox30.Text.Trim = "0" Then
                Return False

            Else
                Return True

            End If

        End If
    End Function
    Public Function validarVenta() As Boolean
        If TextBox2.Text <> "" And TextBox22.Text <> "" Then
            Return True
        ElseIf TextBox3.Text <> "" And TextBox21.Text <> "" Then
            Return True
        ElseIf TextBox4.Text <> "" And TextBox20.Text <> "" Then
            Return True
        ElseIf TextBox5.Text <> "" And TextBox19.Text <> "" Then
            Return True
        ElseIf TextBox6.Text <> "" And TextBox18.Text <> "" Then
            Return True
        ElseIf TextBox7.Text <> "" And TextBox17.Text <> "" Then
            Return True
        ElseIf TextBox8.Text <> "" And TextBox16.Text = "" Then
            Return True
        Else
            Return False
        End If

    End Function
    Private Sub limpiaPantalla()
        TextBox2.Text = ""
        TextBox9.Text = ""
        TextBox29.Text = ""
        TextBox22.Text = ""
        TextBox3.Text = ""
        TextBox10.Text = ""
        TextBox28.Text = ""
        TextBox21.Text = ""
        TextBox4.Text = ""
        TextBox11.Text = ""
        TextBox27.Text = ""
        TextBox20.Text = ""
        TextBox5.Text = ""
        TextBox12.Text = ""
        TextBox26.Text = ""
        TextBox19.Text = ""
        TextBox6.Text = ""
        TextBox13.Text = ""
        TextBox25.Text = ""
        TextBox18.Text = ""
        TextBox7.Text = ""
        TextBox14.Text = ""
        TextBox24.Text = ""
        TextBox17.Text = ""
        TextBox8.Text = ""
        TextBox15.Text = ""
        TextBox23.Text = ""
        TextBox16.Text = ""
        Label18.Text = ""
        Label19.Text = ""
        TextBox30.Text = ""
        TextBox31.Text = ""
    End Sub
    Private Sub escribeArchivoVentas()
        Dim dt As Date = Today
        Dim sdate As String = Replace(dt, "-", "")
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter("C:\ventasPOS\baseventas\ventasPOS" & sdate & ".csv", True)

        'Dim Linea As String = "Línea de texto " & vbNewLine & "Otra linea de texto"
        Dim Linea As String

        Linea = dt & ";" & Now.ToLongTimeString & ";"

        Linea = Linea & TextBox2.Text & ";" & TextBox9.Text & ";" & TextBox29.Text & ";" & TextBox22.Text & ";" _
                      & TextBox3.Text & ";" & TextBox10.Text & ";" & TextBox28.Text & ";" & TextBox21.Text & ";" _
                      & TextBox4.Text & ";" & TextBox11.Text & ";" & TextBox27.Text & ";" & TextBox20.Text & ";" _
                      & TextBox5.Text & ";" & TextBox12.Text & ";" & TextBox26.Text & ";" & TextBox19.Text & ";" _
                      & TextBox6.Text & ";" & TextBox13.Text & ";" & TextBox25.Text & ";" & TextBox18.Text & ";" _
                      & TextBox7.Text & ";" & TextBox14.Text & ";" & TextBox24.Text & ";" & TextBox17.Text & ";" _
                      & TextBox8.Text & ";" & TextBox15.Text & ";" & TextBox23.Text & ";" & TextBox16.Text & ";" _
                      & Label18.Text & ";" & Label19.Text & ";" & TextBox30.Text & ";" & TextBox31.Text
        file.WriteLine(Linea)
        'oSW.WriteLine(Linea)

        'file.Flush()
        file.Close()
    End Sub

    Private Sub cargarProducto()
        'Button1.BackgroundImage = My.Resources.Imagen1
        'Me.PictureBox1.Image = Image.FromFile("C:\Temp\myImage.jpg")
        'Imagenes para botones de productos
        Try
            Button15.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto1.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button15.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try
        Try
            Button16.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto2.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button16.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try
        Try
            Button17.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto3.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button17.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try
        Try
            Button18.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto4.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button18.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try
        Try
            Button19.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto5.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button19.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try
        Try
            Button20.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto6.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button20.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try
        Try
            Button42.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto7.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button42.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try
        Try
            Button41.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto8.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button41.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try
        Try
            Button40.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto9.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button40.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try

        Try
            Button50.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto10.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button50.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try
        Try
            Button51.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto11.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button51.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try
        Try
            Button52.BackgroundImage = Image.FromFile("C:\ventasPOS\img\producto12.jpeg")
        Catch ex As System.IO.FileNotFoundException
            Button52.BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
        End Try

        'Imagenes para botones de billetes
        Button35.BackgroundImage = Image.FromFile("C:\ventasPOS\img\billete20mil.jpeg")
        Button36.BackgroundImage = Image.FromFile("C:\ventasPOS\img\billete10mil.jpeg")
        Button37.BackgroundImage = Image.FromFile("C:\ventasPOS\img\billete5mil.jpeg")
        Button39.BackgroundImage = Image.FromFile("C:\ventasPOS\img\billete2mil.jpeg")
        Button38.BackgroundImage = Image.FromFile("C:\ventasPOS\img\billete1mil.jpeg")



    End Sub



    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        insertaCompra(Label6.Text, Label7.Text)
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub


    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        insertaCompra(Label9.Text, Label8.Text)

        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        insertaCompra(Label11.Text, Label10.Text)
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        insertaCompra(Label17.Text, Label16.Text)
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        insertaCompra(Label15.Text, Label14.Text)
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        insertaCompra(Label13.Text, Label12.Text)
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        If TextBox9.Text.Trim <> "" Then
            TextBox9.Text = Integer.Parse(TextBox9.Text) + 1
            TextBox22.Text = Integer.Parse(TextBox9.Text) * Integer.Parse(TextBox29.Text)
            actualizaTotalCantidades()
            actualizaTotaPagar()
        End If
    End Sub
    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        If TextBox10.Text.Trim <> "" Then
            TextBox10.Text = Integer.Parse(TextBox10.Text) + 1
            TextBox21.Text = Integer.Parse(TextBox10.Text) * Integer.Parse(TextBox28.Text)
            actualizaTotalCantidades()
            actualizaTotaPagar()
        End If
    End Sub
    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        If TextBox11.Text.Trim <> "" Then
            TextBox11.Text = Integer.Parse(TextBox11.Text) + 1
            TextBox20.Text = Integer.Parse(TextBox11.Text) * Integer.Parse(TextBox27.Text)
            actualizaTotalCantidades()
            actualizaTotaPagar()
        End If
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        If TextBox12.Text.Trim <> "" Then
            TextBox12.Text = Integer.Parse(TextBox12.Text) + 1
            TextBox19.Text = Integer.Parse(TextBox12.Text) * Integer.Parse(TextBox26.Text)
            actualizaTotalCantidades()
            actualizaTotaPagar()
        End If
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        If TextBox13.Text.Trim <> "" Then
            TextBox13.Text = Integer.Parse(TextBox13.Text) + 1
            TextBox18.Text = Integer.Parse(TextBox13.Text) * Integer.Parse(TextBox25.Text)
            actualizaTotalCantidades()
            actualizaTotaPagar()
        End If
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        If TextBox14.Text.Trim <> "" Then
            TextBox14.Text = Integer.Parse(TextBox14.Text) + 1
            TextBox17.Text = Integer.Parse(TextBox14.Text) * Integer.Parse(TextBox24.Text)
            actualizaTotalCantidades()
            actualizaTotaPagar()
        End If
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        If TextBox15.Text.Trim <> "" Then
            TextBox15.Text = Integer.Parse(TextBox15.Text) + 1
            TextBox16.Text = Integer.Parse(TextBox15.Text) * Integer.Parse(TextBox23.Text)
            actualizaTotalCantidades()
            actualizaTotaPagar()
        End If
    End Sub

    '--BOTONES PARA ELIMINAR CANTIDAD DE PRODUCTOS
    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        If TextBox9.Text.Trim <> "" Then
            If Integer.Parse(TextBox9.Text) > 0 Then
                TextBox9.Text = Integer.Parse(TextBox9.Text) - 1
                TextBox22.Text = Integer.Parse(TextBox9.Text) * Integer.Parse(TextBox29.Text)
                actualizaTotalCantidades()
                actualizaTotaPagar()
            Else
                TextBox9.Text = ""
                TextBox29.Text = ""
                TextBox22.Text = ""
                TextBox2.Text = ""

            End If
        End If
    End Sub
    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        If TextBox10.Text.Trim <> "" Then
            If Integer.Parse(TextBox10.Text) > 0 Then
                TextBox10.Text = Integer.Parse(TextBox10.Text) - 1
                TextBox21.Text = Integer.Parse(TextBox10.Text) * Integer.Parse(TextBox28.Text)
                actualizaTotalCantidades()
                actualizaTotaPagar()
            Else
                TextBox10.Text = ""
                TextBox28.Text = ""
                TextBox21.Text = ""
                TextBox3.Text = ""
            End If
        End If
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        If TextBox11.Text.Trim <> "" Then
            If Integer.Parse(TextBox11.Text) > 0 Then
                TextBox11.Text = Integer.Parse(TextBox11.Text) - 1
                TextBox20.Text = Integer.Parse(TextBox11.Text) * Integer.Parse(TextBox27.Text)
                actualizaTotalCantidades()
                actualizaTotaPagar()
            Else
                TextBox11.Text = ""
                TextBox27.Text = ""
                TextBox20.Text = ""
                TextBox4.Text = ""
            End If
        End If
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        If TextBox12.Text.Trim <> "" Then
            If Integer.Parse(TextBox12.Text) > 0 Then
                TextBox12.Text = Integer.Parse(TextBox12.Text) - 1
                TextBox19.Text = Integer.Parse(TextBox12.Text) * Integer.Parse(TextBox26.Text)
                actualizaTotalCantidades()
                actualizaTotaPagar()
            Else
                TextBox12.Text = ""
                TextBox26.Text = ""
                TextBox19.Text = ""
                TextBox5.Text = ""
            End If
        End If
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        If TextBox13.Text.Trim <> "" Then
            If Integer.Parse(TextBox13.Text) > 0 Then
                TextBox13.Text = Integer.Parse(TextBox13.Text) - 1
                TextBox18.Text = Integer.Parse(TextBox13.Text) * Integer.Parse(TextBox25.Text)
                actualizaTotalCantidades()
                actualizaTotaPagar()
            Else
                TextBox13.Text = ""
                TextBox25.Text = ""
                TextBox18.Text = ""
                TextBox6.Text = ""
            End If
        End If
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        If TextBox14.Text.Trim <> "" Then
            If Integer.Parse(TextBox14.Text) > 0 Then
                TextBox14.Text = Integer.Parse(TextBox14.Text) - 1
                TextBox17.Text = Integer.Parse(TextBox14.Text) * Integer.Parse(TextBox24.Text)
                actualizaTotalCantidades()
                actualizaTotaPagar()
            Else
                TextBox14.Text = ""
                TextBox24.Text = ""
                TextBox17.Text = ""
                TextBox7.Text = ""
            End If
        End If
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        If TextBox15.Text.Trim <> "" Then
            If Integer.Parse(TextBox15.Text) > 0 Then
                TextBox15.Text = Integer.Parse(TextBox15.Text) - 1
                TextBox16.Text = Integer.Parse(TextBox15.Text) * Integer.Parse(TextBox23.Text)
                actualizaTotalCantidades()
                actualizaTotaPagar()
            Else
                TextBox15.Text = ""
                TextBox23.Text = ""
                TextBox16.Text = ""
                TextBox8.Text = ""
            End If
        End If
    End Sub



    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub


    Private Sub TextBox29_FOCUS(sender As Object, e As EventArgs) Handles TextBox29.TextChanged, TextBox29.Click
        IndiceFocus = 9
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Select Case mfocus
            Case 1
                TextBox9.Text = 1
                TextBox29.Text = TextBox29.Text + "1"
            Case 2
                TextBox10.Text = 1
                TextBox28.Text = TextBox28.Text + "1"
            Case 3
                TextBox11.Text = 1
                TextBox27.Text = TextBox27.Text + "1"
            Case 4
                TextBox12.Text = 1
                TextBox26.Text = TextBox26.Text + "1"
            Case 5
                TextBox13.Text = 1
                TextBox25.Text = TextBox25.Text + "1"
            Case 6
                TextBox14.Text = 1
                TextBox24.Text = TextBox24.Text + "1"
            Case 7
                TextBox15.Text = 1
                TextBox23.Text = TextBox23.Text + "1"
            Case Else
                'nada
        End Select
    End Sub



    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        If TextBox30.Text.Trim = "" Then
            TextBox30.Text = 0
        End If
        TextBox30.Text = Integer.Parse(TextBox30.Text) + 20000
        TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)

    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        If TextBox30.Text.Trim = "" Then
            TextBox30.Text = 0
        End If
        TextBox30.Text = Integer.Parse(TextBox30.Text) + 10000
        TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        If TextBox30.Text.Trim = "" Then
            TextBox30.Text = 0
        End If
        TextBox30.Text = Integer.Parse(TextBox30.Text) + 5000
        TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)
    End Sub
    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        If TextBox30.Text.Trim = "" Then
            TextBox30.Text = 0
        End If
        TextBox30.Text = Integer.Parse(TextBox30.Text) + 2000
        TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)
    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        If TextBox30.Text.Trim = "" Then
            TextBox30.Text = 0
        End If
        TextBox30.Text = Integer.Parse(TextBox30.Text) + 1000
        TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)
    End Sub
    Private Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        If TextBox30.Text.Trim = "" Then
            TextBox30.Text = 0
        End If
        TextBox30.Text = Integer.Parse(TextBox30.Text) + 500
        TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)
    End Sub
    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        If TextBox30.Text.Trim = "" Then
            TextBox30.Text = 0
        End If
        TextBox30.Text = Integer.Parse(TextBox30.Text) + 100
        TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)
    End Sub

    Private Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        If TextBox30.Text.Trim = "" Then
            TextBox30.Text = 0
        End If
        TextBox30.Text = Integer.Parse(TextBox30.Text) + 50
        TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)
    End Sub

    Private Sub Button47_Click(sender As Object, e As EventArgs) Handles Button47.Click
        If TextBox30.Text.Trim = "" Then
            TextBox30.Text = 0
        End If
        TextBox30.Text = Integer.Parse(TextBox30.Text) + 10
        TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)
    End Sub

    Private Sub Button48_Click(sender As Object, e As EventArgs) Handles Button48.Click
        If TextBox30.Text.Trim = "" Then
            TextBox30.Text = 0
        End If
        TextBox30.Text = Integer.Parse(TextBox30.Text) + 1
        TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)
    End Sub




    Private Sub TextBox30_TextChanged(sender As Object, e As EventArgs) Handles TextBox30.Enter

        If TextBox31.Text.Trim <> "" Then
            TextBox31.Text = TextBox30.Text - Integer.Parse(Label19.Text)
        End If
    End Sub



    Private Sub btnImprimirSimple_Click(sender As Object, e As EventArgs) Handles btnImprimirSimple.Click
        generarVenta()
    End Sub

    Private Sub generarVenta()
        If obtenerEstadoCaja() Then
            actualizaTotalesManuales()
            actualizaTotalCantidades()
            actualizaTotaPagar()

            If validarVenta() Then
                If validarPago() Then
                    If Integer.Parse(TextBox31.Text) >= 0 Then
                        If MessageBox.Show("Ingresar venta?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                            If getConfigPrintTicket() Then
                                PrintDocument1.Print()
                            End If
                            escribeArchivoVentas()
                            limpiaPantalla()
                        End If
                    Else
                        MsgBox("Aún resta por pagar " & TextBox31.Text, , "MiPOSLite")
                    End If
                Else
                    TextBox30.Text = Label19.Text   ' Si no se agrega el monto con el que paga cliente se asume monto exacto
                    TextBox31.Text = 0              ' Se asume vuelto = 0
                    ' MsgBox("No ha ingresado el monto del pago.", , "MiPOSLite")
                    generarVenta()
                End If
            Else
                MsgBox("No ha ingresado ninguna venta.", , "MiPOSLite")
            End If
        Else
            MsgBox("Caja se encuentra Cerrada, proceda a apertura desde Menu->Caja->Apertura/Cierre de Caja", "MiPOSLite")
        End If



    End Sub

    Private Function getConfigPrintTicket() As Boolean
        Dim sLine As String = ""
        Dim sfile As String

        sfile = "C:\ventasPOS\config\impresion_ticket.ini"

        Dim objReader As New IO.StreamReader(sfile)

        Dim imprimeTicket As Boolean

        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                If sLine.Trim = "IMPRIMIR_TICKET=SI" Then
                    imprimeTicket = True

                ElseIf sLine.Trim = "IMPRIMIR_TICKET=NO" Then
                    imprimeTicket = False

                Else
                    imprimeTicket = False
                End If
            End If
        Loop Until sLine Is Nothing
        objReader.Close()
        Return imprimeTicket

    End Function

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        TextBox30.Text = Label19.Text
        TextBox31.Text = 0
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        initScreen()


        Dim lcoms = POSIntegrado.Instance.ListPorts()

        If (lcoms IsNot Nothing) Then

            For Each item As String In lcoms

                'MsgBox(item)
                Debug.Print(item)

            Next

        End If

        ' Pruebas POS ########################################################
        ' Pruebas POS ########################################################
        Dim portName As String = "COM1"

        POSIntegrado.Instance.OpenPort(portName)   ' Abrir puerto com

        POSIntegrado.Instance.ClosePort()          ' Cerrar puerto com



        'POSIntegrado.Instance.IntermediateResponseChange += NewIntermediateMessageReceived   ' EventHandler para los mensajes intermedios.

        Dim Task_resp = POSIntegrado.Instance.Sale(999, "Ticket", False)
        Try

            Dim miFunction = Task_resp.Result.Amount '": 210,
            Dim miResponse = Task_resp.Result.Response '" :  "Aprobado",
            Dim miResponseCode = Task_resp.Result.ResponseCode '" :  "Aprobado",
            Dim miResponseMsg = Task_resp.Result.ResponseMessage '" :  "Aprobado",
            Dim miComerceCode = Task_resp.Result.CommerceCode '"Commerce Code": 550062700310,
            Dim miTerminalId = Task_resp.Result.TerminalId '"Terminal Id": "ABC1234C",
            Dim miTicket = Task_resp.Result.Ticket '"Ticket" :  "ABC123",
            Dim miAuthCode = Task_resp.Result.AuthorizationCode '"Authorization Code": "XZ123456",
            Dim miMonto = Task_resp.Result.Amount
            Dim miSharesNumber = Task_resp.Result.SharesNumber '"Shares Number": 3,
            Dim miSharesAount = Task_resp.Result.SharesAmount  '"Shares Amount" :  5000,
            Dim miLast4Digit = Task_resp.Result.Last4Digits '"Last 4 Digits": 6677,
            Dim miOperationNumber = Task_resp.Result.OperationNumber  '"Operation Number" :  60,
            Dim miCardType = Task_resp.Result.CardType '"Card Type": "CR",
            Dim miAccountingDate = Task_resp.Result.AccountingDate '"Accounting Date" : "28/10/2019 22:35:12",
            Dim miAccountNumber = Task_resp.Result.AccountNumber '"Account Number":"300000000",
            Dim miCardBrand = Task_resp.Result.CardBrand '"Card Brand" :  "AX",
            Dim miRealDate = Task_resp.Result.RealDate '"Real Date": "28/10/2019 22:35:12",
            Dim miEmployeId = Task_resp.Result.EmployeeId '"Employee Id" : 1,
            Dim miTip = Task_resp.Result.Tip '"Tip": 1500,
            '  Dim miChange = Task_resp.Result.c '"Change" :  150,
            'Dim miCommerceProviderCode = Task_resp.Result.co  '"CommerceProviderCode:": 550062712310
            'MsgBox(Monto)

            Debug.Print(miFunction)
            Debug.Print(miResponse)
            Debug.Print(miResponseCode)
            Debug.Print(miResponseMsg)
            Debug.Print(miComerceCode)
            Debug.Print(miTerminalId)
            Debug.Print(miTicket)
            Debug.Print(miAuthCode)
            Debug.Print(miMonto)
            Debug.Print(miSharesNumber)
            Debug.Print(miSharesAount)
            Debug.Print(miLast4Digit)
            Debug.Print(miOperationNumber)
            Debug.Print(miCardType)
            Debug.Print(miAccountingDate)
            Debug.Print(miAccountNumber)
            Debug.Print(miCardBrand)
            Debug.Print(miRealDate)
            Debug.Print(miEmployeId)
            Debug.Print(miTip)


        Catch ex As Exception
            'MsgBox(ex.StackTrace.ToString)
            Debug.Print(ex.StackTrace.ToString)
        End Try


        'Manejador de mensajes intermedios...
    End Sub

    ' Pruebas POS ########################################################
    Private Sub NewIntermediateMessageReceived(sender As Object, IntermediateResponse As Object)


    End Sub
    ' Pruebas POS ########################################################


    Private Sub initScreen()
        cargarProducto() 'Carga imagenes de productos
        cargarMarizProductosyPrecios() 'Carga nombres de productos y precios
        If obtenerEstadoCaja() Then
            Label38.Text = "Abierta"
        Else
            Label38.Text = "Cerrada"
        End If
    End Sub



    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        insertaCompra(Label30.Text, Label29.Text)
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub

    Private Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        insertaCompra(Label28.Text, Label27.Text)
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub

    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
        insertaCompra(Label26.Text, Label25.Text)
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub

    Private Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        limpiaPantalla()
    End Sub



    Private Sub MantenedorDeProductosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MantenedorDeProductosToolStripMenuItem.Click
        Dim form2 As New Form2()
        form2.Show()
    End Sub

    Private Sub TextBox29_Enter(sender As Object, e As EventArgs) Handles TextBox29.Enter
        limpiaVentasSinValor()
        mfocus = 1
        TextBox29.Select()
    End Sub

    Private Sub TextBox28_Enter(sender As Object, e As EventArgs) Handles TextBox28.Enter
        limpiaVentasSinValor()
        mfocus = 2
    End Sub

    Private Sub TextBox27_Enter(sender As Object, e As EventArgs) Handles TextBox27.Enter
        limpiaVentasSinValor()
        mfocus = 3
    End Sub

    Private Sub TextBox26_Enter(sender As Object, e As EventArgs) Handles TextBox26.Enter
        limpiaVentasSinValor()
        mfocus = 4
    End Sub

    Private Sub TextBox25_Enter(sender As Object, e As EventArgs) Handles TextBox25.Enter
        limpiaVentasSinValor()
        mfocus = 5
    End Sub

    Private Sub TextBox24_Enter(sender As Object, e As EventArgs) Handles TextBox24.Enter
        limpiaVentasSinValor()
        mfocus = 6
    End Sub

    Private Sub TextBox23_Enter(sender As Object, e As EventArgs) Handles TextBox23.Enter
        limpiaVentasSinValor()
        mfocus = 7
    End Sub
    Public Sub limpiaVentasSinValor()
        If TextBox29.Text.Trim = "" Then
            TextBox2.Text = ""
            TextBox9.Text = ""
            TextBox22.Text = ""
        End If
        If TextBox28.Text.Trim = "" Then
            TextBox3.Text = ""
            TextBox10.Text = ""
            TextBox21.Text = ""
        End If
        If TextBox27.Text.Trim = "" Then
            TextBox4.Text = ""
            TextBox11.Text = ""
            TextBox20.Text = ""
        End If
        If TextBox26.Text.Trim = "" Then
            TextBox5.Text = ""
            TextBox12.Text = ""
            TextBox19.Text = ""
        End If
        If TextBox25.Text.Trim = "" Then
            TextBox6.Text = ""
            TextBox13.Text = ""
            TextBox18.Text = ""
        End If
        If TextBox24.Text.Trim = "" Then
            TextBox7.Text = ""
            TextBox14.Text = ""
            TextBox17.Text = ""
        End If
        If TextBox23.Text.Trim = "" Then
            TextBox8.Text = ""
            TextBox15.Text = ""
            TextBox16.Text = ""
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Select Case mfocus
            Case 1
                TextBox9.Text = 1
                TextBox29.Text = TextBox29.Text + "2"
            Case 2
                TextBox10.Text = 1
                TextBox28.Text = TextBox28.Text + "2"
            Case 3
                TextBox11.Text = 1
                TextBox27.Text = TextBox27.Text + "2"
            Case 4
                TextBox12.Text = 1
                TextBox26.Text = TextBox26.Text + "2"
            Case 5
                TextBox13.Text = 1
                TextBox25.Text = TextBox25.Text + "2"
            Case 6
                TextBox14.Text = 1
                TextBox24.Text = TextBox24.Text + "2"
            Case 7
                TextBox15.Text = 1
                TextBox23.Text = TextBox23.Text + "2"
            Case Else
                'nada
        End Select
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Select Case mfocus
            Case 1
                TextBox9.Text = 1
                TextBox29.Text = TextBox29.Text + "3"
            Case 2
                TextBox10.Text = 1
                TextBox28.Text = TextBox28.Text + "3"
            Case 3
                TextBox11.Text = 1
                TextBox27.Text = TextBox27.Text + "3"
            Case 4
                TextBox12.Text = 1
                TextBox26.Text = TextBox26.Text + "3"
            Case 5
                TextBox13.Text = 1
                TextBox25.Text = TextBox25.Text + "3"
            Case 6
                TextBox14.Text = 1
                TextBox24.Text = TextBox24.Text + "3"
            Case 7
                TextBox15.Text = 1
                TextBox23.Text = TextBox23.Text + "3"
            Case Else
                'nada
        End Select
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Select Case mfocus
            Case 1
                TextBox9.Text = 1
                TextBox29.Text = TextBox29.Text + "4"
            Case 2
                TextBox10.Text = 1
                TextBox28.Text = TextBox28.Text + "4"
            Case 3
                TextBox11.Text = 1
                TextBox27.Text = TextBox27.Text + "4"
            Case 4
                TextBox12.Text = 1
                TextBox26.Text = TextBox26.Text + "4"
            Case 5
                TextBox13.Text = 1
                TextBox25.Text = TextBox25.Text + "4"
            Case 6
                TextBox14.Text = 1
                TextBox24.Text = TextBox24.Text + "4"
            Case 7
                TextBox15.Text = 1
                TextBox23.Text = TextBox23.Text + "4"
            Case Else
                'nada
        End Select
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Select Case mfocus
            Case 1
                TextBox29.Text = TextBox29.Text + "5"
            Case 2
                TextBox28.Text = TextBox28.Text + "5"
            Case 3
                TextBox27.Text = TextBox27.Text + "5"
            Case 4
                TextBox26.Text = TextBox26.Text + "5"
            Case 5
                TextBox25.Text = TextBox25.Text + "5"
            Case 6
                TextBox24.Text = TextBox24.Text + "5"
            Case 7
                TextBox23.Text = TextBox23.Text + "5"
            Case Else
                'nada
        End Select
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Select Case mfocus
            Case 1
                TextBox9.Text = 1
                TextBox29.Text = TextBox29.Text + "6"
            Case 2
                TextBox10.Text = 1
                TextBox28.Text = TextBox28.Text + "6"
            Case 3
                TextBox11.Text = 1
                TextBox27.Text = TextBox27.Text + "6"
            Case 4
                TextBox12.Text = 1
                TextBox26.Text = TextBox26.Text + "6"
            Case 5
                TextBox13.Text = 1
                TextBox25.Text = TextBox25.Text + "6"
            Case 6
                TextBox14.Text = 1
                TextBox24.Text = TextBox24.Text + "6"
            Case 7
                TextBox15.Text = 1
                TextBox23.Text = TextBox23.Text + "6"
            Case Else
                'nada
        End Select
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Select Case mfocus
            Case 1
                TextBox9.Text = 1
                TextBox29.Text = TextBox29.Text + "7"
            Case 2
                TextBox10.Text = 1
                TextBox28.Text = TextBox28.Text + "7"
            Case 3
                TextBox11.Text = 1
                TextBox27.Text = TextBox27.Text + "7"
            Case 4
                TextBox12.Text = 1
                TextBox26.Text = TextBox26.Text + "7"
            Case 5
                TextBox13.Text = 1
                TextBox25.Text = TextBox25.Text + "7"
            Case 6
                TextBox14.Text = 1
                TextBox24.Text = TextBox24.Text + "7"
            Case 7
                TextBox15.Text = 1
                TextBox23.Text = TextBox23.Text + "7"
            Case Else
                'nada
        End Select
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Select Case mfocus
            Case 1
                TextBox9.Text = 1
                TextBox29.Text = TextBox29.Text + "8"
            Case 2
                TextBox10.Text = 1
                TextBox28.Text = TextBox28.Text + "8"
            Case 3
                TextBox11.Text = 1
                TextBox27.Text = TextBox27.Text + "8"
            Case 4
                TextBox12.Text = 1
                TextBox26.Text = TextBox26.Text + "8"
            Case 5
                TextBox13.Text = 1
                TextBox25.Text = TextBox25.Text + "8"
            Case 6
                TextBox14.Text = 1
                TextBox24.Text = TextBox24.Text + "8"
            Case 7
                TextBox15.Text = 1
                TextBox23.Text = TextBox23.Text + "8"
            Case Else
                'nada
        End Select
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Select Case mfocus
            Case 1
                TextBox9.Text = 1
                TextBox29.Text = TextBox29.Text + "9"
            Case 2
                TextBox10.Text = 1
                TextBox28.Text = TextBox28.Text + "9"
            Case 3
                TextBox11.Text = 1
                TextBox27.Text = TextBox27.Text + "9"
            Case 4
                TextBox12.Text = 1
                TextBox26.Text = TextBox26.Text + "9"
            Case 5
                TextBox13.Text = 1
                TextBox25.Text = TextBox25.Text + "9"
            Case 6
                TextBox14.Text = 1
                TextBox24.Text = TextBox24.Text + "9"
            Case 7
                TextBox15.Text = 1
                TextBox23.Text = TextBox23.Text + "9"
            Case Else
                'nada
        End Select
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Select Case mfocus
            Case 1
                TextBox9.Text = 1
                TextBox29.Text = TextBox29.Text + "0"
            Case 2
                TextBox10.Text = 1
                TextBox28.Text = TextBox28.Text + "0"
            Case 3
                TextBox11.Text = 1
                TextBox27.Text = TextBox27.Text + "0"
            Case 4
                TextBox12.Text = 1
                TextBox26.Text = TextBox26.Text + "0"
            Case 5
                TextBox13.Text = 1
                TextBox25.Text = TextBox25.Text + "0"
            Case 6
                TextBox14.Text = 1
                TextBox24.Text = TextBox24.Text + "0"
            Case 7
                TextBox15.Text = 1
                TextBox23.Text = TextBox23.Text + "0"
            Case Else
                'nada
        End Select
    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click

    End Sub

    Private Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click
        TextBox30.Text = 0
        TextBox31.Text = 0
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Select Case mfocus
            Case 1
                If (TextBox29.Text.Length - 1) >= 0 Then
                    TextBox29.Text = TextBox29.Text.Substring(0, TextBox29.Text.Length - 1)
                End If
                If (TextBox29.Text.Length = 0) Then
                    limpiaVentasSinValor()
                End If

            Case 2
                If (TextBox28.Text.Length - 1) >= 0 Then
                    TextBox28.Text = TextBox28.Text.Substring(0, TextBox28.Text.Length - 1)
                End If
                If (TextBox28.Text.Length = 0) Then
                    limpiaVentasSinValor()
                End If
            Case 3
                If (TextBox27.Text.Length - 1) >= 0 Then
                    TextBox27.Text = TextBox27.Text.Substring(0, TextBox27.Text.Length - 1)
                End If
                If (TextBox27.Text.Length = 0) Then
                    limpiaVentasSinValor()
                End If
            Case 4
                If (TextBox26.Text.Length - 1) >= 0 Then
                    TextBox26.Text = TextBox26.Text.Substring(0, TextBox26.Text.Length - 1)
                End If
                If (TextBox26.Text.Length = 0) Then
                    limpiaVentasSinValor()
                End If
            Case 5
                If (TextBox25.Text.Length - 1) >= 0 Then
                    TextBox25.Text = TextBox25.Text.Substring(0, TextBox25.Text.Length - 1)
                End If
                If (TextBox25.Text.Length = 0) Then
                    limpiaVentasSinValor()
                End If
            Case 6
                If (TextBox24.Text.Length - 1) >= 0 Then
                    TextBox24.Text = TextBox24.Text.Substring(0, TextBox24.Text.Length - 1)
                End If
                If (TextBox24.Text.Length = 0) Then
                    limpiaVentasSinValor()
                End If
            Case 7
                If (TextBox23.Text.Length - 1) >= 0 Then
                    TextBox23.Text = TextBox23.Text.Substring(0, TextBox23.Text.Length - 1)
                End If
                If (TextBox23.Text.Length = 0) Then
                    limpiaVentasSinValor()
                End If
            Case Else
                'nada
        End Select
    End Sub

    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        insertaCompra(Label32.Text, Label31.Text)
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        insertaCompra(Label34.Text, Label33.Text)
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub

    Private Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
        insertaCompra(Label36.Text, Label35.Text)
        actualizaTotalesManuales()
        actualizaTotalCantidades()
        actualizaTotaPagar()
    End Sub

    Private Sub AperturaDeCajaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AperturaDeCajaToolStripMenuItem.Click
        Dim AperturaCaja As New AperturaCaja()
        AperturaCaja.Show()
        Me.Hide()
    End Sub




    Private Sub Form1_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If obtenerEstadoCaja() Then
            Label38.Text = "Abierta"
        Else
            Label38.Text = "Cerrada"
        End If
    End Sub

    Private Sub AyudaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AyudaToolStripMenuItem.Click
        Dim form3 As New Form3()
        form3.Show()
    End Sub

    Private Sub ConfiguraciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfiguraciónToolStripMenuItem.Click
        Dim configuracion As New Configuracion()
        configuracion.Show()
    End Sub

    Private Sub AdministraciónTBKToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdministraciónTBKToolStripMenuItem.Click
        Dim administraciontbk As New administraciontbk()
        administraciontbk.Show()
    End Sub
End Class