using Microsoft.Extensions.Configuration;
using Models;
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
            var passEncrypted = value.Length > 30 && value.IndexOf(string.Format("$2a${0}$", workfactor.ToString())) > -1 ? true : false;
            string salt = BCrypt.Net.BCrypt.GenerateSalt(workfactor);

            if (!passEncrypted)
            {
                value = BCrypt.Net.BCrypt.HashPassword(value, salt);
            }
            return value;
        }
    }
}
