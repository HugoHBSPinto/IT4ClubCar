using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Jogador;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Validacoes;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Popups
{
    class LogInPopupViewModel : BaseViewModel
    {
        #region Propriedades
        /// <summary>
        /// Obtém e define o _jogador.
        /// </summary>
        private JogadorWrapperViewModel _jogador;

        /// <summary>
        /// Obtém e define o Email.
        /// </summary>
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

        /// <summary>
        /// Obtém e define a Senha.
        /// </summary>
        private ValidatableObject<string> _senha;
        public ValidatableObject<string> Senha
        {
            get
            {
                return _senha;
            }
            set
            {
                _senha = value;
                OnPropertyChanged("Senha");
            }
        }

        /// <summary>
        /// Obtém e define os DadosInvalidos.
        /// </summary>
        private bool _dadosInvalidos;
        public bool DadosInvalidos
        {
            get
            {
                return _dadosInvalidos;
            }
            set
            {
                _dadosInvalidos = value;
                OnPropertyChanged("DadosInvalidos");
            }
        }
        #endregion

        #region Commands
        private ICommand _jogarComoGuestCommand;
        public ICommand JogarComoGuestCommand
        {
            get
            {
                if (_jogarComoGuestCommand == null)
                    _jogarComoGuestCommand = new Command(async p => await JogarComoGuest(),p => { return true; });
                return _jogarComoGuestCommand;
            }
        }

        private ICommand _fazerLogInCommand;
        public ICommand FazerLogInCommand
        {
            get
            {
                if (_fazerLogInCommand == null)
                    _fazerLogInCommand = new Command(async p => await FazerLogIn(), p => { return true; });
                return _fazerLogInCommand;
            }
        }
        #endregion

        #region Services
        private IJogadorService _jogadorService;
        #endregion



        public LogInPopupViewModel( INavigationService navigationService, 
                                    IDialogService dialogService,
                                    IJogadorService jogadorService) 
                                    : base(navigationService,dialogService)
        {
            _jogadorService = jogadorService;

            InicializarComunicacaoMediador();
            InicializarValidacoes();
        }



        /// <summary>
        /// Inicializa as propriedades Email e Senha, associando a cada uma às regras de validação
        /// necessárias.
        /// </summary>
        private void InicializarValidacoes()
        {
            Email = new ValidatableObject<string>();
            Email.Valor = String.Empty;
            Email.RegrasValidacao.AddRange(new List<IValidationRule<string>>() { new EmailValidationRule<string>(), new EspacoEmBrancoValidationRule<string>(), new EmptyValidationRule<string>() });

            Senha = new ValidatableObject<string>();
            Senha.Valor = String.Empty;
            Senha.RegrasValidacao.AddRange(new List<IValidationRule<string>>() { new EspacoEmBrancoValidationRule<string>(), new EmptyValidationRule<string>() });
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do MediadorMens
        /// </summary>
        private void InicializarComunicacaoMediador()
        {
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogadorAEditar, p => { _jogador = (JogadorWrapperViewModel)p; });
            base.MensagensUsadas.Add(MediadorMensagensService.ViewModelMensagens.JogadorAEditar);
        }



        /// <summary>
        /// Abre a janela EditarJogador com os dados default.
        /// </summary>
        private async Task JogarComoGuest()
        {
            await base.NavigationService.SairDeLogIn();
            await base.NavigationService.IrParaEditarJogador();
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogadorAEditar, _jogador);
            base.LimparMemoria();
        }



        /// <summary>
        /// Valida os dados do jogador e caso sejam válidos obtém o Jogador associado ao email inserido.
        /// </summary>
        private async Task FazerLogIn()
        {
            if ((ValidarCampos()) && (await ValidarDadosAcesso()))
            {
                //Se os campos estão a vermelho, remover erros.
                if (DadosInvalidos)
                    DadosInvalidos = false;
                //Obter dados do jogador associado ao email.
                JogadorWrapperViewModel jogadorAssociado = await _jogadorService.ObterJogador(Email.Valor);
                _jogador.DefinirModel(jogadorAssociado.ObterModel());
                //Abrir popup para editar dados do jogador.
                await base.NavigationService.SairDeLogIn();
                await base.NavigationService.IrParaEditarJogador();
                MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogadorAEditar, _jogador);
                //Remover dados de memória.
                base.LimparMemoria();
            }
            else
                DadosInvalidos = true;
        }



        /// <summary>
        /// Valida o email e senha inserido pelo utilizador em termos de estrutura.
        /// </summary>
        private bool ValidarCampos()
        {
            return ((Email.Validate()) && (Senha.Validate()));
        }



        /// <summary>
        /// Verifica se existe algum jogador com os dados de acesso passados pelo utilizador.
        /// </summary>
        private async Task<bool> ValidarDadosAcesso()
        {
            return await _jogadorService.VerificarSeJogadorExiste(Email.Valor, Senha.Valor);
        }

    }
}
