using IT4ClubCar.IT4ClubCar.Services.BuggyBar;
using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.EmailService;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.TelemovelService;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Popups
{
    class PedirBuggyBarPopupViewModel : BaseViewModel
    {
        /// <summary>
        /// Obtém e define o _buggyBarService.
        /// </summary>
        private IBuggyBarService _buggyBarService;

        /// <summary>
        /// Obtém e define o _telemovelService.
        /// </summary>
        private ITelemovelService _telemovelService;

        /// <summary>
        /// Obtém e define o Campo.
        /// </summary>
        private CampoWrapperViewModel _campo;
        public CampoWrapperViewModel Campo
        {
            get
            {
                return _campo;
            }
            set
            {
                _campo = value;
                OnPropertyChanged("Campo");
            }
        }

        /// <summary>
        /// Obtém e define o BuracoSelecionado.
        /// </summary>
        private BuracoWrapperViewModel _buracoSelecionado;
        public BuracoWrapperViewModel BuracoSelecionado
        {
            get
            {
                return _buracoSelecionado;
            }
            set
            {
                _buracoSelecionado = value;
                OnPropertyChanged("BuracoSelecionado");
            }
        }

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

        private ICommand _fecharPopupCommand;
        public ICommand FecharPopupCommand
        {
            get
            {
                if (_fecharPopupCommand == null)
                    _fecharPopupCommand = new Command(async p => await base.NavigationService.SairDePedirBuggyBar(), p => { return true; });
                return _fecharPopupCommand;
            }
        }

        private ICommand _enviarPedidoCommand;
        public ICommand EnviarPedidoCommand
        {
            get
            {
                if (_enviarPedidoCommand == null)
                    _enviarPedidoCommand = new Command(async p => await EnviarPedido(), p => { return true; });
                return _enviarPedidoCommand;
            }
        }



        public PedirBuggyBarPopupViewModel(INavigationService navigationService,
                                           IDialogService dialogService,
                                           IBuggyBarService buggyBarService,
                                           ITelemovelService telemovelService)
                                           : base(navigationService,dialogService)
        {
            _buggyBarService = buggyBarService;
            _telemovelService = telemovelService;

            CorDeFundo = "#20000000";
            
            InicializarComunicacaoComMediadorMensagens();
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do Mediador Mensagem.
        /// </summary>
        private void InicializarComunicacaoComMediadorMensagens()
        {
            //Campo Atual.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.CampoAtual, p => InicializarCampo((CampoWrapperViewModel)p));
        }



        /// <summary>
        /// Inicializa a propriedade Campo inicializando também a propriedade BuracoSelecionado.
        /// </summary>
        private void InicializarCampo(CampoWrapperViewModel campoEscolhido)
        {
            if (campoEscolhido == null)
                return;

            Campo = campoEscolhido;
            InicializarBuracoSelecionado(Campo.Buracos[0]);
        }



        /// <summary>
        /// Inicializa a propriedade BuracoSelecionado.
        /// </summary>
        /// <param name="buraco">Buraco cujo valor vai ser usado para inicializar a propriedade BuracoSelecionado.</param>
        private void InicializarBuracoSelecionado(BuracoWrapperViewModel buraco)
        {
            if (!buraco.Numero.Equals(Campo.Buracos.Count))
                BuracoSelecionado = Campo.Buracos.Where(p => p.Numero.Equals(buraco.Numero + 1)).FirstOrDefault();
            else
                BuracoSelecionado = buraco;
        }



        /// <summary>
        /// Envia um sms ao BuggyBar a pedir a sua presença no buraco indicado pelo utilizador.
        /// </summary>
        private async Task EnviarPedido()
        {
            //Mostrar Activity indicator.
            IsActivityIndicatorACorrer = true;
            IsActivityIndicatorVisivel = true;
            CorDeFundo = "#CC000000";

            string numeroTelemovelBuggyBar = await _buggyBarService.ObterNumeroTelemovel();

            string mensagemAEnviar = "\n Campo : " + Campo.Nome + "\n Buraco : " + BuracoSelecionado.Numero;

            await _telemovelService.EnviarSMS(numeroTelemovelBuggyBar, mensagemAEnviar);

            await base.NavigationService.SairDePedirBuggyBar();

            //Esconder Activity Indicator.
            IsActivityIndicatorACorrer = false;
            IsActivityIndicatorVisivel = false;
        }

    }
}
