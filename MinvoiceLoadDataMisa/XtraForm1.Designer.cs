namespace MinvoiceLoadDataMisa
{
    partial class XtraForm1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraForm1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lấyDữLiệuNgayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thoátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnSetupDatabase = new DevExpress.XtraEditors.SimpleButton();
            this.btnGetInvoiceByDate = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnSettingSystem = new DevExpress.XtraEditors.SimpleButton();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Lấy dữ liệu Misa";
            this.notifyIcon1.BalloonTipTitle = "Lấy dữ liệu Misa";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Lấy dữ liệu Misa";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lấyDữLiệuNgayToolStripMenuItem,
            this.thoátToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 48);
            // 
            // lấyDữLiệuNgayToolStripMenuItem
            // 
            this.lấyDữLiệuNgayToolStripMenuItem.Name = "lấyDữLiệuNgayToolStripMenuItem";
            this.lấyDữLiệuNgayToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.lấyDữLiệuNgayToolStripMenuItem.Text = "Lấy dữ liệu ngay";
            this.lấyDữLiệuNgayToolStripMenuItem.Click += new System.EventHandler(this.lấyDữLiệuNgayToolStripMenuItem_Click);
            // 
            // thoátToolStripMenuItem
            // 
            this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            this.thoátToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.thoátToolStripMenuItem.Text = "Thoát";
            this.thoátToolStripMenuItem.Click += new System.EventHandler(this.thoátToolStripMenuItem_Click);
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Location = new System.Drawing.Point(12, 168);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(235, 19);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "Tổng đài hỗ trợ 24/7: 0901 80 16 18";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Location = new System.Drawing.Point(12, 193);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(190, 32);
            this.labelControl1.TabIndex = 26;
            this.labelControl1.Text = "          Copyright © M-Invoice 2020      \r\nUpdate : 25 - 08 - 2020";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnSetupDatabase
            // 
            this.btnSetupDatabase.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetupDatabase.Appearance.Options.UseFont = true;
            this.btnSetupDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btnSetupDatabase.Image")));
            this.btnSetupDatabase.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnSetupDatabase.Location = new System.Drawing.Point(291, 178);
            this.btnSetupDatabase.Name = "btnSetupDatabase";
            this.btnSetupDatabase.Size = new System.Drawing.Size(43, 40);
            this.btnSetupDatabase.TabIndex = 29;
            this.btnSetupDatabase.Text = "Connect Database";
            this.btnSetupDatabase.Click += new System.EventHandler(this.btnSetupDatabase_Click);
            // 
            // btnGetInvoiceByDate
            // 
            this.btnGetInvoiceByDate.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetInvoiceByDate.Appearance.Options.UseFont = true;
            this.btnGetInvoiceByDate.Image = ((System.Drawing.Image)(resources.GetObject("btnGetInvoiceByDate.Image")));
            this.btnGetInvoiceByDate.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnGetInvoiceByDate.Location = new System.Drawing.Point(12, 100);
            this.btnGetInvoiceByDate.Name = "btnGetInvoiceByDate";
            this.btnGetInvoiceByDate.Size = new System.Drawing.Size(126, 62);
            this.btnGetInvoiceByDate.TabIndex = 33;
            this.btnGetInvoiceByDate.Text = "Load Invoice";
            this.btnGetInvoiceByDate.Click += new System.EventHandler(this.btnGetInvoiceByDate_Click_1);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Appearance.Options.UseFont = true;
            this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
            this.btnUpdate.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnUpdate.Location = new System.Drawing.Point(151, 100);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(107, 62);
            this.btnUpdate.TabIndex = 31;
            this.btnUpdate.Text = "Update Invoice";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSettingSystem
            // 
            this.btnSettingSystem.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettingSystem.Appearance.Options.UseFont = true;
            this.btnSettingSystem.Image = ((System.Drawing.Image)(resources.GetObject("btnSettingSystem.Image")));
            this.btnSettingSystem.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnSettingSystem.Location = new System.Drawing.Point(340, 178);
            this.btnSettingSystem.Name = "btnSettingSystem";
            this.btnSettingSystem.Size = new System.Drawing.Size(41, 40);
            this.btnSettingSystem.TabIndex = 30;
            this.btnSettingSystem.Text = "Configs";
            this.btnSettingSystem.Click += new System.EventHandler(this.btnSettingSystem_Click);
            // 
            // btnStop
            // 
            this.btnStop.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Appearance.Options.UseFont = true;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnStop.Location = new System.Drawing.Point(275, 100);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(101, 62);
            this.btnStop.TabIndex = 25;
            this.btnStop.Text = "Dừng";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelControl1.ContentImage = global::MinvoiceLoadDataMisa.Properties.Resources.LogoAppMinvoice;
            this.panelControl1.Location = new System.Drawing.Point(2, 1);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(390, 93);
            this.panelControl1.TabIndex = 23;
            this.panelControl1.UseDisabledStatePainter = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl4.Location = new System.Drawing.Point(151, 208);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(108, 17);
            this.labelControl4.TabIndex = 35;
            this.labelControl4.Text = "Version : 1.4.0.1";
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(393, 229);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.btnSetupDatabase);
            this.Controls.Add(this.btnGetInvoiceByDate);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSettingSystem);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Office 2013";
            this.MaximizeBox = false;
            this.Name = "XtraForm1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hóa đơn điện tử M-Invoice";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem lấyDữLiệuNgayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thoátToolStripMenuItem;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnStop;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSetupDatabase;
        private DevExpress.XtraEditors.SimpleButton btnSettingSystem;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.SimpleButton btnGetInvoiceByDate;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}