using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.Services.DataAccess;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using Newtonsoft.Json.Linq;

namespace IT4ClubCar.IT4ClubCar.Services.Metrico
{
    class MetricoService : IMetricoService
    {
        private WebService _webService;



        public MetricoService(WebService webService)
        {
            _webService = webService;
        }



        /// <summary>
        /// Obtém o id do Métrico definido como default na BD.
        /// </summary>
        /// <returns>int que representa o id do Metrico default.</returns>
        public async Task<int> ObterMetricoDefault()
        {
            string dataJson = await _webService.GetStringJson("GetMetricoDefault");

            JObject metrico = JObject.Parse(dataJson);

            int id = int.Parse(metrico["Id"].ToString());

            return id;
        }



        /// <summary>
        /// Obtém todos as formas de métrica disponíveis.
        /// </summary>
        /// <returns>ObservableCollection<MetricoWrapperViewModel> com o tipo de métrica disponível.</returns>
        public async Task<ObservableCollection<MetricoWrapperViewModel>> ObterMetricosDisponiveis()
        {
            string dataJson = await _webService.GetStringJson("GetMetricos");

            JArray metricos = JArray.Parse(dataJson);

            List<MetricoModel> metricosModel = new List<MetricoModel>();

            for (int i = 0; i < metricos.Count; i++)
            {
                int id = int.Parse(metricos[i]["Id"].ToString());
                string nome = metricos[i]["Nome"].ToString();
                metricosModel.Add(new MetricoModel(id,nome));
            }

            return new ObservableCollection<MetricoWrapperViewModel>(metricosModel.Select(p => new MetricoWrapperViewModel(p)));
        }
    }
}
