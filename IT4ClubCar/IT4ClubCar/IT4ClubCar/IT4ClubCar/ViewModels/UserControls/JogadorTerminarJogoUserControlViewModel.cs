using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.EmailService;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Validacoes;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.UserControls
{
    class JogadorTerminarJogoUserControlViewModel : BaseViewModel
    {
        private IEmailService _emailService;

        private JogadorWrapperViewModel _jogador;
        public JogadorWrapperViewModel Jogador
        {
            get
            {
                return _jogador;
            }
            set
            {
                _jogador = value;
                OnPropertyChanged("Jogador");
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

        private bool _podeAlterarEmail;
        public bool PodeAlterarEmail
        {
            get
            {
                return _podeAlterarEmail;
            }
            set
            {
                _podeAlterarEmail = value;
                OnPropertyChanged("PodeAlterarEmail");
            }
        }

        private ICommand _alterarEmailCommand;
        public ICommand AlterarEmailCommand
        {
            get
            {
                if (_alterarEmailCommand == null)
                    _alterarEmailCommand = new Command(p => AlterarEmail(),p => { return true; });
                return _alterarEmailCommand;
            }
        }

        private ICommand _enviarScoresCommand;
        public ICommand EnviarScoresCommand
        {
            get
            {
                if (_enviarScoresCommand == null)
                    _enviarScoresCommand = new Command(async p => await EnviarScores(), p => VerificarSePodeClicarEmEnviar());
                return _enviarScoresCommand;
            }
        }



        public JogadorTerminarJogoUserControlViewModel(INavigationService navigationService, 
                                                       IDialogService dialogService,
                                                       IEmailService emailService) 
                                                       : base(navigationService, dialogService)
        {
            _emailService = emailService;

            InicializarComunicacaoComMediadorMensagens();

            Email = new ValidatableObject<string>();
            Email.RegrasValidacao.AddRange(new List<IValidationRule<string>>() { new EmailValidationRule<string>(), new EspacoEmBrancoValidationRule<string>(), new EmptyValidationRule<string>() });
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do mediador mensagens.
        /// </summary>
        private void InicializarComunicacaoComMediadorMensagens()
        {
            //Jogador que quer terminar jogo.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogadorATerminarJogo, p => InicializarJogador((JogadorWrapperViewModel)p));
            //Popup a fechar.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.AFecharPopupTerminarJogo, p => Limpar());
        }



        /// <summary>
        /// Inicializa propriedade Jogador.
        /// </summary>
        private void InicializarJogador(JogadorWrapperViewModel jogador)
        {
            if((Jogador==null) && (!jogador.EmUso))
            {
                Jogador = jogador;
                Jogador.EmUso = true;
                Email.Valor = Jogador.Email;

                ((Command)EnviarScoresCommand).ChangeCanExecute();
            }
        }



        /// <summary>
        /// Abre um popup com o scorecard e um botão para enviar uma foto do mesmp.
        /// </summary>
        private async Task EnviarScores()
        {
            await base.NavigationService.IrParaScorecard();
            //Enviar jogador a enviar scorecard.
            JogoWrapperViewModel Jogo = null;
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogoAtual, p => { Jogo = p as JogoWrapperViewModel; });
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.PedirPorJogo,null);
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogoAtual, Jogo);
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogadorAEnviarScorecard, Jogador);
        }



        /// <summary>
        /// Atualiza o email do jogador.
        /// </summary>
        private void AlterarEmail()
        {
            if (!PodeAlterarEmail)
                DesbloquearEntryEmail();
            else
            {
                BloquearEntryEmail();
                ((Command)EnviarScoresCommand).ChangeCanExecute();
            }
        }

        /// <summary>
        /// Torna a Entry com o email editável.
        /// </summary>
        private void DesbloquearEntryEmail()
        {
            PodeAlterarEmail = true;
            ((Command)EnviarScoresCommand).ChangeCanExecute();
        }

        /// <summary>
        /// Torna a entry com o email não editável, atualizando a propriedade Email do jogador.
        /// </summary>
        private void BloquearEntryEmail()
        {
            if (Email.Validate())
            {
                PodeAlterarEmail = false;
                Jogador.Email = Email.Valor;
            }
        }



        /// <summary>
        /// Bloqueia ou desbloqueia o botão "Enviar Scores".
        /// </summary>
        /// <returns>True se deve estar desbloqueado,false se deve estar bloqueado.</returns>
        private bool VerificarSePodeClicarEmEnviar()
        {
            //Se o email ainda não estiver carregado em memória bloquear botão.
            if (Email.Valor == null)
                return false;

            //Se o jogador estiver a editar o email, bloquear o botão.
            if (PodeAlterarEmail)
                return false;

            //Se o jogador não tiver alterado o email, sendo ainda o default, bloquear o botão.
            return !Email.Valor.Equals("dont-want@aejd.pt");
        }



        /// <summary>
        /// Liberta o jogador usado.
        /// </summary>
        private void Limpar()
        {
            if(Jogador!=null)
            {
                Jogador.EmUso = false;
                Jogador = null;
            }
        }
    }
}
