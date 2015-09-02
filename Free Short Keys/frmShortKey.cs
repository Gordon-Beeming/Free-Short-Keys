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
            LoadSpecialKeys();
        }

        private void LoadSpecialKeys()
        {
            cbxInsertKey.DataSource = Keylogger.SpecialKeys;
            cbxInsertKey.DisplayMember = "Key";
        }

        public void SetShortKey(ShortKey key)
        {
            cbxCategory.DataSource = ShortKeyConfiguration.GetCategories();
            shortKeyBindingSource.DataSource = key;
            cbxCategory.Text = key.Category;
            cbxCategory.Enabled = false;
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
            if (this.ValidateParams())
            {
                SaveCursorPosition();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void SaveCursorPosition()
        {
            if (chkSaveRepalcementKeyCursorPosition.Checked)
            {
                ShortKey key = GetShortKey();
                string guid = Guid.NewGuid().ToString("N");
                string replacementKey = key.ReplacementKey;
                replacementKey = replacementKey.Insert(txtReplacementKey.SelectionStart, guid);
                replacementKey = Keylogger.PerformNewLineFix(replacementKey);
                int index = replacementKey.IndexOf(guid);
                key.CursorLeftCount = replacementKey.Length - index - guid.Length;
            }
        }

        private bool ValidateParams()
        {
            ShortKey key = GetShortKey();
            try
            {
                key.Validate();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "validation failed!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        private void cbxInsertKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpecialKey key = (SpecialKey)cbxInsertKey.SelectedItem;
            var selectionIndex = txtReplacementKey.SelectionStart;
            txtReplacementKey.Text = txtReplacementKey.Text.Insert(selectionIndex, key.Codes[0]);
            txtReplacementKey.SelectionStart = selectionIndex + key.Codes[0].Length;
            cbxInsertKey.SelectedIndex = 0;
        }
    }
}
