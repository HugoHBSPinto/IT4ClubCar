using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;

namespace IT4ClubCar.IT4ClubCar.ViewModels.UserControls
{
    class JogadorUserControlViewModel : BaseViewModel
    {
        public JogadorWrapperViewModel Jogador { get; set; }

        private ICommand _editarJogadorCommand;
        public ICommand EditarJogadorCommand
        {
            get
            {
                if (_editarJogadorCommand == null)
                    _editarJogadorCommand = new Command(async p => await EditarJogador(), p => { return true; });
                return _editarJogadorCommand;
            }
        }



        public JogadorUserControlViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService,dialogService)
        {
            Jogador = new JogadorWrapperViewModel();
        }



        /// <summary>
        /// Abre o popup EditarJogador com as informações deste jogador.
        /// </summary>
        private async Task EditarJogador()
        {
            await base.NavigationService.IrParaEditarJogador();
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.JogadorAEditar, Jogador);
        }


    }
}
