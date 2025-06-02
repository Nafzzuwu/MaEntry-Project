using System;
using System.Data;
using System.Windows.Forms;
using maentry;
using Npgsql;

namespace project_maentry
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            LoadProdi(); // Load data prodi ke ComboBox
        }

        private void LoadProdi()
        {
            try
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT prodi_id, nama_prodi FROM Prodi ORDER BY nama_prodi;";
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Pastikan ComboBox prodi ada di form
                            if (cmbProdi != null)
                            {
                                cmbProdi.Items.Clear();
                                cmbProdi.DisplayMember = "Value";
                                cmbProdi.ValueMember = "Key";

                                while (reader.Read())
                                {
                                    cmbProdi.Items.Add(new KeyValuePair<int, string>(
                                        reader.GetInt32("prodi_id"),
                                        reader.GetString("nama_prodi")
                                    ));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading prodi: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method untuk mencari user_id yang kosong atau mendapatkan ID berikutnya
        private int GetAvailableUserId(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            // Cari user_id terkecil yang tidak digunakan
            string findGapQuery = @"
                SELECT COALESCE(MIN(t1.user_id + 1), 1) as next_id
                FROM (
                    SELECT 0 as user_id
                    UNION ALL
                    SELECT user_id FROM users
                ) t1
                LEFT JOIN users t2 ON t1.user_id + 1 = t2.user_id
                WHERE t2.user_id IS NULL
                AND t1.user_id + 1 > 0";

            using (var cmd = new NpgsqlCommand(findGapQuery, connection, transaction))
            {
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        private void btnrgs_Click(object sender, EventArgs e)
        {
            string nim = txtusername.Text.Trim();
            string password = txtpassword.Text;
            string nama = txtNama.Text.Trim(); // Field nama mahasiswa
            string role = "mahasiswa";

            // Validasi input sederhana
            if (string.IsNullOrEmpty(nim) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nama))
            {
                MessageBox.Show("NIM, nama, dan password tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validasi NIM harus berupa angka
            if (!long.TryParse(nim, out _))
            {
                MessageBox.Show("NIM harus berupa angka saja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validasi panjang NIM dan password
            if (nim.Length < 8 || nim.Length > 12)
            {
                MessageBox.Show("NIM harus antara 8-12 digit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password minimal 6 karakter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validasi prodi dipilih
            if (cmbProdi == null || cmbProdi.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih Program Studi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedProdi = (KeyValuePair<int, string>)cmbProdi.SelectedItem;
            int prodiId = selectedProdi.Key;

            try
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    // Mulai transaksi untuk memastikan konsistensi data
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Periksa apakah NIM sudah ada di tabel Users
                            string checkUserQuery = "SELECT COUNT(*) FROM users WHERE username = @username;";
                            using (var checkCmd = new NpgsqlCommand(checkUserQuery, connection, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@username", nim);
                                int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                                if (exists > 0)
                                {
                                    MessageBox.Show("NIM sudah terdaftar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            // Periksa apakah NIM sudah ada di tabel Mahasiswa
                            string checkMahasiswaQuery = "SELECT COUNT(*) FROM mahasiswa WHERE nim = @nim;";
                            using (var checkCmd = new NpgsqlCommand(checkMahasiswaQuery, connection, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@nim", nim);
                                int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                                if (exists > 0)
                                {
                                    MessageBox.Show("NIM sudah terdaftar sebagai mahasiswa!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            // Dapatkan user_id yang tersedia (mengisi gap atau ID berikutnya)
                            int availableUserId = GetAvailableUserId(connection, transaction);

                            // Insert data ke tabel Users dengan user_id yang spesifik
                            string insertUserQuery = "INSERT INTO users (user_id, username, password, role) VALUES (@user_id, @username, md5(@password), @role);";
                            using (var cmd = new NpgsqlCommand(insertUserQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@user_id", availableUserId);
                                cmd.Parameters.AddWithValue("@username", nim);
                                cmd.Parameters.AddWithValue("@password", password);
                                cmd.Parameters.AddWithValue("@role", role);
                                cmd.ExecuteNonQuery();
                            }

                            // Insert data ke tabel Mahasiswa
                            string insertMahasiswaQuery = "INSERT INTO mahasiswa (nim, nama, user_id, prodi_id) VALUES (@nim, @nama, @user_id, @prodi_id);";
                            using (var cmd = new NpgsqlCommand(insertMahasiswaQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@nim", nim);
                                cmd.Parameters.AddWithValue("@nama", nama);
                                cmd.Parameters.AddWithValue("@user_id", availableUserId);
                                cmd.Parameters.AddWithValue("@prodi_id", prodiId);
                                cmd.ExecuteNonQuery();
                            }

                            // Commit transaksi jika semua berhasil
                            transaction.Commit();

                            MessageBox.Show($"Registrasi berhasil! Silakan login.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Clear form fields
                            txtusername.Text = "";
                            txtpassword.Text = "";
                            txtNama.Text = "";
                            if (cmbProdi != null) cmbProdi.SelectedIndex = -1;

                            Login loginform = new Login();
                            loginform.Show();
                            this.Close();
                        }
                        catch (Exception)
                        {
                            // Rollback jika ada error
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelexitrgs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Clear all form fields
            txtusername.Text = "";
            txtNama.Text = "";
            txtpassword.Text = "";
            if (cmbProdi != null)
            {
                cmbProdi.SelectedIndex = -1;
                cmbProdi.Text = "Pilih Program Studi";
            }
        }

        private void cmbProdi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    // Helper class untuk ComboBox
    public struct KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; }
        public TValue Value { get; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return Value?.ToString() ?? "";
        }
    }
}