using CGAirlineReservationSystem.DB_Context;
using CGAirlineReservationSystem.DTOs.FlightDTOs;
using CGAirlineReservationSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AirlineDbContext context;

        public FlightRepository(AirlineDbContext context)
        {
            this.context = context;
        }
        
        public FlightDTO AddFlight(Flight flight)
        {
            FlightDTO flightDTO = new ();
            try
            {
                /*flight.FlightID = Convert.ToString(Guid.NewGuid());*/
                flight.FlightID = context.Flights.Max(x => x.FlightID) + 1;
                flight.LaunchDate = DateTime.Today;
                flight.Status = "Active";

                context.Flights.Add(flight);
                context.SaveChanges();

                flightDTO.IsSuccess = true;
                flightDTO.Flight = flight;
                flightDTO.Message = "Flight successfully added to fleet";
            }
            catch (Exception E)
            {
                flightDTO.IsSuccess = false;
                flightDTO.Flight = null;
                flightDTO.Message = E.Message;
            }
            
            return flightDTO;
        }

        public FlightDTO EditFlight(Flight flight)
        {
            FlightDTO flightDTO = new ();
            try
            {
                context.Flights.Where(x => x.FlightID == flight.FlightID)
                .ToList()
                .ForEach(y =>
                {
                    y.Origin = flight.Origin;
                    y.Destination = flight.Destination;
                    y.DeptTime = flight.DeptTime;
                    y.ArrivalTime = flight.ArrivalTime;
                    y.NoOfSeats = flight.NoOfSeats;
                    y.Fare = flight.Fare;
                });

                context.SaveChanges();

                flightDTO.IsSuccess = true;
                flightDTO.Flight = flight;
                flightDTO.Message = "Edits successful";
            }

            catch (Exception E)
            {
                flightDTO.IsSuccess = false;
                flightDTO.Flight = context.Flights.Where(x => x.FlightID == flight.FlightID).SingleOrDefault();
                flightDTO.Message = E.Message;
            }
            return flightDTO;
        }

        public FlightandSeatDTO GetAllFlights(string origin, string destination, DateTime journeyDate)
        {
            FlightandSeatDTO flightandSeatDTO = new ();
            
            try
            {
                List<FlightAndSeat> result = new();
                FlightAndSeat flightAndSeat = new ();

                var flights = context.Flights.Where(x => x.Origin == origin && x.Destination == destination && x.Status == "Active").ToList();
                foreach (Flight flight in flights)
                {
                    flightAndSeat.FlightID = flight.FlightID;
                    flightAndSeat.Origin = flight.Origin;
                    flightAndSeat.Destination = flight.Destination;
                    flightAndSeat.DeptTime = flight.DeptTime;
                    flightAndSeat.ArrivalTime = flight.ArrivalTime;
                    flightAndSeat.NoOfSeats = flight.NoOfSeats;
                    flightAndSeat.Fare = flight.Fare;

                    var SeatsBooked = context.Reservations.Where(x => x.FlightID == flight.FlightID
                                                                            && x.JourneyDate == journeyDate && x.Status == "Booked").Select(x => x.NoOfTickets).Sum();

                    flightAndSeat.NoOfSeatsAvailable = flight.NoOfSeats - SeatsBooked;

                    result.Add(flightAndSeat);
                }
                
                flightandSeatDTO.IsSuccess = true;
                flightandSeatDTO.FlightAndSeats = result;
                flightandSeatDTO.Message = "List of Flights fetched Successfully";
            }
            catch(Exception E)
            {
                flightandSeatDTO.IsSuccess = false;
                flightandSeatDTO.FlightAndSeats = null;
                flightandSeatDTO.Message = E.Message;
            }
            return flightandSeatDTO;
        }

        public FlightDTO GetFlightsByID(int id)
        {
            FlightDTO flightDTO = new ();
            try
            {
                var flight = context.Flights.Where(x => x.FlightID == id).SingleOrDefault();
                
                flightDTO.IsSuccess = true;
                flightDTO.Flight = flight;
                flightDTO.Message = "Flight fetched Successfully";
            }
            catch (Exception E)
            {
                flightDTO.IsSuccess = false;
                flightDTO.Flight = null;
                flightDTO.Message = E.Message;
            }
            return flightDTO;
            
        }

        public FlightDTO RemoveFlight(int id)
        {
            FlightDTO flightDTO = new ();
            try
            {
                context.Flights.Where(x => x.FlightID == id).ToList().ForEach(x => x.Status = "Inactive");
                context.SaveChanges();

                flightDTO.IsSuccess = true;
                flightDTO.Flight = context.Flights.Where(x => x.FlightID == id).SingleOrDefault();
                flightDTO.Message = "Flight removed from ACTIVE status";
            }
            catch (Exception E)
            {
                flightDTO.IsSuccess = false;
                flightDTO.Flight = null;
                flightDTO.Message = E.Message;
            }
            return flightDTO;
        }
    }
}
