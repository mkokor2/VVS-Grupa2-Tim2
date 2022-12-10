// NAPOMENA!
//
// Ova klasa je implementirana za potrebe realizacije funkcionalnosti broj 2 (iz postavke zadaće).
// Njena je svrha da, u okviru svojih instanci, čuva bitne informacije vezane za članstvo nekog kandidata
// u nekoj stranci (dakle, sav njezin kod je dio implementacije funkcionalnosti broj 2).
// Autor ove klase, a i implementacije kompletne funkcionalnosti broj 2, je Matija Kokor.

using System;

namespace OnlineGlasanje
{
    public class EvidencijaČlanstva
    {

        #region Atributi

        Stranka stranka;
        DateTime datumPočetkaČlanstva;
        DateTime datumZavršetkaČlanstva;

        #endregion


        #region Properties

        public Stranka Stranka { get => stranka; }
        public DateTime DatumPočetkaČlanstva { get => datumPočetkaČlanstva; }
        public DateTime DatumZavršetkaČlanstva { get => datumZavršetkaČlanstva; set => datumZavršetkaČlanstva = value; }

        #endregion


        #region Konstruktor

        // Konstruktor ne mora primiti četvrti parametar jer se evidencija o članstvu može kreirati neovisno od toga
        // da li je završilo članstvo kandidata ili ne (neophodno je samo da je počelo, jer nema smisla da nije).
        // U slučaju kada se ne proslijedi datum završetka članstva, u ciljani atribut se pohrani default-na vrijednost tipa DateTime
        // (a što je svakako datum koji se u realnom slučaju ne bi trebao pojaviti kao validan datum završetka članstva). 
        public EvidencijaČlanstva(Stranka stranka, DateTime datumPočetkaČlanstva, DateTime datumZavršetkaČlanstva = default(DateTime))
        {
            this.stranka = stranka;
            this.datumPočetkaČlanstva = datumPočetkaČlanstva;
            if (!datumZavršetkaČlanstva.Equals(default(DateTime)) && datumZavršetkaČlanstva < datumPočetkaČlanstva)
                throw new ArgumentException("Datum završetka članstva mora biti veći od datuma početka članstva!");
            DatumZavršetkaČlanstva = datumZavršetkaČlanstva;
        }

        #endregion


        #region Metode

        public bool daLiJeČlanstvoZavršeno()
        {
            return !DatumZavršetkaČlanstva.Equals(default(DateTime));
        }

        public override string ToString()
        {
            return "Stranka: " + Stranka.Naziv + ", Članstvo od: " + DatumPočetkaČlanstva.ToString("dd/MM/yyyy") + ", Članstvo do: " + DatumZavršetkaČlanstva.ToString("MM/dd/yyyy");
        }

        #endregion

    }
}
