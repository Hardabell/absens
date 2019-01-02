Imports System.IO.Ports
Imports System.Threading.Thread
Public Class absen


    Private Tunda As Integer
    Private WithEvents COMport As New SerialPort

    Private Sub form_sms_atcommand_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each COMString As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(COMString)
        Next

        ComboBox1.Sorted = True
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        COMport.PortName = ComboBox1.Text
        COMport.BaudRate = 19200
        COMport.WriteTimeout = 2000

        Try
            COMport.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Sleep(300)   '....tunggu 0.3 second
        Tunda = 300
        Sleep(Tunda)

        Application.DoEvents()

        If COMport.IsOpen Then
            Try
                Dim x As String = "AT+CMGF=1" & Chr(13)
                COMport.Write(x)
                Sleep(Tunda)
                Dim y As String = "AT+CMGS=" & Chr(34) & TextBox1.Text & Chr(34) & Chr(13)
                COMport.Write(y)
                Sleep(Tunda)
                Dim z As String = TextBox2.Text & Chr(26)
                COMport.Write(z)
                Sleep(Tunda)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("COM port tertutup.")
        End If

    End Sub

End Class