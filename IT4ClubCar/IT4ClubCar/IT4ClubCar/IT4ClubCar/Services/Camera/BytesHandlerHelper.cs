using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Services.Camera
{
    static class BytesHandlerHelper
    {
        public static string ConverterMediaFileEmBase64(MediaFile mediaFile)
        {
            if (mediaFile == null)
                return String.Empty;

            string imagemBase64 = "";

            using (Stream stream = mediaFile.GetStream())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    byte[] arrayByte = memoryStream.ToArray();
                    imagemBase64 = ConverterByteArrayEmBase64(arrayByte);
                }
            }

            return imagemBase64;
        }



        public static ImageSource ConverterBase64EmImageSource(string imagemBase64)
        {
            return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(imagemBase64)));
        }

        

        public static string ConverterByteArrayEmBase64(byte[] byteArray)
        {
            return Convert.ToBase64String(byteArray);
        }

    }
}
