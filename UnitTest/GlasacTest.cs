using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineGlasanje;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 * Elvir Vlahovljak
 * 18702
 */

namespace UnitTest
{
    [TestClass]
    public class GlasacTest
    {
        #region Inline Testovi

        //new object[] {ime, prezime, adresa, datum, licna, maticni},
        static IEnumerable<object[]> NeispravniGlasaci
        {
            get
            {
                return new[]
                {
                    new object[] {"E", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004"},
                    new object[] {new string('E', 41), "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004"},
                    new object[] {"Elvir1@", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004" },

                    new object[] {"Elvirko-Nemirko", "VL", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004"},
                    new object[] {"Elvirko-Nemirko", new string('V', 51), "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004"},
                    new object[] {"Elvir", "Vlahovljak1@", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004" },
                };
            }
        }

        static IEnumerable<object[]> IspravniGlasaci
        {
            get
            {
                return new[]
                {
                    new object[] {"Elvirko-Nemirko"}
                };
            }
        }

        [TestMethod]
        [DynamicData(NeispravniGlasaci)]
        [ExpectedException(ArgumentException)]
        public void 

        #endregion

    }
}
