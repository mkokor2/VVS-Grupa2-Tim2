using System;
using System.Collections.Generic;

namespace OnlineGlasanje
{
    public class Kandidat
    {

        #region Atributi
        
        string ime, prezime;
        Stranka trenutnaStranka;
        int brojGlasova;
        List<EvidencijaČlanstva> evidencijeČlanstava;

        #endregion


        #region Properties

        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public Stranka TrenutnaStranka { get => trenutnaStranka; set => trenutnaStranka = value; } 
        public int BrojGlasova { get => brojGlasova; set => brojGlasova = value; }
        public List<EvidencijaČlanstva> EvidencijeČlanstava { get => evidencijeČlanstava; set => evidencijeČlanstava = value; }

        #endregion


        #region Konstruktor

        public Kandidat(string ime, string prezime, Stranka trenutnaStranka = null) 
        { 
            this.ime = ime;
            this.prezime = prezime;
            EvidencijeČlanstava = new List<EvidencijaČlanstva>();
            if (trenutnaStranka != null)
                UčlaniSe(trenutnaStranka);
            else
                TrenutnaStranka = null;
            brojGlasova = 0;
        }

        #endregion


        #region Metode

        // Ova metoda provjerava da li je kandidat član bilo koje stranke. 
        public bool DaLiJeČlanStranke()
        {
            return TrenutnaStranka != null;
        }

        public void ZavršiTrenutnoČlanstvo()
        {
            EvidencijeČlanstava.FindLast(evidencija => evidencija.Stranka.Naziv.Equals(TrenutnaStranka.Naziv))
                               .DatumZavršetkaČlanstva = DateTime.Now;
        }

        public void UčlaniSe(Stranka stranka)
        {
            if (DaLiJeČlanStranke())
                throw new InvalidOperationException("Kandidat je već član stranke " + TrenutnaStranka.Naziv + "!");
            TrenutnaStranka = stranka;
            EvidencijeČlanstava.Add(new EvidencijaČlanstva(stranka, DateTime.Now));
        }

        #endregion

    }
}
