using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class TeeBuracoDistanciaWrapperViewModel
    {
        private TeeBuracoDistanciaModel _teeBuracoDistanciaModel;

        /// <summary>
        /// Obtém o Tee.
        /// </summary>
        public TeeWrapperViewModel Tee { get; private set; }

        /// <summary>
        /// Obtém a Distancia.
        /// </summary>
        public int Distancia
        {
            get
            {
                return _teeBuracoDistanciaModel.Distancia;
            }
        }



        public TeeBuracoDistanciaWrapperViewModel(TeeBuracoDistanciaModel teeBuracoDistanciaModel)
        {
            _teeBuracoDistanciaModel = teeBuracoDistanciaModel;
            Tee = new TeeWrapperViewModel(_teeBuracoDistanciaModel.Tee);
        }
        
        
        
        public TeeBuracoDistanciaModel ObterModel()
        {
            return _teeBuracoDistanciaModel;
        }



    }
}
