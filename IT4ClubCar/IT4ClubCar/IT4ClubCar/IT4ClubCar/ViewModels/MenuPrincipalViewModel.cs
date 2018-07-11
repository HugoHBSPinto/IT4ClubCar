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
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;

namespace IT4ClubCar.IT4ClubCar.ViewModels
{
    class MenuPrincipalViewModel : BaseViewModel
    {
        private JogoWrapperViewModel _jogoPausado;

        #region Commands
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

        private ICommand _irParaVerTempoCommand;
        public ICommand IrParaVerTempoCommand
        {
            get
            {
                if (_irParaVerTempoCommand == null)
                    _irParaVerTempoCommand = new Command(async p => await IrParaVerTempo(), p => { return true; });
                return _irParaVerTempoCommand;
            }
            set
            {
                _irParaVerTempoCommand = value;
            }
        }

        private ICommand _voltarParaJogoCommand;
        public ICommand VoltarParaJogoCommand
        {
            get
            {
                if (_voltarParaJogoCommand == null)
                    _voltarParaJogoCommand = new Command(async p => await VoltarParaJogo(), p => { return _jogoPausado != null; });
                return _voltarParaJogoCommand;
            }
            set
            {
                _voltarParaJogoCommand = value;
            }
        }
        #endregion



        public MenuPrincipalViewModel(INavigationService navegationService, IDialogService dialogService) : base(navegationService,dialogService)
        {
            InicializarComunicacaoComViewModel();
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do MediadorMensagens.
        /// </summary>
        private void InicializarComunicacaoComViewModel()
        {
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogoPausado,p => DefinirJogoPausado(p as JogoWrapperViewModel));
        }



        /// <summary>
        /// Define a propriedade JogoPausado.
        /// </summary>
        /// <param name="jogo">Jogo com o qual se vai definir a propriedade JogoPausado.</param>
        private void DefinirJogoPausado(JogoWrapperViewModel jogo)
        {
            if (jogo == null)
                return;

            _jogoPausado = jogo;

            ((Command)VoltarParaJogoCommand).ChangeCanExecute();
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



        private async Task IrParaVerTempo()
        {
            await base.NavigationService.IrParaVerTempo();
            LimparMemoria();
        }



        private async Task VoltarParaJogo()
        {
            await base.NavigationService.IrParaJogo();
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.RecomecarJogo, _jogoPausado);
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
