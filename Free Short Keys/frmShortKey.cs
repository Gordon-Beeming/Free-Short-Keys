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
    public partial class frmShortKey : Form
    {
        public frmShortKey()
        {
            InitializeComponent();
        }
        
        private void frmShortKey_Load(object sender, EventArgs e)
        {
            
        }

        public void SetShortKey(ShortKey key)
        {
            shortKeyBindingSource.DataSource = key;
        }

        public ShortKey GetShortKey()
        {
            return shortKeyBindingSource.DataSource as ShortKey;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
