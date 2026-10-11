using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProEIng.Services;

namespace WebProEIng.Pages
{
    // Toto je "kód na pozadí" webové stránky Kurzy.cshtml.
    // Jeho jediný úkol: přečíst hotový XML soubor a předat ho stránce k zobrazení.
    public class KurzyModel : PageModel
    {
        // Odkaz na službu, díky které zjistíme, kde XML soubor leží.
        private readonly SberDatSluzba _sberDat;

        // Konstruktor - službu dostaneme automaticky "zvenčí".
        public KurzyModel(SberDatSluzba sberDat)
        {
            _sberDat = sberDat;
        }

        // Tyto vlastnosti uvidí stránka (.cshtml) a může je vypsat.
        public string? XmlCesta { get; private set; }  // kde soubor leží
        public string? Xml { get; private set; }        // obsah souboru
        public string? Chyba { get; private set; }       // případná chybová hláška

        // Spustí se pokaždé, když někdo otevře stránku /Kurzy.
        public async Task OnGetAsync()
        {
            var cesta = _sberDat.XmlCesta;
            XmlCesta = cesta;

            // Pokud soubor ještě neexistuje (data se stahují), zobrazíme hlášku.
            if (!System.IO.File.Exists(cesta))
            {
                Chyba = "Data zatím nebyla stažena. Zkuste to prosím za chvíli.";
                return;
            }

            // Přečteme celý obsah souboru do textu, který stránka vypíše.
            Xml = await System.IO.File.ReadAllTextAsync(cesta);
        }

        // Toto se zavolá při kliknutí na tlačítko "Stáhnout XML" (/Kurzy?handler=Raw).
        // Vrátí rovnou soubor ke stažení místo zobrazení stránky.
        public async Task<IActionResult> OnGetRawAsync()
        {
            var cesta = _sberDat.XmlCesta;
            if (!System.IO.File.Exists(cesta))
            {
                return NotFound("Data zatím nebyla stažena.");
            }

            var xml = await System.IO.File.ReadAllBytesAsync(cesta);
            return File(xml, "application/xml");
        }
    }
}
