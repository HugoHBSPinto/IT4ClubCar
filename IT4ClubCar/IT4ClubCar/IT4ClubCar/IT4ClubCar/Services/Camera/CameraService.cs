using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace IT4ClubCar.IT4ClubCar.Services.Camera
{
    class CameraService : ICameraService
    {
        public async Task<MediaFile> TirarFoto()
        {
            await CrossMedia.Current.Initialize();

            MediaFile ficheiroImagem = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            return ficheiroImagem;
        }
    }
}
