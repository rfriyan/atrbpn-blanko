//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlankoATRBPN.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PENGELOLAAN_BLANKO
    {
        public decimal PENGELOLAAN_BLANKO_ID { get; set; }
        public Nullable<decimal> BERITA_ACARA_ID { get; set; }
        public Nullable<decimal> TIPE_BLANKO_ID { get; set; }
        public Nullable<decimal> TIPE_PROSES_BLANKO_ID { get; set; }
        public Nullable<decimal> STATUS_PENGELOLAAN_BLANKO_ID { get; set; }
        public Nullable<System.DateTime> TANGGAL_PEMBUATAN { get; set; }
        public string BLANKOID { get; set; }
        public string USERPERORANGANID { get; set; }
    
        public virtual BERITA_ACARA BERITA_ACARA { get; set; }
        public virtual BLANKO BLANKO { get; set; }
        public virtual STATUS_PENGELOLAAN_BLANKO STATUS_PENGELOLAAN_BLANKO { get; set; }
        public virtual TIPE_BLANKO TIPE_BLANKO { get; set; }
        public virtual TIPE_PROSES_BLANKO TIPE_PROSES_BLANKO { get; set; }
        public virtual USERPERORANGAN USERPERORANGAN { get; set; }
    }
}
