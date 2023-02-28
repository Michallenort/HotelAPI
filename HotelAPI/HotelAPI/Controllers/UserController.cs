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
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,User")]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            var usersDtos = _userService.GetAll();

            return Ok(usersDtos);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<UserDto> GetById([FromRoute] int userId)
        {
            var userDto = _userService.GetById(userId);

            return Ok(userDto);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreateUserDto dto)
        {
            var id = _userService.Create(dto);

            return Created($"/api/user/{id}", null);
        }

        [HttpPost("owner")]
        [AllowAnonymous]
        public ActionResult PostOwner([FromBody] CreateUserDto dto)
        {
            dto.RoleId = 2;

            var id = _userService.Create(dto);

            return Created($"/api/user/owner/{id}", null);
        }

        [HttpPost("admin")]
        //[Authorize(Roles = "Administrator")]
        public ActionResult PostAdmin([FromBody] CreateAdminDto dto)
        {
            var id = _userService.CreateAdmin(dto);

            return Created($"/api/user/admin/{id}", null);
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete([FromRoute] int userId)
        {
            _userService.Delete(userId);

            return NoContent();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _userService.GenerateJwt(dto);

            return Ok(token);
        }
    }
}
