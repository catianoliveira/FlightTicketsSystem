using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Flights.Web.Helpers
{
    public class ImageHelper : IImageHelper
    {
     
        //TODO publish video 23/06
        //TODO imagens + video 23/6 e 06/07 p/ users e bg

        public async Task<string> UploadImageAsyc(IFormFile imageFile, string folder)
        {
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";

            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\images\\{folder}",
                file);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"~/images/Users/{folder}";
        }
    }
}
