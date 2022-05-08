using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.DTOs.FlightDTOs
{
    public class FlightInputDTO
    {
        /*public int FlightID { get; set; }*/
        public DateTime LaunchDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DeptTime { get; set; }
        public string ArrivalTime { get; set; }
        public int NoOfSeats { get; set; }
        public float Fare { get; set; }
        public string Status { get; set; }
    }
}
