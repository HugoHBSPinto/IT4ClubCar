using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.DataAccess
{
    /// <summary>
    /// Classe que permite pedir dados a APIS.
    /// </summary>
    class BaseWebService
    {
        private static HttpClient _cliente = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(2)
        };



        public BaseWebService() { }



        /// <summary>
        ///Obtém uma string que contém os dados requisitados podendo ser convertida num JArray.
        /// </summary>
        /// <param name="url">URL a pedir os dados.</param>
        /// <param name="dadosRequisitados">Dados a serem obtidos.</param>
        /// <param name="token">Token a ser associado com este pedido.</param>
        /// <returns>String que pode ser convertida em json.</returns>
        public async Task<string> GetStringJson(string url,string dadosRequisitados, CancellationToken token)
        {
            string data = "";

            string pedido = url + dadosRequisitados;

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



        /// <summary>
        ///Atualiza os dados de um registo através de um endereço.
        /// </summary>
        /// <param name="url">URL que permite inserir dados.</param>
        /// <param name="dadosRequisitados">Dados a serem inseridos.</param>
        /// <param name="token">Token a ser associado com este pedido.</param>
        public async Task InserirDados(string url, string dadosAInserir, CancellationToken token)
        {
            string pedido = url + dadosAInserir;

            try
            {
                using (HttpResponseMessage resposta = await _cliente.GetAsync(requestUri: pedido, cancellationToken: token))
                {
                    //if (resposta.IsSuccessStatusCode)
                    //    data = await resposta.Content.ReadAsStringAsync();
                }
            }
            catch (TaskCanceledException e)
            {
                throw;
            }
        }

    }
}
