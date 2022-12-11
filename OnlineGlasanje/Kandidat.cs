// NAPOMENA!
// 
// U ovoj klasi nalazi se kod koji je razvijen u sklopu implementacije funkcionalnosti broj 2 (iz postavke zadaće).
// Iznad svakog dijela koda, koji je dio spomenute implementacije, bit će napisan komentar "Funkcionalnost 2" (radi lakšeg 
// razgraničavanja tog koda od ostatka klase, tj. lakšeg pregleda zadaće).
// Treba istaći da su, u sklopu navedene implementacije, dodane i metode za ulazak i izlazak kandidata iz stranke. Tehnički,
// te metode nisu sastavni dio spomenute funkcionalnosti, ali prethodno nisu postojale, pa ih je bilo neophodno implementirati
// (jer inače kandidat ne bi mogao mijenjati stranku u koju je učlanjen, i ne bi imalo smisla tražiti njegova prethodna članstva).
// Autor spomenutog koda, a i kompletne funkcionalnosti broj 2, je Matija Kokor.

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

        // Funkcionalnost 2 (AUTOR: Matija Kokor)
        List<EvidencijaČlanstva> evidencijeČlanstava;
        // 

        #endregion


        #region Properties

        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public Stranka TrenutnaStranka { get => trenutnaStranka; set => trenutnaStranka = value; } 
        public int BrojGlasova { get => brojGlasova; set => brojGlasova = value; }

        // Funkcionalnost 2 (AUTOR: Matija Kokor)
        public List<EvidencijaČlanstva> EvidencijeČlanstava { get => evidencijeČlanstava; set => evidencijeČlanstava = value; }
        //

        #endregion


        #region Konstruktor

        public Kandidat(string ime, string prezime, Stranka trenutnaStranka = null) 
        { 
            Ime = ime;
            Prezime = prezime;

            // Funkcionalnost 2 (AUTOR: Matija Kokor)
            EvidencijeČlanstava = new List<EvidencijaČlanstva>();
            if (trenutnaStranka != null)
                UčlaniSe(trenutnaStranka);
            else
                TrenutnaStranka = null;
            //

            BrojGlasova = 0;
        }

        #endregion

        // Funkcionalnost 2 (AUTOR: Matija Kokor) 
        // Dakle, čitava regija.
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
            TrenutnaStranka = null;
        }

        public void UčlaniSe(Stranka stranka)
        {
            if (DaLiJeČlanStranke())
                throw new InvalidOperationException("Kandidat je već član stranke " + TrenutnaStranka.Naziv + "!");
            TrenutnaStranka = stranka;
            EvidencijeČlanstava.Add(new EvidencijaČlanstva(stranka, DateTime.Now));
        }

        public string DajOpisKandidata()
        {
            string historijaČlanstava = "";
            EvidencijeČlanstava.ForEach(evidencija =>
            {
                if (evidencija.daLiJeČlanstvoZavršeno())
                {
                    historijaČlanstava += "član stranke " + evidencija.Stranka.Naziv + " " + evidencija.DatumPočetkaČlanstva.ToString("dd/MM/yyyy") + ". do " + evidencija.DatumZavršetkaČlanstva.ToString("dd/MM/yyyy") + ".";
                }
            });
            historijaČlanstava = historijaČlanstava.Equals("") ? "Kandidat nije bio član niti jedne stranke u prošlosti!" : "Kandidat je bio " + historijaČlanstava;
            return historijaČlanstava;
        }

        public List<string> DajPrethodnaČlanstvaKandidata()
        {
            List<string> prethodnaČlanstva = new List<string>();
            EvidencijeČlanstava.ForEach(evidencija =>
            {
                if (evidencija.daLiJeČlanstvoZavršeno())
                        prethodnaČlanstva.Add(evidencija.ToString());
            });
            return prethodnaČlanstva;
        }

        #endregion

    }
}
