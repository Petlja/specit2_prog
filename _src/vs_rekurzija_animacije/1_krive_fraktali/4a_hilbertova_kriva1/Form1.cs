using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HilbertovaKriva1
{
    public partial class Form1 : Form
    {
        const int DX = 3, DY = 3;
        int X, Y;

        static Size Gore = new Size(0, -DY);
        static Size Dole = new Size(0, DY);
        static Size Levo = new Size(-DX, 0);
        static Size Desno = new Size(DX, 0);

        Dictionary<char, string> Podorijentacije = new Dictionary<char, string>
        {
            { 'c', "uccn" }, {'u', "cuu3" }, {'n', "3nnc" }, {'3', "n33u" }
        };
        Dictionary<char, Size[]> Vektori = new Dictionary<char, Size[]>
        {
            { 'c', new Size[] { Levo, Dole, Desno } },
            { 'u', new Size[] { Dole, Levo, Gore } },
            { 'n', new Size[] { Gore, Desno, Dole }  },
            { '3', new Size[] { Desno, Gore, Levo }  }
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Linija(Graphics g, Size vektor)
        {
            int x2 = X + vektor.Width;
            int y2 = Y + vektor.Height;
            g.DrawLine(Pens.Black, X, Y, x2, y2);
            X = x2;
            Y = y2;
        }

        private void Crtaj(Graphics g, int red, char orijentacija)
        {
            if (red > 0)
            {
                Crtaj(g, red - 1, Podorijentacije[orijentacija][0]);
                Linija(g, Vektori[orijentacija][0]);
                Crtaj(g, red - 1, Podorijentacije[orijentacija][1]);
                Linija(g, Vektori[orijentacija][1]);
                Crtaj(g, red - 1, Podorijentacije[orijentacija][2]);
                Linija(g, Vektori[orijentacija][2]);
                Crtaj(g, red - 1, Podorijentacije[orijentacija][3]);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            X = ClientSize.Width - 20;
            Y = 20;
            Crtaj(e.Graphics, (int)numRedKrive.Value, 'c');
        }

        private void numRedKrive_ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

    }
}
