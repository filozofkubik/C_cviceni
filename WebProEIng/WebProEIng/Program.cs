// ==========================================================================
// Program.cs = startovní bod celé aplikace. Zde se vše "nastaví" a spustí.
// ==========================================================================

// "builder" slouží k nastavení aplikace (jaké služby bude používat).
var builder = WebApplication.CreateBuilder(args);

// Zapneme Razor Pages = webové stránky (soubory .cshtml ve složce Pages).
builder.Services.AddRazorPages();

// HttpClient umí stahovat data z internetu (např. z webu ČNB).
builder.Services.AddHttpClient();

// --------------------------------------------------------------------------
// ZDROJE DAT
// Každý zdroj dat je jedna třída. Pokud chcete přidat nový zdroj,
// vytvoříte novou třídu a přidáte sem jeden řádek (jako ty dva níže).
// --------------------------------------------------------------------------
builder.Services.AddSingleton<WebProEIng.Services.IDataProvider, WebProEIng.Services.Pocasi>();
builder.Services.AddSingleton<WebProEIng.Services.IDataProvider, WebProEIng.Services.CnbKurzyProvider>();
builder.Services.AddSingleton<WebProEIng.Services.IDataProvider, WebProEIng.Services.AktualniCasProvider>();
builder.Services.AddSingleton<WebProEIng.Services.IDataProvider, WebProEIng.Services.Pokus>();


// Služba, která běží na pozadí, pravidelně projde všechny zdroje dat
// a uloží je do jednoho XML souboru.
builder.Services.AddSingleton<WebProEIng.Services.SberDatSluzba>();
builder.Services.AddHostedService(sp =>
    sp.GetRequiredService<WebProEIng.Services.SberDatSluzba>());

// Postavíme hotovou aplikaci z nastavení výše.
var app = builder.Build();

// Pokud aplikace neběží ve vývoji (ale "naostro"), zapneme hezčí chyby a HTTPS.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // přesměruje http -> https
app.UseRouting();          // umí najít správnou stránku podle adresy
app.UseAuthorization();    // kontrola oprávnění (zde nepoužíváme)

app.MapStaticAssets();     // obrázky, CSS, JavaScript
app.MapRazorPages()        // naše stránky
   .WithStaticAssets();

// Spustíme aplikaci. Od teď poslouchá a čeká na návštěvníky webu.
app.Run();
