using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Toolbox
{
    class ActivityIndicatorTool : BaseToolbox
    {
        /// <summary>
        /// Obtém e define IsVisivel.
        /// </summary>
        private bool _isVisivel;
        public bool IsVisivel
        {
            get
            {
                return _isVisivel;
            }
            set
            {
                _isVisivel = value;
                OnPropertyChanged("IsVisivel");
            }
        }

        /// <summary>
        /// Obtém e define a BackgroundColorEscondido.
        /// </summary>
        private string _backgroundCorEscondido;

        /// <summary>
        /// Obtém e define a BackgroundCorVisivel.
        /// </summary>
        private string _backgroundCorVisivel;

        /// <summary>
        /// Obtém e define a BackgroundCorEmUso.
        /// </summary>
        private string _backgroundCorEmUso;
        public string BackgroundCorEmUso
        {
            get
            {
                return _backgroundCorEmUso;
            }
            set
            {
                _backgroundCorEmUso = value;
                OnPropertyChanged("BackgroundCorEmUso");
            }
        }

        /// <summary>
        /// Obtém e define IsRunning.
        /// </summary>
        private bool _isRunning;
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                OnPropertyChanged("IsRunning");
            }
        }

        /// <summary>
        /// Obtém e define a ActivityIndicatorCor.
        /// </summary>
        private string _activityIndicatorCor;
        public string ActivityIndicatorCor
        {
            get
            {
                return _activityIndicatorCor;
            }
            set
            {
                _activityIndicatorCor = value;
                OnPropertyChanged("ActivityIndicatorCor");
            }
        }

        /// <summary>
        /// Obtém e defina a MensagemAMostrar.
        /// </summary>
        private string _mensagemAMostrar;
        public string MensagemAMostrar
        {
            get
            {
                return _mensagemAMostrar;
            }
            set
            {
                _mensagemAMostrar = value;
                OnPropertyChanged("MensagemAMostrar");
            }
        }



        public ActivityIndicatorTool(string activityIndicatorCor = "#000000",string mensagemAMostrar = "",string backgroundCorVisivel = "#ffffff", string backgroundCorEscondido = "#00000000")
        {
            ActivityIndicatorCor = activityIndicatorCor;
            MensagemAMostrar = mensagemAMostrar;
            _backgroundCorVisivel = backgroundCorVisivel;
            _backgroundCorEscondido = backgroundCorEscondido;

            //Inicializa-se a cor em uso com o valor da cor escondido.
            BackgroundCorEmUso = _backgroundCorEscondido;
        }



        public void ExecutarRoda()
        {
            IsVisivel = true;
            IsRunning = true;
            BackgroundCorEmUso = _backgroundCorVisivel;
        }



        public void PararRoda()
        {
            IsVisivel = false;
            IsRunning = false;
            BackgroundCorEmUso = _backgroundCorEscondido;
        }



    }
}
