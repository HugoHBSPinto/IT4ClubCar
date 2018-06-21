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



        public TeeWrapperViewModel(TeeModel tee)
        {
            _teeModel = tee;
        }



        public TeeModel ObterModel()
        {
            return _teeModel;
        }

    }
}
