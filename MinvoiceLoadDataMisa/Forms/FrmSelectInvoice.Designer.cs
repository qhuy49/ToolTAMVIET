namespace MinvoiceLoadDataMisa.Forms
{
    partial class FrmSelectInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectInvoice));
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.slokMauHoaDon1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mau_so = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ky_hieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCall = new DevExpress.XtraEditors.CheckButton();
            this.btnGetData = new DevExpress.XtraEditors.SimpleButton();
            this.dteDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dteTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slokMauHoaDon1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2013";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnUpdate);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 338);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(681, 41);
            this.panelControl1.TabIndex = 1;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(152, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 30);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(399, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.slokMauHoaDon1);
            this.panelControl2.Controls.Add(this.btnCall);
            this.panelControl2.Controls.Add(this.btnGetData);
            this.panelControl2.Controls.Add(this.dteDenNgay);
            this.panelControl2.Controls.Add(this.dteTuNgay);
            this.panelControl2.Controls.Add(this.chkAll);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(681, 37);
            this.panelControl2.TabIndex = 3;
            // 
            // slokMauHoaDon1
            // 
            this.slokMauHoaDon1.EditValue = "";
            this.slokMauHoaDon1.Location = new System.Drawing.Point(264, 8);
            this.slokMauHoaDon1.Name = "slokMauHoaDon1";
            this.slokMauHoaDon1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slokMauHoaDon1.Properties.NullText = "";
            this.slokMauHoaDon1.Properties.View = this.searchLookUpEdit1View;
            this.slokMauHoaDon1.Size = new System.Drawing.Size(210, 20);
            this.slokMauHoaDon1.TabIndex = 64;
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
            // btnCall
            // 
            this.btnCall.Image = ((System.Drawing.Image)(resources.GetObject("btnCall.Image")));
            this.btnCall.Location = new System.Drawing.Point(480, 4);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(81, 30);
            this.btnCall.TabIndex = 4;
            this.btnCall.Text = "Load ";
            this.btnCall.CheckedChanged += new System.EventHandler(this.btnCall_CheckedChanged);
            // 
            // btnGetData
            // 
            this.btnGetData.Image = ((System.Drawing.Image)(resources.GetObject("btnGetData.Image")));
            this.btnGetData.Location = new System.Drawing.Point(581, 4);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(88, 29);
            this.btnGetData.TabIndex = 2;
            this.btnGetData.Text = "Tải dữ liệu";
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // dteDenNgay
            // 
            this.dteDenNgay.EditValue = null;
            this.dteDenNgay.Location = new System.Drawing.Point(143, 9);
            this.dteDenNgay.Name = "dteDenNgay";
            this.dteDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDenNgay.Size = new System.Drawing.Size(111, 20);
            this.dteDenNgay.TabIndex = 2;
            // 
            // dteTuNgay
            // 
            this.dteTuNgay.EditValue = null;
            this.dteTuNgay.Location = new System.Drawing.Point(26, 9);
            this.dteTuNgay.Name = "dteTuNgay";
            this.dteTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTuNgay.Size = new System.Drawing.Size(111, 20);
            this.dteTuNgay.TabIndex = 1;
            // 
            // chkAll
            // 
            this.chkAll.Location = new System.Drawing.Point(3, 9);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Caption = "";
            this.chkAll.Size = new System.Drawing.Size(17, 19);
            this.chkAll.TabIndex = 0;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // checkedListBoxControl1
            // 
            this.checkedListBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxControl1.Location = new System.Drawing.Point(0, 37);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.checkedListBoxControl1.Size = new System.Drawing.Size(681, 301);
            this.checkedListBoxControl1.TabIndex = 4;
            // 
            // FrmSelectInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(681, 379);
            this.Controls.Add(this.checkedListBoxControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách hóa đơn";
            this.Load += new System.EventHandler(this.FrmSelectInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.slokMauHoaDon1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CheckEdit chkAll;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl1;
        private DevExpress.XtraEditors.SimpleButton btnGetData;
        private DevExpress.XtraEditors.DateEdit dteDenNgay;
        private DevExpress.XtraEditors.DateEdit dteTuNgay;
        private DevExpress.XtraEditors.CheckButton btnCall;
        private DevExpress.XtraEditors.SearchLookUpEdit slokMauHoaDon1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn id;
        private DevExpress.XtraGrid.Columns.GridColumn mau_so;
        private DevExpress.XtraGrid.Columns.GridColumn ky_hieu;
    }
}