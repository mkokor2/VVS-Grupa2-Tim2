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

        #endregion


        #region Properties

        public string Naziv { get => naziv; set => naziv = value; }    
        public int BrojGlasova { get => brojGlasova; set => brojGlasova = value; }

        #endregion


        #region Konstruktor
        public Stranka(string naziv)
        {
            this.naziv = naziv;
            // Broj glasova se na početku postavlja na 0.
            brojGlasova = 0;
        }

        #endregion

    }
}
