
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;

namespace WebProEIng.Services
{

    public class Pocasi : IDataProvider
    {
        private const string ChmuXmlUrl =
            "https://opendata.chmi.cz/meteorology/climate/historical_csv/";

    private readonly IHttpClientFactory _httpClientFactory;

        public Pocasi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public string Nazev => "Český hydrometorologický úřad ";

        public async Task<XElement> ZiskejDataAsync(CancellationToken stoppingToken)
        {
            XDocument dokument = new XDocument();
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();


                var xml = await client.GetStringAsync(ChmuXmlUrl, stoppingToken);


                 dokument = XDocument.Parse(xml);
            }
            catch (Exception ex) { 
            Debug.WriteLine(ex.Message);
            }

            return dokument.Root!;
        }
    }
}