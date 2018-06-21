using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                //Adicionar o novo campo.
                camposModel.Add(new CampoModel(id, nome,par,slopeRating,numeroBuracos));
            }

            return new ObservableCollection<CampoWrapperViewModel>(camposModel.Select(p => new CampoWrapperViewModel(p)));
        }
        
        
        
    }
}
