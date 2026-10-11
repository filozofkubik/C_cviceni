


using System.Xml.Linq;

namespace WebProEIng.Services
{
    // Nejjednodušší možný zdroj dat: vrátí aktuální datum a čas.
    // Nic nestahuje z internetu - jen se zeptá počítače, kolik je hodin.
    public class AktualniCasProvider : IDataProvider
    {
        // Název zdroje (jen pro log).
        public string Nazev => "Aktuální čas";

        // Vrátí element <aktualniCas> s dnešním datem a časem.
        public Task<XElement> ZiskejDataAsync(CancellationToken stoppingToken)
        {
            // DateTime.Now = právě teď. ToString(...) převede čas na text.
            string cas = DateTime.Now.ToString("d.M.yyyy HH:mm:ss");

            // Vytvoříme XML element, např.: <aktualniCas>9.10.2026 08:00:00</aktualniCas>
            var element = new XElement("aktualniCas", cas);

            // Tento zdroj nic nestahuje, takže výsledek vrátíme rovnou.
            return Task.FromResult(element);
        }
    }
}
