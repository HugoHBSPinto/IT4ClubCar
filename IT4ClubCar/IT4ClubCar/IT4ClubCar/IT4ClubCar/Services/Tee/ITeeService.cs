using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Tee
{
    interface ITeeService
    {
        /// <summary>
        /// Obtém o id do Tee definido como default na BD.
        /// </summary>
        /// <returns>int que representa o id do tee default.</returns>
        Task<int> ObterTeeDefault();


        /// <summary>
        /// Obtém o id do Tee definido como Starting Tee na BD.
        /// </summary>
        /// <returns>int que representa o id do starting tee default</returns>
        Task<int> ObterStartingTeeDefault();

        /// <summary>
        /// Obtém todos os tees existentes.
        /// </summary>
        /// <returns>ObservableCollection<TeeWrapperViewModel> com os tees existentes.</returns>
        Task<ObservableCollection<TeeWrapperViewModel>> ObterTeesExistentes();
    }
}
