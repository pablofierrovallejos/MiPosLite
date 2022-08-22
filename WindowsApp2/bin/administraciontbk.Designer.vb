<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class administraciontbk
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(administraciontbk))
        Me.cmdPolling = New System.Windows.Forms.Button()
        Me.cmdManualTbk = New System.Windows.Forms.Button()
        Me.cmdCargaLlaves = New System.Windows.Forms.Button()
        Me.cmdDetalleVentas = New System.Windows.Forms.Button()
        Me.cmdUltmaVenta = New System.Windows.Forms.Button()
        Me.cmdCierre = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.cmdPrintVentasPos = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdPolling
        '
        Me.cmdPolling.Location = New System.Drawing.Point(8, 322)
        Me.cmdPolling.Name = "cmdPolling"
        Me.cmdPolling.Size = New System.Drawing.Size(144, 40)
        Me.cmdPolling.TabIndex = 0
        Me.cmdPolling.Text = "Polling(OK)"
        Me.cmdPolling.UseVisualStyleBackColor = True
        '
        'cmdManualTbk
        '
        Me.cmdManualTbk.Location = New System.Drawing.Point(8, 271)
        Me.cmdManualTbk.Name = "cmdManualTbk"
        Me.cmdManualTbk.Size = New System.Drawing.Size(144, 40)
        Me.cmdManualTbk.TabIndex = 1
        Me.cmdManualTbk.Text = "Cambio POSTbk Normal (OK)"
        Me.cmdManualTbk.UseVisualStyleBackColor = True
        '
        'cmdCargaLlaves
        '
        Me.cmdCargaLlaves.Location = New System.Drawing.Point(8, 67)
        Me.cmdCargaLlaves.Name = "cmdCargaLlaves"
        Me.cmdCargaLlaves.Size = New System.Drawing.Size(144, 40)
        Me.cmdCargaLlaves.TabIndex = 2
        Me.cmdCargaLlaves.Text = "Carga de Llaves"
        Me.cmdCargaLlaves.UseVisualStyleBackColor = True
        '
        'cmdDetalleVentas
        '
        Me.cmdDetalleVentas.Location = New System.Drawing.Point(8, 169)
        Me.cmdDetalleVentas.Name = "cmdDetalleVentas"
        Me.cmdDetalleVentas.Size = New System.Drawing.Size(144, 40)
        Me.cmdDetalleVentas.TabIndex = 3
        Me.cmdDetalleVentas.Text = "Detalle de Ventas(OK)"
        Me.cmdDetalleVentas.UseVisualStyleBackColor = True
        '
        'cmdUltmaVenta
        '
        Me.cmdUltmaVenta.Location = New System.Drawing.Point(8, 220)
        Me.cmdUltmaVenta.Name = "cmdUltmaVenta"
        Me.cmdUltmaVenta.Size = New System.Drawing.Size(144, 40)
        Me.cmdUltmaVenta.TabIndex = 4
        Me.cmdUltmaVenta.Text = "Ultima Venta(OK)"
        Me.cmdUltmaVenta.UseVisualStyleBackColor = True
        '
        'cmdCierre
        '
        Me.cmdCierre.Location = New System.Drawing.Point(8, 118)
        Me.cmdCierre.Name = "cmdCierre"
        Me.cmdCierre.Size = New System.Drawing.Size(144, 40)
        Me.cmdCierre.TabIndex = 5
        Me.cmdCierre.Text = "Cierre"
        Me.cmdCierre.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(158, 54)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(622, 433)
        Me.TextBox1.TabIndex = 6
        '
        'cmdPrintVentasPos
        '
        Me.cmdPrintVentasPos.Location = New System.Drawing.Point(158, 493)
        Me.cmdPrintVentasPos.Name = "cmdPrintVentasPos"
        Me.cmdPrintVentasPos.Size = New System.Drawing.Size(144, 40)
        Me.cmdPrintVentasPos.TabIndex = 7
        Me.cmdPrintVentasPos.Text = "Imprimir Ventas en POS"
        Me.cmdPrintVentasPos.UseVisualStyleBackColor = True
        '
        'administraciontbk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 583)
        Me.Controls.Add(Me.cmdPrintVentasPos)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.cmdCierre)
        Me.Controls.Add(Me.cmdUltmaVenta)
        Me.Controls.Add(Me.cmdDetalleVentas)
        Me.Controls.Add(Me.cmdCargaLlaves)
        Me.Controls.Add(Me.cmdManualTbk)
        Me.Controls.Add(Me.cmdPolling)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "administraciontbk"
        Me.Text = "Administración Transbank"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdPolling As Button
    Friend WithEvents cmdManualTbk As Button
    Friend WithEvents cmdCargaLlaves As Button
    Friend WithEvents cmdDetalleVentas As Button
    Friend WithEvents cmdUltmaVenta As Button
    Friend WithEvents cmdCierre As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents cmdPrintVentasPos As Button
End Class
