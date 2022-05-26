using System;

class Program
{
    // popunjava se kvadrat dimenzije dim cije je gornje levo teme na
    // poziciji (v0, k0) i kome je polje na poziciji (v, k) vec popunjeno
    // broj je redni broj narednog trimina koji se moze postaviti koji
    // se tokom popunjavanja uvecava
    static void Trimini(int[,] tabla, int dim, int v0, int k0,
                        int v, int k, ref int broj)
    {
        // ako je dimenzija kvadrata 1x1 u kome je jedno polje popunjeno,
        // tada je ceo kvadrat vec popunjen
        if (dim > 1)
        {
            // kordinate 4 polja oko sredista kvadrata
            int vsred1 = v0 + dim/2 - 1;
            int vsred2 = v0 + dim/2;
            int ksred1 = k0 + dim/2 - 1;
            int ksred2 = k0 + dim/2;

            // odredjujemo kvadrant kome pripada postojeca rupa
            bool rupaGoreLevo = v <= vsred1 && k <= ksred1;
            bool rupaDoleLevo = v >= vsred2 && k <= ksred1;
            bool rupaGoreDesno = v <= vsred1 && k >= ksred2;
            bool rupaDoleDesno = v >= vsred2 && k >= ksred2;

            // postavljamo trimino u sredisna polja kvadrata
            if (!rupaGoreLevo)
                tabla[vsred1, ksred1] = broj;
            if (!rupaDoleLevo)
                tabla[vsred2, ksred1] = broj;
            if (!rupaGoreDesno)
                tabla[vsred1, ksred2] = broj;
            if (!rupaDoleDesno)
                tabla[vsred2, ksred2] = broj;
            broj++;

            // rekurzivno popunjavamo 4 manja kvadrata

            // uvek ispitujemo da li je u tom kvadrantu stara rupa ili
            // je nastala postavljanjem novog trimina

            // kvadrat gore levo - gornje levo teme mu je na (v0, k0)
            // a nova rupa mu je u njegovom donjem desnom temenu na (vsred1, ksred1)
            if (rupaGoreLevo)
                Trimini(tabla, dim/2, v0, k0, v, k, ref broj);
            else
                Trimini(tabla, dim/2, v0, k0, vsred1, ksred1, ref broj);

            // kvadrat dole levo - gornje levo teme mu je na (vsred2, k0)
            // a nova rupa mu je u njegovom gornjem desnom temenu na (vsred2, ksred1)
            if (rupaDoleLevo)
                Trimini(tabla, dim/2, vsred2, k0, v, k, ref broj);
            else
                Trimini(tabla, dim/2, vsred2, k0, vsred2, ksred1, ref broj);

            // kvadrat gore desno - gornje levo teme mu je na (v0, ksred2)
            // a nova rupa mu je u njegovom donjem levom temenu na (vsred1, ksred2)
            if (rupaGoreDesno)
                Trimini(tabla, dim/2, v0, ksred2, v, k, ref broj);
            else
                Trimini(tabla, dim/2, v0, ksred2, vsred1, ksred2, ref broj);

            // kvadrat dole desno - gornje levo teme mu je na (vsred2, ksred2)
            // i nova rupa mu je u tom gornjem levom temenu na (vsred2, ksred2)
            if (rupaDoleDesno)
                Trimini(tabla, dim/2, vsred2, ksred2, v, k, ref broj);
            else
                Trimini(tabla, dim/2, vsred2, ksred2, vsred2, ksred2, ref broj);
        }
    }

    // triminima popunjavamo tablu dimenzije dim kome nedostaje polje
    // (v, k)
    static int[,] Trimini(int dim, int v, int k)
    {
        // alociramo matricu u koju cemo upisivati rezultat
        int[,] tabla = new int[dim, dim];
        // popunjavamo pocetno polje
        int broj = 0;
        tabla[v, k] = broj++;
        // rekurzivnom funkcijom popunjavamo ostatak table
        Trimini(tabla, dim, 0, 0, v, k, ref broj);
        return tabla;
    }
    
    static void Main()
    {
        // dimenzija table
        int dim = 8;
        // nasumicno odredjujemo polje koje je inicijalno popunjeno
        Random rnd = new Random();
        int v = rnd.Next(0, dim);
        int k = rnd.Next(0, dim);
        // popunjavamo tablu triminima 
        int[,] tabla = Trimini(dim, v, k);
        // ispisujemo dobijeni rezultat
        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
                Console.Write((char)('a' + tabla[i, j]));
            Console.WriteLine();
        }
    }
}
