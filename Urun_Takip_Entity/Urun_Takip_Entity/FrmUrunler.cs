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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        DbUrunEntities db = new DbUrunEntities();

        void urunlistesi()
        {
            var urunler = from x in db.TBLURUNLER
                          select new
                          {
                              x.UrunId,
                              x.UrunAd,
                              x.Stok,
                              x.AlisFiyat,
                              x.SatisFiyat,
                              x.TBLKATEGORI.Ad
                          };
            dataGridView1.DataSource = urunler.ToList();
        }

        void temizle()
        {
            TxtID.Text = "";
            TxtUrunAd.Text = "";
            TxtStok.Text = "";
            TxtAlisFiyat.Text = "";
            TxtSatisFiyat.Text = "";
        }
        private void BtnListele_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.TBLURUNLER.ToList();
            urunlistesi();
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            urunlistesi();
            comboBox1.DataSource = db.TBLKATEGORI.ToList();
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Ad";
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TBLURUNLER t = new TBLURUNLER();
            t.UrunAd = TxtUrunAd.Text;
            t.Stok = short.Parse(TxtStok.Text);
            t.AlisFiyat = decimal.Parse(TxtAlisFiyat.Text);
            t.SatisFiyat = decimal.Parse(TxtSatisFiyat.Text);
            t.Kategori = int.Parse(comboBox1.SelectedValue.ToString());
            db.TBLURUNLER.Add(t);
            db.SaveChanges();
            MessageBox.Show("Ürün başarılı bir şekilde sisteme kaydedildi.");
            urunlistesi();
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtUrunAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtStok.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtAlisFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtSatisFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            //comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (TxtID.Text != "")
            {
                int id = int.Parse(TxtID.Text);
                var x = db.TBLURUNLER.Find(id);
                db.TBLURUNLER.Remove(x);
                db.SaveChanges();
                MessageBox.Show("Ürün başarılı bir şekilde silindi.", "Silme işlemi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                MessageBox.Show("Lütfen verileri listeledikten sonra bir satıra tıklayıp silmek istediğiniz satırı seçiniz", 
                    "Silme işlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            urunlistesi();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = int.Parse(TxtID.Text);
            var x = db.TBLURUNLER.Find(id);
            x.UrunAd = TxtUrunAd.Text;
            x.Stok =short.Parse(TxtStok.Text);
            x.AlisFiyat = decimal.Parse(TxtAlisFiyat.Text);
            x.SatisFiyat = decimal.Parse(TxtSatisFiyat.Text);
            x.Kategori = int.Parse(comboBox1.SelectedValue.ToString());
            db.SaveChanges();
            MessageBox.Show("Verileriniz başarılı bir şekilde güncellendi", 
                "Güncelleme Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            urunlistesi();
        }
    }
}
