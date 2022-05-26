using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinarnoDrvo
{
    public partial class Form1 : Form
    {
        private Pen Olovka = new Pen(Color.DarkGreen, 3);
        const float DEG_TO_RAD = (float)Math.PI / 180.0f;

        public Form1()
        {
            InitializeComponent();
            Text = "Binarno drvo";
            ResizeRedraw = true;
            BackColor = Color.FromArgb(23, 187, 156);
        }

        private void Crtaj(Graphics g, float x, float y, float a, float ugao, int dubina)
        {
            float x1 = x + a * (float)Math.Cos(ugao);
            float y1 = y - a * (float)Math.Sin(ugao);
            g.DrawLine(Olovka, x, y, x1, y1);
            if (dubina > 0)
            {
                Crtaj(g, x1, y1, a * 0.6f, ugao + 35 * DEG_TO_RAD, dubina - 1);
                Crtaj(g, x1, y1, a * 0.6f, ugao - 35 * DEG_TO_RAD, dubina - 1);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            float x = ClientSize.Width / 2;
            float y = ClientSize.Height - 10;
            float a = ClientSize.Height / 3;
            float ugao = (float)Math.PI / 2;
            int dubina = (int)numericUpDown1.Value;
            Graphics g = e.Graphics;
            Crtaj(g, x, y, a, ugao, dubina);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
