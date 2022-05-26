using System;
using System.Collections.Generic;   // IEnumerable
using System.Drawing;
using System.Text;                  // StringBuilder
using System.Threading;             // Sleep
using System.Linq;
using System.Windows.Forms;

namespace Lavirint
{
    public partial class Form1 : Form
    {
        int D = 60;
        const int MARGINA_GORE = 60, MARGINA_DOLE = 40, MARGINA_LEVO = 10, MARGINA_DESNO = 10;
        const int PROLAZ = 0, ZID = 1, TEKUCA_PUTANJA = 2, ISTRAZENO = 3, START = 8, CILJ = 9;
        static int BrojVrsta = 0, BrojKolona = 0, VrstaStart = 0, KolonaStart = 0, VrstaCilj = 0,  KolonaCilj = 0;
        static int[,] Tabla;
        static bool LavirintResen;
        static int Interval = 1000;

        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;
            tslBrzina.Text = trackBar1.Value.ToString() + " FPS";
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            PostaviDimenziju(BrojVrsta, BrojKolona);
        }

        void PostaviDimenziju(int brVrsta, int brKolona)
        {
            if (brVrsta == 0 || brKolona == 0)
                return;

            if (BrojVrsta != brVrsta || BrojKolona != brKolona)
            {
                BrojVrsta = brVrsta;
                BrojKolona = brKolona;
                Tabla = new int[BrojVrsta, BrojKolona];
            }

            int w = ClientSize.Width - MARGINA_LEVO - MARGINA_DESNO;
            int h = ClientSize.Height - MARGINA_GORE - MARGINA_DOLE;
            D = Math.Min(w / BrojKolona, h / BrojVrsta);
            Invalidate();
        }

        IEnumerable<int> NadjiPut(int v, int k)
        {
            if (v == VrstaCilj && k == KolonaCilj)
                LavirintResen = true;
            else
            {
                int[] dv = { 0, 1, 0, -1 }; // desno, dole, levo, gore
                int[] dk = { 1, 0, -1, 0 }; // desno, dole, levo, gore
                for (int smer = 0; smer < 4 && !LavirintResen; smer++)
                {
                    int v1 = v + dv[smer];
                    int k1 = k + dk[smer];
                    if (0 <= v1 && v1 < BrojVrsta && 0 <= k1 && k1 < BrojKolona && 
                        (Tabla[v1, k1] == PROLAZ || Tabla[v1, k1] == CILJ))
                    {
                        if (Tabla[v, k] != START)
                            Tabla[v, k] = TEKUCA_PUTANJA;
                        yield return 0;
                        foreach (var _ in NadjiPut(v1, k1))
                            yield return _;
                    }
                }
                if (!LavirintResen)
                    Tabla[v, k] = ISTRAZENO;
                yield return 0;
            }
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                    string[] line;
                    line = sr.ReadLine().Split();
                    BrojVrsta = int.Parse(line[0]);
                    BrojKolona = int.Parse(line[1]);
                    Tabla = new int[BrojVrsta, BrojKolona];
                    for (int v = 0; v < BrojVrsta; v++)
                    {
                        line = sr.ReadLine().Split();
                        for (int k = 0; k < BrojKolona; k++)
                        {
                            Tabla[v, k] = int.Parse(line[k]);
                            if (Tabla[v, k] == START)
                            {
                                VrstaStart = v;
                                KolonaStart = k;
                            }
                            if (Tabla[v, k] == CILJ)
                            {
                                VrstaCilj = v;
                                KolonaCilj = k;
                            }
                        }
                    }
                    sr.Close();
                    PostaviDimenziju(BrojVrsta, BrojKolona);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnResi_Click(object sender, EventArgs e)
        {
            LavirintResen = false;
            StringBuilder sb = new StringBuilder();
            foreach (var x in NadjiPut(VrstaStart, KolonaStart))
            {
                sb.Append(x.ToString());
                Invalidate();
                if (Interval > 0)
                    Thread.Sleep(Interval);
                Application.DoEvents();
            }
            if (!LavirintResen)
                MessageBox.Show("Nema resenja");
            Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // Podesavamo brzinu animacije
            int fps = trackBar1.Value;
            Interval = 1000 / fps;
            if (Interval == 0)
                tslBrzina.Text = "Max FPS";
            else
                tslBrzina.Text = fps.ToString() + " FPS";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush cetkaTekucaPutanja = new SolidBrush(Color.Red);
            Brush cetkaStart = new SolidBrush(Color.Green);
            Brush cetka = new SolidBrush(Color.Black);
            Pen olovka = new Pen(Color.Black, 5);
            int r = D / 3;

            for (int v = 0; v < BrojVrsta; v++)
            {
                for (int k = 0; k < BrojKolona; k++)
                {
                    int xc = MARGINA_LEVO + k * D + D / 2; // x centra polja
                    int yc = MARGINA_GORE + v * D + D / 2; // y centra polja
                    Rectangle pr = new Rectangle(MARGINA_LEVO + k * D, MARGINA_GORE + v * D, D, D);
                    g.DrawRectangle(Pens.Black, pr);
                    if (Tabla[v, k] == ZID)
                        g.FillRectangle(cetka, pr);
                    else if (Tabla[v, k] == TEKUCA_PUTANJA)
                    {
                        if (LavirintResen)
                            g.FillEllipse(cetkaStart, xc - r, yc - r, 2 * r, 2 * r);
                        else
                            g.FillEllipse(cetkaTekucaPutanja, xc - r, yc - r, 2 * r, 2 * r);
                    }
                    else if (Tabla[v, k] == ISTRAZENO)
                        g.FillEllipse(cetka, xc - r, yc - r, 2 * r, 2 * r);
                    else if (Tabla[v, k] == START)
                        g.FillEllipse(cetkaStart, xc - r, yc - r, 2 * r, 2 * r);
                    else if (Tabla[v, k] == CILJ)
                    {
                        g.DrawLine(olovka, xc - r, yc - r, xc + r, yc + r);
                        g.DrawLine(olovka, xc - r, yc + r, xc + r, yc - r);
                    }
                }
            }
        }
    }
}
