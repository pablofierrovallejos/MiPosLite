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
        Dim objReader As New IO.StreamReader(sfile)
        Dim sLine As String = ""

        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                If sLine.Trim = "IMPRIMIR_TICKET=SI" Then
                    CheckBox1.Checked = True

                ElseIf sLine.Trim = "IMPRIMIR_TICKET=NO" Then
                    CheckBox1.Checked = False

                Else
                    CheckBox1.Checked = False
                End If
            End If
        Loop Until sLine Is Nothing
        objReader.Close()
    End Sub

    Private Sub Configuracion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        obtenerConfig("C:\ventasPOS\config\impresion_ticket.ini")

        Dim lcoms = POSIntegrado.Instance.ListPorts()   ' Agrega los coms detectados por windows
        If (lcoms IsNot Nothing) Then
            For Each item As String In lcoms
                ComboBox1.Items.Add(item)
                Dim mindex = ComboBox1.FindStringExact(item)  ' Deja seleccionado el último COM encontrado
                ComboBox1.SelectedIndex = mindex
                miComm = item
            Next
        End If



    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click
        If CheckBox1.Checked Then
            agregarRegistro("C:\ventasPOS\config\impresion_ticket.ini", "IMPRIMIR_TICKET=SI")
        Else
            agregarRegistro("C:\ventasPOS\config\impresion_ticket.ini", "IMPRIMIR_TICKET=NO")
        End If
    End Sub

    Private Sub cmdLeer_Click(sender As Object, e As EventArgs) Handles cmdLeer.Click
        ' Leer del fichero INI
        '
        Dim sFicINI As String = Application.StartupPath & "\configparams.ini"
        Dim sSeccion As String = "PARAMETROS"
        Dim sClave As String = "COM_TRANSBANK"
        Dim sValor As String = ""
        '
        TextBox1.Text = mINI.IniGet(sFicINI, sSeccion, sClave, sValor)
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        ' Añadir la sección, clave y/o valor
        '
        Dim sFicINI As String = Application.StartupPath & "\configparams.ini"
        Dim sSeccion As String = "PARAMETROS"
        Dim sClave As String = "COM_TRANSBANK"
        Dim sValor As String = TextBox1.Text
        '
        mINI.IniWrite(sFicINI, sSeccion, sClave, sValor)
    End Sub

End Class