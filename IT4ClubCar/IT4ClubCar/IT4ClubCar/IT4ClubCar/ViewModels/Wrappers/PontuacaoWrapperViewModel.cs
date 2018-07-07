using IT4ClubCar.IT4ClubCar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class PontuacaoWrapperViewModel
    {
        private PontuacaoModel _pontuacaoModel;

        /// <summary>
        /// Obtém e define o Buraco.
        /// </summary>
        public BuracoWrapperViewModel Buraco { get; set; }

        /// <summary>
        /// Obtém e define os Pontos.
        /// </summary>
        public int Pontos
        {
            get
            {
                return _pontuacaoModel.Pontos;
            }
            set
            {
                _pontuacaoModel.Pontos = value;
            }
        }



        public PontuacaoWrapperViewModel(PontuacaoModel pontuacaoModel)
        {
            _pontuacaoModel = pontuacaoModel;
            Buraco = new BuracoWrapperViewModel(_pontuacaoModel.Buraco);
        }
        
        
        
        public PontuacaoModel ObterModelo()
        {
            return _pontuacaoModel;
        }
    }
}
