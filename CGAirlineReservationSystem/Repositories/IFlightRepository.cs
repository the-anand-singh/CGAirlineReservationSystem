using CGAirlineReservationSystem.DTOs;
using CGAirlineReservationSystem.DTOs.FlightDTOs;
using CGAirlineReservationSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.Repositories
{
    public interface IFlightRepository
    {
        FlightandSeatDTO GetAllFlights(string origin, string destination, DateTime journeyDate, bool IsAdmin);
        FlightDTO GetFlightsByID(int id);
        FlightDTO AddFlight(Flight flight);
        FlightDTO RemoveFlight(int id);
        FlightDTO EditFlight(Flight flight);

    }
}
