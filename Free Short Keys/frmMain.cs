﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        public static frmMain Me { get; set; }

        public frmMain()
        {
            Me = this;

            InitializeComponent();
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {
            await ShortKeyConfiguration.Start();
            txtSuffix.Text = ShortKeyConfiguration.Default.Prefix;
            RefreshAll();
        }

        private void RefreshBinding()
        {
            gridShortKeys.DataSource = null;
            gridShortKeys.DataSource = ShortKeyConfiguration.GetShortKeys();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            frmShortKey frm = new frmShortKey();
            frm.SetShortKey(new ShortKey(), true);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                await ShortKeyConfiguration.AddShortKey(frm.GetShortKey());
                RefreshAll();
            }
        }

        private void RefreshAll()
        {
            RefreshBinding();
            RefreshUpdateButton();
            RefreshMenuItems();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            ShortKeyConfiguration.Default.Prefix = txtSuffix.Text;
            await ShortKeyConfiguration.Save();
            RefreshUpdateButton();
        }

        private void txtSuffix_TextChanged(object sender, EventArgs e)
        {
            RefreshUpdateButton();
        }

        private void RefreshUpdateButton()
        {
            btnUpdate.Enabled = string.Compare(ShortKeyConfiguration.Default.Prefix, txtSuffix.Text, StringComparison.InvariantCultureIgnoreCase) != 0;
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (gridShortKeys.SelectedRows.Count > 0)
            {
                frmShortKey frm = new frmShortKey();
                frm.SetShortKey(GetSelectedItem(), false);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await ShortKeyConfiguration.UpdateShortKey(frm.GetShortKey());
                    RefreshAll();
                }
            }
        }

        private ShortKey GetSelectedItem()
        {
            if (gridShortKeys.SelectedRows.Count > 0)
            {
                return (ShortKey)gridShortKeys.SelectedRows[0].DataBoundItem;
            }
            return null;
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            await ShortKeyConfiguration.RemoveShortKey(GetSelectedItem());
            RefreshBinding();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        private void openStoragePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShortKeyConfiguration.GetDefaultConfigurationDirectory().Length > 0)
            {
                Process.Start(ShortKeyConfiguration.GetDefaultConfigurationDirectory());
            }
        }

        private async void logKeysdebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShortKeyConfiguration.Default.LogKeysDebug = !ShortKeyConfiguration.Default.LogKeysDebug;
            await ShortKeyConfiguration.Save();
            RefreshMenuItems();
        }

        private void RefreshMenuItems()
        {
            openStoragePathToolStripMenuItem.Enabled = ShortKeyConfiguration.GetDefaultConfigurationDirectory().Length > 0;
            logKeysdebugToolStripMenuItem.Checked = ShortKeyConfiguration.Default.LogKeysDebug;
            flushKeysToLogToolStripMenuItem.Enabled = ShortKeyConfiguration.Default.LogKeysDebug;
        }

        private void flushKeysToLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShortKeyConfiguration.FlushLogs();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                ShowHideApp(true);
            }
            else
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowHideApp(false);
        }

        private void ShowHideApp(bool hideApp)
        {
            this.Visible = !hideApp;
            notifyIcon1.Visible = hideApp;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
