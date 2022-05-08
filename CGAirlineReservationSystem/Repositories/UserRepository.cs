using CGAirlineReservationSystem.DB_Context;
using CGAirlineReservationSystem.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AirlineDbContext context;

        public UserRepository(AirlineDbContext context)
        {
            this.context = context;
        }

        public UserDTO Authorize(string Username, string Password)
        {
            UserDTO userDTO = new();
            try
            {
                var user = context.Users.Where(x => x.Username == Username && x.Password == Password).SingleOrDefault();

                userDTO.IsSuccess = true;
                UserInfo userInfo = new();
                userInfo.UserName = user.Username;
                userInfo.IsAdmin = true;

                userDTO.UserInfo = userInfo;
                userDTO.Message = "Authorized";
            }
            catch(Exception E)
            {
                userDTO.IsSuccess = false;
                userDTO.UserInfo = null;
                userDTO.Message = E.Message;
            }
            return userDTO;
        }
    }
}
