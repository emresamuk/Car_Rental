using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace motorKiralamaTakip
{
    public partial class AracEkleSilDüzenle : Form
    {
        private MainForm _mainForm; 
        private FirestoreDb _firestoreDb; 

        public AracEkleSilDüzenle(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm; 

            try
            {
                _firestoreDb = FirestoreDb.Create("your firebase project name");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Firestore bağlantısı kurulamadı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Vehicle add
        private async void btnAracEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string sigortaTarihi = dtpSigortaTarihi.Value.ToString("yyyy-MM-dd");
                string bakimTarihi = dtpBakimTarihi.Value.ToString("yyyy-MM-dd");


                string detay = chckdDetay.Text ?? "Boşta";

                // Generate vehicle data
                var aracData = new Dictionary<string, object>
                {
                    { "Plaka", txtPlaka.Text },
                    { "Marka", txtMarka.Text },
                    { "Model", txtModel.Text },
                    { "SigortaTarihi", sigortaTarihi },
                    { "BakimTarihi", bakimTarihi },
                    { "Detay", detay },
                    { "Renk", txtRenk.Text } 
                };

                CollectionReference araclarCollection = _firestoreDb.Collection("Araclar");
                await araclarCollection.AddAsync(aracData);

                MessageBox.Show("Araç başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _mainForm.LoadAracData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Araç eklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Vehicle Delete 
        private async void btnAracSil_Click(object sender, EventArgs e)
        {
            try
            {
                string plaka = txtPlaka.Text.Trim();

                if (string.IsNullOrEmpty(plaka))
                {
                    MessageBox.Show("Lütfen silmek istediğiniz aracın plakasını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var aracCollection = _firestoreDb.Collection("Araclar");
                QuerySnapshot snapshot = await aracCollection.WhereEqualTo("Plaka", plaka).GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    foreach (var document in snapshot.Documents)
                    {
                        await document.Reference.DeleteAsync();
                    }

                    MessageBox.Show("Araç başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _mainForm.LoadAracData();
                }
                else
                {
                    MessageBox.Show("Bu plakaya sahip bir araç bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Araç silinirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Vehicle update
        private async void btnAracGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                string sigortaTarihi = dtpSigortaTarihi.Value.ToString("yyyy-MM-dd");
                string bakimTarihi = dtpBakimTarihi.Value.ToString("yyyy-MM-dd");
                string detay = chckdDetay.Text ?? "Boşta";

                var aracCollection = _firestoreDb.Collection("Araclar");
                QuerySnapshot snapshot = await aracCollection.WhereEqualTo("Plaka", txtPlaka.Text).GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    foreach (var document in snapshot.Documents)
                    {
                        var aracData = new Dictionary<string, object>
                        {
                            { "Marka", txtMarka.Text },
                            { "Model", txtModel.Text },
                            { "SigortaTarihi", sigortaTarihi },
                            { "BakimTarihi", bakimTarihi },
                            { "Detay", detay },
                            { "Renk", txtRenk.Text }
                        };

                        await document.Reference.UpdateAsync(aracData);
                    }

                    MessageBox.Show("Araç başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _mainForm.LoadAracData();
                }
                else
                {
                    MessageBox.Show("Bu plakaya sahip bir araç bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Araç güncellenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
