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
    public class Zadatak2Test
    {
        public static Glasač glasac1;
        public static Glasač glasac2;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            glasac1 = new Glasač("Neko", "Nekic", "Tamo negdje 1", DateTime.Parse("11/04/1999"), "123E456", "1104999123456");
            glasac2 = new Glasač("Drugi", "Nekic", "Tamo negdje 1", DateTime.Parse("11/04/1999"), "123E456", "1104999123456");
        }


        //Spy

        #region Spy


        public class SpyProvjeraStatusaGlasanjaGlasaca : IProvjera
        {
            public bool DaLiJeVecGlasao(string IDBroj)
            {
                if (IDBroj == "NeNeTa041211")
                    return false;
                else return true;
            }
        }

        [TestMethod]
        public void TestZamjenskiObjekat()
        {

            SpyProvjeraStatusaGlasanjaGlasaca spy = new SpyProvjeraStatusaGlasanjaGlasaca();
            
            Assert.IsTrue(glasac1.VjerodostojnostGlasaca(spy));
            Assert.ThrowsException<Exception>(() => glasac2.VjerodostojnostGlasaca(spy));
        }

        #endregion

        #region Stub

        //preko stubova

        //public class StubGlasacGlasao : IProvjera
        //{
        //    public bool DaLiJeVecGlasao(string IDBroj)
        //    {
        //        return true;
        //    }
        //}

        //public class StubGlasacNijeGlasao : IProvjera
        //{
        //    public bool DaLiJeVecGlasao(string IDBroj)
        //    {
        //        return false;
        //    }
        //}

        //[TestMethod]
        //[DynamicData("IspravniGlasaci")]
        //public void TestIProvjeraGlasacNijeGlasaoStub(string ime, string prezime, string adresa, DateTime datum, string brojLicne, string maticniBroj)
        //{
        //    Glasač glasac = new Glasač(ime, prezime, adresa, datum, brojLicne, maticniBroj);

        //    StubGlasacNijeGlasao stub = new StubGlasacNijeGlasao();

        //    Assert.IsTrue(glasac.VjerodostojnostGlasaca(stub));
        //}

        //[TestMethod]
        //[DynamicData("IspravniGlasaci")]
        //[ExpectedException(typeof(Exception))]
        //public void TestIProvjeraGlasacJesteGlasaoSpy(string ime, string prezime, string adresa, DateTime datum, string brojLicne, string maticniBroj)
        //{

        //    Glasač glasac = new Glasač(ime, prezime, adresa, datum, brojLicne, maticniBroj);

        //    StubGlasacGlasao stub = new StubGlasacGlasao();

        //    glasac.VjerodostojnostGlasaca(stub);
        //}

        #endregion
    }
}
