using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.DataAccess
{
    class WebService
    {
        public WebService()
        {
            
        }



        /// <summary>
        ///Obtém uma string que contém os dados requisitados podendo ser convertida num JArray.
        /// </summary>
        /// <param name="dadosRequisitados">Dados a serem obtidos</param>
        /// <returns>String que pode ser convertida em json</returns>
        public async Task<string> GetStringJson(string dadosRequisitados)
        {
            string data = "";

            string pedido = "http://127.0.0.1/it4clubcar/it4clubcarWebService.php?pedido=" + dadosRequisitados;

            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    using (HttpResponseMessage resposta = await cliente.GetAsync(pedido))
                    {
                        if (resposta.IsSuccessStatusCode)
                        {
                            data = await resposta.Content.ReadAsStringAsync();
                        }
                    }
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
