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
    [Route("api/hotel/{hotelId}/room/{roomId}/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int hotelId, [FromRoute] int roomId, [FromBody] CreateReservationDto dto)
        {
            var newReservationId = _reservationService.Create(hotelId, roomId, dto);

            return Created($"api/hotel/{hotelId}/room/{roomId}/reservation/{newReservationId}", null);
        }
    }
}
