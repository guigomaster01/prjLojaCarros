using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjLojaCarros
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void marcaVeículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMarca marca = new frmMarca();
            marca.Show();
        }
    }
}
