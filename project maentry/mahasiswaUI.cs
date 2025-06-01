using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_maentry
{
    public partial class mahasiswaUI : Form
    {
        bool sidebarExpand;
        public mahasiswaUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

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
        }

        private void menu_Click(object sender, EventArgs e)
        {
            sidebartimer.Start();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}