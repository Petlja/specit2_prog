using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;             // Sleep
using System.Windows.Forms;

namespace Trimino
{
    public partial class Form1 : Form
    {
        const int MarginaGore = 60, MarginaDole = 40, MarginaLevo = 10, MarginaDesno = 10;
        static int SirinaPolja;
        static int VisinaPolja;

        static int Interval = 1000;
        static int[,] Tabla;
        static int VelicinaTable;
        static int VrstaX;
        static int KolonaX;

        static List<Brush> Cetke = new List<Brush>();
        static Random Rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;
            PostaviDimenziju(8);
            NoviZadatak();
        }

        private void PostaviDimenziju(int n)
        {
            if (n != VelicinaTable)
            {
                VelicinaTable = n;
                Tabla = new int[VelicinaTable, VelicinaTable];
                NoviZadatak();
            }

            int w = (ClientSize.Width - MarginaLevo - MarginaDesno);
            int h = (ClientSize.Height - MarginaGore - MarginaDole);
            SirinaPolja = w / VelicinaTable;
            VisinaPolja = h / VelicinaTable;
            Invalidate();
        }

        private void NoviZadatak()
        {
            Cetke.Clear();
            Cetke.Add(new SolidBrush(Color.White));
            Cetke.Add(new SolidBrush(Color.Black));

            int v, k;
            for (v = 0; v < VelicinaTable; v++)
                for (k = 0; k < VelicinaTable; k++)
                    Tabla[v, k] = 0;

            VrstaX = Rnd.Next(VelicinaTable);
            KolonaX = Rnd.Next(VelicinaTable);
            Tabla[VrstaX, KolonaX] = 1;
            Invalidate();
        }

        static IEnumerable<int> PopuniTablu(int n, int vStart, int kStart, int vX, int kX)
        {
            if (n > 1)
            {
                bool[,] trebaObojiti = { { true, true }, { true, true } };
                int[,] vxSledece = { { vX, vX }, { vX, vX } };
                int[,] kxSledece = { { kX, kX }, { kX, kX } };

                n = n / 2;
                int dv = (vX - vStart) / n;
                int dk = (kX - kStart) / n;
                trebaObojiti[dv, dk] = false;

                int novaBoja = Cetke.Count;
                Cetke.Add(new SolidBrush(Color.FromArgb(Rnd.Next(256), Rnd.Next(256), Rnd.Next(256))));

                for (dv = 0; dv < 2; dv++)
                    for (dk = 0; dk < 2; dk++)
                        if (trebaObojiti[dv, dk])
                        {
                            vxSledece[dv, dk] = vStart + n - 1 + dv;
                            kxSledece[dv, dk] = kStart + n - 1 + dk;
                            Tabla[vxSledece[dv, dk], kxSledece[dv, dk]] = novaBoja;
                        }
                yield return 0;

                for (dv = 0; dv < 2; dv++)
                    for (dk = 0; dk < 2; dk++)
                        foreach (var x in PopuniTablu(n, vStart + n * dv, kStart + n * dk,
                            vxSledece[dv, dk], kxSledece[dv, dk]))
                        {
                            yield return x;
                        }
            }
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            PostaviDimenziju(VelicinaTable);
        }

        private void btnNova_Click(object sender, EventArgs e)
        {
            NoviZadatak();
        }

        private void btnResi_Click(object sender, EventArgs e)
        {
            foreach (var x in PopuniTablu(VelicinaTable, 0, 0, VrstaX, KolonaX))
            {
                Invalidate();
                if (Interval > 0)
                    Thread.Sleep(Interval);
                Application.DoEvents();
            }
            Invalidate();
        }

        private void trackBarBrzina_Scroll(object sender, EventArgs e)
        {
            // Podesavamo brzinu animacije
            int fps = trackBarBrzina.Value;
            Interval = 1000 / fps;
            tslBrzina.Text = fps.ToString() + " FPS";
        }

        private void numVelTable_ValueChanged(object sender, EventArgs e)
        {
            PostaviDimenziju(1 << (int)numBrojVrsta.Value);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Crtaj resetku
            Pen olovka = new Pen(Color.Black, 1);
            for (int i = 0; i <= VelicinaTable; i++)
            {
                g.DrawLine(olovka, 
                    MarginaLevo, MarginaGore + i * VisinaPolja, 
                    MarginaLevo + VelicinaTable * SirinaPolja, MarginaGore + i * VisinaPolja);
                g.DrawLine(olovka, 
                    MarginaLevo + i * SirinaPolja, MarginaGore, 
                    MarginaLevo + i * SirinaPolja, MarginaGore + VelicinaTable * VisinaPolja);
            }

            // Crtaj polja table
            for (int red = 0; red < VelicinaTable; red++)
            {
                for (int kol = 0; kol < VelicinaTable; kol++)
                {
                    int x = MarginaLevo + kol * SirinaPolja;
                    int y = MarginaGore + red * VisinaPolja;
                    Rectangle pr = new Rectangle(x + 2, y + 2, SirinaPolja - 4, VisinaPolja - 4);
                    g.FillRectangle(Cetke[Tabla[red,kol]], pr);
                }
            }
        }
    }
}
