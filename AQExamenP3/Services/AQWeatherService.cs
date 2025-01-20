using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AQExamenP3.Models;
using AQExamenP3.ViewModels;
using AQExamenP3.Services;


namespace AQExamenP3.Services
{
    public class AQWeatherService
    {
        private static readonly HttpClient client = new HttpClient();
        private const string ApiKey = "3ce8cc64a5668c1963c47ef1ea9c0e55"; // Usa tu clave aquí
        private const string BaseUrl = "https://api.weatherstack.com/current";

        public async Task<AQWeather> GetWeatherAsync(string location)
        {
            try
            {
                var response = await client.GetStringAsync($"{BaseUrl}?access_key={ApiKey}&query={location}");
                var weatherData = JsonConvert.DeserializeObject<WeatherApiResponse>(response);

                return new AQWeather
                {
                    Location = weatherData.location.name,
                    Country = weatherData.location.country,
                    Temperature = weatherData.current.temperature,
                    WeatherDescription = weatherData.current.weather_descriptions[0],
                    WeatherIconUrl = weatherData.current.weather_icons[0],
                    ObservationTime = DateTime.Parse(weatherData.current.observation_time)
                };
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Error al obtener los datos del clima: {ex.Message}");
                return null; // Devolver null si ocurre un error
            }
        }
    }

    // Clases internas para mapear la respuesta JSON
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

    public   class Current
    {
        public int temperature { get; set; }
        public string[] weather_descriptions { get; set; }
        public string[] weather_icons { get; set; }
        public string observation_time { get; set; }
    }
}
