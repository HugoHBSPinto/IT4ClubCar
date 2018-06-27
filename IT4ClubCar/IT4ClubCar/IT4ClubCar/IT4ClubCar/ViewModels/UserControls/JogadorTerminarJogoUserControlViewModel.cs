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
                    _enviarScoresCommand = new Command(async p => await EnviarScores(), p => { return true; });
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
            Email.RegrasValidacao.Add(new EmailValidationRule<string>());
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
            }
        }



        /// <summary>
        /// Envia para o email do jogador os seus scores no jogo decorrido.
        /// </summary>
        private async Task EnviarScores()
        {
            string nome = Jogador.Nome;

            string pontos = String.Empty;

            foreach (PontuacaoWrapperViewModel pontuacao in Jogador.Pontuacoes)
                pontos += pontuacao.Pontos + ";";

            string mensagemAEnviar = String.Format("Jogador : {0} / Pontos : {1}",nome,pontos);

            await _emailService.EnviarEmail(Jogador.Email, mensagemAEnviar);
        }



        /// <summary>
        /// Atualiza o email do jogador.
        /// </summary>
        private void AlterarEmail()
        {
            if (!PodeAlterarEmail)
                DesbloquearEntryEmail();
            else
                BloquearEntryEmail();
        }

        /// <summary>
        /// Torna a Entry com o email editável.
        /// </summary>
        private void DesbloquearEntryEmail()
        {
            PodeAlterarEmail = true;
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
