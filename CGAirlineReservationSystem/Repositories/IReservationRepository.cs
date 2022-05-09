using CGAirlineReservationSystem.DTOs.ReservationDTOs;
using CGAirlineReservationSystem.Entities;

namespace CGAirlineReservationSystem.Repositories
{
    public interface IReservationRepository
    {
        ReservationListDTO GetAllTickets();
        ReservationDTO GetTicketByID(int TicketNo, string PassengerName);
        ReservationListDTO GetTicketByPassengerName(string PassengerName);
        ReservationDTO AddTicket(Reservation reservation);
        ReservationDTO CancelTicket(Reservation reservation);
        RevenueDTO GetRevenueByFlight(int FlightID);
        RevenueDTO GetTotalRevenue();
    }
}
