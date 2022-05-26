using System;
using System.Collections.Generic;   // IEnumerable
using System.Drawing;
using System.Text;                  // StringBuilder
using System.Threading;             // Sleep
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        const int D = 60;
        const int MarginaGore = 60, MarginaDole = 40, MarginaLevo = 10, MarginaDesno = 10;
        static int N = 2;
        static int[,] Tabla = new int[N * N, N * N];
        static int[,] PocetnaTabla = new int[N * N, N * N];
        static bool SudokuResen;
        static int Interval = 10;

        public Form1()
        {
            InitializeComponent();
            tslBrzina.Text = trackBar1.Value.ToString() + " FPS";
            PostaviN(2);
        }

        void PostaviN(int n)
        {
            N = n;
            Tabla = new int[N * N, N * N];
            PocetnaTabla = new int[N * N, N * N];
            ClientSize = new Size(
                N * N * D + MarginaLevo + MarginaDesno, 
                N * N * D + MarginaGore + MarginaDole);
        }

        static bool Konflikt(int[,] A, int i, int j)
        {
            // da li se A[i, j] već nalazi u koloni j
            for (int k = 0; k < N * N; k++)
                if (k != i && A[i, j] == A[k, j])
                    return true;

            // da li se A[i, j] već nalazi u vrsti i
            for (int k = 0; k < N * N; k++)
                if (k != j && A[i, j] == A[i, k])
                    return true;

            // da li se A[i, j] nalazi već u kvadratu koji sadrži polje (i, j)
            int x = i / N, y = j / N;
            for (int k = x * N; k < (x + 1) * N; k++)
                for (int l = y * N; l < (y + 1) * N; l++)
                    if (k != i && l != j && A[i, j] == A[k, l])
                        return true;

            // ne postoji konflikt
            return false;
        }

        IEnumerable<string> Resi(int[,] A, int rbr)
        {
            // ako je u pitanju poslednje polje, uspešno smo popunili ceo sudoku
            if (rbr == N * N * N * N)
                SudokuResen = true;
            else
            {
                int i = rbr / (N * N), j = rbr % (N * N);
                // ako je polje (i, j) već popunjeno
                if (A[i, j] != 0)
                {
                    // rekurzivno nastavljamo sa popunjavanjem
                    foreach (var x in Resi(A, rbr + 1))
                        yield return x;
                }
                else
                {
                    // upisujemo svaku moguću vrednost na polje (i, j)
                    for (int k = 1; k <= N * N && !SudokuResen; k++)
                    {
                        // upisujemo vrednost k
                        A[i, j] = k;
                        yield return string.Format("{0}{1}{2} ", i, j, k);
                        // ako time nije napravljen neki konflikt, nastavljamo popunjavanje
                        // ako se sudoku uspešno popuni, prekidamo dalju pretragu
                        if (!Konflikt(A, i, j))
                        {
                            foreach (var x in Resi(A, rbr + 1))
                                yield return x;
                        }
                    }
                    // poništavamo vrednost upisanu na polje (i, j)
                    if (!SudokuResen)
                        A[i, j] = 0;
                }
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
                    while ((line = file.ReadLine()) != null)
                    {
                        if (red == 0)
                            PostaviN((int)(0.5 + Math.Sqrt(line.Length)));

                        for (int kol = 0; kol < N * N; kol++)
                            PocetnaTabla[red, kol] = Tabla[red, kol] = line[kol] - '0';

                        red++;
                    }
                    file.Close();
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
            StringBuilder sb = new StringBuilder();
            foreach (var x in Resi(Tabla, 0))
            {
                sb.Append(x.ToString());
                Invalidate();
                if (Interval > 0)
                    Thread.Sleep(Interval);
                Application.DoEvents();
            }
            //MessageBox.Show(sb.ToString());
            if (!SudokuResen)
                MessageBox.Show("Nema resenja");
            Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // Podesavamo brzinu animacije
            int fps = 10 * trackBar1.Value;
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
            Font f = new Font("Arial", 30);
            Brush cetkaPocetni = new SolidBrush(Color.Red);
            Brush cetka = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            for (int red = 0; red < N * N; red++)
                for (int kol = 0; kol < N * N; kol++)
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
