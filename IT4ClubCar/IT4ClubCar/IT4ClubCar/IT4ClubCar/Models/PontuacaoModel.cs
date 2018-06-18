using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class PontuacaoModel
    {
        /// <summary>
        /// Obtém e define o Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém e define o Buraco.
        /// </summary>
        public BuracoModel Buraco { get; set; }

        /// <summary>
        /// Obtém e define os Pontos.
        /// </summary>
        public int Pontos { get; set; }


        public PontuacaoModel()
        {
            
        }



        public PontuacaoModel(BuracoModel buraco, int pontos)
        {
            Buraco = buraco;
            Pontos = pontos;
        }



    }
}
