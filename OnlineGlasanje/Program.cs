using System;
using System.Collections.Generic;

namespace OnlineGlasanje
{
    public class Program
    {

        #region Atributi

        static Izbori izbori = new Izbori();

        #endregion

        #region Pomoćne metode

        static void Napuni()
        {
            Glasač glasač1 = new Glasač("Marko", "Marković", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(2002, 05, 09, 10, 22, 33), "7043", "0509002673328");
            Glasač glasač2 = new Glasač("Ivana", "Ivankovic", "Sarajevo, Novo Sarajevo, Žrtava fašizma 10", new DateTime(1997, 04, 09, 10, 22, 33), "7943", "04099997673458");
            Glasač glasač3 = new Glasač("Meho", "Mehić", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(1968, 12, 09, 10, 22, 33), "6913", "1209968612328");
            Glasač glasač4 = new Glasač("Luka", "Lukić", "Sarajevo, Novo Sarajevo, Žrtava fašizma 16", new DateTime(1987, 04, 11, 10, 22, 33), "8943", "04119987633451");
            izbori.DodajGlasača(glasač1);
            izbori.DodajGlasača(glasač2);
            izbori.DodajGlasača(glasač3);
            izbori.DodajGlasača(glasač4);
            Kandidat nezavisni1 = new Kandidat("Petar", "Nikolić", null);
            Kandidat nezavisni2 = new Kandidat("Marija", "Borić", null);
            Kandidat nezavisni3 = new Kandidat("Muhamed", "Halkić", null);
            Kandidat nezavisni4 = new Kandidat("Lidija", "Mirković", null);
            izbori.DodajKandidata(nezavisni1);
            izbori.DodajKandidata(nezavisni2);
            izbori.DodajKandidata(nezavisni3);
            izbori.DodajKandidata(nezavisni4);
            Stranka stranka1 = new Stranka("SDP");
            Stranka stranka2 = new Stranka("SBB");
            izbori.DodajStranku(stranka1);
            izbori.DodajStranku(stranka2);
            Kandidat kandidat1 = new Kandidat("Ivan", "Petrović", stranka1);
            Kandidat kandidat2 = new Kandidat("Lejla", "Ahmetović", stranka1);
            Kandidat kandidat3 = new Kandidat("Katarina", "Ćupić", stranka2);
            Kandidat kandidat4 = new Kandidat("Boris", "Kontić", stranka2);
            izbori.DodajKandidata(kandidat1);
            izbori.DodajKandidata(kandidat2);
            izbori.DodajKandidata(kandidat3);
            izbori.DodajKandidata(kandidat4);
        }

        static void GlasajZaNezavisnogKandidata(string idGlasača)
        {
            if (izbori.DajGlasača(idGlasača).Glasao)
            {
                Console.WriteLine("Glasač je već glasao! Ponovno glasanje nije moguće.\n");
                return;            
            }
            while (true)
            {
                Console.WriteLine("\nAktuelni kandidati:");
                int redniBroj = 1;
                izbori.DajNezavisne().ForEach(kandidat =>
                {
                    Console.WriteLine("    " + redniBroj + ". " + kandidat.Ime + " " + kandidat.Prezime);
                    redniBroj++;
                });
                Console.Write("Unesite redni broj odabira (BEZ TAČKE): ");
                string redniBrojKandidata = Console.ReadLine();
                if (Int32.Parse(redniBrojKandidata) > izbori.Kandidati.Count)
                {
                    Console.WriteLine("Nepostojeći kandidat! Probajte ponovo.");
                    continue;
                }
                izbori.GlasajZaKandidata(izbori.Glasači.Find(glasač => Equals(glasač.Id, idGlasača)), izbori.DajNezavisne()[Int32.Parse(redniBrojKandidata) - 1]);
                break;
            }
            Console.WriteLine("Glasanje je uspješno zabilježeno!\n");
        }

        static void GlasajZaStranku(string idGlasača)
        {
            if (izbori.DajGlasača(idGlasača).Glasao)
            {
                Console.WriteLine("Glasač je već glasao! Ponovno glasanje nije moguće.\n");
                return;
            }
            while (true)
            {
                Console.WriteLine("\nAktuelne stranke:");
                int redniBroj = 1;
                izbori.Stranke.ForEach(stranka =>
                {
                    Console.WriteLine("    " + redniBroj + ". " + stranka.Naziv);
                    redniBroj++;
                });
                Console.Write("Unesite redni broj odabira (BEZ TAČKE): ");
                string redniBrojStranke = Console.ReadLine();
                if (Int32.Parse(redniBrojStranke) > izbori.Stranke.Count)
                {
                    Console.WriteLine("Nepostojeća stranka! Probajte ponovo.");
                    continue;
                }
                izbori.GlasajZaStranku(izbori.Glasači.Find(glasač => Equals(glasač.Id, idGlasača)), izbori.Stranke[Int32.Parse(redniBrojStranke) - 1]);
                break;
            }
            Console.WriteLine("Glasanje je uspješno zabilježeno!\n");
        }

        static void GlasajZaStrankuIKandidateIzNje(string idGlasača)
        {
            if (izbori.DajGlasača(idGlasača).Glasao)
            {
                Console.WriteLine("Glasač je već glasao! Ponovno glasanje nije moguće.\n");
                return;
            }
            while (true)
            {
                Console.WriteLine("\nAktuelne stranke:");
                int redniBroj = 1;
                izbori.Stranke.ForEach(stranka =>
                {
                    Console.WriteLine("    " + redniBroj + ". " + stranka.Naziv);
                    redniBroj++;
                });
                Console.Write("Unesite redni broj odabira (BEZ TAČKE): ");
                string redniBrojStranke = Console.ReadLine();
                if (Int32.Parse(redniBrojStranke) > izbori.Stranke.Count)
                {
                    Console.WriteLine("Nepostojeća stranka! Probajte ponovo.");
                    continue;
                }
                Stranka izabranaStranka = izbori.Stranke[Int32.Parse(redniBrojStranke) - 1];
                while (true)
                {
                    Console.WriteLine("\n    Aktuelni kandidati iz odabrane stranke: ");
                    redniBroj = 1;
                    List<Kandidat> kandidatiStranke = izbori.DajKandidateStranke(izabranaStranka);
                    kandidatiStranke.ForEach(kandidat =>
                    {
                        Console.WriteLine("        " + redniBroj + ". " + kandidat.Ime + " " + kandidat.Prezime);
                        redniBroj++;
                    });
                    Console.Write("    Unesite redne brojeve odabira (npr. 1,2,3): ");
                    string[] uneseno = Console.ReadLine().Split(",");
                    List<int> redniBrojeviKandidata = new List<int>();
                    bool unosKorektan = true;
                    foreach (int broj in redniBrojeviKandidata)
                        if (broj < 0 || broj > kandidatiStranke.Count)
                        {
                            unosKorektan = false;
                            break;
                        } 

                    if (!unosKorektan)
                    {
                            Console.WriteLine("Unesen je nepostojeći redni broj! Probajte ponovo.");
                            continue;
                    }
                    foreach (string unos in uneseno)
                        redniBrojeviKandidata.Add(Int32.Parse(unos));
                    foreach (string unos in uneseno)
                        redniBrojeviKandidata.Add(Int32.Parse(unos));
                    izbori.GlasajZaStrankuIKandidate(izbori.DajGlasača(idGlasača), izabranaStranka, izbori.DajKandidateStranke(izabranaStranka, redniBrojeviKandidata));
                    break;
                }
                break;
            }
            Console.WriteLine("Glasanje je uspješno zabilježeno!\n");
        }

        static void PrikažiRezultate()
        {
            Console.WriteLine("\n ## IZVJEŠTAJ ## ");
            Console.WriteLine("-----------------");
            Console.WriteLine("Ukupan broj glasača: " + izbori.Glasači.Count);
            Console.WriteLine("Broj glasača koji su glasali: " + izbori.UkupanBrojGlasova);
            Console.WriteLine("Izlaznost: " + izbori.DajIzlaznost() + "%");
            Console.WriteLine("-----------------");
            Console.WriteLine("Nezavisni kandidati koji su osvojili mandate: ");
            int redniBroj = 1;
            izbori.DajNezavisne().ForEach(kandidat =>
            {
                if (izbori.OsvojioMandat(kandidat))
                    Console.WriteLine("    " + redniBroj + ". " + kandidat.Ime + " " + kandidat.Prezime);
                redniBroj++;
            });
            Console.WriteLine("-----------------");
            Console.WriteLine("Stranke i kandidati stranke koji su osvojili mandate: ");
            redniBroj = 1;
            izbori.DajStrankeKojeSuOsvojileMandat().ForEach(stranka =>
            {
                Console.WriteLine("    " + redniBroj + ". " + stranka.Naziv);
                izbori.DajKandidateKojiSuOsvojiliMandatStranke(stranka).ForEach(kandidat =>
                {
                    Console.WriteLine("     " + kandidat.Ime + " " + kandidat.Prezime);
                });
            });
            Console.WriteLine("");
        }

        #endregion

        #region Main

        static void Main(string[] args)
        {
            Napuni();
            Console.WriteLine("Dobrodošli u sistem za online glasanje!\n\n");
            while (true)
            {
                Console.Write("\nUnesite jedinstveni identifikacioni kod glasača (-1 za izlaz): ");
                string idGlasača = Console.ReadLine().Trim();
                if (Equals(idGlasača, "-1")) break;
                if (!izbori.IdentificirajGlasača(idGlasača))
                {
                    Console.WriteLine("Nepostojeći kod! Probajte ponovo.\n");
                    continue;
                }
                Console.Write("\nDostupne opcije:\n    1. Glasaj za nezavisnog kandidata\n    2. Glasaj za stranku\n    3. Glasaj za stranku i kandidate iz nje\n    4. Prikaži trenutne rezultate izbora\nUnesite redni broj odabira (1, 2, 3 ili 4): ");
                string odabir = Console.ReadLine().Trim();
                switch (odabir)
                {
                    case "1":
                        GlasajZaNezavisnogKandidata(idGlasača);
                        break;
                    case "2":
                        GlasajZaStranku(idGlasača);
                        break;
                    case "3":
                        GlasajZaStrankuIKandidateIzNje(idGlasača);
                        break;
                    case "4":
                        PrikažiRezultate();
                        break;
                }
            }
            Console.WriteLine("\n\nHvala na korištenju!");
        }

        #endregion

    }
}
