namespace project_maentry
{
    partial class dosenUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dosenUI));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.sidebartimer = new System.Windows.Forms.Timer(this.components);
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.home = new System.Windows.Forms.Button();
            this.search = new System.Windows.Forms.Button();
            this.create = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menubutton = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.sidebar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menubutton)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.menubutton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1335, 55);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "MENU DOSEN | MAENTRY";
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.sidebar.Controls.Add(this.panel3);
            this.sidebar.Controls.Add(this.panel4);
            this.sidebar.Controls.Add(this.panel5);
            this.sidebar.Controls.Add(this.panel2);
            this.sidebar.Controls.Add(this.panel9);
            this.sidebar.Controls.Add(this.panel8);
            this.sidebar.Controls.Add(this.panel10);
            this.sidebar.Controls.Add(this.panel7);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 55);
            this.sidebar.MaximumSize = new System.Drawing.Size(249, 0);
            this.sidebar.MinimumSize = new System.Drawing.Size(58, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(58, 574);
            this.sidebar.TabIndex = 1;
            this.sidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.menucontainer_Paint);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.home);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(246, 60);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.search);
            this.panel4.Location = new System.Drawing.Point(3, 69);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(262, 60);
            this.panel4.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.create);
            this.panel5.Location = new System.Drawing.Point(3, 135);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(218, 60);
            this.panel5.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.delete);
            this.panel2.Location = new System.Drawing.Point(3, 201);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(218, 60);
            this.panel2.TabIndex = 3;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.button8);
            this.panel9.Location = new System.Drawing.Point(3, 267);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(218, 60);
            this.panel9.TabIndex = 5;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(-4, -14);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(238, 85);
            this.button8.TabIndex = 4;
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.button3);
            this.panel8.Location = new System.Drawing.Point(3, 333);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(218, 60);
            this.panel8.TabIndex = 5;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(-4, -18);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(238, 84);
            this.button4.TabIndex = 4;
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // sidebartimer
            // 
            this.sidebartimer.Interval = 10;
            this.sidebartimer.Tick += new System.EventHandler(this.sidebartransition_Tick);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.button2);
            this.panel7.Location = new System.Drawing.Point(3, 465);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(218, 60);
            this.panel7.TabIndex = 5;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.button4);
            this.panel10.Location = new System.Drawing.Point(3, 399);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(218, 60);
            this.panel10.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(-4, -14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(238, 85);
            this.button3.TabIndex = 5;
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // home
            // 
            this.home.BackColor = System.Drawing.Color.LightSkyBlue;
            this.home.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.home.ForeColor = System.Drawing.SystemColors.ControlText;
            this.home.Image = global::project_maentry.Properties.Resources.icons8_home_50;
            this.home.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.home.Location = new System.Drawing.Point(-9, -9);
            this.home.Name = "home";
            this.home.Size = new System.Drawing.Size(267, 85);
            this.home.TabIndex = 4;
            this.home.Text = "Home";
            this.home.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.home.UseVisualStyleBackColor = false;
            this.home.Click += new System.EventHandler(this.home_Click);
            // 
            // search
            // 
            this.search.BackColor = System.Drawing.Color.LightSkyBlue;
            this.search.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.ForeColor = System.Drawing.SystemColors.ControlText;
            this.search.Image = ((System.Drawing.Image)(resources.GetObject("search.Image")));
            this.search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.search.Location = new System.Drawing.Point(-8, -11);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(267, 85);
            this.search.TabIndex = 4;
            this.search.Text = "Search";
            this.search.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.search.UseVisualStyleBackColor = false;
            // 
            // create
            // 
            this.create.BackColor = System.Drawing.Color.LightSkyBlue;
            this.create.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.create.ForeColor = System.Drawing.SystemColors.ControlText;
            this.create.Image = global::project_maentry.Properties.Resources.icons8_create_50;
            this.create.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.create.Location = new System.Drawing.Point(-7, -11);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(238, 85);
            this.create.TabIndex = 4;
            this.create.Text = "Create";
            this.create.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.create.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.create.UseVisualStyleBackColor = false;
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.LightSkyBlue;
            this.delete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.delete.Location = new System.Drawing.Point(-8, -8);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(411, 81);
            this.delete.TabIndex = 4;
            this.delete.Text = "Delete";
            this.delete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.delete.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(-6, -13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(238, 85);
            this.button2.TabIndex = 4;
            this.button2.Text = "Logout";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // menubutton
            // 
            this.menubutton.Image = global::project_maentry.Properties.Resources.icons8_menu_50;
            this.menubutton.Location = new System.Drawing.Point(3, 8);
            this.menubutton.Name = "menubutton";
            this.menubutton.Size = new System.Drawing.Size(46, 42);
            this.menubutton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.menubutton.TabIndex = 1;
            this.menubutton.TabStop = false;
            this.menubutton.Click += new System.EventHandler(this.menubutton_Click);
            // 
            // dosenUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 629);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.panel1);
            this.Name = "dosenUI";
            this.Text = "dosen";
            this.Load += new System.EventHandler(this.dosenUI_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.sidebar.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.menubutton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox menubutton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FlowLayoutPanel sidebar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button home;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Timer sidebartimer;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel7;
    }
}
