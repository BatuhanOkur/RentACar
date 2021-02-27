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
    public partial class FrmRandevuPaneli : Form
    {
        public FrmRandevuPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmRandevuPaneli_Load(object sender, EventArgs e)
        {
            //müsait zamanları dgye aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select RandevuTarih as Tarih,RandevuSaat as Saat from Tbl_Randevular where RandevuDurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            //Aktif randevuları dgye aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select Randevuid as ID,RandevuTarih as Tarih,RandevuSaat as Saat,RandevuArac as Arac,MusteriTC as TC,RandevuDetay as Detay from Tbl_Randevular where RandevuDurum=1", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtArac.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            lblTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            SqlCommand komut = new SqlCommand("select MusteriAd,MusteriSoyad from Tbl_Musteriler where MusteriTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();
            rchDetay.Text= dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat) values (@p1,@p2)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", mskSaat.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müsait zaman dilimi eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //müsait zamanları dgye aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select RandevuTarih as Tarih,RandevuSaat as Saat from Tbl_Randevular where RandevuDurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            //Aktif randevuları dgye aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select Randevuid as ID,RandevuTarih as Tarih,RandevuSaat as Saat,RandevuArac as Arac,MusteriTC as TC,RandevuDetay as Detay from Tbl_Randevular where RandevuDurum=1", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Randevular where RandevuTarih=@p1 and RandevuSaat=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", mskSaat.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Zaman dilimi silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            mskTarih.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            mskSaat.Text = dataGridView2.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            mskTarih.Text = "";
            mskSaat.Text = "";
            mskTarih.Focus();
        }
    }
}
