using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class TeeBuracoDistanciaModel
    {
        /// <summary>
        /// Obtém e define o Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém e define o Tee.
        /// </summary>
        public TeeModel Tee { get; set; }

        /// <summary>
        /// Obtém e define a Distancia.
        /// </summary>
        public int Distancia { get; set; }



        public TeeBuracoDistanciaModel()
        {
            
        }



        public TeeBuracoDistanciaModel(TeeModel tee, int distancia)
        {
            Tee = tee;
            Distancia = distancia;
        }



    }
}
