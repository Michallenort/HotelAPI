using AutoMapper;
using AutoMapper.Configuration.Conventions;
using HotelAPI.Models;
using HotelAPI.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        [HttpGet("{id}")]
        public ActionResult<HotelDto> GetById([FromRoute] int id)
        {
            var hotelDto = _hotelService.GetById(id);

            return Ok(hotelDto);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateHotelDto dto)
        {

            var id = _hotelService.Create(dto);

            return Created($"/api/hotel/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateHotelDto dto, [FromRoute] int id)
        {
            _hotelService.Update(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _hotelService.Delete(id);

            return NoContent();
        }
    }
}
