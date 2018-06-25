using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.UserControls
{
    class JogadorPontuacaoUserControlViewModel : BaseViewModel
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



        public JogadorPontuacaoUserControlViewModel(INavigationService navigationService,IDialogService dialogService) : base(navigationService,dialogService)
        {
            InicializarComunicacaoMediadorMensagens();
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do Mediador Mensagens.
        /// </summary>
        private void InicializarComunicacaoMediadorMensagens()
        {
            //Jogador enviado para obter pontuação.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.PontuacaoAMostrar, p => InicializarPontuacoes((JogadorWrapperViewModel)p));
            //Popup a ser fechado.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.AFecharPopup, p => LibertarJogador());
        }



        /// <summary>
        /// Inicializa a propriedade Pontuacoes, caso ainda não tenha sido inicializada.
        /// </summary>
        /// <param name="jogador">Jogador cujas pontuações vão ser usadas para definir a propriedade Pontuacoes.</param>
        private void InicializarPontuacoes(JogadorWrapperViewModel jogador)
        {
            if ((Jogador == null) && (!jogador.PontuacaoEmUso))
            {
                Jogador = jogador;
                Jogador.PontuacaoEmUso = true;
            }
        }



        /// <summary>
        /// Altera a propriedade PontuacaoEmUso do jogador para false.
        /// </summary>
        private void LibertarJogador()
        {
            if(Jogador!=null)
                Jogador.PontuacaoEmUso = false;
        }

    }
}
