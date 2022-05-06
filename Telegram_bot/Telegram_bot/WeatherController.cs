using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Telegram_bot
{
    internal class WeatherController
    {
        public static async Task<string> GetInfoFrom(string city)
        {
            // Wrap in method
            var url = @$"https://api.openweathermap.org/data/2.5/weather?q={city}&&lang=ru&appid={Configuration.OpenWeatherToken}";
            var client = new HttpClient();
            const float offset = 273f;
            const string degreeCelsius = "\u00B0C";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var node = JObject.Parse(body);

            var temperature = (float)node["main"]?["temp"] - offset;
            var weatherDescription = (string)node["weather"]?[0]?["description"];
            var windSpeed = (string)node["wind"]?["speed"];

            return $"{city}:\n{temperature:F1}{degreeCelsius}, {weatherDescription}, cкорость ветра - {windSpeed} м/c.";
        }
    }
}
