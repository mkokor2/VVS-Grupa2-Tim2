using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public void GlasajZaStrankuIKandidate(Glasač glasač, Stranka stranka, List<Kandidat> kandidati)
        {
            if (!IdentificirajGlasača(glasač.Id))
                throw new ArgumentException("Nevalidan glasač!");
            if (glasač.Glasao)
                throw new InvalidOperationException("Glasač je već glasao!");
            stranka.BrojGlasova++;
            ukupanBrojGlasova++;
            foreach (Kandidat kandidat in kandidati)
                kandidat.BrojGlasova++;
            glasač.Glasao = true;
        }

        public List<Kandidat> DajKandidateStranke(Stranka stranka, List<int> redniBrojeviKandidata)
        {
            List<Kandidat> izabraniKandidati = new List<Kandidat>();
            List<Kandidat> sviKandidati = DajKandidateStranke(stranka);
            for (int i = 0; i < sviKandidati.Count; i++)
                if (!izabraniKandidati.Contains(sviKandidati[i]) && redniBrojeviKandidata.Contains(i + 1))
                    izabraniKandidati.Add(sviKandidati[i]);
            return izabraniKandidati;
        }

        public List<Kandidat> DajKandidateStranke(Stranka stranka)
        {
            List<Kandidat> kandidatiStranke = new List<Kandidat>();
            foreach(Kandidat kandidat in Kandidati)
                if(kandidat.TrenutnaStranka != null && kandidat.TrenutnaStranka.Naziv.Equals(stranka.Naziv))
                    kandidatiStranke.Add(kandidat);
            return kandidatiStranke;
        }


        public bool OsvojilaMandat(Stranka stranka)
        {
            return (double)stranka.BrojGlasova / ukupanBrojGlasova >= 0.02;
        }

        public bool OsvojioMandat(Kandidat kandidat)
        {
            return (double)kandidat.BrojGlasova / UkupanBrojGlasova >= 0.02;
        }

        public List<Kandidat> DajNezavisne()
        {
            List<Kandidat> lista = new List<Kandidat>();
            foreach(Kandidat k in Kandidati)
            {
                if(k.TrenutnaStranka==null)
                {
                    lista.Add(k);
                }
            } return lista;
        }

        public bool OsvojioMandatStranke(Kandidat kandidat)
        {
            if (kandidat.TrenutnaStranka == null)
                throw new ArgumentException("Kandidat je nezavisan!");
            return (double)kandidat.BrojGlasova / kandidat.TrenutnaStranka.BrojGlasova >= 0.2;

        }

        public double DajIzlaznost()
        {
            return ((double)UkupanBrojGlasova / Glasači.Count) * 100; 
        }

        public Glasač DajGlasača(string id)
        {
            return Glasači.Find(glasač => glasač.Id.Equals(id));
        }

        public List<Stranka> DajStrankeKojeSuOsvojileMandat()
        {
            List<Stranka> stranke = new List<Stranka>();
            foreach (Stranka stranka in Stranke)
                if (OsvojilaMandat(stranka))
                    stranke.Add(stranka);
            return stranke;
        }

        public List<Kandidat> DajKandidateKojiSuOsvojiliMandatStranke(Stranka stranka)
        {
            List<Kandidat> kandidati = new List<Kandidat>();
            DajKandidateStranke(stranka).ForEach(kandidat =>
            {
                if (OsvojioMandat(kandidat))
                    kandidati.Add(kandidat);
            });
            return kandidati;
        }
        //metodu napisao Admir Mehmedagić u sklopu funkcionalnosti 4
        public List<Kandidat> DajRukovodioceStranke(Stranka stranka)
        {
            List<Kandidat> rukovodiociStranke = new List<Kandidat>();
            foreach (Kandidat kandidat in Kandidati)
                if (kandidat.Stranka != null && kandidat.Stranka.Naziv.Equals(stranka.Naziv) && kandidat.Stranka.RukovodstvoStranke.Contains(kandidat))
                    rukovodiociStranke.Add(kandidat);
            return rukovodiociStranke;
        }
        //metodu napisao Admir Mehmedagić u sklopu funkcionalnosti 4
        public List<Kandidat> DajClanoveRukovodstvaStrankeKojiSuOsvojiliMandate(Stranka stranka)
        {
            {
                List<Kandidat> rukovodioci = new List<Kandidat>();
                DajRukovodioceStranke(stranka).ForEach(kandidat =>
                {
                    if (OsvojioMandat(kandidat))
                        rukovodioci.Add(kandidat);
                });
                return rukovodioci;
            }
        }
        //metodu napisao Admir Mehmedagić u sklopu funkcionalnosti 4
        public int DajGlasoveRukovodstvaStranke(Stranka stranka)
        {
            var lista = DajClanoveRukovodstvaStrankeKojiSuOsvojiliMandate(stranka);
            int zbir = 0;
            foreach (Kandidat kandidat in lista) zbir += kandidat.BrojGlasova;
            return zbir;
        }
        //metodu napisao Admir Mehmedagić u sklopu funkcionalnosti 4
        public String IspisiInformacijeORukovodstvuStrankeKojiSuOsvojiliMandat(Stranka stranka)
        {
            var lista = DajClanoveRukovodstvaStrankeKojiSuOsvojiliMandate(stranka);
            int ukupanBrojGlasova = DajGlasoveRukovodstvaStranke(stranka);
            string ispis = "Ukupan broj glasova: " + ukupanBrojGlasova + "; Kandidati: ";
            for(int i = 0; i < lista.Count; i++)
            {
                for(int j = 0; j < glasači.Count; j++)
                {
                    if (lista[i].MatičniBroj.Equals(glasači[j].MatičniBroj))
                    {
                        if (i == lista.Count - 1) { ispis += "Identifikacioni broj: " + glasači[j].Id; }

                        else { ispis += "Identifikacioni broj: " + glasači[j].Id + ","; }
                    }
                }
            }


        //metodu napisala Nikolina Kokor u sklopu implementacije funkcionalnosti 3 sa zadace 2
        public string ispisiKandidateStrankeKojiSuOsvojiliMandat(Stranka stranka)
        {
            string ispis = "\n";
            List<Kandidat> kandidatiSMandatom = DajKandidateKojiSuOsvojiliMandatStranke(stranka);
            for(int i = 0; i < kandidatiSMandatom.Count; i++)
            {
               int postotak = (kandidatiSMandatom[i].BrojGlasova / stranka.BrojGlasova) * 100;
               ispis += kandidatiSMandatom[i].Ime + " " + kandidatiSMandatom[i].Prezime + kandidatiSMandatom[i].BrojGlasova + "glasova, što je " + postotak + "%\n";
                
            }
            return ispis;
        }

        //metodu napisala Nikolina Kokor u sklopu implementacije funkcionalnosti 3 sa zadace 2
        public int dajBrojOsvojenihMandataZaStranku(Stranka stranka)
        {
             return DajKandidateKojiSuOsvojiliMandatStranke(stranka).Count;
        }

        //metodu napisala Nikolina Kokor u sklopu implementacije funkcionalnosti 3 sa zadace 2
        public string ispisiRezultatZaStranku(Stranka stranka)
        {
            int brojGlasova = stranka.BrojGlasova;
            int postotakGlasova = brojGlasova / ukupanBrojGlasova * 100;
            int brojOsvojenihMandata = dajBrojOsvojenihMandataZaStranku(stranka);
            string ispis = "Naziv stranke: " + stranka.Naziv;
            string osvojilaMandat = "DA";
            if(OsvojilaMandat(stranka) == false)
            {
                osvojilaMandat = "NE";
            }
            ispis += "\nStranka osvojila mandat: " + osvojilaMandat +"\n";
            ispis += ("\nBroj osvojenih glasova stranke: " + brojGlasova);
            ispis += ("\nPostotak osvojenih glasova stranke: " + postotakGlasova + "%");
            ispis += "\nBroj kandidata koji su osvojili mandat stranke: " + brojOsvojenihMandata;
            ispis += "\nKandidati koji su osvojili mandat: ";
            ispis += ispisiKandidateStrankeKojiSuOsvojiliMandat(stranka);
            return ispis;
        }

        //metodu napisala Nikolina Kokor u sklopu implementacije funkcionalnosti 3 sa zadace 2
        public string ispisiRezultateZaSveStranke()
        {
            string ispis = "";
            stranke.ForEach(stranka => ispis += ispisiRezultatZaStranku(stranka));
            return ispis;
        }

        //metodu napisala Nikolina Kokor u sklopu implementacija funkcionalnosti 3 sa zadace 2
        public void prikaziRezultateZaStranke()
        {
            Console.WriteLine(ispisiRezultateZaSveStranke());
        }

        #endregion

    }
}
