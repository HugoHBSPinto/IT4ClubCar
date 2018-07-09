using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Popups
{
    class ProTipPopupViewModel : BaseViewModel
    {
        /// <summary>
        /// Obtém e define a Dica.
        /// </summary>
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
                    _fecharPopupCommand = new Command(async p => await FecharPopup(), p => { return true; });
                return _fecharPopupCommand;
            }
            set
            {
                _fecharPopupCommand = null;
            }
        }



        public ProTipPopupViewModel(INavigationService navigationService, 
                                    IDialogService dialogService) 
                                    : base(navigationService,dialogService)
        {
            InicializarComunicacaoComMediadorMensagens();
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do MediadorMensagens.
        /// </summary>
        private void InicializarComunicacaoComMediadorMensagens()
        {
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.ProTipConteudo, p => DefinirDica(p as string));
            base.MensagensUsadas.Add(MediadorMensagensService.ViewModelMensagens.ProTipConteudo);
        }



        /// <summary>
        /// Define a propriedade Dica.
        /// </summary>
        /// <param name="dica">Texto a ser mostrado como a dica.</param>
        private void DefinirDica(string dica)
        {
            Dica = dica;
        }



        private async Task FecharPopup()
        {
            await base.NavigationService.SairDeProTip();
            LimparMemoria();
        }



        protected override void LimparMemoria()
        {
            Dica = null;
            FecharPopupCommand = null;
            base.LimparMemoria();
        }
    }
}
