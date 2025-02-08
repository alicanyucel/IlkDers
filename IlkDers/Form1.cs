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
        // her yerdeb eri�ilebilir crud i�lemelrini yapt�g�m s�n�f
        PersonelDal personelDal = new PersonelDal();
        private void btnAra_Click(object sender, EventArgs e)
        {
            // BA�LANTI Stringi verdik.
            // adonette ba�lant�lar SqlConnection s�nf��ndan nesne �ren�i �retilir
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-L6NJT48\\SQLEXPRESS;Initial Catalog=IlkDers;Integrated Security=True");
            // ba�lant� kontrol yaapcaz ba�lant� kapal�ysa ba�lant�y� a�acaz
            if (connection.State == ConnectionState.Closed) // ba�lant� durmu kapal�ysa
            {
                connection.Open(); // ba�lant�y� a�t�m asl�nda mssqle ba�land�m ba�lant�y� a�t�n
            }
            // Adonette sql sorgular�nda ise SqlCommand S�nf��ndan nesne �rne�i �retilir
            SqlCommand ara = new SqlCommand("select * from Personel where ad like'%" + txtAra.Text + "%'", connection);
            // sql command 2 parametre al�r 1.sorgu 2.sql connection nesnesi
            // D�sar�dan bir parametre yollayacaksak sqlDataAdapter s�n�f�n� kullanacaz
            SqlDataAdapter da = new SqlDataAdapter(ara);
            DataSet ds = new DataSet(); //
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];// s�rasy�la verileri arama yap�p g�sterecek
            connection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // BURADA VER�LER� ALMAK ���N GETaLL FONS�YONUNDAN �EKECEZ
        private void btnSil_Click(object sender, EventArgs e)
        {
            // burdan datagriddeki id yi yaklay�p id de�i�kene atad�m
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            personelDal.Delete(id);// silme i�lemi yap�ld�
            dataGridView1.DataSource = personelDal.GetAll(); // sildim verileri listeledim
            MessageBox.Show("kay�t silindi");

        }
        // form1 load form y�klendi�i zaman bizi kar��layan ekran
        private void Form1_Load(object sender, EventArgs e)
        {
            // form y�lendi�i zaman verileri datagirdviewde g�ster
            dataGridView1.DataSource = personelDal.GetAll();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtBrans.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            personelDal.Ekle(new Personel
            {
                Ad = txtAd.Text,
                Soyad = txtSoyad.Text,
                Brans = txtBrans.Text,
            });
            dataGridView1.DataSource = personelDal.GetAll();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
        
            Personel personel = new Personel
            {
                // id=0 indeks ad=1. indeks soyad 2. indeks brans da 3.indeks e denk geliyor
                Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()),
                Ad = txtAd.Text,
                Soyad = txtSoyad.Text,
                Brans = txtBrans.Text,
            }; // burada tetboxlardan gelen de�erlerle s�n�f� doldurdum
            personelDal.Update(personel);
            MessageBox.Show("G�ncellendi");
            dataGridView1.DataSource = personelDal.GetAll();
        }
    }
}
