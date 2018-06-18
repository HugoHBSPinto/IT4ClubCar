using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using IT4ClubCar.IT4ClubCar.Models;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class JogoWrapperViewModel
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
        
        
        
        public JogoWrapperViewModel(JogoModel jogoModel)
        {
            _jogoModel = jogoModel;
            Campo = new CampoWrapperViewModel(_jogoModel.Campo);
            ModoJogo = new ModoJogoWrapperViewModel(_jogoModel.ModoJogo);
            Metrico = new MetricoWrapperViewModel(_jogoModel.Metrico);
            Jogadores = new ObservableCollection<JogadorWrapperViewModel>(_jogoModel.Jogadores.Select(p => new JogadorWrapperViewModel(p)));
        }



    }
}
