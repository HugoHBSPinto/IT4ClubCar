using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.Dialog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System.Diagnostics;
using IT4ClubCar.IT4ClubCar.Services.ScreenshotService;

namespace IT4ClubCar.IT4ClubCar.ViewModels
{
    class MenuPrincipalViewModel : BaseViewModel
    {
        private ICommand _irParaJogoConfiguracao;
        public ICommand IrParaJogoConfiguracao
        {
            get
            {
                if (_irParaJogoConfiguracao == null)
                    _irParaJogoConfiguracao = new Command(async p => await base.NavigationService.IrParaJogoConfiguracao(), p => { return true; });
                return _irParaJogoConfiguracao;
            }
        }

        private ICommand _voltarParaJogoCommand;
        public ICommand VoltarParaJogoCommand
        {
            get
            {
                if (_voltarParaJogoCommand == null)
                    _voltarParaJogoCommand = new Command(async p => await base.NavigationService.IrParaPaginaAnterior(), p => { return true; });
                return _voltarParaJogoCommand;
            }
        }



        public MenuPrincipalViewModel(INavigationService navegationService, IDialogService dialogService) : base(navegationService,dialogService)
        {
            
        }

    }
}
