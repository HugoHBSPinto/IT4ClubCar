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
using System.Threading;

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
            set
            {
                _irParaCampoInformacoesCommand = value;
            }
        }

        private ICommand _irParaCampoInformacoesCommand;
        public ICommand IrParaCampoInformacoesCommand
        {
            get
            {
                if (_irParaCampoInformacoesCommand == null)
                    _irParaCampoInformacoesCommand = new Command(async p => await IrParaCampoInformacoes(), p => { return true; });
                return _irParaCampoInformacoesCommand;
            }
            set
            {
                _irParaCampoInformacoesCommand = value;
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
            set
            {
                _voltarParaJogoCommand = value;
            }
        }



        public MenuPrincipalViewModel(INavigationService navegationService, IDialogService dialogService) : base(navegationService,dialogService)
        {
            
        }



        private async Task IrParaJogoConfiguracao()
        {
            await base.NavigationService.IrParaJogoConfiguracao();
            LimparMemoria();
        }



        private async Task IrParaCampoInformacoes()
        {
            await base.NavigationService.IrParaCampoInformacoes();
            LimparMemoria();
        }



        private async Task VoltarParaJogo()
        {
            await base.NavigationService.IrParaPaginaAnterior();
            LimparMemoria();
        }



        protected override void LimparMemoria()
        {
            IrParaJogoConfiguracaoCommand = null;
            IrParaCampoInformacoesCommand = null;
            VoltarParaJogoCommand = null;
            base.LimparMemoria();
        }

    }
}
