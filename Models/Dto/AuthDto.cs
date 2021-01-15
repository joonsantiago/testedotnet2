using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dto
{
    /// <summary>
    /// Auth model for authentication
    /// </summary>
    public class AuthDto
    {
        /// <summary>
        /// Login User
        /// </summary>
        public string Login { get; set; }
       
        /// <summary>
        /// Password user
        /// </summary>
        public string Password { get; set; }
    }
}
