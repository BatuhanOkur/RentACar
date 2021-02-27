using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralama
{
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        private void btnMusteriGiris_Click(object sender, EventArgs e)
        {
            FrmMusteriGiris fr = new FrmMusteriGiris();
            fr.Show();
            this.Hide();
        }

        private void btnYetkiliGiris_Click(object sender, EventArgs e)
        {
            FrmYetkiliGiris fr = new FrmYetkiliGiris();
            fr.Show();
            this.Hide();
        }
    }
}
