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
    public partial class Frmistatistik : Form
    {
        public Frmistatistik()
        {
            InitializeComponent();
        }
        DbUrunEntities db = new DbUrunEntities();
        private void Frmistatistik_Load(object sender, EventArgs e)
        {
            DateTime bugun = DateTime.Today;
            LblMusteriSayisi.Text = db.TBLMUSTERI.Count().ToString();
            LblKategoriSayisi.Text = db.TBLKATEGORI.Count().ToString();
            LblUrunSayisi.Text = db.TBLURUNLER.Count().ToString();
            LblBeyazEsya.Text = db.TBLURUNLER.Count(X => X.Kategori == 1).ToString();   // x öyleki anlamına geliyor.
            LblToplamStok.Text = db.TBLURUNLER.Sum(X => X.Stok).ToString();   // Entity Framework Linq == lambda olarak da geçer.
            LblBugunSatisAdedi.Text = db.TBLSATISLAR.Count(X => X.Tarih == bugun).ToString();
            LblToplamKasaTutari.Text = db.TBLSATISLAR.Sum(X => X.Toplam).ToString() + " ₺";
            LblBugunkuKarTutari.Text = db.TBLSATISLAR.Where(X => X.Tarih == bugun).Sum(Y => Y.Toplam).ToString() + " ₺";
            LblEnYuksekFiyatliUrun.Text = (from x in db.TBLURUNLER orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            LblEnDusukFiyatliUrun.Text = (from x in db.TBLURUNLER orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
            LblEnFazlaStokluUrun.Text = (from x in db.TBLURUNLER orderby x.Stok descending select x.UrunAd).FirstOrDefault();
            LblEnAzStokluUrun.Text = (from x in db.TBLURUNLER orderby x.Stok ascending select x.UrunAd).FirstOrDefault();
        }
    }
}
