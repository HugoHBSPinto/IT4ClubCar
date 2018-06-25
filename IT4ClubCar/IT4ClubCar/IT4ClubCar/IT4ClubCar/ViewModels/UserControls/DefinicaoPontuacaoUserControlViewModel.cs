using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.UserControls
{
    class DefinicaoPontuacaoUserControlViewModel : BaseViewModel
    {
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

        private BuracoWrapperViewModel _buraco;
        public BuracoWrapperViewModel Buraco
        {
            get
            {
                return _buraco;
            }
            set
            {
                _buraco = value;
                OnPropertyChanged("Buraco");
            }
        }

        private int _pontos;
        public int Pontos
        {
            get
            {
                return _pontos;
            }
            set
            {
                _pontos = value;
                OnPropertyChanged("Pontos");
            }
        }

        #region Commmands
        private ICommand _adicionarPontoCommand;
        public ICommand AdicionarPontoCommand
        {
            get
            {
                if (_adicionarPontoCommand == null)
                    _adicionarPontoCommand = new Command(p => AdicionarPonto(), p => { return true; });
                return _adicionarPontoCommand;
            }
        }

        private ICommand _removerPontoCommand;
        public ICommand RemoverPontoCommand
        {
            get
            {
                if (_removerPontoCommand == null)
                    _removerPontoCommand = new Command(p => SubtrairPonto(), p => { return true; });
                return _removerPontoCommand;
            }
        }
        #endregion



        public DefinicaoPontuacaoUserControlViewModel(INavigationService navigationService,IDialogService dialogService) : base(navigationService,dialogService)
        {
            InicializarComunicacaoMediadorMensagens();
        }



        /// <summary>
        /// Regista o ViewModel às mensagens necessárias do MediadorMensagens.
        /// </summary>
        private void InicializarComunicacaoMediadorMensagens()
        {
            //Registar ao JogadorAEditar.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogadorPontuacaoAEditar, p => InicializarJogador((JogadorWrapperViewModel)p));
            //Registar ao buraco a editar.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.BuracoPontuacaoAEditar, p => InicializarPontos((BuracoWrapperViewModel)p));
            //Registar ao evento de guardar.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.GuardarPontuacoes, p => GuardarPontos());
        }



        /// <summary>
        /// Inicializa o jogador, caso ainda não tenha sido inicializado.
        /// </summary>
        private void InicializarJogador(JogadorWrapperViewModel jogador)
        {
            if ((Jogador == null) && (!jogador.EmUso))
            {
                Jogador = jogador;
                Jogador.EmUso = true;
            }
        }



        /// <summary>
        /// Define o buraco e inicializa os Pontos com a pontuação do jogador nesse buraco.
        /// </summary>
        private void InicializarPontos(BuracoWrapperViewModel buraco)
        {
            Buraco = buraco;

            if(Jogador != null)
                Pontos = Jogador.Pontuacoes.Where(p => p.Buraco.Numero.Equals(Buraco.Numero)).FirstOrDefault().Pontos;
        }



        /// <summary>
        /// Adiciona um ponto à pontuação do jogador neste buraco.
        /// </summary>
        private void AdicionarPonto()
        {
            Pontos++;
        }



        /// <summary>
        /// Se possível, remove um ponto à pontuação do jogador neste buraco.
        /// </summary>
        private void SubtrairPonto()
        {
            //Subtrair um ponto SE a pontuação já não for zero.
            if (!Pontos.Equals(0))
                Pontos--;
        }



        /// <summary>
        /// Atualiza a propriedade da pontuação do jogador neste buraco com o valor da propriedade Pontos.
        /// </summary>
        private void GuardarPontos()
        {
            if ((Jogador != null) && (Buraco != null))
            { 
                Jogador.Pontuacoes.Where(p => p.Buraco.Numero.Equals(Buraco.Numero)).FirstOrDefault().Pontos = Pontos;
                Jogador.EmUso = false;
            }
        }

    }
}
