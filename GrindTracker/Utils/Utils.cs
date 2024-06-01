using System;

namespace GrindTracker.Utils
{
    public class Utils
    {
        public static int ParseInt(string a)
        {
            string b = string.Empty;
            int val = 0;

            for (int i=0; i< a.Length; i++)
            {
                if (Char.IsDigit(a[i]))
                    b += a[i];
            }

            if (b.Length>0)
                val = int.Parse(b);
            return val;
        }
    }
}