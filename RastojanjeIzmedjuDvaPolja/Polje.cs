using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RastojanjeIzmedjuDvaPolja
{
    public class Polje
    {
        public Int32 Indeks, Red, Kolona, Udaljenost;
        public static Int32 BrojRedova, BrojKolona;
        public Boolean Sanduk;
        public Button Dugme;

        public Polje(Int32 Ind, Int32 Br_Redova, Int32 Br_Kolona, Button DugmeSaForme)
        {
            Indeks = Ind;
            BrojRedova = Br_Redova;
            BrojKolona = Br_Kolona;
            Red    = (Indeks - 1) / BrojKolona + 1;
            Kolona = (Indeks - 1) % BrojKolona + 1;
            Udaljenost = -1;
            Sanduk = false;
            Dugme = DugmeSaForme;
        }
    }
}
