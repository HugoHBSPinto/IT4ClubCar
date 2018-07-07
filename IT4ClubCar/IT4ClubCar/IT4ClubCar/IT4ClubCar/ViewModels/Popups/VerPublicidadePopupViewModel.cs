using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.Publicidade;
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
    class VerPublicidadePopupViewModel : BaseViewModel
    {
        private IPublicidadeService _publicidadeService;

        /// <summary>
        /// Obtém e define a publicidade.
        /// </summary>
        private PublicidadeWrapperViewModel _publicidade;
        public PublicidadeWrapperViewModel Publicidade
        {
            get
            {
                return _publicidade;
            }
            set
            {
                _publicidade = value;
                OnPropertyChanged("Publicidade");
            }
        }

        private ICommand _fecharPopupCommand;
        public ICommand FecharPopupCommand
        {
            get
            {
                if (_fecharPopupCommand == null)
                    _fecharPopupCommand = new Command(async p => await base.NavigationService.SairDePublicidadeDoDia(), p => { return true; });
                return _fecharPopupCommand;
            }
        }



        public VerPublicidadePopupViewModel(INavigationService navigationService,
                                            IDialogService dialogService,
                                            IPublicidadeService publicidadeService) 
                                            : base(navigationService,dialogService)
        {
            _publicidadeService = publicidadeService;

            //Obter publicidade do dia.
            Task.Run(async () => await ObterPublicidadeDoDia());
        }
        


        /// <summary>
        /// Atualiza a propriedade Publicidade com a publicidade do dia.
        /// </summary>
        /// <returns></returns>
        private async Task ObterPublicidadeDoDia()
        {
            Publicidade = await _publicidadeService.ObterPublicidadeDoDia();
        }

    }
}
