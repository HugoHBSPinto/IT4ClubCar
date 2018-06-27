using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.BuggyBar
{
    interface IBuggyBarService
    {
        /// <summary>
        /// Obtém o número de telemóvel do BuggyBar.
        /// </summary>
        /// <returns>String com o número do BuggyBar.</returns>
        Task<string> ObterNumeroTelemovel();
    }
}
