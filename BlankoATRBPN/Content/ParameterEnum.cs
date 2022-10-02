using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnnisaCake.Web.Content
{
    public class ParameterEnum
    {
        public enum Parameter
        {
            jenis_pesanan,
            jenis_pembelian,
            jenis_pembayaran,
            biaya_ongkir,
            jenis_pengambilan
        }

        public enum StatusPesanan
        {
            Pengecekan = 1,
            Diperoses =2,
            Diantar =3,
            Ditolak =4,
            Selesai  =5
        }
    }
}