// NAPOMENA!
//
// Ova klasa je implementirana za potrebe realizacije funkcionalnosti broj 2 (iz postavke zadaće).
// Njena je svrha da, u okviru svojih instanci, čuva bitne informacije vezane za članstvo nekog kandidata
// u nekoj stranci.
// Autor ove klase, a i implementacije kompletne funkcionalnosti broj 2, je Matija Kokor.

using System;

namespace OnlineGlasanje
{
    public class EvidencijaČlanstva
    {
        #region Atributi

        Kandidat kandidat;
        Stranka stranka;
        DateTime datumPočetkaČlanstva;
        DateTime datumZavršetkaČlanstva;

        #endregion


        #region Properties

        public Kandidat Kandidat { get => kandidat; set => kandidat = value; }
        public Stranka Stranka { get => stranka; set => stranka = value; }
        public DateTime DatumPočetkaČlanstva { get => datumPočetkaČlanstva; set => datumPočetkaČlanstva = value; }
        public DateTime DatumZavršetkaČlanstva { get => datumZavršetkaČlanstva; set => datumZavršetkaČlanstva = value; }

        #endregion


        #region Konstruktor

        // Konstruktor ne mora primiti četvrti parametar jer se evidencija o članstvu može kreirati neovisno od toga
        // da li je završilo članstvo kandidata ili ne (neophodno je samo da je počelo, jer nema smisla da nije).
        // U slučaju kada se ne proslijedi datum završetka članstva, u ciljani atribut se pohrani default-na vrijednost tipa DateTime
        // (a što je svakako datum koji se u realnom slučaju ne bi trebao pojaviti kao validan datum završetka članstva). 
        public EvidencijaČlanstva(Kandidat kandidat, Stranka stranka, DateTime datumPočetkaČlanstva, DateTime datumZavršetkaČlanstva = default(DateTime))
        {
            Kandidat = kandidat;
            Stranka = stranka;
            DatumPočetkaČlanstva = datumPočetkaČlanstva;
            DatumZavršetkaČlanstva = datumZavršetkaČlanstva;
        }

        #endregion
    }
}
