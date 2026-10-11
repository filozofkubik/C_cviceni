using System.Xml.Linq;

namespace WebProEIng.Services
{
    // Toto je služba, která běží "na pozadí" - sama od sebe, bez návštěvníka webu.
    // Každou hodinu projde všechny zdroje dat a uloží je do jednoho XML souboru.
    // "BackgroundService" je hotová pomůcka z .NET pro úlohy běžící na pozadí.
    public class SberDatSluzba : BackgroundService
    {
        // Jak často data obnovujeme. Teď je to 1 hodina.
        // Chcete rychleji? Změňte na TimeSpan.FromMinutes(1) apod.
        private static readonly TimeSpan Interval = TimeSpan.FromMinutes(1);
        // Seznam všech zdrojů dat (ČNB, čas, ...). Dostaneme ho automaticky.
        private readonly IEnumerable<IDataProvider> _providery;

        // Pomocník pro psaní zpráv do logu (okno Output ve Visual Studiu).
        private readonly ILogger<SberDatSluzba> _logger;

        // Celá cesta k souboru, kam ukládáme výsledné XML.
        private readonly string _xmlCesta;

        // Konstruktor - spustí se jednou při startu aplikace.
        public SberDatSluzba(
            IEnumerable<IDataProvider> providery,
            ILogger<SberDatSluzba> logger,
            IWebHostEnvironment prostredi)
        {
            _providery = providery;
            _logger = logger;

            // Vytvoříme složku "App_Data" (pokud ještě není) a cestu k souboru data.xml.
            var slozka = Path.Combine(prostredi.ContentRootPath, "App_Data");
            Directory.CreateDirectory(slozka);
        
            _xmlCesta = Path.Combine(slozka, "data.xml");

            _logger.LogInformation("Výsledné XML se ukládá do: {Cesta}", _xmlCesta);
        }

        // Umožní webové stránce zjistit, kde soubor leží.
        public string XmlCesta => _xmlCesta;

        // Tato metoda se spustí automaticky a běží celou dobu života aplikace.
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Opakuj pořád dokola, dokud se aplikace nevypne.
            while (!stoppingToken.IsCancellationRequested)
            {
                // 1) Seber data a ulož je. Kdyby nastala chyba, jen ji zapíšeme do logu.
                try
                {
                    await SberejAUlozAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Chyba při sběru dat.");
                }

                // 2) Počkej daný interval (třeba hodinu) a pak zase od začátku.
                try
                {
                    await Task.Delay(Interval, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    // Tahle výjimka přijde při vypínání aplikace - ukončíme smyčku.
                    break;
                }
            }
        }

        // Projde všechny zdroje dat a poskládá z nich jedno XML.
        private async Task SberejAUlozAsync(CancellationToken stoppingToken)
        {
            // Vytvoříme hlavní element <data>. Do něj budeme vkládat výstupy zdrojů.
            var root = new XElement("data");

            // SMYČKA: pro každý zdroj dat...
            foreach (var provider in _providery)
            {
                try
                {
                    // ...zavoláme jeho metodu, která vrátí kousek XML...
                    var element = await provider.ZiskejDataAsync(stoppingToken);

                    // ...a ten kousek vložíme do <data>.
                    root.Add(element);

                    _logger.LogInformation("Zdroj '{Nazev}' úspěšně načten.", provider.Nazev);
                }
                catch (Exception ex)
                {
            
                    // Když jeden zdroj selže, ostatní tím nezastavíme.
                    _logger.LogError(ex, "Zdroj '{Nazev}' selhal.", provider.Nazev);
                }
            }

            // Z elementu <data> uděláme celý XML dokument (včetně prvního řádku <?xml ...?>).
            var dokument = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                root);

            // Uložíme na disk.
            UlozXml(dokument);
            _logger.LogInformation("Data uložena do {Cesta}.", _xmlCesta);
        }

        // Uloží XML do souboru.
        private void UlozXml(XDocument dokument)
        {
            // Nejdřív zapíšeme do dočasného souboru (.tmp) a pak ho přejmenujeme.
            // Díky tomu web nikdy neuvidí napůl zapsaný soubor.
            var docasny = _xmlCesta + ".tmp";
            dokument.Save(docasny);
            File.Move(docasny, _xmlCesta, overwrite: true);
        }
    }
}
