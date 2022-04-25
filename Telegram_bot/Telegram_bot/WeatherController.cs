using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Telegram_bot
{
    internal class WeatherController
    {
        public static async void GetInfoFrom(string city)
        {
            try
            {
                var url = @$"https://api.openweathermap.org/data/2.5/weather?q={city}&&lang=ru&appid={Configuration.OpenWeatherToken}";
                var client = new HttpClient();

                var httpResponse = await client.GetAsync(url);
                var responseBody = await httpResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<WeatherData>(responseBody);

                var _ = data?.Main.FirstOrDefault(r => r.Key == "temp").Value;

                Console.WriteLine(_);

            }
            catch (WebException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

    }
}
