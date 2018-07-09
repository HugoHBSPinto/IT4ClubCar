using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.Services.DataAccess;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using Newtonsoft.Json.Linq;

namespace IT4ClubCar.IT4ClubCar.Services.Weather
{
    class WeatherService : IWeatherService
    {
        private WeatherAPI _weatherAPI;



        public WeatherService(WeatherAPI weatherAPI)
        {
            _weatherAPI = weatherAPI;
        }



        public async Task<WeatherWrapperViewModel> ObterTempoPorNomeCidade(string nomeCidade)
        {
            string apiKey = await ObterAPIKey();

            string weatherInfoData = await _weatherAPI.ObterDadosJson(apiKey, nomeCidade);

            JObject weatherObject = JObject.Parse(weatherInfoData);

            WeatherModel weatherModel = new WeatherModel(String.Empty, String.Empty, String.Empty, 0, 0, 0);

            return new WeatherWrapperViewModel(weatherModel);
        }



        public async Task<string> ObterAPIKey()
        {
            return String.Empty;
        }

    }
}
