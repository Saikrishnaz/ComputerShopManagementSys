Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class LoadingPage
    Dim i As Integer = 70

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Interval = i
        VProgressBar1.Value = VProgressBar1.Value + 1
        VProgressBar2.Value = VProgressBar2.Value + 1
        If VProgressBar1.Value = 100 And VProgressBar2.Value = 100 Then
            Timer1.Stop()
            LoginForm.Show()
            Me.Hide()
        End If
    End Sub
End Class