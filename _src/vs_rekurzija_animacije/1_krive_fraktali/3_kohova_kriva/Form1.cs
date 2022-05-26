using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fraktal
{
    public partial class Form1 : Form
    {
        private Pen p;

        private int Smer = 0;
        private int brSmerova = 6;
        private float[] DX = new float[6];
        private float[] DY = new float[6];
        private float X;
        private float Y;

        public Form1()
        {
            InitializeComponent();
            Text = "Kohova kriva";
            ResizeRedraw = true;
            BackColor = Color.FromArgb(23, 187, 156);
            p = new Pen(Color.Black, 2);

            double phi = 2 * Math.PI / brSmerova;
            for (int i = 0; i < brSmerova; i++)
            {
                DX[i] = (float)(Math.Cos(i * phi));
                DY[i] = (float)(Math.Sin(i * phi));
            }
        }
        private void Desno() { Smer = (Smer + 1) % brSmerova; }
        private void Levo() { Smer = (Smer + brSmerova - 1) % brSmerova; }
        private void Napred(Graphics g, float d, int dubina)
        {
            if (dubina > 1)
            {
                Napred(g, d / 3, dubina - 1); Desno();
                Napred(g, d / 3, dubina - 1); Levo(); Levo();
                Napred(g, d / 3, dubina - 1); Desno();
                Napred(g, d / 3, dubina - 1);
            }
            else
            {
                float x1 = X + d * DX[Smer];
                float y1 = Y + d * DY[Smer];
                g.DrawLine(p, X, Y, x1, y1);
                X = x1;
                Y = y1;
            }
        }


        private void Crtaj(Graphics g, int dubina)
        {
            for (int i = 0; i < 3; i++)
            {
                Napred(g, 300, dubina);
                Levo();
                Levo();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            X = 250;
            Y = 400;
            int dubina = (int)numericUpDown1.Value;
            Graphics g = e.Graphics;
            Crtaj(g, dubina);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
