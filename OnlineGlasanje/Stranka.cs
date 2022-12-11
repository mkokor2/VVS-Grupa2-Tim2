using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGlasanje
{
    public class Stranka
    {

        #region Atributi

        string naziv;
        int brojGlasova;
        List<Kandidat> rukovodstvoStranke;

        #endregion


        #region Properties

        public string Naziv { get => naziv; set => naziv = value; }    
        public int BrojGlasova { get => brojGlasova; set => brojGlasova = value; }
        public List<Kandidat> RukovodstvoStranke { get => rukovodstvoStranke; set => rukovodstvoStranke = value; }

        #endregion


        #region Konstruktor
        public Stranka(string naziv)
        {
            this.naziv = naziv;
            // Broj glasova se na početku postavlja na 0.
            brojGlasova = 0;
            rukovodstvoStranke = new List<Kandidat>();
        }
        #endregion

        #region Metode
        
        #endregion

    }
}
