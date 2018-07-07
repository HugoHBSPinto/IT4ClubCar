using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Navegacao
{
    /// <summary>
    /// Intreface que permite navegar entre as páginas da aplicação.
    /// </summary>
    interface INavigationService
    {
        Task IrParaPaginaAnterior();
        Task IrParaMenuPrincipal();
        Task IrParaCampoInformacoes();
        Task IrParaJogoConfiguracao();
        Task IrParaEditarJogador();
        Task SairDeEditarJogador();
        Task IrParaJogo();
        Task IrParaProTip();
        Task SairDeProTip();
        Task IrParaPontuacoes();
        Task SairDePontuacoes();
        Task IrParaScorecard();
        Task SairDeScorecard();
        Task IrParaPublicidadeDoDia();
        Task SairDePublicidadeDoDia();
        Task IrParaPedirBuggyBar();
        Task SairDePedirBuggyBar();
        Task IrParaMenuJogo();
        Task SairDeMenuJogo();
        Task IrParaTerminarJogo();
        Task SairDeTerminarJogo();
    }
}