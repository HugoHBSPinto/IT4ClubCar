using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class HandicapModel
    {
        /// <summary>
        /// Obtém e define o Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém e define o Valor.
        /// </summary>
        public int Valor { get; set; }



        public HandicapModel()
        {
            
        }



        public HandicapModel(int valor)
        {
            Valor = valor;
        }



    }
}
