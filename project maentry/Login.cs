using System;
using System.Windows.Forms;
using maentry;
using Npgsql;

namespace project_maentry
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text.Trim();
            string password = txtpassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username dan password tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                // Query untuk login dan mendapatkan informasi user
                string query = @"
                    SELECT u.role, u.user_id, m.nim, d.nip 
                    FROM users u
                    LEFT JOIN mahasiswa m ON u.user_id = m.user_id
                    LEFT JOIN dosen d ON u.user_id = d.user_id
                    WHERE u.username = @username AND u.password = md5(@password)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string userRole = reader["role"].ToString();

                            MessageBox.Show("Login berhasil sebagai " + userRole, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Arahkan ke form UI sesuai role
                            if (userRole == "dosen")
                            {
                                new dosenUI().Show();
                            }
                            else if (userRole == "mahasiswa")
                            {
                                // Ambil NIM dari hasil query
                                string nim = reader["nim"]?.ToString() ?? "";

                                if (!string.IsNullOrEmpty(nim))
                                {
                                    new mahasiswaUI(nim).Show();
                                }
                                else
                                {
                                    MessageBox.Show("Data mahasiswa tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Username atau password salah, silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtusername.Clear();
                            txtpassword.Clear();
                            txtusername.Focus();
                        }
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            txtusername.Clear();
            txtpassword.Clear();
            txtusername.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Kosong, bisa digunakan untuk inisialisasi jika perlu
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Register registerform = new Register();
            registerform.Show();
            this.Hide();
        }
    }
}