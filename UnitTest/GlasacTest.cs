using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineGlasanje;
using System;

/*
 * Elvir Vlahovljak
 * 18702
 */

namespace UnitTest
{
    [TestClass]
    public class GlasacTest
    {
        static Glasač glasac;

        [TestInitialize] 
        public void InicijalizacijaPrijeSvakogTesta()
        {
            glasac = new Glasač("Elvir-Rivle", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "EJK1MTE", "2401000150004");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestImeSaBrojevima()
        {
            glasac.Ime = "Elvir123";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestImeSaNeAlfanumerickimZnakovima()
        {
            glasac.Ime = "$lv#r";
        }
    }
}
