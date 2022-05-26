using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sjerpinski
{
    public partial class Form1 : Form
    {
        private Brush b = new SolidBrush(Color.Yellow);

        public Form1()
        {
            InitializeComponent();
            Text = "Tepih Sjerpinskog";
            ResizeRedraw = true;
            BackColor = Color.FromArgb(23, 187, 156);
        }

        private void Crtaj(Graphics g, int x, int y, int a, int dubina)
        {
            if (dubina > 0)
            {
                a = a / 3;
                dubina--;
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                        if (dx != 0 || dy != 0)
                            Crtaj(g, x + 2 * a * dx, y + 2 * a * dy, a, dubina);
            }
            else
            {
                int d = 1;
                g.FillRectangle(b, x - a + d, y - a + d, 2 * a - 2 * d, 2 * a - 2 * d);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;
            int a = Math.Min(x, y);
            int dubina = (int)numericUpDown1.Value;
            Graphics g = e.Graphics;
            Crtaj(g, x, y, a, dubina);
        }
    }
}
