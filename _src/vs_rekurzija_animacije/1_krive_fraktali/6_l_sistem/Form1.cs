using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace L_sistem_0
{
    public partial class Form1 : Form
    {
        const float RAD = (float)Math.PI / 180.0f;
        public struct Stanje { public float X, Y, SmerRad; }
        public struct Fraktal
        {
            public Stanje PocStanje;
            public float D0, KoefSmanjenja, JedUgaoRad;
            public string Pupoljak, Razvoj;
            public Fraktal(float d0, float q, float x, float y, float s0, float phi, 
                string pupoljak, string zamene, float cw, float ch)
            {
                D0 = d0 * cw;
                KoefSmanjenja = q;
                PocStanje = new Stanje() { X = x * cw, Y = y * ch, SmerRad = s0 * RAD };
                JedUgaoRad = phi * RAD;
                Pupoljak = pupoljak;
                Razvoj = zamene;
            }
        }
        Stack<Stanje> PrethodnaStanja = new Stack<Stanje>();
        Fraktal[] Fraktali;

        Pen Olovka = new Pen(Color.DarkGreen, 3);
        Fraktal F; // Izabrani fraktal
        Stanje TekuceStanje;

        public Form1()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(23, 187, 156);
            float cw = ClientSize.Width;
            float ch = ClientSize.Height;
            Fraktali = new Fraktal[] 
            {
                // d0, q, x0, y0, pocSmerStepeni, jedUgaoStepeni, pupoljak, zamena)
                new Fraktal(0.8f, 1 / 3.0f, 0.1f, 0.6f, 0f, 0f, "X", "XfX", cw, ch), // Kantorov skup
                new Fraktal(0.4f, 0.5f, 0.5f, 0.9f, 90f, 30f, "X", "F[-X][+X]", cw, ch), // Drvo 2
                new Fraktal(0.4f, 0.5f, 0.5f, 0.9f, 90f, 22.5f, "X", "F[-X][+X][--X][++X]", cw, ch),// Drvo 4
                new Fraktal(0.4f, 1 / 3.0f, 0.2f, 0.7f, 0f, 60f, "X++X++X", "X-X++X-X", cw, ch), // Kohova kriva 3 (iz trougla)
                new Fraktal(0.2f, 1 / 3.0f, 0.3f, 0.8f, 0f, 60f, "X+X+X+X+X+X", "X-X++X-X", cw, ch), // Kohova kriva 6 (iz sestougla)
                new Fraktal(0.2f, 0.5f, 0.2f, 0.9f, 0f, 60f, "[X]FF++[X]FF++[X]FF++", "[X]FF++[X]FF++[X]FF++", cw, ch), // Trougao Sjerpinskog
                new Fraktal(0.15f, 0.5f, 0.5f, 0.9f, 75f, 25f, "X", "F+[[X]-X]-F[-FX]+X", cw, ch), // Biljka
                new Fraktal(0.3f, 0.3f, 0.5f, 0.5f, 0f, 60f, "[X]+[X]+[X]+[X]+[X]+[X]", "[F[+X][-X]F[X]]", cw, ch), // Pahuljica
            };
        }

        private void Napred(Graphics g, float d, bool crtajLiniju)
        {
            float sledeceX = TekuceStanje.X + d * (float)Math.Cos(TekuceStanje.SmerRad);
            float sledeceY = TekuceStanje.Y - d * (float)Math.Sin(TekuceStanje.SmerRad);
            if (crtajLiniju)
                g.DrawLine(Olovka, TekuceStanje.X, TekuceStanje.Y, sledeceX, sledeceY);

            TekuceStanje.X = sledeceX;
            TekuceStanje.Y = sledeceY;
        }

        private void Crtaj(Graphics g, int dubina, float d, string pupoljak)
        {
            foreach (char c in pupoljak)
            {
                if (c == 'F') Napred(g, d, true);
                else if (c == 'f') Napred(g, d, false);
                else if (c == '+') TekuceStanje.SmerRad += F.JedUgaoRad;
                else if (c == '-') TekuceStanje.SmerRad -= F.JedUgaoRad;
                else if (c == '[') PrethodnaStanja.Push(TekuceStanje);
                else if (c == ']') TekuceStanje = PrethodnaStanja.Pop();
                else
                {
                    if (dubina > 1)
                        Crtaj(g, dubina - 1, d * F.KoefSmanjenja, F.Razvoj);
                    else
                        Napred(g, d, true);
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                F = Fraktali[comboBox1.SelectedIndex];
                int dubina = (int)numericUpDown1.Value;
                TekuceStanje = F.PocStanje;
                Crtaj(e.Graphics, dubina, F.D0, F.Pupoljak);
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
