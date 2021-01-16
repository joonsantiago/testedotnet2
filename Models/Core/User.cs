using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Models.Core
{
    public class User
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Login { get; set; }
        public int Role { get; set; }
        
        public string Password { get; set; }
    }
}
