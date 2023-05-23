Imports Transbank.POSAutoservicio
Imports Transbank.Responses.CommonResponses


Imports Transbank.POSIntegrado
Imports Transbank.Exceptions.CommonExceptions
Imports Transbank.Exceptions.IntegradoExceptions
Imports Transbank.Responses
Imports Transbank.Responses.AutoservicioResponse.LastSaleResponse
Imports Transbank.Responses.AutoservicioResponse
Imports System.Reflection.Emit
Imports System.Windows.Forms.AxHost

Public Class frmMenu
    Dim btnMatriz(30) As System.Windows.Forms.Button    ' Botones de productos
    Dim tbNombreProd(30) As TextBox                     ' Textbox para nombre de productos
    Dim tbPrecio(30) As TextBox                         ' Textbox para precio de cada producto
    Public tbHProd(12) As TextBox                          ' Columna de Textbox para los nombres de productos comprados
    Public tbHUnid(12) As TextBox                          ' Columna de Textbox para las cantidades de productos comprados       
    Public tbHValor(12) As TextBox                         ' Columna de Textbox para valor de productos comprados
    Public tbHSubT(12) As TextBox                          ' Columna de Textbox para subtotal productos comprados
    Public indexCompras As Integer = 0 ' Guarda el indice de la compra actual
    Dim itotalProductos As Integer = 0
    Dim miVuelto As Integer = 0
    Dim posTimeout As String = Integer.Parse(readConfig("POS_TIMEOUT_VENTA_MS"))
    Dim habilitarMenu As String = readConfig("HABILITAR_MENU")
    Dim habilitarSonido As String = readConfig("HABILITAR_SONIDO")
    Dim smodoCaja As String = readConfig("MODO_CAJA")
    Dim sonidoHabilitado As Boolean = True
    ' Logger.Nivel = Logger.TipoMensaje.WARNING
    ' Logger.e("Error con excepción", ex)
    'Logger.d("Debug con traza", New StackFrame(True))
    'Logger.i("Info sin traza", New StackFrame(True))
    'Logger.e("Error con excepción y traza", ex, New StackFrame(True))
    Dim mitarjeta
    Dim icontador As Integer
    Dim vueltoDispensado As Boolean
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
        icontador = 0  ' Resetea el contador del timer que limpia la pantalla
        Dim btn As Button = CType(sender, Button)
        Logger.i("ClickButton: btn.Name: " & btn.Name & " ", New StackFrame(True))
        If btn.Name.Length > 3 Then
            Module1.enableButtonPagar = True
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

        habilitaCom_Controlador()
        habilitaCom_Coin()

        'Genera la matriz de botones
        Logger.i("frmMenu_Load ", New StackFrame(True))

        Dim xPos As Integer = 15
        Dim yPos As Integer = 0
        Dim xPosV As Integer = 100     'Posición inicial x de vector columnas textbox con ventas
        Dim yPosV As Integer = 45      'Posición inicial y de vector columnas textbox con ventas
        Dim columna As Integer = 0
        indexCompras = 0
        Dim tamanioFontListaCompras As String = readConfig("TAMANIO_FONT_LIS_COMPRAS")
        Dim espaciadoListaCompras As String = readConfig("ESPACIADO_LIS_COMPRAS")
        Dim totalnroArticulos As String = readConfig("NRO_LINEAS_LIS_COMPRAS")

        btnLimpiar.Location = New Point(102, readConfig("YPOS_BTN_LIMPIAR"))
        lblnumTotal.Location = New Point(576, readConfig("YPOS_LBL_NUMTOTAL"))
        lblcredito.Location = New Point(576, readConfig("YPOS_LBL_CREDITO"))
        lblUnidades.Location = New Point(410, readConfig("YPOS_LBL_TOTUNIDADES"))
        lblPeso.Location = New Point(460, readConfig("YPOS_LBL_TOTVENTA"))
        lblcreditot.Location = New Point(460, readConfig("YPOS_LBL_TOTCREDITO"))
        Panel1.Location = New Point(0, 25)
        Panel2.Location = New Point(readConfig("XPOS_PANEL_BOTONES"), readConfig("YPOS_PANEL_BOTONES"))

        PictureBox1.Location = New Point(readConfig("XPOS_LOGO"), readConfig("YPOS_LOGO"))
        lbltitulo1.Location = New Point(readConfig("XPOS_TITULO1"), readConfig("YPOS_TITULO1"))
        lbltitulo2.Location = New Point(readConfig("XPOS_TITULO2"), readConfig("YPOS_TITULO2"))
        TextBox2.Location = New Point(readConfig("XPOS_TEXT_SERIAL"), readConfig("YPOS_TEXT_SERIAL"))
        Button1.Location = New Point(readConfig("XPOS_BTN_SENDSERIAL"), readConfig("YPOS_BTN_SENDSERIAL"))
        btnAnular.Location = New Point(readConfig("XPOS_BTN_ANULAR"), readConfig("YPOS_BTN_ANULAR"))


        For i As Integer = 0 To totalnroArticulos   ' 11 default , Inicializa Vector columnas productos, unidades, valor y subtotal donde se muestran las compras seleccionadas por cliente
            tbHProd(i) = New TextBox With {
                .Top = yPosV,
                .Left = xPosV,
                .Width = 300,
                .ReadOnly = True,
                .Font = New Font("Arial", tamanioFontListaCompras), .ForeColor = Color.Black
            }
            tbHUnid(i) = New TextBox With {
                .Top = yPosV,
                .Left = xPosV + 310,
                .Width = 90,
                .ReadOnly = True,
                .Font = New Font("Arial", tamanioFontListaCompras), .ForeColor = Color.Black
            }
            tbHValor(i) = New TextBox With {
                .Top = yPosV,
                .Left = xPosV + 410,
                .Width = 90,
                .ReadOnly = True,
                .Font = New Font("Arial", tamanioFontListaCompras), .ForeColor = Color.Black
            }
            tbHSubT(i) = New TextBox With {
                .Top = yPosV,
                .Left = xPosV + 510,
                .Width = 90,
                .ReadOnly = True,
                .Font = New Font("Arial", tamanioFontListaCompras), .ForeColor = Color.Black
            }
            'yPosV += 20
            yPosV += espaciadoListaCompras


            Panel1.Controls.Add(tbHProd(i)) ' Agrega el nombre del producto de compras realizadas al panel 1
            tbHProd(i).BringToFront()

            Panel1.Controls.Add(tbHUnid(i)) ' Agrega la cantidad de compras realizadas al panel 1
            tbHUnid(i).BringToFront()

            Panel1.Controls.Add(tbHValor(i)) ' Agrega el valor compras realizadas al panel 1
            tbHValor(i).BringToFront()

            Panel1.Controls.Add(tbHSubT(i)) ' Agrega el subtotal de compras realizadas al panel 1
            tbHSubT(i).BringToFront()
        Next

        Dim anchoBtnMatriz As String = readConfig("ANCHO_BOTON_MATRIZ")
        Dim altoBtnMatriz As String = readConfig("ALTO_BOTON_MATRIZ")
        Dim nrocolumnasMatriz As String = readConfig("NRO_COLUMNAS_MATRIZ")

        For i As Integer = 0 To 29 ' Inicializa matriz de botones para seleccionar los productos a comprar
            btnMatriz(i) = New System.Windows.Forms.Button
            btnMatriz(i).BackgroundImageLayout = ImageLayout.Stretch
            btnMatriz(i).Width = anchoBtnMatriz '170
            btnMatriz(i).Height = altoBtnMatriz ' 160
            btnMatriz(i).Top = yPos
            btnMatriz(i).Left = xPos

            tbNombreProd(i) = New TextBox
            tbNombreProd(i).Text = ""
            tbNombreProd(i).Top = yPos + 94 ' 145
            tbNombreProd(i).Left = xPos
            tbNombreProd(i).Width = anchoBtnMatriz
            tbNombreProd(i).ReadOnly = True
            tbNombreProd(i).TextAlign = HorizontalAlignment.Center
            tbNombreProd(i).Font = New Font("Arial", 13)
            tbNombreProd(i).ForeColor = Color.RoyalBlue

            tbPrecio(i) = New TextBox
            tbPrecio(i).Text = ""
            tbPrecio(i).Top = yPos + 118
            tbPrecio(i).Left = xPos
            tbPrecio(i).Width = anchoBtnMatriz
            tbPrecio(i).ReadOnly = True
            tbPrecio(i).TextAlign = HorizontalAlignment.Center
            tbPrecio(i).Font = New Font("Arial", 13)
            tbPrecio(i).ForeColor = Color.Red

            If columna < nrocolumnasMatriz Then  '5
                xPos = xPos + 150
                columna = columna + 1
            Else
                columna = 0
                xPos = 15
                yPos = yPos + 156
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
        Dim totalnroArticulos As String = readConfig("NRO_LINEAS_LIS_COMPRAS")
        Dim habilitarSonido As String = readConfig("HABILITAR_SONIDO")
        If indexCompras <= totalnroArticulos Then
            addItemLst(sNomProd, sPrecio)
        Else
            If existeProductoEnLista(sNomProd) Then
                addItemLst(sNomProd, sPrecio)
            Else
                MsgBox("Ha alcanzado el máximo de productos por compra. ", vbInformation, "MiPOSLite")
            End If
        End If
    End Sub
    Public Function existeProductoEnLista(sNomProd As String) As Boolean
        For i As Integer = 0 To (indexCompras - 1)
            If tbHProd(i).Text = sNomProd Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Sub addItemLst(sNomProd As String, sPrecio As String)
        Dim existProd As Boolean = False
        Dim totalMonto As Integer = 0
        Dim totalnroArticulos As String = readConfig("NRO_LINEAS_LIS_COMPRAS")
        For i As Integer = 0 To totalnroArticulos
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
        lblUnidades.Text = sumarProductos()
    End Sub
    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        Logger.i("btnLimpiar_Click():  indexCompras: " & indexCompras, New StackFrame(True))
        If indexCompras > 0 Then
            If sonidoHabilitado Then playsoundbtn("sound3.wav")
            indexCompras -= 1
            lblnumTotal.Text = Integer.Parse(lblnumTotal.Text.Replace(".", "")) - Integer.Parse(tbHSubT(indexCompras).Text.Replace(".", ""))
            tbHProd(indexCompras).Text = ""
            tbHValor(indexCompras).Text = ""
            tbHUnid(indexCompras).Text = ""
            tbHSubT(indexCompras).Text = ""
            lblUnidades.Text = sumarProductos()
            Module1.enableButtonPagar = True
        End If
        icontador = 0  ' Resetea el contador del timer que limpia la pantalla
    End Sub
    Public Function sumarProductos() As Integer
        Dim totprod As Integer = 0
        For i As Integer = 0 To (indexCompras - 1)
            totprod = totprod + Integer.Parse(tbHUnid(i).Text)
        Next
        Return totprod
    End Function
    Private Sub btnPagar_Click(sender As Object, e As EventArgs) Handles btnPagar.Click
        Logger.i("btnPagar_Click(): lblnumTotal: " & lblnumTotal.Text & " Module1.enableButtonPagar: " & Module1.enableButtonPagar, New StackFrame(True))

        If smodoCaja = "EFECTIVO" Then
            ejecutaVentaEfectivo()
        Else
            If Module1.enableButtonPagar Then
                Module1.enableButtonPagar = False

                If lblnumTotal.Text = "0" Or lblnumTotal.Text = "" Then
                    MsgBox("No ha seleccionado ningún producto para pagar.", vbInformation, "MiPOSLite")
                Else
                    If sonidoHabilitado Then playsoundbtn("sound3.wav")
                    ejecutarPosForm()
                End If
            Else
                mitarjeta.BringToFront()
            End If
            icontador = 0  ' Resetea el contador del timer que limpia la pantalla
        End If
    End Sub
    Public Sub ejecutarPosForm()

        mitarjeta = New tarjeta()

        Module1.smontoVenta = lblnumTotal.Text
        Logger.i("ejecutarPosForm(): Module1.smontoVenta: " & Module1.smontoVenta, New StackFrame(True))

        Module1.stotalProductos = itotalProductos
        Logger.i("ejecutarPosForm(): Module1.stotalProductos: " & Module1.stotalProductos, New StackFrame(True))

        Module1.tbHProd = Me.tbHProd
        logTextBox(Module1.tbHProd, " Module1.tbHProd")

        Module1.tbHUnid = Me.tbHUnid
        logTextBox(Module1.tbHUnid, " Module1.tbHUnid")

        Module1.tbHValor = Me.tbHValor
        logTextBox(Module1.tbHValor, " Module1.tbHValor")

        Module1.tbHSubT = Me.tbHSubT
        logTextBox(Module1.tbHSubT, " Module1.tbHSubT")

        Module1.indexCompras = Me.indexCompras
        Logger.i("ejecutarPosForm(): Module1.indexCompras: " & Module1.indexCompras, New StackFrame(True))

        Logger.i("ejecutarPosForm():  indexCompras: " & indexCompras, New StackFrame(True))

        mitarjeta.Show()
        'cleanScreen()
    End Sub

    Public Sub logTextBox(listTextBox() As TextBox, slabel As String)

        For i As Integer = 0 To 11
            If listTextBox(i).Text.Length > 0 Then
                Logger.i("ejecutarPosForm(): logTextBox(): Label:" & slabel & " " & listTextBox(i).Text, New StackFrame(True))
            End If

        Next
    End Sub

    Public Sub cleanScreen()
        Dim totalnroArticulos As String = readConfig("NRO_LINEAS_LIS_COMPRAS")
        Logger.i("cleanScreen():", New StackFrame(True))
        For i As Integer = 0 To totalnroArticulos   ' Inicializa Vector columnas productos, unidades, valor y subtotal donde se muestran las compras seleccionadas por cliente
            indexCompras = 0
            lblnumTotal.Text = "0"
            lblUnidades.Text = "0"
            tbHProd(i).Text = ""
            tbHUnid(i).Text = ""
            tbHValor(i).Text = ""
            tbHSubT(i).Text = ""
        Next
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



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        icontador = icontador + 1
        'Logger.i("Timer1_Tick(): contador: " & contador, New StackFrame(True))
        If icontador >= 30 And isPrintedOK Then
            cleanScreen()
            Logger.i("Timer1_Tick(30sec): Timeout form Me Trigger cleanScreen() ", New StackFrame(True))
            icontador = 0
            isPrintedOK = False
        End If
        If icontador >= 120 Then
            cleanScreen()
            Logger.i("Timer1_Tick(60sec): Timeout form Me Trigger cleanScreen() ", New StackFrame(True))
            icontador = 0
            isPrintedOK = False
        End If
    End Sub

    Public Function habilitaCom_Controlador() As Boolean
        Const puerto As String = "PUERTO_COM_CONTROLADOR"
        Dim sportCom As String = ""
        Try
            sportCom = readConfig(puerto)
            SerialPort1.PortName = sportCom     'Connect on COM4
            SerialPort1.BaudRate = "9600"       'Set BaudRate to 9600
            SerialPort1.Open()
            Return True
        Catch ex As Exception
            MsgBox("No se pudo abrir " + puerto + " config: " + sportCom)
            Return False
        End Try
    End Function
    Public Function habilitaCom_Coin() As Boolean
        Const puerto As String = "PUERTO_COM_COIN"
        Dim sportCom As String = ""
        Try
            sportCom = readConfig(puerto)
            SerialPort2.PortName = sportCom     'Connect on COM4
            SerialPort2.BaudRate = "9600"       'Set BaudRate to 9600

            SerialPort2.Open()
            Return True
        Catch ex As Exception
            MsgBox("No se pudo abrir " + puerto + " config: " + sportCom)
            Return False
        End Try
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SerialPort1.WriteLine("Z")
        TextBox2.AppendText("Enviado>Z" + vbNewLine)
    End Sub


    Public Delegate Sub mydelegate(ByVal s As String)
    Private Sub SerialPort2_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort2.DataReceived
        'Para controlar lectura puerto com coin
        Dim b As String
        SerialPort2.NewLine = vbCr
        SerialPort2.ReadTimeout = 50
        Try
            Dim byteBuffer() As Byte = {0, 0, 0, 0, 0}
            SerialPort2.Read(byteBuffer, 0, 5)

            Dim sbuff As String = ""
            For Each currentField In byteBuffer
                sbuff = sbuff + currentField.ToString + ";"
            Next


            ' b = SerialPort2.Read.ReadExisting '.ReadByte() '.ReadLine()
            Me.BeginInvoke(New mydelegate(AddressOf txt_outCoin), sbuff)
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub
    Private Sub txt_outCoin(ByVal s As String)
        Dim subcadenas() As String
        subcadenas = Split(s, ";")

        s = ""
        For Each currentField In subcadenas
            If currentField.Length >= 1 Then
                s = s + Hex(Integer.Parse(currentField)).ToString + ";"
            End If
        Next

        TextBox2.AppendText("EVENTCoin>" + s + vbNewLine)
        Debug.Print(s)
        ' MsgBox(s)
    End Sub




    Public Delegate Sub mydel(ByVal s As String)
    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Dim b As String
        SerialPort1.NewLine = vbCr
        SerialPort1.ReadTimeout = 50
        Try
            b = SerialPort1.ReadLine()
            Me.BeginInvoke(New mydel(AddressOf txt_out), b)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txt_out(ByVal s As String)
        TextBox2.AppendText("EVENT>" + s + vbNewLine)
        If s.Length >= 2 Then
            Dim command As String
            Dim sdata As String

            s = quitarSaltosLinea(s, "")
            command = Mid(s, 1, 1)
            sdata = Mid(s, 2, s.Length)

            Dim totalventa As Integer
            Dim totalcreadito As Integer
            Dim vueltMaximoDispensador As Integer = Integer.Parse(readConfig("VUELTO_MAXIMO_DISPENS"))

            If command = "U" Then  'Si hay saldo ejecutar venta
                lblcredito.Text = FormatNumber(Int(sdata), 0)
                lblcredito.Refresh()
                If lblnumTotal.Text.Trim.Length > 0 Then
                    totalventa = Int(lblnumTotal.Text.Replace(".", ""))
                End If
                If lblcredito.Text.Trim.Length > 0 Then
                    totalcreadito = Int(lblcredito.Text.Replace(".", ""))
                End If
                If (totalcreadito >= totalventa) Then
                    If (totalcreadito > 0 And totalventa > 0) Then
                        miVuelto = totalcreadito - totalventa
                        If miVuelto <= vueltMaximoDispensador Then
                            vueltoDispensado = True
                            PrintDocument1.Print()
                            Dim nroMonedasVuelto As Integer = Int(miVuelto / 100)
                            SerialPort1.WriteLine("B" + CStr(nroMonedasVuelto))
                            TextBox2.AppendText("Enviado>B" + CStr(nroMonedasVuelto) + vbNewLine) 'Indica a controlador dar vuelto y resetear monto importe a 0
                        Else
                            vueltoDispensado = False
                            PrintDocument1.Print()
                            SerialPort1.WriteLine("R")
                            TextBox2.AppendText("Enviado>R" + vbNewLine) 'Resetea el monto en el controlador
                        End If
                        cleanScreen()
                    End If
                End If
            End If
            If command = "V" Then ' En caso de que se presione boton cancelar venta
                Dim montoInformado As Integer = FormatNumber(Int(sdata), 0)
                SerialPort1.WriteLine("W")
                TextBox2.AppendText("Enviado>W" + vbNewLine) 'Resetea el monto en el controlador
            End If
            If command = "W" Then ' En caso de que se presione boton cancelar venta
                miVuelto = FormatNumber(Int(sdata), 0)
                If miVuelto <= vueltMaximoDispensador Then
                    vueltoDispensado = True
                    PrintDocument1.Print()
                Else
                    vueltoDispensado = False
                    PrintDocument1.Print()
                End If
            End If
        End If
    End Sub

    Private Sub ejecutaVentaEfectivo()

        ' lblcredito.Text = FormatNumber(Int(sdata), 0)
        Dim totalventa As Integer
        Dim totalcreadito As Integer
        Dim vueltMaximoDispensador As Integer = Integer.Parse(readConfig("VUELTO_MAXIMO_DISPENS"))

        lblcredito.Refresh()
        If lblnumTotal.Text.Trim.Length > 0 Then
            totalventa = Int(lblnumTotal.Text.Replace(".", ""))
        End If
        If lblcredito.Text.Trim.Length > 0 Then
            totalcreadito = Int(lblcredito.Text.Replace(".", ""))
        End If
        If (totalcreadito >= totalventa And totalcreadito > 0) Then
            If (totalcreadito > 0 And totalventa > 0) Then
                miVuelto = totalcreadito - totalventa
                If miVuelto <= vueltMaximoDispensador Then
                    vueltoDispensado = True
                    PrintDocument1.Print()
                    Dim nroMonedasVuelto As Integer = Int(miVuelto / 100)
                    SerialPort1.WriteLine("B" + CStr(nroMonedasVuelto))
                    TextBox2.AppendText("Enviado>B" + CStr(nroMonedasVuelto) + vbNewLine) 'Indica a controlador dar vuelto y resetear monto importe a 0
                Else
                    vueltoDispensado = False
                    PrintDocument1.Print()
                    SerialPort1.WriteLine("R")
                    TextBox2.AppendText("Enviado>R" + vbNewLine) 'Resetea el monto en el controlador
                End If
                cleanScreen()
            End If
        Else
            If sonidoHabilitado Then playsoundbtn("sound3.wav")
        End If

    End Sub
    Private Function quitarSaltosLinea(ByVal texto As String, caracterReemplazar As String) As String
        quitarSaltosLinea = Replace(texto, Chr(13), caracterReemplazar)
        quitarSaltosLinea = Replace(quitarSaltosLinea, Chr(13), caracterReemplazar)
        quitarSaltosLinea = Replace(quitarSaltosLinea, vbLf, caracterReemplazar)
    End Function

    Private Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        SerialPort1.WriteLine("R")
        TextBox2.AppendText("Enviado>R" + vbNewLine)
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            Dim margenIzq As Integer
            Dim ilinea As Integer
            Dim ipaso As Integer
            Dim sCompra As String
            Dim mrelleno As Integer
            Dim mrellenoc As Integer
            Dim numAleatorio As New Random()
            Dim valorAleatorio As Integer = numAleatorio.Next(100, 999) ' Numero aleatorio para el ticket
            mrelleno = 17  'Relleno largo  del nombre del producto
            mrellenoc = 2  'Relleno largo  de cantidad de productos
            margenIzq = 0
            ilinea = 2
            ipaso = 18
            Dim prFont As New Font("Consolas", 9, FontStyle.Regular)   ' La fuente a usar en cuerpo
            Dim prFontTit As New Font("Arial", 12, FontStyle.Regular) ' La fuente del titulo
            Dim prFontTerm As New Font("Consolas", 6, FontStyle.Bold)

            e.Graphics.DrawString("    Heladería Serrano", prFontTit, Brushes.Black, margenIzq, ilinea)  'imprimimos el nombre del Local
            ilinea = ilinea + ipaso

            Dim sDate = Date.Now.ToShortDateString.ToString.Replace("-", "")
            Dim shora = Date.Now.ToShortTimeString.ToString.Replace(": ", "")

            Logger.i("PrintDocument1(): sDate: " & sDate & " shora: " & shora, New StackFrame(True))

            e.Graphics.DrawString("Ticket: " & sDate & shora & "-" & valorAleatorio, prFontTerm, Brushes.Black, margenIzq, ilinea)  'Imprimir numero de ticket
            ilinea = ilinea + ipaso

            e.Graphics.DrawString(" ", prFont, Brushes.Black, margenIzq, ilinea)
            sCompra = "Producto       Uni  Sub"  'imprimimos Detalle de la compra
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)

            Dim subtbHProd = ""
            Dim subtbHUnid = ""
            Dim subtbHSubT = ""
            Dim subtmp = ""
            For i As Integer = 0 To indexCompras - 1   ' 
                ilinea = ilinea + ipaso
                Logger.i("PrintDocument1(): i: " & i, New StackFrame(True))

                subtbHProd = tbHProd(i).Text.PadRight(mrelleno, " ")
                Logger.i("PrintDocument1(): subtbHProd: " & subtbHProd, New StackFrame(True))

                subtbHUnid = tbHUnid(i).Text.PadRight(mrellenoc, " ")
                Logger.i("PrintDocument1(): subtbHUnid: " & subtbHUnid, New StackFrame(True))

                subtmp = tbHSubT(i).Text
                Logger.i("PrintDocument1(): subtmp: " & subtmp, New StackFrame(True))

                subtbHSubT = FormatNumber(Integer.Parse(subtmp.Replace(".", "")), 0)
                Logger.i("PrintDocument1(): subtbHSubT: " & subtbHSubT, New StackFrame(True))

                sCompra = subtbHProd & subtbHUnid & " $" & subtbHSubT
                Logger.i("PrintDocument1(): sCompra: " & sCompra, New StackFrame(True))

                e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            Next

            ilinea = ilinea + ipaso
            e.Graphics.DrawString(" ", prFont, Brushes.Black, margenIzq, ilinea) ' hace un salto de linea

            ilinea = ilinea + ipaso
            sCompra = "Total Venta  : $" & lblnumTotal.Text
            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            Logger.i("PrintDocument1(): Total Venta: " & lblnumTotal.Text, New StackFrame(True))

            ilinea = ilinea + ipaso
            sCompra = "Total Importe: $" & lblcredito.Text
            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            Logger.i("PrintDocument1(): Total Importe: " & lblcredito.Text, New StackFrame(True))

            ilinea = ilinea + ipaso
            sCompra = "Vuelto:        $" & miVuelto
            If vueltoDispensado Then
                If miVuelto > 0 Then
                    sCompra = sCompra + " (PAGADO)"
                End If
            Else
                sCompra = sCompra + " Solicitar en Caja!"
            End If

            e.Graphics.DrawString(sCompra, prFont, Brushes.Black, margenIzq, ilinea)
            Logger.i("PrintDocument1():" + sCompra, New StackFrame(True))

            ilinea = ilinea + ipaso
            e.Graphics.DrawString("Retire con este ticket.", prFont, Brushes.Black, margenIzq, ilinea)
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(" ", prFont, Brushes.Black, margenIzq, ilinea)
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(" ", prFontTerm, Brushes.Black, margenIzq, ilinea)
            ilinea = ilinea + ipaso
            e.Graphics.DrawString(" ", prFontTerm, Brushes.Black, margenIzq, ilinea)

            e.HasMorePages = False
            isPrintedOK = True
        Catch ex As Exception
            Logger.i("PrintDocument1(): " & ex.Message, New StackFrame(True))
            Logger.i("PrintDocument1(): " & ex.StackTrace.ToString, New StackFrame(True))
            MessageBox.Show("ERROR: " & ex.Message, "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class