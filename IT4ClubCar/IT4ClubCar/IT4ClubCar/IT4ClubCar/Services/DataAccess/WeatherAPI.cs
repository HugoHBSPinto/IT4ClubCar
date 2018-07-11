using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.DataAccess
{
    class WeatherAPI : BaseWebService
    {

        public WeatherAPI() { }



        /// <summary>
        ///Obtém uma string que contém os dados requisitados podendo ser convertida num JArray.
        /// </summary>
        /// <param name="dadosRequisitados">Dados a serem obtidos</param>
        /// <returns>String que pode ser convertida em json</returns>
        public async Task<string> ObterDadosJson(string apiKey,string dadosRequisitados, CancellationToken token = new CancellationToken())
        {
            return await base.GetStringJson("http://api.openweathermap.org/data/2.5/forecast?units=metric&q=", dadosRequisitados + "&APPID=" + apiKey, token);
        }
    }
}
