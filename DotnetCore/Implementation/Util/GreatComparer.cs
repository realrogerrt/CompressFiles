using System;
using System.Collections.Generic;
using System.Text;

namespace CompressionCore.Util
{
    public class GreatComparer: IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return (int)(y - x);
        }
    }

    public static class UtilFunctions
    {
        public static int MaxBits(int n)
        {
            if (n == 0) return 1;
            return (int)Math.Ceiling(Math.Log(n + 1, 2));
        }

        public static string ToBinaryString(int n)
        {
            string r = "";
            while (n!=0)
            {
                if (n%2==0)
                {
                    r = "0" + r;
                }
                else
                {
                    r = "1" + r;
                }
                n >>= 1;
            }

            return r;
        }
    }
}
