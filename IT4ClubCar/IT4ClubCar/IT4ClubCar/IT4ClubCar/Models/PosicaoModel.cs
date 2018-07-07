using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class PosicaoModel
    {
        /// <summary>
        /// Obtém e define a Latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Obtém e define a Longitude.
        /// </summary>
        public double Longitude { get; set; }


        public PosicaoModel(double latitude,double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

    }
}
