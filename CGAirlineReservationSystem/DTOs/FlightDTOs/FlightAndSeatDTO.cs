using CGAirlineReservationSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.DTOs.FlightDTOs
{
    public class FlightAndSeat
    {

        
        public int FlightID { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DeptTime { get; set; }
        public string ArrivalTime { get; set; }
        public int NoOfSeats { get; set; }
        public float Fare { get; set; }
        public string Status { get; set; }
        public int NoOfSeatsAvailable { get; set; }
    }

    public class FlightandSeatDTO
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<FlightAndSeat> FlightAndSeats { get; set; }
        public string Message { get; set; }

        public FlightandSeatDTO()
        {
        }

        public FlightandSeatDTO(bool isSuccess, IEnumerable<FlightAndSeat> flightAndSeats, string message)
        {
            this.IsSuccess = isSuccess;
            this.FlightAndSeats = flightAndSeats;
            Message = message;
        }
    }
}
