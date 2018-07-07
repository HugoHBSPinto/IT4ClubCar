using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Weather
{
    interface IWeatherService
    {
        /// <summary>
        /// Obtém o tempo de uma determinada cidade no momento.
        /// </summary>
        /// <param name="nomeCidade">Nome da cidade a obter-se o tempo.</param>
        /// <returns>WeatherWrapperViewModel com o tempo da cidade passada como parâmetro.</returns>
        Task<WeatherWrapperViewModel> ObterTempoPorNomeCidade(string nomeCidade);

        /// <summary>
        /// Obtém a API key para a API da OpenWeatherMap.
        /// </summary>
        /// <returns>String que representa a API Key.</returns>
        Task<string> ObterAPIKey();
    }
}
