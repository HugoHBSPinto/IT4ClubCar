using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IT4ClubCar.Droid.InterfacesImplementadas;
using IT4ClubCar.IT4ClubCar.Services.ScreenshotService;
using Xamarin.Forms;
using Android;
using Android.Graphics;
using System.IO;

[assembly: Dependency (typeof(ScreenshotService))]
namespace IT4ClubCar.Droid.InterfacesImplementadas
{
    class ScreenshotService : IScreenshotService
    {
        public async Task<byte[]> TirarScreenshotAsync()
        {
            var rootView = ((Activity)MainActivity.Context).Window.DecorView.RootView;

            rootView.DrawingCacheEnabled = true;

            Bitmap bitmap = rootView.GetDrawingCache(true);

            byte[] bitmapData;

            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png,0,stream);
                bitmapData = stream.ToArray();
            }

            return bitmapData;
        }
    }
}