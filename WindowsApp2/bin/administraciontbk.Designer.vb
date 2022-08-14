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
        Me.SuspendLayout()
        '
        'cmdPolling
        '
        Me.cmdPolling.Location = New System.Drawing.Point(39, 322)
        Me.cmdPolling.Name = "cmdPolling"
        Me.cmdPolling.Size = New System.Drawing.Size(141, 26)
        Me.cmdPolling.TabIndex = 0
        Me.cmdPolling.Text = "Polling"
        Me.cmdPolling.UseVisualStyleBackColor = True
        '
        'cmdManualTbk
        '
        Me.cmdManualTbk.Location = New System.Drawing.Point(39, 287)
        Me.cmdManualTbk.Name = "cmdManualTbk"
        Me.cmdManualTbk.Size = New System.Drawing.Size(141, 29)
        Me.cmdManualTbk.TabIndex = 1
        Me.cmdManualTbk.Text = "Cambio POSTbk Manual"
        Me.cmdManualTbk.UseVisualStyleBackColor = True
        '
        'administraciontbk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.cmdManualTbk)
        Me.Controls.Add(Me.cmdPolling)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "administraciontbk"
        Me.Text = "Administración Transbank"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdPolling As Button
    Friend WithEvents cmdManualTbk As Button
End Class
