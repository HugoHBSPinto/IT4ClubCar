using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Buracos
{
    interface IBuracosService
    {
        /// <summary>
        /// Obtém todos os buracos de uma determinado campo.
        /// </summary>
        /// <param name="campo">Campo do qual se quer obter os buracos.</param>
        /// <returns>ObservableCollection<BuracoWrapperViewModel> com os buracos do campo.</returns>
        Task<ObservableCollection<BuracoWrapperViewModel>> ObterBuracosDeCampo(CampoWrapperViewModel campo);
    }
}
