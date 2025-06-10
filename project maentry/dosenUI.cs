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
using maentry; // Import namespace untuk Database.cs

namespace project_maentry
{
    public partial class dosenUI : Form
    {
        bool sidebarExpand;
        private Timer refreshTimer; // Timer untuk refresh data real time
        private Panel mainPanel; // Panel utama untuk konten
        private DataGridView attendanceGrid; // Grid untuk data kehadiran
        private Label todayLabel; // Label untuk tanggal hari ini
        private Label totalStudentsLabel; // Label total mahasiswa hadir
        private Label statusLabel; // Label status koneksi
        private Label lastUpdateLabel; // Label terakhir update
        private Label summaryLabel; // Label summary statistik

        public dosenUI()
        {
            InitializeComponent();
            InitializeMainPanel();
            InitializeRefreshTimer();
            this.FormClosed += dosenUI_FormClosed;
        }

        private void InitializeMainPanel()
        {
            // Buat main panel untuk konten
            mainPanel = new Panel();
            mainPanel.Location = new Point(sidebar.Width + 10, panel1.Height + 10);
            mainPanel.Size = new Size(this.Width - sidebar.Width - 30, this.Height - panel1.Height - 50);
            mainPanel.BackColor = Color.White;
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Visible = false; // Awalnya hidden
            this.Controls.Add(mainPanel);

            // Header panel dengan gradient background
            Panel headerPanel = new Panel();
            headerPanel.Location = new Point(0, 0);
            headerPanel.Size = new Size(mainPanel.Width, 140);
            headerPanel.BackColor = Color.FromArgb(240, 248, 255);
            headerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            headerPanel.Paint += (s, e) => {
                // Gradient background untuk header
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(240, 248, 255),
                    Color.FromArgb(220, 235, 255),
                    90F))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }
            };
            mainPanel.Controls.Add(headerPanel);

            // Label untuk tanggal hari ini
            todayLabel = new Label();
            todayLabel.Location = new Point(20, 15);
            todayLabel.Size = new Size(600, 35);
            todayLabel.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            todayLabel.Text = "📊 Kehadiran Mahasiswa - " + DateTime.Now.ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
            todayLabel.ForeColor = Color.FromArgb(25, 25, 112);
            todayLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            headerPanel.Controls.Add(todayLabel);

            // Panel untuk statistik
            Panel statsPanel = new Panel();
            statsPanel.Location = new Point(20, 55);
            statsPanel.Size = new Size(headerPanel.Width - 40, 70);
            statsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            headerPanel.Controls.Add(statsPanel);

            // Label untuk total mahasiswa hadir
            totalStudentsLabel = new Label();
            totalStudentsLabel.Location = new Point(0, 0);
            totalStudentsLabel.Size = new Size(300, 30);
            totalStudentsLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            totalStudentsLabel.Text = "👥 Total Mahasiswa Hadir: 0";
            totalStudentsLabel.ForeColor = Color.FromArgb(34, 139, 34);
            statsPanel.Controls.Add(totalStudentsLabel);

            // Summary label untuk statistik detail
            summaryLabel = new Label();
            summaryLabel.Location = new Point(0, 30);
            summaryLabel.Size = new Size(400, 25);
            summaryLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            summaryLabel.Text = "✅ Hadir: 0 | ⚠ Izin: 0 | ❌ Alpa: 0";
            summaryLabel.ForeColor = Color.FromArgb(70, 70, 70);
            statsPanel.Controls.Add(summaryLabel);

            // Status label
            statusLabel = new Label();
            statusLabel.Location = new Point(statsPanel.Width - 250, 0);
            statusLabel.Size = new Size(250, 25);
            statusLabel.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            statusLabel.Text = "🔄 Status: Memuat data...";
            statusLabel.ForeColor = Color.Blue;
            statusLabel.TextAlign = ContentAlignment.MiddleRight;
            statusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            statsPanel.Controls.Add(statusLabel);

            // Last update label
            lastUpdateLabel = new Label();
            lastUpdateLabel.Location = new Point(statsPanel.Width - 250, 25);
            lastUpdateLabel.Size = new Size(250, 20);
            lastUpdateLabel.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lastUpdateLabel.Text = "Terakhir diperbarui: -";
            lastUpdateLabel.ForeColor = Color.Gray;
            lastUpdateLabel.TextAlign = ContentAlignment.MiddleRight;
            lastUpdateLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            statsPanel.Controls.Add(lastUpdateLabel);

            // Panel untuk toolbar
            Panel toolbarPanel = new Panel();
            toolbarPanel.Location = new Point(20, 150);
            toolbarPanel.Size = new Size(mainPanel.Width - 40, 40);
            toolbarPanel.BackColor = Color.FromArgb(248, 248, 248);
            toolbarPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Controls.Add(toolbarPanel);

            // Button refresh manual
            Button refreshButton = new Button();
            refreshButton.Location = new Point(10, 5);
            refreshButton.Size = new Size(100, 30);
            refreshButton.Text = "🔄 Refresh";
            refreshButton.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            refreshButton.BackColor = Color.FromArgb(70, 130, 180);
            refreshButton.ForeColor = Color.White;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.Cursor = Cursors.Hand;
            refreshButton.Click += (s, e) => {
                LoadTodayAttendance();
                MessageBox.Show("Data berhasil diperbarui!", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            toolbarPanel.Controls.Add(refreshButton);

            // Label info auto refresh
            Label autoRefreshLabel = new Label();
            autoRefreshLabel.Location = new Point(120, 10);
            autoRefreshLabel.Size = new Size(200, 20);
            autoRefreshLabel.Text = "⏱ Auto refresh setiap 30 detik";
            autoRefreshLabel.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            autoRefreshLabel.ForeColor = Color.Gray;
            toolbarPanel.Controls.Add(autoRefreshLabel);

            // Button export data
            Button exportButton = new Button();
            exportButton.Location = new Point(330, 5);
            exportButton.Size = new Size(100, 30);
            exportButton.Text = "📊 Export";
            exportButton.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            exportButton.BackColor = Color.FromArgb(34, 139, 34);
            exportButton.ForeColor = Color.White;
            exportButton.FlatStyle = FlatStyle.Flat;
            exportButton.FlatAppearance.BorderSize = 0;
            exportButton.Cursor = Cursors.Hand;
            exportButton.Click += ExportButton_Click;
            toolbarPanel.Controls.Add(exportButton);

            // DataGridView untuk menampilkan data kehadiran
            attendanceGrid = new DataGridView();
            attendanceGrid.Location = new Point(20, 200);
            attendanceGrid.Size = new Size(mainPanel.Width - 40, mainPanel.Height - 230);
            attendanceGrid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            attendanceGrid.BackgroundColor = Color.White;
            attendanceGrid.BorderStyle = BorderStyle.Fixed3D;
            attendanceGrid.AllowUserToAddRows = false;
            attendanceGrid.AllowUserToDeleteRows = false;
            attendanceGrid.ReadOnly = true;
            attendanceGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            attendanceGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            attendanceGrid.MultiSelect = false;
            attendanceGrid.AllowUserToResizeRows = false;

            // Styling untuk header
            attendanceGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
            attendanceGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            attendanceGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            attendanceGrid.ColumnHeadersHeight = 45;
            attendanceGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            attendanceGrid.EnableHeadersVisualStyles = false;

            // Styling untuk rows
            attendanceGrid.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            attendanceGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
            attendanceGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            attendanceGrid.RowsDefaultCellStyle.BackColor = Color.White;
            attendanceGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            attendanceGrid.RowTemplate.Height = 35;

            // Enable grid lines
            attendanceGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            attendanceGrid.GridColor = Color.LightGray;

            // Hover effect
            attendanceGrid.CellMouseEnter += AttendanceGrid_CellMouseEnter;
            attendanceGrid.CellMouseLeave += AttendanceGrid_CellMouseLeave;

            mainPanel.Controls.Add(attendanceGrid);
        }

        private void AttendanceGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                attendanceGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 250);
            }
        }

        private void AttendanceGrid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Reset color berdasarkan status kehadiran
                if (attendanceGrid.Rows[e.RowIndex].Cells["status"] != null &&
                    attendanceGrid.Rows[e.RowIndex].Cells["status"].Value != null)
                {
                    string status = attendanceGrid.Rows[e.RowIndex].Cells["status"].Value.ToString();
                    switch (status.ToLower())
                    {
                        case "hadir":
                            attendanceGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(144, 238, 144);
                            break;
                        case "izin":
                            attendanceGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 224);
                            break;
                        case "alpa":
                            attendanceGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 182, 193);
                            break;
                        default:
                            if (e.RowIndex % 2 == 0)
                                attendanceGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                            else
                                attendanceGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
                            break;
                    }
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (attendanceGrid.Rows.Count == 0)
                {
                    MessageBox.Show("Tidak ada data untuk diekspor!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (.csv)|.csv";
                saveFileDialog.FileName = $"Kehadiran_{DateTime.Now:yyyy-MM-dd}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder csv = new StringBuilder();

                    // Header
                    csv.AppendLine("Nama Mahasiswa,NIM,Program Studi,Mata Kuliah,Tanggal,Waktu,Status");

                    // Data rows
                    foreach (DataGridViewRow row in attendanceGrid.Rows)
                    {
                        if (row.Cells["nama"].Value != null)
                        {
                            csv.AppendLine($"{row.Cells["nama"].Value}," +
                                          $"{row.Cells["nim"].Value}," +
                                          $"{row.Cells["nama_prodi"].Value}," +
                                          $"{row.Cells["nama_matakuliah"].Value}," +
                                          $"{row.Cells["tanggal"].Value}," +
                                          $"{row.Cells["waktu"].Value}," +
                                          $"{row.Cells["status"].Value}");
                        }
                    }

                    System.IO.File.WriteAllText(saveFileDialog.FileName, csv.ToString());
                    MessageBox.Show("Data berhasil diekspor!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat export: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeRefreshTimer()
        {
            // Timer untuk refresh data setiap 30 detik
            refreshTimer = new Timer();
            refreshTimer.Interval = 30000; // 30 detik
            refreshTimer.Tick += RefreshTimer_Tick;
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            // Refresh data kehadiran
            LoadTodayAttendance();
            lastUpdateLabel.Text = "Terakhir diperbarui: " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void dosenUI_Load(object sender, EventArgs e)
        {
            // Setup form
            this.WindowState = FormWindowState.Maximized;

            // Update today label saat form load
            if (todayLabel != null)
            {
                todayLabel.Text = "📊 Kehadiran Mahasiswa - " + DateTime.Now.ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
            }
        }

        private void home_Click(object sender, EventArgs e)
        {
            // Tampilkan main panel dan mulai timer refresh
            mainPanel.Visible = true;
            mainPanel.BringToFront();

            // Load data kehadiran hari ini
            LoadTodayAttendance();

            // Mulai timer untuk refresh real time
            refreshTimer.Start();

            // Update status
            statusLabel.Text = "🟢 Status: Aktif";
            statusLabel.ForeColor = Color.Green;
            lastUpdateLabel.Text = "Terakhir diperbarui: " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void LoadTodayAttendance()
        {
            try
            {
                // Update label tanggal
                todayLabel.Text = "📊 Kehadiran Mahasiswa - " + DateTime.Now.ToString("dddd, dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));

                // Gunakan Database.cs yang sudah ada
                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    // Query yang disesuaikan dengan schema database yang benar
                    string query = @"
                        SELECT 
                            fa.id_absensi,
                            fa.nim,
                            fa.nama_mahasiswa as nama,
                            fa.tanggal,
                            fa.waktu,
                            fa.status,
                            mk.nama_matakuliah,
                            p.nama_prodi
                        FROM Form_Absensi fa
                        LEFT JOIN MataKuliah mk ON fa.matakuliah_id = mk.matakuliah_id
                        LEFT JOIN Prodi p ON mk.prodi_id = p.prodi_id
                        WHERE fa.tanggal = CURRENT_DATE
                        ORDER BY fa.waktu ASC";

                    using (var adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Set data ke DataGridView
                        attendanceGrid.DataSource = dataTable;

                        // Update total mahasiswa hadir dengan emoticon
                        int totalStudents = dataTable.Rows.Count;
                        totalStudentsLabel.Text = $"👥 Total Mahasiswa Hadir: {totalStudents}";
                        totalStudentsLabel.ForeColor = totalStudents > 0 ? Color.FromArgb(34, 139, 34) : Color.Orange;

                        // Hitung statistik kehadiran berdasarkan status yang ada di database
                        int hadir = 0, izin = 0, alpa = 0;

                        foreach (DataRow row in dataTable.Rows)
                        {
                            string status = row["status"].ToString().ToLower();
                            switch (status)
                            {
                                case "hadir":
                                    hadir++;
                                    break;
                                case "izin":
                                    izin++;
                                    break;
                                case "alpa":
                                    alpa++;
                                    break;
                            }
                        }

                        // Update summary label
                        summaryLabel.Text = $"✅ Hadir: {hadir} | ⚠ Izin: {izin} | ❌ Alpa: {alpa}";

                        // Customize column headers jika ada data
                        if (attendanceGrid.Columns.Count > 0)
                        {
                            // Hide ID column
                            if (attendanceGrid.Columns.Contains("id_absensi"))
                                attendanceGrid.Columns["id_absensi"].Visible = false;

                            // Set header text dengan emoji
                            if (attendanceGrid.Columns.Contains("nim"))
                                attendanceGrid.Columns["nim"].HeaderText = "🎓 NIM";
                            if (attendanceGrid.Columns.Contains("nama"))
                                attendanceGrid.Columns["nama"].HeaderText = "📝 Nama Mahasiswa";
                            if (attendanceGrid.Columns.Contains("tanggal"))
                                attendanceGrid.Columns["tanggal"].HeaderText = "📅 Tanggal";
                            if (attendanceGrid.Columns.Contains("waktu"))
                                attendanceGrid.Columns["waktu"].HeaderText = "⏰ Waktu";
                            if (attendanceGrid.Columns.Contains("status"))
                                attendanceGrid.Columns["status"].HeaderText = "✅ Status";
                            if (attendanceGrid.Columns.Contains("nama_matakuliah"))
                                attendanceGrid.Columns["nama_matakuliah"].HeaderText = "📚 Mata Kuliah";
                            if (attendanceGrid.Columns.Contains("nama_prodi"))
                                attendanceGrid.Columns["nama_prodi"].HeaderText = "🏫 Program Studi";

                            // Set column widths proportionally
                            if (attendanceGrid.Columns.Contains("nama"))
                                attendanceGrid.Columns["nama"].FillWeight = 25;
                            if (attendanceGrid.Columns.Contains("nim"))
                                attendanceGrid.Columns["nim"].FillWeight = 15;
                            if (attendanceGrid.Columns.Contains("nama_prodi"))
                                attendanceGrid.Columns["nama_prodi"].FillWeight = 15;
                            if (attendanceGrid.Columns.Contains("nama_matakuliah"))
                                attendanceGrid.Columns["nama_matakuliah"].FillWeight = 20;
                            if (attendanceGrid.Columns.Contains("tanggal"))
                                attendanceGrid.Columns["tanggal"].FillWeight = 12;
                            if (attendanceGrid.Columns.Contains("waktu"))
                                attendanceGrid.Columns["waktu"].FillWeight = 10;
                            if (attendanceGrid.Columns.Contains("status"))
                                attendanceGrid.Columns["status"].FillWeight = 13;

                            // Center align untuk kolom tertentu
                            if (attendanceGrid.Columns.Contains("nim"))
                                attendanceGrid.Columns["nim"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            if (attendanceGrid.Columns.Contains("tanggal"))
                                attendanceGrid.Columns["tanggal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            if (attendanceGrid.Columns.Contains("waktu"))
                                attendanceGrid.Columns["waktu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            if (attendanceGrid.Columns.Contains("status"))
                                attendanceGrid.Columns["status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            // Format waktu dan tanggal
                            if (attendanceGrid.Columns.Contains("waktu"))
                                attendanceGrid.Columns["waktu"].DefaultCellStyle.Format = "HH:mm:ss";
                            if (attendanceGrid.Columns.Contains("tanggal"))
                                attendanceGrid.Columns["tanggal"].DefaultCellStyle.Format = "dd/MM/yyyy";
                        }

                        // Color coding berdasarkan status kehadiran
                        foreach (DataGridViewRow row in attendanceGrid.Rows)
                        {
                            if (row.Cells["status"] != null && row.Cells["status"].Value != null)
                            {
                                string status = row.Cells["status"].Value.ToString().ToLower();
                                switch (status)
                                {
                                    case "hadir":
                                        row.DefaultCellStyle.BackColor = Color.FromArgb(144, 238, 144);
                                        break;
                                    case "izin":
                                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 224);
                                        break;
                                    case "alpa":
                                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 182, 193);
                                        break;
                                }
                            }
                        }

                        // Update status label dengan detail
                        statusLabel.Text = $"🟢 Aktif | ✅{hadir} ⚠{izin} ❌{alpa}";
                        statusLabel.ForeColor = Color.Green;

                        // Show message jika tidak ada data
                        if (totalStudents == 0)
                        {
                            // Buat row untuk pesan "tidak ada data"
                            DataRow emptyRow = dataTable.NewRow();
                            emptyRow["nama"] = "Belum ada mahasiswa yang hadir hari ini";
                            emptyRow["nim"] = "-";
                            emptyRow["nama_prodi"] = "-";
                            emptyRow["nama_matakuliah"] = "-";
                            emptyRow["tanggal"] = DateTime.Now.ToString("yyyy-MM-dd");
                            emptyRow["waktu"] = DBNull.Value;
                            emptyRow["status"] = "Info";
                            dataTable.Rows.Add(emptyRow);

                            attendanceGrid.DataSource = dataTable;
                            attendanceGrid.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                            attendanceGrid.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                            attendanceGrid.Rows[0].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading attendance data: {ex.Message}\n\nDetail: {ex.ToString()}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Set pesan error di label jika koneksi gagal
                totalStudentsLabel.Text = "❌ Error: Tidak dapat terhubung ke database";
                totalStudentsLabel.ForeColor = Color.Red;
                statusLabel.Text = "🔴 Status: Error koneksi";
                statusLabel.ForeColor = Color.Red;
                lastUpdateLabel.Text = "Error pada: " + DateTime.Now.ToString("HH:mm:ss");
                summaryLabel.Text = "❌ Error: Tidak dapat memuat statistik";

                // Clear grid
                attendanceGrid.DataSource = null;
            }
        }

        // Event handlers untuk menu buttons
        private void button8_Click(object sender, EventArgs e)
        {
            // Stop timer jika berpindah ke menu lain
            refreshTimer.Stop();
            mainPanel.Visible = false;
            if (statusLabel != null)
            {
                statusLabel.Text = "⏸ Status: Tidak aktif";
                statusLabel.ForeColor = Color.Orange;
            }
        }

        private void menucontainer_Paint(object sender, PaintEventArgs e)
        {
            // Paint event untuk menu container
        }

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

            // Adjust main panel position ketika sidebar berubah ukuran
            if (mainPanel != null)
            {
                mainPanel.Location = new Point(sidebar.Width + 10, panel1.Height + 10);
                mainPanel.Size = new Size(this.Width - sidebar.Width - 30, this.Height - panel1.Height - 50);
            }
        }

        private void menubutton_Click(object sender, EventArgs e)
        {
            sidebartimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Stop timer sebelum keluar
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
            }
            Application.Exit();
        }

        // Method untuk cleanup timer saat form ditutup
        private void dosenUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
            }
        }

        // Method tambahan untuk kontrol manual
        public void ForceRefreshData()
        {
            if (mainPanel != null && mainPanel.Visible)
            {
                LoadTodayAttendance();
            }
        }

        public void StartMonitoring()
        {
            if (!refreshTimer.Enabled)
            {
                refreshTimer.Start();
                if (statusLabel != null)
                {
                    statusLabel.Text = "🟢 Status: Monitoring aktif";
                    statusLabel.ForeColor = Color.Green;
                }
            }
        }

        public void StopMonitoring()
        {
            if (refreshTimer.Enabled)
            {
                refreshTimer.Stop();
                if (statusLabel != null)
                {
                    statusLabel.Text = "⏸ Status: Monitoring dihentikan";
                    statusLabel.ForeColor = Color.Orange;
                }
            }
        }
    }
}
