using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LubyBackend.Services
{
    public static class BCryptService
    {
        public static string GenerateBCryptHash(string value, int workfactor)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(workfactor);
            return BCrypt.Net.BCrypt.HashPassword(value, salt);
        }
    }
}
