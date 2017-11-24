using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace MyFramework.Helper.StringDecrypt
{
    public static class StringDecrypt
    {
        public static string ToEncode(this string data)
        {
            if (data== null) return data;
            byte[] proclaimBytes = UnicodeEncoding.BigEndianUnicode.GetBytes(data);
            int proclaimBytesCount = proclaimBytes.Length;
            byte[] cipherBytes = new byte[proclaimBytesCount * 2];
            for (int i = 0; i < proclaimBytesCount; i += 2)
            {
                byte proclaimByte = proclaimBytes[i];
                int upperByte = proclaimByte & 0xf0;
                int lowerByte = proclaimByte & 0x0f;
                upperByte = upperByte >> 4;
                lowerByte = lowerByte << 4;

                byte proclaimByte2 = proclaimBytes[i + 1];
                int upperByte2 = proclaimByte2 & 0xf0;
                int lowerByte2 = proclaimByte2 & 0x0f;
                upperByte2 |= upperByte;
                lowerByte2 |= lowerByte;

                cipherBytes[i * 2] = 0x4e;
                cipherBytes[i * 2 + 1] = Convert.ToByte(upperByte2);
                cipherBytes[(i + 1) * 2] = 0x4f;
                cipherBytes[(i + 1) * 2 + 1] = Convert.ToByte(lowerByte2);
            }
            return UnicodeEncoding.BigEndianUnicode.GetString(cipherBytes, 0, cipherBytes.Length);
        }

        public static string ToDecode(this string data)
        {
            if (data==null) return data;

            byte[] cipherBytes = UnicodeEncoding.BigEndianUnicode.GetBytes(data);
            int cipherBytesCount = cipherBytes.Length;

            byte[] proclaimBytes = new byte[cipherBytesCount / 2];

            for (int i = 0; i < cipherBytesCount; i += 4)
            {
                byte cipherByte1 = cipherBytes[i + 1];
                byte cipherByte2 = cipherBytes[i + 3];

                int lowerByte = (cipherByte1 & 0x0f) << 4;
                int upperByte = cipherByte1 & 0xf0;

                int lowerByte2 = cipherByte2 & 0x0f;
                int upperByte2 = (cipherByte2 & 0xf0) >> 4;

                proclaimBytes[i / 2] = Convert.ToByte(lowerByte | upperByte2);
                proclaimBytes[i / 2 + 1] = Convert.ToByte(upperByte | lowerByte2);
            }
            return UnicodeEncoding.BigEndianUnicode.GetString(proclaimBytes, 0, proclaimBytes.Length);  
        }
    }
}