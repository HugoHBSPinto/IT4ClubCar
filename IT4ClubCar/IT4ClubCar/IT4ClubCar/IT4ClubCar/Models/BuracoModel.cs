using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class BuracoModel
    {
        public static BuracoModel BuracoVazio = new BuracoModel(-1, 0, 0, 0, String.Empty,0,0);


        /// <summary>
        /// Obtém e define o Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém e define o Numero.
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Obtém e define o Par.
        /// </summary>
        public int Par { get; set; }

        /// <summary>
        /// Obtém e define o StrokeIndex.
        /// </summary>
        public int StrokeIndex { get; set; }

        /// <summary>
        /// Obtém e define a Dica.
        /// </summary>
        public string Dica { get; set; }

        /// <summary>
        /// Obtém e define a Latitude.
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        /// Obtém e define a Longitude.
        /// </summary>
        public float Longitude { get; set; }



        public BuracoModel()
        {
            
        }



        public BuracoModel(int id, int numero, int par, int strokeIndex, string dica, float latitude,float longitude)
        {
            Id = id;
            Numero = numero;
            Par = par;
            StrokeIndex = strokeIndex;
            Dica = dica;
            Latitude = latitude;
            Longitude = longitude;
        }



    }
}
