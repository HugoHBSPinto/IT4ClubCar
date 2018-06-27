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

    }
}
