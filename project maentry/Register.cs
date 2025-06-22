using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using maentry;

namespace project_maentry
{
    public partial class Register : Form
    {
        // ABSTRAKSI + POLIMORFISME
        private readonly IUserService _userService;
        private readonly IProdiService _prodiService;

        public Register()
        {
            InitializeComponent();
            _userService = new UserService();
            _prodiService = new ProdiService();

            LoadProdi();
        }

        private void LoadProdi()
        {
            try
            {
                List<KeyValuePair<int, string>> daftarProdi = _prodiService.GetAllProdi();
                if (cmbProdi != null)
                {
                    cmbProdi.Items.Clear();
                    cmbProdi.DisplayMember = "Value";
                    cmbProdi.ValueMember = "Key";

                    foreach (var prodi in daftarProdi)
                    {
                        cmbProdi.Items.Add(prodi);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading prodi: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnrgs_Click(object sender, EventArgs e)
        {
            string nim = txtusername.Text.Trim();
            string password = txtpassword.Text;
            string nama = txtNama.Text.Trim();
            string role = "mahasiswa";

            // VALIDASI INPUT TEXT DULU
            if (string.IsNullOrEmpty(nim) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nama))
            {
                MessageBox.Show("NIM, nama, dan password tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!long.TryParse(nim, out _))
            {
                MessageBox.Show("NIM harus berupa angka saja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nim.Length < 12)
            {
                MessageBox.Show("NIM harus 12 digit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password minimal 6 karakter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // VALIDASI PRODI
            if (cmbProdi == null || cmbProdi.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih Program Studi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedProdi = (KeyValuePair<int, string>)cmbProdi.SelectedItem;
            int prodiId = selectedProdi.Key;

            try
            {
                _userService.RegisterUser(nim, password, nama, role, prodiId);

                MessageBox.Show("Registrasi berhasil! Silakan login.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtusername.Text = "";
                txtpassword.Text = "";
                txtNama.Text = "";
                cmbProdi.SelectedIndex = -1;

                new Login().Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelexitrgs_Click(object sender, EventArgs e) => Application.Exit();

        private void label2_Click(object sender, EventArgs e)
        {
            txtusername.Text = "";
            txtNama.Text = "";
            txtpassword.Text = "";
            if (cmbProdi != null)
            {
                cmbProdi.SelectedIndex = -1;
                cmbProdi.Text = "Pilih Program Studi";
            }
        }

        private void cmbProdi_SelectedIndexChanged(object sender, EventArgs e) { }
    }

    // ===== INTERFACE (POLIMORFISME) =====
    public interface IUserService
    {
        void RegisterUser(string nim, string password, string nama, string role, int prodiId);
    }

    public interface IProdiService
    {
        List<KeyValuePair<int, string>> GetAllProdi();
    }

    // ===== BASE CLASS (INHERITANCE) =====
    public class BaseService
    {
        protected NpgsqlConnection GetConnection()
        {
            return Database.GetConnection();
        }
    }

    // ===== IMPLEMENTASI SERVICE USER (ABSTRAKSI + ENKAPSULASI) =====
    public class UserService : BaseService, IUserService
    {
        public void RegisterUser(string nim, string password, string nama, string role, int prodiId)
        {
            using var conn = GetConnection();
            conn.Open();
            using var trx = conn.BeginTransaction();

            if (UserExists(conn, trx, "mahasiswa", "nama", nama))
                throw new Exception("Nama sudah terdaftar!");

            if (UserExists(conn, trx, "mahasiswa", "nim", nim))
                throw new Exception("NIM sudah terdaftar sebagai mahasiswa!");

            int userId = GetAvailableUserId(conn, trx);

            InsertUser(conn, trx, userId, nim, password, role);
            InsertMahasiswa(conn, trx, nim, nama, userId, prodiId);

            trx.Commit();
        }

        private bool UserExists(NpgsqlConnection conn, NpgsqlTransaction trx, string table, string column, string value)
        {
            string query = $"SELECT COUNT(*) FROM {table} WHERE {column} = @val;";
            using var cmd = new NpgsqlCommand(query, conn, trx);
            cmd.Parameters.AddWithValue("@val", value);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private int GetAvailableUserId(NpgsqlConnection conn, NpgsqlTransaction trx)
        {
            string sql = @"
                SELECT COALESCE(MIN(t1.user_id + 1), 1) as next_id
                FROM (
                    SELECT 0 as user_id
                    UNION ALL
                    SELECT user_id FROM users
                ) t1
                LEFT JOIN users t2 ON t1.user_id + 1 = t2.user_id
                WHERE t2.user_id IS NULL AND t1.user_id + 1 > 0";

            using var cmd = new NpgsqlCommand(sql, conn, trx);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private void InsertUser(NpgsqlConnection conn, NpgsqlTransaction trx, int id, string nim, string password, string role)
        {
            string sql = "INSERT INTO users (user_id, username, password, role) VALUES (@id, @nim, md5(@pass), @role)";
            using var cmd = new NpgsqlCommand(sql, conn, trx);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nim", nim);
            cmd.Parameters.AddWithValue("@pass", password);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.ExecuteNonQuery();
        }

        private void InsertMahasiswa(NpgsqlConnection conn, NpgsqlTransaction trx, string nim, string nama, int userId, int prodiId)
        {
            string sql = "INSERT INTO mahasiswa (nim, nama, user_id, prodi_id) VALUES (@nim, @nama, @uid, @pid)";
            using var cmd = new NpgsqlCommand(sql, conn, trx);
            cmd.Parameters.AddWithValue("@nim", nim);
            cmd.Parameters.AddWithValue("@nama", nama);
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@pid", prodiId);
            cmd.ExecuteNonQuery();
        }
    }

    // ===== IMPLEMENTASI SERVICE PRODI =====
    public class ProdiService : BaseService, IProdiService
    {
        public List<KeyValuePair<int, string>> GetAllProdi()
        {
            var list = new List<KeyValuePair<int, string>>();
            using var conn = GetConnection();
            conn.Open();

            string sql = "SELECT prodi_id, nama_prodi FROM Prodi ORDER BY nama_prodi;";
            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
            }

            return list;
        }
    }

    // ENKAPSULASI: Digunakan untuk menampilkan prodi di ComboBox
    public struct KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; }
        public TValue Value { get; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString() => Value?.ToString() ?? "";
    }
}
