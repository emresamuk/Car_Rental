using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace motorKiralamaTakip
{
    public partial class Faturalar : Form
    {
        private readonly FirestoreDb firestoreDb; 

        public Faturalar(MainForm mainForm)
        {
            InitializeComponent();

            string jsonPath = @"your firebase json file path";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);
            firestoreDb = FirestoreDb.Create("your firebase project name");

            LoadFaturalar(); 
        }

        private async void LoadFaturalar()
        {
            DataTable table = new DataTable();
            table.Columns.Add("FaturaID");
            table.Columns.Add("ToplamUcret");
            table.Columns.Add("Musteri");
            table.Columns.Add("Durum");

            try
            {
                CollectionReference faturalarCollection = firestoreDb.Collection("Faturalar");
                QuerySnapshot snapshot = await faturalarCollection.GetSnapshotAsync();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        var data = document.ToDictionary();
                        DataRow row = table.NewRow();

                        row["FaturaID"] = document.Id;

                        if (data.ContainsKey("ToplamUcret") && data["ToplamUcret"] is double toplamUcretDouble)
                        {
                            row["ToplamUcret"] = toplamUcretDouble.ToString("F2");
                        }
                        else
                        {
                            row["ToplamUcret"] = "0.00";
                        }

                        row["Musteri"] = data.ContainsKey("Musteri") ? data["Musteri"].ToString() : "";
                        row["Durum"] = data.ContainsKey("Durum") ? data["Durum"].ToString() : "Ödenmedi";

                        table.Rows.Add(row);
                    }
                }

                dgvFaturalar.DataSource = table;
                dgvFaturalar.Columns["FaturaID"].Visible = false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Faturalar yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public async void AddFatura(string musteriTC, DateTime baslangic, DateTime bitis, double kiraBedeli)
        {
            try
            {
                int gunSayisi = (int)(bitis - baslangic).TotalDays; 
                double toplamUcret = gunSayisi * kiraBedeli; 

                var yeniFatura = new
                {
                    ToplamUcret = toplamUcret,
                    Musteri = musteriTC,
                    Durum = "Ödenmedi" 
                };

                await firestoreDb.Collection("Faturalar").AddAsync(yeniFatura);

                MessageBox.Show("Fatura başarıyla oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadFaturalar(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatura oluşturulurken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void dgvFaturalar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dgvFaturalar.Rows[e.RowIndex];
                    string faturaID = selectedRow.Cells["FaturaID"].Value.ToString();
                    string mevcutDurum = selectedRow.Cells["Durum"].Value.ToString();

                    if (mevcutDurum == "Ödenmedi")
                    {
                        DialogResult result = MessageBox.Show("Bu faturayı ödendi olarak işaretlemek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            DocumentReference faturaRef = firestoreDb.Collection("Faturalar").Document(faturaID);
                            await faturaRef.UpdateAsync("Durum", "Ödendi");

                            MessageBox.Show("Fatura ödendi olarak işaretlendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadFaturalar();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bu fatura zaten ödendi olarak işaretlenmiş.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatura durumu güncellenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}