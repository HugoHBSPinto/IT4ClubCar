using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IT4ClubCar.IT4ClubCar.Excepcoes;
using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.Services.DataAccess;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IT4ClubCar.IT4ClubCar.Services.Campo
{
    class CampoService : ICampoService
    {
        private WebService _webService;


        public CampoService(WebService webService)
        {
            _webService = webService;
        }



        /// <summary>
        /// Obtém o id do campo definido como default na BD.
        /// </summary>
        /// <returns>int que representa o id do campo default.</returns>
        public async Task<int> ObterCampoDefault()
        {
            string dataJson = await _webService.GetStringJson("GetCampoDefault");

            JObject idCampo = JObject.Parse(dataJson);

            int id = int.Parse(idCampo["Id"].ToString());

            return id;
        }



        /// <summary>
        /// Obtém todos os campos disponíveis.
        /// </summary>
        /// <returns>ObservableCollection<CampoWrapperViewModel> só preenchidos com o id e o nome.</returns>
        public async Task<ObservableCollection<CampoWrapperViewModel>> ObterCamposDisponiveis()
        {
            ObservableCollection<CampoWrapperViewModel> camposDisponiveis = new ObservableCollection<CampoWrapperViewModel>();

            string dataJson = await _webService.GetStringJson("GetCampos");

            JArray campos = JArray.Parse(dataJson);

            List<CampoModel> camposModel = new List<CampoModel>();

            for (int i = 0; i < campos.Count; i++)
            {
                //Obter Propriedades.
                int id = int.Parse(campos[i]["Id"].ToString());
                string nome = campos[i]["Nome"].ToString();
                int par = int.Parse(campos[i]["Par"].ToString());
                int slopeRating = int.Parse(campos[i]["SlopeRating"].ToString());
                int numeroBuracos = int.Parse(campos[i]["NumeroBuracos"].ToString());
                int distancia = int.Parse(campos[i]["Distancia"].ToString());
                string arquitecto = campos[i]["Arquitecto"].ToString();
                string localizacao = campos[i]["Localizacao"].ToString();
                string iconBase64 = campos[i]["Icon"].ToString();
                string imagemAmostraBase64 = campos[i]["ImagemAmostra"].ToString();

                //Adicionar o novo campo.
                camposModel.Add(new CampoModel(id, nome, par, slopeRating, numeroBuracos,distancia,arquitecto,localizacao,iconBase64,imagemAmostraBase64));
            }

            camposModel.ForEach(p => camposDisponiveis.Add(new CampoWrapperViewModel(p)));

            return camposDisponiveis;
        }
        
        
        
    }
}
