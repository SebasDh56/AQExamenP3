using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using AQExamenP3.Models;

namespace AQExamenP3.Services
{
    public class AQWeatherService
    {
        public static readonly HttpClient client = new HttpClient();
        public const string ApiKey = "3ce8cc64a5668c1963c47ef1ea9c0e55"; // Cambia esto por tu clave de acceso
        public const string BaseUrl = "https://api.weatherstack.com/current";

        public async Task<AQWeather> GetWeatherAsync(string location)
        {
            var response = await client.GetStringAsync($"{BaseUrl}?access_key={ApiKey}&query={location}");
            var weatherData = JsonConvert.DeserializeObject<WeatherApiResponse>(response);
            return new AQWeather
            {
                Location = weatherData.location.name,
                Country = weatherData.location.country,
                Temperature = weatherData.current.temperature,
                WeatherDescription = weatherData.current.weather_descriptions[0],
                WeatherIconUrl = weatherData.current.weather_icons[0]
            };
        }

        public class WeatherApiResponse
        {
            public Location location { get; set; }
            public Current current { get; set; }
        }

        public class Location
        {
            public string name { get; set; }
            public string country { get; set; }
        }

        public class Current
        {
            public int temperature { get; set; }
            public string[] weather_descriptions { get; set; }
            public string[] weather_icons { get; set; }
        }
    }
}
