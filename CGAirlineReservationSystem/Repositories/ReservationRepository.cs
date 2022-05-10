using CGAirlineReservationSystem.DB_Context;
using CGAirlineReservationSystem.DTOs.ReservationDTOs;
using CGAirlineReservationSystem.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace CGAirlineReservationSystem.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AirlineDbContext context;
        private readonly IConfiguration config;

        public ReservationRepository(AirlineDbContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        public ReservationDTO AddTicket(Reservation reservation)
        {
            ReservationDTO reservationDTO = new();
            try
            {
                int BookingsMade = context.Reservations.Where(x => x.FlightID == reservation.FlightID
                && x.JourneyDate == reservation.JourneyDate && x.Status == "Booked").Select(x => x.NoOfTickets).Sum();
                var NoOfSeats = context.Flights.Where(x => x.FlightID == reservation.FlightID).Select(x => x.NoOfSeats).ToList()[0];
                if (BookingsMade + reservation.NoOfTickets > NoOfSeats)
                {
                    reservationDTO.IsSuccess = false;
                    reservationDTO.Reservation = null;
                    reservationDTO.Message = "No Seats Available on Flight";
                }
                    
                else
                {
                    reservation.TicketNo = context.Reservations.Max(x => x.TicketNo) + 1;
                    var flight = context.Flights.Where(x => x.FlightID == reservation.FlightID).SingleOrDefault();
                    reservation.TotalFare = reservation.NoOfTickets * flight.Fare;
                    reservation.Status = "Booked";
                    bool isUploaded = Helper.UploadBlob(config, reservation).Result;
                    if (isUploaded)
                    {
                        reservationDTO.IsSuccess = true;
                        reservationDTO.Reservation = reservation;
                        reservationDTO.Message = "Booking Request is being Processed";
                    }
                    else 
                    { 
                        reservationDTO.IsSuccess = false;
                        reservationDTO.Reservation = null;
                        reservationDTO.Message = "Error occured: Request could not be processed. Please try again!";
                    }
                }
            }
            catch(Exception E)
            {
                reservationDTO.IsSuccess = false;
                reservationDTO.Reservation = null;
                reservationDTO.Message = E.Message;
            }
            return reservationDTO;
        }

        public ReservationDTO CancelTicket(int TicketNo) //Reservation reservation
        {
            ReservationDTO reservationDTO = new();
            /*if (reservation.JourneyDate <= DateTime.Now)
            {
                reservationDTO.IsSuccess = false;
                reservationDTO.Reservation = reservation;
                reservationDTO.Message = "Ticket Could not be cancelled - Ticket expired!";
            }*/

            try
            {
                context.Reservations.Where(x => x.TicketNo == TicketNo).ToList().ForEach(x => x.Status = "Cancelled");
                context.SaveChanges();

                reservationDTO.IsSuccess = true;
                reservationDTO.Reservation = context.Reservations.Where(x => x.TicketNo == TicketNo).SingleOrDefault();
                reservationDTO.Message = "Ticket Cancelled - you will recieve refund in 5-7 business days";
            }
            catch (Exception E)
            {
                reservationDTO.IsSuccess = false;
                reservationDTO.Reservation = context.Reservations.Where(x => x.TicketNo == TicketNo).SingleOrDefault(); ;
                reservationDTO.Message = E.Message;
            }
            return reservationDTO;
        }

        public ReservationListDTO GetAllTickets()
        {
            ReservationListDTO reservationListDTO = new();

            try
            {
                var ticket = context.Reservations.ToList();


                reservationListDTO.IsSuccess = true;
                reservationListDTO.Reservation = ticket;
                reservationListDTO.Message = "Request Processed Successfully";
            }
            catch (Exception E)
            {
                reservationListDTO.IsSuccess = false;
                reservationListDTO.Reservation = null;
                reservationListDTO.Message = E.Message;
            }
            return reservationListDTO;
        }

        public ReservationDTO GetTicketByID(int TicketNo, string PassengerName)
        {
            ReservationDTO reservationDTO = new();
            
            try
            {
                var ticket = context.Reservations.Where(x => x.TicketNo == TicketNo && x.PassengerName == PassengerName).ToList();
                if (ticket.Count == 0)
                {
                    reservationDTO.IsSuccess = true;
                    reservationDTO.Reservation = null;
                    reservationDTO.Message = "Error: You do not have any booking with that TicketID";
                }
                else
                {
                    reservationDTO.IsSuccess = true;
                    reservationDTO.Reservation = ticket[0];
                    reservationDTO.Message = "Request Processed Successfully";
                }

                
            }
            catch (Exception E)
            {
                reservationDTO.IsSuccess = false;
                reservationDTO.Reservation = null;
                reservationDTO.Message = E.Message;
            }
            return reservationDTO;
            
        }

        public ReservationListDTO GetTicketByPassengerName(string PassengerName)
        {
            ReservationListDTO reservationListDTO = new();

            try
            {
                var tickets = context.Reservations.Where(x => x.PassengerName == PassengerName).ToList();


                reservationListDTO.IsSuccess = true;
                reservationListDTO.Reservation = tickets;
                reservationListDTO.Message = "Request Processed Successfully";
            }
            catch (Exception E)
            {
                reservationListDTO.IsSuccess = false;
                reservationListDTO.Reservation = null;
                reservationListDTO.Message = E.Message;
            }
            return reservationListDTO;

        }

        public RevenueDTO GetRevenueByFlight(int FlightID)
        {
            RevenueDTO revenueDTO = new();
            try
            {
                float revenue = context.Reservations.Where(x => x.FlightID == FlightID).Select(x => x.TotalFare).Sum();

                revenueDTO.IsSuccess = true;
                revenueDTO.Revenue = revenue;
                revenueDTO.Message = "Request processed successfully";
            }
            catch(Exception E)
            {
                revenueDTO.IsSuccess = false;
                revenueDTO.Revenue = 0;
                revenueDTO.Message = E.Message;
            }
            return revenueDTO;
        }

        public RevenueDTO GetTotalRevenue()
        {
            RevenueDTO revenueDTO = new();
            try
            {
                float revenue = context.Reservations.Select(x => x.TotalFare).Sum();

                revenueDTO.IsSuccess = true;
                revenueDTO.Revenue = revenue;
                revenueDTO.Message = "Request processed successfully";
            }
            catch (Exception E)
            {
                revenueDTO.IsSuccess = false;
                revenueDTO.Revenue = 0;
                revenueDTO.Message = E.Message;
            }
            return revenueDTO;
        }
    }
}
