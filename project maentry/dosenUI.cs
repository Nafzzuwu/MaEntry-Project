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
        private Panel searchPanel; // Panel untuk search functionality
        private Panel createPanel; // Panel untuk create functionality
        private Panel deletePanel; // Panel untuk delete functionality

        // Home panel components
        private DataGridView attendanceGrid; // Grid untuk data kehadiran
        private Label todayLabel; // Label untuk tanggal hari ini
        private Label totalStudentsLabel; // Label total mahasiswa hadir
        private Label statusLabel; // Label status koneksi
        private Label lastUpdateLabel; // Label terakhir update
        private Label summaryLabel; // Label summary statistik

        // Search panel components
        private TextBox searchTextBox;
        private ComboBox searchFilterComboBox;
        private DataGridView searchResultGrid;
        private Label searchResultLabel;

        // Create panel components
        private ComboBox studentComboBox;
        private ComboBox subjectComboBox;
        private ComboBox statusComboBox;
        private DateTimePicker datePicker;
        private DateTimePicker timePicker;
        private Button saveButton;
        private DataGridView createPreviewGrid;

        // Delete panel components
        private DataGridView deleteGrid;
        private Button deleteButton;
        private Label deleteStatusLabel;

        public dosenUI()
        {
            InitializeComponent();
            InitializeAllPanels();
            InitializeRefreshTimer();
            ConnectSidebarEvents();
            this.FormClosed += dosenUI_FormClosed;
        }

        private void ConnectSidebarEvents()
        {
            // Sesuaikan dengan nama button yang ada di form designer
            search.Click += search_Click;
            create.Click += create_Click;
            delete.Click += delete_Click;
        }

        private void InitializeAllPanels()
        {
            InitializeMainPanel();
            InitializeSearchPanel();
            InitializeCreatePanel();
            InitializeDeletePanel();
        }

        private void InitializeMainPanel()
        {
            // Buat main panel untuk konten HOME
            mainPanel = new Panel();
            mainPanel.Location = new Point(sidebar.Width + 10, panel1.Height + 10);
            mainPanel.Size = new Size(this.Width - sidebar.Width - 30, this.Height - panel1.Height - 50);
            mainPanel.BackColor = Color.White;
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Visible = false;
            this.Controls.Add(mainPanel);

            // Header panel dengan gradient background
            Panel headerPanel = new Panel();
            headerPanel.Location = new Point(0, 0);
            headerPanel.Size = new Size(mainPanel.Width, 140);
            headerPanel.BackColor = Color.FromArgb(240, 248, 255);
            headerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            headerPanel.Paint += (s, e) => {
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
            SetupDataGridView(attendanceGrid);
            mainPanel.Controls.Add(attendanceGrid);
        }

        private void InitializeSearchPanel()
        {
            // Panel untuk SEARCH functionality
            searchPanel = new Panel();
            searchPanel.Location = new Point(sidebar.Width + 10, panel1.Height + 10);
            searchPanel.Size = new Size(this.Width - sidebar.Width - 30, this.Height - panel1.Height - 50);
            searchPanel.BackColor = Color.White;
            searchPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            searchPanel.BorderStyle = BorderStyle.FixedSingle;
            searchPanel.Visible = false;
            this.Controls.Add(searchPanel);

            // Header untuk Search
            Label searchHeaderLabel = new Label();
            searchHeaderLabel.Location = new Point(20, 20);
            searchHeaderLabel.Size = new Size(400, 40);
            searchHeaderLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            searchHeaderLabel.Text = "🔍 Pencarian Data Mahasiswa";
            searchHeaderLabel.ForeColor = Color.FromArgb(25, 25, 112);
            searchPanel.Controls.Add(searchHeaderLabel);

            // Panel untuk search controls
            Panel searchControlPanel = new Panel();
            searchControlPanel.Location = new Point(20, 70);
            searchControlPanel.Size = new Size(searchPanel.Width - 40, 100);
            searchControlPanel.BackColor = Color.FromArgb(248, 248, 248);
            searchControlPanel.BorderStyle = BorderStyle.FixedSingle;
            searchControlPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            searchPanel.Controls.Add(searchControlPanel);

            // Label untuk search
            Label searchLabel = new Label();
            searchLabel.Location = new Point(15, 15);
            searchLabel.Size = new Size(120, 25);
            searchLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            searchLabel.Text = "🔎 Cari berdasarkan:";
            searchControlPanel.Controls.Add(searchLabel);

            // ComboBox untuk filter pencarian
            searchFilterComboBox = new ComboBox();
            searchFilterComboBox.Location = new Point(140, 15);
            searchFilterComboBox.Size = new Size(150, 25);
            searchFilterComboBox.Font = new Font("Segoe UI", 10);
            searchFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            searchFilterComboBox.Items.AddRange(new string[] { "Semua", "Nama", "NIM", "Program Studi", "Mata Kuliah", "Status" });
            searchFilterComboBox.SelectedIndex = 0;
            searchControlPanel.Controls.Add(searchFilterComboBox);

            // TextBox untuk input pencarian
            searchTextBox = new TextBox();
            searchTextBox.Location = new Point(310, 15);
            searchTextBox.Size = new Size(200, 25);
            searchTextBox.Font = new Font("Segoe UI", 10);
            searchTextBox.PlaceholderText = "Masukkan kata kunci...";
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
            searchControlPanel.Controls.Add(searchTextBox);

            // Button untuk search
            Button searchButton = new Button();
            searchButton.Location = new Point(530, 15);
            searchButton.Size = new Size(80, 25);
            searchButton.Text = "Cari";
            searchButton.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            searchButton.BackColor = Color.FromArgb(70, 130, 180);
            searchButton.ForeColor = Color.White;
            searchButton.FlatStyle = FlatStyle.Flat;
            searchButton.FlatAppearance.BorderSize = 0;
            searchButton.Cursor = Cursors.Hand;
            searchButton.Click += SearchButton_Click;
            searchControlPanel.Controls.Add(searchButton);

            // Button untuk reset search
            Button resetSearchButton = new Button();
            resetSearchButton.Location = new Point(620, 15);
            resetSearchButton.Size = new Size(80, 25);
            resetSearchButton.Text = "Reset";
            resetSearchButton.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            resetSearchButton.BackColor = Color.FromArgb(255, 140, 0);
            resetSearchButton.ForeColor = Color.White;
            resetSearchButton.FlatStyle = FlatStyle.Flat;
            resetSearchButton.FlatAppearance.BorderSize = 0;
            resetSearchButton.Cursor = Cursors.Hand;
            resetSearchButton.Click += ResetSearchButton_Click;
            searchControlPanel.Controls.Add(resetSearchButton);

            // Label untuk hasil pencarian
            searchResultLabel = new Label();
            searchResultLabel.Location = new Point(15, 50);
            searchResultLabel.Size = new Size(400, 25);
            searchResultLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            searchResultLabel.Text = "📊 Hasil pencarian: 0 data ditemukan";
            searchResultLabel.ForeColor = Color.FromArgb(70, 70, 70);
            searchControlPanel.Controls.Add(searchResultLabel);

            // DataGridView untuk hasil pencarian
            searchResultGrid = new DataGridView();
            searchResultGrid.Location = new Point(20, 180);
            searchResultGrid.Size = new Size(searchPanel.Width - 40, searchPanel.Height - 210);
            searchResultGrid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            SetupDataGridView(searchResultGrid);
            searchPanel.Controls.Add(searchResultGrid);
        }


        private void SearchResultGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex].Name == "Edit")
            {
                var row = grid.Rows[e.RowIndex];

                int idAbsensi = Convert.ToInt32(row.Cells["ID"].Value);
                string currentStatus = row.Cells["Status"].Value.ToString();

                string[] options = { "hadir", "izin", "sakit", "alpa" };
                string newStatus = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Status saat ini: {currentStatus}\n\nMasukkan status baru (hadir/izin/sakit/alpa):",
                    "Ubah Status Kehadiran",
                    currentStatus);

                if (!string.IsNullOrEmpty(newStatus) && options.Contains(newStatus.ToLower()))
                {
                    try
                    {
                        using (var conn = Database.GetConnection())
                        {
                            conn.Open();
                            string updateQuery = "UPDATE Form_Absensi SET status = @status WHERE id_absensi = @id";
                            using (var cmd = new NpgsqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@status", newStatus.ToLower());
                                cmd.Parameters.AddWithValue("@id", idAbsensi);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Status berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PerformSearch();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal memperbarui status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!string.IsNullOrEmpty(newStatus))
                {
                    MessageBox.Show("Status tidak valid. Gunakan: hadir, izin, sakit, atau alpa.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void InitializeCreatePanel()
        {
            // Panel untuk CREATE functionality
            createPanel = new Panel();
            createPanel.Location = new Point(sidebar.Width + 10, panel1.Height + 10);
            createPanel.Size = new Size(this.Width - sidebar.Width - 30, this.Height - panel1.Height - 50);
            createPanel.BackColor = Color.White;
            createPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            createPanel.BorderStyle = BorderStyle.FixedSingle;
            createPanel.Visible = false;
            this.Controls.Add(createPanel);

            // Header untuk Create
            Label createHeaderLabel = new Label();
            createHeaderLabel.Location = new Point(20, 20);
            createHeaderLabel.Size = new Size(500, 40);
            createHeaderLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            createHeaderLabel.Text = "➕ Tambah Data Absensi";
            createHeaderLabel.ForeColor = Color.FromArgb(25, 25, 112);
            createPanel.Controls.Add(createHeaderLabel);

            // Panel untuk form input
            Panel inputPanel = new Panel();
            inputPanel.Location = new Point(20, 70);
            inputPanel.Size = new Size(createPanel.Width - 40, 200);
            inputPanel.BackColor = Color.FromArgb(248, 248, 248);
            inputPanel.BorderStyle = BorderStyle.FixedSingle;
            inputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            createPanel.Controls.Add(inputPanel);

            // Form controls
            int yPos = 20;
            int spacing = 35;

            // Mahasiswa selection
            Label studentLabel = new Label();
            studentLabel.Location = new Point(20, yPos);
            studentLabel.Size = new Size(120, 25);
            studentLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            studentLabel.Text = "👨‍🎓 Mahasiswa:";
            inputPanel.Controls.Add(studentLabel);

            studentComboBox = new ComboBox();
            studentComboBox.Location = new Point(150, yPos);
            studentComboBox.Size = new Size(300, 25);
            studentComboBox.Font = new Font("Segoe UI", 10);
            studentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            inputPanel.Controls.Add(studentComboBox);

            yPos += spacing;

            // Mata kuliah selection
            Label subjectLabel = new Label();
            subjectLabel.Location = new Point(20, yPos);
            subjectLabel.Size = new Size(120, 25);
            subjectLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            subjectLabel.Text = "📚 Mata Kuliah:";
            inputPanel.Controls.Add(subjectLabel);

            subjectComboBox = new ComboBox();
            subjectComboBox.Location = new Point(150, yPos);
            subjectComboBox.Size = new Size(300, 25);
            subjectComboBox.Font = new Font("Segoe UI", 10);
            subjectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            inputPanel.Controls.Add(subjectComboBox);

            yPos += spacing;

            // Status selection
            Label statusLabel = new Label();
            statusLabel.Location = new Point(20, yPos);
            statusLabel.Size = new Size(120, 25);
            statusLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            statusLabel.Text = "✅ Status:";
            inputPanel.Controls.Add(statusLabel);

            statusComboBox = new ComboBox();
            statusComboBox.Location = new Point(150, yPos);
            statusComboBox.Size = new Size(150, 25);
            statusComboBox.Font = new Font("Segoe UI", 10);
            statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            statusComboBox.Items.AddRange(new string[] { "hadir", "izin", "sakit", "alpa" });
            statusComboBox.SelectedIndex = 0;
            inputPanel.Controls.Add(statusComboBox);

            yPos += spacing;

            // Date selection
            Label dateLabel = new Label();
            dateLabel.Location = new Point(20, yPos);
            dateLabel.Size = new Size(120, 25);
            dateLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            dateLabel.Text = "📅 Tanggal:";
            inputPanel.Controls.Add(dateLabel);

            datePicker = new DateTimePicker();
            datePicker.Location = new Point(150, yPos);
            datePicker.Size = new Size(150, 25);
            datePicker.Font = new Font("Segoe UI", 10);
            datePicker.Format = DateTimePickerFormat.Short;
            datePicker.Value = DateTime.Now;
            inputPanel.Controls.Add(datePicker);

            // Time selection
            Label timeLabel = new Label();
            timeLabel.Location = new Point(320, yPos);
            timeLabel.Size = new Size(60, 25);
            timeLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            timeLabel.Text = "⏰ Waktu:";
            inputPanel.Controls.Add(timeLabel);

            timePicker = new DateTimePicker();
            timePicker.Location = new Point(390, yPos);
            timePicker.Size = new Size(100, 25);
            timePicker.Font = new Font("Segoe UI", 10);
            timePicker.Format = DateTimePickerFormat.Time;
            timePicker.ShowUpDown = true;
            timePicker.Value = DateTime.Now;
            inputPanel.Controls.Add(timePicker);

            yPos += spacing + 10;

            // Buttons
            saveButton = new Button();
            saveButton.Location = new Point(150, yPos);
            saveButton.Size = new Size(100, 35);
            saveButton.Text = "💾 Simpan";
            saveButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            saveButton.BackColor = Color.FromArgb(34, 139, 34);
            saveButton.ForeColor = Color.White;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.Cursor = Cursors.Hand;
            saveButton.Click += SaveButton_Click;
            inputPanel.Controls.Add(saveButton);

            Button clearButton = new Button();
            clearButton.Location = new Point(260, yPos);
            clearButton.Size = new Size(100, 35);
            clearButton.Text = "🗑 Bersihkan";
            clearButton.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            clearButton.BackColor = Color.FromArgb(220, 20, 60);
            clearButton.ForeColor = Color.White;
            clearButton.FlatStyle = FlatStyle.Flat;
            clearButton.FlatAppearance.BorderSize = 0;
            clearButton.Cursor = Cursors.Hand;
            clearButton.Click += ClearButton_Click;
            inputPanel.Controls.Add(clearButton);

            // Preview grid
            Label previewLabel = new Label();
            previewLabel.Location = new Point(20, 290);
            previewLabel.Size = new Size(300, 30);
            previewLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            previewLabel.Text = "📋 Preview Data Absensi";
            previewLabel.ForeColor = Color.FromArgb(25, 25, 112);
            createPanel.Controls.Add(previewLabel);

            createPreviewGrid = new DataGridView();
            createPreviewGrid.Location = new Point(20, 320);
            createPreviewGrid.Size = new Size(createPanel.Width - 40, createPanel.Height - 350);
            createPreviewGrid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            SetupDataGridView(createPreviewGrid);
            createPanel.Controls.Add(createPreviewGrid);

            // Load initial data
            LoadStudentsForComboBox();
            LoadSubjectsForComboBox();
            LoadCreatePreviewData();
        }

        private void InitializeDeletePanel()
        {
            // Panel untuk DELETE functionality
            deletePanel = new Panel();
            deletePanel.Location = new Point(sidebar.Width + 10, panel1.Height + 10);
            deletePanel.Size = new Size(this.Width - sidebar.Width - 30, this.Height - panel1.Height - 50);
            deletePanel.BackColor = Color.White;
            deletePanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            deletePanel.BorderStyle = BorderStyle.FixedSingle;
            deletePanel.Visible = false;
            this.Controls.Add(deletePanel);

            // Header untuk Delete
            Label deleteHeaderLabel = new Label();
            deleteHeaderLabel.Location = new Point(20, 20);
            deleteHeaderLabel.Size = new Size(400, 40);
            deleteHeaderLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            deleteHeaderLabel.Text = "🗑 Hapus Data Absensi";
            deleteHeaderLabel.ForeColor = Color.FromArgb(220, 20, 60);
            deletePanel.Controls.Add(deleteHeaderLabel);

            // Warning label
            Label warningLabel = new Label();
            warningLabel.Location = new Point(20, 70);
            warningLabel.Size = new Size(600, 30);
            warningLabel.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            warningLabel.Text = "⚠ Pilih data yang ingin dihapus dari tabel di bawah ini";
            warningLabel.ForeColor = Color.FromArgb(255, 140, 0);
            deletePanel.Controls.Add(warningLabel);

            // Control panel
            Panel deleteControlPanel = new Panel();
            deleteControlPanel.Location = new Point(20, 110);
            deleteControlPanel.Size = new Size(deletePanel.Width - 40, 60);
            deleteControlPanel.BackColor = Color.FromArgb(248, 248, 248);
            deleteControlPanel.BorderStyle = BorderStyle.FixedSingle;
            deleteControlPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            deletePanel.Controls.Add(deleteControlPanel);

            // Delete button
            deleteButton = new Button();
            deleteButton.Location = new Point(15, 15);
            deleteButton.Size = new Size(120, 30);
            deleteButton.Text = "🗑 Hapus Terpilih";
            deleteButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            deleteButton.BackColor = Color.FromArgb(220, 20, 60);
            deleteButton.ForeColor = Color.White;
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.Cursor = Cursors.Hand;
            deleteButton.Click += DeleteButton_Click;
            deleteControlPanel.Controls.Add(deleteButton);

            // Refresh delete data button
            Button refreshDeleteButton = new Button();
            refreshDeleteButton.Location = new Point(145, 15);
            refreshDeleteButton.Size = new Size(100, 30);
            refreshDeleteButton.Text = "🔄 Refresh";
            refreshDeleteButton.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            refreshDeleteButton.BackColor = Color.FromArgb(70, 130, 180);
            refreshDeleteButton.ForeColor = Color.White;
            refreshDeleteButton.FlatStyle = FlatStyle.Flat;
            refreshDeleteButton.FlatAppearance.BorderSize = 0;
            refreshDeleteButton.Cursor = Cursors.Hand;
            refreshDeleteButton.Click += (s, e) => LoadDeleteData();
            deleteControlPanel.Controls.Add(refreshDeleteButton);

            // Status label untuk delete
            deleteStatusLabel = new Label();
            deleteStatusLabel.Location = new Point(260, 20);
            deleteStatusLabel.Size = new Size(300, 20);
            deleteStatusLabel.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            deleteStatusLabel.Text = "📊 Pilih baris data untuk menghapus";
            deleteStatusLabel.ForeColor = Color.FromArgb(70, 70, 70);
            deleteControlPanel.Controls.Add(deleteStatusLabel);

            // DataGridView untuk data yang bisa dihapus
            deleteGrid = new DataGridView();
            deleteGrid.Location = new Point(20, 180);
            deleteGrid.Size = new Size(deletePanel.Width - 40, deletePanel.Height - 210);
            deleteGrid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            SetupDataGridView(deleteGrid);
            deleteGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            deleteGrid.MultiSelect = true;
            deleteGrid.SelectionChanged += DeleteGrid_SelectionChanged;
            deletePanel.Controls.Add(deleteGrid);
        }

        private void SetupDataGridView(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.Fixed3D;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.MultiSelect = false;
            grid.AllowUserToResizeRows = false;

            // Styling untuk header
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            grid.ColumnHeadersHeight = 45;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.EnableHeadersVisualStyles = false;

            // Styling untuk rows
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            // Styling untuk rows (lanjutan dari kode sebelumnya)
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.Black;
            grid.RowTemplate.Height = 35;
            grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Event handlers
            grid.CellFormatting += Grid_CellFormatting;
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid.Columns[e.ColumnIndex].Name == "status" && e.Value != null)
            {
                string status = e.Value.ToString().ToLower();
                switch (status)
                {
                    case "hadir":
                        e.CellStyle.BackColor = Color.FromArgb(144, 238, 144);
                        e.CellStyle.ForeColor = Color.FromArgb(0, 100, 0);
                        break;
                    case "izin":
                        e.CellStyle.BackColor = Color.FromArgb(255, 255, 224);
                        e.CellStyle.ForeColor = Color.FromArgb(255, 140, 0);
                        break;
                    case "sakit":
                        e.CellStyle.BackColor = Color.FromArgb(255, 228, 225);
                        e.CellStyle.ForeColor = Color.FromArgb(255, 69, 0);
                        break;
                    case "alpa":
                        e.CellStyle.BackColor = Color.FromArgb(255, 182, 193);
                        e.CellStyle.ForeColor = Color.FromArgb(139, 0, 0);
                        break;
                }
            }
        }

        private void InitializeRefreshTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 30000; // 30 detik
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (mainPanel.Visible) // Hanya refresh jika panel home sedang aktif
            {
                LoadTodayAttendance();
            }
        }

        private void LoadTodayAttendance()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            fa.id_absensi as ""ID"",
                            m.nim as ""NIM"",
                            COALESCE(fa.nama_mahasiswa, m.nama) as ""Nama Mahasiswa"",
                            mk.nama_matakuliah as ""Mata Kuliah"",
                            p.nama_prodi as ""Program Studi"",
                            fa.tanggal as ""Tanggal"",
                            fa.waktu as ""Waktu"",
                            fa.status as ""Status""
                        FROM Form_Absensi fa
                        JOIN Mahasiswa m ON fa.nim = m.nim
                        JOIN MataKuliah mk ON fa.matakuliah_id = mk.matakuliah_id
                        JOIN Prodi p ON mk.prodi_id = p.prodi_id
                        WHERE fa.tanggal = @tanggal
                        ORDER BY fa.waktu DESC, m.nama ASC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tanggal", DateTime.Now.Date);

                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            attendanceGrid.DataSource = dt;

                            // Update statistik
                            UpdateStatistics(dt);

                            statusLabel.Text = "✅ Status: Data terbaru";
                            statusLabel.ForeColor = Color.Green;
                            lastUpdateLabel.Text = "Terakhir diperbarui: " + DateTime.Now.ToString("HH:mm:ss");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = "❌ Status: Error - " + ex.Message;
                statusLabel.ForeColor = Color.Red;
                MessageBox.Show("Error saat memuat data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatistics(DataTable dt)
        {
            int totalHadir = 0, totalIzin = 0, totalSakit = 0, totalAlpa = 0;

            foreach (DataRow row in dt.Rows)
            {
                string status = row["Status"].ToString().ToLower();
                switch (status)
                {
                    case "hadir": totalHadir++; break;
                    case "izin": totalIzin++; break;
                    case "sakit": totalSakit++; break;
                    case "alpa": totalAlpa++; break;
                }
            }

            totalStudentsLabel.Text = $"👥 Total Mahasiswa Hadir: {totalHadir}";
            summaryLabel.Text = $"✅ Hadir: {totalHadir} | 📋 Izin: {totalIzin} | 🏥 Sakit: {totalSakit} | ❌ Alpa: {totalAlpa}";
        }

        private void LoadStudentsForComboBox()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT m.nim, m.nama, p.nama_prodi 
                        FROM Mahasiswa m 
                        JOIN Prodi p ON m.prodi_id = p.prodi_id 
                        ORDER BY m.nama";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        studentComboBox.Items.Clear();
                        while (reader.Read())
                        {
                            string displayText = $"{reader["nama"]} ({reader["nim"]}) - {reader["nama_prodi"]}";
                            studentComboBox.Items.Add(new ComboBoxItem
                            {
                                Text = displayText,
                                Value = reader["nim"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSubjectsForComboBox()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT mk.matakuliah_id, mk.nama_matakuliah, p.nama_prodi 
                        FROM MataKuliah mk 
                        JOIN Prodi p ON mk.prodi_id = p.prodi_id 
                        ORDER BY mk.nama_matakuliah";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        subjectComboBox.Items.Clear();
                        while (reader.Read())
                        {
                            string displayText = $"{reader["nama_matakuliah"]} - {reader["nama_prodi"]}";
                            subjectComboBox.Items.Add(new ComboBoxItem
                            {
                                Text = displayText,
                                Value = reader["matakuliah_id"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading subjects: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCreatePreviewData()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            fa.id_absensi as ""ID"",
                            m.nim as ""NIM"",
                            COALESCE(fa.nama_mahasiswa, m.nama) as ""Nama Mahasiswa"",
                            mk.nama_matakuliah as ""Mata Kuliah"",
                            fa.tanggal as ""Tanggal"",
                            fa.waktu as ""Waktu"",
                            fa.status as ""Status""
                        FROM Form_Absensi fa
                        JOIN Mahasiswa m ON fa.nim = m.nim
                        JOIN MataKuliah mk ON fa.matakuliah_id = mk.matakuliah_id
                        ORDER BY fa.tanggal DESC, fa.waktu DESC
                        LIMIT 50";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        createPreviewGrid.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading preview data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDeleteData()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            fa.id_absensi as ""ID"",
                            m.nim as ""NIM"",
                            COALESCE(fa.nama_mahasiswa, m.nama) as ""Nama Mahasiswa"",
                            mk.nama_matakuliah as ""Mata Kuliah"",
                            p.nama_prodi as ""Program Studi"",
                            fa.tanggal as ""Tanggal"",
                            fa.waktu as ""Waktu"",
                            fa.status as ""Status""
                        FROM Form_Absensi fa
                        JOIN Mahasiswa m ON fa.nim = m.nim
                        JOIN MataKuliah mk ON fa.matakuliah_id = mk.matakuliah_id
                        JOIN Prodi p ON mk.prodi_id = p.prodi_id
                        ORDER BY fa.tanggal DESC, fa.waktu DESC";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        deleteGrid.DataSource = dt;

                        deleteStatusLabel.Text = $"📊 Total {dt.Rows.Count} data tersedia";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading delete data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event Handlers
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (searchTextBox.Text.Length >= 3 || string.IsNullOrEmpty(searchTextBox.Text))
            {
                PerformSearch();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void ResetSearchButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            searchFilterComboBox.SelectedIndex = 0;
            searchResultGrid.DataSource = null;
            searchResultLabel.Text = "📊 Hasil pencarian: 0 data ditemukan";
        }

        private void PerformSearch()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string baseQuery = @"
                        SELECT 
                            fa.id_absensi as ""ID"",
                            m.nim as ""NIM"",
                            COALESCE(fa.nama_mahasiswa, m.nama) as ""Nama Mahasiswa"",
                            mk.nama_matakuliah as ""Mata Kuliah"",
                            p.nama_prodi as ""Program Studi"",
                            fa.tanggal as ""Tanggal"",
                            fa.waktu as ""Waktu"",
                            fa.status as ""Status""
                        FROM Form_Absensi fa
                        JOIN Mahasiswa m ON fa.nim = m.nim
                        JOIN MataKuliah mk ON fa.matakuliah_id = mk.matakuliah_id
                        JOIN Prodi p ON mk.prodi_id = p.prodi_id";

                    string whereClause = "";
                    string searchText = searchTextBox.Text.Trim();

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        string filter = searchFilterComboBox.SelectedItem.ToString();
                        switch (filter)
                        {
                            case "Nama":
                                whereClause = " WHERE LOWER(COALESCE(fa.nama_mahasiswa, m.nama)) LIKE LOWER(@searchText)";
                                break;
                            case "NIM":
                                whereClause = " WHERE m.nim LIKE @searchText";
                                break;
                            case "Program Studi":
                                whereClause = " WHERE LOWER(p.nama_prodi) LIKE LOWER(@searchText)";
                                break;
                            case "Mata Kuliah":
                                whereClause = " WHERE LOWER(mk.nama_matakuliah) LIKE LOWER(@searchText)";
                                break;
                            case "Status":
                                whereClause = " WHERE LOWER(fa.status) LIKE LOWER(@searchText)";
                                break;
                            default: // Semua
                                whereClause = @" WHERE (
                                    LOWER(COALESCE(fa.nama_mahasiswa, m.nama)) LIKE LOWER(@searchText) OR
                                    m.nim LIKE @searchText OR
                                    LOWER(p.nama_prodi) LIKE LOWER(@searchText) OR
                                    LOWER(mk.nama_matakuliah) LIKE LOWER(@searchText) OR
                                    LOWER(fa.status) LIKE LOWER(@searchText)
                                )";
                                break;
                        }
                    }

                    string fullQuery = baseQuery + whereClause + " ORDER BY fa.tanggal DESC, fa.waktu DESC";

                    using (var cmd = new NpgsqlCommand(fullQuery, conn))
                    {
                        if (!string.IsNullOrEmpty(searchText))
                        {
                            cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                        }

                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            searchResultGrid.DataSource = dt;
                            searchResultLabel.Text = $"📊 Hasil pencarian: {dt.Rows.Count} data ditemukan";

                            if (!searchResultGrid.Columns.Contains("Edit") && searchResultGrid.Columns.Contains("Status"))
                            {
                                int statusIndex = searchResultGrid.Columns["Status"].Index;

                                DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                                editButton.Name = "Edit";
                                editButton.HeaderText = "Aksi";
                                editButton.Text = "🖊";
                                editButton.UseColumnTextForButtonValue = true;
                                editButton.Width = 50;
                                editButton.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                                searchResultGrid.Columns.Insert(statusIndex + 1, editButton);

                                searchResultGrid.CellClick -= SearchResultGrid_CellClick;
                                searchResultGrid.CellClick += SearchResultGrid_CellClick;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error performing search: " + ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (studentComboBox.SelectedItem == null || subjectComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Harap pilih mahasiswa dan mata kuliah terlebih dahulu!", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ComboBoxItem selectedStudent = (ComboBoxItem)studentComboBox.SelectedItem;
                ComboBoxItem selectedSubject = (ComboBoxItem)subjectComboBox.SelectedItem;

                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    // Get student name
                    string getStudentQuery = "SELECT nama FROM Mahasiswa WHERE nim = @nim";
                    string studentName = "";
                    using (var cmd = new NpgsqlCommand(getStudentQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@nim", selectedStudent.Value);
                        studentName = cmd.ExecuteScalar()?.ToString() ?? "";
                    }

                    // Get valid NIP from MataKuliah table (dosen pengampu mata kuliah)
                    string getDosenQuery = @"
                SELECT mk.nip 
                FROM MataKuliah mk 
                WHERE mk.matakuliah_id = @matakuliah_id 
                AND mk.nip IS NOT NULL";

                    string dosenNip = null;
                    string dosenName = "Dosen Pengampu"; // Default nama dosen

                    using (var cmd = new NpgsqlCommand(getDosenQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@matakuliah_id", int.Parse(selectedSubject.Value));
                        dosenNip = cmd.ExecuteScalar()?.ToString();
                    }

                    // Jika tidak ada NIP di mata kuliah, ambil NIP pertama dari tabel Dosen
                    if (string.IsNullOrEmpty(dosenNip))
                    {
                        string getFirstDosenQuery = "SELECT nip FROM Dosen LIMIT 1";
                        using (var cmd = new NpgsqlCommand(getFirstDosenQuery, conn))
                        {
                            dosenNip = cmd.ExecuteScalar()?.ToString();
                        }
                    }

                    // Jika masih tidak ada, buat NIP default (pastikan ada di database)
                    if (string.IsNullOrEmpty(dosenNip))
                    {
                        // Coba insert dosen default jika belum ada
                        string checkDefaultQuery = "SELECT COUNT(*) FROM Dosen WHERE nip = 'DEFAULT001'";
                        using (var cmd = new NpgsqlCommand(checkDefaultQuery, conn))
                        {
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            if (count == 0)
                            {
                                string insertDefaultQuery = "INSERT INTO Dosen (nip) VALUES ('DEFAULT001')";
                                using (var insertCmd = new NpgsqlCommand(insertDefaultQuery, conn))
                                {
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }
                        dosenNip = "DEFAULT001";
                        dosenName = "Dosen Default";
                    }

                    // Jika masih tidak ada dosen, beri peringatan
                    if (string.IsNullOrEmpty(dosenNip))
                    {
                        MessageBox.Show("Tidak ada data dosen yang valid di database!", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string insertQuery = @"
                INSERT INTO Form_Absensi (nim, nama_mahasiswa, nip, nama_dosen, tanggal, waktu, status, matakuliah_id)
                VALUES (@nim, @nama_mahasiswa, @nip, @nama_dosen, @tanggal, @waktu, @status, @matakuliah_id)";

                    using (var cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@nim", selectedStudent.Value);
                        cmd.Parameters.AddWithValue("@nama_mahasiswa", studentName);
                        cmd.Parameters.AddWithValue("@nip", dosenNip);
                        cmd.Parameters.AddWithValue("@nama_dosen", dosenName);
                        cmd.Parameters.AddWithValue("@tanggal", datePicker.Value.Date);
                        cmd.Parameters.AddWithValue("@waktu", timePicker.Value.TimeOfDay);
                        cmd.Parameters.AddWithValue("@status", statusComboBox.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@matakuliah_id", int.Parse(selectedSubject.Value));

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data absensi berhasil disimpan!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCreatePreviewData();
                ClearFormInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearFormInputs();
        }

        private void ClearFormInputs()
        {
            studentComboBox.SelectedIndex = -1;
            subjectComboBox.SelectedIndex = -1;
            statusComboBox.SelectedIndex = 0;
            datePicker.Value = DateTime.Now;
            timePicker.Value = DateTime.Now;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (deleteGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Harap pilih data yang ingin dihapus!", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus {deleteGrid.SelectedRows.Count} data absensi?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        conn.Open();

                        foreach (DataGridViewRow row in deleteGrid.SelectedRows)
                        {
                            int idAbsensi = Convert.ToInt32(row.Cells["ID"].Value);

                            string deleteQuery = "DELETE FROM Form_Absensi WHERE id_absensi = @id";
                            using (var cmd = new NpgsqlCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", idAbsensi);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show($"{deleteGrid.SelectedRows.Count} data berhasil dihapus!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDeleteData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteGrid_SelectionChanged(object sender, EventArgs e)
        {
            deleteStatusLabel.Text = $"📊 {deleteGrid.SelectedRows.Count} data terpilih untuk dihapus";
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.FileName = $"attendance_data_{DateTime.Now:yyyyMMdd}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportDataToCSV(attendanceGrid, saveFileDialog.FileName);
                    MessageBox.Show("Data berhasil diekspor!", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportDataToCSV(DataGridView grid, string fileName)
        {
            StringBuilder csv = new StringBuilder();

            // Header
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                csv.Append(grid.Columns[i].HeaderText);
                if (i < grid.Columns.Count - 1)
                    csv.Append(",");
            }
            csv.AppendLine();

            // Data rows
            foreach (DataGridViewRow row in grid.Rows)
            {
                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    csv.Append(row.Cells[i].Value?.ToString() ?? "");
                    if (i < grid.Columns.Count - 1)
                        csv.Append(",");
                }
                csv.AppendLine();
            }

            System.IO.File.WriteAllText(fileName, csv.ToString());
        }

        // Navigation methods
        private void ShowPanel(Panel panelToShow)
        {
            // Hide all panels
            mainPanel.Visible = false;
            searchPanel.Visible = false;
            createPanel.Visible = false;
            deletePanel.Visible = false;

            // Show selected panel
            panelToShow.Visible = true;
            panelToShow.BringToFront();
        }

        // Sidebar navigation event handlers (these should be connected to your sidebar buttons)
        private void HomeButton_Click(object sender, EventArgs e)
        {
            ShowPanel(mainPanel);
            LoadTodayAttendance();
        }

        private void search_Click(object sender, EventArgs e)
        {
            ShowPanel(searchPanel);
        }

        private void create_Click(object sender, EventArgs e)
        {
            ShowPanel(createPanel);
            LoadCreatePreviewData();
            LoadStudentsForComboBox();
            LoadSubjectsForComboBox();

        }

        private void delete_Click(object sender, EventArgs e)
        {
            ShowPanel(deletePanel);
            LoadDeleteData();
        }

        private void dosenUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            refreshTimer?.Stop();
            refreshTimer?.Dispose();
        }

        // Helper class for ComboBox items
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }


        private void menucontainer_Paint(object sender, PaintEventArgs e) { }

        private void button8_Click(object sender, EventArgs e) { }

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

            // Penyesuaian ulang untuk semua panel
            AdjustPanelLayout(mainPanel);
            AdjustPanelLayout(searchPanel);
            AdjustPanelLayout(createPanel);
            AdjustPanelLayout(deletePanel);
        }

        private void AdjustPanelLayout(Panel panel)
        {
            if (panel != null)
            {
                panel.Location = new Point(sidebar.Width + 10, panel1.Height + 10);
                panel.Size = new Size(this.Width - sidebar.Width - 30, this.Height - panel1.Height - 50);
            }
        }

        private void home_Click(object sender, EventArgs e)
        {
            ShowPanel(mainPanel);
            LoadTodayAttendance(); // panggil fungsi Home
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

        private void menubutton_Click(object sender, EventArgs e)
        {
            sidebartimer.Start();
        }

        private void dosenUI_Load(object sender, EventArgs e)
        {
            ShowPanel(mainPanel); // default tampilkan home
            LoadTodayAttendance();
        }
    }
}