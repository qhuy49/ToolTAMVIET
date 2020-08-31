using System.Windows.Forms;
using DevExpress.XtraEditors;
using MinvoiceLoadDataMisa.Services;

namespace MinvoiceLoadDataMisa.Forms
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (CommonService.CheckLogin(txtPass.Text))
                {
                    Close();
                    FrmSettingSystem frmSettingSystem = new FrmSettingSystem();
                    frmSettingSystem.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("Mật khẩu không chính xác", "Lỗi", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

        }

        private void FrmLogin_Load(object sender, System.EventArgs e)
        {

        }
    }
}