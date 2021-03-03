namespace LogIO
{
    partial class FmLogViewer
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._btnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._btnRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this._btnClear = new System.Windows.Forms.ToolStripMenuItem();
            this._tbxLog = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.toolTToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(454, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnSave,
            this.toolStripSeparator1,
            this._btnExit});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // _btnSave
            // 
            this._btnSave.Name = "_btnSave";
            this._btnSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this._btnSave.Size = new System.Drawing.Size(160, 22);
            this._btnSave.Text = "Save(&S)...";
            this._btnSave.Click += new System.EventHandler(this._btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // _btnExit
            // 
            this._btnExit.Name = "_btnExit";
            this._btnExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this._btnExit.Size = new System.Drawing.Size(160, 22);
            this._btnExit.Text = "Exit(&X)";
            this._btnExit.Click += new System.EventHandler(this._btnExit_Click);
            // 
            // toolTToolStripMenuItem
            // 
            this.toolTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnRefresh,
            this._btnClear});
            this.toolTToolStripMenuItem.Name = "toolTToolStripMenuItem";
            this.toolTToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.toolTToolStripMenuItem.Text = "Tool(&T)";
            // 
            // _btnRefresh
            // 
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this._btnRefresh.Size = new System.Drawing.Size(172, 22);
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.Click += new System.EventHandler(this._btnRefresh_Click);
            // 
            // _btnClear
            // 
            this._btnClear.Name = "_btnClear";
            this._btnClear.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this._btnClear.Size = new System.Drawing.Size(172, 22);
            this._btnClear.Text = "Clear";
            this._btnClear.Click += new System.EventHandler(this._btnClear_Click);
            // 
            // _tbxLog
            // 
            this._tbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tbxLog.Location = new System.Drawing.Point(0, 24);
            this._tbxLog.Multiline = true;
            this._tbxLog.Name = "_tbxLog";
            this._tbxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._tbxLog.Size = new System.Drawing.Size(454, 426);
            this._tbxLog.TabIndex = 2;
            // 
            // FmLogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 450);
            this.Controls.Add(this._tbxLog);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FmLogViewer";
            this.Text = "LOG";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmLogViewer_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.FmLogViewer_VisibleChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _btnExit;
        private System.Windows.Forms.ToolStripMenuItem toolTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _btnRefresh;
        private System.Windows.Forms.ToolStripMenuItem _btnClear;
        private System.Windows.Forms.TextBox _tbxLog;
    }
}