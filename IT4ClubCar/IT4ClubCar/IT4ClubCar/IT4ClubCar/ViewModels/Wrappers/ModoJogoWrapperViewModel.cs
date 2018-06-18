using IT4ClubCar.IT4ClubCar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class ModoJogoWrapperViewModel
    {
        private ModoJogoModel _modoJogoModel;

        /// <summary>
        /// Obtém o Nome.
        /// </summary>
        public string Nome
        {
            get
            {
                return _modoJogoModel.Nome;
            }
        }



        public ModoJogoWrapperViewModel(ModoJogoModel modoJogoModel)
        {
            _modoJogoModel = modoJogoModel;
        }



        public ModoJogoModel ObterModel()
        {
            return _modoJogoModel;
        }
    }
}
