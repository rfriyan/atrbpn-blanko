using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankoATRBPN.ViewModel
{
    public class ViewModel
    {
    }

    //public class PengembalianBlankoViewModel
    //{
    //    public decimal PENGELOLAAN_BLANKO_ID { get; set; }
    //    public Nullable<System.DateTime> tanggal { get; set; }
    //    public string blanko { get; set; }
    //    public string tipe_blanko { get; set; }
    //    public string beritaAcaraFile { get; set; }
    //}
    public class PengembalianBlankoViewModel
    {
        public decimal PENGELOLAAN_BLANKO_ID { get; set; }
        public Nullable<System.DateTime> TANGGAL { get; set; }
        public string BLANKO { get; set; }
        public string TIPE_BLANKO { get; set; }
        public string BERITAACARAFILE { get; set; }
    }



}