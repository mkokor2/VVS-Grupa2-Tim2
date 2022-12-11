using CsvHelper;
using OnlineGlasanje;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace UnitTestovi

    //testnu klasu pisala Nikolina Kokor u svrhu ostvarivanja potpune pokrivenosti koda koji realizuje funkcionalnost 3 sa zadace2
  
{
    [TestClass]
    public class TestFunkcionalnosti3
    {
        static Izbori izbori;
        static Stranka stranka1, stranka2, stranka3;
        static Kandidat kandidat1, kandidat2, kandidat3, kandidat4;
        static Glasač glasač1, glasač2, glasač3;

        //objekti se kreiraju samo jednom jer se ni u jednom testu ne vrši nikakva izmjena njihovog sadržaja
        [ClassInitialize]
        public static void InicijalizacijaPrijeSvihTestova(TestContext testContext)
        {
            izbori = new Izbori();
            stranka1 = new Stranka("SDP");
            stranka2 = new Stranka("SBB");
            stranka3 = new Stranka("BNB");
            izbori.DodajStranku(stranka1);
            izbori.DodajStranku(stranka2);
            izbori.DodajStranku(stranka3);
            kandidat1 = new Kandidat("Ivan", "Petrović", stranka1);
            kandidat2 = new Kandidat("Lejla", "Ahmetović", stranka1);
            kandidat3 = new Kandidat("Katarina", "Ćupić", stranka2);
            kandidat4 = new Kandidat("Boris", "Kontić", stranka2);
            izbori.DodajKandidata(kandidat1);
            izbori.DodajKandidata(kandidat2);
            izbori.DodajKandidata(kandidat3);
            izbori.DodajKandidata(kandidat4);
            glasač1 = new Glasač("Marko", "Marković", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(2002, 05, 09, 10, 22, 33), "123E456", "0905002673328");
            glasač2 = new Glasač("Ivana", "Ivankovic", "Sarajevo, Novo Sarajevo, Žrtava fašizma 10", new DateTime(1997, 04, 09, 10, 22, 33), "654M321", "0904997767345");
            glasač3 = new Glasač("Meho", "Mehić", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(1968, 12, 09, 10, 22, 33), "135K991", "0912968612328");
            izbori.DodajGlasača(glasač1);
            izbori.DodajGlasača(glasač2);
            izbori.DodajGlasača(glasač3);
            List<Kandidat> kandidati1 = new List<Kandidat>();
            kandidati1.Add(kandidat1);
            kandidati1.Add(kandidat2);
            List<Kandidat> kandidati2 = new List<Kandidat>();
            kandidati2.Add(kandidat4);
            izbori.GlasajZaStrankuIKandidate(glasač1, stranka1, kandidati1);
            izbori.GlasajZaStrankuIKandidate(glasač2, stranka2, kandidati2);
            izbori.GlasajZaStrankuIKandidate(glasač3, stranka2, kandidati2);
        }

        #region Data-Driven Testiranje

        //testiranje pomoću inline tipa podataka
        static IEnumerable<object[]> Stranke
        {
            get
            {
                return new[]
                {
                 new object[] { "NTN" },
                 new object[] { "RTT" },
                 new object[] { "HDZ" },
                 new object[] { "SDA" },
                 };
            }
        }

        [TestMethod]
        [DynamicData("Stranke")]
        public void TestPregledaRezultataJedneStranke(string naziv)
        {
            Stranka s = new Stranka(naziv);
            izbori.DodajStranku(s);
            Assert.IsTrue(izbori.ispisiRezultatZaStranku(s).Contains("Broj osvojenih glasova stranke: 0"));
        }

        //testiranje pomoću CSV formata

        public static IEnumerable<object[]> UčitajPodatkeCSV()
        {
            using (var reader = new StreamReader("podaci.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0] };
                }
            }
        }

        static IEnumerable<object[]> StrankeCSV
        {
            get
            {
                return UčitajPodatkeCSV();
            }
        }

        [TestMethod]
        [DynamicData("StrankeCSV")]
        public void TestBrojaKandidataStrankeKojiSuOsvojiliMandat(string naziv)
        {
            Stranka s = new Stranka(naziv);
            izbori.DodajStranku(s);
            Assert.IsTrue(izbori.ispisiRezultatZaStranku(s).Contains("Stranka osvojila mandat: NE"));
            Assert.AreEqual(izbori.dajBrojOsvojenihMandataZaStranku(s), 0);
        }


        #endregion

        #region Testovi za glavne metode za realizovanje funkcionalnosti 3

                   [TestMethod]
        public void TestIspisRezultataZaSveStranke()
        {
            StringAssert.Contains(izbori.ispisiRezultateZaSveStranke(), "Naziv stranke: BNB\nStranka osvojila mandat: NE");
            StringAssert.Contains(izbori.ispisiRezultateZaSveStranke(), "Naziv stranke: SDP\nStranka osvojila mandat: DA");
            StringAssert.Contains(izbori.ispisiRezultateZaSveStranke(), "Naziv stranke: SBB\nStranka osvojila mandat: DA");
        }

        [TestMethod]
        public void TestPrikazaRezultataZaStranke()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            izbori.prikaziRezultateZaStranke();
            StringAssert.Contains(stringWriter.ToString(), "Stranka osvojila mandat: NE");
            StringAssert.StartsWith(stringWriter.ToString(), "Naziv stranke: SDP\nStranka osvojila mandat: DA\n");
            StringAssert.Contains(stringWriter.ToString(), "Naziv stranke: BNB\nStranka osvojila mandat: NE\n");
        }

        #endregion

        #region Testovi pomoćnih metoda za realizovanje funkcionalnosti 3

        [TestMethod]
        public void TestIspisRezultataZaStranku()
        {
            StringAssert.StartsWith(izbori.ispisiRezultatZaStranku(stranka1), "Naziv stranke: SDP\nStranka osvojila mandat: DA");
        }

        [TestMethod]
        public void TestBrojaOsvojenihMandataZaStranku()
        {
            Assert.AreEqual(izbori.dajBrojOsvojenihMandataZaStranku(stranka1), 2);
            Console.WriteLine(izbori.ispisiKandidateStrankeKojiSuOsvojiliMandat(stranka1));
            Assert.AreEqual(izbori.dajBrojOsvojenihMandataZaStranku(stranka2), 1);
        }

        [TestMethod]
        public void TestIspisaKandidataStrankeKojiSuOsvojiliMandat()
        {
            StringAssert.Contains(izbori.ispisiKandidateStrankeKojiSuOsvojiliMandat(stranka2), "Boris Kontić");
            StringAssert.Contains(izbori.ispisiKandidateStrankeKojiSuOsvojiliMandat(stranka1), "Ivan Petrović");
            StringAssert.Contains(izbori.ispisiKandidateStrankeKojiSuOsvojiliMandat(stranka1), "Lejla Ahmetović");
        }

        #endregion
    }
}