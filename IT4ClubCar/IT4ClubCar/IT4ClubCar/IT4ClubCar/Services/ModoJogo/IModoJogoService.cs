using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.ModoJogo
{
    interface IModoJogoService
    {
        /// <summary>
        /// Obtém o id do ModoJogo definido como default na BD.
        /// </summary>
        /// <returns>int que representa o id do ModoJogo default.</returns>
        Task<int> ObterModoJogoDefault();



        /// <summary>
        /// Obtém os modos de jogo disponíveis.
        /// </summary>
        /// <returns>ObservableCollection<ModoJogoWrapperViewModel> com os modos de jogo disponíveis.</returns>
        Task<ObservableCollection<ModoJogoWrapperViewModel>> ObterModosJogoDisponiveis();
    }
}
