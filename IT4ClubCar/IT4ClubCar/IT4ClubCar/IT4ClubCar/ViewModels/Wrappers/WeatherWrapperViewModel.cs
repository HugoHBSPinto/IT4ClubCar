using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.Services.Weather;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static IT4ClubCar.IT4ClubCar.Models.WeatherModel;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class WeatherWrapperViewModel : BaseWrapperViewModel
    {
        private WeatherModel _weatherModel;

        /// <summary>
        /// Obtém o NomeCidade.
        /// </summary>
        public string NomeCidade => _weatherModel.NomeCidade;

        /// <summary>
        /// Obtém o Nome.
        /// </summary>
        public string Nome => _weatherModel.Nome;

        /// <summary>
        /// Obtém a Descricao.
        /// </summary>
        public string Descricao => _weatherModel.Descricao;

        /// <summary>
        /// Obtém e define a Temperatura.
        /// </summary>
        public float Temperatura
        {
            get
            {
                return _weatherModel.Temperatura;
            }
            private set
            {
                _weatherModel.Temperatura = value;
                OnPropertyChanged("Temperatura");
            }
        }

        /// <summary>
        /// Obtém e define a TemperaturaMaxima.
        /// </summary>
        public float TemperaturaMaxima
        {
            get
            {
                return _weatherModel.TemperaturaMaxima;
            }
            private set
            {
                _weatherModel.TemperaturaMaxima = value;
                OnPropertyChanged("TemperaturaMaxima");
            }
        }

        /// <summary>
        /// Obtém e define TemperaturaMinima.
        /// </summary>
        public float TemperaturaMinima
        {
            get
            {
                return _weatherModel.TemperaturaMinima;
            }
            private set
            {
                _weatherModel.TemperaturaMinima = value;
                OnPropertyChanged("TemperaturaMinima");
            }
        }

        /// <summary>
        /// Obtém as horas.
        /// </summary>
        public string Horas => _weatherModel.Horas;

        /// <summary>
        /// Obtém o TipoTemperatura.
        /// </summary>
        public TipoTemperatura TipoTemperatura
        {
            get
            {
                return _weatherModel.TipoTemp;
            }
            private set
            {
                _weatherModel.TipoTemp = value;
            }
        }



        public WeatherWrapperViewModel(WeatherModel weatherModel) => _weatherModel = weatherModel;



        public void AlterarTipoTemperatura(IWeatherService weatherService,TipoTemperatura tipoTemperaturaPedida)
        {
            if (TipoTemperatura.Equals(tipoTemperaturaPedida))
                return;

            if(tipoTemperaturaPedida.Equals(TipoTemperatura.ºC))
            {
                Temperatura = weatherService.ConverterFahrenheitEmCelsius(Temperatura);
                TemperaturaMaxima = weatherService.ConverterFahrenheitEmCelsius(TemperaturaMaxima);
                TemperaturaMinima = weatherService.ConverterFahrenheitEmCelsius(TemperaturaMinima);
            }
            else
            {
                Temperatura = weatherService.ConverterCelsiusEmFahrenheit(Temperatura);
                TemperaturaMaxima = weatherService.ConverterCelsiusEmFahrenheit(TemperaturaMaxima);
                TemperaturaMinima = weatherService.ConverterCelsiusEmFahrenheit(TemperaturaMinima);
            }

            TipoTemperatura = tipoTemperaturaPedida;
        }
        
    }
}
