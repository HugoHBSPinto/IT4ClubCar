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
        public BuracoWrapperViewModel Buraco { get; private set; }
        
        /// <summary>
        /// Obtém e define a Latitude.
        /// </summary>
        public float Latitude
        {
            get
            {
                return _teeBuracoDistanciaModel.Latitude;
            }
        }

        /// <summary>
        /// Obtém e define a Longitude.
        /// </summary>
        public float Longitude
        {
            get
            {
                return _teeBuracoDistanciaModel.Longitude;
            }
        }
        
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
            Buraco = new BuracoWrapperViewModel(_teeBuracoDistanciaModel.Buraco);
        }
        
        
        
        public TeeBuracoDistanciaModel ObterModel()
        {
            return _teeBuracoDistanciaModel;
        }



    }
}
