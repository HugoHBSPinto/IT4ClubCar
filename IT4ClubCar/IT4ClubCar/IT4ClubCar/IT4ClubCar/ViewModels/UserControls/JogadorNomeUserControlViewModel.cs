using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.ViewModels.UserControls
{
    class JogadorNomeUserControlViewModel : BaseViewModel
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



        public JogadorNomeUserControlViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService,dialogService)
        {
            InicializarComunicacaoMediadorMensagens();
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do MediadorMensagens.
        /// </summary>
        private void InicializarComunicacaoMediadorMensagens()
        {
            //Jogador enviado para obter nome.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.NomeAMostrar, p => InicializarNome((JogadorWrapperViewModel)p));
            //Popup a ser fechado.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.AFecharPopup, p => LibertarJogador());
        }



        /// <summary>
        /// Inicializa a propriedade Nome, caso ainda não tenha sido inicializado.
        /// </summary>
        /// <param name="jogador">Jogador cujo nome vai ser usado para inicializar a propriedade Nome.</param>
        private void InicializarNome(JogadorWrapperViewModel jogador)
        {
            if((Jogador==null) && (!jogador.NomeEmUso))
            {
                Jogador = jogador;
                Jogador.NomeEmUso = true;
            }
        }



        private void LibertarJogador()
        {
            if (Jogador != null)
            {
                Jogador.NomeEmUso = false;
                Jogador = null;
            }
        }

    }
}
