using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace MinvoiceLoadDataMisa.Forms
{
    public partial class GridView : DevExpress.XtraEditors.XtraForm
    {

        public DataTable _readLog2;
        public GridView()
        {
            InitializeComponent();
          
        }


        private void GridView_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = _readLog2;
        }
    }
}