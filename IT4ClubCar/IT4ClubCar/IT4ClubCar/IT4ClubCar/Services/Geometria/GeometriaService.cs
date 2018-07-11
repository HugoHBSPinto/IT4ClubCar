using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace IT4ClubCar.IT4ClubCar.Services.Geometria
{
    class GeometriaService : IGeometriaService
    {
        public double ObterDistancia(Position posicaoOrigem, Position posicaoDestino, Metrico metrico)
        {
            //6371 - Quilómetros
            //3960 - Miles
            const double raio = 6371;

            var sdlat = Math.Sin((posicaoDestino.Latitude - posicaoOrigem.Latitude) / 2);
            var sdlon = Math.Sin((posicaoDestino.Longitude - posicaoOrigem.Longitude) / 2);
            var q = sdlat * sdlat + Math.Cos(posicaoOrigem.Latitude) * Math.Cos(posicaoDestino.Latitude) * sdlon * sdlon;
            var d = 2 * raio * Math.Asin(Math.Sqrt(q));

            Distance distancia = Distance.FromKilometers(d);

            double distanciaFinal = (metrico.Equals(Metrico.Metro)) ? distancia.Meters : (distancia.Meters * 1.0936133);

            return distanciaFinal;
        }



        public Position ObterPosicaoMeio(Position posicaoOrigem, Position posicaoDestino)
        {
            double latitudeMedia = (posicaoOrigem.Latitude + posicaoDestino.Latitude) / 2;
            double longitudeMedia = (posicaoOrigem.Longitude + posicaoDestino.Longitude) / 2;

            return new Position(latitudeMedia, longitudeMedia);
        }

    }
}
