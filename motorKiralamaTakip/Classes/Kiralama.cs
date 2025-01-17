using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motorKiralamaTakip.Classes
{
    public class Kiralama
    {
        public string Id { get; set; }
        public string AracPlaka { get; set; }
        public string MusteriTC { get; set; }
        public DateTime AlmaTarihi { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public string Durum { get; set; }
    }
}