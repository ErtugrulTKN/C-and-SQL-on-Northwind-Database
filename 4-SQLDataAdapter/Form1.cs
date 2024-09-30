using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLDataAdapter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string kaynak = "Server=(local); Database = öğrenci; uid=sa; pwd =123";
        string sorgu;

        //-----------------KAYDET-----------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            sorgu = string.Format("insert into Öğrenciler (Numara, Adsoyad, Bölüm) values ('{0}', '{1}', '{2}')",
                 textBox1.Text, textBox2.Text, textBox3.Text);

            SqlDataAdapter adaptör  = new SqlDataAdapter(sorgu, kaynak);
            DataSet veriseti = new DataSet();

            adaptör.Fill(veriseti);
        }

        //-----------------GÜNCELLE-----------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = string.Format("update Öğrenciler set Numara='{0}',  Adsoyad='{1}', Bölüm='{2}'  where Numara='{3}'",
                                          textBox1.Text, textBox2.Text, textBox3.Text, textBox1.Text);

            SqlDataAdapter adaptör = new SqlDataAdapter(sorgu, kaynak);
            DataSet veriseti = new DataSet();

            adaptör.Fill(veriseti);
        }

        //-----------------SİL----------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = string.Format("delete from Öğrenciler where Numara='{0}'", textBox1.Text);

            SqlDataAdapter adaptör = new SqlDataAdapter(sorgu, kaynak);
            DataSet veriseti = new DataSet();

            adaptör.Fill(veriseti);
        }

        //-----------------ARAMA (NUMARAYA GÖRE)---------------
        private void button4_Click(object sender, EventArgs e)
        {
            string sorgu = string.Format("select * from Öğrenciler where Numara='{0}'", textBox1.Text);
            SqlDataAdapter adaptör = new SqlDataAdapter(sorgu, kaynak);
            DataSet veriseti = new DataSet();

            adaptör.Fill(veriseti, "ds_tablo1");

            textBox1.Text = veriseti.Tables["ds_tablo1"].Rows[0].ItemArray[0].ToString();
            textBox2.Text = veriseti.Tables["ds_tablo1"].Rows[0].ItemArray[1].ToString();
            textBox3.Text = veriseti.Tables["ds_tablo1"].Rows[0].ItemArray[2].ToString();

            //textBox1.DataBindings.Add("Text", veriseti.Tables["ds_tablo1"], "Numara");
            //textBox2.DataBindings.Add("Text", veriseti.Tables["ds_tablo1"], "Adı Soyadı");
            //textBox3.DataBindings.Add("Text", veriseti.Tables["ds_tablo1"], "Bölümü");

            dataGridView1.DataSource = veriseti.Tables["ds_tablo1"];
        }

        //-----------------BÖLÜM LİSTE--------------------
        private void button5_Click(object sender, EventArgs e)
        {
            string sorgu = string.Format("select * from Öğrenciler where Bölüm='{0}'", textBox3.Text);
            SqlDataAdapter adaptör = new SqlDataAdapter(sorgu, kaynak);
            DataSet veriseti = new DataSet();

            adaptör.Fill(veriseti, "ds_tablo1");
            dataGridView1.DataSource = veriseti.Tables["ds_tablo1"];
        }

        //-----------------TÜM LİSTE-----------------------------
        private void button6_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from öğrenciler";
            SqlDataAdapter adaptör = new SqlDataAdapter(sorgu, kaynak);
            DataSet veriseti = new DataSet();

            adaptör.Fill(veriseti, "ds_tablo1");
            dataGridView1.DataSource = veriseti.Tables["ds_tablo1"];
        }
    }
}
