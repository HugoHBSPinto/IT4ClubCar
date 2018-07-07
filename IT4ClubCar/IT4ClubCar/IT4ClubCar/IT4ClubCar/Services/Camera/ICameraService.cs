using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Camera
{
    interface ICameraService
    {
        Task<MediaFile> TirarFoto();
    }
}
