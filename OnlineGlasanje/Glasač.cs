using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGlasanje
{
    public class Glasač
    {

        #region Atributi

        string ime, prezime, adresa, brojLičneKarte, matičniBroj, id;
        DateTime datumRođenja;
        bool glasao;

        #endregion


        #region Properties

        public string Ime { set => ime = value;  }
        public string Prezime { set => prezime = value; }
        public string Adresa { set => adresa = value; }
        public DateTime DatumRođenja { set => datumRođenja = value; }
        public string BrojLičneKarte { set => brojLičneKarte = value; }
        public string MatičniBroj { set => matičniBroj = value; }
        public string Id { get => id; }
        public bool Glasao { get => glasao; set => glasao = value; }

        #endregion


        #region Konstruktor

        public Glasač(string ime, string prezime, string adresa, DateTime datumRođenja, string brojLičneKarte, string matičniBroj)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.adresa = adresa;
            this.datumRođenja = datumRođenja;
            this.brojLičneKarte = brojLičneKarte;
            this.matičniBroj = matičniBroj;
            id = ime.Substring(0, 2) + prezime.Substring(0, 2) + adresa.Substring(0, 2) + datumRođenja.ToString("MM/dd/yyyy").Substring(8, 10) + brojLičneKarte.Substring(0, 2) + matičniBroj.Substring(0, 2);
            glasao = false;
        }

        #endregion

    }
}
