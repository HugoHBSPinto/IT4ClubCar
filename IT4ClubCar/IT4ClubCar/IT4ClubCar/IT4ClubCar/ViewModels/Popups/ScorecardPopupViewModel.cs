using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Popups
{
    class ScorecardPopupViewModel : BaseViewModel
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



        public ScorecardPopupViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService,dialogService)
        {
            
        }



        private async Task FecharPopup()
        {
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.AFecharPopup, null);

            MediadorMensagensService.Instancia.ResetMensagens(MediadorMensagensService.ViewModelMensagens.NomeAMostrar);
            MediadorMensagensService.Instancia.ResetMensagens(MediadorMensagensService.ViewModelMensagens.PontuacaoAMostrar);

            await base.NavigationService.SairDeScorecard();
        }

    }
}
