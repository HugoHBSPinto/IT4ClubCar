using IT4ClubCar.IT4ClubCar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class MetricoWrapperViewModel
    {
        private MetricoModel _metricoModel;

        /// <summary>
        /// Obtém o Id.
        /// </summary>
        public int Id
        {
            get
            {
                return _metricoModel.Id;
            }
        }

        /// <summary>
        /// Obtém o nome.
        /// </summary>
        public string Nome
        {
            get
            {
                return _metricoModel.Nome;
            }
        }



        public MetricoWrapperViewModel(MetricoModel metricoModel)
        {
            _metricoModel = metricoModel;
        }



        public MetricoModel ObterModel()
        {
            return _metricoModel;
        }
    }
}
