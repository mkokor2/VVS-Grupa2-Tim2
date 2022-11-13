using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGlasanje
{
    public class Izbori
    {

        #region Atributi

        List<Glasač> glasači = new List<Glasač>();
        List<Kandidat> kandidati = new List<Kandidat>();
        List<Stranka> stranke = new List<Stranka>();
        int ukupanBrojGlasova;

        #endregion


        #region Properties

        public int UkupanBrojGlasova { get => ukupanBrojGlasova; set => ukupanBrojGlasova = value; }
        public List<Glasač> Glasači { get => glasači; }
        public List<Kandidat> Kandidati { get => kandidati; }
        public List<Stranka> Stranke { get => stranke; }

        #endregion


        #region Metode

        public void DodajGlasača(Glasač glasač)
        {
            if (!Glasači.Contains(glasač))
                glasači.Add(glasač);
        }

        public void DodajKandidata(Kandidat kandidat)
        {
            if (!Kandidati.Contains(kandidat))
                kandidati.Add(kandidat);
        }

        public void DodajStranku(Stranka stranka)
        {
            if (!Stranke.Contains(stranka))
                stranke.Add(stranka);
        }

        public bool IdentificirajGlasača(string idGlasača)
        {
            foreach (Glasač glasač in glasači)
                if (glasač.Id == idGlasača)
                    return true;
            return false;
        }

        public void GlasajZaKandidata(Glasač glasač, Kandidat kandidat)
        {
            if (!IdentificirajGlasača(glasač.Id))
                throw new ArgumentException("Nevalidan glasač!");
            if (glasač.Glasao)
                throw new InvalidOperationException("Glasač je već glasao!");
            kandidat.BrojGlasova++;
            UkupanBrojGlasova++;
            glasač.Glasao = true;
        }

        public void GlasajZaStranku(Glasač glasač, Stranka stranka)
        {
            if (!IdentificirajGlasača(glasač.Id))
                throw new ArgumentException("Nevalidan glasač!");
            if (glasač.Glasao)
                throw new InvalidOperationException("Glasač je već glasao!");
            stranka.BrojGlasova++;
            UkupanBrojGlasova++;
            glasač.Glasao = true;
        }

        public List<Kandidat> DajKandidateStranke(Stranka s)
        {
            List<Kandidat> lista = new List<Kandidat>();
            foreach(Kandidat k in Kandidati)
            {
                if(k.Stranka != null && k.Stranka.Naziv.Equals(s.Naziv))
                {
                    lista.Add(k);
                }
            }
            return lista;
        }

        public bool OsvojilaStranka(Stranka stranka)
        {
            return stranka.BrojGlasova / ukupanBrojGlasova >= 0.02;
        }

        public bool OsvojioNezavisni(Kandidat kandidat)
        {
            return kandidat.BrojGlasova / UkupanBrojGlasova >= 0.02;
        }

        public List<Kandidat> DajNezavisne()
        {
            List<Kandidat> lista = new List<Kandidat>();
            foreach(Kandidat k in Kandidati)
            {
                if(k.Stranka==null)
                {
                    lista.Add(k);
                }
            } return lista;
        }

        public bool OsvojioMandatStranke(Kandidat kandidat)
        {
            int glasovi = 0;
            foreach(Kandidat k in Kandidati)
            {
                if(k.Stranka == kandidat.Stranka)
                    glasovi += k.BrojGlasova;
            }
            return kandidat.BrojGlasova >= 0.02 * glasovi;

        }

        #endregion

    }
}
