using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class CampoWrapperViewModel
    {
        private CampoModel _campoModel;

        /// <summary>
        /// Obtém o id.
        /// </summary>
        public int Id
        {
            get
            {
                return _campoModel.Id;
            }
        }

        /// <summary>
        /// Obtém o Nome.
        /// </summary>
        public string Nome
        {
            get
            {
                return _campoModel.Nome;
            }
        }

        /// <summary>
        /// Obtém o Par.
        /// </summary>
        public int Par
        {
            get
            {
                return _campoModel.Par;
            }
        }

        /// <summary>
        /// Obtém o StrokeRating.
        /// </summary>
        public int SlopeRating
        {
            get
            {
                return _campoModel.SlopeRating;
            }
        }

        /// <summary>
        /// Obtém o NumeroBuracos.
        /// </summary>
        public int NumeroBuracos
        {
            get
            {
                return _campoModel.NumeroBuracos;
            }
        }

        /// <summary>
        /// Obtém e define os Buracos.
        /// </summary>
        public ObservableCollection<BuracoWrapperViewModel> Buracos { get; private set; }



        public CampoWrapperViewModel(CampoModel campo)
        {
            _campoModel = campo;
            Buracos = new ObservableCollection<BuracoWrapperViewModel>(_campoModel.Buracos.Select(p => new BuracoWrapperViewModel(p)));
        }



        /// <summary>
        /// Adiciona um novo buraco à lista de buracos do campo.
        /// </summary>
        /// <param name="buracoAAdicionar"></param>
        public void AdicionarBuraco(BuracoWrapperViewModel buracoAAdicionar)
        {
            //Adicionar ao modelo.
            Buracos.Add(buracoAAdicionar);
            //Adicionar à propriedade.
            _campoModel.Buracos.Add(buracoAAdicionar.ObterModelo());
        }



        public CampoModel ObterModel()
        {
            return _campoModel;
        }
    }
}
