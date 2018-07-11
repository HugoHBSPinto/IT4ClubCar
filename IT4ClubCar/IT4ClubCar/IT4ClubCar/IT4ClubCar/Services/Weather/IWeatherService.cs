using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Weather
{
    interface IWeatherService
    {
        /// <summary>
        /// Obtém as previsões de tempo do dia atual em intervalos de 3 horas.
        /// </summary>
        /// <param name="nomeCidade">Nome da cidade a obter-se o tempo.</param>
        /// <returns>ObservableCollection<WeatherWrapperViewModel> com as previsões da cidade passada como parâmetro.</returns>
        Task<ObservableCollection<WeatherWrapperViewModel>> ObterPrevisoesPorNomeCidade(string nomeCidade);

        /// <summary>
        /// Obtém a API key para a API da OpenWeatherMap.
        /// </summary>
        /// <returns>String que representa a API Key.</returns>
        Task<string> ObterAPIKey();

        /// <summary>
        /// Converte uma temperatura em Fahrenheit para Celsius.
        /// </summary>
        /// <param name="tempEmFah">Temperatura em Fahrenheit.</param>
        /// <returns>float com a temperatura em Celsius.</returns>
        float ConverterFahrenheitEmCelsius(float tempEmFah);

        /// <summary>
        /// Converte uma temperatura em Celsius para Fahrenheit.
        /// </summary>
        /// <param name="tempEmCelsius">Temperatura em Celsius.</param>
        /// <returns>float com a temperatura em Fahrenheit.</returns>
        float ConverterCelsiusEmFahrenheit(float tempEmCelsius);
    }
}
