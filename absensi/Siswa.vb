Imports absensi.ConnectionDB
Imports MySql.Data.MySqlClient
Public Class Siswa
    Dim conn As New MySqlConnection("Server=localhost; user=root; database=simabsensi")
    Dim cmd As New MySqlCommand
    Dim data As New MySqlDataAdapter

    Dim ds As DataSet

    Sub SetDataGrid()
        Try
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(1).Width = 70
            DataGridView1.Columns(2).Width = 70
            DataGridView1.Columns(3).Width = 70
            DataGridView1.Columns(3).Width = 70
            DataGridView1.Columns(3).Width = 70
            DataGridView1.Columns(3).Width = 70

            DataGridView1.Columns(0).HeaderText = "NIS"
            DataGridView1.Columns(1).HeaderText = "Nama Siswa"
            DataGridView1.Columns(2).HeaderText = "Jenis Kelamin"
            DataGridView1.Columns(3).HeaderText = "Alamat Siswa"
            DataGridView1.Columns(3).HeaderText = "Nama Wali"
            DataGridView1.Columns(3).HeaderText = "Nomor Wali"
            DataGridView1.Columns(3).HeaderText = "ID Kelas"

        Catch ex As Exception
        End Try
    End Sub

    Sub KondisiAwal()
        conn.Open()
        da = New MySqlDataAdapter("select * from tbsiswa", conn)
        ds = New DataSet
        da.Fill(ds, "tbsiswa")
        DataGridView1.DataSource = ds.Tables("tbsiswa")
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        ComboBox1.Text = ""
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Laki-laki")
        ComboBox1.Items.Add("Perempuan")
        conn.Close()
    End Sub

    Private Sub Load_Room_Name(sender As Object, e As EventArgs) Handles MyBase.Load

        Call combobox_idkelas()
    End Sub

    Sub combobox_idkelas()
        conn.Open()
        Dim str As String
        str = "select IDKelas from tbkelas"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            Do While rd.Read
                ComboBox2.Items.Add(rd("IDKelas"))
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
            cmd.CommandText = "INSERT INTO tbsiswa (NIS, NamaSiswa, JenisKelamin, AlamatSiswa,  NamaWali, NomorWali, IDKelas) VALUES ('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & ComboBox1.Text & "', '" & TextBox3.Text & "', '" & TextBox4.Text & "', '" & TextBox5.Text & "', '" & ComboBox2.Text & "')"
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
                TextBox4.Text = .Item(4, baris).Value
                TextBox5.Text = .Item(5, baris).Value
                ComboBox2.Text = .Item(6, baris).Value

                TextBox1.Enabled = True
                TextBox2.Enabled = True
                ComboBox1.Enabled = True
                TextBox3.Enabled = True
                TextBox4.Enabled = True
                TextBox5.Enabled = True
                ComboBox2.Enabled = True
                TextBox2.Focus()

            End With
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conn.Open()
        Try
            Dim str As String
            str = "Update tbsiswa set NIS = '" & TextBox1.Text & "', NamaSiswa = '" & TextBox2.Text & "', JenisKelamin = '" & ComboBox1.Text & "', AlamatSiswa = '" & TextBox3.Text & "', NamaWali = '" & TextBox4.Text & "', NomorWali = '" & TextBox5.Text & "', IDKelas = '" & ComboBox2.Text & "' where NIS = '" & TextBox1.Text & "'"
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
            str = "delete from tbsiswa where NIS = '" & TextBox1.Text & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Delete Siswa Success")
        Catch ex As Exception
            MessageBox.Show("Delete Siswa Failed")
        End Try
        conn.Close()
        Call KondisiAwal()
    End Sub
End Class