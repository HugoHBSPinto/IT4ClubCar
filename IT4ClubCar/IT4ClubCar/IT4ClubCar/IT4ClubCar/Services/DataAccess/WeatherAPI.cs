using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.DataAccess
{
    class WeatherAPI
    {
        private static HttpClient _cliente = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(2)
        };



        public WeatherAPI()
        {
            
        }



        /// <summary>
        ///Obtém uma string que contém os dados requisitados podendo ser convertida num JArray.
        /// </summary>
        /// <param name="dadosRequisitados">Dados a serem obtidos</param>
        /// <returns>String que pode ser convertida em json</returns>
        public async Task<string> GetStringJson(string apiKey,string dadosRequisitados, CancellationToken token = new CancellationToken())
        {
            string data = "";

            string pedido = "http://api.openweathermap.org/data/2.5/weather?q=" + dadosRequisitados + "&APPID=9ea50f72023fede98b2d8929b8b5a693" + apiKey;

            try
            {
                using (HttpResponseMessage resposta = await _cliente.GetAsync(requestUri: pedido, cancellationToken: token))
                {
                    if (resposta.IsSuccessStatusCode)
                        data = await resposta.Content.ReadAsStringAsync();
                }
            }
            catch (TaskCanceledException e)
            {
                throw;
            }

            return data;
        }
    }
}
