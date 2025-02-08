using IlkDers.DataAccessLayer;
using IlkDers.Models;
using System.Data;
using System.Data.SqlClient;

namespace IlkDers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // her yerdeb eriþilebilir crud iþlemelrini yaptýgým sýnýf
        PersonelDal personelDal = new PersonelDal();
        private void btnAra_Click(object sender, EventArgs e)
        {
            // BAÐLANTI Stringi verdik.
            // adonette baðlantýlar SqlConnection sýnfýýndan nesne örenði üretilir
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-L6NJT48\\SQLEXPRESS;Initial Catalog=IlkDers;Integrated Security=True");
            // baðlantý kontrol yaapcaz baðlantý kapalýysa baðlantýyý açacaz
            if (connection.State == ConnectionState.Closed) // baðlantý durmu kapalýysa
            {
                connection.Open(); // baðlantýyý açtým aslýnda mssqle baðlandým baðlantýyý açtýn
            }
            // Adonette sql sorgularýnda ise SqlCommand Sýnfýýndan nesne örneði üretilir
            SqlCommand ara = new SqlCommand("select * from Personel where ad like'%" + txtAra.Text + "%'", connection);
            // sql command 2 parametre alýr 1.sorgu 2.sql connection nesnesi
            // Dýsarýdan bir parametre yollayacaksak sqlDataAdapter sýnýfýný kullanacaz
            SqlDataAdapter da = new SqlDataAdapter(ara);
            DataSet ds = new DataSet(); //
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];// sýrasyýla verileri arama yapýp gösterecek
            connection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // BURADA VERÝLERÝ ALMAK ÝÇÝN GETaLL FONSÝYONUNDAN ÇEKECEZ
        private void btnSil_Click(object sender, EventArgs e)
        {
            // burdan datagriddeki id yi yaklayýp id deðiþkene atadým
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            personelDal.Delete(id);// silme iþlemi yapýldý
            dataGridView1.DataSource=personelDal.GetAll(); // sildim verileri listeledim
            MessageBox.Show("kayýt silindi");

        }
        // form1 load form yüklendiði zaman bizi karýþlayan ekran
        private void Form1_Load(object sender, EventArgs e)
        {
            // form yülendiði zaman verileri datagirdviewde göster
            dataGridView1.DataSource = personelDal.GetAll();
        }
    }
}
