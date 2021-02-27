using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AracKiralama
{
    public partial class FrmYetkiliKiraGecmisi : Form
    {
        public FrmYetkiliKiraGecmisi()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmYetkiliKiraGecmisi_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Kayıtid as KayıtID,AracMarka as Marka,AracModel as Model,KiralamaBaslangic,KiralamaBitis,KiralamaUcret as Ucret,KiralamaDepozito as Depozito,KiralayanTC as MusteriTC from Tbl_KiraGecmisi", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
