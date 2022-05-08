using CGAirlineReservationSystem.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.DTOs.ReservationDTOs
{
    public class ReservationDTO
    {
        public bool IsSuccess { get; set; }
        public Reservation Reservation { get; set; }
        public string Message { get; set; }
    }
}
