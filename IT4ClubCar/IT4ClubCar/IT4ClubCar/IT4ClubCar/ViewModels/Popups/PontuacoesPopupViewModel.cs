using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Popups
{
    class PontuacoesPopupViewModel : BaseViewModel
    {
        private ICommand _fecharPopupCommand;
        public ICommand FecharPopupCommand
        {
            get
            {
                if (_fecharPopupCommand == null)
                    _fecharPopupCommand = new Command(async p => await GuardarPontuacoes(), p => { return true; });
                return _fecharPopupCommand;
            }
        }

        

        public PontuacoesPopupViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService,dialogService)
        {
            
        }



        /// <summary>
        /// Fecha o popup, antes guardando todas as pontuações.
        /// </summary>
        private async Task GuardarPontuacoes()
        {
            //Avisar a todos os usercontrols para guardar os pontos definidos.
            MediadorMensagensService.Instancia.Avisar(MediadorMensagensService.ViewModelMensagens.GuardarPontuacoes,null);

            MediadorMensagensService.Instancia.ResetMensagens(MediadorMensagensService.ViewModelMensagens.JogadorPontuacaoAEditar);
            MediadorMensagensService.Instancia.ResetMensagens(MediadorMensagensService.ViewModelMensagens.BuracoPontuacaoAEditar);

            //Fechar popup.
            await base.NavigationService.SairDePontuacoes();
        }

    }
}
