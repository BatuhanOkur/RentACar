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
    public partial class FrmKiralikAraclarim : Form
    {
        public FrmKiralikAraclarim()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string tc;
        private void FrmKiralikAraclarim_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select AracMarka as Marka,AracModel as Model,AracGunlukUcret as GunlukUcret,AracDepozito as Depozito,AracKiraBaslangic as BaslangıcTarihi,AracKiraBitis as BitisTarihi from Tbl_Araclar where AracDurum=1 and AracKiralayanTC="+tc, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
