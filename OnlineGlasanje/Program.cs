using System;

namespace OnlineGlasanje
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Izbori izbori = new Izbori();
            Glasač glasač1 = new Glasač("Marko", "Marković", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(2002, 05, 09), "7043", "0509002673328");
            Glasač glasač2 = new Glasač("Ivana", "Ivankovic", "Sarajevo, Novo Sarajevo, Žrtava fašizma 10", new DateTime(1997, 04, 09), "7943", "04099997673458");
            Glasač glasač3 = new Glasač("Meho", "Mehić", "Sarajevo, Novo Sarajevo, Zmaja od Bosne bb", new DateTime(1968, 12, 09), "6913", "1209968612328");
            Glasač glasač4 = new Glasač("Luka", "Lukić", "Sarajevo, Novo Sarajevo, Žrtava fašizma 16", new DateTime(1987, 04, 11), "8943", "04119987633451");
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
            Kandidat kandidat1 = new Kandidat("Ivan", "Petrović", stranka1);
            Kandidat kandidat2 = new Kandidat("Lejla", "Ahmetović", stranka1);
            Kandidat kandidat3 = new Kandidat("Katarina", "Ćupić", stranka2);
            Kandidat kandidat4 = new Kandidat("Boris", "Kontić", stranka2);
            izbori.DodajKandidata(kandidat1);
            izbori.DodajKandidata(kandidat2);
            izbori.DodajKandidata(kandidat3);
            izbori.DodajKandidata(kandidat4);

        }
    }
}
