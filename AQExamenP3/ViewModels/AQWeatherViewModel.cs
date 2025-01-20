using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AQExamenP3.Models;
using AQExamenP3.Services;
using System.Threading.Tasks;

namespace AQExamenP3.ViewModels
{
    public class AQWeatherViewModel : INotifyPropertyChanged
    {
        public AQWeatherService WeatherService { get; set; }
        private string _location;
        private AQWeather _currentWeather;
        private bool _isBusy;

        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();  // Notificar que Location ha cambiado
            }
        }

        public AQWeather CurrentWeather
        {
            get => _currentWeather;
            set
            {
                _currentWeather = value;
                OnPropertyChanged();  // Notificar que CurrentWeather ha cambiado
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();  // Notificar que IsBusy ha cambiado
            }
        }

        public ICommand FetchWeatherCommand { get; set; }

        public AQWeatherViewModel()
        {
            WeatherService = new AQWeatherService();
            FetchWeatherCommand = new Command(async () => await FetchWeatherAsync(), () => !IsBusy);
        }

        public async Task FetchWeatherAsync()
        {
            if (string.IsNullOrEmpty(Location))
                return;

            IsBusy = true;

            try
            {
                CurrentWeather = await WeatherService.GetWeatherAsync(Location);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
