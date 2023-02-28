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
    [Route("api/hotel/{hotelId}/room")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<RoomDto>> Get([FromRoute] int hotelId)
        {
            var result = _roomService.GetAll(hotelId);

            return Ok(result);
        }

        [HttpGet("{roomId}")]
        [AllowAnonymous]
        public ActionResult<RoomDto> Get([FromRoute] int hotelId, [FromRoute] int roomId)
        {
            RoomDto room = _roomService.GetById(hotelId, roomId);

            return Ok(room);
        }

        [HttpPost]
        [Authorize(Roles = "Hotel Owner")]
        public ActionResult Post([FromRoute] int hotelId, [FromBody] CreateRoomDto dto)
        {
            var newRoomId = _roomService.Create(hotelId, dto);

            return Created($"api/hotel/{hotelId}/room/{newRoomId}", null);
        }

        [HttpPut("{roomId}")]
        [Authorize(Roles = "Hotel Owner")]
        public ActionResult Put([FromRoute] int hotelId, [FromRoute] int roomId, [FromBody] UpdateRoomDto dto)
        {
            _roomService.Update(hotelId, roomId, dto);

            return Ok();
        }

        [HttpDelete("{roomId}")]
        [Authorize(Roles = "Hotel Owner")]
        public ActionResult Delete([FromRoute] int hotelId, [FromRoute] int roomId)
        {
            _roomService.DeleteById(hotelId, roomId);

            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "Hotel Owner")]
        public ActionResult Delete([FromRoute] int hotelId)
        {
            _roomService.DeleteAll(hotelId);

            return NoContent();
        }
    }
}
