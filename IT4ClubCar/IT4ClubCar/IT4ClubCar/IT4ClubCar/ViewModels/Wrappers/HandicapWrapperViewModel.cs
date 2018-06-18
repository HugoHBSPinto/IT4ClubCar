using IT4ClubCar.IT4ClubCar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class HandicapWrapperViewModel
    {
        private HandicapModel _handicapModel;

        /// <summary>
        /// Obtém e define o Valor.
        /// </summary>
        public int Valor
        {
            get
            {
                return _handicapModel.Valor;
            }
            set
            {
                _handicapModel.Valor = value;
            }
        }



        public HandicapWrapperViewModel(HandicapModel handicapModel)
        {
            _handicapModel = handicapModel;
        }



    }
}
