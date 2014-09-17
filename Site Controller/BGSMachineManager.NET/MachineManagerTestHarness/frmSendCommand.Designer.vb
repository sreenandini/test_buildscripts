<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSendCommand
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
        Me.lblDatapak = New System.Windows.Forms.Label
        Me.txtDatapakNo = New System.Windows.Forms.TextBox
        Me.btnEnableMachine = New System.Windows.Forms.Button
        Me.btnDisableMachine = New System.Windows.Forms.Button
        Me.btnDisableNA = New System.Windows.Forms.Button
        Me.btnEnableNA = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Txt4 = New System.Windows.Forms.TextBox
        Me.Txt3 = New System.Windows.Forms.TextBox
        Me.Txt2 = New System.Windows.Forms.TextBox
        Me.Txt1 = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txt205 = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txt205datapakno = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDatapak
        '
        Me.lblDatapak.AutoSize = True
        Me.lblDatapak.Location = New System.Drawing.Point(27, 32)
        Me.lblDatapak.Name = "lblDatapak"
        Me.lblDatapak.Size = New System.Drawing.Size(49, 13)
        Me.lblDatapak.TabIndex = 0
        Me.lblDatapak.Text = "GMU No"
        '
        'txtDatapakNo
        '
        Me.txtDatapakNo.Location = New System.Drawing.Point(125, 25)
        Me.txtDatapakNo.Name = "txtDatapakNo"
        Me.txtDatapakNo.Size = New System.Drawing.Size(95, 20)
        Me.txtDatapakNo.TabIndex = 1
        '
        'btnEnableMachine
        '
        Me.btnEnableMachine.Location = New System.Drawing.Point(22, 85)
        Me.btnEnableMachine.Name = "btnEnableMachine"
        Me.btnEnableMachine.Size = New System.Drawing.Size(97, 25)
        Me.btnEnableMachine.TabIndex = 2
        Me.btnEnableMachine.Text = "EnableMachine"
        Me.btnEnableMachine.UseVisualStyleBackColor = True
        '
        'btnDisableMachine
        '
        Me.btnDisableMachine.Location = New System.Drawing.Point(125, 85)
        Me.btnDisableMachine.Name = "btnDisableMachine"
        Me.btnDisableMachine.Size = New System.Drawing.Size(95, 25)
        Me.btnDisableMachine.TabIndex = 3
        Me.btnDisableMachine.Text = "DisableMachine"
        Me.btnDisableMachine.UseVisualStyleBackColor = True
        '
        'btnDisableNA
        '
        Me.btnDisableNA.Location = New System.Drawing.Point(125, 121)
        Me.btnDisableNA.Name = "btnDisableNA"
        Me.btnDisableNA.Size = New System.Drawing.Size(95, 25)
        Me.btnDisableNA.TabIndex = 4
        Me.btnDisableNA.Text = "DisableNA"
        Me.btnDisableNA.UseVisualStyleBackColor = True
        '
        'btnEnableNA
        '
        Me.btnEnableNA.Location = New System.Drawing.Point(22, 121)
        Me.btnEnableNA.Name = "btnEnableNA"
        Me.btnEnableNA.Size = New System.Drawing.Size(97, 25)
        Me.btnEnableNA.TabIndex = 5
        Me.btnEnableNA.Text = "Enable NA"
        Me.btnEnableNA.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Txt4)
        Me.GroupBox1.Controls.Add(Me.Txt3)
        Me.GroupBox1.Controls.Add(Me.Txt2)
        Me.GroupBox1.Controls.Add(Me.Txt1)
        Me.GroupBox1.Location = New System.Drawing.Point(312, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(285, 217)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Add machine to Polling List"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(53, 166)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(175, 28)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Add to Polling List"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(50, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Poll Type"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(50, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Port"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(50, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Position"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(50, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Datapak No"
        '
        'Txt4
        '
        Me.Txt4.Location = New System.Drawing.Point(141, 123)
        Me.Txt4.Name = "Txt4"
        Me.Txt4.Size = New System.Drawing.Size(123, 20)
        Me.Txt4.TabIndex = 3
        '
        'Txt3
        '
        Me.Txt3.Location = New System.Drawing.Point(141, 97)
        Me.Txt3.Name = "Txt3"
        Me.Txt3.Size = New System.Drawing.Size(123, 20)
        Me.Txt3.TabIndex = 2
        '
        'Txt2
        '
        Me.Txt2.Location = New System.Drawing.Point(141, 66)
        Me.Txt2.Name = "Txt2"
        Me.Txt2.Size = New System.Drawing.Size(123, 20)
        Me.Txt2.TabIndex = 1
        '
        'Txt1
        '
        Me.Txt1.Location = New System.Drawing.Point(141, 35)
        Me.Txt1.Name = "Txt1"
        Me.Txt1.Size = New System.Drawing.Size(123, 20)
        Me.Txt1.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txt205datapakno)
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txt205)
        Me.GroupBox2.Location = New System.Drawing.Point(313, 247)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(284, 167)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Sector 205 Call"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Sector 205 Command"
        '
        'txt205
        '
        Me.txt205.Location = New System.Drawing.Point(141, 17)
        Me.txt205.Name = "txt205"
        Me.txt205.Size = New System.Drawing.Size(123, 20)
        Me.txt205.TabIndex = 5
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(52, 93)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(175, 33)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Send Sector 205 Cmd"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Datapak No"
        '
        'txt205datapakno
        '
        Me.txt205datapakno.Location = New System.Drawing.Point(141, 44)
        Me.txt205datapakno.Name = "txt205datapakno"
        Me.txt205datapakno.Size = New System.Drawing.Size(123, 20)
        Me.txt205datapakno.TabIndex = 10
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 154)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(292, 302)
        Me.TextBox1.TabIndex = 8
        '
        'frmSendCommand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(613, 469)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnEnableNA)
        Me.Controls.Add(Me.btnDisableNA)
        Me.Controls.Add(Me.btnDisableMachine)
        Me.Controls.Add(Me.btnEnableMachine)
        Me.Controls.Add(Me.txtDatapakNo)
        Me.Controls.Add(Me.lblDatapak)
        Me.Name = "frmSendCommand"
        Me.Text = "Send Command"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDatapak As System.Windows.Forms.Label
    Friend WithEvents txtDatapakNo As System.Windows.Forms.TextBox
    Friend WithEvents btnEnableMachine As System.Windows.Forms.Button
    Friend WithEvents btnDisableMachine As System.Windows.Forms.Button
    Friend WithEvents btnDisableNA As System.Windows.Forms.Button
    Friend WithEvents btnEnableNA As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Txt4 As System.Windows.Forms.TextBox
    Friend WithEvents Txt3 As System.Windows.Forms.TextBox
    Friend WithEvents Txt2 As System.Windows.Forms.TextBox
    Friend WithEvents Txt1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt205 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt205datapakno As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox

End Class
