using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            stranka = stranka;
            brojGlasova = 0;
        }

        #endregion


        #region Metode
        public bool OsvojioMandatStranke()
        {
            return BrojGlasova >= Stranka.BrojGlasova * 0.2;
        }

        #endregion

    }
}
