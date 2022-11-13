﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Ime { set => ime = value;  }
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
            this.ime = ime;
            this.prezime = prezime;
            this.adresa = adresa;
            this.datum = datumRođenja;
            ličnaKarta = brojLičneKarte;
            this.matičniBroj = matičniBroj;
            /* ID se automatski generiše na osnovu ličnih podatakaa glasača */
            id = ime.Substring(0, 2) + prezime.Substring(0, 2) + adresa.Substring(0, 2) + datumRođenja.ToString("MM/dd/yyyy").Substring(0, 2) + brojLičneKarte.Substring(0, 2) + matičniBroj.Substring(0, 2);
            glasao = false;
        }

        #endregion

    }
}
