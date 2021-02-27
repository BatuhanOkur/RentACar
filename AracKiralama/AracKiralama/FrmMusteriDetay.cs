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
    public partial class FrmMusteriDetay : Form
    {
        public FrmMusteriDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmMusteriDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;
            //Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("select MusteriAd,MusteriSoyad from Tbl_Musteriler where MusteriTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //Kullanılabilir Araçları Dgye aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select AracMarka as Marka,AracModel as Model,AracVites as VitesTipi,AracYakit as YakıtTipi,AracKmSinir as KMSiniri,AracGunlukUcret as GunlukUcret,AracDepozito as Depozito from Tbl_Araclar where AracDurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Cmbye araçları çekme
            SqlCommand komut2 = new SqlCommand("select AracMarka,AracModel from Tbl_Araclar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbArac.Items.Add(dr2[0] + " " + dr2[1]);
            }
            bgl.baglanti().Close();

            //Müsait zaman dilimlerini dgye çekme
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select Randevuid as ID,RandevuTarih as Tarih,RandevuSaat as Saat from Tbl_Randevular where RandevuDurum=0", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            cmbArac.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            mskGun.Text = dataGridView2.Rows[secilen].Cells[1].Value.ToString();
            mskSaat.Text = dataGridView2.Rows[secilen].Cells[2].Value.ToString();
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Randevular set RandevuDurum=1,MusteriTC=@p1,RandevuArac=@p2,RandevuDetay=@p3 where Randevuid=@p4 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            komut.Parameters.AddWithValue("@p2", cmbArac.Text);
            komut.Parameters.AddWithValue("@p3", rchDetay.Text);
            komut.Parameters.AddWithValue("@p4", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRandevularim_Click(object sender, EventArgs e)
        {
            FrmRandevularim fr = new FrmRandevularim();
            fr.tc = lblTC.Text;
            fr.Show();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Kullanılabilir Araçları Dgye aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select AracMarka as Marka,AracModel as Model,AracVites as VitesTipi,AracYakit as YakıtTipi,AracKmSinir as KMSiniri,AracGunlukUcret as GunlukUcret,AracDepozito as Depozito from Tbl_Araclar where AracDurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Müsait zaman dilimlerini dgye çekme
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select Randevuid as ID,RandevuTarih as Tarih,RandevuSaat as Saat from Tbl_Randevular where RandevuDurum=0", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            rchDetay.Text = "";
            cmbArac.Text = "";
            mskGun.Text = "";
            mskSaat.Text = "";
            rchDetay.Focus();
        }
        
        private void btnKiralikAraçlar_Click(object sender, EventArgs e)
        {
            FrmKiralikAraclarim fr = new FrmKiralikAraclarim();
            fr.tc = lblTC.Text;
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmMusteriKiralamaGecmisi fr = new FrmMusteriKiralamaGecmisi();
            fr.tc = lblTC.Text;
            fr.Show();
        }
    }
}
