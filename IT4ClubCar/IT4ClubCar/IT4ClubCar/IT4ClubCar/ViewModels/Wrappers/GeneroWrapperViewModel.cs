using IT4ClubCar.IT4ClubCar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class GeneroWrapperViewModel
    {
        private GeneroModel _generoModel;

        /// <summary>
        /// Obtém o id.
        /// </summary>
        public int Id
        {
            get
            {
                return _generoModel.Id;
            }
        }

        /// <summary>
        /// Obtém o nome.
        /// </summary>
        public string Nome
        {
            get
            {
                return _generoModel.Nome;
            }
        }



        public GeneroWrapperViewModel(GeneroModel generoModel)
        {
            _generoModel = generoModel;
        }



    }
}
