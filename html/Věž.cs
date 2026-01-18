namespace ConsoleApp10
{
    internal class Program
    {
        const int POCET_RAD = 8;
        const int SeDADLA_NA_RADU = 8;
        const int CENA = 180;
        const int VIP_CENA = 70;
        const int VIP_OD_RADY = 7;
        const int VIP_DO_RADY = 8;
        const char VOLNY = 'O';
        const char OBSAZENO = 'X';
        const char KRAL = 'K';
        const char CIL = 'C';
        const char POTENCIAL = 'P';
        static void Main()
        {
            char[,] kino = Vytvorkino();
            Console.Write("Kolik překážek:");
            int cislo = int.Parse(Console.ReadLine());
            for (int i = 0; i < cislo; i++)
            {
                Rezervace(kino, OBSAZENO);
            }
            Console.Write("Zacatek krale:");
            Rezervace(kino, KRAL);
            Console.Write("CIL cesty vyber:");
            Rezervace(kino, CIL);
            ZobrazKino(kino);
            int kroky = 0;
            while (true)
            {
                NajdiPotencial(kino);
                kroky++;
                while (true)
                {
                    if (NajdiKrale(kino, out int Krada, out int Ksedlo))
                    {
                        int[] dxp = { 0, 1, 2, 3, 4, 5, 6, 7 };
                        int[] dyp = { 1, 2, 3, 4, 5, 6, 7 };
                        int[] dxm = { -1, -2, -3, -4, -5, -6, -7 };
                        int[] dym = { -1, -2, -3, -4, -5, -6, -7 };
                        for (int i = 0; i < dxp.Length; i++)
                        {

                            int novaRada = Krada + dxp[i];
                            int noveSedlo = Ksedlo;
                            if (dxp[i] == 0)
                            {
                                kino[novaRada, noveSedlo] = OBSAZENO;
                            }

                            if (novaRada >= 0 && novaRada < POCET_RAD &&
                            noveSedlo >= 0 && noveSedlo < SeDADLA_NA_RADU)
                                if (kino[novaRada, noveSedlo] == VOLNY)
                                { kino[novaRada, noveSedlo] = POTENCIAL; }
                                else if (kino[novaRada, noveSedlo] == CIL)
                                {
                                    Console.WriteLine("ANO NALEZENO!");
                                    Console.WriteLine($"takhle dlouho ({kroky}tah/y)to tlustochovi trvalo");
                                    ZobrazKino(kino);
                                    return;
                                }
                                else if (kino[novaRada, noveSedlo] == KRAL)
                                { kino[novaRada, noveSedlo] = KRAL; }
                                else if (kino[novaRada, noveSedlo] == POTENCIAL) { kino[novaRada, noveSedlo] = POTENCIAL; }
                                else if (kino[novaRada, noveSedlo] == OBSAZENO)
                                { break; }
                        }
                        for (int j = 0; j < dyp.Length; j++)
                        {
                            int novaRada = Krada;
                            int noveSedlo = Ksedlo + dyp[j];

                            if (novaRada >= 0 && novaRada < POCET_RAD &&
                            noveSedlo >= 0 && noveSedlo < SeDADLA_NA_RADU)
                                if (kino[novaRada, noveSedlo] == VOLNY)
                                { kino[novaRada, noveSedlo] = POTENCIAL; }
                                else if (kino[novaRada, noveSedlo] == CIL)
                                {
                                    Console.WriteLine("ANO NALEZENO!");
                                    Console.WriteLine($"takhle dlouho ({kroky}tah/y)to tlustochovi trvalo");
                                    ZobrazKino(kino);
                                    return;
                                }
                                else if (kino[novaRada, noveSedlo] == KRAL)
                                { kino[novaRada, noveSedlo] = KRAL; }
                                else if (kino[novaRada, noveSedlo] == POTENCIAL) { kino[novaRada, noveSedlo] = POTENCIAL; }
                                else if (kino[novaRada, noveSedlo] == OBSAZENO)
                                { break; }
                        }
                        for (int k = 0; k < dxm.Length; k++)
                        {
                            int novaRada = Krada + dxm[k];
                            int noveSedlo = Ksedlo;

                            if (novaRada >= 0 && novaRada < POCET_RAD &&
                            noveSedlo >= 0 && noveSedlo < SeDADLA_NA_RADU)
                                if (kino[novaRada, noveSedlo] == VOLNY)
                                { kino[novaRada, noveSedlo] = POTENCIAL; }
                                else if (kino[novaRada, noveSedlo] == CIL)
                                {
                                    Console.WriteLine("ANO NALEZENO!");
                                    Console.WriteLine($"takhle dlouho ({kroky}tah/y)to tlustochovi trvalo");
                                    ZobrazKino(kino);
                                    return;
                                }
                                else if (kino[novaRada, noveSedlo] == KRAL)
                                { kino[novaRada, noveSedlo] = KRAL; }
                                else if (kino[novaRada, noveSedlo] == POTENCIAL) { kino[novaRada, noveSedlo] = POTENCIAL; }
                                else if (kino[novaRada, noveSedlo] == OBSAZENO)
                                { break; }
                        }
                        for (int l = 0; l < dym.Length; l++)
                        {
                            int novaRada = Krada;
                            int noveSedlo = Ksedlo + dym[l];

                            if (novaRada >= 0 && novaRada < POCET_RAD &&
                            noveSedlo >= 0 && noveSedlo < SeDADLA_NA_RADU)
                                if (kino[novaRada, noveSedlo] == VOLNY)
                                { kino[novaRada, noveSedlo] = POTENCIAL; }
                                else if (kino[novaRada, noveSedlo] == CIL)
                                {
                                    Console.WriteLine("ANO NALEZENO!");
                                    Console.WriteLine($"takhle dlouho ({kroky}tah/y)to tlustochovi trvalo");
                                    ZobrazKino(kino);
                                    return;
                                }
                                else if (kino[novaRada, noveSedlo] == KRAL)
                                { kino[novaRada, noveSedlo] = KRAL; }
                                else if (kino[novaRada, noveSedlo] == POTENCIAL) { kino[novaRada, noveSedlo] = POTENCIAL; }
                                else if (kino[novaRada, noveSedlo] == OBSAZENO)
                                { break; }
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                if (Jepotencial(kino))
                {
                    ZobrazKino(kino);
                    continue;

                }
                else
                {
                    Console.Write("-1:");
                    ZobrazKino(kino);
                    break;
                }
            }

            static char[,] Vytvorkino()
            {
                char[,] kino = new char[POCET_RAD, SeDADLA_NA_RADU];
                for (int r = 0; r < POCET_RAD; r++)
                    for (int s = 0; s < SeDADLA_NA_RADU; s++)
                        kino[r, s] = VOLNY;
                return kino;
            }
            static void NajdiPotencial(char[,] kino)
            {
                for (int r = 0; r < POCET_RAD; r++)
                {
                    for (int s = 0; s < SeDADLA_NA_RADU; s++)
                    {
                        if (kino[r, s] == POTENCIAL)
                        {
                            kino[r, s] = KRAL;
                        }
                    }
                }
            }
            static bool Jepotencial(char[,] kino)
            {
                for (int r = 0; r < POCET_RAD; r++)
                {
                    for (int s = 0; s < SeDADLA_NA_RADU; s++)
                    {
                        if (kino[r, s] == POTENCIAL)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            static bool NajdiKrale(char[,] kino, out int rada, out int sedlo)
            {
                for (int r = 0; r < POCET_RAD; r++)
                {
                    for (int s = 0; s < SeDADLA_NA_RADU; s++)
                    {
                        if (kino[r, s] == KRAL)
                        {
                            rada = r;
                            sedlo = s;
                            return true;
                        }
                    }
                }
                rada = -1;
                sedlo = -1;

                return false;
            }
            static void Menu()
            {
                Console.WriteLine("\n1 - Zobrazit kino");
                Console.WriteLine("2 - Reservovat sedla");
                Console.WriteLine("3 - je sedlo volne vole ");
                Console.WriteLine("4 - Ukončit program");
            }
            static int Nacti(string message)
            {
                Console.Write(message);
                return int.TryParse(Console.ReadLine(), out int value) ? value : -1;
            }
            static bool IsInRange(int rada, int sedlo)
            {
                return rada >= 0 && rada < POCET_RAD &&
                sedlo >= 0 && sedlo < SeDADLA_NA_RADU;
            }
            static bool JeSedloVolne(char[,] kino, int rada, int sedlo)
            {
                return kino[rada, sedlo] == VOLNY;
            }
            static bool ReservovatSedlo(char[,] kino, int rada, int sedlo, char Typ)
            {
                if (!IsInRange(rada, sedlo)) return false;
                if (!JeSedloVolne(kino, rada, sedlo)) return false;
                kino[rada, sedlo] = Typ;
                return true;
            }
            static int SpocitejCena(int rada)
            {
                int cena = CENA;
                if (JeVip(rada))
                    cena = cena + VIP_CENA;
                return cena;
            }
            static bool JeVip(int rada)
            {
                int radaNumber = rada + 1;
                return radaNumber >= VIP_OD_RADY && radaNumber <= VIP_DO_RADY;
            }
            static void ZobrazKino(char[,] kino)
            {
                Console.WriteLine("\nKino:");
                Console.Write(" ");
                for (int s = 0; s < SeDADLA_NA_RADU; s++)
                    Console.Write($"{s + 1} ");
                Console.WriteLine();
                for (int r = 0; r < POCET_RAD; r++)
                {
                    Console.Write($"{r + 1}: ");
                    for (int s = 0; s < SeDADLA_NA_RADU; s++)
                        Console.Write($"{kino[r, s]} ");
                    Console.WriteLine();
                }
            }
            static void Rezervace(char[,] kino, char typ)
            {
                int rada = Nacti("Zadejte řadu: ") - 1;
                int sedlo = Nacti("Zadejte sedlo: ") - 1;
                if (!ReservovatSedlo(kino, rada, sedlo, typ))
                {
                    Console.WriteLine("blbě to máš.");
                    return;
                }
                int cena = SpocitejCena(rada);

            }
            static void ZkontrolujObsazeni(char[,] kino)
            {
                int rada = Nacti("Zadejte řadu: ") - 1;
                int sedlo = Nacti("Zadejte sedlo: ") - 1;
                if (!IsInRange(rada, sedlo))
                {
                    Console.WriteLine("Sedadlo neexistuje.");
                    return;
                }
                Console.WriteLine(JeSedloVolne(kino, rada, sedlo)
                ? "Volne."
                : "to snad vidis ze je plno");
            }
        }
    }
}
