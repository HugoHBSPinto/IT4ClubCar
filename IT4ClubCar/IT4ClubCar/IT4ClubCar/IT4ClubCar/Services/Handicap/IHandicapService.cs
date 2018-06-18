using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Buracos
{
    interface IHandicapService
    {
        /// <summary>
        /// Obtém o Handicap definido como default na BD.
        /// </summary>
        /// <returns>HandicapWrapperViewModel que representa o handicap default.</returns>
        Task<HandicapWrapperViewModel> ObterHandicapDefault();



        /// <summary>
        /// Obtém o Handicap mínimo possível, definido na BD.
        /// </summary>
        /// <returns>HandicapWrapperViewModel que representa o handicap mínimo.</returns>
        Task<HandicapWrapperViewModel> ObterHandicapMinimo();



        /// <summary>
        /// Obtém o Handicap máximo possível, definido na BD.
        /// </summary>
        /// <returns>HandicapWrapperViewModel que representa o handicap mínimo.</returns>
        Task<HandicapWrapperViewModel> ObterHandicapMaximo();
    }
}
