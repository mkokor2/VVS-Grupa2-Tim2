using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineGlasanje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Elvir Vlahovljak
 * 18702
 */

namespace UnitTestovi
{
    [TestClass]
    public class Zadaca3Test
    {
        //DajRukovodioceStranke
        [TestMethod]
        public void TestZamjenskiObjekat()
        {
            Stranka stranka = new Stranka("Testna Stranka");

            IList<Kandidat> kandidati = new List<Kandidat>();

            for (var i = 0; i < 100000; ++i)
            {
                Kandidat k = new Kandidat($"Nezavisni-{i}", $"Nezavisni-{i}", null);
                kandidati.Add(k);

                k = new Kandidat($"Zavisni-{i}", $"Zavisni-{i}", stranka);
                kandidati.Add(k);
            }

            Izbori izbori = new Izbori();

            foreach(Kandidat k in kandidati)
            {
                //dodaj kandidata predugo traje jer poziva svaki put contains
                izbori.Kandidati.Add(k);
            }

            //pola kandidata je u rukovodstvu
            for (var i = 1; i < kandidati.Count(); i += 2)
            {
                stranka.RukovodstvoStranke.Add(kandidati.ElementAt(i));
            }

            var rukovodiociStranke = izbori.DajRukovodioceStranke(stranka);

            //za svaki slucaj da GC ne pokupi rukovodiociStranke
            foreach(Kandidat rs in rukovodiociStranke)
            {
                Console.WriteLine(rs);
            }

            Assert.IsTrue(true);
        }
    }
}
