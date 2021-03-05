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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmLogViewer));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._btnSwitchFile = new System.Windows.Forms.ToolStripMenuItem();
            this._btnOpenDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this._btnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._btnRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this._btnTogleAutoRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._btnClear = new System.Windows.Forms.ToolStripMenuItem();
            this._tbxLog = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.toolTToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            resources.ApplyResources(this.fileFToolStripMenuItem, "fileFToolStripMenuItem");
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnSwitchFile,
            this._btnOpenDirectory,
            this._btnSave,
            this.toolStripSeparator1,
            this._btnExit});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            // 
            // _btnSwitchFile
            // 
            resources.ApplyResources(this._btnSwitchFile, "_btnSwitchFile");
            this._btnSwitchFile.Name = "_btnSwitchFile";
            this._btnSwitchFile.Click += new System.EventHandler(this._btnSwitchFile_Click);
            // 
            // _btnOpenDirectory
            // 
            resources.ApplyResources(this._btnOpenDirectory, "_btnOpenDirectory");
            this._btnOpenDirectory.Name = "_btnOpenDirectory";
            this._btnOpenDirectory.Click += new System.EventHandler(this._btnOpenDirectory_Click);
            // 
            // _btnSave
            // 
            resources.ApplyResources(this._btnSave, "_btnSave");
            this._btnSave.Name = "_btnSave";
            this._btnSave.Click += new System.EventHandler(this._btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // _btnExit
            // 
            resources.ApplyResources(this._btnExit, "_btnExit");
            this._btnExit.Name = "_btnExit";
            this._btnExit.Click += new System.EventHandler(this._btnExit_Click);
            // 
            // toolTToolStripMenuItem
            // 
            resources.ApplyResources(this.toolTToolStripMenuItem, "toolTToolStripMenuItem");
            this.toolTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnRefresh,
            this._btnTogleAutoRefresh,
            this.toolStripSeparator2,
            this._btnClear});
            this.toolTToolStripMenuItem.Name = "toolTToolStripMenuItem";
            // 
            // _btnRefresh
            // 
            resources.ApplyResources(this._btnRefresh, "_btnRefresh");
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Click += new System.EventHandler(this._btnRefresh_Click);
            // 
            // _btnTogleAutoRefresh
            // 
            resources.ApplyResources(this._btnTogleAutoRefresh, "_btnTogleAutoRefresh");
            this._btnTogleAutoRefresh.Checked = true;
            this._btnTogleAutoRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btnTogleAutoRefresh.Name = "_btnTogleAutoRefresh";
            this._btnTogleAutoRefresh.CheckedChanged += new System.EventHandler(this._btnTogleAutoRefresh_CheckedChanged);
            this._btnTogleAutoRefresh.Click += new System.EventHandler(this._btnTogleAutoRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // _btnClear
            // 
            resources.ApplyResources(this._btnClear, "_btnClear");
            this._btnClear.Name = "_btnClear";
            this._btnClear.Click += new System.EventHandler(this._btnClear_Click);
            // 
            // _tbxLog
            // 
            resources.ApplyResources(this._tbxLog, "_tbxLog");
            this._tbxLog.Name = "_tbxLog";
            // 
            // FmLogViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._tbxLog);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FmLogViewer";
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
        private System.Windows.Forms.ToolStripMenuItem _btnOpenDirectory;
        private System.Windows.Forms.ToolStripMenuItem _btnTogleAutoRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem _btnSwitchFile;
    }
}