/*
Pravilan ikosaedar je poliedar čije strane su jednakostranični trouglovi, a ima ih ukupno 20.
Kod svakog temena se sastaje 5 strana (vidi https://en.wikipedia.org/wiki/Regular_icosahedron)

Strane jednog takvog ikosaedra su numerisane brojevima 1-20. Treba obići strane tog ikosaedra tako da:
- svaka strana bude posećena tacno jednom
- uzastopne strane pri obilasku budu susedne na poliedru
- zbir 1*a[1] + 2*a[2] + ... 20*a[20] bude najmanji mogući, pri čemu je a[i] broj ispisan na strani posećenoj u i-tom koraku.

Zadatak ima dve varijante.
Varijanta A: susedne strane su one koje imaju zajedničku ivicu (svaka strana ima 3 suseda)
Varijanta B: susedne strane su one koje imaju zajedničko teme (svaka strana ima 9 suseda)
Susedstvo je zadato matricama SusedstvoA i SusedstvoB na početku programa.

Rešenje bektrekom uz ograničavanje pretrage (engl. branch and bound) i izmenjen redosled spuštanja niz grane pretrage. 
Ovo rešenje je mnooogo efikasnije od prethodnog.
*/
using System;

namespace IkosaedarBB
{
    class Program
    {
        static int[,] SusedstvoA = {
            { 7,13,19}, {12,18,20}, {16,17,19}, {11,14,18}, {13,15,18},
            { 9,14,16}, { 1,15,17}, {10,16,20}, { 6,11,19}, { 8,12,17},
            { 4, 9,13}, { 2,10,15}, { 1, 5,11}, { 4, 6,20}, { 5, 7,12},
            { 3, 8, 6}, { 3, 7,10}, { 2, 4, 5}, { 1, 3, 9}, { 2, 8,14}
        };
        static int[,] SusedstvoB = {
            { 3, 5, 7, 9,11,13,15,17,19}, { 4, 5, 8,10,12,14,15,18,20},
            { 1, 6, 7, 8, 9,10,16,17,19}, { 2, 5, 6, 9,11,13,14,18,20},
            { 1, 2, 4, 7,11,12,13,15,18}, { 3, 4, 8, 9,11,14,16,19,20},
            { 1, 3, 5,10,12,13,15,17,19}, { 2, 3, 6,10,12,14,16,17,20},
            { 1, 3, 4, 6,11,13,14,16,19}, { 2, 3, 7, 8,12,15,16,17,20},
            { 1, 4, 5, 6, 9,13,14,18,19}, { 2, 5, 7, 8,10,15,17,18,20},
            { 1, 4, 5, 7, 9,11,15,18,19}, { 2, 4, 6, 8, 9,11,16,18,20},
            { 1, 2, 5, 7,10,12,13,17,18}, { 3, 6, 8, 9,10,14,17,19,20},
            { 1, 3, 7, 8,10,12,15,16,19}, { 2, 4, 5,11,12,13,14,15,20},
            { 1, 3, 6, 7, 9,11,13,16,17}, { 2, 4, 6, 8,10,12,14,16,18}
        };

        static int[,] Susedstvo = SusedstvoB; // SusedstvoA ili SusedstvoB

        static int[] Resenje = new int[20];
        static int[] NajmanjeResenje = new int[20];
        static bool[] Posecen = new bool[20];
        static int Cena, NajnizaCena;
        static int BrPoziva;

        static void Trazi(int cvor, int dubina)
        {
            BrPoziva++;
            if (!Posecen[cvor])
            {
                Resenje[dubina] = cvor;
                Cena += (dubina + 1) * (cvor + 1);
                if (dubina == 19)
                {
                    if (NajnizaCena > Cena)
                    {
                        NajnizaCena = Cena;
                        for (int k = 0; k < 20; k++)
                            NajmanjeResenje[k] = Resenje[k];
                    }
                }
                else
                {
                    Posecen[cvor] = true;
                    int PreostalaCena = 0, d = dubina + 2;
                    for (int k = 19; k >= 0; k--) if (!Posecen[k]) PreostalaCena += (k + 1) * (d++);
                    if (NajnizaCena > Cena + PreostalaCena)
                    {
                        for (int iSused = Susedstvo.GetLength(1) - 1; iSused >= 0; iSused--)
                        //for (int iSused = 0; iSused < Susedstvo.GetLength(1); iSused++)
                            Trazi(Susedstvo[cvor, iSused] - 1, dubina + 1);
                    }
                    Posecen[cvor] = false;
                }

                Cena = Cena - (dubina + 1) * (cvor + 1);
            }
        }

        static void Main(string[] args)
        {
            Cena = 0;
            NajnizaCena = int.MaxValue;
            BrPoziva = 0;
            for (int i = 0; i < 20; i++) Posecen[i] = false;

            for (int i = 19; i >= 0; i--) Trazi(i, 0);
            //for (int i = 0; i < 20; i++) Trazi(i, 0);

            for (int i = 0; i < 20; i++)
                Console.Write("{0, 3}", NajmanjeResenje[i] + 1);
            Console.WriteLine(" : " + NajnizaCena); // A: 1963, B: 1575
            Console.WriteLine(BrPoziva); // A: 33_212 (93_677), B: 31_808 (1_076_438)
        }
    }
}
