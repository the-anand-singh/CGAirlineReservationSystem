using CGAirlineReservationSystem.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.Repositories
{
    public interface IUserRepository
    {
        UserDTO Authorize(string Username, string Password);
    }
}
