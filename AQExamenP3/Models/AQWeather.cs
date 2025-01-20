using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQExamenP3.Models
{
    public class AQWeather
    {
        public string Location { get; set; }
        public string Country { get; set; }
        public int Temperature { get; set; }
        public string WeatherDescription { get; set; }
        public string WeatherIconUrl { get; set; }
        public DateTime ObservationTime { get; set; }
    }
}
    