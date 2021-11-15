using Core.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authentication.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        public AuthService(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<User> Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = await _userService.GetByField(u => u.Email == email);
            //var user = _userService.SingleOrDefault(u => u.Email == email);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!_userService.VerifyPasswordHash(password, user.Password))
                return null;

            // authentication successful
            return user;
        }
        
    }
}
