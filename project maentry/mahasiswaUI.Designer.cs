namespace project_maentry
{
    partial class mahasiswaUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mahasiswaUI));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.PictureBox();
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.history = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.search = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.logout = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.sidebartimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menu)).BeginInit();
            this.sidebar.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Controls.Add(this.menu);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1334, 53);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "MENU MAHASISWA | MAENTRY ";
            // 
            // menu
            // 
            this.menu.Image = global::project_maentry.Properties.Resources.icons8_menu_50;
            this.menu.Location = new System.Drawing.Point(6, 6);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(42, 41);
            this.menu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.menu.TabIndex = 1;
            this.menu.TabStop = false;
            this.menu.Click += new System.EventHandler(this.menu_Click);
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.sidebar.Controls.Add(this.panel5);
            this.sidebar.Controls.Add(this.panel3);
            this.sidebar.Controls.Add(this.panel2);
            this.sidebar.Controls.Add(this.panel10);
            this.sidebar.Controls.Add(this.panel9);
            this.sidebar.Controls.Add(this.panel8);
            this.sidebar.Controls.Add(this.panel7);
            this.sidebar.Controls.Add(this.panel4);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 53);
            this.sidebar.MaximumSize = new System.Drawing.Size(200, 0);
            this.sidebar.MinimumSize = new System.Drawing.Size(58, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(58, 619);
            this.sidebar.TabIndex = 1;
            this.sidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // history
            // 
            this.history.BackColor = System.Drawing.Color.LightSkyBlue;
            this.history.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history.Image = ((System.Drawing.Image)(resources.GetObject("history.Image")));
            this.history.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.history.Location = new System.Drawing.Point(-6, -13);
            this.history.Name = "history";
            this.history.Size = new System.Drawing.Size(257, 96);
            this.history.TabIndex = 2;
            this.history.Text = "History";
            this.history.UseVisualStyleBackColor = false;
            this.history.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.history);
            this.panel2.Location = new System.Drawing.Point(3, 157);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(197, 71);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.search);
            this.panel3.Location = new System.Drawing.Point(3, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(197, 71);
            this.panel3.TabIndex = 4;
            // 
            // search
            // 
            this.search.BackColor = System.Drawing.Color.LightSkyBlue;
            this.search.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.Image = ((System.Drawing.Image)(resources.GetObject("search.Image")));
            this.search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.search.Location = new System.Drawing.Point(-7, -13);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(258, 96);
            this.search.TabIndex = 2;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.logout);
            this.panel4.Location = new System.Drawing.Point(3, 542);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(197, 71);
            this.panel4.TabIndex = 4;
            // 
            // logout
            // 
            this.logout.BackColor = System.Drawing.Color.LightSkyBlue;
            this.logout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logout.Image = ((System.Drawing.Image)(resources.GetObject("logout.Image")));
            this.logout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.logout.Location = new System.Drawing.Point(-8, -13);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(259, 96);
            this.logout.TabIndex = 2;
            this.logout.Text = "Logout";
            this.logout.UseVisualStyleBackColor = false;
            this.logout.Click += new System.EventHandler(this.logout_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button3);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(197, 71);
            this.panel5.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::project_maentry.Properties.Resources.icons8_home_50;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(-8, -13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(259, 96);
            this.button3.TabIndex = 2;
            this.button3.Text = "Home";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // panel7
            // 
            this.panel7.Location = new System.Drawing.Point(3, 465);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(251, 71);
            this.panel7.TabIndex = 5;
            // 
            // panel8
            // 
            this.panel8.Location = new System.Drawing.Point(3, 388);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(251, 71);
            this.panel8.TabIndex = 5;
            // 
            // panel9
            // 
            this.panel9.Location = new System.Drawing.Point(3, 311);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(251, 71);
            this.panel9.TabIndex = 5;
            // 
            // panel10
            // 
            this.panel10.Location = new System.Drawing.Point(3, 234);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(251, 71);
            this.panel10.TabIndex = 5;
            // 
            // sidebartimer
            // 
            this.sidebartimer.Interval = 10;
            this.sidebartimer.Tick += new System.EventHandler(this.sidebartransition_Tick);
            // 
            // mahasiswaUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 672);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.panel1);
            this.Name = "mahasiswaUI";
            this.Text = "mahasiswaUI";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menu)).EndInit();
            this.sidebar.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

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