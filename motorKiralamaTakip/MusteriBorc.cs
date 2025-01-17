using Google.Cloud.Firestore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace motorKiralamaTakip
{
    public partial class MusteriBorc : Form
    {
        private readonly FirestoreDb firestoreDb;

        public MusteriBorc(MainForm mainForm)
        {
            InitializeComponent();

            string jsonPath = @"your firebase json file path";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);
            firestoreDb = FirestoreDb.Create("your firebase project name");

            LoadMusteriListesi(); 
        }

        private async void LoadMusteriListesi()
        {
            try
            {
                QuerySnapshot musterilerSnapshot = await firestoreDb.Collection("Musteriler").GetSnapshotAsync();

                if (musterilerSnapshot.Count == 0)
                {
                    MessageBox.Show("Hiçbir müşteri bulunamadı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (DocumentSnapshot musteri in musterilerSnapshot.Documents)
                {
                    if (musteri.TryGetValue("Ad", out string musteriAd) &&
                        musteri.TryGetValue("Soyad", out string musteriSoyad))
                    {
                        // We add it by combining the customer name and surname
                        string musteriTamAdi = $"{musteriAd} {musteriSoyad}";
                        checkedBorcluMusteri.Properties.Items.Add(musteriTamAdi);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müşteri listesi yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnListele_Click(object sender, EventArgs e)
        {
            await LoadFaturalar();
        }

        private async Task LoadFaturalar()
        {
            try
            {
                string selectedTamAd = checkedBorcluMusteri.EditValue?.ToString();

                if (string.IsNullOrEmpty(selectedTamAd))
                {
                    MessageBox.Show("Lütfen bir müşteri seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvBorclar.DataSource = null;
                    label4.Text = "0.00 TL";
                    return;
                }

                // Separate the selected full name (Name Surname)
                string[] adSoyad = selectedTamAd.Split(' ');
                if (adSoyad.Length < 2)
                {
                    MessageBox.Show("Müşteri adı geçersiz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string musteriAd = adSoyad[0];
                string musteriSoyad = adSoyad[1];

                // Find TR ID by Name and Surname
                QuerySnapshot musteriSnapshot = await firestoreDb.Collection("Musteriler")
                    .WhereEqualTo("Ad", musteriAd)
                    .WhereEqualTo("Soyad", musteriSoyad)
                    .GetSnapshotAsync();

                if (musteriSnapshot.Count == 0)
                {
                    MessageBox.Show("Seçilen müşteri sistemde bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string musteriTC = musteriSnapshot.Documents[0].GetValue<string>("TC");

                // Pull invoices with "unpaid" status for selected customer
                QuerySnapshot faturalarSnapshot = await firestoreDb.Collection("Faturalar")
                    .WhereEqualTo("Musteri", musteriTC)
                    .WhereEqualTo("Durum", "Ödenmedi")
                    .GetSnapshotAsync();

                if (faturalarSnapshot.Count == 0)
                {
                    MessageBox.Show("Seçilen müşteri için ödenmemiş borç bulunamadı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvBorclar.DataSource = null;
                    label4.Text = "0.00 TL";
                    return;
                }


                DataTable table = new DataTable();
                table.Columns.Add("Fatura ID");
                table.Columns.Add("Durum");
                table.Columns.Add("Toplam Ücret");

                double toplamBorc = 0;

                foreach (DocumentSnapshot fatura in faturalarSnapshot.Documents)
                {
                    string durum = fatura.TryGetValue("Durum", out string faturaDurum) ? faturaDurum : "Bilinmiyor";
                    double toplamUcret = fatura.TryGetValue("ToplamUcret", out double ucret) ? ucret : 0;

                    DataRow row = table.NewRow();
                    row["Fatura ID"] = fatura.Id;
                    row["Durum"] = durum;
                    row["Toplam Ücret"] = toplamUcret.ToString("F2");
                    table.Rows.Add(row);

                    toplamBorc += toplamUcret;
                }


                dgvBorclar.DataSource = table;
                label4.Text = $"{toplamBorc:F2} TL";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Borçlar yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
