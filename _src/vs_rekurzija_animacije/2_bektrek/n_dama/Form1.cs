using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ndama
{
    public partial class Form1 : Form
    {
        const int VEL_POLJA = 64; // tolike su slike
        const int VEL_TABLE = 8;
        int[] MestaDama = new int[VEL_TABLE];

        int Dubina = 0;
        bool BioNaPauzi = false;

        Image[,] SlikaDame = {
            { Properties.Resources.CrnoPolje_Prazno,    Properties.Resources.BeloPolje_Prazno },
            { Properties.Resources.CrnoPolje_DobraDama, Properties.Resources.BeloPolje_DobraDama },
            { Properties.Resources.CrnoPolje_LosaDama,  Properties.Resources.BeloPolje_LosaDama }
        };

        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(VEL_TABLE * VEL_POLJA, VEL_TABLE * VEL_POLJA);
            Text = "N dama (pritisni F1 za uputstvo)";
        }

        private bool PozicijaJeRegularna() // proverava se samo poslednja dama protiv ostalih
        {
            if (Dubina < 2) return true;
            int novaKolona = Dubina - 1;
            int novRed = MestaDama[novaKolona];
            for (int prethKolona = 0; prethKolona < novaKolona; prethKolona++)
            {
                int prethRed = MestaDama[prethKolona];
                if (novRed == prethRed ||
                    novRed + novaKolona == prethRed + prethKolona ||
                    novRed - novaKolona == prethRed - prethKolona)
                    return false;
            }
            return true;
        }

        private void Korak()
        {
            bool regularna = PozicijaJeRegularna();
            if (regularna && Dubina < VEL_TABLE)
                MestaDama[Dubina++] = 0;
            else if (regularna && Dubina == VEL_TABLE && !BioNaPauzi)
            {
                timer1.Enabled = false;
                BioNaPauzi = true;
            }
            else
            {
                BioNaPauzi = false;
                while (Dubina > 0 && MestaDama[Dubina - 1] == VEL_TABLE - 1)
                    Dubina--;
                if (Dubina > 0)
                    MestaDama[Dubina - 1]++;
            }
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Korak();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: timer1.Interval = Math.Max(10, (int)(timer1.Interval * 0.9)); break;
                case Keys.Down: timer1.Interval = Math.Min(2000, (int)(timer1.Interval * 1.1)); break;
                case Keys.P: timer1.Enabled = !timer1.Enabled; break;
                case Keys.Right: Korak(); break;
                case Keys.F1: MessageBox.Show(
                    "Strelica gore - ubrzaj\n" +
                    "Strelica dole - uspori\n" +
                    "Strelica desno - sledeći korak\n" +
                    "P - pauza\n");
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush[] cetka = { new SolidBrush(Color.Black), new SolidBrush(Color.White) };
            // Prazna tabla
            for (int y = 0; y < VEL_TABLE; y++)
                for (int x = 0; x < VEL_TABLE; x++)
                    g.DrawImage(SlikaDame[0, (x + y) % 2], x * VEL_POLJA, (VEL_TABLE - 1 - y) * VEL_POLJA);

            // Sve dame osim poslednje
            int k = Dubina - 1;
            for (int i = 0; i < k; i++)
                g.DrawImage(SlikaDame[1, (i + MestaDama[i]) % 2], 
                    i * VEL_POLJA, (VEL_TABLE - 1 - MestaDama[i]) * VEL_POLJA);

            // Poslednja dama
            int indeksPoslednjeDame = PozicijaJeRegularna() ? 1 : 2;
            if (k >= 0)
                g.DrawImage(SlikaDame[indeksPoslednjeDame, (k + MestaDama[k]) % 2], 
                    k * VEL_POLJA, (VEL_TABLE - 1 - MestaDama[k]) * VEL_POLJA);
        }

    }
}
