using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using IT4ClubCar.IT4ClubCar.Services.Campo;
using System.Collections.ObjectModel;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System.Threading.Tasks;
using System.Linq;
using IT4ClubCar.IT4ClubCar.Services.ModoJogo;
using IT4ClubCar.IT4ClubCar.Services.Metrico;

namespace IT4ClubCar.IT4ClubCar.ViewModels
{
    class JogoConfiguracaoViewModel : BaseViewModel
    {
        private ICampoService _campoService;
        private IModoJogoService _modoJogoService;
        private IMetricoService _metricoService;

        private ObservableCollection<CampoWrapperViewModel> _camposExistentes;
        public ObservableCollection<CampoWrapperViewModel> CamposExistentes
        {
            get
            {
                return _camposExistentes;
            }
            set
            {
                _camposExistentes = value;
                OnPropertyChanged("CamposExistentes");
            }
        }

        private CampoWrapperViewModel _campoDefault;
        public CampoWrapperViewModel CampoDefault
        {
            get
            {
                return _campoDefault;
            }
            set
            {
                _campoDefault = value;
                OnPropertyChanged("CampoDefault");
            }
        }

        private ObservableCollection<ModoJogoWrapperViewModel> _modosJogoExistentes;
        public ObservableCollection<ModoJogoWrapperViewModel> ModosJogoExistentes
        {
            get
            {
                return _modosJogoExistentes;
            }
            set
            {
                _modosJogoExistentes = value;
                OnPropertyChanged("ModosJogoExistentes");
            }
        }

        private ModoJogoWrapperViewModel _modoJogoDefault;
        public ModoJogoWrapperViewModel ModoJogoDefault
        {
            get
            {
                return _modoJogoDefault;
            }
            set
            {
                _modoJogoDefault = value;
                OnPropertyChanged("ModoJogoDefault");
            }
        }

        private ObservableCollection<MetricoWrapperViewModel> _metricosExistentes;
        public ObservableCollection<MetricoWrapperViewModel> MetricosExistentes
        {
            get
            {
                return _metricosExistentes;
            }
            set
            {
                _metricosExistentes = value;
                OnPropertyChanged("MetricosExistentes");
            }
        }

        private MetricoWrapperViewModel _metricoDefault;
        public MetricoWrapperViewModel MetricoDefault
        {
            get
            {
                return _metricoDefault;
            }
            set
            {
                _metricoDefault = value;
                OnPropertyChanged("MetricoDefault");
            }
        }



        public JogoConfiguracaoViewModel(
            INavigationService navigationService, 
            IDialogService dialogService,
            ICampoService campoService,
            IModoJogoService modoJogoService,
            IMetricoService metricoService) 
            : base(navigationService, dialogService)
        {
            _campoService = campoService;
            _modoJogoService = modoJogoService;
            _metricoService = metricoService;

            Task.Run(() => InicializarDados());
        }



        private async void InicializarDados()
        {
            CamposExistentes = await _campoService.ObterCamposDisponiveis();
            int idCampoDefault = await _campoService.ObterCampoDefault();
            CampoDefault = CamposExistentes.Where(p => p.Id.Equals(idCampoDefault)).FirstOrDefault();

            ModosJogoExistentes = await _modoJogoService.ObterModosJogoDisponiveis();
            int idModoJogoDefault = await _modoJogoService.ObterModoJogoDefault();
            ModoJogoDefault = ModosJogoExistentes.Where(p => p.Id.Equals(idModoJogoDefault)).FirstOrDefault();

            MetricosExistentes = await _metricoService.ObterMetricosDisponiveis();
            int idMetricoDefault = await _metricoService.ObterMetricoDefault();
            MetricoDefault = MetricosExistentes.Where(p => p.Id.Equals(idMetricoDefault)).FirstOrDefault();
        }



    }
}
