using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyelvizsga
{
    class Program
    {
        public static List<Vizsga> Vizsgak;
        public static int bekertEv;

        static void Main(string[] args)
        {
            Vizsgak = Vizsga.beolvasas();
            legnepszerubbVizsgak();
            bekertEv = evBekeres();
            legnagyobbSikertelenVizsgak();
            vizsgazoNelkuliNyelv();
            osszesites();

            Console.ReadKey();
        }

        private static void osszesites()
        {
            var sw = new StreamWriter(@"../../res/osszesites.csv");

            foreach (var nyelv in Vizsgak)
            {
                sw.WriteLine($"{nyelv.Nyelv};{nyelv.SikeresekSzama.Values.Sum() + nyelv.SikertelenekSzama.Values.Sum()};{((double)nyelv.SikeresekSzama.Values.Sum() / (nyelv.SikeresekSzama.Values.Sum() + nyelv.SikertelenekSzama.Values.Sum())) * 100:0.00}%");

            }
            sw.Close();

        }

        private static void vizsgazoNelkuliNyelv()
        {
            Console.WriteLine("5. feladat:");
            int vizsgazok = 0;
            foreach (var nyelv in Vizsgak)
            {
                if (nyelv.SikeresekSzama[bekertEv] == 0 && nyelv.SikertelenekSzama[bekertEv] == 0)
                {
                    Console.WriteLine("\t"+nyelv.Nyelv);
                    vizsgazok++;
                }
            }

            if (vizsgazok == 0)
            {
                Console.WriteLine("Minden nyelvbol volt vizsgazo");
            }
        }

        private static void legnagyobbSikertelenVizsgak()
        {
            Console.WriteLine("4. feladat: ");


            string tempNev = "";
            double tempArany = 0;
            foreach (var vizsga in Vizsgak)
            {
                //Console.WriteLine((double)vizsga.SikertelenekSzama[bekertEv] +"\t"+ ((double)vizsga.SikeresekSzama[bekertEv] + (double)vizsga.SikertelenekSzama[bekertEv]));
                if (tempArany < (double)vizsga.SikertelenekSzama[bekertEv] / ((double)vizsga.SikeresekSzama[bekertEv]+ (double)vizsga.SikertelenekSzama[bekertEv]))
                {
                    tempArany = (double)vizsga.SikertelenekSzama[bekertEv] / ((double)vizsga.SikeresekSzama[bekertEv] + (double)vizsga.SikertelenekSzama[bekertEv]);
                    tempNev = vizsga.Nyelv;
                }

            }
            Console.WriteLine($"{bekertEv}-ben a {tempNev} nyelvbol a sikertelen vizsgak aranya {tempArany*100:0.00}%");


        }

        private static int evBekeres()
        {
            Console.Write("3. feladat: \n\tVizsgalando ev: ");
            int ev = Int32.Parse(Console.ReadLine());
            while ( ev < 2009 || ev > 2018  )
            {
                ev = Int32.Parse(Console.ReadLine());
            }

            return ev;
        }

        private static void legnepszerubbVizsgak()
        {

            Console.WriteLine("2. feladat: A legnepszerubb nyelvek: ");
            var nepszeruNyelvek =  Vizsgak.OrderByDescending(x => x.SikeresekSzama.Values.Sum() + x.SikertelenekSzama.Values.Sum()).Select(x => new { nev = x.Nyelv, vizsgakSzama = x.SikeresekSzama.Values.Sum() + x.SikertelenekSzama.Values.Sum() }).Take(3).ToList();
            foreach (var nyelv in nepszeruNyelvek)
            {
                Console.WriteLine($"\t{nyelv.nev}\t{nyelv.vizsgakSzama}");
            }


            //Nem LINQ

            //var vizsgakSzama = new Dictionary<string, int>();
            //int tempOsszeg = 0;
            //string tempNev;
            //foreach (var vizsga in Vizsgak)
            //{
            //    tempNev = vizsga.Nyelv;
            //    foreach (var evszam in vizsga.SikeresekSzama)
            //    {
            //        tempOsszeg += evszam.Value;
            //    }
            //    foreach (var evszam in vizsga.SikertelenekSzama)
            //    {
            //        tempOsszeg += evszam.Value;
            //    }
            //    vizsgakSzama.Add(tempNev, tempOsszeg);
            //    tempOsszeg = 0;

            //}

            //List<string> legnepszerubb = new List<string>();
            //string nyelvTemp = "";
            //for (int i = 0; i < 3; i++)
            //{

            //    foreach (var v in vizsgakSzama)
            //    {
            //        if (tempOsszeg < v.Value)
            //        {
            //            nyelvTemp = v.Key;
            //            tempOsszeg = v.Value;
            //        }

            //    }
            //    legnepszerubb.Add(nyelvTemp);
            //    vizsgakSzama.Remove(nyelvTemp);
            //    tempOsszeg = 0;
            //}

            //foreach (var top in legnepszerubb)
            //{
            //    Console.WriteLine("\t"+top);
            //}




        }
    }
}
