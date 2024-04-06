<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoadingPage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.VProgressBar1 = New Guna.UI2.WinForms.Guna2VProgressBar()
        Me.VProgressBar2 = New Guna.UI2.WinForms.Guna2VProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = Global.ComputerShopManagementSys.My.Resources.Resources.c06a35841eafb08e4a171fb974fc1d98
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(361, 396)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Black
        Me.Label5.Font = New System.Drawing.Font("Mongolian Baiti", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(77, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(272, 16)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Computer Shop Billing Management"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Black
        Me.Label1.Font = New System.Drawing.Font("Microsoft Uighur", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(4, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(345, 83)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Computer World"
        '
        'VProgressBar1
        '
        Me.VProgressBar1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.VProgressBar1.Location = New System.Drawing.Point(339, 89)
        Me.VProgressBar1.Name = "VProgressBar1"
        Me.VProgressBar1.ProgressColor = System.Drawing.Color.Yellow
        Me.VProgressBar1.ProgressColor2 = System.Drawing.Color.Navy
        Me.VProgressBar1.Size = New System.Drawing.Size(10, 300)
        Me.VProgressBar1.TabIndex = 20
        Me.VProgressBar1.Text = "VProgressBar1"
        Me.VProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault
        '
        'VProgressBar2
        '
        Me.VProgressBar2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.VProgressBar2.Location = New System.Drawing.Point(12, 89)
        Me.VProgressBar2.Name = "VProgressBar2"
        Me.VProgressBar2.ProgressColor = System.Drawing.Color.Navy
        Me.VProgressBar2.ProgressColor2 = System.Drawing.Color.Yellow
        Me.VProgressBar2.Size = New System.Drawing.Size(10, 300)
        Me.VProgressBar2.TabIndex = 20
        Me.VProgressBar2.Text = "ProgressBar1"
        Me.VProgressBar2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'LoadingPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(361, 396)
        Me.Controls.Add(Me.VProgressBar2)
        Me.Controls.Add(Me.VProgressBar1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LoadingPage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LoadingPage"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents VProgressBar1 As Guna.UI2.WinForms.Guna2VProgressBar
    Friend WithEvents VProgressBar2 As Guna.UI2.WinForms.Guna2VProgressBar
    Friend WithEvents Timer1 As Timer
End Class
