using AQExamenP3.Models;
using AQExamenP3.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AQExamenP3.ViewModels
{
    public class AQWeatherViewModel : INotifyPropertyChanged
    {
        private readonly AQWeatherService _weatherService;
        private string _location;
        private AQWeather _currentWeather;
        private bool _isBusy;

        public AQWeatherViewModel()
        {
            _weatherService = new AQWeatherService();
            FetchWeatherCommand = new Command(async () => await FetchWeatherAsync(), () => !IsBusy);
        }

        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        public AQWeather CurrentWeather
        {
            get => _currentWeather;
            set
            {
                _currentWeather = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                ((Command)FetchWeatherCommand).ChangeCanExecute();
            }
        }

        public ICommand FetchWeatherCommand { get; }

        private async Task FetchWeatherAsync()
        {
            if (string.IsNullOrEmpty(Location))
                return;

            IsBusy = true;

            try
            {
                // Llamar al servicio de clima
                CurrentWeather = await _weatherService.GetWeatherAsync(Location);

                if (CurrentWeather == null)
                {
                    // Manejo de error: si no se obtienen resultados, mostramos un mensaje o hacemos alguna acción.
                    Console.WriteLine("No se pudo obtener los datos del clima.");
                }
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
