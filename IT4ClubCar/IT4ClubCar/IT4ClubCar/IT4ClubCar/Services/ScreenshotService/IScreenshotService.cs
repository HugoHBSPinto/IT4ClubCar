using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Services.ScreenshotService
{
    interface IScreenshotService
    {
        Task<byte[]> TirarScreenshotAsync();
    }
}
