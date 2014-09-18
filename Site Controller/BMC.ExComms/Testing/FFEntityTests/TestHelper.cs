using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFEntityTests
{
    public static class TestHelper
    {
        public static byte[] GetBuffer(string input)
        {
            int idx = input.IndexOf("[");
            if (idx > 0)
            {
                input = input.Substring(idx, input.Length - idx);
            }
            string data = input.Replace("[", "").Replace("]", ",");
            string[] splitted = data.Split(new char[] { ',' });

            byte[] buffer = new byte[splitted.Length - 1];
            for (int i = 0; i < splitted.Length - 1; i++)
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(splitted[i], 16);
                }
                catch
                {
                    val = Convert.ToInt32(splitted[i], 10);
                }
                buffer[i] = (byte)val;
            }
            return buffer;
        }
    }
}
