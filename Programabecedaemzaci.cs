using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
class Program
{
    static void Main()
    {
        string zadani = Console.ReadLine();

        string[]slova = zadani.Split(' ');
        var seznamznaku = new HashSet<char>();


        
        foreach (var slovo in slova)
            foreach (char znak in slovo)
                seznamznaku.Add(znak);



        var graph = seznamznaku.ToDictionary(znak => znak, _ => new List<char>());



        var hhhh = seznamznaku.ToDictionary(znak => znak, _ => 0);
  





        for (int i = 0; i < slova.Length - 1; i++)
        {
            string prvnislovo = slova[i];       
            string druhydlovo = slova[i + 1];  









            int porovnatddd = Math.Min(prvnislovo.Length, druhydlovo.Length);

          
            bool rozdilnyznak = false;

            
            for (int j = 0; j < porovnatddd; j++)
            {
                char znakzprvni = prvnislovo[j];




                char znakzdruhy = druhydlovo[j];

                
                if (znakzprvni != znakzdruhy)
                {
                    
                    if (!graph[znakzprvni].Contains(znakzdruhy))
                    {
                        graph[znakzprvni].Add(znakzdruhy);  




                        hhhh[znakzdruhy]++;                
                    }

                    rozdilnyznak = true;  
                    break;                   
                }
            }

            






            if (!rozdilnyznak && prvnislovo.Length > druhydlovo.Length)
            {




                Console.WriteLine("obsahuje cyklus => nejde");
                return; 

            }
        }
        var rada = new Queue<char>(seznamznaku.Where(c => hhhh[c] == 0) );





        var jjjj = new List<char>();             
        
        while (rada.Count > 0)
        {
            char uuuu = rada.Dequeue();      
            jjjj.Add(uuuu);         
            




           
            foreach (char xxxx in graph[uuuu])
            {
                hhhh[xxxx]--;                
                if (hhhh[xxxx] == 0)  
                    



                
                    rada.Enqueue(xxxx);       
                }
  
        }
        if (jjjj.Count != seznamznaku.Count)
            Console.WriteLine("obsahuje cyklus => nejde");
        else
            
            Console.WriteLine(string.Join(" -> ", jjjj));
    }
}



// nebude je vidět na prikladu v ucebně5
//ne kdyz uvnitř bude cyclus priklad6







