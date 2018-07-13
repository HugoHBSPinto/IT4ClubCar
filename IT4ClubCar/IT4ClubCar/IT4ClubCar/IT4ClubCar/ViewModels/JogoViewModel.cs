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
using IT4ClubCar.IT4ClubCar.CustomControls;
using IT4ClubCar.IT4ClubCar.Services.Geometria;
using IT4ClubCar.IT4ClubCar.Toolbox;

namespace IT4ClubCar.IT4ClubCar.ViewModels
{
    class JogoViewModel : BaseViewModel
    {
        #region Propriedades
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
                //AtualizarPosicaoTeePin();
            }
        }

        private MapSpan _centroMapa;
        public MapSpan CentroMapa
        {
            get
            {
                return _centroMapa;
            }
            set
            {
                _centroMapa = value;
                OnPropertyChanged("CentroMapa");
            }
        }

        private Position _buracoPinPosicao;
        public Position BuracoPinPosicao
        {
            get
            {

                if (_buracoPinPosicao == null)
                    _buracoPinPosicao = new Position(1.0,-1.0);
                return _buracoPinPosicao;
            }
            set
            {
                _buracoPinPosicao = value;
                OnPropertyChanged("BuracoPinPosicao");
                CalcularNovaDistancia();
            }
        }

        private Position _teePinPosicao;
        public Position TeePinPosicao
        {
            get
            {
                if (_teePinPosicao == null)
                    _teePinPosicao = new Position(1.0, -1.0);
                return _teePinPosicao;
            }
            set
            {
                _teePinPosicao = value;
                OnPropertyChanged("TeePinPosicao");
                CalcularNovaDistancia();
            }
        }

        private Position _meioPinPosicao;
        public Position MeioPinPosicao
        {
            get
            {
                if (_meioPinPosicao == null)
                    _meioPinPosicao = new Position(1.0, -1.0);
                return _meioPinPosicao;
            }
            set
            {
                _meioPinPosicao = value;
                OnPropertyChanged("MeioPinPosicao");
                CalcularNovaDistancia();
            }
        }

        private double _distanciaBuracoMeio;
        public double DistanciaBuracoMeio
        {
            get
            {
                return Math.Round(_distanciaBuracoMeio, 2);
            }
            set
            {
                _distanciaBuracoMeio = value;
                OnPropertyChanged("DistanciaBuracoMeio");
            }
        }

        private double _distanciaTeeMeio;
        public double DistanciaTeeMeio
        {
            get
            {
                return Math.Round(_distanciaTeeMeio, 2);
            }
            set
            {
                _distanciaTeeMeio = value;
                OnPropertyChanged("DistanciaTeeMeio");
            }
        }
        #endregion

        #region Commands
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

        private ICommand _resetMapaCommand;
        public ICommand ResetMapaCommand
        {
            get
            {
                if (_resetMapaCommand == null)
                    _resetMapaCommand = new Command(p => ResetMapa(), p => { return true; });
                return _resetMapaCommand;
            }
        }

        private ICommand _alterarEstadoPosicaoPinTee;
        public ICommand AlterarEstadoPosicaoPinTee
        {
            get
            {
                if (_alterarEstadoPosicaoPinTee == null)
                    _alterarEstadoPosicaoPinTee = new Command(p => MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.AlterarDraggablePinTee,null), p => { return true; });
                return _alterarEstadoPosicaoPinTee;
            }
        }
        #endregion

        #region Services
        private IGeometriaService _geometriaService;
        #endregion

        public JogoViewModel(   INavigationService navigationService, 
                                IDialogService dialogService,
                                IGeometriaService geometriaService) 
                                : base(navigationService,dialogService)
        {
            _geometriaService = geometriaService;

            InicializarComunicacaoMediadorMensagens();
        }



        /// <summary>
        /// Regista o ViewModel às mensagens necessárias do MediadorMensagensService.
        /// </summary>
        private void InicializarComunicacaoMediadorMensagens()
        {
            //Jogo enviado.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.NovoJogo, p => InicializarJogo((JogoWrapperViewModel)p));
            //Ouvir por pedidos do Jogo.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.PedirPorJogo, p => MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogoAtual, Jogo));
            //Ouvir pelo pedido de continuar um jogo parado.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.RecomecarJogo, p => RecomecarJogoPausado((JogoWrapperViewModel)p));
        }



        /// <summary>
        /// Define a propriedade Jogo e atualiza a propriedade Buraco com o valor do primeiro buraco do campo do jogo.
        /// </summary>
        private void InicializarJogo(JogoWrapperViewModel jogo)
        {
            Jogo = jogo;

            //Obter buraco atual. Neste caso o primeiro buraco do campo.
            Jogo.BuracoAtual = Jogo.Campo.Buracos[0];

            //Guardar numa lista todos os tees usados pelos jogadores para mostrar no picker.
            TeesUsados = new ObservableCollection<TeeWrapperViewModel>();
            Jogo.Jogadores.ToList().ForEach(p => TeesUsados.Add(p.Tee));

            TeeSelecionado = TeesUsados[0];

            //Atualizar posição dos pins.
            TeeBuracoDistanciaWrapperViewModel teeDistanciaInicial = TeeSelecionado.TeeBuracosDistancia.Where(p => p.Buraco.Numero.Equals(Jogo.BuracoAtual.Numero)).FirstOrDefault();

            BuracoPinPosicao = new Position(Jogo.BuracoAtual.Latitude, Jogo.BuracoAtual.Longitude);
            TeePinPosicao = new Position(teeDistanciaInicial.Latitude, teeDistanciaInicial.Longitude);

            MeioPinPosicao = _geometriaService.ObterPosicaoMeio(BuracoPinPosicao,TeePinPosicao);
            
            //Atualizar as coordenadas do mapa para estarem voltadas para as coordenadas do primeiro buraco.
            CentroMapa = MapSpan.FromCenterAndRadius(new Position(MeioPinPosicao.Latitude, MeioPinPosicao.Longitude), Distance.FromMeters(80));
        }



        /// <summary>
        /// Recomeça um jogo.
        /// </summary>
        private void RecomecarJogoPausado(JogoWrapperViewModel jogo)
        {
            if (jogo == null)
                return;
            
            Jogo = jogo;

            Jogo.BuracoAtual = Jogo.BuracoPausado;

            //Guardar numa lista todos os tees usados pelos jogadores para mostrar no picker.
            TeesUsados = new ObservableCollection<TeeWrapperViewModel>();
            Jogo.Jogadores.ToList().ForEach(p => TeesUsados.Add(p.Tee));

            TeeSelecionado = TeesUsados[0];

            //Atualizar posição dos pins.
            TeeBuracoDistanciaWrapperViewModel teeDistanciaInicial = TeeSelecionado.TeeBuracosDistancia.Where(p => p.Buraco.Numero.Equals(Jogo.BuracoAtual.Numero)).FirstOrDefault();

            BuracoPinPosicao = new Position(Jogo.BuracoAtual.Latitude, Jogo.BuracoAtual.Longitude);
            TeePinPosicao = new Position(teeDistanciaInicial.Latitude, teeDistanciaInicial.Longitude);

            MeioPinPosicao = _geometriaService.ObterPosicaoMeio(BuracoPinPosicao, TeePinPosicao);

            //Atualizar as coordenadas do mapa para estarem voltadas para as coordenadas do primeiro buraco.
            CentroMapa = MapSpan.FromCenterAndRadius(new Position(MeioPinPosicao.Latitude, MeioPinPosicao.Longitude), Distance.FromMeters(80));
        }



        /// <summary>
        /// Se possível altera o BuracoAtual para o valor do buraco anterior.
        /// </summary>
        private void IrParaBuracoAnterior()
        {
            //Verifica-se se o número do buraco atual é diferente de 1, ou seja, o buraco atual não é o primeiro.
            //Se não for pode-se ver o buraco anterior.
            if (Jogo.BuracoAtual.Numero.Equals(1))
                return;

            Jogo.BuracoAtual = Jogo.Campo.Buracos.Where(p => p.Numero.Equals((Jogo.BuracoAtual.Numero - 1))).FirstOrDefault();

            AtualizarCoordenadasMapa();
        }
        


        /// <summary>
        /// Se possível altera o buracoAtual para o buraco seguinte.
        /// </summary>
        private void IrParaBuracoSeguinte()
        {
            //Verifica-se se o numero do BuracoAtual é diferente do número de buracos do campo, ou seja, se não é o último buraco.
            //Se não for pode-se ver o buraco seguinte.
            int numeroBuracos = Jogo.Campo.NumeroBuracos;

            if (Jogo.BuracoAtual.Numero.Equals(numeroBuracos))
                return;

            Jogo.BuracoAtual = Jogo.Campo.Buracos.Where(p => p.Numero.Equals((Jogo.BuracoAtual.Numero+1))).FirstOrDefault();

            AtualizarCoordenadasMapa();
        }



        /// <summary>
        /// Atualiza a região mostrada pelo mapa, atualizando as coordenadas dos marcadores.
        /// </summary>
        private void AtualizarCoordenadasMapa()
        {
            TeePinPosicao = new Position(TeeSelecionado.TeeBuracosDistancia.Where(p => p.Buraco.Numero.Equals(Jogo.BuracoAtual.Numero)).FirstOrDefault().Latitude, TeeSelecionado.TeeBuracosDistancia.Where(p => p.Buraco.Numero.Equals(Jogo.BuracoAtual.Numero)).FirstOrDefault().Longitude);
            BuracoPinPosicao = new Position(Jogo.BuracoAtual.Latitude, Jogo.BuracoAtual.Longitude);
            MeioPinPosicao = _geometriaService.ObterPosicaoMeio(BuracoPinPosicao,TeePinPosicao);

            CentroMapa = MapSpan.FromCenterAndRadius(MeioPinPosicao, new Distance(80));
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.NovaPosicaoTeePin, TeePinPosicao);
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.NovaPosicaoBuraco, BuracoPinPosicao);
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.NovaPosicaoMeio, MeioPinPosicao);
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.ResetRegionMapa, CentroMapa);
        }



        /// <summary>
        /// Mostra um popup com a pro tip do BuracoAtual.
        /// </summary>
        private async Task VerProTip()
        {
            await base.NavigationService.IrParaProTip();
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.ProTipConteudo, Jogo.BuracoAtual.Dica);
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
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.BuracoPontuacaoAEditar, Jogo.BuracoAtual);
        }



        /// <summary>
        /// Mostra um popup com um scorecard onde se pode ver os pontos.
        /// </summary>
        private async Task VerScorecard()
        {
            await base.NavigationService.IrParaScorecard();

            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogoAtual,Jogo);
        }



        /// <summary>
        /// Mostra um popup onde o jogador pode pedir o buggybar para um determinado buraco.
        /// </summary>
        /// <returns></returns>
        private async Task PedirBuggyBar()
        {
            await base.NavigationService.IrParaPedirBuggyBar();

            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.CampoAtual, Jogo.Campo);
        }



        private async Task AbrirMenu()
        {
            await base.NavigationService.IrParaMenuJogo();
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogoAtual, Jogo);
        }
        
        
        
        /// <summary>
        /// Atualiza as propriedades DistanciaBuracoMeio e DistanciaTeeMeio.
        /// </summary>
        private void CalcularNovaDistancia()
        {
            if (Jogo == null)
                return;

            Metrico tipoMetrico = (Jogo.Metrico.Nome.Equals("Metros")) ? Metrico.Metro : Metrico.Yard;
            DistanciaBuracoMeio = _geometriaService.ObterDistancia(BuracoPinPosicao, MeioPinPosicao, tipoMetrico);
            DistanciaTeeMeio = _geometriaService.ObterDistancia(TeePinPosicao, MeioPinPosicao, tipoMetrico);
        }



        /// <summary>
        /// Atualiza a posição do marker do Tee para a posição do TeeSelecionado.
        /// </summary>
        private void AtualizarPosicaoTeePin()
        {
            TeeBuracoDistanciaWrapperViewModel teeBuracoDistancia = TeeSelecionado.TeeBuracosDistancia.Where(p => p.Buraco.Numero.Equals(Jogo.BuracoAtual.Numero)).FirstOrDefault();
            Position novaPosicao = new Position(teeBuracoDistancia.Latitude, teeBuracoDistancia.Longitude);
            TeePinPosicao = novaPosicao;
            //Enviar nova posição para o CustomRenderer do CustomMap.
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.NovaPosicaoTeePin, TeePinPosicao);
        }



        /// <summary>
        /// Atualiza a posição do Tee Pin para o TeeSelecionado e a posição do Tee Meio para o meio. Atualiza a region mostrada
        /// pelo mapa.
        /// </summary>
        private void ResetMapa()
        {
            //Fazer Reset à posição do Pin Tee.
            TeeBuracoDistanciaWrapperViewModel distancia = TeeSelecionado.TeeBuracosDistancia.Where(p => p.Buraco.Numero.Equals(Jogo.BuracoAtual.Numero)).FirstOrDefault();
            TeePinPosicao = new Position(distancia.Latitude, distancia.Longitude);
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.NovaPosicaoTeePin, TeePinPosicao);
            //Fazer Reset à posição do Pin Meio.
            MeioPinPosicao = _geometriaService.ObterPosicaoMeio(BuracoPinPosicao,TeePinPosicao);
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.NovaPosicaoMeio, MeioPinPosicao);
            //Atualizar region mostrada pelo mapa.
            CentroMapa = MapSpan.FromCenterAndRadius(new Position(MeioPinPosicao.Latitude, MeioPinPosicao.Longitude), Distance.FromMeters(80));
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.ResetRegionMapa, CentroMapa);
        }
        
    }
}
