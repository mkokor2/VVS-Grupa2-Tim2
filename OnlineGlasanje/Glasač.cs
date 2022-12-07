using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

/*
 * Elvir Vlahovljak
 * 18702
 */

namespace OnlineGlasanje
{
    public class Glasač
    {

        #region Atributi

        string ime, prezime, adresa, ličnaKarta, matičniBroj, id;
        DateTime datum;
        bool glasao;

        #endregion


        #region Properties

        public string Ime { set
            {
                Regex imeValidacija = new Regex(@"^[a-zA-Z\-]{2,40}$");

                if (!imeValidacija.IsMatch(value))
                {
                    throw new ArgumentException("Ime smije sadrzavati samo slova i crticu!");
                }

                ime = value;
            }
            get => ime;
        }
        public string Prezime { set => prezime = value; }
        public string Adresa { set => adresa = value; }
        public DateTime Datum { set => datum = value; }
        public string LičnaKarta { set => ličnaKarta = value; }
        public string MatičniBroj { set => matičniBroj = value; }
        public string Id { get => id; }
        public bool Glasao { get => glasao; set => glasao = value; }

        #endregion


        #region Konstruktor

        public Glasač(string ime, string prezime, string adresa, DateTime datumRođenja, string brojLičneKarte, string matičniBroj)
        {
            Ime = ime;
            Prezime = prezime;
            Adresa = adresa;
            Datum = datumRođenja;
            LičnaKarta = brojLičneKarte;
            MatičniBroj = matičniBroj;
            /* 
             * ID se automatski generiše na osnovu ličnih podatakaa glasača
             */
            this.id = generisiID(ime, prezime, adresa, datumRođenja, brojLičneKarte, matičniBroj);
            Glasao = false;
        }

        #endregion

        #region Pomocne Metode

        public string generisiID(string ime, string prezime, string adresa, DateTime datum, string brojLicneKarte, string maticniBroj)
        {
            string id = ime.Substring(0, 2) + prezime.Substring(0, 2) + adresa.Substring(0, 2) + datum.ToString("MM/dd/yyyy").Substring(0, 2) + brojLicneKarte.Substring(0, 2) + matičniBroj.Substring(0, 2);
            return id;
        }

        #endregion

    }
}
