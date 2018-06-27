using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Publicidade
{
    interface IPublicidadeService
    {
        /// <summary>
        /// Obtém a publicidade do dia.
        /// </summary>
        /// <returns>PublicidadeWrapperViewModel com a publicidade do dia.</returns>
        Task<PublicidadeWrapperViewModel> ObterPublicidadeDoDia();
    }
}
