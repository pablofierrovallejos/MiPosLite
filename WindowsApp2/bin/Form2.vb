﻿Public Class Form2
    Dim nroRegistroActual As Integer
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim texto As String =
            TextBox1.Text & ";" &   ' Nombre producto
            TextBox2.Text & ";" &   ' Precio
            TextBox3.Text & ";" &   ' UPC
            TextBox4.Text & ";" &   ' Correlativo
            TextBox5.Text & ";" &   ' Archivo de imagen
            "ENABLED"               ' Estado del producto default ENABLED
        agregarRegistro(texto)
        Label7.Text = Integer.Parse(Label7.Text) + 1
        nroRegistroActual = Integer.Parse(Label7.Text) + 1
        Label5.Text = Integer.Parse(Label5.Text) + 1

        'Refrescar pantalla
        frmMenu.Close()
        frmMenu.Show()
        Me.BringToFront()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nroRegistroActual = 1
        leerRegistro(nroRegistroActual)
        Label7.Text = nroRegistroActual
        Label5.Text = contarRegistros()
        If Integer.Parse(Label5.Text) = 0 Then
            Label7.Text = 0
            nroRegistroActual = 0
        End If
    End Sub


    Private Sub leerRegistro(nroRegSolicitado As Integer)
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
                    If nroFila = nroRegSolicitado Then
                        Dim currentField As String
                        For Each currentField In currentRow
                            setearLabel(nroCol, currentField)
                            nroCol = nroCol + 1
                        Next
                    End If
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
                nroFila = nroFila + 1
            End While
        End Using
    End Sub

    Private Sub setearLabel(nroCol As Integer, currentField As String)
        Select Case nroCol
            Case 1                                ' Columna del archivo con el nombre del producto
                TextBox1.Text = currentField
            Case 2                                ' Columna del archivo con el precio
                TextBox2.Text = currentField
            Case 3                                ' Columna del archivo con el UPC
                TextBox3.Text = currentField
            Case 4                                ' Columna que no se ocupa  
                TextBox4.Text = currentField
            Case 5                                ' Columna del archivo con el nombre de archivo de imagen
                TextBox5.Text = currentField
                If Len(currentField) > 3 Then
                    PictureBox1.Image = Image.FromFile(currentField)
                    PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                Else
                    PictureBox1.ImageLocation = ""
                    PictureBox1.Image = Nothing
                    PictureBox1.Refresh()
                End If
            Case 6
                If currentField.Trim = "ENABLED" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            Case Else
                'nada
        End Select

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Integer.Parse(Label5.Text) > 0 Then
            If Integer.Parse(Label5.Text) > Integer.Parse(Label7.Text) Then
                nroRegistroActual = nroRegistroActual + 1
                Label7.Text = Integer.Parse(Label7.Text) + 1
                leerRegistro(Integer.Parse(Label7.Text))
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Integer.Parse(Label7.Text) > 1 Then
            Label7.Text = Integer.Parse(Label7.Text) - 1
            leerRegistro(Integer.Parse(Label7.Text))
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ' nroRegistroActual = 1
        If Integer.Parse(Label5.Text) > 0 Then
            leerRegistro(1)

            Label7.Text = 1
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Integer.Parse(Label5.Text) > 0 Then
            eliminarRegistro(Integer.Parse(Label7.Text))
            Label5.Text = Integer.Parse(Label5.Text) - 1
            'If Integer.Parse(Label7.Text) <= Integer.Parse(Label5.Text) Then
            Label7.Text = Integer.Parse(Label7.Text) - 1
            leerRegistro(Integer.Parse(Label7.Text))
            If Integer.Parse(Label5.Text) = 0 Then
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""

            End If
        End If

        'Refrescar pantalla
        frmMenu.Close()
        frmMenu.Show()
        Me.BringToFront()
    End Sub
    Private Function contarRegistros() As Integer
        Dim nroreg As Integer = 0
        Dim objReader As New IO.StreamReader("C:\ventasPOS\productos.txt")
        Dim sLine As String = ""

        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                'arrText.Add(sLine)
                nroreg = nroreg + 1
            End If
        Loop Until sLine Is Nothing
        objReader.Close()
        ' Label5.Text = nroreg
        Return nroreg
    End Function

    Private Sub eliminarRegistro(nroReg As Integer)
        'Primero caragamos el archivo completo en una lista excepto el que se va a eliminar
        Dim objReader As New IO.StreamReader("C:\ventasPOS\productos.txt")
        Dim sLine As String = ""
        Dim arrText As New ArrayList()
        Dim regActual As Integer = 1
        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                If regActual <> nroReg Then  'Solo carga si es distinto al solicitado a eliminar
                    arrText.Add(sLine)
                End If
            End If
            regActual = regActual + 1
        Loop Until sLine Is Nothing
        objReader.Close()

        'Luego vaciamos el archivo
        vaciarArchivo()

        'finalmente escribimos la lista en el archivo
        For Each sLine In arrText
            agregarRegistro(sLine)
            'Console.WriteLine(sLine)
        Next
    End Sub



    Private Sub agregarRegistro(sLin As String)
        Const fic As String = "C:\ventasPOS\productos.txt"
        ' Dim texto As String = TextBox1.Text & ";" & TextBox2.Text & ";" & TextBox3.Text

        Dim sw As New System.IO.StreamWriter(fic, True)
        sw.WriteLine(sLin)
        sw.Close()
    End Sub

    Private Sub vaciarArchivo()
        Using file As New IO.StreamWriter("C:\ventasPOS\productos.txt")
            file.Flush()
        End Using
    End Sub

    Private Sub BtnGuardaRegActual_Click(sender As Object, e As EventArgs) Handles btnGuardaRegActual.Click
        guardarRegistroActual()
    End Sub
    Public Sub guardarRegistroActual()
        If Integer.Parse(Label5.Text) > 0 Then
            Dim sregistro As String
            sregistro = TextBox1.Text & ";" &
            TextBox2.Text & ";" &
            TextBox3.Text & ";" &
            TextBox4.Text & ";" &
            TextBox5.Text
            If CheckBox1.Checked Then
                sregistro = sregistro & ";ENABLED"
            Else
                sregistro = sregistro & ";DISABLED"
            End If

            actualizarBase(Integer.Parse(Label7.Text), sregistro)

            leerRegistro(Integer.Parse(Label7.Text))
            If Integer.Parse(Label5.Text) = 0 Then
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""

            End If
        End If

        'Refrescar pantalla
        frmMenu.Close()
        frmMenu.Show()
        Me.BringToFront()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If Integer.Parse(Label5.Text) > 0 Then
            leerRegistro(Integer.Parse(Label5.Text))

            Label7.Text = Label5.Text
        End If
    End Sub
    Private Sub actualizarBase(nroReg As Integer, sregistro As String)
        'Primero caragamos el archivo completo en una lista excepto el que se va a eliminar
        Dim objReader As New IO.StreamReader("C:\ventasPOS\productos.txt")
        Dim sLine As String = ""
        Dim arrText As New ArrayList()
        Dim regActual As Integer = 1
        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                If regActual <> nroReg Then  'Solo carga si es distinto al solicitado a eliminar
                    arrText.Add(sLine)
                Else
                    arrText.Add(sregistro)
                End If
            End If
            regActual = regActual + 1
        Loop Until sLine Is Nothing
        objReader.Close()

        'Luego vaciamos el archivo
        vaciarArchivo()

        'finalmente escribimos la lista en el archivo
        For Each sLine In arrText
            agregarRegistro(sLine)
            'Console.WriteLine(sLine)
        Next
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Dim form1 As New Form1()
        frmMenu.Close()
        frmMenu.Show()
        Me.BringToFront()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    'Solo permite digitos para los montos
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
    'Solo permite digitos para el UPC
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub cmdSelecImage_Click(sender As Object, e As EventArgs) Handles cmdSelecImage.Click
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Dim sImagen As String
            sImagen = OpenFileDialog1.FileName
            PictureBox1.Image = Image.FromFile(sImagen)
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            TextBox5.Text = sImagen

        End If
    End Sub

    Private Sub cmdAtras_Click(sender As Object, e As EventArgs) Handles cmdAtras.Click
        If Label7.Text <> Label5.Text Then
            Dim nuevaPosicReg As Integer = Integer.Parse(Label7.Text) + 1
            moverRegistroAtras(Integer.Parse(Label7.Text))
            leerRegistro(nuevaPosicReg)
            Label7.Text = nuevaPosicReg

            'Refresca´pantalla
            frmMenu.Close()
            frmMenu.Show()
            Me.BringToFront()
        End If
    End Sub
    Private Sub moverRegistroAtras(nroReg As Integer)
        'Primero caragamos el archivo completo en una lista , el que movemos lo guardamos en un temporal

        Dim objReader As New IO.StreamReader("C:\ventasPOS\productos.txt")
        Dim sLine As String = ""
        Dim arrText As New ArrayList()
        Dim regActual As Integer = 1
        Dim slineMov As String = ""
        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                If regActual <> nroReg Then  'Solo carga si es distinto al solicitado a mover
                    arrText.Add(sLine)
                Else
                    slineMov = sLine
                End If
            End If
            regActual = regActual + 1
        Loop Until sLine Is Nothing
        objReader.Close()

        arrText.Insert(nroReg, slineMov)

        'Luego vaciamos el archivo
        vaciarArchivo()

        'finalmente escribimos la lista en el archivo
        For Each sLine In arrText
            agregarRegistro(sLine)
            'Console.WriteLine(sLine)
        Next
    End Sub

    Private Sub cmdAdelante_Click(sender As Object, e As EventArgs) Handles cmdAdelante.Click
        If Label7.Text <> "1" Then
            Dim nuevaPosicReg As Integer = Integer.Parse(Label7.Text) - 1
            moverRegistroAdelante(Integer.Parse(Label7.Text))
            leerRegistro(nuevaPosicReg)
            Label7.Text = nuevaPosicReg

            'Refresca´pantalla
            frmMenu.Close()
            frmMenu.Show()
            Me.BringToFront()
        End If
    End Sub
    Private Sub moverRegistroAdelante(nroReg As Integer)
        'Primero caragamos el archivo completo en una lista , el que movemos lo guardamos en un temporal

        Dim objReader As New IO.StreamReader("C:\ventasPOS\productos.txt")
        Dim sLine As String = ""
        Dim arrText As New ArrayList()
        Dim regActual As Integer = 1
        Dim slineMov As String = ""
        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                If regActual <> nroReg Then  'Solo carga si es distinto al solicitado a mover
                    arrText.Add(sLine)
                Else
                    slineMov = sLine
                End If
            End If
            regActual = regActual + 1
        Loop Until sLine Is Nothing
        objReader.Close()

        arrText.Insert((nroReg - 2), slineMov)

        'Luego vaciamos el archivo
        vaciarArchivo()

        'finalmente escribimos la lista en el archivo
        For Each sLine In arrText
            agregarRegistro(sLine)
            'Console.WriteLine(sLine)
        Next
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        'guardarRegistroActual()
    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click
        guardarRegistroActual()
    End Sub
End Class