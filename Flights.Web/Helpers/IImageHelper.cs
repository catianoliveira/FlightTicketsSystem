using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsyc(IFormFile imageFile, string folder);

    }
}
