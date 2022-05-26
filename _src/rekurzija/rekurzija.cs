using System;
using System.Collections.Generic;

class Program
{
    // 1. Definiši rekurzivnu funkciju koja izračunava zbir elemenata niza a
    // a[0] + a[1] + ... + a[n-2] + a[n-1]
    static int Zbir(int[] a, int i)
    {
	if (i == a.Length)
	    return 0;
	return Zbir(a, i+1) + a[i];
    }

    static int Zbir(int[] a)
    {
	return Zbir(a, 0);
    }

    // 2. Definiši rekurzivnu funkciju koja izračunava zbir kvadrata elemenata niza a
    static int ZbirKvadrata(int[] a, int n)
    {
	if (n == 0)
	    return 0;
	return ZbirKvadrata(a, n-1) + a[n-1]*a[n-1];
    }
    
    static int ZbirKvadrata(int[] a)
    {
	return ZbirKvadrata(a, a.Length);
    }

    // ZbirKvadrata({1, 2, 3}) = ZbirKvadrata({1, 2, 3}, 3) = ZbirKvadrata({1, 2, 3}, 2) + 9 =
    // (ZbirKvadrata({1, 2, 3}, 1) + 4) + 9 = ((ZbirKvadrata({1, 2, 3}, 0) + 1) + 4) + 9 =
    // ((0 + 1) + 4) + 9 = 14

    // 3. Definiši rekurzivnu funkciju koja određuje najmanji element nepraznog niza a
    static int Min(int[] a, int n)
    {
	if (n == 1)
	    return a[0];
	return Math.Min(Min(a, n-1), a[n-1]);
    }

    static int Min(int[] a)
    {
	return Min(a, a.Length);
    }    

    // 4. Definiši rekurzivnu funkciju koja proverava da li dati niz sadrži dati broj
    static bool SadrziParni(int[] a, int n)
    {
	if (n == 0)
	    return false;
	return SadrziParni(a, n-1) || a[n-1] % 2 == 0;
    }
    
    static bool SadrziParni(int[] a)
    {
	return SadrziParni(a, a.Length);
    }
    
    // 5. Definiši rekurzivnu funkciju koja izračunava broj parnih elemenata niza
    static int BrojParnih(int[] a, int n)
    {
	if (n == 0)
	    return 0;
	return BrojParnih(a, n-1) + (a[n-1] % 2 == 0 ? 1 : 0);
    }
    
    static int BrojParnih(int[] a)
    {
	return BrojParnih(a, a.Length);
    }

    // 6. Definiši rekurzivnu funkciju koja izdvaja sve parne elemente liste u novu listu
    static List<int> IzdvojParne(List<int> a, int n)
    {
	if (n == 0)
	    return new List<int>();
	List<int> parni = IzdvojParne(a, n-1);
	if (a[n-1] % 2 == 0)
	    parni.Add(a[n-1]);
	return parni;
    }

    
    static List<int> IzdvojParne(List<int> a)
    {
	return IzdvojParne(a, a.Count);
    }

    // 7. Definiši rekurzivnu funkciju koja obrće dati niz brojeva
    static void Obrni(int[] a, int i, int j)
    {
	if (i < j)
	{
	    int tmp = a[i];
	    a[i] = a[j];
	    a[j] = tmp;
	    Obrni(a, i+1, j-1);
	}
    }
    
    static void Obrni(int[] a)
    {
	Obrni(a, 0, a.Length - 1);
    }

    // 8. Definiši rekurzivnu funkciju koja istovremeno određuje najmanji i najveći element niza
    static void MinMax(int[] a, int n, out int min, out int max)
    {
	if (n == 1)
	{
	    min = max = a[0];
	}
	else
	{
	    MinMax(a, n-1, out min, out max);
	    if (a[n-1] < min)
		min = a[n-1];
	    else if (a[n-1] > max)
		max = a[n-1];
	}
    }
    
    static void MinMax(int[] a, out int min, out int max)
    {
	MinMax(a, a.Length, out min, out max);
    }

    static void Main()
    {
	int[] a = {5, 1, 6, 3, 4, 7, 8};
	int min, max;
	MinMax(a, out min, out max);
	Console.WriteLine(min);
	Console.WriteLine(max);
    }
}
