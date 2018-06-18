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
using Newtonsoft.Json.Linq;

namespace IT4ClubCar.IT4ClubCar.Services.Buracos
{
    class BuracosService : IBuracosService
    {
        private WebService _webService;



        public BuracosService(WebService webService)
        {
            _webService = webService;
        }



        /// <summary>
        /// Obtém todos os buracos de uma determinado campo.
        /// </summary>
        /// <param name="campo">Campo do qual se quer obter os buracos.</param>
        /// <returns>ObservableCollection<BuracoWrapperViewModel> com os buracos do campo.</returns>
        public async Task<ObservableCollection<BuracoWrapperViewModel>> ObterBuracosDeCampo(CampoWrapperViewModel campo)
        {
            string dataJson = await _webService.GetStringJson("GetBuracosCampo&IdCampo="+campo.Id);

            JArray buracos = JArray.Parse(dataJson);

            List<BuracoModel> buracosModel = new List<BuracoModel>();

            for (int i = 0; i < buracos.Count; i++)
            {
                int id = int.Parse(buracos[i]["Id"].ToString());
                int numero = int.Parse(buracos[i]["Numero"].ToString());
                int par = int.Parse(buracos[i]["Par"].ToString());
                int strokeIndex = int.Parse(buracos[i]["StrokeIndex"].ToString());
                string dica = buracos[i]["Dica"].ToString();

                buracosModel.Add(new BuracoModel(id,numero, par, strokeIndex, dica));
            }

            return new ObservableCollection<BuracoWrapperViewModel>(buracosModel.Select(p => new BuracoWrapperViewModel(p)));
        }



    }
}
