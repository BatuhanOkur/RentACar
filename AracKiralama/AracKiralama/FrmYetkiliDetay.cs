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
    public partial class FrmYetkiliDetay : Form
    {
        public FrmYetkiliDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmYetkiliDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;
            //Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("select YetkiliAd,YetkiliSoyad from Tbl_Yetkililer where YetkiliTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //Kullanımda Olan Araçları Dgye aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Aracid as ID,AracMarka as Marka,AracModel as Model,AracGunlukUcret as GunlukUcret,AracDepozito as Depozito,AracKiraBaslangic as BaslangıcTarihi,AracKiraBitis as BitisTarihi,AracKiralayanTC as MusteriTC from Tbl_Araclar where AracDurum=1", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Kullanımda Olmayan Araçları Dgye aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select Aracid as ID,AracMarka as Marka,AracModel as Model,AracGunlukUcret as GunlukUcret,AracDepozito as Depozito from Tbl_Araclar where AracDurum=0", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtMarka.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtModel.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtUcret.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtDepozito.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            mskKiraBaslangic.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            mskKiraBitis.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            mskTC.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            SqlCommand komut = new SqlCommand("select MusteriAd,MusteriSoyad from Tbl_Musteriler where MusteriTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            txtMarka.Text = dataGridView2.Rows[secilen].Cells[1].Value.ToString();
            txtModel.Text = dataGridView2.Rows[secilen].Cells[2].Value.ToString();
            txtUcret.Text = dataGridView2.Rows[secilen].Cells[3].Value.ToString();
            txtDepozito.Text = dataGridView2.Rows[secilen].Cells[4].Value.ToString();
        }

        private void btnKirala_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Araclar set AracDurum=1,AracKiraBaslangic=@p2,AracKiraBitis=@p3,AracKiralayanTC=@p4 where Aracid=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtID.Text);
            komut.Parameters.AddWithValue("@p2", mskKiraBaslangic.Text);
            komut.Parameters.AddWithValue("@p3", mskKiraBitis.Text);
            komut.Parameters.AddWithValue("@p4", mskTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            SqlCommand komut2 = new SqlCommand("insert into Tbl_KiraGecmisi (AracMarka,AracModel,KiralamaBaslangic,KiralamaBitis,KiralayanTC,KiralamaUcret,KiralamaDepozito) values (@p1,@p2,@p3,@p4,@p5,@p7,@p8)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtMarka.Text);
            komut2.Parameters.AddWithValue("@p2", txtModel.Text);
            komut2.Parameters.AddWithValue("@p3", mskKiraBaslangic.Text);
            komut2.Parameters.AddWithValue("@p4", mskKiraBitis.Text);
            komut2.Parameters.AddWithValue("@p5", mskTC.Text);
            komut2.Parameters.AddWithValue("@p7", txtUcret.Text);
            komut2.Parameters.AddWithValue("@p8", txtDepozito.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Araç kiralandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Araclar set AracDurum=0,AracKiraBaslangic=NULL,AracKiraBitis=NULL,AracKiralayanTC=NULL where Aracid=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Araç tekrardan kiralanmaya hazır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmRandevuPaneli fr = new FrmRandevuPaneli();
            fr.Show();
        }


        private void btnAracPanel_Click(object sender, EventArgs e)
        {
            FrmAracPaneli fr = new FrmAracPaneli();
            fr.Show();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtMarka.Text = "";
            txtModel.Text = "";
            txtUcret.Text = "";
            txtDepozito.Text = "";
            mskKiraBaslangic.Text = "";
            mskKiraBitis.Text = "";
            mskTC.Text = "";
            txtAdSoyad.Text = "";
        }

        private void btnMusteriler_Click(object sender, EventArgs e)
        {
            FrmMusteriler fr = new FrmMusteriler();
            fr.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Kullanımda Olan Araçları Dgye aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Aracid as ID,AracMarka as Marka,AracModel as Model,AracGunlukUcret as GunlukUcret,AracDepozito as Depozito,AracKiraBaslangic as BaslangıcTarihi,AracKiraBitis as BitisTarihi,AracKiralayanTC as MusteriTC from Tbl_Araclar where AracDurum=1", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Kullanımda Olmayan Araçları Dgye aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select Aracid as ID,AracMarka as Marka,AracModel as Model,AracGunlukUcret as GunlukUcret,AracDepozito as Depozito from Tbl_Araclar where AracDurum=0", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmYetkiliKiraGecmisi fr = new FrmYetkiliKiraGecmisi();
            fr.Show();
        }
    }
}
