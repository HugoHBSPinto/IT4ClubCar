using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.TeeDistancias
{
    interface ITeeDistanciaService
    {
        /// <summary>
        /// Obtém todas as distâncias entre um tee e os buracos de um campo.
        /// </summary>
        /// <param name="tee">Tee do qual se quer obter as distâncias.</param>
        /// <returns></returns>
        Task<TeeBuracoDistanciaWrapperViewModel> ObterDistancias(BuracoWrapperViewModel buraco, TeeWrapperViewModel tee);
    }
}
