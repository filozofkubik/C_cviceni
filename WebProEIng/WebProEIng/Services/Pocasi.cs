
using System.Diagnostics;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace WebProEIng.Services
{

    public class Pocasi : IDataProvider
    {
        private const string ChmuXmlUrl =
            "https://opendata.chmi.cz/meteorology/weather/forecast/now/web_pCRntx_101600.json";

    private readonly IHttpClientFactory _httpClientFactory;

        public Pocasi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public string Nazev => "Český hydrometorologický úřad ";

        public async Task<XElement> ZiskejDataAsync(CancellationToken stoppingToken)
        {
            
            XDocument dokument = new XDocument();
          
         
                HttpClient http = new HttpClient();


                string json = await http.GetStringAsync(ChmuXmlUrl);

                using JsonDocument doc = JsonDocument.Parse(json);
                var parsovacitrida = new ParserJson(doc);

                ReadPocasi readpocasi = new ReadPocasi(doc);
                readpocasi.ZjistiPocasi();
            string text = this.Nazev;

            Console.WriteLine(readpocasi.VypisPocasi());
            // Vytvoříme XML element, např.: <aktualniCas>9.10.2026 08:00:00</aktualniCas>
            var element = new XElement(this.Nazev, text);

            // Tento zdroj nic nestahuje, takže výsledek vrátíme rovnou.
            return Task.FromResult(element);
        }
    }
}