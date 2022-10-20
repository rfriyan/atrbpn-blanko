using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankoATRBPN.ViewModel
{
    public class ViewModel
    {
    }

    public class PengembalianBlankoViewModel
    {
        public decimal PENGELOLAAN_BLANKO_ID { get; set; }
        public decimal tanggal { get; set; }
        public string blanko { get; set; }
        public string tipe_blanko { get; set; }
        public string beritaAcaraFile { get; set; }
    }
}