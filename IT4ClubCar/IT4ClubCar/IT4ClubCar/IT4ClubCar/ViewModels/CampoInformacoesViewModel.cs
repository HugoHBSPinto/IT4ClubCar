using IT4ClubCar.IT4ClubCar.Services.Campo;
using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels
{
    class CampoInformacoesViewModel : BaseViewModel
    {
        #region Propriedades
        /// <summary>
        /// Obtém e define os camposExistentes.
        /// </summary>
        private ObservableCollection<CampoWrapperViewModel> _camposExistentes;

        /// <summary>
        /// Obtém e define o indicadorCampoAtual.
        /// </summary>
        /// <remarks>Guarda a posição do CampoAtual na lista de campos existentes.
        /// Quando é definida avisa a view que o CampoAtual mudou.</remarks>
        private int _indicadorCampoAtual;
        public int IndicadorCampoAtual
        {
            get
            {
                return _indicadorCampoAtual;
            }
            set
            {
                _indicadorCampoAtual = value;
                OnPropertyChanged("CampoAtual");
            }
        }

        /// <summary>
        /// Obtém o CampoAtual.
        /// </summary>
        /// <remarks>Quando se obtém o CampoAtual está-se a devolver o campo na posição na lista de campos existentes
        /// indicada pelo indicadorCampoAtual</remarks>
        public CampoWrapperViewModel CampoAtual
        {
            get
            {
                if (_camposExistentes == null)
                    return null;

                return _camposExistentes[_indicadorCampoAtual];
            }
        }
        #endregion

        #region Servicos
        private ICampoService _campoService;
        #endregion

        #region Commands
        private ICommand _verProximoCampoCommand;
        public ICommand VerProximoCampoCommand
        {
            get
            {
                if (_verProximoCampoCommand == null)
                    _verProximoCampoCommand = new Command(p => VerProximoCampo(),p => { return true; });
                return _verProximoCampoCommand;
            }
        }

        private ICommand _verCampoAnteriorCommand;
        public ICommand VerCampoAnteriorCommand
        {
            get
            {
                if (_verCampoAnteriorCommand == null)
                    _verCampoAnteriorCommand = new Command(p => VerCampoAnterior(), p => { return true; });
                return _verCampoAnteriorCommand;
            }
        }

        private ICommand _irParaMenuPrincipalCommand;
        public ICommand IrParaMenuPrincipalCommand
        {
            get
            {
                if (_irParaMenuPrincipalCommand == null)
                    _irParaMenuPrincipalCommand = new Command(async p => await FecharJanela(), p => { return true; });
                return _irParaMenuPrincipalCommand;
            }
        }
        #endregion



        public CampoInformacoesViewModel(   INavigationService navigationService
                                            ,IDialogService dialogService,
                                            ICampoService campoService)
                                            : base(navigationService,dialogService)
        {
            _campoService = campoService;

            Task.Run(async () => await ObterCamposExistentes());
        }



        /// <summary>
        /// Obtém todos os campos existentes.
        /// </summary>
        private async Task ObterCamposExistentes()
        {
            _camposExistentes = await _campoService.ObterCamposDisponiveis();

            if (_camposExistentes.Count.Equals(0))
                return;

            //Definir primeiro campo a mostrar os detalhes.
            IndicadorCampoAtual = 0;
        }



        /// <summary>
        /// Atualiza o IndicadorCampoAtual para apontar para o próximo campo na lista de campos existentes.
        /// </summary>
        private void VerProximoCampo()
        {
            //Ver se este já não é o último campo. Verifica-se se o apontador é igual (-1 porque começa no zero) ao Count da lista de campos
            //existentes.
            if (IndicadorCampoAtual.Equals(_camposExistentes.Count-1))
                return;

            IndicadorCampoAtual++;
        }



        /// <summary>
        /// Atualiza o IndicadorCampoAtual para apontar para o campo anterior na lista de campos existentes.
        /// </summary>
        private void VerCampoAnterior()
        {
            //Ver se este não é o primeiro campo. Verifica-se se o apontador é igual a zero.
            if (IndicadorCampoAtual.Equals(0))
                return;

            IndicadorCampoAtual--;
        }



        private async Task FecharJanela()
        {
            _camposExistentes = null;
            await base.NavigationService.IrParaMenuPrincipal();
        }

    }
}
