

using System.Net.Http;
using System.Text;
using System.Text.Json;
using test;

Console.OutputEncoding = Encoding.UTF8;
HttpClient http = new HttpClient();

    string url =
        "https://opendata.chmi.cz/meteorology/weather/forecast/now/web_pCRntx_101600.json";

    string json = await http.GetStringAsync(url);

    using JsonDocument doc = JsonDocument.Parse(json);
    var parsovacitrida = new ParserJson(doc);

    ReadPocasi readpocasi = new ReadPocasi(doc);
    readpocasi.ZjistiPocasi();

    Console.WriteLine(readpocasi.VypisPocasi());


