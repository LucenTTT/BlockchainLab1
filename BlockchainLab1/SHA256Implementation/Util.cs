using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainLab1.SHA256Implementation
{
    public static class Util
    {
        public static string BytesToString(IReadOnlyCollection<byte> bytes)
        {
            var stringBuilder = new StringBuilder(bytes.Count * 2);
            foreach (byte b in bytes)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }

            return stringBuilder.ToString();
        }
    }
}
