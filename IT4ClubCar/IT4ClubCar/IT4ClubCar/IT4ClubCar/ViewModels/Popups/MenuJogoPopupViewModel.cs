using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
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
    class MenuJogoPopupViewModel : BaseViewModel
    {
        private JogoWrapperViewModel _jogo;

        private ICommand _fecharPopupCommad;
        public ICommand FecharPopupCommand
        {
            get
            {
                if (_fecharPopupCommad == null)
                    _fecharPopupCommad = new Command(p => base.NavigationService.SairDeMenuJogo(), p => { return true; });
                return _fecharPopupCommad;
            }
        }

        private ICommand _abrirTerminarJogoCommand;
        public ICommand AbrirTerminarJogoCommand
        {
            get
            {
                if (_abrirTerminarJogoCommand == null)
                    _abrirTerminarJogoCommand = new Command(async p => await AbrirTerminarJogo(), p => { return true; });
                return _abrirTerminarJogoCommand;
            }
        }

        private ICommand _pausarJogoCommand;
        public ICommand PausarJogoCommand
        {
            get
            {
                if (_pausarJogoCommand == null)
                    _pausarJogoCommand = new Command(async p => await PausarJogo(), p => { return true; });
                return _pausarJogoCommand;
            }
        }



        public MenuJogoPopupViewModel(INavigationService navigationService,IDialogService dialogService) : base(navigationService,dialogService)
        {
            InicializarComunicacaoComMediadorMensagens();
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do mediador mensagens.
        /// </summary>
        private void InicializarComunicacaoComMediadorMensagens()
        {
            //Obtém o jogo em uso.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.JogoAtual, p => { _jogo = (JogoWrapperViewModel)p; });
        }



        private async Task AbrirTerminarJogo()
        {
            await base.NavigationService.IrParaTerminarJogo();

            foreach (JogadorWrapperViewModel jogador in _jogo.Jogadores)
                MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogadorATerminarJogo, jogador);

            //Antes de puderem terminar o jogo, mostrar publicidade.
            await base.NavigationService.IrParaPublicidadeDoDia();
        }
        


        private async Task PausarJogo()
        {
            //Fechar Popup.
            await base.NavigationService.SairDeMenuJogo();

            //Abrir Menu Principal.
            await base.NavigationService.IrParaMenuPrincipal();
        }

    }
}
