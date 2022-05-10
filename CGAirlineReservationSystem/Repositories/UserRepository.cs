using CGAirlineReservationSystem.DB_Context;
using CGAirlineReservationSystem.DTOs.UserDTOs;
using CGAirlineReservationSystem.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AirlineDbContext context;
        private readonly IConfiguration config;

        public UserRepository(AirlineDbContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        public UserDTO Authorize(string Username, string Password)
        {
            UserDTO userDTO = new();
            try
            {
                var user = context.Users.Where(x => x.Username == Username && x.Password == Password).ToList();

                if (user.Count == 0)
                {
                    userDTO.IsSuccess = false;
                    userDTO.UserInfo = null;
                    userDTO.Message = "Username/Password mismatch";

                    return userDTO;
                }

                userDTO.IsSuccess = true;

                UserInfo userInfo = new();
                userInfo.UserName = user[0].Username;
                userInfo.IsAdmin = true;
                userInfo.Token = GenerateJwtToken(user[0]);

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

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = config.GetValue<string>("Secret");
            var expiryDays = config.GetValue<int>("ExpiryDays");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = config["Issuer"],
                Audience = config["Audience"],
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("IsAdmin", user.IsAdmin.ToString())

                }),
                Expires = DateTime.Now.AddDays(expiryDays),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
