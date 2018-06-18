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

namespace IT4ClubCar.IT4ClubCar.Services.ModoJogo
{
    class ModoJogoService : IModoJogoService
    {
        private WebService _webService;



        public ModoJogoService(WebService webService)
        {
            _webService = webService;
        }



        /// <summary>
        /// Obtém o id do ModoJogo definido como default na BD.
        /// </summary>
        /// <returns>int que representa o id do ModoJogo default.</returns>
        public async Task<int> ObterModoJogoDefault()
        {
            string dataJson = await _webService.GetStringJson("GetModoJogoDefault");

            JObject modoJogo = JObject.Parse(dataJson);

            int id = int.Parse(modoJogo["Id"].ToString());

            return id;
        }




        /// <summary>
        /// Obtém todos os campos disponíveis.
        /// </summary>
        /// <returns>ObservableCollection<CampoWrapperViewModel> só preenchidos com o id e o nome.</returns>
        public async Task<ObservableCollection<ModoJogoWrapperViewModel>> ObterModosJogoDisponiveis()
        {
            string dataJson = await _webService.GetStringJson("GetModosJogo");

            JArray modosJogo = JArray.Parse(dataJson);

            List<ModoJogoModel> modosJogoModel = new List<ModoJogoModel>();

            for(int i = 0; i < modosJogo.Count; i++)
            {
                int id = int.Parse(modosJogo[i]["Id"].ToString());
                string nome = modosJogo[i]["Nome"].ToString();
                modosJogoModel.Add(new ModoJogoModel(id, nome));
            }

            return new ObservableCollection<ModoJogoWrapperViewModel>(modosJogoModel.Select(p => new ModoJogoWrapperViewModel(p)));
        }



    }
}
