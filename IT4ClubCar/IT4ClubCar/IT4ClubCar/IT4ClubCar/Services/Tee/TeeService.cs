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

namespace IT4ClubCar.IT4ClubCar.Services.Tee
{
    class TeeService : ITeeService
    {
        private WebService _webService;



        public TeeService(WebService webService)
        {
            _webService = webService;
        }



        /// <summary>
        /// Obtém o id do Tee definido como Starting Tee na BD.
        /// </summary>
        /// <returns>int que representa o id do starting tee default</returns>
        public async Task<int> ObterStartingTeeDefault()
        {
            string dataJson = await _webService.ObterDadosJson("GetStartingTeeDefault");

            JObject tee = JObject.Parse(dataJson);

            int id = int.Parse(tee["Id"].ToString());

            return id;
        }



        /// <summary>
        /// Obtém o id do Tee definido como default na BD.
        /// </summary>
        /// <returns>int que representa o id do tee default.</returns>
        public async Task<int> ObterTeeDefault()
        {
            string dataJson = await _webService.ObterDadosJson("GetTeeDefault");

            JObject tee = JObject.Parse(dataJson);

            int id = int.Parse(tee["Id"].ToString());

            return id;
        }



        /// <summary>
        /// Obtém todos os tees existentes.
        /// </summary>
        /// <returns>ObservableCollection<TeeWrapperViewModel> com os tees existentes.</returns>
        public async Task<ObservableCollection<TeeWrapperViewModel>> ObterTeesExistentes()
        {
            string dataJson = await _webService.ObterDadosJson("GetTees");

            JArray tees = JArray.Parse(dataJson);

            List<TeeModel> teesModel = new List<TeeModel>();

            for(int i = 0; i < tees.Count; i++)
            {
                int id = int.Parse(tees[i]["Id"].ToString());
                string nome = tees[i]["Nome"].ToString();
                teesModel.Add(new TeeModel(id, nome));
            }

            return new ObservableCollection<TeeWrapperViewModel>(teesModel.Select(p => new TeeWrapperViewModel(p)));
        }
    }
}
