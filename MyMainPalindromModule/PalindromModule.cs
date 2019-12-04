using Parcs;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MyMainPalindromModule
{
    class PalindromModule : IModule
    {
        private static List<int> SimplePalindrom(int a, int b)
        {
            List<int> res = new List<int>();
            for (int j = a; j <= b; ++j)
            {
                if(IsSimple(j) && IsPalindrom(j))
                {
                    res.Add(j);
                }
            }

            return res;
        }

        private static bool IsSimple(int n)
        {
            if (n == 1) return false;

            int k = 0;

            for (int i=2;i<=(int)Math.Sqrt(n); ++i)
            {
                if (n % i == 0) { k += 1; }
            }

            if (k == 0)
                return true;

            return false;
        }

        private static bool IsPalindrom(int n)
        {
            string s1 = n.ToString();

            for (int i=0; i<s1.Length/2; ++i)
            {
                if (s1[i] != s1[s1.Length - i - 1])
                {
                    return false;
                }
            }

            return true;
        }

        public void Run(ModuleInfo info, CancellationToken token = default(CancellationToken))
        {
            int a = info.Parent.ReadInt();
            int b = info.Parent.ReadInt();
           
            List<int> res = SimplePalindrom(a, b);
            info.Parent.WriteObject(res);
        }
    }
}
