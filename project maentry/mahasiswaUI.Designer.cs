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
            panel1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1186, 53);
            panel1.TabIndex = 0;
            // 
            // menu
            // 
            menu.Image = Properties.Resources.icons8_menu_50;
            menu.Location = new System.Drawing.Point(5, 6);
            menu.Name = "menu";
            menu.Size = new System.Drawing.Size(37, 41);
            menu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            menu.TabIndex = 1;
            menu.TabStop = false;
            menu.Click += menu_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(41, 13);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(257, 23);
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
            sidebar.Location = new System.Drawing.Point(0, 53);
            sidebar.MaximumSize = new System.Drawing.Size(178, 0);
            sidebar.MinimumSize = new System.Drawing.Size(52, 0);
            sidebar.Name = "sidebar";
            sidebar.Size = new System.Drawing.Size(52, 619);
            sidebar.TabIndex = 1;
            sidebar.Paint += flowLayoutPanel1_Paint;
            // 
            // panel5
            // 
            panel5.Controls.Add(button3);
            panel5.Location = new System.Drawing.Point(3, 3);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(175, 71);
            panel5.TabIndex = 4;
            // 
            // button3
            // 
            button3.BackColor = System.Drawing.Color.LightSkyBlue;
            button3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button3.Image = Properties.Resources.icons8_home_50;
            button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button3.Location = new System.Drawing.Point(-7, -13);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(230, 96);
            button3.TabIndex = 2;
            button3.Text = "JADWAL";
            button3.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(search);
            panel3.Location = new System.Drawing.Point(3, 80);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(175, 71);
            panel3.TabIndex = 4;
            // 
            // search
            // 
            search.BackColor = System.Drawing.Color.LightSkyBlue;
            search.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            search.Image = (System.Drawing.Image)resources.GetObject("search.Image");
            search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            search.Location = new System.Drawing.Point(-6, -13);
            search.Name = "search";
            search.Size = new System.Drawing.Size(229, 96);
            search.TabIndex = 2;
            search.Text = "ABSENSI";
            search.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(history);
            panel2.Location = new System.Drawing.Point(3, 157);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(175, 71);
            panel2.TabIndex = 3;
            // 
            // history
            // 
            history.BackColor = System.Drawing.Color.LightSkyBlue;
            history.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            history.Image = (System.Drawing.Image)resources.GetObject("history.Image");
            history.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            history.Location = new System.Drawing.Point(-5, -13);
            history.Name = "history";
            history.Size = new System.Drawing.Size(228, 96);
            history.TabIndex = 2;
            history.Text = "HISTORY";
            history.UseVisualStyleBackColor = false;
            history.Click += button1_Click;
            // 
            // panel10
            // 
            panel10.Location = new System.Drawing.Point(3, 234);
            panel10.Name = "panel10";
            panel10.Size = new System.Drawing.Size(223, 71);
            panel10.TabIndex = 5;
            // 
            // panel9
            // 
            panel9.Location = new System.Drawing.Point(3, 311);
            panel9.Name = "panel9";
            panel9.Size = new System.Drawing.Size(223, 71);
            panel9.TabIndex = 5;
            // 
            // panel8
            // 
            panel8.Location = new System.Drawing.Point(3, 388);
            panel8.Name = "panel8";
            panel8.Size = new System.Drawing.Size(223, 71);
            panel8.TabIndex = 5;
            // 
            // panel7
            // 
            panel7.Location = new System.Drawing.Point(3, 465);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(223, 71);
            panel7.TabIndex = 5;
            // 
            // panel4
            // 
            panel4.Controls.Add(logout);
            panel4.Location = new System.Drawing.Point(3, 542);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(175, 71);
            panel4.TabIndex = 4;
            // 
            // logout
            // 
            logout.BackColor = System.Drawing.Color.LightSkyBlue;
            logout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            logout.Image = (System.Drawing.Image)resources.GetObject("logout.Image");
            logout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            logout.Location = new System.Drawing.Point(-7, -13);
            logout.Name = "logout";
            logout.Size = new System.Drawing.Size(230, 96);
            logout.TabIndex = 2;
            logout.Text = "Logout";
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
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1186, 672);
            Controls.Add(sidebar);
            Controls.Add(panel1);
            Name = "mahasiswaUI";
            Text = "mahasiswaUI";
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