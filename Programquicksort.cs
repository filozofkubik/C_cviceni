using System;
using System.Collections.Generic;           // ← přidáno (jen 1 řádek)
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        int[] vstup = { 1, 2, 3, 4, 75, 6, 7, 88, 9, 10, 11, 12, 13, 44, 15, 16, 1, 28, 59, 20, 21, 82, 23, 24, 25, 26, 87, 28, 29, 30, 31, 52 };

        int[] vysledek = new Program().algoritmus(vstup);   // ← příklad volání

        Console.WriteLine(string.Join(", ", vysledek));
    }

    public int[] algoritmus(int[] vstup)        // ← změněno: int → int[]
    {
        int delka = vstup.Length;
        if (delka <= 1)
        {                      

            int[] vystup = vstup;
            return vystup;
        }
        int p = vstup[0];

        List<int> mensi = new List<int>();
        List<int> stejny = new List<int>();
        List<int> vetsi = new List<int>();

        for (int i = 0; i < vstup.Length; i++)
        {
            if (vstup[i] == p)
            {

                stejny.Add(vstup[i]);
            }
            if (vstup[i] < p)
            {

                mensi.Add(vstup[i]);
            }
            if (vstup[i] > p)
            {

                vetsi.Add(vstup[i]);
            }
        }
        mensi = new List<int>(algoritmus(mensi.ToArray()));  
        vetsi = new List<int>(algoritmus(vetsi.ToArray()));  

        List<int> zpet = new List<int>();

        zpet.AddRange(mensi);      
        zpet.AddRange(stejny);     
        zpet.AddRange(vetsi);      

        return zpet.ToArray();
    }
}



//1. bonus medián hledáme tak že to celý rozdělíme na skupiny po pěti prvcích v těchto skupinách najdeme medíán a to děláme do kola dokud nenajdeme konečný medián
//2.bonus 68,75 % pro skoromedián  0% pro minimum pokud nejsou dvojité prvky
//3.bonus pokaždé když dělíte na dvě skupiny vyberete tu menší zbytek ukládáme a zpětně se k ní vracíme
//4.bonus pokud je tam například jena hodnota uplně mimo(1,2,3,5,100000000) medián 3 puměr hodně(20 000 002)