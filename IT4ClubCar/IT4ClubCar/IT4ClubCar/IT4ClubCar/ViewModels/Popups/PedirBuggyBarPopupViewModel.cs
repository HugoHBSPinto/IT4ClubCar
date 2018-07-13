using IT4ClubCar.IT4ClubCar.Services.BuggyBar;
using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.EmailService;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.TelemovelService;
using IT4ClubCar.IT4ClubCar.Toolbox;
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
        #region Propriedades
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
        /// Obtém e define a ActivityIndicatorTool.
        /// </summary>
        private ActivityIndicatorTool _activityIndicatorTool;
        public ActivityIndicatorTool ActivityIndicatorTool
        {
            get
            {
                return _activityIndicatorTool;
            }
            set
            {
                _activityIndicatorTool = value;
                OnPropertyChanged("ActivityIndicatorTool");
            }
        }
        #endregion

        #region Services
        /// <summary>
        /// Obtém e define o _buggyBarService.
        /// </summary>
        private IBuggyBarService _buggyBarService;

        /// <summary>
        /// Obtém e define o _telemovelService.
        /// </summary>
        private ITelemovelService _telemovelService;
        #endregion

        #region Commands
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
        #endregion



        public PedirBuggyBarPopupViewModel(INavigationService navigationService,
                                           IDialogService dialogService,
                                           IBuggyBarService buggyBarService,
                                           ITelemovelService telemovelService)
                                           : base(navigationService,dialogService)
        {
            _buggyBarService = buggyBarService;
            _telemovelService = telemovelService;

            ActivityIndicatorTool = new ActivityIndicatorTool(activityIndicatorCor: "#e2243d", mensagemAMostrar: "Calling BuggyBar...", backgroundCorVisivel: "#CC000000", backgroundCorEscondido: "#00000000");

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
            ActivityIndicatorTool.ExecutarRoda();

            string numeroTelemovelBuggyBar = await _buggyBarService.ObterNumeroTelemovel();

            string mensagemAEnviar = "\n Campo : " + Campo.Nome + "\n Buraco : " + BuracoSelecionado.Numero;

            await _telemovelService.EnviarSMS(numeroTelemovelBuggyBar, mensagemAEnviar);

            await base.NavigationService.SairDePedirBuggyBar();

            ActivityIndicatorTool.PararRoda();
        }

    }
}
