using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// Obtém a Temperatura.
        /// </summary>
        public float Temperatura => _weatherModel.Temperatura;

        /// <summary>
        /// Obtém a TemperaturaMaxima.
        /// </summary>
        public float TemperaturaMaxima => _weatherModel.TemperaturaMaxima;

        /// <summary>
        /// Obtém a TemperaturaMinima.
        /// </summary>
        public float TemperaturaMinima => _weatherModel.TemperaturaMinima;



        public WeatherWrapperViewModel(WeatherModel weatherModel) => _weatherModel = weatherModel;
    }
}
