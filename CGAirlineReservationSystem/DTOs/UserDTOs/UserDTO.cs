using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.DTOs.UserDTOs
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
    public class UserDTO
    {
        public bool IsSuccess { get; set; }
        public UserInfo UserInfo { get; set; }
        public string Message { get; set; }

    }
}