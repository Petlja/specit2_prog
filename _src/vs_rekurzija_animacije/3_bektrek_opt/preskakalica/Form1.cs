// Vrlo spora (prakticno neupotrebljiva) verzija
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Preskakalica
{
    public partial class Form1 : Form
    {
        int[] XL = { // logicke x koordinate cvorova grafa         
            0,
            2, 1, -1, -2, -1, 1,
            4, 3, 2, 0, -2, -3, -4, -3, -2, 0, 2, 3,
            6, 5, 4, 3, 1, -1, -3, -4, -5, -6, -5, -4, -3, -1, 1, 3, 4, 5
        };
        int[] YL =  { // logicke y koordinate cvorova grafa
            0,
            0, -2, -2, 0, 2, 2,
            0, -2, -4, -4, -4, -2, 0, 2, 4, 4, 4, 2,
            0, -2, -4, -6, -6, -6, -6, -4, -2, 0, 2, 4, 6, 6, 6, 6, 4, 2
        };
        int[,] Potezi = { // int[114, 3] 
            { 0, 1, 7}, { 0, 2, 9}, { 0, 3,11}, { 0, 4,13}, { 0, 5,15}, { 0, 6,17},
            { 1, 0, 4}, { 1, 2,10}, { 1, 6,16}, { 1, 7,19}, { 1, 8,21}, { 1,18,35},
            { 2, 0, 5}, { 2, 1,18}, { 2, 3,12}, { 2, 8,20}, { 2, 9,22}, { 2,10,24},
            { 3, 0, 6}, { 3, 2, 8}, { 3, 4,14}, { 3,10,23}, { 3,11,25}, { 3,12,27},
            { 4, 0, 1}, { 4, 3,10}, { 4, 5,16}, { 4,12,26}, { 4,13,28}, { 4,14,30},
            { 5, 0, 2}, { 5, 4,12}, { 5, 6,18}, { 5,14,29}, { 5,15,31}, { 5,16,33},
            { 6, 0, 3}, { 6, 1, 8}, { 6, 5,14}, { 6,16,32}, { 6,17,34}, { 6,18,36},
            { 7, 1, 0}, { 7, 8, 9}, { 7,18,17}, { 8, 1, 6}, { 8, 2, 3}, { 8, 7,36},
            { 8, 9,23}, { 9, 2, 0}, { 9, 8, 7}, { 9,10,11}, {10, 2, 1}, {10, 3, 4},
            {10, 9,21}, {10,11,26}, {11, 3, 0}, {11,10, 9}, {11,12,13}, {12, 3, 2},
            {12, 4, 5}, {12,11,24}, {12,13,29}, {13, 4, 0}, {13,12,11}, {13,14,15},
            {14, 4, 3}, {14, 5, 6}, {14,13,27}, {14,15,32}, {15, 5, 0}, {15,14,13},
            {15,16,17}, {16, 5, 4}, {16, 6, 1}, {16,15,30}, {16,17,35}, {17, 6, 0},
            {17,16,15}, {17,18, 7}, {18, 1, 2}, {18, 6, 5}, {18, 7,20}, {18,17,33},
            {19, 7, 1}, {20, 7,18}, {20, 8, 2}, {21, 8, 1}, {21, 9,10}, {22, 9, 2},
            {23, 9, 8}, {23,10, 3}, {24,10, 2}, {24,11,24}, {25,11, 3}, {26,11,10},
            {26,12, 4}, {27,12, 3}, {27,13,14}, {28,13, 4}, {29,13,12}, {29,14, 5},
            {30,14, 4}, {30,15,16}, {31,15, 5}, {32,15,14}, {32,16, 6}, {33,16, 5},
            {33,17,18}, {34,17, 6}, {35,17,16}, {35,18, 1}, {36, 7, 8}, {36,18, 6},
        };
        int R_POLJA;
        int RASTOJANJE_POLJA;
        int[] X = new int[37]; // ekranske x koordinate cvorova grafa
        int[] Y = new int[37]; // ekranske y koordinate cvorova grafa
        bool[] Zauzeto = new bool[37];
        int[] Resenje = new int[37];
        int BrVracenihPoteza = 0;
        int PocetnoPrazno = 0;

        public Form1()
        {
            InitializeComponent();

            Text = "Preskakalica";
            BackColor = Color.FromArgb(23, 187, 156);
            lbxResenje.BackColor = Color.FromArgb(23, 187, 156);
            WindowState = FormWindowState.Maximized;
            ResizeRedraw = true;
            InicijalizujKoordinate();
        }

        private void InicijalizujKoordinate()
        {
            RASTOJANJE_POLJA = Math.Min(pictureBox1.Width, pictureBox1.Height) / 20;
            R_POLJA = RASTOJANJE_POLJA / 3;
            lbxResenje.Font = new Font("Courier New", R_POLJA);
            for (int i = 0; i < 37; i++)
            {
                X[i] = XL[i] * RASTOJANJE_POLJA + pictureBox1.Width / 2;
                Y[i] = YL[i] * RASTOJANJE_POLJA + pictureBox1.Height / 2;
            }
        }

        private bool Trazi(int ostaloFigura)
        {
            if (ostaloFigura == 1)
                return true;

            for (int iPotez = 0; iPotez < Potezi.GetLength(0); iPotez++)
            {
                Resenje[ostaloFigura] = iPotez;
                if (Zauzeto[Potezi[iPotez, 0]] && Zauzeto[Potezi[iPotez, 1]] && !Zauzeto[Potezi[iPotez, 2]])
                {
                    Zauzeto[Potezi[iPotez, 0]] = false;
                    Zauzeto[Potezi[iPotez, 1]] = false;
                    Zauzeto[Potezi[iPotez, 2]] = true;

                    if (Trazi(ostaloFigura - 1))
                        return true;

                    Zauzeto[Potezi[iPotez, 0]] = true;
                    Zauzeto[Potezi[iPotez, 1]] = true;
                    Zauzeto[Potezi[iPotez, 2]] = false;
                    BrVracenihPoteza++;
                }
            }
            return false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int[] neresiva = { 0, 8, 10, 12, 14, 16, 18, 19, 22, 25, 28, 31, 34 };

            lbxResenje.Items.Clear();
            PocetnoPrazno = (int)numericUpDown1.Value;
            for (int i = 0; i < 37; i++)
                Zauzeto[i] = true;
            Zauzeto[PocetnoPrazno] = false;
            BrVracenihPoteza = 0;

            if (neresiva.Contains(PocetnoPrazno))
                lbxResenje.Items.Add("Nema resenja");
            else
            {
                Trazi(36);
                lbxResenje.Items.Add("Pocetna");
                for (int i = 36; i > 1; i--)
                {
                    int iPotez = Resenje[i];
                    lbxResenje.Items.Add(string.Format("{0, 2} - {1, 2} - {2, 2}",
                        Potezi[iPotez, 0], Potezi[iPotez, 1], Potezi[iPotez, 2]));
                }
            }
            lblBrVracenihPoteza.Text = "Broj vraćenih poteza: " + BrVracenihPoteza.ToString();
            lbxResenje.SelectedIndex = 0;
        }

        private void lbxResenje_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 37; i++)
                Zauzeto[i] = true;
            Zauzeto[PocetnoPrazno] = false;

            for (int i = 36; i > 36-lbxResenje.SelectedIndex; i--)
            {
                int a = Potezi[Resenje[i], 0];
                int b = Potezi[Resenje[i], 1];
                int c = Potezi[Resenje[i], 2];
                Zauzeto[a] = !Zauzeto[a];
                Zauzeto[b] = !Zauzeto[b];
                Zauzeto[c] = !Zauzeto[c];
            }
            pictureBox1.Invalidate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Width = ClientSize.Width - 150;
            lbxResenje.Height = ClientSize.Height - 50;
            InicijalizujKoordinate();
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen olovka = new Pen(Color.Black);
            Brush cetkaZaSlobodnoPolje = new SolidBrush(Color.Black);
            Brush cetkaZaZauzetoPolje = new SolidBrush(Color.Khaki);

            int[] leviKraj = {
                25, 23, 26, 27, 28, 29, 30, 31, 33, 28, 26, 29, 30, 31,
                32, 33, 34, 36, 31, 29, 32, 33, 34, 35, 36, 19, 21
            };
            int[] desniKraj = {
                24, 22, 21, 20, 19, 36, 35, 32, 34, 27, 25, 24, 23, 22,
                21, 20, 35, 19, 30, 28, 27, 26, 25, 24, 23, 20, 22
            };

            for (int iLin = 0; iLin < leviKraj.Length; iLin++)
                g.DrawLine(olovka, X[leviKraj[iLin]], Y[leviKraj[iLin]], X[desniKraj[iLin]], Y[desniKraj[iLin]]);

            for (int iPolje = 0; iPolje < 37; iPolje++)
            {
                Rectangle r = new Rectangle(X[iPolje] - R_POLJA, Y[iPolje] - R_POLJA, 2 * R_POLJA, 2 * R_POLJA);
                if (Zauzeto[iPolje])
                    g.FillEllipse(cetkaZaZauzetoPolje, r);
                else
                {
                    g.FillEllipse(cetkaZaSlobodnoPolje, r);
                    g.DrawEllipse(olovka, r);
                }
                Font f = new Font("Arial", R_POLJA);
                g.DrawString(iPolje.ToString(), f, cetkaZaSlobodnoPolje, X[iPolje] + R_POLJA, Y[iPolje] + R_POLJA);
            }
        }
    }
}
