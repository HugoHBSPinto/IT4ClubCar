using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Navegacao
{
    /// <summary>
    /// Intreface que permite navegar entre as p�ginas da aplica��o.
    /// </summary>
    interface INavigationService
    {
        Task IrParaPaginaAnterior();
        Task IrParaMenuPrincipal();
    }
}