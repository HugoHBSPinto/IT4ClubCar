using IT4ClubCar.IT4ClubCar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class TeeWrapperViewModel
    {
        private TeeModel _teeModel;

        /// <summary>
        /// Obtém o Id.
        /// </summary>
        public int Id
        {
            get
            {
                return _teeModel.Id;
            }
        }

        /// <summary>
        /// Obtém o Nome.
        /// </summary>
        public string Nome
        {
            get
            {
                return _teeModel.Nome;
            }
        }

        /// <summary>
        /// Obtém os TeeBuracosDistancia.
        /// </summary>
        public ObservableCollection<TeeBuracoDistanciaWrapperViewModel> TeeBuracosDistancia { get; private set; }



        public TeeWrapperViewModel(TeeModel tee)
        {
            _teeModel = tee;
            TeeBuracosDistancia = new ObservableCollection<TeeBuracoDistanciaWrapperViewModel>(_teeModel.Distancias.Select(p => new TeeBuracoDistanciaWrapperViewModel(p)));
        }


        public void AdicionarDistancia(TeeBuracoDistanciaWrapperViewModel distancia)
        {
            _teeModel.Distancias.Add(distancia.ObterModel());
            TeeBuracosDistancia.Add(distancia);
        }



        public TeeModel ObterModel()
        {
            return _teeModel;
        }

    }
}
