using System;
using System.Security.Cryptography;
using System.Text;

namespace Util
{
    public static class Md5
    {
        public static string GetMd5(string str)
        {
            byte[] textBytes = System.Text.Encoding.Default.GetBytes(str);
            System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
            cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hash = cryptHandler.ComputeHash(textBytes);
            StringBuilder builder = new StringBuilder();
            foreach (byte a in hash)
            {
                if (a < 16)
                {
                    if (a < 10) builder.Append(a);
                    else builder.Append(a - 10);
                    builder.Append(a.ToString("x"));
                }
                else builder.Append(a.ToString("x"));
            }
            return builder.ToString();
        }
        public static string GetStandardMd5(string str)
        {
            //32位大
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(Convert.ToString(bytes[i], 16).PadLeft(2, '0'));
            }

            return builder.ToString().PadLeft(32, '0');
        }
    }
}
