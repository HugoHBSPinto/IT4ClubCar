using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.Dialog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System.Diagnostics;

namespace IT4ClubCar.IT4ClubCar.ViewModels
{
    class MenuPrincipalViewModel : BaseViewModel
    {
        public MenuPrincipalViewModel(INavigationService navegationService, IDialogService dialogService) : base(navegationService,dialogService)
        {
            
        }



        #region Commands

        private ICommand _irParaJogoConfiguracao;
        public ICommand IrParaJogoConfiguracao
        {
            get
            {
                if (_irParaJogoConfiguracao == null)
                    _irParaJogoConfiguracao = new Command(p => base.NavigationService.IrParaJogoConfiguracao(), p => { return true; });
                return _irParaJogoConfiguracao;
            }
        }

        #endregion
    }
}
