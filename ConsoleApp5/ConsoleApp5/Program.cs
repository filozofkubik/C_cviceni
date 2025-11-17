namespace řetízkový_graf
{
    internal class Program
    {

        static void Main(string[] args)
        {
            int pocetLidi = Convert.ToInt32(Console.ReadLine());
            int[,] graf = new int[pocetLidi, pocetLidi];
            string vstupniData = Console.ReadLine();
            string[] dvojice = vstupniData.Split();




            double[,] matrix = new double[pocetLidi + 1, pocetLidi + 1];

            for (int i = 0; i < dvojice.Length; i++)
            {
                string[] par = dvojice[i].Split('-');
                int a = Convert.ToInt32(par[0]);
                int b = Convert.ToInt32(par[1]);
                matrix[a, b] = 1;
                matrix[b, a] = 1;

            }
        }

    }
}

    }
}
