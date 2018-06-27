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

        /// <summary>
        /// Obtém e define o IsActivityIndicatorVisivel.
        /// </summary>
        private bool _isActivityIndicatorVisivel;
        public bool IsActivityIndicatorVisivel
        {
            get
            {
                return _isActivityIndicatorVisivel;
            }
            set
            {
                _isActivityIndicatorVisivel = value;
                OnPropertyChanged("IsActivityIndicatorVisivel");
            }
        }

        /// <summary>
        /// Obtém e define o IsActivityIndicatorACorrer.
        /// </summary>
        private bool _isActivityIndicatorACorrer;
        public bool IsActivityIndicatorACorrer
        {
            get
            {
                return _isActivityIndicatorACorrer;
            }
            set
            {
                _isActivityIndicatorACorrer = value;
                OnPropertyChanged("IsActivityIndicatorACorrer");
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
            IsActivityIndicatorACorrer = true;
            IsActivityIndicatorVisivel = true;

            InicializarComunicacaoComMediadorMensagens();
        }



        /// <summary>
        /// Regista o viewmodel às mensagens necessárias do MediadorMensagens.
        /// </summary>
        private void InicializarComunicacaoComMediadorMensagens()
        {
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.ProTipConteudo, p => DefinirDica(p as string));
        }



        /// <summary>
        /// Define a propriedade Dica, escondendo a ActivityIndicator.
        /// </summary>
        /// <param name="dica"></param>
        private void DefinirDica(string dica)
        {
            Dica = dica;

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
             {
                 IsActivityIndicatorACorrer = false;
                 IsActivityIndicatorVisivel = false;
                 return true;
             });
        }

    }
}
