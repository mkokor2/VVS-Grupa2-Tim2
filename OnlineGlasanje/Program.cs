using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Versioning;

namespace OnlineGlasanje
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Izbori izbori = new Izbori();
            Glasač glasač1 = new Glasač("Marko", "Marković", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(2002, 05, 09, 10, 22, 33), "7043", "0509002673328");
            Glasač glasač2 = new Glasač("Ivana", "Ivankovic", "Sarajevo, Novo Sarajevo, Žrtava fašizma 10", new DateTime(1997, 04, 09, 10, 22, 33), "7943", "04099997673458");
            Glasač glasač3 = new Glasač("Meho", "Mehić", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(1968, 12, 09, 10, 22, 33), "6913", "1209968612328");
            Glasač glasač4 = new Glasač("Luka", "Lukić", "Sarajevo, Novo Sarajevo, Žrtava fašizma 16", new DateTime(1987, 04, 11, 10, 22, 33), "8943", "04119987633451");
            izbori.DodajGlasača(glasač1);
            izbori.DodajGlasača(glasač2);
            izbori.DodajGlasača(glasač3);
            izbori.DodajGlasača(glasač4);
            Console.WriteLine(glasač1.Id);
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
            while(true)
            {
                Console.WriteLine("Unesite vaš glasački id (-1 za izlaz): ");
                String id = Console.ReadLine();
                if(id == "-1")
                {
                    Console.WriteLine("Hvala Vam na posjeti. Dovidjenja!");
                    break;
                }
                 else if(izbori.IdentificirajGlasača(id) == false)
                {
                    Console.WriteLine("Glasač nije identificiran, probajte ponovo.");
                }
                else
                {
                    Console.WriteLine("Odaberite opciju: ");
                    Console.WriteLine("1 - Glasaj za nezavisnog kandidata");
                    Console.WriteLine("2 - Glasaj za stranku");
                    Console.WriteLine("3 - Glasaj za stranku i njene kandidate");
                    int opcija = Int32.Parse(Console.ReadLine());
                    if(opcija == 1)
                    {
                        Console.WriteLine("Unesite redni broj kandidata za kojeg želite glasati: ");
                        int redniBroj = 1;
                        foreach (Kandidat k in izbori.DajNezavisne())
                        {
                            Console.WriteLine(redniBroj + ". " + k.Ime + " " + k.Prezime);
                            redniBroj++;
                        }
                        int izbor = Int32.Parse(Console.ReadLine());
                        Glasač glasac = izbori.Glasači.Find(glasač => glasač.Id == id);
                        Kandidat kandidat = izbori.DajNezavisne()[izbor - 1];
                        Kandidat stvarniKandidat = izbori.Kandidati.Find(k => k.Ime + k.Prezime == kandidat.Ime + kandidat.Prezime);

                        try
                        { 
                            izbori.GlasajZaKandidata(glasac, stvarniKandidat);
                            Console.WriteLine("Hvala Vam na Vašem glasu!");
                        } catch (InvalidOperationException) {
                                Console.WriteLine("Već ste glasali!");
                            }
                           
                    }
                    else if(opcija == 2)
                    {
                        Console.WriteLine("Unesite redni broj stranke za koju želite glasati: ");
                        int redniBroj = 1;
                        foreach (Stranka s in izbori.Stranke)
                        {
                            Console.WriteLine(redniBroj + ". " + s.Naziv);
                            redniBroj++;
                        }
                        int izbor = Int32.Parse(Console.ReadLine());
                        Stranka stranka = izbori.Stranke[izbor - 1];
                        Glasač glasac = izbori.Glasači.Find(glasač => glasač.Id == id);
                        try
                            {
                                izbori.GlasajZaStranku(glasac, stranka);
                                Console.WriteLine("Hvala Vam na Vašem glasu!");
                            }
                            catch (InvalidOperationException)
                            {
                                Console.WriteLine("Već ste glasali!");
                            }
                    }
                    else
                    {
                        Console.WriteLine("Unesite redni broj stranke za koju želite glasati: ");
                        int redniBroj = 1;
                        foreach (Stranka s in izbori.Stranke)
                        {
                            Console.WriteLine(redniBroj + ". " + s.Naziv);
                            redniBroj++;
                        }
                        int izbor = Int32.Parse(Console.ReadLine());
                        Stranka stranka = izbori.Stranke[izbor-1];
                        Glasač glasac = izbori.Glasači.Find(glasač => glasač.Id == id);

                        Console.WriteLine(stranka.Naziv);
                        List<Kandidat> kandidati = izbori.DajKandidateStranke(stranka);
                        try
                            {
                                izbori.GlasajZaStranku(glasac, stranka);

                                Console.WriteLine("Unesite redne brojeve kandidata za koje želite glasati (npr: 1 2 5): ");
                                redniBroj = 1;
                                foreach (Kandidat k in izbori.DajKandidateStranke(stranka))
                                {
                                    Console.WriteLine(redniBroj + ". " + k.Ime + " " + k.Prezime);
                                    redniBroj++;
                                    string unos = Console.ReadLine();
                                    string[] redniBrojevi = unos.Split(' ');
                                    try
                                    {
                                        foreach(string i in redniBrojevi)
                                        {
                                            Kandidat kandidat = kandidati[Int32.Parse(i)-1];
                                            Kandidat stvarniKandidat = izbori.Kandidati.Find(k => (k.Ime + k.Prezime).Equals(kandidat.Ime + kandidat.Prezime));
                                            izbori.GlasajZaKandidata(glasac, stvarniKandidat);

                                        }
                                        Console.WriteLine("Hvala Vam na Vašem glasu!");
                                    }
                                    catch (InvalidOperationException)
                                    {
                                        Console.WriteLine("Već ste glasali!");
                                    }

                                }

                                Console.WriteLine("Hvala Vam na Vašem glasu!");
                            }
                            catch (InvalidOperationException)
                            {
                                Console.WriteLine("Već ste glasali!");
                            }

                        }
                    }
                }
            }

        }
}
