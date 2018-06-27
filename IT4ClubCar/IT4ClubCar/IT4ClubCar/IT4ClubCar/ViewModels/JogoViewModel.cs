using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace IT4ClubCar.IT4ClubCar.ViewModels
{
    class JogoViewModel : BaseViewModel
    {
        private JogoWrapperViewModel _jogo;
        public JogoWrapperViewModel Jogo
        {
            get
            {
                return _jogo;
            }
            set
            {
                _jogo = value;
                OnPropertyChanged("Jogo");
            }
        }

        private BuracoWrapperViewModel _buracoAtual;
        public BuracoWrapperViewModel BuracoAtual
        {
            get
            {
                return _buracoAtual;
            }
            set
            {
                _buracoAtual = value;
                OnPropertyChanged("BuracoAtual");
            }
        }

        private ObservableCollection<TeeWrapperViewModel> _teesUsados;
        public ObservableCollection<TeeWrapperViewModel> TeesUsados
        {
            get
            {
                return _teesUsados;
            }
            set
            {
                _teesUsados = value;
                OnPropertyChanged("TeesUsados");
            }
        }

        private Map _mapa;
        public Map Mapa
        {
            get
            {
                return _mapa;
            }
            set
            {
                _mapa = value;
                OnPropertyChanged("Mapa");
            }
        }

        private ICommand _irParaBuracoAnteriorCommand;
        public ICommand IrBuracoAnteriorCommand
        {
            get
            {
                if (_irParaBuracoAnteriorCommand == null)
                    _irParaBuracoAnteriorCommand = new Command(p => IrParaBuracoAnterior(), p => { return true; });
                return _irParaBuracoAnteriorCommand;
            }
        }

        private ICommand _irParaBuracoSeguinteCommand;
        public ICommand IrBuracoSeguinteCommand
        {
            get
            {
                if (_irParaBuracoSeguinteCommand == null)
                    _irParaBuracoSeguinteCommand = new Command(p => IrParaBuracoSeguinte(), p => { return true; });
                return _irParaBuracoSeguinteCommand;
            }
        }

        private ICommand _verProTipCommand;
        public ICommand VerProTipCommand
        {
            get
            {
                if (_verProTipCommand == null)
                    _verProTipCommand = new Command(async p => await VerProTip(), p => { return true; });
                return _verProTipCommand;
            }
        }

        private ICommand _definirPontuacoesCommand;
        public ICommand DefinirPontuacoesCommand
        {
            get
            {
                if (_definirPontuacoesCommand == null)
                    _definirPontuacoesCommand = new Command(async p => await DefinirPontuacoes(), p => { return true; });
                return _definirPontuacoesCommand;
            }
        }

        private ICommand _verScorecardCommand;
        public ICommand VerScorecardCommand
        {
            get
            {
                if (_verScorecardCommand == null)
                    _verScorecardCommand = new Command(async p => await VerScorecard(), p => { return true; });
                return _verScorecardCommand;
            }
        }

        private ICommand _pedirBuggyBarCommand;
        public ICommand PedirBuggyBarCommand
        {
            get
            {
                if (_pedirBuggyBarCommand == null)
                    _pedirBuggyBarCommand = new Command(async p => await PedirBuggyBar(), p => { return true; });
                return _pedirBuggyBarCommand;
            }
        }

        private ICommand _abrirMenuCommand;
        public ICommand AbrirMenuCommand
        {
            get
            {
                if (_abrirMenuCommand == null)
                    _abrirMenuCommand = new Command(async p => await AbrirMenu(), p => { return true; });
                return _abrirMenuCommand;
            }
        }



        public JogoViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService,dialogService)
        {
            InicializarComunicacaoMediadorMensagens();
            InicializarPropriedadeMapa();
        }



        /// <summary>
        /// Inicializa a propriedade Mapa.
        /// </summary>
        private void InicializarPropriedadeMapa()
        {   
            Mapa = new Map();
            Mapa.IsShowingUser = false;
            Mapa.MapType = MapType.Satellite;
            Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.145206, -8.359953), new Distance(550)));
        }



        /// <summary>
        /// Regista o ViewModel às mensagens necessárias do MediadorMensagensService.
        /// </summary>
        private void InicializarComunicacaoMediadorMensagens()
        {
            //Jogo enviado.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.NovoJogo, p => InicializarJogo((JogoWrapperViewModel)p));
        }



        /// <summary>
        /// Define a propriedade Jogo e atualiza a propriedade Buraco com o valor do primeiro buraco do campo do jogo.
        /// </summary>
        private void InicializarJogo(JogoWrapperViewModel jogo)
        {
            Jogo = jogo;
            BuracoAtual = Jogo.Campo.Buracos[0];
            TeesUsados = new ObservableCollection<TeeWrapperViewModel>();
            Jogo.Jogadores.ToList().ForEach(p => TeesUsados.Add(p.Tee));
        }



        /// <summary>
        /// Se possível altera o BuracoAtual para o valor do buraco anterior.
        /// </summary>
        private void IrParaBuracoAnterior()
        {
            //Verifica-se se o número do buraco atual é diferente de 1, ou seja, o buraco atual não é o primeiro.
            //Se não for pode-se ver o buraco anterior.
            if (!BuracoAtual.Numero.Equals(1))
                BuracoAtual = Jogo.Campo.Buracos.Where(p => p.Numero.Equals((BuracoAtual.Numero-1))).FirstOrDefault();
        }
        


        /// <summary>
        /// Se possível altera o buracoAtual para o buraco seguinte.
        /// </summary>
        private void IrParaBuracoSeguinte()
        {
            //Verifica-se se o numero do BuracoAtual é diferente do número de buracos do campo, ou seja, se não é o último buraco.
            //Se não for pode-se ver o buraco seguinte.
            int numeroBuracos = Jogo.Campo.NumeroBuracos;
            if(!BuracoAtual.Numero.Equals(numeroBuracos))
                BuracoAtual = Jogo.Campo.Buracos.Where(p => p.Numero.Equals((BuracoAtual.Numero+1))).FirstOrDefault();
        }



        /// <summary>
        /// Mostra um popup com a pro tip do BuracoAtual.
        /// </summary>
        private async Task VerProTip()
        {
            await base.NavigationService.IrParaProTip();
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.ProTipConteudo, BuracoAtual.Dica);
        }



        /// <summary>
        /// Mostra um popup onde os utilizadores podem definir os pontos.
        /// </summary>
        private async Task DefinirPontuacoes()
        {
            await base.NavigationService.IrParaPontuacoes();

            //Passar jogadores existentes para serem definidos.
            foreach (JogadorWrapperViewModel jogador in Jogo.Jogadores)
                MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogadorPontuacaoAEditar, jogador);

            //Passar Buraco cujas pontuações devem ser definidas.
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.BuracoPontuacaoAEditar, BuracoAtual);
        }



        /// <summary>
        /// Mostra um popup com um scorecard onde se pode ver os pontos.
        /// </summary>
        private async Task VerScorecard()
        {
            await base.NavigationService.IrParaScorecard();

            foreach (JogadorWrapperViewModel jogador in Jogo.Jogadores)
            {
                MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.PontuacaoAMostrar, jogador);
                MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.NomeAMostrar, jogador);
            }
        }



        /// <summary>
        /// Mostra um popup onde o jogador pode pedir o buggybar para um determinado buraco.
        /// </summary>
        /// <returns></returns>
        private async Task PedirBuggyBar()
        {
            await base.NavigationService.IrParaPedirBuggyBar();

            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.CampoAtual, Jogo.Campo);
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.BuracoAtual, BuracoAtual);
        }



        private async Task AbrirMenu()
        {
            await base.NavigationService.IrParaMenuJogo();
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogoAtual, Jogo);
        }

    }
}
