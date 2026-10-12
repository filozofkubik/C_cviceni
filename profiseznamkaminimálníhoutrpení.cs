namespace institucializované_otroctví
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("napiš počet stvoření lidské rasy ženského pohlaví");
            int a = Convert.ToInt32(Console.ReadLine());




            int[,] preferencežen = new int[a, a];



            int[,] preferencemužů = new int[a, a];
            for (int i = 0; i < a; i++)

            {
                Console.WriteLine($"ženo {i + 1} seřad ted budeš řadit muže dle jejich krásy muži jsou označeny čísly od jedné do n kdy n je počet mužů, jako první napiš muže nejpohlednějšího jako n tého lidskou trosku ležící u COV");
                for (int j = 0; j < a; j++)
                {




                    Console.Write($"Napiš {j + 1}. muže tvého srdce ");
                    int vyska = Convert.ToInt32(Console.ReadLine());




                    preferencežen[i, j] = vyska;
                }
            }
            for (int i = 0; i < a; i++)
            {
                Console.WriteLine($"muži {i + 1} seřad ted budeš řadit ženy dle jejich krásy ženy jsou označeny čísly od jedné do n kdy n je počet žen, jako první napiš ženu nejpohlednější jako n tou lidskou trosku ležící u mekáče");
                for (int j = 0; j < a; j++)
                {




                    Console.Write($"Napiš {j + 1}. ženu tvého srdce ");



                    int sirka = Convert.ToInt32(Console.ReadLine());
                    preferencemužů[i, j] = sirka;
                }
            }

            int[] mužpartnerka = new int[a];





            int[] ženapartner = new int[a];


            int[] dalšívmilovanémpořadí = new int[a];




            for (int i = 0; i < a; i++)
            {
                mužpartnerka[i] = -1;


                ženapartner[i] = -1;




                dalšívmilovanémpořadí[i] = 0;
            }
            int pocetsvobodnychmužu = a;






            while (pocetsvobodnychmužu > 0)
            {
                int m = -1;

                
                for (int i = 0; i < a; i++)
                {
                    if (mužpartnerka[i] == -1)
                    {
                        m = i;
                        break;
                    }
                }
                int zenacislo = preferencemužů[m, dalšívmilovanémpořadí[m]]-1;
                dalšívmilovanémpořadí[m] = dalšívmilovanémpořadí[m] + 1;
                if (ženapartner[zenacislo] == -1) {
                    ženapartner[zenacislo] = m;
                    mužpartnerka[m] = zenacislo;
                    pocetsvobodnychmužu = pocetsvobodnychmužu - 1;

                }
                else
                {
                    int puvodnímilenec = ženapartner[zenacislo];
                    int novýobjekttouhy = -1;
                    int pořadístarehomilence = -1;


                    for (int j = 0; j < a; j++)
                    {
                        if (preferencežen[zenacislo, j] - 1 == m) novýobjekttouhy = j;








                        if (preferencežen[zenacislo, j] - 1 == puvodnímilenec) pořadístarehomilence = j;
                    }


                    if (novýobjekttouhy < pořadístarehomilence)
                    {
                        
                        ženapartner[zenacislo] = m;
                        mužpartnerka[m] = zenacislo;

                        
                        mužpartnerka[puvodnímilenec] = -1;
                    }
                }
            
            }
            for (int i = 0; i < a; i++)
            {
                Console.WriteLine($"Z{i + 1}-M{ženapartner[i] + 1}");
            }
        }
}
}