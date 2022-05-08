<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SramViewerForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SramViewerForm))
        Me.SramTreeView = New System.Windows.Forms.TreeView()
        Me.ContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyToClipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportGamePatch = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropLabel = New System.Windows.Forms.Label()
        Me.SaveDialog = New System.Windows.Forms.SaveFileDialog()
        Me.ContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'SramTreeView
        '
        Me.SramTreeView.AllowDrop = True
        Me.SramTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SramTreeView.Location = New System.Drawing.Point(0, 0)
        Me.SramTreeView.Name = "SramTreeView"
        Me.SramTreeView.Size = New System.Drawing.Size(800, 450)
        Me.SramTreeView.TabIndex = 0
        '
        'ContextMenu
        '
        Me.ContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyToClipboard, Me.ExportGamePatch})
        Me.ContextMenu.Name = "ContextMenuStrip1"
        Me.ContextMenu.Size = New System.Drawing.Size(246, 48)
        '
        'CopyToClipboard
        '
        Me.CopyToClipboard.Image = Global.XBAND_SRAM_Viewer.My.Resources.Resources.copy
        Me.CopyToClipboard.Name = "CopyToClipboard"
        Me.CopyToClipboard.Size = New System.Drawing.Size(245, 22)
        Me.CopyToClipboard.Text = "Copy selected entry to clipboard"
        '
        'ExportGamePatch
        '
        Me.ExportGamePatch.Image = Global.XBAND_SRAM_Viewer.My.Resources.Resources.save
        Me.ExportGamePatch.Name = "ExportGamePatch"
        Me.ExportGamePatch.Size = New System.Drawing.Size(245, 22)
        Me.ExportGamePatch.Text = "Export GamePatch to .smsg file"
        '
        'DropLabel
        '
        Me.DropLabel.AllowDrop = True
        Me.DropLabel.BackColor = System.Drawing.SystemColors.Window
        Me.DropLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DropLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DropLabel.Location = New System.Drawing.Point(0, 0)
        Me.DropLabel.Name = "DropLabel"
        Me.DropLabel.Size = New System.Drawing.Size(800, 450)
        Me.DropLabel.TabIndex = 1
        Me.DropLabel.Text = "Drop SRAM dump here ..."
        Me.DropLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SaveDialog
        '
        Me.SaveDialog.Filter = "XBAND GamePatch|*.smsg"
        '
        'SramViewerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.DropLabel)
        Me.Controls.Add(Me.SramTreeView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SramViewerForm"
        Me.Text = "XBAND SRAM Viewer"
        Me.ContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SramTreeView As TreeView
    Friend WithEvents ContextMenu As ContextMenuStrip
    Friend WithEvents CopyToClipboard As ToolStripMenuItem
    Friend WithEvents ExportGamePatch As ToolStripMenuItem
    Friend WithEvents DropLabel As Label
    Friend WithEvents SaveDialog As SaveFileDialog
End Class
