using System;

namespace project_maentry
{
    partial class mahasiswaUI
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mahasiswaUI));
            panel1 = new System.Windows.Forms.Panel();
            menu = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            sidebar = new System.Windows.Forms.FlowLayoutPanel();
            panel5 = new System.Windows.Forms.Panel();
            button3 = new System.Windows.Forms.Button();
            panel3 = new System.Windows.Forms.Panel();
            search = new System.Windows.Forms.Button();
            panel2 = new System.Windows.Forms.Panel();
            history = new System.Windows.Forms.Button();
            panel10 = new System.Windows.Forms.Panel();
            panel9 = new System.Windows.Forms.Panel();
            panel8 = new System.Windows.Forms.Panel();
            panel7 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            logout = new System.Windows.Forms.Button();
            sidebartimer = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)menu).BeginInit();
            sidebar.SuspendLayout();
            panel5.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            panel1.Controls.Add(menu);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Font = new System.Drawing.Font("Segoe UI", 10F);
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1482, 66);
            panel1.TabIndex = 0;
            // 
            // menu
            // 
            menu.Image = Properties.Resources.icons8_menu_50;
            menu.Location = new System.Drawing.Point(6, 8);
            menu.Margin = new System.Windows.Forms.Padding(4);
            menu.Name = "menu";
            menu.Size = new System.Drawing.Size(46, 51);
            menu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            menu.TabIndex = 1;
            menu.TabStop = false;
            menu.Click += menu_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(51, 16);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(299, 28);
            label1.TabIndex = 0;
            label1.Text = "MENU MAHASISWA | MAENTRY ";
            // 
            // sidebar
            // 
            sidebar.BackColor = System.Drawing.Color.LightSkyBlue;
            sidebar.Controls.Add(panel5);
            sidebar.Controls.Add(panel3);
            sidebar.Controls.Add(panel2);
            sidebar.Controls.Add(panel10);
            sidebar.Controls.Add(panel9);
            sidebar.Controls.Add(panel8);
            sidebar.Controls.Add(panel7);
            sidebar.Controls.Add(panel4);
            sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            sidebar.Location = new System.Drawing.Point(0, 66);
            sidebar.Margin = new System.Windows.Forms.Padding(4);
            sidebar.MaximumSize = new System.Drawing.Size(180, 0);
            sidebar.MinimumSize = new System.Drawing.Size(45, 0);
            sidebar.Name = "sidebar";
            sidebar.Size = new System.Drawing.Size(45, 774);
            sidebar.TabIndex = 1;
            sidebar.Paint += flowLayoutPanel1_Paint;
            // 
            // panel5
            // 
            panel5.Controls.Add(button3);
            panel5.Location = new System.Drawing.Point(4, 4);
            panel5.Margin = new System.Windows.Forms.Padding(4);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(219, 89);
            panel5.TabIndex = 4;
            // 
            // button3
            // 
            button3.BackColor = System.Drawing.Color.LightSkyBlue;
            button3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            button3.Image = (System.Drawing.Image)resources.GetObject("button3.Image");
            button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button3.Location = new System.Drawing.Point(-9, -16);
            button3.Margin = new System.Windows.Forms.Padding(4);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(313, 120);
            button3.TabIndex = 2;
            button3.Text = "JADWAL";
            button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            button3.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(search);
            panel3.Location = new System.Drawing.Point(4, 101);
            panel3.Margin = new System.Windows.Forms.Padding(4);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(219, 89);
            panel3.TabIndex = 4;
            // 
            // search
            // 
            search.BackColor = System.Drawing.Color.LightSkyBlue;
            search.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            search.Image = (System.Drawing.Image)resources.GetObject("search.Image");
            search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            search.Location = new System.Drawing.Point(-9, -16);
            search.Margin = new System.Windows.Forms.Padding(4);
            search.Name = "search";
            search.Size = new System.Drawing.Size(286, 120);
            search.TabIndex = 2;
            search.Text = "ABSENSI";
            search.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            search.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(history);
            panel2.Location = new System.Drawing.Point(4, 198);
            panel2.Margin = new System.Windows.Forms.Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(219, 89);
            panel2.TabIndex = 3;
            // 
            // history
            // 
            history.BackColor = System.Drawing.Color.LightSkyBlue;
            history.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            history.Image = (System.Drawing.Image)resources.GetObject("history.Image");
            history.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            history.Location = new System.Drawing.Point(-8, -16);
            history.Margin = new System.Windows.Forms.Padding(4);
            history.Name = "history";
            history.Size = new System.Drawing.Size(285, 120);
            history.TabIndex = 2;
            history.Text = "HISTORY";
            history.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            history.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            history.UseVisualStyleBackColor = false;
            history.Click += button1_Click;
            // 
            // panel10
            // 
            panel10.Location = new System.Drawing.Point(4, 295);
            panel10.Margin = new System.Windows.Forms.Padding(4);
            panel10.Name = "panel10";
            panel10.Size = new System.Drawing.Size(279, 89);
            panel10.TabIndex = 5;
            // 
            // panel9
            // 
            panel9.Location = new System.Drawing.Point(4, 392);
            panel9.Margin = new System.Windows.Forms.Padding(4);
            panel9.Name = "panel9";
            panel9.Size = new System.Drawing.Size(279, 89);
            panel9.TabIndex = 5;
            // 
            // panel8
            // 
            panel8.Location = new System.Drawing.Point(4, 489);
            panel8.Margin = new System.Windows.Forms.Padding(4);
            panel8.Name = "panel8";
            panel8.Size = new System.Drawing.Size(279, 89);
            panel8.TabIndex = 5;
            // 
            // panel7
            // 
            panel7.Location = new System.Drawing.Point(4, 586);
            panel7.Margin = new System.Windows.Forms.Padding(4);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(279, 89);
            panel7.TabIndex = 5;
            // 
            // panel4
            // 
            panel4.Controls.Add(logout);
            panel4.Location = new System.Drawing.Point(4, 683);
            panel4.Margin = new System.Windows.Forms.Padding(4);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(219, 89);
            panel4.TabIndex = 4;
            // 
            // logout
            // 
            logout.BackColor = System.Drawing.Color.LightSkyBlue;
            logout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            logout.Image = (System.Drawing.Image)resources.GetObject("logout.Image");
            logout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            logout.Location = new System.Drawing.Point(-9, -16);
            logout.Margin = new System.Windows.Forms.Padding(4);
            logout.Name = "logout";
            logout.Size = new System.Drawing.Size(288, 120);
            logout.TabIndex = 2;
            logout.Text = "LOGOUT";
            logout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            logout.UseVisualStyleBackColor = false;
            logout.Click += logout_Click;
            // 
            // sidebartimer
            // 
            sidebartimer.Interval = 10;
            sidebartimer.Tick += sidebartransition_Tick;
            // 
            // mahasiswaUI
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1482, 840);
            Controls.Add(sidebar);
            Controls.Add(panel1);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "mahasiswaUI";
            Text = "mahasiswaUI";
            Load += mahasiswaUI_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)menu).EndInit();
            sidebar.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void mahasiswaUI_Load(object sender, EventArgs e)
        {
            // Styling DataGridView setelah form dimuat
            Action<System.Windows.Forms.DataGridView> beautifyGrid = grid =>
            {
                grid.EnableHeadersVisualStyles = false;
                grid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue;
                grid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
                grid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                grid.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
                grid.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
                grid.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                grid.RowTemplate.Height = 28;
                grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
                grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
                grid.GridColor = System.Drawing.Color.LightGray;
                grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                grid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
            };

            foreach (System.Windows.Forms.Control control in this.Controls)
            {
                if (control is System.Windows.Forms.Panel mainPanel)
                {
                    foreach (System.Windows.Forms.Control subPanel in mainPanel.Controls)
                    {
                        foreach (System.Windows.Forms.Control inner in subPanel.Controls)
                        {
                            if (inner is System.Windows.Forms.DataGridView grid)
                                beautifyGrid(grid);
                        }
                    }
                }
            }
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox menu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel sidebar;
        private System.Windows.Forms.Button history;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Timer sidebartimer;
    }
}