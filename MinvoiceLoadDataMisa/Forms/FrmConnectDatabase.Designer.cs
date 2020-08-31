namespace MinvoiceLoadDataMisa.Forms
{
    partial class FrmConnectDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConnectDatabase));
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cboeAuthentication = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnLoadDatabaseName = new DevExpress.XtraEditors.SimpleButton();
            this.lokDatabaseName = new DevExpress.XtraEditors.LookUpEdit();
            this.txtServerName = new DevExpress.XtraEditors.TextEdit();
            this.btnTestConnection = new DevExpress.XtraEditors.SimpleButton();
            this.brnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboeAuthentication.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lokDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2013";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Server name";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cboeAuthentication);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtPassword);
            this.groupControl1.Controls.Add(this.txtUsername);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Location = new System.Drawing.Point(22, 67);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(285, 148);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Log on the server";
            // 
            // cboeAuthentication
            // 
            this.cboeAuthentication.EditValue = "Windows Authentication";
            this.cboeAuthentication.Location = new System.Drawing.Point(95, 32);
            this.cboeAuthentication.Name = "cboeAuthentication";
            this.cboeAuthentication.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboeAuthentication.Properties.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.cboeAuthentication.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboeAuthentication.Size = new System.Drawing.Size(174, 20);
            this.cboeAuthentication.TabIndex = 11;
            this.cboeAuthentication.SelectedIndexChanged += new System.EventHandler(this.cboeAuthentication_SelectedIndexChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(19, 35);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(70, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Authentication";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(94, 99);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(175, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(94, 68);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(175, 20);
            this.txtUsername.TabIndex = 4;
            this.txtUsername.EditValueChanged += new System.EventHandler(this.txtUsername_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(19, 102);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Password";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(19, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Username";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnLoadDatabaseName);
            this.groupControl2.Controls.Add(this.lokDatabaseName);
            this.groupControl2.Location = new System.Drawing.Point(22, 221);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(285, 89);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "Connect to database";
            // 
            // btnLoadDatabaseName
            // 
            this.btnLoadDatabaseName.Enabled = false;
            this.btnLoadDatabaseName.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadDatabaseName.Image")));
            this.btnLoadDatabaseName.Location = new System.Drawing.Point(19, 24);
            this.btnLoadDatabaseName.Name = "btnLoadDatabaseName";
            this.btnLoadDatabaseName.Size = new System.Drawing.Size(137, 23);
            this.btnLoadDatabaseName.TabIndex = 12;
            this.btnLoadDatabaseName.Text = "Load Database Name";
            this.btnLoadDatabaseName.Click += new System.EventHandler(this.btnLoadDatabaseName_Click);
            // 
            // lokDatabaseName
            // 
            this.lokDatabaseName.Enabled = false;
            this.lokDatabaseName.Location = new System.Drawing.Point(19, 57);
            this.lokDatabaseName.Name = "lokDatabaseName";
            this.lokDatabaseName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lokDatabaseName.Properties.NullText = "";
            this.lokDatabaseName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lokDatabaseName.Size = new System.Drawing.Size(250, 20);
            this.lokDatabaseName.TabIndex = 9;
            this.lokDatabaseName.EditValueChanged += new System.EventHandler(this.lokDatabaseName_EditValueChanged);
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(22, 38);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(285, 20);
            this.txtServerName.TabIndex = 8;
            this.txtServerName.EditValueChanged += new System.EventHandler(this.txtServerName_EditValueChanged);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Image = ((System.Drawing.Image)(resources.GetObject("btnTestConnection.Image")));
            this.btnTestConnection.Location = new System.Drawing.Point(193, 327);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(114, 23);
            this.btnTestConnection.TabIndex = 7;
            this.btnTestConnection.Text = "Test connections";
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // brnCancel
            // 
            this.brnCancel.Image = ((System.Drawing.Image)(resources.GetObject("brnCancel.Image")));
            this.brnCancel.Location = new System.Drawing.Point(103, 327);
            this.brnCancel.Name = "brnCancel";
            this.brnCancel.Size = new System.Drawing.Size(75, 23);
            this.brnCancel.TabIndex = 6;
            this.brnCancel.Text = "Cancel";
            this.brnCancel.Click += new System.EventHandler(this.brnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(22, 327);
            this.btnOk.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FrmConnectDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 370);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.brnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmConnectDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Server Connection";
            this.Load += new System.EventHandler(this.FrmConnectDatabase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboeAuthentication.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lokDatabaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton brnCancel;
        private DevExpress.XtraEditors.SimpleButton btnTestConnection;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lokDatabaseName;
        private DevExpress.XtraEditors.TextEdit txtServerName;
        private DevExpress.XtraEditors.ComboBoxEdit cboeAuthentication;
        private DevExpress.XtraEditors.SimpleButton btnLoadDatabaseName;
    }
}