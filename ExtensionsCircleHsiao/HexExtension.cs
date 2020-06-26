using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCircleHsiao
{
    public static class HexExtension
    {
        /// <summary>
        /// Return a string that represents the byte array
        /// as a series of hexadecimal values separated
        /// by a separator character.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>hexadecimal formatted string</returns>
        public static string ToHex(this byte[] data)
        {
            if (data == null)
                return string.Empty;
            if (data.Length == 0)
                return string.Empty;
            StringBuilder dataStr = new StringBuilder();

            dataStr.Append("0x");
            foreach (byte b in data)
            {
                dataStr.Append(b.ToString("X2"));
            }
            return dataStr.ToString();
        }

        /// <summary>
        /// Strings to byte array.
        /// </summary>
        /// <param name="hex">The hexadecimal.</param>
        /// <returns></returns>
        public static byte[] StringToByteArray(this string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
