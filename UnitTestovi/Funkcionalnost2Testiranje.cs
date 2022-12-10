using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineGlasanje;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UnitTestovi
{

    // NAPOMENA!
    // Ova testna klasa je razvijena za potrebe testiranja funkcionalnosti 2 (iz postavke zadaće).
    // Kompletnu klasu je razvio Matija Kokor.
    [TestClass]
    public class Funkcionalnost2Testiranje
    {

        #region Atributi

        static Kandidat kandidat;
        static Stranka strankaA;
        static Stranka strankaB;

        #endregion


        #region Inline podaci za potrebe data-driven testa

        static IEnumerable<object[]> EvidencijeČlanstava
        {
            get
            {
                return new[]
                {
                    new object[] { "strankaA" },
                    new object[] { "strankaB" },
                    new object[] { "strankaC" }
                };
            }
        }

        #endregion


        #region Učitavanje podataka iz csv file-a

        public static IEnumerable<object[]> UčitajPodatkeCSV()
        {
            using (var reader = new StreamReader("Evidencije.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1], elements[2] };
                }
            }
        }

        static IEnumerable<object[]> EvidencijeCSV
        {
            get
            {
                return UčitajPodatkeCSV();
            }
        }

        #endregion


        #region Inicijalizacijske metode

        [ClassInitialize]
        public static void InicijalizacijaStranaka(TestContext context)
        {
            strankaA = new Stranka("SDA");
            strankaB = new Stranka("HDZ");
        }

        [TestInitialize]
        public void InicijalizacijaKandidata()
        {
            kandidat = new Kandidat("Marko", "Marković");
        }

        #endregion


        #region Testovi funkcionalnosti klase EvidencijaČlanstva

        /// <summary>
        /// Ovim testom se testira vrijednost datuma završetka članstva, u slučaju kada članstvo nije završeno.
        /// </summary>
        [TestMethod]
        public void TestInstanciranjaEvidencijeNezavršenogČlanstva()
        {
            EvidencijaČlanstva nezavršenoČlanstvo = new EvidencijaČlanstva(strankaA, DateTime.Now);
            Assert.AreEqual(default(DateTime), nezavršenoČlanstvo.DatumZavršetkaČlanstva);
        }


        /// <summary>
        /// Ovim testom se testira provjera da li je članstvo završeno.
        /// </summary>
        [TestMethod]
        public void TestProvjereZavršetkaČlanstva()
        {
            EvidencijaČlanstva članstvoA = new EvidencijaČlanstva(strankaA, DateTime.Now);
            EvidencijaČlanstva članstvoB = new EvidencijaČlanstva(strankaB, DateTime.Now, DateTime.Now.AddYears(2));
            bool članstvoNijeZavršeno = članstvoA.daLiJeČlanstvoZavršeno();
            bool članstvoJeZavršeno = članstvoB.daLiJeČlanstvoZavršeno();
            Assert.IsFalse(članstvoNijeZavršeno);
            Assert.IsTrue(članstvoJeZavršeno);
        }

        /// <summary>
        /// Ovim testom se testira da li je onemogućeno evidentiranje članstva kod kojeg je datum završetka
        /// prije datuma početka.
        /// </summary>
        [TestMethod, ExpectedException(typeof(ArgumentException)), DynamicData("EvidencijeCSV")]
        public void TestValidacijeNeispravnogDatumaZavršetkaČlanstva(string nazivStranke, string datumPočetka, string datumZavršetka)
        {
            new EvidencijaČlanstva(new Stranka(nazivStranke), DateTime.Parse(datumPočetka), DateTime.Parse(datumZavršetka));
        }

        #endregion


        #region Testovi funkcionalnosti u klasi Kandidat 

        /// <summary>
        /// Ovim testom se provjerava da li se pravilno evidentira svako članstvo za kandidata.
        /// </summary>
        [TestMethod]
        public void TestDodavanjaEvidencijeČlanstva()
        {
            kandidat.UčlaniSe(strankaA);
            int jednoČlanstvo = kandidat.EvidencijeČlanstava.Count;
            kandidat.ZavršiTrenutnoČlanstvo();
            kandidat.UčlaniSe(strankaB);
            int dvaČlanstva = kandidat.EvidencijeČlanstava.Count;
            Assert.IsTrue(jednoČlanstvo == 1);
            Assert.AreEqual(2, dvaČlanstva);
        }

        /// <summary>
        /// Ovim testom se testira da li se datum završetka članstva korektno postavi ako
        /// članstvo još uvijek nije završeno.
        /// </summary>
        [TestMethod]
        public void TestEvidencijeTrenutnogČlanstva()
        {
            kandidat.UčlaniSe(strankaA);
            EvidencijaČlanstva evidencijaTrenutnogČlanstva = kandidat.EvidencijeČlanstava[0];
            Assert.IsTrue(evidencijaTrenutnogČlanstva.DatumZavršetkaČlanstva < evidencijaTrenutnogČlanstva.DatumPočetkaČlanstva);
        }

        /// <summary>
        /// Ovim testom se provjerava da li je spriječeno da kandidat može istovremeno biti
        /// član dvije stranke.
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TestEvidencijeDvaTrenutnaČlanstva()
        {
            kandidat.UčlaniSe(strankaA);
            kandidat.UčlaniSe(strankaB);
        }

        [TestMethod, DynamicData("EvidencijeČlanstava")]
        public void TestIspisaEvidencijeČlanstva(string nazivStranke)
        {
            Kandidat noviKandidat = new Kandidat("Marko", "Marković");
            noviKandidat.UčlaniSe(new Stranka(nazivStranke));
            noviKandidat.ZavršiTrenutnoČlanstvo();
            StringAssert.Equals(noviKandidat.DajPrethodnaČlanstvaKandidata()[0], "Stranka: " + nazivStranke + ", Članstvo od: " + DateTime.Now.ToString("dd/MM/yyyy") + ", Članstvo do: " + DateTime.Now.ToString("dd/MM/yyyy"));
        }

        /// <summary>
        /// Ovaj test validira format ispisa opisa kandidata.
        /// </summary>
        [TestMethod]
        public void TestOpisaSaČlanstvomZaKandidata()
        {
            string opisKandidataBezČlanstva = kandidat.DajOpisKandidata();
            kandidat.UčlaniSe(strankaA);
            kandidat.ZavršiTrenutnoČlanstvo();
            kandidat.UčlaniSe(strankaB);
            string opisKandidataSaČlanstvom = kandidat.DajOpisKandidata();
            StringAssert.Equals(opisKandidataBezČlanstva, "Kandidat je bio član stranke " + strankaA.Naziv + " od " + DateTime.Now.ToString("dd/MM/yyyy") + " do " + DateTime.Now.ToString("dd/MM/yyyy") + ".");
            StringAssert.Equals(opisKandidataBezČlanstva, "Kandidat nije bio član niti jedne stranke u prošlosti!");   
        }

        /// <summary>
        /// Ovaj test testira da li se trenutno članstvo tretira kao članstvo iz "prošlosti" (tj. kao da je prošlo).
        /// </summary>
        [TestMethod]
        public void TestOpisaTrenutnogČlanstva()
        {
            Kandidat noviKandidat = new Kandidat("Ivan", "Ivanović", strankaA);
            string opisKandidata = noviKandidat.DajOpisKandidata();
            StringAssert.Equals(opisKandidata, "Kandidat nije bio član niti jedne stranke u prošlosti!");
        }

        #endregion

    }
}
