using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineGlasanje;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;

/*
 * Testovi za funkcionalnost 1
 * Elvir Vlahovljak
 * 18702
 */

namespace UnitTestovi
{
    [TestClass]
    public class GlasacTest
    {
        #region Inline Testovi

        #region Inline Podaci

        static IEnumerable<object[]> NeispravniGlasaci
        {
            get
            {
                return new[]
                {
                    new object[] {"E", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004"},
                    new object[] {new string('E', 41), "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004"},
                    new object[] {"Elvir1@", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004" },
                    new object[] {null, "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004" },

                    new object[] {"Elvirko-Nemirko", "VL", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004"},
                    new object[] {"Elvirko-Nemirko", new string('V', 51), "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004"},
                    new object[] {"Elvir", "Vlahovljak1@", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004" },
                    new object[] {"Elvir", null, "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401000150004" },

                    new object[] {"Elvir", "Vlahovljak", "", DateTime.Parse("24/01/2000"), "123E456", "2401000150004" },
                    new object[] {"Elvir", "Vlahovljak", null, DateTime.Parse("24/01/2000"), "123E456", "2401000150004" },

                    new object[] {"El-vir", "Vlahovljak", "Tamo negdje 1", DateTime.Now.AddDays(1), "123E456", "2401000150004"},
                    new object[] {"El-vir", "Vlahovljak", "Tamo negdje 1", DateTime.Now.AddYears(-17), "123E456", "2401000150004"},

                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "12E34", "2401000150004" },
                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "E123456", "2401000150004" },
                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "EEEEEEEEEE", "2401000150004" },
                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123Q456", "2401000150004" },
                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), null, "2401000150004" },

                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "123456789012345" },
                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "123ABC123ABC123" },
                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "AB$" },
                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "0001000150004" },
                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2400000150004" },
                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "2401999150004" },
                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", "" },
                    new object[] {"Elvir", "Vlahovljak", "Tamo negdje 1", DateTime.Parse("24/01/2000"), "123E456", null },
                };
            }
        }

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

        #endregion

        #region Inline Testne Metode

        [TestMethod]
        [DynamicData("NeispravniGlasaci")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestKonstruktoraNevalidnihGlasaca(string ime, string prezime, string adresa, DateTime datum, string brojLicne, string maticniBroj)
        {
            Glasač glasac = new Glasač(ime, prezime, adresa, datum, brojLicne, maticniBroj);
        }

        [TestMethod]
        [DynamicData("IspravniGlasaci")]
        public void TestKonstruktoraValidnihGlasaca(string ime, string prezime, string adresa, DateTime datum, string brojLicne, string maticniBroj)
        {
            Glasač glasac = new Glasač(ime, prezime, adresa, datum, brojLicne, maticniBroj);

            Assert.AreEqual(glasac.Ime, ime);
            Assert.AreEqual(glasac.Prezime, prezime);
            Assert.AreEqual(glasac.Adresa, adresa);
            Assert.AreEqual(glasac.Datum, datum);
            Assert.AreEqual(glasac.LičnaKarta, brojLicne);
            Assert.AreEqual(glasac.MatičniBroj, maticniBroj);

            Assert.AreEqual(glasac.Id, "ElVlTa011224");
        }

        #endregion

        #endregion


        #region CSV Testovi

        static IEnumerable<object[]> NeispravniGlasaciCSV
        {
            get
            {
                return UcitajPodatkeCSV();
            }
        }

        public static IEnumerable<object[]> UcitajPodatkeCSV()
        {
            using (var reader = new StreamReader("Glasaci.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    for (int i = 0; i < elements.Count; i++)
                    {
                        if (elements[i] == "null")
                        {
                            elements[i] = null;
                        }
                    }
                    yield return new object[] { elements[0], elements[1], elements[2], DateTime.Parse(elements[3]), elements[4], elements[5] };
                }
            }
        }

        [TestMethod]
        [DynamicData("NeispravniGlasaciCSV")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestKonstruktoraNevalidnihGlasacaCSV(string ime, string prezime, string adresa, DateTime datum, string brojLicne, string maticniBroj)
        {
            Glasač glasac = new Glasač(ime, prezime, adresa, datum, brojLicne, maticniBroj);
        }

        #endregion

    }
}
