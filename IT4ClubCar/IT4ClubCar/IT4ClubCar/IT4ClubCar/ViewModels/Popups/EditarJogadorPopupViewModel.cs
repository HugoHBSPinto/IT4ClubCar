using IT4ClubCar.IT4ClubCar.Services.Buracos;
using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Genero;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.Tee;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using IT4ClubCar.IT4ClubCar.Models;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using IT4ClubCar.IT4ClubCar.Services.Camera;
using System.IO;
using IT4ClubCar.IT4ClubCar.Services.ScreenshotService;
using IT4ClubCar.IT4ClubCar.Validacoes;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Popups
{
    class EditarJogadorPopupViewModel : BaseViewModel
    {
        #region Serviços
        private ITeeService _teeService;
        private IHandicapService _handicapService;
        private IGeneroService _generoService;
        private ICameraService _cameraService;
        #endregion

        #region Dados Disponiveis

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
        /// Obtém e define os GenerosExistentes.
        /// </summary>
        private ObservableCollection<GeneroWrapperViewModel> _generosExistentes;
        public ObservableCollection<GeneroWrapperViewModel> GenerosExistentes
        {
            get
            {
                return _generosExistentes;
            }
            set
            {
                _generosExistentes = value;
                OnPropertyChanged("GenerosExistentes");
            }
        }

        /// <summary>
        /// Obtém e define o HandicapMinimo.
        /// </summary>
        private HandicapWrapperViewModel _handicapMinimo;
        public HandicapWrapperViewModel HandicapMinimo
        {
            get
            {
                return _handicapMinimo;
            }
            set
            {
                _handicapMinimo = value;
                OnPropertyChanged("HandicapMinimo");
            }
        }

        /// <summary>
        /// Obtém e define o HandicapMaximo.
        /// </summary>
        private HandicapWrapperViewModel _handicapMaximo;
        public HandicapWrapperViewModel HandicapMaximo
        {
            get
            {
                return _handicapMaximo;
            }
            set
            {
                _handicapMaximo = value;
                OnPropertyChanged("HandicapMaximo");
            }
        }


        #endregion

        #region Dados Utilizador

        private JogadorWrapperViewModel _jogador;

        private ValidatableObject<string> _nome;
        public ValidatableObject<string> Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value;
                OnPropertyChanged("Nome");
            }
        }

        private ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private ImageSource _foto;
        public ImageSource Foto
        {
            get
            {
                return _foto;
            }
            set
            {
                _foto = value;
                OnPropertyChanged("Foto");
            }
        }

        private GeneroWrapperViewModel _genero;
        public GeneroWrapperViewModel Genero
        {
            get
            {
                return _genero;
            }
            set
            {
                _genero = value;
                OnPropertyChanged("Genero");
            }
        }

        private TeeWrapperViewModel _tee;
        public TeeWrapperViewModel Tee
        {
            get
            {
                return _tee;
            }
            set
            {
                _tee = value;
                OnPropertyChanged("Tee");
            }
        }

        private HandicapWrapperViewModel _handicap;
        public HandicapWrapperViewModel Handicap
        {
            get
            {
                return _handicap;
            }
            set
            {
                _handicap = value;
                OnPropertyChanged("Handicap");
            }
        }

        #endregion

        #region Commands

        private ICommand _tirarFotoCommand;
        public ICommand TirarFotoCommand
        {
            get
            {
                if (_tirarFotoCommand == null)
                    _tirarFotoCommand = new Command(async p => await TirarFoto(), p => { return true; });
                return _tirarFotoCommand;
            }
        }

        private ICommand _guardarDadosCommand;
        public ICommand GuardarDadosCommand
        {
            get
            {
                if (_guardarDadosCommand == null)
                    _guardarDadosCommand = new Command(async p => await GuardarDados(), p => { return true; });
                return _guardarDadosCommand;
            }
        }

        private ICommand _removerJogadorCommand;
        public ICommand RemoverJogadorCommand
        {
            get
            {
                if (_removerJogadorCommand == null)
                    _removerJogadorCommand = new Command(async p => await RemoverUtilizador(), p => { return true; });
                return _removerJogadorCommand;
            }
        }

        private ICommand _cancelarEdicaoCommand;
        public ICommand CancelarEdicaoCommand
        {
            get
            {
                if (_cancelarEdicaoCommand == null)
                    _cancelarEdicaoCommand = new Command(async p => await CancelarEdicao(), p => { return true; });
                return _cancelarEdicaoCommand;
            }
        }


        #endregion


        public EditarJogadorPopupViewModel(INavigationService navigationService, 
                                           IDialogService dialogService,
                                           ITeeService teeService,
                                           IHandicapService handicapService,
                                           IGeneroService generoService,
                                           ICameraService cameraService) 
                                           : base(navigationService,dialogService)
        {
            _teeService = teeService;
            _handicapService = handicapService;
            _generoService = generoService;
            _cameraService = cameraService;

            Task.Run(async () => await InicializarDados())
                .ContinueWith(p => InicializarComunicacaoMediador());

            Email = new ValidatableObject<string>();
            Email.RegrasValidacao.AddRange(new List<IValidationRule<string>>() { new EmailValidationRule<string>(),new EspacoEmBrancoValidationRule<string>(),new EmptyValidationRule<string>()});

            Nome = new ValidatableObject<string>();
            Nome.RegrasValidacao.AddRange(new List<IValidationRule<string>>() { new EspacoEmBrancoValidationRule<string>(), new EmptyValidationRule<string>() });
        }
        
        
        
        /// <summary>
        /// Inicializa todas as listas.
        /// </summary>
        private async Task InicializarDados()
        {
            TeesExistentes = await _teeService.ObterTeesExistentes();
            GenerosExistentes = await _generoService.ObterGenerosDisponiveis();
            HandicapMinimo = await _handicapService.ObterHandicapMinimo();
            HandicapMaximo = await _handicapService.ObterHandicapMaximo();
        }
        
        
        
        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do MediadorMensagens.
        /// </summary>
        private void InicializarComunicacaoMediador()
        {
            //Registar à mensagem JogadorAEditar.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogadorAEditar, async p => await PreencherInformacoes((JogadorWrapperViewModel)p));
            base.MensagensUsadas.Add(MediadorMensagensService.ViewModelMensagens.JogadorAEditar);
        }
        
        
        
        /// <summary>
        /// Preenche o formulário com as informações de um jogador.
        /// </summary>
        /// <param name="jogadorAEditar">Jogador cujas informações são utilizadas para preencher o formulário.</param>
        private async Task PreencherInformacoes(JogadorWrapperViewModel jogadorAEditar)
        {
            _jogador = jogadorAEditar;

            //Verifica-se se o jogador está bloqueado. Se está, quer dizer que o utilizador acabou de desbloqueá-lo.
            if(_jogador.Bloqueado)
            {
                //O utilizador acabou de desbloquear este jogador. Todas as informações serão as default.
                Nome.Valor = "Player";

                Email.Valor = "dont-want@aejd.pt";
                
                Foto = ImageSource.FromFile("Player.Png");

                int idGeneroDefault = await _generoService.ObterGeneroDefault();
                Genero = GenerosExistentes.Where(p => p.Id.Equals(idGeneroDefault)).FirstOrDefault();

                int idTeeDefault = await _teeService.ObterTeeDefault();
                Tee = TeesExistentes.Where(p => p.Id.Equals(idTeeDefault)).FirstOrDefault();

                Handicap = await _handicapService.ObterHandicapDefault();
            }
            else
            {
                //Este jogador já estava desbloqueado. Copia-se o valor das propriedades deste jogador, para se
                //poder alterá-las.
                Nome.Valor = _jogador.Nome;

                Email.Valor = _jogador.Email;

                Foto = _jogador.Foto;

                Genero = GenerosExistentes.Where(p => p.Id.Equals(_jogador.Genero.Id)).FirstOrDefault();

                Tee = TeesExistentes.Where(p => p.Id.Equals(_jogador.Tee.Id)).FirstOrDefault();

                Handicap = new HandicapWrapperViewModel(new HandicapModel(_jogador.Handicap.Valor));
            }
        }
        
        
        
        private async Task TirarFoto()
        {
            MediaFile ficheiroImagem = await _cameraService.TirarFoto();

            string fotoBase64 = BytesHandlerHelper.ConverterMediaFileEmBase64(ficheiroImagem);

            if(!fotoBase64.Equals(String.Empty))
                Foto = BytesHandlerHelper.ConverterBase64EmImageSource(fotoBase64);
        }



        private bool ValidarDados()
        {
            return ((Nome.Validate()) && (Email.Validate()));
        }



        private async Task GuardarDados()
        {
            //Validar dados atuais.
            if (!ValidarDados())
                return;

            //Se o jogador antes estava bloqueado, é necessário criar um model para o mesmo.
            if(_jogador.Bloqueado)
            {
                JogadorModel jogadorModel = new JogadorModel(Nome.Valor,Email.Valor,Genero.ObterModel(),Foto,Handicap.ObterModel(),Tee.ObterModel());
                _jogador.DefinirModel(jogadorModel);

                //Avisar que utilizador foi criado.
                MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogadorAdicionado, _jogador);
            }
            else
            {
                //O modelo já está criado. Basta atualizar os valores do mesmo.
                _jogador.Nome = Nome.Valor;
                _jogador.Email = Email.Valor;
                _jogador.Foto = Foto;
                _jogador.Foto = Foto;
                _jogador.Genero = Genero;
                _jogador.Tee = Tee;
                _jogador.Handicap = Handicap;
            }

            _jogador = null;

            base.LimparComunicacaoMediadorMensagens();

            //Fechar PopUp.
            await base.NavigationService.SairDeEditarJogador();
        }
        
        
        
        private async Task RemoverUtilizador()
        {
            _jogador.ResetJogador();

            //Avisar que jogador foi removido.
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogadorRemovido, _jogador);

            //Fechar PopUp.
            await base.NavigationService.SairDeEditarJogador();
        }
        


        private async Task CancelarEdicao()
        {
            //Fechar PopUp.
            await base.NavigationService.SairDeEditarJogador();
        }

    }
}
