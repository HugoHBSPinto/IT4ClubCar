using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.EmailService;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Popups
{
    class TerminarJogoPopupViewModel : BaseViewModel
    {
        #region Commands
        private ICommand _fecharPopupCommand;
        public ICommand FecharPopupCommand
        {
            get
            {
                if (_fecharPopupCommand == null)
                    _fecharPopupCommand = new Command(async p => await FecharPopup(), p => { return true; });
                return _fecharPopupCommand;
            }
        }

        private ICommand _terminarJogoCommand;
        public ICommand TerminarJogoCommand
        {
            get
            {
                if (_terminarJogoCommand == null)
                    _terminarJogoCommand = new Command(async p => await TerminarJogo(), p => { return true; });
                return _terminarJogoCommand;
            }
        }
        #endregion



        public TerminarJogoPopupViewModel(INavigationService navigationService,
                                          IDialogService dialogService)
                                          : base(navigationService,dialogService)
        {
            
        }


        private async Task FecharPopup()
        {
            await base.NavigationService.SairDeTerminarJogo();
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.AFecharPopupTerminarJogo, null);
            MediadorMensagensService.Instancia.ResetMensagens(MediadorMensagensService.ViewModelMensagens.AFecharPopupTerminarJogo);
            MediadorMensagensService.Instancia.ResetMensagens(MediadorMensagensService.ViewModelMensagens.JogadorATerminarJogo);
        }



        private async Task TerminarJogo()
        {
            await base.NavigationService.SairDeTerminarJogo();
            await base.NavigationService.SairDeMenuJogo();
            await base.NavigationService.IrParaMenuPrincipal();
        }

    }
}
