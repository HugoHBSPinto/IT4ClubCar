using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

            string weatherInfoData = await _weatherAPI.GetStringJson(apiKey, nomeCidade);

            JObject weatherObject = JObject.Parse(weatherInfoData);

            

            return id;
        }



        public async Task<string> ObterAPIKey()
        {
            return String.Empty;
        }

    }
}
