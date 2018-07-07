using IT4ClubCar.IT4ClubCar.Services.DataAccess;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace IT4ClubCar.IT4ClubCar.Services.TelemovelService
{
    class TelemovelService : ITelemovelService
    {
        private WebService _webService;



        public TelemovelService(WebService webService)
        {
            _webService = webService;
        }



        /// <summary>
        /// Envia uma mensagem para um determinado número de telemóvel.
        /// </summary>
        /// <param name="numeroTelemovelDestino">Número para o qual enviar a mensagem.</param>
        /// <param name="mensagemAEnviar">Mensagem a ser enviada.</param>
        public async Task EnviarSMS(string numeroTelemovelDestino, string mensagemAEnviar)
        {
            string SID = await ObterSID();
            string authToken = await ObterAuthToken();
            string numeroTelemovelOrigem = await ObterNumeroDeOndeEnviar();

            TwilioClient.Init(SID,authToken);

            var message = await MessageResource.CreateAsync(
                body: mensagemAEnviar,
                from: new Twilio.Types.PhoneNumber(numeroTelemovelOrigem),
                to: new Twilio.Types.PhoneNumber(numeroTelemovelDestino)
            );
        }



        /// <summary>
        /// Obtém o número de onde se pode enviar SMS.
        /// </summary>
        /// <returns>String com o número de telemóvel.</returns>
        public async Task<string> ObterNumeroDeOndeEnviar()
        {
            string dataJson = await _webService.GetStringJson("GetNumeroTelemovelTwilio");

            JObject numeroTelemovelObject = JObject.Parse(dataJson);

            string numeroTelemovel = numeroTelemovelObject["NumeroTelemovel"].ToString();

            return numeroTelemovel;
        }



        /// <summary>
        /// Obtém o token de autenticação para enviar SMS.
        /// </summary>
        /// <returns>String com o token de autenticação.</returns>
        public async Task<string> ObterSID()
        {
            string dataJson = await _webService.GetStringJson("GetSIDTwilio");

            JObject SIDObject = JObject.Parse(dataJson);

            string SID = SIDObject["SID"].ToString();

            return SID;
        }



        /// <summary>
        /// Obtém o SID da conta Twilio para enviar SMS.
        /// </summary>
        /// <returns>String com o SID.</returns>
        public async Task<string> ObterAuthToken()
        {
            string dataJson = await _webService.GetStringJson("GetAuthTokenTwilio");

            JObject authTokenObject = JObject.Parse(dataJson);

            string authToken = authTokenObject["AuthToken"].ToString();

            return authToken;
        }
        

    }
}
