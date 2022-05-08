using CGAirlineReservationSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.DTOs.FlightDTOs
{
    public class FlightDTO
    {
        public bool IsSuccess { get; set; }
        public Flight Flight { get; set; }
        public string Message { get; set; }
    }
}
