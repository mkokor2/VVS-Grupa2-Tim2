using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineGlasanje;
using System;
using System.Linq.Expressions;

namespace UnitTestovi
{
    [TestClass]
    public class Funkcionalnost2Testiranje
    {

        #region Atributi

        static Kandidat kandidat;
        static Stranka strankaA;
        static Stranka strankaB;

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
            Assert.IsTrue(članstvoNijeZavršeno);
            Assert.IsFalse(članstvoJeZavršeno);
        }

        #endregion

    }
}
