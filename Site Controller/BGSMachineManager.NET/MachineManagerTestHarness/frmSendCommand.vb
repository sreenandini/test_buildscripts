Imports BGSMachineManagerNET
Imports System.Threading
Public Class frmSendCommand
    Dim manualevent As ManualResetEvent
    Dim objMachineManager As New BGSMachineManagerNET.MachineManager
    Private Sub btnEnableMachine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnableMachine.Click
        objMachineManager.EnableMachine(CInt(txtDatapakNo.Text))
        'testevent()
    End Sub

    Private Sub btnDisableMachine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisableMachine.Click
        objMachineManager.DisableMachine(CInt(txtDatapakNo.Text))
    End Sub

    Private Sub btnEnableNA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnableNA.Click
        objMachineManager.EnableNoteAcceptor(CInt(txtDatapakNo.Text))
    End Sub

    Private Sub btnDisableNA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisableNA.Click
        objMachineManager.DisableNoteAcceptor(CInt(txtDatapakNo.Text))
    End Sub

    Private Sub frmSendCommand_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        manualevent = New ManualResetEvent(False)
    End Sub

    Private Sub testevent()
        MessageBox.Show("waiting for manual event to be set")
        manualevent.WaitOne()
        MessageBox.Show("manualevent set")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        objMachineManager.AddUDPToList(Int32.Parse(Txt1.Text), Int32.Parse(Txt3.Text), Int32.Parse(Txt4.Text), 1)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        objMachineManager.SendSector205_Comexchange(Int32.Parse(txt205datapakno.Text), txt205.Text)
        TextBox1.Text = objMachineManager.m_SectorUpdate
    End Sub
End Class
