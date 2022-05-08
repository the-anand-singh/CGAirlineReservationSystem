using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.DTOs.ReservationDTOs
{
    public class RevenueDTO
    {
        public bool IsSuccess { get; set; }
        public float Revenue { get; set; }
        public string Message { get; set; }
    }
}
