

using System.Data;
using System.Data.SqlClient;

namespace IlkDers.DataAccessLayer
{
    // burada crud operasyonlarını yapacağımız yer
    public class PersonelDal
    {
        //serverin localdeki adresi                       //sunucu adresi.................... //database............. //wimdoew authanticationn 
                                                                                                                     //şifre ve paralo girmeden
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-L6NJT48\\SQLEXPRESS;Initial Catalog=IlkDers;ıntegrated Security=True");
               //geriye değer dondurmezse void olur
        public   void ConnectionKontrol()
        {
                      //bağlantı kapalıysa
            if(connection.State==ConnectionState.Closed)
            {
                connection.Open(); // bağlantıyı aç
            }
        }
        // datatable aslında server side calısıyor en cok kullanılan yapı verileri dolduracak listeleyecek
        public DataTable GetAll()
        {
            // BEST PARACTİTSE OLARAK
            // ConnectionKontrol()
            // VEYA
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            // SqlCommand tipinden command adı altında bir nesne oluşturdum ve ornekledim
            // SqlCommand 2 tane parametre alır 1.sorgu 2. si bağlantı
            SqlCommand command = new SqlCommand("Select * from Personel",connection);
            // verileri okumak için SqlDataReadr var 
            SqlDataReader reader = command.ExecuteReader(); // komutu çalıştırıyor okuma işlemi yapıldı
            // Datatable dan dt adında nesne ürettik okduugmuz veriler dtnin içine atılacak
            DataTable dt = new DataTable();
            dt.Load(reader); // budada reader SqlDaraReaderin readaer nesnesinden gelen verileri dt dolduruyor
            reader.Close(); // okuma işlemini sonlandırdık.
            connection.Close(); // bağlantıyı kapattık
            return dt; // datatable i donmemiz gerekiyor
        }
    }
}
