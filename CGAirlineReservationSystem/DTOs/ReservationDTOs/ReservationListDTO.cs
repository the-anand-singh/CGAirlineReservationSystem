using CGAirlineReservationSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.DTOs.ReservationDTOs
{
    public class ReservationListDTO
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<Reservation> Reservation { get; set; }
        public string Message { get; set; }
    }
}
