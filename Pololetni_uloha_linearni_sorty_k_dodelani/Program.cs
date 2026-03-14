using System.Text;

namespace PololetniUloha
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // (20b) 1. Seřaďte známky ze souboru znamky.txt od 1 do 5 algoritmem s lineární časovou složitostí vzhledem k počtu známek. 
            // Vypište je na řádek a pak vypište i četnosti jednotlivých známek.
            int[] pocetz = new int[6];
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\..\..\..\znamky.txt")) // otevření souboru pro čtení
            {
                    string asasa;
                
                
                while ((asasa = sr.ReadLine()) != null) // dokud jsme nedošli na konec souboru
                        {
                        int znamka;
                        if (int.TryParse(asasa, out znamka) && znamka >= 1 && znamka <= 5)
                        {
                            pocetz[znamka]++;
                        }

                    }


                }
            

            for(int i = 1; i <= 5;i++)
            {
                for(int j = 0; j < pocetz[i];j++)
                {

                    Console.Write(i + " ");
                }
            }
            Console.WriteLine("\n");

                for (int i = 1; i <= 5; i++)
                {
                    if (pocetz[i] > 0)
                    {
                        Console.WriteLine($"Známka {i}: {pocetz[i]}×");
                    }
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba pri ctení dokumentu");
            }
            // => to, co jste pravděpodobně stvořili se nazývá Counting Sort
            Console.WriteLine("\n");


            // (40b) 2. Ze souboru znamky_prezdivky.csv vytvořte objekty typu Student se správně přiřazenou známkou a přezdívkou.
            // Seřaďte je podle známek (stabilně = dodržte pořadí v souboru) a vypište seřazené dvojice (znamka: přezdívka) - na každý řádek jednu.
            List<Student>[] seznamy = new List<Student>[6];
            for (int i = 0; i < seznamy.Length; i++)
            {
                seznamy[i] = new List<Student>();
            }

            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\..\..\..\znamky_prezdivky.csv"))
            {
                    string sdsdsa;
                while ((sdsdsa = sr.ReadLine()) != null)
                {
                        string[] casti = sdsdsa.Split(';');
                        string prezdivka = casti[0];
                        {
                            if (int.TryParse(casti[1], out int znamka) && znamka >= 1 && znamka <= 5)
                            {
                                seznamy[znamka].Add(new Student(prezdivka, znamka));
                            }
                        }
                        ;
                    

                }                
                 
            }

                // => to, co jste pravděpodobně stvořili se nazývá Bucket Sort (přihrádkové řazení)
                for (int i = 1; i <= 5; i++)
                {
                    foreach (var student in seznamy[i])
                    {
                        Console.WriteLine($"{student.Znamka}: {student.Prezdivka}");
                    }
                }



                // (10b) 3. Určete časovou a prostorovou složitost algoritmu z 2. úkolu




                // (+60b) 4. BONUS: Napište kód, který bude řadit lexikograficky velká čísla v lineárním čase. Využijte dat ze souboru velka_cisla.txt
            }
            catch (Exception ex)
            {
                Console.WriteLine("chybne ctení z dokumentu");
            }
            // (10b) 3. Určete časovou a prostorovou složitost algoritmu z 2. úkolu====================>>>>> časová složitost v nejhorším případě kvadratická, prostorová lineární
            // (+60b) 4. BONUS: Napište kód, který bude řadit lexikograficky velká čísla v lineárním čase. Využijte dat ze souboru velka_cisla.txt
            string filePath = @"..\..\..\..\..\velka_cisla.txt";
            List<string> seznamcisel = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string hsjjsjj;
                    while ((hsjjsjj = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(hsjjsjj))
                        {
                            seznamcisel.Add(hsjjsjj.Trim());
                        }
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("chybne ctení z dokumentu");
            }

            int n = seznamcisel.Count;

            // Bubble Sort
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (string.Compare(seznamcisel[j], seznamcisel[j + 1], StringComparison.Ordinal) > 0)
                    {
                        string promena = seznamcisel[j];
                        seznamcisel[j] = seznamcisel[j + 1];
                        seznamcisel[j + 1] = promena;
                    }
                }
            }


            for (int i = 0; i < n; i++)
            {
                Console.Write(seznamcisel[i] + " ");
            }





        }
    }

    class Student
    {
        public string Prezdivka { get; } // tím, že je zde pouze get říkáme, že tato vlastnost třídy Student jde mimo třídu pouze číst, nikoli upravovat
        public int Znamka { get; }
        public Student(string prezdivka, int znamka) // konstruktor třídy
        {
            // použitím samotného { get; } také říkáme, že tyto vlastnosti jdou nastavit nejpozději v konstruktoru - tedy v této metodě
            Prezdivka = prezdivka;
            Znamka = znamka;
        }
    }
}