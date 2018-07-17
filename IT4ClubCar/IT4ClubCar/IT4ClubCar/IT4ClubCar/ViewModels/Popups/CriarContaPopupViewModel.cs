using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.Services.Buracos;
using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Genero;
using IT4ClubCar.IT4ClubCar.Services.Jogador;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.Tee;
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
    class CriarContaPopupViewModel : BaseViewModel
    {
        #region Propriedades
        /// <summary>
        /// Obtém e define o _jogador.
        /// </summary>
        private JogadorWrapperViewModel _jogador;

        /// <summary>
        /// Obtém e define o NomeJogador.
        /// </summary>
        private ValidatableObject<string> _nomeJogador;
        public ValidatableObject<string> NomeJogador
        {
            get
            {
                return _nomeJogador;
            }
            set
            {
                _nomeJogador = value;
                OnPropertyChanged("NomeJogador");
            }
        }

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
        /// Obtém e define o Senha.
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
        /// Obtém e define o EmailEmUso.
        /// </summary>
        /// <remarks>Torna-se true quando o utilizador insere um email que já está a ser usado por outro.</remarks>
        private bool _emailEmUso;
        public bool EmailEmUso
        {
            get
            {
                return _emailEmUso;
            }
            set
            {
                _emailEmUso = value;
                OnPropertyChanged("EmailEmUso");
            }
        }

        /// <summary>
        /// Obtém e define o NomeEmUso.
        /// </summary>
        /// <remarks>Torna-se true quando o utilizador insere um email que já está a ser usado por outro.</remarks>
        private bool _nomeEmUso;
        public bool NomeEmUso
        {
            get
            {
                return _nomeEmUso;
            }
            set
            {
                _nomeEmUso = value;
                OnPropertyChanged("NomeEmUso");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Obtém o CriarContaCommand.
        /// </summary>
        /// <remarks>Lazy Loading para a criação do Command.</remarks>
        private ICommand _criarContaCommand;
        public ICommand CriarContaCommand
        {
            get
            {
                if (_criarContaCommand == null)
                    _criarContaCommand = new Command(async p => await CriarConta(),p => { return true; });
                return _criarContaCommand;
            }
        }

        /// <summary>
        /// Obtém o FecharPopupCommand.
        /// </summary>
        /// <remarks>Lazy Loading para a criação do Command.</remarks>
        private ICommand _fecharPopupCommand;
        public ICommand FecharPopupCommand
        {
            get
            {
                if (_fecharPopupCommand == null)
                    _fecharPopupCommand = new Command(p => { }, p => { return true; });
                return _fecharPopupCommand;
            }
        }
        #endregion

        #region Services
        private IJogadorService _jogadorService;
        private ITeeService _teeService;
        private IGeneroService _generoService;
        private IHandicapService _handicapService;
        #endregion



        public CriarContaPopupViewModel(INavigationService navigationService,
                                        IDialogService dialogService,
                                        IJogadorService jogadorService,
                                        ITeeService teeService,
                                        IGeneroService generoService,
                                        IHandicapService handicapService)
                                        :base(navigationService,dialogService)
        {
            _jogadorService = jogadorService;
            _teeService = teeService;
            _generoService = generoService;
            _handicapService = handicapService;

            InicializarValidatableObjects();

            InicializarComunicacaoComMediador();
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do MediadorMensagens.
        /// </summary>
        private void InicializarComunicacaoComMediador()
        {
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogadorAEditar, p => { _jogador = (JogadorWrapperViewModel)p; });
        }



        /// <summary>
        /// Inicializa todas as propriedades do tipo ValidatableObject e atribui as regras
        /// de validação necessárias.
        /// </summary>
        private void InicializarValidatableObjects()
        {
            NomeJogador = new ValidatableObject<string>();
            NomeJogador.Valor = String.Empty;
            NomeJogador.RegrasValidacao.AddRange(new List<IValidationRule<string>>() { new EmptyValidationRule<string>(), new EspacoEmBrancoValidationRule<string>() });

            Email = new ValidatableObject<string>();
            Email.Valor = String.Empty;
            Email.RegrasValidacao.AddRange(new List<IValidationRule<string>>() { new EmptyValidationRule<string>(), new EspacoEmBrancoValidationRule<string>(), new EmailValidationRule<string>() });

            Senha = new ValidatableObject<string>();
            Senha.Valor = String.Empty;
            Senha.RegrasValidacao.AddRange(new List<IValidationRule<string>>() { new EmptyValidationRule<string>(), new EspacoEmBrancoValidationRule<string>() });
        }



        private async Task CriarConta()
        {
            //Verificar se todos os campos foram preenchidos corretamente.
            if(ValidarEstruturaCampos())
            {
                //Verificar se os dados inseridos pelo utilizador já não estão a ser usados.
                await ValidarDadosBD();
                if ((!EmailEmUso)&&(!NomeEmUso))
                {
                    //Criar Conta.
                    //Criar novo model para o wrapper.
                    int id = await _jogadorService.ObterJogadorUltimoId();
                    id++;
                    string nome = NomeJogador.Valor;
                    string email = Email.Valor;
                    string fotoBase64 = await _jogadorService.ObterFotoPessoaDefaultAsync();
                    TeeWrapperViewModel tee = await _teeService.ObterTeeDefault();
                    GeneroWrapperViewModel genero = await _generoService.ObterGeneroDefault();
                    HandicapWrapperViewModel handicap = await _handicapService.ObterHandicapDefault();

                    _jogador.DefinirModel(new JogadorModel(nome, email, genero.ObterModel(), handicap.ObterModel(), tee.ObterModel(), id: id, senha: Senha.Valor,foto: fotoBase64));

                    await _jogadorService.InserirNovoJogador(_jogador);

                    //Ir para janela EditarJogador.
                    await base.NavigationService.SairDeCriarConta();
                    await base.NavigationService.IrParaEditarJogador();
                    MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogadorAEditar, _jogador);

                    //Caso antes estivessem true devido a uma validação anterior, fazer reset, tornando-as false.
                    EmailEmUso = false;
                    NomeEmUso = false;
                }
            }
        }



        /// <summary>
        /// Verifica se os dados inseridos pelo utilizador são, estruturalmente, válidos.
        /// </summary>
        /// <returns></returns>
        private bool ValidarEstruturaCampos()
        {
            //Não se devolve logo tudo num só return, para caso algum corra mal, os outros métodos sejam executados na mesma.
            bool nomeValido = NomeJogador.Validate();
            bool emailValido = Email.Validate();
            bool senhaValida = Senha.Validate();

            return ((nomeValido)&&(emailValido)&&(senhaValida));
        }



        private async Task ValidarDadosBD()
        {
            EmailEmUso = await VerificarSeEmailEstaEmUso();
            NomeEmUso = await VerificarSeNomeEstaEmUso();
        }



        private async Task<bool> VerificarSeEmailEstaEmUso()
        {
            return await _jogadorService.VerificarSeEmailEstaEmUso(Email.Valor);
        }



        private async Task<bool> VerificarSeNomeEstaEmUso()
        {
            return await _jogadorService.VerificarSeNomeEstaEmUso(NomeJogador.Valor);
        }

    }
}
