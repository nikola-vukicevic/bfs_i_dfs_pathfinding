using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RastojanjeIzmedjuDvaPolja
{
    public class Putanja
    {
        public Int32 BrojPolja;
        public Polje[] Polja;

        public Putanja(Stack<Polje> PoljaStek)
        {
            BrojPolja = PoljaStek.Count;
            Polja = new Polje[BrojPolja];
            PoljaStek.CopyTo(Polja, 0);
        }

        public String IspisUString()
        {
            String s = "", s_pom = "";

            for (Int32 i = BrojPolja - 1; i > 0; i--)
            {
                s_pom = String.Format("{0, -3}", Polja[i].Indeks);
                s += s_pom + " >>   ";
            }

            s += Polja[0].Indeks.ToString();

            return s;
        }
    }
}
