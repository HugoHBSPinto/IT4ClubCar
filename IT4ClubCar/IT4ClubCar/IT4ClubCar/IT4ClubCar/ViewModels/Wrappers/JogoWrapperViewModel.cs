using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class JogoWrapperViewModel : BaseWrapperViewModel
    {
        private JogoModel _jogoModel;

        /// <summary>
        /// Obtém o Campo.
        /// </summary>
        public CampoWrapperViewModel Campo { get; private set; }

        /// <summary>
        /// Obtém o modo jogo.
        /// </summary>
        public ModoJogoWrapperViewModel ModoJogo { get; private set; }

        /// <summary>
        /// Obtém o Métrico.
        /// </summary>
        public MetricoWrapperViewModel Metrico { get; private set; }

        /// <summary>
        /// Obtém os Jogadores.
        /// </summary>
        public ObservableCollection<JogadorWrapperViewModel> Jogadores { get; private set; }

        /// <summary>
        /// Obtém e define o Buraco onde o jogador se encontra no momento.
        /// </summary>
        private BuracoWrapperViewModel _buracoAtual;
        public BuracoWrapperViewModel BuracoAtual
        {
            get
            {
                return _buracoAtual;
            }
            set
            {
                _buracoAtual = value;
                OnPropertyChanged("BuracoAtual");
            }
        }

        /// <summary>
        /// Obtém e define o Buraco onde o jogador pausou o jogo.
        /// </summary>
        private BuracoWrapperViewModel _buracoPausado;
        public BuracoWrapperViewModel BuracoPausado
        {
            get
            {
                return _buracoPausado;
            }
            set
            {
                _buracoPausado = value;
            }
        }

        
        
        
        public JogoWrapperViewModel(JogoModel jogoModel)
        {
            _jogoModel = jogoModel;
            Campo = new CampoWrapperViewModel(_jogoModel.Campo);
            ModoJogo = new ModoJogoWrapperViewModel(_jogoModel.ModoJogo);
            Metrico = new MetricoWrapperViewModel(_jogoModel.Metrico);
            Jogadores = new ObservableCollection<JogadorWrapperViewModel>(_jogoModel.Jogadores.Select(p => new JogadorWrapperViewModel(p)));
            BuracoAtual = null;
            BuracoPausado = null;
        }



    }
}
