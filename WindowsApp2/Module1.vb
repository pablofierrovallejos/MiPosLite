Module Module1
    Private mINI As New cIniArray

    Public miComm As String
    Public iscomopen As Boolean
    Public sRespuestaTktTransb As String = ""   ' La respuesta de transbank
    Public bsRespuestaTktTransb As Boolean = False
    Public smontoVenta As String = ""
    Public stotalProductos As String = ""

    Public tbHProd(12) As TextBox                          ' Columna de Textbox para los nombres de productos comprados
    Public tbHUnid(12) As TextBox                          ' Columna de Textbox para las cantidades de productos comprados       
    Public tbHValor(12) As TextBox                         ' Columna de Textbox para valor de productos comprados
    Public tbHSubT(12) As TextBox                          ' Columna de Textbox para subtotal productos comprados
    Public indexCompras As Integer

    Public Function writeConfig(scampoClave As String, sValor As String) As Boolean
        Dim sFicINI As String = Application.StartupPath & "\configparams.ini"
        Dim sSeccion As String = "PARAMETROS"
        mINI.IniWrite(sFicINI, sSeccion, scampoClave, sValor)
        Return True
    End Function

    Public Function readConfig(scampoClave As String) As String
        Dim sFicINI As String = Application.StartupPath & "\configparams.ini"
        Dim sSeccion As String = "PARAMETROS"
        Dim sValor As String = ""
        Return mINI.IniGet(sFicINI, sSeccion, scampoClave, sValor)
    End Function

    Private Function obtenerEstadoCaja() As Boolean
        Dim sEstadoCaja As String = Integer.Parse(readConfig("ESTADO_CAJA"))
        If sEstadoCaja.Trim = "ABIERTA" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub escribeArchivoVentas(sMensajeTbk As String, SMensajeTbkLong As String, brespPos As Boolean)
        Dim dt As Date = Today
        Dim sdate As String = Replace(dt, "-", "")
        Dim file As System.IO.StreamWriter
        Dim sfileName = "C:\ventasPOS\baseventas\ventasPOS" & sdate & ".csv"
        Dim isizeFile As Integer
        Dim resultadoTran As String = ""

        Try
            isizeFile = tamanioArchivo(sfileName)
            file = My.Computer.FileSystem.OpenTextFileWriter(sfileName, True)

            Dim hLinea As String
            Dim bLinea As String
            Dim sTotales As String
            hLinea = dt & ";" & Now.ToLongTimeString & ";" & numeroAleatorioDistinto() & ";"
            sTotales = smontoVenta

            If isizeFile = 0 Then ' si el arcchivo esta vacío escribir cabecera
                file.WriteLine("Fecha; Hora; Secuencia; Producto ; Cantidad; Valor; Subtotal; Cantidad Total;Monto Total; Comunicacion POS; Resultado Transbank;Comprobante Transbk")
            End If

            If brespPos Then
                resultadoTran = "EXITOSA"
            Else
                resultadoTran = "FALLIDA"
            End If

            If SMensajeTbkLong Is Nothing Then
                SMensajeTbkLong = ""
            End If
            If sMensajeTbk <> Nothing Then
                sMensajeTbk = sMensajeTbk.Replace("ó", "o")
            End If
            For i As Integer = 0 To indexCompras - 1   ' 
                bLinea = hLinea & tbHProd(i).Text & ";" & tbHUnid(i).Text & ";" & tbHValor(i).Text & ";" & tbHSubT(i).Text & "; " & stotalProductos & ";" & sTotales & ";" & resultadoTran
                file.WriteLine(bLinea & ";" & sMensajeTbk & ";" & SMensajeTbkLong.Replace(vbLf, "|"))
            Next

            file.Close()
        Catch ex As Exception
            MsgBox("Error al escribir archivo ventas. ", vbInformation, "MiPOSLite")
        End Try


    End Sub

    Public Function tamanioArchivo(sfile As String) As Integer
        Dim text As String
        Try
            text = System.IO.File.ReadAllText(sfile)
            Return text.Length
        Catch
            Return 0
        End Try
    End Function
    Private Function numeroAleatorioDistinto() As String
        Dim numAleatorio As New Random(CInt(Date.Now.Ticks And Integer.MaxValue))
        Dim sAleatorio = System.Convert.ToString(numAleatorio.Next(10000, 99999))
        Return sAleatorio
    End Function
End Module
