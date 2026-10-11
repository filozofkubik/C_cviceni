using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;


namespace WebProEIng.Services
{
    public class ReadPocasi
    {
        private JsonDocument chmupocasi;
        private string vyslednePocasi;




        public void ZjistiPocasi()
        {
            string nazevpolozky = "";
                string obsahpolozky = "";
            var parsovacitrida = new ParserJson(chmupocasi);
            var castelementu = parsovacitrida.ParseCastiJsonElement("/data/features/properties/data");
            foreach (JsonElement prvek in castelementu.EnumerateArray())
            {
                 nazevpolozky = "";
                 obsahpolozky = "";
                foreach (JsonProperty prvek1 in prvek.EnumerateObject())
                {
                    if (prvek1.Name == "headline" )
                    {
                        nazevpolozky = prvek1.Value.ToString();
                    }
                    if ( prvek1.Name == "displayText")
                    {
                        obsahpolozky = prvek1.Value.ToString();

                    }
                }
               
                vyslednePocasi += nazevpolozky + " " + obsahpolozky;
            }
        }
  
        public string VypisPocasi() {  return vyslednePocasi; }
        public ReadPocasi(JsonDocument ChmuJsonPocasi) {

            chmupocasi = ChmuJsonPocasi;
        }

    }
}
