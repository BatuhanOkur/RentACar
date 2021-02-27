using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AracKiralama
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-35OUD05\\SQLEXPRESS;Initial Catalog=AracKiralama;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
