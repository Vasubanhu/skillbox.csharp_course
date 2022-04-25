using System.Collections.Generic;

namespace Telegram_bot
{
    public class WeatherData
    {
        //public IList<KeyValuePair<string, dynamic>> Weather { get; set; }
        public string Base { get; set; }
        public IList<KeyValuePair<string, object>> Main { get; set; }
        //internal IList<KeyValuePair<string, int>> Wind { get; set; }
    }
}
