using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGAirlineReservationSystem.Entities
{
    [Table("Reservations")]
    //Reservation Class contains details of the ticket booked by the users.
    public class Reservation
    {
        [Key] // TicketNo generated after Booking a flight
        [Column(TypeName = "int")]
        public int TicketNo { get; set; }

        //FlightNo of booked FLight
        [Required]
        [ForeignKey("Flights")]
        [Column(TypeName = "int")]
        public int FlightID { get; set; }


        //Date of Booking
        [Required]
        [Column(TypeName = "Date")]
        public DateTime DateOfBooking { get; set; }

        //Date of Journey
        [Required]
        [Column(TypeName = "Date")]
        public DateTime JourneyDate { get; set; }


        //Passenger Name
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string PassengerName { get; set; }

        //phone numbr of passenger
        [Required]
        [Column(TypeName = "varchar")]
        public string ContactNo { get; set; }

        //Email if od passenger
        [Required]
        [StringLength(70)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        //No of tickets booked
        [Required]
        [Column(TypeName = "int")]
        public int NoOfTickets { get; set; }

        //Total fare (No. of Tickets Booked*price of a ticket)
        [Required]
        [Column(TypeName = "decimal")]
        public float TotalFare { get; set; }

        //booked/cancelled
        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar")]
        public string Status { get; set; }

    }
}
