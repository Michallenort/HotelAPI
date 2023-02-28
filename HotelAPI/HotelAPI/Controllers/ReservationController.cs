using HotelAPI.Models;
using HotelAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [Authorize(Roles = "Hotel Owner")]
        public ActionResult<IEnumerable<ReservationDto>> GetAll([FromRoute] int hotelId, [FromRoute] int roomId)
        {
            var reservationsDtos = _reservationService.GetAll(hotelId, roomId);

            return Ok(reservationsDtos);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult Post([FromRoute] int hotelId, [FromRoute] int roomId, [FromBody] CreateReservationDto dto)
        {
            var newReservationId = _reservationService.Create(hotelId, roomId, dto);

            return Created($"api/hotel/{hotelId}/room/{roomId}/reservation/{newReservationId}", null);
        }

        [HttpDelete("{reservationId}")]
        [Authorize(Roles = "Hotel Owner,User")]
        public ActionResult Delete([FromRoute] int hotelId, [FromRoute] int roomId, [FromRoute] int reservationId)
        {
            _reservationService.Delete(hotelId, roomId, reservationId);

            return NoContent();
        }
    }
}
