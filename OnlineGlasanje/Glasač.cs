using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

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
                Regex imeValidacija = new Regex(@"^[\p{L}\-]{2,40}$");

                if (value == null || !imeValidacija.IsMatch(value))
                {
                    throw new ArgumentException("Ime smije sadrzavati samo slova i crticu! Ime mora biti duze od 2, a krace od 40 karaktera!");
                }

                ime = value;
            }
            get => ime;
        }
        public string Prezime { set
            {
                Regex prezimeValidacija = new Regex(@"^[\p{L}\-]{3,50}$");

                if (value == null || !prezimeValidacija.IsMatch(value))
                {
                    throw new ArgumentException("Prezime smije sadrzavati samo slova i crticu! Prezime mora biti duze od 3, a krace od 50 karaktera!");
                }

                prezime = value;
            }
            get => prezime;
        }
        public string Adresa { set
            {
                if (value == null || value.Length == 0)
                {
                    throw new ArgumentException("Adresa prebivalista ne smije biti prazna!");
                }

                adresa = value;
            }
            get => adresa;
        }
        public DateTime Datum { set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Datum rodjenja ne moze biti u buducnosti!");
                }
                else if (value.AddYears(18) > DateTime.Now)
                {
                    throw new ArgumentException("Glasac mora biti punoljetan!");
                }

                datum = value;
            }
            get => datum;
        }
        public string LičnaKarta { set
            {
                Regex licnaKartaValidacija = new Regex(@"^\d{3}[EKJMT]{1}\d{3}$");

                if (value == null || !licnaKartaValidacija.IsMatch(value))
                {
                    throw new ArgumentException("Pogresan format licne karte!");
                }

                ličnaKarta = value;
            }
            get => ličnaKarta;
        }
        public string MatičniBroj { set
            {
                Regex maticniBrojValidacija = new Regex(@"^[0-9]{13}$");

                if (value == null || !maticniBrojValidacija.IsMatch(value))
                {
                    throw new ArgumentException("Maticni broj mora imati tacno 13 brojeva!");
                }

                maticniBrojValidacija = new Regex(@"^([0-9]{2})([0-9]{2})([0-9]{3})[0-9]+$");
                Match match = maticniBrojValidacija.Match(value);
                int danUMaticnom = int.Parse(match.Groups[1].Captures[0].Value);
                int mjesecUMaticnom = int.Parse(match.Groups[2].Captures[0].Value);
                int godinaUMaticnom = int.Parse(match.Groups[3].Captures[0].Value);

                if (godinaUMaticnom % 100 == 0) 
                {
                    godinaUMaticnom += 2000;
                }
                else
                {
                    godinaUMaticnom += 1000;
                }

                if (danUMaticnom != datum.Day)
                {
                    throw new ArgumentException("Dan u maticnom broju se ne poklapa sa danom rodjenja!");
                }
                else if (mjesecUMaticnom != datum.Month)
                {
                    throw new ArgumentException("Mjesec u maticnom broju se ne poklapa sa mjesecom rodjenja!");
                }
                else if (godinaUMaticnom != datum.Year)
                {
                    throw new ArgumentException("Godina u maticnom broju se ne poklapa sa godinom rodjenja!");
                }

                matičniBroj = value;
            }
            get => matičniBroj;
        }
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

        public bool VjerodostojnostGlasaca(IProvjera sigurnosnaProvjera)
        {
            if (sigurnosnaProvjera.DaLiJeVecGlasao(id))
                throw new Exception("Glasač je već izvršio glasanje!");
            return true;
        }

        #endregion

    }
}
