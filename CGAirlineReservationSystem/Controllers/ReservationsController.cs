using CGAirlineReservationSystem.DTOs.ReservationDTOs;
using CGAirlineReservationSystem.Entities;
using CGAirlineReservationSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CGAirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {

        private readonly IReservationRepository reservationRepository;

        public ReservationsController(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }



        // GET: api/<ReservationsController>
        [HttpGet("/reservations/tickets")]
        public IActionResult GetAllTickets()
        {
            ReservationListDTO reservationListDTO = reservationRepository.GetAllTickets();
            if (reservationListDTO.IsSuccess)
            {
                return Ok(reservationListDTO);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, reservationListDTO.Message);
        }

        [Authorize]
        [HttpGet("/reservations/total-revenue")]
        public IActionResult GetTotalRevenue()
        {
            RevenueDTO revenueDTO = reservationRepository.GetTotalRevenue();
            if (revenueDTO.IsSuccess)
            {
                return Ok(revenueDTO);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, revenueDTO.Message);
        }

        // GET api/<ReservationsController>/5
        [HttpGet("/reservations/get-ticket-by-id")]
        public IActionResult GetTicketByID(int id, string PassengerName)
        {
            if (ModelState.IsValid)
            {
                ReservationDTO reservationDTO = reservationRepository.GetTicketByID(id, PassengerName);
                if (reservationDTO.IsSuccess)
                {
                    return Ok(reservationDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, reservationDTO.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("/reservations/get-ticket-by-passenger-name")]
        public IActionResult GetTicketByPassengerName(string PassengerName)
        {
            if (ModelState.IsValid)
            {
                ReservationListDTO reservationListDTO = reservationRepository.GetTicketByPassengerName(PassengerName);
                if (reservationListDTO.IsSuccess)
                {
                    return Ok(reservationListDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, reservationListDTO.Message);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet("/reservations/get-revenue-by-flight")]
        public IActionResult GetRevenueByFlight(int FlightID)
        {
            if (ModelState.IsValid)
            {
                RevenueDTO revenueDTO = reservationRepository.GetRevenueByFlight(FlightID);
                if (revenueDTO.IsSuccess)
                {
                    return Ok(revenueDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, revenueDTO.Message);
            }
            return BadRequest(ModelState);
        }

        // POST api/<ReservationsController>
        [HttpPost("/reservations/book-ticket")]
        public IActionResult BookTicket([FromBody] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                ReservationDTO reservationDTO = reservationRepository.AddTicket(reservation);
                if (reservationDTO.IsSuccess)
                {
                    return Ok(reservationDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, reservationDTO.Message);
            }
            return BadRequest(ModelState);
        }

        // 
        [HttpPut("/reservations/cancel-ticket")]
        public IActionResult CancelTicket(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                ReservationDTO reservationDTO = reservationRepository.CancelTicket(reservation);
                if (reservationDTO.IsSuccess)
                {
                    return Ok(reservationDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, reservationDTO.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
