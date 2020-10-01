namespace MinvoiceLoadDataMisa.Forms
{
    partial class FrmGetDate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetDate));
            this.btnGetData = new DevExpress.XtraEditors.SimpleButton();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.txtCodeId = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnGetMau = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMauSoHD = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mau_so = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ky_hieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtKyHieuHD = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMauSoHD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKyHieuHD.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetData
            // 
            this.btnGetData.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetData.Appearance.Options.UseFont = true;
            this.btnGetData.Image = ((System.Drawing.Image)(resources.GetObject("btnGetData.Image")));
            this.btnGetData.Location = new System.Drawing.Point(72, 142);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(162, 42);
            this.btnGetData.TabIndex = 5;
            this.btnGetData.Text = "UpLoad";
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(72, 3);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(162, 20);
            this.dtpTuNgay.TabIndex = 6;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(72, 33);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(162, 20);
            this.dtpDenNgay.TabIndex = 7;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(12, 9);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(49, 13);
            this.lblTuNgay.TabIndex = 8;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(12, 39);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(56, 13);
            this.lblDenNgay.TabIndex = 9;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // txtCodeId
            // 
            this.txtCodeId.Location = new System.Drawing.Point(72, 114);
            this.txtCodeId.Name = "txtCodeId";
            this.txtCodeId.Size = new System.Drawing.Size(121, 20);
            this.txtCodeId.TabIndex = 77;
            this.txtCodeId.EditValueChanged += new System.EventHandler(this.slokMauHoaDon_EditValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 76;
            this.label3.Text = "Code_ID";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(199, 113);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(35, 23);
            this.simpleButton1.TabIndex = 75;
            this.simpleButton1.Text = "Lưu";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnGetMau
            // 
            this.btnGetMau.Location = new System.Drawing.Point(199, 57);
            this.btnGetMau.Name = "btnGetMau";
            this.btnGetMau.Size = new System.Drawing.Size(35, 23);
            this.btnGetMau.TabIndex = 74;
            this.btnGetMau.Text = "Lấy";
            this.btnGetMau.Click += new System.EventHandler(this.btnGetMau_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "Mẫu số:";
            // 
            // txtMauSoHD
            // 
            this.txtMauSoHD.EditValue = "";
            this.txtMauSoHD.Location = new System.Drawing.Point(72, 59);
            this.txtMauSoHD.Name = "txtMauSoHD";
            this.txtMauSoHD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtMauSoHD.Properties.NullText = "";
            this.txtMauSoHD.Properties.View = this.searchLookUpEdit1View;
            this.txtMauSoHD.Size = new System.Drawing.Size(121, 20);
            this.txtMauSoHD.TabIndex = 72;
            this.txtMauSoHD.EditValueChanged += new System.EventHandler(this.slokMauHoaDon_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.id,
            this.mau_so,
            this.ky_hieu});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Default;
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // id
            // 
            this.id.Caption = "Invoice code id";
            this.id.FieldName = "Id";
            this.id.Name = "id";
            this.id.Visible = true;
            this.id.VisibleIndex = 0;
            // 
            // mau_so
            // 
            this.mau_so.Caption = "Mẫu số";
            this.mau_so.FieldName = "MauSo";
            this.mau_so.Name = "mau_so";
            this.mau_so.Visible = true;
            this.mau_so.VisibleIndex = 1;
            // 
            // ky_hieu
            // 
            this.ky_hieu.Caption = "Ký hiệu";
            this.ky_hieu.FieldName = "KyHieu";
            this.ky_hieu.Name = "ky_hieu";
            this.ky_hieu.Visible = true;
            this.ky_hieu.VisibleIndex = 2;
            // 
            // txtKyHieuHD
            // 
            this.txtKyHieuHD.Location = new System.Drawing.Point(72, 88);
            this.txtKyHieuHD.Name = "txtKyHieuHD";
            this.txtKyHieuHD.Size = new System.Drawing.Size(121, 20);
            this.txtKyHieuHD.TabIndex = 71;
            this.txtKyHieuHD.EditValueChanged += new System.EventHandler(this.slokMauHoaDon_EditValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 70;
            this.label2.Text = "Ký hiệu:";
            // 
            // FrmGetDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 194);
            this.Controls.Add(this.txtCodeId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnGetMau);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMauSoHD);
            this.Controls.Add(this.txtKyHieuHD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDenNgay);
            this.Controls.Add(this.lblTuNgay);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.btnGetData);
            this.Name = "FrmGetDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmGetDate";
            this.Load += new System.EventHandler(this.FrmGetDate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMauSoHD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKyHieuHD.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnGetData;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private DevExpress.XtraEditors.TextEdit txtCodeId;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnGetMau;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit txtMauSoHD;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn id;
        private DevExpress.XtraGrid.Columns.GridColumn mau_so;
        private DevExpress.XtraGrid.Columns.GridColumn ky_hieu;
        private DevExpress.XtraEditors.TextEdit txtKyHieuHD;
        private System.Windows.Forms.Label label2;
    }
}