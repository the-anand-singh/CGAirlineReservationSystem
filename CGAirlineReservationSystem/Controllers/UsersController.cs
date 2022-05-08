using CGAirlineReservationSystem.DTOs.UserDTOs;
using CGAirlineReservationSystem.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CGAirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET api/<UsersController>/5
        [HttpGet("/user/authorize")]
        public IActionResult Authorize(string UserName, string Password)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDTO = userRepository.Authorize(UserName, Password);
                if (userDTO.IsSuccess)
                {
                    return Ok(userDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, userDTO.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
