<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMenu
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Public IndiceFocus As Integer


    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenu))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.btnPagar = New System.Windows.Forms.Button()
        Me.lblcreditot = New System.Windows.Forms.Label()
        Me.lblcredito = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblUnidades = New System.Windows.Forms.Label()
        Me.lbltitulo2 = New System.Windows.Forms.Label()
        Me.lbltitulo1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnLimpiar = New System.Windows.Forms.Button()
        Me.lblPeso = New System.Windows.Forms.Label()
        Me.lblsubtotalh = New System.Windows.Forms.Label()
        Me.lblnumTotal = New System.Windows.Forms.Label()
        Me.lblValor = New System.Windows.Forms.Label()
        Me.lblUnidad = New System.Windows.Forms.Label()
        Me.lblProducto = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CajaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AperturaDeCajaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MantenedorDeProductosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfiguraciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdministraciónTBKToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VentasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AyudaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnsalir = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.SerialPort2 = New System.IO.Ports.SerialPort(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.btnAnular)
        Me.Panel1.Controls.Add(Me.btnPagar)
        Me.Panel1.Controls.Add(Me.lblcreditot)
        Me.Panel1.Controls.Add(Me.lblcredito)
        Me.Panel1.Controls.Add(Me.TextBox2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.lblUnidades)
        Me.Panel1.Controls.Add(Me.lbltitulo2)
        Me.Panel1.Controls.Add(Me.lbltitulo1)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.btnLimpiar)
        Me.Panel1.Controls.Add(Me.lblPeso)
        Me.Panel1.Controls.Add(Me.lblsubtotalh)
        Me.Panel1.Controls.Add(Me.lblnumTotal)
        Me.Panel1.Controls.Add(Me.lblValor)
        Me.Panel1.Controls.Add(Me.lblUnidad)
        Me.Panel1.Controls.Add(Me.lblProducto)
        Me.Panel1.Location = New System.Drawing.Point(0, 26)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1350, 262)
        Me.Panel1.TabIndex = 0
        '
        'btnAnular
        '
        Me.btnAnular.Location = New System.Drawing.Point(945, 234)
        Me.btnAnular.Name = "btnAnular"
        Me.btnAnular.Size = New System.Drawing.Size(75, 23)
        Me.btnAnular.TabIndex = 84
        Me.btnAnular.Text = "Anular"
        Me.btnAnular.UseVisualStyleBackColor = True
        '
        'btnPagar
        '
        Me.btnPagar.Font = New System.Drawing.Font("Arial Black", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPagar.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnPagar.Location = New System.Drawing.Point(617, 22)
        Me.btnPagar.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPagar.Name = "btnPagar"
        Me.btnPagar.Size = New System.Drawing.Size(228, 108)
        Me.btnPagar.TabIndex = 10
        Me.btnPagar.Text = "PAGAR"
        Me.btnPagar.UseVisualStyleBackColor = True
        '
        'lblcreditot
        '
        Me.lblcreditot.AutoSize = True
        Me.lblcreditot.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcreditot.Location = New System.Drawing.Point(445, 208)
        Me.lblcreditot.Name = "lblcreditot"
        Me.lblcreditot.Size = New System.Drawing.Size(112, 20)
        Me.lblcreditot.TabIndex = 83
        Me.lblcreditot.Text = "Total Crédito $"
        '
        'lblcredito
        '
        Me.lblcredito.AutoSize = True
        Me.lblcredito.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold)
        Me.lblcredito.ForeColor = System.Drawing.Color.Green
        Me.lblcredito.Location = New System.Drawing.Point(617, 208)
        Me.lblcredito.Name = "lblcredito"
        Me.lblcredito.Size = New System.Drawing.Size(51, 56)
        Me.lblcredito.TabIndex = 82
        Me.lblcredito.Text = "0"
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(709, 13)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox2.Size = New System.Drawing.Size(363, 190)
        Me.TextBox2.TabIndex = 81
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(864, 234)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 80
        Me.Button1.Text = "Consultar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lblUnidades
        '
        Me.lblUnidades.AutoSize = True
        Me.lblUnidades.Location = New System.Drawing.Point(462, 338)
        Me.lblUnidades.Name = "lblUnidades"
        Me.lblUnidades.Size = New System.Drawing.Size(10, 13)
        Me.lblUnidades.TabIndex = 79
        Me.lblUnidades.Text = "."
        '
        'lbltitulo2
        '
        Me.lbltitulo2.AutoSize = True
        Me.lbltitulo2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltitulo2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lbltitulo2.Location = New System.Drawing.Point(885, 142)
        Me.lbltitulo2.Name = "lbltitulo2"
        Me.lbltitulo2.Size = New System.Drawing.Size(106, 29)
        Me.lbltitulo2.TabIndex = 78
        Me.lbltitulo2.Text = "Serrano"
        '
        'lbltitulo1
        '
        Me.lbltitulo1.AutoSize = True
        Me.lbltitulo1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltitulo1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lbltitulo1.Location = New System.Drawing.Point(877, 117)
        Me.lbltitulo1.Name = "lbltitulo1"
        Me.lbltitulo1.Size = New System.Drawing.Size(127, 29)
        Me.lbltitulo1.TabIndex = 77
        Me.lbltitulo1.Text = "Heladería"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(864, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(149, 129)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 76
        Me.PictureBox1.TabStop = False
        '
        'btnLimpiar
        '
        Me.btnLimpiar.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.Location = New System.Drawing.Point(88, 191)
        Me.btnLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(112, 37)
        Me.btnLimpiar.TabIndex = 75
        Me.btnLimpiar.Text = "Limpiar"
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'lblPeso
        '
        Me.lblPeso.AutoSize = True
        Me.lblPeso.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeso.Location = New System.Drawing.Point(445, 183)
        Me.lblPeso.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPeso.Name = "lblPeso"
        Me.lblPeso.Size = New System.Drawing.Size(104, 20)
        Me.lblPeso.TabIndex = 62
        Me.lblPeso.Text = "Total Venta $"
        '
        'lblsubtotalh
        '
        Me.lblsubtotalh.AutoSize = True
        Me.lblsubtotalh.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsubtotalh.Location = New System.Drawing.Point(623, 23)
        Me.lblsubtotalh.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblsubtotalh.Name = "lblsubtotalh"
        Me.lblsubtotalh.Size = New System.Drawing.Size(81, 19)
        Me.lblsubtotalh.TabIndex = 61
        Me.lblsubtotalh.Text = "Sub Total"
        '
        'lblnumTotal
        '
        Me.lblnumTotal.AutoSize = True
        Me.lblnumTotal.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnumTotal.ForeColor = System.Drawing.Color.Red
        Me.lblnumTotal.Location = New System.Drawing.Point(617, 154)
        Me.lblnumTotal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblnumTotal.Name = "lblnumTotal"
        Me.lblnumTotal.Size = New System.Drawing.Size(51, 56)
        Me.lblnumTotal.TabIndex = 53
        Me.lblnumTotal.Text = "0"
        '
        'lblValor
        '
        Me.lblValor.AutoSize = True
        Me.lblValor.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValor.Location = New System.Drawing.Point(553, 23)
        Me.lblValor.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblValor.Name = "lblValor"
        Me.lblValor.Size = New System.Drawing.Size(48, 19)
        Me.lblValor.TabIndex = 39
        Me.lblValor.Text = "Valor"
        '
        'lblUnidad
        '
        Me.lblUnidad.AutoSize = True
        Me.lblUnidad.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnidad.Location = New System.Drawing.Point(437, 23)
        Me.lblUnidad.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblUnidad.Name = "lblUnidad"
        Me.lblUnidad.Size = New System.Drawing.Size(64, 19)
        Me.lblUnidad.TabIndex = 31
        Me.lblUnidad.Text = "Unidad"
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProducto.Location = New System.Drawing.Point(98, 23)
        Me.lblProducto.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(80, 19)
        Me.lblProducto.TabIndex = 30
        Me.lblProducto.Text = "Producto"
        '
        'Panel2
        '
        Me.Panel2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.Navy
        Me.Panel2.Location = New System.Drawing.Point(11, 292)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1350, 1447)
        Me.Panel2.TabIndex = 1
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.OrangeRed
        Me.TextBox1.Location = New System.Drawing.Point(759, 1127)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(310, 180)
        Me.TextBox1.TabIndex = 11
        Me.TextBox1.Text = "Tenga su tarjeta" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "a mano antes de " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     presionar " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "      PAGAR!"
        '
        'PrintDocument1
        '
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem, Me.AyudaToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(1084, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CajaToolStripMenuItem, Me.MantenedorDeProductosToolStripMenuItem, Me.ConfiguraciónToolStripMenuItem, Me.AdministraciónTBKToolStripMenuItem, Me.VentasToolStripMenuItem})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(50, 22)
        Me.MenuToolStripMenuItem.Text = "Menu"
        '
        'CajaToolStripMenuItem
        '
        Me.CajaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AperturaDeCajaToolStripMenuItem})
        Me.CajaToolStripMenuItem.Name = "CajaToolStripMenuItem"
        Me.CajaToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.CajaToolStripMenuItem.Text = "Caja"
        '
        'AperturaDeCajaToolStripMenuItem
        '
        Me.AperturaDeCajaToolStripMenuItem.Name = "AperturaDeCajaToolStripMenuItem"
        Me.AperturaDeCajaToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.AperturaDeCajaToolStripMenuItem.Text = "Apertura/Cierre de Caja"
        '
        'MantenedorDeProductosToolStripMenuItem
        '
        Me.MantenedorDeProductosToolStripMenuItem.Name = "MantenedorDeProductosToolStripMenuItem"
        Me.MantenedorDeProductosToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.MantenedorDeProductosToolStripMenuItem.Text = "Mantenedor de Productos"
        '
        'ConfiguraciónToolStripMenuItem
        '
        Me.ConfiguraciónToolStripMenuItem.Name = "ConfiguraciónToolStripMenuItem"
        Me.ConfiguraciónToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ConfiguraciónToolStripMenuItem.Text = "Configuración"
        '
        'AdministraciónTBKToolStripMenuItem
        '
        Me.AdministraciónTBKToolStripMenuItem.Name = "AdministraciónTBKToolStripMenuItem"
        Me.AdministraciónTBKToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.AdministraciónTBKToolStripMenuItem.Text = "Administración TBK"
        '
        'VentasToolStripMenuItem
        '
        Me.VentasToolStripMenuItem.Name = "VentasToolStripMenuItem"
        Me.VentasToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.VentasToolStripMenuItem.Text = "Ventas"
        '
        'AyudaToolStripMenuItem
        '
        Me.AyudaToolStripMenuItem.Name = "AyudaToolStripMenuItem"
        Me.AyudaToolStripMenuItem.Size = New System.Drawing.Size(53, 22)
        Me.AyudaToolStripMenuItem.Text = "Ayuda"
        '
        'btnsalir
        '
        Me.btnsalir.Location = New System.Drawing.Point(1063, 0)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.Size = New System.Drawing.Size(21, 31)
        Me.btnsalir.TabIndex = 80
        Me.btnsalir.Text = "x"
        Me.btnsalir.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'SerialPort1
        '
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 20000
        '
        'SerialPort2
        '
        '
        'frmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1084, 749)
        Me.Controls.Add(Me.btnsalir)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "miAutoPos"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnLimpiar As Button
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents lblnumTotal As Label
    Friend WithEvents lblsubtotalh As Label
    Friend WithEvents lblPeso As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnPagar As Button
    Friend WithEvents lblValor As Label
    Friend WithEvents lblUnidad As Label
    Friend WithEvents lblProducto As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents MenuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MantenedorDeProductosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CajaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AperturaDeCajaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AyudaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConfiguraciónToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AdministraciónTBKToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VentasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lbltitulo2 As Label
    Friend WithEvents lbltitulo1 As Label
    Friend WithEvents lblUnidades As Label
    Friend WithEvents btnsalir As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Timer2 As Timer
    Friend WithEvents lblcredito As Label
    Friend WithEvents lblcreditot As Label
    Friend WithEvents btnAnular As Button
    Friend WithEvents SerialPort2 As IO.Ports.SerialPort
End Class
