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

namespace UnitTest
{
    [TestClass]
    public class Zadatak2Test
    {
        //Buni me ovaj IDBroj parametar, sta sa njim?
        //Pretpostavljam da ce prava metoda procesljati neku bazu podataka i traziti ima li taj IDBroj u tabeli onih koji su glasali
        //Ispade da su sasvim dovoljna 2 stub objekta i da ignorisem ovaj parametar

        //Stoga cu uraditi na 2 nacina: 1 Spy i 2 Stub

        static IEnumerable<object[]> IspravniGlasaci
        {
            get
            {
                return new[]
                {
                    new object[] {"Elvirko-Nemirko", "Vlaho-vljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004"}
                };
            }
        }

        #region Spy


        public class SpyProvjeraStatusaGlasanjaGlasaca : IProvjera
        {
            public string IdGlasaca { get; set; }

            public bool DaLiJeVecGlasao(string IDBroj)
            {
                //Prava metoda u pravoj klasi ce vjerovatno uzeti ovaj parametar
                //procesljati neku pazu podataka i zaista vidjeti je li glasaca glasao

                //mi cemo reci da samo 1 glasac nije glasao
                if (IdGlasaca == "ElVlTa011224")
                    return false;
                else return true;
            }
        }

        [TestMethod]
        [DynamicData("IspravniGlasaci")]
        public void TestIProvjeraGlasacNijeGlasaoSpy(string ime, string prezime, string adresa, DateTime datum, string brojLicne, string maticniBroj)
        {
            Glasač glasac = new Glasač(ime, prezime, adresa, datum, brojLicne, maticniBroj);

            SpyProvjeraStatusaGlasanjaGlasaca spy = new SpyProvjeraStatusaGlasanjaGlasaca();
            spy.IdGlasaca = glasac.Id;

            Assert.IsTrue(glasac.VjerodostojnostGlasaca(spy));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestIProvjeraGlasacJesteGlasaoSpy()
        {

            Glasač glasac = new Glasač("Neko", "Drugi", "Negdje tamo 1", DateTime.Parse("01/01/1999"), "444M555", "0101999000150");

            SpyProvjeraStatusaGlasanjaGlasaca spy = new SpyProvjeraStatusaGlasanjaGlasaca();
            spy.IdGlasaca = glasac.Id;

            glasac.VjerodostojnostGlasaca(spy);
        }

        #endregion

        #region Stub

        //preko stubova

        public class StubGlasacGlasao : IProvjera
        {
            public bool DaLiJeVecGlasao(string IDBroj)
            {
                return true;
            }
        }

        public class StubGlasacNijeGlasao : IProvjera
        {
            public bool DaLiJeVecGlasao(string IDBroj)
            {
                return false;
            }
        }

        [TestMethod]
        [DynamicData("IspravniGlasaci")]
        public void TestIProvjeraGlasacNijeGlasaoStub(string ime, string prezime, string adresa, DateTime datum, string brojLicne, string maticniBroj)
        {
            Glasač glasac = new Glasač(ime, prezime, adresa, datum, brojLicne, maticniBroj);

            StubGlasacNijeGlasao stub = new StubGlasacNijeGlasao();

            Assert.IsTrue(glasac.VjerodostojnostGlasaca(stub));
        }

        [TestMethod]
        [DynamicData("IspravniGlasaci")]
        [ExpectedException(typeof(Exception))]
        public void TestIProvjeraGlasacJesteGlasaoSpy(string ime, string prezime, string adresa, DateTime datum, string brojLicne, string maticniBroj)
        {

            Glasač glasac = new Glasač(ime, prezime, adresa, datum, brojLicne, maticniBroj);

            StubGlasacGlasao stub = new StubGlasacGlasao();

            glasac.VjerodostojnostGlasaca(stub);
        }

        #endregion
    }
}
