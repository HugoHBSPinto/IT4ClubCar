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



        public async Task IrParaCampoInformacoes()
        {
            Application.Current.MainPage = new CampoInformacoesView();
        }



        public async Task IrParaVerTempo()
        {
            Application.Current.MainPage = new VerTempoView();
        }



        public async Task IrParaMenuPrincipal()
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(new MenuPrincipalView());
            Application.Current.MainPage = new MenuPrincipalView();
        }



        public async Task IrParaJogoConfiguracao()
        {
            Application.Current.MainPage = new JogoConfiguracaoView();
            //await Application.Current.MainPage.Navigation.PushModalAsync(new JogoConfiguracaoView());
        }



        public async Task IrParaLogIn()
        {
            await PopupNavigation.PushAsync(new LogInPopupView(), animate: false);
        }



        public async Task SairDeLogIn()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaCriarConta()
        {
            await PopupNavigation.PushAsync(new CriarContaPopupView(), animate: false);
        }



        public async Task SairDeCriarConta()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaEditarJogador()
        {
            await PopupNavigation.PushAsync(new EditarJogadorPopupView(), animate: false);
        }



        public async Task SairDeEditarJogador()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaJogo()
        {
            Application.Current.MainPage = new JogoView();
            //await Application.Current.MainPage.Navigation.PushModalAsync(new JogoView());
        }



        public async Task IrParaProTip()
        {
            await PopupNavigation.PushAsync(new ProTipPopupView(), animate: false);
        }



        public async Task SairDeProTip()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaPontuacoes()
        {
            await PopupNavigation.PushAsync(new  PontuacoesPopupView(), animate: false);
        }



        public async Task SairDePontuacoes()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaScorecard()
        {
            await PopupNavigation.PushAsync(new ScorecardPopupView(), animate: false);
        }



        public async Task SairDeScorecard()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaPublicidadeDoDia()
        {
            await PopupNavigation.PushAsync(new VerPublicidadePopupView(), animate: false);
        }



        public async Task SairDePublicidadeDoDia()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaPedirBuggyBar()
        {
            await PopupNavigation.PushAsync(new PedirBuggyBarPopupView(), animate: false);
        }



        public async Task SairDePedirBuggyBar()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaMenuJogo()
        {
            await PopupNavigation.PushAsync(new MenuJogoPopupView(), animate: false);
        }



        public async Task SairDeMenuJogo()
        {
            await PopupNavigation.PopAsync();
        }



        public async Task IrParaTerminarJogo()
        {
            await PopupNavigation.PushAsync(new TerminarJogoPopupView(), animate: false);
        }



        public async Task SairDeTerminarJogo()
        {
            await PopupNavigation.PopAsync();
        }
        
    }
}
