using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace Vitros350
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string iconPath = Path.Combine(Application.StartupPath,"Icons", "Vitros350.ico");
            notifyIcon1.Icon = new Icon(iconPath); // Set your icon here
            notifyIcon1.Text = "Vitros350";
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(5000, "Vitros350", "Application minimized to tray.", ToolTipIcon.Info);
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RestoreFromTray();
        }

        private void RestoreFromTray()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
    }
}
