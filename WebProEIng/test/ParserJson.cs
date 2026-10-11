
 
 
using System.Globalization;


using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace test
{
    class NavratHodnoty
    {
        private bool naplneno;
        private JsonElement hodnota;
       
        public bool IsNaplneno()
        {
            return naplneno;
        }
        public void napln(JsonElement hodnota)
        {
            naplneno = true;
            this.hodnota = hodnota;
        }
        public string vrathodnotu()
        {
            return hodnota.ToString();

        }
        public JsonElement vrathodnotuJsonElement()
        {
            return hodnota;

        }
        public NavratHodnoty()
        {
            this.naplneno = false;
            this.hodnota =new JsonElement();

        }
    }
    class ParserJson
    {
        private JsonDocument dokumentkezpracování;
        private string oddelovac = "/";
        
        public ParserJson(JsonDocument ChmuJsonPocasi)
        {

            dokumentkezpracování = ChmuJsonPocasi;


        }
        public string ParseCasti(string cestanavrat)
        {
            NavratHodnoty hodnota;
            hodnota = returndata(dokumentkezpracování.RootElement, "", cestanavrat);
            return hodnota.vrathodnotu();
        }
        public JsonElement ParseCastiJsonElement(string cestanavrat)
        {
            NavratHodnoty hodnota;
            hodnota = returndata(dokumentkezpracování.RootElement, "", cestanavrat);
            return hodnota.vrathodnotuJsonElement();
        }

        
        private NavratHodnoty returndata(JsonElement dataroot, string cesta, string cestanavrat)
        {

            NavratHodnoty vystup = new NavratHodnoty();


           
            if (cesta == cestanavrat)
            {

                vystup.napln(dataroot);
             
                return vystup;


            }




          
            if (dataroot.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in dataroot.EnumerateObject())
                {
                    string klic = property.Name;
                    JsonElement hodnota = property.Value;

                    if (hodnota.ValueKind == JsonValueKind.Object)
                    {

                        
                        vystup = returndata(hodnota, cesta + oddelovac + klic, cestanavrat);
                        if (vystup.IsNaplneno())
                        {
                            return vystup;
                        }
                    }
                    else if (hodnota.ValueKind == JsonValueKind.Array)
                    {

                        
                        vystup = returndata(hodnota, cesta + oddelovac + klic, cestanavrat);
                        if (vystup.IsNaplneno())
                        {
                            return vystup;
                        }
                    }
                    else
                    {
                        if (vystup.IsNaplneno())
                        {
                            return vystup;
                        }
                      
                    }
                }
            }
            
            else if (dataroot.ValueKind == JsonValueKind.Array)
            {
                int index = 0;
                foreach (JsonElement prvek in dataroot.EnumerateArray())
                {
                    if (prvek.ValueKind == JsonValueKind.Object || prvek.ValueKind == JsonValueKind.Array)
                    {

                        
                        vystup = returndata(prvek, cesta, cestanavrat);
                        if (vystup.IsNaplneno())
                        {
                            return vystup;
                        }
                    }
                    else
                    {
                        
                        if (vystup.IsNaplneno())
                        {
                            return vystup;
                        }
                    }
                    index++;
                }
            }
            
            if (vystup.IsNaplneno())
            {
                return vystup;
            }
            return vystup;
        }



        private string parserdata(JsonElement dataroot, string odsazeni, string cesta, bool showtext, string cestanavrat, bool filtrujcestu)
        {

            string vystup;
            vystup = "nic";
            if (filtrujcestu == true)
            {
                Console.WriteLine("ladeni cesta " + cesta);
                Console.WriteLine("ladeni cestanavrat " + cestanavrat);
                if (cesta == cestanavrat)
                {
                    vystup = dataroot.ToString();
                    Console.WriteLine("ladeni vystup " + vystup.Substring(5));
                    return vystup;


                }



            }
            
            if (dataroot.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in dataroot.EnumerateObject())
                {
                    string klic = property.Name;
                    JsonElement hodnota = property.Value;

                    if (hodnota.ValueKind == JsonValueKind.Object)
                    {
                        Console.WriteLine($"{odsazeni}Klíč: {klic} {{Objekt}} -> Zanořuji se:");
                       
                        vystup = parserdata(hodnota, odsazeni + "  ", cesta + oddelovac + klic, showtext, cestanavrat, filtrujcestu);
                    }
                    else if (hodnota.ValueKind == JsonValueKind.Array)
                    {
                        Console.WriteLine($"{odsazeni}Klíč: {klic} [Pole] -> Zanořuji se do prvků:");
                       
                        vystup = parserdata(hodnota, odsazeni + "  ", cesta + oddelovac + klic, showtext, cestanavrat, filtrujcestu);

                    }
                    else
                    {
                        
                        VypisHodnotu(klic, hodnota, odsazeni, cesta, showtext);
                    }
                }
            }
            
            else if (dataroot.ValueKind == JsonValueKind.Array)
            {
                int index = 0;
                foreach (JsonElement prvek in dataroot.EnumerateArray())
                {
                    if (prvek.ValueKind == JsonValueKind.Object || prvek.ValueKind == JsonValueKind.Array)
                    {
                        Console.WriteLine($"{odsazeni}Položka [{index}]:");
                        
                        vystup = parserdata(prvek, odsazeni + "  ", cesta, showtext, cestanavrat, filtrujcestu);

                    }
                    else
                    {
                        VypisHodnotu($"[{index}]", prvek, odsazeni, cesta, showtext);
                    }
                    index++;
                }
            }
            return vystup;
        }

        
        private void VypisHodnotu(string klic, JsonElement hodnota, string odsazeni)
        {
            string textHodnoty = hodnota.ValueKind == JsonValueKind.String ? hodnota.GetString() : hodnota.GetRawText();
            Console.WriteLine($"{odsazeni}Klíč: {klic}, Hodnota: {textHodnoty}");
        }
        private void VypisHodnotu(string klic, JsonElement hodnota, string odsazeni, string cesta, bool showtext)

        {
            if (showtext == true)
            {
                string textHodnoty = hodnota.ValueKind == JsonValueKind.String ? hodnota.GetString() : hodnota.GetRawText();
                Console.WriteLine($"{odsazeni}Klíč: {klic}, Hodnota: {textHodnoty}");
                Console.WriteLine("cesta" + cesta);
            }
        }



        public void VypisCelyJason()
        {
            parserdata(dokumentkezpracování.RootElement, "", "", true, "", false);
           
        }

    }
}
