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
    public partial class FrmMusteriKiralamaGecmisi : Form
    {
        public FrmMusteriKiralamaGecmisi()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmMusteriKiralamaGecmisi_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Kayıtid as KayıtID,AracMarka as Marka,AracModel as Model,KiralamaBaslangic,KiralamaBitis,KiralamaUcret as Ucret,KiralamaDepozito as Depozito from Tbl_KiraGecmisi where KiralayanTC=" + tc, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
