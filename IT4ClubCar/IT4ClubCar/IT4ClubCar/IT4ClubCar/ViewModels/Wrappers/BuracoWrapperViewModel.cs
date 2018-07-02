using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class BuracoWrapperViewModel : BaseWrapperViewModel
    {
        private BuracoModel _buracoModel;

        /// <summary>
        /// Obtém o Id.
        /// </summary>
        public int Id
        {
            get
            {
                return _buracoModel.Id;
            }
        }

        /// <summary>
        /// Obtém o Numero.
        /// </summary>
        public int Numero
        {
            get
            {
                return _buracoModel.Numero;
            }
        }

        /// <summary>
        /// Obtém o Par.
        /// </summary>
        public int Par
        {
            get
            {
                return _buracoModel.Par;
            }
        }

        /// <summary>
        /// Obtém o StrokeIndex.
        /// </summary>
        public int StrokeIndex
        {
            get
            {
                return _buracoModel.StrokeIndex;
            }
        }

        /// <summary>
        /// Obtém a Dica.
        /// </summary>
        public string Dica
        {
            get
            {
                return _buracoModel.Dica;
            }
        }

        /// <summary>
        /// Obtém e define a Latitude.
        /// </summary>
        public float Latitude
        {
            get
            {
                return _buracoModel.Latitude;
            }
            set
            {
                _buracoModel.Latitude = value;
            }
        }

        /// <summary>
        /// Obtém e define a Longitude.
        /// </summary>
        public float Longitude
        {
            get
            {
                return _buracoModel.Longitude;
            }
            set
            {
                _buracoModel.Longitude = value;
            }
        }



        public BuracoWrapperViewModel(BuracoModel buracoModel)
        {
            _buracoModel = buracoModel;
        }
        
        
        
        public BuracoModel ObterModelo()
        {
            return _buracoModel;
        }



    }
}
