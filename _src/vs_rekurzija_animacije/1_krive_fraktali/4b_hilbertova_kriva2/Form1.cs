using System;
using System.Drawing;
using System.Windows.Forms;

namespace HilbertovaKriva
{
    public partial class Form1 : Form
    {
        const int DX = 3, DY = 3;
        int X, Y;

        public Form1()
        {
            InitializeComponent();
        }

        private void Linija(Graphics g, int smer)
        {
            // dole, levo, gore, desno
            int[,] smerovi = { { 0, DY }, { -DX, 0 }, { 0, -DY }, { DX, 0 } };
            int x2 = X + smerovi[smer, 0];
            int y2 = Y + smerovi[smer, 1];
            g.DrawLine(Pens.Black, X, Y, x2, y2);
            X = x2;
            Y = y2;
        }

        private void Crtaj(Graphics g, int n, int smer)
        {
            if (n > 0)
            {
                int smerPrvogDela = smer ^ 1, smerPoslednjegDela = smer ^ 3;
                Crtaj(g, n - 1, smerPrvogDela); Linija(g, smerPrvogDela);
                Crtaj(g, n - 1, smer); Linija(g, smer);
                Crtaj(g, n - 1, smer); Linija(g, smerPoslednjegDela);
                Crtaj(g, n - 1, smerPoslednjegDela);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            X = ClientSize.Width - 20;
            Y = 20;
            Crtaj(e.Graphics, (int)numRedKrive.Value, 0);
        }

        private void numRedKrive_ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

    }
}
