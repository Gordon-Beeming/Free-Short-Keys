using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Free_Short_Keys
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx
    /// </summary>
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {
            await ShortKeyConfiguration.Start();
            txtSuffix.Text = ShortKeyConfiguration.Instance.DefaultSuffix;
            RefreshBinding();
            RefreshUpdateButton();
        }

        private void RefreshBinding()
        {
            gridShortKeys.DataSource = null;
            gridShortKeys.DataSource = ShortKeyConfiguration.Instance.ShortKeys;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            frmShortKey frm = new frmShortKey();
            frm.SetShortKey(new ShortKey());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ShortKey key = frm.GetShortKey();
                ShortKeyConfiguration.Instance.ShortKeys.Add(key);
                await ShortKeyConfiguration.Save();
                RefreshBinding();
                RefreshUpdateButton();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            ShortKeyConfiguration.Instance.DefaultSuffix = txtSuffix.Text;
            await ShortKeyConfiguration.Save();
            RefreshUpdateButton();
        }

        private void txtSuffix_TextChanged(object sender, EventArgs e)
        {
            RefreshUpdateButton();
        }

        private void RefreshUpdateButton()
        {
            btnUpdate.Enabled = string.Compare(ShortKeyConfiguration.Instance.DefaultSuffix, txtSuffix.Text, StringComparison.InvariantCultureIgnoreCase) != 0;
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (gridShortKeys.SelectedRows.Count > 0)
            {
                frmShortKey frm = new frmShortKey();
                frm.SetShortKey(GetSelectedItem());
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ShortKey key = frm.GetShortKey();
                    foreach (var item in ShortKeyConfiguration.Instance.ShortKeys)
                    {
                        if (item.Id == key.Id)
                        {
                            foreach (PropertyInfo prop in typeof(ShortKey).GetProperties())
                            {
                                if (prop.Name != "Id")
                                {
                                    prop.SetValue(item, prop.GetValue(key));
                                }
                            }
                        }
                    }
                    await ShortKeyConfiguration.Save();
                    RefreshBinding();
                    RefreshUpdateButton();
                }
            }
        }

        private ShortKey GetSelectedItem()
        {
            return (ShortKey)gridShortKeys.SelectedRows[0].DataBoundItem;
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            ShortKey key = GetSelectedItem();
            ShortKeyConfiguration.Instance.ShortKeys.Remove(key);
            await ShortKeyConfiguration.Save();
            RefreshBinding();
        }
    }
}
