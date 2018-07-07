using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.Services.DataAccess;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Genero
{
    class GeneroService : IGeneroService
    {
        private WebService _webService;



        public GeneroService(WebService webService)
        {
            _webService = webService;
        }



        /// <summary>
        /// Obtém o id do genero definido como default na BD.
        /// </summary>
        /// <returns>int que representa o id do genero default.</returns>
        public async Task<int> ObterGeneroDefault()
        {
            string dataJson = await _webService.GetStringJson("GetGeneroDefault");

            JObject genero = JObject.Parse(dataJson);

            int id = int.Parse(genero["Id"].ToString());

            return id;
        }



        /// <summary>
        /// Obtém todos os generos existentes.
        /// </summary>
        /// <returns>ObservableCollection<GeneroWrapperViewModel> com os generos existentes.</returns>
        public async Task<ObservableCollection<GeneroWrapperViewModel>> ObterGenerosDisponiveis()
        {
            string dataJson = await _webService.GetStringJson("GetGeneros");

            JArray generos = JArray.Parse(dataJson);

            List<GeneroModel> generosModel = new List<GeneroModel>();

            for (int i = 0; i < generos.Count; i++)
            {
                int id = int.Parse(generos[i]["Id"].ToString());
                string nome = generos[i]["Nome"].ToString();
                generosModel.Add(new GeneroModel(id,nome));
            }

            return new ObservableCollection<GeneroWrapperViewModel>(generosModel.Select(p => new GeneroWrapperViewModel(p)));
        }



    }
}
