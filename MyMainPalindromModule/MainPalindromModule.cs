using Parcs;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace MyMainPalindromModule
{
    class MainPalindromModule : IModule
    {
        static void Main(string[] args)
        {
            var job = new Job();
            if (!job.AddFile(Assembly.GetExecutingAssembly().Location/*"MyMainPalindromModule.exe"*/))
            {
                Console.WriteLine("File doesn't exist");
                return;
            }

            (new MainPalindromModule()).Run(new ModuleInfo(job, null));
            Console.ReadKey();
        }

        public void Run(ModuleInfo info, CancellationToken token = default(CancellationToken))
        {
            Console.WriteLine("Enter left border a:");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter right border b:");
            int b = Convert.ToInt32(Console.ReadLine());
            const int pointsNum = 2;
            var points = new IPoint[pointsNum];
            var channels = new IChannel[pointsNum];
            for (int i = 0; i < pointsNum; ++i)
            {
                points[i] = info.CreatePoint();
                channels[i] = points[i].CreateChannel();
                points[i].ExecuteClass("MyMainPalindromModule.PalindromModule");
            }

            int y = a;
            int c = a;
            for (int i = 0; i < pointsNum; ++i)
            {
                channels[i].WriteData(c);
                channels[i].WriteData(Math.Min(y + (int)Math.Floor((double)(b - a) / pointsNum),b));
                y += (int)Math.Floor((double)(b - a) / pointsNum);
                c = y+1;
            }
            DateTime time = DateTime.Now;
            Console.WriteLine("Waiting for result...");

            List<int> res = new List<int>();
            for (int i = 0; i < pointsNum; ++i)
            {
                res.AddRange(channels[i].ReadObject<List<int>>());
            }

            foreach (var number in res)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Number of simple palindroms: {0}, time = {1}", res.Count, Math.Round((DateTime.Now - time).TotalSeconds, 3));

        }
    }
}
