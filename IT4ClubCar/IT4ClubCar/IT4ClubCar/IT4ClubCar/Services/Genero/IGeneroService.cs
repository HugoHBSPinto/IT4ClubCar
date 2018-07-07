using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Genero
{
    interface IGeneroService
    {
        /// <summary>
        /// Obtém o id do genero definido como default na BD.
        /// </summary>
        /// <returns>int que representa o id do genero default.</returns>
        Task<int> ObterGeneroDefault();



        /// <summary>
        /// Obtém todos os generos existentes.
        /// </summary>
        /// <returns>ObservableCollection<GeneroWrapperViewModel> com os generos existentes.</returns>
        Task<ObservableCollection<GeneroWrapperViewModel>> ObterGenerosDisponiveis();
    }
}
