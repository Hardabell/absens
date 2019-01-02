Imports absensi.ConnectionDB
Imports MySql.Data.MySqlClient
Public Class Guru
    Dim conn As New MySqlConnection("Server=localhost; user=root; database=simabsensi")
    Dim cmd As New MySqlCommand
    Dim data As New MySqlDataAdapter

    Dim ds As DataSet

    Sub SetDataGrid()
        Try
            DataGridView1.Columns(0).Width = 130
            DataGridView1.Columns(1).Width = 130
            DataGridView1.Columns(2).Width = 130
            DataGridView1.Columns(3).Width = 130

            DataGridView1.Columns(0).HeaderText = "NIP"
            DataGridView1.Columns(1).HeaderText = "Nama Guru"
            DataGridView1.Columns(2).HeaderText = "Jenis Kelamin"
            DataGridView1.Columns(3).HeaderText = "Nomor Guru"

        Catch ex As Exception
        End Try
    End Sub


    Sub KondisiAwal()
        conn.Open()
        da = New MySqlDataAdapter("select * from tbguru", conn)
        ds = New DataSet
        da.Fill(ds, "tbguru")
        DataGridView1.DataSource = ds.Tables("tbguru")
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox1.Text = ""
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Laki-laki")
        ComboBox1.Items.Add("Perempuan")
        conn.Close()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
        Call SetDataGrid()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        conn.Open()
        Try
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "INSERT INTO tbguru (NIP, NamaGuru, JenisKelamin, NomorGuru) VALUES ('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & ComboBox1.Text & "', '" & TextBox3.Text & "')"
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
                TextBox3.Text = .Item(3, baris).Value

                TextBox1.Enabled = True
                TextBox2.Enabled = True
                ComboBox1.Enabled = True
                TextBox3.Enabled = True
                TextBox2.Focus()

            End With
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conn.Open()
        Try
            Dim str As String
            str = "Update tbguru set NIP = '" & TextBox1.Text & "', NamaGuru = '" & TextBox2.Text & "', JenisKelamin = '" & ComboBox1.Text & "', NomorGuru = '" & TextBox3.Text & "' where NIP = '" & TextBox1.Text & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Update Guru Success")
        Catch ex As Exception
            MessageBox.Show("Update Guru Failed")
        End Try
        conn.Close()
        Call KondisiAwal()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        conn.Open()
        Try
            Dim str As String
            str = "delete from tbguru where NIP = '" & TextBox1.Text & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Delete Guru Success")
        Catch ex As Exception
            MessageBox.Show("Delete Guru Failed")
        End Try
        conn.Close()
        Call KondisiAwal()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub


End Class
