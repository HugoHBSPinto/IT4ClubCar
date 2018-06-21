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

namespace IT4ClubCar.IT4ClubCar.ViewModels
{
    class JogoConfiguracaoViewModel : BaseViewModel
    {
        private ICampoService _campoService;
        private IModoJogoService _modoJogoService;
        private IMetricoService _metricoService;
        private IBuracosService _buracoService;
        private ITeeDistanciaService _teeDistanciaService;

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
        /// Obtém e define os Jogadores.
        /// </summary>
        public ObservableCollection<JogadorWrapperViewModel> Jogadores { get; set; }

        #endregion

        #region Commands

        private ICommand _comecarJogoCommand;
        public ICommand ComecarJogoCommand
        {
            get
            {
                if (_comecarJogoCommand == null)
                    _comecarJogoCommand = new Command(async p => await ComecarJogo(), p => { return true; });
                return _comecarJogoCommand;
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
            ITeeDistanciaService teeDistanciaService) 
            : base(navigationService, dialogService)
        {
            _campoService = campoService;
            _modoJogoService = modoJogoService;
            _metricoService = metricoService;
            _buracoService = buracoService;
            _teeDistanciaService = teeDistanciaService;

            //Preencher Pickers.
            Task.Run(async () => await InicializarDados()).ContinueWith(p => InicializarComunicacaoMediadorMensagens());
        }



        private async Task InicializarDados()
        {
            ///Preencher CamposExistentes e definir o CampoSelecionado como o CampoDefault.
            CamposExistentes = await _campoService.ObterCamposDisponiveis();
            int idCampoDefault = await _campoService.ObterCampoDefault();
            CampoSelecionado = CamposExistentes.Where(p => p.Id.Equals(idCampoDefault)).FirstOrDefault();

            ///Preencher ModosJogoExistentes e definir o ModoSelecionado como o ModoJogoDefault.
            ModosJogoExistentes = await _modoJogoService.ObterModosJogoDisponiveis();
            int idModoJogoDefault = await _modoJogoService.ObterModoJogoDefault();
            ModoJogoSelecionado = ModosJogoExistentes.Where(p => p.Id.Equals(idModoJogoDefault)).FirstOrDefault();

            ///Preencher MetricosExistentes e definir o MetricoSelecionado como o MetricoDefault.
            MetricosExistentes = await _metricoService.ObterMetricosDisponiveis();
            int idMetricoDefault = await _metricoService.ObterMetricoDefault();
            MetricoSelecionado = MetricosExistentes.Where(p => p.Id.Equals(idMetricoDefault)).FirstOrDefault();

            Jogadores = new ObservableCollection<JogadorWrapperViewModel>();
        }


        /// <summary>
        /// Regista o viewmodel às mensagens necessárias.
        /// </summary>
        private void InicializarComunicacaoMediadorMensagens()
        {
            //Jogador Adicionado.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogadorAdicionado, p => { Jogadores.Add((JogadorWrapperViewModel)p); });
            //Jogador Removido.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogadorRemovido, p => { Jogadores.Remove((JogadorWrapperViewModel)p); });
        }



        private async Task ComecarJogo()
        {
            await ConfigurarJogo();
            await base.NavigationService.IrParaJogo();
        }



        private async Task ConfigurarJogo()
        {
            //Obter os buracos do campo selecionado.
            ObservableCollection<BuracoWrapperViewModel> buracos = await _buracoService.ObterBuracosDeCampo(CampoSelecionado);

            //Obter distâncias de cada buraco para cada tee.
            //Obter tees utilizados pelos jogadores.
            ObservableCollection<TeeWrapperViewModel> tees = new ObservableCollection<TeeWrapperViewModel>();
            foreach (JogadorWrapperViewModel jogador in Jogadores)
                tees.Add(jogador.Tee);
            //Obter distância de cada buraco para cada tee.
            foreach(BuracoWrapperViewModel buraco in buracos)
            {
                ObservableCollection<TeeBuracoDistanciaWrapperViewModel> distancias = new ObservableCollection<TeeBuracoDistanciaWrapperViewModel>();
                foreach (TeeWrapperViewModel tee in tees)
                {
                    distancias.Add(await _teeDistanciaService.ObterDistancias(buraco, tee));
                }
                distancias.ToList().ForEach(p => buraco.AdicionarDistancia(p));
            }

            buracos.ToList().ForEach(p => CampoSelecionado.AdicionarBuraco(p));

            //Inicializar pontuacoes dos jogadores a zero.
            foreach(JogadorWrapperViewModel jogador in Jogadores)
            {
                ObservableCollection<PontuacaoWrapperViewModel> pontuacoes = new ObservableCollection<PontuacaoWrapperViewModel>();
                foreach(BuracoWrapperViewModel buraco in buracos)
                {
                    pontuacoes.Add(new PontuacaoWrapperViewModel(new Models.PontuacaoModel(buraco.ObterModelo(), 250)));
                }
                pontuacoes.ToList().ForEach(p => jogador.AdicionarPontuacao(p));
            }

            List<JogadorModel> jogadores = new List<JogadorModel>();

            foreach (JogadorWrapperViewModel jogador in Jogadores)
                jogadores.Add(jogador.ObterModel());

            JogoModel jogoModel = new JogoModel(CampoSelecionado.ObterModel(),ModoJogoSelecionado.ObterModel(),MetricoSelecionado.ObterModel(),jogadores);

            JogoWrapperViewModel jogo = new JogoWrapperViewModel(jogoModel);

            for (int i = 0; i < 255; i++)
                Debug.Print("Pontuacao do Jogador 1 : {0}",jogo.Jogadores[0].Pontuacoes.Where(p => p.Buraco.Numero.Equals(1)).FirstOrDefault().Pontos);
        }

    }
}
