using System.Xml.Linq;

namespace WebProEIng.Services
{
    // Tento zdroj dat stáhne kurzovní lístek z webu ČNB.
    // ČNB naštěstí posílá data rovnou jako XML, takže nic nemusíme počítat.
    // ": IDataProvider" znamená, že tato třída plní "smlouvu" IDataProvider.
    public class CnbKurzyProvider : IDataProvider
    {
       
        // Adresa (URL), ze které data stahujeme.
        private const string CnbXmlUrl =
            "https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.xml";

        // Pomocník, který umí stahovat z internetu. Dostaneme ho "zvenčí".
        private readonly IHttpClientFactory _httpClientFactory;

        // Konstruktor - spustí se při vytvoření této třídy a uloží si pomocníka.
        public CnbKurzyProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Název zdroje (jen pro log).
        public string Nazev => "ČNB kurzovní lístek";
 

        // Tady se děje hlavní práce: stáhneme XML a vrátíme ho.
        public async Task<XElement> ZiskejDataAsync(CancellationToken stoppingToken)
        {
         
            // Vytvoříme si "stahovač".
            HttpClient client = _httpClientFactory.CreateClient();

            // Stáhneme text (XML) z adresy ČNB. "await" = počkej, než se stáhne.
            var xml = await client.GetStringAsync(CnbXmlUrl, stoppingToken);

            // Z textu uděláme XML, se kterým umí program pracovat.
            var dokument = XDocument.Parse(xml);

            // Vrátíme hlavní (kořenový) element <kurzy ...> z toho XML.
            return dokument.Root!;
        }
    }
}
