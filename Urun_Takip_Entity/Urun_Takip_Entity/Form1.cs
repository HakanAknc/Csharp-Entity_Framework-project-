using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Urun_Takip_Entity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbUrunEntities db = new DbUrunEntities();
        private void BtnListele_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.TBLMUSTERI.ToList();
            var degerler = from x in db.TBLMUSTERI
                           select new
                           {
                               x.MusteriID,
                               x.Ad,
                               x.Soyad,
                               x.Sehir,
                               x.Bakiye
                           };
            dataGridView1.DataSource = degerler.ToList();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TBLMUSTERI t = new TBLMUSTERI();
            t.Ad = TxtAd.Text;
            t.Soyad = TxtSoyad.Text;
            t.Sehir = TxtSehir.Text;
            t.Bakiye = decimal.Parse(TxtBakiye.Text);
            db.TBLMUSTERI.Add(t);
            db.SaveChanges();     //Değişiklikleri kaydet
            MessageBox.Show("Yeni Müşteri Kaydı Yapıldı!");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = int.Parse(TxtID.Text);
            var x = db.TBLMUSTERI.Find(id);
            db.TBLMUSTERI.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Müşteri sistemden silindi.");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = int.Parse(TxtID.Text);
            var x = db.TBLMUSTERI.Find(id);
            x.Ad = TxtAd.Text;
            x.Soyad = TxtSoyad.Text;
            x.Sehir = TxtSehir.Text;
            x.Bakiye = decimal.Parse(TxtBakiye.Text);
            db.SaveChanges();
            MessageBox.Show("Müşteri bilgisi güncellendi.");
        }
    }
}
