using System.Xml.Linq;

namespace WebProEIng.Services
{
    // Toto je "rozhraní" (interface). Je to jako smlouva:
    // každý zdroj dat musí umět dvě věci - mít název a vrátit data jako XML.
    // Díky tomu můžeme mít mnoho různých zdrojů a pracovat s nimi stejně.
    public interface IDataProvider
    {
        // Název zdroje - používáme ho jen při psaní zpráv do logu.
        string Nazev { get; }
   

        // Získá data a vrátí je jako jeden kousek XML (element).
        // "Task" a "async" znamenají, že to může chvíli trvat (třeba stahování).
        Task<XElement> ZiskejDataAsync(CancellationToken stoppingToken);
    }
}
