using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Common.Security.Cryptography
{
    public static class RandomText
    {
        /// <summary>
        /// Generates an 4 letter random text.
        /// </summary>
        public static string Generate()
        {
            // Generate random text
            string s = "";
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            int index;
            int lenght = RNG.Next(4, 6);
            for (int i = 0; i < lenght; i++)
            {
                index = RNG.Next(chars.Length - 1);
                s += chars[index].ToString();
            }
            return s;
        }


        public static string Generate(int min_length, int max_length)
        {
            // Generate random text
            string s = "";
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            int index;
            int lenght = RNG.Next(min_length, max_length);
            for (int i = 0; i < lenght; i++)
            {
                index = RNG.Next(chars.Length - 1);
                s += chars[index].ToString();
            }
            return s;
        }
    }
}
