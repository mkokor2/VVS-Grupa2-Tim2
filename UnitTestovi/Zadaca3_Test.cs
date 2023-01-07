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
        [TestMethod]
        public void CodeTuningTest()
        {
            Izbori izbori = new Izbori();
            Stranka testnaStranka = new Stranka("Testna stranka");

            izbori.Stranke.Add(testnaStranka);

            for (int i = 0; i < 100000; i++)
                izbori.Kandidati.Add(new Kandidat($"Ime {i}", $"Prezime {i}", testnaStranka));

            List<int> izabraniKandidati = new List<int>();

            for (int i = 0; i < 100000; i += 2)
                izabraniKandidati.Add(i + 1);

            int prviBreakpoint = 0;

            izbori.DajKandidateStranke(testnaStranka, izabraniKandidati);

            int drugiBreakpoint = 0;

            Assert.IsTrue(true);
        }
    }
}
