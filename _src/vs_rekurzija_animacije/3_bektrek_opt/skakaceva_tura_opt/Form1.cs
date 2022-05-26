﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;             // Sleep
using System.Linq;
using System.Windows.Forms;

namespace SkakacevaTuraOpt
{
    public partial class Form1 : Form
    {
        struct KvalitetPoteza { public int indeks; public int brNastavaka; }

        const int MarginaGore = 60, MarginaDole = 40, MarginaLevo = 10, MarginaDesno = 10;
        static int BrojVrsta;
        static int BrojKolona;
        static int SirinaPolja;
        static int VisinaPolja;
        static int VelicinaSlova;

        static int PotezBr = 1;
        static int Interval = 1000;
        static bool ProblemResen;
        static int[,] Tabla;
        static int[,] Potezi = { { -1, -2 }, { -1, 2 }, { 1, -2 }, { 1, 2 }, { -2, -1 }, { -2, 1 }, { 2, -1 }, { 2, 1 } };

        Brush cetkaBrojevi = new SolidBrush(Color.Red);
        StringFormat sf = new StringFormat
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center
        };

        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;
            PostaviDimenziju(6, 6);
        }

        private void PostaviDimenziju(int brVrsta, int brKolona)
        {
            if (brVrsta != BrojVrsta || brKolona != BrojKolona)
            {
                BrojVrsta = brVrsta;
                BrojKolona = brKolona;
                Tabla = new int[BrojVrsta, BrojKolona];
            }

            int w = (ClientSize.Width - MarginaLevo - MarginaDesno);
            int h = (ClientSize.Height - MarginaGore - MarginaDole);
            SirinaPolja = w / BrojKolona;
            VisinaPolja = h / BrojVrsta;
            VelicinaSlova = Math.Min(SirinaPolja, VisinaPolja) / 2;
            Invalidate();
        }

        static IEnumerable<int> ObidjiTabluSkakacem(int v, int k)
        {
            Tabla[v, k] = PotezBr;
            yield return 0;

            if (PotezBr == BrojVrsta * BrojKolona)
            {
                ProblemResen = true;
                yield return 0;
            }
            else
            {
                // Odredi kvalitet (broj nastavaka) svakog moguceg poteza
                List<KvalitetPoteza> redosled = new List<KvalitetPoteza>();
                for (int i = 0; i < Potezi.GetLength(0); i++)
                {
                    int br = 0;
                    int vn = v + Potezi[i, 0], kn = k + Potezi[i, 1];
                    if (0 <= vn && vn < BrojVrsta && 0 <= kn && kn < BrojKolona && Tabla[vn, kn] == 0)
                    {
                        for (int iNast = 0; iNast < Potezi.GetLength(0); iNast++)
                        {
                            int vn2 = vn + Potezi[iNast, 0], kn2 = kn + Potezi[iNast, 1];
                            if (0 <= vn2 && vn2 < BrojVrsta && 0 <= kn2 && kn2 < BrojKolona && Tabla[vn2, kn2] == 0)
                                br++; // brojimo jedan nastavak vise
                        }
                    }
                    redosled.Add(new KvalitetPoteza() { indeks = i, brNastavaka = br });
                }
                redosled = redosled.OrderBy(x => x.brNastavaka).ToList();

                for (int i = 0; i < Potezi.GetLength(0) && !ProblemResen; i++)
                {
                    int ii = redosled[i].indeks; // indeks poteza u rastucem redosledu broja mogucih nastavaka
                    int vn = v + Potezi[ii, 0], kn = k + Potezi[ii, 1];
                    if (0 <= vn && vn < BrojVrsta && 0 <= kn && kn < BrojKolona && Tabla[vn, kn] == 0)
                    {
                        PotezBr++;
                        foreach (var _ in ObidjiTabluSkakacem(vn, kn))
                            yield return _;
                        PotezBr--;
                    }
                    yield return 0;
                }

                if (!ProblemResen)
                    Tabla[v, k] = 0;
            }
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            Tabla = new int[BrojVrsta, BrojKolona];
            ProblemResen = false;
            PotezBr = 1;
            foreach (var x in ObidjiTabluSkakacem(0, 0))
            {
                Invalidate();
                if (Interval > 0)
                    Thread.Sleep(Interval);
                Application.DoEvents();
            }
            if (!ProblemResen)
                MessageBox.Show("Nema resenja");
            Invalidate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            PostaviDimenziju(BrojVrsta, BrojKolona);
        }

        private void trackBarBrzina_Scroll(object sender, EventArgs e)
        {
            // Podesavamo brzinu animacije
            int fps = 10 * trackBarBrzina.Value;
            if (fps == 0) fps = 1;
            Interval = 1000 / fps;
            if (Interval == 0)
                tslBrzina.Text = "Max FPS";
            else
                tslBrzina.Text = fps.ToString() + " FPS";
        }

        private void numVelTable_ValueChanged(object sender, EventArgs e)
        {
            PostaviDimenziju((int)numBrojVrsta.Value, (int)numBrojKolona.Value);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Brush[] cetkaPolje = { new SolidBrush(Color.Black), new SolidBrush(Color.White) };
            Font f = new Font("Arial", VelicinaSlova);


            // Crtaj Tablu
            for (int red = 0; red < BrojVrsta; red++)
            {
                for (int kol = 0; kol < BrojKolona; kol++)
                {
                    int x = MarginaLevo + kol * SirinaPolja;
                    int y = MarginaGore + (BrojVrsta - 1 - red) * VisinaPolja;
                    Rectangle pr = new Rectangle(x, y, SirinaPolja, VisinaPolja);
                    g.FillRectangle(cetkaPolje[(red + kol) % 2], pr);
                    if (Tabla[red, kol] > 0)
                    {
                        g.DrawString(Tabla[red, kol].ToString(), f, cetkaBrojevi, pr, sf);
                    }
                }
            }
        }
    }
}
