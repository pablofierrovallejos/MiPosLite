Public Class Form2
    Dim nroRegistroActual As Integer
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim texto As String = TextBox1.Text & ";" & TextBox2.Text & ";" & TextBox3.Text
        agregarRegistro(texto)
        Label7.Text = Integer.Parse(Label7.Text) + 1
        nroRegistroActual = Integer.Parse(Label7.Text) + 1
        Label5.Text = Integer.Parse(Label5.Text) + 1


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
            Case 1
                TextBox1.Text = currentField
            Case 2
                TextBox2.Text = currentField
            Case 3
                TextBox3.Text = currentField
            Case 4
                TextBox4.Text = currentField
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If Integer.Parse(Label5.Text) > 0 Then
            leerRegistro(Integer.Parse(Label5.Text))

            Label7.Text = Label5.Text
        End If
    End Sub
End Class