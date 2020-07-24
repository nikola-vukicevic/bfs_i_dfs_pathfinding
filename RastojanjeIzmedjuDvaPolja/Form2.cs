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
    public partial class Form2 : Form
    {
        Form1 pozivajucaForma;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 form1Konstr)
        {
            InitializeComponent();
            pozivajucaForma = form1Konstr;
            textBox1.Text = pozivajucaForma.TablaSaPoljima.MinBrojSanduka.ToString();
            textBox2.Text = pozivajucaForma.TablaSaPoljima.MaksBrojSanduka.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Int32 Min = 0, Maks = 0;

            if (!Int32.TryParse(textBox1.Text, out Min))
            {
                MessageBox.Show("Vrednost za minimum generisanih sanduka je pogrešno uneta.", "PAŽNJA!");
                return;
            }

            if (!Int32.TryParse(textBox2.Text, out Maks))
            {
                MessageBox.Show("Vrednost za maksimum generisanih sanduka je pogrešno uneta.", "PAŽNJA!");
                return;
            }

            if (Min > Maks || Min < 0 || Maks > pozivajucaForma.TablaSaPoljima.BrojPolja)
            {
                String tekstZaIspis = "Unete vrednosti moraju poštovati sledeća pravila:\r\n\r\n" +
                                      "- Minimum mora biti veći od 0 if manji od Maksimuma;\r\n" +
                                      "-Maksimum mora biti veći od Minimuma i manji ili jednak 180;";
                MessageBox.Show(tekstZaIspis, "PAŽNJA!");
                return;
            }

            pozivajucaForma.TablaSaPoljima.MinBrojSanduka = Min;
            pozivajucaForma.TablaSaPoljima.MaksBrojSanduka = Maks;

            this.Close();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
