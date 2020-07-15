using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flights.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirplanesController : Controller
    {
        private readonly IAirplaneRepository _airplaneRepository;

        public AirplanesController(IAirplaneRepository airplaneRepository)
        {
            _airplaneRepository = airplaneRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_airplaneRepository.GetAllWithUsers());
        }
    }
}
