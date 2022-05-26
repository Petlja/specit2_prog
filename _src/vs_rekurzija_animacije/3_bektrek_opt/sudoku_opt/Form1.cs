// klizacen se podesava brzina animacije
// na F1 se ukljucuje/iskljucije prikazivanje mogucih poteza
using System;
using System.Collections.Generic;   // IEnumerable
using System.Drawing;
using System.Threading;             // Sleep
using System.Windows.Forms;

namespace SudokuOpt
{
    public partial class Form1 : Form
    {
        int D = 50;
        const int MarginaGore = 60, MarginaDole = 40, MarginaLevo = 10, MarginaDesno = 10;
        const int N_MAX = 4;
        static int N = 2;
        static int Interval = 1000;
        static int[] BrojBita = new int[1 << (N_MAX * N_MAX + 1)];
        static int[] Bit = new int[1 << (N_MAX * N_MAX + 1)];
        int VelicinaSlova = 30;
        bool Pomoc = false;

        // Stanje pretrage
        static int[,] Tabla = new int[N * N, N * N];
        static int[,] PocetnaTabla = new int[N * N, N * N];
        static int[,] MoguceRedovi = new int[N * N * N * N + 1, N * N];
        static int[,] MoguceKolone = new int[N * N * N * N + 1, N * N];
        static int[,,] MoguceKvadrati = new int[N * N * N * N + 1, N, N];
        static int BrPunihPolja = 0;
        static bool SudokuResen;

        public Form1()
        {
            InitializeComponent();

            int fps = 10 * trackBar1.Value;
            if (fps == 0) fps = 1;
            tslBrzina.Text = fps.ToString() + " FPS";

            PostaviDimenziju(2);

            BrojBita[0] = 0;
            for (int i = 1; i < (1 << (N_MAX * N_MAX + 1)); i++)
                BrojBita[i] = BrojBita[i >> 1] + (i & 1);
            for (int i = 0; i < (N_MAX * N_MAX + 1); i++)
                Bit[1 << i] = i;
        }

        void PostaviDimenziju(int n)
        {
            N = n;
            Tabla = new int[N * N, N * N];
            PocetnaTabla = new int[N * N, N * N];
            MoguceRedovi = new int[N * N * N * N + 1, N * N];
            MoguceKolone = new int[N * N * N * N + 1, N * N];
            MoguceKvadrati = new int[N * N * N * N + 1, N, N];

            if (N == 2) { D = 100; VelicinaSlova = 35; }
            else if (N == 3) { D = 60; VelicinaSlova = 30; }
            else if (N == 4) { D = 40; VelicinaSlova = 20; }
            ClientSize = new Size(
                N * N * D + MarginaLevo + MarginaDesno,
                N * N * D + MarginaGore + MarginaDole);
        }

        static void AzurirajMoguce(int red, int kol)
        {
            int maska = 1 << Tabla[red, kol];
            MoguceRedovi[BrPunihPolja, red] &= ~maska;
            MoguceKolone[BrPunihPolja, kol] &= ~maska;
            MoguceKvadrati[BrPunihPolja, red / N, kol / N] &= ~maska;
        }

        static bool Dozvoljeno(int x, int red, int kol)
        {
            return
                (MoguceRedovi[BrPunihPolja, red] & (1 << x)) > 0 &&
                (MoguceKolone[BrPunihPolja, kol] & (1 << x)) > 0 &&
                (MoguceKvadrati[BrPunihPolja, red / N, kol / N] & (1 << x)) > 0;
        }

        IEnumerable<int> Resi()
        {
            // ako je u pitanju poslednje polje, uspešno smo popunili ceo sudoku
            if (BrPunihPolja == N * N * N * N)
                SudokuResen = true;
            else
            {
                // Formiramo listu praznih polja (sa brojem mogucih poteza za svako polje)
                List<Tuple<int, int, int>> PraznaPolja = new List<Tuple<int, int, int>>();
                for (int red = 0; red < N * N; red++)
                {
                    for (int kol = 0; kol < N * N; kol++)
                    {
                        if (Tabla[red, kol] == 0)
                        {
                            int m = MoguceRedovi[BrPunihPolja, red] &
                                MoguceKolone[BrPunihPolja, kol] &
                                MoguceKvadrati[BrPunihPolja, red / N, kol / N];
                            PraznaPolja.Add(new Tuple<int, int, int>(BrojBita[m], red, kol));
                        }
                    }
                }
                PraznaPolja.Sort(); // sortiramo listu po broju mogucih poteza na polju
                if (PraznaPolja.Count > 0 && PraznaPolja[0].Item1 > 0)
                {
                    // Isprobavamo prvo poteze na poljima sa manje mogucnosti
                    for (int pp = 0; pp < PraznaPolja.Count; pp++)
                    {
                        int red = PraznaPolja[pp].Item2;
                        int kol = PraznaPolja[pp].Item3;
                        for (int x = 1; x <= N * N; x++)
                        {
                            if (Dozvoljeno(x, red, kol) && !SudokuResen)
                            {
                                BrPunihPolja++;
                                Tabla[red, kol] = x;
                                for (int r = 0; r < N * N; r++)
                                    MoguceRedovi[BrPunihPolja, r] = MoguceRedovi[BrPunihPolja - 1, r];
                                for (int k = 0; k < N * N; k++)
                                    MoguceKolone[BrPunihPolja, k] = MoguceKolone[BrPunihPolja - 1, k];
                                for (int r = 0; r < N; r++)
                                    for (int k = 0; k < N; k++)
                                        MoguceKvadrati[BrPunihPolja, r, k] = MoguceKvadrati[BrPunihPolja - 1, r, k];

                                AzurirajMoguce(red, kol);
                                yield return 0;
                                foreach (var _ in Resi())
                                    yield return _;
                                if (!SudokuResen)
                                    BrPunihPolja--;
                            }
                        }
                        if (!SudokuResen)
                            Tabla[red, kol] = 0;
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Pomoc = !Pomoc;
                Invalidate();
            }
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog1.FileName);
                    string line;
                    int red = 0;
                    BrPunihPolja = 0;
                    while ((line = file.ReadLine()) != null)
                    {
                        if (red == 0)
                            PostaviDimenziju((int)(0.5 + Math.Sqrt(line.Length)));

                        for (int kol = 0; kol < N * N; kol++)
                        {
                            int x = (line[kol] <= '9') ? line[kol] - '0' : line[kol] - 'A' + 10;
                            PocetnaTabla[red, kol] = x;
                            Tabla[red, kol] = x;
                            BrPunihPolja += (x > 0) ? 1 : 0;
                        }
                        red++;
                    }
                    file.Close();

                    for (int r = 0; r < N * N; r++)
                        MoguceRedovi[BrPunihPolja, r] = (1 << (N * N + 1)) - 2;
                    for (int k = 0; k < N * N; k++)
                        MoguceKolone[BrPunihPolja, k] = (1 << (N * N + 1)) - 2;
                    for (int r = 0; r < N; r++)
                        for (int k = 0; k < N; k++)
                            MoguceKvadrati[BrPunihPolja, r, k] = (1 << (N * N + 1)) - 2;

                    for (red = 0; red < N * N; red++)
                    {
                        for (int kol = 0; kol < N * N; kol++)
                        {
                            if (Tabla[red, kol] > 0)
                                AzurirajMoguce(red, kol);
                        }
                    }
                    Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnResi_Click(object sender, EventArgs e)
        {
            SudokuResen = false;
            btnUcitaj.Enabled = false;
            foreach (var x in Resi())
            {
                Invalidate();
                if (Interval > 0)
                    Thread.Sleep(Interval);
                Application.DoEvents();
            }
            if (!SudokuResen)
                MessageBox.Show("Nema resenja");
            btnUcitaj.Enabled = true;
            Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // Podesavamo brzinu animacije
            int fps = 10 * trackBar1.Value;
            if (fps == 0) fps = 1;
            Interval = 1000 / fps;
            if (Interval == 0)
                tslBrzina.Text = "Max FPS";
            else
                tslBrzina.Text = fps.ToString() + " FPS";
        }

        private void CrtajResetku(Graphics g, Pen olovka, int brPolja, int d)
        {
            for (int i = 0; i <= brPolja; i++)
            {
                g.DrawLine(olovka, MarginaLevo, MarginaGore + i * d, MarginaLevo + brPolja * d, MarginaGore + i * d);
                g.DrawLine(olovka, MarginaLevo + i * d, MarginaGore, MarginaLevo + i * d, MarginaGore + brPolja * d);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // crtaj tablu
            Pen debljaOlovka = new Pen(Color.Black, 3);
            Pen tanjaOlovka = new Pen(Color.Black, 1);
            CrtajResetku(e.Graphics, tanjaOlovka, N * N, D);
            CrtajResetku(e.Graphics, debljaOlovka, N, N * D);

            // crtaj brojeve
            Font f = new Font("Arial", VelicinaSlova);
            Font mf = new Font("Arial", 10);
            Brush cetkaPocetni = new SolidBrush(Color.Red);
            Brush cetka = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            for (int red = 0; red < N * N; red++)
                for (int kol = 0; kol < N * N; kol++)
                {
                    if (Pomoc)
                    {
                        int m = MoguceRedovi[BrPunihPolja, red] &
                            MoguceKolone[BrPunihPolja, kol] &
                            MoguceKvadrati[BrPunihPolja, red / N, kol / N];
                        if (Tabla[red, kol] > 0) m = 0;
                        string potezi = "";
                        for (int x = 1; x <= N * N; x++)
                            if ((m & (1 << x)) > 0) potezi += x.ToString();

                        e.Graphics.DrawString(potezi, mf, cetka, MarginaLevo + kol * D, MarginaGore + red * D);
                    }

                    if (PocetnaTabla[red, kol] > 0)
                    {
                        Rectangle pr = new Rectangle(MarginaLevo + kol * D, MarginaGore + red * D, D, D);
                        e.Graphics.DrawString(Tabla[red, kol].ToString(), f, cetkaPocetni, pr, sf);
                    }
                    else if (Tabla[red, kol] > 0)
                    {
                        Rectangle pr = new Rectangle(MarginaLevo + kol * D, MarginaGore + red * D, D, D);
                        e.Graphics.DrawString(Tabla[red, kol].ToString(), f, cetka, pr, sf);
                    }
                }
        }
    }
}
