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
    public partial class FrmAracPaneli : Form
    {
        public FrmAracPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Araclar (AracMarka,AracModel,AracVites,AracYakit,AracKmSinir,AracGunlukUcret,AracDepozito) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtMarka.Text);
            komut.Parameters.AddWithValue("@p2", txtModel.Text);
            komut.Parameters.AddWithValue("@p3", cmbVites.Text);
            komut.Parameters.AddWithValue("@p4", cmbYakit.Text);
            komut.Parameters.AddWithValue("@p5", mskKmSinir.Text);
            komut.Parameters.AddWithValue("@p6", txtUcret.Text);
            komut.Parameters.AddWithValue("@p7", txtDepozito.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Araç eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Araclar where Aracid=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Seçilen araç silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Araclar set AracMarka=@p1,AracModel=@p2,AracVites=@p3,AracYakit=@p4,AracKmSinir=@p5,AracGunlukUcret=@p6,AracDepozito=@p7 where Aracid=@p8", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtMarka.Text);
            komut.Parameters.AddWithValue("@p2", txtModel.Text);
            komut.Parameters.AddWithValue("@p3", cmbVites.Text);
            komut.Parameters.AddWithValue("@p4", cmbYakit.Text);
            komut.Parameters.AddWithValue("@p5", mskKmSinir.Text);
            komut.Parameters.AddWithValue("@p6", txtUcret.Text);
            komut.Parameters.AddWithValue("@p7", txtDepozito.Text);
            komut.Parameters.AddWithValue("@p8", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Seçilen araç güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmAracPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Aracid as ID,AracMarka as Marka,AracModel as Model,AracVites as Vites,AracYakit as Yakit,AracKmSinir as KmSinir, AracGunlukUcret as GunlukUcret,AracDepozito as Depozito from Tbl_Araclar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Aracid as ID,AracMarka as Marka,AracModel as Model,AracVites as Vites,AracYakit as Yakit,AracKmSinir as KmSinir, AracGunlukUcret as GunlukUcret,AracDepozito as Depozito from Tbl_Araclar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtMarka.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtModel.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbVites.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            cmbYakit.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            mskKmSinir.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtUcret.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            txtDepozito.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtMarka.Text = "";
            txtModel.Text = "";
            cmbVites.Text = "";
            cmbYakit.Text = "";
            mskKmSinir.Text = "";
            txtUcret.Text = "";
            txtDepozito.Text = "";
            txtMarka.Focus();
        }
    }
}
