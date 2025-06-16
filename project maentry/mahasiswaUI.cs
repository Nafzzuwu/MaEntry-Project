using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace project_maentry
{
    public partial class mahasiswaUI : Form
    {
        bool sidebarExpand;
        private string currentNim;
        private int currentProdiId;

        private Panel contentPanel;
        private Panel homePanel;
        private Panel searchPanel;
        private Panel historyPanel;

        private DataGridView scheduleGrid;
        private DataGridView todayGrid;
        private DataGridView missedGrid;
        private Label timeLabel;
        private Timer timeTimer;

        public mahasiswaUI(string nim)
        {
            InitializeComponent();
            currentNim = nim;
            LoadMahasiswaData();
            InitializeContentPanels();
            ShowHomeContent();

            // Connect button events properly
            ConnectButtonEvents();
        }

        private void ConnectButtonEvents()
        {
            // Connect the buttons to their respective methods
            button3.Click += HomeButton_Click;
            search.Click += SearchButton_Click;
            history.Click += HistoryButton_Click;
        }

        private void LoadMahasiswaData()
        {
            try
            {
                using (NpgsqlConnection conn = maentry.Database.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT prodi_id FROM Mahasiswa WHERE nim = @nim";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nim", currentNim);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            currentProdiId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading mahasiswa data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void InitializeContentPanels()
        {
            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.White;
            this.Controls.Add(contentPanel);
            contentPanel.BringToFront();

            homePanel = CreateHomePanel();
            searchPanel = CreateSearchPanel();
            historyPanel = CreateHistoryPanel();

            contentPanel.Controls.Add(homePanel);
            contentPanel.Controls.Add(searchPanel);
            contentPanel.Controls.Add(historyPanel);
        }

        private Panel CreateHomePanel()
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.White;

            Label titleLabel = new Label();
            titleLabel.Text = "Jadwal Kuliah";
            titleLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(200, 30);
            panel.Controls.Add(titleLabel);

            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            string[] dayLabels = { "Senin", "Selasa", "Rabu", "Kamis", "Jumat" };

            for (int i = 0; i < days.Length; i++)
            {
                Button dayButton = new Button();
                dayButton.Text = dayLabels[i];
                dayButton.Tag = days[i];
                dayButton.Size = new Size(80, 35);
                dayButton.Location = new Point(20 + (i * 90), 70);
                dayButton.BackColor = Color.LightBlue;
                dayButton.FlatStyle = FlatStyle.Flat;
                dayButton.Click += DayButton_Click;
                panel.Controls.Add(dayButton);
            }

            scheduleGrid = new DataGridView();
            scheduleGrid.Location = new Point(20, 120);
            scheduleGrid.Size = new Size(800, 400);
            scheduleGrid.BackgroundColor = Color.White;
            scheduleGrid.BorderStyle = BorderStyle.FixedSingle;
            scheduleGrid.ReadOnly = true;
            scheduleGrid.AllowUserToAddRows = false;
            scheduleGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            scheduleGrid.Columns.Clear();
            scheduleGrid.Columns.Add("Waktu", "Waktu");
            scheduleGrid.Columns.Add("MataKuliah", "Mata Kuliah");
            scheduleGrid.Columns.Add("Dosen", "Dosen");

            panel.Controls.Add(scheduleGrid);

            return panel;
        }

        private Panel CreateSearchPanel()
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.White;

            Label titleLabel = new Label();
            titleLabel.Text = "Absensi Hari Ini";
            titleLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(200, 30);
            panel.Controls.Add(titleLabel);

            timeLabel = new Label();
            timeLabel.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy - HH:mm:ss");
            timeLabel.Font = new Font("Segoe UI", 12);
            timeLabel.Location = new Point(20, 60);
            timeLabel.Size = new Size(400, 25);
            panel.Controls.Add(timeLabel);

            timeTimer = new Timer();
            timeTimer.Interval = 1000;
            timeTimer.Tick += TimeTimer_Tick;

            todayGrid = new DataGridView();
            todayGrid.Location = new Point(20, 100);
            todayGrid.Size = new Size(800, 350);
            todayGrid.BackgroundColor = Color.White;
            todayGrid.BorderStyle = BorderStyle.FixedSingle;
            todayGrid.AllowUserToAddRows = false;
            todayGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            todayGrid.Columns.Clear();
            todayGrid.Columns.Add("JadwalId", "ID");
            todayGrid.Columns["JadwalId"].Visible = false;
            todayGrid.Columns.Add("Waktu", "Waktu");
            todayGrid.Columns.Add("MataKuliah", "Mata Kuliah");
            todayGrid.Columns.Add("Dosen", "Dosen");
            todayGrid.Columns.Add("Status", "Status Absen");

            DataGridViewButtonColumn absenButton = new DataGridViewButtonColumn();
            absenButton.Name = "AbsenButton";
            absenButton.HeaderText = "Aksi";
            absenButton.Text = "Absen";
            absenButton.UseColumnTextForButtonValue = true;
            todayGrid.Columns.Add(absenButton);

            todayGrid.CellClick += TodayGrid_CellClick;
            panel.Controls.Add(todayGrid);

            return panel;
        }

        private Panel CreateHistoryPanel()
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.White;

            Label titleLabel = new Label();
            titleLabel.Text = "Riwayat Absensi";
            titleLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(300, 30);
            panel.Controls.Add(titleLabel);

            missedGrid = new DataGridView();
            missedGrid.Location = new Point(20, 70);
            missedGrid.Size = new Size(800, 450);
            missedGrid.BackgroundColor = Color.White;
            missedGrid.BorderStyle = BorderStyle.FixedSingle;
            missedGrid.ReadOnly = true;
            missedGrid.AllowUserToAddRows = false;
            missedGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            missedGrid.Columns.Clear();
            missedGrid.Columns.Add("Tanggal", "Tanggal");
            missedGrid.Columns.Add("Waktu", "Waktu");
            missedGrid.Columns.Add("MataKuliah", "Mata Kuliah");
            missedGrid.Columns.Add("Dosen", "Dosen");
            missedGrid.Columns.Add("Status", "Status");

            panel.Controls.Add(missedGrid);

            return panel;
        }

        public void HomeButton_Click(object sender, EventArgs e) => ShowHomeContent();
        public void SearchButton_Click(object sender, EventArgs e) => ShowSearchContent();
        public void HistoryButton_Click(object sender, EventArgs e) => ShowHistoryContent();

        private void DayButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string day = btn.Tag.ToString();
            LoadScheduleForDay(day);
        }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            if (timeLabel != null)
                timeLabel.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy - HH:mm:ss");
        }

        private void TodayGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == todayGrid.Columns["AbsenButton"].Index && e.RowIndex >= 0)
            {
                int jadwalId = Convert.ToInt32(todayGrid.Rows[e.RowIndex].Cells["JadwalId"].Value);
                string mataKuliah = todayGrid.Rows[e.RowIndex].Cells["MataKuliah"].Value.ToString();
                string namaDosen = todayGrid.Rows[e.RowIndex].Cells["Dosen"].Value.ToString();
                ShowAttendanceDialog(jadwalId, mataKuliah, namaDosen);
            }
        }

        private void ShowHomeContent()
        {
            homePanel.Visible = true;
            searchPanel.Visible = false;
            historyPanel.Visible = false;

            if (timeTimer != null)
                timeTimer.Stop();

            LoadTodaySchedule();
        }

        private void ShowSearchContent()
        {
            homePanel.Visible = false;
            searchPanel.Visible = true;
            historyPanel.Visible = false;

            if (timeTimer != null)
                timeTimer.Start();

            LoadTodayAttendance();
        }

        private void ShowHistoryContent()
        {
            homePanel.Visible = false;
            searchPanel.Visible = false;
            historyPanel.Visible = true;

            if (timeTimer != null)
                timeTimer.Stop();

            LoadAttendanceHistory();
        }

        private void LoadScheduleForDay(string day)
        {
            try
            {
                using (NpgsqlConnection conn = maentry.Database.GetConnection())
                {
                    conn.Open();
                    // Query yang terintegrasi dengan nama dosen dari tabel MataKuliah
                    string query = @"
                        SELECT 
                            CONCAT(j.jam_mulai, ' - ', j.jam_selesai) as Waktu,
                            mk.nama_matakuliah as MataKuliah,
                            COALESCE(d.nama, 'Belum Ditentukan') as Dosen
                        FROM Jadwal j
                        JOIN MataKuliah mk ON j.matakuliah_id = mk.matakuliah_id
                        LEFT JOIN Dosen d ON mk.nip = d.nip
                        WHERE j.hari = @hari AND mk.prodi_id = @prodi_id
                        ORDER BY j.jam_mulai";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@hari", day);
                        cmd.Parameters.AddWithValue("@prodi_id", currentProdiId);

                        scheduleGrid.Rows.Clear();
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                scheduleGrid.Rows.Add(
                                    reader["Waktu"].ToString(),
                                    reader["MataKuliah"].ToString(),
                                    reader["Dosen"].ToString()
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading schedule: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTodaySchedule()
        {
            string today = DateTime.Now.ToString("dddd", System.Globalization.CultureInfo.InvariantCulture);
            LoadScheduleForDay(today);
        }

        // Perbaikan untuk method LoadTodayAttendance - dengan fitur auto alpa
        private void LoadTodayAttendance()
        {
            try
            {
                string today = DateTime.Now.ToString("dddd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime currentTime = DateTime.Now;

                using (NpgsqlConnection conn = maentry.Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            j.jadwal_id,
                            CONCAT(j.jam_mulai, ' - ', j.jam_selesai) as Waktu,
                            mk.nama_matakuliah as MataKuliah,
                            COALESCE(d.nama, 'Belum Ditentukan') as Dosen,
                            COALESCE(fa.status, 'Belum Absen') as Status,
                            j.jam_mulai,
                            j.jam_selesai
                        FROM Jadwal j
                        JOIN MataKuliah mk ON j.matakuliah_id = mk.matakuliah_id
                        LEFT JOIN Dosen d ON mk.nip = d.nip
                        LEFT JOIN Form_Absensi fa ON fa.matakuliah_id = mk.matakuliah_id 
                            AND fa.nim = @nim AND fa.tanggal = CURRENT_DATE
                        WHERE j.hari = @hari AND mk.prodi_id = @prodi_id
                        ORDER BY j.jam_mulai";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@hari", today);
                        cmd.Parameters.AddWithValue("@prodi_id", currentProdiId);
                        cmd.Parameters.AddWithValue("@nim", currentNim);

                        todayGrid.Rows.Clear();
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TimeSpan jamMulai = TimeSpan.Parse(reader["jam_mulai"].ToString());
                                TimeSpan jamSelesai = TimeSpan.Parse(reader["jam_selesai"].ToString());
                                TimeSpan currentTimeSpan = currentTime.TimeOfDay;

                                string statusAbsen = reader["Status"].ToString();
                                bool canAttend = currentTimeSpan >= jamMulai && currentTimeSpan <= jamSelesai;
                                bool isClassOver = currentTimeSpan > jamSelesai;

                                // Logika untuk mengubah status otomatis
                                if (statusAbsen == "Belum Absen" && isClassOver)
                                {
                                    statusAbsen = "alpa";
                                    // Update status di database juga
                                    UpdateAttendanceStatusToAlpa(Convert.ToInt32(reader["jadwal_id"]));
                                }

                                todayGrid.Rows.Add(
                                    reader["jadwal_id"].ToString(),
                                    reader["Waktu"].ToString(),
                                    reader["MataKuliah"].ToString(),
                                    reader["Dosen"].ToString(),
                                    statusAbsen,
                                    canAttend ? "Absen" : "Tidak Aktif"
                                );

                                // Disable button jika tidak dalam jam kuliah atau sudah absen
                                int rowIndex = todayGrid.Rows.Count - 1;
                                if (!canAttend || statusAbsen != "Belum Absen")
                                {
                                    todayGrid.Rows[rowIndex].Cells["AbsenButton"].Style.BackColor = Color.Gray;
                                    todayGrid.Rows[rowIndex].Cells["AbsenButton"].Style.ForeColor = Color.White;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading today's attendance: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Method baru untuk update status menjadi alpa secara otomatis
        private void UpdateAttendanceStatusToAlpa(int jadwalId)
        {
            try
            {
                using (NpgsqlConnection conn = maentry.Database.GetConnection())
                {
                    conn.Open();

                    // Ambil data mahasiswa, matakuliah, dan dosen
                    string getMahasiswaQuery = @"
                        SELECT 
                            m.nama as nama_mahasiswa,
                            mk.matakuliah_id,
                            mk.nip,
                            COALESCE(d.nama, 'Belum Ditentukan') as nama_dosen
                        FROM Jadwal j
                        JOIN MataKuliah mk ON j.matakuliah_id = mk.matakuliah_id
                        JOIN Mahasiswa m ON m.nim = @nim
                        LEFT JOIN Dosen d ON mk.nip = d.nip
                        WHERE j.jadwal_id = @jadwal_id";

                    string namaMahasiswa = "";
                    int matakuliahId = 0;
                    string nipDosen = "";
                    string namaDosen = "";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(getMahasiswaQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@jadwal_id", jadwalId);
                        cmd.Parameters.AddWithValue("@nim", currentNim);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                namaMahasiswa = reader["nama_mahasiswa"].ToString();
                                matakuliahId = Convert.ToInt32(reader["matakuliah_id"]);
                                nipDosen = reader["nip"]?.ToString() ?? "";
                                namaDosen = reader["nama_dosen"].ToString();
                            }
                        }
                    }

                    if (matakuliahId == 0) return;

                    // Insert/Update status alpa
                    string insertQuery = @"
                        INSERT INTO Form_Absensi (nim, nama_mahasiswa, nip, nama_dosen, tanggal, waktu, status, matakuliah_id)
                        VALUES (@nim, @nama_mahasiswa, @nip, @nama_dosen, @tanggal, @waktu, @status, @matakuliah_id)
                        ON CONFLICT (nim, matakuliah_id, tanggal) 
                        DO UPDATE SET status = @status WHERE Form_Absensi.status = 'Belum Absen'";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@nim", currentNim);
                        cmd.Parameters.AddWithValue("@nama_mahasiswa", namaMahasiswa);

                        if (!string.IsNullOrEmpty(nipDosen))
                        {
                            cmd.Parameters.AddWithValue("@nip", nipDosen);
                            cmd.Parameters.AddWithValue("@nama_dosen", namaDosen);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@nip", DBNull.Value);
                            cmd.Parameters.AddWithValue("@nama_dosen", "Belum Ditentukan");
                        }

                        cmd.Parameters.AddWithValue("@tanggal", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@waktu", DateTime.Now.TimeOfDay);
                        cmd.Parameters.AddWithValue("@status", "alpa");
                        cmd.Parameters.AddWithValue("@matakuliah_id", matakuliahId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error tapi jangan tampilkan ke user karena ini proses otomatis
                Console.WriteLine($"Error updating attendance to alpa: {ex.Message}");
            }
        }

        private void LoadAttendanceHistory()
        {
            try
            {
                using (NpgsqlConnection conn = maentry.Database.GetConnection())
                {
                    conn.Open();

                    // Query sederhana tanpa kompleksitas join jadwal
                    string query = @"
                        SELECT 
                            fa.tanggal,
                            fa.waktu::text as waktu,
                            mk.nama_matakuliah,
                            COALESCE(fa.nama_dosen, d.nama, 'Belum Ditentukan') as nama_dosen,
                            fa.status
                        FROM Form_Absensi fa
                        JOIN MataKuliah mk ON fa.matakuliah_id = mk.matakuliah_id
                        LEFT JOIN Dosen d ON mk.nip = d.nip
                        WHERE fa.nim = @nim
                        ORDER BY fa.tanggal DESC, fa.waktu DESC
                        LIMIT 50";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nim", currentNim);

                        missedGrid.Rows.Clear();
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Format waktu dari TimeSpan ke string yang readable
                                string waktu = reader["waktu"].ToString();
                                if (TimeSpan.TryParse(waktu, out TimeSpan timeSpan))
                                {
                                    waktu = timeSpan.ToString(@"hh\:mm");
                                }

                                missedGrid.Rows.Add(
                                    Convert.ToDateTime(reader["tanggal"]).ToString("dd/MM/yyyy"),
                                    waktu,
                                    reader["nama_matakuliah"].ToString(),
                                    reader["nama_dosen"].ToString(),
                                    reader["status"].ToString()
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading attendance history: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Perbaikan untuk method ShowAttendanceDialog - hanya 2 opsi: Hadir dan Izin
        private void ShowAttendanceDialog(int jadwalId, string mataKuliah, string namaDosen)
        {
            // Cek apakah dalam jam kuliah
            if (!IsInClassTime(jadwalId))
            {
                MessageBox.Show("Absensi hanya dapat dilakukan selama jam kuliah berlangsung!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dialog dengan 3 opsi: Hadir, Izin, dan Sakit
            DialogResult result = MessageBox.Show(
                $"Mata Kuliah: {mataKuliah}\n" +
                $"Dosen: {namaDosen}\n\n" +
                "Pilih status absensi:\n\n" +
                "YES = Hadir\n" +
                "NO = Izin\n" +
                "CANCEL = Sakit",
                "Absensi",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            string status = "";
            switch (result)
            {
                case DialogResult.Yes:
                    status = "hadir";
                    break;
                case DialogResult.No:
                    status = "izin";
                    break;
                case DialogResult.Cancel:
                    status = "sakit";
                    break;
                default:
                    return; // User close dialog with X button
            }

            SaveAttendance(jadwalId, status, namaDosen);
        }

        private bool IsInClassTime(int jadwalId)
        {
            try
            {
                using (NpgsqlConnection conn = maentry.Database.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT jam_mulai, jam_selesai FROM Jadwal WHERE jadwal_id = @jadwal_id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@jadwal_id", jadwalId);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TimeSpan jamMulai = TimeSpan.Parse(reader["jam_mulai"].ToString());
                                TimeSpan jamSelesai = TimeSpan.Parse(reader["jam_selesai"].ToString());
                                TimeSpan currentTime = DateTime.Now.TimeOfDay;

                                return currentTime >= jamMulai && currentTime <= jamSelesai;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking class time: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void SaveAttendance(int jadwalId, string status, string namaDosen)
        {
            try
            {
                using (NpgsqlConnection conn = maentry.Database.GetConnection())
                {
                    conn.Open();

                    // Ambil data matakuliah dan dosen - perbaiki query
                    string getMataKuliahQuery = @"
                        SELECT 
                            mk.matakuliah_id, 
                            mk.nama_matakuliah, 
                            m.nama,
                            mk.nip,
                            COALESCE(d.nama, 'Belum Ditentukan') as nama_dosen
                        FROM Jadwal j
                        JOIN MataKuliah mk ON j.matakuliah_id = mk.matakuliah_id
                        JOIN Mahasiswa m ON m.nim = @nim
                        LEFT JOIN Dosen d ON mk.nip = d.nip
                        WHERE j.jadwal_id = @jadwal_id";

                    int matakuliahId = 0;
                    string namaMataKuliah = "";
                    string namaMahasiswa = "";
                    string nipDosen = "";
                    string namaDosenFromDB = "";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(getMataKuliahQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@jadwal_id", jadwalId);
                        cmd.Parameters.AddWithValue("@nim", currentNim);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                matakuliahId = Convert.ToInt32(reader["matakuliah_id"]);
                                namaMataKuliah = reader["nama_matakuliah"].ToString();
                                namaMahasiswa = reader["nama"].ToString();
                                nipDosen = reader["nip"]?.ToString() ?? "";
                                namaDosenFromDB = reader["nama_dosen"].ToString();
                            }
                        }
                    }

                    if (matakuliahId == 0)
                    {
                        MessageBox.Show("Data jadwal tidak ditemukan!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Insert absensi dengan informasi dosen
                    string insertQuery = @"
                        INSERT INTO Form_Absensi (nim, nama_mahasiswa, nip, nama_dosen, tanggal, waktu, status, matakuliah_id)
                        VALUES (@nim, @nama_mahasiswa, @nip, @nama_dosen, @tanggal, @waktu, @status, @matakuliah_id)
                        ON CONFLICT (nim, matakuliah_id, tanggal) 
                        DO UPDATE SET status = @status, waktu = @waktu, nama_dosen = @nama_dosen, nip = @nip";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@nim", currentNim);
                        cmd.Parameters.AddWithValue("@nama_mahasiswa", namaMahasiswa);

                        // Gunakan NIP dan nama dosen dari database
                        if (!string.IsNullOrEmpty(nipDosen))
                        {
                            cmd.Parameters.AddWithValue("@nip", nipDosen);
                            cmd.Parameters.AddWithValue("@nama_dosen", namaDosenFromDB);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@nip", DBNull.Value);
                            cmd.Parameters.AddWithValue("@nama_dosen", "Belum Ditentukan");
                        }

                        cmd.Parameters.AddWithValue("@tanggal", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@waktu", DateTime.Now.TimeOfDay);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@matakuliah_id", matakuliahId);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Absensi berhasil dicatat dengan status: {status.ToUpper()}",
                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadTodayAttendance();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving attendance: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e) { }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void sidebartransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebartimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebartimer.Stop();
                }
            }
        }
        private void menu_Click(object sender, EventArgs e) => sidebartimer.Start();
        private void logout_Click(object sender, EventArgs e)
        {
            if (timeTimer != null)
                timeTimer.Stop();
            Application.Exit();
        }
    }
}
