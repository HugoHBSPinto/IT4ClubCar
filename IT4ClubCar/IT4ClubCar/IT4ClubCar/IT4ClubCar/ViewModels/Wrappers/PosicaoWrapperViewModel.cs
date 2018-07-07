using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class PosicaoWrapperViewModel : BaseWrapperViewModel
    {
        private PosicaoModel _posicaoModel;

        /// <summary>
        /// Obtém e define a Latitude.
        /// </summary>
        public double Latitude
        {
            get
            {
                return _posicaoModel.Latitude;
            }
            set
            {
                _posicaoModel.Latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        /// <summary>
        /// Obtém e define a Longitude.
        /// </summary>
        public double Longitude
        {
            get
            {
                return _posicaoModel.Longitude;
            }
            set
            {
                _posicaoModel.Longitude = value;
                OnPropertyChanged("Longitude");
            }
        }



        public PosicaoWrapperViewModel(PosicaoModel posicaoModel)
        {
            _posicaoModel = posicaoModel;
        }

    }
}
