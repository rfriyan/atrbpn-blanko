using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankoATRBPN.Helper
{
    public class Utils
    {
        public static string OracleToDotNet(string text)
        {
            byte[] bytes = ParseHex(text);
            Guid guid = new Guid(bytes);
            return guid.ToString("N").ToUpperInvariant();
        }

        public static string DotNetToOracle(string text)
        {
            Guid guid = new Guid(text);
            return BitConverter.ToString(guid.ToByteArray()).Replace("-", "");
        }

        static byte[] ParseHex(string text)
        {
            // Not the most efficient code in the world, but
            // it works...
            byte[] ret = new byte[text.Length / 2];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = Convert.ToByte(text.Substring(i * 2, 2), 16);
            }
            return ret;
        }
    }
}