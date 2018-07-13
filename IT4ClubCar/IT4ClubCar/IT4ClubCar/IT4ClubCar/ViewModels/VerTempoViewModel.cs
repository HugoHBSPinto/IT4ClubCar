using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.Weather;
using IT4ClubCar.IT4ClubCar.Toolbox;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static IT4ClubCar.IT4ClubCar.Models.WeatherModel;

namespace IT4ClubCar.IT4ClubCar.ViewModels
{
    class VerTempoViewModel : BaseViewModel
    {
        #region Propriedades
        /// <summary>
        /// Obtém e define as PrevisoesTempo.
        /// </summary>
        /// <remarks>Quando é definidade também evoca o PropertyChanged para o WeatherAtual e para o TipoTemperaturaEmUso.</remarks>
        private ObservableCollection<WeatherWrapperViewModel> _previsoesTempo;
        public ObservableCollection<WeatherWrapperViewModel> PrevisoesTempo
        {
            get
            {
                return _previsoesTempo;
            }
            set
            {
                _previsoesTempo = value;
                OnPropertyChanged("PrevisoesTempo");
                OnPropertyChanged("WeatherAtual");
                OnPropertyChanged("TipoTemperaturaEmUso");
            }
        }

        /// <summary>
        /// Obtém o WeatherAtual.
        /// </summary>
        /// <remarks>Devolve o primeiro elemento das PrevisoesTempo.</remarks>
        public WeatherWrapperViewModel WeatherAtual
        {
            get
            {
                if (PrevisoesTempo == null || PrevisoesTempo.Count.Equals(0))
                    return null;

                return PrevisoesTempo[0];
            }
        }

        /// <summary>
        /// Obtém o TipoTemperatura em uso.
        /// </summary>
        /// <remarks>Devolve o TipoTemperatura do primeiro elemento das PrevisoesTempo.</remarks>
        public TipoTemperatura TipoTemperaturaEmUso
        {
            get
            {
                if (PrevisoesTempo == null || PrevisoesTempo.Count.Equals(0))
                    return TipoTemperatura.Indefinido;

                return PrevisoesTempo[0].TipoTemperatura;
            }
        }

        /// <summary>
        /// Obtém e define a ActivityIndicatorTool.
        /// </summary>
        private ActivityIndicatorTool _activityIndicatorTool;
        public ActivityIndicatorTool ActivityIndicatorTool
        {
            get
            {
                return _activityIndicatorTool;
            }
            set
            {
                _activityIndicatorTool = value;
                OnPropertyChanged("ActivityIndicatorTool");
            }
        }
        #endregion

        private IWeatherService _weatherService;

        private ICommand _alterarTipoTemperaturaCommand;
        public ICommand AlterarTipoTemperaturaCommand
        {
            get
            {
                if (_alterarTipoTemperaturaCommand == null)
                    _alterarTipoTemperaturaCommand = new Command(p => AlterarTipoTemperatura(), p => { return true; });
                return _alterarTipoTemperaturaCommand;
            }
            set
            {
                _alterarTipoTemperaturaCommand = value;
            }
        }

        private ICommand _fecharJanelaCommand;
        public ICommand FecharJanelaCommand
        {
            get
            {
                if (_fecharJanelaCommand == null)
                    _fecharJanelaCommand = new Command(async p => await FecharJanela(),p => { return true; });
                return _fecharJanelaCommand;
            }
            set
            {
                _fecharJanelaCommand = value;
            }
        }



        public VerTempoViewModel(   INavigationService navigationService,
                                    IDialogService dialogService,
                                    IWeatherService weatherService)
                                    : base(navigationService,dialogService)
        {
            _weatherService = weatherService;

            ActivityIndicatorTool = new ActivityIndicatorTool(activityIndicatorCor: "#4286f4", mensagemAMostrar: "Predicting weather...", backgroundCorVisivel: "#CC000000", backgroundCorEscondido: "#00000000");

            //Obter tempo atual
            Task.Run(async () => await ObterTempoAtual());
        }



        /// <summary>
        /// Obtém o tempo atual e guarda os valores na propriedade WeatherAtual.
        /// </summary>
        private async Task ObterTempoAtual()
        {
            ActivityIndicatorTool.ExecutarRoda();

            PrevisoesTempo = await _weatherService.ObterPrevisoesPorNomeCidade("Alcantarilha");

            ActivityIndicatorTool.PararRoda();
        }



        /// <summary>
        /// Altera os graus em que as temperaturas estão a ser mostradas.
        /// Se estão a ser mostradas em Celsius passam a ser mostradas em Fahrenheit e vice-versa.
        /// </summary>
        private void AlterarTipoTemperatura()
        {
            TipoTemperatura novoTipo = (TipoTemperaturaEmUso.Equals(TipoTemperatura.ºC)) ? TipoTemperatura.ºF : TipoTemperatura.ºC;

            PrevisoesTempo.ToList().ForEach(p => p.AlterarTipoTemperatura(_weatherService,novoTipo));

            OnPropertyChanged("TipoTemperaturaEmUso");
        }



        private async Task FecharJanela()
        {
            await base.NavigationService.IrParaMenuPrincipal();
            LimparMemoria();
        }



        protected override void LimparMemoria()
        {
            PrevisoesTempo = null;

            _weatherService = null;

            AlterarTipoTemperaturaCommand = null;
            FecharJanelaCommand = null;

            base.LimparMemoria();
        }

    }
}
