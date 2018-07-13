using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private WebService _webService;
        private WeatherAPI _weatherAPI;



        public WeatherService(WeatherAPI weatherAPI,WebService webService)
        {
            _weatherAPI = weatherAPI;
            _webService = webService;
        }



        public async Task<ObservableCollection<WeatherWrapperViewModel>> ObterPrevisoesPorNomeCidade(string nomeCidade)
        {
            ObservableCollection<WeatherWrapperViewModel> previsoes = new ObservableCollection<WeatherWrapperViewModel>();

            string apiKey = await ObterAPIKey();

            string weatherInfoData = await _weatherAPI.ObterDadosJson(apiKey, nomeCidade);

            JObject weatherObjectPrincipal = JObject.Parse(weatherInfoData);

            JArray weatherObjects = JArray.Parse(weatherObjectPrincipal["list"].ToString());

            //propriedades comuns a todos os WeatherWrappers.
            string cidade = nomeCidade;

            string dataHoje = ObterDataAtual();

            //Percorrer previsões obtidades.
            for (int i = 0; i < weatherObjects.Count; i++)
            {
                //Obter data do weatherobject para comparar com a data de hoje. Se já for do dia a seguir sair do loop. Só se quer as previsões para
                //hoje.
                string dataHoraObject = weatherObjects[i]["dt_txt"].ToString();
                string data = dataHoraObject.Split(' ')[0];
                if (!dataHoje.Equals(data))
                    break;

                //Propriedades para o novo objecto Weather.
                string nomeTempo = weatherObjects[i]["weather"][0]["main"].ToString();
                string descricao = weatherObjects[i]["weather"][0]["description"].ToString();
                float temperatura = float.Parse(weatherObjects[i]["main"]["temp"].ToString());
                float temperaturaMinima = float.Parse(weatherObjects[i]["main"]["temp_min"].ToString());
                float temperaturaMaxima = float.Parse(weatherObjects[i]["main"]["temp_max"].ToString());
                string horas = dataHoraObject.Split(' ')[1].ToString();

                previsoes.Add(new WeatherWrapperViewModel(new WeatherModel(cidade, nomeTempo, descricao, temperatura, temperaturaMaxima, temperaturaMinima,horas)));
            }
            
            return previsoes;
        }



        public async Task<string> ObterAPIKey()
        {
            string stringData = await _webService.ObterDadosJson("GetOpenWeatherMapAPIKey");

            JObject apiKeyObject = JObject.Parse(stringData);

            return apiKeyObject["APIKey"].ToString();
        }



        /// <summary>
        /// Obtém a data Atual.
        /// </summary>
        /// <returns>String no formato dd-mm-aa.</returns>
        private string ObterDataAtual()
        {
            return DateTime.Today.ToString("yyyy-MM-dd");
        }



        /// <summary>
        /// Converte uma temperatura em Fahrenheit para Celsius.
        /// </summary>
        /// <param name="tempEmFah">Temperatura em Fahrenheit.</param>
        /// <returns>float com uma casa decimal, com a temperatura em Celsius.</returns>
        public float ConverterFahrenheitEmCelsius(float tempEmFah)
        {
            return (float)Math.Round(((5.0/9.0)*(tempEmFah-32)),1);
        }

        /// <summary>
        /// Converte uma temperatura em Celsius para Fahrenheit.
        /// </summary>
        /// <param name="tempEmCelsius">Temperatura em Celsius.</param>
        /// <returns>float com uma casa decimal, com a temperatura em Fahrenheit.</returns>
        public float ConverterCelsiusEmFahrenheit(float tempEmCelsius)
        {
            return (float)Math.Round((((9.0/5.0)*tempEmCelsius)+32),1);
        }

    }
}
