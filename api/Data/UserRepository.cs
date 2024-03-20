using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Data
{
    public class UserRepository : IUserRepository 
    {  
        public string GetAuthenticatedUserName()
        {
            return "GetAuthenticatedUserName";
        }
    }
}