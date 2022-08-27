Imports Transbank.POSAutoservicio
Imports Transbank.Responses.CommonResponses


Imports Transbank.POSIntegrado
Imports Transbank.Exceptions.CommonExceptions
Imports Transbank.Exceptions.IntegradoExceptions
Imports Transbank.Responses
Imports Transbank.Responses.AutoservicioResponse.LastSaleResponse
Imports Transbank.Responses.AutoservicioResponse


Public Class frmMenu
    Dim btnMatriz(30) As System.Windows.Forms.Button    ' Botones de productos
    Dim tbNombreProd(30) As TextBox                     ' Textbox para nombre de productos
    Dim tbPrecio(30) As TextBox                         ' Textbox para precio de cada producto
    Public tbHProd(12) As TextBox                          ' Columna de Textbox para los nombres de productos comprados
    Public tbHUnid(12) As TextBox                          ' Columna de Textbox para las cantidades de productos comprados       
    Public tbHValor(12) As TextBox                         ' Columna de Textbox para valor de productos comprados
    Public tbHSubT(12) As TextBox                          ' Columna de Textbox para subtotal productos comprados
    Public indexCompras As Integer ' Guarda el indice de la compra actual
    Dim itotalProductos As Integer = 0

    Dim posTimeout As String = Integer.Parse(readConfig("POS_TIMEOUT_VENTA_MS"))
    Dim habilitarMenu As String = readConfig("HABILITAR_MENU")
    Dim habilitarSonido As String = readConfig("HABILITAR_SONIDO")
    Dim sonidoHabilitado As Boolean = True
    ' Logger.Nivel = Logger.TipoMensaje.WARNING
    ' Logger.e("Error con excepción", ex)
    'Logger.d("Debug con traza", New StackFrame(True))
    'Logger.i("Info sin traza", New StackFrame(True))
    'Logger.e("Error con excepción y traza", ex, New StackFrame(True))

    Public Sub cargarMarizProductosyPrecios()
        Dim nroCol As Integer = 1
        Dim nroFila As Integer = 1
        Dim sNombreProducto As String = ""
        Dim sPrecio As String = ""
        Dim sArchivoImagen As String = ""
        Dim bEnabled As Boolean = True


        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("C:\ventasPOS\productos.txt")
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(";")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                sNombreProducto = ""
                sPrecio = ""
                sArchivoImagen = ""
                bEnabled = True
                Try
                    nroCol = 1
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String

                    For Each currentField In currentRow
                        If (nroCol = 6) Then
                            If (currentField.Trim = "ENABLED") Then
                                bEnabled = True
                            Else
                                bEnabled = False
                            End If
                        End If
                        If (nroCol = 1) Then
                            sNombreProducto = currentField
                        End If
                        If (nroCol = 2) Then
                            sPrecio = currentField
                        End If
                        If (nroCol = 5) Then
                            sArchivoImagen = currentField
                        End If
                        nroCol = nroCol + 1
                    Next
                    If bEnabled Then
                        setearLabel(nroFila, 1, sNombreProducto)
                        setearLabel(nroFila, 2, sPrecio)
                        setearLabel(nroFila, 5, sArchivoImagen)
                        nroFila = nroFila + 1
                    End If

                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
                'nroFila = nroFila + 1
            End While
        End Using
    End Sub

    Private Sub setearLabel(nroFila As Integer, nroCol As Integer, currentField As String)
        If nroCol = 1 Then   ' Seteamos los nombres de productos en los botones
            Try
                tbNombreProd(nroFila - 1).Text = currentField
            Catch ex As System.IO.FileNotFoundException
                tbNombreProd(nroFila - 1).Text = "."
            End Try
            btnMatriz(nroFila - 1).Name = "BTN" & nroFila - 1
        ElseIf nroCol = 2 Then 'Seteamos los precios tbPrecio(i).BringToFront()
            Try
                tbPrecio(nroFila - 1).Text = "$" & currentField
            Catch ex As System.IO.FileNotFoundException
                tbPrecio(nroFila - 1).Text = "."
            End Try
        ElseIf nroCol = 5 Then 'Seteamos las imagenes en los botones
            Try
                btnMatriz(nroFila - 1).BackgroundImage = Image.FromFile(currentField)
            Catch ex As System.IO.FileNotFoundException
                btnMatriz(nroFila - 1).BackgroundImage = Image.FromFile("C:\ventasPOS\img\nodisponible.jpeg")
            End Try
        End If
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

    Private Sub ClickButton(ByVal sender As Object, ByVal e As System.EventArgs) 'Eventos para el arreglo de botones
        Dim btn As Button = CType(sender, Button)
        Logger.i("ClickButton: btn.Name: " & btn.Name & " ", New StackFrame(True))
        If btn.Name.Length > 3 Then
            If sonidoHabilitado Then playsoundbtn("sound5.wav")
            Dim sNombre = btn.Name
            Dim nlargo = sNombre.Length
            Dim subIndx = sNombre.Substring(3, (nlargo - 3))
            Dim nindex = Integer.Parse(subIndx)

            Logger.i("putSaleOnList: tbNombreProd: " & tbNombreProd(nindex).Text & ", tbPrecio: " & tbPrecio(nindex).Text.Replace("$", ""), New StackFrame(True))
            putSaleOnList(tbNombreProd(nindex).Text, tbPrecio(nindex).Text.Replace("$", ""))
        Else
            If sonidoHabilitado Then playsoundbtn("sound0.wav")
        End If

    End Sub

    Private Sub MantenedorDeProductosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MantenedorDeProductosToolStripMenuItem.Click
        If habilitarMenu = "SI" Then
            Dim form2 As New Form2()
            form2.Show()
        End If
    End Sub

    Private Sub AperturaDeCajaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AperturaDeCajaToolStripMenuItem.Click
        If habilitarMenu = "SI" Then
            Dim AperturaCaja As New AperturaCaja()
            AperturaCaja.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub AyudaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AyudaToolStripMenuItem.Click
        Dim form3 As New Form3()
        form3.Show()
    End Sub

    Private Sub ConfiguraciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfiguraciónToolStripMenuItem.Click
        If habilitarMenu = "SI" Then
            Dim configuracion As New Configuracion()
            configuracion.Show()
        End If
    End Sub

    Private Sub AdministraciónTBKToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdministraciónTBKToolStripMenuItem.Click
        If habilitarMenu = "SI" Then
            Dim administraciontbk As New administraciontbk()
            administraciontbk.Show()
        End If
    End Sub

    Private Sub VentasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentasToolStripMenuItem.Click
        If habilitarMenu = "SI" Then
            Dim ventas As New ventas()
            ventas.Show()
        End If
    End Sub


    Public Function generaVentaTransbk(smonto As String) As Boolean
        smonto = smonto.Replace(".", "")
        Dim lcoms = POSAutoservicio.Instance.ListPorts()
        If (lcoms IsNot Nothing) Then
            For Each item As String In lcoms
                Debug.Print(item)
            Next
        End If
        Dim miResponseMsg As String = ""
        Try
            Dim portName As String = readConfig("COM_TRANSBANK")    'Viene de modulo bass
            POSAutoservicio.Instance.OpenPort(portName)

            Dim miResponse As Object
            Dim imonto As Integer = Integer.Parse(smonto)

            Dim Task_resp As Task(Of SaleResponse) = Task.Run(Async Function() Await POSAutoservicio.Instance.Sale(imonto, "Ticket", False, True))
            Dim bresp = Task_resp.Wait(posTimeout)
            If bresp Then
                miResponse = Task_resp.Result.Response                      '" :  "Aprobado",
                Dim miResponseCode = Task_resp.Result.ResponseCode          '" :  "Aprobado",
                miResponseMsg = Task_resp.Result.ResponseMessage            '" :  "Aprobado",
                Dim miComerceCode = Task_resp.Result.CommerceCode           '"Commerce Code": 550062700310,
                Dim miTerminalId = Task_resp.Result.TerminalId              '"Terminal Id": "ABC1234C",
                Dim miTicket = Task_resp.Result.Ticket                      '"Ticket" :  "ABC123",
                Dim miAuthCode = Task_resp.Result.AuthorizationCode         '"Authorization Code": "XZ123456",
                Dim miMonto = Task_resp.Result.Amount                       ' Monto
                Dim miSharesNumber = Task_resp.Result.SharesNumber          '"Shares Number": 3,
                Dim miSharesAount = Task_resp.Result.SharesAmount           '"Shares Amount" :  5000,
                Dim miLast4Digit = Task_resp.Result.Last4Digits             '"Last 4 Digits": 6677,
                Dim miOperationNumber = Task_resp.Result.OperationNumber    '"Operation Number" :  60,
                Dim miCardType = Task_resp.Result.CardType                  '"Card Type": "CR",
                Dim miAccountingDate = Task_resp.Result.AccountingDate      '"Accounting Date" : "28/10/2019 22:35:12",
                Dim miAccountNumber = Task_resp.Result.AccountNumber        '"Account Number":"300000000",
                Dim miCardBrand = Task_resp.Result.CardBrand                '"Card Brand" :  "AX",
                Dim miRealDate = Task_resp.Result.RealDate                  '"Real Date": "28/10/2019 22:35:12",

                Debug.Print("Response          :" & miResponse)
                Debug.Print("ResponseCode      :" & miResponseCode)
                Debug.Print("ResponseMessage   :" & miResponseMsg)
                Debug.Print("CommerceCode      :" & miComerceCode)
                Debug.Print("TerminalId        :" & miTerminalId)
                Debug.Print("Ticket            :" & miTicket)
                Debug.Print("AuthorizationCode :" & miAuthCode)
                Debug.Print("Amount            :" & miMonto)
                Debug.Print("SharesNumber      :" & miSharesNumber)
                Debug.Print("SharesAmount      :" & miSharesAount)
                Debug.Print("Last4Digits       :" & miLast4Digit)
                Debug.Print("OperationNumber   :" & miOperationNumber)
                Debug.Print("CardType          :" & miCardType)
                Debug.Print("AccountingDate    :" & miAccountingDate)
                Debug.Print("AccountNumber     :" & miAccountNumber)
                Debug.Print("CardBrand         :" & miCardBrand)
                Debug.Print("RealDate          :" & miRealDate)
                POSAutoservicio.Instance.ClosePort()
                Module1.sRespuestaTktTransb = miResponse.ToString
            Else
                miResponseMsg = "Rechazado"
            End If
        Catch ex As Exception
            Debug.Print(ex.StackTrace.ToString)
        End Try

        If miResponseMsg = "Aprobado" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Genera la matriz de botones
        Logger.i("frmMenu_Load ", New StackFrame(True))

        Dim xPos As Integer = 15
        Dim yPos As Integer = 0
        Dim xPosV As Integer = 100     'Posición inicial de vector columnas textbox con ventas
        Dim yPosV As Integer = 45     'Posición inicial de vector columnas textbox con ventas
        Dim columna As Integer = 0
        indexCompras = 0

        For i As Integer = 0 To 11   ' Inicializa Vector columnas productos, unidades, valor y subtotal donde se muestran las compras seleccionadas por cliente
            tbHProd(i) = New TextBox With {
                .Top = yPosV,
                .Left = xPosV,
                .Width = 300,
                .ReadOnly = True,
                .Font = New Font("Arial", 12), .ForeColor = Color.Black
            }
            tbHUnid(i) = New TextBox With {
                .Top = yPosV,
                .Left = xPosV + 310,
                .Width = 90,
                .ReadOnly = True,
                .Font = New Font("Arial", 12), .ForeColor = Color.Black
            }
            tbHValor(i) = New TextBox With {
                .Top = yPosV,
                .Left = xPosV + 410,
                .Width = 90,
                .ReadOnly = True,
                .Font = New Font("Arial", 12), .ForeColor = Color.Black
            }
            tbHSubT(i) = New TextBox With {
                .Top = yPosV,
                .Left = xPosV + 510,
                .Width = 90,
                .ReadOnly = True,
                .Font = New Font("Arial", 12), .ForeColor = Color.Black
            }
            yPosV += 20
            Panel1.Controls.Add(tbHProd(i)) ' Agrega el nombre del producto de compras realizadas al panel 1
            tbHProd(i).BringToFront()

            Panel1.Controls.Add(tbHUnid(i)) ' Agrega la cantidad de compras realizadas al panel 1
            tbHUnid(i).BringToFront()

            Panel1.Controls.Add(tbHValor(i)) ' Agrega el valor compras realizadas al panel 1
            tbHValor(i).BringToFront()

            Panel1.Controls.Add(tbHSubT(i)) ' Agrega el subtotal de compras realizadas al panel 1
            tbHSubT(i).BringToFront()
        Next

        For i As Integer = 0 To 29 ' Inicializa matriz de botones para seleccionar los productos a comprar
            btnMatriz(i) = New System.Windows.Forms.Button
            btnMatriz(i).BackgroundImageLayout = ImageLayout.Stretch
            btnMatriz(i).Width = 170 '170; 160
            btnMatriz(i).Height = 160
            btnMatriz(i).Top = yPos
            btnMatriz(i).Left = xPos

            tbNombreProd(i) = New TextBox
            tbNombreProd(i).Text = ""
            tbNombreProd(i).Top = yPos + 145
            tbNombreProd(i).Left = xPos
            tbNombreProd(i).Width = 170
            tbNombreProd(i).ReadOnly = True
            tbNombreProd(i).TextAlign = HorizontalAlignment.Center
            tbNombreProd(i).Font = New Font("Arial", 14)
            tbNombreProd(i).ForeColor = Color.RoyalBlue

            tbPrecio(i) = New TextBox
            tbPrecio(i).Text = ""
            tbPrecio(i).Top = yPos + 172
            tbPrecio(i).Left = xPos
            tbPrecio(i).Width = 170
            tbPrecio(i).ReadOnly = True
            tbPrecio(i).TextAlign = HorizontalAlignment.Center
            tbPrecio(i).Font = New Font("Arial", 14)
            tbPrecio(i).ForeColor = Color.Red

            If columna < 5 Then
                xPos = xPos + 175
                columna = columna + 1
            Else
                columna = 0
                xPos = 15
                yPos = yPos + 230
            End If
            Panel2.Controls.Add(btnMatriz(i))    ' Agrega los botons al panel con productos
            AddHandler btnMatriz(i).Click, AddressOf Me.ClickButton

            Panel2.Controls.Add(tbNombreProd(i)) ' Agrega el nombre del producto de los botones al panel
            Panel2.Controls.Add(tbPrecio(i))     ' Agrega el precio del producto de los botones al panel
            tbNombreProd(i).BringToFront()
            tbPrecio(i).BringToFront()
        Next i

        cargarMarizProductosyPrecios()

        If habilitarSonido = "SI" Then
            sonidoHabilitado = True
        Else
            sonidoHabilitado = False
        End If

    End Sub

    Public Sub putSaleOnList(sNomProd As String, sPrecio As String)
        Dim existProd As Boolean = False
        Dim totalMonto As Integer = 0
        If indexCompras < 12 Then
            For i As Integer = 0 To 11
                If tbHProd(i).Text = sNomProd Then
                    existProd = True
                    tbHUnid(i).Text = Integer.Parse(tbHUnid(i).Text + 1)
                    tbHSubT(i).Text = FormatNumber(Integer.Parse(tbHUnid(i).Text.Replace(".", "") * sPrecio), 0)
                End If
                If tbHSubT(i).Text.Length > 0 Then
                    totalMonto = totalMonto + Integer.Parse(tbHSubT(i).Text.Replace(".", ""))
                End If
            Next
            If Not existProd Then
                tbHProd(indexCompras).Text = sNomProd
                tbHValor(indexCompras).Text = FormatNumber(sPrecio, 0)
                tbHUnid(indexCompras).Text = 1
                tbHSubT(indexCompras).Text = FormatNumber(sPrecio, 0)
                totalMonto = totalMonto + Integer.Parse(sPrecio)
                indexCompras += 1
            End If
            lblnumTotal.Text = FormatNumber(totalMonto, 0)
        Else
            MsgBox("Ha alcanzado el máximo de productos por compra. ", vbInformation, "MiPOSLite")
        End If
        itotalProductos = itotalProductos + 1
        lblUnidades.Text = itotalProductos
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        If indexCompras > 0 Then
            If sonidoHabilitado Then playsoundbtn("sound3.wav")
            indexCompras -= 1
            lblnumTotal.Text = Integer.Parse(lblnumTotal.Text.Replace(".", "")) - Integer.Parse(tbHSubT(indexCompras).Text.Replace(".", ""))
            tbHProd(indexCompras).Text = ""
            tbHValor(indexCompras).Text = ""
            tbHUnid(indexCompras).Text = ""
            tbHSubT(indexCompras).Text = ""
        End If
    End Sub

    Private Sub btnPagar_Click(sender As Object, e As EventArgs) Handles btnPagar.Click
        Logger.i("btnPagar_Click: lblnumTotal: " & lblnumTotal.Text & " ", New StackFrame(True))
        If lblnumTotal.Text = "0" Then
            MsgBox("No ha seleccionado ningún producto para pagar.", vbInformation, "MiPOSLite")
        Else
            If sonidoHabilitado Then playsoundbtn("sound3.wav")
            ejecutarPosForm()
        End If
    End Sub
    Public Sub ejecutarPosForm()
        Module1.smontoVenta = lblnumTotal.Text
        Module1.stotalProductos = itotalProductos
        Module1.tbHProd = Me.tbHProd
        Module1.tbHUnid = Me.tbHUnid
        Module1.tbHValor = Me.tbHValor
        Module1.tbHSubT = Me.tbHSubT
        Module1.indexCompras = Me.indexCompras
        Logger.i("ejecutarPosForm: smontoVenta: " & Module1.smontoVenta & " stotalProductos: " & Module1.stotalProductos, New StackFrame(True))
        Dim mitarjeta As New tarjeta()
        mitarjeta.Show()
    End Sub

    Public Sub playsoundbtn(sfilesound As String)
        Dim player = New Media.SoundPlayer()
        player.SoundLocation = "C:\ventasPOS\img\sound\" & sfilesound
        player.LoadAsync()
        player.PlaySync()
    End Sub

    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Logger.i("btnsalir_Click", New StackFrame(True))
        Me.Close()
    End Sub


End Class