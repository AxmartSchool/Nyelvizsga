using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyelvizsga
{
    class Vizsga
    {

        public string Nyelv { get; set; }
        public Dictionary<int, int> SikeresekSzama { get; set; }
        public Dictionary<int, int> SikertelenekSzama { get; set; }
        

        public static List<Vizsga> beolvasas()
        {
            var output = new List<Vizsga>();

            var sr1 = new StreamReader(@"../../res/sikeres.csv");
            var sr2 = new StreamReader(@"../../res/sikertelen.csv");
            sr1.ReadLine();
            sr2.ReadLine();
            string[] temp1;
            string[] temp2;
            while (!sr1.EndOfStream)
            {

                temp1 = sr1.ReadLine().Split(';');
                temp2 = sr2.ReadLine().Split(';');
                Vizsga vizsga = new Vizsga() { Nyelv = temp1[0], SikeresekSzama = new Dictionary<int, int>(), SikertelenekSzama = new Dictionary<int, int>() };
                for (int i = 2009; i < 2019; i++) //a for ciklus kell, h feltöltsem a dic-t
                {
                                                                    // i = 2009   [2009-2008] = [1]
                    vizsga.SikeresekSzama.Add(i, Int32.Parse(temp1[i-2008])); //i lesz a kulcs, Int32.Parse(temp1[i-2008]) az érték, parse-olni kell, mert ez string
                    vizsga.SikertelenekSzama.Add(i, Int32.Parse(temp2[i - 2008]));
                }
                output.Add(vizsga);




            }

            sr1.Close();
            sr2.Close();

            return output;
        }
    }
}
