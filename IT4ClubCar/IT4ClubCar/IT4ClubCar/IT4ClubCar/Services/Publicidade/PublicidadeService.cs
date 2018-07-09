using IT4ClubCar.IT4ClubCar.Services.Camera;
using IT4ClubCar.IT4ClubCar.Services.DataAccess;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Services.Publicidade
{
    class PublicidadeService : IPublicidadeService
    {
        private WebService _webService;



        public PublicidadeService(WebService webService)
        {
            _webService = webService;
        }


        /// <summary>
        /// Obtém a publicidade do dia.
        /// </summary>
        /// <returns>PublicidadeWrapperViewModel com a publicidade do dia.</returns>
        public async Task<PublicidadeWrapperViewModel> ObterPublicidadeDoDia()
        {
            string dataJson = await _webService.ObterDadosJson("GetPublicidadeDoDia");

            JObject publicidade = JObject.Parse(dataJson);

            string titulo = publicidade["Titulo"].ToString();
            string descricao = publicidade["Descricao"].ToString();
            string base64 = publicidade["Imagem"].ToString();
            ImageSource imagemSource = BytesHandlerHelper.ConverterBase64EmImageSource(base64);

            return new PublicidadeWrapperViewModel(new Models.PublicidadeModel(titulo,descricao,imagemSource));
        }
        
    }
}
