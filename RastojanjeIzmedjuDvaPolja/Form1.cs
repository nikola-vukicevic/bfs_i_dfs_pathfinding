﻿/* *************************************************************************** */
/* Demonstracija algoritama BFS i DFS                                          */
/* Autor: Nikola Vukicevic (projekat za III godinu)                            */
/* 14.04.2016.                                                                 */
/* F1 - Podesavanja                                                            */ 
/* F2 - Kreiranje novog lavirinta                                              */
/* F3 - Brisanje "sanduka"                                                     */
/* F4 - Prikaz svih putanja                                                    */
/* *************************************************************************** */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RastojanjeIzmedjuDvaPolja
{
    public partial class Form1 : Form
    {
        public Tabla TablaSaPoljima;
        
        public Form1()
        {
            InitializeComponent();
            TablaSaPoljima=new Tabla(this);
            
        }
        
        private void PostavljanjeBrojaSanduka()
        {
            Form2 f2 = new Form2(this);
            f2.Owner = this;
            f2.ShowDialog();
        }

        private void ZapocinjanjePretrage(object sender, EventArgs e)
        {
            Button Dugme = (Button)sender;
            Int32  IndeksDugmeta = Convert.ToInt32(Dugme.Name.Substring(6));

            TablaSaPoljima.ZapocinjanjePretrage(IndeksDugmeta);
        }

        private void PronalazenjeSvihPutanja()
        {
            if (!TablaSaPoljima.PutanjaPronadjena)
            {
                MessageBox.Show("Nijedna putanja nije pronađena.", "GREŠKA!");
                return;
            }

            if (!TablaSaPoljima.PutanjeUpisane) TablaSaPoljima.KreiranjeListePutanja();
            
            Form3 f3 = new Form3(this);
            f3.Owner = this;
            String t = "Putanje (duzina najkrace putanje: " + TablaSaPoljima.DuzinaPutanje.ToString() +
                       "; broj putanja: " + TablaSaPoljima.Putanje.Count.ToString() + ")";
            f3.Text = t;
            Int32 i = 1;

            foreach (Putanja P in TablaSaPoljima.Putanje)
            {
                f3.listView1.Items.Add(i.ToString() + ". "+ P.IspisUString());
                i++;
            }
            f3.listView1.Items[0].Selected = true;
            f3.listView1.Select();
            f3.Location = new Point(this.Right - 250, this.Top + 60);
            f3.StartPosition = FormStartPosition.Manual;
            f3.ShowDialog();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1: PostavljanjeBrojaSanduka(); break;
                case Keys.F2: TablaSaPoljima.NoviLavirint(); break;
                case Keys.F3: TablaSaPoljima.ResetTabele(); break;
                case Keys.F4: PronalazenjeSvihPutanja(); break;
                default: break;
            }
        }

        private void Bojenje(object sender, MouseEventArgs e)
        {
            if (TablaSaPoljima.UcitanoPocetnoPolje)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                Button Dugme = (Button)sender;
                Int32 Indeks = Convert.ToInt32(Dugme.Name.Substring(6));
                Polje P = TablaSaPoljima.TabelaPolja[Indeks];

                P.Sanduk = true;
                P.Udaljenost = -2;
                P.Dugme.BackColor = Color.DarkGray;
                P.Dugme.Enabled = false;

                TablaSaPoljima.Sanduci.Add(P);
            }
        }
    }
}
