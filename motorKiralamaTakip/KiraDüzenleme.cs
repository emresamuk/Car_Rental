using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace motorKiralamaTakip
{
    public partial class KiraDüzenleme : Form
    {
        private MainForm mainForm; 
        private readonly FirestoreDb firestoreDb;

        public KiraDüzenleme(MainForm form)
        {
            InitializeComponent();
            mainForm = form;

            string jsonPath = @"your firebase json file path";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);

            firestoreDb = FirestoreDb.Create("your firebase project name");
        }
        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            string aracPlaka = txtAracPlaka.Text.Trim();
            string musteriTC = txtMusteriTC.Text.Trim();
            DateTime kiralamaBaslangici = dtpAlmaTarihi.Value;
            DateTime kiralamaBitisi = dtpTeslimTarihi.Value;
            string kiraBedeliText = txtKiraBedeli.Text.Trim();

            if (string.IsNullOrEmpty(aracPlaka))
            {
                MessageBox.Show("Lütfen araç plakasını girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(musteriTC))
            {
                MessageBox.Show("Lütfen müşteri TC numarasını girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(kiraBedeliText) || !double.TryParse(kiraBedeliText, out double kiraBedeli) || kiraBedeli <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir kira bedeli girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Check if the license plate exists in the "Cars" collection
                QuerySnapshot aracQuerySnapshot = await firestoreDb.Collection("Araclar")
                    .WhereEqualTo("Plaka", aracPlaka)
                    .GetSnapshotAsync();

                if (aracQuerySnapshot.Count == 0)
                {
                    MessageBox.Show("Böyle bir araç bulunmamaktadır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                QuerySnapshot kiralamaQuerySnapshot = await firestoreDb.Collection("Kiralama")
                    .WhereEqualTo("AracPlaka", aracPlaka)
                    .GetSnapshotAsync();

                if (kiralamaQuerySnapshot.Count > 0)
                {
                    MessageBox.Show("Bu plakaya sahip bir araç zaten kirada!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if customer ID exists in "Customers" collection
                QuerySnapshot musteriQuerySnapshot = await firestoreDb.Collection("Musteriler")
                    .WhereEqualTo("TC", musteriTC)
                    .GetSnapshotAsync();

                if (musteriQuerySnapshot.Count == 0)
                {
                    MessageBox.Show("Bu müşteri sistemde kayıtlı değil!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update the vehicle's document in the "Vehicle" collection
                DocumentSnapshot aracDoc = aracQuerySnapshot.Documents[0];

                // Update the "Details" field to "Rented"
                var updateData = new Dictionary<string, object>
        {
            { "Detay", "Kirada" }
        };

                await aracDoc.Reference.UpdateAsync(updateData);

                // New Rental Registration
                var yeniKira = new
                {
                    AracPlaka = aracPlaka,
                    MusteriTC = musteriTC,
                    AlmaTarihi = kiralamaBaslangici.ToString("yyyy-MM-dd"),
                    TeslimTarihi = kiralamaBitisi.ToString("yyyy-MM-dd"),
                    Durum = "Kirada",
                    KiraBedeli = kiraBedeli 
                };

                await firestoreDb.Collection("Kiralama").AddAsync(yeniKira);

                MessageBox.Show("Veriler başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Add an invoice
                Faturalar faturalar = new Faturalar(mainForm);
                faturalar.AddFatura(musteriTC, kiralamaBaslangici, kiralamaBitisi, kiraBedeli);

                mainForm.LoadKiralikAracData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnKiraSil_Click(object sender, EventArgs e)
        {
            string aracPlaka = txtAracPlaka.Text.Trim();

            if (string.IsNullOrEmpty(aracPlaka))
            {
                MessageBox.Show("Lütfen silmek istediğiniz aracın plakasını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Find the rental record in the "Rental" collection
                QuerySnapshot querySnapshot = await firestoreDb.Collection("Kiralama")
                    .WhereEqualTo("AracPlaka", aracPlaka)
                    .GetSnapshotAsync();

                if (querySnapshot.Count > 0)
                {
                    // Delete rental record
                    foreach (DocumentSnapshot doc in querySnapshot.Documents)
                    {
                        await doc.Reference.DeleteAsync();
                    }

                    // Update the relevant vehicle in the "Vehicles" collection
                    QuerySnapshot aracQuerySnapshot = await firestoreDb.Collection("Araclar")
                        .WhereEqualTo("Plaka", aracPlaka)
                        .GetSnapshotAsync();

                    if (aracQuerySnapshot.Count > 0)
                    {
                        DocumentSnapshot aracDoc = aracQuerySnapshot.Documents[0];

                        // Update the vehicle's "Detail" field to "Idle"
                        var updateData = new Dictionary<string, object>
                {
                    { "Detay", "Boşta" }
                };

                        await aracDoc.Reference.UpdateAsync(updateData);
                    }

                    MessageBox.Show("Kayıt başarıyla silindi ve araç durumu güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    mainForm.LoadKiralikAracData();
                }
                else
                {
                    MessageBox.Show("Silinecek kayıt bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}