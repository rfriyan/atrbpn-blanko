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
    
    public partial class BERITA_ACARA
    {
        public BERITA_ACARA()
        {
            this.PENGELOLAAN_BLANKO = new HashSet<PENGELOLAAN_BLANKO>();
        }
    
        public decimal BERITA_ACARA_ID { get; set; }
        public string NOMOR_BERITA_ACARA { get; set; }
        public string FILE_NAME { get; set; }
    
        public virtual ICollection<PENGELOLAAN_BLANKO> PENGELOLAAN_BLANKO { get; set; }
    }
}
