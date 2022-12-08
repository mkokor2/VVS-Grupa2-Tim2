namespace OnlineGlasanje
{
    public class Kandidat
    {

        #region Atributi
        
        string ime, prezime;
        Stranka stranka;
        int brojGlasova;

        #endregion


        #region Properties
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public Stranka Stranka { get => stranka; set => stranka = value; } 
        public int BrojGlasova { get => brojGlasova; set => brojGlasova = value; }

        #endregion


        #region Konstruktor

        public Kandidat(string ime, string prezime, Stranka stranka = null) 
        { 
            this.ime = ime;
            this.prezime = prezime;
            this.stranka = stranka;
            brojGlasova = 0;
        }

        #endregion


    }
}
