using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OleDb_SqlServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string kaynak = "Provider=sqloledb;Data Source = (local); Initial Catalog = Öğrenci2; User Id = sa; Password=123";

        OleDbConnection baglanti = new OleDbConnection(kaynak);

        //---------------KAYDET------------------------ 
        private void button1_Click(object sender, EventArgs e)
        {

            string sorguKayıt = string.Format("insert into Öğrenciler (Numara, Adsoyad, Bölüm) values ('{0}','{1}','{2}')", 
                                               textBox1.Text, textBox2.Text, textBox3.Text);

            OleDbCommand komut2 = new OleDbCommand(sorguKayıt, baglanti);

            try
            {
                baglanti.Open();
                komut2.ExecuteNonQuery();
                baglanti.Close();

                label4.Text = sorguKayıt;
                label5.Text = "Kayıt yapıldı";
            }
            catch (Exception hata) { label5.Text = hata.Message; baglanti.Close(); }

        }

        //---------------GÜNCELLE------------------------ 
        private void button2_Click(object sender, EventArgs e)
        {
            string sorguGüncelle = string.Format("update Öğrenciler set Numara='{0}', Adsoyad='{1}', Bölüm='{2}' where Numara='{3}'",
                                                  textBox1.Text, textBox2.Text, textBox3.Text, textBox1.Text);

            OleDbCommand komut = new OleDbCommand(sorguGüncelle, baglanti);

            try
            {
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                label4.Text = sorguGüncelle;
                label5.Text = "Kayıt güncellendi";
            }
            catch (Exception hata) { label5.Text = hata.ToString(); baglanti.Close(); }
        }

        //---------------SİL------------------------ 
        private void button3_Click(object sender, EventArgs e)
        {
            string sorguSil = string.Format("delete from Öğrenciler where Numara='{0}'", textBox1.Text);

            OleDbCommand komut = new OleDbCommand(sorguSil, baglanti);

            try
            {
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                label4.Text = sorguSil;
                label5.Text = "Kayıt silindi";
            }
            catch (Exception hata) { label5.Text = hata.Message; baglanti.Close(); }
        }

        //---------------ARA------------------------ 
        private void button4_Click(object sender, EventArgs e)
        {
            string sorguArama = string.Format("select * from Öğrenciler where Numara='{0}'", textBox1.Text);

            OleDbCommand komut = new OleDbCommand(sorguArama, baglanti);
            OleDbDataReader veri;
            
            try
            {
                baglanti.Open();
                veri = komut.ExecuteReader();
                veri.Read();
                textBox1.Text = veri["Numara"].ToString();
                textBox2.Text = veri["Adsoyad"].ToString();
                textBox3.Text = veri["Bölüm"].ToString();
                veri.Close();
                baglanti.Close();

                label4.Text = sorguArama;
                label5.Text = "Arama tamamlandı";
            }
            catch (Exception hata) { label5.Text = hata.Message; baglanti.Close(); }

        }

        //---------------LİSTELE------------------------ 
        private void button5_Click(object sender, EventArgs e)
        {
            string sorguListe = "select * from Öğrenciler";

            OleDbCommand komut = new OleDbCommand(sorguListe, baglanti);
            OleDbDataReader veri;

            try
            {
                listView1.Items.Clear();
                baglanti.Open();
                veri = komut.ExecuteReader();
                while (veri.Read())
                {
                    ListViewItem kayit = new ListViewItem(veri["Numara"].ToString());
                    kayit.SubItems.Add(veri["Adsoyad"].ToString());
                    kayit.SubItems.Add(veri["Bölüm"].ToString());
                    listView1.Items.Add(kayit);
                }
                veri.Close();
                baglanti.Close();

                label4.Text = sorguListe;
                label5.Text = "Tüm kayıtlar listelendi";
            }
            catch (Exception hata) { label5.Text = hata.Message; baglanti.Close(); }
        }

        //---------------VERİTABANI OLUŞTUR------------------------ 
        private void button6_Click(object sender, EventArgs e)
        {
            string sorguVeriTabanıOluştur = "create database öğrenci1";

            OleDbCommand komut = new OleDbCommand(sorguVeriTabanıOluştur, baglanti);

            try
            {
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                label4.Text = sorguVeriTabanıOluştur;
                label5.Text = "Veri Tabanı oluşturuldu";
            }
            catch (Exception hata) { label5.Text = hata.Message; baglanti.Close(); }
        }

        //---------------TABLO OLUŞTUR------------------------ 
        private void button7_Click(object sender, EventArgs e)
        {
            string sorguTabloOluştur = "create table öğrenciler (Numara varchar(10), Adsoyad varchar(20), Bölüm varchar(20))";

            OleDbCommand komut = new OleDbCommand(sorguTabloOluştur, baglanti);

            try
            {
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                label4.Text = sorguTabloOluştur;
                label5.Text = "Tablo oluşturuldu";
            }
            catch (Exception hata) { label5.Text = hata.Message; baglanti.Close(); }
        }
    }
}
