using CGAirlineReservationSystem.DB_Context;
using CGAirlineReservationSystem.DTOs.FlightDTOs;
using CGAirlineReservationSystem.Entities;
using CGAirlineReservationSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CGAirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightRepository flightRepository;

        public FlightsController(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }


        // GET: api/<FlightsController>
        [HttpGet("/flights/all-flights")]
        public IActionResult GetAllFlights(string origin, string destination, DateTime journeyDate, bool IsAdmin)
        {
            if (ModelState.IsValid)
            {
                FlightandSeatDTO flightandSeatDTO = flightRepository.GetAllFlights(origin, destination, journeyDate, IsAdmin);
                if (flightandSeatDTO.IsSuccess)
                {
                    return Ok(flightandSeatDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, flightandSeatDTO.Message);
            }
            return BadRequest(ModelState);
        }

        // GET api/<FlightsController>/5
        [HttpGet("/flights/flights-by-id")]
        public IActionResult GetFlightsByID(int id)
        {
            if (ModelState.IsValid)
            {
                FlightDTO flightDTO = flightRepository.GetFlightsByID(id);
                if (flightDTO.IsSuccess)
                {
                    return Ok(flightDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, flightDTO.Message);
            }
            return BadRequest(ModelState);
        }

        // POST api/<FlightsController>
        [Authorize]
        [HttpPost("/flights/add-flight")]
        public IActionResult AddFlight(Flight flight)
        {
            if (ModelState.IsValid)
            {
                FlightDTO flightDTO = flightRepository.AddFlight(flight);
                if (flightDTO.IsSuccess)
                {
                    return Ok(flightDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, flightDTO.Message);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/<FlightsController>/5
        [Authorize]
        [HttpDelete("/flights/remove-flight")]
        public IActionResult RemoveFlight(int id)
        {
            if (ModelState.IsValid)
            {
                FlightDTO flightDTO = flightRepository.RemoveFlight(id);
                if (flightDTO.IsSuccess)
                {
                    return Ok(flightDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, flightDTO.Message);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/<FlightsController>/5
        [Authorize]
        [HttpPut("/flights/edit-flight")]
        public IActionResult EditFlight(Flight flight)
        {
            if (ModelState.IsValid)
            {
                FlightDTO flightDTO = flightRepository.EditFlight(flight);
                if (flightDTO.IsSuccess)
                {
                    return Ok(flightDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, flightDTO.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
