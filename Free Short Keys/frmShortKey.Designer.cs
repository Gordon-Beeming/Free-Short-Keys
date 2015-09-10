using System;

namespace Free_Short_Keys
{
    partial class frmShortKey
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShortKey));
            this.label1 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.shortKeyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkUseClipboard = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxCategory = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxInsertKey = new System.Windows.Forms.ComboBox();
            this.txtReplacementKey = new System.Windows.Forms.TextBox();
            this.chkSaveRepalcementKeyCursorPosition = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.shortKeyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Key";
            // 
            // txtKey
            // 
            this.txtKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKey.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.shortKeyBindingSource, "Key", true));
            this.txtKey.Location = new System.Drawing.Point(102, 43);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(376, 25);
            this.txtKey.TabIndex = 3;
            // 
            // shortKeyBindingSource
            // 
            this.shortKeyBindingSource.DataSource = typeof(Free_Short_Keys.ShortKey);
            // 
            // txtSuffix
            // 
            this.txtSuffix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSuffix.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.shortKeyBindingSource, "Suffix", true));
            this.txtSuffix.Location = new System.Drawing.Point(102, 74);
            this.txtSuffix.MaxLength = 2;
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(376, 25);
            this.txtSuffix.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Suffix";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Replacement Key";
            // 
            // txtNotes
            // 
            this.txtNotes.AcceptsReturn = true;
            this.txtNotes.AcceptsTab = true;
            this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotes.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.shortKeyBindingSource, "Notes", true));
            this.txtNotes.Location = new System.Drawing.Point(11, 311);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(468, 95);
            this.txtNotes.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Notes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Use Clipboard";
            // 
            // chkUseClipboard
            // 
            this.chkUseClipboard.AutoSize = true;
            this.chkUseClipboard.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.shortKeyBindingSource, "UseClipboard", true));
            this.chkUseClipboard.Location = new System.Drawing.Point(102, 110);
            this.chkUseClipboard.Name = "chkUseClipboard";
            this.chkUseClipboard.Size = new System.Drawing.Size(15, 14);
            this.chkUseClipboard.TabIndex = 7;
            this.chkUseClipboard.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(404, 412);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(323, 412);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Category";
            // 
            // cbxCategory
            // 
            this.cbxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxCategory.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.shortKeyBindingSource, "Category", true));
            this.cbxCategory.FormattingEnabled = true;
            this.cbxCategory.Location = new System.Drawing.Point(102, 12);
            this.cbxCategory.Name = "cbxCategory";
            this.cbxCategory.Size = new System.Drawing.Size(377, 25);
            this.cbxCategory.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(171, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "Special Keys";
            // 
            // cbxInsertKey
            // 
            this.cbxInsertKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxInsertKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxInsertKey.FormattingEnabled = true;
            this.cbxInsertKey.Location = new System.Drawing.Point(254, 125);
            this.cbxInsertKey.Name = "cbxInsertKey";
            this.cbxInsertKey.Size = new System.Drawing.Size(224, 25);
            this.cbxInsertKey.TabIndex = 10;
            this.cbxInsertKey.SelectedIndexChanged += new System.EventHandler(this.cbxInsertKey_SelectedIndexChanged);
            // 
            // txtReplacementKey
            // 
            this.txtReplacementKey.AcceptsReturn = true;
            this.txtReplacementKey.AcceptsTab = true;
            this.txtReplacementKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplacementKey.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.shortKeyBindingSource, "ReplacementKey", true));
            this.txtReplacementKey.Location = new System.Drawing.Point(12, 153);
            this.txtReplacementKey.Multiline = true;
            this.txtReplacementKey.Name = "txtReplacementKey";
            this.txtReplacementKey.Size = new System.Drawing.Size(467, 135);
            this.txtReplacementKey.TabIndex = 11;
            // 
            // chkSaveRepalcementKeyCursorPosition
            // 
            this.chkSaveRepalcementKeyCursorPosition.AutoSize = true;
            this.chkSaveRepalcementKeyCursorPosition.Location = new System.Drawing.Point(12, 414);
            this.chkSaveRepalcementKeyCursorPosition.Name = "chkSaveRepalcementKeyCursorPosition";
            this.chkSaveRepalcementKeyCursorPosition.Size = new System.Drawing.Size(235, 21);
            this.chkSaveRepalcementKeyCursorPosition.TabIndex = 14;
            this.chkSaveRepalcementKeyCursorPosition.Text = "Save replacement key cursor position";
            this.chkSaveRepalcementKeyCursorPosition.UseVisualStyleBackColor = true;
            // 
            // frmShortKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(491, 447);
            this.Controls.Add(this.chkSaveRepalcementKeyCursorPosition);
            this.Controls.Add(this.cbxInsertKey);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxCategory);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkUseClipboard);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtReplacementKey);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSuffix);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmShortKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Short Key - BETA";
            this.Load += new System.EventHandler(this.frmShortKey_Load);
            ((System.ComponentModel.ISupportInitialize)(this.shortKeyBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkUseClipboard;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.BindingSource shortKeyBindingSource;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxCategory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxInsertKey;
        private System.Windows.Forms.TextBox txtReplacementKey;
        private System.Windows.Forms.CheckBox chkSaveRepalcementKeyCursorPosition;
    }
}