using IT4ClubCar.IT4ClubCar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class BuracoWrapperViewModel
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
        /// Obtém os TeeBuracosDistancia.
        /// </summary>
        public ObservableCollection<TeeBuracoDistanciaWrapperViewModel> TeeBuracosDistancia { get; private set; }



        public BuracoWrapperViewModel(BuracoModel buracoModel)
        {
            _buracoModel = buracoModel;
            TeeBuracosDistancia = new ObservableCollection<TeeBuracoDistanciaWrapperViewModel>(_buracoModel.Distancias.Select(p => new TeeBuracoDistanciaWrapperViewModel(p)));
        }
        
        
        
        public void AdicionarDistancia(TeeBuracoDistanciaWrapperViewModel distancia)
        {
            _buracoModel.Distancias.Add(distancia.ObterModel());
            TeeBuracosDistancia.Add(distancia);
        }



        public BuracoModel ObterModelo()
        {
            return _buracoModel;
        }



    }
}
