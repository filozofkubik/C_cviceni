namespace ConsoleApp7
{
    internal class Program
    {

        const int POCET_RAD = 8;
    const int SeDADLA_NA_RADU = 10;

    const int CENA = 180;
    const int VIP_CENA = 70;

    const int VIP_OD_RADY = 7;
    const int VIP_DO_RADY = 8;

    const char VOLNY = 'O';
    const char OBSAZENO = 'X';

    static void Main()
    {
        char[,] kino = Vytvorkino();

        while (true)
        {
            Menu();
            int choice = Nacti("Vyberte akci: ");

            switch (choice)
            {
                case 1:
                    ZobrazKino(kino);
                    break;

                case 2:
                    Rezervace(kino);
                    break;

                case 3:
                    ZkontrolujObsazeni(kino);
                    break;

                case 4:
                    Console.WriteLine("Program ukončen.");
                    return;

                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
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


    static bool ReservovatSedlo(char[,] kino, int rada, int sedlo)
    {
        if (!IsInRange(rada, sedlo)) return false;
        if (!JeSedloVolne(kino, rada, sedlo)) return false;

        kino[rada, sedlo] = OBSAZENO;
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

        Console.Write("   ");
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


    static void Rezervace(char[,] kino)
    {
        int rada = Nacti("Zadejte řadu: ") - 1;
        int sedlo = Nacti("Zadejte sedlo: ") - 1;

        if (!ReservovatSedlo(kino, rada, sedlo))
        {
            Console.WriteLine("blbě to máš.");
            return;
        }

        int cena = SpocitejCena(rada);
        Console.WriteLine($"Sedadlo reservováno. Cena: {cena} Kč");
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
