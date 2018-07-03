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
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.ViewModels
{
    class MenuPrincipalViewModel : BaseViewModel
    {
        private ICommand _irParaJogoConfiguracaoCommand;
        public ICommand IrParaJogoConfiguracaoCommand
        {
            get
            {
                if (_irParaJogoConfiguracaoCommand == null)
                    _irParaJogoConfiguracaoCommand = new Command(async p => await IrParaJogoConfiguracao(), p => { return true; });
                return _irParaJogoConfiguracaoCommand;
            }
        }

        private ICommand _voltarParaJogoCommand;
        public ICommand VoltarParaJogoCommand
        {
            get
            {
                if (_voltarParaJogoCommand == null)
                    _voltarParaJogoCommand = new Command(async p => await VoltarParaJogo(), p => { return true; });
                return _voltarParaJogoCommand;
            }
        }



        public MenuPrincipalViewModel(INavigationService navegationService, IDialogService dialogService) : base(navegationService,dialogService)
        {
            
        }



        private async Task IrParaJogoConfiguracao()
        {
            await base.NavigationService.IrParaJogoConfiguracao();
            base.LimparComunicacaoMediadorMensagens();
        }



        private async Task VoltarParaJogo()
        {
            await base.NavigationService.IrParaPaginaAnterior();
            base.LimparComunicacaoMediadorMensagens();
        }
        
    }
}
