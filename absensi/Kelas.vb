Imports absensi.ConnectionDB
Imports MySql.Data.MySqlClient
Public Class Kelas
    Dim conn As New MySqlConnection("Server=localhost; user=root; database=simabsensi")
    Dim cmd As New MySqlCommand
    Dim data As New MySqlDataAdapter

    Dim ds As DataSet
    Sub SetDataGrid()
        Try
            DataGridView1.Columns(0).Width = 130
            DataGridView1.Columns(1).Width = 130
            DataGridView1.Columns(2).Width = 130

            DataGridView1.Columns(0).HeaderText = "ID Kelas"
            DataGridView1.Columns(1).HeaderText = "Nama Kelas"
            DataGridView1.Columns(2).HeaderText = "NIP Guru"

        Catch ex As Exception
        End Try
    End Sub
    Sub KondisiAwal()
        conn.Open()
        da = New MySqlDataAdapter("select * from tbkelas", conn)
        ds = New DataSet
        da.Fill(ds, "tbkelas")
        DataGridView1.DataSource = ds.Tables("tbkelas")
        TextBox1.Text = ""
        TextBox2.Text = ""
        conn.Close()
    End Sub
    Private Sub Load_Room_Name(sender As Object, e As EventArgs) Handles MyBase.Load

        Call combobox_NIP()
    End Sub

    Sub combobox_NIP()
        conn.Open()
        Dim str As String
        str = "select NIP from tbguru"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            Do While rd.Read
                ComboBox1.Items.Add(rd("NIP"))
            Loop
        Else
        End If
        conn.Close()
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
        Call SetDataGrid()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn.Open()
        Try
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "INSERT INTO tbkelas (IDKelas, NamaKelas, NIP) VALUES ('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & ComboBox1.Text & "')"
            cmd.Connection = conn
            cmd.ExecuteNonQuery()
            MsgBox("Data berhasil disimpan", MsgBoxStyle.Information, "Informasi")

        Catch ex As Exception
            MsgBox("Data gagal disimpan" + ex.Message, MsgBoxStyle.Critical)
        End Try
        conn.Close()
        Call KondisiAwal()
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        If DataGridView1().RowCount > 0 Then
            Dim baris As Integer
            With DataGridView1
                baris = .CurrentRow.Index
                TextBox1.Text = .Item(0, baris).Value
                TextBox2.Text = .Item(1, baris).Value
                ComboBox1.Text = .Item(2, baris).Value

                TextBox1.Enabled = True
                TextBox2.Enabled = True
                ComboBox1.Enabled = True
                TextBox2.Focus()

            End With
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conn.Open()
        Try
            Dim str As String
            str = "Update tbkelas set IDKelas = '" & TextBox1.Text & "', NamaKelas = '" & TextBox2.Text & "', NIP = '" & ComboBox1.Text & "' where IDKelas = '" & TextBox1.Text & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Update Kelas Success")
        Catch ex As Exception
            MessageBox.Show("Update Kelas Failed")
        End Try
        conn.Close()
        Call KondisiAwal()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        conn.Open()
        Try
            Dim str As String
            str = "delete from tbkelas where IDKelas = '" & TextBox1.Text & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Delete Kelas Success")
        Catch ex As Exception
            MessageBox.Show("Delete Kelas Failed")
        End Try
        conn.Close()
        Call KondisiAwal()
    End Sub
End Class