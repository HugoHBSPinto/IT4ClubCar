using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Campo
{
    interface ICampoService
    {
        /// <summary>
        /// Obtém o id do campo definido como default na BD.
        /// </summary>
        /// <returns>int que representa o id do campo default.</returns>
        Task<int> ObterCampoDefault();



        /// <summary>
        /// Obtém todos os campos disponíveis.
        /// </summary>
        /// <returns>ObservableCollection<CampoWrapperViewModel> só preenchidos com o id e o nome.</returns>
        Task<ObservableCollection<CampoWrapperViewModel>> ObterCamposDisponiveis();
    }
}
