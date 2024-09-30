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

namespace SQLClient_SqlServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //static string kaynak = "server=(local); database=öğrenci; integrated security=true";
        static string kaynak = "server=(local); database=öğrenci; uid=sa; pwd=123";


        string sorguArama = "select * from Öğrenciler where Numara=@no";
        
        //string sorguListe = "select * from Öğrenciler where Bölüm=@blm";
        string sorguListe = "select * from Öğrenciler";

        string sorguKayıt = "insert into Öğrenciler (Numara, Adsoyad, Bölüm) values (@no, @ad, @blm)";
        string sorguGüncelle = "update Öğrenciler set Numara=@no, Adsoyad=@ad, Bölüm=@blm where Numara=@no";
        string sorguSil = "delete from Öğrenciler where Numara=@no";

        string sorguVeriTabanıOluştur = "create database öğrenci1";
        string sorguTabloOluştur = "create table öğrenciler1 (numara varchar(10), adsoyad varchar(20), bölüm varchar(20))";

        SqlConnection baglanti = new SqlConnection(kaynak);

        

        //---------------KAYDET------------------------ 
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(sorguKayıt, baglanti);

            komut.Parameters.AddWithValue("@no", textBox1.Text);
            komut.Parameters.AddWithValue("@ad", textBox2.Text);
            komut.Parameters.AddWithValue("@blm", textBox3.Text);

            try
            {
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                label4.Text = sorguKayıt;
                label5.Text = "Kayıt yapıldı";
            }
            catch (Exception hata) { label5.Text = hata.Message; baglanti.Close(); }
        }
        //---------------GÜNCELLE------------------------ 
        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(sorguGüncelle, baglanti);

            komut.Parameters.AddWithValue("@no", textBox1.Text);
            komut.Parameters.AddWithValue("@ad", textBox2.Text);
            komut.Parameters.AddWithValue("@blm", textBox3.Text);

            try
            {
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                label4.Text = sorguGüncelle;
                label5.Text = "Kayıt güncellendi";
            }
            catch (Exception hata) { label5.Text = hata.Message; baglanti.Close(); }
        }

        //---------------SİL------------------------ 
        private void button3_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand(sorguSil, baglanti);

            komut.Parameters.AddWithValue("@no", textBox1.Text);

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
            SqlCommand komut = new SqlCommand(sorguArama, baglanti);

            SqlDataReader veri;
            
            komut.Parameters.AddWithValue("@no", textBox1.Text);

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
            SqlCommand komut = new SqlCommand(sorguListe, baglanti);
            SqlDataReader veri;

            komut.Parameters.AddWithValue("@blm", textBox3.Text);
           
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
            SqlCommand komut = new SqlCommand(sorguVeriTabanıOluştur, baglanti);

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
            SqlCommand komut = new SqlCommand(sorguTabloOluştur, baglanti);

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
