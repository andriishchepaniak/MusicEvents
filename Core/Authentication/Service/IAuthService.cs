﻿using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authentication.Service
{
    public interface IAuthService
    {
        Task<User> Authenticate(string email, string password);
    }
}
