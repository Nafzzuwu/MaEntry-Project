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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dosenUI));
            panel1 = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            menubutton = new System.Windows.Forms.PictureBox();
            sidebar = new System.Windows.Forms.FlowLayoutPanel();
            panel3 = new System.Windows.Forms.Panel();
            home = new System.Windows.Forms.Button();
            panel4 = new System.Windows.Forms.Panel();
            search = new System.Windows.Forms.Button();
            panel5 = new System.Windows.Forms.Panel();
            create = new System.Windows.Forms.Button();
            panel2 = new System.Windows.Forms.Panel();
            delete = new System.Windows.Forms.Button();
            panel9 = new System.Windows.Forms.Panel();
            button8 = new System.Windows.Forms.Button();
            panel8 = new System.Windows.Forms.Panel();
            button3 = new System.Windows.Forms.Button();
            panel10 = new System.Windows.Forms.Panel();
            button4 = new System.Windows.Forms.Button();
            panel7 = new System.Windows.Forms.Panel();
            button2 = new System.Windows.Forms.Button();
            sidebartimer = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)menubutton).BeginInit();
            sidebar.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel2.SuspendLayout();
            panel9.SuspendLayout();
            panel8.SuspendLayout();
            panel10.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(menubutton);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1484, 69);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(58, 19);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(243, 28);
            label1.TabIndex = 1;
            label1.Text = "MENU DOSEN | MAENTRY";
            // 
            // menubutton
            // 
            menubutton.Image = Properties.Resources.icons8_menu_50;
            menubutton.Location = new System.Drawing.Point(4, 10);
            menubutton.Margin = new System.Windows.Forms.Padding(4);
            menubutton.Name = "menubutton";
            menubutton.Size = new System.Drawing.Size(51, 52);
            menubutton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            menubutton.TabIndex = 1;
            menubutton.TabStop = false;
            menubutton.Click += menubutton_Click;
            // 
            // sidebar
            // 
            sidebar.BackColor = System.Drawing.Color.LightSkyBlue;
            sidebar.Controls.Add(panel3);
            sidebar.Controls.Add(panel4);
            sidebar.Controls.Add(panel5);
            sidebar.Controls.Add(panel2);
            sidebar.Controls.Add(panel9);
            sidebar.Controls.Add(panel8);
            sidebar.Controls.Add(panel10);
            sidebar.Controls.Add(panel7);
            sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            sidebar.Location = new System.Drawing.Point(0, 69);
            sidebar.Margin = new System.Windows.Forms.Padding(4);
            sidebar.MaximumSize = new System.Drawing.Size(150, 0);
            sidebar.MinimumSize = new System.Drawing.Size(48, 0);
            sidebar.Name = "sidebar";
            sidebar.Size = new System.Drawing.Size(48, 717);
            sidebar.TabIndex = 1;
            sidebar.Paint += menucontainer_Paint;
            // 
            // panel3
            // 
            panel3.Controls.Add(home);
            panel3.Location = new System.Drawing.Point(4, 4);
            panel3.Margin = new System.Windows.Forms.Padding(4);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(274, 75);
            panel3.TabIndex = 5;
            // 
            // home
            // 
            home.BackColor = System.Drawing.Color.LightSkyBlue;
            home.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            home.ForeColor = System.Drawing.SystemColors.ControlText;
            home.Image = (System.Drawing.Image)resources.GetObject("home.Image");
            home.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            home.Location = new System.Drawing.Point(-7, -11);
            home.Margin = new System.Windows.Forms.Padding(4);
            home.Name = "home";
            home.Size = new System.Drawing.Size(296, 106);
            home.TabIndex = 4;
            home.Text = "HOME";
            home.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            home.UseVisualStyleBackColor = false;
            home.Click += home_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(search);
            panel4.Location = new System.Drawing.Point(4, 87);
            panel4.Margin = new System.Windows.Forms.Padding(4);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(291, 75);
            panel4.TabIndex = 8;
            // 
            // search
            // 
            search.BackColor = System.Drawing.Color.LightSkyBlue;
            search.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            search.ForeColor = System.Drawing.SystemColors.ControlText;
            search.Image = (System.Drawing.Image)resources.GetObject("search.Image");
            search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            search.Location = new System.Drawing.Point(-7, -14);
            search.Margin = new System.Windows.Forms.Padding(4);
            search.Name = "search";
            search.Size = new System.Drawing.Size(296, 106);
            search.TabIndex = 4;
            search.Text = "SEARCH";
            search.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            search.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            panel5.Controls.Add(create);
            panel5.Location = new System.Drawing.Point(4, 170);
            panel5.Margin = new System.Windows.Forms.Padding(4);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(242, 75);
            panel5.TabIndex = 5;
            // 
            // create
            // 
            create.BackColor = System.Drawing.Color.LightSkyBlue;
            create.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            create.ForeColor = System.Drawing.SystemColors.ControlText;
            create.Image = (System.Drawing.Image)resources.GetObject("create.Image");
            create.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            create.Location = new System.Drawing.Point(-7, -14);
            create.Margin = new System.Windows.Forms.Padding(4);
            create.Name = "create";
            create.Size = new System.Drawing.Size(265, 106);
            create.TabIndex = 4;
            create.Text = "CREATE";
            create.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            create.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            create.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(delete);
            panel2.Location = new System.Drawing.Point(4, 253);
            panel2.Margin = new System.Windows.Forms.Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(242, 75);
            panel2.TabIndex = 3;
            // 
            // delete
            // 
            delete.BackColor = System.Drawing.Color.LightSkyBlue;
            delete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            delete.ForeColor = System.Drawing.SystemColors.ControlText;
            delete.Image = (System.Drawing.Image)resources.GetObject("delete.Image");
            delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            delete.Location = new System.Drawing.Point(-7, -10);
            delete.Margin = new System.Windows.Forms.Padding(4);
            delete.Name = "delete";
            delete.Size = new System.Drawing.Size(456, 101);
            delete.TabIndex = 4;
            delete.Text = "DELETE";
            delete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            delete.UseVisualStyleBackColor = false;
            // 
            // panel9
            // 
            panel9.Controls.Add(button8);
            panel9.Location = new System.Drawing.Point(4, 336);
            panel9.Margin = new System.Windows.Forms.Padding(4);
            panel9.Name = "panel9";
            panel9.Size = new System.Drawing.Size(242, 75);
            panel9.TabIndex = 5;
            // 
            // button8
            // 
            button8.BackColor = System.Drawing.Color.LightSkyBlue;
            button8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button8.ForeColor = System.Drawing.SystemColors.ControlText;
            button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button8.Location = new System.Drawing.Point(-5, -18);
            button8.Margin = new System.Windows.Forms.Padding(4);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(265, 106);
            button8.TabIndex = 4;
            button8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // panel8
            // 
            panel8.Controls.Add(button3);
            panel8.Location = new System.Drawing.Point(4, 419);
            panel8.Margin = new System.Windows.Forms.Padding(4);
            panel8.Name = "panel8";
            panel8.Size = new System.Drawing.Size(242, 75);
            panel8.TabIndex = 5;
            // 
            // button3
            // 
            button3.BackColor = System.Drawing.Color.LightSkyBlue;
            button3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button3.ForeColor = System.Drawing.SystemColors.ControlText;
            button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button3.Location = new System.Drawing.Point(-5, -18);
            button3.Margin = new System.Windows.Forms.Padding(4);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(265, 106);
            button3.TabIndex = 5;
            button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            button3.UseVisualStyleBackColor = false;
            // 
            // panel10
            // 
            panel10.Controls.Add(button4);
            panel10.Location = new System.Drawing.Point(4, 502);
            panel10.Margin = new System.Windows.Forms.Padding(4);
            panel10.Name = "panel10";
            panel10.Size = new System.Drawing.Size(242, 75);
            panel10.TabIndex = 5;
            // 
            // button4
            // 
            button4.BackColor = System.Drawing.Color.LightSkyBlue;
            button4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button4.ForeColor = System.Drawing.SystemColors.ControlText;
            button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button4.Location = new System.Drawing.Point(-5, -22);
            button4.Margin = new System.Windows.Forms.Padding(4);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(265, 105);
            button4.TabIndex = 4;
            button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            button4.UseVisualStyleBackColor = false;
            // 
            // panel7
            // 
            panel7.Controls.Add(button2);
            panel7.Location = new System.Drawing.Point(4, 585);
            panel7.Margin = new System.Windows.Forms.Padding(4);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(242, 75);
            panel7.TabIndex = 5;
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.LightSkyBlue;
            button2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button2.ForeColor = System.Drawing.SystemColors.ControlText;
            button2.Image = (System.Drawing.Image)resources.GetObject("button2.Image");
            button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button2.Location = new System.Drawing.Point(-5, -16);
            button2.Margin = new System.Windows.Forms.Padding(4);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(265, 106);
            button2.TabIndex = 4;
            button2.Text = "LOGOUT";
            button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // sidebartimer
            // 
            sidebartimer.Interval = 10;
            sidebartimer.Tick += sidebartransition_Tick;
            // 
            // dosenUI
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1484, 786);
            Controls.Add(sidebar);
            Controls.Add(panel1);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "dosenUI";
            Text = "dosen";
            Load += dosenUI_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)menubutton).EndInit();
            sidebar.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel7.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox menubutton;
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