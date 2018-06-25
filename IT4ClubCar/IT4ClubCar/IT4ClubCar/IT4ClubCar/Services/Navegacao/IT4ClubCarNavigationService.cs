using IT4ClubCar.IT4ClubCar.Interfaces;
using IT4ClubCar.IT4ClubCar.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using IT4ClubCar.IT4ClubCar.Views.Popups;
using System.Diagnostics;

namespace IT4ClubCar.IT4ClubCar.Services.Navegacao
{
    /// <summary>
    /// Classe que implementa a interface INavegationService, oferencendo métodos para navegar entre as páginas da aplicação.
    /// </summary>
    class IT4ClubCarNavigationService : INavigationService
    {
        public async Task IrParaPaginaAnterior()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }



        public async Task IrParaMenuPrincipal()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new MenuPrincipalView());
        }



        public async Task IrParaJogoConfiguracao()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new JogoConfiguracaoView());
        }



        public async Task IrParaEditarJogador()
        {
            await PopupNavigation.PushAsync(new EditarJogadorPopupView(),true);
        }



        public async Task SairDeEditarJogador()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaJogo()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new JogoView());
        }



        public async Task IrParaProTip()
        {
            await PopupNavigation.PushAsync(new ProTipPopupView(), true);
        }



        public async Task SairDeProTip()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaPontuacoes()
        {
            await PopupNavigation.PushAsync(new  PontuacoesPopupView(), true);
        }



        public async Task SairDePontuacoes()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaScorecard()
        {
            await PopupNavigation.PushAsync(new ScorecardPopupView(), true);
        }



        public async Task SairDeScorecard()
        {
            await PopupNavigation.PopAsync();
        }

    }
}
