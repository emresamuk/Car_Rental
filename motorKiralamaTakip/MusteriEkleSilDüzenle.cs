using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace motorKiralamaTakip
{
    public partial class MusteriEkleSilDüzenle : Form
    {
        private MainForm _mainForm; 
        private FirestoreDb firestoreDb; 

        public MusteriEkleSilDüzenle(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;

            // Connect to Firestore database
            string path = "your firebase json file path";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            firestoreDb = FirestoreDb.Create("your firebase project name");
        }

        // Adding new customers 
        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                CollectionReference musterilerCollection = firestoreDb.Collection("Musteriler");

                var musteriData = new Dictionary<string, object>
                {
                    { "Ad", txtAd.Text },
                    { "Soyad", txtSoyad.Text },
                    { "TC", txtTC.Text },
                    { "EhliyetTuru", txtEhliyetTuru.Text },
                    { "Detay", richtxtDetay.Text },
                    { "Telefon", txtTelefon.Text }
                };

                await musterilerCollection.AddAsync(musteriData);
                MessageBox.Show("Müşteri başarıyla kaydedildi!");


                _mainForm.LoadMusteriData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // Delete customer 
        private async void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                CollectionReference musterilerCollection = firestoreDb.Collection("Musteriler");

                QuerySnapshot querySnapshot = await musterilerCollection.WhereEqualTo("TC", txtTC.Text).GetSnapshotAsync();

                if (querySnapshot.Documents.Count > 0)
                {
                    foreach (DocumentSnapshot document in querySnapshot.Documents)
                    {
                        await document.Reference.DeleteAsync();
                    }

                    MessageBox.Show("Müşteri başarıyla silindi!");

                    _mainForm.LoadMusteriData();
                }
                else
                {
                    MessageBox.Show("Silinecek müşteri bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // Customer record update
        private async void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                CollectionReference musterilerCollection = firestoreDb.Collection("Musteriler");

                QuerySnapshot querySnapshot = await musterilerCollection.WhereEqualTo("TC", txtTC.Text).GetSnapshotAsync();

                if (querySnapshot.Documents.Count > 0)
                {
                    foreach (DocumentSnapshot document in querySnapshot.Documents)
                    {
                        var updatedData = new Dictionary<string, object>
                        {
                            { "Ad", txtAd.Text },
                            { "Soyad", txtSoyad.Text },
                            { "EhliyetTuru", txtEhliyetTuru.Text },
                            { "Detay", richtxtDetay.Text },
                            { "Telefon", txtTelefon.Text }
                        };

                        await document.Reference.UpdateAsync(updatedData);
                    }

                    MessageBox.Show("Müşteri başarıyla güncellendi!");

                    _mainForm.LoadMusteriData();
                }
                else
                {
                    MessageBox.Show("Güncellenecek müşteri bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
