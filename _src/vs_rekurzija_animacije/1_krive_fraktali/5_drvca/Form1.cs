using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Drvca
{
    public partial class Form1 : Form
    {
        const float DEG_TO_RAD = (float)Math.PI / 180.0f;
        const float DVA_PI = (float)Math.PI * 2.0f;

        string Razvoj;
        float PocDuzina = 0;
        float JedUgao = 0;
        float KoefSmanjenja = 0.6f;
        float NaDole = 0;
        float MaxRandom = 0;

        Pen Olovka = new Pen(Color.DarkGreen, 3);
        Random Rnd = new Random();

        public struct Stanje { public float X, Y, Smer; }
        Stanje TekuceStanje = new Stanje();
        Stack<Stanje> PrethodnaStanja = new Stack<Stanje>();

        public Form1()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(23, 187, 156);
            numPocDuzina.Value = 200;
            numUgaoDeg.Value = 30;
            numKoefSmanjenja.Value = 50;
            cbxRazvoj.Text = "F[-X][+X]";
            ResizeRedraw = true;
            WindowState = FormWindowState.Maximized;
        }

        private void Napred(Graphics g, float d)
        {
            float sledeceX = TekuceStanje.X + d * (float)Math.Cos(TekuceStanje.Smer);
            float sledeceY = TekuceStanje.Y - d * (float)Math.Sin(TekuceStanje.Smer);
            g.DrawLine(Olovka, TekuceStanje.X, TekuceStanje.Y, sledeceX, sledeceY);
            TekuceStanje.X = sledeceX;
            TekuceStanje.Y = sledeceY;
        }

        private void VarirajUgao()
        {
            int znak = Math.Sign(Math.Cos(TekuceStanje.Smer));
            float dodatakRandom = MaxRandom * (2 * (float)Rnd.NextDouble() - 1);
            TekuceStanje.Smer += dodatakRandom - znak * NaDole;
        }

        private void NaLevo(float ugao) { TekuceStanje.Smer += ugao; VarirajUgao(); }

        private void NaDesno(float ugao) { TekuceStanje.Smer -= ugao; VarirajUgao(); }

        private void InputChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void cbxRazvoj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string s = cbxRazvoj.Text;
                if (!cbxRazvoj.Items.Contains(s))
                    cbxRazvoj.Items.Add(s);
                Invalidate();
            }
        }

        private void Crtaj(Graphics g, int dubina, float d)
        {
            if (dubina > 1)
            {
                foreach (char c in Razvoj)
                {
                    switch (c)
                    {
                        case 'F': Napred(g, d); break;
                        case '+': NaDesno(JedUgao); break;
                        case '-': NaLevo(JedUgao); break;
                        case '[': PrethodnaStanja.Push(TekuceStanje); break;
                        case ']': TekuceStanje = PrethodnaStanja.Pop(); break;
                        case 'X': Crtaj(g, dubina - 1, d * KoefSmanjenja); break;
                    }
                }
            }
            else
                Napred(g, d);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Uvek krecemo od sredine donje ivice navise
            TekuceStanje.X = ClientSize.Width / 2;
            TekuceStanje.Y = ClientSize.Height - 10;
            TekuceStanje.Smer = 90 * DEG_TO_RAD;

            Razvoj = cbxRazvoj.Text;
            PocDuzina = (int)numPocDuzina.Value;
            JedUgao = (int)numUgaoDeg.Value * DEG_TO_RAD;
            KoefSmanjenja = (int)numKoefSmanjenja.Value * 0.01f;
            NaDole = (int)numNaDole.Value * DEG_TO_RAD;
            MaxRandom = (int)numMaxRandom.Value * DEG_TO_RAD;
            timer1.Enabled = !cbNepomican.Checked;
            int dubina = (int)numRedKrive.Value;

            Crtaj(e.Graphics, dubina, PocDuzina);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int x = ClientSize.Width - 10;
            int y = 10;

            lblRazvojText.Location = new Point(x - cbxRazvoj.Width - lblRazvojText.Width, y);
            cbxRazvoj.Location = new Point(x - cbxRazvoj.Width, y);
            y += cbxRazvoj.Height + 8;

            lblRedKriveText.Location = new Point(x - numRedKrive.Width - lblRedKriveText.Width, y);
            numRedKrive.Location = new Point(x - numRedKrive.Width, y);
            y += numRedKrive.Height + 8;

            lblPocDuzinaText.Location = new Point(x - numPocDuzina.Width - lblPocDuzinaText.Width, y);
            numPocDuzina.Location = new Point(x - numPocDuzina.Width, y);
            y += numPocDuzina.Height + 8;

            lblUgaoText.Location = new Point(x - numUgaoDeg.Width - lblUgaoText.Width, y);
            numUgaoDeg.Location = new Point(x - numUgaoDeg.Width, y);
            y += numUgaoDeg.Height + 8;

            lblKoefSmanjenjaText.Location = new Point(x - numKoefSmanjenja.Width - lblKoefSmanjenjaText.Width, y);
            numKoefSmanjenja.Location = new Point(x - numKoefSmanjenja.Width, y);
            y += numKoefSmanjenja.Height + 8;

            lblNaDoleText.Location = new Point(x - numNaDole.Width - lblNaDoleText.Width, y);
            numNaDole.Location = new Point(x - numNaDole.Width, y);
            y += numNaDole.Height + 8;

            lblMaxRandomText.Location = new Point(x - numMaxRandom.Width - lblMaxRandomText.Width, y);
            numMaxRandom.Location = new Point(x - numMaxRandom.Width, y);
            y += numMaxRandom.Height + 8;

            cbNepomican.Location = new Point(x - cbNepomican.Width, y);
        }
    }
}
