using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.TelemovelService
{
    interface ITelemovelService
    {
        /// <summary>
        /// Envia uma mensagem para um determinado número de telemóvel.
        /// </summary>
        /// <param name="numeroTelemovelDestino">Número para o qual enviar a mensagem.</param>
        /// <param name="mensagem">Mensagem a ser enviada.</param>
        Task EnviarSMS(string numeroTelemovelDestino, string mensagem);

        /// <summary>
        /// Obtém o número de onde se pode enviar SMS.
        /// </summary>
        /// <returns>String com o número de telemóvel.</returns>
        Task<string> ObterNumeroDeOndeEnviar();

        /// <summary>
        /// Obtém o SID da conta Twilio para enviar SMS.
        /// </summary>
        /// <returns>String com o SID.</returns>
        Task<string> ObterSID();

        /// <summary>
        /// Obtém o token de autenticação para enviar SMS.
        /// </summary>
        /// <returns>String com o token de autenticação.</returns>
        Task<string> ObterAuthToken();
    }
}
