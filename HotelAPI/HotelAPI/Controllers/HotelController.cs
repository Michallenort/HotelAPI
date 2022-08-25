using AutoMapper;
using HotelAPI.Models;
using HotelAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Controllers
{
    [Route("api/hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;


        public HotelController(IHotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HotelDto>> GetAll()
        {
            var hotelsDtos = _hotelService.GetAll();

            return Ok(hotelsDtos);
        }
    }
}
