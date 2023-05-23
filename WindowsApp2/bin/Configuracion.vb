Imports Transbank.POSIntegrado
Imports Transbank.Responses.CommonResponses
Imports Transbank.Responses.IntegradoResponse

Public Class Configuracion

    Private mINI As New cIniArray               'Para inicializar clase que genera archivo de inicialización
    Private Sub vaciarArchivo(sfile As String)
        Using file As New IO.StreamWriter(sfile)
            file.Flush()
            file.Close()
        End Using
    End Sub

    Private Sub agregarRegistro(sfile As String, sLin As String)
        Dim sw As New System.IO.StreamWriter(sfile, False)
        sw.WriteLine(sLin)
        sw.Close()
    End Sub

    Private Sub obtenerConfig(sfile As String)
        ' obtenerConfig("C:\ventasPOS\config\impresion_ticket.ini")
        ' Dim objReader As New IO.StreamReader(sfile)
        ' Dim sLine As String = ""

        '   Do
        '  sLine = objReader.ReadLine()
        ' If Not sLine Is Nothing Then
        'If sLine.Trim = "IMPRIMIR_TICKET=SI" Then
        'CheckBox1.Checked = True
        'ElseIf sLine.Trim = "IMPRIMIR_TICKET=NO" Then
        'CheckBox1.Checked = False
        'Else
        'CheckBox1.Checked = False
        'End If
        'End If
        'Loop Until sLine Is Nothing
        'objReader.Close()
    End Sub

    Private Sub Configuracion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'obtenerConfig("C:\ventasPOS\config\impresion_ticket.ini")

        Dim lcoms = POSIntegrado.Instance.ListPorts()           ' Agrega los coms detectados por windows
        If (lcoms IsNot Nothing) Then
            For Each item As String In lcoms
                ComboBox1.Items.Add(item)
                Dim mindex = ComboBox1.FindStringExact(item)    ' Deja seleccionado el último COM encontrado
                ComboBox1.SelectedIndex = mindex
                miComm = item
            Next
        End If

        readFullConfig()
    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click
        If CheckBox1.Checked Then
            agregarRegistro("C:\ventasPOS\config\impresion_ticket.ini", "IMPRIMIR_TICKET=SI")
        Else
            agregarRegistro("C:\ventasPOS\config\impresion_ticket.ini", "IMPRIMIR_TICKET=NO")
        End If
    End Sub

    Private Sub cmdLeer_Click(sender As Object, e As EventArgs) Handles cmdLeer.Click
        readFullConfig()
    End Sub

    Public Sub readFullConfig()
        TextBox1.Text = readConfig("COM_TRANSBANK")

        Dim tmp = readConfig("PRINT_TKT_INTERNO")
        If tmp = "SI" Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If

        If readConfig("PRINT_TKT_TRANSBK") = "SI" Then
            CheckBox2.Checked = True
        Else
            CheckBox2.Checked = False
        End If

    End Sub
    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        ' Añadir la sección, clave y/o valor
        writeConfig("COM_TRANSBANK", TextBox1.Text)

        If CheckBox1.Checked Then
            writeConfig("PRINT_TKT_INTERNO", "SI")
        Else
            writeConfig("PRINT_TKT_INTERNO", "NO")
        End If

        If CheckBox2.Checked Then
            writeConfig("PRINT_TKT_TRANSBK", "SI")
        Else
            writeConfig("PRINT_TKT_TRANSBK", "NO")
        End If
    End Sub

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = ComboBox1.SelectedItem
        writeConfig("COM_TRANSBANK", TextBox1.Text)
    End Sub


End Class