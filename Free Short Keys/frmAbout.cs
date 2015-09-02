using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Free_Short_Keys
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void lnkProjectPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Gordon-Beeming/Free-Short-Keys");
        }

        private void lnkContributor_GordonBeeming_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://binary-stuff.com/");
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            lblAppVersion.Text = $"v{typeof(frmAbout).Assembly.GetName().Version}";
        }

        private void lnkClickOnceAppSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://clickonce.binary-stuff.com/");
        }
    }
}
