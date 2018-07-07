using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.Services.DataAccess;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.TeeDistancias
{
    class TeeDistanciaService : ITeeDistanciaService
    {
        private WebService _webService;



        public TeeDistanciaService(WebService webService)
        {
            _webService = webService;
        }



        /// <summary>
        /// Obtém todas as distâncias entre um tee e os buracos de um campo.
        /// </summary>
        /// <param name="tee">Tee do qual se quer obter as distâncias.</param>
        /// <returns></returns>
        public async Task<TeeBuracoDistanciaWrapperViewModel> ObterDistancias(BuracoWrapperViewModel buraco, TeeWrapperViewModel tee)
        {
            string dataJson = await _webService.GetStringJson("GetTeeBuracoDistancia&IdBuraco="+buraco.Id+"&IdTee="+tee.Id);

            if (dataJson.Equals("[]"))
                return null;

            JObject teeBuracoDistancia = JObject.Parse(dataJson);

            int distancia = int.Parse(teeBuracoDistancia["Distancia"].ToString());
            float latitude = float.Parse(teeBuracoDistancia["Latitude"].ToString());
            float longitude = float.Parse(teeBuracoDistancia["Longitude"].ToString());

            TeeBuracoDistanciaModel teeBuracoDistanciaModel = new TeeBuracoDistanciaModel(buraco.ObterModelo(),latitude,longitude,distancia);

            return new TeeBuracoDistanciaWrapperViewModel(teeBuracoDistanciaModel);
        }

    }
}
