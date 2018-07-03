using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.EmailService;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.ScreenshotService;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Popups
{
    class ScorecardPopupViewModel : BaseViewModel
    {
        private IScreenshotService _screenshotService;
        private IEmailService _emailService;

        /// <summary>
        /// Obtém e define o Jogo.
        /// </summary>
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

        /// <summary>
        /// Obtém e define os Tees.
        /// </summary>
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

        /// <summary>
        /// Obtém e define o JogadorAEnviarPrint.
        /// </summary>
        private JogadorWrapperViewModel _jogadorAEnviarPrint;
        public JogadorWrapperViewModel JogadorAEnviarPrint
        {
            get
            {
                return _jogadorAEnviarPrint;
            }
            set
            {
                _jogadorAEnviarPrint = value;
                OnPropertyChanged("JogadorAEnviarPrint");
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

        private ICommand _tirarPrintCommand;
        public ICommand TirarPrintCommand
        {
            get
            {
                if (_tirarPrintCommand == null)
                    _tirarPrintCommand = new Command(async p => await TirarPrint(), p => { return true; });
                return _tirarPrintCommand;
            }
        }

        private ICommand _fecharPopupCommand;
        public ICommand FecharPopupCommand
        {
            get
            {
                if (_fecharPopupCommand == null)
                    _fecharPopupCommand = new Command(async p => await FecharPopup(), p => { return true; });
                return _fecharPopupCommand;
            }
        }



        public ScorecardPopupViewModel( INavigationService navigationService, 
                                        IDialogService dialogService,
                                        IScreenshotService screenshotService,
                                        IEmailService emailService) 
                                        : base(navigationService,dialogService)
        {
            _screenshotService = screenshotService;
            _emailService = emailService;

            CorDeFundo = "#80000000";

            TeesUsados = new ObservableCollection<TeeWrapperViewModel>();
            InicializarComunicacaoMediadorMensagens();
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do MediadorMensagens.
        /// </summary>
        private void InicializarComunicacaoMediadorMensagens()
        {
            //Jogo a mostrar no scorecard.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogoAtual, p => InicializarPropriedadeJogo(p as JogoWrapperViewModel));
            //Jogador a enviar uma print do scorecard.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogadorAEnviarScorecard, p => { JogadorAEnviarPrint = p as JogadorWrapperViewModel; });
        }



        /// <summary>
        /// Inicializa a propriedade Jogo.
        /// </summary>
        /// <param name="jogo">Parâmetro cujo valor vai ser utilizado para inicializar a propriedade Jogo.</param>
        private void InicializarPropriedadeJogo(JogoWrapperViewModel jogo)
        {
            Jogo = jogo;

            //Jogo.Jogadores.ToList().ForEach(p => {
            //    if (!TeesUsados.Select(s => s.Nome.Equals(p.Tee.Nome)).Any())
            //        TeesUsados.Add(p.Tee);
            //});

            Jogo.Jogadores.ToList().ForEach(p =>
            {
                TeesUsados.Add(p.Tee);
            });
        }



        private async Task TirarPrint()
        {
            IsActivityIndicatorACorrer = true;
            IsActivityIndicatorVisivel = true;
            CorDeFundo = "#CC000000";

            //Tirar screenshot.
            byte[] screenshot = await _screenshotService.TirarScreenshotAsync();

            //Guardar screenshot como anexo do email.
            MimeKit.AttachmentCollection attachments = new MimeKit.AttachmentCollection();
            attachments.Add("ScorecardPNG,", screenshot, ContentType.Parse("image/png"));

            //Enviar Email.
            await _emailService.EnviarEmail(emailDestino: JogadorAEnviarPrint.Email, assunto: "IT4ClubCar Game Results",mensagemConteudo: "Like you asked :)",attachments: attachments);

            await base.NavigationService.SairDeScorecard();

            IsActivityIndicatorACorrer = false;
            IsActivityIndicatorVisivel = false;
        }



        private async Task FecharPopup()
        {
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.AFecharPopup, null);
            
            await base.NavigationService.SairDeScorecard();
        }

    }
}
