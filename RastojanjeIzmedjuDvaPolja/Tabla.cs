using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace RastojanjeIzmedjuDvaPolja
{
    public class Tabla
    {
        Form Forma;
        public Int32 BrojRedova, BrojKolona, DuzinaPutanje;
        public Int32 MinBrojSanduka = 100, MaksBrojSanduka = 150, BrojPolja;
        public Boolean UcitanoPocetnoPolje, UcitanoKrajnjePolje,
                       TabelaGenerisana, PutanjaPronadjena, PutanjeUpisane;
        public Polje PocetnoPolje, KrajnjePolje;
        public Queue<Polje> RedPoljaZaIspitivanje;
        public Stack<Polje> StekPoljaZaIspitivanje;
        public List<Polje> Sanduci;
        public List<Polje> TabelaPolja;
        public List<Putanja> Putanje;

        public Tabla(Form1 forma1Konstr)
        {
            Forma = forma1Konstr;
            BrojRedova = 15;
            BrojKolona = 20;
            BrojPolja = BrojRedova * BrojKolona;
            DuzinaPutanje = 0;
            UcitanoPocetnoPolje = UcitanoKrajnjePolje =
            TabelaGenerisana = PutanjaPronadjena = PutanjeUpisane = false;
            TabelaPolja = new List<Polje>();
            Putanje     = new List<Putanja>();
            Sanduci     = new List<Polje>();
            RedPoljaZaIspitivanje  = new Queue<Polje>();
            StekPoljaZaIspitivanje = new Stack<Polje>();
            PopunjavanjeTabelePolja();
            ResetTabele();
        }

        private void PopunjavanjeTabelePolja()
        {
            Polje P = new Polje(0, BrojRedova, BrojKolona, new Button());
            TabelaPolja.Add(P);

            for (Int32 i = 1; i <= BrojPolja; i++)
            {
                String ime = "button" + i.ToString();
                Button Dugme = (Button)Forma.Controls[ime];
                P = new Polje(i, BrojRedova, BrojKolona, Dugme);
                TabelaPolja.Add(P);
            }
        }

        public void ResetTabele()
        {
            PocetnoPolje = KrajnjePolje = null;
            UcitanoPocetnoPolje = UcitanoKrajnjePolje =
            TabelaGenerisana = PutanjaPronadjena = PutanjeUpisane = false;
            DuzinaPutanje = 0;
            Putanje.Clear();
            Sanduci.Clear();

            for (Int32 i = 1; i <= BrojPolja; i++)
            {
                Polje P = TabelaPolja[i];
                P.Udaljenost = -1;
                P.Dugme.BackColor = Color.FromArgb(225, 225, 225);
                P.Dugme.Text = "";
                P.Dugme.Enabled = true;
                P.Sanduk = false;
            }

        }

        private void GenerisanjeLavirinta()
        {
            Random gsb = new Random();
            Int32 i, brojKutija = gsb.Next(MinBrojSanduka, MaksBrojSanduka);

            for (i = 1; i <= brojKutija; i++)
            {
                Int32 indeks = gsb.Next(1, BrojPolja + 1);
                Polje P = TabelaPolja[indeks];
                P.Sanduk = true;
                Sanduci.Add(P);
                P.Dugme.BackColor = Color.DarkGray;
                P.Dugme.Enabled = false;
            }
        }

        public void IscrtavanjeSanduka()
        {
            foreach (Polje P in Sanduci)
            {
                P.Dugme.BackColor = Color.DarkGray;
            }
        }

        public void NoviLavirint()
        {
            ResetTabele();
            GenerisanjeLavirinta();
        }

        public void ZapocinjanjePretrage(Int32 Indeks)
        {
            Polje P = TabelaPolja[Indeks];
            
            if (!UcitanoPocetnoPolje)
            {
                PocetnoPolje = P;
                PocetnoPolje.Dugme.Enabled = false;
                UcitanoPocetnoPolje = true;
                PocetnoPolje.Udaljenost = 0;
                PocetnoPolje.Dugme.BackColor = Color.FromArgb(60, 220, 60);
                PocetnoPolje.Dugme.Text = "0";
                return;
            }

            if (UcitanoKrajnjePolje)
            {
                return;
            }

            KrajnjePolje = P;
            KrajnjePolje.Dugme.Enabled = false;
            KrajnjePolje.Dugme.BackColor = Color.Orange;
            UcitanoKrajnjePolje = true;
            PronalazenjePuta();
        }

        private void ObradaPoljaReda(Int32 Red, Int32 Kolona, Int32 VrednostZaUpis)
        {
            if (Red >= 1 && Red <= BrojRedova && Kolona >= 1 && Kolona <= BrojKolona)
            {
                Int32 IndeksPolja = (Red - 1) * BrojKolona + Kolona;
                Polje P = TabelaPolja[IndeksPolja];

                if (P.Sanduk || P.Udaljenost > -1)
                {
                    return;
                }

                P.Udaljenost = VrednostZaUpis;
                P.Dugme.Text = P.Udaljenost.ToString();
                RedPoljaZaIspitivanje.Enqueue(P);
            }
        }

        private void ObradaRedaZaCekanje()
        {
            RedPoljaZaIspitivanje.Enqueue(PocetnoPolje);

            while (RedPoljaZaIspitivanje.Count > 0)
            {
                Polje P = RedPoljaZaIspitivanje.Dequeue();
                Int32 redPomocni, kolonaPomocna;

                redPomocni    = P.Red - 1;
                kolonaPomocna = P.Kolona;
                ObradaPoljaReda(redPomocni, kolonaPomocna, P.Udaljenost + 1);

                redPomocni    = P.Red;
                kolonaPomocna = P.Kolona - 1;
                ObradaPoljaReda(redPomocni, kolonaPomocna, P.Udaljenost + 1);

                redPomocni    = P.Red;
                kolonaPomocna = P.Kolona + 1;
                ObradaPoljaReda(redPomocni, kolonaPomocna, P.Udaljenost + 1);

                redPomocni    = P.Red + 1;
                kolonaPomocna = P.Kolona;
                ObradaPoljaReda(redPomocni, kolonaPomocna, P.Udaljenost + 1);
            }
        }

        private void PronalazenjePuta()
        {
            ObradaRedaZaCekanje();
            TabelaGenerisana = true;
            
            if (KrajnjePolje.Udaljenost != -1)
            {
                PutanjaPronadjena = true;
                DuzinaPutanje = Convert.ToInt32(KrajnjePolje.Dugme.Text);
                
                String tekstZaIspis = "Odredišno polje je dostižno u ";
                
                if (KrajnjePolje.Udaljenost > 1)
                {
                    tekstZaIspis += KrajnjePolje.Udaljenost.ToString() + " poteza.";
                }
                else
                {
                    tekstZaIspis += " prvom potezu.";
                }

                MessageBox.Show(tekstZaIspis, "Putanja uspešno pronađena.");
                return;
            }
            else
            {
                PutanjaPronadjena = false;
                MessageBox.Show("Put do odredišnog polja je blokiran!", "Putanja ne postoji");
                return;
            }
        }

        private void UpisivanjeStekaUListuPutanja()
        {
            Putanja P = new Putanja(StekPoljaZaIspitivanje);
            Putanje.Add(P);
        }

        private void ObradaPoljaSteka(Int32 Red, Int32 Kolona, Int32 PrethodnaVrednost)
        {
            Boolean uslov = Red >= 1 && Red <= BrojRedova && Kolona >= 1 && Kolona <= BrojKolona;
            if (!uslov) return;

            Int32 indeksPolja = (Red - 1) * BrojKolona + Kolona;
            Polje P = TabelaPolja[indeksPolja];

            uslov = P.Sanduk || P.Udaljenost != PrethodnaVrednost + 1 || P.Udaljenost > DuzinaPutanje;
            if (uslov) return;

            StekPoljaZaIspitivanje.Push(P);

            if (P == KrajnjePolje)
            {
                UpisivanjeStekaUListuPutanja();
                StekPoljaZaIspitivanje.Pop();
                return;
            }

            Int32 redPomocni, kolonaPomocna;

            redPomocni = Red - 1; kolonaPomocna = Kolona;
            ObradaPoljaSteka(redPomocni, kolonaPomocna, P.Udaljenost);

            redPomocni = Red; kolonaPomocna = Kolona - 1;
            ObradaPoljaSteka(redPomocni, kolonaPomocna, P.Udaljenost);

            redPomocni = Red; kolonaPomocna = Kolona + 1;
            ObradaPoljaSteka(redPomocni, kolonaPomocna, P.Udaljenost);

            redPomocni = Red + 1; kolonaPomocna = Kolona;
            ObradaPoljaSteka(redPomocni, kolonaPomocna, P.Udaljenost);

            StekPoljaZaIspitivanje.Pop();
        }

        public void KreiranjeListePutanja()
        {
            Int32 Red, Kolona;
            Red    = (PocetnoPolje.Indeks - 1) / BrojKolona + 1;
            Kolona = (PocetnoPolje.Indeks - 1) % BrojKolona + 1;
            ObradaPoljaSteka(Red, Kolona, -1);
            PutanjeUpisane = true;
        }

        public void BrisanjePutanje()
        {
            foreach (Polje P1 in TabelaPolja)
            {
                P1.Dugme.BackColor = Color.FromArgb(225, 225, 225);
            }

            IscrtavanjeSanduka();
            PocetnoPolje.Dugme.BackColor = Color.FromArgb(60, 220, 60);
            KrajnjePolje.Dugme.BackColor = Color.Orange;
 
        }

        public void IscrtavanjePutanje(Int32 RedniBrojPutanje)
        {
            BrisanjePutanje();
            
            Putanja P = Putanje[RedniBrojPutanje];
            Int32 b = P.Polja.Count();
            
            for (Int32 i = 1; i < b - 1; i++)
            {
                P.Polja[i].Dugme.BackColor = Color.FromArgb(120, 140, 240);
            } 
        }
    }
}
