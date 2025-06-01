using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_maentry
{
    public partial class dosenUI : Form
    {
        bool sidebarExpand;
        public dosenUI()
        {
            InitializeComponent();
        }

        private void dosenUI_Load(object sender, EventArgs e)
        {






        }


        private void home_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void menucontainer_Paint(object sender, PaintEventArgs e)
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

        private void menubutton_Click(object sender, EventArgs e)
        {
            sidebartimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }        
}