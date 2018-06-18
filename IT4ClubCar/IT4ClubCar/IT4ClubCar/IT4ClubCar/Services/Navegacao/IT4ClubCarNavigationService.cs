using IT4ClubCar.IT4ClubCar.Interfaces;
using IT4ClubCar.IT4ClubCar.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using IT4ClubCar.IT4ClubCar.Views;

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
    }
}
