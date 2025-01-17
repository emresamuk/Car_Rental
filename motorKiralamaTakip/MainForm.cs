using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace motorKiralamaTakip
{
    public partial class MainForm : Form
    {
        public DataGridView dgvAraclar;

        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadMusteriData();
            LoadAracData();
            CheckAracMaintenanceAndInsurance();
            LoadKiralikAracData();
        }

        // Function to retrieve customer data from database
        public async void LoadMusteriData()
        {
            //For json file u need to download it from firebase and add to Config file, then copy paste the file path 
            string jsonPath = @"your firebase json file path";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);

            //you can find it from json file(must be project_id or something like that
            FirestoreDb firestoreDb = FirestoreDb.Create("your firebase project name");

            DataTable dataTable = new DataTable();

            // Define the DataTable columns
            dataTable.Columns.Add("Ad");
            dataTable.Columns.Add("Soyad");
            dataTable.Columns.Add("TC");
            dataTable.Columns.Add("EhliyetTuru");
            dataTable.Columns.Add("Detay");
            dataTable.Columns.Add("Telefon");

            try
            {
                CollectionReference musteriCollection = firestoreDb.Collection("Musteriler");
                QuerySnapshot snapshot = await musteriCollection.GetSnapshotAsync();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        var data = document.ToDictionary();
                        DataRow row = dataTable.NewRow();

                        row["Ad"] = data.ContainsKey("Ad") ? data["Ad"].ToString() : "";
                        row["Soyad"] = data.ContainsKey("Soyad") ? data["Soyad"].ToString() : "";
                        row["TC"] = data.ContainsKey("TC") ? data["TC"].ToString() : "";
                        row["EhliyetTuru"] = data.ContainsKey("EhliyetTuru") ? data["EhliyetTuru"].ToString() : "";
                        row["Detay"] = data.ContainsKey("Detay") ? data["Detay"].ToString() : "";
                        row["Telefon"] = data.ContainsKey("Telefon") ? data["Telefon"].ToString() : "";

                        dataTable.Rows.Add(row);
                    }
                }

                if (dataTable.Rows.Count > 0)
                {
                    dgvMusteriler.DataSource = dataTable; 
                }
                else
                {
                    MessageBox.Show("Firestore'da müşteri bulunmamaktadır.");
                    dgvMusteriler.DataSource = null; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message); 
            }
        }

        // Function to retrieve vehicle data from database
        public async void LoadAracData()
        {
            string jsonPath = @"your firebase json file path";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);

            FirestoreDb firestoreDb = FirestoreDb.Create("your firebase project name");

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Plaka");
            dataTable.Columns.Add("Marka");
            dataTable.Columns.Add("Model");
            dataTable.Columns.Add("Renk");
            dataTable.Columns.Add("SigortaTarihi");
            dataTable.Columns.Add("BakimTarihi");
            dataTable.Columns.Add("Detay");
            

            try
            {
                CollectionReference araclarCollection = firestoreDb.Collection("Araclar");
                QuerySnapshot snapshot = await araclarCollection.GetSnapshotAsync();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        var data = document.ToDictionary();
                        DataRow row = dataTable.NewRow();

                        row["Plaka"] = data.ContainsKey("Plaka") ? data["Plaka"].ToString() : "";
                        row["Marka"] = data.ContainsKey("Marka") ? data["Marka"].ToString() : "";
                        row["Model"] = data.ContainsKey("Model") ? data["Model"].ToString() : "";
                        row["Renk"] = data.ContainsKey("Renk") ? data["Renk"].ToString() : "";
                        row["SigortaTarihi"] = data.ContainsKey("SigortaTarihi") ? data["SigortaTarihi"].ToString() : "";
                        row["BakimTarihi"] = data.ContainsKey("BakimTarihi") ? data["BakimTarihi"].ToString() : "";
                        row["Detay"] = data.ContainsKey("Detay") ? data["Detay"].ToString() : "";
                        

                        dataTable.Rows.Add(row);
                    }
                }

                if (dataTable.Rows.Count > 0)
                {
                    dgvAraclar.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("Veritabanında araç bulunmamaktadır.");
                    dgvAraclar.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // Function to check the remaining days until the maintenance and insurance dates of the vehicles
        private async void CheckAracMaintenanceAndInsurance()
        {
            string jsonPath = @"your firebase json file path";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);

            FirestoreDb firestoreDb = FirestoreDb.Create("your firebase project name");

            try
            {
                CollectionReference araclarCollection = firestoreDb.Collection("Araclar");
                QuerySnapshot snapshot = await araclarCollection.GetSnapshotAsync();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        var data = document.ToDictionary();
                        string plaka = data.ContainsKey("Plaka") ? data["Plaka"].ToString() : "";
                        string sigortaTarihiStr = data.ContainsKey("SigortaTarihi") ? data["SigortaTarihi"].ToString() : "";
                        string bakimTarihiStr = data.ContainsKey("BakimTarihi") ? data["BakimTarihi"].ToString() : "";

                        DateTime sigortaTarihi = DateTime.Parse(sigortaTarihiStr);
                        DateTime bakimTarihi = DateTime.Parse(bakimTarihiStr);

                        int daysUntilSigorta = (sigortaTarihi - DateTime.Now).Days;
                        int daysUntilBakim = (bakimTarihi - DateTime.Now).Days;

                       
                        if (daysUntilSigorta <= 7) 
                        {
                            ShowNotification($"{plaka} 'lı aracın sigorta tarihine {daysUntilSigorta} gün kaldı!", "Sigorta Hatırlatıcı", MessageBoxIcon.Warning);
                        }

                        if (daysUntilBakim <= 7) 
                        {
                            ShowNotification($"{plaka} 'lı aracın bakım tarihine {daysUntilBakim} gün kaldı!", "Bakım Hatırlatıcı", MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }


        public void ShowNotification(string message, string caption, MessageBoxIcon icon)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
        }


        public async void LoadKiralikAracData()
        {
            string jsonPath = @"your firebase json file path";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);

            FirestoreDb firestoreDb = FirestoreDb.Create("your firebase project name");

            DataTable dataTable = new DataTable();

            // Define the DataTable columns
            dataTable.Columns.Add("AracPlaka");
            dataTable.Columns.Add("MusteriTC");
            dataTable.Columns.Add("AlmaTarihi");
            dataTable.Columns.Add("TeslimTarihi");
            dataTable.Columns.Add("Durum");

            try
            {
                CollectionReference kiralamaCollection = firestoreDb.Collection("Kiralama");
                QuerySnapshot snapshot = await kiralamaCollection.GetSnapshotAsync();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        var data = document.ToDictionary();
                        DataRow row = dataTable.NewRow();

                        row["AracPlaka"] = data.ContainsKey("AracPlaka") ? data["AracPlaka"].ToString() : "";
                        row["MusteriTC"] = data.ContainsKey("MusteriTC") ? data["MusteriTC"].ToString() : "";
                        row["AlmaTarihi"] = data.ContainsKey("AlmaTarihi") ? data["AlmaTarihi"].ToString() : "";
                        row["TeslimTarihi"] = data.ContainsKey("TeslimTarihi") ? data["TeslimTarihi"].ToString() : "";
                        row["Durum"] = data.ContainsKey("Durum") ? data["Durum"].ToString() : "";

                        dataTable.Rows.Add(row);
                    }
                }

                if (dataTable.Rows.Count > 0)
                {
                    dgvKiralikAraclar.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("Kiralama tablosunda veri bulunmamaktadır.");
                    dgvKiralikAraclar.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // ToolStrip Menu 
        private void müşteriBilgisiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            MusteriEkleSilDüzenle musteriDuzenlemeFormu = new MusteriEkleSilDüzenle(this);
            musteriDuzenlemeFormu.Show();
        }

        private void araçBilgisiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AracEkleSilDüzenle aracDuzenlemeFormu = new AracEkleSilDüzenle(this);
            aracDuzenlemeFormu.Show();
        }


        private void kiralamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KiraDüzenleme kiraDuzenle = new KiraDüzenleme(this);
            kiraDuzenle.Show();

        }

        private void faturalarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Faturalar faturalar = new Faturalar(this);
            faturalar.Show();
        }

        private void müşteriBorçSayfasıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusteriBorc MusteriBorc = new MusteriBorc(this);
            MusteriBorc.Show();
        }
    }
}