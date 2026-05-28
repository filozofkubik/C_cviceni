using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {

        int[] cisla = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);


        int cil = int.Parse(Console.ReadLine());


        Dictionary<int, int> mapa = new Dictionary<int, int>();

        

        for (int i = 0; i < cisla.Length; i++)
        {
            int doplnek = cil - cisla[i];


            if (mapa.ContainsKey(doplnek))
            {
                Console.WriteLine($"{mapa[doplnek]} {i}");
                return;
            }


            mapa[cisla[i]] = i;
        }


        Console.WriteLine("-1");
    }
}