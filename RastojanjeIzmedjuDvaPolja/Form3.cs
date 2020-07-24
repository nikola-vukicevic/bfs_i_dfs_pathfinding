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
    public partial class Form3 : Form
    {
        Form1 pozivajucaForma;

        public Form3(Form1 form1Konstr)
        {
            InitializeComponent();
            pozivajucaForma = form1Konstr;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }

            Int32 Indeks = listView1.SelectedIndices[0];
            pozivajucaForma.TablaSaPoljima.IscrtavanjePutanje(Indeks);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            Int32 Indeks = listView1.SelectedIndices[0];
            String s = pozivajucaForma.TablaSaPoljima.Putanje[Indeks].IspisUString();
            MessageBox.Show(s, "Putanja br. " + (Indeks + 1).ToString());
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            pozivajucaForma.TablaSaPoljima.BrisanjePutanje();
        }
    }
}
