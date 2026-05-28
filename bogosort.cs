using System;

class Program
{
    static void Main()
    {
        int[] pole = { 3, 1, 4, 2 };
        Random nahodne = new Random();
        while (true)
        {
            bool serazeno = true;




            for (int i = 0; i < pole.Length - 1; i++)

            {
                if (pole[i] > pole[i + 1]) serazeno = false;}

            if (serazeno) break; 

            for (int i = 0; i < pole.Length; i++)
            {
                int j = nahodne.Next(pole.Length);
                int k = pole[i];

                pole[i] = pole[j];

                pole[j] = k;
            }}


        Console.WriteLine(string.Join(" ", pole));
    }}