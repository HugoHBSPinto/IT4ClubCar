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

        /// <summary>
        /// Obtém e define a CorDeFundo.
        /// </summary>
        private string _corDeFundo;
        public string CorDeFundo
        {
            get
            {
                return _corDeFundo;
            }
            set
            {
                _corDeFundo = value;
                OnPropertyChanged("CorDeFundo");
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
            set
            {
                _verProximoCampoCommand = value;
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
            set
            {
                _verCampoAnteriorCommand = value;
            }
        }

        private ICommand _fecharJanelaCommand;
        public ICommand FecharJanelaCommand
        {
            get
            {
                if (_fecharJanelaCommand == null)
                    _fecharJanelaCommand = new Command(async p => await FecharJanela(), p => { return true; });
                return _fecharJanelaCommand;
            }
            set
            {
                _fecharJanelaCommand = value;
            }
        }
        #endregion



        public CampoInformacoesViewModel(   INavigationService navigationService
                                            ,IDialogService dialogService,
                                            ICampoService campoService)
                                            : base(navigationService,dialogService)
        {
            _campoService = campoService;

            CorDeFundo = "#00000000";

            Task.Run(async () => await ObterCamposExistentes());
        }



        /// <summary>
        /// Obtém todos os campos existentes.
        /// </summary>
        private async Task ObterCamposExistentes()
        {
            IsActivityIndicatorACorrer = true;
            IsActivityIndicatorVisivel = true;
            CorDeFundo = "#CC000000";

            _camposExistentes = await _campoService.ObterCamposDisponiveis();

            if (_camposExistentes.Count.Equals(0))
                return;

            //Definir primeiro campo a mostrar os detalhes.
            IndicadorCampoAtual = 0;

            IsActivityIndicatorACorrer = false;
            IsActivityIndicatorVisivel = false;
            CorDeFundo = "#00000000";
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
            await base.NavigationService.IrParaMenuPrincipal();
            LimparMemoria();
        }



        protected override void LimparMemoria()
        {
            _camposExistentes = null;

            _campoService = null;

            _verCampoAnteriorCommand = null;
            _verProximoCampoCommand = null;
            _fecharJanelaCommand = null;

            base.LimparMemoria();
        }
    }
}
