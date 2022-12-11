using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineGlasanje;
using System;
using System.Collections.Generic;
using System.Security;

namespace UnitTestovi
{
    [TestClass]
    public class TestFunkcionalnosti4
    {
        static Izbori izbori;
        static Stranka stranka1, stranka2, stranka3;
        static Kandidat kandidat1, kandidat2, kandidat3, kandidat4;
        static Glasač glasač1, glasač2, glasač3, glasač4, glasač5;
        [ClassInitialize]
        public static void InicijalizacijaPrijeTestova(TestContext testContext)
        {
            izbori = new Izbori();
            stranka1 = new Stranka("SDP");
            stranka2 = new Stranka("SBB");
            stranka3 = new Stranka("BNB");
            izbori.DodajStranku(stranka1);
            izbori.DodajStranku(stranka2);
            izbori.DodajStranku(stranka3);
            kandidat1 = new Kandidat("Ivan", "Petrović", stranka1, "0509002673328");
            kandidat2 = new Kandidat("Lejla", "Ahmetović", stranka1, "2202999100022");
            kandidat3 = new Kandidat("Katarina", "Ćupić");
            kandidat4 = new Kandidat("Boris", "Kontić", stranka2, "1209968612328");
            stranka1.RukovodstvoStranke.Add(kandidat1);
            stranka1.RukovodstvoStranke.Add(kandidat2);
            izbori.DodajKandidata(kandidat1);
            izbori.DodajKandidata(kandidat2);
            izbori.DodajKandidata(kandidat3);
            izbori.DodajKandidata(kandidat4);
            glasač1 = new Glasač("Ivan", "Petrović", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(2002, 05, 09, 10, 22, 33), "111E111", "0905002673328");
            glasač2 = new Glasač("Ivana", "Ivankovic", "Sarajevo, Novo Sarajevo, Žrtava fašizma 10", new DateTime(1997, 04, 09, 10, 22, 33), "111E111", "0904997673458");
            glasač3 = new Glasač("Boris", "Kontić", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(1968, 12, 09, 10, 22, 33), "111E111", "0912968612328");
            glasač4 = new Glasač("Neko", "Nekić", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(1988, 12, 09, 10, 22, 33), "111E111", "0912988612328");
            glasač5 = new Glasač("Lejla", "Ahmetović", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(1999, 02, 22, 10, 22, 33), "111E111", "2202999100022");
            izbori.DodajGlasača(glasač1);
            izbori.DodajGlasača(glasač2);
            izbori.DodajGlasača(glasač3);
            izbori.DodajGlasača(glasač4);
            izbori.DodajGlasača(glasač5);
            List<Kandidat> kandidati1 = new List<Kandidat>();
            kandidati1.Add(kandidat1);
            kandidati1.Add(kandidat2);
            List<Kandidat> kandidati2 = new List<Kandidat>();
            kandidati2.Add(kandidat4);
            izbori.GlasajZaStrankuIKandidate(glasač1, stranka1, kandidati1);
            izbori.GlasajZaStrankuIKandidate(glasač2, stranka1, kandidati1);
            izbori.GlasajZaStrankuIKandidate(glasač3, stranka2, kandidati2);
            izbori.GlasajZaStrankuIKandidate(glasač4,stranka1, kandidati1);
            izbori.GlasajZaStrankuIKandidate(glasač5, stranka1, kandidati1);
        }

        #region Inline testiranje

        //testiranje pomoću inline tipa podataka

        static IEnumerable<object[]> rukovodstvaStranaka
        {
            get
            {
                return new[]
                {
                    new object[] {"Admir", "Mehmedagić", stranka1, "2905999170056"},
                    new object[] {"Nikolina", "Kokor", stranka2, "2906999170055"},
                    new object[] {"Matija", "Kokor", stranka1, "2203002175543"}
                };
            }
        }
        [TestMethod]
        [DynamicData("rukovodstvaStranaka")]
        public void TestPregledajRukovodstvaStranke1(string ime, string prezime,Stranka stranka, string maticniBroj)
        {
            Assert.IsTrue(izbori.DajRukovodioceStranke(stranka1).Contains(kandidat1));
        }
        #endregion

        #region Pokrivenost testovi

        //testovi glavnih metoda funkcionalnosti 4
        [TestMethod]
        public void JeLiOsvojioMandatRukovodilac()
        {
            Assert.IsTrue(izbori.DajClanoveRukovodstvaStrankeKojiSuOsvojiliMandate(stranka1).Contains(kandidat1));
        }
        [TestMethod]
        public void SabirajuLiSeGLasovi()
        {
            Assert.AreEqual(8, izbori.DajGlasoveRukovodstvaStranke(stranka1));
        }
        [TestMethod]
        public void ProvjeriIspis()
        {
            Assert.IsTrue(izbori.IspisiInformacijeORukovodstvuStrankeKojiSuOsvojiliMandat(stranka1).Contains("Ukupan broj glasova: "));
        }
        [TestMethod]
        public void ProvjeriIspis2()
        {
            Assert.IsFalse(izbori.IspisiInformacijeORukovodstvuStrankeKojiSuOsvojiliMandat(stranka1).Contains("IvPeSa051109"));
            
        }

        #endregion

    }

}

