using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using IT4ClubCar.IT4ClubCar.Services.Campo;
using System.Collections.ObjectModel;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System.Threading.Tasks;
using System.Linq;
using IT4ClubCar.IT4ClubCar.Services.ModoJogo;
using IT4ClubCar.IT4ClubCar.Services.Metrico;
using System.Windows.Input;
using Xamarin.Forms;
using IT4ClubCar.IT4ClubCar.Services.Buracos;
using IT4ClubCar.IT4ClubCar.Services.TeeDistancias;
using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.Services.ScreenshotService;
using IT4ClubCar.IT4ClubCar.Services.EmailService;
using IT4ClubCar.IT4ClubCar.Services.Tee;
using IT4ClubCar.IT4ClubCar.Excepcoes;

namespace IT4ClubCar.IT4ClubCar.ViewModels
{
    class JogoConfiguracaoViewModel : BaseViewModel
    {
        #region Serviços
        private ICampoService _campoService;
        private IModoJogoService _modoJogoService;
        private IMetricoService _metricoService;
        private IBuracosService _buracoService;
        private ITeeDistanciaService _teeDistanciaService;
        private ITeeService _teeService;
        #endregion

        #region Propriedades

        /// <summary>
        /// Obtém e define os CamposExistentes.
        /// </summary>
        private ObservableCollection<CampoWrapperViewModel> _camposExistentes;
        public ObservableCollection<CampoWrapperViewModel> CamposExistentes
        {
            get
            {
                return _camposExistentes;
            }
            set
            {
                _camposExistentes = value;
                OnPropertyChanged("CamposExistentes");
            }
        }

        /// <summary>
        /// Obtém e define o CampoSelecionado.
        /// </summary>
        private CampoWrapperViewModel _campoSelecionado;
        public CampoWrapperViewModel CampoSelecionado
        {
            get
            {
                return _campoSelecionado;
            }
            set
            {
                _campoSelecionado = value;
                OnPropertyChanged("CampoSelecionado");
            }
        }

        /// <summary>
        /// Obtém e define os ModosJogoExistentes.
        /// </summary>
        private ObservableCollection<ModoJogoWrapperViewModel> _modosJogoExistentes;
        public ObservableCollection<ModoJogoWrapperViewModel> ModosJogoExistentes
        {
            get
            {
                return _modosJogoExistentes;
            }
            set
            {
                _modosJogoExistentes = value;
                OnPropertyChanged("ModosJogoExistentes");
            }
        }

        /// <summary>
        /// Obtém e define o ModoJogoSelecionado.
        /// </summary>
        private ModoJogoWrapperViewModel _modoJogoSelecionado;
        public ModoJogoWrapperViewModel ModoJogoSelecionado
        {
            get
            {
                return _modoJogoSelecionado;
            }
            set
            {
                _modoJogoSelecionado = value;
                OnPropertyChanged("ModoJogoSelecionado");
            }
        }

        /// <summary>
        /// Obtém e define os MetricosExistentes.
        /// </summary>
        private ObservableCollection<MetricoWrapperViewModel> _metricosExistentes;
        public ObservableCollection<MetricoWrapperViewModel> MetricosExistentes
        {
            get
            {
                return _metricosExistentes;
            }
            set
            {
                _metricosExistentes = value;
                OnPropertyChanged("MetricosExistentes");
            }
        }

        /// <summary>
        /// Obtém e define o MetricoSelecionado.
        /// </summary>
        private MetricoWrapperViewModel _metricoSelecionado;
        public MetricoWrapperViewModel MetricoSelecionado
        {
            get
            {
                return _metricoSelecionado;
            }
            set
            {
                _metricoSelecionado = value;
                OnPropertyChanged("MetricoSelecionado");
            }
        }

        /// <summary>
        /// Obtém e define os TeesExistentes.
        /// </summary>
        private ObservableCollection<TeeWrapperViewModel> _teesExistentes;
        public ObservableCollection<TeeWrapperViewModel> TeesExistentes
        {
            get
            {
                return _teesExistentes;
            }
            set
            {
                _teesExistentes = value;
                OnPropertyChanged("TeesExistentes");
            }
        }

        /// <summary>
        /// Obtém e define o TeeSelecionado.
        /// </summary>
        private TeeWrapperViewModel _teeSelecionado;
        public TeeWrapperViewModel TeeSelecionado
        {
            get
            {
                return _teeSelecionado;
            }
            set
            {
                _teeSelecionado = value;
                OnPropertyChanged("TeeSelecionado");
            }
        }

        /// <summary>
        /// Obtém e define os Jogadores.
        /// </summary>
        public ObservableCollection<JogadorWrapperViewModel> Jogadores { get; set; }

        /// <summary>
        /// Obtém e define o Jogo.
        /// </summary>
        private JogoWrapperViewModel Jogo { get; set; }

        /// <summary>
        /// Obtém e define o IsActivityIndicatorVisivel.
        /// </summary>
        private bool _isActivityIndicatorVisivel;
        public bool IsActivityIndicatorVisivel
        {
            get
            {
                return _isActivityIndicatorVisivel;
            }
            set
            {
                _isActivityIndicatorVisivel = value;
                OnPropertyChanged("IsActivityIndicatorVisivel");
            }
        }

        /// <summary>
        /// Obtém e define o IsActivityIndicatorACorrer.
        /// </summary>
        private bool _isActivityIndicatorACorrer;
        public bool IsActivityIndicatorACorrer
        {
            get
            {
                return _isActivityIndicatorACorrer;
            }
            set
            {
                _isActivityIndicatorACorrer = value;
                OnPropertyChanged("IsActivityIndicatorACorrer");
            }
        }

        /// <summary>
        /// Obtém e define a CorDeFundo.
        /// </summary>
        private string _corDeFundo;
        public string CorDeFundo
        {
            get
            {
                return _corDeFundo;
            }
            set
            {
                _corDeFundo = value;
                OnPropertyChanged("CorDeFundo");
            }
        }

        #endregion

        #region Commands

        private ICommand _comecarJogoCommand;
        public ICommand ComecarJogoCommand
        {
            get
            {
                if (_comecarJogoCommand == null)
                    _comecarJogoCommand = new Command(async p => await ComecarJogo(), p => { return (Jogadores?.Count!=0); });
                return _comecarJogoCommand;
            }
            set
            {
                _comecarJogoCommand = value;
            }
        }

        private ICommand _cancelarJogoCommand;
        public ICommand CancelarJogoCommand
        {
            get
            {
                if (_cancelarJogoCommand == null)
                    _cancelarJogoCommand = new Command(async p => await base.NavigationService.IrParaPaginaAnterior(), p => { return true; });
                return _cancelarJogoCommand;
            }
            set
            {
                _comecarJogoCommand = value;
            }
        }

        #endregion



        public JogoConfiguracaoViewModel(
            INavigationService navigationService, 
            IDialogService dialogService,
            ICampoService campoService,
            IModoJogoService modoJogoService,
            IMetricoService metricoService,
            IBuracosService buracoService,
            ITeeService teeService, 
            ITeeDistanciaService teeDistanciaService) 
            : base(navigationService, dialogService)
        {
            _campoService = campoService;
            _modoJogoService = modoJogoService;
            _metricoService = metricoService;
            _buracoService = buracoService;
            _teeService = teeService;
            _teeDistanciaService = teeDistanciaService;

            CorDeFundo = "#00000000";

            Jogadores = new ObservableCollection<JogadorWrapperViewModel>();

            InicializarComunicacaoMediadorMensagens();

            //Preencher Pickers.
            Task.Run(async () => await InicializarDados());
        }



        private async Task InicializarDados()
        {
            //Preencher CamposExistentes e definir o CampoSelecionado como o CampoDefault.
            CamposExistentes = await _campoService.ObterCamposDisponiveis();
            int idCampoDefault = await _campoService.ObterCampoDefault();
            CampoSelecionado = CamposExistentes.Where(p => p.Id.Equals(idCampoDefault)).FirstOrDefault();

            //Preencher ModosJogoExistentes e definir o ModoSelecionado como o ModoJogoDefault.
            ModosJogoExistentes = await _modoJogoService.ObterModosJogoDisponiveis();
            int idModoJogoDefault = await _modoJogoService.ObterModoJogoDefault();
            ModoJogoSelecionado = ModosJogoExistentes.Where(p => p.Id.Equals(idModoJogoDefault)).FirstOrDefault();

            //Preencher MetricosExistentes e definir o MetricoSelecionado como o MetricoDefault.
            MetricosExistentes = await _metricoService.ObterMetricosDisponiveis();
            int idMetricoDefault = await _metricoService.ObterMetricoDefault();
            MetricoSelecionado = MetricosExistentes.Where(p => p.Id.Equals(idMetricoDefault)).FirstOrDefault();

            //Preencher os TeesExistentes e definir o TeeSelecionado como o StartingTeeDefault.
            TeesExistentes = await _teeService.ObterTeesExistentes();
            int idStartingTeeDefault = await _teeService.ObterStartingTeeDefault();
            TeeSelecionado = TeesExistentes.Where(p => p.Id.Equals(idStartingTeeDefault)).FirstOrDefault();
        }


        /// <summary>
        /// Regista o viewmodel às mensagens necessárias.
        /// </summary>
        private void InicializarComunicacaoMediadorMensagens()
        {
            //Jogador Adicionado.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogadorAdicionado, p => AdicionarJogador(jogadorAAdicionar: p as JogadorWrapperViewModel));
            //Jogador Removido.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogadorRemovido, p => RemoverJogador(jogadorARemover: p as JogadorWrapperViewModel));

            base.MensagensUsadas.Add(MediadorMensagensService.ViewModelMensagens.JogadorAdicionado);
            base.MensagensUsadas.Add(MediadorMensagensService.ViewModelMensagens.JogadorRemovido);
            base.MensagensUsadas.Add(MediadorMensagensService.ViewModelMensagens.Teste);
        }



        /// <summary>
        /// Adiciona um jogador à lista de Jogadores definidos.
        /// </summary>
        /// <remarks>Atualiza o CanExecute do botão ComecarJogo.</remarks>
        /// <param name="jogadorAAdicionar">Jogador a adicionar à lista de Jogadores definidos.</param>
        private void AdicionarJogador(JogadorWrapperViewModel jogadorAAdicionar)
        {
            Jogadores?.Add(jogadorAAdicionar);
            //Atualiar CanExecute do botão ComecarJogo.
            ((Command)ComecarJogoCommand).ChangeCanExecute();
        }



        /// <summary>
        /// Remove um jogador da lista de Jogadores definidos.
        /// </summary>
        /// <remarks>Atualiza o CanExecute do botão ComecarJogo.</remarks>
        /// <param name="jogadorARemover">Jogador a remover da lista de Jogadores definidos.</param>
        private void RemoverJogador(JogadorWrapperViewModel jogadorARemover)
        {
            Jogadores?.Remove(jogadorARemover);
            //Atualiar CanExecute do botão ComecarJogo.
            ((Command)ComecarJogoCommand).ChangeCanExecute();
        }



        /// <summary>
        /// Executado quando o utilizador clica no botão "Start" após ter configurado o jogo.
        /// </summary>
        private async Task ComecarJogo()
        {
            IsActivityIndicatorACorrer = true;
            IsActivityIndicatorVisivel = true;
            CorDeFundo = "#CC000000";

            await ConfigurarJogo();
            await base.NavigationService.IrParaJogo();

            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.NovoJogo, Jogo);

            LimparMemoria();
        }



        private async Task ConfigurarJogo()
        {
            //Obter os buracos do campo selecionado.
            await DefinirBuracosCampo();

            //Definir as distâncias de cada buraco para cada tee.
            await DefinirDistancias();

            //Definir as pontuações iniciais de cada jogador.
            DefinirPontuacoes();
            
            //Criar novo jogo.
            CriarNovoJogo();
        }

        /// <summary>
        /// Obtém os buracos do campo selecionado.
        /// </summary>
        private async Task DefinirBuracosCampo()
        {
            ObservableCollection<BuracoWrapperViewModel> buracos = await _buracoService.ObterBuracosDeCampo(CampoSelecionado);
            buracos.ToList().ForEach(p => CampoSelecionado.AdicionarBuraco(p));
        }

        /// <summary>
        /// Define em cada buraco a distância do mesmo para os tees escolhidos pelo utilizador.
        /// </summary>
        private async Task DefinirDistancias()
        {
            //Obter tees utilizados pelos jogadores.
            ObservableCollection<TeeWrapperViewModel> tees = new ObservableCollection<TeeWrapperViewModel>();

            foreach (JogadorWrapperViewModel jogador in Jogadores)
                tees.Add(jogador.Tee);

            //Obter distância de cada buraco para cada tee.
            foreach (TeeWrapperViewModel tee in tees)
            {
                ObservableCollection<TeeBuracoDistanciaWrapperViewModel> distancias = new ObservableCollection<TeeBuracoDistanciaWrapperViewModel>();

                foreach (BuracoWrapperViewModel buraco in CampoSelecionado.Buracos)
                {
                    TeeBuracoDistanciaWrapperViewModel distancia = await _teeDistanciaService.ObterDistancias(buraco, tee);

                    if (distancia == null)
                        continue;

                    distancias.Add(distancia);
                }

                distancias.ToList().ForEach(p => tee.AdicionarDistancia(p));
            }
        }

        /// <summary>
        /// Define as pontuações iniciais de cada jogador.
        /// </summary>
        private void DefinirPontuacoes()
        {
            //Inicializar pontuacoes dos jogadores a zero.
            foreach (JogadorWrapperViewModel jogador in Jogadores)
            {
                ObservableCollection<PontuacaoWrapperViewModel> pontuacoes = new ObservableCollection<PontuacaoWrapperViewModel>();
                foreach (BuracoWrapperViewModel buraco in CampoSelecionado.Buracos)
                    pontuacoes.Add(new PontuacaoWrapperViewModel(new PontuacaoModel(buraco.ObterModelo(), 0)));

                //O campo pode não ter 18 buracos, mas é necessário ter pontuações para os dezoito buracos.
                while(!pontuacoes.Count.Equals(18))
                {
                    pontuacoes.Add(new PontuacaoWrapperViewModel(new PontuacaoModel(BuracoModel.BuracoVazio,0)));
                }

                pontuacoes.ToList().ForEach(p => jogador.AdicionarPontuacao(p));
            }
        }

        /// <summary>
        /// Cria um novo JogoWrapperViewModel que tem todas as configurações definidas pelo utilizador.
        /// </summary>
        private void CriarNovoJogo()
        {
            List<JogadorModel> jogadores = new List<JogadorModel>();

            foreach (JogadorWrapperViewModel jogador in Jogadores)
                jogadores.Add(jogador.ObterModel());

            JogoModel jogoModel = new JogoModel(CampoSelecionado.ObterModel(), ModoJogoSelecionado.ObterModel(), MetricoSelecionado.ObterModel(), jogadores);

            Jogo = new JogoWrapperViewModel(jogoModel);
        }



        protected override void LimparMemoria()
        {
            _campoService = null;
            _modoJogoService = null;
            _metricoService = null;
            _buracoService = null;
            _teeDistanciaService = null;
            _teeService = null;

            CamposExistentes = null;
            CampoSelecionado = null;
            ModosJogoExistentes = null;
            ModoJogoSelecionado = null;
            MetricosExistentes = null;
            MetricoSelecionado =  null;
            TeesExistentes = null;
            TeeSelecionado = null;
            Jogadores = null;
            Jogo = null;
            IsActivityIndicatorVisivel = false;
            IsActivityIndicatorACorrer = false;
            CorDeFundo = null;

            ComecarJogoCommand = null;
            CancelarJogoCommand = null;

            base.LimparMemoria();
        }
    }
}
