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
        /// Obtém e define o Buraco.
        /// </summary>
        public BuracoModel Buraco { get; set; }
        
        /// <summary>
        /// Obtém e define a Latitude.
        /// </summary>
        public float Latitude { get; set; }
        
        /// <summary>
        /// Obtém e define a Longitude.
        /// </summary>
        public float Longitude { get; set; }
        
        /// <summary>
        /// Obtém e define a Distancia.
        /// </summary>
        public int Distancia { get; set; }



        public TeeBuracoDistanciaModel()
        {
            
        }



        public TeeBuracoDistanciaModel(BuracoModel buraco, float latitude,float longitude, int distancia)
        {
            Buraco = buraco;
            Latitude = latitude;
            Longitude = longitude;
            Distancia = distancia;
        }



    }
}
