using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Free_Short_Keys
{
    /// <summary>
    /// All Keyboard hook code taken orignally from http://www.codeproject.com/Articles/7294/Processing-Global-Mouse-and-Keyboard-Hooks-in-C
    /// </summary>
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Keylogger kl = new Keylogger(@"M:\_drops\logfile.txt");


            kl.Enabled = true; // enable key logging


            kl.FlushInterval = 10000; // set buffer flush interval


            kl.Flush2File(@"M:\_drops\logfile.txt", true); // force buffer flush


            //SendKeys.Send("blah");
        }
    }
}
