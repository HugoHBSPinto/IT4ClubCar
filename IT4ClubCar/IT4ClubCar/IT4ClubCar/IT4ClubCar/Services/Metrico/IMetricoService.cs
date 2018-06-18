using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Metrico
{
    interface IMetricoService
    {
        /// <summary>
        /// Obtém o id do Métrico definido como default na BD.
        /// </summary>
        /// <returns>int que representa o id do Metrico default.</returns>
        Task<int> ObterMetricoDefault();



        /// <summary>
        /// Obtém todos as formas de métrica disponíveis.
        /// </summary>
        /// <returns>ObservableCollection<MetricoWrapperViewModel> com o tipo de métrica disponível.</returns>
        Task<ObservableCollection<MetricoWrapperViewModel>> ObterMetricosDisponiveis();
    }
}
