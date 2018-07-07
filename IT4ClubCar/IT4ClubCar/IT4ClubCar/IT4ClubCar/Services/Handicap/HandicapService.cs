using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.Services.DataAccess;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Buracos
{
    class HandicapService : IHandicapService
    {
        private WebService _webService;



        public HandicapService(WebService webService)
        {
            _webService = webService;
        }



        /// <summary>
        /// Obtém o Handicap definido como default na BD.
        /// </summary>
        /// <returns>HandicapWrapperViewModel que representa o handicap default.</returns>
        public async Task<HandicapWrapperViewModel> ObterHandicapDefault()
        {
            string dataJson = await _webService.GetStringJson("GetHandicapDefault");

            JObject handicap = JObject.Parse(dataJson);

            int valor = int.Parse(handicap["Valor"].ToString());

            HandicapModel handicapModel = new HandicapModel(valor);

            return new HandicapWrapperViewModel(handicapModel);
        }



        /// <summary>
        /// Obtém o Handicap máximo possível, definido na BD.
        /// </summary>
        /// <returns>HandicapWrapperViewModel que representa o handicap mínimo.</returns>
        public async Task<HandicapWrapperViewModel> ObterHandicapMinimo()
        {
            string dataJson = await _webService.GetStringJson("GetHandicapMinimo");

            JObject handicap = JObject.Parse(dataJson);

            int valor = int.Parse(handicap["Valor"].ToString());

            HandicapModel handicapModel = new HandicapModel(valor);

            return new HandicapWrapperViewModel(handicapModel);
        }



        /// <summary>
        /// Obtém o Handicap mínimo possível, definido na BD.
        /// </summary>
        /// <returns>HandicapWrapperViewModel que representa o handicap mínimo.</returns>
        public async Task<HandicapWrapperViewModel> ObterHandicapMaximo()
        {
            string dataJson = await _webService.GetStringJson("GetHandicapMaximo");

            JObject handicap = JObject.Parse(dataJson);

            int valor = int.Parse(handicap["Valor"].ToString());

            HandicapModel handicapModel = new HandicapModel(valor);

            return new HandicapWrapperViewModel(handicapModel);
        }
    }
}
