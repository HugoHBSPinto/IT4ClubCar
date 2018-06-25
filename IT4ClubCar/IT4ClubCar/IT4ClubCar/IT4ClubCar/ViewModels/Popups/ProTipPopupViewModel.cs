using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Popups
{
    class ProTipPopupViewModel : BaseViewModel
    {
        private string _dica;
        public string Dica
        {
            get
            {
                return _dica;
            }
            set
            {
                _dica = value;
                OnPropertyChanged("Dica");
            }
        }

        private ICommand _fecharPopupCommand;
        public ICommand FecharPopupCommand
        {
            get
            {
                if (_fecharPopupCommand == null)
                    _fecharPopupCommand = new Command(async p => { await base.NavigationService.SairDeProTip(); }, p => { return true; });
                return _fecharPopupCommand;
            }
        }



        public ProTipPopupViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService,dialogService)
        {
            InicializarComunicacaoComMediadorMensagens();
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do MediadorMensagens.
        /// </summary>
        private void InicializarComunicacaoComMediadorMensagens()
        {
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.ProTipConteudo, p => { Dica = (string)p; });
        }

    }
}
