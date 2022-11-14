using System;

namespace OnlineGlasanje
{
    public class Program
    {
        #region Atributi

        static Izbori izbori = new Izbori();

        #endregion

        #region Pomoćne metode

        static void napuni()
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

        static void glasajZaNezavisnogKandidata(string idGlasača)
        {
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
                try
                {
                    izbori.GlasajZaKandidata(izbori.Glasači.Find(glasač => Equals(glasač.Id, idGlasača)), izbori.DajNezavisne()[Int32.Parse(redniBrojKandidata) - 1]);
                    break;
                }
                catch (InvalidOperationException izuzetak)
                {
                    Console.Write(izuzetak.Message + " Ponovno glasanje nije moguće!");
                    return;
                }
            }
            Console.WriteLine("Glasanje je uspješno zabilježeno!\n");
        }

        static void glasajZaStranku(string idGlasača)
        {
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
                try
                {
                    izbori.GlasajZaStranku(izbori.Glasači.Find(glasač => Equals(glasač.Id, idGlasača)), izbori.Stranke[Int32.Parse(redniBrojStranke) - 1]);
                    break;
                }
                catch (InvalidOperationException izuzetak)
                {
                    Console.Write(izuzetak.Message + " Ponovno glasanje nije moguće!");
                    return;
                }
            }
            Console.WriteLine("Glasanje je uspješno zabilježeno!\n");
        }

        static void glasajZaStrankuIKandidateIzNje(string idGlasača)
        {
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
                while (true)
                {
                    Console.WriteLine("    Aktuelni kandidati iz odabrane stranke: ");
                    redniBroj = 1;
                    izbori.DajKandidateStranke(izbori.Stranke[Int32.Parse(redniBrojStranke) - 1]).ForEach(kandidat =>
                    {
                        Console.WriteLine("        " + redniBroj + ". " + kandidat.Ime + " " + kandidat.Prezime);
                        redniBroj++;
                    });
                    Console.Write("    Unesite redni broj odabira (-1 za kraj): ");
                    string redniBrojKandidata = Console.ReadLine();
                    if (Equals(redniBrojKandidata, "-1"))
                    {
                        izbori.GlasajZaStranku(izbori.Glasači.Find(glasač => Equals(glasač.Id, idGlasača)), izbori.Stranke[Int32.Parse(redniBrojStranke) - 1]);
                        break;
                    }
                    if (Int32.Parse(redniBrojKandidata) > izbori.DajKandidateStranke(izbori.Stranke[Int32.Parse(redniBrojStranke) - 1]).Count)
                    {
                        Console.WriteLine("Nepostojeći kandidat! Probajte ponovo.");
                        continue;
                    }
                    izbori.GlasajZaKandidata(izbori.Glasači.Find(glasač => Equals(glasač.Id, idGlasača)), izbori.DajKandidateStranke(izbori.Stranke[Int32.Parse(redniBrojStranke) - 1])[Int32.Parse(redniBrojKandidata) - 1]);

                }
            }
            Console.WriteLine("Glasanje je uspješno zabilježeno!\n");
        }

        static void prikažiRezultate()
        {
            Console.WriteLine("IZLAZNOST: " + izbori.UkupanBrojGlasova + " glasača, od ukupno: " + izbori.Glasači.Count + " glasača");
            Console.WriteLine("NEZAVISNI KANDIDATI KOJI SU OSVOJILI MANDATE: ");
            int redniBroj = 1;
            izbori.DajNezavisne().ForEach(kandidat =>
            {
                if (izbori.OsvojioNezavisni(kandidat))
                    Console.WriteLine("    " + redniBroj + ". " + kandidat.Ime + " " + kandidat.Prezime);
                redniBroj++;
            });
            Console.WriteLine("STRANKE I KANDIDATI STRANKE KOJI SU OSVOJILI MANDATE: ");
            redniBroj = 1;
            izbori.Stranke.ForEach(stranka =>
            {
                if (izbori.OsvojilaStranka(stranka))
                    Console.WriteLine("    " + redniBroj + ". " + stranka.Naziv);
                    izbori.DajKandidateStranke(stranka).ForEach(kandidat =>
                    {
                        if (izbori.OsvojioMandatStranke(kandidat))
                            Console.Write("        " + kandidat.Ime + " " + kandidat.Prezime);
                    });
                redniBroj++;
            });
        }

        #endregion

        #region Main

        static void Main(string[] args)
        {
            napuni();
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
                Console.Write("\nDostupne opcije:\n    1. Glasaj za nezavisnog kandidata\n    2. Glasaj za stranku\n    3. Glasaj za stranku i kandidate iz nje\n    4. Prikaži trenutne rezultate izbora\nUnesite redni broj odabira (1, 2 ili 3): ");
                string odabir = Console.ReadLine().Trim();
                switch (odabir)
                {
                    case "1":
                        glasajZaNezavisnogKandidata(idGlasača);
                        break;
                    case "2":
                        glasajZaStranku(idGlasača);
                        break;
                    case "3":
                        glasajZaStrankuIKandidateIzNje(idGlasača);
                        break;
                    case "4":
                        prikažiRezultate();
                        break;
                }
            }
            Console.WriteLine("\n\nHvala na korištenju!");
        }

        #endregion
    }
}
