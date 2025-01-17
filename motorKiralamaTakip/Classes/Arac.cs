using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motorKiralamaTakip.Classes
{
    public class Arac
    {
        public string Id { get; set; }
        public string Marka { get; set; }
        public string Plaka { get; set; }
        public decimal Ucret { get; set; }
        public string Durum { get; set; } // Kirada, Uygun, Bozuk
        public DateTime TrafikSigortasiTarihi { get; set; }
        public DateTime MuayeneTarihi { get; set; }
    }
}
